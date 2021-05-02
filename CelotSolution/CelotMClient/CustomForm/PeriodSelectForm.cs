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
    public partial class PeriodSelectForm : Form
    {

        public int DateType { get; set; }
        public int Period { get; set; }
        public bool SelectPeriodChanged
        {
            get;
            set;
        }
        public PeriodSelectForm()
        {
            InitializeComponent();
        }

        private void panel2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void okBtn_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(this.periodTextBox.Text))
            {
                MessageBox.Show("조회하려는 기간을 입력해주세요");
                return;
            }
            int period = 0;
            bool result = int.TryParse(this.periodTextBox.Text, out period);
            if (!result)
            {
                MessageBox.Show("조회기간은 0이 아니거나 문자가 아니어야 합니다");
                return ;
            }

            if (this.DateType == Constants.DATETYPE_MONTH && period > 6)
            {
                MessageBox.Show("최대 6개월 전까지 조회가 가능합니다");
                return;
            }

            this.Period = period;
            this.DialogResult = DialogResult.OK;
            
        }

        private void cancelBtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void PeriodSelectForm_Load(object sender, EventArgs e)
        {
            dayRadioBtn.PerformClick();
        }

        private void dayRadioBtn_Click(object sender, EventArgs e)
        {
            this.DateType = Constants.DATETYPE_DAY;
            dateLabel.Text = "일";
        }

        private void monthRadioBtn_Click(object sender, EventArgs e)
        {
            this.DateType = Constants.DATETYPE_MONTH;
            dateLabel.Text = "개월";
        }
    }
}
