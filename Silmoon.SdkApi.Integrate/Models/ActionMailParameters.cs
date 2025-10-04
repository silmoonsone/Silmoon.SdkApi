using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Mail;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Silmoon.SdkApi.Integrate.Models
{
    public class ActionMailParameters
    {
        private static readonly Lazy<string> _templateCache = new Lazy<string>(() =>
        {
            var assembly = Assembly.GetExecutingAssembly();
            var resourceName = "Silmoon.SdkApi.Integrate.HtmlTemplates.ActionNotificationTemplate.html";

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
        public DateTime DateTime { get; set; }
        public string Email { get; set; }

        public string GetHtmlBody()
        {
            try
            {
                var template = _templateCache.Value;

                // 格式化日期时间
                var formattedDateTime = DateTime.ToString("yyyy年MM月dd日 HH:mm:ss", CultureInfo.GetCultureInfo("zh-CN"));

                // 替换模板变量
                return template
                    .Replace("{{AppName}}", AppName ?? "")
                    .Replace("{{Username}}", Username ?? "")
                    .Replace("{{Action}}", Action ?? "")
                    .Replace("{{DateTime}}", formattedDateTime)
                    .Replace("{{Email}}", Email ?? "");
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"生成操作通知邮件HTML时发生错误: {ex.Message}", ex);
            }
        }
        public static ActionMailParameters Create(MailAddress from, MailAddress to, string subject, string appName, string username, string action, DateTime dateTime, string email)
        {
            return new ActionMailParameters
            {
                From = from,
                To = to,
                Subject = subject,
                AppName = appName,
                Username = username,
                Action = action,
                DateTime = dateTime,
                Email = email,
            };
        }
    }
}
