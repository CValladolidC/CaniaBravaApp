using CaniaBrava.Interface;
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
    public partial class ui_controlling : Form
    {
        Funciones funciones = new Funciones();
        public ui_controlling()
        {
            InitializeComponent();

            if (funciones.VersionAssembly()) Application.ExitThread();
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            //ui_controllingload ui_detalle = new ui_controllingload();
            ui_controllingcarga ui_controllingcarga = new ui_controllingcarga();
            ui_controllingcarga.Activate();
            ui_controllingcarga.BringToFront();
            ui_controllingcarga.ShowDialog();
            ui_controllingcarga.Dispose();
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



            string query = @"SELECT a.Tipo, a.anio AS[Año], 
                Month(a.Fecha) AS Mes, a.versi AS[Version], a.escenario, 
                CONVERT(VARCHAR, CONVERT(VARCHAR, CAST(SUM(c.ValMScCO) AS MONEY),1)) MontoDolares, 
                CONVERT(VARCHAR, CONVERT(VARCHAR, CAST(SUM(c.ValMonObj) AS MONEY),1)) MontoSoles,
                CASE WHEN a.indicador = 1 THEN 'Activado' ELSE 'Desactivado' END Estado
                FROM controlling_version AS a
                LEFT JOIN controlling_final AS c ON c.escenario = a.escenario
                WHERE a.anio = '" + anio + "' GROUP BY a.Tipo, a.anio, a.Fecha, a.versi, a.escenario, a.indicador ORDER BY a.indicador desc;";

            //string query = @"SELECT Tipo, anio AS [Año], Fecha, versi AS [Version] FROM controlling_version WHERE anio='" + anio + "' ORDER BY Tipo,versi;";

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

                    dgvdetalle.Columns[0].Width = 50;
                    dgvdetalle.Columns[1].Width = 50;
                    dgvdetalle.Columns[2].Width = 50;
                    dgvdetalle.Columns[3].Width = 50;
                    dgvdetalle.Columns[7].Width = 70;

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
            string query = "SELECT DISTINCT CAST(anio AS VARCHAR(4)) AS descripcion FROM controlling_version";
            funciones.listaComboBoxUnCampo(query, cmbAño, "B");
        }

       
    }
}