using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CelotMClient.Model;
using CelotMClient.CustomForm;
using CelotMClient.Worker;
using System.Diagnostics;
using CelotMClient.Manager;
using System.Globalization;
using MySql.Data.MySqlClient;
using CelotMClient.Model.NMS;
using CelotMClient.NMSStructure;
using CelotMClient.CDao;
using CelotMClient.Util;

namespace CelotMClient.CustomView
{
    public partial class Alerts : UserControl
    {
        public static string[] columnsTitles = new string[]{
                 "Name","Phone Number","Occurrence Time","TYPE", 
                 "Lan IP","Wan IP","Description","Time Duration","mode","NMS VERSION"
            };
        private ColumnInfo[] columnInfos;

        private List<NMSReportCommand> nmsReportCommandList;
        public List<NMSReportCommand> alertNmsReportCommandList;

        public int selectedDeviceSessionId = -1;
        public int selectedDevicePhoneNumber = -1;
        public int selectedDeviceIndex = -1;

        public string searchKeyword = "";
        public int searchOptionIndex = 0;
        public string searchOptionStr = "";


        DevAge.Drawing.BorderLine border;
        DevAge.Drawing.RectangleBorder cellBorder;
        CellBackColorAlternate viewNormal;
        CheckBoxBackColorAlternate viewCheckBox;
        SourceGrid.Cells.Views.ColumnHeader viewColumnHeader;
        DevAge.Drawing.VisualElements.ColumnHeader backHeader;
        SourceGrid.Cells.Views.ColumnHeader viewColumnHeader1;
        DevAge.Drawing.VisualElements.ColumnHeader backHeader1;

        public Alerts()
        {
            InitializeComponent();
            this.selectedDevicePhoneNumber = -1;
            this.selectedDeviceSessionId = -1;
            this.selectedDeviceIndex = -1;

            //그리드와 관련한 필요 object 생성
            //Border
            border = new DevAge.Drawing.BorderLine(Color.Black, 1);
            cellBorder = new DevAge.Drawing.RectangleBorder(border, border);

            //Views
            viewNormal = new CellBackColorAlternate(Color.Khaki, Color.DarkKhaki);
            viewNormal.Border = cellBorder;
            viewNormal.Font = new Font("", 9, FontStyle.Regular);
            viewNormal.TextAlignment = DevAge.Drawing.ContentAlignment.MiddleCenter;
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

            this.searchCombo.SelectedIndex = this.searchOptionIndex;
            this.searchTextBox.Text = this.searchKeyword;

        }

        private void initColumnInfo(string[] columnsTitles)
        {
            columnInfos = new ColumnInfo[columnsTitles.Length];
            for (int i = 0; i < columnsTitles.Length; i++)
            {
                columnInfos[i] = new ColumnInfo(columnsTitles[i], true);
            }
        }

        private void Alerts_Load(object sender, EventArgs e)
        {
            this.Visible = false;
            this.initColumnInfo(columnsTitles);
            this.InitAlertDetailGridColumn();
            this.InitCurStatusGridColumn();
            this.initChart();
            this.PrepareAlert();
            ApplicationCache.Instance().NMSDataNotify += new NMSDataNotiHandler(this.Alert_NMSDataNotify);
        }

