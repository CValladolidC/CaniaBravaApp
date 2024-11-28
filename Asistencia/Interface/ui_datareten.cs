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
    public partial class ui_datareten : Form
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

        public ui_datareten()
        {
            InitializeComponent();
        }

        private void ui_ListaRetenciones()
        {
            Funciones funciones = new Funciones();
            string idcia = this._idcia;
            string idproddes = funciones.getValorComboBox(cmbProducto, 2);
            string idzontra = funciones.getValorComboBox(cmbZona, 2);
            string emplea = funciones.getValorComboBox(cmbEmpleador, 11);
            string estane = funciones.getValorComboBox(cmbEstablecimiento, 4);
            string periodo = txtMesSem.Text.Trim() + funciones.replicateCadena(" ", 6);
            string messem = periodo.Substring(0, 2);
            string anio = periodo.Substring(3, 4);
            string idtipocal = this._idtipocal;
            string idtipoper = this._idtipoper;
            string idtipoplan = "";
            string query = " select B.idperplan,D.Parm1maesgen as cortotipodoc,B.nrodoc,";
            query = query + " CONCAT(B.apepat,' ',B.apemat,' , ',B.nombres) as nombre,";
            query = query + " SUM(A.cantidad) as cantidad,SUM(adicional) as adicional,";
            query = query + " SUM(reten) as reten,SUM(A.total) as total from desret A ";
            query = query + " left join perret B on A.idperplan=B.idperplan and A.idcia=B.idcia ";
            query = query + " left join maesgen D on D.idmaesgen='002' and B.tipdoc=D.clavemaesgen ";
            query = query + " left join zontra E on A.idzontra=E.idzontra and A.idcia=E.idcia ";
            query = query + " where A.idcia='" + @idcia + "' and idtipocal='" + @idtipocal + "' ";
            query = query + " and A.idtipoper='" + @idtipoper + "' ";
            query = query + " and A.messem='" + @messem + "' and A.anio='" + @anio + "' ";
            query = query + " and idproddes='" + @idproddes + "' and A.idtipoplan='" + @idtipoplan + "' ";
            query = query + " and A.emplea='" + @emplea + "' and A.estane='" + @estane + "' and A.idzontra='" + @idzontra + "'";
            query = query + " group by B.idperplan,D.Parm1maesgen,B.nrodoc,B.apepat,B.apemat,B.nombres ";
            query = query + " order by B.apepat asc,B.apemat asc,B.nombres asc;";
            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();
            try
            {
                using (SqlDataAdapter myDataAdapter = new SqlDataAdapter(query, conexion))
                {
                    DataSet myDataSet = new DataSet();
                    myDataAdapter.Fill(myDataSet, "tblReten");
                    funciones.formatearDataGridView(dgvdetalle);

                    dgvdetalle.DataSource = myDataSet.Tables["tblReten"];
                    dgvdetalle.Columns[0].HeaderText = "Cód.Int.";
                    dgvdetalle.Columns[1].HeaderText = "Doc.Ident.";
                    dgvdetalle.Columns[2].HeaderText = "Nro.Doc.";
                    dgvdetalle.Columns[3].HeaderText = "Apellidos y Nombres";
                    dgvdetalle.Columns[4].HeaderText = "Cantidad";
                    dgvdetalle.Columns[5].HeaderText = "Adicional";
                    dgvdetalle.Columns[6].HeaderText = "Retención";
                    dgvdetalle.Columns[7].HeaderText = "Importe Total MN";
                    dgvdetalle.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    dgvdetalle.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    dgvdetalle.Columns[6].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    dgvdetalle.Columns[7].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

                    dgvdetalle.Columns[4].DefaultCellStyle.Format = "###,###.##";
                    dgvdetalle.Columns[5].DefaultCellStyle.Format = "###,###.##";
                    dgvdetalle.Columns[6].DefaultCellStyle.Format = "###,###.##";
                    dgvdetalle.Columns[7].DefaultCellStyle.Format = "###,###.##";

                    dgvdetalle.Columns[0].Width = 50;
                    dgvdetalle.Columns[1].Width = 60;
                    dgvdetalle.Columns[2].Width = 65;
                    dgvdetalle.Columns[3].Width = 220;
                    dgvdetalle.Columns[4].Width = 70;
                    dgvdetalle.Columns[5].Width = 70;
                    dgvdetalle.Columns[6].Width = 70;
                    dgvdetalle.Columns[7].Width = 70;

                    dgvdetalle.RowHeadersVisible = true;
                    if (dgvdetalle.Rows.Count > 0)
                    {
                        for (int r = 0; r < dgvdetalle.Rows.Count; r++)
                        {
                            this.dgvdetalle.Rows[r].HeaderCell.Value = (r + 1).ToString();
                        }
                    }
                    dgvdetalle.AutoResizeRowHeadersWidth(DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders);
                }

                float importe = float.Parse(funciones.sumaColumnaDataGridView(dgvdetalle, 7));
                txtImporte.Text = Convert.ToString(importe);

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
                }
                else
                {
                    MessageBox.Show("El Periodo Laboral no existe", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtFechaIni.Clear();
                    txtFechaFin.Clear();
                    e.Handled = true;
                    txtMesSem.Focus();
                }
                ui_ListaRetenciones();
            }
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            Funciones funciones = new Funciones();
            string idcia = this._idcia;
            string emplea = funciones.getValorComboBox(cmbEmpleador, 11);
            string estane = funciones.getValorComboBox(cmbEstablecimiento, 4);
            string idproddes = funciones.getValorComboBox(cmbProducto, 2);
            string idzontra = funciones.getValorComboBox(cmbZona, 2);
            string periodo = txtMesSem.Text.Trim() + funciones.replicateCadena(" ", 6);
            string messem = periodo.Substring(0, 2);
            string anio = periodo.Substring(3, 4);
            string idtipocal = this._idtipocal;
            string idtipoper = this._idtipoper;
            string titulo = cmbProducto.Text.Trim() + " / " + cmbZona.Text.Trim();
            string idtipoplan = "";
            CalPlan calplan = new CalPlan();
            if (calplan.getDatosCalPlan(messem, anio, idtipoper, idcia, idtipocal, "FECHAINI") != "")
            {
                string fini = calplan.getDatosCalPlan(messem, anio, idtipoper, idcia, idtipocal, "FECHAINI");
                string ffin = calplan.getDatosCalPlan(messem, anio, idtipoper, idcia, idtipocal, "FECHAFIN");
                ui_upddatareten ui_detalle = new ui_upddatareten();
                ui_detalle._FormPadre = this;
                ui_detalle.setValores(messem, anio, idcia, idtipoper, idtipocal, idtipoplan, idproddes,
                idzontra, emplea, estane, fini, ffin, titulo);
                ui_detalle.ui_iniPerPlan("AGREGAR", "");
                ui_detalle.Activate();
                ui_detalle.BringToFront();
                ui_detalle.ShowDialog();
                ui_detalle.Dispose();
            }
            else
            {
                MessageBox.Show("El Periodo Laboral no existe", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cmbProducto_SelectedIndexChanged(object sender, EventArgs e)
        {
            ui_ListaRetenciones();
        }

        private void cmbZona_SelectedIndexChanged(object sender, EventArgs e)
        {
            ui_ListaRetenciones();
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            Funciones funciones = new Funciones();
            CalPlan calplan = new CalPlan();
            string idcia = this._idcia;
            string emplea = funciones.getValorComboBox(cmbEmpleador, 11);
            string estane = funciones.getValorComboBox(cmbEstablecimiento, 4);
            string idproddes = funciones.getValorComboBox(cmbProducto, 2);
            string idzontra = funciones.getValorComboBox(cmbZona, 2);
            string titulo = cmbProducto.Text.Trim() + " / " + cmbZona.Text.Trim();
            string periodo = txtMesSem.Text.Trim() + funciones.replicateCadena(" ", 6);
            string messem = periodo.Substring(0, 2);
            string anio = periodo.Substring(3, 4);
            string idtipocal = this._idtipocal;
            string idtipoper = this._idtipoper;
            string idtipoplan = "";
            string fini = calplan.getDatosCalPlan(messem, anio, idtipoper, idcia, idtipocal, "FECHAINI");
            string ffin = calplan.getDatosCalPlan(messem, anio, idtipoper, idcia, idtipocal, "FECHAFIN");

            Int32 selectedCellCount =
               dgvdetalle.GetCellCount(DataGridViewElementStates.Selected);
            if (selectedCellCount > 0)
            {
                string idperplan = dgvdetalle.Rows[dgvdetalle.SelectedCells[1].RowIndex].Cells[0].Value.ToString();

                ui_upddatareten ui_detalle = new ui_upddatareten();
                ui_detalle._FormPadre = this;
                ui_detalle.setValores(messem, anio, idcia, idtipoper, idtipocal, idtipoplan, idproddes,
                idzontra, emplea, estane, fini, ffin, titulo);
                ui_detalle.ui_iniPerPlan("EDITAR", idperplan);
                ui_detalle.Activate();
                ui_detalle.BringToFront();
                ui_detalle.ShowDialog();
                ui_detalle.Dispose();
            }
            else
            {
                MessageBox.Show("No ha seleccionado registro a Editar", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            ui_ListaRetenciones();
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            Exporta exporta = new Exporta();
            exporta.Excel_FromDataGridView(dgvdetalle);
        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void cmbTipoPlan_SelectedIndexChanged(object sender, EventArgs e)
        {
            ui_ListaRetenciones();
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
            ui_ListaRetenciones();
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
            ui_ListaRetenciones();
            cmbEmpleador.Focus();
        }

        private void cmbEmpleador_SelectedIndexChanged(object sender, EventArgs e)
        {
            Funciones funciones = new Funciones();
            string idcia = this._idcia;
            string ruccia = funciones.getValorComboBox(cmbEmpleador, 11);
            string squery_esta = "SELECT idestane as clave,desestane as descripcion FROM estane WHERE codemp='" + @ruccia + "' and idciafile='" + @idcia + "' order by idestane asc;";
            funciones.listaComboBox(squery_esta, cmbEstablecimiento, "");
            ui_ListaRetenciones();
            cmbEstablecimiento.Focus();
        }

        private void cmbEstablecimiento_SelectedIndexChanged(object sender, EventArgs e)
        {
            ui_ListaRetenciones();
        }

        private void ui_datareten_Load(object sender, EventArgs e)
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
            ui_ListaRetenciones();

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
                string clasepadre = "ui_datareten";
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

        private void groupBox2_Enter_1(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void groupBox46_Enter(object sender, EventArgs e)
        {

        }

        private void txtMesSem_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }
    }
}