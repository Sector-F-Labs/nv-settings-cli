using NDesk.Options;
using NvAPIWrapper.GPU;
using System;

namespace nvidia_settings_cli
{
    class Program
    {
        static void REPL()
        {
            while (true)
            {
                Console.WriteLine("What speed do you want your fan to go between 0 and 100 ? ");
                var input = Console.ReadLine();
                Console.WriteLine(""); // We want a blank line for future prints
                var parsed = int.TryParse(input, out int speed);
                if (!parsed)
                {
                    Console.WriteLine("Please enter a proper number.");
                    continue;
                }
                GpuFunctions.SetFanSpeed(speed);

                var gpus = PhysicalGPU.GetPhysicalGPUs();
                System.Threading.Thread.Sleep(1000);
                foreach (var gpu in gpus)
                {
                    GpuFunctions.ReportSpeeds(gpu);
                }
            }
        }

        static void ShowHelp(OptionSet optionSet)
        {
            Console.WriteLine("Usage: nv-settings-cli [OPTIONS]");
            Console.WriteLine("Interact with Nvidia GPUs via the windows command line.");
            Console.WriteLine("If there are no options the GPU fan speed will be set to 50%");
            Console.WriteLine();
            Console.WriteLine("Options:");
            optionSet.WriteOptionDescriptions(Console.Out);
        }

        static void Main(string[] args)
        {

            bool repl = false;
            bool show_help = false;
            //bool debug = false;
            int speed = 50;

            var optionSet = new OptionSet() {
                { "r|repl",
                   "enter a read-evaluate-print-loop which will keep prompting\n" +
                      "for new speeds.",
                     v => repl = v != null
                },

                 { "s|speed=",
                   "set the fan to this speed.\n" +
                      "this must be an integer between 1 and 100.",
                    (int v) => speed = v },

                { "h|help",  "show this message and exit",
                   v => show_help = v != null },
            };


            var e = optionSet.Parse(args);

            if (show_help)
            {
                ShowHelp(optionSet);
                return;
            }
            if (repl)
            {
                REPL();
                return;
            }
            GpuFunctions.SetFanSpeed(speed);

        }
    }
}
