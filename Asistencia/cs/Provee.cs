using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Data;

namespace CaniaBrava
{
    class Provee
    {
        public Provee() { }

        public string genCodProvee()
        {
            Funciones funciones = new Funciones();
            string codigoInterno = "00001";
            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();
            string query = "select isnull(max(codprovee),'1') as existencia,";
            query = query + "max(codprovee)+1 as codigointerno from provee; ";
            try
            {
                SqlCommand myCommand = new SqlCommand(query, conexion);
                SqlDataReader myReader = myCommand.ExecuteReader();
                if (myReader.Read())
                {
                    if (myReader.GetString(myReader.GetOrdinal("existencia")).Trim().Equals("1"))
                    {
                        codigoInterno = "00001";
                    }
                    else
                    {
                        string codigo = myReader.GetInt32(myReader.GetOrdinal("codigointerno")).ToString();
                        codigoInterno = funciones.replicateCadena("0", 5 - codigo.Trim().Length) + codigo;
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

        public bool verificaRuc(string ruc)
        {
            bool resultado = false;
            DataTable dtprovee = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection();
                conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
                conexion.Open();
                string query = "Select * from provee where ruc='" + @ruc + "' ;";
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = new SqlCommand(query, conexion);
                da.Fill(dtprovee);
                if (dtprovee.Rows.Count > 0)
                {
                    resultado = true;
                }
                conexion.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            return resultado;
        }

        public string getNumeroRegistrosProvee()
        {
            string numero = "0";
            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();
            string squery = "select count(*) as numero from provee;";
            try
            {
                SqlCommand myCommand = new SqlCommand(squery, conexion);
                SqlDataReader myReader = myCommand.ExecuteReader();

                if (myReader.Read())
                {
                    numero = myReader.GetInt32(myReader.GetOrdinal("numero")).ToString();
                }

            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);

            }

            conexion.Close();
            return numero;
        }

        public void updProvee(string operacion, string codprovee, string desprovee, string ruc,
            string website, string fcrea, string fmod, string usuario, string estado, string dirprovee)
        {
            string query;
            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();
            if (operacion.Equals("AGREGAR"))
            {
                query = "INSERT INTO provee (codprovee,desprovee,ruc,website,fcrea,fmod,usuario,estado,dirprovee) ";
                query = query + "VALUES ('" + @codprovee + "', '" + @desprovee + "', '" + @ruc + "', ";
                query = query + "'" + @website + "','" + @fcrea + "','" + @fmod + "','" + @usuario + "','" + @estado + "','" + @dirprovee + "');";
            }
            else
            {
                query = "UPDATE provee SET desprovee='" + @desprovee + "',ruc='" + @ruc + "'";
                query = query + " ,website='" + @website + "',fmod='" + @fmod + "',";
                query = query + " usuario='" + @usuario + "',estado='" + @estado + "',dirprovee='" + @dirprovee + "' ";
                query = query + " WHERE codprovee='" + @codprovee + "';";
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

        public void delProvee(string codprovee)
        {
            string query;
            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();
            query = "DELETE from provee WHERE codprovee='" + @codprovee + "';";
            query = query + "DELETE from estanepro WHERE codprovee='" + @codprovee + "';";
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

        public string ui_getDatos(string codprovee, string dato)
        {
            string query;
            string resultado = string.Empty;

            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();

            query = "Select * from provee where (codprovee='" + @codprovee + "' OR ruc='" + @codprovee + "');";
            try

            {
                SqlCommand myCommand = new SqlCommand(query, conexion);
                SqlDataReader myReader = myCommand.ExecuteReader();

                if (myReader.Read())
                {
                    if (dato.Equals("CODIGO"))
                    {
                        resultado = myReader.GetString(myReader.GetOrdinal("codprovee"));
                    }

                    if (dato.Equals("NOMBRE"))
                    {
                        resultado = myReader.GetString(myReader.GetOrdinal("desprovee"));
                    }

                    if (dato.Equals("RUC"))
                    {
                        resultado = myReader.GetString(myReader.GetOrdinal("ruc"));
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