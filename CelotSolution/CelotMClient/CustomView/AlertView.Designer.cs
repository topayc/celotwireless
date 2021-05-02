namespace CelotMClient.CustomView
{
    partial class AlertView
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
            this.alertLogBtn = new System.Windows.Forms.Button();
            this.aletsBtn = new System.Windows.Forms.Button();
            this.alertPanel = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // alertLogBtn
            // 
            this.alertLogBtn.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(100)))));
            this.alertLogBtn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.alertLogBtn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(70)))), ((int)(((byte)(70)))));
            this.alertLogBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.alertLogBtn.ForeColor = System.Drawing.Color.Ivory;
            this.alertLogBtn.Location = new System.Drawing.Point(159, 8);
            this.alertLogBtn.Name = "alertLogBtn";
            this.alertLogBtn.Size = new System.Drawing.Size(147, 27);
            this.alertLogBtn.TabIndex = 9;
            this.alertLogBtn.Text = "LOG";
            this.alertLogBtn.UseVisualStyleBackColor = true;
            this.alertLogBtn.Visible = false;
            this.alertLogBtn.Click += new System.EventHandler(this.alertLogBtn_Click);
            // 
            // aletsBtn
            // 
            this.aletsBtn.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(100)))));
            this.aletsBtn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.aletsBtn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(70)))), ((int)(((byte)(70)))));
            this.aletsBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.aletsBtn.ForeColor = System.Drawing.Color.Ivory;
            this.aletsBtn.Location = new System.Drawing.Point(9, 8);
            this.aletsBtn.Name = "aletsBtn";
            this.aletsBtn.Size = new System.Drawing.Size(147, 27);
            this.aletsBtn.TabIndex = 8;
            this.aletsBtn.Text = "ALERT";
            this.aletsBtn.UseVisualStyleBackColor = true;
            this.aletsBtn.Click += new System.EventHandler(this.aletsBtn_Click);
            // 
            // alertPanel
            // 
            this.alertPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.alertPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.alertPanel.Location = new System.Drawing.Point(9, 41);
            this.alertPanel.Name = "alertPanel";
            this.alertPanel.Size = new System.Drawing.Size(770, 600);
            this.alertPanel.TabIndex = 7;
            // 
            // AlertView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.Controls.Add(this.alertLogBtn);
            this.Controls.Add(this.aletsBtn);
            this.Controls.Add(this.alertPanel);
            this.Name = "AlertView";
            this.Size = new System.Drawing.Size(789, 649);
            this.Load += new System.EventHandler(this.AlertView_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button alertLogBtn;
        private System.Windows.Forms.Button aletsBtn;
        private System.Windows.Forms.Panel alertPanel;
    }
}
