using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Configuration;
using System.Windows.Forms;
using System.Data;

namespace CaniaBrava
{
    class QuiCat
    {
        public void actualizarQuiCat(string operacion, string idcia, string idperplan, string mes, string anio,
            float impcal, float impext, float remafecta, string tipcal, float impman)
        {
            string query;
            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();
            if (operacion.Equals("AGREGAR"))
            {
                query = "INSERT INTO quicat(idcia,idperplan,mes,anio,impcal,impext,remafecta,tipcal,impman) ";
                query += " VALUES ('" + @idcia + "', '" + @idperplan + "', '" + @mes + "','" + @anio + "'";
                query += ",'" + @impcal + "','" + @impext + "','" + @remafecta + "','" + @tipcal + "','" + @impman + "');";
            }
            else
            {
                query = "UPDATE quicat SET impcal='" + @impcal + "',impext='" + @impext + "',";
                query += " remafecta='" + @remafecta + "',tipcal='" + @tipcal + "',impman='" + @impman + "' ";
                query += " WHERE idcia='" + @idcia + "' and idperplan='" + @idperplan + "' ";
                query += " and mes='" + @mes + "' and anio='" + @anio + "';";
            }
            try
            {
                SqlCommand myCommand = new SqlCommand(query, conexion);
                myCommand.ExecuteNonQuery();
                myCommand.Dispose();
            }
            catch (SqlException ex) { MessageBox.Show(ex.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop); }
            conexion.Close();
        }

        public void eliminarQuiCat(string idcia,string idperplan,string mes, string anio)
        {

            string query;
            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();
            query = "DELETE from quicat WHERE idcia='" + @idcia + "' and idperplan='" + @idperplan + "' and mes='" + @mes + "' and anio='" + @anio + "';";
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

        public string getDatos(string idcia, string idperplan, string mes, string anio,string dato)
        {
            string resultado = string.Empty;

            if (dato.Equals("IMPMAN")) resultado = "0";

            if (dato.Equals("TIPO")) resultado = "A";

            try
            {
                DataTable dt = new DataTable();
                SqlConnection conexion = new SqlConnection();
                conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
                conexion.Open();
                string query =  " Select * from quicat A ";
                query = query + " where A.idcia='" + @idcia + "' and A.idperplan='" + @idperplan + "' ";
                query = query + " and A.mes='" + @mes + "' and A.anio='" + @anio + "' ;";
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = new SqlCommand(query, conexion);
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow row_dt in dt.Rows)
                    {
                        if (dato.Equals("TIPO")) resultado = row_dt["tipcal"].ToString();

                        if (dato.Equals("IMPMAN")) resultado = row_dt["impman"].ToString();
                    }
                }
                conexion.Close();
            }
            catch (SqlException e)
            {
                MessageBox.Show(e.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }

            return resultado;
        }

        public float calcularQuiCat(string idcia, string idperplan, string idtipocal, string idtipoper, string messem, string anio,string idtipoplan)
        {
            float valorquinta = 0;
            try
            {
                string query;
                float impext = 0;
                float remuneracion = 0, nppmobre = 0, nppmemp = 0;
                int factorquinta = 0;
                float remproyectada = 0;

                float remafecta = 0;
                float valorUIT = 0;
                float descuentoUIT = 0;
                float saldoquinta = 0;
                float factor = 0;
                float acuquicat = 0;
                float topeMinimoQuinta = 0;
                string saldaquicat = "N";

                SqlConnection conexion = new SqlConnection();
                conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
                conexion.Open();
                CalPlan calplan = new CalPlan();
                saldaquicat = calplan.getDatosCalPlan(messem, anio, idtipoper, idcia, idtipocal, "SALDAQUICAT");
                string mes = calplan.getDatosCalPlan(messem, anio, idtipoper, idcia, idtipocal, "MES");

                //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                /////////////////////////NUMERO DE PERIODOS POR MES DE EMPLEADOS Y OBREROS////////////////////////////////////////////////////////
                //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                DataTable dt = new DataTable();
                query = "SELECT nppmobre,nppmemp FROM reglabcia WHERE idcia='"+@idcia+"' and idtipoplan='"+@idtipoplan+"' ; ";
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = new SqlCommand(query, conexion);
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow row_dt in dt.Rows)
                    {
                        nppmobre = int.Parse(row_dt["nppmobre"].ToString());
                        nppmemp = int.Parse(row_dt["nppmemp"].ToString());
                    }
                }
               
                string tipocalculo = this.getDatos(idcia, idperplan, mes, anio, "TIPO");
             
