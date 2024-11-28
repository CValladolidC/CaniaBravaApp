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
    public partial class ui_solialma : Form
    {
        Funciones funciones = new Funciones();
        string _codcia;

        public ui_solialma()
        {
            InitializeComponent();

            if (funciones.VersionAssembly()) Application.ExitThread();
        }

        private void ui_solialma_Load(object sender, EventArgs e)
        {
            GlobalVariables gv = new GlobalVariables();
            MaesGen maesgen = new MaesGen();
            this._codcia = gv.getValorCia();
            string usuario = gv.getValorUsr();
            string codcia = this._codcia;
            string query = " Select A.codalma as clave,A.desalma as descripcion ";
            query += " from alalma A /*inner join almausr B on A.codcia=B.codcia and A.codalma=B.codalma */";
            query += " where /*A.codcia='" + @codcia + "' and */A.estado='V' /*and B.idusr='" + @usuario + "' */order by 1 asc; ";
            //maesgen.listaDetMaesGen("150", cmbSeccion, "");
            DateTime hoy = DateTime.Today;
            DateTime anterior = hoy.AddDays(-7);
            txtFechaIni.Value = anterior;
            txtFechaFin.Value = DateTime.Now;
            funciones.listaComboBox(query, cmbAlmacen, "");
            ui_ListaSolAlma();
        }

        private void ui_ListaSolAlma()
        {
            string codcia = this._codcia;
            string alma = funciones.getValorComboBox(cmbAlmacen, 2);
            string secsoli = "";// funciones.getValorComboBox(cmbSeccion, 4);
            string fechaini = txtFechaIni.Value.ToString("yyyy-MM-dd");
            string fechafin = txtFechaFin.Value.ToString("yyyy-MM-dd");

            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();
            string query = "select A.alma/*,A.secsoli*/,A.solalma,CONVERT(VARCHAR(10),A.fecha,120),A.hora,A.titulo, ";
            query += "/*A.dessoli1,*/A.usuario,CASE A.estado WHEN 'V' THEN 'VIGENTE' ELSE 'FINALIZADO' END AS estado ";
            query += "from solalmac A where A.alma='" + @alma + "' /*and A.codcia='" + @codcia + "' and A.secsoli='" + @secsoli + "'*/ ";
            query += "and A.fecha BETWEEN '" + @fechaini + "' and '" + @fechafin + "' ";
            query += "order by A.alma asc ,A.secsoli asc ,A.solalma asc;";
            try
            {
                using (SqlDataAdapter myDataAdapter = new SqlDataAdapter(query, conexion))
                {
                    DataSet myDataSet = new DataSet();
                    myDataAdapter.Fill(myDataSet, "tblData");

                    funciones.formatearDataGridView(dgvData);
                    dgvData.DataSource = myDataSet.Tables["tblData"];
                    dgvData.Columns[0].HeaderText = "Comedor";
                    //dgvData.Columns[1].HeaderText = "Sección";
                    dgvData.Columns[1].HeaderText = "Código";
                    dgvData.Columns[2].HeaderText = "Fecha";
                    dgvData.Columns[3].HeaderText = "Hora";
                    dgvData.Columns[4].HeaderText = "Titulo";
                    //dgvData.Columns[5].HeaderText = "Descripción";
                    dgvData.Columns[5].HeaderText = "Usuario";
                    dgvData.Columns[6].HeaderText = "Estado";

                    dgvData.Columns[0].Width = 65;
                    //dgvData.Columns[1].Width = 50;
                    dgvData.Columns[1].Width = 63;
                    dgvData.Columns[2].Width = 75;
                    dgvData.Columns[3].Width = 70;
                    dgvData.Columns[4].Width = 330;
                    //dgvData.Columns[5].Width = 300;
                    dgvData.Columns[5].Width = 100;
                    dgvData.Columns[6].Width = 100;

                    dgvData.AllowUserToResizeRows = false;
                    dgvData.AllowUserToResizeColumns = false;
                    foreach (DataGridViewColumn column in dgvData.Columns)
                    {
                        column.SortMode = DataGridViewColumnSortMode.NotSortable;
                    }
                }
                ui_calculaNumRegistros();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            conexion.Close();
        }

        public void ui_calculaNumRegistros()
        {
            SolAlma solalma = new SolAlma();
            string codcia = this._codcia;
            string alma = funciones.getValorComboBox(cmbAlmacen, 2);
            string secsoli = "";// funciones.getValorComboBox(cmbSeccion, 4);
            txtRegTotal.Text = string.Empty;
            string numreg = solalma.getNumeroRegistros(codcia, alma, secsoli);
            txtRegTotal.Text = funciones.replicateCadena(" ", 15 - numreg.Trim().Length) + numreg.Trim() + funciones.replicateCadena(" ", 20) + "Registrados";
            string numregencontrados = Convert.ToString(dgvData.RowCount);
            txtRegEncontrados.Text = funciones.replicateCadena(" ", 15 - numregencontrados.Trim().Length) + numregencontrados.Trim() + funciones.replicateCadena(" ", 20) + " De acuerdo al criterio de búsqueda";
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            string codcia = this._codcia;
            string alma = funciones.getValorComboBox(cmbAlmacen, 2);
            string desalma = cmbAlmacen.Text;
            string secsoli = "";// funciones.getValorComboBox(cmbSeccion, 4);
            string desseccion = "";// cmbSeccion.Text;
            if (alma != string.Empty)
            {
                //if (secsoli != string.Empty)
                {
                    ui_updsolialma ui_updsolialma = new ui_updsolialma();
                    ui_updsolialma._FormPadre = this;
                    ui_updsolialma.Activate();
                    ui_updsolialma.setData(codcia, alma, secsoli, desalma, desseccion);
                    ui_updsolialma.ui_ActualizaComboBox();
                    ui_updsolialma.BringToFront();
                    ui_updsolialma.agregar();
                    ui_updsolialma.ShowDialog();
                    ui_updsolialma.Dispose();
                }
                //else
                //{
                //    MessageBox.Show("No ha seleccionado Sección", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //}
            }
            else
            {
                MessageBox.Show("No ha seleccionado Almacén de Trabajo", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            Exporta exporta = new Exporta();
            exporta.Excel_FromDataGridView(dgvData);
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            ui_ListaSolAlma();
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            Int32 selectedCellCount = dgvData.GetCellCount(DataGridViewElementStates.Selected);
            if (selectedCellCount > 0)
            {
                string desalma = cmbAlmacen.Text;
                string desseccion = "";// cmbSeccion.Text;
                string alma = dgvData.Rows[dgvData.SelectedCells[1].RowIndex].Cells[0].Value.ToString();
                string secsoli = "";// dgvData.Rows[dgvData.SelectedCells[1].RowIndex].Cells[1].Value.ToString();
                string solalma = dgvData.Rows[dgvData.SelectedCells[1].RowIndex].Cells[1].Value.ToString();
                string codcia = alma;
                ui_updsolialma ui_updsolialma = new ui_updsolialma();
                ui_updsolialma._FormPadre = this;
                ui_updsolialma.Activate();
                ui_updsolialma.BringToFront();
                ui_updsolialma.setData(codcia, alma, secsoli, desalma, desseccion);
                ui_updsolialma.ui_ActualizaComboBox();
                ui_updsolialma.editar(codcia, alma, secsoli, solalma);
                ui_updsolialma.ShowDialog();
                ui_updsolialma.Dispose();
            }
            else
            {
                MessageBox.Show("No ha seleccionado registro a Editar", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void cmbAlmacen_SelectedIndexChanged(object sender, EventArgs e)
        {
            ui_ListaSolAlma();
        }

        private void cmbSeccion_SelectedIndexChanged(object sender, EventArgs e)
        {
            ui_ListaSolAlma();
        }

        private void txtFechaIni_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                if (UtiFechas.IsDate(txtFechaIni.Text))
                {
                    ui_ListaSolAlma();
                    e.Handled = true;
                    SendKeys.Send("{TAB}");
                }
                else
                {
                    MessageBox.Show("Fecha Inicial no válida", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    e.Handled = true;
                    txtFechaIni.Focus();
                }
            }
        }

        private void txtFechaFin_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                if (UtiFechas.IsDate(txtFechaFin.Text))
                {
                    ui_ListaSolAlma();
                    e.Handled = true;
                    SendKeys.Send("{TAB}");
                }
                else
                {
                    MessageBox.Show("Fecha Final no válida", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    e.Handled = true;
                    txtFechaFin.Focus();
                }
            }
        }

        private void btnTodos_Click(object sender, EventArgs e)
        {
            txtBuscar.Clear();
            ui_ListaSolAlma();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            string buscar = txtBuscar.Text.Trim();

            if (buscar != string.Empty)
            {
                string codcia = this._codcia;
                string alma = funciones.getValorComboBox(cmbAlmacen, 2);
                string secsoli = "";// funciones.getValorComboBox(cmbSeccion, 4);
                txtRegEncontrados.Text = string.Empty;
                string cadenaBusqueda = string.Empty;

                if (radioButtonCodigo.Checked && txtBuscar.Text.Trim() != string.Empty)
                {
                    cadenaBusqueda = " A.solalma='" + @buscar + "' ";
                }
                else
                {
                    if (radioButtonUsuario.Checked && txtBuscar.Text.Trim() != string.Empty)
                    {
                        cadenaBusqueda = " A.usuario='" + @buscar + "' ";
                    }
                    else
                    {
                        cadenaBusqueda = " A.titulo like '%" + @buscar + "%' ";
                    }
                }

                string query = "  select A.alma/*,A.secsoli*/,A.solalma,A.fecha,A.hora,A.titulo,";
                query = query + " A.dessoli1,A.usuario,CASE A.estado WHEN 'V' THEN 'VIGENTE' ELSE 'FINALIZADO' END AS estado  from solalmac A ";
                query = query + " where A.alma='" + @alma + "' /*and A.codcia='" + @codcia + "' and A.secsoli='" + @secsoli + "'*/ ";
                query = query + " and " + @cadenaBusqueda;
                query = query + " order by A.alma asc ,A.secsoli asc ,A.solalma asc;";

                SqlConnection conexion = new SqlConnection();
                conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
                conexion.Open();
                try
                {
                    using (SqlDataAdapter myDataAdapter = new SqlDataAdapter(query, conexion))
                    {
                        DataSet myDataSet = new DataSet();
                        myDataAdapter.Fill(myDataSet, "tblData");
                        funciones.formatearDataGridView(dgvData);
                        dgvData.DataSource = myDataSet.Tables["tblData"];
                        dgvData.Columns[0].HeaderText = "Almacén";
                        //dgvData.Columns[1].HeaderText = "Sección";
                        dgvData.Columns[1].HeaderText = "Código";
                        dgvData.Columns[2].HeaderText = "Fecha";
                        dgvData.Columns[3].HeaderText = "Hora";
                        dgvData.Columns[4].HeaderText = "Titulo";
                        dgvData.Columns[5].HeaderText = "Descripción";
                        dgvData.Columns[6].HeaderText = "Usuario";
                        dgvData.Columns[7].HeaderText = "E.";
                        dgvData.Columns[0].Width = 50;
                        //dgvData.Columns[1].Width = 50;
                        dgvData.Columns[1].Width = 63;
                        dgvData.Columns[2].Width = 75;
                        dgvData.Columns[3].Width = 50;
                        dgvData.Columns[4].Width = 180;
                        dgvData.Columns[5].Width = 300;
                        dgvData.Columns[6].Width = 100;
                        dgvData.Columns[7].Width = 100;
                    }
                    ui_calculaNumRegistros();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }

                conexion.Close();
            }
        }

        private void radioButtonUsuario_CheckedChanged(object sender, EventArgs e)
        {
            txtBuscar.Text = string.Empty;
            txtBuscar.Focus();
        }

        private void radioButtonCodigo_CheckedChanged(object sender, EventArgs e)
        {
            txtBuscar.Text = string.Empty;
            txtBuscar.Focus();
        }

        private void radioButtonTitulo_CheckedChanged(object sender, EventArgs e)
        {
            txtBuscar.Text = string.Empty;
            txtBuscar.Focus();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            Int32 selectedCellCount =
            dgvData.GetCellCount(DataGridViewElementStates.Selected);
            if (selectedCellCount > 0)
            {
                GlobalVariables gv = new GlobalVariables();
                ui_autentica ui_autentica = new ui_autentica();
                ui_autentica._tipousr = "M";
                ui_autentica.Activate();
                ui_autentica.BringToFront();
                ui_autentica.ShowDialog();
                ui_autentica.Dispose();

                string autentica = gv.getAutentica();
                if (autentica.Equals("S"))
                {
                    string codcia = this._codcia;
                    string alma = dgvData.Rows[dgvData.SelectedCells[1].RowIndex].Cells[0].Value.ToString();
                    string secsoli = "";// dgvData.Rows[dgvData.SelectedCells[1].RowIndex].Cells[1].Value.ToString();
                    string codsolalma = dgvData.Rows[dgvData.SelectedCells[1].RowIndex].Cells[1].Value.ToString();
                    DialogResult resultado = MessageBox.Show("¿Desea eliminar la solicitud " + @codsolalma + "?",
                    "Consulta Importante",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);
                    if (resultado == DialogResult.Yes)
                    {
                        SolAlma solalma = new SolAlma();
                        solalma.delSolAlma(codcia, alma, secsoli, codsolalma);
                        ui_ListaSolAlma();
                    }
                }
            }
            else
            {
                MessageBox.Show("No ha seleccionado registro a Eliminar", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void dgvData_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if ((this.dgvData.Rows[e.RowIndex].Cells["estado"].Value).ToString().Equals("V"))
            {
                foreach (DataGridViewCell celda in

                this.dgvData.Rows[e.RowIndex].Cells)
                {
                    celda.Style.BackColor = Color.BurlyWood;
                    celda.Style.ForeColor = Color.Black;
                }
            }

        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            Int32 selectedCellCount =
            dgvData.GetCellCount(DataGridViewElementStates.Selected);
            if (selectedCellCount > 0)
            {
                string codcia = this._codcia;
                string alma = dgvData.Rows[dgvData.SelectedCells[1].RowIndex].Cells[0].Value.ToString();
                string secsoli = "";// dgvData.Rows[dgvData.SelectedCells[1].RowIndex].Cells[1].Value.ToString();
                string codsolalma = dgvData.Rows[dgvData.SelectedCells[1].RowIndex].Cells[1].Value.ToString();
                string estado = dgvData.Rows[dgvData.SelectedCells[1].RowIndex].Cells[6].Value.ToString();
                if (estado.Equals("FINALIZADO"))
                {
                    SolAlma solalma = new SolAlma();
                    string strPrinter = solalma.imprime(codcia, alma, secsoli, codsolalma);
                    ui_reportetxt ui_reportetxt = new ui_reportetxt();
                    ui_reportetxt._texto = strPrinter;
                    ui_reportetxt.Activate();
                    ui_reportetxt.BringToFront();
                    ui_reportetxt.ShowDialog();
                    ui_reportetxt.Dispose();
                }
                else
                {
                    MessageBox.Show("No se puede imprimir. Solicitud de pedido no finalizada", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
            }
            else
            {
                MessageBox.Show("No ha seleccionado registro a Imprimir", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void btnDesFin_Click(object sender, EventArgs e)
        {
            Int32 selectedCellCount =
         dgvData.GetCellCount(DataGridViewElementStates.Selected);
            if (selectedCellCount > 0)
            {
                GlobalVariables gv = new GlobalVariables();
                string codcia = this._codcia;
                string alma = dgvData.Rows[dgvData.SelectedCells[1].RowIndex].Cells[0].Value.ToString();
                string secsoli = dgvData.Rows[dgvData.SelectedCells[1].RowIndex].Cells[1].Value.ToString();
                string codsolalma = dgvData.Rows[dgvData.SelectedCells[1].RowIndex].Cells[1].Value.ToString();
                ui_autentica ui_autentica = new ui_autentica();
                ui_autentica._tipousr = "M";
                ui_autentica.Activate();
                ui_autentica.BringToFront();
                ui_autentica.ShowDialog();
                ui_autentica.Dispose();

                string autentica = gv.getAutentica();
                if (autentica.Equals("S"))
                {
                    DialogResult resultado = MessageBox.Show("¿Desea DESFINALIZAR la solicitud " + @codsolalma + "?",
                       "Consulta Importante",
                       MessageBoxButtons.YesNo,
                       MessageBoxIcon.Question);
                    if (resultado == DialogResult.Yes)
                    {
                        SolAlma solalma = new SolAlma();
                        solalma.updDesFinSolAlma(codcia, alma, secsoli, codsolalma);
                        ui_ListaSolAlma();
                    }
                }

            }
            else
            {
                MessageBox.Show("No ha seleccionado registro a DESFINALIZAR", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void txtFechaIni_ValueChanged(object sender, EventArgs e)
        {
            ui_ListaSolAlma();
            SendKeys.Send("{TAB}");
        }

        private void txtFechaFin_ValueChanged(object sender, EventArgs e)
        {
            ui_ListaSolAlma();
            SendKeys.Send("{TAB}");
        }
    }
}