        private void Alert_NMSDataNotify(object sender, NMSDataNotifyEventArgs args)
        {
            this.Invoke(new Action(delegate()
            {
                NMSDataWrapper nmsDataWrapper = args.Data;
                this.nmsReportCommandList = nmsDataWrapper.NMSReportCommandList;

                this.InitAlertGridColumn();
                this.alertNmsReportCommandList = this.FilterAlertDevice(this.nmsReportCommandList);
                if (this.alertNmsReportCommandList.Count > 0)
                {
                    this.SuspendLayout();
                    if (!String.IsNullOrEmpty(this.searchKeyword))
                    {
                        this.FilterDeviceBySearchkeyword();
                    }

                    this.Visible = true;
                    this.SetAlertGrid();
                    this.setAlertColumnShowSetting();

                    NMSReportCommand tmpCommand = null;
                    if (this.selectedDevicePhoneNumber == -1 && this.selectedDeviceSessionId == -1)
                    {
                        if (this.alertNmsReportCommandList.Count > 0)
                        {
                            tmpCommand = alertNmsReportCommandList[0];
                            this.selectedDeviceIndex = 1;
                        }
                    }
                    else
                    {
                        if (this.selectedDevicePhoneNumber != -1)
                        {
                            tmpCommand = alertNmsReportCommandList.Find(
                                r =>
                                {
                                    if (r.Device != null && r.Device.PhoneNumber == this.selectedDevicePhoneNumber)
                                    {
                                        return true;
                                    }
                                    return false;
                                }
                              );
                        }
                        else
                        {
                            tmpCommand = alertNmsReportCommandList.Find(
                                r =>
                                {
                                    if (r.nms_reprot_t != null && r.nms_reprot_t.header.session_id == this.selectedDeviceSessionId)
                                    {
                                        return true;
                                    }
                                    return false;
                                }
                            );
                        }
                    }
                    if (this.alertNmsReportCommandList.Count > 0)
                    {
                        this.selectedDevicePhoneNumber = tmpCommand.Device != null ? tmpCommand.Device.PhoneNumber : -1;
                        this.selectedDeviceSessionId = tmpCommand.nms_reprot_t != null ? (int)tmpCommand.nms_reprot_t.header.session_id : -1;
                        this.alertGrid.Selection.SelectRow(this.selectedDeviceIndex, true);
                        this.SetCurStatusGridData(tmpCommand.AlertList);
                        this.ShowAlertDetail(tmpCommand);
                    }
                    this.ResumeLayout(true);
                }
                else
                {
                    this.Visible = true;
                }
                nmsDataWrapper = null;
            }));
        }
      

        private void PrepareAlert()
        {
            if (ApplicationCache.Instance().CurNMSDataWrapper == null) return;
            NMSDataWrapper nmsDataWrapper = ApplicationCache.Instance().CurNMSDataWrapper;
            this.nmsReportCommandList = nmsDataWrapper.NMSReportCommandList;

            this.InitAlertGridColumn();
            this.alertNmsReportCommandList = this.FilterAlertDevice(this.nmsReportCommandList);
            if (this.alertNmsReportCommandList.Count > 0)
            {
                this.SuspendLayout();
                if (!String.IsNullOrEmpty(this.searchKeyword))
                {
                    this.FilterDeviceBySearchkeyword();
                }

                this.Visible = true;
                this.SetAlertGrid();
                this.setAlertColumnShowSetting();

                NMSReportCommand tmpCommand = null;
                if (this.selectedDevicePhoneNumber == -1 && this.selectedDeviceSessionId == -1)
                {
                    if (this.alertNmsReportCommandList.Count > 0)
                    {
                        tmpCommand = alertNmsReportCommandList[0];
                        this.selectedDeviceIndex = 1;
                    }
                }
                else
                {
                    if (this.selectedDevicePhoneNumber != -1)
                    {
                        tmpCommand = alertNmsReportCommandList.Find(
                            r =>
                            {
                                if (r.Device != null && r.Device.PhoneNumber == this.selectedDevicePhoneNumber)
                                {
                                    return true;
                                }
                                return false;
                            }
                          );
                    }
                    else
                    {
                        tmpCommand = alertNmsReportCommandList.Find(
                            r =>
                            {
                                if (r.nms_reprot_t != null && r.nms_reprot_t.header.session_id == this.selectedDeviceSessionId)
                                {
                                    return true;
                                }
                                return false;
                            }
                        );
                    }
                }
                if (this.alertNmsReportCommandList.Count > 0)
                {
                    this.selectedDevicePhoneNumber = tmpCommand.Device != null ? tmpCommand.Device.PhoneNumber : -1;
                    this.selectedDeviceSessionId = tmpCommand.nms_reprot_t != null ? (int)tmpCommand.nms_reprot_t.header.session_id : -1;
                    this.alertGrid.Selection.SelectRow(this.selectedDeviceIndex, true);
                    this.SetCurStatusGridData(tmpCommand.AlertList);
                    this.ShowAlertDetail(tmpCommand);
                }
                this.ResumeLayout(true);
            }
            else
            {
                this.Visible = true;
            }
            nmsDataWrapper = null;
        }

