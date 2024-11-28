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
    class Plan
    {
        public Plan() { }

        public void actualizarPlan(string idperplan, string idcia, string anio,
        string messem, string idtipoper, string idtipocal, string idtipoplan)
        {

            string query;
            string idcolplan;
            string valor;
            string finilab = "";
            string ffinlab = "";
            string finivac = "";
            string ffinvac = "";
            string idfonpen = "";
            string idlabper = "";
            string seccion = "";
            string rucemp = "";
            string estane = "";
            string anio_vaca = "";

            float diasefelab = 0;
            float diassubsi = 0;
            float diasnosubsi = 0;
            float dias = 0;
            float diurno = 0;
            float nocturno = 0;
            float diasvaca = 0;
            float hext25 = 0;
            float hext35 = 0;
            float hext100 = 0;
            float rembas = 0;
            float jorbas = 0;
            float dom = 0;
            float remvac = 0;
            float comides = 0;
            float imphext25 = 0;
            float imphext35 = 0;
            float imphext100 = 0;
            float asigfam = 0;
            float asigesc = 0;
            float asigotros = 0;
            float reintegro = 0;
            float bonifica = 0;
            float cts = 0;
            float gratifica = 0;
            float sppao = 0;
            float sppav = 0;
            float sppcp = 0;
            float sppps = 0;
            float snp = 0;
            float esvida = 0;
            float quicat = 0;
            float adelanto = 0;
            float sindica = 0;
            float desjud = 0;
            float desnodedu = 0;
            float desdedu = 0;
            float sppavemp = 0;
            float essalud = 0;
            float essctr = 0;
            float senati = 0;
            float aporemp = 0;
            float epssctr = 0;
            float sis = 0;
            float total_ingresos = 0;
            float total_descuentos = 0;
            float total_aporta = 0;
            float neto = 0;
            float despres = 0;
            float desban = 0;
            float desseg = 0;
            float total_hxtras = 0;

            SqlConnection conexion = new SqlConnection();
            string bd_prov = ConfigurationManager.AppSettings.Get("BD_PROV");
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();

            query = " select P.seccion,P.rucemp,P.estane,P.idlabper,ISNULL(G.fechaini) as isnullfechaini,";
            query += " G.fechaini,ISNULL(G.fechafin) as isnullfechafin,";
            query += " G.fechafin,(CASE ISNULL(H.idfonpen) ";
            query += " WHEN 1 THEN '' WHEN 0 THEN H.idfonpen END) as idfonpen,";
            query += " B.idcolplan,SUM(A.valor) as valor,Z.finivac,Z.ffinvac,Z.pervac as anio_vaca from conbol A inner join ";
            query += " detconplan B on A.idconplan=B.idconplan ";
            query += " and A.idtipoplan=B.idtipoplan and A.idtipoper=B.idtipoper";
            query += " and A.idcia=B.idcia and A.idtipocal=B.idtipocal ";
            //Agregado por Oliver Cruz Tuanama
            //if (bd_prov.Equals("agromango")) { query += "AND B.idcolplan NOT IN ('010','020','030','100','110','120') "; }
            query += " left join view_perlab F on A.idcia=F.idcia and ";
            query += " A.idperplan=F.idperplan left join perlab G ";
            query += " on F.idcia=G.idcia and F.idperplan=G.idperplan ";
            query += " and F.idperlab=G.idperlab ";
            query += " left join fonpenper H on A.idcia=H.idcia ";
            query += " and A.idperplan=H.idperplan and H.statefonpenper='V' ";
            query += " left join perplan P on A.idcia=P.idcia ";
            query += " and A.idperplan=P.idperplan ";
            query += " left join dataplan Z on A.idcia=Z.idcia and A.idperplan=Z.idperplan ";
            query += " and A.idtipoper=Z.idtipoper and A.idtipoplan=Z.idtipoplan and ";
            query += " A.idtipocal=Z.idtipocal and A.messem=Z.messem and A.anio=Z.anio ";
            query += " where A.idperplan='" + @idperplan + "' and ";
            query += " A.idcia='" + @idcia + "' and A.anio='" + @anio + "' and A.messem='" + @messem + "' ";
            query += " and A.idtipoper='" + @idtipoper + "' and ";
            query += " A.idtipocal='" + @idtipocal + "' and A.idtipoplan='" + @idtipoplan + "' ";
            query += " and B.idcolplan<>''";
            query += " group by P.seccion,P.rucemp,P.estane,P.idlabper,G.fechaini,";
            query += " G.fechafin,H.idfonpen,B.idcolplan,Z.finivac,Z.ffinvac,Z.pervac";
            query += " having SUM(A.valor)>0 ";
            query += " order by B.idcolplan asc;";

            DataTable dtplan = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = new SqlCommand(query, conexion);
            da.Fill(dtplan);
            foreach (DataRow row_dtplan in dtplan.Rows)
            {
                idfonpen = row_dtplan["idfonpen"].ToString();
                idlabper = row_dtplan["idlabper"].ToString();
                idcolplan = row_dtplan["idcolplan"].ToString();
                seccion = row_dtplan["seccion"].ToString();
                rucemp = row_dtplan["rucemp"].ToString();
                estane = row_dtplan["estane"].ToString();
                valor = row_dtplan["valor"].ToString();

                if (row_dtplan["isnullfechaini"].ToString().Equals("0"))
                {
                    finilab = row_dtplan["fechaini"].ToString().Substring(0, 10);
                }
                else
                {
                    finilab = "";
                }

                if (row_dtplan["isnullfechafin"].ToString().Equals("0"))
                {
                    ffinlab = row_dtplan["fechafin"].ToString().Substring(0, 10);

                }
                else
                {
                    ffinlab = "";
                }

                finivac = row_dtplan["finivac"].ToString();
                ffinvac = row_dtplan["ffinvac"].ToString();
                anio_vaca = row_dtplan["anio_vaca"].ToString();


                //////////NO ES CONSTRUCCION CIVIL////////////
                if (idtipoplan != "400")
                {
                    /////////////////////////////////////////
                    ///////DIAS EFECTIVAMENTE LABORADOS//////
                    /////////////////001/////////////////////

                    if (idcolplan.Equals("001"))
                    {
                        diasefelab = float.Parse(valor);
                    }
                    /////////////////////////////////////////
                    ///////DIAS SUBSIDIADOS//////////////////
                    /////////////////002/////////////////////

                    if (idcolplan.Equals("002"))
                    {
                        diassubsi = float.Parse(valor);
                    }

                    /////////////////////////////////////////
                    /////DIAS NO SUBSIDIADOS-NO LABORADOS////
                    /////////////////003/////////////////////

                    if (idcolplan.Equals("003"))
                    {
                        diasnosubsi = float.Parse(valor);
                    }


                    /////////////////////////////////////////
                    ////////////DIAS VACACIONES /////////////
                    /////////////////004/////////////////////

                    if (idcolplan.Equals("004"))
                    {
                        diasvaca = float.Parse(valor);
                    }

                    /////////////////////////////////////////
                    ////////////DIAS DIURNO /////////////
                    /////////////////005/////////////////////

                    if (idcolplan.Equals("005"))
                    {
                        diurno = float.Parse(valor);
                    }


                    /////////////////////////////////////////
                    ////////////DIAS NOCTURNO /////////////
                    /////////////////006/////////////////////

                    if (idcolplan.Equals("006"))
                    {
                        nocturno = float.Parse(valor);
                    }

                    /////////////////////////////////////////
                    ////////NRO. HORAS EXTRAS 25%////////////
                    /////////////////010/////////////////////

                    if (idcolplan.Equals("010"))
                    {
                        hext25 = float.Parse(valor);
                    }

                    /////////////////////////////////////////
                    ////////NRO. HORAS EXTRAS 35%////////////
                    /////////////////020/////////////////////

                    if (idcolplan.Equals("020"))
                    {
                        hext35 = float.Parse(valor);
                    }

                    /////////////////////////////////////////
                    ////////NRO. HORAS EXTRAS 100%///////////
                    /////////////////030/////////////////////

                    if (idcolplan.Equals("030"))
                    {
                        hext100 = float.Parse(valor);
                    }

                    /////////////////////////////////////////
                    //////////REMUNERACION BASICA////////////
                    /////////////////040/////////////////////

                    if (idcolplan.Equals("040"))
                    {
                        rembas = float.Parse(valor);
                    }

                    ////////////////////////////////////
                    ///////////JORNAL BASICO////////////
                    /////////////////050////////////////

                    if (idcolplan.Equals("050"))
                    {
                        jorbas = float.Parse(valor);
                    }

                    ////////////////////////////////////
                    //////////////DOMINICAL/////////////
                    /////////////////060////////////////

                    if (idcolplan.Equals("060"))
                    {
                        dom = float.Parse(valor);
                    }

                    ////////////////////////////////////
                    /////REMUNERACION VACACIONAL////////
                    /////////////////070////////////////

                    if (idcolplan.Equals("070"))
                    {
                        remvac = float.Parse(valor);
                    }

                    ////////////////////////////////////
                    ////////COMISIONES O DESTAJO////////
                    /////////////////090////////////////

                    if (idcolplan.Equals("090"))
                    {
                        comides = float.Parse(valor);
                    }

                    ////////////////////////////////////
                    ///////IMPORTES H.EXTRAS 25%////////
                    ///////////////100//////////////////

                    if (idcolplan.Equals("100"))
                    {
                        imphext25 = float.Parse(valor);
                    }

                    ////////////////////////////////////
                    ///////IMPORTES H.EXTRAS 35%////////
                    /////////////////110////////////////

                    if (idcolplan.Equals("110"))
                    {
                        imphext35 = float.Parse(valor);
                    }

                    ////////////////////////////////////
                    //////IMPORTES H.EXTRAS 100%////////
                    /////////////////120////////////////

                    if (idcolplan.Equals("120"))
                    {
                        imphext100 = float.Parse(valor);
                    }

                    ////////////////////////////////////
                    /////////ASIGNACION FAMILIAR////////
                    /////////////////130////////////////

                    if (idcolplan.Equals("130"))
                    {
                        asigfam = float.Parse(valor);
                    }

                    ////////////////////////////////////
                    /////ASIGNACION POR ESCOLARIDAD/////
                    /////////////////140////////////////

                    if (idcolplan.Equals("140"))
                    {
                        asigesc = float.Parse(valor);
                    }

                    ////////////////////////////////////
                    /////ASIGNACION POR OTROS MOTIVOS///
                    /////////////////160////////////////

                    if (idcolplan.Equals("160"))
                    {
                        asigotros = float.Parse(valor);
                    }

                    ////////////////////////////////////
                    /////////////REINTEGROS/////////////
                    /////////////////170////////////////

                    if (idcolplan.Equals("170"))
                    {
                        reintegro = float.Parse(valor);
                    }

                    ////////////////////////////////////
                    ////////////BONIFICACIONES//////////
                    /////////////////180////////////////

                    if (idcolplan.Equals("180"))
                    {
                        bonifica = float.Parse(valor);
                    }

                    ////////////////////////////////////
                    /////////////////CTS////////////////
                    /////////////////190////////////////

                    if (idcolplan.Equals("190"))
                    {
                        cts = float.Parse(valor);
                    }

                    ////////////////////////////////////
                    ////////////GRATIFICACION///////////
                    /////////////////200////////////////

                    if (idcolplan.Equals("200"))
                    {
                        gratifica = float.Parse(valor);
                    }

                    ///////////////////////////////////////////////////////////
                    ///SISTEMA PRIVADO DE PENSIONES - APORTACION OBLIGATORIA///
                    /////////////////310///////////////////////////////////////

                    if (idcolplan.Equals("310"))
                    {
                        sppao = float.Parse(valor);
                    }

                    ///////////////////////////////////////////////////////////
                    ///SISTEMA PRIVADO DE PENSIONES - APORTACION VOLUNTARIA////
                    /////////////////315///////////////////////////////////////

                    if (idcolplan.Equals("315"))
                    {
                        sppav = float.Parse(valor);
                    }

                    ///////////////////////////////////////////////////////////
                    /////////////////COMISION AFP PORCENTUAL///////////////////
                    //////////////////////////320//////////////////////////////

                    if (idcolplan.Equals("320"))
                    {
                        sppcp = float.Parse(valor);
                    }

                    ///////////////////////////////////////////////////////////
                    /////////////////////PRIMA DE SEGURO AFP///////////////////
                    //////////////////////////330//////////////////////////////

                    if (idcolplan.Equals("330"))
                    {
                        sppps = float.Parse(valor);
                    }

                    ///////////////////////////////////////////////////////////
                    ///////////SISTEMA NACIONAL DE PENSIONES///////////////////
                    //////////////////////////340//////////////////////////////

                    if (idcolplan.Equals("340"))
                    {
                        snp = float.Parse(valor);
                    }

                    ///////////////////////////////////////////////////////////
                    ///////////////////////////ESSALUD+VIDA///////////////////
                    //////////////////////////350//////////////////////////////

                    if (idcolplan.Equals("350"))
                    {
                        esvida = float.Parse(valor);
                    }

                    ///////////////////////////////////////////////////////////
                    /////////RENTA QUINTA CATEGORIA RETENCIONES////////////////
                    /////////////////////////360///////////////////////////////

                    if (idcolplan.Equals("360"))
                    {
                        quicat = float.Parse(valor);
                    }

                    ///////////////////////////////////////////////////////////
                    ////////////////////////ADELANTO///////////////////////////
                    /////////////////////////370///////////////////////////////

                    if (idcolplan.Equals("370"))
                    {
                        adelanto = float.Parse(valor);
                    }

                    ///////////////////////////////////////////////////////////
                    /////////////////////CUOTA SINDICAL///////////////////////
                    /////////////////////////380///////////////////////////////

                    if (idcolplan.Equals("380"))
                    {
                        sindica = float.Parse(valor);
                    }

                    ///////////////////////////////////////////////////////////
                    ////DESCUENTO AUTORIZADO U ORDENADO POR MANDATO JUDICIAL///
                    /////////////////////////390///////////////////////////////

                    if (idcolplan.Equals("390"))
                    {
                        desjud = float.Parse(valor);
                    }

                    ///////////////////////////////////////////////////////////
                    ////DESCUENTO POR PRESTAMO INTERNO/////////////////////////
                    /////////////////////////395///////////////////////////////

                    if (idcolplan.Equals("395"))
                    {
                        despres = float.Parse(valor);
                    }

                    ///////////////////////////////////////////////////////////
                    ////DESCUENTO POR CONVENIO BANCARIO////////////////////////
                    /////////////////////////395///////////////////////////////

                    if (idcolplan.Equals("396"))
                    {
                        desban = float.Parse(valor);
                    }

                    ///////////////////////////////////////////////////////////
                    ////DESCUENTO POR SEGURO PRIVADO///////////////////////////
                    /////////////////////////397///////////////////////////////

                    if (idcolplan.Equals("397"))
                    {
                        desseg = float.Parse(valor);
                    }

                    ///////////////////////////////////////////////////////////
                    ////OTROS DESCUENTOS NO DEDUCIBLES DE LA BASE IMPONIBLE////
                    /////////////////////////400///////////////////////////////

                    if (idcolplan.Equals("400"))
                    {
                        desnodedu = float.Parse(valor);
                    }

                    ///////////////////////////////////////////////////////////
                    ////OTROS DESCUENTOS  DEDUCIBLES DE LA BASE IMPONIBLE//////
                    /////////////////////////410///////////////////////////////

                    if (idcolplan.Equals("410"))
                    {
                        desdedu = float.Parse(valor);
                    }

                    ///////////////////////////////////////////////////////////
                    ////SISTEMA PRIVADO DE PENSIONES - APORTACION VOLUNTARIA //
                    /////////////////////////600///////////////////////////////

                    if (idcolplan.Equals("600"))
                    {
                        sppavemp = float.Parse(valor);
                    }

                    ///////////////////////////////////////////////////////////
                    ///////////////////////ESSALUD/////////////////////////////
                    /////////////////////////610///////////////////////////////

                    if (idcolplan.Equals("610"))
                    {
                        essalud = float.Parse(valor);
                    }

                    ///////////////////////////////////////////////////////////
                    ///ESSALUD - SEGURO COMPLEMENTARIO DE TRABAJO DE RIESGO////
                    /////////////////////////620///////////////////////////////

                    if (idcolplan.Equals("620"))
                    {
                        essctr = float.Parse(valor);
                    }

                    ///////////////////////////////////////////////////////////
                    ////////////////////////SENATI/////////////////////////////
                    /////////////////////////630///////////////////////////////

                    if (idcolplan.Equals("630"))
                    {
                        senati = float.Parse(valor);
                    }

                    ///////////////////////////////////////////////////////////
                    //////////OTRAS APORTACIONES DE CARDO DEL EMPLEADOR////////
                    /////////////////////////640///////////////////////////////

                    if (idcolplan.Equals("640"))
                    {
                        aporemp = float.Parse(valor);
                    }

                    ///////////////////////////////////////////////////////////
                    ////EPS - SEGURO COMPLEMENTARIO DE TRABAJO DE RIESGO///////
                    /////////////////////////650///////////////////////////////

                    if (idcolplan.Equals("650"))
                    {
                        epssctr = float.Parse(valor);
                    }

                    ///////////////////////////////////////////////////////////
                    //////////////////////SEGURO INTEGRAL DE SALUD (SIS)///////
                    /////////////////////////660///////////////////////////////

                    if (idcolplan.Equals("660"))
                    {
                        sis = float.Parse(valor);
                    }

                }
            }

            dias = diasnosubsi + diassubsi + diasefelab;

            if (bd_prov.Equals("agromango"))
            {
                total_ingresos = rembas + jorbas + dom + remvac + comides + asigfam + asigesc + asigotros + reintegro + bonifica + cts + gratifica;
            }
            else { total_ingresos = rembas + jorbas + dom + remvac + comides + imphext25 + imphext35 + imphext100 + asigfam + asigesc + asigotros + reintegro + bonifica + cts + gratifica; }

            total_descuentos = sppao + sppav + sppcp + sppps + snp + esvida + quicat + adelanto + sindica + desjud + desnodedu + desdedu + despres + desban + desseg;
            total_aporta = sppavemp + essalud + essctr + senati + aporemp + epssctr + sis;
            neto = total_ingresos - total_descuentos;
            total_hxtras = imphext25 + imphext35 + imphext100;


            query = "INSERT INTO plan (idperplan,idcia,anio, messem,idtipoper,idtipocal,idtipoplan,finilab,ffinlab,hext25,hext35,hext100,diasefelab,diassubsi, ";
            query += " diasnosubsi,dias,rembas,jorbas,dom,diasvaca,remvac,comides,imphext25,imphext35, imphext100,asigfam,asigesc, ";
            query += " asigotros,reintegro,bonifica,cts,gratifica,total_ingresos,sppao,sppav,sppcp,sppps,snp,esvida,quicat,adelanto,sindica,desjud,";
            query += " desnodedu,desdedu,total_descuentos,sppavemp,essalud,essctr,senati,aporemp,epssctr,sis,total_aporta,neto,idfonpen,";
            query += " idlabper,finivac,ffinvac,seccion,rucemp,estane,diurno,nocturno,despres,desban,desseg";

            if (bd_prov.Equals("agromango")) { query += " ,tot_horasxtra"; }

            query += " ) VALUES ('" + @idperplan + "','" + @idcia + "','" + @anio + "','" + @messem + "','" + @idtipoper + "','" + @idtipocal + "','" + @idtipoplan + "','" + @finilab + "','" + @ffinlab + "','" + @hext25 + "','" + @hext35 + "','" + @hext100 + "','" + @diasefelab + "','" + @diassubsi + "', ";
            query += " '" + @diasnosubsi + "','" + @dias + "','" + @rembas + "','" + @jorbas + "','" + @dom + "','" + @diasvaca + "','" + @remvac + "','" + @comides + "','" + @imphext25 + "','" + @imphext35 + "','" + @imphext100 + "','" + @asigfam + "','" + @asigesc + "', ";
            query += " '" + @asigotros + "','" + @reintegro + "','" + @bonifica + "','" + @cts + "','" + @gratifica + "','" + @total_ingresos + "','" + @sppao + "','" + @sppav + "','" + @sppcp + "','" + @sppps + "','" + @snp + "','" + @esvida + "','" + @quicat + "','" + @adelanto + "','" + @sindica + "','" + @desjud + "', ";
            query += " '" + @desnodedu + "','" + @desdedu + "','" + @total_descuentos + "','" + @sppavemp + "','" + @essalud + "','" + @essctr + "','" + @senati + "','" + @aporemp + "','" + @epssctr + "','" + @sis + "','" + @total_aporta + "','" + @neto + "','" + @idfonpen + "','" + @idlabper + "','" + finivac + "','" + ffinvac + "','" + @seccion + "','" + @rucemp + "','" + @estane + "','" + @diurno + "','" + @nocturno + "', ";
            query += " '" + @despres + "','" + @desban + "','" + @desseg + "'";

            if (bd_prov.Equals("agromango")) { query += " , '" + total_hxtras + "'"; }

            query += " );";

            try
            {
                SqlCommand cmdActualiza = new SqlCommand(query, conexion);
                cmdActualiza.ExecuteNonQuery();
                cmdActualiza.Dispose();

            }
            catch (SqlException e)
            {
                MessageBox.Show(e.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);

            }

            conexion.Close();

        }

        public void eliminarPlan(string idcia, string anio,
        string messem, string idtipoper, string idtipocal, string idtipoplan)
        {
            string query;
            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();
            query = "DELETE from plan_ WHERE idcia='" + @idcia + "' and anio='" + @anio + "' ";
            query = query + " and messem='" + @messem + "' and idtipoper='" + @idtipoper + "' ";
            query = query + " and idtipocal='" + @idtipocal + "' and idtipoplan='" + @idtipoplan + "';";
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
        
        public void eliminarPlanPersonal(string idcia, string anio,
        string messem, string idtipoper, string idtipocal, string idtipoplan,string idperplan)
        {
            string query;
            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();
            query = "DELETE from plan_ WHERE idcia='" + @idcia + "' and anio='" + @anio + "' ";
            query = query + " and messem='" + @messem + "' and idtipoper='" + @idtipoper + "' ";
            query = query + " and idtipocal='" + @idtipocal + "' and idtipoplan='" + @idtipoplan + "' ";
            query = query + " and idperplan='" + @idperplan + "';";
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