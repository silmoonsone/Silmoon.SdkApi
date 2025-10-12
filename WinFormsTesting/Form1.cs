using Newtonsoft.Json.Linq;
using Silmoon.Extension;
using Silmoon.SdkApi.Integration.Mail;
using Silmoon.SdkApi.Integration.Models;
using System.Diagnostics;
using System.Net.Mail;
using System.Threading.Tasks;

namespace WinFormsTesting
{
    public partial class Form1 : Form
    {
        public static JObject Config { get; set; }

        public Form1()
        {
            InitializeComponent();
            if (File.Exists("config.local.json"))
                Config = JsonHelperV2.LoadJsonFromFile("config.local.json");
            else
                Config = JsonHelperV2.LoadJsonFromFile("config.json");
        }

        private void ctlAppleSdkTestButton_Click(object sender, EventArgs e)
        {
            var form = new AppleSdkForm();
            form.FormClosed += (object? sender, FormClosedEventArgs e) =>
            {
                Application.Exit();
            };
            form.Show();
            Hide();
        }

        private void ctlSendGridTestButton_Click(object sender, EventArgs e)
        {

        }

        private async void ctlResendTestButton_Click(object sender, EventArgs e)
        {
            var apiKey = Config["resend"]?["apiKey"]?.Value<string>();
            var resend = new ResendSdk(apiKey);
            //var result = await resend.RawSend(new MailAddress("sone@resend.silmoon.com", "SILMOON"), new MailAddress("sone@silmoon.com"), "hello from resend", "<h1>this is test</h1>");
            var result = await resend.SendVerifyCodeMail(VerifyCodeMailParameters.Create(new MailAddress("sone@resend.silmoon.com", "SILMOON验证码通知"), new MailAddress("sone@silmoon.com"), "【SILMOON】验证码通知", "SILMOON", "sone", "测试", "123456", 5, "sone@silmoon.com"));

            Debug.WriteLine(result.ToJsonString());
        }
    }
}
