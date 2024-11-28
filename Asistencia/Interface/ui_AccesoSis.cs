using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Reflection;
using System.Configuration;
using System.Data.SqlClient;
using CaniaBrava.cs;
using CaniaBrava.Interface;




namespace CaniaBrava
{
    public partial class ui_AccesoSis : Form
    {
        int intentos = 0;

        public ui_AccesoSis()
        {
            InitializeComponent();
        }

        private void txtusuario_Enter(object sender, EventArgs e)
        {
            txtusuario.SelectAll();
        }

        private void txtusuario_TextChanged(object sender, EventArgs e)
        {

            txtpropietario.Text = string.Empty;
            lblCompania.Visible = false;
            cmbCompania.Visible = false;
            Funciones funciones = new Funciones();
            funciones.clearComboBox(cmbCompania);

        }

        private void txtclave_Enter(object sender, EventArgs e)
        {
            txtclave.SelectAll();
        }

        private void txtusuario_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                e.Handled = true;
                txtclave.Focus();
            }
        }

        private void txtclave_KeyPress(object sender, KeyPressEventArgs e)
        {
            string bd_prov = ConfigurationManager.AppSettings.Get("BD_PROV");
            if (e.KeyChar == '\r')
            {
                string usuarioActivo;
                string typeUsr;
                string squery;

                string usuario = txtusuario.Text.Trim();
                string clave = txtclave.Text.Trim();

                e.Handled = true;

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
                        if (intentos < 2)
                        {
                            UsrFile usrfile = new UsrFile();
                            usuarioActivo = usrfile.validaUsrFile(usuario, clave, "USUARIO");
                            typeUsr = usrfile.validaUsrFile(usuario, clave, "TIPO_USUARIO");

                            if (usuarioActivo == string.Empty)
                            {
                                intentos++;
                                MessageBox.Show("Acceso denegado al Sistema, Clave Incorrecta", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                                txtusuario.Text = string.Empty;
                                txtclave.Text = string.Empty;
                                txtpropietario.Text = string.Empty;
                                txtTypeUsr.Text = string.Empty;
                                lblCompania.Visible = false;
                                cmbCompania.Visible = false;
                                txtusuario.Focus();
                            }
                            else
                            {
                                if (typeUsr.Equals("00"))
                                {
                                    squery = "Select (idcia+'  '+ruccia) as clave,descia as descripcion from ciafile order by 1 asc;";
                                }
                                else
                                {
                                    squery = "Select (A.idcia+'  '+A.ruccia) as clave,A.descia as descripcion from ciafile A,ciausrfile B where A.idcia=B.idcia and B.idusr='" + @usuario + "'  order by 1 asc;";
                                }

                                Funciones funciones = new Funciones();
                                funciones.listaComboBox(squery, cmbCompania, "");
                                lblPropietario.Visible = true;
                                txtpropietario.Visible = true;
                                txtpropietario.Text = usuarioActivo;
                                txtTypeUsr.Text = typeUsr;
                                //lblCompania.Visible = true;
                                //cmbCompania.Visible = true;
                                //cmbCompania.Focus();

                                e.Handled = true;
                                toolStripForm.Items[0].Select();
                                toolStripForm.Focus();
                            }
                        }
                        else
                        {
                            MessageBox.Show("3 Intentos Fallados, Salida del Sistema por Seguridad", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            this.Close();
                        }
                    }
                }
            }
        }

        private void btnIniciar_Click(object sender, EventArgs e)
        {
            Funciones funciones = new Funciones();

            string usuarioActivo, nivelusr;
            string usuario = txtusuario.Text.Trim();
            string clave = txtclave.Text.Trim();
            string typeusr = txtTypeUsr.Text.Trim();
            string compania = funciones.getValorComboBox(cmbCompania, 2);

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
                    if (intentos < 2)
                    {
                        UsrFile usrfile = new UsrFile();
                        usuarioActivo = usrfile.validaUsrFile(usuario, clave, "USUARIO");
                        typeusr = usrfile.validaUsrFile(usuario, clave, "TIPO_USUARIO");
                        nivelusr = usrfile.validaUsrFile(usuario, clave, "NIVEL_USUARIO");

                        if (usuarioActivo == String.Empty)
                        {
                            intentos++;
                            MessageBox.Show("Acceso denegado al Sistema, Clave Incorrecta", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                            txtusuario.Text = string.Empty;
                            txtclave.Text = string.Empty;
                            txtpropietario.Text = string.Empty;
                            txtTypeUsr.Text = string.Empty;
                            lblCompania.Visible = false;
                            cmbCompania.Visible = false;
                            txtusuario.Focus();
                        }
                        else
                        {
                            string ruccia = "12345678901";//cmbCompania.Text.Substring(4, 11);
                            string namecia = "GRUPO CAÑA BRAVA"; //cmbCompania.Text.Substring(15, cmbCompania.Text.Length - 15);
                            string mail = usrfile.ui_getDatos(usuario, "EMAIL");
                            GlobalVariables variables = new GlobalVariables();
                            variables.setValores(compania, usuario, typeusr, ruccia, namecia, mail, usuarioActivo, nivelusr);
                            //  MessageBox.Show("Bienvenido al Sistema", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            this.Hide();
                            ui_mdiprincipal ui_mdisistema = new ui_mdiprincipal();
                            ui_mdisistema.usuarioActivo("USUARIO ACTIVO : " + usuarioActivo + "    COMPAÑÍA : GRUPO CAÑA BRAVA" /*+ cmbCompania.Text.Trim()*/);
                            ui_mdisistema.Show();


                            SqlConnection conexion = new SqlConnection();
                            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
                            {
                                conexion.Open();
                                string insertSql = "INSERT INTO logueolog (idusr) VALUES (@usuario)";
                                using (SqlCommand command = new SqlCommand(insertSql, conexion))
                                {
                                    command.Parameters.AddWithValue("@usuario", usuario);
                                    command.ExecuteNonQuery();
                                }
                            }

                        }
                    }
                    else
                    {
                        MessageBox.Show("3 Intentos Fallados, el Usuario se encuentra bloqueado, salida del sistema por seguridad", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);


                        SqlConnection conexion = new SqlConnection();
                        conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
                        {
                            conexion.Open();
                            string insertSql = "UPDATE usrfile SET stateusr = 'A' WHERE idusr = @usuario";
                            using (SqlCommand command = new SqlCommand(insertSql, conexion))
                            {
                                command.Parameters.AddWithValue("@usuario", usuario);
                                command.ExecuteNonQuery();
                            }
                        }


                        this.Close();
                    }
                }
            }
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtclave_TextChanged(object sender, EventArgs e)
        {
            txtpropietario.Text = string.Empty;
            txtTypeUsr.Text = string.Empty;
            lblCompania.Visible = false;
            cmbCompania.Visible = false;
            Funciones funciones = new Funciones();
            funciones.clearComboBox(cmbCompania);
        }

        private void cmbCompania_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                if (cmbCompania.Text != String.Empty)
                {
                    Funciones funcionusr = new Funciones();
                    string clave = funcionusr.getValorComboBox(cmbCompania, 2);

                    string squery = "SELECT CONCAT(idcia,'  ',ruccia) as clave,descia as descripcion FROM ciafile WHERE idcia='" + @clave + "';";
                    funcionusr.validarCombobox(squery, cmbCompania);
                }

                e.Handled = true;
                toolStripForm.Items[0].Select();
                toolStripForm.Focus();

            }
        }

        private void ui_AccesoSis_Load(object sender, EventArgs e)
        {
            GlobalVariables globalVariable = new GlobalVariables();
            lblregistrado.Text = globalVariable.getValorLicRazon();
            this.lblVersion.Text = String.Format("Versión {0}", Application.ProductVersion/*AssemblyVersion*/);
            txtusuario.Focus();
        }

        public string AssemblyVersion
        {
            get
            {
                return Assembly.GetExecutingAssembly().GetName().Version.ToString();
            }
        }
    }
}