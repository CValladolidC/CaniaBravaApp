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
    public partial class ui_movprovee : ui_form
    {
        string _codcia;

        private TextBox TextBoxActivo;

        public TextBox _TextBoxActivo
        {
            get { return TextBoxActivo; }
            set { TextBoxActivo = value; }
        }


        public ui_movprovee()
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

            if (txtProveedor.Text.Trim() == string.Empty)
            {

                MessageBox.Show("No ha especificado Proveedor", "Aviso Importante", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                valorValida = "B";
                txtProveedor.Focus();
            }

            return valorValida;
        }

        private void ui_ListaMovArti()
        {

            Funciones funciones = new Funciones();
            string codcia = this._codcia;
            string alma = cmbAlmacen.Text.Substring(0, 2);
            string codarti = txtCodigo.Text.Trim();
            string codpro = txtProveedor.Text.Trim();

            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();

            string query = " select A.fecdoc,A.td,A.numdoc,A.codmov,A.rftdoc,A.rfndoc,";
            query = query + " D.ruc,A.glosa1,A.codmon,B.preuni,B.cantidad,B.lote ";
            query = query + " from almovc A inner join almovd B ";
            query = query + " on A.codcia=B.codcia and A.alma=B.alma and A.td=B.td and A.numdoc=B.numdoc ";
            query = query + " left join alarti C on ";
            query = query + " B.codcia=C.codcia and B.codarti=C.codarti left join provee D on A.codpro=D.codprovee ";
            query = query + " where A.codcia='"+@codcia+"' and A.alma='" + @alma + "' and B.codarti='" + @codarti + "' and ";
            query = query + " B.cantidad>0 and A.codpro='" + @codpro + "' and A.situa='F' ";
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
                    dgvdetalle.Columns[7].HeaderText = "Glosa";
                    dgvdetalle.Columns[8].HeaderText = "Mon.";
                    dgvdetalle.Columns[9].HeaderText = "P.Unit.";
                    dgvdetalle.Columns[10].HeaderText = "Cantidad";
                    dgvdetalle.Columns[11].HeaderText = "Lote";
                   

                    dgvdetalle.Columns[0].Width = 70;
                    dgvdetalle.Columns[1].Width = 30;
                    dgvdetalle.Columns[2].Width = 70;
                    dgvdetalle.Columns[3].Width = 30;
                    dgvdetalle.Columns[4].Width = 40;
                    dgvdetalle.Columns[5].Width = 90;
                    dgvdetalle.Columns[6].Width = 80;
                    dgvdetalle.Columns[7].Width = 250;
                    dgvdetalle.Columns[8].Width = 30;
                    dgvdetalle.Columns[9].Width = 60;
                    dgvdetalle.Columns[10].Width = 60;
                    dgvdetalle.Columns[11].Width = 120;
                   

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }

            conexion.Close();

        }

        private void txtProveedor_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(Keys.Enter))
            {
                
                string codprovee = txtProveedor.Text.Trim();
                Provee provee = new Provee();
                string nombre = provee.ui_getDatos(codprovee, "NOMBRE");
                if (nombre == string.Empty || codprovee==string.Empty)
                {
                    txtProveedor.Text = "";
                    lblProveedor.Text = "";
                    lblRuc.Text = "";
                    e.Handled = true;
                    txtProveedor.Focus();
                }
                else
                {
                    txtProveedor.Text = provee.ui_getDatos(codprovee, "CODIGO");
                    lblProveedor.Text = provee.ui_getDatos(codprovee, "NOMBRE");
                    lblRuc.Text = provee.ui_getDatos(codprovee, "RUC");
                    e.Handled = true;
                    SendKeys.Send("{TAB}");
                }

            }
        }

        private void txtProveedor_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                this._TextBoxActivo = txtProveedor;
                ui_viewprovee ui_viewprovee = new ui_viewprovee();
                ui_viewprovee._FormPadre = this;
                ui_viewprovee._clasePadre = "ui_movprovee";
                ui_viewprovee._condicionAdicional = string.Empty;
                ui_viewprovee.BringToFront();
                ui_viewprovee.ShowDialog();
                ui_viewprovee.Dispose();
            }
        }

        private void btnConsultar_Click(object sender, EventArgs e)
        {
            string valorValida = ui_valida();
            if (valorValida.Equals("G"))
            {
                ui_ListaMovArti();
            }
        }

        private void ui_limpiar()
        {
            txtCodigo.Clear();
            txtDescripcion.Clear();
            txtUnidad.Clear();
            txtFamilia.Clear();
            txtGrupo.Clear();
            txtProveedor.Clear();
            lblProveedor.Text = "";
            lblRuc.Text = "";

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
                    Alstock alstock = new Alstock();
                    string codigo = alarti.ui_getDatos(codcia,codarti, "CODIGO");
                    txtCodigo.Text = codigo;
                    txtDescripcion.Text = alarti.ui_getDatos(codcia,codarti, "NOMBRE");
                    txtUnidad.Text = alarti.ui_getDatos(codcia,codarti, "UNIDAD");
                    txtFamilia.Text = alarti.ui_getDatos(codcia,codarti, "FAMILIA");
                    txtGrupo.Text = alarti.ui_getDatos(codcia,codarti, "GRUPO");
                    alstock.recalcularStockProducto(codcia,alma, codigo);
                    txtStock.Text = alstock.ui_getStock(codcia,alma, codigo);
                    e.Handled = true;
                    txtProveedor.Focus();
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
                ui_viewarti._clasePadre = "ui_movprovee";
                ui_viewarti._condicionAdicional = string.Empty;
                ui_viewarti.BringToFront();
                ui_viewarti.ShowDialog();
                ui_viewarti.Dispose();
            }
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

        private void btnExcel_Click(object sender, EventArgs e)
        {
            Exporta exporta = new Exporta();
            exporta.Excel_FromDataGridView(dgvdetalle);
        }

       

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ui_movprovee_Load(object sender, EventArgs e)
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
    }
}
