using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace CaniaBrava
{
    class CenPer
    {
        string idperplan;
        string idcia;
        string idcencos;
        float porcentaje;
        public CenPer() { }

        public void setCenPer(string idperplan,string idcia,string idcencos,float porcentaje)
        {
            this.idperplan = idperplan;
            this.idcia = idcia;
            this.idcencos = idcencos;
            this.porcentaje = porcentaje;
        }

        public void actualizarCenPer()
        {
            string idperplan=this.idperplan;
            string idcia = this.idcia;
            string idcencos = this.idcencos;
            float porcentaje = this.porcentaje;

            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();

            string query = "INSERT INTO cenper (idperplan,idcia,idcencos,porcentaje) VALUES ('" + @idperplan + "', '" + @idcia + "', '" + @idcencos + "','"+@porcentaje+"');";
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

        public void eliminarCenPer(string idperplan, string idcia,string idcencos)
        {
            
            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();
            string query = "DELETE from cenper WHERE idperplan='" + @idperplan + "' and idcia='" + @idcia + "' and idcencos='"+@idcencos+"';";
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