        private void FilterDeviceBySearchkeyword()
        {
            //MessageBox.Show(String.Format("{0} :{1} : {2}", this.searchOptionStr, this.searchKeyword,this.nmsReportCommandList.Count));
            switch (this.searchOptionStr)
            {
                case "Name":
                    var result =
                        this.alertNmsReportCommandList.Where(
                            r =>
                            {
                                if (r.Device != null)
                                {
                                    if (r.Device.Name.Contains(this.searchKeyword)) return true;
                                }
                                return false;
                            }

                            );
                    List<NMSReportCommand> commandList = new List<NMSReportCommand>();
                    foreach (NMSReportCommand command in result)
                    {
                        commandList.Add(command);
                    }
                    this.alertNmsReportCommandList = commandList;
                    break;
                case "Phone Number":
                    var result1 =
                       this.alertNmsReportCommandList.Where(
                            r =>
                            {
                                if (r.Device != null)
                                {
                                    if (Convert.ToString(r.Device.PhoneNumber).Contains(this.searchKeyword)) return true;
                                }
                                else if (r.nms_reprot_t != null)
                                {
                                    if (Convert.ToString(r.nms_reprot_t.header.session_id).Contains(this.searchKeyword)) return true;
                                }
                                return false;
                            }
                         );
                    List<NMSReportCommand> commandList1 = new List<NMSReportCommand>();
                    foreach (NMSReportCommand command in result1)
                    {
                        commandList1.Add(command);
                    }
                    this.alertNmsReportCommandList = commandList1;
                    break;

                case "Router(LAN) IP":
                    var result2 =
                       this.alertNmsReportCommandList.Where(
                            r =>
                            {
                                if (r.Device != null)
                                {
                                    if (r.Device.RouterIp.Contains(this.searchKeyword)) return true;
                                }
                                else if (r.nms_reprot_t != null)
                                {
                                    if (r.GetLanIPString().Contains(this.searchKeyword)) return true;
                                }
                                return false;
                            }
                         );
                    List<NMSReportCommand> commandList2 = new List<NMSReportCommand>();
                    foreach (NMSReportCommand command in result2)
                    {
                        commandList2.Add(command);
                    }
                    
                    this.alertNmsReportCommandList = commandList2;
                    break;

                case "WAN IP":
                    var result3 =
                        this.alertNmsReportCommandList.Where(
                               r =>
                               {
                                   if (r.nms_reprot_t != null)
                                   {
                                       if (r.GetWanIPString().Contains(this.searchKeyword)) return true;
                                   }
                                   return false;
                               }
                            );
                    List<NMSReportCommand> commandList3 = new List<NMSReportCommand>();
                    foreach (NMSReportCommand command in result3)
                    {
                        commandList3.Add(command);
                    }
                    this.alertNmsReportCommandList = commandList3;
                    break;
            }
        }

        private void setAlertColumnShowSetting()
        {
            Dictionary<String, int> alertSettingMap = ApplicationConfig.Instance().AlertColumnSetting;
            for (int i = 0; i < columnsTitles.Length; i++)
            {
                string columnName = columnsTitles[i];
                this.alertGrid.Columns[i].Visible = alertSettingMap[columnName] == 1 ? true : false;
            }
        }
      

        private List<NMSReportCommand> FilterAlertDevice(List<NMSReportCommand> nmsReportCommandList)
        {
            List<NMSReportCommand> alertList = new List<NMSReportCommand>();
            foreach (NMSReportCommand nmsReportCommand in nmsReportCommandList)
            {
                if (nmsReportCommand.DeviceAlertCassification != DeviceAlertCassification.NORMAL)
                {
                    alertList.Add(nmsReportCommand);
                }
            }
            return alertList;
        }
    
