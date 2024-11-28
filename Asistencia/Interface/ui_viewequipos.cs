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
    public partial class ui_viewequipos : Form
    {
        GlobalVariables variables = new GlobalVariables();
        Funciones funciones = new Funciones();
        private int Id { get; set; }
        private string Fecha { get; set; }
        private string Usuario { get; set; }
        private string Capataz { get; set; }
        private string Fundo { get; set; }
        private Form FormPadre;
        public Form _FormPadre
        {
            get { return FormPadre; }
            set { FormPadre = value; }
        }

        public ui_viewequipos()
        {
            InitializeComponent();

            if (funciones.VersionAssembly()) Application.ExitThread();
        }

        public void Load_Datos(string usuario, string fecha, string capataz, string fundo, int id)
        {
            this.Id = id;
            this.Fecha = fecha;
            this.Usuario = usuario;
            this.Capataz = capataz;
            this.Fundo = fundo;
            LoadDatos();
        }

        private void LoadDatos()
        {
            string query = string.Empty;

            query = " SELECT parm2maesgen [id] FROM maesgen (NOLOCK) WHERE idmaesgen='163' ";
            if (this.Usuario != string.Empty)
            {
                query += "AND desmaesgen='" + this.Usuario + "' ";
            }
            query += "AND abrevia='" + this.Capataz + "' AND parm1maesgen='" + this.Fundo + "' AND statemaesgen='V' GROUP BY parm2maesgen";

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

                    dgvdetalle.AllowUserToResizeRows = false;
                    dgvdetalle.AllowUserToResizeColumns = false;
                    foreach (DataGridViewColumn column in dgvdetalle.Columns)
                    {
                        column.SortMode = DataGridViewColumnSortMode.NotSortable;
                    }

                    if (this.Usuario != string.Empty)
                    {
                        if (dgvdetalle.Rows.Count == 1)
                        {
                            query = "INSERT INTO progagri_fecafueq VALUES ('" + this.Id + "','" + this.Fecha + "','" +
                                this.Capataz + "','" + this.Fundo + "','" + dgvdetalle.Rows[0].Cells[0].Value + "');";
                            EjecutarQuery(query);
                        }
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

            if (this.Id == 0)
            {
                if (query != string.Empty)
                {
                    ((ui_reprogramacionInicialAgri)FormPadre).txteq.Text = query;
                    this.Close();
                }
                else { MessageBox.Show("No ha seleccionado ningun registro.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop); }
            }
            else { EjecutarQuery(query); }
        }

        private string GetDatosGrid()
        {
            var selectedRows = dgvdetalle.SelectedRows
            .OfType<DataGridViewRow>()
            .Where(row => !row.IsNewRow)
            .ToArray();

            string cadena = string.Empty;
            if (this.Id == 0)
            {
                if (selectedRows.Length == 1)
                {
                    cadena = selectedRows[0].Cells[0].Value.ToString();
                }
            }
            else
            {
                foreach (var row in selectedRows)
                {
                    cadena += "INSERT INTO progagri_fecafueq VALUES ('" + this.Id + "','" + this.Fecha + "','" +
                        this.Capataz + "','" + this.Fundo + "','" + row.Cells[0].Value + "');";
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
                    MessageBox.Show("Ya ingreso Equipo", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
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
