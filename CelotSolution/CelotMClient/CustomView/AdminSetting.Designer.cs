﻿namespace CelotMClient.CustomView
{
    partial class AdminSetting
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
            this.adminGrid = new SourceGrid.Grid();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.adminAddBtn = new Glass.GlassButton();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // adminGrid
            // 
            this.adminGrid.AutoSize = true;
            this.adminGrid.AutoStretchColumnsToFitWidth = true;
            this.adminGrid.BackColor = System.Drawing.Color.Transparent;
            this.adminGrid.ColumnsCount = 6;
            this.adminGrid.EnableSort = true;
            this.adminGrid.Location = new System.Drawing.Point(3, 29);
            this.adminGrid.Name = "adminGrid";
            this.adminGrid.OptimizeMode = SourceGrid.CellOptimizeMode.ForColumns;
            this.adminGrid.SelectionMode = SourceGrid.GridSelectionMode.Row;
            this.adminGrid.Size = new System.Drawing.Size(0, 0);
            this.adminGrid.TabIndex = 0;
            this.adminGrid.TabStop = true;
            this.adminGrid.ToolTipText = "";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.adminGrid, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.adminAddBtn, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 305F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(736, 331);
            this.tableLayoutPanel1.TabIndex = 2;
            // 
            // adminAddBtn
            // 
            this.adminAddBtn.Font = new System.Drawing.Font("맑은 고딕", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.adminAddBtn.Location = new System.Drawing.Point(3, 3);
            this.adminAddBtn.Name = "adminAddBtn";
            this.adminAddBtn.OuterBorderColor = System.Drawing.Color.Gray;
            this.adminAddBtn.Size = new System.Drawing.Size(75, 19);
            this.adminAddBtn.TabIndex = 1;
            this.adminAddBtn.Text = "관리자 등록";
            this.adminAddBtn.Click += new System.EventHandler(this.adminAddBtn_Click);
            // 
            // AdminSetting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(100)))));
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "AdminSetting";
            this.Size = new System.Drawing.Size(736, 620);
            this.Load += new System.EventHandler(this.AdminView_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Glass.GlassButton adminAddBtn;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        public SourceGrid.Grid adminGrid;

    }
}
