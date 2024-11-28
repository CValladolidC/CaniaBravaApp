using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace CaniaBrava
{
    class DescJud
    {
        public DescJud() { }

        public string generaCodigo(string idcia){

            Funciones funciones = new Funciones();
            string codigoInterno = "0001";
            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();
            string query = "select isnull(max(iddesjud)) as existencia,max(iddesjud)+1 as codigointerno from desjud where (idcia='" + @idcia + "');";

            try
            {
                SqlCommand myCommand = new SqlCommand(query, conexion);
                SqlDataReader myReader = myCommand.ExecuteReader();

                if (myReader.Read())
                {

                    if (myReader["existencia"].Equals("1"))
                    {
                        codigoInterno = "0001";
                    }
                    else
                    {
                        string codigo = myReader["codigointerno"].ToString();
                        codigoInterno = funciones.replicateCadena("0", 4 - codigo.Trim().Length) + codigo;
                    }
                }
                else
                {
                    codigoInterno = "0001";
                }

                myReader.Close();
                myCommand.Dispose();

            }

            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }

            conexion.Close();

            return codigoInterno;

        }

        public void actualizaDesJud(string soperacion,string iddesjud,string idcia,string idperplan,string fecemi,string fecrec,string nrodocaut,string dirigido_a,string fecinidem,
            string nrodocini,string demanda,string motivo,string ispor,string isimp,float porcentaje,float importe, string tipdesc, string tippago,string entfin,string nrocta,
            string moncta,string tipcta,string nomjuez,string feciniorden,string vigencia,string fecfinorden)
        {
            string squery;

            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();

            if (soperacion.Equals("AGREGAR"))
            {
                if (vigencia.Equals("2"))
                {
                    squery = "INSERT INTO desjud (iddesjud,idcia,idperplan,fecemi,nrodocaut,dirigido_a,fecinidem,nrodocini,";
                    squery = squery + "demanda,motivo,ispor,isimp,porcentaje,importe,tipdesc,tippago,entfin,nrocta,moncta,tipcta,";
                    squery = squery + "nomjuez,feciniorden,vigencia,fecfinorden,fecrec)";
                    squery = squery + " VALUES ('" + @iddesjud + "','" + @idcia + "','" + @idperplan + "'," + " STR_TO_DATE('" + @fecemi + "', '%d/%m/%Y') ";
                    squery = squery + ",'" + @nrodocaut + "','" + @dirigido_a + "'," + " STR_TO_DATE('" + @fecinidem + "', '%d/%m/%Y') " + ",'" + @nrodocini + "','" + @demanda + "','" + @motivo + "','" + @ispor;
                    squery = squery + "','" + @isimp + "','" + @porcentaje + "','" + @importe + "','" + @tipdesc + "','" + @tippago + "','" + @entfin + "','" + @nrocta + "','" + @moncta ;
                    squery = squery + "','" + @tipcta + "','" + @nomjuez + "'," + " STR_TO_DATE('" + @feciniorden + "', '%d/%m/%Y') " + ",'" + @vigencia + "'," + " STR_TO_DATE('" + @fecfinorden + "', '%d/%m/%Y'), STR_TO_DATE('" + @fecrec + "', '%d/%m/%Y')) ;";
                }
                else
                {
                    squery = "INSERT INTO desjud (iddesjud,idcia,idperplan,fecemi,nrodocaut,dirigido_a,fecinidem,nrodocini,";
                    squery = squery + "demanda,motivo,ispor,isimp,porcentaje,importe,tipdesc,tippago,entfin,nrocta,moncta,tipcta,";
                    squery = squery + "nomjuez,feciniorden,vigencia,fecrec)";
                    squery = squery + " VALUES ('" + @iddesjud + "','" + @idcia + "','" + @idperplan + "'," + " STR_TO_DATE('" + @fecemi + "', '%d/%m/%Y') ";
                    squery = squery + ",'" + @nrodocaut + "','" + @dirigido_a + "'," + " STR_TO_DATE('" + @fecinidem + "', '%d/%m/%Y') " + ",'" + @nrodocini + "','" + @demanda + "','" + @motivo + "','" + @ispor;
                    squery = squery + "','" + @isimp + "','" +@porcentaje+"','"+@importe+"','" +@tipdesc + "','" + @tippago + "','" + @entfin + "','" + @nrocta + "','" + @moncta ;
                    squery = squery + "','" + @tipcta + "','" + @nomjuez + "'," + " STR_TO_DATE('" + @feciniorden + "', '%d/%m/%Y') " + ",'" + @vigencia + "', STR_TO_DATE('" + @fecrec + "', '%d/%m/%Y')) ;";
                }
            }
            else
            {
                if (vigencia.Equals("2"))
                {
                    squery = "UPDATE desjud SET fecemi=STR_TO_DATE('" + @fecemi + "', '%d/%m/%Y') " + ",nrodocaut='" + @nrodocaut + "',";
                    squery = squery + "dirigido_a='" + @dirigido_a + "',fecinidem=STR_TO_DATE('" + @fecinidem + "', '%d/%m/%Y')" + ",nrodocini='" + @nrodocini + "',demanda='" + @demanda + "',motivo='" + @motivo + "',ispor='" + @ispor + "',";
                    squery = squery + "isimp='" + @isimp + "',porcentaje='" + @porcentaje + "',importe='" + @importe + "',tipdesc='" + @tipdesc + "',tippago='" + @tippago + "',entfin='" + @entfin + "',nrocta='" + @nrocta + "',";
                    squery = squery + "moncta='" + @moncta + "',tipcta='" + @tipcta + "',nomjuez='" + @nomjuez + "',feciniorden=STR_TO_DATE('" + @feciniorden + "', '%d/%m/%Y')" + ",vigencia='" + @vigencia + "',fecfinorden=STR_TO_DATE('" + @fecfinorden + "', '%d/%m/%Y'),fecrec=STR_TO_DATE('" + @fecrec + "', '%d/%m/%Y'),idperplan='"+@idperplan+"' ";
                    squery = squery + " WHERE idcia='" + @idcia + "' and iddesjud='" + @iddesjud + "';";
                }
                else
                {
                    squery = "UPDATE desjud SET fecemi=STR_TO_DATE('" + @fecemi + "', '%d/%m/%Y') " + ",nrodocaut='" + @nrodocaut + "',";
                    squery = squery + "dirigido_a='" + @dirigido_a + "',fecinidem=STR_TO_DATE('" + @fecinidem + "', '%d/%m/%Y')" + ",nrodocini='" + @nrodocini + "',demanda='" + @demanda + "',motivo='" + @motivo + "',ispor='" + @ispor + "',";
                    squery = squery + "isimp='" + @isimp + "',porcentaje='" + @porcentaje + "',importe='" + @importe + "',tipdesc='" + @tipdesc + "',tippago='" + @tippago + "',entfin='" + @entfin + "',nrocta='" + @nrocta + "',";
                    squery = squery + "moncta='" + @moncta + "',tipcta='" + @tipcta + "',nomjuez='" + @nomjuez + "',feciniorden=STR_TO_DATE('" + @feciniorden + "', '%d/%m/%Y')" + ",vigencia='" + @vigencia + "',fecrec=STR_TO_DATE('" + @fecrec + "', '%d/%m/%Y'),idperplan='" + @idperplan + "' ";
                    squery = squery + " WHERE idcia='" + @idcia + "' and iddesjud='" + @iddesjud + "';";
                }
            }


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

        public void eliminarDesJud(string idcia, string iddesjud)
        {
            string squery;

            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();

            squery = "DELETE from desjud WHERE idcia='" + @idcia + "' and iddesjud='" + @iddesjud + "';";

            try
            {
                SqlCommand myCommand = new SqlCommand(squery, conexion);
                myCommand.ExecuteNonQuery();
                myCommand.Dispose();

            }
            catch (SqlException)
            {
                MessageBox.Show("A ocurrido un error en el proceso de Eliminación", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);

            }

            conexion.Close();

        }
    }
}