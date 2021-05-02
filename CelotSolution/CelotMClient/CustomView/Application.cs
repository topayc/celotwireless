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
using CelotMClient.Worker;
using System.IO;
using CelotMClient.CustomForm;
using System.Diagnostics;
using CelotMClient.CustomControll;
using CelotMClient.Manager;
using CelotMClient.Util;

namespace CelotMClient.CustomView
{
    public partial class Application : UserControl
    {
        private string[] columnsTitles;
        private ColumnInfo[] columnInfos;
        public DownloadManager downloadManager;

        DevAge.Drawing.BorderLine border;
        DevAge.Drawing.RectangleBorder cellBorder;
        CellBackColorAlternate viewNormal;
        CheckBoxBackColorAlternate viewCheckBox;
        SourceGrid.Cells.Views.ColumnHeader viewColumnHeader;
        DevAge.Drawing.VisualElements.ColumnHeader backHeader;
        SourceGrid.Cells.Views.ColumnHeader viewColumnHeader1;
        DevAge.Drawing.VisualElements.ColumnHeader backHeader1;

        protected override void Dispose(bool disposing)
        {
             if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        protected override void OnHandleDestroyed(EventArgs e)
        {
            this.downloadManager.Save();
            ApplicationConfig.Instance().Reload();
            base.OnHandleDestroyed(e);
        }

        public Application()
        {
            InitializeComponent();
            this.Dock = DockStyle.Fill;
            columnsTitles = new string[]{
                 "Activate", "No","Router IP","Download File","Download Status","Success / Fall" 
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

            downloadGrid.BorderStyle = BorderStyle.FixedSingle;
            downloadGrid.Dock = DockStyle.Fill;
        }

        private void initColumnInfo(string[] columnsTitles)
        {
            columnInfos = new ColumnInfo[columnsTitles.Length];
            for (int i = 0; i < columnsTitles.Length; i++)
            {
                columnInfos[i] = new ColumnInfo(columnsTitles[i], true);
            }
        }

        private void Application_Load(object sender, EventArgs e)
        {
            this.downloadManager = new DownloadManager(this);
            //this.downloadManager.ConcurentDownloadCount = ApplicationConfig.Instance().ConcurrentDownloadCount;
            this.Visible = false;
            this.SuspendLayout();
            this.InitControl();
            this.ResumeLayout();
            this.Visible = true;
            CelotApplication.Instance().ViewTransferEnable = true;
            

            if (CelotApplication.Instance().ViewIntent != null)
            {
                String ip = (string)CelotApplication.Instance().ViewIntent.Data;
                this.downloadManager.ClearDownloadMap();
                this.downloadManager.add(ip);
                if (this.downloadManager.DownloadFileName != null && !"".Equals(this.downloadManager.DownloadFileName) &&
                  this.downloadManager.DownloadFileName.Length != 0)
                {
                    this.downloadManager.SetAllDownloadFile(this.downloadManager.DownloadFileName);
                }
                this.initDownloadInfoPanel();
                this.setDataToDownloadGrid();
                CelotApplication.Instance().ViewIntent = null;
            }
        }

        public void InitControl()
        {
            this.SuspendLayout();
            this.initDownloadInfoPanel();
            this.initDownloadGridColumn();
            this.downloadTypeCombo.SelectedIndex = 0;
            this.ResumeLayout();
        }


        /*
         * this method called from the DownloadManager when downloads finished
        */
         public void DownloadCompleted(bool controlInit)
        {
            ApplicationConfig.Instance().Reload();
            if (controlInit) this.InitControl();
        }
       

        private void setDataToDownloadGrid()
        {
            this.downloadGrid.Rows.Clear();
            if (this.downloadManager.getDownloadMap().Count > 0)
            {
                this.initDownloadGridColumn();
                foreach (KeyValuePair<string, Download> temp in this.downloadManager.getDownloadMap())
                {
                    this.addRowToDownloadGrid(temp.Value);
                }
            }
            this.downloadGrid.AutoSizeCells();
            this.downloadGrid.FixedRows = 1;
        }

        private void initDownloadInfoPanel()
        {
            this.ipsFileNameLabel.Text = this.downloadManager.IpListFileName;
            this.versionBox.Text = this.downloadManager.Version;
            this.hostIpBox.Text = this.downloadManager.HostIP;
            this.concurrentDownCount.Text = this.downloadManager.ConcurentDownloadCount.ToString();
            this.fileNameBox.Text = this.downloadManager.DownloadFileName;
            this.fileSizeLabel.Text = this.downloadManager.FileLength == 0 ? "" : this.downloadManager.FileLength.ToString();
            this.adminIdBox.Text = this.downloadManager.AdminId;
            this.adminPaswordBox.Text = this.downloadManager.AdminPassword;
            this.ftpIdBox.Text = this.downloadManager.FtpId;
            this.ftpPasswordBox.Text  = this.downloadManager.FtpPassword;
        }

        private void initDownloadGridColumn()
        {
            this.downloadGrid.Rows.Clear();
            this.downloadGrid.SuspendLayout();
            this.downloadGrid.ColumnsCount = this.columnInfos.Length;
            this.downloadGrid.FixedRows = 0;
            SourceGrid.Cells.ColumnHeader columnHeader;
            downloadGrid.Rows.Insert(0);
            for (int i = 0; i < this.columnInfos.Length; i++)
            {
                columnHeader = new SourceGrid.Cells.ColumnHeader(columnInfos[i].ColumnName);
                columnHeader.View = viewColumnHeader1;
                downloadGrid[0, i] = columnHeader;
            }
            this.downloadGrid.AutoSizeCells();
            this.downloadGrid.ResumeLayout();
           

        }

        #region 다운로드 그리드 행 추가
        private void addRowToDownloadGrid(Download download)
        {
            PopupMenu menuController = new PopupMenu(this);
            int insertRowCount = downloadGrid.Rows.Count;
            download.RowIndex = insertRowCount;

            downloadGrid.Rows.Insert(insertRowCount);
            downloadGrid[insertRowCount, 0] = new SourceGrid.Cells.CheckBox(null, download.DownloadEnable);
            downloadGrid[insertRowCount, 0].View = viewCheckBox;
            downloadGrid[insertRowCount, 0].AddController(menuController);
            downloadGrid[insertRowCount, 0].Controller.RemoveController(downloadGrid[insertRowCount, 0].FindController<SourceGrid.Cells.Controllers.CheckBox>());
            downloadGrid[insertRowCount, 0].Controller.AddController(new SourceGrid.Cells.Controllers.CheckBox(false));
            downloadGrid[insertRowCount, 0].FindController<SourceGrid.Cells.Controllers.CheckBox>().CheckedChanged +=
                   ChangeActivateCheckBox(insertRowCount);

            downloadGrid[insertRowCount, 1] = new SourceGrid.Cells.Cell(download.No);
            downloadGrid[insertRowCount, 1].View = viewNormal;
            downloadGrid[insertRowCount, 1].AddController(menuController);

            downloadGrid[insertRowCount, 2] = new SourceGrid.Cells.Cell(download.Ip);
            downloadGrid[insertRowCount, 2].View = viewNormal;
            downloadGrid[insertRowCount, 2].AddController(menuController);

            downloadGrid[insertRowCount, 3] = new SourceGrid.Cells.Cell(Path.GetFileName(download.DownloadFileName));
            downloadGrid[insertRowCount, 3].View = viewNormal;
            downloadGrid[insertRowCount, 3].AddController(menuController);

            ProgressBar progressBar = new ProgressBar();
            progressBar.BackColor = Color.Gray;
            progressBar.ForeColor = Color.DarkSlateGray;
            progressBar.Value = 0;
            SourceGrid.Cells.Cell cell = new SourceGrid.Cells.Cell(download.Progress);
            downloadGrid[insertRowCount, 4] = cell;
            downloadGrid[insertRowCount, 4].View = viewNormal;
            downloadGrid[insertRowCount, 4].AddController(menuController);
            SourceGrid.LinkedControlValue linkedControlValue = new SourceGrid.LinkedControlValue(progressBar, new SourceGrid.Position(insertRowCount, 4));
            downloadGrid.LinkedControls.Add(linkedControlValue);

            SourceGrid.Cells.Views.Cell statusCell = new SourceGrid.Cells.Views.Cell();
            statusCell.Border = cellBorder;
            statusCell.Font = new Font("돋음", 8, FontStyle.Regular);
            statusCell.BackColor = download.Status.ColorParse();

            SourceGrid.Cells.Cell cell5 = new SourceGrid.Cells.Cell(download.Status.Parse());
            downloadGrid[insertRowCount, 5] = cell5;
            downloadGrid[insertRowCount, 5].View = statusCell;
            downloadGrid[insertRowCount, 5].AddController(menuController);

            DownLoadBinding binding = new DownLoadBinding();
            binding.ProgressBar = progressBar;
            binding.Cell = cell5;
            binding.StatusCell = statusCell;
            downloadGrid.Rows[insertRowCount].Tag = binding;
        }
        #endregion

        private EventHandler ChangeActivateCheckBox(int row)
        {
            return delegate
            {
                if (this.downloadManager.DownloadManagerStatus == DownloadManagerStatus.Started)
                {
                    downloadGrid[row, 0].Value = !(bool)downloadGrid[row, 0].Value;
                    MessageBox.Show("다운로드가 진행중입니다.");
                    return;
                }
                bool check = (bool)downloadGrid[row, 0].Value;
                this.downloadManager.setDownloadEnable(this.downloadGrid[row, 2].DisplayText.Trim(), check);
            };
        }


        public void deleteDownloadGridRow(int rowIndex)
        {
            downloadGrid.Rows.Remove(rowIndex);
        }
        public void ModifyDownloadGridRow(int rowIndex, Download download)
        {
            PopupMenu menuController = new PopupMenu(this);
            int insertRowCount = rowIndex;
            download.RowIndex = rowIndex;

            //downloadGrid.Rows.Insert(insertRowCount);
            downloadGrid[insertRowCount, 0] = new SourceGrid.Cells.CheckBox(null, download.DownloadEnable);
            downloadGrid[insertRowCount, 0].View = viewCheckBox;
            downloadGrid[insertRowCount, 0].AddController(menuController);
            downloadGrid[insertRowCount, 0].Controller.RemoveController(downloadGrid[insertRowCount, 0].FindController<SourceGrid.Cells.Controllers.CheckBox>());
            downloadGrid[insertRowCount, 0].Controller.AddController(new SourceGrid.Cells.Controllers.CheckBox(false));
            downloadGrid[insertRowCount, 0].FindController<SourceGrid.Cells.Controllers.CheckBox>().CheckedChanged +=
                   ChangeActivateCheckBox(insertRowCount);

            downloadGrid[insertRowCount, 1] = new SourceGrid.Cells.Cell(download.No);
            downloadGrid[insertRowCount, 1].View = viewNormal;
            downloadGrid[insertRowCount, 1].AddController(menuController);

            downloadGrid[insertRowCount, 2] = new SourceGrid.Cells.Cell(download.Ip);
            downloadGrid[insertRowCount, 2].View = viewNormal;
            downloadGrid[insertRowCount, 2].AddController(menuController);

            downloadGrid[insertRowCount, 3] = new SourceGrid.Cells.Cell(Path.GetFileName(download.DownloadFileName));
            downloadGrid[insertRowCount, 3].View = viewNormal;
            downloadGrid[insertRowCount, 3].AddController(menuController);

            ProgressBar progressBar = new ProgressBar();
            progressBar.BackColor = Color.Gray;
            progressBar.Style = ProgressBarStyle.Marquee;
            progressBar.ForeColor = Color.DarkSlateGray;
           
            SourceGrid.Cells.Cell cell = new SourceGrid.Cells.Cell(download.Progress);
            downloadGrid[insertRowCount, 4] = cell;
            downloadGrid[insertRowCount, 4].View = viewNormal;
            downloadGrid[insertRowCount, 4].AddController(menuController);
            SourceGrid.LinkedControlValue linkedControlValue = new SourceGrid.LinkedControlValue(progressBar, new SourceGrid.Position(insertRowCount, 4));
            downloadGrid.LinkedControls.Add(linkedControlValue);

            SourceGrid.Cells.Views.Cell statusCell = new SourceGrid.Cells.Views.Cell();
            statusCell.Border = cellBorder;
            statusCell.Font = new Font("돋음", 8, FontStyle.Regular);
            statusCell.BackColor = download.Status.ColorParse();

            SourceGrid.Cells.Cell cell5 = new SourceGrid.Cells.Cell(download.Status.Parse());
            downloadGrid[insertRowCount, 5] = cell5;
            downloadGrid[insertRowCount, 5].View = statusCell;
            downloadGrid[insertRowCount, 5].AddController(menuController);

            downloadGrid.Invalidate();
            DownLoadBinding binding = new DownLoadBinding();
            binding.ProgressBar = progressBar;
            binding.Cell = cell5;
            binding.StatusCell = statusCell;
            downloadGrid.Tag = binding;
        }

        private void glassButton1_Click(object sender, EventArgs e)
        {
            if (this.downloadManager.DownloadStarted == true)
            {
                MessageBox.Show("다운로드가 현재 진행중에 있습니다");
                return;
            }
            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                string fileName = fileDialog.FileName;
                string[] ips = File.ReadAllLines(fileName);
                FileInfo fileInfo = new FileInfo(fileName);
                this.downloadManager.IpListFileName = fileName;

                if (ips.Length > 0)
                {
                    //valid ip check
                    bool ipValid = true;
                    StringBuilder strBuilder = new StringBuilder();
                    for (int i = 0; i < ips.Length; i++)
                    {
                        if (!CelotUtility.CheckValidIp(ips[i]))
                        {
                            ipValid = false;
                            strBuilder.AppendLine(String.Format("Ip ({0})  in {1} row  invalid", ips[i], i + 1));
                        }
                    }

                    if (!ipValid)
                    {
                        Logger.singleton().log(logLevel.Error, "IP", strBuilder.ToString());
                        MessageBox.Show("유효하지 않는 IP가 파일에 있습니다.\n자세한 사항은 로그파일을 확인해주세요");
                        return;
                    }

                    this.downloadManager.ClearDownloadMap();
                    this.downloadManager.addRange(ips);
                    if (!String.IsNullOrEmpty(this.downloadManager.DownloadFileName) && 
                        !String.IsNullOrWhiteSpace(this.downloadManager.DownloadFileName) &&
                        this.downloadManager.DownloadFileName.Length > 0)
                    {
                        this.downloadManager.SetAllDownloadFile(this.downloadManager.DownloadFileName);
                    }
                    this.InitControl();
                    this.setDataToDownloadGrid();
                }
            }
        }

        private void downloadFileBrowser_Click(object sender, EventArgs e)
        {
            if (this.downloadManager.DownloadStarted == true)
            {
                MessageBox.Show("다운로드가 현재 진행중에 있습니다");
                return;
            }

            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                this.downloadManager.setDownloadEnableAll(true);
                string fileName = fileDialog.FileName;
                this.downloadManager.DownloadFileName = Path.GetFileName(fileName);
                this.downloadManager.SetAllDownloadFile(Path.GetFileName(fileName));
                this.downloadManager.FileLength = new FileInfo(fileName).Length;
                this.fileNameBox.Text = this.downloadManager.DownloadFileName;
                this.fileSizeLabel.Text = this.downloadManager.FileLength.ToString();
                this.setDataToDownloadGrid();
            }
        }

