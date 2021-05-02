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
    public partial class ApplicationView : UserControl
    {
        public ApplicationView()
        {
            InitializeComponent();
        }

        private void ApplicationView_Load(object sender, EventArgs e)
        {
            appPanel.Controls.Clear();
            CustomView.Application app = new CustomView.Application();
            app.Dock = DockStyle.Fill;
            appPanel.Controls.Add(app);
        }

        private void appBtn_Click(object sender, EventArgs e)
        {
            appPanel.Controls.Clear();
            CustomView.Application app = new CustomView.Application();
            app.Dock = DockStyle.Fill;
            appPanel.Controls.Add(app);
        }

        private void downloadBtn_Click(object sender, EventArgs e)
        {
            appPanel.Controls.Clear();
        }

      

    }
}
