using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace CaniaBrava
{
    public class plantiphorario
    {
        public string idplantiphorario { get; set; }
        public int iddias_semana { get; set; }
        public string hor_entrada { get; set; }
        public string hor_salida { get; set; }
        public int mensaje { get; set; }
        public string dias_en { get; set; }

        public List<plantiphorario> GetPlantiphorario(string idplantiphorario)
        {
            List<plantiphorario> lista = new List<plantiphorario>();
            string query = "select a.*,b.dias_en from plantiphorariodet a (nolock) ";
            query += "inner join dias_semana b (nolock) on b.iddias_semana = a.iddias_semana ";
            query += "where a.idplantiphorario IN (" + idplantiphorario + ")";
            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();

            try
            {
                plantiphorario plan = null;
                SqlCommand myCommand = new SqlCommand(query, conexion);
                SqlDataReader odr = myCommand.ExecuteReader();

                while (odr.Read())
                {
                    plan = new plantiphorario()
                    {
                        idplantiphorario = odr.GetString(odr.GetOrdinal("idplantiphorario")),
                        iddias_semana = odr.GetInt32(odr.GetOrdinal("iddias_semana")),
                        hor_entrada = odr.GetString(odr.GetOrdinal("hor_entrada")),
                        hor_salida = odr.GetString(odr.GetOrdinal("hor_salida")),
                        mensaje = odr.GetInt32(odr.GetOrdinal("mensaje")),
                        dias_en = odr.GetString(odr.GetOrdinal("dias_en"))
                    };
                    lista.Add(plan);
                }

                odr.Close();
                myCommand.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            finally
            {
                conexion.Close();
            }

            return lista;
        }
    }
}