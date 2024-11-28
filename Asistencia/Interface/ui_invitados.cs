using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Windows.Forms;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;

namespace CaniaBrava
{
    public partial class ui_invitados : Form
    {
        //Oliver Cruz Tuanama
        string bd_prov = ConfigurationManager.AppSettings.Get("BD_PROV");
        string query = "";
        GlobalVariables variables = new GlobalVariables();
        Funciones funciones = new Funciones();
        SqlConnection conexion = new SqlConnection();

        private ui_updinvitados form = null;

        private ui_updinvitados FormInstance
        {
            get
            {
                if (form == null)
                {
                    form = new ui_updinvitados();
                    form.Disposed += new EventHandler(form_Disposed);
                }

                return form;
            }
        }

        void form_Disposed(object sender, EventArgs e) { form = null; }

        public ui_invitados()
        {
            InitializeComponent();

            if (funciones.VersionAssembly()) Application.ExitThread();
        }

        private void ui_trabajadores_Load(object sender, EventArgs e)
        {
            query = "SELECT idtipoper as clave,destipoper as descripcion FROM tipoper order by 1 asc;";
            MaesGen maesgen = new MaesGen();
            ui_ListaPerPlan();
        }

        private void ui_ListaPerPlan()
        {
            string idcia = variables.getValorCia();
            string cadenaidtipoper = string.Empty;
            string cadenasituacion = string.Empty;
            string cadenaseccion = string.Empty;

            query = " SELECT dni AS Documento, Nombres, Apellidos, ";
            query += "(CASE area WHEN 'I' THEN 'INVITADO' WHEN 'T' THEN 'TRABAJADOR DE LA EMPRESA' ";
            query += "WHEN 'S' THEN 'TRABAJADOR DE SAMANGA' WHEN 'B' THEN 'TRABAJADOR DE CATTY' WHEN 'E' THEN 'ENFERMERIA' WHEN 'F' THEN 'FUMIGADORES' END) AS Perfil,";
            query += "(CASE estado WHEN 'V' THEN 'VIGENTE' WHEN 'A' THEN 'INACTIVO' END) ";
            query += "AS Estado FROM invitados (NOLOCK) WHERE estado='V' ORDER BY area";

            loadqueryDatos(query);
        }

        private void loadqueryDatos(string query)
        {
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();

            try
            {
                using (SqlDataAdapter myDataAdapter = new SqlDataAdapter(query, conexion))
                {
                    DataSet myDataSet = new DataSet();
                    myDataAdapter.Fill(myDataSet, "tblPerPlan");
                    funciones.formatearDataGridView(dgvdetalle);

                    dgvdetalle.DataSource = myDataSet.Tables["tblPerPlan"];

                    dgvdetalle.Columns[0].Width = 70;
                    dgvdetalle.Columns[1].Width = 160;
                    dgvdetalle.Columns[2].Width = 160;
                    dgvdetalle.Columns[3].Width = 170;
                    dgvdetalle.Columns[4].Width = 75;

                    dgvdetalle.AllowUserToResizeRows = false;
                    dgvdetalle.AllowUserToResizeColumns = false;
                    foreach (DataGridViewColumn column in dgvdetalle.Columns)
                    {
                        column.SortMode = DataGridViewColumnSortMode.NotSortable;
                    }
                }

                ui_calculaNumRegistros();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }

            conexion.Close();
        }

        public void ui_calculaNumRegistros()
        {
            PerPlan perplan = new PerPlan();
            
            string numreg = perplan.getNumeroRegistrosPerPlan(variables.getValorCia());
            string numregencontrados = Convert.ToString(dgvdetalle.RowCount);
        }

        private void btnSalir_Click(object sender, EventArgs e) { Close(); }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            ui_insinvitados ui_detalle = new ui_insinvitados();
            ui_detalle._FormPadre = this;
            ui_detalle.newInvitado();
            ui_detalle.Activate();
            ui_detalle.BringToFront();
            ui_detalle.ShowDialog();
            ui_detalle.Dispose();
        }

