using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CelotMClient.Worker;
using CelotMClient.Model;
using CelotMClient.Util;

namespace CelotMClient.CustomView
{
    public partial class Sms : UserControl
    {
        private string[] smsAppColumns;
        private ColumnInfo[] smsAppColumnInfos;

        private string[] smsDetailRecordColumns;
        private ColumnInfo[] smsDetailRecordColumnInfos;

        public Sms()
        {
            InitializeComponent();
            smsAppColumns = new string[]{
                "Name", "Description", "Group", "Number","RouterIP","Received","Send","Status"
            };
            smsDetailRecordColumns = new string[]{
                "Number", "Phone Number", "Time", "Received Bos / Send Box"
            };
            this.initColumnInfo();
        }

        private void Sms_Load(object sender, EventArgs e)
        {
            this.Visible = false;
            this.setSmsAppGrid();
            this.Visible = true;
        }

        private void initColumnInfo()
        {
            smsAppColumnInfos = new ColumnInfo[smsAppColumns.Length];
            for (int i = 0; i < smsAppColumns.Length; i++)
            {
                smsAppColumnInfos[i] = new ColumnInfo(smsAppColumns[i], true);
            }

            smsDetailRecordColumnInfos = new ColumnInfo[smsAppColumns.Length];
            for (int i = 0; i < smsDetailRecordColumns.Length; i++)
            {
                smsDetailRecordColumnInfos[i] = new ColumnInfo(smsDetailRecordColumns[i], true);
            }
        }
        public void setSmsAppGrid(){
            List<SmsModel> smsList = ApplicationManager.getDummySmsList();
            DevAge.Drawing.BorderLine border = new DevAge.Drawing.BorderLine(Color.Black, 1);
            DevAge.Drawing.RectangleBorder cellBorder = new DevAge.Drawing.RectangleBorder(border, border);

            PopupMenu menuController = new PopupMenu();

            //Views
            CellBackColorAlternate viewNormal = new CellBackColorAlternate(Color.FromArgb(255, 210, 210, 210), Color.FromArgb(255, 240, 240, 240));
            viewNormal.Border = cellBorder;
            viewNormal.Font = new Font("돋음", 8, FontStyle.Regular);
            CheckBoxBackColorAlternate viewCheckBox = new CheckBoxBackColorAlternate(Color.Khaki, Color.DarkKhaki);
            viewCheckBox.Border = cellBorder;

            //ColumnHeader view
            SourceGrid.Cells.Views.ColumnHeader viewColumnHeader = new SourceGrid.Cells.Views.ColumnHeader();
            DevAge.Drawing.VisualElements.ColumnHeader backHeader = new DevAge.Drawing.VisualElements.ColumnHeader();
            backHeader.BackColor = Color.DimGray;
            //backHeader.Border = DevAge.Drawing.RectangleBorder.NoBorder;
            viewColumnHeader.Background = backHeader;
            viewColumnHeader.ForeColor = Color.White;
            viewColumnHeader.Font = new Font("맑은 고딕", 10, FontStyle.Bold);

            SourceGrid.Cells.Views.ColumnHeader viewColumnHeader1 = new SourceGrid.Cells.Views.ColumnHeader();
            DevAge.Drawing.VisualElements.ColumnHeader backHeader1 = new DevAge.Drawing.VisualElements.ColumnHeader();
            backHeader1.BackColor = Color.DarkSlateGray;
            viewColumnHeader1.Background = backHeader1;
            //viewColumnHeader1.Border = cellBorder; 
            viewColumnHeader1.ForeColor = Color.White;
            viewColumnHeader1.Font = new Font("굴림", 8, FontStyle.Regular);
            viewColumnHeader1.TextAlignment = DevAge.Drawing.ContentAlignment.MiddleCenter;

            //Editors
            SourceGrid.Cells.Editors.TextBox editorString = new SourceGrid.Cells.Editors.TextBox(typeof(string));
            SourceGrid.Cells.Editors.TextBoxUITypeEditor editorDateTime = new SourceGrid.Cells.Editors.TextBoxUITypeEditor(typeof(DateTime));


            //Create the grid
            smsAppGrid.BorderStyle = BorderStyle.FixedSingle;

            smsAppGrid.ColumnsCount = 8;
            smsAppGrid.FixedRows = 1;
            smsAppGrid.Rows.Insert(0);

            SourceGrid.Cells.ColumnHeader columnHeader;
            for (int i = 0; i < smsAppColumns.Length; i++)
            {
                columnHeader = new SourceGrid.Cells.ColumnHeader(smsAppColumns[i]);
                columnHeader.View = viewColumnHeader1;
                smsAppGrid[0, i] = columnHeader;
            }
            int length = smsList.Count;
            Random random = new Random();
            CellClickEvent clickController = new CellClickEvent(this);
            for (int r = 1; r < smsList.Count; r++)
            {
                smsAppGrid.Rows.Insert(r);
                SourceGrid.Cells.Views.ColumnHeader nameHeaderView1 = new SourceGrid.Cells.Views.ColumnHeader();
                DevAge.Drawing.VisualElements.ColumnHeader namebackHeader1 = new DevAge.Drawing.VisualElements.ColumnHeader();
                namebackHeader1.BackColor = Color.DarkSlateGray;
                nameHeaderView1.Background = namebackHeader1;
                nameHeaderView1.Border = cellBorder;
                nameHeaderView1.ForeColor = Color.White;
                nameHeaderView1.Font = new Font("굴림", 8, FontStyle.Regular);
                nameHeaderView1.TextAlignment = DevAge.Drawing.ContentAlignment.MiddleCenter;

                SmsModel smsModel = smsList[r - 1];
                smsAppGrid[r, 0] = new SourceGrid.Cells.Cell(smsModel.Name);
                smsAppGrid[r, 0].View = viewNormal;
                smsAppGrid[r, 0].AddController(menuController);
                smsAppGrid[r, 0].AddController(clickController);

                smsAppGrid[r, 1] = new SourceGrid.Cells.Cell(smsModel.Description);
                smsAppGrid[r, 1].View = viewNormal;
                smsAppGrid[r, 1].AddController(menuController);
                smsAppGrid[r, 1].AddController(clickController);

                smsAppGrid[r, 2] = new SourceGrid.Cells.Cell(smsModel.Group);
                smsAppGrid[r, 2].View = viewNormal;
                smsAppGrid[r, 2].AddController(menuController);
                smsAppGrid[r, 2].AddController(clickController);

                smsAppGrid[r, 3] = new SourceGrid.Cells.Cell(smsModel.Number);
                smsAppGrid[r, 3].View = viewNormal;
                smsAppGrid[r, 3].AddController(menuController);
                smsAppGrid[r, 3].AddController(clickController);

                smsAppGrid[r, 4] = new SourceGrid.Cells.Cell(smsModel.RouterIp);
                smsAppGrid[r, 4].View = viewNormal;
                smsAppGrid[r, 4].AddController(menuController);
                smsAppGrid[r, 4].AddController(clickController);

                smsAppGrid[r, 5] = new SourceGrid.Cells.Cell(smsModel.Receive);
                smsAppGrid[r, 5].View = viewNormal;
                smsAppGrid[r, 5].AddController(menuController);
                smsAppGrid[r, 5].AddController(clickController);

                smsAppGrid[r, 6] = new SourceGrid.Cells.Cell(smsModel.Send);
                smsAppGrid[r, 6].View = viewNormal;
                smsAppGrid[r, 6].AddController(menuController);
                smsAppGrid[r, 6].AddController(clickController);

                smsAppGrid[r, 7] = new SourceGrid.Cells.Cell(smsModel.Status);
                smsAppGrid[r, 7].View = viewNormal;
                smsAppGrid[r, 7].AddController(menuController);
                smsAppGrid[r, 7].AddController(clickController);
            }
           
        }

