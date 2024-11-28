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
    public partial class ui_viewfundos : Form
    {
        GlobalVariables variables = new GlobalVariables();
        Funciones funciones = new Funciones();
        private int Id { get; set; }
        private string Fecha { get; set; }
        private string Fecha2 { get; set; }
        private string Usuario { get; set; }
        private string Capataz { get; set; }

        public ui_viewfundos()
        {
            InitializeComponent();

            if (funciones.VersionAssembly()) Application.ExitThread();
        }

        public void Load_Datos(string usuario, string fecha1, string fecha2, string capataz, int id)
        {
            this.Id = id;
            this.Fecha = fecha1;
            this.Fecha2 = fecha2;
            this.Usuario = usuario;
            this.Capataz = capataz;
            LoadDatos();
        }

        private void LoadDatos()
        {
            string query = string.Empty;

            query = " SELECT parm1maesgen [id] FROM maesgen (NOLOCK) WHERE idmaesgen='163' and desmaesgen='" + this.Usuario + "' ";
            query += "AND abrevia='" + this.Capataz + "' GROUP BY parm1maesgen ";

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

//                    if (dgvdetalle.Rows.Count == 1)
//                    {
//                        //query += "INSERT INTO progagri_fecafu VALUES ('" + this.Id + "','" + this.Fecha + "','" + this.Capataz + "','" + dgvdetalle.Rows[0].Cells[0].Value + "');";
//                        string fundo = "AND Tipo_Proy = 'Calendario' AND Cod_Fun='" + dgvdetalle.Rows[0].Cells[0].Value + "'";
//                        if (dgvdetalle.Rows[0].Cells[0].Value.ToString() == "INFR")
//                        {
//                            fundo = "AND Cod_McFun='" + dgvdetalle.Rows[0].Cells[0].Value + "'";
//                        }
//                        query += @"
//INSERT INTO progagri_fecafu
//SELECT '" + this.Id + @"',FAplicacion,'" + this.Capataz + @"',Cod_Fun 
//FROM[PlanMo_New].[dbo].[TEMP_PROYECCION_DIA_MODIFY] WHERE Cod_Escenario = '2021-04' AND Anho = 2021
//AND FAplicacion BETWEEN '" + this.Fecha + "' AND '" + this.Fecha2 + "' AND Tipo_Recurso = 'MO' " + fundo + @";
//INSERT INTO progagri_fecafueq
//SELECT '" + this.Id + @"',FAplicacion,'" + this.Capataz + @"',Cod_Fun,Cod_Eq 
//FROM[PlanMo_New].[dbo].[TEMP_PROYECCION_DIA_MODIFY] WHERE Cod_Escenario = '2021-04' AND Anho = 2021
//AND FAplicacion BETWEEN '" + this.Fecha + "' AND '" + this.Fecha2 + "' AND Tipo_Recurso = 'MO' " + fundo + @";
//                        INSERT INTO progagri_fecafueqtu
//                        SELECT '" + this.Id + @"',FAplicacion,'" + this.Capataz + @"',Cod_Fun,Cod_Eq,Cod_Tr 
//FROM[PlanMo_New].[dbo].[TEMP_PROYECCION_DIA_MODIFY] WHERE Cod_Escenario = '2021-04' AND Anho = 2021
//AND FAplicacion BETWEEN '" + this.Fecha + "' AND '" + this.Fecha2 + "' AND Tipo_Recurso = 'MO' " + fundo + @";
//                        INSERT INTO progagri_fecafueqtuac
//                        SELECT '" + this.Id + @"',FAplicacion,'" + this.Capataz + @"',Cod_Fun,Cod_Eq,Cod_Tr,Cod_Act,Total,-1,1,0,'' 
//FROM[PlanMo_New].[dbo].[TEMP_PROYECCION_DIA_MODIFY] WHERE Cod_Escenario = '2021-04' AND Anho = 2021
//AND FAplicacion BETWEEN '" + this.Fecha + "' AND '" + this.Fecha2 + "' AND Tipo_Recurso = 'MO' " + fundo + "; ";
//                        EjecutarQuery(query);
//                    }
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
                //cadena += "INSERT INTO progagri_fecafu VALUES ('" + this.Id + "','" + this.Fecha + "','" + this.Capataz + "','" + row.Cells[0].Value + "');";
                string fundo = "AND Tipo_Proy = 'Calendario' AND Cod_Fun='" + row.Cells[0].Value + "'";
                if (row.Cells[0].Value.ToString() == "INFR")
                {
                    fundo = "AND Cod_McFun='" + row.Cells[0].Value + "'";
                }
                cadena += @"IF (SELECT COUNT(1) FROM [PlanMo_New].[dbo].[TEMP_PROYECCION_DIA_MODIFY] WHERE Cod_Escenario = '2021-04' AND Anho = 2021
AND FAplicacion BETWEEN '" + this.Fecha + "' AND '" + this.Fecha2 + "' AND Tipo_Recurso = 'MO' " + fundo + @")>0 BEGIN
INSERT INTO progagri_feca
SELECT DISTINCT '" + this.Id + @"',FAplicacion,'" + this.Capataz + @"'  
FROM [PlanMo_New].[dbo].[TEMP_PROYECCION_DIA_MODIFY] WHERE Cod_Escenario = '2021-04' AND Anho = 2021
AND FAplicacion BETWEEN '" + this.Fecha + "' AND '" + this.Fecha2 + "' AND Tipo_Recurso = 'MO' " + fundo + @"
AND FAplicacion NOT IN (SELECT fecha FROM progagri_feca WHERE fecha BETWEEN '" + this.Fecha + "' AND '" + this.Fecha2 + @"');
INSERT INTO progagri_fecafu
SELECT DISTINCT '" + this.Id + @"',FAplicacion,'" + this.Capataz + @"',Cod_Fun 
FROM [PlanMo_New].[dbo].[TEMP_PROYECCION_DIA_MODIFY] WHERE Cod_Escenario = '2021-04' AND Anho = 2021
AND FAplicacion BETWEEN '" + this.Fecha + "' AND '" + this.Fecha2 + "' AND Tipo_Recurso = 'MO' " + fundo + @";
INSERT INTO progagri_fecafueq
SELECT DISTINCT '" + this.Id + @"',FAplicacion,'" + this.Capataz + @"',Cod_Fun,Cod_Eq 
FROM[PlanMo_New].[dbo].[TEMP_PROYECCION_DIA_MODIFY] WHERE Cod_Escenario = '2021-04' AND Anho = 2021
AND FAplicacion BETWEEN '" + this.Fecha + "' AND '" + this.Fecha2 + "' AND Tipo_Recurso = 'MO' " + fundo + @";
INSERT INTO progagri_fecafueqtu
SELECT DISTINCT '" + this.Id + @"',FAplicacion,'" + this.Capataz + @"',Cod_Fun,Cod_Eq,Cod_Tr 
FROM [PlanMo_New].[dbo].[TEMP_PROYECCION_DIA_MODIFY] WHERE Cod_Escenario = '2021-04' AND Anho = 2021
AND FAplicacion BETWEEN '" + this.Fecha + "' AND '" + this.Fecha2 + "' AND Tipo_Recurso = 'MO' " + fundo + @";
INSERT INTO progagri_fecafueqtuac
SELECT DISTINCT '" + this.Id + @"',FAplicacion,'" + this.Capataz + @"',Cod_Fun,Cod_Eq,Cod_Tr,Cod_Act,Total,-1,1,0,'' 
FROM [PlanMo_New].[dbo].[TEMP_PROYECCION_DIA_MODIFY] WHERE Cod_Escenario = '2021-04' AND Anho = 2021
AND FAplicacion BETWEEN '" + this.Fecha + "' AND '" + this.Fecha2 + "' AND Tipo_Recurso = 'MO' " + fundo + @" END ELSE BEGIN 
    INSERT INTO progagri_fecafu VALUES ('" + this.Id + "','" + this.Fecha + "','" + this.Capataz + "','" + row.Cells[0].Value + @"');
END; ";
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
            }
            catch (SqlException ex)
            {
                //if (ex.Message.Contains("PRIMARY KEY"))
                //{
                //    MessageBox.Show("Ya ingreso Fundo", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                //}
                //else
                //{
                //    MessageBox.Show(ex.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                //}
            }
            conexion.Close();
            this.Close();
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
