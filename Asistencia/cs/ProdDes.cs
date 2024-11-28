using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Configuration;
using System.Windows.Forms;

namespace CaniaBrava
{
    class ProdDes
    {
        string idproddes;
        string idcia;
        string desproddes;
        string cortoproddes;
        string unidad;
        string stateproddes;

        public ProdDes() { }

        public void setProdDes(string idproddes,string idcia,string desproddes,string cortoproddes,string unidad,string stateproddes)
        {
            this.idproddes = idproddes;
            this.idcia = idcia;
            this.desproddes = desproddes;
            this.cortoproddes = cortoproddes;
            this.unidad = unidad;
            this.stateproddes = stateproddes;
        }

        public void actualizarProdDes(string soperacion)
        {
            string squery;
            string idproddes = this.idproddes;
            string idcia = this.idcia;
            string desproddes = this.desproddes;
            string cortoproddes = this.cortoproddes;
            string unidad = this.unidad;
            string stateproddes = this.stateproddes;

            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();

            if (soperacion.Equals("AGREGAR"))
            {
                squery = "INSERT INTO proddes (idproddes,idcia,desproddes,cortoproddes,unidad,stateproddes) VALUES ('" + @idproddes + "','"+@idcia+"', '" + @desproddes + "', '" + @cortoproddes + "', '" + @unidad + "','"+@stateproddes+"');";
            }
            else
            {
                squery = "UPDATE proddes SET desproddes='" + @desproddes + "',cortoproddes='" + @cortoproddes + "',unidad='"+@unidad+"',stateproddes='"+@stateproddes+"' WHERE idproddes='" + @idproddes + "' and idcia='"+@idcia+"';";
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

        public void eliminarProdDes(string idproddes,string idcia)
        {
            string squery;
            string squery_detalle;

            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();

            squery_detalle = "DELETE from detproddes WHERE idproddes='" + @idproddes + "' and idcia='" + @idcia + "' ;";
            squery = "DELETE from proddes WHERE idproddes='" + @idproddes + "' and idcia='"+@idcia+"';";

            try
            {
                SqlCommand myCommand_detalle = new SqlCommand(squery_detalle, conexion);
                myCommand_detalle.ExecuteNonQuery();
                myCommand_detalle.Dispose();

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
