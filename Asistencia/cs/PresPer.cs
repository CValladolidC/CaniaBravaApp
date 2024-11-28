using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Configuration;
using System.Windows.Forms;

namespace CaniaBrava
{
    class PresPer
    {
        public PresPer() { }

        public string generaCodigoInterno(string idcia)
        {

            Funciones funciones = new Funciones();
            string codigoInterno = "00001";
            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();
            string query = "select isnull(max(idpresper)) as existencia,max(idpresper)+1 as codigointerno from presper where (idcia='" + @idcia + "');";

            try
            {
                SqlCommand myCommand = new SqlCommand(query, conexion);
                SqlDataReader myReader = myCommand.ExecuteReader();

                if (myReader.Read())
                {

                    if (myReader["existencia"].Equals("1"))
                    {
                        codigoInterno = "00001";
                    }
                    else
                    {
                        string codigo = myReader["codigointerno"].ToString();
                        codigoInterno = funciones.replicateCadena("0", 5 - codigo.Trim().Length) + codigo;
                    }
                }
                else
                {
                    codigoInterno = "00001";
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
        
        public string getNumeroRegistros(string idcia,string idtipoper)
        {
            string numero = "0";
            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();
            string squery = "select count(*) as numero from presper A left join perplan B on A.idcia=B.idcia and A.idperplan=B.idperplan where A.idcia='" + @idcia + "' and B.idtipoper='"+@idtipoper+"';";
            try
            {
                SqlCommand myCommand = new SqlCommand(squery, conexion);
                SqlDataReader myReader = myCommand.ExecuteReader();

                if (myReader.Read())
                {
                    numero = myReader["numero"].ToString();
                }

            }
            catch (SqlException e)
            {
                MessageBox.Show(e.Message,"Aviso",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
            
            conexion.Close();
            return numero;
        }

        public void actualizarPresPer(string soperacion, string idpresper, string idcia, string idperplan, string fecha, float importe, float cuota,
            string motivo, string comen, string mon, string suspendido, string tipodocpres, string nrodocpres)
        {
            string squery;
            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();

            if (soperacion.Equals("AGREGAR"))
            {
                squery = "INSERT INTO presper(idpresper,idcia,idperplan,fecha,importe,cuota,motivo,comen,mon,suspendido,tipodocpres,nrodocpres) VALUES ('" + @idpresper + "', '" + @idcia + "', '" + @idperplan + "', " + " STR_TO_DATE('" + @fecha + "', '%d/%m/%Y') " + ",'" + @importe + "','" + @cuota + "','" + @motivo + "','" + @comen + "','" + @mon + "','" + @suspendido + "','" + @tipodocpres + "','" + @nrodocpres + "');";
            }
            else
            {
                squery = "UPDATE presper SET idperplan='" + @idperplan + "',fecha=" + " STR_TO_DATE('" + @fecha + "', '%d/%m/%Y') " + ",importe='" + @importe + "',cuota='" + @cuota + "',motivo='" + @motivo + "',comen='" + @comen + "',mon='" + @mon + "',suspendido='" + @suspendido + "',tipodocpres='" + @tipodocpres + "',nrodocpres='" + @nrodocpres + "' WHERE idcia='" + @idcia + "' and idpresper='" + @idpresper + "';";
            }
            try
            {
                SqlCommand myCommand = new SqlCommand(squery, conexion);
                myCommand.ExecuteNonQuery();
                myCommand.Dispose();

            }
            catch (SqlException)
            {
                MessageBox.Show("No se ha podido realizar el proceso de Actualización", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }

            conexion.Close();
        }

        public void eliminarPresPer(string idpresper,string idcia)
        {
            
            string squery;
            string squery_detalle;

            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();

            squery_detalle = "DELETE from detpresper WHERE  idpresper='" + @idpresper + "' and idcia='" + @idcia + "';";
            squery = "DELETE from presper WHERE idpresper='" + @idpresper + "' and idcia='"+@idcia+"';";

            try
            {
                SqlCommand myCommand_detalle = new SqlCommand(squery_detalle, conexion);
                myCommand_detalle.ExecuteNonQuery();
                myCommand_detalle.Dispose();

                SqlCommand myCommand = new SqlCommand(squery, conexion);
                myCommand.ExecuteNonQuery();
                myCommand.Dispose();

            }
            catch (SqlException)
            {
                MessageBox.Show("A ocurrido un error en el proceso de Eliminación", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);

            }
            
            conexion.Close();

        }

        public string ui_getDatosPresPer(string idcia, string idpresper, string datossolicitado)
        {
            string query;
            string resultado = string.Empty;
            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();
            query = "Select * from presper where idcia='" + @idcia + "' and idpresper='" + @idpresper + "'; ";
            try
            {
                SqlCommand myCommand = new SqlCommand(query, conexion);
                SqlDataReader myReader = myCommand.ExecuteReader();

                if (myReader.Read())
                {
                    if (datossolicitado.Equals("CODIGO"))
                    {
                        resultado = myReader["idpresper"].ToString();
                    }
                    if (datossolicitado.Equals("FECHA"))
                    {
                        resultado = myReader["fecha"].ToString();
                    }
                    if (datossolicitado.Equals("IMPORTE"))
                    {
                        resultado = myReader["importe"].ToString();
                    }
                    if (datossolicitado.Equals("CUOTA"))
                    {
                        resultado = myReader["cuota"].ToString();
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