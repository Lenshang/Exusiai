using Exusiai.Monitor;
using OpenHardwareMonitor.Hardware;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exusiai
{
    class ComputerData
    {
        public Computer Computer { get; set; }
        public Visitor Visitor { get; set; }
        private ComputerData()
        {
            this.Visitor = new Visitor();
            this.Computer = new Computer() {
                MainboardEnabled = true,
                CPUEnabled = true,
                RAMEnabled = true,
                GPUEnabled = true,
                FanControllerEnabled = false,
                HDDEnabled = false
            };
            this.Computer.Accept(this.Visitor);
            this.Computer.Open();
        }
        public void Refresh()
        {
            this.Computer.Accept(this.Visitor);
        }
        public IHardware GetCpu()
        {
            foreach(var item in this.Computer.Hardware)
            {
                if(item.HardwareType== HardwareType.CPU)
                {
                    return item;
                }
            }
            return null;
        }
        public IHardware GetHardware(HardwareType type)
        {
            foreach (var item in this.Computer.Hardware)
            {
                if (item.HardwareType == type)
                {
                    return item;
                }
            }
            return null;
        }

        public string GetTemperature(IHardware hardware)
        {
            foreach(var item in hardware.Sensors)
            {
                if (item.SensorType == SensorType.Temperature)
                {
                    return item.Value.ToString();
                }
            }
            return "";
        }
        public string GetLoad(IHardware hardware)
        {
            foreach (var item in hardware.Sensors)
            {
                if (item.SensorType == SensorType.Load)
                {
                    return item.Value.ToString();
                }
            }
            return "";
        }
        public string GetCpuTemperature()
        {
            var hard = this.GetCpu();
            if (hard==null)
            {
                return "";
            }
            return this.GetTemperature(hard);
        }
        public string GetGpuTemperature()
        {
            var hard = this.GetHardware(HardwareType.GpuNvidia);
            if (hard == null)
            {
                hard = this.GetHardware(HardwareType.GpuAti);
            }
            if (hard == null)
            {
                return "";
            }
            return this.GetTemperature(hard);
        }

        public string GetCpuLoad()
        {
            var hard = this.GetCpu();
            if (hard == null)
            {
                return "";
            }
            return this.GetLoad(hard);
        }
        public string GetMemoryLoad()
        {
            var hard = this.GetHardware(HardwareType.RAM);
            if (hard == null)
            {
                return "";
            }
            return this.GetLoad(hard);
        }
        private static ComputerData _computer;
        public static ComputerData Get()
        {
            if (ComputerData._computer == null)
            {
                ComputerData._computer = new ComputerData();
            }
            return ComputerData._computer;
        }
    }
}
