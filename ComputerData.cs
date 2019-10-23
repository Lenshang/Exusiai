using Exusiai.Model;
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
                HDDEnabled = true
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
        /// <summary>
        /// 获得指定硬件，返回默认第一个
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
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
        /// <summary>
        /// 获得指定硬件，返回全部
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public List<IHardware> GetHardwares(HardwareType type)
        {
            List<IHardware> hardwares = new List<IHardware>();
            foreach (var item in this.Computer.Hardware)
            {
                if (item.HardwareType == type)
                {
                    hardwares.Add(item);
                }
            }
            return hardwares;
        }
        /// <summary>
        /// 获得默认第一个传感器的温度
        /// </summary>
        /// <param name="hardware"></param>
        /// <returns></returns>
        public float? GetTemperature(IHardware hardware)
        {
            foreach(var item in hardware.Sensors)
            {
                if (item.SensorType == SensorType.Temperature)
                {
                    return item.Value;
                }
            }
            return 0;
        }
        /// <summary>
        /// 获得指定硬件的硬件的所有指定传感器
        /// </summary>
        /// <param name="hardware"></param>
        /// <returns></returns>
        public List<ISensor> GetSensors(IHardware hardware,SensorType sensorType)
        {
            List<ISensor> sensors = new List<ISensor>();
            foreach (var item in hardware.Sensors)
            {
                if (item.SensorType == sensorType)
                {
                    sensors.Add(item);
                }
            }
            return sensors;
        }
        public float? GetLoad(IHardware hardware)
        {
            foreach (var item in hardware.Sensors)
            {
                if (item.SensorType == SensorType.Load)
                {
                    return item.Value;
                }
            }
            return 0;
        }
        /// <summary>
        /// 获得CPU的温度
        /// </summary>
        /// <returns></returns>
        public float? GetCpuTemperature()
        {
            var hard = this.GetCpu();
            if (hard==null)
            {
                return 0;
            }
            var sensors = this.GetSensors(hard,SensorType.Temperature);
            var totalSensor = sensors.Where(i => i.Name.ToLower().Contains("total")).FirstOrDefault();
            if (totalSensor == null)
            {
                totalSensor = sensors.FirstOrDefault();
            }
            return totalSensor?.Value;
        }
        public float? GetGpuTemperature()
        {
            var hard = this.GetHardware(HardwareType.GpuNvidia);
            if (hard == null)
            {
                hard = this.GetHardware(HardwareType.GpuAti);
            }
            if (hard == null)
            {
                return 0;
            }
            return this.GetTemperature(hard);
        }

        public float? GetCpuLoad()
        {
            var hard = this.GetCpu();
            if (hard == null)
            {
                return 0;
            }
            var sensors = this.GetSensors(hard, SensorType.Load);
            var totalSensor = sensors.Where(i => i.Name.ToLower().Contains("total")).FirstOrDefault();
            if (totalSensor == null)
            {
                totalSensor = sensors.FirstOrDefault();
            }
            return totalSensor?.Value;
        }
        public float? GetMemoryLoad()
        {
            var hard = this.GetHardware(HardwareType.RAM);
            if (hard == null)
            {
                return 0;
            }
            return this.GetLoad(hard);
        }
        /// <summary>
        /// 获得磁盘的信息
        /// </summary>
        /// <returns></returns>
        public List<DiskInfoModel> GetDiskLoad()
        {
            List<DiskInfoModel> result = new List<DiskInfoModel>();
            var disks = this.GetHardwares(HardwareType.HDD);
            foreach(var disk in disks)
            {
                var loadValue = this.GetLoad(disk);
                result.Add(new DiskInfoModel() {
                    Name=disk.Name,
                    Load= loadValue
                });
            }
            return result;
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
