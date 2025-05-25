using Jose;
using Jose.keys;
using Newtonsoft.Json.Linq;
using Silmoon.Models;
using Silmoon.SdkApi.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Silmoon.SdkApi.Warpper.Apple
{
    public class ApplePushNotification
    {
        static string GetApplePushToken(string pushKeyId, string pushKey, string teamId)
        {
            var epochNow = (int)DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalSeconds;
            var payload = new Dictionary<string, object>()
            {
                {"iss", teamId},
                {"iat", epochNow}
            };
            var extraHeaders = new Dictionary<string, object>()
            {
                {"kid", pushKeyId}
            };
            var privateKey = GetApplePushPrivateKey2(pushKey);
            return JWT.Encode(payload, privateKey, JwsAlgorithm.ES256, extraHeaders);
        }
        public static ECDsa GetApplePushPrivateKey2(string keyFileContent)
        {
            const string header = "-----BEGIN PRIVATE KEY-----";
            const string footer = "-----END PRIVATE KEY-----";

            int start = keyFileContent.IndexOf(header) + header.Length;
            int end = keyFileContent.IndexOf(footer, start);
            string base64 = keyFileContent[start..end].Replace("\n", "").Replace("\r", "").Trim();

            byte[] keyBytes = Convert.FromBase64String(base64);

            var ecdsa = ECDsa.Create();
            ecdsa.ImportPkcs8PrivateKey(keyBytes, out _);
            return ecdsa;
        }

        public static async Task<StateSet<bool, HttpStatusCode>> PushRemoteNotification(ApplePush applePush, bool isTestApns, string pushKeyId, string pushKey, string teamId)
        {
            using HttpClient httpClient = new HttpClient();
            using HttpRequestMessage requestMessage = new HttpRequestMessage();

            requestMessage.Version = new Version("2.0");


            JObject requestJson = new JObject();
            requestJson["aps"] = new JObject();
            requestJson["aps"]["alert"] = new JObject();
            if (applePush.Title != null) requestJson["aps"]["alert"]["title"] = applePush.Title;
            if (applePush.SubTitle != null) requestJson["aps"]["alert"]["subtitle"] = applePush.SubTitle;
            if (applePush.Body != null) requestJson["aps"]["alert"]["body"] = applePush.Body;

            if (applePush.Badge != null) requestJson["aps"]["badge"] = applePush.Badge;
            if (applePush.Sound != null) requestJson["aps"]["sound"] = applePush.Sound;
            if (applePush.Category != null) requestJson["aps"]["category"] = applePush.Category;
            if (applePush.Icon != null) requestJson["aps"]["icon"] = applePush.Icon;
            if (applePush.Url != null) requestJson["aps"]["url"] = applePush.Url;

            if (applePush.Data != null)
            {
                if (applePush.Data.GetType() == typeof(JObject))
                    requestJson["data"] = (JObject)applePush.Data;
                else
                    requestJson["data"] = JToken.FromObject(applePush.Data);
            }

            var str = requestJson.ToString();
            requestMessage.Content = new StringContent(str, Encoding.UTF8, "application/json");

            if (isTestApns)
                requestMessage.RequestUri = new Uri("https://api.sandbox.push.apple.com/3/device/" + applePush.DeviceToken);
            else
                requestMessage.RequestUri = new Uri("https://api.push.apple.com/3/device/" + applePush.DeviceToken);

            var authToken = GetApplePushToken(pushKeyId, pushKey, teamId);

            requestMessage.Method = HttpMethod.Post;
            requestMessage.Headers.Add("authorization", "bearer " + authToken);
            requestMessage.Headers.Add("apns-topic", applePush.AppId);
            var response = await httpClient.SendAsync(requestMessage);
            var responseBody = await response.Content.ReadAsStringAsync();

            return new StateSet<bool, HttpStatusCode>() { State = true, Data = response.StatusCode, Message = responseBody };
        }
    }
}
