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
    public partial class ui_destajo : Form
    {
        string _idcia;
        string _idtipocal;
        string _idtipoper;

        private MaskedTextBox TextBoxActivo;

        public MaskedTextBox _TextBoxActivo
        {
            get { return TextBoxActivo; }
            set { TextBoxActivo = value; }
        }

        public ui_destajo()
        {
            InitializeComponent();
        }

        private void ui_destajo_Load(object sender, EventArgs e)
        {
            Funciones funciones = new Funciones();
            GlobalVariables variables = new GlobalVariables();
            this._idcia = variables.getValorCia();
            string idcia = this._idcia;
            string squery;
            squery = "SELECT ruccia as clave,descia as descripcion FROM ciafile WHERE idcia='" + @idcia + "';";
            funciones.listaComboBox(squery, cmbEmpleador, "");
            squery = "SELECT idproddes as clave,desproddes as descripcion FROM proddes where idcia='" + @idcia + "' and stateproddes='V' order by 1 asc";
            funciones.listaComboBox(squery, cmbProducto, "");
            squery = "SELECT idzontra as clave,deszontra as descripcion FROM zontra WHERE idcia='" + @idcia + "' order by 1 asc;";
            funciones.listaComboBox(squery, cmbZona, "");
            cmbTipoRegistro.Text = "R   PERSONAL DE RETENCIONES";
            lblPeriodo.Text = "";
            this._idtipocal = "D";
            this._idtipoper = "O";
            ui_ListaDestajo();
        }

        private void ui_ListaDestajo()
        {
            Funciones funciones = new Funciones();
            string idcia = this._idcia;
            string idproddes = funciones.getValorComboBox(cmbProducto, 2);
            string codvar = funciones.getValorComboBox(cmbVariedad, 2);
            string idzontra = funciones.getValorComboBox(cmbZona, 2);
            string periodo = txtMesSem.Text.Trim() + funciones.replicateCadena(" ", 6);
            string messem = periodo.Substring(0, 2);
            string anio = periodo.Substring(3, 4);
            string fecha = txtFechaProceso.Text;
            string idtipocal = this._idtipocal;
            string idtipoper = this._idtipoper;
            string tiporegistro = funciones.getValorComboBox(cmbTipoRegistro, 1);
            string emplea = funciones.getValorComboBox(cmbEmpleador, 11);
            string estane = funciones.getValorComboBox(cmbEstablecimiento, 4);
            string idtipoplan;
            if (tiporegistro.Equals("P"))
            {
                idtipoplan = funciones.getValorComboBox(cmbTipoPlan, 3);
            }
            else
            {
                idtipoplan = "";
            }

            string nombreTablaRet = string.Empty;
            string nombreTablaPer = string.Empty;

            if (tiporegistro.Equals("R"))
            {
                nombreTablaRet = "desret";
                nombreTablaPer = "perret";
            }
            else
            {
                nombreTablaRet = "desplan";
                nombreTablaPer = "perplan";
            }

            string cadenaZonTra = string.Empty;

            if (idzontra != "X")
            {
                cadenaZonTra = " and A.idzontra= '" + @idzontra + "' ";

            }

            string cadenaVariedad = string.Empty;

            if (codvar != "X")
            {
                cadenaVariedad = " and A.codvar= '" + @codvar + "' ";

            }

            string query = " select B.idperplan,D.Parm1maesgen as cortotipodoc,B.nrodoc,";
            query = query + " CONCAT(B.apepat,' ',B.apemat,' , ',B.nombres) as nombre,F.desvar,A.cantidad,A.precio,A.subtotal,";
            query = query + " A.movilidad,A.refrigerio,A.adicional,A.total,A.iddestajo,A.codvar ";
            query = query + " from " + @nombreTablaRet + " A  ";
            query = query + " left join " + @nombreTablaPer + " B on A.idperplan=B.idperplan and A.idcia=B.idcia ";
            query = query + " left join maesgen D on D.idmaesgen='002' and B.tipdoc=D.clavemaesgen ";
            query = query + " left join varproddes F on A.idproddes=F.idproddes and A.idcia=F.idcia and A.codvar=F.codvar ";
            query = query + " left join zontra E on A.idzontra=E.idzontra and A.idcia=E.idcia ";
            query = query + " where A.idcia='" + @idcia + "' and idtipocal='" + @idtipocal + "' and A.idtipoper='" + @idtipoper + "' ";
            query = query + " and A.messem='" + @messem + "' and A.anio='" + @anio + "' and A.idproddes='" + @idproddes + "' and A.idtipoplan='" + @idtipoplan + "' and A.emplea='" + @emplea + "' and A.estane='" + @estane + "'" + cadenaZonTra + cadenaVariedad;
            query = query + " and fecha=" + " STR_TO_DATE('" + @fecha + "', '%d/%m/%Y') ";
            query = query + " order by A.iddestajo desc;";

            string query_resumen = "select B.deszontra,SUM(A.cantidad) as cantidad,SUM(A.total) as total from " + @nombreTablaRet + " A left join zontra B on A.idzontra=B.idzontra and A.idcia=B.idcia ";
            query_resumen = query_resumen + " where A.idcia='" + @idcia + "' and idtipocal='" + @idtipocal + "' and A.idtipoper='" + @idtipoper + "' ";
            query_resumen = query_resumen + " and A.messem='" + @messem + "' and A.anio='" + @anio + "' and A.idproddes='" + @idproddes + "' and A.idtipoplan='" + @idtipoplan + "' and A.emplea='" + @emplea + "' and A.estane='" + @estane + "'";
            query_resumen = query_resumen + " and fecha=" + " STR_TO_DATE('" + @fecha + "', '%d/%m/%Y') ";
            query_resumen = query_resumen + " group by B.deszontra order by A.idzontra asc; ";

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
                    dgvdetalle.Columns[4].HeaderText = "Variedad";
                    dgvdetalle.Columns[5].HeaderText = "Cantidad";
                    dgvdetalle.Columns[6].HeaderText = "Precio por Unidad";
                    dgvdetalle.Columns[7].HeaderText = "SubTotal";
                    dgvdetalle.Columns[8].HeaderText = "Movilidad";
                    dgvdetalle.Columns[9].HeaderText = "Refrigerio";
                    dgvdetalle.Columns[10].HeaderText = "Adicional";
                    dgvdetalle.Columns[11].HeaderText = "Importe Total MN";


                    dgvdetalle.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    dgvdetalle.Columns[6].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    dgvdetalle.Columns[7].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    dgvdetalle.Columns[8].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    dgvdetalle.Columns[9].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    dgvdetalle.Columns[10].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    dgvdetalle.Columns[11].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;


                    dgvdetalle.Columns["iddestajo"].Visible = false;
                    dgvdetalle.Columns["codvar"].Visible = false;


                    dgvdetalle.Columns[5].DefaultCellStyle.Format = "###,###.##";
                    dgvdetalle.Columns[6].DefaultCellStyle.Format = "###,###.##";
                    dgvdetalle.Columns[7].DefaultCellStyle.Format = "###,###.##";
                    dgvdetalle.Columns[8].DefaultCellStyle.Format = "###,###.##";
                    dgvdetalle.Columns[9].DefaultCellStyle.Format = "###,###.##";
                    dgvdetalle.Columns[10].DefaultCellStyle.Format = "###,###.##";
                    dgvdetalle.Columns[11].DefaultCellStyle.Format = "###,###.##";



                    dgvdetalle.Columns[0].Width = 40;
                    dgvdetalle.Columns[1].Width = 50;
                    dgvdetalle.Columns[2].Width = 60;
                    dgvdetalle.Columns[3].Width = 160;
                    dgvdetalle.Columns[4].Width = 100;
                    dgvdetalle.Columns[5].Width = 60;
                    dgvdetalle.Columns[6].Width = 60;
                    dgvdetalle.Columns[7].Width = 60;
                    dgvdetalle.Columns[8].Width = 60;
                    dgvdetalle.Columns[9].Width = 60;
                    dgvdetalle.Columns[10].Width = 60;
                    dgvdetalle.Columns[11].Width = 60;


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
                    dgvresumen.Columns[0].HeaderText = "Zona Trabajo";
                    dgvresumen.Columns[1].HeaderText = "Cantidad";
                    dgvresumen.Columns[2].HeaderText = "Importe Total MN";

                    dgvresumen.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    dgvresumen.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

                    dgvresumen.Columns[1].DefaultCellStyle.Format = "###,###.##";
                    dgvresumen.Columns[2].DefaultCellStyle.Format = "###,###.##";

                    dgvresumen.Columns[0].Width = 50;
                    dgvresumen.Columns[1].Width = 70;
                    dgvresumen.Columns[2].Width = 70;



                }

                float cantidad = float.Parse(funciones.sumaColumnaDataGridView(dgvdetalle, 5));
                float subtotal = float.Parse(funciones.sumaColumnaDataGridView(dgvdetalle, 7));

                txtCantidad.Text = funciones.sumaColumnaDataGridView(dgvdetalle, 5);
                txtSubtotal.Text = funciones.sumaColumnaDataGridView(dgvdetalle, 7);
                txtMovilidad.Text = funciones.sumaColumnaDataGridView(dgvdetalle, 8);
                txtRefrigerio.Text = funciones.sumaColumnaDataGridView(dgvdetalle, 9);
                txtAdicional.Text = funciones.sumaColumnaDataGridView(dgvdetalle, 10);
                txtImporte.Text = funciones.sumaColumnaDataGridView(dgvdetalle, 11);

                if (cantidad > 0)
                {

                    txtPrecio.Text = Convert.ToString(subtotal / cantidad);
                }
                else
                {
                    txtPrecio.Text = "0";
                }

                float cantidadGen = float.Parse(funciones.sumaColumnaDataGridView(dgvresumen, 1));
                float importeGen = float.Parse(funciones.sumaColumnaDataGridView(dgvresumen, 2));
                txtCantidadGen.Text = Convert.ToString(cantidadGen);
                txtImporteGen.Text = Convert.ToString(importeGen);
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
                string idtipocal = this._idtipocal;
                string idtipoper = this._idtipoper;

                if (calplan.getDatosCalPlan(messem, anio, idtipoper, idcia, idtipocal, "FECHAINI") != "")
                {
                    lblPeriodo.Visible = true;
                    txtFechaIni.Text = calplan.getDatosCalPlan(messem, anio, idtipoper, idcia, idtipocal, "FECHAINI");
                    txtFechaProceso.Text = calplan.getDatosCalPlan(messem, anio, idtipoper, idcia, idtipocal, "FECHAINI");
                    txtFechaFin.Text = calplan.getDatosCalPlan(messem, anio, idtipoper, idcia, idtipocal, "FECHAFIN");
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
                    txtFechaProceso.Clear();
                    e.Handled = true;
                    txtMesSem.Focus();
                }
                ui_ListaDestajo();

            }
        }

        private void txtMesSem_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }

        private void txtFechaProceso_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                if (UtileriasFechas.IsDate(txtFechaProceso.Text))
                {

                    if (UtileriasFechas.compararFecha(txtFechaProceso.Text, ">=", txtFechaIni.Text) && UtileriasFechas.compararFecha(txtFechaProceso.Text, "<=", txtFechaFin.Text))
                    {
                        ui_ListaDestajo();

                    }
                    else
                    {

                        MessageBox.Show("La fecha de proceso no se encuentra dentro del rango del periodo", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        e.Handled = true;
                        txtFechaProceso.Clear();
                        txtFechaProceso.Focus();
                    }
                }
                else
                {
                    MessageBox.Show("Fecha de proceso no válida", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    e.Handled = true;
                    txtFechaProceso.Clear();
                    txtFechaProceso.Focus();

                }
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
            string idproddes = funciones.getValorComboBox(cmbProducto, 2);
            string idzontra = funciones.getValorComboBox(cmbZona, 2);
            string periodo = txtMesSem.Text.Trim() + funciones.replicateCadena(" ", 6);
            string messem = periodo.Substring(0, 2);
            string anio = periodo.Substring(3, 4);
            string fecha = txtFechaProceso.Text;
            string idtipocal = this._idtipocal;
            string idtipoper = this._idtipoper;
            string tiporegistro = funciones.getValorComboBox(cmbTipoRegistro, 1);
            string idtipoplan = funciones.getValorComboBox(cmbTipoPlan, 3);
            string emplea = funciones.getValorComboBox(cmbEmpleador, 11);
            string estane = funciones.getValorComboBox(cmbEstablecimiento, 4);
            string iddestajo = "";

            CalPlan calplan = new CalPlan();

            if (calplan.getDatosCalPlan(messem, anio, idtipoper, idcia, idtipocal, "FECHAINI") != "")
            {

                if (calplan.getDatosCalPlan(messem, anio, idtipoper, idcia, idtipocal, "ESTADO") != "C")
                {
                    if (UtileriasFechas.IsDate(txtFechaProceso.Text))
                    {

                        if (UtileriasFechas.compararFecha(txtFechaProceso.Text, ">=", txtFechaIni.Text) && UtileriasFechas.compararFecha(txtFechaProceso.Text, "<=", txtFechaFin.Text))
                        {
                            ui_upddestajo ui_detalle = new ui_upddestajo();
                            ui_detalle._FormPadre = this;
                            ui_detalle.setValores(idcia, idproddes, messem, anio, idtipoper, idtipocal, idzontra, tiporegistro, fecha, idtipoplan, emplea, estane, iddestajo);
                            ui_detalle.ui_newDestajo();
                            ui_detalle.Activate();
                            ui_detalle.BringToFront();
                            ui_detalle.ShowDialog();
                            ui_detalle.Dispose();
                        }
                        else
                        {
                            MessageBox.Show("Fecha fuera de Rango", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);

                        }
                    }
                    else
                    {
                        MessageBox.Show("Fecha no válida", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
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

        private void cmbTipoRegistro_SelectedIndexChanged(object sender, EventArgs e)
        {

            Funciones fn = new Funciones();

            if (fn.getValorComboBox(cmbTipoRegistro, 1).Equals("P"))
            {
                string idcia = this._idcia;
                string query = "SELECT idtipoplan as clave,destipoplan as descripcion FROM tipoplan where idtipoplan in (select idtipoplan from reglabcia where idcia='" + @idcia + "') order by 1 asc;";
                fn.listaComboBox(query, cmbTipoPlan, "");
                lblTipoPlanilla.Visible = true;
                cmbTipoPlan.Visible = true;
                cmbTipoPlan.Focus();
            }
            else
            {
                lblTipoPlanilla.Visible = false;
                cmbTipoPlan.Visible = false;
            }

            ui_ListaDestajo();
        }

        private void cmbProducto_SelectedIndexChanged(object sender, EventArgs e)
        {
            Funciones funciones = new Funciones();
            string idcia = this._idcia;
            string idproddes = funciones.getValorComboBox(cmbProducto, 2);
            string query = "SELECT codvar as clave,desvar as descripcion FROM varproddes WHERE idcia='" + @idcia + "' and idproddes='" + @idproddes + "' order by codvar asc;";
            funciones.listaComboBox(query, cmbVariedad, "X");
            ui_ListaDestajo();
            cmbVariedad.Focus();

        }

        private void cmbZona_SelectedIndexChanged(object sender, EventArgs e)
        {
            ui_ListaDestajo();
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {

            Funciones funciones = new Funciones();
            CalPlan calplan = new CalPlan();

            string idcia = this._idcia;
            string idproddes = funciones.getValorComboBox(cmbProducto, 2);
            string idzontra = funciones.getValorComboBox(cmbZona, 2);
            string periodo = txtMesSem.Text.Trim() + funciones.replicateCadena(" ", 6);
            string messem = periodo.Substring(0, 2);
            string anio = periodo.Substring(3, 4);
            string fecha = txtFechaProceso.Text;
            string idtipocal = this._idtipocal;
            string idtipoper = this._idtipoper;
            string tiporegistro = funciones.getValorComboBox(cmbTipoRegistro, 1);
            string idtipoplan = funciones.getValorComboBox(cmbTipoPlan, 3);
            string emplea = funciones.getValorComboBox(cmbEmpleador, 11);
            string estane = funciones.getValorComboBox(cmbEstablecimiento, 4);

            if (calplan.getDatosCalPlan(messem, anio, idtipoper, idcia, idtipocal, "ESTADO") != "C")
            {
                Int32 selectedCellCount =
                   dgvdetalle.GetCellCount(DataGridViewElementStates.Selected);


                if (selectedCellCount > 0)
                {
                    string idperplan = dgvdetalle.Rows[dgvdetalle.SelectedCells[1].RowIndex].Cells[0].Value.ToString();

                    string cantidad = dgvdetalle.Rows[dgvdetalle.SelectedCells[1].RowIndex].Cells[5].Value.ToString();
                    string precio = dgvdetalle.Rows[dgvdetalle.SelectedCells[1].RowIndex].Cells[6].Value.ToString();
                    string subtotal = dgvdetalle.Rows[dgvdetalle.SelectedCells[1].RowIndex].Cells[7].Value.ToString();
                    string movilidad = dgvdetalle.Rows[dgvdetalle.SelectedCells[1].RowIndex].Cells[8].Value.ToString();
                    string refrigerio = dgvdetalle.Rows[dgvdetalle.SelectedCells[1].RowIndex].Cells[9].Value.ToString();
                    string adicional = dgvdetalle.Rows[dgvdetalle.SelectedCells[1].RowIndex].Cells[10].Value.ToString();
                    string total = dgvdetalle.Rows[dgvdetalle.SelectedCells[1].RowIndex].Cells[11].Value.ToString();
                    string iddestajo = dgvdetalle.Rows[dgvdetalle.SelectedCells[1].RowIndex].Cells[12].Value.ToString();
                    string codvar = dgvdetalle.Rows[dgvdetalle.SelectedCells[1].RowIndex].Cells[13].Value.ToString();

                    ui_upddestajo ui_detalle = new ui_upddestajo();
                    ui_detalle._FormPadre = this;
                    ui_detalle.setValores(idcia, idproddes, messem, anio, idtipoper, idtipocal, idzontra, tiporegistro, fecha, idtipoplan, emplea, estane, iddestajo);
                    ui_detalle.editar(idperplan, cantidad, precio, subtotal, movilidad, refrigerio, adicional, total, codvar);
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

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            ui_ListaDestajo();
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            Exporta exporta = new Exporta();
            exporta.Excel_FromDataGridView(dgvdetalle);
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            GlobalVariables variables = new GlobalVariables();
            Funciones funciones = new Funciones();
            CalPlan calplan = new CalPlan();

            string idcia = variables.getValorCia();
            string periodo = txtMesSem.Text.Trim() + funciones.replicateCadena(" ", 6);
            string messem = periodo.Substring(0, 2);
            string anio = periodo.Substring(3, 4);
            string idtipocal = this._idtipocal;
            string idtipoper = this._idtipoper;
            string tiporegistro = funciones.getValorComboBox(cmbTipoRegistro, 1);
            if (calplan.getDatosCalPlan(messem, anio, idtipoper, idcia, idtipocal, "ESTADO") != "C")
            {
                Int32 selectedCellCount =
                dgvdetalle.GetCellCount(DataGridViewElementStates.Selected);
                if (selectedCellCount > 0)
                {
                    string desperplan = dgvdetalle.Rows[dgvdetalle.SelectedCells[1].RowIndex].Cells[3].Value.ToString();
                    string iddestajo = dgvdetalle.Rows[dgvdetalle.SelectedCells[1].RowIndex].Cells[12].Value.ToString();

                    DialogResult resultado = MessageBox.Show("¿Desea eliminar la información de destajo del trabajador " + desperplan + "?",
                    "Consulta Importante",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);
                    if (resultado == DialogResult.Yes)
                    {
                        Destajo destajo = new Destajo();
                        destajo.eliminarDestajo(tiporegistro, iddestajo);
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

        private void button1_Click(object sender, EventArgs e)
        {



            Funciones funciones = new Funciones();
            string idcia = this._idcia;
            string idproddes = funciones.getValorComboBox(cmbProducto, 2);
            string idzontra = funciones.getValorComboBox(cmbZona, 2);
            string periodo = txtMesSem.Text.Trim() + funciones.replicateCadena(" ", 6);
            string messem = periodo.Substring(0, 2);
            string anio = periodo.Substring(3, 4);
            string fechaini = txtFechaIni.Text;
            string fechafin = txtFechaFin.Text;
            string idtipocal = this._idtipocal;
            string idtipoper = this._idtipoper;
            string tiporegistro = funciones.getValorComboBox(cmbTipoRegistro, 1);
            string emplea = funciones.getValorComboBox(cmbEmpleador, 11);
            string estane = funciones.getValorComboBox(cmbEstablecimiento, 4);

            if (txtMesSem.Text.Trim().Length > 1)
            {
                ui_resumendestajo ui_rd = new ui_resumendestajo();
                ui_rd.setValores(idcia, idproddes, messem, anio, idtipoper, idtipocal, idzontra, tiporegistro, fechaini, fechafin, emplea, estane);
                ui_rd.Activate();
                ui_rd.BringToFront();
                ui_rd.ShowDialog();
                ui_rd.Dispose();
            }
            else
            {
                MessageBox.Show("No ha seleccionado periodo a visualizar", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }


        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void cmbTipoPlan_SelectedIndexChanged(object sender, EventArgs e)
        {
            ui_ListaDestajo();
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

        private void cmbEmpleador_SelectedIndexChanged(object sender, EventArgs e)
        {
            Funciones funciones = new Funciones();
            GlobalVariables variables = new GlobalVariables();
            string idcia = variables.getValorCia();
            string ruccia = funciones.getValorComboBox(cmbEmpleador, 11);
            string squery_esta = "SELECT idestane as clave,desestane as descripcion FROM estane WHERE codemp='" + @ruccia + "' and idciafile='" + @idcia + "' order by idestane asc;";
            funciones.listaComboBox(squery_esta, cmbEstablecimiento, "");
            ui_ListaDestajo();
            cmbEstablecimiento.Focus();
        }

        private void cmbEstablecimiento_SelectedIndexChanged(object sender, EventArgs e)
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
                string clasepadre = "ui_destajo";
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

        private void cmbVariedad_SelectedIndexChanged(object sender, EventArgs e)
        {
            ui_ListaDestajo();
        }
    }
}