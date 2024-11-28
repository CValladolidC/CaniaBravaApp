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
    public partial class ui_proddestajo : Form
    {
        public ui_proddestajo()
        {
            InitializeComponent();
        }

        private void ui_ListaProdDes()
        {

            GlobalVariables variables = new GlobalVariables();
            Funciones funciones = new Funciones();
            string idcia = variables.getValorCia();
            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();
            string query =  " select idproddes,desproddes,unidad,stateproddes,cortoproddes,";
            query = query + " idcia from proddes where idcia='" + @idcia + "' ";
            query = query + " order by idproddes asc;";
            try
            {
                using (SqlDataAdapter myDataAdapter = new SqlDataAdapter(query, conexion))
                {
                    DataSet myDataSet = new DataSet();
                    myDataAdapter.Fill(myDataSet, "tblProdDes");
                    funciones.formatearDataGridView(dgvdetalle);
                    dgvdetalle.DataSource = myDataSet.Tables["tblProdDes"];
                    dgvdetalle.Columns[0].HeaderText = "Código";
                    dgvdetalle.Columns[1].HeaderText = "Nombre Descriptivo";
                    dgvdetalle.Columns[2].HeaderText = "Unidad Referencial";
                    dgvdetalle.Columns[3].HeaderText = "Estado";
                    dgvdetalle.Columns["idcia"].Visible = false;
                    dgvdetalle.Columns["cortoproddes"].Visible = false;
                    dgvdetalle.Columns[0].Width = 50;
                    dgvdetalle.Columns[1].Width = 300;
                    dgvdetalle.Columns[2].Width = 80;
                    dgvdetalle.Columns[3].Width = 50;
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

        private void ui_proddestajo_Load(object sender, EventArgs e)
        {
            ui_ListaProdDes();
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            GlobalVariables globalVariable=new GlobalVariables();
            ui_updproddes ui_detalle = new ui_updproddes();
            ui_detalle._FormPadre = this;
            ui_detalle.ui_setVariableForm(globalVariable.getValorCia());
            ui_detalle.ui_newProdDes();
            ui_detalle.Activate();
            ui_detalle.BringToFront();
            ui_detalle.ShowDialog();
            ui_detalle.Dispose();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            string idcia;
            string idproddes;
            string desproddes;

            Int32 selectedCellCount =
            dgvdetalle.GetCellCount(DataGridViewElementStates.Selected);
            if (selectedCellCount > 0)
            {
                idproddes = dgvdetalle.Rows[dgvdetalle.SelectedCells[1].RowIndex].Cells[0].Value.ToString();
                desproddes = dgvdetalle.Rows[dgvdetalle.SelectedCells[1].RowIndex].Cells[1].Value.ToString();
                idcia = dgvdetalle.Rows[dgvdetalle.SelectedCells[1].RowIndex].Cells[5].Value.ToString();

                DialogResult resultado = MessageBox.Show("¿Desea eliminar la Sección " + desproddes + "?",
                "Consulta Importante",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);
                if (resultado == DialogResult.Yes)
                {
                    ProdDes proddes = new ProdDes();
                    proddes.eliminarProdDes(idproddes, idcia);
                    this.ui_ListaProdDes();
                }
            }
            else
            {
                MessageBox.Show("No ha seleccionado registro a Eliminar", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            string idproddes;
            string desproddes;
            string unidad;
            string cortoproddes;
            string idcia;
            string stateproddes;
            
            Int32 selectedCellCount =
            dgvdetalle.GetCellCount(DataGridViewElementStates.Selected);
            if (selectedCellCount > 0)
            {
                idproddes = dgvdetalle.Rows[dgvdetalle.SelectedCells[1].RowIndex].Cells[0].Value.ToString();
                desproddes = dgvdetalle.Rows[dgvdetalle.SelectedCells[1].RowIndex].Cells[1].Value.ToString();
                unidad = dgvdetalle.Rows[dgvdetalle.SelectedCells[1].RowIndex].Cells[2].Value.ToString();
                stateproddes=dgvdetalle.Rows[dgvdetalle.SelectedCells[1].RowIndex].Cells[3].Value.ToString();
                cortoproddes=dgvdetalle.Rows[dgvdetalle.SelectedCells[1].RowIndex].Cells[4].Value.ToString();
                idcia = dgvdetalle.Rows[dgvdetalle.SelectedCells[1].RowIndex].Cells[5].Value.ToString();

                ui_updproddes ui_detalle = new ui_updproddes();
                ui_detalle._FormPadre = this;
                ui_detalle.ui_setVariableForm(idcia);
                ui_detalle.ui_loadProdDes(idproddes, idcia, desproddes, cortoproddes, unidad, stateproddes);
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
            ui_ListaProdDes();
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            Exporta exporta = new Exporta();
            exporta.Excel_FromDataGridView(dgvdetalle);
        }

        private void btnPDF_Click(object sender, EventArgs e)
        {
            Exporta exporta = new Exporta();
            exporta.Pdf_FromDataGridView(dgvdetalle, 1);
        }

        private void toolstripform_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }
    }
}