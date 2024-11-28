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
    public partial class ui_resumendestajoperiodo : Form
    {
        private MaskedTextBox TextBoxActivo;

        public MaskedTextBox _TextBoxActivo
        {
            get { return TextBoxActivo; }
            set { TextBoxActivo = value; }
        }

        public ui_resumendestajoperiodo()
        {
            InitializeComponent();
        }

        private void cmbTipoRegistro_SelectedIndexChanged(object sender, EventArgs e)
        {
            Funciones fn = new Funciones();

            if (fn.getValorComboBox(cmbTipoRegistro, 1).Equals("P"))
            {
                GlobalVariables gv = new GlobalVariables();
                string idcia = gv.getValorCia();
                string query = "SELECT idtipoplan as clave,destipoplan as descripcion FROM tipoplan where idtipoplan in (select idtipoplan from reglabcia where idcia='" + @idcia + "') order by 1 asc;";
                fn.listaComboBox(query, cmbTipoPlan, "");
                lblTipoPlanilla.Visible = true;
                cmbTipoPlan.Visible = true;
                cmbTipoPlan.Focus();
            }
            else
            {
                lblTipoPlanilla.Visible = false;
                cmbTipoPlan.Visible = false;
            }
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

        private void btnVisualizar_Click(object sender, EventArgs e)
        {
            Funciones funciones = new Funciones();
            GlobalVariables variables = new GlobalVariables();
            string idcia = variables.getValorCia();
            string idtipocal = "D";
            string idtipoper = "O";
            string tiporegistro = funciones.getValorComboBox(cmbTipoRegistro, 1);
            string emplea = funciones.getValorComboBox(cmbEmpleador, 11);
            string estane = funciones.getValorComboBox(cmbEstablecimiento, 4);
            string periodo = txtMesSem.Text.Trim() + funciones.replicateCadena(" ", 6);
            string messem = periodo.Substring(0, 2);
            string anio = periodo.Substring(3, 4);
            string tiporeporte = funciones.getValorComboBox(cmbTipoReporte, 1);

            string idtipoplan = "";
            if (tiporegistro.Equals("P"))
            {
                idtipoplan = funciones.getValorComboBox(cmbTipoPlan, 3);
            }

            string nombreTablaRet = string.Empty;
            string nombreTablaPer = string.Empty;

            if (tiporegistro.Equals("R"))
            {
                nombreTablaRet = "desret";
                nombreTablaPer = "perret";
            }
            else
            {
                nombreTablaRet = "desplan";
                nombreTablaPer = "perplan";
            }
            string query;
            if (tiporeporte.Equals("1"))
            {
                query = "  select A.anio,A.messem,F.descia,F.ruccia,B.idperplan,D.Parm1maesgen as cortotipodoc,B.nrodoc,";
                query += " CONCAT(B.apepat,' ',B.apemat,' , ',B.nombres) as nombre,E.deszontra,G.desvar, ";
                query += " SUM(A.cantidad) as cantidad,SUM(A.subtotal)/SUM(A.cantidad) as precio,SUM(A.subtotal) as subtotal,";
                query += " SUM(A.movilidad) as movilidad,SUM(A.refrigerio) as refrigerio,SUM(A.adicional) as adicional,SUM(A.total) as total ";
                query += " from " + @nombreTablaRet + " A  ";
                query += " left join " + @nombreTablaPer + " B on A.idperplan=B.idperplan and A.idcia=B.idcia ";
                query += " left join maesgen D on D.idmaesgen='002' and B.tipdoc=D.clavemaesgen ";
                query += " left join zontra E on A.idzontra=E.idzontra and A.idcia=E.idcia ";
                query += " left join varproddes G on A.idproddes=G.idproddes and A.idcia=G.idcia and A.codvar=G.codvar ";
                query += " left join ciafile F on A.idcia=F.idcia ";
                query += " where A.idcia='" + @idcia + "' and A.idtipocal='" + @idtipocal + "' and A.idtipoper='" + @idtipoper + "' ";
                query += " and A.idtipoplan='" + @idtipoplan + "' and A.emplea='" + @emplea + "' and A.estane='" + @estane + "'";
                query += " and A.anio='" + @anio + "' and A.messem='" + @messem + "' ";
                query += " group by A.anio,A.messem,F.descia,F.ruccia,B.idperplan,D.Parm1maesgen,B.nrodoc,B.apepat,B.apemat,B.nombres,E.deszontra,G.desvar ";
                query += " order by F.descia,E.idzontra asc,G.desvar,B.apepat asc,B.apemat asc ,B.nombres asc;";
            }
            else
            {
                if (tiporeporte.Equals("2"))
                {
                    query = "  SELECT A.anio,A.messem,F.descia,F.ruccia,B.idperplan,D.Parm1maesgen as cortotipodoc,B.nrodoc,";
                    query += " CONCAT(B.apepat,' ',B.apemat,' , ',B.nombres) as nombre, ";
                    query += " SUM(A.cantidad) as cantidad,SUM(A.subtotal)/SUM(A.cantidad) as precio,SUM(A.subtotal) as subtotal,";
                    query += " SUM(A.movilidad) as movilidad,SUM(A.refrigerio) as refrigerio,SUM(A.adicional) as adicional,SUM(A.total) as total ";
                    query += " from " + @nombreTablaRet + " A  ";
                    query += " left join " + @nombreTablaPer + " B on A.idperplan=B.idperplan and A.idcia=B.idcia ";
                    query += " left join maesgen D on D.idmaesgen='002' and B.tipdoc=D.clavemaesgen ";
                    query += " left join ciafile F on A.idcia=F.idcia ";
                    query += " where A.idcia='" + @idcia + "' and A.idtipocal='" + @idtipocal + "' and A.idtipoper='" + @idtipoper + "' ";
                    query += " and A.idtipoplan='" + @idtipoplan + "' and A.emplea='" + @emplea + "' and A.estane='" + @estane + "'";
                    query += " and A.anio='" + @anio + "' and A.messem='" + @messem + "' ";
                    query += " group by A.anio,A.messem,F.descia,F.ruccia,B.idperplan,D.Parm1maesgen,B.nrodoc,B.apepat,B.apemat,B.nombres ";
                    query += " order by F.descia,B.apepat asc,B.apemat asc ,B.nombres asc;";
                }
                else
                {
                    query = "  SELECT A.anio,A.messem,F.descia,F.ruccia,B.idperplan,D.Parm1maesgen ";
                    query += " as cortotipodoc,B.nrodoc,CONCAT(B.apepat,' ',B.apemat,' , ',B.nombres) as nombre, ";
                    query += " SUM(A.total) as destajo,CASE isnull(P.Neto) WHEN '1' THEN 0 WHEN 0 THEN P.neto END as planilla from desplan A ";
                    query += " left join perplan B on A.idperplan=B.idperplan and A.idcia=B.idcia  ";
                    query += " left join maesgen D on D.idmaesgen='002' and B.tipdoc=D.clavemaesgen  ";
                    query += " left join ciafile F on A.idcia=F.idcia left join plan_ P on A.anio=P.anio  ";
                    query += " and A.messem=P.messem and A.idcia=P.idcia and A.idperplan=P.idperplan and A.idtipocal=P.idtipocal and ";
                    query += " A.idtipoper=P.idtipoper and A.idtipoplan=P.idtipoplan ";
                    query += " where A.idcia='" + @idcia + "' and A.idtipocal='" + @idtipocal + "' and A.idtipoper='" + @idtipoper + "' ";
                    query += " and A.idtipoplan='" + @idtipoplan + "' and A.emplea='" + @emplea + "' and A.estane='" + @estane + "'";
                    query += " and A.anio='" + @anio + "' and A.messem='" + @messem + "' ";
                    query += " group by A.anio,A.messem,F.descia,F.ruccia,B.idperplan,D.Parm1maesgen, ";
                    query += " B.nrodoc,B.apepat,B.apemat,B.nombres order by F.descia,B.apepat asc,B.apemat asc ,B.nombres asc;  ";
                }
            }

            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();
            try
            {
                using (SqlDataAdapter myDataAdapter = new SqlDataAdapter(query, conexion))
                {
                    if (tiporeporte.Equals("1"))
                    {
                        DataSet dsresdesper = new DataSet();
                        cr.crresdesper cr = new cr.crresdesper();
                        myDataAdapter.Fill(dsresdesper, "dtresdesper");
                        ui_reporte ui_reporte = new ui_reporte();
                        ui_reporte.asignaDataSet(cr, dsresdesper);
                        ui_reporte.Activate();
                        ui_reporte.BringToFront();
                        ui_reporte.ShowDialog();
                        ui_reporte.Dispose();
                    }
                    else
                    {
                        if (tiporeporte.Equals("2"))
                        {
                            DataSet dsresdesper = new DataSet();
                            cr.crresgendesper cr = new cr.crresgendesper();
                            myDataAdapter.Fill(dsresdesper, "dtresdesper");
                            ui_reporte ui_reporte = new ui_reporte();
                            ui_reporte.asignaDataSet(cr, dsresdesper);
                            ui_reporte.Activate();
                            ui_reporte.BringToFront();
                            ui_reporte.ShowDialog();
                            ui_reporte.Dispose();
                        }
                        else
                        {
                            DataSet dscomparadesplan = new DataSet();
                            cr.crComparaDesPlan cr = new cr.crComparaDesPlan();
                            myDataAdapter.Fill(dscomparadesplan, "dtcompara");
                            ui_reporte ui_reporte = new ui_reporte();
                            ui_reporte.asignaDataSet(cr, dscomparadesplan);
                            ui_reporte.Activate();
                            ui_reporte.BringToFront();
                            ui_reporte.ShowDialog();
                            ui_reporte.Dispose();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }

            conexion.Close();
        }

        private void txtMesSem_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                Funciones funciones = new Funciones();
                CalPlan calplan = new CalPlan();
                GlobalVariables variables = new GlobalVariables();
                string idcia = variables.getValorCia();
                string idtipoper = "O";
                string periodo = txtMesSem.Text.Trim() + funciones.replicateCadena(" ", 6);
                string messem = periodo.Substring(0, 2);
                string anio = periodo.Substring(3, 4);
                string idtipocal = "D";

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

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            GlobalVariables variables = new GlobalVariables();
            string idtipoper = "O";
            string idtipocal = "D";
            string idcia = variables.getValorCia();
            string clasepadre = "ui_resumendestajoperiodo";
            this._TextBoxActivo = txtMesSem;

            ui_buscarcalplan ui_buscarcalplan = new ui_buscarcalplan();
            ui_buscarcalplan._FormPadre = this;
            ui_buscarcalplan.setValores(idtipoper, idcia, clasepadre, idtipocal);
            ui_buscarcalplan.Activate();
            ui_buscarcalplan.BringToFront();
            ui_buscarcalplan.ShowDialog();
            ui_buscarcalplan.Dispose();
        }

        private void ui_resumendestajoperiodo_Load(object sender, EventArgs e)
        {
            Funciones funciones = new Funciones();
            GlobalVariables variables = new GlobalVariables();
            string squery;
            squery = "SELECT ruccia as clave,descia as descripcion FROM ciafile WHERE idcia='" + @variables.getValorCia() + "';";
            funciones.listaComboBox(squery, cmbEmpleador, "");
            cmbTipoRegistro.Text = "R   PERSONAL DE RETENCIONES";
            cmbTipoReporte.Text = "2    RESUMEN GENERAL";
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}