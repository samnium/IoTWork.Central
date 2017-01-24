using IotWork.Utils.Helpers;
using IoTWork.IoTPicker.Interfaces;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.CommunicationFramework.Clients;
using System.CommunicationFramework.Common;
using System.CommunicationFramework.Interfaces;
using System.CommunicationFramework.Servers;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace IoTWork.IoTPicker.Servers
{
    public class UdpServer: IServer
    {
        private UdpMessageServer<byte[]> udpMessageServer;

        Uri _uri;
        System.Net.IPAddress _ipaddress;
        int _port;

        Boolean _mustrun;
        Thread _thread;

        byte[] _tmpReceiveBuffer;
        ConcurrentQueue<NetworkPacket> _messagequeue;
        ConcurrentQueue<NetworkPacket> _inputqueue;
        ConcurrentQueue<byte[]> _outputqueue;

        public UdpServer()
        {
            _tmpReceiveBuffer = new byte[0];

            _messagequeue = new ConcurrentQueue<NetworkPacket>();
            _inputqueue = new ConcurrentQueue<NetworkPacket>();
            _outputqueue = new ConcurrentQueue<byte[]>();
        }

        public void SetUri(Uri uri)
        {
            _uri = uri;
            _ipaddress = _uri.Host == "localhost" ? System.Net.IPAddress.Loopback : System.Net.IPAddress.Parse(_uri.Host);
            _port = _uri.Port;
        }

        public void Start()
        {
            _mustrun = true;
            _thread = new Thread(_Run);
            _thread.Start();

            IMessageEncoder<byte[]> encoder = new ByteEchoEncoder();

            this.udpMessageServer = new UdpMessageServer<byte[]>("IoTPicker UDP", _ipaddress, _port, 1024*10, 1024 * 20, encoder);
            this.udpMessageServer.Started += ServerStarted;
            this.udpMessageServer.Stopped += ServerStopped;
            this.udpMessageServer.GeneralEvent += ServerGeneralEvent;
            this.udpMessageServer.MessageReceived += UdpMessageReceived;
            this.udpMessageServer.DecodingError += DecodingError;

            this.udpMessageServer.Start();
        }

        public void Stop()
        {
            this.udpMessageServer.Stop();
        }

        public bool TryGetMessage(out NetworkPacket packet)
        {
            packet = null;
            return _inputqueue.TryDequeue(out packet);
        }

        private void ServerStarted(object sender, EventArgs e)
        {
        }

        private void ServerStopped(object sender, EventArgs e)
        {
        }

        private void ServerGeneralEvent(object sender, CancellableMethodManagerEventArgs e)
        {
            if (e.Exception != null)
            {
                //Logger.Log("{0}. {1}. Exception: {2}.", (sender as CfServer).ServerIdentifier, e.Message, e.Exception);
            }
            else
            {
                //Logger.Log("{0}. {1}.", (sender as CfServer).ServerIdentifier, e.Message);
            }
        }

        private void UdpMessageReceived(object sender, ReceivedMessageEventArgs<byte[]> e)
        {
            NetworkPacket pkt = new NetworkPacket();
            pkt.Message = e.ReceivedMessageInfo.Message;
            pkt.RemoteEndPoint = e.ReceivedMessageInfo.RemoteEndPoint;

            _messagequeue.Enqueue(pkt);
        }

        private void DecodingError(object sender, DecodingErrorEventArgs e)
        {
            string mesage = string.Format(
                "Exception while decoding received a datagram.\nException:\n{0}",
                e.Exception.Message);
            e.Handled = true;
        }

        private void _Run()
        {
            while(_mustrun)
            {
                byte[] message = null;
                NetworkPacket packet = null;

                bool dequeued = false;

                try
                {
                    dequeued = _messagequeue.TryDequeue(out packet);
                }
                catch(Exception)
                {
                    dequeued = false;
                }

                if (dequeued)
                {
                    try
                    {
                        message = packet.Message;
                        _tmpReceiveBuffer = new byte[0];
                        var assembledmessage = TryAssembleFrame(message);
                        if (assembledmessage != null)
                        {
                            NetworkPacket pkt = new NetworkPacket();
                            pkt.Message = assembledmessage;
                            pkt.RemoteEndPoint = packet.RemoteEndPoint;
                            _inputqueue.Enqueue(pkt);
                        }
                    }
                    catch (Exception)
                    {
                    }
                }

                Thread.Sleep(50);
            }
        }

        private byte[] TryAssembleFrame(byte[] data)
        {
            Boolean gotdata = false;
            byte[] receivedbytes = null;

            // get data
            if (data != null)
            {
                _tmpReceiveBuffer = IoTWorkHelper.ArrayMerge<byte>(_tmpReceiveBuffer, data);
                gotdata = true;
            }

            // try build a frame
            if (gotdata && _tmpReceiveBuffer.Length > 4)
            {
                byte[] lengthbytes = new byte[4];

                lengthbytes[0] = _tmpReceiveBuffer[3];
                lengthbytes[1] = _tmpReceiveBuffer[2];
                lengthbytes[2] = _tmpReceiveBuffer[1];
                lengthbytes[3] = _tmpReceiveBuffer[0];

                uint length = BitConverter.ToUInt32(lengthbytes, 0);

                if (_tmpReceiveBuffer.Length + 4 >= length)
                {
                    receivedbytes = new byte[length];

                    Array.Copy(_tmpReceiveBuffer, 4, receivedbytes, 0, length);

                    if (_tmpReceiveBuffer.Length + 4 > length + 4)
                    {
                        byte[] tb = new byte[_tmpReceiveBuffer.Length - 4 - length];
                        Array.Copy(_tmpReceiveBuffer, length + 4, tb, 0, _tmpReceiveBuffer.Length - 4 - length);
                        _tmpReceiveBuffer = tb;
                    }
                    else
                    {
                        _tmpReceiveBuffer = null;
                    }
                }
            }

            return receivedbytes;
        }
    }
}