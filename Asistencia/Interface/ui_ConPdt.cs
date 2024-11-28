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
    public partial class ui_ConPdt : Form
    {
        public ui_ConPdt()
        {
            InitializeComponent();
        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void ui_ConPdt_Load(object sender, EventArgs e)
        {
            Funciones funciones = new Funciones();
            string squery = "SELECT rp_ccodigo as clave,rp_cdescri as descripcion FROM tgrpts where rp_cindice='TC' order by 1 asc;";
            funciones.listaToolStripComboBox(squery, cmbTipoConcepto);
            ui_ListaConceptos();
        }

        private void ui_ListaConceptos()
        {

            try
            {
                Funciones funciones = new Funciones();
                string tipconpdt = funciones.getValorToolStripComboBox(cmbTipoConcepto, 4);
                string query = " select idconpdt,desconpdt,tipconpdt,devengado,pagado,";
                query = query + " regcero,devenplame,pagaplame,regceroplame from conpdt ";
                query = query + " where tipconpdt='" + @tipconpdt + "' order by idconpdt asc";

                SqlConnection conexion = new SqlConnection();
                conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
                conexion.Open();

                try
                {
                    using (SqlDataAdapter myDataAdapter = new SqlDataAdapter(query, conexion))
                    {
                        DataSet myDataSet = new DataSet();
                        myDataAdapter.Fill(myDataSet, "tblConPdt");
                        funciones.formatearDataGridView(dgvdetalle);

                        dgvdetalle.DataSource = myDataSet.Tables["tblConPdt"];
                        dgvdetalle.Columns[0].HeaderText = "Código";
                        dgvdetalle.Columns[1].HeaderText = "Descripción";
                        dgvdetalle.Columns[2].HeaderText = "Tipo";
                        dgvdetalle.Columns[3].HeaderText = "Devengado PDT?";
                        dgvdetalle.Columns[4].HeaderText = "Pagado / Descontado PDT?";
                        dgvdetalle.Columns[5].HeaderText = "¿Se registra monto en Cero PDT?";
                        dgvdetalle.Columns[6].HeaderText = "Devengado PLAME?";
                        dgvdetalle.Columns[7].HeaderText = "Pagado / Descontado PLAME?";
                        dgvdetalle.Columns[8].HeaderText = "¿Se registra monto en Cero PLAME?";

                        dgvdetalle.Columns[0].Width = 80;
                        dgvdetalle.Columns[1].Width = 300;
                        dgvdetalle.Columns[2].Width = 60;
                        dgvdetalle.Columns[3].Width = 80;
                        dgvdetalle.Columns[4].Width = 80;
                        dgvdetalle.Columns[5].Width = 80;
                        dgvdetalle.Columns[6].Width = 80;
                        dgvdetalle.Columns[7].Width = 80;
                        dgvdetalle.Columns[8].Width = 80;


                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }

                conexion.Close();
            }
            catch (ArgumentOutOfRangeException)
            {

            }

        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            ui_ListaConceptos();
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            Funciones funciones = new Funciones();
            string tipconpdt = cmbTipoConcepto.Text.Substring(0, 4);
            ui_UpdConPdt ui_detalle = new ui_UpdConPdt();
            ui_detalle._FormPadre = this;
            ui_detalle.setValoresConPdt(tipconpdt);
            ui_detalle.newConPdt();
            ui_detalle.Activate();
            ui_detalle.BringToFront();
            ui_detalle.ShowDialog();
            ui_detalle.Dispose();
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            string idconpdt;
            string desconpdt;
            string tipconpdt;
            string devengado;
            string pagado;
            string regcero;

            Int32 selectedCellCount =
            dgvdetalle.GetCellCount(DataGridViewElementStates.Selected);
            if (selectedCellCount > Convert.ToInt32(0))
            {
                idconpdt = dgvdetalle.Rows[dgvdetalle.SelectedCells[1].RowIndex].Cells[0].Value.ToString();
                desconpdt = dgvdetalle.Rows[dgvdetalle.SelectedCells[1].RowIndex].Cells[1].Value.ToString();
                tipconpdt = dgvdetalle.Rows[dgvdetalle.SelectedCells[1].RowIndex].Cells[2].Value.ToString();
                devengado = dgvdetalle.Rows[dgvdetalle.SelectedCells[1].RowIndex].Cells[3].Value.ToString();
                pagado = dgvdetalle.Rows[dgvdetalle.SelectedCells[1].RowIndex].Cells[4].Value.ToString();
                regcero = dgvdetalle.Rows[dgvdetalle.SelectedCells[1].RowIndex].Cells[5].Value.ToString();

                ui_UpdConPdt ui_detalle = new ui_UpdConPdt();
                ui_detalle._FormPadre = this;
                ui_detalle.setValoresConPdt(tipconpdt);
                ui_detalle.loadConPdt(idconpdt, desconpdt, devengado, pagado, regcero);
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
            string idconpdt;
            string desconpdt;

            Int32 selectedCellCount =
            dgvdetalle.GetCellCount(DataGridViewElementStates.Selected);
            if (selectedCellCount > Convert.ToInt32(1))
            {
                idconpdt = dgvdetalle.Rows[dgvdetalle.SelectedCells[1].RowIndex].Cells[0].Value.ToString();
                desconpdt = dgvdetalle.Rows[dgvdetalle.SelectedCells[1].RowIndex].Cells[1].Value.ToString();

                DialogResult resultado = MessageBox.Show("¿Desea eliminar el Concepto PDT " + idconpdt + " - " + desconpdt + "?",
                "Consulta Importante",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);
                if (resultado == DialogResult.Yes)
                {
                    ConPdt conpdt = new ConPdt();
                    conpdt.eliminarConPdt(idconpdt);
                    this.ui_ListaConceptos();
                }

            }
            else
            {
                MessageBox.Show("No ha seleccionado registro a Eliminar", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void cmbTipoConcepto_SelectedIndexChanged(object sender, EventArgs e)
        {
            ui_ListaConceptos();
        }
    }
}