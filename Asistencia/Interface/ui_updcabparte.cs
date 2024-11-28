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
    public partial class ui_updcabparte : ui_form
    {
        string _operacion;
        string _codcia;

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

        public ui_updcabparte()
        {
            InitializeComponent();
        }

        private void ui_updcabparte_Load(object sender, EventArgs e)
        {
            GlobalVariables gv = new GlobalVariables();
            Funciones funciones = new Funciones();
            this._codcia = gv.getValorCia();
            string codcia = this._codcia;
            string usuario = gv.getValorUsr();
            string query = " Select A.codalma as clave,A.desalma as descripcion ";
            query = query + " from alalma A inner join almausr B on A.codcia=B.codcia and A.codalma=B.codalma ";
            query = query + " where A.codcia='" + @codcia + "' and A.estado='V' and B.idusr='" + @usuario + "' order by 1 asc; ";
            funciones.listaComboBox(query, cmbAlmacen, "");
            ui_ActualizaComboBox();
            limpiarGen();
        }


        public void ui_ActualizaComboBox()
        {
            MaesGen maesgen = new MaesGen();
            Funciones funciones = new Funciones();
            string query;
            cmbTipo.Text = "PE  PARTE DE ENTRADA";
            string tipomov = funciones.getValorComboBox(cmbTipo, 2);
            string codalma = funciones.getValorComboBox(cmbAlmacen, 2);
            string codcia = this._codcia;
            query = "SELECT codmov as clave,desmov as descripcion FROM tipomov WHERE tipomov='" + @tipomov + "' and estado='V';";
            funciones.listaComboBox(query, cmbMov, "");
            maesgen.listaDetMaesGen("160", cmbTipoDoc, "B");
            maesgen.listaDetMaesGen("150", cmbSeccion, "B");
            query = "SELECT codcencos as clave,descencos as descripcion FROM cencos WHERE codcia='"+@codcia+"' and codalma='" +@codalma  + "' and estado='V';";
            funciones.listaComboBox(query, cmbCenCos, "");
            query = "SELECT codalma as clave,desalma as descripcion FROM alalma WHERE codcia='" + @codcia + "' and codalma<>'" + @codalma + "' and estado='V';";
            funciones.listaComboBox(query, cmbAlmRef, "B");

        }

        public void limpiarGen()
        {
            txtFecha.Clear();
            cmbMov.Text = "";
            cmbTipoDoc.Text = "";
            txtNroDoc.Clear();
            txtProveedor.Clear();
            lblRuc.Text = "";
            lblProveedor.Text = "";
            txtOrdenCon.Clear();
            cmbSeccion.Text = "";
            cmbSolicitante.Text = "";
            txtNomRec.Clear();
            cmbCenCos.Text = "";
            txtCliente.Clear();
            lblCliente.Text = "";
            txtGlosa1.Clear();
            txtGlosa2.Clear();
            txtGlosa3.Clear();
            cmbAlmRef.Text = "";
            txtEstado.Clear();
            txtFecCrea.Clear();
            txtFecMod.Clear();
            txtUsuario.Clear();
            tabControl.SelectTab(0);
        }

        public void editar(string codcia,string alma,string td,string numdoc)
        {
            string query;
            this._operacion = "EDITAR";
            
            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();
            query = "SELECT * FROM almovc where codcia='"+@codcia+"' and alma='" + @alma + "' and td='"+@td+"' and numdoc='"+@numdoc+"'; ";

            try
            {
                SqlCommand myCommand = new SqlCommand(query, conexion);
                SqlDataReader myReader = myCommand.ExecuteReader();

                if (myReader.Read())
                {

                    Funciones funciones = new Funciones();
                    MaesGen maesgen = new MaesGen();
                    txtFecha.Text = myReader.GetString(myReader.GetOrdinal("fecdoc"));
                    query = " SELECT codmov as clave,desmov as descripcion FROM tipomov WHERE ";
                    query = query + " tipomov='" + @myReader.GetString(myReader.GetOrdinal("td")) + "' and codmov='" + @myReader.GetString(myReader.GetOrdinal("codmov")) + "';";
                    funciones.consultaComboBox(query, cmbMov);
                    maesgen.consultaDetMaesGen("160", myReader.GetString(myReader.GetOrdinal("rftdoc")), cmbTipoDoc);
                    txtNroDoc.Text = myReader.GetString(myReader.GetOrdinal("rfndoc"));
                    txtProveedor.Text = myReader.GetString(myReader.GetOrdinal("codpro"));
                    Provee provee = new Provee();
                    lblProveedor.Text = provee.ui_getDatos(myReader.GetString(myReader.GetOrdinal("codpro")), "NOMBRE");
                    lblRuc.Text = provee.ui_getDatos(myReader.GetString(myReader.GetOrdinal("codpro")), "RUC");
                    txtOrdenCon.Text=myReader.GetString(myReader.GetOrdinal("rfnsoli"));
                    maesgen.consultaDetMaesGen("150", myReader.GetString(myReader.GetOrdinal("secsoli")), cmbSeccion);
                    query = "SELECT codsoli as clave,dessoli as descripcion FROM solicita ";
                    query = query + "WHERE codcia='"+@codcia+"' and secsoli='" + @myReader.GetString(myReader.GetOrdinal("secsoli")) + "' and codsoli='" + @myReader.GetString(myReader.GetOrdinal("persoli")) + "';";
                    funciones.validarCombobox(query, cmbSolicitante);
                    txtNomRec.Text = myReader.GetString(myReader.GetOrdinal("nomrec"));
                    query = "SELECT codcencos as clave,descencos as descripcion FROM cencos WHERE codcia='"+@codcia+"' and ";
                    query = query + " codalma='" + @myReader.GetString(myReader.GetOrdinal("alma")) + "' and ";
                    query = query + " codcencos='" + @myReader.GetString(myReader.GetOrdinal("cencos")) + "';";
                    funciones.consultaComboBox(query, cmbCenCos);
                    txtCliente.Text = myReader.GetString(myReader.GetOrdinal("codclie"));
                    Clie clie = new Clie();
                    lblCliente.Text = clie.ui_getDatos(myReader.GetString(myReader.GetOrdinal("codclie")),"NOMBRE");
                    txtGlosa1.Text = myReader.GetString(myReader.GetOrdinal("glosa1"));
                    txtGlosa2.Text = myReader.GetString(myReader.GetOrdinal("glosa2"));
                    txtGlosa3.Text = myReader.GetString(myReader.GetOrdinal("glosa3"));
                    query = "SELECT codalma as clave,desalma as descripcion FROM alalma WHERE codcia='"+@codcia+"' and codalma='" + @myReader.GetString(myReader.GetOrdinal("rfalma")) + "' ;";
                    funciones.consultaComboBox(query, cmbAlmRef);
                    txtFecCrea.Text = myReader.GetString(myReader.GetOrdinal("fcrea"));
                    txtFecMod.Text = myReader.GetString(myReader.GetOrdinal("fmod"));
                    txtUsuario.Text = myReader.GetString(myReader.GetOrdinal("usuario"));
                    if (myReader.GetString(myReader.GetOrdinal("situa")).Equals("V"))
                    {
                        txtEstado.Text = "VIGENTE";
                    }
                    else
                    {
                        if (myReader.GetString(myReader.GetOrdinal("situa")).Equals("A"))
                        {
                            txtEstado.Text = "ANULADO";
                        }
                        else
                        {
                            txtEstado.Text = "FINALIZADO";
                        }
                    }
                    
                }
                habilitaOpciones(myReader.GetString(myReader.GetOrdinal("td")), myReader.GetString(myReader.GetOrdinal("codmov")));
                ui_listaItem();
                tabControl.SelectTab(0);
                cmbMov.Focus();
            }
            catch (Exception)
            {

                MessageBox.Show("Parte no encontrado", "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }

            conexion.Close();
        }
              
        
        private void btnAceptar_Click(object sender, EventArgs e)
        {
            Funciones funciones = new Funciones();
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
                    string codcia = this._codcia;
                    string alma = funciones.getValorComboBox(cmbAlmacen, 2);
                    string fecdoc = txtFecha.Text;
                    string rftdoc = funciones.getValorComboBox(cmbTipoDoc, 4);
                    string rfndoc = txtNroDoc.Text.Trim();
                    string codpro = txtProveedor.Text;
                    string secsoli = funciones.getValorComboBox(cmbSeccion, 4);
                    string persoli = funciones.getValorComboBox(cmbSolicitante, 2);
                    string rfnsoli = txtOrdenCon.Text.Trim();
                    string nomrec = txtNomRec.Text;
                    string cencos = funciones.getValorComboBox(cmbCenCos, 2);
                    string codclie = txtCliente.Text;
                    string glosa1 = txtGlosa1.Text.Trim();
                    string glosa2 = txtGlosa2.Text.Trim();
                    string glosa3 = txtGlosa3.Text.Trim();
                    string rfalma = funciones.getValorComboBox(cmbAlmRef, 2);
                    string situa = txtEstado.Text.Substring(0, 1);
                    string fcrea = txtFecCrea.Text;
                    string fmod = DateTime.Now.ToString("dd/MM/yyyy");
                    string usuario = variables.getValorUsr();
                    string numdoc = txtNumDoc.Text;
                    almovc.updAlmovC(operacion, codcia, alma, td, numdoc, fecdoc, codmov, situa, rftdoc, rfndoc,
                        secsoli, persoli, rfnsoli, nomrec, codpro, cencos, rfalma, glosa1, glosa2, glosa3,
                        codclie, fcrea, fmod, usuario,"ALMA");
                    MessageBox.Show("Datos de Cabecera actualizados", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                   
                }
                catch (FormatException ex)

                {
                    MessageBox.Show(ex.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
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
                Funciones funciones = new Funciones();
                if (cmbSeccion.Text != String.Empty)
                {
                    MaesGen maesgen = new MaesGen();
                    maesgen.validarDetMaesGen("150", cmbSeccion, cmbSeccion.Text);
                }
                
                string codcia=this._codcia;
                string secsoli = funciones.getValorComboBox(cmbSeccion, 4);
                string query = "SELECT codsoli as clave,dessoli as descripcion FROM solicita ";
                query = query + " WHERE codcia='"+@codcia+"' and secsoli='" + @secsoli + "' ;";
                funciones.listaComboBox(query, cmbSolicitante, "");
                e.Handled = true;
                SendKeys.Send("{TAB}");

            }
        }

        private void cmbMov_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(Keys.Enter))
            {
                if (cmbMov.Text != String.Empty)
                {
                    Funciones funciones = new Funciones();
                    string tipomov = funciones.getValorComboBox(cmbTipo, 2);
                    string codmov = funciones.getValorComboBox(cmbMov, 2);
                    string query = "SELECT codmov as clave,desmov as descripcion FROM tipomov ";
                    query = query + "WHERE tipomov='" + @tipomov + "' and codmov='" + @codmov + "';";
                    funciones.validarCombobox(query, cmbMov);
                    limpiarMov();
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
                if (cmbSolicitante.Text != String.Empty)
                {
                    Funciones funciones = new Funciones();
                    string codcia = this._codcia;
                    string secsoli = funciones.getValorComboBox(cmbSeccion, 4);
                    string codsoli = funciones.getValorComboBox(cmbSolicitante, 2);
                    string query = "SELECT codsoli as clave,dessoli as descripcion FROM solicita ";
                    query = query + "WHERE codcia='"+@codcia+"' and secsoli='" + @secsoli + "' and codsoli='" + @codsoli + "';";
                    funciones.validarCombobox(query, cmbSolicitante);
                }
                e.Handled = true;
                SendKeys.Send("{TAB}");

            }
        }

        private void cmbCenCos_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(Keys.Enter))
            {
                if (cmbCenCos.Text != String.Empty)
                {
                    Funciones funciones = new Funciones();
                    string codcia=this._codcia;
                    string codalma = funciones.getValorComboBox(cmbAlmacen, 2);
                    string codcencos = funciones.getValorComboBox(cmbCenCos, 2);
                    string query = "SELECT codcencos as clave,descencos as descripcion FROM cencos ";
                    query = query + "WHERE codcia='"+@codcia+"' and codalma='" + @codalma + "' and codcencos='" + @codcencos + "';";
                    funciones.validarCombobox(query, cmbCenCos);
                }
                e.Handled = true;
                SendKeys.Send("{TAB}");

            }
        }

        private void cmbAlmRef_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(Keys.Enter))
            {
                if (cmbAlmRef.Text != String.Empty)
                {
                    Funciones funciones = new Funciones();
                    string codcia = this._codcia;
                    string codalma = funciones.getValorComboBox(cmbAlmRef, 2);
                    string query = " SELECT codalma as clave,desalma as descripcion FROM alalma ";
                    query=query+" WHERE codcia='"+@codcia+"' and codalma='" + @codalma + "';";
                    funciones.validarCombobox(query, cmbAlmRef);
                }
                e.Handled = true;
                SendKeys.Send("{TAB}");

            }
        }

        private void cmbSeccion_SelectedIndexChanged(object sender, EventArgs e)
        {
            Funciones funciones = new Funciones();
            string codcia = this._codcia;
            string secsoli = funciones.getValorComboBox(cmbSeccion, 4);
            string query = "SELECT codsoli as clave,dessoli as descripcion FROM solicita ";
            query = query + " WHERE codcia='"+@codcia+"' and secsoli='" + @secsoli + "' ;";
            funciones.listaComboBox(query, cmbSolicitante, "");

        }

        private void limpiarMov()
        {
            
            txtProveedor.Clear();
            lblProveedor.Text = "";
            lblRuc.Text = "";
            cmbSeccion.Text = "";
            cmbSolicitante.Text = "";
            txtNomRec.Text = "";
            cmbAlmRef.Text = "";
            txtCliente.Text = "";
            lblCliente.Text = "";
            cmbTipoDoc.Text = "";
            txtNroDoc.Text = "";
            cmbCenCos.Text = "";
            txtGlosa1.Clear();
            txtGlosa2.Clear();
            txtGlosa3.Clear();
            txtOrdenCon.Clear();

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
                    }
                    else
                    {
                        txtProveedor.Enabled = false;
                    }

                    if (myReader.GetString(myReader.GetOrdinal("ordencon")).Equals("1"))
                    {
                        txtOrdenCon.Enabled = true;
                        cmbSeccion.Enabled = false;
                        cmbSolicitante.Enabled = false;
                        txtNomRec.Enabled = false;
                    }
                    else
                    {
                        txtOrdenCon.Enabled = false;

                        if (myReader.GetString(myReader.GetOrdinal("solicitante")).Equals("1"))

                        {
                            cmbSeccion.Enabled = true;
                            cmbSolicitante.Enabled = true;
                            txtNomRec.Enabled = true;
                        }
                        else
                        {
                            cmbSeccion.Enabled = false;
                            cmbSolicitante.Enabled = false;
                            txtNomRec.Enabled = false;
                        }
                    }

                    if (myReader.GetString(myReader.GetOrdinal("almacen")).Equals("1"))
                    {
                        cmbAlmRef.Enabled = true;
                    }
                    else
                    {
                        cmbAlmRef.Enabled = false;
                    }

                    if (myReader.GetString(myReader.GetOrdinal("cliente")).Equals("1"))
                    {
                        txtCliente.Enabled = true;
                    }
                    else
                    {
                        txtCliente.Enabled = false;
                    }

                    if (myReader.GetString(myReader.GetOrdinal("docref")).Equals("1"))
                    {
                        cmbTipoDoc.Enabled = true;
                        txtNroDoc.Enabled = true;
                    }
                    else
                    {
                        cmbTipoDoc.Enabled = false;
                        txtNroDoc.Enabled = false;
                    }

                    if (myReader.GetString(myReader.GetOrdinal("cencos")).Equals("1"))
                    {
                        cmbCenCos.Enabled = true;
                    }
                    else
                    {
                        cmbCenCos.Enabled = false;
                    }

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
            Funciones funciones = new Funciones();
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
                query = "SELECT codmov as clave,desmov as descripcion FROM tipomov WHERE tipomov='" + @tipomov + "' and codmov='"+@codmov+"';";
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
                            if (cmbSeccion.Text == string.Empty && valorValida == "G")
                            {
                                valorValida = "B";
                                MessageBox.Show("No ha seleccionado Sección", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                tabControl.SelectTab(0);
                                cmbSeccion.Focus();
                            }
                            if (cmbSeccion.Text != string.Empty && valorValida == "G")
                            {
                                string resultado = maesgen.verificaComboBoxMaesGen("150", cmbSeccion.Text.Trim());
                                if (resultado.Trim() == string.Empty)
                                {
                                    valorValida = "B";
                                    MessageBox.Show("Dato incorrecto en Sección", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    tabControl.SelectTab(0);
                                    cmbSeccion.Focus();
                                }
                            }
                            if (cmbSolicitante.Text == string.Empty && valorValida == "G")
                            {
                                valorValida = "B";
                                MessageBox.Show("No ha seleccionado Solicitante", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                tabControl.SelectTab(0);
                                cmbSolicitante.Focus();
                            }
                            if (cmbSolicitante.Text != string.Empty && valorValida == "G")
                            {
                                string secsoli = funciones.getValorComboBox(cmbSeccion, 4);
                                string codsoli = funciones.getValorComboBox(cmbSolicitante, 2);
                                query = "SELECT codsoli as clave,dessoli as descripcion FROM solicita ";
                                query = query + "WHERE codcia='"+@codcia+"' and secsoli='" + @secsoli + "' and codsoli='" + @codsoli + "';";
                                string resultado = funciones.verificaItemComboBox(query, cmbSolicitante);
                                if (resultado.Trim() == string.Empty)
                                {
                                    valorValida = "B";
                                    MessageBox.Show("Dato incorrecto en Solicitante", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    tabControl.SelectTab(0);
                                    cmbSolicitante.Focus();
                                }
                            }
                            if (txtNomRec.Text == string.Empty && valorValida == "G")
                            {
                                valorValida = "B";
                                MessageBox.Show("No ha ingresado Nombre del Receptor", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                tabControl.SelectTab(0);
                                txtNomRec.Focus();
                            }
                        }

                    }
                    if (myReader.GetString(myReader.GetOrdinal("cencos")).Equals("1"))
                    {
                        if (cmbCenCos.Text == string.Empty && valorValida == "G")
                        {
                            valorValida = "B";
                            MessageBox.Show("No ha seleccionado Centro de Costo", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            tabControl.SelectTab(0);
                            cmbCenCos.Focus();
                        }
                        if (cmbCenCos.Text != string.Empty && valorValida == "G")
                        {
                            string codalma = funciones.getValorComboBox(cmbAlmacen, 2);
                            string codcencos = funciones.getValorComboBox(cmbCenCos, 2);
                            query = "SELECT codcencos as clave,descencos as descripcion FROM cencos WHERE ";
                            query = query + " codcia='"+@codcia+"' and codalma='" + @codalma + "' and ";
                            query = query + " codcencos='" + @codcencos + "';";
                            string resultado = funciones.verificaItemComboBox(query, cmbCenCos);
                            if (resultado.Trim() == string.Empty)
                            {
                                valorValida = "B";
                                MessageBox.Show("Dato incorrecto en Centro de Costo", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                tabControl.SelectTab(0);
                                cmbCenCos.Focus();
                            }
                        }
                    }
                    if (myReader.GetString(myReader.GetOrdinal("cliente")).Equals("1"))
                    {
                        if (txtCliente.Text == string.Empty && valorValida == "G")
                        {
                            valorValida = "B";
                            MessageBox.Show("No ha ingresado Cliente", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            tabControl.SelectTab(0);
                            txtCliente.Focus();
                        }
                        Clie clie = new Clie();
                        if (txtCliente.Text != string.Empty && valorValida == "G" && clie.ui_getDatos(txtCliente.Text, "NOMBRE") == string.Empty)
                        {
                            valorValida = "B";
                            MessageBox.Show("Cliente no existe", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            tabControl.SelectTab(0);
                            txtCliente.Focus();
                        }
                    }
                    if (myReader.GetString(myReader.GetOrdinal("almacen")).Equals("1"))
                    {
                        if (cmbAlmRef.Text == string.Empty && valorValida == "G")
                        {
                            valorValida = "B";
                            MessageBox.Show("No ha seleccionado Almacén Referencial", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            tabControl.SelectTab(0);
                            cmbAlmRef.Focus();
                        }
                        if (cmbAlmRef.Text != string.Empty && valorValida == "G")
                        {
                            string codalma = funciones.getValorComboBox(cmbAlmRef, 2);
                            query = "SELECT codalma as clave,desalma as descripcion FROM alalma WHERE codcia='"+@codcia+"' and codalma='" + @codalma + "' ;";
                            string resultado = funciones.verificaItemComboBox(query, cmbAlmRef);
                            if (resultado.Trim() == string.Empty)
                            {
                                valorValida = "B";
                                MessageBox.Show("Dato incorrecto en Almacén Referencial", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                tabControl.SelectTab(0);
                                cmbAlmRef.Focus();
                            }
                        }
                    }
                    if (myReader.GetString(myReader.GetOrdinal("glosa")).Equals("1"))
                    {
                        if (txtGlosa1.Text == string.Empty && valorValida == "G")
                        {
                            valorValida = "B";
                            MessageBox.Show("No ha ingresado Glosa", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            tabControl.SelectTab(0);
                            txtGlosa1.Focus();
                        }
                    }
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
            
        }

        public void ui_listaItem()
        {
            Funciones funciones = new Funciones();
            string codcia = this._codcia;
            string alma = funciones.getValorComboBox(cmbAlmacen, 2);
            string td = funciones.getValorComboBox(cmbTipo,2);
            string numdoc = txtNumDoc.Text.Trim();
            string query = string.Empty;

            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();
            
                query = " select A.item,A.codarti,B.desarti,A.lote,B.unidad,A.cantidad ";
                query = query + " from almovd A left join alarti B on A.codcia=B.codcia and A.codarti = B.codarti";
                query = query + " where A.codcia='"+@codcia+"' and A.alma='" + @alma + "' and A.td='" + @td + "' and A.numdoc='" + @numdoc + "' ";
                query = query + " order by A.item asc ;";
           
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
                    dgvData.Columns[2].Width = 250;
                    dgvData.Columns[3].Width = 120;
                    dgvData.Columns[4].Width = 75;
                    dgvData.Columns[5].Width = 75;
                   

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
                    Funciones funciones = new Funciones();
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
            Funciones funciones = new Funciones();
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
                        MessageBox.Show("La fecha no puede ser mayor a "+fechaactual, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            if (e.KeyChar == (char)(Keys.Enter))
            {
                e.Handled = true;
                SendKeys.Send("{TAB}");
            }
        }

        private void txtProveedor_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(Keys.Enter))
            {
                Provee provee = new Provee();
                string codprovee = txtProveedor.Text.Trim();
                string nombre=provee.ui_getDatos(codprovee, "NOMBRE");
                if (nombre == string.Empty)
                {
                    txtProveedor.Text = "";
                    lblProveedor.Text = "";
                    lblRuc.Text = "";
                    e.Handled = true;
                    txtProveedor.Focus();
                }
                else
                {
                    txtProveedor.Text = provee.ui_getDatos(codprovee, "CODIGO");
                    lblProveedor.Text = provee.ui_getDatos(codprovee, "NOMBRE");
                    lblRuc.Text = provee.ui_getDatos(codprovee, "RUC");
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
                Clie clie = new Clie();
                string codclie = txtCliente.Text.Trim();
                string nombre = clie.ui_getDatos(codclie, "NOMBRE");
                if (nombre == string.Empty)
                {
                    txtCliente.Text = "";
                    lblCliente.Text = "";
                    e.Handled = true;
                    txtCliente.Focus();
                }
                else
                {
                    lblCliente.Text = nombre;
                    e.Handled = true;
                    SendKeys.Send("{TAB}");
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
                SendKeys.Send("{TAB}");
            }
        }

        private void cmbMov_SelectedIndexChanged(object sender, EventArgs e)
        {
            Funciones funciones = new Funciones();
            string tipomov = funciones.getValorComboBox(cmbTipo, 2);
            string codmov = funciones.getValorComboBox(cmbMov, 2);
            string query = "SELECT codmov as clave,desmov as descripcion FROM tipomov ";
            query = query + "WHERE tipomov='" + @tipomov + "' and codmov='" + @codmov + "';";
            funciones.validarCombobox(query, cmbMov);
            limpiarMov();
            habilitaOpciones(tipomov, codmov);

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
                ui_viewprovee._clasePadre = "ui_updcabparte";
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
                this._TextBoxActivo = txtCliente;
                ui_viewclientes ui_viewclientes = new ui_viewclientes();
                ui_viewclientes._FormPadre = this;
                ui_viewclientes._clasePadre = "ui_updcabparte";
                ui_viewclientes._condicionAdicional = string.Empty;
                ui_viewclientes.BringToFront();
                ui_viewclientes.ShowDialog();
                ui_viewclientes.Dispose();
            }
        }

        private void tabPageEX2_Click(object sender, EventArgs e)
        {

        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            Funciones funciones=new Funciones();
            Int32 selectedCellCount = dgvData.GetCellCount(DataGridViewElementStates.Selected);
            if (selectedCellCount > 0)
            {
                string codcia = this._codcia;
                string alma = funciones.getValorComboBox(cmbAlmacen, 2);
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
            
                Funciones funciones = new Funciones();
                string codcia = this._codcia;
                string alma = funciones.getValorComboBox(cmbAlmacen, 2);
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

        private void txtNumDoc_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(Keys.Enter))
            {

                Funciones funciones = new Funciones();
                string codcia = this._codcia;
                string alma = funciones.getValorComboBox(cmbAlmacen, 2);
                string td = funciones.getValorComboBox(cmbTipo, 2);
                string numdoc = txtNumDoc.Text.Trim();
                limpiarGen();
                editar(codcia, alma, td, numdoc);
                e.Handled = true;
                cmbMov.Focus();

            }
        }

       
    }
}
