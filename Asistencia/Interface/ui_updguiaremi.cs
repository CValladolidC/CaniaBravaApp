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
    public partial class ui_updguiaremi : Form
    {
        string _operacion;
        string _numdoc;
        string _codcia;
        string _codcaja;
        string _descaja;
        string _descia;
        string _finaliza;
        string _codmov;
        string _alma;
        string _opeitem;
        string _item=string.Empty;
       

        private TextBox TextBoxActivo;
        private Form FormPadre;

        public Form _FormPadre
        {
            get { return FormPadre; }
            set { FormPadre = value; }
        }

        public TextBox _TextBoxActivo
        {
            get { return TextBoxActivo; }
            set { TextBoxActivo = value; }
        }

        public ui_updguiaremi()
        {
            InitializeComponent();
        }

        private void ui_updguiaremi_Load(object sender, EventArgs e)
        {
            this.Text = "Guía de Remisión "+this._descia+ " Caja : "+this._descaja;
            this.ui_listarPunPar(this._codcia);
        }


        public void ui_ActualizarClie()
        {
            Clie clie = new Clie();
            string codclie = txtCliente.Text.Trim();
            string nombre = clie.ui_getDatos(codclie, "NOMBRE");
            if (nombre == string.Empty)
            {
                txtCliente.Text = string.Empty;
                txtRazonDesti.Text = string.Empty;
                txtRucDesti.Text = string.Empty;

            }
            else
            {
                txtCliente.Text = clie.ui_getDatos(codclie, "CODIGO");
                txtRazonDesti.Text = clie.ui_getDatos(codclie, "NOMBRE");
                txtRucDesti.Text = clie.ui_getDatos(codclie, "RUC");
            }
        }

        public void ui_listarPunPar(string idcia)
        {
            
            string query = "Select codpartida as clave,despartida as descripcion ";
            query = query + "from punpar where idcia='"+@idcia+"' order by 1 asc;";
            Funciones funciones = new Funciones();
            funciones.listaComboBox(query, cmbPartida, "");

        }

        public void ui_listarPunClie(string codclie)
        {

            string query = "Select codpartida as clave,despartida as descripcion ";
            query = query + "from punclie where codclie='" + @codclie + "' order by 1 asc;";
            Funciones funciones = new Funciones();
            funciones.listaComboBox(query, cmbLlegada, "");
                    
        }

        public void ui_listarEstaClie(string codclie)
        {
            string query = "Select codesta as clave,desesta as descripcion ";
            query = query + "from estaclie where codclie='" + @codclie + "' order by 1 asc;";
            Funciones funciones = new Funciones();
            funciones.listaComboBox(query, cmbTienda, "");
        }


        public void ui_listaLotes(string codcia, string alma, string codarti)
        {
            Funciones funciones = new Funciones();
            CiaFile ciafile = new CiaFile();
            string kardex = ciafile.ui_getDatosCiaFile(codcia, "KARDEX");
            string ordena = string.Empty;

            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();

            if (kardex.Equals("FIFO"))
            {
                ordena = " order by B.fecdoc asc ";
            }
            else
            {
                if (kardex.Equals("LIFO"))
                {
                    ordena = " order by B.fecdoc desc ";
                }
                else
                {
                    ordena = " order by B.fecdoc asc ";
                }
            }

            string query = " select A.lote,B.fecdoc,A.stock,B.lotep,B.fprod,B.fven ";
            query = query + " from alsklote A  left join vista_lotes B on A.codcia=B.codcia ";
            query = query + " and A.alma=B.alma and A.codarti=B.codarti and A.lote=B.lote ";
            query = query + " where A.codcia='" + @codcia + "' and A.alma='" + @alma + "' and A.codarti='" + @codarti + "' ";
            query = query + " and A.stock>0 " + @ordena + " ;";

            try
            {
                using (SqlDataAdapter myDataAdapter = new SqlDataAdapter(query, conexion))
                {
                    DataSet myDataSet = new DataSet();
                    myDataAdapter.Fill(myDataSet, "tblData");
                    funciones.formatearDataGridView(dgvStock);
                    dgvStock.DataSource = myDataSet.Tables["tblData"];
                    dgvStock.Columns[0].HeaderText = "Lote Sistema";
                    dgvStock.Columns[1].HeaderText = "Fecha de Ingreso del Lote";
                    dgvStock.Columns[2].HeaderText = "Stock Disponible";
                    dgvStock.Columns[3].HeaderText = "Lote Produccion";
                    dgvStock.Columns[4].HeaderText = "Fecha Prod.";
                    dgvStock.Columns[5].HeaderText = "Fecha Venc.";
                    dgvStock.Columns[0].Width = 120;
                    dgvStock.Columns[1].Width = 100;
                    dgvStock.Columns[1].Width = 100;
                    dgvStock.Columns[3].Width = 120;
                    dgvStock.Columns[4].Width = 100;
                    dgvStock.Columns[5].Width = 100;
                }

                SqlCommand myCommand = new SqlCommand(query, conexion);
                DataTable dt = new DataTable();
                dt.Load(myCommand.ExecuteReader());
                myCommand.Dispose();
                cmbLote.Items.Clear();
                string valorini = "";
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        if (i == 0)
                        {
                            valorini = (String)dt.Rows[i]["lote"];
                        }
                        cmbLote.Items.Add((String)dt.Rows[i]["lote"]);
                    }
                    cmbLote.Text = valorini;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            conexion.Close();
        }

        
        public void setData(string codcia, string codcaja,
            string descaja, string descia)
        {
            this._codcia = codcia;
            this._codcaja = codcaja;
            this._descaja = descaja;
            this._descia = descia;

        }

        private void agregarItem()
        {
            this._opeitem = "AGREGAR";
            ui_limpiarItem();
            ui_habilitarItem(true);
            txtPesoUni.Text = "0";
            txtCantidad.Text = "0";
            txtPeso.Text = "0";
            txtCodigo.Focus();

        }

        private void ui_habilitarItem(bool estado)
        {
            txtCodigo.Enabled = estado;
            txtCantidad.Enabled = estado;
         
        }

        private void ui_limpiarItem()
        {
            txtCodigo.Text = string.Empty;
            txtDescri.Text = string.Empty;
            txtUnidad.Text = string.Empty;
            txtPesoUni.Text = string.Empty;
            txtCantidad.Text = string.Empty;
            txtPeso.Text = string.Empty;
            ui_listaLotes("","", "");
        }

        public void ui_actualizaComboBox()
        {
            MaesGen maesgen = new MaesGen();
            maesgen.listaDetMaesGen("220", cmbDocIdenDesti, "B");
            maesgen.listaDetMaesGen("210", cmbMotivoTraslado, "");
        }
        
        public void agregar()
        {
            Funciones funciones = new Funciones();
            string codcia = this._codcia;
            string codcaja = this._codcaja;
            this._finaliza = "N";
            this._operacion = "AGREGAR";
            btnNuevo.Visible = false;
            btnGrabar.Visible = false;
            this.ui_actualizaComboBox();
            this.ui_limpiar();
            string rftdoc = "GR";
            NumDoc numdoc = new NumDoc();
            string numero = numdoc.genNumDoc(codcia, codcaja, rftdoc);
            if (numero != string.Empty)
            {
                txtSerie.Text = numero.Substring(0, 4);
                txtNumero.Text = numero.Substring(4, 8);
            }
            else
            {
                txtSerie.Text = string.Empty;
                txtNumero.Text = string.Empty;
            }
            ui_listaItem();
           
            txtSerie.Focus();
        }


        public void editar(string alma, string td, string numdoc)
        {
            string query;
            this._operacion = "EDITAR";
            btnNuevo.Visible = true;
            btnGrabar.Visible = true;
           

            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();
            
            string codcia = this._codcia;

            query = "SELECT * FROM almovc where codcia='"+@codcia+"' and alma='" + @alma + "' and td='" + @td + "' and numdoc='" + @numdoc + "'; ";
            try
            {
                SqlCommand myCommand = new SqlCommand(query, conexion);
                SqlDataReader myReader = myCommand.ExecuteReader();

                if (myReader.Read())
                {
                    Funciones funciones = new Funciones();
                    MaesGen maesgen = new MaesGen();
                    string idcia = this._codcia;
                    this._alma = myReader.GetString(myReader.GetOrdinal("alma"));
                    this._codmov=myReader.GetString(myReader.GetOrdinal("codmov"));
                    this._numdoc = myReader.GetString(myReader.GetOrdinal("numdoc"));
                    txtSerie.Text = myReader.GetString(myReader.GetOrdinal("rfserieguia"));
                    txtNumero.Text = myReader.GetString(myReader.GetOrdinal("rfnroguia"));
                    txtFechaDoc.Text = myReader.GetString(myReader.GetOrdinal("fecguia"));
                    txtAlbaran.Text = myReader.GetString(myReader.GetOrdinal("albaran"));
                    txtFechaTraslado.Text = myReader.GetString(myReader.GetOrdinal("fectras"));
                    string codpartida=myReader.GetString(myReader.GetOrdinal("partida"));
                    query = "Select codpartida as clave,despartida as descripcion ";
                    query = query + "from punpar where idcia='" + @idcia + "' and ";
                    query = query + " codpartida='" + @codpartida + "'order by 1 asc;";
                    funciones.consultaComboBox(query, cmbPartida);
                    txtCliente.Text = myReader.GetString(myReader.GetOrdinal("codclie"));
                    Clie clie = new Clie();
                    txtRazonDesti.Text = clie.ui_getDatos(myReader.GetString(myReader.GetOrdinal("codclie")), "NOMBRE");
                    txtRucDesti.Text = clie.ui_getDatos(myReader.GetString(myReader.GetOrdinal("codclie")), "RUC");
                    string codclie = myReader.GetString(myReader.GetOrdinal("codclie"));
                    ui_listarPunClie(codclie);
                    string llegada = myReader.GetString(myReader.GetOrdinal("llegada"));
                    query = "Select codpartida as clave,despartida as descripcion ";
                    query = query + "from punclie where codclie='" + @codclie+ "' and ";
                    query = query + " codpartida='" + @llegada + "'order by 1 asc;";
                    funciones.consultaComboBox(query, cmbLlegada);
                    ui_listarEstaClie(codclie);
                    string codesta = myReader.GetString(myReader.GetOrdinal("codesta"));
                    query = "Select codesta as clave,desesta as descripcion ";
                    query = query + "from estaclie where codclie='" + @codclie + "' and ";
                    query = query + " codesta='" + @codesta + "'order by 1 asc;";
                    funciones.consultaComboBox(query, cmbTienda);

                    maesgen.consultaDetMaesGen("220", myReader.GetString(myReader.GetOrdinal("docidendesti")), cmbDocIdenDesti);
                    txtNroDocIdenDesti.Text = myReader.GetString(myReader.GetOrdinal("nrodocdesti"));
                    txtRazonTrans.Text = myReader.GetString(myReader.GetOrdinal("razontrans"));
                    txtMarcaTrans.Text = myReader.GetString(myReader.GetOrdinal("marcatrans"));
                    txtCertInsTrans.Text = myReader.GetString(myReader.GetOrdinal("certrans"));
                    txtLicenciaTrans.Text = myReader.GetString(myReader.GetOrdinal("lictrans"));
                    maesgen.consultaDetMaesGen("210", myReader.GetString(myReader.GetOrdinal("mottras")), cmbMotivoTraslado);
                    txtFecCrea.Text = myReader.GetString(myReader.GetOrdinal("fcrea"));
                    txtFecMod.Text = myReader.GetString(myReader.GetOrdinal("fmod"));
                    txtUsuario.Text = myReader.GetString(myReader.GetOrdinal("usuario"));

                    string orden = myReader.GetString(myReader.GetOrdinal("orden"));
                    if (orden.Equals("S"))
                    {
                        chkOrden.Checked = true;
                        txtNumeroOC.Text = myReader.GetString(myReader.GetOrdinal("rfnroOC")) ;
                        txtFechaOC.Text = myReader.GetString(myReader.GetOrdinal("fechaOC"));
                    }
                    else
                    {
                        chkOrden.Checked = false;
                        txtNumeroOC.Text = "";
                        txtFechaOC.Text = "";
                    }

                    ui_listaLotes(codcia,myReader.GetString(myReader.GetOrdinal("alma")), "");
                    
                    if (myReader.GetString(myReader.GetOrdinal("situagr")).Equals("V"))
                    {
                        txtEstado.Text = "VIGENTE";
                        this._finaliza = "N";
                        btnAceptar.Enabled = true;
                        btnFinaliza.Enabled = true;
                        btnNuevo.Enabled = true;
                        btnGrabar.Enabled = true;
                        btnEliminar.Enabled = true;
                        lblMensaje.Visible = false;
                    }
                    else
                    {
                        if (myReader.GetString(myReader.GetOrdinal("situagr")).Equals("F"))
                        {
                            txtEstado.Text = "FINALIZADO";
                        }
                        else
                        {
                            txtEstado.Text = "ANULADO";
                        }

                        this._finaliza = "S";
                        btnAceptar.Enabled = false;
                        btnFinaliza.Enabled = false;
                        btnNuevo.Enabled = false;
                        btnGrabar.Enabled = false;
                        btnEliminar.Enabled = false;
                        lblMensaje.Visible = true;
                    }

                }
                ui_listaItem();
                txtFechaDoc.Focus();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }

            conexion.Close();
        }

        private void ui_limpiar()
        {
            GlobalVariables variables = new GlobalVariables();
            txtSerie.Text=string.Empty;
            txtNumero.Text = string.Empty;
            txtFechaDoc.Text=DateTime.Now.ToString("dd/MM/yyyy");
            txtFechaTraslado.Text = DateTime.Now.ToString("dd/MM/yyyy");
            
            chkOrden.Checked = false;
            txtNumeroOC.Text = string.Empty;
            txtFechaOC.Text = string.Empty;
            txtAlbaran.Text = string.Empty;

            cmbPartida.Text = string.Empty;
            cmbLlegada.Text = string.Empty;
            txtCliente.Clear();
            txtRazonDesti.Clear();
            cmbDocIdenDesti.Text = string.Empty;
            txtNroDocIdenDesti.Clear();
            txtRazonTrans.Clear();
            txtMarcaTrans.Clear();
            txtCertInsTrans.Clear();
            txtLicenciaTrans.Clear();
            cmbMotivoTraslado.Text = string.Empty;
            txtFecCrea.Text=DateTime.Now.ToString("dd/MM/yyyy");
            txtFecMod.Text=DateTime.Now.ToString("dd/MM/yyyy");
            txtUsuario.Text = variables.getValorUsr();
            txtEstado.Text = "VIGENTE";
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            ((ui_guiaremi)FormPadre).btnActualizar.PerformClick();
            this.Close();
        }

        private string ui_valida()
        {
            string valorValida = "G";
            MaesGen maesgen = new MaesGen();
            Funciones funciones = new Funciones();
                        
            if (txtSerie.Text == string.Empty && valorValida == "G")
            {
                valorValida = "B";
                MessageBox.Show("No ha ingresado Serie", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtSerie.Focus();
            }

            if (txtNumero.Text == string.Empty && valorValida == "G")
            {
                valorValida = "B";
                MessageBox.Show("No ha ingresado Nro. Doc.", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtNumero.Focus();
            }
                     

            if (cmbMotivoTraslado.Text == string.Empty && valorValida == "G")
            {
                valorValida = "B";
                MessageBox.Show("No ha seleccionado Motivo de Traslado", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cmbMotivoTraslado.Focus();
            }
            if (cmbMotivoTraslado.Text != string.Empty && valorValida == "G")
            {
                string resultado = maesgen.verificaComboBoxMaesGen("210", cmbMotivoTraslado.Text.Trim());
                if (resultado.Trim() == string.Empty)
                {
                    valorValida = "B";
                    MessageBox.Show("Dato incorrecto en Motivo de Traslado", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    cmbMotivoTraslado.Focus();
                }
            }

            if (cmbPartida.Text == string.Empty && valorValida == "G")
            {
                valorValida = "B";
                MessageBox.Show("No ha seleccionado Punto de Partida", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cmbPartida.Focus();
            }

            if (cmbPartida.Text != string.Empty && valorValida == "G")
            {
                string codigo = funciones.getValorComboBox(cmbPartida, 3);
                string idcia = this._codcia;
                string query =  " Select codpartida as clave,despartida as descripcion ";
                query = query + " from punpar where idcia='" + @idcia + "' and codpartida='" + @codigo + "' ";
                query = query + " order by 1 asc;";
                string resultado = funciones.verificaItemComboBox(query, cmbPartida);

                if (resultado.Trim() == string.Empty)
                {
                    valorValida = "B";
                    MessageBox.Show("Dato incorrecto en Punto de Partida", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    cmbPartida.Focus();
                }
            }

            if (txtCliente.Text == string.Empty && valorValida == "G")
            {
                valorValida = "B";
                MessageBox.Show("No ha ingresado Cliente Destinatario", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtCliente.Focus();
            }

            Clie clie = new Clie();
            if (txtCliente.Text != string.Empty && valorValida == "G" && clie.ui_getDatos(txtCliente.Text, "NOMBRE") == string.Empty)
            {
                valorValida = "B";
                MessageBox.Show("Cliente Destinatario no existe", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtCliente.Focus();
            }

            if (cmbLlegada.Text == string.Empty && valorValida == "G")
            {
                valorValida = "B";
                MessageBox.Show("No ha seleccionado Punto de Llegada", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cmbLlegada.Focus();
            }

            if (cmbLlegada.Text != string.Empty && valorValida == "G")
            {
                string codigo = funciones.getValorComboBox(cmbLlegada, 3);
                string codclie = txtCliente.Text;
                string query = " Select codpartida as clave,despartida as descripcion ";
                query = query + " from punclie where codclie='" + @codclie + "' and codpartida='" + @codigo + "' ";
                query = query + " order by 1 asc;";
                string resultado = funciones.verificaItemComboBox(query, cmbLlegada);

                if (resultado.Trim() == string.Empty)
                {
                    valorValida = "B";
                    MessageBox.Show("Dato incorrecto en Punto de Llegada", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    cmbLlegada.Focus();
                }
            }

            if (txtRazonTrans.Text == string.Empty && valorValida == "G")
            {
                valorValida = "B";
                MessageBox.Show("No ha ingresado Razón Social del Transportista", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtRazonTrans.Focus();
            }

            if (txtMarcaTrans.Text == string.Empty && valorValida == "G")
            {
                valorValida = "B";
                MessageBox.Show("No ha ingresado Vehículo Marca y Placa del Transportista", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtMarcaTrans.Focus();
            }

            return valorValida;
        }
        
        private void btnAceptar_Click(object sender, EventArgs e)
        {
            string valorValida = ui_valida();

            if (valorValida.Equals("G"))
            {
                try
                {
                    AlmovC almovc = new AlmovC();
                    Funciones funciones = new Funciones();
                    GlobalVariables variables = new GlobalVariables();
                    string operacion = this._operacion;
                    string codcia = this._codcia;
                    string codcaja = this._codcaja;
                    string td = "PS";
                    string fecdoc = txtFechaDoc.Text;
                    string fectras = txtFechaTraslado.Text;
                    string rftdoc = "GR";

                    if (txtSerie.Text.Length < 4)
                    {
                        txtSerie.Text = funciones.replicateCadena("0", 4 - txtSerie.Text.Trim().Length) + txtSerie.Text.Trim();
                    }

                    if (txtNumero.Text.Length < 8)
                    {
                        txtNumero.Text = funciones.replicateCadena("0", 8 - txtNumero.Text.Trim().Length) + txtNumero.Text.Trim();
                    }

                    if (txtAlbaran.Text.Length < 7)
                    {
                        txtAlbaran.Text = funciones.replicateCadena("0", 7 - txtAlbaran.Text.Trim().Length) + txtAlbaran.Text.Trim();
                    }

                    string rfndoc = txtSerie.Text.Trim() + "-" + txtNumero.Text.Trim();
                    string rfserie = txtSerie.Text.Trim();
                    string rfnro = txtNumero.Text.Trim();
                    string partida = funciones.getValorComboBox(cmbPartida, 3);
                    string llegada = funciones.getValorComboBox(cmbLlegada, 3);
                    string codclie = txtCliente.Text;
                    string razondesti = txtRazonDesti.Text;
                    string docidendesti = funciones.getValorComboBox(cmbDocIdenDesti, 4);
                    string nrodocdesti = txtNroDocIdenDesti.Text;
                    string razontrans = txtRazonTrans.Text;
                    string marcatrans = txtMarcaTrans.Text;
                    string certrans = txtCertInsTrans.Text;
                    string lictrans = txtLicenciaTrans.Text;
                    string mottrans = funciones.getValorComboBox(cmbMotivoTraslado, 4);
                    string glosa1 = rftdoc + " " + rfndoc + " " + razondesti;
                    string codesta = funciones.getValorComboBox(cmbTienda, 5);
                    string albaran = txtAlbaran.Text;
                    string orden;
                    string rfserieOC;
                    string rfnumeroOC;
                    string fechaOC;

                    if (chkOrden.Checked)
                    {
                        orden = "S";

                        if (txtNumeroOC.Text.Length < 10)
                        {
                            txtNumeroOC.Text = funciones.replicateCadena("0", 10 - txtNumeroOC.Text.Trim().Length) + txtNumeroOC.Text.Trim();
                        }

                        rfserieOC = "";
                        rfnumeroOC = txtNumeroOC.Text;
                        fechaOC = txtFechaOC.Text;
                    }
                    else
                    {
                        orden = "N";

                        rfserieOC = "";
                        rfnumeroOC = "";
                        fechaOC = "";
                    }


                    string situa = "F";
                    string situaval = "F";
                    string situafac = "V";
                    string situagr = txtEstado.Text.Substring(0, 1);
                    string fcrea = txtFecCrea.Text;
                    string fmod = DateTime.Now.ToString("dd/MM/yyyy");
                    string usuario = variables.getValorUsr();
                    string alma;
                    string codmov;
                    string numdoc;

                    if (operacion.Equals("AGREGAR"))
                    {
                        Cajas cajas = new Cajas();
                        alma = cajas.ui_getDatos(codcia, codcaja, "ALMAFAC");
                        codmov = cajas.ui_getDatos(codcia, codcaja, "MOVGUIA");
                        numdoc = almovc.genCodAlmov(codcia,alma, td);
                        this._alma = alma;
                        this._numdoc = numdoc;
                        this._codmov = codmov;
                    }
                    else
                    {
                        alma = this._alma;
                        codmov = this._codmov;
                        numdoc = this._numdoc;
                    }

                    almovc.updGuiaRemision(operacion, codcia, codcaja, alma, numdoc, codmov, rftdoc,
                        rfndoc, rfserie, rfnro, fecdoc, fectras, partida, llegada, codclie,
                        docidendesti, nrodocdesti, razontrans, marcatrans, certrans, lictrans, mottrans, glosa1,
                        situa, situaval, situagr, fcrea, fmod, usuario, "GRCS", situafac, orden, rfserieOC, 
                        rfnumeroOC, fechaOC,codesta,albaran);
                    
                    this._finaliza = "S";

                    if (operacion.Equals("AGREGAR"))
                    {
                        this._operacion = "EDITAR";
                        btnNuevo.Visible = true;
                        btnGrabar.Visible = true;
                        btnNuevo.Focus();
                    }
                }
                catch (FormatException ex)
                {
                    MessageBox.Show(ex.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        
        public void ui_listaItem()
        {
            Funciones funciones = new Funciones();
            string alma = this._alma;
            string codcia = this._codcia;
            string td = "PS";
            string numdoc = this._numdoc;
            
            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();
            string query = " select A.item,A.codarti,B.desarti,B.unidad,A.lote,A.cantidad,A.pesouni,A.pesototal ";
            query = query + " from almovd A left join alarti B on A.codcia=B.codcia and A.codarti = B.codarti";
            query = query + " where A.codcia='"+@codcia+"' and A.alma='" + @alma + "' and A.td='" + @td + "' and A.numdoc='" + @numdoc + "' ";
            query = query + " order by A.item asc ;";
            try
            {
                using (SqlDataAdapter myDataAdapter = new SqlDataAdapter(query, conexion))
                {
                    DataSet myDataSet = new DataSet();
                    myDataAdapter.Fill(myDataSet, "tblData");
                    funciones.formatearDataGridView(dgvData);
                    dgvData.DataSource = myDataSet.Tables["tblData"];
                    dgvData.Columns[0].HeaderText = "Item";
                    dgvData.Columns[1].HeaderText = "Código";
                    dgvData.Columns[2].HeaderText = "Descripción del Producto";
                    dgvData.Columns[3].HeaderText = "Und.";
                    dgvData.Columns[4].HeaderText = "Lote";
                    dgvData.Columns[5].HeaderText = "Cantidad";
                    dgvData.Columns[6].HeaderText = "Peso Unit.";
                    dgvData.Columns[7].HeaderText = "Peso Total";
                    
                    if (dgvData.Rows.Count > 2)
                    {
                        dgvData.CurrentCell = dgvData.Rows[dgvData.Rows.Count - 1].Cells[0];
                    }

                    dgvData.Columns[0].Width = 40;
                    dgvData.Columns[1].Width = 70;
                    dgvData.Columns[2].Width = 300;
                    dgvData.Columns[3].Width = 40;
                    dgvData.Columns[4].Width = 120;
                    dgvData.Columns[5].Width = 70;
                    dgvData.Columns[6].Width = 60;
                    dgvData.Columns[7].Width = 70;
                   

                    float pesototal = float.Parse(funciones.sumaColumnaDataGridView(dgvData, 7));
                    txtPesoTotal.Text = pesototal.ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            conexion.Close();
        }
        
        
       

        private void txtFechaDoc_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                if (UtiFechas.IsDate(txtFechaDoc.Text))
                {
                    txtFechaTraslado.Text = txtFechaDoc.Text;
                    e.Handled = true;
                    SendKeys.Send("{TAB}");
                }
                else
                {
                    MessageBox.Show("Fecha no válida", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    e.Handled = true;
                    txtFechaDoc.Focus();
                }
            }
        }

        private void txtSerie_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                Funciones funciones = new Funciones();
                if (txtSerie.Text.Trim() != string.Empty)
                {
                    if (txtSerie.Text.Length == 4)
                    {
                        e.Handled = true;
                        txtNumero.Focus();
                    }
                    else
                    {
                        txtSerie.Text = funciones.replicateCadena("0", 4 - txtSerie.Text.Trim().Length) + txtSerie.Text.Trim();
                        e.Handled = true;
                        txtNumero.Focus();

                    }
                }
                else
                {
                    MessageBox.Show("Deberá asignar un número de serie del documento", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    e.Handled = true;
                    txtSerie.Focus();
                }

            }

           
        }

        private void txtNumero_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                Funciones funciones = new Funciones();
                if (txtNumero.Text.Trim() != string.Empty)
                {
                    if (txtNumero.Text.Length == 8)
                    {
                        e.Handled = true;
                        txtFechaDoc.Focus();
                    }
                    else
                    {
                        txtNumero.Text = funciones.replicateCadena("0", 8 - txtNumero.Text.Trim().Length) + txtNumero.Text.Trim();
                        e.Handled = true;
                        txtFechaDoc.Focus();

                    }
                }
                else
                {
                    MessageBox.Show("Deberá asignar un número al documento", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    e.Handled = true;
                    txtNumero.Focus();
                }

            }

           
        }

        private void txtCodigo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                this._TextBoxActivo = txtCodigo;
                ui_viewarti ui_viewarti = new ui_viewarti();
                ui_viewarti._FormPadre = this;
                ui_viewarti._codcia = this._codcia;
                ui_viewarti._clasePadre = "ui_updguiaremi";
                ui_viewarti._condicionAdicional = string.Empty;
                ui_viewarti.BringToFront();
                ui_viewarti.ShowDialog();
                ui_viewarti.Dispose();
            }
        }

        public void ui_consultaProducto(string codcia, string codarti)
        {
            AlArti alarti = new AlArti();
            string codigo = alarti.ui_getDatos(codcia, codarti, "CODIGO");
            txtCodigo.Text = codigo;
            txtDescri.Text = alarti.ui_getDatos(codcia, codarti, "NOMBRE");
            txtUnidad.Text = alarti.ui_getDatos(codcia, codarti, "UNIDAD");
            txtPesoUni.Text = alarti.ui_getDatos(codcia, codarti, "PESOKG");
        }

        private void txtCodigo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(Keys.Enter))
            {
                AlArti alarti = new AlArti();
                string codcia = this._codcia;
                string alma = this._alma;
                string codarti = txtCodigo.Text.Trim();
                string nombre = alarti.ui_getDatos(codcia,codarti, "NOMBRE");
                if (nombre == string.Empty || codarti == string.Empty)
                {
                    ui_limpiarItem();
                    e.Handled = true;
                    txtCodigo.Focus();
                }
                else
                {
                    ui_consultaProducto(codcia, codarti);
                    ///////////////////////////////////////////////////////////////////////
                    /////////////////////////////STOCK////////////////////////////////////
                    ///////////////////////////////////////////////////////////////////////
                    Alstock alstock = new Alstock();
                    alstock.recalcularStockProducto(codcia, alma, codarti);
                    ui_listaLotes(codcia, alma, codarti);
                    e.Handled = true;
                    txtCantidad.Focus();
                }

            }
        }

        private void calcula()
        {
            float cantidad = float.Parse(txtCantidad.Text);
            float pesouni = float.Parse(txtPesoUni.Text);
            txtPeso.Text = (cantidad * pesouni).ToString("0.##");
        }

        private void txtCantidad_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                try
                {
                    float numero = float.Parse(txtCantidad.Text);
                    calcula();
                    e.Handled = true;
                    btnGrabar.Focus();
                }
                catch (FormatException)
                {
                    MessageBox.Show("Cantidad no válida", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    txtCantidad.Clear();
                    txtCantidad.Focus();
                }

            }
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            this.agregarItem();
        }


        private string validaItem()
        {
            string valorValida = "G";

            if (txtCodigo.Text == string.Empty && valorValida == "G")
            {
                valorValida = "B";
                MessageBox.Show("No ha ingresado Código de Producto", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtCodigo.Focus();
            }

            if (valorValida == "G")
            {
                try
                {
                    float numero = float.Parse(txtCantidad.Text);
                    if (numero <= 0)
                    {
                        valorValida = "B";
                        MessageBox.Show("Cantidad no válida", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txtCantidad.Clear();
                        txtCantidad.Focus();
                    }

                }
                catch (FormatException)
                {
                    valorValida = "B";
                    MessageBox.Show("Cantidad no válida", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtCantidad.Clear();
                    txtCantidad.Focus();
                }
            }


            if (valorValida == "G")
            {
                try
                {
                    string codcia = this._codcia;
                    string alma = this._alma;
                    string codarti = txtCodigo.Text.Trim();
                    string lote = cmbLote.Text;
                    float cantidad = float.Parse(txtCantidad.Text);
                    Alstock alstock = new Alstock();
                    float stockdisp = float.Parse(alstock.ui_getStockLote(codcia,alma, codarti, lote));

                    if (cantidad <= 0)
                    {
                        valorValida = "B";
                        MessageBox.Show("Cantidad no válida", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txtCantidad.Text = "0";
                        txtCantidad.Focus();
                    }

                    if (valorValida == "G" && cantidad > stockdisp)
                    {
                        valorValida = "B";
                        MessageBox.Show("El producto no posee Stock en el Lote seleccionado", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txtCantidad.Text = stockdisp.ToString();
                        txtCantidad.Focus();
                    }

                }
                catch (FormatException)
                {
                    valorValida = "B";
                    MessageBox.Show("Cantidad no válida", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtCantidad.Clear();
                    txtCantidad.Focus();
                }
            }

            if (valorValida == "G")
            {
                try
                {
                    float numero = float.Parse(txtPeso.Text);
                    if (numero < 0)
                    {
                        valorValida = "B";
                        MessageBox.Show("Peso Unitario no válido", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txtPeso.Clear();
                        txtPeso.Focus();
                    }

                }
                catch (FormatException)
                {
                    valorValida = "B";
                    MessageBox.Show("Precio no válido", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtPeso.Clear();
                    txtPeso.Focus();
                }
            }

            return valorValida;
        }

        private void btnGrabar_Click(object sender, EventArgs e)
        {
            string valorValida = validaItem();
            if (valorValida.Equals("G"))
            {
                try
                {
                    string operacion = this._opeitem;
                    string codcia = this._codcia;
                    string alma = this._alma;
                    string td = "PS";
                    string numdoc = this._numdoc;
                    string codarti = txtCodigo.Text.Trim();
                    string lote = cmbLote.Text.Trim();
                    string cantidad = txtCantidad.Text;
                    string pesouni = txtPesoUni.Text;
                    string pesototal = txtPeso.Text;
                    
                    Alstock alstock = new Alstock();
                    //CiaFile ciafile=new CiaFile();
                    //string metodo = ciafile.ui_getDatosCiaFile(codcia, "KARDEX");
                    //string loteTurno=alstock.ui_validaMetodoValoracion(codcia, alma, codarti, lote, metodo);
                    //if (lote.Equals(loteTurno))
                    //{

                        AlmovD almovd = new AlmovD();
                        string item;
                        if (this._opeitem.Equals("AGREGAR"))
                        {
                            item = almovd.genCod(codcia, alma, td, numdoc);
                        }
                        else
                        {
                            item = this._item;
                        }
                        almovd.updAlmovDGuia(operacion, codcia, alma, td, numdoc, item, codarti,
                            cantidad, pesouni, pesototal, lote);
                        alstock.recalcularStockProducto(codcia, alma, codarti);
                        this.ui_listaItem();
                        this.agregarItem();

                   //}
                   // else
                   // {
                   //     MessageBox.Show("El Lote a seleccionar deberá ser el " + loteTurno + ". Método de valoración de Inventario : " + metodo, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                   // }
                  
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            
            Int32 selectedCellCount =
            dgvData.GetCellCount(DataGridViewElementStates.Selected);
            if (selectedCellCount > 0)
            {
                Funciones funciones = new Funciones();
                string alma = this._alma;
                string codcia = this._codcia;
                string td = "PS";
                string numdoc = this._numdoc;
                string item = dgvData.Rows[dgvData.SelectedCells[1].RowIndex].Cells[0].Value.ToString();
                DialogResult resultado = MessageBox.Show("¿Desea eliminar el Item " + @item + "?",
                "Consulta Importante",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);
                if (resultado == DialogResult.Yes)
                {
                    AlmovD almovd = new AlmovD();
                    almovd.delAlmovD(codcia,alma, td, numdoc, item);
                    ui_listaItem();
                }
            }
            else
            {
                MessageBox.Show("No ha seleccionado registro a Eliminar", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void btnFinaliza_Click(object sender, EventArgs e)
        {
            string situa = txtEstado.Text.Substring(0, 1);

            if (situa.Equals("V"))
            {
                Funciones funciones = new Funciones();
                string td = "PS";
                string codmov = this._codmov;
                string valorValida = ui_valida();
                if (valorValida.Equals("G"))
                {
                    try
                    {
                        if (this._finaliza.Equals("S"))
                        {
                            AlmovC almovc = new AlmovC();
                            GlobalVariables variables = new GlobalVariables();
                            string codcia = this._codcia;
                            string alma = this._alma;
                            string numdoc = this._numdoc;
                            string fmod = DateTime.Now.ToString("dd/MM/yyyy");
                            string usuario = variables.getValorUsr();
                            almovc.updFinGuia(codcia,alma, td, numdoc, fmod, usuario);
                            txtEstado.Text = "FINALIZADO";
                            ((ui_guiaremi)FormPadre).btnActualizar.PerformClick();
                            MessageBox.Show("Guía de Remisión Finalizada", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            this.Close();
                            DialogResult resultado = MessageBox.Show("¿Desea Imprimir la Guía de Remisión ?",
                               "Consulta Importante",
                               MessageBoxButtons.YesNo,
                               MessageBoxIcon.Question);
                            if (resultado == DialogResult.Yes)
                            {
                                
                                DataTable dtguia = new DataTable();
                                string rftdoc = "GR";
                                string rfserie = txtSerie.Text.Trim();
                                string rfnro = txtNumero.Text.Trim();
                                GuiaRemi guiaremi = new GuiaRemi();
                                dtguia = guiaremi.generaGuiasRemiWin(codcia, rftdoc, rfserie, rfnro, rfnro);
                                if (dtguia.Rows.Count > 0)
                                {
                                    ui_impresora ui_impresora = new ui_impresora();
                                    ui_impresora.Activate();
                                    ui_impresora.BringToFront();
                                    ui_impresora.ShowDialog();
                                    ui_impresora.Dispose();
                                    cr.crGuiaRemi cr = new cr.crGuiaRemi();
                                    GlobalVariables gv = new GlobalVariables();
                                    PrintDialog pd = new PrintDialog();
                                    cr.SetDataSource(dtguia);
                                    cr.PrintOptions.PrinterName = gv.getPrinterName();
                                    cr.PrintToPrinter(1, true, 0, 0);

                                }
                                else
                                {
                                    MessageBox.Show("No existe información registrada en el criterio seleccionado", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                                }
                            }

                        }
                        else
                        {
                            MessageBox.Show("Primero debe de guardar la información", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        }
                    }
                    catch (FormatException ex)
                    {
                        MessageBox.Show(ex.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("El registro ya se encuentra Finalizado", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void txtFechaTraslado_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                if (UtiFechas.IsDate(txtFechaTraslado.Text))
                {
                    e.Handled = true;
                    cmbMotivoTraslado.Focus();
                }
                else
                {
                    MessageBox.Show("Fecha de Inicio de Traslado no válida", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    e.Handled = true;
                    txtFechaTraslado.Focus();
                }
            }
        }

        private void txtPartida_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(Keys.Enter))
            {
                e.Handled = true;
                SendKeys.Send("{TAB}");
            }
        }

       
        

        private void txtRazonDesti_KeyPress(object sender, KeyPressEventArgs e)
        {
            
        }

        private void txtNroDocIdenDesti_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(Keys.Enter))
            {
                e.Handled = true;
                cmbPartida.Focus();
            }
        }

        private void txtRazonTrans_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(Keys.Enter))
            {
                e.Handled = true;
                SendKeys.Send("{TAB}");
            }
        }

        private void txtMarcaTrans_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(Keys.Enter))
            {
                e.Handled = true;
                SendKeys.Send("{TAB}");
            }
        }

        private void txtCertInsTrans_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(Keys.Enter))
            {
                e.Handled = true;
                SendKeys.Send("{TAB}");
            }
        }

        private void txtLicenciaTrans_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(Keys.Enter))
            {
                e.Handled = true;
                toolStripForm.Items[0].Select();
                toolStripForm.Focus();
            }
        }

        private void cmbMotivoTraslado_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(Keys.Enter))
            {
                if (cmbMotivoTraslado.Text != String.Empty)
                {
                    MaesGen maesgen = new MaesGen();
                    maesgen.validarDetMaesGen("210", cmbMotivoTraslado, cmbMotivoTraslado.Text);
                }
                e.Handled = true;
                SendKeys.Send("{TAB}");
            }
        }

        private void cmbDocIdenDesti_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(Keys.Enter))
            {
                if (cmbDocIdenDesti.Text != String.Empty)
                {
                    MaesGen maesgen = new MaesGen();
                    maesgen.validarDetMaesGen("220", cmbDocIdenDesti, cmbDocIdenDesti.Text);
                }
                e.Handled = true;
                txtNroDocIdenDesti.Focus();
            }
        }

        private void txtPesoUni_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                try
                {
                    float numero = float.Parse(txtPesoUni.Text);
                    calcula();
                    e.Handled = true;
                    SendKeys.Send("{TAB}");
                }
                catch (FormatException)
                {
                    MessageBox.Show("Peso Unitario no válido", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    txtPesoUni.Clear();
                    txtPesoUni.Focus();
                }

            }
        }

        private void cmbPartida_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(Keys.Enter))
            {
                if (cmbPartida.Text != String.Empty)
                {
                    Funciones funciones = new Funciones();
                    string idcia = this._codcia;
                    string codpartida = funciones.getValorComboBox(cmbPartida, 3);
                    string query = "SELECT codpartida as clave,despartida as descripcion FROM punpar ";
                    query = query + "WHERE codpartida='" + @codpartida+ "' and idcia='" + @idcia + "';";
                    funciones.validarCombobox(query, cmbPartida);
                }
                e.Handled = true;
                cmbLlegada.Focus();

            }
        }

        private void txtRucDesti_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                this._TextBoxActivo = txtCliente;
                ui_viewclientes ui_viewclientes = new ui_viewclientes();
                ui_viewclientes._FormPadre = this;
                ui_viewclientes._clasePadre = "ui_updguiaremi";
                ui_viewclientes._condicionAdicional = string.Empty;
                ui_viewclientes.BringToFront();
                ui_viewclientes.ShowDialog();
                ui_viewclientes.Dispose();
            }
        }

        private void txtCliente_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(Keys.Enter))
            {
                Clie clie = new Clie();
                string codclie = txtCliente.Text.Trim();
                string nombre = clie.ui_getDatos(codclie, "NOMBRE");
                if (nombre == string.Empty)
                {
                    txtCliente.Text = string.Empty;
                    txtRazonDesti.Text = string.Empty;
                    txtRucDesti.Text = string.Empty;
                    cmbDocIdenDesti.Text = string.Empty;
                    txtNroDocIdenDesti.Text = string.Empty;
                    e.Handled = true;
                    txtCliente.Focus();
                }
                else
                {
                    string rucclie = clie.ui_getDatos(codclie, "RUC");
                    string docidendesti = string.Empty;
                    string nrodocdesti = string.Empty;
                    txtCliente.Text = clie.ui_getDatos(codclie, "CODIGO");
                    txtRazonDesti.Text = clie.ui_getDatos(codclie, "NOMBRE");
                    txtRucDesti.Text = rucclie;
                    if (rucclie == string.Empty)
                    {
                        nrodocdesti = clie.ui_getDatos(codclie, "DNI");
                        if (nrodocdesti != string.Empty)
                        {
                            docidendesti = "01";
                        }
                    }
                    else
                    {
                        nrodocdesti = rucclie;
                        docidendesti = "06";
                    }
                    MaesGen maesgen = new MaesGen();
                    maesgen.consultaDetMaesGen("220", docidendesti, cmbDocIdenDesti);
                    txtNroDocIdenDesti.Text = nrodocdesti;
                    e.Handled = true;
                    cmbDocIdenDesti.Focus();
                }
                
                ui_listarPunClie(codclie);
                ui_listarEstaClie(codclie);

            }
        }

        private void btnNuevoClie_Click(object sender, EventArgs e)
        {
            ui_updclie ui_updclie = new ui_updclie();
            ui_updclie._FormPadre = this;
            ui_updclie.setValores("ui_updguiaremi");
            ui_updclie.Activate();
            ui_updclie.agregar();
            ui_updclie.ui_actualizaComboBox();
            ui_updclie.BringToFront();
            ui_updclie.ShowDialog();
            ui_updclie.Dispose();
        }

        private void btnEditarClie_Click(object sender, EventArgs e)
        {
            string codclie = txtCliente.Text.Trim();
            if (codclie != string.Empty)
            {
                Clie clie = new Clie();
                string nombre = clie.ui_getDatos(codclie, "NOMBRE");
                if (nombre == string.Empty)
                {
                    MessageBox.Show("Código de Cliente a editar no existe", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
                else
                {
                    ui_updclie ui_updclie = new ui_updclie();
                    ui_updclie._FormPadre = this;
                    ui_updclie.Activate();
                    ui_updclie.setValores("ui_updguiaremi");
                    ui_updclie.ui_actualizaComboBox();
                    ui_updclie.BringToFront();
                    ui_updclie.editar(codclie);
                    ui_updclie.ShowDialog();
                    ui_updclie.Dispose();
                }
            }
            else
            {
                MessageBox.Show("No ha ingresado Código de Cliente a editar", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void cmbLlegada_KeyPress(object sender, KeyPressEventArgs e)
        {
            
            if (e.KeyChar == (char)(Keys.Enter))
            {
                if (cmbLlegada.Text != String.Empty)
                {
                    Funciones funciones = new Funciones();
                    string codclie = txtCliente.Text;
                    string codpartida = funciones.getValorComboBox(cmbLlegada, 3);
                    string query = "SELECT codpartida as clave,despartida as descripcion FROM punclie ";
                    query = query + "WHERE codpartida='" + @codpartida + "' and codclie='" + @codclie + "';";
                    funciones.validarCombobox(query, cmbLlegada);
                             

                }
                e.Handled = true;
                cmbTienda.Focus();

            }
        }

        private void btnLlegada_Click(object sender, EventArgs e)
        {
            string codclie = txtCliente.Text.Trim();
            if (codclie != string.Empty)
            {
                Clie clie = new Clie();
                string nombre = clie.ui_getDatos(codclie, "NOMBRE");
                if (nombre == string.Empty)
                {
                    MessageBox.Show("Código de Cliente no existe", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
                else
                {
                    ui_updpunclie ui_updpunclie = new ui_updpunclie();
                    ui_updpunclie._FormPadre = this;
                    ui_updpunclie.Activate();
                    ui_updpunclie.setValores("ui_updguiaremi");
                    ui_updpunclie._codclie = codclie;
                    ui_updpunclie._desclie = clie.ui_getDatos(codclie, "NOMBRE");
                    ui_updpunclie._rucclie = clie.ui_getDatos(codclie, "RUC");
                    ui_updpunclie._dniclie = clie.ui_getDatos(codclie, "DNI");
                    ui_updpunclie.BringToFront();
                    ui_updpunclie.ShowDialog();
                    ui_updpunclie.Dispose();
                }
            }
            else
            {
                MessageBox.Show("No ha ingresado Código de Cliente a editar", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void cmbLlegada_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            Funciones funciones = new Funciones();
            string codclie = txtCliente.Text;
            string codpartida = funciones.getValorComboBox(cmbLlegada, 3);
            
        }

        private void lblMensaje_Click(object sender, EventArgs e)
        {

        }

       
        private void txtNumeroOC_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                Funciones funciones = new Funciones();
                if (txtNumeroOC.Text.Trim() != string.Empty)
                {
                    if (txtNumeroOC.Text.Length == 10)
                    {
                        e.Handled = true;
                        txtFechaOC.Focus();
                    }
                    else
                    {
                        txtNumeroOC.Text = funciones.replicateCadena("0", 10 - txtNumeroOC.Text.Trim().Length) + txtNumeroOC.Text.Trim();
                        e.Handled = true;
                        txtFechaOC.Focus();

                    }
                }
                else
                {
                    MessageBox.Show("Deberá asignar un número a la OC", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    e.Handled = true;
                    txtNumeroOC.Focus();
                }

            }
        }

        private void estadoOrdenCompra(bool estado)
        {
            txtNumeroOC.Enabled = estado;
            txtFechaOC.Enabled = estado;
        }


        private void chkOrden_CheckedChanged(object sender, EventArgs e)
        {
            txtNumeroOC.Clear();

            if (chkOrden.Checked)
            {
                estadoOrdenCompra(true);
                txtFechaOC.Text = DateTime.Now.ToString("dd/MM/yyyy");
                
            }
            else
            {
                estadoOrdenCompra(false);
                txtFechaOC.Clear();
            }
            
        }

        private void btnTienda_Click(object sender, EventArgs e)
        {
            string codclie = txtCliente.Text.Trim();
            if (codclie != string.Empty)
            {
                Clie clie = new Clie();
                string nombre = clie.ui_getDatos(codclie, "NOMBRE");
                if (nombre == string.Empty)
                {
                    MessageBox.Show("Código de Cliente no existe", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
                else
                {
                    ui_estaclie ui_estaclie = new ui_estaclie();
                    ui_estaclie._FormPadre = this;
                    ui_estaclie.Activate();
                    ui_estaclie.setValores("ui_updguiaremi");
                    ui_estaclie._codclie = codclie;
                    ui_estaclie._desclie = clie.ui_getDatos(codclie, "NOMBRE");
                    ui_estaclie._rucclie = clie.ui_getDatos(codclie, "RUC");
                    ui_estaclie._dniclie = clie.ui_getDatos(codclie, "DNI");
                    ui_estaclie.BringToFront();
                    ui_estaclie.ShowDialog();
                    ui_estaclie.Dispose();
                }
            }
            else
            {
                MessageBox.Show("No ha ingresado Código de Cliente a editar", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void cmbTienda_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(Keys.Enter))
            {
                if (cmbTienda.Text != String.Empty)
                {
                    Funciones funciones = new Funciones();
                    string codclie = txtCliente.Text;
                    string codesta = funciones.getValorComboBox(cmbTienda, 5);
                    string query = "SELECT codesta as clave,desesta as descripcion FROM estaclie ";
                    query = query + "WHERE codesta='" + @codesta + "' and codclie='" + @codclie + "';";
                    funciones.validarCombobox(query, cmbTienda);


                }
                e.Handled = true;
                txtRazonTrans.Focus();

            }
        }

        private void txtAlbaran_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                Funciones funciones = new Funciones();
                if (txtAlbaran.Text.Trim() != string.Empty)
                {
                    if (txtAlbaran.Text.Length == 7)
                    {
                        e.Handled = true;
                        chkOrden.Focus();
                    }
                    else
                    {
                        txtAlbaran.Text = funciones.replicateCadena("0", 7 - txtAlbaran.Text.Trim().Length) + txtAlbaran.Text.Trim();
                        e.Handled = true;
                        chkOrden.Focus();

                    }
                }
                else
                {
                    MessageBox.Show("Deberá asignar un número ALBARAN", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    e.Handled = true;
                    txtAlbaran.Focus();
                }

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ui_updalarti ui_updalarti = new ui_updalarti();
            ui_updalarti._FormPadre = this;
            ui_updalarti._codcia = this._codcia;
            ui_updalarti._clasePadre = "ui_updguiaremi";
            ui_updalarti.Activate();
            ui_updalarti.agregar();
            ui_updalarti.ui_ActualizaComboBox();
            ui_updalarti.BringToFront();
            ui_updalarti.ShowDialog();
            ui_updalarti.Dispose();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string codarti = txtCodigo.Text.Trim();

            if (codarti != string.Empty)
            {
                AlArti alarti = new AlArti();
                string codcia = this._codcia;

                string nombre = alarti.ui_getDatos(codcia, codarti, "NOMBRE");
                if (nombre == string.Empty)
                {
                    MessageBox.Show("Código del Producto a editar no existe", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
                else
                {
                    ui_updalarti ui_updalarti = new ui_updalarti();
                    ui_updalarti._FormPadre = this;
                    ui_updalarti._clasePadre = "ui_updguiaremi";
                    ui_updalarti._codcia = this._codcia;
                    ui_updalarti.ui_ActualizaComboBox();
                    ui_updalarti.Activate();
                    ui_updalarti.BringToFront();
                    ui_updalarti.editar(codarti);
                    ui_updalarti.ShowDialog();
                    ui_updalarti.Dispose();
                }
            }
            else
            {
                MessageBox.Show("No ha ingresado Código de Producto a editar", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

       
    }
}
