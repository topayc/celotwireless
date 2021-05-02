using CelotMClient.Model;
using CelotMClient.Model;
using CelotMClient.Worker;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CelotMClient.Util
{
    public class ApplicationManager
    {
        public static List<SmsModel> getDummySmsList()
        {
            List<SmsModel> smsList = new List<SmsModel>();
            Random rand = new Random();
            for (int i = 0; i < 25; i++)
            {
                SmsModel smsModel = new SmsModel();
                smsModel.Name = "Celet " + rand.Next(100).ToString();
                smsModel.Description = "Wireless Router";
                smsModel.Group = "Celot";
                smsModel.Number = rand.Next(2000);
                smsModel.RouterIp = String.Format("{0}.{1}.{2}.{3}", rand.Next(255).ToString(), rand.Next(255).ToString(), rand.Next(255).ToString(), rand.Next(255).ToString());
                smsModel.Receive = rand.Next(100);
                smsModel.Send = rand.Next(100);
                smsList.Add(smsModel);
            }
            return smsList;
        }

        public static List<SmsDetailRecord> getDummySmsDetailRecordList()
        {
            List<SmsDetailRecord> smsRecordList = new List<SmsDetailRecord>();
            Random rand = new Random();
            for (int i = 0; i < 20; i++)
            {
                SmsDetailRecord smsDetailModel = new SmsDetailRecord();
                smsDetailModel.Number = rand.Next(100);
                smsDetailModel.PhoneNumber = "010 9890 0919";
                smsDetailModel.Time = DateTime.Now.ToString();
                smsDetailModel.RSBox = "Sms send Message " + rand.Next(100000);
                smsRecordList.Add(smsDetailModel);
            }

            return smsRecordList;
        }
    }
}
