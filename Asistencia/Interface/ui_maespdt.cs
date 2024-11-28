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
    public partial class ui_maespdt : Form
    {
        string cadenaConexion;

        public ui_maespdt()
        {
            InitializeComponent();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dgvMaesGen_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void ui_listaMaesPdt()
        {
            string rp_cindice = "00";
            Funciones funciones = new Funciones();
            SqlConnection conexion = new SqlConnection();
            this.cadenaConexion = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.ConnectionString = cadenaConexion;
            conexion.Open();
            string query = "select RP_CCODIGO,RP_CDESCRI,RP_CINDICE from TGRPTS where RP_CINDICE='" + @rp_cindice + "' order by RP_CINDICE asc;";



            try
            {
                using (SqlDataAdapter myDataAdapter = new SqlDataAdapter(query, conexion))
                {
                    DataSet myDataSet = new DataSet();
                    myDataAdapter.Fill(myDataSet, "tblMaesPdt");
                    funciones.formatearDataGridView(dgvMaesGen);

                    dgvMaesGen.DataSource = myDataSet.Tables["tblMaesPdt"];
                    dgvMaesGen.Columns[0].HeaderText = "Código";
                    dgvMaesGen.Columns[1].HeaderText = "Descripción";
                    dgvMaesGen.Columns["RP_CINDICE"].Visible = false;
                    dgvMaesGen.Columns[0].Width = 50;
                    dgvMaesGen.Columns[1].Width = 600;

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }

            conexion.Close();
        }

        private void ui_maespdt_Load(object sender, EventArgs e)
        {
            ui_listaMaesPdt();
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            GlobalVariables variables = new GlobalVariables();
            string rp_ccodigo;
            string rp_cdescri;

            Int32 selectedCellCount = dgvMaesGen.GetCellCount(DataGridViewElementStates.Selected);
            if (selectedCellCount > Convert.ToInt32(0))
            {

                rp_ccodigo = dgvMaesGen.Rows[dgvMaesGen.SelectedCells[1].RowIndex].Cells[0].Value.ToString();
                rp_cdescri = dgvMaesGen.Rows[dgvMaesGen.SelectedCells[1].RowIndex].Cells[1].Value.ToString();

                ui_updmaespdt ui_detalle = new ui_updmaespdt();
                ui_detalle._FormPadre = this;
                ui_detalle.maestroActivo(rp_ccodigo, rp_cdescri);
                ui_detalle.Activate();
                ui_detalle.BringToFront();
                ui_detalle.ShowDialog();
                ui_detalle.Dispose();


            }
            else
            {
                MessageBox.Show("No ha seleccionado Maestro a Editar", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            ui_listaMaesPdt();
        }

        private void toolStripForm_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }
    }
}