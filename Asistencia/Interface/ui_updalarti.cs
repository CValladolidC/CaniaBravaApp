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
using System.Text.RegularExpressions;

namespace CaniaBrava
{
    public partial class ui_updalarti : Form
    {
        Funciones funciones = new Funciones();
        public string _codcia;
        private string _operacion;
        private string _gencodigo;
        public string _clasePadre;
        string _desarti;
        private const char SignoDecimal = '.'; // Carácter separador decimal
        private string _prevTextBoxValue; // Variable que almacena el valor anterior del Textbox

        private Form FormPadre;

        public Form _FormPadre
        {
            get { return FormPadre; }
            set { FormPadre = value; }
        }

        public ui_updalarti()
        {
            InitializeComponent();

            if (funciones.VersionAssembly()) Application.ExitThread();
        }

        public void ui_ActualizaComboBox()
        {
            MaesGen maesgen = new MaesGen();
            maesgen.listaDetMaesGen("121", cmbUndAltura, "B");
            maesgen.listaDetMaesGen("121", cmbUndAncho, "B");
            maesgen.listaDetMaesGen("121", cmbUndLargo, "B");
            maesgen.listaDetMaesGen("121", cmbUndDiametro, "B");

            maesgen.listaDetMaesGen("122", cmbUndPeso, "B");

            maesgen.listaDetMaesGen("120", cmbUnidad, "B");
            maesgen.listaDetMaesGen("110", cmbFamilia, "B");
            maesgen.listaDetMaesGen("130", cmbClasificacion, "B");
            maesgen.listaDetMaesGen("140", cmbTipo, "B");

            dpFechaini.MinDate = DateTime.Now;
        }

