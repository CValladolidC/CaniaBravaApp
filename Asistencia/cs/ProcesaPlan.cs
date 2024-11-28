using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using System.Windows.Forms;

namespace CaniaBrava
{
    class ProcesaPlan
    {
        public class CurrentState
        {
            public int LinesCounted;
            public int TotalLines;
        }

        private int LinesCounted;
        public string diasperlab;
        public string idcia;
        public string idtipocal;
        public string idtipoper;
        public string messem;
        public string anio;
        public string rucemp;
        public string estane;
        public string idtipoplan;
        public string tipoproceso;

        public void procesa(System.ComponentModel.BackgroundWorker worker, System.ComponentModel.DoWorkEventArgs e)
        {
            
            CurrentState state = new CurrentState();

            string query = "";
            string idperplan = "";
            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();
            try
            {
                ///////////////////////////////////////////////
                ////////////////PARTE DE PLANILLA//////////////
                ///////////////////////////////////////////////
                string condiEstane = string.Empty;
                if (estane != "X")
                {
                    condiEstane = " and A.estane='" + @estane + "' ";
                }

                DataTable dataplan = new DataTable();
                SqlDataAdapter da_dataplan = new SqlDataAdapter();
                query = "select A.idcia,A.idperplan,A.diasefelab,A.diassubsi,A.diasnosubsi,A.diastotal,A.hext25, ";
                query += " A.hext35,A.hext100,A.diasvac,A.candes,A.impdes,A.diurno,A.nocturno,B.mes, ";
                query += " B.fechaini,B.fechafin,A.diasdom,A.gratifica,A.emplea,A.estane from dataplan A  ";
                query += " inner join calplan B on A.idtipocal=B.idtipocal and A.idcia=B.idcia ";
                query += " and A.messem=B.messem and A.anio=B.anio and A.idtipoper=B.idtipoper ";
                query += " where A.idcia='" + @idcia + "' and A.idtipocal='" + @idtipocal + "' ";
                query += " and A.idtipoper='" + @idtipoper + "' and A.messem='" + @messem + "' ";
                query += " and A.anio='" + @anio + "' and A.emplea='" + @rucemp + "' ";
                query += condiEstane+" and A.idtipoplan='" + @idtipoplan + "' ;";
                da_dataplan.SelectCommand = new SqlCommand(query, conexion);
                da_dataplan.Fill(dataplan);

                state.TotalLines = dataplan.Rows.Count;

                ///////////////////////////////////////////////////////////////////////
                ///////////ELIMINA DATOS DE PLANILLA Y CONCEPTOS DE BOLETAS////////////
                ///////////////////////////////////////////////////////////////////////
                Plan plan = new Plan();
                plan.eliminarPlan(idcia, anio, messem, idtipoper, idtipocal, idtipoplan);
                ConBol conbol = new ConBol();
                conbol.eliminarConBolPeriodo(idcia, anio, messem, idtipoper, idtipocal, idtipoplan);
                ProcPlan procplan = new ProcPlan();
                procplan.eliminarProcPlan(idcia, idtipoper, idtipoplan, idtipoplan, anio, messem);

                ////////////////////////////////////////////////
                /// VARIABLES DE PLANILLA CONSTANTES       ///// 
                /// ////////////////////////////////////////////

                DataTable concons = new DataTable();
                SqlDataAdapter da_concons = new SqlDataAdapter();
                //query = " select A.idsisparm,A.constante,CASE B.ispor WHEN '0' ";
                //query = query + " THEN (CASE ISNULL(B.importe) WHEN 1 THEN 0 WHEN 0 THEN B.importe END) ";
                //query = query + " WHEN '1' THEN (CASE ISNULL(B.porcentaje) WHEN 1 THEN 0 WHEN 0 THEN B.porcentaje END) END as valor,C.grupo ";
                //query = query + " from sisparm A inner join detsisparm B on A.idsisparm=B.idsisparm and B.state='V' ";
                //query = query + " inner join constante C on A.constante=C.idconstante where A.tipo='C';";
                query = "call calculo_personal";
                da_concons.SelectCommand = new SqlCommand(query, conexion);
                da_concons.Fill(concons);

                foreach (DataRow row_dataplan in dataplan.Rows)
                { 
                    if (worker.CancellationPending)
                    {
                        MessageBox.Show("Error");
                        e.Cancel = true;
                        break;  
                    }
                    else
                    {
                        LinesCounted += 1;
         
                        idperplan = row_dataplan["idperplan"].ToString();
                        string hext25 = row_dataplan["hext25"].ToString();
                        string hext35 = row_dataplan["hext35"].ToString();
                        string hext100 = row_dataplan["hext100"].ToString();
                        string defla = row_dataplan["diasefelab"].ToString();
                        string dnlns = row_dataplan["diasnosubsi"].ToString();
                        string diassub = row_dataplan["diassubsi"].ToString();
                        string candes = row_dataplan["candes"].ToString();
                        string impdes = row_dataplan["impdes"].ToString();
                        string diasvac = row_dataplan["diasvac"].ToString();
                        string diurno = row_dataplan["diurno"].ToString();
                        string nocturno = row_dataplan["nocturno"].ToString();
                        string mes = row_dataplan["mes"].ToString();
                        string diasdom = row_dataplan["diasdom"].ToString();
                        string gratifica = row_dataplan["gratifica"].ToString();
                        string estanexo = row_dataplan["estane"].ToString();

                        ProcessVariables procesa = new ProcessVariables();
                        procesa.procesaVariablesPlanilla(concons, idcia, idperplan, hext25, hext35,
                        hext100, defla, dnlns, diassub, idtipoplan, idtipoper, anio, messem, idtipocal,
                        rucemp, estanexo, candes, impdes, diasvac, diurno, nocturno, mes, diasperlab, 
                        diasdom,gratifica);

                        state.LinesCounted = LinesCounted;
                        worker.ReportProgress(0, state);
                    }
                }

                if (tipoproceso.Equals("D"))
                {
                    CalPlan calplan = new CalPlan();
                    calplan.cambiaEstadoCalPlan(messem, anio, idtipoper, idcia, idtipocal, "C");
                }
            }
            catch (Exception) { }
            conexion.Close();
        }
    }
}