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
    public partial class ui_biosalc_mao : Form
    {
        public ui_biosalc_mao()
        {
            InitializeComponent();
        }

        private void ui_regvac_Load(object sender, EventArgs e)
        {
            Funciones funciones = new Funciones();
            string squery;
            squery = @"SELECT T.descripcion FROM (
 /*SELECT 0 item,'' AS descripcion UNION */
SELECT ROW_NUMBER() OVER(ORDER BY Anho DESC) item, CAST(Anho AS CHAR(4)) AS descripcion FROM PlanMo_New.dbo.biosalc_os_mao_det_manodeobra_redistribuida (NOLOCK) 
WHERE Anho=YEAR(GETDATE()) GROUP BY Anho 
) T 
ORDER BY T.item ";
            funciones.listaComboBoxUnCampo(squery, cmbAño, "B");
        }

        private void ui_Lista()
        {
            btnBuscar.Invoke((MethodInvoker)delegate { dgvdetalle.Controls.Add(loadingNext1); loadingNext1.Visible = true; });
            Application.DoEvents();

            Funciones funciones = new Funciones();
            string anio = funciones.getValorComboBox(cmbAño, 4);
            //string mes = funciones.getValorToolStripComboBox(cmbmes, 2);

            string query = @"SELECT a.desmaesgen AS Mes,CAST(ROUND(T.Proyectado,2) AS NUMERIC(36,2)) AS Proyectado, CAST(ROUND(U.Real,2) AS NUMERIC(36,2)) AS Real,T.Mes AS Id 
FROM (  SELECT Mes,SUM(Total) AS Proyectado FROM PlanMo_New.dbo.temp_proyeccion_dia_modify (NOLOCK) 
WHERE Cod_Escenario LIKE '%MOD4%' AND Tipo_Recurso='MO' AND Anho='" + anio + @"' GROUP BY Mes ) T
LEFT JOIN (SELECT Mes,SUM(Jor_Real) AS Real FROM PlanMo_New.dbo.biosalc_os_mao_det_manodeobra_redistribuida (NOLOCK) 
			WHERE Anho='" + anio + @"' GROUP BY Mes) U ON U.Mes=T.Mes 
LEFT JOIN maesgen a (NOLOCK) ON a.idmaesgen='035' AND a.clavemaesgen=RIGHT('0'+CAST(T.Mes AS VARCHAR(2)),2) 
/*ORDER BY T.Mes*/";

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
            Funciones funciones = new Funciones();
            GlobalVariables variables = new GlobalVariables();
            string idcia = variables.getValorCia();

            Int32 selectedCellCount = dgvdetalle.GetCellCount(DataGridViewElementStates.Selected);
            if (selectedCellCount > 0)
            {
                string anio = funciones.getValorComboBox(cmbAño, 4);
                string mes = dgvdetalle.Rows[dgvdetalle.SelectedCells[1].RowIndex].Cells[3].Value.ToString();
                ui_updbiosalc_mao ui_detalle = new ui_updbiosalc_mao();
                ui_detalle.ui_detalle(anio, mes);
                ui_detalle.Activate();
                ui_detalle.BringToFront();
                ui_detalle.ShowDialog();
                ui_detalle.Dispose();
            }
            else
            {
                MessageBox.Show("Debe elegir un Tipo de Personal", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void btnBuscar_Click_1(object sender, EventArgs e)
        {
            ui_Lista();
        }
    }
}