                if (tipocalculo.Equals("A"))
                {
                    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                    ///////////////////////////DESCUENTOS EXTERNOS DE QUINTA CATEGORIA HASTA EL MES ANTERIOR /////////////////////////////////////////
                    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                    query = "  SELECT CASE ISNULL(SUM(impext)) WHEN 1 THEN 0 WHEN 0 THEN SUM(impext) END as impext FROM quicat ";
                    query += " where idcia='" + @idcia + "' and idperplan='" + @idperplan + "' and anio='" + @anio + "' and mes<'" + @mes + "' ";
                    SqlCommand myCommandExt = new SqlCommand(query, conexion);
                    SqlDataReader myReaderExt = myCommandExt.ExecuteReader();
                    if (myReaderExt.Read())
                    {
                        impext = float.Parse(myReaderExt["impext"].ToString());
                    }
                    myReaderExt.Close();
                    myCommandExt.Dispose();

                    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                    /////////////////////////ACUMULADO DE DESCUENTOS DE QUINTA CATEGORIA HASTA EL MES ANTERIOR//////////////////////////////////////
                    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

                    query = "  SELECT CASE ISNULL(SUM(A.quicat)) WHEN 1 THEN 0 WHEN 0 THEN SUM(A.quicat) END as acuquicat ";
                    query += " from plan_ A inner join calplan B on A.idcia=B.idcia and A.idtipoper=B.idtipoper and A.idtipocal=B.idtipocal ";
                    query += " and A.anio=B.anio and A.messem=B.messem ";
                    query += " WHERE A.idcia='" + @idcia + "' and A.idperplan='" + @idperplan + "' ";
                    query += " and A.anio='" + @anio + "' and B.mes<'" + @mes + "' ";

                    SqlCommand myCommandAcu = new SqlCommand(query, conexion);
                    SqlDataReader myReaderAcu = myCommandAcu.ExecuteReader();
                    if (myReaderAcu.Read())
                    {
                        acuquicat = float.Parse(myReaderAcu["acuquicat"].ToString());
                    }
                    myReaderAcu.Close();
                    myCommandAcu.Dispose();


                    /********************************************************************************************************************************/
                    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                    ///////////////////////////INICIO DEL CALCULO DE LA REMUNERACION PROYECTADA///////////////////////////////////////////////////////
                    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                    /********************************************************************************************************************************/

                    if (saldaquicat.Equals("N"))
                    {
                        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                        ///////////////CALCULO DE REMUNERACION AFECTA TOMANDO COMO REFERENCIA CONCEPTOS PROYECTABLES//////////////////////////////////////
                        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

                        query = "  SELECT CASE ISNULL(SUM(A.valor)) WHEN 1 THEN 0 WHEN 0 THEN SUM(A.valor) END ";
                        query += " as remafecta FROM conbol A inner join detconplan B on A.idcia=B.idcia and A.idtipocal=B.idtipocal ";
                        query += " and A.idtipoplan=B.idtipoplan and A.idtipoper=B.idtipoper and A.idconplan=B.idconplan ";
                        query += " WHERE B.proy5tacat='S' and A.idcia='" + @idcia + "' and A.idperplan='" + @idperplan + "' and ";
                        query += " A.idtipocal='" + @idtipocal + "' and A.messem='" + @messem + "' and A.anio='" + @anio + "' ";

                        SqlCommand myCommand = new SqlCommand(query, conexion);
                        SqlDataReader myReader = myCommand.ExecuteReader();
                        if (myReader.Read())
                        {
                            remuneracion = float.Parse(myReader["remafecta"].ToString());
                        }
                        myReader.Close();
                        myCommand.Dispose();

                        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                        ///////////////REMUNERACION PROYECTADA PARA CALCULO QUINTA CATEGORIA /////////////////////////////////////////////////////////////
                        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                        if (idtipoplan.Equals("200"))
                        {
                            factorquinta = 12;
                        }
                        else
                        {
                            factorquinta = 14;
                        }
                        if (idtipoper.Equals("E"))
                        {
                            remproyectada = (remuneracion * factorquinta * nppmemp);
                        }
                        else
                        {
                            remproyectada = (remuneracion * factorquinta * nppmobre);
                        }
                    }
                    else
                    {
                        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                        ///////////////SI LA QUINTA SE SALDA SE CALCULA EL TOTAL REMUNERACION PERCIBIDA EN EL AÑO/////////////////////////////////////////
                        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                        query = "  SELECT CASE ISNULL(SUM(A.valor)) WHEN 1 THEN 0 WHEN 0 THEN SUM(A.valor) END as remafecta";
                        query += " FROM conbol A inner join detconplan B on A.idcia=B.idcia and A.idtipocal=B.idtipocal ";
                        query += " and A.idtipoplan=B.idtipoplan and A.idtipoper=B.idtipoper and A.idconplan=B.idconplan ";
                        query += " WHERE B.proy5tacat='S' and A.idcia='" + @idcia + "' and A.idperplan='" + @idperplan + "' ";
                        query += " and A.anio='" + @anio + "'; ";
                        SqlCommand myCommand = new SqlCommand(query, conexion);
                        SqlDataReader myReader = myCommand.ExecuteReader();
                        if (myReader.Read())
                        {
                            remuneracion = float.Parse(myReader["remafecta"].ToString());
                        }
                        myReader.Close();
                        myCommand.Dispose();
                        remproyectada = remuneracion;
                    }

                    /********************************************************************************************************************************/
                    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                    ///////////////////////////FIN DEL CALCULO DE LA REMUNERACION PROYECTADA//////////////// /////////////////////////////////////////
                    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                    /********************************************************************************************************************************/

                    /********************************************************************************************************************************/
                    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                    ///////////////////////////CALCULO DEL MONTO DE QUINTA CATEGORÍA//////////////////////// /////////////////////////////////////////
                    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                    /********************************************************************************************************************************/

                    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                    ///////////////CALCULO DEL MONTO DE QUINTA CATEGORÍA DE ACUERDO A ESCALAS DE /////////////////////////////////////////////////////
                    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

                    DetSisParm detsisparm = new DetSisParm();
                    valorUIT = float.Parse(detsisparm.getSisParm("VAUIT"));

                    float n27uit = 27 * valorUIT;
                    float n54uit = 54 * valorUIT;

                    topeMinimoQuinta = valorUIT * 7;

                    if (remproyectada > topeMinimoQuinta)
                    {
                        descuentoUIT = valorUIT * 7;
                        remafecta = (remproyectada - descuentoUIT);

                        float anual15 = 0;
                        float anual21 = 0;
                        float anual30 = 0;
                        /*27uit*/
                        if (remproyectada <= n27uit)
                        {
                            factor = 15;
                            anual15 = ((remafecta * factor) / 100) - impext;
                            anual21 = 0;
                            anual30 = 0;
                        }
                        else
                        {
                            /*54uit*/
                            if (remproyectada <= n54uit)
                            {
                                factor = 15;
                                anual15 = ((n27uit * factor) / 100);
                                factor = 21;
                                anual21 = (((remafecta - n27uit) * factor) / 100);
                                anual30 = 0;
                            }
                            else
                            /*>54UIT*/
                            {
                                factor = 15;
                                anual15 = ((n27uit * factor) / 100);
                                factor = 21;
                                anual21 = (((n54uit - n27uit) * factor) / 100);
                                factor = 30;
                                anual30 = (((remafecta - n54uit) * factor) / 100);
                            }
                        }

                        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                        /////////////////////////////////////SALDO PENDIENTE QUINTA CATEGORIA///////////////////////////////////////////////////////////
                        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                        saldoquinta = (anual15 + anual21 + anual30) - (acuquicat + impext);

                        if (saldaquicat.Equals("N"))
                        {
                            if (idtipoper.Equals("E"))
                            {
                                valorquinta = (saldoquinta / ((12 - (int.Parse(mes) - 1)) * nppmemp));
                            }
                            else
                            {
                                valorquinta = (saldoquinta / ((12 - (int.Parse(mes) - 1)) * nppmobre));
                            }
                        }
                        else
                        {
                            valorquinta = saldoquinta;
                        }
                    }
                    else
                    {
                        valorquinta = 0;
                    }
                }
                else
                {
                    float impman = float.Parse(this.getDatos(idcia, idperplan, mes, anio, "IMPMAN"));
                    if (idtipoper.Equals("E"))
                    {
                        if (nppmemp > 0)
                        {
                            valorquinta = (impman / nppmemp);
                        }
                        else
                        {
                            valorquinta = 0;
                        }
                    }
                    else
                    {
                        if (nppmobre > 0)
                        {
                            valorquinta = (impman / nppmobre);
                        }
                        else
                        {
                            valorquinta = 0;
                        }
                    }
                }
                conexion.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                valorquinta = 0;
            }
            
