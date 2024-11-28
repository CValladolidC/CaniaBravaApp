using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;

namespace CaniaBrava
{
    class Ventas
    {
        public class CurrentState
        {
            public int LinesCounted;
            public int TotalLines;
        }

        public string Path;
        public string Tipo;
        public string Anio;
        public string Mes;
        public int Versi;

        public void procesa(System.ComponentModel.BackgroundWorker worker, System.ComponentModel.DoWorkEventArgs e)
        {
            DataTable dtexcel = new DataTable();
            string ConnString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + Path + ";Extended Properties=\"Excel 12.0;HDR=NO;\"";
            //string ConnString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + Path + ";Extended Properties = Excel 8.0";
            string query = string.Empty;

            SqlConnection conexion = new SqlConnection();
            try
            {
                query = @"INSERT INTO ventas_version VALUES ('" + Tipo + "','" + Anio + "','" + Anio + "-" + (Mes == string.Empty ? "01" : Mes) + "-01" + "','" + Versi + @"');

INSERT INTO ventas SELECT *," + (Versi - 1) + " FROM ventas_final WHERE escenario='" + Tipo + Anio.Substring(2, 2) + @"';

DELETE FROM ventas_final WHERE escenario='" + Tipo + Anio.Substring(2, 2) + "';";

                conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
                conexion.Open();
                SqlCommand myCommand = new SqlCommand(query, conexion);
                myCommand.ExecuteNonQuery();
                myCommand.Dispose();
            }
            catch (SqlException e_)
            {

            }
            finally { conexion.Close(); }

            //            List<MaesControlling> MaesControl = null;
            //            conexion = new SqlConnection();
            //            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            //            conexion.Open();
            //            query = @"select b.desmaesgen AS dactividad,c.desmaesgen AS drecurso,'' AS drubro,a.* from maescontrolling a 
            //INNER JOIN maesgen b ON b.idmaesgen='104' AND b.clavemaesgen=a.actividad 
            //INNER JOIN maesgen c ON c.idmaesgen='106' AND c.clavemaesgen=a.recurso ";
            //            using (SqlCommand cmd = new SqlCommand(query, conexion))
            //            {
            //                try
            //                {
            //                    using (var reader = cmd.ExecuteReader())
            //                    {
            //                        MaesControl = new List<MaesControlling>();
            //                        while (reader.Read())
            //                        {
            //                            MaesControl.Add(new MaesControlling()
            //                            {
            //                                id = int.Parse(reader["id"].ToString()),
            //                                idmaescon = reader["idmaescon"].ToString(),
            //                                um = reader["um"].ToString(),
            //                                cuencon = reader["cuencon"].ToString(),
            //                                cod = reader["cod"].ToString(),
            //                                drubro = reader["drubro"].ToString(),
            //                                rubro = reader["rubro"].ToString(),
            //                                dactividad = reader["dactividad"].ToString(),
            //                                actividad = reader["actividad"].ToString(),
            //                                drecurso = reader["drecurso"].ToString(),
            //                                recurso = reader["recurso"].ToString()
            //                            });
            //                        }
            //                    }
            //                    conexion.Close();
            //                }
            //                catch (Exception ex)
            //                {

            //                }
            //            }

            using (OleDbConnection con = new OleDbConnection(ConnString))
            {
                try
                {
                    OleDbDataAdapter oleAdpt = new OleDbDataAdapter("select * from [Hoja1$]", con); //here we read data from sheet1  
                    oleAdpt.Fill(dtexcel); //fill excel data into dataTable 

                    //dtexcel.Columns.Add("Tipo");
                    //dtexcel.Columns.Add("Actividades");
                    //dtexcel.Columns.Add("Rubro");
                    //dtexcel.Columns.Add("Recurso");
                    //foreach (DataRow item in dtexcel.Rows)
                    //{
                    //    item["Tipo"] = Tipo + Anio.Substring(2, 2);

                    //    var result = MaesControl.Find(x => x.cod == item["F13"] + item["F16"].ToString().ToUpper());
                    //    if (result != null)
                    //    {
                    //        item["Actividades"] = result.dactividad;
                    //        item["Rubro"] = result.drubro;
                    //        item["Recurso"] = result.drecurso;
                    //    }
                    //}

                    dtexcel.Rows.RemoveAt(0);
                    DataTable dt = new DataTable();
                    dt.Columns.Add("escenario", typeof(string));
                    dt.Columns.Add("tipo", typeof(int));
                    dt.Columns.Add("dtipo", typeof(string));
                    dt.Columns.Add("fecha", typeof(DateTime));
                    dt.Columns.Add("valor", typeof(decimal));

                    DataRow dr = null;
                    string Mes = string.Empty;
                    foreach (DataRow item in dtexcel.Rows)
                    {
                        for (int i = 0; i < 12; i++)
                        {
                            Mes = "0" + (i + 1);
                            dr = dt.NewRow();

                            dr[0] = Tipo + Anio.Substring(2, 2);
                            dr[1] = item["F1"].ToString() == "Azucar" ? 1 : 2;
                            dr[2] = item["F2"].ToString();
                            dr[3] = DateTime.Parse("01-" + Mes.Substring(Mes.Length - 2) + "-" + Anio);
                            dr[4] = item["F" + (3 + i)];

                            dt.Rows.Add(dr);
                        }
                        
                    }

                    string sqlConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");

                    using (SqlBulkCopy bulkCopy = new SqlBulkCopy(sqlConnectionString))
                    {
                        bulkCopy.ColumnMappings.Add("escenario", "escenario");
                        bulkCopy.ColumnMappings.Add("tipo", "tipo");
                        bulkCopy.ColumnMappings.Add("dtipo", "dtipo");
                        bulkCopy.ColumnMappings.Add("fecha", "fecha");
                        bulkCopy.ColumnMappings.Add("valor", "valor");
                        bulkCopy.DestinationTableName = "ventas_final";
                        bulkCopy.WriteToServer(dt);
                        bulkCopy.BulkCopyTimeout = 1360;
                    }
                }
                catch (Exception ex)
                {
                    conexion = new SqlConnection();
                    try
                    {
                        query = "DELETE FROM ventas_version WHERE anio='" + Anio + "' AND tipo='" + Tipo + "' AND versi='" + Versi + @"';

INSERT INTO ventas_final 
SELECT id,escenario,tipo,dtipo,fecha,valor 
FROM ventas WHERE escenario='" + Tipo + Anio.Substring(2, 2) + "' AND version='" + (Versi - 1) + @"';

DELETE FROM ventas WHERE escenario='" + Tipo + Anio.Substring(2, 2) + "' AND version='" + (Versi - 1) + "'";

                        conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
                        conexion.Open();
                        SqlCommand myCommand = new SqlCommand(query, conexion);
                        myCommand.ExecuteNonQuery();
                        myCommand.Dispose();
                    }
                    catch (SqlException ex_) { }
                    finally { conexion.Close(); }
                }
            }
        }
    }
}