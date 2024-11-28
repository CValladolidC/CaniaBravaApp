using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Configuration;

namespace CaniaBrava
{
    public partial class ui_destajoPeriodo : Form
    {
        string _idtipoper;
        string _idtipocal;

        private MaskedTextBox TextBoxActivo;

        public MaskedTextBox _TextBoxActivo
        {
            get { return TextBoxActivo; }
            set { TextBoxActivo = value; }
        }

        public ui_destajoPeriodo()
        {
            InitializeComponent();
        }

        private void txtMesSem_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                Funciones funciones = new Funciones();
                CalPlan calplan = new CalPlan();
                GlobalVariables variables = new GlobalVariables();
                string idcia = variables.getValorCia();
                string idtipoper = this._idtipoper;
                string idtipocal = this._idtipocal;
                string periodo = txtMesSem.Text.Trim() + funciones.replicateCadena(" ", 6);
                string messem = periodo.Substring(0, 2);
                string anio = periodo.Substring(3, 4);

                if (calplan.getDatosCalPlan(messem, anio, idtipoper, idcia, idtipocal, "FECHAINI") != "")
                {
                    txtFechaIni.Text = calplan.getDatosCalPlan(messem, anio, idtipoper, idcia, idtipocal, "FECHAINI");
                    txtFechaFin.Text = calplan.getDatosCalPlan(messem, anio, idtipoper, idcia, idtipocal, "FECHAFIN");

                    lblPeriodo.Visible = true;

                    if (calplan.getDatosCalPlan(messem, anio, idtipoper, idcia, idtipocal, "ESTADO") == "V")
                    {
                        lblPeriodo.Text = "Periodo Laboral Vigente";

                    }
                    else
                    {
                        lblPeriodo.Text = "Periodo Laboral Cerrado";

                    }
                }
                else
                {
                    MessageBox.Show("El Periodo Laboral no existe", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    lblPeriodo.Visible = false;
                    txtFechaIni.Clear();
                    txtFechaFin.Clear();
                    e.Handled = true;
                    txtMesSem.Focus();
                }

                ui_ListaDestajo();

            }
        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void ui_destajoPeriodo_Load(object sender, EventArgs e)
        {

            Funciones funciones = new Funciones();
            GlobalVariables variables = new GlobalVariables();
            string idcia = variables.getValorCia();
            string squery;
            squery = "SELECT ruccia as clave,descia as descripcion FROM ciafile WHERE idcia='" + @idcia + "';";
            funciones.listaComboBox(squery, cmbEmpleador, "");
            squery = "SELECT idproddes as clave,desproddes as descripcion FROM proddes where idcia='" + @idcia + "' and stateproddes='V' order by 1 asc";
            funciones.listaComboBox(squery, cmbProducto, "");
            squery = "SELECT idzontra as clave,deszontra as descripcion FROM zontra WHERE idcia='" + @idcia + "' order by 1 asc;";
            funciones.listaComboBox(squery, cmbZona, "");
            squery = "SELECT idtipoplan as clave,destipoplan as descripcion FROM tipoplan where idtipoplan in (select idtipoplan from reglabcia where idcia='" + @idcia + "') order by 1 asc;";
            funciones.listaComboBox(squery, cmbTipoPlan, "");
            radioButtonNoEmp.Checked = true;
            cmbModoDes.Text = "M    MANUAL";
            this._idtipoper = "O";
            this._idtipocal = "D";
            ui_ListaDestajo();

        }

