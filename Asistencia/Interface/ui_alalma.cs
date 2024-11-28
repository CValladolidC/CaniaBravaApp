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
    public partial class ui_alalma : Form
    {
        Funciones funciones = new Funciones();
        string _codcia;

        public ui_alalma()
        {
            InitializeComponent();

            if (funciones.VersionAssembly()) Application.ExitThread();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void ui_alalma_Load(object sender, EventArgs e)
        {
            GlobalVariables gv = new GlobalVariables();
            this._codcia = gv.getValorCia();
            ui_ListaAlalma();
        }
        private void ui_ListaAlalma()
        {
            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();
            string codcia = this._codcia;
            string query = "select codalma,desalma,nrope,nrops,CASE estado WHEN 'V' THEN 'VIGENTE' ELSE 'ANULADO' END from alalma /*where codcia='" + @_codcia + "'*/";
            query = query + "order by codalma asc;";

            try
            {
                using (SqlDataAdapter myDataAdapter = new SqlDataAdapter(query, conexion))
                {
                    DataSet myDataSet = new DataSet();
                    myDataAdapter.Fill(myDataSet, "tblAlalma");
                    funciones.formatearDataGridView(dgvData);
                    dgvData.DataSource = myDataSet.Tables["tblAlalma"];
                    dgvData.Columns[0].HeaderText = "Codigo";
                    dgvData.Columns[1].HeaderText = "Descripcion";
                    dgvData.Columns[2].HeaderText = "Nro.Parte Entrada";
                    dgvData.Columns[3].HeaderText = "Nro.Parte Salida";
                    dgvData.Columns[4].HeaderText = "Estado";
                    dgvData.Columns[0].Width = 70;
                    dgvData.Columns[1].Width = 260;
                    dgvData.Columns[2].Width = 110;
                    dgvData.Columns[3].Width = 110;
                    dgvData.Columns[4].Width = 110;

                    dgvData.AllowUserToResizeRows = false;
                    dgvData.AllowUserToResizeColumns = false;
                    foreach (DataGridViewColumn column in dgvData.Columns)
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

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            ui_ListaAlalma();
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            ui_updalalma ui_detalle = new ui_updalalma();
            ui_detalle._FormPadre = this;
            ui_detalle._codcia = this._codcia;
            ui_detalle.agregar();
            ui_detalle.Activate();
            ui_detalle.BringToFront();
            ui_detalle.ShowDialog();
            ui_detalle.Dispose();
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            Int32 selectedCellCount =
            dgvData.GetCellCount(DataGridViewElementStates.Selected);
            if (selectedCellCount > 0)
            {
                string codalma = dgvData.Rows[dgvData.SelectedCells[1].RowIndex].Cells[0].Value.ToString();
                string desalma = dgvData.Rows[dgvData.SelectedCells[1].RowIndex].Cells[1].Value.ToString();
                string nrope = dgvData.Rows[dgvData.SelectedCells[1].RowIndex].Cells[2].Value.ToString();
                string nrops = dgvData.Rows[dgvData.SelectedCells[1].RowIndex].Cells[3].Value.ToString();
                string estado = dgvData.Rows[dgvData.SelectedCells[1].RowIndex].Cells[4].Value.ToString();
                ui_updalalma ui_detalle = new ui_updalalma();
                ui_detalle._FormPadre = this;
                ui_detalle._codcia = this._codcia;
                ui_detalle.editar(codalma, desalma, nrope, nrops, estado);
                ui_detalle.Activate();
                ui_detalle.BringToFront();
                ui_detalle.ShowDialog();
            }
            else
            {
                MessageBox.Show("No ha seleccionado registro a Editar", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            Int32 selectedCellCount =
            dgvData.GetCellCount(DataGridViewElementStates.Selected);
            if (selectedCellCount > 0)
            {
                string codcia = this._codcia;
                string codalma = dgvData.Rows[dgvData.SelectedCells[1].RowIndex].Cells[0].Value.ToString();
                string desalma = dgvData.Rows[dgvData.SelectedCells[1].RowIndex].Cells[1].Value.ToString();
                DialogResult resultado = MessageBox.Show("¿Desea eliminar el almacén " + desalma + "?",
                "Consulta Importante",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);
                if (resultado == DialogResult.Yes)
                {
                    Alalma alalma = new Alalma();
                    alalma.delAlalma(codcia, codalma);
                    this.ui_ListaAlalma();
                }
            }
            else
            {
                MessageBox.Show("No ha seleccionado registro a Eliminar", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void dgvData_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void toolstripform_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }
    }
}