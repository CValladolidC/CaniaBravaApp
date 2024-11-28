using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Excel = Microsoft.Office.Interop.Excel;

namespace ReporteIngSISASIS
{
    class Exporta
    {
        /// <summary>
        /// Exporta a un archivo Excel desde un Datatable
        /// </summary>
        /// <param name="dt">Datatable</param>
        public void Excel_FromDataTable(System.Data.DataTable dt)
        {
            Excel.Application excel = new Excel.Application();
            Excel.Workbook workbook = excel.Application.Workbooks.Add(true);

            int iCol = 0;
            foreach (DataColumn c in dt.Columns)
            {
                iCol++;
                excel.Cells[1, iCol] = c.ColumnName;
            }

            int iRow = 0;
            foreach (DataRow r in dt.Rows)
            {
                iRow++;
                iCol = 0;
                foreach (DataColumn c in dt.Columns)
                {
                    iCol++;
                    excel.Cells[iRow + 1, iCol] = r[c.ColumnName];
                }
            }

            object missing = System.Reflection.Missing.Value;

            workbook.SaveAs("MyExcelWorkBook.xls",
                Excel.XlFileFormat.xlXMLSpreadsheet, missing, missing,
                false, false, Excel.XlSaveAsAccessMode.xlNoChange,
                missing, missing, missing, missing, missing);

            excel.Visible = true;
            Excel.Worksheet worksheet = (Excel.Worksheet)excel.ActiveSheet;
            ((Excel._Worksheet)worksheet).Activate();

            //((Excel._Application)excel).Quit();
        }

        /// <summary>
        /// Exporta a un archivo Excel con una ruta especificada a partir de un Datatable
        /// </summary>
        /// <param name="dt">Datatable</param>
        /// <param name="filename">Ruta y Nombre del Archivo</param>
        public void Excel_FromDataTable(System.Data.DataTable dt, string filename)
        {
            Excel.Application excel = new Excel.Application();
            Excel.Workbook workbook = excel.Application.Workbooks.Add(true);

            int iCol = 0;
            foreach (DataColumn c in dt.Columns)
            {
                iCol++;
                excel.Cells[1, iCol] = c.ColumnName;
            }

            int iRow = 0;
            foreach (DataRow r in dt.Rows)
            {
                iRow++;
                iCol = 0;

                foreach (DataColumn c in dt.Columns)
                {
                    iCol++;

                    excel.Cells[iRow + 1, iCol] = r[c.ColumnName];
                }
            }

            object missing = System.Reflection.Missing.Value;

            workbook.SaveAs(filename,
                Excel.XlFileFormat.xlXMLSpreadsheet, missing, missing,
                false, false, Excel.XlSaveAsAccessMode.xlNoChange,
                missing, missing, missing, missing, missing);

            //excel.Visible = true;
            //Excel.Worksheet worksheet = (Excel.Worksheet)excel.ActiveSheet;
            //((Excel._Worksheet)worksheet).Activate();

            ((Excel._Application)excel).Quit();
        }

        public int Cadena_FromAscii(string s)
        {
            return Encoding.ASCII.GetBytes(s)[0];
        }

        public string Ascii_FromCadena(string str)
        {
            Encoding ascii = Encoding.ASCII;
            Byte[] arrayBytes = ascii.GetBytes(str);
            string cadena = "";
            foreach (Char c in arrayBytes)
            {
                if (c.ToString().Equals("\n"))
                {
                    cadena += "\r\n";

                }

                else
                {
                    if (c.ToString().Equals("\f"))
                    {
                        cadena += "\r\n" + "\r\n" + "\r\n" + "\r\f";
                    }
                    else
                    {
                        cadena += c.ToString();
                    }

                }
            }

            return cadena;
        }
    }
}
