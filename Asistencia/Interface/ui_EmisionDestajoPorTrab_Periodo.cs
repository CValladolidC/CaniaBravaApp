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
    public partial class ui_EmisionDestajoPorTrab_Periodo : Form
    {
        private MaskedTextBox MaskedTextBoxActivo;

        public MaskedTextBox _MaskedTextBoxActivo
        {
            get { return MaskedTextBoxActivo; }
            set { MaskedTextBoxActivo = value; }
        }

        private TextBox TextBoxActivo;

        public TextBox _TextBoxActivo
        {
            get { return TextBoxActivo; }
            set { TextBoxActivo = value; }
        }

        public ui_EmisionDestajoPorTrab_Periodo()
        {
            InitializeComponent();
        }

        private void ui_EmisionDestajoPorTrab_Periodo_Load(object sender, EventArgs e)
        {
            cmbTipoRegistro.Text = "R   PERSONAL DE RETENCIONES";

        }

        private void cmbTipoRegistro_SelectedIndexChanged(object sender, EventArgs e)
        {
            Funciones fn = new Funciones();

            if (fn.getValorComboBox(cmbTipoRegistro, 1).Equals("P"))
            {
                GlobalVariables gv = new GlobalVariables();
                string idcia = gv.getValorCia();
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

        }

        private void ui_listaDetalle()
        {
            Funciones funciones = new Funciones();
            GlobalVariables globlaVariables = new GlobalVariables();
            string idcia = globlaVariables.getValorCia();
            string periodo = txtMesSem.Text.Trim() + funciones.replicateCadena(" ", 6);
            string messem = periodo.Substring(0, 2);
            string anio = periodo.Substring(3, 4);
            string idtipocal = "D";
            string idtipoper = "O";
            string tiporegistro = funciones.getValorComboBox(cmbTipoRegistro, 1);
            string idperplan = txtCodigoInterno.Text.Trim();
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


            string query = " select A.fecha,F.desproddes,E.deszontra,A.cantidad,A.precio,A.total from " + @nombreTablaRet + " A ";
            query = query + " left join zontra E on A.idzontra=E.idzontra and A.idcia=E.idcia  ";
            query = query + " left join proddes F on A.idproddes=F.idproddes and A.idcia=F.idcia ";
            query = query + " where A.idcia='" + @idcia + "' and A.idtipocal='" + @idtipocal + "' and A.idtipoper='" + @idtipoper + "' ";
            query = query + " and A.messem='" + @messem + "' and A.anio='" + @anio + "' and A.idperplan='" + @idperplan + "' and A.idtipoplan='" + @idtipoplan + "' ";
            query = query + " order by A.fecha asc;";


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
                    dgvdetalle.Columns[0].HeaderText = "Fecha";
                    dgvdetalle.Columns[1].HeaderText = "Producto";
                    dgvdetalle.Columns[2].HeaderText = "Zona";
                    dgvdetalle.Columns[3].HeaderText = "Cantidad Procesada";
                    dgvdetalle.Columns[4].HeaderText = "Precio por Unidad";
                    dgvdetalle.Columns[5].HeaderText = "Importe Total MN";

                    dgvdetalle.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    dgvdetalle.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    dgvdetalle.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;


                    dgvdetalle.Columns[3].DefaultCellStyle.Format = "###,###.##";
                    dgvdetalle.Columns[4].DefaultCellStyle.Format = "###,###.##";
                    dgvdetalle.Columns[5].DefaultCellStyle.Format = "###,###.##";


                    dgvdetalle.Columns[0].Width = 75;
                    dgvdetalle.Columns[1].Width = 150;
                    dgvdetalle.Columns[2].Width = 150;
                    dgvdetalle.Columns[3].Width = 70;
                    dgvdetalle.Columns[4].Width = 70;
                    dgvdetalle.Columns[5].Width = 70;

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


                float cantidad = float.Parse(funciones.sumaColumnaDataGridView(dgvdetalle, 3));
                float importe = float.Parse(funciones.sumaColumnaDataGridView(dgvdetalle, 5));
                txtCantidad.Text = Convert.ToString(cantidad);
                txtImporte.Text = Convert.ToString(importe);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }


            conexion.Close();
        }

        private void txtCodigoInterno_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                if (txtCodigoInterno.Text.Trim() != string.Empty)
                {
                    GlobalVariables globalVariables = new GlobalVariables();
                    Funciones funciones = new Funciones();
                    string idcia = globalVariables.getValorCia();
                    string idperplan = txtCodigoInterno.Text.Trim();
                    string tiporegistro = funciones.getValorComboBox(cmbTipoRegistro, 1);

                    if (tiporegistro.Equals("P"))
                    {
                        PerPlan perplan = new PerPlan();
                        string codigoInterno = perplan.ui_getDatosPerPlan(idcia, idperplan, "0");
                        if (codigoInterno == string.Empty)
                        {
                            MessageBox.Show("Código no registrado del trabajador", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            txtCodigoInterno.Clear();
                            txtDocIdent.Clear();
                            txtNroDocIden.Clear();
                            txtNombres.Clear();
                            txtCodigoInterno.Focus();

                        }
                        else
                        {
                            txtCodigoInterno.Text = perplan.ui_getDatosPerPlan(idcia, idperplan, "0");
                            txtDocIdent.Text = perplan.ui_getDatosPerPlan(idcia, idperplan, "1");
                            txtNroDocIden.Text = perplan.ui_getDatosPerPlan(idcia, idperplan, "2");
                            txtNombres.Text = perplan.ui_getDatosPerPlan(idcia, idperplan, "3");
                            e.Handled = true;

                        }
                    }
                    else
                    {
                        PerRet perret = new PerRet();
                        string codigoInterno = perret.ui_getDatosPerRet(idcia, idperplan, "0");
                        if (codigoInterno == string.Empty)
                        {
                            MessageBox.Show("Código no registrado del trabajador", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            txtCodigoInterno.Clear();
                            txtDocIdent.Clear();
                            txtNroDocIden.Clear();
                            txtNombres.Clear();
                            txtCodigoInterno.Focus();

                        }
                        else
                        {
                            txtCodigoInterno.Text = perret.ui_getDatosPerRet(idcia, idperplan, "0");
                            txtDocIdent.Text = perret.ui_getDatosPerRet(idcia, idperplan, "1");
                            txtNroDocIden.Text = perret.ui_getDatosPerRet(idcia, idperplan, "2");
                            txtNombres.Text = perret.ui_getDatosPerRet(idcia, idperplan, "3");
                            e.Handled = true;

                        }
                    }

                }
                else
                {
                    txtCodigoInterno.Clear();
                    txtDocIdent.Clear();
                    txtNroDocIden.Clear();
                    txtNombres.Clear();
                    txtCodigoInterno.Focus();
                }
                ui_listaDetalle();



            }
        }

        private void txtCodigoInterno_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                GlobalVariables globalVariables = new GlobalVariables();
                Funciones funciones = new Funciones();
                string idcia = globalVariables.getValorCia();
                string idperplan = txtCodigoInterno.Text.Trim();
                string tiporegistro = funciones.getValorComboBox(cmbTipoRegistro, 1);
                string idtipoper = "O";
                string idtipoplan;
                if (tiporegistro.Equals("P"))
                {
                    idtipoplan = funciones.getValorComboBox(cmbTipoPlan, 3);
                }
                else
                {
                    idtipoplan = "";
                }


                if (tiporegistro.Equals("P"))
                {
                    string cadenaBusqueda = string.Empty;
                    this._TextBoxActivo = txtCodigoInterno;
                    string condicionAdicional = " and A.idtipoplan='" + @idtipoplan + "'  ";
                    FiltrosMaestros filtros = new FiltrosMaestros();
                    filtros.filtrarPerPlan("ui_EmisionDestajoPorTrab_Periodo", this, txtCodigoInterno, idcia, idtipoper, "V", cadenaBusqueda, condicionAdicional);
                }
                else
                {
                    string clasepadre = "ui_EmisionDestajoPorTrab_Periodo";
                    string cadenaBusqueda = string.Empty;
                    this._TextBoxActivo = txtCodigoInterno;
                    ui_buscarperret ui_buscarperret = new ui_buscarperret();
                    ui_buscarperret._FormPadre = this;
                    ui_buscarperret.setValores(idcia, cadenaBusqueda, clasepadre);
                    ui_buscarperret.Activate();
                    ui_buscarperret.BringToFront();
                    ui_buscarperret.ShowDialog();
                    ui_buscarperret.Dispose();
                }
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtMesSem_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                Funciones funciones = new Funciones();
                CalPlan calplan = new CalPlan();
                GlobalVariables variables = new GlobalVariables();
                string idcia = variables.getValorCia();
                string idtipoper = "O";
                string periodo = txtMesSem.Text.Trim() + funciones.replicateCadena(" ", 6);
                string messem = periodo.Substring(0, 2);
                string anio = periodo.Substring(3, 4);
                string idtipocal = "D";
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

            }
        }
    }
}