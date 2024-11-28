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
    public partial class ui_ingdatosplanilla : Form
    {
        Funciones funciones = new Funciones();
        GlobalVariables variables = new GlobalVariables();

        public ui_ingdatosplanilla()
        {
            InitializeComponent();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void txtFechaIni_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }

        private void txtFechaFin_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void ui_ingdatosplanilla_Load(object sender, EventArgs e)
        {
            string idcia = variables.getValorCia();
            string squery;

            //  squery = "SELECT idtipoper as clave,destipoper as descripcion FROM tipoper order by 1 asc";
            squery = "call sp_Pla_SelecTipooper(' ')";
            funciones.listaComboBox(squery, cmbTipoTrabajador, "");
            //  squery = "SELECT ruccia as clave,descia as descripcion FROM ciafile WHERE idcia='" + @idcia + "';";
            squery = "call sp_devuelveCia('" + @idcia + "')";
            funciones.listaComboBox(squery, cmbEmpleador, "");
            //  squery = "SELECT idtipoplan as clave,destipoplan as descripcion FROM tipoplan where idtipoplan in (select idtipoplan from reglabcia where idcia='" + @idcia + "') order by 1 asc;";
            squery = "call planes('" + @idcia + "');";
            funciones.listaComboBox(squery, cmbTipoPlan, "");
            string idtipoplan = funciones.getValorComboBox(cmbTipoPlan, 3);
            ui_listaTipoCal(idcia, idtipoplan);
            ui_ListaDataPlan();
        }

        private void ui_listaTipoCal(string idcia, string idtipoplan)
        {
            string query;
            query = " SELECT idtipocal as clave,destipocal as descripcion ";
            query = query + " FROM tipocal where idtipocal not in ('G','B') and idtipocal in (select idtipocal ";
            query = query + " from calcia where idcia='" + @idcia + "' and idtipoplan='" + @idtipoplan + "') ";
            query = query + " order by ordentipocal asc;";
            funciones.listaComboBox(query, cmbTipoCal, "");
        }

        private void ui_ListaDataPlan()
        {
            string idcia = variables.getValorCia();
            string idtipoper = funciones.getValorComboBox(cmbTipoTrabajador, 1);
            string idtipoplan = funciones.getValorComboBox(cmbTipoPlan, 3);
            string periodo = txtMesSem.Text.Trim() + funciones.replicateCadena(" ", 6);
            string messem = periodo.Substring(0, 2);
            string anio = periodo.Substring(3, 4);
            string idtipocal = funciones.getValorComboBox(cmbTipoCal, 1);
            string rucemp = funciones.getValorComboBox(cmbEmpleador, 11);
            string query;
            //string query =  " select B.idperplan,D.Parm1maesgen as cortotipodoc,B.nrodoc,";
            //query = query + " CONCAT(B.apepat,' ',B.apemat,' , ',B.nombres) as nombre,G.fechaini, ";
            //query = query + " G.fechafin,A.estane,A.riesgo,A.diurno,A.nocturno,A.diasefelab,A.diasdom ,";
            //query = query + " A.diassubsi,A.diasnosubsi,A.diastotal,A.hext25,A.hext35,A.hext100, ";
            //query = query + " A.finivac,A.ffinvac,A.diasvac,A.candes,A.impdes,H.desestane,A.regvac,A.pervac,A.automatic ";
            //query = query + " from dataplan A  ";
            //query = query + " left join perplan B on A.idperplan=B.idperplan and A.idcia=B.idcia ";
            //query = query + " left join maesgen D on D.idmaesgen='002' and B.tipdoc=D.clavemaesgen ";
            //query = query + " left join view_perlab F on B.idcia=F.idcia and B.idperplan=F.idperplan ";
            //query = query + " left join perlab G on F.idcia=G.idcia and F.idperplan=G.idperplan and F.idperlab=G.idperlab ";
            //query = query + " left join estane H on A.emplea=H.codemp and A.estane=H.idestane and A.idcia=H.idciafile ";
            //query = query + " where A.idcia='" + @idcia + "' and idtipocal='" + @idtipocal + "' ";
            //query = query + " and A.idtipoper='" + @idtipoper + "' and A.messem='" + @messem + "' ";
            //query = query + " and A.anio='" + @anio + "' and A.emplea='" + @rucemp + "' ";
            //query = query + " and A.idtipoplan='" + @idtipoplan + "'  ";
            //query = query + " order by A.iddataplan desc;";

            query = "call Ho_ListaDatosPlanilla('" + @idcia + "','" + @idtipocal + "','" + @idtipoper + "','" + @messem + "','" + @anio + "','" + @rucemp + "','" + @idtipoplan + "')";

            loadqueryDatos(query, idtipocal);
        }

        private void loadqueryDatos(string query, string idtipocal)
        {
            string variable = "";
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

                    dgvdetalle.Columns[8].HeaderText = "Dias Diurnos";
                    dgvdetalle.Columns[9].HeaderText = "Dias Nocturnos";
                    dgvdetalle.Columns[10].HeaderText = "Minut. Tardanzas";
                    dgvdetalle.Columns[11].HeaderText = "Total hor. Lab.";
                    dgvdetalle.Columns[12].HeaderText = "Dias Efec. Lab.";
                    dgvdetalle.Columns[13].HeaderText = "Dias Dom.";
                    dgvdetalle.Columns[14].HeaderText = "Dias Subsi.";
                    dgvdetalle.Columns[15].HeaderText = "Dias No Subsi.";
                    dgvdetalle.Columns[16].HeaderText = "Total Dias";

                    dgvdetalle.Columns[17].HeaderText = "H.Ext. 25%";
                    dgvdetalle.Columns[18].HeaderText = "H.Ext. 35%";
                    dgvdetalle.Columns[19].HeaderText = "H.Ext. 100%";

                    dgvdetalle.Columns[20].HeaderText = "F.Inicio Vac.";
                    dgvdetalle.Columns[21].HeaderText = "F.Fin Vac.";
                    dgvdetalle.Columns[22].HeaderText = "Días Vac.";

                    dgvdetalle.Columns[23].HeaderText = "Cantidad Destajo";
                    dgvdetalle.Columns[24].HeaderText = "Importe Destajo";

                    dgvdetalle.Columns["idperplan"].Frozen = true;
                    dgvdetalle.Columns["cortotipodoc"].Frozen = true;
                    dgvdetalle.Columns["nrodoc"].Frozen = true;
                    dgvdetalle.Columns["nombre"].Frozen = true;

                    dgvdetalle.Columns["desestane"].Visible = false;
                    dgvdetalle.Columns["regvac"].Visible = false;
                    dgvdetalle.Columns["pervac"].Visible = false;

                    dgvdetalle.Columns["ntardanzas"].Visible = false;
                    dgvdetalle.Columns["nhoras"].Visible = false;

                    if (idtipocal.Equals("V"))
                    {
                        dgvdetalle.Columns["riesgo"].Visible = false;
                        dgvdetalle.Columns["diurno"].Visible = false;
                        dgvdetalle.Columns["nocturno"].Visible = false;
                        dgvdetalle.Columns["diasefelab"].Visible = false;
                        dgvdetalle.Columns["diasdom"].Visible = false;
                        dgvdetalle.Columns["diassubsi"].Visible = false;
                        dgvdetalle.Columns["diasnosubsi"].Visible = false;
                        dgvdetalle.Columns["diastotal"].Visible = false;
                        dgvdetalle.Columns["hext25"].Visible = false;
                        dgvdetalle.Columns["hext35"].Visible = false;
                        dgvdetalle.Columns["hext100"].Visible = false;
                        dgvdetalle.Columns["candes"].Visible = false;
                        dgvdetalle.Columns["impdes"].Visible = false;
                    }
                    else
                    {
                        dgvdetalle.Columns["riesgo"].Visible = true;
                        dgvdetalle.Columns["diurno"].Visible = true;
                        dgvdetalle.Columns["nocturno"].Visible = true;
                        dgvdetalle.Columns["diasefelab"].Visible = true;
                        dgvdetalle.Columns["diasdom"].Visible = true;
                        dgvdetalle.Columns["diassubsi"].Visible = true;
                        dgvdetalle.Columns["diasnosubsi"].Visible = true;
                        dgvdetalle.Columns["diastotal"].Visible = true;
                        dgvdetalle.Columns["hext25"].Visible = true;
                        dgvdetalle.Columns["hext35"].Visible = true;
                        dgvdetalle.Columns["hext100"].Visible = true;
                        dgvdetalle.Columns["candes"].Visible = true;
                        dgvdetalle.Columns["impdes"].Visible = true;
                    }

                    dgvdetalle.Columns[0].Width = 50;
                    dgvdetalle.Columns[1].Width = 50;
                    dgvdetalle.Columns[2].Width = 75;
                    dgvdetalle.Columns[3].Width = 200;
                    dgvdetalle.Columns[4].Width = 75;
                    dgvdetalle.Columns[5].Width = 75;
                    dgvdetalle.Columns[6].Width = 40;
                    dgvdetalle.Columns[7].Width = 40;
                    dgvdetalle.Columns[8].Width = 60;
                    dgvdetalle.Columns[9].Width = 60;
                    dgvdetalle.Columns[10].Width = 50;
                    dgvdetalle.Columns[11].Width = 50;
                    dgvdetalle.Columns[12].Width = 50;
                    dgvdetalle.Columns[13].Width = 50;
                    dgvdetalle.Columns[14].Width = 50;
                    dgvdetalle.Columns[15].Width = 50;
                    dgvdetalle.Columns[16].Width = 50;
                    dgvdetalle.Columns[17].Width = 50;
                    dgvdetalle.Columns[18].Width = 75;
                    dgvdetalle.Columns[19].Width = 75;
                    dgvdetalle.Columns[20].Width = 65;
                    dgvdetalle.Columns[21].Width = 60;
                    dgvdetalle.Columns[22].Width = 60;
                    dgvdetalle.Columns[23].Width = 60;
                    dgvdetalle.Columns[24].Width = 60;

                    dgvdetalle.RowHeadersVisible = true;

                    if (dgvdetalle.Rows.Count > 0)
                    {
                        for (int r = 0; r < dgvdetalle.Rows.Count; r++)
                        {
                            this.dgvdetalle.Rows[r].HeaderCell.Value = (r + 1).ToString();
                            variable = dgvdetalle.Rows[r].Cells["automatic"].Value.ToString();
                            if (variable == "P")
                            {
                                dgvdetalle.Rows[r].DefaultCellStyle.BackColor = Color.Green;
                            }
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
                string clave = funciones.getValorComboBox(cmbTipoTrabajador, 1);
                string squery;
                if (cmbTipoTrabajador.Text != String.Empty)
                {
                    squery = "SELECT idtipoper as clave,destipoper as descripcion FROM tipoper WHERE idtipoper='" + @clave + "';";
                    funciones.validarCombobox(squery, cmbTipoTrabajador);
                }
                lblPeriodo.Visible = false;
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
                CalPlan calplan = new CalPlan();
                string idcia = variables.getValorCia();
                string idtipoper = funciones.getValorComboBox(cmbTipoTrabajador, 1);
                string periodo = txtMesSem.Text.Trim() + funciones.replicateCadena(" ", 6);
                string messem = periodo.Substring(0, 2);
                string anio = periodo.Substring(3, 4);
                string idtipocal = funciones.getValorComboBox(cmbTipoCal, 1);
                if (calplan.getDatosCalPlan(messem, anio, idtipoper, idcia, idtipocal, "FECHAINI") != "")
                {
                    txtFechaIni.Text = calplan.getDatosCalPlan(messem, anio, idtipoper, idcia, idtipocal, "FECHAINI");
                    txtFechaFin.Text = calplan.getDatosCalPlan(messem, anio, idtipoper, idcia, idtipocal, "FECHAFIN");
                    lblPeriodo.Visible = true;

                    if (calplan.getDatosCalPlan(messem, anio, idtipoper, idcia, idtipocal, "ESTADO") == "V")
                    {
                        lblPeriodo.Text = "Periodo Laboral Vigente";
                    }
                    else
                    {
                        lblPeriodo.Text = "Periodo Laboral Cerrado";
                    }
                }
                else
                {
                    MessageBox.Show("El Periodo Laboral no existe", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    lblPeriodo.Visible = false;
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
            string idcia = variables.getValorCia();
            string squery = "SELECT ruccia as clave,descia as descripcion FROM ciafile WHERE idcia='" + @idcia + "';";
            funciones.listaComboBox(squery, cmbEmpleador, "");
            cmbEmpleador.Enabled = false;
            ui_ListaDataPlan();
        }

        private void radioButtonSiEmp_CheckedChanged(object sender, EventArgs e)
        {
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
            CalPlan calplan = new CalPlan();
            string idcia = variables.getValorCia();
            string idtipoper = funciones.getValorComboBox(cmbTipoTrabajador, 1);
            string empleador = cmbEmpleador.Text.Trim();
            string periodo = txtMesSem.Text.Trim() + funciones.replicateCadena(" ", 6);
            string messem = periodo.Substring(0, 2);
            string anio = periodo.Substring(3, 4);
            string fechaini = txtFechaIni.Text;
            string fechafin = txtFechaFin.Text;
            string idtipoplan = funciones.getValorComboBox(cmbTipoPlan, 3);
            string idtipocal = funciones.getValorComboBox(cmbTipoCal, 1);

            if (calplan.getDatosCalPlan(messem, anio, idtipoper, idcia, idtipocal, "FECHAINI") != "")
            {
                if (empleador != string.Empty)
                {
                    ui_upddatosplanilla ui_detalle = new ui_upddatosplanilla();
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
            Int32 selectedCellCount = dgvdetalle.GetCellCount(DataGridViewElementStates.Selected);
            if (selectedCellCount > 0)
            {
                string idperplan = dgvdetalle.Rows[dgvdetalle.SelectedCells[1].RowIndex].Cells[0].Value.ToString();
                string idestane = dgvdetalle.Rows[dgvdetalle.SelectedCells[1].RowIndex].Cells[6].Value.ToString();
                string riesgo = dgvdetalle.Rows[dgvdetalle.SelectedCells[1].RowIndex].Cells[7].Value.ToString();

                string diasdiurnos = dgvdetalle.Rows[dgvdetalle.SelectedCells[1].RowIndex].Cells[8].Value.ToString();
                string diasnocturnos = dgvdetalle.Rows[dgvdetalle.SelectedCells[1].RowIndex].Cells[9].Value.ToString();

                string ntardanzas = dgvdetalle.Rows[dgvdetalle.SelectedCells[1].RowIndex].Cells[10].Value.ToString();
                string nhoras = dgvdetalle.Rows[dgvdetalle.SelectedCells[1].RowIndex].Cells[11].Value.ToString();
                string diasefelab = dgvdetalle.Rows[dgvdetalle.SelectedCells[1].RowIndex].Cells[12].Value.ToString();
                string diasdom = dgvdetalle.Rows[dgvdetalle.SelectedCells[1].RowIndex].Cells[13].Value.ToString();

                string diassubsi = dgvdetalle.Rows[dgvdetalle.SelectedCells[1].RowIndex].Cells[14].Value.ToString();
                string diasnosubsi = dgvdetalle.Rows[dgvdetalle.SelectedCells[1].RowIndex].Cells[15].Value.ToString();
                string diastotal = dgvdetalle.Rows[dgvdetalle.SelectedCells[1].RowIndex].Cells[16].Value.ToString();

                string hext25 = dgvdetalle.Rows[dgvdetalle.SelectedCells[1].RowIndex].Cells[17].Value.ToString();
                string hext35 = dgvdetalle.Rows[dgvdetalle.SelectedCells[1].RowIndex].Cells[18].Value.ToString();
                string hext100 = dgvdetalle.Rows[dgvdetalle.SelectedCells[1].RowIndex].Cells[19].Value.ToString();

                string finivac = dgvdetalle.Rows[dgvdetalle.SelectedCells[1].RowIndex].Cells[20].Value.ToString();
                string ffinvac = dgvdetalle.Rows[dgvdetalle.SelectedCells[1].RowIndex].Cells[21].Value.ToString();
                string diasvac = dgvdetalle.Rows[dgvdetalle.SelectedCells[1].RowIndex].Cells[22].Value.ToString();
                string candes = dgvdetalle.Rows[dgvdetalle.SelectedCells[1].RowIndex].Cells[23].Value.ToString();
                string impdes = dgvdetalle.Rows[dgvdetalle.SelectedCells[1].RowIndex].Cells[24].Value.ToString();
                string regvac = dgvdetalle.Rows[dgvdetalle.SelectedCells[1].RowIndex].Cells[26].Value.ToString();
                string pervac = dgvdetalle.Rows[dgvdetalle.SelectedCells[1].RowIndex].Cells[27].Value.ToString();

                string establecimiento = dgvdetalle.Rows[dgvdetalle.SelectedCells[1].RowIndex].Cells[6].Value.ToString() + "  " + dgvdetalle.Rows[dgvdetalle.SelectedCells[1].RowIndex].Cells[25].Value.ToString();

                string idtipoper = funciones.getValorComboBox(cmbTipoTrabajador, 1);
                string idtipoplan = funciones.getValorComboBox(cmbTipoPlan, 3);
                string idtipocal = funciones.getValorComboBox(cmbTipoCal, 1);
                string empleador = cmbEmpleador.Text.Trim();
                string periodo = txtMesSem.Text.Trim() + funciones.replicateCadena(" ", 6);
                string messem = periodo.Substring(0, 2);
                string anio = periodo.Substring(3, 4);
                string fechaini = txtFechaIni.Text;
                string fechafin = txtFechaFin.Text;

                ui_upddatosplanilla ui_detalle = new ui_upddatosplanilla();
                ui_detalle._FormPadre = this;
                ui_detalle.setValores(variables.getValorCia(), idtipoper, empleador, anio, messem, idtipocal, fechaini, fechafin, idtipoplan);
                ui_detalle.Activate();
                ui_detalle.ui_loadDatosPlanilla(idperplan, establecimiento, riesgo, diasefelab, diassubsi, diasnosubsi, diastotal, idestane, hext25, hext35, hext100, finivac, ffinvac, diasvac, candes, impdes, diasdiurnos, diasnocturnos, diasdom, regvac, pervac, nhoras, ntardanzas);

                ui_detalle.BringToFront();
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
            ui_ListaDataPlan();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            DataPlan dataplan = new DataPlan();
            Int32 selectedCellCount = dgvdetalle.GetCellCount(DataGridViewElementStates.Selected);
            if (selectedCellCount > Convert.ToInt32(0))
            {
                string idperplan = dgvdetalle.Rows[dgvdetalle.SelectedCells[1].RowIndex].Cells[0].Value.ToString();
                string trabajador = dgvdetalle.Rows[dgvdetalle.SelectedCells[1].RowIndex].Cells[3].Value.ToString();
                string idtipoper = funciones.getValorComboBox(cmbTipoTrabajador, 1);
                string periodo = txtMesSem.Text.Trim() + funciones.replicateCadena(" ", 6);
                string messem = periodo.Substring(0, 2);
                string anio = periodo.Substring(3, 4);
                string idtipoplan = funciones.getValorComboBox(cmbTipoPlan, 3);
                string idtipocal = funciones.getValorComboBox(cmbTipoCal, 1);

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
                MessageBox.Show("No ha seleccionado registro a Eliminar", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void cmbTipoPlan_SelectedIndexChanged(object sender, EventArgs e)
        {
            string idtipoplan = funciones.getValorComboBox(cmbTipoPlan, 3);
            string idcia = variables.getValorCia();
            lblPeriodo.Visible = false;
            txtMesSem.Clear();
            txtFechaIni.Clear();
            txtFechaFin.Clear();
            txtMesSem.Focus();
            ui_listaTipoCal(idcia, idtipoplan);
            ui_ListaDataPlan();
        }

        private void cmbTipoCal_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblPeriodo.Visible = false;
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
                string idcia = variables.getValorCia();
                string idtipoper = funciones.getValorComboBox(cmbTipoTrabajador, 1);
                string idtipoplan = funciones.getValorComboBox(cmbTipoPlan, 3);
                string periodo = txtMesSem.Text.Trim() + funciones.replicateCadena(" ", 6);
                string messem = periodo.Substring(0, 2);
                string anio = periodo.Substring(3, 4);
                string idtipocal = funciones.getValorComboBox(cmbTipoCal, 1);
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

                string query = " select B.idperplan,D.Parm1maesgen as cortotipodoc,B.nrodoc,";
                query += " CONCAT(B.apepat,' ',B.apemat,' , ',B.nombres) as nombre,G.fechaini, ";
                query += " G.fechafin,A.estane,A.riesgo,A.diurno,A.nocturno,A.ntardanzas,A.nhoras,A.diasefelab,A.diasdom ,";
                query += " A.diassubsi,A.diasnosubsi,A.diastotal,A.hext25,A.hext35,A.hext100, ";
                query += " A.finivac,A.ffinvac,A.diasvac,A.candes,A.impdes,H.desestane,A.regvac,A.pervac,A.automatic";
                query += "  from dataplan A  ";
                query += " left join perplan B on A.idperplan=B.idperplan and A.idcia=B.idcia ";
                query += " left join maesgen D on D.idmaesgen='002' and B.tipdoc=D.clavemaesgen ";
                query += " left join view_perlab F on B.idcia=F.idcia and B.idperplan=F.idperplan ";
                query += " left join perlab G on F.idcia=G.idcia and F.idperplan=G.idperplan and F.idperlab=G.idperlab ";
                query += " left join estane H on A.emplea=H.codemp and A.estane=H.idestane and A.idcia=H.idciafile ";
                query += " where A.idcia='" + @idcia + "' and idtipocal='" + @idtipocal + "' ";
                query += " and A.idtipoper='" + @idtipoper + "' and A.messem='" + @messem + "' ";
                query += " and A.anio='" + @anio + "' and A.emplea='" + @rucemp + "' ";
                query += " and A.idtipoplan='" + @idtipoplan + "'  " + @cadenaBusqueda;
                query += " order by A.iddataplan desc;";

                loadqueryDatos(query, idtipocal);
                /*
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

                        dgvdetalle.Columns[8].HeaderText = "Dias Diurnos";
                        dgvdetalle.Columns[9].HeaderText = "Dias Nocturnos";
                        dgvdetalle.Columns[10].HeaderText = "Dias Efec. Lab.";
                        dgvdetalle.Columns[11].HeaderText = "Dias Dom.";
                        dgvdetalle.Columns[12].HeaderText = "Dias Subsi.";
                        dgvdetalle.Columns[13].HeaderText = "Dias No Subsi.";
                        dgvdetalle.Columns[14].HeaderText = "Total Dias";


                        dgvdetalle.Columns[15].HeaderText = "H.Ext. 25%";
                        dgvdetalle.Columns[16].HeaderText = "H.Ext. 35%";
                        dgvdetalle.Columns[17].HeaderText = "H.Ext. 100%";

                        dgvdetalle.Columns[18].HeaderText = "F.Inicio Vac.";
                        dgvdetalle.Columns[19].HeaderText = "F.Fin Vac.";
                        dgvdetalle.Columns[20].HeaderText = "Días Vac.";

                        dgvdetalle.Columns[21].HeaderText = "Cantidad Destajo";
                        dgvdetalle.Columns[22].HeaderText = "Importe Destajo";

                        dgvdetalle.Columns["idperplan"].Frozen = true;
                        dgvdetalle.Columns["cortotipodoc"].Frozen = true;
                        dgvdetalle.Columns["nrodoc"].Frozen = true;
                        dgvdetalle.Columns["nombre"].Frozen = true;


                        dgvdetalle.Columns["desestane"].Visible = false;
                        dgvdetalle.Columns["regvac"].Visible = false;

                        if (idtipocal.Equals("V"))
                        {
                            dgvdetalle.Columns["riesgo"].Visible = false;
                            dgvdetalle.Columns["diurno"].Visible = false;
                            dgvdetalle.Columns["nocturno"].Visible = false;
                            dgvdetalle.Columns["diasefelab"].Visible = false;
                            dgvdetalle.Columns["diasdom"].Visible = false;
                            dgvdetalle.Columns["diassubsi"].Visible = false;
                            dgvdetalle.Columns["diasnosubsi"].Visible = false;
                            dgvdetalle.Columns["diastotal"].Visible = false;
                            dgvdetalle.Columns["hext25"].Visible = false;
                            dgvdetalle.Columns["hext35"].Visible = false;
                            dgvdetalle.Columns["hext100"].Visible = false;
                            dgvdetalle.Columns["candes"].Visible = false;
                            dgvdetalle.Columns["impdes"].Visible = false;
                        }
                        else
                        {
                            dgvdetalle.Columns["riesgo"].Visible = true;
                            dgvdetalle.Columns["diurno"].Visible = true;
                            dgvdetalle.Columns["nocturno"].Visible = true;
                            dgvdetalle.Columns["diasefelab"].Visible = true;
                            dgvdetalle.Columns["diasdom"].Visible = true;
                            dgvdetalle.Columns["diassubsi"].Visible = true;
                            dgvdetalle.Columns["diasnosubsi"].Visible = true;
                            dgvdetalle.Columns["diastotal"].Visible = true;
                            dgvdetalle.Columns["hext25"].Visible = true;
                            dgvdetalle.Columns["hext35"].Visible = true;
                            dgvdetalle.Columns["hext100"].Visible = true;
                            dgvdetalle.Columns["candes"].Visible = true;
                            dgvdetalle.Columns["impdes"].Visible = true;
                        }

                        dgvdetalle.Columns[0].Width = 50;
                        dgvdetalle.Columns[1].Width = 50;
                        dgvdetalle.Columns[2].Width = 75;
                        dgvdetalle.Columns[3].Width = 200;
                        dgvdetalle.Columns[4].Width = 75;
                        dgvdetalle.Columns[5].Width = 75;
                        dgvdetalle.Columns[6].Width = 40;
                        dgvdetalle.Columns[7].Width = 40;
                        dgvdetalle.Columns[8].Width = 60;
                        dgvdetalle.Columns[9].Width = 60;
                        dgvdetalle.Columns[10].Width = 50;
                        dgvdetalle.Columns[11].Width = 50;
                        dgvdetalle.Columns[12].Width = 50;
                        dgvdetalle.Columns[13].Width = 50;
                        dgvdetalle.Columns[14].Width = 50;
                        dgvdetalle.Columns[15].Width = 50;
                        dgvdetalle.Columns[16].Width = 50;
                        dgvdetalle.Columns[17].Width = 50;
                        dgvdetalle.Columns[18].Width = 75;
                        dgvdetalle.Columns[19].Width = 75;
                        dgvdetalle.Columns[20].Width = 65;
                        dgvdetalle.Columns[21].Width = 60;
                        dgvdetalle.Columns[22].Width = 60;

                        dgvdetalle.RowHeadersVisible = true;

                        if (dgvdetalle.Rows.Count > 0)
                        {
                            for (int r = 0; r < dgvdetalle.Rows.Count; r++)
                            {
                                this.dgvdetalle.Rows[r].HeaderCell.Value = (r + 1].ToString();
                                variable = dgvdetalle.Rows[r].Cells["automatic"].Value.ToString();
                                if (variable == "P")
                                {
                                    dgvdetalle.Rows[r].DefaultCellStyle.BackColor = Color.Green;
                                }
                            }
                        }
                        dgvdetalle.AutoResizeRowHeadersWidth(DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }

                conexion.Close();*/
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

        private void txtMesSem_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }
    }
}