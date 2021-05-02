using CelotMClient.CustomForm;
using CelotMClient.Manager;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CelotMClient
{

    static class Program
    {
        /// <summary>
        /// 해당 응용 프로그램의 주 진입점입니다.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // 프로그램의 중복 실행 방지
            System.Diagnostics.Process[] myProc = System.Diagnostics.Process.GetProcessesByName( System.Diagnostics.Process.GetCurrentProcess().ProcessName);
            if (myProc.Length > 1)
            {
                MessageBox.Show("이미 해당 프로그램이 실행되고 있습니다", "알림");
                Application.Exit();
            }
            else
            {
                int BrowserVer, RegVal;

                // get the installed IE version
                using (WebBrowser Wb = new WebBrowser())
                    BrowserVer = Wb.Version.Major;

                // set the appropriate IE version
                if (BrowserVer >= 11)
                    RegVal = 11001;
                else if (BrowserVer == 10)
                    RegVal = 10001;
                else if (BrowserVer == 9)
                    RegVal = 9999;
                else if (BrowserVer == 8)
                    RegVal = 8888;
                else
                    RegVal = 7000;

                // set the actual key
                RegistryKey Key = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Microsoft\Internet Explorer\Main\FeatureControl\FEATURE_BROWSER_EMULATION", true);

                if (Key == null)
                    Key = Registry.CurrentUser.OpenSubKey(@"Software\wow6432node\Microsoft\Internet Explorer\Main\FeatureControl\FEATURE_BROWSER_EMULATION", true);

                if (Key != null)
                {
                    Key.SetValue(System.Diagnostics.Process.GetCurrentProcess().ProcessName + ".exe", RegVal, RegistryValueKind.DWord);
                    Key.Close();
                }

                Application.EnableVisualStyles();

                Thread splashthread = new Thread(new ThreadStart(SplashScreen.ShowSplashScreen));
                splashthread.IsBackground = true;
                splashthread.Start();
                Thread.Sleep(500);

                SplashScreen.UdpateStatusTextWithStatus("Initializing Application Config", TypeOfMessage.Success);

                if (!File.Exists(IniUtil.INI_FILENAME) || IniUtil.Instance().GetIniIntValue(ApplicationConfig.SECTION_CLIENT, ApplicationConfig.KEY_INSTALL) == 0)
                {
                    File.Create(IniUtil.INI_FILENAME).Close();
                    SplashScreen.UdpateStatusTextWithStatus("처음 실행시 설정 및 설치의 초기화 작업을 진행합니다", TypeOfMessage.Success);
                    Thread.Sleep(1500);
                    SplashScreen.CloseSplashScreen();

                    SetupForm setup = new SetupForm();
                    Thread.Sleep(1000);
                    Application.Run(setup);
                }
                else
                {

                    //Application.SetCompatibleTextRenderingDefault(false);
                    MainForm mainForm = new MainForm();
                    Application.Run(mainForm);
                }

            }
        }

    }
}