        private void ui_ListaDestajo()
        {
            Funciones funciones = new Funciones();
            GlobalVariables variables = new GlobalVariables();

            string idcia = variables.getValorCia();
            string idproddes = funciones.getValorComboBox(cmbProducto, 2);
            string idzontra = funciones.getValorComboBox(cmbZona, 2);
            string periodo = txtMesSem.Text.Trim() + funciones.replicateCadena(" ", 6);
            string messem = periodo.Substring(0, 2);
            string anio = periodo.Substring(3, 4);
            string idtipoper = this._idtipoper;
            string idtipocal = this._idtipocal;
            string emplea = funciones.getValorComboBox(cmbEmpleador, 11);
            string estane = funciones.getValorComboBox(cmbEstablecimiento, 4);
            string idtipoplan = funciones.getValorComboBox(cmbTipoPlan, 3);

            string query = "  select B.idperplan,D.Parm1maesgen as cortotipodoc,B.nrodoc,";
            query = query + " CONCAT(B.apepat,' ',B.apemat,' , ',B.nombres) as nombre,E.deszontra,";
            query = query + " A.cantidad,A.precio,A.total,A.iddestajo,A.glosa,P.diasefelab,";
            query = query + " R.desboleta,P.valor,S.desboleta as desboletades,P.valordes from desplan A  ";
            query = query + " left join perplan B on A.idperplan=B.idperplan and A.idcia=B.idcia ";
            query = query + " left join maesgen D on D.idmaesgen='002' and B.tipdoc=D.clavemaesgen ";
            query = query + " left join zontra E on A.idzontra=E.idzontra and A.idcia=E.idcia ";
            query = query + " left join predesplan P on A.idcia=P.idcia and A.idperplan=P.idperplan ";
            query = query + " and A.anio=P.anio and A.messem=P.messem and A.idtipoper=P.idtipoper ";
            query = query + " and A.idtipocal=P.idtipocal ";
            query = query + " left join detconplan R on P.idcia=R.idcia and P.idconplan=R.idconplan ";
            query = query + " and P.idtipoper=R.idtipoper and P.idtipoplan=R.idtipoplan ";
            query = query + " left join detconplan S on P.idcia=S.idcia and P.idconplandes=S.idconplan ";
            query = query + " and P.idtipoper=S.idtipoper and P.idtipoplan=S.idtipoplan ";
            query = query + " where A.idcia='" + @idcia + "' and A.idtipocal='" + @idtipocal + "' ";
            query = query + " and A.idtipoper='" + @idtipoper + "' ";
            query = query + " and A.messem='" + @messem + "' and A.anio='" + @anio + "' ";
            query = query + " and idproddes='" + @idproddes + "' and A.idtipoplan='" + @idtipoplan + "' ";
            query = query + " and A.emplea='" + @emplea + "' and A.estane='" + @estane + "' ";
            query = query + " and A.idzontra= '" + @idzontra + "' ";
            query = query + " order by A.iddestajo desc;";

            string query_resumen = "select A.idzontra,B.deszontra,C.desproddes,SUM(A.cantidad) as cantidad,";
            query_resumen = query_resumen + " SUM(A.total) as total from desplan A left join zontra B ";
            query_resumen = query_resumen + " on A.idzontra=B.idzontra and A.idcia=B.idcia ";
            query_resumen = query_resumen + " left join proddes C on A.idproddes=C.idproddes and A.idcia=C.idcia ";
            query_resumen = query_resumen + " where A.idcia='" + @idcia + "' and idtipocal='" + @idtipocal + "' ";
            query_resumen = query_resumen + " and A.idtipoper='" + @idtipoper + "' ";
            query_resumen = query_resumen + " and A.messem='" + @messem + "' and A.anio='" + @anio + "' ";
            query_resumen = query_resumen + " and A.idtipoplan='" + @idtipoplan + "' ";
            query_resumen = query_resumen + " and A.emplea='" + @emplea + "' and A.estane='" + @estane + "'";
            query_resumen = query_resumen + " group by A.idzontra,C.desproddes,B.deszontra ";
            query_resumen = query_resumen + " order by A.idzontra asc,C.desproddes asc; ";

            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();

            try
            {
                using (SqlDataAdapter myDataAdapter = new SqlDataAdapter(query, conexion))
                {
                    DataSet myDataSet = new DataSet();
                    myDataAdapter.Fill(myDataSet, "tblDestajo");
                    funciones.formatearDataGridView(dgvdetalle);

                    dgvdetalle.DataSource = myDataSet.Tables["tblDestajo"];
                    dgvdetalle.Columns[0].HeaderText = "Cód.Int.";
                    dgvdetalle.Columns[1].HeaderText = "Doc.Ident.";
                    dgvdetalle.Columns[2].HeaderText = "Nro.Doc.";
                    dgvdetalle.Columns[3].HeaderText = "Apellidos y Nombres";
                    dgvdetalle.Columns[4].HeaderText = "Zona Trabajo";
                    dgvdetalle.Columns[5].HeaderText = "Cantidad";
                    dgvdetalle.Columns[6].HeaderText = "Precio por Unidad";
                    dgvdetalle.Columns[7].HeaderText = "Importe Total MN";
                    dgvdetalle.Columns[10].HeaderText = "Dias Efe.Lab.";
                    dgvdetalle.Columns[11].HeaderText = "Adicionales";
                    dgvdetalle.Columns[12].HeaderText = "Importe";
                    dgvdetalle.Columns[13].HeaderText = "Descuentos";
                    dgvdetalle.Columns[14].HeaderText = "Imp.Desc.";



                    dgvdetalle.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    dgvdetalle.Columns[6].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    dgvdetalle.Columns[7].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

                    dgvdetalle.Columns["iddestajo"].Visible = false;
                    dgvdetalle.Columns["glosa"].Visible = false;

                    dgvdetalle.Columns[5].DefaultCellStyle.Format = "###,###.##";
                    dgvdetalle.Columns[6].DefaultCellStyle.Format = "###,###.##";
                    dgvdetalle.Columns[7].DefaultCellStyle.Format = "###,###.##";
                    dgvdetalle.Columns[12].DefaultCellStyle.Format = "###,###.##";
                    dgvdetalle.Columns[14].DefaultCellStyle.Format = "###,###.##";



                    dgvdetalle.Columns[0].Width = 50;
                    dgvdetalle.Columns[1].Width = 60;
                    dgvdetalle.Columns[2].Width = 65;
                    dgvdetalle.Columns[3].Width = 180;
                    dgvdetalle.Columns[4].Width = 90;
                    dgvdetalle.Columns[5].Width = 65;
                    dgvdetalle.Columns[6].Width = 65;
                    dgvdetalle.Columns[7].Width = 65;
                    dgvdetalle.Columns[10].Width = 50;
                    dgvdetalle.Columns[11].Width = 80;
                    dgvdetalle.Columns[12].Width = 60;
                    dgvdetalle.Columns[13].Width = 80;
                    dgvdetalle.Columns[14].Width = 60;

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

                using (SqlDataAdapter myDataAdapter = new SqlDataAdapter(query_resumen, conexion))
                {
                    DataSet myDataSet = new DataSet();
                    myDataAdapter.Fill(myDataSet, "tblresumen");
                    funciones.formatearDataGridView(dgvresumen);

                    dgvresumen.DataSource = myDataSet.Tables["tblresumen"];
                    dgvresumen.Columns[0].HeaderText = "Código";
                    dgvresumen.Columns[1].HeaderText = "Zona Trabajo";
                    dgvresumen.Columns[2].HeaderText = "Producto";
                    dgvresumen.Columns[3].HeaderText = "Cantidad";
                    dgvresumen.Columns[4].HeaderText = "Importe Total MN";

                    dgvresumen.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    dgvresumen.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

                    dgvresumen.Columns[3].DefaultCellStyle.Format = "###,###.##";
                    dgvresumen.Columns[4].DefaultCellStyle.Format = "###,###.##";



                    dgvresumen.Columns[0].Width = 50;
                    dgvresumen.Columns[1].Width = 150;
                    dgvresumen.Columns[2].Width = 150;
                    dgvresumen.Columns[3].Width = 70;
                    dgvresumen.Columns[4].Width = 70;


                }

                float cantidad = float.Parse(funciones.sumaColumnaDataGridView(dgvdetalle, 5));
                float importe = float.Parse(funciones.sumaColumnaDataGridView(dgvdetalle, 7));
                txtCantidad.Text = Convert.ToString(cantidad);
                txtImporte.Text = Convert.ToString(importe);
                if (cantidad > 0)
                {

                    txtPrecio.Text = Convert.ToString(importe / cantidad);
                }
                else
                {
                    txtPrecio.Text = "0";
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }


            conexion.Close();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            GlobalVariables variables = new GlobalVariables();
            Funciones funciones = new Funciones();
            string idcia = variables.getValorCia();
            string idproddes = funciones.getValorComboBox(cmbProducto, 2);
            string idzontra = funciones.getValorComboBox(cmbZona, 2);
            string periodo = txtMesSem.Text.Trim() + funciones.replicateCadena(" ", 6);
            string messem = periodo.Substring(0, 2);
            string anio = periodo.Substring(3, 4);
            string idtipoper = this._idtipoper;
            string idtipocal = this._idtipocal;
            string idtipoplan = funciones.getValorComboBox(cmbTipoPlan, 3);
            string emplea = funciones.getValorComboBox(cmbEmpleador, 11);
            string estane = funciones.getValorComboBox(cmbEstablecimiento, 4);
            string fecha = txtFechaIni.Text;
            string modo = funciones.getValorComboBox(cmbModoDes, 1);
            string iddestajo = "";
            CalPlan calplan = new CalPlan();
            string fechaini = calplan.getDatosCalPlan(messem, anio, idtipoper, idcia, idtipocal, "FECHAINI");
            string fechafin = calplan.getDatosCalPlan(messem, anio, idtipoper, idcia, idtipocal, "FECHAFIN");


            if (fechaini != "")
            {

                if (calplan.getDatosCalPlan(messem, anio, idtipoper, idcia, idtipocal, "ESTADO") != "C")
                {
                    ui_upddestajoplan ui_detalle = new ui_upddestajoplan();
                    ui_detalle._FormPadre = this;
                    ui_detalle.setValores(idcia, idproddes, messem, anio, idtipoper, idtipocal, idzontra, idtipoplan, emplea, estane, iddestajo, fecha, modo, fechaini, fechafin);
                    ui_detalle.ui_newDestajo();
                    ui_detalle.Activate();
                    ui_detalle.BringToFront();
                    ui_detalle.ShowDialog();
                    ui_detalle.Dispose();
                }
                else
                {
                    MessageBox.Show("El Periodo Laboral ya se encuentra cerrado", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
            else
            {
                MessageBox.Show("El Periodo Laboral no existe", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
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
            ui_ListaDestajo();
            cmbEmpleador.Focus();
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
            ui_ListaDestajo();
            cmbEstablecimiento.Focus();
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            Exporta exporta = new Exporta();
            exporta.Excel_FromDataGridView(dgvdetalle);
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            ui_ListaDestajo();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            GlobalVariables variables = new GlobalVariables();
            Funciones funciones = new Funciones();
            CalPlan calplan = new CalPlan();

            string idcia = variables.getValorCia();
            string idproddes = funciones.getValorComboBox(cmbProducto, 2);
            string idzontra = funciones.getValorComboBox(cmbZona, 2);
            string periodo = txtMesSem.Text.Trim() + funciones.replicateCadena(" ", 6);
            string idtipoplan = funciones.getValorComboBox(cmbTipoPlan, 3);
            string messem = periodo.Substring(0, 2);
            string anio = periodo.Substring(3, 4);
            string idtipoper = this._idtipoper;
            string idtipocal = this._idtipocal;
            if (calplan.getDatosCalPlan(messem, anio, idtipoper, idcia, idtipocal, "ESTADO") != "C")
            {
                Int32 selectedCellCount =
                dgvdetalle.GetCellCount(DataGridViewElementStates.Selected);
                if (selectedCellCount > 0)
                {
                    string idperplan = dgvdetalle.Rows[dgvdetalle.SelectedCells[1].RowIndex].Cells[0].Value.ToString();
                    string desperplan = dgvdetalle.Rows[dgvdetalle.SelectedCells[1].RowIndex].Cells[3].Value.ToString();
                    string iddestajo = dgvdetalle.Rows[dgvdetalle.SelectedCells[1].RowIndex].Cells[8].Value.ToString();


                    DialogResult resultado = MessageBox.Show("¿Desea eliminar la información de destajo del trabajador " + desperplan + "?",
                    "Consulta Importante",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);
                    if (resultado == DialogResult.Yes)
                    {
                        PreDesPlan predesplan = new PreDesPlan();
                        predesplan.eliminarPreDesPlan(idperplan, idcia, anio, messem, idtipoper, idtipocal, idtipoplan);

                        Destajo destajo = new Destajo();
                        destajo.eliminarDestajoPlan(idcia, idperplan, messem, anio, idtipocal, idtipoper, idproddes, idzontra, iddestajo);
                        this.ui_ListaDestajo();
                    }

                }
                else
                {
                    MessageBox.Show("No ha seleccionado registro a Eliminar", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
            }
            else
            {
                MessageBox.Show("El Periodo Laboral ya se encuentra cerrado", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            GlobalVariables variables = new GlobalVariables();
            Funciones funciones = new Funciones();
            CalPlan calplan = new CalPlan();

            string idcia = variables.getValorCia();
            string idproddes = funciones.getValorComboBox(cmbProducto, 2);
            string idzontra = funciones.getValorComboBox(cmbZona, 2);
            string periodo = txtMesSem.Text.Trim() + funciones.replicateCadena(" ", 6);
            string messem = periodo.Substring(0, 2);
            string anio = periodo.Substring(3, 4);
            string fecha = txtFechaIni.Text;
            string idtipoper = this._idtipoper;
            string idtipocal = this._idtipocal;
            string idtipoplan = funciones.getValorComboBox(cmbTipoPlan, 3);
            string emplea = funciones.getValorComboBox(cmbEmpleador, 11);
            string estane = funciones.getValorComboBox(cmbEstablecimiento, 4);
            string modo = funciones.getValorComboBox(cmbModoDes, 1);
            string fechaini = calplan.getDatosCalPlan(messem, anio, idtipoper, idcia, idtipocal, "FECHAINI");
            string fechafin = calplan.getDatosCalPlan(messem, anio, idtipoper, idcia, idtipocal, "FECHAFIN");

            if (calplan.getDatosCalPlan(messem, anio, idtipoper, idcia, idtipocal, "ESTADO") != "C")
            {
                Int32 selectedCellCount =
                   dgvdetalle.GetCellCount(DataGridViewElementStates.Selected);
                if (selectedCellCount > 0)
                {


                    string idperplan = dgvdetalle.Rows[dgvdetalle.SelectedCells[1].RowIndex].Cells[0].Value.ToString();
                    string cantidad = dgvdetalle.Rows[dgvdetalle.SelectedCells[1].RowIndex].Cells[5].Value.ToString();
                    string precio = dgvdetalle.Rows[dgvdetalle.SelectedCells[1].RowIndex].Cells[6].Value.ToString();
                    string total = dgvdetalle.Rows[dgvdetalle.SelectedCells[1].RowIndex].Cells[7].Value.ToString();
                    string iddestajo = dgvdetalle.Rows[dgvdetalle.SelectedCells[1].RowIndex].Cells[8].Value.ToString();
                    string glosa = dgvdetalle.Rows[dgvdetalle.SelectedCells[1].RowIndex].Cells[9].Value.ToString();

                    ui_upddestajoplan ui_detalle = new ui_upddestajoplan();
                    ui_detalle._FormPadre = this;
                    ui_detalle.setValores(idcia, idproddes, messem, anio, idtipoper, idtipocal, idzontra, idtipoplan, emplea, estane, iddestajo, fecha, modo, fechaini, fechafin);
                    ui_detalle.ui_loadDatos(idperplan, cantidad, precio, total, glosa);
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
            else
            {
                MessageBox.Show("El Periodo Laboral ya se encuentra cerrado", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void dgvresumen_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void cmbProducto_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.ui_ListaDestajo();
        }

        private void cmbZona_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.ui_ListaDestajo();
        }

        private void cmbTipoPlan_SelectedIndexChanged(object sender, EventArgs e)
        {
            ui_ListaDestajo();
        }

        private void txtMesSem_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                GlobalVariables variables = new GlobalVariables();
                string idtipoper = this._idtipoper;
                string idtipocal = this._idtipocal;
                string idcia = variables.getValorCia();
                string clasepadre = "ui_destajoPeriodo";
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
    }
}