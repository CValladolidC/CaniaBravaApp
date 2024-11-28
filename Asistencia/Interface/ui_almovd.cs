using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Configuration;
using System.Data.SqlClient;

namespace CaniaBrava
{
    public partial class ui_almovd : Form
    {
        public string _codcia;
        public string _alma;
        public string _td;
        public string _numdoc;
        string _operacion;
        string _item;
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

        public ui_almovd()
        {
            InitializeComponent();
        }

        public void agregar()
        {
            this._operacion = "AGREGAR";
            lblTipo.Text = this._td;
            lblNroMov.Text = this._numdoc;
            txtCodigo.Clear();
            txtDescri.Clear();
            txtUnidad.Clear();
            txtFamilia.Clear();
            txtGrupo.Clear();
            //txtLote.Clear();

            //chkCerti.Checked = false;
            //txtFechaProd.Text = "";
            //txtFechaVen.Text = "";
            //txtFechaProd.Enabled = false;
            //txtFechaVen.Enabled = false;
            //cmbAna.Text = "NO";
            //cmbCal.Text = "NO";
            //txtGlosa1.Clear();
            //txtGlosa2.Clear();
            //txtGlosa3.Clear();
            txtStock.Text = "0";
            txtCantidad.Text = "0";
            btnAceptar.Enabled = true;
            txtCodigo.Focus();
        }

