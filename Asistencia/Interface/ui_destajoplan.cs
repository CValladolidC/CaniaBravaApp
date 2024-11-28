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
    public partial class ui_destajoplan : Form
    {
        string _idperplan;
        string _idcia;
        string _idtipoper;
        string _operacion;
        string _anio;
        string _messem;
        string _idtipocal;
        string _idtipoplan;

        private Form FormPadre;

        public Form _FormPadre
        {
            get { return FormPadre; }
            set { FormPadre = value; }
        }

        public ui_destajoplan()
        {
            InitializeComponent();
        }

        public void ui_loadDatos(string idproddes, string idzontra, string cantidad, string precio, string total, string glosa)
        {


            this._operacion = "EDITAR";
            Funciones funciones = new Funciones();
            string squery;
            string idcia = this._idcia;
            ui_listaComboBox();
            squery = "Select idproddes as clave,desproddes as descripcion from proddes where idproddes='" + @idproddes + "' and idcia='" + @idcia + "';";
            funciones.consultaComboBox(squery, cmbProducto);
            squery = "Select idzontra as clave,deszontra as descripcion from zontra where idzontra='" + @idzontra + "' and idcia='" + @idcia + "';";
            funciones.consultaComboBox(squery, cmbZona);
            cmbProducto.Enabled = false;
            cmbZona.Enabled = false;
            txtCantidad.Text = cantidad;
            txtPrecio.Text = precio;
            txtTotal.Text = total;
            txtGlosa.Text = glosa;
            txtCantidad.Focus();

        }

        private void ui_destajoplan_Load(object sender, EventArgs e)
        {


        }

        internal void ui_listaComboBox()
        {

            Funciones funciones = new Funciones();
            string squery;
            string idcia = this._idcia;
            squery = "SELECT idproddes as clave,desproddes as descripcion FROM proddes where idcia='" + @idcia + "' and stateproddes='V' order by 1 asc";
            funciones.listaComboBox(squery, cmbProducto, "");
            squery = "SELECT idzontra as clave,deszontra as descripcion FROM zontra WHERE idcia='" + @idcia + "' order by 1 asc;";
            funciones.listaComboBox(squery, cmbZona, "");

        }

        internal float ui_calculaTotal(float cantidad, float precio)
        {
            float resultado = 0;
            resultado = cantidad * precio;
            return resultado;
        }

        public void setValores(string idperplan, string idcia, string idtipoper, string anio, string messem, string idtipocal, string idtipoplan)
        {
            this._operacion = string.Empty;
            this._idperplan = idperplan;
            this._idcia = idcia;
            this._idtipoper = idtipoper;
            this._anio = anio;
            this._messem = messem;
            this._idtipocal = idtipocal;
            this._idtipoplan = idtipoplan;
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        internal void ui_newDestajo()
        {
            this._operacion = "AGREGAR";
            ui_listaComboBox();
            txtCantidad.Text = "0";
            txtPrecio.Text = "0";
            txtTotal.Text = "0";
            txtCantidad.Focus();

        }

        private void txtCantidad_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                try
                {
                    float cantidad = float.Parse(txtCantidad.Text);
                    float precio = float.Parse(txtPrecio.Text);
                    txtTotal.Text = Convert.ToString(ui_calculaTotal(cantidad, precio));
                    e.Handled = true;
                    txtPrecio.Focus();
                }

                catch (FormatException ex)
                {
                    MessageBox.Show(ex.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtTotal.Text = "0";
                    e.Handled = true;
                    txtCantidad.Focus();
                }

            }
        }

        private void txtPrecio_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                try
                {
                    float cantidad = float.Parse(txtCantidad.Text);
                    float precio = float.Parse(txtPrecio.Text);
                    txtTotal.Text = Convert.ToString(ui_calculaTotal(cantidad, precio));
                    e.Handled = true;
                    toolStripForm.Items[0].Select();
                    toolStripForm.Focus();
                }

                catch (FormatException ex)
                {
                    MessageBox.Show(ex.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtTotal.Text = "0";
                    e.Handled = true;
                    txtPrecio.Focus();
                }

            }
        }

        private string ui_Validar()
        {
            string valorValida = "G";

            Funciones funciones = new Funciones();

            if (cmbProducto.Text == string.Empty && valorValida == "G")
            {
                valorValida = "B";
                MessageBox.Show("No ha seleccionado Producto", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cmbProducto.Focus();
            }

            if (cmbProducto.Text != string.Empty && valorValida == "G")
            {
                string idproddes = funciones.getValorComboBox(cmbProducto, 2);
                string idcia = this._idcia;
                string query = "SELECT idproddes as clave,desproddes as descripcion FROM proddes WHERE idproddes='" + @idproddes + "' and idcia='" + @idcia + "';";
                string resultado = funciones.verificaItemComboBox(query, cmbProducto);

                if (resultado.Trim() == string.Empty)
                {
                    valorValida = "B";
                    MessageBox.Show("Dato incorrecto en Producto", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    cmbProducto.Focus();
                }
            }

            if (cmbZona.Text == string.Empty && valorValida == "G")
            {
                valorValida = "B";
                MessageBox.Show("No ha seleccionado Zona", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cmbZona.Focus();
            }

            if (cmbZona.Text != string.Empty && valorValida == "G")
            {
                string idzontra = funciones.getValorComboBox(cmbZona, 2);
                string idcia = this._idcia;
                string query = "SELECT idzontra as clave,deszontra as descripcion FROM zontra WHERE idzontra='" + @idzontra + "' and idcia='" + @idcia + "';";
                string resultado = funciones.verificaItemComboBox(query, cmbZona);

                if (resultado.Trim() == string.Empty)
                {
                    valorValida = "B";
                    MessageBox.Show("Dato incorrecto en Zona", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    cmbZona.Focus();
                }
            }

            return valorValida;

        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            try
            {
                string resultado = ui_Validar();

                if (resultado.Equals("G"))
                {
                    Funciones funciones = new Funciones();
                    string idproddes = funciones.getValorComboBox(cmbProducto, 2);
                    string idzontra = funciones.getValorComboBox(cmbZona, 2);
                    float cantidad = float.Parse(txtCantidad.Text);
                    float precio = float.Parse(txtPrecio.Text);
                    float total = float.Parse(txtCantidad.Text) * float.Parse(txtPrecio.Text);
                    string glosa = txtGlosa.Text.Trim();
                    if (cantidad <= 0)
                    {
                        MessageBox.Show("Cantidad incorrecta", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        txtCantidad.Focus();

                    }
                    else
                    {
                        if (total > 0)
                        {
                            Tareo tareo = new Tareo();
                            tareo.actualizarTareo(this._operacion, this._idcia, this._idperplan, this._messem, this._anio, this._idtipocal, this._idtipoper, idproddes, idzontra, cantidad, precio, total, glosa, this._idtipoplan);
                            ((ui_upddatosplanilla)FormPadre).ui_ListaTareo();
                            ((ui_upddatosplanilla)FormPadre).ui_actualizar();
                            this.Close();

                        }

                    }


                }

            }
            catch (FormatException ex)
            {
                MessageBox.Show(ex.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cmbProducto_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                if (cmbProducto.Text != String.Empty)
                {
                    Funciones funciones = new Funciones();
                    string idcia = this._idcia;
                    string idproddes = funciones.getValorComboBox(cmbProducto, 2);
                    string squery = "SELECT idproddes as clave,desproddes as descripcion FROM proddes WHERE idcia='" + @idcia + "' and idproddes='" + @idproddes + "';";
                    funciones.validarCombobox(squery, cmbProducto);
                }
                e.Handled = true;
                cmbZona.Focus();

            }
        }

        private void cmbZona_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                if (cmbZona.Text != String.Empty)
                {
                    Funciones funciones = new Funciones();
                    string idcia = this._idcia;
                    string idzontra = funciones.getValorComboBox(cmbZona, 2);
                    string squery = "SELECT idzontra as clave,deszontra as descripcion FROM zontra WHERE idcia='" + @idcia + "' and idzontra='" + @idzontra + "';";
                    funciones.validarCombobox(squery, cmbZona);
                }
                e.Handled = true;
                txtCantidad.Focus();

            }
        }
    }
}