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
using System.Drawing.Printing;

namespace CaniaBrava
{
    public partial class ui_Boleta : Form
    {
        private TextBox TextBoxActivo;
        private Form FormPadre;
        string _boleta;

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

        public ui_Boleta()
        {
            InitializeComponent();
        }

        private void ui_Boleta_Load(object sender, EventArgs e)
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
                ui_mostrarBoleta();
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
                ui_mostrarBoleta();
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
                    filtros.filtrarPerPlan("ui_Boleta", this, txtCodigoInterno, idcia, idtipoper, "", cadenaBusqueda, condicionAdicional);
                }
            }
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {

        }

        private void txtCodigoInterno_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Allow the user to select a file.
            OpenFileDialog ofd = new OpenFileDialog();
            if (DialogResult.OK == ofd.ShowDialog(this))
            {
                // Allow the user to select a printer.
                PrintDialog pd = new PrintDialog();
                pd.PrinterSettings = new PrinterSettings();
                if (DialogResult.OK == pd.ShowDialog(this))
                {
                    // Print the file to the printer.
                    RawPrinterHelper.SendFileToPrinter(pd.PrinterSettings.PrinterName, ofd.FileName);
                }
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void ui_mostrarBoleta()
        {
            Funciones fn = new Funciones();
            Exporta exporta = new Exporta();
            GlobalVariables gv = new GlobalVariables();
            string idtipoper = fn.getValorComboBox(cmbTipoTrabajador, 1);
            string periodo = txtMesSem.Text.Trim() + fn.replicateCadena(" ", 6);
            string messem = periodo.Substring(0, 2);
            string anio = periodo.Substring(3, 4);
            string idtipocal = fn.getValorComboBox(cmbTipoCal, 1);
            string idtipoplan = fn.getValorComboBox(cmbTipoPlan, 3);
            string idcia = gv.getValorCia();
            string idperplan = txtCodigoInterno.Text.Trim();
            if (idperplan != string.Empty)
            {
                lblprocesando.Visible = true;
                string strPrinter = "";
                Boleta boleta = new Boleta();
                strPrinter = strPrinter + boleta.generaBoletaTabular(idperplan, anio, messem, idcia, idtipoper, idtipoplan, idtipocal);
                this._boleta = strPrinter;
                txtBoleta.Text = exporta.Ascii_FromCadena(strPrinter);
                lblprocesando.Visible = false;
            }
            else
            {
                txtBoleta.Text = "";
            }

        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            string strPrinter = this._boleta;
            PrintDialog pd = new PrintDialog();
            pd.PrinterSettings = new PrinterSettings();
            pd.AllowSelection = true;
            pd.AllowSomePages = true;

            if (DialogResult.OK == pd.ShowDialog(this))
            {
                RawPrinterHelper.SendStringToPrinter(pd.PrinterSettings.PrinterName, strPrinter);
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {



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
            ui_mostrarBoleta();
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
            ui_mostrarBoleta();
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
            ui_mostrarBoleta();
        }

        private void button1_Click_2(object sender, EventArgs e)
        {

        }
    }
}