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
    public partial class ui_updcuentas : Form
    {
        private Form FormPadre;

        public Form _FormPadre
        {
            get { return FormPadre; }
            set { FormPadre = value; }
        }

        public ui_updcuentas()
        {
            InitializeComponent();
        }

        public void agregar()
        {
            txtOperacion.Text = "AGREGAR";
            txtCodigo.Enabled = true;
            txtCodigo.Clear();
            txtDescripcion.Clear();
            radioButtonDetallado.Checked = false;
            radioButtonResumen.Checked = true;
            cmbAnexo.Text = "XX     NO POSEE";
            cmbAnexoRef.Text = "XX     NO POSEE";
            txtTipoAne.Enabled = false;
            txtAne.Enabled = false;
            txtTipoAneRef.Enabled = false;
            txtAneRef.Enabled = false;
            txtTipoAne.Clear();
            txtAne.Clear();
            txtTipoAneRef.Clear();
            txtAneRef.Clear();
            txtCodigo.Focus();
        }

        public void editar(string codcuenta, string descuenta, string detallado,
            string modoane, string modoaneref, string tipane, string ane, string tipaneref, string aneref)
        {



            txtOperacion.Text = "EDITAR";
            txtCodigo.Enabled = false;
            txtCodigo.Text = codcuenta;
            txtDescripcion.Text = descuenta;
            if (detallado.Equals("S"))
            {
                radioButtonDetallado.Checked = true;
                radioButtonResumen.Checked = false;
            }
            else
            {
                radioButtonDetallado.Checked = false;
                radioButtonResumen.Checked = true;
            }

            switch (modoane)
            {
                case "MO":
                    cmbAnexo.Text = "MO   DEL CONCEPTO DE PLANILLA";
                    break;
                case "CU":
                    cmbAnexo.Text = "CU    DE LA CUENTA CONTABLE";
                    break;
                case "DO":
                    cmbAnexo.Text = "DO    DOCUMENTO DE IDENTIDAD DEL TRABAJADOR";
                    break;
                case "CO":
                    cmbAnexo.Text = "CO    CODIGO INTERNO DEL TRABAJADOR";
                    break;
                case "CA":
                    cmbAnexo.Text = "CA    CODIGO AUXILIAR DEL TRABAJADOR";
                    break;
                default:
                    cmbAnexo.Text = "XX     NO POSEE";
                    break;
            }

            switch (modoaneref)
            {
                case "MO":
                    cmbAnexoRef.Text = "MO   DEL CONCEPTO DE PLANILLA";
                    break;
                case "CU":
                    cmbAnexoRef.Text = "CU    DE LA CUENTA CONTABLE";
                    break;
                case "DO":
                    cmbAnexoRef.Text = "DO    DOCUMENTO DE IDENTIDAD DEL TRABAJADOR";
                    break;
                case "CO":
                    cmbAnexoRef.Text = "CO    CODIGO INTERNO DEL TRABAJADOR";
                    break;
                case "CA":
                    cmbAnexoRef.Text = "CA    CODIGO AUXILIAR DEL TRABAJADOR";
                    break;
                default:
                    cmbAnexoRef.Text = "XX     NO POSEE";
                    break;
            }
            txtTipoAne.Text = tipane;
            txtTipoAneRef.Text = tipaneref;
            txtAne.Text = ane;
            txtAneRef.Text = aneref;
            txtDescripcion.Focus();
        }

        private void txtDescripcion_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                e.Handled = true;
                radioButtonDetallado.Focus();
            }
        }

        private void txtCodigo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                e.Handled = true;
                txtDescripcion.Focus();
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private string ui_validarDatos()
        {

            Funciones funciones = new Funciones();
            string valorValida = "G";

            if (txtCodigo.Text == string.Empty && valorValida == "G")
            {
                valorValida = "B";
                MessageBox.Show("No ha ingresado Código de Cuenta", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtCodigo.Focus();
            }

            if (txtDescripcion.Text == string.Empty && valorValida == "G")
            {
                valorValida = "B";
                MessageBox.Show("No ha ingresado Nombre de Cuenta", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtDescripcion.Focus();
            }

            string modoane = funciones.getValorComboBox(cmbAnexo, 2);
            if (valorValida == "G")
            {
                if (modoane.Equals("CU"))
                {
                    if (txtTipoAne.Text == string.Empty && valorValida == "G")
                    {
                        valorValida = "B";
                        MessageBox.Show("No ha ingresado Tipo de Anexo", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txtTipoAne.Focus();
                    }
                    if (txtAne.Text == string.Empty && valorValida == "G")
                    {
                        valorValida = "B";
                        MessageBox.Show("No ha ingresado Anexo", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txtAne.Focus();
                    }
                }
            }

            string modoaneref = funciones.getValorComboBox(cmbAnexoRef, 2);
            if (valorValida == "G")
            {
                if (modoaneref.Equals("CU"))
                {
                    if (txtTipoAneRef.Text == string.Empty && valorValida == "G")
                    {
                        valorValida = "B";
                        MessageBox.Show("No ha ingresado Tipo de Anexo de Referencia", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txtTipoAneRef.Focus();
                    }
                    if (txtAneRef.Text == string.Empty && valorValida == "G")
                    {
                        valorValida = "B";
                        MessageBox.Show("No ha ingresado Anexo de Referencia", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txtAneRef.Focus();
                    }
                }
            }

            return valorValida;

        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            try
            {
                string valorValida = ui_validarDatos();
                if (valorValida.Equals("G"))
                {
                    GlobalVariables variables = new GlobalVariables();
                    Funciones funciones = new Funciones();
                    string idcia = variables.getValorCia();
                    string operacion = txtOperacion.Text.Trim();
                    string codcuenta = txtCodigo.Text.Trim();
                    string descuenta = txtDescripcion.Text.Trim();
                    string detallado;
                    if (radioButtonDetallado.Checked)
                    {
                        detallado = "S";
                    }
                    else
                    {
                        detallado = "N";
                    }
                    string modoane = funciones.getValorComboBox(cmbAnexo, 2);
                    string modoaneref = funciones.getValorComboBox(cmbAnexoRef, 2);
                    string tipane = txtTipoAne.Text;
                    string ane = txtAne.Text;
                    string tipaneref = txtTipoAneRef.Text;
                    string aneref = txtAneRef.Text;
                    CuenCon cuencon = new CuenCon();
                    cuencon.actualizarCuenCon(operacion, codcuenta, descuenta, detallado, idcia, modoane,
                        tipane, ane, modoaneref, tipaneref, aneref);
                    ((ui_cuentas)FormPadre).btnActualizar.PerformClick();
                    this.Close();
                }
            }
            catch (FormatException ex)
            {
                MessageBox.Show(ex.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ui_updcuentas_Load(object sender, EventArgs e)
        {

        }

        private void txtOperacion_TextChanged(object sender, EventArgs e)
        {

        }

        private void radioButtonDetallado_CheckedChanged(object sender, EventArgs e)
        {


        }

        private void cmbAnexo_SelectedIndexChanged(object sender, EventArgs e)
        {
            Funciones funciones = new Funciones();
            string valor = funciones.getValorComboBox(cmbAnexo, 2);
            if (valor.Equals("CU"))
            {
                txtTipoAne.Enabled = true;
                txtAne.Enabled = true;
                txtTipoAne.Clear();
                txtAne.Clear();
                txtTipoAne.Focus();
            }
            else
            {
                txtTipoAne.Enabled = false;
                txtAne.Enabled = false;
                txtTipoAne.Clear();
                txtAne.Clear();
            }

        }

        private void cmbAnexoRef_SelectedIndexChanged(object sender, EventArgs e)
        {
            Funciones funciones = new Funciones();
            string valor = funciones.getValorComboBox(cmbAnexoRef, 2);
            if (valor.Equals("CU"))
            {
                txtTipoAneRef.Enabled = true;
                txtAneRef.Enabled = true;
                txtTipoAneRef.Clear();
                txtAneRef.Clear();
                txtTipoAneRef.Focus();
            }
            else
            {
                txtTipoAneRef.Enabled = false;
                txtAneRef.Enabled = false;
                txtTipoAneRef.Clear();
                txtAneRef.Clear();
            }
        }
    }
}