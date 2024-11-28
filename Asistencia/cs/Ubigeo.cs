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
    class Ubigeo
    {
        public Ubigeo() { }

        public string consultaCodigoUbigeo(string departamento, string provincia, string distrito)
        {
            string ubigeo=String.Empty;

            string squery = "SELECT ubigeo FROM ubigeo WHERE departam='" + @departamento + "' and provincia='" + @provincia + "' and distrito='" + @distrito + "';";

            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();

            try
            {

                SqlCommand myCommand_table = new SqlCommand(squery, conexion);
                DataTable dt_table = new DataTable();
                dt_table.Load(myCommand_table.ExecuteReader());
                myCommand_table.Dispose();

                if (dt_table.Rows.Count > Convert.ToInt16(0))
                {

                    ubigeo = (String)dt_table.Rows[0]["ubigeo"];
                 
                }
                else
                {
                    ubigeo = String.Empty;
                }
                
                
            }
            catch (SqlException ex)
            {
                MessageBox.Show("A ocurrido un error en la consulta [" + ex.Message + "]", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);

            }

            conexion.Close();
            
            return ubigeo;
            

        }
    }
}