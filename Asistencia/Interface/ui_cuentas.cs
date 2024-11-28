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
    public partial class ui_cuentas : Form
    {
        private ui_updcuentas form = null;

        private ui_updcuentas FormInstance
        {
            get
            {
                if (form == null)
                {
                    form = new ui_updcuentas();
                    form.Disposed += new EventHandler(form_Disposed);

                }

                return form;
            }
        }

        void form_Disposed(object sender, EventArgs e)
        {
            form = null;
        }

        public ui_cuentas()
        {
            InitializeComponent();
        }

        private void ui_ListaCuentas()
        {

            GlobalVariables variables = new GlobalVariables();
            Funciones funciones = new Funciones();
            string idcia = variables.getValorCia();
            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();
            string query = " select codcuenta,descuenta,detallado,modoane,modoaneref,";
            query = query + " tipane,ane,tipaneref,aneref,idcia ";
            query = query + " from cuencon where idcia='" + @idcia + "' and s_estado = 1 order by codcuenta asc;";
            try
            {
                using (SqlDataAdapter myDataAdapter = new SqlDataAdapter(query, conexion))
                {
                    DataSet myDataSet = new DataSet();
                    myDataAdapter.Fill(myDataSet, "tblCuentas");
                    funciones.formatearDataGridView(dgvdetalle);
                    dgvdetalle.DataSource = myDataSet.Tables["tblCuentas"];
                    dgvdetalle.Columns[0].HeaderText = "Código";
                    dgvdetalle.Columns[1].HeaderText = "Nombre Descriptivo";
                    dgvdetalle.Columns[2].HeaderText = "Detallado?";
                    dgvdetalle.Columns[3].HeaderText = "Modo Anex.";
                    dgvdetalle.Columns[4].HeaderText = "Modo Anex.Ref.";
                    dgvdetalle.Columns[5].HeaderText = "Tipo Anexo";
                    dgvdetalle.Columns[6].HeaderText = "Anexo";
                    dgvdetalle.Columns[7].HeaderText = "Tipo Anexo Ref.";
                    dgvdetalle.Columns[8].HeaderText = "Anexo Ref.";
                    dgvdetalle.Columns["idcia"].Visible = false;
                    dgvdetalle.Columns[0].Width = 50;
                    dgvdetalle.Columns[1].Width = 400;
                    dgvdetalle.Columns[2].Width = 60;
                    dgvdetalle.Columns[3].Width = 60;
                    dgvdetalle.Columns[4].Width = 60;
                    dgvdetalle.Columns[5].Width = 50;
                    dgvdetalle.Columns[6].Width = 80;
                    dgvdetalle.Columns[7].Width = 50;
                    dgvdetalle.Columns[8].Width = 80;
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
            ui_updcuentas ui_detalle = this.FormInstance;
            ui_detalle._FormPadre = this;
            ui_detalle.agregar();
            ui_detalle.Activate();
            ui_detalle.BringToFront();
            ui_detalle.ShowDialog();
            ui_detalle.Dispose();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            Int32 selectedCellCount =
            dgvdetalle.GetCellCount(DataGridViewElementStates.Selected);
            if (selectedCellCount > 0)
            {
                string codcuenta = dgvdetalle.Rows[dgvdetalle.SelectedCells[1].RowIndex].Cells[0].Value.ToString();
                string descuenta = dgvdetalle.Rows[dgvdetalle.SelectedCells[1].RowIndex].Cells[1].Value.ToString();
                string detallado = dgvdetalle.Rows[dgvdetalle.SelectedCells[1].RowIndex].Cells[2].Value.ToString();
                string idcia = dgvdetalle.Rows[dgvdetalle.SelectedCells[1].RowIndex].Cells[9].Value.ToString();

                DialogResult resultado = MessageBox.Show("¿Desea eliminar la Cuenta " + descuenta + "?",
                "Consulta Importante",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);
                if (resultado == DialogResult.Yes)
                {

                    CuenCon cuencon = new CuenCon();
                    cuencon.eliminarCuenCon(codcuenta, idcia);
                    this.ui_ListaCuentas();
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
            dgvdetalle.GetCellCount(DataGridViewElementStates.Selected);
            if (selectedCellCount > 0)
            {
                string codcuenta = dgvdetalle.Rows[dgvdetalle.SelectedCells[1].RowIndex].Cells[0].Value.ToString();
                string descuenta = dgvdetalle.Rows[dgvdetalle.SelectedCells[1].RowIndex].Cells[1].Value.ToString();
                string detallado = dgvdetalle.Rows[dgvdetalle.SelectedCells[1].RowIndex].Cells[2].Value.ToString();
                string modoane = dgvdetalle.Rows[dgvdetalle.SelectedCells[1].RowIndex].Cells[3].Value.ToString();
                string modoaneref = dgvdetalle.Rows[dgvdetalle.SelectedCells[1].RowIndex].Cells[4].Value.ToString();
                string tipane = dgvdetalle.Rows[dgvdetalle.SelectedCells[1].RowIndex].Cells[5].Value.ToString();
                string ane = dgvdetalle.Rows[dgvdetalle.SelectedCells[1].RowIndex].Cells[6].Value.ToString();
                string tipaneref = dgvdetalle.Rows[dgvdetalle.SelectedCells[1].RowIndex].Cells[7].Value.ToString();
                string aneref = dgvdetalle.Rows[dgvdetalle.SelectedCells[1].RowIndex].Cells[8].Value.ToString();
                string idcia = dgvdetalle.Rows[dgvdetalle.SelectedCells[1].RowIndex].Cells[9].Value.ToString();

                ui_updcuentas ui_detalle = this.FormInstance;
                ui_detalle._FormPadre = this;
                ui_detalle.editar(codcuenta, descuenta, detallado, modoane, modoaneref, tipane, ane, tipaneref, aneref);
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
            ui_ListaCuentas();
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

        private void ui_cuentas_Load(object sender, EventArgs e)
        {
            ui_ListaCuentas();
        }

        private void toolstripform_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }
    }
}