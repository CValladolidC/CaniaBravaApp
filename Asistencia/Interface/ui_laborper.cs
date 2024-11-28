using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Configuration;

namespace CaniaBrava
{
    public partial class ui_laborper : Form
    {
        public ui_laborper()
        {
            InitializeComponent();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void ui_laborper_Load(object sender, EventArgs e)
        {
            string squery;
            squery = "SELECT idtipoper as clave,destipoper as descripcion FROM tipoper order by 1 asc;";
            Funciones funciones = new Funciones();
            funciones.listaToolStripComboBox(squery, cmbTipoPer);
            ui_ListaLabPer();

        }

        private void ui_ListaLabPer()
        {

            if (cmbTipoPer.Text == string.Empty)
            {
                MessageBox.Show("No ha definido Tipo de Personal", "Aviso Importante", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }
            else
            {

                Funciones funciones = new Funciones();
                GlobalVariables variables = new GlobalVariables();
                string idcia = variables.getValorCia();
                string idtipoper = cmbTipoPer.Text.Substring(0, 1);

                SqlConnection conexion = new SqlConnection();
                conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
                conexion.Open();
                string query = "select idlabper,deslabper,statelabper,idcia,idtipoper from labper where idcia='" + @idcia + "' and idtipoper='" + @idtipoper + "' order by idlabper asc;";

                try
                {
                    using (SqlDataAdapter myDataAdapter = new SqlDataAdapter(query, conexion))
                    {
                        DataSet myDataSet = new DataSet();
                        myDataAdapter.Fill(myDataSet, "tblLabPer");
                        funciones.formatearDataGridView(dgvdetalle);

                        dgvdetalle.DataSource = myDataSet.Tables["tblLabPer"];
                        dgvdetalle.Columns[0].HeaderText = "Código";
                        dgvdetalle.Columns[1].HeaderText = "Labor";
                        dgvdetalle.Columns[2].HeaderText = "Estado";

                        dgvdetalle.Columns["idtipoper"].Visible = false;
                        dgvdetalle.Columns["idcia"].Visible = false;

                        dgvdetalle.Columns[0].Width = 50;
                        dgvdetalle.Columns[1].Width = 350;
                        dgvdetalle.Columns[2].Width = 80;

                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }

                conexion.Close();
            }
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            string tipoper = cmbTipoPer.Text;
            ui_updlabper ui_detalle = new ui_updlabper();
            ui_detalle._FormPadre = this;
            ui_detalle.newLabPer(tipoper);
            ui_detalle.Activate();
            ui_detalle.BringToFront();
            ui_detalle.ShowDialog();
            ui_detalle.Dispose();

        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            Int32 selectedCellCount =
            dgvdetalle.GetCellCount(DataGridViewElementStates.Selected);
            if (selectedCellCount > 0)
            {
                string idlabper = dgvdetalle.Rows[dgvdetalle.SelectedCells[1].RowIndex].Cells[0].Value.ToString();
                string deslabper = dgvdetalle.Rows[dgvdetalle.SelectedCells[1].RowIndex].Cells[1].Value.ToString();
                string statelabper = dgvdetalle.Rows[dgvdetalle.SelectedCells[1].RowIndex].Cells[2].Value.ToString();
                string tipoper = cmbTipoPer.Text;
                ui_updlabper ui_detalle = new ui_updlabper();
                ui_detalle._FormPadre = this;
                ui_detalle.loadLabPer(idlabper, deslabper, statelabper, tipoper);
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
            ui_ListaLabPer();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {

            Int32 selectedCellCount =
            dgvdetalle.GetCellCount(DataGridViewElementStates.Selected);
            if (selectedCellCount > 0)
            {
                string idlabper = dgvdetalle.Rows[dgvdetalle.SelectedCells[1].RowIndex].Cells[0].Value.ToString();
                string deslabper = dgvdetalle.Rows[dgvdetalle.SelectedCells[1].RowIndex].Cells[1].Value.ToString();
                string idcia = dgvdetalle.Rows[dgvdetalle.SelectedCells[1].RowIndex].Cells[3].Value.ToString();
                string idtipoper = dgvdetalle.Rows[dgvdetalle.SelectedCells[1].RowIndex].Cells[4].Value.ToString();

                DialogResult resultado = MessageBox.Show("¿Desea eliminar la Labor " + deslabper + "?",
                "Consulta Importante",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);
                if (resultado == DialogResult.Yes)
                {
                    LabPer labper = new LabPer();
                    labper.eliminarLabPer(idlabper, idcia, idtipoper);
                    this.ui_ListaLabPer();
                }


            }
            else
            {
                MessageBox.Show("No ha seleccionado registro a Eliminar", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void cmbTipoPer_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.ui_ListaLabPer();
        }

        private void dgvdetalle_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void toolStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void toolstripform_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            Exporta exporta = new Exporta();
            exporta.Excel_FromDataGridView(dgvdetalle);

        }

        private void btnPDF_Click(object sender, EventArgs e)
        {
            Exporta exporta = new Exporta();
            exporta.Pdf_FromDataGridView(dgvdetalle, 2);

        }
    }
}