        public void editar(string item)
        {
            string query;
            this._operacion = "EDITAR";
            lblTipo.Text = this._td;
            lblNroMov.Text = this._numdoc;
            string codcia = this._codcia;
            string alma = this._alma;
            string td = this._td.Substring(0, 2);
            string numdoc = this._numdoc;
            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();
            query = "SELECT * FROM almovd where /*codcia='" + @codcia + "' and*/ alma='" + @alma + "' and td='" + @td + "' ";
            query = query + " and numdoc='" + @numdoc + "' and item='" + @item + "'; ";
            try

            {
                SqlCommand myCommand = new SqlCommand(query, conexion);
                SqlDataReader myReader = myCommand.ExecuteReader();

                if (myReader.Read())
                {
                    this._item = myReader.GetString(myReader.GetOrdinal("item"));
                    AlArti alarti = new AlArti();
                    txtCodigo.Text = myReader.GetString(myReader.GetOrdinal("codarti"));
                    txtDescri.Text = alarti.ui_getDatos(codcia, myReader.GetString(myReader.GetOrdinal("codarti")), "NOMBRE");
                    txtUnidad.Text = alarti.ui_getDatos(codcia, myReader.GetString(myReader.GetOrdinal("codarti")), "UNIDAD");
                    txtFamilia.Text = alarti.ui_getDatos(codcia, myReader.GetString(myReader.GetOrdinal("codarti")), "FAMILIA");
                    txtGrupo.Text = alarti.ui_getDatos(codcia, myReader.GetString(myReader.GetOrdinal("codarti")), "GRUPO");
                    //txtLote.Text = myReader.GetString(myReader.GetOrdinal("lote"));
                    //loteProduccion.Text = myReader.GetString(myReader.GetOrdinal("lotep")); 
                    if (myReader.GetString(myReader.GetOrdinal("certificado")).Equals("1"))
                    {
                        //chkCerti.Checked = true;
                        //txtFechaProd.Enabled = true;
                        //txtFechaVen.Enabled = true;
                        //cmbAna.Enabled = true;
                        //cmbCal.Enabled = true;
                        //txtGlosa1.Enabled = true;
                        //txtGlosa2.Enabled = true;
                        //txtGlosa3.Enabled = true;

                        //txtFechaProd.Text = myReader.GetString(myReader.GetOrdinal("fprod"));
                        //txtFechaVen.Text = myReader.GetString(myReader.GetOrdinal("fven"));
                        //if (myReader.GetString(myReader.GetOrdinal("analisis")).Equals("S"))
                        //{
                        //    cmbAna.Text = "SI";
                        //}
                        //else
                        //{
                        //    cmbAna.Text = "NO";
                        //}

                        //if (myReader.GetString(myReader.GetOrdinal("calidad")).Equals("S"))
                        //{
                        //    cmbCal.Text = "SI";
                        //}
                        //else
                        //{
                        //    cmbCal.Text = "NO";
                        //}
                        //txtGlosa1.Text = myReader.GetString(myReader.GetOrdinal("glosana1"));
                        //txtGlosa2.Text = myReader.GetString(myReader.GetOrdinal("glosana2"));
                        //txtGlosa3.Text = myReader.GetString(myReader.GetOrdinal("glosana3"));
                    }
                    else
                    {
                        //chkCerti.Checked = false;
                        //txtFechaProd.Enabled = false;
                        //txtFechaVen.Enabled = false;
                        //cmbAna.Enabled = false;
                        //cmbCal.Enabled = false;
                        //txtGlosa1.Enabled = false;
                        //txtGlosa2.Enabled = false;
                        //txtGlosa3.Enabled = false;

                        //txtFechaProd.Text = "";
                        //txtFechaVen.Text = "";
                        //cmbAna.Text = "NO";
                        //cmbCal.Text = "NO";
                        //txtGlosa1.Clear();
                        //txtGlosa2.Clear();
                        //txtGlosa3.Clear();
                    }
                }
                Alstock alstock = new Alstock();
                txtStock.Text = alstock.ui_getStock(codcia, alma, myReader.GetString(myReader.GetOrdinal("codarti")));
                txtCantidad.Text = myReader.GetDouble(myReader.GetOrdinal("cantidad")).ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            conexion.Close();
            btnAceptar.Enabled = false;
            if (td == "PE") { btnAceptar.Enabled = true; }
            txtCodigo.Focus();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            string valorValida = valida();
            if (valorValida.Equals("G"))
            {
                try
                {
                    Funciones funciones = new Funciones();
                    string operacion = this._operacion;
                    string codcia = "01";// this._codcia;
                    string alma = this._alma;
                    string td = this._td.Substring(0, 2);
                    string numdoc = this._numdoc;
                    string codarti = txtCodigo.Text.Trim();
                    string certificado = "0";

                    AlmovD almovd = new AlmovD();
                    string item;
                    if (this._operacion.Equals("AGREGAR"))
                    {
                        item = almovd.genCod(codcia, alma, td, numdoc);
                    }
                    else
                    {
                        item = this._item;
                    }

                    //if (chkCerti.Checked)
                    //{
                    //    certificado = "1";
                    //}

                    string vloteProduccion = string.Empty;
                    string lote = numdoc + "/" + item;
                    string fprod = "";//txtFechaProd.Text;
                    string fven = "";//txtFechaVen.Text;
                    string analisis = "";//funciones.getValorComboBox(cmbAna, 1);
                    string calidad = "";//funciones.getValorComboBox(cmbCal, 1);
                    string glosana1 = "";//txtGlosa1.Text.Trim();
                    string glosana2 = "";//txtGlosa2.Text.Trim();
                    string glosana3 = "";//txtGlosa3.Text.Trim();
                    float cantidad = float.Parse(txtCantidad.Text);
                    string codcenpro = "", tiposec = "", codseccion = "";
                    vloteProduccion = "";//loteProduccion.Text.Trim();    

                    almovd.updAlmovD(operacion, codcia, alma, td, numdoc, item, codarti, cantidad, certificado, lote,
                        fprod, fven, analisis, calidad, glosana1, glosana2, glosana3, codcenpro, tiposec, codseccion, vloteProduccion);

                    Alstock alstock = new Alstock();
                    alstock.recalcularStockProducto(codcia, alma, codarti);

                    ((ui_updalmov)FormPadre).ui_listaItem();
                    if (this._operacion.Equals("AGREGAR"))
                    {
                        agregar();
                    }
                    else
                    {
                        this.Close();
                    }
                }
                catch (FormatException ex)
                {
                    MessageBox.Show(ex.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void txtCodigo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                this._TextBoxActivo = txtCodigo;
                ui_viewarti ui_viewarti = new ui_viewarti();
                ui_viewarti._FormPadre = this;
                ui_viewarti._codcia = this._codcia;
                ui_viewarti._clasePadre = "ui_almovd";
                ui_viewarti._condicionAdicional = string.Empty;
                ui_viewarti.BringToFront();
                ui_viewarti.ShowDialog();
                ui_viewarti.Dispose();
            }
        }

        private string valida()
        {
            string valorValida = "G";
            string codcia = this._codcia;

            if (txtCodigo.Text == string.Empty && valorValida == "G")
            {
                valorValida = "B";
                MessageBox.Show("No ha ingresado Código de Producto", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtCodigo.Focus();
            }

            //if (chkCerti.Checked)
            //{
            //    if (valorValida == "G")
            //    {
            //        if (UtiFechas.IsDate(txtFechaProd.Text))
            //        {

            //            if (UtiFechas.IsDate(txtFechaVen.Text))
            //            {
            //                if (UtiFechas.compararFecha(txtFechaProd.Text, ">", txtFechaVen.Text))
            //                {
            //                    valorValida = "B";
            //                    MessageBox.Show("Fecha de Vencimiento no puede ser menor que la Fecha de Producción", "Avios", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //                    txtFechaVen.Focus();
            //                }
            //            }
            //            else
            //            {
            //                valorValida = "B";
            //                MessageBox.Show("Fecha de Vencimiento no válida", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //                txtFechaVen.Focus();
            //            }
            //        }
            //        else
            //        {
            //            valorValida = "B";
            //            MessageBox.Show("Fecha de Producción no válida", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //            txtFechaProd.Focus();
            //        }
            //    }
            //}

            if (valorValida == "G")
            {
                try
                {
                    float numero = float.Parse(txtCantidad.Text);
                    if (numero <= 0)
                    {
                        valorValida = "B";
                        MessageBox.Show("Cantidad no válida", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txtCantidad.Clear();
                        txtCantidad.Focus();
                    }

                }
                catch (FormatException)
                {
                    valorValida = "B";
                    MessageBox.Show("Cantidad no válida", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtCantidad.Clear();
                    txtCantidad.Focus();
                }
            }

            return valorValida;
        }

        private void ui_almovd_Load(object sender, EventArgs e)
        {

        }

        public void ui_consultaProducto(string codcia, string codarti)
        {
            AlArti alarti = new AlArti();
            string codigo = alarti.ui_getDatos(codcia, codarti, "CODIGO");
            txtCodigo.Text = codigo;
            txtDescri.Text = alarti.ui_getDatos(codcia, codarti, "NOMBRE");
            txtUnidad.Text = alarti.ui_getDatos(codcia, codarti, "UNIDAD");
            txtFamilia.Text = alarti.ui_getDatos(codcia, codarti, "FAMILIA");
            txtGrupo.Text = alarti.ui_getDatos(codcia, codarti, "GRUPO");
        }

        private void txtCodigo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(Keys.Enter))
            {
                AlArti alarti = new AlArti();
                string codcia = this._codcia;
                string alma = this._alma;
                string codarti = txtCodigo.Text.Trim();
                string nombre = alarti.ui_getDatos(codcia, codarti, "NOMBRE");
                if (nombre == string.Empty || codarti == string.Empty)
                {
                    txtCodigo.Clear();
                    txtDescri.Clear();
                    txtUnidad.Clear();
                    txtFamilia.Clear();
                    txtGrupo.Clear();
                    e.Handled = true;
                    txtCodigo.Focus();
                }
                else
                {
                    ui_consultaProducto(codcia, codarti);
                    Alstock alstock = new Alstock();
                    alstock.recalcularStockProducto(codcia, alma, codarti);
                    txtStock.Text = alstock.ui_getStock(codcia, alma, codarti);
                    e.Handled = true;
                    txtCantidad.Focus();
                }

            }
        }

        private void txtGlosa1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(Keys.Enter))
            {
                e.Handled = true;
                SendKeys.Send("{TAB}");
            }
        }

        private void txtGlosa2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(Keys.Enter))
            {
                e.Handled = true;
                SendKeys.Send("{TAB}");
            }
        }

