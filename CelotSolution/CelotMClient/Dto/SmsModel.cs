using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CelotMClient.Model
{
    public class SmsModel
    {
        public string Name
        {
            get;
            set;
        }

        public string Description
        {
            get;
            set;
        }

        public string Group
        {
            get;
            set;
        }

        public int Number
        {
            get;
            set;
        }

        public string RouterIp
        {
            get;
            set;
        }

        public int Receive
        {
            get;
            set;
        }

        public int Send
        {
            get;
            set;
        }

        public int Status
        {
            get;
            set;
        }
    }
}
