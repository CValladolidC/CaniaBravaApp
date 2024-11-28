using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CaniaBrava
{
    public partial class ui_ChangePass : Form
    {
        Funciones funciones = new Funciones();

        public ui_ChangePass()
        {
            InitializeComponent();

            if (funciones.VersionAssembly()) Application.ExitThread();
        }

        private void txtNewPass_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                e.Handled = true;
                txtNewPass2.Focus();
            }
        }

        private void txtNewPass2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                e.Handled = true;
                btnAceptar.Focus();
            }
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            string pass = txtNewPass.Text.Trim().ToUpper();
            string pass2 = txtNewPass2.Text.Trim().ToUpper();
            if (pass == string.Empty)
            {
                MessageBox.Show("Debe ingresar una contraseña", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (pass2 == string.Empty)
            {
                MessageBox.Show("Debe ingresar confirmación de contraseña", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (pass == pass2)
            {
                UsrFile updusrfile = new UsrFile();
                updusrfile.ChangePassword(txtUsuario.Text, pass);
                MessageBox.Show("Actualizacion de Contraseña Correcta..!!", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            else
            {
                MessageBox.Show("Las contraseñas son distintas..!!", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtNewPass.Clear();
                txtNewPass2.Clear();
                txtNewPass.Focus();
            }
        }

        private void ui_ChangePass_Load(object sender, EventArgs e)
        {
            GlobalVariables variables = new GlobalVariables();
            txtUsuario.Text = variables.getValorUsr();
        }
    }
}
