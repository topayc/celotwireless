using CelotMClient.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace CelotMClient.Worker
{
    class CelotTelnet : TelnetConnection
    {
        private Download download;
        const int READ_TIME_OUT = 60000;

        public CelotTelnet(string Hostname, int Port) : base(Hostname, Port) { }
        public CelotTelnet(Download download, string Hostname, int Port)
            : base(Hostname, Port)
        {
            this.download = download;
        }
        public void flush() { }

        public bool ReadMatch(string matchStr, int readTimeOut = READ_TIME_OUT)
        {
            bool result = false;
            int elapsedTime = 0;
            string message = "";
            try
            {
                while (elapsedTime < readTimeOut)
                {
                    message += this.Read();
                    if (message.Contains(matchStr))
                    {
                        result = true;
                        break;
                    }
                    System.Threading.Thread.Sleep(this.TimeOutMs);
                    elapsedTime += this.TimeOutMs;
                }
            }
            catch (Exception ee)
            {
                result = false;
            }
            return result;
        }


        public bool ReadMatch(Regex matchReg, int readTimeOut = READ_TIME_OUT)
        {
            bool result = false;
            int elapsedTime = 0;
            string message = "";
            try
            {
                while (elapsedTime < readTimeOut)
                {
                    message += this.Read();
                    if (matchReg.Match(message).Success)
                    {
                        result = true;
                        break;
                    }
                    System.Threading.Thread.Sleep(this.TimeOutMs);
                    elapsedTime += this.TimeOutMs;
                }
            }
            catch (Exception ee)
            {
                result = false;
            }
            return result;
        }

        public bool Login(string adminId, string adminPw)
        {

            bool result = false;
            Regex loginRegex = new Regex("BusyBox v1\\.12\\.1 \\(.*");
            String prompt = base.Login(adminId, adminPw, 3000).Trim();
            Match match = loginRegex.Match(prompt);

            Debug.WriteLine("####### Telnet login #####");
            Debug.WriteLine(prompt);
            Debug.WriteLine("");

            prompt = prompt.Substring(prompt.Length - 1, 1);
            if (prompt != "#" && prompt != ">")
            {
                result = false;
            }
            else
            {
                result = true;
            }
            return result;
        }

        public bool checkShell()
        {
            bool result = false;
            Regex shellRegex = new Regex("^\r\nUpgrade#");
            string cmd = "export set PS1=Upgrade#\n";
            this.Write(cmd);
            this.Read();
            this.Write("\n");

            result = this.ReadMatch(shellRegex);

            Debug.WriteLine("####### checkShell #####");
            Debug.WriteLine(this.message);
            Debug.WriteLine("");

            return result;
        }


        public bool CheckVersion()
        {
            string orgingOutput =
             @"Linux version 2.6.21 (Celot Wireless@Celot Wireless) (gcc version 3.4.2) #1445 Fri Jan 22 15:14:00 KST 2016";

            bool result = false;
            string cmd = "cat /proc/version\n";
            string strCatVer = "Linux version 2.6.21 (Celot Wireless@Celot Wireless) (gcc version 3.4.2) #";

            this.Write(cmd);
            result = this.ReadMatch(strCatVer);
            // string msg = this.Read();

            Debug.WriteLine("###### CheckVersion #####");
            Debug.WriteLine(message);
            Debug.WriteLine("");

            return result;
        }

        public bool InitUpgrade()
        {
            bool result = false;
            string matchStr = @"\r\nUpgrade#$";
            Regex shellRegex = new Regex(matchStr);
            // Regex shellRegex = new Regex("\r\nUpgrade#$");

            string upgradeScript = "";
            upgradeScript += "#!/bin/sh\\n";
            upgradeScript += "sip=$1\\n";
            upgradeScript += "file=$2\\n";
            upgradeScript += "filesize=$3\\n";
            upgradeScript += "ftpid=$4\\n";
            upgradeScript += "ftppw=$5\\n";
            upgradeScript += "wget_exist=`ls /usr/bin/wget | wc -l`\\n";
            upgradeScript += "if [ \"$wget_exist\" = \"1\" ]; then\\n";
            upgradeScript += "    wget ftp://$ftpid:$ftppw@$sip/home/$file\\n";
            upgradeScript += "else\\n";
            upgradeScript += "    cwget ftp://$ftpid:$ftppw@$sip/home/$file\\n";
            upgradeScript += "fi\\n";
            upgradeScript += "dnfile_ok=`ls $file | wc -l`\\n";
            upgradeScript += "if [ \\\"$dnfile_ok\\\" = \\\"1\\\" ]; then\\n";
            upgradeScript += "    echo Download Ok.\\n";
            upgradeScript += "else\\n";
            upgradeScript += "    echo Download Fail.\\n";
            upgradeScript += "    exit 0\\n";
            upgradeScript += "fi\\n";
            upgradeScript += "actualsize=$(wc -c <$file)\\n";
            upgradeScript += "if [ $actualsize -eq $filesize ]; then\\n";
            upgradeScript += "    rm -rf /var/upgrade_kernelapp.lock\\n";
            upgradeScript += "    /usr/sbin/upgrader -d 1\\n";
            upgradeScript += "    /usr/sbin/upgrader -l $actualsize -w $file -i Kernel 2>&1\\n";
            //	upgrade_scrpt +="    reboot -d 1 &\\n";
            upgradeScript += "    echo Upgrade Done.\\n";
            upgradeScript += "else\\n";
            upgradeScript += "    echo Filesize Error.\\n";
            upgradeScript += "    exit 0\\n";
            upgradeScript += "fi\\n";
            string cmd = String.Format("echo -e '{0}' > upgrade.sh;chmod +x  upgrade.sh\n", upgradeScript);

            Debug.WriteLine("##### Upgrade Iint (Transfer Script) String #####");
            Debug.WriteLine(cmd);
            Debug.WriteLine("");

            this.Write(cmd);
            result = this.ReadMatch(shellRegex);

            Debug.WriteLine("##### Transfer Script");
            Debug.WriteLine(message);
            Debug.WriteLine("");

            return result;
        }

        public bool Upgrade(string hostIp, string fileName, long fileLength, string ftpId, string ftpPw)
        {
            bool result = false;
            this.TimeOutMs = 200;

            string downloadOk = "Download Ok.";
            string downloadFail = "Download Fail.";
            string fileSizeError = "Filesize Error.";
            string upgradDone = "Upgrade Done.";
            Regex ftpTimeoutRegex = new Regex("cwget: .*: Connection timed out");

            string cmd = String.Format("./upgrade.sh {0} {1} {2} {3} {4}\n", hostIp, fileName, fileLength, ftpId, ftpPw);

            Debug.WriteLine("##### Upgrade Cmd String #####");
            Debug.WriteLine(cmd);
            Debug.WriteLine("");

            this.Write(cmd);

            string proMsg;
            Debug.WriteLine("##### Upgrade #####");

            int readTimeout = 60000;
            int elapsedTime = 0;
            int sleepTime = 200;

            Debug.WriteLine("# Waiting for download status from telnet ......");
            while (true)
            {
                proMsg = this.Read();

                Match match = ftpTimeoutRegex.Match(proMsg);
                if (match.Success)
                {
                    Debug.WriteLine(proMsg);
                    this.download.Status = DownloadStatus.DownloadFail;
                    result = false;
                    throw new Exception("Ftp connection timeout");
                }

                if (elapsedTime > readTimeout)
                {
                    this.download.Status = DownloadStatus.DownloadFail;
                    result = false; ;
                    throw new Exception("Fail : read timeout");
                }

                if (proMsg.Contains(downloadFail))
                {
                    Debug.WriteLine(proMsg);
                    this.download.Status = DownloadStatus.DownloadFail;
                    result = false; ;
                    throw new Exception("download failed");
                }

                else if (proMsg.Contains(downloadOk))
                {
                    Debug.WriteLine(proMsg);
                    this.download.Status = DownloadStatus.DownloadComplete;
                    this.download.worker.ReportProgress(0);

                    //download ok 의 경우 설치 작업이 진행되기 때문에, 메시지를 리셋하고 elapsedTime를 초기화한다.
                    proMsg = "";
                    elapsedTime = 0;
                    continue;
                }

                else if (proMsg.Contains(upgradDone))
                {
                    Debug.WriteLine(proMsg);
                    this.download.Status = DownloadStatus.UpgradeComplete;
                    this.download.worker.ReportProgress(100);
                    result = true;
                    //라우터 재부팅을 위한 대기 시간
                    System.Threading.Thread.Sleep(3000);
                    break;
                }

                else if (proMsg.Contains(fileSizeError))
                {
                    Debug.WriteLine(proMsg);
                    this.download.Status = DownloadStatus.FileSizeError;
                    result = false;
                    break;
                }

                else
                {
                    Debug.WriteLine("[Not filterd message] ==>  " + proMsg);
                }

                System.Threading.Thread.Sleep(sleepTime);
                elapsedTime += sleepTime;
            }

            return result;
        }
    }
}
