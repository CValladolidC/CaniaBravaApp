using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Configuration;

namespace CaniaBrava
{
    class DataPlan
    {
        public DataPlan() { }
        
        public void actualizarDataPlan(string operacion, string idperplan, string idcia, string anio, string messem, string idtipoper, string idtipocal, int diasefelab, int diassubsi, 
            int diasnosubsi, int diastotal, string emplea, string estane, float riesgo, string idtipoplan, float hext25, float hext35, float hext100, string finivac, 
            string ffinvac, int diasvac, float impdes, float candes, int diurno, int nocturno, int diasdom,string regvac,string pervac)
        {
            string query;
            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();
            if (operacion.Equals("AGREGAR"))
            {
                query = "INSERT INTO dataplan (idperplan,idcia,anio,messem,idtipoper,idtipocal,diasefelab,diassubsi,diasnosubsi,diastotal,emplea,estane,riesgo,idtipoplan,hext25,hext35,hext100,finivac,ffinvac,diasvac,candes,impdes,diurno,nocturno,diasdom,regvac,pervac) VALUES ('" + @idperplan + "', '" + @idcia + "', '" + @anio + "', '" + @messem+ "','"+@idtipoper+"','"+@idtipocal+"','"+@diasefelab+"','"+@diassubsi+"','"+@diasnosubsi+"','"+@diastotal+"','"+@emplea+"','"+@estane+"','"+@riesgo+"','"+@idtipoplan+"','"+@hext25+"','"+@hext35+"','"+@hext100+"','"+@finivac+"','"+@ffinvac+"','"+@diasvac+"','"+@candes+"','"+@impdes+"','"+@diurno+"','"+@nocturno+"','"+@diasdom+"','"+@regvac+"','"+@pervac+"');";
            }
            else
            {
                query = "UPDATE dataplan SET automatic='C', diasefelab='" + @diasefelab + "',diasdom='"+@diasdom+"',diassubsi='" + @diassubsi + "',diasnosubsi='"+@diasnosubsi+"',diastotal='"+@diastotal+"',riesgo='"+@riesgo+"',hext25='"+@hext25+"',hext35='"+@hext35+"',hext100='"+@hext100+"',finivac='"+@finivac+"',ffinvac='"+@ffinvac+"',diasvac='"+@diasvac+"',candes='"+@candes+"',impdes='"+@impdes+"',diurno='"+@diurno+"',nocturno='"+@nocturno+"',regvac='"+@regvac+"',pervac='"+@pervac+"' WHERE idcia='" + @idcia + "' and idperplan='"+@idperplan+"' and anio='"+@anio+"' and messem='"+@messem+"' and idtipoper='"+@idtipoper+"' and idtipocal='"+@idtipocal+"' and idtipoplan='"+@idtipoplan+"';";
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

        public void eliminarDataPlan(string idperplan, string idcia, string anio, string messem, string idtipoper, string idtipocal,string idtipoplan)
        {
            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();

            string  query = " DELETE FROM dataplan WHERE idcia='" + @idcia + "' and idperplan='" + @idperplan + "' ";
            query = query + " and anio='" + @anio + "' and messem='" + @messem + "' ";
            query = query + " and idtipoper='" + @idtipoper + "' and idtipocal='" + @idtipocal + "' ";
            query = query + " and idtipoplan='" + @idtipoplan + "'; ";

            query = query + " DELETE from diassubsi WHERE idcia='" + @idcia + "' ";
            query = query + " and idperplan='" + @idperplan + "' and anio='" + @anio + "' ";
            query = query + " and messem='" + @messem + "' and idtipoper='" + @idtipoper + "' ";
            query = query + " and idtipocal='" + @idtipocal + "' and idtipoplan='" + @idtipoplan + "'; ";

            query = query + " DELETE from condataplan WHERE idcia='" + @idcia + "' ";
            query = query + " and idperplan='" + @idperplan + "' and anio='" + @anio + "' ";
            query = query + " and messem='" + @messem + "' and idtipoper='" + @idtipoper + "' ";
            query = query + " and idtipocal='" + @idtipocal + "' and idtipoplan='" + @idtipoplan + "'; ";

            query = query + " DELETE from conbol WHERE idcia='" + @idcia + "' ";
            query = query + " and idperplan='" + @idperplan + "' and anio='" + @anio + "' ";
            query = query + " and messem='" + @messem + "' and idtipoper='" + @idtipoper + "' ";
            query = query + " and idtipocal='" + @idtipocal + "' and idtipoplan='" + @idtipoplan + "'; ";

            query = query + " DELETE from plan_ WHERE idcia='" + @idcia + "' ";
            query = query + " and idperplan='" + @idperplan + "' and anio='" + @anio + "' ";
            query = query + " and messem='" + @messem + "' and idtipoper='" + @idtipoper + "' ";
            query = query + " and idtipocal='" + @idtipocal + "' and idtipoplan='" + @idtipoplan + "'; ";

            query = query + " DELETE from tareo WHERE idcia='" + @idcia + "' and ";
            query = query + " idperplan='" + @idperplan + "' and anio='" + @anio + "' ";
            query = query + " and messem='" + @messem + "' and idtipoper='" + @idtipoper + "' ";
            query = query + " and idtipocal='" + @idtipocal + "' and idtipoplan='" + @idtipoplan + "'; ";

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

        public void eliminarDataPlanPorPeriodo(string idcia, string anio, string messem, string idtipoper, string idtipocal, string idtipoplan)
        {
            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();
            
            string query =  " DELETE from dataplan WHERE idcia='" + @idcia + "' ";
            query = query + " and anio='" + @anio + "' and messem='" + @messem + "' ";
            query = query + " and idtipoper='" + @idtipoper + "' and idtipocal='" + @idtipocal + "' ";
            query = query + " and idtipoplan='" + @idtipoplan + "'; ";

            query = query + " DELETE from diassubsi WHERE idcia='" + @idcia + "' ";
            query = query + " and anio='" + @anio + "' and messem='" + @messem + "' ";
            query = query + " and idtipoper='" + @idtipoper + "' and idtipocal='" + @idtipocal + "' ";
            query = query + " and idtipoplan='" + @idtipoplan + "'; ";

            query = query + " DELETE from condataplan WHERE idcia='" + @idcia + "' ";
            query = query + " and anio='" + @anio + "' and messem='" + @messem + "' ";
            query = query + " and idtipoper='" + @idtipoper + "' and idtipocal='" + @idtipocal + "' ";
            query = query + " and idtipoplan='" + @idtipoplan + "'; ";

            query = query + " DELETE from tareo WHERE idcia='" + @idcia + "' ";
            query = query + " and anio='" + @anio + "' and messem='" + @messem + "' ";
            query = query + " and idtipoper='" + @idtipoper + "' and idtipocal='" + @idtipocal + "' ";
            query = query + " and idtipoplan='" + @idtipoplan + "'; ";

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

        public void actualizarDiasDataPlan(string idperplan,string idcia,string anio, string messem,string idtipoper,string idtipocal,string idtipoplan,int diasefelab,
            int diassubsi,int diasnosubsi,int diastotal,int diurno,int nocturno,int diasdom, float candes,float impdes)
        {
            string query;
            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();
            query = "UPDATE dataplan SET diasefelab='" + @diasefelab + "' ";
            query = query + ",diasdom='" + @diasdom + "',diassubsi='" + @diassubsi + "'";
            query = query + ",diasnosubsi='" + @diasnosubsi + "',diastotal='" + @diastotal + "'";
            query = query + ",diurno='" + @diurno + "',nocturno='" + @nocturno + "' ";
            query = query + ",candes='" + @candes + "',impdes='" + @impdes + "' ";
            query = query + " WHERE idcia='" + @idcia + "' and idperplan='" + @idperplan + "' ";
            query = query + " and anio='" + @anio + "' and messem='" + @messem + "' ";
            query = query + " and idtipoper='" + @idtipoper + "' and idtipocal='" + @idtipocal + "' ";
            query = query + " and idtipoplan='" + @idtipoplan + "';";
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

        public void actualizarDataPlanGratifica(string operacion, string idperplan, string idcia, string anio, string messem, 
            string idtipoper, string idtipocal, string emplea, string estane, float riesgo, string idtipoplan, float gratifica)
        {
            string query;
            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();
            if (operacion.Equals("AGREGAR"))
            {
                query = "INSERT INTO dataplan (idperplan,idcia,anio,messem,idtipoper,idtipocal,";
                query = query + " emplea,estane,riesgo,idtipoplan,gratifica) VALUES ";
                query = query + " ('" + @idperplan + "', '" + @idcia + "', '" + @anio + "', '" + @messem + "' ";
                query = query + " ,'" + @idtipoper + "','" + @idtipocal + "','" + @emplea + "','" + @estane + "', ";
                query=query+ " '" + @riesgo + "','" + @idtipoplan + "','" + @gratifica + "');";
            }
            else
            {
                query = "UPDATE dataplan SET riesgo='" + @riesgo + "',gratifica='" + @gratifica + "' ";
                query = query + " WHERE idcia='" + @idcia + "' and idperplan='" + @idperplan + "' ";
                query = query + " and anio='" + @anio + "' and messem='" + @messem + "' ";
                query = query + " and idtipoper='" + @idtipoper + "' and idtipocal='" + @idtipocal + "' ";
                query = query + " and idtipoplan='" + @idtipoplan + "';";
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

        public void actualizarDataPlanBonificacion(string operacion, string idperplan, string idcia, string anio, string messem, 
            string idtipoper, string idtipocal, string emplea, string estane, float riesgo, string idtipoplan)
        {
            string query;
            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();
            if (operacion.Equals("AGREGAR"))
            {
                query = "INSERT INTO dataplan (idperplan,idcia,anio,messem,idtipoper,idtipocal,";
                query = query + " emplea,estane,riesgo,idtipoplan) VALUES ";
                query = query + " ('" + @idperplan + "', '" + @idcia + "', '" + @anio + "', '" + @messem + "' ";
                query = query + " ,'" + @idtipoper + "','" + @idtipocal + "','" + @emplea + "','" + @estane + "', ";
                query = query + " '" + @riesgo + "','" + @idtipoplan + "');";
            }
            else
            {
                query = "UPDATE dataplan SET riesgo='" + @riesgo + "' ";
                query = query + " WHERE idcia='" + @idcia + "' and idperplan='" + @idperplan + "' ";
                query = query + " and anio='" + @anio + "' and messem='" + @messem + "' ";
                query = query + " and idtipoper='" + @idtipoper + "' and idtipocal='" + @idtipocal + "' ";
                query = query + " and idtipoplan='" + @idtipoplan + "';";
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
    }
}