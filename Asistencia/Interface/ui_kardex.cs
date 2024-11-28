using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Configuration;
using System.Data.SqlClient;

namespace CaniaBrava
{
    public partial class ui_kardex : ui_form
    {
        string _codcia;

        private TextBox TextBoxActivo;

        public TextBox _TextBoxActivo
        {
            get { return TextBoxActivo; }
            set { TextBoxActivo = value; }
        }

        public ui_kardex()
        {
            InitializeComponent();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtFechaIni_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                if (UtiFechas.IsDate(txtFechaIni.Text))
                {
                    e.Handled = true;
                    txtFechaFin.Focus();
                }
                else
                {
                    MessageBox.Show("Fecha Inicial no válida", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    e.Handled = true;
                    txtFechaIni.Focus();
                }
            }
        }

        private void txtFechaFin_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                if (UtiFechas.IsDate(txtFechaFin.Text))
                {
                    e.Handled = true;
                    SendKeys.Send("{TAB}");
                }
                else
                {
                    MessageBox.Show("Fecha Final no válida", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    e.Handled = true;
                    txtFechaFin.Focus();
                }
            }
        }

        private void ui_kardex_Load(object sender, EventArgs e)
        {
            Funciones funciones = new Funciones();
            GlobalVariables gv = new GlobalVariables();
            this._codcia = gv.getValorCia();
            string codcia = this._codcia;
            string query = "Select codalma as clave,desalma as descripcion ";
            query = query + "from alalma where codcia='"+@codcia+"' and estado='V' order by 1 asc;";
            funciones.listaComboBox(query, cmbAlmacen, "");
            txtFechaIni.Text = DateTime.Now.ToString("dd/MM/yyyy");
            txtFechaFin.Text = DateTime.Now.ToString("dd/MM/yyyy");
            txtCodigoIni.Clear();
            txtCodigoFin.Clear();
            cmbAgrupar.Text = "FAMILIA DE ARTICULOS";
            cmbTipoKardex.Text = "Detallado";
        }

