using CelotMClient.Manager;
using CelotMClient.Model;
using CelotMClient.Model.NMS;
using CelotMClient.Worker;
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
    public partial class AdminRegistration : Form
    {
        string title;
        Admin admin;
        private int mode;
        List<AdminGroup> adminGroupList;

        public Admin Admin {
            get { return this.admin; }
            set { this.admin = value; }
        }
        public AdminRegistration()
        {
            InitializeComponent();
        }

        public AdminRegistration(string title,int mode, Admin admin)
        {
            // TODO: Complete member initialization
            InitializeComponent();
            this.titleLabel.Text = title;
            this.Text = this.title;
            this.mode = mode;
            this.admin= admin;

            AdminGroupDao adminGroupDao = new AdminGroupDao();
            adminGroupDao.notifyDBfinished += new NotifyDBfinishedHandler(this.AdminGroup_Receive);
            adminGroupDao.getAdminGroups();
        }

        private void AdminRegistration_Load(object sender, EventArgs e)
        {
           
        }

        private void AdminGroup_Receive(object Sender, DBFinishedEventArgs e)
        {
            this.adminGroupList = (List<AdminGroup>)e.Result;
            this.initControll();
        }

        private void initControll()
        {
            this.groupComboBox.DataSource = this.adminGroupList;
          
            if (this.mode == Constants.MODIFY)
            {
                this.titleLabel.Text= "관리자 수정";
                this.groupComboBox.SelectedIndex = 0;
                this.nameBox.Text = this.admin.Name;
                this.idBox.Text = this.admin.Id;
                this.passwordBox.Text = this.admin.Password;
                this.groupComboBox.SelectedValue = this.admin.AdminGroupNo;
            }
            else
            {
                this.titleLabel.Text = "관리자 생성";
                this.groupComboBox.SelectedIndex = 0;
            }
        }

        private void okBtn_Click(object sender, EventArgs e)
        {
            string message = "";
            bool check = true;
            
            if (this.nameBox.Text.Length == 0)
            {
                message = "이름을 입력해주세요";
                check = false;
            }
            if (this.idBox.Text.Length == 0)
            {
                message = "ID를 입력해주세요";
                check = false;
            }
            if (this.passwordBox.Text.Length == 0)
            {
                message = "비밀번호를 입력해주세요";
                check = false;
            }
            if (!check)
            {
                MessageBox.Show(message);
                return;
            }
            Admin admin;
            if (this.mode == Constants.CREATE)
            {
                admin = new Admin();
            }
            else
            {
                admin = this.Admin;
            }
           
            admin.AdminGroupNo = Convert.ToInt32(this.groupComboBox.SelectedValue);
            admin.Id = this.idBox.Text;
            admin.Name = this.nameBox.Text;
            admin.Password = this.passwordBox.Text;
            this.Admin = admin;
          
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void cancelBtn_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void panel2_MouseUp(object sender, MouseEventArgs e)
        {
            isMouseDown = false;
            xLast = e.X;
            yLast = e.Y;
        }

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

        private bool isMouseDown = false;
        private int xLast;
        private int yLast;
        private string p;

        private void panel3_MouseClick(object sender, MouseEventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }
    }
}
