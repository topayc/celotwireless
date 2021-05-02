using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CelotMClient.Model;
using CelotMClient.CustomForm;
using CelotMClient.Worker;
using CelotMClient.Manager;
using CelotMClient.Model.NMS;
using CelotMClient.Util;

namespace CelotMClient.CustomView
{
    public partial class AdminSetting : UserControl
    {
        private string[] columnsTitles;
        public AdminDao adminDao;
        private ColumnInfo[] columnInfos;
        public List<AdminCommand> adminCommandList = new List<AdminCommand>();

        DevAge.Drawing.BorderLine border;
        DevAge.Drawing.RectangleBorder cellBorder;
        CellBackColorAlternate viewNormal;
        CheckBoxBackColorAlternate viewCheckBox;
        SourceGrid.Cells.Views.ColumnHeader viewColumnHeader;
        DevAge.Drawing.VisualElements.ColumnHeader backHeader;
        SourceGrid.Cells.Views.ColumnHeader viewColumnHeader1;
        DevAge.Drawing.VisualElements.ColumnHeader backHeader1;
        public AdminSetting()
        {
            InitializeComponent();
            columnsTitles = new string[]{
                "No", "GROUP", "NAME", "ID","PASSWORD","REG_DATE"
            };
            this.initColumnInfo(columnsTitles);

            //그리드와 관련한 필요 object 생성
            //Border
            border = new DevAge.Drawing.BorderLine(Color.Black, 1);
            cellBorder = new DevAge.Drawing.RectangleBorder(border, border);

            //Views
            viewNormal = new CellBackColorAlternate(Color.Khaki, Color.DarkKhaki);
            viewNormal.Border = cellBorder;
            viewNormal.Font = new Font("돋음", 8, FontStyle.Regular);
            viewCheckBox = new CheckBoxBackColorAlternate(Color.Khaki, Color.DarkKhaki);
            viewCheckBox.Border = cellBorder;

            //ColumnHeader view
            viewColumnHeader = new SourceGrid.Cells.Views.ColumnHeader();
            backHeader = new DevAge.Drawing.VisualElements.ColumnHeader();
            backHeader.BackColor = Color.DimGray;
            //backHeader.Border = DevAge.Drawing.RectangleBorder.NoBorder;
            viewColumnHeader.Background = backHeader;
            viewColumnHeader.ForeColor = Color.White;
            viewColumnHeader.Font = new Font("맑은 고딕", 10, FontStyle.Bold);

            viewColumnHeader1 = new SourceGrid.Cells.Views.ColumnHeader();
            backHeader1 = new DevAge.Drawing.VisualElements.ColumnHeader();
            backHeader1.BackColor = Color.DarkSlateGray;
            viewColumnHeader1.Background = backHeader1;
            //viewColumnHeader1.Border = cellBorder; 
            viewColumnHeader1.ForeColor = Color.White;
            viewColumnHeader1.Font = new Font("굴림", 8, FontStyle.Regular);
            viewColumnHeader1.TextAlignment = DevAge.Drawing.ContentAlignment.MiddleCenter;

            adminGrid.BorderStyle = BorderStyle.FixedSingle;
            adminGrid.Dock = DockStyle.Fill;
            
            adminDao = new AdminDao(new NotifyDBfinishedHandler(this.NotifyDBfinishedHandler));
        }

        private void NotifyDBfinishedHandler(object Sender, DBFinishedEventArgs e)
        {
            CelotApplication.Instance().ViewTransferEnable = true;          
            this.Visible = false;
            switch(this.adminDao.QueryMode){
                case QueryMode.SelectRows:
                this.adminCommandList = (List<AdminCommand>)e.Result;
                if(this.adminCommandList.Count > 0)
                {
                    this.initAdminGridColumn();
                    foreach (AdminCommand adminCommand in this.adminCommandList)
                    {
                        this.AddRowToAdminGrid(adminCommand);
                    }
                }
                else
                {
                    this.adminGrid.Visible = false;
                }
         
                 break;
                case QueryMode.Delete:
                case QueryMode.Update:
                case QueryMode.Insert:
                    this.adminDao.GetAdminCommands();
                    break;
            }
            this.Visible = true;
        }

        private void initColumnInfo(string[] columnsTitles)
        {
            columnInfos = new ColumnInfo[columnsTitles.Length];
            for (int i = 0; i < columnsTitles.Length; i++)
            {
                columnInfos[i] = new ColumnInfo(columnsTitles[i], true);
            }
        }

