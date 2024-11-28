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
    public partial class ui_remuneraciones : Form
    {
        string _codcia;

        public ui_remuneraciones()
        {
            InitializeComponent();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            string buscar = txtBuscar.Text.Trim();

            if (buscar != string.Empty)
            {
                Funciones funciones = new Funciones();
                PerPlan perplan = new PerPlan();
                cmbCategoria.Text = "X   TODOS";
                txtRegEncontrados.Text = string.Empty;
                string idcia = this._codcia;
                string cadenaBusqueda = string.Empty;

                if (radioButtonCodigoInterno.Checked && txtBuscar.Text.Trim() != string.Empty)
                {
                    cadenaBusqueda = " and A.idperplan='" + @buscar + "' ";
                }
                else
                {
                    if (radioButtonNombre.Checked && txtBuscar.Text.Trim() != string.Empty)
                    {
                        cadenaBusqueda = " and CONCAT(CONCAT(CONCAT(A.apepat,' '),CONCAT(A.apemat,' , ')),A.nombres) like '%" + @buscar + "%' ";
                    }
                    else
                    {
                        cadenaBusqueda = " and A.nrodoc='" + @buscar + "' ";
                    }
                }
                string query = "Select A.idperplan,B.cortotipoper,C.Parm1maesgen as cortotipodoc,A.nrodoc,";
                query = query + "CONCAT(CONCAT(CONCAT(A.apepat,' '),CONCAT(A.apemat,' , ')),A.nombres) as nombre, ";
                query = query + "G.fini,G.importe from perplan A left join tipoper B on A.idtipoper=B.idtipoper ";
                query = query + "left join remu G on A.idcia=G.idcia and A.idperplan=G.idperplan and G.State='V'";
                query = query + "left join maesgen C on C.idmaesgen='002' and A.tipdoc=C.clavemaesgen ";
                query = query + "where A.idcia='" + @idcia + "'" + @cadenaBusqueda + " order by idperplan asc;";

                SqlConnection conexion = new SqlConnection();
                conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
                conexion.Open();

                try
                {
                    using (SqlDataAdapter myDataAdapter = new SqlDataAdapter(query, conexion))
                    {
                        DataSet myDataSet = new DataSet();
                        myDataAdapter.Fill(myDataSet, "tblPerPlan");
                        funciones.formatearDataGridView(dgvdetalle);
                        dgvdetalle.DataSource = myDataSet.Tables["tblPerPlan"];
                        dgvdetalle.Columns[0].HeaderText = "Cód.Int.";
                        dgvdetalle.Columns[1].HeaderText = "Cat.";
                        dgvdetalle.Columns[2].HeaderText = "Doc.Ident.";
                        dgvdetalle.Columns[3].HeaderText = "Nro.Doc.";
                        dgvdetalle.Columns[4].HeaderText = "Apellidos y Nombres";
                        dgvdetalle.Columns[5].HeaderText = "F.Ini.";
                        dgvdetalle.Columns[6].HeaderText = "Rem. Jornal/ Básico";

                        dgvdetalle.Columns[6].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                        dgvdetalle.Columns[6].DefaultCellStyle.Format = "###,###.##";

                        dgvdetalle.Columns[0].Width = 50;
                        dgvdetalle.Columns[1].Width = 40;
                        dgvdetalle.Columns[2].Width = 60;
                        dgvdetalle.Columns[3].Width = 70;
                        dgvdetalle.Columns[4].Width = 220;
                        dgvdetalle.Columns[5].Width = 75;
                        dgvdetalle.Columns[6].Width = 100;

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

        private void ui_remuneraciones_Load(object sender, EventArgs e)
        {
            GlobalVariables gv = new GlobalVariables();
            this._codcia = gv.getValorCia();
            string query = "SELECT idtipoper as clave,destipoper as descripcion FROM tipoper order by 1 asc;";
            Funciones funciones = new Funciones();
            MaesGen maesgen = new MaesGen();
            funciones.listaComboBox(query, cmbCategoria, "X");
            cmbCategoria.Text = "X   TODOS";
            ui_ListaPerPlan();
        }

        internal void ui_calculaNumRegistros()
        {
            PerPlan perplan = new PerPlan();
            Funciones funciones = new Funciones();
            txtRegTotal.Text = string.Empty;
            string numreg = perplan.getNumeroRegistrosPerPlan(this._codcia);
            txtRegTotal.Text = funciones.replicateCadena(" ", 15 - numreg.Trim().Length) + numreg.Trim() + funciones.replicateCadena(" ", 20) + "Registrados";
            string numregencontrados = Convert.ToString(dgvdetalle.RowCount);
            txtRegEncontrados.Text = funciones.replicateCadena(" ", 15 - numregencontrados.Trim().Length) + numregencontrados.Trim() + funciones.replicateCadena(" ", 20) + " De acuerdo al criterio de búsqueda";
        }

        private void ui_ListaPerPlan()
        {
            Funciones funciones = new Funciones();
            string buscar = txtBuscar.Text.Trim();
            string idcia = this._codcia;
            string idtipoper = funciones.getValorComboBox(cmbCategoria, 1);
            string cadenaidtipoper = string.Empty;
            string cadenaBusqueda = string.Empty;
            if (idtipoper != "X")
            {
                cadenaidtipoper = " and A.idtipoper='" + @idtipoper + "' ";
            }

            if (buscar != string.Empty)
            {
                if (radioButtonCodigoInterno.Checked && txtBuscar.Text.Trim() != string.Empty)
                {
                    cadenaBusqueda = " and A.idperplan='" + @buscar + "' ";
                }
                else
                {
                    if (radioButtonNombre.Checked && txtBuscar.Text.Trim() != string.Empty)
                    {
                        cadenaBusqueda = " and RTRIM(A.apepat)+' '+RTRIM(A.apemat)+', '+RTRIM(A.nombres) like '%" + @buscar + "%' ";
                    }
                    else
                    {
                        cadenaBusqueda = " and A.nrodoc='" + @buscar + "' ";
                    }
                }
            }

            string query = "Select A.idperplan,RTRIM(B.cortotipoper),C.Parm1maesgen as cortotipodoc,A.nrodoc,";
            query = query + "RTRIM(A.apepat)+' '+RTRIM(A.apemat)+', '+RTRIM(A.nombres) as nombre, ";
            query = query + "G.fini,G.importe,CASE G.state WHEN 'V' THEN 'Vigente' ELSE 'Cerrado' END [Estado] from perplan (NOLOCK) A left join tipoper (NOLOCK) B on A.idtipoper=B.idtipoper ";
            query = query + "left join remu (NOLOCK) G on A.idcia=G.idcia and A.idperplan=G.idperplan and G.State='V'";
            query = query + "left join maesgen (NOLOCK) C on C.idmaesgen='002' and A.tipdoc=C.clavemaesgen ";
            query = query + "where A.idcia='" + @idcia + "'" + @cadenaidtipoper + @cadenaBusqueda + " order by idperplan asc;";

            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();

            try
            {
                using (SqlDataAdapter myDataAdapter = new SqlDataAdapter(query, conexion))
                {
                    DataSet myDataSet = new DataSet();
                    myDataAdapter.Fill(myDataSet, "tblPerPlan");
                    funciones.formatearDataGridView(dgvdetalle);

                    dgvdetalle.DataSource = myDataSet.Tables["tblPerPlan"];
                    dgvdetalle.Columns[0].HeaderText = "Cód.Int.";
                    dgvdetalle.Columns[1].HeaderText = "Cat.";
                    dgvdetalle.Columns[2].HeaderText = "Doc.Ident.";
                    dgvdetalle.Columns[3].HeaderText = "Nro.Doc.";
                    dgvdetalle.Columns[4].HeaderText = "Apellidos y Nombres";
                    dgvdetalle.Columns[5].HeaderText = "F.Ini.";
                    dgvdetalle.Columns[6].HeaderText = "Rem. Jornal/ Básico";

                    dgvdetalle.Columns[6].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    dgvdetalle.Columns[6].DefaultCellStyle.Format = "###,###.##";

                    dgvdetalle.Columns[0].Width = 70;
                    dgvdetalle.Columns[1].Width = 70;
                    dgvdetalle.Columns[2].Width = 60;
                    dgvdetalle.Columns[3].Width = 70;
                    dgvdetalle.Columns[4].Width = 220;
                    dgvdetalle.Columns[5].Width = 75;
                    dgvdetalle.Columns[6].Width = 95;
                    dgvdetalle.Columns[7].Width = 70;
                }
                ui_calculaNumRegistros();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            conexion.Close();
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            Int32 selectedCellCount = dgvdetalle.GetCellCount(DataGridViewElementStates.Selected);
            if (selectedCellCount > 0)
            {
                string idperplan = dgvdetalle.Rows[dgvdetalle.SelectedCells[1].RowIndex].Cells[0].Value.ToString();
                string idcia = this._codcia;
                ui_updremuneracion ui_detalle = new ui_updremuneracion();
                ui_detalle._FormPadre = this;
                ui_detalle.Activate();
                ui_detalle.ui_load(idcia, idperplan);
                ui_detalle.ui_lista(idcia, idperplan);
                ui_detalle.BringToFront();
                ui_detalle.ShowDialog();
                ui_detalle.Dispose();
            }
            else
            {
                MessageBox.Show("No ha seleccionado registro a Configurar", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            this.ui_ListaPerPlan();
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

        private void btnTodos_Click(object sender, EventArgs e)
        {
            this.ui_ListaPerPlan();
        }

        private void cmbCategoria_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.ui_ListaPerPlan();
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            Exporta exporta = new Exporta();
            exporta.Excel_FromDataGridView(dgvdetalle);
        }

        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            this.ui_ListaPerPlan();
        }
    }
}