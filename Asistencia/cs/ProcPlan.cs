using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Configuration;
using System.Windows.Forms;

namespace CaniaBrava
{
    class ProcPlan
    {
        public ProcPlan() { }

        public void agregarProcPlan(string idconplan, string constante, float valor, string tipo, string grupo, string idcia,
            string idperplan, string idtipoper, string idtipoplan, string idtipocal, string anio, string messem, string regcero)
        {
            string query;
            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();
            query = "INSERT INTO procplan (idconplan,constante,valor,tipo,grupo,idcia,";
            query = query + " idperplan,idtipoper,idtipoplan,idtipocal,anio,messem) ";
            query = query + " VALUES ('" + @idconplan + "','" + @constante + "','" + @valor + "', ";
            query = query + "'" + @tipo + "','" + @grupo + "','" + @idcia + "','" + @idperplan + "', ";
            query = query + "'" + @idtipoper + "','" + @idtipoplan + "','" + @idtipocal + "','" + @anio + "','" + @messem + "');";
            try
            {
                SqlCommand myCommand = new SqlCommand(query, conexion);
                myCommand.ExecuteNonQuery();
                myCommand.Dispose();
                if (tipo.Equals("C"))
                {
                    if (regcero == "S" || (regcero == "N" && valor > 0))
                    {
                        ConBol conbol = new ConBol();
                        conbol.actualizarConBol(idperplan, idcia, anio, messem, idtipoper, idtipocal, idtipoplan, idconplan, valor);
                    }
                }
            }
            catch (SqlException ex) { MessageBox.Show(ex.Message); }
            conexion.Close();
        }

         public void eliminarProcPlan(string idcia,string idtipoper, string idtipoplan, string idtipocal, string anio, string messem)
         {
            string query;
            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();
            query = "DELETE from procplan where idcia='" + @idcia + "' and ";
            query=query+ " idtipoper='" + @idtipoper + "' and idtipoplan='"+@idtipoplan+"' ";
            query = query + " and idtipocal='" + @idtipocal + "' and ";
            query = query + " anio='" + @anio + "' and messem='" + @messem + "';";
            try
            {
                SqlCommand myCommand = new SqlCommand(query, conexion);
                myCommand.ExecuteNonQuery();
                myCommand.Dispose();
            }
            catch (SqlException){}
            conexion.Close();
        }

         public void eliminarProcPlanGeneral()
         {
             string query;
             SqlConnection conexion = new SqlConnection();
             conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
             conexion.Open();
             query = "DELETE from procplan ";
             try
             {
                 SqlCommand myCommand = new SqlCommand(query, conexion);
                 myCommand.ExecuteNonQuery();
                 myCommand.Dispose();
             }
             catch (SqlException) { }
             conexion.Close();
         }

         public void eliminarProcPlanPersona(string idcia, string idperplan, string idtipoper, string idtipoplan, string idtipocal, string anio, string messem)
         {
             string query;
             SqlConnection conexion = new SqlConnection();
             conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
             conexion.Open();
             query = "DELETE from procplan where idcia='" + @idcia + "' and ";
             query += " idtipoper='" + @idtipoper + "' and idtipoplan='" + @idtipoplan + "' ";
             query += " and idtipocal='" + @idtipocal + "' and ";
             query += " anio='" + @anio + "' and messem='" + @messem + "' ";
             query += " and idperplan='" + @idperplan + "'; ";
             try
             {
                 SqlCommand myCommand = new SqlCommand(query, conexion);
                 myCommand.ExecuteNonQuery();
                 myCommand.Dispose();
             }
             catch (SqlException) { }
             conexion.Close();
         }
    }
}