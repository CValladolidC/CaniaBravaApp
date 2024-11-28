using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Configuration;
using System.Windows.Forms;

namespace CaniaBrava
{
    class DetSisParm
    {
        public DetSisParm() { }

        public void actualizarDetSisParm(string operacion,string iddetsisparm, string idsisparm, string fini,string ffin,string ispor,float importe,float porcentaje)
        {
            string squery;
            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();

            if (operacion.Equals("AGREGAR"))
            {
                squery = "INSERT INTO detsisparm (idsisparm,fini,ispor,porcentaje,importe) VALUES ('" + @idsisparm + "', " + "STR_TO_DATE('" + @fini + "', '%d/%m/%Y'),'" + @ispor+"','"+@porcentaje+"','"+@importe+"');";
            }
            else
            {
                string state = "C";
                squery = "UPDATE detsisparm SET fini=" + "STR_TO_DATE('" + @fini + "', '%d/%m/%Y'), ffin=" + "STR_TO_DATE('" + @ffin + "', '%d/%m/%Y')" + ",state='" + @state + "' WHERE iddetsisparm='" + @iddetsisparm + "' and idsisparm='" + @idsisparm + "';";
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

        public void eliminarDetSisParm(string idsisparm, string iddetsisparm)
        {
            string query;
            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();

            query = "DELETE from DetSisParm WHERE idsisparm='" + @idsisparm + "' and iddetsisparm='" + @iddetsisparm + "' ;";

            
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

        public string getSisParm(string idsisparm)
        {

            string valor = "0";
            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();
            string  query = " select A.idsisparm,CASE B.ispor WHEN '0' ";
            query = query + " THEN (CASE ISNULL(B.importe) WHEN 1 THEN 0 WHEN 0 THEN B.importe END) ";
            query = query + " WHEN '1' THEN (CASE ISNULL(B.porcentaje) WHEN 1 THEN 0 WHEN 0 THEN B.porcentaje END) END as valor ";
            query = query + " from sisparm A inner join detsisparm B on A.idsisparm=B.idsisparm and B.state='V' ";
            query = query + " where A.idsisparm='" + @idsisparm + "';";

            try
            {
                SqlCommand myCommand = new SqlCommand(query, conexion);
                SqlDataReader myReader = myCommand.ExecuteReader();

                if (myReader.Read())
                {

                    valor = myReader["valor"].ToString();

                }
                else
                {
                    valor = "0";
                }

                myReader.Close();
                myCommand.Dispose();

            }

            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }

            conexion.Close();

            return valor;

        }
    }
}