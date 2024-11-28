using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace CaniaBrava
{
    class RegVac
    {
        public RegVac() { }

        public void actualizarRegVac(string soperacion, string idperplan, string idcia, string anio, string finivac, string ffinvac, int diasvac, string idregvac)
        {
            string squery;
            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();

            string nombreDes = string.Empty;

            if (soperacion.Equals("AGREGAR"))
            {
                squery = "INSERT INTO regvac(idperplan,idcia,anio,finivac,ffinvac,diasvac,idregvac) VALUES (";
                squery = squery + "'" + @idperplan + "', '" + @idcia + "', '" + @anio + "',";
                squery = squery + "'" + @finivac + "','" + @ffinvac + "',";
                squery = squery + "'" + @diasvac + "','" + @idregvac + "');";
            }
            else
            {
                squery = "UPDATE regvac SET finivac='" + @finivac + "',ffinvac='" + @ffinvac + "',";
                squery = squery + " diasvac='" + @diasvac + "',anio='" + @anio + "' ";
                squery = squery + " where idcia='" + @idcia + "' and idregvac='" + @idregvac + "';";
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

        public string generaCodigoRegVac(string idcia)
        {
            Funciones funciones = new Funciones();
            string codigoInterno = "0001";
            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();
            string query = " select isnull(max(idregvac),'1') as existencia,max(idregvac)+1 ";
            query = query + " as codigo from regvac ;";
            try
            {
                SqlCommand myCommand = new SqlCommand(query, conexion);
                SqlDataReader myReader = myCommand.ExecuteReader();
                if (myReader.Read())
                {
                    if (myReader.GetString(myReader.GetOrdinal("existencia")).Trim().Equals("1"))
                    {
                        codigoInterno = "0001";
                    }
                    else
                    {
                        string codigo = myReader.GetInt32(myReader.GetOrdinal("codigo")).ToString();
                        codigoInterno = funciones.replicateCadena("0", 4 - codigo.Trim().Length) + codigo;
                    }
                }
                else
                {
                    codigoInterno = "0001";
                }

                myReader.Close();
                myCommand.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            conexion.Close();
            return codigoInterno;

        }

        public void eliminarRegVac(string idcia, string idregvac)
        {
            string query;

            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();

            query = "DELETE FROM regvac WHERE idregvac = '" + @idregvac + "' ;";

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