        public void ExportAlert(int row)
        {
            NMSReportCommand nmsReportCommand = this.alertNmsReportCommandList[row - 1];
            if (nmsReportCommand.nms_reprot_t == null  )
            {
                MessageBox.Show("선택하신 디바이스는 No NMS Report 상태로 기록된 log 가 없습니다");
                return;
            }
            int sessionId = (int)nmsReportCommand.nms_reprot_t.header.session_id;
            AlertExelForm excel = new AlertExelForm(Type.GetType("CelotMClient.NMSStructure.NMSReportCommand"), sessionId, "AlertDevice");
            excel.ShowDialog();
        }


        public void SetAlertGrid()
        {
            this.alertGrid.SuspendLayout();
            foreach(NMSReportCommand command in this.alertNmsReportCommandList)
            {
                this.AddRowToAlertGridData(command);
            }
            this.alertGrid.AutoSizeCells();
            this.alertGrid.ResumeLayout();
            
         
           
        }
        public void InitAlertGridColumn()
        {
            this.alertGrid.Rows.Clear();
            HeaderRightController headerController = new HeaderRightController(this, this.columnInfos);
            this.alertGrid.ColumnsCount = this.columnInfos.Length;
            SourceGrid.Cells.ColumnHeader columnHeader;
            this.alertGrid.Rows.Insert(0);
            for (int i = 0; i < this.columnInfos.Length; i++)
            {
                columnHeader = new SourceGrid.Cells.ColumnHeader(columnInfos[i].ColumnName);
                columnHeader.View = viewColumnHeader1;
                this.alertGrid[0, i] = columnHeader;
                this.alertGrid[0, i].AddController(headerController);
            }
        }

        public void AddRowToAlertGridData(NMSReportCommand nmsReportCommand)
        {
            PopupMenu menuController = new PopupMenu(this);
            CellClickEvent clickController = new CellClickEvent(this);

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

            alertGrid[insertRowCount, 0] = new SourceGrid.Cells.Cell(nmsReportCommand.Device == null ? "" : nmsReportCommand.Device.Name);
            alertGrid[insertRowCount, 0].View = viewNormal;
            alertGrid[insertRowCount, 0].AddController(menuController);
            alertGrid[insertRowCount, 0].AddController(clickController);

            alertGrid[insertRowCount, 1] = new SourceGrid.Cells.Cell(nmsReportCommand.Device == null ? "" :  CelotUtility.ChangePhoneNumberToFormatString(nmsReportCommand.Device.PhoneNumber));
            alertGrid[insertRowCount, 1].View = viewNormal;
            alertGrid[insertRowCount, 1].AddController(menuController);
            alertGrid[insertRowCount, 1].AddController(clickController);

            //DateTime curTime = DateTime.ParseExact(deviceReportCommand.CurrentTime, "yyyyMMddHHmmss", null);
            //alertGrid[insertRowCount, 2] = 
            //    new SourceGrid.Cells.Cell(String.Format("{0:yyyy-MM-dd HH:mm:ss}",curTime ));
            alertGrid[insertRowCount, 2] = new SourceGrid.Cells.Cell(nmsReportCommand.nms_reprot_t == null ? "" : CelotUtility.ChangeStampStringToLocalFormat(nmsReportCommand.nms_reprot_t.data.current_time));
            alertGrid[insertRowCount, 2].View = viewNormal;
            alertGrid[insertRowCount, 2].AddController(menuController);
            alertGrid[insertRowCount, 2].AddController(clickController);

            alertGrid[insertRowCount, 3] = new SourceGrid.Cells.Cell(nmsReportCommand.DeviceAlertCassification.ParseShort());
            alertGrid[insertRowCount, 3].View = viewNormal;
            alertGrid[insertRowCount, 3].AddController(menuController);
            alertGrid[insertRowCount, 3].AddController(clickController);

            alertGrid[insertRowCount, 4] = new SourceGrid.Cells.Cell(nmsReportCommand.nms_reprot_t == null ? "" : nmsReportCommand.GetLanIPString());
            alertGrid[insertRowCount, 4].View = viewNormal;
            alertGrid[insertRowCount, 4].AddController(menuController);
            alertGrid[insertRowCount, 4].AddController(clickController);

            alertGrid[insertRowCount, 5] = new SourceGrid.Cells.Cell(nmsReportCommand.nms_reprot_t == null ? "" : nmsReportCommand.GetWanIPString());
            alertGrid[insertRowCount, 5].View = viewNormal;
            alertGrid[insertRowCount, 5].AddController(menuController);
            alertGrid[insertRowCount, 5].AddController(clickController);

            alertGrid[insertRowCount, 6] = new SourceGrid.Cells.Cell(nmsReportCommand.Device == null ? "" : nmsReportCommand.Device.Des);
            alertGrid[insertRowCount, 6].View = viewNormal;
            alertGrid[insertRowCount, 6].AddController(menuController);
            alertGrid[insertRowCount, 6].AddController(clickController);

            string alertTimeAgo = "";
            if (nmsReportCommand.Device != null)
            {
                if (nmsReportCommand.AlertList.Count == 1 && nmsReportCommand.AlertList[0].AlertType == AlertType.Router)
                {
                    alertTimeAgo = nmsReportCommand.AlertList[0].AlertDuration;
                }
                else if (nmsReportCommand.Device.AlertStatus == 2)
                {
                    alertTimeAgo = CelotUtility.getTimeAgo(CelotUtility.ConvertToDateTime(nmsReportCommand.Device.AlertOccurentTime));
                }
                
            }
            alertGrid[insertRowCount, 7] = new SourceGrid.Cells.Cell(alertTimeAgo);
            alertGrid[insertRowCount, 7].View = viewNormal;
            alertGrid[insertRowCount, 7].AddController(menuController);
            alertGrid[insertRowCount, 7].AddController(clickController);

            alertGrid[insertRowCount, 8] = new SourceGrid.Cells.Cell(nmsReportCommand.nms_reprot_t == null ? "" : ((MessageType)nmsReportCommand.nms_reprot_t.header.message_type).Parse());
            alertGrid[insertRowCount, 8].View = viewNormal;
            alertGrid[insertRowCount, 8].AddController(menuController);
            alertGrid[insertRowCount, 8].AddController(clickController);

            alertGrid[insertRowCount, 9] = new SourceGrid.Cells.Cell(nmsReportCommand.nms_reprot_t == null ? "" : nmsReportCommand.nms_reprot_t.header.pro_ver.ToString());
            alertGrid[insertRowCount, 9].View = viewNormal;
            alertGrid[insertRowCount, 9].AddController(menuController);
            alertGrid[insertRowCount, 9].AddController(clickController);
        }

