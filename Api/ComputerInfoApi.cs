using Exusiai.Model;
using Nancy;
using OpenHardwareMonitor.Hardware;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exusiai.Api
{
    public class ComputerInfoApi : NancyModule
    {
        public ComputerInfoApi()
        {
#if DEBUG
            After.AddItemToEndOfPipeline((ctx) => ctx.Response
            .WithHeader("Access-Control-Allow-Origin", "*")
            .WithHeader("Access-Control-Allow-Methods", "POST,GET,OPTIONS")
            .WithHeader("Access-Control-Allow-Headers", "Accept, Origin, Content-type"));
#endif

            Get("/",x=> {
                return View["Web/index.html"];
            });
            Get("/GetTemp", x => {
                var cdata = ComputerData.Get();
                cdata.Refresh();//todo 可能需要改成独立线程刷新
                //Dictionary<string, string> result = new Dictionary<string, string>();
                //result.Add("cputemp", cdata.GetCpuTemperature());
                //result.Add("gputemp", cdata.GetGpuTemperature());
                //result.Add("cpuload", cdata.GetCpuLoad());
                //result.Add("ramload", cdata.GetMemoryLoad());
                ComputerInfoModel result = new ComputerInfoModel() {
                    CpuLoad= cdata.GetCpuLoad(),
                    CpuTemp= cdata.GetCpuTemperature(),
                    RamLoad= cdata.GetMemoryLoad(),
                    GpuTemp= cdata.GetGpuTemperature(),
                    DiskLoad=cdata.GetDiskLoad()
                };
                return Response.AsJson(result);
            });
            Get("/test", x => {

                var cpu= ComputerData.Get().GetCpu();
                if (cpu == null)
                {
                    return "Cant find cpu";
                }

                return ComputerData.Get().GetTemperature(cpu);
            });
        }
    }
}
