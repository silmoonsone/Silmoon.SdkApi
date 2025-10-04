using Newtonsoft.Json.Linq;
using SendGrid;
using SendGrid.Helpers.Mail;
using Silmoon.Extension;
using Silmoon.Models;
using Silmoon.Secure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Silmoon.SdkApi.Integrate.Mail
{
    public class SendGridSdk
    {
        SendGridClient SendGrid { get; set; }
        string ApiKey { get; set; }
        public SendGridSdk(string apiKey)
        {
            ApiKey = apiKey;
            SendGrid = new SendGridClient(apiKey);
        }
        public async Task<Response> _sendEmailVerifyCode(string Email, string VerifyCode, string Action)
        {
            var from = new EmailAddress("no-reply@corp.silmoon.com", "SILMOON账户系统");
            var to = new EmailAddress("sone@silmoon.com");
            var msg = new SendGridMessage();
            msg.SetFrom(from);
            msg.AddTo(to);
            msg.SetTemplateId("d-32718b5db5064847aafe65dcf1fb6c14");
            msg.SetTemplateData(new JObject
            {
                ["action"] = Action,
                ["email"] = Email,
                ["verifycode"] = VerifyCode,
            });
            var response = await SendGrid.SendEmailAsync(msg);
            return response;
        }
        public async Task<StateSet<bool>> SendEmailVerifyCode(string Email, string TraceId, string Action)
        {
            if (Email.IsEmail())
            {
                var verifyCode = HashHelper.RandomNumbers(6);
                var response = await _sendEmailVerifyCode(Email, verifyCode, Action);
                if (response.IsSuccessStatusCode)
                {
                    VerifyCodeCacheHelper.SetVerifyCodeCache(TraceId, verifyCode, 300);
                    return true.ToStateSet("验证码已发送。");
                }
                else
                {
                    return false.ToStateSet("验证码发送失败(" + await response.Body.ReadAsStringAsync() + ")。");
                }
            }
            else return false.ToStateSet("邮箱格式错误。");
        }
    }
}
