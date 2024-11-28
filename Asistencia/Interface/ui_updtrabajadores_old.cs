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
using System.Globalization;

namespace CaniaBrava
{
    public partial class ui_updtrabajadores_old : Form
    {
        //Oliver Cruz Tuanama
        string bd_prov = ConfigurationManager.AppSettings.Get("BD_PROV");
        string bd = "A";
        Funciones funciones = new Funciones();
        GlobalVariables globalvariables = new GlobalVariables();
        SqlConnection conexion = new SqlConnection();

        string _gencodigo;
        string idcia = "";

        private Form FormPadre;

        public Form _FormPadre
        {
            get { return FormPadre; }
            set { FormPadre = value; }
        }

        private TextBox TextBoxUbigeo;
        private TextBox TextBoxDscUbigeo;
        private TextBox TextBoxActivo;

        public TextBox _TextBoxActivo
        {
            get { return TextBoxActivo; }
            set { TextBoxActivo = value; }
        }

        public TextBox _TextBoxUbigeo
        {
            get { return TextBoxUbigeo; }
            set { TextBoxUbigeo = value; }
        }

        public TextBox _TextBoxDscUbigeo
        {
            get { return TextBoxDscUbigeo; }
            set { TextBoxDscUbigeo = value; }
        }

        public ui_updtrabajadores_old()
        {
            InitializeComponent();
            idcia = globalvariables.getValorCia();
        }

