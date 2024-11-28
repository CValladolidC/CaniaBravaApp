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
    public partial class ui_solalmad : Form
    {
        public string _codcia;
        public string _alma;
        public string _secsoli;
        public string _solalma;
        string _operacion;
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

        public ui_solalmad()
        {
            InitializeComponent();
        }

        public void agregar()
        {
            this._operacion = "AGREGAR";
            txtItem.Clear();
            txtItem.Enabled = false;
            txtCodigo.Clear();
            lblDescri.Text = "";
            lblUnidad.Text = "";
            chkRegistro.Checked = false;
            lblDescri.Text = "";
            txtDescri.Text = "";
            txtDescri.Visible = false;
            txtGlosa1.Clear();
            txtGlosa2.Clear();
            txtGlosa3.Clear();
            txtStock.Text = "0";
            txtCantidad.Text = "0";
            txtCodigo.Focus();
        }

        public void editar(string item)
        {
            string query;
            this._operacion = "EDITAR";
            string alma = this._alma;
            string codcia = this._codcia;
            string secsoli = this._secsoli;
            string codsoli = this._solalma;
            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();
            query = "SELECT * FROM solalmad where /*codcia='" + @codcia + "' and secsoli='" + @secsoli + "' and*/ alma='" + @alma + "' ";
            query += "and solalma='" + @codsoli + "' and item='" + @item + "'; ";
            try
            {
                SqlCommand myCommand = new SqlCommand(query, conexion);
                SqlDataReader myReader = myCommand.ExecuteReader();
                if (myReader.Read())
                {
                    txtItem.Text = myReader.GetString(myReader.GetOrdinal("item"));
                    AlArti alarti = new AlArti();
                    txtCodigo.Text = myReader.GetString(myReader.GetOrdinal("codarti"));
                    if (myReader.GetString(myReader.GetOrdinal("manual")).Equals("0"))
                    {
                        chkRegistro.Checked = false;
                        lblDescri.Text = alarti.ui_getDatos(codcia, myReader.GetString(myReader.GetOrdinal("codarti")), "NOMBRE");
                        lblUnidad.Text = alarti.ui_getDatos(codcia, myReader.GetString(myReader.GetOrdinal("codarti")), "UNIDAD");
                        txtDescri.Text = alarti.ui_getDatos(codcia, myReader.GetString(myReader.GetOrdinal("codarti")), "NOMBRE");
                        txtUnidad.Text = alarti.ui_getDatos(codcia, myReader.GetString(myReader.GetOrdinal("codarti")), "UNIDAD");
                        lblDescri.Visible = true;
                        lblUnidad.Visible = true;
                        lblUni.Visible = true;
                        txtDescri.Visible = false;
                        Alstock alstock = new Alstock();
                        txtStock.Text = alstock.ui_getStock(codcia, alma, myReader.GetString(myReader.GetOrdinal("codarti")));
                    }
                    else
                    {
                        chkRegistro.Checked = true;
                        lblDescri.Text = "";
                        lblUnidad.Text = "";
                        txtDescri.Text = myReader.GetString(myReader.GetOrdinal("desarti"));
                        txtUnidad.Text = myReader.GetString(myReader.GetOrdinal("unidad"));
                        lblDescri.Visible = false;
                        lblUnidad.Visible = false;
                        lblUni.Visible = false;
                        txtDescri.Visible = true;
                        txtStock.Text = "0";
                    }
                }
                txtCantidad.Text = myReader.GetDouble(myReader.GetOrdinal("cantidad")).ToString();
                txtGlosa1.Text = myReader.GetString(myReader.GetOrdinal("glosa1"));
                txtGlosa2.Text = myReader.GetString(myReader.GetOrdinal("glosa2"));
                txtGlosa3.Text = myReader.GetString(myReader.GetOrdinal("glosa3"));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            conexion.Close();
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
                    string codcia = this._codcia;
                    string alma = this._alma;
                    string secsoli = this._secsoli;
                    string solalma = this._solalma;
                    string codarti;
                    string manual;
                    if (chkRegistro.Checked)
                    {
                        manual = "1";
                        codarti = "";
                    }
                    else
                    {
                        manual = "0";
                        codarti = txtCodigo.Text.Trim();
                    }
                    string desarti = txtDescri.Text.Trim();
                    string unidad = txtUnidad.Text.Trim();
                    string glosa1 = txtGlosa1.Text.Trim();
                    string glosa2 = txtGlosa2.Text.Trim();
                    string glosa3 = txtGlosa3.Text.Trim();
                    float cantidad = float.Parse(txtCantidad.Text);
                    SolAlmaD solalmad = new SolAlmaD();
                    string item;
                    if (this._operacion.Equals("AGREGAR"))
                    {
                        item = solalmad.genCod(codcia, alma, secsoli, solalma);
                    }
                    else
                    {
                        item = txtItem.Text;
                    }
                    solalmad.updSolalmaD(operacion, codcia, alma, secsoli, solalma, item, codarti, manual, cantidad, glosa1,
                        glosa2, glosa3, desarti, unidad);

                    ((ui_updsolialma)FormPadre).ui_listaItem();
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
                ui_viewarti._clasePadre = "ui_solalmad";
                ui_viewarti._condicionAdicional = string.Empty;
                ui_viewarti.BringToFront();
                ui_viewarti.ShowDialog();
                ui_viewarti.Dispose();
            }
        }

        private string valida()
        {
            string valorValida = "G";

            if (chkRegistro.Checked)
            {
                if (txtDescri.Text == string.Empty && valorValida == "G")
                {
                    valorValida = "B";
                    MessageBox.Show("No ha ingresado descripción de Producto", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtDescri.Focus();
                }
            }
            else
            {
                if (txtCodigo.Text == string.Empty && valorValida == "G")
                {
                    valorValida = "B";
                    MessageBox.Show("No ha ingresado Código de Producto", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtCodigo.Focus();
                }
            }
            if (valorValida == "G")
            {
                try
                {
                    float cantidad = float.Parse(txtCantidad.Text);
                    if (cantidad <= 0)
                    {
                        valorValida = "B";
                        MessageBox.Show("Cantidad no válida", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txtCantidad.Text = "0";
                        txtCantidad.Focus();
                    }

                }
                catch (FormatException)
                {
                    valorValida = "B";
                    MessageBox.Show("Cantidad no válida", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtCantidad.Text = "0";
                    txtCantidad.Focus();
                }
            }
            return valorValida;
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
                    txtCodigo.Text = "";
                    lblDescri.Text = "";
                    lblUnidad.Text = "";
                    txtUnidad.Text = "";
                    txtDescri.Text = "";
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

        public void ui_consultaProducto(string codcia, string codarti)
        {
            AlArti alarti = new AlArti();
            string codigo = alarti.ui_getDatos(codcia, codarti, "CODIGO");
            txtCodigo.Text = codigo;
            lblDescri.Text = alarti.ui_getDatos(codcia, codarti, "NOMBRE");
            txtDescri.Text = alarti.ui_getDatos(codcia, codarti, "NOMBRE");
            txtUnidad.Text = alarti.ui_getDatos(codcia, codarti, "UNIDAD");
            lblUnidad.Text = alarti.ui_getDatos(codcia, codarti, "UNIDAD");

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
                toolStripForm.Items[0].Select();
                toolStripForm.Focus();
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
                    //txtGlosa1.Focus();
                    toolStripForm.Items[0].Select();
                    toolStripForm.Focus();
                }
                catch (FormatException exc)
                {
                    MessageBox.Show(exc.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    txtCantidad.Text = "0";
                    txtCantidad.Focus();
                }

            }
        }

        private void chkRegistro_CheckedChanged(object sender, EventArgs e)
        {
            txtCodigo.Text = "";
            lblDescri.Text = "";
            lblUnidad.Text = "";
            txtDescri.Text = "";
            txtUnidad.Text = "";
            if (chkRegistro.Checked == true)
            {

                txtCodigo.Enabled = false;
                lblDescri.Visible = false;
                lblUnidad.Visible = false;
                lblUni.Visible = false;
                txtDescri.Visible = true;
                txtUnidad.Visible = true;
                txtDescri.Focus();
            }
            else
            {
                txtCodigo.Enabled = true;
                lblDescri.Visible = true;
                lblUnidad.Visible = true;
                lblUni.Visible = true;
                txtDescri.Visible = false;
                txtUnidad.Visible = false;
                txtCodigo.Focus();
            }
        }

        private void ui_solalmad_Load(object sender, EventArgs e)
        {

        }

        private void txtDescri_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(Keys.Enter))
            {
                e.Handled = true;
                txtCantidad.Focus();
            }

        }
    }
}