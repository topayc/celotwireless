namespace CelotMClient.CustomForm
{
    partial class AlertExelForm
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.saveFileBox = new System.Windows.Forms.TextBox();
            this.saveFileDialogBtn = new Glass.GlassButton();
            this.closeBtn = new Glass.GlassButton();
            this.alertGrid = new SourceGrid.Grid();
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.tableLayoutPanel1.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.flowLayoutPanel1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.alertGrid, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 41F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(940, 511);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.saveFileBox);
            this.flowLayoutPanel1.Controls.Add(this.saveFileDialogBtn);
            this.flowLayoutPanel1.Controls.Add(this.closeBtn);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(3, 3);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(934, 35);
            this.flowLayoutPanel1.TabIndex = 0;
            // 
            // saveFileBox
            // 
            this.saveFileBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(140)))), ((int)(((byte)(140)))), ((int)(((byte)(140)))));
            this.saveFileBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.saveFileBox.ForeColor = System.Drawing.Color.White;
            this.saveFileBox.Location = new System.Drawing.Point(3, 6);
            this.saveFileBox.Margin = new System.Windows.Forms.Padding(3, 6, 3, 3);
            this.saveFileBox.Name = "saveFileBox";
            this.saveFileBox.Size = new System.Drawing.Size(306, 21);
            this.saveFileBox.TabIndex = 0;
            // 
            // saveFileDialogBtn
            // 
            this.saveFileDialogBtn.Font = new System.Drawing.Font("맑은 고딕", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.saveFileDialogBtn.Location = new System.Drawing.Point(315, 6);
            this.saveFileDialogBtn.Margin = new System.Windows.Forms.Padding(3, 6, 3, 3);
            this.saveFileDialogBtn.Name = "saveFileDialogBtn";
            this.saveFileDialogBtn.OuterBorderColor = System.Drawing.Color.DarkGray;
            this.saveFileDialogBtn.Size = new System.Drawing.Size(52, 21);
            this.saveFileDialogBtn.TabIndex = 1;
            this.saveFileDialogBtn.Text = "저장";
            this.saveFileDialogBtn.Click += new System.EventHandler(this.saveFileDialogBtn_Click);
            // 
            // closeBtn
            // 
            this.closeBtn.Font = new System.Drawing.Font("맑은 고딕", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.closeBtn.Location = new System.Drawing.Point(373, 6);
            this.closeBtn.Margin = new System.Windows.Forms.Padding(3, 6, 3, 3);
            this.closeBtn.Name = "closeBtn";
            this.closeBtn.OuterBorderColor = System.Drawing.Color.DarkGray;
            this.closeBtn.Size = new System.Drawing.Size(52, 21);
            this.closeBtn.TabIndex = 2;
            this.closeBtn.Text = "닫기";
            this.closeBtn.Click += new System.EventHandler(this.closeBtn_Click);
            // 
            // alertGrid
            // 
            this.alertGrid.AutoStretchColumnsToFitWidth = true;
            this.alertGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.alertGrid.EnableSort = false;
            this.alertGrid.FixedRows = 1;
            this.alertGrid.Location = new System.Drawing.Point(3, 44);
            this.alertGrid.Name = "alertGrid";
            this.alertGrid.OptimizeMode = SourceGrid.CellOptimizeMode.ForRows;
            this.alertGrid.SelectionMode = SourceGrid.GridSelectionMode.Row;
            this.alertGrid.Size = new System.Drawing.Size(934, 464);
            this.alertGrid.TabIndex = 1;
            this.alertGrid.TabStop = true;
            this.alertGrid.ToolTipText = "";
            // 
            // AlertExelForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(100)))));
            this.ClientSize = new System.Drawing.Size(940, 511);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "AlertExelForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "AlertExelForm";
            this.Load += new System.EventHandler(this.AlertExelForm_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.TextBox saveFileBox;
        private Glass.GlassButton saveFileDialogBtn;
        private SourceGrid.Grid alertGrid;
        private System.Windows.Forms.SaveFileDialog saveFileDialog;
        private Glass.GlassButton closeBtn;
    }
}