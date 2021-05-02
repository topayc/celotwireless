using CelotMClient.CustomView;
using CelotMClient.Manager;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CelotMClient.CustomForm
{
    public partial class ColumnSelection : Form
    {
        private SourceGrid.Grid grid;
        private CustomView.ColumnInfo[] columnInfos;
        private bool isMouseDown = false;
        private int xLast;
        private int yLast;
        private Object obj;
        private string p;
        private Dictionary<string, int> columnMap;

        public ColumnSelection()
        {
            InitializeComponent();
        }


        public ColumnSelection(Object device)
        {
            InitializeComponent();
            // TODO: Complete member initialization
        }

        public ColumnSelection(Object obj, ColumnInfo[] columnInfos,string p)
        {
         
            InitializeComponent();
            // TODO: Complete member initialization
            this.p = p;
            this.obj = obj;
            this.columnInfos = columnInfos;

            if (p.Equals("alert"))
            {
                this.grid = ((Alerts)this.obj).alertGrid;
                this.columnMap = ApplicationConfig.Instance().AlertColumnSetting;
            }
            if (p.Equals("device")){
                 this.grid = ((Device)this.obj).deviceGrid;
                 this.columnMap = ApplicationConfig.Instance().DeviceColumnSetting;

            }
        }


        private void ColumnSelection_Load(object sender, EventArgs e)
        {
            SuspendLayout();
            CheckBox[] columnCheckBoxes = new CheckBox[columnInfos.Length];
            for (int i = 0; i < columnInfos.Length; i++)
            {
                columnCheckBoxes[i] = new CheckBox();
                columnCheckBoxes[i].Font = new Font("맑은 고딕", 8, FontStyle.Bold);
                columnCheckBoxes[i].Dock = DockStyle.Fill;
                columnCheckBoxes[i].Name = "checkBox_" + i.ToString()+ "_" + columnInfos[i].ColumnName;
                columnCheckBoxes[i].Text = columnInfos[i].ColumnName;
                columnCheckBoxes[i].Checked = this.columnMap[columnInfos[i].ColumnName] == 1 ? true : false;
                columnCheckBoxes[i].CheckedChanged += new EventHandler(this.checkbox_checkedChange);
                tableLayoutPanel1.Controls.Add(columnCheckBoxes[i]);
            }
            ResumeLayout();
        }
        private void checkbox_checkedChange(object sender, EventArgs e)
        {
            CheckBox check = (CheckBox)sender;
            string[] tmp = check.Name.Split('_');
            int index = Convert.ToInt32(tmp[1]);
            string columName = tmp[2];
            grid.Columns[index].Visible = check.Checked;
            grid.ResumeLayout();
            
            this.columnMap[columName] = check.Checked ? 1 : 0;
            this.SetColumnSettings();
        }

        public void SetColumnSettings()
        {
            if (p.Equals("alert"))
            {
                ApplicationConfig.Instance().AlertColumnSetting = this.columnMap;
            }
            if (p.Equals("device"))
            {
                ApplicationConfig.Instance().DeviceColumnSetting = this.columnMap;

            }
        }
        private void panel2_MouseClick(object sender, MouseEventArgs e)
        {
            this.SetColumnSettings();
            Close();
        }

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (isMouseDown)
            {
                int newY = this.Top + (e.Y - yLast);
                int newX = this.Left + (e.X - xLast);
                this.Location = new Point(newX, newY);
            }
        }

        private void panel1_MouseUp(object sender, MouseEventArgs e)
        {
            isMouseDown = false;
            xLast = e.X;
            yLast = e.Y;
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            isMouseDown = true;
            yLast = e.Y;
            xLast = e.X;
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

    }
}
