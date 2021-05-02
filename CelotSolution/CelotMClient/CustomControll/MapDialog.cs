using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CelotMClient.CustomControll
{
    public partial class MapDialog : UserControl
    {
        public string _mapUrl;
        public MapDialog()
        {
            InitializeComponent();
        }

        public string MapUrl {
            get 
            {
                return this._mapUrl;
            }
            set {
                this._mapUrl = value;
                this.mapBigPic.LoadAsync(value);
            }
        }
    }
}
