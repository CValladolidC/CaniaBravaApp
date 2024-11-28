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
    public partial class ui_updgrparti : Form
    {
        string _famarti;
        string _operacion;

        private Form FormPadre;

        public Form _FormPadre
        {
            get { return FormPadre; }
            set { FormPadre = value; }
        }

        public ui_updgrparti()
        {
            InitializeComponent();
        }

        public void setData(string famarti)
        {
            this._famarti = famarti;
        }

        public void agregar()
        {
            this._operacion = "AGREGAR";
            txtCodigo.Enabled = true;
            txtCodigo.Clear();
            txtDescripcion.Clear();
            cmbEstado.Text = "V         VIGENTE";
            txtCodigo.Focus();
        }

        public void editar(string grparti,string desgrparti,string estado)
        {

            this._operacion = "EDITAR";
            txtCodigo.Enabled = false;
            txtCodigo.Text = grparti;
            txtDescripcion.Text = desgrparti;
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

        
        private void txtDescripcion_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                e.Handled = true;
                cmbEstado.Focus();
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
                string operacion = this._operacion;
                string famarti = this._famarti;
                string grparti = txtCodigo.Text.Trim();
                string desgrparti = txtDescripcion.Text.Trim();
                string estado = cmbEstado.Text.Substring(0, 1);
                              

                if (grparti == string.Empty)
                {
                    MessageBox.Show("No ha especificado Código", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    txtCodigo.Focus();

                }
                else
                {

                    if (desgrparti == String.Empty)
                    {
                        MessageBox.Show("No ha especificado Descripción", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        txtDescripcion.Focus();
                    }
                    else
                    {

                        if (estado == string.Empty)
                        {
                            MessageBox.Show("No ha especificado Estado", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                            cmbEstado.Focus();
                        }
                        else
                        {
                            if (estado != "V" && estado != "A")
                            {
                                MessageBox.Show("Dato incorrecto en Estado", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                                cmbEstado.Focus();
                            }
                            else
                            {
                                GrpArti grupoarti = new GrpArti();
                                grupoarti.updGrpArti(operacion, famarti, grparti, desgrparti, estado);
                                ((ui_grupoarti)FormPadre).btnActualizar.PerformClick();
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

        private void ui_updgrparti_Load(object sender, EventArgs e)
        {


        }

       
    }
}
