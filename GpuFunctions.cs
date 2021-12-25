using NvAPIWrapper.GPU;
using System;
using System.Linq;

namespace nvidia_settings_cli
{
    class GpuFunctions
    {
        public static void SetCoolerSpeed(PhysicalGPU gpu, int coolerId, int speed)
        {
            gpu.CoolerInformation.SetCoolerSettings(coolerId, speed);
        }

        public static void SetSpeedForGpu(PhysicalGPU gpu, int speed)
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

        public static void ReportSpeeds(PhysicalGPU gpu)
        {
            var coolers = gpu.CoolerInformation.Coolers;
            Console.WriteLine($"GPU: {gpu.FullName}");
            Console.WriteLine($"Found {coolers.Count()} coolers.");
            foreach (var cooler in coolers)
            {
                Console.Write($"Current Fan Speed for Cooler {cooler.CoolerId} is {gpu.CoolerInformation.CurrentFanSpeedLevel} %.");
            }
        }

        public static void SetFanSpeed(int speed)
        {
            var gpus = PhysicalGPU.GetPhysicalGPUs();
            foreach (var gpu in gpus)
            {
                SetSpeedForGpu(gpu, speed);
            }
        }
    }
}
