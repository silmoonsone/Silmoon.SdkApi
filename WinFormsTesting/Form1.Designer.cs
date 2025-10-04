namespace WinFormsTesting
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            ctlAppleSdkTestButton = new Button();
            ctlSendGridTestButton = new Button();
            ctlResendTestButton = new Button();
            SuspendLayout();
            // 
            // ctlAppleSdkTestButton
            // 
            ctlAppleSdkTestButton.Location = new Point(12, 12);
            ctlAppleSdkTestButton.Name = "ctlAppleSdkTestButton";
            ctlAppleSdkTestButton.Size = new Size(75, 23);
            ctlAppleSdkTestButton.TabIndex = 0;
            ctlAppleSdkTestButton.Text = "Apple SDK";
            ctlAppleSdkTestButton.UseVisualStyleBackColor = true;
            ctlAppleSdkTestButton.Click += ctlAppleSdkTestButton_Click;
            // 
            // ctlSendGridTestButton
            // 
            ctlSendGridTestButton.Location = new Point(93, 12);
            ctlSendGridTestButton.Name = "ctlSendGridTestButton";
            ctlSendGridTestButton.Size = new Size(75, 23);
            ctlSendGridTestButton.TabIndex = 1;
            ctlSendGridTestButton.Text = "SendGrid";
            ctlSendGridTestButton.UseVisualStyleBackColor = true;
            ctlSendGridTestButton.Click += ctlSendGridTestButton_Click;
            // 
            // ctlResendTestButton
            // 
            ctlResendTestButton.Location = new Point(174, 12);
            ctlResendTestButton.Name = "ctlResendTestButton";
            ctlResendTestButton.Size = new Size(75, 23);
            ctlResendTestButton.TabIndex = 2;
            ctlResendTestButton.Text = "Resend";
            ctlResendTestButton.UseVisualStyleBackColor = true;
            ctlResendTestButton.Click += ctlResendTestButton_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(ctlResendTestButton);
            Controls.Add(ctlSendGridTestButton);
            Controls.Add(ctlAppleSdkTestButton);
            Name = "Form1";
            Text = "Form1";
            ResumeLayout(false);
        }

        #endregion

        private Button ctlAppleSdkTestButton;
        private Button ctlSendGridTestButton;
        private Button ctlResendTestButton;
    }
}
