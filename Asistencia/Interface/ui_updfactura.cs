using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Configuration;
using System.Data.SqlClient;

namespace CaniaBrava
{
    public partial class ui_updfactura : Form
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
        float _igv;
        string _usuario;
   

        public void setData(string codcia, string codcaja, 
            string descaja,string descia)
        {
            this._codcia = codcia;
            this._codcaja = codcaja;
            this._descaja = descaja;
            this._descia = descia;
   
        }

        public ui_updfactura()
        {
            InitializeComponent();
        }

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

        private void ui_updfactura_Load(object sender, EventArgs e)
        {
            GlobalVariables gv = new GlobalVariables();
            this._usuario = gv.getValorUsr();
            this.lblEmpresa.Text = this._descia;
            this.lblCaja.Text = this._descaja;
        }

        private void agregarItem()
        {
            this._opeitem = "AGREGAR";
            ui_limpiarItem();
            ui_habilitarItem(true);
            txtCantidad.Text = "0";
            txtPrecio.Text = "0";
            txtSubTotalItem.Text = "0";
            txtDescItem.Text = "0";
            txtNetoItem.Text = "0";
            txtIgvItem.Text = "0";
            txtTotalItem.Text = "0";
            txtCodigo.Focus();

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


        private void ui_habilitarItem(bool estado)
        {
            txtCodigo.Enabled = estado;
            txtCantidad.Enabled = estado;
            txtPrecio.Enabled = estado;
            txtDescItem.Enabled = estado;
            txtIgvItem.Enabled = estado;
        }

        private void ui_limpiarItem()
        {
            txtCodigo.Text = string.Empty;
            txtDescri.Text = string.Empty;
            txtUnidad.Text = string.Empty;
            txtCantidad.Text = string.Empty;
            txtPrecio.Text = string.Empty;
            txtSubTotalItem.Text = string.Empty;
            txtDescItem.Text = string.Empty;
            txtNetoItem.Text = string.Empty;
            txtIgvItem.Text = string.Empty;
            txtTotalItem.Text = string.Empty;
            ui_listaLotes("","", "");
        }

        public void ui_actualizaComboBox()
        {
            MaesGen maesgen = new MaesGen();
            maesgen.listaDetMaesGen("180", cmbTipoDoc, "");
            maesgen.listaDetMaesGen("170", cmbMoneda, "");
            maesgen.listaDetMaesGen("190", cmbConver, "");
        }
        
        public void agregar()
        {
            this._finaliza = "N";
            this._operacion = "AGREGAR";
            cmbTipoDoc.Enabled = true;
            txtSerie.Enabled = true;
            txtNumero.Enabled = true;
            btnNuevo.Visible = false;
            btnGrabar.Visible = false;
            this.ui_actualizaComboBox();
            this.ui_limpiar();
            this.ui_limpiarItem();
            ui_listaItem();
            cmbTipoDoc.Focus();
        }


        public void editar(string alma, string td, string numdoc)
        {
            string query;
            this._operacion = "EDITAR";
            cmbTipoDoc.Enabled = false;
            txtSerie.Enabled = false;
            txtNumero.Enabled = false;
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
                    this._alma = myReader.GetString(myReader.GetOrdinal("alma"));
                    this._codmov=myReader.GetString(myReader.GetOrdinal("codmov"));
                    this._numdoc = myReader.GetString(myReader.GetOrdinal("numdoc"));
                    maesgen.consultaDetMaesGen("180", myReader.GetString(myReader.GetOrdinal("rftdoc")), cmbTipoDoc);
                    txtSerie.Text = myReader.GetString(myReader.GetOrdinal("rfserie"));
                    txtNumero.Text = myReader.GetString(myReader.GetOrdinal("rfnro"));
                    txtFechaDoc.Text = myReader.GetString(myReader.GetOrdinal("fecdoc"));
                    maesgen.consultaDetMaesGen("170", myReader.GetString(myReader.GetOrdinal("codmon")), cmbMoneda);
                    maesgen.consultaDetMaesGen("190", myReader.GetString(myReader.GetOrdinal("tipconver")), cmbConver);
                    txtTC.Text = myReader.GetString(myReader.GetOrdinal("tipcam"));
                    txtCliente.Text = myReader.GetString(myReader.GetOrdinal("codclie"));
                    Clie clie = new Clie();
                    txtRazon.Text= clie.ui_getDatos(myReader.GetString(myReader.GetOrdinal("codclie")), "NOMBRE");
                    txtRuc.Text = clie.ui_getDatos(myReader.GetString(myReader.GetOrdinal("codclie")), "RUC");
                    txtDir.Text = clie.ui_getDatos(myReader.GetString(myReader.GetOrdinal("codclie")), "DIRECCION1");
                    txtGlosa.Text = myReader.GetString(myReader.GetOrdinal("glosa1"));
                    txtFecCrea.Text = myReader.GetString(myReader.GetOrdinal("fcrea"));
                    txtFecMod.Text = myReader.GetString(myReader.GetOrdinal("fmod"));
                    txtUsuario.Text = myReader.GetString(myReader.GetOrdinal("usuario"));
                    ui_listaLotes(codcia,myReader.GetString(myReader.GetOrdinal("alma")), "");
                    
                    if (myReader.GetString(myReader.GetOrdinal("situafac")).Equals("V"))
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
                        if (myReader.GetString(myReader.GetOrdinal("situafac")).Equals("F"))
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
                cmbTipoDoc.Focus();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }

            conexion.Close();
        }

