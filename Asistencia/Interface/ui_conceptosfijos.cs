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
    public partial class ui_conceptosfijos : Form
    {
        public ui_conceptosfijos()
        {
            InitializeComponent();
        }

        private void ui_listaTipoCal(string idcia, string idtipoplan)
        {
            Funciones funciones = new Funciones();
            string query;
            query = " SELECT idtipocal as clave,destipocal as descripcion ";
            query = query + " FROM tipocal where idtipocal in (select idtipocal ";
            query = query + " from calcia where idcia='" + @idcia + "' and idtipoplan='" + @idtipoplan + "') ";
            query = query + " order by ordentipocal asc;";
            funciones.listaToolStripComboBox(query, cmbTipoCal);
        }

        private void ui_conceptosfijos_Load(object sender, EventArgs e)
        {
            Funciones funciones = new Funciones();
            GlobalVariables globalVariable = new GlobalVariables();
            string squery;
            string idcia = globalVariable.getValorCia();
            squery = "SELECT idtipoper as clave,destipoper as descripcion FROM tipoper order by 1 asc;";
            funciones.listaToolStripComboBox(squery, cmbTipoPer);
            squery = "SELECT idtipoplan as clave,destipoplan as descripcion FROM tipoplan where idtipoplan in (select idtipoplan from reglabcia where idcia='" + @idcia + "') order by 1 asc;";
            funciones.listaToolStripComboBox(squery, cmbTipoPlan);
            string idtipoplan = cmbTipoPlan.Text.Substring(0, 3);
            ui_listaTipoCal(idcia, idtipoplan);
            ui_Lista();
        }

        private void ui_Lista()
        {
            try
            {
                Funciones funciones = new Funciones();
                GlobalVariables globalVariable = new GlobalVariables();
                string idcia = globalVariable.getValorCia();
                string idtipoper = cmbTipoPer.Text.Substring(0, 1);
                string idtipoplan = cmbTipoPlan.Text.Substring(0, 3);
                string idtipocal = cmbTipoCal.Text.Substring(0, 1);

                SqlConnection conexion = new SqlConnection();
                conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
                conexion.Open();

                string query = " select A.idperplan,D.parm1maesgen,B.nrodoc,CONCAT(CONCAT(CONCAT(B.apepat,' ') ";
                query = query + " ,CONCAT(B.apemat,' , ')),B.nombres) as nombre ,A.idconplan,E.desboleta,";
                query = query + " A.tipocalculo,A.valor,A.idcia from confijos A left join perplan B on ";
                query = query + " A.idperplan=B.idperplan and A.idcia=B.idcia left join maesgen D";
                query = query + " on B.tipdoc=D.clavemaesgen and D.idmaesgen='002' left join detconplan E";
                query = query + " on  A.idcia=E.idcia and A.idconplan=E.idconplan and A.idtipoplan=E.idtipoplan and A.idtipoper=E.idtipoper and A.idtipocal=E.idtipocal";
                query = query + " where  A.idcia='" + @idcia + "' and A.idtipoper='" + @idtipoper;
                query = query + "' and A.idtipoplan='" + @idtipoplan + "' and A.idtipocal='" + @idtipocal + "' order by nombre asc;";


                using (SqlDataAdapter myDataAdapter = new SqlDataAdapter(query, conexion))
                {
                    DataSet myDataSet = new DataSet();
                    myDataAdapter.Fill(myDataSet, "tblConceptos");
                    funciones.formatearDataGridView(dgvdetalle);

                    dgvdetalle.DataSource = myDataSet.Tables["tblConceptos"];
                    dgvdetalle.Columns[0].HeaderText = "Código";
                    dgvdetalle.Columns[1].HeaderText = "Doc.Ident.";
                    dgvdetalle.Columns[2].HeaderText = "Nro.Doc.Ident.";
                    dgvdetalle.Columns[3].HeaderText = "Apellidos y Nombres";
                    dgvdetalle.Columns[4].HeaderText = "Concepto";
                    dgvdetalle.Columns[5].HeaderText = "Des.Boleta";
                    dgvdetalle.Columns[6].HeaderText = "Tipo Cálculo";
                    dgvdetalle.Columns[7].HeaderText = "Valor";


                    dgvdetalle.Columns[7].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    dgvdetalle.Columns[7].DefaultCellStyle.Format = "###,###.##";
                    dgvdetalle.Columns["idcia"].Visible = false;

                    dgvdetalle.Columns[0].Width = 50;
                    dgvdetalle.Columns[1].Width = 60;
                    dgvdetalle.Columns[2].Width = 80;
                    dgvdetalle.Columns[3].Width = 250;
                    dgvdetalle.Columns[4].Width = 60;
                    dgvdetalle.Columns[5].Width = 150;
                    dgvdetalle.Columns[6].Width = 50;
                    dgvdetalle.Columns[7].Width = 70;
                    ui_calculaNumRegistros();

                }
                conexion.Close();
            }
            catch (Exception)
            {

            }





        }

        internal void ui_calculaNumRegistros()
        {
            ConFijos confijos = new ConFijos();
            Funciones funciones = new Funciones();
            string idtipoper = cmbTipoPer.Text.Substring(0, 1);
            string idtipoplan = cmbTipoPlan.Text.Substring(0, 3);
            string idtipocal = cmbTipoCal.Text.Substring(0, 1);
            txtRegTotal.Text = string.Empty;
            string numreg = confijos.getNumeroRegistros(idtipoplan, idtipoper, idtipocal);
            txtRegTotal.Text = funciones.replicateCadena(" ", 15 - numreg.Trim().Length) + numreg.Trim() + funciones.replicateCadena(" ", 20) + "Registrados";
            string numregencontrados = Convert.ToString(dgvdetalle.RowCount);
            txtRegEncontrados.Text = funciones.replicateCadena(" ", 15 - numregencontrados.Trim().Length) + numregencontrados.Trim() + funciones.replicateCadena(" ", 20) + " De acuerdo al criterio de búsqueda";

        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {

            if (cmbTipoPer.Text == string.Empty)
            {
                MessageBox.Show("No ha definido Tipo de Personal", "Aviso Importante", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }
            else
            {
                if (cmbTipoPlan.Text == string.Empty)
                {
                    MessageBox.Show("No ha definido Tipo de Planilla", "Aviso Importante", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    GlobalVariables globalVariable = new GlobalVariables();
                    string idtipoper = cmbTipoPer.Text.Substring(0, 1);
                    string idtipoplan = cmbTipoPlan.Text.Substring(0, 3);
                    string idtipocal = cmbTipoCal.Text.Substring(0, 1);
                    string idcia = globalVariable.getValorCia();
                    ui_updconfijos ui_detalle = new ui_updconfijos();
                    ui_detalle._FormPadre = this;
                    ui_detalle.ui_setConFijo(idtipoper, idtipoplan, idcia, idtipocal);
                    ui_detalle.ui_newConFijo();
                    ui_detalle.ui_actualizaComboBox();
                    ui_detalle.Activate();
                    ui_detalle.BringToFront();
                    ui_detalle.ShowDialog();
                    ui_detalle.Dispose();
                }
            }
        }

        private void btnTodos_Click(object sender, EventArgs e)
        {
            this.ui_Lista();
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            this.ui_Lista();
        }

        private void cmbTipoPer_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.ui_Lista();
        }

        private void cmbTipoPlan_SelectedIndexChanged(object sender, EventArgs e)
        {
            GlobalVariables variables = new GlobalVariables();
            string idtipoplan = cmbTipoPlan.Text.Substring(0, 3);
            string idcia = variables.getValorCia();
            ui_listaTipoCal(idcia, idtipoplan);
            this.ui_Lista();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            string buscar = txtBuscar.Text.Trim();

            if (buscar != string.Empty)
            {
                GlobalVariables globalVariable = new GlobalVariables();
                Funciones funciones = new Funciones();
                ConFijos confijos = new ConFijos();
                txtRegEncontrados.Text = string.Empty;
                string idcia = globalVariable.getValorCia();
                string idtipoper = cmbTipoPer.Text.Substring(0, 1);
                string idtipoplan = cmbTipoPlan.Text.Substring(0, 3);
                string idtipocal = cmbTipoCal.Text.Substring(0, 1);

                string cadenaBusqueda = string.Empty;


                if (radioButtonCodigoInterno.Checked && txtBuscar.Text.Trim() != string.Empty)
                {
                    cadenaBusqueda = " and A.idperplan='" + @buscar + "' ";
                }
                else
                {
                    if (radioButtonNombre.Checked && txtBuscar.Text.Trim() != string.Empty)
                    {
                        cadenaBusqueda = " and CONCAT(CONCAT(CONCAT(B.apepat,' '),CONCAT(B.apemat,' , ')),B.nombres) like '%" + @buscar + "%' ";
                    }
                    else
                    {
                        if (radioButtonNroDoc.Checked && txtBuscar.Text.Trim() != string.Empty)
                        {
                            cadenaBusqueda = " and B.nrodoc='" + @buscar + "' ";
                        }

                    }

                }

                string query = "select A.idperplan,D.parm1maesgen,B.nrodoc,CONCAT(CONCAT(CONCAT(B.apepat,' ') ";
                query = query + " ,CONCAT(B.apemat,' , ')),B.nombres) as nombre ,A.idconplan,E.desboleta,";
                query = query + " A.tipocalculo,A.valor,A.idcia from confijos A left join perplan B on ";
                query = query + " A.idperplan=B.idperplan and A.idcia=B.idcia left join maesgen D";
                query = query + " on B.tipdoc=D.clavemaesgen and D.idmaesgen='002' left join detconplan E";
                query = query + " on  A.idconplan=E.idconplan and A.idtipoplan=E.idtipoplan and A.idtipoper=E.idtipoper and A.idtipocal=E.idtipocal";
                query = query + " where  A.idcia='" + @idcia + "' and A.idtipoper='" + @idtipoper;
                query = query + "' and A.idtipoplan='" + @idtipoplan + "' and A.idtipocal='" + @idtipocal + "' " + @cadenaBusqueda + " order by nombre asc;";



                SqlConnection conexion = new SqlConnection();
                conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
                conexion.Open();

                try
                {
                    using (SqlDataAdapter myDataAdapter = new SqlDataAdapter(query, conexion))
                    {
                        DataSet myDataSet = new DataSet();
                        myDataAdapter.Fill(myDataSet, "tblConceptos");
                        funciones.formatearDataGridView(dgvdetalle);

                        dgvdetalle.DataSource = myDataSet.Tables["tblConceptos"];
                        dgvdetalle.Columns[0].HeaderText = "Código";
                        dgvdetalle.Columns[1].HeaderText = "Doc.Ident.";
                        dgvdetalle.Columns[2].HeaderText = "Nro.Doc.Ident.";
                        dgvdetalle.Columns[3].HeaderText = "Apellidos y Nombres";
                        dgvdetalle.Columns[4].HeaderText = "Concepto";
                        dgvdetalle.Columns[5].HeaderText = "Des.Boleta";
                        dgvdetalle.Columns[6].HeaderText = "Tipo Cálculo";
                        dgvdetalle.Columns[7].HeaderText = "Valor";

                        dgvdetalle.Columns["idcia"].Visible = false;

                        dgvdetalle.Columns[0].Width = 50;
                        dgvdetalle.Columns[1].Width = 60;
                        dgvdetalle.Columns[2].Width = 80;
                        dgvdetalle.Columns[3].Width = 250;
                        dgvdetalle.Columns[4].Width = 60;
                        dgvdetalle.Columns[5].Width = 150;
                        dgvdetalle.Columns[6].Width = 50;
                        dgvdetalle.Columns[7].Width = 70;
                        ui_calculaNumRegistros();

                    }


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

        private void btnEditar_Click(object sender, EventArgs e)
        {
            string idcia;
            string idperplan;
            string idtipoper;
            string idtipoplan;
            string idconplan;
            string idtipocal;
            string tipocalculo;
            float valor;

            Int32 selectedCellCount = dgvdetalle.GetCellCount(DataGridViewElementStates.Selected);
            if (selectedCellCount > Convert.ToInt32(0))
            {
                idtipoper = cmbTipoPer.Text.Substring(0, 1);
                idtipoplan = cmbTipoPlan.Text.Substring(0, 4);
                idtipocal = cmbTipoCal.Text.Substring(0, 1);
                idperplan = dgvdetalle.Rows[dgvdetalle.SelectedCells[1].RowIndex].Cells[0].Value.ToString();
                idconplan = dgvdetalle.Rows[dgvdetalle.SelectedCells[1].RowIndex].Cells[4].Value.ToString();
                tipocalculo = dgvdetalle.Rows[dgvdetalle.SelectedCells[1].RowIndex].Cells[6].Value.ToString();
                valor = float.Parse(dgvdetalle.Rows[dgvdetalle.SelectedCells[1].RowIndex].Cells[7].Value.ToString());
                idcia = dgvdetalle.Rows[dgvdetalle.SelectedCells[1].RowIndex].Cells[8].Value.ToString();

                ui_updconfijos ui_detalle = new ui_updconfijos();
                ui_detalle._FormPadre = this;
                ui_detalle.ui_setConFijo(idtipoper, idtipoplan, idcia, idtipocal);
                ui_detalle.ui_actualizaComboBox();
                ui_detalle.Activate();
                ui_detalle.ui_loadForm(idperplan, idconplan, tipocalculo, valor);
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
            string idcia;
            string idperplan;
            string idtipoper;
            string idtipocal;
            string idtipoplan;
            string idconplan;
            string desboleta;

            Int32 selectedCellCount =
            dgvdetalle.GetCellCount(DataGridViewElementStates.Selected);
            if (selectedCellCount > Convert.ToInt32(1))
            {
                idtipoper = cmbTipoPer.Text.Substring(0, 1);
                idtipoplan = cmbTipoPlan.Text.Substring(0, 4);
                idtipocal = cmbTipoCal.Text.Substring(0, 1);
                idperplan = dgvdetalle.Rows[dgvdetalle.SelectedCells[1].RowIndex].Cells[0].Value.ToString();
                idconplan = dgvdetalle.Rows[dgvdetalle.SelectedCells[1].RowIndex].Cells[4].Value.ToString();
                desboleta = dgvdetalle.Rows[dgvdetalle.SelectedCells[1].RowIndex].Cells[5].Value.ToString();
                idcia = dgvdetalle.Rows[dgvdetalle.SelectedCells[1].RowIndex].Cells[8].Value.ToString();

                DialogResult resultado = MessageBox.Show("¿Desea eliminar el Concepto Fijo " + desboleta + "?",
                "Consulta Importante",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);
                if (resultado == DialogResult.Yes)
                {
                    ConFijos confijos = new ConFijos();
                    confijos.eliminarConFijos(idconplan, idtipoplan, idtipoper, idcia, idperplan, idtipocal);
                    this.ui_Lista();
                }

            }
            else
            {
                MessageBox.Show("No ha seleccionado registro a Eliminar", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void cmbTipoCal_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.ui_Lista();
        }
    }
}