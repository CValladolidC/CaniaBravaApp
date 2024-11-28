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
    public partial class ui_updalmov : Form
    {
        Funciones funciones = new Funciones();

        string _operacion;
        string _codcia;
        string _codalma;
        string _desalma;
        string _finaliza;

        private TextBox TextBoxActivo;
        private Form FormPadre;

        public Form _FormPadre
        {
            get { return FormPadre; }
            set { FormPadre = value; }
        }

        public TextBox _TextBoxActivo
        {
            get { return TextBoxActivo; }
            set { TextBoxActivo = value; }
        }

        public ui_updalmov()
        {
            InitializeComponent();
        }

        public void ui_ActualizaComboBox()
        {
            MaesGen maesgen = new MaesGen();
            string query;
            cmbTipo.Text = "PE  PARTE DE ENTRADA";
            string tipomov = funciones.getValorComboBox(cmbTipo, 2);
            string codalma = this._codalma;
            string codcia = this._codcia;
            query = "SELECT codmov as clave,desmov as descripcion FROM tipomov WHERE tipomov='" + @tipomov + "' and estado='V';";
            funciones.listaComboBox(query, cmbMov, "");
            maesgen.listaDetMaesGen("160", cmbTipoDoc, "B");
            //maesgen.listaDetMaesGen("150", cmbSeccion, "B");
            //query = "SELECT codcencos as clave,descencos as descripcion FROM cencos WHERE codcia='" + @codcia + "' and codalma='" + @codalma + "' and estado='V';";
            //funciones.listaComboBox(query, cmbCenCos, "");
            //query = "SELECT codalma as clave,desalma as descripcion FROM alalma WHERE codcia='" + @codcia + "' and codalma<>'" + @codalma + "' and estado='V';";
            //funciones.listaComboBox(query, cmbAlmRef, "B");
        }

        public void setData(string codcia, string codalma, string desalmacen)
        {
            this._codcia = codcia;
            this._codalma = codalma;
            this._desalma = desalmacen;
            this.Text = "Actualización de Datos - " + desalmacen;
        }

        public void agregar()
        {

            GlobalVariables variables = new GlobalVariables();
            this._operacion = "AGREGAR";
            this._finaliza = "N";
            cmbTipo.Text = "PE  PARTE DE ENTRADA";
            txtNumDoc.Clear();
            txtFecha.Text = DateTime.Now.ToString("dd/MM/yyyy");
            cmbMov.Text = "";
            cmbTipoDoc.Text = "";
            txtNroDoc.Clear();
            txtProveedor.Clear();
            txtRucProvee.Clear();
            txtDscProveedor.Clear();
            txtOrdenCon.Clear();
            txtOrdenDes.Clear();
            //cmbSeccion.Text = "";
            //cmbSolicitante.Text = "";
            //txtNomRec.Clear();
            //cmbCenCos.Text = "";
            //txtCliente.Clear();
            //txtDscCliente.Clear();
            //txtRucClie.Clear();
            txtGlosa1.Clear();
            txtGlosa2.Clear();
            txtGlosa3.Clear();
            //cmbAlmRef.Text = "";
            txtEstado.Text = "VIGENTE";
            txtFecCrea.Text = DateTime.Now.ToString("dd/MM/yyyy");
            txtFecMod.Text = DateTime.Now.ToString("dd/MM/yyyy");
            txtUsuario.Text = variables.getValorUsr();
            tabPageEX2.Enabled = false;
            tabControl.SelectTab(0);
            cmbTipo.Focus();
        }

        public void editar(string codcia, string alma, string td, string numdoc)
        {
            string query;
            this._operacion = "EDITAR";

            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();
            query = "SELECT * FROM almovc where /*codcia='" + @codcia + "' and */alma='" + @alma + "' and td='" + @td + "' and numdoc='" + @numdoc + "'; ";

            try
            {
                SqlCommand myCommand = new SqlCommand(query, conexion);
                SqlDataReader myReader = myCommand.ExecuteReader();

                if (myReader.Read())
                {
                    MaesGen maesgen = new MaesGen();

                    if (myReader.GetString(myReader.GetOrdinal("td")).Equals("PE"))
                    {
                        cmbTipo.Text = "PE  INGRESOS";
                    }
                    else
                    {
                        cmbTipo.Text = "PS  CONSUMOS";
                    }

                    txtNumDoc.Text = myReader.GetString(myReader.GetOrdinal("numdoc"));
                    txtFecha.Value = myReader.GetDateTime(myReader.GetOrdinal("fecdoc"));
                    query = " SELECT codmov as clave,desmov as descripcion FROM tipomov WHERE ";
                    query = query + " tipomov='" + @myReader.GetString(myReader.GetOrdinal("td")) + "' and codmov='" + @myReader.GetString(myReader.GetOrdinal("codmov")) + "';";
                    funciones.consultaComboBox(query, cmbMov);
                    maesgen.consultaDetMaesGen("160", myReader.GetString(myReader.GetOrdinal("rftdoc")), cmbTipoDoc);

                    string rfndoc = myReader.GetString(myReader.GetOrdinal("rfndoc")) + funciones.replicateCadena(" ", 13);
                    txtSerie.Text = rfndoc.Substring(0, 4);
                    txtNroDoc.Text = rfndoc.Substring(5, 8);

                    if (myReader.GetString(myReader.GetOrdinal("codpro")).Trim() != string.Empty)
                    {
                        txtProveedor.Text = myReader.GetString(myReader.GetOrdinal("codpro"));
                        Provee provee = new Provee();
                        txtDscProveedor.Text = provee.ui_getDatos(myReader.GetString(myReader.GetOrdinal("codpro")), "NOMBRE");
                        txtRucProvee.Text = provee.ui_getDatos(myReader.GetString(myReader.GetOrdinal("codpro")), "RUC");
                    }
                    else
                    {
                        txtProveedor.Clear();
                        txtDscProveedor.Clear();
                        txtRucProvee.Clear();
                    }

                    txtOrdenCon.Text = myReader.GetString(myReader.GetOrdinal("rfnsoli")).Trim();
                    //txtOrdenDes.Text = myReader.GetString(myReader.GetOrdinal("rfnsolides"));
                    //maesgen.consultaDetMaesGen("150", myReader.GetString(myReader.GetOrdinal("secsoli")), cmbSeccion);
                    //query = "SELECT codsoli as clave,dessoli as descripcion FROM solicita ";
                    //query = query + "WHERE codcia='" + @codcia + "' and secsoli='" + @myReader.GetString(myReader.GetOrdinal("secsoli")) + "' and codsoli='" + @myReader.GetString(myReader.GetOrdinal("persoli")) + "';";
                    //funciones.validarCombobox(query, cmbSolicitante);
                    //txtNomRec.Text = myReader.GetString(myReader.GetOrdinal("nomrec"));
                    //query = "SELECT codcencos as clave,descencos as descripcion FROM cencos WHERE codcia='" + @codcia + "' and ";
                    //query = query + " codalma='" + @myReader.GetString(myReader.GetOrdinal("alma")) + "' and ";
                    //query = query + " codcencos='" + @myReader.GetString(myReader.GetOrdinal("cencos")) + "';";
                    //funciones.consultaComboBox(query, cmbCenCos);

                    //if (myReader.GetString(myReader.GetOrdinal("codclie")).Trim() != string.Empty)
                    //{
                    //    txtCliente.Text = myReader.GetString(myReader.GetOrdinal("codclie"));
                    //    Clie clie = new Clie();
                    //    txtDscCliente.Text = clie.ui_getDatos(myReader.GetString(myReader.GetOrdinal("codclie")), "NOMBRE");
                    //    txtRucClie.Text = clie.ui_getDatos(myReader.GetString(myReader.GetOrdinal("codclie")), "RUC");
                    //}
                    //else
                    //{
                    //    txtCliente.Clear();
                    //    txtDscCliente.Clear();
                    //    txtRucClie.Clear();
                    //}

                    txtGlosa1.Text = myReader.GetString(myReader.GetOrdinal("glosa1"));
                    txtGlosa2.Text = myReader.GetString(myReader.GetOrdinal("glosa2"));
                    txtGlosa3.Text = myReader.GetString(myReader.GetOrdinal("glosa3"));
                    //query = "SELECT codalma as clave,desalma as descripcion FROM alalma WHERE codcia='" + @codcia + "' and codalma='" + @myReader.GetString(myReader.GetOrdinal("rfalma")) + "' ;";
                    //funciones.consultaComboBox(query, cmbAlmRef);
                    txtFecCrea.Text = myReader.GetString(myReader.GetOrdinal("fcrea"));
                    txtFecMod.Text = myReader.GetString(myReader.GetOrdinal("fmod"));
                    txtUsuario.Text = myReader.GetString(myReader.GetOrdinal("usuario"));
                    if (myReader.GetString(myReader.GetOrdinal("situa")).Equals("V"))
                    {

                        txtEstado.Text = "VIGENTE";
                        this._finaliza = "N";
                        btnAceptar.Enabled = true;
                        btnFinaliza.Enabled = true;
                        btnNuevo.Enabled = true;
                        btnEditar.Enabled = true;
                        btnEliminar.Enabled = true;

                    }
                    else
                    {
                        this._finaliza = "S";
                        btnAceptar.Enabled = false;
                        btnFinaliza.Enabled = false;
                        btnNuevo.Enabled = false;
                        btnEditar.Enabled = false;
                        btnEliminar.Enabled = false;
                        if (myReader.GetString(myReader.GetOrdinal("situa")).Equals("A"))
                        {
                            txtEstado.Text = "ANULADO";
                            btnImprimir.Enabled = false;
                        }
                        else
                        {
                            txtEstado.Text = "FINALIZADO";
                            btnImprimir.Enabled = true;
                        }
                    }
                    habilitaOpciones(myReader.GetString(myReader.GetOrdinal("td")), myReader.GetString(myReader.GetOrdinal("codmov")));
                }
                lblUsuario.Visible = true;
                txtNumDoc.Visible = true;
                ui_listaItem();
                tabPageEX2.Enabled = true;
                tabControl.SelectTab(0);
                cmbTipo.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }

            conexion.Close();
        }
        
        private void btnAceptar_Click(object sender, EventArgs e)
        {
            string td = funciones.getValorComboBox(cmbTipo, 2);
            string codmov = funciones.getValorComboBox(cmbMov, 2);
            string valorValida = validaOpciones(td, codmov);
            if (valorValida.Equals("G"))
            {
                try
                {
                    AlmovC almovc = new AlmovC();
                    GlobalVariables variables = new GlobalVariables();
                    string operacion = this._operacion;
                    string codcia = "01";// this._codcia;
                    string alma = this._codalma;
                    string fecdoc = txtFecha.Value.ToString("yyyy-MM-dd");
                    string rftdoc = funciones.getValorComboBox(cmbTipoDoc, 4);
                    string rfndoc = string.Empty;
                    if (rftdoc.Trim() != string.Empty)
                    {
                        rfndoc = String.Concat(funciones.replicateCadena("0", 4 - txtSerie.Text.Trim().Length) + txtSerie.Text.Trim(), "-", funciones.replicateCadena("0", 8 - txtNroDoc.Text.Trim().Length) + txtNroDoc.Text.Trim());
                    }
                    string codpro = txtProveedor.Text;
                    string secsoli = "";//funciones.getValorComboBox(cmbSeccion, 4);
                    string persoli = "";//funciones.getValorComboBox(cmbSolicitante, 2);
                    string rfnsoli = txtOrdenCon.Text.Trim();
                    string nomrec = "";//txtNomRec.Text;
                    string cencos = "";//funciones.getValorComboBox(cmbCenCos, 2);
                    string codclie = "";//txtCliente.Text;
                    string glosa1 = txtGlosa1.Text.Trim();
                    string glosa2 = txtGlosa2.Text.Trim();
                    string glosa3 = txtGlosa3.Text.Trim();
                    string rfalma = "";//funciones.getValorComboBox(cmbAlmRef, 2);
                    string situa = txtEstado.Text.Substring(0, 1);
                    string fcrea = txtFecCrea.Text;
                    string fmod = DateTime.Now.ToString("dd/MM/yyyy");
                    string usuario = variables.getValorUsr();
                    string produccion = string.Empty;
                    string año = string.Empty;

                    if (td == "PE" && codmov == "PR")
                    {
                        produccion = cmb_dato1.Text.Trim();
                        año = cmb_dato2.Text.Trim();
                    }
                    else
                    {
                        produccion = string.Empty;
                        año = string.Empty;
                    }

                    if (operacion.Equals("AGREGAR"))
                    {
                        txtNumDoc.Text = almovc.genCodAlmov(codcia, alma, td);
                    }
                    string numdoc = txtNumDoc.Text;
                    almovc.updAlmovC(operacion, codcia, alma, td, numdoc, fecdoc, codmov, situa, rftdoc, rfndoc,
                        secsoli, persoli, rfnsoli, nomrec, codpro, cencos, rfalma, glosa1, glosa2, glosa3,
                        codclie, fcrea, fmod, usuario, produccion, año, "ALMA");

                    MessageBox.Show("Información registrada correctamente", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ((ui_almov)FormPadre).btnActualizar.PerformClick();
                    this._finaliza = "S";
                    if (operacion.Equals("AGREGAR"))
                    {
                        this._operacion = "EDITAR";
                        lblUsuario.Visible = true;
                        txtNumDoc.Visible = true;

                        if (td == "PE") { CopyPedidoToIngreso(numdoc, alma, rfnsoli); }

                        ui_listaItem();
                        tabPageEX2.Enabled = true;
                        tabControl.SelectTab(1);
                        btnNuevo.Focus();
                    }
                }
                catch (FormatException ex)

                {
                    MessageBox.Show(ex.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void CopyPedidoToIngreso(string numdoc, string alma, string solalma)
        {
            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();
            string query = "INSERT INTO almovd (codcia,alma,td,numdoc,item,codarti,cantidad,lote) ";
            query += " SELECT codcia,alma,'PE' [td],'" + @numdoc + "' [numdoc],item,codarti,cantidad,'" + @numdoc + "'+'/'+item [lote] ";
            query += "FROM solalmad (NOLOCK) ";
            query += "WHERE alma='" + @alma + "' AND solalma='" + @solalma + "' ";
            query += "ORDER BY item asc ;";

            try
            {
                SqlCommand myCommand = new SqlCommand(query, conexion);
                myCommand.ExecuteNonQuery();
                myCommand.Dispose();
            }
            catch (SqlException e)
            {
                MessageBox.Show(e.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void cmbTipoDoc_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(Keys.Enter))
            {
                if (cmbTipoDoc.Text != String.Empty)
                {
                    MaesGen maesgen = new MaesGen();
                    maesgen.validarDetMaesGen("160", cmbTipoDoc, cmbTipoDoc.Text);
                }
                e.Handled = true;
                SendKeys.Send("{TAB}");
            }
        }

        private void cmbSeccion_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(Keys.Enter))
            {
                //if (cmbSeccion.Text != String.Empty)
                //{
                //    MaesGen maesgen = new MaesGen();
                //    maesgen.validarDetMaesGen("150", cmbSeccion, cmbSeccion.Text);
                //}

                //string codcia = this._codcia;
                //string secsoli = funciones.getValorComboBox(cmbSeccion, 4);
                //string query = "SELECT codsoli as clave,dessoli as descripcion FROM solicita ";
                //query = query + " WHERE codcia='" + @codcia + "' and secsoli='" + @secsoli + "' ;";
                //funciones.listaComboBox(query, cmbSolicitante, "");
                //e.Handled = true;
                //SendKeys.Send("{TAB}");
            }
        }

        private void cmbMov_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(Keys.Enter))
            {
                if (cmbMov.Text != String.Empty)
                {
                    string tipomov = funciones.getValorComboBox(cmbTipo, 2);
                    string codmov = funciones.getValorComboBox(cmbMov, 2);
                    string query = "SELECT codmov as clave,desmov as descripcion FROM tipomov ";
                    query = query + "WHERE tipomov='" + @tipomov + "' and codmov='" + @codmov + "';";
                    funciones.validarCombobox(query, cmbMov);
                    limpiar();
                    habilitaOpciones(tipomov, codmov);
                    e.Handled = true;
                    SendKeys.Send("{TAB}");
                }
            }
        }

        private void cmbSolicitante_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(Keys.Enter))
            {
                //if (cmbSolicitante.Text != String.Empty)
                //{
                //    string codcia = this._codcia;
                //    string secsoli = funciones.getValorComboBox(cmbSeccion, 4);
                //    string codsoli = funciones.getValorComboBox(cmbSolicitante, 2);
                //    string query = "SELECT codsoli as clave,dessoli as descripcion FROM solicita ";
                //    query = query + "WHERE codcia='" + @codcia + "' and secsoli='" + @secsoli + "' and codsoli='" + @codsoli + "';";
                //    funciones.validarCombobox(query, cmbSolicitante);
                //}
                e.Handled = true;
                SendKeys.Send("{TAB}");

            }
        }

        private void cmbCenCos_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(Keys.Enter))
            {
                //if (cmbCenCos.Text != String.Empty)
                //{
                //    string codcia = this._codcia;
                //    string codalma = this._codalma;
                //    string codcencos = funciones.getValorComboBox(cmbCenCos, 2);
                //    string query = "SELECT codcencos as clave,descencos as descripcion FROM cencos ";
                //    query = query + "WHERE codcia='" + @codcia + "' and codalma='" + @codalma + "' and codcencos='" + @codcencos + "';";
                //    funciones.validarCombobox(query, cmbCenCos);
                //}
                e.Handled = true;
                SendKeys.Send("{TAB}");
            }
        }

        private void cmbAlmRef_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(Keys.Enter))
            {
                //if (cmbAlmRef.Text != String.Empty)
                //{
                //    string codcia = this._codcia;
                //    string codalma = funciones.getValorComboBox(cmbAlmRef, 2);
                //    string query = " SELECT codalma as clave,desalma as descripcion FROM alalma ";
                //    query = query + " WHERE codcia='" + @codcia + "' and codalma='" + @codalma + "';";
                //    funciones.validarCombobox(query, cmbAlmRef);
                //}
                //e.Handled = true;
                //SendKeys.Send("{TAB}");
            }
        }

        private void cmbSeccion_SelectedIndexChanged(object sender, EventArgs e)
        {
            string codcia = this._codcia;
            //string secsoli = funciones.getValorComboBox(cmbSeccion, 4);
            //string query = "SELECT codsoli as clave,dessoli as descripcion FROM solicita ";
            //query = query + " WHERE codcia='" + @codcia + "' and secsoli='" + @secsoli + "' ;";
            //funciones.listaComboBox(query, cmbSolicitante, "");
        }

        private void limpiar()
        {
            txtProveedor.Clear();
            txtDscProveedor.Clear();
            txtRucProvee.Clear();
            //cmbSeccion.Text = "";
            //cmbSolicitante.Text = "";
            //txtNomRec.Clear();
            //cmbAlmRef.Text = "";
            //txtCliente.Clear();
            //txtDscCliente.Clear();
            //txtRucClie.Clear();
            cmbTipoDoc.Text = "";
            txtSerie.Clear();
            txtNroDoc.Clear();
            //cmbCenCos.Text = "";
            txtGlosa1.Clear();
            txtGlosa2.Clear();
            txtGlosa3.Clear();
            txtOrdenCon.Clear();
            txtOrdenDes.Clear();
        }

        private void habilitaOpciones(string tipomov, string codmov)
        {
            string query;
            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();
            query = "SELECT * FROM tipomov where tipomov='" + @tipomov + "' and codmov='" + @codmov + "'; ";

            try
            {
                SqlCommand myCommand = new SqlCommand(query, conexion);
                SqlDataReader myReader = myCommand.ExecuteReader();

                if (myReader.Read())
                {
                    if (myReader.GetString(myReader.GetOrdinal("proveedor")).Equals("1"))
                    {
                        txtProveedor.Enabled = true;
                        btnAddProvee.Enabled = true;
                        btnUpdProvee.Enabled = true;
                    }
                    else
                    {
                        txtProveedor.Enabled = false;
                        btnAddProvee.Enabled = false;
                        btnUpdProvee.Enabled = false;
                    }
                    if (myReader.GetString(myReader.GetOrdinal("dato1")).Equals("1"))
                    {
                        cmb_dato1.Visible = true;
                        lbl_produccion.Visible = true;
                        lbl_año.Visible = true;
                    }
                    else
                    {
                        cmb_dato1.Visible = false;
                    }
                    if (myReader.GetString(myReader.GetOrdinal("dato2")).Equals("1"))
                    {
                        cmb_dato2.Visible = true;
                        lbl_produccion.Visible = true;
                        lbl_año.Visible = true;
                    }
                    else
                    {
                        cmb_dato2.Visible = false;
                        lbl_produccion.Visible = false;
                        lbl_año.Visible = false;
                    }

                    if (myReader.GetString(myReader.GetOrdinal("ordencon")).Equals("1"))
                    {
                        txtOrdenCon.Enabled = true;
                        //    cmbSeccion.Enabled = false;
                        //    cmbSolicitante.Enabled = false;
                        //    txtNomRec.Enabled = false;
                    }
                    else
                    {
                        txtOrdenCon.Enabled = false;

                        //    if (myReader.GetString(myReader.GetOrdinal("solicitante")).Equals("1"))
                        //    {
                        //        cmbSeccion.Enabled = true;
                        //        cmbSolicitante.Enabled = true;
                        //        txtNomRec.Enabled = true;
                        //    }
                        //    else
                        //    {
                        //        cmbSeccion.Enabled = false;
                        //        cmbSolicitante.Enabled = false;
                        //        txtNomRec.Enabled = false;
                        //    }
                    }

                    //if (myReader.GetString(myReader.GetOrdinal("almacen")).Equals("1"))
                    //{
                    //    cmbAlmRef.Enabled = true;
                    //}
                    //else
                    //{
                    //    cmbAlmRef.Enabled = false;
                    //}

                    //if (myReader.GetString(myReader.GetOrdinal("cliente")).Equals("1"))
                    //{
                    //    txtCliente.Enabled = true;
                    //    btnAddClie.Enabled = true;
                    //    btnUpdClie.Enabled = true;
                    //}
                    //else
                    //{
                    //    txtCliente.Enabled = false;
                    //    btnAddClie.Enabled = false;
                    //    btnUpdClie.Enabled = false;
                    //}

                    if (myReader.GetString(myReader.GetOrdinal("docref")).Equals("1"))
                    {
                        cmbTipoDoc.Enabled = true;
                        txtSerie.Enabled = true;
                        txtNroDoc.Enabled = true;
                    }
                    else
                    {
                        cmbTipoDoc.Enabled = false;
                        txtSerie.Enabled = false;
                        txtNroDoc.Enabled = false;
                    }

                    //if (myReader.GetString(myReader.GetOrdinal("cencos")).Equals("1"))
                    //{
                    //    cmbCenCos.Enabled = true;
                    //}
                    //else
                    //{
                    //    cmbCenCos.Enabled = false;
                    //}

                    if (myReader.GetString(myReader.GetOrdinal("glosa")).Equals("1"))
                    {
                        txtGlosa1.Enabled = true;
                        txtGlosa2.Enabled = true;
                        txtGlosa3.Enabled = true;
                    }
                    else
                    {
                        txtGlosa1.Enabled = false;
                        txtGlosa2.Enabled = false;
                        txtGlosa3.Enabled = false;
                    }
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }

            conexion.Close();

        }

        private string validaOpciones(string tipomov, string codmov)
        {
            string query;
            string valorValida = "G";
            MaesGen maesgen = new MaesGen();
            string codcia = this._codcia;

            if (cmbMov.Text == string.Empty && valorValida == "G")
            {
                valorValida = "B";
                MessageBox.Show("No ha seleccionado Código de Movimiento", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                tabControl.SelectTab(0);
                cmbMov.Focus();
            }
            if (cmbMov.Text != string.Empty && valorValida == "G")
            {
                query = "SELECT codmov as clave,desmov as descripcion FROM tipomov WHERE tipomov='" + @tipomov + "' and codmov='" + @codmov + "';";
                string resultado = funciones.verificaItemComboBox(query, cmbMov);
                if (resultado.Trim() == string.Empty)
                {
                    valorValida = "B";
                    MessageBox.Show("Dato incorrecto en Código de Movimiento", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    tabControl.SelectTab(0);
                    cmbMov.Focus();
                }
            }

            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();
            query = "SELECT * FROM tipomov where tipomov='" + @tipomov + "' and codmov='" + @codmov + "'; ";
            try
            {
                SqlCommand myCommand = new SqlCommand(query, conexion);
                SqlDataReader myReader = myCommand.ExecuteReader();
                if (myReader.Read())
                {
                    if (myReader.GetString(myReader.GetOrdinal("docref")).Equals("1"))
                    {
                        if (cmbTipoDoc.Text == string.Empty && valorValida == "G")
                        {
                            valorValida = "B";
                            MessageBox.Show("No ha seleccionado Tipo de Documento", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            tabControl.SelectTab(0);
                            cmbTipoDoc.Focus();
                        }
                        if (cmbTipoDoc.Text != string.Empty && valorValida == "G")
                        {
                            string resultado = maesgen.verificaComboBoxMaesGen("160", cmbTipoDoc.Text.Trim());
                            if (resultado.Trim() == string.Empty)
                            {
                                valorValida = "B";
                                MessageBox.Show("Dato incorrecto en Tipo de Documento", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                tabControl.SelectTab(0);
                                cmbTipoDoc.Focus();
                            }
                        }

                        if (txtSerie.Text == string.Empty && valorValida == "G")
                        {
                            valorValida = "B";
                            MessageBox.Show("No ha ingresado Serie", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            tabControl.SelectTab(0);
                            txtSerie.Focus();
                        }

                        if (txtNroDoc.Text == string.Empty && valorValida == "G")
                        {
                            valorValida = "B";
                            MessageBox.Show("No ha ingresado Nro.Doc.", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            tabControl.SelectTab(0);
                            txtNroDoc.Focus();
                        }
                    }
                    if (myReader.GetString(myReader.GetOrdinal("proveedor")).Equals("1"))
                    {
                        if (txtProveedor.Text == string.Empty && valorValida == "G")
                        {
                            valorValida = "B";
                            MessageBox.Show("No ha ingresado Proveedor", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            tabControl.SelectTab(0);
                            txtProveedor.Focus();
                        }
                        Provee provee = new Provee();
                        if (txtProveedor.Text != string.Empty && valorValida == "G" && provee.ui_getDatos(txtProveedor.Text, "NOMBRE") == string.Empty)
                        {
                            valorValida = "B";
                            MessageBox.Show("Proveedor no existe", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            tabControl.SelectTab(0);
                            txtProveedor.Focus();
                        }
                    }
                    if (myReader.GetString(myReader.GetOrdinal("ordencon")).Equals("1"))
                    {
                        if (txtOrdenCon.Text == string.Empty && valorValida == "G")
                        {
                            valorValida = "B";
                            MessageBox.Show("No ha ingresado Orden de Consumo", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            tabControl.SelectTab(0);
                            txtOrdenCon.Focus();
                        }
                    }
                    else
                    {
                        if (myReader.GetString(myReader.GetOrdinal("solicitante")).Equals("1"))
                        {
                            //if (cmbSeccion.Text == string.Empty && valorValida == "G")
                            //{
                            //    valorValida = "B";
                            //    MessageBox.Show("No ha seleccionado Sección", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            //    tabControl.SelectTab(0);
                            //    cmbSeccion.Focus();
                            //}
                            //if (cmbSeccion.Text != string.Empty && valorValida == "G")
                            //{
                            //    string resultado = maesgen.verificaComboBoxMaesGen("150", cmbSeccion.Text.Trim());
                            //    if (resultado.Trim() == string.Empty)
                            //    {
                            //        valorValida = "B";
                            //        MessageBox.Show("Dato incorrecto en Sección", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            //        tabControl.SelectTab(0);
                            //        cmbSeccion.Focus();
                            //    }
                            //}
                            //if (cmbSolicitante.Text == string.Empty && valorValida == "G")
                            //{
                            //    valorValida = "B";
                            //    MessageBox.Show("No ha seleccionado Solicitante", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            //    tabControl.SelectTab(0);
                            //    cmbSolicitante.Focus();
                            //}
                            //if (cmbSolicitante.Text != string.Empty && valorValida == "G")
                            //{
                            //    string secsoli = funciones.getValorComboBox(cmbSeccion, 4);
                            //    string codsoli = funciones.getValorComboBox(cmbSolicitante, 2);
                            //    query = "SELECT codsoli as clave,dessoli as descripcion FROM solicita ";
                            //    query = query + "WHERE codcia='" + @codcia + "' and secsoli='" + @secsoli + "' and codsoli='" + @codsoli + "';";
                            //    string resultado = funciones.verificaItemComboBox(query, cmbSolicitante);
                            //    if (resultado.Trim() == string.Empty)
                            //    {
                            //        valorValida = "B";
                            //        MessageBox.Show("Dato incorrecto en Solicitante", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            //        tabControl.SelectTab(0);
                            //        cmbSolicitante.Focus();
                            //    }
                            //}
                            //if (txtNomRec.Text == string.Empty && valorValida == "G")
                            //{
                            //    valorValida = "B";
                            //    MessageBox.Show("No ha ingresado Nombre del Receptor", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            //    tabControl.SelectTab(0);
                            //    txtNomRec.Focus();
                            //}
                        }

                    }
                    if (myReader.GetString(myReader.GetOrdinal("cencos")).Equals("1"))
                    {
                        //if (cmbCenCos.Text == string.Empty && valorValida == "G")
                        //{
                        //    valorValida = "B";
                        //    MessageBox.Show("No ha seleccionado Centro de Costo", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        //    tabControl.SelectTab(0);
                        //    cmbCenCos.Focus();
                        //}
                        //if (cmbCenCos.Text != string.Empty && valorValida == "G")
                        //{
                        //    string codalma = this._codalma;
                        //    string codcencos = funciones.getValorComboBox(cmbCenCos, 2);
                        //    query = "SELECT codcencos as clave,descencos as descripcion FROM cencos WHERE ";
                        //    query = query + " codcia='" + @codcia + "' and codalma='" + @codalma + "' and ";
                        //    query = query + " codcencos='" + @codcencos + "';";
                        //    string resultado = funciones.verificaItemComboBox(query, cmbCenCos);
                        //    if (resultado.Trim() == string.Empty)
                        //    {
                        //        valorValida = "B";
                        //        MessageBox.Show("Dato incorrecto en Centro de Costo", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        //        tabControl.SelectTab(0);
                        //        cmbCenCos.Focus();
                        //    }
                        //}
                    }
                    if (myReader.GetString(myReader.GetOrdinal("cliente")).Equals("1"))
                    {
                        //if (txtCliente.Text == string.Empty && valorValida == "G")
                        //{
                        //    valorValida = "B";
                        //    MessageBox.Show("No ha ingresado Cliente", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        //    tabControl.SelectTab(0);
                        //    txtCliente.Focus();
                        //}
                        //Clie clie = new Clie();
                        //if (txtCliente.Text != string.Empty && valorValida == "G" && clie.ui_getDatos(txtCliente.Text, "NOMBRE") == string.Empty)
                        //{
                        //    valorValida = "B";
                        //    MessageBox.Show("Cliente no existe", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        //    tabControl.SelectTab(0);
                        //    txtCliente.Focus();
                        //}
                    }
                    if (myReader.GetString(myReader.GetOrdinal("almacen")).Equals("1"))
                    {
                        //if (cmbAlmRef.Text == string.Empty && valorValida == "G")
                        //{
                        //    valorValida = "B";
                        //    MessageBox.Show("No ha seleccionado Almacén Referencial", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        //    tabControl.SelectTab(0);
                        //    cmbAlmRef.Focus();
                        //}
                        //if (cmbAlmRef.Text != string.Empty && valorValida == "G")
                        //{
                        //    string codalma = funciones.getValorComboBox(cmbAlmRef, 2);
                        //    query = "SELECT codalma as clave,desalma as descripcion FROM alalma WHERE codcia='" + @codcia + "' and codalma='" + @codalma + "' ;";
                        //    string resultado = funciones.verificaItemComboBox(query, cmbAlmRef);
                        //    if (resultado.Trim() == string.Empty)
                        //    {
                        //        valorValida = "B";
                        //        MessageBox.Show("Dato incorrecto en Almacén Referencial", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        //        tabControl.SelectTab(0);
                        //        cmbAlmRef.Focus();
                        //    }
                        //}
                    }
                    //if (myReader.GetString(myReader.GetOrdinal("glosa")).Equals("1"))
                    //{
                    //    if (txtGlosa1.Text == string.Empty && valorValida == "G")
                    //    {
                    //        valorValida = "B";
                    //        MessageBox.Show("No ha ingresado Glosa", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    //        tabControl.SelectTab(0);
                    //        txtGlosa1.Focus();
                    //    }
                    //}
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            conexion.Close();
            return valorValida;
        }

        private void ui_updalmov_Load(object sender, EventArgs e)
        {
            cmb_dato1.Items.Add("1ERA PRODUCCION");
            cmb_dato1.Items.Add("2DA PRODUCCION");
            cmb_dato1.Items.Add("3RA PRODUCCION");
            cmb_dato1.Items.Add("4TA PRODUCCION");
            cmb_dato1.Items.Add("5TA PRODUCCION");
            cmb_dato1.Items.Add("6TA PRODUCCION");
            cmb_dato1.Items.Add("7MA PRODUCCION");
            cmb_dato1.Items.Add("8VA PRODUCCION");
            cmb_dato1.Items.Add("9NA PRODUCCION");
            cmb_dato1.Items.Add("10MA PRODUCCION");
            cmb_dato1.Items.Add("11VA PRODUCCION");
            cmb_dato1.Items.Add("12VA PRODUCCION");
            cmb_dato1.Items.Add("13VA PRODUCCION");
            cmb_dato1.Items.Add("14VA PRODUCCION");
            cmb_dato1.Items.Add("15VA PRODUCCION");
            cmb_dato1.Text = "1ERA PRODUCCION";
            cmb_dato2.Items.Add("2007");
            cmb_dato2.Items.Add("2008");
            cmb_dato2.Items.Add("2009");
            cmb_dato2.Items.Add("2010");
            cmb_dato2.Items.Add("2011");
            cmb_dato2.Items.Add("2012");
            cmb_dato2.Items.Add("2013");
            cmb_dato2.Items.Add("2014");
            cmb_dato2.Items.Add("2015");
            cmb_dato2.Items.Add("2016");
            cmb_dato2.Text = "2013";
        }

        public void ui_listaItem()
        {
            string codcia = this._codcia;
            string alma = this._codalma;
            string td = funciones.getValorComboBox(cmbTipo, 2);
            string numdoc = txtNumDoc.Text.Trim();
            string query = string.Empty;

            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();

            query = " select A.item,A.codarti,B.desarti,A.lote,B.unidad,A.cantidad ";
            query += "from almovd A left join alarti B on /*A.codcia=B.codcia and */A.codarti = B.codarti ";
            query += "where /*A.codcia='" + @codcia + "' and */A.alma='" + @alma + "' and A.td='" + @td + "' and A.numdoc='" + @numdoc + "' ";
            query += "order by A.item asc ;";

            try
            {
                using (SqlDataAdapter myDataAdapter = new SqlDataAdapter(query, conexion))
                {
                    DataSet myDataSet = new DataSet();
                    myDataAdapter.Fill(myDataSet, "tblData");
                    funciones.formatearDataGridView(dgvData);
                    dgvData.DataSource = myDataSet.Tables["tblData"];
                    dgvData.Columns[0].HeaderText = "Item";
                    dgvData.Columns[1].HeaderText = "Código";
                    dgvData.Columns[2].HeaderText = "Descripción del Producto";
                    dgvData.Columns[3].HeaderText = "Lote";
                    dgvData.Columns[4].HeaderText = "Unidad";
                    dgvData.Columns[5].HeaderText = "Cantidad";

                    if (dgvData.Rows.Count > 12)
                    {
                        dgvData.CurrentCell = dgvData.Rows[dgvData.Rows.Count - 1].Cells[0];
                    }

                    dgvData.Columns[0].Width = 40;
                    dgvData.Columns[1].Width = 70;
                    dgvData.Columns[2].Width = 280;
                    dgvData.Columns[3].Width = 100;
                    dgvData.Columns[4].Width = 70;
                    dgvData.Columns[5].Width = 70;

                    //dgvData.AllowUserToResizeRows = false;
                    //dgvData.AllowUserToResizeColumns = false;
                    //foreach (DataGridViewColumn column in dgvData.Columns)
                    //{
                    //    column.SortMode = DataGridViewColumnSortMode.NotSortable;
                    //}
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            conexion.Close();
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
                    switch (cmbTipo.Text.ToUpper().Substring(0, 2))
                    {
                        case "PE":
                            cmbTipo.Text = "PE  PARTE DE ENTRADA";
                            break;
                        case "PS":
                            cmbTipo.Text = "PS  PARTE DE SALIDA";
                            break;
                        default:
                            cmbTipo.Text = "PE  PARTE DE ENTRADA";
                            break;
                    }
                    string tipomov = funciones.getValorComboBox(cmbTipo, 2);
                    string query = "SELECT codmov as clave,desmov as descripcion FROM tipomov WHERE tipomov='" + @tipomov + "' and estado='V';";
                    funciones.listaComboBox(query, cmbMov, "");
                    e.Handled = true;
                    txtFecha.Focus();
                }
            }
        }

        private void cmbTipo_SelectedIndexChanged(object sender, EventArgs e)
        {
            string tipomov = funciones.getValorComboBox(cmbTipo, 2);
            string query = "SELECT codmov as clave,desmov as descripcion FROM tipomov WHERE tipomov='" + @tipomov + "' and estado='V';";
            funciones.listaComboBox(query, cmbMov, "");
        }

        private void txtFecha_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                if (UtiFechas.IsDate(txtFecha.Text))
                {
                    string fechaactual = DateTime.Now.Date.ToString("dd/MM/yyyy");

                    if (UtiFechas.compararFecha(txtFecha.Text, ">", fechaactual))
                    {
                        MessageBox.Show("La fecha no puede ser mayor a " + fechaactual, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txtFecha.Text = fechaactual;
                        e.Handled = true;
                        txtFecha.Focus();
                    }
                    else
                    {
                        e.Handled = true;
                        SendKeys.Send("{TAB}");
                    }
                }
                else
                {
                    MessageBox.Show("Fecha no válida", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    e.Handled = true;
                    txtFecha.Focus();
                }
            }
        }

        private void txtNroDoc_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                if (txtNroDoc.Text.Trim() != string.Empty)
                {
                    if (txtNroDoc.Text.Length == 8)
                    {
                        e.Handled = true;
                        SendKeys.Send("{TAB}");
                    }
                    else
                    {
                        txtNroDoc.Text = funciones.replicateCadena("0", 8 - txtNroDoc.Text.Trim().Length) + txtNroDoc.Text.Trim();
                        e.Handled = true;
                        SendKeys.Send("{TAB}");
                    }
                }
                else
                {
                    MessageBox.Show("Deberá asignar un número al documento", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    e.Handled = true;
                    txtNroDoc.Focus();
                }
            }
        }

        private void txtProveedor_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(Keys.Enter))
            {
                Provee provee = new Provee();
                string codprovee = txtProveedor.Text.Trim();
                string nombre = provee.ui_getDatos(codprovee, "NOMBRE");
                if (nombre == string.Empty || codprovee == string.Empty)
                {
                    txtProveedor.Text = "";
                    txtDscProveedor.Text = "";
                    txtRucProvee.Text = "";
                    e.Handled = true;
                    txtProveedor.Focus();
                }
                else
                {
                    txtProveedor.Text = provee.ui_getDatos(codprovee, "CODIGO");
                    txtDscProveedor.Text = provee.ui_getDatos(codprovee, "NOMBRE");
                    txtRucProvee.Text = provee.ui_getDatos(codprovee, "RUC");
                    e.Handled = true;
                    SendKeys.Send("{TAB}");
                }
            }
        }

        private void txtNomRec_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(Keys.Enter))
            {
                e.Handled = true;
                SendKeys.Send("{TAB}");
            }
        }

        private void txtCliente_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(Keys.Enter))
            {
                //Clie clie = new Clie();
                //string codclie = txtCliente.Text.Trim();
                //string nombre = clie.ui_getDatos(codclie, "NOMBRE");
                //string ruc = clie.ui_getDatos(codclie, "RUC");
                //if (nombre == string.Empty)
                //{
                //    txtCliente.Clear();
                //    txtDscCliente.Clear();
                //    txtRucClie.Clear();
                //    e.Handled = true;
                //    txtCliente.Focus();
                //}
                //else
                //{
                //    txtDscCliente.Text = nombre;
                //    txtRucClie.Text = ruc;
                //    e.Handled = true;
                //    SendKeys.Send("{TAB}");
                //}
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
                toolStripForm.Items[0].Select();
                toolStripForm.Focus();
            }
        }

        private void tabPageEX1_Click(object sender, EventArgs e)
        {

        }

        private void txtOrdenCon_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(Keys.Enter))
            {
                e.Handled = true;
                //SendKeys.Send("{TAB}");
                toolStripForm.Items[0].Select();
                toolStripForm.Focus();
            }
        }

        private void cmbMov_SelectedIndexChanged(object sender, EventArgs e)
        {
            string tipomov = funciones.getValorComboBox(cmbTipo, 2);
            string codmov = funciones.getValorComboBox(cmbMov, 2);
            string query = "SELECT codmov as clave,desmov as descripcion FROM tipomov ";
            query = query + "WHERE tipomov='" + @tipomov + "' and codmov='" + @codmov + "';";
            funciones.validarCombobox(query, cmbMov);
            limpiar();
            habilitaOpciones(tipomov, codmov);
            SendKeys.Send("{TAB}");
        }

        private void txtProveedor_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }

        private void txtProveedor_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                this._TextBoxActivo = txtProveedor;
                ui_viewprovee ui_viewprovee = new ui_viewprovee();
                ui_viewprovee._FormPadre = this;
                ui_viewprovee._clasePadre = "ui_updalmov";
                ui_viewprovee._condicionAdicional = string.Empty;
                ui_viewprovee.BringToFront();
                ui_viewprovee.ShowDialog();
                ui_viewprovee.Dispose();
            }
        }

        private void txtCliente_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                //this._TextBoxActivo = txtCliente;
                ui_viewclientes ui_viewclientes = new ui_viewclientes();
                ui_viewclientes._FormPadre = this;
                ui_viewclientes._clasePadre = "ui_updalmov";
                ui_viewclientes._condicionAdicional = string.Empty;
                ui_viewclientes.BringToFront();
                ui_viewclientes.ShowDialog();
                ui_viewclientes.Dispose();
            }
        }

        private void btnFinaliza_Click(object sender, EventArgs e)
        {
            string situa = txtEstado.Text.Substring(0, 1);
            if (situa.Equals("V"))
            {
                string td = funciones.getValorComboBox(cmbTipo, 2);
                string codmov = funciones.getValorComboBox(cmbMov, 2);
                string valorValida = validaOpciones(td, codmov);
                string rfalma = "";// funciones.getValorComboBox(cmbAlmRef, 2);

                if (valorValida.Equals("G"))
                {
                    try
                    {
                        if (this._finaliza.Equals("S"))
                        {
                            AlmovC almovc = new AlmovC();
                            GlobalVariables variables = new GlobalVariables();
                            string codcia = "01";// this._codcia;
                            string alma = this._codalma;
                            string fmod = DateTime.Now.ToString("dd/MM/yyyy");
                            string usuario = variables.getValorUsr();
                            string numdoc = txtNumDoc.Text;
                            almovc.updFinAlmovC(codcia, alma, td, numdoc, fmod, usuario);
                            if (td == "PS" && codmov == "TR")
                            {
                                automaticoparteentrada(codcia, alma, td, numdoc, rfalma, "TR", fmod);
                            }
                            txtEstado.Text = "FINALIZADO";
                            ((ui_almov)FormPadre).btnActualizar.PerformClick();
                            MessageBox.Show("Registro Finalizado", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            this.Close();
                        }
                        else
                        {
                            MessageBox.Show("Primero debe de guardar la información", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        }
                    }
                    catch (FormatException ex)
                    {
                        MessageBox.Show(ex.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("El registro ya se encuentra Finalizado", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void automaticoparteentrada(string codcia, string alma, string td, string numdoc, string almadestino, string codmovdestino, string fecdocdestino)
        {
            AlmovC almovc = new AlmovC();
            string tddestino = "PE";
            string numdocdestino = almovc.genCodAlmov(codcia, almadestino, tddestino);


            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();
            string query = "INSERT INTO almovc (codcia,alma,td,numdoc, ";
            query = query + " fecdoc,codmov,situa,rftdoc,rfndoc, ";
            query = query + " secsoli,persoli,rfnsoli,nomrec,codpro, ";
            query = query + " cencos,rfalma,glosa1,glosa2,glosa3, ";
            query = query + " codclie,fcrea,fmod,usuario,flag,almaori,tdori,numdocori) ";
            query = query + " SELECT codcia,'" + @almadestino + "','" + @tddestino + "','" + @numdocdestino + "', ";
            query = query + " fecdoc,'" + @codmovdestino + "',situa,rftdoc,rfndoc, ";
            query = query + " secsoli,persoli,rfnsoli,nomrec,codpro, ";
            query = query + " cencos,alma,glosa1,glosa2,glosa3, ";
            query = query + " codclie,fcrea,fmod,usuario,'ALMA',alma,td,numdoc FROM almovc WHERE /*codcia='" + @codcia + "' ";
            query = query + " and*/ alma='" + @alma + "' and td='" + @td + "' and numdoc='" + @numdoc + "' ; ";

            query = query + " INSERT INTO almovd (codcia,alma,td,numdoc, ";
            query = query + " item,codarti,cantidad,certificado,lote,fprod,fven,";
            query = query + " analisis,calidad,glosana1,glosana2,glosana3) ";
            query = query + " SELECT codcia,'" + @almadestino + "','" + @tddestino + "','" + @numdocdestino + "', ";
            query = query + " item,codarti,cantidad,certificado,lote,fprod,fven,";
            query = query + " analisis,calidad,glosana1,glosana2,glosana3 FROM almovd WHERE /*codcia='" + @codcia + "' ";
            query = query + " and*/ alma='" + @alma + "' and td='" + @td + "' and numdoc='" + @numdoc + "' ; ";
            query = query + " UPDATE alalma SET nrope='" + @numdocdestino + "' where codcia='" + @codcia + "' and codalma='" + @almadestino + "'; ";
            MessageBox.Show("Parte de Entrada Generado :" + @numdocdestino);
            try
            {
                SqlCommand myCommand = new SqlCommand(query, conexion);
                myCommand.ExecuteNonQuery();
                myCommand.Dispose();
            }
            catch (SqlException e)
            {
                MessageBox.Show(e.Message, "Aviso Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);

            }
            conexion.Close();
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            if (this._operacion.Equals("EDITAR"))
            {
                string tipo = funciones.getValorComboBox(cmbTipo, 2);
                if (tipo.Equals("PE"))
                {
                    ui_almovd ui_almovd = new ui_almovd();
                    ui_almovd._FormPadre = this;
                    ui_almovd._codcia = this._codcia;
                    ui_almovd._alma = this._codalma;
                    ui_almovd._td = cmbTipo.Text;
                    ui_almovd._numdoc = txtNumDoc.Text.Trim();
                    ui_almovd.Activate();
                    ui_almovd.BringToFront();
                    ui_almovd.agregar();
                    ui_almovd.ShowDialog();
                    ui_almovd.Dispose();
                }
                else
                {
                    ui_almovdps ui_almovd = new ui_almovdps();
                    ui_almovd._FormPadre = this;
                    ui_almovd._formpadre = "ui_updalmov";
                    ui_almovd._codcia = this._codcia;
                    ui_almovd._alma = this._codalma;
                    ui_almovd._td = cmbTipo.Text;
                    ui_almovd._numdoc = txtNumDoc.Text.Trim();
                    ui_almovd.Activate();
                    ui_almovd.BringToFront();
                    ui_almovd.agregar();
                    ui_almovd.ShowDialog();
                    ui_almovd.Dispose();
                }
            }
            else
            {
                MessageBox.Show("Primero debe de grabar el registro de cabecera", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void tabPageEX2_Click(object sender, EventArgs e)
        {

        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            Int32 selectedCellCount = dgvData.GetCellCount(DataGridViewElementStates.Selected);
            if (selectedCellCount > 0)
            {
                string codcia = this._codcia;
                string alma = this._codalma;
                string tipo = funciones.getValorComboBox(cmbTipo, 2);
                string td = cmbTipo.Text;
                string numdoc = txtNumDoc.Text.Trim();
                string item = dgvData.Rows[dgvData.SelectedCells[1].RowIndex].Cells[0].Value.ToString();
                if (tipo.Equals("PE"))
                {
                    ui_almovd ui_almovd = new ui_almovd();
                    ui_almovd._FormPadre = this;
                    ui_almovd._codcia = codcia;
                    ui_almovd._alma = alma;
                    ui_almovd._numdoc = numdoc;
                    ui_almovd._td = td;
                    ui_almovd.Activate();
                    ui_almovd.BringToFront();
                    ui_almovd.editar(item);
                    ui_almovd.ShowDialog();
                    ui_almovd.Dispose();
                }
                else
                {
                    ui_almovdps ui_almovd = new ui_almovdps();
                    ui_almovd._FormPadre = this;
                    ui_almovd._formpadre = "ui_updalmov";
                    ui_almovd._codcia = codcia;
                    ui_almovd._alma = alma;
                    ui_almovd._numdoc = numdoc;
                    ui_almovd._td = td;
                    ui_almovd.Activate();
                    ui_almovd.BringToFront();
                    ui_almovd.editar(item);
                    ui_almovd.ShowDialog();
                    ui_almovd.Dispose();
                }
            }
            else
            {
                MessageBox.Show("No ha seleccionado registro a Editar", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            Int32 selectedCellCount =
            dgvData.GetCellCount(DataGridViewElementStates.Selected);
            if (selectedCellCount > 0)
            {
                string codcia = "01";// this._codcia;
                string alma = this._codalma;
                string desalma = this._desalma;
                string td = funciones.getValorComboBox(cmbTipo, 2);
                string numdoc = txtNumDoc.Text.Trim();
                string item = dgvData.Rows[dgvData.SelectedCells[1].RowIndex].Cells[0].Value.ToString();
                DialogResult resultado = MessageBox.Show("¿Desea eliminar el Item " + @item + "?",
                "Consulta Importante",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);
                if (resultado == DialogResult.Yes)
                {
                    DataTable dt = new DataTable();
                    dt = ui_ListaEnlazados(codcia, alma, td, numdoc, item);
                    if (dt.Rows.Count > 0)
                    {
                        MessageBox.Show("Existen movimientos enlazados al item, no se puede eliminar", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        ui_movligados ui_movligados = new ui_movligados();
                        ui_movligados._codcia = codcia;
                        ui_movligados._alma = alma;
                        ui_movligados._desalma = desalma;
                        ui_movligados._td = td;
                        ui_movligados._numdoc = numdoc;
                        ui_movligados._item = item;
                        ui_movligados._reporte = "D";
                        ui_movligados.Activate();
                        ui_movligados.BringToFront();
                        ui_movligados.ShowDialog();
                        ui_movligados.Dispose();
                    }
                    else
                    {
                        AlmovD almovd = new AlmovD();
                        almovd.delAlmovD(codcia, alma, td, numdoc, item);

                        Alstock alstock = new Alstock();
                        alstock.recalcularStockAlmacen(codcia, alma);

                        ui_listaItem();
                    }
                }
            }
            else
            {
                MessageBox.Show("No ha seleccionado registro a Eliminar", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private DataTable ui_ListaEnlazados(string codcia, string alma, string td, string numdoc, string item)
        {
            DataTable dtmov = new DataTable();
            try
            {
                if (td.Equals("PE"))
                {
                    string query;
                    SqlConnection conexion = new SqlConnection();
                    conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
                    conexion.Open();
                    query = " select A.alma,A.td,A.numdoc,A.fecdoc,A.codmov,B.item,C.desarti,B.lote,";
                    query = query + " B.cantidad from almovc A left join almovd B on ";
                    query = query + " A.codcia=B.codcia and A.alma=B.alma and A.td=B.td and A.numdoc=B.numdoc ";
                    query = query + " left join alarti C on B.codcia=C.codcia and B.codarti=C.codarti ";
                    query = query + " where A.codcia='" + @codcia + "' and A.alma='" + @alma + "' and A.td='PS' and B.lote in ";
                    query = query + " (Select lote from almovd where codcia='" + @codcia + "' and alma='" + @alma + "' ";
                    query = query + " and td='" + @td + "' and numdoc='" + @numdoc + "' and item='" + @item + "') ";
                    query = query + " order by A.alma asc ,A.td asc ,A.numdoc asc;";

                    SqlDataAdapter damov = new SqlDataAdapter();
                    damov.SelectCommand = new SqlCommand(query, conexion);
                    damov.Fill(dtmov);
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            return dtmov;
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            string codcia = this._codcia;
            string alma = this._codalma;
            string td = funciones.getValorComboBox(cmbTipo, 2);
            string numdoc = txtNumDoc.Text.Trim();
            string situa = txtEstado.Text.Substring(0, 1);

            if (situa.Equals("F"))
            {
                AlmovC almovc = new AlmovC();
                string strPrinter = almovc.printAlmov(codcia, alma, td, numdoc);
                ui_reportetxt ui_reportetxt = new ui_reportetxt();
                ui_reportetxt._texto = strPrinter;
                ui_reportetxt.Activate();
                ui_reportetxt.BringToFront();
                ui_reportetxt.ShowDialog();
                ui_reportetxt.Dispose();
            }
            else
            {
                MessageBox.Show("No se puede imprimir Parte no Finalizado", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }

        private void txtDscProveedor_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnAddClie_Click(object sender, EventArgs e)
        {
            ui_updclie ui_updclie = new ui_updclie();
            ui_updclie._FormPadre = this;
            ui_updclie.setValores("ui_updalmov");
            ui_updclie.Activate();
            ui_updclie.agregar();
            ui_updclie.ui_actualizaComboBox();
            ui_updclie.BringToFront();
            ui_updclie.ShowDialog();
            ui_updclie.Dispose();
        }

        public void ui_ActualizarClie()
        {
            //Clie clie = new Clie();
            //string codclie = txtCliente.Text.Trim();
            //string nombre = clie.ui_getDatos(codclie, "NOMBRE");
            //if (nombre == string.Empty)
            //{
            //    txtCliente.Text = string.Empty;
            //    txtDscCliente.Text = string.Empty;
            //    txtRucClie.Text = string.Empty;
            //}
            //else
            //{
            //    txtCliente.Text = clie.ui_getDatos(codclie, "CODIGO");
            //    txtDscCliente.Text = clie.ui_getDatos(codclie, "NOMBRE");
            //    txtRucClie.Text = clie.ui_getDatos(codclie, "RUC");
            //}
        }

        public void ui_ActualizarProvee()
        {
            Provee provee = new Provee();
            string codprovee = txtProveedor.Text.Trim();
            string nombre = provee.ui_getDatos(codprovee, "NOMBRE");
            if (nombre == string.Empty)
            {
                txtProveedor.Text = "";
                txtDscProveedor.Text = "";
                txtRucProvee.Text = "";
            }
            else
            {
                txtProveedor.Text = provee.ui_getDatos(codprovee, "CODIGO");
                txtDscProveedor.Text = provee.ui_getDatos(codprovee, "NOMBRE");
                txtRucProvee.Text = provee.ui_getDatos(codprovee, "RUC");
            }
        }

        private void btnUpdClie_Click(object sender, EventArgs e)
        {
            //string codclie = txtCliente.Text.Trim();
            //if (codclie != string.Empty)
            //{
            //    Clie clie = new Clie();
            //    string nombre = clie.ui_getDatos(codclie, "NOMBRE");
            //    if (nombre == string.Empty)
            //    {
            //        MessageBox.Show("Código de Cliente a editar no existe", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            //    }
            //    else
            //    {
            //        ui_updclie ui_updclie = new ui_updclie();
            //        ui_updclie._FormPadre = this;
            //        ui_updclie.Activate();
            //        ui_updclie.setValores("ui_updalmov");
            //        ui_updclie.ui_actualizaComboBox();
            //        ui_updclie.BringToFront();
            //        ui_updclie.editar(codclie);
            //        ui_updclie.ShowDialog();
            //        ui_updclie.Dispose();
            //    }
            //}
            //else
            //{
            //    MessageBox.Show("No ha ingresado Código de Cliente a editar", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            //}
        }

        private void txtSerie_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                if (txtSerie.Text.Trim() != string.Empty)
                {
                    if (txtSerie.Text.Length == 4)
                    {
                        e.Handled = true;
                        txtNroDoc.Focus();
                    }
                    else
                    {
                        txtSerie.Text = funciones.replicateCadena("0", 4 - txtSerie.Text.Trim().Length) + txtSerie.Text.Trim();
                        e.Handled = true;
                        txtNroDoc.Focus();
                    }
                }
                else
                {
                    MessageBox.Show("Deberá asignar un número de serie del documento", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    e.Handled = true;
                    txtSerie.Focus();
                }
            }
        }

        private void btnAddProvee_Click(object sender, EventArgs e)
        {
            ui_updprovee ui_updprovee = new ui_updprovee();
            ui_updprovee._FormPadre = this;
            ui_updprovee.setValores("ui_updalmov");
            ui_updprovee._tipo = "M";
            ui_updprovee.Activate();
            ui_updprovee.agregar();
            ui_updprovee.BringToFront();
            ui_updprovee.ui_listarComboBox();
            ui_updprovee.ShowDialog();
            ui_updprovee.Dispose();
        }

        private void btnUpdProvee_Click(object sender, EventArgs e)
        {
            string codprovee = txtProveedor.Text.Trim();
            if (codprovee != string.Empty)
            {
                Provee provee = new Provee();
                string nombre = provee.ui_getDatos(codprovee, "NOMBRE");
                if (nombre == string.Empty)
                {
                    MessageBox.Show("Código de Proveedor a editar no existe", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
                else
                {
                    ui_updprovee ui_updprovee = new ui_updprovee();
                    ui_updprovee._FormPadre = this;
                    ui_updprovee._tipo = "M";
                    ui_updprovee.setValores("ui_updalmov");
                    ui_updprovee.Activate();
                    ui_updprovee.editar(codprovee);
                    ui_updprovee.BringToFront();
                    ui_updprovee.ui_listarComboBox();
                    ui_updprovee.ShowDialog();
                    ui_updprovee.Dispose();
                }
            }
            else
            {
                MessageBox.Show("No ha ingresado Código de Proveedor a editar", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void tabControl_SelectedIndexChanging(object sender, Dotnetrix.Controls.TabPageChangeEventArgs e)
        {

        }

        private void cmb_dato1_Validated(object sender, EventArgs e)
        {
            txtGlosa1.Text = cmb_dato1.Text + ' ' + cmb_dato2.Text;
        }

        private void cmb_dato2_Validated(object sender, EventArgs e)
        {
            txtGlosa1.Text = cmb_dato1.Text + ' ' + cmb_dato2.Text;
        }

        private void txtGlosa3_TextChanged(object sender, EventArgs e)
        {

        }

        private void cmbTipoDoc_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtSerie.Clear();
            txtNroDoc.Clear();
            txtSerie.Focus();
        }

        private void txtSerie_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void txtSerie_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyData == Keys.Tab)
            {
                if (txtSerie.Text.Trim() != string.Empty)
                {
                    if (txtSerie.Text.Length == 4)
                    {
                        txtNroDoc.Focus();
                    }
                    else
                    {
                        txtSerie.Text = funciones.replicateCadena("0", 4 - txtSerie.Text.Trim().Length) + txtSerie.Text.Trim();
                        txtNroDoc.Focus();
                    }
                }
                else
                {
                    MessageBox.Show("Deberá asignar un número de serie del documento", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    txtSerie.Focus();
                }
                e.IsInputKey = true;
            }
            if (e.KeyData == (Keys.Tab | Keys.Shift))
            {
                MessageBox.Show("Shift + Tab");
                e.IsInputKey = true;
            }
        }

        private void txtNroDoc_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyData == Keys.Tab)
            {
                if (txtNroDoc.Text.Trim() != string.Empty)
                {
                    if (txtNroDoc.Text.Length == 8)
                    {
                        //SendKeys.Send("{TAB}");
                        txtProveedor.Focus();
                    }
                    else
                    {
                        txtNroDoc.Text = funciones.replicateCadena("0", 8 - txtNroDoc.Text.Trim().Length) + txtNroDoc.Text.Trim();
                        //SendKeys.Send("{TAB}");
                        txtProveedor.Focus();
                    }
                }
                else
                {
                    MessageBox.Show("Deberá asignar un número al documento", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    txtNroDoc.Focus();
                }
                e.IsInputKey = true;
            }
            if (e.KeyData == (Keys.Tab | Keys.Shift))
            {
                //MessageBox.Show("Shift + Tab");
                e.IsInputKey = true;
            }
        }
    }
}