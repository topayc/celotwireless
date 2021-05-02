using CelotMClient.Manager;
using System.Diagnostics;
namespace CelotMClient.CustomView
{
    partial class Device
    {
        /// <summary> 
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
                ApplicationCache.Instance().NMSDataNotify -= new NMSDataNotiHandler(this.Router_NMSDataNotify);
            base.Dispose(disposing);
        }

        #region 구성 요소 디자이너에서 생성한 코드

        /// <summary> 
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마십시오.
        /// </summary>
        private void InitializeComponent()
        {   
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Device));
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend2 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series3 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.deviceGrid = new SourceGrid.Grid();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.periodPicture = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.labelRssi = new System.Windows.Forms.Label();
            this.trafficLineChart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.labelTraffic = new System.Windows.Forms.Label();
            this.rssiLineChart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.tableDeviceSumContainer = new System.Windows.Forms.TableLayoutPanel();
            this.pictureBoxMap = new System.Windows.Forms.PictureBox();
            this.tableDeviceDetail = new System.Windows.Forms.TableLayoutPanel();
            this.nmsPeriodLabel = new System.Windows.Forms.Label();
            this.label46 = new System.Windows.Forms.Label();
            this.lanLabel = new System.Windows.Forms.Label();
            this.label44 = new System.Windows.Forms.Label();
            this.nmsLabel = new System.Windows.Forms.Label();
            this.label42 = new System.Windows.Forms.Label();
            this.coodLabel = new System.Windows.Forms.Label();
            this.label38 = new System.Windows.Forms.Label();
            this.wanLabel = new System.Windows.Forms.Label();
            this.label36 = new System.Windows.Forms.Label();
            this.firmLabel = new System.Windows.Forms.Label();
            this.label34 = new System.Windows.Forms.Label();
            this.battteryLabel = new System.Windows.Forms.Label();
            this.label30 = new System.Windows.Forms.Label();
            this.trafficLabel = new System.Windows.Forms.Label();
            this.label28 = new System.Windows.Forms.Label();
            this.serialLabel = new System.Windows.Forms.Label();
            this.label26 = new System.Windows.Forms.Label();
            this.smsLabel = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.rssiLabel = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.phoneLabel = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.wifiLabel = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.bandLabel = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.desLabel = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.lastConLabel = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.vpnLabel = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.nameLabel = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.deviceAddBtn = new System.Windows.Forms.Button();
            this.searchCombo = new System.Windows.Forms.ComboBox();
            this.searchTextBox = new System.Windows.Forms.TextBox();
            this.searchBtn = new System.Windows.Forms.Button();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.periodPicture)).BeginInit();
            this.panel2.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trafficLineChart)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rssiLineChart)).BeginInit();
            this.tableDeviceSumContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxMap)).BeginInit();
            this.tableDeviceDetail.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // deviceGrid
            // 
            this.deviceGrid.AutoStretchColumnsToFitWidth = true;
            this.deviceGrid.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.deviceGrid.ColumnsCount = 18;
            this.tableLayoutPanel1.SetColumnSpan(this.deviceGrid, 2);
            this.deviceGrid.DefaultWidth = 60;
            this.deviceGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.deviceGrid.EnableSort = false;
            this.deviceGrid.FixedRows = 1;
            this.deviceGrid.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.deviceGrid.Location = new System.Drawing.Point(3, 39);
            this.deviceGrid.Margin = new System.Windows.Forms.Padding(3, 6, 3, 3);
            this.deviceGrid.Name = "deviceGrid";
            this.deviceGrid.OptimizeMode = SourceGrid.CellOptimizeMode.ForRows;
            this.deviceGrid.SelectionMode = SourceGrid.GridSelectionMode.Row;
            this.deviceGrid.Size = new System.Drawing.Size(876, 360);
            this.deviceGrid.TabIndex = 0;
            this.deviceGrid.TabStop = true;
            this.deviceGrid.ToolTipText = "";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.deviceGrid, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.panel2, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.flowLayoutPanel1, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 4;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 33F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 55F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 31F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 45F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(882, 736);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.tableLayoutPanel1.SetColumnSpan(this.panel1, 2);
            this.panel1.Controls.Add(this.periodPicture);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 405);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(876, 25);
            this.panel1.TabIndex = 1;
            // 
            // periodPicture
            // 
            this.periodPicture.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("periodPicture.BackgroundImage")));
            this.periodPicture.Location = new System.Drawing.Point(103, 7);
            this.periodPicture.Name = "periodPicture";
            this.periodPicture.Size = new System.Drawing.Size(15, 15);
            this.periodPicture.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.periodPicture.TabIndex = 1;
            this.periodPicture.TabStop = false;
            this.periodPicture.Click += new System.EventHandler(this.periodPicture_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.label1.Location = new System.Drawing.Point(6, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(90, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "DATA HISTORY";
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Transparent;
            this.tableLayoutPanel1.SetColumnSpan(this.panel2, 2);
            this.panel2.Controls.Add(this.tableLayoutPanel2);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(3, 436);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(876, 297);
            this.panel2.TabIndex = 2;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.BackColor = System.Drawing.Color.Transparent;
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 101F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Controls.Add(this.labelRssi, 0, 2);
            this.tableLayoutPanel2.Controls.Add(this.trafficLineChart, 1, 1);
            this.tableLayoutPanel2.Controls.Add(this.labelTraffic, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.rssiLineChart, 1, 2);
            this.tableLayoutPanel2.Controls.Add(this.tableDeviceSumContainer, 0, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 3;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 41.85448F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 28.0103F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 30.13522F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(876, 297);
            this.tableLayoutPanel2.TabIndex = 0;
            // 
            // labelRssi
            // 
            this.labelRssi.AutoSize = true;
            this.labelRssi.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.labelRssi.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelRssi.Font = new System.Drawing.Font("맑은 고딕", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.labelRssi.ForeColor = System.Drawing.Color.Cyan;
            this.labelRssi.Location = new System.Drawing.Point(3, 208);
            this.labelRssi.Margin = new System.Windows.Forms.Padding(3, 1, 0, 1);
            this.labelRssi.Name = "labelRssi";
            this.labelRssi.Size = new System.Drawing.Size(98, 88);
            this.labelRssi.TabIndex = 1;
            this.labelRssi.Text = "RSSI Level";
            this.labelRssi.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // trafficLineChart
            // 
            this.trafficLineChart.BorderlineColor = System.Drawing.Color.Black;
            this.trafficLineChart.BorderSkin.BackColor = System.Drawing.Color.Black;
            this.trafficLineChart.BorderSkin.BorderColor = System.Drawing.Color.Transparent;
            chartArea1.AxisX.IsLabelAutoFit = false;
            chartArea1.AxisX.LabelStyle.Font = new System.Drawing.Font("맑은 고딕", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            chartArea1.AxisX.LabelStyle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            chartArea1.AxisY.IsLabelAutoFit = false;
            chartArea1.AxisY.LabelStyle.Font = new System.Drawing.Font("맑은 고딕", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            chartArea1.AxisY.LabelStyle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            chartArea1.BorderColor = System.Drawing.Color.Transparent;
            chartArea1.Name = "ChartArea1";
            this.trafficLineChart.ChartAreas.Add(chartArea1);
            this.trafficLineChart.Dock = System.Windows.Forms.DockStyle.Fill;
            legend1.Name = "Legend1";
            this.trafficLineChart.Legends.Add(legend1);
            this.trafficLineChart.Location = new System.Drawing.Point(103, 125);
            this.trafficLineChart.Margin = new System.Windows.Forms.Padding(2, 1, 1, 1);
            this.trafficLineChart.Name = "trafficLineChart";
            series1.BackGradientStyle = System.Windows.Forms.DataVisualization.Charting.GradientStyle.TopBottom;
            series1.BackSecondaryColor = System.Drawing.Color.Transparent;
            series1.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            series1.BorderWidth = 3;
            series1.ChartArea = "ChartArea1";
            series1.Color = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            series1.IsVisibleInLegend = false;
            series1.Legend = "Legend1";
            series1.Name = "Traffic_tx";
            series1.XValueMember = "Time";
            series1.YValueMembers = "Tx";
            series2.BackGradientStyle = System.Windows.Forms.DataVisualization.Charting.GradientStyle.DiagonalRight;
            series2.BackSecondaryColor = System.Drawing.Color.Transparent;
            series2.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            series2.BorderDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.NotSet;
            series2.BorderWidth = 3;
            series2.ChartArea = "ChartArea1";
            series2.Color = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            series2.Font = new System.Drawing.Font("맑은 고딕", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            series2.IsVisibleInLegend = false;
            series2.Legend = "Legend1";
            series2.Name = "Traffic_rx";
            series2.XValueMember = "Time";
            series2.YValueMembers = "Rx";
            this.trafficLineChart.Series.Add(series1);
            this.trafficLineChart.Series.Add(series2);
            this.trafficLineChart.Size = new System.Drawing.Size(772, 81);
            this.trafficLineChart.TabIndex = 0;
            this.trafficLineChart.Text = "chart1";
            // 
            // labelTraffic
            // 
            this.labelTraffic.AutoSize = true;
            this.labelTraffic.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.labelTraffic.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelTraffic.Font = new System.Drawing.Font("맑은 고딕", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.labelTraffic.ForeColor = System.Drawing.Color.Cyan;
            this.labelTraffic.Location = new System.Drawing.Point(3, 125);
            this.labelTraffic.Margin = new System.Windows.Forms.Padding(3, 1, 0, 1);
            this.labelTraffic.Name = "labelTraffic";
            this.labelTraffic.Size = new System.Drawing.Size(98, 81);
            this.labelTraffic.TabIndex = 0;
            this.labelTraffic.Text = "Traffic(TX, RX)";
            this.labelTraffic.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // rssiLineChart
            // 
            chartArea2.AxisX.IsLabelAutoFit = false;
            chartArea2.AxisX.LabelStyle.Font = new System.Drawing.Font("맑은 고딕", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            chartArea2.AxisX.LabelStyle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            chartArea2.AxisY.IsLabelAutoFit = false;
            chartArea2.AxisY.LabelStyle.Font = new System.Drawing.Font("맑은 고딕", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            chartArea2.AxisY.LabelStyle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            chartArea2.Name = "ChartArea1";
            this.rssiLineChart.ChartAreas.Add(chartArea2);
            this.rssiLineChart.Dock = System.Windows.Forms.DockStyle.Fill;
            legend2.Name = "Legend1";
            this.rssiLineChart.Legends.Add(legend2);
            this.rssiLineChart.Location = new System.Drawing.Point(103, 208);
            this.rssiLineChart.Margin = new System.Windows.Forms.Padding(2, 1, 1, 1);
            this.rssiLineChart.Name = "rssiLineChart";
            series3.BackGradientStyle = System.Windows.Forms.DataVisualization.Charting.GradientStyle.TopBottom;
            series3.BackSecondaryColor = System.Drawing.Color.Transparent;
            series3.BorderColor = System.Drawing.Color.Olive;
            series3.BorderWidth = 3;
            series3.ChartArea = "ChartArea1";
            series3.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.SplineArea;
            series3.Color = System.Drawing.Color.Olive;
            series3.IsVisibleInLegend = false;
            series3.Legend = "Legend1";
            series3.Name = "RSSI Level";
            series3.XValueMember = "Time";
            series3.XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.String;
            series3.YValueMembers = "RssiLevel";
            series3.YValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Int32;
            this.rssiLineChart.Series.Add(series3);
            this.rssiLineChart.Size = new System.Drawing.Size(772, 88);
            this.rssiLineChart.TabIndex = 2;
            this.rssiLineChart.Text = "chart3";
            // 
            // tableDeviceSumContainer
            // 
            this.tableDeviceSumContainer.ColumnCount = 2;
            this.tableLayoutPanel2.SetColumnSpan(this.tableDeviceSumContainer, 2);
            this.tableDeviceSumContainer.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 80.99379F));
            this.tableDeviceSumContainer.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 19.00621F));
            this.tableDeviceSumContainer.Controls.Add(this.pictureBoxMap, 1, 0);
            this.tableDeviceSumContainer.Controls.Add(this.tableDeviceDetail, 0, 0);
            this.tableDeviceSumContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableDeviceSumContainer.Location = new System.Drawing.Point(1, 0);
            this.tableDeviceSumContainer.Margin = new System.Windows.Forms.Padding(1, 0, 1, 3);
            this.tableDeviceSumContainer.Name = "tableDeviceSumContainer";
            this.tableDeviceSumContainer.RowCount = 1;
            this.tableDeviceSumContainer.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableDeviceSumContainer.Size = new System.Drawing.Size(874, 121);
            this.tableDeviceSumContainer.TabIndex = 6;
            // 
            // pictureBoxMap
            // 
            this.pictureBoxMap.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.pictureBoxMap.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBoxMap.ErrorImage = global::CelotMClient.Properties.Resources.web_init_image;
            this.pictureBoxMap.InitialImage = global::CelotMClient.Properties.Resources.web_init_image;
            this.pictureBoxMap.Location = new System.Drawing.Point(707, 0);
            this.pictureBoxMap.Margin = new System.Windows.Forms.Padding(0);
            this.pictureBoxMap.Name = "pictureBoxMap";
            this.pictureBoxMap.Size = new System.Drawing.Size(167, 121);
            this.pictureBoxMap.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBoxMap.TabIndex = 0;
            this.pictureBoxMap.TabStop = false;
            this.pictureBoxMap.Click += new System.EventHandler(this.pictureBoxMap_Click);
            // 
            // tableDeviceDetail
            // 
            this.tableDeviceDetail.ColumnCount = 6;
            this.tableDeviceDetail.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableDeviceDetail.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableDeviceDetail.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableDeviceDetail.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableDeviceDetail.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableDeviceDetail.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableDeviceDetail.Controls.Add(this.nmsPeriodLabel, 5, 5);
            this.tableDeviceDetail.Controls.Add(this.label46, 4, 5);
            this.tableDeviceDetail.Controls.Add(this.lanLabel, 3, 5);
            this.tableDeviceDetail.Controls.Add(this.label44, 2, 5);
            this.tableDeviceDetail.Controls.Add(this.nmsLabel, 1, 5);
            this.tableDeviceDetail.Controls.Add(this.label42, 0, 5);
            this.tableDeviceDetail.Controls.Add(this.coodLabel, 5, 4);
            this.tableDeviceDetail.Controls.Add(this.label38, 4, 4);
            this.tableDeviceDetail.Controls.Add(this.wanLabel, 3, 4);
            this.tableDeviceDetail.Controls.Add(this.label36, 2, 4);
            this.tableDeviceDetail.Controls.Add(this.firmLabel, 1, 4);
            this.tableDeviceDetail.Controls.Add(this.label34, 0, 4);
            this.tableDeviceDetail.Controls.Add(this.battteryLabel, 5, 3);
            this.tableDeviceDetail.Controls.Add(this.label30, 4, 3);
            this.tableDeviceDetail.Controls.Add(this.trafficLabel, 3, 3);
            this.tableDeviceDetail.Controls.Add(this.label28, 2, 3);
            this.tableDeviceDetail.Controls.Add(this.serialLabel, 1, 3);
            this.tableDeviceDetail.Controls.Add(this.label26, 0, 3);
            this.tableDeviceDetail.Controls.Add(this.smsLabel, 5, 2);
            this.tableDeviceDetail.Controls.Add(this.label22, 4, 2);
            this.tableDeviceDetail.Controls.Add(this.rssiLabel, 3, 2);
            this.tableDeviceDetail.Controls.Add(this.label20, 2, 2);
            this.tableDeviceDetail.Controls.Add(this.phoneLabel, 1, 2);
            this.tableDeviceDetail.Controls.Add(this.label18, 0, 2);
            this.tableDeviceDetail.Controls.Add(this.wifiLabel, 5, 1);
            this.tableDeviceDetail.Controls.Add(this.label14, 4, 1);
            this.tableDeviceDetail.Controls.Add(this.bandLabel, 3, 1);
            this.tableDeviceDetail.Controls.Add(this.label12, 2, 1);
            this.tableDeviceDetail.Controls.Add(this.desLabel, 1, 1);
            this.tableDeviceDetail.Controls.Add(this.label10, 0, 1);
            this.tableDeviceDetail.Controls.Add(this.lastConLabel, 5, 0);
            this.tableDeviceDetail.Controls.Add(this.label6, 4, 0);
            this.tableDeviceDetail.Controls.Add(this.vpnLabel, 3, 0);
            this.tableDeviceDetail.Controls.Add(this.label4, 2, 0);
            this.tableDeviceDetail.Controls.Add(this.nameLabel, 1, 0);
            this.tableDeviceDetail.Controls.Add(this.label2, 0, 0);
            this.tableDeviceDetail.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableDeviceDetail.Font = new System.Drawing.Font("맑은 고딕", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.tableDeviceDetail.ForeColor = System.Drawing.Color.Gainsboro;
            this.tableDeviceDetail.Location = new System.Drawing.Point(1, 0);
            this.tableDeviceDetail.Margin = new System.Windows.Forms.Padding(1, 0, 3, 0);
            this.tableDeviceDetail.Name = "tableDeviceDetail";
            this.tableDeviceDetail.RowCount = 6;
            this.tableDeviceDetail.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableDeviceDetail.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableDeviceDetail.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableDeviceDetail.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableDeviceDetail.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableDeviceDetail.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableDeviceDetail.Size = new System.Drawing.Size(703, 121);
            this.tableDeviceDetail.TabIndex = 1;
            // 
            // nmsPeriodLabel
            // 
            this.nmsPeriodLabel.AutoSize = true;
            this.nmsPeriodLabel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.nmsPeriodLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.nmsPeriodLabel.Font = new System.Drawing.Font("맑은 고딕", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.nmsPeriodLabel.ForeColor = System.Drawing.Color.White;
            this.nmsPeriodLabel.Location = new System.Drawing.Point(569, 101);
            this.nmsPeriodLabel.Margin = new System.Windows.Forms.Padding(1);
            this.nmsPeriodLabel.Name = "nmsPeriodLabel";
            this.nmsPeriodLabel.Padding = new System.Windows.Forms.Padding(7, 0, 0, 0);
            this.nmsPeriodLabel.Size = new System.Drawing.Size(133, 19);
            this.nmsPeriodLabel.TabIndex = 45;
            this.nmsPeriodLabel.Text = "  ";
            this.nmsPeriodLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label46
            // 
            this.label46.AutoSize = true;
            this.label46.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.label46.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label46.Font = new System.Drawing.Font("맑은 고딕", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label46.ForeColor = System.Drawing.Color.Gainsboro;
            this.label46.Location = new System.Drawing.Point(469, 101);
            this.label46.Margin = new System.Windows.Forms.Padding(1);
            this.label46.Name = "label46";
            this.label46.Padding = new System.Windows.Forms.Padding(0, 0, 10, 0);
            this.label46.Size = new System.Drawing.Size(98, 19);
            this.label46.TabIndex = 44;
            this.label46.Text = "NMS 주기";
            this.label46.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lanLabel
            // 
            this.lanLabel.AutoSize = true;
            this.lanLabel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.lanLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lanLabel.Font = new System.Drawing.Font("맑은 고딕", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lanLabel.ForeColor = System.Drawing.Color.White;
            this.lanLabel.Location = new System.Drawing.Point(335, 101);
            this.lanLabel.Margin = new System.Windows.Forms.Padding(1);
            this.lanLabel.Name = "lanLabel";
            this.lanLabel.Padding = new System.Windows.Forms.Padding(7, 0, 0, 0);
            this.lanLabel.Size = new System.Drawing.Size(132, 19);
            this.lanLabel.TabIndex = 43;
            this.lanLabel.Text = "  ";
            this.lanLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label44
            // 
            this.label44.AutoSize = true;
            this.label44.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.label44.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label44.Font = new System.Drawing.Font("맑은 고딕", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label44.ForeColor = System.Drawing.Color.Gainsboro;
            this.label44.Location = new System.Drawing.Point(235, 101);
            this.label44.Margin = new System.Windows.Forms.Padding(1);
            this.label44.Name = "label44";
            this.label44.Padding = new System.Windows.Forms.Padding(0, 0, 10, 0);
            this.label44.Size = new System.Drawing.Size(98, 19);
            this.label44.TabIndex = 42;
            this.label44.Text = "Lan IP";
            this.label44.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // nmsLabel
            // 
            this.nmsLabel.AutoSize = true;
            this.nmsLabel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.nmsLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.nmsLabel.Font = new System.Drawing.Font("맑은 고딕", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.nmsLabel.ForeColor = System.Drawing.Color.White;
            this.nmsLabel.Location = new System.Drawing.Point(101, 101);
            this.nmsLabel.Margin = new System.Windows.Forms.Padding(1);
            this.nmsLabel.Name = "nmsLabel";
            this.nmsLabel.Padding = new System.Windows.Forms.Padding(7, 0, 0, 0);
            this.nmsLabel.Size = new System.Drawing.Size(132, 19);
            this.nmsLabel.TabIndex = 41;
            this.nmsLabel.Text = "  ";
            this.nmsLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label42
            // 
            this.label42.AutoSize = true;
            this.label42.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.label42.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label42.Font = new System.Drawing.Font("맑은 고딕", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label42.ForeColor = System.Drawing.Color.Gainsboro;
            this.label42.Location = new System.Drawing.Point(1, 101);
            this.label42.Margin = new System.Windows.Forms.Padding(1);
            this.label42.Name = "label42";
            this.label42.Padding = new System.Windows.Forms.Padding(0, 0, 10, 0);
            this.label42.Size = new System.Drawing.Size(98, 19);
            this.label42.TabIndex = 40;
            this.label42.Text = "NMS";
            this.label42.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // coodLabel
            // 
            this.coodLabel.AutoSize = true;
            this.coodLabel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.coodLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.coodLabel.Font = new System.Drawing.Font("맑은 고딕", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.coodLabel.ForeColor = System.Drawing.Color.White;
            this.coodLabel.Location = new System.Drawing.Point(569, 81);
            this.coodLabel.Margin = new System.Windows.Forms.Padding(1);
            this.coodLabel.Name = "coodLabel";
            this.coodLabel.Padding = new System.Windows.Forms.Padding(7, 0, 0, 0);
            this.coodLabel.Size = new System.Drawing.Size(133, 18);
            this.coodLabel.TabIndex = 37;
            this.coodLabel.Text = "  ";
            this.coodLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label38
            // 
            this.label38.AutoSize = true;
            this.label38.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.label38.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label38.Font = new System.Drawing.Font("맑은 고딕", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label38.ForeColor = System.Drawing.Color.Gainsboro;
            this.label38.Location = new System.Drawing.Point(469, 81);
            this.label38.Margin = new System.Windows.Forms.Padding(1);
            this.label38.Name = "label38";
            this.label38.Padding = new System.Windows.Forms.Padding(0, 0, 10, 0);
            this.label38.Size = new System.Drawing.Size(98, 18);
            this.label38.TabIndex = 36;
            this.label38.Text = "Coordinate";
            this.label38.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // wanLabel
            // 
            this.wanLabel.AutoSize = true;
            this.wanLabel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.wanLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.wanLabel.Font = new System.Drawing.Font("맑은 고딕", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.wanLabel.ForeColor = System.Drawing.Color.White;
            this.wanLabel.Location = new System.Drawing.Point(335, 81);
            this.wanLabel.Margin = new System.Windows.Forms.Padding(1);
            this.wanLabel.Name = "wanLabel";
            this.wanLabel.Padding = new System.Windows.Forms.Padding(7, 0, 0, 0);
            this.wanLabel.Size = new System.Drawing.Size(132, 18);
            this.wanLabel.TabIndex = 35;
            this.wanLabel.Text = "  ";
            this.wanLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label36
            // 
            this.label36.AutoSize = true;
            this.label36.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.label36.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label36.Font = new System.Drawing.Font("맑은 고딕", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label36.ForeColor = System.Drawing.Color.Gainsboro;
            this.label36.Location = new System.Drawing.Point(235, 81);
            this.label36.Margin = new System.Windows.Forms.Padding(1);
            this.label36.Name = "label36";
            this.label36.Padding = new System.Windows.Forms.Padding(0, 0, 10, 0);
            this.label36.Size = new System.Drawing.Size(98, 18);
            this.label36.TabIndex = 34;
            this.label36.Text = "Wan IP";
            this.label36.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // firmLabel
            // 
            this.firmLabel.AutoSize = true;
            this.firmLabel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.firmLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.firmLabel.Font = new System.Drawing.Font("맑은 고딕", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.firmLabel.ForeColor = System.Drawing.Color.White;
            this.firmLabel.Location = new System.Drawing.Point(101, 81);
            this.firmLabel.Margin = new System.Windows.Forms.Padding(1);
            this.firmLabel.Name = "firmLabel";
            this.firmLabel.Padding = new System.Windows.Forms.Padding(7, 0, 0, 0);
            this.firmLabel.Size = new System.Drawing.Size(132, 18);
            this.firmLabel.TabIndex = 33;
            this.firmLabel.Text = "  ";
            this.firmLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label34
            // 
            this.label34.AutoSize = true;
            this.label34.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.label34.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label34.Font = new System.Drawing.Font("맑은 고딕", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label34.ForeColor = System.Drawing.Color.Gainsboro;
            this.label34.Location = new System.Drawing.Point(1, 81);
            this.label34.Margin = new System.Windows.Forms.Padding(1);
            this.label34.Name = "label34";
            this.label34.Padding = new System.Windows.Forms.Padding(0, 0, 10, 0);
            this.label34.Size = new System.Drawing.Size(98, 18);
            this.label34.TabIndex = 32;
            this.label34.Text = "Firmware";
            this.label34.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // battteryLabel
            // 
            this.battteryLabel.AutoSize = true;
            this.battteryLabel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.battteryLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.battteryLabel.Font = new System.Drawing.Font("맑은 고딕", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.battteryLabel.ForeColor = System.Drawing.Color.White;
            this.battteryLabel.Location = new System.Drawing.Point(569, 61);
            this.battteryLabel.Margin = new System.Windows.Forms.Padding(1);
            this.battteryLabel.Name = "battteryLabel";
            this.battteryLabel.Padding = new System.Windows.Forms.Padding(7, 0, 0, 0);
            this.battteryLabel.Size = new System.Drawing.Size(133, 18);
            this.battteryLabel.TabIndex = 29;
            this.battteryLabel.Text = "  ";
            this.battteryLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label30
            // 
            this.label30.AutoSize = true;
            this.label30.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.label30.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label30.Font = new System.Drawing.Font("맑은 고딕", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label30.ForeColor = System.Drawing.Color.Gainsboro;
            this.label30.Location = new System.Drawing.Point(469, 61);
            this.label30.Margin = new System.Windows.Forms.Padding(1);
            this.label30.Name = "label30";
            this.label30.Padding = new System.Windows.Forms.Padding(0, 0, 10, 0);
            this.label30.Size = new System.Drawing.Size(98, 18);
            this.label30.TabIndex = 28;
            this.label30.Text = "Battery";
            this.label30.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // trafficLabel
            // 
            this.trafficLabel.AutoSize = true;
            this.trafficLabel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.trafficLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.trafficLabel.Font = new System.Drawing.Font("맑은 고딕", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.trafficLabel.ForeColor = System.Drawing.Color.White;
            this.trafficLabel.Location = new System.Drawing.Point(335, 61);
            this.trafficLabel.Margin = new System.Windows.Forms.Padding(1);
            this.trafficLabel.Name = "trafficLabel";
            this.trafficLabel.Padding = new System.Windows.Forms.Padding(7, 0, 0, 0);
            this.trafficLabel.Size = new System.Drawing.Size(132, 18);
            this.trafficLabel.TabIndex = 27;
            this.trafficLabel.Text = "  ";
            this.trafficLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label28
            // 
            this.label28.AutoSize = true;
            this.label28.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.label28.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label28.Font = new System.Drawing.Font("맑은 고딕", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label28.ForeColor = System.Drawing.Color.Gainsboro;
            this.label28.Location = new System.Drawing.Point(235, 61);
            this.label28.Margin = new System.Windows.Forms.Padding(1);
            this.label28.Name = "label28";
            this.label28.Padding = new System.Windows.Forms.Padding(0, 0, 10, 0);
            this.label28.Size = new System.Drawing.Size(98, 18);
            this.label28.TabIndex = 26;
            this.label28.Text = "Traffic";
            this.label28.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // serialLabel
            // 
            this.serialLabel.AutoSize = true;
            this.serialLabel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.serialLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.serialLabel.Font = new System.Drawing.Font("맑은 고딕", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.serialLabel.ForeColor = System.Drawing.Color.White;
            this.serialLabel.Location = new System.Drawing.Point(101, 61);
            this.serialLabel.Margin = new System.Windows.Forms.Padding(1);
            this.serialLabel.Name = "serialLabel";
            this.serialLabel.Padding = new System.Windows.Forms.Padding(7, 0, 0, 0);
            this.serialLabel.Size = new System.Drawing.Size(132, 18);
            this.serialLabel.TabIndex = 25;
            this.serialLabel.Text = "  ";
            this.serialLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.label26.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label26.Font = new System.Drawing.Font("맑은 고딕", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label26.ForeColor = System.Drawing.Color.Gainsboro;
            this.label26.Location = new System.Drawing.Point(1, 61);
            this.label26.Margin = new System.Windows.Forms.Padding(1);
            this.label26.Name = "label26";
            this.label26.Padding = new System.Windows.Forms.Padding(0, 0, 10, 0);
            this.label26.Size = new System.Drawing.Size(98, 18);
            this.label26.TabIndex = 24;
            this.label26.Text = "Serial Num";
            this.label26.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // smsLabel
            // 
            this.smsLabel.AutoSize = true;
            this.smsLabel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.smsLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.smsLabel.Font = new System.Drawing.Font("맑은 고딕", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.smsLabel.ForeColor = System.Drawing.Color.White;
            this.smsLabel.Location = new System.Drawing.Point(569, 41);
            this.smsLabel.Margin = new System.Windows.Forms.Padding(1);
            this.smsLabel.Name = "smsLabel";
            this.smsLabel.Padding = new System.Windows.Forms.Padding(7, 0, 0, 0);
            this.smsLabel.Size = new System.Drawing.Size(133, 18);
            this.smsLabel.TabIndex = 21;
            this.smsLabel.Text = "  ";
            this.smsLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.label22.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label22.Font = new System.Drawing.Font("맑은 고딕", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label22.ForeColor = System.Drawing.Color.Gainsboro;
            this.label22.Location = new System.Drawing.Point(469, 41);
            this.label22.Margin = new System.Windows.Forms.Padding(1);
            this.label22.Name = "label22";
            this.label22.Padding = new System.Windows.Forms.Padding(0, 0, 10, 0);
            this.label22.Size = new System.Drawing.Size(98, 18);
            this.label22.TabIndex = 20;
            this.label22.Text = "SMS";
            this.label22.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // rssiLabel
            // 
            this.rssiLabel.AutoSize = true;
            this.rssiLabel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.rssiLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rssiLabel.Font = new System.Drawing.Font("맑은 고딕", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.rssiLabel.ForeColor = System.Drawing.Color.White;
            this.rssiLabel.Location = new System.Drawing.Point(335, 41);
            this.rssiLabel.Margin = new System.Windows.Forms.Padding(1);
            this.rssiLabel.Name = "rssiLabel";
            this.rssiLabel.Padding = new System.Windows.Forms.Padding(7, 0, 0, 0);
            this.rssiLabel.Size = new System.Drawing.Size(132, 18);
            this.rssiLabel.TabIndex = 19;
            this.rssiLabel.Text = "  ";
            this.rssiLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.label20.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label20.Font = new System.Drawing.Font("맑은 고딕", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label20.ForeColor = System.Drawing.Color.Gainsboro;
            this.label20.Location = new System.Drawing.Point(235, 41);
            this.label20.Margin = new System.Windows.Forms.Padding(1);
            this.label20.Name = "label20";
            this.label20.Padding = new System.Windows.Forms.Padding(0, 0, 10, 0);
            this.label20.Size = new System.Drawing.Size(98, 18);
            this.label20.TabIndex = 18;
            this.label20.Text = "RSSI";
            this.label20.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // phoneLabel
            // 
            this.phoneLabel.AutoSize = true;
            this.phoneLabel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.phoneLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.phoneLabel.Font = new System.Drawing.Font("맑은 고딕", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.phoneLabel.ForeColor = System.Drawing.Color.White;
            this.phoneLabel.Location = new System.Drawing.Point(101, 41);
            this.phoneLabel.Margin = new System.Windows.Forms.Padding(1);
            this.phoneLabel.Name = "phoneLabel";
            this.phoneLabel.Padding = new System.Windows.Forms.Padding(7, 0, 0, 0);
            this.phoneLabel.Size = new System.Drawing.Size(132, 18);
            this.phoneLabel.TabIndex = 17;
            this.phoneLabel.Text = "  ";
            this.phoneLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.label18.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label18.Font = new System.Drawing.Font("맑은 고딕", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label18.ForeColor = System.Drawing.Color.Gainsboro;
            this.label18.Location = new System.Drawing.Point(1, 41);
            this.label18.Margin = new System.Windows.Forms.Padding(1);
            this.label18.Name = "label18";
            this.label18.Padding = new System.Windows.Forms.Padding(0, 0, 10, 0);
            this.label18.Size = new System.Drawing.Size(98, 18);
            this.label18.TabIndex = 16;
            this.label18.Text = "Phone Num";
            this.label18.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // wifiLabel
            // 
            this.wifiLabel.AutoSize = true;
            this.wifiLabel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.wifiLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.wifiLabel.Font = new System.Drawing.Font("맑은 고딕", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.wifiLabel.ForeColor = System.Drawing.Color.White;
            this.wifiLabel.Location = new System.Drawing.Point(569, 21);
            this.wifiLabel.Margin = new System.Windows.Forms.Padding(1);
            this.wifiLabel.Name = "wifiLabel";
            this.wifiLabel.Padding = new System.Windows.Forms.Padding(7, 0, 0, 0);
            this.wifiLabel.Size = new System.Drawing.Size(133, 18);
            this.wifiLabel.TabIndex = 13;
            this.wifiLabel.Text = "  ";
            this.wifiLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.label14.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label14.Font = new System.Drawing.Font("맑은 고딕", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label14.ForeColor = System.Drawing.Color.Gainsboro;
            this.label14.Location = new System.Drawing.Point(469, 21);
            this.label14.Margin = new System.Windows.Forms.Padding(1);
            this.label14.Name = "label14";
            this.label14.Padding = new System.Windows.Forms.Padding(0, 0, 10, 0);
            this.label14.Size = new System.Drawing.Size(98, 18);
            this.label14.TabIndex = 12;
            this.label14.Text = "WIFI";
            this.label14.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // bandLabel
            // 
            this.bandLabel.AutoSize = true;
            this.bandLabel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.bandLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.bandLabel.Font = new System.Drawing.Font("맑은 고딕", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.bandLabel.ForeColor = System.Drawing.Color.White;
            this.bandLabel.Location = new System.Drawing.Point(335, 21);
            this.bandLabel.Margin = new System.Windows.Forms.Padding(1);
            this.bandLabel.Name = "bandLabel";
            this.bandLabel.Padding = new System.Windows.Forms.Padding(7, 0, 0, 0);
            this.bandLabel.Size = new System.Drawing.Size(132, 18);
            this.bandLabel.TabIndex = 11;
            this.bandLabel.Text = "  ";
            this.bandLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.label12.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label12.Font = new System.Drawing.Font("맑은 고딕", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label12.ForeColor = System.Drawing.Color.Gainsboro;
            this.label12.Location = new System.Drawing.Point(235, 21);
            this.label12.Margin = new System.Windows.Forms.Padding(1);
            this.label12.Name = "label12";
            this.label12.Padding = new System.Windows.Forms.Padding(0, 0, 10, 0);
            this.label12.Size = new System.Drawing.Size(98, 18);
            this.label12.TabIndex = 10;
            this.label12.Text = "BAND";
            this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // desLabel
            // 
            this.desLabel.AutoSize = true;
            this.desLabel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.desLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.desLabel.Font = new System.Drawing.Font("맑은 고딕", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.desLabel.ForeColor = System.Drawing.Color.White;
            this.desLabel.Location = new System.Drawing.Point(101, 21);
            this.desLabel.Margin = new System.Windows.Forms.Padding(1);
            this.desLabel.Name = "desLabel";
            this.desLabel.Padding = new System.Windows.Forms.Padding(7, 0, 0, 0);
            this.desLabel.Size = new System.Drawing.Size(132, 18);
            this.desLabel.TabIndex = 9;
            this.desLabel.Text = "  ";
            this.desLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.label10.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label10.Font = new System.Drawing.Font("맑은 고딕", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label10.ForeColor = System.Drawing.Color.Gainsboro;
            this.label10.Location = new System.Drawing.Point(1, 21);
            this.label10.Margin = new System.Windows.Forms.Padding(1);
            this.label10.Name = "label10";
            this.label10.Padding = new System.Windows.Forms.Padding(0, 0, 10, 0);
            this.label10.Size = new System.Drawing.Size(98, 18);
            this.label10.TabIndex = 8;
            this.label10.Text = "Description";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lastConLabel
            // 
            this.lastConLabel.AutoSize = true;
            this.lastConLabel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.lastConLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lastConLabel.Font = new System.Drawing.Font("맑은 고딕", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lastConLabel.ForeColor = System.Drawing.Color.White;
            this.lastConLabel.Location = new System.Drawing.Point(569, 1);
            this.lastConLabel.Margin = new System.Windows.Forms.Padding(1);
            this.lastConLabel.Name = "lastConLabel";
            this.lastConLabel.Padding = new System.Windows.Forms.Padding(7, 0, 0, 0);
            this.lastConLabel.Size = new System.Drawing.Size(133, 18);
            this.lastConLabel.TabIndex = 5;
            this.lastConLabel.Text = "  ";
            this.lastConLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.label6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label6.Font = new System.Drawing.Font("맑은 고딕", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label6.ForeColor = System.Drawing.Color.Gainsboro;
            this.label6.Location = new System.Drawing.Point(469, 1);
            this.label6.Margin = new System.Windows.Forms.Padding(1);
            this.label6.Name = "label6";
            this.label6.Padding = new System.Windows.Forms.Padding(0, 0, 10, 0);
            this.label6.Size = new System.Drawing.Size(98, 18);
            this.label6.TabIndex = 4;
            this.label6.Text = "Last Con";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // vpnLabel
            // 
            this.vpnLabel.AutoSize = true;
            this.vpnLabel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.vpnLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.vpnLabel.Font = new System.Drawing.Font("맑은 고딕", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.vpnLabel.ForeColor = System.Drawing.Color.White;
            this.vpnLabel.Location = new System.Drawing.Point(335, 1);
            this.vpnLabel.Margin = new System.Windows.Forms.Padding(1);
            this.vpnLabel.Name = "vpnLabel";
            this.vpnLabel.Padding = new System.Windows.Forms.Padding(7, 0, 0, 0);
            this.vpnLabel.Size = new System.Drawing.Size(132, 18);
            this.vpnLabel.TabIndex = 3;
            this.vpnLabel.Text = "  ";
            this.vpnLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.label4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label4.Font = new System.Drawing.Font("맑은 고딕", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label4.ForeColor = System.Drawing.Color.Gainsboro;
            this.label4.Location = new System.Drawing.Point(235, 1);
            this.label4.Margin = new System.Windows.Forms.Padding(1);
            this.label4.Name = "label4";
            this.label4.Padding = new System.Windows.Forms.Padding(0, 0, 10, 0);
            this.label4.Size = new System.Drawing.Size(98, 18);
            this.label4.TabIndex = 2;
            this.label4.Text = "VPN";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // nameLabel
            // 
            this.nameLabel.AutoSize = true;
            this.nameLabel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.nameLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.nameLabel.Font = new System.Drawing.Font("맑은 고딕", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.nameLabel.ForeColor = System.Drawing.Color.White;
            this.nameLabel.Location = new System.Drawing.Point(101, 1);
            this.nameLabel.Margin = new System.Windows.Forms.Padding(1);
            this.nameLabel.Name = "nameLabel";
            this.nameLabel.Padding = new System.Windows.Forms.Padding(7, 0, 0, 0);
            this.nameLabel.Size = new System.Drawing.Size(132, 18);
            this.nameLabel.TabIndex = 1;
            this.nameLabel.Text = "  ";
            this.nameLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Font = new System.Drawing.Font("맑은 고딕", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label2.ForeColor = System.Drawing.Color.Gainsboro;
            this.label2.Location = new System.Drawing.Point(1, 1);
            this.label2.Margin = new System.Windows.Forms.Padding(1);
            this.label2.Name = "label2";
            this.label2.Padding = new System.Windows.Forms.Padding(0, 0, 10, 0);
            this.label2.Size = new System.Drawing.Size(98, 18);
            this.label2.TabIndex = 0;
            this.label2.Text = "Name";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // flowLayoutPanel1
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.flowLayoutPanel1, 2);
            this.flowLayoutPanel1.Controls.Add(this.deviceAddBtn);
            this.flowLayoutPanel1.Controls.Add(this.searchCombo);
            this.flowLayoutPanel1.Controls.Add(this.searchTextBox);
            this.flowLayoutPanel1.Controls.Add(this.searchBtn);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(3, 3);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(876, 27);
            this.flowLayoutPanel1.TabIndex = 3;
            // 
            // deviceAddBtn
            // 
            this.deviceAddBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.deviceAddBtn.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(100)))));
            this.deviceAddBtn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.deviceAddBtn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(75)))), ((int)(((byte)(75)))), ((int)(((byte)(75)))));
            this.deviceAddBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.deviceAddBtn.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.deviceAddBtn.Location = new System.Drawing.Point(1, 3);
            this.deviceAddBtn.Margin = new System.Windows.Forms.Padding(1, 3, 5, 3);
            this.deviceAddBtn.Name = "deviceAddBtn";
            this.deviceAddBtn.Size = new System.Drawing.Size(75, 23);
            this.deviceAddBtn.TabIndex = 4;
            this.deviceAddBtn.Text = "DEVICE";
            this.deviceAddBtn.UseVisualStyleBackColor = false;
            this.deviceAddBtn.Click += new System.EventHandler(this.deviceAddBtn_Click);
            // 
            // searchCombo
            // 
            this.searchCombo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.searchCombo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.searchCombo.ForeColor = System.Drawing.Color.White;
            this.searchCombo.FormattingEnabled = true;
            this.searchCombo.Items.AddRange(new object[] {
            "Name",
            "Phone Number",
            "Router(LAN) IP",
            "WAN IP"});
            this.searchCombo.Location = new System.Drawing.Point(82, 3);
            this.searchCombo.Margin = new System.Windows.Forms.Padding(1, 3, 3, 3);
            this.searchCombo.Name = "searchCombo";
            this.searchCombo.Size = new System.Drawing.Size(136, 20);
            this.searchCombo.TabIndex = 1;
            // 
            // searchTextBox
            // 
            this.searchTextBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.searchTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.searchTextBox.ForeColor = System.Drawing.Color.White;
            this.searchTextBox.Location = new System.Drawing.Point(224, 3);
            this.searchTextBox.Name = "searchTextBox";
            this.searchTextBox.Size = new System.Drawing.Size(216, 21);
            this.searchTextBox.TabIndex = 2;
            this.searchTextBox.KeyUp += new System.Windows.Forms.KeyEventHandler(this.searchTextBox_KeyUp);
            // 
            // searchBtn
            // 
            this.searchBtn.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.searchBtn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.searchBtn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(65)))), ((int)(((byte)(65)))));
            this.searchBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.searchBtn.ForeColor = System.Drawing.Color.Gainsboro;
            this.searchBtn.Location = new System.Drawing.Point(446, 3);
            this.searchBtn.Name = "searchBtn";
            this.searchBtn.Size = new System.Drawing.Size(59, 23);
            this.searchBtn.TabIndex = 3;
            this.searchBtn.Text = "Search";
            this.searchBtn.UseVisualStyleBackColor = true;
            this.searchBtn.Click += new System.EventHandler(this.searchBtn_Click);
            // 
            // Device
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(100)))));
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "Device";
            this.Size = new System.Drawing.Size(882, 736);
            this.Load += new System.EventHandler(this.Device_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.periodPicture)).EndInit();
            this.panel2.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trafficLineChart)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rssiLineChart)).EndInit();
            this.tableDeviceSumContainer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxMap)).EndInit();
            this.tableDeviceDetail.ResumeLayout(false);
            this.tableDeviceDetail.PerformLayout();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        public SourceGrid.Grid deviceGrid;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.DataVisualization.Charting.Chart trafficLineChart;
        private System.Windows.Forms.DataVisualization.Charting.Chart rssiLineChart;
        private System.Windows.Forms.PictureBox periodPicture;
        private System.Windows.Forms.Label labelTraffic;
        private System.Windows.Forms.Label labelRssi;
        private System.Windows.Forms.TableLayoutPanel tableDeviceSumContainer;
        private System.Windows.Forms.PictureBox pictureBoxMap;
        private System.Windows.Forms.TableLayoutPanel tableDeviceDetail;
        private System.Windows.Forms.Label nmsPeriodLabel;
        private System.Windows.Forms.Label label46;
        private System.Windows.Forms.Label lanLabel;
        private System.Windows.Forms.Label label44;
        private System.Windows.Forms.Label nmsLabel;
        private System.Windows.Forms.Label label42;
        private System.Windows.Forms.Label coodLabel;
        private System.Windows.Forms.Label label38;
        private System.Windows.Forms.Label wanLabel;
        private System.Windows.Forms.Label label36;
        private System.Windows.Forms.Label firmLabel;
        private System.Windows.Forms.Label label34;
        private System.Windows.Forms.Label battteryLabel;
        private System.Windows.Forms.Label label30;
        private System.Windows.Forms.Label trafficLabel;
        private System.Windows.Forms.Label label28;
        private System.Windows.Forms.Label serialLabel;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.Label smsLabel;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.Label rssiLabel;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label phoneLabel;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label wifiLabel;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label bandLabel;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label desLabel;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label lastConLabel;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label vpnLabel;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label nameLabel;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.ComboBox searchCombo;
        private System.Windows.Forms.TextBox searchTextBox;
        private System.Windows.Forms.Button searchBtn;
        private System.Windows.Forms.Button deviceAddBtn;
    }
}
