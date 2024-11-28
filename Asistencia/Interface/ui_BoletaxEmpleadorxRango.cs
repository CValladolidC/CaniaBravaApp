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
using System.Drawing.Printing;

namespace CaniaBrava
{
    public partial class ui_BoletaxEmpleadorxRango : Form
    {
        string _boleta;

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

        public ui_BoletaxEmpleadorxRango()
        {
            InitializeComponent();
        }

        private void txtMesSem_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                Funciones funciones = new Funciones();
                CalPlan calplan = new CalPlan();
                GlobalVariables variables = new GlobalVariables();

                string idcia = variables.getValorCia();
                string idtipoper = funciones.getValorComboBox(cmbTipoTrabajador, 1);
                string periodo = txtMesSem.Text.Trim() + funciones.replicateCadena(" ", 6);
                string messem = periodo.Substring(0, 2);
                string anio = periodo.Substring(3, 4);
                string idtipocal = funciones.getValorComboBox(cmbTipoCal, 1);

                if (calplan.getDatosCalPlan(messem, anio, idtipoper, idcia, idtipocal, "FECHAINI") != "")
                {
                    txtFechaIni.Text = calplan.getDatosCalPlan(messem, anio, idtipoper, idcia, idtipocal, "FECHAINI");
                    txtFechaFin.Text = calplan.getDatosCalPlan(messem, anio, idtipoper, idcia, idtipocal, "FECHAFIN");
                    e.Handled = true;
                    txtCodigoInterno.Focus();
                    ui_mostrarBoletas();


                }
                else
                {
                    MessageBox.Show("El Periodo Laboral no existe", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtFechaIni.Clear();
                    txtFechaFin.Clear();
                    e.Handled = true;
                    txtMesSem.Focus();
                }
            }
        }

        private void radioButtonSiEmp_CheckedChanged(object sender, EventArgs e)
        {
            Funciones funciones = new Funciones();
            GlobalVariables variables = new GlobalVariables();
            string idcia = variables.getValorCia();

            string squery = "SELECT rucemp as clave,razonemp as descripcion FROM emplea WHERE idciafile='" + @idcia + "' order by rucemp asc;";
            funciones.listaComboBox(squery, cmbEmpleador, "");
            cmbEmpleador.Enabled = true;

            string ruccia = funciones.getValorComboBox(cmbEmpleador, 11);
            string squery_esta = "SELECT idestane as clave,desestane as descripcion FROM estane WHERE codemp='" + @ruccia + "' and idciafile='" + @idcia + "' order by idestane asc;";
            funciones.listaComboBox(squery_esta, cmbEstablecimiento, "");
            cmbEmpleador.Focus();
        }

        private void radioButtonNoEmp_CheckedChanged(object sender, EventArgs e)
        {
            Funciones funciones = new Funciones();
            GlobalVariables variables = new GlobalVariables();
            string idcia = variables.getValorCia();
            string ruccia = variables.getValorRucCia();

            string squery = "SELECT ruccia as clave,descia as descripcion FROM ciafile WHERE idcia='" + @idcia + "';";
            funciones.listaComboBox(squery, cmbEmpleador, "");
            cmbEmpleador.Enabled = false;

            string squery_esta = "SELECT idestane as clave,desestane as descripcion FROM estane WHERE codemp='" + @ruccia + "' and idciafile='" + @idcia + "' order by idestane asc;";
            funciones.listaComboBox(squery_esta, cmbEstablecimiento, "");
            cmbEstablecimiento.Focus();
        }

