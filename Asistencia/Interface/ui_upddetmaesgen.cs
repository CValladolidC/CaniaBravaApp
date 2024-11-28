using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CaniaBrava
{
    public partial class ui_upddetmaesgen : Form
    {
        private Form FormPadre;
        private string ClasePadre;

        public Form _FormPadre
        {
            get { return FormPadre; }
            set { FormPadre = value; }
        }

        public string _clasePadre
        {
            get { return ClasePadre; }
            set { ClasePadre = value; }
        }

        public ui_upddetmaesgen()
        {
            InitializeComponent();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void cmbEstado_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                if (cmbEstado.Text == String.Empty)
                {
                    MessageBox.Show("El campo no acepta valores vacíos", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    e.Handled = true;
                    cmbEstado.Focus();
                }
                else
                {
                    switch (cmbEstado.Text.ToUpper().Substring(0, 1))
                    {
                        case "A":
                            cmbEstado.Text = "A        ANULADO";
                            break;
                        case "V":
                            cmbEstado.Text = "V         VIGENTE";
                            break;
                        default:
                            cmbEstado.Text = "V         VIGENTE";
                            break;
                    }
                    e.Handled = true;
                    toolStripForm.Items[0].Select();
                    toolStripForm.Focus();
                }
            }
        }

        private void ui_upddetmaesgen_Load(object sender, EventArgs e)
        {

        }

        public void newDetMaesGen(string maesgen)
        {
            txtOperacion.Text = "AGREGAR";
            txtMaestro.Text = maesgen;
            txtCodigo.Enabled = true;
            txtCodigo.Clear();
            txtDescripcion.Clear();
            txtAbrevia.Clear();
            txtParametro1.Clear();
            txtParametro2.Clear();
            txtParametro3.Clear();
            cmbTipo.Text = "U        USUARIO";
            cmbEstado.Text = "V         VIGENTE";
            txtCodigo.Focus();
        }

        public void newDetMaesGen(string maesgen, string usuario, string capataz, string fundo, string equipo, string turno)
        {
            txtOperacion.Text = "AGREGAR";
            txtMaestro.Text = maesgen;
            txtCodigo.Enabled = true;
            txtCodigo.Clear();
            txtDescripcion.Clear();
            txtAbrevia.Clear();
            txtParametro1.Clear();
            txtParametro2.Clear();
            txtParametro3.Clear();
            if (maesgen == "163")
            {
                lblDescripcion.Text = "Supervisor";
                lblAbrevia.Text = "Tareador";
                label1.Text = "Fundo :";
                label2.Text = "Equipo :";
                label3.Text = "Turnos :";
                lblTR.Text = "(Separar turnos en comas: \",\")";

                if (usuario != string.Empty)
                {
                    txtDescripcion.Text = usuario;
                }
                if (capataz != string.Empty)
                {
                    txtAbrevia.Text = capataz;
                }
                if (fundo != string.Empty)
                {
                    txtParametro1.Text = fundo;
                }
                if (equipo != string.Empty)
                {
                    txtParametro2.Text = equipo;
                }
                if (turno != string.Empty)
                {
                    txtParametro3.Text = turno;
                }
            }
            cmbTipo.Text = "U        USUARIO";
            cmbEstado.Text = "V         VIGENTE";
            txtCodigo.Focus();
        }

        public void loadDetMaesGen(string maesgen, string clavemaesgen, string desmaesgen, string abrevia,
            string parm1maesgen, string parm2maesgen, string parm3maesgen, string statemaesgen, string tipomaesgen)
        {

            txtOperacion.Text = "EDITAR";
            txtMaestro.Text = maesgen;
            txtCodigo.Enabled = false;
            txtCodigo.Text = clavemaesgen;
            txtDescripcion.Text = desmaesgen;
            txtAbrevia.Text = abrevia;
            txtParametro1.Text = parm1maesgen;
            txtParametro2.Text = parm2maesgen;
            txtParametro3.Text = parm3maesgen;

            switch (tipomaesgen)
            {
                case "U":
                    cmbTipo.Text = "U        USUARIO";
                    break;
                case "S":
                    cmbTipo.Text = "S        SISTEMA";
                    break;
                default:
                    cmbTipo.Text = "U        USUARIO";
                    break;
            }


            switch (statemaesgen)
            {
                case "A":
                    cmbEstado.Text = "A        ANULADO";
                    break;
                case "V":
                    cmbEstado.Text = "V         VIGENTE";
                    break;
                default:
                    cmbEstado.Text = "V         VIGENTE";
                    break;
            }

            txtDescripcion.Focus();

        }

        private void txtCodigo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                if (this.ClasePadre == "ui_view163")
                {
                    if (txtCodigo.Text.Trim() != string.Empty)
                    {
                        string idperplan = txtCodigo.Text.Trim();

                        MaesGen maesgen = new MaesGen();
                        string codigo = maesgen.verificaComboBoxMaesGen("162", txtCodigo.Text);
                        PerPlan perplan = new PerPlan();
                        string codigoInterno = perplan.ui_getDatosPerPlan(idperplan, "0");
                        if (codigo == string.Empty)
                        {
                            MessageBox.Show("La actividad no existe.\nSe abrira ventana para ingresar nueva actividad",
                                "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            ui_upddetmaesgen ui_updalarti = new ui_upddetmaesgen();
                            ui_updalarti._FormPadre = this;
                            ui_updalarti._clasePadre = "ui_view162";
                            ui_updalarti.Activate();
                            ui_updalarti.newDetMaesGen("162");
                            ui_updalarti.BringToFront();
                            ui_updalarti.ShowDialog();
                            ui_updalarti.Dispose();
                            this.ClasePadre = "ui_view162";
                        }
                        else
                        {
                            MessageBox.Show(codigo, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            txtCodigo.Text = codigo.Substring(0, 4);
                            e.Handled = true;
                            toolStripForm.Items[0].Select();
                            toolStripForm.Focus();
                        }
                    }
                    else { LimpiarFiltros(); }
                }
                else
                {
                    e.Handled = true;
                    txtDescripcion.Focus();
                }
            }
        }

        private void LimpiarFiltros()
        {
            throw new NotImplementedException();
        }

        private void txtDescripcion_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                e.Handled = true;
                txtAbrevia.Focus();
            }
        }

        private void txtParametro1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                e.Handled = true;
                txtParametro2.Focus();
            }
        }

        private void txtParametro2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                e.Handled = true;
                txtParametro3.Focus();
            }
        }

        private void txtParametro3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                e.Handled = true;
                cmbTipo.Focus();
            }
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            try
            {
                Funciones funciones = new Funciones();

                string operacion = txtOperacion.Text.Trim();
                string idmaesgen = txtMaestro.Text.Substring(0, 3);
                string clavemaesgen = txtCodigo.Text.Trim();
                string desmaesgen = txtDescripcion.Text.Trim();
                string abrevia = txtAbrevia.Text.Trim();
                string parm1maesgen = txtParametro1.Text.Trim();
                string parm2maesgen = txtParametro2.Text.Trim();
                string parm3maesgen = txtParametro3.Text.Trim();
                string tipomaesgen = funciones.getValorComboBox(cmbTipo, 1);
                string statemaesgen = funciones.getValorComboBox(cmbEstado, 1);

                if (clavemaesgen == string.Empty)
                {
                    MessageBox.Show("No ha especificado Código", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    txtCodigo.Focus();
                }
                else
                {
                    if (tipomaesgen == string.Empty)
                    {
                        MessageBox.Show("No ha especificado Tipo", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        cmbTipo.Focus();
                    }
                    else
                    {
                        if (tipomaesgen != "U" && tipomaesgen != "S")
                        {
                            MessageBox.Show("Dato incorrecto en Tipo", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                            cmbTipo.Focus();
                        }
                        else
                        {
                            if (statemaesgen == string.Empty)
                            {
                                MessageBox.Show("No ha especificado Estado", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                                cmbEstado.Focus();
                            }
                            else
                            {
                                if (statemaesgen != "V" && statemaesgen != "A")
                                {
                                    MessageBox.Show("Dato incorrecto en Estado", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                                    cmbEstado.Focus();
                                }
                                else
                                {
                                    MaesGen updDetMaesGen = new MaesGen();
                                    updDetMaesGen.actualizarMaesGen(operacion, idmaesgen, clavemaesgen, desmaesgen, abrevia, parm1maesgen, parm2maesgen, parm3maesgen, statemaesgen, tipomaesgen);

                                    if (this.ClasePadre == "ui_view163")
                                    {
                                        desmaesgen = ((ui_viewactividadesjornales)FormPadre).Usuario;
                                        abrevia = ((ui_viewactividadesjornales)FormPadre).Capataz;
                                        parm1maesgen = ((ui_viewactividadesjornales)FormPadre).Fundo;
                                        parm2maesgen = ((ui_viewactividadesjornales)FormPadre).Equipo;
                                        parm3maesgen = ((ui_viewactividadesjornales)FormPadre).Turno;

                                        updDetMaesGen.actualizarMaesGen(operacion, "163", clavemaesgen, desmaesgen, abrevia, parm1maesgen, parm2maesgen, parm3maesgen, statemaesgen, tipomaesgen);
                                    }
                                    else
                                    {
                                        if (this.ClasePadre == "ui_view162")
                                        {
                                            txtDescripcion.Text = desmaesgen;
                                        }
                                        else
                                        {
                                            if (this.ClasePadre == "ui_view")
                                            {
                                                //((ui_viewactividades)FormPadre).
                                            }
                                            else
                                            {
                                                ((ui_detmaesgen)FormPadre).btnActualizar.PerformClick();
                                            }
                                        }
                                    }
                                    this.Close();
                                }
                            }
                        }
                    }
                }
            }
            catch (FormatException ex)
            {
                MessageBox.Show(ex.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cmbTipo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                if (cmbTipo.Text == String.Empty)
                {
                    MessageBox.Show("El campo no acepta valores vacíos", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    e.Handled = true;
                    cmbTipo.Focus();
                }
                else
                {
                    switch (cmbTipo.Text.ToUpper().Substring(0, 1))
                    {
                        case "S":
                            cmbTipo.Text = "S         SISTEMA";
                            break;
                        case "U":
                            cmbTipo.Text = "U         USUARIO";
                            break;
                        default:
                            cmbTipo.Text = "S         SISTEMA";
                            break;
                    }
                    e.Handled = true;
                    cmbEstado.Focus();

                }
            }
        }

        private void txtAbrevia_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                e.Handled = true;
                txtParametro1.Focus();
            }
        }
    }
}