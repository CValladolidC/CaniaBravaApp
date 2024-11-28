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
    public partial class ui_upddetmaespdt : Form
    {
        private Form FormPadre;

        public Form _FormPadre
        {
            get { return FormPadre; }
            set { FormPadre = value; }
        }

        public ui_upddetmaespdt()
        {
            InitializeComponent();
        }

        private void ui_upddetmaespdt_Load(object sender, EventArgs e)
        {

        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        internal void newDetMaesGen(string maesgen)
        {
            txtOperacion.Text = "AGREGAR";
            txtMaestro.Text = maesgen;
            txtCodigo.Enabled = true;
            txtCodigo.Clear();
            txtDescripcion.Clear();
            txtCodigo.Focus();
        }

        internal void loadDetMaesGen(string maesgen, string rp_ccodigo, string rp_cdescri, string rp_cindice)
        {

            txtOperacion.Text = "EDITAR";
            txtMaestro.Text = maesgen;
            txtCodigo.Enabled = false;
            txtCodigo.Text = rp_ccodigo;
            txtDescripcion.Text = rp_cdescri;
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

        private void txtDescripcion_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtDescripcion_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                e.Handled = true;
                toolStripForm.Items[0].Select();
                toolStripForm.Focus();

            }
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            try
            {
                Funciones funciones = new Funciones();
                string operacion = txtOperacion.Text.Trim();
                string rp_cindice = txtMaestro.Text.Substring(0, 2);
                string rp_ccodigo = txtCodigo.Text.Trim();
                string rp_cdescri = txtDescripcion.Text.Trim();

                if (rp_ccodigo == string.Empty)
                {
                    MessageBox.Show("No ha especificado Código", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    txtCodigo.Focus();

                }
                else
                {
                    MaesPdt maespdt = new MaesPdt();
                    maespdt.actualizarMaesPdt(operacion, rp_cindice, rp_ccodigo, rp_cdescri);
                    ((ui_updmaespdt)FormPadre).btnActualizar.PerformClick();
                    this.Close();

                }

            }
            catch (FormatException ex)
            {
                MessageBox.Show(ex.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}