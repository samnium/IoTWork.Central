using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IoTWork.Communication.Model;
using IoTWork.Infrastructure.Types;

namespace IoTWork.Communication.Helpers
{
    public static class IoTCommHelper
    {
        public static string NuriToString(IoTCommNuri nuri)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append(nuri.NetworkOrdinal.ToString());
            sb.Append('.');
            sb.Append(nuri.RegionOrdinal.ToString());
            if (nuri.RingOrdinal.HasValue)
            {
                sb.Append('.');
                sb.Append(nuri.RingOrdinal.ToString());
            }
            if (nuri.DeviceOrdinal.HasValue)
            {
                sb.Append('.');
                sb.Append(nuri.DeviceOrdinal.ToString());
            }
            if (!String.IsNullOrEmpty(nuri.DeviceEntityOrdinal))
            {
                sb.Append('.');
                sb.Append(nuri.DeviceEntityOrdinal.ToString());
            }

            return sb.ToString();
        }

        public static IoTCommNuri StringToNuri(string nuri)
        {
            IoTCommNuri _nuri = new IoTCommNuri();

            var ids = nuri.Split('.');

            if (ids.Length >= 1)
            {
                _nuri.NetworkOrdinal = Int32.Parse(ids[0]);
                _nuri.Layer = IoTCommNuriLayer.Network;
            }
            if (ids.Length >= 2)
            {
                _nuri.RegionOrdinal = Int32.Parse(ids[1]);
                _nuri.Layer = IoTCommNuriLayer.Region;
            }
            if (ids.Length >= 3)
            {
                _nuri.RingOrdinal = Int32.Parse(ids[2]);
                _nuri.Layer = IoTCommNuriLayer.Ring;
            }
            if (ids.Length >= 4)
            {
                _nuri.DeviceOrdinal = Int32.Parse(ids[3]);
                _nuri.Layer = IoTCommNuriLayer.Device;
            }
            if (ids.Length >= 5)
            {
                _nuri.DeviceEntityOrdinal = ids[4];
                _nuri.Layer = IoTCommNuriLayer.DeviceEntity;
            }

            return _nuri;
        }
    }
}
