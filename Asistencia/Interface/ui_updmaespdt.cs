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
    public partial class ui_updmaespdt : Form
    {
        string cadenaConexion;

        private Form FormPadre;

        public Form _FormPadre
        {
            get { return FormPadre; }
            set { FormPadre = value; }
        }

        public ui_updmaespdt()
        {
            InitializeComponent();
        }

        private void ui_updmaespdt_Load(object sender, EventArgs e)
        {
            listaDetMaesPdt();
        }

        internal void maestroActivo(string rp_ccodigo, string rp_cdescri)
        {
            this.Text = rp_ccodigo + " - " + rp_cdescri;

        }

        private void listaDetMaesPdt()
        {
            string rp_cindice = this.Text.Substring(0, 2);
            Funciones funciones = new Funciones();
            SqlConnection conexion = new SqlConnection();
            this.cadenaConexion = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.ConnectionString = cadenaConexion;
            conexion.Open();
            string query = "select rp_ccodigo,rp_cdescri,rp_cindice from tgrpts where rp_cindice='" + @rp_cindice+ "' order by rp_ccodigo asc;";

            try
            {
                using (SqlDataAdapter myDataAdapter = new SqlDataAdapter(query, conexion))
                {
                    DataSet myDataSet = new DataSet();
                    myDataAdapter.Fill(myDataSet, "tblDetMaesPdt");
                    funciones.formatearDataGridView(dgvDetMaesGen);

                    dgvDetMaesGen.DataSource = myDataSet.Tables["tblDetMaesPdt"];
                    dgvDetMaesGen.Columns[0].HeaderText = "Código";
                    dgvDetMaesGen.Columns[1].HeaderText = "Descripción";
                    dgvDetMaesGen.Columns["rp_cindice"].Visible = false;
                    dgvDetMaesGen.Columns[0].Width = 150;
                    dgvDetMaesGen.Columns[1].Width = 500;
                  
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
            Close();
            ((ui_maespdt)FormPadre).btnActualizar.PerformClick();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            string rp_cindice;
            string rp_ccodigo;
            string rp_cdescri;

            Int32 selectedCellCount =
            dgvDetMaesGen.GetCellCount(DataGridViewElementStates.Selected);
            if (selectedCellCount > Convert.ToInt32(0))
            {
                rp_ccodigo = dgvDetMaesGen.Rows[dgvDetMaesGen.SelectedCells[1].RowIndex].Cells[0].Value.ToString();
                rp_cdescri = dgvDetMaesGen.Rows[dgvDetMaesGen.SelectedCells[1].RowIndex].Cells[1].Value.ToString();
                rp_cindice = dgvDetMaesGen.Rows[dgvDetMaesGen.SelectedCells[1].RowIndex].Cells[2].Value.ToString();
                DialogResult resultado = MessageBox.Show("¿Desea eliminar el registro " + rp_ccodigo + " " + rp_cdescri + "?",
                "Consulta Importante",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);
                if (resultado == DialogResult.Yes)
                {
                    MaesPdt maespdt = new MaesPdt();
                    maespdt.eliminarMaesPdt(rp_cindice, rp_ccodigo);
                    this.listaDetMaesPdt();
                }

            }
            else
            {
                MessageBox.Show("No ha seleccionado registro a Eliminar", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }

        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            string rp_cindice;
            string rp_ccodigo;
            string rp_cdescri;
            string maespdt = this.Text;

            Int32 selectedCellCount =
            dgvDetMaesGen.GetCellCount(DataGridViewElementStates.Selected);
            if (selectedCellCount > Convert.ToInt32(0))
            {
                rp_ccodigo =  dgvDetMaesGen.Rows[dgvDetMaesGen.SelectedCells[1].RowIndex].Cells[0].Value.ToString();
                rp_cdescri = dgvDetMaesGen.Rows[dgvDetMaesGen.SelectedCells[1].RowIndex].Cells[1].Value.ToString();
                rp_cindice = dgvDetMaesGen.Rows[dgvDetMaesGen.SelectedCells[1].RowIndex].Cells[2].Value.ToString();
           
                
                ui_upddetmaespdt ui_upddetalle = new ui_upddetmaespdt();
                ui_upddetalle._FormPadre = this;
                ui_upddetalle.loadDetMaesGen(maespdt, rp_ccodigo, rp_cdescri, rp_cindice);
                ui_upddetalle.Activate();
                ui_upddetalle.BringToFront();
                ui_upddetalle.ShowDialog();
                ui_upddetalle.Dispose();

            }
            else
            {
                MessageBox.Show("No ha seleccionado registro a Editar", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            string maesgen = this.Text;
            ui_upddetmaespdt ui_upddetalle = new ui_upddetmaespdt();
            ui_upddetalle._FormPadre = this;
            ui_upddetalle.newDetMaesGen(maesgen);
            ui_upddetalle.Activate();
            ui_upddetalle.BringToFront();
            ui_upddetalle.ShowDialog();
            ui_upddetalle.Dispose();
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            Exporta exporta = new Exporta();
            exporta.Excel_FromDataGridView(dgvDetMaesGen);
        }

        private void btnPDF_Click(object sender, EventArgs e)
        {
            Exporta exporta = new Exporta();
            exporta.Pdf_FromDataGridView(dgvDetMaesGen, 1);
           
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            listaDetMaesPdt();
        }
    }
}