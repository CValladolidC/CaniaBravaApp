using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Configuration;
using System.Windows.Forms;

namespace CaniaBrava
{
    class Tareo
    {
        public Tareo() { }

        public void actualizarTareo(string soperacion, string idcia, string idperplan, string messem,
        string anio,string idtipocal,string idtipoper,
        string idproddes,string idzontra,float cantidad,float precio,float total,string glosa,string idtipoplan)
        {
            string squery;
            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();

            string nombreDes = string.Empty;
                       
            if (soperacion.Equals("AGREGAR"))
            {
                squery = "INSERT INTO tareo(idperplan,idcia,anio,messem,idtipoper,idtipocal ";
                squery = squery+",idproddes,idzontra,cantidad,precio,total,glosa,idtipoplan) VALUES (";
                squery = squery + "'" + @idperplan + "', '" + @idcia + "', '" + @anio + "','" + @messem + "',";
                squery = squery + "'" + @idtipoper + "','" + @idtipocal + "',";
                squery = squery + "'" + @idproddes + "','" + @idzontra + "','" + @cantidad + "','" + @precio + "','" + @total + "','"+@glosa+"','"+@idtipoplan+"');";
            }
            else
            {
                squery = "UPDATE tareo SET cantidad='" + @cantidad + "',precio='" + @precio + "',";
                squery = squery + "total='" + @total + "' where idcia='" + @idcia + "' and idperplan='" + @idperplan;
                squery = squery + "' and messem='" + @messem + "' and anio='" + @anio + "' and idtipocal='" + @idtipocal + "'";
                squery = squery + " and idtipoper='" + @idtipoper + "' and idproddes='" + @idproddes + "'";
                squery = squery + " and idzontra='" + @idzontra + "' and idtipoplan='"+@idtipoplan+"';";
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

        public void eliminarTareo(string idcia, string idperplan, string messem,
        string anio, string idtipocal, string idtipoper,string idproddes, string idzontra,string idtipoplan)
        {
            string squery;

            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();

            squery = "DELETE from tareo where idcia='" + @idcia + "' and idperplan='" + @idperplan;
            squery = squery + "' and messem='" + @messem + "' and anio='" + @anio + "' and idtipocal='" + @idtipocal + "'";
            squery = squery + " and idtipoper='" + @idtipoper + "' and idproddes='" + @idproddes + "'";
            squery = squery + " and idzontra='" + @idzontra + "' and idtipoplan='"+@idtipoplan+"';";

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