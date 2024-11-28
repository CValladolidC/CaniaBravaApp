using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Configuration;
using System.Windows.Forms;
using System.Globalization;

namespace CaniaBrava
{
    class CalPlan
    {
        public CalPlan() { }

        public void actualizaCalPlan(string operacion, string mes, string anio, string messem,
            string idtipoper, string idtipocal, string idcia, string fechaini, string fechafin,
            string mespdt, string aniopdt, int diasdom, string saldaquinta)
        {
            string query;
            Funciones funciones = new Funciones();
            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();
            if (operacion.Equals("AGREGAR"))
            {
                query = "INSERT INTO calplan (mes,anio,messem,idtipoper,idtipocal,idcia,fechaini,";
                query = query + " fechafin,mespdt,aniopdt,diasdom,saldaquinta) VALUES ('" + @mes + "'";
                query = query + " ,'" + @anio + "', '" + @messem + "', '" + @idtipoper + "'";
                query = query + " , '" + @idtipocal + "', '" + @idcia + "', ";
                query = query + " STR_TO_DATE('" + @fechaini + "', '%d/%m/%Y'),";
                query = query + " STR_TO_DATE('" + @fechafin + "', '%d/%m/%Y') ,'" + @mespdt + "'";
                query = query + " ,'" + @aniopdt + "','" + @diasdom + "','" + @saldaquinta + "');";
            }
            else
            {
                query = "UPDATE calplan SET mespdt='" + @mespdt + "',aniopdt='" + @aniopdt + "',mes='" + @mes + "' ";
                query = query + " , fechaini=STR_TO_DATE('" + @fechaini + "', '%d/%m/%Y'),";
                query = query + " fechafin=STR_TO_DATE('" + @fechafin + "', '%d/%m/%Y'),";
                query = query + " diasdom='" + @diasdom + "',saldaquinta='" + @saldaquinta + "'  ";
                query = query + " WHERE idcia='" + @idcia + "' and messem='" + @messem + "' ";
                query = query + " and anio='" + @anio + "' and idtipocal='" + @idtipocal + "';";
            }
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

        public void eliminarCalPlan(string messem, string anio, string idtipoper, string idcia, string idtipocal)
        {
            string query;
            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();
            query = "DELETE from calplan WHERE anio='" + @anio + "' and idcia='" + @idcia + "' ";
            query = query + " and idtipoper='" + @idtipoper + "' and idtipocal='" + @idtipocal + "' ";
            query = query + " and messem='" + @messem + "'; ";
            query = query + " DELETE from conbol WHERE anio='" + @anio + "' and idcia='" + @idcia + "' ";
            query = query + " and idtipoper='" + @idtipoper + "' and idtipocal='" + @idtipocal + "' ";
            query = query + " and messem='" + @messem + "'; ";
            query = query + " DELETE from condataplan WHERE anio='" + @anio + "' and idcia='" + @idcia + "' ";
            query = query + " and idtipoper='" + @idtipoper + "' and idtipocal='" + @idtipocal + "' ";
            query = query + " and messem='" + @messem + "'; ";
            query = query + " DELETE from dataplan WHERE anio='" + @anio + "' and idcia='" + @idcia + "' ";
            query = query + " and idtipoper='" + @idtipoper + "' and idtipocal='" + @idtipocal + "' ";
            query = query + " and messem='" + @messem + "'; ";
            query = query + " DELETE from desplan WHERE anio='" + @anio + "' and idcia='" + @idcia + "' ";
            query = query + " and idtipoper='" + @idtipoper + "' and idtipocal='" + @idtipocal + "' ";
            query = query + " and messem='" + @messem + "'; ";
            query = query + " DELETE from desret WHERE anio='" + @anio + "' and idcia='" + @idcia + "' ";
            query = query + " and idtipoper='" + @idtipoper + "' and idtipocal='" + @idtipocal + "' ";
            query = query + " and messem='" + @messem + "'; ";
            query = query + " DELETE from diassubsi WHERE anio='" + @anio + "' and idcia='" + @idcia + "' ";
            query = query + " and idtipoper='" + @idtipoper + "' and idtipocal='" + @idtipocal + "' ";
            query = query + " and messem='" + @messem + "'; ";
            query = query + " DELETE from plan_ WHERE anio='" + @anio + "' and idcia='" + @idcia + "' ";
            query = query + " and idtipoper='" + @idtipoper + "' and idtipocal='" + @idtipocal + "' ";
            query = query + " and messem='" + @messem + "'; ";
            query = query + " DELETE from predesplan WHERE anio='" + @anio + "' and idcia='" + @idcia + "' ";
            query = query + " and idtipoper='" + @idtipoper + "' and idtipocal='" + @idtipocal + "' ";
            query = query + " and messem='" + @messem + "'; ";
            query = query + " DELETE from procplan WHERE anio='" + @anio + "' and idcia='" + @idcia + "' ";
            query = query + " and idtipoper='" + @idtipoper + "' and idtipocal='" + @idtipocal + "' ";
            query = query + " and messem='" + @messem + "'; ";
            query = query + " DELETE from tareo WHERE anio='" + @anio + "' and idcia='" + @idcia + "' ";
            query = query + " and idtipoper='" + @idtipoper + "' and idtipocal='" + @idtipocal + "' ";
            query = query + " and messem='" + @messem + "'; ";

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

        /// <summary>
        /// Cambia el estado al Calendario de Planilla, puede tomar el valor de V VIGENTE o C CERRADO
        /// </summary>

        public void cambiaEstadoCalPlan(string messem, string anio, string idtipoper, string idcia, string idtipocal, string estado)
        {
            string query;
            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();

            query = "UPDATE calplan SET estado='" + @estado + "' WHERE anio='" + @anio + "' ";
            query = query + " and idcia='" + @idcia + "' and idtipoper='" + @idtipoper + "' ";
            query = query + " and idtipocal='" + @idtipocal + "' and messem='" + @messem + "';";

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

        /// <summary>
        /// Devuelve información referente a Calendario de Planilla
        /// </summary>
        /// <param name="datossolicitado">FECHAINI Fecha de Inicio, FECHAFIN Fecha Final,DIAS_FI_FF Dias entre Fecha Inicio y Fecha Fin, ESTADO Estado del Calendario,DIASDOM dias dominicales,SALDAQUICAT salda quinta categoría?,MES mes del periodo laboral </param>
        /// <returns></returns>
        public string getDatosCalPlan(string messem, string anio, string idtipoper, string idcia, string idtipocal, string datossolicitado)
        {
            string query;
            string resultado = string.Empty;

            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();
            query = " SELECT * from calplan WHERE anio='" + @anio + "' and idcia='" + @idcia + "' ";
            query += "and idtipoper='" + @idtipoper + "' and idtipocal='" + @idtipocal + "' ";
            query += "and messem='" + @messem + "';";

            try
            {
                SqlCommand myCommand = new SqlCommand(query, conexion);
                SqlDataReader myReader = myCommand.ExecuteReader();

                if (myReader.Read())
                {
                    if (datossolicitado.Equals("FECHAINI")) resultado = myReader["fechaini"].ToString();

                    if (datossolicitado.Equals("FECHAFIN")) resultado = myReader["fechafin"].ToString();

                    if (datossolicitado.Equals("DIAS_FI_FF"))
                    {
                        UtileriasFechas uf = new UtileriasFechas();
                        resultado = Convert.ToString(uf.diferenciaEntreFechas(myReader["fechaini"].ToString(), myReader["fechafin"].ToString()));
                    }

                    if (datossolicitado.Equals("ESTADO")) resultado = myReader["estado"].ToString();

                    if (datossolicitado.Equals("DIASDOM")) resultado = myReader["diasdom"].ToString();

                    if (datossolicitado.Equals("SALDAQUICAT")) resultado = myReader["saldaquinta"].ToString();

                    if (datossolicitado.Equals("MES")) resultado = myReader["mes"].ToString();
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

        public string getDatosCalPlanMensual(string mes, string anio, string idtipoper, string idcia, string idtipocal, string datossolicitado)
        {

            string query;
            string resultado = string.Empty;

            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();
            query = "SELECT (SELECT fechaini FROM calplan WHERE anio = '" + @anio + "' and idcia = '" + @idcia + "' ";
            query += "AND idtipoper = '" + @idtipoper + "' and idtipocal = '" + idtipocal + "' and mes = '" + @mes + "' ORDER BY fechaini LIMIT 1) AS fechaini, ";
            query += "(SELECT fechafin FROM calplan WHERE anio = '" + @anio + "' and idcia = '" + @idcia + "' ";
            query += "AND idtipoper = '" + @idtipoper + "' and idtipocal = '" + idtipocal + "' and mes = '" + @mes + "' ORDER BY fechafin DESC LIMIT 1) AS fechafin ";

            try
            {
                SqlCommand myCommand = new SqlCommand(query, conexion);
                SqlDataReader myReader = myCommand.ExecuteReader();

                if (myReader.Read())
                {
                    if (datossolicitado.Equals("FECHAINI")) resultado = myReader["fechaini"].ToString();

                    if (datossolicitado.Equals("FECHAFIN")) resultado = myReader["fechafin"].ToString();
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