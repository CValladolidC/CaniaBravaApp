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
    public partial class ui_upddestajoplan : Form
    {
        string operacion;
        string idcia;
        string idproddes;
        string messem;
        string anio;
        string idtipoper;
        string idtipocal;
        string idzontra;
        string idtipoplan;
        string emplea;
        string estane;
        string iddestajo;
        string fecha;
        string fechaini;
        string fechafin;
        string modo;

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

        public ui_upddestajoplan()
        {
            InitializeComponent();
        }

        private void ui_upddestajoplan_Load(object sender, EventArgs e)
        {

            MaesGen maesgen = new MaesGen();
            maesgen.listaDetMaesGen("030", cmbTipoSubsi, "B");
            maesgen.listaDetMaesGen("031", cmbTipoNoSubsi, "B");


        }

        private void txtCodigoInterno_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {

                string idcia = this.idcia;
                string idtipoper = this.idtipoper;
                string rucemp = this.emplea;
                string estane = this.estane;
                string cadenaBusqueda = string.Empty;
                this._TextBoxActivo = txtCodigoInterno;
                string condicionAdicional = " and A.idtipoplan='" + @idtipoplan + "' and A.rucemp='" + @rucemp + "' and A.estane='" + @estane + "' ";
                FiltrosMaestros filtros = new FiltrosMaestros();
                filtros.filtrarPerPlan("ui_upddestajoplan", this, txtCodigoInterno, idcia, idtipoper, "V", cadenaBusqueda, condicionAdicional);

            }
        }

        private void ui_actualizaComboBox()
        {
            Funciones funciones = new Funciones();
            string idcia = this.idcia;
            string messem = this.messem;
            string anio = this.anio;
            string idtipoper = this.idtipoper;
            string idtipocal = this.idtipocal;

            string query;
            query = " select A.idconplan as clave,A.desboleta as descripcion from detconplan  A  left join ";
            query = query + " conplan B on A.idtipoplan=B.idtipoplan ";
            query = query + " and A.idconplan=B.idconplan and A.idcia=B.idcia and A.idtipocal=B.idtipocal ";
            query = query + " where A.idcia='" + @idcia + "' and A.idtipoplan='" + @idtipoplan + "' ";
            query = query + " and A.idtipoper='" + @idtipoper + "' and A.idtipocal='" + @idtipocal + "' ";
            query = query + " and B.tipo='O' and A.destajo='NO' and B.idclascol='I' order by 1 asc";

            funciones.listaComboBox(query, cmbConcepto, "B");

            string querydes;
            querydes = " select A.idconplan as clave,A.desboleta as descripcion from detconplan  A  left join ";
            querydes = querydes + " conplan B on A.idtipoplan=B.idtipoplan ";
            querydes = querydes + " and A.idconplan=B.idconplan and A.idcia=B.idcia and A.idtipocal=B.idtipocal ";
            querydes = querydes + " where A.idcia='" + @idcia + "' and A.idtipoplan='" + @idtipoplan + "' ";
            querydes = querydes + " and A.idtipoper='" + @idtipoper + "' and A.idtipocal='" + @idtipocal + "' ";
            querydes = querydes + " and B.tipo='O' and A.destajo='NO' and B.idclascol='D' order by 1 asc";

            funciones.listaComboBox(querydes, cmbConceptoDes, "B");

        }

        private void txtCodigoInterno_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                if (txtCodigoInterno.Text.Trim() != string.Empty)
                {
                    string idcia = this.idcia;
                    string idperplan = txtCodigoInterno.Text.Trim();
                    PerPlan perplan = new PerPlan();
                    string codigoInterno = perplan.ui_getDatosPerPlan(idcia, idperplan, "0");
                    if (codigoInterno == string.Empty)
                    {
                        MessageBox.Show("Código no registrado del trabajador", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txtCodigoInterno.Clear();
                        txtNombres.Clear();
                        txtCodigoInternoAl.Clear();
                        txtNombresAl.Clear();
                        txtCodigoInterno.Focus();

                    }
                    else
                    {
                        txtCodigoInterno.Text = perplan.ui_getDatosPerPlan(idcia, idperplan, "0");
                        txtNombres.Text = perplan.ui_getDatosPerPlan(idcia, idperplan, "3");

                        txtCodigoInternoAl.Text = perplan.ui_getDatosPerPlan(idcia, idperplan, "0");
                        txtNombresAl.Text = perplan.ui_getDatosPerPlan(idcia, idperplan, "3");
                        ui_consultaPreDesPlan(idperplan);
                        e.Handled = true;
                        txtCodigoInternoAl.Focus();
                    }

                }
                else
                {
                    txtCodigoInterno.Clear();
                    txtNombres.Clear();

                    txtCodigoInternoAl.Clear();
                    txtNombresAl.Clear();

                    txtCodigoInterno.Focus();
                }
            }
        }

        public void setValores(string idcia, string idproddes, string messem, string anio, string idtipoper, string idtipocal, string idzontra, string idtipoplan, string emplea, string estane, string iddestajo, string fecha, string modo, string fechaini, string fechafin)
        {
            this.idcia = idcia;
            this.idproddes = idproddes;
            this.messem = messem;
            this.anio = anio;
            this.idtipoper = idtipoper;
            this.idtipocal = idtipocal;
            this.idzontra = idzontra;
            this.idtipoplan = idtipoplan;
            this.emplea = emplea;
            this.estane = estane;
            this.iddestajo = iddestajo;
            this.fecha = fecha;
            this.modo = modo;
            this.fechaini = fechaini;
            this.fechafin = fechafin;
        }

        public void ui_newDestajo()
        {
            this.operacion = "AGREGAR";
            txtCodigoInterno.Enabled = true;
            lblF2.Visible = true;
            pictureBoxBuscar.Visible = true;
            txtCodigoInterno.Clear();
            txtNombres.Clear();
            txtCodigoInternoAl.Clear();
            txtNombresAl.Clear();


            txtCantidad.Text = "0";
            txtPrecio.Text = "0";
            txtTotal.Text = "0";
            txtGlosa.Text = "";
            grpDe.Visible = true;
            grpAl.Visible = true;

            if (this.modo.Equals("A"))
            {
                txtCantidad.Visible = false;
                txtPrecio.Visible = false;
                txtTotal.Visible = false;
                txtGlosa.Visible = false;

                lblPrecio.Visible = false;
                lblImporte.Visible = false;
                lblCantidad.Visible = false;
                lblGlosa.Visible = false;
            }
            else
            {
                txtCantidad.Visible = true;
                txtPrecio.Visible = true;
                txtTotal.Visible = true;
                txtGlosa.Visible = true;
                lblCantidad.Visible = true;
                lblPrecio.Visible = true;
                lblImporte.Visible = true;
                lblGlosa.Visible = true;
            }

            txtDiasEfeLab.Text = "0";
            txtDiasDom.Text = "0";

            chkSub.Checked = false;
            cmbTipoSubsi.Enabled = false;
            txtCITT.Enabled = false;
            txtInicioSubsi.Enabled = false;
            txtFinSubsi.Enabled = false;
            txtDiasSubsiCITT.Enabled = false;

            chkNoSub.Checked = false;
            cmbTipoNoSubsi.Enabled = false;
            txtInicioNoSubsi.Enabled = false;
            txtFinNoSubsi.Enabled = false;

            cmbTipoSubsi.Text = "";
            txtCITT.Clear();
            txtInicioSubsi.Clear();
            txtFinSubsi.Clear();
            txtDiasSubsiCITT.Text = "0";
            txtDiasSubsi.Text = "0";

            cmbTipoNoSubsi.Text = "";
            txtInicioNoSubsi.Clear();
            txtFinNoSubsi.Clear();
            txtDiasNoSubsi.Text = "0";

            ui_calculaDias();

            chkAdi.Checked = false;
            cmbConcepto.Enabled = false;
            cmbTipoCalculo.Enabled = false;
            txtValor.Enabled = false;

            chkDes.Checked = false;
            cmbConceptoDes.Enabled = false;
            cmbTipoCalculoDes.Enabled = false;
            txtValorDes.Enabled = false;

            ui_actualizaComboBox();
            txtValor.Text = "0";
            txtValorDes.Text = "0";

            txtCodigoInterno.Focus();

        }

        private void ui_calculaDias()
        {
            try
            {
                string idcia = this.idcia;
                string messem = this.messem;
                string anio = this.anio;
                string idtipoper = this.idtipoper;
                string idtipocal = this.idtipocal;

                CalPlan calplan = new CalPlan();
                int dias = int.Parse(calplan.getDatosCalPlan(messem, anio, idtipoper, idcia, idtipocal, "DIAS_FI_FF"));
                int nDiasDom = int.Parse(calplan.getDatosCalPlan(messem, anio, idtipoper, idcia, idtipocal, "DIASDOM"));
                int nDiasNoSubsi = int.Parse(txtDiasNoSubsi.Text);
                int nDiasSubsi = int.Parse(txtDiasSubsi.Text);

                if (dias >= nDiasSubsi - nDiasDom - nDiasNoSubsi)
                {
                    int nDiasEfeLab = dias - nDiasSubsi - nDiasDom - nDiasNoSubsi;
                    txtDiasEfeLab.Text = Convert.ToString(nDiasEfeLab);
                    txtDiasDom.Text = Convert.ToString(nDiasDom);
                    txtDiasSubsi.Text = Convert.ToString(nDiasSubsi);
                    txtDiasNoSubsi.Text = Convert.ToString(nDiasNoSubsi);
                    txtDiasTotal.Text = Convert.ToString(dias);
                }
                else
                {
                    txtDiasEfeLab.Text = "0";
                    txtDiasDom.Text = "0";
                    txtDiasSubsi.Text = "0";
                    txtDiasNoSubsi.Text = "0";
                    txtDiasTotal.Text = "0";
                }
            }
            catch (FormatException)
            {
                txtDiasEfeLab.Text = "0";
                txtDiasDom.Text = "0";
                txtDiasSubsi.Text = "0";
                txtDiasNoSubsi.Text = "0";
                txtDiasTotal.Text = "0";
            }
        }

        public void ui_loadDatos(string idperplan, string cantidad, string precio, string total, string glosa)
        {

            PerPlan perplan = new PerPlan();
            ui_actualizaComboBox();
            this.operacion = "EDITAR";
            grpDe.Enabled = false;
            grpAl.Enabled = false;
            txtCodigoInterno.Text = idperplan;
            txtNombres.Text = perplan.ui_getDatosPerPlan(this.idcia, idperplan, "3");
            txtCodigoInternoAl.Text = idperplan;
            txtNombresAl.Text = perplan.ui_getDatosPerPlan(this.idcia, idperplan, "3");
            txtCantidad.Text = cantidad;
            txtPrecio.Text = precio;
            txtTotal.Text = total;
            txtGlosa.Text = glosa;
            ui_consultaPreDesPlan(idperplan);
            txtCantidad.Focus();

        }

        public void ui_consultaPreDesPlan(string idperplan)
        {

            string squery;
            MaesGen maesgen = new MaesGen();

            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();

            squery = "SELECT * from predesplan where idperplan='" + @idperplan + "' and idcia='" + @idcia + "'";
            squery = squery + " and anio='" + @anio + "' and messem='" + @messem + "' and idtipoper='" + @idtipoper + "' ";
            squery = squery + " and idtipocal='" + @idtipocal + "' and idtipoplan='" + @idtipoplan + "' ;";

            SqlCommand myCommand = new SqlCommand(squery, conexion);
            SqlDataReader myReader = myCommand.ExecuteReader();

            if (myReader.Read())
            {

                txtDiasEfeLab.Text = myReader["diasefelab"].ToString();
                txtDiasDom.Text = myReader["diasdom"].ToString();
                txtDiasTotal.Text = myReader["diastotal"].ToString();

                if (myReader["subsi"].Equals("1"))
                {

                    chkSub.Checked = true;
                    maesgen.consultaDetMaesGen("030", myReader["motivosubsi"].ToString(), cmbTipoSubsi);
                    txtCITT.Text = myReader["citt"].ToString();
                    txtInicioSubsi.Text = myReader["fechainisubsi"].ToString();
                    txtFinSubsi.Text = myReader["fechafinsubsi"].ToString();
                    txtDiasSubsi.Text = myReader["diassubsi"].ToString();
                    txtDiasSubsiCITT.Text = myReader["diascitt"].ToString();
                    cmbTipoSubsi.Enabled = true;
                    txtCITT.Enabled = true;
                    txtInicioSubsi.Enabled = true;
                    txtFinSubsi.Enabled = true;
                    txtDiasSubsiCITT.Enabled = true;
                }
                else
                {
                    chkSub.Checked = false;
                    cmbTipoSubsi.Text = "";
                    txtCITT.Clear();
                    txtInicioSubsi.Clear();
                    txtFinSubsi.Clear();
                    txtDiasSubsi.Text = "0";
                    txtDiasSubsiCITT.Text = "0";
                    cmbTipoSubsi.Enabled = false;
                    txtCITT.Enabled = false;
                    txtInicioSubsi.Enabled = false;
                    txtFinSubsi.Enabled = false;
                    txtDiasSubsiCITT.Enabled = false;

                }

                if (myReader["nosubsi"].Equals("1"))
                {

                    chkNoSub.Checked = true;
                    maesgen.consultaDetMaesGen("031", myReader["motivonosubsi"].ToString(), cmbTipoNoSubsi);
                    txtInicioNoSubsi.Text = myReader["fechaininosubsi"].ToString();
                    txtFinNoSubsi.Text = myReader["fechafinnosubsi"].ToString();
                    txtDiasNoSubsi.Text = myReader["diasnosubsi"].ToString();

                    cmbTipoNoSubsi.Enabled = true;
                    txtInicioNoSubsi.Enabled = true;
                    txtFinNoSubsi.Enabled = true;
                }
                else
                {
                    chkNoSub.Checked = false;
                    cmbTipoNoSubsi.Text = "";
                    txtInicioNoSubsi.Clear();
                    txtFinNoSubsi.Clear();
                    txtDiasNoSubsi.Text = "0";
                    cmbTipoNoSubsi.Enabled = false;
                    txtInicioNoSubsi.Enabled = false;
                    txtFinNoSubsi.Enabled = false;
                }

                if (myReader["adicon"].Equals("1"))
                {
                    chkAdi.Checked = true;

                    string query;
                    query = " select A.idconplan as clave,A.desboleta as descripcion from detconplan  A  left join ";
                    query = query + " conplan B on A.idtipoplan=B.idtipoplan ";
                    query = query + " and A.idconplan=B.idconplan and A.idcia=B.idcia and A.idtipocal=B.idtipocal ";
                    query = query + " where A.idcia='" + @idcia + "' and A.idtipoplan='" + @idtipoplan + "' ";
                    query = query + " and A.idtipoper='" + @idtipoper + "' and A.idtipocal='" + @idtipocal + "' ";
                    query = query + " and A.idconplan='" + @myReader["idconplan"] + "' ";

                    Funciones funciones = new Funciones();
                    funciones.consultaComboBox(query, cmbConcepto);

                    if (myReader["adicon"].Equals("F"))
                    {
                        cmbTipoCalculo.Text = "F   FORMULA";

                    }
                    else
                    {
                        cmbTipoCalculo.Text = "V   VALOR";
                    }

                    txtValor.Text = myReader["valor"].ToString();
                    cmbConcepto.Enabled = true;
                    cmbTipoCalculo.Enabled = true;
                    txtValor.Enabled = true;
                }
                else
                {
                    chkAdi.Checked = false;
                    cmbConcepto.Text = "";
                    cmbTipoCalculo.Text = "F   FORMULA";
                    txtValor.Text = "0";
                    cmbConcepto.Enabled = false;
                    cmbTipoCalculo.Enabled = false;
                    txtValor.Enabled = false;
                }

                if (myReader["descon"].Equals("1"))
                {
                    chkDes.Checked = true;

                    string query;
                    query = " select A.idconplan as clave,A.desboleta as descripcion from detconplan  A  left join ";
                    query = query + " conplan B on A.idtipoplan=B.idtipoplan ";
                    query = query + " and A.idconplan=B.idconplan and A.idcia=B.idcia and A.idtipocal=B.idtipocal ";
                    query = query + " where A.idcia='" + @idcia + "' and A.idtipoplan='" + @idtipoplan + "' ";
                    query = query + " and A.idtipoper='" + @idtipoper + "' and A.idtipocal='" + @idtipocal + "' ";
                    query = query + " and A.idconplan='" + @myReader["idconplandes"] + "' ";

                    Funciones funciones = new Funciones();
                    funciones.consultaComboBox(query, cmbConceptoDes);

                    if (myReader["descon"].Equals("F"))
                    {
                        cmbTipoCalculoDes.Text = "F   FORMULA";
                    }
                    else
                    {
                        cmbTipoCalculoDes.Text = "V   VALOR";
                    }

                    txtValorDes.Text = myReader["valordes"].ToString();
                    cmbConceptoDes.Enabled = true;
                    cmbTipoCalculoDes.Enabled = true;
                    txtValorDes.Enabled = true;
                }
                else
                {
                    chkDes.Checked = false;
                    cmbConceptoDes.Text = "";
                    cmbTipoCalculoDes.Text = "F   FORMULA";
                    txtValorDes.Text = "0";
                    cmbConceptoDes.Enabled = false;
                    cmbTipoCalculoDes.Enabled = false;
                    txtValorDes.Enabled = false;
                }
            }
        }

        private float ui_calculaTotal(float cantidad, float precio)
        {
            float resultado = 0;
            resultado = cantidad * precio;
            return resultado;
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
                    txtGlosa.Focus();
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

        private void txtTotal_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {

            UtileriasFechas utileriasfechas = new UtileriasFechas();
            Funciones funciones = new Funciones();

            string idperplan;
            string valida = "B";

            string operacion = this.operacion;
            string iddestajo = this.iddestajo;
            string idcia = this.idcia;
            string idproddes = this.idproddes;
            string messem = this.messem;
            string anio = this.anio;
            string idtipoper = this.idtipoper;
            string idtipocal = this.idtipocal;
            string idzontra = this.idzontra;
            string emplea = this.emplea;
            string estane = this.estane;
            string idtipoplan = this.idtipoplan;
            string fecha = this.fecha;
            string fechaini = this.fechaini;
            string fechafin = this.fechafin;
            string codvar = string.Empty;

            valida = ui_validar();
            if (valida.Equals("G"))
            {
                string idperplanini = txtCodigoInterno.Text;
                string idperplanfin = txtCodigoInternoAl.Text;

                string subsi;
                if (chkSub.Checked)
                    subsi = "1";
                else
                    subsi = "0";

                string motivoSubsi = funciones.getValorComboBox(cmbTipoSubsi, 4);
                string citt = txtCITT.Text.Trim();
                string fechaInicioSubsi = txtInicioSubsi.Text;
                string fechaFinSubsi = txtFinSubsi.Text;

                string nosubsi;

                if (chkNoSub.Checked)
                    nosubsi = "1";
                else
                    nosubsi = "0";

                string motivoNoSubsi = funciones.getValorComboBox(cmbTipoNoSubsi, 4);
                string fechaInicioNoSubsi = txtInicioNoSubsi.Text;
                string fechaFinNoSubsi = txtFinNoSubsi.Text;


                txtDiasSubsi.Text = Convert.ToString(utileriasfechas.diferenciaEntreFechas(txtInicioSubsi.Text, txtFinSubsi.Text));
                txtDiasNoSubsi.Text = Convert.ToString(utileriasfechas.diferenciaEntreFechas(txtInicioNoSubsi.Text, txtFinNoSubsi.Text));

                string diasefelab = txtDiasEfeLab.Text;
                string diasdom = txtDiasDom.Text;
                string diasNoSubsi = txtDiasNoSubsi.Text;
                string diasSubsi = txtDiasSubsi.Text;
                string diasCitt = txtDiasSubsiCITT.Text;
                string diastotal = txtDiasTotal.Text;

                float cantidad = float.Parse(txtCantidad.Text);
                float precio = float.Parse(txtPrecio.Text);
                float subtotal = cantidad * precio;
                float movilidad = 0;
                float refrigerio = 0;
                float adicional = 0;
                float total = subtotal + movilidad + refrigerio + adicional;
                string glosa = txtGlosa.Text.Trim();

                string adicon;
                string idconplan;
                string tipocalculo;
                string valor;

                if (chkAdi.Checked)
                {
                    adicon = "1";
                    idconplan = funciones.getValorComboBox(cmbConcepto, 4);
                    tipocalculo = funciones.getValorComboBox(cmbTipoCalculo, 1);
                    if (tipocalculo.Equals("F"))
                    {
                        valor = "0";
                    }
                    else
                    {
                        valor = txtValor.Text;
                    }
                }
                else
                {
                    adicon = "0";
                    idconplan = string.Empty;
                    tipocalculo = string.Empty;
                    valor = "0";
                }

                string descon;
                string idconplandes;
                string tipocalculodes;
                string valordes;

                if (chkDes.Checked)
                {
                    descon = "1";
                    idconplandes = funciones.getValorComboBox(cmbConceptoDes, 4);
                    tipocalculodes = funciones.getValorComboBox(cmbTipoCalculoDes, 1);
                    if (tipocalculodes.Equals("F"))
                    {
                        valordes = "0";
                    }
                    else
                    {
                        valordes = txtValorDes.Text;
                    }
                }
                else
                {
                    descon = "0";
                    idconplandes = string.Empty;
                    tipocalculodes = string.Empty;
                    valordes = "0";
                }

                string query = " Select A.idperplan from perplan A ";
                query = query + " left join view_perlab F on A.idcia=F.idcia ";
                query = query + " and A.idperplan=F.idperplan left join perlab G ";
                query = query + " on F.idcia=G.idcia and F.idperplan=G.idperplan ";
                query = query + " and F.idperlab=G.idperlab ";
                query = query + " where A.idcia='" + @idcia + "' and A.idtipoper='" + @idtipoper + "' ";
                query = query + " and A.idtipoplan='" + @idtipoplan + "' and A.rucemp='" + @emplea + "'";
                query = query + " and A.estane='" + @estane + "' and G.stateperlab='V' and ";
                query = query + " A.idperplan between '" + @idperplanini + "' and '" + @idperplanfin + "' ";
                query = query + " order by idperplan asc;";

                SqlConnection conexion = new SqlConnection();
                conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
                conexion.Open();

                DataTable dtperplan = new DataTable();
                SqlDataAdapter da_dtperplan = new SqlDataAdapter();
                da_dtperplan.SelectCommand = new SqlCommand(query, conexion);
                da_dtperplan.Fill(dtperplan);

                foreach (DataRow row_dtperplan in dtperplan.Rows)
                {
                    idperplan = row_dtperplan["idperplan"].ToString();
                    Destajo destajo = new Destajo();

                    destajo.actualizarDestajo("P", operacion, idcia, idperplan, messem, anio, idtipocal, idtipoper, fecha,
                        idproddes, idzontra, cantidad, precio, subtotal, movilidad, refrigerio, adicional, total,
                        idtipoplan, emplea, estane, iddestajo, glosa, codvar);

                    PreDesPlan predesplan = new PreDesPlan();
                    predesplan.actualizarPreDesPlan(idperplan, idcia, anio, messem, idtipoper, idtipocal, idtipoplan,
                        subsi, motivoSubsi, citt, fechaInicioSubsi, fechaFinSubsi, diasCitt, diasSubsi, nosubsi,
                        motivoNoSubsi, fechaInicioNoSubsi, fechaFinNoSubsi, diasNoSubsi, adicon, idconplan,
                        tipocalculo, valor, diasefelab, diasdom, diastotal, descon, idconplandes, tipocalculodes, valordes);

                }

                ((ui_destajoPeriodo)FormPadre).btnActualizar.PerformClick();
                if (operacion.Equals("AGREGAR"))
                {
                    this.ui_newDestajo();
                }
                else
                {
                    this.Close();
                }
            }

        }

        private string ui_validar()
        {

            string valida = "G";
            try
            {


                MaesGen maesgen = new MaesGen();
                UtileriasFechas utileriasfechas = new UtileriasFechas();
                Funciones funciones = new Funciones();

                string operacion = this.operacion;
                string iddestajo = this.iddestajo;
                string idcia = this.idcia;
                string idproddes = this.idproddes;
                string messem = this.messem;
                string anio = this.anio;
                string idtipoper = this.idtipoper;
                string idtipocal = this.idtipocal;
                string idzontra = this.idzontra;
                string emplea = this.emplea;
                string estane = this.estane;
                string idtipoplan = this.idtipoplan;
                string fecha = this.fecha;
                string fechaini = this.fechaini;
                string fechafin = this.fechafin;
                float cantidad = float.Parse(txtCantidad.Text);
                float precio = float.Parse(txtPrecio.Text);


                if (txtCodigoInterno.Text == string.Empty && valida == "G")
                {
                    MessageBox.Show("No ha especificado Código de Trabajador Inicial", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    valida = "B";
                    txtCodigoInterno.Focus();
                }

                if (txtCodigoInternoAl.Text == string.Empty && valida == "G")
                {
                    MessageBox.Show("No ha especificado Código de Trabajador Final", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    valida = "B";
                    txtCodigoInternoAl.Focus();
                }

                if (chkSub.Checked)
                {
                    if (cmbTipoSubsi.Text == string.Empty && valida == "G")
                    {
                        valida = "B";
                        MessageBox.Show("No ha seleccionado Tipo en días subsidiados", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        cmbTipoSubsi.Focus();
                    }

                    if (cmbTipoSubsi.Text != string.Empty && valida == "G")
                    {
                        string resultado = maesgen.verificaComboBoxMaesGen("030", cmbTipoSubsi.Text.Trim());
                        if (resultado.Trim() == string.Empty)
                        {
                            valida = "B";
                            MessageBox.Show("Dato incorrecto en Tipo en días subsidiados", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            cmbTipoSubsi.Focus();
                        }
                    }

                    if (txtCITT.Text == string.Empty && valida == "G")
                    {
                        valida = "B";
                        MessageBox.Show("CITT no ingresado", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txtCITT.Focus();
                    }

                    if (UtileriasFechas.IsDate(txtInicioSubsi.Text) == false && valida == "G")
                    {
                        valida = "B";
                        MessageBox.Show("Fecha de Inicio no válida", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txtInicioSubsi.Focus();
                    }

                    if (UtileriasFechas.IsDate(txtFinSubsi.Text) == false && valida == "G")
                    {
                        valida = "B";
                        MessageBox.Show("Fecha de Fin no válida", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txtFinSubsi.Focus();
                    }

                    if (UtileriasFechas.compararFecha(txtFinSubsi.Text, "<", txtInicioSubsi.Text) && valida == "G")
                    {
                        valida = "B";
                        MessageBox.Show("Fecha de Fin no válida", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txtFinSubsi.Focus();
                    }

                    if (int.Parse(txtDiasSubsi.Text) <= 0 && valida == "G")
                    {
                        valida = "B";
                        MessageBox.Show("Dato incorrecto en días subsidiados", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txtDiasSubsi.Focus();
                    }

                    if (int.Parse(txtDiasSubsi.Text) > int.Parse(txtDiasSubsiCITT.Text) && valida == "G")
                    {
                        valida = "B";
                        MessageBox.Show("La cantidad de días subsidiados no puede ser mayor a " + txtDiasSubsiCITT.Text.Trim(), "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txtDiasSubsi.Focus();
                    }

                }

                if (chkNoSub.Checked)
                {
                    if (cmbTipoNoSubsi.Text == string.Empty && valida == "G")
                    {
                        valida = "B";
                        MessageBox.Show("No ha seleccionado Tipo en días No Subsidiados", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        cmbTipoNoSubsi.Focus();
                    }

                    if (cmbTipoNoSubsi.Text != string.Empty && valida == "G")
                    {
                        string resultado = maesgen.verificaComboBoxMaesGen("031", cmbTipoNoSubsi.Text.Trim());
                        if (resultado.Trim() == string.Empty)
                        {
                            valida = "B";
                            MessageBox.Show("Dato incorrecto en Tipo en días No Subsidiados", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            cmbTipoNoSubsi.Focus();
                        }
                    }

                    if (UtileriasFechas.IsDate(txtInicioNoSubsi.Text) == false && valida == "G")
                    {
                        valida = "B";
                        MessageBox.Show("Fecha de Inicio no válida", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txtInicioNoSubsi.Focus();
                    }

                    if (UtileriasFechas.IsDate(txtFinNoSubsi.Text) == false && valida == "G")
                    {
                        valida = "B";
                        MessageBox.Show("Fecha de Fin no válida", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txtFinNoSubsi.Focus();
                    }

                    if (UtileriasFechas.compararFecha(txtFinNoSubsi.Text, "<", txtInicioNoSubsi.Text) && valida == "G")
                    {
                        valida = "B";
                        MessageBox.Show("Fecha de Fin no válida", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txtFinNoSubsi.Focus();
                    }
                }

                if (this.modo.Equals("M"))
                {
                    if (cantidad <= 0 && valida == "G")
                    {
                        MessageBox.Show("Cantidad incorrecta", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        valida = "B";
                        txtCantidad.Focus();
                    }

                    if (precio <= 0 && valida == "G")
                    {
                        MessageBox.Show("Precio incorrecto", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        valida = "B";
                        txtPrecio.Focus();
                    }
                }

                string idconplan = funciones.getValorComboBox(cmbConcepto, 4);
                string tipocalculo = funciones.getValorComboBox(cmbTipoCalculo, 1);
                float valor = float.Parse(txtValor.Text);

                if (chkAdi.Checked)
                {
                    if (valida == "G" && idconplan.Trim() == string.Empty)
                    {
                        MessageBox.Show("No ha seleccionado Concepto", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        valida = "B";
                    }

                    if (tipocalculo == string.Empty && valida == "G")
                    {
                        MessageBox.Show("No ha seleccionado el Tipo de Cálculo", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        valida = "B";
                    }

                    if (valida == "G")
                    {
                        DetConPlan detconplan = new DetConPlan();
                        string automatico = detconplan.getDetConPlan(idtipoplan, idconplan, idtipoper, idcia, idtipocal, "AUTOMATICO");

                        if (automatico == "N" && tipocalculo == "F")
                        {
                            MessageBox.Show("No existe Fórmula asociada al concepto seleccionado", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            cmbTipoCalculo.Text = "V   VALOR";
                            valida = "B";
                        }
                        else
                        {
                            if (tipocalculo == "V" && valor == 0)
                            {
                                MessageBox.Show("El valor no puede ser cero", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                valida = "B";
                                txtValor.Focus();
                            }

                        }
                    }

                }


                string idconplandes = funciones.getValorComboBox(cmbConceptoDes, 4);
                string tipocalculodes = funciones.getValorComboBox(cmbTipoCalculoDes, 1);
                float valordes = float.Parse(txtValorDes.Text);

                if (chkDes.Checked)
                {
                    if (valida == "G" && idconplandes.Trim() == string.Empty)
                    {
                        MessageBox.Show("No ha seleccionado Concepto de Descuento", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        valida = "B";
                    }

                    if (tipocalculodes == string.Empty && valida == "G")
                    {
                        MessageBox.Show("No ha seleccionado el Tipo de Cálculo de Descuento", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        valida = "B";
                    }

                    if (valida == "G")
                    {
                        DetConPlan detconplan = new DetConPlan();
                        string automatico = detconplan.getDetConPlan(idtipoplan, idconplan, idtipoper, idcia, idtipocal, "AUTOMATICO");

                        if (automatico == "N" && tipocalculodes == "F")
                        {
                            MessageBox.Show("No existe Fórmula asociada al concepto de Descuento seleccionado", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            cmbTipoCalculoDes.Text = "V   VALOR";
                            valida = "B";
                        }
                        else
                        {
                            if (tipocalculodes == "V" && valordes == 0)
                            {
                                MessageBox.Show("El valor de Descuento no puede ser cero", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                valida = "B";
                                txtValorDes.Focus();
                            }

                        }
                    }

                }

            }
            catch (FormatException ex)
            {

                MessageBox.Show(ex.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                valida = "B";
            }
            return valida;
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtGlosa_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                e.Handled = true;
                toolStripForm.Items[0].Select();
                toolStripForm.Focus();
            }
        }

        private void txtCodigoInternoAl_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                if (txtCodigoInternoAl.Text.Trim() != string.Empty)
                {
                    string idcia = this.idcia;
                    string idperplan = txtCodigoInternoAl.Text.Trim();
                    PerPlan perplan = new PerPlan();
                    string codigoInterno = perplan.ui_getDatosPerPlan(idcia, idperplan, "0");
                    if (codigoInterno == string.Empty)
                    {
                        MessageBox.Show("Código no registrado del trabajador", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txtCodigoInternoAl.Clear();
                        txtNombresAl.Clear();

                        txtCodigoInternoAl.Clear();
                        txtNombresAl.Clear();
                        txtCodigoInterno.Focus();

                    }
                    else
                    {
                        txtCodigoInternoAl.Text = perplan.ui_getDatosPerPlan(idcia, idperplan, "0");
                        txtNombresAl.Text = perplan.ui_getDatosPerPlan(idcia, idperplan, "3");
                        ui_consultaPreDesPlan(idperplan);
                        e.Handled = true;
                        txtCantidad.Focus();
                    }

                }
                else
                {
                    txtCodigoInternoAl.Clear();
                    txtNombresAl.Clear();

                    txtCodigoInternoAl.Focus();
                }
            }
        }

        private void txtCodigoInternoAl_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {

                string idcia = this.idcia;
                string idtipoper = this.idtipoper;
                string rucemp = this.emplea;
                string estane = this.estane;
                string cadenaBusqueda = string.Empty;
                this._TextBoxActivo = txtCodigoInternoAl;
                string condicionAdicional = " and A.idtipoplan='" + @idtipoplan + "' and A.rucemp='" + @rucemp + "' and A.estane='" + @estane + "' ";
                FiltrosMaestros filtros = new FiltrosMaestros();
                filtros.filtrarPerPlan("ui_upddestajoplan", this, txtCodigoInternoAl, idcia, idtipoper, "V", cadenaBusqueda, condicionAdicional);

            }
        }

        private void txtDiasSubsi_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }

        private void label14_Click(object sender, EventArgs e)
        {

        }

        private void chkSub_CheckedChanged(object sender, EventArgs e)
        {
            if (chkSub.Checked)
            {

                cmbTipoSubsi.Enabled = true;
                txtCITT.Enabled = true;
                txtInicioSubsi.Enabled = true;
                txtFinSubsi.Enabled = true;
                txtDiasSubsiCITT.Enabled = true;
            }
            else
            {
                cmbTipoSubsi.Enabled = false;
                txtCITT.Enabled = false;
                txtInicioSubsi.Enabled = false;
                txtFinSubsi.Enabled = false;
                txtDiasSubsiCITT.Enabled = false;

                cmbTipoSubsi.Text = "";
                txtCITT.Clear();
                txtInicioSubsi.Clear();
                txtFinSubsi.Clear();
                txtDiasSubsiCITT.Text = "0";
                txtDiasSubsi.Text = "0";

            }
        }

        private void grpAl_Enter(object sender, EventArgs e)
        {

        }

        private void txtDiasDom_TextChanged(object sender, EventArgs e)
        {

        }

        private void chkNoSub_CheckedChanged(object sender, EventArgs e)
        {
            if (chkNoSub.Checked)
            {

                cmbTipoNoSubsi.Enabled = true;
                txtInicioNoSubsi.Enabled = true;
                txtFinNoSubsi.Enabled = true;
            }
            else
            {

                cmbTipoNoSubsi.Enabled = false;
                txtInicioNoSubsi.Enabled = false;
                txtFinNoSubsi.Enabled = false;

                cmbTipoNoSubsi.Text = "";
                txtInicioNoSubsi.Clear();
                txtFinNoSubsi.Clear();
                txtDiasNoSubsi.Text = "0";

            }
        }

        private void cmbTipoSubsi_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                if (cmbTipoSubsi.Text != String.Empty)
                {
                    MaesGen maesgen = new MaesGen();
                    maesgen.validarDetMaesGen("030", cmbTipoSubsi, cmbTipoSubsi.Text);
                }
                e.Handled = true;
                txtCITT.Focus();

            }
        }

        private void txtCITT_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {

                e.Handled = true;
                txtInicioSubsi.Focus();


            }
        }

        private void txtInicioSubsi_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                if (UtileriasFechas.IsDate(txtInicioSubsi.Text))
                {

                    if (UtileriasFechas.compararFecha(txtInicioSubsi.Text, ">=", this.fechaini) && UtileriasFechas.compararFecha(txtInicioSubsi.Text, "<=", this.fechafin))
                    {
                        txtFinSubsi.Text = txtInicioSubsi.Text;
                        UtileriasFechas utileriasfechas = new UtileriasFechas();
                        txtDiasSubsi.Text = Convert.ToString(utileriasfechas.diferenciaEntreFechas(txtInicioSubsi.Text, txtFinSubsi.Text));
                        txtDiasSubsiCITT.Text = Convert.ToString(utileriasfechas.diferenciaEntreFechas(txtInicioSubsi.Text, txtFinSubsi.Text));
                        ui_calculaDias();
                        e.Handled = true;
                        txtFinSubsi.Focus();

                    }
                    else
                    {

                        MessageBox.Show("La fecha de inicio no se encuentra dentro del rango del periodo", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        e.Handled = true;
                        txtInicioSubsi.Clear();
                        txtFinSubsi.Clear();
                        txtDiasSubsi.Clear();
                        txtDiasSubsiCITT.Clear();
                        txtInicioSubsi.Focus();
                    }
                }
                else
                {
                    MessageBox.Show("Fecha de Inicio no válida", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    e.Handled = true;
                    txtInicioSubsi.Clear();
                    txtFinSubsi.Clear();
                    txtDiasSubsi.Clear();
                    txtDiasSubsiCITT.Clear();
                    txtInicioSubsi.Focus();

                }
            }

        }

        private void txtFinSubsi_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                if (UtileriasFechas.IsDate(txtFinSubsi.Text))
                {
                    if (UtileriasFechas.compararFecha(txtFinSubsi.Text, ">=", this.fechaini) && UtileriasFechas.compararFecha(txtFinSubsi.Text, "<=", this.fechafin))
                    {

                        if (UtileriasFechas.compararFecha(txtInicioSubsi.Text, "<=", txtFinSubsi.Text))
                        {
                            UtileriasFechas utileriasfechas = new UtileriasFechas();
                            txtDiasSubsi.Text = Convert.ToString(utileriasfechas.diferenciaEntreFechas(txtInicioSubsi.Text, txtFinSubsi.Text));
                            txtDiasSubsiCITT.Text = Convert.ToString(utileriasfechas.diferenciaEntreFechas(txtInicioSubsi.Text, txtFinSubsi.Text));
                            ui_calculaDias();
                            e.Handled = true;
                            txtDiasSubsiCITT.Focus();
                        }
                        else
                        {
                            MessageBox.Show("Fecha de Fin no puede ser menor que la Fecha de Inicio", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            e.Handled = true;
                            txtFinSubsi.Clear();
                            txtDiasSubsi.Clear();
                            txtDiasSubsiCITT.Clear();
                            txtFinSubsi.Focus();
                        }

                    }
                    else
                    {

                        MessageBox.Show("La fecha de inicio no se encuentra dentro del rango del periodo", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        e.Handled = true;
                        txtFinSubsi.Clear();
                        txtDiasSubsi.Clear();
                        txtDiasSubsiCITT.Clear();
                        txtFinSubsi.Focus();
                    }
                }
                else
                {
                    MessageBox.Show("Fecha de Fin no válida", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    e.Handled = true;
                    txtFinSubsi.Clear();
                    txtDiasSubsi.Clear();
                    txtDiasSubsiCITT.Clear();
                    txtFinSubsi.Focus();

                }
            }
        }

        private void label12_Click(object sender, EventArgs e)
        {

        }

        private void txtDiasEfeLab_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void cmbTipoNoSubsi_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                if (cmbTipoNoSubsi.Text != String.Empty)
                {
                    MaesGen maesgen = new MaesGen();
                    maesgen.validarDetMaesGen("031", cmbTipoNoSubsi, cmbTipoNoSubsi.Text);
                }
                e.Handled = true;
                txtInicioNoSubsi.Focus();

            }
        }

        private void txtInicioNoSubsi_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                if (UtileriasFechas.IsDate(txtInicioNoSubsi.Text))
                {

                    if (UtileriasFechas.compararFecha(txtInicioNoSubsi.Text, ">=", this.fechaini) && UtileriasFechas.compararFecha(txtInicioNoSubsi.Text, "<=", this.fechafin))
                    {

                        txtFinNoSubsi.Text = txtInicioNoSubsi.Text;
                        UtileriasFechas utileriasfechas = new UtileriasFechas();
                        txtDiasNoSubsi.Text = Convert.ToString(utileriasfechas.diferenciaEntreFechas(txtInicioNoSubsi.Text, txtFinNoSubsi.Text));
                        ui_calculaDias();
                        e.Handled = true;
                        txtFinNoSubsi.Focus();

                    }
                    else
                    {

                        MessageBox.Show("La fecha de inicio no se encuentra dentro del rango del periodo", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        e.Handled = true;
                        txtInicioNoSubsi.Clear();
                        txtFinNoSubsi.Clear();
                        txtDiasNoSubsi.Clear();
                        txtInicioNoSubsi.Focus();
                    }
                }
                else
                {
                    MessageBox.Show("Fecha de Inicio no válida", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    e.Handled = true;
                    txtInicioNoSubsi.Clear();
                    txtFinNoSubsi.Clear();
                    txtDiasNoSubsi.Clear();
                    txtInicioNoSubsi.Focus();

                }
            }
        }

        private void txtFinNoSubsi_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                if (UtileriasFechas.IsDate(txtFinNoSubsi.Text))
                {
                    if (UtileriasFechas.compararFecha(txtFinNoSubsi.Text, ">=", this.fechaini) && UtileriasFechas.compararFecha(txtFinNoSubsi.Text, "<=", this.fechafin))
                    {

                        if (UtileriasFechas.compararFecha(txtInicioNoSubsi.Text, "<=", txtFinNoSubsi.Text))
                        {
                            UtileriasFechas utileriasfechas = new UtileriasFechas();
                            txtDiasNoSubsi.Text = Convert.ToString(utileriasfechas.diferenciaEntreFechas(txtInicioNoSubsi.Text, txtFinNoSubsi.Text));
                            ui_calculaDias();
                            e.Handled = true;
                            txtDiasNoSubsi.Focus();
                        }
                        else
                        {
                            MessageBox.Show("Fecha de Fin no puede ser menor que la Fecha de Inicio", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            e.Handled = true;
                            txtFinNoSubsi.Clear();
                            txtDiasNoSubsi.Clear();
                            txtFinNoSubsi.Focus();
                        }

                    }
                    else
                    {

                        MessageBox.Show("La fecha de inicio no se encuentra dentro del rango del periodo", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        e.Handled = true;
                        txtFinNoSubsi.Clear();
                        txtDiasNoSubsi.Clear();
                        txtFinNoSubsi.Focus();
                    }
                }
                else
                {
                    MessageBox.Show("Fecha de Fin no válida", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    e.Handled = true;
                    txtFinNoSubsi.Clear();
                    txtDiasNoSubsi.Clear();
                    txtFinNoSubsi.Focus();

                }
            }
        }

        private void txtDiasDom_KeyPress(object sender, KeyPressEventArgs e)
        {


        }

        private void chkAdi_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAdi.Checked)
            {

                cmbConcepto.Enabled = true;
                cmbTipoCalculo.Enabled = true;
                txtValor.Enabled = true;
            }
            else
            {

                cmbConcepto.Enabled = false;
                cmbTipoCalculo.Enabled = false;
                txtValor.Enabled = false;

                cmbConcepto.Text = "";
                cmbTipoCalculo.Text = "";
                txtValor.Text = "0";

            }
        }

        private void cmbConcepto_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void cmbTipoCalculo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbTipoCalculo.Text.Substring(0, 1).Equals("F"))
            {
                txtValor.Text = "0";
                txtValor.Enabled = false;
            }
            else
            {
                txtValor.Enabled = true;
            }
        }

        private void txtDiasTotal_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }

        private void txtCantidad_TextChanged(object sender, EventArgs e)
        {

        }

        private void lblPrecio_Click(object sender, EventArgs e)
        {

        }

        private void txtPrecio_TextChanged(object sender, EventArgs e)
        {

        }

        private void lblCantidad_Click(object sender, EventArgs e)
        {

        }

        private void cmbTipoCalculoDes_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbTipoCalculoDes.Text.Substring(0, 1).Equals("F"))
            {
                txtValorDes.Text = "0";
                txtValorDes.Enabled = false;
            }
            else
            {
                txtValorDes.Enabled = true;
            }
        }

        private void chkDes_CheckedChanged(object sender, EventArgs e)
        {
            if (chkDes.Checked)
            {

                cmbConceptoDes.Enabled = true;
                cmbTipoCalculoDes.Enabled = true;
                txtValorDes.Enabled = true;
            }
            else
            {

                cmbConceptoDes.Enabled = false;
                cmbTipoCalculoDes.Enabled = false;
                txtValorDes.Enabled = false;

                cmbConceptoDes.Text = "";
                cmbTipoCalculoDes.Text = "";
                txtValorDes.Text = "0";

            }
        }
    }
}