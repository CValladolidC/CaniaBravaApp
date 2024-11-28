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
    public partial class ui_calplan : Form
    {
        private ui_updcalplan form = null;

        private ui_updcalplan FormInstance
        {
            get
            {
                if (form == null)
                {
                    form = new ui_updcalplan();
                    form.Disposed += new EventHandler(form_Disposed);

                }

                return form;
            }
        }

        void form_Disposed(object sender, EventArgs e)
        {
            form = null;
        }

        public ui_calplan()
        {
            InitializeComponent();
        }

        private void ui_calplan_Load(object sender, EventArgs e)
        {
            string squery;
            squery = "SELECT idtipoper as clave,destipoper as descripcion FROM tipoper order by 1 asc;";
            Funciones funciones = new Funciones();
            funciones.listaToolStripComboBox(squery, cmbTipoPer);
            squery = "SELECT idtipocal as clave,destipocal as descripcion FROM tipocal order by ordentipocal asc;";
            funciones.listaToolStripComboBox(squery, cmbTipoCal);

            ui_ListaCalPlan();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void dgvdetalle_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void ui_ListaCalPlan()
        {
            try
            {
                Funciones funciones = new Funciones();
                GlobalVariables variables = new GlobalVariables();
                string idcia = variables.getValorCia();
                string idtipoper = cmbTipoPer.Text.Substring(0, 1);
                string idtipocal = cmbTipoCal.Text.Substring(0, 1);

                SqlConnection conexion = new SqlConnection();
                conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
                conexion.Open();
                string query = " select A.messem,C.desmaesgen,A.anio,A.idtipocal,A.fechaini,A.fechafin, ";
                query = query + " A.idcia,A.idtipoper,CONCAT(CONCAT(A.idtipocal,'  '),B.destipocal) ";
                query = query + " as tipocal,A.mes,A.mespdt,A.aniopdt,A.diasdom,A.saldaquinta ";
                query = query + " from calplan A left join tipocal B on A.idtipocal=B.idtipocal left join maesgen C on ";
                query = query + " A.mes=C.clavemaesgen and C.idmaesgen='035' ";
                query = query + " where A.idcia='" + @idcia + "' and A.idtipoper='" + @idtipoper + "' ";
                query = query + " and A.idtipocal='" + @idtipocal + "'  order by A.anio desc,A.mes desc,A.messem desc ;";

                try
                {
                    using (SqlDataAdapter myDataAdapter = new SqlDataAdapter(query, conexion))
                    {
                        DataSet myDataSet = new DataSet();
                        myDataAdapter.Fill(myDataSet, "tblCalPlan");
                        funciones.formatearDataGridView(dgvdetalle);

                        dgvdetalle.DataSource = myDataSet.Tables["tblCalPlan"];
                        dgvdetalle.Columns[0].HeaderText = "Sem./Mes";
                        dgvdetalle.Columns[1].HeaderText = "Mes";
                        dgvdetalle.Columns[2].HeaderText = "Año";
                        dgvdetalle.Columns[3].HeaderText = "Tipo";
                        dgvdetalle.Columns[4].HeaderText = "Fecha Inicio";
                        dgvdetalle.Columns[5].HeaderText = "Fecha Fin";
                        dgvdetalle.Columns[10].HeaderText = "Mes PDT";
                        dgvdetalle.Columns[11].HeaderText = "Año PDT";
                        dgvdetalle.Columns[12].HeaderText = "Dias Dominicales";
                        dgvdetalle.Columns[13].HeaderText = "Salda 5ta.Categoría?";


                        dgvdetalle.Columns["idtipoper"].Visible = false;
                        dgvdetalle.Columns["idcia"].Visible = false;
                        dgvdetalle.Columns["tipocal"].Visible = false;
                        dgvdetalle.Columns["mes"].Visible = false;

                        dgvdetalle.Columns[0].Width = 80;
                        dgvdetalle.Columns[1].Width = 80;
                        dgvdetalle.Columns[2].Width = 50;
                        dgvdetalle.Columns[3].Width = 50;
                        dgvdetalle.Columns[4].Width = 100;
                        dgvdetalle.Columns[5].Width = 100;
                        dgvdetalle.Columns[10].Width = 50;
                        dgvdetalle.Columns[11].Width = 50;
                        dgvdetalle.Columns[12].Width = 70;

                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
                conexion.Close();
            }
            catch (Exception) { }


        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            string destipoper = cmbTipoPer.Text.Trim();
            string idtipoper = cmbTipoPer.Text.Substring(0, 1);
            string idtipocal = cmbTipoCal.Text.Substring(0, 1);
            ui_updcalplan ui_detalle = this.FormInstance;
            ui_detalle._FormPadre = this;
            ui_detalle.newCalPlan(destipoper, idtipoper, idtipocal);
            ui_detalle.Activate();
            ui_detalle.BringToFront();
            ui_detalle.ShowDialog();
            ui_detalle.Dispose();
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            string destipoper;
            string idtipoper;
            string idcia;
            string anio;
            string messem;
            string fechaini;
            string fechafin;
            string idtipocal;
            string tipocal;
            string mes;
            string mespdt;
            string aniopdt;
            string diasdom;
            string saldaquinta;


            Int32 selectedCellCount =
            dgvdetalle.GetCellCount(DataGridViewElementStates.Selected);
            if (selectedCellCount > Convert.ToInt32(0))
            {
                destipoper = cmbTipoPer.Text.Trim();
                messem = dgvdetalle.Rows[dgvdetalle.SelectedCells[1].RowIndex].Cells[0].Value.ToString();
                anio = dgvdetalle.Rows[dgvdetalle.SelectedCells[1].RowIndex].Cells[2].Value.ToString();
                idtipocal = dgvdetalle.Rows[dgvdetalle.SelectedCells[1].RowIndex].Cells[3].Value.ToString();
                fechaini = dgvdetalle.Rows[dgvdetalle.SelectedCells[1].RowIndex].Cells[4].Value.ToString();
                fechafin = dgvdetalle.Rows[dgvdetalle.SelectedCells[1].RowIndex].Cells[5].Value.ToString();
                idcia = dgvdetalle.Rows[dgvdetalle.SelectedCells[1].RowIndex].Cells[6].Value.ToString();
                idtipoper = dgvdetalle.Rows[dgvdetalle.SelectedCells[1].RowIndex].Cells[7].Value.ToString();
                tipocal = dgvdetalle.Rows[dgvdetalle.SelectedCells[1].RowIndex].Cells[8].Value.ToString();
                mes = dgvdetalle.Rows[dgvdetalle.SelectedCells[1].RowIndex].Cells[9].Value.ToString();
                mespdt = dgvdetalle.Rows[dgvdetalle.SelectedCells[1].RowIndex].Cells[10].Value.ToString();
                aniopdt = dgvdetalle.Rows[dgvdetalle.SelectedCells[1].RowIndex].Cells[11].Value.ToString();
                diasdom = dgvdetalle.Rows[dgvdetalle.SelectedCells[1].RowIndex].Cells[12].Value.ToString();
                saldaquinta = dgvdetalle.Rows[dgvdetalle.SelectedCells[1].RowIndex].Cells[13].Value.ToString();


                ui_updcalplan ui_detalle = this.FormInstance;
                ui_detalle._FormPadre = this;
                ui_detalle.Activate();
                ui_detalle.BringToFront();
                ui_detalle.loadCalPlan(destipoper, idtipoper, messem, mes, anio, fechaini, fechafin, idtipocal, mespdt, aniopdt, diasdom, saldaquinta);
                ui_detalle.ShowDialog();
                ui_detalle.Dispose();

            }
            else
            {
                MessageBox.Show("No ha seleccionado registro a Editar", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            ui_ListaCalPlan();
        }

        private void toolStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void cmbTipoPer_SelectedIndexChanged(object sender, EventArgs e)
        {
            ui_ListaCalPlan();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {

            string idtipoper;
            string idcia;
            string anio;
            string messem;
            string idtipocal;

            Int32 selectedCellCount =
            dgvdetalle.GetCellCount(DataGridViewElementStates.Selected);
            if (selectedCellCount > Convert.ToInt32(1))
            {

                messem = dgvdetalle.Rows[dgvdetalle.SelectedCells[1].RowIndex].Cells[0].Value.ToString();
                anio = dgvdetalle.Rows[dgvdetalle.SelectedCells[1].RowIndex].Cells[2].Value.ToString();
                idtipocal = dgvdetalle.Rows[dgvdetalle.SelectedCells[1].RowIndex].Cells[3].Value.ToString();
                idcia = dgvdetalle.Rows[dgvdetalle.SelectedCells[1].RowIndex].Cells[6].Value.ToString();
                idtipoper = dgvdetalle.Rows[dgvdetalle.SelectedCells[1].RowIndex].Cells[7].Value.ToString();



                DialogResult resultado = MessageBox.Show("¿Desea eliminar el Calendario " + messem + '-' + anio + "?",
                "Consulta Importante",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);
                if (resultado == DialogResult.Yes)
                {
                    CalPlan calplan = new CalPlan();
                    calplan.eliminarCalPlan(messem, anio, idtipoper, idcia, idtipocal);
                    this.ui_ListaCalPlan();
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
            exporta.Excel_FromDataGridView(dgvdetalle);

        }

        private void btnPDF_Click(object sender, EventArgs e)
        {
            Exporta exporta = new Exporta();
            exporta.Pdf_FromDataGridView(dgvdetalle, 3);
        }

        private void cmbTipoCal_SelectedIndexChanged(object sender, EventArgs e)
        {
            ui_ListaCalPlan();
        }
    }
}