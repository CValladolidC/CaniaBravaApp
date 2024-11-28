using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace CaniaBrava
{
    class ManteTablas
    {
        public string mantenimientoTabla(string nombreTabla,string tituloTabla)
        {
            string query;
            string mensaje;
            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();
            query = "Optimize table "+@nombreTabla+";";
            try
            {
                SqlCommand myCommand = new SqlCommand(query, conexion);
                myCommand.ExecuteNonQuery();
                myCommand.Dispose();
                mensaje = tituloTabla + " optimize status OK";
            }
            catch (SqlException ex)
            {
                mensaje = ex.Message;
            }
            conexion.Close();
            return mensaje;

        }
    }
}