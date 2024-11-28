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
    public partial class ui_upddatosplanilla : Form
    {
        string _empleador;
        string _idcia;
        string _idtipoper;
        string _operacion;
        string _anio;
        string _messem;
        string _idtipocal;
        string _fechaini;
        string _fechafin;
        string _idtipoplan;
        string query = string.Empty;
        CalPlan calplan = new CalPlan();

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

        public void setValores(string idcia, string idtipoper, string empleador, string anio, string messem, string idtipocal, string fechaini, string fechafin, string idtipoplan)
        {
            this._operacion = string.Empty;
            this._idcia = idcia;
            this._idtipoper = idtipoper;
            this._empleador = empleador;
            this._anio = anio;
            this._messem = messem;
            this._idtipocal = idtipocal;
            this._fechaini = fechaini;
            this._fechafin = fechafin;
            this._idtipoplan = idtipoplan;
        }

        public ui_upddatosplanilla()
        {
            InitializeComponent();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            ((ui_ingdatosplanilla)FormPadre).btnActualizar.PerformClick();
            Close();
        }

        private void txtCodigoInterno_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                string idcia = this._idcia;
                string idtipoper = this._idtipoper;
                string rucemp = this._empleador.Substring(0, 11);
                string anio = this._anio;
                string messem = this._messem;
                string idtipocal = this._idtipocal;
                string idtipoplan = this._idtipoplan;

                string cadenaBusqueda = string.Empty;
                this._TextBoxActivo = txtCodigoInterno;
                string condicionAdicional = " and A.idtipoplan='" + @idtipoplan + "' and A.rucemp='" + @rucemp + "' and CONCAT(A.idperplan,A.idcia) not in (Select CONCAT(idperplan,idcia) from dataplan where anio='" + @anio + "' and messem='" + @messem + "' and idtipoper='" + @idtipoper + "' and idtipocal='" + idtipocal + "' and idtipoplan='" + @idtipoplan + "')";
                FiltrosMaestros filtros = new FiltrosMaestros();
                filtros.filtrarPerPlan("ui_upddatosplanilla", this, txtCodigoInterno, idcia, idtipoper, "V", cadenaBusqueda, condicionAdicional);
            }
        }

        public void ui_newDatosPlanilla()
        {
            UtileriasFechas utileriasfechas = new UtileriasFechas();
            this._operacion = "AGREGAR";
            txtEmpleador.Text = this._empleador;
            txtCodigoInterno.Enabled = true;
            lblF2.Visible = true;
            pictureBoxBuscar.Visible = true;
            txtCodigoInterno.Clear();
            txtNombres.Clear();
            txtDocIdent.Clear();
            txtNroDocIden.Clear();
            txtFecIniPerLab.Clear();
            txtEstablecimiento.Clear();
            cmbSCTR.Text = "";
            txtntardanzas.Text = "00:00";
            txtnhoras.Text = "0";
            txtDiasEfeLab.Text = "0";
            txtDiasDom.Text = "0";
            txtDiasSubsi.Text = "0";
            txtDiasNoSubsi.Text = "0";
            txtDiasTotal.Text = "0";
            txtDiurnos.Text = "0";
            txtNocturnos.Text = "0";
            txtHExt25.Text = "0";
            txtHExt35.Text = "0";
            txtHExt100.Text = "0";
            tabControl.Visible = false;

            if (this._idtipocal.Equals("V"))
            {
                chkRegVac.Checked = true;
                chkRegVac.Visible = false;
                txtInicio.Enabled = true;
                txtFin.Enabled = true;
                txtAnio.Enabled = true;
                txtAnio.Text = Convert.ToString(DateTime.Now.Year);
                txtInicio.Text = this._fechaini;
                txtFin.Text = this._fechafin;
                txtDiasVaca.Text = Convert.ToString(utileriasfechas.diferenciaEntreFechas(txtInicio.Text, txtFin.Text));
            }
            else
            {
                chkRegVac.Checked = false;
                chkRegVac.Visible = true;
                txtInicio.Enabled = false;
                txtFin.Enabled = false;
                txtAnio.Enabled = false;
                txtAnio.Text = "";
                txtInicio.Text = "";
                txtFin.Text = "";
                txtDiasVaca.Text = "0";
                ui_calculaDias();
            }
            ui_ListaConceptos();
            ui_ListaTareo();
            txtCodigoInterno.Focus();
        }

        public void ui_loadDatosPlanilla(string idperplan, string establecimiento, string riesgo,
            string diasefelab, string diassubsi, string diasnosubsi, string diastotal,
            string idestane, string hext25, string hext35, string hext100, string finivac,
            string ffinvac, string diasvac, string candes, string impdes, string diasdiurnos,
            string diasnocturnos, string diasdom, string regvac, string pervac, string nhoras, string ntardanzas)
        {

            UtileriasFechas utileriasfechas = new UtileriasFechas();
            Funciones funciones = new Funciones();
            PerPlan perplan = new PerPlan();
            EstAne estane = new EstAne();

            this._operacion = "EDITAR";
            txtEmpleador.Text = this._empleador;
            txtCodigoInterno.Enabled = false;
            lblF2.Visible = false;
            pictureBoxBuscar.Visible = false;
            txtCodigoInterno.Text = idperplan;
            txtDocIdent.Text = perplan.ui_getDatosPerPlan(this._idcia, idperplan, "1");
            txtNroDocIden.Text = perplan.ui_getDatosPerPlan(this._idcia, idperplan, "2");
            txtNombres.Text = perplan.ui_getDatosPerPlan(this._idcia, idperplan, "3");
            txtFecIniPerLab.Text = perplan.ui_getDatosPerPlan(this._idcia, idperplan, "4");
            txtEstablecimiento.Text = establecimiento;
            txtRiesgo.Text = estane.ui_getDatosEstane(idestane, this._empleador.Substring(0, 11), this._idcia, "2");
            if (estane.ui_getDatosEstane(idestane, this._empleador.Substring(0, 11), this._idcia, "2").Equals("SI"))
            {
                cmbSCTR.Enabled = true;
                cmbSCTR.Text = riesgo;
            }
            else
            {
                cmbSCTR.Text = "0";
                cmbSCTR.Enabled = false;
            }

            if (regvac.Equals("S"))
            {
                chkRegVac.Checked = true;
                txtAnio.Enabled = true;
                txtInicio.Enabled = true;
                txtFin.Enabled = true;
                txtAnio.Text = pervac;
                txtInicio.Text = finivac;
                txtFin.Text = ffinvac;
                txtDiasVaca.Text = diasvac;
            }
            else
            {
                chkRegVac.Checked = false;
                txtInicio.Enabled = false;
                txtFin.Enabled = false;
                txtAnio.Enabled = false;
                txtAnio.Text = "";
                txtInicio.Text = "";
                txtFin.Text = "";
                txtDiasVaca.Text = "0";

            }


            if (this._idtipocal.Equals("V"))
            {
                txtntardanzas.Text = "00:00";
                txtnhoras.Text = "0";
                txtDiasEfeLab.Text = "0";
                txtDiasDom.Text = "0";
                txtDiasSubsi.Text = "0";
                txtDiasNoSubsi.Text = "0";
                txtDiasTotal.Text = "0";
                txtNocturnos.Text = "0";
                txtDiurnos.Text = "0";
                txtHExt25.Text = "0";
                txtHExt35.Text = "0";
                txtHExt100.Text = "0";
                chkRegVac.Visible = false;
                tabControl.Visible = true;
                grpDias.Visible = false;
                grpTipoDias.Visible = false;
                grpHoras.Visible = false;
                tabPageEX3.Enabled = false;
                txtInicio.Focus();
            }
            else
            {
                txtntardanzas.Text = ntardanzas;
                txtnhoras.Text = nhoras;
                txtDiasEfeLab.Text = diasefelab;
                txtNocturnos.Text = diasnocturnos;
                txtDiasDom.Text = diasdom;
                txtDiasSubsi.Text = diassubsi;
                txtDiasNoSubsi.Text = diasnosubsi;
                txtDiasTotal.Text = diastotal;
                txtDiurnos.Text = diasdiurnos;
                txtHExt25.Text = hext25;
                txtHExt35.Text = hext35;
                txtHExt100.Text = hext100;
                tabControl.Visible = true;
                chkRegVac.Visible = true;
                grpDias.Visible = true;
                grpTipoDias.Visible = true;
                grpHoras.Visible = true;
                if (this._idtipocal.Equals("D"))
                {
                    tabPageEX3.Enabled = true;
                }
                else
                {
                    tabPageEX3.Enabled = false;
                }
                tabControl.SelectTab(0);
                txtDiasEfeLab.Focus();
            }

            ui_ListaConceptos();
            ui_ListaTareo();
            cmbSCTR.Focus();
            diassubsidiados();
            ui_calHorasAsistencia(idperplan, this._idcia);
        }

        public void diassubsidiados()
        {
            Funciones funciones = new Funciones();
            string data = this.txtCodigoInterno.Text;
            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();
            string idcia_v = this._idcia;
            DataTable temporal = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            query = "select idperplan,anio,messem,ifnull(sum(diassubsi),0) as diasum from dataplan where diassubsi>0 ";
            query += "and idperplan='" + @data + "' and anio=year(curdate()) and idcia='" + @idcia_v + "' group by idcia,idtipoper";
            da.SelectCommand = new SqlCommand(query, conexion);
            da.Fill(temporal);

            if (temporal.Rows.Count > 0)
            {
                foreach (DataRow row_registro in temporal.Rows)
                {
                    this.txt_subsit.Text = row_registro["diasum"].ToString();
                }
                try
                {


                }
                catch (SqlException ex)
                {
                    MessageBox.Show(ex.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
                conexion.Close();

            }
        }

        private void ui_calculaDias()
        {
            string idcia = this._idcia;
            string messem = this._messem;
            string anio = this._anio;
            string idtipoper = this._idtipoper;
            string idtipocal = this._idtipocal;
            int dias = 0;
            int nDiasDom = 0;
            try
            {
                int nDiasNoSubsi = int.Parse(txtDiasNoSubsi.Text);
                int nDiasSubsi = int.Parse(txtDiasSubsi.Text);
                int nDiasDiurnos = int.Parse(txtDiurnos.Text);

                if (idtipocal != "V")
                {
                    dias = int.Parse(calplan.getDatosCalPlan(messem, anio, idtipoper, idcia, idtipocal, "DIAS_FI_FF"));

                    if (nDiasNoSubsi == dias)
                    {
                        nDiasDom = 0;
                    }
                    else
                    {
                        nDiasDom = int.Parse(calplan.getDatosCalPlan(messem, anio, idtipoper, idcia, idtipocal, "DIASDOM"));
                    }
                }


                if (dias >= nDiasSubsi + nDiasDom + nDiasNoSubsi)
                {

                    int nDiasNocturnos;
                    int nDiasEfeLab = dias - (nDiasSubsi + nDiasDom + nDiasNoSubsi);
                    if (nDiasEfeLab >= nDiasDiurnos)
                    {
                        nDiasNocturnos = nDiasEfeLab - nDiasDiurnos;
                    }
                    else
                    {
                        nDiasDiurnos = nDiasEfeLab;
                        nDiasNocturnos = 0;
                    }

                    txtDiasEfeLab.Text = Convert.ToString(nDiasEfeLab);
                    txtDiasDom.Text = Convert.ToString(nDiasDom);
                    txtDiasSubsi.Text = Convert.ToString(nDiasSubsi);
                    txtDiasNoSubsi.Text = Convert.ToString(nDiasNoSubsi);
                    txtDiasTotal.Text = Convert.ToString(dias);
                    txtDiurnos.Text = Convert.ToString(nDiasDiurnos);
                    txtNocturnos.Text = Convert.ToString(nDiasNocturnos);

                }

            }
            catch (FormatException) { }

        }

        private void ui_actualizaComboBox(string idtipoplan, string idtipoper, string idcia, string idperplan, string idtipocal, string anio, string messem)
        {
            Funciones funciones = new Funciones();
            query = "  select A.idconplan as clave,A.desboleta as descripcion from detconplan  A  left join ";
            query += " conplan B on A.idtipoplan=B.idtipoplan ";
            query += " and A.idconplan=B.idconplan and A.idcia=B.idcia and A.idtipocal=B.idtipocal ";
            query += " where A.idcia='" + @idcia + "' and A.idtipoplan='" + @idtipoplan + "' ";
            query += " and A.idtipoper='" + @idtipoper + "' and A.idtipocal='" + @idtipocal + "' ";
            query += " and B.tipo='O' and A.destajo='NO' and CONCAT(A.idconplan,A.idtipoplan,A.idcia,A.idtipocal) ";
            query += " not in (select CONCAT(idconplan,idtipoplan,idcia,idtipocal) from confijos ";
            query += " where idtipoplan='" + @idtipoplan + "' and idtipoper='" + @idtipoper + "' and idtipocal='" + @idtipocal + "' ";
            query += " and idcia='" + @idcia + "' and  idperplan='" + @idperplan + "') ";
            query += " and A.idconplan not in (select idconplan from condataplan where idperplan='" + @idperplan + "' ";
            query += " and idcia='" + @idcia + "' and messem='" + @messem + "' and anio='" + @anio + "' ";
            query += " and idtipoper='" + @idtipoper + "' ";
            query += " and idtipocal='" + @idtipocal + "' and idtipoplan='" + @idtipoplan + "') order by 1 asc";

            funciones.listaComboBox(query, cmbConcepto, "B");
        }

        private void ui_upddatosplanilla_Load(object sender, EventArgs e)
        {
            string idperplan = txtCodigoInterno.Text.Trim();
            this.Text = "Información de Planilla del Periodo " + this._messem + "/" + this._anio + "  " + "Del " + this._fechaini + " al " + this._fechafin;
            ui_actualizaComboBox(this._idtipoplan, this._idtipoper, this._idcia, idperplan, this._idtipocal, this._anio, this._messem);

            if (calplan.getDatosCalPlan(this._messem, this._anio, this._idtipoper, this._idcia, this._idtipocal, "ESTADO") == "V")
            {
                btnAceptar.Enabled = true;
                btnGrabar.Enabled = true;
                btnEliminar.Enabled = true;
                btnNuevoDes.Enabled = true;
                btnEditaDes.Enabled = true;
                btnEliminaDes.Enabled = true;
            }
            else
            {
                btnAceptar.Enabled = false;
                btnGrabar.Enabled = false;
                btnEliminar.Enabled = false;
                btnNuevoDes.Enabled = false;
                btnEditaDes.Enabled = false;
                btnEliminaDes.Enabled = false;
            }
        }

        public void ui_ListaTareo()
        {
            Funciones funciones = new Funciones();
            string idcia = this._idcia;
            string idtipocal = this._idtipocal;
            string idtipoper = this._idtipoper;
            string messem = this._messem;
            string anio = this._anio;
            string idperplan = txtCodigoInterno.Text.Trim();

            query = "  select P.desproddes,E.deszontra,A.cantidad,A.precio,A.total,A.glosa, ";
            query += " A.idproddes,A.idzontra,A.idperplan from tareo A  ";
            query += " left join zontra E on A.idzontra=E.idzontra and A.idcia=E.idcia ";
            query += " left join proddes P on A.idproddes=P.idproddes and A.idcia=P.idcia ";
            query += " where A.idcia='" + @idcia + "' and idtipocal='" + @idtipocal + "' and A.idtipoper='" + @idtipoper + "' ";
            query += " and A.messem='" + @messem + "' and A.anio='" + @anio + "' and idperplan='" + @idperplan + "' ";
            query += " order by P.desproddes asc,E.deszontra asc;";

            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();

            try
            {
                using (SqlDataAdapter myDataAdapter = new SqlDataAdapter(query, conexion))
                {
                    DataSet myDataSet = new DataSet();
                    myDataAdapter.Fill(myDataSet, "tblTareo");
                    funciones.formatearDataGridView(dgvtareo);

                    dgvtareo.DataSource = myDataSet.Tables["tblTareo"];
                    dgvtareo.Columns[0].HeaderText = "Producto";
                    dgvtareo.Columns[1].HeaderText = "Zona de Trabajo";
                    dgvtareo.Columns[2].HeaderText = "Cantidad";
                    dgvtareo.Columns[3].HeaderText = "Precio Unit.";
                    dgvtareo.Columns[4].HeaderText = "Importe";
                    dgvtareo.Columns[5].HeaderText = "Glosa";

                    dgvtareo.Columns["idproddes"].Visible = false;
                    dgvtareo.Columns["idzontra"].Visible = false;
                    dgvtareo.Columns["idperplan"].Visible = false;

                    dgvtareo.Columns[3].DefaultCellStyle.Format = "###,###.##";
                    dgvtareo.Columns[4].DefaultCellStyle.Format = "###,###.##";
                    dgvtareo.Columns[5].DefaultCellStyle.Format = "###,###.##";

                    dgvtareo.Columns[0].Width = 150;
                    dgvtareo.Columns[1].Width = 150;
                    dgvtareo.Columns[2].Width = 70;
                    dgvtareo.Columns[3].Width = 70;
                    dgvtareo.Columns[4].Width = 70;
                    dgvtareo.Columns[5].Width = 220;
                }

                float cantidad = float.Parse(funciones.sumaColumnaDataGridView(dgvtareo, 2));
                float importe = float.Parse(funciones.sumaColumnaDataGridView(dgvtareo, 4));
                txtCanDes.Text = Convert.ToString(cantidad);
                txtImpDes.Text = Convert.ToString(importe);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }

            conexion.Close();
        }

        private void ui_ListaConceptos()
        {
            try
            {
                string idcia = this._idcia;
                string anio = this._anio;
                string messem = this._messem;
                string idtipoper = this._idtipoper;
                string idtipocal = this._idtipocal;
                string idtipoplan = this._idtipoplan;
                string idperplan = txtCodigoInterno.Text.Trim();

                Funciones funciones = new Funciones();
                SqlConnection conexion = new SqlConnection();
                conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
                conexion.Open();

                query = "  select A.idconplan,E.desboleta,A.valor,A.tipocalculo,A.idpresper,A.comen ";
                query += " from condataplan A left join detconplan E on  ";
                query += " A.idconplan=E.idconplan and A.idtipoplan=E.idtipoplan ";
                query += " and A.idtipoper=E.idtipoper and A.idcia=E.idcia and ";
                query += " A.idtipocal=E.idtipocal ";
                query += " where  A.idcia='" + @idcia + "' and A.idperplan='" + @idperplan + "' ";
                query += " and A.anio='" + @anio + "' and A.messem='" + @messem + "' ";
                query += " and A.idtipocal='" + @idtipocal + "' and A.idtipoper='" + @idtipoper + "' ";
                query += " and A.idtipoplan='" + @idtipoplan + "' ;";

                using (SqlDataAdapter myDataAdapter = new SqlDataAdapter(query, conexion))
                {
                    DataSet myDataSet = new DataSet();
                    myDataAdapter.Fill(myDataSet, "tblConDataPlan");
                    funciones.formatearDataGridView(dgvdetalle);

                    dgvdetalle.DataSource = myDataSet.Tables["tblConDataPlan"];
                    dgvdetalle.Columns[0].HeaderText = "Concepto";
                    dgvdetalle.Columns[1].HeaderText = "Des.Boleta";
                    dgvdetalle.Columns[2].HeaderText = "Valor";
                    dgvdetalle.Columns[4].HeaderText = "Código Préstamo";
                    dgvdetalle.Columns[5].HeaderText = "Glosa";

                    dgvdetalle.Columns["tipocalculo"].Visible = false;
                    dgvdetalle.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    dgvdetalle.Columns[2].DefaultCellStyle.Format = "###,###.##";

                    dgvdetalle.Columns[0].Width = 60;
                    dgvdetalle.Columns[1].Width = 150;
                    dgvdetalle.Columns[2].Width = 70;
                    dgvdetalle.Columns[4].Width = 70;
                    dgvdetalle.Columns[5].Width = 100;
                }
                conexion.Close();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void txtCodigoInterno_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                if (txtCodigoInterno.Text.Trim() != string.Empty)
                {
                    string idcia = this._idcia;
                    string idperplan = txtCodigoInterno.Text.Trim();

                    PerPlan perplan = new PerPlan();
                    string codigoInterno = perplan.ui_getDatosPerPlan(idcia, idperplan, "0");
                    if (codigoInterno == string.Empty)
                    {
                        MessageBox.Show("Código no registrado del trabajador", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txtCodigoInterno.Clear();
                        txtDocIdent.Clear();
                        txtNroDocIden.Clear();
                        txtNombres.Clear();
                        txtFecIniPerLab.Clear();
                        txtEstablecimiento.Clear();
                        txtRiesgo.Clear();
                        cmbSCTR.Text = "0";
                        cmbSCTR.Enabled = false;
                    }
                    else
                    {
                        txtCodigoInterno.Text = perplan.ui_getDatosPerPlan(idcia, idperplan, "0");
                        txtDocIdent.Text = perplan.ui_getDatosPerPlan(idcia, idperplan, "1");
                        txtNroDocIden.Text = perplan.ui_getDatosPerPlan(idcia, idperplan, "2");
                        txtNombres.Text = perplan.ui_getDatosPerPlan(idcia, idperplan, "3");
                        txtFecIniPerLab.Text = perplan.ui_getDatosPerPlan(idcia, idperplan, "4");
                        txtEstablecimiento.Text = perplan.ui_getDatosPerPlan(idcia, idperplan, "5");
                        txtRiesgo.Text = perplan.ui_getDatosPerPlan(idcia, idperplan, "6");

                        if (perplan.ui_getDatosPerPlan(idcia, idperplan, "6").Equals("SI"))
                        {
                            Funciones funciones = new Funciones();
                            cmbSCTR.Enabled = true;
                            string rucemp = this._empleador.Substring(0, 11);
                            query = "SELECT tasa as clave,'' as descripcion FROM tasaest where codemp='" + @rucemp + "' and idciafile='" + @idcia + "' order by 1 asc";
                            funciones.listaComboBox(query, cmbSCTR, "");
                            e.Handled = true;
                            cmbSCTR.Focus();
                        }
                        else
                        {
                            cmbSCTR.Text = "0";
                            cmbSCTR.Enabled = false;
                            e.Handled = true;
                            toolStripForm.Items[0].Select();
                            toolStripForm.Focus();
                        }
                        ui_calHorasAsistencia(txtCodigoInterno.Text, this._idcia);
                        ui_actualizaComboBox(this._idtipoplan, this._idtipoper, this._idcia, txtCodigoInterno.Text, this._idtipocal, this._anio, this._messem);
                        diassubsidiados();
                    }
                }
                else
                {
                    txtCodigoInterno.Clear();
                    txtDocIdent.Clear();
                    txtNroDocIden.Clear();
                    txtNombres.Clear();
                    txtFecIniPerLab.Clear();
                    txtEstablecimiento.Clear();
                    txtRiesgo.Clear();
                    cmbSCTR.Text = "0";
                    cmbSCTR.Enabled = false;
                    txtCodigoInterno.Focus();
                }
            }
        }

        private void ui_calHorasAsistencia(string v_idperplan, string idcia)
        {
            string v_tothorastarde = "", v_total_horas = "", v_xtra25 = "", v_xtra35 = "", total_dias = "";
            string fecini = calplan.getDatosCalPlan(this._messem, this._anio, this._idtipoper, this._idcia, this._idtipocal, "FECHAINI").Split(' ')[0];
            string fecfin = calplan.getDatosCalPlan(this._messem, this._anio, this._idtipoper, this._idcia, this._idtipocal, "FECHAFIN").Split(' ')[0];
            string bd_prov = ConfigurationManager.AppSettings.Get("BD_PROV");
            string _bd = (bd_prov.Equals("accounting")) ? "A" : (bd_prov.Equals("planlima")) ? "L" : "P";

            query = "CALL Sp_calHorasAsistencia('" + v_idperplan + "','" + fecini.Split('/')[2] + "-" + fecini.Split('/')[1] + "-" + fecini.Split('/')[0] + "',";
            query += "'" + fecfin.Split('/')[2] + "-" + fecfin.Split('/')[1] + "-" + fecfin.Split('/')[0] + "', '" + idcia + "', '" + _bd + "');";

            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();

            try
            {
                using (SqlCommand cmd = new SqlCommand(query, conexion))
                {
                    using (SqlDataReader odr = cmd.ExecuteReader())
                    {
                        if (odr.HasRows)
                        {
                            odr.Read();
                            v_tothorastarde = odr["v_tothorastarde"].ToString();
                            v_total_horas = odr["v_total_horas"].ToString();
                            v_xtra25 = odr["v_xtra25"].ToString();
                            v_xtra35 = odr["v_xtra35"].ToString();
                            total_dias = odr["total_dias"].ToString();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }

            conexion.Close();

            txtnhoras.Text = (txtnhoras.Text.Equals("")) ? v_total_horas : txtnhoras.Text;
            txtntardanzas.Text = (txtntardanzas.Text.Equals("")) ? v_tothorastarde : txtntardanzas.Text;
            txtHExt25.Text = (txtHExt25.Text.Equals("")) ? v_xtra25 : txtHExt25.Text/*(int.Parse(txtHExt25.Text) + int.Parse(v_xtra25)].ToString()*/;
            txtHExt35.Text = (txtHExt35.Text.Equals("")) ? v_xtra35 : txtHExt35.Text/*(int.Parse(txtHExt35.Text) + int.Parse(v_xtra35)].ToString()*/;
            txt_tot_dias.Text = (txt_tot_dias.Text.Equals("")) ? total_dias.ToString() : txt_tot_dias.Text;
        }

        private void txtCodigoInterno_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtDiasEfeLab_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void txtDiasSubsi_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                e.Handled = true;
                txtDiasNoSubsi.Focus();


            }
        }

        private void txtDiasNoSubsi_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                e.Handled = true;
                txtDiurnos.Focus();

            }
        }

        private void cmbSCTR_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                e.Handled = true;
                toolStripForm.Items[0].Select();
                toolStripForm.Focus();

            }
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            try
            {
                string idcia = this._idcia;
                string anio = this._anio;
                string messem = this._messem;
                string idtipoper = this._idtipoper;
                string idtipocal = this._idtipocal;
                string idperplan = txtCodigoInterno.Text.Trim();
                string idtipoplan = this._idtipoplan;

                if (idperplan != string.Empty)
                {
                    int diasefelab = int.Parse(txtDiasEfeLab.Text);
                    int diasdom = int.Parse(txtDiasDom.Text);
                    int diassubsi = int.Parse(txtDiasSubsi.Text);
                    int diasnosubsi = int.Parse(txtDiasNoSubsi.Text);
                    int diastotal = int.Parse(txtDiasTotal.Text);

                    int diurno = int.Parse(txtDiurnos.Text);
                    int nocturno = int.Parse(txtNocturnos.Text);

                    string emplea = this._empleador.Substring(0, 11);
                    string estane = txtEstablecimiento.Text.Substring(0, 4).Trim();
                    float riesgo = float.Parse(cmbSCTR.Text);
                    float hext25 = float.Parse(txtHExt25.Text);
                    float hext35 = float.Parse(txtHExt35.Text);
                    float hext100 = float.Parse(txtHExt100.Text);
                    float candes = float.Parse(txtCanDes.Text);
                    float impdes = float.Parse(txtImpDes.Text);

                    string regvac;
                    string finivac;
                    string ffinvac;
                    int diasvac;
                    string pervac;

                    if (chkRegVac.Checked)
                    {
                        regvac = "S";
                        pervac = txtAnio.Text;
                        finivac = txtInicio.Text;
                        ffinvac = txtFin.Text;
                        UtileriasFechas utileriasfechas = new UtileriasFechas();
                        diasvac = utileriasfechas.diferenciaEntreFechas(txtInicio.Text, txtFin.Text);
                    }
                    else
                    {
                        pervac = "";
                        regvac = "N";
                        finivac = "";
                        ffinvac = "";
                        diasvac = 0;
                    }

                    string valorValida = "G";

                    if (txtCodigoInterno.Text == string.Empty && valorValida == "G")
                    {
                        valorValida = "B";
                        MessageBox.Show("No ha seleccionado Trabajador", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txtCodigoInterno.Focus();
                    }


                    if ((riesgo < 0 || riesgo > 100) && valorValida == "G")
                    {
                        valorValida = "B";
                        MessageBox.Show("Rango de % SCTR-Salud inválido, se aceptan valores de 0 a 100", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        cmbSCTR.Focus();
                    }

                    if (regvac.Equals("S"))
                    {
                        if (diasvac == 0 && valorValida == "G")
                        {
                            valorValida = "B";
                            MessageBox.Show("Periodo de Vacaciones no válido", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            txtInicio.Focus();
                        }
                    }

                    if ((diasefelab != diurno + nocturno) && valorValida == "G")
                    {
                        valorValida = "B";
                        MessageBox.Show("La suma de dias Diurnos y Nocturnos no coinciden con los días efectivamente laborados", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txtDiurnos.Focus();
                    }


                    if ((diastotal != diasefelab + diasdom + diassubsi + diasnosubsi) && valorValida == "G")
                    {
                        valorValida = "B";
                        MessageBox.Show("La suma de dias no coinciden con el total dias", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txtDiasDom.Focus();
                    }

                    if (valorValida.Equals("G"))
                    {

                        DataPlan dataplan = new DataPlan();
                        dataplan.actualizarDataPlan(this._operacion, idperplan, idcia, anio, messem, idtipoper,
                            idtipocal, diasefelab, diassubsi, diasnosubsi, diastotal, emplea, estane,
                            riesgo, idtipoplan, hext25, hext35, hext100, finivac, ffinvac, diasvac,
                            impdes, candes, diurno, nocturno, diasdom, regvac, pervac);

                        if (this._operacion.Equals("EDITAR"))
                        {
                            MessageBox.Show("Datos guardados exitosamente", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            this._operacion = "EDITAR";
                        }

                        tabControl.Visible = true;
                        if (idtipocal.Equals("V"))
                        {
                            chkRegVac.Visible = false;
                            grpDias.Visible = false;
                            grpHoras.Visible = false;
                            grpTipoDias.Visible = false;
                            tabPageEX3.Enabled = false;
                        }
                        else
                        {
                            chkRegVac.Visible = true;
                            grpDias.Visible = true;
                            grpHoras.Visible = true;
                            grpTipoDias.Visible = true;
                            if (this._idtipocal.Equals("D"))
                            {
                                tabPageEX3.Enabled = true;
                            }
                            else
                            {
                                tabPageEX3.Enabled = false;
                            }
                        }
                        tabControl.SelectTab(0);
                    }
                }
                else
                {
                    MessageBox.Show("No ha seleccionado trabajador", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtCodigoInterno.Focus();
                }

            }
            catch (FormatException ex)
            {
                MessageBox.Show(ex.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void label15_Click(object sender, EventArgs e)
        {


        }

        private void txtInicioPeriodo_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }

        private void label16_Click(object sender, EventArgs e)
        {

        }

        private void txtFinPeriodo_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }

        private void cmbMotivoFinPeriodo_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label17_Click(object sender, EventArgs e)
        {

        }

        private void txtDiasSubsi_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                if (this._operacion.Equals("EDITAR"))
                {

                    string idperplan = txtCodigoInterno.Text.Trim();
                    if (idperplan != string.Empty)
                    {
                        PerPlan perplan = new PerPlan();
                        string idcia = this._idcia;
                        string idtipoper = this._idtipoper;
                        string idtipoplan = this._idtipoplan;
                        string rucemp = this._empleador.Substring(0, 11);
                        string anio = this._anio;
                        string messem = this._messem;
                        string idtipocal = this._idtipocal;
                        string nombretrabajador = txtNombres.Text.Trim();
                        string sexo = perplan.ui_getDatosPerPlan(idcia, idperplan, "7");

                        ui_diassubsi ui_diassubsi = new ui_diassubsi();
                        ui_diassubsi._FormPadre = this;
                        ui_diassubsi.setValores(idcia, this._empleador, idtipoper, idperplan, nombretrabajador, anio, messem, idtipocal, this._fechaini, this._fechafin, sexo, this._operacion, idtipoplan);
                        ui_diassubsi.Activate();
                        ui_diassubsi.BringToFront();
                        ui_diassubsi.ShowDialog();
                        ui_diassubsi.Dispose();
                    }
                    else
                    {
                        MessageBox.Show("No ha seleccionado trabajador", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txtCodigoInterno.Focus();
                    }
                }
                else
                {
                    MessageBox.Show("Primero deberá de grabar el registro", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }

            }



        }

        private void txtDiasSubsi_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }

        public void ui_actualizar()
        {
            try
            {
                if (this._operacion.Equals("EDITAR"))
                {
                    string idcia = this._idcia;
                    string anio = this._anio;
                    string messem = this._messem;
                    string idtipoper = this._idtipoper;
                    string idtipocal = this._idtipocal;
                    string idtipoplan = this._idtipoplan;
                    string idperplan = txtCodigoInterno.Text.Trim();
                    int diasefelab = int.Parse(txtDiasEfeLab.Text);
                    int diasdom = int.Parse(txtDiasDom.Text);
                    int diassubsi = int.Parse(txtDiasSubsi.Text);
                    int diasnosubsi = int.Parse(txtDiasNoSubsi.Text);
                    int diastotal = int.Parse(txtDiasTotal.Text);
                    int diurno = int.Parse(txtDiurnos.Text);
                    int nocturno = int.Parse(txtNocturnos.Text);
                    int diasvac = int.Parse(txtDiasVaca.Text);
                    float candes = float.Parse(txtCanDes.Text);
                    float impdes = float.Parse(txtImpDes.Text);
                    DataPlan dataplan = new DataPlan();
                    dataplan.actualizarDiasDataPlan(idperplan, idcia, anio, messem,
                        idtipoper, idtipocal, idtipoplan, diasefelab, diassubsi, diasnosubsi,
                        diastotal, diurno, nocturno, diasdom, candes, impdes);
                }

            }
            catch (FormatException ex)
            {
                MessageBox.Show(ex.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {

        }

        private void txtDiasSubsi_TextChanged(object sender, EventArgs e)
        {
            try
            {
                UtileriasFechas utileriasfechas = new UtileriasFechas();
                int dias = utileriasfechas.diferenciaEntreFechas(this._fechaini, this._fechafin);
                ui_calculaDias();
            }
            catch (FormatException) { }
        }

        private void txtDiasNoSubsi_TextChanged(object sender, EventArgs e)
        {
            ui_calculaDias();

        }

        private void txtDiasNoSubsi_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                if (this._operacion.Equals("EDITAR"))
                {
                    string idperplan = txtCodigoInterno.Text.Trim();
                    if (idperplan != string.Empty)
                    {
                        PerPlan perplan = new PerPlan();
                        string idcia = this._idcia;
                        string idtipoper = this._idtipoper;
                        string rucemp = this._empleador.Substring(0, 11);
                        string anio = this._anio;
                        string messem = this._messem;
                        string idtipocal = this._idtipocal;
                        string nombretrabajador = txtNombres.Text.Trim();
                        string sexo = perplan.ui_getDatosPerPlan(idcia, idperplan, "7");

                        ui_diasnosubsi ui_diasnosubsi = new ui_diasnosubsi();
                        ui_diasnosubsi._FormPadre = this;
                        ui_diasnosubsi.setValores(idcia, this._empleador, idtipoper, idperplan, nombretrabajador, anio, messem, idtipocal, this._fechaini, this._fechafin, sexo, this._operacion, this._idtipoplan);
                        ui_diasnosubsi.Activate();
                        ui_diasnosubsi.BringToFront();
                        ui_diasnosubsi.ShowDialog();
                        ui_diasnosubsi.Dispose();
                    }
                    else
                    {
                        MessageBox.Show("No ha seleccionado trabajador", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txtCodigoInterno.Focus();
                    }
                }
                else
                {
                    MessageBox.Show("Primero deberá de grabar el registro", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }

            }
        }

        private void txtHExt25_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                e.Handled = true;
                txtHExt35.Focus();


            }
        }

        private void txtHExt35_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                e.Handled = true;
                txtHExt100.Focus();


            }
        }

        private void txtDiasTotal_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void cmbTipoCalculo_SelectedIndexChanged(object sender, EventArgs e)
        {


        }

        private void groupBox6_Enter(object sender, EventArgs e)
        {

        }

        private void btnGrabar_Click(object sender, EventArgs e)
        {
            try
            {
                Funciones funciones = new Funciones();
                DetConPlan detconplan = new DetConPlan();
                string idcia = this._idcia;
                string anio = this._anio;
                string messem = this._messem;
                string idtipoper = this._idtipoper;
                string idtipocal = this._idtipocal;
                string idperplan = txtCodigoInterno.Text.Trim();
                string idtipoplan = this._idtipoplan;
                string tipocalculo = txtTipoCalculo.Text;
                string presta = txtPresta.Text;
                string idpresper = txtPrestamo.Text;
                string idconplan = funciones.getValorComboBox(cmbConcepto, 4);
                float valor = float.Parse(txtValor.Text);
                string comen = txtComentario.Text;
                string valorValida = "G";
                if (idperplan == string.Empty && valorValida == "G")
                {
                    valorValida = "B";
                    MessageBox.Show("No ha seleccionado Trabajador", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtCodigoInterno.Focus();
                }
                if (idconplan == string.Empty && valorValida == "G")
                {
                    valorValida = "B";
                    MessageBox.Show("No ha seleccionado Concepto", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    cmbConcepto.Focus();
                }
                if (tipocalculo == "V" && valor == 0)
                {
                    valorValida = "B";
                    MessageBox.Show("El valor no puede ser cero", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtValor.Focus();
                }
                if (presta == "S" && idpresper == string.Empty)
                {
                    valorValida = "B";
                    MessageBox.Show("Debe de seleccionar el Préstamo", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtPrestamo.Focus();
                }
                if (valorValida == "G")
                {
                    ConDataPlan condataplan = new ConDataPlan();
                    condataplan.actualizarConDataPlan("AGREGAR", idperplan, idcia, anio, messem, idtipoper,
                        idtipocal, idtipoplan, idconplan, tipocalculo, valor, idpresper, comen);
                    ui_ListaConceptos();
                    ui_actualizaComboBox(idtipoplan, idtipoper, idcia, idperplan, idtipocal, anio, messem);
                    txtPrestamo.Text = "";
                    txtFechaPres.Text = "";
                    txtImportePres.Text = "";
                    txtCuotaPres.Text = "";
                    txtValor.Text = "0";
                    txtComentario.Text = "";
                    cmbConcepto.Focus();
                }
            }
            catch (FormatException ex)
            {
                MessageBox.Show(ex.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {

            Int32 selectedCellCount =
            dgvdetalle.GetCellCount(DataGridViewElementStates.Selected);
            if (selectedCellCount > 0)
            {
                string idconplan = dgvdetalle.Rows[dgvdetalle.SelectedCells[1].RowIndex].Cells[0].Value.ToString();
                string idperplan = txtCodigoInterno.Text;


                DialogResult resultado = MessageBox.Show("¿Desea eliminar el Concepto " + idconplan + "?",
                "Consulta Importante",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);
                if (resultado == DialogResult.Yes)
                {
                    ConDataPlan condataplan = new ConDataPlan();
                    condataplan.eliminarConDataPlan(idperplan, _idcia, _anio, _messem, _idtipoper, _idtipocal, _idtipoplan, idconplan);
                    this.ui_ListaConceptos();
                    this.ui_actualizaComboBox(this._idtipoplan, this._idtipoper, this._idcia, idperplan, this._idtipocal, this._anio, this._messem);
                    cmbConcepto.Focus();
                }

            }
            else
            {
                MessageBox.Show("No ha seleccionado registro a Eliminar", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void txtDiasTotal_KeyUp(object sender, KeyEventArgs e)
        {

        }

        private void txtValor_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                e.Handled = true;
                txtComentario.Focus();


            }
        }

        private void txtHExt100_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                tabControl.SelectTab(1);
                cmbConcepto.Focus();
            }


        }

        private void txtInicio_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void txtFin_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                if (UtileriasFechas.IsDate(txtFin.Text))
                {


                    if (UtileriasFechas.compararFecha(txtInicio.Text, "<=", txtFin.Text))
                    {
                        UtileriasFechas utileriasfechas = new UtileriasFechas();
                        txtDiasVaca.Text = Convert.ToString(utileriasfechas.diferenciaEntreFechas(txtInicio.Text, txtFin.Text));
                        e.Handled = true;
                        toolStripForm.Items[0].Select();
                        toolStripForm.Focus();
                    }
                    else
                    {
                        MessageBox.Show("Fecha de Fin no puede ser menor que la Fecha de Inicio", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        e.Handled = true;
                        txtFin.Text = DateTime.Now.ToString("dd/MM/yyyy");
                        txtDiasVaca.Clear();
                        txtFin.Focus();
                    }

                }


                else
                {
                    MessageBox.Show("Fecha de Fin no válida", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    e.Handled = true;
                    txtFin.Text = DateTime.Now.ToString("dd/MM/yyyy");
                    txtDiasVaca.Clear();
                    txtFin.Focus();

                }
            }
        }

        private void txtCanDes_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void txtImpDes_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void txtDiasNoSubsi_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void txtNocturnos_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void txtDiurnos_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                e.Handled = true;
                txtHExt25.Focus();



            }
        }

        private void txtDiurnos_TextChanged(object sender, EventArgs e)
        {

            ui_calculaDias();

        }

        private void txtDiasDom_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void txtDiasDom_TextChanged(object sender, EventArgs e)
        {
            ui_calculaDias();
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            string idperplan = txtCodigoInterno.Text.Trim();
            ui_destajoplan ui_destajoplan = new ui_destajoplan();
            ui_destajoplan._FormPadre = this;
            ui_destajoplan.setValores(idperplan, this._idcia, this._idtipoper, this._anio, this._messem, this._idtipocal, this._idtipoplan);
            ui_destajoplan.ui_newDestajo();
            ui_destajoplan.Activate();
            ui_destajoplan.BringToFront();
            ui_destajoplan.ShowDialog();
            ui_destajoplan.Dispose();

        }

        private void button2_Click(object sender, EventArgs e)
        {

            Int32 selectedCellCount =
            dgvtareo.GetCellCount(DataGridViewElementStates.Selected);
            if (selectedCellCount > Convert.ToInt32(0))
            {
                string desproddes = dgvtareo.Rows[dgvtareo.SelectedCells[1].RowIndex].Cells[0].Value.ToString();
                string deszontra = dgvtareo.Rows[dgvtareo.SelectedCells[1].RowIndex].Cells[1].Value.ToString();
                string idproddes = dgvtareo.Rows[dgvtareo.SelectedCells[1].RowIndex].Cells[6].Value.ToString();
                string idzontra = dgvtareo.Rows[dgvtareo.SelectedCells[1].RowIndex].Cells[7].Value.ToString();
                string idperplan = dgvtareo.Rows[dgvtareo.SelectedCells[1].RowIndex].Cells[8].Value.ToString();

                DialogResult resultado = MessageBox.Show("¿Desea eliminar la información de destajo del producto " + desproddes + " y la zona " + deszontra + " ?",
                "Consulta Importante",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);
                if (resultado == DialogResult.Yes)
                {
                    Tareo tareo = new Tareo();
                    tareo.eliminarTareo(this._idcia, idperplan, this._messem, this._anio, this._idtipocal, this._idtipoper, idproddes, idzontra, this._idtipoplan);
                    this.ui_ListaTareo();
                    this.ui_actualizar();
                }

            }
            else
            {
                MessageBox.Show("No ha seleccionado registro a Eliminar", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            Int32 selectedCellCount =
            dgvtareo.GetCellCount(DataGridViewElementStates.Selected);
            if (selectedCellCount > Convert.ToInt32(0))
            {

                string desproddes = dgvtareo.Rows[dgvtareo.SelectedCells[1].RowIndex].Cells[0].Value.ToString();
                string deszontra = dgvtareo.Rows[dgvtareo.SelectedCells[1].RowIndex].Cells[1].Value.ToString();

                string cantidad = dgvtareo.Rows[dgvtareo.SelectedCells[1].RowIndex].Cells[2].Value.ToString();
                string precio = dgvtareo.Rows[dgvtareo.SelectedCells[1].RowIndex].Cells[3].Value.ToString();
                string total = dgvtareo.Rows[dgvtareo.SelectedCells[1].RowIndex].Cells[4].Value.ToString();

                string glosa = dgvtareo.Rows[dgvtareo.SelectedCells[1].RowIndex].Cells[5].Value.ToString();

                string idproddes = dgvtareo.Rows[dgvtareo.SelectedCells[1].RowIndex].Cells[6].Value.ToString();
                string idzontra = dgvtareo.Rows[dgvtareo.SelectedCells[1].RowIndex].Cells[7].Value.ToString();
                string idperplan = dgvtareo.Rows[dgvtareo.SelectedCells[1].RowIndex].Cells[8].Value.ToString();

                ui_destajoplan ui_destajoplan = new ui_destajoplan();
                ui_destajoplan._FormPadre = this;
                ui_destajoplan.setValores(idperplan, this._idcia, this._idtipoper, this._anio, this._messem, this._idtipocal, this._idtipoplan);
                ui_destajoplan.ui_loadDatos(idproddes, idzontra, cantidad, precio, total, glosa);
                ui_destajoplan.Activate();
                ui_destajoplan.BringToFront();
                ui_destajoplan.ShowDialog();
                ui_destajoplan.Dispose();


            }
            else
            {
                MessageBox.Show("No ha seleccionado registro a Editar", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void dgvtareo_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void tabPageEX1_Click(object sender, EventArgs e)
        {

        }

        private void tabControl_SelectedIndexChanging(object sender, Dotnetrix.Controls.TabPageChangeEventArgs e)
        {

        }

        private void cmbConcepto_SelectedIndexChanged(object sender, EventArgs e)
        {
            DetConPlan detconplan = new DetConPlan();
            Funciones funciones = new Funciones();

            string idcia = this._idcia;
            string anio = this._anio;
            string messem = this._messem;
            string idtipoper = this._idtipoper;
            string idtipocal = this._idtipocal;
            string idtipoplan = this._idtipoplan;
            string idconplan = funciones.getValorComboBox(cmbConcepto, 4);

            txtPrestamo.Text = "";
            txtFechaPres.Text = "";
            txtImportePres.Text = "";
            txtCuotaPres.Text = "";
            txtValor.Text = "0";
            txtComentario.Text = "";

            if (idconplan != string.Empty)
            {
                string automatico = detconplan.getDetConPlan(idtipoplan, idconplan, idtipoper, idcia, idtipocal, "AUTOMATICO");
                string presta = detconplan.getDetConPlan(idtipoplan, idconplan, idtipoper, idcia, idtipocal, "PRESTA");
                if (presta.Equals("S"))
                {
                    txtPrestamo.Enabled = true;
                    txtPresta.Text = "S";
                }
                else
                {
                    txtPrestamo.Enabled = false;
                    txtPresta.Text = "N";
                }
                if (automatico == "N")
                {
                    txtTipoCalculo.Text = "V";
                    lblValor.Visible = true;
                    txtValor.Visible = true;
                    txtValor.Focus();
                }
                else
                {
                    txtTipoCalculo.Text = "F";
                    lblValor.Visible = false;
                    txtValor.Visible = false;
                }
            }
            else
            {
                txtTipoCalculo.Text = "F";
                txtPresta.Text = "N";
                txtPrestamo.Enabled = false;
                lblValor.Visible = false;
                txtValor.Visible = false;
            }

        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void txtFin_KeyPress_1(object sender, KeyPressEventArgs e)
        {

        }

        private void txtInicio_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                if (UtileriasFechas.IsDate(txtInicio.Text))
                {

                    if (UtileriasFechas.compararFecha(txtInicio.Text, ">=", this._fechaini) && UtileriasFechas.compararFecha(txtInicio.Text, "<=", this._fechafin))
                    {


                        UtileriasFechas utileriasfechas = new UtileriasFechas();
                        txtDiasVaca.Text = Convert.ToString(utileriasfechas.diferenciaEntreFechas(txtInicio.Text, txtFin.Text));
                        e.Handled = true;
                        txtFin.Focus();

                    }
                    else
                    {

                        MessageBox.Show("La fecha de inicio no se encuentra dentro del rango del periodo", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        e.Handled = true;
                        txtInicio.Clear();
                        txtFin.Clear();
                        txtDiasVaca.Clear();
                        txtInicio.Focus();
                    }
                }
                else
                {
                    MessageBox.Show("Fecha de Inicio no válida", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    e.Handled = true;
                    txtInicio.Clear();
                    txtFin.Clear();
                    txtDiasVaca.Clear();
                    txtInicio.Focus();

                }
            }
        }

        private void txtFin_KeyPress_2(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                if (UtileriasFechas.IsDate(txtFin.Text))
                {
                    if (UtileriasFechas.compararFecha(txtInicio.Text, "<=", txtFin.Text))
                    {
                        UtileriasFechas utileriasfechas = new UtileriasFechas();
                        txtDiasVaca.Text = Convert.ToString(utileriasfechas.diferenciaEntreFechas(txtInicio.Text, txtFin.Text));
                        e.Handled = true;
                        toolStripForm.Items[0].Select();
                        toolStripForm.Focus();
                    }
                    else
                    {
                        MessageBox.Show("Fecha de Fin no puede ser menor que la Fecha de Inicio", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        e.Handled = true;
                        txtFin.Clear();
                        txtDiasVaca.Clear();
                        txtFin.Focus();
                    }

                }


                else
                {
                    MessageBox.Show("Fecha de Fin no válida", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    e.Handled = true;
                    txtFin.Clear();
                    txtDiasVaca.Clear();
                    txtFin.Focus();

                }

            }
        }

        private void chkRegVac_CheckedChanged(object sender, EventArgs e)
        {
            if (chkRegVac.Checked)
            {
                txtAnio.Enabled = true;
                txtInicio.Enabled = true;
                txtFin.Enabled = true;
                txtAnio.Text = Convert.ToString(DateTime.Now.Year);
                txtInicio.Text = DateTime.Now.ToString("dd/MM/yyyy");
                txtFin.Text = DateTime.Now.ToString("dd/MM/yyyy");
                UtileriasFechas utileriasfechas = new UtileriasFechas();
                txtDiasVaca.Text = Convert.ToString(utileriasfechas.diferenciaEntreFechas(txtInicio.Text, txtFin.Text));
            }
            else
            {
                txtAnio.Enabled = false;
                txtInicio.Enabled = false;
                txtFin.Enabled = false;
                txtAnio.Text = "";
                txtInicio.Text = "";
                txtFin.Text = "";
                txtDiasVaca.Text = "0";
            }
        }

        private void label31_Click(object sender, EventArgs e)
        {

        }

        private void txtImporte_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }

        private void txtComentario_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                e.Handled = true;
                btnGrabar.Focus();


            }
        }

        private void txtPrestamo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                string idcia = this._idcia;
                string idperplan = txtCodigoInterno.Text.Trim();
                string cadenaBusqueda = string.Empty;
                this._TextBoxActivo = txtPrestamo;
                FiltrosMaestros filtros = new FiltrosMaestros();
                filtros.filtrarPresPer("ui_upddatosplanilla", this, txtPrestamo, idcia, idperplan);
            }
        }

        private void txtPrestamo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                if (txtPrestamo.Text.Trim() != string.Empty)
                {
                    string idcia = this._idcia;
                    string idpresper = txtPrestamo.Text.Trim();
                    PresPer presper = new PresPer();
                    string codigopresper = presper.ui_getDatosPresPer(idcia, idpresper, "CODIGO");
                    if (codigopresper == string.Empty)
                    {
                        MessageBox.Show("Código no registrado", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txtPrestamo.Clear();
                        txtFechaPres.Clear();
                        txtImportePres.Clear();
                        txtCuotaPres.Clear();
                        e.Handled = true;
                        txtPrestamo.Focus();
                    }
                    else
                    {
                        txtFechaPres.Text = presper.ui_getDatosPresPer(idcia, idpresper, "FECHA");
                        txtImportePres.Text = presper.ui_getDatosPresPer(idcia, idpresper, "IMPORTE");
                        txtCuotaPres.Text = presper.ui_getDatosPresPer(idcia, idpresper, "CUOTA");
                        e.Handled = true;
                        txtValor.Focus();
                    }

                }
                else
                {
                    txtPrestamo.Clear();
                    txtFechaPres.Clear();
                    txtImportePres.Clear();
                    txtCuotaPres.Clear();
                    e.Handled = true;
                    txtPrestamo.Focus();
                }

            }
        }

        private void txtAnio_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                e.Handled = true;
                txtInicio.Focus();
            }
        }

        private void toolStripForm_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }
    }
}