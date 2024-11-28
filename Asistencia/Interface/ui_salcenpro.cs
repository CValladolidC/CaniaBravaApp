using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Configuration;
using System.Data.SqlClient;

namespace CaniaBrava
{
    public partial class ui_salcenpro : ui_form
    {
        string _codcia;

        public ui_salcenpro()
        {
            InitializeComponent();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

       

        public void ui_calculaNumRegistros()
        {
            AlmovC almovc = new AlmovC();
            Funciones funciones = new Funciones();
            string codcia = this._codcia;
            string alma = funciones.getValorComboBox(cmbAlmacen, 2);
            txtRegTotal.Text = string.Empty;
            string numreg = almovc.getNumeroRegistros(codcia,alma);
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
            Funciones funciones = new Funciones();
            string codcia = this._codcia;
            string alma=funciones.getValorComboBox(cmbAlmacen,2);
            string fechaini = txtFechaIni.Text;
            string fechafin = txtFechaFin.Text;
            
            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();
            string query =  " select A.alma,A.td,A.numdoc,A.fecdoc,A.codmov,A.rftdoc,A.rfndoc,B.desprovee, ";
            query = query + " A.glosa1,A.situa from almovc A left join provee B on A.codpro = B.codprovee ";
            query = query + " where A.codcia='"+@codcia+"' and A.alma='" + @alma + "' and A.flag='PSCP' and A.fecdoc >= ";
            query = query + " STR_TO_DATE('" + @fechaini + "', '%d/%m/%Y')  and  ";
            query = query + " A.fecdoc <= STR_TO_DATE('" + @fechafin + "', '%d/%m/%Y') ";
            query = query + " order by A.fecdoc asc;";
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

                    if (dgvData.Rows.Count > 12)
                    {
                        dgvData.CurrentCell = dgvData.Rows[dgvData.Rows.Count - 1].Cells[0];
                    }

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

        private void cmbAlmacen_SelectedIndexChanged(object sender, EventArgs e)
        {
            ui_ListaAlmov();
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
           Funciones funciones=new Funciones();
           string codcia = this._codcia;
           string codalma = funciones.getValorComboBox(cmbAlmacen, 2);
           string desalma = cmbAlmacen.Text;
           if (codalma != string.Empty)
           {
               ui_updsalcenpro ui_updsalcenpro = new ui_updsalcenpro();
               ui_updsalcenpro._FormPadre = this;
               ui_updsalcenpro.Activate();
               ui_updsalcenpro.setData(codcia, codalma, desalma);
               ui_updsalcenpro.ui_ActualizaComboBox();
               ui_updsalcenpro.BringToFront();
               ui_updsalcenpro.agregar();
               ui_updsalcenpro.ShowDialog();
               ui_updsalcenpro.Dispose();
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
                string codcia = this._codcia;
                string desalma = cmbAlmacen.Text;
                string alma = dgvData.Rows[dgvData.SelectedCells[1].RowIndex].Cells[0].Value.ToString();
                string td = dgvData.Rows[dgvData.SelectedCells[1].RowIndex].Cells[1].Value.ToString();
                string numdoc = dgvData.Rows[dgvData.SelectedCells[1].RowIndex].Cells[2].Value.ToString();
                ui_updsalcenpro ui_updsalcenpro = new ui_updsalcenpro();
                ui_updsalcenpro._FormPadre = this;
                ui_updsalcenpro.Activate();
                ui_updsalcenpro.BringToFront();
                ui_updsalcenpro.setData(codcia, alma, desalma);
                ui_updsalcenpro.ui_ActualizaComboBox();
                ui_updsalcenpro.editar(codcia, alma, td, numdoc);
                ui_updsalcenpro.ShowDialog();
                ui_updsalcenpro.Dispose();
            }
            else
            {
                MessageBox.Show("No ha seleccionado registro a Editar", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void btnTodos_Click(object sender, EventArgs e)
        {
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
                Funciones funciones = new Funciones();
                string codcia = this._codcia;
                string alma = funciones.getValorComboBox(cmbAlmacen, 2);
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
                        cadenaBusqueda = " B.ruc='" + @buscar + "' ";
                    }

                }

                string query = " select A.alma,A.td,A.numdoc,A.fecdoc,A.codmov,A.rftdoc,A.rfndoc,B.desprovee, ";
                query = query + " A.glosa1,A.situa from almovc A left join provee B on A.codpro = B.codprovee ";
                query = query + " where A.codcia='"+@codcia+"' and A.alma='" + @alma + "' and A.flag='PSCP' and " + @cadenaBusqueda ;
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
                    DialogResult resultado = MessageBox.Show("¿Desea eliminar el movimiento " + @td + "/" + @numdoc + "?",
                "Consulta Importante",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);
                    if (resultado == DialogResult.Yes)
                    {
                        AlmovC almovc = new AlmovC();
                        almovc.delAlmovC(codcia,alma, td, numdoc);
                        ui_ListaAlmov();
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

       

        private void btnDesFina_Click(object sender, EventArgs e)
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

        private void ui_salcenpro_Load(object sender, EventArgs e)
        {
            GlobalVariables gv = new GlobalVariables();
            Funciones funciones = new Funciones();
            this._codcia = gv.getValorCia();
            string codcia = this._codcia;
            string usuario = gv.getValorUsr();
            string query = " Select A.codalma as clave,A.desalma as descripcion ";
            query = query + " from alalma A inner join almausr B on A.codcia=B.codcia and A.codalma=B.codalma ";
            query = query + " where A.codcia='" + @codcia + "' and A.estado='V' and B.idusr='" + @usuario + "' order by 1 asc; ";
            DateTime hoy = DateTime.Today;
            DateTime anterior = hoy.AddDays(-7);
            txtFechaIni.Text = anterior.ToString("dd/MM/yyyy");
            txtFechaFin.Text = DateTime.Now.ToString("dd/MM/yyyy");
            funciones.listaComboBox(query, cmbAlmacen, "");
            ui_ListaAlmov();
        }

       

    }
}