        private void cmbEmpleador_SelectedIndexChanged(object sender, EventArgs e)
        {
            Funciones funciones = new Funciones();
            GlobalVariables variables = new GlobalVariables();
            string idcia = variables.getValorCia();
            string ruccia = funciones.getValorComboBox(cmbEmpleador, 11);
            string squery_esta = "SELECT idestane as clave,desestane as descripcion FROM estane WHERE codemp='" + @ruccia + "' and idciafile='" + @idcia + "' order by idestane asc;";
            funciones.listaComboBox(squery_esta, cmbEstablecimiento, "");
            cmbEstablecimiento.Focus();
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            string strPrinter = this._boleta;
            PrintDialog pd = new PrintDialog();
            pd.PrinterSettings = new PrinterSettings();
            pd.AllowSelection = true;
            pd.AllowSomePages = true;

            if (DialogResult.OK == pd.ShowDialog(this))
            {
                RawPrinterHelper.SendStringToPrinter(pd.PrinterSettings.PrinterName, strPrinter);
            }
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ui_listaTipoCal(string idcia, string idtipoplan)
        {
            Funciones funciones = new Funciones();
            string query;
            query = " SELECT idtipocal as clave,destipocal as descripcion ";
            query = query + " FROM tipocal where idtipocal in (select idtipocal ";
            query = query + " from calcia where idcia='" + @idcia + "' and idtipoplan='" + @idtipoplan + "') ";
            query = query + " order by ordentipocal asc;";
            funciones.listaComboBox(query, cmbTipoCal, "");
        }

        private void ui_mostrarBoletas()
        {
            DataTable boletas = new DataTable();
            string idperplan = "";
            string strPrinter = "";
            int nBetweenBol;
            int nNroBolPag;
            int nrobol = 1;

            Funciones fn = new Funciones();
            GlobalVariables gv = new GlobalVariables();
            Exporta exporta = new Exporta();
            string idtipoper = fn.getValorComboBox(cmbTipoTrabajador, 1);
            string periodo = txtMesSem.Text.Trim() + fn.replicateCadena(" ", 6);
            string messem = periodo.Substring(0, 2);
            string anio = periodo.Substring(3, 4);
            string idtipocal = fn.getValorComboBox(cmbTipoCal, 1);
            string idtipoplan = fn.getValorComboBox(cmbTipoPlan, 3);
            string idcia = gv.getValorCia();
            string rucemp = fn.getValorComboBox(cmbEmpleador, 11);
            string estane = fn.getValorComboBox(cmbEstablecimiento, 4);
            string de_idperplan = txtCodigoInterno.Text;
            string a_idperplan = txtCodigoInternoAl.Text;

            string query = " Select P.idperplan,CONCAT(A.apepat,' ',A.apemat,' , ',A.nombres) as nombre ";
            query = query + " from plan_ P inner join perplan A on P.idcia=A.idcia and P.idperplan=A.idperplan ";
            query = query + " where P.rucemp='" + @rucemp + "' and P.estane='" + @estane + "' and P.anio='" + @anio + "' and P.messem='" + @messem + "' and P.idcia='" + @idcia + "' and P.idtipoper='" + @idtipoper + "' ";
            query = query + " and P.idtipoplan='" + @idtipoplan + "' and P.idtipocal='" + @idtipocal + "' ";
            query = query + " and P.idperplan between '" + @de_idperplan + "' and '" + @a_idperplan + "' order by nombre asc;";

            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();
            SqlDataAdapter da = new SqlDataAdapter();


            da.SelectCommand = new SqlCommand(query, conexion);
            da.Fill(boletas);

            Configsis configsis = new Configsis();
            nBetweenBol = int.Parse(configsis.consultaConfigSis("BETWEENBOL"));
            nNroBolPag = int.Parse(configsis.consultaConfigSis("NROBOLPAG"));


            if (boletas.Rows.Count > 0)
            {
                lblprocesando.Visible = true;
                foreach (DataRow row_boletas in boletas.Rows)
                {

                    idperplan = row_boletas["idperplan"].ToString();
                    Boleta boleta = new Boleta();
                    strPrinter = strPrinter + boleta.generaBoletaTabular(idperplan, anio, messem, idcia, idtipoper, idtipoplan, idtipocal);

                    for (int x = 0; x <= nBetweenBol; x++)
                    {
                        strPrinter = strPrinter + '\n';
                    }
                    if (nrobol >= nNroBolPag)
                    {
                        nrobol = 1;
                        strPrinter = strPrinter + '\f';
                    }
                    else
                    {
                        nrobol++;
                    }

                }
                lblprocesando.Visible = false;

            }

            this._boleta = strPrinter;
            txtBoleta.Text = exporta.Ascii_FromCadena(strPrinter);

        }

        private void cmbTipoTrabajador_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtMesSem.Clear();
            txtFechaIni.Clear();
            txtFechaFin.Clear();
            txtMesSem.Focus();
            ui_mostrarBoletas();
        }

        private void cmbTipoPlan_SelectedIndexChanged(object sender, EventArgs e)
        {
            Funciones funciones = new Funciones();
            GlobalVariables variables = new GlobalVariables();
            string idcia = variables.getValorCia();
            string idtipoplan = funciones.getValorComboBox(cmbTipoPlan, 3);
            ui_listaTipoCal(idcia, idtipoplan);
            txtMesSem.Clear();
            txtFechaIni.Clear();
            txtFechaFin.Clear();
            txtMesSem.Focus();
            ui_mostrarBoletas();
        }

