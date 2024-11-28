using System;
using System.Data;
using System.Windows.Forms;
using System.Configuration;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace CaniaBrava
{
    public partial class ui_rep_comedor : Form
    {
        GlobalVariables variables = new GlobalVariables();
        Funciones funciones = new Funciones();
        SqlConnection conexion = new SqlConnection();

        private TextBox TextBoxActivo;
        public TextBox _TextBoxActivo
        {
            get { return TextBoxActivo; }
            set { TextBoxActivo = value; }
        }

        public ui_rep_comedor()
        {
            InitializeComponent();

            if (funciones.VersionAssembly()) Application.ExitThread();
        }

        private void ui_rep_asistencia_Load(object sender, EventArgs e)
        {
            string query = " SELECT idusr AS clave,desusr AS descripcion FROM usrfile (NOLOCK) WHERE idusr LIKE 'COM_%' ";
            funciones.listaComboBoxUnCampo(query, cmbComedor, "X");

            query = " select a.idcia as clave, a.descia as descripcion from ciafile a (nolock) ";
            funciones.listaComboBox(query, cmbCia, "X");

            dtpFecfin.MinDate = dtpFecini.Value;
        }

        #region _Click
        private void btnSalir_Click(object sender, EventArgs e) { Close(); }
        #endregion

        private void dtpFecini_ValueChanged(object sender, EventArgs e)
        {
            dtpFecfin.MinDate = dtpFecini.Value;
            if (dtpFecini.Value > dtpFecfin.Value)
            {
                dtpFecfin.Value = dtpFecini.Value;
            }
        }

        private void btnGenerar_Click(object sender, EventArgs e)
        {
            btnGenerar.Enabled = false;
            loadingNext1.Visible = true;

            DataSet dscomedor = new DataSet();
            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();

            string query = string.Empty, parametros = string.Empty;
            string comedor = funciones.getValorComboBox(cmbComedor, 20);
            string cia = funciones.getValorComboBox(cmbCia, 2);
            string geren = funciones.getValorComboBox(cmbGerencia, 8);
            string area = funciones.getValorComboBox(cmbCencos, 8);
            string fec1 = string.Empty, fec2 = string.Empty, tipo = string.Empty;

            if (comedor.Trim() == "X   TODOS") { comedor = string.Empty; }
            if (cia == "X") { cia = string.Empty; }
            if (geren == "X   TODO") { geren = string.Empty; }
            if (area == "X   TODO") { area = string.Empty; }

            if (chkFechas.Checked)
            {
                fec1 = dtpFecini.Value.ToString("yyyy-MM-dd");
                fec2 = dtpFecfin.Value.ToString("yyyy-MM-dd");
            }

            if (chkPerfiles.Checked)
            {
                tipo = (rTrab.Checked) ? "I" : ((rInvi.Checked) ? "T" : "C");
            }

            parametros = "'" + cia + "','" + geren + "','" + area + "','" + txtCodigoInterno.Text.Trim() + "','" +
                fec1 + "','" + fec2 + "','" + tipo + "','" + comedor.Trim() + "' ";

            if (rDetallado.Checked) { query = "EXEC [SP_REPORTE_COM] " + parametros; }
            if (rDetCeco.Checked) { query = "EXEC SP_REPORTE_COM_DIA " + parametros; }
            if (rResumen.Checked) { query = "EXEC SP_REPORTE_COM_RES " + parametros; }
            if (rSemanal.Checked) { query = "EXEC SP_REPORTE_COM_SEM " + parametros; }
            if (resuCeco.Checked) { query = "EXEC SP_REPORTE_COM_CECO " + parametros; }

            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = new SqlCommand(query, conexion);
            da.SelectCommand.CommandTimeout = 360;
            da.Fill(dscomedor, "dtcomedor");

            ui_reporte ui_reporte = new ui_reporte();
            if (rDetallado.Checked)
            {
                cr.crcomedor cre = new cr.crcomedor();
                ui_reporte.asignaDataSet(cre, dscomedor);
            }
            else
            {
                if (rDetCeco.Checked)
                {
                    cr.crcomedorrep cre = new cr.crcomedorrep();
                    ui_reporte.asignaDataSet(cre, dscomedor);
                }
                else
                {
                    if (resuCeco.Checked)
                    {
                        da.Fill(dscomedor, "dtcomedorceco");
                        cr.crcomedorceco cre = new cr.crcomedorceco();
                        ui_reporte.asignaDataSet(cre, dscomedor);
                    }
                    else
                    {
                        cr.crcomedoracu cre = new cr.crcomedoracu();
                        ui_reporte.asignaDataSet(cre, dscomedor);
                    }
                }
            }
            ui_reporte.Activate();
            ui_reporte.BringToFront();
            ui_reporte.ShowDialog();
            ui_reporte.Dispose();

            loadingNext1.Visible = false;
            btnGenerar.Enabled = true;
        }

        private DataSet GetDatos()
        {
            DataSet ds = new DataSet();
            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();

            string query = "exec [SP_REPORTE_COM] ";
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = new SqlCommand(query, conexion);
            da.Fill(ds, "dtcomedor");

            return ds;
        }

        private void cmbCia_SelectedIndexChanged(object sender, EventArgs e)
        {
            string cia = funciones.getValorComboBox(cmbCia, 2);
            //if (cia != "X")
            {
                string query = " select distinct a.clavemaesgen as clave, a.desmaesgen as descripcion from maesgen a (nolock) ";
                query += "left join gerenciasusr b (nolock) on b.idgerencia=a.clavemaesgen where a.idmaesgen='040' and a.parm1maesgen = '" + cia + "' ";
                funciones.listaComboBox(query, cmbGerencia, "X");
                cmbGerencia.Text = "X   TODOS";
            }
        }

        private void cmbGerencia_SelectedIndexChanged(object sender, EventArgs e)
        {
            string cia = funciones.getValorComboBox(cmbCia, 2);
            string geren = funciones.getValorComboBox(cmbGerencia, 8);
            //if (geren != "X")
            {
                string query = " select distinct a.clavemaesgen as clave, a.desmaesgen as descripcion from maesgen a (nolock) ";
                query += "left join cencosusr b (nolock) on b.idcencos=a.clavemaesgen where a.idmaesgen='008' and a.parm1maesgen = '" + geren + "' and a.parm2maesgen = '" + cia + "' ";
                funciones.listaComboBox(query, cmbCencos, "X");
                cmbCencos.Text = "X   TODOS";
            }
        }

        private void txtCodigoInterno_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                GlobalVariables gv = new GlobalVariables();
                Funciones fn = new Funciones();
                string idcia = fn.getValorComboBox(cmbCia, 2);
                string idgerencia = fn.getValorComboBox(cmbGerencia, 8);
                string idarea = fn.getValorComboBox(cmbCencos, 8);
                this._TextBoxActivo = txtCodigoInterno;
                string cadenaBusqueda = string.Empty;
                string condicionAdicional = string.Empty;

                if (idcia != "X") condicionAdicional += " and A.idcia='" + @idcia + "' ";
                if (idgerencia != "X   TODO") condicionAdicional += " and A.codaux='" + @idgerencia + "' ";
                if (idarea != "X   TODO") condicionAdicional += " and A.seccion='" + @idgerencia + "' ";

                FiltrosMaestros filtros = new FiltrosMaestros();
                filtros.filtrarPerPlan2("ui_rep_comedor", this, txtCodigoInterno, null, cadenaBusqueda, condicionAdicional);
            }
        }

        private void txtCodigoInterno_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                if (txtCodigoInterno.Text.Trim() != string.Empty)
                {
                    GlobalVariables gv = new GlobalVariables();
                    PerPlan perplan = new PerPlan();
                    string idcia = gv.getValorCia();
                    string idperplan = txtCodigoInterno.Text.Trim();
                    string codigoInterno = perplan.ui_getDatosPerPlan(idperplan, "0");

                    if (codigoInterno == string.Empty)
                    {
                        MessageBox.Show("Código no registrado del trabajador", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txtCodigoInterno.Clear();
                        txtDocIdent.Clear();
                        txtNroDocIden.Clear();
                        txtNombres.Clear();
                        e.Handled = true;
                        txtCodigoInterno.Focus();
                    }
                    else
                    {
                        txtCodigoInterno.Text = perplan.ui_getDatosPerPlan(idperplan, "0");
                        txtDocIdent.Text = perplan.ui_getDatosPerPlan(idperplan, "1");
                        txtNroDocIden.Text = perplan.ui_getDatosPerPlan(idperplan, "2");
                        txtNombres.Text = perplan.ui_getDatosPerPlan(idperplan, "3");
                        e.Handled = true;
                        btnGenerar.Focus();
                    }
                }
                else
                {
                    txtCodigoInterno.Clear();
                    txtDocIdent.Clear();
                    txtNroDocIden.Clear();
                    txtNombres.Clear();
                    e.Handled = true;
                    txtCodigoInterno.Focus();
                }
            }
        }

        private void chkFechas_CheckedChanged(object sender, EventArgs e)
        {
            dtpFecini.Enabled = false;
            dtpFecfin.Enabled = false;
            if (chkFechas.Checked)
            {
                dtpFecini.Enabled = true;
                dtpFecfin.Enabled = true;
            }
        }

        private void chkPerfiles_CheckedChanged(object sender, EventArgs e)
        {
            grTrabajador.Enabled = false;
            rInvi.Enabled = false;
            rTrab.Enabled = false;
            rCat.Enabled = false;
            if (chkPerfiles.Checked)
            {
                rInvi.Enabled = true;
                rTrab.Enabled = true;
                rCat.Enabled = true;
                if (rTrab.Checked)
                {
                    grTrabajador.Enabled = true;
                    txtCodigoInterno.Clear();
                    txtDocIdent.Clear();
                    txtNombres.Clear();
                    txtNroDocIden.Clear();
                    txtCodigoInterno.Focus();
                }
            }
        }

        private void rTrab_CheckedChanged(object sender, EventArgs e)
        {
            if (rTrab.Checked)
            {
                grTrabajador.Enabled = true;
                txtCodigoInterno.Clear();
                txtDocIdent.Clear();
                txtNombres.Clear();
                txtNroDocIden.Clear();
                txtCodigoInterno.Focus();
            }
        }

        private void rInvi_CheckedChanged(object sender, EventArgs e)
        {
            if (rInvi.Checked)
            {
                txtCodigoInterno.Clear();
                txtDocIdent.Clear();
                txtNombres.Clear();
                txtNroDocIden.Clear();
                grTrabajador.Enabled = false;
            }
        }

        private void rCat_CheckedChanged(object sender, EventArgs e)
        {
            if (rInvi.Checked)
            {
                txtCodigoInterno.Clear();
                txtDocIdent.Clear();
                txtNombres.Clear();
                txtNroDocIden.Clear();
                grTrabajador.Enabled = false;
            }
        }
    }
}