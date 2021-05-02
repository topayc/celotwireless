using CelotMClient.Manager;
using CelotMClient.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CelotMClient.NMSStructure
{
    public class NMSReportCommand
    {
        private Device _device;
        private nms_reprot_t _nms_reprot_t;
        private List<AlertClass> _alertList;
       
        public bool HaveAlert()
        {
            return _alertList.Count > 0 ? true : false;
        }
        public NMSReportCommand(Device device, nms_reprot_t report)
        {
            this.Device = device;
            this.nms_reprot_t = report;
            this._alertList = new List<AlertClass>();
        }

        public DeviceAlertCassification DeviceAlertCassification
        {
            get;
            set;
        }

        public bool isRegisterdDevice()
        {
            if (Device != null) 
            { 
                return true; 
            }
            return false;
        }

        public Device  Device{
            get { return _device; }
            set { _device = value; }
        }
        public nms_reprot_t nms_reprot_t
        {
            get { return _nms_reprot_t; }
            set { _nms_reprot_t = value; }
        }

        public List<AlertClass> AlertList
        {
            get { return _alertList; }
            set { _alertList = value; }
        }

        public void AddAlert(AlertClass alertClass) 
        {
            this._alertList.Add(alertClass);
        }

        public String GetAlertMessages()
        {
            string alertMessages = null;
            foreach (AlertClass alertClass in _alertList)
                alertMessages += " " + alertClass.ErrorMessage;
            return alertMessages;
        }

        public String LanIp
        {
            get { return GetLanIPString(); }
           
        }

        public String WanIP
        {
            get { return GetWanIPString(); }
        }

        public int Battery
        {
            get { 
                return this.nms_reprot_t.data.ext_device1[0];
            }

        }
        public String GetLanIPString()
        {
                int index = this.nms_reprot_t.data.current_ip_address.IndexOf("(");
                if (index < 0) return "";
                int index2 = this.nms_reprot_t.data.current_ip_address.LastIndexOf(")");
                return this.nms_reprot_t.data.current_ip_address.Substring(index + 1, index2 - index - 1).Trim();
        }

        public String GetWanIPString()
        {
            int index = this.nms_reprot_t.data.current_ip_address.IndexOf("(");
            if (index < 0) return "";
            return this.nms_reprot_t.data.current_ip_address.Substring(0, index).Trim();
        }

        public bool isProblem()
        {
            return _alertList.Count > 0 ? true : false;
        }
    }
}
