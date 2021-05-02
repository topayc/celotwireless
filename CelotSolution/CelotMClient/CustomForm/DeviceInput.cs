using CelotMClient.Manager;
using CelotMClient.Model;
using CelotMClient.Util;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CelotMClient.CustomForm
{
    public partial class DeviceInput : Form
    {
        private string p;
        private CelotMClient.Model.Device device;
        private int mode;

        public Device Device
        {
            get { return this.device; }
            set { this.device = value; }
        }

        public int Mode
        {
            get { return this.mode; }
            set { this.mode = value; }
        }

        public DeviceInput(string title, int mode, CelotMClient.Model.Device device)
        {
            // TODO: Complete member initialization
            InitializeComponent();
            this.Text = title;
            this.mode = mode;
            this.device = device;
        }

        private void DeviceInputForm_Load(object sender, EventArgs e)
        {
            this.initControll();
        }

        private void initControll()
        {
            this.smsSupportCombo.SelectedIndex = 0;
            this.batterySupportCombo.SelectedIndex = 0;
            this.wifiSupportCombo.SelectedIndex = 0;
            this.vpnSupportCombo.SelectedIndex = 0;

            if (this.device != null)
            {
                this.nameBox.Text = this.device.Name;
                this.serialBox.Text = this.device.SerialNo.ToString();
                this.secuCodeBox.Text = this.device.SecuCode.ToString();
                this.groupNameBox.Text = this.device.GroupName;
                this.routerIpBox.Text = this.device.RouterIp;

                this.latitudeBox.Text = String.Format("{0:F7}", this.device.Latitude);
                this.longitudeBox.Text = String.Format("{0:F7}", this.device.Longitude);
                this.desBox.Text = this.device.Des;
                this.phoneBox_1.Text = this.device.PhoneNumber.ToString();
                
                this.smsSupportCombo.SelectedIndex = this.device.SmsSupport; 
                this.batterySupportCombo.SelectedIndex = this.device.BatterySupport;
                this.wifiSupportCombo.SelectedIndex = this.device.WifiSupport;
                this.vpnSupportCombo.SelectedIndex = this.device.VpnSupport;
               
            }
        }

        private void glassButton2_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void glassButton1_Click(object sender, EventArgs e)
        {
            if (this.CheckField())
            {
                this.DialogResult = DialogResult.OK;
            }
        }

        private bool CheckField()
        {
            if (String.IsNullOrEmpty(this.nameBox.Text))
            {
                MessageBox.Show("라우터 이름을 입력해주세요");
                this.nameBox.Focus();
                return false;
            }
            String name = this.nameBox.Text;

            if (String.IsNullOrEmpty(this.groupNameBox.Text))
            {
                MessageBox.Show("라우터 그룹을 입력해주세요");
                this.nameBox.Focus();
                return false;
            }
            String groupName = this.groupNameBox.Text;


            if (String.IsNullOrEmpty(this.serialBox.Text))
            {
                MessageBox.Show("시리얼 번호를 입력해주세요");
                this.serialBox.Focus();
                return false;
            }

            if (String.IsNullOrEmpty(this.secuCodeBox.Text) || this.secuCodeBox.Text.Length !=8 )
            {
                MessageBox.Show("8자리의 보안코드를 입력해주세요");
                this.secuCodeBox.Focus();
                return false;
            }
            String secuCode = this.secuCodeBox.Text;

            int serialNo;
            bool isNum = int.TryParse(this.serialBox.Text, out serialNo);
            if (!isNum )
            {
                ShowMessage("시리얼 번호는 숫자만 가능합니다");
                this.serialBox.Focus();
                return false;
            }

            string routerIp = this.routerIpBox.Text;
            if (String.IsNullOrEmpty(routerIp))
            {
                ShowMessage("라우터 IP 를 입력해주세요");
                this.routerIpBox.Focus();
                return false;
            }

            if (!CelotUtility.CheckValidIp(routerIp))
            {
                ShowMessage("아이피 포맷이 잘못되었습니다. ");
                this.routerIpBox.Focus();
                return false;
            }

            if (String.IsNullOrEmpty(this.latitudeBox.Text))
            {
                ShowMessage("위도 정보를 입력해주세요");
                this.latitudeBox.Focus();
                return false;
            }

            double latitude;
            bool isFloat = double.TryParse(this.latitudeBox.Text, out latitude);
            if (!isFloat)
            {
                ShowMessage("위도 정보는 실수만 가능합니다");
                this.latitudeBox.Focus();
                return false;
            }

            if (String.IsNullOrEmpty(this.longitudeBox.Text))
            {
                MessageBox.Show("경도 정보를 입력해주세요");
                this.longitudeBox.Focus();
                return false;
            }

            double longitude;
            bool isFloat1 = double.TryParse(this.longitudeBox.Text, out longitude);
            if (!isFloat1)
            {
                ShowMessage("경도 정보는 실수만 가능합니다");
                this.longitudeBox.Focus();
                return false;
            }


            if (String.IsNullOrEmpty(this.desBox.Text))
            {
                ShowMessage("라우터 설명을 입력해주세요");
                this.desBox.Focus();
                return false;
            }
            string des = this.desBox.Text;
            

            if (String.IsNullOrEmpty(this.phoneBox_1.Text))
            {
                MessageBox.Show("라우터 전화번호를 입력해주세요");
                this.phoneBox_1.Focus();
                return false;
            }

            int phoneNumber;
            bool isNum1 = int.TryParse(this.phoneBox_1.Text, out phoneNumber);
            if (!isNum1)
            {
                ShowMessage("전화번호는 숫자만 가능합니다");
                this.phoneBox_1.Focus();
                return false;
            }

            if (this.mode == Constants.CREATE)
            {
                this.device = new Device();
            }

            this.device.Name = name;
            this.device.SecuCode = secuCode;
            this.device.Des = des;
            this.device.Latitude = this.latitudeBox.Text;
            this.device.Longitude = this.longitudeBox.Text;
            this.device.PhoneNumber = phoneNumber;
            this.device.GroupName = this.groupNameBox.Text;
            this.device.RouterIp = routerIp;
            this.device.SerialNo = serialNo;
            this.device.SmsSupport = this.smsSupportCombo.SelectedIndex;
            this.device.VpnSupport = this.vpnSupportCombo.SelectedIndex;
            this.device.WifiSupport = this.wifiSupportCombo.SelectedIndex;
            this.device.BatterySupport = this.batterySupportCombo.SelectedIndex;
            
            return true;
        }

        private void ShowMessage(string mes)
        {
            MessageBox.Show(mes);
        }



    }
}
