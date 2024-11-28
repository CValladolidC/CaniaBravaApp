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
    public partial class ui_Plan : Form
    {
        public ui_Plan()
        {
            InitializeComponent();
            string bd_prov = ConfigurationManager.AppSettings.Get("BD_PROV");
            if (bd_prov.Equals("agromango")) { btnImprimir.Visible = false; }
        }

        private void ui_Plan_Load(object sender, EventArgs e)
        {
            Funciones funciones = new Funciones();
            GlobalVariables variables = new GlobalVariables();
            string idcia = variables.getValorCia();
            string squery;
            squery = "SELECT idtipoper as clave,destipoper as descripcion FROM tipoper order by 1 asc";
            funciones.listaComboBox(squery, cmbTipoTrabajador, "");
            squery = "SELECT ruccia as clave,descia as descripcion FROM ciafile WHERE idcia='" + @idcia + "';";
            funciones.listaComboBox(squery, cmbEmpleador, "");
            squery = "SELECT idtipoplan as clave,destipoplan as descripcion FROM tipoplan where idtipoplan in (select idtipoplan from reglabcia where idcia='" + @idcia + "') order by 1 asc;";
            funciones.listaComboBox(squery, cmbTipoPlan, "");
            string idtipoplan = funciones.getValorComboBox(cmbTipoPlan, 3);
            ui_listaTipoCal(idcia, idtipoplan);
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

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ui_ListaPlan()
        {
            Funciones funciones = new Funciones();
            GlobalVariables globalVariable = new GlobalVariables();
            DataTable dtplan = new DataTable();
            string bd_prov = ConfigurationManager.AppSettings.Get("BD_PROV");

            string idtipoper = funciones.getValorComboBox(cmbTipoTrabajador, 1);
            string periodo = txtMesSem.Text.Trim() + funciones.replicateCadena(" ", 6);
            string messem = periodo.Substring(0, 2);
            string anio = periodo.Substring(3, 4);
            string idtipocal = funciones.getValorComboBox(cmbTipoCal, 1);
            string idtipoplan = funciones.getValorComboBox(cmbTipoPlan, 3);
            string idcia = globalVariable.getValorCia();
            string rucemp = funciones.getValorComboBox(cmbEmpleador, 11);
            string estane = funciones.getValorComboBox(cmbEstablecimiento, 4);

            string query = " Select A.idperplan,C.Parm1maesgen as cortotipodoc,A.nrodoc, ";
            query += " CONCAT(CONCAT(CONCAT(A.apepat,' '),CONCAT(A.apemat,' , ')),A.nombres) as nombre,";
            query += " A.cuspp,D.desmaesgen as seccion, ";
            query += " E.deslabper as ocupacion,P.finilab,P.ffinlab,";
            query += " P.diasefelab,P.diassubsi,P.diasnosubsi,P.dias, ";

            //Agregado por Oliver Cruz Tuanama
            if (!bd_prov.Equals("agromango")) { query += " P.hext25,P.hext35,P.hext100, "; }

            query += " P.rembas,P.jorbas,P.dom,P.finivac,P.ffinvac,P.diasvaca,P.remvac,P.comides, ";

            //Agregado por Oliver Cruz Tuanama
            if (!bd_prov.Equals("agromango")) { query += " P.imphext25,P.imphext35,P.imphext100, "; }

            query += " P.asigfam,P.asigesc,P.asigotros,";
            query += " P.reintegro,P.bonifica,P.cts,P.gratifica,P.total_ingresos, ";
            query += " L.desfonpen,P.sppao,P.sppav,P.sppcp,P.sppps,P.snp,P.esvida,P.quicat,";
            query += " P.adelanto,P.sindica,P.desjud,P.despres,P.desban,P.desseg,P.desnodedu,P.desdedu, ";
            query += " P.total_descuentos,P.sppavemp,P.essalud, ";
            query += " P.essctr,P.senati,P.aporemp,P.epssctr,P.sis,P.total_aporta,P.neto, ";

            //Agregado por Oliver Cruz Tuanama
            if (bd_prov.Equals("agromango"))
            {
                query += " P.hext25,P.hext35,P.hext100,P.imphext25,P.imphext35,P.imphext100,P.tot_horasxtra, ";
            }

            query += " CONCAT(F.desmaesgen,' ',G.Parm1maesgen,' ',A.monrem) ";
            query += " as tipocuentarem,A.nroctarem,CONCAT(H.desmaesgen,' ',I.Parm1maesgen,' ',A.moncts) ";
            query += " as tipocuentacts,A.nroctacts from plan_ P ";
            query += " left join labper E on P.idcia=E.idcia and P.idlabper=E.idlabper and P.idtipoper=E.idtipoper ";
            query += " inner join ciafile Q on P.idcia=Q.idcia ";
            query += " inner join calplan R on P.anio=R.anio and P.messem=R.messem ";
            query += " and P.idtipoper=R.idtipoper and P.idtipocal=R.idtipocal and P.idcia=R.idcia ";
            query += " left join fonpen L on P.idfonpen=L.idfonpen ";
            query += " left join maesgen D on D.idmaesgen='008' and P.seccion=D.clavemaesgen ";
            query += " inner join tipoplan U on P.idtipoplan=U.idtipoplan  ";
            query += " inner join perplan A on P.idcia=A.idcia and P.idperplan=A.idperplan ";
            query += " left join tipoper B on A.idtipoper=B.idtipoper ";
            query += " left join maesgen C on C.idmaesgen='002' and A.tipdoc=C.clavemaesgen  ";
            query += " left join maesgen F on F.idmaesgen='007' and A.entfinrem=F.clavemaesgen ";
            query += " left join maesgen G on G.idmaesgen='014' and A.tipctarem=G.clavemaesgen ";
            query += " left join maesgen H on H.idmaesgen='007' and A.entfincts=H.clavemaesgen ";
            query += " left join maesgen I on I.idmaesgen='014' and A.tipctacts=I.clavemaesgen ";
            query += " where P.rucemp='" + @rucemp + "' and P.estane='" + @estane + "' ";
            query += " and P.anio='" + @anio + "' and P.messem='" + @messem + "' and ";
            query += " P.idcia='" + @idcia + "' and P.idtipoper='" + @idtipoper + "' ";
            query += " and P.idtipoplan='" + @idtipoplan + "' and P.idtipocal='" + @idtipocal + "' ";
            query += " order by nombre asc;";

            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();
            SqlDataAdapter da = new SqlDataAdapter();

            da.SelectCommand = new SqlCommand(query, conexion);
            da.Fill(dtplan);

            funciones.formatearDataGridView(dgvdetalle);
            dgvdetalle.DataSource = dtplan;
            dgvdetalle.Columns[0].HeaderText = "Cód. Int.";
            dgvdetalle.Columns[1].HeaderText = "Doc. Identidad";
            dgvdetalle.Columns[2].HeaderText = "Nro. Doc.";
            dgvdetalle.Columns[3].HeaderText = "Apellidos y Nombres";
            dgvdetalle.Columns[4].HeaderText = "CUSSP";
            dgvdetalle.Columns[5].HeaderText = "Sección";
            dgvdetalle.Columns[6].HeaderText = "Ocupación";
            dgvdetalle.Columns[7].HeaderText = "Fecha Inicio";
            dgvdetalle.Columns[8].HeaderText = "Fecha Cese";

            dgvdetalle.Columns[9].HeaderText = "Dias Efe. Lab.";
            dgvdetalle.Columns[10].HeaderText = "Dias Subsi.";
            dgvdetalle.Columns[11].HeaderText = "Dias No Subsi";
            dgvdetalle.Columns[12].HeaderText = "Total Dias";
            
            //Agregado por Oliver Cruz Tuanama
            if (!bd_prov.Equals("agromango"))
            {
                dgvdetalle.Columns[13].HeaderText = "H.Ext. 25%";
                dgvdetalle.Columns[14].HeaderText = "H.Ext. 35%";
                dgvdetalle.Columns[15].HeaderText = "H.Ext. 100%";

                dgvdetalle.Columns[16].HeaderText = "Rem. Básica";
                dgvdetalle.Columns[17].HeaderText = "Jornal Básico";
                dgvdetalle.Columns[18].HeaderText = "Dom.";
                dgvdetalle.Columns[19].HeaderText = "F. Inicio Vac.";
                dgvdetalle.Columns[20].HeaderText = "F. Fin Vac.";
                dgvdetalle.Columns[21].HeaderText = "Dias Vac.";

                dgvdetalle.Columns[22].HeaderText = "Rem.Vac.";
                dgvdetalle.Columns[23].HeaderText = "Comisión / Destajo";

                dgvdetalle.Columns[24].HeaderText = "Imp.H.Ext. 25%";
                dgvdetalle.Columns[25].HeaderText = "Imp.H.Ext. 35%";
                dgvdetalle.Columns[26].HeaderText = "Imp.H.Ext. 100%";

                dgvdetalle.Columns[27].HeaderText = "Asig. Familiar";
                dgvdetalle.Columns[28].HeaderText = "Asig. Escolar";
                dgvdetalle.Columns[29].HeaderText = "Otras Asig.";
                dgvdetalle.Columns[30].HeaderText = "Reintegro";
                dgvdetalle.Columns[31].HeaderText = "Bonificación";
                dgvdetalle.Columns[32].HeaderText = "C.T.S.";
                dgvdetalle.Columns[33].HeaderText = "Gratifiación";

                dgvdetalle.Columns[34].HeaderText = "Total Ingresos";
                dgvdetalle.Columns[35].HeaderText = "Fondo Pensiones";
                dgvdetalle.Columns[36].HeaderText = "SPP. Aport. Oblig.";
                dgvdetalle.Columns[37].HeaderText = "SPP. Aport. Vol.";
                dgvdetalle.Columns[38].HeaderText = "SPP Com. Porcentual";
                dgvdetalle.Columns[39].HeaderText = "SPP Prima Seguro";
                dgvdetalle.Columns[40].HeaderText = "S.N.P.";
                dgvdetalle.Columns[41].HeaderText = "EsVida";
                dgvdetalle.Columns[42].HeaderText = "5ta. Categoría";
                dgvdetalle.Columns[43].HeaderText = "Adelando";
                dgvdetalle.Columns[44].HeaderText = "Sindicato";
                dgvdetalle.Columns[45].HeaderText = "Descuento Judicial";
                dgvdetalle.Columns[46].HeaderText = "Prestamos";
                dgvdetalle.Columns[47].HeaderText = "Convenio Bancario";
                dgvdetalle.Columns[48].HeaderText = "Seguro Privado";

                dgvdetalle.Columns[49].HeaderText = "Dscto. No Deducible";
                dgvdetalle.Columns[50].HeaderText = "Dscto Deducible";
                dgvdetalle.Columns[51].HeaderText = "Total Descuentos";

                dgvdetalle.Columns[52].HeaderText = "Aport.SPP Vol. Empleador";
                dgvdetalle.Columns[53].HeaderText = "EsSalud";
                dgvdetalle.Columns[54].HeaderText = "EsSalud - SCTR";
                dgvdetalle.Columns[55].HeaderText = "SENATI";
                dgvdetalle.Columns[56].HeaderText = "Otras Aportaciones";
                dgvdetalle.Columns[57].HeaderText = "EPS - SCTR";
                dgvdetalle.Columns[58].HeaderText = "SIS";
                dgvdetalle.Columns[59].HeaderText = "Total Aportaciones";
                dgvdetalle.Columns[60].HeaderText = "Neto a Recibir";
                dgvdetalle.Columns[61].HeaderText = "Tipo Cta. Rem.";
                dgvdetalle.Columns[62].HeaderText = "Nro. Cta. Rem.";
                dgvdetalle.Columns[63].HeaderText = "Tipo Cta.CTS";
                dgvdetalle.Columns[64].HeaderText = "Nro. Cta. CTS";

            }
            else
            {
                dgvdetalle.Columns[13].HeaderText = "Rem. Básica";
                dgvdetalle.Columns[14].HeaderText = "Jornal Básico";
                dgvdetalle.Columns[15].HeaderText = "Dom.";
                dgvdetalle.Columns[16].HeaderText = "F. Inicio Vac.";
                dgvdetalle.Columns[17].HeaderText = "F. Fin Vac.";
                dgvdetalle.Columns[18].HeaderText = "Dias Vac.";

                dgvdetalle.Columns[19].HeaderText = "Rem.Vac.";
                dgvdetalle.Columns[20].HeaderText = "Comisión / Destajo";

                dgvdetalle.Columns[21].HeaderText = "Asig. Familiar";
                dgvdetalle.Columns[22].HeaderText = "Asig. Escolar";
                dgvdetalle.Columns[23].HeaderText = "Otras Asig.";
                dgvdetalle.Columns[24].HeaderText = "Reintegro";
                dgvdetalle.Columns[25].HeaderText = "Bonificación";
                dgvdetalle.Columns[26].HeaderText = "C.T.S.";
                dgvdetalle.Columns[27].HeaderText = "Gratifiación";

                dgvdetalle.Columns[28].HeaderText = "Total Ingresos";
                dgvdetalle.Columns[29].HeaderText = "Fondo Pensiones";
                dgvdetalle.Columns[30].HeaderText = "SPP. Aport. Oblig.";
                dgvdetalle.Columns[31].HeaderText = "SPP. Aport. Vol.";
                dgvdetalle.Columns[32].HeaderText = "SPP Com. Porcentual";
                dgvdetalle.Columns[33].HeaderText = "SPP Prima Seguro";
                dgvdetalle.Columns[34].HeaderText = "S.N.P.";
                dgvdetalle.Columns[35].HeaderText = "EsVida";
                dgvdetalle.Columns[36].HeaderText = "5ta. Categoría";
                dgvdetalle.Columns[37].HeaderText = "Adelando";
                dgvdetalle.Columns[38].HeaderText = "Sindicato";
                dgvdetalle.Columns[39].HeaderText = "Descuento Judicial";
                dgvdetalle.Columns[40].HeaderText = "Prestamos";
                dgvdetalle.Columns[41].HeaderText = "Convenio Bancario";
                dgvdetalle.Columns[42].HeaderText = "Seguro Privado";

                dgvdetalle.Columns[43].HeaderText = "Dscto. No Deducible";
                dgvdetalle.Columns[44].HeaderText = "Dscto Deducible";
                dgvdetalle.Columns[45].HeaderText = "Total Descuentos";

                dgvdetalle.Columns[46].HeaderText = "Aport.SPP Vol. Empleador";
                dgvdetalle.Columns[47].HeaderText = "EsSalud";
                dgvdetalle.Columns[48].HeaderText = "EsSalud - SCTR";
                dgvdetalle.Columns[49].HeaderText = "SENATI";
                dgvdetalle.Columns[50].HeaderText = "Otras Aportaciones";
                dgvdetalle.Columns[51].HeaderText = "EPS - SCTR";
                dgvdetalle.Columns[52].HeaderText = "SIS";
                dgvdetalle.Columns[53].HeaderText = "Total Aportaciones";
                dgvdetalle.Columns[54].HeaderText = "Neto a Recibir";

                dgvdetalle.Columns[55].HeaderText = "H.Ext. 25%";
                dgvdetalle.Columns[56].HeaderText = "H.Ext. 35%";
                dgvdetalle.Columns[57].HeaderText = "H.Ext. 100%";
                dgvdetalle.Columns[58].HeaderText = "Imp.H.Ext. 25%";
                dgvdetalle.Columns[59].HeaderText = "Imp.H.Ext. 35%";
                dgvdetalle.Columns[60].HeaderText = "Imp.H.Ext. 100%";
                dgvdetalle.Columns[61].HeaderText = "Total H.Ext.";
                dgvdetalle.Columns[62].HeaderText = "Tipo Cta. Rem.";
                dgvdetalle.Columns[63].HeaderText = "Nro. Cta. Rem.";
                dgvdetalle.Columns[64].HeaderText = "Tipo Cta.CTS";
                dgvdetalle.Columns[65].HeaderText = "Nro. Cta. CTS";
            }

            dgvdetalle.Columns["idperplan"].Frozen = true;
            dgvdetalle.Columns["cortotipodoc"].Frozen = true;
            dgvdetalle.Columns["nrodoc"].Frozen = true;
            dgvdetalle.Columns["nombre"].Frozen = true;

            int i;

            for (i = 9; i < 19; i++)
            {
                dgvdetalle.Columns[i].DefaultCellStyle.Format = "###,###.##";
                dgvdetalle.Columns[i].DefaultCellStyle.Alignment = DataGridViewContentAlignment.BottomRight;
                dgvdetalle.Columns[i].Width = 75;
            }

            dgvdetalle.Columns[21].DefaultCellStyle.Format = "###,###.##";
            dgvdetalle.Columns[21].DefaultCellStyle.Alignment = DataGridViewContentAlignment.BottomRight;
            dgvdetalle.Columns[21].Width = 75;

            for (i = 22; i < 35; i++)
            {
                dgvdetalle.Columns[i].DefaultCellStyle.Format = "###,###.##";
                dgvdetalle.Columns[i].DefaultCellStyle.Alignment = DataGridViewContentAlignment.BottomRight;
                dgvdetalle.Columns[i].Width = 75;
            }

            for (i = 36; i < 61; i++)
            {
                dgvdetalle.Columns[i].DefaultCellStyle.Format = "###,###.##";
                dgvdetalle.Columns[i].DefaultCellStyle.Alignment = DataGridViewContentAlignment.BottomRight;
                dgvdetalle.Columns[i].Width = 75;
            }

            float totalIngresos = float.Parse(funciones.sumaColumnaDataGridView(dgvdetalle, 34));
            float totalDescuentos = float.Parse(funciones.sumaColumnaDataGridView(dgvdetalle, 48));
            float totalAportes = float.Parse(funciones.sumaColumnaDataGridView(dgvdetalle, 56));
            float neto = float.Parse(funciones.sumaColumnaDataGridView(dgvdetalle, 57));

            txtIngresos.Text = Convert.ToString(totalIngresos);
            txtDescuentos.Text = Convert.ToString(totalDescuentos);
            txtAportaciones.Text = Convert.ToString(totalAportes);

            dgvdetalle.Columns[0].Width = 50;
            dgvdetalle.Columns[1].Width = 50;
            dgvdetalle.Columns[2].Width = 60;
            dgvdetalle.Columns[3].Width = 200;
            dgvdetalle.Columns[4].Width = 80;
            dgvdetalle.Columns[5].Width = 100;
            dgvdetalle.Columns[6].Width = 100;
            dgvdetalle.Columns[7].Width = 75;
            dgvdetalle.Columns[8].Width = 75;

            dgvdetalle.Columns[19].Width = 75;
            dgvdetalle.Columns[20].Width = 75;

            dgvdetalle.Columns[35].Width = 75;
            dgvdetalle.Columns[61].Width = 150;
            dgvdetalle.Columns[62].Width = 120;
            dgvdetalle.Columns[63].Width = 150;
            dgvdetalle.Columns[64].Width = 120;

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
                string idtipoper = funciones.getValorComboBox(cmbTipoTrabajador, 1);
                string periodo = txtMesSem.Text.Trim() + funciones.replicateCadena(" ", 6);
                string messem = periodo.Substring(0, 2);
                string anio = periodo.Substring(3, 4);
                string idtipocal = funciones.getValorComboBox(cmbTipoCal, 1);
                if (calplan.getDatosCalPlan(messem, anio, idtipoper, idcia, idtipocal, "FECHAINI") != "")
                {
                    txtFechaIni.Text = calplan.getDatosCalPlan(messem, anio, idtipoper, idcia, idtipocal, "FECHAINI");
                    txtFechaFin.Text = calplan.getDatosCalPlan(messem, anio, idtipoper, idcia, idtipocal, "FECHAFIN");
                    ui_ListaPlan();
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
            ui_ListaPlan();
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
            ui_ListaPlan();
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
            ui_ListaPlan();
            cmbEstablecimiento.Focus();
        }

        private void cmbTipoTrabajador_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtMesSem.Clear();
            txtFechaIni.Clear();
            txtFechaFin.Clear();
            ui_ListaPlan();
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
            ui_ListaPlan();
            txtMesSem.Focus();

        }

        private void cmbTipoCal_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtMesSem.Clear();
            txtFechaIni.Clear();
            txtFechaFin.Clear();
            ui_ListaPlan();
            txtMesSem.Focus();
        }

        private void cmbEstablecimiento_SelectedIndexChanged(object sender, EventArgs e)
        {
            ui_ListaPlan();
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            Exporta exporta = new Exporta();
            exporta.Excel_FromDataGridView(dgvdetalle);
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            GlobalVariables globalVariable = new GlobalVariables();
            Funciones funciones = new Funciones();
            string idtipoper = funciones.getValorComboBox(cmbTipoTrabajador, 1);
            string periodo = txtMesSem.Text.Trim() + funciones.replicateCadena(" ", 6);
            string messem = periodo.Substring(0, 2);
            string anio = periodo.Substring(3, 4);
            string idtipocal = funciones.getValorComboBox(cmbTipoCal, 1);
            string idtipoplan = funciones.getValorComboBox(cmbTipoPlan, 3);
            string idcia = globalVariable.getValorCia();
            string rucemp = funciones.getValorComboBox(cmbEmpleador, 11);
            string estane = funciones.getValorComboBox(cmbEstablecimiento, 4);

            Int32 selectedCellCount =
            dgvdetalle.GetCellCount(DataGridViewElementStates.Selected);
            if (selectedCellCount > Convert.ToInt32(0))
            {

                string idperplan = dgvdetalle.Rows[dgvdetalle.SelectedCells[1].RowIndex].Cells[0].Value.ToString();
                string nombre = dgvdetalle.Rows[dgvdetalle.SelectedCells[1].RowIndex].Cells[3].Value.ToString();

                ui_detallePlan ui_detalle = new ui_detallePlan();
                ui_detalle.ui_setVariables(anio, messem, idcia, idtipoper, idtipoplan, idtipocal, idperplan, nombre);
                ui_detalle.Activate();
                ui_detalle.BringToFront();
                ui_detalle.ShowDialog();
                ui_detalle.Dispose();

            }
            else
            {
                MessageBox.Show("No ha seleccionado registro a visualizar", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            string valida = "G";
            Funciones fn = new Funciones();
            GlobalVariables gv = new GlobalVariables();

            string idtipoper = fn.getValorComboBox(cmbTipoTrabajador, 1);
            string periodo = txtMesSem.Text.Trim() + fn.replicateCadena(" ", 6);
            string messem = periodo.Substring(0, 2);
            string anio = periodo.Substring(3, 4);
            string idtipocal = fn.getValorComboBox(cmbTipoCal, 1);
            string idtipoplan = fn.getValorComboBox(cmbTipoPlan, 3);
            string idcia = gv.getValorCia();
            string rucemp = fn.getValorComboBox(cmbEmpleador, 11);
            string estane = fn.getValorComboBox(cmbEstablecimiento, 4);

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

                string query = " Select A.idperplan,B.destipoper,C.Parm1maesgen as cortotipodoc,A.nrodoc,A.cuspp, ";
                query = query + " CONCAT(CONCAT(CONCAT(A.apepat,' '),CONCAT(A.apemat,' , ')),A.nombres) as nombre,D.desmaesgen as seccion, ";
                query = query + " E.deslabper as ocupacion,P.finilab,P.ffinlab,P.anio,P.messem,P.idtipoper,P.idtipocal,P.idtipoplan,";
                query = query + " P.hext25,P.hext35,P.hext100,P.diasefelab,P.diassubsi,P.diasnosubsi,P.dias,P.rembas,P.jorbas,P.dom,P.remvac,P.comides, ";
                query = query + " P.imphext25,P.imphext35,P.imphext100,P.asigfam,P.asigesc,P.asigotros,P.reintegro,P.bonifica,P.cts,P.gratifica,P.total_ingresos, ";
                query = query + " P.sppao,P.sppav,P.sppcp,P.sppps,P.snp,P.esvida,P.quicat,P.adelanto,P.sindica,P.desjud,P.desnodedu,P.desdedu,P.total_descuentos, ";
                query = query + " P.sppavemp,P.essalud,P.essctr,P.sis,P.total_aporta,P.neto, ";
                query = query + " P.finivac,P.ffinvac,P.diasvaca,A.idcia,Q.descia,Q.ruccia,S.fechaini,S.fechafin,T.desfonpen,U.destipoplan,U.decreto, ";
                query = query + " Q.regpatcia,P.aporemp,P.senati,P.epssctr from plan_ P ";
                query = query + " left join labper E on P.idcia=E.idcia and P.idlabper=E.idlabper and P.idtipoper=E.idtipoper ";
                query = query + " inner join ciafile Q on P.idcia=Q.idcia ";
                query = query + " inner join calplan S on P.anio=S.anio and P.messem=S.messem and P.idtipoper=S.idtipoper and P.idtipocal=S.idtipocal and P.idcia=S.idcia ";
                query = query + " left join fonpen T on P.idfonpen=T.idfonpen ";
                query = query + " left join maesgen D on D.idmaesgen='008' and P.seccion=D.clavemaesgen ";
                query = query + " inner join tipoplan U on P.idtipoplan=U.idtipoplan  ";
                query = query + " inner join perplan A on P.idcia=A.idcia and P.idperplan=A.idperplan ";
                query = query + " left join tipoper B on A.idtipoper=B.idtipoper ";
                query = query + " left join maesgen C on C.idmaesgen='002' and A.tipdoc=C.clavemaesgen  ";
                query = query + " where P.rucemp='" + @rucemp + "' and P.estane='" + @estane + "' and P.anio='" + @anio + "' and P.messem='" + @messem + "' and P.idcia='" + @idcia + "' and P.idtipoper='" + @idtipoper + "' ";
                query = query + " and P.idtipoplan='" + @idtipoplan + "' and P.idtipocal='" + @idtipocal + "' order by nombre asc;";

                SqlConnection conexion = new SqlConnection();
                conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
                conexion.Open();
                try
                {
                    using (SqlDataAdapter myDataAdapter = new SqlDataAdapter(query, conexion))
                    {

                        DataSet boleta = new DataSet();

                        /////////////////////////////////////
                        ////////////LEY AGRARIA//////////////
                        /////////////////////////////////////

                        if (idtipoplan.Equals("200"))
                        {
                            if (idtipocal.Equals("N"))
                            {
                                if (idtipoper.Equals("E"))
                                {
                                    cr.crplanilla_200_emp_normal cr = new cr.crplanilla_200_emp_normal();
                                    myDataAdapter.Fill(boleta, "dtboleta");
                                    ui_reporte ui_reporte = new ui_reporte();
                                    ui_reporte.asignaDataSet(cr, boleta);
                                    ui_reporte.Activate();
                                    ui_reporte.BringToFront();
                                    ui_reporte.ShowDialog();
                                    ui_reporte.Dispose();
                                }
                                else
                                {
                                    cr.crplanilla_200_obr_normal cr = new cr.crplanilla_200_obr_normal();
                                    myDataAdapter.Fill(boleta, "dtboleta");
                                    ui_reporte ui_reporte = new ui_reporte();
                                    ui_reporte.asignaDataSet(cr, boleta);
                                    ui_reporte.Activate();
                                    ui_reporte.BringToFront();
                                    ui_reporte.ShowDialog();
                                    ui_reporte.Dispose();

                                }
                            }
                            else
                            {
                                if (idtipocal.Equals("V"))
                                {
                                    cr.crplanilla_200_vac cr = new cr.crplanilla_200_vac();
                                    myDataAdapter.Fill(boleta, "dtboleta");
                                    ui_reporte ui_reporte = new ui_reporte();
                                    ui_reporte.asignaDataSet(cr, boleta);
                                    ui_reporte.Activate();
                                    ui_reporte.BringToFront();
                                    ui_reporte.ShowDialog();
                                    ui_reporte.Dispose();
                                }
                            }

                        }
                        else
                        {
                            if (idtipoplan.Equals("100"))
                            {
                                if (idtipocal.Equals("N"))
                                {
                                    if (idtipoper.Equals("E"))
                                    {
                                        cr.crplanilla_100_emp_normal cr = new cr.crplanilla_100_emp_normal();
                                        myDataAdapter.Fill(boleta, "dtboleta");
                                        ui_reporte ui_reporte = new ui_reporte();
                                        ui_reporte.asignaDataSet(cr, boleta);
                                        ui_reporte.Activate();
                                        ui_reporte.BringToFront();
                                        ui_reporte.ShowDialog();
                                        ui_reporte.Dispose();
                                    }
                                    else
                                    {
                                        cr.crplanilla_100_obr_normal cr = new cr.crplanilla_100_obr_normal();
                                        myDataAdapter.Fill(boleta, "dtboleta");
                                        ui_reporte ui_reporte = new ui_reporte();
                                        ui_reporte.asignaDataSet(cr, boleta);
                                        ui_reporte.Activate();
                                        ui_reporte.BringToFront();
                                        ui_reporte.ShowDialog();
                                        ui_reporte.Dispose();

                                    }
                                }
                                else
                                {
                                    if (idtipocal.Equals("V"))
                                    {
                                        cr.crplanilla_200_vac cr = new cr.crplanilla_200_vac();
                                        myDataAdapter.Fill(boleta, "dtboleta");
                                        ui_reporte ui_reporte = new ui_reporte();
                                        ui_reporte.asignaDataSet(cr, boleta);
                                        ui_reporte.Activate();
                                        ui_reporte.BringToFront();
                                        ui_reporte.ShowDialog();
                                        ui_reporte.Dispose();
                                    }
                                }

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
        }

        private void groupBox46_Enter(object sender, EventArgs e)
        {

        }

        private void dgvdetalle_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void groupBox5_Enter(object sender, EventArgs e)
        {

        }

        private void toolstripform_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }
    }
}