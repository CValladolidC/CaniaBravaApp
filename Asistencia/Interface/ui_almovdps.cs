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
    public partial class ui_almovdps : Form
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

        public ui_almovdps()
        {
            InitializeComponent();
        }

        public void agregar()
        {
            this._operacion = "AGREGAR";
            lblTipo.Text = this._td;
            lblNroMov.Text = this._numdoc;
            txtCodigo.Clear();
            txtDescri.Clear();
            txtUnidad.Clear();
            txtFamilia.Clear();
            txtGrupo.Clear();
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
            query = "SELECT * FROM almovd where /*codcia='" + @codcia + "' and */alma='" + @alma + "' and td='" + @td + "' ";
            query = query + " and numdoc='" + @numdoc + "' and item='" + @item + "'; ";
            try
            {
                SqlCommand myCommand = new SqlCommand(query, conexion);
                SqlDataReader myReader = myCommand.ExecuteReader();

                if (myReader.Read())
                {
                    this._item = myReader.GetString(myReader.GetOrdinal("item"));
                    AlArti alarti = new AlArti();
                    txtCodigo.Text = myReader.GetString(myReader.GetOrdinal("codarti"));
                    txtDescri.Text = alarti.ui_getDatos(codcia, myReader.GetString(myReader.GetOrdinal("codarti")), "NOMBRE");
                    txtUnidad.Text = alarti.ui_getDatos(codcia, myReader.GetString(myReader.GetOrdinal("codarti")), "UNIDAD");
                    txtFamilia.Text = alarti.ui_getDatos(codcia, myReader.GetString(myReader.GetOrdinal("codarti")), "FAMILIA");
                    txtGrupo.Text = alarti.ui_getDatos(codcia, myReader.GetString(myReader.GetOrdinal("codarti")), "GRUPO");
                    Alstock alstock = new Alstock();
                    ui_listaLotes(codcia, alma, myReader.GetString(myReader.GetOrdinal("codarti")));
                    txtStock.Text = alstock.ui_getStock(codcia, alma, myReader.GetString(myReader.GetOrdinal("codarti")));
                    txtCantidad.Text = myReader.GetDouble(myReader.GetOrdinal("cantidad")).ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            conexion.Close();
            //btnAceptar.Enabled = false;
            txtCodigo.Focus();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            string codcia = "01";// this._codcia;
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
                    string fprod = "", fven = "", analisis = "", calidad = "", glosana1 = "", glosana2 = "", glosana3 = "", codcenpro = "", tiposec = "", codseccion = "";
                    float cantidad = float.Parse(txtCantidad.Text);
                    AlmovD almovd = new AlmovD();

                    string item;

                    if (this._operacion.Equals("AGREGAR"))
                    {
                        item = almovd.genCod(codcia, alma, td, numdoc);
                    }
                    else
                    {
                        item = this._item;
                    }

                    almovd.updAlmovD(operacion, codcia, alma, td, numdoc, item, codarti, cantidad, certificado, lote,
                        fprod, fven, analisis, calidad, glosana1, glosana2, glosana3, codcenpro, tiposec, codseccion, " ");

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
                ui_viewarti._clasePadre = "ui_almovdps";
                ui_viewarti._condicionAdicional = string.Empty;
                ui_viewarti.BringToFront();
                ui_viewarti.ShowDialog();
                ui_viewarti.Dispose();
            }
        }

        private string valida(string codcia, string alma, string codarti, string lote)
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
                    double cantidad = double.Parse(txtCantidad.Text);
                    Alstock alstock = new Alstock();
                    double stockdisp = alstock.ui_getStockLotef(codcia, alma, codarti, lote);
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
                    txtDescri.Clear();
                    txtUnidad.Clear();
                    txtFamilia.Clear();
                    txtGrupo.Clear();
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
                    txtFamilia.Text = alarti.ui_getDatos(codcia, codarti, "FAMILIA");
                    txtGrupo.Text = alarti.ui_getDatos(codcia, codarti, "GRUPO");
                    alstock.recalcularStockProducto(codcia, alma, codigo);
                    txtStock.Text = alstock.ui_getStock(codcia, alma, codigo);
                    e.Handled = true;
                    txtCantidad.Focus();
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

        private void ui_almovdps_Load(object sender, EventArgs e)
        {

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

            string query = " select A.lote,B.fecdoc,B.fprod,B.fven,round(A.stock,3)stock ";
            query = query + " from alsklote A  left join vista_lotes B on A.codcia=B.codcia ";
            query = query + " and A.alma=B.alma and A.codarti=B.codarti and A.lote=B.lote ";
            query = query + " where /*A.codcia='" + @codcia + "' and */A.alma='" + @alma + "' and A.codarti='" + @codarti + "' ";
            query = query + " and round(A.stock,3)>0 " + @ordena + " ;";

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
                    dgvData.Columns[1].Width = 200;
                    dgvData.Columns[2].Visible = false;
                    dgvData.Columns[3].Visible = false;
                    dgvData.Columns[4].Width = 100;
                    dgvData.Columns[4].DefaultCellStyle.Format = "##,###.###";
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

        private void txtCodigo_TextChanged(object sender, EventArgs e)
        {

        }
    }
}