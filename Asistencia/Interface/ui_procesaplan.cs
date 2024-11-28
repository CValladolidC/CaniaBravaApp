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
using System.Diagnostics;

namespace CaniaBrava
{
    public partial class ui_procesaplan : Form
    {
        string _ejecucion;

        public ui_procesaplan()
        {
            InitializeComponent();
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            // This event handler is called when the background thread finishes.
            // This method runs on the main thread.
            if (e.Error != null)
            {
                MessageBox.Show("Error: " + e.Error.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                if (e.Cancelled)
                {
                    MessageBox.Show("Proceso Cancelado", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                    this._ejecucion = "NO";
                }
                else
                {
                    MessageBox.Show("Proceso Finalizado con éxito", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    btnActualizar.Enabled = true; btnCancelar.Enabled = true; btnSalir.Enabled = true;
                    this._ejecucion = "NO";
                    ui_ListaCalPlan();
                }
            }

            pgbAvance.Value = 0;
            pgbAvance.Visible = false;

        }

        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            // This method runs on the main thread.
            ProcesaPlan.CurrentState state =
                (ProcesaPlan.CurrentState)e.UserState;
            pgbAvance.Visible = true;
            pgbAvance.Value = state.LinesCounted;
            pgbAvance.Maximum = state.TotalLines;

        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            // This event handler is where the actual work is done.
            // This method runs on the background thread.

            // Get the BackgroundWorker object that raised this event.
            System.ComponentModel.BackgroundWorker worker;
            worker = (System.ComponentModel.BackgroundWorker)sender;

            // Get the Words object and call the main method.
            ProcesaPlan WC = (ProcesaPlan)e.Argument;
            WC.procesa(worker, e);

        }

        private void StartThread(string idcia, string idtipoper, string idtipoplan, string messem,
        string anio, string idtipocal, string rucemp, string estane, string diasperlab, string tipoproceso)
        {

            ProcesaPlan WC = new ProcesaPlan();
            WC.diasperlab = diasperlab;
            WC.idcia = idcia;
            WC.idtipocal = idtipocal;
            WC.idtipoper = idtipoper;
            WC.messem = messem;
            WC.anio = anio;
            WC.rucemp = rucemp;
            WC.estane = estane;
            WC.idtipoplan = idtipoplan;
            WC.tipoproceso = tipoproceso;
            backgroundWorker1.RunWorkerAsync(WC);

        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            if (this._ejecucion.Equals("NO"))
            {
                this.Close();
            }
            else
            {
                MessageBox.Show("Existe un proceso en ejecución, para salir primero debe cancelarlo", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void ui_ListaCalPlan()
        {
            try
            {
                Funciones funciones = new Funciones();
                GlobalVariables variables = new GlobalVariables();
                string idcia = variables.getValorCia();
                string idtipoper = funciones.getValorComboBox(cmbTipoTrabajador, 1);
                string idtipocal = funciones.getValorComboBox(cmbTipoCal, 1);

                SqlConnection conexion = new SqlConnection();
                conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
                conexion.Open();
                string query = " select concat(A.messem,'/',A.anio) as periodo,A.fechaini,A.fechafin,CASE A.estado WHEN 'C' THEN 'CERRADO' WHEN 'V' THEN '' END  as estado";
                query = query + " from calplan A left join maesgen C on ";
                query = query + " A.mes=C.clavemaesgen and C.idmaesgen='035' ";
                query = query + " where A.idcia='" + @idcia + "' and A.idtipoper='" + @idtipoper + "' ";
                query = query + " and A.idtipocal='" + @idtipocal + "'  order by anio desc,messem desc;";

                try
                {
                    using (SqlDataAdapter myDataAdapter = new SqlDataAdapter(query, conexion))
                    {
                        DataSet myDataSet = new DataSet();
                        myDataAdapter.Fill(myDataSet, "tblCalPlan");
                        funciones.formatearDataGridView(dgvdetalle);

                        dgvdetalle.DataSource = myDataSet.Tables["tblCalPlan"];
                        dgvdetalle.Columns[0].HeaderText = "Periodo";
                        dgvdetalle.Columns[1].HeaderText = "Fecha Inicio";
                        dgvdetalle.Columns[2].HeaderText = "Fecha Fin";
                        dgvdetalle.Columns[3].HeaderText = "Estado";


                        dgvdetalle.Columns[0].Width = 60;
                        dgvdetalle.Columns[1].Width = 90;
                        dgvdetalle.Columns[2].Width = 90;
                        dgvdetalle.Columns[3].Width = 100;



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

        private void ui_procesaplan_Load(object sender, EventArgs e)
        {
            this._ejecucion = "NO";

            Funciones funciones = new Funciones();
            GlobalVariables variables = new GlobalVariables();
            string idcia = variables.getValorCia();
            string squery = "SELECT idtipoper as clave,destipoper as descripcion FROM tipoper order by 1 asc";
            funciones.listaComboBox(squery, cmbTipoTrabajador, "");
            squery = "SELECT ruccia as clave,descia as descripcion FROM ciafile WHERE idcia='" + @idcia + "';";
            funciones.listaComboBox(squery, cmbEmpleador, "");
            squery = "SELECT idtipoplan as clave,destipoplan as descripcion FROM tipoplan where idtipoplan in (select idtipoplan from reglabcia where idcia='" + @idcia + "') order by 1 asc;";
            funciones.listaComboBox(squery, cmbTipoPlan, "");
            string idtipoplan = funciones.getValorComboBox(cmbTipoPlan, 3);
            ui_listaTipoCal(idcia, idtipoplan);
            cmbTipoProceso.Text = "P    CALCULO PREVIO";
            ui_ListaCalPlan();

        }

        private void cmbTipoPlan_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void txtMesSem_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                Funciones funciones = new Funciones();
                CalPlan calplan = new CalPlan();
                GlobalVariables globalVariable = new GlobalVariables();

                string idcia = globalVariable.getValorCia();
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
                    MessageBox.Show("El perido Laboral no existe", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    lblPeriodo.Visible = false;
                    txtFechaIni.Clear();
                    txtFechaFin.Clear();
                    e.Handled = true;
                    txtMesSem.Focus();
                }


            }
        }

        private void ui_listaTipoCal(string idcia, string idtipoplan)
        {
            Funciones funciones = new Funciones();
            string query;
            query = " SELECT idtipocal as clave,destipocal as descripcion ";
            query = query + " FROM tipocal where idtipocal in (select idtipocal ";
            query = query + " from calcia where idcia='" + @idcia + "' and idtipoplan='" + @idtipoplan + "') ";
            query = query + " order by ordentipocal asc;";
            funciones.listaComboBox(query, cmbTipoCal, "");
        }

        private void radioButtonSiEmp_CheckedChanged(object sender, EventArgs e)
        {
            Funciones funciones = new Funciones();
            GlobalVariables globalVariable = new GlobalVariables();
            string idcia = globalVariable.getValorCia();
            string squery = "SELECT rucemp as clave,razonemp as descripcion FROM emplea WHERE idciafile='" + @idcia + "' order by rucemp asc;";
            funciones.listaComboBox(squery, cmbEmpleador, "");
            cmbEmpleador.Enabled = true;
            string ruccia = funciones.getValorComboBox(cmbEmpleador, 11);
            string squery_esta = "SELECT idestane as clave,desestane as descripcion FROM estane WHERE codemp='" + @ruccia + "' and idciafile='" + @idcia + "' order by idestane asc;";
            funciones.listaComboBox(squery_esta, cmbEstablecimiento, "X");
            cmbEmpleador.Focus();
        }

        private void radioButtonNoEmp_CheckedChanged(object sender, EventArgs e)
        {
            Funciones funciones = new Funciones();
            GlobalVariables globalVariable = new GlobalVariables();
            string idcia = globalVariable.getValorCia();
            string ruccia = globalVariable.getValorRucCia();
            string squery = "SELECT ruccia as clave,descia as descripcion FROM ciafile WHERE idcia='" + @idcia + "';";
            funciones.listaComboBox(squery, cmbEmpleador, "");
            cmbEmpleador.Enabled = false;
            string squery_esta = "SELECT idestane as clave,desestane as descripcion FROM estane WHERE codemp='" + @ruccia + "' and idciafile='" + @idcia + "' order by idestane asc;";
            funciones.listaComboBox(squery_esta, cmbEstablecimiento, "X");
            cmbEstablecimiento.Focus();
        }

        private void cmbTipoTrabajador_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtMesSem.Clear();
            txtFechaIni.Clear();
            txtFechaFin.Clear();
            txtMesSem.Focus();
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            Funciones funciones = new Funciones();
            GlobalVariables globalVariable = new GlobalVariables();
            PerPlan perplan = new PerPlan();
            string idcia = globalVariable.getValorCia();
            string idtipoper = funciones.getValorComboBox(cmbTipoTrabajador, 1);
            string idtipoplan = funciones.getValorComboBox(cmbTipoPlan, 3);
            string periodo = txtMesSem.Text.Trim() + funciones.replicateCadena(" ", 6);
            string messem = periodo.Substring(0, 2);
            string anio = periodo.Substring(3, 4);
            string idtipocal = funciones.getValorComboBox(cmbTipoCal, 1);
            string rucemp = funciones.getValorComboBox(cmbEmpleador, 11);
            string estane = funciones.getValorComboBox(cmbEstablecimiento, 4);
            string tipoproceso = funciones.getValorComboBox(cmbTipoProceso, 1);
            string estado = "V";

            string resultado = "0";
            CalPlan calplan = new CalPlan();
            if (calplan.getDatosCalPlan(messem, anio, idtipoper, idcia, idtipocal, "FECHAINI") != "")
            {
                txtFechaIni.Text = calplan.getDatosCalPlan(messem, anio, idtipoper, idcia, idtipocal, "FECHAINI");
                txtFechaFin.Text = calplan.getDatosCalPlan(messem, anio, idtipoper, idcia, idtipocal, "FECHAFIN");
                estado = calplan.getDatosCalPlan(messem, anio, idtipoper, idcia, idtipocal, "ESTADO");
                if (estado.Equals("V"))
                {
                    resultado = perplan.ui_verificaPerPlan_DataPlan(idcia, idtipocal, idtipoper, messem, anio, rucemp, estane, idtipoplan);
                    if (resultado.Equals("0"))
                    {
                        var opc = MessageBox.Show("¿Realmente desea proceder a calcular la planilla?", "Aviso", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                        if (opc == DialogResult.Yes)
                        {
                            btnActualizar.Enabled = false; btnCancelar.Enabled = false; btnSalir.Enabled = false;
                            string diasperlab = calplan.getDatosCalPlan(messem, anio, idtipoper, idcia, idtipocal, "DIAS_FI_FF");
                            this._ejecucion = "SI";
                            StartThread(idcia, idtipoper, idtipoplan, messem, anio, idtipocal, rucemp, estane, diasperlab, tipoproceso);
                        }
                    }
                }
                else
                {
                    MessageBox.Show("El Periodo Laboral " + messem + "/" + anio + " ya se encuentra CERRADO", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("El Periodo Laboral no existe", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtFechaIni.Clear();
                txtFechaFin.Clear();
                txtMesSem.Focus();
            }
        }

        private void cmbEmpleador_SelectedIndexChanged(object sender, EventArgs e)
        {
            Funciones funciones = new Funciones();
            GlobalVariables variables = new GlobalVariables();
            string idcia = variables.getValorCia();
            string ruccia = funciones.getValorComboBox(cmbEmpleador, 11);
            string squery_esta = "SELECT idestane as clave,desestane as descripcion FROM estane WHERE codemp='" + @ruccia + "' and idciafile='" + @idcia + "' order by idestane asc;";
            funciones.listaComboBox(squery_esta, cmbEstablecimiento, "X");
            cmbEstablecimiento.Focus();
        }

        private void lblprocesando_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {

        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.backgroundWorker1.CancelAsync();

        }

        private void cmbTipoTrabajador_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            txtMesSem.Clear();
            txtFechaIni.Clear();
            txtFechaFin.Clear();
            txtMesSem.Focus();
            lblPeriodo.Visible = false;
            ui_ListaCalPlan();
        }

        private void cmbTipoCal_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtMesSem.Clear();
            txtFechaIni.Clear();
            txtFechaFin.Clear();
            txtMesSem.Focus();
            lblPeriodo.Visible = false;
            ui_ListaCalPlan();
        }

        private void dgvdetalle_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (this.dgvdetalle.Columns[e.ColumnIndex].Name == "estado")
            {
                try
                {
                    // comprobar si la celda tiene contenido válido
                    if (e.Value.GetType() != typeof(System.DBNull))
                    {
                        // si la celda tiene valor y el mes es febrero 
                        // formatear el contenido
                        if ((e.Value).Equals("C"))
                        {

                            e.CellStyle.Format = "D";
                            e.CellStyle.BackColor = Color.Aqua;
                        }
                    }
                }
                catch (NullReferenceException)
                {
                }
            }
        }

        private void dgvdetalle_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void cmbTipoPlan_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            Funciones funciones = new Funciones();
            GlobalVariables variables = new GlobalVariables();
            string idcia = variables.getValorCia();
            string idtipoplan = funciones.getValorComboBox(cmbTipoPlan, 3);
            ui_listaTipoCal(idcia, idtipoplan);
            txtMesSem.Clear();
            txtFechaIni.Clear();
            txtFechaFin.Clear();
            txtMesSem.Focus();
            lblPeriodo.Visible = false;
        }

        private void toolstripform_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }
    }
}