using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CelotMClient.Manager
{
    public class ViewIntent
    {
        public object Data { get; set; }
    }
    public class CelotApplication
    {
        private static CelotApplication celotApplication;
        public MainForm MainForm { get; set; }
        public bool[] MenuSettings { get; set; }


        protected CelotApplication() { }
        public static CelotApplication Instance()
        {
            if (celotApplication == null)
            {
                celotApplication = new CelotApplication();
                bool[] menusSettings = new bool[5];
                for (int i = 0; i < menusSettings.Length; i++)
                {
                    menusSettings[i] = true;
                }
                celotApplication.MenuSettings = menusSettings;
            }
            return celotApplication;
        }

        public ViewIntent ViewIntent { get; set; }
        public bool ViewTransferEnable { get; set; }
        public string JobType { get; set; }
    }
}
