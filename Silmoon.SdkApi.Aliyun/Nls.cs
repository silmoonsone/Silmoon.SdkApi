using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Silmoon.SdkApi.Aliyun
{
    public class Nls
    {
        public static async Task<byte[]> GetVoice(AliyunRequestToken aliyunRequestToken, string appKey, string text, string voice = "Aixia")
        {
            using HttpClient httpClient = new HttpClient();
            var result = aliyunRequestToken.GetAccsssToken();
            JObject j = JObject.Parse(result);

            string url = $"https://nls-gateway.cn-shanghai.aliyuncs.com/stream/v1/tts?appkey={appKey}&voice={voice}&token={j["Token"]["Id"]}&text={HttpUtility.UrlEncode(text)}&format=mp3&sample_rate=16000";
            byte[] data = null;
            HttpResponseMessage response = await httpClient.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                data = await response.Content.ReadAsByteArrayAsync();
            }
            return data;
        }
    }
}
