using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace CaniaBrava
{
    class DetPresPer
    {
        public DetPresPer() { }

        public string generaCodigo(string idcia,string idpresper)
        {

            Funciones funciones = new Funciones();
            string codigoInterno = "001";
            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();
            string query = "select isnull(max(iddetpresper)) as existencia,max(iddetpresper)+1 as codigointerno from detpresper where (idcia='" + @idcia + "' and idpresper='"+@idpresper+"');";

            try
            {
                SqlCommand myCommand = new SqlCommand(query, conexion);
                SqlDataReader myReader = myCommand.ExecuteReader();

                if (myReader.Read())
                {

                    if (myReader["existencia"].Equals("1"))
                    {
                        codigoInterno = "001";
                    }
                    else
                    {
                        string codigo = myReader["codigointerno"].ToString();
                        codigoInterno = funciones.replicateCadena("0", 3 - codigo.Trim().Length) + codigo;
                    }
                }
                else
                {
                    codigoInterno = "001";
                }

                myReader.Close();
                myCommand.Dispose();

            }

            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }

            conexion.Close();

            return codigoInterno;

        }
        
        public void actualizarDetPresPer(string soperacion,string iddetpresper, string idpresper, string idcia, string tipo, float importe, string fecha, string comen)

        {
            string squery;
            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();

            if (soperacion.Equals("AGREGAR"))
            {
                squery = "INSERT INTO detpresper (iddetpresper,idpresper,idcia,tipo,importe,fecha,comen) VALUES ('" + @iddetpresper + "', '" + @idpresper + "', '" + @idcia + "', '" + @tipo + "','" + @importe + "'," + " STR_TO_DATE('" + @fecha + "', '%d/%m/%Y') " + ",'" + @comen + "');";
            }
            else
            {
                squery = "UPDATE detpresper SET tipo='" + @tipo + "',importe='" + @importe + "',fecha=" + " STR_TO_DATE('" + @fecha + "', '%d/%m/%Y') " + ",comen='" + @comen + "' WHERE iddetpresper='" + @iddetpresper + "' and idpresper='" + @idpresper + "' and idcia='" + @idcia + "';";
            }
            try
            {
            
                SqlCommand myCommand = new SqlCommand(squery, conexion);
                myCommand.ExecuteNonQuery();
                myCommand.Dispose();
                           
            }
            catch (SqlException )
            {
                MessageBox.Show("No se ha podido realizar el proceso de Actualización", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);

            }

            conexion.Close();

        }

        public void eliminarDetPresPer(string iddetpresper, string idpresper, string idcia)
        {
            string squery;
           
            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();

            squery = "DELETE from detpresper WHERE iddetpresper='" + @iddetpresper + "' and idpresper='" + @idpresper + "' and idcia='" + @idcia + "';";

            try
            {
            
                SqlCommand myCommand = new SqlCommand(squery, conexion);
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