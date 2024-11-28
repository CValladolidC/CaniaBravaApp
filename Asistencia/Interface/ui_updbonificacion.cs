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
    public partial class ui_updbonificacion : Form
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

        public ui_updbonificacion()
        {
            InitializeComponent();
        }

        private void ui_updbonificacion_Load(object sender, EventArgs e)
        {
            CalPlan calplan = new CalPlan();
            string idperplan = txtCodigoInterno.Text.Trim();
            ui_actualizaComboBox(this._idtipoplan, this._idtipoper, this._idcia, idperplan, this._idtipocal, this._anio, this._messem);

            if (calplan.getDatosCalPlan(this._messem, this._anio, this._idtipoper, this._idcia, this._idtipocal, "ESTADO") == "V")
            {
                btnAceptar.Enabled = true;
                btnGrabar.Enabled = true;
                btnEliminar.Enabled = true;
            }
            else
            {
                btnAceptar.Enabled = false;
                btnGrabar.Enabled = false;
                btnEliminar.Enabled = false;
            }

        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            ((ui_bonificaciones)FormPadre).btnActualizar.PerformClick();
            Close();
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
                filtros.filtrarPerPlan("ui_updbonificacion", this, txtCodigoInterno, idcia, idtipoper, "V", cadenaBusqueda, condicionAdicional);
            }
        }

        private void estadoDetalle(bool estado)
        {
            btnGrabar.Visible = estado;
            btnEliminar.Visible = estado;
        }

        public void ui_newDatosPlanilla()
        {
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
            estadoDetalle(false);
            txtCodigoInterno.Focus();
        }

        public void ui_loadDatosPlanilla(string idperplan, string establecimiento, string riesgo,
            string idestane)
        {

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

            ui_ListaConceptos();
            estadoDetalle(true);
            cmbConcepto.Focus();

        }

        private void ui_actualizaComboBox(string idtipoplan, string idtipoper, string idcia,
            string idperplan, string idtipocal, string anio, string messem)
        {
            Funciones funciones = new Funciones();
            string query;
            query = " select A.idconplan as clave,A.desboleta as descripcion from detconplan  A  left join ";
            query = query + " conplan B on A.idtipoplan=B.idtipoplan ";
            query = query + " and A.idconplan=B.idconplan and A.idcia=B.idcia and A.idtipocal=B.idtipocal ";
            query = query + " where A.idcia='" + @idcia + "' and A.idtipoplan='" + @idtipoplan + "' ";
            query = query + " and A.idtipoper='" + @idtipoper + "' and A.idtipocal='" + @idtipocal + "' ";
            query = query + " and B.tipo='O' and A.destajo='NO' and CONCAT(A.idconplan,A.idtipoplan,A.idcia,A.idtipocal) ";
            query = query + " not in (select CONCAT(idconplan,idtipoplan,idcia,idtipocal) from confijos ";
            query = query + " where idtipoplan='" + @idtipoplan + "' and idtipoper='" + @idtipoper + "' and idtipocal='" + @idtipocal + "' ";
            query = query + " and idcia='" + @idcia + "' and  idperplan='" + @idperplan + "') ";
            query = query + " and A.idconplan not in (select idconplan from condataplan where idperplan='" + @idperplan + "' ";
            query = query + " and idcia='" + @idcia + "' and messem='" + @messem + "' and anio='" + @anio + "' ";
            query = query + " and idtipoper='" + @idtipoper + "' ";
            query = query + " and idtipocal='" + @idtipocal + "' and idtipoplan='" + @idtipoplan + "') order by 1 asc";

            funciones.listaComboBox(query, cmbConcepto, "B");
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
                            string query = "SELECT tasa as clave,'' as descripcion FROM tasaest where codemp='" + @rucemp + "' and idciafile='" + @idcia + "' order by 1 asc";
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
                string emplea = this._empleador.Substring(0, 11);
                string estane = txtEstablecimiento.Text.Substring(0, 4).Trim();
                float riesgo = float.Parse(cmbSCTR.Text);
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


                if (valorValida.Equals("G"))
                {
                    DataPlan dataplan = new DataPlan();
                    dataplan.actualizarDataPlanBonificacion(this._operacion, idperplan, idcia,
                        anio, messem, idtipoper, idtipocal, emplea, estane, riesgo,
                        idtipoplan);
                    estadoDetalle(true);

                    if (this._operacion.Equals("EDITAR"))
                    {
                        MessageBox.Show("Datos guardados exitosamente", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        this._operacion = "EDITAR";
                        cmbConcepto.Focus();
                    }

                }
            }
            catch (FormatException ex)
            {
                MessageBox.Show(ex.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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

            txtValor.Text = "0";
            txtComentario.Text = "";

            if (idconplan != string.Empty)
            {
                string automatico = detconplan.getDetConPlan(idtipoplan, idconplan, idtipoper, idcia, idtipocal, "AUTOMATICO");


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
                lblValor.Visible = false;
                txtValor.Visible = false;
            }

        }

        private void txtValor_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                e.Handled = true;
                txtComentario.Focus();


            }
        }

        private void txtComentario_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                e.Handled = true;
                btnGrabar.Focus();


            }

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
                string idpresper = string.Empty;
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

                if (valorValida == "G")
                {
                    ConDataPlan condataplan = new ConDataPlan();
                    condataplan.actualizarConDataPlan("AGREGAR", idperplan, idcia, anio, messem, idtipoper,
                        idtipocal, idtipoplan, idconplan, tipocalculo, valor, idpresper, comen);
                    ui_ListaConceptos();
                    ui_actualizaComboBox(idtipoplan, idtipoper, idcia, idperplan, idtipocal, anio, messem);
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

                string query = " select A.idconplan,E.desboleta,A.valor,A.tipocalculo,A.comen ";
                query = query + " from condataplan A left join detconplan E on  ";
                query = query + " A.idconplan=E.idconplan and A.idtipoplan=E.idtipoplan ";
                query = query + " and A.idtipoper=E.idtipoper and A.idcia=E.idcia and ";
                query = query + " A.idtipocal=E.idtipocal ";
                query = query + " where  A.idcia='" + @idcia + "' and A.idperplan='" + @idperplan + "' ";
                query = query + " and A.anio='" + @anio + "' and A.messem='" + @messem + "' ";
                query = query + " and A.idtipocal='" + @idtipocal + "' and A.idtipoper='" + @idtipoper + "' ";
                query = query + " and A.idtipoplan='" + @idtipoplan + "' ;";

                using (SqlDataAdapter myDataAdapter = new SqlDataAdapter(query, conexion))
                {
                    DataSet myDataSet = new DataSet();
                    myDataAdapter.Fill(myDataSet, "tblConDataPlan");
                    funciones.formatearDataGridView(dgvdetalle);

                    dgvdetalle.DataSource = myDataSet.Tables["tblConDataPlan"];
                    dgvdetalle.Columns[0].HeaderText = "Concepto";
                    dgvdetalle.Columns[1].HeaderText = "Des.Boleta";
                    dgvdetalle.Columns[2].HeaderText = "Valor";
                    dgvdetalle.Columns[4].HeaderText = "Glosa";

                    dgvdetalle.Columns["tipocalculo"].Visible = false;
                    dgvdetalle.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    dgvdetalle.Columns[2].DefaultCellStyle.Format = "###,###.##";


                    dgvdetalle.Columns[0].Width = 60;
                    dgvdetalle.Columns[1].Width = 250;
                    dgvdetalle.Columns[2].Width = 70;
                    dgvdetalle.Columns[4].Width = 200;


                }
                conexion.Close();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }

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
    }
}