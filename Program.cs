using NvAPIWrapper.GPU;
using System;
using System.Linq;

namespace nvidia_settings_cli
{
    class Program
    {
        static void SetCoolerSpeed(PhysicalGPU gpu, int coolerId, int speed)
        {
            gpu.CoolerInformation.SetCoolerSettings(coolerId, speed);
        }

        static void SetSpeedForGpu(PhysicalGPU gpu, int speed)
        {
            var coolers = gpu.CoolerInformation.Coolers;
            Console.WriteLine($"GPU: {gpu.FullName}");
            Console.WriteLine($"Found {coolers.Count()} coolers.");
            foreach (var cooler in coolers)
            {
                Console.WriteLine($"Setting cooler {cooler.CoolerId} to {speed}.");
                SetCoolerSpeed(gpu, cooler.CoolerId, speed);
            }
        }

        static void ReportSpeeds(PhysicalGPU gpu)
        {
            var coolers = gpu.CoolerInformation.Coolers;
            Console.WriteLine($"GPU: {gpu.FullName}");
            Console.WriteLine($"Found {coolers.Count()} coolers.");
            foreach (var cooler in coolers)
            {
                Console.Write($"Current Fan Speed for Cooler {cooler.CoolerId} is {gpu.CoolerInformation.CurrentFanSpeedLevel} %.");
            }
        }


        static void SetFanSpeed(int speed)
        {
            var gpus = PhysicalGPU.GetPhysicalGPUs();
            foreach (var gpu in gpus)
            {
                SetSpeedForGpu(gpu, speed);
            }
        }

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
                SetFanSpeed(speed);

                var gpus = PhysicalGPU.GetPhysicalGPUs();
                System.Threading.Thread.Sleep(1000);
                foreach (var gpu in gpus)
                {
                    ReportSpeeds(gpu);
                }
            }
        }

        static void Main(string[] args)
        {
            REPL();
        }
    }
}
