using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CelotMClient.CustomForm;

namespace CelotMClient.Setup
{
    public partial class Setup2 : UserControl
    {
        private SetupForm setupForm;

        public Setup2(SetupForm setupForm)
        {
            this.setupForm = setupForm;
            InitializeComponent();
        }

        private void Setup2_Load(object sender, EventArgs e)
        {
            progressTextBox.ReadOnly = true;
        }

        private void progressTextBox_TextChanged(object sender, EventArgs e)
        {
            progressTextBox.SelectionStart = progressTextBox.Text.Length;
            progressTextBox.ScrollToCaret();
        }
    }
}
