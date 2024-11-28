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
    public partial class ui_updprovee : Form
    {
        Funciones funciones = new Funciones();
        string _operacion;
        string _gencodigo;
        string _clasePadre;
        public string _tipo;

        private Form FormPadre;

        public Form _FormPadre
        {
            get { return FormPadre; }
            set { FormPadre = value; }
        }

        public ui_updprovee()
        {
            InitializeComponent();

            if (funciones.VersionAssembly()) Application.ExitThread();
        }

        private void groupBox3_Enter(object sender, EventArgs e)
        {

        }

        private void ui_updprovee_Load(object sender, EventArgs e)
        {

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
            txtDirLegal.Clear();
            txtWeb.Clear();
            cmbEstado.Text = "V         VIGENTE";
            txtFecCrea.Text = DateTime.Now.ToString("dd/MM/yyyy");
            txtFecMod.Text = DateTime.Now.ToString("dd/MM/yyyy");
            txtUsuario.Text = variables.getValorUsr();
            tabControlEmp.SelectTab(0);
            DialogResult opcion = MessageBox.Show("¿Desea que se genere el Código del Proveedor en forma automática?", "Aviso Importante", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
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

        public void editar(string codprovee)
        {
            this._operacion = "EDITAR";
            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();
            string query = "SELECT * FROM provee where codprovee='" + @codprovee + "'; ";
            try
            {
                SqlCommand myCommand = new SqlCommand(query, conexion);
                SqlDataReader myReader = myCommand.ExecuteReader();

                if (myReader.Read())
                {
                    txtCodigo.Enabled = false;
                    tabPageEX2.Enabled = true;
                    MaesGen maesgen = new MaesGen();
                    string titulo = myReader.GetString(myReader.GetOrdinal("ruc")) + "   " + myReader.GetString(myReader.GetOrdinal("desprovee"));
                    lblNombre.Text = titulo.Trim();
                    txtCodigo.Text = myReader.GetString(myReader.GetOrdinal("codprovee"));
                    txtRuc.Text = myReader.GetString(myReader.GetOrdinal("ruc"));
                    txtDirLegal.Text = myReader.GetString(myReader.GetOrdinal("dirprovee"));
                    txtRazon.Text = myReader.GetString(myReader.GetOrdinal("desprovee"));
                    txtWeb.Text = myReader.GetString(myReader.GetOrdinal("website"));
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
                    ui_listaEstAnePro(codprovee);
                    tabControlEmp.SelectTab(0);
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
            Provee provee = new Provee();
            GlobalVariables variables = new GlobalVariables();
            string operacion = this._operacion;
            string codprovee;
            if (this._gencodigo == "A" && operacion == "AGREGAR")
            {
                codprovee = provee.genCodProvee();
                txtCodigo.Text = codprovee;
            }
            else
            {
                codprovee = txtCodigo.Text.Trim();
            }
            string desprovee = txtRazon.Text.Trim();
            string ruc = txtRuc.Text.Trim();
            string dirprovee = txtDirLegal.Text.Trim();
            string website = txtWeb.Text.Trim();
            string estado = funciones.getValorComboBox(cmbEstado, 1);
            string fcrea = txtFecCrea.Text;
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
                bool existencia = provee.verificaRuc(txtRuc.Text);
                if (existencia)
                {
                    valorValida = "B";
                    MessageBox.Show("El Nro. de R.U.C. ingresado ya ha sido asignado anteriormente", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtRuc.Focus();
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

            if (website == string.Empty && valorValida == "G")
            {
                valorValida = "B";
                MessageBox.Show("No ha especificado un Email", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                cmbEstado.Focus();
            }

            if (website != string.Empty && valorValida == "G")
            {
                if (!Funciones.IsValidEmail(txtWeb.Text))
                {
                    valorValida = "B";
                    MessageBox.Show("Email incorrecto.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    txtWeb.Focus();
                }
            }

            if (valorValida.Equals("G"))
            {
                provee.updProvee(operacion, codprovee, desprovee, ruc, website, fcrea, fmod, usuario, estado, dirprovee);
                MessageBox.Show("Información registrada correctamente", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);

                if (this._tipo.Equals("F"))
                {
                    ((ui_provee)FormPadre).btnActualizar.PerformClick();
                    this._operacion = "EDITAR";
                    tabPageEX2.Enabled = true;
                }
                else
                {
                    if (this._clasePadre.Equals("ui_updalmov"))
                    {
                        ((ui_updalmov)FormPadre).txtProveedor.Text = codprovee;
                        ((ui_updalmov)FormPadre).ui_ActualizarProvee();
                    }
                    this.Close();
                }
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnNuevoEstAne_Click(object sender, EventArgs e)
        {
            ui_activarEstAnePro(true);
            ui_clearEstAnePro();
            txtOpeEsta.Text = "AGREGAR";
            txtCodigoEsta.Focus();
        }

        private void btnEditarEstAne_Click(object sender, EventArgs e)
        {
            MaesGen maesgen = new MaesGen();
            Int32 selectedCellCount = dgvEstAne.GetCellCount(DataGridViewElementStates.Selected);
            if (selectedCellCount > 0)
            {
                string idestane = dgvEstAne.Rows[dgvEstAne.SelectedCells[1].RowIndex].Cells[0].Value.ToString();
                string desestane = dgvEstAne.Rows[dgvEstAne.SelectedCells[1].RowIndex].Cells[2].Value.ToString();
                string tipoestane = dgvEstAne.Rows[dgvEstAne.SelectedCells[1].RowIndex].Cells[3].Value.ToString();
                string codprovee = dgvEstAne.Rows[dgvEstAne.SelectedCells[1].RowIndex].Cells[4].Value.ToString();
                string direstane = dgvEstAne.Rows[dgvEstAne.SelectedCells[1].RowIndex].Cells[5].Value.ToString();
                string nomcontac = dgvEstAne.Rows[dgvEstAne.SelectedCells[1].RowIndex].Cells[6].Value.ToString();
                string mailcontac = dgvEstAne.Rows[dgvEstAne.SelectedCells[1].RowIndex].Cells[7].Value.ToString();
                string localidad = dgvEstAne.Rows[dgvEstAne.SelectedCells[1].RowIndex].Cells[8].Value.ToString();
                string pais = dgvEstAne.Rows[dgvEstAne.SelectedCells[1].RowIndex].Cells[9].Value.ToString();
                string telcontac1 = dgvEstAne.Rows[dgvEstAne.SelectedCells[1].RowIndex].Cells[10].Value.ToString();
                string telcontac2 = dgvEstAne.Rows[dgvEstAne.SelectedCells[1].RowIndex].Cells[11].Value.ToString();
                string telcontac3 = dgvEstAne.Rows[dgvEstAne.SelectedCells[1].RowIndex].Cells[12].Value.ToString();
                string cargocontac = dgvEstAne.Rows[dgvEstAne.SelectedCells[1].RowIndex].Cells[13].Value.ToString();
                string comentario = dgvEstAne.Rows[dgvEstAne.SelectedCells[1].RowIndex].Cells[14].Value.ToString();
                string faxcontac = dgvEstAne.Rows[dgvEstAne.SelectedCells[1].RowIndex].Cells[15].Value.ToString();

                txtOpeEsta.Text = "EDITAR";
                txtCodigoEsta.Text = idestane;
                ui_activarEstAnePro(true);
                txtCodigoEsta.Enabled = false;
                txtNombreEsta.Text = desestane;
                maesgen.consultaDetMaesGen("100", tipoestane, cmbTipoEsta);
                txtDireccion.Text = direstane;
                txtNombre.Text = nomcontac;
                txtCorreo.Text = mailcontac;
                txtLocalidad.Text = localidad;
                txtPais.Text = pais;
                txtTel1.Text = telcontac1;
                txtTel2.Text = telcontac2;
                txtTel3.Text = telcontac3;
                txtCargo.Text = cargocontac;
                txtComentario.Text = comentario;
                txtFax.Text = faxcontac;
                txtNombreEsta.Focus();
            }
            else
            {
                MessageBox.Show("No ha seleccionado registro a Editar", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        public void ui_listarComboBox()
        {
            MaesGen maesgen = new MaesGen();
            maesgen.listaDetMaesGen("100", cmbTipoEsta, "B");
        }

        private void btnGrabarEstAne_Click(object sender, EventArgs e)
        {
            if (this._operacion.Trim().Equals("EDITAR"))
            {

                try
                {
                    Funciones funciones = new Funciones();
                    MaesGen maesgen = new MaesGen();
                    EstAnePro estanepro = new EstAnePro();

                    string operacion = txtOpeEsta.Text.Trim();
                    string idestane = txtCodigoEsta.Text.Trim();
                    string desestane = txtNombreEsta.Text.Trim();
                    string tipoestane = funciones.getValorComboBox(cmbTipoEsta, 4);
                    string codprovee = txtCodigo.Text.Trim();
                    string direstane = txtDireccion.Text;
                    string nomcontac = txtNombre.Text;
                    string mailcontac = txtCorreo.Text;
                    string localidad = txtLocalidad.Text;
                    string pais = txtPais.Text;
                    string telcontac1 = txtTel1.Text;
                    string telcontac2 = txtTel2.Text;
                    string telcontac3 = txtTel3.Text;
                    string cargocontac = txtCargo.Text;
                    string comentario = txtComentario.Text;
                    string faxcontac = txtFax.Text;

                    string valorValida = "G";

                    if (txtCodigoEsta.Text == string.Empty && valorValida == "G")
                    {
                        valorValida = "B";
                        MessageBox.Show("No ha ingresado Código de Establecimiento", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txtCodigoEsta.Focus();
                    }

                    if (txtNombreEsta.Text == string.Empty && valorValida == "G")
                    {
                        valorValida = "B";
                        MessageBox.Show("No ha ingresado Nombre de Establecimiento", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txtNombreEsta.Focus();
                    }

                    if (cmbTipoEsta.Text == string.Empty && valorValida == "G")
                    {
                        valorValida = "B";
                        MessageBox.Show("No ha seleccionado Tipo de Establecimiento", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        cmbTipoEsta.Focus();
                    }

                    if (cmbTipoEsta.Text != string.Empty && valorValida == "G")
                    {

                        string resultado = maesgen.verificaComboBoxMaesGen("100", cmbTipoEsta.Text.Trim());
                        if (resultado.Trim() == string.Empty)
                        {
                            valorValida = "B";
                            MessageBox.Show("Dato incorrecto en Tipo de Establecimiento", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            tabControlEmp.SelectTab(1);
                            cmbTipoEsta.Focus();
                        }
                    }
                    if (valorValida.Equals("G"))
                    {
                        estanepro.updEstAne(operacion, idestane, codprovee, desestane, tipoestane, direstane, nomcontac,
                            mailcontac, localidad, pais, telcontac1, telcontac2, telcontac3, cargocontac, comentario, faxcontac);
                        ui_listaEstAnePro(codprovee);
                        ui_clearEstAnePro();
                        ui_activarEstAnePro(false);
                    }
                }
                catch (FormatException ex)
                {
                    MessageBox.Show(ex.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Primero debe de grabar la información del Empleador", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void ui_clearEstAnePro()
        {

            txtOpeEsta.Clear();
            txtCodigoEsta.Clear();
            txtNombreEsta.Clear();
            cmbTipoEsta.Text = string.Empty;
            txtDireccion.Clear();
            txtNombre.Clear();
            txtCorreo.Clear();
            txtLocalidad.Clear();
            txtPais.Clear();
            txtTel1.Clear();
            txtTel2.Clear();
            txtTel3.Clear();
            txtCargo.Clear();
            txtComentario.Clear();
            txtFax.Clear();

        }

        public void ui_activarEstAnePro(bool estado)
        {
            txtCodigoEsta.Enabled = estado;
            txtNombreEsta.Enabled = estado;
            cmbTipoEsta.Enabled = estado;
            txtDireccion.Enabled = estado;
            txtNombre.Enabled = estado;
            txtCorreo.Enabled = estado;
            txtLocalidad.Enabled = estado;
            txtPais.Enabled = estado;
            txtTel1.Enabled = estado;
            txtTel2.Enabled = estado;
            txtTel3.Enabled = estado;
            txtCargo.Enabled = estado;
            txtComentario.Enabled = estado;
            txtFax.Enabled = estado;
        }

        public void ui_listaEstAnePro(string codprovee)
        {
            Funciones funciones = new Funciones();
            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();
            string query = " select A.idestane,B.desmaesgen,A.desestane,A.tipoestane,A.codprovee, ";
            query = query + " A.direstane,A.nomcontac,A.mailcontac,A.localidad,A.pais,A.telcontac1,";
            query = query + " A.telcontac2,A.telcontac3,A.cargocontac,A.comentario,A.faxcontac ";
            query = query + " from EstAnePro A left join MaesGen B on A.tipoestane=B.clavemaesgen ";
            query = query + " and B.idmaesgen='100' where codprovee='" + @codprovee + "' ";
            query = query + " order by A.idestane asc;";
            try
            {
                using (SqlDataAdapter myDataAdapter = new SqlDataAdapter(query, conexion))
                {
                    DataSet myDataSet = new DataSet();
                    myDataAdapter.Fill(myDataSet, "tblEstAne");
                    funciones.formatearDataGridView(dgvEstAne);

                    dgvEstAne.DataSource = myDataSet.Tables["tblEstAne"];
                    dgvEstAne.Columns[0].HeaderText = "Código Estab.";
                    dgvEstAne.Columns[1].HeaderText = "Tipo de Establecimiento";
                    dgvEstAne.Columns[2].HeaderText = "Denominación";
                    dgvEstAne.Columns["tipoestane"].Visible = false;
                    dgvEstAne.Columns["codprovee"].Visible = false;
                    dgvEstAne.Columns["direstane"].Visible = false;
                    dgvEstAne.Columns["nomcontac"].Visible = false;
                    dgvEstAne.Columns["mailcontac"].Visible = false;
                    dgvEstAne.Columns["localidad"].Visible = false;
                    dgvEstAne.Columns["pais"].Visible = false;
                    dgvEstAne.Columns["telcontac1"].Visible = false;
                    dgvEstAne.Columns["telcontac2"].Visible = false;
                    dgvEstAne.Columns["telcontac3"].Visible = false;
                    dgvEstAne.Columns["cargocontac"].Visible = false;
                    dgvEstAne.Columns["comentario"].Visible = false;
                    dgvEstAne.Columns["faxcontac"].Visible = false;

                    dgvEstAne.Columns[0].Width = 100;
                    dgvEstAne.Columns[1].Width = 150;
                    dgvEstAne.Columns[2].Width = 280;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }



            conexion.Close();


        }

        private void btnEliminarEstAne_Click(object sender, EventArgs e)
        {
            //EstAnePro estanepro = new EstAnePro();
            Int32 selectedCellCount = dgvEstAne.GetCellCount(DataGridViewElementStates.Selected);
            if (selectedCellCount > 0)
            {
                string idestane = dgvEstAne.Rows[dgvEstAne.SelectedCells[1].RowIndex].Cells[0].Value.ToString();
                string desestane = dgvEstAne.Rows[dgvEstAne.SelectedCells[1].RowIndex].Cells[2].Value.ToString();
                string codprovee = dgvEstAne.Rows[dgvEstAne.SelectedCells[1].RowIndex].Cells[4].Value.ToString();
                DialogResult resultado = MessageBox.Show("¿Desea eliminar el Establecimiento " + desestane + "?",
                "Consulta Importante",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);
                if (resultado == DialogResult.Yes)
                {
                    //estanepro.delEstAne(idestane, codprovee);
                    ui_listaEstAnePro(codprovee);
                }
            }
            else
            {
                MessageBox.Show("No ha seleccionado registro a Eliminar", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void txtCodigoEsta_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                e.Handled = true;
                txtNombreEsta.Focus();
            }
        }

        private void txtNombreEsta_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                e.Handled = true;
                cmbTipoEsta.Focus();
            }
        }

        private void cmbTipoEsta_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                if (cmbTipoEsta.Text != String.Empty)
                {
                    MaesGen maesgen = new MaesGen();
                    maesgen.validarDetMaesGen("100", cmbTipoEsta, cmbTipoEsta.Text);
                    txtDireccion.Focus();
                }
            }
        }

        private void txtDireccion_KeyPress(object sender, KeyPressEventArgs e)
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
            //Para obligar a que sólo se introduzcan números
            if (Char.IsDigit(e.KeyChar))
            {
                e.Handled = false;
            }
            else
              if (Char.IsControl(e.KeyChar)) //permitir teclas de control como retroceso
            {
                e.Handled = false;
            }
            else
            {
                //el resto de teclas pulsadas se desactivan
                e.Handled = true;
            }
            if (e.KeyChar == '\r')
            {
                e.Handled = true;
                txtTel2.Focus();
            }
        }

        private void txtTel2_KeyPress(object sender, KeyPressEventArgs e)
        {
            //Para obligar a que sólo se introduzcan números
            if (Char.IsDigit(e.KeyChar))
            {
                e.Handled = false;
            }
            else
              if (Char.IsControl(e.KeyChar)) //permitir teclas de control como retroceso
            {
                e.Handled = false;
            }
            else
            {
                //el resto de teclas pulsadas se desactivan
                e.Handled = true;
            }
            if (e.KeyChar == '\r')
            {
                e.Handled = true;
                txtTel3.Focus();
            }
        }

        private void txtTel3_KeyPress(object sender, KeyPressEventArgs e)
        {
            //Para obligar a que sólo se introduzcan números
            if (Char.IsDigit(e.KeyChar))
            {
                e.Handled = false;
            }
            else
              if (Char.IsControl(e.KeyChar)) //permitir teclas de control como retroceso
            {
                e.Handled = false;
            }
            else
            {
                //el resto de teclas pulsadas se desactivan
                e.Handled = true;
            }
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

        private void txtComentario_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                e.Handled = true;
                btnGrabarEstAne.Focus();
            }
        }

        private void txtFax_KeyPress(object sender, KeyPressEventArgs e)
        {
            //Para obligar a que sólo se introduzcan números
            if (Char.IsDigit(e.KeyChar))
            {
                e.Handled = false;
            }
            else
              if (Char.IsControl(e.KeyChar)) //permitir teclas de control como retroceso
            {
                e.Handled = false;
            }
            else
            {
                //el resto de teclas pulsadas se desactivan
                e.Handled = true;
            }
            if (e.KeyChar == '\r')
            {
                e.Handled = true;
                txtComentario.Focus();
            }
        }

        private void txtRazon_TextChanged(object sender, EventArgs e)
        {
            string razon;
            razon = txtRuc.Text.Trim() + "   " + txtRazon.Text.Trim();
            lblNombre.Text = razon.Trim();
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
                txtDirLegal.Focus();
            }
        }

        private void txtWeb_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                if (Funciones.IsValidEmail(txtWeb.Text))
                {
                    e.Handled = true;
                    cmbEstado.Focus();
                }
                else
                {
                    MessageBox.Show("Email incorrecto.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
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

        private void txtDirLegal_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                e.Handled = true;
                txtWeb.Focus();
            }
        }
    }
}