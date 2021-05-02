namespace CelotMClient.CustomForm
{
    partial class RouterCommand
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.panel1 = new System.Windows.Forms.Panel();
            this.rawDataBtn = new System.Windows.Forms.Button();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.routerIpLabel = new System.Windows.Forms.Label();
            this.routerNameLabel = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.routerResetBtn = new System.Windows.Forms.Button();
            this.firmDownBtn = new System.Windows.Forms.Button();
            this.httpBtn = new System.Windows.Forms.Button();
            this.pinaBtn = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.panel5 = new System.Windows.Forms.Panel();
            this.resultTextBox = new System.Windows.Forms.TextBox();
            this.panel1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel5.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.rawDataBtn);
            this.panel1.Controls.Add(this.tableLayoutPanel1);
            this.panel1.Controls.Add(this.progressBar1);
            this.panel1.Controls.Add(this.routerResetBtn);
            this.panel1.Controls.Add(this.firmDownBtn);
            this.panel1.Controls.Add(this.httpBtn);
            this.panel1.Controls.Add(this.pinaBtn);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Controls.Add(this.panel5);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(411, 537);
            this.panel1.TabIndex = 0;
            // 
            // rawDataBtn
            // 
            this.rawDataBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(45)))));
            this.rawDataBtn.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.rawDataBtn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Black;
            this.rawDataBtn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(10)))), ((int)(((byte)(10)))));
            this.rawDataBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rawDataBtn.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.rawDataBtn.ForeColor = System.Drawing.Color.White;
            this.rawDataBtn.Location = new System.Drawing.Point(12, 149);
            this.rawDataBtn.Name = "rawDataBtn";
            this.rawDataBtn.Size = new System.Drawing.Size(387, 31);
            this.rawDataBtn.TabIndex = 10;
            this.rawDataBtn.Text = "PACKET RAW DATA";
            this.rawDataBtn.UseVisualStyleBackColor = false;
            this.rawDataBtn.Click += new System.EventHandler(this.rawDataBtn_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 26.75325F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 73.24675F));
            this.tableLayoutPanel1.Controls.Add(this.routerIpLabel, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.routerNameLabel, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.label2, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.label3, 0, 1);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(13, 38);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 33F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 27F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(385, 68);
            this.tableLayoutPanel1.TabIndex = 7;
            // 
            // routerIpLabel
            // 
            this.routerIpLabel.AutoSize = true;
            this.routerIpLabel.BackColor = System.Drawing.Color.Transparent;
            this.routerIpLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.routerIpLabel.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.routerIpLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.routerIpLabel.Location = new System.Drawing.Point(107, 35);
            this.routerIpLabel.Name = "routerIpLabel";
            this.routerIpLabel.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.routerIpLabel.Size = new System.Drawing.Size(274, 32);
            this.routerIpLabel.TabIndex = 3;
            this.routerIpLabel.Text = "110.110.110.100";
            this.routerIpLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // routerNameLabel
            // 
            this.routerNameLabel.AutoSize = true;
            this.routerNameLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.routerNameLabel.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.routerNameLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.routerNameLabel.Location = new System.Drawing.Point(107, 1);
            this.routerNameLabel.Name = "routerNameLabel";
            this.routerNameLabel.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.routerNameLabel.Size = new System.Drawing.Size(274, 33);
            this.routerNameLabel.TabIndex = 2;
            this.routerNameLabel.Text = "celot 110";
            this.routerNameLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(1, 1);
            this.label2.Margin = new System.Windows.Forms.Padding(0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(102, 33);
            this.label2.TabIndex = 0;
            this.label2.Text = "ROUTER NAME";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label3.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(1, 35);
            this.label3.Margin = new System.Windows.Forms.Padding(0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(102, 32);
            this.label3.TabIndex = 1;
            this.label3.Text = "ROUTER IP";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(13, 513);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(385, 15);
            this.progressBar1.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.progressBar1.TabIndex = 5;
            this.progressBar1.Value = 30;
            this.progressBar1.Visible = false;
            // 
            // routerResetBtn
            // 
            this.routerResetBtn.BackColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.routerResetBtn.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.routerResetBtn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Black;
            this.routerResetBtn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(10)))), ((int)(((byte)(10)))));
            this.routerResetBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.routerResetBtn.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.routerResetBtn.ForeColor = System.Drawing.Color.White;
            this.routerResetBtn.Location = new System.Drawing.Point(13, 258);
            this.routerResetBtn.Name = "routerResetBtn";
            this.routerResetBtn.Size = new System.Drawing.Size(387, 31);
            this.routerResetBtn.TabIndex = 4;
            this.routerResetBtn.Text = "ROUTER RESET";
            this.routerResetBtn.UseVisualStyleBackColor = false;
            this.routerResetBtn.Click += new System.EventHandler(this.routerResetBtn_Click);
            // 
            // firmDownBtn
            // 
            this.firmDownBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(45)))));
            this.firmDownBtn.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.firmDownBtn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Black;
            this.firmDownBtn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(10)))), ((int)(((byte)(10)))));
            this.firmDownBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.firmDownBtn.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.firmDownBtn.ForeColor = System.Drawing.Color.White;
            this.firmDownBtn.Location = new System.Drawing.Point(13, 222);
            this.firmDownBtn.Name = "firmDownBtn";
            this.firmDownBtn.Size = new System.Drawing.Size(387, 31);
            this.firmDownBtn.TabIndex = 3;
            this.firmDownBtn.Text = "FIRMWARE DOWNLOAD";
            this.firmDownBtn.UseVisualStyleBackColor = false;
            this.firmDownBtn.Click += new System.EventHandler(this.firmDownBtn_Click);
            // 
            // httpBtn
            // 
            this.httpBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(45)))));
            this.httpBtn.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.httpBtn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Black;
            this.httpBtn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(10)))), ((int)(((byte)(10)))));
            this.httpBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.httpBtn.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.httpBtn.ForeColor = System.Drawing.Color.White;
            this.httpBtn.Location = new System.Drawing.Point(13, 186);
            this.httpBtn.Name = "httpBtn";
            this.httpBtn.Size = new System.Drawing.Size(387, 31);
            this.httpBtn.TabIndex = 2;
            this.httpBtn.Text = "HTTP OPEN";
            this.httpBtn.UseVisualStyleBackColor = false;
            this.httpBtn.Click += new System.EventHandler(this.httpBtn_Click);
            // 
            // pinaBtn
            // 
            this.pinaBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(45)))));
            this.pinaBtn.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.pinaBtn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Black;
            this.pinaBtn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(10)))), ((int)(((byte)(10)))));
            this.pinaBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.pinaBtn.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.pinaBtn.ForeColor = System.Drawing.Color.White;
            this.pinaBtn.Location = new System.Drawing.Point(13, 112);
            this.pinaBtn.Name = "pinaBtn";
            this.pinaBtn.Size = new System.Drawing.Size(387, 31);
            this.pinaBtn.TabIndex = 1;
            this.pinaBtn.Text = "PING TEST";
            this.pinaBtn.UseVisualStyleBackColor = false;
            this.pinaBtn.Click += new System.EventHandler(this.pinaBtn_Click);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.panel3);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(409, 26);
            this.panel2.TabIndex = 0;
            this.panel2.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panel2_MouseDown);
            this.panel2.MouseMove += new System.Windows.Forms.MouseEventHandler(this.panel2_MouseMove);
            this.panel2.MouseUp += new System.Windows.Forms.MouseEventHandler(this.panel2_MouseUp);
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.Silver;
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel3.Location = new System.Drawing.Point(381, 6);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(17, 13);
            this.panel3.TabIndex = 1;
            this.panel3.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panel3_MouseDown);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(10, 5);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(126, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "ROUTER COMMAND";
            // 
            // panel5
            // 
            this.panel5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(10)))), ((int)(((byte)(10)))));
            this.panel5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel5.Controls.Add(this.resultTextBox);
            this.panel5.Location = new System.Drawing.Point(13, 301);
            this.panel5.Name = "panel5";
            this.panel5.Padding = new System.Windows.Forms.Padding(6);
            this.panel5.Size = new System.Drawing.Size(385, 206);
            this.panel5.TabIndex = 9;
            // 
            // resultTextBox
            // 
            this.resultTextBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(10)))), ((int)(((byte)(10)))));
            this.resultTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.resultTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.resultTextBox.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.resultTextBox.ForeColor = System.Drawing.Color.White;
            this.resultTextBox.HideSelection = false;
            this.resultTextBox.Location = new System.Drawing.Point(6, 6);
            this.resultTextBox.Margin = new System.Windows.Forms.Padding(6);
            this.resultTextBox.Multiline = true;
            this.resultTextBox.Name = "resultTextBox";
            this.resultTextBox.ReadOnly = true;
            this.resultTextBox.Size = new System.Drawing.Size(371, 192);
            this.resultTextBox.TabIndex = 6;
            // 
            // RouterCommand
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(411, 537);
            this.ControlBox = false;
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "RouterCommand";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "RouterCommandForm";
            this.Load += new System.EventHandler(this.RouterCommand_Load);
            this.panel1.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button pinaBtn;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button routerResetBtn;
        private System.Windows.Forms.Button firmDownBtn;
        private System.Windows.Forms.Button httpBtn;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label routerIpLabel;
        private System.Windows.Forms.Label routerNameLabel;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.TextBox resultTextBox;
        private System.Windows.Forms.Button rawDataBtn;
        
    }
}