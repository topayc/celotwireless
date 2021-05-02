namespace CelotMClient.Setup
{
    partial class Setup2
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.progressTextBox = new System.Windows.Forms.RichTextBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.resultBox = new System.Windows.Forms.RichTextBox();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.progressTextBox);
            this.panel1.Location = new System.Drawing.Point(23, 29);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(557, 234);
            this.panel1.TabIndex = 0;
            // 
            // progressTextBox
            // 
            this.progressTextBox.BackColor = System.Drawing.Color.White;
            this.progressTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.progressTextBox.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.progressTextBox.Location = new System.Drawing.Point(7, 7);
            this.progressTextBox.Name = "progressTextBox";
            this.progressTextBox.Size = new System.Drawing.Size(535, 222);
            this.progressTextBox.TabIndex = 0;
            this.progressTextBox.Text = "";
            this.progressTextBox.TextChanged += new System.EventHandler(this.progressTextBox_TextChanged);
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.resultBox);
            this.panel2.Location = new System.Drawing.Point(23, 269);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(557, 199);
            this.panel2.TabIndex = 1;
            // 
            // resultBox
            // 
            this.resultBox.BackColor = System.Drawing.Color.White;
            this.resultBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.resultBox.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.resultBox.Location = new System.Drawing.Point(7, 29);
            this.resultBox.Name = "resultBox";
            this.resultBox.Size = new System.Drawing.Size(534, 181);
            this.resultBox.TabIndex = 0;
            this.resultBox.Text = "";
            // 
            // Setup2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "Setup2";
            this.Size = new System.Drawing.Size(611, 499);
            this.Load += new System.EventHandler(this.Setup2_Load);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        public System.Windows.Forms.RichTextBox progressTextBox;
        private System.Windows.Forms.Panel panel2;
        public System.Windows.Forms.RichTextBox resultBox;
    }
}
