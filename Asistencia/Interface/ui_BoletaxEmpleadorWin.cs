using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using CrystalDecisions.CrystalReports.Engine;
using System.Configuration;

namespace CaniaBrava
{
    public partial class ui_BoletaxEmpleadorWin : Form
    {
        public ui_BoletaxEmpleadorWin()
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
            funciones.listaComboBox(query, cmbTipoCal, "");
        }

        private void ui_BoletaxEmpleadorWin_Load(object sender, EventArgs e)
        {
            Funciones funciones = new Funciones();
            GlobalVariables variables = new GlobalVariables();
            string idcia = variables.getValorCia();
            string squery = "SELECT idtipoper as clave,destipoper as descripcion FROM tipoper order by 1 asc";
            funciones.listaComboBox(squery, cmbTipoTrabajador, "");
            squery = "SELECT ruccia as clave,descia as descripcion FROM ciafile WHERE idcia='" + @idcia + "';";
            funciones.listaComboBox(squery, cmbEmpleador, "");
            squery = "SELECT idtipoplan as clave,destipoplan as descripcion FROM tipoplan where idtipoplan in (select idtipoplan from reglabcia where idcia='" + @variables.getValorCia() + "') order by 1 asc;";
            funciones.listaComboBox(squery, cmbTipoPlan, "");
            string idtipoplan = funciones.getValorComboBox(cmbTipoPlan, 3);
            ui_listaTipoCal(idcia, idtipoplan);
            cmbTipo.Text = "CON LOGOTIPO";
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
                string idtipocal = funciones.getValorComboBox(cmbTipoCal, 1);

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
            }
        }

        private void radioButtonSiEmp_CheckedChanged(object sender, EventArgs e)
        {
            Funciones funciones = new Funciones();
            GlobalVariables variables = new GlobalVariables();
            string idcia = variables.getValorCia();

            string squery = "SELECT rucemp as clave,razonemp as descripcion FROM emplea WHERE idciafile='" + @idcia + "' order by rucemp asc;";
            funciones.listaComboBox(squery, cmbEmpleador, "");
            cmbEmpleador.Enabled = true;

            string ruccia = funciones.getValorComboBox(cmbEmpleador, 11);
            string squery_esta = "SELECT idestane as clave,desestane as descripcion FROM estane WHERE codemp='" + @ruccia + "' and idciafile='" + @idcia + "' order by idestane asc;";
            funciones.listaComboBox(squery_esta, cmbEstablecimiento, "");
            cmbEmpleador.Focus();
        }

        private void radioButtonNoEmp_CheckedChanged(object sender, EventArgs e)
        {
            Funciones funciones = new Funciones();
            GlobalVariables variables = new GlobalVariables();
            string idcia = variables.getValorCia();
            string ruccia = variables.getValorRucCia();

            string squery = "SELECT ruccia as clave,descia as descripcion FROM ciafile WHERE idcia='" + @idcia + "';";
            funciones.listaComboBox(squery, cmbEmpleador, "");
            cmbEmpleador.Enabled = false;

            string squery_esta = "SELECT idestane as clave,desestane as descripcion FROM estane WHERE codemp='" + @ruccia + "' and idciafile='" + @idcia + "' order by idestane asc;";
            funciones.listaComboBox(squery_esta, cmbEstablecimiento, "");
            cmbEstablecimiento.Focus();
        }

        private void cmbEmpleador_SelectedIndexChanged(object sender, EventArgs e)
        {
            Funciones funciones = new Funciones();
            GlobalVariables variables = new GlobalVariables();
            string idcia = variables.getValorCia();
            string ruccia = funciones.getValorComboBox(cmbEmpleador, 11);
            string squery_esta = "SELECT idestane as clave,desestane as descripcion FROM estane WHERE codemp='" + @ruccia + "' and idciafile='" + @idcia + "' order by idestane asc;";
            funciones.listaComboBox(squery_esta, cmbEstablecimiento, "");
            cmbEstablecimiento.Focus();
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            string valida = "G";
            Funciones fn = new Funciones();
            GlobalVariables gv = new GlobalVariables();
            CalPlan calplan = new CalPlan();

            string idtipoper = fn.getValorComboBox(cmbTipoTrabajador, 1);
            string periodo = txtMesSem.Text.Trim() + fn.replicateCadena(" ", 6);
            string messem = periodo.Substring(0, 2);
            string anio = periodo.Substring(3, 4);
            string idtipocal = fn.getValorComboBox(cmbTipoCal, 1);
            string idtipoplan = fn.getValorComboBox(cmbTipoPlan, 3);
            string idcia = gv.getValorCia();
            string rucemp = fn.getValorComboBox(cmbEmpleador, 11);
            string estane = fn.getValorComboBox(cmbEstablecimiento, 4);
            string tipo = cmbTipo.Text.Trim();

            if (anio.Trim() == string.Empty)
            {
                MessageBox.Show("No ha especificado Periodo Laboral", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtMesSem.Focus();
                valida = "B";
            }

            if (rucemp.Trim() == string.Empty)
            {
                MessageBox.Show("No ha seleccionado Empleador", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cmbEmpleador.Focus();
                valida = "B";
            }

            if (estane.Trim() == string.Empty)
            {
                MessageBox.Show("No ha seleccionado Establecimiento", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cmbEstablecimiento.Focus();
                valida = "B";
            }

            if (valida.Equals("G"))
            {

                lblprocesando.Visible = true;
                cr.crboleta_agrofrutos cra;
                cr.crboleta_agromarin crb;
                cr.crboleta_agronegocios crc;
                cr.crboleta_asepesac crd;
                cr.crboleta_citricos cre;


                Boleta boleta = new Boleta();
                DataTable dtboleta = new DataTable();
                dtboleta = boleta.generaBoletasWin(idtipoper, messem, anio, idtipocal, idtipoplan, idcia, rucemp, estane);
                if (dtboleta.Rows.Count > 0)
                {
                    if (tipo.Equals("SIN LOGOTIPO"))
                    {
                        //      cr.crboleta cr = new cr.crboleta();
                        //   ui_reporte ui_reporte = new ui_reporte();
                        //     ui_reporte.asignaDataTable(cr, dtboleta);
                        //    ui_reporte.Activate();
                        //    ui_reporte.BringToFront();
                        //   ui_reporte.ShowDialog();
                        //  ui_reporte.Dispose();

                        ui_reporte ui_reporte = new ui_reporte();
                        if (idcia == "01")
                        {
                            cre = new cr.crboleta_citricos();
                            ui_reporte.asignaDataTable(cre, dtboleta);
                        }
                        else
                        {
                            if (idcia == "02")
                            {

                                crb = new cr.crboleta_agromarin();
                                ui_reporte.asignaDataTable(crb, dtboleta);
                            }
                            else
                            {
                                if (idcia == "03")
                                {

                                    crc = new cr.crboleta_agronegocios();
                                    ui_reporte.asignaDataTable(crc, dtboleta);
                                }
                                else
                                {
                                    if (idcia == "04")
                                    {
                                        cra = new cr.crboleta_agrofrutos();
                                        ui_reporte.asignaDataTable(cra, dtboleta);
                                    }
                                    else
                                    {
                                        crd = new cr.crboleta_asepesac();
                                        ui_reporte.asignaDataTable(crd, dtboleta);
                                    }
                                }
                            }
                        }

                        ui_reporte.Activate();
                        ui_reporte.BringToFront();
                        ui_reporte.ShowDialog();
                        ui_reporte.Dispose();



                    }
                    else
                    {
                        cr.crboletalogo cr = new cr.crboletalogo();
                        ui_reporte ui_reporte = new ui_reporte();
                        ui_reporte.asignaDataTable(cr, dtboleta);
                        ui_reporte.Activate();
                        ui_reporte.BringToFront();
                        ui_reporte.ShowDialog();
                        ui_reporte.Dispose();
                    }
                }
                else
                {
                    MessageBox.Show("No existe información registrada en el criterio seleccionado", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }

                lblprocesando.Visible = false;
            }
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cmbTipoTrabajador_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtMesSem.Clear();
            txtFechaIni.Clear();
            txtFechaFin.Clear();
            txtMesSem.Focus();
        }

        private void cmbTipoPlan_SelectedIndexChanged(object sender, EventArgs e)
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
        }

        private void cmbTipoCal_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtMesSem.Clear();
            txtFechaIni.Clear();
            txtFechaFin.Clear();
            txtMesSem.Focus();
        }
    }
}