        private void txtGlosa3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(Keys.Enter))
            {
                e.Handled = true;
                txtCantidad.Focus();
            }
        }

        private void txtFechaProd_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                //if (UtiFechas.IsDate(txtFechaProd.Text))
                //{
                //    e.Handled = true;
                //    txtFechaVen.Focus();
                //}
                //else
                //{
                //    MessageBox.Show("Fecha de Producción no válida", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //    e.Handled = true;
                //    txtFechaProd.Focus();
                //}
            }
        }

        private void txtFechaVen_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                //if (UtiFechas.IsDate(txtFechaVen.Text))
                //{
                //    e.Handled = true;
                //    cmbAna.Focus();
                //}
                //else
                //{
                //    MessageBox.Show("Fecha de Vencimiento no válida", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //    e.Handled = true;
                //    txtFechaVen.Focus();
                //}
            }
        }

        private void txtCantidad_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                try
                {
                    float numero = float.Parse(txtCantidad.Text);
                    e.Handled = true;
                    toolStripForm.Items[0].Select();
                    toolStripForm.Focus();
                }
                catch (FormatException exc)
                {
                    MessageBox.Show(exc.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    txtCantidad.Clear();
                    txtCantidad.Focus();
                }

            }
        }

        private void chkCerti_CheckedChanged(object sender, EventArgs e)
        {
            //if (chkCerti.Checked == true)
            //{
            //    txtFechaProd.Enabled = true;
            //    txtFechaVen.Enabled = true;
            //    cmbAna.Enabled = true;
            //    cmbCal.Enabled = true;
            //    txtGlosa1.Enabled = true;
            //    txtGlosa2.Enabled = true;
            //    txtGlosa3.Enabled = true;
            //    txtFechaProd.Focus();
            //}
            //else
            //{
            //    txtFechaProd.Enabled = false;
            //    txtFechaVen.Enabled = false;
            //    cmbAna.Enabled = false;
            //    cmbCal.Enabled = false;
            //    txtGlosa1.Enabled = false;
            //    txtGlosa2.Enabled = false;
            //    txtGlosa3.Enabled = false;
            //    txtFechaProd.Text = "";
            //    txtFechaVen.Text = "";
            //    cmbAna.Text = "NO";
            //    cmbCal.Text = "NO";
            //    txtGlosa1.Clear();
            //    txtGlosa2.Clear();
            //    txtGlosa3.Clear();
            //    txtCantidad.Focus();
            //}
        }

        private void cmbAna_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(Keys.Enter))
            {
                e.Handled = true;
                SendKeys.Send("{TAB}");
            }
        }

        private void cmbCal_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(Keys.Enter))
            {
                e.Handled = true;
                SendKeys.Send("{TAB}");
            }
        }

        private void btnNuevoClie_Click(object sender, EventArgs e)
        {
            ui_updalarti ui_updalarti = new ui_updalarti();
            ui_updalarti._FormPadre = this;
            ui_updalarti._codcia = this._codcia;
            ui_updalarti._clasePadre = "ui_almovd";
            ui_updalarti.Activate();
            ui_updalarti.agregar();
            ui_updalarti.ui_ActualizaComboBox();
            ui_updalarti.BringToFront();
            ui_updalarti.ShowDialog();
            ui_updalarti.Dispose();
        }

        private void btnEditarClie_Click(object sender, EventArgs e)
        {
            string codarti = txtCodigo.Text.Trim();

            if (codarti != string.Empty)
            {
                AlArti alarti = new AlArti();
                string codcia = this._codcia;

                string nombre = alarti.ui_getDatos(codcia, codarti, "NOMBRE");
                if (nombre == string.Empty)
                {
                    MessageBox.Show("Código del Producto a editar no existe", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
                else
                {
                    ui_updalarti ui_updalarti = new ui_updalarti();
                    ui_updalarti._FormPadre = this;
                    ui_updalarti._clasePadre = "ui_almovd";
                    ui_updalarti._codcia = this._codcia;
                    ui_updalarti.ui_ActualizaComboBox();
                    ui_updalarti.Activate();
                    ui_updalarti.BringToFront();
                    ui_updalarti.editar(codarti);
                    ui_updalarti.ShowDialog();
                    ui_updalarti.Dispose();
                }
            }
            else
            {
                MessageBox.Show("No ha ingresado Código de Producto a editar", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }

        }
    }
}