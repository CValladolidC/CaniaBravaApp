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
    public partial class ui_asismanual : ui_form
    {
        string _idusr;

        public ui_asismanual()
        {
            InitializeComponent();
        }

        private void ui_asisdia_Load(object sender, EventArgs e)
        {
            Funciones funciones = new Funciones();
            GlobalVariables variables = new GlobalVariables();
            MaesGen maesgen = new MaesGen();
            this._idusr = variables.getValorUsr();
            ui_ListaDataPlan();
        }

        private void ui_ListaDataPlan()
        {
            Funciones funciones = new Funciones();
            CalPlan calplan = new CalPlan();
            UtileriasFechas utilfechas = new UtileriasFechas();
            string idusr = this._idusr;

            Asiste asiste = new Asiste();
            DataTable dt = new DataTable();
            string query = @"  SELECT T.idperplan [Cod.Trabajador],T.fechadiaria [Fecha], T.destrabajador [Trabajador],
            T.destipohorario [T.Horario], T.regEntrada [H.Entrada], T.regSalida [H.Salida],
            CONVERT(VARCHAR(16),T.fechaini,120), CONVERT(VARCHAR(16),T.fechaini,120)
            FROM(
            SELECT a.*, b.regEntrada, b.regSalida FROM progdet a(NOLOCK)
            LEFT JOIN control b(NOLOCK) ON b.idperplan = a.idperplan AND b.fecha = a.fechadiaria
            WHERE a.fechadiaria <= GETDATE() AND a.seccion IN (SELECT idcencos FROM cencosusr (NOLOCK) WHERE idusr = '" + @idusr + @"') 
            ) T WHERE T.regEntrada IS NULL  ";

            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = new SqlCommand(query, conexion);
            da.Fill(dt);
            funciones.formatearDataGridView(dgvdetalle);
            dgvdetalle.DataSource = dt;

            dgvdetalle.Columns[0].Width = 90;
            dgvdetalle.Columns[1].Width = 90;
            dgvdetalle.Columns[2].Width = 230;
            dgvdetalle.Columns[3].Width = 230;
            dgvdetalle.Columns[4].Width = 70;
            dgvdetalle.Columns[5].Width = 70;
            dgvdetalle.Columns[6].Visible = false;
            dgvdetalle.Columns[7].Visible = false;

            dgvdetalle.AllowUserToResizeRows = false;
            dgvdetalle.AllowUserToResizeColumns = false;
            foreach (DataGridViewColumn column in dgvdetalle.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnGrabar_Click(object sender, EventArgs e)
        {
            Funciones funciones = new Funciones();
            CalPlan calplan = new CalPlan();
            Asiste asiste = new Asiste();
            Datasis datasis = new Datasis();

            Int32 selectedCellCount = dgvdetalle.GetCellCount(DataGridViewElementStates.Selected);
            if (selectedCellCount > 0)
            {
                var confirmResult = MessageBox.Show("¿Esta seguro en agregar Registro Manual de Asistencia del registro seleccionado?", "Confirmación", MessageBoxButtons.YesNo);
                if (confirmResult == DialogResult.Yes)
                {
                    string idperplan = dgvdetalle.Rows[dgvdetalle.SelectedCells[1].RowIndex].Cells[0].Value.ToString();
                    string fecha = dgvdetalle.Rows[dgvdetalle.SelectedCells[1].RowIndex].Cells[1].Value.ToString();
                    string entrada = dgvdetalle.Rows[dgvdetalle.SelectedCells[1].RowIndex].Cells[6].Value.ToString();
                    string salida = dgvdetalle.Rows[dgvdetalle.SelectedCells[1].RowIndex].Cells[7].Value.ToString();

                    string query = string.Empty;

                    SqlConnection conexion = new SqlConnection();
                    conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
                    conexion.Open();

                    query = "EXEC [SP_ASISTENCIA] '" + @idperplan + "','" + @entrada + "','SYSTEM','M';";
                    query += " EXEC [SP_ASISTENCIA] '" + @idperplan + "','" + @salida + "','SYSTEM','M';";

                    try
                    {
                        SqlCommand myCommand = new SqlCommand(query, conexion);
                        myCommand.ExecuteNonQuery();
                        myCommand.Dispose();
                        MessageBox.Show("Información guardada exitosamente", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        ui_ListaDataPlan();
                    }
                    catch (SqlException ex)
                    {
                        MessageBox.Show(ex.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    }
                    conexion.Close();

                }
            }
            else
            {
                MessageBox.Show("No ha seleccionado registro", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            string texto = txtBuscar.Text.Trim();
            Busquedas busquedas = new Busquedas();
            busquedas.buscarDataGridView(texto, 1, dgvdetalle);
        }

        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            string texto = txtBuscar.Text.Trim();
            Busquedas busquedas = new Busquedas();
            busquedas.buscarDataGridView(texto, 2, dgvdetalle);
        }

        private void chkCese_CheckedChanged(object sender, EventArgs e)
        {
            ui_ListaDataPlan();
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            Exporta exporta = new Exporta();
            exporta.Excel_FromDataGridView(dgvdetalle);
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            ui_ListaDataPlan();
        }
    }
}