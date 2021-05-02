namespace CelotMClient.CustomForm
{
    partial class MenuEditForm
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
            this.cancelBtn = new Glass.GlassButton();
            this.okBtn = new Glass.GlassButton();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.dashCheck = new System.Windows.Forms.CheckBox();
            this.deviceCheck = new System.Windows.Forms.CheckBox();
            this.alertCheck = new System.Windows.Forms.CheckBox();
            this.appCheck = new System.Windows.Forms.CheckBox();
            this.smsCheck = new System.Windows.Forms.CheckBox();
            this.panel1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.cancelBtn);
            this.panel1.Controls.Add(this.okBtn);
            this.panel1.Controls.Add(this.tableLayoutPanel1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(270, 266);
            this.panel1.TabIndex = 0;
            // 
            // cancelBtn
            // 
            this.cancelBtn.Font = new System.Drawing.Font("맑은 고딕", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.cancelBtn.Location = new System.Drawing.Point(128, 224);
            this.cancelBtn.Name = "cancelBtn";
            this.cancelBtn.OuterBorderColor = System.Drawing.Color.Gray;
            this.cancelBtn.Size = new System.Drawing.Size(39, 23);
            this.cancelBtn.TabIndex = 2;
            this.cancelBtn.Text = "취소";
            this.cancelBtn.Click += new System.EventHandler(this.cancelBtn_Click);
            // 
            // okBtn
            // 
            this.okBtn.Font = new System.Drawing.Font("맑은 고딕", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.okBtn.Location = new System.Drawing.Point(83, 224);
            this.okBtn.Name = "okBtn";
            this.okBtn.OuterBorderColor = System.Drawing.Color.Gray;
            this.okBtn.Size = new System.Drawing.Size(39, 23);
            this.okBtn.TabIndex = 1;
            this.okBtn.Text = "확인";
            this.okBtn.Click += new System.EventHandler(this.okBtn_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.58209F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 66.41791F));
            this.tableLayoutPanel1.Controls.Add(this.panel2, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.label2, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.label3, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.label4, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.label5, 1, 4);
            this.tableLayoutPanel1.Controls.Add(this.label6, 1, 5);
            this.tableLayoutPanel1.Controls.Add(this.dashCheck, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.deviceCheck, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.alertCheck, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.appCheck, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.smsCheck, 0, 5);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 6;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 33F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 33F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 33F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 33F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 33F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(268, 197);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.tableLayoutPanel1.SetColumnSpan(this.panel2, 2);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Margin = new System.Windows.Forms.Padding(0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(268, 24);
            this.panel2.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("맑은 고딕", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(9, 5);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "메뉴 편집";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Font = new System.Drawing.Font("맑은 고딕", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(99, 24);
            this.label2.Margin = new System.Windows.Forms.Padding(10, 0, 15, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(154, 33);
            this.label2.TabIndex = 1;
            this.label2.Text = "DASHBOARD";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label3.Font = new System.Drawing.Font("맑은 고딕", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(99, 57);
            this.label3.Margin = new System.Windows.Forms.Padding(10, 0, 15, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(154, 33);
            this.label3.TabIndex = 2;
            this.label3.Text = "ROUTER";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label4.Font = new System.Drawing.Font("맑은 고딕", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(99, 90);
            this.label4.Margin = new System.Windows.Forms.Padding(10, 0, 15, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(154, 33);
            this.label4.TabIndex = 3;
            this.label4.Text = "ALERT";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label5.Font = new System.Drawing.Font("맑은 고딕", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(99, 123);
            this.label5.Margin = new System.Windows.Forms.Padding(10, 0, 15, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(154, 33);
            this.label5.TabIndex = 4;
            this.label5.Text = "APPLICATION";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label6.Font = new System.Drawing.Font("맑은 고딕", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label6.ForeColor = System.Drawing.Color.White;
            this.label6.Location = new System.Drawing.Point(99, 156);
            this.label6.Margin = new System.Windows.Forms.Padding(10, 0, 15, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(154, 41);
            this.label6.TabIndex = 5;
            this.label6.Text = "SMS";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // dashCheck
            // 
            this.dashCheck.AutoSize = true;
            this.dashCheck.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dashCheck.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.dashCheck.Location = new System.Drawing.Point(3, 27);
            this.dashCheck.Margin = new System.Windows.Forms.Padding(3, 3, 10, 3);
            this.dashCheck.Name = "dashCheck";
            this.dashCheck.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.dashCheck.Size = new System.Drawing.Size(76, 27);
            this.dashCheck.TabIndex = 7;
            this.dashCheck.Text = "    ";
            this.dashCheck.UseVisualStyleBackColor = true;
            // 
            // deviceCheck
            // 
            this.deviceCheck.AutoSize = true;
            this.deviceCheck.Dock = System.Windows.Forms.DockStyle.Fill;
            this.deviceCheck.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.deviceCheck.Location = new System.Drawing.Point(3, 60);
            this.deviceCheck.Margin = new System.Windows.Forms.Padding(3, 3, 10, 3);
            this.deviceCheck.Name = "deviceCheck";
            this.deviceCheck.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.deviceCheck.Size = new System.Drawing.Size(76, 27);
            this.deviceCheck.TabIndex = 8;
            this.deviceCheck.Text = "    ";
            this.deviceCheck.UseVisualStyleBackColor = true;
            // 
            // alertCheck
            // 
            this.alertCheck.AutoSize = true;
            this.alertCheck.Dock = System.Windows.Forms.DockStyle.Fill;
            this.alertCheck.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.alertCheck.Location = new System.Drawing.Point(3, 93);
            this.alertCheck.Margin = new System.Windows.Forms.Padding(3, 3, 10, 3);
            this.alertCheck.Name = "alertCheck";
            this.alertCheck.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.alertCheck.Size = new System.Drawing.Size(76, 27);
            this.alertCheck.TabIndex = 9;
            this.alertCheck.Text = "    ";
            this.alertCheck.UseVisualStyleBackColor = true;
            // 
            // appCheck
            // 
            this.appCheck.AutoSize = true;
            this.appCheck.Dock = System.Windows.Forms.DockStyle.Fill;
            this.appCheck.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.appCheck.Location = new System.Drawing.Point(3, 126);
            this.appCheck.Margin = new System.Windows.Forms.Padding(3, 3, 10, 3);
            this.appCheck.Name = "appCheck";
            this.appCheck.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.appCheck.Size = new System.Drawing.Size(76, 27);
            this.appCheck.TabIndex = 10;
            this.appCheck.Text = "    ";
            this.appCheck.UseVisualStyleBackColor = true;
            // 
            // smsCheck
            // 
            this.smsCheck.AutoSize = true;
            this.smsCheck.Dock = System.Windows.Forms.DockStyle.Fill;
            this.smsCheck.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.smsCheck.Location = new System.Drawing.Point(3, 159);
            this.smsCheck.Margin = new System.Windows.Forms.Padding(3, 3, 10, 3);
            this.smsCheck.Name = "smsCheck";
            this.smsCheck.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.smsCheck.Size = new System.Drawing.Size(76, 35);
            this.smsCheck.TabIndex = 11;
            this.smsCheck.Text = "    ";
            this.smsCheck.UseVisualStyleBackColor = true;
            // 
            // MenuEditForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(270, 266);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "MenuEditForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "MenuEditForm";
            this.Load += new System.EventHandler(this.MenuEditForm_Load);
            this.panel1.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private Glass.GlassButton cancelBtn;
        private Glass.GlassButton okBtn;
        private System.Windows.Forms.CheckBox dashCheck;
        private System.Windows.Forms.CheckBox deviceCheck;
        private System.Windows.Forms.CheckBox alertCheck;
        private System.Windows.Forms.CheckBox appCheck;
        private System.Windows.Forms.CheckBox smsCheck;
    }
}