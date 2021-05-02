using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CelotMClient.CustomView
{
    public partial class SmsView : UserControl
    {
        public SmsView()
        {
            InitializeComponent();
            smsPanel.Controls.Clear();
            CustomView.Sms alert = new CustomView.Sms();
            alert.Dock = DockStyle.Fill;
            smsPanel.Controls.Add(alert);
        }

        private void appBtn_Click(object sender, EventArgs e)
        {
            smsPanel.Controls.Clear();
            CustomView.Sms alert = new CustomView.Sms();
            alert.Dock = DockStyle.Fill;
            smsPanel.Controls.Add(alert);
        }

        private void smsListBtn_Click(object sender, EventArgs e)
        {
            smsPanel.Controls.Clear();
        }
    }
}
