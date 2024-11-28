using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace CaniaBrava
{
    class TasaEst
    {
        string idestane;
        string codemp;
        string idciafile;
        string tasa;

        public TasaEst() { }

        public void setTasaEst(string idestane, string codemp, string idciafile, string tasa)
        {
            this.idestane = idestane;
            this.codemp = codemp;
            this.idciafile = idciafile;
            this.tasa = tasa;
        
        }

        public void actualizarTasaEst()
        {
            string squery;
            string sidestane = this.idestane;
            string scodemp   = this.codemp;
            string idciafile = this.idciafile;
            string tasa = this.tasa;

            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();
            squery = "INSERT INTO tasaest (idestane,codemp,idciafile,tasa) VALUES ('" + @sidestane +  "', '" + @scodemp + "', '" +@idciafile+"','"+@tasa+"');";
           
            try
            {
                SqlCommand myCommand = new SqlCommand(squery, conexion);
                myCommand.ExecuteNonQuery();
                myCommand.Dispose();

            }
            catch (SqlException)
            {
                MessageBox.Show("No se ha podido realizar el proceso de Actualización", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);

            }

            conexion.Close();

        }

        public void eliminarTasaEst(string sidestane,string scodemp,string sidciafile,string tasa)
        {
            string squery;
            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();

            squery = "DELETE from tasaest WHERE idestane='" + @sidestane + "' and codemp='"+@scodemp+"' and idciafile='"+@sidciafile+"' and tasa='"+@tasa+"';";

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