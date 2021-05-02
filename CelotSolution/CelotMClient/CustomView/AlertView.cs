using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CelotMClient.Worker;

namespace CelotMClient.CustomView
{
    public partial class AlertView : UserControl
    {
        public AlertView()
        {
            InitializeComponent();
           
        }

        private void AlertView_Load(object sender, EventArgs e)
        {
            alertPanel.Controls.Clear();
            this.setAlert();
         
        }

        private void alertLogBtn_Click(object sender, EventArgs e)
        {
            alertPanel.Controls.Clear();
        }

        private void aletsBtn_Click(object sender, EventArgs e)
        {
            this.setAlert();
        }

        public void setAlert()
        {
            alertPanel.Controls.Clear();
            CustomView.Alerts alert = new CustomView.Alerts();
             alert .BackColor = Color.FromArgb(255,50,50,50);
            alert.Dock = DockStyle.Fill;
            alertPanel.Controls.Add(alert);
        }

    }
}
