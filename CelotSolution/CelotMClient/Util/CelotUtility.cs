using CelotMClient.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace CelotMClient.Util
{
    public class CelotUtility
    {
        public static string GetBase64FromImage(Image image)
        {
            string base64Image = null;
            using (System.IO.MemoryStream stream = new System.IO.MemoryStream())
            {
                image.Save(stream, image.RawFormat);
                base64Image = Convert.ToBase64String(stream.ToArray());
            }
            return base64Image;
        }

        //인터넷 가능 여부 확인
        public static bool IsConnectedToInternet()
        {
            return System.Net.NetworkInformation.NetworkInterface.GetIsNetworkAvailable();
        }

        public static bool IsAdministratorRun()
        {
            System.Security.Principal.WindowsIdentity identity = System.Security.Principal.WindowsIdentity.GetCurrent();
            if (null != identity)
            {
                System.Security.Principal.WindowsPrincipal principal = new System.Security.Principal.WindowsPrincipal(identity);
                return principal.IsInRole(System.Security.Principal.WindowsBuiltInRole.Administrator);
            }
            return false;
        }


        public static String ChangeStampStringToLocalFormat(
            string timeStamp,
            String inputFormat = "yyMMddHHmmss",
            string outputFormat = "yy-MM-dd HH:mm:ss")
        {
            string result = "";
            try
            {
                return DateTime.ParseExact(timeStamp, inputFormat, null).ToString(outputFormat);
                
            }
            catch (Exception ee)
            {
                return result = "";
            }
        }

        public static DateTime UnixTimeStampToDateTime(int tick)
        {
            DateTime origin = new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return origin.AddSeconds(tick);
        }

        public static string UnixTimeStampToDateString(int tick)
        {
            return UnixTimeStampToDateTime(tick).ToLocalTime().ToString("yy-MM-dd HH:mm:ss");
        }

        public static string GetCurrentTimeString(string output = "yy-MM-dd HH:mm:ss")
        {
            return DateTime.Now.ToLocalTime().ToString(output);
        }

        public static int GetCurrentUnixTimeStamp()
        {
            DateTime date = DateTime.Now.ToLocalTime();
            var span = (date - new DateTime(1970, 1, 1, 0, 0, 0, 0).ToLocalTime());
            return (int)span.TotalSeconds;
        }

        public static string ChangePhoneNumberToFormatString(int phoneNumber)
        {
            string fmt = "0000-0000";
            return phoneNumber.ToString(fmt); ;
        }

        //정규식에 의한 IP 체크 
        public static bool CheckValidIp(string strIP)
        {
            string reg = @"^(([0-9]|[1-9][0-9]|1[0-9]{2}|2[0-4][0-9]|25[0-5])\.){3}([0-9]|[1-9][0-9]|1[0-9]{2}|2[0-4][0-9]|25[0-5])$";
            Regex regex = new Regex(reg);
            Match match = regex.Match(strIP);
            return match.Success;
        }

        //내부 검사에 의한 IP 체크 
        public static bool CheckValidIp2(string strIP)
        {
            char chrFullStop = '.';
            string[] arrOctets = strIP.Split(chrFullStop);
            if (arrOctets.Length != 4)
            {
                return false;
            }
            Int16 MAXVALUE = 255;
            Int32 temp; // Parse returns Int32
            foreach (String strOctet in arrOctets)
            {
                if (strOctet.Length > 3)
                {
                    return false;
                }

                temp = int.Parse(strOctet);
                if (temp > MAXVALUE)
                {
                    return false;
                }
            }
            return true;
        }

        public static DateTime ConvertToDateTime(string timeStamp, String inputFormat = "yyMMddHHmmss")
        {
            IFormatProvider KR_Format = new System.Globalization.CultureInfo("ko-KR", true);
            DateTime date = DateTime.ParseExact(timeStamp, inputFormat, KR_Format);
            return date;
        }

        public static string getTimeAgo(DateTime strDate)
        {
            string strTime = string.Empty;
            if (IsDate(Convert.ToString(strDate)))
            {
                TimeSpan t = DateTime.UtcNow.ToLocalTime() - Convert.ToDateTime(strDate);
                double deltaSeconds = t.TotalSeconds;

                double deltaMinutes = deltaSeconds / 60.0f;
                int minutes;

                if (deltaSeconds < 5)
                {
                    return "just now";
                }
                else if (deltaSeconds < 60)
                {
                    return Math.Floor(deltaSeconds) + " seconds ago";
                }
                else if (deltaSeconds < 120)
                {
                    return "a minute ago";
                }
                else if (deltaMinutes < 60)
                {
                    return Math.Floor(deltaMinutes) + " minutes ago";
                }
                else if (deltaMinutes < 120)
                {
                    return "an hour ago";
                }
                else if (deltaMinutes < (24 * 60))
                {
                    minutes = (int)Math.Floor(deltaMinutes / 60);
                    return minutes + " hours ago";
                }
                else if (deltaMinutes < (24 * 60 * 2))
                {
                    return "yesterday";
                }
                else if (deltaMinutes < (24 * 60 * 7))
                {
                    minutes = (int)Math.Floor(deltaMinutes / (60 * 24));
                    return minutes + " days ago";
                }
                else if (deltaMinutes < (24 * 60 * 14))
                {
                    return "last week";
                }
                else if (deltaMinutes < (24 * 60 * 31))
                {
                    minutes = (int)Math.Floor(deltaMinutes / (60 * 24 * 7));
                    return minutes + " weeks ago";
                }
                else if (deltaMinutes < (24 * 60 * 61))
                {
                    return "last month";
                }
                else if (deltaMinutes < (24 * 60 * 365.25))
                {
                    minutes = (int)Math.Floor(deltaMinutes / (60 * 24 * 30));
                    return minutes + " months ago";
                }
                else if (deltaMinutes < (24 * 60 * 731))
                {
                    return "last year";
                }

                minutes = (int)Math.Floor(deltaMinutes / (60 * 24 * 365));
                return minutes + " years ago";
            }
            else
            {
                return "";
            }
        }
        public static bool IsDate(string o)
        {
            DateTime tmp;
            return DateTime.TryParse(o, out tmp);
        }
    }
}
