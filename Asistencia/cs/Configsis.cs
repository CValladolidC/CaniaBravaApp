using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Configuration;
using System.Windows.Forms;

namespace CaniaBrava
{
    class Configsis
    {
        public void updConfigsis(string maxfilabol, string betweenbol, string nrobolpag, string maxfilabolwin)
        {
            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();
            string query =  " DELETE from configsis ;";
            query = query + " INSERT INTO configsis (maxfilabol, betweenbol, nrobolpag, maxfilabolwin)";
            query = query + " VALUES ('" + @maxfilabol + "', '" + @betweenbol + "',";
            query = query + " '" + @nrobolpag + "','"+@maxfilabolwin+"');";

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

        public string consultaConfigSis(string variable)
        {
            string valor = "";
            string strmaxfilabol = "";
            string strmaxfilabolwin = "";
            string strbetweenbol = "";
            string strnrobolpag = "";


            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();
            string query = "SELECT * FROM configsis;";
            try
            {
                SqlCommand myCommand = new SqlCommand(query, conexion);
                SqlDataReader myReader = myCommand.ExecuteReader();

                if (myReader.Read())
                {
                    strmaxfilabol = myReader["maxfilabol"].ToString();
                    strbetweenbol = myReader["betweenbol"].ToString();
                    strnrobolpag = myReader["nrobolpag"].ToString();
                    strmaxfilabolwin = myReader["maxfilabolwin"].ToString();
                }
            }
            catch (Exception) { MessageBox.Show("aquies el error"); }
            if (variable.Equals("MAXFILABOL"))
            {
                valor = strmaxfilabol;
            }

            if (variable.Equals("BETWEENBOL"))
            {
                valor = strbetweenbol;
            }

            if (variable.Equals("NROBOLPAG"))
            {
                valor = strnrobolpag;
            }
            if (variable.Equals("MAXFILABOLWIN"))
            {
                valor = strmaxfilabolwin;
            }
            conexion.Close();
            return valor;
        }
    }
}