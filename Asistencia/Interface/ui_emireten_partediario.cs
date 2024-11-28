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
    public partial class ui_emireten_partediario : Form
    {
        string _idtipocal;
        string _idtipoper;
        string _idcia;

        private MaskedTextBox TextBoxActivo;
        public MaskedTextBox _TextBoxActivo
        {
            get { return TextBoxActivo; }
            set { TextBoxActivo = value; }
        }

        public ui_emireten_partediario()
        {
            InitializeComponent();
        }

        private void txtMesSem_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                Funciones funciones = new Funciones();
                CalPlan calplan = new CalPlan();
                string idcia = this._idcia;
                string periodo = txtMesSem.Text.Trim() + funciones.replicateCadena(" ", 6);
                string messem = periodo.Substring(0, 2);
                string anio = periodo.Substring(3, 4);
                string idtipoper = this._idtipoper;
                string idtipocal = this._idtipocal;

                if (calplan.getDatosCalPlan(messem, anio, idtipoper, idcia, idtipocal, "FECHAINI") != "")
                {
                    txtFechaIni.Text = calplan.getDatosCalPlan(messem, anio, idtipoper, idcia, idtipocal, "FECHAINI");
                    txtFechaFin.Text = calplan.getDatosCalPlan(messem, anio, idtipoper, idcia, idtipocal, "FECHAFIN");
                    e.Handled = true;
                    btnImprimir.Select();
                    toolstripform.Focus();
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

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
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
            string idcia = this._idcia;
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
            string idcia = this._idcia;
            string ruccia = funciones.getValorComboBox(cmbEmpleador, 11);
            string squery_esta = "SELECT idestane as clave,desestane as descripcion FROM estane WHERE codemp='" + @ruccia + "' and idciafile='" + @idcia + "' order by idestane asc;";
            funciones.listaComboBox(squery_esta, cmbEstablecimiento, "");
            cmbEstablecimiento.Focus();
        }

        private void txtMesSem_KeyUp(object sender, KeyEventArgs e)
        {

        }

        private void txtMesSem_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                GlobalVariables variables = new GlobalVariables();
                string idtipoper = this._idtipoper;
                string idtipocal = this._idtipocal;
                string idcia = variables.getValorCia();
                string clasepadre = "ui_emireten_partediario";
                this._TextBoxActivo = txtMesSem;

                ui_buscarcalplan ui_buscarcalplan = new ui_buscarcalplan();
                ui_buscarcalplan._FormPadre = this;
                ui_buscarcalplan.setValores(idtipoper, idcia, clasepadre, idtipocal);
                ui_buscarcalplan.Activate();
                ui_buscarcalplan.BringToFront();
                ui_buscarcalplan.ShowDialog();
                ui_buscarcalplan.Dispose();
            }
        }

        private void ui_emireten_partediario_Load(object sender, EventArgs e)
        {
            Funciones funciones = new Funciones();
            GlobalVariables variables = new GlobalVariables();
            string query;
            string idcia = variables.getValorCia();
            this._idcia = idcia;
            query = "SELECT ruccia as clave,descia as descripcion FROM ciafile WHERE idcia='" + @idcia + "';";
            funciones.listaComboBox(query, cmbEmpleador, "");
            query = "SELECT idproddes as clave,desproddes as descripcion FROM proddes where idcia='" + @idcia + "' and stateproddes='V' order by 1 asc";
            funciones.listaComboBox(query, cmbProducto, "");
            query = "SELECT idzontra as clave,deszontra as descripcion FROM zontra WHERE idcia='" + @idcia + "' order by 1 asc;";
            funciones.listaComboBox(query, cmbZona, "");
            this._idtipocal = "N";
            this._idtipoper = "O";
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            Funciones funciones = new Funciones();
            string idcia = this._idcia;
            string idproddes = funciones.getValorComboBox(cmbProducto, 2);
            string idzontra = funciones.getValorComboBox(cmbZona, 2);
            string idtipocal = this._idtipocal;
            string idtipoper = this._idtipoper;
            string emplea = funciones.getValorComboBox(cmbEmpleador, 11);
            string estane = funciones.getValorComboBox(cmbEstablecimiento, 4);
            string periodo = txtMesSem.Text.Trim() + funciones.replicateCadena(" ", 6);
            string messem = periodo.Substring(0, 2);
            string anio = periodo.Substring(3, 4);
            string fechaini = txtFechaIni.Text;
            string fechafin = txtFechaFin.Text;
            string condicion;

            if (radioButtonPeriodo.Checked)
            {
                condicion = " and A.messem='" + @messem + "' and A.anio='" + @anio + "' ";
            }
            else
            {
                condicion = " and A.fecha >= STR_TO_DATE('" + @fechaini + "', '%d/%m/%Y')  and  ";
                condicion = condicion + " A.fecha<= STR_TO_DATE('" + @fechafin + "', '%d/%m/%Y') ";
            }

            string query = " select F.descia,F.ruccia,A.fecha,B.idperplan,D.Parm1maesgen as cortotipodoc,B.nrodoc,";
            query = query + " CONCAT(B.apepat,' ',B.apemat,' , ',B.nombres) as nombre,A.total,A.idproddes,";
            query = query + " P.desproddes,A.idzontra,C.deszontra,A.messem,A.anio,G.fechaini,G.fechafin from desret A  ";
            query = query + " left join perret B on A.idperplan=B.idperplan and A.idcia=B.idcia ";
            query = query + " left join maesgen D on D.idmaesgen='002' and B.tipdoc=D.clavemaesgen ";
            query = query + " left join ciafile F on A.idcia=F.idcia ";
            query = query + " left join proddes P on A.idproddes=P.idproddes and A.idcia=P.idcia ";
            query = query + " left join zontra C on A.idzontra=C.idzontra and A.idcia=C.idcia ";
            query = query + " left join calplan G on A.idcia=G.idcia and A.idtipocal=G.idtipocal and A.messem=G.messem and  ";
            query = query + " A.anio=G.anio and A.idtipoper=G.idtipoper  ";
            query = query + " where A.idcia='" + @idcia + "' and A.idtipocal='" + @idtipocal + "' ";
            query = query + " and A.idtipoper='" + @idtipoper + "' ";
            query = query + " and A.idproddes='" + @idproddes + "' and A.idzontra='" + @idzontra + "' ";
            query = query + " and A.emplea='" + @emplea + "' and A.estane='" + @estane + "' " + condicion;
            query = query + " order by F.descia,P.idproddes,C.idzontra,B.apepat asc,B.apemat asc,B.nombres asc,A.fecha asc;";

            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();
            try
            {
                using (SqlDataAdapter myDataAdapter = new SqlDataAdapter(query, conexion))
                {
                    DataSet dsRetenParDia = new DataSet();
                    cr.crretenpardia cr = new cr.crretenpardia();
                    myDataAdapter.Fill(dsRetenParDia, "dtreporte");

                    ui_reporte ui_reporte = new ui_reporte();
                    ui_reporte.asignaDataSet(cr, dsRetenParDia);
                    ui_reporte.Activate();
                    ui_reporte.BringToFront();
                    ui_reporte.ShowDialog();
                    ui_reporte.Dispose();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            conexion.Close();
        }

        private void radioButtonPeriodo_CheckedChanged(object sender, EventArgs e)
        {
            txtMesSem.Clear();
            txtFechaIni.Clear();
            txtFechaFin.Clear();
            txtMesSem.Enabled = true;
            txtFechaIni.Enabled = false;
            txtFechaFin.Enabled = false;
            txtMesSem.Focus();
        }

        private void radioButtonFechas_CheckedChanged(object sender, EventArgs e)
        {
            txtMesSem.Clear();
            txtFechaIni.Text = DateTime.Now.ToString("dd/MM/yyyy");
            txtFechaFin.Text = DateTime.Now.ToString("dd/MM/yyyy");
            txtMesSem.Enabled = false;
            txtFechaIni.Enabled = true;
            txtFechaFin.Enabled = true;
            txtFechaIni.Focus();
        }

        private void txtFechaIni_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                if (UtiFechas.IsDate(txtFechaIni.Text))
                {
                    e.Handled = true;
                    txtFechaFin.Focus();
                }
                else
                {
                    MessageBox.Show("Fecha Inicial no válida", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    e.Handled = true;
                    txtFechaIni.Focus();
                }
            }
        }

        private void txtFechaFin_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                if (UtiFechas.IsDate(txtFechaFin.Text))
                {
                    e.Handled = true;
                    toolstripform.Focus();
                }
                else
                {
                    MessageBox.Show("Fecha Final no válida", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    e.Handled = true;
                    txtFechaFin.Focus();
                }
            }
        }
    }
}