using CelotMClient.Manager;
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
    public partial class MenuEditForm : Form
    {
        public MenuEditForm()
        {
            InitializeComponent();
        }

        private void cancelBtn_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void okBtn_Click(object sender, EventArgs e)
        {
            bool[] menuSettings = new bool[5];
            menuSettings[MenuIndex.Dashboard] = this.dashCheck.Checked;
            menuSettings[MenuIndex.Device] = this.deviceCheck.Checked;
            menuSettings[MenuIndex.Alert] = this.alertCheck.Checked;
            menuSettings[MenuIndex.Application] = this.appCheck.Checked;
            menuSettings[MenuIndex.Sms]  = this.smsCheck.Checked;

            CelotApplication.Instance().MenuSettings = menuSettings;
            ApplicationConfig.Instance().MenuSettings = menuSettings;
            CelotApplication.Instance().MainForm.changeMenu();
            this.DialogResult = DialogResult.OK;
        }

        private void MenuEditForm_Load(object sender, EventArgs e)
        {
            bool[] menuSettings = ApplicationConfig.Instance().MenuSettings;
            this.dashCheck.Checked = menuSettings[MenuIndex.Dashboard];
            this.deviceCheck.Checked = menuSettings[MenuIndex.Device];
            this.alertCheck.Checked = menuSettings[MenuIndex.Alert];
            this.appCheck.Checked = menuSettings[MenuIndex.Application];
            this.smsCheck.Checked = menuSettings[MenuIndex.Sms];
        }
    }
}
