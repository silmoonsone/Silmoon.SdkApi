using Aliyun.Acs.Core;
using Aliyun.Acs.Core.Exceptions;
using Aliyun.Acs.Core.Profile;
using Newtonsoft.Json.Linq;
using Silmoon.Enums;
using System;
using System.Collections.Specialized;
using System.Net;
using System.Text;
using System.Xml;

namespace Silmoon.SdkApi.Aliyun
{
    public class AliyunRequestToken
    {
        public string AccessKeyId { get; set; }
        public string AccessKeySecret { get; set; }
        public string RegionId { get; set; }

        public AliyunRequestToken(string accessKeyId, string accessKeySecret, string regionId = "cn-hangzhou")
        {
            AccessKeyId = accessKeyId;
            AccessKeySecret = accessKeySecret;
            RegionId = regionId;
        }
        public string GetAccsssToken()
        {
            IClientProfile profile = DefaultProfile.GetProfile(RegionId, AccessKeyId, AccessKeySecret);
            DefaultAcsClient client = new DefaultAcsClient(profile);
            try
            {
                // 构造请求
                CommonRequest request = new CommonRequest();
                request.Domain = "nls-meta.cn-shanghai.aliyuncs.com";
                request.Version = "2019-02-28";
                // 因为是 RPC 风格接口，需指定 ApiName(Action)
                request.Action = "CreateToken";
                // 发起请求，并得到 Response
                CommonResponse response = client.GetCommonResponse(request);
                //JObject j = JObject.Parse(response.Data);
                //var tokenId = j["Token"]["Id"];
                return response.Data;
            }
            catch (ServerException ex)
            {
                Console.WriteLine(ex.ToString());
                return null;
            }
            catch (ClientException ex)
            {
                Console.WriteLine(ex.ToString());
                return null;
            }
        }
    }
}
