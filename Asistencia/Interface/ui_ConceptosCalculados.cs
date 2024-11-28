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
    public partial class ui_ConceptosCalculados : Form
    {
        private TextBox TextBoxActivo;
        private Form FormPadre;

        public Form _FormPadre
        {
            get { return FormPadre; }
            set { FormPadre = value; }
        }

        public TextBox _TextBoxActivo
        {
            get { return TextBoxActivo; }
            set { TextBoxActivo = value; }
        }

        public ui_ConceptosCalculados()
        {
            InitializeComponent();
        }

        private void ui_ConceptosCalculados_Load(object sender, EventArgs e)
        {
            Funciones funciones = new Funciones();
            GlobalVariables variables = new GlobalVariables();
            string squery;
            string idcia = variables.getValorCia();

            squery = "SELECT idtipoper as clave,destipoper as descripcion FROM tipoper order by 1 asc";
            funciones.listaComboBox(squery, cmbTipoTrabajador, "");
            squery = "SELECT idtipoplan as clave,destipoplan as descripcion FROM tipoplan where idtipoplan in (select idtipoplan from reglabcia where idcia='" + @idcia + "') order by 1 asc;";
            funciones.listaComboBox(squery, cmbTipoPlan, "");
            squery = "SELECT idtipocal as clave,destipocal as descripcion FROM tipocal order by ordentipocal asc;";
            funciones.listaComboBox(squery, cmbTipoCal, "");
        }

        public void ui_ListaConBol()
        {

            Funciones fn = new Funciones();
            GlobalVariables gv = new GlobalVariables();

            string idtipoper = fn.getValorComboBox(cmbTipoTrabajador, 1);
            string periodo = txtMesSem.Text.Trim() + fn.replicateCadena(" ", 6);
            string messem = periodo.Substring(0, 2);
            string anio = periodo.Substring(3, 4);
            string idtipocal = fn.getValorComboBox(cmbTipoCal, 1);
            string idtipoplan = fn.getValorComboBox(cmbTipoPlan, 3);
            string idcia = gv.getValorCia();
            string idperplan = txtCodigoInterno.Text.Trim();

            DataTable dtconbol = new DataTable();
            Funciones funciones = new Funciones();

            string query = " select A.idconplan,B.desboleta,A.valor,B.idcolplan,B.pdt from conbol A ";
            query = query + " inner join detconplan B on A.idcia=B.idcia and ";
            query = query + " A.idtipoplan=B.idtipoplan and A.idtipocal=B.idtipocal and A.idtipoper=B.idtipoper ";
            query = query + " and A.idconplan=B.idconplan";
            query = query + " where A.anio='" + @anio + "' and A.messem='" + @messem + "' ";
            query = query + " and A.idcia='" + @idcia + "' and A.idtipoper='" + @idtipoper + "' ";
            query = query + " and A.idtipoplan='" + @idtipoplan + "' and A.idtipocal='" + @idtipocal + "' ";
            query = query + " and A.idperplan='" + @idperplan + "' order by A.idconplan asc;";


            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();
            SqlDataAdapter da = new SqlDataAdapter();

            da.SelectCommand = new SqlCommand(query, conexion);
            da.Fill(dtconbol);

            funciones.formatearDataGridView(dgvdetalle);
            dgvdetalle.DataSource = dtconbol;

            dgvdetalle.Columns[0].HeaderText = "Código";
            dgvdetalle.Columns[1].HeaderText = "Concepto de Planilla";
            dgvdetalle.Columns[2].HeaderText = "Valor";

            dgvdetalle.Columns["idcolplan"].Visible = false;
            dgvdetalle.Columns["pdt"].Visible = false;

            dgvdetalle.Columns[2].DefaultCellStyle.Format = "###,###.##";
            dgvdetalle.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.BottomRight;

            dgvdetalle.Columns[0].Width = 60;
            dgvdetalle.Columns[1].Width = 250;
            dgvdetalle.Columns[2].Width = 80;

            conexion.Close();

        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dgvdetalle_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

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
                    string codigoInterno = perplan.ui_getDatosPerPlan(idcia, idperplan, "0");

                    if (codigoInterno == string.Empty)
                    {
                        MessageBox.Show("Código no registrado del trabajador", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txtCodigoInterno.Clear();
                        txtDocIdent.Clear();
                        txtNroDocIden.Clear();
                        txtNombres.Clear();
                        ui_ListaConBol();
                        e.Handled = true;
                        txtCodigoInterno.Focus();

                    }

                    else
                    {
                        txtCodigoInterno.Text = perplan.ui_getDatosPerPlan(idcia, idperplan, "0");
                        txtDocIdent.Text = perplan.ui_getDatosPerPlan(idcia, idperplan, "1");
                        txtNroDocIden.Text = perplan.ui_getDatosPerPlan(idcia, idperplan, "2");
                        txtNombres.Text = perplan.ui_getDatosPerPlan(idcia, idperplan, "3");
                        ui_ListaConBol();

                    }

                }
                else
                {
                    txtCodigoInterno.Clear();
                    txtDocIdent.Clear();
                    txtNroDocIden.Clear();
                    txtNombres.Clear();
                    ui_ListaConBol();
                    e.Handled = true;
                    txtCodigoInterno.Focus();

                }
            }
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
                string periodo = txtMesSem.Text.Trim() + fn.replicateCadena(" ", 6);
                string messem = periodo.Substring(0, 2);
                string anio = periodo.Substring(3, 4);
                string idtipocal = fn.getValorComboBox(cmbTipoCal, 1);
                this._TextBoxActivo = txtCodigoInterno;
                string cadenaBusqueda = string.Empty;

                if (anio.Trim() == string.Empty)
                {
                    MessageBox.Show("No ha especificado Periodo Laboral", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtMesSem.Focus();

                }
                else
                {

                    string condicionAdicional = " and A.idtipoplan='" + @idtipoplan + "' and CONCAT(A.idperplan,A.idcia) in (select CONCAT(idperplan,idcia) from plan_ where idtipoper='" + @idtipoper + "' and messem='" + @messem + "' and anio='" + @anio + "' and idtipocal='" + @idtipocal + "' and idcia='" + @idcia + "' and idtipoplan='" + @idtipoplan + "' ) ";
                    FiltrosMaestros filtros = new FiltrosMaestros();
                    filtros.filtrarPerPlan("ui_ConceptosCalculados", this, txtCodigoInterno, idcia, idtipoper, "", cadenaBusqueda, condicionAdicional);
                }
            }
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
                    txtCodigoInterno.Clear();
                    txtDocIdent.Clear();
                    txtNroDocIden.Clear();
                    txtNombres.Clear();
                    ui_ListaConBol();
                    e.Handled = true;
                    txtCodigoInterno.Focus();

                }
                else
                {
                    MessageBox.Show("El Periodo Laboral no existe", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtFechaIni.Clear();
                    txtFechaFin.Clear();
                    txtCodigoInterno.Clear();
                    txtDocIdent.Clear();
                    txtNroDocIden.Clear();
                    txtNombres.Clear();
                    ui_ListaConBol();
                    e.Handled = true;
                    txtMesSem.Focus();
                }
            }
        }
    }
}