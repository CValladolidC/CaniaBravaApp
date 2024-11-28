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
    public partial class ui_plantrab : Form
    {
        private TextBox TextBoxActivo;

        public TextBox _TextBoxActivo
        {
            get { return TextBoxActivo; }
            set { TextBoxActivo = value; }
        }

        private Form FormPadre;

        public Form _FormPadre
        {
            get { return FormPadre; }
            set { FormPadre = value; }
        }

        public ui_plantrab()
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

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ui_ListaPlan()
        {
            Funciones funciones = new Funciones();
            GlobalVariables globalVariable = new GlobalVariables();
            DataTable dtplan = new DataTable();

            string idtipoper = funciones.getValorComboBox(cmbTipoTrabajador, 1);
            string anio = txtAnio.Text;
            string idtipocal = funciones.getValorComboBox(cmbTipoCal, 1);
            string idtipoplan = funciones.getValorComboBox(cmbTipoPlan, 3);
            string idcia = globalVariable.getValorCia();
            string rucemp = funciones.getValorComboBox(cmbEmpleador, 11);
            string estane = funciones.getValorComboBox(cmbEstablecimiento, 4);
            string idperplan = txtCodigoInterno.Text;

            string query = "  Select D.desmaesgen as seccion,E.deslabper as ocupacion,";
            query = query + " CONCAT(P.messem,'/',P.anio) as periodo,P.finilab,P.ffinlab,";
            query = query + " P.diasefelab,P.diassubsi,P.diasnosubsi,P.dias,P.hext25,P.hext35,";
            query = query + " P.hext100,P.rembas,P.jorbas,P.dom,P.finivac,P.ffinvac,P.diasvaca,P.remvac,P.comides, ";
            query = query + " P.imphext25,P.imphext35,P.imphext100,P.asigfam,P.asigesc,P.asigotros,";
            query = query + " P.reintegro,P.bonifica,P.cts,P.gratifica,P.total_ingresos, ";
            query = query + " T.desfonpen,P.sppao,P.sppav,P.sppcp,P.sppps,P.snp,P.esvida,P.quicat,";
            query = query + " P.adelanto,P.sindica,P.desjud,P.desnodedu,P.desdedu,P.total_descuentos, ";
            query = query + " P.sppavemp,P.essalud,P.essctr,P.senati,P.aporemp,P.epssctr,";
            query = query + " P.sis,P.total_aporta,P.neto,P.messem,P.anio,P.idperplan from plan_ P ";
            query = query + " left join labper E on P.idcia=E.idcia and P.idlabper=E.idlabper and P.idtipoper=E.idtipoper ";
            query = query + " inner join ciafile Q on P.idcia=Q.idcia ";
            query = query + " inner join calplan S on P.anio=S.anio and P.messem=S.messem ";
            query = query + " and P.idtipoper=S.idtipoper and P.idtipocal=S.idtipocal and P.idcia=S.idcia ";
            query = query + " left join fonpen T on P.idfonpen=T.idfonpen ";
            query = query + " left join maesgen D on D.idmaesgen='008' and P.seccion=D.clavemaesgen ";
            query = query + " inner join tipoplan U on P.idtipoplan=U.idtipoplan  ";
            query = query + " inner join perplan A on P.idcia=A.idcia and P.idperplan=A.idperplan ";
            query = query + " left join tipoper B on A.idtipoper=B.idtipoper ";
            query = query + " where P.rucemp='" + @rucemp + "' and P.estane='" + @estane + "' ";
            query = query + " and P.anio='" + @anio + "' and P.idperplan='" + @idperplan + "' and ";
            query = query + " P.idcia='" + @idcia + "' and P.idtipoper='" + @idtipoper + "' ";
            query = query + " and P.idtipoplan='" + @idtipoplan + "' and P.idtipocal='" + @idtipocal + "' ";
            query = query + " order by periodo desc;";

            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();
            SqlDataAdapter da = new SqlDataAdapter();

            da.SelectCommand = new SqlCommand(query, conexion);
            da.Fill(dtplan);

            funciones.formatearDataGridView(dgvdetalle);
            dgvdetalle.DataSource = dtplan;
            dgvdetalle.Columns[0].HeaderText = "Sección";
            dgvdetalle.Columns[1].HeaderText = "Ocupación";
            dgvdetalle.Columns[2].HeaderText = "Periodo";

            dgvdetalle.Columns[3].HeaderText = "Fecha Inicio";
            dgvdetalle.Columns[4].HeaderText = "Fecha Cese";

            dgvdetalle.Columns[5].HeaderText = "Dias Efe. Lab.";
            dgvdetalle.Columns[6].HeaderText = "Dias Subsi.";
            dgvdetalle.Columns[7].HeaderText = "Dias No Subsi";
            dgvdetalle.Columns[8].HeaderText = "Total Dias";
            dgvdetalle.Columns[9].HeaderText = "H.Ext. 25%";
            dgvdetalle.Columns[10].HeaderText = "H.Ext. 35%";
            dgvdetalle.Columns[11].HeaderText = "H.Ext. 100%";

            dgvdetalle.Columns[12].HeaderText = "Rem. Básica";
            dgvdetalle.Columns[13].HeaderText = "Jornal Básico";
            dgvdetalle.Columns[14].HeaderText = "Dom.";
            dgvdetalle.Columns[15].HeaderText = "F. Inicio Vac.";
            dgvdetalle.Columns[16].HeaderText = "F. Fin Vac.";
            dgvdetalle.Columns[17].HeaderText = "Dias Vac.";

            dgvdetalle.Columns[18].HeaderText = "Rem.Vac.";
            dgvdetalle.Columns[19].HeaderText = "Comisión / Destajo";
            dgvdetalle.Columns[20].HeaderText = "Imp.H.Ext. 25%";
            dgvdetalle.Columns[21].HeaderText = "Imp.H.Ext. 35%";
            dgvdetalle.Columns[22].HeaderText = "Imp.H.Ext. 100%";
            dgvdetalle.Columns[23].HeaderText = "Asig. Familiar";
            dgvdetalle.Columns[24].HeaderText = "Asig. Escolar";
            dgvdetalle.Columns[25].HeaderText = "Otras Asig.";
            dgvdetalle.Columns[26].HeaderText = "Reintegro";
            dgvdetalle.Columns[27].HeaderText = "Bonificación";
            dgvdetalle.Columns[28].HeaderText = "C.T.S.";
            dgvdetalle.Columns[29].HeaderText = "Gratificación";
            dgvdetalle.Columns[30].HeaderText = "Total Ingresos";

            dgvdetalle.Columns[31].HeaderText = "Fondo Pensiones";

            dgvdetalle.Columns[32].HeaderText = "SPP. Aport. Oblig.";
            dgvdetalle.Columns[33].HeaderText = "SPP. Aport. Vol.";
            dgvdetalle.Columns[34].HeaderText = "SPP Com. Porcentual";
            dgvdetalle.Columns[35].HeaderText = "SPP Prima Seguro";
            dgvdetalle.Columns[36].HeaderText = "S.N.P.";
            dgvdetalle.Columns[37].HeaderText = "EsVida";
            dgvdetalle.Columns[38].HeaderText = "5ta. Categoría";
            dgvdetalle.Columns[39].HeaderText = "Adelando";
            dgvdetalle.Columns[40].HeaderText = "Sindicato";
            dgvdetalle.Columns[41].HeaderText = "Descuento Judicial";
            dgvdetalle.Columns[42].HeaderText = "Dscto. No Deducible";
            dgvdetalle.Columns[43].HeaderText = "Dscto Deducible";
            dgvdetalle.Columns[44].HeaderText = "Total Descuentos";

            dgvdetalle.Columns[45].HeaderText = "Aport.SPP Vol. Empleador";
            dgvdetalle.Columns[46].HeaderText = "EsSalud";
            dgvdetalle.Columns[47].HeaderText = "EsSalud - SCTR";
            dgvdetalle.Columns[48].HeaderText = "SENATI";
            dgvdetalle.Columns[49].HeaderText = "Otras Aportaciones";
            dgvdetalle.Columns[50].HeaderText = "EPS - SCTR";
            dgvdetalle.Columns[51].HeaderText = "SIS";
            dgvdetalle.Columns[52].HeaderText = "Total Aportaciones";
            dgvdetalle.Columns[53].HeaderText = "Neto a Recibir";


            dgvdetalle.Columns["seccion"].Frozen = true;
            dgvdetalle.Columns["ocupacion"].Frozen = true;
            dgvdetalle.Columns["periodo"].Frozen = true;
            dgvdetalle.Columns["messem"].Visible = false;
            dgvdetalle.Columns["anio"].Visible = false;
            dgvdetalle.Columns["idperplan"].Visible = false;

            int i;

            for (i = 5; i < 15; i++)
            {
                dgvdetalle.Columns[i].DefaultCellStyle.Format = "###,###.##";
                dgvdetalle.Columns[i].DefaultCellStyle.Alignment = DataGridViewContentAlignment.BottomRight;
                dgvdetalle.Columns[i].Width = 70;
            }

            dgvdetalle.Columns[17].DefaultCellStyle.Format = "###,###.##";
            dgvdetalle.Columns[17].DefaultCellStyle.Alignment = DataGridViewContentAlignment.BottomRight;
            dgvdetalle.Columns[17].Width = 70;

            for (i = 18; i < 31; i++)
            {
                dgvdetalle.Columns[i].DefaultCellStyle.Format = "###,###.##";
                dgvdetalle.Columns[i].DefaultCellStyle.Alignment = DataGridViewContentAlignment.BottomRight;
                dgvdetalle.Columns[i].Width = 70;
            }

            for (i = 32; i < 54; i++)
            {
                dgvdetalle.Columns[i].DefaultCellStyle.Format = "###,###.##";
                dgvdetalle.Columns[i].DefaultCellStyle.Alignment = DataGridViewContentAlignment.BottomRight;
                dgvdetalle.Columns[i].Width = 70;
            }

            float totalIngresos = float.Parse(funciones.sumaColumnaDataGridView(dgvdetalle, 30));
            float totalDescuentos = float.Parse(funciones.sumaColumnaDataGridView(dgvdetalle, 44));
            float totalAportes = float.Parse(funciones.sumaColumnaDataGridView(dgvdetalle, 52));
            float neto = float.Parse(funciones.sumaColumnaDataGridView(dgvdetalle, 53));

            txtIngresos.Text = Convert.ToString(totalIngresos);
            txtDescuentos.Text = Convert.ToString(totalDescuentos);
            txtAportaciones.Text = Convert.ToString(totalAportes);

            dgvdetalle.Columns[0].Width = 100;
            dgvdetalle.Columns[1].Width = 100;
            dgvdetalle.Columns[2].Width = 75;
            dgvdetalle.Columns[3].Width = 75;
            dgvdetalle.Columns[4].Width = 75;

            dgvdetalle.Columns[15].Width = 75;
            dgvdetalle.Columns[16].Width = 75;
            dgvdetalle.Columns[31].Width = 75;

            conexion.Close();



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

        private void cmbTipoTrabajador_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtCodigoInterno.Clear();
            txtNombres.Clear();

        }

        private void cmbTipoPlan_SelectedIndexChanged(object sender, EventArgs e)
        {
            Funciones funciones = new Funciones();
            GlobalVariables variables = new GlobalVariables();
            string idcia = variables.getValorCia();
            string idtipoplan = funciones.getValorComboBox(cmbTipoPlan, 3);
            ui_listaTipoCal(idcia, idtipoplan);
            txtCodigoInterno.Clear();
            txtNombres.Clear();
        }

        private void cmbTipoCal_SelectedIndexChanged(object sender, EventArgs e)
        {

            txtCodigoInterno.Clear();
            txtNombres.Clear();
            txtAnio.Focus();

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

        }

        private void ui_plantrab_Load(object sender, EventArgs e)
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
            txtAnio.Text = DateTime.Now.Year.ToString();

        }

        private void txtCodigoInterno_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                GlobalVariables gv = new GlobalVariables();
                Funciones fn = new Funciones();
                string idcia = gv.getValorCia();
                string idtipoper = fn.getValorComboBox(cmbTipoTrabajador, 1);
                string idtipoplan = fn.getValorComboBox(cmbTipoPlan, 3);
                string idtipocal = fn.getValorComboBox(cmbTipoCal, 1);
                string rucemp = fn.getValorComboBox(cmbEmpleador, 11);
                string estane = fn.getValorComboBox(cmbEstablecimiento, 4);
                string anio = txtAnio.Text;
                string cadenaBusqueda = string.Empty;
                this._TextBoxActivo = txtCodigoInterno;

                string condicionAdicional = " and A.idtipoplan='" + @idtipoplan + "' and A.rucemp='" + @rucemp + "' ";
                condicionAdicional = condicionAdicional + " and A.estane='" + @estane + "' and CONCAT(A.idperplan,A.idcia) ";
                condicionAdicional = condicionAdicional + " in (select CONCAT(idperplan,idcia) from plan_ where ";
                condicionAdicional = condicionAdicional + " idtipoper='" + @idtipoper + "' and anio='" + @anio + "' ";
                condicionAdicional = condicionAdicional + " and idtipocal='" + @idtipocal + "' and idcia='" + @idcia + "' ";
                condicionAdicional = condicionAdicional + " and idtipoplan='" + @idtipoplan + "' ) ";

                FiltrosMaestros filtros = new FiltrosMaestros();
                filtros.filtrarPerPlan("ui_plantrab", this, txtCodigoInterno, idcia, idtipoper, "", cadenaBusqueda, condicionAdicional);

            }
        }

        private void txtCodigoInterno_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                if (txtCodigoInterno.Text.Trim() != string.Empty)
                {
                    GlobalVariables gv = new GlobalVariables();
                    string idcia = gv.getValorCia();
                    string idperplan = txtCodigoInterno.Text.Trim();
                    PerPlan perplan = new PerPlan();
                    string codigoInterno = perplan.ui_getDatosPerPlan(idcia, idperplan, "0");
                    if (codigoInterno == string.Empty)
                    {
                        MessageBox.Show("Código no registrado del trabajador", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txtCodigoInterno.Clear();
                        txtNombres.Clear();
                        txtCodigoInterno.Focus();

                    }
                    else
                    {
                        txtCodigoInterno.Text = perplan.ui_getDatosPerPlan(idcia, idperplan, "0");
                        txtNombres.Text = perplan.ui_getDatosPerPlan(idcia, idperplan, "3");

                    }

                }
                else
                {
                    txtCodigoInterno.Clear();
                    txtNombres.Clear();
                    txtCodigoInterno.Focus();
                }
                ui_ListaPlan();
            }
        }

        private void txtCodigoInterno_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtAnio_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                e.Handled = true;
                txtCodigoInterno.Focus();
                ui_ListaPlan();
            }
        }
    }
}