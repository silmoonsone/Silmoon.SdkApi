using Silmoon.Extensions;
using Silmoon.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AlibabaOpenApiClientConfig = AlibabaCloud.OpenApiClient.Models.Config;

namespace Silmoon.SdkApi.Aliyun
{
    public class AliyunComm
    {
        public static async Task<StateSet<bool>> SendText(string accessKeyId, string accessKeySecret, string phoneNumber, string signatureName, string templateCode, string templateParam)
        {
            var config = new AlibabaOpenApiClientConfig
            {
                AccessKeyId = accessKeyId,
                AccessKeySecret = accessKeySecret
            };

            AlibabaCloud.SDK.Dysmsapi20170525.Client client = new AlibabaCloud.SDK.Dysmsapi20170525.Client(config);
            AlibabaCloud.SDK.Dysmsapi20170525.Models.SendSmsRequest request = new AlibabaCloud.SDK.Dysmsapi20170525.Models.SendSmsRequest
            {
                PhoneNumbers = phoneNumber,
                SignName = signatureName,
                TemplateCode = templateCode,
                TemplateParam = templateParam
            };
            var result = await client.SendSmsAsync(request);

            if (result.Body.Code == "OK")
                return true.ToStateSet();
            else if (result.Body.Code == "isv.BUSINESS_LIMIT_CONTROL")
                return false.ToStateSet(result.Body.Code + ":" + result.Body.Message);
            else
                return false.ToStateSet(result.Body.Code + ":" + result.Body.Message);
        }
    }
}