        public void InitCurStatusGridColumn()
        {
            this.curStatusGrid.Controls.Clear();
            this.curStatusGrid.Columns.Add("AlertTypeName", "경고 타입", typeof(string));
            this.curStatusGrid.Columns.Add("ErrorMessage", "경고 메세지", typeof(string));
            this.curStatusGrid.AutoStretchColumnsToFitWidth = true;
        }

        public void InitAlertDetailGridColumn()
        {
            this.alertDetailGrid.Controls.Clear();
            this.alertDetailGrid.Columns.Add("Name", "경고 타입", typeof(string));
            this.alertDetailGrid.Columns.Add("Value", "통계", typeof(int));
            this.alertDetailGrid.AutoStretchColumnsToFitWidth = true;
        }

        public void initChart()
        {
            alertChart.BackColor = Color.FromArgb(255, 90,90,90);
            alertChart.ChartAreas[0].BackColor = Color.FromArgb(255, 90, 90, 90);
        }

        public void SetCurStatusGridData(List<AlertClass> alertList)
        {
            this.curStatusGrid.DataSource = null;
            DevAge.ComponentModel.BoundList<AlertClass> mBoundList =
                new DevAge.ComponentModel.BoundList<AlertClass>(alertList);
            mBoundList.AllowDelete = false;
            mBoundList.AllowNew = false;
            mBoundList.AllowEdit = false;
            this.curStatusGrid.DataSource = mBoundList;
        }


