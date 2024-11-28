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
    public partial class ui_updventas : Form
    {
        DataTable dt_PB;
        DataTable dt_Real;
        public ui_updventas()
        {
            InitializeComponent();
        }

        private void txtanio_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                e.Handled = true;
                txtdescripcion.Text = "PB-" + txtanio.Text + "-VENTAS";
                btnGrabar.Focus();
            }
        }

        private void btnGrabar_Click(object sender, EventArgs e)
        {

        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            txtdetalle.Enabled = true;
            txtdetalle.Focus();
            btnUpdate.Enabled = false;
            btnDelete.Enabled = false;
            btnSave.Enabled = true;
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            Funciones funciones = new Funciones();
            GlobalVariables variables = new GlobalVariables();
            string idcia = variables.getValorCia();

            Int32 selectedCellCount = dgvPB.GetCellCount(DataGridViewElementStates.Selected);
            if (selectedCellCount > 0)
            {
                string id = dgvPB.Rows[dgvPB.SelectedCells[0].RowIndex].Cells[0].Value.ToString();
                string detall = dgvPB.Rows[dgvPB.SelectedCells[0].RowIndex].Cells[1].Value.ToString();

                txtcodigo.Text = id;
                txtdetalle.Text = detall;
            }
            else
            {
                MessageBox.Show("Debe elegir un registro", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string anio = txtanio.Text;
            string descripcion = txtdescripcion.Text;
            string detalle = txtdetalle.Text;
            string query = string.Empty, mes = string.Empty;

            if (detalle != string.Empty)
            {
                for (int i = 1; i < 13; i++)
                {
                    mes = ("0" + i);
                    query += @"INSERT INTO [SERVHISTORI].[database_indicadores].[dbo].[Presupuesto] VALUES ((SELECT MAX(id)+1 FROM [SERVHISTORI].[database_indicadores].[dbo].[Presupuesto]),'" + descripcion + @"',
(SELECT MAX(tipo)" + (i == 1 ? "+1" : "") + " FROM [SERVHISTORI].[database_indicadores].[dbo].[Presupuesto] WHERE escenario LIKE '%PB%VENTAS'),'" + detalle + "','" + anio + "-" + mes.Substring(mes.Length - 2) + "-01',0);";
                }

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
                    MessageBox.Show(ex.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
                conexion.Close();

                Load_PB(txtanio.Text);
                Load_Real(txtanio.Text);
                Load_RealPB();
            }
            else { MessageBox.Show("Debe ingresar una descripcion", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop); txtdetalle.Focus(); }

            txtcodigo.Clear();
            txtdetalle.Clear();
            txtdetalle.Enabled = false;
            btnUpdate.Enabled = true;
            btnDelete.Enabled = true;
            btnSave.Enabled = false;
        }

        public void ui_detalle(string anio)
        {
            txtanio.Text = anio;
            txtdescripcion.Text = "PB-" + txtanio.Text + "-VENTAS";
            txtanio.ReadOnly = true;
            btnGrabar.Enabled = false;
            btnNew.Enabled = true;
            btnUpdate.Enabled = true;
            btnDelete.Enabled = true;
            btnSave.Enabled = false;
            Load_PB(anio);
            Load_Real(anio);
            Load_RealPB();
        }

        private void Load_PB(string anio)
        {
            string query = @"SELECT MONTH(fecha) AS Mes,tipo,dtipo,SUM(valor) AS total FROM ventas_final 
WHERE escenario = 'PB" + anio.Substring(2, 2) + @"' AND YEAR(fecha)=" + anio + " GROUP BY MONTH(fecha),tipo,dtipo ORDER BY tipo";

            try
            {
                Funciones funciones = new Funciones();
                SqlConnection conexion = new SqlConnection();
                conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
                conexion.Open();

                using (SqlDataAdapter myDataAdapter = new SqlDataAdapter())
                {
                    DataTable dt = new DataTable();
                    dt_PB = new DataTable();
                    myDataAdapter.SelectCommand = new SqlCommand(query, conexion);
                    myDataAdapter.SelectCommand.CommandTimeout = 360;
                    myDataAdapter.Fill(dt_PB);
                    //funciones.formatearDataGridViewWhite2(dgvPB);

                    string nameMes = string.Empty;
                    for (int i = 0; i < 14; i++)
                    {
                        switch (i)
                        {
                            case 0: nameMes = "Tipo"; break;
                            case 1: nameMes = "Descripcion"; break;
                            case 2: nameMes = "Enero"; break;
                            case 3: nameMes = "Febrero"; break;
                            case 4: nameMes = "Marzo"; break;
                            case 5: nameMes = "Abril"; break;
                            case 6: nameMes = "Mayo"; break;
                            case 7: nameMes = "Junio"; break;
                            case 8: nameMes = "Julio"; break;
                            case 9: nameMes = "Agosto"; break;
                            case 10: nameMes = "Septiembre"; break;
                            case 11: nameMes = "Octubre"; break;
                            case 12: nameMes = "Noviembre"; break;
                            case 13: nameMes = "Diciembre"; break;
                        }

                        dt.Columns.Add(nameMes, typeof(string));
                    }

                    if (dt_PB.Rows.Count > 0)
                    {
                        var gTipo = dt_PB.AsEnumerable().GroupBy(x => new { tipo = x.Field<int>("tipo"), dtipo = x.Field<string>("dtipo") }).ToList();
                        foreach (var g in gTipo)
                        {
                            var deta = dt_PB.AsEnumerable().Where(x => x.Field<int>("tipo") == g.Key.tipo && x.Field<string>("dtipo") == g.Key.dtipo).ToList();

                            DataRow dr;
                            dr = dt.NewRow();
                            dr[0] = (deta.FirstOrDefault().Field<int>("tipo") == 1 ? "Azucar" : "Alcohol");
                            dr[1] = deta.FirstOrDefault().Field<string>("dtipo");
                            for (int i = 2; i < 14; i++)
                            {
                                dr[i] = deta.Where(x => x.Field<int>("Mes") == (i - 1)).FirstOrDefault().Field<decimal>("total").ToString();
                            }
                            //dr[13] = deta.FirstOrDefault().Field<int>("tipo");
                            dt.Rows.Add(dr);
                        }

                        txttotPB.Text = dt_PB.AsEnumerable().Sum(x => x.Field<decimal>("total")).ToString();
                    }

                    dgvPB.DataSource = dt;

                    dgvPB.Columns[0].Width = 65;
                    dgvPB.Columns[1].Width = 190;
                    dgvPB.Columns[2].Width = 65;
                    dgvPB.Columns[3].Width = 65;
                    dgvPB.Columns[4].Width = 65;
                    dgvPB.Columns[5].Width = 65;
                    dgvPB.Columns[6].Width = 65;
                    dgvPB.Columns[7].Width = 65;
                    dgvPB.Columns[8].Width = 65;
                    dgvPB.Columns[9].Width = 65;
                    dgvPB.Columns[10].Width = 65;
                    dgvPB.Columns[11].Width = 65;
                    dgvPB.Columns[12].Width = 65;
                    dgvPB.Columns[13].Width = 65;

                    dgvPB.RowHeadersVisible = false;
                    dgvPB.AllowUserToAddRows = false;
                    dgvPB.Columns[0].ReadOnly = true;
                    dgvPB.Columns[0].Frozen = true;
                    dgvPB.AllowUserToResizeRows = false;
                    dgvPB.AllowUserToResizeColumns = false;
                    foreach (DataGridViewColumn column in dgvPB.Columns)
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

        private void Load_Real(string anio)
        {
            string query = @"SELECT Periodo AS Mes, Material, Descripcion, CAST(ROUND(SUM(Dolares), 2) AS NUMERIC(36, 2)) AS total
                            FROM[SERVHISTORI].[database_indicadores].[dbo].[Ventas]
                            WHERE NOT(Orgvt = '1570' AND Cliente IN ('1423345','1131422')) 
                            AND NOT(Orgvt = '1530' AND Cliente IN ('1130205','1131422')) 
                            AND (Anio = " + anio + @") AND (CeBe = 'USD')
                            GROUP BY Periodo, Anio, Material, Descripcion";
            try
            {
                Funciones funciones = new Funciones();
                SqlConnection conexion = new SqlConnection();
                conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
                conexion.Open();

                using (SqlDataAdapter myDataAdapter = new SqlDataAdapter())
                {
                    DataTable dt = new DataTable();
                    dt_Real = new DataTable();
                    myDataAdapter.SelectCommand = new SqlCommand(query, conexion);
                    myDataAdapter.SelectCommand.CommandTimeout = 360;
                    myDataAdapter.Fill(dt_Real);
                    funciones.formatearDataGridViewWhite(dgvReal);

                    string nameMes = string.Empty;
                    for (int i = 0; i < 13; i++)
                    {
                        switch (i)
                        {
                            case 0: nameMes = "Tipo"; break;
                            case 1: nameMes = "Enero"; break;
                            case 2: nameMes = "Febrero"; break;
                            case 3: nameMes = "Marzo"; break;
                            case 4: nameMes = "Abril"; break;
                            case 5: nameMes = "Mayo"; break;
                            case 6: nameMes = "Junio"; break;
                            case 7: nameMes = "Julio"; break;
                            case 8: nameMes = "Agosto"; break;
                            case 9: nameMes = "Septiembre"; break;
                            case 10: nameMes = "Octubre"; break;
                            case 11: nameMes = "Noviembre"; break;
                            case 12: nameMes = "Diciembre"; break;
                        }

                        dt.Columns.Add(nameMes, typeof(string));
                    }

                    if (dt_Real.Rows.Count > 0)
                    {
                        var gTipo = dt_Real.AsEnumerable().GroupBy(x => x.Field<string>("Material")).ToList();
                        foreach (var g in gTipo)
                        {
                            var deta = dt_Real.AsEnumerable().Where(x => x.Field<string>("Material") == g.Key).ToList();

                            DataRow dr;
                            dr = dt.NewRow();
                            dr[0] = deta.FirstOrDefault().Field<string>("Descripcion");
                            for (int i = 1; i < 13; i++)
                            {
                                var dat = deta.Where(x => x.Field<int>("Mes") == i).FirstOrDefault();
                                if (dat == null)
                                {
                                    dr[i] = string.Empty;
                                }
                                else { dr[i] = dat.Field<decimal>("total").ToString(); }
                            }
                            dt.Rows.Add(dr);
                        }

                        txttotReal.Text = dt_Real.AsEnumerable().Sum(x => x.Field<decimal>("total")).ToString();
                    }

                    dgvReal.DataSource = dt;

                    dgvReal.Columns[0].Width = 190;
                    dgvReal.Columns[1].Width = 65;
                    dgvReal.Columns[2].Width = 65;
                    dgvReal.Columns[3].Width = 65;
                    dgvReal.Columns[4].Width = 65;
                    dgvReal.Columns[5].Width = 65;
                    dgvReal.Columns[6].Width = 65;
                    dgvReal.Columns[7].Width = 65;
                    dgvReal.Columns[8].Width = 65;
                    dgvReal.Columns[9].Width = 65;
                    dgvReal.Columns[10].Width = 65;
                    dgvReal.Columns[11].Width = 65;
                    dgvReal.Columns[12].Width = 65;

                    dgvReal.AllowUserToResizeRows = false;
                    dgvReal.AllowUserToResizeColumns = false;
                    foreach (DataGridViewColumn column in dgvReal.Columns)
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

        private void Load_RealPB()
        {
            try
            {
                Funciones funciones = new Funciones();
                funciones.formatearDataGridViewWhite(dgvRealPB);
                DataTable dt = new DataTable();

                string nameMes = string.Empty;
                for (int i = 0; i < 13; i++)
                {
                    switch (i)
                    {
                        case 0: nameMes = "Tipo"; break;
                        case 1: nameMes = "Enero"; break;
                        case 2: nameMes = "Febrero"; break;
                        case 3: nameMes = "Marzo"; break;
                        case 4: nameMes = "Abril"; break;
                        case 5: nameMes = "Mayo"; break;
                        case 6: nameMes = "Junio"; break;
                        case 7: nameMes = "Julio"; break;
                        case 8: nameMes = "Agosto"; break;
                        case 9: nameMes = "Septiembre"; break;
                        case 10: nameMes = "Octubre"; break;
                        case 11: nameMes = "Noviembre"; break;
                        case 12: nameMes = "Diciembre"; break;
                    }

                    dt.Columns.Add(nameMes, typeof(string));
                }

                DataRow dr;
                dr = dt.NewRow();
                dr[0] = "VENTAS PB " + txtanio.Text;
                if (dt_PB.Rows.Count > 0)
                {
                    for (int i = 1; i < 13; i++)
                    {
                        dr[i] = dt_PB.AsEnumerable().Where(x => x.Field<int>("Mes") == i).Sum(x => x.Field<decimal>("total")).ToString();
                    }
                }
                dt.Rows.Add(dr);

                dr = dt.NewRow();
                dr[0] = "VENTAS REAL " + txtanio.Text;
                if (dt_Real.Rows.Count > 0)
                {
                    for (int i = 1; i < 13; i++)
                    {
                        dr[i] = dt_Real.AsEnumerable().Where(x => x.Field<int>("Mes") == i).Sum(x => x.Field<decimal>("total")).ToString();
                    }
                }
                dt.Rows.Add(dr);

                dgvRealPB.DataSource = dt;

                dgvRealPB.Columns[0].Width = 190;
                dgvRealPB.Columns[1].Width = 65;
                dgvRealPB.Columns[2].Width = 65;
                dgvRealPB.Columns[3].Width = 65;
                dgvRealPB.Columns[4].Width = 65;
                dgvRealPB.Columns[5].Width = 65;
                dgvRealPB.Columns[6].Width = 65;
                dgvRealPB.Columns[7].Width = 65;
                dgvRealPB.Columns[8].Width = 65;
                dgvRealPB.Columns[9].Width = 65;
                dgvRealPB.Columns[10].Width = 65;
                dgvRealPB.Columns[11].Width = 65;
                dgvRealPB.Columns[12].Width = 65;

                dgvRealPB.AllowUserToResizeRows = false;
                dgvRealPB.AllowUserToResizeColumns = false;
                foreach (DataGridViewColumn column in dgvRealPB.Columns)
                {
                    column.SortMode = DataGridViewColumnSortMode.NotSortable;
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void ui_updventas_Load(object sender, EventArgs e)
        {

        }

        private void dgvPB_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (dgvPB.RowCount > 0)
                {
                    string valor = dgvPB.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
                    string anio = txtanio.Text;

                    if (valor.Trim() != string.Empty)
                    {
                        string cabecera = dgvPB.Columns[e.ColumnIndex].HeaderCell.Value.ToString();
                        string descrip = dgvPB.Rows[e.RowIndex].Cells[0].Value.ToString();
                        decimal n = 0;

                        if (!decimal.TryParse(valor, out n))
                        {
                            dgvPB.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = string.Empty;
                            MessageBox.Show("Dato ingresado incorrecto", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        else
                        {
                            string mes = string.Empty;
                            switch (cabecera)
                            {
                                case "Enero": mes = "-01-01"; break;
                                case "Febrero": mes = "-02-01"; break;
                                case "Marzo": mes = "-03-01"; break;
                                case "Abril": mes = "-04-01"; break;
                                case "Mayo": mes = "-05-01"; break;
                                case "Junio": mes = "-06-01"; break;
                                case "Julio": mes = "-07-01"; break;
                                case "Agosto": mes = "-08-01"; break;
                                case "Septiembre": mes = "-09-01"; break;
                                case "Octubre": mes = "-10-01"; break;
                                case "Noviembre": mes = "-11-01"; break;
                                case "Diciembre": mes = "-12-01"; break;
                            }

                            string query = @"UPDATE [SERVHISTORI].[database_indicadores].[dbo].[Presupuesto] SET valor=" + valor + " WHERE dtipo='" + descrip + "' AND fecha='" + anio + mes + "'";
                            SqlConnection conexion = new SqlConnection();
                            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
                            conexion.Open();
                            try
                            {
                                SqlCommand myCommand = new SqlCommand(query, conexion);
                                myCommand.ExecuteNonQuery();
                                myCommand.Dispose();

                                Load_PB(txtanio.Text);
                                Load_Real(txtanio.Text);
                                Load_RealPB();
                            }
                            catch (SqlException ex)
                            {
                                MessageBox.Show(ex.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                            }
                            conexion.Close();
                        }
                    }
                }
            }
            catch (FormatException ex)
            {
                MessageBox.Show(ex.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}