using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.ServiceProcess;
using System.Diagnostics;
using CelotMClient.Util;


namespace CelotMClient.Manager
{
    public enum CelotServiceStatus
    {
        NotInstalled,
        Installed,
        Running,
        Stopped
    }

    public enum DatabaseStatus
    {
        Connected,
        NotConnected,
    }

    public static class CelotServiceStatusEnumExtension
    {
        public static string Parse(this CelotServiceStatus status)
        {
            string str = "";
            switch (status)
            {
                case CelotServiceStatus.NotInstalled:
                    str = "C-Cloud service not installed";
                    break;
                case CelotServiceStatus.Installed:
                    str = "C-Cloud service installed";
                    break;
                case CelotServiceStatus.Running:
                    str = "C-Cloud service running";
                    break;
                case CelotServiceStatus.Stopped:
                    str = "C-Cloud Service stopped";
                    break;
            }
            return str;
        }

        public static string Parse(this DatabaseStatus status)
        {
            string str = "";
            switch (status)
            {
                case DatabaseStatus.NotConnected:
                    str = "Database disconnected";
                    //str = "MySQL Service not Installed";
                    break;
                case DatabaseStatus.Connected:
                    str = "Database connected ";
                    //str = "MySQL Service Running";
                    break;
            }
            return str;
        }

    }
    public class ServiceManager
    {
        #region user control service
        public const int CONTROL_SERVICE_RELOAD_DEVICE = 128;
        #endregion

        #region DLLImport

        [DllImport("advapi32.dll")]
        public static extern IntPtr OpenSCManager(string lpMachineName, string lpSCDB, int scParameter);

        [DllImport("Advapi32.dll")]
        public static extern IntPtr CreateService(IntPtr SC_HANDLE, string lpSvcName, string lpDisplayName,
            int dwDesiredAccess, int dwServiceType, int dwStartType, int dwErrorControl, string lpPathName,
            string lpLoadOrderGroup, int lpdwTagId, string lpDependencies, string lpServiceStartName, string lpPassword
        );

        [DllImport("advapi32.dll")]
        public static extern void CloseServiceHandle(IntPtr SCHANDLE);

        [DllImport("advapi32.dll")]
        public static extern int StartService(IntPtr SVHANDLE, int dwNumServiceArgs, string lpServiceArgVectors);

        [DllImport("advapi32.dll", SetLastError = true)]
        public static extern IntPtr OpenService(IntPtr SCHANDLE, string lpSvcName, int dwNumServiceArgs);

        [DllImport("advapi32.dll")]
        public static extern int DeleteService(IntPtr SVHANDLE);

        [DllImport("kernel32.dll")]
        public static extern int GetLastError();

        #endregion DLLImport

        #region Private Variables

        private static ServiceManager _serviceManager;
        private string _servicePath;
        private string _serviceName;
        private string _serviceDisplayName;
        private string _mySqlSeviceName;

        #endregion Private Variables

        public string Message { get; set; }
        public CelotServiceStatus ServiceStatus { get; set; }
        public DatabaseStatus DataBaseStatus { get; set; }

        public string ServiceFilePath
        {
            get { return this._servicePath; }
            set { this._servicePath = value; }
        }
        public string ServiceName
        {
            get { return this._serviceName; }
            set { this._serviceName = value; }
        }
        public string ServiceDisplayName
        {
            get { return this._serviceDisplayName; }
            set { this._serviceDisplayName = value; }
        }

        public string MySqlServiceName
        {
            get { return this._mySqlSeviceName; }
            set { this._mySqlSeviceName = value; }
        }


        private ServiceManager()
        {
        }

        public static ServiceManager Instance()
        {
            if (_serviceManager == null)
            {
                _serviceManager = new ServiceManager();
            }
            return _serviceManager;
        }

        public void Init()
        {
            this.ServiceStatus = CelotServiceStatus.NotInstalled;
            this.DataBaseStatus = DatabaseStatus.NotConnected;

            this.ServiceFilePath = ApplicationConfig.Instance().ServiceFilePath;
            this.ServiceName = ApplicationConfig.Instance().ServiceName;
            this.ServiceDisplayName = ApplicationConfig.Instance().ServiceDisplayName;
            this.SetServiceStatus();
        }

        public void RefreshSeriveStatus()
        {
            this.Init();
        }

        public bool InstallService(string serviceName, string serviceDispName, string serviceFilePath)
        {
            this.ServiceName = serviceName;
            this.ServiceDisplayName = serviceDispName;
            this.ServiceFilePath = serviceFilePath;
            return this.InstallService();
        }

        public bool InstallService(string serviceName, string serviceDispName, string serviceFilePath, string mySqlSeviceName)
        {
            this.ServiceName = serviceName;
            this.ServiceDisplayName = serviceDispName;
            this.ServiceFilePath = serviceFilePath;
            this.MySqlServiceName = mySqlSeviceName;
            return this.InstallService();
        }

