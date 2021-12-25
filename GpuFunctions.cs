using NvAPIWrapper.GPU;
using System;
using System.Linq;

namespace nvidia_settings_cli
{
    class GpuFunctions
    {
        public static bool DEBUG;

        private static void _SetCoolerSpeed(PhysicalGPU gpu, int coolerId, int speed)
        {
            gpu.CoolerInformation.SetCoolerSettings(coolerId, speed);
        }

        public static void SetFanSpeed(PhysicalGPU gpu, int speed)
        {
            var coolers = gpu.CoolerInformation.Coolers;
            
            Logger.Debug($"GPU: {gpu.FullName}");
            Logger.Debug($"Found {coolers.Count()} coolers.");
            foreach (var cooler in coolers)
            {
                Console.WriteLine($"Setting cooler {cooler.CoolerId} to {speed}.");
                _SetCoolerSpeed(gpu, cooler.CoolerId, speed);
            }
        }

        public static int GetTemperature(PhysicalGPU gpu)
        {
            var sensors = gpu.ThermalInformation.ThermalSensors.ToArray();
            foreach (var sensor in sensors)
            {
                Logger.Debug($"Found Sensor: {sensor.SensorId}");
            }
            return gpu.ThermalInformation.ThermalSensors.ToArray().FirstOrDefault().CurrentTemperature;
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
                SetFanSpeed(gpu, speed);
            }
        }
    }
}
