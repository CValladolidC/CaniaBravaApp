using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace CaniaBrava
{
    class FonPenPer
    {
        public FonPenPer() { }

        public void actualizarFonPenPer(string soperacion, string idfonpenper, string idcia, string idperplan, 
            string fechaini, string fechafin, string idfonpen, float apvoltra, float apvolemp)
        {
            string squery;
            string statefonpenper;
            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();

            if (soperacion.Equals("AGREGAR"))
            {
                squery = "INSERT INTO fonpenper (idcia,idperplan,fechaini,idfonpen) VALUES ('" + @idcia + "', '" + @idperplan + "'," + "STR_TO_DATE('" + @fechaini + "', '%d/%m/%Y'),'"+@idfonpen+"');";
            }
            else
            {
                if (soperacion.Equals("FINALIZAR"))
                {
                    statefonpenper = "C";
                    squery = "UPDATE fonpenper SET fechaini=" + "STR_TO_DATE('" + @fechaini + "', '%d/%m/%Y')" + ", fechafin=" + "STR_TO_DATE('" + @fechafin + "', '%d/%m/%Y')" + ",idfonpen='" + @idfonpen + "',statefonpenper='" + @statefonpenper + "' WHERE idfonpenper='" + @idfonpenper + "' and idcia='" + @idcia + "' and idperplan='" + @idperplan + "';";
                }
                else
                {
                    squery = "UPDATE fonpenper SET fechaini=" + "STR_TO_DATE('" + @fechaini + "', '%d/%m/%Y'), apvolemp='"+ @apvolemp+ "',apvoltra='"+@apvoltra+"',idfonpen='" + @idfonpen + "' WHERE idfonpenper='" + @idfonpenper + "' and idcia='" + @idcia + "' and idperplan='" + @idperplan + "';";
                }
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

        public void eliminarFonPenPer(string idfonpenper,string idcia,string idperplan)
        {
            string query;
            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();

            query = "DELETE from fonpenper WHERE idfonpenper='" + @idfonpenper + "' and idcia='"+@idcia+"' and idperplan='"+@idperplan+"';";
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

        public string consultarFonPenPer(string idcia, string idperplan,string datossolicitado)
        {
            string query;
            string resultado = "0";
            string fecnac;
            string fecact;
            int anos = 0;
            
            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();
            query = "select A.apvoltra,A.apvolemp,B.psfonpen,B.cvfonpen,B.cffonpen,";
            query = query + "B.snpfonpen from fonpenper A inner join fonpen B on ";
            query = query + "A.idfonpen=B.idfonpen where idcia='" + idcia + "' ";
            query = query + "and idperplan='" + idperplan + "' and statefonpenper='V';";
            
            try
            {
                SqlCommand myCommand = new SqlCommand(query, conexion);
                SqlDataReader myReader = myCommand.ExecuteReader();

                if (myReader.Read())
                {
                    if (datossolicitado.Equals("0"))
                    {
                        resultado = myReader["apvoltra"].ToString();
                    }
                    if (datossolicitado.Equals("1"))
                    {
                        resultado = myReader["apvolemp"].ToString();
                    }
                    if (datossolicitado.Equals("2"))
                    {
                        UtileriasFechas utileriasfechas = new UtileriasFechas();
                        PerPlan perplan=new PerPlan();
                        fecnac=perplan.ui_getDatosPerPlan(idcia,idperplan,"9"); //Fecha de Nacimiento
                        fecact=DateTime.Now.ToString("dd/MM/yyyy");
                        anos = (int)(utileriasfechas.diferenciaEntreFechas(fecnac, fecact) / 365.25);
                        
                        if (anos >= 65)
                        {
                            resultado = "0";
                        }
                        else
                        {
                            resultado = myReader["psfonpen"].ToString();
                        }
                    }
                    if (datossolicitado.Equals("3"))
                    {
                        resultado = myReader["cvfonpen"].ToString();
                    }
                    if (datossolicitado.Equals("4"))
                    {
                        resultado = myReader["cffonpen"].ToString();
                    }
                    if (datossolicitado.Equals("5"))
                    {
                        resultado = myReader["snpfonpen"].ToString();
                    }
                }

                myReader.Close();
                myCommand.Dispose();

            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);

            }

            conexion.Close();
            return resultado;
        }
    }
}