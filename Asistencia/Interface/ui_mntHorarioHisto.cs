using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Configuration;
using System.Globalization;

namespace CaniaBrava
{
    public partial class ui_mntHorarioHisto : Form
    {
        private ui_asgHorUpd form = null;
        private ui_asgHorUpd FormInstance
        {
            get
            {
                if (form == null)
                {
                    form = new ui_asgHorUpd();
                    form.Disposed += new EventHandler(form_Disposed);

                }

                return form;
            }
        }

        private Form FormPadre;
        public Form _FormPadre
        {
            get { return FormPadre; }
            set { FormPadre = value; }
        }

        void form_Disposed(object sender, EventArgs e)
        {
            form = null;
        }

        public ui_mntHorarioHisto()
        {
            InitializeComponent();
        }

        public void ui_lista(string codTipHor)
        {
            Funciones funciones = new Funciones();
            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();
            string query = " SELECT idplantiphorario, descripcion, nominas ";
            query += ",evento,CONVERT(VARCHAR(20),fevento,120) as fevento FROM plantiphorario_histo (NOLOCK) ";
            query += "WHERE idplantiphorario = '" + codTipHor + "' order by fevento; ";

            try
            {
                using (SqlDataAdapter myDataAdapter = new SqlDataAdapter(query, conexion))
                {
                    DataSet myDataSet = new DataSet();
                    myDataAdapter.Fill(myDataSet, "tbl_lista");
                    funciones.formatearDataGridView(dgvDetalle);
                    dgvDetalle.DataSource = myDataSet.Tables["tbl_lista"];
                    dgvDetalle.Columns[0].HeaderText = "Código";
                    dgvDetalle.Columns[1].HeaderText = "Tipo de Horario";
                    dgvDetalle.Columns[2].HeaderText = "Nominas";
                    dgvDetalle.Columns[3].HeaderText = "Evento";
                    dgvDetalle.Columns[4].HeaderText = "Fecha Evento";

                    dgvDetalle.Columns[0].Width = 60;
                    dgvDetalle.Columns[1].Width = 230;
                    dgvDetalle.Columns[2].Width = 90;
                    dgvDetalle.Columns[3].Width = 100;
                    dgvDetalle.Columns[4].Width = 120;

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

        private void btnExcel_Click(object sender, EventArgs e)
        {
            if (dgvDetalle.Rows.Count > 0)
            {
                Exporta exporta = new Exporta();
                exporta.Excel_FromDataGridView(dgvDetalle);
            }
            else { MessageBox.Show("Sin registro para exportar.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information); }
        }

        private void btnDetalle_Click(object sender, EventArgs e)
        {
            string codTipHor = string.Empty;
            Int32 selectedCellCount = dgvDetalle.GetCellCount(DataGridViewElementStates.Selected);
            if (selectedCellCount > 0)
            {
                codTipHor = dgvDetalle.Rows[dgvDetalle.SelectedCells[1].RowIndex].Cells[0].Value.ToString();
                string nominas = dgvDetalle.Rows[dgvDetalle.SelectedCells[1].RowIndex].Cells[2].Value.ToString();
                string fecevnto = dgvDetalle.Rows[dgvDetalle.SelectedCells[1].RowIndex].Cells[4].Value.ToString();
                string evnto = dgvDetalle.Rows[dgvDetalle.SelectedCells[1].RowIndex].Cells[3].Value.ToString();
                ui_asgHorUpd ui_detalle = this.FormInstance;
                ui_detalle._FormPadre = this;
                ui_detalle.Activate();
                ui_detalle.BringToFront();
                ui_detalle.asgHorUpdLoad(codTipHor, fecevnto, evnto, true, nominas);
                ui_detalle.ShowDialog();
                ui_detalle.Dispose();
            }
        }
    }
}