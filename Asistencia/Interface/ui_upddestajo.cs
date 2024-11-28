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
    public partial class ui_upddestajo : Form
    {
        string operacion;
        string idcia;
        string idproddes;
        string messem;
        string anio;
        string idtipoper;
        string idtipocal;
        string tiporegistro;
        string idzontra;
        string fecha;
        string idtipoplan;
        string emplea;
        string estane;
        string iddestajo;

        private TextBox TextBoxActivo;

        public TextBox _TextBoxActivo
        {
            get { return TextBoxActivo; }
            set { TextBoxActivo = value; }
        }

        private Form FormPadre;

        public Form _FormPadre
        {
            get { return FormPadre; }
            set { FormPadre = value; }
        }

        public ui_upddestajo()
        {
            InitializeComponent();
        }

        private void ui_upddestajo_Load(object sender, EventArgs e)
        {
            Funciones funciones = new Funciones();
            string idcia = this.idcia;
            string idproddes = this.idproddes;
            string squery;
            squery = "SELECT codvar as clave,desvar as descripcion FROM varproddes WHERE idcia='" + @idcia + "' and idproddes='" + @idproddes + "';";
            funciones.listaComboBox(squery, cmbVariedad, "");
        }

        public void ui_newDestajo()
        {
            this.operacion = "AGREGAR";
            txtCodigoInterno.Enabled = true;
            lblF2.Visible = true;
            pictureBoxBuscar.Visible = true;
            txtCodigoInterno.Clear();
            txtNombres.Clear();
            txtDocIdent.Clear();
            txtNroDocIden.Clear();
            txtCantidad.Text = "0";
            txtPrecio.Text = "0";
            txtSubTotal.Text = "0";
            txtMovilidad.Text = "0";
            txtRefrigerio.Text = "0";
            txtAdicional.Text = "0";
            txtTotal.Text = "0";
            txtCodigoInterno.Focus();

        }

        public void editar(string idperplan, string cantidad, string precio, string subtotal, string movilidad, string refrigerio, string adicional, string total, string codvar)
        {
            Funciones funciones = new Funciones();
            this.operacion = "EDITAR";
            txtCodigoInterno.Enabled = false;
            lblF2.Visible = false;
            pictureBoxBuscar.Visible = false;
            txtCodigoInterno.Text = idperplan;

            if (this.tiporegistro.Equals("P"))
            {
                PerPlan perplan = new PerPlan();
                txtDocIdent.Text = perplan.ui_getDatosPerPlan(this.idcia, idperplan, "1");
                txtNroDocIden.Text = perplan.ui_getDatosPerPlan(this.idcia, idperplan, "2");
                txtNombres.Text = perplan.ui_getDatosPerPlan(this.idcia, idperplan, "3");
            }
            else
            {
                PerRet perret = new PerRet();
                txtDocIdent.Text = perret.ui_getDatosPerRet(this.idcia, idperplan, "1");
                txtNroDocIden.Text = perret.ui_getDatosPerRet(this.idcia, idperplan, "2");
                txtNombres.Text = perret.ui_getDatosPerRet(this.idcia, idperplan, "3");
            }

            string query = "SELECT codvar as clave,desvar as descripcion FROM varproddes ";
            query = query + " WHERE idcia='" + @idcia + "' and idproddes='" + @idproddes + "';";
            funciones.consultaComboBox(query, cmbVariedad);

            txtCantidad.Text = cantidad;
            txtPrecio.Text = precio;
            txtSubTotal.Text = subtotal;
            txtMovilidad.Text = movilidad;
            txtRefrigerio.Text = refrigerio;
            txtAdicional.Text = adicional;
            txtTotal.Text = total;
            txtCantidad.Focus();

        }

        public float ui_calculaDestajo(float cantidad, float precio)
        {
            float resultado = 0;
            resultado = cantidad * precio;
            return resultado;
        }

        public float ui_calculaTotal(float subtotal, float movilidad, float refrigerio, float adicional)
        {
            float resultado = 0;
            resultado = subtotal + movilidad + refrigerio + adicional;
            return resultado;
        }

        public void setValores(string idcia, string idproddes, string messem, string anio, string idtipoper, string idtipocal, string idzontra, string tiporegistro, string fecha, string idtipoplan, string emplea, string estane, string iddestajo)
        {
            this.idcia = idcia;
            this.idproddes = idproddes;
            this.messem = messem;
            this.anio = anio;
            this.idtipoper = idtipoper;
            this.idtipocal = idtipocal;
            this.tiporegistro = tiporegistro;
            this.idzontra = idzontra;
            this.fecha = fecha;
            this.idtipoplan = idtipoplan;
            this.emplea = emplea;
            this.estane = estane;
            this.iddestajo = iddestajo;

        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtCodigoInterno_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                if (this.tiporegistro.Equals("P"))
                {
                    string idcia = this.idcia;
                    string idtipoper = this.idtipoper;
                    string rucemp = this.emplea;
                    string estane = this.estane;
                    string cadenaBusqueda = string.Empty;
                    this._TextBoxActivo = txtCodigoInterno;
                    string condicionAdicional = " and A.idtipoplan='" + @idtipoplan + "' and A.rucemp='" + @rucemp + "' and A.estane='" + @estane + "' ";
                    FiltrosMaestros filtros = new FiltrosMaestros();
                    filtros.filtrarPerPlan("ui_upddestajo", this, txtCodigoInterno, idcia, idtipoper, "V", cadenaBusqueda, condicionAdicional);
                }
                else
                {
                    string idcia = this.idcia;
                    string clasepadre = "ui_upddestajo";
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

        private void txtCodigoInterno_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                if (txtCodigoInterno.Text.Trim() != string.Empty)
                {
                    string idcia = this.idcia;
                    string idperplan = txtCodigoInterno.Text.Trim();

                    if (this.tiporegistro.Equals("P"))
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
                            cmbVariedad.Focus();
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
                            cmbVariedad.Focus();
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



            }
        }

        private void txtCantidad_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                try
                {
                    float cantidad = float.Parse(txtCantidad.Text);
                    float precio = float.Parse(txtPrecio.Text);
                    txtSubTotal.Text = Convert.ToString(ui_calculaDestajo(cantidad, precio));
                    float subtotal = float.Parse(txtSubTotal.Text);
                    float movilidad = float.Parse(txtMovilidad.Text);
                    float refrigerio = float.Parse(txtRefrigerio.Text);
                    float adicional = float.Parse(txtAdicional.Text);
                    txtTotal.Text = Convert.ToString(ui_calculaTotal(subtotal, movilidad, refrigerio, adicional));
                    e.Handled = true;
                    txtPrecio.Focus();
                }

                catch (FormatException ex)
                {
                    MessageBox.Show(ex.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtSubTotal.Text = "0";
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
                    txtSubTotal.Text = Convert.ToString(ui_calculaDestajo(cantidad, precio));
                    float subtotal = float.Parse(txtSubTotal.Text);
                    float movilidad = float.Parse(txtMovilidad.Text);
                    float refrigerio = float.Parse(txtRefrigerio.Text);
                    float adicional = float.Parse(txtAdicional.Text);
                    txtTotal.Text = Convert.ToString(ui_calculaTotal(subtotal, movilidad, refrigerio, adicional));
                    e.Handled = true;
                    txtMovilidad.Focus();
                }

                catch (FormatException ex)
                {
                    MessageBox.Show(ex.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtSubTotal.Text = "0";
                    e.Handled = true;
                    txtPrecio.Focus();
                }

            }
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            try
            {
                Funciones funciones = new Funciones();
                string operacion = this.operacion;
                string iddestajo = this.iddestajo;
                string idcia = this.idcia;
                string idproddes = this.idproddes;
                string messem = this.messem;
                string anio = this.anio;
                string idtipoper = this.idtipoper;
                string idtipocal = this.idtipocal;
                string tiporegistro = this.tiporegistro;
                string idzontra = this.idzontra;
                string fecha = this.fecha;
                string idperplan = txtCodigoInterno.Text;
                string emplea = this.emplea;
                string estane = this.estane;
                float cantidad = float.Parse(txtCantidad.Text);
                float precio = float.Parse(txtPrecio.Text);
                float subtotal = cantidad * precio;
                float movilidad = float.Parse(txtMovilidad.Text);
                float refrigerio = float.Parse(txtRefrigerio.Text);
                float adicional = float.Parse(txtAdicional.Text);
                float total = subtotal + movilidad + refrigerio + adicional;
                string glosa = string.Empty;
                string codvar = funciones.getValorComboBox(cmbVariedad, 2);

                string idtipoplan;

                if (tiporegistro.Equals("P"))
                {
                    idtipoplan = this.idtipoplan;
                }
                else
                {
                    idtipoplan = "";
                }

                if (idperplan == string.Empty)
                {
                    MessageBox.Show("No ha especificado Código de Trabajador", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    txtCodigoInterno.Focus();

                }
                else
                {
                    if (cantidad <= 0)
                    {
                        MessageBox.Show("Cantidad incorrecta", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        txtCantidad.Focus();
                    }
                    else
                    {
                        if (total > 0)
                        {
                            Destajo destajo = new Destajo();
                            destajo.actualizarDestajo(tiporegistro, operacion, idcia, idperplan, messem, anio, idtipocal, idtipoper, fecha,
                                idproddes, idzontra, cantidad, precio, subtotal, movilidad, refrigerio, adicional, total, idtipoplan, emplea,
                                estane, iddestajo, glosa, codvar);
                            ((ui_destajo)FormPadre).btnActualizar.PerformClick();
                            this.ui_newDestajo();
                        }

                    }
                }

            }
            catch (FormatException ex)
            {
                MessageBox.Show(ex.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtAdicional_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                try
                {
                    float cantidad = float.Parse(txtCantidad.Text);
                    float precio = float.Parse(txtPrecio.Text);
                    txtSubTotal.Text = Convert.ToString(ui_calculaDestajo(cantidad, precio));
                    float subtotal = float.Parse(txtSubTotal.Text);
                    float movilidad = float.Parse(txtMovilidad.Text);
                    float refrigerio = float.Parse(txtRefrigerio.Text);
                    float adicional = float.Parse(txtAdicional.Text);
                    txtTotal.Text = Convert.ToString(ui_calculaTotal(subtotal, movilidad, refrigerio, adicional));
                    e.Handled = true;
                    toolStripForm.Items[0].Select();
                    toolStripForm.Focus();
                }

                catch (FormatException ex)
                {
                    MessageBox.Show(ex.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtAdicional.Text = "0";
                    e.Handled = true;
                    txtAdicional.Focus();
                }

            }


        }

        private void txtMovilidad_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                try
                {
                    float cantidad = float.Parse(txtCantidad.Text);
                    float precio = float.Parse(txtPrecio.Text);
                    txtSubTotal.Text = Convert.ToString(ui_calculaDestajo(cantidad, precio));
                    float subtotal = float.Parse(txtSubTotal.Text);
                    float movilidad = float.Parse(txtMovilidad.Text);
                    float refrigerio = float.Parse(txtRefrigerio.Text);
                    float adicional = float.Parse(txtAdicional.Text);
                    txtTotal.Text = Convert.ToString(ui_calculaTotal(subtotal, movilidad, refrigerio, adicional));
                    e.Handled = true;
                    txtRefrigerio.Focus();
                }

                catch (FormatException ex)
                {
                    MessageBox.Show(ex.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtMovilidad.Text = "0";
                    e.Handled = true;
                    txtMovilidad.Focus();
                }

            }
        }

        private void txtRefrigerio_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                try
                {
                    float cantidad = float.Parse(txtCantidad.Text);
                    float precio = float.Parse(txtPrecio.Text);
                    txtSubTotal.Text = Convert.ToString(ui_calculaDestajo(cantidad, precio));
                    float subtotal = float.Parse(txtSubTotal.Text);
                    float movilidad = float.Parse(txtMovilidad.Text);
                    float refrigerio = float.Parse(txtRefrigerio.Text);
                    float adicional = float.Parse(txtAdicional.Text);
                    txtTotal.Text = Convert.ToString(ui_calculaTotal(subtotal, movilidad, refrigerio, adicional));
                    e.Handled = true;
                    txtAdicional.Focus();
                }

                catch (FormatException ex)
                {
                    MessageBox.Show(ex.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtRefrigerio.Text = "0";
                    e.Handled = true;
                    txtRefrigerio.Focus();
                }

            }
        }

        private void cmbVariedad_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void cmbVariedad_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                e.Handled = true;
                txtCantidad.Focus();
            }

        }
    }
}