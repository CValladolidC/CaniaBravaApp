using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.IO;
//using OfficeOpenXml;
using System.Globalization;

namespace ConsolaSISASIS
{
    class Program
    {
        static void Main(string[] args)
        {
            //String path = string.Empty;// @"C:\Caña Brava\_Asistencia\Asistencia\Maestro Caña Brava 13.08.2020.xlsx";// dialog.FileName; // get name of file
            //String servidor = string.Empty;

            //FileInfo fa = new FileInfo(AppDomain.CurrentDomain.BaseDirectory + "/directorio.txt");
            //if (fa.Exists)
            //{
            //    OpeIO opeIO = new OpeIO(AppDomain.CurrentDomain.BaseDirectory + "/directorio.txt");
            //    servidor = opeIO.ReadLineByNum(1);
            //    path = opeIO.ReadLineByNum(2);
            //}
            //int row_ = 1, column_ = 1;
            //int iniRow = 2, finRow = row_;
            //int iniCol = 1, finCol = 0;
            
            //if (File.Exists(path))
            //{
            //    FileInfo fileInfo = new FileInfo(path);
            //    using (OfficeOpenXml.ExcelPackage xlPackage = new ExcelPackage(fileInfo))
            //    {
            //        ExcelWorksheet ws = null;

            //        ws = xlPackage.Workbook.Worksheets[1];
            //        if (ws != null)
            //        {
            //            #region Obtenemos Total Columnas
            //            bool valide = true;
            //            while (valide)
            //            {
            //                if (ws.Cells[row_, column_].Value != null) { column_++; }
            //                else { valide = false; }
            //                finCol = column_;
            //            }
            //            #endregion

            //            #region Obtenemos Total Filas
            //            valide = true;
            //            row_ = 2;
            //            column_ = 1;
            //            while (valide)
            //            {
            //                if (ws.Cells[row_, column_].Value != null) { row_++; }
            //                else { valide = false; }
            //                finRow = row_;
            //            }
            //            #endregion

            //            string cadena = string.Empty;
            //            string query = string.Empty;
            //            for (int i = iniRow; i < finRow; i++)
            //            {
            //                query += "INSERT INTO MaestroSAP VALUES (";
            //                column_ = 1;
            //                valide = true;

            //                for (int x = iniCol; x < finCol; x++)
            //                {
            //                    cadena = (ws.Cells[i, x].Value != null ? ws.Cells[i, x].Value.ToString() : string.Empty);
            //                    if (x == 213 & cadena == string.Empty) { cadena = "0000"; }
            //                    switch (x)
            //                    {
            //                        case 1:
            //                        case 12://16
            //                        case 173://213
            //                            if (cadena != string.Empty)
            //                            {
            //                                cadena = "0000" + cadena;
            //                                query += "'" + cadena.Substring(cadena.Length - 8) + "',";
            //                            }
            //                            else { query += "'0',"; }
            //                            break;
            //                        case 78://82
            //                        //case 129://114
            //                        case 130://119
            //                        case 131://133
            //                        case 132://134
            //                        case 133://168
            //                        case 193://169
            //                                 //case 170:
            //                                 //case 171:
            //                                 //case 238:
            //                            if (cadena.Length > 0)
            //                            {
            //                                cadena = DateTime.ParseExact(cadena.Substring(0, 10), "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd");
            //                            }
            //                            query += "'" + cadena + "',";
            //                            break;
            //                        default:
            //                            query += "'" + cadena.Replace("'", "") + "',";
            //                            break;
            //                    }
            //                }

            //                query += ");\r\n";
            //                //var arrTemp = query.Split(',');
            //                //datosSAP.Add(new MaestroSAP()
            //                //{
            //                //});
            //            }

            //            query = query.Replace("|", "").Replace(",);", ");");

            //            //your connection string 
            //            string connString = @"Data Source=" + servidor + ";Database=Asistencia;uid=usr_asistencia;pwd=4Sist3nc14@21;";

            //            SqlConnection conexion = new SqlConnection();
            //            conexion.ConnectionString = connString;
            //            conexion.Open();

            //            try
            //            {
            //                SqlCommand myCommand = new SqlCommand(query, conexion);
            //                myCommand.CommandTimeout = 360;
            //                myCommand.ExecuteNonQuery();
            //                myCommand.Dispose();
            //                //MessageBox.Show("Sincronización de datos correcto..!!", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //                Console.WriteLine("Ejecucion finalizada!");
            //            }
            //            catch (SqlException ex)
            //            {
            //                //MessageBox.Show(ex.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            //                Console.WriteLine(ex.Message);
            //            }
            //            conexion.Close();
            //        }
            //    }
            //}
        }
    }
}