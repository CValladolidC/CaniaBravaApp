using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CaniaBrava
{
    public partial class ui_viewactividades : Form
    {
        GlobalVariables variables = new GlobalVariables();
        Funciones funciones = new Funciones();
        private Form FormPadre;
        public Form _FormPadre
        {
            get { return FormPadre; }
            set { FormPadre = value; }
        }
        public string Capataz { get; set; }
        public string Fundo { get; set; }

        public ui_viewactividades()
        {
            InitializeComponent();

            if (funciones.VersionAssembly()) Application.ExitThread();
        }

        public void Load_Datos(string capataz, string fundo)
        {
            this.Capataz = capataz;
            this.Fundo = fundo;
            LoadDatos();
        }

        private void LoadDatos()
        {
            string query = string.Empty;
            string buscar = txtBuscar.Text.Trim();

            query = " SELECT clavemaesgen [Cod.],desmaesgen [Actividad] FROM maesgen (NOLOCK) WHERE idmaesgen='162' AND clavemaesgen IN (";
            query += "SELECT clavemaesgen FROM maesgen (NOLOCK) WHERE idmaesgen='163' ";
            query += "AND abrevia='" + this.Capataz + "' AND parm1maesgen='" + this.Fundo + "' ";
            query += "AND statemaesgen='V') ";

            if (buscar != String.Empty)
            {
                query += "AND desmaesgen LIKE '%" + buscar + "%' ";
            }
            query += "ORDER BY clavemaesgen";

            loadqueryDatos(query);
        }

        private void loadqueryDatos(string query)
        {
            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();

            try
            {
                using (SqlDataAdapter myDataAdapter = new SqlDataAdapter(query, conexion))
                {
                    UtileriasFechas utilfechas = new UtileriasFechas();
                    DataTable dt = new DataTable();

                    DataSet myDataSet = new DataSet();
                    myDataAdapter.Fill(dt);

                    funciones.formatearDataGridView(dgvdetalle);
                    dgvdetalle.RowHeadersVisible = false;
                    dgvdetalle.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
                    dgvdetalle.AllowUserToAddRows = false;
                    dgvdetalle.MultiSelect = false;

                    dgvdetalle.DataSource = dt;
                    dgvdetalle.Columns[0].Width = 35;
                    dgvdetalle.Columns[1].Width = 280;

                    dgvdetalle.Columns[0].ReadOnly = true;
                    dgvdetalle.Columns[1].ReadOnly = true;

                    dgvdetalle.AllowUserToResizeRows = false;
                    dgvdetalle.AllowUserToResizeColumns = false;
                    foreach (DataGridViewColumn column in dgvdetalle.Columns)
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
            dgvdetalle.Enabled = true;
        }

        #region Eventos Click
        private void btnAdd_Click(object sender, EventArgs e)
        {
            string query = GetDatosGrid();

            if (query != string.Empty)
            {
                string[] arr_ = query.Split('|');
                ((ui_reprogramacionInicialAgri)FormPadre).txtact.Text = arr_[0];
                ((ui_reprogramacionInicialAgri)FormPadre).txtdesact.Text = arr_[1];
                this.Close();
            }
            else { MessageBox.Show("No ha seleccionado ningun registro.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop); }
        }

        private string GetDatosGrid()
        {
            var selectedRows = dgvdetalle.SelectedRows
            .OfType<DataGridViewRow>()
            .Where(row => !row.IsNewRow)
            .ToArray();

            string cadena = string.Empty;
            if (selectedRows.Length == 1)
            {
                cadena = selectedRows[0].Cells[0].Value + "|" + selectedRows[0].Cells[1].Value;
            }

            return cadena;
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            ui_upddetmaesgen ui_updalarti = new ui_upddetmaesgen();
            ui_updalarti._FormPadre = this;
            ui_updalarti._clasePadre = "ui_view";
            ui_updalarti.Activate();
            ui_updalarti.newDetMaesGen("162", string.Empty, this.Capataz, this.Fundo, string.Empty, string.Empty);
            ui_updalarti.BringToFront();
            ui_updalarti.ShowDialog();
            ui_updalarti.Dispose();

            LoadDatos();
        }
        #endregion

        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            LoadDatos();
        }
    }
}
