using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Data;

namespace CaniaBrava
{
    class Remu
    {
        public Remu() { }

        public void actualizarRemu(string operacion, string idcia, string idperplan, string idremu, string fini, string ffin, float importe)
        {
            string squery;
            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();

            if (operacion.Equals("AGREGAR"))
            {
                squery = "INSERT INTO remu (idcia,idperplan,fini,importe,state) VALUES ('" + @idcia + "','" + @idperplan + "','" + @fini + "','" + @importe + "','V');";
            }
            else
            {
                string state = "C";
                squery = "UPDATE remu SET fini=" + "'" + @fini + "', ffin=" + "'" + @ffin + "'" + ",state='" + @state + "' WHERE idcia='" + @idcia + "' and idperplan='" + @idperplan + "' and idremu='" + @idremu + "';";
            }
            try
            {
                SqlCommand myCommand = new SqlCommand(squery, conexion);
                myCommand.ExecuteNonQuery();
                myCommand.Dispose();
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            conexion.Close();
        }

        public void eliminarRemu(string idcia, string idperplan,string idremu)
        {
            string query;
            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();

            query = "DELETE from remu WHERE idcia='" + @idcia + "' and idperplan='" + @idperplan + "' and idremu='"+@idremu+"' ;";

            
            try
            {
                SqlCommand myCommand = new SqlCommand(query, conexion);
                myCommand.ExecuteNonQuery();
                myCommand.Dispose();

            }
            catch (SqlException e)
            {
                MessageBox.Show(e.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);

            }

            conexion.Close();

        }

        public string consultarRemu(string idcia, string idperplan,string dato)
        {
            string resultado = string.Empty;
            if (dato.Equals("IMPORTE"))
            {
                 resultado = "0";
            }
            try
            {
                DataTable dt = new DataTable();
                SqlConnection conexion = new SqlConnection();
                conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
                conexion.Open();
                string query = "Select A.idperplan,G.importe,G.fini,G.ffin from perplan A ";
                query = query + "inner join remu G on A.idcia=G.idcia COLLATE Modern_Spanish_CI_AI and A.idperplan=G.idperplan COLLATE Modern_Spanish_CI_AI ";
                query = query + "and G.State='V' where A.idcia='" + @idcia + "' and A.idperplan='" + @idperplan + "' ;";
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = new SqlCommand(query, conexion);
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow row_dt in dt.Rows)
                    {
                        if (dato.Equals("IMPORTE"))
                        {
                            resultado = row_dt["importe"].ToString();
                        }
                        
                        if (dato.Equals("FINI"))
                        {
                            resultado = row_dt["fini"].ToString();
                        }
                    }
                }
                conexion.Close();
            }
            catch (SqlException e)
            {
                MessageBox.Show(e.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            return resultado;
        }
    }
}