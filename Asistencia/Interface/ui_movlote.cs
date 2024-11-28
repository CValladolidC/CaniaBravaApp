using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Configuration;
using System.Data.SqlClient;

namespace CaniaBrava
{
    public partial class ui_movlote : CaniaBrava.ui_form
    {
        string _codcia;

        private TextBox TextBoxActivo;

        public TextBox _TextBoxActivo
        {
            get { return TextBoxActivo; }
            set { TextBoxActivo = value; }
        }

        public ui_movlote()
        {
            InitializeComponent();
        }

        private string ui_valida()
        {
            string valorValida = "G";

            if (cmbAlmacen.Text == string.Empty)
            {
                MessageBox.Show("No ha definido Almacén", "Aviso Importante", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                valorValida = "B";
            }

            if (txtCodigo.Text.Trim() == string.Empty)
            {
                
                MessageBox.Show("No ha especificado artículo", "Aviso Importante", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                valorValida = "B";
                txtCodigo.Focus();

            }

            if (txtLote.Text.Trim() == string.Empty)
            {

                MessageBox.Show("No ha especificado Lote", "Aviso Importante", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                valorValida = "B";
                txtLote.Focus();
            }

            return valorValida;
        }

        private void ui_ListaMovArti()
        {

            Funciones funciones = new Funciones();

            string codcia = this._codcia;
            string alma = cmbAlmacen.Text.Substring(0, 2);
            string codarti = txtCodigo.Text.Trim();
            string lote = txtLote.Text.Trim();

            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();

            string query = " select A.fecdoc,A.td,A.numdoc,A.codmov,A.rftdoc,A.rfndoc,";
            query = query + " D.ruc,D.desprovee,A.glosa1,A.codmon,B.preuni,";
            query = query + " CASE A.td WHEN 'PE' THEN B.cantidad ELSE (B.cantidad*-1) END as cantidad,B.lote ";
            query = query + " from almovc A inner join almovd B ";
            query = query + " on A.codcia=B.codcia and A.alma=B.alma and A.td=B.td and A.numdoc=B.numdoc ";
            query = query + " left join alarti C on ";
            query = query + " B.codcia=C.codcia and B.codarti=C.codarti left join provee D on A.codpro=D.codprovee ";
            query = query + " where A.codcia='" + @codcia + "' and A.alma='" + @alma + "' and B.codarti='" + @codarti + "' and B.cantidad>0 ";
            query = query + " and B.lote='" + @lote + "' and A.situa='F' ";
            query = query + " order by A.fecdoc desc,A.td asc,A.numdoc asc";
            
            try
            {
                using (SqlDataAdapter myDataAdapter = new SqlDataAdapter(query, conexion))
                {
                    DataSet myDataSet = new DataSet();
                    myDataAdapter.Fill(myDataSet, "tblMov");
                    funciones.formatearDataGridView(dgvdetalle);

                    dgvdetalle.DataSource = myDataSet.Tables["tblMov"];
                    dgvdetalle.Columns[0].HeaderText = "Fecha Doc.";
                    dgvdetalle.Columns[1].HeaderText = "Mov";
                    dgvdetalle.Columns[2].HeaderText = "Nro.Mov.";
                    dgvdetalle.Columns[3].HeaderText = "Mov.";
                    dgvdetalle.Columns[4].HeaderText = "T.Doc.";
                    dgvdetalle.Columns[5].HeaderText = "Nro.Doc.";
                    dgvdetalle.Columns[6].HeaderText = "RUC Proveedor";
                    dgvdetalle.Columns[7].HeaderText = "Razón Social Proveedor";
                    dgvdetalle.Columns[8].HeaderText = "Glosa";
                    dgvdetalle.Columns[9].HeaderText = "Mon.";
                    dgvdetalle.Columns[10].HeaderText = "P.Unit.";
                    dgvdetalle.Columns[11].HeaderText = "Cantidad";
                    dgvdetalle.Columns[12].HeaderText = "Lote";

                    dgvdetalle.Columns[0].Width = 70;
                    dgvdetalle.Columns[1].Width = 30;
                    dgvdetalle.Columns[2].Width = 70;
                    dgvdetalle.Columns[3].Width = 30;
                    dgvdetalle.Columns[4].Width = 40;
                    dgvdetalle.Columns[5].Width = 90;
                    dgvdetalle.Columns[6].Width = 80;
                    dgvdetalle.Columns[7].Width = 200;
                    dgvdetalle.Columns[8].Width = 200;
                    dgvdetalle.Columns[9].Width = 30;
                    dgvdetalle.Columns[10].Width = 60;
                    dgvdetalle.Columns[11].Width = 60;
                    dgvdetalle.Columns[12].Width = 120;

                    dgvdetalle.Columns[10].DefaultCellStyle.Format = "#,###,###.##";
                    dgvdetalle.Columns[11].DefaultCellStyle.Format = "#,###,###.##";
                    dgvdetalle.Columns[10].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    dgvdetalle.Columns[11].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

                }

            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }

            conexion.Close();

        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ui_limpiar()
        {
            txtCodigo.Clear();
            txtDescripcion.Clear();
            txtUnidad.Clear();
            txtFamilia.Clear();
            txtGrupo.Clear();
            txtStock.Clear();
            txtStockLote.Clear();

        }
        private void txtCodigo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(Keys.Enter))
            {
                AlArti alarti = new AlArti();
                Funciones funciones = new Funciones();
                string codcia = this._codcia;
                string alma = funciones.getValorComboBox(cmbAlmacen, 2);
                string codarti = txtCodigo.Text.Trim();
                string nombre = alarti.ui_getDatos(codcia,codarti, "NOMBRE");
                if (nombre == string.Empty || codarti == string.Empty)
                {
                    ui_limpiar();
                    e.Handled = true;
                    txtCodigo.Focus();
                }
                else
                {
                    txtCodigo.Text = alarti.ui_getDatos(codcia, codarti, "CODIGO");
                    txtDescripcion.Text = alarti.ui_getDatos(codcia,codarti, "NOMBRE");
                    txtUnidad.Text = alarti.ui_getDatos(codcia,codarti, "UNIDAD");
                    txtFamilia.Text = alarti.ui_getDatos(codcia,codarti, "FAMILIA");
                    txtGrupo.Text = alarti.ui_getDatos(codcia,codarti, "GRUPO");
                    e.Handled = true;
                    txtLote.Focus();
                }

            }
        }

        private void txtCodigo_KeyDown(object sender, KeyEventArgs e)
        {
            
                if (e.KeyCode == Keys.F2)
                {
                    Funciones funciones = new Funciones();
                    string alma = funciones.getValorComboBox(cmbAlmacen, 2);
                    this._TextBoxActivo = txtCodigo;
                    ui_viewarti ui_viewarti = new ui_viewarti();
                    ui_viewarti._FormPadre = this;
                    ui_viewarti._codcia = this._codcia;
                    ui_viewarti._clasePadre = "ui_movlote";
                    ui_viewarti._condicionAdicional = string.Empty;
                    ui_viewarti.BringToFront();
                    ui_viewarti.ShowDialog();
                    ui_viewarti.Dispose();
                }
        }

        private void btnConsultar_Click(object sender, EventArgs e)
        {
            string valorValida = ui_valida();
            if (valorValida.Equals("G"))
            {
                Funciones funciones = new Funciones();
                string codcia = this._codcia;
                string alma = funciones.getValorComboBox(cmbAlmacen, 2);
                string codarti = txtCodigo.Text.Trim();
                string lote = txtLote.Text.Trim();
                Alstock alstock = new Alstock();
                alstock.recalcularStockProducto(codcia, alma, codarti);
                txtStockLote.Text=alstock.ui_getStockLote(codcia, alma, codarti, lote);
                txtStock.Text = alstock.ui_getStock(codcia, alma, codarti);
                ui_ListaMovArti();
            }
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            Exporta exporta = new Exporta();
            exporta.Excel_FromDataGridView(dgvdetalle);
        }

       

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void ui_movlote_Load(object sender, EventArgs e)
        {
            GlobalVariables gv = new GlobalVariables();
            this._codcia = gv.getValorCia();
            string codcia = this._codcia;
            string query = "Select codalma as clave,desalma as descripcion ";
            query = query + "from alalma where codcia='"+@codcia+"' order by 1 asc;";
            Funciones funciones = new Funciones();
            funciones.listaComboBox(query, cmbAlmacen, "");
            ui_limpiar();
            ui_ListaMovArti();
        }

        private void txtLote_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(Keys.Enter))
            {
                    e.Handled = true;
                    btnConsultar.Focus();
              
            }
        }

