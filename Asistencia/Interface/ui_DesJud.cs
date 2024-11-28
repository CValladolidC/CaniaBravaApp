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
    public partial class ui_DesJud : Form
    {
        private ui_descjudicial form = null;

        private ui_descjudicial FormInstance
        {
            get
            {
                if (form == null)
                {
                    form = new ui_descjudicial();
                    form.Disposed += new EventHandler(form_Disposed);

                }

                return form;
            }
        }

        void form_Disposed(object sender, EventArgs e)
        {
            form = null;
        }

        public ui_DesJud()
        {
            InitializeComponent();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void ui_DesJud_Load(object sender, EventArgs e)
        {
            GlobalVariables variables = new GlobalVariables();
            string idcia = variables.getValorCia();
            ui_ListaDesJud(idcia);
        }

        private void ui_ListaDesJud(string idcia)
        {
            GlobalVariables variables = new GlobalVariables();
            Funciones funciones = new Funciones();

            string query = "select A.iddesjud,CONCAT(CONCAT(CONCAT(B.apepat,' '),CONCAT(B.apemat,' , ')),B.nombres) as nombre , A.fecemi,A.nrodocaut,C.desmaesgen as MotivoDescuento,A.feciniorden,A.fecfinorden,A.idcia,A.idperplan from desjud A left join perplan B on A.idperplan=B.idperplan and A.idcia=B.idcia left join maesgen C on A.motivo=C.clavemaesgen and C.idmaesgen='024' ";
            query = query + "where A.idcia='" + @idcia + "'" + " order by iddesjud asc;";

            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();

            try
            {
                using (SqlDataAdapter myDataAdapter = new SqlDataAdapter(query, conexion))
                {
                    DataSet myDataSet = new DataSet();
                    myDataAdapter.Fill(myDataSet, "tblDesJud");
                    funciones.formatearDataGridView(dgvdetalle);

                    dgvdetalle.DataSource = myDataSet.Tables["tblDesJud"];
                    dgvdetalle.Columns[0].HeaderText = "Cód.Int.";
                    dgvdetalle.Columns[1].HeaderText = "Trabajador";
                    dgvdetalle.Columns[2].HeaderText = "F.Emi.Orden";
                    dgvdetalle.Columns[3].HeaderText = "Nro.Doc.Orden";
                    dgvdetalle.Columns[4].HeaderText = "Motivo";
                    dgvdetalle.Columns[5].HeaderText = "F.Ini.Orden";
                    dgvdetalle.Columns[6].HeaderText = "F.Fin Orden";
                    dgvdetalle.Columns["idcia"].Visible = false;
                    dgvdetalle.Columns["idperplan"].Visible = false;
                    dgvdetalle.Columns[0].Width = 50;
                    dgvdetalle.Columns[1].Width = 220;
                    dgvdetalle.Columns[2].Width = 75;
                    dgvdetalle.Columns[3].Width = 120;
                    dgvdetalle.Columns[4].Width = 220;
                    dgvdetalle.Columns[5].Width = 75;
                    dgvdetalle.Columns[6].Width = 75;


                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }


            conexion.Close();
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            ui_descjudicial ui_detalle = this.FormInstance;
            ui_detalle._FormPadre = this;
            ui_detalle.ui_newDescuentoJudicial();
            ui_detalle.Activate();
            ui_detalle.BringToFront();
            ui_detalle.ShowDialog();
            ui_detalle.Dispose();
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            GlobalVariables variables = new GlobalVariables();
            string idcia = variables.getValorCia();
            ui_ListaDesJud(idcia);
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            string idcia;
            string iddesjud;

            Int32 selectedCellCount = dgvdetalle.GetCellCount(DataGridViewElementStates.Selected);
            if (selectedCellCount > Convert.ToInt32(0))
            {
                iddesjud = dgvdetalle.Rows[dgvdetalle.SelectedCells[1].RowIndex].Cells[0].Value.ToString();
                idcia = dgvdetalle.Rows[dgvdetalle.SelectedCells[1].RowIndex].Cells[7].Value.ToString();

                ui_descjudicial ui_detalle = this.FormInstance;
                ui_detalle._FormPadre = this;
                ui_detalle.Activate();
                ui_detalle.ui_ActualizaComboBox();
                ui_detalle.ui_loadDescuentoJudicial(iddesjud, idcia);
                ui_detalle.BringToFront();
                ui_detalle.ShowDialog();
                ui_detalle.Dispose();

            }
            else
            {
                MessageBox.Show("No ha seleccionado registro a Editar", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            Exporta exporta = new Exporta();
            exporta.Excel_FromDataGridView(dgvdetalle);
        }

        private void btnPdf_Click(object sender, EventArgs e)
        {
            if (dgvdetalle.RowCount > 0)
            {
                Exporta exporta = new Exporta();
                exporta.Pdf_FromDataGridView(dgvdetalle, 2);
            }
            else
            {
                MessageBox.Show("No se puede exportar a PDF", "Aviso Importante", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            string idcia;
            string iddesjud;

            Int32 selectedCellCount = dgvdetalle.GetCellCount(DataGridViewElementStates.Selected);
            if (selectedCellCount > Convert.ToInt32(0))
            {
                iddesjud = dgvdetalle.Rows[dgvdetalle.SelectedCells[1].RowIndex].Cells[0].Value.ToString();
                idcia = dgvdetalle.Rows[dgvdetalle.SelectedCells[1].RowIndex].Cells[7].Value.ToString();

                DialogResult resultado = MessageBox.Show("¿Desea eliminar el Descuento Judicial N° " + iddesjud + "?",
                "Consulta Importante",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);
                if (resultado == DialogResult.Yes)
                {
                    DescJud desjud = new DescJud();
                    desjud.eliminarDesJud(idcia, iddesjud);
                    this.ui_ListaDesJud(idcia);
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
    }
}