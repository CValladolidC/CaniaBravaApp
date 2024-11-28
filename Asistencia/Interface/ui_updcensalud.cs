using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace CaniaBrava
{
    public partial class ui_updcensalud : Form
    {
        Funciones funciones = new Funciones();
        private TextBox TextBoxActivo;
        private Form FormPadre;
        private string IsTrabajador;
        string _clasePadre;
        private TextBox TextBoxUbigeo;
        private TextBox TextBoxDscUbigeo;

        public TextBox _TextBoxUbigeo
        {
            get { return TextBoxUbigeo; }
            set { TextBoxUbigeo = value; }
        }

        public TextBox _TextBoxDscUbigeo
        {
            get { return TextBoxDscUbigeo; }
            set { TextBoxDscUbigeo = value; }
        }

        public TextBox _TextBoxActivo
        {
            get { return TextBoxActivo; }
            set { TextBoxActivo = value; }
        }

        public Form _FormPadre
        {
            get { return FormPadre; }
            set { FormPadre = value; }
        }

        public ui_updcensalud()
        {
            InitializeComponent();

            this.IsTrabajador = string.Empty;

            if (funciones.VersionAssembly()) Application.ExitThread();
        }

        public void New_()
        {
            txtDNI.Enabled = false;
            txtNombres.Focus();
        }

        public void setValores(string clasePadre)
        {
            this._clasePadre = clasePadre;
        }

        #region Eventos KeyPress
        private void cmbEstado_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                e.Handled = true;
                txtDNI.Focus();
            }
        }

        private void txtDNI_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                GlobalVariables gv = new GlobalVariables();
                PerPlan perplan = new PerPlan();
                string trabajador = string.Empty;
                string idperplan = txtDNI.Text.Trim();
                string codigoInterno = perplan.ui_getDatosPerPlan(idperplan, "0");

                if (codigoInterno != string.Empty)
                {
                    txtDNI.Text = perplan.ui_getDatosPerPlan(idperplan, "2");
                    trabajador = perplan.ui_getDatosPerPlan(idperplan, "3");
                    txtNombres.Text = trabajador.Split(',')[1].Trim();
                    txtApellidos.Text = trabajador.Split(',')[0].Trim();
                    e.Handled = true;
                }
                else
                {
                    txtNombres.Clear();
                    txtApellidos.Clear();
                }
                e.Handled = true;
                txtNombres.Focus();
            }
        }

        private void txtPropietario_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                e.Handled = true;
                txtApellidos.Focus();
            }
        }

        private void txtClave_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                e.Handled = true;
                btnUbigeo.Focus();
                //toolStripForm.Items[0].Select();
                //toolStripForm.Focus();
            }
        } 

        private void txtespecialidad_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                e.Handled = true;
                toolStripForm.Items[0].Select();
                toolStripForm.Focus();
            }
        }
        #endregion

        #region Eventos Enter
        private void txtDNI_Enter(object sender, EventArgs e)
        {
            txtDNI.SelectAll();
        }

        private void txtPropietario_Enter(object sender, EventArgs e)
        {
            txtNombres.SelectAll();
        }

        private void txtClave_Enter(object sender, EventArgs e)
        {
            txtApellidos.SelectAll();
        }
        #endregion

        #region Eventos Click
        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            string ruc = txtDNI.Text.Trim();
            string centrosalud = txtNombres.Text.Trim();
            string telefono = txtApellidos.Text.Trim();
            string ubigeo = txtcodUbigeo.Text.Trim();
            string direccion = txtdireccion.Text.Trim();

            //if (ruc == string.Empty)
            //{
            //    MessageBox.Show("No ha especificado RUC", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            //    txtDNI.Focus();
            //}
            //else
            {
                if (centrosalud == String.Empty)
                {
                    MessageBox.Show("No ha especificado un Centro de Salud / Clinica / Hospital", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    txtNombres.Focus();
                }
                else
                {
                    if (telefono == String.Empty)
                    {
                        MessageBox.Show("No ha especificado telefono", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        txtApellidos.Focus();
                    }
                    else
                    {
                        if (direccion == String.Empty)
                        {
                            MessageBox.Show("No ha especificado una Direccion", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                            txtApellidos.Focus();
                        }
                        else
                        {
                            Add_(ruc, centrosalud, telefono, ubigeo, direccion);

                            if (this._clasePadre.Equals("ui_updregdescanso"))
                            {
                                ((ui_updregdescanso)FormPadre).txtEspecialidad.Text = centrosalud;
                            }
                            else
                            {
                                //((ui_invitados)FormPadre).btnActualizar.PerformClick();
                            }
                            this.Close();
                        }
                    }
                }
            }
        }

        private void Add_(string ruc, string centrosalud, string telefono, string ubigeo, string direccion)
        {
            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();

            string query = " INSERT INTO censalud VALUES ";
            query += "((SELECT ISNULL(MAX(ruc),0)+1 FROM censalud),'" + @centrosalud + "','" + @telefono + "','" + @ubigeo + "','" + @direccion + "','V');";

            try
            {
                SqlCommand myCommand = new SqlCommand(query, conexion);
                myCommand.ExecuteNonQuery();
                myCommand.Dispose();
            }
            catch (SqlException e)
            {
                MessageBox.Show(e.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }

            conexion.Close();
        }
        #endregion

        private void btnUbigeo_Click(object sender, EventArgs e)
        {
            _TextBoxDscUbigeo = txtDscUbigeo;
            _TextBoxUbigeo = txtcodUbigeo;
            ui_ubigeo ui_ubigeo = new ui_ubigeo();
            ui_ubigeo._FormPadre = this;
            ui_ubigeo._clasePadre = "ui_updcensalud";

            ui_ubigeo.ui_nuevaSeleccion();

            if (ui_ubigeo.ShowDialog(this) == DialogResult.OK)
            {
                txtdireccion.Focus();
            }
            else
            {
                txtdireccion.Focus();
            }
            ui_ubigeo.Dispose();
        }
    }
}