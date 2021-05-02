namespace CelotMClient.CustomForm
{
    partial class PeriodSelectForm
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
            this.panel2 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.cancelBtn = new Glass.GlassButton();
            this.okBtn = new Glass.GlassButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.monthRadioBtn = new System.Windows.Forms.RadioButton();
            this.dayRadioBtn = new System.Windows.Forms.RadioButton();
            this.dateLabel = new System.Windows.Forms.Label();
            this.periodTextBox = new System.Windows.Forms.TextBox();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Controls.Add(this.cancelBtn);
            this.panel1.Controls.Add(this.okBtn);
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Controls.Add(this.dateLabel);
            this.panel1.Controls.Add(this.periodTextBox);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(208, 189);
            this.panel1.TabIndex = 0;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(100)))));
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.label1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(206, 24);
            this.panel2.TabIndex = 9;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(8, 5);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(85, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "조회 기간 선택";
            // 
            // cancelBtn
            // 
            this.cancelBtn.FadeOnFocus = true;
            this.cancelBtn.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.cancelBtn.Location = new System.Drawing.Point(108, 146);
            this.cancelBtn.Name = "cancelBtn";
            this.cancelBtn.OuterBorderColor = System.Drawing.Color.Gray;
            this.cancelBtn.Size = new System.Drawing.Size(40, 20);
            this.cancelBtn.TabIndex = 8;
            this.cancelBtn.Text = "취소";
            this.cancelBtn.Click += new System.EventHandler(this.cancelBtn_Click);
            // 
            // okBtn
            // 
            this.okBtn.FadeOnFocus = true;
            this.okBtn.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.okBtn.Location = new System.Drawing.Point(62, 146);
            this.okBtn.Name = "okBtn";
            this.okBtn.OuterBorderColor = System.Drawing.Color.Gray;
            this.okBtn.Size = new System.Drawing.Size(40, 20);
            this.okBtn.TabIndex = 7;
            this.okBtn.Text = "확인";
            this.okBtn.Click += new System.EventHandler(this.okBtn_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.monthRadioBtn);
            this.groupBox1.Controls.Add(this.dayRadioBtn);
            this.groupBox1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.groupBox1.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.groupBox1.ForeColor = System.Drawing.Color.White;
            this.groupBox1.Location = new System.Drawing.Point(18, 42);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(168, 50);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "조회 타입";
            // 
            // monthRadioBtn
            // 
            this.monthRadioBtn.AutoSize = true;
            this.monthRadioBtn.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.monthRadioBtn.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.monthRadioBtn.Location = new System.Drawing.Point(95, 20);
            this.monthRadioBtn.Name = "monthRadioBtn";
            this.monthRadioBtn.Size = new System.Drawing.Size(34, 16);
            this.monthRadioBtn.TabIndex = 1;
            this.monthRadioBtn.TabStop = true;
            this.monthRadioBtn.Text = "월";
            this.monthRadioBtn.UseVisualStyleBackColor = true;
            this.monthRadioBtn.Click += new System.EventHandler(this.monthRadioBtn_Click);
            // 
            // dayRadioBtn
            // 
            this.dayRadioBtn.AutoSize = true;
            this.dayRadioBtn.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.dayRadioBtn.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.dayRadioBtn.Location = new System.Drawing.Point(28, 20);
            this.dayRadioBtn.Name = "dayRadioBtn";
            this.dayRadioBtn.Size = new System.Drawing.Size(34, 16);
            this.dayRadioBtn.TabIndex = 0;
            this.dayRadioBtn.TabStop = true;
            this.dayRadioBtn.Text = "일";
            this.dayRadioBtn.UseVisualStyleBackColor = true;
            this.dayRadioBtn.Click += new System.EventHandler(this.dayRadioBtn_Click);
            // 
            // dateLabel
            // 
            this.dateLabel.AutoSize = true;
            this.dateLabel.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.dateLabel.ForeColor = System.Drawing.Color.White;
            this.dateLabel.Location = new System.Drawing.Point(163, 112);
            this.dateLabel.Name = "dateLabel";
            this.dateLabel.Size = new System.Drawing.Size(17, 12);
            this.dateLabel.TabIndex = 5;
            this.dateLabel.Text = "일";
            // 
            // periodTextBox
            // 
            this.periodTextBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.periodTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.periodTextBox.ForeColor = System.Drawing.Color.White;
            this.periodTextBox.Location = new System.Drawing.Point(20, 107);
            this.periodTextBox.Name = "periodTextBox";
            this.periodTextBox.Size = new System.Drawing.Size(140, 21);
            this.periodTextBox.TabIndex = 4;
            // 
            // PeriodSelectForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(208, 189);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "PeriodSelectForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "PeriodSelectForm";
            this.Load += new System.EventHandler(this.PeriodSelectForm_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label dateLabel;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton monthRadioBtn;
        private System.Windows.Forms.RadioButton dayRadioBtn;
        private System.Windows.Forms.TextBox periodTextBox;
        private Glass.GlassButton cancelBtn;
        private Glass.GlassButton okBtn;
        private System.Windows.Forms.Panel panel2;

    }
}