        public void setSmsDetailRecordGrid(int row, int col)
        {
            smsListGrid.Controls.Clear();
            smsListGrid.Redim(0, 4);
            
            List<SmsDetailRecord> smsDetailRecordList = ApplicationManager.getDummySmsDetailRecordList();
            DevAge.Drawing.BorderLine border = new DevAge.Drawing.BorderLine(Color.Black, 1);
            DevAge.Drawing.RectangleBorder cellBorder = new DevAge.Drawing.RectangleBorder(border, border);

            PopupMenu menuController = new PopupMenu();
          
            //Views
            CellBackColorAlternate viewNormal = new CellBackColorAlternate(Color.FromArgb(255, 210, 210, 210), Color.FromArgb(255, 240, 240, 240));
            viewNormal.Border = cellBorder;
            viewNormal.Font = new Font("돋음", 8, FontStyle.Regular);
            CheckBoxBackColorAlternate viewCheckBox = new CheckBoxBackColorAlternate(Color.Khaki, Color.DarkKhaki);
            viewCheckBox.Border = cellBorder;

            //ColumnHeader view
            SourceGrid.Cells.Views.ColumnHeader viewColumnHeader = new SourceGrid.Cells.Views.ColumnHeader();
            DevAge.Drawing.VisualElements.ColumnHeader backHeader = new DevAge.Drawing.VisualElements.ColumnHeader();
            backHeader.BackColor = Color.DimGray;
            //backHeader.Border = DevAge.Drawing.RectangleBorder.NoBorder;
            viewColumnHeader.Background = backHeader;
            viewColumnHeader.ForeColor = Color.White;
            viewColumnHeader.Font = new Font("맑은 고딕", 10, FontStyle.Bold);

            SourceGrid.Cells.Views.ColumnHeader viewColumnHeader1 = new SourceGrid.Cells.Views.ColumnHeader();
            DevAge.Drawing.VisualElements.ColumnHeader backHeader1 = new DevAge.Drawing.VisualElements.ColumnHeader();
            backHeader1.BackColor = Color.DarkSlateGray;
            viewColumnHeader1.Background = backHeader1;
            //viewColumnHeader1.Border = cellBorder; 
            viewColumnHeader1.ForeColor = Color.White;
            viewColumnHeader1.Font = new Font("굴림", 8, FontStyle.Regular);
            viewColumnHeader1.TextAlignment = DevAge.Drawing.ContentAlignment.MiddleCenter;

            //Editors
            SourceGrid.Cells.Editors.TextBox editorString = new SourceGrid.Cells.Editors.TextBox(typeof(string));
            SourceGrid.Cells.Editors.TextBoxUITypeEditor editorDateTime = new SourceGrid.Cells.Editors.TextBoxUITypeEditor(typeof(DateTime));


            //Create the grid
            smsListGrid.BorderStyle = BorderStyle.FixedSingle;
            smsListGrid.ColumnsCount = 4;
            smsListGrid.FixedRows = 1;
            smsListGrid.Rows.Insert(0);

            SourceGrid.Cells.ColumnHeader columnHeader;
            for (int i = 0; i < smsDetailRecordColumns.Length; i++)
            {
                columnHeader = new SourceGrid.Cells.ColumnHeader(smsDetailRecordColumns[i]);
                columnHeader.View = viewColumnHeader1;
                smsListGrid[0, i] = columnHeader;
            }
            int length = smsDetailRecordList.Count;
            Random random = new Random();
            CellClickEvent clickController = new CellClickEvent(this);
            for (int r = 1; r < smsDetailRecordList.Count; r++)
            {
                smsListGrid.Rows.Insert(r);
                SourceGrid.Cells.Views.ColumnHeader nameHeaderView1 = new SourceGrid.Cells.Views.ColumnHeader();
                DevAge.Drawing.VisualElements.ColumnHeader namebackHeader1 = new DevAge.Drawing.VisualElements.ColumnHeader();
                namebackHeader1.BackColor = Color.DarkSlateGray;
                nameHeaderView1.Background = namebackHeader1;
                nameHeaderView1.Border = cellBorder;
                nameHeaderView1.ForeColor = Color.White;
                nameHeaderView1.Font = new Font("굴림", 8, FontStyle.Regular);
                nameHeaderView1.TextAlignment = DevAge.Drawing.ContentAlignment.MiddleCenter;

                SmsDetailRecord smsModel = smsDetailRecordList[r - 1];
                smsListGrid[r, 0] = new SourceGrid.Cells.Cell(smsModel.Number.ToString());
                smsListGrid[r, 0].View = viewNormal;

                smsListGrid[r, 1] = new SourceGrid.Cells.Cell(smsModel.PhoneNumber);
                smsListGrid[r, 1].View = viewNormal;

                smsListGrid[r, 2] = new SourceGrid.Cells.Cell(smsModel.Time);
                smsListGrid[r, 2].View = viewNormal;

                smsListGrid[r, 3] = new SourceGrid.Cells.Cell(smsModel.RSBox);
                smsListGrid[r, 3].View = viewNormal;
            }
        }

       

