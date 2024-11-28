using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace CaniaBrava
{
    class FonPen
    {
        string idfonpen;
        string desfonpen;
        float psfonpen;
        float cvfonpen;
        float cffonpen;
        float snpfonpen;
        string statefonpen;
        string codnet;

        public FonPen() { }

        public void setFonPen(string idfonpen,string desfonpen,float psfonpen,float cvfonpen,float cffonpen,string statefonpen,float snpfonpen,string codnet){
            this.idfonpen = idfonpen;
            this.desfonpen = desfonpen;
            this.psfonpen = psfonpen;
            this.cvfonpen = cvfonpen;
            this.cffonpen = cffonpen;
            this.snpfonpen = snpfonpen;
            this.statefonpen = statefonpen;
            this.codnet = codnet;

        }

        public void actualizarFonPen(string soperacion)
        {
            string squery;
            string sidfonpen = this.idfonpen;
            string sdesfonpen = this.desfonpen;
            float fpsfonpen  = this.psfonpen;
            float fcvfonpen = this.cvfonpen;
            float fcffonpen = this.cffonpen;
            float snpfonpen = this.snpfonpen;
            string sstatefonpen = this.statefonpen;
            string scodnet = this.codnet;

            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();

            if (soperacion.Equals("AGREGAR"))
            {
                squery = "INSERT INTO fonpen (idfonpen, desfonpen,psfonpen, cvfonpen, cffonpen,statefonpen,snpfonpen,codnet) VALUES ('" + @sidfonpen + "', '" + @sdesfonpen + "', '" + @fpsfonpen + "', '" + @fcvfonpen + "', '" + @fcffonpen + "', '" + @sstatefonpen + "','"+@snpfonpen+"','"+@codnet+"');";
            }
            else
            {
                squery = "UPDATE fonpen SET desfonpen='" + @sdesfonpen + "',psfonpen='" + @fpsfonpen + "', cvfonpen='" + @fcvfonpen + "', cffonpen='" + @fcffonpen + "',snpfonpen='"+@snpfonpen+"', statefonpen='" + @sstatefonpen + "',codnet='"+@codnet+"' WHERE idfonpen='" + @sidfonpen + "';";
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

        public void eliminarFonPen(string sidfonpen)
        {
            string squery;
            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();

            squery = "DELETE from fonpen WHERE idfonpen='" + @sidfonpen + "';";

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