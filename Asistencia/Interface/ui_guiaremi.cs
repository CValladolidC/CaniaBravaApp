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
    public partial class ui_guiaremi : Form
    {
        string _codcia;
        string _descia;

        public ui_guiaremi()
        {
            InitializeComponent();
        }

        private void ui_guiaremi_Load(object sender, EventArgs e)
        {
            GlobalVariables gv = new GlobalVariables();
            Funciones funciones = new Funciones();
            CiaFile ciafile = new CiaFile();
            this._codcia = gv.getValorCia();
            string codcia = this._codcia;
            this._descia = ciafile.ui_getDatosCiaFile(codcia, "DESCRIPCION");
            string query = " Select codcaja as clave,descaja as descripcion ";
            query = query + " from cajas where codcia='" + @codcia + "' order by 1 asc; ";
            funciones.listaComboBox(query, cmbCaja, "");
            DateTime hoy = DateTime.Today;
            DateTime anterior = hoy.AddDays(-7);
            txtFechaIni.Text = anterior.ToString("dd/MM/yyyy");
            txtFechaFin.Text = DateTime.Now.ToString("dd/MM/yyyy");
            ui_ListaGuiasRemi();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ui_ListaGuiasRemi()
        {
            Funciones funciones = new Funciones();
            string codcia = this._codcia;
            string codcaja = funciones.getValorComboBox(cmbCaja, 4);
            string fechaini = txtFechaIni.Text;
            string fechafin = txtFechaFin.Text;
            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();
            string query = "  select A.alma,A.td,A.numdoc,A.rftguia,A.rfnguia,";
            query = query + " A.fecdoc,A.fectras,B.rucclie,B.desclie,C.desmaesgen as desmottras,A.albaran,";
            query = query + " A.situagr,A.rfserieguia,A.rfnroguia from almovc ";
            query = query + " A left join clie B on A.codclie=B.codclie ";
            query = query + " left join maesgen C on C.idmaesgen='210' and A.mottras=C.clavemaesgen ";
            query = query + " where A.codcia='" + @codcia + "' and A.codcaja='" + @codcaja + "' ";
            query = query + " and A.flag='GRCS' and A.fecguia >= ";
            query = query + " STR_TO_DATE('" + @fechaini + "', '%d/%m/%Y')  and  ";
            query = query + " A.fecguia <= STR_TO_DATE('" + @fechafin + "', '%d/%m/%Y') ";
            query = query + " order by A.numdoc asc ;";
            try
            {
                using (SqlDataAdapter myDataAdapter = new SqlDataAdapter(query, conexion))
                {
                    DataSet myDataSet = new DataSet();
                    myDataAdapter.Fill(myDataSet, "tblData");
                    funciones.formatearDataGridView(dgvData);
                    dgvData.DataSource = myDataSet.Tables["tblData"];
                    dgvData.Columns[0].HeaderText = "Almacén";
                    dgvData.Columns[1].HeaderText = "TD";
                    dgvData.Columns[2].HeaderText = "Mov.Almacén";
                    dgvData.Columns[3].HeaderText = "Tipo Doc.";
                    dgvData.Columns[4].HeaderText = "Nro.Doc.";
                    dgvData.Columns[5].HeaderText = "Fecha Doc.";
                    dgvData.Columns[6].HeaderText = "Fecha Inicio Traslado";
                    dgvData.Columns[7].HeaderText = "RUC Destinatario";
                    dgvData.Columns[8].HeaderText = "Razón Social";
                    dgvData.Columns[9].HeaderText = "Motivo Traslado";
                    dgvData.Columns[10].HeaderText = "Nro. Albaran";
                    dgvData.Columns[11].HeaderText = "E";

                    dgvData.Columns["rfserieguia"].Visible = false;
                    dgvData.Columns["rfnroguia"].Visible = false;

                    dgvData.Columns[0].Width = 50;
                    dgvData.Columns[1].Width = 40;
                    dgvData.Columns[2].Width = 80;
                    dgvData.Columns[3].Width = 50;
                    dgvData.Columns[4].Width = 100;
                    dgvData.Columns[5].Width = 75;
                    dgvData.Columns[6].Width = 75;
                    dgvData.Columns[7].Width = 100;
                    dgvData.Columns[8].Width = 150;
                    dgvData.Columns[9].Width = 120;
                    dgvData.Columns[10].Width = 80;
                    dgvData.Columns[11].Width = 30;

                    if (dgvData.Rows.Count > 7)
                    {
                        dgvData.CurrentCell = dgvData.Rows[dgvData.Rows.Count - 1].Cells[0];
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
            Funciones funciones = new Funciones();
            string numregencontrados = Convert.ToString(dgvData.RowCount);
            txtRegEncontrados.Text = funciones.replicateCadena(" ", 15 - numregencontrados.Trim().Length) + numregencontrados.Trim() + funciones.replicateCadena(" ", 20) + " De acuerdo al criterio de búsqueda";
        }



        private void btnNuevo_Click(object sender, EventArgs e)
        {
            Funciones funciones = new Funciones();
            string codcia = this._codcia;
            string descia = this._descia;
            string codcaja = funciones.getValorComboBox(cmbCaja, 4);
            string descaja = cmbCaja.Text;

            if (codcia != string.Empty)
            {
                if (codcaja != string.Empty)
                {
                    ui_updguiaremi ui_updguiaremi = new ui_updguiaremi();
                    ui_updguiaremi._FormPadre = this;
                    ui_updguiaremi.Activate();
                    ui_updguiaremi.setData(codcia, codcaja, descaja, descia);
                    ui_updguiaremi.BringToFront();
                    ui_updguiaremi.agregar();
                    ui_updguiaremi.ShowDialog();
                    ui_updguiaremi.Dispose();
                }
                else
                {
                    MessageBox.Show("No ha seleccionado Caja", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("No ha seleccionado Empresa de Trabajo", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            Exporta exporta = new Exporta();
            exporta.Excel_FromDataGridView(dgvData);
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            ui_ListaGuiasRemi();
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            Int32 selectedCellCount = dgvData.GetCellCount(DataGridViewElementStates.Selected);
            if (selectedCellCount > 0)
            {
                Funciones funciones = new Funciones();
                string alma = dgvData.Rows[dgvData.SelectedCells[1].RowIndex].Cells[0].Value.ToString();
                string td = dgvData.Rows[dgvData.SelectedCells[1].RowIndex].Cells[1].Value.ToString();
                string numdoc = dgvData.Rows[dgvData.SelectedCells[1].RowIndex].Cells[2].Value.ToString();
                string codcia = this._codcia;
                string descia = this._descia;
                string codcaja = funciones.getValorComboBox(cmbCaja, 4);
                string descaja = cmbCaja.Text;
                ui_updguiaremi ui_updguiaremi = new ui_updguiaremi();
                ui_updguiaremi._FormPadre = this;
                ui_updguiaremi.Activate();
                ui_updguiaremi.BringToFront();
                ui_updguiaremi.setData(codcia, codcaja, descaja, descia);
                ui_updguiaremi.ui_actualizaComboBox();
                ui_updguiaremi.editar(alma, td, numdoc);
                ui_updguiaremi.ShowDialog();
                ui_updguiaremi.Dispose();
            }
            else
            {
                MessageBox.Show("No ha seleccionado registro a Editar", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }


        private void txtFechaIni_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                if (UtiFechas.IsDate(txtFechaIni.Text))
                {
                    ui_ListaGuiasRemi();
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
                    ui_ListaGuiasRemi();
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
            ui_ListaGuiasRemi();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            string buscar = txtBuscar.Text.Trim();

            if (buscar != string.Empty)
            {
                Funciones funciones = new Funciones();
                string codcia = this._codcia;
                string codcaja = funciones.getValorComboBox(cmbCaja, 4);
                txtRegEncontrados.Text = string.Empty;
                string cadenaBusqueda = string.Empty;

                if (radioButtonNroCompro.Checked && txtBuscar.Text.Trim() != string.Empty)
                {
                    cadenaBusqueda = " A.rfndoc='" + @buscar + "' ";
                }
                else
                {
                    if (radioButtonRuc.Checked && txtBuscar.Text.Trim() != string.Empty)
                    {
                        cadenaBusqueda = " A.rucdesti='" + @buscar + "' ";

                    }
                    else
                    {
                        if (radioButtonRazon.Checked && txtBuscar.Text.Trim() != string.Empty)
                        {
                            cadenaBusqueda = " A.razondesti like '%" + @buscar + "%' ";
                        }
                        else
                        {
                            cadenaBusqueda = " A.numdoc='" + @buscar + "' ";
                        }
                    }

                }

                string query = "  select A.alma,A.td,A.numdoc,A.rftguia,A.rfnguia,";
                query = query + " A.fecdoc,A.fectras,B.rucclie,B.desclie,C.desmaesgen as desmottras,A.albaran,";
                query = query + " A.situagr,A.rfserieguia,A.rfnroguia from almovc ";
                query = query + " A left join clie B on A.codclie=B.codclie ";
                query = query + " left join maesgen C on C.idmaesgen='210' and A.mottras=C.clavemaesgen ";
                query = query + " where A.codcia='" + @codcia + "' and A.codcaja='" + @codcaja + "' ";
                query = query + " and A.flag='GRCS' and " + @cadenaBusqueda;
                query = query + " order by A.numdoc asc ;";

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
                        dgvData.Columns[1].HeaderText = "TD";
                        dgvData.Columns[2].HeaderText = "Mov.Almacén";
                        dgvData.Columns[3].HeaderText = "Tipo Doc.";
                        dgvData.Columns[4].HeaderText = "Nro.Doc.";
                        dgvData.Columns[5].HeaderText = "Fecha Doc.";
                        dgvData.Columns[6].HeaderText = "Fecha Inicio Traslado";
                        dgvData.Columns[7].HeaderText = "RUC Destinatario";
                        dgvData.Columns[8].HeaderText = "Razón Social";
                        dgvData.Columns[9].HeaderText = "Motivo Traslado";
                        dgvData.Columns[10].HeaderText = "Nro. Albaran";
                        dgvData.Columns[11].HeaderText = "E";

                        dgvData.Columns["rfserieguia"].Visible = false;
                        dgvData.Columns["rfnroguia"].Visible = false;

                        dgvData.Columns[0].Width = 50;
                        dgvData.Columns[1].Width = 40;
                        dgvData.Columns[2].Width = 80;
                        dgvData.Columns[3].Width = 50;
                        dgvData.Columns[4].Width = 100;
                        dgvData.Columns[5].Width = 75;
                        dgvData.Columns[6].Width = 75;
                        dgvData.Columns[7].Width = 100;
                        dgvData.Columns[8].Width = 150;
                        dgvData.Columns[9].Width = 120;
                        dgvData.Columns[10].Width = 80;
                        dgvData.Columns[10].Width = 30;

                        if (dgvData.Rows.Count > 7)
                        {
                            dgvData.CurrentCell = dgvData.Rows[dgvData.Rows.Count - 1].Cells[0];
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
        }



        private void btnEliminar_Click(object sender, EventArgs e)
        {
            Int32 selectedCellCount =
            dgvData.GetCellCount(DataGridViewElementStates.Selected);
            if (selectedCellCount > 0)
            {
                string codcia = this._codcia;
                string alma = dgvData.Rows[dgvData.SelectedCells[1].RowIndex].Cells[0].Value.ToString();
                string td = dgvData.Rows[dgvData.SelectedCells[1].RowIndex].Cells[1].Value.ToString();
                string numdoc = dgvData.Rows[dgvData.SelectedCells[1].RowIndex].Cells[2].Value.ToString();
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
                    DialogResult resultado = MessageBox.Show("¿Desea eliminar el documento " + @td + "/" + @numdoc + "?",
           "Consulta Importante",
           MessageBoxButtons.YesNo,
           MessageBoxIcon.Question);
                    if (resultado == DialogResult.Yes)
                    {
                        AlmovC almovc = new AlmovC();
                        almovc.delAlmovC(codcia, alma, td, numdoc);
                        ui_ListaGuiasRemi();
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

            if ((this.dgvData.Rows[e.RowIndex].Cells["situagr"].Value).ToString().Equals("V"))
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
                if ((this.dgvData.Rows[e.RowIndex].Cells["situagr"].Value).ToString().Equals("A"))
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

        private void dgvData_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        

        private void radioButtonNroCompro_CheckedChanged(object sender, EventArgs e)
        {
            txtBuscar.Text = string.Empty;
            txtBuscar.Focus();
        }

        private void radioButtonRuc_CheckedChanged(object sender, EventArgs e)
        {
            txtBuscar.Text = string.Empty;
            txtBuscar.Focus();
        }

        private void radioButtonRazon_CheckedChanged(object sender, EventArgs e)
        {
            txtBuscar.Text = string.Empty;
            txtBuscar.Focus();
        }

        private void radioButtonAlma_CheckedChanged(object sender, EventArgs e)
        {
            txtBuscar.Text = string.Empty;
            txtBuscar.Focus();
        }

        

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            Int32 selectedCellCount = dgvData.GetCellCount(DataGridViewElementStates.Selected);
            if (selectedCellCount > 0)
            {

                Funciones funciones = new Funciones();
                DataTable dtguia = new DataTable();
                string codcia = this._codcia;
                string rftdoc = dgvData.Rows[dgvData.SelectedCells[1].RowIndex].Cells[3].Value.ToString();
                string rfserie = dgvData.Rows[dgvData.SelectedCells[1].RowIndex].Cells[12].Value.ToString();
                string rfnro = dgvData.Rows[dgvData.SelectedCells[1].RowIndex].Cells[13].Value.ToString();
                GuiaRemi guiaremi = new GuiaRemi();
                dtguia = guiaremi.generaGuiasRemiWin(codcia, rftdoc, rfserie, rfnro, rfnro);
               
                if (dtguia.Rows.Count > 0)
                {
                    
                    ui_impresora ui_impresora = new ui_impresora();
                    ui_impresora.Activate();
                    ui_impresora.BringToFront();
                    ui_impresora.ShowDialog();
                    ui_impresora.Dispose();
                    cr.crGuiaRemi cr = new cr.crGuiaRemi();
                    GlobalVariables gv = new GlobalVariables();
                    PrintDialog pd = new PrintDialog();
                    cr.SetDataSource(dtguia);
                    cr.PrintOptions.PrinterName = gv.getPrinterName();
                    cr.PrintToPrinter(1, true, 0, 0);

                }
                else
                {
                    MessageBox.Show("No existe información registrada en el criterio seleccionado", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }


            }
            else
            {
                MessageBox.Show("No ha seleccionado registro a imprimir", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void cmbCaja_SelectedIndexChanged(object sender, EventArgs e)
        {
            ui_ListaGuiasRemi();
        }

        private void btnAnular_Click(object sender, EventArgs e)
        {
            Int32 selectedCellCount =
            dgvData.GetCellCount(DataGridViewElementStates.Selected);
            if (selectedCellCount > 0)
            {
                string codcia = this._codcia;
                string alma = dgvData.Rows[dgvData.SelectedCells[1].RowIndex].Cells[0].Value.ToString();
                string td = dgvData.Rows[dgvData.SelectedCells[1].RowIndex].Cells[1].Value.ToString();
                string numdoc = dgvData.Rows[dgvData.SelectedCells[1].RowIndex].Cells[2].Value.ToString();
                ui_autentica ui_autentica = new ui_autentica();
                ui_autentica._tipousr = "O";
                ui_autentica.Activate();
                ui_autentica.BringToFront();
                ui_autentica.ShowDialog();
                ui_autentica.Dispose();
                GlobalVariables gv = new GlobalVariables();
                string autentica = gv.getAutentica();
                if (autentica.Equals("S"))
                {
                    DialogResult resultado = MessageBox.Show("¿Desea anular el documento " + @td + "/" + @numdoc + "?",
           "Consulta Importante",
           MessageBoxButtons.YesNo,
           MessageBoxIcon.Question);
                    if (resultado == DialogResult.Yes)
                    {
                        AlmovC almovc = new AlmovC();
                        almovc.updAnuGuia(codcia,alma, td, numdoc);
                        ui_ListaGuiasRemi();
                    }
                }

            }
            else
            {
                MessageBox.Show("No ha seleccionado registro a Anular", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void dgvData_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            Int32 selectedCellCount =
           dgvData.GetCellCount(DataGridViewElementStates.Selected);
            if (selectedCellCount > 0)
            {
                GlobalVariables gv = new GlobalVariables();
                string codcia = this._codcia;
                string alma = dgvData.Rows[dgvData.SelectedCells[1].RowIndex].Cells[0].Value.ToString();
                string td = dgvData.Rows[dgvData.SelectedCells[1].RowIndex].Cells[1].Value.ToString();
                string numdoc = dgvData.Rows[dgvData.SelectedCells[1].RowIndex].Cells[2].Value.ToString();
                string fmod = DateTime.Now.ToString("dd/MM/yyyy");
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
                        almovc.updDesFinGuia(codcia,alma, td, numdoc, fmod, usuario);
                        ui_ListaGuiasRemi();
                    }
                }

            }
            else
            {
                MessageBox.Show("No ha seleccionado registro a DESFINALIZAR", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

    }
}
