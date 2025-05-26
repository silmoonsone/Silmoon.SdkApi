using Silmoon.Extension.Enums;
using Silmoon.Extension;
using Silmoon.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Silmoon.SdkApi.Aliyun.Models;

namespace Silmoon.SdkApi.Aliyun
{
    public class AliyunOcr
    {
        public static StateSet<bool, RecognizeIdCardResult> RecognizeIdCard(string accessKeyId, string accessKeySecret, byte[] imageData)
        {
            AlibabaCloud.OpenApiClient.Models.Config config = new AlibabaCloud.OpenApiClient.Models.Config
            {
                AccessKeyId = accessKeyId,
                AccessKeySecret = accessKeySecret,
                Endpoint = "ocr-api.cn-hangzhou.aliyuncs.com",
            };

            var client = new AlibabaCloud.SDK.Ocr_api20210707.Client(config);


            using Stream bodyStream = imageData.GetStream();
            AlibabaCloud.SDK.Ocr_api20210707.Models.RecognizeIdcardRequest recognizeIdcardRequest = new AlibabaCloud.SDK.Ocr_api20210707.Models.RecognizeIdcardRequest
            {
                Body = bodyStream,
                OutputFigure = true,
                OutputQualityInfo = true,
            };
            AlibabaCloud.TeaUtil.Models.RuntimeOptions runtime = new AlibabaCloud.TeaUtil.Models.RuntimeOptions();
            try
            {
                var response = client.RecognizeIdcardWithOptions(recognizeIdcardRequest, runtime);

                if (response.StatusCode == 200 && response.Body.Code is null)
                {
                    JObject data = JsonConvert.DeserializeObject<JObject>(response.Body.Data);
                    if (data["data"]["face"] != null)
                    {
                        RecognizeIdCardResult result2 = new RecognizeIdCardResult
                        {
                            IsFaceSide = true,
                            Name = data["data"]["face"]["data"]["name"].Value<string>(),
                            Address = data["data"]["face"]["data"]["address"].Value<string>(),
                            Ethnicity = data["data"]["face"]["data"]["ethnicity"].Value<string>(),
                            Sex = data["data"]["face"]["data"]["sex"].Value<string>() == "男" ? Sex.Male : Sex.Female,
                            BirthDate = data["data"]["face"]["data"]["birthDate"].Value<DateTime>(),
                            IdNumber = data["data"]["face"]["data"]["idNumber"].Value<string>(),

                            IsReshoot = data["data"]["face"]["warning"]["isReshoot"].Value<bool>(),
                            IsCopy = data["data"]["face"]["warning"]["isCopy"].Value<bool>(),
                            CompletenessScore = data["data"]["face"]["warning"]["completenessScore"].Value<double>(),
                            QualityScore = data["data"]["face"]["warning"]["qualityScore"].Value<double>(),
                            TamperScore = data["data"]["face"]["warning"]["tamperScore"].Value<double>(),
                        };
                        return true.ToStateSet(result2);
                    }
                    if (data["data"]["back"] != null)
                    {
                        string[] validPeriod = data["data"]["back"]["data"]["validPeriod"].Value<string>().Split('-');
                        RecognizeIdCardResult result = new RecognizeIdCardResult
                        {
                            IsFaceSide = false,
                            IssueAuthority = data["data"]["back"]["data"]["issueAuthority"].Value<string>(),
                            ValidFrom = DateTime.Parse(validPeriod[0]),
                            ValidTo = DateTime.Parse(validPeriod[1]),

                            IsReshoot = data["data"]["back"]["warning"]["isReshoot"].Value<bool>(),
                            IsCopy = data["data"]["back"]["warning"]["isCopy"].Value<bool>(),
                            CompletenessScore = data["data"]["back"]["warning"]["completenessScore"].Value<double>(),
                            QualityScore = data["data"]["back"]["warning"]["qualityScore"].Value<double>(),
                            TamperScore = data["data"]["back"]["warning"]["tamperScore"].Value<double>(),
                        };
                        return true.ToStateSet(result);
                    }
                    return false.ToStateSet<RecognizeIdCardResult>(null, "no face or back data");
                }
                return false.ToStateSet<RecognizeIdCardResult>(null, response.Body.Message);
            }
            catch (Exception exception)
            {
                return false.ToStateSet<RecognizeIdCardResult>(null, exception.Message);
            }
        }
    }
}
