using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CelotMClient.CustomForm
{
    public class CircularProgress
    {
        static CircularProgressForm cpf = null;

        public static void ShowCircularProgress()
        {
            if (cpf == null)
            {
                cpf = new CircularProgressForm();
                cpf.ShowProgress();
            }
        }

        public static void CloseCircularProgress()
        {
            if (cpf == null)
            {
                cpf = new CircularProgressForm();
                cpf.ClosePrgress();
            }
        }
    }
}
