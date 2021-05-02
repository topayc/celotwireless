namespace CelotMClient.CustomForm
{
    partial class CoordForm
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
            this.glassButton1 = new Glass.GlassButton();
            this.okBtn = new Glass.GlassButton();
            this.lngTextBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.latTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.glassButton1);
            this.panel1.Controls.Add(this.okBtn);
            this.panel1.Controls.Add(this.lngTextBox);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.latTextBox);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(250, 142);
            this.panel1.TabIndex = 0;
            // 
            // glassButton1
            // 
            this.glassButton1.Location = new System.Drawing.Point(177, 93);
            this.glassButton1.Name = "glassButton1";
            this.glassButton1.OuterBorderColor = System.Drawing.Color.DimGray;
            this.glassButton1.Size = new System.Drawing.Size(40, 23);
            this.glassButton1.TabIndex = 5;
            this.glassButton1.Text = "취소";
            this.glassButton1.Click += new System.EventHandler(this.glassButton1_Click);
            // 
            // okBtn
            // 
            this.okBtn.Location = new System.Drawing.Point(135, 93);
            this.okBtn.Name = "okBtn";
            this.okBtn.OuterBorderColor = System.Drawing.Color.DimGray;
            this.okBtn.Size = new System.Drawing.Size(40, 23);
            this.okBtn.TabIndex = 4;
            this.okBtn.Text = "적용";
            this.okBtn.Click += new System.EventHandler(this.okBtn_Click);
            // 
            // lngTextBox
            // 
            this.lngTextBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lngTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lngTextBox.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lngTextBox.ForeColor = System.Drawing.Color.White;
            this.lngTextBox.Location = new System.Drawing.Point(55, 55);
            this.lngTextBox.Name = "lngTextBox";
            this.lngTextBox.Size = new System.Drawing.Size(162, 21);
            this.lngTextBox.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(13, 55);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(31, 15);
            this.label2.TabIndex = 2;
            this.label2.Text = "경도";
            // 
            // latTextBox
            // 
            this.latTextBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.latTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.latTextBox.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.latTextBox.ForeColor = System.Drawing.Color.White;
            this.latTextBox.Location = new System.Drawing.Point(55, 19);
            this.latTextBox.Name = "latTextBox";
            this.latTextBox.Size = new System.Drawing.Size(162, 21);
            this.latTextBox.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(13, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(31, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "위도";
            // 
            // CoordForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(250, 142);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "CoordForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "CoordForm";
            this.Load += new System.EventHandler(this.CoordForm_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox lngTextBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox latTextBox;
        private Glass.GlassButton glassButton1;
        private Glass.GlassButton okBtn;
    }
}