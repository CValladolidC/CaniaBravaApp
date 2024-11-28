using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Configuration;
using System.Data.SqlClient;

namespace CaniaBrava
{
    public partial class ui_CalculaPlanPersonal : Form
    {
        private TextBox TextBoxActivo;
        private Form FormPadre;

        public Form _FormPadre
        {
            get { return FormPadre; }
            set { FormPadre = value; }
        }

        public TextBox _TextBoxActivo
        {
            get { return TextBoxActivo; }
            set { TextBoxActivo = value; }
        }

        public ui_CalculaPlanPersonal()
        {
            InitializeComponent();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ui_listaTipoCal(string idcia, string idtipoplan)
        {
            Funciones funciones = new Funciones();
            string query;
            query = " SELECT idtipocal as clave,destipocal as descripcion ";
            query = query + " FROM tipocal where idtipocal in (select idtipocal ";
            query = query + " from calcia where idcia='" + @idcia + "' and idtipoplan='" + @idtipoplan + "') ";
            query = query + " order by ordentipocal asc;";
            funciones.listaComboBox(query, cmbTipoCal, "");
        }

        private void ui_CalculaPlanPersonal_Load(object sender, EventArgs e)
        {

            Funciones funciones = new Funciones();
            GlobalVariables variables = new GlobalVariables();
            string idcia = variables.getValorCia();
            string squery = "SELECT idtipoper as clave,destipoper as descripcion FROM tipoper order by 1 asc";
            funciones.listaComboBox(squery, cmbTipoTrabajador, "");
            squery = "SELECT idtipoplan as clave,destipoplan as descripcion FROM tipoplan where idtipoplan in (select idtipoplan from reglabcia where idcia='" + @idcia + "') order by 1 asc;";
            funciones.listaComboBox(squery, cmbTipoPlan, "");
            string idtipoplan = funciones.getValorComboBox(cmbTipoPlan, 3);
            ui_listaTipoCal(idcia, idtipoplan);

        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            Funciones funciones = new Funciones();
            GlobalVariables globalVariable = new GlobalVariables();
            PerPlan perplan = new PerPlan();
            string idcia = globalVariable.getValorCia();
            string idtipoper = funciones.getValorComboBox(cmbTipoTrabajador, 1);
            string idtipoplan = funciones.getValorComboBox(cmbTipoPlan, 3);
            string periodo = txtMesSem.Text.Trim() + funciones.replicateCadena(" ", 6);
            string messem = periodo.Substring(0, 2);
            string anio = periodo.Substring(3, 4);
            string idtipocal = funciones.getValorComboBox(cmbTipoCal, 1);
            string idperplan = txtCodigoInterno.Text.Trim();
            string query;
            string resultado = "0";


            CalPlan calplan = new CalPlan();

            if (calplan.getDatosCalPlan(messem, anio, idtipoper, idcia, idtipocal, "FECHAINI") != "")
            {
                txtFechaIni.Text = calplan.getDatosCalPlan(messem, anio, idtipoper, idcia, idtipocal, "FECHAINI");
                txtFechaFin.Text = calplan.getDatosCalPlan(messem, anio, idtipoper, idcia, idtipocal, "FECHAFIN");
                if (calplan.getDatosCalPlan(messem, anio, idtipoper, idcia, idtipocal, "ESTADO") != "C")
                {
                    SqlConnection conexion = new SqlConnection();
                    conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
                    conexion.Open();
                    try
                    {

                        lblprocesando.Visible = true;

                        resultado = perplan.ui_verificaPerPlan(idcia, idperplan);

                        if (resultado.Equals("0"))
                        {

                            var opc = MessageBox.Show("¿Realmente desea proceder a calcular la planilla?", "Aviso", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                            if (opc == DialogResult.Yes)
                            {
                                string diasperlab = calplan.getDatosCalPlan(messem, anio, idtipoper, idcia, idtipocal, "DIAS_FI_FF");

                                ///////////////////////////////////////////////
                                ////////////////PARTE DE PLANILLA//////////////
                                ///////////////////////////////////////////////

                                DataTable dataplan = new DataTable();
                                SqlDataAdapter da_dataplan = new SqlDataAdapter();
                                query = "select A.idcia,A.idperplan,A.diasefelab,A.diassubsi,A.diasnosubsi,A.diastotal,A.hext25, ";
                                query = query + " A.hext35,A.hext100,A.diasvac,A.candes,A.impdes,A.diurno,A.nocturno,B.mes, ";
                                query = query + " B.fechaini,B.fechafin,A.diasdom,A.emplea, A.estane,A.gratifica from dataplan A  ";
                                query = query + " inner join calplan B on A.idtipocal=B.idtipocal and A.idcia=B.idcia ";
                                query = query + " and A.messem=B.messem and A.anio=B.anio and A.idtipoper=B.idtipoper ";
                                query = query + " where A.idcia='" + @idcia + "' and A.idtipocal='" + @idtipocal + "' ";
                                query = query + " and A.idtipoper='" + @idtipoper + "' and A.messem='" + @messem + "' ";
                                query = query + " and A.anio='" + @anio + "' and A.idperplan='" + @idperplan + "' ";
                                query = query + " and A.idtipoplan='" + @idtipoplan + "'  ;";
                                da_dataplan.SelectCommand = new SqlCommand(query, conexion);
                                da_dataplan.Fill(dataplan);

                                ////////////////////////////////////////////////
                                ///////////ELIMINA DATOS DE PLANILLA////////////
                                ////////////////////////////////////////////////
                                Plan plan = new Plan();
                                plan.eliminarPlanPersonal(idcia, anio, messem, idtipoper, idtipocal, idtipoplan, idperplan);
                                ConBol conbol = new ConBol();
                                conbol.eliminarConBol(idperplan, idcia, anio, messem, idtipoper, idtipocal, idtipoplan);
                                ProcPlan procplan = new ProcPlan();
                                procplan.eliminarProcPlanPersona(idcia, idperplan, idtipoper, idtipoplan, idtipocal, anio, messem);

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
                                query = "call calculo_personal ";
                                da_concons.SelectCommand = new SqlCommand(query, conexion);
                                da_concons.Fill(concons);

                                foreach (DataRow row_dataplan in dataplan.Rows)
                                {

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
                                    string rucemp = row_dataplan["emplea"].ToString();
                                    string estane = row_dataplan["estane"].ToString();
                                    string gratifica = row_dataplan["gratifica"].ToString();

                                    ProcessVariables procesa = new ProcessVariables();
                                    procesa.procesaVariablesPlanilla(concons, idcia, idperplan, hext25, hext35,
                                    hext100, defla, dnlns, diassub, idtipoplan, idtipoper, anio, messem, idtipocal,
                                    rucemp, estane, candes, impdes, diasvac, diurno, nocturno, mes, diasperlab,
                                    diasdom, gratifica);

                                }

                                MessageBox.Show("Proceso Finalizado", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }

                        }
                        lblprocesando.Visible = false;
                    }

                    catch (Exception) { }
                    conexion.Close();
                }
                else
                {
                    MessageBox.Show("El Periodo Laboral ya se encuentra cerrado", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
            else
            {
                MessageBox.Show("El periodo ingresado no existe en el Calendario de Planilla", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtFechaIni.Clear();
                txtFechaFin.Clear();
                txtMesSem.Focus();
            }

        }

        private void txtCodigoInterno_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                if (txtCodigoInterno.Text.Trim() != string.Empty)
                {
                    GlobalVariables gv = new GlobalVariables();
                    PerPlan perplan = new PerPlan();
                    string idcia = gv.getValorCia();
                    string idperplan = txtCodigoInterno.Text.Trim();
                    string codigoInterno = perplan.ui_getDatosPerPlan(idcia, idperplan, "0");

                    if (codigoInterno == string.Empty)
                    {
                        MessageBox.Show("Código no registrado del trabajador", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txtCodigoInterno.Clear();
                        txtDocIdent.Clear();
                        txtNroDocIden.Clear();
                        txtNombres.Clear();
                        txtFecIniPerLab.Clear();
                        txtFecFinPerLab.Clear();
                        e.Handled = true;
                        txtCodigoInterno.Focus();

                    }
                    else
                    {
                        txtCodigoInterno.Text = perplan.ui_getDatosPerPlan(idcia, idperplan, "0");
                        txtDocIdent.Text = perplan.ui_getDatosPerPlan(idcia, idperplan, "1");
                        txtNroDocIden.Text = perplan.ui_getDatosPerPlan(idcia, idperplan, "2");
                        txtNombres.Text = perplan.ui_getDatosPerPlan(idcia, idperplan, "3");
                        txtFecIniPerLab.Text = perplan.ui_getDatosPerPlan(idcia, idperplan, "4");
                        txtFecFinPerLab.Text = perplan.ui_getDatosPerPlan(idcia, idperplan, "8");
                        e.Handled = true;
                        toolstripform.Items[0].Select();
                        toolstripform.Focus();

                    }
                }
                else
                {
                    txtCodigoInterno.Clear();
                    txtDocIdent.Clear();
                    txtNroDocIden.Clear();
                    txtNombres.Clear();
                    txtFecIniPerLab.Clear();
                    txtFecFinPerLab.Clear();
                    e.Handled = true;
                    txtCodigoInterno.Focus();

                }
            }
        }

        private void txtCodigoInterno_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                GlobalVariables gv = new GlobalVariables();
                Funciones fn = new Funciones();
                string idcia = gv.getValorCia();
                string idtipoper = fn.getValorComboBox(cmbTipoTrabajador, 1);
                string idtipoplan = fn.getValorComboBox(cmbTipoPlan, 3);
                string periodo = txtMesSem.Text.Trim() + fn.replicateCadena(" ", 6);
                string messem = periodo.Substring(0, 2);
                string anio = periodo.Substring(3, 4);
                string idtipocal = fn.getValorComboBox(cmbTipoCal, 1);
                this._TextBoxActivo = txtCodigoInterno;
                string cadenaBusqueda = string.Empty;

                if (anio.Trim() == string.Empty)
                {
                    MessageBox.Show("No ha especificado Periodo Laboral", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtMesSem.Focus();

                }
                else
                {

                    string condicionAdicional = " and A.idtipoplan='" + @idtipoplan + "' and CONCAT(A.idperplan,A.idcia) in (select CONCAT(idperplan,idcia) from dataplan where idtipoper='" + @idtipoper + "' and messem='" + @messem + "' and anio='" + @anio + "' and idtipocal='" + @idtipocal + "' and idcia='" + @idcia + "' and idtipoplan='" + @idtipoplan + "' ) ";
                    FiltrosMaestros filtros = new FiltrosMaestros();
                    filtros.filtrarPerPlan("ui_CalculaPlanPersonal", this, txtCodigoInterno, idcia, idtipoper, "", cadenaBusqueda, condicionAdicional);
                }
            }
        }

        private void txtMesSem_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                Funciones funciones = new Funciones();
                CalPlan calplan = new CalPlan();
                GlobalVariables globalVariable = new GlobalVariables();
                string idcia = globalVariable.getValorCia();
                string idtipoper = funciones.getValorComboBox(cmbTipoTrabajador, 1);
                string periodo = txtMesSem.Text.Trim() + funciones.replicateCadena(" ", 6);
                string messem = periodo.Substring(0, 2);
                string anio = periodo.Substring(3, 4);
                string idtipocal = funciones.getValorComboBox(cmbTipoCal, 1);

                if (calplan.getDatosCalPlan(messem, anio, idtipoper, idcia, idtipocal, "FECHAINI") != "")
                {
                    txtFechaIni.Text = calplan.getDatosCalPlan(messem, anio, idtipoper, idcia, idtipocal, "FECHAINI");
                    txtFechaFin.Text = calplan.getDatosCalPlan(messem, anio, idtipoper, idcia, idtipocal, "FECHAFIN");
                    lblPeriodo.Visible = true;

                    if (calplan.getDatosCalPlan(messem, anio, idtipoper, idcia, idtipocal, "ESTADO") == "V")
                    {
                        lblPeriodo.Text = "Periodo Laboral Vigente";

                    }
                    else
                    {
                        lblPeriodo.Text = "Periodo Laboral Cerrado";

                    }
                    e.Handled = true;
                    txtCodigoInterno.Focus();
                }
                else
                {
                    MessageBox.Show("El Periodo Laboral no existe", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    lblPeriodo.Visible = false;
                    txtFechaIni.Clear();
                    txtFechaFin.Clear();
                    e.Handled = true;
                    txtMesSem.Focus();
                }


            }
        }

        private void cmbTipoPlan_SelectedIndexChanged(object sender, EventArgs e)
        {
            Funciones funciones = new Funciones();
            GlobalVariables variables = new GlobalVariables();
            string idcia = variables.getValorCia();
            string idtipoplan = funciones.getValorComboBox(cmbTipoPlan, 3);
            ui_listaTipoCal(idcia, idtipoplan);
            txtMesSem.Clear();
            txtFechaIni.Clear();
            txtFechaFin.Clear();
            txtMesSem.Focus();
            lblPeriodo.Visible = false;
        }

        private void cmbTipoTrabajador_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtMesSem.Clear();
            txtFechaIni.Clear();
            txtFechaFin.Clear();
            txtMesSem.Focus();
            lblPeriodo.Visible = false;
        }

        private void cmbTipoCal_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtMesSem.Clear();
            txtFechaIni.Clear();
            txtFechaFin.Clear();
            txtMesSem.Focus();
            lblPeriodo.Visible = false;
        }

        private void txtCodigoInterno_TextChanged(object sender, EventArgs e)
        {

        }
    }
}