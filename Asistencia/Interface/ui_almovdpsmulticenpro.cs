using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Configuration;

namespace CaniaBrava
{
    public partial class ui_almovdpsmulticenpro : ui_form
    {
        public string _codcia;
        public string _alma;
        public string _td;
        public string _numdoc;
        public string _formpadre;
        string _operacion;
        string _item;

        private TextBox TextBoxActivo;

        public TextBox _TextBoxActivo
        {
            get { return TextBoxActivo; }
            set { TextBoxActivo = value; }
        }

        private Form FormPadre;

        public Form _FormPadre
        {
            get { return FormPadre; }
            set { FormPadre = value; }
        }

        public ui_almovdpsmulticenpro()
        {
            InitializeComponent();
        }

        private void ui_almovdpsmulticenpro_Load(object sender, EventArgs e)
        {

        }

        private void ui_agregaritem(string seccion)
        {
            lstSeccion.Items.Add(seccion);
        }

        public void ui_ActualizaComboBox()
        {
            MaesGen maesgen = new MaesGen();
            Funciones funciones = new Funciones();
            string query;
            string codalma = this._alma;
            string codcia = this._codcia;
            query = "SELECT A.codcenpro as clave,B.descenpro as descripcion FROM cenproalm A left join cenpro B ";
            query = query + " on A.codcia=B.codcia and A.codcenpro=B.codcenpro ";
            query = query + " WHERE A.codcia='" + @codcia + "' and A.codalma='" + @codalma + "' order by A.codcenpro asc;";
            funciones.listaComboBox(query, cmbCenPro, "");
            maesgen.listaDetMaesGen("700", cmbTipoSeccion, "");
            string tiposec = funciones.getValorComboBox(cmbTipoSeccion, 4);
            string codcenpro = funciones.getValorComboBox(cmbCenPro, 4);
            ui_listaSecciones(codcia, codcenpro, tiposec);
        }