        private void btnActualizar_Click(object sender, EventArgs e) { ui_ListaPerPlan(); }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            GlobalVariables variables = new GlobalVariables();
            string idcia = variables.getValorCia();
            Int32 selectedCellCount = dgvdetalle.GetCellCount(DataGridViewElementStates.Selected);
            if (selectedCellCount > 0)
            {
                string dni = dgvdetalle.Rows[dgvdetalle.SelectedCells[1].RowIndex].Cells[0].Value.ToString();
                string nombres = dgvdetalle.Rows[dgvdetalle.SelectedCells[1].RowIndex].Cells[1].Value.ToString();
                string apellidos = dgvdetalle.Rows[dgvdetalle.SelectedCells[1].RowIndex].Cells[2].Value.ToString();

                ui_updinvitados ui_detalle = this.FormInstance;
                ui_detalle._FormPadre = this;
                ui_detalle.Activate();
                ui_detalle.setValores(dni, nombres, apellidos);
                ui_detalle.BringToFront();
                ui_detalle.ShowDialog();
                ui_detalle.Dispose();
            }
            else
            {
                MessageBox.Show("No ha seleccionado registro a Editar", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            Int32 selectedCellCount =
            dgvdetalle.GetCellCount(DataGridViewElementStates.Selected);
            if (selectedCellCount > 0)
            {
                string dni = dgvdetalle.Rows[dgvdetalle.SelectedCells[1].RowIndex].Cells[0].Value.ToString();
                string nombre = dgvdetalle.Rows[dgvdetalle.SelectedCells[1].RowIndex].Cells[1].Value.ToString().Trim();
                string apelli = dgvdetalle.Rows[dgvdetalle.SelectedCells[1].RowIndex].Cells[2].Value.ToString().Trim();
                DialogResult resultado = MessageBox.Show("¿Desea eliminar el Registro " + @nombre + " " + apelli + "?",
                "Consulta Importante",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

                if (resultado == DialogResult.Yes)
                {

                    SqlConnection conexion = new SqlConnection();
                    conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
                    conexion.Open();

                    string squery = "UPDATE invitados SET estado='A' WHERE dni='" + @dni + "';";

                    try
                    {
                        SqlCommand myCommand = new SqlCommand(squery, conexion);
                        myCommand.ExecuteNonQuery();
                        myCommand.Dispose();

                    }
                    catch (SqlException ex)
                    {
                        MessageBox.Show(ex.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    }
                    conexion.Close();

                    this.ui_ListaPerPlan();
                }
            }
            else
            {
                MessageBox.Show("No ha seleccionado registro a Eliminar", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void radioButtonNroDoc_CheckedChanged(object sender, EventArgs e)
        {
            txtBuscar.Text = string.Empty;
            txtBuscar.Focus();
        }

        private void radioButtonNombre_CheckedChanged(object sender, EventArgs e)
        {
            txtBuscar.Text = string.Empty;
            txtBuscar.Focus();
        }

        private void btnTodos_Click(object sender, EventArgs e)
        {
            txtBuscar.Clear();
            ui_ListaPerPlan();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            string buscar = txtBuscar.Text.Trim();

            if (buscar != string.Empty)
            {
                PerPlan perplan = new PerPlan();
                
                string idcia = variables.getValorCia();
                string cadenaBusqueda = string.Empty;

                if (radioButtonNombre.Checked && txtBuscar.Text.Trim() != string.Empty)
                {
                    cadenaBusqueda = " and RTRIM(apellidos)+', '+RTRIM(nombres) LIKE '%" + @buscar + "%' ";
                }
                else
                {
                    cadenaBusqueda = " and dni ='" + @buscar + "' ";
                }

                query = " SELECT dni AS Documento, Nombres, Apellidos, ";
                query += "(CASE area WHEN 'I' THEN 'INVITADO' WHEN 'T' THEN 'TRABAJADOR DE LA EMPRESA' WHEN 'S' THEN 'TRABAJADOR DE SAMANGA' WHEN 'B' THEN 'TRABAJADOR DE CATTY' END) AS Perfil,";
                query += "(CASE estado WHEN 'V' THEN 'VIGENTE' WHEN 'A' THEN 'INACTIVO' END) ";
                query += "AS Estado FROM invitados (NOLOCK) ";
                query += "WHERE 1 = 1 " + @cadenaBusqueda + " ";
                query += "ORDER BY area ";

                loadqueryDatos(query);
            }
        }

        private void btnPdf_Click(object sender, EventArgs e)
        {
            if (dgvdetalle.RowCount > 0)
            {
                Exporta exporta = new Exporta();
                exporta.Pdf_FromDataGridView(dgvdetalle, 1);
            }
            else
            {
                MessageBox.Show("No se puede exportar a PDF", "Aviso Importante", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            Exporta exporta = new Exporta();
            foreach (DataGridViewRow row in dgvdetalle.Rows)
            {
                foreach (DataGridViewCell cell in row.Cells)
                {
                    if (cell.ColumnIndex == 0 || cell.ColumnIndex == 7)
                    {
                        cell.Value = "'" + cell.Value.ToString();
                    }
                }
            }
            exporta.Excel_FromDataGridView(dgvdetalle);
            ui_ListaPerPlan();
        }

        private void btnMasivo_Click(object sender, EventArgs e)
        {
            ui_updinvitadosMasivo ui_detalle = new ui_updinvitadosMasivo();
            ui_detalle._FormPadre = this;
            ui_detalle.Activate();
            ui_detalle.setValores(string.Empty, string.Empty, string.Empty);
            ui_detalle.BringToFront();
            ui_detalle.ShowDialog();
            ui_detalle.Dispose();
        }

        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            string buscar = txtBuscar.Text.Trim();

            if (buscar != string.Empty)
            {
                PerPlan perplan = new PerPlan();

                string idcia = variables.getValorCia();
                string cadenaBusqueda = string.Empty;

                if (radioButtonNombre.Checked && txtBuscar.Text.Trim() != string.Empty)
                {
                    cadenaBusqueda = " and RTRIM(apellidos)+', '+RTRIM(nombres) LIKE '%" + @buscar + "%' ";
                }
                else
                {
                    cadenaBusqueda = " and dni ='" + @buscar + "' ";
                }

                query = " SELECT dni AS Documento, Nombres, Apellidos, ";
                query += "(CASE area WHEN 'I' THEN 'INVITADO' WHEN 'T' THEN 'TRABAJADOR DE LA EMPRESA' WHEN 'S' THEN 'TRABAJADOR DE SAMANGA' WHEN 'B' THEN 'TRABAJADOR DE CATTY' END) AS Perfil,";
                query += "(CASE estado WHEN 'V' THEN 'VIGENTE' WHEN 'A' THEN 'INACTIVO' END) ";
                query += "AS Estado FROM invitados (NOLOCK) ";
                query += "WHERE estado='V' " + @cadenaBusqueda + " ";
                query += "ORDER BY area ";

                loadqueryDatos(query);
            }
        }
    }
}