        private void AdminView_Load(object sender, EventArgs e)
        {
            this.Visible = false;
            adminDao.GetAdminCommands();
        }

        private void initAdminGridColumn()
        {
            this.adminGrid.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.adminGrid.Rows.Clear();
            this.adminGrid.SuspendLayout();
            this.adminGrid.ColumnsCount = this.columnInfos.Length;
            this.adminGrid.FixedRows = 1;
            SourceGrid.Cells.ColumnHeader columnHeader;
            this.adminGrid.Rows.Insert(0);
            for (int i = 0; i < this.columnInfos.Length; i++)
            {
                columnHeader = new SourceGrid.Cells.ColumnHeader(columnInfos[i].ColumnName);
                columnHeader.View = viewColumnHeader1;
                this.adminGrid[0, i] = columnHeader;
            }
            this.adminGrid.ResumeLayout();
            this.adminGrid.AutoSizeCells();
        }

        private void AddRowToAdminGrid(AdminCommand adminCommand)
        {
            PopupMenu menuController = new PopupMenu(this);   
            int insertRowCount = this.adminGrid.Rows.Count;
            adminGrid.Rows.Insert(insertRowCount);

            SourceGrid.Cells.Views.ColumnHeader nameHeaderView1 = new SourceGrid.Cells.Views.ColumnHeader();
            DevAge.Drawing.VisualElements.ColumnHeader namebackHeader1 = new DevAge.Drawing.VisualElements.ColumnHeader();
            namebackHeader1.BackColor = Color.DarkSlateGray;
            nameHeaderView1.Background = namebackHeader1;
            nameHeaderView1.Border = cellBorder;
            nameHeaderView1.ForeColor = Color.White;
            nameHeaderView1.Font = new Font("굴림", 8, FontStyle.Regular);
            nameHeaderView1.TextAlignment = DevAge.Drawing.ContentAlignment.MiddleCenter;

            adminGrid[insertRowCount, 0] = new SourceGrid.Cells.Cell(adminCommand.AdminNo);
            adminGrid[insertRowCount, 0].View = viewNormal;
            adminGrid[insertRowCount, 0].AddController(menuController);

            adminGrid[insertRowCount, 1] = new SourceGrid.Cells.Cell(adminCommand.AdminGroupName);
            adminGrid[insertRowCount, 1].View = viewNormal;
            adminGrid[insertRowCount, 1].AddController(menuController);

            adminGrid[insertRowCount, 2] = new SourceGrid.Cells.Cell(adminCommand.Name);
            adminGrid[insertRowCount, 2].View = viewNormal;
            adminGrid[insertRowCount, 2].AddController(menuController);

            adminGrid[insertRowCount, 3] = new SourceGrid.Cells.Cell(adminCommand.Id);
            adminGrid[insertRowCount, 3].View = viewNormal;
            adminGrid[insertRowCount, 3].AddController(menuController);

            adminGrid[insertRowCount, 4] = new SourceGrid.Cells.Cell(adminCommand.Password);
            adminGrid[insertRowCount, 4].View = viewNormal;
            adminGrid[insertRowCount, 4].AddController(menuController);

            adminGrid[insertRowCount, 5] = new SourceGrid.Cells.Cell(adminCommand.AdminRegDate);
            adminGrid[insertRowCount, 5].View = viewNormal;
            adminGrid[insertRowCount, 5].AddController(menuController);
        }

        public void DeleteAdmin(int row, int col)
        {
            int adminNo = Convert.ToInt32(this.adminGrid[row, 0].DisplayText);
            Admin admin = new Admin();
            admin.AdminNo = adminNo;
            this.adminDao.DeleteAdmin(admin);
        }

        public void ModifyAdmin()
        {
           
        }

        private void adminAddBtn_Click(object sender, EventArgs e)
        {
            this.CreateAdmin();
        }

        private void CreateAdmin()
        {
            AdminRegistration adminForm = new AdminRegistration("관리자 등록", Constants.CREATE, null);
            adminForm.StartPosition = FormStartPosition.CenterParent;
            if (adminForm.ShowDialog() == DialogResult.OK)
            {
                Admin addAdmin = adminForm.Admin;
                addAdmin.AdminRegDate = CelotUtility.GetCurrentTimeString();
                this.adminDao.CreateAdmin(addAdmin);
            }
        }