        public void newPerPlan()
        {
            txtOperacion.Text = "AGREGAR";
            txtValid.Text = "Q";
            pictureValidOk.Visible = false;
            pictureValidBad.Visible = false;
            pictureValidAsk.Visible = true;
            txtCodigoInterno.Clear();
            lblNombre.Text = "";
            txtApPat.Clear();
            txtApMat.Clear();
            txtNombres.Clear();
            cmbCategoriaOcupacional.Text = "";
            cmbSeccion.Text = "";
            cmbTipoDocumento.Text = "";
            txtNroDoc.Clear();
            txtTelFijo.Clear();
            txtCelular.Clear();
            cmbEstadoCivil.Text = "";
            txtEmail.Clear();

            MaesGen maesgen = new MaesGen();
            string stipo = string.Empty;
            string squery = "SELECT clavemaesgen,desmaesgen,parm1maesgen,parm2maesgen,parm3maesgen," +
                     "statemaesgen,idmaesgen FROM maesgen WHERE statemaesgen='V' " +
                     "and idmaesgen='029' and parm1maesgen='" + @stipo + "';";


            DialogResult opcion = MessageBox.Show("¿Desea que se genere el Código Interno en forma automática?", "Aviso Importante", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (opcion == DialogResult.Yes)
            {
                this._gencodigo = "A";
                txtCodigoInterno.Enabled = false;
                txtApPat.Focus();
            }
            else
            {
                this._gencodigo = "M";
                txtCodigoInterno.Enabled = true;
                txtCodigoInterno.Focus();
            }
        }

        public void ui_loadPerPlan(string idcia, string idperplan)
        {
            idcia = globalvariables.getValorCia();

            MaesGen maesgen = new MaesGen();
            MaesPdt maespdt = new MaesPdt();
            txtOperacion.Text = "EDITAR";
            txtValid.Text = "Q";
            pictureValidOk.Visible = false;
            pictureValidBad.Visible = false;
            pictureValidAsk.Visible = false;

            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();
            string query = "SELECT a.*, ISNULL(b.idasighorper, 0) AS idasighorper FROM perplan a ";
            query += "LEFT JOIN asig_hor_per b ON b.idperplan=a.idperplan AND b.idcia=a.idcia ";
            query += "WHERE a.idperplan='" + @idperplan + "' and a.idcia='" + @idcia + "';";

            try
            {
                SqlCommand myCommand = new SqlCommand(query, conexion);
                SqlDataReader myReader = myCommand.ExecuteReader();
                 
                if (myReader.Read())
                {
                    // DATOS PERSONALES
                    txtCodigoInterno.Enabled = false;
                    txtCodigoInterno.Text = myReader["idperplan"].ToString().Trim();
                    txtAuxiliar.Text = myReader["codaux"].ToString().Trim();
                    txtApPat.Text = myReader["apepat"].ToString().Trim();
                    txtApMat.Text = myReader["apemat"].ToString().Trim();
                    txtNombres.Text = myReader["nombres"].ToString().Trim();

                    txtTelFijo.Text = myReader["telfijo"].ToString().Trim();
                    txtCelular.Text = myReader["celular"].ToString().Trim();
                    var fecnac_ = myReader["fecnac"].ToString().Split(' ');
                    if (fecnac_.Length > 1)
                    {
                        string fec_ = "0" + fecnac_[0];
                        dtFechaNac.Value = DateTime.ParseExact(fec_.Substring(fec_.Length - 10, 10), "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    }
                    txtEmail.Text = myReader["email"].ToString().Trim();
                    txtNroDoc.Text = myReader["nrodoc"].ToString().Trim();

                    maesgen.consultaDetMaesGen("002", myReader["tipdoc"].ToString(), cmbTipoDocumento);

                    maesgen.consultaDetMaesGen("001", myReader["estcivil"].ToString(), cmbEstadoCivil);
                    maesgen.consultaDetMaesGen("019", myReader["sexo"].ToString(), cmbSexo);

                    query = " SELECT a.idplantiphorario as clave, a.descripcion FROM plantiphorario a ";
                    query += "INNER JOIN asig_hor_per b ON b.idplantiphorario=a.idplantiphorario and b.idasighorper=" + int.Parse(myReader["idasighorper"].ToString()) + " ";
                    query += "WHERE a.estado = 0;";
                    funciones.consultaComboBox(query, cmbTipHorario);

                    query = "Select idtipoper as clave,destipoper as descripcion from tipoper where idtipoper='" + myReader["idtipoper"] + "';";
                    funciones.consultaComboBox(query, cmbCategoriaOcupacional);
                    query = "Select idlabper as clave,deslabper as descripcion from labper where idtipoper='" + myReader["idtipoper"] + "' and idcia='" + myReader["idcia"] + "' and idlabper='" + myReader["idlabper"] + "';";
                    funciones.consultaComboBox(query, cmbOcupacion);
                    maesgen.consultaDetMaesGen("008", myReader["seccion"].ToString(), cmbSeccion);

                    PerPlan perplan = new PerPlan();
                    perplan.consultaHorario(myReader["idasighorper"].ToString(), cmbTipHorario);
                    txtIdAsigHorPer.Text = myReader["idasighorper"].ToString();

                    Remu remu = new Remu();
                    idcia = globalvariables.getValorCia();
                }

                myReader.Close();
                myCommand.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }

            conexion.Close();
        }

        public void ui_loadDerhab(string idcia, string idperplan, string idderhab)
        {
            string squery;
            MaesGen maesgen = new MaesGen();

            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();
            squery = "SELECT * FROM derhab where (idperplan='" + @idperplan + "' and idcia='" + @idcia + "' and idderhab='" + @idderhab + "');";

            try
            {
                SqlCommand myCommand = new SqlCommand(squery, conexion);
                SqlDataReader myReader = myCommand.ExecuteReader();
                if (myReader.Read())
                {

                }
                myReader.Close();
                myCommand.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }

            conexion.Close();
        }

        private void ui_updpersonal_Load(object sender, EventArgs e)
        {
            tabControlPer.SelectTab(0);
        }

        public void ui_ActualizaComboBox()
        {
            string query;
            MaesGen maesgen = new MaesGen();
            MaesPdt maespdt = new MaesPdt();
            idcia = globalvariables.getValorCia();
            
            string condicionSector = string.Empty;

            CiaFile ciafile = new CiaFile();
            string sector = ciafile.ui_getDatosCiaFile(idcia, "SECTOR");
            if (sector.Equals("1"))
            {
                condicionSector = " and parm1maesgen='A' ";
            }
            else
            {
                if (sector.Equals("2"))
                {
                    condicionSector = " and parm2maesgen='A' ";
                }
                else
                {
                    condicionSector = " and parm3maesgen='A' ";
                }
            }

            maesgen.listaDetMaesGen("002", cmbTipoDocumento, "B");

            maesgen.listaDetMaesGen("001", cmbEstadoCivil, "B");
            
            maesgen.listaDetMaesGen("019", cmbSexo, "B");

            string ruccia = globalvariables.getValorRucCia();

            query = "SELECT idtipoper as clave,destipoper as descripcion FROM tipoper order by 1 asc";
            funciones.listaComboBox(query, cmbCategoriaOcupacional, "B");

            maesgen.listaDetMaesGen("008", cmbSeccion, "B");

            query = "SELECT idplantiphorario as clave, descripcion FROM plantiphorario WHERE idcia='" + @idcia + "' and estado = 0 ;";
            funciones.listaComboBox(query, cmbTipHorario, "B");
        }

        private void cmbCategoriaOcupacional_SelectedValueChanged(object sender, EventArgs e)
        {
            string clave = funciones.getValorComboBox(cmbCategoriaOcupacional, 1);
            string squery;

            squery = "SELECT idlabper as clave,deslabper as descripcion FROM labper WHERE idcia='" + @idcia + "' and idtipoper='" + @clave + "';";
            funciones.listaComboBox(squery, cmbOcupacion, "B");

            cmbSeccion.Focus();
        }

        private void ui_validarDatos(string ope)
        {
            MaesGen maesgen = new MaesGen();

            string valorValida = "G";

            if (txtApPat.Text == string.Empty && valorValida == "G")
            {
                valorValida = "B";
                MessageBox.Show("No ha ingresado Apellido Paterno", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //tabControlPer.SelectTab(0);
                txtApPat.Focus();
            }

            if (txtApMat.Text == string.Empty && valorValida == "G")
            {
                valorValida = "B";
                MessageBox.Show("No ha ingresado Apellido Materno", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //tabControlPer.SelectTab(0);
                txtApMat.Focus();
            }

            if (txtNombres.Text == string.Empty && valorValida == "G")
            {
                valorValida = "B";
                MessageBox.Show("No ha ingresado Nombres", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //tabControlPer.SelectTab(0);
                txtNombres.Focus();
            }

            if (cmbTipoDocumento.Text == string.Empty && valorValida == "G")
            {
                valorValida = "B";
                MessageBox.Show("No ha seleccionado Tipo de Documento", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //tabControlPer.SelectTab(0);
                cmbTipoDocumento.Focus();
            }

            if (txtNroDoc.Text == string.Empty && valorValida == "G")
            {
                valorValida = "B";
                MessageBox.Show("No ha ingresado Nro.Documento", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //tabControlPer.SelectTab(0);
                txtNroDoc.Focus();
            }

            if (txtNroDoc.Text != string.Empty && valorValida == "G")
            {
                if (cmbTipoDocumento.Text.Substring(0, 4).Trim() == "01")
                {
                    if (txtNroDoc.Text.Trim().Length != 8)
                    {
                        valorValida = "B";
                        MessageBox.Show("Nro.Documento incorrecto", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        //tabControlPer.SelectTab(0);
                        txtNroDoc.Focus();
                    }
                }
            }

            if (UtileriasFechas.IsDate(dtFechaNac.Text) == false && valorValida == "G")
            {
                valorValida = "B";
                MessageBox.Show("Fecha de Nacimiento no válida", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //tabControlPer.SelectTab(0);
                dtFechaNac.Focus();
            }

            if (cmbCategoriaOcupacional.Text == string.Empty && valorValida == "G")
            {
                valorValida = "B";
                MessageBox.Show("No ha seleccionado Categoría Ocupacional", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //tabControlPer.SelectTab(2);
                cmbCategoriaOcupacional.Focus();
            }

            if (cmbCategoriaOcupacional.Text != string.Empty && valorValida == "G")
            {
                string idtipoper = funciones.getValorComboBox(cmbCategoriaOcupacional, 1);
                string query = "SELECT idtipoper as clave,destipoper as descripcion FROM tipoper where idtipoper='" + @idtipoper + "' order by 1 asc";
                string resultado = funciones.verificaItemComboBox(query, cmbCategoriaOcupacional);

                if (resultado.Trim() == string.Empty)
                {
                    valorValida = "B";
                    MessageBox.Show("Dato incorrecto en Categoría Ocupacional", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    //tabControlPer.SelectTab(2);
                    cmbCategoriaOcupacional.Focus();
                }
            }

            if (cmbOcupacion.Text == string.Empty && valorValida == "G")
            {
                valorValida = "B";
                MessageBox.Show("No ha seleccionado Ocupación", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //tabControlPer.SelectTab(2);
                cmbOcupacion.Focus();
            }

            if (cmbOcupacion.Text != string.Empty && valorValida == "G")
            {
                string idtipoper = funciones.getValorComboBox(cmbCategoriaOcupacional, 1);
                string idlabper = funciones.getValorComboBox(cmbOcupacion, 2);
                string query = "SELECT idlabper as clave,deslabper as descripcion FROM labper WHERE idcia='" + globalvariables.getValorCia() + "' and idtipoper='" + @idtipoper + "' and idlabper='" + @idlabper + "';";
                string resultado = funciones.verificaItemComboBox(query, cmbOcupacion);

                if (resultado.Trim() == string.Empty)
                {
                    valorValida = "B";
                    MessageBox.Show("Dato incorrecto en Ocupación", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    //tabControlPer.SelectTab(2);
                    cmbOcupacion.Focus();
                }
            }

            if (cmbSeccion.Text == string.Empty && valorValida == "G")
            {
                valorValida = "B";
                MessageBox.Show("No ha seleccionado Sección", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //tabControlPer.SelectTab(2);
                cmbSeccion.Focus();
            }

            if (cmbSeccion.Text != string.Empty && valorValida == "G")
            {
                string resultado = maesgen.verificaComboBoxMaesGen("008", cmbSeccion.Text.Trim());
                if (resultado.Trim() == string.Empty)
                {
                    valorValida = "B";
                    MessageBox.Show("Dato incorrecto en Sección", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    //tabControlPer.SelectTab(2);
                    cmbSeccion.Focus();
                }
            }

            if (cmbSexo.Text == string.Empty && valorValida == "G")
            {
                valorValida = "B";
                MessageBox.Show("No ha seleccionado Sexo", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //tabControlPer.SelectTab(0);
                cmbSexo.Focus();
            }

            if (cmbSexo.Text != string.Empty && valorValida == "G")
            {
                string resultado = maesgen.verificaComboBoxMaesGen("019", cmbSexo.Text.Trim());
                if (resultado.Trim() == string.Empty)
                {
                    valorValida = "B";
                    MessageBox.Show("Dato incorrecto en Sexo", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    //tabControlPer.SelectTab(0);
                    cmbSexo.Focus();
                }
            }

            if (cmbEstadoCivil.Text == string.Empty && valorValida == "G")
            {
                valorValida = "B";
                MessageBox.Show("No ha seleccionado Estado Civil", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //tabControlPer.SelectTab(0);
                cmbEstadoCivil.Focus();
            }

            if (cmbEstadoCivil.Text != string.Empty && valorValida == "G")
            {
                string resultado = maesgen.verificaComboBoxMaesGen("001", cmbEstadoCivil.Text.Trim());
                if (resultado.Trim() == string.Empty)
                {
                    valorValida = "B";
                    MessageBox.Show("Dato incorrecto en Estado Civil", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    //tabControlPer.SelectTab(0);
                    cmbEstadoCivil.Focus();
                }
            }
            

            if (txtEmail.Text.Trim() != string.Empty && valorValida == "G")
            {
                if (funciones.email_bien_escrito(txtEmail.Text.Trim()) == false)
                {
                    valorValida = "B";
                    MessageBox.Show("Correo Electrónico Inválido", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtEmail.Focus();
                }
            }
            

            if (valorValida.Equals("G")) //GOOD 
            {
                txtValid.Text = "G";
                pictureValidAsk.Visible = false;
                pictureValidOk.Visible = true;
                pictureValidBad.Visible = false;
                MessageBox.Show("Registro validado exitosamente", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                if (valorValida.Equals("B")) //BAD
                {
                    txtValid.Text = "B";
                    pictureValidAsk.Visible = false;
                    pictureValidOk.Visible = false;
                    pictureValidBad.Visible = true;
                }
                else
                {
                    txtValid.Text = "Q"; //QUESTION
                    pictureValidAsk.Visible = true;
                    pictureValidOk.Visible = false;
                    pictureValidBad.Visible = false;
                }
            }
        }

        #region KeyPress
        private void cmbTipoDocumento_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                if (cmbTipoDocumento.Text != String.Empty)
                {
                    txtNroDoc.Focus();
                }
            }
        }

        private void cmbOcupacion_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                if (cmbCategoriaOcupacional.Text != String.Empty)
                {
                    string claveCategoriaOcupacional = funciones.getValorComboBox(cmbCategoriaOcupacional, 1);
                    string claveLabPer = funciones.getValorComboBox(cmbOcupacion, 2);

                    string squery = "SELECT idlabper as clave,deslabper as descripcion FROM labper WHERE idcia='" + @idcia + "' and idtipoper='" + @claveCategoriaOcupacional + "' and idlabper='" + @claveLabPer + "';";
                    funciones.validarCombobox(squery, cmbOcupacion);
                }

                e.Handled = true;
                cmbEstadoCivil.Focus();
            }
        }

        private void cmbSeccion_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                if (cmbSeccion.Text != String.Empty)
                {
                    MaesGen maesgen = new MaesGen();
                    maesgen.validarDetMaesGen("008", cmbSeccion, cmbSeccion.Text);
                }
                e.Handled = true;
                cmbOcupacion.Focus();
            }
        } 

        private void cmbSexo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                if (cmbSexo.Text != String.Empty)
                {
                    MaesGen maesgen = new MaesGen();
                    maesgen.validarDetMaesGen("019", cmbSexo, cmbSexo.Text);
                }
                e.Handled = true;
                cmbEstadoCivil.Focus();

            }
        }

        private void txtCodigoInterno_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                if (txtCodigoInterno.Text.Trim() != string.Empty)
                {
                    if (txtCodigoInterno.Text.Length == 5)
                    {
                        e.Handled = true;
                        txtApPat.Focus();
                    }
                    else
                    {
                        txtCodigoInterno.Text = funciones.replicateCadena("0", 5 - txtCodigoInterno.Text.Trim().Length) + txtCodigoInterno.Text.Trim();
                        e.Handled = true;
                        txtApPat.Focus();
                    }
                }
                else
                {
                    MessageBox.Show("Deberá asignar un Código Único al Trabajador", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    e.Handled = true;
                    txtCodigoInterno.Focus();
                }
            }
        }

        private void txtCelular_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                e.Handled = true;
            }
        }

        private void txtEmail_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                e.Handled = true;
                dtFechaNac.Focus();
            }
        }

        private void cmbEstadoCivil_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                if (cmbEstadoCivil.Text != String.Empty)
                {
                    MaesGen maesgen = new MaesGen();
                    maesgen.validarDetMaesGen("001", cmbEstadoCivil, cmbEstadoCivil.Text);
                }
                e.Handled = true;

            }
        }

        private void cmbCategoriaOcupacional_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                string clave = funciones.getValorComboBox(cmbCategoriaOcupacional, 1);
                string squery;
                if (cmbCategoriaOcupacional.Text != String.Empty)
                {
                    squery = "SELECT idtipoper as clave,destipoper as descripcion FROM tipoper WHERE idtipoper='" + @clave + "';";
                    funciones.validarCombobox(squery, cmbCategoriaOcupacional);
                }

                squery = "SELECT idlabper as clave,deslabper as descripcion FROM labper WHERE idcia='" + @idcia + "' and idtipoper='" + @clave + "';";
                funciones.listaComboBox(squery, cmbOcupacion, "B");

                e.Handled = true;
                cmbSeccion.Focus();
            }
        }

        private void txtApPat_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                e.Handled = true;
                txtApMat.Focus();
            }
        }

        private void txtApMat_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                e.Handled = true;
                txtNombres.Focus();
            }
        }

        private void txtNombres_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                e.Handled = true;
                cmbTipoDocumento.Focus();
            }
        }

