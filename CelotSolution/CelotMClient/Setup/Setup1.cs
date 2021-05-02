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
using CelotMClient.Manager;
using System.ServiceProcess;

namespace CelotMClient.Setup
{
    public partial class Setup1 : UserControl
    {
        private SetupForm parent;
        private bool canDbConnected = false;
        public  bool isDirectInputMySqlName = false;
        private List<string> mySqlSeviceNameList;
        public Setup1(SetupForm setupForm)
        {
            this.parent = setupForm;
            mySqlSeviceNameList = new List<string>();
            InitializeComponent();
        }

        private void Setup1_Load(object sender, EventArgs e)
        {   
            ServiceController[] services  = ServiceController.GetServices();
            foreach(ServiceController controller in services){
                string serviceName = controller.ServiceName.ToUpper();
                if (serviceName.Contains("MYSQL"))
                {
                    mySqlSeviceNameList.Add(controller.ServiceName);
                }
            }
            if (mySqlSeviceNameList.Count < 1)
            {
                StringBuilder strBuilder = new StringBuilder();
                strBuilder.AppendLine("C-Cloud의 필수 요소인 MySql 이 설치되지 않은 상태인 것 같습니다");
                strBuilder.AppendLine("먼저 MySql 데이타 베이스를 설치해주세요");
                strBuilder.AppendLine("다른 이름으로 MySql 서비스를 설치했다면 [확인] 클릭하시고, 다음 페이지에서 직접 MySqlSevice 이름을 입력해주세요");
                strBuilder.AppendLine("진행하시겠습니까?");
                if (MessageBox.Show(strBuilder.ToString(), "확인", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    isDirectInputMySqlName = true;
                    SetControl();
                }
                else
                {
                    Application.Exit();
                }
            }else {
                isDirectInputMySqlName = false;
                SetControl();
            }
        }

        private void SetControl()
        {
            serviceNameBox.Text = parent.serviceName;
            serviceDiaplayBox.Text = parent.serviceDisplayName;
            servicePortBox.Text = parent.servicePort.ToString();
            sessionCountBox.Text = parent.maxSessionCount.ToString();
            protocolCombo.SelectedIndex = parent.protocol == 100 ? 0 : 1;
            if (this.isDirectInputMySqlName)
            {
                mySqlNameCombo.Enabled = false;
                mySqlNameBox.Enabled = true;
            }
            else
            {
                mySqlNameCombo.Enabled = true;
                mySqlNameBox.Enabled = false;
                mySqlNameCombo.DataSource = mySqlSeviceNameList;
                mySqlNameCombo.SelectedIndex = 0;
            }
           
            dbIdBox.Text = parent.dbId;
            dbPassBox.Text = parent.dbPass;
            remotePortBox.Text = parent.remotePort.ToString();
            downloadCountBox.Text = parent.concurrentDownloadCount.ToString();
            logFileBox.Text = parent.logFile;
            lowBatteryLimitBox.Text = parent.lowBatteryLimit.ToString();
            updatePeriodBox.Text = parent.updatePeriod.ToString();
            latBox.Text = parent.lat.ToString();
            lngBox.Text = parent.lng.ToString();
            desBox.Text = parent.des;
           
            dbHostBox.Text = parent.dbHost;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            fileDialog.InitialDirectory = Application.StartupPath;
            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                this.serviceFieBox.Text = fileDialog.FileName;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(dbHostBox.Text) || 
                String.IsNullOrEmpty(dbIdBox.Text)  ||
                String.IsNullOrEmpty(dbPassBox.Text))
            {
                MessageBox.Show("Database 관련 정보를 입력해주세요");
                return;
            }

            if (DatabaseManager.Instance().CheckConnection(
                dbHostBox.Text.Trim(), dbIdBox.Text.Trim(), dbPassBox.Text.Trim()
                ))
            {
                canDbConnected = true;
                parent.isDbConnected = true;
                MessageBox.Show("[SUCCESS] Database connected");
            }
            else
            {
                parent.isDbConnected = false;
                canDbConnected = false;
                MessageBox.Show(String.Format("[ERROR] Database not connected \n{0}", DatabaseManager.Instance().Message));
            }
        }
    }
}
