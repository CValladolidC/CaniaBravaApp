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
    public partial class ui_updlabper : Form
    {
        public ui_updlabper()
        {
            InitializeComponent();
        }

        private Form FormPadre;

        public Form _FormPadre
        {
            get { return FormPadre; }
            set { FormPadre = value; }
        }

        internal void newLabPer(string tipoper)
        {
            txtOperacion.Text = "AGREGAR";
            txtTipo.Text = tipoper;
            txtCodigo.Enabled = true;
            txtCodigo.Clear();
            txtDescripcion.Clear();
            cmbEstado.Text = "V         VIGENTE";
            txtCodigo.Focus();
        }

        internal void loadLabPer(string idlabper, string deslabper, string statelabper,string tipoper)
        {
            txtOperacion.Text = "EDITAR";
            txtCodigo.Enabled = false;
            txtCodigo.Text = idlabper;
            txtDescripcion.Text = deslabper;
            txtTipo.Text = tipoper;
            
            switch (statelabper)
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

        private void txtCodigo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                e.Handled = true;
                txtDescripcion.Focus();
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

        private void txtDescripcion_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                e.Handled = true;
                cmbEstado.Focus();
            }
        }

        private void ui_updlabper_Load(object sender, EventArgs e)
        {

        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            try
            {
                string operacion = txtOperacion.Text.Trim();
                string idlabper = txtCodigo.Text.Trim();
                string deslabper = txtDescripcion.Text.Trim();
                string statelabper = cmbEstado.Text.Substring(0, 1);
                string idtipoper = txtTipo.Text.Substring(0, 1);

                GlobalVariables variables = new GlobalVariables();
                string idcia = variables.getValorCia();


                if (idlabper == string.Empty)
                {
                    MessageBox.Show("No ha especificado Código", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    txtCodigo.Focus();

                }
                else
                {

                    if (deslabper == String.Empty)
                    {
                        MessageBox.Show("No ha especificado Labor", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        txtDescripcion.Focus();
                    }
                    else
                    {

                        if (statelabper == string.Empty)
                        {
                            MessageBox.Show("No ha especificado Estado", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                            cmbEstado.Focus();
                        }
                        else
                        {
                            if (statelabper != "V" && statelabper != "A")
                            {
                                MessageBox.Show("Dato incorrecto en Estado", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                                cmbEstado.Focus();
                            }
                            else
                            {
                                LabPer updlabper = new LabPer();
                                updlabper.setLabPer(idcia, idtipoper, idlabper, deslabper, statelabper);
                                updlabper.actualizarLabPer(operacion);
                                ((ui_laborper)FormPadre).btnActualizar.PerformClick();
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

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}