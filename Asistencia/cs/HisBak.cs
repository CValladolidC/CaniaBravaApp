using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace CaniaBrava
{
    class HisBak
    {
        public HisBak() { }
        
        public void actualizarHisBak(string usuario,string fecha,string file)
        {
            string squery;
          
            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();
            squery = "INSERT INTO hisbak (usuario,fecha,file) VALUES ('" + @usuario + "', STR_TO_DATE('" + @fecha + "', '%d/%m/%Y') , '" + @file + "');";
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
    }
}