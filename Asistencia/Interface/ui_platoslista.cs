using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using System.Data.SqlClient;


namespace CaniaBrava.Interface
{
    public partial class ui_platoslista : Form
    {
        public ui_platoslista()
        {
            InitializeComponent();
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            ui_platos ui_platos = new ui_platos();
            ui_platos._FormPadre2 = this;
            ui_platos.Activate();
            ui_platos.BringToFront();
            ui_platos.ShowDialog();
            ui_platos.Dispose();
        }
        
        private void ui_platoslista_Load(object sender, EventArgs e)
        {
            cmbcomedor.Items.Add("");
            cmbcomedor.Items.Add("MONTELIMA");
            cmbcomedor.Items.Add("LOBO");
            cmbcomedor.Items.Add("SAN VICENTE");
            cmbcomedor.SelectedIndex = 0;
            //ui_Listaplatos();
        }


        private void ui_Listaplatos()
        {
            Funciones funciones = new Funciones();
            string comedor = funciones.getValorComboBox(cmbcomedor, 11);
            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();

            string query = "SELECT * FROM Asistencia.dbo.platos WHERE comedor = '" + comedor + "'";
            //string query = "SELECT * FROM Asistencia.dbo.platos WHERE fecha >= GETDATE() - 7 AND comedor = '" + comedor + "'";

            try
            {
                using (SqlDataAdapter myDataAdapter = new SqlDataAdapter(query, conexion))
                {
                    DataSet myDataSet = new DataSet();
                    myDataAdapter.Fill(myDataSet, "tblData");
                    funciones.formatearDataGridView(dgvData);
                    dgvData.DataSource = myDataSet.Tables["tblData"];
                    dgvData.Columns[0].HeaderText = "ID";
                    dgvData.Columns[1].HeaderText = "COMEDOR";
                    dgvData.Columns[2].HeaderText = "FECHA";
                    dgvData.Columns[3].HeaderText = "HORA";
                    dgvData.Columns[4].HeaderText = "TIPO";
                    dgvData.Columns[5].HeaderText = "CANTIDAD";
                    dgvData.Columns[6].HeaderText = "PLATO";
                    dgvData.Columns[0].Width = 0;
                    dgvData.Columns[1].Width = 150;
                    dgvData.Columns[2].Width = 100;
                    dgvData.Columns[3].Width = 100;
                    dgvData.Columns[4].Width = 90;
                    dgvData.Columns[5].Width = 100;
                    dgvData.Columns[6].Width = 250;

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

        private void cmbcomedor_SelectedIndexChanged(object sender, EventArgs e)
        {
            ui_Listaplatos();
        }

        private void btnSalir_Click(object sender, EventArgs e) { Close(); }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            Int32 selectedCellCount = dgvData.GetCellCount(DataGridViewElementStates.Selected);
            //int selectedCellCount;
            //selectedCellCount = dgvData.CurrentRow.Index;
            if (selectedCellCount > 0)
            {
                //string id = dgvData.Rows[dgvData.SelectedCells[1].RowIndex].Cells[0].Value.ToString();
                //string comedor = dgvData.Rows[dgvData.SelectedCells[2].RowIndex].Cells[1].Value.ToString();
                //string fecha = dgvData.Rows[dgvData.SelectedCells[3].RowIndex].Cells[2].Value.ToString();
                //string tipo = dgvData.Rows[dgvData.SelectedCells[5].RowIndex].Cells[4].Value.ToString();
                //string cantidad = dgvData.Rows[dgvData.SelectedCells[6].RowIndex].Cells[5].Value.ToString();
                //string plato = dgvData.Rows[dgvData.SelectedCells[7].RowIndex].Cells[6].Value.ToString();

                //MessageBox.Show(id);
                //MessageBox.Show(comedor);
                //MessageBox.Show(fecha);
                //MessageBox.Show(tipo);
                //MessageBox.Show(cantidad);
                //MessageBox.Show(plato);
                //label2.Text = id;

                ui_platosupd ui_platosupd = new ui_platosupd();
                ui_platosupd._FormPadre3 = this;
                ui_platosupd.Activate();
                ui_platosupd.BringToFront();
                //ui_platosupd.ui_listarComboBox();
                //ui_platosupd.editar(id);
                ui_platosupd.ShowDialog();
                ui_platosupd.Dispose();

            }
            else
            {
                MessageBox.Show("No ha seleccionado registro a Editar", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void dgvData_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {

            //Dim i As Integer
        //    i = dtgAvisos.CurrentRow.Index
        //txtidregistro.Text = dtgAvisos.Item(1, i).Value()
        //cbmacrofundo.Text = dtgAvisos.Item(2, i).Value()
        //cbtipoevento.Text = dtgAvisos.Item(3, i).Value()
        //fregistro.Text = dtgAvisos.Item(4, i).Value()
        //cbequipo.Text = dtgAvisos.Item(5, i).Value()
        //cbturno.Text = dtgAvisos.Item(6, i).Value()
        //txtparcela.Text = dtgAvisos.Item(7, i).Value()
        //txttextofalla.Text = dtgAvisos.Item(8, i).Value()
        //cbstatusriego.Text = dtgAvisos.Item(9, i).Value()
        //cbstatustrabajo.Text = dtgAvisos.Item(10, i).Value()
        //txtoperador.Text = dtgAvisos.Item(11, i).Value()
        //cbresptrabajo.Text = dtgAvisos.Item(12, i).Value()
        //finicio.Text = dtgAvisos.Item(13, i).Value()
        //ffin.Text = dtgAvisos.Item(14, i).Value()
        //hinicio.Text = dtgAvisos.Item(15, i).Value()
        //hfin.Text = dtgAvisos.Item(16, i).Value()

        }

        public void Get_Informacion(string descrip, string mes, string anio, string iop, string val)
        {
            //ui_platosupd.txtcantidad.Text = descrip;
            //txtmes.Text = mes;
            //txtanio.Text = anio;
            //txtIOP.Text = iop;
            //txtvalor.Text = val;
        }

        public void dgvData_SelectionChanged(object sender, EventArgs e)
        {
            var rowsCount = dgvData.SelectedRows.Count;
            if (rowsCount == 0 || rowsCount > 1) return;

            var row = dgvData.SelectedRows[0];
            if (row == null) return;

            string descrip = row.Cells[0].Value.ToString();
            string mes = row.Cells[3].Value.ToString();
            string anio = row.Cells[4].Value.ToString();
            string iop = row.Cells[5].Value.ToString();
            string val = row.Cells[6].Value.ToString();
            Get_Informacion(descrip, mes, anio, iop, val);
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            SaveFileDialog fichero = new SaveFileDialog();
            fichero.Filter = "Excel (*.xls)|*.xls";
            if (fichero.ShowDialog() == DialogResult.OK)
            {
                Microsoft.Office.Interop.Excel.Application aplicacion;
                Microsoft.Office.Interop.Excel.Workbook libros_trabajo;
                Microsoft.Office.Interop.Excel.Worksheet hoja_trabajo;
                aplicacion = new Microsoft.Office.Interop.Excel.Application();
                libros_trabajo = aplicacion.Workbooks.Add();
                hoja_trabajo =
                    (Microsoft.Office.Interop.Excel.Worksheet)libros_trabajo.Worksheets.get_Item(1);
                //Recorremos el DataGridView rellenando la hoja de trabajo
                for (int i = 0; i < dgvData.Rows.Count; i++)
                {
                    for (int j = 0; j < dgvData.Columns.Count; j++)
                    {
                        hoja_trabajo.Cells[i + 1, j + 1] = dgvData.Rows[i].Cells[j].Value.ToString();
                    }
                }
                libros_trabajo.SaveAs(fichero.FileName,
                    Microsoft.Office.Interop.Excel.XlFileFormat.xlWorkbookNormal);
                libros_trabajo.Close(true);
                //aplicacion.Quit();
            }

        }
    }
}
