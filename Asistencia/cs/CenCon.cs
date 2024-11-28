using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace CaniaBrava
{
    class CenCon
    {
        public CenCon() { }
        
        public void actualizarCenCon(string idcia, string idcencos, float porcentaje)
        {
            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();

            string query = "INSERT INTO cencon (idcia,idcencos,porcentaje) ";
            query = query + "VALUES ('" + @idcia + "', '" + @idcencos + "',";
            query = query + "'" + @porcentaje + "');";
            
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

        public void eliminarCenCon(string idcia,string idcencos)
        {
            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();
            string query = "DELETE from cencon WHERE idcia='" + @idcia + "' and idcencos='"+@idcencos+"';";
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