        private void txtNroDoc_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                e.Handled = true;
            }
        }

        private void txtTelFijo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                e.Handled = true;
                txtCelular.Focus();
            }
        }
        #endregion

        #region TextChanged
        private void txtApPat_TextChanged(object sender, EventArgs e)
        {
            string nombre;
            nombre = txtApPat.Text.Trim() + " " + txtApMat.Text.Trim() + " , " + txtNombres.Text.Trim();
            lblNombre.Text = nombre;
        }

        private void txtApMat_TextChanged(object sender, EventArgs e)
        {
            string nombre;
            nombre = txtApPat.Text.Trim() + " " + txtApMat.Text.Trim() + " , " + txtNombres.Text.Trim();
            lblNombre.Text = nombre;
        }

        private void txtNombres_TextChanged(object sender, EventArgs e)
        {
            string nombre;
            nombre = txtApPat.Text.Trim() + " " + txtApMat.Text.Trim() + " , " + txtNombres.Text.Trim();
            lblNombre.Text = nombre;
        } 
        #endregion

        #region Click
        private void btnAceptar_Click(object sender, EventArgs e)
        {
            string operacion = txtOperacion.Text.Trim();
            ui_validarDatos(operacion);

            if (txtValid.Text.Equals("G"))
            {
                string idperplan;

                if (this._gencodigo == "A" && operacion == "AGREGAR")
                {
                    PerPlan perplan = new PerPlan();
                    idperplan = perplan.generaCodigoInterno(idcia);
                    txtCodigoInterno.Text = idperplan;
                }
                else
                {
                    idperplan = txtCodigoInterno.Text.Trim();
                }
                string codaux = txtAuxiliar.Text.Trim();
                string apepat = txtApPat.Text.Trim();
                string apemat = txtApMat.Text.Trim();
                string nombres = txtNombres.Text.Trim();
                string fecnac = dtFechaNac.Value.ToString("yyyy-MM-dd");
                string tipdoc = funciones.getValorComboBox(cmbTipoDocumento, 4);
                string nrodoc = txtNroDoc.Text.Trim();
                string telfijo = txtTelFijo.Text.Trim();
                string celular = txtCelular.Text.Trim();
                string sexo = funciones.getValorComboBox(cmbSexo, 4);
                string estcivil = funciones.getValorComboBox(cmbEstadoCivil, 4);
                string email = txtEmail.Text.Trim();
                string idtipoper = funciones.getValorComboBox(cmbCategoriaOcupacional, 1);
                string idlabper = funciones.getValorComboBox(cmbOcupacion, 2);
                string seccion = funciones.getValorComboBox(cmbSeccion, 4);

                string ocurpts = string.Empty;

                string idTipHorario = funciones.getValorComboBox(cmbTipHorario, 4);
                int idAsigTipHorPer = int.Parse(txtIdAsigHorPer.Text);
                
                try
                {
                    PerPlan perplan = new PerPlan();
                    string validaExistenciaDoc = "0";

                    if (operacion == "AGREGAR")
                    {
                        validaExistenciaDoc = perplan.verificaPerPlanxDoc(idcia, tipdoc, nrodoc);
                    }

                    if (validaExistenciaDoc == "0")
                    {
                        perplan.actualizaTrabajador(operacion, idperplan, idcia, apepat, apemat, nombres, fecnac, tipdoc, nrodoc,
                        telfijo, celular, estcivil, email, codaux, sexo, idtipoper, idlabper, seccion);

                        Remu remu = new Remu();

                        //Oliver Cruz Tuanama
                        perplan.asignaHorario(idcia, bd, idperplan, idTipHorario, idAsigTipHorPer);

                        txtOperacion.Text = "EDITAR";
                    }
                    else
                    {
                        MessageBox.Show("El Documento de Identidad ya ha sido registrado, por favor verificar", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        tabControlPer.SelectTab(0);
                        txtNroDoc.Focus();
                    }
                }
                catch (FormatException ex)
                {
                    MessageBox.Show(ex.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void pictureValidOk_Click(object sender, EventArgs e)
        {

        }

        private void tabPageEX5_Click(object sender, EventArgs e)
        {

        } 

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            ((ui_trabajadores)FormPadre).btnActualizar.PerformClick();
            this.Close();
        }
        #endregion

        #region SelectedIndexChanged
        private void cmbTipoDocumento_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbTipoDocumento.Text != String.Empty)
            {
                txtNroDoc.Focus();
            }
        }

        private void cmbOcupacion_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbCategoriaOcupacional.Text != String.Empty)
            {
                string claveCategoriaOcupacional = funciones.getValorComboBox(cmbCategoriaOcupacional, 1);
                string claveLabPer = funciones.getValorComboBox(cmbOcupacion, 2);

                if (!string.IsNullOrEmpty((claveLabPer)))
                {
                    string squery = "SELECT idlabper as clave,deslabper as descripcion FROM labper WHERE idcia='" + @idcia + "' and idtipoper='" + @claveCategoriaOcupacional + "' and idlabper='" + @claveLabPer + "';";
                    funciones.validarCombobox(squery, cmbOcupacion);
                }
            }

            cmbEstadoCivil.Focus();
        }

        private void cmbCategoriaOcupacional_SelectedIndexChanged(object sender, EventArgs e)
        {
            string clave = funciones.getValorComboBox(cmbCategoriaOcupacional, 1);
            if (!string.IsNullOrEmpty(clave))
            {
                string squery = "SELECT idlabper as clave,deslabper as descripcion FROM labper WHERE idcia='" + @idcia + "' and idtipoper='" + @clave + "';";
                funciones.listaComboBox(squery, cmbOcupacion, "B");

                cmbSeccion.Focus();
            }
        }

        private void cmbSeccion_SelectedIndexChanged(object sender, EventArgs e)
        {
            string clave = funciones.getValorComboBox(cmbSeccion, 2);
            if (!string.IsNullOrEmpty(clave))
            {
                cmbOcupacion.Focus();
            }
        }
        #endregion
    }
}