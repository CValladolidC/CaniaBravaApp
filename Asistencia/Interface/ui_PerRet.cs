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
    public partial class ui_PerRet : Form
    {
        public ui_PerRet()
        {
            InitializeComponent();
        }

        private void ui_PerRet_Load(object sender, EventArgs e)
        {
            ui_ListaPerRet();
        }

        private void ui_ListaPerRet()
        {

            GlobalVariables variables = new GlobalVariables();
            Funciones funciones = new Funciones();

            string idcia = variables.getValorCia();
            string query = " Select A.idperplan,C.Parm1maesgen as cortotipodoc,A.nrodoc,";
            query = query + " CONCAT(A.apepat,' ',A.apemat,' , ',A.nombres) as nombre,";
            query = query + " A.fecnac,A.ruc,A.idcia ";
            query = query + " from perret A left join maesgen C on C.idmaesgen='002' ";
            query = query + " and A.tipdoc=C.clavemaesgen ";
            query = query + " where A.idcia='" + @idcia + "' order by idperplan asc;";

            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();

            try
            {
                using (SqlDataAdapter myDataAdapter = new SqlDataAdapter(query, conexion))
                {
                    DataSet myDataSet = new DataSet();
                    myDataAdapter.Fill(myDataSet, "tblPerRet");
                    funciones.formatearDataGridView(dgvdetalle);

                    dgvdetalle.DataSource = myDataSet.Tables["tblPerRet"];
                    dgvdetalle.Columns[0].HeaderText = "Cód.Int.";
                    dgvdetalle.Columns[1].HeaderText = "Doc.Ident.";
                    dgvdetalle.Columns[2].HeaderText = "Nro.Doc.";
                    dgvdetalle.Columns[3].HeaderText = "Apellidos y Nombres";
                    dgvdetalle.Columns[4].HeaderText = "F. Nac.";
                    dgvdetalle.Columns[5].HeaderText = "R.U.C.";

                    dgvdetalle.Columns["idcia"].Visible = false;

                    dgvdetalle.Columns[0].Width = 50;
                    dgvdetalle.Columns[1].Width = 60;
                    dgvdetalle.Columns[2].Width = 70;
                    dgvdetalle.Columns[3].Width = 300;
                    dgvdetalle.Columns[4].Width = 75;
                    dgvdetalle.Columns[5].Width = 100;
                }
                ui_calculaNumRegistros();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            conexion.Close();
        }

        internal void ui_calculaNumRegistros()
        {
            GlobalVariables variables = new GlobalVariables();
            PerRet perret = new PerRet();
            Funciones funciones = new Funciones();

            txtRegTotal.Text = string.Empty;
            string numreg = perret.getNumeroRegistrosPerRet(variables.getValorCia());
            txtRegTotal.Text = funciones.replicateCadena(" ", 15 - numreg.Trim().Length) + numreg.Trim() + funciones.replicateCadena(" ", 20) + "Registrados";
            string numregencontrados = Convert.ToString(dgvdetalle.RowCount);
            txtRegEncontrados.Text = funciones.replicateCadena(" ", 15 - numregencontrados.Trim().Length) + numregencontrados.Trim() + funciones.replicateCadena(" ", 20) + " De acuerdo al criterio de búsqueda";

        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            ui_updPerRet ui_detalle = new ui_updPerRet();
            ui_detalle._FormPadre = this;
            ui_detalle.ui_ActualizaComboBox();
            ui_detalle.newPerRet();
            ui_detalle.Activate();
            ui_detalle.BringToFront();
            ui_detalle.ShowDialog();
            ui_detalle.Dispose();
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            string idcia;
            string idperplan;

            Int32 selectedCellCount = dgvdetalle.GetCellCount(DataGridViewElementStates.Selected);
            if (selectedCellCount > 0)
            {
                idperplan = dgvdetalle.Rows[dgvdetalle.SelectedCells[1].RowIndex].Cells[0].Value.ToString();
                idcia = dgvdetalle.Rows[dgvdetalle.SelectedCells[1].RowIndex].Cells[6].Value.ToString();

                ui_updPerRet ui_detalle = new ui_updPerRet();
                ui_detalle._FormPadre = this;
                ui_detalle.ui_ActualizaComboBox();
                ui_detalle.Activate();
                ui_detalle.ui_loadPerRet(idcia, idperplan);
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

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            ui_ListaPerRet();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnTodos_Click(object sender, EventArgs e)
        {
            ui_ListaPerRet();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            string buscar = txtBuscar.Text.Trim();

            if (buscar != string.Empty)
            {
                GlobalVariables variables = new GlobalVariables();
                Funciones funciones = new Funciones();
                PerRet perret = new PerRet();
                txtRegEncontrados.Text = string.Empty;
                string idcia = variables.getValorCia();
                string cadenaBusqueda = string.Empty;

                if (radioButtonCodigoInterno.Checked && txtBuscar.Text.Trim() != string.Empty)
                {
                    cadenaBusqueda = " and A.idperplan='" + @buscar + "' ";
                }
                else
                {
                    if (radioButtonNombre.Checked && txtBuscar.Text.Trim() != string.Empty)
                    {
                        cadenaBusqueda = " and CONCAT(A.apepat,' ',A.apemat,' , ',A.nombres) like '%" + @buscar + "%' ";
                    }
                    else
                    {
                        cadenaBusqueda = " and A.nrodoc='" + @buscar + "' ";
                    }

                }

                string query = "Select A.idperplan,C.Parm1maesgen as cortotipodoc,A.nrodoc,";
                query = query + "CONCAT(A.apepat,' ',A.apemat,' , ',A.nombres) as nombre,A.fecnac,A.ruc,A.idcia ";
                query = query + " from perret A left join maesgen C on C.idmaesgen='002' and A.tipdoc=C.clavemaesgen ";
                query = query + "where A.idcia='" + @idcia + "'" + @cadenaBusqueda + " order by idperplan asc;";


                SqlConnection conexion = new SqlConnection();
                conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
                conexion.Open();

                try
                {
                    using (SqlDataAdapter myDataAdapter = new SqlDataAdapter(query, conexion))
                    {
                        DataSet myDataSet = new DataSet();
                        myDataAdapter.Fill(myDataSet, "tblPerRet");
                        funciones.formatearDataGridView(dgvdetalle);

                        dgvdetalle.DataSource = myDataSet.Tables["tblPerRet"];
                        dgvdetalle.Columns[0].HeaderText = "Cód.Int.";
                        dgvdetalle.Columns[1].HeaderText = "Doc.Ident.";
                        dgvdetalle.Columns[2].HeaderText = "Nro.Doc.";
                        dgvdetalle.Columns[3].HeaderText = "Apellidos y Nombres";
                        dgvdetalle.Columns[4].HeaderText = "F. Nac.";
                        dgvdetalle.Columns[5].HeaderText = "R.U.C.";

                        dgvdetalle.Columns["idcia"].Visible = false;

                        dgvdetalle.Columns[0].Width = 50;
                        dgvdetalle.Columns[1].Width = 60;
                        dgvdetalle.Columns[2].Width = 70;
                        dgvdetalle.Columns[3].Width = 300;
                        dgvdetalle.Columns[4].Width = 75;
                        dgvdetalle.Columns[5].Width = 100;


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

        private void radioButtonCodigoInterno_CheckedChanged(object sender, EventArgs e)
        {
            txtBuscar.Text = string.Empty;
            txtBuscar.Focus();
        }

        private void radioButtonNroDoc_CheckedChanged(object sender, EventArgs e)
        {
            txtBuscar.Text = string.Empty;
            txtBuscar.Focus();
        }

        private void radioButtonNombre_CheckedChanged(object sender, EventArgs e)
        {
            txtBuscar.Text = string.Empty;
            txtBuscar.Focus();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            Int32 selectedCellCount =
            dgvdetalle.GetCellCount(DataGridViewElementStates.Selected);
            if (selectedCellCount > 0)
            {
                string idperplan = dgvdetalle.Rows[dgvdetalle.SelectedCells[1].RowIndex].Cells[0].Value.ToString();
                string idcia = dgvdetalle.Rows[dgvdetalle.SelectedCells[1].RowIndex].Cells[6].Value.ToString();
                string nombre = dgvdetalle.Rows[dgvdetalle.SelectedCells[1].RowIndex].Cells[3].Value.ToString();
                DialogResult resultado = MessageBox.Show("¿Desea eliminar el Prestador de Servicios " + @nombre + "?",
                "Consulta Importante",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);
                if (resultado == DialogResult.Yes)
                {
                    PerRet perret = new PerRet();
                    perret.eliminarPerRet(idcia, idperplan);
                    this.ui_ListaPerRet();
                }

            }
            else
            {
                MessageBox.Show("No ha seleccionado registro a Eliminar", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }
    }
}