        public string ui_getPlantas(string codcenpro, string valvula)
        {
            string query;
            string resultado = "0";
            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();
            query = " Select valvula,CASE ISNULL(SUM(cantidad)) WHEN 1 THEN 0 WHEN 0 THEN SUM(cantidad) END ";
            query = query + " as cantidad from agro.censo where existe='P' and codcenpro='" + @codcenpro + "' and valvula='" + @valvula + "';";
            try
            {
                SqlCommand myCommand = new SqlCommand(query, conexion);
                SqlDataReader myReader = myCommand.ExecuteReader();
                if (myReader.Read())
                {
                    resultado = myReader.GetString(myReader.GetOrdinal("cantidad"));
                }
                myReader.Close();
                myCommand.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            conexion.Close();
            return resultado;
        }


        public void ui_listaSecciones(string codcia, string codcenpro, string tiposec)
        {
            codcenpro = codcenpro.Substring(2, 2);
            Funciones funciones = new Funciones();
            //MessageBox.Show(codcenpro); 
            //string query = "SELECT distinct valvula as descripcion from agro.censo where codcenpro='" + @codcenpro + "' order by valvula asc;";
            string query = "SELECT ";
            query += "      CASE WHEN LENGTH(c_valvula) = 3 THEN ";
            query += "        RIGHT(CONCAT('0000',REPLACE(CAST(c_valvula AS CHAR), '0', 'A')),4) ";
            query += "      ELSE ";
            query += "        RIGHT(CONCAT('0000',CAST(c_valvula AS CHAR)),4) ";
            query += "      END AS descripcion ";
            query += "FROM agro.censo WHERE c_cenpro = " + int.Parse(codcenpro) + " GROUP BY c_valvula";
            funciones.listaComboBoxUnCampo(query, cmbSeccion, "");
        }

        public void agregar()
        {
            this._operacion = "AGREGAR";
            lblTipo.Text = this._td;
            lblNroMov.Text = this._numdoc;
            txtCodigo.Clear();
            txtDescri.Text = "";
            txtUnidad.Text = "";
            ui_listaLotes("", "", "");
            txtStock.Text = "0";
            txtCantidad.Text = "0";
            btnAceptar.Enabled = true;
            txtCodigo.Focus();
        }

        public void editar(string item)
        {
            string query;
            this._operacion = "EDITAR";
            lblTipo.Text = this._td;
            lblNroMov.Text = this._numdoc;
            string codcia = this._codcia;
            string alma = this._alma;
            string td = this._td.Substring(0, 2);
            string numdoc = this._numdoc;
            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();
            query = "SELECT * FROM almovd where codcia='" + @codcia + "' and alma='" + @alma + "' and td='" + @td + "' ";
            query = query + " and numdoc='" + @numdoc + "' and item='" + @item + "'; ";
            try
            {
                SqlCommand myCommand = new SqlCommand(query, conexion);
                SqlDataReader myReader = myCommand.ExecuteReader();

                if (myReader.Read())
                {
                    Funciones funciones = new Funciones();
                    MaesGen maesgen = new MaesGen();
                    this._item = myReader.GetString(myReader.GetOrdinal("item"));
                    AlArti alarti = new AlArti();
                    txtCodigo.Text = myReader.GetString(myReader.GetOrdinal("codarti"));
                    txtDescri.Text = alarti.ui_getDatos(codcia, myReader.GetString(myReader.GetOrdinal("codarti")), "NOMBRE");
                    txtUnidad.Text = alarti.ui_getDatos(codcia, myReader.GetString(myReader.GetOrdinal("codarti")), "UNIDAD");
                    Alstock alstock = new Alstock();
                    ui_listaLotes(codcia, alma, myReader.GetString(myReader.GetOrdinal("codarti")));
                    txtStock.Text = alstock.ui_getStock(codcia, alma, myReader.GetString(myReader.GetOrdinal("codarti")));
                    txtCantidad.Text = myReader.GetString(myReader.GetOrdinal("cantidad"));
                    query = " SELECT codcenpro as clave,descenpro as descripcion FROM cenpro WHERE ";
                    query = query + " codcia='" + @myReader.GetString(myReader.GetOrdinal("codcia")) + "' and codcenpro='" + @myReader.GetString(myReader.GetOrdinal("codcenpro")) + "';";
                    funciones.consultaComboBox(query, cmbCenPro);
                    maesgen.consultaDetMaesGen("700", myReader.GetString(myReader.GetOrdinal("tiposec")), cmbTipoSeccion);
                    query = " SELECT codseccion as clave,desseccion as descripcion FROM secciones WHERE ";
                    query = query + " codcia='" + @myReader.GetString(myReader.GetOrdinal("codcia")) + "' and ";
                    query = query + " codcenpro='" + @myReader.GetString(myReader.GetOrdinal("codcenpro")) + "' and tiposec='" + @myReader.GetString(myReader.GetOrdinal("tiposec")) + "' ";
                    query = query + " and codseccion='" + @myReader.GetString(myReader.GetOrdinal("codseccion")) + "';";
                    funciones.consultaComboBox(query, cmbSeccion);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            conexion.Close();
            btnAceptar.Enabled = false;
            txtCodigo.Focus();
        }


        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            string codcia = this._codcia;
            string alma = this._alma;
            string codarti = txtCodigo.Text.Trim();
            string lote = cmbLote.Text.Trim();
            string valorValida = valida(codcia, alma, codarti, lote);
            if (valorValida.Equals("G"))
            {
                try
                {
                    Funciones funciones = new Funciones();
                    Alstock alstock = new Alstock();

                    string operacion = this._operacion;
                    string td = this._td.Substring(0, 2);
                    string numdoc = this._numdoc;
                    string certificado = "0";
                    string fprod = "", fven = "", analisis = "", calidad = "", glosana1 = "", glosana2 = "", glosana3 = "";
                    string codcenpro = funciones.getValorComboBox(cmbCenPro, 4);
                    string tiposec = funciones.getValorComboBox(cmbTipoSeccion, 4);
                    string codseccion = string.Empty;
                    float totalcantidad = float.Parse(txtCantidad.Text);
                    AlmovD almovd = new AlmovD();

                    string item;
                    int nroTotalPlantas = 0;
                    string codcenprocenso = codcenpro.Substring(2, 2);

                    foreach (string itemlst in lstSeccion.Items)
                    {
                        nroTotalPlantas = nroTotalPlantas + int.Parse(ui_getPlantas(codcenprocenso, itemlst));
                    }

                    float cantidadPorcentual = 0;
                    foreach (string itemlst in lstSeccion.Items)
                    {
                        codseccion = itemlst;
                        item = almovd.genCod(codcia, alma, td, numdoc);
                        int nroPlantas = int.Parse(ui_getPlantas(codcenprocenso, codseccion));
                        glosana1 = "Nro. Plantas " + nroPlantas.ToString();
                        cantidadPorcentual = (totalcantidad * nroPlantas) / nroTotalPlantas;
                        almovd.updAlmovD(operacion, codcia, alma, td, numdoc, item, codarti, cantidadPorcentual, certificado, lote,
                        fprod, fven, analisis, calidad, glosana1, glosana2, glosana3, codcenpro, tiposec, codseccion, "");
                    }

                    alstock.recalcularStockProducto(codcia, alma, codarti);

                    if (this._formpadre.Equals("ui_updalmov"))
                    {
                        ((ui_updalmov)FormPadre).ui_listaItem();
                    }

                    if (this._formpadre.Equals("ui_updsalcenpro"))
                    {
                        ((ui_updsalcenpro)FormPadre).ui_listaItem();
                    }

                    if (this._operacion.Equals("AGREGAR"))
                    {
                        agregar();
                    }
                    else
                    {
                        this.Close();
                    }

                }
                catch (FormatException ex)
                {
                    MessageBox.Show(ex.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void ui_updcencos_Load(object sender, EventArgs e)
        {

        }

        private void txtCodigo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                this._TextBoxActivo = txtCodigo;
                ui_viewarti ui_viewarti = new ui_viewarti();
                ui_viewarti._FormPadre = this;
                ui_viewarti._codcia = this._codcia;
                ui_viewarti._clasePadre = "ui_almovdpsmulticenpro";
                ui_viewarti._condicionAdicional = string.Empty;
                ui_viewarti.BringToFront();
                ui_viewarti.ShowDialog();
                ui_viewarti.Dispose();
            }
        }

        private string valida(string codcia, string alma, string codarti, string lote)
        {
            Funciones funciones = new Funciones();
            MaesGen maesgen = new MaesGen();

            string valorValida = "G";

            if (txtCodigo.Text == string.Empty && valorValida == "G")
            {
                valorValida = "B";
                MessageBox.Show("No ha ingresado Código de Producto", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtCodigo.Focus();
            }

            if (cmbCenPro.Text == string.Empty && valorValida == "G")
            {
                valorValida = "B";
                MessageBox.Show("No ha seleccionado Centro Productivo", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cmbCenPro.Focus();
            }

            if (cmbCenPro.Text != string.Empty && valorValida == "G")
            {
                string codcenpro = funciones.getValorComboBox(cmbCenPro, 4);
                string query = "SELECT codcenpro as clave,descenpro as descripcion FROM cenpro WHERE codcia='" + @codcia + "' and codcenpro='" + @codcenpro + "';";
                string resultado = funciones.verificaItemComboBox(query, cmbCenPro);
                if (resultado.Trim() == string.Empty)
                {
                    valorValida = "B";
                    MessageBox.Show("Dato incorrecto en Centro Productivo", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    cmbCenPro.Focus();
                }
            }

            if (cmbTipoSeccion.Text == string.Empty && valorValida == "G")
            {
                valorValida = "B";
                MessageBox.Show("No ha seleccionado Tipo de Sección", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cmbTipoSeccion.Focus();
            }
            if (cmbTipoSeccion.Text != string.Empty && valorValida == "G")
            {
                string resultado = maesgen.verificaComboBoxMaesGen("700", cmbTipoSeccion.Text.Trim());
                if (resultado.Trim() == string.Empty)
                {
                    valorValida = "B";
                    MessageBox.Show("Dato incorrecto en Tipo de Sección", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    cmbTipoSeccion.Focus();
                }
            }

            if (valorValida == "G")
            {

                try
                {
                    float cantidad = float.Parse(txtCantidad.Text);
                    Alstock alstock = new Alstock();
                    float stockdisp = float.Parse(alstock.ui_getStockLote(codcia, alma, codarti, lote));

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
            return valorValida;
        }

        private void ui_almovd_Load(object sender, EventArgs e)
        {

        }

        private void txtCodigo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(Keys.Enter))
            {
                AlArti alarti = new AlArti();
                string alma = this._alma;
                string codcia = this._codcia;
                string codarti = txtCodigo.Text.Trim();
                string nombre = alarti.ui_getDatos(codcia, codarti, "NOMBRE");
                if (nombre == string.Empty || codarti == string.Empty)
                {
                    txtCodigo.Text = "";
                    txtDescri.Text = "";
                    txtUnidad.Text = "";
                    e.Handled = true;
                    txtCodigo.Focus();
                }
                else
                {
                    Alstock alstock = new Alstock();
                    string codigo = alarti.ui_getDatos(codcia, codarti, "CODIGO");
                    txtCodigo.Text = codigo;
                    txtDescri.Text = alarti.ui_getDatos(codcia, codarti, "NOMBRE");
                    txtUnidad.Text = alarti.ui_getDatos(codcia, codarti, "UNIDAD");
                    alstock.recalcularStockProducto(codcia, alma, codigo);
                    txtStock.Text = alstock.ui_getStock(codcia, alma, codigo);
                    e.Handled = true;
                    cmbCenPro.Focus();
                }

                ui_listaLotes(codcia, alma, txtCodigo.Text.Trim());

            }
        }

        private void txtCantidad_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                try
                {
                    float numero = float.Parse(txtCantidad.Text);
                    e.Handled = true;
                    toolStripForm.Items[0].Select();
                    toolStripForm.Focus();
                }
                catch (FormatException)
                {
                    MessageBox.Show("Cantidad no válida", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    txtCantidad.Text = "0";
                    txtCantidad.Focus();
                }

            }
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

            string query = " select A.lote,B.fecdoc,B.fprod,B.fven,A.stock ";
            query = query + " from alsklote A  left join vista_lotes B on A.codcia=B.codcia ";
            query = query + " and A.alma=B.alma and A.codarti=B.codarti and A.lote=B.lote ";
            query = query + " where A.codcia='" + @codcia + "' and A.alma='" + @alma + "' and A.codarti='" + @codarti + "' ";
            query = query + " and format(A.stock,3)>0 " + @ordena + " ;";

            try
            {
                using (SqlDataAdapter myDataAdapter = new SqlDataAdapter(query, conexion))
                {
                    DataSet myDataSet = new DataSet();
                    myDataAdapter.Fill(myDataSet, "tblData");
                    funciones.formatearDataGridView(dgvData);
                    dgvData.DataSource = myDataSet.Tables["tblData"];
                    dgvData.Columns[0].HeaderText = "Lote";
                    dgvData.Columns[1].HeaderText = "Fecha de Ingreso del Lote";
                    dgvData.Columns[2].HeaderText = "F. Producción";
                    dgvData.Columns[3].HeaderText = "F. Vencimiento";
                    dgvData.Columns[4].HeaderText = "Stock Disponible";
                    dgvData.Columns[0].Width = 120;
                    dgvData.Columns[1].Width = 100;
                    dgvData.Columns[2].Width = 100;
                    dgvData.Columns[3].Width = 100;
                    dgvData.Columns[4].Width = 100;
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

        private void cmbCenPro_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(Keys.Enter))
            {
                if (cmbCenPro.Text != String.Empty)
                {
                    Funciones funciones = new Funciones();
                    string codcia = this._codcia;
                    string codcenpro = funciones.getValorComboBox(cmbCenPro, 4);
                    string tiposec = funciones.getValorComboBox(cmbTipoSeccion, 4);
                    string query = "SELECT codcenpro as clave,descenpro as descripcion FROM cenpro ";
                    query = query + "WHERE codcia='" + @codcia + "' and codcenpro='" + @codcenpro + "';";
                    funciones.validarCombobox(query, cmbCenPro);
                    ui_listaSecciones(codcia, codcenpro, tiposec);
                    e.Handled = true;
                    SendKeys.Send("{TAB}");
                }
            }
        }

        private void cmbTipoSeccion_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(Keys.Enter))
            {
                if (cmbTipoSeccion.Text != String.Empty)
                {
                    Funciones funciones = new Funciones();
                    MaesGen maesgen = new MaesGen();
                    maesgen.validarDetMaesGen("700", cmbTipoSeccion, cmbTipoSeccion.Text);
                    string codcia = this._codcia;
                    string codcenpro = funciones.getValorComboBox(cmbCenPro, 4);
                    string tiposec = funciones.getValorComboBox(cmbTipoSeccion, 4);
                    ui_listaSecciones(codcia, codcenpro, tiposec);
                }
                e.Handled = true;
                SendKeys.Send("{TAB}");
            }
        }

        private void cmbSeccion_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(Keys.Enter))
            {
                if (cmbSeccion.Text != String.Empty)
                {
                    Funciones funciones = new Funciones();
                    string codcia = this._codcia;
                    string codcenpro = funciones.getValorComboBox(cmbCenPro, 4);
                    string tiposec = funciones.getValorComboBox(cmbTipoSeccion, 4);
                    string codseccion = funciones.getValorComboBox(cmbSeccion, 4);
                    string query = "SELECT codseccion as clave,desseccion as descripcion FROM secciones ";
                    query = query + "WHERE codcia='" + @codcia + "' and tiposec='" + @tiposec + "' and codseccion='" + @codseccion + "';";
                    funciones.validarCombobox(query, cmbSeccion);
                    e.Handled = true;
                    SendKeys.Send("{TAB}");
                }
            }
        }

        private void cmbCenPro_SelectedIndexChanged(object sender, EventArgs e)
        {
            Funciones funciones = new Funciones();
            string codcia = this._codcia;
            string codcenpro = funciones.getValorComboBox(cmbCenPro, 4);
            string tiposec = funciones.getValorComboBox(cmbTipoSeccion, 4);
            ui_listaSecciones(codcia, codcenpro, tiposec);
        }

        private void cmbTipoSeccion_SelectedIndexChanged(object sender, EventArgs e)
        {
            Funciones funciones = new Funciones();
            string codcia = this._codcia;
            string codcenpro = funciones.getValorComboBox(cmbCenPro, 4);
            string tiposec = funciones.getValorComboBox(cmbTipoSeccion, 4);
            ui_listaSecciones(codcia, codcenpro, tiposec);
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            string item = cmbSeccion.Text;
            string itemInQuestion = item;

            if (lstSeccion.Items.Contains(itemInQuestion))
            {
                MessageBox.Show("La sección " + item + " ya se encuentra en la lista...");
            }
            else
            {
                ui_agregaritem(item);
            }

        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (lstSeccion.SelectedIndex != -1)
            {
                DialogResult opcion = MessageBox.Show("Esta seguro de eliminar : \n" + lstSeccion.SelectedItem.ToString(), "Eliminar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (opcion == DialogResult.Yes)
                {
                    lstSeccion.Items.RemoveAt(lstSeccion.SelectedIndex);
                }
            }
            else
            {
                MessageBox.Show("Debe seleccionar un elemento a eliminar...");
            }
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
    }
}