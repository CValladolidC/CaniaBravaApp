using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Configuration;
using System.Windows.Forms;

namespace CaniaBrava
{
    class DetConPlan
    {
        public DetConPlan() { }

        public void actualizarDetConPlan(string operacion, string idconplan,string idtipoplan,string idtipoper,
        string desboleta,string idcolplan,string pdt,string automatico,string formula,string poseetmin,
        float topemin,string si_topemin,string poseetmax,float topemax,string si_topemax,string proy5tacat,
        string ctadebe, string ctahaber, string state, string destajo, string idcia, string imprime, 
            string idtipocal, string con5tacat,string remprom,string regcero,string presta,string tipanede,
            string anede,string tipanerefde,string anerefde,string tipaneha,string aneha,string tipanerefha,
            string anerefha,string asegurable)
        {
            string query;
            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();

            if (operacion.Equals("AGREGAR"))
            {
                query = "INSERT INTO detconplan (idconplan,idtipoplan,idtipoper,desboleta,";
                query = query + " idcolplan,pdt,automatico,formula,poseetmin,topemin,si_topemin,";
                query = query + " poseetmax,topemax,si_topemax,proy5tacat,ctadebe,ctahaber,state,";
                query = query + " destajo,idcia,imprime,idtipocal,con5tacat,remprom,regcero,";
                query = query + " presta,tipanede,anede,tipanerefde,anerefde,tipaneha,aneha,tipanerefha,";
                query = query + " anerefha,asegura) VALUES ('" + @idconplan + "', '" + @idtipoplan + "',";
                query = query + " '" + @idtipoper + "','" + @desboleta + "','" + @idcolplan + "',";
                query = query + " '" + @pdt + "','" + @automatico + "','" + @formula + "',";
                query = query + " '" + @poseetmin + "','" + @topemin + "','" + @si_topemin + "',";
                query = query + " '" + @poseetmax + "','" + @topemax + "','" + @si_topemax + "',";
                query = query + " '" + @proy5tacat + "','" + @ctadebe + "','" + @ctahaber + "',";
                query = query + " '" + @state + "','" + @destajo + "','" + @idcia + "',";
                query = query + " '" + @imprime + "','" + @idtipocal + "','" + @con5tacat + "',";
                query = query + " '" + @remprom + "','" + @regcero + "','" + @presta + "',";
                query = query + " '" + @tipanede + "','" + @anede + "','" + @tipanerefde + "',";
                query = query + " '" + anerefde + "','" + @tipaneha + "','" + @aneha + "',";
                query = query + " '" + @tipanerefha + "','" + @anerefha + "','"+@asegurable+"');";
            }
            else
            {
                query = "UPDATE detconplan SET desboleta='" + @desboleta + "',idcolplan='" + @idcolplan + "',";
                query = query + " pdt='" + @pdt + "',automatico='" + @automatico + "',formula='" + @formula + "',";
                query = query + " poseetmin='" + @poseetmin + "',topemin='" + @topemin + "',";
                query = query + " si_topemin='" + @si_topemin + "',poseetmax='" + @poseetmax + "',";
                query = query + " topemax='" + @topemax + "',si_topemax='" + @si_topemax + "',";
                query = query + " proy5tacat='" + @proy5tacat + "',ctadebe='" + @ctadebe + "',";
                query = query + " ctahaber='" + @ctahaber + "',state='" + @state + "',";
                query = query + " destajo='" + @destajo + "',imprime='" + @imprime + "',";
                query = query + " con5tacat='" + @con5tacat + "',remprom='" + @remprom + "',";
                query = query + " regcero='" + @regcero + "',presta='" + @presta + "' ,";
                query = query + " tipanede='" + @tipanede + "',anede='" + @anede + "',";
                query = query + " tipanerefde='" + @tipanerefde + "',anerefde='" + @anerefde + "',";
                query = query + " tipaneha='" + @tipaneha + "',aneha='" + @aneha + "',";
                query = query + " tipanerefha='" + @tipanerefha + "',anerefha='"+@anerefha+"',asegura='"+@asegurable+"' ";
                query = query + " WHERE idconplan='" + @idconplan + "' and idtipoplan='" + @idtipoplan + "' ";
                query = query + " and idtipoper='" + @idtipoper + "' and idcia='" + @idcia + "'";
                query = query + " and idtipocal='" + @idtipocal + "';";
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

        public void eliminarDetConPlan(string idconplan, string idtipoplan,string idtipoper,string idcia,string idtipocal)
        {
            string query;
            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();

            query = "DELETE from detconplan WHERE idconplan='" + @idconplan + "' and idtipoplan='" + @idtipoplan + "' and idtipoper='"+@idtipoper+"' and idcia='"+@idcia+"' and idtipocal='"+@idtipocal+"';";

            
            try
            {
                SqlCommand myCommand = new SqlCommand(query, conexion);
                myCommand.ExecuteNonQuery();
                myCommand.Dispose();

            }
            catch (SqlException)
            {
                MessageBox.Show("A ocurrido un error en el proceso de Eliminación", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);

            }

            conexion.Close();

        }

        public string getDetConPlan(string idtipoplan, string idconplan, string idtipoper, string idcia, string idtipocal,string dato)
        {
            string resultado = "";
            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();
            string query = "SELECT * FROM detconplan WHERE idtipoplan='" + @idtipoplan + "' ";
            query = query + " and idtipoper='" + @idtipoper + "' and idconplan='" + @idconplan + "' ";
            query = query + " and idcia='" + @idcia + "' and idtipocal='" + @idtipocal + "';";
            try
            {
                SqlCommand myCommand = new SqlCommand(query, conexion);
                SqlDataReader myReader = myCommand.ExecuteReader();
                if (myReader.Read())
                {
                    if (dato.Equals("AUTOMATICO"))
                    {
                        resultado = myReader["automatico"].ToString();
                    }
                    
                    if (dato.Equals("PRESTA"))
                    {
                        resultado = myReader["presta"].ToString();
                    }
                }

            }
            catch (SqlException e)
            {
                MessageBox.Show(e.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            conexion.Close();
            return resultado;
        }
    }
}
