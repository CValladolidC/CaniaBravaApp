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
    public partial class ui_viewactividadesjornales : Form
    {
        GlobalVariables variables = new GlobalVariables();
        Funciones funciones = new Funciones();
        private int Id { get; set; }
        private string Fecha { get; set; }
        public string Usuario { get; set; }
        public string Capataz { get; set; }
        public string Fundo { get; set; }
        public string Equipo { get; set; }
        public string Turno { get; set; }

        public ui_viewactividadesjornales()
        {
            InitializeComponent();

            if (funciones.VersionAssembly()) Application.ExitThread();
        }

        public void Load_Datos(string usuario, string fecha, string capataz, string fundo, string equipo, string turno, int id)
        {
            this.Id = id;
            this.Fecha = fecha;
            this.Usuario = usuario;
            this.Capataz = capataz;
            this.Fundo = fundo;
            this.Equipo = equipo;
            this.Turno = turno;
            LoadDatos();
        }

        private void LoadDatos()
        {
            string query = string.Empty;
            string buscar = txtBuscar.Text.Trim();

            query = " SELECT clavemaesgen [Cod.],desmaesgen [Actividad],'' [# Trab] FROM maesgen (NOLOCK) WHERE idmaesgen='162' AND clavemaesgen IN (";
            query += "SELECT clavemaesgen FROM maesgen (NOLOCK) WHERE idmaesgen='163' ";
            if (this.Usuario != string.Empty)
            {
                query += "and desmaesgen='" + this.Usuario + "' ";
            }
            query += "AND abrevia='" + this.Capataz + "' AND parm1maesgen='" + this.Fundo + "' ";
            if (this.Equipo != string.Empty)
            {
                query += "AND parm2maesgen='" + this.Equipo + "' ";
            }
            if (this.Turno != string.Empty)
            {
                query += "AND parm3maesgen='" + this.Turno + "' ";
            }
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

                    //dgvdetalle.RowHeadersVisible = false;
                    dgvdetalle.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
                    dgvdetalle.AllowUserToAddRows = false;
                    dgvdetalle.MultiSelect = true;

                    dgvdetalle.DataSource = dt;
                    dgvdetalle.Columns[0].Width = 35;
                    dgvdetalle.Columns[1].Width = 280;
                    dgvdetalle.Columns[2].Width = 45;

                    dgvdetalle.Columns[0].ReadOnly = true;
                    dgvdetalle.Columns[1].ReadOnly = true;
                    ((DataGridViewTextBoxColumn)dgvdetalle.Columns[2]).MaxInputLength = 3;

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
                EjecutarQuery(query);
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
            foreach (var row in selectedRows)
            {
                if (row.Cells[2].Value.ToString() != string.Empty)
                {
                    cadena += "INSERT INTO progagri_fecafueqtuac VALUES ('" + this.Id + "','" + this.Fecha + "','" + this.Capataz + "','" +
                        this.Fundo + "','" + this.Equipo + "','" + this.Turno + "','" + row.Cells[0].Value + "','" + row.Cells[2].Value + "',-1,1,0,'');";
                }
            }

            return cadena;
        }

        private void EjecutarQuery(string query)
        {
            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();

            try
            {
                SqlCommand myCommand = new SqlCommand(query, conexion);
                myCommand.ExecuteNonQuery();
                myCommand.Dispose();
                this.Close();
            }
            catch (SqlException ex)
            {
                if (ex.Message.Contains("PRIMARY KEY"))
                {
                    MessageBox.Show("Existe una actividad ya agregada", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
                else
                {
                    MessageBox.Show(ex.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
            }
            conexion.Close();
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            ui_upddetmaesgen ui_updalarti = new ui_upddetmaesgen();
            ui_updalarti._FormPadre = this;
            ui_updalarti._clasePadre = "ui_view163";
            ui_updalarti.Activate();
            ui_updalarti.newDetMaesGen("163", this.Usuario, this.Capataz, this.Fundo, this.Equipo, this.Turno);
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

        private void dgvdetalle_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            if (e.ColumnIndex == 2) // 1 should be your column index
            {
                int i;

                if (e.FormattedValue.ToString() != string.Empty)
                {
                    if (!int.TryParse(Convert.ToString(e.FormattedValue), out i))
                    {
                        e.Cancel = true;
                        MessageBox.Show("Debe ingresar solo numeros enteros");
                    }
                    dgvdetalle.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = i;
                }
            }
        }
    }
}
