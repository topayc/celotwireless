namespace CelotMClient.CustomView
{
    partial class Sms
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.smsAppGrid = new SourceGrid.Grid();
            this.smsListGrid = new SourceGrid.Grid();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(100)))));
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.smsAppGrid, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.smsListGrid, 0, 2);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 55F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 45F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(814, 663);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // smsAppGrid
            // 
            this.smsAppGrid.AutoStretchColumnsToFitWidth = true;
            this.smsAppGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.smsAppGrid.EnableSort = true;
            this.smsAppGrid.Location = new System.Drawing.Point(3, 3);
            this.smsAppGrid.Name = "smsAppGrid";
            this.smsAppGrid.OptimizeMode = SourceGrid.CellOptimizeMode.ForRows;
            this.smsAppGrid.SelectionMode = SourceGrid.GridSelectionMode.Row;
            this.smsAppGrid.Size = new System.Drawing.Size(808, 353);
            this.smsAppGrid.TabIndex = 0;
            this.smsAppGrid.TabStop = true;
            this.smsAppGrid.ToolTipText = "";
            // 
            // smsListGrid
            // 
            this.smsListGrid.AutoStretchColumnsToFitWidth = true;
            this.smsListGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.smsListGrid.EnableSort = true;
            this.smsListGrid.Location = new System.Drawing.Point(3, 372);
            this.smsListGrid.Name = "smsListGrid";
            this.smsListGrid.OptimizeMode = SourceGrid.CellOptimizeMode.ForRows;
            this.smsListGrid.SelectionMode = SourceGrid.GridSelectionMode.Cell;
            this.smsListGrid.Size = new System.Drawing.Size(808, 288);
            this.smsListGrid.TabIndex = 1;
            this.smsListGrid.TabStop = true;
            this.smsListGrid.ToolTipText = "";
            // 
            // Sms
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "Sms";
            this.Size = new System.Drawing.Size(814, 663);
            this.Load += new System.EventHandler(this.Sms_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private SourceGrid.Grid smsAppGrid;
        private SourceGrid.Grid smsListGrid;
    }
}
