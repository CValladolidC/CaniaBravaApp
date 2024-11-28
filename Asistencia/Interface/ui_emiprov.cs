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
    public partial class ui_emiprov : ui_form
    {
        string _codcia, query = "";

        private TextBox TextBoxActivo;

        public TextBox _TextBoxActivo
        {
            get { return TextBoxActivo; }
            set { TextBoxActivo = value; }
        }

        public ui_emiprov()
        {
            InitializeComponent();
        }

        private void ui_emiprov_Load(object sender, EventArgs e)
        {
            GlobalVariables gv = new GlobalVariables();
            this._codcia = gv.getValorCia();
            string codcia = this._codcia;
            query = " Select codalma as clave,desalma as descripcion from alalma where codcia='" + @codcia + "' order by 1 asc;";
            Funciones funciones = new Funciones();
            funciones.listaComboBox(query, cmbAlmacen, "");
            ui_limpiar();
            ui_ListaMov();
        }
        
        private string ui_valida()
        {
            string valorValida = "G";

            if (cmbAlmacen.Text == string.Empty)
            {
                MessageBox.Show("No ha definido Almacén", "Aviso Importante", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                valorValida = "B";
            }

            if (txtProveedor.Text.Trim() == string.Empty)
            {
                MessageBox.Show("No ha especificado Proveedor", "Aviso Importante", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                valorValida = "B";
                txtProveedor.Focus();
            }

            return valorValida;
        }

        private void ui_ListaMov()
        {
            Funciones funciones = new Funciones();
            string codcia = this._codcia;
            string alma = cmbAlmacen.Text.Substring(0, 2);
            string codpro = txtProveedor.Text.Trim();

            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();

            query = "  select A.fecdoc,A.td,A.numdoc,A.codmov,A.rftdoc,A.rfndoc,";
            query += " D.ruc,A.glosa1,A.codmon,SUM(B.total) as total ";
            query += " from almovc A inner join almovd B ";
            query += " on A.codcia=B.codcia and A.alma=B.alma and A.td=B.td and A.numdoc=B.numdoc ";
            query += " left join provee D on A.codpro=D.codprovee ";
            query += " where A.codcia='" + @codcia + "' and A.alma='" + @alma + "' and A.codpro<>'' and ";
            query += " B.cantidad>0 and A.codpro='" + @codpro + "' and A.situa='F' ";
            query += " group by A.fecdoc,A.td,A.numdoc,A.codmov,A.rftdoc,A.rfndoc,D.ruc,A.glosa1,A.codmon ";
            query += " order by A.fecdoc desc,A.td asc,A.numdoc asc";

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
                    dgvdetalle.Columns[9].HeaderText = "Importe Total";

                    dgvdetalle.Columns[0].Width = 70;
                    dgvdetalle.Columns[1].Width = 30;
                    dgvdetalle.Columns[2].Width = 70;
                    dgvdetalle.Columns[3].Width = 30;
                    dgvdetalle.Columns[4].Width = 40;
                    dgvdetalle.Columns[5].Width = 90;
                    dgvdetalle.Columns[6].Width = 80;
                    dgvdetalle.Columns[7].Width = 250;
                    dgvdetalle.Columns[8].Width = 30;
                    dgvdetalle.Columns[9].Width = 75;

                    dgvdetalle.Columns[9].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    dgvdetalle.Columns[9].DefaultCellStyle.Format = "###,###.##";
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
                Provee provee = new Provee();
                string codprovee = txtProveedor.Text.Trim();
                string nombre = provee.ui_getDatos(codprovee, "NOMBRE");
                if (nombre == string.Empty)
                {
                    txtProveedor.Text = "";
                    txtDscProvee.Text = "";
                    txtRuc.Text = "";
                    e.Handled = true;
                    txtProveedor.Focus();
                }
                else
                {
                    txtProveedor.Text = provee.ui_getDatos(codprovee, "CODIGO");
                    txtDscProvee.Text = provee.ui_getDatos(codprovee, "NOMBRE");
                    txtRuc.Text = provee.ui_getDatos(codprovee, "RUC");
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
                ui_viewprovee._clasePadre = "ui_emiprov";
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
                ui_ListaMov();
            }
        }

        private void ui_limpiar()
        {
            txtProveedor.Clear();
            txtDscProvee.Clear();
            txtRuc.Clear();
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
    }
}