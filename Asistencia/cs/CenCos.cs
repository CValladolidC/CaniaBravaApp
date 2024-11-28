using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Configuration;
using System.Windows.Forms;

namespace CaniaBrava
{
    class CenCos
    {
        public CenCos() { }

        public void actualizarCenCos(string operacion, string idcencos, string idcia, string descencos, string statecencos,string codaux)
        {
            string query;
            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();
            if (operacion.Equals("AGREGAR"))
            {
                query = "INSERT INTO cencos (idcencos,idcia,descencos,statecencos,codaux) ";
                query = query + "VALUES ('" + @idcencos + "', '" + @idcia + "', '" + @descencos + "' ";
                query = query + ", '" + @statecencos + "','"+@codaux +"');";
            }
            else
            {
                query = "UPDATE cencos SET descencos='" + @descencos + "',";
                query = query + "statecencos='" + @statecencos + "',codaux='" + @codaux + "' ";
                query = query + "WHERE idcia='" + @idcia + "' ";
                query = query + "and idcencos='" + @idcencos + "';";
            }
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

        public void eliminarCenCos(string idcencos,string idcia)
        {
            string query;
            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();
            query = "DELETE from cencos WHERE idcencos='" + @idcencos + "' ";
            query = query + "and idcia='" + @idcia + "';";

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