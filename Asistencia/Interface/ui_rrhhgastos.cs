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
    public partial class ui_rrhhgastos : Form
    {
        Funciones funciones = new Funciones();
        public ui_rrhhgastos()
        {
            InitializeComponent();

            if (funciones.VersionAssembly()) Application.ExitThread();
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            ui_rrhhgastosload ui_detalle = new ui_rrhhgastosload();
            ui_detalle.Activate();
            ui_detalle.BringToFront();
            ui_detalle.ShowDialog();
            ui_detalle.Dispose();
            ui_controlling_Load(sender, e);
        }

        private void cmbAño_SelectedIndexChanged(object sender, EventArgs e)
        {
            dgvdetalle.DataSource = null;
            string anio = funciones.getValorComboBox(cmbAño, 4);
            if (anio != string.Empty)
            {
                Load_Datos();
            }
        }

        private void Load_Datos()
        {
            cmbAño.Invoke((MethodInvoker)delegate { dgvdetalle.Controls.Add(loadingNext1); loadingNext1.Visible = true; });
            Application.DoEvents();

            string anio = funciones.getValorComboBox(cmbAño, 4);

            string query = @"SELECT Tipo, anio AS [Año], Fecha, versi AS [Version] FROM gastos_version WHERE anio='" + anio + "' ORDER BY Tipo,versi;";

            LoadQuery(query);

            cmbAño.Invoke((MethodInvoker)delegate { dgvdetalle.Controls.Remove(loadingNext1); loadingNext1.Visible = false; });
            Application.DoEvents();
        }

        private void LoadQuery(string query)
        {
            try
            {
                Funciones funciones = new Funciones();
                SqlConnection conexion = new SqlConnection();
                conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
                conexion.Open();

                using (SqlDataAdapter myDataAdapter = new SqlDataAdapter())
                {
                    DataTable myData = new DataTable();
                    myDataAdapter.SelectCommand = new SqlCommand(query, conexion);
                    myDataAdapter.SelectCommand.CommandTimeout = 360;
                    myDataAdapter.Fill(myData);
                    funciones.formatearDataGridView(dgvdetalle);

                    dgvdetalle.DataSource = myData;

                    dgvdetalle.Columns[0].Width = 150;
                    dgvdetalle.Columns[1].Width = 100;
                    dgvdetalle.Columns[2].Width = 100;

                    dgvdetalle.AllowUserToResizeRows = false;
                    dgvdetalle.AllowUserToResizeColumns = false;
                    foreach (DataGridViewColumn column in dgvdetalle.Columns)
                    {
                        column.SortMode = DataGridViewColumnSortMode.NotSortable;
                    }
                }
                conexion.Close();
            }
            catch (Exception ex)
            {

            }
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {

        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            Load_Datos();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ui_controlling_Load(object sender, EventArgs e)
        {
            string query = "SELECT DISTINCT CAST(anio AS VARCHAR(4)) AS descripcion FROM gastos_version";
            funciones.listaComboBoxUnCampo(query, cmbAño, "B");
        }

        private void btnSap_Click(object sender, EventArgs e)
        {
            ui_PCP0 ui_detalle = new ui_PCP0();
            ui_detalle.Activate();
            ui_detalle.BringToFront();
            ui_detalle.ShowDialog();
            ui_detalle.Dispose();
            ui_controlling_Load(sender, e);
        }
    }
}