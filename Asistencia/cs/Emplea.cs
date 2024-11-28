using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Configuration;
using System.Windows.Forms;

namespace CaniaBrava
{
    class Emplea
    {
        public Emplea() { }

        public void actualizarEmplea(string operacion, string idciafile, string rucemp, string razonemp, string actividad,string fini,string ffin)
        {
            string query;

            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();
            if (operacion.Equals("AGREGAR"))
            {
                query = "INSERT INTO emplea (idciafile,rucemp,razonemp,actividad,fini,ffin) ";
                query=query+" VALUES ('" + @idciafile + "', '" + @rucemp + "', '" + @razonemp + "', '" + @actividad + "','" + @fini + "','" + @ffin + "');";
            }
            else
            {
                query = "UPDATE emplea SET razonemp='" + @razonemp + "',actividad='" + @actividad + "',";
                query = query + " fini='" + @fini + "',ffin='" + @ffin + "' WHERE idciafile='" + @idciafile + "' and rucemp='" + @rucemp + "';";
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

        public void eliminarEmplea(string rucemp,string idciafile)
        {
            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();
            string query = "DELETE from emplea WHERE idciafile='" + @idciafile + "' and rucemp='" + @rucemp + "';";
            query = query + " DELETE from estane WHERE codemp='" + @rucemp + "' and idciafile='" + @idciafile + "';";
            query = query + " DELETE from tasaest WHERE codemp='" + @rucemp + "' and idciafile='" + @idciafile + "';";
            try
            {
                SqlCommand myCommand_tasa = new SqlCommand(query, conexion);
                myCommand_tasa.ExecuteNonQuery();
                myCommand_tasa.Dispose();
            }
            catch (SqlException e)
            {
                MessageBox.Show(e.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            conexion.Close();
        }
    }
}