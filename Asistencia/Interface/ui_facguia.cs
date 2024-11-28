using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Configuration;
using System.Data.SqlClient;
using System.Drawing.Printing;


namespace CaniaBrava
{
    public partial class ui_facguia : ui_form
    {
        string _codcia;

        public ui_facguia()
        {
            InitializeComponent();
        }

        private void ui_facguia_Load(object sender, EventArgs e)
        {
            Funciones funciones = new Funciones();
            GlobalVariables gv = new GlobalVariables();
            this._codcia = gv.getValorCia();
            string codcia = this._codcia;
            string query = " Select codcaja as clave,descaja as descripcion ";
            query = query + " from cajas where codcia='" + @codcia + "' order by 1 asc; ";
            funciones.listaComboBox(query, cmbCaja, "");
            DateTime hoy = DateTime.Today;
            DateTime anterior = hoy.AddDays(-7);
            txtFechaIni.Text = anterior.ToString("dd/MM/yyyy");
            txtFechaFin.Text = DateTime.Now.ToString("dd/MM/yyyy");
            cmbListar.Text = "X   TODAS";
            cmbFecha.Text = "G   DE LA GUÍA DE REMISION";
            ui_ListaGuiasRemi();
        }

        private void ui_ListaGuiasRemi()
        {
            Funciones funciones = new Funciones();
            string codcia = this._codcia;
            string codcaja = funciones.getValorComboBox(cmbCaja, 4);
            string fechaini = txtFechaIni.Text;
            string fechafin = txtFechaFin.Text;
            string listar = funciones.getValorComboBox(cmbListar, 1);
            string fechaTomada = funciones.getValorComboBox(cmbFecha, 1);
            string condicionListar;
            string condicionFechaTomada;

            if (listar.Equals("X"))
            {
                condicionListar = string.Empty;
            }
            else
            {
                if (listar.Equals("P"))
                {
                    condicionListar = " and A.situafac='V' ";
                }
                else
                {
                    condicionListar = " and A.situafac='F' ";
                }
            }

            if (fechaTomada.Equals("G"))
            {
                condicionFechaTomada = " and A.fecguia >= STR_TO_DATE('" + @fechaini + "', '%d/%m/%Y')  and  ";
                condicionFechaTomada = condicionFechaTomada + " A.fecguia <= STR_TO_DATE('" + @fechafin + "', '%d/%m/%Y') ";
            }
            else
            {
                condicionFechaTomada = " and A.fecdoc >= STR_TO_DATE('" + @fechaini + "', '%d/%m/%Y')  and  ";
                condicionFechaTomada = condicionFechaTomada + " A.fecdoc <= STR_TO_DATE('" + @fechafin + "', '%d/%m/%Y') ";
            }

            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();
            string query = "  select A.alma,A.td,A.numdoc,A.rftdoc,A.rfndoc,A.fecdoc, ";
            query = query + "  A.rftguia,A.rfnguia,A.fecguia,";
            query = query + " A.fectras,B.rucclie,B.desclie,";
            query = query + " A.situafac,A.rfserie,A.rfnro from almovc A left join clie B on A.codclie=B.codclie ";
            query = query + " where A.codcia='" + @codcia + "' and A.codcaja='" + @codcaja + "' ";
            query = query + " and A.flag='GRCS' and A.situagr='F'  "+condicionFechaTomada+condicionListar;
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
                    dgvData.Columns[6].HeaderText = "Tipo Doc. Guía";
                    dgvData.Columns[7].HeaderText = "Nro.Doc. Guía";
                    dgvData.Columns[8].HeaderText = "Fecha Guía";
                    dgvData.Columns[9].HeaderText = "Fecha Inicio Traslado";
                    dgvData.Columns[10].HeaderText = "RUC Destinatario";
                    dgvData.Columns[11].HeaderText = "Razón Social";
                    dgvData.Columns[12].HeaderText = "E";

                    dgvData.Columns["rfserie"].Visible = false;
                    dgvData.Columns["rfnro"].Visible = false;

                    dgvData.Columns[0].Width = 50;
                    dgvData.Columns[1].Width = 40;
                    dgvData.Columns[2].Width = 80;
                    dgvData.Columns[3].Width = 50;
                    dgvData.Columns[4].Width = 100;
                    dgvData.Columns[5].Width = 75;
                    dgvData.Columns[6].Width = 50;
                    dgvData.Columns[7].Width = 100;
                    dgvData.Columns[8].Width = 75;
                    dgvData.Columns[9].Width = 75;
                    dgvData.Columns[10].Width = 120;
                    dgvData.Columns[11].Width = 250;
                    dgvData.Columns[12].Width = 50;

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

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

       

        private void cmbCaja_SelectedIndexChanged(object sender, EventArgs e)
        {
            ui_ListaGuiasRemi();
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

        private void btnEditar_Click(object sender, EventArgs e)
        {
            Int32 selectedCellCount = dgvData.GetCellCount(DataGridViewElementStates.Selected);
            if (selectedCellCount > 0)
            {
                Funciones funciones = new Funciones();
                CiaFile ciafile = new CiaFile();
                string codcia = this._codcia;
                string alma = dgvData.Rows[dgvData.SelectedCells[1].RowIndex].Cells[0].Value.ToString();
                string td = dgvData.Rows[dgvData.SelectedCells[1].RowIndex].Cells[1].Value.ToString();
                string numdoc = dgvData.Rows[dgvData.SelectedCells[1].RowIndex].Cells[2].Value.ToString();
                string descia = ciafile.ui_getDatosCiaFile(codcia, "DESCRIPCION");
                string codcaja = funciones.getValorComboBox(cmbCaja, 4);
                string descaja = cmbCaja.Text;
                ui_updfacguia ui_updfacguia = new ui_updfacguia();
                ui_updfacguia._FormPadre = this;
                ui_updfacguia.Activate();
                ui_updfacguia.BringToFront();
                ui_updfacguia.setData(codcia, codcaja, descaja, descia);
                ui_updfacguia.ui_actualizaComboBox();
                ui_updfacguia.editar(alma, td, numdoc);
                ui_updfacguia.ShowDialog();
                ui_updfacguia.Dispose();
            }
            else
            {
                MessageBox.Show("No ha seleccionado registro a Editar", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
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

                if (radioButtonNroGuia.Checked && txtBuscar.Text.Trim() != string.Empty)
                {
                    cadenaBusqueda = " and A.rfndoc='" + @buscar + "' ";
                }
                else
                {
                    if (radioButtonRuc.Checked && txtBuscar.Text.Trim() != string.Empty)
                    {
                        cadenaBusqueda = " and B.rucclie='" + @buscar + "' ";

                    }
                    else
                    {
                        if (radioButtonRazon.Checked && txtBuscar.Text.Trim() != string.Empty)
                        {
                            cadenaBusqueda = " and B.desclie like '%" + @buscar + "%' ";
                        }
                        else
                        {
                            cadenaBusqueda = " and A.numdoc='" + @buscar + "' ";
                        }
                    }

                }

                string query = "  select A.alma,A.td,A.numdoc,A.rftdoc,A.rfndoc,A.fecdoc, ";
                query = query + "  A.rftguia,A.rfnguia,A.fecguia,";
                query = query + " A.fectras,B.rucclie,B.desclie,";
                query = query + " A.situafac,A.rfserie,A.rfnro from almovc A left join clie B on A.codclie=B.codclie ";
                query = query + " where A.codcia='" + @codcia + "' and A.codcaja='" + @codcaja + "' ";
                query = query + " and A.flag='GRCS' and A.situagr='F'  "+cadenaBusqueda;
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
                        dgvData.Columns[6].HeaderText = "Tipo Doc. Guía";
                        dgvData.Columns[7].HeaderText = "Nro.Doc. Guía";
                        dgvData.Columns[8].HeaderText = "Fecha Guía";
                        dgvData.Columns[9].HeaderText = "Fecha Inicio Traslado";
                        dgvData.Columns[10].HeaderText = "RUC Destinatario";
                        dgvData.Columns[11].HeaderText = "Razón Social";
                        dgvData.Columns[12].HeaderText = "E";

                        dgvData.Columns["rfserie"].Visible = false;
                        dgvData.Columns["rfnro"].Visible = false;

                        dgvData.Columns[0].Width = 50;
                        dgvData.Columns[1].Width = 40;
                        dgvData.Columns[2].Width = 80;
                        dgvData.Columns[3].Width = 50;
                        dgvData.Columns[4].Width = 100;
                        dgvData.Columns[5].Width = 75;
                        dgvData.Columns[6].Width = 50;
                        dgvData.Columns[7].Width = 100;
                        dgvData.Columns[8].Width = 75;
                        dgvData.Columns[9].Width = 75;
                        dgvData.Columns[10].Width = 120;
                        dgvData.Columns[11].Width = 250;
                        dgvData.Columns[12].Width = 50;

                        
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

        private void dgvData_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dgvData_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if ((this.dgvData.Rows[e.RowIndex].Cells["situafac"].Value).ToString().Equals("V"))
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
                if ((this.dgvData.Rows[e.RowIndex].Cells["situafac"].Value).ToString().Equals("A"))
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

        private void cmbFecha_SelectedIndexChanged(object sender, EventArgs e)
        {
            ui_ListaGuiasRemi();
        }

        private void cmbListar_SelectedIndexChanged(object sender, EventArgs e)
        {
            ui_ListaGuiasRemi();
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            ui_ListaGuiasRemi();
        }

        private void radioButtonNroGuia_CheckedChanged(object sender, EventArgs e)
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

        private void radioButtonMov_CheckedChanged(object sender, EventArgs e)
        {
            txtBuscar.Text = string.Empty;
            txtBuscar.Focus();
        }

        private void btnTodos_Click(object sender, EventArgs e)
        {
            ui_ListaGuiasRemi();
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            Int32 selectedCellCount = dgvData.GetCellCount(DataGridViewElementStates.Selected);
            if (selectedCellCount > 0)
            {
                
                Funciones funciones = new Funciones();
                DataTable dtfactura = new DataTable();
                string codcia = this._codcia;
                string rftdoc = dgvData.Rows[dgvData.SelectedCells[1].RowIndex].Cells[3].Value.ToString();
                string rfserie = dgvData.Rows[dgvData.SelectedCells[1].RowIndex].Cells[13].Value.ToString();
                string rfnro = dgvData.Rows[dgvData.SelectedCells[1].RowIndex].Cells[14].Value.ToString();
  
                Factura factura = new Factura();
                dtfactura = factura.generaFacturasWin(codcia, rftdoc, rfserie, rfnro, rfnro);

               
                if (dtfactura.Rows.Count > 0)
                {
                    ui_impresora ui_impresora = new ui_impresora();
                    ui_impresora.Activate();
                    ui_impresora.BringToFront();
                    ui_impresora.ShowDialog();
                    ui_impresora.Dispose();
                    cr.crFactura cr = new cr.crFactura();
                    GlobalVariables gv = new GlobalVariables();
                    PrintDialog pd = new PrintDialog();
                    cr.SetDataSource(dtfactura);
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

        private void btnExcel_Click(object sender, EventArgs e)
        {
            Exporta exporta = new Exporta();
            exporta.Excel_FromDataGridView(dgvData);
        }

        private void btnDesFinaliza_Click(object sender, EventArgs e)
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
                        almovc.updDesFinFacGuia(codcia,alma, td, numdoc, fmod, usuario);
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
