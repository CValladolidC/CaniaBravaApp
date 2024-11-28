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
    public partial class ui_motivos : Form
    {
        public ui_motivos()
        {
            InitializeComponent();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ui_ListaConceptos()
        {

            try
            {
                Funciones funciones = new Funciones();
                GlobalVariables globalVariable = new GlobalVariables();
                string idcia = globalVariable.getValorCia();
                string idtipoplan = cmbTipoPlan.Text.Substring(0, 3);
                string idclascol = cmbClasificacion.Text.Substring(0, 1);
                string idtipocal = cmbTipoCal.Text.Substring(0, 1);
                string tipo = cmbTipo.Text.Substring(0, 1);

                string cadenaClasificacion = string.Empty;
                string cadenaTipo = string.Empty;

                if (idclascol.Equals("X"))
                {
                    cadenaClasificacion = " ";
                }
                else
                {
                    cadenaClasificacion = " and idclascol='" + @idclascol + "' ";
                }

                if (tipo.Equals("X"))
                {
                    cadenaTipo = " ";
                }
                else
                {
                    cadenaTipo = " and tipo='" + @tipo + "' ";
                }
                string query = "select idconplan,desconplan,constante,idclascol,tipo,idtipoplan,idtipocal from conplan where idtipoplan='" + @idtipoplan + "' and idtipocal='" + @idtipocal + "' and idcia='" + @idcia + "' " + @cadenaClasificacion + @cadenaTipo + " order by idconplan asc";

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
                        dgvdetalle.Columns[1].HeaderText = "Descripción";
                        dgvdetalle.Columns[2].HeaderText = "Variable en Fórmula";
                        dgvdetalle.Columns[3].HeaderText = "Clasificación en Planilla";
                        dgvdetalle.Columns[4].HeaderText = "Tipo";

                        dgvdetalle.Columns["idtipoplan"].Visible = false;
                        dgvdetalle.Columns["idtipocal"].Visible = false;


                        dgvdetalle.Columns[0].Width = 100;
                        dgvdetalle.Columns[1].Width = 350;
                        dgvdetalle.Columns[2].Width = 100;
                        dgvdetalle.Columns[3].Width = 80;
                        dgvdetalle.Columns[4].Width = 50;


                    }
                    ui_calculaNumRegistros();
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

        internal void ui_calculaNumRegistros()
        {

            ConPlan conplan = new ConPlan();
            Funciones funciones = new Funciones();
            GlobalVariables globalVariable = new GlobalVariables();
            string idtipoplan = cmbTipoPlan.Text.Substring(0, 3);
            string idtipocal = cmbTipoCal.Text.Substring(0, 1);
            string idcia = globalVariable.getValorCia();
            txtRegTotal.Text = string.Empty;
            string numreg = conplan.getNumeroRegistros(idtipoplan, idcia, idtipocal);
            txtRegTotal.Text = funciones.replicateCadena(" ", 15 - numreg.Trim().Length) + numreg.Trim() + funciones.replicateCadena(" ", 20) + "Registrados";
            string numregencontrados = Convert.ToString(dgvdetalle.RowCount);
            txtRegEncontrados.Text = funciones.replicateCadena(" ", 15 - numregencontrados.Trim().Length) + numregencontrados.Trim() + funciones.replicateCadena(" ", 20) + " De acuerdo al criterio de búsqueda";

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

        private void ui_motivos_Load(object sender, EventArgs e)
        {
            GlobalVariables gv = new GlobalVariables();
            Funciones funciones = new Funciones();
            string idcia = gv.getValorCia();
            string squery = "SELECT idtipoplan as clave,destipoplan as descripcion FROM tipoplan where idtipoplan in (select idtipoplan from reglabcia where idcia='" + @idcia + "') order by 1 asc;";
            funciones.listaToolStripComboBox(squery, cmbTipoPlan);
            if (cmbTipoPlan.Items.Count > 0)
            {
                string idtipoplan = cmbTipoPlan.Text.Substring(0, 3);
                ui_listaTipoCal(idcia, idtipoplan);
                cmbClasificacion.Text = "X   TODAS";
                cmbTipo.Text = "X   TODAS";
            }
            ui_ListaConceptos();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            string buscar = txtBuscar.Text.Trim();

            if (buscar != string.Empty)
            {
                try
                {

                    Funciones funciones = new Funciones();
                    ConPlan conplan = new ConPlan();
                    GlobalVariables globalVariable = new GlobalVariables();
                    txtRegEncontrados.Text = string.Empty;
                    string idtipoplan = cmbTipoPlan.Text.Substring(0, 3);
                    string idclascol = cmbClasificacion.Text.Substring(0, 1);
                    string tipo = cmbTipo.Text.Substring(0, 1);
                    string idcia = globalVariable.getValorCia();

                    string cadenaBusqueda = string.Empty;
                    string cadenaTipo = string.Empty;


                    if (radioButtonCodigo.Checked && txtBuscar.Text.Trim() != string.Empty)
                    {
                        cadenaBusqueda = " and idconplan='" + @buscar + "' ";
                    }
                    else
                    {
                        if (radioButtonDescripcion.Checked && txtBuscar.Text.Trim() != string.Empty)
                        {
                            cadenaBusqueda = " and desconplan like '%" + @buscar + "%' ";
                        }
                        else
                        {
                            if (radioButtonVariable.Checked && txtBuscar.Text.Trim() != string.Empty)
                            {
                                cadenaBusqueda = " and constante='" + @buscar + "' ";
                            }

                        }

                    }

                    string cadenaClasificacion;

                    if (idclascol.Equals("X"))
                    {
                        cadenaClasificacion = " ";
                    }
                    else
                    {
                        cadenaClasificacion = " and idclascol='" + @idclascol + "' ";
                    }

                    if (tipo.Equals("X"))
                    {
                        cadenaTipo = " ";
                    }
                    else
                    {
                        cadenaTipo = " and tipo='" + @tipo + "' ";
                    }

                    string query = "select idconplan,desconplan,constante,idclascol,tipo,idtipoplan,idtipocal from conplan where idtipoplan='" + @idtipoplan + "' and idcia='" + @idcia + "' " + @cadenaClasificacion + @cadenaBusqueda + @cadenaTipo + " order by idconplan asc";

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
                            dgvdetalle.Columns[1].HeaderText = "Descripción";
                            dgvdetalle.Columns[2].HeaderText = "Variable en Fórmula";
                            dgvdetalle.Columns[3].HeaderText = "Clasificación en Planilla";
                            dgvdetalle.Columns[4].HeaderText = "Tipo";

                            dgvdetalle.Columns["idtipoplan"].Visible = false;
                            dgvdetalle.Columns["idtipocal"].Visible = false;


                            dgvdetalle.Columns[0].Width = 100;
                            dgvdetalle.Columns[1].Width = 350;
                            dgvdetalle.Columns[2].Width = 100;
                            dgvdetalle.Columns[3].Width = 80;
                            dgvdetalle.Columns[4].Width = 50;


                        }
                        ui_calculaNumRegistros();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    }

                    conexion.Close();
                }
                catch (ArgumentOutOfRangeException) { }
            }
        }

        private void radioButtonCodigo_CheckedChanged(object sender, EventArgs e)
        {
            txtBuscar.Text = string.Empty;
            txtBuscar.Focus();
        }

        private void radioButtonDescripcion_CheckedChanged(object sender, EventArgs e)
        {
            txtBuscar.Text = string.Empty;
            txtBuscar.Focus();
        }

        private void radioButtonVariable_CheckedChanged(object sender, EventArgs e)
        {
            txtBuscar.Text = string.Empty;
            txtBuscar.Focus();
        }

        private void btnTodos_Click(object sender, EventArgs e)
        {
            ui_ListaConceptos();
        }

        private void cmbTipoPlan_SelectedIndexChanged(object sender, EventArgs e)
        {
            GlobalVariables variables = new GlobalVariables();
            string idcia = variables.getValorCia();
            string idtipoplan = cmbTipoPlan.Text.Substring(0, 3);
            ui_listaTipoCal(idcia, idtipoplan);
            ui_ListaConceptos();
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            ui_ListaConceptos();
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            GlobalVariables globalVariable = new GlobalVariables();
            string idcia = globalVariable.getValorCia();
            string idtipoplan = cmbTipoPlan.Text.Substring(0, 3);
            string idtipocal = cmbTipoCal.Text.Substring(0, 1);

            ui_updmotivos ui_detalle = new ui_updmotivos();
            ui_detalle._FormPadre = this;
            ui_detalle.ui_newConPlan(idtipoplan, idcia, idtipocal);
            ui_detalle.Activate();
            ui_detalle.BringToFront();
            ui_detalle.ShowDialog();
            ui_detalle.Dispose();
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {

        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            string idconplan;
            string idtipoplan;
            string idtipocal;
            string desconplan;
            string idcia;

            Int32 selectedCellCount =
            dgvdetalle.GetCellCount(DataGridViewElementStates.Selected);
            if (selectedCellCount > Convert.ToInt32(1))
            {
                idconplan = dgvdetalle.Rows[dgvdetalle.SelectedCells[1].RowIndex].Cells[0].Value.ToString();
                desconplan = dgvdetalle.Rows[dgvdetalle.SelectedCells[1].RowIndex].Cells[1].Value.ToString();
                idtipoplan = dgvdetalle.Rows[dgvdetalle.SelectedCells[1].RowIndex].Cells[5].Value.ToString();
                idtipocal = dgvdetalle.Rows[dgvdetalle.SelectedCells[1].RowIndex].Cells[6].Value.ToString();

                GlobalVariables globalVariable = new GlobalVariables();
                idcia = globalVariable.getValorCia();
                DialogResult resultado = MessageBox.Show("¿Desea eliminar el Concepto de Planilla " + desconplan + "?",
                "Consulta Importante",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);
                if (resultado == DialogResult.Yes)
                {
                    ConPlan conplan = new ConPlan();
                    conplan.eliminarConplan(idconplan, idtipoplan, idcia, idtipocal);
                    this.ui_ListaConceptos();
                }

            }
            else
            {
                MessageBox.Show("No ha seleccionado registro a Eliminar", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            Exporta exporta = new Exporta();
            exporta.Excel_FromDataGridView(dgvdetalle);
        }

        private void btnConfigurar_Click(object sender, EventArgs e)
        {

            string idconplan;
            string idtipoplan;
            string idtipocal;
            string desconplan;
            string constante;
            string idclascol;
            string tipo;
            string idcia;

            Int32 selectedCellCount =
            dgvdetalle.GetCellCount(DataGridViewElementStates.Selected);
            if (selectedCellCount > Convert.ToInt32(0))
            {
                idconplan = dgvdetalle.Rows[dgvdetalle.SelectedCells[1].RowIndex].Cells[0].Value.ToString();
                desconplan = dgvdetalle.Rows[dgvdetalle.SelectedCells[1].RowIndex].Cells[1].Value.ToString();
                constante = dgvdetalle.Rows[dgvdetalle.SelectedCells[1].RowIndex].Cells[2].Value.ToString();
                idclascol = dgvdetalle.Rows[dgvdetalle.SelectedCells[1].RowIndex].Cells[3].Value.ToString();
                tipo = dgvdetalle.Rows[dgvdetalle.SelectedCells[1].RowIndex].Cells[4].Value.ToString();
                idtipoplan = dgvdetalle.Rows[dgvdetalle.SelectedCells[1].RowIndex].Cells[5].Value.ToString();
                idtipocal = dgvdetalle.Rows[dgvdetalle.SelectedCells[1].RowIndex].Cells[6].Value.ToString();

                GlobalVariables globalVariable = new GlobalVariables();
                idcia = globalVariable.getValorCia();
                ui_confmotivos ui_confmotivos = new ui_confmotivos();
                ui_confmotivos._FormPadre = this;
                ui_confmotivos.ui_asignaparametros(idtipoplan, idclascol, idconplan, desconplan, tipo, idcia, idtipocal);
                ui_confmotivos.Activate();
                ui_confmotivos.BringToFront();
                ui_confmotivos.ShowDialog();
                ui_confmotivos.Dispose();

            }
            else
            {
                MessageBox.Show("No ha seleccionado registro a Configurar", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }

        }

        private void cmbTipo_Click(object sender, EventArgs e)
        {

        }

        private void cmbTipo_SelectedIndexChanged(object sender, EventArgs e)
        {
            ui_ListaConceptos();
        }

        private void cmbClasificacion_SelectedIndexChanged(object sender, EventArgs e)
        {
            ui_ListaConceptos();
        }

        private void cmbTipoCal_SelectedIndexChanged(object sender, EventArgs e)
        {
            ui_ListaConceptos();
        }

        private void btnEstandar_Click(object sender, EventArgs e)
        {
            GlobalVariables globalVariable = new GlobalVariables();
            string idcia = globalVariable.getValorCia();
            string idtipoplan = cmbTipoPlan.Text.Substring(0, 3);
            string idtipocal = cmbTipoCal.Text.Substring(0, 1);
            DialogResult resultado = MessageBox.Show("¿Desea copiar la Configuración Estándar?, tener en cuenta que la anterior Configuración se eliminará por completo",
                "Consulta Importante",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);
            if (resultado == DialogResult.Yes)
            {
                ConPlan conplan = new ConPlan();
                conplan.conPlanEstandar(idtipoplan, idcia, idtipocal);
                MessageBox.Show("Proceso Concluído", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ui_ListaConceptos();
            }

        }

        private void dgvdetalle_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void toolStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void groupBox3_Enter(object sender, EventArgs e)
        {

        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void toolstripform_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }
    }
}