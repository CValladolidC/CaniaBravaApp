using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Configuration;
using System.Windows.Forms;

namespace CaniaBrava
{
    class PerLab
    {
        public PerLab() { }

        public void actualizarPerLab(string operacion, string idperlab, string idcia, string idperplan, string fechaini, string fechafin, string motivo)
        {
            string query;
            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();

            if (operacion.Equals("AGREGAR"))
            {
                query = " INSERT INTO perlab(idcia,idperplan,fechaini,motivo,stateperlab) ";
                query += "VALUES ('" + @idcia + "', '" + @idperplan + "',";
                query += "'" + @fechaini + "','" + @motivo + "','V');";
            }
            else
            {
                string[] fech_ini = fechaini.Split('/');
                string[] fech_fin = fechafin.Split('/');
                //stateperlab = "C";
                /*query = " UPDATE perlab SET fechaini=STR_TO_DATE('" + @fechaini + "', '%d/%m/%Y'), ";
                query += "fechafin=STR_TO_DATE('" + @fechafin + "', '%d/%m/%Y'),";
                query += "motivo='" + @motivo + "',stateperlab='" + @stateperlab + "' ";
                query += "WHERE idperlab='" + @idperlab + "' and idcia='" + @idcia + "' and idperplan='" + @idperplan + "';";*/
                query = "EXECUTE sp_updperlab '" + idperlab + "', '" + idcia + "', '" + idperplan
                    + "', '" + fechaini + "', '" + fechafin + "', '" + motivo + "', 'C';";
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

        public void eliminarPerLab(string idperlab,string idcia,string idperplan)
        {
            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();
            string query = "DELETE from perlab WHERE idperlab='" + @idperlab + "' and idcia='"+@idcia+"' and idperplan='"+@idperplan+"';";
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