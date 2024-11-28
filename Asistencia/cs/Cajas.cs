using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Configuration;
using System.Windows.Forms;

namespace CaniaBrava
{
    class Cajas
    {
        public Cajas() { }

        public void updCajas(string operacion,string codcia,string codcaja,string descaja,
            string codalma,string codmov,string codmovguia)
        {
            string query;
            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();

            if (operacion.Equals("AGREGAR"))
            {
                query = "INSERT INTO cajas (codcia,codcaja,descaja,codalma,codmov,codmovguia) VALUES ('" + @codcia + "', ";
                query=query+"'" + @codcaja + "', '" + @descaja + "','"+@codalma+"','"+@codmov+"','"+@codmovguia+"');";
            }
            else
            {
                query = "UPDATE cajas SET descaja='" + @descaja + "',codalma='"+@codalma+"',codmov='"+@codmov+"',codmovguia='"+@codmovguia+"' ";
                query = query + " WHERE codcia='" + @codcia + "' and codcaja='" + @codcaja + "';";
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

        public void delCajas(string codcia,string codcaja)
        {
            string query;
            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();
            query = "DELETE from cajas WHERE codcia='" + @codcia + "' and codcaja='"+@codcaja+"';";
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

        public string ui_getDatos(string codcia, string codcaja,string dato)
        {
            string query;
            string resultado = string.Empty;

            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();

            query = "Select A.codalma,A.codmov,B.desalma,A.codmovguia from cajas A ";
            query = query + " left join alalma B on A.codcia=B.codcia and A.codalma=B.codalma where ";
            query = query + " A.codcia='" + @codcia + "' and A.codcaja='" + @codcaja + "' ;";

            try
            {
                SqlCommand myCommand = new SqlCommand(query, conexion);
                SqlDataReader myReader = myCommand.ExecuteReader();

                if (myReader.Read())
                {

                    if (dato.Equals("ALMAFAC"))
                    {
                        resultado = myReader.GetString(myReader.GetOrdinal("codalma"));
                    }

                    if (dato.Equals("DESALMAFAC"))
                    {
                        resultado = myReader.GetString(myReader.GetOrdinal("desalma"));
                    }

                    if (dato.Equals("MOVFAC"))
                    {
                        resultado = myReader.GetString(myReader.GetOrdinal("codmov"));
                    }

                    if (dato.Equals("MOVGUIA"))
                    {
                        resultado = myReader.GetString(myReader.GetOrdinal("codmovguia"));
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
    }
}
