using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace CaniaBrava
{
    public partial class ui_autentica : ui_form
    {
        string _autentica = string.Empty;
        public string _tipousr;

        public ui_autentica()
        {
            InitializeComponent();
        }

        private void txtclave_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                string usuarioActivo;
                string usuario = txtusuario.Text.Trim();
                string clave = txtclave.Text.Trim();

                if (usuario == string.Empty)
                {
                    MessageBox.Show("No ha especificado usuario", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    txtusuario.Focus();
                }
                else
                {
                    if (clave == string.Empty)
                    {
                        MessageBox.Show("No ha ingresado clave de acceso", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        txtclave.Focus();
                    }
                    else
                    {

                        UsrFile usrfile = new UsrFile();
                        usuarioActivo = usrfile.validaUsrFile(usuario, clave, "USUARIO");

                        if (usuarioActivo == string.Empty)
                        {
                            MessageBox.Show("Clave Incorrecta", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                            txtusuario.Clear();
                            txtclave.Clear();
                            txtpropietario.Clear();
                            txtusuario.Focus();

                        }
                        else
                        {
                            txtpropietario.Text = usuarioActivo;
                            e.Handled = true;
                            toolStripForm.Items[0].Select();
                            toolStripForm.Focus();
                        }
                    }

                }

            }
        }

        private void btnIniciar_Click(object sender, EventArgs e)
        {
            Funciones funciones = new Funciones();
            string usuarioActivo;
            string usuario = txtusuario.Text.Trim();
            string clave = txtclave.Text.Trim();
            string tipousr = string.Empty;

            if (usuario == string.Empty)
            {
                MessageBox.Show("No ha especificado usuario", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                txtusuario.Focus();
            }
            else
            {
                if (clave == string.Empty)
                {
                    MessageBox.Show("No ha ingresado clave de acceso", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    txtclave.Focus();
                }
                else
                {
                    UsrFile usrfile = new UsrFile();
                    usuarioActivo = usrfile.validaUsrFile(usuario, clave, "USUARIO");

                    if (usuarioActivo == String.Empty)
                    {
                        MessageBox.Show("Credenciales Incorrectos", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        tipousr = string.Empty;
                        txtusuario.Text = string.Empty;
                        txtclave.Text = string.Empty;
                        txtpropietario.Text = string.Empty;
                        txtusuario.Focus();
                    }
                    else
                    {
                        tipousr = usrfile.validaUsrFile(usuario, clave, "TIPO_USUARIO");
                        if (tipousr.Equals(this._tipousr) || tipousr.Equals("00"))
                        {
                            GlobalVariables gv = new GlobalVariables();
                            gv.setAutentica("S");
                            MessageBox.Show("Usuario Autenticado", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            this.Close();
                        }
                        else
                        {
                            MessageBox.Show("El usuario " + usuario + " no posee los derechos suficientes para realizar la acción", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                            tipousr = string.Empty;
                            txtusuario.Text = string.Empty;
                            txtclave.Text = string.Empty;
                            txtpropietario.Text = string.Empty;
                            txtusuario.Focus();
                        }
                    }
                }
            }
        }

        private void ui_autentica_Load(object sender, EventArgs e)
        {
            GlobalVariables gv = new GlobalVariables();
            gv.setAutentica("N");

            if (this._tipousr.Equals("M"))
            {
                pbMaster.Visible = true;
            }
            else
            {
                pbMaster.Visible = false;
            }
        }

        private void txtusuario_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                e.Handled = true;
                txtclave.Focus();
            }
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}