        private void txtLote_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                Funciones funciones=new Funciones();
                string codarti=txtCodigo.Text.Trim();
                if (codarti != string.Empty)
                {
                    string codcia = this._codcia;
                    string desarti = txtDescripcion.Text.Trim();
                    string alma = funciones.getValorComboBox(cmbAlmacen, 2);
                    string query = "SELECT lote,fecdoc from vista_lotes ";
                    query = query + " where codcia='"+@codcia+"' and codarti='" + @codarti + "' and alma='" + @alma + "' order by fecdoc desc;";
                    this._TextBoxActivo = txtLote;
                    ui_viewlotes ui_viewlotes = new ui_viewlotes();
                    ui_viewlotes.setData(query, "ui_movlote", "Seleccionar Lote del Producto " + desarti);
                    ui_viewlotes._FormPadre = this;
                    ui_viewlotes.BringToFront();
                    ui_viewlotes.ShowDialog();
                    ui_viewlotes.Dispose();
                }
                else
                {
                    MessageBox.Show("Primero debe de registrar código del artículo", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
            }
        }

        private void txtLote_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnVisualizar_Click(object sender, EventArgs e)
        {
            Int32 selectedCellCount = dgvdetalle.GetCellCount(DataGridViewElementStates.Selected);
            if (selectedCellCount > 0)
            {
                Funciones funciones = new Funciones();
                string codcia = this._codcia;
                string desalma = cmbAlmacen.Text;
                string alma = funciones.getValorComboBox(cmbAlmacen, 2);
                string td = dgvdetalle.Rows[dgvdetalle.SelectedCells[1].RowIndex].Cells[1].Value.ToString();
                string numdoc = dgvdetalle.Rows[dgvdetalle.SelectedCells[1].RowIndex].Cells[2].Value.ToString();
                ui_updalmov ui_updalmov = new ui_updalmov();
                ui_updalmov._FormPadre = this;
                ui_updalmov.Activate();
                ui_updalmov.BringToFront();
                ui_updalmov.setData(codcia,alma, desalma);
                ui_updalmov.ui_ActualizaComboBox();
                ui_updalmov.editar(codcia,alma, td, numdoc);
                ui_updalmov.ShowDialog();
                ui_updalmov.Dispose();
            }
            else
            {
                MessageBox.Show("No ha seleccionado registro a Valorizar", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void dgvdetalle_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if ((this.dgvdetalle.Rows[e.RowIndex].Cells["td"].Value).ToString().Equals("PS"))
            {
                foreach (DataGridViewCell celda in

                this.dgvdetalle.Rows[e.RowIndex].Cells)
                {
                    celda.Style.ForeColor = Color.Red;
                }
            }
        }

       
    }
}
