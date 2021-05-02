using CelotMClient.Model;
using CelotMClient.Model.NMS;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CelotMClient.Manager
{
    public class ExcelManager
    {
        public string[] ExcelHeader { get; set;}
        public List<DeviceReportCommand> DeviceReportCommandList;
        public string FileName { get; set; }

        public ExcelManager(){}

        public void Export(DataTable ds, string fileName)
        {
            Microsoft.Office.Interop.Excel.Application oAppln;
            Microsoft.Office.Interop.Excel.Workbook oWorkBook;
            Microsoft.Office.Interop.Excel.Worksheet oWorkSheet;
            Microsoft.Office.Interop.Excel.Range oRange;
            try
            {
                oAppln = new Microsoft.Office.Interop.Excel.Application();
                oWorkBook = (Microsoft.Office.Interop.Excel.Workbook)(oAppln.Workbooks.Add(true));
                oWorkSheet = (Microsoft.Office.Interop.Excel.Worksheet)oWorkBook.ActiveSheet;
                int iRow = 2;
                if (ds.Rows.Count > 0)
                {
                    for (int j = 0; j < ds.Columns.Count; j++)
                    {
                        oWorkSheet.Cells[1, j + 1] = ds.Columns[j].ColumnName;
                    }
                    for (int rowNo = 0; rowNo < ds.Rows.Count; rowNo++)
                    {
                        for (int colNo = 0; colNo < ds.Columns.Count; colNo++)
                        {
                            oWorkSheet.Cells[iRow, colNo + 1] = ds.Rows[rowNo][colNo].ToString();
                        }
                        iRow++;
                    }
                }
                oRange = oWorkSheet.get_Range("A1", "IV1");
                oRange.EntireColumn.AutoFit();
                oAppln.UserControl = false;
                oAppln.Visible = false;
                oWorkBook.SaveAs(
                fileName, Microsoft.Office.Interop.Excel.XlFileFormat.xlWorkbookNormal, null, null, false, false,
                Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlShared, false, false, null, null, null);
            }
            catch (Exception theException)
            {
                MessageBox.Show(theException.Message.ToString());
            }
        }

        public bool Export(IDataReader reader)
        {
            return false;
        }
    }
}
