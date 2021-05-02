using System;
using System.Collections.Generic;
using System.IO.MemoryMappedFiles;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CelotMClient.NMSStructure
{
    public class NMSAlertTracer
    {
        public static string SHARE_MEMORY_NAME = "celot_share_data.dat";
        private static NMSAlertTracer tracer;

        private  MemoryMappedFile mmf;
        private MemoryMappedViewStream mmfvs;
        
        
        public static NMSAlertTracer Instance()
        {
            if (tracer == null)
            {
                tracer = new NMSAlertTracer();
                tracer.Init();
            }
            return tracer;
        }

        private void Init()
        {
            mmf = MemoryMappedFile.OpenExisting(SHARE_MEMORY_NAME);
            mmfvs = mmf.CreateViewStream();
        }

        
        public int GetAlertTrace(int sessionId)
        {
            return 0;
        }

        public void GetAllAlertTrace()
        {

        }
    }
}
