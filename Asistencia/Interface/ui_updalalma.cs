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
    public partial class ui_updalalma : Form
    {
        Funciones funciones = new Funciones();
        public string _codcia;
        private string _operacion;

        private Form FormPadre;

        public Form _FormPadre
        {
            get { return FormPadre; }
            set { FormPadre = value; }
        }

        public ui_updalalma()
        {
            InitializeComponent();

            if (funciones.VersionAssembly()) Application.ExitThread();
        }

        private void txtUsuario_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                e.Handled = true;
                txtDescripcion.Focus();
            }
        }

        private void txtDescripcion_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                e.Handled = true;
                txtPE.Focus();
            }
        }

        private void txtPE_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                txtPE.Text = funciones.replicateCadena("0", 10 - txtPE.Text.Trim().Length) + txtPE.Text.Trim();
                e.Handled = true;
                txtPS.Focus();
            }
        }

        private void txtPS_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                txtPS.Text = funciones.replicateCadena("0", 10 - txtPS.Text.Trim().Length) + txtPS.Text.Trim();
                e.Handled = true;
                cmbEstado.Focus();
            }
        }

        private void cmbEstado_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {

                e.Handled = true;
                toolStripForm.Items[0].Select();
                toolStripForm.Focus();

            }
        }

        public void agregar()
        {
            this._operacion = "AGREGAR";
            txtCodigo.Enabled = true;
            txtCodigo.Clear();
            txtDescripcion.Clear();
            txtPE.Clear();
            txtPS.Clear();
            cmbEstado.Text = "V         VIGENTE";
            txtCodigo.Focus();
        }

        public void editar(string codalma, string desalma, string nrope, string nrops, string estado)
        {
            this._operacion = "EDITAR";
            txtCodigo.Enabled = false;
            txtCodigo.Text = codalma;
            txtDescripcion.Text = desalma.Trim();
            txtPE.Text = nrope;
            txtPS.Text = nrops;
            switch (estado)
            {
                case "A":
                    cmbEstado.Text = "A        ANULADO";
                    break;
                case "V":
                    cmbEstado.Text = "V         VIGENTE";
                    break;
                default:
                    cmbEstado.Text = "V         VIGENTE";
                    break;
            }
            txtDescripcion.Focus();
        }

        private string ui_valida()
        {
            string valorValida = "G";

            if (txtCodigo.Text == string.Empty && valorValida == "G")
            {
                MessageBox.Show("No ha especificado Código", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                valorValida = "B";
                txtCodigo.Focus();
            }

            if (txtDescripcion.Text == string.Empty && valorValida == "G")
            {
                MessageBox.Show("No ha ingresado descripción", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                valorValida = "B";
                txtDescripcion.Focus();
            }

            if (txtPE.Text == string.Empty && valorValida == "G")
            {
                MessageBox.Show("No ha definido Nro. de Parte de Entrada", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                valorValida = "B";
                txtPE.Focus();
            }

            if (txtPS.Text == string.Empty && valorValida == "G")
            {
                MessageBox.Show("No ha definido Nro. de Parte de Salida", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                valorValida = "B";
                txtPS.Focus();
            }

            string estado = funciones.getValorComboBox(cmbEstado, 1);

            if (estado == string.Empty && valorValida == "G")
            {
                MessageBox.Show("No ha especificado Estado", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                valorValida = "B";
                cmbEstado.Focus();
            }

            if (estado != "V" && estado != "A" && valorValida == "G")
            {
                MessageBox.Show("Dato incorrecto en Estado", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                valorValida = "B";
                cmbEstado.Focus();
            }

            return valorValida;
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            string valorValida = ui_valida();

            if (valorValida.Equals("G"))
            {
                string operacion = this._operacion;
                string codcia = this._codcia;
                string codalma = funciones.replicateCadena("0", 2 - txtCodigo.Text.Trim().Length) + txtCodigo.Text.Trim();
                string desalma = txtDescripcion.Text;
                string nrope = funciones.replicateCadena("0", 10 - txtPE.Text.Trim().Length) + txtPE.Text.Trim();
                string nrops = funciones.replicateCadena("0", 10 - txtPS.Text.Trim().Length) + txtPS.Text.Trim();
                string estado = funciones.getValorComboBox(cmbEstado, 1);

                Alalma alalma = new Alalma();
                alalma.updAlalma(operacion, codcia, codalma, desalma, nrope, nrops, estado);
                ((ui_alalma)FormPadre).btnActualizar.PerformClick();
                this.Close();
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ui_updalalma_Load(object sender, EventArgs e)
        {

        }
    }
}