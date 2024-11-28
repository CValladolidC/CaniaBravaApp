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
    class Clie
    {
         public Clie() { }

        public string genCodClie()
        {
            Funciones funciones = new Funciones();
            string codigoInterno = "00001";
            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();
            string query = "select isnull(max(codclie)) as existencia,";
            query = query + "max(codclie)+1 as codigointerno from clie; ";
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

        public string getNumeroRegistrosClie()
        {
            string numero = "0";
            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();
            string squery = "select count(*) as numero from clie;";
            try
            {
                SqlCommand myCommand = new SqlCommand(squery, conexion);
                SqlDataReader myReader = myCommand.ExecuteReader();

                if (myReader.Read())
                {
                    numero = myReader.GetString(myReader.GetOrdinal("numero"));
                }

            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);

            }

            conexion.Close();
            return numero;
        }

        public void updClie(string operacion,string codclie,string desclie,
            string rucclie,string dniclie,string dirclie1,string dirclie2,string dirclie3,
            string nomcontac,string localidad,string pais,string telcontac1,string telcontac2,
            string telcontac3,string cargocontac,string mailcontac,string comentario,string faxcontac,
            string estado,string fcrea,string fmod,string usuario,string tippreven)
        {
            string query;
            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();
            if (operacion.Equals("AGREGAR"))
            {
                query = "INSERT INTO clie (codclie,desclie,rucclie,dniclie,dirclie1,dirclie2,dirclie3,";
                query = query + "nomcontac,mailcontac,localidad,pais,telcontac1,telcontac2,telcontac3,cargocontac,comentario,";
                query = query + "faxcontac,estado,fcrea,fmod,usuario,tippreven) ";
                query = query + "VALUES ('" + @codclie + "', '" + @desclie + "', '" + @rucclie + "', ";
                query = query + "'" + @dniclie + "','" + @dirclie1 + "','" + @dirclie2 + "','" + @dirclie3 + "','" + @nomcontac + "','"+@mailcontac+"',";
                query = query + "'" + @localidad + "','" + @pais + "','" + @telcontac1 + "','" + @telcontac2 + "','" + @telcontac3 + "',";
                query = query + "'" + @cargocontac + "','" + @comentario + "','" + @faxcontac + "','" + @estado + "','" + @fcrea + "',";
                query = query + "'" + @fmod + "','" + @usuario + "','"+@tippreven+"');";
            }
            else
            {
                query = "UPDATE clie SET desclie='" + @desclie + "',rucclie='" + @rucclie + "'";
                query = query + " ,dniclie='" + @dniclie + "',dirclie1='" + @dirclie1 + "',";
                query = query + " dirclie2='"+@dirclie2+"',dirclie3='" + @dirclie3 + "',nomcontac='"+@nomcontac+"', ";
                query = query + " mailcontac='"+@mailcontac+"',localidad='" + @localidad + "',pais='" + @pais + "',telcontac1='" + @telcontac1 + "',telcontac2='" + @telcontac2 + "',";
                query = query + " telcontac3='" + @telcontac3 + "',cargocontac='" + @cargocontac + "',comentario='" + @comentario + "',";
                query = query + " faxcontac='" + @faxcontac + "',estado='" + @estado + "',fmod='" + @fmod + "',";
                query = query + " usuario='" + @usuario + "',tippreven='" + @tippreven + "' ";
                query = query + " WHERE codclie='" + @codclie + "';";
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

        public void delClie(string codclie)
        {
            string query;
            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();
            query = "DELETE from clie WHERE codclie='" + @codclie + "';";
          
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

        public string ui_getDatos(string codclie, string dato)
        {
            string query;
            string resultado = string.Empty;

            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();

            query = "Select * from clie where (codclie='" + @codclie + "' || rucclie='" + @codclie + "') ;";

            try
            {
                SqlCommand myCommand = new SqlCommand(query, conexion);
                SqlDataReader myReader = myCommand.ExecuteReader();

                if (myReader.Read())
                {
                    
                    if (dato.Equals("CODIGO"))
                    {
                        resultado = myReader.GetString(myReader.GetOrdinal("codclie"));
                    }

                    if (dato.Equals("RUC"))
                    {
                        resultado = myReader.GetString(myReader.GetOrdinal("rucclie"));
                    }

                    if (dato.Equals("NOMBRE"))
                    {
                        resultado = myReader.GetString(myReader.GetOrdinal("desclie"));
                    }

                    if (dato.Equals("DIRECCION1"))
                    {
                        resultado = myReader.GetString(myReader.GetOrdinal("dirclie1"));
                    }

                    if (dato.Equals("DIRECCION2"))
                    {
                        resultado = myReader.GetString(myReader.GetOrdinal("dirclie2"));
                    }

                    if (dato.Equals("DIRECCION3"))
                    {
                        resultado = myReader.GetString(myReader.GetOrdinal("dirclie3"));
                    }

                    if (dato.Equals("TIPOPRECIO"))
                    {
                        resultado = myReader.GetString(myReader.GetOrdinal("tippreven"));
                    }

                    if (dato.Equals("DNI"))
                    {
                        resultado = myReader.GetString(myReader.GetOrdinal("dniclie"));
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

        public bool verificaRucClie(string rucclie)
        {
            bool resultado = false;
            DataTable dtclie = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection();
                conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
                conexion.Open();
                string query = "Select * from clie where rucclie='" + @rucclie + "' ;";
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = new SqlCommand(query, conexion);
                da.Fill(dtclie);
                if (dtclie.Rows.Count > 0)
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

    }
}
