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
    public partial class ui_updcencos : Form
    {
        private Form FormPadre;

        public Form _FormPadre
        {
            get { return FormPadre; }
            set { FormPadre = value; }
        }

        public ui_updcencos()
        {
            InitializeComponent();
        }

        internal void newCenCos()
        {
            txtOperacion.Text = "AGREGAR";
            txtCodigo.Enabled = true;
            txtCodigo.Clear();
            txtDescripcion.Clear();
            txtCodAux.Clear();
            cmbEstado.Text = "V         VIGENTE";
            txtCodigo.Focus();
        }

        internal void loadCenCos(string idcencos, string descencos, string statecencos,string codaux)
        {

            txtOperacion.Text = "EDITAR";
            txtCodigo.Enabled = false;
            txtCodigo.Text = idcencos;
            txtDescripcion.Text = descencos;
            txtCodAux.Text = codaux;
            switch (statecencos)
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

        private void txtDescripcion_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                e.Handled = true;
                txtCodAux.Focus();
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

        private void cmbEstado_SelectedIndexChanged(object sender, EventArgs e)
        {

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

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            try
            {
                string operacion = txtOperacion.Text.Trim();
                string idcencos = txtCodigo.Text.Trim();
                string descencos = txtDescripcion.Text.Trim();
                string codaux = txtCodAux.Text.Trim();
                string statecencos = cmbEstado.Text.Substring(0, 1);
                
                GlobalVariables variables=new GlobalVariables();
                string idcia = variables.getValorCia();
                

                if (idcencos == string.Empty)
                {
                    MessageBox.Show("No ha especificado Código", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    txtCodigo.Focus();

                }
                else
                {

                    if (descencos == String.Empty)
                    {
                        MessageBox.Show("No ha especificado Centro de Costo", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        txtDescripcion.Focus();
                    }
                    else
                    {

                        if (statecencos == string.Empty)
                        {
                            MessageBox.Show("No ha especificado Estado", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                            cmbEstado.Focus();
                        }
                        else
                        {
                            if (statecencos != "V" && statecencos != "A")
                            {
                                MessageBox.Show("Dato incorrecto en Estado", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                                cmbEstado.Focus();
                            }
                            else
                            {
                                CenCos updcencos = new CenCos();
                                updcencos.actualizarCenCos(operacion, idcencos, idcia, descencos, statecencos,codaux);
                                ((ui_cencos)FormPadre).btnActualizar.PerformClick();
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

        private void ui_updcencos_Load(object sender, EventArgs e)
        {

        }

        private void txtCodAux_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                e.Handled = true;
                cmbEstado.Focus();
            }
        }
    }
}