        public void agregar()
        {
            GlobalVariables variables = new GlobalVariables();
            this._operacion = "AGREGAR";
            lblNombre.Text = "";
            txtCodigo.Clear();
            txtBarras.Clear();
            txtProducto.Clear();
            txtEspecie.Clear();
            txtMarca.Clear();
            txtSerie.Clear();
            cmbUnidad.Text = "";
            cmbFamilia.Text = "";
            cmbGrupo.Text = "";
            cmbTipo.Text = "";
            cmbClasificacion.Text = "";

            txtLargo.Clear();
            txtAncho.Clear();
            txtAltura.Clear();
            txtPeso.Clear();
            txtDiametro.Clear();

            txtPrecio.Text = "0.00";
            cmbEstado.Text = "V         VIGENTE";
            txtFecCrea.Text = DateTime.Now.ToString("dd/MM/yyyy");
            txtFecMod.Text = DateTime.Now.ToString("dd/MM/yyyy");
            txtUsuario.Text = variables.getValorUsr();
            tabControl.SelectTab(0);
            DialogResult opcion = MessageBox.Show("¿Desea que se genere el Código Interno en forma automática?", "Aviso Importante", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (opcion == DialogResult.Yes)
            {
                this._gencodigo = "A";
                txtCodigo.Enabled = false;
                txtProducto.Focus();
            }
            else
            {
                this._gencodigo = "M";
                txtCodigo.Enabled = true;
                txtCodigo.Focus();
            }
        }

        private string ui_emsamblaNombreProducto()
        {
            string nombre = string.Empty;

            nombre = String.Concat(nombre, " ", txtProducto.Text.Trim());

            if (chkEspecie.Checked)
            {
                nombre = String.Concat(nombre, " ", txtEspecie.Text.Trim());
            }
            if (chkSerie.Checked)
            {
                nombre = String.Concat(nombre, " ", txtSerie.Text.Trim());
            }
            if (chkMarca.Checked)
            {
                nombre = String.Concat(nombre, " ", txtMarca.Text.Trim());
            }
            if (chkLargo.Checked)
            {
                nombre = String.Concat(nombre, " LARGO: ", txtLargo.Text.Trim(), " ", funciones.getValorComboBox(cmbUndLargo, 4).Trim());
            }
            if (chkAncho.Checked)
            {
                nombre = String.Concat(nombre, " ANCHO: ", txtAncho.Text.Trim(), " ", funciones.getValorComboBox(cmbUndAncho, 4).Trim());
            }
            if (chkAltura.Checked)
            {
                nombre = String.Concat(nombre, " ALTURA: ", txtAltura.Text.Trim(), " ", funciones.getValorComboBox(cmbUndAltura, 4).Trim());
            }
            if (chkPeso.Checked)
            {
                nombre = String.Concat(nombre, " PESO: ", txtPeso.Text.Trim(), " ", funciones.getValorComboBox(cmbUndPeso, 4).Trim());
            }

            if (chkDiametro.Checked)
            {
                nombre = String.Concat(nombre, " DIAMETRO: ", txtDiametro.Text.Trim(), " ", funciones.getValorComboBox(cmbUndDiametro, 4).Trim());
            }

            this._desarti = nombre.Trim();

            return nombre.Trim();
        }

        public void editar(string codarti)
        {
            string query;
            this._operacion = "EDITAR";
            this._gencodigo = "M";
            txtCodigo.Enabled = false;
            string codcia = this._codcia;

            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();
            query = "SELECT * FROM alarti where /*codcia='" + @codcia + "' and */codarti='" + @codarti + "'; ";

            try
            {
                SqlCommand myCommand = new SqlCommand(query, conexion);
                SqlDataReader myReader = myCommand.ExecuteReader();

                if (myReader.Read())
                {
                    MaesGen maesgen = new MaesGen();
                    lblNombre.Text = myReader.GetString(myReader.GetOrdinal("codarti")) + " - " + myReader.GetString(myReader.GetOrdinal("desarti"));
                    txtCodigo.Text = myReader.GetString(myReader.GetOrdinal("codarti"));
                    txtBarras.Text = myReader.GetString(myReader.GetOrdinal("codartiba"));

                    txtProducto.Text = myReader.GetString(myReader.GetOrdinal("nombre"));

                    if (myReader.GetString(myReader.GetOrdinal("isserie")).Equals("S"))
                    {
                        chkSerie.Checked = true;
                        txtSerie.Text = myReader.GetString(myReader.GetOrdinal("codigo"));
                    }
                    else
                    {
                        chkSerie.Checked = false;
                    }

                    if (myReader.GetString(myReader.GetOrdinal("isespecie")).Equals("S"))
                    {
                        chkEspecie.Checked = true;
                        txtEspecie.Text = myReader.GetString(myReader.GetOrdinal("especie"));
                    }
                    else
                    {
                        chkEspecie.Checked = false;
                    }

                    if (myReader.GetString(myReader.GetOrdinal("ismarca")).Equals("S"))
                    {
                        chkMarca.Checked = true;
                        txtMarca.Text = myReader.GetString(myReader.GetOrdinal("marca"));
                    }
                    else
                    {
                        chkMarca.Checked = false;
                    }

                    if (myReader.GetString(myReader.GetOrdinal("islargo")).Equals("S"))
                    {
                        chkLargo.Checked = true;
                        txtLargo.Text = myReader.GetString(myReader.GetOrdinal("largo"));
                        maesgen.consultaDetMaesGen("121", myReader.GetString(myReader.GetOrdinal("ulargo")), cmbUndLargo);
                    }
                    else
                    {
                        chkLargo.Checked = false;
                    }

                    if (myReader.GetString(myReader.GetOrdinal("ispeso")).Equals("S"))
                    {
                        chkPeso.Checked = true;
                        txtPeso.Text = myReader.GetString(myReader.GetOrdinal("peso"));
                        maesgen.consultaDetMaesGen("122", myReader.GetString(myReader.GetOrdinal("upeso")), cmbUndPeso);
                    }
                    else
                    {
                        chkPeso.Checked = false;
                    }

                    if (myReader.GetString(myReader.GetOrdinal("isancho")).Equals("S"))
                    {
                        chkAncho.Checked = true;
                        txtAncho.Text = myReader.GetString(myReader.GetOrdinal("ancho"));
                        maesgen.consultaDetMaesGen("121", myReader.GetString(myReader.GetOrdinal("uancho")), cmbUndPeso);
                    }
                    else
                    {
                        chkAncho.Checked = false;
                    }

                    if (myReader.GetString(myReader.GetOrdinal("isdiametro")).Equals("S"))
                    {
                        chkDiametro.Checked = true;
                        txtDiametro.Text = myReader.GetString(myReader.GetOrdinal("diametro"));
                        maesgen.consultaDetMaesGen("121", myReader.GetString(myReader.GetOrdinal("udiametro")), cmbUndPeso);
                    }
                    else
                    {
                        chkDiametro.Checked = false;
                    }

                    if (myReader.GetString(myReader.GetOrdinal("isaltura")).Equals("S"))
                    {
                        chkAltura.Checked = true;
                        txtAltura.Text = myReader.GetString(myReader.GetOrdinal("altura"));
                        maesgen.consultaDetMaesGen("121", myReader.GetString(myReader.GetOrdinal("ualtura")), cmbUndPeso);
                    }
                    else
                    {
                        chkAltura.Checked = false;
                    }

                    maesgen.consultaDetMaesGen("110", myReader.GetString(myReader.GetOrdinal("famarti")), cmbFamilia);
                    query = " Select grparti as clave,desgrparti as descripcion from grparti where ";
                    query = query + " famarti='" + myReader.GetString(myReader.GetOrdinal("famarti")) + "' and ";
                    query = query + " grparti='" + myReader.GetString(myReader.GetOrdinal("grparti")) + "';";
                    funciones.consultaComboBox(query, cmbGrupo);
                    maesgen.consultaDetMaesGen("120", myReader.GetString(myReader.GetOrdinal("unidad")), cmbUnidad);
                    maesgen.consultaDetMaesGen("130", myReader.GetString(myReader.GetOrdinal("clasarti")), cmbClasificacion);
                    maesgen.consultaDetMaesGen("140", myReader.GetString(myReader.GetOrdinal("tipoarti")), cmbTipo);
                    //txtPrecio.Text = myReader.GetDouble(myReader.GetOrdinal("prepubli")).ToString();
                    //txtPreDistri.Text = myReader.GetDouble(myReader.GetOrdinal("predistri")).ToString();

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
                }
                tabPageEX3.Enabled = true;
                btnGrabar.Enabled = true;
                tabControl.SelectTab(0);
                txtProducto.Focus();
                ui_listaVariacionPrecios(txtCodigo.Text.Trim());
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }

            conexion.Close();
        }

        private string valida()
        {
            MaesGen maesgen = new MaesGen();
            string estado = funciones.getValorComboBox(cmbEstado, 1);
            string valorValida = "G";

            if (this._gencodigo.Equals("M"))
            {
                if (txtCodigo.Text == string.Empty && valorValida == "G")
                {
                    valorValida = "B";
                    MessageBox.Show("No ha ingresado Código", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    tabControl.SelectTab(0);
                    txtCodigo.Focus();
                }
            }

            if (txtProducto.Text == string.Empty && valorValida == "G")
            {
                valorValida = "B";
                MessageBox.Show("No ha ingresado Nombre Descriptivo", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                tabControl.SelectTab(0);
                txtProducto.Focus();
            }

            if (chkEspecie.Checked)
            {
                if (txtEspecie.Text == string.Empty && valorValida == "G")
                {
                    valorValida = "B";
                    MessageBox.Show("No ha ingresado Especie del producto", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    tabControl.SelectTab(0);
                    txtEspecie.Focus();
                }
            }

            if (chkSerie.Checked)
            {
                if (txtSerie.Text == string.Empty && valorValida == "G")
                {
                    valorValida = "B";
                    MessageBox.Show("No ha ingresado Serie / Modelo del producto", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    tabControl.SelectTab(0);
                    txtSerie.Focus();
                }
            }

            if (chkMarca.Checked)
            {
                if (txtMarca.Text == string.Empty && valorValida == "G")
                {
                    valorValida = "B";
                    MessageBox.Show("No ha ingresado Marca del producto", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    tabControl.SelectTab(0);
                    txtMarca.Focus();
                }
            }

            if (chkLargo.Checked)
            {
                if (txtLargo.Text == string.Empty && valorValida == "G")
                {
                    valorValida = "B";
                    MessageBox.Show("No ha ingresado Largo del producto", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    tabControl.SelectTab(0);
                    txtLargo.Focus();
                }

                if (cmbUndLargo.Text.Trim() == string.Empty && valorValida == "G")
                {
                    valorValida = "B";
                    MessageBox.Show("No ha seleccionado Unidad para el Largo del Producto", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    tabControl.SelectTab(0);
                    cmbUndLargo.Focus();
                }

            }

            if (chkAncho.Checked)
            {
                if (txtAncho.Text == string.Empty && valorValida == "G")
                {
                    valorValida = "B";
                    MessageBox.Show("No ha ingresado Ancho del producto", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    tabControl.SelectTab(0);
                    txtAncho.Focus();
                }

                if (cmbUndAncho.Text.Trim() == string.Empty && valorValida == "G")
                {
                    valorValida = "B";
                    MessageBox.Show("No ha seleccionado Unidad para el Ancho del Producto", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    tabControl.SelectTab(0);
                    cmbUndAncho.Focus();
                }

            }

            if (chkAltura.Checked)
            {
                if (txtAltura.Text == string.Empty && valorValida == "G")
                {
                    valorValida = "B";
                    MessageBox.Show("No ha ingresado Altura del producto", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    tabControl.SelectTab(0);
                    txtAltura.Focus();
                }

                if (cmbUndAltura.Text.Trim() == string.Empty && valorValida == "G")
                {
                    valorValida = "B";
                    MessageBox.Show("No ha seleccionado Unidad para la Altura del Producto", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    tabControl.SelectTab(0);
                    cmbUndAltura.Focus();
                }

            }


            if (chkDiametro.Checked)
            {
                if (txtDiametro.Text == string.Empty && valorValida == "G")
                {
                    valorValida = "B";
                    MessageBox.Show("No ha ingresado Diámetro del producto", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    tabControl.SelectTab(0);
                    txtDiametro.Focus();
                }

                if (cmbUndDiametro.Text.Trim() == string.Empty && valorValida == "G")
                {
                    valorValida = "B";
                    MessageBox.Show("No ha seleccionado Unidad para el Diámetro del Producto", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    tabControl.SelectTab(0);
                    cmbUndDiametro.Focus();
                }

            }


            if (chkPeso.Checked)
            {
                if (txtPeso.Text == string.Empty && valorValida == "G")
                {
                    valorValida = "B";
                    MessageBox.Show("No ha ingresado Peso del producto", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    tabControl.SelectTab(0);
                    txtPeso.Focus();
                }

                if (cmbUndPeso.Text.Trim() == string.Empty && valorValida == "G")
                {
                    valorValida = "B";
                    MessageBox.Show("No ha seleccionado Unidad para el Peso del Producto", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    tabControl.SelectTab(0);
                    cmbUndPeso.Focus();
                }

            }

            if (cmbUnidad.Text == string.Empty && valorValida == "G")
            {
                valorValida = "B";
                MessageBox.Show("No ha seleccionado Unidad", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                tabControl.SelectTab(0);
                cmbUnidad.Focus();
            }

            if (cmbUnidad.Text != string.Empty && valorValida == "G")
            {
                string resultado = maesgen.verificaComboBoxMaesGen("120", cmbUnidad.Text.Trim());
                if (resultado.Trim() == string.Empty)
                {
                    valorValida = "B";
                    MessageBox.Show("Dato incorrecto en Unidad", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    tabControl.SelectTab(0);
                    cmbUnidad.Focus();
                }
            }

            if (cmbFamilia.Text == string.Empty && valorValida == "G")
            {
                valorValida = "B";
                MessageBox.Show("No ha seleccionado Familia", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                tabControl.SelectTab(0);
                cmbFamilia.Focus();
            }

            if (cmbFamilia.Text != string.Empty && valorValida == "G")
            {
                string resultado = maesgen.verificaComboBoxMaesGen("110", cmbFamilia.Text.Trim());
                if (resultado.Trim() == string.Empty)
                {
                    valorValida = "B";
                    MessageBox.Show("Dato incorrecto en Familia", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    tabControl.SelectTab(0);
                    cmbFamilia.Focus();
                }
            }

            if (cmbGrupo.Text == string.Empty && valorValida == "G")
            {
                valorValida = "B";
                MessageBox.Show("No ha seleccionado Grupo", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                tabControl.SelectTab(0);
                cmbGrupo.Focus();
            }

            if (cmbGrupo.Text != string.Empty && valorValida == "G")
            {
                string famarti = funciones.getValorComboBox(cmbFamilia, 4);
                string grparti = funciones.getValorComboBox(cmbGrupo, 2);
                string query = " SELECT grparti as clave,desgrparti as descripcion FROM grparti where ";
                query = query + " famarti='" + @famarti + "' and grparti='" + @grparti + "' ";
                string resultado = funciones.verificaItemComboBox(query, cmbGrupo);

                if (resultado.Trim() == string.Empty)
                {
                    valorValida = "B";
                    MessageBox.Show("Dato incorrecto en Grupo", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    tabControl.SelectTab(0);
                    cmbGrupo.Focus();
                }
            }

            if (cmbClasificacion.Text == string.Empty && valorValida == "G")
            {
                valorValida = "B";
                MessageBox.Show("No ha seleccionado Clasificación", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                tabControl.SelectTab(0);
                cmbClasificacion.Focus();
            }

            if (cmbClasificacion.Text != string.Empty && valorValida == "G")
            {
                string resultado = maesgen.verificaComboBoxMaesGen("130", cmbClasificacion.Text.Trim());
                if (resultado.Trim() == string.Empty)
                {
                    valorValida = "B";
                    MessageBox.Show("Dato incorrecto en Clasificación", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    tabControl.SelectTab(0);
                    cmbClasificacion.Focus();
                }
            }

            //if (cmbTipo.Text == string.Empty && valorValida == "G")
            //{
            //    valorValida = "B";
            //    MessageBox.Show("No ha seleccionado Tipo", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    tabControl.SelectTab(0);
            //    cmbTipo.Focus();
            //}

            //if (cmbTipo.Text != string.Empty && valorValida == "G")
            //{
            //    string resultado = maesgen.verificaComboBoxMaesGen("140", cmbTipo.Text.Trim());
            //    if (resultado.Trim() == string.Empty)
            //    {
            //        valorValida = "B";
            //        MessageBox.Show("Dato incorrecto en Tipo", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //        tabControl.SelectTab(0);
            //        cmbTipo.Focus();
            //    }
            //}

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
            return valorValida;
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            string valorValida = valida();
            if (valorValida.Equals("G"))
            {
                try
                {
                    AlArti alarti = new AlArti();
                    GlobalVariables variables = new GlobalVariables();
                    string operacion = this._operacion;
                    string codcia = this._codcia;
                    string codarti;
                    string letra = txtProducto.Text.Substring(0, 1);
                    string codartiba = txtBarras.Text;
                    string nombre = txtProducto.Text.Trim();

                    ////////////////////////////////////////////////////////////////////////////////////////////////////////
                    //////////////////////////////ESTRUCTURA DEL NOMBRE DEL PRODUCTO ///////////////////////////////////////
                    ////////////////////////////////////////////////////////////////////////////////////////////////////////

                    string especie = string.Empty;
                    string isespecie = "N";
                    string codigo = string.Empty;
                    string isserie = "N";
                    string marca = string.Empty;
                    string ismarca = "N";
                    if (chkEspecie.Checked)
                    {
                        especie = txtEspecie.Text.Trim();
                        isespecie = "S";
                    }

                    if (chkSerie.Checked)
                    {
                        codigo = txtSerie.Text.Trim();
                        isserie = "S";
                    }

                    if (chkMarca.Checked)
                    {
                        marca = txtMarca.Text.Trim();
                        ismarca = "S";
                    }

                    ////////////////////////////////////////////////////////////////////////////////////////////////////////
                    ///////////////////////////////////////////////DIMENSIONES DEL PRODUCTO////////////////////////////////
                    ////////////////////////////////////////////////////////////////////////////////////////////////////////

                    float ancho = 0;
                    float largo = 0;
                    float peso = 0;
                    float diametro = 0;
                    float altura = 0;

                    string isancho = "N";
                    string islargo = "N";
                    string ispeso = "N";
                    string isdiametro = "N";
                    string isaltura = "N";
                    string monpreven = "MN";

                    string uancho = string.Empty;
                    string ulargo = string.Empty;
                    string upeso = string.Empty;
                    string udiametro = string.Empty;
                    string ualtura = string.Empty;

                    if (chkAncho.Checked)
                    {
                        ancho = float.Parse(txtAncho.Text);
                        isancho = "S";
                        uancho = funciones.getValorComboBox(cmbUndAncho, 4);
                    }

                    if (chkLargo.Checked)
                    {
                        largo = float.Parse(txtLargo.Text);
                        islargo = "S";
                        ulargo = funciones.getValorComboBox(cmbUndLargo, 4);
                    }

                    if (chkPeso.Checked)
                    {
                        peso = float.Parse(txtPeso.Text);
                        ispeso = "S";
                        upeso = funciones.getValorComboBox(cmbUndPeso, 4);
                    }

                    if (chkDiametro.Checked)
                    {
                        diametro = float.Parse(txtDiametro.Text);
                        isdiametro = "S";
                        udiametro = funciones.getValorComboBox(cmbUndDiametro, 4);
                    }

                    if (chkAltura.Checked)
                    {
                        altura = float.Parse(txtAltura.Text);
                        isaltura = "S";
                        ualtura = funciones.getValorComboBox(cmbUndAltura, 4);
                    }

                    string desarti = this._desarti;

                    string famarti = funciones.getValorComboBox(cmbFamilia, 4);
                    string grparti = funciones.getValorComboBox(cmbGrupo, 2);
                    string unidad = funciones.getValorComboBox(cmbUnidad, 4);
                    string clasarti = funciones.getValorComboBox(cmbClasificacion, 4);
                    string tipoarti = funciones.getValorComboBox(cmbTipo, 4);


                    float prepubli = 0;// float.Parse(txtPrecio.Text);
                    float predistri = 0;// float.Parse(txtPreDistri.Text);
                    string controlstk = string.Empty;
                    string prelibre = string.Empty;
                    string exoigv = string.Empty;
                    string esporigv = string.Empty;
                    string porigv = string.Empty;

                    if (this._gencodigo == "A" && operacion == "AGREGAR")
                    {
                        codarti = alarti.genCodArti(codcia, grparti, letra);
                        txtCodigo.Text = codarti;
                    }
                    else
                    {
                        codarti = txtCodigo.Text.Trim();
                    }

                    string estado = funciones.getValorComboBox(cmbEstado, 1);
                    string fcrea = txtFecCrea.Text;
                    string fmod = DateTime.Now.ToString("dd/MM/yyyy");
                    string usuario = variables.getValorUsr();

                    alarti.updAlArti(operacion, codcia, codarti, codartiba, nombre, especie, codigo, marca, desarti,
                        unidad, ancho, largo, peso, altura, diametro, clasarti, famarti, grparti, tipoarti,
                        fcrea, fmod, usuario, estado, prepubli, predistri, 0, controlstk, prelibre, exoigv,
                        monpreven, esporigv, porigv, ulargo, uancho, upeso, ualtura, udiametro, isespecie,
                        isserie, ismarca, islargo, isaltura, isancho, ispeso, isdiametro, 0);

                    if (this._clasePadre.Equals("ui_almovd"))
                    {
                        ((ui_almovd)FormPadre).txtCodigo.Text = codarti;
                        ((ui_almovd)FormPadre).ui_consultaProducto(codcia, codarti);
                        ((ui_almovd)FormPadre).txtCodigo.Focus();
                    }

                    if (this._clasePadre.Equals("ui_alarti"))
                    {
                        ((ui_alarti)FormPadre).btnActualizar.PerformClick();
                    }

                    if (this._clasePadre.Equals("ui_updguiaremi"))
                    {
                        ((ui_updguiaremi)FormPadre).txtCodigo.Text = codarti;
                        ((ui_updguiaremi)FormPadre).ui_consultaProducto(codcia, codarti);
                        ((ui_updguiaremi)FormPadre).txtCodigo.Focus();
                    }

                    MessageBox.Show("Información registrada correctamente", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    this.Close();
                    tabPageEX3.Enabled = true;
                    btnGrabar.Enabled = true;
                    tabControl.SelectTab(1);
                    txtPrecio.Focus();
                }
                catch (FormatException ex)
                {
                    MessageBox.Show(ex.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Close();
            if (this._clasePadre.Equals("ui_alarti"))
            {
                ((ui_alarti)FormPadre).btnActualizar.PerformClick();
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

        private void txtCodigo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(Keys.Enter))
            {
                e.Handled = true;
                SendKeys.Send("{TAB}");
            }
        }

        private void txtNombre_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(Keys.Enter))
            {
                e.Handled = true;
                SendKeys.Send("{TAB}");
            }
        }

        private void cmbUnidad_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(Keys.Enter))
            {
                if (cmbUnidad.Text != String.Empty)
                {
                    MaesGen maesgen = new MaesGen();
                    maesgen.validarDetMaesGen("120", cmbUnidad, cmbUnidad.Text);
                }
                e.Handled = true;
                SendKeys.Send("{TAB}");
            }
        }

        private void cmbFamilia_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(Keys.Enter))
            {
                if (cmbFamilia.Text != String.Empty)
                {
                    MaesGen maesgen = new MaesGen();
                    maesgen.validarDetMaesGen("110", cmbFamilia, cmbFamilia.Text);
                }
                string famarti = funciones.getValorComboBox(cmbFamilia, 4);
                string query = "SELECT grparti as clave,desgrparti as descripcion FROM grparti ";
                query = query + " WHERE famarti='" + @famarti + "' ;";
                funciones.listaComboBox(query, cmbGrupo, "B");
                e.Handled = true;
                SendKeys.Send("{TAB}");
            }
        }

        private void cmbGrupo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(Keys.Enter))
            {
                if (cmbGrupo.Text != String.Empty)
                {
                    string grparti = funciones.getValorComboBox(cmbGrupo, 2);
                    string famarti = funciones.getValorComboBox(cmbFamilia, 4);
                    string query = " SELECT grparti as clave,desgrparti as descripcion FROM grparti ";
                    query = query + " WHERE grparti='" + @grparti + "' and famarti='" + @famarti + "';";
                    funciones.validarCombobox(query, cmbGrupo);
                }

                e.Handled = true;
                SendKeys.Send("{TAB}");
            }
        }

        private void cmbClasificacion_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(Keys.Enter))
            {
                if (cmbClasificacion.Text != String.Empty)
                {
                    MaesGen maesgen = new MaesGen();
                    maesgen.validarDetMaesGen("130", cmbClasificacion, cmbClasificacion.Text);
                }
                e.Handled = true;
                SendKeys.Send("{TAB}");
            }
        }

        private void cmbTipo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(Keys.Enter))
            {
                if (cmbTipo.Text != String.Empty)
                {
                    MaesGen maesgen = new MaesGen();
                    maesgen.validarDetMaesGen("140", cmbTipo, cmbTipo.Text);
                }
                e.Handled = true;
                SendKeys.Send("{TAB}");
            }
        }

        private void txtAncho_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(Keys.Enter))
            {
                e.Handled = true;
                SendKeys.Send("{TAB}");
            }
        }

        private void txtLargo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(Keys.Enter))
            {
                e.Handled = true;
                SendKeys.Send("{TAB}");
            }
        }

        private void txtPeso_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(Keys.Enter))
            {
                e.Handled = true;
                SendKeys.Send("{TAB}");
            }
        }

        private void txtVolumen_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(Keys.Enter))
            {
                e.Handled = true;
                SendKeys.Send("{TAB}");
            }
        }

        private void txtArea_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(Keys.Enter))
            {
                e.Handled = true;
                SendKeys.Send("{TAB}");
            }
        }

        private void cmbFamilia_SelectedIndexChanged(object sender, EventArgs e)
        {
            string famarti = funciones.getValorComboBox(cmbFamilia, 4);
            string query = " SELECT grparti as clave,desgrparti as descripcion FROM grparti ";
            query = query + " WHERE famarti='" + @famarti + "' ;";
            funciones.listaComboBox(query, cmbGrupo, "B");
            SendKeys.Send("{TAB}");
        }

        private void txtBarras_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(Keys.Enter))
            {
                e.Handled = true;
                SendKeys.Send("{TAB}");
            }
        }

        private void txtPorDesc_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(Keys.Enter))
            {
                e.Handled = true;
                SendKeys.Send("{TAB}");
            }
        }

        private void tabPageEX3_Click(object sender, EventArgs e)
        {

        }

        private void tabPageEX2_Click(object sender, EventArgs e)
        {

        }

        private void tabPageEX1_Click(object sender, EventArgs e)
        {

        }

        private void txtDiametro_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(Keys.Enter))
            {
                e.Handled = true;
                SendKeys.Send("{TAB}");
            }
        }

