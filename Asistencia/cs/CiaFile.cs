using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Configuration;
using System.Windows.Forms;

namespace CaniaBrava
{
    public class CiaFile
    {
        public CiaFile() { }
        
        public void actualizarCiafile(string operacion, string idcia, string descia, string ruccia, string repcia, string shortcia, string statecia, string typecia, string dircia,
            string regpatcia, string desSi, string desNo, string actividad, string reglab, string codaux,string logo)
        {
            string query;
            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();

            logo = logo.Replace(@"\", "/");

            if (operacion.Equals("AGREGAR"))
            {
                query = "INSERT INTO ciafile (idcia,descia,ruccia,repcia,shortcia,typecia,statecia,";
                query = query + " dircia,regpatcia,desSi,desNo,actividad,reglab,codaux,logo) ";
                query = query + " VALUES ('" + @idcia + "', '" + @descia + "', '" + @ruccia + "',";
                query = query + " '" + @repcia + "', '" + @shortcia + "', '" + @typecia + "', ";
                query = query + " '" + @statecia + "', '" + @dircia + "', '" + @regpatcia + "',";
                query = query + " '" + @desSi + "','" + @desNo + "','" + @actividad + "',";
                query = query + " '" + @reglab + "','" + @codaux + "','"+@logo+"');";
            }
            else
            {
                query = "UPDATE ciafile SET descia='" + @descia + "',ruccia='" + @ruccia + "', ";
                query = query + " repcia='" + @repcia + "', shortcia='" + @shortcia + "', typecia='" + @typecia + "',";
                query = query + " statecia='" + @statecia + "', dircia='" + @dircia + "', regpatcia='" + @regpatcia + "',";
                query = query + " desSi='" + @desSi + "',desNo='" + @desNo + "',actividad='" + @actividad + "',";
                query=query+" reglab='" + @reglab + "',codaux='" + @codaux + "',logo='"+@logo+"' WHERE idcia='" + @idcia + "';";
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

        public void eliminarCiafile(string idcia)
        {
            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();

            string query =  " DELETE from confijos WHERE idcia='" + @idcia + "' ;";
            query = query + " DELETE from dataplan WHERE idcia='" + @idcia + "' ;";
            query = query + " DELETE from derhab WHERE idcia='" + @idcia + "' ;";
            query = query + " DELETE from desjud WHERE idcia='" + @idcia + "' ;";
            query = query + " DELETE from desplan WHERE idcia='" + @idcia + "' ;";
            query = query + " DELETE from diassubsi WHERE idcia='" + @idcia + "' ;";
            query = query + " DELETE from fonpenper WHERE idcia='" + @idcia + "' ;";
            query = query + " DELETE from plan_ WHERE idcia='" + @idcia + "' ;";
            query = query + " DELETE from presper WHERE idcia='" + @idcia + "' ;";
            query = query + " DELETE from remu WHERE idcia='" + @idcia + "' ;";
            query = query + " DELETE from condataplan WHERE idcia='" + @idcia + "' ;";
            query = query + " DELETE from perlab WHERE idcia='" + @idcia + "' ;";
            query = query + " DELETE from cenper WHERE idcia='" + @idcia + "' ;";
            query = query + " DELETE from quicat WHERE idcia='" + @idcia + "' ;";
            query = query + " DELETE from conbol WHERE idcia='" + @idcia + "' ;";
            query = query + " DELETE from perplan WHERE idcia='" + @idcia + "' ;";
            query = query + " DELETE from tareo WHERE idcia='" + @idcia + "' ;";
            query = query + " DELETE from predesplan WHERE idcia='" + @idcia + "' ;";
            
            query = query + " DELETE from calplan WHERE idcia='" + @idcia + "' ;";
            query = query + " DELETE from cencos WHERE idcia='" + @idcia + "' ;";
            query = query + " DELETE from cenper WHERE idcia='" + @idcia + "' ;";
            query = query + " DELETE from ciausrfile WHERE idcia='" + @idcia + "' ;";
            query = query + " DELETE from desret WHERE idcia='" + @idcia + "' ;";
            query = query + " DELETE from detconplan WHERE idcia='" + @idcia + "' ;";
            query = query + " DELETE from detpresper WHERE idcia='" + @idcia + "' ;";
            query = query + " DELETE from detproddes WHERE idcia='" + @idcia + "' ;";
            query = query + " DELETE from estane WHERE idciafile='" + @idcia + "' ;";
            query = query + " DELETE from labper WHERE idcia='" + @idcia + "' ;";
            query = query + " DELETE from perret WHERE idcia='" + @idcia + "' ;";
            query = query + " DELETE from proddes WHERE idcia='" + @idcia + "' ;";
            query = query + " DELETE from reglabcia WHERE idcia='" + @idcia + "' ;";
            query = query + " DELETE from tasaest WHERE idciafile='" + @idcia + "' ;";
            query = query + " DELETE from zontra WHERE idcia='" + @idcia + "' ;";
            query = query + " DELETE from conplan WHERE idcia='" + @idcia + "' ;";
            query = query + " DELETE from calcia WHERE idcia='" + @idcia + "' ;";
            query = query + " DELETE from regvac WHERE idcia='" + @idcia + "' ;";
            query = query + " DELETE from ciafile WHERE idcia='" + @idcia + "' ;";    
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

        public string ui_getDatosCiaFile(string idcia, string datossolicitado)
        {
            string query;
            string resultado = string.Empty;
            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();
            query = "Select * from ciafile where idcia='" + @idcia + "' ;";
            try
            {
                SqlCommand myCommand = new SqlCommand(query, conexion);
                SqlDataReader myReader = myCommand.ExecuteReader();
                if (myReader.Read())
                {
                    if (datossolicitado.Equals("CODAUX"))
                    {
                        resultado = myReader["codaux"].ToString();
                    }

                    if (datossolicitado.Equals("DESCRIPCION"))
                    {
                        resultado = myReader["descia"].ToString();
                    }

                    if (datossolicitado.Equals("RUC"))
                    {
                        resultado = myReader["ruccia"].ToString();
                    }

                    if (datossolicitado.Equals("SECTOR"))
                    {
                        resultado = myReader["reglab"].ToString();
                    }

                }
                myReader.Close();
                myCommand.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            conexion.Close();
            return resultado;
        }
    }
}