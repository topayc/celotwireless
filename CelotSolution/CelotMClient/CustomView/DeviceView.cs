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
    public partial class DeviceView : UserControl
    {
        public DeviceView()
        {
            InitializeComponent();
        }

        private void DeviceView_Load(object sender, EventArgs e)
        {
            this.setDeviceOverView();
        }

        private void deviceBtn_Click(object sender, EventArgs e)
        {
            devicePanel.Controls.Clear();
        }

        private void onverViewBtn_Click(object sender, EventArgs e)
        {
            this.setDeviceOverView();
        }

        public void setDeviceOverView()
        {
            devicePanel.Controls.Clear();
            CustomView.Device device = new CustomView.Device();
            device.BackColor = Color.FromArgb(255, 50, 50, 50);
            device.Dock = DockStyle.Fill;
            devicePanel.Controls.Add(device);
        }
    }
}
