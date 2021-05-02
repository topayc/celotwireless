using CelotMClient.CustomControll;
namespace CelotMClient
{
    partial class MainForm
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
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다.
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마십시오.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.panel1 = new System.Windows.Forms.Panel();
            this.timeLabel = new System.Windows.Forms.Label();
            this.titleLabel = new System.Windows.Forms.Label();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.menuExtender = new System.Windows.Forms.Button();
            this.menuTableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.dashButton = new System.Windows.Forms.Button();
            this.deviceButton = new System.Windows.Forms.Button();
            this.settingButton = new System.Windows.Forms.Button();
            this.alertButton = new System.Windows.Forms.Button();
            this.smsButton = new System.Windows.Forms.Button();
            this.appButton = new System.Windows.Forms.Button();
            this.alertPanel = new System.Windows.Forms.Panel();
            this.recentTableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.recentAlertLabel = new System.Windows.Forms.Label();
            this.alertLabel = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.totalDeviceCount = new System.Windows.Forms.Label();
            this.normalDeviceCount = new System.Windows.Forms.Label();
            this.proDeviceCount = new System.Windows.Forms.Label();
            this.reservcePanel = new System.Windows.Forms.Panel();
            this.timeTimer = new System.Windows.Forms.Timer(this.components);
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.menuTableLayoutPanel.SuspendLayout();
            this.alertPanel.SuspendLayout();
            this.recentTableLayoutPanel.SuspendLayout();
            this.panel2.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(120)))), ((int)(((byte)(120)))));
            this.panel1.Controls.Add(this.timeLabel);
            this.panel1.Controls.Add(this.titleLabel);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1210, 27);
            this.panel1.TabIndex = 0;
            // 
            // timeLabel
            // 
            this.timeLabel.AutoSize = true;
            this.timeLabel.Font = new System.Drawing.Font("맑은 고딕", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.timeLabel.ForeColor = System.Drawing.Color.White;
            this.timeLabel.Location = new System.Drawing.Point(197, 6);
            this.timeLabel.Name = "timeLabel";
            this.timeLabel.Size = new System.Drawing.Size(0, 13);
            this.timeLabel.TabIndex = 1;
            // 
            // titleLabel
            // 
            this.titleLabel.AutoSize = true;
            this.titleLabel.Font = new System.Drawing.Font("맑은 고딕", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.titleLabel.ForeColor = System.Drawing.Color.Aqua;
            this.titleLabel.Location = new System.Drawing.Point(50, 4);
            this.titleLabel.Name = "titleLabel";
            this.titleLabel.Size = new System.Drawing.Size(66, 20);
            this.titleLabel.TabIndex = 0;
            this.titleLabel.Text = "C-Cloud";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.IsSplitterFixed = true;
            this.splitContainer1.Location = new System.Drawing.Point(0, 27);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.BackColor = System.Drawing.Color.Black;
            this.splitContainer1.Panel1.Controls.Add(this.menuExtender);
            this.splitContainer1.Panel1.Controls.Add(this.menuTableLayoutPanel);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.BackColor = System.Drawing.Color.Black;
            this.splitContainer1.Panel2.Font = new System.Drawing.Font("돋움", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.splitContainer1.Panel2MinSize = 400;
            this.splitContainer1.Size = new System.Drawing.Size(1210, 662);
            this.splitContainer1.SplitterDistance = 189;
            this.splitContainer1.SplitterWidth = 1;
            this.splitContainer1.TabIndex = 1;
            // 
            // menuExtender
            // 
            this.menuExtender.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.menuExtender.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.menuExtender.FlatAppearance.BorderColor = System.Drawing.Color.Aqua;
            this.menuExtender.FlatAppearance.BorderSize = 0;
            this.menuExtender.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.menuExtender.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(100)))));
            this.menuExtender.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.menuExtender.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.menuExtender.ForeColor = System.Drawing.Color.White;
            this.menuExtender.Location = new System.Drawing.Point(0, 638);
            this.menuExtender.Margin = new System.Windows.Forms.Padding(3, 1, 1, 1);
            this.menuExtender.Name = "menuExtender";
            this.menuExtender.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.menuExtender.Size = new System.Drawing.Size(189, 24);
            this.menuExtender.TabIndex = 8;
            this.menuExtender.Text = "<";
            this.menuExtender.UseVisualStyleBackColor = false;
            this.menuExtender.Click += new System.EventHandler(this.menuExtender_Click);
            // 
            // menuTableLayoutPanel
            // 
            this.menuTableLayoutPanel.ColumnCount = 1;
            this.menuTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.menuTableLayoutPanel.Controls.Add(this.dashButton, 0, 0);
            this.menuTableLayoutPanel.Controls.Add(this.deviceButton, 0, 1);
            this.menuTableLayoutPanel.Controls.Add(this.settingButton, 0, 5);
            this.menuTableLayoutPanel.Controls.Add(this.alertButton, 0, 2);
            this.menuTableLayoutPanel.Controls.Add(this.smsButton, 0, 4);
            this.menuTableLayoutPanel.Controls.Add(this.appButton, 0, 3);
            this.menuTableLayoutPanel.Controls.Add(this.alertPanel, 0, 9);
            this.menuTableLayoutPanel.Controls.Add(this.panel2, 0, 7);
            this.menuTableLayoutPanel.Controls.Add(this.reservcePanel, 0, 8);
            this.menuTableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.menuTableLayoutPanel.Location = new System.Drawing.Point(0, 0);
            this.menuTableLayoutPanel.Margin = new System.Windows.Forms.Padding(0);
            this.menuTableLayoutPanel.Name = "menuTableLayoutPanel";
            this.menuTableLayoutPanel.RowCount = 10;
            this.menuTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.menuTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.menuTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.menuTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.menuTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.menuTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.menuTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.menuTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 72F));
            this.menuTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 90F));
            this.menuTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            this.menuTableLayoutPanel.Size = new System.Drawing.Size(189, 510);
            this.menuTableLayoutPanel.TabIndex = 7;
            // 
            // dashButton
            // 
            this.dashButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.dashButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dashButton.FlatAppearance.BorderColor = System.Drawing.Color.Aqua;
            this.dashButton.FlatAppearance.BorderSize = 0;
            this.dashButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.dashButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(100)))));
            this.dashButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.dashButton.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.dashButton.ForeColor = System.Drawing.Color.White;
            this.dashButton.Location = new System.Drawing.Point(3, 1);
            this.dashButton.Margin = new System.Windows.Forms.Padding(3, 1, 1, 1);
            this.dashButton.Name = "dashButton";
            this.dashButton.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.dashButton.Size = new System.Drawing.Size(185, 27);
            this.dashButton.TabIndex = 0;
            this.dashButton.Text = "DASHBOARD";
            this.dashButton.UseVisualStyleBackColor = false;
            this.dashButton.Click += new System.EventHandler(this.dashButton_Click);
            // 
            // deviceButton
            // 
            this.deviceButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.deviceButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.deviceButton.FlatAppearance.BorderColor = System.Drawing.Color.Aqua;
            this.deviceButton.FlatAppearance.BorderSize = 0;
            this.deviceButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.deviceButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(100)))));
            this.deviceButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.deviceButton.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.deviceButton.ForeColor = System.Drawing.Color.White;
            this.deviceButton.Location = new System.Drawing.Point(3, 30);
            this.deviceButton.Margin = new System.Windows.Forms.Padding(3, 1, 3, 1);
            this.deviceButton.Name = "deviceButton";
            this.deviceButton.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.deviceButton.Size = new System.Drawing.Size(183, 27);
            this.deviceButton.TabIndex = 1;
            this.deviceButton.Text = "ROUTER";
            this.deviceButton.UseVisualStyleBackColor = false;
            this.deviceButton.Click += new System.EventHandler(this.deviceButton_Click);
            // 
            // settingButton
            // 
            this.settingButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.settingButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.settingButton.FlatAppearance.BorderColor = System.Drawing.Color.Aqua;
            this.settingButton.FlatAppearance.BorderSize = 0;
            this.settingButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.settingButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(100)))));
            this.settingButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.settingButton.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.settingButton.ForeColor = System.Drawing.Color.White;
            this.settingButton.Location = new System.Drawing.Point(3, 146);
            this.settingButton.Margin = new System.Windows.Forms.Padding(3, 1, 3, 1);
            this.settingButton.Name = "settingButton";
            this.settingButton.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.settingButton.Size = new System.Drawing.Size(183, 27);
            this.settingButton.TabIndex = 5;
            this.settingButton.Text = "SETTINGS";
            this.settingButton.UseVisualStyleBackColor = false;
            this.settingButton.Click += new System.EventHandler(this.settingButton_Click);
            // 
            // alertButton
            // 
            this.alertButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.alertButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.alertButton.FlatAppearance.BorderColor = System.Drawing.Color.Aqua;
            this.alertButton.FlatAppearance.BorderSize = 0;
            this.alertButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.alertButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(100)))));
            this.alertButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.alertButton.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.alertButton.ForeColor = System.Drawing.Color.White;
            this.alertButton.Location = new System.Drawing.Point(3, 59);
            this.alertButton.Margin = new System.Windows.Forms.Padding(3, 1, 3, 1);
            this.alertButton.Name = "alertButton";
            this.alertButton.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.alertButton.Size = new System.Drawing.Size(183, 27);
            this.alertButton.TabIndex = 2;
            this.alertButton.Text = "ALERTS";
            this.alertButton.UseVisualStyleBackColor = false;
            this.alertButton.Click += new System.EventHandler(this.alertButton_Click);
            // 
            // smsButton
            // 
            this.smsButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.smsButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.smsButton.FlatAppearance.BorderColor = System.Drawing.Color.Aqua;
            this.smsButton.FlatAppearance.BorderSize = 0;
            this.smsButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.smsButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(100)))));
            this.smsButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.smsButton.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.smsButton.ForeColor = System.Drawing.Color.White;
            this.smsButton.Location = new System.Drawing.Point(3, 117);
            this.smsButton.Margin = new System.Windows.Forms.Padding(3, 1, 3, 1);
            this.smsButton.Name = "smsButton";
            this.smsButton.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.smsButton.Size = new System.Drawing.Size(183, 27);
            this.smsButton.TabIndex = 4;
            this.smsButton.Text = "SMS";
            this.smsButton.UseVisualStyleBackColor = false;
            this.smsButton.Click += new System.EventHandler(this.smsButton_Click);
            // 
            // appButton
            // 
            this.appButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.appButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.appButton.FlatAppearance.BorderColor = System.Drawing.Color.Aqua;
            this.appButton.FlatAppearance.BorderSize = 0;
            this.appButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.appButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(100)))));
            this.appButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.appButton.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.appButton.ForeColor = System.Drawing.Color.White;
            this.appButton.Location = new System.Drawing.Point(3, 88);
            this.appButton.Margin = new System.Windows.Forms.Padding(3, 1, 3, 1);
            this.appButton.Name = "appButton";
            this.appButton.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.appButton.Size = new System.Drawing.Size(183, 27);
            this.appButton.TabIndex = 3;
            this.appButton.Text = "APPLICATION";
            this.appButton.UseVisualStyleBackColor = false;
            this.appButton.Click += new System.EventHandler(this.appButton_Click);
            // 
            // alertPanel
            // 
            this.alertPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(45)))));
            this.alertPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.alertPanel.Controls.Add(this.recentTableLayoutPanel);
            this.alertPanel.Controls.Add(this.label1);
            this.alertPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.alertPanel.Location = new System.Drawing.Point(3, 338);
            this.alertPanel.Margin = new System.Windows.Forms.Padding(3, 2, 3, 3);
            this.alertPanel.Name = "alertPanel";
            this.alertPanel.Size = new System.Drawing.Size(183, 169);
            this.alertPanel.TabIndex = 6;
            // 
            // recentTableLayoutPanel
            // 
            this.recentTableLayoutPanel.ColumnCount = 1;
            this.recentTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.recentTableLayoutPanel.Controls.Add(this.recentAlertLabel, 0, 0);
            this.recentTableLayoutPanel.Controls.Add(this.alertLabel, 0, 1);
            this.recentTableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.recentTableLayoutPanel.Location = new System.Drawing.Point(0, 0);
            this.recentTableLayoutPanel.Margin = new System.Windows.Forms.Padding(3, 1, 3, 3);
            this.recentTableLayoutPanel.Name = "recentTableLayoutPanel";
            this.recentTableLayoutPanel.RowCount = 2;
            this.recentTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 11.05991F));
            this.recentTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 88.94009F));
            this.recentTableLayoutPanel.Size = new System.Drawing.Size(181, 167);
            this.recentTableLayoutPanel.TabIndex = 1;
            // 
            // recentAlertLabel
            // 
            this.recentAlertLabel.AutoSize = true;
            this.recentAlertLabel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(70)))), ((int)(((byte)(70)))));
            this.recentAlertLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.recentAlertLabel.Font = new System.Drawing.Font("맑은 고딕", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.recentAlertLabel.ForeColor = System.Drawing.Color.White;
            this.recentAlertLabel.Location = new System.Drawing.Point(0, 0);
            this.recentAlertLabel.Margin = new System.Windows.Forms.Padding(0);
            this.recentAlertLabel.Name = "recentAlertLabel";
            this.recentAlertLabel.Padding = new System.Windows.Forms.Padding(5, 0, 0, 0);
            this.recentAlertLabel.Size = new System.Drawing.Size(181, 18);
            this.recentAlertLabel.TabIndex = 2;
            this.recentAlertLabel.Text = "RECENT ALERT";
            this.recentAlertLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // alertLabel
            // 
            this.alertLabel.AutoSize = true;
            this.alertLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.alertLabel.Font = new System.Drawing.Font("굴림", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.alertLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(235)))), ((int)(((byte)(235)))));
            this.alertLabel.Location = new System.Drawing.Point(8, 25);
            this.alertLabel.Margin = new System.Windows.Forms.Padding(8, 7, 3, 0);
            this.alertLabel.Name = "alertLabel";
            this.alertLabel.Size = new System.Drawing.Size(170, 142);
            this.alertLabel.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(6, -561);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "알람 정보";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.tableLayoutPanel2);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(3, 179);
            this.panel2.Margin = new System.Windows.Forms.Padding(3, 5, 3, 2);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(183, 65);
            this.panel2.TabIndex = 7;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 60F));
            this.tableLayoutPanel2.Controls.Add(this.label4, 0, 2);
            this.tableLayoutPanel2.Controls.Add(this.label2, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.label3, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.totalDeviceCount, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.normalDeviceCount, 1, 1);
            this.tableLayoutPanel2.Controls.Add(this.proDeviceCount, 1, 2);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(3, 0, 3, 1);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 3;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33334F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33334F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(183, 65);
            this.tableLayoutPanel2.TabIndex = 0;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(70)))), ((int)(((byte)(70)))));
            this.label4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label4.Font = new System.Drawing.Font("굴림", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(2, 44);
            this.label4.Margin = new System.Windows.Forms.Padding(1);
            this.label4.Name = "label4";
            this.label4.Padding = new System.Windows.Forms.Padding(5, 4, 0, 0);
            this.label4.Size = new System.Drawing.Size(70, 19);
            this.label4.TabIndex = 5;
            this.label4.Text = "장애";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(70)))), ((int)(((byte)(70)))));
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Font = new System.Drawing.Font("굴림", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(2, 2);
            this.label2.Margin = new System.Windows.Forms.Padding(1);
            this.label2.Name = "label2";
            this.label2.Padding = new System.Windows.Forms.Padding(5, 4, 0, 0);
            this.label2.Size = new System.Drawing.Size(70, 18);
            this.label2.TabIndex = 3;
            this.label2.Text = "수량";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(70)))), ((int)(((byte)(70)))));
            this.label3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label3.Font = new System.Drawing.Font("굴림", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(2, 23);
            this.label3.Margin = new System.Windows.Forms.Padding(1);
            this.label3.Name = "label3";
            this.label3.Padding = new System.Windows.Forms.Padding(5, 4, 0, 0);
            this.label3.Size = new System.Drawing.Size(70, 18);
            this.label3.TabIndex = 4;
            this.label3.Text = "정상";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // totalDeviceCount
            // 
            this.totalDeviceCount.AutoSize = true;
            this.totalDeviceCount.Dock = System.Windows.Forms.DockStyle.Fill;
            this.totalDeviceCount.Font = new System.Drawing.Font("굴림", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.totalDeviceCount.ForeColor = System.Drawing.Color.White;
            this.totalDeviceCount.Location = new System.Drawing.Point(77, 2);
            this.totalDeviceCount.Margin = new System.Windows.Forms.Padding(3, 1, 3, 1);
            this.totalDeviceCount.Name = "totalDeviceCount";
            this.totalDeviceCount.Size = new System.Drawing.Size(102, 18);
            this.totalDeviceCount.TabIndex = 6;
            this.totalDeviceCount.Text = "  ";
            this.totalDeviceCount.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // normalDeviceCount
            // 
            this.normalDeviceCount.AutoSize = true;
            this.normalDeviceCount.Dock = System.Windows.Forms.DockStyle.Fill;
            this.normalDeviceCount.Font = new System.Drawing.Font("굴림", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.normalDeviceCount.ForeColor = System.Drawing.Color.White;
            this.normalDeviceCount.Location = new System.Drawing.Point(77, 23);
            this.normalDeviceCount.Margin = new System.Windows.Forms.Padding(3, 1, 3, 1);
            this.normalDeviceCount.Name = "normalDeviceCount";
            this.normalDeviceCount.Size = new System.Drawing.Size(102, 18);
            this.normalDeviceCount.TabIndex = 7;
            this.normalDeviceCount.Text = "  ";
            this.normalDeviceCount.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // proDeviceCount
            // 
            this.proDeviceCount.AutoSize = true;
            this.proDeviceCount.Dock = System.Windows.Forms.DockStyle.Fill;
            this.proDeviceCount.Font = new System.Drawing.Font("굴림", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.proDeviceCount.ForeColor = System.Drawing.Color.Red;
            this.proDeviceCount.Location = new System.Drawing.Point(77, 44);
            this.proDeviceCount.Margin = new System.Windows.Forms.Padding(3, 1, 3, 1);
            this.proDeviceCount.Name = "proDeviceCount";
            this.proDeviceCount.Size = new System.Drawing.Size(102, 19);
            this.proDeviceCount.TabIndex = 8;
            this.proDeviceCount.Text = "  ";
            this.proDeviceCount.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // reservcePanel
            // 
            this.reservcePanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(45)))));
            this.reservcePanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.reservcePanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.reservcePanel.Location = new System.Drawing.Point(3, 247);
            this.reservcePanel.Margin = new System.Windows.Forms.Padding(3, 1, 3, 2);
            this.reservcePanel.Name = "reservcePanel";
            this.reservcePanel.Size = new System.Drawing.Size(183, 87);
            this.reservcePanel.TabIndex = 8;
            // 
            // timeTimer
            // 
            this.timeTimer.Interval = 1000;
            this.timeTimer.Tick += new System.EventHandler(this.timeTimer_Tick);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1210, 689);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.panel1);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "CelotManagerClient";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainForm_FormClosed);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Resize += new System.EventHandler(this.MainForm_Resize);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.menuTableLayoutPanel.ResumeLayout(false);
            this.alertPanel.ResumeLayout(false);
            this.alertPanel.PerformLayout();
            this.recentTableLayoutPanel.ResumeLayout(false);
            this.recentTableLayoutPanel.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Button settingButton;
        private System.Windows.Forms.Button smsButton;
        private System.Windows.Forms.Button appButton;
        private System.Windows.Forms.Button alertButton;
        private System.Windows.Forms.Button deviceButton;
        private System.Windows.Forms.Button dashButton;
        private System.Windows.Forms.Panel alertPanel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label alertLabel;
        private System.Windows.Forms.TableLayoutPanel menuTableLayoutPanel;
        private System.Windows.Forms.Label recentAlertLabel;
        private System.Windows.Forms.TableLayoutPanel recentTableLayoutPanel;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label totalDeviceCount;
        private System.Windows.Forms.Label normalDeviceCount;
        private System.Windows.Forms.Label proDeviceCount;
        private System.Windows.Forms.Label timeLabel;
        private System.Windows.Forms.Label titleLabel;
        private System.Windows.Forms.Timer timeTimer;
        private System.Windows.Forms.Panel reservcePanel;
        private System.Windows.Forms.Button menuExtender;
    }
}

