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
    public partial class ui_gratifica : Form
    {
        string _idtipoplan;
        string _idtipocal;

        public ui_gratifica()
        {
            InitializeComponent();
        }

        private void ui_gratifica_Load(object sender, EventArgs e)
        {
            Funciones funciones = new Funciones();
            MaesGen maesgen = new MaesGen();
            GlobalVariables variables = new GlobalVariables();
            this._idtipocal = "G";
            this._idtipoplan = "100";
            string query;
            query = "SELECT idtipoper as clave,destipoper as descripcion FROM tipoper order by 1 asc";
            funciones.listaComboBox(query, cmbTipoTrabajador, "");
            query = "SELECT ruccia as clave,descia as descripcion FROM ciafile WHERE idcia='" + @variables.getValorCia() + "';";
            funciones.listaComboBox(query, cmbEmpleador, "");
            maesgen.listaDetMaesGen("035", cmbMesIni, "");
            maesgen.listaDetMaesGen("035", cmbMesFin, "");
            ui_ListaDataPlan();
        }

        private void ui_ListaDataPlan()
        {

            Funciones funciones = new Funciones();
            GlobalVariables variables = new GlobalVariables();
            string idcia = variables.getValorCia();
            string idtipoper = funciones.getValorComboBox(cmbTipoTrabajador, 1);
            string periodo = txtMesSem.Text.Trim() + funciones.replicateCadena(" ", 6);
            string messem = periodo.Substring(0, 2);
            string anio = periodo.Substring(3, 4);
            string rucemp = funciones.getValorComboBox(cmbEmpleador, 11);
            string idtipocal=this._idtipocal;
            string idtipoplan=this._idtipoplan;
            string query = "  select B.idperplan,D.Parm1maesgen as cortotipodoc,B.nrodoc,";
            query = query + " CONCAT(B.apepat,' ',B.apemat,' , ',B.nombres) as nombre,G.fechaini,";
            query = query + " G.fechafin,A.estane,A.riesgo,A.gratifica,H.desestane";
            query = query + " from dataplan A  ";
            query = query + " left join perplan B on A.idperplan=B.idperplan and A.idcia=B.idcia ";
            query = query + " left join maesgen D on D.idmaesgen='002' and B.tipdoc=D.clavemaesgen ";
            query = query + " left join view_perlab F on B.idcia=F.idcia and B.idperplan=F.idperplan ";
            query = query + " left join perlab G on F.idcia=G.idcia and F.idperplan=G.idperplan and F.idperlab=G.idperlab ";
            query = query + " left join estane H on A.emplea=H.codemp and A.estane=H.idestane and A.idcia=H.idciafile ";
            query = query + " where A.idcia='" + @idcia + "' and idtipocal='" + @idtipocal + "' ";
            query = query + " and A.idtipoper='" + @idtipoper + "' and A.messem='" + @messem + "' ";
            query = query + " and A.anio='" + @anio + "' and A.emplea='" + @rucemp + "' ";
            query = query + " and A.idtipoplan='" + @idtipoplan + "'  ";
            query = query + " order by A.iddataplan desc;";
            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();
            try
            {
                using (SqlDataAdapter myDataAdapter = new SqlDataAdapter(query, conexion))
                {
                    DataSet myDataSet = new DataSet();
                    myDataAdapter.Fill(myDataSet, "tblDataPlan");
                    funciones.formatearDataGridView(dgvdetalle);

                    dgvdetalle.DataSource = myDataSet.Tables["tblDataPlan"];
                    dgvdetalle.Columns[0].HeaderText = "Cód.Int.";
                    dgvdetalle.Columns[1].HeaderText = "Doc. Ident.";
                    dgvdetalle.Columns[2].HeaderText = "Nro.Doc.";
                    dgvdetalle.Columns[3].HeaderText = "Apellidos y Nombres";
                    dgvdetalle.Columns[4].HeaderText = "F.Ini.Lab.";
                    dgvdetalle.Columns[5].HeaderText = "F.Fin.Lab.";
                    dgvdetalle.Columns[6].HeaderText = "Estab.";
                    dgvdetalle.Columns[7].HeaderText = "% SCTR";
                    dgvdetalle.Columns[8].HeaderText = "Gratificación";
                    dgvdetalle.Columns["desestane"].Visible = false;
                    dgvdetalle.Columns[0].Width = 50;
                    dgvdetalle.Columns[1].Width = 50;
                    dgvdetalle.Columns[2].Width = 75;
                    dgvdetalle.Columns[3].Width = 200;
                    dgvdetalle.Columns[4].Width = 75;
                    dgvdetalle.Columns[5].Width = 75;
                    dgvdetalle.Columns[6].Width = 40;
                    dgvdetalle.Columns[7].Width = 40;
                    dgvdetalle.Columns[8].Width = 120;
                    dgvdetalle.RowHeadersVisible = true;
                    if (dgvdetalle.Rows.Count > 0)
                    {
                        for (int r = 0; r < dgvdetalle.Rows.Count; r++)
                        {
                            this.dgvdetalle.Rows[r].HeaderCell.Value = (r + 1).ToString();
                        }
                    }
                    dgvdetalle.AutoResizeRowHeadersWidth(DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            conexion.Close();
        }

        private void cmbTipoTrabajador_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                Funciones funciones = new Funciones();
                string clave = funciones.getValorComboBox(cmbTipoTrabajador, 1);
                string squery;
                if (cmbTipoTrabajador.Text != String.Empty)
                {
                    squery = "SELECT idtipoper as clave,destipoper as descripcion FROM tipoper WHERE idtipoper='" + @clave + "';";
                    funciones.validarCombobox(squery, cmbTipoTrabajador);
                }
                txtMesSem.Clear();
                txtFechaIni.Clear();
                txtFechaFin.Clear();
                e.Handled = true;
                txtMesSem.Focus();
            }
        }

        private void txtMesSem_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                Funciones funciones = new Funciones();
                CalPlan calplan = new CalPlan();
                GlobalVariables variables = new GlobalVariables();
                string idcia = variables.getValorCia();
                string idtipoper = funciones.getValorComboBox(cmbTipoTrabajador, 1);
                string periodo = txtMesSem.Text.Trim() + funciones.replicateCadena(" ", 6);
                string messem = periodo.Substring(0, 2);
                string anio = periodo.Substring(3, 4);
                string idtipocal = this._idtipocal;
                if (calplan.getDatosCalPlan(messem, anio, idtipoper, idcia, idtipocal, "FECHAINI") != "")
                {
                    txtFechaIni.Text = calplan.getDatosCalPlan(messem, anio, idtipoper, idcia, idtipocal, "FECHAINI");
                    txtFechaFin.Text = calplan.getDatosCalPlan(messem, anio, idtipoper, idcia, idtipocal, "FECHAFIN");
                }
                else
                {
                    MessageBox.Show("El Periodo Laboral no existe", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtFechaIni.Clear();
                    txtFechaFin.Clear();
                    e.Handled = true;
                    txtMesSem.Focus();
                }
                ui_ListaDataPlan();

            }
        }

        private void radioButtonNoEmp_CheckedChanged(object sender, EventArgs e)
        {
            Funciones funciones = new Funciones();
            GlobalVariables variables = new GlobalVariables();
            string idcia = variables.getValorCia();
            string squery = "SELECT ruccia as clave,descia as descripcion FROM ciafile WHERE idcia='" + @idcia + "';";
            funciones.listaComboBox(squery, cmbEmpleador, "");
            cmbEmpleador.Enabled = false;
            ui_ListaDataPlan();
        }

        private void radioButtonSiEmp_CheckedChanged(object sender, EventArgs e)
        {
            Funciones funciones = new Funciones();
            GlobalVariables variables = new GlobalVariables();
            string idcia = variables.getValorCia();
            string squery = "SELECT rucemp as clave,razonemp as descripcion FROM emplea WHERE idciafile='" + @idcia + "' order by rucemp asc;";
            funciones.listaComboBox(squery, cmbEmpleador, "");
            cmbEmpleador.Enabled = true;
            ui_ListaDataPlan();
            cmbEmpleador.Focus();
        }

        private void cmbTipoTrabajador_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtMesSem.Clear();
            txtFechaIni.Clear();
            txtFechaFin.Clear();
            txtMesSem.Focus();
            ui_ListaDataPlan();
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            GlobalVariables variables = new GlobalVariables();
            Funciones funciones = new Funciones();
            CalPlan calplan = new CalPlan();
            string idcia = variables.getValorCia();
            string idtipoper = funciones.getValorComboBox(cmbTipoTrabajador, 1);
            string empleador = cmbEmpleador.Text.Trim();
            string periodo = txtMesSem.Text.Trim() + funciones.replicateCadena(" ", 6);
            string messem = periodo.Substring(0, 2);
            string anio = periodo.Substring(3, 4);
            string fechaini = txtFechaIni.Text;
            string fechafin = txtFechaFin.Text;
            string idtipoplan = this._idtipoplan;
            string idtipocal = this._idtipocal;
            if (calplan.getDatosCalPlan(messem, anio, idtipoper, idcia, idtipocal, "FECHAINI") != "")
            {
                if (calplan.getDatosCalPlan(messem, anio, idtipoper, idcia, idtipocal, "ESTADO") != "C")
                {
                    if (empleador != string.Empty)
                    {
                        ui_updgratifica ui_detalle = new ui_updgratifica();
                        ui_detalle._FormPadre = this;
                        ui_detalle.setValores(variables.getValorCia(), idtipoper, empleador, anio, messem, idtipocal, fechaini, fechafin, idtipoplan);
                        ui_detalle.ui_newDatosPlanilla();
                        ui_detalle.Activate();
                        ui_detalle.BringToFront();
                        ui_detalle.ShowDialog();
                        ui_detalle.Dispose();
                    }
                    else
                    {
                        MessageBox.Show("No ha seleccionado Empleador", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("El Periodo Laboral ya se encuentra cerrado", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("El Periodo Laboral no existe", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtMesSem.Focus();
            }
        }

        private void cmbEmpleador_SelectedIndexChanged(object sender, EventArgs e)
        {
            ui_ListaDataPlan();
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            Int32 selectedCellCount =
            dgvdetalle.GetCellCount(DataGridViewElementStates.Selected);
            if (selectedCellCount > 0)
            {
                GlobalVariables variables = new GlobalVariables();
                Funciones funciones = new Funciones();
                CalPlan calplan = new CalPlan();
                string idperplan = dgvdetalle.Rows[dgvdetalle.SelectedCells[1].RowIndex].Cells[0].Value.ToString();
                string idestane = dgvdetalle.Rows[dgvdetalle.SelectedCells[1].RowIndex].Cells[6].Value.ToString();
                string riesgo = dgvdetalle.Rows[dgvdetalle.SelectedCells[1].RowIndex].Cells[7].Value.ToString();
                string gratifica = dgvdetalle.Rows[dgvdetalle.SelectedCells[1].RowIndex].Cells[8].Value.ToString();
                string establecimiento = dgvdetalle.Rows[dgvdetalle.SelectedCells[1].RowIndex].Cells[6].Value.ToString() + "  " + dgvdetalle.Rows[dgvdetalle.SelectedCells[1].RowIndex].Cells[9].Value.ToString();
                string idtipoper = funciones.getValorComboBox(cmbTipoTrabajador, 1);
                string idtipoplan = this._idtipoplan;
                string idtipocal = this._idtipocal;
                string empleador = cmbEmpleador.Text.Trim();
                string periodo = txtMesSem.Text.Trim() + funciones.replicateCadena(" ", 6);
                string messem = periodo.Substring(0, 2);
                string anio = periodo.Substring(3, 4);
                string fechaini = txtFechaIni.Text;
                string fechafin = txtFechaFin.Text;
                string idcia = variables.getValorCia();

                if (calplan.getDatosCalPlan(messem, anio, idtipoper, idcia, idtipocal, "ESTADO") != "C")
                {
                    ui_updgratifica ui_detalle = new ui_updgratifica();
                    ui_detalle._FormPadre = this;
                    ui_detalle.setValores(idcia, idtipoper, empleador, anio, messem, idtipocal, fechaini, fechafin, idtipoplan);
                    ui_detalle.Activate();
                    ui_detalle.ui_loadDatosPlanilla(idperplan, establecimiento, riesgo, idestane, gratifica);
                    ui_detalle.BringToFront();
                    ui_detalle.ShowDialog();
                    ui_detalle.Dispose();
                }
                else
                {
                    MessageBox.Show("El Periodo Laboral ya se encuentra cerrado", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
            else
            {
                MessageBox.Show("No ha seleccionado registro a Editar", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            ui_ListaDataPlan();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            DataPlan dataplan = new DataPlan();
            Int32 selectedCellCount = dgvdetalle.GetCellCount(DataGridViewElementStates.Selected);
            if (selectedCellCount > 0)
            {
                GlobalVariables variables = new GlobalVariables();
                Funciones funciones = new Funciones();
                CalPlan calplan = new CalPlan();
                string idperplan = dgvdetalle.Rows[dgvdetalle.SelectedCells[1].RowIndex].Cells[0].Value.ToString();
                string trabajador = dgvdetalle.Rows[dgvdetalle.SelectedCells[1].RowIndex].Cells[3].Value.ToString();
                string idtipoper = funciones.getValorComboBox(cmbTipoTrabajador, 1);
                string periodo = txtMesSem.Text.Trim() + funciones.replicateCadena(" ", 6);
                string messem = periodo.Substring(0, 2);
                string anio = periodo.Substring(3, 4);
                string idtipoplan = this._idtipoplan;
                string idtipocal = this._idtipocal;
                string idcia = variables.getValorCia();

                if (calplan.getDatosCalPlan(messem, anio, idtipoper, idcia, idtipocal, "ESTADO") != "C")
                {
                    DialogResult resultado = MessageBox.Show("¿Desea eliminar la información de Planilla del trabajador " + trabajador + "?",
                    "Consulta Importante",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);
                    if (resultado == DialogResult.Yes)
                    {
                        dataplan.eliminarDataPlan(idperplan, variables.getValorCia(), anio, messem, idtipoper, idtipocal, idtipoplan);
                        ui_ListaDataPlan();
                    }
                }
                else
                {
                    MessageBox.Show("El Periodo Laboral ya se encuentra cerrado", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
            else
            {
                MessageBox.Show("No ha seleccionado registro a Eliminar", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void cmbTipoPlan_SelectedIndexChanged(object sender, EventArgs e)
        {
            ui_ListaDataPlan();
        }

        private void cmbTipoCal_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtMesSem.Clear();
            txtFechaIni.Clear();
            txtFechaFin.Clear();
            txtMesSem.Focus();
            ui_ListaDataPlan();
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            Exporta exporta = new Exporta();
            exporta.Excel_FromDataGridView(dgvdetalle);
        }

        private void btnTodos_Click(object sender, EventArgs e)
        {
            ui_ListaDataPlan();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            string buscar = txtBuscar.Text.Trim();

            if (buscar != string.Empty)
            {
                Funciones funciones = new Funciones();
                GlobalVariables variables = new GlobalVariables();
                string idcia = variables.getValorCia();
                string idtipoper = funciones.getValorComboBox(cmbTipoTrabajador, 1);
                string idtipoplan = this._idtipoplan;
                string periodo = txtMesSem.Text.Trim() + funciones.replicateCadena(" ", 6);
                string messem = periodo.Substring(0, 2);
                string anio = periodo.Substring(3, 4);
                string idtipocal = this._idtipocal;
                string rucemp = funciones.getValorComboBox(cmbEmpleador, 11);
                string cadenaBusqueda = string.Empty;

                if (radioButtonCodigoInterno.Checked && txtBuscar.Text.Trim() != string.Empty)
                {
                    cadenaBusqueda = " and A.idperplan='" + @buscar + "' ";
                }
                else
                {
                    if (radioButtonNombre.Checked && txtBuscar.Text.Trim() != string.Empty)
                    {
                        cadenaBusqueda = " and CONCAT(B.apepat,' ',B.apemat,' , ',B.nombres) like '%" + @buscar + "%' ";
                    }
                    else
                    {
                        if (radioButtonNroDoc.Checked && txtBuscar.Text.Trim() != string.Empty)
                        {
                            cadenaBusqueda = " and B.nrodoc='" + @buscar + "' ";
                        }

                    }

                }

                string query = "  select B.idperplan,D.Parm1maesgen as cortotipodoc,B.nrodoc,";
                query = query + " CONCAT(B.apepat,' ',B.apemat,' , ',B.nombres) as nombre,G.fechaini,";
                query = query + " G.fechafin,A.estane,A.riesgo,A.gratifica,H.desestane";
                query = query + " from dataplan A  ";
                query = query + " left join perplan B on A.idperplan=B.idperplan and A.idcia=B.idcia ";
                query = query + " left join maesgen D on D.idmaesgen='002' and B.tipdoc=D.clavemaesgen ";
                query = query + " left join view_perlab F on B.idcia=F.idcia and B.idperplan=F.idperplan ";
                query = query + " left join perlab G on F.idcia=G.idcia and F.idperplan=G.idperplan and F.idperlab=G.idperlab ";
                query = query + " left join estane H on A.emplea=H.codemp and A.estane=H.idestane and A.idcia=H.idciafile ";
                query = query + " where A.idcia='" + @idcia + "' and idtipocal='" + @idtipocal + "' ";
                query = query + " and A.idtipoper='" + @idtipoper + "' and A.messem='" + @messem + "' ";
                query = query + " and A.anio='" + @anio + "' and A.emplea='" + @rucemp + "' ";
                query = query + " and A.idtipoplan='" + @idtipoplan + "'  " + @cadenaBusqueda;
                query = query + " order by A.iddataplan desc;";
                SqlConnection conexion = new SqlConnection();
                conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
                conexion.Open();
                try
                {
                    using (SqlDataAdapter myDataAdapter = new SqlDataAdapter(query, conexion))
                    {
                        DataSet myDataSet = new DataSet();
                        myDataAdapter.Fill(myDataSet, "tblDataPlan");
                        funciones.formatearDataGridView(dgvdetalle);

                        dgvdetalle.DataSource = myDataSet.Tables["tblDataPlan"];
                        dgvdetalle.Columns[0].HeaderText = "Cód.Int.";
                        dgvdetalle.Columns[1].HeaderText = "Doc. Ident.";
                        dgvdetalle.Columns[2].HeaderText = "Nro.Doc.";
                        dgvdetalle.Columns[3].HeaderText = "Apellidos y Nombres";
                        dgvdetalle.Columns[4].HeaderText = "F.Ini.Lab.";
                        dgvdetalle.Columns[5].HeaderText = "F.Fin.Lab.";
                        dgvdetalle.Columns[6].HeaderText = "Estab.";
                        dgvdetalle.Columns[7].HeaderText = "% SCTR";
                        dgvdetalle.Columns[8].HeaderText = "Gratificación";
                        dgvdetalle.Columns["desestane"].Visible = false;
                        dgvdetalle.Columns[0].Width = 50;
                        dgvdetalle.Columns[1].Width = 50;
                        dgvdetalle.Columns[2].Width = 75;
                        dgvdetalle.Columns[3].Width = 200;
                        dgvdetalle.Columns[4].Width = 75;
                        dgvdetalle.Columns[5].Width = 75;
                        dgvdetalle.Columns[6].Width = 40;
                        dgvdetalle.Columns[7].Width = 40;
                        dgvdetalle.Columns[8].Width = 120;
                        dgvdetalle.RowHeadersVisible = true;
                        if (dgvdetalle.Rows.Count > 0)
                        {
                            for (int r = 0; r < dgvdetalle.Rows.Count; r++)
                            {
                                this.dgvdetalle.Rows[r].HeaderCell.Value = (r + 1).ToString();
                            }
                        }
                        dgvdetalle.AutoResizeRowHeadersWidth(DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders);
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

        private void btnSalir_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}