        private void txtAltura_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }

        private void txtAltura_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(Keys.Enter))
            {
                e.Handled = true;
                SendKeys.Send("{TAB}");
            }
        }

        private void txtProducto_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(Keys.Enter))
            {
                e.Handled = true;
                SendKeys.Send("{TAB}");
            }
        }

        private void txtEspecie_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(Keys.Enter))
            {
                e.Handled = true;
                SendKeys.Send("{TAB}");
            }
        }

        private void txtSerie_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(Keys.Enter))
            {
                e.Handled = true;
                SendKeys.Send("{TAB}");
            }
        }

        private void txtMarca_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(Keys.Enter))
            {
                e.Handled = true;
                SendKeys.Send("{TAB}");
            }
        }

        private void chkSerie_CheckedChanged(object sender, EventArgs e)
        {
            if (chkSerie.Checked)
            {
                txtSerie.Enabled = true;
                txtSerie.Focus();
            }
            else
            {
                txtSerie.Enabled = false;
                txtSerie.Clear();
            }
        }

        private void chkLargo_CheckedChanged(object sender, EventArgs e)
        {
            if (chkLargo.Checked)
            {
                txtLargo.Enabled = true;
                cmbUndLargo.Enabled = true;
                txtLargo.Focus();
            }
            else
            {
                txtLargo.Enabled = false;
                cmbUndLargo.Enabled = false;
                cmbUndLargo.Text = funciones.replicateCadena(" ", 30);
                txtLargo.Clear();
            }
        }

        private void chkAncho_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAncho.Checked)
            {
                txtAncho.Enabled = true;
                cmbUndAncho.Enabled = true;
                txtAncho.Focus();
            }
            else
            {
                txtAncho.Enabled = false;
                cmbUndAncho.Text = funciones.replicateCadena(" ", 30);
                cmbUndAncho.Enabled = false;
                txtAncho.Clear();
            }
        }

        private void chkDiametro_CheckedChanged(object sender, EventArgs e)
        {
            if (chkDiametro.Checked)
            {
                txtDiametro.Enabled = true;
                cmbUndDiametro.Enabled = true;
                txtDiametro.Focus();
            }
            else
            {
                txtDiametro.Enabled = false;
                cmbUndDiametro.Text = funciones.replicateCadena(" ", 30);
                cmbUndDiametro.Enabled = false;
                txtDiametro.Clear();
            }
        }

        private void chkAltura_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAltura.Checked)
            {
                txtAltura.Enabled = true;
                cmbUndAltura.Enabled = true;
                txtAltura.Focus();
            }
            else
            {
                txtAltura.Enabled = false;
                cmbUndAltura.Enabled = false;
                cmbUndAltura.Text = funciones.replicateCadena(" ", 30);
                txtAltura.Clear();
            }
        }

        private void chkPeso_CheckedChanged(object sender, EventArgs e)
        {
            if (chkPeso.Checked)
            {
                txtPeso.Enabled = true;
                cmbUndPeso.Enabled = true;
                txtPeso.Focus();
            }
            else
            {
                txtPeso.Enabled = false;
                cmbUndPeso.Enabled = false;
                cmbUndPeso.Text = funciones.replicateCadena(" ", 30);
                txtPeso.Clear();
            }
        }

        private void chkMarca_CheckedChanged(object sender, EventArgs e)
        {
            if (chkMarca.Checked)
            {
                txtMarca.Enabled = true;
                txtMarca.Focus();
            }
            else
            {
                txtMarca.Enabled = false;
                txtMarca.Clear();
            }
        }

        private void chkEspecie_CheckedChanged(object sender, EventArgs e)
        {
            if (chkEspecie.Checked)
            {
                txtEspecie.Enabled = true;
                txtEspecie.Focus();
            }
            else
            {
                txtEspecie.Enabled = false;
                txtEspecie.Clear();
            }
        }

        private void txtProducto_TextChanged(object sender, EventArgs e)
        {
            lblNombre.Text = ui_emsamblaNombreProducto();
        }

        private void txtEspecie_TextChanged(object sender, EventArgs e)
        {
            lblNombre.Text = ui_emsamblaNombreProducto();
        }

        private void txtSerie_TextChanged(object sender, EventArgs e)
        {
            lblNombre.Text = ui_emsamblaNombreProducto();
        }

        private void txtMarca_TextChanged(object sender, EventArgs e)
        {
            lblNombre.Text = ui_emsamblaNombreProducto();
        }

        private void txtLargo_TextChanged(object sender, EventArgs e)
        {
            lblNombre.Text = ui_emsamblaNombreProducto();
        }

        private void txtAncho_TextChanged(object sender, EventArgs e)
        {
            lblNombre.Text = ui_emsamblaNombreProducto();
        }

        private void txtAltura_TextChanged(object sender, EventArgs e)
        {
            lblNombre.Text = ui_emsamblaNombreProducto();
        }

        private void txtDiametro_TextChanged(object sender, EventArgs e)
        {
            lblNombre.Text = ui_emsamblaNombreProducto();
        }

        private void txtPeso_TextChanged(object sender, EventArgs e)
        {
            lblNombre.Text = ui_emsamblaNombreProducto();
        }

        private void cmbUndAncho_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblNombre.Text = ui_emsamblaNombreProducto();
        }

        private void cmbUndAltura_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblNombre.Text = ui_emsamblaNombreProducto();
        }

        private void cmbUndDiametro_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblNombre.Text = ui_emsamblaNombreProducto();
        }

        private void cmbUndPeso_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblNombre.Text = ui_emsamblaNombreProducto();
        }

        private void cmbUndLargo_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblNombre.Text = ui_emsamblaNombreProducto();
        }

        private void txtPesoKg_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(Keys.Enter))
            {
                e.Handled = true;
                SendKeys.Send("{TAB}");
            }
        }

        public void ui_listaVariacionPrecios(string codarti)
        {
            string query = "select CONVERT(VARCHAR(10),A.fechaini,120) AS [Fecha Inicio Precio],CONVERT(VARCHAR(10),A.fechafin,120) AS [Fecha Fin Precio],A.Precio ";
            query += " from alarti_peri A (NOLOCK) ";
            query += " inner join alarti B (NOLOCK) on A.codarti=B.codarti AND B.codarti = '" + @codarti + "'";
            query += " order by A.fechaini asc;";

            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();

            try
            {
                using (SqlDataAdapter myDataAdapter = new SqlDataAdapter(query, conexion))
                {
                    DataSet myDataSet = new DataSet();
                    myDataAdapter.Fill(myDataSet, "tabla");

                    funciones.formatearDataGridView(gvVariacionPrecios);
                    gvVariacionPrecios.DataSource = myDataSet.Tables["tabla"];

                    gvVariacionPrecios.Columns[0].Width = 150;
                    gvVariacionPrecios.Columns[1].Width = 150;
                    gvVariacionPrecios.Columns[2].Width = 120;

                    gvVariacionPrecios.AllowUserToResizeRows = false;
                    gvVariacionPrecios.AllowUserToResizeColumns = false;
                    foreach (DataGridViewColumn column in gvVariacionPrecios.Columns)
                    {
                        column.SortMode = DataGridViewColumnSortMode.NotSortable;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }

            conexion.Close();
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            dpFechaini.Enabled = true;
            txtPrecio.Enabled = true;
            btnGrabar.Enabled = true;
            txtPrecio.Focus();
        }

        private void btnGrabar_Click(object sender, EventArgs e)
        {
            string valorValida = "G";
            string fechaini = dpFechaini.Value.ToString("yyyy-MM-dd");
            string preciopu = txtPrecio.Text.Trim();

            if ((preciopu == string.Empty || preciopu == "0") && valorValida == "G")
            {
                valorValida = "B";
                MessageBox.Show("Precio Publico no válido", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtPrecio.Focus();
            }

            if (valorValida.Equals("G"))
            {
                string query;
                query = " IF (SELECT COUNT(1) FROM alarti_peri WHERE codarti = '" + txtCodigo.Text.Trim() + "' AND fechafin IS NULL) > 0  ";
                query += "BEGIN UPDATE alarti_peri SET fechafin = '" + @fechaini + "' WHERE codarti = '" + txtCodigo.Text.Trim() + "'; END";
                query += " INSERT INTO alarti_peri ";
                query += "VALUES ('" + txtCodigo.Text.Trim() + "', '" + @fechaini + "', NULL, '" + preciopu + "');";

                SqlConnection conexion = new SqlConnection();
                conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
                conexion.Open();

                try
                {
                    SqlCommand myCommand = new SqlCommand(query, conexion);
                    myCommand.ExecuteNonQuery();
                    myCommand.Dispose();

                    ui_listaVariacionPrecios(txtCodigo.Text.Trim());

                    dpFechaini.Enabled = false;
                    txtPrecio.Clear();
                    txtPrecio.Enabled = false;
                    btnGrabar.Enabled = false;
                }
                catch (SqlException ex)
                {
                    MessageBox.Show(ex.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
                conexion.Close();
            }
        }

        private void txtPrecio_TextChanged(object sender, EventArgs e)
        {
            var textBox = (TextBox)sender;
            // Comprueba si el valor del TextBox se ajusta a un valor válido
            if (Regex.IsMatch(textBox.Text, @"^(?:\d+\.?\d*)?$"))
            {
                // Si es válido se almacena el valor actual en la variable privada
                _prevTextBoxValue = textBox.Text;
            }
            else
            {
                // Si no es válido se recupera el valor de la variable privada con el valor anterior
                // Calcula el nº de caracteres después del cursor para dejar el cursor en la misma posición
                var charsAfterCursor = textBox.TextLength - textBox.SelectionStart - textBox.SelectionLength;
                // Recupera el valor anterior
                textBox.Text = _prevTextBoxValue;
                // Posiciona el cursor en la misma posición
                textBox.SelectionStart = Math.Max(0, textBox.TextLength - charsAfterCursor);
            }
        }

        private void txtPrecio_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(Keys.Enter))
            {
                var textBox = (TextBox)sender;
                // Si el carácter pulsado no es un carácter válido se anula
                e.Handled = !char.IsDigit(e.KeyChar) // No es dígito
                            && !char.IsControl(e.KeyChar) // No es carácter de control (backspace)
                            && (e.KeyChar != SignoDecimal // No es signo decimal o es la 1ª posición o ya hay un signo decimal
                                || textBox.SelectionStart == 0
                                || textBox.Text.Contains(SignoDecimal));

                btnGrabar.Focus();
            }
        }
    }
}