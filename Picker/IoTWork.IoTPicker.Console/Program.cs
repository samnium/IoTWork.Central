using CommandLine;
using IoTWork.IoTPicker;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace IoTWork.IoTPicker.Console
{
    class Options
    {
        [Option('n', "networkid", DefaultValue = 1, Required = false, HelpText = "The ring id of the network.")]
        public int NetworkId { get; set; }

        [Option('r', "ringid", DefaultValue = 1, Required =false, HelpText = "The ring id of the network.")]
        public int RingId { get; set; }

        [Option('i', "id", DefaultValue = -1, HelpText = "The id of the IoTPicker.")]
        public int Id { get; set; }

        [Option('c', "central", DefaultValue = false, HelpText = "Enable communication with the central newtork manager")]
        public bool EnableHookToCentral { get; set; }

        [Option('d', "database", DefaultValue = false, HelpText = "Enable writing on database")]
        public bool EnableDatabaseWriting { get; set; }

        [Option('a', "agent", DefaultValue = false, HelpText = "Enable custom agents for data manipulation")]
        public bool EnableCustomAgents { get; set; }

        [Option('m', "management", DefaultValue = false, HelpText = "Enable custom agents for data manipulation")]
        public bool EnableManagementInterface { get; set; }

        [Option('a', "address", DefaultValue = "", HelpText = "The base address of the management interface of the IoTPicker.")]
        public string ManagementAddress { get; set; }

        [HelpOption]
        public string GetUsage()
        {
            // this without using CommandLine.Text
            var usage = new StringBuilder();
            usage.AppendLine("IoTPicker Application 1.0");
            usage.AppendLine("Read user manual for usage instructions...");
            return usage.ToString();
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var options = new Options();
            Parser parser = new CommandLine.Parser();
            if (parser.ParseArguments(args, options))
            {
                Manager manager = new Manager();
                manager.Run(options.NetworkId, options.RingId, options.Id, options.ManagementAddress,
                    options.EnableCustomAgents, options.EnableDatabaseWriting, options.EnableHookToCentral, options.EnableManagementInterface);
            }
        }
    }
}