        private void txtCodigoIni_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(Keys.Enter))
            {
                if (cmbAgrupar.Text.Equals("CODIGO DE ARTICULO"))
                {
                    AlArti alarti = new AlArti();
                    Funciones funciones = new Funciones();
                    string codcia = this._codcia;
                    string alma = funciones.getValorComboBox(cmbAlmacen, 2);
                    string codarti = txtCodigoIni.Text.Trim();
                    string nombre = alarti.ui_getDatos(codcia,codarti, "NOMBRE");
                    if (nombre == string.Empty || codarti == string.Empty)
                    {
                        txtCodigoIni.Clear();
                        txtCodigoFin.Clear();
                        e.Handled = true;
                        txtCodigoIni.Focus();
                    }
                    else
                    {
                        string codigo = alarti.ui_getDatos(codcia,codarti, "CODIGO");
                        txtCodigoIni.Text = codigo;
                        txtCodigoFin.Text = codigo;
                        e.Handled = true;
                        txtCodigoFin.Focus();
                    }
                }
                else
                {
                    txtCodigoFin.Text = txtCodigoIni.Text;
                    e.Handled = true;
                    txtCodigoFin.Focus();
                }
            }
        }

        private void txtCodigoIni_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                string codcia = this._codcia;
                string tipo = cmbAgrupar.Text.Trim();
                string cadena = txtCodigoIni.Text.Trim();
                string query = string.Empty;
                this._TextBoxActivo = txtCodigoIni;
                if (tipo.Equals("FAMILIA DE ARTICULOS"))
                {
                    query = "SELECT clavemaesgen as codigo,desmaesgen as descripcion from maesgen where ";
                    query = query + "idmaesgen='110' order by clavemaesgen asc ";
                    ui_viewmaestros ui_viewmaestros = new ui_viewmaestros();
                    ui_viewmaestros.setData(query, "ui_kardex", "Seleccionar " + tipo);
                    ui_viewmaestros._FormPadre = this;
                    ui_viewmaestros.BringToFront();
                    ui_viewmaestros.ShowDialog();
                    ui_viewmaestros.Dispose();
                }
                else
                {
                    ui_viewarti ui_viewarti = new ui_viewarti();
                    ui_viewarti._FormPadre = this;
                    ui_viewarti._codcia = this._codcia;
                    ui_viewarti._clasePadre = "ui_kardex";
                    ui_viewarti._condicionAdicional = string.Empty;
                    ui_viewarti.BringToFront();
                    ui_viewarti.ShowDialog();
                    ui_viewarti.Dispose();
                }
            }
        }

        private void txtCodigoFin_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(Keys.Enter))
            {
                if (cmbAgrupar.Text.Equals("CODIGO DE ARTICULO"))
                {
                    AlArti alarti = new AlArti();
                    Funciones funciones = new Funciones();
                    string codcia = this._codcia;
                    string alma = funciones.getValorComboBox(cmbAlmacen, 2);
                    string codarti = txtCodigoFin.Text.Trim();
                    string nombre = alarti.ui_getDatos(codcia, codarti, "NOMBRE");
                    if (nombre == string.Empty || codarti == string.Empty)
                    {
                        txtCodigoFin.Clear();
                        e.Handled = true;
                        txtCodigoFin.Focus();
                    }
                    else
                    {
                        string codigo = alarti.ui_getDatos(codcia, codarti, "CODIGO");
                        txtCodigoFin.Text = codigo;
                        e.Handled = true;
                        txtFechaIni.Focus();
                    }
                }
                else
                {
                    e.Handled = true;
                    txtFechaIni.Focus();
                }
            }
        }

        private void txtCodigoFin_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                string codcia = this._codcia;
                string cadena = txtCodigoFin.Text.Trim();
                string tipo = cmbAgrupar.Text.Trim();
                string query = string.Empty;
                this._TextBoxActivo = txtCodigoFin;
                if (tipo.Equals("FAMILIA DE ARTICULOS"))
                {
                    query = "SELECT clavemaesgen as codigo,desmaesgen as descripcion from maesgen where ";
                    query = query + "idmaesgen='110' order by clavemaesgen asc ";
                    ui_viewmaestros ui_viewmaestros = new ui_viewmaestros();
                    ui_viewmaestros.setData(query, "ui_kardex", "Seleccionar " + tipo);
                    ui_viewmaestros._FormPadre = this;
                    ui_viewmaestros.BringToFront();
                    ui_viewmaestros.ShowDialog();
                    ui_viewmaestros.Dispose();
                }
                else
                {
                    ui_viewarti ui_viewarti = new ui_viewarti();
                    ui_viewarti._FormPadre = this;
                    ui_viewarti._codcia = this._codcia;
                    ui_viewarti._clasePadre = "ui_kardex";
                    ui_viewarti._condicionAdicional = string.Empty;
                    ui_viewarti.BringToFront();
                    ui_viewarti.ShowDialog();
                    ui_viewarti.Dispose();
                }
            }
        }

        private void ui_imprimeKardexDetalle(string strTitulo)
        {
            Funciones funciones = new Funciones();
            DataTable dtarti = new DataTable();
            string codcia = this._codcia;
            string alma = funciones.getValorComboBox(cmbAlmacen, 2);
            string codini = txtCodigoIni.Text.Trim();
            string codfin = txtCodigoFin.Text.Trim();
            string fechaini = txtFechaIni.Text;
            string fechafin = txtFechaFin.Text;
            DateTime anterior = DateTime.ParseExact(fechaini, "dd/MM/yyyy", null).AddDays(-1);
            string mesant = anterior.Month.ToString().PadLeft(2, '0');
            string anioant = anterior.Year.ToString();
            DateTime actual = DateTime.ParseExact(fechafin, "dd/MM/yyyy", null);
            string mesact = actual.Month.ToString().PadLeft(2, '0');
            string anioact = actual.Year.ToString();

            string query = string.Empty;
            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();

            string cadcondicion = string.Empty;

            if (cmbAgrupar.Text.Equals("CODIGO DE ARTICULO"))
            {
                cadcondicion = " and A.codarti between '" + @codini + "' and '" + @codfin + "' ";
            }
            else
            {
                cadcondicion = " and A.famarti between '" + @codini + "' and '" + @codfin + "' ";
            }

            query = " select A.codarti,A.desarti,A.famarti,C.desmaesgen as desfamarti,A.grparti,B.desgrparti,A.unidad ";
            query = query + " from alarti A left join grparti B on A.grparti=B.grparti and A.famarti=B.famarti ";
            query = query + " left join maesgen C on C.idmaesgen='110' and A.famarti=C.clavemaesgen ";
            query = query + " where A.codcia='" + @codcia + "' and  A.codarti ";
            query = query + " in (Select codarti from almovd where codcia='" + @codcia + "' and alma='" + @alma + "') " + cadcondicion;
            query = query + " order by C.desmaesgen asc,B.desgrparti asc,A.desarti asc ";

            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = new SqlCommand(query, conexion);
            da.Fill(dtarti);

            string strPrinter = string.Empty;
            strPrinter = strPrinter + strTitulo;

            if (dtarti.Rows.Count > 0)
            {
                foreach (DataRow row_arti in dtarti.Rows)
                {
                    string codarti = row_arti["codarti"].ToString();
                    Alstock alstock = new Alstock();
                    decimal saldoini = decimal.Parse(alstock.ui_getStockAnterior(codcia, alma, codarti, fechaini));
                    decimal saldofin = decimal.Parse(alstock.ui_getStockEnFecha(codcia, alma, codarti, fechafin));
                    decimal entrada = decimal.Parse(alstock.ui_getResMovEnRangoFecha(codcia, alma, "PE", codarti, fechaini, fechafin));
                    decimal salida = decimal.Parse(alstock.ui_getResMovEnRangoFecha(codcia, alma, "PS", codarti, fechaini, fechafin));
                    bool imprime = true;
                    if (chkSinStock.Checked)
                    {
                        if (saldoini + saldofin + entrada + salida > 0)
                        {
                            imprime = true;
                        }
                        else
                        {
                            imprime = false;
                        }
                    }
                    if (imprime)
                    {
                        strPrinter = strPrinter + "\n\n";
                        strPrinter = strPrinter + " CODIGO              :  " + row_arti["codarti"].ToString() + "\n";
                        strPrinter = strPrinter + " NOMBRE DESCRIPTIVO  :  " + row_arti["desarti"].ToString().Trim() + "\n";
                        strPrinter = strPrinter + " FAMILIA             :  " + String.Concat(row_arti["famarti"].ToString(), " ", row_arti["desfamarti"].ToString()) + "\n";
                        strPrinter = strPrinter + " GRUPO DE PRODUCTOS  :  " + String.Concat(row_arti["grparti"].ToString(), " ", row_arti["desgrparti"].ToString()) + "\n";
                        strPrinter = strPrinter + " UNIDAD DE MEDIDA    :  " + row_arti["unidad"].ToString() + "\n";
                        strPrinter = strPrinter + " --------------------------------------------------------------------------------------------------------------------------------------------------------" + "\n\n";
                        strPrinter = strPrinter + " TD" + " " + "  NUMDOC  " + " " + "TM" + " " + "FECHA DOC." + " " + "  NRO.DOCUMENTO " + " " + "    RUC    " + " " + "         RAZON SOCIAL / GLOSA           " + " " + "P.C.UNIT" + " " + " ENTRADAS " + " " + "IMP.COS.PE" + " " + " SALIDAS  " + " " + "IMP.COS.PS" + " " + "\n";//"►  Cant.UND "+ " "+" Cost.Uni "+ " "+" Cost.Total  "+ "\n";
                        strPrinter = strPrinter + " --------------------------------------------------------------------------------------------------------------------------------------------------------" + "\n";

                        DataTable dtmov = new DataTable();
                        Valoriza valoriza = new Valoriza();
                        //query = " select  A.td,A.numdoc,A.codmov,A.fecdoc,A.rftdoc,A.rfndoc,C.ruc,C.desprovee,A.glosa1,";
                        //query = query + " B.cantidad,B.precosuni,B.totcosuni ";
                        //query = query + " from almovc A inner join almovd B ";
                        //query = query + " on A.codcia=B.codcia and A.alma=B.alma and A.td=B.td and A.numdoc=B.numdoc ";
                        //query = query + " left join provee C on A.codpro=C.codprovee";
                        //query = query + " where A.codcia='" + @codcia + "' and A.alma='" + @alma + "' and B.codarti='" + @codarti + "' ";
                        //query = query + " and B.cantidad>0 and A.fecdoc >= ";
                        //query = query + " STR_TO_DATE('" + @fechaini + "', '%d/%m/%Y')  and  ";
                        //query = query + " A.fecdoc <= STR_TO_DATE('" + @fechafin + "', '%d/%m/%Y') ";
                        //query = query + " order by A.fecdoc asc,A.td asc,A.numdoc asc";

                        query = "call kardexalmacen('" + @codcia + "','" + @alma + "','" + @codarti + "','" + @fechaini + "','" + @fechafin + "')";

                        SqlDataAdapter damov = new SqlDataAdapter();
                        damov.SelectCommand = new SqlCommand(query, conexion);
                        damov.Fill(dtmov);
                        strPrinter = strPrinter + funciones.replicateCadena(" ", 58) + "         S A L D O  I N I C I A L       " + " ";
                        decimal prepromant = decimal.Parse(valoriza.getPrecioProm(codcia, alma, codarti, mesant, anioant));
                        strPrinter = strPrinter + funciones.alineacionNumero(prepromant.ToString("#,##0.00"), 8) + " ";
                        strPrinter = strPrinter + funciones.alineacionNumero(saldoini.ToString("#,##0.00"), 10) + " ";
                        strPrinter = strPrinter + funciones.alineacionNumero((prepromant * saldoini).ToString("#,##0.00"), 10) + funciones.longitudCadena(" ", 21) + " \n";
                        if (dtmov.Rows.Count > 0)
                        {
                            decimal totpe = 0;
                            decimal totps = 0;
                            decimal canpe = 0;
                            decimal canps = 0;
                            foreach (DataRow row_mov in dtmov.Rows)
                            {
                                strPrinter = strPrinter + " " + funciones.longitudCadena(row_mov["td"].ToString(), 2) + " " + funciones.longitudCadena(row_mov["numdoc"].ToString(), 10) + " ";
                                strPrinter = strPrinter + funciones.longitudCadena(row_mov["codmov"].ToString(), 2) + " ";
                                strPrinter = strPrinter + funciones.longitudCadena(row_mov["fecdoc"].ToString(), 10) + " " + funciones.longitudCadena(row_mov["rftdoc"].ToString(), 2) + " ";
                                strPrinter = strPrinter + funciones.longitudCadena(row_mov["rfndoc"].ToString(), 13) + " " + funciones.longitudCadena(row_mov["ruc"].ToString(), 11) + " ";
                                if (row_mov["td"].ToString().Equals("PE"))
                                {
                                    strPrinter = strPrinter + funciones.longitudCadena(row_mov["desprovee"].ToString(), 40) + " ";
                                }
                                else
                                {
                                    strPrinter = strPrinter + funciones.longitudCadena(row_mov["glosa1"].ToString(), 40) + " ";
                                }//"##,##0.000;(##,##0.000);0.000"

                                strPrinter = strPrinter + funciones.alineacionNumero(float.Parse(row_mov["precosuni"].ToString()).ToString("#,##0.00"), 8) + " ";

                                if (row_mov["td"].ToString().Equals("PE"))
                                {
                                    strPrinter = strPrinter + funciones.alineacionNumero(float.Parse(row_mov["cantidad"].ToString()).ToString("#,##0.00"), 10) + " "; //##,##0.000;(##,##0.000);0.000
                                    strPrinter = strPrinter + funciones.alineacionNumero(float.Parse(row_mov["totcosuni"].ToString()).ToString("#,##0.00"), 10) + funciones.longitudCadena(" ", 21) + " |  \n";
                                    totpe += decimal.Parse(row_mov["totcosuni"].ToString());
                                    canpe += decimal.Parse(row_mov["cantidad"].ToString());


                                }
                                else
                                {
                                    strPrinter = strPrinter + funciones.longitudCadena(" ", 21);
                                    strPrinter = strPrinter + funciones.alineacionNumero(float.Parse(row_mov["cantidad"].ToString()).ToString("#,##0.00"), 10) + " ";
                                    strPrinter = strPrinter + funciones.alineacionNumero(float.Parse(row_mov["totcosuni"].ToString()).ToString("#,##0.00"), 10) + " | \n";
                                    totps += decimal.Parse(row_mov["totcosuni"].ToString());
                                    canps += decimal.Parse(row_mov["cantidad"].ToString());
                                }
                            }
                            strPrinter = strPrinter + " ---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------" + "\n";
                            strPrinter = strPrinter + funciones.replicateCadena(" ", 108);
                            strPrinter = strPrinter + funciones.alineacionNumero(canpe.ToString("#,##0.00"), 10) + " ";
                            strPrinter = strPrinter + funciones.alineacionNumero(totpe.ToString("#,##0.00"), 10) + " ";
                            strPrinter = strPrinter + funciones.alineacionNumero(canps.ToString("#,##0.00"), 10) + " ";
                            strPrinter = strPrinter + funciones.alineacionNumero(totps.ToString("#,##0.00"), 10) + "\n";
                        }

                        strPrinter = strPrinter + funciones.replicateCadena(" ", 58) + "          S A L D O  F I N A L          " + " ";
                        decimal prepromact = decimal.Parse(valoriza.getPrecioProm(codcia, alma, codarti, mesact, anioact));
                        strPrinter = strPrinter + funciones.alineacionNumero(prepromact.ToString("#,##0.00"), 8) + " ";
                        strPrinter = strPrinter + funciones.alineacionNumero(saldofin.ToString("#,##0.00"), 10) + " ";
                        strPrinter = strPrinter + funciones.alineacionNumero((prepromact * saldofin).ToString("#,##0.00"), 10) + "\n";
                    }
                }
                ui_reportetxt ui_reportetxt = new ui_reportetxt();
                ui_reportetxt._texto = strPrinter;
                ui_reportetxt.Activate();
                ui_reportetxt.BringToFront();
                ui_reportetxt.ShowDialog();
                ui_reportetxt.Dispose();
            }
        }

        private void ui_imprimeKardexResumen(string strTitulo)
        {

            Funciones funciones = new Funciones();
            
            DataTable dtgrp = new DataTable();
            string codcia = this._codcia;
            string alma = funciones.getValorComboBox(cmbAlmacen, 2);
            string codini = txtCodigoIni.Text.Trim();
            string codfin = txtCodigoFin.Text.Trim();
            string fechaini = txtFechaIni.Text;
            string fechafin = txtFechaFin.Text;
            DateTime anterior = DateTime.ParseExact(fechaini, "dd/MM/yyyy", null).AddDays(-1);
            string mesant = anterior.Month.ToString().PadLeft(2, '0');
            string anioant = anterior.Year.ToString();
            DateTime actual = DateTime.ParseExact(fechafin, "dd/MM/yyyy", null);
            string mesact = actual.Month.ToString().PadLeft(2, '0');
            string anioact = actual.Year.ToString();

            string query = string.Empty;
            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();

            string cadcondicion = string.Empty;

            if (cmbAgrupar.Text.Equals("CODIGO DE ARTICULO"))
            {
                cadcondicion = " and A.codarti between '" + @codini + "' and '" + @codfin + "' ";
            }
            else
            {
                cadcondicion = " and A.famarti between '" + @codini + "' and '" + @codfin + "' ";
            }

            int nlinea = 0;
            string strPrinter = string.Empty;
            strPrinter = strPrinter + strTitulo;

            query = " select A.grparti,A.desgrparti,A.famarti,B.desmaesgen as desfamarti from grparti A  ";
            query = query + " left join maesgen B on B.idmaesgen='110' and A.famarti=B.clavemaesgen ";
            query = query + " where A.grparti in ";
            query = query + " (select A.grparti from alarti A  where A.codcia='" + @codcia + "' and  A.codarti ";
            query = query + " in (Select codarti from almovd where codcia='" + @codcia + "' and alma='" + @alma + "') " + cadcondicion;
            query = query + " group by A.grparti)";
            query = query + " order by A.famarti asc,A.grparti asc ";

            SqlDataAdapter dagrp = new SqlDataAdapter();
            dagrp.SelectCommand = new SqlCommand(query, conexion);
            dagrp.Fill(dtgrp);

            if (dtgrp.Rows.Count > 0)
            {
                foreach (DataRow row_grp in dtgrp.Rows)
                {
                    string grparti = row_grp["grparti"].ToString();
                    string famarti = row_grp["famarti"].ToString();
                    
                    DataTable dtarti = new DataTable();
                    query = " select A.codarti,A.desarti,A.famarti,C.desmaesgen as desfamarti,A.grparti,B.desgrparti,A.unidad ";
                    query = query + " from alarti A left join grparti B on A.grparti=B.grparti and A.famarti=B.famarti ";
                    query = query + " left join maesgen C on C.idmaesgen='110' and A.famarti=C.clavemaesgen ";
                    query = query + " where A.codcia='" + @codcia + "' and A.famarti='"+@famarti+"' and A.grparti='" + @grparti + "' and  A.codarti ";
                    query = query + " in (Select codarti from almovd where codcia='" + @codcia + "' and alma='" + @alma + "') " + cadcondicion;
                    query = query + " order by C.desmaesgen asc,B.desgrparti asc,A.desarti asc ";
                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = new SqlCommand(query, conexion);
                    da.Fill(dtarti);

                    if (dtarti.Rows.Count > 0)
                    {
                        nlinea += 10;
                        strPrinter = strPrinter + "\n\n";
                        strPrinter = strPrinter + " CODIGO DE GRUPO     :  " + row_grp["grparti"].ToString() + "\n";
                        strPrinter = strPrinter + " GRUPO DE ARTICULO   :  " + row_grp["desgrparti"].ToString() + "\n";
                        strPrinter = strPrinter + " FAMILIA             :  " + String.Concat(row_grp["famarti"].ToString(), " ", row_grp["desfamarti"].ToString()) + "\n";
                        strPrinter = strPrinter + " ----------------------------------------------------------------------------------------------------------------------------------------------------" + "\n\n";
                        strPrinter = strPrinter + " CODIGO " + " " + "           NOMBRE DESCRIPTIVO           " + " " + "UND." + " " + "SALDO ANT." + " " + "IMP.COS.ANT" + " " + " ENTRADAS " + " " + "IMP.COS.PE" + " " + " SALIDAS  " + " " + "IMP.COS.PS" + " " + "SALDO ACT." + " " + "IMP.COS.ACT" + "\n";
                        strPrinter = strPrinter + " ----------------------------------------------------------------------------------------------------------------------------------------------------" + "\n";
                        
                        decimal totcospe=0;
                        decimal totcosps = 0;
                        decimal totsalini = 0;
                        decimal totsalfin = 0;

                        foreach (DataRow row_arti in dtarti.Rows)
                        {
                            
                            string codarti = row_arti["codarti"].ToString();
                            Alstock alstock = new Alstock();
                            decimal saldoini = decimal.Parse(alstock.ui_getStockAnterior(codcia, alma, codarti, fechaini));
                            decimal saldofin = decimal.Parse(alstock.ui_getStockEnFecha(codcia, alma, codarti, fechafin));
                            decimal entrada = decimal.Parse(alstock.ui_getResMovEnRangoFecha(codcia, alma, "PE", codarti, fechaini, fechafin));
                            decimal cospe = decimal.Parse(alstock.ui_getResTotCosEnRangoFecha(codcia, alma, "PE", codarti, fechaini, fechafin));
                            decimal salida = decimal.Parse(alstock.ui_getResMovEnRangoFecha(codcia, alma, "PS", codarti, fechaini, fechafin));
                            decimal cosps = decimal.Parse(alstock.ui_getResTotCosEnRangoFecha(codcia, alma, "PS", codarti, fechaini, fechafin));
                            bool imprime = true;
                            if (chkSinStock.Checked)
                            {
                                if (saldoini + saldofin + entrada + salida > 0)
                                {
                                    imprime = true;
                                }
                                else
                                {
                                    imprime = false;
                                }
                            }
                            if (imprime)
                            {
                                nlinea++;
                                Valoriza valoriza = new Valoriza();
                                decimal prepromant = decimal.Parse(valoriza.getPrecioProm(codcia, alma, codarti, mesant, anioant));
                                decimal prepromact = decimal.Parse(valoriza.getPrecioProm(codcia, alma, codarti, mesact, anioact));
                                totsalini = totsalini + (saldoini * prepromant);
                                totcospe = totcospe + cospe;
                                totcosps = totcosps + cosps;
                                totsalfin = totsalfin + (saldofin * prepromact);
                                strPrinter = strPrinter + " " + funciones.longitudCadena(row_arti["codarti"].ToString(), 8) + " " + funciones.longitudCadena(row_arti["desarti"].ToString(), 40) + " ";
                                strPrinter = strPrinter + funciones.longitudCadena(row_arti["unidad"].ToString(), 4) + " ";
                                strPrinter = strPrinter + funciones.alineacionNumero(saldoini.ToString("##,##0.00;(##,##0.00);0.00"), 10) + " ";
                                strPrinter = strPrinter + funciones.alineacionNumero((saldoini * prepromant).ToString("##,##0.00;(##,##0.00);0.00"), 10) + " ";
                                strPrinter = strPrinter + funciones.alineacionNumero(entrada.ToString("##,##0.00;(##,##0.00);0.00"), 10) + " ";
                                strPrinter = strPrinter + funciones.alineacionNumero(cospe.ToString("##,##0.00;(##,##0.00);0.00"), 10) + " ";
                                strPrinter = strPrinter + funciones.alineacionNumero(salida.ToString("##,##0.00;(##,##0.00);0.00"), 10) + " ";
                                strPrinter = strPrinter + funciones.alineacionNumero(cosps.ToString("##,##0.00;(##,##0.00);0.00"), 10) + " ";
                                strPrinter = strPrinter + funciones.alineacionNumero(saldofin.ToString("##,##0.00;(##,##0.00);0.00"), 10) + " ";
                                strPrinter = strPrinter + funciones.alineacionNumero((saldofin * prepromact).ToString("##,##0.00;(##,##0.00);0.00"), 10);
                                if (nlinea < 90)
                                {
                                    strPrinter = strPrinter + "\n";
                                }
                                else
                                {
                                    strPrinter = strPrinter + "\f\n\n\n" + strTitulo + "\n\n"; ;
                                    nlinea = 0;
                                }
                            }
                        }
                        
                        strPrinter = strPrinter + " ----------------------------------------------------------------------------------------------------------------------------------------------------" + "\n";
                        strPrinter = strPrinter + funciones.replicateCadena(" ", 43) + "IMPORTE TOTAL" + " " + funciones.replicateCadena(" ", 10);
                        strPrinter = strPrinter + funciones.alineacionNumero(totsalini.ToString("##,##0.00;(##,##0.00);0.00"), 10) + " " + funciones.replicateCadena(" ", 10) + " ";
                        strPrinter = strPrinter + funciones.alineacionNumero(totcospe.ToString("##,##0.00;(##,##0.00);0.00"), 10) + " " + funciones.replicateCadena(" ", 10) + " ";
                        strPrinter = strPrinter + funciones.alineacionNumero(totcosps.ToString("##,##0.00;(##,##0.00);0.00"), 10) + " " + funciones.replicateCadena(" ", 10) + " ";
                        strPrinter = strPrinter + funciones.alineacionNumero(totsalfin.ToString("##,##0.00;(##,##0.00);0.00"), 10) + "\n\n\n";

                    }
                }
                ui_reportetxt ui_reportetxt = new ui_reportetxt();
                ui_reportetxt._texto = strPrinter;
                ui_reportetxt.Activate();
                ui_reportetxt.BringToFront();
                ui_reportetxt.ShowDialog();
                ui_reportetxt.Dispose();
            }
        }

        private string ui_validar()
        {
            string valorValida = "G";

            if (txtCodigoIni.Text == string.Empty && valorValida == "G")
            {
                MessageBox.Show("No ha especificado código inicial", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                valorValida = "B";
                txtCodigoIni.Focus();
            }

            if (txtCodigoFin.Text == string.Empty && valorValida == "G")
            {
                MessageBox.Show("No ha especificado código Final", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                valorValida = "B";
                txtCodigoFin.Focus();
            }

            return valorValida;
        }

        private void btnVisualiza_Click(object sender, EventArgs e)
        {
            string valorValida = ui_validar();
            if (valorValida.Equals("G"))
            {
                Funciones funciones = new Funciones();
                string codcia = this._codcia;
                string alma = funciones.getValorComboBox(cmbAlmacen, 2);
                string codini = txtCodigoIni.Text.Trim();
                string codfin = txtCodigoFin.Text.Trim();
                string fechaini = txtFechaIni.Text;
                string fechafin = txtFechaFin.Text;
                Alalma alalma = new Alalma();
                string desalma = alalma.ui_getDatos(codcia, alma, "DESCRIPCION");
                string strTitulo = string.Empty;

                strTitulo = strTitulo + "\n\n";
                strTitulo = strTitulo + funciones.replicateCadena(" ", 40) + "  K A R D E X    D E    A L M A C E N    V A L O R I Z A D O " + "\n";
                strTitulo = strTitulo + funciones.replicateCadena(" ", 40) + "                   " + desalma + "                     " + "\n";
                strTitulo = strTitulo + funciones.replicateCadena(" ", 40) + "             ( DEL " + fechaini + " AL " + fechafin + " )            " + "\n";

                if (chkStock.Checked)
                {
                    Alstock alstock = new Alstock();
                    alstock.recalcularStockAlmacen(codcia, alma);
                }

                if (cmbTipoKardex.Text.Trim().Equals("Detallado"))
                {
                    ui_imprimeKardexDetalle(strTitulo);
                }
                else
                {
                    ui_imprimeKardexResumen(strTitulo);
                }
            }
        }
    }
}
