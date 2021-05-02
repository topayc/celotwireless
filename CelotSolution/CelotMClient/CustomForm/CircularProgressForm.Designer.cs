namespace CelotMClient.CustomForm
{
    partial class CircularProgressForm
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
            this.optimizedCircularProgressControl1 = new OptProgressControl.OptimizedCircularProgressControl();
            this.SuspendLayout();
            // 
            // optimizedCircularProgressControl1
            // 
            this.optimizedCircularProgressControl1.BackColor = System.Drawing.Color.Transparent;
            this.optimizedCircularProgressControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.optimizedCircularProgressControl1.Interval = 60;
            this.optimizedCircularProgressControl1.Location = new System.Drawing.Point(0, 0);
            this.optimizedCircularProgressControl1.MinimumSize = new System.Drawing.Size(28, 28);
            this.optimizedCircularProgressControl1.Name = "optimizedCircularProgressControl1";
            this.optimizedCircularProgressControl1.Rotation = OptProgressControl.OptimizedCircularProgressControl.Direction.CLOCKWISE;
            this.optimizedCircularProgressControl1.Size = new System.Drawing.Size(50, 50);
            this.optimizedCircularProgressControl1.StartAngle = 30F;
            this.optimizedCircularProgressControl1.TabIndex = 0;
            this.optimizedCircularProgressControl1.TickColor = System.Drawing.Color.White;
            // 
            // CircularProgressForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.ClientSize = new System.Drawing.Size(50, 50);
            this.Controls.Add(this.optimizedCircularProgressControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "CircularProgressForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "CircularProgressForm";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.CircularProgressForm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private OptProgressControl.OptimizedCircularProgressControl optimizedCircularProgressControl1;
    }
}