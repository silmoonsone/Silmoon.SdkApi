using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Silmoon.SdkApi.Integrate.Models
{
    public class VerifyCodeMailParameters
    {
        private static readonly Lazy<string> _templateCache = new Lazy<string>(() =>
        {
            var assembly = Assembly.GetExecutingAssembly();
            var resourceName = "Silmoon.SdkApi.Integrate.HtmlTemplates.VerifyCodeTemplate.html";

            using var stream = assembly.GetManifestResourceStream(resourceName);
            if (stream == null)
                throw new InvalidOperationException($"无法找到嵌入资源: {resourceName}");

            using var reader = new StreamReader(stream);
            return reader.ReadToEnd();
        });

        public MailAddress From { get; set; }
        public MailAddress To { get; set; }
        public string Subject { get; set; }

        public string AppName { get; set; }
        public string Username { get; set; }
        public string Action { get; set; }
        public string VerifyCode { get; set; }
        public int ExpireMinutes { get; set; }
        public string Email { get; set; }

        public string GetHtmlBody()
        {
            try
            {
                var template = _templateCache.Value;

                // 替换模板变量
                return template
                    .Replace("{{AppName}}", AppName ?? "")
                    .Replace("{{Username}}", Username ?? "")
                    .Replace("{{Action}}", Action ?? "")
                    .Replace("{{VerifyCode}}", VerifyCode ?? "")
                    .Replace("{{ExpireMinutes}}", ExpireMinutes.ToString())
                    .Replace("{{Email}}", Email ?? "");
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"生成验证码邮件HTML时发生错误: {ex.Message}", ex);
            }
        }
        public static VerifyCodeMailParameters Create(MailAddress from, MailAddress to, string subject, string appName, string username, string action, string verifyCode, int expireMinutes, string email)
        {
            return new VerifyCodeMailParameters
            {
                From = from,
                To = to,
                Subject = subject,
                AppName = appName,
                Username = username,
                Action = action,
                VerifyCode = verifyCode,
                ExpireMinutes = expireMinutes,
                Email = email,
            };
        }
    }
}
