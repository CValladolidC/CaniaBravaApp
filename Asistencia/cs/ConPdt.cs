using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace CaniaBrava
{
    class ConPdt
    {      
        public ConPdt() { }

        public void actualizarConPdt(string operacion, string idconpdt, string desconpdt, string tipconpdt, string devengado, string pagado, string regcero,string devenplame,string pagaplame,string regceroplame)
        {
            string query;
           
            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();

            if (operacion.Equals("AGREGAR"))
            {
                query = "INSERT INTO conpdt (idconpdt,desconpdt,tipconpdt,devengado,pagado,regcero,devenplame,pagaplame,regceroplame) VALUES ('" + @idconpdt + "', '" + @desconpdt + "', '" + @tipconpdt + "', '" + @devengado + "','"+@pagado+"','"+@regcero+"','"+@devenplame+"','"+@pagaplame+"','"+@regceroplame+"');";
            }
            else
            {
                query = "UPDATE conpdt SET desconpdt='" + @desconpdt + "',tipconpdt='" + @tipconpdt + "',devengado='"+@devengado+"',pagado='"+@pagado+"',regcero='"+@regcero+"',devenplame='"+@devenplame+"',pagaplame='"+@pagaplame+"',regceroplame='"+@regceroplame+"' WHERE idconpdt='" + @idconpdt + "';";
            }
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

        public void eliminarConPdt(string idconpdt)
        {
            string query;
            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();

            query = "DELETE from conpdt WHERE idconpdt='" + @idconpdt + "';";

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