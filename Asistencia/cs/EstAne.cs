using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Configuration;
using System.Windows.Forms;

namespace CaniaBrava
{
    class EstAne
    {
        string idestane;
        string codemp;
        string desestane;
        string tipoestane;
        string idcia;
        string riesgo;

        public EstAne() { }

        public void setEstAne(string idestane,string codemp,string desestane,string tipoestane,string idcia,string riesgo)
        {
            this.idestane = idestane;
            this.desestane = desestane;
            this.codemp = codemp;
            this.tipoestane = tipoestane;
            this.idcia = idcia;
            this.riesgo = riesgo;

        
        }

        public void actualizarEstAne(string soperacion)
        {
            string squery;
            string sidestane = this.idestane;
            string sdesestane = this.desestane;
            string scodemp   = this.codemp;
            string stipoestane = this.tipoestane;
            string sidcia = this.idcia;
            string sriesgo = this.riesgo;


            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();

            if (soperacion.Equals("AGREGAR"))
            {
                squery = "INSERT INTO estane (idestane, desestane,codemp,tipoestane,idciafile,riesgo) VALUES ('" + @sidestane + "', '" + @sdesestane + "', '" + @scodemp + "', '" + @stipoestane+ "','"+@sidcia+"','"+@sriesgo+"');";
            }
            else
            {
                squery = "UPDATE estane SET desestane='" + @sdesestane + "',tipoestane='" + @stipoestane + "',riesgo='"+@sriesgo+"' WHERE idestane='" + @sidestane + "' and codemp='"+@scodemp+"' and idciafile='"+@sidcia+"';";
            }
            try
            {
                SqlCommand myCommand = new SqlCommand(squery, conexion);
                myCommand.ExecuteNonQuery();
                myCommand.Dispose();
                
                if (riesgo.Equals("0"))
                {
                    string squery_tasa = "DELETE from tasaest WHERE idestane='" + @sidestane + "' and codemp='" + @scodemp + "' and idciafile='" + @sidcia + "';";
                    SqlCommand myCommand_tasa = new SqlCommand(squery_tasa, conexion);
                    myCommand_tasa.ExecuteNonQuery();
                    myCommand_tasa.Dispose();

                }
            }
            catch (SqlException)
            {
                MessageBox.Show("No se ha podido realizar el proceso de Actualización", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);

            }

            conexion.Close();

        }

        public void eliminarEstAne(string sidestane,string scodemp,string sidcia)
        {
            string squery;
            string squery_tasa;
            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();

            squery_tasa = "DELETE from tasaest WHERE idestane='" + @sidestane + "' and codemp='" + @scodemp + "' and idciafile='" + @sidcia + "';";
            squery = "DELETE from estane WHERE idestane='" + @sidestane + "' and codemp='"+@scodemp+"' and idciafile='"+@sidcia+"';";

            try
            {
                
                SqlCommand myCommand_tasa = new SqlCommand(squery_tasa, conexion);
                myCommand_tasa.ExecuteNonQuery();
                myCommand_tasa.Dispose();
                
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

        public string ui_getDatosEstane(string sidestane, string scodemp, string sidcia,string datossolicitado)
        {
            string query;
            string resultado = string.Empty;

            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();

            query = "SELECT * from estane WHERE idestane='" + @sidestane + "' and codemp='" + @scodemp + "' and idciafile='" + @sidcia + "';";

            try
            {
                SqlCommand myCommand = new SqlCommand(query, conexion);
                SqlDataReader myReader = myCommand.ExecuteReader();

                if (myReader.Read())
                {
                    if (datossolicitado.Equals("0"))
                    {
                        resultado = myReader["idestane"].ToString();
                    }
                    if (datossolicitado.Equals("1"))
                    {
                        resultado = myReader["desestane"].ToString();
                    }
                    if (datossolicitado.Equals("2"))
                    {
                        if (myReader["riesgo"].Equals("1"))
                        {
                            resultado = "SI";
                        }
                        else
                        {
                            resultado = "NO";
                        }
                    }

                }

                myReader.Close();
                myCommand.Dispose();

            }

            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }

            conexion.Close();
            return resultado;

        }
    }
}