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
    public partial class ui_checklistneuma : Form
    {
        Funciones funciones = new Funciones();
        string _codcia;

        public ui_checklistneuma()
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

            DateTime hoy = DateTime.Today;
            DateTime anterior = hoy.AddDays(-7);
            txtFechaIni.Value = anterior;
            txtFechaFin.Value = DateTime.Now;

            string query = "SELECT RIGHT('0'+CAST(id AS VARCHAR),2) AS clave,tipo as descripcion FROM vehiculo (NOLOCK) ";
            funciones.listaComboBox(query, cmbTransporte, "X");

            ui_ListaSolAlma();
        }

        private void ui_ListaSolAlma()
        {
            string codcia = this._codcia;
            string transp = funciones.getValorComboBox(cmbTransporte, 2);
            string fechaini = txtFechaIni.Value.ToString("yyyy-MM-dd");
            string fechafin = txtFechaFin.Value.ToString("yyyy-MM-dd");

            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();
            string query = @" SELECT T.* FROM (
SELECT DISTINCT c.Tipo,b.Unidad,CONVERT(VARCHAR(10), a.fecha, 103) AS Fecha, CONVERT(VARCHAR(5), a.fecha, 8) AS Hora,  
a.valvulasintapa AS [Valvula sin Tapa],a.valvulainaccesible AS [Valvula inaccesible],a.valvulasinextension AS [Valvula sin extension],a.golpecortecostado AS [Golpe/Corte costado],
a.picadacortebanda AS [Picada/Corte Banda],a.dualmalhermanado [Dual mal hermanado],a.desgasteirregular AS [Desgaste irregular],a.corte [Corte banda central],a.exposicion [Exposicion de Alambres],a.sopladura [Sopladura Banda Rodamiento]
,a.Aeolus,a.bridgestone [Durun],a.firestone [Supercargo],a.Doublehappines,a.teckine [Long March],a.goodyear [Roadlux],a.Douprog,a.Golden,a.Maxzez,a.Compas,a.patas [Patas de Oso],a.Mixta,a.Nuevo,a.reenc [Reencauche],h.Nombre AS Empresa,
RTRIM(i.Nombre) + ' ' + RTRIM(i.ApellidoPaterno) + ' ' + RTRIM(i.ApellidoMaterno) AS Conductor,j.Descripcion AS Fundo,a.digital [Hub.Digital],a.viaj [Viaje],a.Movil,a.tecnic [Tecnico],a.transp [	Coord.Transporte],
a.obser AS [Observaciones],a.fecha as fechita 
FROM checklistneuma (NOLOCK) a
INNER JOIN vehiculodet (NOLOCK) b ON b.idDet = a.idDet
INNER JOIN vehiculo (NOLOCK) c ON c.id = b.id ";

            if (transp != "X")
            {
                query += "AND c.id = '" + int.Parse(transp == string.Empty ? "0" : transp) + @"'";
            }

            query += @"LEFT JOIN DB_CTC.dbo.empresa (NOLOCK) h ON h.IdEmpresa = a.empre
LEFT JOIN DB_CTC.dbo.chofer (NOLOCK) i ON i.IdChofer = a.conduc
LEFT JOIN DB_CTC.dbo.fundo (NOLOCK) j ON j.IdFundo = a.fund 
WHERE CONVERT(VARCHAR(10),a.fecha,120) BETWEEN '" + fechaini + @"' AND '" + fechafin + @"' 
) T
ORDER BY T.fechita DESC, T.hora DESC, T.tipo  ";

            try
            {
                using (SqlDataAdapter myDataAdapter = new SqlDataAdapter(query, conexion))
                {
                    DataSet myDataSet = new DataSet();
                    myDataAdapter.Fill(myDataSet, "tblData");

                    funciones.formatearDataGridView(dgvData);
                    dgvData.DataSource = myDataSet.Tables["tblData"];

                    dgvData.Columns[37].Visible = false;

                    dgvData.AllowUserToResizeRows = false;
                    dgvData.AllowUserToResizeColumns = false;
                    foreach (DataGridViewColumn column in dgvData.Columns)
                    {
                        column.SortMode = DataGridViewColumnSortMode.NotSortable;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            conexion.Close();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            string codcia = this._codcia;
            string alma = funciones.getValorComboBox(cmbTransporte, 2);
            string desalma = cmbTransporte.Text;
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
                string desalma = cmbTransporte.Text;
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
        //    if ((this.dgvData.Rows[e.RowIndex].Cells["estado"].Value).ToString().Equals("V"))
        //    {
        //        foreach (DataGridViewCell celda in

        //        this.dgvData.Rows[e.RowIndex].Cells)
        //        {
        //            celda.Style.BackColor = Color.BurlyWood;
        //            celda.Style.ForeColor = Color.Black;
        //        }
        //    }
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
            txtFechaFin.MinDate = txtFechaIni.Value;
            ui_ListaSolAlma();
            SendKeys.Send("{TAB}");
        }

        private void txtFechaFin_ValueChanged(object sender, EventArgs e)
        {
            ui_ListaSolAlma();
            SendKeys.Send("{TAB}");
        }

        private void btnExportar_Click(object sender, EventArgs e)
        {
            if (dgvData.Rows.Count > 0)
            {
                var data = ((DataTable)dgvData.DataSource).Copy();
                data.Columns.Remove("fechita");
                dataGridView1.DataSource = data;

                Exporta exporta = new Exporta();
                exporta.Excel_FromDataGridView(dataGridView1, true);
            }
            else
            {
                MessageBox.Show("No existe ningun registro a exportar.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}