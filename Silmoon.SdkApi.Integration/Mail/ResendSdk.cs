using Resend;
using Silmoon.Extension;
using Silmoon.Models;
using Silmoon.SdkApi.Integration.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Silmoon.SdkApi.Integration.Mail
{
    public class ResendSdk
    {
        string ApiKey { get; set; }
        IResend Resend { get; set; }
        public ResendSdk(string apiKey)
        {
            ApiKey = apiKey;
            Resend = ResendClient.Create(apiKey);

        }
        public async Task<StateSet<bool>> RawSend(MailAddress from, MailAddress to, string subject, string htmlBody) => await RawSend(from.ToString(), to.ToString(), subject, htmlBody);
        public async Task<StateSet<bool>> RawSend(string from, string to, string subject, string htmlBody)
        {
            var resp = await Resend.EmailSendAsync(new EmailMessage()
            {
                From = from,
                To = to,
                Subject = subject,
                HtmlBody = htmlBody,
            });
            return resp.Success.ToStateSet(resp.Exception?.Message);
        }
        public async Task<StateSet<bool>> SendVerifyCodeMail(VerifyCodeMailParameters parameters)
        {
            var htmlBody = parameters.GetHtmlBody();
            return await RawSend(parameters.From, parameters.To, parameters.Subject, htmlBody);
        }
    }
}