        public void SetAlertDetailGridData(List<NameValuePair> pairList)
        {
            this.alertDetailGrid.DataSource = null;
            DevAge.ComponentModel.BoundList<NameValuePair> mBoundList =
                new DevAge.ComponentModel.BoundList<NameValuePair>(pairList);
            mBoundList.AllowDelete = false;
            mBoundList.AllowNew = false;
            mBoundList.AllowEdit = false;
            this.alertDetailGrid.DataSource = mBoundList;
        }

        public void setAlertChart(List<NameValuePair> pairList)
        {
            alertChart.DataSource = null;
            alertChart.DataSource = pairList;
            alertChart.Invalidate();
        }

       public void AlertClicked(int row, int column)
        {
            this.selectedDeviceIndex = row;
            NMSReportCommand nmsReprotCommand = this.alertNmsReportCommandList[row - 1];
            this.selectedDevicePhoneNumber = nmsReprotCommand.Device != null ? nmsReprotCommand.Device.PhoneNumber : -1;
            this.selectedDeviceSessionId = nmsReprotCommand.nms_reprot_t != null ? (int)nmsReprotCommand.nms_reprot_t.header.session_id : -1;
            this.ShowAlertDetail(nmsReprotCommand);
        }

        private void ShowAlertDetail(NMSReportCommand nmsReprotCommand)
        {
            List<AlertClass> alertList = nmsReprotCommand.AlertList;
            this.SetCurStatusGridData(alertList);

            if (nmsReprotCommand.nms_reprot_t == null)
            {
                List<NameValuePair> blankList1 = new List<NameValuePair>();
                this.SetAlertDetailGridData(blankList1);

                this.alertChart.Series[0].LegendText = @"#VALX";
                List<NameValuePair> blankList2 = new List<NameValuePair>();
                blankList2.Add(new NameValuePair { Name = "None", Value = 1 });
                this.setAlertChart(blankList2);

            }
            else
            {
                this.alertChart.Series[0].LegendText = @"#VALX (#VAL)";
                this.GetAlertChartData(nmsReprotCommand);
            }
        }

        private void GetAlertChartData(NMSReportCommand nmsReportCommand)
        {
            if (nmsReportCommand.nms_reprot_t == null)
            {
                MessageBox.Show("선택하신 디바이스는 No NMS Report 상태로 현재 상태의 정보만 가능합니다");
                return;
            }

            int sessionId = (int)nmsReportCommand.nms_reprot_t.header.session_id;
            CCloudDao dao = new CCloudDao(false);
            dao.NotifyDataBaseFinished += new NotifyDataBaseFinishedHandler(this.DeviceChartStats_receved);
            dao.GetNMSLogReportCommandList(sessionId);
        }

        private void DeviceChartStats_receved(object Sender, DataBaseFinishedEventArgs e)
        {
            NMSDataWrapper nmsDataWrapper = (NMSDataWrapper)e.Result;
            if (nmsDataWrapper == null)
            {
                MessageBox.Show("널입니다");
            }

            List<NameValuePair> pairList = nmsDataWrapper.AllAlertsStats;
            if (!this.IsDisposed)
            {
                this.SetAlertDetailGridData(pairList);
                this.setAlertChart(pairList);
            }
        }


        public class CellClickEvent : SourceGrid.Cells.Controllers.ControllerBase
        {
            Alerts alert;
            bool isClicking = false;
            public CellClickEvent(Alerts alert)
            {
                this.alert = alert;
            }

            public override void OnMouseDown(SourceGrid.CellContext sender, MouseEventArgs e)
            {
                base.OnMouseUp(sender, e);
                if (e.Button == MouseButtons.Left)
                {
                    isClicking = true;
                }
            }

            public override void OnClick(SourceGrid.CellContext sender, EventArgs e)
            {
                base.OnClick(sender, e);
                int row = sender.Position.Row;
                int column = sender.Position.Column;
                if (isClicking)
                {
                    alert.AlertClicked(row, column);
                    isClicking = false;
                }
            }
        }

