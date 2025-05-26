namespace WinFormsTesting
{
    partial class AppleSdkForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            groupBox1 = new GroupBox();
            ctlDeviceTokenTextBox = new TextBox();
            label4 = new Label();
            ctlAppIdTextBox = new TextBox();
            lable0 = new Label();
            ctlSubmitButton = new Button();
            ctlDevelopmentCheckBox = new CheckBox();
            ctlTeamIdTextBox = new TextBox();
            ctlPushKeyTextBox = new TextBox();
            ctlPushKeyIdTextBox = new TextBox();
            label3 = new Label();
            label2 = new Label();
            label1 = new Label();
            groupBox1.SuspendLayout();
            SuspendLayout();
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(ctlDeviceTokenTextBox);
            groupBox1.Controls.Add(label4);
            groupBox1.Controls.Add(ctlAppIdTextBox);
            groupBox1.Controls.Add(lable0);
            groupBox1.Controls.Add(ctlSubmitButton);
            groupBox1.Controls.Add(ctlDevelopmentCheckBox);
            groupBox1.Controls.Add(ctlTeamIdTextBox);
            groupBox1.Controls.Add(ctlPushKeyTextBox);
            groupBox1.Controls.Add(ctlPushKeyIdTextBox);
            groupBox1.Controls.Add(label3);
            groupBox1.Controls.Add(label2);
            groupBox1.Controls.Add(label1);
            groupBox1.Location = new Point(12, 12);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(329, 507);
            groupBox1.TabIndex = 0;
            groupBox1.TabStop = false;
            groupBox1.Text = "APN";
            // 
            // ctlDeviceTokenTextBox
            // 
            ctlDeviceTokenTextBox.Location = new Point(18, 393);
            ctlDeviceTokenTextBox.Name = "ctlDeviceTokenTextBox";
            ctlDeviceTokenTextBox.Size = new Size(292, 23);
            ctlDeviceTokenTextBox.TabIndex = 11;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(18, 373);
            label4.Name = "label4";
            label4.Size = new Size(82, 17);
            label4.TabIndex = 10;
            label4.Text = "DeviceToken";
            // 
            // ctlAppIdTextBox
            // 
            ctlAppIdTextBox.Location = new Point(18, 50);
            ctlAppIdTextBox.Name = "ctlAppIdTextBox";
            ctlAppIdTextBox.Size = new Size(292, 23);
            ctlAppIdTextBox.TabIndex = 4;
            // 
            // lable0
            // 
            lable0.AutoSize = true;
            lable0.Location = new Point(18, 30);
            lable0.Name = "lable0";
            lable0.Size = new Size(44, 17);
            lable0.TabIndex = 0;
            lable0.Text = "AppId";
            // 
            // ctlSubmitButton
            // 
            ctlSubmitButton.Location = new Point(235, 469);
            ctlSubmitButton.Name = "ctlSubmitButton";
            ctlSubmitButton.Size = new Size(75, 23);
            ctlSubmitButton.TabIndex = 9;
            ctlSubmitButton.Text = "Submit";
            ctlSubmitButton.UseVisualStyleBackColor = true;
            ctlSubmitButton.Click += ctlSubmitButton_Click;
            // 
            // ctlDevelopmentCheckBox
            // 
            ctlDevelopmentCheckBox.AutoSize = true;
            ctlDevelopmentCheckBox.Location = new Point(18, 471);
            ctlDevelopmentCheckBox.Name = "ctlDevelopmentCheckBox";
            ctlDevelopmentCheckBox.Size = new Size(104, 21);
            ctlDevelopmentCheckBox.TabIndex = 8;
            ctlDevelopmentCheckBox.Text = "Development";
            ctlDevelopmentCheckBox.UseVisualStyleBackColor = true;
            // 
            // ctlTeamIdTextBox
            // 
            ctlTeamIdTextBox.Location = new Point(18, 347);
            ctlTeamIdTextBox.Name = "ctlTeamIdTextBox";
            ctlTeamIdTextBox.Size = new Size(292, 23);
            ctlTeamIdTextBox.TabIndex = 7;
            // 
            // ctlPushKeyTextBox
            // 
            ctlPushKeyTextBox.Location = new Point(18, 142);
            ctlPushKeyTextBox.Multiline = true;
            ctlPushKeyTextBox.Name = "ctlPushKeyTextBox";
            ctlPushKeyTextBox.Size = new Size(292, 182);
            ctlPushKeyTextBox.TabIndex = 6;
            // 
            // ctlPushKeyIdTextBox
            // 
            ctlPushKeyIdTextBox.Location = new Point(18, 96);
            ctlPushKeyIdTextBox.Name = "ctlPushKeyIdTextBox";
            ctlPushKeyIdTextBox.Size = new Size(292, 23);
            ctlPushKeyIdTextBox.TabIndex = 5;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(18, 327);
            label3.Name = "label3";
            label3.Size = new Size(52, 17);
            label3.TabIndex = 3;
            label3.Text = "TeamId";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(18, 122);
            label2.Name = "label2";
            label2.Size = new Size(56, 17);
            label2.TabIndex = 2;
            label2.Text = "PushKey";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(18, 76);
            label1.Name = "label1";
            label1.Size = new Size(68, 17);
            label1.TabIndex = 1;
            label1.Text = "PushKeyId";
            // 
            // AppleSdkForm
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(356, 534);
            Controls.Add(groupBox1);
            Name = "AppleSdkForm";
            Text = "AppleSdkForm";
            Load += AppleSdkForm_Load;
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private GroupBox groupBox1;
        private TextBox ctlTeamIdTextBox;
        private TextBox ctlPushKeyTextBox;
        private TextBox ctlPushKeyIdTextBox;
        private Label label3;
        private Label label2;
        private Label label1;
        private Button ctlSubmitButton;
        private CheckBox ctlDevelopmentCheckBox;
        private TextBox ctlAppIdTextBox;
        private Label lable0;
        private TextBox ctlDeviceTokenTextBox;
        private Label label4;
    }
}