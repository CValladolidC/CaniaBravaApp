using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Configuration;
using System.IO;

namespace CaniaBrava
{
    public partial class ui_pdtplame : ui_form
    {
        public ui_pdtplame()
        {
            InitializeComponent();
        }

        private void cmbTipoPlan_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void cmbTipoTrabajador_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {

        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {

        }

        private void btnSalir_Click(object sender, EventArgs e)
        {

        }

        private void ui_pdtplame_Load(object sender, EventArgs e)
        {
            Funciones funciones = new Funciones();
            GlobalVariables variables = new GlobalVariables();
            string squery = "SELECT idtipoper as clave,destipoper as descripcion FROM tipoper order by 1 asc";
            funciones.listaComboBox(squery, cmbTipoTrabajador, "");
            squery = "SELECT idtipoplan as clave,destipoplan as descripcion FROM tipoplan where idtipoplan in (select idtipoplan from reglabcia where idcia='" + @variables.getValorCia() + "') order by 1 asc;";
            funciones.listaComboBox(squery, cmbTipoPlan, "");
            cmbMes.Text = "01   ENERO";
        }

        private void toolStripButton1_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ui_DatosPlanilla()
        {

            Funciones funciones = new Funciones();
            GlobalVariables globalVariable = new GlobalVariables();
            string idtipoper = funciones.getValorComboBox(cmbTipoTrabajador, 1);
            string idtipoplan = funciones.getValorComboBox(cmbTipoPlan, 3);
            string idcia = globalVariable.getValorCia();
            string aniopdt = txtAnio.Text;
            string mespdt = funciones.getValorComboBox(cmbMes, 2);
            string ruc = globalVariable.getValorRucCia();
            string valida = "G";
            string query;
            string rutaFile = string.Empty;

            if (aniopdt.Trim() == string.Empty && valida == "G")
            {
                MessageBox.Show("El Año del Periodo Tributario no es válido", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                valida = "B";
            }

            if (valida.Equals("G"))
            {
                SqlConnection conexion = new SqlConnection();
                string bd_prov = ConfigurationManager.AppSettings.Get("BD_PROV");
                conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
                conexion.Open();
                FolderBrowserDialog dialogoRuta = new FolderBrowserDialog();
                if (dialogoRuta.ShowDialog() == DialogResult.OK)
                {
                    rutaFile = dialogoRuta.SelectedPath;
                }

                if (rutaFile != string.Empty)
                {

                    /////////////////////////////////////////////////////////////////////////////////////////////////////////////
                    //////////////////////E1 DATOS DE ESTABLECIMIENTOS PROPIOS DEL EMPLEADOR/////////////////////////////////////
                    /////////////////////////////////////////////////////////////////////////////////////////////////////////////
                    //Para importar en el T-Registro la información de los establecimientos, 
                    //indicador de centro de riesgo		

                    if (chkEstPropios.Checked)
                    {
                        DataTable dt = new DataTable();
                        query = "select idestane,riesgo from estane";
                        query = query + " where idciafile='" + @idcia + "' and codemp='" + @ruc + "'; ";
                        SqlDataAdapter da = new SqlDataAdapter();
                        da.SelectCommand = new SqlCommand(query, conexion);
                        da.Fill(dt);
                        string filename = rutaFile + "/RP_" + ruc + ".esp";
                        StreamWriter archivo = File.CreateText(filename);
                        archivo.Close();
                        foreach (DataRow row_dt in dt.Rows)
                        {
                            string idestane = funciones.formatoLongitudPdt(row_dt["idestane"].ToString(), 4);
                            string riesgo = funciones.formatoLongitudPdt(row_dt["riesgo"].ToString(), 1);
                            OpeIO opeIO = new OpeIO(filename);
                            opeIO.WriteNWL(idestane + "|" + riesgo + "|");
                        }
                    }

                    /////////////////////////////////////////////////////////////////////////////////////////////////////////////
                    ////////////////////////////E2 EMPLEADORES A QUIEN DESTACO O DESPLAZO PERSONAL///////////////////////////////
                    /////////////////////////////////////////////////////////////////////////////////////////////////////////////
                    if (chkEmpDestacoPer.Checked)
                    {
                        //Para importar en el T-Registro el nombre o razón social de los empleadores 
                        //a quienes destaco/desplazo personal y el servicio prestado 				

                        DataTable dt = new DataTable();
                        string filename;
                        query = "select rucemp,actividad,fini,ffin from emplea where idciafile='" + @idcia + "'; ";
                        SqlDataAdapter da = new SqlDataAdapter();
                        da.SelectCommand = new SqlCommand(query, conexion);
                        da.Fill(dt);

                        filename = rutaFile + "/RP_" + ruc + ".edd";
                        StreamWriter archivo = File.CreateText(filename);
                        archivo.Close();
                        foreach (DataRow row_dt in dt.Rows)
                        {
                            string rucemp = funciones.formatoLongitudPdt(row_dt["rucemp"].ToString(), 11);
                            string actividad = funciones.formatoLongitudPdt(row_dt["actividad"].ToString(), 6);
                            string fini = funciones.formatoLongitudPdt(row_dt["fini"].ToString(), 10);
                            string ffin = funciones.formatoLongitudPdt(row_dt["ffin"].ToString(), 10);
                            OpeIO opeIO = new OpeIO(filename);
                            opeIO.WriteNWL(rucemp + "|" + actividad + "|" + fini + "|" + ffin + "|");
                        }

                        //Para importar información del establecimiento a donde destaca o desplaza personal 
                        //y el indicador si dicho personal desarrollará actividad de riesgo SCTR				

                        DataTable dtEsta = new DataTable();
                        query = "select  A.codemp,A.idestane,A.riesgo,B.tasa ";
                        query = query + " from estane A inner join tasaest B on ";
                        query = query + " A.idestane=B.idestane and A.codemp=B.codemp ";
                        query = query + " where A.idciafile='" + @idcia + "' and A.codemp<>'" + @ruc + "'; ";

                        SqlDataAdapter da_est = new SqlDataAdapter();
                        da_est.SelectCommand = new SqlCommand(query, conexion);
                        da_est.Fill(dtEsta);

                        filename = rutaFile + "/RP_" + ruc + ".ldd";
                        StreamWriter archivo_est = File.CreateText(filename);
                        archivo_est.Close();
                        foreach (DataRow row_dtEsta in dtEsta.Rows)
                        {
                            string codemp = funciones.formatoLongitudPdt(row_dtEsta["codemp"].ToString(), 11);
                            string idestane = funciones.formatoLongitudPdt(row_dtEsta["idestane"].ToString(), 4);
                            string riesgo = funciones.formatoLongitudPdt(row_dtEsta["riesgo"].ToString(), 1);

                            OpeIO opeIO = new OpeIO(filename);
                            opeIO.WriteNWL(codemp + "|" + idestane + "|" + riesgo + "|");
                        }
                    }

                    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                    //////////////E4 Datos personales del trabajador, pensionista, personal en formación - modalidad formativa laboral y personal de terceros/////////////////////////
                    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                    if (chkTrabPen.Checked)
                    {
                        DataTable dt = new DataTable();
                        query = "select tipdoc,nrodoc,paisemi,apepat,apemat,nombres,fecnac,sexo,nacion,disnac,telfijo,email, ";
                        query = query + " tipvia,nomvia,nrovia,deparvia,intvia,manzavia,lotevia,kmvia,block,etapa, ";
                        query = query + " tipzona,nomzona,refzona,ubigeo from perplan where rucemp='" + @ruc + "' ";
                        query = query + " and idtipoper='" + @idtipoper + "' and idcia='" + @idcia + "'";

                        SqlDataAdapter da = new SqlDataAdapter();
                        da.SelectCommand = new SqlCommand(query, conexion);
                        da.Fill(dt);

                        string filename = rutaFile + "/RP_" + ruc + ".ide";
                        StreamWriter archivo = File.CreateText(filename);
                        archivo.Close();
                        foreach (DataRow row_dt in dt.Rows)
                        {

                            string tipdoc = funciones.formatoLongitudPdt(row_dt["tipdoc"].ToString(), 2);
                            string nrodoc = funciones.formatoLongitudPdt(row_dt["nrodoc"].ToString(), 15);
                            string paisemi = funciones.formatoLongitudPdt(row_dt["paisemi"].ToString(), 3);
                            string fecnac = funciones.formatoLongitudPdt(row_dt["fecnac"].ToString(), 10);
                            string apepat = funciones.formatoLongitudPdt(row_dt["apepat"].ToString(), 40);
                            string apemat = funciones.formatoLongitudPdt(row_dt["apemat"].ToString(), 40);
                            string nombres = funciones.formatoLongitudPdt(row_dt["nombres"].ToString(), 40);
                            string sexo;
                            if (row_dt["sexo"].ToString().Equals("M"))
                            {
                                sexo = "1";
                            }
                            else
                            {
                                sexo = "2";
                            }
                            string nacion = funciones.formatoLongitudPdt(row_dt["nacion"].ToString(), 4);
                            string disnac = funciones.formatoLongitudPdt(row_dt["disnac"].ToString(), 3);
                            string telfijo = funciones.formatoLongitudPdt(row_dt["telfijo"].ToString(), 9);
                            string email = funciones.formatoLongitudPdt(row_dt["email"].ToString(), 50);
                            string tipvia = funciones.formatoLongitudPdt(row_dt["tipvia"].ToString(), 2);
                            string nomvia = funciones.formatoLongitudPdt(row_dt["nomvia"].ToString(), 20);
                            string nrovia = funciones.formatoLongitudPdt(row_dt["nrovia"].ToString(), 4);
                            string deparvia = funciones.formatoLongitudPdt(row_dt["deparvia"].ToString(), 4);
                            string intvia = funciones.formatoLongitudPdt(row_dt["intvia"].ToString(), 4);
                            string manzavia = funciones.formatoLongitudPdt(row_dt["manzavia"].ToString(), 4);
                            string lotevia = funciones.formatoLongitudPdt(row_dt["lotevia"].ToString(), 4);
                            string kmvia = funciones.formatoLongitudPdt(row_dt["kmvia"].ToString(), 4);
                            string block = funciones.formatoLongitudPdt(row_dt["block"].ToString(), 4);
                            string etapa = funciones.formatoLongitudPdt(row_dt["etapa"].ToString(), 4);
                            string tipzona = funciones.formatoLongitudPdt(row_dt["tipzona"].ToString(), 2);
                            string nomzona = funciones.formatoLongitudPdt(row_dt["nomzona"].ToString(), 20);
                            string refzona = funciones.formatoLongitudPdt(row_dt["refzona"].ToString(), 40);
                            string ubigeo = funciones.formatoLongitudPdt(row_dt["ubigeo"].ToString(), 6);

                            string tipvia1 = funciones.formatoLongitudPdt("", 2);
                            string nomvia1 = funciones.formatoLongitudPdt("", 20);
                            string nrovia1 = funciones.formatoLongitudPdt("", 4);
                            string deparvia1 = funciones.formatoLongitudPdt("", 4);
                            string intvia1 = funciones.formatoLongitudPdt("", 4);
                            string manzavia1 = funciones.formatoLongitudPdt("", 4);
                            string lotevia1 = funciones.formatoLongitudPdt("", 4);
                            string kmvia1 = funciones.formatoLongitudPdt("", 4);
                            string block1 = funciones.formatoLongitudPdt("", 4);
                            string etapa1 = funciones.formatoLongitudPdt("", 4);
                            string tipzona1 = funciones.formatoLongitudPdt("", 2);
                            string nomzona1 = funciones.formatoLongitudPdt("", 20);
                            string refzona1 = funciones.formatoLongitudPdt("", 40);
                            string ubigeo1 = funciones.formatoLongitudPdt("", 6);

                            string indcaessa = "1";

                            OpeIO opeIO = new OpeIO(filename);
                            opeIO.WriteNWL(tipdoc + "|" + nrodoc + "|" + paisemi + "|" + fecnac + "|" + apepat + "|" + apemat + "|" + nombres + "|" + sexo + "|" + nacion
                                + "|" + disnac + "|" + telfijo + "|" + email + "|" + "|" + tipvia + "|" + nomvia + "|" + nrovia + "|" + deparvia
                                + "|" + intvia + "|" + manzavia + "|" + lotevia + "|" + kmvia + "|" + block + "|" + etapa + "|" + tipzona + "|" + nomzona + "|" + refzona + "|" + ubigeo
                                + "|" + tipvia1 + "|" + nomvia1 + "|" + nrovia1 + "|" + deparvia1
                                + "|" + intvia1 + "|" + manzavia1 + "|" + lotevia1 + "|" + kmvia1 + "|" + block1 + "|" + etapa1 + "|" + tipzona1 + "|" + nomzona1 + "|" + refzona1 + "|" + ubigeo1 + "|" + indcaessa + "|");
                        }

                    }


                    /////////////////////////////////////////////////////////////////////////////////////////////////////////////
                    ///////////////////////////////////////////DATOS DEL TRABAJADOR///////////////////////////////////////////////
                    /////////////////////////////////////////////////////////////////////////////////////////////////////////////
                    if (chkDatosTrabajador.Checked)
                    {
                        DataTable dt = new DataTable();
                        query = " select A.tipdoc,A.nrodoc,A.paisemi,A.reglab,A.nivedu,A.ocurpts,A.discapa, ";
                        query = query + " A.cuspp,A.sctrpennin,A.sctrpenonp,A.sctrpenseg,A.contrab,";
                        query = query + " A.regalterna,A.trabmax,A.trabnoc,A.sindica,A.pering,";
                        query = query + " CASE ISNULL(D.importe) WHEN 1 THEN 0 WHEN 0 THEN ROUND(D.importe,2) END as rembas, ";
                        query = query + " A.situatrab,A.quicat,A.sitesp,A.tippag,C.pdt as catocu,A.apliconve,A.ruc ";
                        query = query + " from perplan A inner join ciafile B ";
                        query = query + " on A.idcia=B.idcia inner join tipoper C on A.idtipoper=C.idtipoper ";
                        query = query + " left join remu D on A.idcia=D.idcia and A.idperplan=D.idperplan and D.state='V' ";
                        query = query + " where rucemp='" + @ruc + "' and A.idtipoper='" + @idtipoper + "' and A.idcia='" + @idcia + "'";

                        SqlDataAdapter da = new SqlDataAdapter();
                        da.SelectCommand = new SqlCommand(query, conexion);
                        da.Fill(dt);

                        string filename = rutaFile + "/RP_" + ruc + ".tra";
                        StreamWriter archivo = File.CreateText(filename);
                        archivo.Close();
                        foreach (DataRow row_dt in dt.Rows)
                        {

                            string tipdoc = funciones.formatoLongitudPdt(row_dt["tipdoc"].ToString(), 2);
                            string nrodoc = funciones.formatoLongitudPdt(row_dt["nrodoc"].ToString(), 15);
                            string paisemi = funciones.formatoLongitudPdt(row_dt["paisemi"].ToString(), 3);
                            string reglab = funciones.formatoLongitudPdt(row_dt["reglab"].ToString(), 2);
                            string nivedu = funciones.formatoLongitudPdt(row_dt["nivedu"].ToString(), 2);
                            string ocurpts = funciones.formatoLongitudPdt(row_dt["ocurpts"].ToString(), 6);
                            string discapa = funciones.formatoLongitudPdt(row_dt["discapa"].ToString(), 1);
                            string cuspp = funciones.formatoLongitudPdt(row_dt["cuspp"].ToString(), 12);
                            string sctrpension = "0";

                            if (row_dt["sctrpennin"].ToString().Equals("1"))
                            {
                                sctrpension = "0";
                            }
                            else
                            {
                                if (row_dt["sctrpenonp"].ToString().Equals("1"))
                                {
                                    sctrpension = "1";
                                }
                                else
                                {
                                    if (row_dt["sctrpenseg"].ToString().Equals("1"))
                                    {
                                        sctrpension = "2";
                                    }
                                }
                            }

                            sctrpension = funciones.formatoLongitudPdt(sctrpension, 1);
                            string contrab = funciones.formatoLongitudPdt(row_dt["contrab"].ToString(), 2);
                            string regalterna = funciones.formatoLongitudPdt(row_dt["regalterna"].ToString(), 1);
                            string trabmax = funciones.formatoLongitudPdt(row_dt["trabmax"].ToString(), 1);
                            string trabnoc = funciones.formatoLongitudPdt(row_dt["trabnoc"].ToString(), 1);
                            string sindica = funciones.formatoLongitudPdt(row_dt["sindica"].ToString(), 1);
                            string pering = funciones.formatoLongitudPdt(row_dt["pering"].ToString(), 1);
                            string rembas = funciones.formatoLongitudPdt("0.00", 9);
                            if (reglab.Equals("01"))
                            {
                                rembas = funciones.formatoLongitudPdt(funciones.formatoPdtNumerico(row_dt["rembas"].ToString()), 9);
                            }
                            string situatrab = funciones.formatoLongitudPdt(row_dt["situatrab"].ToString(), 2);
                            string quicat = funciones.formatoLongitudPdt(row_dt["quicat"].ToString(), 1);
                            string sitesp = funciones.formatoLongitudPdt(row_dt["sitesp"].ToString(), 1);
                            string tippag = funciones.formatoLongitudPdt(row_dt["tippag"].ToString(), 1);
                            string catocu = funciones.formatoLongitudPdt(row_dt["catocu"].ToString(), 2);
                            string apliconve = funciones.formatoLongitudPdt(row_dt["apliconve"].ToString(), 1);
                            string rucper = funciones.formatoLongitudPdt(row_dt["ruc"].ToString(), 11);

                            OpeIO opeIO = new OpeIO(filename);
                            opeIO.WriteNWL(tipdoc + "|" + nrodoc + "|" + paisemi + "|" + reglab + "|" +
                                nivedu + "|" + ocurpts + "|" + discapa + "|" + cuspp + "|" + sctrpension + "|" +
                                contrab + "|" + regalterna + "|" + trabmax + "|" + trabnoc + "|" + sindica + "|" + pering + "|" +
                                rembas + "|" + situatrab + "|" + quicat + "|" + sitesp + "|" + tippag + "|" +
                                catocu + "|" + apliconve + "|" + rucper + "|");
                        }

                    }

                    /////////////////////////////////////////////////////////////////////////////////////////////////////////////
                    //////////////////////DATOS DE PERIODOS//////////////////////////////////////////////////////////////////////
                    /////////////////////////////////////////////////////////////////////////////////////////////////////////////
                    if (chkPeriodos.Checked)
                    {
                        DataTable dt = new DataTable();
                        query = " select B.tipdoc ,B.nrodoc,B.paisemi,'1' as categoria,'1' as tipreg,";
                        query = query + " A.fechaini,A.fechafin,A.motivo,'' as eps";
                        query = query + " from perlab A inner join perplan B on A.idcia=B.idcia and A.idperplan=B.idperplan";
                        query = query + " where A.idcia='" + @idcia + "' and B.idtipoper='" + @idtipoper + "'; ";

                        SqlDataAdapter da = new SqlDataAdapter();
                        da.SelectCommand = new SqlCommand(query, conexion);
                        da.Fill(dt);

                        string filename = rutaFile + "/RP_" + ruc + ".per";
                        StreamWriter archivo = File.CreateText(filename);
                        archivo.Close();
                        foreach (DataRow row_dt in dt.Rows)
                        {
                            string tipdoc = funciones.formatoLongitudPdt(row_dt["tipdoc"].ToString(), 2);
                            string nrodoc = funciones.formatoLongitudPdt(row_dt["nrodoc"].ToString(), 15);
                            string paisemi = funciones.formatoLongitudPdt(row_dt["paisemi"].ToString(), 3);
                            string categoria = funciones.formatoLongitudPdt(row_dt["categoria"].ToString(), 1);
                            string tipreg = funciones.formatoLongitudPdt(row_dt["tipreg"].ToString(), 1);
                            string fechaini = funciones.formatoLongitudPdt(row_dt["fechaini"].ToString(), 10);
                            string fechafin = funciones.formatoLongitudPdt(row_dt["fechafin"].ToString(), 10);
                            string motivo = funciones.formatoLongitudPdt(row_dt["motivo"].ToString(), 2);
                            string eps = funciones.formatoLongitudPdt(row_dt["eps"].ToString(), 1);

                            OpeIO opeIO = new OpeIO(filename);
                            opeIO.WriteNWL(tipdoc + "|" + nrodoc + "|" + paisemi + "|" + categoria + "|" + tipreg + "|" + fechaini +
                                "|" + fechafin + "|" + motivo + "|" + eps + "|");
                        }

                    }


                    /////////////////////////////////////////////////////////////////////////////////////////////////////////////
                    //////////////////////DATOS DE LA JORNADA LABORAL DEL TRABAJADOR/////////////////////////////////////////////
                    /////////////////////////////////////////////////////////////////////////////////////////////////////////////

                    if (chkJorTrab.Checked)
                    {
                        try
                        {
                            DataTable dt = new DataTable();
                            int numhext;
                            query = "select A.idperplan,C.tipdoc,C.nrodoc,SUM(A.diasefelab) as diasefelab,";

                            if (bd_prov.Equals("agromango")) { query += "  0 "; }
                            else { query += "  ROUND(SUM(hext25+hext35+hext100)) "; }

                            query += "as hext from dataplan A ";
                            query += "inner join perplan C on A.idperplan=C.idperplan and A.idcia=C.idcia ";
                            query += "inner join calplan E on A.idtipocal=E.idtipocal and A.idcia=E.idcia ";
                            query += "and A.messem=E.messem and A.anio=E.anio and A.idtipoper=E.idtipoper ";
                            query += "where E.aniopdt='" + @aniopdt + "' and E.mespdt='" + @mespdt + "'  ";
                            query += "and A.idcia='" + @idcia + "' and A.idtipoper='" + @idtipoper + "' ";
                            query += "and A.idtipoplan='" + @idtipoplan + "' ";
                            query += "group by A.idperplan,C.tipdoc,C.nrodoc ;";

                            SqlDataAdapter da = new SqlDataAdapter();
                            da.SelectCommand = new SqlCommand(query, conexion);
                            da.Fill(dt);

                            string filename = rutaFile + "/" + "0601" + aniopdt + mespdt + ruc + ".jor";
                            StreamWriter archivo = File.CreateText(filename);
                            archivo.Close();
                            foreach (DataRow row_dt in dt.Rows)
                            {
                                numhext = int.Parse(row_dt["hext"].ToString());
                                string tipdoc = funciones.formatoLongitudPdt(row_dt["tipdoc"].ToString(), 2).Trim();
                                string nrodoc = funciones.formatoLongitudPdt(row_dt["nrodoc"].ToString(), 15).Trim();
                                string nhordtrab = funciones.formatoLongitudPdt((int.Parse(row_dt["diasefelab"].ToString()) * 8).ToString(), 3).Trim();
                                string nminordtrab = "";
                                string nhorsobtrab = "";
                                if (numhext > 0)
                                {
                                    nhorsobtrab = funciones.formatoLongitudPdt(numhext.ToString(), 3).Trim();
                                }
                                string nminsobtrab = "";

                                OpeIO opeIO = new OpeIO(filename);
                                opeIO.WriteNWL(tipdoc + "|" + nrodoc + "|" + nhordtrab + "|" + nminordtrab + "|" + nhorsobtrab + "|" + nminsobtrab + "|");
                            }
                        }
                        catch (Exception e)
                        {
                            MessageBox.Show(e.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }

                    if (chkIngTribDesc.Checked)
                    {
                        DataTable dt = new DataTable();
                        query = " select A.idperplan,C.tipdoc,C.nrodoc,B.pdt, ";
                        /*if (bd_prov.Equals("agromango"))
                        {
                            query += " ROUND(CASE A.idconplan WHEN '3000' THEN SUM(A.valor)-(sum(ifnull(xx.valor,0)+ifnull(yy.valor,0)+ifnull(zz.valor,0))*0.10) ";
                            query += " ELSE SUM(A.valor) END,2) as valor, ";
                        }
                        else { query += " ROUND(SUM(A.valor),2) as valor, "; }*/
                        query += " ROUND(SUM(A.valor),2) as valor, ";
                        query += " D.devenplame,D.pagaplame,D.regceroplame ";
                        query += "  from conbol A ";
                        query += " inner join detconplan B on A.idcia=B.idcia and ";
                        query += " A.idtipoplan=B.idtipoplan and A.idtipocal=B.idtipocal and A.idtipoper=B.idtipoper ";
                        if (bd_prov.Equals("agromango")) { query += "AND B.idcolplan NOT IN ('010','020','030','100','110','120') "; }
                        query += " and A.idconplan=B.idconplan inner join perplan C on A.idperplan=C.idperplan and A.idcia=C.idcia ";
                        query += " inner join conpdt D on B.pdt=D.idconpdt inner join calplan E on A.idtipocal=E.idtipocal and A.idcia=E.idcia ";
                        query += " and A.messem=E.messem and A.anio=E.anio and A.idtipoper=E.idtipoper ";
                        /*if (bd_prov.Equals("agromango"))
                        {
                            query += " left join conbol xx on A.idperplan=xx.idperplan and A.idcia=xx.idcia and A.anio=xx.anio and A.messem=xx.messem and A.idtipoper=xx.idtipoper ";
                            query += " and A.idtipocal=xx.idtipocal and A.idtipoplan=xx.idtipoplan and xx.idconplan = '0500' ";
                            query += " left join conbol yy on A.idperplan=yy.idperplan and A.idcia=yy.idcia and A.anio=yy.anio and A.messem=yy.messem and A.idtipoper=yy.idtipoper ";
                            query += " and A.idtipocal=yy.idtipocal and A.idtipoplan=yy.idtipoplan and yy.idconplan = '0600' ";
                            query += " left join conbol zz on A.idperplan=zz.idperplan and A.idcia=zz.idcia and A.anio=zz.anio and A.messem=zz.messem and A.idtipoper=zz.idtipoper ";
                            query += " and A.idtipocal=zz.idtipocal and A.idtipoplan=zz.idtipoplan and zz.idconplan = '0700' ";
                        }*/
                        query += " where E.aniopdt='" + @aniopdt + "' and E.mespdt='" + @mespdt + "'  ";
                        query += " and A.idcia='" + @idcia + "' and A.idtipoper='" + @idtipoper + "' ";
                        query += " and A.idtipoplan='" + @idtipoplan + "' ";
                        query += " group by A.idperplan,C.tipdoc,C.nrodoc,B.pdt,D.devengado,D.pagado,D.regcero ;";

                        SqlDataAdapter da = new SqlDataAdapter();
                        da.SelectCommand = new SqlCommand(query, conexion);
                        da.Fill(dt);

                        string filename = rutaFile + "/" + "0601" + aniopdt + mespdt + ruc + ".rem";
                        StreamWriter archivo = File.CreateText(filename);
                        archivo.Close();
                        foreach (DataRow row_dt in dt.Rows)
                        {
                            float valorConcepto;
                            string regcero;
                            string procesa = "S";

                            valorConcepto = float.Parse(row_dt["valor"].ToString());

                            regcero = row_dt["regceroplame"].ToString();

                            if (valorConcepto > 0)
                            {
                                procesa = "S";
                            }
                            else
                            {
                                if (regcero.Equals("S"))
                                {
                                    procesa = "S";
                                }
                                else
                                {
                                    procesa = "N";
                                }
                            }

                            if (procesa.Equals("S"))
                            {
                                string tipdoc = funciones.formatoLongitudPdt(row_dt["tipdoc"].ToString(), 2).Trim();
                                string nrodoc = funciones.formatoLongitudPdt(row_dt["nrodoc"].ToString(), 15).Trim();
                                string pdt = funciones.formatoLongitudPdt(row_dt["pdt"].ToString(), 4).Trim();
                                string valor = funciones.formatoPdtNumerico(row_dt["valor"].ToString()).Trim();
                                string isdevengado = row_dt["devenplame"].ToString();
                                string ispagado = row_dt["pagaplame"].ToString();
                                string devengado;
                                string pagado;
                                if (isdevengado.Equals("S"))
                                {
                                    devengado = funciones.formatoLongitudPdt(valor, 9).Trim();
                                }
                                else
                                {
                                    devengado = funciones.formatoLongitudPdt("0.00", 9).Trim();
                                }

                                if (ispagado.Equals("S"))
                                {
                                    pagado = funciones.formatoLongitudPdt(valor, 9).Trim();
                                }
                                else
                                {
                                    pagado = funciones.formatoLongitudPdt("0.00", 9).Trim();
                                }

                                OpeIO opeIO = new OpeIO(filename);
                                opeIO.WriteNWL(tipdoc + "|" + nrodoc + "|" + pdt + "|" + devengado + "|"
                                    + pagado + "|");
                            }
                        }

                    }

                    /////////////////////////////////////////////////////////////////////////////////////////////////////////////
                    //////////////////////DATOS DE LOS DIAS SUBSIDIADOS DEL TRABAJADOR///////////////////////////////////////////
                    /////////////////////////////////////////////////////////////////////////////////////////////////////////////
                    if (chkDiasSubsi.Checked)
                    {
                        DataTable dt = new DataTable();
                        query = " select C.tipdoc,C.nrodoc,B.desmaesgen,SUM(A.dias) as dias,A.motivo from diassubsi A left join maesgen B";
                        query = query + " on A.motivo=B.clavemaesgen and B.idmaesgen in ('030','031') ";
                        query = query + " inner join perplan C on A.idperplan=C.idperplan and A.idcia=C.idcia ";
                        query = query + " inner join calplan E on A.idtipocal=E.idtipocal and A.idcia=E.idcia ";
                        query = query + " and A.messem=E.messem and A.anio=E.anio and A.idtipoper=E.idtipoper ";
                        query = query + " where A.idcia='" + @idcia + "' and E.aniopdt='" + @aniopdt + "' ";
                        query = query + " and E.mespdt='" + @mespdt + "' and A.idtipoper='" + @idtipoper + "' ";
                        query = query + " and A.idtipoplan='" + @idtipoplan + "' group by C.tipdoc,C.nrodoc,B.desmaesgen,A.motivo;";

                        SqlDataAdapter da = new SqlDataAdapter();
                        da.SelectCommand = new SqlCommand(query, conexion);
                        da.Fill(dt);

                        string filename = rutaFile + "/" + "0601" + aniopdt + mespdt + ruc + ".snl";
                        StreamWriter archivo = File.CreateText(filename);
                        archivo.Close();
                        foreach (DataRow row_dt in dt.Rows)
                        {
                            string tipdoc = funciones.formatoLongitudPdt(row_dt["tipdoc"].ToString(), 2).Trim();
                            string nrodoc = funciones.formatoLongitudPdt(row_dt["nrodoc"].ToString(), 15).Trim();
                            string motivo = funciones.formatoLongitudPdt(row_dt["motivo"].ToString(), 2).Trim();
                            string dias = funciones.formatoLongitudPdt(row_dt["dias"].ToString(), 2).Trim();
                            OpeIO opeIO = new OpeIO(filename);
                            opeIO.WriteNWL(tipdoc + "|" + nrodoc + "|" + motivo + "|" + dias + "|");
                        }
                    }
                }

                conexion.Close();
                MessageBox.Show("Exportación SUNAT Finalizada", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }

        private void btnNuevo_Click_1(object sender, EventArgs e)
        {
            ui_DatosPlanilla();
        }
    }
}