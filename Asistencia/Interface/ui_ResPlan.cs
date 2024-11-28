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
    public partial class ui_ResPlan : Form
    {
        public ui_ResPlan()
        {
            InitializeComponent();
        }

        private void ui_ResPlan_Load(object sender, EventArgs e)
        {
            Funciones funciones = new Funciones();
            GlobalVariables variables = new GlobalVariables();
            string squery = "SELECT idtipoper as clave,destipoper as descripcion FROM tipoper order by 1 asc";
            funciones.listaComboBox(squery, cmbTipoTrabajador, "");
            squery = "SELECT idtipoplan as clave,destipoplan as descripcion FROM tipoplan where idtipoplan in (select idtipoplan from reglabcia where idcia='" + @variables.getValorCia() + "') order by 1 asc;";
            funciones.listaComboBox(squery, cmbTipoPlan, "");
            cmbMes.Text = "01   ENERO";

        }

        private void ui_ListaPlan()
        {
            Funciones funciones = new Funciones();
            GlobalVariables globalVariable = new GlobalVariables();
            DataTable dtplan = new DataTable();

            string bd_prov = ConfigurationManager.AppSettings.Get("BD_PROV");
            string idtipoper = funciones.getValorComboBox(cmbTipoTrabajador, 1);
            string idtipoplan = funciones.getValorComboBox(cmbTipoPlan, 3);
            string idcia = globalVariable.getValorCia();
            string aniopdt = txtAnio.Text;
            string mespdt = funciones.getValorComboBox(cmbMes, 2);
            string ruc = globalVariable.getValorRucCia();


            string query = " Select A.idperplan,C.Parm1maesgen as cortotipodoc,A.nrodoc, ";
            query += " CONCAT(A.apepat,' ',A.apemat,' , ',A.nombres) as nombre,A.cuspp,D.desmaesgen as seccion, ";
            query += " E.deslabper as ocupacion,MIN(P.finilab) as finilab ,MAX(P.ffinlab) as ffinlab,";
            query += " SUM(P.diasefelab)  as diasefelab,SUM(P.diassubsi) as diassubsi,SUM(P.diasnosubsi) as diasnosubsi,SUM(P.dias) as dias,SUM(P.hext25) as hext25,";
            query += " SUM(P.hext35) as hext35,SUM(P.hext100) as hext100,SUM(P.rembas) as rembas,SUM(P.jorbas) as jorbas,SUM(P.dom) as dom,";
            query += " SUM(P.diasvaca) as diasvaca,SUM(P.remvac) as remvac,SUM(P.comides) as comides,";
            query += " SUM(P.imphext25) as imphext25,SUM(P.imphext35) as imphext35,SUM(P.imphext100) as imphext100,";
            query += " SUM(P.asigfam) as asigfam,SUM(P.asigesc) as asigesc,SUM(P.asigotros) as asigotros,";
            query += " SUM(P.reintegro) as reintegro,SUM(P.bonifica) as bonifica,SUM(P.cts) as cts,";
            query += " SUM(P.gratifica) as gratifica,SUM(P.total_ingresos) as total_ingresos, ";
            query += " L.desfonpen, ";
            
            /*if (bd_prov.Equals("agromango"))
            {
                query += " (SUM(P.sppao)-sum(IFNULL(Y.valor,0)+IFNULL(S.valor,0)+IFNULL(T.valor,0))*(K.cffonpen/100)) as sppao, ";
            }
            else { query += " SUM(P.sppao) as sppao, "; }*/
            query += " SUM(P.sppao) as sppao, ";

            query += " SUM(P.sppav) as sppav, ";
            
            /*if (bd_prov.Equals("agromango"))
            {
                query += " (SUM(P.sppcp)-sum(IFNULL(Y.valor,0)+IFNULL(S.valor,0)+IFNULL(T.valor,0))*(K.cvfonpen/100)) as sppcp, ";
                query += " (SUM(P.sppps)-sum(IFNULL(Y.valor,0)+IFNULL(S.valor,0)+IFNULL(T.valor,0))*(K.psfonpen/100)) as sppps, ";
            }
            else { query += " SUM(P.sppcp) as sppps,SUM(P.sppps) as sppps, "; }*/
            query += " SUM(P.sppcp) as sppps,SUM(P.sppps) as sppps, ";

            query += " SUM(P.snp) as snp,SUM(P.esvida) as esvida,SUM(P.quicat) as quicat,";
            query += " SUM(P.adelanto) as adelanto,SUM(P.sindica) as sindica,SUM(P.desjud) as desjud, ";
            query += " SUM(P.despres) as despres,SUM(P.desban) as desban,SUM(desseg) as desseg,SUM(P.desnodedu) as desnodedu,";
            query += " SUM(P.desdedu) as desdedu, ";

            /*if (bd_prov.Equals("agromango"))
            {
                query += " SUM(P.total_descuentos)-(sum(IFNULL(Y.valor,0)+IFNULL(S.valor,0)+IFNULL(T.valor,0))*(K.cffonpen/100)) as total_descuentos, ";
            }
            else { query += " SUM(P.total_descuentos) as total_descuentos, "; }*/
            query += " SUM(P.total_descuentos) as total_descuentos, ";

            query += " SUM(P.sppavemp) as sppavemp, ";
            
            /*if (bd_prov.Equals("agromango"))
            {
                if (idtipoper == "O" || idtipoplan == "200") query += " SUM(P.essalud)-(sum(IFNULL(Y.valor,0)+IFNULL(S.valor,0)+IFNULL(T.valor,0))*0.04) as essalud, ";
                if (idtipoper == "E" || idtipoplan == "100") query += " SUM(P.essalud)-(sum(IFNULL(Y.valor,0)+IFNULL(S.valor,0)+IFNULL(T.valor,0))*0.09) as essalud, ";
            }
            else { query += " SUM(P.essalud) as essalud, "; }*/
            query += " SUM(P.essalud) as essalud, ";

            query += " SUM(P.essctr) as essctr,SUM(P.senati) as senati,";
            query += " SUM(P.aporemp) as aporemp,SUM(P.epssctr) as epssctr,SUM(P.sis) as sis,SUM(P.total_aporta) as total_aporta,";

            /*if (bd_prov.Equals("agromango"))
            {
                query += "(SUM(P.total_ingresos)-(SUM(P.total_descuentos)-(sum(IFNULL(Y.valor,0)+IFNULL(S.valor,0)+IFNULL(T.valor,0))*(K.cffonpen/100)))) as neto";
            }
            else { query += " SUM(P.neto) as neto "; }*/
            query += " SUM(P.neto) as neto ";

            query += " from plan_ P inner join ciafile Q on P.idcia=Q.idcia ";
            query += " inner join calplan R on P.anio=R.anio and P.messem=R.messem and P.idtipoper=R.idtipoper and P.idtipocal=R.idtipocal and P.idcia=R.idcia ";
            query += " left join fonpen L on P.idfonpen=L.idfonpen ";
            query += " inner join tipoplan U on P.idtipoplan=U.idtipoplan  ";
            query += " inner join perplan A on P.idcia=A.idcia and P.idperplan=A.idperplan ";
            query += " left join maesgen D on D.idmaesgen='008' and A.seccion=D.clavemaesgen ";
            query += " left join labper E on P.idcia=E.idcia and A.idlabper=E.idlabper and A.idtipoper=E.idtipoper ";
            query += " left join tipoper B on A.idtipoper=B.idtipoper ";
            query += " left join maesgen C on C.idmaesgen='002' and A.tipdoc=C.clavemaesgen  ";

            /*if (bd_prov.Equals("agromango"))
            {
                query += " left join conbol Y on P.idperplan=Y.idperplan and P.idcia=Y.idcia and P.anio=Y.anio and P.messem=Y.messem and P.idtipoper=Y.idtipoper ";
                query += " and P.idtipocal=Y.idtipocal and P.idtipoplan=Y.idtipoplan and Y.idconplan = '0500' ";
                query += " left join conbol S on P.idperplan=S.idperplan and P.idcia=S.idcia and P.anio=S.anio and P.messem=S.messem and P.idtipoper=S.idtipoper ";
                query += " and P.idtipocal=S.idtipocal and P.idtipoplan=S.idtipoplan and S.idconplan = '0600' ";
                query += " left join conbol T on P.idperplan=T.idperplan and P.idcia=T.idcia and P.anio=T.anio and P.messem=T.messem and P.idtipoper=T.idtipoper ";
                query += " and P.idtipocal=T.idtipocal and P.idtipoplan=T.idtipoplan and T.idconplan = '0700' ";

                query += " inner join fonpenper J ON J.idperplan=P.idperplan AND J.idcia=P.idcia and J.statefonpenper='V' inner join fonpen K ON K.idfonpen=J.idfonpen ";
            }*/

            query += " where R.aniopdt='" + @aniopdt + "' and R.mespdt='" + @mespdt + "'  ";
            query += " and P.idcia='" + @idcia + "' and P.idtipoper='" + @idtipoper + "' ";
            query += " and P.idtipoplan='" + @idtipoplan + "' ";
            //query += " group by A.idperplan,C.Parm1maesgen,A.nrodoc,A.apepat,A.apemat,A.nombres,D.desmaesgen,E.deslabper,T.desfonpen";
            query += " group by A.idperplan,C.Parm1maesgen,A.nrodoc,A.apepat,A.apemat,A.nombres,D.desmaesgen,E.deslabper";

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
            dgvdetalle.Columns[13].HeaderText = "H.Ext. 25%";
            dgvdetalle.Columns[14].HeaderText = "H.Ext. 35%";
            dgvdetalle.Columns[15].HeaderText = "H.Ext. 100%";

            dgvdetalle.Columns[16].HeaderText = "Rem. Básica";
            dgvdetalle.Columns[17].HeaderText = "Jornal Básico";
            dgvdetalle.Columns[18].HeaderText = "Dom.";
            dgvdetalle.Columns[19].HeaderText = "Dias Vac.";

            dgvdetalle.Columns[20].HeaderText = "Rem.Vac.";
            dgvdetalle.Columns[21].HeaderText = "Comisión / Destajo";
            dgvdetalle.Columns[22].HeaderText = "Imp.H.Ext. 25%";
            dgvdetalle.Columns[23].HeaderText = "Imp.H.Ext. 35%";
            dgvdetalle.Columns[24].HeaderText = "Imp.H.Ext. 100%";
            dgvdetalle.Columns[25].HeaderText = "Asig. Familiar";
            dgvdetalle.Columns[26].HeaderText = "Asig. Escolar";
            dgvdetalle.Columns[27].HeaderText = "Otras Asig.";
            dgvdetalle.Columns[28].HeaderText = "Reintegro";
            dgvdetalle.Columns[29].HeaderText = "Bonificación";
            dgvdetalle.Columns[30].HeaderText = "C.T.S.";
            dgvdetalle.Columns[31].HeaderText = "Gratifiación";

            dgvdetalle.Columns[32].HeaderText = "Total Ingresos";
            dgvdetalle.Columns[33].HeaderText = "Fondo Pensiones";
            dgvdetalle.Columns[34].HeaderText = "SPP. Aport. Oblig.";
            dgvdetalle.Columns[35].HeaderText = "SPP. Aport. Vol.";
            dgvdetalle.Columns[36].HeaderText = "SPP Com. Porcentual";
            dgvdetalle.Columns[37].HeaderText = "SPP Prima Seguro";
            dgvdetalle.Columns[38].HeaderText = "S.N.P.";
            dgvdetalle.Columns[39].HeaderText = "EsVida";
            dgvdetalle.Columns[40].HeaderText = "5ta. Categoría";
            dgvdetalle.Columns[41].HeaderText = "Adelando";
            dgvdetalle.Columns[42].HeaderText = "Sindicato";
            dgvdetalle.Columns[43].HeaderText = "Descuento Judicial";

            dgvdetalle.Columns[44].HeaderText = "Préstamo Interno";
            dgvdetalle.Columns[45].HeaderText = "Convenio Bancario";
            dgvdetalle.Columns[46].HeaderText = "Seguro Privado";
            dgvdetalle.Columns[47].HeaderText = "Dscto. No Deducible";
            dgvdetalle.Columns[48].HeaderText = "Dscto Deducible";
            dgvdetalle.Columns[49].HeaderText = "Total Descuentos";

            dgvdetalle.Columns[50].HeaderText = "Aport.SPP Vol. Empleador";
            dgvdetalle.Columns[51].HeaderText = "EsSalud";
            dgvdetalle.Columns[52].HeaderText = "EsSalud - SCTR";
            dgvdetalle.Columns[53].HeaderText = "SENATI";
            dgvdetalle.Columns[54].HeaderText = "Otras Aportaciones";
            dgvdetalle.Columns[55].HeaderText = "EPS - SCTR";
            dgvdetalle.Columns[56].HeaderText = "SIS";
            dgvdetalle.Columns[57].HeaderText = "Total Aportaciones";
            dgvdetalle.Columns[58].HeaderText = "Neto a Recibir";

            dgvdetalle.Columns["idperplan"].Frozen = true;
            dgvdetalle.Columns["cortotipodoc"].Frozen = true;
            dgvdetalle.Columns["nrodoc"].Frozen = true;
            dgvdetalle.Columns["nombre"].Frozen = true;

            int i;

            for (i = 9; i < 20; i++)
            {
                dgvdetalle.Columns[i].DefaultCellStyle.Format = "###,###.##";
                dgvdetalle.Columns[i].DefaultCellStyle.Alignment = DataGridViewContentAlignment.BottomRight;
                dgvdetalle.Columns[i].Width = 75;
            }

            dgvdetalle.Columns[21].DefaultCellStyle.Format = "###,###.##";
            dgvdetalle.Columns[21].DefaultCellStyle.Alignment = DataGridViewContentAlignment.BottomRight;
            dgvdetalle.Columns[21].Width = 75;

            for (i = 20; i < 33; i++)
            {
                dgvdetalle.Columns[i].DefaultCellStyle.Format = "###,###.##";
                dgvdetalle.Columns[i].DefaultCellStyle.Alignment = DataGridViewContentAlignment.BottomRight;
                dgvdetalle.Columns[i].Width = 75;
            }

            for (i = 34; i < 59; i++)
            {
                dgvdetalle.Columns[i].DefaultCellStyle.Format = "###,###.##";
                dgvdetalle.Columns[i].DefaultCellStyle.Alignment = DataGridViewContentAlignment.BottomRight;
                dgvdetalle.Columns[i].Width = 75;
            }


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

            dgvdetalle.Columns[33].Width = 75;

            conexion.Close();



        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtAnio_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {

                ui_ListaPlan();

            }
        }

        private void cmbTipoTrabajador_SelectedIndexChanged(object sender, EventArgs e)
        {
            ui_ListaPlan();
        }

        private void cmbTipoPlan_SelectedIndexChanged(object sender, EventArgs e)
        {
            ui_ListaPlan();
        }

        private void cmbMes_SelectedIndexChanged(object sender, EventArgs e)
        {
            ui_ListaPlan();
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            Exporta exporta = new Exporta();
            exporta.Excel_FromDataGridView(dgvdetalle);
        }

        private void txtAnio_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }

        private void ui_ListaPlan_fechas()
        {
            Funciones funciones = new Funciones();
            GlobalVariables globalVariable = new GlobalVariables();
            DataTable dtplan = new DataTable();

            string idtipoper = funciones.getValorComboBox(cmbTipoTrabajador, 1);
            string idtipoplan = funciones.getValorComboBox(cmbTipoPlan, 3);
            string idcia = globalVariable.getValorCia();
            string aniopdt = txtAnio.Text;
            string mespdt = funciones.getValorComboBox(cmbMes, 2);
            string ruc = globalVariable.getValorRucCia();

            string fechai = maskedTextBox2.Text;
            string fechaf = maskedTextBox1.Text;
            string mesi = maskedTextBox3.Text;
            string mesf = maskedTextBox4.Text;
            string dato1 = (mesi + '-' + fechai).Trim();
            string dato2 = (mesf + '-' + fechaf).Trim();

            string query = "call Pl_BusqConsolidadoMeses('" + @idcia + "','" + @idtipoper + "','" + @idtipoplan + "','" + @dato1 + "','" + @dato2 + "')";
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
            dgvdetalle.Columns[13].HeaderText = "H.Ext. 25%";
            dgvdetalle.Columns[14].HeaderText = "H.Ext. 35%";
            dgvdetalle.Columns[15].HeaderText = "H.Ext. 100%";

            dgvdetalle.Columns[16].HeaderText = "Rem. Básica";
            dgvdetalle.Columns[17].HeaderText = "Jornal Básico";
            dgvdetalle.Columns[18].HeaderText = "Dom.";
            dgvdetalle.Columns[19].HeaderText = "Dias Vac.";

            dgvdetalle.Columns[20].HeaderText = "Rem.Vac.";
            dgvdetalle.Columns[21].HeaderText = "Comisión / Destajo";
            dgvdetalle.Columns[22].HeaderText = "Imp.H.Ext. 25%";
            dgvdetalle.Columns[23].HeaderText = "Imp.H.Ext. 35%";
            dgvdetalle.Columns[24].HeaderText = "Imp.H.Ext. 100%";
            dgvdetalle.Columns[25].HeaderText = "Asig. Familiar";
            dgvdetalle.Columns[26].HeaderText = "Asig. Escolar";
            dgvdetalle.Columns[27].HeaderText = "Otras Asig.";
            dgvdetalle.Columns[28].HeaderText = "Reintegro";
            dgvdetalle.Columns[29].HeaderText = "Bonificación";
            dgvdetalle.Columns[30].HeaderText = "C.T.S.";
            dgvdetalle.Columns[31].HeaderText = "Gratifiación";

            dgvdetalle.Columns[32].HeaderText = "Total Ingresos";
            dgvdetalle.Columns[33].HeaderText = "Fondo Pensiones";
            dgvdetalle.Columns[34].HeaderText = "SPP. Aport. Oblig.";
            dgvdetalle.Columns[35].HeaderText = "SPP. Aport. Vol.";
            dgvdetalle.Columns[36].HeaderText = "SPP Com. Porcentual";
            dgvdetalle.Columns[37].HeaderText = "SPP Prima Seguro";
            dgvdetalle.Columns[38].HeaderText = "S.N.P.";
            dgvdetalle.Columns[39].HeaderText = "EsVida";
            dgvdetalle.Columns[40].HeaderText = "5ta. Categoría";
            dgvdetalle.Columns[41].HeaderText = "Adelando";
            dgvdetalle.Columns[42].HeaderText = "Sindicato";
            dgvdetalle.Columns[43].HeaderText = "Descuento Judicial";

            dgvdetalle.Columns[44].HeaderText = "Préstamo Interno";
            dgvdetalle.Columns[45].HeaderText = "Convenio Bancario";
            dgvdetalle.Columns[46].HeaderText = "Seguro Privado";
            dgvdetalle.Columns[47].HeaderText = "Dscto. No Deducible";
            dgvdetalle.Columns[48].HeaderText = "Dscto Deducible";
            dgvdetalle.Columns[49].HeaderText = "Total Descuentos";

            dgvdetalle.Columns[50].HeaderText = "Aport.SPP Vol. Empleador";
            dgvdetalle.Columns[51].HeaderText = "EsSalud";
            dgvdetalle.Columns[52].HeaderText = "EsSalud - SCTR";
            dgvdetalle.Columns[53].HeaderText = "SENATI";
            dgvdetalle.Columns[54].HeaderText = "Otras Aportaciones";
            dgvdetalle.Columns[55].HeaderText = "EPS - SCTR";
            dgvdetalle.Columns[56].HeaderText = "SIS";
            dgvdetalle.Columns[57].HeaderText = "Total Aportaciones";
            dgvdetalle.Columns[58].HeaderText = "Neto a Recibir";

            dgvdetalle.Columns["idperplan"].Frozen = true;
            dgvdetalle.Columns["cortotipodoc"].Frozen = true;
            dgvdetalle.Columns["nrodoc"].Frozen = true;
            dgvdetalle.Columns["nombre"].Frozen = true;

            int i;

            for (i = 9; i < 20; i++)
            {
                dgvdetalle.Columns[i].DefaultCellStyle.Format = "###,###.##";
                dgvdetalle.Columns[i].DefaultCellStyle.Alignment = DataGridViewContentAlignment.BottomRight;
                dgvdetalle.Columns[i].Width = 75;
            }

            dgvdetalle.Columns[21].DefaultCellStyle.Format = "###,###.##";
            dgvdetalle.Columns[21].DefaultCellStyle.Alignment = DataGridViewContentAlignment.BottomRight;
            dgvdetalle.Columns[21].Width = 75;

            for (i = 20; i < 33; i++)
            {
                dgvdetalle.Columns[i].DefaultCellStyle.Format = "###,###.##";
                dgvdetalle.Columns[i].DefaultCellStyle.Alignment = DataGridViewContentAlignment.BottomRight;
                dgvdetalle.Columns[i].Width = 75;
            }

            for (i = 34; i < 59; i++)
            {
                dgvdetalle.Columns[i].DefaultCellStyle.Format = "###,###.##";
                dgvdetalle.Columns[i].DefaultCellStyle.Alignment = DataGridViewContentAlignment.BottomRight;
                dgvdetalle.Columns[i].Width = 75;
            }


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

            dgvdetalle.Columns[33].Width = 75;

            conexion.Close();



        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                groupBox3.Enabled = false;
                groupBox2.Enabled = true;
                checkBox2.Checked = false;
            }
            else
            {
                groupBox3.Enabled = true;
                groupBox2.Enabled = false;
            }


        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked == true)
            {
                groupBox2.Enabled = false;
                groupBox3.Enabled = true;
                checkBox1.Checked = false;

            }
            else
            {
                groupBox2.Enabled = true;
                groupBox3.Enabled = false;
                // checkBox2.Checked = false;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                ui_ListaPlan_fechas();
            }
            else
            { ui_ListaPlan(); }
        }

        private void toolstripform_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }
    }
}