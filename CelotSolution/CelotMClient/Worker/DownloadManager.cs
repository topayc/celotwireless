using CelotMClient.Manager;
using CelotMClient.Model;
using CelotMClient.Util;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CelotMClient.Worker
{
    public enum DownloadFileType
    {
        Kernel,Uboot
    }
    public class DownloadManager
    {
        private CelotMClient.CustomView.Application application;
        private Dictionary<string, Download> downloadMap;
        private Semaphore semaphore;
        private int concurrentDownloadCount;
        private SourceGrid.Grid grid;

        public DownloadManager(CelotMClient.CustomView.Application application)
        {
            this.application = application;
            this.grid = application.downloadGrid;
            this.downloadMap = new Dictionary<string, Download>();
            this.Init();
        }

        public void Init()
        {
            this.downloadMap.Clear();
            this.concurrentDownloadCount = ApplicationConfig.Instance().ConcurrentDownloadCount;
            this.semaphore = new Semaphore(this.concurrentDownloadCount, this.concurrentDownloadCount);
            this.DownloadFileName = ApplicationConfig.Instance().FirmwareDownloadInfo["DOWNLOAD_FILE_NAME"];
            this.Version = ApplicationConfig.Instance().FirmwareDownloadInfo["VERSION"];
            this.AdminId = ApplicationConfig.Instance().FirmwareDownloadInfo["ADMIN_ID"];
            this.AdminPassword = ApplicationConfig.Instance().FirmwareDownloadInfo["ADMIN_PASSWORD"];
            this.FtpId = ApplicationConfig.Instance().FirmwareDownloadInfo["FTP_ID"];
            this.FtpPassword = ApplicationConfig.Instance().FirmwareDownloadInfo["FTP_PASSWORD"];
            this.HostIP = ApplicationConfig.Instance().FirmwareDownloadInfo["HOST_IP"];
            this.FileLength = Convert.ToInt64(ApplicationConfig.Instance().FirmwareDownloadInfo["FIRMWARE_LENGTH"]);
            
            this.IpListFileName = "";
            this.DownloadStarted = false;
            this.DownloadManagerStatus = DownloadManagerStatus.Ready;
            this.AvailableDownloadCount = 0;
            this.FinishedDownloadCount = 0;
        }

        public void Save()
        {
            Dictionary<string, string> firmInfoMap = new Dictionary<string, string>();
            firmInfoMap.Add("HOST_IP", this.HostIP);
            firmInfoMap.Add("VERSION", this.Version);
            firmInfoMap.Add("DOWNLOAD_FILE_NAME", String.IsNullOrEmpty(this.DownloadFileName) ? " " : this.DownloadFileName);
            firmInfoMap.Add("ADMIN_ID", this.AdminId);
            firmInfoMap.Add("ADMIN_PASSWORD", this.AdminPassword);
            firmInfoMap.Add("FTP_ID", this.FtpId);
            firmInfoMap.Add("FTP_PASSWORD", this.FtpPassword);
            firmInfoMap.Add("FIRMWARE_LENGTH", this.FileLength.ToString());
            ApplicationConfig.Instance().FirmwareDownloadInfo = firmInfoMap;
            ApplicationConfig.Instance().Reload();
        }

        public void addRange(String[] ips)
        {
            for (int i = 0; i < ips.Length; i++)
            {
                if (!this.add(ips[i]))
                {
                    return;
                }
            }
        }
        
        public bool add(string ip)
        {
           
            if (this.downloadMap.ContainsKey(ip.Trim()))
            {
                MessageBox.Show("이미 등록된 IP 입니다");
                return false; ;
            }
            Download download = new Download(this,this.grid, this.semaphore);
            download.Ip = ip;
            download.No = this.downloadMap.Count + 1;
            this.downloadMap.Add(ip.Trim(), download);
            return true;
        }

        public void removeRange(string[] ips)
        {
            foreach (string ip in ips)
            {
                if (!this.remove(ip))
                {
                    return;
                }
            }
        }

        public bool remove(string ip)
        {
            if (!this.downloadMap.ContainsKey(ip.Trim()))
            {
                MessageBox.Show("등록되지 않은 IP 입니다");
                return false; ;
            }
            this.downloadMap.Remove(ip.Trim());
            return true;
        }

        public void SetDownloadFile(string ip, string fileName)
        {
            this.DownloadFileName = fileName;
            Download download = this.downloadMap[ip];
            download.DownloadFileName = fileName;
        }

        public bool SetAllDownloadFile(string fileName)
        {
            if (String.IsNullOrEmpty(fileName))
            {
                MessageBox.Show("파일 이름은 빈 문자열이 될 수 없습니다");
                return false;
            }
            this.DownloadFileName = fileName;
            System.Diagnostics.Debug.WriteLine("Manager DownloadFileName : " + this.DownloadFileName);
            System.Diagnostics.Debug.WriteLine("Manager IpList  : " + this.IpListFileName);
            for (int i = 0; i < this.downloadMap.Count; i++)
            {
                Download download = this.downloadMap.Values.ToList()[i];
                download.DownloadFileName = fileName;
                System.Diagnostics.Debug.WriteLine("Download DownloadFileName : " + download.DownloadFileName);
            }
            return true;
        }
         
        public void setDownloadEnable(string ip, bool enabled){
            this.downloadMap[ip].DownloadEnable = enabled;
        }

        public void setDownloadEnableAll(bool enabled)
        {
            for (int i = 0; i < downloadMap.Count; i++)
            {
                Download download = this.downloadMap.Values.ToList()[i];
                download.DownloadEnable = enabled;
            }
        }


        public void setSemaphore(Semaphore sema)
        {

            for (int i = 0; i < downloadMap.Count; i++)
            {
                Download download = this.downloadMap.Values.ToList()[i];
                download.Semaphore = sema;
            }
        }

        public Download GetDownload(string ip)
        {
            Download download = null;
            download = this.downloadMap[ip];
            return download;
        }

        public int GetAvailableDownloadCount()
        {
            int count = 0; 
            for (int i = 0; i < downloadMap.Count; i++)
            {
                Download download = this.downloadMap.Values.ToList()[i];
                if (download.DownloadEnable == true)
                {
                    count++;
                }
            }
            return count;
        }
        public void StartAll()
        {
            for (int i = 0; i < downloadMap.Count; i++ )
            {
                Download download = this.downloadMap.Values.ToList()[i];
                if (download.DownloadEnable == true)
                {
                    Thread.Sleep(10);
                    download.start();
                    this.AvailableDownloadCount++;
                }               
            }
            this.DownloadStarted = true;
            this.DownloadManagerStatus = DownloadManagerStatus.Started;
        }

        public void Start(string ip)
        {
            Download download = this.downloadMap[ip.Trim()];
            download.start();
            this.AvailableDownloadCount++;
            this.DownloadManagerStatus = DownloadManagerStatus.Started;
        }

        public void StartRange(string[] ips)
        {
            foreach (string ip in ips)
            {
                this.Start(ip);
            }
            this.DownloadManagerStatus = DownloadManagerStatus.Started;
        }

        public void StopAllDownload()
        {
            for (int i = 0; i < downloadMap.Count; i++)
            {
                Download download = this.downloadMap.Values.ToList()[i];
                download.Stop();
            }
        }

        public void ClearDownloadMap()
        {
            this.DownloadStarted = false;
            this.DownloadManagerStatus = DownloadManagerStatus.Ready;
            this.AvailableDownloadCount = 0;
            this.FinishedDownloadCount = 0;
        }
        public void notifyDownloadFinished()
        {
            this.FinishedDownloadCount++;
            if (this.AvailableDownloadCount == this.FinishedDownloadCount)
            {
                CelotApplication.Instance().ViewTransferEnable = true;
                CelotApplication.Instance().JobType = "";
                this.DownloadStarted = false;

                StringBuilder strBuilder = new StringBuilder();
                strBuilder.AppendLine("작업이 종료되었습니다. 로그파일을 확인해주세요");
                strBuilder.AppendLine("그리드를 초기화 하시겠습니까?");
                if (MessageBox.Show(strBuilder.ToString(), "다운로드 완료", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    this.Init();
                    this.application.DownloadCompleted(true);
                }
                else
                {
                    this.DownloadManagerStatus = DownloadManagerStatus.Ready;
                    this.application.DownloadCompleted(false);
                }
                this.Log();
            }
         
        }

        public void Log()
        {
            string downtime = CelotUtility.GetCurrentTimeString("yy_MM_dd_HHmmss");
            Thread logWriteThread = new Thread(new ParameterizedThreadStart(
               delegate(object o)
               {
                   string sDirPath;
                   sDirPath = Application.StartupPath + "\\LOGS_FIRMWARE";
                   DirectoryInfo di = new DirectoryInfo(sDirPath);
                   if (di.Exists == false)
                   {
                       di.Create();
                   }

                   StreamWriter writer = File.CreateText(Path.Combine(sDirPath, String.Format("Firmwaredown_{0}_log.txt", downtime)));
                   for (int i = 0; i < downloadMap.Count; i++)
                   {
                       Download download = this.downloadMap.Values.ToList()[i];
                       if (download.DownloadEnable == true)
                       {
                           String writeData = String.Format("HOST IP[{0}] , FIRM_FILE[{1}] , VERSION[{2}] , ROUTER_IP[{3}] , DOWNLOAD_TIME[{4}] : RESULT[{5}]", 
                               this.HostIP,this.DownloadFileName ,this.Version, download.Ip, downtime, download.Status.Parse()
                               );
                           writer.WriteLine(writeData);
                       }
                   }
                   writer.Close();
               
               }));
            logWriteThread.Start(3);
        }

        public bool CanDownloadStart()
        {
            if (this.DownloadStarted == true)
            {
                MessageBox.Show("이미 다운로드가 진행중입니다");
                return false;
            }
            if (this.GetAvailableDownloadCount() == 0)
            {
                MessageBox.Show("다운로드할 수 있는 등록정보가 없습니다");
                return false;
            }
          
            if (String.IsNullOrEmpty(this.DownloadFileName) ||
                String.IsNullOrWhiteSpace(this.DownloadFileName) || 
                this.FileLength < 1)
            {
                MessageBox.Show("다운로드 파일이 지정되지 않았습니다");
                return false;
            }
            if (String.IsNullOrEmpty(this.AdminId))
            {
                MessageBox.Show("관리자 아이디를 설정해주세요");
                return false;
            }

            if (String.IsNullOrEmpty(this.AdminPassword))
            {
                MessageBox.Show("관리자 비밀번호를 설정해주세요");
                return false;
            }

            if (String.IsNullOrEmpty(this.Version))
            {
                MessageBox.Show("버젼을 입력해주세요");
                return false;
            }

            if (String.IsNullOrEmpty(this.FtpId))
            {
                MessageBox.Show("FTP Id 를 입력해주세요");
                return false;
            }

            if (String.IsNullOrEmpty(this.FtpPassword))
            {
                MessageBox.Show("FTP Password 를 입력해주세요");
                return false;
            }

            return true;
        }

        public Dictionary<string, Download> getDownloadMap(){return this.downloadMap;}
        public string IpListFileName{get;set;}
        public int AvailableDownloadCount{ get; set; }
        public int DownloadCount {get { return this.downloadMap.Count; }}
        public string DownloadFileName { get; set; }
        public string Version { get; set; }
        public DownloadManagerStatus DownloadManagerStatus { get; set; }
        public long FileLength { get; set; }
        public int FinishedDownloadCount { get; set; }
        public int ConcurentDownloadCount {
            get
            {
                return this.concurrentDownloadCount;
            }
            set 
            {

                if (this.semaphore != null)
                {
                    this.semaphore.Dispose();
                    this.semaphore = null;
                }
                
                this.concurrentDownloadCount = value;
                this.semaphore = new Semaphore(value, value);
                this.setSemaphore(this.semaphore);
            } 
        }
        public string HostIP { get; set; }
        public DownloadFileType DownloadFileType { get; set; }
        public bool DownloadStarted{get;set; }
        public Semaphore Semaphore { get; set; }
        public String AdminId { get; set; }
        public String AdminPassword { get; set; }
        public String FtpId { get; set; }
        public String FtpPassword{ get; set; }
        



    }

    public enum DownloadManagerStatus {
        NoFile,Ready, Started, Finished, Error
    }

    public static class DownloadManagerStatusExtension
    {
        public static string Parse(this DownloadManagerStatus downloadStatus)
        {
            switch (downloadStatus)
            {
                case DownloadManagerStatus.NoFile: return "Not Download File";
                case DownloadManagerStatus.Ready: return "Ready";
                case DownloadManagerStatus.Started: return "Started";
                case DownloadManagerStatus.Finished: return "Finished";
                case DownloadManagerStatus.Error: return "Error";
                default: throw new Exception();
            }
        }
    }
}
 