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
    public partial class ui_UpdCambioPrecio : Form
    {
        string _idcia;
        string _messem;
        string _anio;
        string _idtipocal;
        string _idtipoplan;
        string _idtipoper;
        string _emplea;
        string _estane;
        string _tiporegistro;

        string _idzontra;
        string _idproddes;
        string _codvar;
        string _nomzona;
        string _nomproddes;
        string _nomvar;

        float _cantidad;
        float _precio;
        float _importe;

        private Form FormPadre;

        public Form _FormPadre
        {
            get { return FormPadre; }
            set { FormPadre = value; }
        }

        public ui_UpdCambioPrecio()
        {
            InitializeComponent();
        }

        public void ui_setValores(string idcia, string messem, string anio, string idtipocal,
            string tiporegistro, string idtipoplan, string idtipoper, string emplea, string estane, string idzontra,
            string idproddes, string nomzona, string nomproddes, float cantidad, float precio,
            float importe, string nomvar, string codvar)
        {
            this._idcia = idcia;
            this._messem = messem;
            this._anio = anio;
            this._idtipocal = idtipocal;
            this._idtipoper = idtipoper;
            this._emplea = emplea;
            this._estane = estane;
            this._idzontra = idzontra;
            this._idproddes = idproddes;
            this._nomzona = nomzona;
            this._nomproddes = nomproddes;
            this._cantidad = cantidad;
            this._precio = precio;
            this._importe = importe;
            this._idtipoplan = idtipoplan;
            this._tiporegistro = tiporegistro;
            this._nomvar = nomvar;
            this._codvar = codvar;
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ui_UpdCambioPrecio_Load(object sender, EventArgs e)
        {
            Funciones funciones = new Funciones();
            string idcia = this._idcia;
            string idproddes = this._idproddes;
            txtZona.Text = this._nomzona;
            txtProducto.Text = this._nomproddes;
            txtVariedad.Text = this._nomvar;
            txtCantidad.Text = Convert.ToString(this._cantidad);
            txtPrecio.Text = Convert.ToString(this._precio);
            txtTotal.Text = Convert.ToString(this._importe);
            txtPrecioNuevo.Text = Convert.ToString(this._precio);
            txtPrecioNuevo.Focus();

        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            try
            {
                Funciones funciones = new Funciones();
                string idcia = this._idcia;
                string messem = this._messem;
                string anio = this._anio;
                string idtipocal = this._idtipocal;
                string idtipoper = this._idtipoper;
                string emplea = this._emplea;
                string estane = this._estane;
                string idzontra = this._idzontra;
                string idproddes = this._idproddes;
                string nomzona = this._nomzona;
                string nomproddes = this._nomproddes;
                float precio = float.Parse(txtPrecioNuevo.Text);
                string idtipoplan = this._idtipoplan;
                string tiporegistro = this._tiporegistro;
                string codvar = this._codvar;
                Destajo destajo = new Destajo();
                destajo.actualizarPrecioDestajo(tiporegistro, idcia, messem, anio, idtipocal, idtipoper,
                    idproddes, idzontra, precio, idtipoplan, emplea, estane, codvar);
                ((ui_CambioPrecioDestajo)FormPadre).btnActualizar.PerformClick();
                this.Close();

            }
            catch (FormatException ex)
            {
                MessageBox.Show(ex.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}