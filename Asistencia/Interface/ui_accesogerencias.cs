using System;
using System.Data;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Configuration;

namespace CaniaBrava
{
    public partial class ui_accesogerencias : Form
    {
        public string _idusr;
        public string _desusr;
        public string _idcia;
        public string _descia;

        private Form FormPadre;
        public Form _FormPadre
        {
            get { return FormPadre; }
            set { FormPadre = value; }
        }

        public ui_accesogerencias()
        {
            InitializeComponent();
        }

        private void ui_accesocencos_Load(object sender, EventArgs e)
        {
            this.Text = this._desusr + " / " + this._descia;
            string idcia = this._idcia;
            string query = "Select clavemaesgen as clave,desmaesgen as descripcion ";
            query += "from maesgen where idmaesgen='040' and parm1maesgen='" + @idcia + "' and statemaesgen='V' order by 1 asc;";
            Funciones funciones = new Funciones();
            funciones.listaToolStripComboBox(query, cmbAlmacen);
            ui_listaCencosUsr();
        }

        private void ui_listaCencosUsr()
        {
            string idusr = this._idusr;
            string idcia = this._idcia;
            Funciones funciones = new Funciones();
            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();
            string query = " Select distinct A.idgerencia as [Código],B.desmaesgen as [Gerencias] from gerenciasusr A ";
            query = query + ", maesgen B where idmaesgen='040' and A.idgerencia=B.clavemaesgen ";
            query = query + " and A.idusr='" + @idusr + "' and A.idcia='" + @idcia + "'";
            query = query + " order by 1 asc;";
            try
            {
                using (SqlDataAdapter myDataAdapter = new SqlDataAdapter(query, conexion))
                {
                    DataSet myDataSet = new DataSet();
                    myDataAdapter.Fill(myDataSet, "tabla");
                    funciones.formatearDataGridView(dgvDetalle);
                    dgvDetalle.DataSource = myDataSet.Tables["tabla"];
                    dgvDetalle.Columns[0].Width = 100;
                    dgvDetalle.Columns[1].Width = 400;

                    dgvDetalle.AllowUserToResizeRows = false;
                    dgvDetalle.AllowUserToResizeColumns = false;
                    foreach (DataGridViewColumn column in dgvDetalle.Columns)
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

        private void btnSalir_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnGrabar_Click(object sender, EventArgs e)
        {
            if (cmbAlmacen.Text == String.Empty)
            {
                MessageBox.Show("No ha especificado Gerencia", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                cmbAlmacen.Focus();
            }
            else
            {
                Funciones funciones = new Funciones();
                string idcencos = funciones.getValorToolStripComboBox(cmbAlmacen, 8);
                string idusr = this._idusr;
                string idcia = this._idcia;
                UsrFile usrfile = new UsrFile();
                bool existe = usrfile.verificaGerenciasUsr(idcia, idcencos, idusr);
                if (existe) { MessageBox.Show("La Gerencia ya se encuentra asignado..", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information); return; }
                usrfile.actualizarGerenciasUsr(idcia, idcencos, idusr);
                ui_listaCencosUsr();
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            Int32 selectedCellCount =
            dgvDetalle.GetCellCount(DataGridViewElementStates.Selected);
            if (selectedCellCount > 0)
            {
                string idcencos = dgvDetalle.Rows[dgvDetalle.SelectedCells[1].RowIndex].Cells[0].Value.ToString();
                string descencos = dgvDetalle.Rows[dgvDetalle.SelectedCells[1].RowIndex].Cells[1].Value.ToString();
                string idusr = this._idusr;
                string idcia = this._idcia;

                DialogResult resultado = MessageBox.Show("¿Desea eliminar el acceso a " + descencos + "?",
                "Consulta Importante",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);
                if (resultado == DialogResult.Yes)
                {
                    UsrFile usrfile = new UsrFile();
                    usrfile.eliminarGerenciasUsr(idcia, idcencos, idusr);
                    this.ui_listaCencosUsr();
                }
            }
            else
            {
                MessageBox.Show("No ha seleccionado registro a Eliminar", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void btnAreas_Click(object sender, EventArgs e)
        {
            Int32 selectedCellCount = dgvDetalle.GetCellCount(DataGridViewElementStates.Selected);
            if (selectedCellCount > 0)
            {
                string idgeren = dgvDetalle.Rows[dgvDetalle.SelectedCells[1].RowIndex].Cells[0].Value.ToString();
                string descia = dgvDetalle.Rows[dgvDetalle.SelectedCells[1].RowIndex].Cells[1].Value.ToString();
                string desusr = this.Text;

                ui_accesocencos ui_accesocencos = new ui_accesocencos();
                ui_accesocencos._FormPadre = this;
                ui_accesocencos._descia = descia;
                ui_accesocencos._idcia = _idcia;
                ui_accesocencos._idusr = _idusr;
                ui_accesocencos._idger = idgeren;
                ui_accesocencos._desusr = desusr;
                ui_accesocencos.Activate();
                ui_accesocencos.BringToFront();
                ui_accesocencos.ShowDialog();
                ui_accesocencos.Dispose();
            }
            else
            {
                MessageBox.Show("No ha seleccionado una Gerencia para asignar una Área", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }
    }
}