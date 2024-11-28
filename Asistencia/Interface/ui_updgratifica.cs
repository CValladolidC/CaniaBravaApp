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
    public partial class ui_updgratifica : Form
    {
        string _empleador;
        string _idcia;
        string _idtipoper;
        string _operacion;
        string _anio;
        string _messem;
        string _idtipocal;
        string _fechaini;
        string _fechafin;
        string _idtipoplan;

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

        public ui_updgratifica()
        {
            InitializeComponent();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            ((ui_gratifica)FormPadre).btnActualizar.PerformClick();
            Close();
        }

        public void setValores(string idcia, string idtipoper, string empleador, string anio, string messem, string idtipocal, string fechaini, string fechafin, string idtipoplan)
        {

            this._operacion = string.Empty;
            this._idcia = idcia;
            this._idtipoper = idtipoper;
            this._empleador = empleador;
            this._anio = anio;
            this._messem = messem;
            this._idtipocal = idtipocal;
            this._fechaini = fechaini;
            this._fechafin = fechafin;
            this._idtipoplan = idtipoplan;

        }

        private void txtCodigoInterno_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                string idcia = this._idcia;
                string idtipoper = this._idtipoper;
                string rucemp = this._empleador.Substring(0, 11);
                string anio = this._anio;
                string messem = this._messem;
                string idtipocal = this._idtipocal;
                string idtipoplan = this._idtipoplan;

                string cadenaBusqueda = string.Empty;
                this._TextBoxActivo = txtCodigoInterno;
                string condicionAdicional = " and A.idtipoplan='" + @idtipoplan + "' and A.rucemp='" + @rucemp + "' and CONCAT(A.idperplan,A.idcia) not in (Select CONCAT(idperplan,idcia) from dataplan where anio='" + @anio + "' and messem='" + @messem + "' and idtipoper='" + @idtipoper + "' and idtipocal='" + idtipocal + "' and idtipoplan='" + @idtipoplan + "')";
                FiltrosMaestros filtros = new FiltrosMaestros();
                filtros.filtrarPerPlan("ui_updgratifica", this, txtCodigoInterno, idcia, idtipoper, "V", cadenaBusqueda, condicionAdicional);
            }
        }

        public void ui_newDatosPlanilla()
        {
            this._operacion = "AGREGAR";
            txtEmpleador.Text = this._empleador;
            txtCodigoInterno.Enabled = true;
            lblF2.Visible = true;
            pictureBoxBuscar.Visible = true;
            txtCodigoInterno.Clear();
            txtNombres.Clear();
            txtDocIdent.Clear();
            txtNroDocIden.Clear();
            txtFecIniPerLab.Clear();
            txtEstablecimiento.Clear();
            cmbSCTR.Text = "";
            txtGratifica.Text = "0";
            txtCodigoInterno.Focus();
        }

        public void ui_loadDatosPlanilla(string idperplan, string establecimiento, string riesgo, string idestane, string gratifica)
        {

            Funciones funciones = new Funciones();
            PerPlan perplan = new PerPlan();
            EstAne estane = new EstAne();

            this._operacion = "EDITAR";
            txtEmpleador.Text = this._empleador;
            txtCodigoInterno.Enabled = false;
            lblF2.Visible = false;
            pictureBoxBuscar.Visible = false;
            txtCodigoInterno.Text = idperplan;
            txtDocIdent.Text = perplan.ui_getDatosPerPlan(this._idcia, idperplan, "1");
            txtNroDocIden.Text = perplan.ui_getDatosPerPlan(this._idcia, idperplan, "2");
            txtNombres.Text = perplan.ui_getDatosPerPlan(this._idcia, idperplan, "3");
            txtFecIniPerLab.Text = perplan.ui_getDatosPerPlan(this._idcia, idperplan, "4");
            txtEstablecimiento.Text = establecimiento;
            txtRiesgo.Text = estane.ui_getDatosEstane(idestane, this._empleador.Substring(0, 11), this._idcia, "2");
            if (estane.ui_getDatosEstane(idestane, this._empleador.Substring(0, 11), this._idcia, "2").Equals("SI"))
            {
                cmbSCTR.Enabled = true;
                cmbSCTR.Text = riesgo;
            }
            else
            {
                cmbSCTR.Text = "0";
                cmbSCTR.Enabled = false;
            }

            txtGratifica.Text = gratifica;
            cmbSCTR.Focus();

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
                        txtEstablecimiento.Clear();
                        txtRiesgo.Clear();
                        cmbSCTR.Text = "0";
                        cmbSCTR.Enabled = false;


                    }

                    else
                    {
                        txtCodigoInterno.Text = perplan.ui_getDatosPerPlan(idcia, idperplan, "0");
                        txtDocIdent.Text = perplan.ui_getDatosPerPlan(idcia, idperplan, "1");
                        txtNroDocIden.Text = perplan.ui_getDatosPerPlan(idcia, idperplan, "2");
                        txtNombres.Text = perplan.ui_getDatosPerPlan(idcia, idperplan, "3");
                        txtFecIniPerLab.Text = perplan.ui_getDatosPerPlan(idcia, idperplan, "4");
                        txtEstablecimiento.Text = perplan.ui_getDatosPerPlan(idcia, idperplan, "5");
                        txtRiesgo.Text = perplan.ui_getDatosPerPlan(idcia, idperplan, "6");

                        if (perplan.ui_getDatosPerPlan(idcia, idperplan, "6").Equals("SI"))
                        {
                            Funciones funciones = new Funciones();
                            cmbSCTR.Enabled = true;
                            string rucemp = this._empleador.Substring(0, 11);
                            string query = "SELECT tasa as clave,'' as descripcion FROM tasaest where codemp='" + @rucemp + "' and idciafile='" + @idcia + "' order by 1 asc";
                            funciones.listaComboBox(query, cmbSCTR, "");
                            e.Handled = true;
                            cmbSCTR.Focus();

                        }
                        else
                        {
                            cmbSCTR.Text = "0";
                            cmbSCTR.Enabled = false;
                            e.Handled = true;
                            txtGratifica.Focus();


                        }

                    }

                }
                else
                {
                    txtCodigoInterno.Clear();
                    txtDocIdent.Clear();
                    txtNroDocIden.Clear();
                    txtNombres.Clear();
                    txtFecIniPerLab.Clear();
                    txtEstablecimiento.Clear();
                    txtRiesgo.Clear();
                    cmbSCTR.Text = "0";
                    cmbSCTR.Enabled = false;
                    txtCodigoInterno.Focus();
                }



            }
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            try
            {
                string idcia = this._idcia;
                string anio = this._anio;
                string messem = this._messem;
                string idtipoper = this._idtipoper;
                string idtipocal = this._idtipocal;
                string idperplan = txtCodigoInterno.Text.Trim();
                string idtipoplan = this._idtipoplan;
                float gratifica = float.Parse(txtGratifica.Text);
                string emplea = this._empleador.Substring(0, 11);
                string estane = txtEstablecimiento.Text.Substring(0, 4).Trim();
                float riesgo = float.Parse(cmbSCTR.Text);
                string valorValida = "G";

                if (txtCodigoInterno.Text == string.Empty && valorValida == "G")
                {
                    valorValida = "B";
                    MessageBox.Show("No ha seleccionado Trabajador", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtCodigoInterno.Focus();
                }

                if ((riesgo < 0 || riesgo > 100) && valorValida == "G")
                {
                    valorValida = "B";
                    MessageBox.Show("Rango de % SCTR-Salud inválido, se aceptan valores de 0 a 100", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    cmbSCTR.Focus();
                }

                if (gratifica == 0 && valorValida == "G")
                {
                    valorValida = "B";
                    MessageBox.Show("Ingrese importe de Gratificación", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtGratifica.Focus();
                }

                if (gratifica < 0 && valorValida == "G")
                {
                    valorValida = "B";
                    MessageBox.Show("Importe de Gratificación incorrecto", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtGratifica.Focus();
                }

                if (valorValida.Equals("G"))
                {
                    DataPlan dataplan = new DataPlan();
                    dataplan.actualizarDataPlanGratifica(this._operacion, idperplan, idcia,
                        anio, messem, idtipoper, idtipocal, emplea, estane, riesgo,
                        idtipoplan, gratifica);
                    ((ui_gratifica)FormPadre).btnActualizar.PerformClick();
                    Close();
                }
            }
            catch (FormatException ex)
            {
                MessageBox.Show(ex.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ui_updgratifica_Load(object sender, EventArgs e)
        {

        }

        private void txtGratifica_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                e.Handled = true;
                toolStripForm.Items[0].Select();
                toolStripForm.Focus();
            }
        }
    }
}