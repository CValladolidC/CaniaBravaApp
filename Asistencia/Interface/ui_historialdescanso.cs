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
    public partial class ui_historialdescanso : Form
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

        public ui_historialdescanso()
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
                filtros.filtrarPerPlan("ui_historialdescanso", this, txtCodigoInterno, cadenaBusqueda);
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
                string codigoInterno = perplan.ui_getDatosPerPlan(idperplan, "0");

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
                    txtCodigoInterno.Text = perplan.ui_getDatosPerPlan(idperplan, "0");
                    txtDocIdent.Text = perplan.ui_getDatosPerPlan(idperplan, "1");
                    txtNroDocIden.Text = perplan.ui_getDatosPerPlan(idperplan, "2");
                    txtNombres.Text = perplan.ui_getDatosPerPlan(idperplan, "3");
                    txtFecIniPerLab.Text = perplan.ui_getDatosPerPlan(idperplan, "4");
                    txtFecFinPerLab.Text = perplan.ui_getDatosPerPlan(idperplan, "8");
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
            string query = " select B.idperplan AS [Cod.Interno],D.Parm1maesgen AS [T.Doc.],B.nrodoc AS [Nro.Documento],";
            query += " RTRIM(B.apepat)+' '+RTRIM(B.apemat)+', '+RTRIM(B.nombres) AS [Apellidos y Nombres],";
            query += " A.anio AS [Año],A.finivac AS [F.Ini Descanso],A.ffinvac AS [F.Fin Descanso],A.diasvac AS [D.Descanso],A.idregvac AS [Nro Registro] ";
            query += " ,a.certificado [C. Medico],a.medico [Doctor(a)],a.Especialidad,a.Colegiatura,a.estacionsalud [Est. de Salud] ";
            query += " from regdescanso A  ";
            query += " left join perplan B on A.idperplan=B.idperplan and A.idcia=B.idcia ";
            query += " left join maesgen D on D.idmaesgen='002' and B.tipdoc=D.clavemaesgen ";
            query += " where A.idperplan='" + @idperplan + "' ";
            query += " order by A.anio desc,A.finivac desc;";

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

                    dgvdetalle.Columns[0].Frozen = true;
                    dgvdetalle.Columns[1].Frozen = true;
                    dgvdetalle.Columns[2].Frozen = true;
                    dgvdetalle.Columns[3].Frozen = true;

                    dgvdetalle.Columns[0].Width = 70;
                    dgvdetalle.Columns[1].Width = 50;
                    dgvdetalle.Columns[2].Width = 95;
                    dgvdetalle.Columns[3].Width = 270;
                    dgvdetalle.Columns[4].Width = 50;
                    dgvdetalle.Columns[5].Width = 110;
                    dgvdetalle.Columns[6].Width = 110;
                    dgvdetalle.Columns[7].Width = 75;
                    dgvdetalle.Columns[8].Width = 80;
                    dgvdetalle.Columns[9].Width = 90;
                    dgvdetalle.Columns[10].Width = 200;
                    dgvdetalle.Columns[11].Width = 200;
                    dgvdetalle.Columns[12].Width = 75;
                    dgvdetalle.Columns[13].Width = 200;

                    dgvdetalle.AllowUserToResizeRows = false;
                    dgvdetalle.AllowUserToResizeColumns = false;
                    foreach (DataGridViewColumn column in dgvdetalle.Columns)
                    {
                        column.SortMode = DataGridViewColumnSortMode.NotSortable;
                    }
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
            query = query + " from regdescanso A  ";
            query = query + " where A.idperplan='" + @idperplan + "' ";
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
                    dgvresumen.Columns[1].HeaderText = "Días Descanso Medico";
                    dgvresumen.Columns[0].Width = 60;
                    dgvresumen.Columns[1].Width = 130;

                    dgvresumen.AllowUserToResizeRows = false;
                    dgvresumen.AllowUserToResizeColumns = false;
                    foreach (DataGridViewColumn column in dgvresumen.Columns)
                    {
                        column.SortMode = DataGridViewColumnSortMode.NotSortable;
                    }
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
            txtCodigoInterno.Focus();
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            Exporta exporta = new Exporta();
            exporta.Excel_FromDataGridView(dgvdetalle);
        }

        private void pictureBoxBuscar_Click(object sender, EventArgs e)
        {

        }

        private void dgvdetalle_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}