using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Configuration;
using System.Windows.Forms;

namespace CaniaBrava
{
    class ZonTra
    {
        string idzontra;
        string idcia;
        string deszontra;
        string statezontra;
        
        public ZonTra() { }

        public void setZonTra(string idzontra,string idcia,string deszontra,string statezontra)
        {
            this.idzontra = idzontra;
            this.idcia = idcia;
            this.deszontra = deszontra;
            this.statezontra = statezontra;
        }

        public void actualizarSisParm(string soperacion)
        {
            string squery;
            string idzontra=this.idzontra;
            string idcia = this.idcia;
            string deszontra = this.deszontra;
            string statezontra = this.statezontra;

            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();

            if (soperacion.Equals("AGREGAR"))
            {
                squery = "INSERT INTO zontra (idzontra,deszontra,statezontra,idcia) VALUES ('" + @idzontra + "', '" + @deszontra + "', '" + @statezontra + "','"+@idcia+"');";
            }
            else
            {
                squery = "UPDATE zontra SET deszontra='" + @deszontra + "',statezontra='" + @statezontra + "' WHERE idzontra='" + @idzontra + "' and idcia='"+@idcia+"';";
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

        public void eliminarZonTra(string idzontra, string idcia)
        {
            
            string squery;
            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();

            squery = "DELETE from zontra WHERE idzontra='" + @idzontra + "' and idcia='" + @idcia + "';";

            try
            {
                SqlCommand myCommand = new SqlCommand(squery, conexion);
                myCommand.ExecuteNonQuery();
                myCommand.Dispose();

            }
            catch (SqlException)
            {
                MessageBox.Show("A ocurrido un error en el proceso de Eliminación [Existen registros asociados]", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);

            }

            conexion.Close();

        }
    }
}