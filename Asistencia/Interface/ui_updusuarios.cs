using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace CaniaBrava
{
    public partial class ui_updusuarios : Form
    {
        Funciones funciones = new Funciones();
        private Form FormPadre;

        public Form _FormPadre
        {
            get { return FormPadre; }
            set { FormPadre = value; }
        }

        public ui_updusuarios()
        {
            InitializeComponent();

            if (funciones.VersionAssembly()) Application.ExitThread();
        }

        private void txtUsuario_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                e.Handled = true;
                txtPropietario.Focus();
            }
        }

        private void txtPropietario_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                e.Handled = true;
                txtClave.Focus();
            }
        }

        private void txtClave_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                e.Handled = true;
                cmbTipo.Focus();
            }
        }

        private void txtUsuario_Enter(object sender, EventArgs e)
        {
            txtUsuario.SelectAll();
        }

        private void txtPropietario_Enter(object sender, EventArgs e)
        {
            txtPropietario.SelectAll();
        }

        private void txtClave_Enter(object sender, EventArgs e)
        {
            txtClave.SelectAll();
        }

        private void cmbTipo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                e.Handled = true;
                cmbEstado.Focus();
            }
        }

        internal void NewUsuario()
        {
            txtOperacion.Text = "AGREGAR";
            txtUsuario.Enabled = true;
            txtUsuario.Clear();
            txtPropietario.Clear();
            txtClave.Clear();
            txtMail.Clear();
            MaesGen maesgen = new MaesGen();
            maesgen.consultaDetMaesGen("039", "04", cmbTipo);
            cmbEstado.Text = "V         VIGENTE";
            txtUsuario.Focus();
        }

        internal void LoadUsuarios(String idusr, String desusr, String pasusr, String typeusr, String stateusr, String mail)
        {
            txtOperacion.Text = "EDITAR";
            txtUsuario.Enabled = false;
            txtUsuario.Text = idusr;
            txtPropietario.Text = desusr;
            txtClave.Text = pasusr;
            txtMail.Text = mail;

            MaesGen maesgen = new MaesGen();
            maesgen.consultaDetMaesGen("039", typeusr, cmbTipo);

            switch (stateusr)
            {
                case "A": cmbEstado.Text = "A        ANULADO"; break;
                default: cmbEstado.Text = "V         VIGENTE"; break;
            }

            txtPropietario.Focus();
        }

        private void cmbEstado_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                e.Handled = true;
                txtMail.Focus();
            }
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            string operacion = txtOperacion.Text.Trim();
            string idUsr = txtUsuario.Text.Trim();
            string desUsr = txtPropietario.Text.Trim();
            string passUsr = txtClave.Text.Trim();
            string typeUsr = cmbTipo.Text.Substring(0, 2);
            string stateUsr = cmbEstado.Text.Substring(0, 1);
            string mail = txtMail.Text.Trim();

            if (idUsr == string.Empty)
            {
                MessageBox.Show("No ha especificado Usuario", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                txtUsuario.Focus();
            }
            else
            {
                if (desUsr == String.Empty)
                {
                    MessageBox.Show("No ha especificado Propietario", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    txtPropietario.Focus();
                }
                else
                {
                    if (passUsr == String.Empty)
                    {
                        MessageBox.Show("No ha especificado Clave de Acceso", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        txtClave.Focus();
                    }
                    else
                    {
                        if (!Regex.IsMatch(passUsr, @"^(?=.*[A-Za-z])(?=.*\d)(?=.*[@$!%*#?&])[A-Za-z\d@$!%*#?&]{8,}$"))
                        {
                            MessageBox.Show("La clave debe contener letras, números y caracteres especiales, ademas debe tener al menos 8 caracteres", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        }
                        else
                        {
                            if (stateUsr == string.Empty)
                            {
                                MessageBox.Show("No ha especificado Estado", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                                cmbEstado.Focus();
                            }
                            else
                            {
                                if (stateUsr != "V" && stateUsr != "A")
                                {
                                    MessageBox.Show("Dato incorrecto en Estado", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                                    cmbEstado.Focus();
                                }
                                else
                                {
                                    if (mail != string.Empty)
                                    {
                                        if (!Funciones.IsValidEmail(mail))
                                        {
                                            MessageBox.Show("Email incorrecto.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                                            txtMail.Focus();
                                        }
                                        else
                                        {
                                            UsrFile updusrfile = new UsrFile();
                                            updusrfile.actualizarUsr(operacion, idUsr, desUsr, passUsr, typeUsr, stateUsr, mail);
                                            ((ui_usuarios)FormPadre).btnActualizar.PerformClick();
                                            this.Close();
                                        }
                                    }
                                    else
                                    {
                                        UsrFile updusrfile = new UsrFile();
                                        updusrfile.actualizarUsr(operacion, idUsr, desUsr, passUsr, typeUsr, stateUsr, mail);
                                        ((ui_usuarios)FormPadre).btnActualizar.PerformClick();
                                        this.Close();
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public void LoadCombos()
        {
            MaesGen maesgen = new MaesGen();
            maesgen.listaDetMaesGen("039", cmbTipo, "");
        }

        private void txtMail_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                if (Funciones.IsValidEmail(txtMail.Text))
                {
                    e.Handled = true;
                    toolStripForm.Items[0].Select();
                    toolStripForm.Focus();
                }
                else
                {
                    MessageBox.Show("Email incorrecto.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
            }
        }
    }
}