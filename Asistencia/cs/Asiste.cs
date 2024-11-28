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
    public class Asiste
    {
        public string Codigo { get; set; }
        public string Trabajador { get; set; }
        public string Empresa { get; set; }
        public string Gerencia { get; set; }
        public string Area { get; set; }
        public string Nomina { get; set; }
        public string TipHorario { get; set; }
        public string Fecha { get; set; }
        public string IngresoProg { get; set; }
        public string SalidaProg { get; set; }
        public string IngresoReal { get; set; }
        public string SalidaReal { get; set; }
        public string Movimientos { get; set; }
        public string Sedes { get; set; }
        public string HrTrab { get; set; }
        public string HrExt { get; set; }
        public string Comentarios { get; set; }
        public string Comentarios2 { get; set; }
        public string nromov { get; set; }

        public bool getAsiste(string idperplan, string idcia, string anio, string messem, string idtipoper, string idtipoplan, string idtipocal, string fecha)
        {
            bool estado = false;

            try
            {
                DataTable dt = new DataTable();
                SqlConnection conexion = new SqlConnection();
                conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
                conexion.Open();
                string query = " select * from asiste WHERE idcia='" + @idcia + "' and idperplan='" + @idperplan + "' ";
                query = query + " and anio='" + @anio + "' and messem='" + @messem + "' ";
                query = query + " and idtipoper='" + @idtipoper + "' and idtipocal='" + @idtipocal + "' ";
                query = query + " and idtipoplan='" + @idtipoplan + "' and fecha=STR_TO_DATE('" + @fecha + "', '%d/%m/%Y'); ";
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = new SqlCommand(query, conexion);
                da.Fill(dt);

                if (dt.Rows.Count > 0) { estado = true; }
                conexion.Close();
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            return estado;
        }

        public void delAsiste(string idperplan, string idcia, string anio, string messem, string idtipoper, string idtipoplan, string idtipocal, string fecha)
        {
            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();

            string query = " DELETE FROM asiste WHERE idperplan='" + @idperplan + "' and idcia='" + @idcia + "' ";
            query = query + " and anio='" + @anio + "' and messem='" + @messem + "' and idtipoper='" + @idtipoper + "' ";
            query = query + " and idtipoplan='" + @idtipoplan + "' and idtipocal='" + @idtipocal + "' ";
            query = query + " and fecha=STR_TO_DATE('" + @fecha + "', '%d/%m/%Y'); ";
           
            try
            {
                SqlCommand myCommand = new SqlCommand(query, conexion);
                myCommand.ExecuteNonQuery();
                myCommand.Dispose();
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);

            }
            conexion.Close();
        }

        public void updAsiste(string idperplan, string idcia, string anio, string messem, string idtipoper, string idtipoplan, string idtipocal, string fecha)
        {
            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();
                        
            string query = " INSERT INTO asiste (idperplan,idcia,anio,messem,idtipoper,idtipoplan,idtipocal,fecha) ";
            query = query + " VALUES ('" + @idperplan + "', '" + @idcia + "',";
            query = query + " '" + @anio + "','" + @messem + "','" + @idtipoper + "','" + @idtipoplan + "',";
            query = query + " '" + @idtipocal + "',STR_TO_DATE('" + @fecha + "', '%d/%m/%Y'));";

            try
            {
                SqlCommand myCommand = new SqlCommand(query, conexion);
                myCommand.ExecuteNonQuery();
                myCommand.Dispose();
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);

            }
            conexion.Close();
        }
    }
}