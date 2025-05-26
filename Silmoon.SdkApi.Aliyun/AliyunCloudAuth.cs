using AlibabaCloud.SDK.Cloudauth20190307.Models;
using Silmoon.Extension;
using Silmoon.Models;
using Silmoon.Secure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Silmoon.SdkApi.Aliyun
{
    public class AliyunCloudAuth
    {
        static AlibabaCloud.SDK.Cloudauth20190307.Client getClient(string accessKeyId, string accessKeySecret)
        {
            AlibabaCloud.OpenApiClient.Models.Config config = new AlibabaCloud.OpenApiClient.Models.Config
            {
                AccessKeyId = accessKeyId,
                AccessKeySecret = accessKeySecret,
            };
            config.Endpoint = "cloudauth.aliyuncs.com";

            var client = new AlibabaCloud.SDK.Cloudauth20190307.Client(config);
            return client;
        }

        public static StateSet<bool, InitFaceVerifyResponse> FaceVerifyInitial_ID_PRO(string accessKeyId, string accessKeySecret, string orderId, long sceneId, string metaInfo, string name, string cardNo, string callbackUrl, bool enableVideo = false)
        {
            var client = getClient(accessKeyId, accessKeySecret);
            InitFaceVerifyRequest initFaceVerifyRequest = new InitFaceVerifyRequest
            {
                SceneId = sceneId,
                OuterOrderNo = orderId,
                ProductCode = "ID_PRO",
                Model = "LIVENESS",
                CertType = "IDENTITY_CARD",
                CertName = name,
                CertNo = cardNo,
                ReturnUrl = callbackUrl,
                MetaInfo = metaInfo,
                CertifyUrlStyle = "L",
                VideoEvidence = enableVideo.ToString().ToLower(),
            };
            AlibabaCloud.TeaUtil.Models.RuntimeOptions runtime = new AlibabaCloud.TeaUtil.Models.RuntimeOptions();
            try
            {
                var result = client.InitFaceVerifyWithOptions(initFaceVerifyRequest, runtime);
                return (result.Body.Code == "200").ToStateSet(result, result.Body?.Message);
            }
            catch (Exception ex)
            {
                return false.ToStateSet<InitFaceVerifyResponse>(null, ex.Message);
            }
        }

        public static StateSet<bool, InitFaceVerifyResponse> FaceVerifyInitial_PV_FV(string accessKeyId, string accessKeySecret, string orderId, long sceneId, string metaInfo, string userId, string? name, string? cardNo, byte[] imageData, string callbackUrl, bool enableVideo = false)
        {
            var client = getClient(accessKeyId, accessKeySecret);
            InitFaceVerifyRequest initFaceVerifyRequest = new InitFaceVerifyRequest
            {
                SceneId = sceneId,
                OuterOrderNo = orderId,
                ProductCode = "PV_FV",
                Model = "LIVENESS",
                CertType = "IDENTITY_CARD",
                CertName = name,
                CertNo = cardNo,
                ReturnUrl = callbackUrl,
                MetaInfo = metaInfo,
                FaceContrastPicture = Convert.ToBase64String(imageData),
                UserId = userId,
                CertifyUrlStyle = "L",
                VideoEvidence = enableVideo.ToString().ToLower(),
            };
            AlibabaCloud.TeaUtil.Models.RuntimeOptions runtime = new AlibabaCloud.TeaUtil.Models.RuntimeOptions();
            try
            {
                var result = client.InitFaceVerifyWithOptions(initFaceVerifyRequest, runtime);
                return (result.Body.Code == "200").ToStateSet(result, result.Body?.Message);
            }
            catch (Exception ex)
            {
                return false.ToStateSet<InitFaceVerifyResponse>(null, ex.Message);
            }
        }
        public static StateSet<bool, InitFaceVerifyResponse> FaceVerifyInitial_PV_FV(string accessKeyId, string accessKeySecret, string orderId, long sceneId, string metaInfo, string userId, string? name, string? cardNo, string imageUrl, string callbackUrl, bool enableVideo = false)
        {
            var client = getClient(accessKeyId, accessKeySecret);
            InitFaceVerifyRequest initFaceVerifyRequest = new InitFaceVerifyRequest
            {
                SceneId = sceneId,
                OuterOrderNo = orderId,
                ProductCode = "PV_FV",
                Model = "LIVENESS",
                CertType = "IDENTITY_CARD",
                CertName = name,
                CertNo = cardNo,
                ReturnUrl = callbackUrl,
                MetaInfo = metaInfo,
                FaceContrastPictureUrl = imageUrl,
                UserId = userId,
                CertifyUrlStyle = "L",
                VideoEvidence = enableVideo.ToString().ToLower(),
            };
            AlibabaCloud.TeaUtil.Models.RuntimeOptions runtime = new AlibabaCloud.TeaUtil.Models.RuntimeOptions();
            try
            {
                var result = client.InitFaceVerifyWithOptions(initFaceVerifyRequest, runtime);
                return (result.Body.Code == "200").ToStateSet(result, result.Body?.Message);
            }
            catch (Exception ex)
            {
                return false.ToStateSet<InitFaceVerifyResponse>(null, ex.Message);
            }
        }
        public static StateSet<bool, InitFaceVerifyResponse> FaceVerifyInitial_PV_FV(string accessKeyId, string accessKeySecret, string orderId, long sceneId, string metaInfo, string userId, string? name, string? cardNo, string ossBucketName, string ossObjectName, string callbackUrl, bool enableVideo = false)
        {
            var client = getClient(accessKeyId, accessKeySecret);
            InitFaceVerifyRequest initFaceVerifyRequest = new InitFaceVerifyRequest
            {
                SceneId = sceneId,
                OuterOrderNo = orderId,
                ProductCode = "PV_FV",
                Model = "LIVENESS",
                CertType = "IDENTITY_CARD",
                CertName = name,
                CertNo = cardNo,
                ReturnUrl = callbackUrl,
                MetaInfo = metaInfo,
                OssBucketName = ossBucketName,
                OssObjectName = ossObjectName,
                UserId = userId,
                CertifyUrlStyle = "L",
                VideoEvidence = enableVideo.ToString().ToLower(),
            };
            AlibabaCloud.TeaUtil.Models.RuntimeOptions runtime = new AlibabaCloud.TeaUtil.Models.RuntimeOptions();
            try
            {
                var result = client.InitFaceVerifyWithOptions(initFaceVerifyRequest, runtime);
                return (result.Body.Code == "200").ToStateSet(result, result.Body?.Message);
            }
            catch (Exception ex)
            {
                return false.ToStateSet<InitFaceVerifyResponse>(null, ex.Message);
            }
        }

        public static StateSet<bool, DescribeFaceVerifyResponse> DescribeFaceVerify(string accessKeyId, string accessKeySecret, long sceneId, string certifyId)
        {
            var client = getClient(accessKeyId, accessKeySecret);
            DescribeFaceVerifyRequest describeFaceVerifyRequest = new DescribeFaceVerifyRequest()
            {
                CertifyId = certifyId,
                SceneId = sceneId,
            };
            AlibabaCloud.TeaUtil.Models.RuntimeOptions runtime = new AlibabaCloud.TeaUtil.Models.RuntimeOptions();
            try
            {
                var result = client.DescribeFaceVerifyWithOptions(describeFaceVerifyRequest, runtime);
                return (result.Body.Code == "200").ToStateSet(result);
            }
            catch (Exception e)
            {
                return false.ToStateSet<DescribeFaceVerifyResponse>(null, e.Message);
            }
        }

        public static StateSet<bool, Id2MetaVerifyResponse> Id2MetaVerify(string accessKeyId, string accessKeySecret, string name, string cardNo)
        {
            var client = getClient(accessKeyId, accessKeySecret);
            Id2MetaVerifyRequest id2MetaVerifyRequest = new Id2MetaVerifyRequest
            {
                IdentifyNum = cardNo,
                UserName = name,
                ParamType = "normal",
            };
            AlibabaCloud.TeaUtil.Models.RuntimeOptions runtime = new AlibabaCloud.TeaUtil.Models.RuntimeOptions();


            try
            {
                var result = client.Id2MetaVerifyWithOptions(id2MetaVerifyRequest, runtime);
                return (result.Body.Code == "200").ToStateSet(result, result.Body?.Message);
            }
            catch (Exception ex)
            {
                return false.ToStateSet<Id2MetaVerifyResponse>(null, ex.Message);
            }

        }
    }
}
