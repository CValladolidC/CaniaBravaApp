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
    public partial class ui_historialvaca : Form
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

        public ui_historialvaca()
        {
            InitializeComponent();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtCodigoInterno_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {

                GlobalVariables gv = new GlobalVariables();
                Funciones fn = new Funciones();
                string idcia = gv.getValorCia();
                this._TextBoxActivo = txtCodigoInterno;
                string cadenaBusqueda = string.Empty;
                string condicionAdicional = string.Empty;
                FiltrosMaestros filtros = new FiltrosMaestros();
                filtros.filtrarPerPlan("ui_historialvaca", this, txtCodigoInterno, idcia, "", "", cadenaBusqueda, condicionAdicional);

            }
        }

        private void txtCodigoInterno_KeyPress(object sender, KeyPressEventArgs e)
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
                    txtFecIniPerLab.Clear();
                    txtFecFinPerLab.Clear();
                    e.Handled = true;
                    txtCodigoInterno.Focus();

                }

                else
                {
                    txtCodigoInterno.Text = perplan.ui_getDatosPerPlan(idcia, idperplan, "0");
                    txtDocIdent.Text = perplan.ui_getDatosPerPlan(idcia, idperplan, "1");
                    txtNroDocIden.Text = perplan.ui_getDatosPerPlan(idcia, idperplan, "2");
                    txtNombres.Text = perplan.ui_getDatosPerPlan(idcia, idperplan, "3");
                    txtFecIniPerLab.Text = perplan.ui_getDatosPerPlan(idcia, idperplan, "4");
                    txtFecFinPerLab.Text = perplan.ui_getDatosPerPlan(idcia, idperplan, "8");
                    e.Handled = true;
                    toolstripform.Items[0].Select();
                    toolstripform.Focus();

                }

            }
            else
            {
                txtCodigoInterno.Clear();
                txtDocIdent.Clear();
                txtNroDocIden.Clear();
                txtNombres.Clear();
                txtFecIniPerLab.Clear();
                txtFecFinPerLab.Clear();
                e.Handled = true;
                txtCodigoInterno.Focus();

            }

            ui_ListaRegVac();
            ui_ListaResumenRegVac();

        }

        private void ui_ListaRegVac()
        {
            Funciones funciones = new Funciones();
            GlobalVariables variables = new GlobalVariables();
            string idcia = variables.getValorCia();
            string idperplan = txtCodigoInterno.Text;
            string query = " select B.idperplan,D.Parm1maesgen as cortotipodoc,B.nrodoc,";
            query = query + " CONCAT(B.apepat,' ',B.apemat,' , ',B.nombres) as nombre,";
            query = query + " A.anio,A.finivac,A.ffinvac,A.diasvac,A.idregvac ";
            query = query + " from view_regvac A  ";
            query = query + " left join perplan B on A.idperplan=B.idperplan and A.idcia=B.idcia ";
            query = query + " left join maesgen D on D.idmaesgen='002' and B.tipdoc=D.clavemaesgen ";
            query = query + " where A.idcia='" + @idcia + "' and A.idperplan='" + @idperplan + "' ";
            query = query + " order by A.anio desc,A.finivac desc;";

            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();
            try
            {
                using (SqlDataAdapter myDataAdapter = new SqlDataAdapter(query, conexion))
                {
                    DataSet myDataSet = new DataSet();
                    myDataAdapter.Fill(myDataSet, "tblRegVac");
                    funciones.formatearDataGridView(dgvdetalle);

                    dgvdetalle.DataSource = myDataSet.Tables["tblRegVac"];
                    dgvdetalle.Columns[0].HeaderText = "Cód.Int.";
                    dgvdetalle.Columns[1].HeaderText = "Doc. Ident.";
                    dgvdetalle.Columns[2].HeaderText = "Nro.Doc.";
                    dgvdetalle.Columns[3].HeaderText = "Apellidos y Nombres";
                    dgvdetalle.Columns[4].HeaderText = "Año";
                    dgvdetalle.Columns[5].HeaderText = "F.Inicio Vac.";
                    dgvdetalle.Columns[6].HeaderText = "F.Fin Vac.";
                    dgvdetalle.Columns[7].HeaderText = "Días Vac.";
                    dgvdetalle.Columns[8].HeaderText = "Nro. Registro";

                    dgvdetalle.Columns["idperplan"].Frozen = true;
                    dgvdetalle.Columns["cortotipodoc"].Frozen = true;
                    dgvdetalle.Columns["nrodoc"].Frozen = true;
                    dgvdetalle.Columns["nombre"].Frozen = true;

                    dgvdetalle.Columns[0].Width = 50;
                    dgvdetalle.Columns[1].Width = 50;
                    dgvdetalle.Columns[2].Width = 75;
                    dgvdetalle.Columns[3].Width = 200;
                    dgvdetalle.Columns[4].Width = 75;
                    dgvdetalle.Columns[5].Width = 75;
                    dgvdetalle.Columns[6].Width = 75;
                    dgvdetalle.Columns[7].Width = 75;
                    dgvdetalle.Columns[8].Width = 75;


                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }


            conexion.Close();
        }

        private void ui_ListaResumenRegVac()
        {
            Funciones funciones = new Funciones();
            GlobalVariables variables = new GlobalVariables();
            string idcia = variables.getValorCia();
            string idperplan = txtCodigoInterno.Text;
            string query = " select A.anio,SUM(A.diasvac) as dias ";
            query = query + " from view_regvac A  ";
            query = query + " where A.idcia='" + @idcia + "' and A.idperplan='" + @idperplan + "' ";
            query = query + " group by A.anio ";
            query = query + " order by A.anio desc";

            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();
            try
            {
                using (SqlDataAdapter myDataAdapter = new SqlDataAdapter(query, conexion))
                {
                    DataSet myDataSet = new DataSet();
                    myDataAdapter.Fill(myDataSet, "tblResRegVac");
                    funciones.formatearDataGridView(dgvresumen);
                    dgvresumen.DataSource = myDataSet.Tables["tblResRegVac"];
                    dgvresumen.Columns[0].HeaderText = "Año";
                    dgvresumen.Columns[1].HeaderText = "Días Vac.";
                    dgvresumen.Columns[0].Width = 60;
                    dgvresumen.Columns[1].Width = 60;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            conexion.Close();
        }

        private void ui_historialvaca_Load(object sender, EventArgs e)
        {
            ui_ListaRegVac();
            ui_ListaResumenRegVac();
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            Exporta exporta = new Exporta();
            exporta.Excel_FromDataGridView(dgvdetalle);

        }
    }
}