using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace CaniaBrava
{
    class Valoriza
    {
        public string getPrecioProm(string codcia, string alma, string codarti,string mes,string anio)
        {
            string resultado = "0";
            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();
            string query =  " Select A.promedio from valoriza A ";
            query = query + " where A.codcia='" + @codcia + "' and A.alma='" + @alma + "' ";
            query = query + " and A.codarti='"+@codarti+"' and A.mes='" + @mes + "' and A.anio='"+@anio+"' ";
            try
            {
                SqlCommand myCommand = new SqlCommand(query, conexion);
                SqlDataReader myReader = myCommand.ExecuteReader();
                if (myReader.Read())
                {
                    resultado = myReader.GetString(myReader.GetOrdinal("promedio"));
                }
                myReader.Close();
                myCommand.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            conexion.Close();
            return resultado;
        }

        public void updValoriza(string codcia, string alma, string codarti, string mes, string anio, decimal promedio)
        {
            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();
            string query = "INSERT INTO valoriza (codcia,alma,codarti,mes,anio,promedio) ";
            query = query + "VALUES ('" + @codcia + "','" + @alma + "', '" + @codarti + "', '" + @mes + "', ";
            query = query + "'" + @anio + "','" + @promedio + "');";
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

        public void delValoriza(string codcia, string alma, string mes, string anio)
        {
            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();
            string query =  "DELETE from valoriza WHERE codcia='" + @codcia + "' and alma='" + @alma + "'  ";
            query = query + " and mes='" + @mes + "' and anio='" + @anio + "';";
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