        private class CellBackColorAlternate : SourceGrid.Cells.Views.Cell
        {
            public CellBackColorAlternate(Color firstColor, Color secondColor)
            {
                FirstBackground = new DevAge.Drawing.VisualElements.BackgroundSolid(firstColor);
                SecondBackground = new DevAge.Drawing.VisualElements.BackgroundSolid(secondColor);
            }

            private DevAge.Drawing.VisualElements.IVisualElement mFirstBackground;
            public DevAge.Drawing.VisualElements.IVisualElement FirstBackground
            {
                get { return mFirstBackground; }
                set { mFirstBackground = value; }
            }

            private DevAge.Drawing.VisualElements.IVisualElement mSecondBackground;
            public DevAge.Drawing.VisualElements.IVisualElement SecondBackground
            {
                get { return mSecondBackground; }
                set { mSecondBackground = value; }
            }

            protected override void PrepareView(SourceGrid.CellContext context)
            {
                base.PrepareView(context);

                if (Math.IEEERemainder(context.Position.Row, 2) == 0)
                    Background = FirstBackground;
                else
                    Background = SecondBackground;
            }
        }

        private class CheckBoxBackColorAlternate : SourceGrid.Cells.Views.CheckBox
        {
            public CheckBoxBackColorAlternate(Color firstColor, Color secondColor)
            {
                FirstBackground = new DevAge.Drawing.VisualElements.BackgroundSolid(firstColor);
                SecondBackground = new DevAge.Drawing.VisualElements.BackgroundSolid(secondColor);
            }

            private DevAge.Drawing.VisualElements.IVisualElement mFirstBackground;
            public DevAge.Drawing.VisualElements.IVisualElement FirstBackground
            {
                get { return mFirstBackground; }
                set { mFirstBackground = value; }
            }

            private DevAge.Drawing.VisualElements.IVisualElement mSecondBackground;
            public DevAge.Drawing.VisualElements.IVisualElement SecondBackground
            {
                get { return mSecondBackground; }
                set { mSecondBackground = value; }
            }

            protected override void PrepareView(SourceGrid.CellContext context)
            {
                base.PrepareView(context);

                if (Math.IEEERemainder(context.Position.Row, 2) == 0)
                    Background = FirstBackground;
                else
                    Background = SecondBackground;
            }
        }

        public class PopupMenu : SourceGrid.Cells.Controllers.ControllerBase
        {
            ContextMenu menu = new ContextMenu();
            AdminSetting adminSetting;
            SourceGrid.CellContext celContext;

            public PopupMenu(AdminSetting adminSetting)
            {
                this.adminSetting = adminSetting;
                menu.MenuItems.Add("관리자 생성", new EventHandler(add_Click));
                menu.MenuItems.Add("관리자 수정", new EventHandler(Modify_Click));
                menu.MenuItems.Add("관리자 삭제", new EventHandler(Delete_Click));
            }

            private void add_Click(object sender, EventArgs e)
            {
                this.adminSetting.CreateAdmin();  
            }

            public override void OnMouseUp(SourceGrid.CellContext sender, MouseEventArgs e)
            {
                base.OnMouseUp(sender, e);

                if (e.Button == MouseButtons.Right)
                {
                    this.celContext = sender;
                    menu.Show(sender.Grid, new Point(e.X, e.Y));
                }
            }

            private void Modify_Click(object sender, EventArgs e)
            {
                int row = celContext.Position.Row;
                AdminCommand adminCommand = this.adminSetting.adminCommandList[row - 1];
                Admin admin = new Admin();
                admin.AdminNo = adminCommand.AdminNo;
                admin.AdminGroupNo = adminCommand.AdminGroupNo;
                admin.Name = adminCommand.Name;
                admin.Id = adminCommand.Id;
                admin.Password = adminCommand.Password;
                AdminRegistration adminReg = new AdminRegistration("관리자 수정", Constants.MODIFY, admin);
                if (adminReg.ShowDialog() == DialogResult.OK)
                {
                    this.adminSetting.adminDao.ModifyAdmin(admin);
                };
               
            }
            private void Delete_Click(object sender, EventArgs e)
            {
                if (MessageBox.Show(celContext.DisplayText + " 님을 정말로 삭제하시겠습니까?","관리자 삭제",MessageBoxButtons.YesNo) == DialogResult.Yes){
                    this.adminSetting.DeleteAdmin(celContext.Position.Row, celContext.Position.Column);
                }
            }
        }
    }
}
