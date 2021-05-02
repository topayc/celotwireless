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
using System.Threading;
using CelotMClient.Worker;
using CelotMClient.Manager;
using CelotMClient.CustomControll;
using System.Diagnostics;
using CelotMClient.Model.NMS;
using CelotMClient.NMSStructure;
using CelotMClient.CDao;
using CelotMClient.Dto;
using CelotMClient.Util;
using System.Resources;

namespace CelotMClient.CustomView
{
    public partial class Device : UserControl
    {
        public ColumnSelection gridSelectionForm;
        public static string[] columnsTitles = new string[]{
                "S","Name","Description","Group", "SerialNo","Phone","Session",
                 "Band","LAN IP","WAN IP","Device Status"
                 ,"VPN","Wifi","SMS","Firmware","Ext Power",
                 "Battery","Signal","주기(분)","NMS version",
                 "Reset Time"
            };
        public ColumnInfo[] columnInfos;

        public NMSAlertManager alertManage = new NMSAlertManager();
        public List<NMSReportCommand> nmsReportCommandList;

        public int selectedDeviceIndex = -1;
        public NMSAlertManager nmsAlertManager = new NMSAlertManager();

        public int selectedDeviceSessionId = -1;
        public int selectedDevicePhoneNumber = -1;
        public NMSReportCommand selectedNMSCommand = null;

        public string searchKeyword = "";
        public int searchOptionIndex = 0;
        public string searchOptionStr = "";

        public int searchTimeBefore = 6;
        public int searchTimeType = Constants.DATETYPE_MONTH;

        public StringBuilder strBuilder = new StringBuilder();
        MapDialog mapDialog;
        bool isBigMapShowing = false;
         //맵 확대 플래그 
        bool mapExtendEnable = true;

        DevAge.Drawing.BorderLine border;
        DevAge.Drawing.RectangleBorder cellBorder;
        CellBackColorAlternate viewNormal;
        CheckBoxBackColorAlternate viewCheckBox;
        SourceGrid.Cells.Views.ColumnHeader viewColumnHeader;
        DevAge.Drawing.VisualElements.ColumnHeader backHeader;
        SourceGrid.Cells.Views.ColumnHeader viewColumnHeader1;
        DevAge.Drawing.VisualElements.ColumnHeader backHeader1;
        ResourceManager rm = CelotMClient.Properties.Resources.ResourceManager;
        Bitmap normalBit;
        Bitmap abnormalBit;
        Bitmap alertBit;
        Bitmap unknownBit;

        public Device()
        {
            InitializeComponent();
            this.selectedDevicePhoneNumber = -1;
            this.selectedDeviceSessionId = -1;
            this.selectedDeviceIndex = -1;

            initColumnInfo(columnsTitles);
            //그리드와 관련한 필요 object 생성
            //Border
            border = new DevAge.Drawing.BorderLine(Color.Black, 1);
            cellBorder = new DevAge.Drawing.RectangleBorder(border, border);
            
            //Views
            viewNormal = new CellBackColorAlternate(Color.Khaki, Color.DarkKhaki);
            viewNormal.Border = cellBorder;
            viewNormal.TextAlignment = DevAge.Drawing.ContentAlignment.MiddleCenter;
            viewNormal.Font = new Font("", 8, FontStyle.Regular);
            viewCheckBox = new CheckBoxBackColorAlternate(Color.Khaki, Color.DarkKhaki);
            viewCheckBox.Border = cellBorder;

            //ColumnHeader view
            viewColumnHeader = new SourceGrid.Cells.Views.ColumnHeader();
            backHeader = new DevAge.Drawing.VisualElements.ColumnHeader();
            backHeader.BackColor = Color.DimGray;
            //backHeader.Border = DevAge.Drawing.RectangleBorder.NoBorder;
            viewColumnHeader.Background = backHeader;
            viewColumnHeader.ForeColor = Color.White;
            viewColumnHeader.Font = new Font("돋움", 10, FontStyle.Bold);

            viewColumnHeader1 = new SourceGrid.Cells.Views.ColumnHeader();
            backHeader1 = new DevAge.Drawing.VisualElements.ColumnHeader();
            backHeader1.BackColor = Color.DarkSlateGray;
            viewColumnHeader1.Background = backHeader1;
            //viewColumnHeader1.Border = cellBorder; 
            viewColumnHeader1.ForeColor = Color.White;
            viewColumnHeader1.Font = new Font("맑은 고딕", 8, FontStyle.Regular);
            viewColumnHeader1.TextAlignment = DevAge.Drawing.ContentAlignment.MiddleCenter;

            this.searchCombo.SelectedIndex = this.searchOptionIndex;
            this.searchTextBox.Text = this.searchKeyword;
            new HeaderRightController(this, this.columnInfos);
            normalBit = (Bitmap)rm.GetObject("normal");
            abnormalBit= (Bitmap)rm.GetObject("abnormal");
            alertBit = (Bitmap)rm.GetObject("alert");
            unknownBit = (Bitmap)rm.GetObject("unknown");

        }

        private void Device_Load(object sender, EventArgs e)
        {
            this.Visible = false;
            this.initChart();
            this.PrepareDevice();
            ApplicationCache.Instance().NMSDataNotify += new NMSDataNotiHandler(this.Router_NMSDataNotify);
        }

