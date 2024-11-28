using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace CaniaBrava
{
    public partial class ui_updmedicos : Form
    {
        Funciones funciones = new Funciones();
        private TextBox TextBoxActivo;
        private Form FormPadre;
        private string IsTrabajador;
        string _clasePadre;

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

        public ui_updmedicos()
        {
            InitializeComponent();

            this.IsTrabajador = string.Empty;

            if (funciones.VersionAssembly()) Application.ExitThread();
        }

        public void New_()
        {
            txtDNI.Focus();
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
                txtespecialidad.Focus();
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
            string tipo = (rdCMP.Checked ? "CMP" : "COP");
            string cmp = txtDNI.Text.Trim();
            string nombres = txtNombres.Text.Trim();
            string apellidos = txtApellidos.Text.Trim();
            string especiali = txtespecialidad.Text.Trim();

            if (cmp == string.Empty)
            {
                MessageBox.Show("No ha especificado CMP", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
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
                        if (especiali == String.Empty)
                        {
                            MessageBox.Show("No ha especificado una Especialidad", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                            txtApellidos.Focus();
                        }
                        else
                        {
                            Add_(tipo, cmp, nombres, apellidos, especiali);

                            //if (this._clasePadre.Equals("ui_buscarmedicos"))
                            //{
                            //    ((ui_buscarmedicos)FormPadre).txtCMP.Text = cmp;
                            //    ((ui_buscarmedicos)FormPadre).txtMedico.Text = nombres + " " + apellidos;
                            //    ((ui_buscarmedicos)FormPadre).txtEspecialidad.Text = especiali;
                            //}
                            //else
                            //{
                            //    ((ui_invitados)FormPadre).btnActualizar.PerformClick();
                            //}
                            this.Close();
                        }
                    }
                }
            }
        }

        private void Add_(string tipo, string cmp, string nombres, string apellidos, string especiali)
        {
            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();

            string query = " INSERT INTO medicos VALUES ";
            query += "('" + @tipo + "','" + @cmp + "','" + @nombres + "','" + @apellidos + "','" + @especiali + "','V');";

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
    }
}