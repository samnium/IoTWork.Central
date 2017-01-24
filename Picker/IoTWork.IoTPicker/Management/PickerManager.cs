using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IoTWork.IoTPicker.Interfaces;
using IoTWork.Infrastructure.Statistics;

namespace IoTWork.IoTPicker.Management
{
    internal class PickerManager
    {
        public ServerManager DataServer { get; internal set; }
        public ServerManager ManagementServer { get; internal set; }

        public ushort UniqueId { get; internal set; }
        public string UniqueName { get; internal set; }

        internal void Start()
        {
            DataServer.UniqueName = UniqueName;
            ManagementServer.UniqueName = UniqueName;

            DataServer.Start();

            ManagementServer.Start();
        }

        internal void Stop()
        {
            DataServer.Stop();

            ManagementServer.Stop();
        }

        internal void Wait()
        {
            DataServer.Wait();

            ManagementServer.Wait();
        }

        internal Statistics GetStatistics()
        {
            Statistics Statistics = new Statistics();

            Statistics.Add(DataServer.GetStatistics());
            Statistics.Add(ManagementServer.GetStatistics());

            return Statistics;
        }

        internal ErrorResume GetErrors()
        {
            ErrorResume ErrorResume = new ErrorResume();

            ErrorResume.Add(DataServer.GetErrors());
            ErrorResume.Add(ManagementServer.GetErrors());

            return ErrorResume;
        }
    }
}
