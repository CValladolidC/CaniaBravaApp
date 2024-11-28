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
    public partial class ui_updbiosalc_mao : Form
    {
        private string Anio { get; set; }
        private string Mes { get; set; }
        private DataTable DT01;
        private DataTable DT02;
        public ui_updbiosalc_mao()
        {
            InitializeComponent();
            DT01 = new DataTable();
            DT02 = new DataTable();
        }

        public void ui_detalle(string anio, string mes)
        {
            Funciones funciones = new Funciones();
            this.Anio = anio;
            this.Mes = mes;

            funciones.formatearDataGridView(dgvdetallePB);
            funciones.formatearDataGridView(dgvdetalleReal);

            string query = @"SELECT Cod_McFun MF,Cod_Fun FD,Cod_Eq EQ,Descripcion_Act Item,CAST(ROUND(SUM(Total),2) AS NUMERIC(36,2)) Total FROM PlanMo_New.dbo.temp_proyeccion_dia_modify (NOLOCK) WHERE Cod_Escenario LIKE '%MOD4%' AND Tipo_Recurso='MO' AND Anho=" +
            this.Anio + " and Mes=" + this.Mes + " GROUP BY Cod_McFun,Cod_Fun,Cod_Eq,Descripcion_Act ORDER BY Cod_McFun,Cod_Fun,Cod_Eq,Descripcion_Act";

            this.DT01 = LoadQuery(query);


            query = @"SELECT MF,FD,EQ,Actividad Item,CAST(ROUND(SUM(Jor_Real),2) AS NUMERIC(36,2)) Total FROM PlanMo_New.dbo.biosalc_os_mao_det_manodeobra_redistribuida (NOLOCK) WHERE Anho=" +
                this.Anio + " and Mes=" + this.Mes + " GROUP BY MF,FD,EQ,Actividad ORDER BY MF,FD,EQ,Actividad";
            this.DT02 = LoadQuery(query);

            Load_();
        }

        private void Load_()
        {
            DataTable query1 = new DataTable(), query2 = new DataTable();

            if (rd01.Checked)
            {
                query1 = this.DT01.AsEnumerable()
                              .GroupBy(r => r.Field<string>("Item"))
                              .Select(g =>
                              {
                                  var row = this.DT01.NewRow();

                                  row["Item"] = g.Key;
                                  row["Total"] = g.Sum(r => r.Field<decimal>("Total"));

                                  return row;
                              }).CopyToDataTable();

                query1.Columns.Remove("MF");
                query1.Columns.Remove("FD");
                query1.Columns.Remove("EQ");

                query2 = this.DT01.AsEnumerable()
                              .GroupBy(r => r.Field<string>("Item"))
                              .Select(g =>
                              {
                                  var row = this.DT02.NewRow();

                                  row["Item"] = g.Key;
                                  row["Total"] = g.Sum(r => r.Field<decimal>("Total"));

                                  return row;
                              }).CopyToDataTable();

                query2.Columns.Remove("MF");
                query2.Columns.Remove("FD");
                query2.Columns.Remove("EQ");
            }

            if (rd02.Checked)
            {
                query1 = this.DT01.AsEnumerable()
                              .GroupBy(r => new { MF = r.Field<string>("MF"), Item = r.Field<string>("Item") })
                              .Select(g =>
                              {
                                  var row = this.DT01.NewRow();

                                  row["MF"] = g.Key.MF;
                                  row["Item"] = g.Key.Item;
                                  row["Total"] = g.Sum(r => r.Field<decimal>("Total"));

                                  return row;
                              }).CopyToDataTable();

                query1.Columns.Remove("FD");
                query1.Columns.Remove("EQ");

                query2 = this.DT01.AsEnumerable()
                              .GroupBy(r => new { MF = r.Field<string>("MF"), Item = r.Field<string>("Item") })
                              .Select(g =>
                              {
                                  var row = this.DT02.NewRow();

                                  row["MF"] = g.Key.MF;
                                  row["Item"] = g.Key.Item;
                                  row["Total"] = g.Sum(r => r.Field<decimal>("Total"));

                                  return row;
                              }).CopyToDataTable();

                query2.Columns.Remove("FD");
                query2.Columns.Remove("EQ");
            }

            if (rd03.Checked)
            {
                query1 = this.DT01.AsEnumerable()
                              .GroupBy(r => new { MF = r.Field<string>("MF"), FD = r.Field<string>("FD"), Item = r.Field<string>("Item") })
                              .Select(g =>
                              {
                                  var row = this.DT01.NewRow();

                                  row["MF"] = g.Key.MF;
                                  row["FD"] = g.Key.FD;
                                  row["Item"] = g.Key.Item;
                                  row["Total"] = g.Sum(r => r.Field<decimal>("Total"));

                                  return row;
                              }).CopyToDataTable();

                query1.Columns.Remove("EQ");

                query2 = this.DT01.AsEnumerable()
                              .GroupBy(r => new { MF = r.Field<string>("MF"), FD = r.Field<string>("FD"), Item = r.Field<string>("Item") })
                              .Select(g =>
                              {
                                  var row = this.DT02.NewRow();

                                  row["MF"] = g.Key.MF;
                                  row["FD"] = g.Key.FD;
                                  row["Item"] = g.Key.Item;
                                  row["Total"] = g.Sum(r => r.Field<decimal>("Total"));

                                  return row;
                              }).CopyToDataTable();

                query2.Columns.Remove("EQ");
            }

            if (rd04.Checked)
            {
                query1 = this.DT01;
                query2 = this.DT02;
            }

            LoadQuery(query1, dgvdetallePB);
            LoadQuery(query2, dgvdetalleReal);

            lblPB.Text = dgvdetallePB.Rows.Count.ToString() + " registro(s)";
            lblReal.Text = dgvdetalleReal.Rows.Count.ToString() + " registro(s)";
        }

        private DataTable LoadQuery(string query)
        {
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection();
                conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
                conexion.Open();

                using (SqlDataAdapter myDataAdapter = new SqlDataAdapter())
                {
                    DataSet myDataSet = new DataSet();
                    myDataAdapter.SelectCommand = new SqlCommand(query, conexion);
                    myDataAdapter.SelectCommand.CommandTimeout = 360;
                    myDataAdapter.Fill(myDataSet, "tabla");

                    dt = myDataSet.Tables["tabla"];
                }
                conexion.Close();
            }
            catch (Exception ex)
            {

            }

            return dt;
        }

        private void LoadQuery(DataTable data, DataGridView dgv)
        {
            try
            {
                dgv.DataSource = null;
                //Funciones funciones = new Funciones();
                //SqlConnection conexion = new SqlConnection();
                //conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
                //conexion.Open();

                //using (SqlDataAdapter myDataAdapter = new SqlDataAdapter())
                {
                    //DataSet myDataSet = new DataSet();
                    //myDataAdapter.SelectCommand = new SqlCommand(query, conexion);
                    //myDataAdapter.SelectCommand.CommandTimeout = 360;
                    //myDataAdapter.Fill(myDataSet, "tabla");
                    //funciones.formatearDataGridView(dgv);

                    dgv.DataSource = data;

                    if (rd01.Checked)
                    {
                        dgv.Columns[0].Width = 300;
                        dgv.Columns[1].Width = 90;
                    }

                    if (rd02.Checked)
                    {
                        dgv.Columns[0].Width = 60;
                        dgv.Columns[1].Width = 300;
                        dgv.Columns[2].Width = 90;
                    }

                    if (rd03.Checked)
                    {
                        dgv.Columns[0].Width = 60;
                        dgv.Columns[1].Width = 60;
                        dgv.Columns[2].Width = 300;
                        dgv.Columns[3].Width = 90;
                    }

                    if (rd04.Checked)
                    {
                        dgv.Columns[0].Width = 60;
                        dgv.Columns[1].Width = 60;
                        dgv.Columns[2].Width = 60;
                        dgv.Columns[3].Width = 300;
                        dgv.Columns[4].Width = 90;
                    }

                    dgv.AllowUserToResizeRows = false;
                    dgv.AllowUserToResizeColumns = false;
                    foreach (DataGridViewColumn column in dgvdetallePB.Columns)
                    {
                        column.SortMode = DataGridViewColumnSortMode.NotSortable;
                    }
                }
                //conexion.Close();
            }
            catch (Exception ex)
            {

            }
        }

        private void rd01_CheckedChanged(object sender, EventArgs e)
        {
            if (rd01.Checked) { Load_(); }
        }

        private void rd02_CheckedChanged(object sender, EventArgs e)
        {
            if (rd02.Checked) { Load_(); }
        }

        private void rd03_CheckedChanged(object sender, EventArgs e)
        {
            if (rd03.Checked) { Load_(); }
        }

        private void rd04_CheckedChanged(object sender, EventArgs e)
        {
            if (rd04.Checked) { Load_(); }
        }
    }
}