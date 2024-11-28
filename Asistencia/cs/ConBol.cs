using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Configuration;

namespace CaniaBrava
{
    class ConBol
    {
        public ConBol() { }
        public void actualizarConBol(string idperplan, string idcia, string anio, 
            string messem, string idtipoper, string idtipocal, string idtipoplan, string idconplan, float valor)
        {
            string query;
            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();
            query = "INSERT INTO conbol (idperplan,idcia,anio,messem,idtipoper,idtipocal,";
            query = query + " idtipoplan,idconplan,valor) VALUES ('" + @idperplan + "' ";
            query = query + ", '" + @idcia + "', '" + @anio + "', '" + @messem + "'";
            query = query + ",'" + @idtipoper + "','" + @idtipocal + "',";
            query = query + "'" + @idtipoplan + "','" + @idconplan + "','" + @valor + "');";
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

        public void eliminarConBol(string idperplan, string idcia, string anio, string messem, string idtipoper, string idtipocal, string idtipoplan)
        {
            string query;
            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();
            query = "DELETE FROM conbol WHERE idcia='" + @idcia + "' and idperplan='" + @idperplan + "' ";
            query += " and anio='" + @anio + "' and messem='" + @messem + "' ";
            query += " and idtipoper='" + @idtipoper + "' and idtipocal='" + @idtipocal + "' ";
            query += " and idtipoplan='" + @idtipoplan + "';";
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

        public void eliminarConBolPeriodo(string idcia, string anio, string messem, string idtipoper, string idtipocal, string idtipoplan)
        {
            string query;
            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();
            query = "DELETE FROM conbol WHERE idcia='" + @idcia + "' and anio='" + @anio + "' ";
            query = query + " and messem='" + @messem + "' and idtipoper='" + @idtipoper + "' ";
            query=query+" and idtipocal='" + @idtipocal + "' and idtipoplan='" + @idtipoplan + "';";
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
    }
}