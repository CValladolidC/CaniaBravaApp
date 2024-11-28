using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Windows.Forms;

namespace CaniaBrava
{
    class Datasis
    {
        public string getDatasis(string idperplan, string idcia, string anio, string messem, string idtipoper, string idtipoplan, string idtipocal,string dato)
        {
            string resultado = "0";

            try
            {
                DataTable dt = new DataTable();
                SqlConnection conexion = new SqlConnection();
                conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
                conexion.Open();
                string query = " select * from datasis WHERE idcia='" + @idcia + "' and idperplan='" + @idperplan + "' ";
                query = query + " and anio='" + @anio + "' and messem='" + @messem + "' ";
                query = query + " and idtipoper='" + @idtipoper + "' and idtipocal='" + @idtipocal + "' ";
                query = query + " and idtipoplan='" + @idtipoplan + "'; ";
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = new SqlCommand(query, conexion);
                da.Fill(dt);

                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow row_dt in dt.Rows)
                    {
                        if (dato.Equals("DIURNO"))
                        {
                            resultado = row_dt["diurno"].ToString();
                        }

                        if (dato.Equals("NOCTURNO"))
                        {
                            resultado = row_dt["nocturno"].ToString();
                        }

                        if (dato.Equals("DIASLAB"))
                        {
                            resultado = row_dt["diaslab"].ToString();
                        }

                        if (dato.Equals("HE25"))
                        {
                            resultado = row_dt["he25"].ToString();
                        }

                        if (dato.Equals("HE35"))
                        {
                            resultado = row_dt["he35"].ToString();
                        }

                        if (dato.Equals("HE100"))
                        {
                            resultado = row_dt["he100"].ToString();
                        }

                    }
                }
               
                conexion.Close();
            }

            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            return resultado;
        }

        public void delDatasis(string idperplan,string idcia, string anio, string messem, string idtipoper, string idtipoplan, string idtipocal)
        {
            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();
            string query =  " DELETE FROM datasis WHERE idcia='" + @idcia + "' ";
            query = query + " and anio='" + @anio + "' and messem='" + @messem + "' and idtipoper='" + @idtipoper + "' ";
            query = query + " and idtipoplan='" + @idtipoplan + "' and ";
            query = query + " idtipocal='" + @idtipocal + "' and idperplan='" + @idperplan + "' ; ";
            try
            {
                SqlCommand myCommand = new SqlCommand(query, conexion);
                myCommand.ExecuteNonQuery();
                myCommand.Dispose();
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);

            }
            conexion.Close();
        }

        public void updDatasis(string idperplan, string idcia, string anio, string messem, string idtipoper, string idtipoplan, 
            string idtipocal, int diurno,int nocturno,int diaslab, decimal he25,decimal he35,decimal he100)
        {

            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();

            string query = " DELETE FROM datasis WHERE idperplan='" + @idperplan + "' and idcia='" + @idcia + "' ";
            query = query + " and anio='" + @anio + "' and messem='" + @messem + "' and idtipoper='" + @idtipoper + "' ";
            query = query + " and idtipoplan='" + @idtipoplan + "' and idtipocal='" + @idtipocal + "'; ";
            query = query + " INSERT INTO datasis (idperplan,idcia,anio,messem,idtipoper,idtipoplan,";
            query = query + " idtipocal,diurno,nocturno,diaslab,he25,he35,he100) ";
            query = query + " VALUES ('" + @idperplan + "', '" + @idcia + "',";
            query = query + " '" + @anio + "','" + @messem + "','" + @idtipoper + "','" + @idtipoplan + "',";
            query = query + " '" + @idtipocal + "','" + diurno + "','" + @nocturno + "','" + @diaslab + "',";
            query = query + " '" + @he25 + "','" + @he35 + "','" + @he100 + "');";

            try
            {
                SqlCommand myCommand = new SqlCommand(query, conexion);
                myCommand.ExecuteNonQuery();
                myCommand.Dispose();
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);

            }
            conexion.Close();
        }
    }
}