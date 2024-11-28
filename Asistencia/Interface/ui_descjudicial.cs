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
    public partial class ui_descjudicial : Form
    {
        private Form FormPadre;

        public Form _FormPadre
        {
            get { return FormPadre; }
            set { FormPadre = value; }
        }

        private TextBox TextBoxActivo;

        public TextBox _TextBoxActivo
        {
            get { return TextBoxActivo; }
            set { TextBoxActivo = value; }
        }

        public ui_descjudicial()
        {
            InitializeComponent();
        }

        private void ui_descjudicial_Load(object sender, EventArgs e)
        {
            ui_ActualizaComboBox();

        }

        public void ui_ActualizaComboBox()
        {

            MaesGen maesgen = new MaesGen();
            maesgen.listaDetMaesGen("024", cmbMotivoSen, "B");
            maesgen.listaDetMaesGen("013", cmbPago, "B");
            maesgen.listaDetMaesGen("007", cmbEntFinRem, "B");
            maesgen.listaDetMaesGen("014", cmbTipoCtaRem, "B");
            maesgen.listaDetMaesGen("015", cmbMonRem, "B");
            maesgen.listaDetMaesGen("025", cmbVigencia, "B");
        }

        internal void ui_newDescuentoJudicial()
        {
            GlobalVariables variables = new GlobalVariables();
            DescJud desjud = new DescJud();
            txtOperacion.Text = "AGREGAR";
            txtValid.Text = "Q";
            txtCodigoDescuento.Text = desjud.generaCodigo(variables.getValorCia());
            txtCodigoInterno.Clear();
            txtDocIdent.Clear();
            txtNroDocIden.Clear();
            txtNombres.Clear();
            txtFecIniPerLab.Clear();
            txtFecEmiSen.Clear();
            txtFecRecSen.Clear();
            txtNroDocSen.Clear();
            txtDirASen.Clear();
            txtFecIniDemSen.Clear();
            txtNroDocIniProSen.Clear();
            txtDemandanteSen.Clear();
            cmbMotivoSen.Text = string.Empty;
            radioButtonImp.Checked = false;
            radioButtonPor.Checked = true;
            txtImporte.Clear();
            txtImporte.Enabled = false;
            txtPorcentaje.Clear();
            txtPorcentaje.Enabled = true;
            cmbDescuento.Text = string.Empty;
            cmbPago.Text = string.Empty;
            txtJuezSen.Clear();
            txtFecIniDescuento.Clear();
            cmbVigencia.Text = string.Empty;
            txtFecFinDescuento.Clear();
            txtFecFinDescuento.Enabled = false;
            ui_habilitarCuentaDeposito(false);
            tabControlDes.SelectTab(0);
            txtCodigoInterno.Focus();


        }

        internal void ui_loadDescuentoJudicial(string iddesjud, string idcia)
        {

            string squery;
            PerPlan perplan = new PerPlan();
            MaesGen maesgen = new MaesGen();
            txtOperacion.Text = "EDITAR";
            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();
            squery = "SELECT * FROM desjud where (iddesjud='" + @iddesjud + "' and idcia='" + @idcia + "');";

            try
            {
                SqlCommand myCommand = new SqlCommand(squery, conexion);
                SqlDataReader myReader = myCommand.ExecuteReader();

                if (myReader.Read())
                {

                    string condicionAdicional = string.Empty;
                    txtCodigoDescuento.Text = myReader["iddesjud"].ToString();
                    txtCodigoInterno.Text = myReader["idperplan"].ToString();
                    txtDocIdent.Text = perplan.ui_getDatosPerPlan(myReader["idcia"].ToString(), myReader["idperplan"].ToString(), "1");
                    txtNroDocIden.Text = perplan.ui_getDatosPerPlan(myReader["idcia"].ToString(), myReader["idperplan"].ToString(), "2");
                    txtNombres.Text = perplan.ui_getDatosPerPlan(myReader["idcia"].ToString(), myReader["idperplan"].ToString(), "3");
                    txtFecIniPerLab.Text = perplan.ui_getDatosPerPlan(myReader["idcia"].ToString(), myReader["idperplan"].ToString(), "4");
                    txtFecEmiSen.Text = myReader["fecemi"].ToString();
                    txtFecRecSen.Text = myReader["fecrec"].ToString();
                    txtNroDocSen.Text = myReader["nrodocaut"].ToString();
                    txtDirASen.Text = myReader["dirigido_a"].ToString();
                    txtFecIniDemSen.Text = myReader["fecinidem"].ToString();
                    txtNroDocIniProSen.Text = myReader["nrodocini"].ToString();
                    txtDemandanteSen.Text = myReader["demanda"].ToString();
                    maesgen.consultaDetMaesGen("024", myReader["motivo"].ToString(), cmbMotivoSen);
                    if (myReader["isimp"].Equals("0"))
                    {
                        radioButtonImp.Checked = false;
                        txtImporte.Enabled = false;
                    }
                    else
                    {
                        radioButtonImp.Checked = true;
                        txtImporte.Enabled = true;
                    }
                    if (myReader["ispor"].Equals("0"))
                    {
                        radioButtonPor.Checked = false;
                        txtPorcentaje.Enabled = false;
                    }
                    else
                    {
                        radioButtonPor.Checked = true;
                        txtPorcentaje.Enabled = true;
                    }
                    txtImporte.Text = myReader["importe"].ToString();
                    txtPorcentaje.Text = myReader["porcentaje"].ToString();
                    if (myReader["tipdesc"].Equals("N"))
                    {
                        cmbDescuento.Text = "N        IMPORTE NETO";
                    }
                    else
                    {
                        cmbDescuento.Text = "B         IMPORTE BRUTO";
                    }
                    maesgen.consultaDetMaesGen("013", myReader["tippago"].ToString(), cmbPago);

                    if (myReader["tippago"].Equals("2"))
                    {
                        ui_habilitarCuentaDeposito(true);
                    }
                    else
                    {
                        ui_habilitarCuentaDeposito(false);
                    }

                    maesgen.consultaDetMaesGen("007", myReader["entfin"].ToString(), cmbEntFinRem);
                    maesgen.consultaDetMaesGen("014", myReader["tipcta"].ToString(), cmbTipoCtaRem);
                    txtNumCtaRem.Text = myReader["nrocta"].ToString();
                    maesgen.consultaDetMaesGen("015", myReader["moncta"].ToString(), cmbMonRem);
                    txtJuezSen.Text = myReader["nomjuez"].ToString();
                    txtFecIniDescuento.Text = myReader["feciniorden"].ToString();
                    maesgen.consultaDetMaesGen("025", myReader["vigencia"].ToString(), cmbVigencia);


                    if (myReader["vigencia"].Equals("2"))
                    {
                        txtFecFinDescuento.Text = myReader["fecfinorden"].ToString();
                        txtFecFinDescuento.Enabled = true;
                    }
                    else
                    {
                        txtFecFinDescuento.Clear();
                        txtFecFinDescuento.Enabled = false;
                    }


                }
                tabControlDes.SelectTab(0);
                txtCodigoInterno.Focus();
                myReader.Close();
                myCommand.Dispose();

            }

            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }

            conexion.Close();
        }

        public void ui_habilitarCuentaDeposito(bool estado)
        {
            cmbEntFinRem.Text = string.Empty;
            cmbMonRem.Text = string.Empty;
            txtNumCtaRem.Text = string.Empty;
            cmbTipoCtaRem.Text = string.Empty;

            cmbEntFinRem.Enabled = estado;
            cmbMonRem.Enabled = estado;
            txtNumCtaRem.Enabled = estado;
            cmbTipoCtaRem.Enabled = estado;
        }

        private void txtCodigo_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.F2)
            {
                GlobalVariables variables = new GlobalVariables();
                string idcia = variables.getValorCia();
                string idtipoper = "";
                string cadenaBusqueda = string.Empty;
                FiltrosMaestros filtros = new FiltrosMaestros();
                this._TextBoxActivo = txtCodigoInterno;
                filtros.filtrarPerPlan("ui_descjudicial", this, txtCodigoInterno, idcia, idtipoper, "V", cadenaBusqueda, "");
            }
        }

        private void txtCodigo_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void txtCodigoInterno_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                if (txtCodigoInterno.Text.Trim() != string.Empty)
                {
                    GlobalVariables variables = new GlobalVariables();
                    string idcia = variables.getValorCia();
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
                        txtCodigoInterno.Focus();
                    }
                    else
                    {
                        txtCodigoInterno.Text = perplan.ui_getDatosPerPlan(idcia, idperplan, "0");
                        txtDocIdent.Text = perplan.ui_getDatosPerPlan(idcia, idperplan, "1");
                        txtNroDocIden.Text = perplan.ui_getDatosPerPlan(idcia, idperplan, "2");
                        txtNombres.Text = perplan.ui_getDatosPerPlan(idcia, idperplan, "3");
                        txtFecIniPerLab.Text = perplan.ui_getDatosPerPlan(idcia, idperplan, "4");
                        e.Handled = true;
                        txtFecEmiSen.Focus();
                    }

                }
                else
                {

                    txtCodigoInterno.Clear();
                    txtDocIdent.Clear();
                    txtNroDocIden.Clear();
                    txtNombres.Clear();
                    txtFecIniPerLab.Clear();
                    txtCodigoInterno.Focus();
                }



            }
        }

        private void txtFecEmiSen_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                if (UtileriasFechas.IsDate(txtFecEmiSen.Text))
                {
                    e.Handled = true;
                    txtFecRecSen.Focus();

                }
                else
                {
                    MessageBox.Show("Fecha de Emisión de Sentencia no válida", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    e.Handled = true;
                    txtFecEmiSen.Focus();
                }
            }
        }

        private void txtNroDocSen_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {

                e.Handled = true;
                txtDirASen.Focus();



            }
        }

        private void txtDirASen_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {

                e.Handled = true;
                txtFecIniDemSen.Focus();




            }
        }

        private void txtFecIniDemSen_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                if (UtileriasFechas.IsDate(txtFecIniDemSen.Text))
                {
                    e.Handled = true;
                    txtNroDocIniProSen.Focus();

                }
                else
                {
                    MessageBox.Show("Fecha de Inicio de Demanda no válida", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    e.Handled = true;
                    txtFecIniDemSen.Focus();
                }
            }
        }

        private void txtNroDocIniProSen_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {

                e.Handled = true;
                txtDemandanteSen.Focus();


            }
        }

        private void txtDemandanteSen_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {

                e.Handled = true;
                cmbMotivoSen.Focus();

            }
        }

        private void txtFecRecSen_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                if (UtileriasFechas.IsDate(txtFecRecSen.Text))
                {
                    e.Handled = true;
                    txtNroDocSen.Focus();

                }
                else
                {
                    MessageBox.Show("Fecha de Recepción de Sentencia no válida", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    e.Handled = true;
                    txtFecRecSen.Focus();
                }
            }
        }

        private void cmbMotivoSen_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                if (cmbMotivoSen.Text != String.Empty)
                {
                    MaesGen maesgen = new MaesGen();
                    maesgen.validarDetMaesGen("024", cmbMotivoSen, cmbMotivoSen.Text);
                }
                e.Handled = true;
                txtJuezSen.Focus();

            }
        }

        private void radioButtonPor_CheckedChanged(object sender, EventArgs e)
        {
            txtImporte.Clear();
            txtPorcentaje.Clear();
            txtImporte.Enabled = false;
            txtPorcentaje.Enabled = true;
            txtPorcentaje.Focus();
        }

        private void radioButtonImp_CheckedChanged(object sender, EventArgs e)
        {
            txtImporte.Clear();
            txtPorcentaje.Clear();
            txtImporte.Enabled = true;
            txtPorcentaje.Enabled = false;
            txtImporte.Focus();
        }

        private void cmbPago_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                if (cmbPago.Text != String.Empty)
                {
                    MaesGen maesgen = new MaesGen();
                    maesgen.validarDetMaesGen("013", cmbPago, cmbPago.Text);
                    Funciones funciones = new Funciones();
                    string clave = funciones.getValorComboBox(cmbPago, 4);
                    if (clave.Equals("2"))
                    {
                        ui_habilitarCuentaDeposito(true);
                        e.Handled = true;
                        cmbEntFinRem.Focus();
                    }
                    else
                    {
                        ui_habilitarCuentaDeposito(false);
                        e.Handled = true;
                        txtFecIniDescuento.Focus();
                    }

                }

            }
        }

        private void cmbEntFinRem_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                if (cmbEntFinRem.Text != String.Empty)
                {
                    MaesGen maesgen = new MaesGen();
                    maesgen.validarDetMaesGen("007", cmbEntFinRem, cmbEntFinRem.Text);
                }
                e.Handled = true;
                txtNumCtaRem.Focus();

            }
        }

        private void txtNumCtaRem_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                e.Handled = true;
                cmbMonRem.Focus();
            }
        }

        private void cmbMonRem_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                if (cmbMonRem.Text != String.Empty)
                {
                    MaesGen maesgen = new MaesGen();
                    maesgen.validarDetMaesGen("015", cmbMonRem, cmbMonRem.Text);
                }
                e.Handled = true;
                cmbTipoCtaRem.Focus();

            }
        }

        private void cmbTipoCtaRem_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                if (cmbTipoCtaRem.Text != String.Empty)
                {
                    MaesGen maesgen = new MaesGen();
                    maesgen.validarDetMaesGen("014", cmbTipoCtaRem, cmbTipoCtaRem.Text);
                }
                e.Handled = true;
                txtFecIniDescuento.Focus();

            }
        }

        private void cmbVigencia_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                if (cmbVigencia.Text != String.Empty)
                {
                    Funciones funciones = new Funciones();
                    MaesGen maesgen = new MaesGen();
                    maesgen.validarDetMaesGen("025", cmbVigencia, cmbVigencia.Text);

                    if (funciones.getValorComboBox(cmbVigencia, 4).Equals("2"))
                    {
                        txtFecFinDescuento.Enabled = true;
                        e.Handled = true;
                        txtFecFinDescuento.Focus();
                    }
                    else
                    {
                        txtFecFinDescuento.Clear();
                        txtFecFinDescuento.Enabled = false;
                    }
                }


            }
        }

        public void ui_validarDatos()
        {
            MaesGen maesgen = new MaesGen();
            Funciones funciones = new Funciones();
            GlobalVariables globalVariables = new GlobalVariables();

            string valorValida = "G";

            if (txtCodigoInterno.Text == string.Empty && valorValida == "G")
            {
                valorValida = "B";
                MessageBox.Show("No ha ingresado trabajador", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                tabControlDes.SelectTab(0);
                txtCodigoInterno.Focus();
            }


            if (UtileriasFechas.IsDate(txtFecEmiSen.Text) == false && valorValida == "G")
            {
                valorValida = "B";
                MessageBox.Show("Fecha de Emisión de la Orden para efectuar el descuento no válida", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);

                tabControlDes.SelectTab(0);
                txtFecEmiSen.Focus();
            }

            if (UtileriasFechas.IsDate(txtFecRecSen.Text) == false && valorValida == "G")
            {
                valorValida = "B";
                MessageBox.Show("Fecha de Recepción de la Orden para efectuar el descuento no válida", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);

                tabControlDes.SelectTab(0);
                txtFecRecSen.Focus();
            }

            if (txtNroDocSen.Text == string.Empty && valorValida == "G")
            {
                valorValida = "B";
                MessageBox.Show("No ha ingresado Número de Documento que autoriza el descuento", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                tabControlDes.SelectTab(0);
                txtNroDocSen.Focus();
            }

            if (txtDirASen.Text == string.Empty && valorValida == "G")
            {
                valorValida = "B";
                MessageBox.Show("No ha ingresado el Responsable de hacer que el descuento se haga efectivo", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                tabControlDes.SelectTab(0);
                txtDirASen.Focus();
            }

            if (UtileriasFechas.IsDate(txtFecIniDemSen.Text) == false && valorValida == "G")
            {
                valorValida = "B";
                MessageBox.Show("Fecha en la que se inició el proceso judicial no válida", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                tabControlDes.SelectTab(0);
                txtFecIniDemSen.Focus();
            }


            if (txtNroDocIniProSen.Text == string.Empty && valorValida == "G")
            {
                valorValida = "B";
                MessageBox.Show("No ha ingresado Número de Documento que dió inicio al proceso judicial", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                tabControlDes.SelectTab(0);
                txtNroDocIniProSen.Focus();
            }

            if (txtDemandanteSen.Text == string.Empty && valorValida == "G")
            {
                valorValida = "B";
                MessageBox.Show("No ha ingresado Demandante", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                tabControlDes.SelectTab(0);
                txtDemandanteSen.Focus();
            }


            if (cmbMotivoSen.Text == string.Empty && valorValida == "G")
            {
                valorValida = "B";
                MessageBox.Show("No ha seleccionado Motivo de Sentencia", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                tabControlDes.SelectTab(0);
                cmbMotivoSen.Focus();
            }

            if (cmbMotivoSen.Text != string.Empty && valorValida == "G")
            {
                string resultado = maesgen.verificaComboBoxMaesGen("024", cmbMotivoSen.Text.Trim());
                if (resultado.Trim() == string.Empty)
                {
                    valorValida = "B";
                    MessageBox.Show("Dato incorrecto en Motivo de Sentencia", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    tabControlDes.SelectTab(0);
                    cmbMotivoSen.Focus();
                }
            }

            if (txtJuezSen.Text == string.Empty && valorValida == "G")
            {
                valorValida = "B";
                MessageBox.Show("No ha ingresado Juez Titular", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                tabControlDes.SelectTab(0);
                txtJuezSen.Focus();
            }

            if (radioButtonImp.Checked && txtImporte.Text == string.Empty && valorValida == "G")
            {
                valorValida = "B";
                MessageBox.Show("No ha ingresado Importe a Descontar", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                tabControlDes.SelectTab(1);
                txtImporte.Focus();
            }

            if (radioButtonPor.Checked && txtPorcentaje.Text == string.Empty && valorValida == "G")
            {
                valorValida = "B";
                MessageBox.Show("No ha ingresado Porcentaje a Descontar", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                tabControlDes.SelectTab(1);
                txtPorcentaje.Focus();
            }

            if (cmbDescuento.Text == string.Empty && valorValida == "G")
            {
                valorValida = "B";
                MessageBox.Show("No ha seleccionado la base para aplicar el descuento", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                tabControlDes.SelectTab(1);
                cmbDescuento.Focus();
            }

            if (cmbDescuento.Text != string.Empty && valorValida == "G")
            {
                string resultado = funciones.getValorComboBox(cmbDescuento, 1);
                if (resultado.Trim() == string.Empty)
                {
                    valorValida = "B";
                    MessageBox.Show("Dato incorrecto en la base para aplicar el descuento", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    tabControlDes.SelectTab(1);
                    cmbDescuento.Focus();
                }
            }

            if (cmbPago.Text == string.Empty && valorValida == "G")
            {
                valorValida = "B";
                MessageBox.Show("No ha seleccionado Forma de Pago", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                tabControlDes.SelectTab(1);
                cmbPago.Focus();
            }

            if (cmbPago.Text != string.Empty && valorValida == "G")
            {
                string resultado = maesgen.verificaComboBoxMaesGen("013", cmbPago.Text.Trim());
                if (resultado.Trim() == string.Empty)
                {
                    valorValida = "B";
                    MessageBox.Show("Dato incorrecto en Forma de Pago", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    tabControlDes.SelectTab(1);
                    cmbPago.Focus();
                }
            }

            if (funciones.getValorComboBox(cmbPago, 4).Equals("2") && valorValida == "G")
            {

                if (cmbEntFinRem.Text != string.Empty && valorValida == "G")
                {
                    string resultado = maesgen.verificaComboBoxMaesGen("007", cmbEntFinRem.Text.Trim());
                    if (resultado.Trim() == string.Empty)
                    {
                        valorValida = "B";
                        MessageBox.Show("Dato incorrecto en Entidad Financiera", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        tabControlDes.SelectTab(1);
                        cmbEntFinRem.Focus();
                    }
                }

                if (txtNumCtaRem.Text == string.Empty && valorValida == "G")
                {
                    valorValida = "B";
                    MessageBox.Show("No ha ingresado Nro. de Cuenta", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    tabControlDes.SelectTab(1);
                    txtNumCtaRem.Focus();
                }

                if (cmbMonRem.Text != string.Empty && valorValida == "G")
                {
                    string resultado = maesgen.verificaComboBoxMaesGen("015", cmbMonRem.Text.Trim());
                    if (resultado.Trim() == string.Empty)
                    {
                        valorValida = "B";
                        MessageBox.Show("Dato incorrecto en Moneda", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        tabControlDes.SelectTab(1);
                        cmbMonRem.Focus();
                    }
                }

                if (cmbTipoCtaRem.Text != string.Empty && valorValida == "G")
                {
                    string resultado = maesgen.verificaComboBoxMaesGen("014", cmbTipoCtaRem.Text.Trim());
                    if (resultado.Trim() == string.Empty)
                    {
                        valorValida = "B";
                        MessageBox.Show("Dato incorrecto en Tipo de Cuenta", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        tabControlDes.SelectTab(3);
                        cmbTipoCtaRem.Focus();
                    }
                }

            }

            if (UtileriasFechas.IsDate(txtFecIniDescuento.Text) == false && valorValida == "G")
            {
                valorValida = "B";
                MessageBox.Show("Fecha de Inicio de aplicación del descuento no válida", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                tabControlDes.SelectTab(1);
                txtFecIniDescuento.Focus();
            }


            if (cmbVigencia.Text == string.Empty && valorValida == "G")
            {
                valorValida = "B";
                MessageBox.Show("No ha seleccionado Vigencia del Descuento", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                tabControlDes.SelectTab(1);
                cmbVigencia.Focus();
            }

            if (cmbVigencia.Text != string.Empty && valorValida == "G")
            {
                string resultado = maesgen.verificaComboBoxMaesGen("025", cmbVigencia.Text.Trim());
                if (resultado.Trim() == string.Empty)
                {
                    valorValida = "B";
                    MessageBox.Show("Dato incorrecto en Vigencia del Descuento", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    tabControlDes.SelectTab(1);
                    cmbVigencia.Focus();
                }
            }

            if (funciones.getValorComboBox(cmbVigencia, 4).Equals("2") && valorValida == "G")
            {
                if (UtileriasFechas.IsDate(txtFecFinDescuento.Text) == false)
                {
                    valorValida = "B";
                    MessageBox.Show("Fecha de Término de aplicación del descuento no válida", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    tabControlDes.SelectTab(1);
                    txtFecFinDescuento.Focus();
                }

            }

            if (valorValida.Equals("G")) //GOOD 
            {
                txtValid.Text = "G";
                MessageBox.Show("Registro validado exitosamente", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                if (valorValida.Equals("B")) //BAD
                {
                    txtValid.Text = "B";

                }
                else
                {
                    txtValid.Text = "Q"; //QUESTION
                }
            }
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            try
            {
                ui_validarDatos();
                if (txtValid.Text.Equals("G"))
                {
                    Funciones funciones = new Funciones();
                    GlobalVariables variables = new GlobalVariables();
                    string operacion = txtOperacion.Text.Trim();
                    string iddesjud = txtCodigoDescuento.Text.Trim();
                    string idperplan = txtCodigoInterno.Text.Trim();
                    string fecemi = txtFecEmiSen.Text.Trim();
                    string fecrec = txtFecRecSen.Text.Trim();
                    string nrodocaut = txtNroDocSen.Text.Trim();
                    string dirigido_a = txtDirASen.Text.Trim();
                    string fecinidem = txtFecIniDemSen.Text.Trim();
                    string nrodocini = txtNroDocIniProSen.Text.Trim();
                    string demanda = txtDemandanteSen.Text.Trim();
                    string motivo = funciones.getValorComboBox(cmbMotivoSen, 4);
                    string ispor = "0";
                    string isimp = "0";
                    float importe = float.Parse("0");
                    float porcentaje = float.Parse("0");
                    if (radioButtonImp.Checked)
                    {
                        ispor = "0";
                        isimp = "1";
                        importe = float.Parse(txtImporte.Text);
                    }
                    else
                    {
                        if (radioButtonPor.Checked)
                        {
                            ispor = "1";
                            isimp = "0";
                            porcentaje = float.Parse(txtPorcentaje.Text);
                        }
                    }


                    string tipdesc = funciones.getValorComboBox(cmbDescuento, 1);
                    string tippago = funciones.getValorComboBox(cmbPago, 1);
                    string entfin = funciones.getValorComboBox(cmbEntFinRem, 4);
                    string nrocta = txtNumCtaRem.Text.Trim();
                    string moncta = funciones.getValorComboBox(cmbMonRem, 4);
                    string tipcta = funciones.getValorComboBox(cmbTipoCtaRem, 4);
                    string nomjuez = txtJuezSen.Text.Trim();
                    string feciniorden = txtFecIniDescuento.Text.Trim();
                    string vigencia = funciones.getValorComboBox(cmbVigencia, 4);
                    string fecfinorden = txtFecFinDescuento.Text.Trim();
                    string idcia = variables.getValorCia();
                    DescJud desjud = new DescJud();
                    desjud.actualizaDesJud(operacion, iddesjud, idcia, idperplan, fecemi, fecrec, nrodocaut, dirigido_a, fecinidem,
                    nrodocini, demanda, motivo, ispor, isimp, porcentaje, importe, tipdesc, tippago, entfin, nrocta, moncta, tipcta,
                    nomjuez, feciniorden, vigencia, fecfinorden);

                    ((ui_DesJud)FormPadre).btnActualizar.PerformClick();
                    this.Close();
                }
            }
            catch (FormatException ex)
            {
                MessageBox.Show(ex.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void cmbDescuento_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                if (cmbDescuento.Text == String.Empty)
                {
                    MessageBox.Show("El campo no acepta valores vacíos", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    e.Handled = true;
                    cmbDescuento.Focus();
                }
                else
                {
                    switch (cmbDescuento.Text.ToUpper().Substring(0, 1))
                    {
                        case "B":
                            cmbDescuento.Text = "B         IMPORTE BRUTO";
                            break;
                        case "N":
                            cmbDescuento.Text = "N        IMPORTE NETO";
                            break;
                        default:
                            cmbDescuento.Text = "N        IMPORTE NETO";
                            break;
                    }
                    e.Handled = true;
                    cmbPago.Focus();

                }
            }
        }

        private void cmbPago_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbPago.Text != String.Empty)
            {
                MaesGen maesgen = new MaesGen();
                maesgen.validarDetMaesGen("013", cmbPago, cmbPago.Text);
                Funciones funciones = new Funciones();
                string clave = funciones.getValorComboBox(cmbPago, 4);
                if (clave.Equals("2"))
                {
                    ui_habilitarCuentaDeposito(true);
                    cmbEntFinRem.Focus();
                }
                else
                {
                    ui_habilitarCuentaDeposito(false);
                    txtFecIniDescuento.Focus();
                }

            }
        }

        private void cmbVigencia_SelectedIndexChanged(object sender, EventArgs e)
        {
            Funciones funciones = new Funciones();
            if (funciones.getValorComboBox(cmbVigencia, 4).Equals("2"))
            {
                txtFecFinDescuento.Enabled = true;
                txtFecFinDescuento.Focus();
            }
            else
            {
                txtFecFinDescuento.Clear();
                txtFecFinDescuento.Enabled = false;
            }
        }

        private void txtImporte_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                try
                {
                    float numero = float.Parse(txtImporte.Text);
                    e.Handled = true;
                    cmbDescuento.Focus();
                }
                catch (FormatException ex)
                {
                    MessageBox.Show(ex.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtImporte.Clear();
                    txtImporte.Focus();
                }

            }
        }

        private void txtPorcentaje_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                try
                {
                    float numero = float.Parse(txtPorcentaje.Text);
                    if (numero <= 100)
                    {
                        e.Handled = true;
                        cmbDescuento.Focus();
                    }
                    else
                    {
                        MessageBox.Show("El valor no puede ser mayor a 100", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtPorcentaje.Clear();
                        txtPorcentaje.Focus();
                    }
                }
                catch (FormatException ex)
                {
                    MessageBox.Show(ex.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtPorcentaje.Clear();
                    txtPorcentaje.Focus();
                }

            }
        }

        private void txtFecIniDescuento_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                if (UtileriasFechas.IsDate(txtFecIniDescuento.Text))
                {
                    e.Handled = true;
                    cmbVigencia.Focus();

                }
                else
                {
                    MessageBox.Show("Fecha de Inicio de Sentencia no válida", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    e.Handled = true;
                    txtFecIniDescuento.Focus();
                }
            }
        }

        private void txtFecFinDescuento_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                if (UtileriasFechas.IsDate(txtFecFinDescuento.Text))
                {
                    e.Handled = true;
                    txtFecFinDescuento.Focus();

                }
                else
                {
                    MessageBox.Show("Fecha Final de Sentencia no válida", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    e.Handled = true;
                    txtFecFinDescuento.Focus();
                }
            }
        }
    }
}