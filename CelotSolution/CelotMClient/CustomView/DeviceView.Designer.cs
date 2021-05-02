namespace CelotMClient.CustomView
{
    partial class DeviceView
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
            this.onverViewBtn = new System.Windows.Forms.Button();
            this.deviceBtn = new System.Windows.Forms.Button();
            this.devicePanel = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // onverViewBtn
            // 
            this.onverViewBtn.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(100)))));
            this.onverViewBtn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.onverViewBtn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(70)))), ((int)(((byte)(70)))));
            this.onverViewBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.onverViewBtn.ForeColor = System.Drawing.Color.Ivory;
            this.onverViewBtn.Location = new System.Drawing.Point(8, 9);
            this.onverViewBtn.Name = "onverViewBtn";
            this.onverViewBtn.Size = new System.Drawing.Size(147, 27);
            this.onverViewBtn.TabIndex = 9;
            this.onverViewBtn.Text = "OVERVIEW";
            this.onverViewBtn.UseVisualStyleBackColor = true;
            this.onverViewBtn.Click += new System.EventHandler(this.onverViewBtn_Click);
            // 
            // deviceBtn
            // 
            this.deviceBtn.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(100)))));
            this.deviceBtn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.deviceBtn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(70)))), ((int)(((byte)(70)))));
            this.deviceBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.deviceBtn.ForeColor = System.Drawing.Color.Ivory;
            this.deviceBtn.Location = new System.Drawing.Point(161, 9);
            this.deviceBtn.Name = "deviceBtn";
            this.deviceBtn.Size = new System.Drawing.Size(147, 27);
            this.deviceBtn.TabIndex = 8;
            this.deviceBtn.Text = "DEVICE";
            this.deviceBtn.UseVisualStyleBackColor = true;
            this.deviceBtn.Visible = false;
            this.deviceBtn.Click += new System.EventHandler(this.deviceBtn_Click);
            // 
            // devicePanel
            // 
            this.devicePanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.devicePanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.devicePanel.Location = new System.Drawing.Point(8, 42);
            this.devicePanel.Name = "devicePanel";
            this.devicePanel.Size = new System.Drawing.Size(770, 600);
            this.devicePanel.TabIndex = 7;
            // 
            // DeviceView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.Controls.Add(this.onverViewBtn);
            this.Controls.Add(this.deviceBtn);
            this.Controls.Add(this.devicePanel);
            this.Name = "DeviceView";
            this.Size = new System.Drawing.Size(789, 649);
            this.Load += new System.EventHandler(this.DeviceView_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button onverViewBtn;
        private System.Windows.Forms.Button deviceBtn;
        private System.Windows.Forms.Panel devicePanel;
    }
}
