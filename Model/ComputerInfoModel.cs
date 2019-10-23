using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exusiai.Model
{
    public class ComputerInfoModel
    {
        public float? CpuTemp { get; set; }
        public float? GpuTemp { get; set; }
        public float? CpuLoad { get; set; }
        public float? RamLoad { get; set; }
        public List<DiskInfoModel> DiskLoad { get; set; } = new List<DiskInfoModel>();
    }
    public class DiskInfoModel
    {
        public string Name { get; set; }
        public float? Load { get; set; }
    }
}
