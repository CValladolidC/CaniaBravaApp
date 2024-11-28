using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Configuration;
using System.Windows.Forms;

namespace CaniaBrava
{
    class RegLabCia
    {
        public RegLabCia() { }
                       
        public void actualizarRegLabCia(string idtipoplan,string idcia,int nppmobre, int nppmemp)
        {
            string squery;
            
            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();

            squery = "INSERT INTO reglabcia (idcia,idtipoplan,nppmemp,nppmobre) VALUES ('" + @idcia + "', '" + @idtipoplan + "','" + @nppmemp + "','" + @nppmobre + "');";
           
            try
            {
                SqlCommand myCommand = new SqlCommand(squery, conexion);
                myCommand.ExecuteNonQuery();
                myCommand.Dispose();

            }
            catch (SqlException)
            {
                MessageBox.Show("No se ha podido realizar el proceso de Actualización", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);

            }
            conexion.Close();

        }

        public void eliminarRegLabCia(string idcia,string idtipoplan)
        {
            string query;
            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();
            query = "DELETE from reglabcia WHERE idcia='" + @idcia + "' and idtipoplan='"+@idtipoplan+"';";
            query = query + "DELETE from calcia WHERE idcia='" + @idcia + "' and idtipoplan='" + @idtipoplan + "';";
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
    }
}