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
    public partial class ui_rescuarta : Form
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

        public ui_rescuarta()
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
                string clasepadre = "ui_rescuarta";
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

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            try
            {
                Funciones funciones = new Funciones();
                DataTable dtreporte = new DataTable();
                string idcia = this._idcia;
                string idproddes = funciones.getValorComboBox(cmbProducto, 2).Trim();
                string idzontra = funciones.getValorComboBox(cmbZona, 2).Trim();
                string idtipocal = this._idtipocal;
                string idtipoper = this._idtipoper;
                string emplea = funciones.getValorComboBox(cmbEmpleador, 11);
                string estane = funciones.getValorComboBox(cmbEstablecimiento, 4);
                string periodo = txtMesSem.Text.Trim() + funciones.replicateCadena(" ", 6);
                string messem = periodo.Substring(0, 2);
                string anio = periodo.Substring(3, 4);
                string condiTipo = string.Empty;
                string condiprod = string.Empty;
                string condizon = string.Empty;
                string fechaini;
                string fechafin;

                if (idproddes != "X")
                {
                    condiprod = " and A.idproddes='" + @idproddes + "' ";
                }

                if (idzontra != "X")
                {
                    condizon = " and A.idzontra='" + @idzontra + "' ";
                }

                if (cmbTipo.Text.Equals("Por Periodo Laboral"))
                {
                    fechaini = txtFechaIni.Text;
                    fechafin = txtFechaFin.Text;
                    condiTipo = " and A.messem='" + @messem + "' and A.anio='" + @anio + "' ";
                }
                else
                {
                    fechaini = txtFechaInicial.Text;
                    fechafin = txtFechaFinal.Text;
                    condiTipo = " and A.fecha >= ";
                    condiTipo = condiTipo + " STR_TO_DATE('" + @fechaini + "', '%d/%m/%Y')  and  ";
                    condiTipo = condiTipo + " A.fecha <= STR_TO_DATE('" + @fechafin + "', '%d/%m/%Y') ";
                }

                string query = " select F.descia,F.ruccia,'" + @fechaini + "' as fechaini,'" + @fechafin + "' as fechafin,";
                query = query + " A.idproddes,P.desproddes,A.idzontra,";
                query = query + " C.deszontra,A.idlabret,L.deslabret, ";
                query = query + " A.idcencos,M.descencos,M.codaux,SUM(A.total) as total from desret A  ";
                query = query + " left join ciafile F on A.idcia=F.idcia ";
                query = query + " left join proddes P on A.idproddes=P.idproddes and A.idcia=P.idcia ";
                query = query + " left join zontra C on A.idzontra=C.idzontra and A.idcia=C.idcia ";
                query = query + " left join labret L on A.idlabret=L.idlabret and A.idcia=L.idcia ";
                query = query + " left join cencos M on A.idcencos=M.idcencos and A.idcia=M.idcia ";
                query = query + " where A.idcia='" + @idcia + "' and A.idtipocal='" + @idtipocal + "' ";
                query = query + " and A.idtipoper='" + @idtipoper + "' ";
                query = query + condiprod + condizon;
                query = query + " and A.emplea='" + @emplea + "' and A.estane='" + @estane + "' ";
                query = query + condiTipo;
                query = query + " group by F.descia,F.ruccia,A.idproddes,P.desproddes,A.idzontra,";
                query = query + " C.deszontra,A.idlabret,L.deslabret, A.idcencos,M.descencos,M.codaux ";
                query = query + " order by F.descia,F.ruccia,A.idproddes,";
                query = query + " A.idzontra,A.idcencos,A.idlabret asc;";

                SqlConnection conexion = new SqlConnection();
                conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
                conexion.Open();
                SqlDataAdapter damov = new SqlDataAdapter();
                damov.SelectCommand = new SqlCommand(query, conexion);
                damov.Fill(dtreporte);
                if (dtreporte.Rows.Count > 0)
                {
                    cr.crrescuarta cr = new cr.crrescuarta();
                    ui_reporte ui_reporte = new ui_reporte();
                    ui_reporte.asignaDataTable(cr, dtreporte);
                    ui_reporte.Activate();
                    ui_reporte.BringToFront();
                    ui_reporte.ShowDialog();
                    ui_reporte.Dispose();
                }
                else
                {
                    MessageBox.Show("No existe información registrada en el criterio de búsqueda", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
                conexion.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }

        }

        private void ui_rescuarta_Load(object sender, EventArgs e)
        {
            Funciones funciones = new Funciones();
            GlobalVariables variables = new GlobalVariables();
            string query;
            string idcia = variables.getValorCia();
            this._idcia = idcia;
            this._idtipocal = "N";
            this._idtipoper = "O";
            query = "SELECT ruccia as clave,descia as descripcion FROM ciafile WHERE idcia='" + @idcia + "';";
            funciones.listaComboBox(query, cmbEmpleador, "");
            query = "SELECT idproddes as clave,desproddes as descripcion FROM proddes where idcia='" + @idcia + "' and stateproddes='V' order by 1 asc";
            funciones.listaComboBox(query, cmbProducto, "X");
            query = "SELECT idzontra as clave,deszontra as descripcion FROM zontra WHERE idcia='" + @idcia + "' order by 1 asc;";
            funciones.listaComboBox(query, cmbZona, "X");
            cmbTipo.Text = "Por Periodo Laboral";

        }

        private void cmbTipo_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtMesSem.Clear();

            if (cmbTipo.Text.Trim().Equals("Por Periodo Laboral"))
            {
                txtMesSem.Enabled = true;
                txtFechaInicial.Clear();
                txtFechaFinal.Clear();
                txtFechaInicial.Enabled = false;
                txtFechaFinal.Enabled = false;

            }
            else
            {
                txtMesSem.Enabled = false;
                txtFechaInicial.Text = DateTime.Now.ToString("dd/MM/yyyy");
                txtFechaFinal.Text = DateTime.Now.ToString("dd/MM/yyyy");
                txtFechaInicial.Enabled = true;
                txtFechaFinal.Enabled = true;

            }
        }
    }
}