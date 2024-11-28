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
    public partial class ui_factura : Form
    {
        string _codcia;
        string _descia;
    
        public ui_factura()
        {
            InitializeComponent();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ui_ListaFactura()
        {
            Funciones funciones = new Funciones();
            string codcia = this._codcia;
            string codcaja = funciones.getValorComboBox(cmbCaja, 4);
            string fechaini = txtFechaIni.Text;
            string fechafin = txtFechaFin.Text;
            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();
            string query = "  select A.alma,A.td,A.numdoc,A.rftdoc,A.rfndoc,";
            query = query + " A.fecdoc,B.rucclie,B.desclie,A.codmon,SUM(C.total) as total,";
            query = query + " A.situafac,A.rfserie,A.rfnro from almovc A ";
            query = query + " left join almovd C on A.codcia=C.codcia and A.alma=C.alma and A.td=C.td and A.numdoc=C.numdoc  ";
            query = query + " left join clie B on A.codclie=B.codclie  ";
            query = query + " where A.codcia='" + @codcia + "' and A.flag='FTCS' and A.codcaja='" + @codcaja + "' and A.fecdoc >= ";
            query = query + " STR_TO_DATE('" + @fechaini + "', '%d/%m/%Y')  and  ";
            query = query + " A.fecdoc <= STR_TO_DATE('" + @fechafin + "', '%d/%m/%Y') ";
            query = query + " group by A.alma,A.td,A.numdoc,A.rftdoc,A.rfndoc,A.fecdoc,B.rucclie,B.desclie,A.codmon ";
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
                    dgvData.Columns[6].HeaderText = "R.U.C.";
                    dgvData.Columns[7].HeaderText = "Razón Social";
                    dgvData.Columns[8].HeaderText = "Moneda";
                    dgvData.Columns[9].HeaderText = "Importe Total";
                    dgvData.Columns[10].HeaderText = "E";

                    dgvData.Columns["rfserie"].Visible = false;
                    dgvData.Columns["rfnro"].Visible = false;

                    dgvData.Columns[0].Width = 50;
                    dgvData.Columns[1].Width = 40;
                    dgvData.Columns[2].Width = 80;
                    dgvData.Columns[3].Width = 50;
                    dgvData.Columns[4].Width = 100;
                    dgvData.Columns[5].Width = 75;
                    dgvData.Columns[6].Width = 100;
                    dgvData.Columns[7].Width = 240;
                    dgvData.Columns[8].Width = 50;
                    dgvData.Columns[9].Width = 75;
                    dgvData.Columns[10].Width = 50;

                    if (dgvData.Rows.Count > 7)
                    {
                        dgvData.CurrentCell = dgvData.Rows[dgvData.Rows.Count - 1].Cells[0];
                    }
                    
                    dgvData.Columns[9].DefaultCellStyle.Format = "###,###.##";
                   

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
            AlmovC almovc = new AlmovC();
            Funciones funciones = new Funciones();
            string codcia = this._codcia;
            string codcaja = funciones.getValorComboBox(cmbCaja, 4);
            txtRegTotal.Text = string.Empty;
            string numreg = almovc.getNumeroFacturas(codcia, codcaja);
            txtRegTotal.Text = funciones.replicateCadena(" ", 15 - numreg.Trim().Length) + numreg.Trim() + funciones.replicateCadena(" ", 20) + "Registrados";
            string numregencontrados = Convert.ToString(dgvData.RowCount);
            txtRegEncontrados.Text = funciones.replicateCadena(" ", 15 - numregencontrados.Trim().Length) + numregencontrados.Trim() + funciones.replicateCadena(" ", 20) + " De acuerdo al criterio de búsqueda";
        }

        

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            Funciones funciones = new Funciones();
            CiaFile ciafile = new CiaFile();
            string codcia = this._codcia;
            string descia = ciafile.ui_getDatosCiaFile(codcia, "DESCRIPCION");
            string codcaja = funciones.getValorComboBox(cmbCaja, 4);
            string descaja = cmbCaja.Text;
            
            if (codcia != string.Empty)
            {
                if (codcaja != string.Empty)
                {
                    ui_updfactura ui_updfactura = new ui_updfactura();
                    ui_updfactura._FormPadre = this;
                    ui_updfactura.Activate();
                    ui_updfactura.setData(codcia, codcaja, descaja, descia);
                    ui_updfactura.BringToFront();
                    ui_updfactura.agregar();
                    ui_updfactura.ShowDialog();
                    ui_updfactura.Dispose();
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
            ui_ListaFactura();
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            Int32 selectedCellCount = dgvData.GetCellCount(DataGridViewElementStates.Selected);
            if (selectedCellCount > 0)
            {
                Funciones funciones=new Funciones();
                string codcia = this._codcia;
                string descia = this._descia;
                string alma = dgvData.Rows[dgvData.SelectedCells[1].RowIndex].Cells[0].Value.ToString();
                string td = dgvData.Rows[dgvData.SelectedCells[1].RowIndex].Cells[1].Value.ToString();
                string numdoc = dgvData.Rows[dgvData.SelectedCells[1].RowIndex].Cells[2].Value.ToString();
                string codcaja = funciones.getValorComboBox(cmbCaja, 4);
                string descaja = cmbCaja.Text;
                ui_updfactura ui_updfactura = new ui_updfactura();
                ui_updfactura._FormPadre = this;
                ui_updfactura.Activate();
                ui_updfactura.BringToFront();
                ui_updfactura.setData(codcia, codcaja, descaja, descia);
                ui_updfactura.ui_actualizaComboBox();
                ui_updfactura.editar(alma, td, numdoc);
                ui_updfactura.ShowDialog();
                ui_updfactura.Dispose();
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
                    ui_ListaFactura();
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
                    ui_ListaFactura();
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
            ui_ListaFactura();
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
                        cadenaBusqueda = " B.rucclie='" + @buscar + "' ";

                    }
                    else
                    {
                        if (radioButtonRazon.Checked && txtBuscar.Text.Trim() != string.Empty)
                        {
                            cadenaBusqueda = " B.desclie like '%" + @buscar + "%' ";
                        }
                        else
                        {
                            cadenaBusqueda = " A.numdoc='" + @buscar + "' ";
                        }
                    }

                }

                string query = "  select A.alma,A.td,A.numdoc,A.rftdoc,A.rfndoc,";
                query = query + " A.fecdoc,B.rucclie,B.desclie,A.codmon,SUM(C.total) as total, ";
                query = query + " A.situafac,A.rfserie,A.rfnro from almovc A ";
                query = query + " left join almovd C on A.codcia=C.codcia and A.alma=C.alma and A.td=C.td and A.numdoc=C.numdoc  ";
                query = query + " left join clie B on A.codclie=B.codclie  ";
                query = query + " where A.codcia='" + @codcia + "' and A.flag='FTCS' and A.codcaja='" + @codcaja + "' ";
                query = query + " and " + @cadenaBusqueda;
                query = query + " group by A.alma,A.td,A.numdoc,A.rftdoc,A.rfndoc,A.fecdoc,B.rucclie,B.desclie,A.codmon ";
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
                        dgvData.Columns[6].HeaderText = "R.U.C.";
                        dgvData.Columns[7].HeaderText = "Razón Social";
                        dgvData.Columns[8].HeaderText = "Moneda";
                        dgvData.Columns[9].HeaderText = "Importe Total";
                        dgvData.Columns[10].HeaderText = "E";

                        dgvData.Columns["rfserie"].Visible = false;
                        dgvData.Columns["rfnro"].Visible = false;

                        dgvData.Columns[0].Width = 50;
                        dgvData.Columns[1].Width = 40;
                        dgvData.Columns[2].Width = 80;
                        dgvData.Columns[3].Width = 50;
                        dgvData.Columns[4].Width = 100;
                        dgvData.Columns[5].Width = 75;
                        dgvData.Columns[6].Width = 100;
                        dgvData.Columns[7].Width = 240;
                        dgvData.Columns[8].Width = 50;
                        dgvData.Columns[9].Width = 75;
                        dgvData.Columns[10].Width = 50;

                        if (dgvData.Rows.Count > 7)
                        {
                            dgvData.CurrentCell = dgvData.Rows[dgvData.Rows.Count - 1].Cells[0];
                        }

                        dgvData.Columns[9].DefaultCellStyle.Format = "###,###.##";
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
                        almovc.delAlmovC(codcia,alma, td, numdoc);
                        ui_ListaFactura();
                    }
                }

            }
            else
            {
                MessageBox.Show("No ha seleccionado registro a Eliminar", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }


        }

       

        private void dgvData_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        private void ui_factura_Load(object sender, EventArgs e)
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
            ui_ListaFactura();
        }

        

        private void cmbCaja_SelectedIndexChanged(object sender, EventArgs e)
        {
            ui_ListaFactura();
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
                cr.crFactura cr = new cr.crFactura();
                Funciones funciones = new Funciones();
                DataTable dtfactura = new DataTable();
                string codcia = this._codcia;
                string rftdoc = dgvData.Rows[dgvData.SelectedCells[1].RowIndex].Cells[3].Value.ToString();
                string rfserie = dgvData.Rows[dgvData.SelectedCells[1].RowIndex].Cells[11].Value.ToString();
                string rfnro = dgvData.Rows[dgvData.SelectedCells[1].RowIndex].Cells[12].Value.ToString();
                Factura factura = new Factura();
                dtfactura=factura.generaFacturasWin(codcia, rftdoc, rfserie, rfnro, rfnro);
                if (dtfactura.Rows.Count > 0)
                {
                    ui_reporte ui_reporte = new ui_reporte();
                    ui_reporte.asignaDataTable(cr, dtfactura);
                    ui_reporte.Activate();
                    ui_reporte.BringToFront();
                    ui_reporte.ShowDialog();
                    ui_reporte.Dispose();
           
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

        private void groupBox1_Enter(object sender, EventArgs e)
        {

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
                string situafac = dgvData.Rows[dgvData.SelectedCells[1].RowIndex].Cells[10].Value.ToString();
                if (situafac != "A")
                {
                    ui_autentica ui_autentica = new ui_autentica();
                    ui_autentica._tipousr = "O";
                    ui_autentica.Activate();
                    ui_autentica.BringToFront();
                    ui_autentica.ShowDialog();
                    ui_autentica.Dispose();
                    GlobalVariables gv = new GlobalVariables();
                    string autentica=gv.getAutentica();
                    if (autentica.Equals("S"))
                    {
                        DialogResult resultado = MessageBox.Show("¿Desea anular el documento " + @td + "/" + @numdoc + "?",
                        "Consulta Importante",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question);
                        if (resultado == DialogResult.Yes)
                        {
                            AlmovC almovc = new AlmovC();
                            almovc.updAnuFactura(codcia,alma, td, numdoc);
                            ui_ListaFactura();
                        }
                    }
                }
                else
                {
                    MessageBox.Show("El registro ya se encuentra Anulado", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("No ha seleccionado registro a Anular", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }


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

        private void toolstripform_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void btnDesfinaliza_Click(object sender, EventArgs e)
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
                        almovc.updDesFinFacGuia(codcia, alma, td, numdoc, fmod, usuario);
                        ui_ListaFactura();
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