        private void startAllDownBtn_Click(object sender, EventArgs e)
        {
            if (!this.downloadManager.CanDownloadStart()) return;
            this.downloadManager.HostIP = this.hostIpBox.Text.Trim();
            this.downloadManager.Version = this.versionBox.Text.Trim();
            this.downloadManager.AdminId = this.adminIdBox.Text.Trim();
            this.downloadManager.AdminPassword = this.adminPaswordBox.Text.Trim();
            this.downloadManager.DownloadFileType = (DownloadFileType)this.downloadTypeCombo.SelectedIndex;
            this.downloadManager.FtpPassword = this.ftpPasswordBox.Text.Trim();
            this.downloadManager.FtpId = this.ftpIdBox.Text.Trim();

            if (MessageBox.Show("다운로드가 시작됩니다.", "다운로드 확인", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                this.downloadManager.Save();
                this.downloadManager.StartAll();
                CelotApplication.Instance().ViewTransferEnable = false;
                CelotApplication.Instance().JobType = "Firmware download";
            }
            
        }

        private void cunDownBtn_Click(object sender, EventArgs e)
        {
            if (this.downloadManager.DownloadStarted == true)
            {
                MessageBox.Show("다운로드가 현재 진행중에 있습니다");
                return;
            }

            if (String.IsNullOrEmpty(this.concurrentDownCount.Text))
            {
                return;
            }
            if (MessageBox.Show("동시 다운로드 수가 변경됩니다", "동시 다운로드 수 변경", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                ApplicationConfig.Instance().ConcurrentDownloadCount = Convert.ToInt32(concurrentDownCount.Text);
                this.downloadManager.ConcurentDownloadCount = Convert.ToInt32(concurrentDownCount.Text);
            }
        }

        private void glassButton3_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("모든 다운로드 작업이 취소됩니다.", "", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                this.downloadManager.StopAllDownload();
            }
            CelotApplication.Instance().ViewTransferEnable = true;
        }

        private void downLoadItemAddBtn_Click(object sender, EventArgs e)
        {
            this.AddDownloadItem();
        }

        public void ModiyDownloadItem(int row, int col)
        {
            if (downloadManager.DownloadStarted == true)
            {
                MessageBox.Show("다운로드가 현재 진행중에 있습니다");
                return;
            }

            string ip = this.downloadGrid[row, 2].DisplayText.Trim();
            Download download = this.downloadManager.GetDownload(ip);
            int no = download.No;

            DownloadRegistrationForm f = new DownloadRegistrationForm(DownloadRegistrationForm.MODE_MODIFY, download);
            if (f.ShowDialog() == DialogResult.OK)
            {
                if (!download.Ip.Equals(f.Ip.Trim()))
                {
                    if (!this.downloadManager.add(f.Ip)) return;
                    this.downloadManager.GetDownload(f.Ip).No = no;
                    this.downloadManager.remove(download.Ip);
                    download = this.downloadManager.GetDownload(f.Ip);
                }

                download.DownloadFileName = f.FileName;
                download.Status = DownloadStatus.Ready;

                this.downloadManager.DownloadFileName = f.FileName;
                this.downloadManager.DownloadManagerStatus = DownloadManagerStatus.Ready;
                this.ModifyDownloadGridRow(row, download);
                downloadGrid.AutoSizeCells();
                
                
            }
        }

        public void DeleteDownloadItem(int row, int col)
        {
            if (this.downloadManager.DownloadStarted == true)
            {
                MessageBox.Show("다운로드가 현재 진행중에 있습니다");
                return;
            }

            string ip = downloadGrid[row, 2].DisplayText.Trim();
       
            downloadManager.remove(ip);
            deleteDownloadGridRow(row);
          
        }

        public void AddDownloadItem()
        {
            if (this.downloadManager.DownloadStarted == true)
            {
                MessageBox.Show("다운로드가 현재 진행중에 있습니다");
                return;
            }

            DownloadRegistrationForm f = new DownloadRegistrationForm(DownloadRegistrationForm.MODE_ADD, null);
            if (f.ShowDialog() == DialogResult.OK)
            {
                if (!downloadManager.add(f.Ip)) return;
                downloadGrid.SuspendLayout();
                downloadManager.GetDownload(f.Ip).DownloadFileName = this.downloadManager.DownloadFileName;
                downloadManager.GetDownload(f.Ip).Status = DownloadStatus.Ready;

                //downloadManager.DownloadManagerStatus = DownloadManagerStatus.Ready;
                addRowToDownloadGrid(downloadManager.GetDownload(f.Ip));
                downloadGrid.AutoSizeCells();
                downloadGrid.ResumeLayout();
                this.downloadGrid.Enabled = true;
            }
        }

        public class PopupMenu : SourceGrid.Cells.Controllers.ControllerBase
        {
            ContextMenu menu = new ContextMenu();
            SourceGrid.CellContext celContext;
            Application application;
            public PopupMenu(Application application)
            {
                this.application = application;
                menu.MenuItems.Add("다운로드 추가", new EventHandler(Add_Ip_Click));
               // menu.MenuItems.Add("다운로드 수정", new EventHandler(Modify_Ip_Click));
                menu.MenuItems.Add("다운로드 삭제", new EventHandler(Delete_Ip_Click));
            }

            private void Modify_Ip_Click(object sender, EventArgs e)
            {
                int row = celContext.Position.Row;
                int col = celContext.Position.Column;
                this.application.ModiyDownloadItem(row, col);
                
             
            }
            private void Add_Ip_Click(object sender, EventArgs e)
            {
                this.application.AddDownloadItem();
            }

            private void Delete_Ip_Click(object sender, EventArgs e)
            {
                int row = celContext.Position.Row;
                int col = celContext.Position.Column;
                application.DeleteDownloadItem(row, col);
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
    }
}
