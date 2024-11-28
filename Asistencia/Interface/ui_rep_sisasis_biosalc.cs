using System;
using System.Data;
using System.Windows.Forms;
using System.Configuration;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Globalization;

namespace CaniaBrava
{
    public partial class ui_rep_sisasis_biosalc : Form
    {
        //Oliver Cruz Tuanama
        GlobalVariables variables = new GlobalVariables();
        Funciones funciones = new Funciones();
        SqlConnection conexion = new SqlConnection();
        DataTable dt = new DataTable();

        private TextBox TextBoxActivo;
        public TextBox _TextBoxActivo
        {
            get { return TextBoxActivo; }
            set { TextBoxActivo = value; }
        }

        public ui_rep_sisasis_biosalc()
        {
            InitializeComponent();

            if (funciones.VersionAssembly()) Application.ExitThread();
        }

        private void ui_rep_asistencia_Load(object sender, EventArgs e)
        {
            string query = "SELECT idusr [clave],desusr [descripcion] FROM usrfile (NOLOCK) WHERE typeusr='05' AND desusr NOT LIKE '%COMEDOR%'";
            funciones.listaComboBox(query, cmbSedes, "X");

            cmbTipo.Text = "00  TODOS";
        }

        private void ui_Lista()
        {
            dgvdetalle.DataSource = null;
            btnGenerar.Invoke((MethodInvoker)delegate { loadingNext1.Visible = true; });
            Application.DoEvents();

            string sede = funciones.getValorComboBox(cmbSedes, 7);
            string tipo = funciones.getValorComboBox(cmbTipo, 2);

            if (sede == "X   TOD") { sede = string.Empty; }

            string fecha1 = dtpFecini.Value.ToString("yyyy-MM-dd");
            string fecha2 = dtpFecfin.Value.ToString("yyyy-MM-dd");
            string query = "EXEC SP_REPORTE_SISASIS_BIOSALC '" + fecha1 + "','" + fecha2 + "','" + sede + "','" + tipo + "';";

            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();

            try
            {
                using (SqlDataAdapter myDataAdapter = new SqlDataAdapter())
                {
                    myDataAdapter.SelectCommand = new SqlCommand(query, conexion);
                    myDataAdapter.SelectCommand.CommandTimeout = 1360;
                    dt = new DataTable();
                    myDataAdapter.Fill(this.dt);
                    funciones.formatearDataGridView(dgvdetalle);

                    dgvdetalle.DataSource = dt;

                    dgvdetalle.Columns[0].Width = 70;
                    dgvdetalle.Columns[1].Width = 240;
                    dgvdetalle.Columns[5].Width = 140;

                    dgvdetalle.AllowUserToResizeRows = false;
                    dgvdetalle.AllowUserToResizeColumns = false;
                    foreach (DataGridViewColumn column in dgvdetalle.Columns)
                    {
                        column.SortMode = DataGridViewColumnSortMode.NotSortable;
                    }

                    btnExcel.Visible = false;
                    txtBuscar.Clear();
                    txtBuscar.Enabled = false;
                    if (dgvdetalle.Rows.Count > 0) { btnExcel.Visible = true; txtBuscar.Enabled = true; txtBuscar.Focus(); }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }

            conexion.Close();

            lblregistros.Text = "0 registros encontrados";
            if (dgvdetalle.Rows.Count == 1) { lblregistros.Text = dgvdetalle.Rows.Count + " registro encontrado"; }
            else { lblregistros.Text = dgvdetalle.Rows.Count + " registros encontrados"; }

            btnGenerar.Invoke((MethodInvoker)delegate { loadingNext1.Visible = false; });
            Application.DoEvents();
        }

        #region _Click
        private void btnSalir_Click(object sender, EventArgs e) { Close(); }

        private void btnActualizar_Click(object sender, EventArgs e) { ui_Lista(); }
        
        private void btnExcel_Click(object sender, EventArgs e)
        {
            Exporta exporta = new Exporta();
            exporta.Excel_FromDataGridView(dgvdetalle);
        }

        private void btnGenerar_Click(object sender, EventArgs e)
        {
            ui_Lista();
        }
        #endregion

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            ui_upd_asistencia ui_detalle = new ui_upd_asistencia();
            ui_detalle._FormPadre = this;
            ui_detalle.Activate();
            ui_detalle.BringToFront();
            ui_detalle.ShowDialog();
            ui_detalle.Dispose();
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            Int32 selectedCellCount = dgvdetalle.GetCellCount(DataGridViewElementStates.Selected);
            if (selectedCellCount > 0)
            {
                string id = dgvdetalle.Rows[dgvdetalle.SelectedCells[1].RowIndex].Cells[18].Value.ToString();
                string idperplan = dgvdetalle.Rows[dgvdetalle.SelectedCells[1].RowIndex].Cells[0].Value.ToString();
                string fecha = dgvdetalle.Rows[dgvdetalle.SelectedCells[1].RowIndex].Cells[7].Value.ToString();

                ui_upd_asistencia ui_detalle = new ui_upd_asistencia();
                ui_detalle._FormPadre = this;
                ui_detalle.Editar(id, idperplan, fecha);
                ui_detalle.Activate();
                ui_detalle.BringToFront();
                ui_detalle.ShowDialog();
                ui_detalle.Dispose();
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            Int32 selectedCellCount = dgvdetalle.GetCellCount(DataGridViewElementStates.Selected);
            if (selectedCellCount > 0)
            {
                string id = dgvdetalle.Rows[dgvdetalle.SelectedCells[1].RowIndex].Cells[18].Value.ToString();
                string idperplan = dgvdetalle.Rows[dgvdetalle.SelectedCells[1].RowIndex].Cells[0].Value.ToString();
                string fecha = dgvdetalle.Rows[dgvdetalle.SelectedCells[1].RowIndex].Cells[7].Value.ToString();
                string regentrad = dgvdetalle.Rows[dgvdetalle.SelectedCells[1].RowIndex].Cells[10].Value.ToString();
                string regsalid = dgvdetalle.Rows[dgvdetalle.SelectedCells[1].RowIndex].Cells[11].Value.ToString();

                string query = "SELECT a.nromov,a.fecha,LEFT(CONVERT(TIME,a.regEntrada,120),5),LEFT(CONVERT(TIME,a.regSalida,120),5),";
                query += "b.desusr,CONVERT(VARCHAR(16),a.regEntrada,120),CONVERT(VARCHAR(16),a.regSalida,120),a.idlogin+'  '+b.desusr ";
                query += "FROM control a (NOLOCK) INNER JOIN usrfile b (NOLOCK) ON b.idusr=a.idlogin ";
                query += "WHERE a.fecha = '" + fecha + "' AND a.idperplan = '" + idperplan + "' ORDER BY a.regEntrada,a.hora";
                SqlConnection conexion = new SqlConnection();
                conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
                conexion.Open();
                int contador = 0;

                try
                {
                    SqlCommand myCommand = new SqlCommand(query, conexion);
                    SqlDataReader odr = myCommand.ExecuteReader();

                    while (odr.Read())
                    {
                        contador++;
                    }

                    odr.Close();
                    myCommand.Dispose();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
                finally { conexion.Close(); }

                if (contador == 1)
                {
                    var confirmResult = MessageBox.Show("¿Esta seguro en Eliminar del registro seleccionado?", "Confirmación", MessageBoxButtons.YesNo);
                    if (confirmResult == DialogResult.Yes)
                    {
                        GlobalVariables variable = new GlobalVariables();
                        DateTime? entrada = null, salida = null;
                        entrada = DateTime.ParseExact(fecha + " " + ObtenerHora24(regentrad), "yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture);
                        if (regsalid != string.Empty)
                        {
                            salida = DateTime.ParseExact(fecha + " " + ObtenerHora24(regsalid), "yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture);
                        }

                        query = "DELETE FROM control WHERE nromov = '" + id + "';";
                        query += "INSERT INTO controlhisto VALUES ((SELECT ISNULL(MAX(id),0)+1 FROM controlhisto (NOLOCK)),'" + id + "','" +
                            idperplan + "','" + fecha + "','" + entrada.Value.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                            (salida != null ? "'" + salida.Value.ToString("yyyy-MM-dd HH:mm:ss") + "'" : "null") + ",'REGISTRO ELIMINADO','" + variable.getValorUsr() + "');";


                        conexion = new SqlConnection();
                        conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
                        conexion.Open();

                        try
                        {
                            SqlCommand myCommand = new SqlCommand(query, conexion);
                            myCommand.ExecuteNonQuery();
                            myCommand.Dispose();
                            MessageBox.Show("Registro Eliminado..", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            ui_Lista();
                        }
                        catch (SqlException ex)
                        {
                            MessageBox.Show(ex.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        }
                        conexion.Close();
                    }
                }
                else
                {
                    ui_upd_asistencia ui_detalle = new ui_upd_asistencia();
                    ui_detalle._FormPadre = this;
                    ui_detalle.Editar(id, idperplan, fecha);
                    ui_detalle.Activate();
                    ui_detalle.BringToFront();
                    ui_detalle.ShowDialog();
                    ui_detalle.Dispose();
                }
            }
        }

        private string ObtenerHora24(string hora)
        {
            string[] cad = hora.Split(':');
            string min = cad[1].Substring(0, 2);
            string format = cad[1].Substring(cad[1].Length - 2, 2);
            int hor = int.Parse(cad[0]);
            string resultado = string.Empty;
            if (format == "pm")
            {
                resultado = (hor + 12).ToString();
            }
            else { resultado = "0" + cad[0].Trim(); resultado = resultado.Substring(resultado.Length - 2, 2); }

            return resultado + ":" + min;
        }

        private void dtpFecini_ValueChanged(object sender, EventArgs e)
        {
            dtpFecfin.MinDate = dtpFecini.Value;
            //dtpFecfin.MaxDate = DateTime.Parse(dtpFecini.Value.Year + "-" + (dtpFecini.Value.Month + 1).ToString("00") + "-01").AddDays(-1);
            if (dtpFecini.Value > dtpFecfin.Value)
            {
                dtpFecfin.Value = dtpFecini.Value;
            }
        }

        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            try
            {
                var query = dt.AsEnumerable().Where(x => x.Field<string>("Trabajador").Contains(txtBuscar.Text.ToUpper())).CopyToDataTable();
                dgvdetalle.DataSource = query;

                dgvdetalle.Columns[0].Width = 70;
                dgvdetalle.Columns[1].Width = 240;
                dgvdetalle.Columns[5].Width = 140;
            }
            catch (Exception ex_)
            {
                dgvdetalle.DataSource = null;
            }
        }
    }
}