        private void ui_limpiar()
        {
            cmbTipoDoc.Text=string.Empty;
            txtSerie.Text=string.Empty;
            txtNumero.Text = string.Empty;
            cmbMoneda.Text = string.Empty;
            cmbConver.Text = string.Empty;
            txtTC.Text = string.Empty;
            txtFechaDoc.Text=DateTime.Now.ToString("dd/MM/yyyy");
            txtRazon.Text=string.Empty;
            txtDir.Text=string.Empty;
            txtRuc.Text=string.Empty;
            txtGlosa.Text=string.Empty;
            txtFecCrea.Text=DateTime.Now.ToString("dd/MM/yyyy");
            txtFecMod.Text=DateTime.Now.ToString("dd/MM/yyyy");
            txtUsuario.Text = this._usuario;
            txtEstado.Text = "VIGENTE";
        }


             

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            ((ui_factura)FormPadre).btnActualizar.PerformClick();
            this.Close();
        }

        private string ui_valida()
        {
            string valorValida = "G";
            MaesGen maesgen = new MaesGen();

            if (cmbTipoDoc.Text == string.Empty && valorValida == "G")
            {
                valorValida = "B";
                MessageBox.Show("No ha seleccionado Tipo de Documento", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cmbTipoDoc.Focus();
            }

            if (cmbTipoDoc.Text != string.Empty && valorValida == "G")
            {
                string resultado = maesgen.verificaComboBoxMaesGen("180", cmbTipoDoc.Text.Trim());
                if (resultado.Trim() == string.Empty)
                {
                    valorValida = "B";
                    MessageBox.Show("Dato incorrecto en Tipo de Documento", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    cmbTipoDoc.Focus();
                }
            }
            
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

            if (cmbMoneda.Text == string.Empty && valorValida == "G")
            {
                valorValida = "B";
                MessageBox.Show("No ha seleccionado Moneda", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cmbMoneda.Focus();
            }

            if (cmbMoneda.Text != string.Empty && valorValida == "G")
            {
                string resultado = maesgen.verificaComboBoxMaesGen("170", cmbMoneda.Text.Trim());
                if (resultado.Trim() == string.Empty)
                {
                    valorValida = "B";
                    MessageBox.Show("Dato incorrecto en Moneda", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    cmbMoneda.Focus();
                }
            }

            if (cmbConver.Text == string.Empty && valorValida == "G")
            {
                valorValida = "B";
                MessageBox.Show("No ha seleccionado Tipo de Conversión", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cmbConver.Focus();
            }

            if (cmbConver.Text != string.Empty && valorValida == "G")
            {
                string resultado = maesgen.verificaComboBoxMaesGen("190", cmbConver.Text.Trim());
                if (resultado.Trim() == string.Empty)
                {
                    valorValida = "B";
                    MessageBox.Show("Dato incorrecto en Tipo de Conversión", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    cmbConver.Focus();
                }
            }

            if (valorValida == "G")
            {
                try
                {
                    float numero = float.Parse(txtTC.Text);
                    if (numero <= 0)
                    {
                        valorValida = "B";
                        MessageBox.Show("Tipo de Cambio no válido", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txtTC.Clear();
                        txtTC.Focus();
                    }

                }
                catch (FormatException)
                {
                    valorValida = "B";
                    MessageBox.Show("Tipo de Cambio no válido", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtTC.Clear();
                    txtTC.Focus();
                }
            }

            if (txtCliente.Text == string.Empty && valorValida == "G")
            {
                valorValida = "B";
                MessageBox.Show("No ha ingresado Cliente", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtCliente.Focus();
            }
            
            Clie clie = new Clie();
            if (txtCliente.Text != string.Empty && valorValida == "G" && clie.ui_getDatos(txtCliente.Text, "NOMBRE") == string.Empty)
            {
                valorValida = "B";
                MessageBox.Show("Cliente no existe", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtCliente.Focus();
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
                    string operacion = this._operacion;
                    string codcia = this._codcia;
                    string codcaja = this._codcaja;
                    string td="PS";
                    string fecdoc = txtFechaDoc.Text;
                    string rftdoc = funciones.getValorComboBox(cmbTipoDoc, 4);
                    string codmon=funciones.getValorComboBox(cmbMoneda, 4);
                    string tipconver = funciones.getValorComboBox(cmbConver, 4);
                    string tipcam = txtTC.Text;
                    
                    if (txtSerie.Text.Length < 4)
                    {
                        txtSerie.Text = funciones.replicateCadena("0", 4 - txtSerie.Text.Trim().Length) + txtSerie.Text.Trim();
                    }

                    if (txtNumero.Text.Length < 8)
                    {
                        txtNumero.Text = funciones.replicateCadena("0", 8 - txtNumero.Text.Trim().Length) + txtNumero.Text.Trim();
                    }

                    string rfndoc = txtSerie.Text.Trim()+"-"+txtNumero.Text.Trim();
                    string rfserie = txtSerie.Text.Trim();
                    string rfnro = txtNumero.Text.Trim();
                    string codclie = txtCliente.Text;
                    string glosa = txtGlosa.Text.Trim();
                    string situa="F";
                    string situaval="F";
                    string situafac = txtEstado.Text.Substring(0, 1);
                    string fcrea = txtFecCrea.Text;
                    string fmod = DateTime.Now.ToString("dd/MM/yyyy");
                    string usuario = this._usuario;
                    string alma;
                    string codmov;
                    string numdoc;
                    
                    if (operacion.Equals("AGREGAR"))
                    {
                        Cajas cajas = new Cajas();
                        alma = cajas.ui_getDatos(codcia, codcaja, "ALMAFAC");
                        codmov = cajas.ui_getDatos(codcia, codcaja, "MOVFAC");
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

                    almovc.updFactura(operacion, codcia, codcaja, alma, numdoc, fecdoc, codmov, situa,
                        rftdoc, rfndoc, glosa, codmon, codclie, situaval, fcrea, fmod, usuario, 
                        situafac,rfserie,rfnro,tipconver,tipcam,"FTCS");
                    
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
            string codcia = this._codcia;
            string alma = this._alma;
            string td = "PS";
            string numdoc = this._numdoc;
            
            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();
            string query = " select A.item,A.codarti,B.desarti,A.lote,A.cantidad,A.preuni,A.preneto,";
            query = query + " A.descuento,A.neto,A.igv,A.total ";
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
                    dgvData.Columns[3].HeaderText = "Lote";
                 


                    dgvData.Columns[4].HeaderText = "Cantidad";
                    dgvData.Columns[5].HeaderText = "P.U.";
                    dgvData.Columns[6].HeaderText = "Subtotal";
                    dgvData.Columns[7].HeaderText = "Descuento";
                    dgvData.Columns[8].HeaderText = "Neto";
                    dgvData.Columns[9].HeaderText = "I.G.V.";
                    dgvData.Columns[10].HeaderText = "Total";
                    
                    if (dgvData.Rows.Count > 2)
                    {
                        dgvData.CurrentCell = dgvData.Rows[dgvData.Rows.Count - 1].Cells[0];
                    }

                    dgvData.Columns[0].Width = 40;
                    dgvData.Columns[1].Width = 70;
                    dgvData.Columns[2].Width = 270;
                    dgvData.Columns[3].Width = 120;
                  
                    dgvData.Columns[4].Width = 60;
                    dgvData.Columns[5].Width = 60;
                    dgvData.Columns[6].Width = 70;
                    dgvData.Columns[7].Width = 70;
                    dgvData.Columns[8].Width = 70;
                    dgvData.Columns[9].Width = 70;
                    dgvData.Columns[10].Width = 70;

                    dgvData.Columns[6].DefaultCellStyle.Format = "###,###.##";
                    dgvData.Columns[7].DefaultCellStyle.Format = "###,###.##";
                    dgvData.Columns[8].DefaultCellStyle.Format = "###,###.##";
                    dgvData.Columns[9].DefaultCellStyle.Format = "###,###.##";
                    dgvData.Columns[10].DefaultCellStyle.Format = "###,###.##";

                    float subtotal = float.Parse(funciones.sumaColumnaDataGridView(dgvData, 6));
                    txtSubtotal.Text = subtotal.ToString("0.##");
                    float desc = float.Parse(funciones.sumaColumnaDataGridView(dgvData, 7));
                    txtDescuento.Text = desc.ToString("0.##");
                    float neto = float.Parse(funciones.sumaColumnaDataGridView(dgvData, 8));
                    txtNeto.Text = neto.ToString("0.##");
                    float igv = float.Parse(funciones.sumaColumnaDataGridView(dgvData, 9));
                    txtIgv.Text = igv.ToString("0.##");
                    float total = float.Parse(funciones.sumaColumnaDataGridView(dgvData, 10));
                    txtTotal.Text = total.ToString("0.##");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            conexion.Close();
        }
        private void cmbTipoDoc_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(Keys.Enter))
            {
                if (cmbTipoDoc.Text != String.Empty)
                {
                    MaesGen maesgen = new MaesGen();
                    maesgen.validarDetMaesGen("180", cmbTipoDoc, cmbTipoDoc.Text);
                }
                
                Funciones funciones = new Funciones();
                string codcia = this._codcia;
                string codcaja = this._codcaja;
                string rftdoc = funciones.getValorComboBox(cmbTipoDoc, 4);
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

                e.Handled = true;
                SendKeys.Send("{TAB}");
            }
           
        }


        private void txtFechaDoc_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                if (UtiFechas.IsDate(txtFechaDoc.Text))
                {
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

        private void txtBusqueda_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(Keys.Enter))
            {
                e.Handled = true;
                SendKeys.Send("{TAB}");
            }
        }

        private void txtRazon_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(Keys.Enter))
            {
                e.Handled = true;
                SendKeys.Send("{TAB}");
            }
        }

        private void txtDir_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(Keys.Enter))
            {
                e.Handled = true;
                SendKeys.Send("{TAB}");
            }
        }

        private void txtRuc_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(Keys.Enter))
            {
                e.Handled = true;
                SendKeys.Send("{TAB}");
            }
        }

        private void txtGlosa_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(Keys.Enter))
            {
                e.Handled = true;
                toolStripForm.Items[0].Select();
                toolStripForm.Focus();
            }
        }

        private void cmbMoneda_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(Keys.Enter))
            {
                if (cmbMoneda.Text != String.Empty)
                {
                    MaesGen maesgen = new MaesGen();
                    maesgen.validarDetMaesGen("170", cmbMoneda, cmbMoneda.Text);
                }
                e.Handled = true;
                SendKeys.Send("{TAB}");
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

        private void txtCliente_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                this._TextBoxActivo = txtCliente;
                ui_viewclientes ui_viewclientes = new ui_viewclientes();
                ui_viewclientes._FormPadre = this;
                ui_viewclientes._clasePadre = "ui_updfactura";
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
                    txtRazon.Text = string.Empty;
                    txtDir.Text = string.Empty;
                    txtRuc.Text = string.Empty;
                    e.Handled = true;
                    txtCliente.Focus();
                }
                else
                {
                    txtCliente.Text = clie.ui_getDatos(codclie, "CODIGO");
                    txtRazon.Text = clie.ui_getDatos(codclie, "NOMBRE");
                    txtDir.Text = clie.ui_getDatos(codclie, "DIRECCION1");
                    txtRuc.Text = clie.ui_getDatos(codclie, "RUC");
                    e.Handled = true;
                    SendKeys.Send("{TAB}");
                }

            }
        }

        private void groupBox3_Enter(object sender, EventArgs e)
        {

        }

        private void cmbTipoDoc_SelectedIndexChanged(object sender, EventArgs e)
        {
            Funciones funciones=new Funciones();
            string codcia = this._codcia;
            string codcaja = this._codcaja;
            string rftdoc = funciones.getValorComboBox(cmbTipoDoc, 4);
            NumDoc numdoc = new NumDoc();
            string numero=numdoc.genNumDoc(codcia, codcaja, rftdoc);
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
        }

        private void txtCodigo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                this._TextBoxActivo = txtCodigo;
                ui_viewarti ui_viewarti = new ui_viewarti();
                ui_viewarti._FormPadre = this;
                ui_viewarti._codcia = this._codcia;
                ui_viewarti._clasePadre = "ui_updfactura";
                ui_viewarti._condicionAdicional = string.Empty;
                ui_viewarti.BringToFront();
                ui_viewarti.ShowDialog();
                ui_viewarti.Dispose();
            }
        }

        private float calculaIgvArticulo(string codcia,string codarti)
        {
            float igv = 0;
            AlArti alarti = new AlArti();
            string exoigv = alarti.ui_getDatos(codcia,codarti, "EXOIGV");
            if (exoigv.Equals("S"))
            {
                igv = 0;
            }
            else
            {
                string esporigv = alarti.ui_getDatos(codcia,codarti, "ESPORIGV");
                if (esporigv.Equals("S"))
                {
                    igv = float.Parse(alarti.ui_getDatos(codcia,codarti, "PORIGV"));
                }
                else
                {
                    SisParm sisparm = new SisParm();
                    igv = float.Parse(sisparm.ui_getDatos("IGV"));
                }
            }
            return igv;
        }

        private void txtCodigo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(Keys.Enter))
            {
                AlArti alarti = new AlArti();
                string codcia = this._codcia;
                string alma = this._alma;
                string codarti = txtCodigo.Text.Trim();
                string codclie=txtCliente.Text.Trim();
                string nombre = alarti.ui_getDatos(codcia,codarti, "NOMBRE");
                if (nombre == string.Empty || codarti == string.Empty)
                {
                    ui_limpiarItem();
                    e.Handled = true;
                    txtCodigo.Focus();
                }
                else
                {
                    Alstock alstock = new Alstock();
                    Clie clie = new Clie();
                    string codigo = alarti.ui_getDatos(codcia,codarti, "CODIGO");
                    string prelibre = alarti.ui_getDatos(codcia,codarti, "PRECIOLIBRE");
                    txtCodigo.Text = codigo;
                    txtDescri.Text = alarti.ui_getDatos(codcia,codarti, "NOMBRE");
                    txtUnidad.Text = alarti.ui_getDatos(codcia,codarti, "UNIDAD");
                    ///////////////////////////////////////////////////////////////////////
                    /////////////////////////////PRECIO////////////////////////////////////
                    ///////////////////////////////////////////////////////////////////////
                    string tippreven=clie.ui_getDatos(codclie, "TIPOPRECIO");
                    if (tippreven.Equals("P"))
                    {
                        txtPrecio.Text = alarti.ui_getDatos(codcia,codarti, "PRECIOPUBLICO");
                    }
                    else
                    {
                        txtPrecio.Text = alarti.ui_getDatos(codcia,codarti, "PRECIODISTRIBUIDOR");
                    }
                    if (prelibre.Equals("S"))
                    {
                        txtPrecio.Enabled = true;
                    }
                    else
                    {
                        txtPrecio.Enabled = false;
                    }
                    ///////////////////////////////////////////////////////////////////////
                    /////////////////////////////STOCK////////////////////////////////////
                    ///////////////////////////////////////////////////////////////////////
                    alstock.recalcularStockProducto(codcia,alma, codigo);
                    ui_listaLotes(codcia,alma, codigo);
                    ///////////////////////////////////////////////////////////////////////
                    /////////////////////////////IGV////////////////////////////////////
                    ///////////////////////////////////////////////////////////////////////
                    this._igv = calculaIgvArticulo(codcia,codarti);
                    string exoigv = alarti.ui_getDatos(codcia,codarti, "EXOIGV");
                    if (exoigv.Equals("S"))
                    {
                        txtIgv.Enabled = false;
                    }
                    else
                    {
                        txtIgv.Enabled = true;
                    }

                    e.Handled = true;
                    txtCantidad.Focus();
                }

            }
        }

        private void calcula()
        {
            float cantidad = float.Parse(txtCantidad.Text);
            float precio = float.Parse(txtPrecio.Text);
            float porcigv = this._igv;
            float desc = float.Parse(txtDescItem.Text);
            txtSubTotalItem.Text = (cantidad * precio).ToString("0.##");
            txtNetoItem.Text = ((cantidad * precio)-desc).ToString("0.##");
            txtIgvItem.Text = (float.Parse(txtNetoItem.Text) * (porcigv / 100)).ToString("0.##");
            txtTotalItem.Text = (float.Parse(txtNetoItem.Text) + float.Parse(txtIgvItem.Text)).ToString("0.##");
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
                    SendKeys.Send("{TAB}");
                }
                catch (FormatException)
                {
                    MessageBox.Show("Cantidad no válida", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    txtCantidad.Clear();
                    txtCantidad.Focus();
                }

            }
        }

        private void txtPrecio_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                try
                {
                    float numero = float.Parse(txtPrecio.Text);
                    calcula();
                    e.Handled = true;
                    SendKeys.Send("{TAB}");
                }
                catch (FormatException)
                {
                    MessageBox.Show("Precio no válido", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    txtPrecio.Clear();
                    txtPrecio.Focus();
                }

            }
        }

        private void txtDescItem_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                try
                {
                    float numero = float.Parse(txtDescItem.Text);
                    calcula();
                    e.Handled = true;
                    SendKeys.Send("{TAB}");
                }
                catch (FormatException)
                {
                    MessageBox.Show("Descuento no válido", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    txtDescItem.Clear();
                    txtDescItem.Focus();
                }

            }
        }

        private void txtIgvItem_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                try
                {
                    float numero = float.Parse(txtIgvItem.Text);
                    txtTotalItem.Text = (float.Parse(txtNetoItem.Text)+float.Parse(txtIgvItem.Text)).ToString("0.##");
                    e.Handled = true;
                    btnGrabar.Focus();
                }
                catch (FormatException)
                {
                    MessageBox.Show("I.G.V. no válido", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    txtIgvItem.Clear();
                    txtIgvItem.Focus();
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
                    float numero = float.Parse(txtPrecio.Text);
                    if (numero < 0)
                    {
                        valorValida = "B";
                        MessageBox.Show("Precio no válido", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txtPrecio.Text="0";
                        txtPrecio.Focus();
                    }

                }
                catch (FormatException)
                {
                    valorValida = "B";
                    MessageBox.Show("Precio no válido", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtPrecio.Clear();
                    txtPrecio.Focus();
                }
            }

            if (valorValida == "G")
            {
                try
                {
                    float numero = float.Parse(txtDescItem.Text);
                    

                }
                catch (FormatException)
                {
                    valorValida = "B";
                    MessageBox.Show("Descuento no válido", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtDescItem.Clear();
                    txtDescItem.Focus();
                }
            }

            if (valorValida == "G")
            {
                try
                {
                    float numero = float.Parse(txtIgvItem.Text);
                    

                }
                catch (FormatException)
                {
                    valorValida = "B";
                    MessageBox.Show("I.G.V. no válido", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtIgvItem.Clear();
                    txtIgvItem.Focus();
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
                    Funciones funciones = new Funciones();
                    string operacion = this._opeitem;
                    string codcia = this._codcia;
                    string alma = this._alma;
                    string td = "PS";
                    string numdoc = this._numdoc;
                    string codarti = txtCodigo.Text.Trim();
                    string cantidad = txtCantidad.Text;
                    string preuni = txtPrecio.Text;
                    string preneto =txtSubTotalItem.Text;
                    string desc = txtDescItem.Text;
                    string neto = txtNetoItem.Text;
                    string igv = txtIgvItem.Text;
                    string total = txtTotalItem.Text;
                    string lote = cmbLote.Text.Trim();
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
                        almovd.updAlmovDFac(operacion, codcia, alma, td, numdoc, item, codarti, cantidad,
                            preuni, preneto, desc, neto, igv, total, lote);
                        alstock.recalcularStockProducto(codcia, alma, codarti);
                        this.ui_listaItem();
                        this.agregarItem();
                    //}
                    //else
                    //{
                    //    MessageBox.Show("El Lote a seleccionar deberá ser el " + loteTurno + ". Método de valoración de Inventario : " + metodo, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    //}
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
                string codcia = this._codcia;
                string alma = this._alma;
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

        private void cmbConver_SelectedIndexChanged(object sender, EventArgs e)
        {
            Funciones funciones = new Funciones();
            TipCam tipcam = new TipCam();
            string fecha = txtFechaDoc.Text;
            string conver = funciones.getValorComboBox(cmbConver, 4);
            txtTC.Text = tipcam.ui_getTipoCambioFechaMax(fecha, conver);
        }

        private void cmbConver_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                if (cmbConver.Text != String.Empty)
                {
                    MaesGen maesgen = new MaesGen();
                    Funciones funciones = new Funciones();
                    TipCam tipcam = new TipCam();
                    maesgen.validarDetMaesGen("190", cmbConver, cmbConver.Text);
                    string fecha = txtFechaDoc.Text;
                    string conver = funciones.getValorComboBox(cmbConver, 4);
                    txtTC.Text = tipcam.ui_getTipoCambioFechaMax(fecha, conver);
                }
                e.Handled = true;
                txtTC.Focus();
            }
        }

        private void txtTC_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                try
                {
                    float numero = float.Parse(txtTC.Text);
                    e.Handled = true;
                    txtCliente.Focus();
                }
                catch (FormatException)
                {
                    MessageBox.Show("Tipo de Cambio no válido", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    txtTC.Clear();
                    txtTC.Focus();
                }

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
                            string codcia = this._codcia;
                            string alma = this._alma;
                            string numdoc = this._numdoc;
                            string fmod = DateTime.Now.ToString("dd/MM/yyyy");
                            string usuario = this._usuario;
                            almovc.updFinFac(codcia,alma, td, numdoc, fmod, usuario);
                            txtEstado.Text = "FINALIZADO";
                            ((ui_factura)FormPadre).btnActualizar.PerformClick();
                            MessageBox.Show("Comprobante Finalizado", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            this.Close();
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

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void txtSubTotalItem_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnNuevoClie_Click(object sender, EventArgs e)
        {
            ui_updclie ui_updclie = new ui_updclie();
            ui_updclie._FormPadre = this;
            ui_updclie.setValores("ui_updfactura");
            ui_updclie.Activate();
            ui_updclie.agregar();
            ui_updclie.ui_actualizaComboBox();
            ui_updclie.BringToFront();
            ui_updclie.ShowDialog();
            ui_updclie.Dispose();
        }

        public void ui_ActualizarClie()
        {
            Clie clie = new Clie();
            string codclie = txtCliente.Text.Trim();
            string nombre = clie.ui_getDatos(codclie, "NOMBRE");
            if (nombre == string.Empty)
            {
                txtCliente.Text = string.Empty;
                txtRazon.Text = string.Empty;
                txtDir.Text = string.Empty;
                txtRuc.Text = string.Empty;
          
            }
            else
            {
                txtCliente.Text = clie.ui_getDatos(codclie, "CODIGO");
                txtRazon.Text = clie.ui_getDatos(codclie, "NOMBRE");
                txtDir.Text = clie.ui_getDatos(codclie, "DIRECCION1");
                txtRuc.Text = clie.ui_getDatos(codclie, "RUC");
              
            }
        }

        private void btnEditarClie_Click(object sender, EventArgs e)
        {
            string codclie = txtCliente.Text.Trim();
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
                ui_updclie.setValores("ui_updfactura");
                ui_updclie.ui_actualizaComboBox();
                ui_updclie.BringToFront();
                ui_updclie.editar(codclie);
                ui_updclie.ShowDialog();
                ui_updclie.Dispose();    
            }
            
        }

        private void groupBox7_Enter(object sender, EventArgs e)
        {

        }

        private void toolStripForm_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

       
    }
}