            return valorquinta;
        }

        public void actualizaQuiCat(string idcia, string idperplan, string idtipocal, string idtipoper, string messem, string anio)
        {
            try
            {
                string query;
                float remafecta = 0;
                float impcal = 0;
                int numero = 0;

                CalPlan calplan = new CalPlan();
                string mes = calplan.getDatosCalPlan(messem, anio, idtipoper, idcia, idtipocal, "MES");
                SqlConnection conexion = new SqlConnection();
                conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
                conexion.Open();

                //////////////IMPORTE CALCULADO/////////////////////////////////////////////////////////
                query = "  select CASE ISNULL(SUM(A.valor)) WHEN 1 THEN 0 WHEN 0 THEN SUM(A.valor) END  ";
                query += " as impcal from conbol A ";
                query += " inner join detconplan B on A.idcia=B.idcia ";
                query += " and A.idtipocal=B.idtipocal ";
                query += " and A.idtipoplan=B.idtipoplan and A.idtipoper=B.idtipoper ";
                query += " and A.idconplan=B.idconplan ";
                query += " inner join calplan D on A.idtipoper=D.idtipoper and ";
                query += " A.idtipocal=D.idtipocal and ";
                query += " A.idcia=D.idcia and A.anio=D.anio and A.messem=D.messem ";
                query += " where B.con5tacat='S' ";
                query += " and A.idcia='" + @idcia + "' and A.idperplan='" + @idperplan + "' and ";
                query += " D.mes='" + @mes + "' and A.anio='" + @anio + "' ";
                SqlCommand myCommandCal = new SqlCommand(query, conexion);
                SqlDataReader myReaderCal = myCommandCal.ExecuteReader();
                if (myReaderCal.Read())
                {
                    impcal = float.Parse(myReaderCal["impcal"].ToString());
                }
                myReaderCal.Close();
                myCommandCal.Dispose();

                //////////////REMUNERACION AFECTA////////////////////////////////////////////////////////
                query = "  select CASE ISNULL(SUM(A.valor)) WHEN 1 THEN 0 WHEN 0 THEN SUM(A.valor) END ";
                query += " as remafecta from conbol A ";
                query += " inner join detconplan B on A.idcia=B.idcia ";
                query += " and A.idtipocal=B.idtipocal ";
                query += " and A.idtipoplan=B.idtipoplan and A.idtipoper=B.idtipoper ";
                query += " and A.idconplan=B.idconplan ";
                query += " inner join calplan D on A.idtipoper=D.idtipoper and ";
                query += " A.idtipocal=D.idtipocal and ";
                query += " A.idcia=D.idcia and A.anio=D.anio and A.messem=D.messem ";
                query += " where B.proy5tacat='S' ";
                query += " and A.idcia='" + @idcia + "' and A.idperplan='" + @idperplan + "' and ";
                query += " D.mes='" + @mes + "' and A.anio='" + @anio + "' ";
                SqlCommand myCommand = new SqlCommand(query, conexion);
                SqlDataReader myReader = myCommand.ExecuteReader();
                if (myReader.Read())
                {
                    remafecta = float.Parse(myReader["remafecta"].ToString());
                }

                myReader.Close();
                myCommand.Dispose();

                //////////////VERIFICA REGISTRO QUINTA CATEGORIA//////////////////////////////////
                query = "  select CASE ISNULL(COUNT(*)) WHEN 1 THEN 0 WHEN 0 THEN COUNT(*) END ";
                query += " as numero from quicat A ";
                query += " where A.idcia='" + @idcia + "' and A.mes='" + @mes + "' ";
                query += " and A.anio='" + @anio + "' and A.idperplan='" + @idperplan + "' ";
                SqlCommand myCommandQuinta = new SqlCommand(query, conexion);
                SqlDataReader myReaderQuinta = myCommandQuinta.ExecuteReader();
                if (myReaderQuinta.Read())
                {
                    numero = int.Parse(myReaderQuinta["numero"].ToString());
                }
                myReaderQuinta.Close();
                myCommandQuinta.Dispose();
                if (numero > 0)
                {
                    query = "  UPDATE quicat SET impcal='" + @impcal + "',remafecta='" + @remafecta + "' ";
                    query += " where idcia='" + @idcia + "' and idperplan='" + @idperplan + "' ";
                    query += " and anio='" + @anio + "' and mes='" + @mes + "' ";
                }
                else
                {
                    float impext = 0;
                    query = "  INSERT INTO quicat(idcia,idperplan,mes,anio,impcal,impext,remafecta) ";
                    query += " VALUES('" + @idcia + "','" + @idperplan + "','" + @mes + "',";
                    query += " '" + @anio + "','" + @impcal + "','" + @impext + "','" + @remafecta + "')";
                }
                SqlCommand myCommandUpdate = new SqlCommand(query, conexion);
                myCommandUpdate.ExecuteNonQuery();
                myCommandUpdate.Dispose();
                conexion.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }
    }
}