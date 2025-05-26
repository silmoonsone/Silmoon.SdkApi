using System;
using System.Collections.Generic;
using System.Text;

namespace Silmoon.SdkApi.Apple.Models
{
    public class ApplePush
    {
        public required string AppId { get; set; }
        public required string DeviceToken { get; set; }
        public string Title { get; set; }
        public string SubTitle { get; set; }
        public string Body { get; set; }
        public int? Badge { get; set; }
        public string Sound { get; set; } = "default";
        public object Data { get; set; }

        public string Category { get; set; }
        public string Icon { get; set; }
        public string Url { get; set; }
    }
}
