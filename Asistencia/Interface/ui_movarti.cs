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
    public partial class ui_movarti : ui_form
    {
        string _codcia;

        private TextBox TextBoxActivo;

        public TextBox _TextBoxActivo
        {
            get { return TextBoxActivo; }
            set { TextBoxActivo = value; }
        }

        public ui_movarti()
        {
            InitializeComponent();
        }

        private void ui_movarti_Load(object sender, EventArgs e)
        {
            GlobalVariables gv = new GlobalVariables();
            this._codcia = gv.getValorCia();
            string codcia = this._codcia;
            string query = "Select codalma as clave,desalma as descripcion ";
            query = query + "from alalma where codcia='"+@codcia+"' order by 1 asc;";
            Funciones funciones = new Funciones();
            funciones.listaComboBox(query, cmbAlmacen, "");
            txtFechaIni.Text = String.Concat("01/01/",DateTime.Now.Year.ToString());
            txtFechaFin.Text = DateTime.Now.ToString("dd/MM/yyyy");
            ui_limpiar();
            ui_ListaMovArti();
        }

        private string ui_valida()
        {
            string valorValida = "G";

            if (cmbAlmacen.Text == string.Empty && valorValida == "G")
            {
                MessageBox.Show("No ha definido Almacén", "Aviso Importante", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                valorValida = "B";
            }

            if (txtCodigo.Text.Trim() == string.Empty && valorValida == "G")
            {

                MessageBox.Show("No ha especificado artículo", "Aviso Importante", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                valorValida = "B";

            }

            if (valorValida == "G")
            {
                if (UtiFechas.IsDate(txtFechaIni.Text))
                {
                    valorValida = "G";
                }
                else
                {
                    MessageBox.Show("Fecha Inicial no válida", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    valorValida = "B";
                    txtFechaIni.Focus();
                }
            }

            if (valorValida == "G")
            {
                if (UtiFechas.IsDate(txtFechaFin.Text))
                {
                    valorValida = "G";
                }
                else
                {
                    MessageBox.Show("Fecha Final no válida", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    valorValida = "B";
                    txtFechaFin.Focus();
                }
            }

            return valorValida;
        }

        private void ui_ListaMovArti()
        {
            
                Funciones funciones = new Funciones();
                string codcia = this._codcia;
                string alma = cmbAlmacen.Text.Substring(0, 2);
                string codarti = txtCodigo.Text.Trim();
                string fechaini = txtFechaIni.Text;
                string fechafin = txtFechaFin.Text;

                SqlConnection conexion = new SqlConnection();
                conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
                conexion.Open();

                string query =  " select A.fecdoc,A.td,A.numdoc,A.codmov,A.rftdoc,A.rfndoc,";
                query = query + " D.ruc,D.desprovee,A.glosa1,A.codmon,B.preuni,";
                query = query + " CASE A.td WHEN 'PE' THEN round(B.cantidad,3) ELSE round((B.cantidad*-1),3) END as cantidad,B.lote ";
                query = query + " from almovc A inner join almovd B ";
                query = query + " on A.codcia=B.codcia and A.alma=B.alma and A.td=B.td and A.numdoc=B.numdoc ";
                query = query + " left join alarti C on ";
                query = query + " B.codcia=C.codcia and B.codarti=C.codarti left join provee D on A.codpro=D.codprovee ";
                query = query + " where A.codcia='" + @codcia + "' and A.alma='" + @alma + "' and ";
                query = query + " B.codarti='" + @codarti + "' and B.cantidad>0 and A.situa='F' ";
                query = query + " and A.fecdoc >= ";
                query = query + " STR_TO_DATE('" + @fechaini + "', '%d/%m/%Y')  and  ";
                query = query + " A.fecdoc <= STR_TO_DATE('" + @fechafin + "', '%d/%m/%Y') ";
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
                        dgvdetalle.Columns[10].DefaultCellStyle.Format = "#,###,###.###";
                        dgvdetalle.Columns[11].DefaultCellStyle.Format = "#,###,###.###";
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
            txtSalAnt.Clear();
            txtEntradas.Clear();
            txtSalidas.Clear();
            txtStockRango.Clear();

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
                    btnConsultar.Focus();
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
                    ui_viewarti._clasePadre = "ui_movarti";
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
                string fechaini = txtFechaIni.Text;
                string fechafin = txtFechaFin.Text;
                Alstock alstock = new Alstock();
                alstock.recalcularStockProducto(codcia, alma, codarti);
                txtSalAnt.Text = alstock.ui_getStockAnterior(codcia, alma, codarti, fechaini);
                txtStockRango.Text = alstock.ui_getStockEnFecha(codcia, alma, codarti, fechafin);
                txtEntradas.Text = alstock.ui_getResMovEnRangoFecha(codcia, alma, "PE", codarti, fechaini, fechafin);
                txtSalidas.Text = alstock.ui_getResMovEnRangoFecha(codcia, alma, "PS", codarti, fechaini, fechafin);
                txtStock.Text = alstock.ui_getStock(codcia, alma, codarti);
                ui_ListaMovArti();
            }
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            Exporta exporta = new Exporta();
            exporta.Excel_FromDataGridView(dgvdetalle);
        }

       

        private void btnVisualiza_Click(object sender, EventArgs e)
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
                MessageBox.Show("No ha seleccionado registro a Visualizar", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void dgvdetalle_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
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

        private void groupBox6_Enter(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void txtStock_TextChanged(object sender, EventArgs e)
        {

        }

        private void label16_Click(object sender, EventArgs e)
        {

        }

        private void txtFechaIni_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                if (UtiFechas.IsDate(txtFechaIni.Text))
                {
                    e.Handled = true;
                    txtFechaFin.Focus();
                }
                else
                {
                    MessageBox.Show("Fecha Inicial no válida", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    e.Handled = true;
                    txtFechaIni.Focus();
                }
            }
        }

        private void txtFechaFin_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                if (UtiFechas.IsDate(txtFechaFin.Text))
                {
                    e.Handled = true;
                    SendKeys.Send("{TAB}");
                }
                else
                {
                    MessageBox.Show("Fecha Final no válida", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    e.Handled = true;
                    txtFechaFin.Focus();
                }
            }
        }

        
    }
}
