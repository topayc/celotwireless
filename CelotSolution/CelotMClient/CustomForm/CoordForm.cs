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
    public partial class CoordForm : Form
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public CoordForm()
        {
            InitializeComponent();
        }


        private void okBtn_Click(object sender, EventArgs e)
        {
            
            if (String.IsNullOrEmpty(this.latTextBox.Text)){
                MessageBox.Show("위도 정보를 입력해주세요");
                this.latTextBox.Focus();
                return;
            }

            if (String.IsNullOrEmpty(this.lngTextBox.Text)){
                MessageBox.Show("경도 정보를 입력해주세요");
                this.latTextBox.Focus();
                return;
            }

            float latitude;
            bool isFloat = float.TryParse(this.latTextBox.Text, out latitude);
            if (!isFloat)
            {
                MessageBox.Show("위도 정보는 실수만 가능합니다");
                this.latTextBox.Focus();
                return;
            }

            float longitude;
            bool isFloat2 = float.TryParse(this.lngTextBox.Text, out latitude);
            if (!isFloat2)
            {
                MessageBox.Show("경도 정보는 실수만 가능합니다");
                this.lngTextBox.Focus();
                return;
            }
            this.Latitude = Convert.ToDouble(this.latTextBox.Text);
            this.Longitude =Convert.ToDouble(this.lngTextBox.Text);
            ApplicationConfig.Instance().Latitude = Convert.ToDouble(this.latTextBox.Text);
            ApplicationConfig.Instance().Longitude = Convert.ToDouble(this.lngTextBox.Text);
            this.DialogResult = DialogResult.OK;
        }

        private void glassButton1_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void CoordForm_Load(object sender, EventArgs e)
        {
            this.latTextBox.Text = ApplicationConfig.Instance().Latitude.ToString();
            this.lngTextBox.Text = ApplicationConfig.Instance().Longitude.ToString();
        }
    }
}
