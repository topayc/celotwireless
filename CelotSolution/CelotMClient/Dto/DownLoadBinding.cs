using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CelotMClient.Model
{
    public class DownLoadBinding
    {
        public ProgressBar ProgressBar
        {
            get;
            set;
        }

        public SourceGrid.Cells.Cell Cell
        {
            get;
            set;
        }

        public SourceGrid.Cells.Views.Cell StatusCell
        {
            get;
            set;
        }
    }
}
