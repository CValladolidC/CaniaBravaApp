using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Configuration;
using System.Windows.Forms;

namespace CaniaBrava
{
    class SisParm
    {
        public SisParm() { }
        public string ui_getDatos(string dato)
        {
            string query;
            string resultado = string.Empty;

            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();

            query = "Select * from sisparm ;";

            try
            {
                SqlCommand myCommand = new SqlCommand(query, conexion);
                SqlDataReader myReader = myCommand.ExecuteReader();

                if (myReader.Read())
                {
                    if (dato.Equals("IGV"))
                    {
                        resultado = myReader.GetString(myReader.GetOrdinal("igv"));
                    }
                    if (dato.Equals("NOMMAIL"))
                    {
                        resultado = myReader.GetString(myReader.GetOrdinal("nommail"));
                    }
                    if (dato.Equals("SMTPMAIL"))
                    {
                        resultado = myReader.GetString(myReader.GetOrdinal("smtpmail"));
                    }
                    if (dato.Equals("USRMAIL"))
                    {
                        resultado = myReader.GetString(myReader.GetOrdinal("usrmail"));
                    }
                    if (dato.Equals("PASSMAIL"))
                    {
                        resultado = myReader.GetString(myReader.GetOrdinal("passmail"));
                    }
                    if (dato.Equals("MAXFILAFAC"))
                    {
                        resultado = myReader.GetString(myReader.GetOrdinal("maxfilafac"));

                    }
                    if (dato.Equals("MAXFILAGUIA"))
                    {
                        resultado = myReader.GetString(myReader.GetOrdinal("maxfilagr"));

                    }
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

        public void actualizarSisParm(string operacion, string idsisparm, string dessisparm, float valor)
        {
            string query;
         
            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();

            if (operacion.Equals("AGREGAR"))
            {
                query = "INSERT INTO sisparm (idsisparm,dessisparm,valor) VALUES ('" + @idsisparm + "', '" + @dessisparm + "', '" + @valor + "');";
            }
            else
            {
                query = "UPDATE sisparm SET dessisparm='" + @dessisparm + "',valor='" + @valor + "' WHERE idsisparm='" + @idsisparm + "';";
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
    }
}