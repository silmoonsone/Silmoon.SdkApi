namespace WinFormsTesting
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
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

    }
}
