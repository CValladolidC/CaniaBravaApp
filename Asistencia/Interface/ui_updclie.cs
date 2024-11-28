using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Configuration;

namespace CaniaBrava
{
    public partial class ui_updclie : Form
    {
        string _operacion;
        string _gencodigo;
        string _clasePadre;
        

        private Form FormPadre;

        public Form _FormPadre
        {
            get { return FormPadre; }
            set { FormPadre = value; }
        }

        public ui_updclie()
        {
            InitializeComponent();
        }

        public void setValores(string clasePadre)
        {
            this._clasePadre = clasePadre;
        }
       
        public void agregar()
        {
            GlobalVariables variables = new GlobalVariables();
            this._operacion = "AGREGAR";
            txtCodigo.Enabled = true;
            txtCodigo.Clear();
            txtRazon.Clear();
            txtRuc.Clear();
            txtDni.Clear();
            txtDir1.Clear();
            txtDir2.Clear();
            txtDir3.Clear();
            txtLocalidad.Clear();
            txtPais.Clear();
            txtNombre.Clear();
            txtCargo.Clear();
            txtCorreo.Clear();
            txtTel1.Clear();
            txtTel2.Clear();
            txtTel3.Clear();
            txtCargo.Clear();
            txtComentario.Clear();
            cmbEstado.Text = "V         VIGENTE";
            txtFecCrea.Text = DateTime.Now.ToString("dd/MM/yyyy");
            txtFecMod.Text = DateTime.Now.ToString("dd/MM/yyyy");
            txtUsuario.Text = variables.getValorUsr();
            
            DialogResult opcion = MessageBox.Show("¿Desea que se genere el Código de Cliente en forma automática?", "Aviso Importante", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (opcion == DialogResult.Yes)
            {
                this._gencodigo = "A";
                txtCodigo.Enabled = false;
                txtRazon.Focus();
            }
            else
            {
                this._gencodigo = "M";
                txtCodigo.Enabled = true;
                txtCodigo.Focus();
            }
           
        }

        public void editar(string codclie)
        {
            string query;
            this._operacion = "EDITAR";
            txtCodigo.Enabled = false;
            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();
            query = "SELECT * FROM clie where codclie='" + @codclie + "'; ";
            try
            {
                SqlCommand myCommand = new SqlCommand(query, conexion);
                SqlDataReader myReader = myCommand.ExecuteReader();

                if (myReader.Read())
                {
                    MaesGen maesgen = new MaesGen();
                    txtCodigo.Text = myReader.GetString(myReader.GetOrdinal("codclie"));
                    txtRazon.Text = myReader.GetString(myReader.GetOrdinal("desclie"));
                    txtRuc.Text = myReader.GetString(myReader.GetOrdinal("rucclie"));
                    txtDni.Text = myReader.GetString(myReader.GetOrdinal("dniclie"));
                    maesgen.consultaDetMaesGen("200", myReader.GetString(myReader.GetOrdinal("tippreven")), cmbTipoPrecioVen);
                    txtDir1.Text = myReader.GetString(myReader.GetOrdinal("dirclie1"));
                    txtDir2.Text = myReader.GetString(myReader.GetOrdinal("dirclie2"));
                    txtDir3.Text = myReader.GetString(myReader.GetOrdinal("dirclie3"));
                    txtLocalidad.Text = myReader.GetString(myReader.GetOrdinal("localidad"));
                    txtPais.Text = myReader.GetString(myReader.GetOrdinal("pais"));
                    txtNombre.Text = myReader.GetString(myReader.GetOrdinal("nomcontac"));
                    txtCorreo.Text = myReader.GetString(myReader.GetOrdinal("mailcontac"));
                    txtTel1.Text = myReader.GetString(myReader.GetOrdinal("telcontac1"));
                    txtTel2.Text = myReader.GetString(myReader.GetOrdinal("telcontac2"));
                    txtTel3.Text = myReader.GetString(myReader.GetOrdinal("telcontac3"));
                    txtCargo.Text = myReader.GetString(myReader.GetOrdinal("cargocontac"));
                    txtFax.Text = myReader.GetString(myReader.GetOrdinal("faxcontac"));
                    txtComentario.Text = myReader.GetString(myReader.GetOrdinal("comentario"));
                    txtFecCrea.Text = myReader.GetString(myReader.GetOrdinal("fcrea"));
                    txtFecMod.Text = myReader.GetString(myReader.GetOrdinal("fmod"));
                    txtUsuario.Text = myReader.GetString(myReader.GetOrdinal("usuario"));
                    switch (myReader.GetString(myReader.GetOrdinal("estado")))
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
                    txtRazon.Focus();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            Funciones funciones = new Funciones();
            Clie clie = new Clie();
            MaesGen maesgen = new MaesGen();
            GlobalVariables variables = new GlobalVariables();
            string operacion = this._operacion;
            string codclie;
            if (this._gencodigo == "A" && operacion == "AGREGAR")
            {
                codclie=clie.genCodClie();
                txtCodigo.Text = codclie;
            }
            else
            {
                codclie = txtCodigo.Text.Trim();
            }
            string desclie = txtRazon.Text;
            string rucclie = txtRuc.Text;
            string dniclie = txtDni.Text;
            string tippreven = funciones.getValorComboBox(cmbTipoPrecioVen, 4);
            string dirclie1 = txtDir1.Text;
            string dirclie2 = txtDir2.Text;
            string dirclie3 = txtDir3.Text;
            string localidad = txtLocalidad.Text;
            string pais = txtPais.Text;
            string nomcontac = txtNombre.Text;
            string mailcontac = txtCorreo.Text;
            string telcontac1 = txtTel1.Text;
            string telcontac2 = txtTel2.Text;
            string telcontac3 = txtTel3.Text;
            string cargocontac = txtCargo.Text;
            string faxcontac=txtFax.Text;
            string comentario = txtComentario.Text;
            string estado= funciones.getValorComboBox(cmbEstado, 1);
            string fcrea=txtFecCrea.Text;
            string fmod = DateTime.Now.ToString("dd/MM/yyyy");
            string usuario = variables.getValorUsr();
            string valorValida = "G";

            if (txtCodigo.Text == string.Empty && valorValida == "G")
            {
                valorValida = "B";
                MessageBox.Show("No ha ingresado Código", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtCodigo.Focus();
            }

            if (txtRazon.Text == string.Empty && valorValida == "G")
            {
                valorValida = "B";
                MessageBox.Show("No ha ingresado Razón Social", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtRazon.Focus();
            }

            if (txtRuc.Text != string.Empty && valorValida == "G" && this._operacion.Equals("AGREGAR"))
            {
                bool existencia=clie.verificaRucClie(txtRuc.Text);
                if (existencia)
                {
                    valorValida = "B";
                    MessageBox.Show("El Nro. de R.U.C. ingresado ya ha sido asignado anteriormente", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtRuc.Focus();
                }
            }

            if (cmbTipoPrecioVen.Text == string.Empty && valorValida == "G")
            {
                valorValida = "B";
                MessageBox.Show("No ha seleccionado Tipo de Precio de Venta", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cmbTipoPrecioVen.Focus();
            }
            if (cmbTipoPrecioVen.Text != string.Empty && valorValida == "G")
            {
                string resultado = maesgen.verificaComboBoxMaesGen("200", cmbTipoPrecioVen.Text.Trim());
                if (resultado.Trim() == string.Empty)
                {
                    valorValida = "B";
                    MessageBox.Show("Dato incorrecto en Tipo de Precio de Venta", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    cmbTipoPrecioVen.Focus();
                }
            }

            if (estado == string.Empty && valorValida == "G")
            {
                valorValida = "B";
                MessageBox.Show("No ha especificado Estado", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                cmbEstado.Focus();
            }

            if (estado != "V" && estado != "A" && valorValida == "G")
            {
                valorValida = "B";
                MessageBox.Show("Dato incorrecto en Estado", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                cmbEstado.Focus();
            }

            if (valorValida.Equals("G"))

            {
                clie.updClie(operacion,codclie,desclie,rucclie,dniclie,dirclie1,dirclie2,dirclie3,nomcontac,
                    localidad,pais,telcontac1,telcontac2,telcontac3,cargocontac,mailcontac,comentario,faxcontac,estado,
                    fcrea,fmod,usuario,tippreven);

                if (this._clasePadre.Equals("ui_clie"))
                {
                    ((ui_clie)FormPadre).btnActualizar.PerformClick();
                }

                if (this._clasePadre.Equals("ui_updfactura"))
                {
                    ((ui_updfactura)FormPadre).txtCliente.Text = codclie;
                    ((ui_updfactura)FormPadre).ui_ActualizarClie();
                }

                if (this._clasePadre.Equals("ui_updguiaremi"))
                {
                    ((ui_updguiaremi)FormPadre).txtCliente.Text = codclie;
                    ((ui_updguiaremi)FormPadre).ui_ActualizarClie();
                }

                if (this._clasePadre.Equals("ui_updfacguia"))
                {
                    ((ui_updfacguia)FormPadre).txtCliente.Text = codclie;
                    ((ui_updfacguia)FormPadre).ui_ActualizarClie();
                }

                //if (this._clasePadre.Equals("ui_updalmov"))
                //{
                //    ((ui_updalmov)FormPadre).txtCliente.Text = codclie;
                //    ((ui_updalmov)FormPadre).ui_ActualizarClie();
                //}

                MessageBox.Show("Información registrada correctamente", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }

        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void txtCodigo_KeyPress(object sender, KeyPressEventArgs e)
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
                txtRuc.Focus();
            }
        }

        private void txtRuc_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                e.Handled = true;
                txtDni.Focus();
            }
        }

        private void txtDni_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                e.Handled = true;
                txtDir1.Focus();
            }
        }

        private void txtDir1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                e.Handled = true;
                txtDir2.Focus();
            }
        }

        private void txtDir2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                e.Handled = true;
                txtDir3.Focus();
            }
        }

        private void txtDir3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                e.Handled = true;
                txtLocalidad.Focus();
            }
        }

        private void txtLocalidad_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                e.Handled = true;
                txtPais.Focus();
            }
        }

        private void txtPais_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                e.Handled = true;
                txtNombre.Focus();
            }
        }

        private void txtNombre_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                e.Handled = true;
                txtCorreo.Focus();
            }
        }

        private void txtCorreo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                e.Handled = true;
                txtTel1.Focus();
            }
        }

        private void txtTel1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                e.Handled = true;
                txtTel2.Focus();
            }
        }

        private void txtTel2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                e.Handled = true;
                txtTel3.Focus();
            }
        }

        private void txtTel3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                e.Handled = true;
                txtCargo.Focus();
            }
        }

        private void txtCargo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                e.Handled = true;
                txtFax.Focus();
            }
        }

        private void txtFax_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                e.Handled = true;
                txtComentario.Focus();
            }
        }

        private void txtComentario_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                e.Handled = true;
                cmbEstado.Focus();
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

        public void ui_actualizaComboBox()
        {
            MaesGen maesgen = new MaesGen();
            maesgen.listaDetMaesGen("200", cmbTipoPrecioVen, "B");
        }

        private void ui_updclie_Load(object sender, EventArgs e)
        {

        }
       
    }
}
