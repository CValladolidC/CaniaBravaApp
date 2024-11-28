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
using CrystalDecisions.CrystalReports.Engine;
using System.Drawing.Printing;

namespace CaniaBrava
{
    public partial class ui_BoletaWin : Form
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

        public ui_BoletaWin()
        {
            InitializeComponent();
        }

        private void ui_BoletaWin_Load(object sender, EventArgs e)
        {
            Funciones funciones = new Funciones();
            GlobalVariables variables = new GlobalVariables();
            string squery;
            string idcia = variables.getValorCia();
            squery = "SELECT idtipoper as clave,destipoper as descripcion FROM tipoper order by 1 asc";
            funciones.listaComboBox(squery, cmbTipoTrabajador, "");
            squery = "SELECT idtipoplan as clave,destipoplan as descripcion FROM tipoplan where idtipoplan in (select idtipoplan from reglabcia where idcia='" + @idcia + "') order by 1 asc;";
            funciones.listaComboBox(squery, cmbTipoPlan, "");
            string idtipoplan = funciones.getValorComboBox(cmbTipoPlan, 3);
            ui_listaTipoCal(idcia, idtipoplan);
            cmbTipo.Text = "CON LOGOTIPO";
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

        private void txtMesSem_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                Funciones funciones = new Funciones();
                CalPlan calplan = new CalPlan();
                GlobalVariables variables = new GlobalVariables();
                string idcia = variables.getValorCia();
                string idtipoper = funciones.getValorComboBox(cmbTipoTrabajador, 1);
                string periodo = txtMesSem.Text.Trim() + funciones.replicateCadena(" ", 6);
                string messem = periodo.Substring(0, 2);
                string anio = periodo.Substring(3, 4);
                string idtipocal = funciones.getValorComboBox(cmbTipoCal, 1);

                if (calplan.getDatosCalPlan(messem, anio, idtipoper, idcia, idtipocal, "FECHAINI") != "")
                {
                    txtFechaIni.Text = calplan.getDatosCalPlan(messem, anio, idtipoper, idcia, idtipocal, "FECHAINI");
                    txtFechaFin.Text = calplan.getDatosCalPlan(messem, anio, idtipoper, idcia, idtipocal, "FECHAFIN");

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
                    MessageBox.Show("El Periodo Laboral no existe", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtFechaIni.Clear();
                    txtFechaFin.Clear();
                    txtCodigoInterno.Clear();
                    txtDocIdent.Clear();
                    txtNroDocIden.Clear();
                    txtNombres.Clear();
                    txtFecIniPerLab.Clear();
                    txtFecFinPerLab.Clear();
                    e.Handled = true;
                    txtMesSem.Focus();
                }
            }
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
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

                    string condicionAdicional = " and A.idtipoplan='" + @idtipoplan + "' and CONCAT(A.idperplan,A.idcia) in (select CONCAT(idperplan,idcia) from plan_ where idtipoper='" + @idtipoper + "' and messem='" + @messem + "' and anio='" + @anio + "' and idtipocal='" + @idtipocal + "' and idcia='" + @idcia + "' and idtipoplan='" + @idtipoplan + "' ) ";
                    FiltrosMaestros filtros = new FiltrosMaestros();
                    filtros.filtrarPerPlan("ui_BoletaWin", this, txtCodigoInterno, idcia, idtipoper, "", cadenaBusqueda, condicionAdicional);
                }
            }
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            string valida = "G";
            Funciones fn = new Funciones();
            GlobalVariables gv = new GlobalVariables();
            CalPlan calplan = new CalPlan();
            string idtipoper = fn.getValorComboBox(cmbTipoTrabajador, 1);
            string periodo = txtMesSem.Text.Trim() + fn.replicateCadena(" ", 6);
            string messem = periodo.Substring(0, 2);
            string anio = periodo.Substring(3, 4);
            string idtipocal = fn.getValorComboBox(cmbTipoCal, 1);
            string idtipoplan = fn.getValorComboBox(cmbTipoPlan, 3);
            string idcia = gv.getValorCia();
            string idperplan = txtCodigoInterno.Text.Trim();
            string tipo = cmbTipo.Text.Trim();

            if (anio.Trim() == string.Empty)
            {
                MessageBox.Show("No ha especificado Periodo Laboral", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtMesSem.Focus();
                valida = "B";
            }

            if (idperplan.Trim() == string.Empty)
            {
                MessageBox.Show("No ha especificado trabajador", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtCodigoInterno.Focus();
                valida = "B";
            }


            if (valida.Equals("G"))
            {

                cr.crboleta_agrofrutos cra;
                cr.crboleta_agromarin crb;
                cr.crboleta_agronegocios crc;
                cr.crboleta_asepesac crd;
                cr.crboleta_citricos cre;

                lblprocesando.Visible = true;

                Boleta boleta = new Boleta();
                DataTable dtboleta = new DataTable();
                dtboleta = boleta.generaDataBoletaWin(idperplan, anio, messem, idcia, idtipoper, idtipoplan, idtipocal);

                if (dtboleta.Rows.Count > 0)
                {
                    if (tipo.Equals("SIN LOGOTIPO"))
                    {

                        ui_reporte ui_reporte = new ui_reporte();
                        if (idcia == "01")
                        {
                            cre = new cr.crboleta_citricos();
                            ui_reporte.asignaDataTable(cre, dtboleta);
                        }
                        else
                        {
                            if (idcia == "02")
                            {

                                crb = new cr.crboleta_agromarin();
                                ui_reporte.asignaDataTable(crb, dtboleta);
                            }
                            else
                            {
                                if (idcia == "03")
                                {

                                    crc = new cr.crboleta_agronegocios();
                                    ui_reporte.asignaDataTable(crc, dtboleta);
                                }
                                else
                                {
                                    if (idcia == "04")
                                    {
                                        cra = new cr.crboleta_agrofrutos();
                                        ui_reporte.asignaDataTable(cra, dtboleta);
                                    }
                                    else
                                    {
                                        crd = new cr.crboleta_asepesac();
                                        ui_reporte.asignaDataTable(crd, dtboleta);
                                    }
                                }
                            }
                        }

                        ui_reporte.Activate();
                        ui_reporte.BringToFront();
                        ui_reporte.ShowDialog();
                        ui_reporte.Dispose();





                    }
                    else
                    {
                        cr.crboletalogo cr2 = new cr.crboletalogo();
                        ui_reporte ui_reporte = new ui_reporte();
                        ui_reporte.asignaDataTable(cr2, dtboleta);
                        ui_reporte.Activate();
                        ui_reporte.BringToFront();
                        ui_reporte.ShowDialog();
                        ui_reporte.Dispose();
                    }
                }
                else
                {
                    MessageBox.Show("No existe información registrada en el criterio seleccionado", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }

                lblprocesando.Visible = false;
            }


        }

        private void cmbTipoTrabajador_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtMesSem.Clear();
            txtFechaIni.Clear();
            txtFechaFin.Clear();
            txtMesSem.Focus();
            txtCodigoInterno.Clear();
            txtDocIdent.Clear();
            txtNroDocIden.Clear();
            txtNombres.Clear();
            txtFecIniPerLab.Clear();
            txtFecFinPerLab.Clear();

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
            txtCodigoInterno.Clear();
            txtDocIdent.Clear();
            txtNroDocIden.Clear();
            txtNombres.Clear();
            txtFecIniPerLab.Clear();
            txtFecFinPerLab.Clear();
        }

        private void cmbTipoCal_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtMesSem.Clear();
            txtFechaIni.Clear();
            txtFechaFin.Clear();
            txtMesSem.Focus();
            txtCodigoInterno.Clear();
            txtDocIdent.Clear();
            txtNroDocIden.Clear();
            txtNombres.Clear();
            txtFecIniPerLab.Clear();
            txtFecFinPerLab.Clear();
        }
    }
}