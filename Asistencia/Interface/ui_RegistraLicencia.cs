using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Configuration;

namespace CaniaBrava
{
    public partial class ui_RegistraLicencia : Form
    {
        public ui_RegistraLicencia()
        {
            InitializeComponent();
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ui_RegistraLicencia_Load(object sender, EventArgs e)
        {
            GlobalVariables globalVariable = new GlobalVariables();
            txtSerie.Text = globalVariable.getNroSerie();
            txtRuc.Text = globalVariable.getValorLicRuc();
            txtRazon.Text = globalVariable.getValorLicRazon();
            txtDireccion.Text = globalVariable.getValorLicDireccion();
            txtEmail.Text = globalVariable.getValorLicEmail();
        }

        private void txtRuc_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                e.Handled = true;
                txtRazon.Focus();
            }
        }

        private void txtRazon_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                e.Handled = true;
                txtDireccion.Focus();
            }
        }

        private void txtDireccion_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                e.Handled = true;
                txtEmail.Focus();
            }
        }

        private void txtEmail_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }
    }
}