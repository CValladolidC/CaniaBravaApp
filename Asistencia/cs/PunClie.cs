using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Configuration;

namespace CaniaBrava
{
    class PunClie
    {
        public PunClie() { }

        public void updPunClie(string operacion, string codclie,string codpartida,string despartida,string codaux)
        {
            string query;
            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();

            if (operacion.Equals("AGREGAR"))
            {
                query = "INSERT INTO punclie (codclie,codpartida,despartida,codaux) VALUES ('" + @codclie + "', ";
                query=query+"'" + @codpartida + "', '" + @despartida + "','"+@codaux+"');";
            }
            else
            {
                query = "UPDATE punclie SET despartida='" + @despartida + "',codaux='"+@codaux+"' ";
                query = query + "WHERE codclie='" + @codclie + "' and codpartida='" + @codpartida + "';";
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

        public void delPunClie(string codclie,string codpartida)
        {
            string query;
            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();
            query = "DELETE from punclie WHERE codclie='" + @codclie + "' and codpartida='"+@codpartida+"';";
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

        public string genCodPunto(string codclie)
        {
            Funciones funciones = new Funciones();
            string codigoInterno = "001";
            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();
            string query = "select isnull(max(codpartida)) as existencia,";
            query = query + "max(codpartida)+1 as codigointerno from punclie where codclie='"+@codclie+"'; ";
            try
            {
                SqlCommand myCommand = new SqlCommand(query, conexion);
                SqlDataReader myReader = myCommand.ExecuteReader();
                if (myReader.Read())
                {
                    if (myReader.GetString(myReader.GetOrdinal("existencia")).Trim().Equals("1"))
                    {
                        codigoInterno = "001";
                    }
                    else
                    {
                        string codigo = myReader.GetInt32(myReader.GetOrdinal("codigointerno")).ToString();
                        codigoInterno = funciones.replicateCadena("0", 3 - codigo.Trim().Length) + codigo;
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
            return codigoInterno;

        }

        public string ui_getDatos(string codclie, string codpartida,string dato)
        {
            string resultado = string.Empty;


            if (codpartida != string.Empty)
            {
                SqlConnection conexion = new SqlConnection();
                conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
                conexion.Open();

                string query = " Select codpartida,despartida,codaux from punclie  ";
                query = query + " where codclie='" + @codclie + "' and codpartida='" + @codpartida + "'";
                try
                {
                    SqlCommand myCommand = new SqlCommand(query, conexion);
                    SqlDataReader myReader = myCommand.ExecuteReader();

                    if (myReader.Read())
                    {
                        if (dato.Equals("CODAUX"))
                        {
                            resultado = myReader.GetString(myReader.GetOrdinal("codaux"));
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
            }
            return resultado;

        }

    }
}
