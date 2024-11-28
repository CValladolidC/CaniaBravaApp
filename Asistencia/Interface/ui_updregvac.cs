using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CaniaBrava
{
    public partial class ui_updregvac : Form
    {
        string _idcia;
        string _idtipoper;
        string _operacion;
        string _idregvac;
       
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

        public void setValores(string idcia, string idtipoper)
        {

            this._operacion = string.Empty;
            this._idcia = idcia;
            this._idtipoper = idtipoper;
        }

        public ui_updregvac()
        {
            InitializeComponent();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            ((ui_regvac)FormPadre).btnActualizar.PerformClick();
            Close();
        }

        private void ui_updregvac_Load(object sender, EventArgs e)
        {

        }

        private void txtCodigoInterno_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                string idcia = this._idcia;
                string idtipoper = this._idtipoper;
                string cadenaBusqueda = string.Empty;
                string condicionAdicional = string.Empty;
                this._TextBoxActivo = txtCodigoInterno;
                FiltrosMaestros filtros = new FiltrosMaestros();
                filtros.filtrarPerPlan("ui_updregvac", this, txtCodigoInterno, idcia, idtipoper, "V", cadenaBusqueda, condicionAdicional);
            }
        }

        public void ui_newRegVac()
        {
            UtileriasFechas utileriasfechas = new UtileriasFechas();
            this._operacion = "AGREGAR";
            txtCodigoInterno.Enabled = true;
            lblF2.Visible = true;
            pictureBoxBuscar.Visible = true;
            txtCodigoInterno.Clear();
            txtNombres.Clear();
            txtDocIdent.Clear();
            txtNroDocIden.Clear();
            txtFecIniPerLab.Clear();
            txtAnio.Text = Convert.ToString(DateTime.Now.Year); 
            txtInicio.Text = DateTime.Now.ToString("dd/MM/yyyy");
            txtFin.Text = DateTime.Now.ToString("dd/MM/yyyy");
            txtDiasVaca.Text = Convert.ToString(utileriasfechas.diferenciaEntreFechas(txtInicio.Text, txtFin.Text));
            txtCodigoInterno.Focus();
        }

        public void ui_loadDatosRegVac(string idperplan, string anio, string finivac, string ffinvac, string diasvac, string idregvac)
        {
            UtileriasFechas utileriasfechas = new UtileriasFechas();
            Funciones funciones = new Funciones();
            PerPlan perplan = new PerPlan();
            this._operacion = "EDITAR";
            txtCodigoInterno.Enabled = false;
            lblF2.Visible = false;
            pictureBoxBuscar.Visible = false;
            txtCodigoInterno.Text = idperplan;
            txtDocIdent.Text = perplan.ui_getDatosPerPlan(this._idcia, idperplan, "1");
            txtNroDocIden.Text = perplan.ui_getDatosPerPlan(this._idcia, idperplan, "2");
            txtNombres.Text = perplan.ui_getDatosPerPlan(this._idcia, idperplan, "3");
            txtFecIniPerLab.Text = perplan.ui_getDatosPerPlan(this._idcia, idperplan, "4");
            this._idregvac = idregvac;
            txtAnio.Text = anio;
            txtInicio.Text = finivac;
            txtFin.Text = ffinvac;
            txtDiasVaca.Text = diasvac;
            txtAnio.Focus();
        }

        private void txtCodigoInterno_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                if (txtCodigoInterno.Text.Trim() != string.Empty)
                {
                    string idcia = this._idcia;
                    string idperplan = txtCodigoInterno.Text.Trim();

                    PerPlan perplan = new PerPlan();
                    string codigoInterno = perplan.ui_getDatosPerPlan(idcia, idperplan, "0");
                    if (codigoInterno == string.Empty)
                    {
                        MessageBox.Show("Código no registrado del trabajador", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txtCodigoInterno.Clear();
                        txtDocIdent.Clear();
                        txtNroDocIden.Clear();
                        txtNombres.Clear();
                        txtFecIniPerLab.Clear();
                    }

                    else
                    {
                        txtCodigoInterno.Text = perplan.ui_getDatosPerPlan(idcia, idperplan, "0");
                        txtDocIdent.Text = perplan.ui_getDatosPerPlan(idcia, idperplan, "1");
                        txtNroDocIden.Text = perplan.ui_getDatosPerPlan(idcia, idperplan, "2");
                        txtNombres.Text = perplan.ui_getDatosPerPlan(idcia, idperplan, "3");
                        txtFecIniPerLab.Text = perplan.ui_getDatosPerPlan(idcia, idperplan, "4");
                        e.Handled = true;
                        txtAnio.Focus();
                    }

                }
                else
                {
                    txtCodigoInterno.Clear();
                    txtDocIdent.Clear();
                    txtNroDocIden.Clear();
                    txtNombres.Clear();
                    txtFecIniPerLab.Clear();
                    txtCodigoInterno.Focus();
                }

            }
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            try
            {
                string idcia = this._idcia;
                string anio = txtAnio.Text;
                string idtipoper = this._idtipoper;
                string idperplan = txtCodigoInterno.Text.Trim();

                if (idperplan != string.Empty)
                {
                    string finivac = txtInicio.Text;
                    string ffinvac = txtFin.Text;
                    UtileriasFechas utileriasfechas = new UtileriasFechas();
                    int diasvac = utileriasfechas.diferenciaEntreFechas(txtInicio.Text, txtFin.Text);
                    string valorValida = "G";
                    if (txtCodigoInterno.Text == string.Empty && valorValida == "G")
                    {
                        valorValida = "B";
                        MessageBox.Show("No ha seleccionado Trabajador", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txtCodigoInterno.Focus();
                    }
                    if (diasvac == 0 && valorValida == "G")
                    {
                        valorValida = "B";
                        MessageBox.Show("Periodo de Vacaciones no válido", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txtInicio.Focus();
                    }
                    if (valorValida.Equals("G"))
                    {
                        RegVac regvac = new RegVac();
                        string idregvac;
                        if (this._operacion.Equals("AGREGAR"))
                        {
                            idregvac = regvac.generaCodigoRegVac(idcia);
                        }
                        else
                        {
                            idregvac = this._idregvac;
                        }
                        regvac.actualizarRegVac(this._operacion, idperplan, idcia, anio, finivac, ffinvac, diasvac, idregvac);
                        ((ui_regvac)FormPadre).btnActualizar.PerformClick();
                        Close();
                    }
                }
                else
                {
                    MessageBox.Show("No ha seleccionado trabajador", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtCodigoInterno.Focus();
                }
            }
            catch (FormatException ex)
            {
                MessageBox.Show(ex.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtInicio_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                if (UtileriasFechas.IsDate(txtInicio.Text))
                {
                        UtileriasFechas utileriasfechas = new UtileriasFechas();
                        txtDiasVaca.Text = Convert.ToString(utileriasfechas.diferenciaEntreFechas(txtInicio.Text, txtFin.Text));
                        e.Handled = true;
                        txtFin.Focus();
                }
                else
                {
                    MessageBox.Show("Fecha de Inicio no válida", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    e.Handled = true;
                    txtInicio.Clear();
                    txtFin.Clear();
                    txtDiasVaca.Clear();
                    txtInicio.Focus();
                }
            }
        }

        private void txtFin_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                if (UtileriasFechas.IsDate(txtFin.Text))
                {
                    if (UtileriasFechas.compararFecha(txtInicio.Text, "<=", txtFin.Text))
                    {
                        UtileriasFechas utileriasfechas = new UtileriasFechas();
                        txtDiasVaca.Text = Convert.ToString(utileriasfechas.diferenciaEntreFechas(txtInicio.Text, txtFin.Text));
                        e.Handled = true;
                        toolStripForm.Items[0].Select();
                        toolStripForm.Focus();
                    }
                    else
                    {
                        MessageBox.Show("Fecha de Fin no puede ser menor que la Fecha de Inicio", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        e.Handled = true;
                        txtFin.Clear();
                        txtDiasVaca.Clear();
                        txtFin.Focus();
                    }

                }
                else
                {
                    MessageBox.Show("Fecha de Fin no válida", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    e.Handled = true;
                    txtFin.Clear();
                    txtDiasVaca.Clear();
                    txtFin.Focus();

                }

            }
        }

        private void txtAnio_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                        e.Handled = true;
                        txtInicio.Focus();
            }
        }
    }
}