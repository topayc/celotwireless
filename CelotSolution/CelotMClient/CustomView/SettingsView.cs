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
    public partial class SettingsView : UserControl
    {
        public SettingsView()
        {
            InitializeComponent();
            settinsAdminPanel.Controls.Clear();
            CustomView.SettingsDetailUserControll settingsDetailUserControll = new CustomView.SettingsDetailUserControll();
            settingsDetailUserControll.Dock = DockStyle.Fill;
            settinsAdminPanel.Controls.Add(settingsDetailUserControll);
            
        }

        private void adminBtn_Click(object sender, EventArgs e)
        {
            settinsAdminPanel.Controls.Clear();
            CustomView.AdminSetting adminView = new CustomView.AdminSetting();
            adminView.BackColor = Color.FromArgb(255, 50, 50, 50);
            adminView.Dock = DockStyle.Fill;
            settinsAdminPanel.Controls.Add(adminView);
        }

        private void settingsBtn_Click(object sender, EventArgs e)
        {
            settinsAdminPanel.Controls.Clear();
            CustomView.SettingsDetailUserControll settingsDetailUserControll = new CustomView.SettingsDetailUserControll();
            settingsDetailUserControll.Dock = DockStyle.Fill;
            settinsAdminPanel.Controls.Add(settingsDetailUserControll);

        }

        private void resetTimeBtn_Click(object sender, EventArgs e)
        {
            
        }

        private void Settings_Load(object sender, EventArgs e)
        {

        }

    }
}
