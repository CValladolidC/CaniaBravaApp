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
    public partial class ui_updfonpen : Form
    {
        private Form FormPadre;

        public Form _FormPadre
        {
            get { return FormPadre; }
            set { FormPadre = value; }
        }

        public ui_updfonpen()
        {
            InitializeComponent();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void txtCodigo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                e.Handled = true;
                txtFondo.Focus();
            }
        }

        private void txtFondo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                e.Handled = true;
                txtCodNet.Focus();
            }
        }

        private void cmbEstado_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                if (cmbEstado.Text == String.Empty)
                {
                    MessageBox.Show("El campo no acepta valores vacíos", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    e.Handled = true;
                    cmbEstado.Focus();
                }
                else
                {
                    switch (cmbEstado.Text.ToUpper().Substring(0, 1))
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
                    e.Handled = true;
                    toolStripForm.Items[0].Select();
                    toolStripForm.Focus();
                }
            }
        }

        internal void newFonPen()
        {
            txtOperacion.Text = "AGREGAR";
            txtCodigo.Enabled = true;
            txtCodigo.Clear();
            txtFondo.Clear();
            txtCodNet.Clear();
            txtPS.Clear();
            txtCV.Clear();
            txtCF.Clear();
            txtSNP.Clear();
            cmbEstado.Text = "V         VIGENTE";
            txtCodigo.Focus();
        }

        internal void loadFonPen(string idfonpen, string desfonpen, float psfonpen, float cvfonpen, float cffonpen, string statefonpen, float snpfonpen, string codnet)
        {

            txtOperacion.Text = "EDITAR";
            txtCodigo.Enabled = false;
            txtCodigo.Text = idfonpen;
            txtFondo.Text = desfonpen;
            txtPS.Text = Convert.ToString(psfonpen);
            txtCV.Text = Convert.ToString(cvfonpen);
            txtCF.Text = Convert.ToString(cffonpen);
            txtSNP.Text = Convert.ToString(snpfonpen);
            txtCodNet.Text = codnet;

            switch (statefonpen)
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

            txtFondo.Focus();

        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            try
            {
                Funciones funciones = new Funciones();
                string operacion = txtOperacion.Text.Trim();
                string idfonpen = txtCodigo.Text.Trim();
                string desfonpen = txtFondo.Text.Trim();
                float psfonpen = float.Parse(txtPS.Text);
                float cvfonpen = float.Parse(txtCV.Text);
                float cffonpen = float.Parse(txtCF.Text);
                float snpfonpen = float.Parse(txtSNP.Text);
                string codnet = txtCodNet.Text;
                string statefonpen = funciones.getValorComboBox(cmbEstado, 1);

                if (idfonpen == string.Empty)
                {
                    MessageBox.Show("No ha especificado Código", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    txtCodigo.Focus();

                }
                else
                {

                    if (desfonpen == String.Empty)
                    {
                        MessageBox.Show("No ha especificado Fondo de Pensiones", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        txtFondo.Focus();
                    }
                    else
                    {

                        if (statefonpen == string.Empty)
                        {
                            MessageBox.Show("No ha especificado Estado", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                            cmbEstado.Focus();
                        }
                        else
                        {
                            if (statefonpen != "V" && statefonpen != "A")
                            {
                                MessageBox.Show("Dato incorrecto en Estado", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                                cmbEstado.Focus();
                            }
                            else
                            {
                                FonPen updfonpen = new FonPen();
                                updfonpen.setFonPen(idfonpen, desfonpen, psfonpen, cvfonpen, cffonpen, statefonpen, snpfonpen, codnet);

                                updfonpen.actualizarFonPen(operacion);
                                ((ui_fonpen)FormPadre).btnActualizar.PerformClick();
                                this.Close();
                            }
                        }

                    }

                }

            }
            catch (FormatException ex)
            {
                MessageBox.Show(ex.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ui_updfonpen_Load(object sender, EventArgs e)
        {

        }

        private void cmbEstado_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void txtPS_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                e.Handled = true;
                txtCV.Focus();
            }
        }

        private void txtCF_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                e.Handled = true;
                txtSNP.Focus();

            }
        }

        private void txtCV_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                e.Handled = true;
                txtCF.Focus();
            }
        }

        private void txtSNP_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                e.Handled = true;
                cmbEstado.Focus();
            }
        }

        private void txtCodNet_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                e.Handled = true;
                txtPS.Focus();
            }
        }
    }
}