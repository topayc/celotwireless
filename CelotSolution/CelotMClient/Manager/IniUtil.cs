using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CelotMClient.Manager
{
    class IniUtil
    {

        public const string SECTION_SERVICE     = "SERVICE";
        public const string SECTION_DATABASE    = "DATABASE";

        public const string SERVICE_PORT        = "SERVICE_PORT";

        public const string DATABASE_HOST       = "DATABASE_HOST";
        public const string DATABASE_NAME       = "DATABASE_NAME";
        public const string DATABASE_ID         = "DATABASE_ID";
        public const string DATABASE_PASSWORD   = "DATABASE_PASSWORD";

        public const string INI_FILENAME        = @".\config.ini";
 
        private object monitor = new object();
        private static IniUtil instance;

        private IniUtil()
        {
            if (!File.Exists(INI_FILENAME))
            {
                File.Create(INI_FILENAME);
            }

        }
        public static IniUtil Instance()
        {
            if (instance == null)
            {
                instance = new IniUtil();
            }
            return instance;

        }

        [DllImport("kernel32.dll")]
        private static extern int GetPrivateProfileString(
            String section,
            String key,
            String def,
            StringBuilder retVal,
            int size,
            String filePath);

        [DllImport("kernel32.dll")]
        private static extern int GetPrivateProfileInt(
            String section,
            String key,
            int def,
            String filePath);

        [DllImport("kernel32.dll")]
        private static extern long WritePrivateProfileString(
            String section,
            String key,
            String val,
            String filePath);

        public String GetIniStringValue(String Section, String Key)
        {
            StringBuilder temp = new StringBuilder(255);
            int i = GetPrivateProfileString(Section, Key, "", temp, 255, INI_FILENAME);
            return temp.ToString();
        }

        public void Init()
        {

        }
        public int GetIniIntValue(String Section, String Key)
        {
            
            return GetPrivateProfileInt(Section, Key, 0, INI_FILENAME);
        }

        public void SetIniValue(String Section, String Key, String Value)
        {
            lock (monitor)
            {
                WritePrivateProfileString(Section, Key, Value, INI_FILENAME);
            }
        }
    }
}
