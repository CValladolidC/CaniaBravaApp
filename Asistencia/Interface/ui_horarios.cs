using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

namespace CaniaBrava
{
    public partial class ui_horarios : Form
    {
        Funciones funciones = new Funciones();
        string bd_prov = ConfigurationManager.AppSettings.Get("BD_PROV");
        private ui_updhorarios form = null;

        private ui_updhorarios FormInstance
        {
            get
            {
                if (form == null)
                {
                    form = new ui_updhorarios();
                    form.Disposed += new EventHandler(form_Disposed);

                }

                return form;
            }
        }

        void form_Disposed(object sender, EventArgs e)
        {
            form = null;
        }

        public ui_horarios()
        {
            InitializeComponent();

            if (funciones.VersionAssembly()) Application.ExitThread();
        }

        private void ui_mntHorario_Load(object sender, EventArgs e)
        {
            GlobalVariables variables = new GlobalVariables();
            if (variables.getValorTypeUsr() != "00") { btnEliminar.Visible = false; }
            ui_ListaHorarios();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void dgvdetalle_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void ui_ListaHorarios()
        {
            Funciones funciones = new Funciones();
            HorarioBE BE = null;
            GlobalVariables variables = new GlobalVariables();
            string idcia = variables.getValorCia();

            try
            {
                SqlConnection conexion = new SqlConnection();
                conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
                conexion.Open();
                string query = " SELECT idplantiphorario AS Codigo, descripcion AS [Tipo de Horario] FROM plantiphorario (NOLOCK) ORDER BY 1; ";

                using (SqlDataAdapter myDataAdapter = new SqlDataAdapter(query, conexion))
                {
                    DataSet myDataSet = new DataSet();
                    myDataAdapter.Fill(myDataSet);
                    funciones.formatearDataGridView(dgvdetalle);

                    dgvdetalle.DataSource = myDataSet.Tables[0];
                }
                conexion.Close();

                dgvdetalle.Columns[0].Width = 50;
                dgvdetalle.Columns[1].Width = 270;

                dgvdetalle.AllowUserToResizeRows = false;
                dgvdetalle.AllowUserToResizeColumns = false;
                foreach (DataGridViewColumn column in dgvdetalle.Columns)
                {
                    column.SortMode = DataGridViewColumnSortMode.NotSortable;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            ui_updhorarios ui_detalle = this.FormInstance;
            ui_detalle._FormPadre = this;
            ui_detalle.NuevoTipHor();
            ui_detalle.Activate();
            ui_detalle.BringToFront();
            ui_detalle.ShowDialog();
            ui_detalle.Dispose();
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            string codTipHor = string.Empty;
            Int32 selectedCellCount = dgvdetalle.GetCellCount(DataGridViewElementStates.Selected);
            if (selectedCellCount > 0)
            {
                codTipHor = dgvdetalle.Rows[dgvdetalle.SelectedCells[1].RowIndex].Cells[0].Value.ToString();
                string desc = dgvdetalle.Rows[dgvdetalle.SelectedCells[1].RowIndex].Cells[1].Value.ToString();
                ui_updhorarios ui_detalle = this.FormInstance;
                ui_detalle._FormPadre = this;
                ui_detalle.Activate();
                ui_detalle.BringToFront();
                ui_detalle.asgHorUpdLoad(codTipHor, desc, string.Empty);
                ui_detalle.ShowDialog();
                ui_detalle.Dispose();
            }
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            ui_ListaHorarios();
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            Exporta exporta = new Exporta();
            exporta.Excel_FromDataGridView(dgvdetalle);
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            string codTipHor = string.Empty;
            Int32 selectedCellCount =
            dgvdetalle.GetCellCount(DataGridViewElementStates.Selected);
            string query = string.Empty;
            SqlConnection conexion = new SqlConnection();
            if (selectedCellCount > Convert.ToInt32(0))
            {
                var confirmResult = MessageBox.Show("¿Esta seguro en Eliminar del registro seleccionado?", "Confirmación", MessageBoxButtons.YesNo);
                if (confirmResult == DialogResult.Yes)
                {
                    codTipHor = dgvdetalle.Rows[dgvdetalle.SelectedCells[1].RowIndex].Cells[0].Value.ToString();
                    conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
                    conexion.Open();
                    query = " DELETE FROM plantiphorariodet WHERE idplantiphorario = '" + codTipHor + "';";
                    query += "DELETE FROM plantiphorario    WHERE idplantiphorario = '" + codTipHor + "';";
                    try
                    {
                        using (SqlCommand myCommand = new SqlCommand(query, conexion))
                        {
                            myCommand.ExecuteNonQuery();
                            myCommand.Dispose();
                        }
                    }
                    catch (SqlException ex)
                    {
                        MessageBox.Show(ex.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    }
                    conexion.Close();

                    MessageBox.Show("Tipo de Horario Eliminado..", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ui_ListaHorarios();
                }
            }
            else
            {
                MessageBox.Show("No se puede eliminar el registro. El tipo de Horario seleccionado ya esta asignado a un trabajador", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void btnHistorial_Click(object sender, EventArgs e)
        {
            string codTipHor = string.Empty;
            Int32 selectedCellCount = dgvdetalle.GetCellCount(DataGridViewElementStates.Selected);
            if (selectedCellCount > 0)
            {
                codTipHor = dgvdetalle.Rows[dgvdetalle.SelectedCells[1].RowIndex].Cells[0].Value.ToString();

                ui_mntHorarioHisto ui_detalle = new ui_mntHorarioHisto();
                ui_detalle._FormPadre = this;
                ui_detalle.Activate();
                ui_detalle.BringToFront();
                ui_detalle.ui_lista(codTipHor);
                ui_detalle.ShowDialog();
                ui_detalle.Dispose();
            }
        }
    }
}