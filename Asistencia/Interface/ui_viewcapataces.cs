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
    public partial class ui_viewcapataces : Form
    {
        GlobalVariables variables = new GlobalVariables();
        Funciones funciones = new Funciones();
        private int Id { get; set; }
        private string Fecha { get; set; }
        private string Usuario { get; set; }

        public ui_viewcapataces()
        {
            InitializeComponent();

            if (funciones.VersionAssembly()) Application.ExitThread();
        }

        public void Load_Datos(string usuario, string fecha, int id)
        {
            this.Id = id;
            this.Fecha = fecha;
            this.Usuario = usuario;
            LoadDatos();
        }

        private void LoadDatos()
        {
            string query = string.Empty;
            string buscar = txtBuscar.Text.Trim();

            query = " SELECT idusr,desusr [Capataz] FROM usrfile (NOLOCK) WHERE idusr IN ( ";
            query += "SELECT abrevia FROM maesgen (NOLOCK) WHERE idmaesgen='163' AND desmaesgen='" + this.Usuario + "' GROUP BY abrevia) ";
            query += "AND stateusr = 'V' ";

            if (buscar != String.Empty)
            {
                query += "AND desusr LIKE '%" + buscar + "%'";
            }

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

                    funciones.formatearDataGridViewWhite(dgvdetalle);
                    dgvdetalle.MultiSelect = true;

                    dgvdetalle.DataSource = dt;
                    dgvdetalle.Columns[0].Visible = false;
                    dgvdetalle.Columns[1].Width = 250;

                    dgvdetalle.AllowUserToResizeRows = false;
                    dgvdetalle.AllowUserToResizeColumns = false;
                    foreach (DataGridViewColumn column in dgvdetalle.Columns)
                    {
                        column.SortMode = DataGridViewColumnSortMode.NotSortable;
                    }

                    if (dgvdetalle.Rows.Count == 1)
                    {
                        query += "INSERT INTO progagri_feca VALUES ('" + this.Id + "','" + this.Fecha + "','" + dgvdetalle.Rows[0].Cells[0].Value + "');";
                        EjecutarQuery(query);
                        this.Close();
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

            EjecutarQuery(query);
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
                cadena += "INSERT INTO progagri_feca VALUES ('" + this.Id + "','" + this.Fecha + "','" + row.Cells[0].Value + "');";
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
                    MessageBox.Show("Ya ingreso Tareador", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
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

        }
        #endregion

        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            LoadDatos();
        }
    }
}