        public class CellClickEvent : SourceGrid.Cells.Controllers.ControllerBase
        {
            Sms sms;
            bool isClicking = false;
            public CellClickEvent(Sms sms)
            {
                this.sms = sms;
            }

            public override void OnMouseDown(SourceGrid.CellContext sender, MouseEventArgs e)
            {
                base.OnMouseUp(sender, e);
                if (e.Button == MouseButtons.Left)
                {
                    isClicking = true;
                }
            }

            public override void OnClick(SourceGrid.CellContext sender, EventArgs e)
            {
                base.OnClick(sender, e);
                int row = sender.Position.Row;
                int column = sender.Position.Column;
                if (isClicking)
                {
                    sms.setSmsDetailRecordGrid(row, column);
                    isClicking = false;
                }
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
            SourceGrid.CellContext celContext;
            public PopupMenu()
            {
                menu.MenuItems.Add("Send SMS Message", new EventHandler(Send_SMS_Click));
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

            private void Send_SMS_Click(object sender, EventArgs e)
            {
                MessageBox.Show(celContext.DisplayText + " SMS 메시지를 빌송합니다");

            }
            private void Menu2_Click(object sender, EventArgs e)
            {
                if (MessageBox.Show(celContext.DisplayText + " 님을 정말로 삭제하시겠습니까?", "관리자 삭제", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    MessageBox.Show("삭제하였습니다");
                }

            }
        }

      

     


    }
}
