using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Configuration;
using System.Windows.Forms;

namespace CaniaBrava
{
    class LabPer
    {
        string idcia;
        string idtipoper;
        string idlabper;
        string deslabper;
        string statelabper;

        public LabPer() { }

        public void setLabPer(string idcia,string idtipoper,string idlabper,string deslabper,string statelabper){
            this.idcia = idcia;
            this.idtipoper = idtipoper;
            this.idlabper = idlabper;
            this.deslabper = deslabper;
            this.statelabper = statelabper;

        }

        public void actualizarLabPer(string soperacion)
        {
            string squery;
            string sidcia = this.idcia;
            string sidtipoper = this.idtipoper;
            string sidlabper = this.idlabper;
            string sdeslabper = this.deslabper;
            string sstatelabper = this.statelabper;

            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();

            if (soperacion.Equals("AGREGAR"))
            {
                squery = "INSERT INTO labper (idlabper, deslabper,statelabper,idcia,idtipoper) VALUES ('" + @sidlabper + "', '" + @sdeslabper + "', '" + @statelabper + "', '" + @sidcia + "', '" + @sidtipoper + "');";
            }
            else
            {
                squery = "UPDATE labper SET deslabper='" + @sdeslabper + "', statelabper='" + @sstatelabper + "' WHERE idlabper='" + @sidlabper + "' and idcia='"+@sidcia+"' and idtipoper='"+@sidtipoper+"';";
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

        public void eliminarLabPer(string sidlabper,string sidcia,string sidtipoper)
        {
            string squery;
            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();

            squery = "DELETE from labper WHERE idlabper='" + @sidlabper + "' and idcia='"+@sidcia+"' and idtipoper='"+@sidtipoper+"';";

            try
            {
                SqlCommand myCommand = new SqlCommand(squery, conexion);
                myCommand.ExecuteNonQuery();
                myCommand.Dispose();

            }
            catch (SqlException)
            {
                MessageBox.Show("A ocurrido un error en el proceso de Eliminación [Existen registros asociados a dicho Fondo de Pensiones]", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);

            }

            conexion.Close();

        }
    }
}