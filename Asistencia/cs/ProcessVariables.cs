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
    class ProcessVariables
    {
        public void procesaVariablesPlanilla(DataTable concons, string idcia, string idperplan, string hext25, string hext35, string hext100, 
            string diasefelab, string diasnolabnosub, string diassub, string idtipoplan, string idtipoper, string anio, string messem, 
            string idtipocal, string emplea, string estane, string candes, string impdes, string diasvac, string diurno, string nocturno, 
            string mes, string diasperlab, string diasdom, string gratifica)
        {

            string bd_prov = ConfigurationManager.AppSettings.Get("BD_PROV");
            string query;
            string formula;
            string idconplan;

            /////////////////////////////////////////////////////////////////////////////////////////////////////
            ///ELIMINAMOS CONCEPTOS DE BOLETA DE LA TABLA CONCEPTOS PARA REALIZAR UNA NUEVA ACTUALIZACION////////
            /////////////////////////////////////////////////////////////////////////////////////////////////////
            ProcPlan procplan = new ProcPlan();
            QuiCat quicat = new QuiCat();
            FonPenPer fonpenper = new FonPenPer();
            DataTable conpers = new DataTable();
            DataTable conplan = new DataTable();
            ConBol conbol = new ConBol();

            conbol.eliminarConBol(idperplan, idcia, anio, messem, idtipoper, idtipocal, idtipoplan);
            procplan.eliminarProcPlanPersona(idcia, idperplan, idtipoper, idtipoplan, idtipocal, anio, messem);

            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();
            SqlDataAdapter da = new SqlDataAdapter();
            foreach (DataRow row_concons in concons.Rows)
            {
                procplan.agregarProcPlan(row_concons["idsisparm"].ToString(), row_concons["constante"].ToString(),
                float.Parse(row_concons["valor"].ToString()), "K", row_concons["grupo"].ToString(),
                idcia, idperplan, idtipoper, idtipoplan, idtipocal, anio, messem, "");
            }

            query = " select A.idsisparm,A.constante,B.grupo from sisparm A inner join constante B ";
            query += "on A.constante=B.idconstante where A.tipo='P';";

            da.SelectCommand = new SqlCommand(query, conexion);
            da.Fill(conpers);

            foreach (DataRow row_conpers in conpers.Rows)
            {
                string valor = "0";
                if (row_conpers["idsisparm"].ToString().Equals("REJOB"))
                {
                    Remu remu = new Remu();
                    valor = remu.consultarRemu(idcia, idperplan, "IMPORTE");
                }
                else
                {
                    if (row_conpers["idsisparm"].ToString().Equals("NHE25"))
                    {
                        valor = hext25;
                    }
                    else
                    {
                        if (row_conpers["idsisparm"].ToString().Equals("NHE35"))
                        {
                            valor = hext35;
                        }
                        else
                        {
                            if (row_conpers["idsisparm"].ToString().Equals("NH100"))
                            {
                                valor = hext100;
                            }
                            else
                            {
                                if (row_conpers["idsisparm"].ToString().Equals("NHIME"))
                                {
                                    DerHab derhab = new DerHab();
                                    valor = derhab.getNumeroDerHab(idcia, idperplan, "1");
                                }
                                else
                                {
                                    if (row_conpers["idsisparm"].ToString().Equals("DEFLA"))
                                    {
                                        valor = diasefelab;
                                    }
                                    else
                                    {
                                        if (row_conpers["idsisparm"].ToString().Equals("DIVAC"))
                                        {
                                            valor = diasvac;
                                        }
                                        else
                                        {
                                            if (row_conpers["idsisparm"].ToString().Equals("DNLNS"))
                                            {
                                                valor = diasnolabnosub;
                                            }
                                            else
                                            {
                                                if (row_conpers["idsisparm"].ToString().Equals("DSUBS"))
                                                {
                                                    valor = diassub;
                                                }
                                                else
                                                {
                                                    if (row_conpers["idsisparm"].ToString().Equals("PSAFP"))
                                                    {
                                                        valor = fonpenper.consultarFonPenPer(idcia, idperplan, "2");
                                                    }
                                                    else
                                                    {
                                                        if (row_conpers["idsisparm"].ToString().Equals("CVAFP"))
                                                        {
                                                            valor = fonpenper.consultarFonPenPer(idcia, idperplan, "3");
                                                        }
                                                        else
                                                        {
                                                            if (row_conpers["idsisparm"].ToString().Equals("CFAFP"))
                                                            {
                                                                valor = fonpenper.consultarFonPenPer(idcia, idperplan, "4");
                                                            }
                                                            else
                                                            {
                                                                if (row_conpers["idsisparm"].ToString().Equals("APVOT"))
                                                                {
                                                                    valor = fonpenper.consultarFonPenPer(idcia, idperplan, "0");
                                                                }
                                                                else
                                                                {
                                                                    if (row_conpers["idsisparm"].ToString().Equals("APVOE"))
                                                                    {
                                                                        valor = fonpenper.consultarFonPenPer(idcia, idperplan, "1");
                                                                    }
                                                                    else
                                                                    {
                                                                        if (row_conpers["idsisparm"].ToString().Equals("CFSNP"))
                                                                        {
                                                                            valor = fonpenper.consultarFonPenPer(idcia, idperplan, "5");
                                                                        }
                                                                        else
                                                                        {
                                                                            if (row_conpers["idsisparm"].ToString().Equals("DIPER"))
                                                                            {
                                                                                valor = diasperlab;
                                                                            }
                                                                            else
                                                                            {
                                                                                if (row_conpers["idsisparm"].ToString().Equals("DDIUR"))
                                                                                {
                                                                                    valor = diurno;
                                                                                }
                                                                                else
                                                                                {
                                                                                    if (row_conpers["idsisparm"].ToString().Equals("DNOCT"))
                                                                                    {
                                                                                        valor = nocturno;
                                                                                    }
                                                                                    else
                                                                                    {
                                                                                        if (row_conpers["idsisparm"].ToString().Equals("NDMES"))
                                                                                        {
                                                                                            valor = Convert.ToString(DateTime.DaysInMonth(Int16.Parse(anio), Int16.Parse(mes)));
                                                                                        }
                                                                                        else
                                                                                        {
                                                                                            if (row_conpers["idsisparm"].ToString().Equals("NDDOM"))
                                                                                            {
                                                                                                valor = diasdom;
                                                                                            }
                                                                                            else
                                                                                            {
                                                                                                if (row_conpers["idsisparm"].ToString().Equals("GRATI"))
                                                                                                {
                                                                                                    valor = gratifica;
                                                                                                }
                                                                                                else
                                                                                                {
                                                                                                    if (row_conpers["idsisparm"].ToString().Equals("ESSAP"))
                                                                                                    {
                                                                                                        PerPlan perplan = new PerPlan();
                                                                                                        QuiCat qcat = new QuiCat();
                                                                                                        DetSisParm detsisparm = new DetSisParm();
                                                                                                        string afiliaeps = perplan.ui_getDatosPerPlan(idcia, idperplan, "AFILIAEPS");
                                                                                                        if (afiliaeps.Equals("1") && idtipocal.Equals("G"))
                                                                                                        {
                                                                                                            valor = detsisparm.getSisParm("ESEPS");
                                                                                                        }
                                                                                                        else
                                                                                                        {
                                                                                                            /*float quicat_valor = qcat.calcularQuiCat(idcia, idperplan, idtipocal, idtipoper, messem, anio, idtipoplan);

                                                                                                            if (quicat_valor > 0) { valor = detsisparm.getSisParm("ESEPS"); }
                                                                                                            else { valor = detsisparm.getSisParm("VESSA"); }*/
                                                                                                            valor = detsisparm.getSisParm("VESSA");
                                                                                                        }
                                                                                                    }
                                                                                                }
                                                                                            }
                                                                                        }
                                                                                    }
                                                                                }
                                                                            }
                                                                        }
                                                                    }
                                                                }
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }

                procplan.agregarProcPlan(row_conpers["idsisparm"].ToString(), row_conpers["constante"].ToString(), float.Parse(valor), "K",
                    row_conpers["grupo"].ToString(), idcia, idperplan, idtipoper, idtipoplan, idtipocal, anio, messem, "");
            }

            ////////////////////////////////////////////////////////////////////////////////////////
            // CONCEPTOS CONFORMANTES DE PLANILLA (NO CONSIDERA CONCEPTOS CONFORMANTES DE DESTAJO)//
            // TODOS LOS CONCEPTOS CONFORMANTE SE CALCULAN MEDIANTE FORMULA/////////////////////////
            ////////////////////////////////////////////////////////////////////////////////////////
            query = " select B.idconplan,A.constante,'F' as tipocalculo, ";

            if (bd_prov.Equals("agromango"))
            {
                query += " replace(replace(replace(B.formula, '+AK', ''),'+AL',''),'+AM','') as formula, ";
            }
            else { query += " B.formula, "; }

            query += " 0 as valor,B.destajo,'' as clasificacion,C.grupo,B.con5tacat,B.regcero ";
            query += " from conplan A inner join detconplan B ";
            query += " on A.idconplan=B.idconplan and A.idtipoplan=B.idtipoplan and A.idcia=B.idcia ";
            query += " and A.idtipocal=B.idtipocal inner join constante C on A.constante=C.idconstante ";
            query += " where B.automatico='S' and A.idcia='" + @idcia + "' and (A.tipo='C' or A.tipo='T') ";
            query += " and B.idtipoplan='" + @idtipoplan + "' and B.idtipoper='" + @idtipoper + "' ";
            query += " and B.destajo='NO' and B.idtipocal='" + @idtipocal + "' and B.con5tacat='N' ";

            query += " union all ";

            query += " select B.idconplan,A.constante,'V' as tipocalculo,B.formula,0 as valor,";
            query += " B.destajo,'' as clasificacion,C.grupo,B.con5tacat,B.regcero ";
            query += " from conplan A inner join detconplan B ";
            query += " on A.idconplan=B.idconplan and A.idtipoplan=B.idtipoplan and A.idcia=B.idcia ";
            query += " and A.idtipocal=B.idtipocal inner join constante C on A.constante=C.idconstante ";
            query += " where A.idcia='" + @idcia + "' and (A.tipo='C' or A.tipo='T') ";
            query += " and B.idtipoplan='" + @idtipoplan + "' and B.idtipoper='" + @idtipoper + "' ";
            query += " and B.destajo='NO' and B.idtipocal='" + @idtipocal + "' and B.con5tacat='S' ";

            query += " union all ";

            ////////////////////////////////////////////////////////////////////////
            /// CONCEPTOS FIJOS DEL TRABAJADOR (NO CONSIDERA CONCEPTOS DE DESTAJO)//
            /// EL CALCULO PUEDE SER POR FORMULA O POR VALOR////////////////////////
            /// ////////////////////////////////////////////////////////////////////
            query += " Select A.idconplan,B.constante,A.tipocalculo,C.formula,A.valor,";
            query += " C.destajo,'' as clasificacion,D.grupo,'N' as con5tacat,C.regcero ";
            query += " from confijos A inner join conplan B ";
            query += " on A.idconplan=B.idconplan and A.idcia=B.idcia and A.idtipocal=B.idtipocal and ";
            query += " A.idtipoplan=B.idtipoplan inner join detconplan C on A.idconplan=C.idconplan ";
            query += " and A.idtipoplan=C.idtipoplan and A.idtipoper=C.idtipoper ";
            query += " and A.idcia=C.idcia and A.idtipocal=C.idtipocal inner join constante D on B.constante=D.idconstante ";
            query += " where A.idcia='" + @idcia + "' and A.idperplan='" + @idperplan + "' ";
            query += " and A.idtipoper='" + @idtipoper + "' and A.idtipoplan='" + @idtipoplan + "' and A.idtipocal='" + @idtipocal + "' ";
            query += " and C.destajo='NO' ";

            query += " union all ";

            ///////////////////////////////////////////////////////////////////////////
            /// CONCEPTOS PLANILLA POR TRABAJADOR (NO CONSIDERA CONCEPTOS DE DESTAJO)//
            /// EL CALCULO PUEDE SER POR FORMULA O POR VALOR///////////////////////////
            /// ///////////////////////////////////////////////////////////////////////
            query += " Select A.idconplan,B.constante,A.tipocalculo,C.formula,A.valor,";
            query += " C.destajo,'' as clasificacion,D.grupo,'N' as con5tacat,C.regcero ";
            query += " from condataplan A inner join conplan B ";
            query += " on A.idconplan=B.idconplan and A.idtipocal=B.idtipocal and ";
            query += " A.idtipoplan=B.idtipoplan and A.idcia=B.idcia inner join detconplan C on A.idconplan=C.idconplan ";
            query += " and A.idtipoplan=C.idtipoplan and A.idtipoper=C.idtipoper ";
            query += " and A.idcia=C.idcia and A.idtipocal=C.idtipocal inner join constante D on B.constante=D.idconstante ";
            query += " where A.idcia='" + @idcia + "' and A.idperplan='" + @idperplan + "' ";
            query += " and A.anio='" + @anio + "' and A.messem='" + @messem + "' and A.idtipoper='" + @idtipoper + "' ";
            query += " and A.idtipocal='" + @idtipocal + "' and A.idtipoplan='" + @idtipoplan + "' and C.destajo='NO' ";


            query += " union all ";

            ////////////////////////////////////////////////////////////////////////
            /// CONCEPTOS DE PLANILLA POR DESTAJO///////////////////////////////////
            /// SE TOMAN VALORES DE CANTIDAD E IMPORTE DEL DESTAJO//////////////////
            /// TODOS LOS CONCEPTOS DE DESTAJO SON DE TIPO CONFORMANTES/////////////

            ///LISTA CONCEPTOS DE PLANILLA RELACIONADOS CON LA CANTIDAD DE LOS PRODUCTO DE DESTAJO
            query += " select A.idconplan,A.constante,'D' as tipocalculo,B.formula,0 as valor,B.destajo,";
            query += " 'C' as clasificacion,C.grupo,'N' as con5tacat,B.regcero ";
            query += " from conplan A inner join detconplan B on A.idtipoplan=B.idtipoplan ";
            query += " and A.idcia=B.idcia and A.idconplan=B.idconplan and A.idtipocal=B.idtipocal ";
            query += " inner join constante C on A.constante=C.idconstante where B.destajo='SI' ";
            query += " and CONCAT(A.idcia,A.idtipoplan,A.idconplan,A.idtipocal) in ";
            query += " (select CONCAT(idcia,idtipoplan,conplancan,idtipocal) ";
            query += " from detproddes where idcia='" + @idcia + "' and idtipoplan='" + @idtipoplan + "' ";
            query += " and idtipocal='" + @idtipocal + "') ";

            query += " union all ";

            ///LISTA CONCEPTOS DE PLANILLA RELACIONADOS CON EL IMPORTE DE LOS PRODUCTO DE DESTAJO
            query += " select A.idconplan,A.constante,'D' as tipocalculo,B.formula,0 as valor,B.destajo,";
            query += " 'I' as clasificacion,C.grupo,'N' as con5tacat,B.regcero ";
            query += " from conplan A inner join detconplan B on A.idtipoplan=B.idtipoplan ";
            query += " and A.idcia=B.idcia and A.idconplan=B.idconplan and A.idtipocal=B.idtipocal ";
            query += " inner join constante C on A.constante=C.idconstante where B.destajo='SI' ";
            query += " and CONCAT(A.idcia,A.idtipoplan,A.idconplan,A.idtipocal) in (select CONCAT(idcia,idtipoplan,conplanimp,idtipocal) ";
            query += " from detproddes where idcia='" + @idcia + "' and idtipoplan='" + @idtipoplan + "' and idtipocal='" + @idtipocal + "') ";
            query += " order by 1 asc ;";

            da.SelectCommand = new SqlCommand(query, conexion);
            da.Fill(conplan);


            /////////////////////////////////////////////////////////////////////////////////////////
            /// DE LOS CONCEPTOS RECOPILADOS PROCEDEMOS A CALCULAR EL RESULTADO DE LA FORMULA////////
            /// O EL VALOR DE DESTAJO YA SEA CANTIDAD O IMPORTE//////////////////////////////////////
            /// O COLOCARLE EL VALOR INGRESADO POR EL USUARIO EN CASO EL INGRESO SEA MANUAL//////////
            /// UNA VEZ CALCULADO EL VALOR SE GRABA EN LA TABLA TEMPORAL PROCPLAN////////////////////
            try
            {
                foreach (DataRow row_conplan in conplan.Rows)
                {
                    string valor;
                    if (row_conplan["tipocalculo"].ToString().Equals("F"))
                    {
                        DataTable conceptos = new DataTable();
                        SqlDataAdapter da_conceptos = new SqlDataAdapter();
                        query = "select idconplan,constante,valor,tipo,grupo from procplan ";
                        query += " where idcia='" + @idcia + "' and idperplan='" + @idperplan + "' and ";
                        query += " idtipoper='" + @idtipoper + "' and idtipoplan='" + @idtipoplan + "' and ";
                        query += " idtipocal='" + @idtipocal + "' and messem='" + @messem + "' ";
                        query += " and anio='" + @anio + "';";

                        da_conceptos.SelectCommand = new SqlCommand(query, conexion);
                        da_conceptos.Fill(conceptos);
                        formula = row_conplan["formula"].ToString();
                        idconplan = row_conplan["idconplan"].ToString();
                        CalculaFormula Formula = new CalculaFormula();
                        valor = Formula.calculaFormula(conceptos, formula, idcia, idtipoplan, idtipoper, idtipocal, anio, messem, idperplan, idconplan);
                    }
                    else
                    {
                        if (row_conplan["destajo"].ToString().Equals("SI"))
                        {
                            Destajo destajo = new Destajo();
                            valor = destajo.ui_getValorConceptoDestajo(idcia, idperplan, messem, anio, idtipocal,
                                idtipoper, idtipoplan, row_conplan["idconplan"].ToString(), row_conplan["clasificacion"].ToString(), emplea, estane);
                        }
                        else
                        {
                            if (row_conplan["con5tacat"].ToString().Equals("S"))
                            {

                                PerPlan perplan = new PerPlan();
                                string exoquicat = perplan.ui_getDatosPerPlan(idcia, idperplan, "EXOQUICAT");
                                if (exoquicat.Equals("0"))
                                {
                                    valor = Convert.ToString(quicat.calcularQuiCat(idcia, idperplan, idtipocal, idtipoper, messem, anio, idtipoplan));
                                }
                                else { valor = "0"; }

                            }
                            else { valor = row_conplan["valor"].ToString(); }
                        }
                    }
                    procplan.agregarProcPlan(row_conplan["idconplan"].ToString(), row_conplan["constante"].ToString(),
                        float.Parse(valor), "C", row_conplan["grupo"].ToString(), idcia, idperplan,
                        idtipoper, idtipoplan, idtipocal, anio, messem, row_conplan["regcero"].ToString());
                }
            }
            catch (SqlException ex) { MessageBox.Show(ex.Message); }

            Plan plan = new Plan();
            plan.actualizarPlan(idperplan, idcia, anio, messem, idtipoper, idtipocal, idtipoplan);
            procplan.eliminarProcPlanPersona(idcia, idperplan, idtipoper, idtipoplan, idtipocal, anio, messem);
            quicat.actualizaQuiCat(idcia, idperplan, idtipocal, idtipoper, messem, anio);
        }
    }
}