using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CelotMClient.Model
{
    public class AdminCommand
    {
        public int AdminNo { get; set; }
        public string Id { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public int AdminGroupNo { get; set; }
        public string AdminRegDate { get; set; }
        public string AdminGroupName { get; set; }
    }
}
