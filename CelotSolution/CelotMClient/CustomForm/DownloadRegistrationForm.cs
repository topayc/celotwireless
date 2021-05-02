using CelotMClient.Model;
using CelotMClient.Util;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CelotMClient.CustomForm
{
    public partial class DownloadRegistrationForm : Form
    {
        private bool isMouseDown = false;
        private int xLast;
        private int yLast;


        public static int MODE_ADD = 1;
        public static int MODE_MODIFY = 2;
        public DownloadRegistrationForm(int mode, Download download)
        {
            InitializeComponent();
            this.Mode = mode;
            if (this.Mode == MODE_MODIFY)
            {
                this.tileLabel.Text = "다운로드 수정";
                this.ipBox.Text = download.Ip;
                this.ipBox.ReadOnly = true;
                this.ipBox.Enabled = false;
                this.ipBox.ForeColor = Color.FromArgb(255, 255, 255, 255);
                this.ipBox.BackColor = Color.FromArgb(255, 50, 50, 50);
               
                
            }
            else
            {
                this.tileLabel.Text = "다운로드 추가";
            }
        }

        public string Ip { get; set; }
        public string FileName { get; set; }
        public int Mode { get; set; }
        public Download Download { get; set; }

    

        private void panel2_MouseDown(object sender, MouseEventArgs e)
        {
            isMouseDown = true;
            yLast = e.Y;
            xLast = e.X;
        }

        private void panel2_MouseMove(object sender, MouseEventArgs e)
        {
            if (isMouseDown)
            {
                int newY = this.Top + (e.Y - yLast);
                int newX = this.Left + (e.X - xLast);
                this.Location = new Point(newX, newY);
            }
        }

        private void panel2_MouseUp(object sender, MouseEventArgs e)
        {
            isMouseDown = false;
            xLast = e.X;
            yLast = e.Y;
        }

        private void panel3_MouseClick(object sender, MouseEventArgs e)
        {
            Close();
        }

        private void okBtn_Click(object sender, EventArgs e)
        {
            if ("".Equals(ipBox.Text))
            {
                MessageBox.Show("IP를 입력해주세요");
                return;
            }

            if (!CelotUtility.CheckValidIp(ipBox.Text.Trim()))
            {
                MessageBox.Show("잘못된 IP 형식입니다");
                return;
            }

           

          
            this.Ip = ipBox.Text;
            this.DialogResult = DialogResult.OK;
        }

        private void cancelBtn_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void fielDlgBtn_Click(object sender, EventArgs e)
        {
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                this.FileName = openFileDialog.FileName;
            }
        }
    }
}
