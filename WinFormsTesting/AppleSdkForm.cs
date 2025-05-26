using Newtonsoft.Json.Linq;
using Silmoon.Extension;
using Silmoon.SdkApi.Apple;
using Silmoon.SdkApi.Apple.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormsTesting
{
    public partial class AppleSdkForm : Form
    {
        JObject Config { get; set; }
        public AppleSdkForm()
        {
            InitializeComponent();

            if (File.Exists("config.local.json"))
                Config = JsonHelperV2.LoadJsonFromFile("config.local.json");
            else
                Config = JsonHelperV2.LoadJsonFromFile("config.json");
        }

        private async void ctlSubmitButton_Click(object sender, EventArgs e)
        {
            ApplePush push = new ApplePush()
            {
                AppId = ctlAppIdTextBox.Text,
                DeviceToken = ctlDeviceTokenTextBox.Text,
                Title = "this title",
                SubTitle = "this subtitle",
                Body = "this is content",
            };
            var result = await ApplePushNotification.PushRemoteNotification(push, ctlDevelopmentCheckBox.Checked, ctlPushKeyIdTextBox.Text, ctlPushKeyTextBox.Text, ctlTeamIdTextBox.Text);
            if (result.State)
            {
                MessageBox.Show("Success: " + result.Data.ToString() + "\n" + result.Message);
            }
            else
            {
                MessageBox.Show("Error: " + result.Data.ToString() + "\n" + result.Message);
            }
        }

        private void AppleSdkForm_Load(object sender, EventArgs e)
        {
            ctlAppIdTextBox.Text = Config["apple"]?["apn"]?["appId"].Value<string>();
            ctlPushKeyIdTextBox.Text = Config["apple"]?["apn"]?["pushKeyId"].Value<string>();
            ctlPushKeyTextBox.Text = Config["apple"]?["apn"]?["pushKey"].Value<string>();
            ctlTeamIdTextBox.Text = Config["apple"]?["apn"]?["teamId"].Value<string>();
            ctlDeviceTokenTextBox.Text = Config["apple"]?["apn"]?["deviceToken"].Value<string>();
            ctlDevelopmentCheckBox.Checked = Config["apple"]?["apn"]?["isDevelopment"].Value<bool>() ?? false;
        }
    }
}
