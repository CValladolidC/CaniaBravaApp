using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace CaniaBrava
{
    class TipCam
    {
        public void updTipCam(string operacion, string mon, string dia, string mes, string anio, string tccom, string tcven, string fecha)
        {
            string query;
            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();
            if (operacion.Equals("AGREGAR"))
            {
                query = "INSERT INTO tipcam (mon,dia,mes,anio,tccom,tcven,fecha) ";
                query = query + "VALUES ('" + @mon + "','" + @dia + "', '" + @mes + "', '" + @anio + "', ";
                query = query + "'" + @tccom + "','" + @tcven + "'," + " STR_TO_DATE('" + @fecha + "', '%d/%m/%Y') " + ");";
            }
            else
            {
                query = "UPDATE tipcam SET tccom='" + @tccom + "',tcven='" + @tcven + "' ";
                query = query + " WHERE dia='" + @dia + "' and mes='" + @mes + "' and anio='" + @anio + "' and mon='" + @mon + "';";
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

        public void delTipCam(string mon, string dia, string mes, string anio)
        {
            string query;
            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();
            query = "DELETE from tipcam  WHERE dia='" + @dia + "' and mes='" + @mes + "' and anio='" + @anio + "' and mon='" + @mon + "';";
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

        public string ui_getTipoCambioFechaMax(string fecha, string tipoconver)
        {
            string tipocambio = string.Empty;
            string query = string.Empty;

            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();
            query = " select count(1) existencia,max(fecha) as fechamax from tipcam where fecha <= '" + @fecha + "' ";
            try
            {
                SqlCommand cmd = new SqlCommand(query, conexion);
                SqlDataReader myReader = cmd.ExecuteReader();
                if (myReader.Read())
                {
                    if (myReader.GetInt32(myReader.GetOrdinal("existencia")) > 0)
                    {
                        string fechamax = myReader.GetString(myReader.GetOrdinal("fechamax"));
                        tipocambio = this.ui_getTipoCambioFecha(fechamax, tipoconver);
                    }
                    else
                    {
                        tipocambio = "0";
                    }
                }
                myReader.Close();
                cmd.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            conexion.Close();

            return tipocambio;
        }

        public string ui_getTipoCambioFecha(string fecha, string tipoconver)
        {
            string tipocambio = string.Empty;
            string query = string.Empty;

            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();
            query = " select tccom,tcven from tipcam where fecha = STR_TO_DATE('" + @fecha + "', '%d/%m/%Y') ";
            try
            {
                SqlCommand cmd = new SqlCommand(query, conexion);
                SqlDataReader myReader = cmd.ExecuteReader();
                if (myReader.Read())
                {
                    if (tipoconver.Equals("V"))
                    {
                        tipocambio = myReader.GetString(myReader.GetOrdinal("tcven"));
                    }
                    else
                    {
                        if (tipoconver.Equals("C"))
                        {
                            tipocambio = myReader.GetString(myReader.GetOrdinal("tccom"));
                        }
                        else
                        {
                            tipocambio = "0";
                        }
                    }
                }
                myReader.Close();
                cmd.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            conexion.Close();
            return tipocambio;
        }
    }
}