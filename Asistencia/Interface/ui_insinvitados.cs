using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace CaniaBrava
{
    public partial class ui_insinvitados : Form
    {
        Funciones funciones = new Funciones();
        private TextBox TextBoxActivo;
        private Form FormPadre;
        private string IsTrabajador;

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

        public ui_insinvitados()
        {
            InitializeComponent();

            this.IsTrabajador = string.Empty;

            if (funciones.VersionAssembly()) Application.ExitThread();
        }

        public void newInvitado()
        {
            cmbEstado.Text = "I           INVITADO";
            txtDNI.Focus();
            SendKeys.Send("{TAB}");
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
                    cmbEstado.Text = "T         TRABAJADOR DE LA EMPRESA";
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
            string dni = txtDNI.Text.Trim();
            string nombres = txtNombres.Text.Trim();
            string apellidos = txtApellidos.Text.Trim();
            string stateUsr = cmbEstado.Text.Substring(0, 1);

            if (dni == string.Empty)
            {
                MessageBox.Show("No ha especificado un DNI", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                txtDNI.Focus();
            }
            else
            {
                if (nombres == String.Empty)
                {
                    MessageBox.Show("No ha especificado un Nombre", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    txtNombres.Focus();
                }
                else
                {
                    if (apellidos == String.Empty)
                    {
                        MessageBox.Show("No ha especificado Apellidos", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        txtApellidos.Focus();
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
                            AddInvitado(dni, nombres, apellidos, stateUsr);
                            ((ui_invitados)FormPadre).btnActualizar.PerformClick();
                            this.Close();
                        }
                    }
                }
            }
        }

        private void AddInvitado(string dni, string nombres, string apellidos, string stateUsr)
        {
            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();

            string query = " INSERT INTO invitados VALUES ";
            query += "('" + @dni + "','" + @nombres + "','" + @apellidos + "','" + @stateUsr + "','V');";

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

        private void txtDNI_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                GlobalVariables gv = new GlobalVariables();
                Funciones fn = new Funciones();
                this._TextBoxActivo = txtDNI;
                string cadenaBusqueda = string.Empty;

                FiltrosMaestros filtros = new FiltrosMaestros();
                filtros.filtrarPerPlan("ui_insinvitados", this, txtDNI, cadenaBusqueda);
            }
        }

        private void ui_insinvitados_Load(object sender, EventArgs e)
        {

        }
    }
}