using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Configuration;

namespace CaniaBrava
{
    class DetProdDes
    {
       public DetProdDes() { }

       public void actualizarDetProdDes(string idproddes,string idcia,string idtipoplan,string conplancan,string conplanimp,string idtipocal)
        {
            string query;
            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();

            query = "INSERT INTO detproddes(idproddes,idcia,idtipoplan,conplancan,conplanimp,idtipocal) VALUES ('" + @idproddes + "', '" + @idcia + "', '" + @idtipoplan + "','" + @conplancan + "','" + @conplanimp + "','"+@idtipocal+"');";
                     
            try
            {
                SqlCommand myCommand = new SqlCommand(query, conexion);
                myCommand.ExecuteNonQuery();
                myCommand.Dispose();

            }
            catch (SqlException) {}
            conexion.Close();

        }

       public void eliminarDetProdDes(string idproddes, string idcia,string idtipoplan,string idtipocal)
        {
            string query;
            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();

            query = "DELETE from detproddes WHERE idproddes='" + @idproddes + "' and idcia='" + @idcia + "' and idtipoplan='"+@idtipoplan+"' and idtipocal='"+@idtipocal+"';";

            try
            {
                SqlCommand myCommand = new SqlCommand(query, conexion);
                myCommand.ExecuteNonQuery();
                myCommand.Dispose();

            }
            catch (SqlException)
            {}
            conexion.Close();

        }
    }
}