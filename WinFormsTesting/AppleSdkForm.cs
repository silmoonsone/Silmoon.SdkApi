using Newtonsoft.Json.Linq;
using Silmoon.Extensions;
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
        public AppleSdkForm()
        {
            InitializeComponent();
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
            ctlAppIdTextBox.Text = Form1.Config["apple"]?["apn"]?["appId"].Value<string>();
            ctlPushKeyIdTextBox.Text = Form1.Config["apple"]?["apn"]?["pushKeyId"].Value<string>();
            ctlPushKeyTextBox.Text = Form1.Config["apple"]?["apn"]?["pushKey"].Value<string>();
            ctlTeamIdTextBox.Text = Form1.Config["apple"]?["apn"]?["teamId"].Value<string>();
            ctlDeviceTokenTextBox.Text = Form1.Config["apple"]?["apn"]?["deviceToken"].Value<string>();
            ctlDevelopmentCheckBox.Checked = Form1.Config["apple"]?["apn"]?["isDevelopment"].Value<bool>() ?? false;
        }
    }
}
