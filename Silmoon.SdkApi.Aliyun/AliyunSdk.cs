using Aliyun.Acs.afs.Model.V20180112;
using Aliyun.Acs.Core;
using Aliyun.Acs.Core.Profile;
using Silmoon.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Silmoon.SdkApi.Aliyun
{
    public class AliyunSdk
    {
        public static async Task<StateSet<bool>> AfsValidate(IClientProfile profile, string scene, string appkey, string sessionId, string sig, string token, string ipAddressStr)
        {
            profile.AddEndpoint("cn-hangzhou", "cn-hangzhou", "afs", "afs.aliyuncs.com");
            var client = new DefaultAcsClient(profile);


            AuthenticateSigRequest request = new AuthenticateSigRequest
            {
                SessionId = sessionId,
                Sig = sig,
                Token = token,
                Scene = scene,
                AppKey = appkey,
                RemoteIp = ipAddressStr
            };

            try
            {
                return await Task.Run(() =>
                {
                    AuthenticateSigResponse response = client.GetAcsResponse(request);// 返回code 100表示验签通过，900表示验签失败
                    return response.Code == 100 ? StateSet<bool>.Create(true) : StateSet<bool>.Create(false, response.Msg);
                });
            }
            catch (Exception e)
            {
                return StateSet<bool>.Create(false, "EXCEPTION: " + e.Message);
            }

        }
    }
}
