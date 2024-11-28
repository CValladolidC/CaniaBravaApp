using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace CaniaBrava
{
    class LabRet
    {
        public LabRet() { }

        public void actualizarLabRet(string operacion, string idcia, string idlabret, string deslabret, string statelabret)
        {
            string query;
            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();

            if (operacion.Equals("AGREGAR"))
            {
                query = "INSERT INTO labret (idlabret, deslabret,statelabret,idcia) VALUES ('" + @idlabret + "', '" + @deslabret + "', '" + @statelabret+ "', '" + @idcia + "');";
            }
            else
            {
                query = "UPDATE labret SET deslabret='" + @deslabret + "', statelabper='" + @statelabret + "' WHERE idlabper='" + @idlabret + "' and idcia='"+@idcia+"' ;";
            }
            try
            {
                SqlCommand myCommand = new SqlCommand(query, conexion);
                myCommand.ExecuteNonQuery();
                myCommand.Dispose();
            }
            catch (SqlException)
            {
                MessageBox.Show("No se ha podido realizar el proceso de Actualización", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);

            }
            conexion.Close();
        }

        public void eliminarLabRet(string idlabret,string idcia)
        {
            string query;
            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();
            query = "DELETE from labret WHERE idlabret='" + @idlabret + "' and idcia='"+@idcia+"';";
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