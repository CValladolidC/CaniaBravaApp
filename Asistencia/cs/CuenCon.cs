using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace CaniaBrava
{
    class CuenCon
    {
        public void actualizarCuenCon(string operacion, string codcuenta, string descuenta,
            string detallado, string idcia,string modoane,string tipane,string ane, string modoaneref,string tipaneref,string aneref)
        {
            string query;
            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();
            if (operacion.Equals("AGREGAR"))
            {
                query = "INSERT INTO cuencon (codcuenta,descuenta,detallado,idcia,modoane,tipane,ane,";
                query=query+" modoaneref,tipaneref,aneref) ";
                query = query + "VALUES ('" + @codcuenta + "', '" + @descuenta + "', '" + @detallado + "', ";
                query = query + " '" + @idcia + "','"+@modoane+"','"+@tipane+"','"+@ane+"',";
                query=query+" '"+@modoaneref+"','"+@tipaneref+"','"+@aneref+"');";
            }
            else
            {
                query = "UPDATE cuencon SET descuenta='" + @descuenta + "',";
                query = query + " detallado='" + @detallado + "',modoane='" + @modoane + "', ";
                query = query + " tipane='" + @tipane + "', ane='" + @ane + "',modoaneref='" + @modoaneref + "',";
                query = query + " tipaneref='" + @tipaneref + "',aneref='" + @aneref + "'";
                query = query + " WHERE idcia='" + @idcia + "' ";
                query = query + " and codcuenta='" + @codcuenta + "';";
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

        public void eliminarCuenCon(string codcuenta, string idcia)
        {
            string query;
            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();
            query = "DELETE from cuencon WHERE codcuenta='" + @codcuenta + "' ";
            query = query + "and idcia='" + @idcia + "';";
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

        public string ui_getDatosCuenCon(string idcia, string codcuenta,string datossolicitado)
        {
            string query;
            string resultado = string.Empty;
            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();
            query = " Select * from cuencon where idcia='" + @idcia + "' and s_estado = 1 ";
            query += "and codcuenta='" + @codcuenta + "' ;";
            try
            {
                SqlCommand myCommand = new SqlCommand(query, conexion);
                SqlDataReader myReader = myCommand.ExecuteReader();

                if (myReader.Read())
                {
                    if (datossolicitado.Equals("DETALLADO"))
                    {
                        resultado = myReader["detallado"].ToString();
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