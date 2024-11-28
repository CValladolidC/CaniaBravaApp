using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Excel = Microsoft.Office.Interop.Excel;

namespace CaniaBrava
{
    class RrhhGastos
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
                query = @"INSERT INTO gastos_version VALUES ('" + Tipo + "','" + Anio + "','" + Anio + "-" + (Mes == string.Empty ? "01" : Mes) + "-01" + "','" + Versi + @"');

INSERT INTO gastos SELECT *," + (Versi - 1) + " FROM gastos_final WHERE Tipo='" + Tipo + Anio.Substring(2, 2) + @"';

DELETE FROM gastos_final WHERE Tipo='" + Tipo + Anio.Substring(2, 2) + "';";

                conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
                conexion.Open();
                SqlCommand myCommand = new SqlCommand(query, conexion);
                //myCommand.ExecuteNonQuery();
                myCommand.Dispose();
            }
            catch (SqlException e_)
            {

            }
            finally { conexion.Close(); }

            List<MaesControlling> MaesControl = null;
            conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();
            query = @"select b.desmaesgen AS dactividad,c.desmaesgen AS drecurso,'' AS drubro,a.* from maesgastos a 
INNER JOIN maesgen b ON b.idmaesgen='104' AND b.clavemaesgen=a.actividad 
INNER JOIN maesgen c ON c.idmaesgen='106' AND c.clavemaesgen=a.recurso ";
            using (SqlCommand cmd = new SqlCommand(query, conexion))
            {
                try
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        MaesControl = new List<MaesControlling>();
                        while (reader.Read())
                        {
                            MaesControl.Add(new MaesControlling()
                            {
                                id = int.Parse(reader["id"].ToString()),
                                idmaescon = reader["idmaescon"].ToString(),
                                um = reader["um"].ToString(),
                                cuencon = reader["cuencon"].ToString(),
                                cod = reader["cod"].ToString(),
                                drubro = reader["drubro"].ToString(),
                                rubro = reader["rubro"].ToString(),
                                dactividad = reader["dactividad"].ToString(),
                                actividad = reader["actividad"].ToString(),
                                drecurso = reader["drecurso"].ToString(),
                                recurso = reader["recurso"].ToString()
                            });
                        }
                    }
                    conexion.Close();
                }
                catch (Exception ex)
                {

                }
            }

            using (OleDbConnection con = new OleDbConnection(ConnString))
            {
                try
                {
                    OleDbDataAdapter oleAdpt = new OleDbDataAdapter("select * from [Data$]", con); //here we read data from sheet1  
                    oleAdpt.Fill(dtexcel); //fill excel data into dataTable 

                    dtexcel.Columns.Add("Tipo");
                    dtexcel.Columns.Add("Actividades");
                    dtexcel.Columns.Add("Rubro");
                    dtexcel.Columns.Add("Recurso");
                    foreach (DataRow item in dtexcel.Rows)
                    {
                        item["Tipo"] = Tipo + Anio.Substring(2, 2);

                        var result = MaesControl.Find(x => x.cod == item["F13"] + item["F16"].ToString().ToUpper());
                        if (result != null)
                        {
                            item["Actividades"] = result.dactividad;
                            item["Rubro"] = result.drubro;
                            item["Recurso"] = result.drecurso;
                        }
                    }

                    dtexcel.Rows.RemoveAt(0);
                    string sqlConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");

                    using (SqlBulkCopy bulkCopy = new SqlBulkCopy(sqlConnectionString))
                    {
                        bulkCopy.ColumnMappings.Add("F1", "Orden");
                        bulkCopy.ColumnMappings.Add("F2", "Cecoste");
                        bulkCopy.ColumnMappings.Add("F3", "Macrofundo");
                        bulkCopy.ColumnMappings.Add("F4", "Fundo");
                        bulkCopy.ColumnMappings.Add("F5", "Sector");
                        bulkCopy.ColumnMappings.Add("F6", "Turno");
                        bulkCopy.ColumnMappings.Add("F7", "Objagrico");
                        bulkCopy.ColumnMappings.Add("F8", "Zafra");
                        bulkCopy.ColumnMappings.Add("F9", "Mes");
                        bulkCopy.ColumnMappings.Add("F10", "Anio");
                        bulkCopy.ColumnMappings.Add("F11", "Labores");
                        bulkCopy.ColumnMappings.Add("F12", "Grp_Act");
                        bulkCopy.ColumnMappings.Add("F13", "ActividadMaterial");
                        bulkCopy.ColumnMappings.Add("F14", "Denominacion");
                        bulkCopy.ColumnMappings.Add("F15", "Material");
                        bulkCopy.ColumnMappings.Add("F16", "UMB");
                        bulkCopy.ColumnMappings.Add("F17", "Cantidad");
                        bulkCopy.ColumnMappings.Add("F18", "MonSol");
                        bulkCopy.ColumnMappings.Add("F19", "ValMonObj");
                        bulkCopy.ColumnMappings.Add("F20", "MonUSD");
                        bulkCopy.ColumnMappings.Add("F21", "ValMScCO");
                        bulkCopy.ColumnMappings.Add("F22", "Tipo");
                        bulkCopy.ColumnMappings.Add("F23", "Actividades");
                        bulkCopy.ColumnMappings.Add("F24", "Rubro");
                        bulkCopy.ColumnMappings.Add("F25", "Recurso");
                        bulkCopy.DestinationTableName = "gastos_final";
                        bulkCopy.WriteToServer(dtexcel);
                        bulkCopy.BulkCopyTimeout = 1360;
                    }
                }
                catch (Exception ex)
                {
                    conexion = new SqlConnection();
                    try
                    {
                        query = "DELETE FROM gastos_version WHERE anio='" + Anio + "' AND tipo='" + Tipo + "' AND versi='" + Versi + @"';

INSERT INTO gastos_final 
SELECT Tipo,Orden,Cecoste,Macrofundo,Fundo,Sector,Turno,Objagrico,Zafra,Mes,Anio,Labores,Grp_Act,ActividadMaterial
,Denominacion,Material,UMB,Cantidad,MonSol,ValMonObj,MonUSD,ValMScCO,Actividades,Rubro,Recurso 
FROM gastos WHERE Tipo='" + Tipo + Anio.Substring(2, 2) + "' AND version='" + (Versi - 1) + @"';

DELETE FROM gastos WHERE Tipo='" + Tipo + Anio.Substring(2, 2) + "' AND version='" + (Versi - 1) + "'";

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