        private void Router_NMSDataNotify(object sender, NMSDataNotifyEventArgs args)
        {
            this.Invoke(new Action(delegate()
            {
                //Stopwatch sw = new Stopwatch();
                //sw.Start();

                NMSDataWrapper nmsDataWrapper = args.Data;
                List<NMSReportCommand> reportList = nmsDataWrapper.NMSReportCommandList;

                this.deviceGrid.SuspendLayout();
                this.InitDeviceGridColumn();
                if (reportList.Count > 0)
                {
                   
                    this.nmsReportCommandList = reportList;

                    if (!String.IsNullOrEmpty(this.searchKeyword))
                    {
                        this.FilterDeviceBySearchkeyword();
                    }

                    this.setDeviceColumnShowSetting();
                    this.setDeviceGrid(nmsReportCommandList);

                    NMSReportCommand tmpCommand = null;
                    if (this.selectedDevicePhoneNumber == -1 && this.selectedDeviceSessionId == -1)
                    {
                        if (this.nmsReportCommandList.Count > 0)
                        {
                            tmpCommand = nmsReportCommandList[0];
                            this.selectedDeviceIndex = 1;
                        }
                    }
                    else
                    {
                        if (this.selectedDevicePhoneNumber != -1)
                        {
                            tmpCommand = nmsReportCommandList.Find(
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
                            tmpCommand = nmsReportCommandList.Find(
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
                    if (this.nmsReportCommandList.Count > 0)
                    {
                        if (tmpCommand != null)
                        {
                            this.selectedDevicePhoneNumber = tmpCommand.Device != null ? tmpCommand.Device.PhoneNumber : -1;
                            this.selectedDeviceSessionId = tmpCommand.nms_reprot_t != null ? (int)tmpCommand.nms_reprot_t.header.session_id : -1;
                            this.ShowDeviceDetailData(tmpCommand);
                        }
                    }
                    this.deviceGrid.Selection.SelectRow(this.selectedDeviceIndex, true);
                    
                }
                this.deviceGrid.ResumeLayout(true);
                this.Visible = true;
                nmsDataWrapper = null;
                reportList = null;
                
               //sw.Stop();
               //MessageBox.Show(sw.ElapsedMilliseconds.ToString() + "ms");
            }));
        }

        private void initChart()
        {
            trafficLineChart.BackColor = Color.DarkSlateGray;
            trafficLineChart.ChartAreas[0].BackColor = Color.DarkSlateGray;

            rssiLineChart.BackColor = Color.DarkSlateGray;
            rssiLineChart.ChartAreas[0].BackColor = Color.DarkSlateGray;
        }

        public void initColumnInfo(string[] columnTitles)
        {
            columnInfos = new ColumnInfo[columnsTitles.Length];
            for (int i = 0; i < columnsTitles.Length; i++)
            {
                columnInfos[i] = new ColumnInfo(columnsTitles[i], true);
            }
        }

        private void PrepareDevice()
        {
            if (ApplicationCache.Instance().CurNMSDataWrapper == null) return;
            NMSDataWrapper nmsDataWrapper = ApplicationCache.Instance().CurNMSDataWrapper;
            List<NMSReportCommand> reportList = nmsDataWrapper.NMSReportCommandList;
            this.InitDeviceGridColumn();
            if (reportList.Count > 0)
            {
                this.SuspendLayout();
                nmsReportCommandList = reportList;

                if (!String.IsNullOrEmpty(this.searchKeyword))
                {
                    this.FilterDeviceBySearchkeyword();
                }

                this.setDeviceColumnShowSetting();
                this.setDeviceGrid(nmsReportCommandList);

                NMSReportCommand tmpCommand = null;
                if (this.selectedDevicePhoneNumber == -1 && this.selectedDeviceSessionId == -1)
                {
                    if (this.nmsReportCommandList.Count > 0)
                    {
                        tmpCommand = nmsReportCommandList[0];
                        this.selectedDeviceIndex = 1;
                    }
                }
                else
                {
                    if (this.selectedDevicePhoneNumber != -1)
                    {
                        tmpCommand = nmsReportCommandList.Find(
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
                        tmpCommand = nmsReportCommandList.Find(
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
                if (this.nmsReportCommandList.Count > 0)
                {
                    if (tmpCommand != null)
                    {
                        this.selectedDevicePhoneNumber = tmpCommand.Device != null ? tmpCommand.Device.PhoneNumber : -1;
                        this.selectedDeviceSessionId = tmpCommand.nms_reprot_t != null ? (int)tmpCommand.nms_reprot_t.header.session_id : -1;
                        this.ShowDeviceDetailData(tmpCommand);
                    }
                }
                this.deviceGrid.Selection.SelectRow(this.selectedDeviceIndex, true);
          
                this.ResumeLayout(true);
            }
            this.Visible = true;
            nmsDataWrapper = null;
            reportList = null;
        }

        private void FilterDeviceBySearchkeyword()  
        {
            //MessageBox.Show(String.Format("{0} :{1} : {2}", this.searchOptionStr, this.searchKeyword,this.nmsReportCommandList.Count));
            switch (this.searchOptionStr)
            {
                case "Name":
                    var result =
                        this.nmsReportCommandList.Where(
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
                    this.nmsReportCommandList = commandList;
                    break;
                case "Phone Number":
                    var result1 =
                       this.nmsReportCommandList.Where(
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
                    this.nmsReportCommandList = commandList1;
                    break;

                case "Router(LAN) IP":
                    var result2 =
                       this.nmsReportCommandList.Where(
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
                    this.nmsReportCommandList = commandList2;
                    break;

                case "WAN IP":
                    var result3 =
                        this.nmsReportCommandList.Where(
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
                    this.nmsReportCommandList = commandList3;
                    break;
            }
        }

        private void setDeviceColumnShowSetting()
        {
            Dictionary<String, int> deviceSettingMap = ApplicationConfig.Instance().DeviceColumnSetting;
            for (int i = 0; i < columnsTitles.Length; i++)
            {
                string columnName = columnsTitles[i];
                this.deviceGrid.Columns[i].Visible = deviceSettingMap[columnName] == 1 ? true : false;
            }
        }

        private void setDeviceGrid(List<NMSReportCommand> nmsReportCommandList)
        {
            foreach (NMSReportCommand nmsReportCommand in nmsReportCommandList)
            {
                this.addRowToDeviceGrid(nmsReportCommand);
            }
         
            this.deviceGrid.AutoSizeCells();
        }


        private void InitDeviceGridColumn()
        {
            this.deviceGrid.Rows.Clear();
            HeaderRightController headerController = new HeaderRightController(this, this.columnInfos);
            this.deviceGrid.ColumnsCount = this.columnInfos.Length;
            this.deviceGrid.FixedRows = 1;
            SourceGrid.Cells.ColumnHeader columnHeader;
            this.deviceGrid.Rows.Insert(0);
            string cellText = "";
            for (int i = 0; i < this.columnInfos.Length; i++)
            {
                cellText = columnInfos[i].ColumnName;
                if (columnInfos[i].ColumnName.Equals("Band"))
                {
                    cellText = columnInfos[i].ColumnName + " Type";
                }

                columnHeader = new SourceGrid.Cells.ColumnHeader(cellText);
                columnHeader.View = viewColumnHeader1;
                this.deviceGrid[0, i] = columnHeader;
                this.deviceGrid[0, i].AddController(headerController);
            }
        }

        private void addRowToDeviceGrid(NMSReportCommand nmsReportCommand)
        {
            int insertRowCount = this.deviceGrid.Rows.Count;
            this.deviceGrid.Rows.Insert(insertRowCount);

            CellClickEvent clickController = new CellClickEvent(this);
            CellRightController cellrightController = new CellRightController(this);

            SourceGrid.Cells.Views.ColumnHeader nameHeaderView1 = new SourceGrid.Cells.Views.ColumnHeader();
            DevAge.Drawing.VisualElements.ColumnHeader namebackHeader1 = new DevAge.Drawing.VisualElements.ColumnHeader();
            namebackHeader1.BackColor = Color.DarkSlateGray;
            nameHeaderView1.Background = namebackHeader1;
            nameHeaderView1.Border = cellBorder;
            nameHeaderView1.ForeColor = Color.White;
            nameHeaderView1.Font = new Font("맑은 고딕", 8, FontStyle.Regular);
            nameHeaderView1.TextAlignment = DevAge.Drawing.ContentAlignment.MiddleCenter;

            CelotMClient.Model.Device device = nmsReportCommand.Device;
            nms_reprot_t nms = nmsReportCommand.nms_reprot_t;

            SourceGrid.Cells.Controllers.ToolTipText toolTipController = new SourceGrid.Cells.Controllers.ToolTipText();
            toolTipController.ToolTipTitle = "ROUTER STATUS ";

            Bitmap statusBitMap = null;
            switch (nmsReportCommand.DeviceAlertCassification)
            {
                case DeviceAlertCassification.NORMAL:
                    statusBitMap = normalBit;
                    strBuilder.AppendLine(String.Format("Status : {0}", DeviceAlertCassification.NORMAL.Parse().Trim()));
                    toolTipController.ToolTipIcon = ToolTipIcon.Info;
                    break;
                case DeviceAlertCassification.ABNORMAL:
                    statusBitMap = abnormalBit;
                    strBuilder.AppendLine(String.Format("Status : {0}\nDetail : {1}", DeviceAlertCassification.ABNORMAL.Parse(), nmsReportCommand.GetAlertMessages().Trim()));
                    toolTipController.ToolTipIcon = ToolTipIcon.Error;
                    break;
                case DeviceAlertCassification.ALERT:
                    statusBitMap = alertBit;
                    strBuilder.AppendLine(String.Format("Status : {0}\nDetail : {1}", DeviceAlertCassification.ALERT.Parse(), nmsReportCommand.GetAlertMessages().Trim()));
                    toolTipController.ToolTipIcon = ToolTipIcon.Warning;
                    break;
                case DeviceAlertCassification.UNKNOWN:
                    statusBitMap = unknownBit;
                    strBuilder.AppendLine(String.Format("Status : {0}\nDetail : {1}", DeviceAlertCassification.UNKNOWN.Parse(), nmsReportCommand.GetAlertMessages().Trim()));
                    toolTipController.ToolTipIcon = ToolTipIcon.None;
                    break;
            }
             
            if ( nmsReportCommand.nms_reprot_t != null )
            {
                strBuilder.AppendLine(String.Format(
                    "Time : {0}", 
                    DateTime.ParseExact(nmsReportCommand.nms_reprot_t.data.current_time, "yyMMddHHmmss", null).ToString("yy-MM-dd HH:mm:ss")));
            }
            else
            {
                strBuilder.AppendLine(String.Format(
                    "Time : {0}",
                    CelotUtility.UnixTimeStampToDateTime(nmsReportCommand.Device.DeviceRegDate).ToString("yy-MM-dd HH:mm:ss")));
            }
            
            deviceGrid[insertRowCount, 0] = new SourceGrid.Cells.Image(statusBitMap);
            deviceGrid[insertRowCount, 0].Editor.EnableEdit = false;   // 해당 셀의 에디터 기능을 비활성화 
            deviceGrid[insertRowCount, 0].View = viewNormal;
            deviceGrid[insertRowCount, 0].ToolTipText = strBuilder.ToString();
            deviceGrid[insertRowCount, 0].AddController(toolTipController);
            
            strBuilder.Clear();

            deviceGrid[insertRowCount, 1] = new SourceGrid.Cells.Cell(device == null ? "" : device.Name);
            deviceGrid[insertRowCount, 1].AddController(cellrightController);
            deviceGrid[insertRowCount, 1].AddController(clickController);
            deviceGrid[insertRowCount, 1].View = viewNormal;

            deviceGrid[insertRowCount, 2] = new SourceGrid.Cells.Cell(device == null ? "" : device.Des);
            deviceGrid[insertRowCount, 2].AddController(cellrightController);
            deviceGrid[insertRowCount, 2].AddController(clickController);
            deviceGrid[insertRowCount, 2].View = viewNormal;

            deviceGrid[insertRowCount, 3] = new SourceGrid.Cells.Cell(device == null ? "" : device.GroupName);
            deviceGrid[insertRowCount, 3].AddController(cellrightController);
            deviceGrid[insertRowCount, 3].AddController(clickController);
            deviceGrid[insertRowCount, 3].View = viewNormal;

            deviceGrid[insertRowCount, 4] = new SourceGrid.Cells.Cell(device == null ? "" : device.SerialNo.ToString());
            deviceGrid[insertRowCount, 4].AddController(cellrightController);
            deviceGrid[insertRowCount, 4].AddController(clickController);
            deviceGrid[insertRowCount, 4].View = viewNormal;

            deviceGrid[insertRowCount, 5] = new SourceGrid.Cells.Cell(device == null ? "" : CelotUtility.ChangePhoneNumberToFormatString(device.PhoneNumber));
            deviceGrid[insertRowCount, 5].AddController(cellrightController);
            deviceGrid[insertRowCount, 5].AddController(clickController);
            deviceGrid[insertRowCount, 5].View = viewNormal;

            deviceGrid[insertRowCount, 6] = new SourceGrid.Cells.Cell(nms == null ? "" : nms.header.session_id.ToString());
            deviceGrid[insertRowCount, 6].AddController(cellrightController);
            deviceGrid[insertRowCount, 6].AddController(clickController);
            deviceGrid[insertRowCount, 6].View = viewNormal;

            deviceGrid[insertRowCount, 7] = new SourceGrid.Cells.Cell(nms == null ? "" : ((ModuleBand)nms.data.moduleband).Parse());
            deviceGrid[insertRowCount, 7].AddController(cellrightController);
            deviceGrid[insertRowCount, 7].AddController(clickController);
            deviceGrid[insertRowCount, 7].View = viewNormal;

            deviceGrid[insertRowCount, 8] = new SourceGrid.Cells.Cell(nms == null ? "" : nmsReportCommand.GetLanIPString());
            deviceGrid[insertRowCount, 8].AddController(cellrightController);
            deviceGrid[insertRowCount, 8].AddController(clickController);
            deviceGrid[insertRowCount, 8].View = viewNormal;

            deviceGrid[insertRowCount, 9] = new SourceGrid.Cells.Cell(nms == null ? "" : nmsReportCommand.GetWanIPString());
            deviceGrid[insertRowCount, 9].AddController(cellrightController);
            deviceGrid[insertRowCount, 9].AddController(clickController);
            deviceGrid[insertRowCount, 9].View = viewNormal;

            deviceGrid[insertRowCount, 10] = new SourceGrid.Cells.Cell(nms == null ? "" : ((DeviceStatus)nms.data.devicestatus).Parse());
            deviceGrid[insertRowCount, 10].AddController(cellrightController);
            deviceGrid[insertRowCount, 10].AddController(clickController);
            deviceGrid[insertRowCount, 10].View = viewNormal;

            deviceGrid[insertRowCount, 11] = new SourceGrid.Cells.Cell(nms == null ? "" : ((VpnStatus)nms.data.vpnstatus).Parse());
            deviceGrid[insertRowCount, 11].AddController(cellrightController);
            deviceGrid[insertRowCount, 11].AddController(clickController);
            deviceGrid[insertRowCount, 11].View = viewNormal;

            deviceGrid[insertRowCount, 12] = new SourceGrid.Cells.Cell(nms == null ? "" : ((WifiStatus)nms.data.wifistatus).Parse());
            deviceGrid[insertRowCount, 12].AddController(cellrightController);
            deviceGrid[insertRowCount, 12].AddController(clickController);
            deviceGrid[insertRowCount, 12].View = viewNormal;
            
            string smsStatus = "";
            if (nms != null)
            {
                smsStatus = nms.data.newsms < 0 ? "N/A" : nms.data.newsms.ToString();
            }

            deviceGrid[insertRowCount, 13] = new SourceGrid.Cells.Cell(smsStatus);
            deviceGrid[insertRowCount, 13].AddController(cellrightController);
            deviceGrid[insertRowCount, 13].AddController(clickController);
            deviceGrid[insertRowCount, 13].View = viewNormal;

            deviceGrid[insertRowCount, 14] = new SourceGrid.Cells.Cell(nms == null ? "" : nms.data.sw_version);
            deviceGrid[insertRowCount, 14].AddController(cellrightController);
            deviceGrid[insertRowCount, 14].AddController(clickController);
            deviceGrid[insertRowCount, 14].View = viewNormal;

            string extPower = "";
            if (nms != null)
            {
                extPower  = ((ExternalPower)nms.data.external_power).Parse();
            }
            deviceGrid[insertRowCount, 15] = new SourceGrid.Cells.Cell(extPower);
            deviceGrid[insertRowCount, 15].AddController(cellrightController);
            deviceGrid[insertRowCount, 15].AddController(clickController);
            deviceGrid[insertRowCount, 15].View = viewNormal;

            string batterStatus = "";
            if (nms!= null)
            {
                batterStatus = nms.data.ext_device1[0].ToString();
                
            }
            deviceGrid[insertRowCount, 16] = new SourceGrid.Cells.Cell(batterStatus);
            deviceGrid[insertRowCount, 16].AddController(cellrightController);
            deviceGrid[insertRowCount, 16].AddController(clickController);
            deviceGrid[insertRowCount, 16].View = viewNormal;

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
            deviceGrid[insertRowCount, 17] = signalCell;
            deviceGrid[insertRowCount, 17].AddController(cellrightController);
            deviceGrid[insertRowCount, 17].AddController(clickController);
            deviceGrid[insertRowCount, 17].View = viewNormal;
            SourceGrid.LinkedControlValue linkedControlValue = new SourceGrid.LinkedControlValue(signal, new SourceGrid.Position(insertRowCount, 17));
            deviceGrid.LinkedControls.Add(linkedControlValue);

            deviceGrid[insertRowCount, 18] = new SourceGrid.Cells.Cell(nms == null ? "" : nms.data.rpt_time.ToString());
            deviceGrid[insertRowCount, 18].AddController(cellrightController);
            deviceGrid[insertRowCount, 18].AddController(clickController);
            deviceGrid[insertRowCount, 18].View = viewNormal;

            deviceGrid[insertRowCount, 19] = new SourceGrid.Cells.Cell(nms == null ? "" : nms.header.pro_ver.ToString());
            deviceGrid[insertRowCount, 19].AddController(cellrightController);
            deviceGrid[insertRowCount, 19].AddController(clickController);
            //deviceGrid[insertRowCount, 19].View = viewNormal; ;
            deviceGrid[insertRowCount, 19].View = nameHeaderView1;

            string currentTime = "";
            if (device != null && device.ResetTime != 0)
            {
                currentTime = CelotUtility.UnixTimeStampToDateString(device.ResetTime);
            }
            deviceGrid[insertRowCount, 20] = new SourceGrid.Cells.Cell(currentTime);
            deviceGrid[insertRowCount, 20].AddController(cellrightController);
            deviceGrid[insertRowCount, 20].AddController(clickController);
            deviceGrid[insertRowCount, 20].View = viewNormal;
        }

        private void initRssiLineChart(List<DeviceChartDto> list)
        {
            rssiLineChart.DataSource = null;
            rssiLineChart.DataSource = list;
        }

        private void initTrafficLinechart(List<DeviceChartDto> list)
        {
            trafficLineChart.DataSource = null;
                trafficLineChart.DataSource = list;
        }

        private void ShowDeviceDetailData(NMSReportCommand nmsReportCommand)
        {
            this.SetSelectedDeviceDetailData(nmsReportCommand);
          
            if (nmsReportCommand.nms_reprot_t != null)
            {
                Debug.WriteLine(String.Format("###### 선택한 라우터 번호 : {0}", nmsReportCommand.nms_reprot_t.header.session_id));
                int session_id = (int)nmsReportCommand.nms_reprot_t.header.session_id;
                DateTime date = DateTime.Now.ToLocalTime();

                if (this.searchTimeType == Constants.DATETYPE_MONTH)
                {
                    date = date.AddMonths(-this.searchTimeBefore);
                }
                else if (this.searchTimeType == Constants.DATETYPE_DAY)
                {
                    date = date.AddDays(-this.searchTimeBefore);
                }

                var span = (date - new DateTime(1970, 1, 1, 0, 0, 0, 0).ToLocalTime());
                int timestamp = (int)span.TotalSeconds;
                this.GetChartData(session_id, timestamp);
                Debug.WriteLine(String.Format("요청 날짜 {0}", date));
            }
            else
            {
                List<DeviceChartDto> chartDtoList = new List<DeviceChartDto>();
                this.initChart();
                this.initRssiLineChart(chartDtoList);
                this.initTrafficLinechart(chartDtoList);
            }
        }

        public void DeviceSelected(int row, int col)
        {
            if (this.isBigMapShowing)
            {
                this.closeBigMap();
            }
            this.selectedDeviceIndex = row;
            NMSReportCommand nmsReportCommand = this.nmsReportCommandList[row - 1];
            this.selectedDevicePhoneNumber = nmsReportCommand.Device != null ? nmsReportCommand.Device.PhoneNumber : -1;
            this.selectedDeviceSessionId = nmsReportCommand.nms_reprot_t != null ? (int)nmsReportCommand.nms_reprot_t.header.session_id : -1;
            this.ShowDeviceDetailData(nmsReportCommand);
        }

        public void SetSelectedDeviceDetailData(NMSReportCommand nmsReportCommand)
        {
            this.selectedNMSCommand = nmsReportCommand;
            CelotMClient.Model.Device device = nmsReportCommand.Device;
            nms_reprot_t nms = nmsReportCommand.nms_reprot_t;

            nameLabel.Text = device == null ? "" : device.Name;
            desLabel.Text = device == null ? "" : device.Des;
            phoneLabel.Text = device == null ? "" : CelotUtility.ChangePhoneNumberToFormatString(device.PhoneNumber);
            serialLabel.Text = device == null ? "" : device.SerialNo.ToString();
            nmsLabel.Text = nms == null ? "" : nms.header.pro_ver.ToString();
            vpnLabel.Text = nms == null ? "" : ((VpnStatus)nms.data.vpnstatus).Parse();
            bandLabel.Text = nms == null ? "" : ((ModuleBand)nms.data.moduleband).Parse();
            rssiLabel.Text = nms == null ? "" : ((RssiLevel)nmsAlertManager.getRssiLevel(nms.data.modulesignal)).Parse() + " (" + nms.data.modulesignal + ") ";
            trafficLabel.Text = nms == null ? "" : String.Format("Tx : {0} Bytes , Rx : {1} Bytes", nms.data.use_tx_amount, nms.data.use_rx_amount);
            wanLabel.Text = nms == null ? "" : nmsReportCommand.GetWanIPString();
            lanLabel.Text = nms == null ? "" : nmsReportCommand.GetLanIPString();
            lastConLabel.Text = nms == null ? "" : CelotUtility.ChangeStampStringToLocalFormat(nms.data.current_time);

            wifiLabel.Text = nms == null ? "" : ((WifiStatus)nms.data.wifistatus).Parse();
            smsLabel.Text = nms == null ? "" : (nms.data.newsms > 0 ? nms.data.newsms.ToString() : "N/A");
            battteryLabel.Text = nms == null ? "" : nms.data.ext_device1[0].ToString();
            coodLabel.Text = device == null ? "" : String.Format("lat : {0},  lng : {1}", Convert.ToString(device.Latitude), Convert.ToString(device.Longitude));
            nmsPeriodLabel.Text = nms == null ? "" : nms.data.rpt_time.ToString();
            firmLabel.Text = nms == null ? "" : nms.data.sw_version;

            if (nmsReportCommand.Device != null)
            {
                if (CelotUtility.IsConnectedToInternet())
                {
                    string latitude = nmsReportCommand.Device.Latitude;
                    string longitude = nmsReportCommand.Device.Longitude;
                    String url = String.Format("http://maps.googleapis.com/maps/api/staticmap?center={0},{1}&size={2}x{3}&visible={4},{5}&markers=color:blue%7C{6},{7}&sensor=false",
                        latitude, longitude, 700, 700,latitude, longitude, latitude, longitude);
                   //맵 자동 갱신 , 일시 주석 처리함 
                    //if (mapExtendEnable)
                    //{
                    //    if (this.mapDialog != null)
                    //    {
                    //        this.pictureBoxMap.LoadCompleted -= (sender, e) =>
                    //        {
                    //            this.mapDialog.mapBigPic.Image = this.pictureBoxMap.Image;
                    //        };

                    //        this.pictureBoxMap.LoadCompleted += (sender, e) =>
                    //        {
                    //            this.pictureBoxMap.Visible = true;
                    //            if (this.mapDialog !=null) this.mapDialog.mapBigPic.Image = this.pictureBoxMap.Image;
                    //        };
                    //    }
                    //}

                    //this.pictureBoxMap.LoadCompleted += (sender, e) =>
                    //{
                    //    this.pictureBoxMap.Visible = true;
                    //};
                    this.pictureBoxMap.LoadAsync(url);
                    this.pictureBoxMap.Visible = true;
                }
                else
                {
                    this.pictureBoxMap.Image = Properties.Resources.web_init_image;
                    this.pictureBoxMap.Visible = true;
                }
            }
            else
            {
                this.pictureBoxMap.Image = null;
                this.pictureBoxMap.Visible = true;
                //this.pictureBoxMap.Visible = false;
            }
        }

        public void GetChartData(int session_id, int startTime)
        {
            CCloudDao cloudDao = new CCloudDao(false);
            cloudDao.NotifyDataBaseFinished += new NotifyDataBaseFinishedHandler(this.DeviceChartData_Recevied);
            cloudDao.GetPackektLogs(session_id, startTime);
        }

        private void DeviceChartData_Recevied(object Sender, DataBaseFinishedEventArgs e)
        {
            List<DeviceChartDto> chartDtoList = (List<DeviceChartDto>)e.Result;

            if (!this.IsDisposed)
            {
                this.initChart();
                this.initRssiLineChart(chartDtoList);
                this.initTrafficLinechart(chartDtoList);
            }
        }

        private void periodPicture_Click(object sender, EventArgs e)
        {
            PeriodSelectForm f = new PeriodSelectForm();
            f.Location = new Point(Control.MousePosition.X - f.Size.Width - 10, Control.MousePosition.Y + 10);
            if (f.ShowDialog() == DialogResult.OK)
            {
                if (this.selectedDeviceIndex == -1) return;

                NMSReportCommand nmsReportCommand = this.nmsReportCommandList[this.selectedDeviceIndex-1];
                if (nmsReportCommand.nms_reprot_t == null)
                {
                    this.SetSelectedDeviceDetailData(nmsReportCommand);
                    // MessageBox.Show("해당 디바이스는 No NMS Report 상태로, 조회할 수 있는 정보가 없습니다");
                    return;
                }
                int session_id = (int)nmsReportCommand.nms_reprot_t.header.session_id;
                int startTime = 0;

                DateTime date = DateTime.Now.ToLocalTime();
                if (f.DateType == Constants.DATETYPE_DAY)
                {
                    date = date.AddDays(-f.Period);
                  
                    System.Diagnostics.Debug.WriteLine(String.Format(" 현재 DateTime {0}", startTime));
                }

                if (f.DateType == Constants.DATETYPE_MONTH)
                {
                    date = date.AddMonths(-f.Period);
                    System.Diagnostics.Debug.WriteLine(String.Format(" 현재 DateTime {0}", startTime));
                }
               
                var span = (date - new DateTime(1970, 1, 1, 0, 0, 0, 0).ToLocalTime());
               
                int timestamp = (int)span.TotalSeconds;
                this.searchTimeBefore = f.Period;
                this.searchTimeType = f.DateType;

                this.GetChartData(session_id, timestamp);
            }
        }

        public void CreateDevice()
        {
            DeviceInput dForm = new DeviceInput("디바이스 생성", Constants.CREATE, null);
            if (dForm.ShowDialog() == DialogResult.OK)
            {
                bool result = true;
                string message = ApplicationCache.Instance().CheckRegisterdDevice(dForm.Device, out result);
                if (!result)
                {
                    MessageBox.Show(message);
                    return;
                }

                CCloudDao dao = new CCloudDao();
                dao.NotifyDataBaseFinished += new NotifyDataBaseFinishedHandler(this.device_created);
                dao.CreateDevice(dForm.Device);
            }
        }

        private void device_created(object Sender, DataBaseFinishedEventArgs e)
        {
            try
            {
                //if (!ServiceManager.Instance().ControlService(ServiceManager.CONTROL_SERVICE_RELOAD_DEVICE))
                //{
                //    MessageBox.Show(ServiceManager.Instance().Message);
                //}
                ApplicationCache.Instance().LoadDevices();
                ApplicationCache.Instance().LoadNMSReportCommandList();
                PrepareDevice();
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.Message + "\n" + ee.StackTrace);
            }
        }

        public void ModifyDevice(int row)
        {
            NMSReportCommand nmsReportCommand = this.nmsReportCommandList[row - 1];
            if (nmsReportCommand.Device == null)
            {
                MessageBox.Show("해당 항목은 디바이스 정보가 없어서 수정할 수 없습니다");
                return;
            }

            CelotMClient.Model.Device device = new CelotMClient.Model.Device();
            device.DeviceNo = nmsReportCommand.Device.DeviceNo;
            device.Name = nmsReportCommand.Device.Name;
            device.Latitude = nmsReportCommand.Device.Latitude;
            device.Des = nmsReportCommand.Device.Des;
            device.Longitude = nmsReportCommand.Device.Longitude;
            device.PhoneNumber = nmsReportCommand.Device.PhoneNumber;
            device.GroupName = nmsReportCommand.Device.GroupName;
            device.RouterIp = nmsReportCommand.Device.RouterIp;
            device.SerialNo = nmsReportCommand.Device.SerialNo;
            device.SecuCode = nmsReportCommand.Device.SecuCode;
            device.SmsSupport = nmsReportCommand.Device.SmsSupport;
            device.VpnSupport = nmsReportCommand.Device.VpnSupport;
            device.WifiSupport = nmsReportCommand.Device.WifiSupport;
            device.BatterySupport = nmsReportCommand.Device.BatterySupport;

            int oriPhoneNumber = device.PhoneNumber;
            DeviceInput dForm = new DeviceInput("디바이스 수정", Constants.MODIFY, device);
            if (dForm.ShowDialog() == DialogResult.OK)
            {
                bool result = true;
                StringBuilder strBuilder = new StringBuilder();
                List<CelotMClient.Model.Device> registertedDeviceList = ApplicationCache.Instance().Devices;
                var query1 = registertedDeviceList.Where(rd => rd.RouterIp.Equals(dForm.Device.RouterIp));
                var query2 = registertedDeviceList.Where(rd => rd.SerialNo == dForm.Device.SerialNo);
                var query3 = registertedDeviceList.Where(rd => rd.PhoneNumber == dForm.Device.PhoneNumber);

                if (query1.Count() == 0 && query2.Count() == 0 && query3.Count() == 0)
                {
                    result = true;
                }
                else
                {
                    if (query1.Count() < 2 && query3.Count() < 2 && query3.Count() < 2)
                    {
                        if (query1.Count() == 1 && query1.First().DeviceNo != device.DeviceNo)
                        {
                            strBuilder.AppendLine(String.Format("ERROR : Already Registered IP ({0})", device.RouterIp));
                            result = false;
                        }

                        if (query2.Count() == 1 && query2.First().DeviceNo != device.DeviceNo)
                        {
                            strBuilder.AppendLine(String.Format("ERROR : Already Registered Serial No ({0})", device.SerialNo));
                            result = false;
                        }

                        if (query3.Count() == 1 && query3.First().DeviceNo != device.DeviceNo)
                        {
                            strBuilder.AppendLine(String.Format("ERROR : Already Registered Phone Number ({0})", device.PhoneNumber));
                            result = false;
                        }
                    }
                    else
                    {
                        strBuilder.AppendLine(String.Format("ERROR : Already Device info"));
                        result = false;
                    }
                }

                if (!result)
                {
                    MessageBox.Show(strBuilder.ToString());
                    return;
                }
                CCloudDao dao = new CCloudDao();
                dao.NotifyDataBaseFinished += new NotifyDataBaseFinishedHandler(device_modified);
                dao.ModifyDevice(oriPhoneNumber, dForm.Device);
            }
        }

        private void device_modified(object Sender, DataBaseFinishedEventArgs e)
        {
            try
            {
                //if (!ServiceManager.Instance().ControlService(ServiceManager.CONTROL_SERVICE_RELOAD_DEVICE))
                //{
                //    MessageBox.Show(ServiceManager.Instance().Message);
                //}
                ApplicationCache.Instance().LoadDevices();
                ApplicationCache.Instance().LoadNMSReportCommandList();
                PrepareDevice();
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.Message + "\n" + ee.StackTrace);
            }
        }

        public void DeleteDevice(int row)
        {
            NMSReportCommand nmsReportCommand = this.nmsReportCommandList[row - 1];
            if (nmsReportCommand.Device == null)
            {
                MessageBox.Show("해당 항목은 디바이스 정보가 없어서 삭제할 수 없습니다");
                return;
            }

            CCloudDao cDao = new CCloudDao();
            cDao.NotifyDataBaseFinished += new NotifyDataBaseFinishedHandler(device_deleted);
            cDao.DeleteDevice(nmsReportCommand.Device.PhoneNumber);
        }

        private void device_deleted(object Sender, DataBaseFinishedEventArgs e)
        {
            try
            {
                //if (!ServiceManager.Instance().ControlService(ServiceManager.CONTROL_SERVICE_RELOAD_DEVICE))
                //{
                //    MessageBox.Show(ServiceManager.Instance().Message);
                //}
                ApplicationCache.Instance().LoadDevices();
                ApplicationCache.Instance().LoadNMSReportCommandList();
                PrepareDevice();
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.Message + "\n" + ee.StackTrace);
            }
        }


        internal void CommandClick(int row, int x, int y)
        {
            NMSReportCommand nmsReportCommand = this.nmsReportCommandList[row - 1];
            if (nmsReportCommand.Device == null)
            {
                MessageBox.Show("등록되지 않은 디바이스입니다. ");
                return;
            }
            CelotMClient.Model.Device device = nmsReportCommand.Device;
            nms_reprot_t nms = nmsReportCommand.nms_reprot_t;

            RouterCommand routerCommandForm = new RouterCommand(nmsReportCommand);
            routerCommandForm.Location = PointToScreen(new Point(x, y));
            routerCommandForm.ShowDialog();
        }

        public class CellClickEvent : SourceGrid.Cells.Controllers.ControllerBase
        {
            Device device;
            bool isClicking = false;
            public CellClickEvent(Device device)
            {
                this.device = device;
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
                    device.DeviceSelected(row, column);
                    isClicking = false;
                }
            }
        }

        class HeaderRightController : SourceGrid.Cells.Controllers.ControllerBase
        {
            ContextMenu menu = new ContextMenu();
            Device device;
            ColumnInfo[] columnInfos;
            public HeaderRightController(Device device, ColumnInfo[] columnInfos)
            {
                this.device = device;
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
                        ColumnSelection deviceColumnSelection = new ColumnSelection(device, columnInfos, "device");
                        deviceColumnSelection.Location = device.PointToScreen(new Point(e.X, e.Y));
                        deviceColumnSelection.ShowDialog();
                        break;
                    case MouseButtons.Left:
                        //int row = sender.Position.Row;
                        //int col = sender.Position.Column;
                        //this.device.gridStatus.OrderColumnIndex = col;
                        //this.device.gridStatus.OrderColumn = this.device.deviceGrid[row, col].DisplayText.Trim();
                        //this.device.gridStatus.Order = (this.device.gridStatus.Order + 1) % 2;

                      
                        //this.device.selectedDeviceSessionId = -1;
                        //this.device.selectedDevicePhoneNumber = -1;
                        //this.device.deviceGrid.Selection.ResetSelection(true);
                        //this.device.PrepareDevice();
                        break;
                      
                      
                }
             
            }

            private void Menu1_Click(object sender, EventArgs e)
            {
               
            }
            private void Menu2_Click(object sender, EventArgs e)
            {
                //TODO Your code here
            }
        }

        class CellRightController : SourceGrid.Cells.Controllers.ControllerBase
        {
            ContextMenu menu = new ContextMenu();
            Device device;
            SourceGrid.CellContext celContext;
            int x;
            int y;
            public CellRightController(Device device)
            {
                this.device = device;
                menu.MenuItems.Add("ROUTER 생성", new EventHandler(Add_Click));
                menu.MenuItems.Add("ROUTER 수정", new EventHandler(Modify_Click));
                menu.MenuItems.Add("ROUTER 삭제", new EventHandler(Delete_Click));
                menu.MenuItems.Add("ROUTER 설정", new EventHandler(Command_Click));
            }

            public override void OnMouseUp(SourceGrid.CellContext sender, MouseEventArgs e)
            {
                if (e.Button == MouseButtons.Right)
                {
                    this.x = e.X;
                    this.y = e.Y;
                    this.celContext = sender;
                    menu.Show(sender.Grid, new Point(e.X, e.Y));
                }
            }

            private void Add_Click(object sender, EventArgs e)
            {
                this.device.CreateDevice();
            }

            private void Modify_Click(object sender, EventArgs e)
            {
                this.device.ModifyDevice(this.celContext.Position.Row);
            }

            private void Delete_Click(object sender, EventArgs e)
            {
                if (MessageBox.Show("정말로 삭제하시겠습니까?", "관리자 삭제", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    int row = this.celContext.Position.Row;
                    this.device.DeleteDevice(row);
                }
            }

            private void Command_Click(object sender, EventArgs e)
            {
                device.CommandClick(this.celContext.Position.Row, this.x, this.y);
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
            public PopupMenu()
            {
                menu.MenuItems.Add("디바이스 삭제", new EventHandler(Menu1_Click));
                menu.MenuItems.Add("디바이스 수정", new EventHandler(Menu2_Click));
                menu.MenuItems.Add("디바이스 커맨드", new EventHandler(Menu2_Click));
            }

            public override void OnMouseUp(SourceGrid.CellContext sender, MouseEventArgs e)
            {
                base.OnMouseUp(sender, e);

                if (e.Button == MouseButtons.Right)
                    menu.Show(sender.Grid, new Point(e.X, e.Y));
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

        private void searchBtn_Click(object sender, EventArgs e)
        {
            searchDevice();
        }

        private void searchDevice()
        {
            searchOptionIndex = this.searchCombo.SelectedIndex;
            searchKeyword = this.searchTextBox.Text;
            searchOptionStr = (string)this.searchCombo.SelectedItem;
            this.selectedDevicePhoneNumber = -1;
            this.selectedDeviceSessionId = -1;
            this.PrepareDevice();
        }

        private void searchTextBox_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                searchDevice();
            }
        }

        private void deviceAddBtn_Click(object sender, EventArgs e)
        {
            this.CreateDevice();
        }

        private void pictureBoxMap_Click(object sender, EventArgs e)
        {
            if (this.mapExtendEnable)
            {
                if (this.isBigMapShowing) return;
                this.mapDialog = new MapDialog();
                this.mapDialog.Size = new Size(600, 400);
                int formX = this.Location.X;
                int formY = this.Location.Y;
                int formWidth = this.Width;
                int formHeight = this.Height;

                this.mapDialog.Location = new Point(
                    formX + (formWidth / 2) - this.mapDialog.Width / 2,
                    formY + 130
                    );
                this.mapDialog.mapBigPic.Dock = DockStyle.Fill;
                this.mapDialog.mapBigPic.Image = this.pictureBoxMap.Image;
                this.mapDialog.mapBigPic.Click += new EventHandler(CloseShowMap);
                this.Controls.Add(mapDialog);
                mapDialog.BringToFront();
                this.isBigMapShowing = true;
            }
        }

        public void closeBigMap()
        {
            if (!this.isBigMapShowing) return;
            this.mapDialog.mapBigPic.Click -= new System.EventHandler(CloseShowMap);
            this.Controls.Remove(mapDialog);
            this.mapDialog.Dispose();
            this.mapDialog = null;
            this.isBigMapShowing = false;
        }
        private void CloseShowMap(object sender, EventArgs e)
        {
            this.closeBigMap();
        }
    }


    public class ColumnInfo
    {
        public ColumnInfo(string name, bool visible)
        {
            ColumnName = name;
            ColumnStatus = visible;
        }
        public string ColumnName
        {
            get;
            set;
        }

        public bool ColumnStatus
        {
            get;
            set;
        }
    }

}
