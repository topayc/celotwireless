using CelotMClient.Manager;
namespace CelotMClient.CustomView
{
    partial class Alerts
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
                ApplicationCache.Instance().NMSDataNotify -= new NMSDataNotiHandler(this.Alert_NMSDataNotify);
            base.Dispose(disposing);
        }

        #region 구성 요소 디자이너에서 생성한 코드

        /// <summary> 
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마십시오.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.alertGrid = new SourceGrid.Grid();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.alertDetailContainerTable = new System.Windows.Forms.TableLayoutPanel();
            this.grid1 = new SourceGrid.Grid();
            this.alertDetailGridTable = new System.Windows.Forms.TableLayoutPanel();
            this.label3 = new System.Windows.Forms.Label();
            this.alertDetailGrid = new SourceGrid.DataGrid();
            this.alertDetailChartTable = new System.Windows.Forms.TableLayoutPanel();
            this.alertChart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.nameValuePairBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.label2 = new System.Windows.Forms.Label();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.label4 = new System.Windows.Forms.Label();
            this.curStatusGrid = new SourceGrid.DataGrid();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.searchCombo = new System.Windows.Forms.ComboBox();
            this.searchTextBox = new System.Windows.Forms.TextBox();
            this.serchBtn = new System.Windows.Forms.Button();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.alertDetailContainerTable.SuspendLayout();
            this.grid1.SuspendLayout();
            this.alertDetailGridTable.SuspendLayout();
            this.alertDetailChartTable.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.alertChart)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nameValuePairBindingSource)).BeginInit();
            this.tableLayoutPanel2.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 51.50376F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 48.49624F));
            this.tableLayoutPanel1.Controls.Add(this.alertGrid, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.alertDetailContainerTable, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.flowLayoutPanel1, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 4;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 33F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 55F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 45F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(798, 678);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // alertGrid
            // 
            this.alertGrid.AutoStretchColumnsToFitWidth = true;
            this.alertGrid.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.tableLayoutPanel1.SetColumnSpan(this.alertGrid, 2);
            this.alertGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.alertGrid.EnableSort = false;
            this.alertGrid.FixedRows = 1;
            this.alertGrid.Location = new System.Drawing.Point(3, 34);
            this.alertGrid.Margin = new System.Windows.Forms.Padding(3, 1, 3, 3);
            this.alertGrid.Name = "alertGrid";
            this.alertGrid.OptimizeMode = SourceGrid.CellOptimizeMode.ForRows;
            this.alertGrid.SelectionMode = SourceGrid.GridSelectionMode.Row;
            this.alertGrid.Size = new System.Drawing.Size(792, 337);
            this.alertGrid.TabIndex = 0;
            this.alertGrid.TabStop = true;
            this.alertGrid.ToolTipText = "";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.tableLayoutPanel1.SetColumnSpan(this.panel1, 2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 377);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(792, 19);
            this.panel1.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.label1.Location = new System.Drawing.Point(7, 0);
            this.label1.Name = "label1";
            this.label1.Padding = new System.Windows.Forms.Padding(0, 5, 0, 0);
            this.label1.Size = new System.Drawing.Size(82, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "LOG HISTORY";
            // 
            // alertDetailContainerTable
            // 
            this.alertDetailContainerTable.ColumnCount = 3;
            this.tableLayoutPanel1.SetColumnSpan(this.alertDetailContainerTable, 2);
            this.alertDetailContainerTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.alertDetailContainerTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.alertDetailContainerTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.alertDetailContainerTable.Controls.Add(this.grid1, 1, 0);
            this.alertDetailContainerTable.Controls.Add(this.alertDetailChartTable, 2, 0);
            this.alertDetailContainerTable.Controls.Add(this.tableLayoutPanel2, 0, 0);
            this.alertDetailContainerTable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.alertDetailContainerTable.Location = new System.Drawing.Point(3, 402);
            this.alertDetailContainerTable.Name = "alertDetailContainerTable";
            this.alertDetailContainerTable.RowCount = 2;
            this.alertDetailContainerTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.alertDetailContainerTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.alertDetailContainerTable.Size = new System.Drawing.Size(792, 273);
            this.alertDetailContainerTable.TabIndex = 2;
            // 
            // grid1
            // 
            this.grid1.Controls.Add(this.alertDetailGridTable);
            this.grid1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grid1.EnableSort = true;
            this.grid1.Location = new System.Drawing.Point(267, 3);
            this.grid1.Name = "grid1";
            this.grid1.OptimizeMode = SourceGrid.CellOptimizeMode.ForRows;
            this.grid1.SelectionMode = SourceGrid.GridSelectionMode.Cell;
            this.grid1.Size = new System.Drawing.Size(258, 247);
            this.grid1.TabIndex = 0;
            this.grid1.TabStop = true;
            this.grid1.ToolTipText = "";
            // 
            // alertDetailGridTable
            // 
            this.alertDetailGridTable.ColumnCount = 1;
            this.alertDetailGridTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.alertDetailGridTable.Controls.Add(this.label3, 0, 0);
            this.alertDetailGridTable.Controls.Add(this.alertDetailGrid, 0, 1);
            this.alertDetailGridTable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.alertDetailGridTable.Location = new System.Drawing.Point(0, 0);
            this.alertDetailGridTable.Name = "alertDetailGridTable";
            this.alertDetailGridTable.RowCount = 2;
            this.alertDetailGridTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.alertDetailGridTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.alertDetailGridTable.Size = new System.Drawing.Size(258, 247);
            this.alertDetailGridTable.TabIndex = 4;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(70)))), ((int)(((byte)(70)))));
            this.label3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label3.Font = new System.Drawing.Font("맑은 고딕", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(3, 0);
            this.label3.Name = "label3";
            this.label3.Padding = new System.Windows.Forms.Padding(5, 0, 0, 0);
            this.label3.Size = new System.Drawing.Size(252, 20);
            this.label3.TabIndex = 0;
            this.label3.Text = "ALERT LOG";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // alertDetailGrid
            // 
            this.alertDetailGrid.AutoStretchColumnsToFitWidth = true;
            this.alertDetailGrid.BackColor = System.Drawing.Color.White;
            this.alertDetailGrid.DeleteQuestionMessage = "Are you sure to delete all the selected rows?";
            this.alertDetailGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.alertDetailGrid.EnableSort = false;
            this.alertDetailGrid.FixedRows = 1;
            this.alertDetailGrid.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.alertDetailGrid.Location = new System.Drawing.Point(3, 23);
            this.alertDetailGrid.Name = "alertDetailGrid";
            this.alertDetailGrid.SelectionMode = SourceGrid.GridSelectionMode.Row;
            this.alertDetailGrid.Size = new System.Drawing.Size(252, 221);
            this.alertDetailGrid.TabIndex = 1;
            this.alertDetailGrid.TabStop = true;
            this.alertDetailGrid.ToolTipText = "";
            // 
            // alertDetailChartTable
            // 
            this.alertDetailChartTable.ColumnCount = 1;
            this.alertDetailChartTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.alertDetailChartTable.Controls.Add(this.alertChart, 0, 1);
            this.alertDetailChartTable.Controls.Add(this.label2, 0, 0);
            this.alertDetailChartTable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.alertDetailChartTable.Location = new System.Drawing.Point(531, 3);
            this.alertDetailChartTable.Name = "alertDetailChartTable";
            this.alertDetailChartTable.RowCount = 2;
            this.alertDetailChartTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.alertDetailChartTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.alertDetailChartTable.Size = new System.Drawing.Size(258, 247);
            this.alertDetailChartTable.TabIndex = 1;
            // 
            // alertChart
            // 
            chartArea1.Area3DStyle.Enable3D = true;
            chartArea1.BackSecondaryColor = System.Drawing.Color.White;
            chartArea1.Name = "ChartArea1";
            this.alertChart.ChartAreas.Add(chartArea1);
            this.alertChart.DataSource = this.nameValuePairBindingSource;
            this.alertChart.Dock = System.Windows.Forms.DockStyle.Fill;
            legend1.BackColor = System.Drawing.Color.Transparent;
            legend1.Font = new System.Drawing.Font("맑은 고딕", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            legend1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            legend1.IsTextAutoFit = false;
            legend1.Name = "Legend1";
            this.alertChart.Legends.Add(legend1);
            this.alertChart.Location = new System.Drawing.Point(3, 23);
            this.alertChart.Name = "alertChart";
            this.alertChart.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.SeaGreen;
            series1.BackImageTransparentColor = System.Drawing.Color.White;
            series1.BackSecondaryColor = System.Drawing.Color.White;
            series1.BorderColor = System.Drawing.Color.Silver;
            series1.ChartArea = "ChartArea1";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Doughnut;
            series1.Color = System.Drawing.Color.White;
            series1.LabelBorderWidth = 2;
            series1.LabelForeColor = System.Drawing.Color.White;
            series1.Legend = "Legend1";
            series1.LegendText = "#VALX (#VAL)";
            series1.Name = "AlertChart";
            series1.XValueMember = "Name";
            series1.XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.String;
            series1.YValueMembers = "Value";
            series1.YValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Int32;
            this.alertChart.Series.Add(series1);
            this.alertChart.Size = new System.Drawing.Size(252, 221);
            this.alertChart.TabIndex = 0;
            this.alertChart.Text = "chart1";
            // 
            // nameValuePairBindingSource
            // 
            this.nameValuePairBindingSource.DataSource = typeof(CelotMClient.Model.NameValuePair);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(70)))), ((int)(((byte)(70)))));
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Font = new System.Drawing.Font("맑은 고딕", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(3, 0);
            this.label2.Name = "label2";
            this.label2.Padding = new System.Windows.Forms.Padding(5, 0, 0, 0);
            this.label2.Size = new System.Drawing.Size(252, 20);
            this.label2.TabIndex = 1;
            this.label2.Text = "ALERT CHART";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 1;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Controls.Add(this.label4, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.curStatusGrid, 0, 1);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 2;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(258, 247);
            this.tableLayoutPanel2.TabIndex = 2;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.label4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label4.Font = new System.Drawing.Font("맑은 고딕", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(3, 0);
            this.label4.Name = "label4";
            this.label4.Padding = new System.Windows.Forms.Padding(5, 0, 0, 0);
            this.label4.Size = new System.Drawing.Size(252, 20);
            this.label4.TabIndex = 1;
            this.label4.Text = "SELECTED DEVICE";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // curStatusGrid
            // 
            this.curStatusGrid.BackColor = System.Drawing.Color.White;
            this.curStatusGrid.DeleteQuestionMessage = "Are you sure to delete all the selected rows?";
            this.curStatusGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.curStatusGrid.EnableSort = false;
            this.curStatusGrid.FixedRows = 1;
            this.curStatusGrid.Location = new System.Drawing.Point(3, 23);
            this.curStatusGrid.Name = "curStatusGrid";
            this.curStatusGrid.SelectionMode = SourceGrid.GridSelectionMode.Row;
            this.curStatusGrid.Size = new System.Drawing.Size(252, 221);
            this.curStatusGrid.TabIndex = 2;
            this.curStatusGrid.TabStop = true;
            this.curStatusGrid.ToolTipText = "";
            // 
            // flowLayoutPanel1
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.flowLayoutPanel1, 2);
            this.flowLayoutPanel1.Controls.Add(this.searchCombo);
            this.flowLayoutPanel1.Controls.Add(this.searchTextBox);
            this.flowLayoutPanel1.Controls.Add(this.serchBtn);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(3, 3);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(792, 27);
            this.flowLayoutPanel1.TabIndex = 3;
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
            this.searchCombo.Location = new System.Drawing.Point(3, 3);
            this.searchCombo.Name = "searchCombo";
            this.searchCombo.Size = new System.Drawing.Size(136, 20);
            this.searchCombo.TabIndex = 5;
            // 
            // searchTextBox
            // 
            this.searchTextBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.searchTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.searchTextBox.ForeColor = System.Drawing.Color.White;
            this.searchTextBox.Location = new System.Drawing.Point(145, 3);
            this.searchTextBox.Name = "searchTextBox";
            this.searchTextBox.Size = new System.Drawing.Size(216, 21);
            this.searchTextBox.TabIndex = 6;
            this.searchTextBox.KeyUp += new System.Windows.Forms.KeyEventHandler(this.searchTextBox_KeyUp);
            // 
            // serchBtn
            // 
            this.serchBtn.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.serchBtn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.serchBtn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(65)))), ((int)(((byte)(65)))));
            this.serchBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.serchBtn.ForeColor = System.Drawing.Color.White;
            this.serchBtn.Location = new System.Drawing.Point(367, 3);
            this.serchBtn.Name = "serchBtn";
            this.serchBtn.Size = new System.Drawing.Size(59, 19);
            this.serchBtn.TabIndex = 7;
            this.serchBtn.Text = "Search";
            this.serchBtn.UseVisualStyleBackColor = true;
            this.serchBtn.Click += new System.EventHandler(this.serchBtn_Click);
            // 
            // Alerts
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(100)))));
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "Alerts";
            this.Size = new System.Drawing.Size(798, 678);
            this.Load += new System.EventHandler(this.Alerts_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.alertDetailContainerTable.ResumeLayout(false);
            this.grid1.ResumeLayout(false);
            this.alertDetailGridTable.ResumeLayout(false);
            this.alertDetailGridTable.PerformLayout();
            this.alertDetailChartTable.ResumeLayout(false);
            this.alertDetailChartTable.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.alertChart)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nameValuePairBindingSource)).EndInit();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        public SourceGrid.Grid alertGrid;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TableLayoutPanel alertDetailContainerTable;
        private SourceGrid.Grid grid1;
        private System.Windows.Forms.TableLayoutPanel alertDetailChartTable;
        private System.Windows.Forms.DataVisualization.Charting.Chart alertChart;
        private System.Windows.Forms.TableLayoutPanel alertDetailGridTable;
        private System.Windows.Forms.Label label3;
        private SourceGrid.DataGrid alertDetailGrid;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Label label4;
        private SourceGrid.DataGrid curStatusGrid;
        private System.Windows.Forms.BindingSource nameValuePairBindingSource;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.ComboBox searchCombo;
        private System.Windows.Forms.TextBox searchTextBox;
        private System.Windows.Forms.Button serchBtn;

    }
}
