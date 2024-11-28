using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Configuration;
using System.IO;

namespace CaniaBrava
{
    public partial class ui_PlanElecSunat : Form
    {
        public ui_PlanElecSunat()
        {
            InitializeComponent();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ui_PlanElecSunat_Load(object sender, EventArgs e)
        {
            Funciones funciones = new Funciones();
            GlobalVariables variables = new GlobalVariables();
            string squery = "SELECT idtipoper as clave,destipoper as descripcion FROM tipoper order by 1 asc";
            funciones.listaComboBox(squery, cmbTipoTrabajador, "");
            squery = "SELECT idtipoplan as clave,destipoplan as descripcion FROM tipoplan where idtipoplan in (select idtipoplan from reglabcia where idcia='" + @variables.getValorCia() + "') order by 1 asc;";
            funciones.listaComboBox(squery, cmbTipoPlan, "");
            cmbMes.Text = "01   ENERO";

        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtMesSem1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {

                e.Handled = true;
                cmbMes.Focus();

            }
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            ui_DatosPlanilla();
        }

        private void ui_DatosPlanilla()
        {
            DataTable dtIngTribDesc = new DataTable();
            DataTable dtEstPropios = new DataTable();
            DataTable dtEmpDestacoPer = new DataTable();
            DataTable dtEstCenRiesgo = new DataTable();
            DataTable dtTrabPen = new DataTable();
            DataTable dtDerHab = new DataTable();
            DataTable dtJorTrab = new DataTable();
            DataTable dtDiasSubsi = new DataTable();
            DataTable dtDatosTrab = new DataTable();
            DataTable dtPeriodos = new DataTable();
            DataTable dtEstaTrab = new DataTable();


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

                /////////////////////////////////////////////////////////////////////////////////////////////////////////////
                //////////////////////DETALLE DE INGRESOS, DESCUENTOS Y APORTACIONES DEL TRABAJADOR//////////////////////////
                /////////////////////////////////////////////////////////////////////////////////////////////////////////////

                SqlConnection conexion = new SqlConnection();
                conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
                conexion.Open();

                FolderBrowserDialog dialogoRuta = new FolderBrowserDialog();
                if (dialogoRuta.ShowDialog() == DialogResult.OK)
                {
                    rutaFile = dialogoRuta.SelectedPath;
                }

                if (rutaFile != string.Empty)
                {

                    if (chkIngTribDesc.Checked)
                    {
                        query = " select A.idperplan,C.tipdoc,C.nrodoc,B.pdt,ROUND(SUM(A.valor),2) as valor,D.devengado,D.pagado,D.regcero ";
                        query = query + "  from conbol A ";
                        query = query + " inner join detconplan B on A.idcia=B.idcia and ";
                        query = query + " A.idtipoplan=B.idtipoplan and A.idtipocal=B.idtipocal and A.idtipoper=B.idtipoper ";
                        query = query + " and A.idconplan=B.idconplan inner join perplan C on A.idperplan=C.idperplan and A.idcia=C.idcia ";
                        query = query + " inner join conpdt D on B.pdt=D.idconpdt inner join calplan E on A.idtipocal=E.idtipocal and A.idcia=E.idcia ";
                        query = query + " and A.messem=E.messem and A.anio=E.anio and A.idtipoper=E.idtipoper ";
                        query = query + " where E.aniopdt='" + @aniopdt + "' and E.mespdt='" + @mespdt + "'  ";
                        query = query + " and A.idcia='" + @idcia + "' and A.idtipoper='" + @idtipoper + "' ";
                        query = query + " and A.idtipoplan='" + @idtipoplan + "' ";
                        query = query + " group by A.idperplan,C.tipdoc,C.nrodoc,B.pdt,D.devengado,D.pagado,D.regcero ;";

                        SqlDataAdapter da = new SqlDataAdapter();
                        da.SelectCommand = new SqlCommand(query, conexion);
                        da.Fill(dtIngTribDesc);

                        string filename = rutaFile + "/" + "0601" + aniopdt + mespdt + ruc + ".rem";
                        StreamWriter archivo = File.CreateText(filename);
                        archivo.Close();
                        foreach (DataRow row_dtIngTribDesc in dtIngTribDesc.Rows)
                        {
                            float valorConcepto;
                            string regcero;
                            string procesa = "S";

                            valorConcepto = float.Parse(row_dtIngTribDesc["valor"].ToString());

                            regcero = row_dtIngTribDesc["regcero"].ToString();

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
                                string tipdoc = funciones.formatoLongitudPdt(row_dtIngTribDesc["tipdoc"].ToString(), 2);
                                string nrodoc = funciones.formatoLongitudPdt(row_dtIngTribDesc["nrodoc"].ToString(), 15);
                                string pdt = funciones.formatoLongitudPdt(row_dtIngTribDesc["pdt"].ToString(), 4);
                                string valor = funciones.formatoPdtNumerico(row_dtIngTribDesc["valor"].ToString());
                                string isdevengado = row_dtIngTribDesc["devengado"].ToString();
                                string ispagado = row_dtIngTribDesc["pagado"].ToString();
                                string devengado;
                                string pagado;
                                if (isdevengado.Equals("S"))
                                {
                                    devengado = funciones.formatoLongitudPdt(valor, 9);
                                }
                                else
                                {
                                    devengado = funciones.formatoLongitudPdt("0.00", 9);
                                }

                                if (ispagado.Equals("S"))
                                {
                                    pagado = funciones.formatoLongitudPdt(valor, 9);
                                }
                                else
                                {
                                    pagado = funciones.formatoLongitudPdt("0.00", 9);
                                }

                                OpeIO opeIO = new OpeIO(filename);
                                opeIO.WriteNWL(tipdoc + "|" + nrodoc + "|" + pdt + "|" + devengado + "|"
                                    + pagado + "|");
                            }
                        }

                    }

                    /////////////////////////////////////////////////////////////////////////////////////////////////////////////
                    //////////////////////DATOS DE ESTABLECIMIENTOS PROPIOS//////////////////////////////////////////////////////
                    /////////////////////////////////////////////////////////////////////////////////////////////////////////////
                    if (chkEstPropios.Checked)
                    {
                        query = " select idestane,codemp,desestane,tipoestane,idciafile,riesgo from estane";
                        query += "where idciafile='" + @idcia + "' and codemp='" + @ruc + "'; ";
                        SqlDataAdapter da = new SqlDataAdapter();
                        da.SelectCommand = new SqlCommand(query, conexion);
                        da.Fill(dtEstPropios);

                        string filename = rutaFile + "/" + ruc + ".esp";
                        StreamWriter archivo = File.CreateText(filename);
                        archivo.Close();
                        foreach (DataRow row_dtEstPropios in dtEstPropios.Rows)
                        {
                            string tipoestane = funciones.formatoLongitudPdt(row_dtEstPropios["tipoestane"].ToString(), 2);
                            string idestane = funciones.formatoLongitudPdt(row_dtEstPropios["idestane"].ToString(), 4);
                            string desestane = funciones.formatoLongitudPdt(row_dtEstPropios["desestane"].ToString(), 40);
                            string riesgo = funciones.formatoLongitudPdt(row_dtEstPropios["riesgo"].ToString(), 1);

                            OpeIO opeIO = new OpeIO(filename);
                            opeIO.WriteNWL(tipoestane + "|" + idestane + "|" + desestane + "|" + riesgo + "|");
                        }

                    }


                    /////////////////////////////////////////////////////////////////////////////////////////////////////////////
                    //////////////////////DATOS DE ESTABLECIMIENTOS DONDE LABORA EL TRABAJADOR///////////////////////////////////
                    /////////////////////////////////////////////////////////////////////////////////////////////////////////////
                    if (chkEstaTrab.Checked)
                    {
                        query = "select A.tipdoc,A.nrodoc,A.rucemp,A.estane,B.riesgo,C.tasa from perplan A inner join estane B ";
                        query = query + " on A.idcia=B.idciafile and A.rucemp=B.codemp and A.estane=B.idestane ";
                        query = query + " left join tasaest C on B.idestane=C.idestane and B.idciafile=C.idciafile ";
                        query = query + " and B.codemp=C.codemp ";
                        query = query + " where A.idcia='" + @idcia + "' and A.idtipoper='" + @idtipoper + "'; ";

                        SqlDataAdapter da = new SqlDataAdapter();
                        da.SelectCommand = new SqlCommand(query, conexion);
                        da.Fill(dtEstaTrab);

                        string filename = rutaFile + "/" + "0601" + aniopdt + mespdt + ruc + ".tes";

                        StreamWriter archivo = File.CreateText(filename);
                        archivo.Close();
                        string tasa;

                        foreach (DataRow row_dtEstaTrab in dtEstaTrab.Rows)
                        {
                            string tipdoc = funciones.formatoLongitudPdt(row_dtEstaTrab["tipdoc"].ToString(), 2);
                            string nrodoc = funciones.formatoLongitudPdt(row_dtEstaTrab["nrodoc"].ToString(), 15);
                            string rucemp = funciones.formatoLongitudPdt(row_dtEstaTrab["rucemp"].ToString(), 11);
                            string estane = funciones.formatoLongitudPdt(row_dtEstaTrab["estane"].ToString(), 4);
                            string riesgo = row_dtEstaTrab["riesgo"].ToString();

                            if (riesgo.Equals("1"))
                            {
                                tasa = funciones.formatoLongitudPdt(row_dtEstaTrab["tasa"].ToString(), 5);
                            }
                            else
                            {
                                tasa = funciones.formatoLongitudPdt("", 5);
                            }

                            OpeIO opeIO = new OpeIO(filename);
                            opeIO.WriteNWL(tipdoc + "|" + nrodoc + "|" + rucemp + "|" + estane + "|" + tasa + "|");
                        }

                    }


                    /////////////////////////////////////////////////////////////////////////////////////////////////////////////
                    //////////////////////DATOS DE LOS DIAS SUBSIDIADOS DEL TRABAJADOR///////////////////////////////////////////
                    /////////////////////////////////////////////////////////////////////////////////////////////////////////////
                    if (chkDiasSubsi.Checked)
                    {
                        string tipo = "S";
                        query = " select C.tipdoc,C.nrodoc,B.desmaesgen,A.dias,A.fechaini,A.fechafin,A.citt,";
                        query = query + " A.diascitt,A.motivo,A.iddiassubsi from diassubsi A left join maesgen B";
                        query = query + " on A.motivo=B.clavemaesgen and B.idmaesgen='030' ";
                        query = query + " inner join perplan C on A.idperplan=C.idperplan and A.idcia=C.idcia ";
                        query = query + " inner join calplan E on A.idtipocal=E.idtipocal and A.idcia=E.idcia ";
                        query = query + " and A.messem=E.messem and A.anio=E.anio and A.idtipoper=E.idtipoper ";
                        query = query + " where A.idcia='" + @idcia + "' and E.aniopdt='" + @aniopdt + "' ";
                        query = query + " and E.mespdt='" + @mespdt + "' and A.idtipoper='" + @idtipoper + "' ";
                        query = query + " and A.idtipoplan='" + @idtipoplan + "' and A.tipo='" + @tipo + "';";

                        SqlDataAdapter da = new SqlDataAdapter();
                        da.SelectCommand = new SqlCommand(query, conexion);
                        da.Fill(dtDiasSubsi);

                        string filename = rutaFile + "/" + "0601" + aniopdt + mespdt + ruc + ".sub";
                        StreamWriter archivo = File.CreateText(filename);
                        archivo.Close();
                        foreach (DataRow row_dtDiasSubsi in dtDiasSubsi.Rows)
                        {

                            string tipdoc = funciones.formatoLongitudPdt(row_dtDiasSubsi["tipdoc"].ToString(), 2);
                            string nrodoc = funciones.formatoLongitudPdt(row_dtDiasSubsi["nrodoc"].ToString(), 15);
                            string motivo = funciones.formatoLongitudPdt(row_dtDiasSubsi["motivo"].ToString(), 2);
                            string citt = funciones.formatoLongitudPdt(row_dtDiasSubsi["citt"].ToString(), 16);
                            string fechaini = funciones.formatoLongitudPdt(row_dtDiasSubsi["fechaini"].ToString(), 10);
                            string fechafin = funciones.formatoLongitudPdt(row_dtDiasSubsi["fechafin"].ToString(), 10);
                            OpeIO opeIO = new OpeIO(filename);
                            opeIO.WriteNWL(tipdoc + "|" + nrodoc + "|" + motivo + "|" + citt + "|" + fechaini + "|" + fechafin + "|");
                        }

                    }


                    /////////////////////////////////////////////////////////////////////////////////////////////////////////////
                    //////////////////////DATOS DE LOS DIAS NO TRABAJADOS Y NO SUBSIDIADOS DEL TRABAJADOR////////////////////////
                    /////////////////////////////////////////////////////////////////////////////////////////////////////////////
                    if (chkDiasNoSubsi.Checked)
                    {
                        string tipo = "N";
                        query = " select C.tipdoc,C.nrodoc,B.desmaesgen,A.dias,A.fechaini,A.fechafin,A.citt,";
                        query = query + " A.diascitt,A.motivo,A.iddiassubsi from diassubsi A left join maesgen B";
                        query = query + " on A.motivo=B.clavemaesgen and B.idmaesgen='030' ";
                        query = query + " inner join perplan C on A.idperplan=C.idperplan and A.idcia=C.idcia ";
                        query = query + " inner join calplan E on A.idtipocal=E.idtipocal and A.idcia=E.idcia ";
                        query = query + " and A.messem=E.messem and A.anio=E.anio and A.idtipoper=E.idtipoper ";
                        query = query + " where A.idcia='" + @idcia + "' and E.aniopdt='" + @aniopdt + "' ";
                        query = query + " and E.mespdt='" + @mespdt + "' and A.idtipoper='" + @idtipoper + "' ";
                        query = query + " and A.idtipoplan='" + @idtipoplan + "' and A.tipo='" + @tipo + "';";

                        SqlDataAdapter da = new SqlDataAdapter();
                        da.SelectCommand = new SqlCommand(query, conexion);
                        da.Fill(dtDiasSubsi);

                        string filename = rutaFile + "/" + "0601" + aniopdt + mespdt + ruc + ".not";
                        StreamWriter archivo = File.CreateText(filename);
                        archivo.Close();
                        foreach (DataRow row_dtDiasSubsi in dtDiasSubsi.Rows)
                        {

                            string tipdoc = funciones.formatoLongitudPdt(row_dtDiasSubsi["tipdoc"].ToString(), 2);
                            string nrodoc = funciones.formatoLongitudPdt(row_dtDiasSubsi["nrodoc"].ToString(), 15);
                            string motivo = funciones.formatoLongitudPdt(row_dtDiasSubsi["motivo"].ToString(), 2);
                            string citt = funciones.formatoLongitudPdt(row_dtDiasSubsi["citt"].ToString(), 16);
                            string fechaini = funciones.formatoLongitudPdt(row_dtDiasSubsi["fechaini"].ToString(), 10);
                            string fechafin = funciones.formatoLongitudPdt(row_dtDiasSubsi["fechafin"].ToString(), 10);
                            OpeIO opeIO = new OpeIO(filename);
                            opeIO.WriteNWL(tipdoc + "|" + nrodoc + "|" + motivo + "|" + fechaini + "|" + fechafin + "|");
                        }

                    }


                    /////////////////////////////////////////////////////////////////////////////////////////////////////////////
                    //////////////////////DATOS PRINCIPALES DE TRABAJADOR , PENSIONISTA//////////////////////////////////////////
                    /////////////////////////////////////////////////////////////////////////////////////////////////////////////
                    if (chkTrabPen.Checked)
                    {
                        query = "select tipdoc,nrodoc,apepat,apemat,nombres,fecnac,sexo,nacion,telfijo,email,domicilia,esvida, ";
                        query = query + " tipvia,nomvia,intvia,tipzona,nomzona,refzona,ubigeo,nrovia from perplan where rucemp='" + @ruc + "' ";
                        query = query + " and idtipoper='" + @idtipoper + "' and idcia='" + @idcia + "'";

                        SqlDataAdapter da = new SqlDataAdapter();
                        da.SelectCommand = new SqlCommand(query, conexion);
                        da.Fill(dtTrabPen);

                        string filename = rutaFile + "/" + ruc + ".t00";
                        StreamWriter archivo = File.CreateText(filename);
                        archivo.Close();
                        foreach (DataRow row_dtTrabPen in dtTrabPen.Rows)
                        {

                            string tipdoc = funciones.formatoLongitudPdt(row_dtTrabPen["tipdoc"].ToString(), 2);
                            string nrodoc = funciones.formatoLongitudPdt(row_dtTrabPen["nrodoc"].ToString(), 15);
                            string apepat = funciones.formatoLongitudPdt(row_dtTrabPen["apepat"].ToString(), 40);
                            string apemat = funciones.formatoLongitudPdt(row_dtTrabPen["apemat"].ToString(), 40);
                            string nombres = funciones.formatoLongitudPdt(row_dtTrabPen["nombres"].ToString(), 40);
                            string fecnac = funciones.formatoLongitudPdt(row_dtTrabPen["fecnac"].ToString(), 10);
                            string sexo;
                            if (row_dtTrabPen["sexo"].ToString().Equals("M"))
                            {
                                sexo = "1";
                            }
                            else
                            {
                                sexo = "2";
                            }
                            string nacion = funciones.formatoLongitudPdt(row_dtTrabPen["nacion"].ToString(), 4);
                            string telfijo = funciones.formatoLongitudPdt(row_dtTrabPen["telfijo"].ToString(), 10);
                            string email = funciones.formatoLongitudPdt(row_dtTrabPen["email"].ToString(), 50);
                            string essaludvida = funciones.formatoLongitudPdt(row_dtTrabPen["esvida"].ToString(), 1);
                            string domiciliado = funciones.formatoLongitudPdt(row_dtTrabPen["domicilia"].ToString(), 1);

                            string tipvia;
                            string nomvia;
                            string nrovia;
                            string intvia;
                            string tipzona;
                            string nomzona;
                            string refzona;
                            string ubigeo;

                            if (tipdoc.Equals("01"))
                            {

                                tipvia = funciones.formatoLongitudPdt("", 2);
                                nomvia = funciones.formatoLongitudPdt("", 20);
                                nrovia = funciones.formatoLongitudPdt("", 4);
                                intvia = funciones.formatoLongitudPdt("", 4);
                                tipzona = funciones.formatoLongitudPdt("".ToString(), 2);
                                nomzona = funciones.formatoLongitudPdt("", 20);
                                refzona = funciones.formatoLongitudPdt("", 40);
                                ubigeo = funciones.formatoLongitudPdt("", 6);

                            }
                            else
                            {
                                tipvia = funciones.formatoLongitudPdt(row_dtTrabPen["tipvia"].ToString(), 2);
                                nomvia = funciones.formatoLongitudPdt(row_dtTrabPen["nomvia"].ToString(), 20);
                                nrovia = funciones.formatoLongitudPdt(row_dtTrabPen["nrovia"].ToString(), 4);
                                intvia = funciones.formatoLongitudPdt(row_dtTrabPen["intvia"].ToString(), 4);
                                tipzona = funciones.formatoLongitudPdt(row_dtTrabPen["tipzona"].ToString(), 2);
                                nomzona = funciones.formatoLongitudPdt(row_dtTrabPen["nomzona"].ToString(), 20);
                                refzona = funciones.formatoLongitudPdt(row_dtTrabPen["refzona"].ToString(), 40);
                                ubigeo = funciones.formatoLongitudPdt(row_dtTrabPen["ubigeo"].ToString(), 6);
                            }

                            OpeIO opeIO = new OpeIO(filename);
                            opeIO.WriteNWL(tipdoc + "|" + nrodoc + "|" + apepat + "|" + apemat + "|" + nombres + "|" + fecnac + "|" + sexo + "|" + nacion + "|" + telfijo + "|" + email + "|" + essaludvida + "|" + domiciliado + "|" + tipvia + "|" + nomvia + "|" + nrovia + "|" + intvia + "|" + tipzona + "|" + nomzona + "|" + refzona + "|" + ubigeo + "|");
                        }

                    }


                    /////////////////////////////////////////////////////////////////////////////////////////////////////////////
                    ///////////////////////////////////////////DATOS DEL TRABAJADOR///////////////////////////////////////////////
                    /////////////////////////////////////////////////////////////////////////////////////////////////////////////
                    if (chkDatosTrabajador.Checked)
                    {
                        query = " select A.tipdoc,A.nrodoc,A.tipotrab,B.reglab,A.nivedu,A.ocurpts,A.discapa,A.regpen,A.fecregpen, ";
                        query = query + " A.cuspp,A.sctrsanin,A.sctrsaessa,A.sctrsaeps,A.sctrpennin,A.sctrpenonp,A.sctrpenseg,A.contrab,";
                        query = query + " A.regalterna,A.trabmax,A.trabnoc,A.quicat,A.sindica,A.pering,A.afiliaeps,A.eps,A.situatrab,A.renexo,";
                        query = query + " A.sitesp,A.tippag,A.asepen,C.pdt as catocu,apliconve from perplan A inner join ciafile B ";
                        query = query + " on A.idcia=B.idcia inner join tipoper C on A.idtipoper=C.idtipoper";
                        query = query + " where rucemp='" + @ruc + "' and A.idtipoper='" + @idtipoper + "' and A.idcia='" + @idcia + "'";

                        SqlDataAdapter da = new SqlDataAdapter();
                        da.SelectCommand = new SqlCommand(query, conexion);
                        da.Fill(dtDatosTrab);

                        string filename = rutaFile + "/" + ruc + ".t01";
                        StreamWriter archivo = File.CreateText(filename);
                        archivo.Close();
                        foreach (DataRow row_dtDatosTrab in dtDatosTrab.Rows)
                        {

                            string tipdoc = funciones.formatoLongitudPdt(row_dtDatosTrab["tipdoc"].ToString(), 2);
                            string nrodoc = funciones.formatoLongitudPdt(row_dtDatosTrab["nrodoc"].ToString(), 15);
                            string tipotrab = funciones.formatoLongitudPdt(row_dtDatosTrab["tipotrab"].ToString(), 2);
                            string reglab = funciones.formatoLongitudPdt(row_dtDatosTrab["reglab"].ToString(), 1);
                            string nivedu = funciones.formatoLongitudPdt(row_dtDatosTrab["nivedu"].ToString(), 2);
                            string ocurpts = funciones.formatoLongitudPdt(row_dtDatosTrab["ocurpts"].ToString(), 6);
                            string discapa = funciones.formatoLongitudPdt(row_dtDatosTrab["discapa"].ToString(), 1);
                            string regpen = funciones.formatoLongitudPdt(row_dtDatosTrab["regpen"].ToString(), 2);
                            string fecregpen = funciones.formatoLongitudPdt(row_dtDatosTrab["fecregpen"].ToString(), 10);
                            string cuspp = funciones.formatoLongitudPdt(row_dtDatosTrab["cuspp"].ToString(), 12);
                            string sctrsalud = "0";
                            if (row_dtDatosTrab["sctrsanin"].ToString().Equals("1"))
                            {
                                sctrsalud = "0";
                            }
                            else
                            {
                                if (row_dtDatosTrab["sctrsaessa"].ToString().Equals("1"))
                                {
                                    sctrsalud = "1";
                                }
                                else
                                {
                                    if (row_dtDatosTrab["sctrsaeps"].ToString().Equals("1"))
                                    {
                                        sctrsalud = "2";
                                    }
                                }
                            }
                            sctrsalud = funciones.formatoLongitudPdt(sctrsalud, 1);

                            string sctrpension = "0";

                            if (row_dtDatosTrab["sctrpennin"].ToString().Equals("1"))
                            {
                                sctrpension = "0";
                            }
                            else
                            {
                                if (row_dtDatosTrab["sctrpenonp"].ToString().Equals("1"))
                                {
                                    sctrpension = "1";
                                }
                                else
                                {
                                    if (row_dtDatosTrab["sctrpenseg"].ToString().Equals("1"))
                                    {
                                        sctrpension = "2";
                                    }
                                }
                            }

                            sctrpension = funciones.formatoLongitudPdt(sctrpension, 1);
                            string contrab = funciones.formatoLongitudPdt(row_dtDatosTrab["contrab"].ToString(), 2);
                            string regalterna = funciones.formatoLongitudPdt(row_dtDatosTrab["regalterna"].ToString(), 1);
                            string trabmax = funciones.formatoLongitudPdt(row_dtDatosTrab["trabmax"].ToString(), 1);
                            string trabnoc = funciones.formatoLongitudPdt(row_dtDatosTrab["trabnoc"].ToString(), 1);
                            string quicat = funciones.formatoLongitudPdt(row_dtDatosTrab["quicat"].ToString(), 1);
                            string sindica = funciones.formatoLongitudPdt(row_dtDatosTrab["sindica"].ToString(), 1);
                            string pering = funciones.formatoLongitudPdt(row_dtDatosTrab["pering"].ToString(), 1);
                            string afiliaeps = funciones.formatoLongitudPdt(row_dtDatosTrab["afiliaeps"].ToString(), 1);
                            string eps = funciones.formatoLongitudPdt(row_dtDatosTrab["eps"].ToString(), 1);
                            string situatrab = funciones.formatoLongitudPdt(row_dtDatosTrab["situatrab"].ToString(), 2);
                            string renexo = funciones.formatoLongitudPdt(row_dtDatosTrab["renexo"].ToString(), 1);
                            string sitesp = funciones.formatoLongitudPdt(row_dtDatosTrab["sitesp"].ToString(), 1);
                            string tippag = funciones.formatoLongitudPdt(row_dtDatosTrab["tippag"].ToString(), 1);
                            string asepen = funciones.formatoLongitudPdt(row_dtDatosTrab["asepen"].ToString(), 1);
                            string catocu = funciones.formatoLongitudPdt(row_dtDatosTrab["catocu"].ToString(), 1);
                            string apliconve = funciones.formatoLongitudPdt(row_dtDatosTrab["apliconve"].ToString(), 1);

                            OpeIO opeIO = new OpeIO(filename);
                            opeIO.WriteNWL(tipdoc + "|" + nrodoc + "|" + tipotrab + "|" + reglab + "|" + nivedu + "|" + ocurpts + "|" + discapa + "|" + regpen + "|" + fecregpen + "|" + cuspp + "|" + sctrsalud + "|" + sctrpension + "|" + contrab + "|" + regalterna + "|" + trabmax + "|" + trabnoc + "|" + quicat + "|" + sindica + "|" + pering + "|" + afiliaeps + "|" + eps + "|" + situatrab + "|" + renexo + "|" + sitesp + "|" + tippag + "|" + asepen + "|" + catocu + "|" + apliconve + "|");
                        }

                    }

                    /////////////////////////////////////////////////////////////////////////////////////////////////////////////
                    //////////////////////DATOS DERECHOHABIENTES//////////////////////////////////////////
                    /////////////////////////////////////////////////////////////////////////////////////////////////////////////
                    if (chkDerHab.Checked)
                    {
                        query = " select B.tipdoc as tipdoctra,B.nrodoc as nrodoctra,A.tipdoc,A.nrodoc, ";
                        query = query + " A.apepat,A.apemat,A.nombres,A.fecnac,A.sexo,A.vinfam, A.docpat,A.sitdh,";
                        query = query + " A.tipvia,A.nomvia,A.intvia,A.tipzona,A.nomzona,A.refzona,A.ubigeo,A.nrovia,";
                        query = query + " A.fecalta,A.motbaja,A.fecbaja,A.rddisca,A.domtra ";
                        query = query + " from derhab A inner join perplan B on A.idcia=B.idcia and A.idperplan=B.idperplan";
                        query = query + " where A.idcia='" + @idcia + "'; ";

                        SqlDataAdapter da = new SqlDataAdapter();
                        da.SelectCommand = new SqlCommand(query, conexion);
                        da.Fill(dtDerHab);

                        string filename = rutaFile + "/" + ruc + ".der";
                        StreamWriter archivo = File.CreateText(filename);
                        archivo.Close();
                        foreach (DataRow row_dtDerHab in dtDerHab.Rows)
                        {
                            string tipdoctra = funciones.formatoLongitudPdt(row_dtDerHab["tipdoctra"].ToString(), 2);
                            string nrodoctra = funciones.formatoLongitudPdt(row_dtDerHab["nrodoctra"].ToString(), 15);
                            string tipdoc = funciones.formatoLongitudPdt(row_dtDerHab["tipdoc"].ToString(), 2);
                            string nrodoc = funciones.formatoLongitudPdt(row_dtDerHab["nrodoc"].ToString(), 15);
                            string apepat = funciones.formatoLongitudPdt(row_dtDerHab["apepat"].ToString(), 40);
                            string apemat = funciones.formatoLongitudPdt(row_dtDerHab["apemat"].ToString(), 40);
                            string nombres = funciones.formatoLongitudPdt(row_dtDerHab["nombres"].ToString(), 40);
                            string fecnac = funciones.formatoLongitudPdt(row_dtDerHab["fecnac"].ToString(), 10);
                            string sexo;
                            if (row_dtDerHab["sexo"].ToString().Equals("M"))
                            {
                                sexo = "1";
                            }
                            else
                            {
                                sexo = "2";
                            }
                            string vinfam = funciones.formatoLongitudPdt(row_dtDerHab["vinfam"].ToString(), 1);
                            string docpat = funciones.formatoLongitudPdt(row_dtDerHab["docpat"].ToString(), 1);
                            string docacrepat = funciones.formatoLongitudPdt("", 20);
                            string sitdh = funciones.formatoLongitudPdt(row_dtDerHab["sitdh"].ToString(), 2);
                            string fecalta = funciones.formatoLongitudPdt(row_dtDerHab["fecalta"].ToString(), 10);
                            string motbaja = funciones.formatoLongitudPdt(row_dtDerHab["motbaja"].ToString(), 1);
                            string fecbaja = funciones.formatoLongitudPdt(row_dtDerHab["fecbaja"].ToString(), 10);
                            string rddisca = funciones.formatoLongitudPdt(row_dtDerHab["rddisca"].ToString(), 20);
                            string domtra;
                            if (row_dtDerHab["domtra"].ToString().Equals("1"))
                            {
                                domtra = "0";
                            }
                            else
                            {
                                domtra = "1";
                            }
                            string tipvia = funciones.formatoLongitudPdt(row_dtDerHab["tipvia"].ToString(), 2);
                            string nomvia = funciones.formatoLongitudPdt(row_dtDerHab["nomvia"].ToString(), 20);
                            string nrovia = funciones.formatoLongitudPdt(row_dtDerHab["nrovia"].ToString(), 4);
                            string intvia = funciones.formatoLongitudPdt(row_dtDerHab["intvia"].ToString(), 4);
                            string tipzona = funciones.formatoLongitudPdt(row_dtDerHab["tipzona"].ToString(), 2);
                            string nomzona = funciones.formatoLongitudPdt(row_dtDerHab["nomzona"].ToString(), 20);
                            string refzona = funciones.formatoLongitudPdt(row_dtDerHab["refzona"].ToString(), 40);
                            string ubigeo = funciones.formatoLongitudPdt(row_dtDerHab["ubigeo"].ToString(), 6);

                            OpeIO opeIO = new OpeIO(filename);
                            opeIO.WriteNWL(tipdoctra + "|" + nrodoctra + "|" + tipdoc + "|" + nrodoc +
                                "|" + apepat + "|" + apemat + "|" + nombres + "|" + fecnac + "|" + sexo + "|" +
                                vinfam + "|" + docpat + "|" + docacrepat + "|" + sitdh + "|" + fecalta + "|" +
                                motbaja + "|" + fecbaja + "|" + rddisca + "|" + domtra + "|" + tipvia + "|" +
                                nomvia + "|" + nrovia + "|" + intvia + "|" + tipzona + "|" + nomzona +
                                "|" + refzona + "|" + ubigeo + "|");
                        }

                    }


                    /////////////////////////////////////////////////////////////////////////////////////////////////////////////
                    //////////////////////DATOS DE PERIODOS//////////////////////////////////////////////////////////////////////
                    /////////////////////////////////////////////////////////////////////////////////////////////////////////////
                    if (chkPeriodos.Checked)
                    {
                        query = " select B.tipdoc ,B.nrodoc ,'1' as categoria,";
                        query = query + " A.fechaini,A.fechafin,A.motivo,'' as tipmodfor";
                        query = query + " from perlab A inner join perplan B on A.idcia=B.idcia and A.idperplan=B.idperplan";
                        query = query + " where A.idcia='" + @idcia + "' and B.idtipoper='" + @idtipoper + "'; ";

                        SqlDataAdapter da = new SqlDataAdapter();
                        da.SelectCommand = new SqlCommand(query, conexion);
                        da.Fill(dtPeriodos);

                        string filename = rutaFile + "/" + ruc + ".p00";
                        StreamWriter archivo = File.CreateText(filename);
                        archivo.Close();
                        foreach (DataRow row_dtPeriodos in dtPeriodos.Rows)
                        {
                            string tipdoc = funciones.formatoLongitudPdt(row_dtPeriodos["tipdoc"].ToString(), 2);
                            string nrodoc = funciones.formatoLongitudPdt(row_dtPeriodos["nrodoc"].ToString(), 15);
                            string categoria = funciones.formatoLongitudPdt(row_dtPeriodos["categoria"].ToString(), 1);
                            string fechaini = funciones.formatoLongitudPdt(row_dtPeriodos["fechaini"].ToString(), 10);
                            string fechafin = funciones.formatoLongitudPdt(row_dtPeriodos["fechafin"].ToString(), 10);
                            string motivo = funciones.formatoLongitudPdt(row_dtPeriodos["motivo"].ToString(), 2);
                            string tipmodfor = funciones.formatoLongitudPdt(row_dtPeriodos["tipmodfor"].ToString(), 2);

                            OpeIO opeIO = new OpeIO(filename);
                            opeIO.WriteNWL(tipdoc + "|" + nrodoc + "|" + categoria + "|" + fechaini +
                                "|" + fechafin + "|" + motivo + "|" + tipmodfor + "|");
                        }

                    }

                    /////////////////////////////////////////////////////////////////////////////////////////////////////////////
                    //////////////////////DATOS DE LA JORNADA LABORAL DEL TRABAJADOR/////////////////////////////////////////////
                    /////////////////////////////////////////////////////////////////////////////////////////////////////////////

                    if (chkJorTrab.Checked)
                    {
                        try
                        {
                            int numhext;
                            query = " select A.idperplan,C.tipdoc,C.nrodoc,SUM(A.diasefelab) as diasefelab,ROUND(SUM(hext25+hext35+hext100)) as hext ";
                            query = query + "  from dataplan A ";
                            query = query + "  inner join perplan C on A.idperplan=C.idperplan and A.idcia=C.idcia ";
                            query = query + "  inner join calplan E on A.idtipocal=E.idtipocal and A.idcia=E.idcia ";
                            query = query + "  and A.messem=E.messem and A.anio=E.anio and A.idtipoper=E.idtipoper ";
                            query = query + "  where E.aniopdt='" + @aniopdt + "' and E.mespdt='" + @mespdt + "'  ";
                            query = query + "  and A.idcia='" + @idcia + "' and A.idtipoper='" + @idtipoper + "' ";
                            query = query + "  and A.idtipoplan='" + @idtipoplan + "' ";
                            query = query + "  group by A.idperplan,C.tipdoc,C.nrodoc ;";

                            SqlDataAdapter da = new SqlDataAdapter();
                            da.SelectCommand = new SqlCommand(query, conexion);
                            da.Fill(dtJorTrab);

                            string filename = rutaFile + "/" + "0601" + aniopdt + mespdt + ruc + ".jor";

                            StreamWriter archivo = File.CreateText(filename);
                            archivo.Close();
                            foreach (DataRow row_dtJorTrab in dtJorTrab.Rows)
                            {
                                numhext = Int16.Parse(row_dtJorTrab["hext"].ToString());

                                string tipdoc = funciones.formatoLongitudPdt(row_dtJorTrab["tipdoc"].ToString(), 2);
                                string nrodoc = funciones.formatoLongitudPdt(row_dtJorTrab["nrodoc"].ToString(), 15);
                                string nhordtrab = funciones.formatoLongitudPdt((Int16.Parse(row_dtJorTrab["diasefelab"].ToString()) * 8).ToString(), 3);
                                string nminordtrab = funciones.formatoLongitudPdt("0", 2);
                                string nhorsobtrab = funciones.formatoLongitudPdt(numhext.ToString(), 3);
                                string nminsobtrab = funciones.formatoLongitudPdt("0", 2);

                                OpeIO opeIO = new OpeIO(filename);
                                opeIO.WriteNWL(tipdoc + "|" + nrodoc + "|" + nhordtrab + "|" + nminordtrab + "|" + nhorsobtrab + "|" + nminsobtrab + "|");
                            }
                        }
                        catch (Exception e)
                        {
                            MessageBox.Show(e.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }

                    /////////////////////////////////////////////////////////////////////////////////////////////////////////////
                    //////////////////////DATOS DE EMPRESAS A QUIEN DESTADO O DESPLAZO PERSONAL//////////////////////////////////
                    /////////////////////////////////////////////////////////////////////////////////////////////////////////////
                    if (chkEmpDestacoPer.Checked)
                    {
                        string filename;
                        //RAZON SOCIAL Y ACTIVIDAD ECONOMICA
                        query = "select rucemp,razonemp,actividad from emplea ";
                        query = query + " where idciafile='" + @idcia + "'; ";
                        SqlDataAdapter da = new SqlDataAdapter();
                        da.SelectCommand = new SqlCommand(query, conexion);
                        da.Fill(dtEmpDestacoPer);

                        filename = rutaFile + "/" + ruc + ".edd";
                        StreamWriter archivo = File.CreateText(filename);
                        archivo.Close();
                        foreach (DataRow row_dtEmpDestacoPer in dtEmpDestacoPer.Rows)
                        {
                            string rucemp = funciones.formatoLongitudPdt(row_dtEmpDestacoPer["rucemp"].ToString(), 11);
                            string razonemp = funciones.formatoLongitudPdt(row_dtEmpDestacoPer["razonemp"].ToString(), 100);
                            string actividad = funciones.formatoLongitudPdt(row_dtEmpDestacoPer["actividad"].ToString(), 6);

                            OpeIO opeIO = new OpeIO(filename);
                            opeIO.WriteNWL(rucemp + "|" + razonemp + "|" + actividad);
                        }

                        //DENOMINACIONES DE ESTABLECIMIENTOS Y CENTROS DE RIESGO

                        query = "select  A.codemp,A.desestane,A.riesgo,B.tasa ";
                        query = query + " from estane A inner join tasaest B on ";
                        query = query + " A.idestane=B.idestane and A.codemp=B.codemp ";
                        query = query + " where A.idciafile='" + @idcia + "' and A.codemp<>'" + @ruc + "'; ";

                        SqlDataAdapter da_est = new SqlDataAdapter();
                        da_est.SelectCommand = new SqlCommand(query, conexion);
                        da_est.Fill(dtEstCenRiesgo);

                        filename = rutaFile + "/" + ruc + ".sdd";
                        StreamWriter archivo_est = File.CreateText(filename);
                        archivo_est.Close();
                        foreach (DataRow row_dtEstCenRiesgo in dtEstCenRiesgo.Rows)
                        {
                            string codemp = funciones.formatoLongitudPdt(row_dtEstCenRiesgo["codemp"].ToString(), 11);
                            string desestane = funciones.formatoLongitudPdt(row_dtEstCenRiesgo["desestane"].ToString(), 40);
                            string riesgo = funciones.formatoLongitudPdt(row_dtEstCenRiesgo["riesgo"].ToString(), 1);
                            string tasa = funciones.formatoLongitudPdt(row_dtEstCenRiesgo["tasa"].ToString(), 5);

                            OpeIO opeIO = new OpeIO(filename);
                            opeIO.WriteNWL(codemp + "|" + desestane + "|" + riesgo + "|" + tasa + "|");
                        }

                    }

                }
                conexion.Close();
                MessageBox.Show("Exportación SUNAT Finalizada", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }

        private void cmbTipoTrabajador_SelectedIndexChanged(object sender, EventArgs e)
        {


        }

        private void cmbTipoPlan_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void lblUsuario_Click(object sender, EventArgs e)
        {

        }

        private void txtMesSem_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void chkEstPropios_CheckedChanged(object sender, EventArgs e)
        {


        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
    }
}