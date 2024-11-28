using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Configuration;
using System.Windows.Forms;

namespace CaniaBrava
{
    class ConPlan
    {
        public ConPlan() { }

        public string getNumeroRegistros(string idtipoplan,string idcia,string idtipocal)
        {
            string numero = "0";
            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();
            string squery = "select count(*) as numero from conplan where idtipoplan='" + @idtipoplan + "' and idcia='"+@idcia+"' and idtipocal='"+@idtipocal+"';";
            try
            {
                SqlCommand myCommand = new SqlCommand(squery, conexion);
                SqlDataReader myReader = myCommand.ExecuteReader();

                if (myReader.Read())
                {
                    numero = myReader["numero"].ToString();
                }

            }
            catch (SqlException e)
            {
                MessageBox.Show(e.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            conexion.Close();
            return numero;
        }
        
        public void actualizarConPlan(string soperacion, string idconplan, string idtipoplan, string desconplan, string constante, string idclascol,string tipo,string idcia,string idtipocal)
        {
            string squery;

            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();

            if (soperacion.Equals("AGREGAR"))
            {
                squery = "INSERT INTO conplan (idconplan,idtipoplan,desconplan,constante,idclascol,tipo,idcia,idtipocal) VALUES ('" + @idconplan + "', '" + @idtipoplan + "', '" + @desconplan + "', '" + @constante + "','" + @idclascol + "','"+@tipo+"','"+@idcia+"','"+@idtipocal+"');";
            }
            else
            {
                squery = "UPDATE conplan SET desconplan='" + @desconplan + "',constante='" + @constante + "',idclascol='" + @idclascol + "',tipo='"+@tipo+"' WHERE idconplan='" + @idconplan + "' and idtipoplan='" + @idtipoplan + "' and idcia='"+@idcia+"' and idtipocal='"+@idtipocal+"';";
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

        public void eliminarConplan(string idconplan, string idtipoplan,string idcia,string idtipocal)
        {
            string squery_conplan;
            string squery_detconplan;
            string squery_detproddes;

            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();
            squery_detproddes = "DELETE from detproddes WHERE (conplancan='" + @idconplan + "' or conplanimp='"+@idconplan+"') and idtipoplan='" + @idtipoplan + "' and idcia='" + @idcia + "' and idtipocal='"+@idtipocal+"';";
            squery_detconplan = "DELETE from detconplan WHERE idconplan='" + @idconplan + "' and idtipoplan='" + @idtipoplan + "' and idcia='"+@idcia+"' and idtipocal='"+@idtipocal+"';";
            squery_conplan = "DELETE from conplan WHERE idconplan='" + @idconplan + "' and idtipoplan='" + @idtipoplan + "' and idcia='"+@idcia+"' and idtipocal='"+@idtipocal+"';";
            

            try
            {
                SqlCommand myCommandDetProdDes = new SqlCommand(squery_detproddes, conexion);
                myCommandDetProdDes.ExecuteNonQuery();
                myCommandDetProdDes.Dispose();

                SqlCommand myCommandDetConPlan = new SqlCommand(squery_detconplan, conexion);
                myCommandDetConPlan.ExecuteNonQuery();
                myCommandDetConPlan.Dispose();

                SqlCommand myCommandConPlan = new SqlCommand(squery_conplan, conexion);
                myCommandConPlan.ExecuteNonQuery();
                myCommandConPlan.Dispose();


            }
            catch (SqlException)
            {}
            conexion.Close();

        }

        public void eliminarConplanBloque(string idtipoplan, string idcia, string idtipocal)
        {
            string squery_conplan;
            string squery_detconplan;
            string squery_detproddes;

            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();
            squery_detproddes = "DELETE from detproddes WHERE idtipoplan='" + @idtipoplan + "' and idcia='" + @idcia + "' and idtipocal='" + @idtipocal + "';";
            squery_detconplan = "DELETE from detconplan WHERE idtipoplan='" + @idtipoplan + "' and idcia='" + @idcia + "' and idtipocal='" + @idtipocal + "';";
            squery_conplan = "DELETE from conplan WHERE idtipoplan='" + @idtipoplan + "' and idcia='" + @idcia + "' and idtipocal='" + @idtipocal + "';";
            
            try
            {
                SqlCommand myCommandDetProdDes = new SqlCommand(squery_detproddes, conexion);
                myCommandDetProdDes.ExecuteNonQuery();
                myCommandDetProdDes.Dispose();

                SqlCommand myCommandDetConPlan = new SqlCommand(squery_detconplan, conexion);
                myCommandDetConPlan.ExecuteNonQuery();
                myCommandDetConPlan.Dispose();

                SqlCommand myCommandConPlan = new SqlCommand(squery_conplan, conexion);
                myCommandConPlan.ExecuteNonQuery();
                myCommandConPlan.Dispose();

            }
            catch (SqlException)
            { }
            conexion.Close();

        }

        public void conPlanEstandar(string idtipoplan,string idcia,string idtipocal)
        {
            string query;
            eliminarConplanBloque(idtipoplan, idcia, idtipocal);
            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();
            query = "INSERT INTO conplan (idconplan,idtipoplan,desconplan,constante,idclascol,tipo,idcia,idtipocal) ";
            query = query + "select idconplan,'"+@idtipoplan+"',desconplan,constante,idclascol,tipo,'"+@idcia+"','"+@idtipocal+"' from modplan";
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