using CelotMClient.Worker;
using SourceGrid;
using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CelotMClient.Model
{
    public static class DownloadStatusExtension
    {
        public static string Parse(this DownloadStatus downloadStatus)
        {
            switch (downloadStatus)
            {
                case DownloadStatus.Ready: return "Ready";
                case DownloadStatus.RequestToOpenTelnetPort: return "Request to open telnet port";
                case DownloadStatus.FailOpenTelnetPort: return "fail to Request open telnet port";
                case DownloadStatus.TelnetLogin: return "Telnet Login";
                case DownloadStatus.TelnetLoginError: return "Telnet Login Error";
                case DownloadStatus.VersionCheck: return "Checking version";
                case DownloadStatus.VersionError: return "Version Error";
                case DownloadStatus.ShellInit: return "Init Shell";
                case DownloadStatus.ShellInitError: return "Init Shell Error";
                case DownloadStatus.TransferScript: return "Transfer Script";
                case DownloadStatus.TransferScriptError: return "Transfer Script Error";
                case DownloadStatus.Process: return "Process";
                case DownloadStatus.DownloadComplete: return "Download Completed";
                case DownloadStatus.Error: return "Error";
                case DownloadStatus.UpgradeComplete: return "Upgrade Completed";
                case DownloadStatus.FileSizeError: return "File Size Error";
                case DownloadStatus.ConnectionFail: return "Connection Fail";
                case DownloadStatus.DownloadFail: return "Download Fail";
                case DownloadStatus.DownloadStopped: return "Download Stopped";
                default:throw new Exception();
            }
        }

        public static Color ColorParse(this DownloadStatus downloadStatus)
        {
            switch (downloadStatus)
            {
                case DownloadStatus.Ready: return Color.Gray;
                case DownloadStatus.RequestToOpenTelnetPort: return Color.LightGray;
                case DownloadStatus.FailOpenTelnetPort: return Color.Red;
                case DownloadStatus.TelnetLogin: return Color.Yellow;
                case DownloadStatus.TelnetLoginError: return Color.Red;
                case DownloadStatus.VersionCheck: return Color.Yellow;
                case DownloadStatus.VersionError: return Color.Red;
                case DownloadStatus.ShellInit: return Color.LightYellow;
                case DownloadStatus.ShellInitError: return Color.Red;
                case DownloadStatus.TransferScript: return Color.LightPink;
                case DownloadStatus.TransferScriptError: return Color.Red;
                case DownloadStatus.Process:return Color.LightGoldenrodYellow;
                case DownloadStatus.DownloadComplete: return Color.YellowGreen;
                case DownloadStatus.Error:return Color.DarkRed;
                case DownloadStatus.UpgradeComplete: return Color.Blue;
                case DownloadStatus.FileSizeError: return Color.Brown;
                case DownloadStatus.ConnectionFail: return Color.Red;
                case DownloadStatus.DownloadFail: return Color.Red;
                case DownloadStatus.DownloadStopped: return Color.DarkGray;
                default: throw new Exception();
            }
        }
    }

    public enum DownloadStatus
    {
        Ready,
        RequestToOpenTelnetPort,
        FailOpenTelnetPort,
        VersionCheck,
        VersionError,
        ShellInit,
        ShellInitError,
        TransferScript,
        TransferScriptError,
        Process,
        DownloadComplete,
        UpgradeComplete,
        Error,
        FileSizeError,
        ConnectionFail,
        DownloadFail,
        TelnetLogin,
        TelnetLoginError,
        DownloadStopped,
    }

    public class Download
    {
        private DownloadManager downloadManager;
        private SourceGrid.Grid downGrid;
        public  BackgroundWorker worker;
        private Semaphore sema;
        
        private string downloadFileName;
        private string adminId;
        private string adminPassword;
        private CelotTelnet celotTelnet;
        private ConcurrentQueue<string> recvQueue;
        private int marqueeInterval;
        
        public Download(DownloadManager downloadManager, SourceGrid.Grid grid, Semaphore sema)
        {
            Random r = new Random();
            this.marqueeInterval = r.Next(1, 6);
            this.downloadManager = downloadManager;
            this.downGrid = grid;
            this.sema = sema;
            this.initWorker();
            this.Ip = "";
            this.No = 0;
            this.Status = DownloadStatus.Ready;
            this.DownloadEnable = true;
            this.DownloadFileName = "";
            this.FileLength = 0;
            this.Progress = 0;
            this.adminId = downloadManager.AdminId;
            this.adminPassword = downloadManager.AdminPassword;
            this.recvQueue = new ConcurrentQueue<string>();
        }

        private void initWorker()
        {
            this.worker = new BackgroundWorker();
            this.worker.WorkerReportsProgress = true;
            this.worker.WorkerSupportsCancellation = true;
            this.worker.DoWork += new DoWorkEventHandler(this.DownloadDoWork);
            this.worker.ProgressChanged += new ProgressChangedEventHandler(this.ProgressChanged);
            this.worker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(this.RunWorkerCompleted);
            this.worker.WorkerSupportsCancellation = true;
        }
        public void Stop()
        {
            this.worker.CancelAsync();
        }

        private void DownloadDoWork(object sender, DoWorkEventArgs e)
        {
            if (worker.CancellationPending)
            {
                e.Cancel = true;
                this.Status = DownloadStatus.DownloadStopped;
                return;
            }

            this.sema.WaitOne();
            this.Status = DownloadStatus.RequestToOpenTelnetPort;
            this.worker.ReportProgress(0);

            string adminId = downloadManager.AdminId;
            string adminPw = downloadManager.AdminPassword;
            string ip = this.Ip;
            int telnetPort = 23;
            
            if (worker.CancellationPending)
            {
                e.Cancel = true;
                this.Status = DownloadStatus.DownloadStopped;
                return;
            }

            // 웹 서버 Basic Auth  통한 텔넷 구동 
            try
            {
                //WebClient webClient = null;
                Regex webInitRegex = new Regex("Location:.*/adm/system_command.asp");

                Uri uri = new Uri(string.Format("http://{0}/goform/SystemCommand?command=telnetd", this.Ip));
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri);
                request.Method = WebRequestMethods.Http.Get;

                NetworkCredential networkCredential = new NetworkCredential(adminId, adminPassword);
                CredentialCache credentialCache = new CredentialCache();
                credentialCache.Add(uri, "Basic", networkCredential);
                request.PreAuthenticate = true;
                request.Credentials = credentialCache;

                Debug.WriteLine("#### 웹포트를 통한 텔넷 개방 요청 ###");
                using (WebResponse response = request.GetResponse())
                {
                    Debug.WriteLine(((HttpWebResponse)response).StatusDescription);
                    using (Stream dataStream = response.GetResponseStream())
                    {
                        using (StreamReader reader = new StreamReader(dataStream))
                        {
                            string responseFromServer = reader.ReadToEnd();
                            Debug.WriteLine(responseFromServer);
                        }
                    }

                    WebHeaderCollection headers = response.Headers;
                    for (int i = 0; i < headers.Count; i++)
                    {
                        Debug.WriteLine(headers.GetKey(i) + " = " + headers.Get(i));
                    }

                }
                //webClient = new WebClient();
                ////webClient.Credentials = new NetworkCredential(adminId, adminPw); //doesnt work
                //string credentials = Convert.ToBase64String(Encoding.ASCII.GetBytes(adminId + ":" + adminPw));
                //webClient.Headers[HttpRequestHeader.Authorization] = string.Format("Basic {0}", credentials);

                //string result = webClient.DownloadString(string.Format("http://{0}/goform/SystemCommand?command=telnetd", this.Ip));
                //WebHeaderCollection  headers = webClient.ResponseHeaders;
                // Debug.WriteLine("헤더 ");
                // Debug.WriteLine(string.Format("상태드 {0}", webClient.S));
                // for (int i = 0; i < headers.Count; i++)
                // {
                //    Debug.WriteLine(headers.GetKey(i) + " = " + headers.Get(i));
                // }
                //Match match = webInitRegex.Match(result);
                //Debug.WriteLine("#### 웹포트를 통한 텔넷 개방 요청 ###");
               // Debug.WriteLine(result);
               // if (!match.Success)
                   // throw new Exception();
            }
            catch (Exception ex)
            {
                ProcessAfterError(DownloadStatus.FailOpenTelnetPort, ex, "텔넷 개방 요청 에러");
                return;
            }

            if (worker.CancellationPending)
            {
                e.Cancel = true;
                this.Status = DownloadStatus.DownloadStopped;
                sema.Release();
                return;
            }

            Thread.Sleep(2000);
            try
            {
                this.celotTelnet = new CelotTelnet(this, this.Ip, telnetPort);
            }
            catch (Exception ex)
            {
                ProcessAfterError(DownloadStatus.ConnectionFail, ex, "연결 에러 ");
                return;
            }
          
            if (worker.CancellationPending)
            {
                e.Cancel = true;
                this.Status = DownloadStatus.DownloadStopped;
                sema.Release();
                return;
            }
            this.Status = DownloadStatus.TelnetLogin;
            this.worker.ReportProgress(0);

            Thread.Sleep(1000);
            try
            {
                //텔넷 로그인 
                if (!celotTelnet.Login(adminId, adminPw))
                {
                    this.Status = DownloadStatus.TelnetLoginError;
                    this.sema.Release();
                    return;
                }
            }
            catch(Exception e3){
                ProcessAfterError(DownloadStatus.TelnetLoginError, e3, "텔넷 로그인 에러 ");
                return;
            }
           
            if (worker.CancellationPending)
            {
                e.Cancel = true;
                this.Status = DownloadStatus.DownloadStopped;
                sema.Release();
                return;
            }

            //Shell 체크 
            Thread.Sleep(300);
            this.Status = DownloadStatus.ShellInit;
            this.worker.ReportProgress(0);
            try
            {
                if (!celotTelnet.checkShell())
                {
                    this.Status = DownloadStatus.ShellInitError;
                    this.sema.Release();
                    return;
                }
            }catch(Exception ee){
                ProcessAfterError(DownloadStatus.ShellInitError, ee, "쉘 체크 에러 ");
                return;
            }

            if (worker.CancellationPending)
            {
                e.Cancel = true;
                this.Status = DownloadStatus.DownloadStopped;
                sema.Release();
                return;
            }

            //버젼 체크 
            Thread.Sleep(300);
            this.Status = DownloadStatus.VersionCheck;
            this.worker.ReportProgress(0);
            try
            {
                if (!celotTelnet.CheckVersion())
                {
                    this.Status = DownloadStatus.VersionError;
                    this.sema.Release();
                    return;
                }
            }
            catch (Exception ee)
            {
                ProcessAfterError(DownloadStatus.VersionError, ee, "버젼 체크 에러");
                return;
            }

            if (worker.CancellationPending)
            {
                e.Cancel = true;
                this.Status = DownloadStatus.DownloadStopped;
                sema.Release();
                return;
            }

            //스트립트 전송
            Thread.Sleep(300);
            this.Status = DownloadStatus.TransferScript;
            this.worker.ReportProgress(0);

            try
            {
                if (!celotTelnet.InitUpgrade())
                {
                    this.Status = DownloadStatus.TransferScriptError;
                    this.sema.Release();
                    return;
                }
            }catch(Exception ee){
                ProcessAfterError(DownloadStatus.TransferScriptError, ee, "업그레이드 초기화 에러");
                return;
            }

            Thread.Sleep(300);
            if (worker.CancellationPending)
            {
                e.Cancel = true;
                this.Status = DownloadStatus.DownloadStopped;
                sema.Release();
                return;
            }
            
            this.Status = DownloadStatus.Process;
            this.worker.ReportProgress(0);
            try
            {
                celotTelnet.Upgrade(
                    this.downloadManager.HostIP,
                    this.downloadManager.DownloadFileName,
                    this.downloadManager.FileLength,
                    this.downloadManager.FtpId,
                    this.downloadManager.FtpPassword);
                sema.Release();
            }
            catch (Exception ee)
            {
                ProcessAfterError(DownloadStatus.DownloadFail, ee, "업그레이드 에러");
                return;
            }
            
        }
        private void ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
           DownLoadBinding binding = (DownLoadBinding)this.downGrid.Rows[this.RowIndex].Tag;
           ProgressBar progressBar = binding.ProgressBar;
           SourceGrid.Cells.Cell cell1 = binding.Cell;
           cell1.Value = this.Status.Parse();
           SourceGrid.Cells.Views.Cell statusCell = binding.StatusCell;
           progressBar.Value = e.ProgressPercentage;
           statusCell.BackColor = this.Status.ColorParse();
           
        }

        public void ProcessAfterError(DownloadStatus status,Exception e, string debugMsg){
            sema.Release();
            this.Status = status;
            Debug.WriteLine("######### " +debugMsg);
            Debug.WriteLine(e.Message);
            Debug.WriteLine(e.StackTrace);
        }

        private void RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (this.celotTelnet != null)
            {
                this.celotTelnet.Close();
            }
            else
            {
                //텔넷 연결이 안되는 경우
                this.Status = DownloadStatus.ConnectionFail;
            }
            DownLoadBinding binding = (DownLoadBinding)this.downGrid.Rows[this.RowIndex].Tag;
            ProgressBar progressBar = binding.ProgressBar;
            progressBar.Style = ProgressBarStyle.Continuous;
            progressBar.MarqueeAnimationSpeed = 0;
            
            SourceGrid.Cells.Cell cell1 = binding.Cell;
            cell1.Value = this.Status.Parse();
            SourceGrid.Cells.Views.Cell statusCell = binding.StatusCell;
            statusCell.BackColor = this.Status.ColorParse();
            this.downloadManager.notifyDownloadFinished();
        }

        public void start()
        {
            DownLoadBinding binding = (DownLoadBinding)this.downGrid.Rows[this.RowIndex].Tag;
            ProgressBar progressBar = binding.ProgressBar;
            progressBar.Style = ProgressBarStyle.Marquee;
            progressBar.MarqueeAnimationSpeed = this.marqueeInterval;
          
            this.worker.RunWorkerAsync();
        }
        public int No { get; set; }
        public string Ip { get; set; }
        public DownloadStatus Status { get; set; }
        public bool DownloadEnable { get; set; }
        public string DownloadFileName {
            get { return this.downloadFileName; }
            set 
            {
                if (value != null && !"".Equals(value))
                {
                    this.downloadFileName = value;
                    this.Status = DownloadStatus.Ready;
                }
            }
        }
        public long FileLength { get; set; }
        public int Progress
        {
            get;
            set;
        }

        public Semaphore Semaphore
        {
            get { return this.sema; }
            set { this.sema = value; }
        }

        public int RowIndex
        {
            get;
            set;
        }
    }

    class DownloadReportObj
    {
        public DownloadStatus DownloadStatus
        {
            get;
            set;
        }

        public int ProgressPercentage
        {
            get;
            set;
        }
    }
}
