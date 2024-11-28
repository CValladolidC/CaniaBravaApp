using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Configuration;

namespace CaniaBrava
{
    public partial class ui_fonpen : Form
    {
        private string cadenaConexion;

        private ui_updfonpen form = null;

        private ui_updfonpen FormInstance
        {
            get
            {
                if (form == null)
                {
                    form = new ui_updfonpen();
                    form.Disposed += new EventHandler(form_Disposed);

                }

                return form;
            }
        }

        void form_Disposed(object sender, EventArgs e)
        {
            form = null;
        }

        public ui_fonpen()
        {
            InitializeComponent();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void ui_fonpen_Load(object sender, EventArgs e)
        {
            ui_ListaFonPen();

        }

        private void ui_ListaFonPen()
        {

            Funciones funciones = new Funciones();
            SqlConnection conexion = new SqlConnection();
            this.cadenaConexion = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.ConnectionString = cadenaConexion;
            conexion.Open();
            string query = "select idfonpen,desfonpen,codnet,psfonpen,cvfonpen,cffonpen,snpfonpen,statefonpen from fonpen order by idfonpen asc;";

            try
            {
                using (SqlDataAdapter myDataAdapter = new SqlDataAdapter(query, conexion))
                {
                    DataSet myDataSet = new DataSet();
                    myDataAdapter.Fill(myDataSet, "tblFonPen");
                    funciones.formatearDataGridView(dgvfonpen);
                    dgvfonpen.DataSource = myDataSet.Tables["tblFonPen"];
                    dgvfonpen.Columns[0].HeaderText = "Código";
                    dgvfonpen.Columns[1].HeaderText = "Fondo de Pensiones";
                    dgvfonpen.Columns[2].HeaderText = "Cód. AFP Net";
                    dgvfonpen.Columns[3].HeaderText = "PS";
                    dgvfonpen.Columns[4].HeaderText = "CV";
                    dgvfonpen.Columns[5].HeaderText = "CF";
                    dgvfonpen.Columns[6].HeaderText = "SNP";
                    dgvfonpen.Columns[7].HeaderText = "Estado";

                    dgvfonpen.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    dgvfonpen.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    dgvfonpen.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    dgvfonpen.Columns[6].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    dgvfonpen.Columns[3].DefaultCellStyle.Format = "###,###.##";
                    dgvfonpen.Columns[4].DefaultCellStyle.Format = "###,###.##";
                    dgvfonpen.Columns[5].DefaultCellStyle.Format = "###,###.##";
                    dgvfonpen.Columns[6].DefaultCellStyle.Format = "###,###.##";

                    dgvfonpen.Columns[0].Width = 50;
                    dgvfonpen.Columns[1].Width = 250;
                    dgvfonpen.Columns[2].Width = 50;
                    dgvfonpen.Columns[3].Width = 50;
                    dgvfonpen.Columns[4].Width = 50;
                    dgvfonpen.Columns[5].Width = 50;
                    dgvfonpen.Columns[6].Width = 50;
                    dgvfonpen.Columns[7].Width = 50;

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }



            conexion.Close();
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            ui_ListaFonPen();
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            ui_updfonpen ui_detalle = this.FormInstance;
            ui_detalle._FormPadre = this;
            ui_detalle.newFonPen();
            ui_detalle.Activate();
            ui_detalle.BringToFront();
            ui_detalle.ShowDialog();
            ui_detalle.Dispose();

        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            string idfonpen;
            string desfonpen;
            string codnet;
            float psfonpen;
            float cvfonpen;
            float cffonpen;
            float snpfonpen;
            string statefonpen;

            Int32 selectedCellCount =
            dgvfonpen.GetCellCount(DataGridViewElementStates.Selected);
            if (selectedCellCount > Convert.ToInt32(0))
            {
                idfonpen = dgvfonpen.Rows[dgvfonpen.SelectedCells[1].RowIndex].Cells[0].Value.ToString();
                desfonpen = dgvfonpen.Rows[dgvfonpen.SelectedCells[1].RowIndex].Cells[1].Value.ToString();
                codnet = dgvfonpen.Rows[dgvfonpen.SelectedCells[1].RowIndex].Cells[2].Value.ToString();
                psfonpen = float.Parse(dgvfonpen.Rows[dgvfonpen.SelectedCells[1].RowIndex].Cells[3].Value.ToString());
                cvfonpen = float.Parse(dgvfonpen.Rows[dgvfonpen.SelectedCells[1].RowIndex].Cells[4].Value.ToString());
                cffonpen = float.Parse(dgvfonpen.Rows[dgvfonpen.SelectedCells[1].RowIndex].Cells[5].Value.ToString());
                snpfonpen = float.Parse(dgvfonpen.Rows[dgvfonpen.SelectedCells[1].RowIndex].Cells[6].Value.ToString());
                statefonpen = dgvfonpen.Rows[dgvfonpen.SelectedCells[1].RowIndex].Cells[7].Value.ToString();

                ui_updfonpen ui_detalle = this.FormInstance;
                ui_detalle._FormPadre = this;
                ui_detalle.loadFonPen(idfonpen, desfonpen, psfonpen, cvfonpen, cffonpen, statefonpen, snpfonpen, codnet);
                ui_detalle.Activate();
                ui_detalle.BringToFront();
                ui_detalle.ShowDialog();
                ui_detalle.Dispose();


            }
            else
            {
                MessageBox.Show("No ha seleccionado registro a Editar", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            string sidfonpen;
            string sdesfonpen;

            Int32 selectedCellCount =
            dgvfonpen.GetCellCount(DataGridViewElementStates.Selected);
            if (selectedCellCount > Convert.ToInt32(1))
            {
                sidfonpen = dgvfonpen.Rows[dgvfonpen.SelectedCells[1].RowIndex].Cells[0].Value.ToString();
                sdesfonpen = dgvfonpen.Rows[dgvfonpen.SelectedCells[1].RowIndex].Cells[1].Value.ToString();
                DialogResult resultado = MessageBox.Show("¿Desea eliminar el Fondo de Pensiones " + sdesfonpen + "?",
                "Consulta Importante",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);
                if (resultado == DialogResult.Yes)
                {
                    FonPen fonpen = new FonPen();
                    fonpen.eliminarFonPen(sidfonpen);
                    this.ui_ListaFonPen();
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

        private void btnExcel_Click(object sender, EventArgs e)
        {
            Exporta exporta = new Exporta();
            exporta.Excel_FromDataGridView(dgvfonpen);

        }

        private void btnPDF_Click(object sender, EventArgs e)
        {


        }

        private void dgvfonpen_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}