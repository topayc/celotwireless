using CelotMClient.CDao;
using CelotMClient.CustomControll;
using CelotMClient.CustomView;
using CelotMClient.Manager;
using CelotMClient.Model;
using CelotMClient.Model.NMS;
using CelotMClient.NMSStructure;
using CelotMClient.Util;
using CelotMClient.Worker;
using MySql.Data.MySqlClient;
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
    public partial class AlertExelForm : Form
    {
        private string fileNameSuffix;
        private string saveFileName;
        private List<NMSReportCommand> alertNMSReportCommandList;
        private NMSAlertManager nmsAlertManager = new NMSAlertManager();
        private int sessionId;
        private ColumnInfo[] columnInfos;
        private Type type;

        DevAge.Drawing.BorderLine border;
        DevAge.Drawing.RectangleBorder cellBorder;
        CellBackColorAlternate viewNormal;
        CheckBoxBackColorAlternate viewCheckBox;
        SourceGrid.Cells.Views.ColumnHeader viewColumnHeader;
        DevAge.Drawing.VisualElements.ColumnHeader backHeader;
        SourceGrid.Cells.Views.ColumnHeader viewColumnHeader1;
        DevAge.Drawing.VisualElements.ColumnHeader backHeader1;

        public AlertExelForm(Type type,int sessionId, string fileNameSuffix)
        {
            
            InitializeComponent();
            this.fileNameSuffix = fileNameSuffix;
            this.sessionId = sessionId;
            this.type = type;

            string[] columnTitles = new string[]{
                 "Name","Des","Group","Serial No","Phone Number","Router IP","Session Id",
                 "Band","Lan IP","Wan IP","Device Status"
                 ,"VPN","WIFI","SMS","Firmware","Ext Power",
                 "Battery","Signal","Period","NMS version","Occuerrence Time",
                 "Reset Time"
            };
            this.columnInfos = new ColumnInfo[columnTitles.Length];
            for (int i = 0; i < columnTitles.Length; i++)
            {
                columnInfos[i] = new ColumnInfo(columnTitles[i], true);
            }

            //그리드와 관련한 필요 object 생성
            //Border
            border = new DevAge.Drawing.BorderLine(Color.Black, 1);
            cellBorder = new DevAge.Drawing.RectangleBorder(border, border);

            //Views
            viewNormal = new CellBackColorAlternate(Color.Khaki, Color.DarkKhaki);
            viewNormal.Border = cellBorder;
            viewNormal.TextAlignment = DevAge.Drawing.ContentAlignment.MiddleCenter;
            viewNormal.Font = new Font("돋음", 8, FontStyle.Regular);
            viewCheckBox = new CheckBoxBackColorAlternate(Color.Khaki, Color.DarkKhaki);
            viewCheckBox.Border = cellBorder;

            //ColumnHeader view
            viewColumnHeader = new SourceGrid.Cells.Views.ColumnHeader();
            backHeader = new DevAge.Drawing.VisualElements.ColumnHeader();
            backHeader.BackColor = Color.DimGray;
            //backHeader.Border = DevAge.Drawing.RectangleBorder.NoBorder;
            viewColumnHeader.Background = backHeader;
            viewColumnHeader.ForeColor = Color.White;
            viewColumnHeader.Font = new Font("맑은 고딕", 10, FontStyle.Bold);

            viewColumnHeader1 = new SourceGrid.Cells.Views.ColumnHeader();
            backHeader1 = new DevAge.Drawing.VisualElements.ColumnHeader();
            backHeader1.BackColor = Color.DarkSlateGray;
            viewColumnHeader1.Background = backHeader1;
            //viewColumnHeader1.Border = cellBorder; 
            viewColumnHeader1.ForeColor = Color.White;
            viewColumnHeader1.Font = new Font("굴림", 8, FontStyle.Regular);
            viewColumnHeader1.TextAlignment = DevAge.Drawing.ContentAlignment.MiddleCenter;
        }

        private void AlertExelForm_Load(object sender, EventArgs e)
        {
            this.PrepareExcelForm();
        }


        private void PrepareExcelForm()
        {
            CCloudDao cDao = new CCloudDao();
            cDao.NotifyDataBaseFinished += new NotifyDataBaseFinishedHandler(Data_Receviced);
            cDao.GetNMSLogReportCommandList(this.sessionId);
        }

        private void Data_Receviced(object Sender, DataBaseFinishedEventArgs e)
        {
            List<NMSReportCommand> nmsReportCommandList = ((NMSDataWrapper)e.Result).NMSReportCommandList;
            if (nmsReportCommandList.Count > 0)
            {
                this.alertNMSReportCommandList = new List<NMSReportCommand>();
                foreach (NMSReportCommand nmsReportCommand in nmsReportCommandList)
                {
                    if (nmsReportCommand.HaveAlert())
                    {
                        this.alertNMSReportCommandList.Add(nmsReportCommand);
                    }
                }
                this.SetAlertGrid();
            }
            else
            {
                MessageBox.Show("해당 디바이스의 로그가 존재하지 않습니다");
            }
        }

        private void SetAlertGrid()
        {
            this.alertGrid.Controls.Clear();
            this.alertGrid.SuspendLayout();
            this.InitAlertGridHeader();
            foreach (NMSReportCommand nmsReportCommand in this.alertNMSReportCommandList)
            {
                this.AddRowToAlertGrid(nmsReportCommand);
            }
            this.alertGrid.ResumeLayout();
            this.alertGrid.AutoSizeCells();
        }

        private void saveFileDialogBtn_Click(object sender, EventArgs e)
        {
            this.saveFileDialog.Filter = "Excel Files (*.xls)|*.xls";
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                string fileName = this.saveFileDialog.FileName;
                this.saveFileName = fileName;
                this.saveFileBox.Text = this.saveFileName;
            }

            this.ExportAlertDataToExcel();

        }
        private void ExportAlertDataToExcel()
        {
            if (String.IsNullOrEmpty(this.saveFileName))
            {
                MessageBox.Show("저장할 파일명을 선택하세요");
                return;
            }
            try {
                ExcelManager excelManager = new ExcelManager();
                excelManager.Export(this.GeneraterDataTableWithAlert(), this.saveFileName);
                MessageBox.Show(String.Format("{0} 파일로 출력을 완료했습니다", this.saveFileName));
            }
            catch (Exception ee)
            {
                Debug.WriteLine(ee.Message);
            }
        }

        private DataTable GeneraterDataTableWithAlert()
        {
            DataTable table = new DataTable("Alert");
            DataColumn col;
            DataRow row;
            foreach (ColumnInfo columnInfo  in this.columnInfos)
            {
                col = new DataColumn(columnInfo.ColumnName, typeof(string));
                if (columnInfo.ColumnName.Equals("Reset Time"))
                {
                    col.MaxLength = 100;
                }
                table.Columns.Add(col);
            }

            foreach(NMSReportCommand command in this.alertNMSReportCommandList){
                row = table.NewRow();
                CelotMClient.Model.Device device = command.Device;
                nms_reprot_t nms = command.nms_reprot_t;

                row["Name"] = device == null ? "" : device.Name;
                row["Des"]    = device == null ? "" : device.Des;
                row["Group"] = device == null ? "" : device.GroupName;
                row["Serial No"] = device == null ? "" : device.SerialNo.ToString();
                row["Phone Number"] = device == null ? "" : device.PhoneNumber.ToString();
                row["Router IP"] = device == null ? "" : device.RouterIp;
                row["Session Id"] = nms == null ? "" : nms.header.session_id.ToString();
                row["Band"] = nms == null ? "" : nms.data.moduleband.ToString();
                row["Lan IP"] = nms == null ? "" : command.GetLanIPString();
                row["Wan IP"] = nms == null ? "" : command.GetWanIPString();
                row["Device Status"] = nms == null ? "" : nms.data.devicestatus.ToString();
                row["VPN"] = nms == null ? "" : nms.data.vpnstatus.ToString();
                row["WIFI"] = nms == null ? "" : nms.data.wifistatus.ToString();
                row["SMS"] = nms == null ? "" : nms.data.newsms.ToString();
                row["Firmware"] = nms == null ? "" : nms.data.sw_version;
                row["Ext Power"] = nms == null ? "" : nms.data.external_power.ToString();
                row["Battery"] = nms == null ? "" : nms.data.ext_device1[0].ToString();
                row["Signal"] = nms == null ? "" : nms.data.modulesignal.ToString();
                row["Period"] = nms == null ? "" : nms.data.rpt_time.ToString();
                row["NMS version"] = nms == null ? "" : nms.header.pro_ver.ToString();

                string currentTime = "";
                if (nms != null)
                {
                    currentTime = CelotUtility.ChangeStampStringToLocalFormat(nms.data.current_time);
                }
                row["Occuerrence Time"] = currentTime;

                string resetTime = "";
                if (device != null && device.ResetTime != 0)
                {
                    resetTime = CelotUtility.UnixTimeStampToDateString(device.ResetTime);
                }
                row["Reset Time"] = resetTime;
                table.Rows.Add(row);
            }
            table.AcceptChanges();
            return table;
        }

        private void InitAlertGridHeader(){
            this.alertGrid.SuspendLayout();
            this.alertGrid.ColumnsCount = this.columnInfos.Length;
            SourceGrid.Cells.ColumnHeader columnHeader;
            this.alertGrid.Rows.Insert(0);
            for (int i = 0; i < this.columnInfos.Length; i++)
            {
                columnHeader = new SourceGrid.Cells.ColumnHeader(columnInfos[i].ColumnName);
                columnHeader.View = viewColumnHeader1;
                this.alertGrid[0, i] = columnHeader;
            }
            this.alertGrid.SuspendLayout();
            this.alertGrid.AutoSizeCells();
            
        }

        private void AddRowToAlertGrid(NMSReportCommand nmsReportCommand)
        {
            CelotMClient.Model.Device device = nmsReportCommand.Device;
            nms_reprot_t nms = nmsReportCommand.nms_reprot_t;

            int insertRowCount = this.alertGrid.Rows.Count;
            this.alertGrid.Rows.Insert(insertRowCount);

            SourceGrid.Cells.Views.ColumnHeader nameHeaderView1 = new SourceGrid.Cells.Views.ColumnHeader();
            DevAge.Drawing.VisualElements.ColumnHeader namebackHeader1 = new DevAge.Drawing.VisualElements.ColumnHeader();
            namebackHeader1.BackColor = Color.DarkSlateGray;
            nameHeaderView1.Background = namebackHeader1;
            nameHeaderView1.Border = cellBorder;
            nameHeaderView1.ForeColor = Color.White;
            nameHeaderView1.Font = new Font("굴림", 8, FontStyle.Regular);
            nameHeaderView1.TextAlignment = DevAge.Drawing.ContentAlignment.MiddleCenter;

            alertGrid[insertRowCount, 0] = new SourceGrid.Cells.Cell(device == null ? "" : device.Name);
            alertGrid[insertRowCount, 0].View = viewNormal;

            alertGrid[insertRowCount, 1] = new SourceGrid.Cells.Cell(device == null ? "" : device.Des);
            alertGrid[insertRowCount, 1].View = viewNormal;

            alertGrid[insertRowCount, 2] = new SourceGrid.Cells.Cell(device == null ? "" : device.GroupName);
            alertGrid[insertRowCount, 2].View = viewNormal;

            alertGrid[insertRowCount, 3] = new SourceGrid.Cells.Cell(device == null ? "" : device.SerialNo.ToString());
            alertGrid[insertRowCount, 3].View = viewNormal;

            alertGrid[insertRowCount, 4] = new SourceGrid.Cells.Cell(device == null ? "" : CelotUtility.ChangePhoneNumberToFormatString(device.PhoneNumber));
            alertGrid[insertRowCount, 4].View = viewNormal;

            alertGrid[insertRowCount, 5] = new SourceGrid.Cells.Cell(device == null ? "" : device.RouterIp);
            alertGrid[insertRowCount, 5].View = viewNormal;

            alertGrid[insertRowCount, 6] = new SourceGrid.Cells.Cell(nms == null ? "" : nms.header.session_id.ToString());
            alertGrid[insertRowCount, 6].View = viewNormal;

            alertGrid[insertRowCount, 7] = new SourceGrid.Cells.Cell(nms == null ? "" : nms.data.moduleband.ToString());
            alertGrid[insertRowCount, 7].View = viewNormal;

            alertGrid[insertRowCount, 8] = new SourceGrid.Cells.Cell(nms == null ? "" : nmsReportCommand.GetLanIPString());
            alertGrid[insertRowCount, 8].View = viewNormal;

            alertGrid[insertRowCount, 9] = new SourceGrid.Cells.Cell(nms == null ? "" : nmsReportCommand.GetWanIPString());
            alertGrid[insertRowCount, 9].View = viewNormal;

            alertGrid[insertRowCount, 10] = new SourceGrid.Cells.Cell(nms == null ? "" : nms.data.devicestatus.ToString());
            alertGrid[insertRowCount, 10].View = viewNormal;

            alertGrid[insertRowCount, 11] = new SourceGrid.Cells.Cell(nms == null ? "" : nms.data.vpnstatus.ToString());
            alertGrid[insertRowCount, 11].View = viewNormal;

            alertGrid[insertRowCount, 12] = new SourceGrid.Cells.Cell(nms == null ? "" : nms.data.wifistatus.ToString());
            alertGrid[insertRowCount, 12].View = viewNormal;

            alertGrid[insertRowCount, 13] = new SourceGrid.Cells.Cell(nms == null ? "" : nms.data.newsms.ToString());
            alertGrid[insertRowCount, 13].View = viewNormal;

            alertGrid[insertRowCount, 14] = new SourceGrid.Cells.Cell(nms == null ? "" : nms.data.sw_version);
            alertGrid[insertRowCount, 14].View = viewNormal;

            alertGrid[insertRowCount, 15] = new SourceGrid.Cells.Cell(nms == null ? "" : nms.data.external_power.ToString());
            alertGrid[insertRowCount, 15].View = viewNormal;

            alertGrid[insertRowCount, 16] = new SourceGrid.Cells.Cell(nms == null ? "" : nms.data.ext_device1[0].ToString());
            alertGrid[insertRowCount, 16].View = viewNormal;

            SignalBar signal = new SignalBar();
            signal.Dock = DockStyle.None;
            signal.Minimum = 0;
            signal.Maximum = 4;
            signal.BackColor = Color.SlateGray;

            if (nms != null)
            {
                signal.CurrentValue = nmsAlertManager.getRssiLevel(nms.data.modulesignal);
            }
            else
            {
                signal.CurrentValue = 0;
            }

            SourceGrid.Cells.Cell signalCell = new SourceGrid.Cells.Cell();
            alertGrid[insertRowCount, 17] = signalCell;
            alertGrid[insertRowCount, 17].View = viewNormal;
            SourceGrid.LinkedControlValue linkedControlValue = new SourceGrid.LinkedControlValue(signal, new SourceGrid.Position(insertRowCount, 17));
            alertGrid.LinkedControls.Add(linkedControlValue);

            alertGrid[insertRowCount, 18] = new SourceGrid.Cells.Cell((nms == null ? "" : nms.data.rpt_time.ToString()));
            alertGrid[insertRowCount, 18].View = viewNormal;

            alertGrid[insertRowCount, 19] = new SourceGrid.Cells.Cell((nms == null ? "" : nms.header.pro_ver.ToString()));
            alertGrid[insertRowCount, 19].View = nameHeaderView1;
   
            string currentTime = CelotUtility.ChangeStampStringToLocalFormat(nms.data.current_time);
            alertGrid[insertRowCount, 20] = new SourceGrid.Cells.Cell(currentTime);
            alertGrid[insertRowCount, 20].View = viewNormal;

            string resetTime = "";
            if (device != null && device.ResetTime != 0)
            {
                resetTime = CelotUtility.UnixTimeStampToDateString(device.ResetTime);
            }
            alertGrid[insertRowCount, 21] = new SourceGrid.Cells.Cell(resetTime);
            alertGrid[insertRowCount, 21].View = viewNormal;
        }

        private void closeBtn_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }


        private class CellBackColorAlternate : SourceGrid.Cells.Views.Cell
        {
            public CellBackColorAlternate(Color firstColor, Color secondColor)
            {
                FirstBackground = new DevAge.Drawing.VisualElements.BackgroundSolid(firstColor);
                SecondBackground = new DevAge.Drawing.VisualElements.BackgroundSolid(secondColor);
            }

            private DevAge.Drawing.VisualElements.IVisualElement mFirstBackground;
            public DevAge.Drawing.VisualElements.IVisualElement FirstBackground
            {
                get { return mFirstBackground; }
                set { mFirstBackground = value; }
            }

            private DevAge.Drawing.VisualElements.IVisualElement mSecondBackground;
            public DevAge.Drawing.VisualElements.IVisualElement SecondBackground
            {
                get { return mSecondBackground; }
                set { mSecondBackground = value; }
            }

            protected override void PrepareView(SourceGrid.CellContext context)
            {
                base.PrepareView(context);

                if (Math.IEEERemainder(context.Position.Row, 2) == 0)
                    Background = FirstBackground;
                else
                    Background = SecondBackground;
            }
        }

        private class CheckBoxBackColorAlternate : SourceGrid.Cells.Views.CheckBox
        {
            public CheckBoxBackColorAlternate(Color firstColor, Color secondColor)
            {
                FirstBackground = new DevAge.Drawing.VisualElements.BackgroundSolid(firstColor);
                SecondBackground = new DevAge.Drawing.VisualElements.BackgroundSolid(secondColor);
            }

            private DevAge.Drawing.VisualElements.IVisualElement mFirstBackground;
            public DevAge.Drawing.VisualElements.IVisualElement FirstBackground
            {
                get { return mFirstBackground; }
                set { mFirstBackground = value; }
            }

            private DevAge.Drawing.VisualElements.IVisualElement mSecondBackground;
            public DevAge.Drawing.VisualElements.IVisualElement SecondBackground
            {
                get { return mSecondBackground; }
                set { mSecondBackground = value; }
            }

            protected override void PrepareView(SourceGrid.CellContext context)
            {
                base.PrepareView(context);

                if (Math.IEEERemainder(context.Position.Row, 2) == 0)
                    Background = FirstBackground;
                else
                    Background = SecondBackground;
            }

        }
    }
}
