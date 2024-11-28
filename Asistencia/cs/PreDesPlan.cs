using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Windows.Forms;

namespace CaniaBrava
{
    class PreDesPlan
    {
        public PreDesPlan() { }

        public void actualizarPreDesPlan(string idperplan, string idcia, string anio,
            string messem, string idtipoper, string idtipocal, string idtipoplan, string subsi,
            string motivosubsi, string citt, string fechainisubsi, string fechafinsubsi,
            string diascitt, string diassubsi, string nosubsi, string motivonosubsi, string fechaininosubsi,
            string fechafinnosubsi, string diasnosubsi, string adicon, string idconplan, string tipocalculo,
            string valor, string diasefelab, string diasdom, string diastotal,string descon,string idconplandes,
            string tipocalculodes,string valordes)
        {
            string squery;

            DataTable dtfila = new DataTable();
            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();

            squery = "Select * from predesplan where idperplan='" + @idperplan + "' and idcia='" + @idcia + "'";
            squery = squery + " and anio='" + @anio + "' and messem='" + @messem + "' and idtipoper='" + @idtipoper + "' ";
            squery = squery + " and idtipocal='" + @idtipocal + "' and idtipoplan='" + @idtipoplan + "' ";

            SqlDataAdapter da_dtfila = new SqlDataAdapter();
            da_dtfila.SelectCommand = new SqlCommand(squery, conexion);
            da_dtfila.Fill(dtfila);

            if (dtfila.Rows.Count > 0)
            {
                
                squery = "UPDATE predesplan set subsi='" + @subsi + "',motivosubsi='" + @motivosubsi + "'";
                squery = squery + ",citt='" + @citt + "',fechainisubsi='" + @fechainisubsi + "'";
                squery = squery + ",fechafinsubsi='" + @fechafinsubsi + "'";
                squery = squery + ",diascitt='" + @diascitt + "',diassubsi='" + @diassubsi + "',nosubsi='" + @nosubsi + "'";
                squery = squery + ",motivonosubsi='" + @motivonosubsi + "',fechaininosubsi='" + @fechaininosubsi + "'";
                squery = squery + ",fechafinnosubsi='" + @fechafinnosubsi + "',diasnosubsi='" + @diasnosubsi + "'";
                squery = squery + ",adicon='" + @adicon + "',idconplan='" + @idconplan + "',tipocalculo='" + @tipocalculo + "'";
                squery = squery + ",valor='" + @valor + "',diasefelab='" + @diasefelab + "',diasdom='" + diasdom + "'";
                squery = squery + ",diastotal='" + @diastotal + "' and descon='" + @descon + "' and idconplandes='" + @idconplandes + "' ";
                squery = squery + " and tipocalculodes='" + @tipocalculodes + "' and valordes='" + @valordes + "' ";
                squery = squery + " where idperplan='" + @idperplan + "' and idcia='" + @idcia + "'";
                squery = squery + " and anio='" + @anio + "' and messem='" + @messem + "' and idtipoper='" + @idtipoper + "' ";
                squery = squery + " and idtipocal='" + @idtipocal + "' and idtipoplan='" + @idtipoplan + "' ";
                
            }
            else
            {

                
                squery = "INSERT INTO predesplan(idperplan,idcia,anio,messem,idtipoper,";
                squery = squery + "idtipocal,idtipoplan,subsi,motivosubsi,citt,fechainisubsi,";
                squery = squery + "fechafinsubsi,diascitt,diassubsi,nosubsi,motivonosubsi,fechaininosubsi,";
                squery = squery + "fechafinnosubsi,diasnosubsi,adicon,idconplan,tipocalculo,";
                squery = squery + "valor,diasefelab,diasdom,diastotal,descon,idconplandes,tipocalculodes,valordes) ";
                squery = squery + "VALUES ('" + @idperplan + "','" + @idcia + "',";
                squery = squery + "'" + @anio + "','" + @messem + "','" + @idtipoper + "',";
                squery = squery + "'" + @idtipocal + "','" + @idtipoplan + "','" + @subsi + "','" + @motivosubsi + "',";
                squery = squery + "'" + @citt + "','" + @fechainisubsi + "',";
                squery = squery + "'" + @fechafinsubsi + "','" + @diascitt + "',";
                squery = squery + "'" + @diassubsi + "','" + @nosubsi + "',";
                squery = squery + "'" + @motivonosubsi + "','" + @fechaininosubsi + "',";
                squery = squery + "'" + @fechafinnosubsi + "',";
                squery = squery + "'" + @diasnosubsi + "','" + @adicon + "','" + @idconplan + "','" + @tipocalculo + "',";
                squery = squery + "'" + @valor + "','" + @diasefelab + "','" + @diasdom + "','" + @diastotal + "','" + @descon + "'";
                squery = squery + ",'"+@idconplandes+"','"+@tipocalculodes+"','"+@valordes+"');";
            }
            try
            {
                SqlCommand myCommand = new SqlCommand(squery, conexion);
                myCommand.ExecuteNonQuery();
                myCommand.Dispose();

            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);

            }
            conexion.Close();

        }

        public void eliminarPreDesPlan(string idperplan, string idcia, string anio, string messem, string idtipoper, string idtipocal, string idtipoplan)
        {
            string squery;

            DataTable dtfila = new DataTable();
            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();

            squery = "SELECT * from desplan where idperplan='" + @idperplan + "' and idcia='" + @idcia + "'";
            squery = squery + " and anio='" + @anio + "' and messem='" + @messem + "' and idtipoper='" + @idtipoper + "' ";
            squery = squery + " and idtipocal='" + @idtipocal + "' and idtipoplan='" + @idtipoplan + "' ";

            SqlDataAdapter da_dtfila = new SqlDataAdapter();
            da_dtfila.SelectCommand = new SqlCommand(squery, conexion);
            da_dtfila.Fill(dtfila);

            if (dtfila.Rows.Count <= 1)
            {
                squery = "DELETE FROM predesplan where idperplan='" + @idperplan + "' and idcia='" + @idcia + "'";
                squery = squery + " and anio='" + @anio + "' and messem='" + @messem + "' and idtipoper='" + @idtipoper + "' ";
                squery = squery + " and idtipocal='" + @idtipocal + "' and idtipoplan='" + @idtipoplan + "' ";
                
                SqlCommand myCommand = new SqlCommand(squery, conexion);
                myCommand.ExecuteNonQuery();
                myCommand.Dispose();
            }
            conexion.Close();
            
        }
    }
}