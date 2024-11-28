using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;

namespace CaniaBrava
{
    class Gratifica
    {
        public void calcularGrati(string idcia,string idtipocal, string idtipoper, string messem_ini,string messem_fin,string anio)
        {

            try
            {
                string query;
                string idperplan;
                float acurem;
                DataTable remuacu = new DataTable();

                query = "         select A.idperplan,SUM(A.valor) as acurem ";
                query = query + " from conbol A inner join detconplan B on A.idcia=B.idcia ";
                query = query + " and A.idtipocal=B.idtipocal ";
                query = query + " and A.idtipoplan=B.idtipoplan and A.idtipoper=B.idtipoper and A.idconplan=B.idconplan ";
                query = query + " inner join reglabcia C on A.idtipoplan=C.idtipoplan and A.idcia=C.idcia ";
                query = query + " inner join calplan D on A.idtipoper=D.idtipoper and A.idtipocal=D.idtipocal and ";
                query = query + " A.idcia=D.idcia and A.anio=D.anio and A.messem=D.messem ";
                query = query + " where B.remprom='S' and A.idcia='" + @idcia + "' and D.mes between ";
                query = query + " '" + @messem_ini + "' and '" + @messem_fin + "' and A.anio='" + @anio + "' ";
                query = query + " group by A.idperplan ;";

                SqlConnection conexion = new SqlConnection();
                conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
                conexion.Open();
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = new SqlCommand(query, conexion);
                da.Fill(remuacu);


                foreach (DataRow row_remuacu in remuacu.Rows)
                {
                    idperplan=row_remuacu["idperplan"].ToString();
                    acurem = float.Parse(row_remuacu["acurem"].ToString());
                }

                conexion.Close();
            }

            catch (Exception)
            {

            }

           
        }
    }
}