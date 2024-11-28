using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace CaniaBrava
{
    class ConFijos
    {
        public ConFijos() { }

        public string getNumeroRegistros(string idtipoplan,string idtipoper,string idtipocal)
        {
            string numero = "0";
            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();
            string squery = "select count(*) as numero from confijos where idtipocal='"+@idtipocal+"' and idtipoplan='" + @idtipoplan + "' and idtipoper='"+@idtipoper+"';";
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
                MessageBox.Show(e.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            conexion.Close();
            return numero;
        }

        public void actualizarConFijos(string soperacion, string idconplan,string idtipoplan,string idtipoper,string idcia,string idperplan,string tipocalculo,float valor,string idtipocal)
        {
            
            string squery;

            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();

            if (soperacion.Equals("AGREGAR"))
            {
                squery = "INSERT INTO confijos (idconplan,idtipoplan,idtipoper,idcia,idperplan,tipocalculo,valor,idtipocal) VALUES ('" + @idconplan + "', '" + @idtipoplan + "', '" + @idtipoper + "', '" + @idcia + "','" + @idperplan + "','"+@tipocalculo+"','"+@valor+"','"+@idtipocal+"');";
            }
            else
            {
                squery = "UPDATE confijos SET tipocalculo='" + @tipocalculo + "',valor='" + @valor + "' WHERE idconplan='" + @idconplan + "' and idtipoplan='" + @idtipoplan + "' and idtipoper='"+@idtipoper+"' and idperplan='"+@idperplan+"' and idcia='"+@idcia+"' and idtipocal='"+@idtipocal+"';";
            }
            try
            {
                SqlCommand myCommand = new SqlCommand(squery, conexion);
                myCommand.ExecuteNonQuery();
                myCommand.Dispose();

            }
            catch (SqlException)
            {}

            conexion.Close();

        }

        public void eliminarConFijos(string idconplan, string idtipoplan, string idtipoper, string idcia, string idperplan,string idtipocal)
        {
            string squery;
          
            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();
            squery = "DELETE from confijos WHERE idconplan='" + @idconplan + "' and idtipoplan='" + @idtipoplan + "' and idtipoper='"+@idtipoper+"' and idcia='"+@idcia+"' and idperplan='"+@idperplan+"' and idtipocal='"+@idtipocal+"';";
            

            try
            {
                SqlCommand myCommand = new SqlCommand(squery, conexion);
                myCommand.ExecuteNonQuery();
                myCommand.Dispose();
  
            }
            catch (SqlException)
            {}
            conexion.Close();

        }
    }
}