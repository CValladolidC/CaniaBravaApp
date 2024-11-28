using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Configuration;
using System.Windows.Forms;

namespace CaniaBrava
{
    class EstAnePro
    {
        public EstAnePro() { }
        
        public void updEstAne(string operacion, string idestane, string codprovee, 
            string desestane, string tipoestane,string direstane,string nomcontac,
            string mailcontac,string localidad,string pais,string telcontac1,
            string telcontac2,string telcontac3,string cargocontac,string comentario,
            string faxcontac)
        {
            string query;
            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();
            if (operacion.Equals("AGREGAR"))
            {
                query = "INSERT INTO estanepro (idestane, desestane,codprovee,tipoestane,";
                query = query + "direstane,nomcontac,mailcontac,localidad,pais,telcontac1,";
                query = query + "telcontac2,telcontac3,cargocontac,comentario,faxcontac) ";
                query = query + " VALUES ('" + @idestane + "', '" + @desestane + "', '" + @codprovee + "' ";
                query = query + ", '" + @tipoestane + "','" + @direstane + "','" + @nomcontac + "'";
                query = query + ", '" + @mailcontac + "','" + @localidad + "','" + @pais + "','" + @telcontac1 + "'";
                query = query + ", '" + @telcontac2 + "','" + @telcontac3 + "','" + @cargocontac + "','" + @comentario + "' ";
                query = query + ", '" + @faxcontac + "');";
            }
            else
            {
                query = " UPDATE estanepro SET desestane='" + @desestane + "',tipoestane='" + @tipoestane + "', ";
                query = query + " direstane='" + @direstane + "',nomcontac='" + @nomcontac + "',mailcontac='" + @mailcontac + "',";
                query = query + " localidad='" + @localidad + "',pais='" + @pais + "',telcontac1='" + @telcontac1 + "',telcontac2='" + @telcontac2 + "',";
                query = query + " telcontac3='" + @telcontac3 + "',cargocontac='" + @cargocontac + "',comentario='" + @comentario + "'";
                query=query+" ,faxcontac='"+@faxcontac+"' WHERE idestane='" + @idestane + "' and codprovee='" + @codprovee + "';";
            }
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

        public void delEstAne(string idestane, string codprovee)
        {
            string query;
            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();

            query = "DELETE from estanepro WHERE idestane='" + @idestane + "' and ";
            query=query+"codprovee='" + @codprovee + "';";

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
               
    }
}
