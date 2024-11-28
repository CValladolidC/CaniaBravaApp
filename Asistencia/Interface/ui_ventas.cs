using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Configuration;
using System.Data.SqlClient;

namespace CaniaBrava
{
    public partial class ui_ventas : Form
    {
        Funciones funciones = new Funciones();
        public ui_ventas()
        {
            InitializeComponent();

            if (funciones.VersionAssembly()) Application.ExitThread();
        }

        private void ui_regvac_Load(object sender, EventArgs e)
        {
            string query = "SELECT DISTINCT CAST(anio AS VARCHAR(4)) AS descripcion FROM ventas_version";
            funciones.listaComboBoxUnCampo(query, cmbAño, "B");
        }

        private void ui_Lista()
        {
            btnBuscar.Invoke((MethodInvoker)delegate { dgvdetalle.Controls.Add(loadingNext1); loadingNext1.Visible = true; });
            Application.DoEvents();

            Funciones funciones = new Funciones();
            string anio = funciones.getValorComboBox(cmbAño, 4);

            string query = @"SELECT CASE T.Mes WHEN 1 THEN 'ENERO'
		WHEN 2 THEN 'FEBRERO'
		WHEN 3 THEN 'MARZO'
		WHEN 4 THEN 'ABRIL'
		WHEN 5 THEN 'MAYO'
		WHEN 6 THEN 'JUNIO'
		WHEN 7 THEN 'JULIO'
		WHEN 8 THEN 'AGOSTO'
		WHEN 9 THEN 'SEPTIEMBRE'
		WHEN 10 THEN 'OCTUBRE'
		WHEN 11 THEN 'NOVIEMBRE'
		WHEN 12 THEN 'DICIEMBRE' END Mes,
CAST(ROUND(T.Proyectado,2) AS NUMERIC(36,2)) AS Proyectado, CAST(ROUND(U.Real,2) AS NUMERIC(36,2)) AS Real,T.Mes AS Id 
FROM (  SELECT MONTH(fecha) AS Mes, SUM(valor) AS Proyectado FROM ventas_final 
WHERE escenario = 'PB" + anio.Substring(2, 2) + @"' AND YEAR(fecha)=" + anio + @" GROUP BY MONTH(fecha) ) T
LEFT JOIN (SELECT MONTH(Fecontab) AS Mes, SUM(Dolares) AS Real FROM [SERVHISTORI].[database_indicadores].[dbo].[Ventas]
WHERE NOT (Orgvt = '1570' AND Cliente IN ('1423345','1131422')) AND NOT (Orgvt = '1530' AND Cliente IN ('1130205','1131422')) AND (YEAR(Fecontab)=" + anio + @") AND (CeBe='USD') GROUP BY MONTH(Fecontab)) U ON U.Mes=T.Mes ";

            LoadQuery(query);

            btnBuscar.Invoke((MethodInvoker)delegate { dgvdetalle.Controls.Remove(loadingNext1); loadingNext1.Visible = false; });
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

                    var data = myData.AsEnumerable().OrderBy(x => x.Field<int>("Id"));
                    dgvdetalle.DataSource = data.CopyToDataTable();

                    dgvdetalle.Columns[0].Width = 150;
                    dgvdetalle.Columns[1].Width = 100;
                    dgvdetalle.Columns[2].Width = 100;
                    dgvdetalle.Columns[3].Visible = false;

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

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            ui_Lista();
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            //Exporta exporta = new Exporta();
            //exporta.Excel_FromDataGridView(dgvdetalle);
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            ui_updventas ui_detalle = new ui_updventas();
            ui_detalle.Activate();
            ui_detalle.BringToFront();
            ui_detalle.ShowDialog();
            ui_detalle.Dispose();
        }

        private void btnBuscar_Click_1(object sender, EventArgs e)
        {
            ui_Lista();
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            Funciones funciones = new Funciones();
            GlobalVariables variables = new GlobalVariables();
            string idcia = variables.getValorCia();

            Int32 selectedCellCount = dgvdetalle.GetCellCount(DataGridViewElementStates.Selected);
            if (selectedCellCount > 0)
            {
                string anio = funciones.getValorComboBox(cmbAño, 4);
                ui_updventas ui_detalle = new ui_updventas();
                ui_detalle.ui_detalle(anio);
                ui_detalle.Activate();
                ui_detalle.BringToFront();
                ui_detalle.ShowDialog();
                ui_detalle.Dispose();
            }
            else
            {
                MessageBox.Show("Debe elegir un Periodo", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void btnCargar_Click(object sender, EventArgs e)
        {
            ui_ventasload ui_detalle = new ui_ventasload();
            ui_detalle.Activate();
            ui_detalle.BringToFront();
            ui_detalle.ShowDialog();
            ui_detalle.Dispose();
            ui_regvac_Load(sender, e);
        }

        private void cmbAño_SelectedIndexChanged(object sender, EventArgs e)
        {
            dgvdetalle.DataSource = null;
            string anio = funciones.getValorComboBox(cmbAño, 4);
            if (anio != string.Empty)
            {
                ui_Lista();
            }
        }
    }
}