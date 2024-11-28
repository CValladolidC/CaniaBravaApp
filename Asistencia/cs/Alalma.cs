using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Configuration;
using System.Windows.Forms;

namespace CaniaBrava
{
    class Alalma
    {
        public Alalma() { }
        public void updAlalma(string operacion,string codcia,string codalma,string desalma,string nrope,
            string nrops,string estado)
        {
            string query;
            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();

            if (operacion.Equals("AGREGAR"))
            {
                query = "INSERT INTO alalma (codcia,codalma,desalma,nrope,nrops,estado) ";
                query = query + "VALUES ('"+@codcia+"','" + @codalma + "', '" + @desalma + "', '" + @nrope + "', ";
                query=query+"'" + @nrops + "','"+@estado+"');";
            }
            else
            {
                query = "UPDATE alalma SET desalma='" + @desalma + "',nrope='" + @nrope + "'";
                query = query + " ,nrops='" + @nrops + "',estado='" + @estado + "' ";
                query = query + " WHERE /*codcia='"+@codcia+"' and */codalma='" + @codalma + "';";
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

        public void delAlalma(string codcia,string codalma)
        {
            string query;
            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();
            query = "DELETE from alalma WHERE codcia='"+@codcia+"' and codalma='" + @codalma + "';";
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

        public string ui_getDatos(string codcia,string codalma,string dato)
        {
            string resultado = string.Empty;
            if (codalma != string.Empty)
            {
                SqlConnection conexion = new SqlConnection();
                conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
                conexion.Open();

                string query = " Select * from alalma ";
                query = query + " where (codcia='"+@codcia+"' and codalma='" + @codalma + "')";
                try
                {
                    SqlCommand myCommand = new SqlCommand(query, conexion);
                    SqlDataReader myReader = myCommand.ExecuteReader();

                    if (myReader.Read())
                    {
                        if (dato.Equals("CODIGO"))
                        {
                            resultado = myReader.GetString(myReader.GetOrdinal("codalma"));
                        }

                        if (dato.Equals("DESCRIPCION"))
                        {
                            resultado = myReader.GetString(myReader.GetOrdinal("desalma"));
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
