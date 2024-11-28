using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace CaniaBrava
{
    class ConDataPlan
    {
        public ConDataPlan() { }

        public void actualizarConDataPlan(string soperacion, string idperplan, string idcia, string anio, string messem, string idtipoper, string idtipocal, string idtipoplan, string idconplan, string tipocalculo, float valor,string idpresper,string comen)
        {
            string squery;
            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();
            if (soperacion.Equals("AGREGAR"))
            {
                squery = "INSERT INTO condataplan (idperplan,idcia,anio,messem,idtipoper,idtipocal,idtipoplan,idconplan,tipocalculo,valor,idpresper,comen) VALUES ('" + @idperplan + "', '" + @idcia + "', '" + @anio + "', '" + @messem + "','" + @idtipoper + "','" + @idtipocal + "','" + @idtipoplan + "','" + @idconplan + "','" + @tipocalculo + "','" + @valor + "','"+@idpresper+"','"+@comen+"');";
            }
            else
            {
                squery = "UPDATE condataplan SET tipocalculo='" + @tipocalculo + "',valor='" + @valor + "',idpresper='"+@idpresper+"',comen='"+@comen+"' WHERE idcia='" + @idcia + "' and idperplan='" + @idperplan + "' and anio='" + @anio + "' and messem='" + @messem + "' and idtipoper='" + @idtipoper + "' and idtipocal='" + @idtipocal + "' and idtipoplan='" + @idtipoplan + "' and idconplan='" + @idconplan + "';";
            }
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

        public void eliminarConDataPlan(string idperplan, string idcia, string anio, string messem, string idtipoper, string idtipocal, string idtipoplan, string idconplan)
        {
            string squery;
            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();
            squery = "DELETE FROM condataplan WHERE idcia='" + @idcia + "' and idperplan='" + @idperplan + "' ";
            squery = squery + " and anio='" + @anio + "' and messem='" + @messem + "' and idtipoper='" + @idtipoper + "' ";
            squery = squery + " and idtipocal='" + @idtipocal + "' and idtipoplan='" + @idtipoplan + "' ";
            squery = squery + " and idconplan='" + @idconplan + "';";
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