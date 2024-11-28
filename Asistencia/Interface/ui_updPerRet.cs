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
    public partial class ui_updPerRet : Form
    {
        private Form FormPadre;

        public Form _FormPadre
        {
            get { return FormPadre; }
            set { FormPadre = value; }
        }

        private TextBox TextBoxUbigeo;
        private TextBox TextBoxDscUbigeo;
        private TextBox TextBoxActivo;

        public TextBox _TextBoxActivo
        {
            get { return TextBoxActivo; }
            set { TextBoxActivo = value; }
        }

        public TextBox _TextBoxUbigeo
        {
            get { return TextBoxUbigeo; }
            set { TextBoxUbigeo = value; }
        }

        public TextBox _TextBoxDscUbigeo
        {
            get { return TextBoxDscUbigeo; }
            set { TextBoxDscUbigeo = value; }
        }

        public ui_updPerRet()
        {
            InitializeComponent();
        }

        private void ui_updPerRet_Load(object sender, EventArgs e)
        {
            tabControlPer.SelectTab(0);
        }

        private void tabControlPer_SelectedIndexChanging(object sender, Dotnetrix.Controls.TabPageChangeEventArgs e)
        {

        }
        public void ui_ActualizaComboBox()
        {

            MaesGen maesgen = new MaesGen();
            maesgen.listaDetMaesGen("002", cmbTipoDocumento, "B");
            maesgen.listaDetMaesGen("001", cmbEstadoCivil, "B");
            maesgen.listaDetMaesGen("003", cmbNacionalidad, "B");
            maesgen.listaDetMaesGen("004", cmbCategoriaBrevete, "B");
            maesgen.listaDetMaesGen("017", cmbTipoVia, "B");
            maesgen.listaDetMaesGen("018", cmbTipoZona, "B");
            maesgen.listaDetMaesGen("019", cmbSexo, "B");

        }

        private void cmbTipoDocumento_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                if (cmbTipoDocumento.Text != String.Empty)
                {
                    MaesGen maesgen = new MaesGen();
                    maesgen.validarDetMaesGen("002", cmbTipoDocumento, cmbTipoDocumento.Text);
                }
                e.Handled = true;
                txtNroDoc.Focus();
            }
        }

        private void txtApPat_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                e.Handled = true;
                txtApMat.Focus();
            }
        }

        private void txtApMat_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                e.Handled = true;
                txtNombres.Focus();
            }
        }

        private void txtNombres_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                e.Handled = true;
                cmbTipoDocumento.Focus();
            }
        }

        private void txtNroDoc_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                e.Handled = true;
                txtTelFijo.Focus();
            }
        }

        private void txtTelFijo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                e.Handled = true;
                txtCelular.Focus();
            }
        }

        private void txtCelular_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                e.Handled = true;
                txtRpm.Focus();
            }
        }

        private void txtRpm_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                e.Handled = true;
                cmbNacionalidad.Focus();
            }
        }

        private void cmbNacionalidad_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                if (cmbNacionalidad.Text != String.Empty)
                {
                    MaesGen maesgen = new MaesGen();
                    maesgen.validarDetMaesGen("003", cmbNacionalidad, cmbNacionalidad.Text);
                }
                e.Handled = true;
                txtEmail.Focus();
            }
        }

        private void txtEmail_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                e.Handled = true;
                txtRuc.Focus();
            }
        }

        private void txtFechaNac_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                if (UtileriasFechas.IsDate(txtFechaNac.Text))
                {
                    e.Handled = true;
                    cmbSexo.Focus();
                }
                else
                {
                    MessageBox.Show("Fecha de Nacimiento no válida", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    e.Handled = true;
                    txtFechaNac.Focus();
                }
            }
        }

        private void cmbEstadoCivil_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                if (cmbEstadoCivil.Text != String.Empty)
                {
                    MaesGen maesgen = new MaesGen();
                    maesgen.validarDetMaesGen("001", cmbEstadoCivil, cmbEstadoCivil.Text);
                }
                e.Handled = true;
                cmbCategoriaBrevete.Focus();

            }
        }

        private void cmbCategoriaBrevete_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                if (cmbCategoriaBrevete.Text != String.Empty)
                {
                    MaesGen maesgen = new MaesGen();
                    Funciones funciones = new Funciones();
                    maesgen.validarDetMaesGen("004", cmbCategoriaBrevete, cmbCategoriaBrevete.Text);
                    string clave = funciones.getValorComboBox(cmbCategoriaBrevete, 4);
                    if (clave.Equals("XX"))
                    {
                        txtNroLicenciaConductor.Text = "";
                        txtNroLicenciaConductor.Enabled = false;
                    }
                    else
                    {

                        txtNroLicenciaConductor.Enabled = true;
                        e.Handled = true;
                        txtNroLicenciaConductor.Focus();
                    }
                }


            }
        }

        private void cmbSexo_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void cmbSexo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                if (cmbSexo.Text != String.Empty)
                {
                    MaesGen maesgen = new MaesGen();
                    maesgen.validarDetMaesGen("019", cmbSexo, cmbSexo.Text);
                }
                e.Handled = true;
                cmbEstadoCivil.Focus();

            }
        }

        private void cmbTipoVia_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                if (cmbTipoVia.Text != String.Empty)
                {
                    MaesGen maesgen = new MaesGen();
                    maesgen.validarDetMaesGen("017", cmbTipoVia, cmbTipoVia.Text);
                }
                e.Handled = true;
                txtNombreVia.Focus();

            }
        }

        private void txtNombreVia_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                e.Handled = true;
                txtNumeroVia.Focus();


            }
        }

        private void txtNumeroVia_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                e.Handled = true;
                txtInteriorVia.Focus();


            }
        }

        private void txtInteriorVia_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                e.Handled = true;
                cmbTipoZona.Focus();

            }
        }

        private void cmbTipoZona_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                if (cmbTipoZona.Text != String.Empty)
                {
                    MaesGen maesgen = new MaesGen();
                    maesgen.validarDetMaesGen("018", cmbTipoZona, cmbTipoZona.Text);
                }
                e.Handled = true;
                txtNombreZona.Focus();

            }
        }

        private void txtNombreZona_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                e.Handled = true;
                txtReferenciasZona.Focus();
            }
        }

        private void txtReferenciasZona_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                e.Handled = true;
                btnUbigeo.Focus();

            }
        }

        private void btnUbigeo_Click(object sender, EventArgs e)
        {
            _TextBoxUbigeo = txtCodigoUbigeo;
            _TextBoxDscUbigeo = txtDscUbigeo;
            ui_ubigeo ui_ubigeo = new ui_ubigeo();
            ui_ubigeo._FormPadre = this;
            ui_ubigeo._clasePadre = "ui_updPerRet";
            ui_ubigeo.ui_nuevaSeleccion();

            if (ui_ubigeo.ShowDialog(this) == DialogResult.OK)
            {
                btnUbigeo.Focus();

            }
            else
            {
                btnUbigeo.Focus();
            }
            ui_ubigeo.Dispose();
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            ui_validarDatos();
            if (txtValid.Text.Equals("G"))
            {
                Funciones funciones = new Funciones();
                GlobalVariables variables = new GlobalVariables();
                string operacion = txtOperacion.Text.Trim();
                string idperplan = txtCodigoInterno.Text.Trim();
                string apepat = txtApPat.Text.Trim();
                string apemat = txtApMat.Text.Trim();
                string nombres = txtNombres.Text.Trim();
                string fecnac = txtFechaNac.Text.Trim();
                string tipdoc = funciones.getValorComboBox(cmbTipoDocumento, 4);
                string nrodoc = txtNroDoc.Text.Trim();
                string telfijo = txtTelFijo.Text.Trim();
                string celular = txtCelular.Text.Trim();
                string rpm = txtRpm.Text.Trim();
                string sexo = funciones.getValorComboBox(cmbSexo, 4);
                string estcivil = funciones.getValorComboBox(cmbEstadoCivil, 4);
                string nacion = funciones.getValorComboBox(cmbNacionalidad, 4);
                string email = txtEmail.Text.Trim();
                string catlic = funciones.getValorComboBox(cmbCategoriaBrevete, 4);
                string nrolic = txtNroLicenciaConductor.Text.Trim();
                string tipvia = funciones.getValorComboBox(cmbTipoVia, 4);
                string nomvia = txtNombreVia.Text.Trim();
                string nrovia = txtNumeroVia.Text.Trim();
                string intvia = txtInteriorVia.Text.Trim();
                string tipzona = funciones.getValorComboBox(cmbTipoZona, 4);
                string nomzona = txtNombreZona.Text.Trim();
                string refzona = txtReferenciasZona.Text.Trim();
                string ubigeo = txtCodigoUbigeo.Text.Trim();
                string dscubigeo = txtDscUbigeo.Text.Trim();
                string ruc = txtRuc.Text;
                string idcia = variables.getValorCia();

                try
                {
                    PerRet perret = new PerRet();
                    string validaExistenciaDoc = "0";

                    if (operacion == "AGREGAR")
                    {
                        validaExistenciaDoc = perret.verificaPerRetxDoc(idcia, tipdoc, nrodoc);
                    }

                    if (validaExistenciaDoc == "0")
                    {


                        perret.actualizaPerRet(operacion, idperplan, idcia, apepat, apemat, nombres, fecnac, tipdoc, nrodoc,
                        telfijo, celular, rpm, estcivil, nacion, email, catlic, nrolic, tipvia, nomvia, nrovia, intvia, tipzona, nomzona,
                        refzona, ubigeo, dscubigeo, sexo, ruc);
                        ((ui_PerRet)FormPadre).btnActualizar.PerformClick();
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("El Documento de Identidad ya ha sido registrado, por favor verificar", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        tabControlPer.SelectTab(0);
                        txtNroDoc.Focus();
                    }

                }
                catch (FormatException ex)
                {
                    MessageBox.Show(ex.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            ((ui_PerRet)FormPadre).btnActualizar.PerformClick();
            this.Close();
        }

        private void ui_validarDatos()
        {
            MaesGen maesgen = new MaesGen();
            Funciones funciones = new Funciones();

            string valorValida = "G";

            if (txtApPat.Text == string.Empty && valorValida == "G")
            {
                valorValida = "B";
                MessageBox.Show("No ha ingresado Apellido Paterno", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                tabControlPer.SelectTab(0);
                txtApPat.Focus();
            }

            if (txtApMat.Text == string.Empty && valorValida == "G")
            {
                valorValida = "B";
                MessageBox.Show("No ha ingresado Apellido Materno", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                tabControlPer.SelectTab(0);
                txtApMat.Focus();
            }

            if (txtNombres.Text == string.Empty && valorValida == "G")
            {
                valorValida = "B";
                MessageBox.Show("No ha ingresado Nombres", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                tabControlPer.SelectTab(0);
                txtNombres.Focus();
            }

            if (cmbTipoDocumento.Text == string.Empty && valorValida == "G")
            {
                valorValida = "B";
                MessageBox.Show("No ha seleccionado Tipo de Documento", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                tabControlPer.SelectTab(0);
                cmbTipoDocumento.Focus();
            }

            if (cmbTipoDocumento.Text != string.Empty && valorValida == "G")
            {
                string resultado = maesgen.verificaComboBoxMaesGen("002", cmbTipoDocumento.Text.Trim());
                if (resultado.Trim() == string.Empty)
                {
                    valorValida = "B";
                    MessageBox.Show("Dato incorrecto en Tipo de Documento", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    tabControlPer.SelectTab(0);
                    cmbTipoDocumento.Focus();
                }
            }

            if (txtNroDoc.Text == string.Empty && valorValida == "G")
            {
                valorValida = "B";
                MessageBox.Show("No ha ingresado Nro.Documento", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                tabControlPer.SelectTab(0);
                txtNroDoc.Focus();
            }

            if (txtNroDoc.Text != string.Empty && valorValida == "G")
            {
                if (cmbTipoDocumento.Text.Substring(0, 4).Trim() == "01")
                {
                    if (txtNroDoc.Text.Length != 8)
                    {
                        valorValida = "B";
                        MessageBox.Show("Nro.Documento incorrecto", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        tabControlPer.SelectTab(0);
                        txtNroDoc.Focus();
                    }

                }

            }

            if (UtileriasFechas.IsDate(txtFechaNac.Text) == false && valorValida == "G")
            {
                valorValida = "B";
                MessageBox.Show("Fecha de Nacimiento no válida", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                tabControlPer.SelectTab(0);
                txtFechaNac.Focus();
            }

            if (cmbNacionalidad.Text == string.Empty && valorValida == "G")
            {
                valorValida = "B";
                MessageBox.Show("No ha seleccionado Nacionalidad", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                tabControlPer.SelectTab(0);
                cmbNacionalidad.Focus();
            }

            if (cmbNacionalidad.Text != string.Empty && valorValida == "G")
            {
                string resultado = maesgen.verificaComboBoxMaesGen("003", cmbNacionalidad.Text.Trim());
                if (resultado.Trim() == string.Empty)
                {
                    valorValida = "B";
                    MessageBox.Show("Dato incorrecto en Nacionalidad", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    tabControlPer.SelectTab(0);
                    cmbNacionalidad.Focus();
                }
            }

            if (cmbNacionalidad.Text != string.Empty && valorValida == "G")
            {
                if (cmbTipoDocumento.Text.Substring(0, 4).Trim() == "01")
                {
                    if (cmbNacionalidad.Text.Substring(0, 4).Trim() != "9589")
                    {
                        valorValida = "B";
                        MessageBox.Show("Nacionalidad seleccionada incorrecta", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        tabControlPer.SelectTab(0);
                        cmbNacionalidad.Focus();
                    }
                }
            }

            if (cmbNacionalidad.Text != string.Empty && valorValida == "G")
            {
                if (cmbTipoDocumento.Text.Substring(0, 4).Trim() == "04")
                {
                    if (cmbNacionalidad.Text.Substring(0, 4).Trim() == "9589")
                    {
                        valorValida = "B";
                        MessageBox.Show("Nacionalidad seleccionada incorrecta", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        tabControlPer.SelectTab(0);
                        cmbNacionalidad.Focus();
                    }
                }
            }


            if (cmbSexo.Text == string.Empty && valorValida == "G")
            {
                valorValida = "B";
                MessageBox.Show("No ha seleccionado Sexo", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                tabControlPer.SelectTab(0);
                cmbSexo.Focus();
            }

            if (cmbSexo.Text != string.Empty && valorValida == "G")
            {
                string resultado = maesgen.verificaComboBoxMaesGen("019", cmbSexo.Text.Trim());
                if (resultado.Trim() == string.Empty)
                {
                    valorValida = "B";
                    MessageBox.Show("Dato incorrecto en Sexo", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    tabControlPer.SelectTab(0);
                    cmbSexo.Focus();
                }
            }

            if (cmbEstadoCivil.Text == string.Empty && valorValida == "G")
            {
                valorValida = "B";
                MessageBox.Show("No ha seleccionado Estado Civil", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                tabControlPer.SelectTab(0);
                cmbEstadoCivil.Focus();
            }

            if (cmbEstadoCivil.Text != string.Empty && valorValida == "G")
            {
                string resultado = maesgen.verificaComboBoxMaesGen("001", cmbEstadoCivil.Text.Trim());
                if (resultado.Trim() == string.Empty)
                {
                    valorValida = "B";
                    MessageBox.Show("Dato incorrecto en Estado Civil", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    tabControlPer.SelectTab(0);
                    cmbEstadoCivil.Focus();
                }
            }

            if (cmbCategoriaBrevete.Text == string.Empty && valorValida == "G")
            {
                valorValida = "B";
                MessageBox.Show("No ha seleccionado Categoría de Brevete", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                tabControlPer.SelectTab(0);
                cmbCategoriaBrevete.Focus();
            }

            if (cmbCategoriaBrevete.Text != string.Empty && valorValida == "G")
            {
                string resultado = maesgen.verificaComboBoxMaesGen("004", cmbCategoriaBrevete.Text.Trim());
                if (resultado.Trim() == string.Empty)
                {
                    valorValida = "B";
                    MessageBox.Show("Dato incorrecto en Categoría de Brevete", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    tabControlPer.SelectTab(0);
                    cmbCategoriaBrevete.Focus();
                }

            }

            if (funciones.getValorComboBox(cmbCategoriaBrevete, 4) != "XX" && valorValida == "G")
            {

                if (txtNroLicenciaConductor.Text == string.Empty)
                {
                    valorValida = "B";
                    MessageBox.Show("No ha ingresado Nro. de Licencia de Conducir", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    tabControlPer.SelectTab(0);
                    txtNroLicenciaConductor.Focus();
                }
            }

            if (txtRuc.Text != string.Empty && valorValida == "G")
            {

                if (txtRuc.Text.Length != 11)
                {
                    valorValida = "B";
                    MessageBox.Show("R.U.C. incorrecto", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    tabControlPer.SelectTab(0);
                    txtRuc.Focus();
                }
            }

            /*if (cmbTipoVia.Text == string.Empty && valorValida == "G")
            {
                valorValida = "B";
                MessageBox.Show("No ha seleccionado Tipo de Vía", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                tabControlPer.SelectTab(1);
                cmbTipoVia.Focus();
            }*/

            if (cmbTipoVia.Text != string.Empty && valorValida == "G")
            {
                string resultado = maesgen.verificaComboBoxMaesGen("017", cmbTipoVia.Text.Trim());
                if (resultado.Trim() == string.Empty)
                {
                    valorValida = "B";
                    MessageBox.Show("Dato incorrecto en Tipo de Vía", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    tabControlPer.SelectTab(1);
                    cmbTipoVia.Focus();
                }
            }

            /*if (txtNombreVia.Text == string.Empty && valorValida == "G")
            {
                valorValida = "B";
                MessageBox.Show("No ha ingresado Nombre de Vía", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                tabControlPer.SelectTab(1);
                txtNombreVia.Focus();
            }*/

            /*if (txtNumeroVia.Text == string.Empty && valorValida == "G")
            {
                valorValida = "B";
                MessageBox.Show("No ha ingresado Interior", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                tabControlPer.SelectTab(1);
                txtNumeroVia.Focus();
            }*/

            /*if (txtInteriorVia.Text == string.Empty && valorValida == "G")
            {
                valorValida = "B";
                MessageBox.Show("No ha ingresado Interior", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                tabControlPer.SelectTab(1);
                txtInteriorVia.Focus();
            }*/

            /*if (cmbTipoZona.Text == string.Empty && valorValida == "G")
            {
                valorValida = "B";
                MessageBox.Show("No ha seleccionado Tipo de Zona", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                tabControlPer.SelectTab(1);
                cmbTipoZona.Focus();
            }*/

            if (cmbTipoZona.Text != string.Empty && valorValida == "G")
            {
                string resultado = maesgen.verificaComboBoxMaesGen("018", cmbTipoZona.Text.Trim());
                if (resultado.Trim() == string.Empty)
                {
                    valorValida = "B";
                    MessageBox.Show("Dato incorrecto en Tipo de Zona", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    tabControlPer.SelectTab(1);
                    cmbTipoZona.Focus();
                }
            }

            /*if (txtNombreZona.Text == string.Empty && valorValida == "G")
            {
                valorValida = "B";
                MessageBox.Show("No ha ingresado Nombre de Zona", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                tabControlPer.SelectTab(1);
                txtNombreZona.Focus();
            }*/

            if (txtCodigoUbigeo.Text == string.Empty && valorValida == "G")
            {
                valorValida = "B";
                MessageBox.Show("No ha seleccionado Ubigeo", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                tabControlPer.SelectTab(1);
                txtCodigoUbigeo.Focus();
            }

            if (valorValida.Equals("G")) //GOOD 
            {
                txtValid.Text = "G";
                pictureValidAsk.Visible = false;
                pictureValidOk.Visible = true;
                pictureValidBad.Visible = false;
                MessageBox.Show("Registro validado exitosamente", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                if (valorValida.Equals("B")) //BAD
                {
                    txtValid.Text = "B";
                    pictureValidAsk.Visible = false;
                    pictureValidOk.Visible = false;
                    pictureValidBad.Visible = true;
                }
                else
                {
                    txtValid.Text = "Q"; //QUESTION
                    pictureValidAsk.Visible = true;
                    pictureValidOk.Visible = false;
                    pictureValidBad.Visible = false;
                }
            }
        }

        private void cmbCategoriaBrevete_SelectedIndexChanged(object sender, EventArgs e)
        {
            Funciones funciones = new Funciones();
            string clave = funciones.getValorComboBox(cmbCategoriaBrevete, 4);
            if (clave.Equals("XX"))
            {
                txtNroLicenciaConductor.Text = "";
                txtNroLicenciaConductor.Enabled = false;
            }
            else
            {

                txtNroLicenciaConductor.Enabled = true;
                txtNroLicenciaConductor.Focus();
            }
        }

        public void newPerRet()
        {
            txtOperacion.Text = "AGREGAR";
            txtValid.Text = "Q";
            pictureValidOk.Visible = false;
            pictureValidBad.Visible = false;
            pictureValidAsk.Visible = true;
            txtCodigoInterno.Clear();
            lblNombre.Text = "";
            txtApPat.Clear();
            txtApMat.Clear();
            txtNombres.Clear();
            txtFechaNac.Clear();
            cmbTipoDocumento.Text = "";
            txtNroDoc.Clear();
            txtTelFijo.Clear();
            txtCelular.Clear();
            txtRpm.Clear();
            cmbEstadoCivil.Text = "";
            cmbNacionalidad.Text = "";
            txtEmail.Clear();
            cmbCategoriaBrevete.Text = "";
            txtNroLicenciaConductor.Clear();
            txtRuc.Clear();
            cmbTipoVia.Text = "";
            txtNombreVia.Clear();
            txtNumeroVia.Clear();
            txtInteriorVia.Clear();
            cmbTipoZona.Text = "";
            txtNombreZona.Clear();
            txtReferenciasZona.Clear();
            txtCodigoUbigeo.Clear();
            txtDscUbigeo.Clear();
            MaesGen maesgen = new MaesGen();
            string stipo = string.Empty;

            DialogResult opcion = MessageBox.Show("¿Desea que se genere el Código Interno en forma automática?", "Aviso Importante", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (opcion == DialogResult.Yes)
            {
                GlobalVariables globalvariables = new GlobalVariables();
                string idcia = globalvariables.getValorCia();
                PerRet perret = new PerRet();
                txtCodigoInterno.Text = perret.generaCodigoInterno(idcia);
                txtCodigoInterno.Enabled = false;
                txtApPat.Focus();

            }
            else
            {
                txtCodigoInterno.Enabled = true;
                txtCodigoInterno.Focus();

            }
        }

        public void ui_loadPerRet(string idcia, string idperplan)
        {
            string squery;
            MaesGen maesgen = new MaesGen();
            txtOperacion.Text = "EDITAR";
            txtValid.Text = "Q";
            pictureValidOk.Visible = false;
            pictureValidBad.Visible = false;
            pictureValidAsk.Visible = false;

            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();
            squery = "SELECT * FROM perret where (idperplan='" + @idperplan + "' and idcia='" + @idcia + "');";

            try
            {
                SqlCommand myCommand = new SqlCommand(squery, conexion);
                SqlDataReader myReader = myCommand.ExecuteReader();

                if (myReader.Read())
                {
                    // DATOS PERSONALES
                    txtCodigoInterno.Enabled = false;
                    txtCodigoInterno.Text = myReader["idperplan"].ToString();
                    txtApPat.Text = myReader["apepat"].ToString();
                    txtApMat.Text = myReader["apemat"].ToString();
                    txtNombres.Text = myReader["nombres"].ToString();
                    txtTelFijo.Text = myReader["telfijo"].ToString();
                    txtCelular.Text = myReader["celular"].ToString();
                    txtRpm.Text = myReader["rpm"].ToString();
                    txtFechaNac.Text = myReader["fecnac"].ToString();
                    txtEmail.Text = myReader["email"].ToString();
                    txtNroDoc.Text = myReader["nrodoc"].ToString();
                    txtNroLicenciaConductor.Text = myReader["nrolic"].ToString();
                    txtRuc.Text = myReader["ruc"].ToString();

                    maesgen.consultaDetMaesGen("002", myReader["tipdoc"].ToString(), cmbTipoDocumento);
                    maesgen.consultaDetMaesGen("004", myReader["catlic"].ToString(), cmbCategoriaBrevete);
                    maesgen.consultaDetMaesGen("003", myReader["nacion"].ToString(), cmbNacionalidad);
                    maesgen.consultaDetMaesGen("001", myReader["estcivil"].ToString(), cmbEstadoCivil);
                    maesgen.consultaDetMaesGen("019", myReader["sexo"].ToString(), cmbSexo);

                    //DIRECCION
                    maesgen.consultaDetMaesGen("017", myReader["tipvia"].ToString(), cmbTipoVia);
                    txtNombreVia.Text = myReader["nomvia"].ToString();
                    txtNumeroVia.Text = myReader["nrovia"].ToString();
                    txtInteriorVia.Text = myReader["intvia"].ToString();
                    maesgen.consultaDetMaesGen("018", myReader["tipzona"].ToString(), cmbTipoZona);
                    txtNombreZona.Text = myReader["nomzona"].ToString();
                    txtReferenciasZona.Text = myReader["refzona"].ToString();
                    txtCodigoUbigeo.Text = myReader["ubigeo"].ToString();
                    txtDscUbigeo.Text = myReader["dscubigeo"].ToString();
                }

                myReader.Close();
                myCommand.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }

            conexion.Close();
        }

        private void txtApPat_TextChanged(object sender, EventArgs e)
        {
            string nombre;
            nombre = txtApPat.Text.Trim() + " " + txtApMat.Text.Trim() + " , " + txtNombres.Text.Trim();
            lblNombre.Text = nombre;
        }

        private void txtApMat_TextChanged(object sender, EventArgs e)
        {
            string nombre;
            nombre = txtApPat.Text.Trim() + " " + txtApMat.Text.Trim() + " , " + txtNombres.Text.Trim();
            lblNombre.Text = nombre;
        }

        private void txtNombres_TextChanged(object sender, EventArgs e)
        {
            string nombre;
            nombre = txtApPat.Text.Trim() + " " + txtApMat.Text.Trim() + " , " + txtNombres.Text.Trim();
            lblNombre.Text = nombre;
        }

        private void txtRuc_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                e.Handled = true;
                txtFechaNac.Focus();
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}