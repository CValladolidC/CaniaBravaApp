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
using CaniaBrava.cs;
using CaniaBrava.Interface;

namespace CaniaBrava
{
    public partial class ui_controlling_recursos : Form
    {
        Funciones funciones = new Funciones();
        string cadenaConexion;

        private ui_detmaesgen form = null;

        private ui_detmaesgen FormInstance
        {
            get
            {
                if (form == null)
                {
                    form = new ui_detmaesgen();
                    form.Disposed += new EventHandler(form_Disposed);

                }

                return form;
            }
        }

        void form_Disposed(object sender, EventArgs e)
        {
            form = null;
        }

        public ui_controlling_recursos()
        {
            InitializeComponent();

            if (funciones.VersionAssembly()) Application.ExitThread();
        }

        private void ui_maesgen_Load(object sender, EventArgs e)
        {
            ui_listaMaesGen();
        }

        private void ui_listaMaesGen()
        {
            SqlConnection conexion = new SqlConnection();
            this.cadenaConexion = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.ConnectionString = cadenaConexion;
            conexion.Open();
            string query = @"SELECT id, d.clavemaesgen AS Codigo, d.abrevia AS [Cod.Material], d.desmaesgen AS Descripcion,UM,cuencon AS [Cuenta Contable],b.desmaesgen AS          [Desc. Cta Contable],COD,Rubro,
                        actividad AS [Cod.Actividad],c.desmaesgen AS [Desc.Actividad],Recurso [Cod.Rec],e.desmaesgen AS [Desc.Recurso] FROM maescontrolling a (NOLOCK) 
                        LEFT JOIN maesgen b (NOLOCK) ON b.idmaesgen='103' AND b.clavemaesgen=a.cuencon
                        LEFT JOIN maesgen c (NOLOCK) ON c.idmaesgen='104' AND c.clavemaesgen=a.actividad
                        LEFT JOIN maesgen d (NOLOCK) ON d.idmaesgen='105' AND d.abrevia=a.idmaescon
                        LEFT JOIN maesgen e (NOLOCK) ON e.idmaesgen='106' AND e.clavemaesgen=a.recurso;";
            try
            {
                using (SqlDataAdapter myDataAdapter = new SqlDataAdapter(query, conexion))
                {
                    DataSet myDataSet = new DataSet();
                    myDataAdapter.Fill(myDataSet, "tabla");
                    funciones.formatearDataGridView(dgvMaesGen);

                    dgvMaesGen.DataSource = myDataSet.Tables["tabla"];
                    dgvMaesGen.Columns[0].Visible = false;
                    dgvMaesGen.Columns[1].Visible = false;
                    dgvMaesGen.Columns[2].Width = 100;
                    dgvMaesGen.Columns[3].Width = 250;
                    dgvMaesGen.Columns[4].Width = 70;
                    dgvMaesGen.Columns[6].Width = 170;
                    dgvMaesGen.Columns[8].Width = 70;
                    dgvMaesGen.Columns[9].Visible = false;
                    dgvMaesGen.Columns[10].Width = 170;
                    dgvMaesGen.Columns[11].Visible = false;

                    dgvMaesGen.AllowUserToResizeRows = false;
                    dgvMaesGen.AllowUserToResizeColumns = false;
                    foreach (DataGridViewColumn column in dgvMaesGen.Columns)
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

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            ui_listaMaesGen();
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            Int32 selectedCellCount = dgvMaesGen.GetCellCount(DataGridViewElementStates.Selected);

            if (selectedCellCount > 0)
            {
                string id = dgvMaesGen.Rows[dgvMaesGen.SelectedCells[1].RowIndex].Cells[0].Value.ToString();
                string idmaescon = dgvMaesGen.Rows[dgvMaesGen.SelectedCells[1].RowIndex].Cells[1].Value.ToString();
                string codmat = dgvMaesGen.Rows[dgvMaesGen.SelectedCells[1].RowIndex].Cells[2].Value.ToString();
                string desmaescon = dgvMaesGen.Rows[dgvMaesGen.SelectedCells[1].RowIndex].Cells[3].Value.ToString();
                string um = dgvMaesGen.Rows[dgvMaesGen.SelectedCells[1].RowIndex].Cells[4].Value.ToString();
                string cuencon = dgvMaesGen.Rows[dgvMaesGen.SelectedCells[1].RowIndex].Cells[5].Value.ToString();
                string descuencon = dgvMaesGen.Rows[dgvMaesGen.SelectedCells[1].RowIndex].Cells[6].Value.ToString();
                string cod = dgvMaesGen.Rows[dgvMaesGen.SelectedCells[1].RowIndex].Cells[7].Value.ToString();
                string rubro = dgvMaesGen.Rows[dgvMaesGen.SelectedCells[1].RowIndex].Cells[8].Value.ToString();
                string actividad = dgvMaesGen.Rows[dgvMaesGen.SelectedCells[1].RowIndex].Cells[9].Value.ToString();
                string desactividad = dgvMaesGen.Rows[dgvMaesGen.SelectedCells[1].RowIndex].Cells[10].Value.ToString();
                string recurso = dgvMaesGen.Rows[dgvMaesGen.SelectedCells[1].RowIndex].Cells[11].Value.ToString();
                string desrecurso = dgvMaesGen.Rows[dgvMaesGen.SelectedCells[1].RowIndex].Cells[12].Value.ToString();

                ui_updcontrolling_recursos ui_upddetalle = new ui_updcontrolling_recursos();
                ui_upddetalle.LoadDatos(id, idmaescon, desmaescon, um, cuencon, descuencon, cod, rubro, actividad, recurso);
                ui_upddetalle.Activate();
                ui_upddetalle.BringToFront();
                ui_upddetalle.ShowDialog();
                ui_upddetalle.Dispose();
            }
            else
            {
                MessageBox.Show("No ha seleccionado registro a Editar", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void Importar_Click(object sender, EventArgs e)
        {
            if (this.Controls.Find("ui_controllingactividad", true).Count() == 0)
            {
                ui_controllingactividad ui_controllingactividad = new ui_controllingactividad();
                ui_controllingactividad.Activate();
                ui_controllingactividad.Show();
                ui_controllingactividad.BringToFront();
            }
        }

        private void btnnew_Click(object sender, EventArgs e)
        {
            ui_controllingagregaractiv ui_controllingagregaractiv = new ui_controllingagregaractiv();
            ui_controllingagregaractiv.Activate();
            ui_controllingagregaractiv.BringToFront();
            ui_controllingagregaractiv.ShowDialog();
            ui_controllingagregaractiv.Dispose();
            ui_maesgen_Load(sender, e);
        }

        private void toolStripForm_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            BindingSource bs = new BindingSource();

            bs.DataSource = dgvMaesGen.DataSource;
            bs.Filter = "Descripcion Like '%" + txtBuscar.Text + "%'";
            dgvMaesGen.DataSource = bs;
        }
    }
}