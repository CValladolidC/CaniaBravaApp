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
    public partial class ui_ubigeo : Form
    {
        public string _clasePadre;

        private Form FormPadre;

        public Form _FormPadre
        {
            get { return FormPadre; }
            set { FormPadre = value; }
        }

        public ui_ubigeo()
        {
            InitializeComponent();
        }

        private void ui_ubigeo_Load(object sender, EventArgs e)
        {
            Funciones funciones = new Funciones();
            string squery = "SELECT DISTINCT departam as descripcion FROM ubigeo ORDER BY 1 ASC;";
            funciones.listaComboBoxUnCampo(squery, cmbDepartamento, "B");
        }

        public void ui_nuevaSeleccion()
        {
            txtCodigoUbigeo.Clear();
            cmbDepartamento.Text = "";
            cmbProvincia.Text = "";
            cmbDistrito.Text = "";
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void cmbDepartamento_SelectedValueChanged(object sender, EventArgs e)
        {
            Funciones funcionusr = new Funciones();
            string clave = funcionusr.getValorComboBox(cmbDepartamento, 40);
            string squery;
            squery = "SELECT DISTINCT provincia as descripcion FROM ubigeo WHERE departam='" + @clave + "' ORDER BY 1 ASC;";
            funcionusr.listaComboBoxUnCampo(squery, cmbProvincia, "B");
            cmbProvincia.Focus();
        }

        private void cmbProvincia_SelectedValueChanged(object sender, EventArgs e)
        {
            Funciones funcionusr = new Funciones();

            string departam = funcionusr.getValorComboBox(cmbDepartamento, 40);
            string provincia = funcionusr.getValorComboBox(cmbProvincia, 40);
            string squery;
            squery = "SELECT DISTINCT distrito as descripcion FROM ubigeo WHERE departam='" + @departam + "' and provincia='" + @provincia + "' ORDER BY 1 ASC;";
            funcionusr.listaComboBoxUnCampo(squery, cmbDistrito, "B");
            cmbDistrito.Focus();
        }

        private void cmbDistrito_SelectedValueChanged(object sender, EventArgs e)
        {
            Funciones funcionusr = new Funciones();

            string departam = funcionusr.getValorComboBox(cmbDepartamento, 40);
            string provincia = funcionusr.getValorComboBox(cmbProvincia, 40);
            string distrito = funcionusr.getValorComboBox(cmbDistrito, 40);
            Ubigeo ubigeo = new Ubigeo();
            txtCodigoUbigeo.Text = ubigeo.consultaCodigoUbigeo(departam, provincia, distrito);
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            if (txtCodigoUbigeo.Text.Trim() != string.Empty)
            {
                if (this._clasePadre.Equals("ui_updpersonal"))
                {
                    ((ui_updpersonal)FormPadre)._TextBoxUbigeo.Text = txtCodigoUbigeo.Text.Trim();
                    ((ui_updpersonal)FormPadre)._TextBoxDscUbigeo.Text = cmbDepartamento.Text.Trim() + ", " + cmbProvincia.Text.Trim() + ", " + cmbDistrito.Text.Trim();
                }
                else
                {
                    if (this._clasePadre.Equals("ui_updPerRet"))
                    {
                        ((ui_updPerRet)FormPadre)._TextBoxUbigeo.Text = txtCodigoUbigeo.Text.Trim();
                        ((ui_updPerRet)FormPadre)._TextBoxDscUbigeo.Text = cmbDepartamento.Text.Trim() + ", " + cmbProvincia.Text.Trim() + ", " + cmbDistrito.Text.Trim();
                    }
                    else
                    {
                        if (this._clasePadre.Equals("ui_updcensalud"))
                        {
                            ((ui_updcensalud)FormPadre)._TextBoxUbigeo.Text = txtCodigoUbigeo.Text.Trim();
                            ((ui_updcensalud)FormPadre)._TextBoxDscUbigeo.Text = cmbDepartamento.Text.Trim() + ", " + cmbProvincia.Text.Trim() + ", " + cmbDistrito.Text.Trim();
                        }
                        else
                        {
                            if (this._clasePadre.Equals("ui_updsolicapacitaciones"))
                            {
                                ((ui_updsolicapacitaciones)FormPadre)._TextBoxUbigeo.Text = txtCodigoUbigeo.Text.Trim();
                                ((ui_updsolicapacitaciones)FormPadre)._TextBoxDscUbigeo.Text = cmbDepartamento.Text.Trim() + ", " + cmbProvincia.Text.Trim() + ", " + cmbDistrito.Text.Trim();
                            }
                        }
                    }
                }
                this.DialogResult = DialogResult.OK;
                Close();
            }
            else
            {
                MessageBox.Show("No ha seleccionado Ubicación Geográfica correctamente", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
     
        }
    }
}