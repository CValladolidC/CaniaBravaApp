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
    public partial class ui_companias : Form
    {
        Funciones funciones = new Funciones();
        String cadenaConexion;

        public ui_companias()
        {
            InitializeComponent();

            if (funciones.VersionAssembly()) Application.ExitThread();
        }

        private ui_updcompanias form = null;

        private ui_updcompanias FormInstance
        {
            get
            {
                if (form == null)
                {
                    form = new ui_updcompanias();
                    form.Disposed += new EventHandler(form_Disposed);

                }

                return form;
            }
        }

        void form_Disposed(object sender, EventArgs e)
        {
            form = null;
        }

        private void ui_companias_Load(object sender, EventArgs e)
        {
            ListaCompanias();
        }

        private void ListaCompanias()
        {
            SqlConnection conexion = new SqlConnection();
            this.cadenaConexion = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.ConnectionString = cadenaConexion;
            conexion.Open();
            string query = "select A.idcia,A.descia,A.ruccia,CASE A.statecia WHEN 'V' THEN 'VIGENTE' ELSE 'ANULADO' END from ciafile A order by A.idcia asc;";
            try
            {
                using (SqlDataAdapter myDataAdapter = new SqlDataAdapter(query, conexion))
                {
                    DataSet myDataSet = new DataSet();
                    myDataAdapter.Fill(myDataSet, "tblCias");
                    funciones.formatearDataGridView(dgvCompanias);
                    dgvCompanias.DataSource = myDataSet.Tables["tblCias"];
                    dgvCompanias.Columns[0].HeaderText = "Código";
                    dgvCompanias.Columns[1].HeaderText = "Nombre / Razón Social";
                    dgvCompanias.Columns[2].HeaderText = "R.U.C.";
                    dgvCompanias.Columns[3].HeaderText = "Estado";
                    dgvCompanias.Columns[0].Width = 50;
                    dgvCompanias.Columns[1].Width = 350;
                    dgvCompanias.Columns[2].Width = 150;
                    dgvCompanias.Columns[3].Width = 100;
                }

                dgvCompanias.AllowUserToResizeRows = false;
                dgvCompanias.AllowUserToResizeColumns = false;
                foreach (DataGridViewColumn column in dgvCompanias.Columns)
                {
                    column.SortMode = DataGridViewColumnSortMode.NotSortable;
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
            this.Close();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            Int32 selectedCellCount =
            dgvCompanias.GetCellCount(DataGridViewElementStates.Selected);
            if (selectedCellCount > 0)
            {
                string idcia = dgvCompanias.Rows[dgvCompanias.SelectedCells[1].RowIndex].Cells[0].Value.ToString();
                string descia = dgvCompanias.Rows[dgvCompanias.SelectedCells[1].RowIndex].Cells[1].Value.ToString();
                DialogResult resultado = MessageBox.Show("¿Desea eliminar la Compañía " + descia + "?",
                "Consulta Importante",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);
                if (resultado == DialogResult.Yes)
                {
                    CiaFile ciafile = new CiaFile();
                    ciafile.eliminarCiafile(idcia);
                    this.ListaCompanias();
                }

            }
            else
            {
                MessageBox.Show("No ha seleccionado registro a Eliminar", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            ListaCompanias();
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            ui_updcompanias ui_detalle = this.FormInstance;
            ui_detalle._FormPadre = this;
            ui_detalle.Activate();
            ui_detalle.NewCompania();
            ui_detalle.ui_listarComboBox();
            ui_detalle.BringToFront();
            ui_detalle.ShowDialog();
            ui_detalle.Dispose();
        }

        private void toolStripForm_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            Int32 selectedCellCount = dgvCompanias.GetCellCount(DataGridViewElementStates.Selected);
            if (selectedCellCount > 0)
            {
                string idcia = dgvCompanias.Rows[dgvCompanias.SelectedCells[1].RowIndex].Cells[0].Value.ToString();
                string ruccia = dgvCompanias.Rows[dgvCompanias.SelectedCells[1].RowIndex].Cells[2].Value.ToString();
                ui_updcompanias ui_detalle = this.FormInstance;
                ui_detalle._FormPadre = this;
                ui_detalle.ui_listaEstAne(idcia, ruccia);
                ui_detalle.ui_listaEmp(idcia);
                ui_detalle.ui_listaRegLab(idcia);
                ui_detalle.Activate();
                ui_detalle.BringToFront();
                ui_detalle.ui_listarComboBox();
                ui_detalle.LoadCompania(idcia);
                ui_detalle.ShowDialog();
                ui_detalle.Dispose();
            }
            else
            {
                MessageBox.Show("No ha seleccionado registro a Editar", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }
    }
}