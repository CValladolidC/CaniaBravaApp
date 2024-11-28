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
    public partial class ui_grupoarti : Form
    {
        public ui_grupoarti()
        {
            InitializeComponent();
        }

        private void ui_ListaGrpArti()
        {
            Funciones funciones = new Funciones();
            string famarti = funciones.getValorToolStripComboBox(cmbFamilia, 2);
            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();
            string query = " select famarti,grparti,desgrparti,estado from grparti ";
            query = query + " where famarti='" + @famarti + "' order by grparti asc;";
            try
            {
                using (SqlDataAdapter myDataAdapter = new SqlDataAdapter(query, conexion))
                {
                    DataSet myDataSet = new DataSet();
                    myDataAdapter.Fill(myDataSet, "tblData");
                    funciones.formatearDataGridView(dgvData);
                    dgvData.DataSource = myDataSet.Tables["tblData"];
                    dgvData.Columns[0].HeaderText = "Familia";
                    dgvData.Columns[1].HeaderText = "Grupo";
                    dgvData.Columns[2].HeaderText = "Descripción del Grupo";
                    dgvData.Columns[3].HeaderText = "Estado";
                    dgvData.Columns[0].Width = 50;
                    dgvData.Columns[1].Width = 50;
                    dgvData.Columns[2].Width = 400;
                    dgvData.Columns[3].Width = 80;

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

        private void btnSalir_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            ui_updgrparti ui_detalle = new ui_updgrparti();
            Funciones funciones = new Funciones();
            string famarti = funciones.getValorToolStripComboBox(cmbFamilia, 4);
            ui_detalle._FormPadre = this;
            ui_detalle.setData(famarti);
            ui_detalle.agregar();
            ui_detalle.Activate();
            ui_detalle.BringToFront();
            ui_detalle.ShowDialog();
            ui_detalle.Dispose();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            string famarti;
            string grparti;
            string desgrparti;

            Int32 selectedCellCount =
            dgvData.GetCellCount(DataGridViewElementStates.Selected);
            if (selectedCellCount > 0)
            {
                famarti = dgvData.Rows[dgvData.SelectedCells[1].RowIndex].Cells[0].Value.ToString();
                grparti = dgvData.Rows[dgvData.SelectedCells[1].RowIndex].Cells[1].Value.ToString();
                desgrparti = dgvData.Rows[dgvData.SelectedCells[1].RowIndex].Cells[2].Value.ToString();

                DialogResult resultado = MessageBox.Show("¿Desea eliminar el Grupo de Artículos " + desgrparti + "?",
                "Consulta Importante",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);
                if (resultado == DialogResult.Yes)
                {
                    GrpArti grupoarti = new GrpArti();
                    grupoarti.delGrpArti(famarti, grparti);
                    ui_ListaGrpArti();
                }
            }
            else
            {
                MessageBox.Show("No ha seleccionado registro a Eliminar", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            Int32 selectedCellCount =
            dgvData.GetCellCount(DataGridViewElementStates.Selected);
            if (selectedCellCount > 0)
            {
                string famarti = dgvData.Rows[dgvData.SelectedCells[1].RowIndex].Cells[0].Value.ToString();
                string grparti = dgvData.Rows[dgvData.SelectedCells[1].RowIndex].Cells[1].Value.ToString();
                string desgrparti = dgvData.Rows[dgvData.SelectedCells[1].RowIndex].Cells[2].Value.ToString();
                string estado = dgvData.Rows[dgvData.SelectedCells[1].RowIndex].Cells[3].Value.ToString();

                ui_updgrparti ui_detalle = new ui_updgrparti();
                ui_detalle._FormPadre = this;
                ui_detalle.setData(famarti);
                ui_detalle.editar(grparti, desgrparti, estado);
                ui_detalle.Activate();
                ui_detalle.BringToFront();
                ui_detalle.ShowDialog();
                ui_detalle.Dispose();
            }
            else
            {
                MessageBox.Show("No ha seleccionado registro a Editar", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            ui_ListaGrpArti();
        }

        private void toolstripform_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            Exporta exporta = new Exporta();
            exporta.Excel_FromDataGridView(dgvData);
        }

        private void ui_grupoarti_Load(object sender, EventArgs e)
        {
            MaesGen maesgen = new MaesGen();
            maesgen.listaDetMaesGenToolStrip("110", cmbFamilia, "B");
            ui_ListaGrpArti();
        }

        private void cmbFamilia_SelectedIndexChanged(object sender, EventArgs e)
        {
            ui_ListaGrpArti();
        }

        private void toolStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }
    }
}