        class HeaderRightController : SourceGrid.Cells.Controllers.ControllerBase
        {
            ContextMenu menu = new ContextMenu();
            Alerts alerts;
            ColumnInfo[] columnInfos;
            public HeaderRightController(Object obj, ColumnInfo[] columnInfos)
            {
                this.alerts = (Alerts)obj;
                this.columnInfos = columnInfos;
                menu.MenuItems.Add("Menu 1", new EventHandler(Menu1_Click));
                menu.MenuItems.Add("Menu 2", new EventHandler(Menu2_Click));
            }

            public override void OnMouseUp(SourceGrid.CellContext sender, MouseEventArgs e)
            {
                base.OnMouseUp(sender, e);

                switch (e.Button)
                {
                    case MouseButtons.Right:
                        ColumnSelection alertColumnSelection = new ColumnSelection(alerts, this.columnInfos, "alert");
                        alertColumnSelection.Location = alerts.PointToScreen(new Point(e.X, e.Y));
                        alertColumnSelection.ShowDialog();
                        break;
                    case MouseButtons.Left:
                        //int row = sender.Position.Row;
                        //int col = sender.Position.Column;
                        //this.alerts.gridStatus.OrderColumnIndex = col;
                        //this.alerts.gridStatus.OrderColumn = this.alerts.alertGrid[row, col].DisplayText.Trim();
                        //this.alerts.gridStatus.Order = (this.alerts.gridStatus.Order + 1) % 2;
                        break;
                }
            }

            private void Menu1_Click(object sender, EventArgs e)
            {
                //TODO Your code here
            }
            private void Menu2_Click(object sender, EventArgs e)
            {
                //TODO Your code here
            }
        }

        class CellRightController : SourceGrid.Cells.Controllers.ControllerBase
        {
            ContextMenu menu = new ContextMenu();
            Alerts alerts;
            public CellRightController(Object alerts)
            {
                this.alerts = (Alerts)alerts;
                menu.MenuItems.Add("Menu 1", new EventHandler(Menu1_Click));
                menu.MenuItems.Add("Menu 2", new EventHandler(Menu2_Click));
            }

            public override void OnMouseUp(SourceGrid.CellContext sender, MouseEventArgs e)
            {
                base.OnMouseUp(sender, e);
                if (e.Button == MouseButtons.Right)
                {
                }
            }

            private void Menu1_Click(object sender, EventArgs e)
            {
                //TODO Your code here
            }
            private void Menu2_Click(object sender, EventArgs e)
            {
                //TODO Your code here
            }
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

        public class PopupMenu : SourceGrid.Cells.Controllers.ControllerBase
        {
            ContextMenu menu = new ContextMenu();
            Alerts alert;
            SourceGrid.CellContext celContext;
            int x;
            int y;
            public PopupMenu(Alerts alert)
            {
                this.alert = alert;
                menu.MenuItems.Add("Excel 로 내보내기", new EventHandler(Export_Click));
               // menu.MenuItems.Add("Alert 삭제", new EventHandler(Delete_Click));
               // menu.MenuItems.Add("Alert 편집", new EventHandler(Modify_Click));
            }

            public override void OnMouseUp(SourceGrid.CellContext sender, MouseEventArgs e)
            {
                base.OnMouseUp(sender, e);

                if (e.Button == MouseButtons.Right)
                {
                    this.x = e.X;
                    this.y = e.Y;
                    this.celContext = sender;
                    menu.Show(sender.Grid, new Point(e.X, e.Y));
                }
            }

            private void Export_Click(object sender, EventArgs e)
            {
                int row = this.celContext.Position.Row;
                alert.ExportAlert(row);
            }
            private void Delete_Click(object sender, EventArgs e)
            {
                //TODO Your code here
            }

            private void Modify_Click(object sender, EventArgs e)
            {
                //TODO Your code here
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

        private void searchDevice()
        {
            searchOptionIndex = this.searchCombo.SelectedIndex;
            searchKeyword = this.searchTextBox.Text;
            searchOptionStr = (string)this.searchCombo.SelectedItem;
            this.selectedDevicePhoneNumber = -1;
            this.selectedDeviceSessionId = -1;
            this.PrepareAlert();
        }
       
        private void serchBtn_Click(object sender, EventArgs e)
        {
            searchDevice();
        }

        private void searchTextBox_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                searchDevice();
            }
        }
    }
}
