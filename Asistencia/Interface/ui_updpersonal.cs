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
    public partial class ui_updpersonal : Form
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

        public ui_updpersonal()
        {
            InitializeComponent();
            idcia = globalvariables.getValorCia();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            ((ui_perplan)FormPadre).btnActualizar.PerformClick();
            this.Close();
        }

        private void tabPage2_Click(object sender, EventArgs e) { }

        private void tabPage1_Click(object sender, EventArgs e) { }

        private void label7_Click(object sender, EventArgs e) { }

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
            txtFechaNac.Clear();
            cmbTipoDocumento.Text = "";
            txtNroDoc.Clear();
            cmbPaisEmisor.Text = "";
            cmbPaisEmisor.Enabled = false;
            cmbCodigosNacionales.Text = "";
            txtTelFijo.Clear();
            txtCelular.Clear();
            txtRpm.Clear();
            cmbEstadoCivil.Text = "";
            cmbNacionalidad.Text = "";
            txtEmail.Clear();
            cmbCategoriaBrevete.Text = "";
            txtNroLicenciaConductor.Clear();
            cmbTipoTrabajador.Text = "";
            cmbCategoriaOcupacional.Text = "";
            cmbNivelEducativo.Text = "";
            cmbCentroCosto.Text = "";
            cmbOcupacion.Text = "";
            cmbSeccion.Text = "";
            cmbRegimen.Text = "";
            txtFecRegPen.Clear();
            txtCuspp.Clear();
            cmbTipoContratoTrabajo.Text = "";
            cmbTipoPago.Text = "";
            cmbPeriodicidadIngreso.Text = "";
            cmbSituacion.Text = "";
            cmbEntFinRem.Text = "";
            txtNumCtaRem.Clear();
            cmbMonRem.Text = "";
            cmbTipoCtaCts.Text = "";
            cmbEntFinCts.Text = "";
            txtNumCtaCts.Clear();
            cmbMonCts.Text = "";
            cmbTipoCtaCts.Text = "";
            cmbTipoVia.Text = "";
            txtNombreVia.Clear();
            txtNumeroVia.Clear();
            txtDeparVia.Clear();
            txtInteriorVia.Clear();
            txtManzanaVia.Clear();
            txtLoteVia.Clear();
            txtKmVia.Clear();
            txtBlockVia.Clear();
            txtEtapaVia.Clear();
            cmbTipoZona.Text = "";
            txtNombreZona.Clear();
            txtReferenciasZona.Clear();
            txtCodigoUbigeo.Clear();
            txtDscUbigeo.Clear();
            txtOcupacion.Clear();
            txtIniRemu.Clear();
            txtImporte.Clear();
            txtIniRemu.Enabled = true;
            txtImporte.Enabled = true;

            chkregalterna.Checked = false;
            chktrabmax.Checked = false;
            chktrabnoc.Checked = false;

            radioButtonSiQC.Checked = false;
            radioButtonNoQC.Checked = true;

            radioButtonSiRenExo.Checked = false;
            radioButtonNoRenExo.Checked = true;
            radioButtonSiRenQuiExo.Checked = false;
            radioButtonNoRenQuiExo.Checked = true;

            radioButtonSiDomicilia.Checked = true;
            radioButtonNoDomicilia.Checked = false;

            radioButtonSiEsVida.Checked = false;
            radioButtonNoEsVida.Checked = true;

            radioButtonSiDisca.Checked = false;
            radioButtonNoDisca.Checked = true;
            radioButtonSiSin.Checked = false;
            radioButtonNoSin.Checked = true;

            radioButtonSIEps.Checked = false;
            radioButtonNOEps.Checked = true;
            cmbEps.Text = "";
            cmbEps.Enabled = false;
            MaesGen maesgen = new MaesGen();
            string stipo = string.Empty;
            string squery = "SELECT clavemaesgen,desmaesgen,parm1maesgen,parm2maesgen,parm3maesgen," +
                     "statemaesgen,idmaesgen FROM maesgen WHERE statemaesgen='V' " +
                     "and idmaesgen='029' and parm1maesgen='" + @stipo + "';";

            maesgen.listaDetMaesGenQuery("029", squery, cmbSituaTrab, "");

            radioButtonSctrSaludNin.Checked = true;
            radioButtonSctrSaludEs.Checked = false;
            radioButtonSctrSaludEps.Checked = false;

            radioButtonSctrPensionNin.Checked = true;
            radioButtonSctrPensionOnp.Checked = false;
            radioButtonSctrPensionSeg.Checked = false;

            radioButtonAsePenSi.Checked = false;
            radioButtonAsePenNo.Checked = true;

            radioButtonSiEmp.Checked = false;
            radioButtonNoEmp.Checked = true;
            cmbEmpleador.Enabled = false;

            tabPageEX5.Enabled = false;
            tabPageEX6.Enabled = false;
            tabPageEX7.Enabled = false;
            tabPageEX12.Enabled = false;

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
            if (bd_prov.Equals("agromango")) bd = "M";
            if (bd_prov.Equals("planilla")) bd = "P";
            if (bd_prov.Equals("planlima")) bd = "L";
            if (bd_prov.Equals("planprueba")) bd = "D";
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
            query += "LEFT JOIN asig_hor_per b ON b.idperplan=a.idperplan AND b.idcia=a.idcia AND b.bd='" + @bd + "' ";
            query += "WHERE a.idperplan='" + @idperplan + "' and a.idcia='" + @idcia + "';";

            try
            {
                SqlCommand myCommand = new SqlCommand(query, conexion);
                SqlDataReader myReader = myCommand.ExecuteReader();

                if (myReader.Read())
                {
                    // DATOS PERSONALES
                    txtCodigoInterno.Enabled = false;
                    txtCodigoInterno.Text = myReader["idperplan"].ToString();
                    txtAuxiliar.Text = myReader["codaux"].ToString();
                    txtApPat.Text = myReader["apepat"].ToString();
                    txtApMat.Text = myReader["apemat"].ToString();
                    txtNombres.Text = myReader["nombres"].ToString();

                    maespdt.consultaDetMaesPdt("29", myReader["disnac"].ToString(), cmbCodigosNacionales);
                    txtTelFijo.Text = myReader["telfijo"].ToString();
                    txtCelular.Text = myReader["celular"].ToString();
                    txtRpm.Text = myReader["rpm"].ToString();
                    txtFechaNac.Text = myReader["fecnac"].ToString();
                    txtEmail.Text = myReader["email"].ToString();
                    txtNroDoc.Text = myReader["nrodoc"].ToString();
                    txtNroLicenciaConductor.Text = myReader["nrolic"].ToString();

                    maesgen.consultaDetMaesGen("002", myReader["tipdoc"].ToString(), cmbTipoDocumento);

                    if (myReader["tipdoc"].Equals("07"))
                    {
                        maespdt.consultaDetMaesPdt("26", myReader["paisemi"].ToString(), cmbPaisEmisor);
                        cmbPaisEmisor.Enabled = true;
                    }
                    else
                    {
                        cmbPaisEmisor.Text = "";
                        cmbPaisEmisor.Enabled = false;
                    }

                    maesgen.consultaDetMaesGen("004", myReader["catlic"].ToString(), cmbCategoriaBrevete);
                    maesgen.consultaDetMaesGen("003", myReader["nacion"].ToString(), cmbNacionalidad);
                    maesgen.consultaDetMaesGen("001", myReader["estcivil"].ToString(), cmbEstadoCivil);
                    maesgen.consultaDetMaesGen("019", myReader["sexo"].ToString(), cmbSexo);

                    //DIRECCION
                    maesgen.consultaDetMaesGen("017", myReader["tipvia"].ToString(), cmbTipoVia);
                    txtNombreVia.Text = myReader["nomvia"].ToString();
                    txtNumeroVia.Text = myReader["nrovia"].ToString();
                    txtDeparVia.Text = myReader["deparvia"].ToString();
                    txtInteriorVia.Text = myReader["intvia"].ToString();
                    txtManzanaVia.Text = myReader["manzavia"].ToString();
                    txtLoteVia.Text = myReader["lotevia"].ToString();
                    txtKmVia.Text = myReader["kmvia"].ToString();
                    txtBlockVia.Text = myReader["block"].ToString();
                    txtEtapaVia.Text = myReader["etapa"].ToString();

                    maesgen.consultaDetMaesGen("018", myReader["tipzona"].ToString(), cmbTipoZona);
                    txtNombreZona.Text = myReader["nomzona"].ToString();
                    txtReferenciasZona.Text = myReader["refzona"].ToString();
                    txtCodigoUbigeo.Text = myReader["ubigeo"].ToString();
                    txtDscUbigeo.Text = myReader["dscubigeo"].ToString();

                    //DATOS LABORALES
                    maesgen.consultaDetMaesGen("038", myReader["reglab"].ToString(), cmbRegimenLab);
                    maesgen.consultaDetMaesGen("005", myReader["tipotrab"].ToString(), cmbTipoTrabajador);
                    if (myReader["tipotrab"].Equals("67"))
                    {
                        txtRuc.Text = myReader["ruc"].ToString();
                        txtRuc.Enabled = true;
                    }
                    else
                    {
                        txtRuc.Enabled = false;
                        txtRuc.Clear();
                    }
                    
                    query = "Select idtipoper as clave,destipoper as descripcion from tipoper where idtipoper='" + myReader["idtipoper"] + "';";
                    funciones.consultaComboBox(query, cmbCategoriaOcupacional);
                    query = "Select idlabper as clave,deslabper as descripcion from labper where idtipoper='" + myReader["idtipoper"] + "' and idcia='" + myReader["idcia"] + "' and idlabper='" + myReader["idlabper"] + "';";
                    funciones.consultaComboBox(query, cmbOcupacion);
                    query = "SELECT idtipoplan as clave,destipoplan as descripcion FROM tipoplan WHERE idtipoplan='" + @myReader["idtipoplan"] + "';";
                    funciones.consultaComboBox(query, cmbTipoPlanilla);

                    maesgen.consultaDetMaesGen("008", myReader["seccion"].ToString(), cmbSeccion);
                    maesgen.consultaDetMaesGen("006", myReader["nivedu"].ToString(), cmbNivelEducativo);
                    maesgen.consultaDetMaesGen("009", myReader["regpen"].ToString(), cmbRegimen);
                    txtFecRegPen.Text = myReader["fecregpen"].ToString();
                    txtCuspp.Text = myReader["cuspp"].ToString();
                    maesgen.consultaDetMaesGen("010", myReader["contrab"].ToString(), cmbTipoContratoTrabajo);
                    maesgen.consultaDetMaesGen("011", myReader["pering"].ToString(), cmbPeriodicidadIngreso);
                    maesgen.consultaDetMaesGen("012", myReader["sitesp"].ToString(), cmbSituacion);
                    maesgen.consultaDetMaesGen("013", myReader["tippag"].ToString(), cmbTipoPago);

                    //ENTIDADES FINANCIERAS
                    maesgen.consultaDetMaesGen("007", myReader["entfinrem"].ToString(), cmbEntFinRem);
                    maesgen.consultaDetMaesGen("014", myReader["tipctarem"].ToString(), cmbTipoCtaRem);
                    txtNumCtaRem.Text = myReader["nroctarem"].ToString();
                    maesgen.consultaDetMaesGen("015", myReader["monrem"].ToString(), cmbMonRem);

                    maesgen.consultaDetMaesGen("007", myReader["entfincts"].ToString(), cmbEntFinCts);
                    maesgen.consultaDetMaesGen("014", myReader["tipctacts"].ToString(), cmbTipoCtaCts);
                    txtNumCtaCts.Text = myReader["nroctacts"].ToString();
                    maesgen.consultaDetMaesGen("015", myReader["moncts"].ToString(), cmbMonCts);

                    //INFORMACION ADICIONAL
                    funciones.consultaRpts("R4", myReader["ocurpts"].ToString(), txtOcupacion);
                    string stipo;
                    if (myReader["afiliaeps"].Equals("1"))
                    {
                        stipo = "EPS";
                        radioButtonSIEps.Checked = true;
                        radioButtonNOEps.Checked = false;
                        maesgen.consultaDetMaesGen("028", myReader["eps"].ToString(), cmbEps);
                        cmbEps.Enabled = true;
                    }
                    else
                    {
                        stipo = string.Empty;
                        radioButtonSIEps.Checked = false;
                        radioButtonNOEps.Checked = true;
                        cmbEps.Text = string.Empty;
                        cmbEps.Enabled = false;
                    }

                    maesgen.consultaDetMaesGen("029", myReader["situatrab"].ToString(), cmbSituaTrab);
                    maesgen.consultaDetMaesGen("036", myReader["apliconve"].ToString(), cmbConve);

                    if (myReader["regalterna"].Equals("1"))
                    {
                        chkregalterna.Checked = true;
                    }
                    else { chkregalterna.Checked = false; }

                    if (myReader["asepen"].Equals("0"))
                    {
                        radioButtonAsePenSi.Checked = false;
                        radioButtonAsePenNo.Checked = true;
                    }
                    else
                    {
                        radioButtonAsePenSi.Checked = true;
                        radioButtonAsePenNo.Checked = false;
                    }

                    if (myReader["trabmax"].Equals("1"))
                    {
                        chktrabmax.Checked = true;
                    }
                    else { chktrabmax.Checked = false; }


                    if (myReader["trabnoc"].Equals("1"))
                    {
                        chktrabnoc.Checked = true;
                    }
                    else
                    {
                        chktrabnoc.Checked = false;
                    }

                    if (myReader["quicat"].Equals("1"))
                    {
                        radioButtonSiQC.Checked = true;
                        radioButtonNoQC.Checked = false;
                    }
                    else
                    {
                        radioButtonSiQC.Checked = false;
                        radioButtonNoQC.Checked = true;
                    }

                    if (myReader["renexo"].Equals("1"))
                    {
                        radioButtonSiRenExo.Checked = true;
                        radioButtonNoRenExo.Checked = false;
                    }
                    else
                    {
                        radioButtonSiRenExo.Checked = false;
                        radioButtonNoRenExo.Checked = true;
                    }

                    if (myReader["exoquicat"].Equals("1"))
                    {
                        radioButtonSiRenQuiExo.Checked = true;
                        radioButtonNoRenQuiExo.Checked = false;
                    }
                    else
                    {
                        radioButtonSiRenQuiExo.Checked = false;
                        radioButtonNoRenQuiExo.Checked = true;
                    }

                    if (myReader["domicilia"].Equals("1"))
                    {
                        radioButtonSiDomicilia.Checked = true;
                        radioButtonNoDomicilia.Checked = false;
                    }
                    else
                    {
                        radioButtonSiDomicilia.Checked = false;
                        radioButtonNoDomicilia.Checked = true;
                    }

                    if (myReader["esvida"].Equals("1"))
                    {
                        radioButtonSiEsVida.Checked = true;
                        radioButtonNoEsVida.Checked = false;
                    }
                    else
                    {
                        radioButtonSiEsVida.Checked = false;
                        radioButtonNoEsVida.Checked = true;
                    }

                    if (myReader["discapa"].Equals("1"))
                    {
                        radioButtonSiDisca.Checked = true;
                        radioButtonNoDisca.Checked = false;
                    }
                    else
                    {
                        radioButtonSiDisca.Checked = false;
                        radioButtonNoDisca.Checked = true;
                    }

                    if (myReader["sindica"].Equals("1"))
                    {
                        radioButtonSiSin.Checked = true;
                        radioButtonNoSin.Checked = false;
                    }
                    else
                    {
                        radioButtonSiSin.Checked = false;
                        radioButtonNoSin.Checked = true;
                    }

                    if (myReader["sctrsanin"].Equals("1"))
                    {
                        radioButtonSctrSaludNin.Checked = true;
                    }
                    else
                    {
                        radioButtonSctrSaludNin.Checked = false;
                    }

                    if (myReader["sctrsaessa"].Equals("1"))
                    {
                        radioButtonSctrSaludEs.Checked = true;
                    }
                    else
                    {
                        radioButtonSctrSaludEs.Checked = false;
                    }

                    if (myReader["sctrsaeps"].Equals("1"))
                    {
                        radioButtonSctrSaludEps.Checked = true;
                    }
                    else
                    {
                        radioButtonSctrSaludEps.Checked = false;
                    }

                    if (myReader["sctrpennin"].Equals("1"))
                    {
                        radioButtonSctrPensionNin.Checked = true;
                    }
                    else
                    {
                        radioButtonSctrPensionNin.Checked = false;
                    }


                    if (myReader["sctrpenonp"].Equals("1"))
                    {
                        radioButtonSctrPensionOnp.Checked = true;
                    }
                    else
                    {
                        radioButtonSctrPensionOnp.Checked = false;
                    }

                    if (myReader["sctrpenseg"].Equals("1"))
                    {
                        radioButtonSctrPensionSeg.Checked = true;
                    }
                    else
                    {
                        radioButtonSctrPensionSeg.Checked = false;
                    }

                    chk_alta_tregistro.Checked = (myReader["alta_tregistro"].Equals("1")) ? true : false;
                    chk_baja_tregistro.Checked = (myReader["baja_tregistro"].Equals("1")) ? true : false;

                    if (myReader["asigemplea"].Equals("1"))
                    {
                        radioButtonSiEmp.Checked = true;
                        query = "Select rucemp as clave,razonemp as descripcion from emplea where idciafile='" + myReader["idcia"] + "' and rucemp='" + myReader["rucemp"] + "';";
                    }
                    else
                    {
                        radioButtonSiEmp.Checked = false;
                        query = "Select ruccia as clave,descia as descripcion from ciafile where idcia='" + myReader["idcia"] + "';";
                    }

                    funciones.consultaComboBox(query, cmbEmpleador);

                    query = "Select idestane as clave,desestane as descripcion from estane where idciafile='" + myReader["idcia"] + "' and codemp='" + myReader["rucemp"] + "' and idestane='" + myReader["estane"] + "';";
                    funciones.consultaComboBox(query, cmbEstablecimiento);

                    query = " SELECT CAST(a.idplantiphorario AS CHAR) as clave, a.descripcion FROM plantiphorario a ";
                    query += "INNER JOIN asig_hor_per b ON b.idplantiphorario=a.idplantiphorario and b.idasighorper=" + int.Parse(myReader["idasighorper"].ToString()) + " ";
                    query += "WHERE a.estado = 0;";
                    funciones.consultaComboBox(query, cmbTipHorario);

                    txtIdAsigHorPer.Text = myReader["idasighorper"].ToString();

                    Remu remu = new Remu();
                    idcia = globalvariables.getValorCia();
                    txtIniRemu.Text = remu.consultarRemu(idcia, idperplan, "FINI");
                    txtImporte.Text = remu.consultarRemu(idcia, idperplan, "IMPORTE");
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
            txtOpeDh.Text = "EDITAR";
            txtValidDh.Text = "Q";

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
                    // DATOS PERSONALES
                    txtCodigoInternoDh.Text = myReader["idderhab"].ToString();
                    txtApPatDh.Text = myReader["apepat"].ToString();
                    txtApMatDh.Text = myReader["apemat"].ToString();
                    txtNombresDh.Text = myReader["nombres"].ToString();
                    txtFechaNacDh.Text = myReader["fecnac"].ToString();
                    txtNroDocDh.Text = myReader["nrodoc"].ToString();

                    maesgen.consultaDetMaesGen("002", myReader["tipdoc"].ToString(), cmbTipoDocumentoDh);
                    maesgen.consultaDetMaesGen("019", myReader["sexo"].ToString(), cmbSexoDh);

                    //VINCULO FAMILIAR
                    maesgen.consultaDetMaesGen("020", myReader["vinfam"].ToString(), cmbVinculoDh);
                    maesgen.consultaDetMaesGen("022", myReader["sitdh"].ToString(), cmbSituacionDh);
                    txtAltaDh.Text = myReader["fecalta"].ToString();
                    maesgen.consultaDetMaesGen("021", myReader["docpat"].ToString(), cmbDocPatDh);
                    maesgen.consultaDetMaesGen("023", myReader["motbaja"].ToString(), cmbMotBajaDh);

                    if (myReader["motbaja"].ToString() != string.Empty)
                    {
                        txtBajaDh.Text = myReader["fecbaja"].ToString();
                        txtBajaDh.Enabled = true;
                        cmbMotBajaDh.Enabled = true;
                    }
                    else
                    {
                        txtBajaDh.Text = string.Empty;
                        txtBajaDh.Enabled = false;
                        cmbMotBajaDh.Enabled = false;
                    }


                    if (myReader["domtra"].ToString() == "1")
                    {
                        radioButtonDomTra.Checked = true;
                        tabPageEX10.Enabled = false;
                    }
                    else
                        radioButtonDomTra.Checked = false;

                    if (myReader["otrodom"].ToString() == "1")
                    {
                        radioButtonOtroDom.Checked = true;
                        tabPageEX10.Enabled = true;
                    }
                    else
                        radioButtonOtroDom.Checked = false;


                    if (myReader["discapa"].ToString() == "1")
                    {
                        radioButtonSiDiscaDh.Checked = true;
                        radioButtonNoDiscaDh.Checked = false;
                        txtResDirDh.Text = myReader["rddisca"].ToString();

                    }
                    else
                    {
                        radioButtonSiDiscaDh.Checked = false;
                        radioButtonNoDiscaDh.Checked = true;
                        txtResDirDh.Text = "";
                    }



                    //DIRECCION
                    maesgen.consultaDetMaesGen("017", myReader["tipvia"].ToString(), cmbTipoViaDh);
                    txtNombreViaDh.Text = myReader["nomvia"].ToString();
                    txtNumeroViaDh.Text = myReader["nrovia"].ToString();
                    txtInteriorViaDh.Text = myReader["intvia"].ToString();
                    maesgen.consultaDetMaesGen("018", myReader["tipvia"].ToString(), cmbTipoZonaDh);
                    txtNombreZonaDh.Text = myReader["nomzona"].ToString();
                    txtReferenciasZonaDh.Text = myReader["refzona"].ToString();
                    txtCodigoUbigeoDh.Text = myReader["ubigeo"].ToString();
                    txtDscUbigeoDh.Text = myReader["dscubigeo"].ToString();
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
            if (bd_prov.Equals("agromango")) bd = "M";
            if (bd_prov.Equals("planilla")) bd = "P";
            if (bd_prov.Equals("planlima")) bd = "L";
            if (bd_prov.Equals("planprueba")) bd = "D";
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
            maespdt.listaDetMaesPdt("26", cmbPaisEmisor, "B");
            maespdt.listaDetMaesPdt("29", cmbCodigosNacionales, "B");

            maesgen.listaDetMaesGen("002", cmbTipoDocumentoDh, "B");

            maesgen.listaDetMaesGen("001", cmbEstadoCivil, "B");
            maesgen.listaDetMaesGen("003", cmbNacionalidad, "B");
            maesgen.listaDetMaesGen("004", cmbCategoriaBrevete, "B");

            maesgen.listaDetMaesGen("017", cmbTipoVia, "B");
            maesgen.listaDetMaesGen("018", cmbTipoZona, "B");


            maesgen.listaDetMaesGen("017", cmbTipoViaDh, "B");
            maesgen.listaDetMaesGen("018", cmbTipoZonaDh, "B");

            query = "SELECT clavemaesgen,desmaesgen,parm1maesgen,parm2maesgen,parm3maesgen," +
                     "statemaesgen,idmaesgen FROM maesgen WHERE statemaesgen='V' " +
                     "and idmaesgen='038' " + condicionSector + " order by clavemaesgen asc;";

            maesgen.listaDetMaesGenQuery("038", query, cmbRegimenLab, "B");

            query = "SELECT clavemaesgen,desmaesgen,parm1maesgen,parm2maesgen,parm3maesgen," +
                     "statemaesgen,idmaesgen FROM maesgen WHERE statemaesgen='V' " +
                     "and idmaesgen='005' " + condicionSector + " order by clavemaesgen asc;";

            maesgen.listaDetMaesGenQuery("005", query, cmbTipoTrabajador, "B");

            maesgen.listaDetMaesGen("006", cmbNivelEducativo, "B");
            maesgen.listaDetMaesGen("008", cmbSeccion, "B");

            query = "SELECT clavemaesgen,desmaesgen,parm1maesgen,parm2maesgen,parm3maesgen," +
                     "statemaesgen,idmaesgen FROM maesgen WHERE statemaesgen='V' " +
                     "and idmaesgen='009' " + condicionSector + " order by clavemaesgen asc;";

            maesgen.listaDetMaesGenQuery("009", query, cmbRegimen, "B");

            maesgen.listaDetMaesGen("010", cmbTipoContratoTrabajo, "B");
            maesgen.listaDetMaesGen("011", cmbPeriodicidadIngreso, "B");
            maesgen.listaDetMaesGen("012", cmbSituacion, "B");
            maesgen.listaDetMaesGen("013", cmbTipoPago, "B");

            query = "SELECT idcencos as clave,descencos as descripcion FROM cencos WHERE idcia='" + @idcia + "' and statecencos='V';";
            funciones.listaComboBox(query, cmbCentroCosto, "B");

            query = "SELECT idtipoper as clave,destipoper as descripcion FROM tipoper order by 1 asc";
            funciones.listaComboBox(query, cmbCategoriaOcupacional, "B");

            query = "SELECT idtipoplan as clave,destipoplan as descripcion FROM tipoplan WHERE idtipoplan in (SELECT  idtipoplan FROM reglabcia WHERE idcia='" + @idcia + "');";
            funciones.listaComboBox(query, cmbTipoPlanilla, "B");

            maesgen.listaDetMaesGen("007", cmbEntFinRem, "B");
            maesgen.listaDetMaesGen("007", cmbEntFinCts, "B");
            maesgen.listaDetMaesGen("014", cmbTipoCtaRem, "B");
            maesgen.listaDetMaesGen("014", cmbTipoCtaCts, "B");
            maesgen.listaDetMaesGen("015", cmbMonRem, "B");
            maesgen.listaDetMaesGen("015", cmbMonCts, "B");

            maesgen.listaDetMaesGen("016", cmbMotivoFinPeriodo, "B");
            query = "SELECT idfonpen as clave,desfonpen as descripcion FROM fonpen order by 1 asc";
            funciones.listaComboBox(query, cmbFonPen, "B");
            maesgen.listaDetMaesGen("019", cmbSexo, "B");
            maesgen.listaDetMaesGen("019", cmbSexoDh, "B");
            maesgen.listaDetMaesGen("020", cmbVinculoDh, "B");
            maesgen.listaDetMaesGen("021", cmbDocPatDh, "B");
            maesgen.listaDetMaesGen("022", cmbSituacionDh, "B");
            maesgen.listaDetMaesGen("023", cmbMotBajaDh, "B");

            maesgen.listaDetMaesGen("028", cmbEps, "B");
            maesgen.listaDetMaesGen("029", cmbSituaTrab, "B");

            maesgen.listaDetMaesGen("036", cmbConve, "B");

            string ruccia = globalvariables.getValorRucCia();
            query = "SELECT ruccia as clave,descia as descripcion FROM ciafile WHERE idcia='" + @idcia + "';";
            funciones.listaComboBox(query, cmbEmpleador, "");

            string query_esta = "SELECT idestane as clave,desestane as descripcion FROM estane WHERE codemp='" + @ruccia + "' and idciafile='" + @idcia + "' order by idestane asc;";
            funciones.listaComboBox(query_esta, cmbEstablecimiento, "");

            query = "SELECT CAST(idplantiphorario AS CHAR) as clave, descripcion FROM plantiphorario WHERE idcia='" + @idcia + "' and estado = 0 AND bd = '" + bd + "';";
            funciones.listaComboBox(query, cmbTipHorario, "B");
        }

        private void cmbTipoDocumento_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                if (cmbTipoDocumento.Text != String.Empty)
                {
                    MaesGen maesgen = new MaesGen();
                    bool resultado = maesgen.validarDetMaesGen("002", cmbTipoDocumento, cmbTipoDocumento.Text);
                    if (resultado)
                    {
                        if (funciones.getValorComboBox(cmbTipoDocumento, 4).Trim().Equals("07"))
                        {
                            cmbPaisEmisor.Enabled = true;
                        }
                        else
                        {
                            cmbPaisEmisor.Enabled = false;
                            cmbPaisEmisor.Text = "";
                        }
                    }

                }
                e.Handled = true;
                txtNroDoc.Focus();
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
                cmbCodigosNacionales.Focus();
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

        private void txtCelular_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                e.Handled = true;
                txtRpm.Focus();
            }
        }

        private void txtRpm_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                e.Handled = true;
                cmbNacionalidad.Focus();
            }
        }

        private void cmbNacionalidad_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                if (cmbNacionalidad.Text != String.Empty)
                {
                    MaesGen maesgen = new MaesGen();
                    maesgen.validarDetMaesGen("003", cmbNacionalidad, cmbNacionalidad.Text);
                }
                e.Handled = true;
                txtEmail.Focus();
            }
        }

        private void txtEmail_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                e.Handled = true;
                txtFechaNac.Focus();
            }
        }

        private void txtFechaNac_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                if (UtileriasFechas.IsDate(txtFechaNac.Text))
                {
                    e.Handled = true;
                    cmbSexo.Focus();
                }
                else
                {
                    MessageBox.Show("Fecha de Nacimiento no válida", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    e.Handled = true;
                    txtFechaNac.Focus();
                }
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
                cmbCategoriaBrevete.Focus();

            }
        }

        private void cmbCategoriaBrevete_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                if (cmbCategoriaBrevete.Text != String.Empty)
                {
                    MaesGen maesgen = new MaesGen();
                    
                    maesgen.validarDetMaesGen("004", cmbCategoriaBrevete, cmbCategoriaBrevete.Text);
                    string clave = funciones.getValorComboBox(cmbCategoriaBrevete, 4);
                    if (clave.Equals("XX"))
                    {
                        txtNroLicenciaConductor.Text = "";
                        txtNroLicenciaConductor.Enabled = false;
                    }
                    else
                    {

                        txtNroLicenciaConductor.Enabled = true;
                        e.Handled = true;
                        txtNroLicenciaConductor.Focus();
                    }
                }


            }
        }

        private void cmbTipoTrabajador_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                if (cmbTipoTrabajador.Text != String.Empty)
                {
                    MaesGen maesgen = new MaesGen();
                    bool resultado = maesgen.validarDetMaesGen("005", cmbTipoTrabajador, cmbTipoTrabajador.Text);
                    if (resultado)
                    {
                        string tipotrab = funciones.getValorComboBox(cmbTipoTrabajador, 4);
                        e.Handled = true;
                        if (tipotrab.Equals("67"))
                        {
                            txtRuc.Enabled = true;
                            txtRuc.Focus();
                        }
                        else
                        {
                            txtRuc.Clear();
                            txtRuc.Enabled = false;
                            cmbNivelEducativo.Focus();
                        }
                    }
                    else
                    {
                        txtRuc.Clear();
                        txtRuc.Enabled = false;
                    }
                }
            }
        }

        private void cmbNivelEducativo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                if (cmbNivelEducativo.Text != String.Empty)
                {
                    MaesGen maesgen = new MaesGen();
                    maesgen.validarDetMaesGen("006", cmbNivelEducativo, cmbNivelEducativo.Text);
                }
                e.Handled = true;
                cmbCategoriaOcupacional.Focus();

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
                cmbRegimen.Focus();
            }
        }

        private void cmbCentroCosto_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                if (cmbCentroCosto.Text != String.Empty)
                {
                    idcia = globalvariables.getValorCia();
                    string clavetmp = cmbCentroCosto.Text + funciones.replicateCadena(" ", 2);
                    string clave = clavetmp.Substring(0, 2).Trim();

                    string squery = "SELECT idcencos as clave,descencos as descripcion FROM cencos WHERE idcia='" + @idcia + "' and idcencos='" + @clave + "';";
                    funciones.validarCombobox(squery, cmbCentroCosto);
                }

                e.Handled = true;
                txtPorcentaje.Focus();
            }
        }

        private void cmbCategoriaOcupacional_SelectedValueChanged(object sender, EventArgs e)
        {
            idcia = globalvariables.getValorCia();
            string clave = funciones.getValorComboBox(cmbCategoriaOcupacional, 1);
            string squery;

            squery = "SELECT idlabper as clave,deslabper as descripcion FROM labper WHERE idcia='" + @idcia + "' and idtipoper='" + @clave + "';";
            funciones.listaComboBox(squery, cmbOcupacion, "B");

            cmbCentroCosto.Focus();
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
                cmbSeccion.Focus();
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
                cmbRegimen.Focus();

            }
        }

        private void cmbRegimen_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                if (cmbRegimen.Text != String.Empty)
                {
                    MaesGen maesgen = new MaesGen();
                    maesgen.validarDetMaesGen("009", cmbRegimen, cmbRegimen.Text);
                }
                e.Handled = true;
                txtFecRegPen.Focus();


            }
        }

        private void txtCuspp_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                e.Handled = true;
                cmbTipoPlanilla.Focus();
            }
        }

        private void cmbTipoContratoTrabajo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                if (cmbTipoContratoTrabajo.Text != String.Empty)
                {
                    MaesGen maesgen = new MaesGen();
                    maesgen.validarDetMaesGen("010", cmbTipoContratoTrabajo, cmbTipoContratoTrabajo.Text);
                }
                e.Handled = true;
                cmbTipoPago.Focus();

            }
        }

        private void cmbTipoPago_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                if (cmbTipoPago.Text != String.Empty)
                {
                    MaesGen maesgen = new MaesGen();
                    maesgen.validarDetMaesGen("013", cmbTipoPago, cmbTipoPago.Text);
                }
                e.Handled = true;
                cmbSituacion.Focus();

            }
        }

        private void cmbPeriodicidadIngreso_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                if (cmbPeriodicidadIngreso.Text != String.Empty)
                {
                    MaesGen maesgen = new MaesGen();
                    maesgen.validarDetMaesGen("011", cmbPeriodicidadIngreso, cmbPeriodicidadIngreso.Text);
                }

            }
        }

        private void cmbSituacion_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                if (cmbSituacion.Text != String.Empty)
                {
                    MaesGen maesgen = new MaesGen();
                    maesgen.validarDetMaesGen("012", cmbSituacion, cmbSituacion.Text);
                }


            }
        }

        private void cmbEntFinRem_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                if (cmbEntFinRem.Text != String.Empty)
                {
                    MaesGen maesgen = new MaesGen();
                    maesgen.validarDetMaesGen("007", cmbEntFinRem, cmbEntFinRem.Text);
                }
                e.Handled = true;
                txtNumCtaRem.Focus();

            }
        }

        private void txtNumCtaRem_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                e.Handled = true;
                cmbMonRem.Focus();
            }
        }

        private void cmbMonRem_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                if (cmbMonRem.Text != String.Empty)
                {
                    MaesGen maesgen = new MaesGen();
                    maesgen.validarDetMaesGen("015", cmbMonRem, cmbMonRem.Text);
                }
                e.Handled = true;
                cmbTipoCtaRem.Focus();

            }
        }

        private void cmbTipoCtaRem_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                if (cmbTipoCtaRem.Text != String.Empty)
                {
                    MaesGen maesgen = new MaesGen();
                    maesgen.validarDetMaesGen("014", cmbTipoCtaRem, cmbTipoCtaRem.Text);
                }
                e.Handled = true;
                cmbEntFinCts.Focus();

            }
        }

        private void cmbEntFinCts_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                if (cmbEntFinCts.Text != String.Empty)
                {
                    MaesGen maesgen = new MaesGen();
                    maesgen.validarDetMaesGen("007", cmbEntFinCts, cmbEntFinCts.Text);
                }
                e.Handled = true;
                txtNumCtaCts.Focus();

            }
        }

        private void txtNumCtaCts_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                e.Handled = true;
                cmbMonCts.Focus();
            }
        }

        private void cmbMonCts_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                if (cmbMonCts.Text != String.Empty)
                {
                    MaesGen maesgen = new MaesGen();
                    maesgen.validarDetMaesGen("015", cmbMonCts, cmbMonCts.Text);
                }
                e.Handled = true;
                cmbTipoCtaCts.Focus();

            }
        }

        private void cmbTipoCtaCts_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                if (cmbTipoCtaCts.Text != String.Empty)
                {
                    MaesGen maesgen = new MaesGen();
                    maesgen.validarDetMaesGen("014", cmbTipoCtaCts, cmbTipoCtaCts.Text);
                }
                e.Handled = true;


            }
        }

        private void cmbMotivoFinPeriodo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                if (cmbMotivoFinPeriodo.Text != String.Empty)
                {
                    MaesGen maesgen = new MaesGen();
                    maesgen.validarDetMaesGen("016", cmbMotivoFinPeriodo, cmbMotivoFinPeriodo.Text);
                }


            }
        }

        private void txtInicioPeriodo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                if (UtileriasFechas.IsDate(txtInicioPeriodo.Text))
                {
                    e.Handled = true;
                    btnGrabar.Focus();
                }
                else
                {
                    MessageBox.Show("Fecha de Inicio de Periodo no válida", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    e.Handled = true;
                    txtInicioPeriodo.Focus();
                }
            }
        }

        private void txtFinPeriodo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                if (UtileriasFechas.IsDate(txtFinPeriodo.Text))
                {
                    e.Handled = true;
                    cmbMotivoFinPeriodo.Focus();
                }
                else
                {
                    MessageBox.Show("Fecha de Fin de Periodo no válida", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    e.Handled = true;
                    txtFinPeriodo.Focus();
                }
            }
        }

        private void txtIniFonPen_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                if (UtileriasFechas.IsDate(txtFechaNac.Text))
                {
                    e.Handled = true;
                    txtAVT.Focus();
                }
                else
                {
                    MessageBox.Show("Fecha de Inicio no válida", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    e.Handled = true;
                    txtIniFonPen.Focus();
                }
            }


        }

        private void txtFinFonPen_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                if (UtileriasFechas.IsDate(txtFechaNac.Text))
                {
                    e.Handled = true;
                    cmbFonPen.Focus();
                }
                else
                {
                    MessageBox.Show("Fecha Final no válida", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    e.Handled = true;
                    txtFinFonPen.Focus();
                }
            }


        }

        private void cmbFonPen_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                if (cmbFonPen.Text != String.Empty)
                {
                    string clave = funciones.getValorComboBox(cmbFonPen, 3);
                    string squery = "SELECT idfonpen as clave,desfonpen as descripcion FROM fonpen WHERE statefonpen='V' and idfonpen='" + @clave + "' order by 1 asc;";
                    funciones.validarCombobox(squery, cmbFonPen);
                }
            }
        }

        private void cmbTipoVia_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                if (cmbTipoVia.Text != String.Empty)
                {
                    MaesGen maesgen = new MaesGen();
                    maesgen.validarDetMaesGen("017", cmbTipoVia, cmbTipoVia.Text);
                }
                e.Handled = true;
                txtNombreVia.Focus();

            }
        }

        private void txtNombreVia_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                e.Handled = true;
                txtNumeroVia.Focus();


            }
        }

        private void TxtNumeroVia_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                e.Handled = true;
                txtDeparVia.Focus();


            }
        }

        private void txtInteriorVia_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                e.Handled = true;
                txtManzanaVia.Focus();

            }
        }

        private void cmbTipoZona_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                if (cmbTipoZona.Text != String.Empty)
                {
                    MaesGen maesgen = new MaesGen();
                    maesgen.validarDetMaesGen("018", cmbTipoZona, cmbTipoZona.Text);
                }
                e.Handled = true;
                txtNombreZona.Focus();

            }
        }

        private void txtNombreZona_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                e.Handled = true;
                txtReferenciasZona.Focus();
            }
        }

        private void txtReferenciasZona_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                e.Handled = true;
                btnUbigeo.Focus();

            }
        }

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
                string fecnac = txtFechaNac.Text.Trim();
                string tipdoc = funciones.getValorComboBox(cmbTipoDocumento, 4);
                string paisemi = funciones.getValorComboBox(cmbPaisEmisor, 4);
                string nrodoc = txtNroDoc.Text.Trim();
                string disnac = funciones.getValorComboBox(cmbCodigosNacionales, 4);
                string telfijo = txtTelFijo.Text.Trim();
                string celular = txtCelular.Text.Trim();
                string rpm = txtRpm.Text.Trim();
                string sexo = funciones.getValorComboBox(cmbSexo, 4);
                string estcivil = funciones.getValorComboBox(cmbEstadoCivil, 4);
                string nacion = funciones.getValorComboBox(cmbNacionalidad, 4);
                string email = txtEmail.Text.Trim();
                string catlic = funciones.getValorComboBox(cmbCategoriaBrevete, 4);
                string nrolic = txtNroLicenciaConductor.Text.Trim();
                string reglab = funciones.getValorComboBox(cmbRegimenLab, 4);
                string tipotrab = funciones.getValorComboBox(cmbTipoTrabajador, 4);
                string idtipoper = funciones.getValorComboBox(cmbCategoriaOcupacional, 1);
                string nivedu = funciones.getValorComboBox(cmbNivelEducativo, 4);
                string idlabper = funciones.getValorComboBox(cmbOcupacion, 2);
                string seccion = funciones.getValorComboBox(cmbSeccion, 4);
                string regpen = funciones.getValorComboBox(cmbRegimen, 4);
                string cuspp = txtCuspp.Text.Trim();
                string contrab = funciones.getValorComboBox(cmbTipoContratoTrabajo, 4);
                string tippag = funciones.getValorComboBox(cmbTipoPago, 4);
                string pering = funciones.getValorComboBox(cmbPeriodicidadIngreso, 4);
                string sitesp = funciones.getValorComboBox(cmbSituacion, 4);
                string entfinrem = funciones.getValorComboBox(cmbEntFinRem, 4);
                string nroctarem = txtNumCtaRem.Text.Trim();
                string monrem = funciones.getValorComboBox(cmbMonRem, 4);
                string tipctarem = funciones.getValorComboBox(cmbTipoCtaRem, 4);
                string entfincts = funciones.getValorComboBox(cmbEntFinCts, 4);
                string nroctacts = txtNumCtaCts.Text.Trim();
                string moncts = funciones.getValorComboBox(cmbMonCts, 4);
                string tipctacts = funciones.getValorComboBox(cmbTipoCtaCts, 4);
                string tipvia = funciones.getValorComboBox(cmbTipoVia, 4);
                string nomvia = txtNombreVia.Text.Trim();
                string nrovia = txtNumeroVia.Text.Trim();
                string intvia = txtInteriorVia.Text.Trim();
                string deparvia = txtDeparVia.Text.Trim();
                string manzavia = txtManzanaVia.Text.Trim();
                string lotevia = txtLoteVia.Text.Trim();
                string kmvia = txtKmVia.Text.Trim();
                string block = txtBlockVia.Text.Trim();
                string etapa = txtEtapaVia.Text.Trim();
                string tipzona = funciones.getValorComboBox(cmbTipoZona, 4);
                string nomzona = txtNombreZona.Text.Trim();
                string refzona = txtReferenciasZona.Text.Trim();
                string ubigeo = txtCodigoUbigeo.Text.Trim();
                string dscubigeo = txtDscUbigeo.Text.Trim();
                string idtipoplan = funciones.getValorComboBox(cmbTipoPlanilla, 3);
                string fecregpen = txtFecRegPen.Text;
                string apliconve = funciones.getValorComboBox(cmbConve, 4);
                string ruc = txtRuc.Text;
                string rem_fecini = txtIniRemu.Text;
                string rem_importe = txtImporte.Text;

                string ocurpts = string.Empty;
                if (txtOcupacion.Text.Trim() != string.Empty)
                {
                    ocurpts = txtOcupacion.Text.Substring(0, 6).Trim();
                }

                string afiliaeps;
                string eps;
                string discapa;
                string sindica;
                string sctrsanin;
                string sctrsaessa;
                string sctrsaeps;
                string sctrpennin;
                string sctrpenonp;
                string sctrpenseg;
                string asigemplea;
                string domicilia;
                string esvida;
                string regalterna;
                string trabmax;
                string trabnoc;
                string quicat;
                string renexo;
                string asepen;
                string exoquicat;

                regalterna = (chkregalterna.Checked) ? "1" : "0";
                trabmax = (chktrabmax.Checked) ? "1" : "0";
                trabnoc = (chktrabnoc.Checked) ? "1" : "0";
                quicat = (radioButtonSiQC.Checked) ? "1" : "0";
                renexo = (radioButtonSiRenExo.Checked) ? "1" : "0";
                exoquicat = (radioButtonSiRenQuiExo.Checked) ? "1" : "0";
                domicilia = (radioButtonSiDomicilia.Checked) ? "1" : "2";
                esvida = (radioButtonSiEsVida.Checked) ? "1" : "0";
                afiliaeps = (radioButtonSIEps.Checked) ? "1" : "0";
                eps = (radioButtonSIEps.Checked) ? funciones.getValorComboBox(cmbEps, 4) : string.Empty;
                discapa = (radioButtonSiDisca.Checked) ? "1" : "0";
                sindica = (radioButtonSiSin.Checked) ? "1" : "0";

                string chk_alta_sunat = (chk_alta_tregistro.Checked) ? "1" : "0";
                string chk_baja_sunat = (chk_baja_tregistro.Checked) ? "1" : "0";

                string situatrab = funciones.getValorComboBox(cmbSituaTrab, 4);

                sctrsanin = (radioButtonSctrSaludNin.Checked) ? "1" : "0";
                sctrsaessa = (radioButtonSctrSaludEs.Checked) ? "1" : "0";
                sctrsaeps = (radioButtonSctrSaludEps.Checked) ? "1" : "0";
                sctrpennin = (radioButtonSctrPensionNin.Checked) ? "1" : "0";
                asepen = (radioButtonAsePenSi.Checked) ? "1" : "0";
                sctrpenonp = (radioButtonSctrPensionOnp.Checked) ? "1" : "0";
                sctrpenseg = (radioButtonSctrPensionSeg.Checked) ? "1" : "0";
                asigemplea = (radioButtonSiEmp.Checked) ? "1" : "0";

                string idTipHorario = funciones.getValorComboBox(cmbTipHorario, 4);
                int idAsigTipHorPer = int.Parse(txtIdAsigHorPer.Text);

                string rucemp = funciones.getValorComboBox(cmbEmpleador, 11);
                string estane = funciones.getValorComboBox(cmbEstablecimiento, 4);

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
                        perplan.actualizaPerPlan(operacion, idperplan, idcia, apepat, apemat, nombres, fecnac, tipdoc, nrodoc, paisemi, disnac,
                        telfijo, celular, rpm, estcivil, nacion, email, catlic, nrolic, tipotrab, idtipoper, nivedu,
                        idlabper, seccion, regpen, cuspp, contrab, tippag, pering, sitesp, entfinrem, nroctarem, monrem,
                        tipctarem, entfincts, nroctacts, moncts, tipctacts, tipvia, nomvia, nrovia, deparvia, intvia, manzavia,
                        lotevia, kmvia, block, etapa, tipzona, nomzona, refzona, ubigeo, dscubigeo, sexo, ocurpts, afiliaeps,
                        eps, discapa, sindica, situatrab, sctrsanin, sctrsaessa, sctrsaeps, sctrpennin, sctrpenonp, sctrpenseg,
                        asigemplea, rucemp, estane, idtipoplan, esvida, domicilia, fecregpen, regalterna, trabmax, trabnoc, quicat,
                        renexo, asepen, apliconve, exoquicat, reglab, ruc, codaux, chk_alta_sunat, chk_baja_sunat);

                        Remu remu = new Remu();
                        if (operacion == "AGREGAR")
                        {
                            //Oliver Cruz Tuanama
                            perplan.asignaHorario(idcia, bd, idperplan, idTipHorario, idAsigTipHorPer);
                            remu.actualizarRemu(operacion, idcia, idperplan, "", rem_fecini, "", float.Parse(rem_importe));
                            txtImporte.Enabled = false;
                            txtIniRemu.Enabled = false;
                        }
                        else
                        {
                            if (idAsigTipHorPer.Equals(0)) { perplan.asignaHorario(idcia, bd, idperplan, idTipHorario, idAsigTipHorPer); }
                        }
                        cmbTipHorario.Enabled = false;

                        txtOperacion.Text = "EDITAR";
                        tabPageEX5.Enabled = true;
                        tabPageEX6.Enabled = true;
                        tabPageEX7.Enabled = true;
                        tabPageEX12.Enabled = true;
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

        private void cmbCategoriaOcupacional_SelectedIndexChanged(object sender, EventArgs e)
        {
            string clave = funciones.getValorComboBox(cmbCategoriaOcupacional, 1);
            string squery;

            squery = "SELECT idlabper as clave,deslabper as descripcion FROM labper WHERE idcia='" + @idcia + "' and idtipoper='" + @clave + "';";
            funciones.listaComboBox(squery, cmbOcupacion, "B");

            cmbCentroCosto.Focus();
        }

        private void ui_validarDatos(string ope)
        {
            MaesGen maesgen = new MaesGen();

            string valorValida = "G";

            if (txtApPat.Text == string.Empty && valorValida == "G")
            {
                valorValida = "B";
                MessageBox.Show("No ha ingresado Apellido Paterno", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                tabControlPer.SelectTab(0);
                txtApPat.Focus();
            }

            if (txtApMat.Text == string.Empty && valorValida == "G")
            {
                valorValida = "B";
                MessageBox.Show("No ha ingresado Apellido Materno", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                tabControlPer.SelectTab(0);
                txtApMat.Focus();
            }

            if (txtNombres.Text == string.Empty && valorValida == "G")
            {
                valorValida = "B";
                MessageBox.Show("No ha ingresado Nombres", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                tabControlPer.SelectTab(0);
                txtNombres.Focus();
            }

            if (cmbTipoDocumento.Text == string.Empty && valorValida == "G")
            {
                valorValida = "B";
                MessageBox.Show("No ha seleccionado Tipo de Documento", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                tabControlPer.SelectTab(0);
                cmbTipoDocumento.Focus();
            }

            if (cmbTipoDocumento.Text != string.Empty && valorValida == "G")
            {
                string resultado = maesgen.verificaComboBoxMaesGen("002", cmbTipoDocumento.Text.Trim());
                if (resultado.Trim() == string.Empty)
                {
                    valorValida = "B";
                    MessageBox.Show("Dato incorrecto en Tipo de Documento", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    tabControlPer.SelectTab(0);
                    cmbTipoDocumento.Focus();
                }
                else
                {
                    if (funciones.getValorComboBox(cmbTipoDocumento, 4).Trim().Equals("07"))
                    {
                        if (cmbPaisEmisor.Text == string.Empty && valorValida == "G")
                        {
                            valorValida = "B";
                            MessageBox.Show("No ha seleccionado País Emisior", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            tabControlPer.SelectTab(0);
                            cmbPaisEmisor.Focus();
                        }

                        if (cmbPaisEmisor.Text != string.Empty && valorValida == "G")
                        {
                            MaesPdt maespdt = new MaesPdt();
                            resultado = maespdt.verificaComboBoxMaesPdt("26", cmbPaisEmisor.Text.Trim());
                            if (resultado.Trim() == string.Empty)
                            {
                                valorValida = "B";
                                MessageBox.Show("Dato incorrecto en País Emisior", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                tabControlPer.SelectTab(0);
                                cmbPaisEmisor.Focus();
                            }
                        }


                    }
                }
            }

            if (txtNroDoc.Text == string.Empty && valorValida == "G")
            {
                valorValida = "B";
                MessageBox.Show("No ha ingresado Nro.Documento", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                tabControlPer.SelectTab(0);
                txtNroDoc.Focus();
            }

            if (txtNroDoc.Text != string.Empty && valorValida == "G")
            {
                if (cmbTipoDocumento.Text.Substring(0, 4).Trim() == "01")
                {
                    if (txtNroDoc.Text.Length != 8)
                    {
                        valorValida = "B";
                        MessageBox.Show("Nro.Documento incorrecto", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        tabControlPer.SelectTab(0);
                        txtNroDoc.Focus();
                    }

                }

            }

            if (UtileriasFechas.IsDate(txtFechaNac.Text) == false && valorValida == "G")
            {
                valorValida = "B";
                MessageBox.Show("Fecha de Nacimiento no válida", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                tabControlPer.SelectTab(0);
                txtFechaNac.Focus();
            }

            if (cmbNacionalidad.Text == string.Empty && valorValida == "G")
            {
                valorValida = "B";
                MessageBox.Show("No ha seleccionado Nacionalidad", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                tabControlPer.SelectTab(0);
                cmbNacionalidad.Focus();
            }

            if (cmbNacionalidad.Text != string.Empty && valorValida == "G")
            {
                string resultado = maesgen.verificaComboBoxMaesGen("003", cmbNacionalidad.Text.Trim());
                if (resultado.Trim() == string.Empty)
                {
                    valorValida = "B";
                    MessageBox.Show("Dato incorrecto en Nacionalidad", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    tabControlPer.SelectTab(0);
                    cmbNacionalidad.Focus();
                }
            }

            if (cmbNacionalidad.Text != string.Empty && valorValida == "G")
            {
                if (cmbTipoDocumento.Text.Substring(0, 4).Trim() == "01")
                {
                    if (cmbNacionalidad.Text.Substring(0, 4).Trim() != "9589")
                    {
                        valorValida = "B";
                        MessageBox.Show("Nacionalidad seleccionada incorrecta", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        tabControlPer.SelectTab(0);
                        cmbNacionalidad.Focus();
                    }
                }
            }

            if (cmbNacionalidad.Text != string.Empty && valorValida == "G")
            {
                if (cmbTipoDocumento.Text.Substring(0, 4).Trim() == "04")
                {
                    if (cmbNacionalidad.Text.Substring(0, 4).Trim() == "9589")
                    {
                        valorValida = "B";
                        MessageBox.Show("Nacionalidad seleccionada incorrecta", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        tabControlPer.SelectTab(0);
                        cmbNacionalidad.Focus();
                    }
                }
            }

            if (cmbSexo.Text == string.Empty && valorValida == "G")
            {
                valorValida = "B";
                MessageBox.Show("No ha seleccionado Sexo", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                tabControlPer.SelectTab(0);
                cmbSexo.Focus();
            }

            if (cmbSexo.Text != string.Empty && valorValida == "G")
            {
                string resultado = maesgen.verificaComboBoxMaesGen("019", cmbSexo.Text.Trim());
                if (resultado.Trim() == string.Empty)
                {
                    valorValida = "B";
                    MessageBox.Show("Dato incorrecto en Sexo", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    tabControlPer.SelectTab(0);
                    cmbSexo.Focus();
                }
            }

            if (cmbCodigosNacionales.Text != string.Empty && valorValida == "G")
            {
                MaesPdt maespdt = new MaesPdt();
                string resultado = maespdt.verificaComboBoxMaesPdt("29", cmbCodigosNacionales.Text.Trim());
                if (resultado.Trim() == string.Empty)
                {
                    valorValida = "B";
                    MessageBox.Show("Dato incorrecto en Código Larga Distancia Nacional", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    tabControlPer.SelectTab(0);
                    cmbCodigosNacionales.Focus();
                }
            }

            if (cmbCodigosNacionales.Text != string.Empty && valorValida == "G")
            {
                if (txtTelFijo.Text == string.Empty)
                {
                    valorValida = "B";
                    MessageBox.Show("No ha ingresado Nro.Teléfono", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    tabControlPer.SelectTab(0);
                    txtTelFijo.Focus();
                }
            }

            if (cmbEstadoCivil.Text == string.Empty && valorValida == "G")
            {
                valorValida = "B";
                MessageBox.Show("No ha seleccionado Estado Civil", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                tabControlPer.SelectTab(0);
                cmbEstadoCivil.Focus();
            }

            if (cmbEstadoCivil.Text != string.Empty && valorValida == "G")
            {
                string resultado = maesgen.verificaComboBoxMaesGen("001", cmbEstadoCivil.Text.Trim());
                if (resultado.Trim() == string.Empty)
                {
                    valorValida = "B";
                    MessageBox.Show("Dato incorrecto en Estado Civil", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    tabControlPer.SelectTab(0);
                    cmbEstadoCivil.Focus();
                }
            }

            if (cmbCategoriaBrevete.Text == string.Empty && valorValida == "G")
            {
                valorValida = "B";
                MessageBox.Show("No ha seleccionado Categoría de Brevete", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                tabControlPer.SelectTab(0);
                cmbCategoriaBrevete.Focus();
            }

            if (cmbCategoriaBrevete.Text != string.Empty && valorValida == "G")
            {
                string resultado = maesgen.verificaComboBoxMaesGen("004", cmbCategoriaBrevete.Text.Trim());
                if (resultado.Trim() == string.Empty)
                {
                    valorValida = "B";
                    MessageBox.Show("Dato incorrecto en Categoría de Brevete", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    tabControlPer.SelectTab(0);
                    cmbCategoriaBrevete.Focus();
                }

            }

            if (txtEmail.Text.Trim() != string.Empty && valorValida == "G")
            {
                if (funciones.email_bien_escrito(txtEmail.Text) == false)
                {
                    valorValida = "B";
                    MessageBox.Show("Correo Electrónico Inválido", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtEmail.Focus();
                }
            }

            if (funciones.getValorComboBox(cmbCategoriaBrevete, 4) != "XX" && valorValida == "G")
            {
                if (txtNroLicenciaConductor.Text == string.Empty)
                {
                    valorValida = "B";
                    MessageBox.Show("No ha ingresado Nro. de Licencia de Conducir", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    tabControlPer.SelectTab(0);
                    txtNroLicenciaConductor.Focus();
                }
            }

            if (cmbTipoVia.Text == string.Empty && valorValida == "G")
            {
                valorValida = "B";
                MessageBox.Show("No ha seleccionado Tipo de Vía", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                tabControlPer.SelectTab(3);
                cmbTipoVia.Focus();
            }

            if (cmbTipoVia.Text != string.Empty && valorValida == "G")
            {
                string resultado = maesgen.verificaComboBoxMaesGen("017", cmbTipoVia.Text.Trim());
                if (resultado.Trim() == string.Empty)
                {
                    valorValida = "B";
                    MessageBox.Show("Dato incorrecto en Tipo de Vía", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    tabControlPer.SelectTab(3);
                    cmbTipoVia.Focus();
                }
            }

            if (txtNombreVia.Text == string.Empty && valorValida == "G")
            {
                valorValida = "B";
                MessageBox.Show("No ha ingresado Nombre de Vía", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                tabControlPer.SelectTab(3);
                txtNombreVia.Focus();
            }

            if (txtNumeroVia.Text == string.Empty && valorValida == "G")
            {
                valorValida = "B";
                MessageBox.Show("No ha ingresado Interior", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                tabControlPer.SelectTab(3);
                txtNumeroVia.Focus();
            }

            if (txtInteriorVia.Text == string.Empty && valorValida == "G")
            {
                valorValida = "B";
                MessageBox.Show("No ha ingresado Interior", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                tabControlPer.SelectTab(3);
                txtInteriorVia.Focus();
            }

            if (cmbTipoZona.Text == string.Empty && valorValida == "G")
            {
                valorValida = "B";
                MessageBox.Show("No ha seleccionado Tipo de Zona", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                tabControlPer.SelectTab(3);
                cmbTipoZona.Focus();
            }

            if (cmbTipoZona.Text != string.Empty && valorValida == "G")
            {
                string resultado = maesgen.verificaComboBoxMaesGen("018", cmbTipoZona.Text.Trim());
                if (resultado.Trim() == string.Empty)
                {
                    valorValida = "B";
                    MessageBox.Show("Dato incorrecto en Tipo de Zona", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    tabControlPer.SelectTab(3);
                    cmbTipoZona.Focus();
                }
            }

            if (txtNombreZona.Text == string.Empty && valorValida == "G")
            {
                valorValida = "B";
                MessageBox.Show("No ha ingresado Nombre de Zona", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                tabControlPer.SelectTab(3);
                txtNombreZona.Focus();
            }

            if (txtCodigoUbigeo.Text == string.Empty && valorValida == "G")
            {
                valorValida = "B";
                MessageBox.Show("No ha seleccionado Ubigeo", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                tabControlPer.SelectTab(1);
                txtCodigoUbigeo.Focus();
            }

            if (cmbRegimenLab.Text == string.Empty && valorValida == "G")
            {
                valorValida = "B";
                MessageBox.Show("No ha seleccionado Régimen Laboral", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                tabControlPer.SelectTab(2);
                cmbRegimenLab.Focus();
            }

            if (cmbRegimenLab.Text != string.Empty && valorValida == "G")
            {
                string resultado = maesgen.verificaComboBoxMaesGen("038", cmbRegimenLab.Text.Trim());
                if (resultado.Trim() == string.Empty)
                {
                    valorValida = "B";
                    MessageBox.Show("Dato incorrecto en Régimen Laboral", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    tabControlPer.SelectTab(2);
                    cmbRegimenLab.Focus();
                }
            }

            if (cmbTipoTrabajador.Text == string.Empty && valorValida == "G")
            {
                valorValida = "B";
                MessageBox.Show("No ha seleccionado Tipo de Trabajador", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                tabControlPer.SelectTab(2);
                cmbTipoTrabajador.Focus();
            }

            if (cmbTipoTrabajador.Text != string.Empty && valorValida == "G")
            {
                string resultado = maesgen.verificaComboBoxMaesGen("005", cmbTipoTrabajador.Text.Trim());
                if (resultado.Trim() == string.Empty)
                {
                    valorValida = "B";
                    MessageBox.Show("Dato incorrecto en Tipo de Trabajador", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    tabControlPer.SelectTab(2);
                    cmbTipoTrabajador.Focus();
                }
            }

            if (funciones.getValorComboBox(cmbTipoTrabajador, 4).Equals("67"))
            {
                if (txtRuc.Text == string.Empty && valorValida == "G")
                {
                    valorValida = "B";
                    MessageBox.Show("No ha ingresado R.U.C.", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    tabControlPer.SelectTab(2);
                    txtRuc.Focus();
                }
            }

            if (cmbCategoriaOcupacional.Text == string.Empty && valorValida == "G")
            {
                valorValida = "B";
                MessageBox.Show("No ha seleccionado Categoría Ocupacional", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                tabControlPer.SelectTab(2);
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
                    tabControlPer.SelectTab(2);
                    cmbCategoriaOcupacional.Focus();
                }
            }

            if (cmbNivelEducativo.Text == string.Empty && valorValida == "G")
            {
                valorValida = "B";
                MessageBox.Show("No ha seleccionado Nivel Educativo", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                tabControlPer.SelectTab(2);
                cmbNivelEducativo.Focus();
            }

            if (cmbNivelEducativo.Text != string.Empty && valorValida == "G")
            {
                string resultado = maesgen.verificaComboBoxMaesGen("006", cmbNivelEducativo.Text.Trim());
                if (resultado.Trim() == string.Empty)
                {
                    valorValida = "B";
                    MessageBox.Show("Dato incorrecto en Nivel Educativo", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    tabControlPer.SelectTab(2);
                    cmbNivelEducativo.Focus();
                }
            }

            if (cmbOcupacion.Text == string.Empty && valorValida == "G")
            {
                valorValida = "B";
                MessageBox.Show("No ha seleccionado Ocupación", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                tabControlPer.SelectTab(2);
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
                    tabControlPer.SelectTab(2);
                    cmbOcupacion.Focus();
                }
            }

            if (cmbSeccion.Text == string.Empty && valorValida == "G")
            {
                valorValida = "B";
                MessageBox.Show("No ha seleccionado Sección", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                tabControlPer.SelectTab(2);
                cmbSeccion.Focus();
            }

            if (cmbSeccion.Text != string.Empty && valorValida == "G")
            {
                string resultado = maesgen.verificaComboBoxMaesGen("008", cmbSeccion.Text.Trim());
                if (resultado.Trim() == string.Empty)
                {
                    valorValida = "B";
                    MessageBox.Show("Dato incorrecto en Sección", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    tabControlPer.SelectTab(2);
                    cmbSeccion.Focus();
                }
            }

            if (cmbRegimen.Text == string.Empty && valorValida == "G")
            {
                valorValida = "B";
                MessageBox.Show("No ha seleccionado Régimen Pensionario", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                tabControlPer.SelectTab(2);
                cmbRegimen.Focus();
            }

            if (cmbRegimen.Text != string.Empty && valorValida == "G")
            {
                string resultado = maesgen.verificaComboBoxMaesGen("009", cmbRegimen.Text.Trim());
                if (resultado.Trim() == string.Empty)
                {
                    valorValida = "B";
                    MessageBox.Show("Dato incorrecto en Régimen Pensionario", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    tabControlPer.SelectTab(2);
                    cmbRegimen.Focus();
                }
            }

            if (cmbTipoPlanilla.Text == string.Empty && valorValida == "G")
            {
                valorValida = "B";
                MessageBox.Show("No ha seleccionado Régimen Laboral", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                tabControlPer.SelectTab(2);
                cmbTipoPlanilla.Focus();
            }

            if (cmbTipoPlanilla.Text != string.Empty && valorValida == "G")
            {
                string idtipoplan = funciones.getValorComboBox(cmbTipoPlanilla, 3);
                string query = "SELECT idtipoplan as clave,destipoplan as descripcion FROM tipoplan WHERE idtipoplan='" + @idtipoplan + "' and idtipoplan in (SELECT  idtipoplan FROM reglabcia WHERE idcia='" + @idcia + "');";
                string resultado = funciones.verificaItemComboBox(query, cmbTipoPlanilla);

                if (resultado.Trim() == string.Empty)
                {
                    valorValida = "B";
                    MessageBox.Show("Dato incorrecto en Régimen Laboral", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    tabControlPer.SelectTab(2);
                    cmbTipoPlanilla.Focus();
                }
            }

            if (UtileriasFechas.IsDate(txtFecRegPen.Text) == false && valorValida == "G")
            {
                valorValida = "B";
                MessageBox.Show("Fecha de Inscripción al Régimen Pensionario Inválida", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                tabControlPer.SelectTab(2);
                txtFecRegPen.Focus();
            }

            if (txtCuspp.Text == string.Empty && valorValida == "G" && (cmbRegimen.Text.Substring(0, 4).Trim() == "21" || cmbRegimen.Text.Substring(0, 4).Trim() == "22" || cmbRegimen.Text.Substring(0, 4).Trim() == "23" || cmbRegimen.Text.Substring(0, 4).Trim() == "24" || cmbRegimen.Text.Substring(0, 4).Trim() == "25"))
            {
                valorValida = "B";
                MessageBox.Show("No ha ingresado CUSPP", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                tabControlPer.SelectTab(2);
                txtCuspp.Focus();
            }

            if (txtCuspp.Text != string.Empty && valorValida == "G" && (cmbRegimen.Text.Substring(0, 4).Trim() != "21" && cmbRegimen.Text.Substring(0, 4).Trim() != "22" && cmbRegimen.Text.Substring(0, 4).Trim() != "23" && cmbRegimen.Text.Substring(0, 4).Trim() != "24" && cmbRegimen.Text.Substring(0, 4).Trim() != "25"))
            {
                valorValida = "B";
                MessageBox.Show("CUSPP no solicitado", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                tabControlPer.SelectTab(2);
                txtCuspp.Focus();
            }

            if (cmbTipoContratoTrabajo.Text == string.Empty && valorValida == "G")
            {
                valorValida = "B";
                MessageBox.Show("No ha seleccionado Tipo de Contrato de Trabajo", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                tabControlPer.SelectTab(2);
                cmbTipoContratoTrabajo.Focus();
            }

            if (cmbTipoContratoTrabajo.Text != string.Empty && valorValida == "G")
            {
                string resultado = maesgen.verificaComboBoxMaesGen("010", cmbTipoContratoTrabajo.Text.Trim());
                if (resultado.Trim() == string.Empty)
                {
                    valorValida = "B";
                    MessageBox.Show("Dato incorrecto en Tipo de Contrato de Trabajo", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    tabControlPer.SelectTab(2);
                    cmbTipoContratoTrabajo.Focus();
                }
            }

            if (cmbTipoPago.Text == string.Empty && valorValida == "G")
            {
                valorValida = "B";
                MessageBox.Show("No ha seleccionado Tipo de Pago", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                tabControlPer.SelectTab(2);
                cmbTipoPago.Focus();
            }

            if (cmbTipoPago.Text != string.Empty && valorValida == "G")
            {
                string resultado = maesgen.verificaComboBoxMaesGen("013", cmbTipoPago.Text.Trim());
                if (resultado.Trim() == string.Empty)
                {
                    valorValida = "B";
                    MessageBox.Show("Dato incorrecto en Tipo de Pago", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    tabControlPer.SelectTab(2);
                    cmbTipoPago.Focus();
                }
            }

            if (cmbTipoPago.Text == string.Empty && valorValida == "G")
            {
                valorValida = "B";
                MessageBox.Show("No ha seleccionado Tipo de Pago", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                tabControlPer.SelectTab(2);
                cmbTipoPago.Focus();
            }

            if (cmbTipoPago.Text != string.Empty && valorValida == "G")
            {
                string resultado = maesgen.verificaComboBoxMaesGen("013", cmbTipoPago.Text.Trim());
                if (resultado.Trim() == string.Empty)
                {
                    valorValida = "B";
                    MessageBox.Show("Dato incorrecto en Tipo de Pago", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    tabControlPer.SelectTab(2);
                    cmbTipoPago.Focus();
                }
            }

            if (cmbSituacion.Text == string.Empty && valorValida == "G")
            {
                valorValida = "B";
                MessageBox.Show("No ha seleccionado Situación Especial", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                tabControlPer.SelectTab(2);
                cmbSituacion.Focus();
            }

            if (cmbSituacion.Text != string.Empty && valorValida == "G")
            {
                string resultado = maesgen.verificaComboBoxMaesGen("012", cmbSituacion.Text.Trim());
                if (resultado.Trim() == string.Empty)
                {
                    valorValida = "B";
                    MessageBox.Show("Dato incorrecto en Situación Especial", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    tabControlPer.SelectTab(2);
                    cmbSituacion.Focus();
                }
            }

            if (cmbEntFinRem.Text != string.Empty && valorValida == "G")
            {
                string resultado = maesgen.verificaComboBoxMaesGen("007", cmbEntFinRem.Text.Trim());
                if (resultado.Trim() == string.Empty)
                {
                    valorValida = "B";
                    MessageBox.Show("Dato incorrecto en Entidad Financiera Remuneraciones", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    tabControlPer.SelectTab(3);
                    cmbEntFinRem.Focus();
                }
            }

            if (cmbMonRem.Text != string.Empty && valorValida == "G")
            {
                string resultado = maesgen.verificaComboBoxMaesGen("015", cmbMonRem.Text.Trim());
                if (resultado.Trim() == string.Empty)
                {
                    valorValida = "B";
                    MessageBox.Show("Dato incorrecto en Moneda", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    tabControlPer.SelectTab(3);
                    cmbMonRem.Focus();
                }
            }

            if (cmbTipoCtaRem.Text != string.Empty && valorValida == "G")
            {
                string resultado = maesgen.verificaComboBoxMaesGen("014", cmbTipoCtaRem.Text.Trim());
                if (resultado.Trim() == string.Empty)
                {
                    valorValida = "B";
                    MessageBox.Show("Dato incorrecto en Tipo de Cuenta de Remuneraciones", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    tabControlPer.SelectTab(3);
                    cmbTipoCtaRem.Focus();
                }
            }


            if (cmbEntFinCts.Text != string.Empty && valorValida == "G")
            {
                string resultado = maesgen.verificaComboBoxMaesGen("007", cmbEntFinCts.Text.Trim());
                if (resultado.Trim() == string.Empty)
                {
                    valorValida = "B";
                    MessageBox.Show("Dato incorrecto en Entidad Financiera CTS", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    tabControlPer.SelectTab(3);
                    cmbEntFinCts.Focus();
                }
            }

            if (cmbMonCts.Text != string.Empty && valorValida == "G")
            {
                string resultado = maesgen.verificaComboBoxMaesGen("015", cmbMonCts.Text.Trim());
                if (resultado.Trim() == string.Empty)
                {
                    valorValida = "B";
                    MessageBox.Show("Dato incorrecto en Moneda", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    tabControlPer.SelectTab(3);
                    cmbMonCts.Focus();
                }
            }

            if (cmbTipoCtaCts.Text != string.Empty && valorValida == "G")
            {
                string resultado = maesgen.verificaComboBoxMaesGen("014", cmbTipoCtaCts.Text.Trim());
                if (resultado.Trim() == string.Empty)
                {
                    valorValida = "B";
                    MessageBox.Show("Dato incorrecto en Tipo de Cuenta de CTS", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    tabControlPer.SelectTab(3);
                    cmbTipoCtaCts.Focus();
                }
            }

            if (txtOcupacion.Text == string.Empty && valorValida == "G")
            {
                valorValida = "B";
                MessageBox.Show("No ha seleccionado Ocupación según Planilla Electrónica", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                tabControlPer.SelectTab(4);
                btnOcupacion.Focus();
            }

            if (radioButtonSIEps.Checked)
            {
                if (cmbEps.Text == string.Empty && valorValida == "G")
                {
                    valorValida = "B";
                    MessageBox.Show("No ha seleccionado E.P.S.", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    tabControlPer.SelectTab(4);
                    cmbEps.Focus();
                }

                if (cmbEps.Text != string.Empty && valorValida == "G")
                {
                    string resultado = maesgen.verificaComboBoxMaesGen("028", cmbEps.Text.Trim());
                    if (resultado.Trim() == string.Empty)
                    {
                        valorValida = "B";
                        MessageBox.Show("Dato incorrecto en E.P.S.", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        tabControlPer.SelectTab(4);
                        cmbEps.Focus();
                    }
                }
            }


            if (cmbSituaTrab.Text == string.Empty && valorValida == "G")
            {
                valorValida = "B";
                MessageBox.Show("No ha seleccionado Situación del Trabajador", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                tabControlPer.SelectTab(4);
                cmbSituaTrab.Focus();
            }

            if (cmbSituaTrab.Text != string.Empty && valorValida == "G")
            {
                string resultado = maesgen.verificaComboBoxMaesGen("029", cmbSituaTrab.Text.Trim());
                if (resultado.Trim() == string.Empty)
                {
                    valorValida = "B";
                    MessageBox.Show("Dato incorrecto en Situación del Trabajador", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    tabControlPer.SelectTab(4);
                    cmbSituaTrab.Focus();
                }
            }

            if (cmbEmpleador.Text == string.Empty && valorValida == "G")
            {
                valorValida = "B";
                MessageBox.Show("Ir a la opción Configuración de Empleadores - Empleadores a quienes destaco o desplazo personal", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                tabControlPer.SelectTab(4);
                cmbEmpleador.Focus();
            }

            if (cmbEmpleador.Text != string.Empty && valorValida == "G")
            {
                string squery;
                string srucemp = funciones.getValorComboBox(cmbEmpleador, 11);
                if (radioButtonSiEmp.Checked)
                {
                    squery = "Select rucemp as clave,razonemp as descripcion from emplea where idciafile='" + globalvariables.getValorCia() + "' and rucemp='" + @srucemp + "';";
                }
                else
                {
                    squery = "Select ruccia as clave,descia as descripcion from ciafile where idcia='" + globalvariables.getValorCia() + "';";
                }

                string resultado = funciones.verificaItemComboBox(squery, cmbEmpleador);

                if (resultado.Trim() == string.Empty)
                {
                    valorValida = "B";
                    MessageBox.Show("Dato incorrecto en Empleador", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    tabControlPer.SelectTab(4);
                    cmbEmpleador.Focus();
                }
            }

            if (cmbEstablecimiento.Text == string.Empty && valorValida == "G")
            {
                string mensaje;
                if (radioButtonSiEmp.Checked)
                {
                    mensaje = "Ir a la opción Información de Empleadores - Empleadores a quienes destaco o desplazo personal - Ver Establecimientos";
                }
                else
                {
                    mensaje = "Ir a la opción Información de Empleadores - Domicilio Fiscal y Establecimientos Anexos";
                }

                valorValida = "B";
                MessageBox.Show(mensaje, "Validacion de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                tabControlPer.SelectTab(4);
                cmbEmpleador.Focus();
            }

            if (cmbEstablecimiento.Text != string.Empty && valorValida == "G")
            {
                string squery;
                string srucemp = funciones.getValorComboBox(cmbEmpleador, 11);
                string sestane = funciones.getValorComboBox(cmbEstablecimiento, 4);
                squery = "Select idestane as clave,desestane as descripcion from estane where idciafile='" + globalvariables.getValorCia() + "' and codemp='" + @srucemp + "' and idestane='" + @sestane + "';";
                string resultado = funciones.verificaItemComboBox(squery, cmbEstablecimiento);

                if (resultado.Trim() == string.Empty)
                {
                    valorValida = "B";
                    MessageBox.Show("Dato incorrecto en Establecimiento asignado", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    tabControlPer.SelectTab(4);
                    cmbEstablecimiento.Focus();
                }
            }

            if (cmbConve.Text != string.Empty && valorValida == "G")
            {
                string resultado = maesgen.verificaComboBoxMaesGen("036", cmbConve.Text.Trim());
                if (resultado.Trim() == string.Empty)
                {
                    valorValida = "B";
                    MessageBox.Show("Dato incorrecto en Convenio para evitar doble imposición", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    tabControlPer.SelectTab(4);
                    cmbConve.Focus();
                }
            }

            if (cmbPeriodicidadIngreso.Text == string.Empty && valorValida == "G")
            {
                valorValida = "B";
                MessageBox.Show("No ha seleccionado Periodicidad de Ingreso", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                tabControlPer.SelectTab(4);
                cmbPeriodicidadIngreso.Focus();
            }

            if (cmbPeriodicidadIngreso.Text != string.Empty && valorValida == "G")
            {
                string resultado = maesgen.verificaComboBoxMaesGen("011", cmbPeriodicidadIngreso.Text.Trim());
                if (resultado.Trim() == string.Empty)
                {
                    valorValida = "B";
                    MessageBox.Show("Dato incorrecto en Periodicidad de Ingreso", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    tabControlPer.SelectTab(4);
                    cmbPeriodicidadIngreso.Focus();
                }
            }
            if (ope == "AGREGAR")
            {
                if (txtImporte.Text == string.Empty && valorValida == "G")
                {
                    valorValida = "B";
                    MessageBox.Show("No Ingreso Importe de Remuneracion", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    tabControlPer.SelectTab(5);
                    txtImporte.Focus();
                }

                if (txtIniRemu.Text == string.Empty && valorValida == "G")
                {
                    valorValida = "B";
                    MessageBox.Show("No Ingreso Fecha Inicio de Remuneracion", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    tabControlPer.SelectTab(5);
                    txtIniRemu.Focus();
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

        public void ui_validarDerechohabiente()
        {
            MaesGen maesgen = new MaesGen();

            string valorValida = "G";

            if (txtApPatDh.Text == string.Empty && valorValida == "G")
            {
                valorValida = "B";
                MessageBox.Show("No ha ingresado Apellido Paterno del Derechohabiente", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                tabControlDh.SelectTab(0);
                txtApPatDh.Focus();
            }

            if (txtApMatDh.Text == string.Empty && valorValida == "G")
            {
                valorValida = "B";
                MessageBox.Show("No ha ingresado Apellido Materno del Derechohabiente", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                tabControlDh.SelectTab(0);
                txtApMatDh.Focus();
            }

            if (txtNombresDh.Text == string.Empty && valorValida == "G")
            {
                valorValida = "B";
                MessageBox.Show("No ha ingresado Nombres del Derechohabiente", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                tabControlDh.SelectTab(0);
                txtNombresDh.Focus();
            }

            if (cmbTipoDocumentoDh.Text == string.Empty && valorValida == "G")
            {
                valorValida = "B";
                MessageBox.Show("No ha seleccionado Tipo de Documento del Derechohabiente", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                tabControlDh.SelectTab(0);
                cmbTipoDocumentoDh.Focus();
            }

            if (cmbTipoDocumentoDh.Text != string.Empty && valorValida == "G")
            {
                string resultado = maesgen.verificaComboBoxMaesGen("002", cmbTipoDocumentoDh.Text.Trim());
                if (resultado.Trim() == string.Empty)
                {
                    valorValida = "B";
                    MessageBox.Show("Dato incorrecto en Tipo de Documento del Derechohabiente", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    tabControlDh.SelectTab(0);
                    cmbTipoDocumentoDh.Focus();
                }
            }

            if (txtNroDocDh.Text == string.Empty && valorValida == "G")
            {
                valorValida = "B";
                MessageBox.Show("No ha ingresado Nro.Documento del Derechohabiente", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                tabControlDh.SelectTab(0);
                txtNroDocDh.Focus();
            }

            if (txtNroDocDh.Text != string.Empty && valorValida == "G")
            {
                if (cmbTipoDocumentoDh.Text.Substring(0, 4).Trim() == "01")
                {
                    if (txtNroDocDh.Text.Length != 8)
                    {
                        valorValida = "B";
                        MessageBox.Show("Nro.Documento incorrecto", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        tabControlDh.SelectTab(0);
                        txtNroDocDh.Focus();
                    }

                }

            }

            if (UtileriasFechas.IsDate(txtFechaNacDh.Text) == false && valorValida == "G")
            {
                valorValida = "B";
                MessageBox.Show("Fecha de Nacimiento no válida", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                tabControlDh.SelectTab(0);
                txtFechaNacDh.Focus();
            }



            if (cmbSexoDh.Text == string.Empty && valorValida == "G")
            {
                valorValida = "B";
                MessageBox.Show("No ha seleccionado Sexo del Derechohabiente", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                tabControlDh.SelectTab(0);
                cmbSexoDh.Focus();
            }

            if (cmbSexoDh.Text != string.Empty && valorValida == "G")
            {
                string resultado = maesgen.verificaComboBoxMaesGen("019", cmbSexoDh.Text.Trim());
                if (resultado.Trim() == string.Empty)
                {
                    valorValida = "B";
                    MessageBox.Show("Dato incorrecto en Sexo del Derechohabiente", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    tabControlDh.SelectTab(0);
                    cmbSexoDh.Focus();
                }
            }


            if (cmbVinculoDh.Text == string.Empty && valorValida == "G")
            {
                valorValida = "B";
                MessageBox.Show("No ha seleccionado Vínculo Familiar", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                tabControlDh.SelectTab(1);
                cmbVinculoDh.Focus();
            }

            if (cmbVinculoDh.Text != string.Empty && valorValida == "G")
            {
                string resultado = maesgen.verificaComboBoxMaesGen("020", cmbVinculoDh.Text.Trim());
                if (resultado.Trim() == string.Empty)
                {
                    valorValida = "B";
                    MessageBox.Show("Dato incorrecto en Vínculo Familiar del Derechohabiente", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    tabControlDh.SelectTab(1);
                    cmbVinculoDh.Focus();
                }
            }


            if (cmbDocPatDh.Text == string.Empty && valorValida == "G" && funciones.getValorComboBox(cmbVinculoDh, 4).Trim() == "4")
            {
                valorValida = "B";
                MessageBox.Show("No ha seleccionado Documento que acredita la paternidad", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                tabControlDh.SelectTab(1);
                cmbDocPatDh.Focus();
            }

            if (cmbDocPatDh.Text != string.Empty && valorValida == "G" && funciones.getValorComboBox(cmbVinculoDh, 4).Trim() == "4")
            {
                string resultado = maesgen.verificaComboBoxMaesGen("021", cmbDocPatDh.Text.Trim());
                if (resultado.Trim() == string.Empty)
                {
                    valorValida = "B";
                    MessageBox.Show("Dato incorrecto en Documento que acredita la paternidad", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    tabControlDh.SelectTab(1);
                    cmbDocPatDh.Focus();
                }
            }

            if (cmbSituacionDh.Text == string.Empty && valorValida == "G")
            {
                valorValida = "B";
                MessageBox.Show("No ha seleccionado Situación del Derechohabiente", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                tabControlDh.SelectTab(1);
                cmbSituacionDh.Focus();
            }

            if (cmbSituacionDh.Text != string.Empty && valorValida == "G")
            {
                string resultado = maesgen.verificaComboBoxMaesGen("022", cmbSituacionDh.Text.Trim());
                if (resultado.Trim() == string.Empty)
                {
                    valorValida = "B";
                    MessageBox.Show("Dato incorrecto en Situación del Derechohabiente", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    tabControlDh.SelectTab(1);
                    cmbSituacionDh.Focus();
                }

            }

            if (UtileriasFechas.IsDate(txtAltaDh.Text) == false && valorValida == "G")
            {
                valorValida = "B";
                MessageBox.Show("Fecha de Alta no válida", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                tabControlDh.SelectTab(1);
                txtAltaDh.Focus();
            }


            if (cmbMotBajaDh.Text == string.Empty && valorValida == "G" && funciones.getValorComboBox(cmbSituacionDh, 4).Trim() == "11")
            {
                valorValida = "B";
                MessageBox.Show("No ha seleccionado Motivo de Baja del Derechohabiente", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                tabControlDh.SelectTab(1);
                cmbMotBajaDh.Focus();
            }

            if (cmbMotBajaDh.Text != string.Empty && valorValida == "G" && funciones.getValorComboBox(cmbSituacionDh, 4).Trim() == "11")
            {
                string resultado = maesgen.verificaComboBoxMaesGen("023", cmbMotBajaDh.Text.Trim());
                if (resultado.Trim() == string.Empty)
                {
                    valorValida = "B";
                    MessageBox.Show("Dato incorrecto en Motivo de Baja del Derechohabiente", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    tabControlDh.SelectTab(1);
                    cmbMotBajaDh.Focus();
                }

            }

            if (UtileriasFechas.IsDate(txtBajaDh.Text) == false && valorValida == "G" && funciones.getValorComboBox(cmbSituacionDh, 4).Trim() == "11")
            {
                valorValida = "B";
                MessageBox.Show("Fecha de Baja no válida", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                tabControlDh.SelectTab(1);
                txtBajaDh.Focus();
            }

            if (radioButtonOtroDom.Checked == true)
            {
                if (cmbTipoViaDh.Text == string.Empty && valorValida == "G")
                {
                    valorValida = "B";
                    MessageBox.Show("No ha seleccionado Tipo de Vía", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    tabControlDh.SelectTab(2);
                    cmbTipoViaDh.Focus();
                }

                if (cmbTipoViaDh.Text != string.Empty && valorValida == "G")
                {
                    string resultado = maesgen.verificaComboBoxMaesGen("017", cmbTipoViaDh.Text.Trim());
                    if (resultado.Trim() == string.Empty)
                    {
                        valorValida = "B";
                        MessageBox.Show("Dato incorrecto en Tipo de Vía", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        tabControlDh.SelectTab(2);
                        cmbTipoViaDh.Focus();
                    }
                }

                if (txtNombreViaDh.Text == string.Empty && valorValida == "G")
                {
                    valorValida = "B";
                    MessageBox.Show("No ha ingresado Nombre de Vía", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    tabControlDh.SelectTab(2);
                    txtNombreViaDh.Focus();
                }

                if (txtNumeroViaDh.Text == string.Empty && valorValida == "G")
                {
                    valorValida = "B";
                    MessageBox.Show("No ha ingresado Número", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    tabControlDh.SelectTab(2);
                    txtNumeroViaDh.Focus();
                }

                if (txtInteriorViaDh.Text == string.Empty && valorValida == "G")
                {
                    valorValida = "B";
                    MessageBox.Show("No ha ingresado Interior", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    tabControlDh.SelectTab(2);
                    txtInteriorViaDh.Focus();
                }

                if (cmbTipoZonaDh.Text == string.Empty && valorValida == "G")
                {
                    valorValida = "B";
                    MessageBox.Show("No ha seleccionado Tipo de Zona", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    tabControlDh.SelectTab(2);
                    cmbTipoZonaDh.Focus();
                }

                if (cmbTipoZonaDh.Text != string.Empty && valorValida == "G")
                {
                    string resultado = maesgen.verificaComboBoxMaesGen("018", cmbTipoZonaDh.Text.Trim());
                    if (resultado.Trim() == string.Empty)
                    {
                        valorValida = "B";
                        MessageBox.Show("Dato incorrecto en Tipo de Zona", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        tabControlDh.SelectTab(2);
                        cmbTipoZonaDh.Focus();
                    }
                }

                if (txtNombreZonaDh.Text == string.Empty && valorValida == "G")
                {
                    valorValida = "B";
                    MessageBox.Show("No ha ingresado Nombre de Zona", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    tabControlDh.SelectTab(2);
                    txtNombreZonaDh.Focus();
                }

                if (txtCodigoUbigeoDh.Text == string.Empty && valorValida == "G")
                {
                    valorValida = "B";
                    MessageBox.Show("No ha seleccionado Ubigeo", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    tabControlDh.SelectTab(2);
                    txtCodigoUbigeoDh.Focus();
                }
            }





            if (valorValida.Equals("G")) //GOOD 
            {
                txtValidDh.Text = "G";
                MessageBox.Show("Registro validado exitosamente", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                if (valorValida.Equals("B")) //BAD
                {
                    txtValidDh.Text = "B";

                }
                else
                {
                    txtValidDh.Text = "Q"; //QUESTION
                }
            }

        }

        private void pictureValidOk_Click(object sender, EventArgs e)
        {

        }

        private void cmbCategoriaBrevete_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            string clave = funciones.getValorComboBox(cmbCategoriaBrevete, 4);
            if (clave.Equals("XX"))
            {
                txtNroLicenciaConductor.Text = "";
                txtNroLicenciaConductor.Enabled = false;
            }
            else
            {

                txtNroLicenciaConductor.Enabled = true;
                txtNroLicenciaConductor.Focus();
            }
        }

        private void ui_limpiarPerLab()
        {
            txtOpePerLab.Text = string.Empty;
            txtCodigoPeriodo.Text = string.Empty;
            txtInicioPeriodo.Text = string.Empty;
            txtFinPeriodo.Text = string.Empty;
            cmbMotivoFinPeriodo.Text = string.Empty;
            txtInicioPeriodo.Enabled = false;
            txtFinPeriodo.Enabled = false;
            cmbMotivoFinPeriodo.Enabled = false;

        }

        private void ui_limpiarDireccionDerechohabiente()
        {

            cmbTipoViaDh.Text = string.Empty;
            txtNombreViaDh.Text = string.Empty;
            txtNumeroViaDh.Text = string.Empty;
            txtInteriorViaDh.Text = string.Empty;
            cmbTipoZonaDh.Text = string.Empty;
            txtNombreZonaDh.Text = string.Empty;
            txtReferenciasZonaDh.Text = string.Empty;
            txtCodigoUbigeoDh.Text = string.Empty;
            txtDscUbigeoDh.Text = string.Empty;
        }

        private void ui_limpiarFonPen()
        {
            txtOpeFonPen.Clear();
            txtCodigoFonPen.Clear();
            txtIniFonPen.Clear();
            txtFinFonPen.Clear();
            txtAVT.Clear();
            txtAVE.Clear();
            cmbFonPen.Text = string.Empty;
            txtIniFonPen.Enabled = false;
            txtFinFonPen.Enabled = false;
            cmbFonPen.Enabled = false;
        }

        private void ui_limpiarCenPer()
        {
            cmbCentroCosto.Text = string.Empty;
            txtPorcentaje.Text = string.Empty;
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            string idperplan = txtCodigoInterno.Text;
            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();
            string query = "SELECT * from perlab where stateperlab='V' and idcia='" + @idcia + "' and ";
            query = query + " idperplan='" + @idperplan + "' ";
            try
            {
                SqlCommand myCommand_table = new SqlCommand(query, conexion);
                DataTable dt_table = new DataTable();
                dt_table.Load(myCommand_table.ExecuteReader());
                myCommand_table.Dispose();
                if (dt_table.Rows.Count > 0)
                {
                    MessageBox.Show("Para ingresar otro periodo debe de cerrar el primero", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    ui_limpiarPerLab();
                    txtOpePerLab.Text = "AGREGAR";
                    txtInicioPeriodo.Enabled = true;
                    txtFinPeriodo.Enabled = false;
                    cmbMotivoFinPeriodo.Enabled = false;
                    txtInicioPeriodo.Focus();
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            conexion.Close();
        }

        public void ui_listaPeriodosLaborales(string idcia, string idperplan)
        {
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();
            string query = "select A.fechaini,A.fechafin,B.desmaesgen,A.idperlab,A.idcia,";
            query += " A.idperplan,A.stateperlab,A.motivo from perlab A ";
            query += " left join maesgen B on A.motivo=B.clavemaesgen and ";
            query += " B.idmaesgen='016' where idcia='" + @idcia + "' ";
            query += " and idperplan='" + @idperplan + "' order by A.idperlab asc;";
            try
            {
                using (SqlDataAdapter myDataAdapter = new SqlDataAdapter(query, conexion))
                {
                    DataSet myDataSet = new DataSet();
                    myDataAdapter.Fill(myDataSet, "tblPerLab");
                    //myDataAdapter.Fill(myDataSet, "PerLab");
                    funciones.formatearDataGridView(dgvPeriodosLaborales);
                    dgvPeriodosLaborales.DataSource = myDataSet.Tables["tblPerLab"];
                    //dgvPeriodosLaborales.DataSource = myDataSet.Tables["PerLab"];
                    dgvPeriodosLaborales.Columns[0].HeaderText = "Fecha Inicio / Reinicio";
                    dgvPeriodosLaborales.Columns[1].HeaderText = "Fecha Fin o Cese / Suspensión";
                    dgvPeriodosLaborales.Columns[2].HeaderText = "Motivo del Fin del Periodo";

                    dgvPeriodosLaborales.Columns["idperlab"].Visible = false;
                    dgvPeriodosLaborales.Columns["idcia"].Visible = false;
                    dgvPeriodosLaborales.Columns["idperplan"].Visible = false;
                    dgvPeriodosLaborales.Columns["stateperlab"].Visible = false;
                    dgvPeriodosLaborales.Columns["motivo"].Visible = false;


                    dgvPeriodosLaborales.Columns[0].Width = 100;
                    dgvPeriodosLaborales.Columns[1].Width = 100;
                    dgvPeriodosLaborales.Columns[2].Width = 450;

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }


            conexion.Close();
        }

        public void ui_listaDerechohabientes(string idcia, string idperplan)
        {
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();
            string query;
            query = "Select CONCAT(CONCAT(CONCAT(A.apepat,' '),CONCAT(A.apemat,' , ')),A.nombres) as nombre,A.fecnac,";
            query = query + "B.desmaesgen as vinfam,C.desmaesgen as situacion,A.idcia,A.idperplan,A.idderhab from derhab A left join maesgen B on A.vinfam=B.clavemaesgen and B.idmaesgen='020'";
            query = query + "left join maesgen C on A.sitdh=C.clavemaesgen and C.idmaesgen='022' where A.idcia='" + @idcia + "' and idperplan='" + @idperplan + "' order by vinfam asc;";

            try
            {
                using (SqlDataAdapter myDataAdapter = new SqlDataAdapter(query, conexion))
                {
                    DataSet myDataSet = new DataSet();
                    myDataAdapter.Fill(myDataSet, "tblDerHab");
                    funciones.formatearDataGridView(dgvDh);

                    dgvDh.DataSource = myDataSet.Tables["tblDerHab"];
                    dgvDh.Columns[0].HeaderText = "Apellidos y Nombres";
                    dgvDh.Columns[1].HeaderText = "Fecha Nac.";
                    dgvDh.Columns[2].HeaderText = "Vínculo Familiar";
                    dgvDh.Columns[3].HeaderText = "Situación";

                    dgvDh.Columns["idderhab"].Visible = false;
                    dgvDh.Columns["idcia"].Visible = false;
                    dgvDh.Columns["idperplan"].Visible = false;


                    dgvDh.Columns[0].Width = 250;
                    dgvDh.Columns[1].Width = 100;
                    dgvDh.Columns[2].Width = 150;
                    dgvDh.Columns[3].Width = 150;


                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }



            conexion.Close();
        }

        public void ui_listaFonPen(string idcia, string idperplan)
        {
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();
            string query = "select A.fechaini,A.fechafin,apvoltra,apvolemp,B.desfonpen,A.idfonpenper,A.idcia,A.idperplan,A.statefonpenper,A.idfonpen from fonpenper A left join fonpen B on A.idfonpen=B.idfonpen where idcia='" + idcia + "' and idperplan='" + idperplan + "' order by A.idfonpenper asc;";
            try
            {
                using (SqlDataAdapter myDataAdapter = new SqlDataAdapter(query, conexion))
                {
                    DataSet myDataSet = new DataSet();
                    myDataAdapter.Fill(myDataSet, "tblFonPen");
                    funciones.formatearDataGridView(dgvFonPen);

                    dgvFonPen.DataSource = myDataSet.Tables["tblFonPen"];
                    dgvFonPen.Columns[0].HeaderText = "Fecha Inicio / Reinicio";
                    dgvFonPen.Columns[1].HeaderText = "Fecha Fin";
                    dgvFonPen.Columns[2].HeaderText = "Ap.vol.Trabajador";
                    dgvFonPen.Columns[3].HeaderText = "Ap.vol.Empleador";
                    dgvFonPen.Columns[4].HeaderText = "Fondo de Pensiones";

                    dgvFonPen.Columns["idfonpenper"].Visible = false;
                    dgvFonPen.Columns["idcia"].Visible = false;
                    dgvFonPen.Columns["idperplan"].Visible = false;
                    dgvFonPen.Columns["statefonpenper"].Visible = false;
                    dgvFonPen.Columns["idfonpen"].Visible = false;


                    dgvFonPen.Columns[0].Width = 100;
                    dgvFonPen.Columns[1].Width = 100;
                    dgvFonPen.Columns[2].Width = 100;
                    dgvFonPen.Columns[3].Width = 100;
                    dgvFonPen.Columns[4].Width = 250;

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }



            conexion.Close();
        }

        public void ui_listaCenPer(string idcia, string idperplan)
        {
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();
            string query = "select A.idcencos,B.descencos,A.porcentaje,A.idperplan,A.idcia from cenper A left join cencos B on A.idcia=B.idcia and A.idcencos=B.idcencos where A.idcia='" + idcia + "' and A.idperplan='" + idperplan + "' order by A.idcencos asc;";
            try
            {
                using (SqlDataAdapter myDataAdapter = new SqlDataAdapter(query, conexion))
                {
                    DataSet myDataSet = new DataSet();
                    myDataAdapter.Fill(myDataSet, "tblCenPer");
                    funciones.formatearDataGridView(dgvCenPer);

                    dgvCenPer.DataSource = myDataSet.Tables["tblCenPer"];
                    dgvCenPer.Columns[0].HeaderText = "Código";
                    dgvCenPer.Columns[1].HeaderText = "Centro de Costo";
                    dgvCenPer.Columns[2].HeaderText = "Porcentaje";

                    dgvCenPer.Columns["idcia"].Visible = false;
                    dgvCenPer.Columns["idperplan"].Visible = false;


                    dgvCenPer.Columns[0].Width = 100;
                    dgvCenPer.Columns[1].Width = 300;
                    dgvCenPer.Columns[2].Width = 100;

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }



            conexion.Close();
        }

        private void btnGrabar_Click(object sender, EventArgs e)
        {
            if (txtOperacion.Text.Trim().Equals("EDITAR"))
            {
                PerLab perlab = new PerLab();
                MaesGen maesgen = new MaesGen();

                string operacion = txtOpePerLab.Text.Trim();
                string idperlab = txtCodigoPeriodo.Text;
                string idperplan = txtCodigoInterno.Text.Trim();
                string categoria = "1";
                string tipreg = "1";
                string fechaini = txtInicioPeriodo.Text;
                string fechafin = txtFinPeriodo.Text;
                string motivo = funciones.getValorComboBox(cmbMotivoFinPeriodo, 4);
                string eps = string.Empty;
                string valorValida = "G";

                if (UtileriasFechas.IsDate(txtInicioPeriodo.Text) == false && operacion == "AGREGAR" && valorValida == "G")
                {
                    valorValida = "B";
                    MessageBox.Show("Fecha de Inicio de Periodo no válida", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtInicioPeriodo.Focus();
                }

                if (UtileriasFechas.IsDate(txtFinPeriodo.Text) == false && operacion == "EDITAR" && valorValida == "G")
                {
                    valorValida = "B";
                    MessageBox.Show("Fecha de Fin de Periodo no válida", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtFinPeriodo.Focus();
                }


                if (cmbMotivoFinPeriodo.Text == string.Empty && valorValida == "G" && operacion == "EDITAR")
                {
                    valorValida = "B";
                    MessageBox.Show("No ha seleccionado Motivo de Fin de Periodo", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    cmbMotivoFinPeriodo.Focus();
                }

                if (cmbMotivoFinPeriodo.Text != string.Empty && valorValida == "G" && operacion == "EDITAR")
                {
                    string resultado = maesgen.verificaComboBoxMaesGen("016", cmbMotivoFinPeriodo.Text.Trim());
                    if (resultado.Trim() == string.Empty)
                    {
                        valorValida = "B";
                        MessageBox.Show("Dato incorrecto en Motivo de Fin de Periodo Laboral", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        cmbMotivoFinPeriodo.Focus();
                    }
                }

                if (valorValida.Equals("G"))
                {
                    perlab.actualizarPerLab(operacion, idperlab, idcia, idperplan, fechaini, fechafin, motivo);
                    ui_listaPeriodosLaborales(idcia, idperplan);
                    ui_limpiarPerLab();
                }
            }
            else
            {
                MessageBox.Show("Primero debe de grabar la información del Trabajador", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            MaesGen maesgen = new MaesGen();
            Int32 selectedCellCount = dgvPeriodosLaborales.GetCellCount(DataGridViewElementStates.Selected);
            if (selectedCellCount > 0)
            {
                string fechaini = dgvPeriodosLaborales.Rows[dgvPeriodosLaborales.SelectedCells[1].RowIndex].Cells[0].Value.ToString();
                string fechafin = dgvPeriodosLaborales.Rows[dgvPeriodosLaborales.SelectedCells[1].RowIndex].Cells[1].Value.ToString();
                string motivo = dgvPeriodosLaborales.Rows[dgvPeriodosLaborales.SelectedCells[1].RowIndex].Cells[7].Value.ToString();
                string idperlab = dgvPeriodosLaborales.Rows[dgvPeriodosLaborales.SelectedCells[1].RowIndex].Cells[3].Value.ToString();
                string idperplan = dgvPeriodosLaborales.Rows[dgvPeriodosLaborales.SelectedCells[1].RowIndex].Cells[4].Value.ToString();
                //string idcia = dgvPeriodosLaborales.Rows[dgvPeriodosLaborales.SelectedCells[1].RowIndex].Cells[5].Value.ToString();
                txtOpePerLab.Text = "EDITAR";
                txtCodigoPeriodo.Text = idperlab;
                txtInicioPeriodo.Text = fechaini;
                txtFinPeriodo.Text = fechafin;
                maesgen.consultaDetMaesGen("016", motivo, cmbMotivoFinPeriodo);
                txtInicioPeriodo.Enabled = false;
                txtFinPeriodo.Enabled = true;
                cmbMotivoFinPeriodo.Enabled = true;
                txtFinPeriodo.Focus();
            }
            else
            {
                MessageBox.Show("No ha seleccionado registro a Editar", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            string idperlab;
            string idcia;
            string idperplan;


            PerLab perlab = new PerLab();
            Int32 selectedCellCount = dgvPeriodosLaborales.GetCellCount(DataGridViewElementStates.Selected);
            if (selectedCellCount > Convert.ToInt32(0))
            {

                DialogResult resultado = MessageBox.Show("¿Desea eliminar el Periodo Laboral?",
                "Consulta Importante",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);
                if (resultado == DialogResult.Yes)
                {
                    idperlab = dgvPeriodosLaborales.Rows[dgvPeriodosLaborales.SelectedCells[1].RowIndex].Cells[3].Value.ToString();
                    idcia = dgvPeriodosLaborales.Rows[dgvPeriodosLaborales.SelectedCells[1].RowIndex].Cells[4].Value.ToString();
                    idperplan = dgvPeriodosLaborales.Rows[dgvPeriodosLaborales.SelectedCells[1].RowIndex].Cells[5].Value.ToString();
                    perlab.eliminarPerLab(idperlab, idcia, idperplan);
                    ui_listaPeriodosLaborales(idcia, idperplan);
                }


            }
            else
            {
                MessageBox.Show("No ha seleccionado registro a Eliminar", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void tabPageEX5_Click(object sender, EventArgs e)
        {

        }

        private void btnNuevoFonPen_Click(object sender, EventArgs e)
        {
            string squery;
            string idperplan = txtCodigoInterno.Text;

            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();

            squery = "SELECT * from fonpenper where statefonpenper='V' and idcia='" + @idcia + "' and idperplan='" + @idperplan + "'";
            try
            {

                SqlCommand myCommand_table = new SqlCommand(squery, conexion);
                DataTable dt_table = new DataTable();
                dt_table.Load(myCommand_table.ExecuteReader());
                myCommand_table.Dispose();

                if (dt_table.Rows.Count > Convert.ToInt16(0))
                {
                    MessageBox.Show("Para asignar otro Fondo de Pensiones debe de cerrar el primero", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                else
                {
                    ui_limpiarFonPen();
                    txtOpeFonPen.Text = "AGREGAR";
                    txtIniFonPen.Enabled = true;
                    txtAVT.Enabled = true;
                    txtAVE.Enabled = true;
                    txtFinFonPen.Enabled = false;
                    cmbFonPen.Enabled = true;
                    txtIniFonPen.Focus();
                }

            }
            catch (SqlException ex)
            {
                MessageBox.Show("A ocurrido un error en la consulta [" + ex.Message + "]", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);

            }

            conexion.Close();
        }

        private void btnGrabarFonPen_Click(object sender, EventArgs e)
        {
            if (txtOperacion.Text.Trim().Equals("EDITAR"))
            {
                try
                {
                    FonPenPer fonpenper = new FonPenPer();

                    string operacion = txtOpeFonPen.Text.Trim();
                    string idfonpenper = txtCodigoFonPen.Text;
                    string idperplan = txtCodigoInterno.Text.Trim();
                    string fechaini = txtIniFonPen.Text;
                    string fechafin = txtFinFonPen.Text;
                    float apvoltra = float.Parse(txtAVT.Text);
                    float apvolemp = float.Parse(txtAVE.Text);
                    string idfonpen = funciones.getValorComboBox(cmbFonPen, 3);
                    string valorValida = "G";

                    if (UtileriasFechas.IsDate(txtIniFonPen.Text) == false && operacion == "AGREGAR" && valorValida == "G")
                    {
                        valorValida = "B";
                        MessageBox.Show("Fecha de Inicio no válida", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txtIniFonPen.Focus();
                    }

                    if (UtileriasFechas.IsDate(txtFinFonPen.Text) == false && operacion == "FINALIZAR" && valorValida == "G")
                    {
                        valorValida = "B";
                        MessageBox.Show("Fecha de Fin no válida", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txtFinFonPen.Focus();
                    }

                    if (cmbFonPen.Text == string.Empty && valorValida == "G")
                    {
                        valorValida = "B";
                        MessageBox.Show("No ha seleccionado Fondo de Pensiones", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        cmbFonPen.Focus();
                    }

                    if (cmbFonPen.Text != string.Empty && valorValida == "G")
                    {
                        string squery = "SELECT idfonpen as clave,desfonpen as descripcion FROM fonpen WHERE idfonpen='" + @idfonpen + "';";
                        string resultado = funciones.verificaItemComboBox(squery, cmbFonPen);

                        if (resultado.Trim() == string.Empty)
                        {
                            valorValida = "B";
                            MessageBox.Show("Dato incorrecto en Fondo de Pensiones", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            cmbFonPen.Focus();
                        }
                    }

                    if (valorValida.Equals("G"))
                    {
                        fonpenper.actualizarFonPenPer(operacion, idfonpenper, idcia, idperplan, fechaini, fechafin, idfonpen, apvoltra, apvolemp);
                        ui_listaFonPen(idcia, idperplan);
                        ui_limpiarFonPen();
                    }
                }
                catch (FormatException ex)
                {
                    MessageBox.Show(ex.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Primero debe de grabar la información del Trabajador", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void btnEliminarFonPen_Click(object sender, EventArgs e)
        {
            string idfonpenper;
            string idcia;
            string idperplan;

            FonPenPer fonpenper = new FonPenPer();
            Int32 selectedCellCount = dgvFonPen.GetCellCount(DataGridViewElementStates.Selected);
            if (selectedCellCount > Convert.ToInt32(0))
            {

                DialogResult resultado = MessageBox.Show("¿Desea eliminar el Fondo de Pensiones?",
                "Consulta Importante",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);
                if (resultado == DialogResult.Yes)
                {
                    idfonpenper = dgvFonPen.Rows[dgvFonPen.SelectedCells[1].RowIndex].Cells[5].Value.ToString();
                    idcia = dgvFonPen.Rows[dgvFonPen.SelectedCells[1].RowIndex].Cells[6].Value.ToString();
                    idperplan = dgvFonPen.Rows[dgvFonPen.SelectedCells[1].RowIndex].Cells[7].Value.ToString();
                    fonpenper.eliminarFonPenPer(idfonpenper, idcia, idperplan);
                    ui_listaFonPen(idcia, idperplan);
                }


            }
            else
            {
                MessageBox.Show("No ha seleccionado registro a Eliminar", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void tabPageEX8_Click(object sender, EventArgs e)
        {

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

        private void radioButtonDomTra_CheckedChanged(object sender, EventArgs e)
        {
            tabPageEX10.Enabled = false;
            ui_limpiarDireccionDerechohabiente();
        }

        private void radioButtonOtroDom_CheckedChanged(object sender, EventArgs e)
        {
            tabPageEX10.Enabled = true;
        }

        private void txtApPatDh_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                e.Handled = true;
                txtApMatDh.Focus();
            }
        }

        private void txtApMatDh_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtApMatDh_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                e.Handled = true;
                txtNombresDh.Focus();
            }
        }

        private void txtNombresDh_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                e.Handled = true;
                cmbTipoDocumentoDh.Focus();
            }
        }

        private void txtTipoDocDh_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                e.Handled = true;
                txtFechaNacDh.Focus();

            }
        }

        private void cmbTipoDocumentoDh_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                if (cmbTipoDocumentoDh.Text != String.Empty)
                {
                    MaesGen maesgen = new MaesGen();
                    maesgen.validarDetMaesGen("002", cmbTipoDocumentoDh, cmbTipoDocumentoDh.Text);
                }
                e.Handled = true;
                txtNroDocDh.Focus();
            }
        }

        private void txtFechaNacDh_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                if (UtileriasFechas.IsDate(txtFechaNacDh.Text))
                {
                    e.Handled = true;
                    cmbSexoDh.Focus();
                }
                else
                {
                    MessageBox.Show("Fecha de Nacimiento no válida", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    e.Handled = true;
                    txtFechaNacDh.Focus();
                }
            }
        }

        private void cmbSexoDh_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                if (cmbSexoDh.Text != String.Empty)
                {
                    MaesGen maesgen = new MaesGen();
                    maesgen.validarDetMaesGen("019", cmbSexoDh, cmbSexoDh.Text);
                }

            }
        }

        private void cmbVinculoDh_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                if (cmbVinculoDh.Text != String.Empty)
                {
                    MaesGen maesgen = new MaesGen();
                    maesgen.validarDetMaesGen("020", cmbVinculoDh, cmbVinculoDh.Text);

                    if (funciones.getValorComboBox(cmbVinculoDh, 4).Equals("1"))
                    {
                        radioButtonSiDiscaDh.Enabled = true;
                        radioButtonNoDiscaDh.Enabled = true;
                        txtResDirDh.Enabled = true;

                    }
                    else
                    {
                        radioButtonSiDiscaDh.Checked = false;
                        radioButtonNoDiscaDh.Checked = true;
                        radioButtonSiDiscaDh.Enabled = false;
                        radioButtonNoDiscaDh.Enabled = false;
                        txtResDirDh.Clear();
                        txtResDirDh.Enabled = false;
                    }

                    if (funciones.getValorComboBox(cmbVinculoDh, 4).Equals("4"))
                    {
                        cmbDocPatDh.Enabled = true;
                        e.Handled = true;
                        cmbDocPatDh.Focus();

                    }
                    else
                    {
                        cmbDocPatDh.Text = "";
                        cmbDocPatDh.Enabled = false;
                        e.Handled = true;
                        cmbSituacionDh.Focus();
                    }



                }


            }
        }

        private void cmbDocPatDh_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                if (cmbDocPatDh.Text != String.Empty)
                {
                    MaesGen maesgen = new MaesGen();
                    maesgen.validarDetMaesGen("021", cmbDocPatDh, cmbDocPatDh.Text);
                }
                e.Handled = true;
                cmbSituacionDh.Focus();
            }
        }

        private void cmbSituacionDh_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                if (cmbSituacionDh.Text != String.Empty)
                {
                    MaesGen maesgen = new MaesGen();
                    maesgen.validarDetMaesGen("022", cmbSituacionDh, cmbSituacionDh.Text);
                    if (funciones.getValorComboBox(cmbSituacionDh, 4).Equals("10"))
                    {
                        cmbMotBajaDh.Text = string.Empty;
                        cmbMotBajaDh.Enabled = false;
                        txtBajaDh.Text = string.Empty;
                        txtBajaDh.Enabled = false;
                        e.Handled = true;
                        txtAltaDh.Focus();
                    }
                    else
                    {
                        cmbMotBajaDh.Text = string.Empty;
                        cmbMotBajaDh.Enabled = true;
                        txtBajaDh.Text = string.Empty;
                        txtBajaDh.Enabled = true;
                        e.Handled = true;
                        cmbMotBajaDh.Focus();
                    }
                }
            }
        }

        private void cmbVinculoDh_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void cmbVinculoDh_SelectedValueChanged(object sender, EventArgs e)
        {
            if (cmbVinculoDh.Text != String.Empty)
            {
                MaesGen maesgen = new MaesGen();
                maesgen.validarDetMaesGen("020", cmbVinculoDh, cmbVinculoDh.Text);

                if (funciones.getValorComboBox(cmbVinculoDh, 4).Equals("1"))
                {
                    radioButtonSiDiscaDh.Enabled = true;
                    radioButtonNoDiscaDh.Enabled = true;
                    txtResDirDh.Enabled = true;
                }
                else
                {
                    radioButtonSiDiscaDh.Checked = false;
                    radioButtonNoDiscaDh.Checked = true;
                    radioButtonSiDiscaDh.Enabled = false;
                    radioButtonNoDiscaDh.Enabled = false;
                    txtResDirDh.Clear();
                    txtResDirDh.Enabled = false;
                }

                if (funciones.getValorComboBox(cmbVinculoDh, 4).Equals("4"))
                {
                    cmbDocPatDh.Enabled = true;
                    cmbDocPatDh.Focus();
                }
                else
                {
                    cmbDocPatDh.Text = "";
                    cmbDocPatDh.Enabled = false;
                    cmbSituacionDh.Focus();
                }
            }
        }

        private void cmbSituacionDh_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void cmbSituacionDh_SelectedValueChanged(object sender, EventArgs e)
        {
            if (cmbSituacionDh.Text != String.Empty)
            {
                MaesGen maesgen = new MaesGen();
                maesgen.validarDetMaesGen("022", cmbSituacionDh, cmbSituacionDh.Text);
                if (funciones.getValorComboBox(cmbSituacionDh, 4).Equals("10"))
                {
                    cmbMotBajaDh.Text = string.Empty;
                    cmbMotBajaDh.Enabled = false;
                    txtBajaDh.Text = string.Empty;
                    txtBajaDh.Enabled = false;
                    txtAltaDh.Focus();
                }
                else
                {
                    cmbMotBajaDh.Text = string.Empty;
                    cmbMotBajaDh.Enabled = true;
                    txtBajaDh.Text = string.Empty;
                    txtBajaDh.Enabled = true;
                    cmbMotBajaDh.Focus();
                }
            }
        }

        private void cmbMotBajaDh_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                if (cmbMotBajaDh.Text != String.Empty)
                {
                    MaesGen maesgen = new MaesGen();
                    maesgen.validarDetMaesGen("023", cmbMotBajaDh, cmbMotBajaDh.Text);
                }
                e.Handled = true;
                txtBajaDh.Focus();
            }
        }

        private void txtAltaDh_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                if (UtileriasFechas.IsDate(txtAltaDh.Text))
                {
                    e.Handled = true;
                    cmbMotBajaDh.Focus();
                }
                else
                {
                    MessageBox.Show("Fecha de Alta no válida", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    e.Handled = true;
                    txtAltaDh.Focus();
                }
            }
        }

        private void txtBajaDh_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                if (UtileriasFechas.IsDate(txtBajaDh.Text))
                {
                    e.Handled = true;
                    radioButtonDomTra.Focus();
                }
                else
                {
                    MessageBox.Show("Fecha de Baja no válida", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    e.Handled = true;
                    txtBajaDh.Focus();
                }
            }
        }

        private void cmbTipoViaDh_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                if (cmbTipoViaDh.Text != String.Empty)
                {
                    MaesGen maesgen = new MaesGen();
                    maesgen.validarDetMaesGen("017", cmbTipoViaDh, cmbTipoViaDh.Text);
                }
                e.Handled = true;
                txtNombreViaDh.Focus();
            }
        }

        private void txtNombreViaDh_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                e.Handled = true;
                txtNumeroViaDh.Focus();
            }
        }

        private void txtNumeroViaDh_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                e.Handled = true;
                txtInteriorViaDh.Focus();
            }
        }

        private void txtInteriorViaDh_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                e.Handled = true;
                cmbTipoZonaDh.Focus();
            }
        }

        private void cmbTipoZonaDh_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                if (cmbTipoZonaDh.Text != String.Empty)
                {
                    MaesGen maesgen = new MaesGen();
                    maesgen.validarDetMaesGen("018", cmbTipoZonaDh, cmbTipoZonaDh.Text);
                }
                e.Handled = true;
                txtNombreZonaDh.Focus();
            }
        }

        private void txtNombreZonaDh_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                e.Handled = true;
                txtReferenciasZonaDh.Focus();
            }
        }

        private void txtReferenciasZonaDh_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                e.Handled = true;
                btnUbigeoDh.Focus();
            }
        }

        internal void ui_limpiaDerechohabiente()
        {
            tabControlDh.SelectTab(0);
            txtOpeDh.Text = string.Empty;
            txtValidDh.Text = string.Empty;
            txtCodigoInternoDh.Text = string.Empty;
            txtApPatDh.Text = string.Empty;
            txtApMatDh.Text = string.Empty;
            txtNombresDh.Text = string.Empty;
            cmbTipoDocumentoDh.Text = string.Empty;
            txtNroDocDh.Text = string.Empty;
            txtFechaNacDh.Text = string.Empty;
            cmbSexoDh.Text = string.Empty;
            cmbVinculoDh.Text = string.Empty;
            cmbDocPatDh.Text = string.Empty;
            cmbSituacionDh.Text = string.Empty;
            txtAltaDh.Text = string.Empty;
            cmbMotBajaDh.Text = string.Empty;
            txtBajaDh.Text = string.Empty;
            radioButtonDomTra.Checked = true;
            radioButtonOtroDom.Checked = false;
            radioButtonSiDiscaDh.Checked = false;
            radioButtonNoDiscaDh.Checked = true;
            txtResDirDh.Text = string.Empty;
            ui_limpiarDireccionDerechohabiente();
        }

        public void ui_habilitaDerechohabiente(bool estado)
        {
            txtApPatDh.Enabled = estado;
            txtApMatDh.Enabled = estado;
            txtNombresDh.Enabled = estado;
            cmbTipoDocumentoDh.Enabled = estado;
            txtNroDocDh.Enabled = estado;
            txtFechaNacDh.Enabled = estado;
            cmbSexoDh.Enabled = estado;
            cmbVinculoDh.Enabled = estado;
            cmbSituacionDh.Enabled = estado;
            txtAltaDh.Enabled = estado;
            radioButtonDomTra.Enabled = estado;
            radioButtonOtroDom.Enabled = estado;
            cmbTipoViaDh.Enabled = estado;
            txtNombreViaDh.Enabled = estado;
            txtNumeroViaDh.Enabled = estado;
            txtInteriorViaDh.Enabled = estado;
            cmbTipoZonaDh.Enabled = estado;
            txtNombreZonaDh.Enabled = estado;
            txtReferenciasZonaDh.Enabled = estado;
            btnUbigeoDh.Enabled = estado;
            radioButtonNoDiscaDh.Enabled = estado;
            radioButtonSiDiscaDh.Enabled = estado;
            txtResDirDh.Enabled = estado;
        }

        private void btnNuevoDh_Click(object sender, EventArgs e)
        {
            ui_limpiaDerechohabiente();
            txtOpeDh.Text = "AGREGAR";
            ui_habilitaDerechohabiente(true);
            tabControlDh.SelectTab(0);
            txtApPatDh.Focus();
        }

        private void btnGrabarDh_Click(object sender, EventArgs e)
        {

            if (txtOperacion.Text.Trim().Equals("EDITAR"))
            {
                ui_validarDerechohabiente();

                if (txtValidDh.Text.Equals("G"))
                {
                    GlobalVariables variables = new GlobalVariables();

                    string operacion = txtOpeDh.Text.Trim();
                    string idperplan = txtCodigoInterno.Text.Trim();
                    string idderhab = txtCodigoInternoDh.Text.Trim();
                    string apepat = txtApPatDh.Text.Trim();
                    string apemat = txtApMatDh.Text.Trim();
                    string nombres = txtNombresDh.Text.Trim();
                    string fecnac = txtFechaNacDh.Text.Trim();
                    string tipdoc = funciones.getValorComboBox(cmbTipoDocumentoDh, 4);
                    string nrodoc = txtNroDocDh.Text.Trim();
                    string sexo = funciones.getValorComboBox(cmbSexoDh, 4);
                    string vinfam = funciones.getValorComboBox(cmbVinculoDh, 4);
                    string docpat = funciones.getValorComboBox(cmbDocPatDh, 4);
                    string sitdh = funciones.getValorComboBox(cmbSituacionDh, 4);
                    string fecalta = txtAltaDh.Text;
                    string fecbaja = txtBajaDh.Text;
                    string motbaja = funciones.getValorComboBox(cmbMotBajaDh, 4);
                    string domtra;
                    string otrodom;
                    string discapa;
                    string rddisca;

                    if (radioButtonSiDiscaDh.Checked)
                    {
                        discapa = "1";
                        rddisca = txtResDirDh.Text;
                    }
                    else
                    {
                        discapa = "0";
                        rddisca = "";
                    }

                    if (radioButtonDomTra.Checked)
                        domtra = "1";
                    else
                        domtra = "0";

                    if (radioButtonOtroDom.Checked)
                        otrodom = "1";
                    else
                        otrodom = "0";

                    string tipvia = funciones.getValorComboBox(cmbTipoViaDh, 4);
                    string nomvia = txtNombreViaDh.Text.Trim();
                    string nrovia = txtNumeroViaDh.Text.Trim();
                    string intvia = txtInteriorViaDh.Text.Trim();
                    string tipzona = funciones.getValorComboBox(cmbTipoZonaDh, 4);
                    string nomzona = txtNombreZonaDh.Text.Trim();
                    string refzona = txtReferenciasZonaDh.Text.Trim();
                    string ubigeo = txtCodigoUbigeoDh.Text.Trim();
                    string dscubigeo = txtDscUbigeoDh.Text.Trim();

                    try
                    {
                        DerHab derhab = new DerHab();
                        derhab.actualizaDerHab(operacion, idperplan, idcia, idderhab, apepat, apemat, nombres, fecnac, tipdoc, nrodoc, sexo,
                        vinfam, docpat, sitdh, fecalta, motbaja, fecbaja, domtra, otrodom, tipvia, nomvia, nrovia, intvia, tipzona, nomzona,
                        refzona, ubigeo, dscubigeo, discapa, rddisca);
                        ui_listaDerechohabientes(idcia, idperplan);
                        ui_limpiaDerechohabiente();
                        ui_habilitaDerechohabiente(false);

                    }
                    catch (FormatException ex)
                    {
                        MessageBox.Show(ex.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Primero debe de grabar la información del Trabajador", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnEditaDh_Click(object sender, EventArgs e)
        {
            string idcia;
            string idperplan;
            string idderhab;

            Int32 selectedCellCount = dgvDh.GetCellCount(DataGridViewElementStates.Selected);
            if (selectedCellCount > Convert.ToInt32(0))
            {

                idcia = dgvDh.Rows[dgvDh.SelectedCells[1].RowIndex].Cells[4].Value.ToString();
                idperplan = dgvDh.Rows[dgvDh.SelectedCells[1].RowIndex].Cells[5].Value.ToString();
                idderhab = dgvDh.Rows[dgvDh.SelectedCells[1].RowIndex].Cells[6].Value.ToString();
                ui_limpiaDerechohabiente();
                ui_habilitaDerechohabiente(true);
                ui_loadDerhab(idcia, idperplan, idderhab);
                tabControlDh.SelectTab(0);
                txtApPat.Focus();
            }
            else
            {
                MessageBox.Show("No ha seleccionado registro a Editar", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void btnEliminarDh_Click(object sender, EventArgs e)
        {
            string idcia;
            string idperplan;
            string idderhab;
            string nomderhab;

            Int32 selectedCellCount = dgvDh.GetCellCount(DataGridViewElementStates.Selected);
            if (selectedCellCount > Convert.ToInt32(0))
            {

                idcia = dgvDh.Rows[dgvDh.SelectedCells[1].RowIndex].Cells[4].Value.ToString();
                idperplan = dgvDh.Rows[dgvDh.SelectedCells[1].RowIndex].Cells[5].Value.ToString();
                idderhab = dgvDh.Rows[dgvDh.SelectedCells[1].RowIndex].Cells[6].Value.ToString();
                nomderhab = dgvDh.Rows[dgvDh.SelectedCells[1].RowIndex].Cells[0].Value.ToString();

                DialogResult resultado = MessageBox.Show("¿Desea eliminar el Derechohabiente " + nomderhab + "?",
                "Consulta Importante",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);
                if (resultado == DialogResult.Yes)
                {
                    DerHab derhab = new DerHab();
                    derhab.eliminarDerHab(idcia, idperplan, idderhab);
                    ui_listaDerechohabientes(idcia, idperplan);
                }
            }
            else
            {
                MessageBox.Show("No ha seleccionado registro a Editar", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {

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

        private void btnUbigeo_Click(object sender, EventArgs e)
        {
            _TextBoxUbigeo = txtCodigoUbigeo;
            _TextBoxDscUbigeo = txtDscUbigeo;
            ui_ubigeo ui_ubigeo = new ui_ubigeo();
            ui_ubigeo._FormPadre = this;
            ui_ubigeo._clasePadre = "ui_updpersonal";

            ui_ubigeo.ui_nuevaSeleccion();

            if (ui_ubigeo.ShowDialog(this) == DialogResult.OK)
            {
                btnUbigeo.Focus();
            }
            else
            {
                btnUbigeo.Focus();
            }
            ui_ubigeo.Dispose();
        }

        private void btnUbigeoDh_Click(object sender, EventArgs e)
        {
            _TextBoxUbigeo = txtCodigoUbigeoDh;
            _TextBoxDscUbigeo = txtDscUbigeoDh;
            ui_ubigeo ui_ubigeo = new ui_ubigeo();
            ui_ubigeo._FormPadre = this;
            ui_ubigeo._clasePadre = "ui_updpersonal";

            ui_ubigeo.ui_nuevaSeleccion();

            if (ui_ubigeo.ShowDialog(this) == DialogResult.OK)
            {
                btnUbigeoDh.Focus();
            }
            else
            {
                btnUbigeoDh.Focus();
            }
            ui_ubigeo.Dispose();
        }

        private void cmbEntFinRem_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnFinFonPen_Click(object sender, EventArgs e)
        {
            string idfonpenper;
            string idcia;
            string idperplan;
            string fechaini;
            string fechafin;
            string idfonpen;
            string apvoltra;
            string apvolemp;
            string squery;
            Int32 selectedCellCount = dgvFonPen.GetCellCount(DataGridViewElementStates.Selected);
            if (selectedCellCount > Convert.ToInt32(0))
            {
                fechaini = dgvFonPen.Rows[dgvFonPen.SelectedCells[1].RowIndex].Cells[0].Value.ToString();
                fechafin = dgvFonPen.Rows[dgvFonPen.SelectedCells[1].RowIndex].Cells[1].Value.ToString();
                apvoltra = dgvFonPen.Rows[dgvFonPen.SelectedCells[1].RowIndex].Cells[2].Value.ToString();
                apvolemp = dgvFonPen.Rows[dgvFonPen.SelectedCells[1].RowIndex].Cells[3].Value.ToString();
                idfonpen = dgvFonPen.Rows[dgvFonPen.SelectedCells[1].RowIndex].Cells[9].Value.ToString();
                idfonpenper = dgvFonPen.Rows[dgvFonPen.SelectedCells[1].RowIndex].Cells[5].Value.ToString();
                idperplan = dgvFonPen.Rows[dgvFonPen.SelectedCells[1].RowIndex].Cells[7].Value.ToString();
                idcia = dgvFonPen.Rows[dgvFonPen.SelectedCells[1].RowIndex].Cells[6].Value.ToString();
                txtOpeFonPen.Text = "FINALIZAR";
                txtCodigoFonPen.Text = idfonpenper;
                txtIniFonPen.Text = fechaini;
                txtFinFonPen.Text = fechafin;
                txtAVT.Text = apvoltra;
                txtAVE.Text = apvolemp;
                squery = "Select idfonpen as clave,desfonpen as descripcion from fonpen where idfonpen='" + idfonpen + "';";
                funciones.consultaComboBox(squery, cmbFonPen);
                txtIniFonPen.Enabled = false;
                txtAVE.Enabled = false;
                txtAVT.Enabled = false;
                txtFinFonPen.Enabled = true;
                cmbFonPen.Enabled = false;
                txtFinFonPen.Focus();
            }
            else
            {
                MessageBox.Show("No ha seleccionado registro a Editar", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void btnEditarFonPen_Click(object sender, EventArgs e)
        {
            string idfonpenper;
            string idcia;
            string idperplan;
            string fechaini;
            string fechafin;
            string idfonpen;
            string squery;
            string apvoltra;
            string apvolemp;
            Int32 selectedCellCount = dgvFonPen.GetCellCount(DataGridViewElementStates.Selected);
            if (selectedCellCount > Convert.ToInt32(0))
            {
                fechaini = dgvFonPen.Rows[dgvFonPen.SelectedCells[1].RowIndex].Cells[0].Value.ToString();
                fechafin = dgvFonPen.Rows[dgvFonPen.SelectedCells[1].RowIndex].Cells[1].Value.ToString();
                apvoltra = dgvFonPen.Rows[dgvFonPen.SelectedCells[1].RowIndex].Cells[2].Value.ToString();
                apvolemp = dgvFonPen.Rows[dgvFonPen.SelectedCells[1].RowIndex].Cells[3].Value.ToString();
                idfonpen = dgvFonPen.Rows[dgvFonPen.SelectedCells[1].RowIndex].Cells[9].Value.ToString();
                idfonpenper = dgvFonPen.Rows[dgvFonPen.SelectedCells[1].RowIndex].Cells[5].Value.ToString();
                idperplan = dgvFonPen.Rows[dgvFonPen.SelectedCells[1].RowIndex].Cells[7].Value.ToString();
                idcia = dgvFonPen.Rows[dgvFonPen.SelectedCells[1].RowIndex].Cells[6].Value.ToString();
                txtOpeFonPen.Text = "EDITAR";
                txtCodigoFonPen.Text = idfonpenper;
                txtIniFonPen.Text = fechaini;
                txtFinFonPen.Text = fechafin;
                txtAVT.Text = apvoltra;
                txtAVE.Text = apvolemp;
                squery = "Select idfonpen as clave,desfonpen as descripcion from fonpen where idfonpen='" + idfonpen + "';";
                funciones.consultaComboBox(squery, cmbFonPen);
                txtIniFonPen.Enabled = true;
                txtAVT.Enabled = true;
                txtAVE.Enabled = true;
                txtFinFonPen.Enabled = false;
                cmbFonPen.Enabled = false;
                txtIniFonPen.Focus();
            }
            else
            {
                MessageBox.Show("No ha seleccionado registro a Editar", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void txtAVT_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                try
                {
                    float numero = float.Parse(txtAVT.Text);
                    e.Handled = true;
                    txtAVE.Focus();
                }
                catch (FormatException exc)
                {
                    MessageBox.Show(exc.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    txtAVT.Clear();
                    txtAVT.Focus();
                }
            }
        }

        private void txtAVE_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                try
                {
                    float numero = float.Parse(txtAVE.Text);
                    e.Handled = true;
                    txtFinFonPen.Focus();
                }
                catch (FormatException excep)
                {
                    MessageBox.Show(excep.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    txtAVE.Clear();
                    txtAVE.Focus();
                }
            }
        }

        private void tabPageEX1_Click(object sender, EventArgs e)
        {

        }

        private void btnOcupacion_Click(object sender, EventArgs e)
        {
            string cadenaBusqueda = string.Empty;
            FiltrosMaestros filtros = new FiltrosMaestros();
            this._TextBoxActivo = txtOcupacion;
            filtros.filtrarTgRpts("ui_updpersonal", this, txtOcupacion, "R4", "");
        }

        private void radioButtonNoCR_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void radioButtonNOEps_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void radioButtonSIEps_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void radioButtonNoEmp_CheckedChanged(object sender, EventArgs e)
        {
            GlobalVariables variables = new GlobalVariables();
            string ruccia = variables.getValorRucCia();

            string squery = "SELECT ruccia as clave,descia as descripcion FROM ciafile WHERE idcia='" + @idcia + "';";
            funciones.listaComboBox(squery, cmbEmpleador, "");
            cmbEmpleador.Enabled = false;

            string squery_esta = "SELECT idestane as clave,desestane as descripcion FROM estane WHERE codemp='" + @ruccia + "' and idciafile='" + @idcia + "' order by idestane asc;";
            funciones.listaComboBox(squery_esta, cmbEstablecimiento, "");
            cmbEstablecimiento.Focus();
        }

        private void radioButtonSiEmp_CheckedChanged(object sender, EventArgs e)
        {
            string squery = "SELECT rucemp as clave,razonemp as descripcion FROM emplea WHERE idciafile='" + @idcia + "' order by rucemp asc;";
            funciones.listaComboBox(squery, cmbEmpleador, "");
            cmbEmpleador.Enabled = true;

            string ruccia = funciones.getValorComboBox(cmbEmpleador, 11);
            string squery_esta = "SELECT idestane as clave,desestane as descripcion FROM estane WHERE codemp='" + @ruccia + "' and idciafile='" + @idcia + "' order by idestane asc;";
            funciones.listaComboBox(squery_esta, cmbEstablecimiento, "");
            cmbEmpleador.Focus();
        }

        private void cmbEmpleador_SelectedIndexChanged(object sender, EventArgs e)
        {
            string ruccia = funciones.getValorComboBox(cmbEmpleador, 11);
            string squery_esta = "SELECT idestane as clave,desestane as descripcion FROM estane WHERE codemp='" + @ruccia + "' and idciafile='" + @idcia + "' order by idestane asc;";
            funciones.listaComboBox(squery_esta, cmbEstablecimiento, "");
            cmbEstablecimiento.Focus();
        }

        private void radioButtonNOEps_Click(object sender, EventArgs e)
        {
            cmbEps.Text = string.Empty;
            cmbEps.Enabled = false;
        }

        private void radioButtonSIEps_Click(object sender, EventArgs e)
        {
            cmbEps.Enabled = true;
            cmbEps.Focus();
        }

        private void cmbTipoPlanilla_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                if (cmbTipoPlanilla.Text != String.Empty)
                {
                    string idtipoplan = funciones.getValorComboBox(cmbTipoPlanilla, 3);

                    string squery = "SELECT idtipoplan as clave,destipoplan as descripcion FROM tipoplan WHERE idtipoplan='" + @idtipoplan + "' and idtipoplan in (SELECT  idtipoplan FROM reglabcia WHERE idcia='" + @idcia + "');";
                    funciones.validarCombobox(squery, cmbTipoPlanilla);
                }

                e.Handled = true;
                cmbTipoContratoTrabajo.Focus();
            }
        }

        private void txtCuspp_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnCargar_Click(object sender, EventArgs e)
        {
            OpenFileDialog Abrir = new OpenFileDialog();
            Abrir.Title = "Seleccionar imagen";
            Abrir.Filter = "JPG(*.jpg)|*.jpg|PNG(*.png)|*.png|GIF(*.gif)|*.gif|Todos(*.Jpg, *.Png, *.Gif, *.Tiff, *.Jpeg, *.Bmp)|*.Jpg; *.Png; *.Gif; *.Tiff; *.Jpeg; *.Bmp";
            Abrir.FilterIndex = 4;
            Abrir.RestoreDirectory = true;

            if (Abrir.ShowDialog() == DialogResult.OK)
            {
                string Dir = Abrir.FileName;
                Bitmap Picture = new Bitmap(Dir);
                pictureBoxImg.SizeMode = PictureBoxSizeMode.StretchImage;
                pictureBoxImg.Image = (Image)Picture;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (txtOperacion.Text.Trim().Equals("EDITAR"))
            {
                try
                {
                    CenPer cenper = new CenPer();
                    string idcencos = funciones.getValorComboBox(cmbCentroCosto, 2);
                    string idperplan = txtCodigoInterno.Text.Trim();
                    float porcentaje = float.Parse(txtPorcentaje.Text);
                    string valorValida = "G";

                    if (cmbCentroCosto.Text == string.Empty && valorValida == "G")
                    {
                        valorValida = "B";
                        MessageBox.Show("No ha seleccionado Centro de Costo", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        cmbCentroCosto.Focus();
                    }

                    if (cmbCentroCosto.Text != string.Empty && valorValida == "G")
                    {
                        string squery = "SELECT idcencos as clave,descencos as descripcion FROM cencos WHERE idcencos='" + @idcencos + "' and idcia='" + @idcia + "';";
                        string resultado = funciones.verificaItemComboBox(squery, cmbCentroCosto);

                        if (resultado.Trim() == string.Empty)
                        {
                            valorValida = "B";
                            MessageBox.Show("Dato incorrecto en Centro de Costo", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            cmbCentroCosto.Focus();
                        }
                    }

                    if (valorValida.Equals("G"))
                    {
                        cenper.setCenPer(idperplan, idcia, idcencos, porcentaje);
                        cenper.actualizarCenPer();
                        ui_listaCenPer(idcia, idperplan);
                        ui_limpiarCenPer();
                    }
                }
                catch (FormatException ex)
                {
                    MessageBox.Show(ex.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Primero debe de grabar la información del Trabajador", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string idcencos;
            string idcia;
            string idperplan;
            string descencos;

            CenPer cenper = new CenPer();
            Int32 selectedCellCount = dgvCenPer.GetCellCount(DataGridViewElementStates.Selected);
            if (selectedCellCount > Convert.ToInt32(0))
            {
                idcencos = dgvCenPer.Rows[dgvCenPer.SelectedCells[1].RowIndex].Cells[0].Value.ToString();
                descencos = dgvCenPer.Rows[dgvCenPer.SelectedCells[1].RowIndex].Cells[1].Value.ToString();
                idperplan = dgvCenPer.Rows[dgvCenPer.SelectedCells[1].RowIndex].Cells[3].Value.ToString();
                idcia = dgvCenPer.Rows[dgvCenPer.SelectedCells[1].RowIndex].Cells[4].Value.ToString();

                DialogResult resultado = MessageBox.Show("¿Desea eliminar el porcentaje asignado al Centro de Costo '" + @descencos + "'?",
                "Consulta Importante",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);
                if (resultado == DialogResult.Yes)
                {
                    cenper.eliminarCenPer(idperplan, idcia, idcencos);
                    ui_listaCenPer(idcia, idperplan);
                }


            }
            else
            {
                MessageBox.Show("No ha seleccionado registro a Eliminar", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void tabPageEX12_Click(object sender, EventArgs e)
        {

        }

        private void tabControlPer_Click(object sender, EventArgs e)
        {

        }

        private void tabPageEX2_Click(object sender, EventArgs e)
        {

        }

        private void radioButtonSiDiscaDh_CheckedChanged(object sender, EventArgs e)
        {
            txtResDirDh.Clear();
            txtResDirDh.Enabled = true;
            txtResDirDh.Focus();
        }

        private void radioButtonNoDiscaDh_CheckedChanged(object sender, EventArgs e)
        {
            txtResDirDh.Clear();
            txtResDirDh.Enabled = false;
        }

        private void maskedTextBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                if (UtileriasFechas.IsDate(txtFecRegPen.Text))
                {
                    e.Handled = true;
                    txtCuspp.Focus();

                }
                else
                {
                    MessageBox.Show("Fecha de Inscripción no válida", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    e.Handled = true;
                    txtFecRegPen.Focus();
                }
            }
        }

        private void cmbConve_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void cmbConve_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                if (cmbConve.Text != String.Empty)
                {
                    MaesGen maesgen = new MaesGen();
                    maesgen.validarDetMaesGen("036", cmbConve, cmbConve.Text);
                }
                e.Handled = true;
            }
        }

        private void cmbCodigosNacionales_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                if (cmbCodigosNacionales.Text != String.Empty)
                {
                    MaesPdt maespdt = new MaesPdt();
                    maespdt.validarDetMaesPdt("29", cmbCodigosNacionales, cmbCodigosNacionales.Text);
                }
                e.Handled = true;
                txtTelFijo.Focus();
            }
        }

        private void cmbPaisEmisor_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                if (cmbPaisEmisor.Text != String.Empty)
                {
                    MaesPdt maespdt = new MaesPdt();
                    maespdt.validarDetMaesPdt("26", cmbPaisEmisor, cmbPaisEmisor.Text);
                }
                e.Handled = true;
                cmbCodigosNacionales.Focus();
            }
        }

        private void txtDeparVia_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                e.Handled = true;
                txtInteriorVia.Focus();


            }
        }

        private void txtEtapaVia_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                e.Handled = true;
                cmbTipoZona.Focus();

            }
        }

        private void txtManzanaVia_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                e.Handled = true;
                txtLoteVia.Focus();

            }
        }

        private void txtLoteVia_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                e.Handled = true;
                txtKmVia.Focus();

            }
        }

        private void txtKmVia_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                e.Handled = true;
                txtBlockVia.Focus();

            }
        }

        private void txtBlockVia_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                e.Handled = true;
                txtEtapaVia.Focus();

            }
        }

        private void cmbTipoDocumento_SelectedIndexChanged(object sender, EventArgs e)
        {
            Funciones fn = new Funciones();
            if (fn.getValorComboBox(cmbTipoDocumento, 4).Trim().Equals("07"))
            {
                cmbPaisEmisor.Enabled = true;
            }
            else
            {
                cmbPaisEmisor.Enabled = false;
                cmbPaisEmisor.Text = "";
            }
        }

        private void cmbRegimenLab_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                if (cmbRegimenLab.Text != String.Empty)
                {
                    MaesGen maesgen = new MaesGen();
                    maesgen.validarDetMaesGen("038", cmbRegimenLab, cmbRegimenLab.Text);
                }
                e.Handled = true;
                cmbTipoTrabajador.Focus();
            }
        }

        private void cmbTipoTrabajador_SelectedIndexChanged(object sender, EventArgs e)
        {
            Funciones fn = new Funciones();
            string tipotrab = fn.getValorComboBox(cmbTipoTrabajador, 4);
            if (tipotrab.Equals("67"))
            {
                txtRuc.Enabled = true;
            }
            else
            {
                txtRuc.Clear();
                txtRuc.Enabled = false;
            }
        }

        private void groupBox7_Enter(object sender, EventArgs e)
        {

        }
    }
}