        private void cmbTipoCal_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtMesSem.Clear();
            txtFechaIni.Clear();
            txtFechaFin.Clear();
            txtMesSem.Focus();
            ui_mostrarBoletas();
        }

        private void ui_BoletaxEmpleadorxRango_Load(object sender, EventArgs e)
        {
            Funciones funciones = new Funciones();
            GlobalVariables variables = new GlobalVariables();
            string squery;
            string idcia = variables.getValorCia();
            squery = "SELECT ruccia as clave,descia as descripcion FROM ciafile WHERE idcia='" + @variables.getValorCia() + "';";
            funciones.listaComboBox(squery, cmbEmpleador, "");
            squery = "SELECT idtipoper as clave,destipoper as descripcion FROM tipoper order by 1 asc";
            funciones.listaComboBox(squery, cmbTipoTrabajador, "");
            squery = "SELECT idtipoplan as clave,destipoplan as descripcion FROM tipoplan where idtipoplan in (select idtipoplan from reglabcia where idcia='" + @idcia + "') order by 1 asc;";
            funciones.listaComboBox(squery, cmbTipoPlan, "");
            string idtipoplan = funciones.getValorComboBox(cmbTipoPlan, 3);
            ui_listaTipoCal(idcia, idtipoplan);

        }

        private void txtCodigoInterno_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                GlobalVariables gv = new GlobalVariables();
                Funciones fn = new Funciones();
                string idcia = gv.getValorCia();
                string idtipoper = fn.getValorComboBox(cmbTipoTrabajador, 1);
                string idtipoplan = fn.getValorComboBox(cmbTipoPlan, 3);
                string periodo = txtMesSem.Text.Trim() + fn.replicateCadena(" ", 6);
                string messem = periodo.Substring(0, 2);
                string anio = periodo.Substring(3, 4);
                string idtipocal = fn.getValorComboBox(cmbTipoCal, 1);
                string rucemp = fn.getValorComboBox(cmbEmpleador, 11);
                string estane = fn.getValorComboBox(cmbEstablecimiento, 4);
                string cadenaBusqueda = string.Empty;
                this._TextBoxActivo = txtCodigoInterno;
                string condicionAdicional = " and A.idtipoplan='" + @idtipoplan + "' and A.rucemp='" + @rucemp + "' and A.estane='" + @estane + "' and CONCAT(A.idperplan,A.idcia) in (select CONCAT(idperplan,idcia) from plan_ where idtipoper='" + @idtipoper + "' and messem='" + @messem + "' and anio='" + @anio + "' and idtipocal='" + @idtipocal + "' and idcia='" + @idcia + "' and idtipoplan='" + @idtipoplan + "' ) ";
                FiltrosMaestros filtros = new FiltrosMaestros();
                filtros.filtrarPerPlan("ui_BoletaxEmpleadorxRango", this, txtCodigoInterno, idcia, idtipoper, "V", cadenaBusqueda, condicionAdicional);

            }
        }

        private void txtCodigoInterno_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                if (txtCodigoInterno.Text.Trim() != string.Empty)
                {
                    GlobalVariables gv = new GlobalVariables();
                    string idcia = gv.getValorCia();
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
                ui_mostrarBoletas();
            }
        }

        private void txtCodigoInternoAl_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                GlobalVariables gv = new GlobalVariables();
                Funciones fn = new Funciones();
                string idcia = gv.getValorCia();
                string idtipoper = fn.getValorComboBox(cmbTipoTrabajador, 1);
                string idtipoplan = fn.getValorComboBox(cmbTipoPlan, 3);
                string periodo = txtMesSem.Text.Trim() + fn.replicateCadena(" ", 6);
                string messem = periodo.Substring(0, 2);
                string anio = periodo.Substring(3, 4);
                string idtipocal = fn.getValorComboBox(cmbTipoCal, 1);
                string rucemp = fn.getValorComboBox(cmbEmpleador, 11);
                string estane = fn.getValorComboBox(cmbEstablecimiento, 4);
                string cadenaBusqueda = string.Empty;
                this._TextBoxActivo = txtCodigoInternoAl;
                string condicionAdicional = " and A.idtipoplan='" + @idtipoplan + "' and CONCAT(A.idperplan,A.idcia) in (select CONCAT(idperplan,idcia) from plan_ where idtipoper='" + @idtipoper + "' and messem='" + @messem + "' and anio='" + @anio + "' and idtipocal='" + @idtipocal + "' and idcia='" + @idcia + "' and idtipoplan='" + @idtipoplan + "' ) ";
                FiltrosMaestros filtros = new FiltrosMaestros();
                filtros.filtrarPerPlan("ui_BoletaxEmpleadorxRango", this, txtCodigoInternoAl, idcia, idtipoper, "V", cadenaBusqueda, condicionAdicional);

            }
        }

        private void txtCodigoInternoAl_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                if (txtCodigoInternoAl.Text.Trim() != string.Empty)
                {
                    GlobalVariables gv = new GlobalVariables();
                    string idcia = gv.getValorCia();
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
                    }

                }
                else
                {
                    txtCodigoInternoAl.Clear();
                    txtNombresAl.Clear();

                    txtCodigoInternoAl.Focus();
                }
                ui_mostrarBoletas();
            }
        }

        private void txtCodigoInternoAl_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtCodigoInterno_TextChanged(object sender, EventArgs e)
        {

        }
    }
}