        public bool InstallService()
        {
            if (!CelotUtility.IsAdministratorRun())
            {
                Message = "관리자 권한이 필요합니다";
                return false;
            }

            string svcName = this.ServiceName;
            string svcPath = this.ServiceFilePath;
            string svcDispName = this.ServiceDisplayName;
            string svcMySqlName = String.IsNullOrWhiteSpace(this.MySqlServiceName) || String.IsNullOrEmpty(this.MySqlServiceName) ? "MySQL" : this.MySqlServiceName; 
            #region service validation
            if (String.IsNullOrEmpty(svcName))
            {
                Message = "서비스 이름이 지정되지 않았습니다";
                return false;
            }

            if (String.IsNullOrEmpty(svcDispName))
            {
                Message = "서비스 표시 이름이 지정되지 않았습니다";
                return false;
            }

            if (String.IsNullOrEmpty(svcPath))
            {
                Message = "서비스로 실행할 exe 파일이 지정되지 않았습니다";
                return false;
            }

            if (!File.Exists(svcPath))
            {
                Message = "지정된 파일이 존재하지 않습니다";
                return false;
            }

            if (!Path.GetExtension(svcPath).Equals(".exe") || Path.GetExtension(svcPath).Equals(".EXE"))
            {
                Message = "정상적인 서비스 실행파일이 아닌 것으로 보입니다";
                return false;
            }
            #endregion service validation

            #region Constants declaration.
            int SC_MANAGER_CREATE_SERVICE = 0x0002;
            int SERVICE_WIN32_OWN_PROCESS = 0x00000010;
            int SERVICE_ERROR_NORMAL = 0x00000001;
            int STANDARD_RIGHTS_REQUIRED = 0xF0000;
            int SERVICE_QUERY_CONFIG = 0x0001;
            int SERVICE_CHANGE_CONFIG = 0x0002;
            int SERVICE_QUERY_STATUS = 0x0004;
            int SERVICE_ENUMERATE_DEPENDENTS = 0x0008;
            int SERVICE_START = 0x0010;
            int SERVICE_STOP = 0x0020;
            int SERVICE_PAUSE_CONTINUE = 0x0040;
            int SERVICE_INTERROGATE = 0x0080;
            int SERVICE_USER_DEFINED_CONTROL = 0x0100;
            int SERVICE_ALL_ACCESS = (STANDARD_RIGHTS_REQUIRED
                                            | SERVICE_QUERY_CONFIG
                                            | SERVICE_CHANGE_CONFIG
                                            | SERVICE_QUERY_STATUS
                                            | SERVICE_ENUMERATE_DEPENDENTS
                                            | SERVICE_START
                                            | SERVICE_STOP
                                            | SERVICE_PAUSE_CONTINUE
                                            | SERVICE_INTERROGATE
                                            | SERVICE_USER_DEFINED_CONTROL);
            int SERVICE_AUTO_START = 0x00000002;
            int SERVICE_DEMAND_START = 0x00000003;

            #endregion Constants declaration.

            try
            {
                IntPtr sc_handle = OpenSCManager(null, null, SC_MANAGER_CREATE_SERVICE);

                if (sc_handle.ToInt32() != 0)
                {
                    IntPtr sv_handle = CreateService(
                        sc_handle,
                        svcName,
                        svcDispName,
                        SERVICE_ALL_ACCESS,
                        SERVICE_WIN32_OWN_PROCESS,
                        SERVICE_AUTO_START,
                        SERVICE_ERROR_NORMAL,
                        svcPath, null, 0, svcMySqlName, null, null);

                    if (sv_handle.ToInt32() == 0)
                    {
                        Message = "서비스를 설치하지 못했습니다";
                        ServiceStatus = CelotServiceStatus.NotInstalled;
                        CloseServiceHandle(sc_handle);
                        return false;
                    }
                    else
                    {
                        ServiceStatus = CelotServiceStatus.Installed;
                        Message = "서비스를 정삭적으로 설치하였습니다";
                        CloseServiceHandle(sc_handle);
                        return true;
                    }
                }
                else
                {
                    Message = "서비스를 설치 할 수 없습니다.권한 문제를 체크해보세요";
                    ServiceStatus = CelotServiceStatus.NotInstalled;
                    return false;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public bool UninstallService()
        {
            if (!CelotUtility.IsAdministratorRun())
            {
                Message = "[서비스 설치] 관리자 권한이 필요합니다";
                return false;
            }

            if (!CelotUtility.IsAdministratorRun())
            {

                return false;
            }

            string svcName = this.ServiceName;
            MessageBox.Show(this.ServiceName);
            int GENERIC_WRITE = 0x40000000;
            IntPtr sc_hndl = OpenSCManager(null, null, GENERIC_WRITE);

            if (sc_hndl.ToInt32() != 0)
            {
                int DELETE = 0x10000;
                IntPtr svc_hndl = OpenService(sc_hndl, svcName, DELETE);

                if (svc_hndl.ToInt32() != 0)
                {
                    int i = DeleteService(svc_hndl);
                    if (i != 0)
                    {
                        CloseServiceHandle(sc_hndl);
                        ServiceStatus = CelotServiceStatus.NotInstalled;
                        Message = "서비스를 제거하였습니다";
                        return true;
                    }
                    else
                    {
                        Message = "서비스제거를 수행하지 못했습니다";
                        CloseServiceHandle(sc_hndl);
                        return false;
                    }
                }
                else
                    return false;
            }
            else
                return false;
        }

        public bool StartService()
        {
            if (!CelotUtility.IsAdministratorRun())
            {
                Message = "[서비스 시작] 관리자 권한이 필요합니다";
                return false;
            }

            try
            {
                using (ServiceController serviceController = new ServiceController(this.ServiceName))
                {
                    if (serviceController.Status == ServiceControllerStatus.Stopped)
                    {
                        serviceController.Start();
                        serviceController.WaitForStatus(ServiceControllerStatus.Running);
                        ServiceStatus = CelotServiceStatus.Running;
                        Message = "서비스를 시작했습니다";
                        return true;
                    }
                    else
                    {
                        Message = "서비스가 이미 실행되고 있습니다";
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                Message = "서비스 시작에 실패하였습니다.\n" + ex.Message;
                return false;
            }
        }

        public bool StopService()
        {
            if (!CelotUtility.IsAdministratorRun())
            {
                Message = "[서비스 중지] 관리자 권한이 필요합니다";
                return false;
            }

            try
            {
                using (ServiceController serviceController =
                    new ServiceController(ApplicationConfig.Instance().ServiceName))
                {
                    if (serviceController.Status == ServiceControllerStatus.Running)
                    {
                        serviceController.Stop();
                        serviceController.WaitForStatus(ServiceControllerStatus.Stopped);
                        ServiceStatus = CelotServiceStatus.Stopped;
                        Message = "서비스를 중지했습니다";
                        return true;
                    }
                    else
                    {
                        Message = "서비스가 이미 중지된 상태입니다";
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                Message = "서비스 중지에 실패하였습니다." + Environment.NewLine + ex.Message;
                return false;
            }
        }

        //SetServiceStatus 와 유사하나 상태 값을 반환함
        public CelotServiceStatus QueryServiceStatus()
        {
            ServiceController sc = null;
            try
            {
                sc = new ServiceController(ApplicationConfig.Instance().ServiceName);
                ServiceControllerStatus status = sc.Status;
                switch (status)
                {
                    case ServiceControllerStatus.Running:
                        this.ServiceStatus = CelotServiceStatus.Running;
                        break;
                    case ServiceControllerStatus.Stopped:
                        this.ServiceStatus = CelotServiceStatus.Stopped;
                        break;
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                this.ServiceStatus = CelotServiceStatus.NotInstalled;
            }
            return this.ServiceStatus;
        }

        //서비스의 상태를 조사하여, 상태 정보를 CelotServiceStatus Enum 으로 저장 
        public void SetServiceStatus()
        {
            ServiceController sc = null;
            try
            {
                sc = new ServiceController(ApplicationConfig.Instance().ServiceName);
                ServiceControllerStatus status = sc.Status;
                switch (status)
                {
                    case ServiceControllerStatus.Running:
                        this.ServiceStatus = CelotServiceStatus.Running;
                        break;
                    case ServiceControllerStatus.Stopped:
                        this.ServiceStatus = CelotServiceStatus.Stopped;
                        break;
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                Debug.WriteLine(e.StackTrace);
                this.ServiceStatus = CelotServiceStatus.NotInstalled;
            }
        }

        //서비스로 사용자 명령 전송
        public bool ControlService(int code)
        {
            if (!CelotUtility.IsAdministratorRun())
            {
                Message = "[컨트롤 서비스] 관리자 권한이 필요합니다";
                return false;
            }

            switch (code)
            {
                case ServiceManager.CONTROL_SERVICE_RELOAD_DEVICE:
                    if (QueryServiceStatus() == CelotServiceStatus.Running)
                    {
                        ServiceController sc = new ServiceController(ApplicationConfig.Instance().ServiceName);
                        sc.ExecuteCommand(CONTROL_SERVICE_RELOAD_DEVICE);
                    }
                    break;
            }
            return true;
        }

        public string GetNextStatusString()
        {
            string str = "";
            switch (this.ServiceStatus)
            {
                case CelotServiceStatus.NotInstalled:
                    str = "Install";
                    break;
                case CelotServiceStatus.Running:
                    str = "Stop";
                    break;
                case CelotServiceStatus.Stopped:
                    str = "Start";
                    break;
            }
            return str;
        }


      
    }
}
