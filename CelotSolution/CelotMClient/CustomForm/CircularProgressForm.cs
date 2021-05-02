using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CelotMClient.CustomForm
{
    public partial class CircularProgressForm : Form
    {
        delegate void SplashShowCloseDelegate();
        bool CloseProgressFlag = false;
        public CircularProgressForm()
        {
            InitializeComponent();
        }

        public void ClosePrgress()
        {
            if (InvokeRequired)
            {
                // We're not in the UI thread, so we need to call BeginInvoke
                BeginInvoke(new SplashShowCloseDelegate(ClosePrgress));
                return;
            }

            CloseProgressFlag = true;
            this.Close();
        }

        public void ShowProgress()
        {
            if (InvokeRequired)
            {
                // We're not in the UI thread, so we need to call BeginInvoke
                BeginInvoke(new SplashShowCloseDelegate(ShowProgress));
                return;
            }
            this.Show();
            Application.Run(this);
        }

        private void CircularProgressForm_Load(object sender, EventArgs e)
        {
            this.Size = new Size(70, 70);
            this.optimizedCircularProgressControl1.Start();
        }
    }
}
