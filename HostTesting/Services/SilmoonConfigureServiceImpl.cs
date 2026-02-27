using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Linq;
using Silmoon.AspNetCore.Interfaces;
using Silmoon.AspNetCore.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Silmoon.AI.HostingTest.Services
{
    public class SilmoonConfigureServiceImpl : SilmoonConfigureService
    {
        ILogger<ISilmoonConfigureService> Logger { get; set; }

        public SilmoonConfigureServiceImpl(IOptions<SilmoonConfigureServiceOption> options, ILogger<ISilmoonConfigureService> logger) : base(options)
        {
            Logger = logger;
            Logger.LogInformation($"当前配置文件{CurrentConfigFilePath}");
        }
    }
}
