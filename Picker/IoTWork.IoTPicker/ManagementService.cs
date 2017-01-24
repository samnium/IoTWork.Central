using IoTWork.Infrastructure.Interfaces;
using IoTWork.Infrastructure.Types;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Threading.Tasks;

namespace IoTWork.IoTPicker
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class ManagementService : IPickerManagementInterface
    {
        internal ConcurrentQueue<Tuple<IoTPickerCommandName, List<object>>> _commands;

        private int _requestlimit = 100;

        public ManagementService()
        {
            _commands = new ConcurrentQueue<Tuple<IoTPickerCommandName, List<object>>>();
        }

        public void AskForAlive()
        {
            if (_commands.Count < _requestlimit)
            {
                _commands.Enqueue(
                    new Tuple<IoTPickerCommandName, List<object>>
                    (IoTPickerCommandName.AskForAlive, null)
                    );
            }
            else
            {
                SetTooManyRequestsHttpResponse();
            }
        }

        public void AskForErrors()
        {
            if (_commands.Count < _requestlimit)
            {
                _commands.Enqueue(
                    new Tuple<IoTPickerCommandName, List<object>>
                    (IoTPickerCommandName.AskForErrors, null)
                    );
            }
            else
            {
                SetTooManyRequestsHttpResponse();
            }
        }

        public void AskForStatistics()
        {
            if (_commands.Count < _requestlimit)
            {
                _commands.Enqueue(
                    new Tuple<IoTPickerCommandName, List<object>>
                    (IoTPickerCommandName.AskForStatistics, null)
                    );
            }
            else
            {
                SetTooManyRequestsHttpResponse();
            }
        }

        public void AskForUpTime()
        {
            if (_commands.Count < _requestlimit)
            {
                _commands.Enqueue(
                    new Tuple<IoTPickerCommandName, List<object>>
                    (IoTPickerCommandName.AskForUpTime, null)
                    );
            }
            else
            {
                SetTooManyRequestsHttpResponse();
            }
        }

        public void Stop()
        {
            if (_commands.Count < _requestlimit)
            {
                _commands.Enqueue(
                    new Tuple<IoTPickerCommandName, List<object>>
                    (IoTPickerCommandName.Stop, null)
                    );
            }
            else
            {
                SetTooManyRequestsHttpResponse();
            }
        }

        internal bool TryGetCommand(out Tuple<IoTPickerCommandName, List<object>> command)
        {
            return _commands.TryDequeue(out command);
        }

        private void SetTooManyRequestsHttpResponse()
        {
            // http://stackoverflow.com/questions/140104/how-can-i-return-a-custom-http-status-code-from-a-wcf-rest-method

            //WebOperationContext ctx = WebOperationContext.Current;
            //ctx.OutgoingResponse.StatusCode = System.Net.HttpStatusCode.NoContent;
            throw new WebFaultException<string>("Queue is full", System.Net.HttpStatusCode.NoContent);
        }
    }
}
