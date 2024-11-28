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
    public partial class ui_detmaesgen : Form
    {
        Funciones funciones = new Funciones();
        string cadenaConexion;

        private ui_upddetmaesgen form = null;

        private ui_upddetmaesgen FormInstance
        {
            get
            {
                if (form == null)
                {
                    form = new ui_upddetmaesgen();
                    form.Disposed += new EventHandler(form_Disposed);

                }

                return form;
            }
        }

        void form_Disposed(object sender, EventArgs e)
        {
            form = null;
        }

        private Form FormPadre;

        public Form _FormPadre
        {
            get { return FormPadre; }
            set { FormPadre = value; }
        }

        public ui_detmaesgen()
        {
            InitializeComponent();

            if (funciones.VersionAssembly()) Application.ExitThread();
        }

        private void ui_detmaesgen_Load(object sender, EventArgs e)
        {
            listaDetMaesGen();
        }

        internal void maestroActivo(string clavemaesgen, string desmaesgen)
        {
            this.Text = clavemaesgen + " - " + desmaesgen;
        }

        private void listaDetMaesGen()
        {
            string idmaesgen = this.Text.Substring(0, 3);
            SqlConnection conexion = new SqlConnection();
            this.cadenaConexion = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.ConnectionString = cadenaConexion;
            conexion.Open();
            string query = "select clavemaesgen,desmaesgen,abrevia,parm1maesgen,parm2maesgen,parm3maesgen,tipomaesgen,statemaesgen,idmaesgen from maesgen where idmaesgen='" + @idmaesgen + "' order by clavemaesgen asc;";

            try
            {
                using (SqlDataAdapter myDataAdapter = new SqlDataAdapter(query, conexion))
                {
                    DataSet myDataSet = new DataSet();
                    myDataAdapter.Fill(myDataSet, "tblDetMaesGen");
                    funciones.formatearDataGridView(dgvDetMaesGen);

                    dgvDetMaesGen.DataSource = myDataSet.Tables["tblDetMaesGen"];
                    dgvDetMaesGen.Columns[0].HeaderText = "Código";
                    dgvDetMaesGen.Columns[1].HeaderText = "Descripción";
                    dgvDetMaesGen.Columns[2].HeaderText = "Descripción Abreviada";
                    dgvDetMaesGen.Columns[3].HeaderText = "Parámetro 1";
                    dgvDetMaesGen.Columns[4].HeaderText = "Parámetro 2";
                    dgvDetMaesGen.Columns[5].HeaderText = "Parámetro 3";
                    dgvDetMaesGen.Columns[6].HeaderText = "Tipo";
                    dgvDetMaesGen.Columns[7].HeaderText = "Estado";
                    dgvDetMaesGen.Columns["idmaesgen"].Visible = false;
                    dgvDetMaesGen.Columns[0].Width = 50;
                    dgvDetMaesGen.Columns[1].Width = 320;
                    dgvDetMaesGen.Columns[2].Width = 220;
                    dgvDetMaesGen.Columns[3].Width = 100;
                    dgvDetMaesGen.Columns[4].Width = 100;
                    dgvDetMaesGen.Columns[5].Width = 100;
                    dgvDetMaesGen.Columns[6].Width = 40;
                    dgvDetMaesGen.Columns[7].Width = 50;

                    dgvDetMaesGen.AllowUserToResizeRows = false;
                    dgvDetMaesGen.AllowUserToResizeColumns = false;
                    foreach (DataGridViewColumn column in dgvDetMaesGen.Columns)
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
            Close();
            ((ui_maesgen)FormPadre).btnActualizar.PerformClick();
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            string maesgen = this.Text;
            ui_upddetmaesgen ui_detmaesgen = this.FormInstance;
            ui_detmaesgen._FormPadre = this;
            ui_detmaesgen.newDetMaesGen(maesgen);
            ui_detmaesgen.Activate();
            ui_detmaesgen.BringToFront();
            ui_detmaesgen.ShowDialog();
            ui_detmaesgen.Dispose();
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            listaDetMaesGen();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            string sidmaesgen;
            string sclavemaesgen;
            string sdesmaesgen;

            Int32 selectedCellCount =
            dgvDetMaesGen.GetCellCount(DataGridViewElementStates.Selected);
            if (selectedCellCount > Convert.ToInt32(0))
            {
                sidmaesgen = dgvDetMaesGen.Rows[dgvDetMaesGen.SelectedCells[1].RowIndex].Cells[8].Value.ToString();
                sclavemaesgen = dgvDetMaesGen.Rows[dgvDetMaesGen.SelectedCells[1].RowIndex].Cells[0].Value.ToString();
                sdesmaesgen = dgvDetMaesGen.Rows[dgvDetMaesGen.SelectedCells[1].RowIndex].Cells[1].Value.ToString();
                DialogResult resultado = MessageBox.Show("¿Desea eliminar el registro " + sclavemaesgen + " " + sdesmaesgen + "?",
                "Consulta Importante",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);
                if (resultado == DialogResult.Yes)
                {
                    MaesGen maesgen = new MaesGen();
                    maesgen.eliminarMaesGen(sidmaesgen, sclavemaesgen);
                    this.listaDetMaesGen();
                }
            }
            else
            {
                MessageBox.Show("No ha seleccionado registro a Eliminar", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }

        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            string maesgen = this.Text;

            Int32 selectedCellCount =
            dgvDetMaesGen.GetCellCount(DataGridViewElementStates.Selected);

            if (selectedCellCount > 0)
            {
                string idmaesgen = dgvDetMaesGen.Rows[dgvDetMaesGen.SelectedCells[1].RowIndex].Cells[8].Value.ToString();
                string clavemaesgen = dgvDetMaesGen.Rows[dgvDetMaesGen.SelectedCells[1].RowIndex].Cells[0].Value.ToString();
                string desmaesgen = dgvDetMaesGen.Rows[dgvDetMaesGen.SelectedCells[1].RowIndex].Cells[1].Value.ToString();
                string abrevia = dgvDetMaesGen.Rows[dgvDetMaesGen.SelectedCells[1].RowIndex].Cells[2].Value.ToString();
                string parm1maesgen = dgvDetMaesGen.Rows[dgvDetMaesGen.SelectedCells[1].RowIndex].Cells[3].Value.ToString();
                string parm2maesgen = dgvDetMaesGen.Rows[dgvDetMaesGen.SelectedCells[1].RowIndex].Cells[4].Value.ToString();
                string parm3maesgen = dgvDetMaesGen.Rows[dgvDetMaesGen.SelectedCells[1].RowIndex].Cells[5].Value.ToString();
                string tipomaesgen = dgvDetMaesGen.Rows[dgvDetMaesGen.SelectedCells[1].RowIndex].Cells[6].Value.ToString();
                string statemaesgen = dgvDetMaesGen.Rows[dgvDetMaesGen.SelectedCells[1].RowIndex].Cells[7].Value.ToString();

                ui_upddetmaesgen ui_upddetalle = this.FormInstance;
                ui_upddetalle._FormPadre = this;
                ui_upddetalle.loadDetMaesGen(maesgen, clavemaesgen, desmaesgen, abrevia, parm1maesgen, parm2maesgen, parm3maesgen, statemaesgen, tipomaesgen);
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

        private void toolstripform_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

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

        private void dgvDetMaesGen_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}