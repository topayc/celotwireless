namespace CelotMClient.CustomView
{
    partial class ApplicationView
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

        #region 구성 요소 디자이너에서 생성한 코드

        /// <summary> 
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마십시오.
        /// </summary>
        private void InitializeComponent()
        {
            this.downloadBtn = new System.Windows.Forms.Button();
            this.appBtn = new System.Windows.Forms.Button();
            this.appPanel = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // downloadBtn
            // 
            this.downloadBtn.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(100)))));
            this.downloadBtn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.downloadBtn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(70)))), ((int)(((byte)(70)))));
            this.downloadBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.downloadBtn.ForeColor = System.Drawing.Color.Ivory;
            this.downloadBtn.Location = new System.Drawing.Point(159, 8);
            this.downloadBtn.Name = "downloadBtn";
            this.downloadBtn.Size = new System.Drawing.Size(147, 27);
            this.downloadBtn.TabIndex = 12;
            this.downloadBtn.Text = "DOWNLOAD";
            this.downloadBtn.UseVisualStyleBackColor = true;
            this.downloadBtn.Visible = false;
            this.downloadBtn.Click += new System.EventHandler(this.downloadBtn_Click);
            // 
            // appBtn
            // 
            this.appBtn.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(100)))));
            this.appBtn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.appBtn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(70)))), ((int)(((byte)(70)))));
            this.appBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.appBtn.ForeColor = System.Drawing.Color.Ivory;
            this.appBtn.Location = new System.Drawing.Point(9, 8);
            this.appBtn.Name = "appBtn";
            this.appBtn.Size = new System.Drawing.Size(147, 27);
            this.appBtn.TabIndex = 11;
            this.appBtn.Text = "APPLICATION";
            this.appBtn.UseVisualStyleBackColor = true;
            this.appBtn.Click += new System.EventHandler(this.appBtn_Click);
            // 
            // appPanel
            // 
            this.appPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.appPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(100)))));
            this.appPanel.Location = new System.Drawing.Point(9, 41);
            this.appPanel.Name = "appPanel";
            this.appPanel.Size = new System.Drawing.Size(770, 600);
            this.appPanel.TabIndex = 10;
            // 
            // ApplicationView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.Controls.Add(this.downloadBtn);
            this.Controls.Add(this.appBtn);
            this.Controls.Add(this.appPanel);
            this.Name = "ApplicationView";
            this.Size = new System.Drawing.Size(789, 649);
            this.Load += new System.EventHandler(this.ApplicationView_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button downloadBtn;
        private System.Windows.Forms.Button appBtn;
        private System.Windows.Forms.Panel appPanel;
    }
}
