﻿using System;
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
    public partial class AlertDialog : Form
    {
        public AlertDialog()
        {
            InitializeComponent();
        }

        private void AlertDialog_Load(object sender, EventArgs e)
        {
            this.StartPosition = FormStartPosition.CenterParent;

        }
    }
}
