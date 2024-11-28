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
    public partial class ui_almov : Form
    {
        Funciones funciones = new Funciones();
        string _codcia;

        public ui_almov()
        {
            InitializeComponent();

            if (funciones.VersionAssembly()) Application.ExitThread();
        }

        private void ui_almov_Load(object sender, EventArgs e)
        {
            GlobalVariables gv = new GlobalVariables();

            string query = " Select A.codalma as clave,A.desalma as descripcion ";
            query += " from alalma A ";
            query += " where A.estado='V' order by 1 asc; ";
            funciones.listaComboBox(query, cmbComedor, "");

            DateTime hoy = DateTime.Today;
            DateTime anterior = hoy.AddDays(-7);
            txtFechaIni.Value = anterior;
            txtFechaFin.Value = DateTime.Now;
            ui_ListaAlmov();
        }

        private DataTable ui_ListaEnlazados(string codcia, string alma, string td, string numdoc)
        {
            DataTable dtmov = new DataTable();
            try
            {
                if (td.Equals("PE"))
                {
                    string query;
                    SqlConnection conexion = new SqlConnection();
                    conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
                    conexion.Open();
                    query = " select A.alma,A.td,A.numdoc,A.fecdoc,A.codmov,B.item,C.desarti,B.lote,";
                    query = query + " B.cantidad from almovc A left join almovd B on ";
                    query = query + " A.codcia=B.codcia and A.alma=B.alma and A.td=B.td and A.numdoc=B.numdoc ";
                    query = query + " left join alarti C on B.codcia=C.codcia and B.codarti=C.codarti ";
                    query = query + " where /*A.codcia='" + @codcia + "' and */A.alma='" + @alma + "' and A.td='PS' and B.lote in ";
                    query = query + " (Select lote from almovd where /*codcia='" + @codcia + "' and */alma='" + @alma + "' ";
                    query = query + " and td='" + @td + "' and numdoc='" + @numdoc + "') ";
                    query = query + " order by A.alma asc ,A.td asc ,A.numdoc asc;";

                    SqlDataAdapter damov = new SqlDataAdapter();
                    damov.SelectCommand = new SqlCommand(query, conexion);
                    damov.Fill(dtmov);
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            return dtmov;
        }

        public void ui_calculaNumRegistros()
        {
            AlmovC almovc = new AlmovC();
            string codcia = this._codcia;
            string alma = funciones.getValorComboBox(cmbComedor, 2);
            txtRegTotal.Text = string.Empty;
            string numreg = almovc.getNumeroRegistros(codcia, alma);
            txtRegTotal.Text = funciones.replicateCadena(" ", 15 - numreg.Trim().Length) + numreg.Trim() + funciones.replicateCadena(" ", 20) + "Registrados";
            string numregencontrados = Convert.ToString(dgvData.RowCount);
            txtRegEncontrados.Text = funciones.replicateCadena(" ", 15 - numregencontrados.Trim().Length) + numregencontrados.Trim() + funciones.replicateCadena(" ", 20) + " De acuerdo al criterio de búsqueda";
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ui_ListaAlmov()
        {
            string cadenaBusqueda = string.Empty;
            string buscar = txtBuscar.Text.Trim();

            if (buscar != string.Empty)
            {
                if (radioButtonCodigo.Checked && txtBuscar.Text.Trim() != string.Empty)
                {
                    cadenaBusqueda = "AND A.numdoc='" + @buscar + "' ";
                }
                else
                {
                    if (radioButtonRuc.Checked && txtBuscar.Text.Trim() != string.Empty)
                    {
                        cadenaBusqueda = "AND B.desprovee='" + @buscar + "' ";
                    }
                    else
                    {
                        cadenaBusqueda = "AND A.rfndoc='" + @buscar + "' ";
                    }
                }
            }
            string codcia = this._codcia;
            string alma = funciones.getValorComboBox(cmbComedor, 2);
            string fechaini = txtFechaIni.Value.ToString("yyyy-MM-dd");
            string fechafin = txtFechaFin.Value.ToString("yyyy-MM-dd");

            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();
            string query = " select A.alma,RTRIM(C.desalma),A.td,A.numdoc,CONVERT(VARCHAR(10),A.fecdoc,120),D.desmov,A.rftdoc,A.rfndoc,B.desprovee, ";
            query += "/*A.glosa1,*/CASE A.situa WHEN 'V' THEN 'VIGENTE' ELSE 'FINALIZADO' END AS situa ";
            query += "from almovc A left join provee B on A.codpro = B.codprovee ";
            query += "inner join alalma C on C.codalma = A.alma ";
            query += "inner join tipomov D on D.tipomov = A.td AND D.codmov=A.codmov ";
            query += "where A.alma='" + @alma + "' and A.flag='ALMA' and A.fecdoc BETWEEN '" + @fechaini + "' and '" + @fechafin + "' ";
            query += cadenaBusqueda;
            query += "order by A.fecdoc desc,A.td asc;";

            try
            {
                using (SqlDataAdapter myDataAdapter = new SqlDataAdapter(query, conexion))
                {
                    DataSet myDataSet = new DataSet();
                    myDataAdapter.Fill(myDataSet, "tblData");
                    funciones.formatearDataGridView(dgvData);
                    dgvData.DataSource = myDataSet.Tables["tblData"];
                    dgvData.Columns[0].HeaderText = "Cod.";
                    dgvData.Columns[1].HeaderText = "Comedor";
                    dgvData.Columns[2].HeaderText = "Tipo";
                    dgvData.Columns[3].HeaderText = "Num.Ope.";
                    dgvData.Columns[4].HeaderText = "Fecha";
                    dgvData.Columns[5].HeaderText = "Cod. Mov.";
                    dgvData.Columns[6].HeaderText = "Doc.Ref.";
                    dgvData.Columns[7].HeaderText = "Nro.Doc Ref.";
                    dgvData.Columns[8].HeaderText = "Proveedor";
                    //dgvData.Columns[9].HeaderText = "Glosa";
                    dgvData.Columns[9].HeaderText = "Estado";

                    if (dgvData.Rows.Count > 12)
                    {
                        dgvData.CurrentCell = dgvData.Rows[dgvData.Rows.Count - 1].Cells[0];
                    }

                    dgvData.Columns[0].Width = 40;
                    dgvData.Columns[1].Width = 160;
                    dgvData.Columns[2].Width = 40;
                    dgvData.Columns[3].Width = 70;
                    dgvData.Columns[4].Width = 75;
                    dgvData.Columns[5].Width = 140;
                    dgvData.Columns[6].Width = 50;
                    dgvData.Columns[7].Width = 100;
                    dgvData.Columns[8].Width = 190;
                    //dgvData.Columns[9].Width = 220;
                    dgvData.Columns[9].Width = 80;

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

        private void cmbAlmacen_SelectedIndexChanged(object sender, EventArgs e)
        {
            ui_ListaAlmov();
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            string codalma = funciones.getValorComboBox(cmbComedor, 2);
            string desalma = cmbComedor.Text;
            string codcia = codalma;
            if (codalma != string.Empty)
            {
                ui_updalmov ui_updalmov = new ui_updalmov();
                ui_updalmov._FormPadre = this;
                ui_updalmov.Activate();
                ui_updalmov.setData(codcia, codalma, desalma);
                ui_updalmov.ui_ActualizaComboBox();
                ui_updalmov.BringToFront();
                ui_updalmov.agregar();
                ui_updalmov.ShowDialog();
                ui_updalmov.Dispose();
            }
            else
            {
                MessageBox.Show("No ha seleccionado Almacén de Trabajo", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtFechaIni_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                if (UtiFechas.IsDate(txtFechaIni.Text))
                {
                    ui_ListaAlmov();
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
                    ui_ListaAlmov();
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

        private void btnEditar_Click(object sender, EventArgs e)
        {
            Int32 selectedCellCount = dgvData.GetCellCount(DataGridViewElementStates.Selected);
            if (selectedCellCount > 0)
            {
                string desalma = cmbComedor.Text;
                string alma = dgvData.Rows[dgvData.SelectedCells[1].RowIndex].Cells[0].Value.ToString();
                string td = dgvData.Rows[dgvData.SelectedCells[1].RowIndex].Cells[2].Value.ToString();
                string numdoc = dgvData.Rows[dgvData.SelectedCells[1].RowIndex].Cells[3].Value.ToString();
                string codcia = alma;
                ui_updalmov ui_updalmov = new ui_updalmov();
                ui_updalmov._FormPadre = this;
                ui_updalmov.Activate();
                ui_updalmov.BringToFront();
                ui_updalmov.setData(codcia, alma, desalma);
                ui_updalmov.ui_ActualizaComboBox();
                ui_updalmov.editar(codcia, alma, td, numdoc);
                ui_updalmov.ShowDialog();
                ui_updalmov.Dispose();
            }
            else
            {
                MessageBox.Show("No ha seleccionado registro a Editar", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void btnTodos_Click(object sender, EventArgs e)
        {
            txtBuscar.Clear();
            ui_ListaAlmov();
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            ui_ListaAlmov();
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            Exporta exporta = new Exporta();
            exporta.Excel_FromDataGridView(dgvData);
        }

        private void radioButtonCodigo_CheckedChanged(object sender, EventArgs e)
        {
            txtBuscar.Text = string.Empty;
            txtBuscar.Focus();
        }

        private void radioButtonRuc_CheckedChanged(object sender, EventArgs e)
        {
            txtBuscar.Text = string.Empty;
            txtBuscar.Focus();
        }

        private void radioButtonGlosa_CheckedChanged(object sender, EventArgs e)
        {
            txtBuscar.Text = string.Empty;
            txtBuscar.Focus();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            string buscar = txtBuscar.Text.Trim();

            if (buscar != string.Empty)
            {
                string codcia = this._codcia;
                string alma = funciones.getValorComboBox(cmbComedor, 2);
                txtRegEncontrados.Text = string.Empty;
                string cadenaBusqueda = string.Empty;

                if (radioButtonCodigo.Checked && txtBuscar.Text.Trim() != string.Empty)
                {
                    cadenaBusqueda = " A.numdoc='" + @buscar + "' ";
                }
                else
                {
                    if (radioButtonGlosa.Checked && txtBuscar.Text.Trim() != string.Empty)
                    {
                        cadenaBusqueda = " A.glosa1 like '%" + @buscar + "%' ";
                    }
                    else
                    {
                        if (radioButtonRuc.Checked && txtBuscar.Text.Trim() != string.Empty)
                        {
                            cadenaBusqueda = " B.ruc='" + @buscar + "' ";
                        }
                        else
                        {
                            cadenaBusqueda = " A.rfndoc='" + @buscar + "' ";
                        }
                    }
                }

                string query = " select A.alma,A.td,A.numdoc,A.fecdoc,A.codmov,A.rftdoc,A.rfndoc,B.desprovee, ";
                query = query + " A.glosa1,A.situa from almovc A left join provee B on A.codpro = B.codprovee ";
                query = query + " where A.codcia='" + @codcia + "' and A.alma='" + @alma + "' and A.flag='ALMA' and " + @cadenaBusqueda;
                query = query + " order by A.alma asc ,A.td asc ,A.numdoc asc;";
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
                        dgvData.Columns[1].HeaderText = "Tipo";
                        dgvData.Columns[2].HeaderText = "Num.Ope.";
                        dgvData.Columns[3].HeaderText = "Fecha";
                        dgvData.Columns[4].HeaderText = "Cod. Mov.";
                        dgvData.Columns[5].HeaderText = "Doc.Ref.";
                        dgvData.Columns[6].HeaderText = "Nro.Doc Ref.";
                        dgvData.Columns[7].HeaderText = "Proveedor";
                        dgvData.Columns[8].HeaderText = "Glosa";
                        dgvData.Columns[9].HeaderText = "E.";

                        dgvData.Columns[0].Width = 50;
                        dgvData.Columns[1].Width = 40;
                        dgvData.Columns[2].Width = 80;
                        dgvData.Columns[3].Width = 75;
                        dgvData.Columns[4].Width = 50;
                        dgvData.Columns[5].Width = 50;
                        dgvData.Columns[6].Width = 100;
                        dgvData.Columns[7].Width = 250;
                        dgvData.Columns[8].Width = 250;
                        dgvData.Columns[9].Width = 40;

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

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            Int32 selectedCellCount =
            dgvData.GetCellCount(DataGridViewElementStates.Selected);
            if (selectedCellCount > 0)
            {
                string codcia = "01";// this._codcia;
                string alma = dgvData.Rows[dgvData.SelectedCells[1].RowIndex].Cells[0].Value.ToString();
                string td = dgvData.Rows[dgvData.SelectedCells[1].RowIndex].Cells[2].Value.ToString();
                string numdoc = dgvData.Rows[dgvData.SelectedCells[1].RowIndex].Cells[3].Value.ToString();
                ui_autentica ui_autentica = new ui_autentica();
                ui_autentica._tipousr = "M";
                ui_autentica.Activate();
                ui_autentica.BringToFront();
                ui_autentica.ShowDialog();
                ui_autentica.Dispose();
                GlobalVariables gv = new GlobalVariables();
                string autentica = gv.getAutentica();
                if (autentica.Equals("S"))
                {
                    DialogResult resultado = MessageBox.Show("¿Desea eliminar el movimiento " + @td + "/" + @numdoc + "?",
                "Consulta Importante",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);
                    if (resultado == DialogResult.Yes)
                    {
                        DataTable dt = new DataTable();
                        dt = ui_ListaEnlazados(codcia, alma, td, numdoc);
                        if (dt.Rows.Count > 0)
                        {
                            MessageBox.Show("Existen movimientos enlazados al Movimiento, no se puede eliminar", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                            ui_movligados ui_movligados = new ui_movligados();
                            ui_movligados._codcia = codcia;
                            ui_movligados._alma = alma;
                            ui_movligados._desalma = cmbComedor.Text;
                            ui_movligados._td = td;
                            ui_movligados._numdoc = numdoc;
                            ui_movligados._item = string.Empty;
                            ui_movligados._reporte = "C";
                            ui_movligados.Activate();
                            ui_movligados.BringToFront();
                            ui_movligados.ShowDialog();
                            ui_movligados.Dispose();
                        }
                        else
                        {
                            AlmovC almovc = new AlmovC();
                            almovc.delAlmovC(codcia, alma, td, numdoc);

                            Alstock alstock = new Alstock();
                            alstock.recalcularStockAlmacen(codcia, alma);

                            ui_ListaAlmov();
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("No ha seleccionado registro a Eliminar", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void toolstripform_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            Int32 selectedCellCount = dgvData.GetCellCount(DataGridViewElementStates.Selected);
            if (selectedCellCount > 0)
            {
                string codcia = this._codcia;
                string alma = dgvData.Rows[dgvData.SelectedCells[1].RowIndex].Cells[0].Value.ToString();
                string td = dgvData.Rows[dgvData.SelectedCells[1].RowIndex].Cells[1].Value.ToString();
                string numdoc = dgvData.Rows[dgvData.SelectedCells[1].RowIndex].Cells[2].Value.ToString();
                string situa = dgvData.Rows[dgvData.SelectedCells[1].RowIndex].Cells[9].Value.ToString();
                string mensaje = string.Empty;

                if (situa.Equals("F"))
                {
                    AlmovC almovc = new AlmovC();
                    string strPrinter = almovc.printAlmov(codcia, alma, td, numdoc);
                    ui_reportetxt ui_reportetxt = new ui_reportetxt();
                    ui_reportetxt._texto = strPrinter;
                    ui_reportetxt.Activate();
                    ui_reportetxt.BringToFront();
                    ui_reportetxt.ShowDialog();
                    ui_reportetxt.Dispose();
                }
                else
                {
                    if (situa.Equals("V"))
                    {
                        mensaje = "No se puede imprimir Parte no Finalizado";

                    }
                    else
                    {
                        mensaje = "No se puede imprimir Parte Anulado";
                    }

                    MessageBox.Show(mensaje, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("No ha seleccionado registro a Imprimir", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void dgvData_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if ((this.dgvData.Rows[e.RowIndex].Cells["situa"].Value).ToString().Equals("V"))
            {
                foreach (DataGridViewCell celda in

                this.dgvData.Rows[e.RowIndex].Cells)
                {

                    celda.Style.BackColor = Color.Yellow;
                    celda.Style.ForeColor = Color.Black;

                }
            }
            else
            {
                if ((this.dgvData.Rows[e.RowIndex].Cells["situa"].Value).ToString().Equals("A"))
                {
                    foreach (DataGridViewCell celda in

                    this.dgvData.Rows[e.RowIndex].Cells)
                    {

                        celda.Style.BackColor = Color.Red;
                        celda.Style.ForeColor = Color.White;

                    }
                }
            }

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void btnDesFina_Click(object sender, EventArgs e)
        {
            Int32 selectedCellCount =
          dgvData.GetCellCount(DataGridViewElementStates.Selected);
            if (selectedCellCount > 0)
            {
                GlobalVariables gv = new GlobalVariables();
                string codcia = this._codcia;
                string alma = dgvData.Rows[dgvData.SelectedCells[1].RowIndex].Cells[0].Value.ToString();
                string td = dgvData.Rows[dgvData.SelectedCells[1].RowIndex].Cells[2].Value.ToString();
                string numdoc = dgvData.Rows[dgvData.SelectedCells[1].RowIndex].Cells[3].Value.ToString();
                string fmod = DateTime.Now.ToString("yyyy-MM-dd");
                string usuario = gv.getValorUsr();
                ui_autentica ui_autentica = new ui_autentica();
                ui_autentica._tipousr = "M";
                ui_autentica.Activate();
                ui_autentica.BringToFront();
                ui_autentica.ShowDialog();
                ui_autentica.Dispose();

                string autentica = gv.getAutentica();
                if (autentica.Equals("S"))
                {
                    DialogResult resultado = MessageBox.Show("¿Desea DESFINALIZAR el documento " + @td + "/" + @numdoc + "?",
           "Consulta Importante",
           MessageBoxButtons.YesNo,
           MessageBoxIcon.Question);
                    if (resultado == DialogResult.Yes)
                    {
                        AlmovC almovc = new AlmovC();
                        almovc.updDesFinMov(codcia, alma, td, numdoc, fmod, usuario);
                        ui_ListaAlmov();
                    }
                }

            }
            else
            {
                MessageBox.Show("No ha seleccionado registro a DESFINALIZAR", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void radioButtonNroDocRef_CheckedChanged(object sender, EventArgs e)
        {
            txtBuscar.Text = string.Empty;
            txtBuscar.Focus();
        }

        private void txtFechaIni_ValueChanged(object sender, EventArgs e)
        {
            ui_ListaAlmov();
            SendKeys.Send("{TAB}");
        }

        private void txtFechaFin_ValueChanged(object sender, EventArgs e)
        {
            ui_ListaAlmov();
            SendKeys.Send("{TAB}");
        }

        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            ui_ListaAlmov();
        }
    }
}