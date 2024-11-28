using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Configuration;
using System.Data.SqlClient;
using System.Collections;
using System.IO;

namespace CaniaBrava
{
    public partial class ui_emiper : ui_form
    {
        string _codcia;

        public ui_emiper()
        {
            InitializeComponent();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ui_emiper_Load(object sender, EventArgs e)
        {
            Funciones funciones = new Funciones();
            GlobalVariables variables = new GlobalVariables();
            this._codcia = variables.getValorCia();
            string idcia = this._codcia;
            string query = "SELECT ruccia as clave,descia as descripcion FROM ciafile WHERE idcia='" + @variables.getValorCia() + "';";
            funciones.listaComboBox(query, cmbEmpleador, "");
            cmbOrdena.Text = "CODIGO INTERNO";
        }

        private void ui_listapersonal()
        {
            try
            {
                Funciones funciones = new Funciones();
                string codcia = this._codcia;
                string rucemp = funciones.getValorComboBox(cmbEmpleador, 11);
                string estane = funciones.getValorComboBox(cmbEstablecimiento, 4);

                string cadenaEstane;

                if (estane.Equals("X"))
                {
                    cadenaEstane = string.Empty;
                }
                else
                {
                    cadenaEstane = " and A.estane='" + @estane + "' ";
                }

                string lineatitulo1 = string.Empty;
                string lineatitulo2 = string.Empty;
                string lineatitulo3 = string.Empty;
                string strItem = string.Empty;
                string strPrinter = string.Empty;
                string jorremu;

                DataTable dtper = new DataTable();
                string query = " SELECT A.idperplan,A.apepat,A.apemat,A.nombres,";
                query = query + " CONCAT(C.Parm1maesgen,' ',A.nrodoc) as DocIdentidad,A.fecnac,D.desmaesgen as Sexo,";
                query = query + " A.telfijo,A.celular,A.rpm,E.desmaesgen as Nacionalidad,F.desmaesgen as EstadoCivil,";
                query = query + " CONCAT(G.desmaesgen,' ',A.nrolic) as LicenciaConducir,";
                query = query + " CONCAT(H.desmaesgen,' ',A.nomvia,' ',A.nrovia,' ',A.intvia,' ',I.desmaesgen,' ',A.nomzona) as Direccion, ";
                query = query + " J.desmaesgen as TipoTrabajador,K.desmaesgen as NivelEducativo,";
                query = query + " M.desmaesgen as Seccion, N.deslabper as ocupacion, ";
                query = query + " O.desmaesgen as RegimenPensionario,A.fecregpen,A.cuspp,";
                query = query + " P.desmaesgen as TipoContrato,Q.desmaesgen as TipoPago,R.desmaesgen as PeriodicidadIngreso,";
                query = query + " S.desmaesgen as SituacionEspecial,CONCAT(T.desmaesgen,' ',A.nroctarem,' ',A.monrem,' ',U.desmaesgen) as CtaRemuneracion, ";
                query = query + " CONCAT(V.desmaesgen,' ',A.nroctacts,' ',A.moncts,' ',W.desmaesgen) as CtaCts, ";
                query = query + " Y.fechaini as FechaInicio,Y.fechafin as FechaCese,Z.rp_cdescri as OcupacionPdt0601, ";
                query = query + " A1.desestane as Establecimiento,A2.desmaesgen Eps,A3.importe as Jornal_Remu ";
                query = query + " FROM perplan A ";
                query = query + " left join maesgen C on C.idmaesgen='002' and A.tipdoc=C.clavemaesgen ";
                query = query + " left join maesgen D on D.idmaesgen='019' and A.sexo=D.clavemaesgen ";
                query = query + " left join maesgen E on E.idmaesgen='003' and A.nacion=E.clavemaesgen ";
                query = query + " left join maesgen F on F.idmaesgen='001' and A.estcivil=F.clavemaesgen ";
                query = query + " left join maesgen G on G.idmaesgen='004' and A.catlic=G.clavemaesgen ";
                query = query + " left join maesgen H on H.idmaesgen='017' and A.tipvia=H.clavemaesgen ";
                query = query + " left join maesgen I on I.idmaesgen='018' and A.tipzona=I.clavemaesgen ";
                query = query + " left join maesgen J on J.idmaesgen='005' and A.tipotrab=J.clavemaesgen ";
                query = query + " left join maesgen K on K.idmaesgen='006' and A.nivedu=K.clavemaesgen ";
                query = query + " left join maesgen M on M.idmaesgen='008' and A.seccion=M.clavemaesgen ";
                query = query + " left join labper N on A.idcia=N.idcia and A.idlabper=N.idlabper and A.idtipoper=N.idtipoper ";
                query = query + " left join maesgen O on O.idmaesgen='009' and A.regpen=O.clavemaesgen ";
                query = query + " left join maesgen P on P.idmaesgen='010' and A.contrab=P.clavemaesgen ";
                query = query + " left join maesgen Q on Q.idmaesgen='013' and A.tippag=Q.clavemaesgen ";
                query = query + " left join maesgen R on R.idmaesgen='011' and A.pering=R.clavemaesgen ";
                query = query + " left join maesgen S on S.idmaesgen='012' and A.sitesp=S.clavemaesgen ";
                query = query + " left join maesgen T on T.idmaesgen='007' and A.entfinrem=T.clavemaesgen ";
                query = query + " left join maesgen U on U.idmaesgen='014' and A.tipctarem=U.clavemaesgen ";
                query = query + " left join maesgen V on V.idmaesgen='007' and A.entfincts=V.clavemaesgen ";
                query = query + " left join maesgen W on W.idmaesgen='014' and A.tipctacts=W.clavemaesgen ";
                query = query + " left join view_perlab X on A.idcia=X.idcia and A.idperplan=X.idperplan ";
                query = query + " left join perlab Y on X.idcia=Y.idcia and X.idperplan=Y.idperplan and X.idperlab=Y.idperlab";
                query = query + " left join tgrpts Z on Z.rp_cindice='R4' and A.ocurpts=Z.rp_ccodigo ";
                query = query + " left join estane A1 on A.idcia=A1.idciafile and A.estane=A1.idestane ";
                query = query + " left join maesgen A2 on A2.idmaesgen='028' and A.eps=A2.clavemaesgen ";
                query = query + " left join remu A3 on A.idcia=A3.idcia and A.idperplan=A3.idperplan and A3.state='V' ";
                query = query + " WHERE A.idcia='" + @codcia + "' and A.rucemp='" + @rucemp + "' " + cadenaEstane;
                if (cmbOrdena.Text.Trim().Equals("CODIGO INTERNO"))
                {
                    query = query + " ORDER BY A.idperplan asc ";
                }
                else
                {
                    query = query + " ORDER BY A.apepat asc,A.apemat asc,A.nombres asc ";
                }

                SqlConnection conexion = new SqlConnection();
                conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
                conexion.Open();
                SqlDataAdapter da = new SqlDataAdapter();


                da.SelectCommand = new SqlCommand(query, conexion);
                da.Fill(dtper);

                if (dtper.Rows.Count > 0)
                {
                    CiaFile ciafile = new CiaFile();
                    strPrinter = ciafile.ui_getDatosCiaFile(codcia, "DESCRIPCION") + "\n";
                    strPrinter = strPrinter + ciafile.ui_getDatosCiaFile(codcia, "RUC") + "\n";
                    strPrinter = strPrinter + "                                        R E P O R T E    D E   P E R S O N A L                                      " + "\n\n";

                    if (chkEnumera.Checked)
                    {
                        lineatitulo1 = lineatitulo1 + "----" + funciones.replicateCadena(" ", 1);
                        lineatitulo2 = lineatitulo2 + "Nro." + funciones.replicateCadena(" ", 1);
                        lineatitulo3 = lineatitulo3 + "----" + funciones.replicateCadena(" ", 1);
                    }

                    foreach (string variable in lstDestino.Items)
                    {

                        if (variable.Equals("CODIGO INTERNO"))
                        {
                            lineatitulo1 = lineatitulo1 + "----" + funciones.replicateCadena(" ", 1);
                            lineatitulo2 = lineatitulo2 + "COD." + funciones.replicateCadena(" ", 1);
                            lineatitulo3 = lineatitulo3 + "----" + funciones.replicateCadena(" ", 1);
                        }

                        if (variable.Equals("APELLIDO PATERNO"))
                        {
                            lineatitulo1 = lineatitulo1 + "---------------" + funciones.replicateCadena(" ", 1);
                            lineatitulo2 = lineatitulo2 + "   AP.PATERNO  " + funciones.replicateCadena(" ", 1);
                            lineatitulo3 = lineatitulo3 + "---------------" + funciones.replicateCadena(" ", 1);
                        }

                        if (variable.Equals("APELLIDO MATERNO"))
                        {
                            lineatitulo1 = lineatitulo1 + "---------------" + funciones.replicateCadena(" ", 1);
                            lineatitulo2 = lineatitulo2 + "   AP.MATERNO  " + funciones.replicateCadena(" ", 1);
                            lineatitulo3 = lineatitulo3 + "---------------" + funciones.replicateCadena(" ", 1);
                        }

                        if (variable.Equals("NOMBRES"))
                        {
                            lineatitulo1 = lineatitulo1 + "-------------------------" + funciones.replicateCadena(" ", 1);
                            lineatitulo2 = lineatitulo2 + "          NOMBRES        " + funciones.replicateCadena(" ", 1);
                            lineatitulo3 = lineatitulo3 + "-------------------------" + funciones.replicateCadena(" ", 1);
                        }

                        if (variable.Equals("DOCUMENTO DE IDENTIDAD"))
                        {
                            lineatitulo1 = lineatitulo1 + "--------------------" + funciones.replicateCadena(" ", 1);
                            lineatitulo2 = lineatitulo2 + "   DOC. IDENTIDAD   " + funciones.replicateCadena(" ", 1);
                            lineatitulo3 = lineatitulo3 + "--------------------" + funciones.replicateCadena(" ", 1);
                        }


                        if (variable.Equals("FECHA DE NACIMIENTO"))
                        {
                            lineatitulo1 = lineatitulo1 + "----------" + funciones.replicateCadena(" ", 1);
                            lineatitulo2 = lineatitulo2 + "  F.NAC.  " + funciones.replicateCadena(" ", 1);
                            lineatitulo3 = lineatitulo3 + "----------" + funciones.replicateCadena(" ", 1);
                        }

                        if (variable.Equals("SEXO"))
                        {
                            lineatitulo1 = lineatitulo1 + "----------" + funciones.replicateCadena(" ", 1);
                            lineatitulo2 = lineatitulo2 + "   SEXO   " + funciones.replicateCadena(" ", 1);
                            lineatitulo3 = lineatitulo3 + "----------" + funciones.replicateCadena(" ", 1);
                        }

                        if (variable.Equals("TELEFONO FIJO"))
                        {
                            lineatitulo1 = lineatitulo1 + "---------------" + funciones.replicateCadena(" ", 1);
                            lineatitulo2 = lineatitulo2 + "   TEL. FIJO   " + funciones.replicateCadena(" ", 1);
                            lineatitulo3 = lineatitulo3 + "---------------" + funciones.replicateCadena(" ", 1);
                        }

                        if (variable.Equals("CELULAR"))
                        {
                            lineatitulo1 = lineatitulo1 + "---------------" + funciones.replicateCadena(" ", 1);
                            lineatitulo2 = lineatitulo2 + "    CELULAR    " + funciones.replicateCadena(" ", 1);
                            lineatitulo3 = lineatitulo3 + "---------------" + funciones.replicateCadena(" ", 1);
                        }

                        if (variable.Equals("RPM"))
                        {
                            lineatitulo1 = lineatitulo1 + "---------------" + funciones.replicateCadena(" ", 1);
                            lineatitulo2 = lineatitulo2 + "      RPM      " + funciones.replicateCadena(" ", 1);
                            lineatitulo3 = lineatitulo3 + "---------------" + funciones.replicateCadena(" ", 1);
                        }

                        if (variable.Equals("NACIONALIDAD"))
                        {
                            lineatitulo1 = lineatitulo1 + "--------------------" + funciones.replicateCadena(" ", 1);
                            lineatitulo2 = lineatitulo2 + "    NACIONALIDAD    " + funciones.replicateCadena(" ", 1);
                            lineatitulo3 = lineatitulo3 + "--------------------" + funciones.replicateCadena(" ", 1);
                        }

                        if (variable.Equals("ESTADO CIVIL"))
                        {
                            lineatitulo1 = lineatitulo1 + "--------------------" + funciones.replicateCadena(" ", 1);
                            lineatitulo2 = lineatitulo2 + "    ESTADO CIVIL    " + funciones.replicateCadena(" ", 1);
                            lineatitulo3 = lineatitulo3 + "--------------------" + funciones.replicateCadena(" ", 1);
                        }

                        if (variable.Equals("LICENCIA DE CONDUCIR"))
                        {
                            lineatitulo1 = lineatitulo1 + "--------------------" + funciones.replicateCadena(" ", 1);
                            lineatitulo2 = lineatitulo2 + "    LIC. CONDUCIR   " + funciones.replicateCadena(" ", 1);
                            lineatitulo3 = lineatitulo3 + "--------------------" + funciones.replicateCadena(" ", 1);
                        }

                        if (variable.Equals("DIRECCION"))
                        {
                            lineatitulo1 = lineatitulo1 + "------------------------------------------------------------" + funciones.replicateCadena(" ", 1);
                            lineatitulo2 = lineatitulo2 + "                          DIRECCION                         " + funciones.replicateCadena(" ", 1);
                            lineatitulo3 = lineatitulo3 + "------------------------------------------------------------" + funciones.replicateCadena(" ", 1);
                        }

                        if (variable.Equals("TIPO DE TRABAJADOR"))
                        {
                            lineatitulo1 = lineatitulo1 + "--------------------" + funciones.replicateCadena(" ", 1);
                            lineatitulo2 = lineatitulo2 + "  TIPO TRABAJADOR   " + funciones.replicateCadena(" ", 1);
                            lineatitulo3 = lineatitulo3 + "--------------------" + funciones.replicateCadena(" ", 1);
                        }

                        if (variable.Equals("TIPO DE TRABAJADOR"))
                        {
                            lineatitulo1 = lineatitulo1 + "--------------------" + funciones.replicateCadena(" ", 1);
                            lineatitulo2 = lineatitulo2 + "  TIPO TRABAJADOR   " + funciones.replicateCadena(" ", 1);
                            lineatitulo3 = lineatitulo3 + "--------------------" + funciones.replicateCadena(" ", 1);
                        }

                        if (variable.Equals("NIVEL EDUCATIVO"))
                        {
                            lineatitulo1 = lineatitulo1 + "---------------------------------------------" + funciones.replicateCadena(" ", 1);
                            lineatitulo2 = lineatitulo2 + "               NIVEL EDUCATIVO               " + funciones.replicateCadena(" ", 1);
                            lineatitulo3 = lineatitulo3 + "---------------------------------------------" + funciones.replicateCadena(" ", 1);
                        }

                        if (variable.Equals("SECCION"))
                        {
                            lineatitulo1 = lineatitulo1 + "--------------------" + funciones.replicateCadena(" ", 1);
                            lineatitulo2 = lineatitulo2 + "       SECCION      " + funciones.replicateCadena(" ", 1);
                            lineatitulo3 = lineatitulo3 + "--------------------" + funciones.replicateCadena(" ", 1);
                        }

                        if (variable.Equals("OCUPACION"))
                        {
                            lineatitulo1 = lineatitulo1 + "-------------------------" + funciones.replicateCadena(" ", 1);
                            lineatitulo2 = lineatitulo2 + "         OCUPACION       " + funciones.replicateCadena(" ", 1);
                            lineatitulo3 = lineatitulo3 + "-------------------------" + funciones.replicateCadena(" ", 1);
                        }

                        if (variable.Equals("REGIMEN PENSIONARIO"))
                        {
                            lineatitulo1 = lineatitulo1 + "------------------------------" + funciones.replicateCadena(" ", 1);
                            lineatitulo2 = lineatitulo2 + "       REG. PENSIONARIO       " + funciones.replicateCadena(" ", 1);
                            lineatitulo3 = lineatitulo3 + "------------------------------" + funciones.replicateCadena(" ", 1);
                        }

                        if (variable.Equals("FECHA INSCRIPCION"))
                        {
                            lineatitulo1 = lineatitulo1 + "----------" + funciones.replicateCadena(" ", 1);
                            lineatitulo2 = lineatitulo2 + " F.INSC.  " + funciones.replicateCadena(" ", 1);
                            lineatitulo3 = lineatitulo3 + "----------" + funciones.replicateCadena(" ", 1);
                        }

                        if (variable.Equals("CUSPP"))
                        {
                            lineatitulo1 = lineatitulo1 + "---------------" + funciones.replicateCadena(" ", 1);
                            lineatitulo2 = lineatitulo2 + "     CUSPP     " + funciones.replicateCadena(" ", 1);
                            lineatitulo3 = lineatitulo3 + "---------------" + funciones.replicateCadena(" ", 1);
                        }

                        if (variable.Equals("TIPO DE CONTRATO"))
                        {
                            lineatitulo1 = lineatitulo1 + "-------------------------" + funciones.replicateCadena(" ", 1);
                            lineatitulo2 = lineatitulo2 + "    TIPO DE CONTRATO     " + funciones.replicateCadena(" ", 1);
                            lineatitulo3 = lineatitulo3 + "-------------------------" + funciones.replicateCadena(" ", 1);
                        }

                        if (variable.Equals("TIPO DE PAGO"))
                        {
                            lineatitulo1 = lineatitulo1 + "--------------------" + funciones.replicateCadena(" ", 1);
                            lineatitulo2 = lineatitulo2 + "    TIPO DE PAGO    " + funciones.replicateCadena(" ", 1);
                            lineatitulo3 = lineatitulo3 + "--------------------" + funciones.replicateCadena(" ", 1);
                        }

                        if (variable.Equals("PERIODICIDAD DEL INGRESO"))
                        {
                            lineatitulo1 = lineatitulo1 + "--------------------" + funciones.replicateCadena(" ", 1);
                            lineatitulo2 = lineatitulo2 + "PERIODICIDAD INGRESO" + funciones.replicateCadena(" ", 1);
                            lineatitulo3 = lineatitulo3 + "--------------------" + funciones.replicateCadena(" ", 1);
                        }

                        if (variable.Equals("SITUACION ESPECIAL"))
                        {
                            lineatitulo1 = lineatitulo1 + "--------------------" + funciones.replicateCadena(" ", 1);
                            lineatitulo2 = lineatitulo2 + " SITUACION ESPECIAL " + funciones.replicateCadena(" ", 1);
                            lineatitulo3 = lineatitulo3 + "--------------------" + funciones.replicateCadena(" ", 1);
                        }

                        if (variable.Equals("CTA. REMUNERACIONES"))
                        {
                            lineatitulo1 = lineatitulo1 + "------------------------------------------------------------" + funciones.replicateCadena(" ", 1);
                            lineatitulo2 = lineatitulo2 + "                CTA. REMUNERACIONES                         " + funciones.replicateCadena(" ", 1);
                            lineatitulo3 = lineatitulo3 + "------------------------------------------------------------" + funciones.replicateCadena(" ", 1);
                        }

                        if (variable.Equals("CTA. CTS"))
                        {
                            lineatitulo1 = lineatitulo1 + "------------------------------------------------------------" + funciones.replicateCadena(" ", 1);
                            lineatitulo2 = lineatitulo2 + "                        CTA. CTS                            " + funciones.replicateCadena(" ", 1);
                            lineatitulo3 = lineatitulo3 + "------------------------------------------------------------" + funciones.replicateCadena(" ", 1);
                        }

                        if (variable.Equals("FECHA DE INGRESO"))
                        {
                            lineatitulo1 = lineatitulo1 + "----------" + funciones.replicateCadena(" ", 1);
                            lineatitulo2 = lineatitulo2 + "F.INGRESO " + funciones.replicateCadena(" ", 1);
                            lineatitulo3 = lineatitulo3 + "----------" + funciones.replicateCadena(" ", 1);
                        }

                        if (variable.Equals("FECHA DE CESE"))
                        {
                            lineatitulo1 = lineatitulo1 + "----------" + funciones.replicateCadena(" ", 1);
                            lineatitulo2 = lineatitulo2 + "  F.CESE  " + funciones.replicateCadena(" ", 1);
                            lineatitulo3 = lineatitulo3 + "----------" + funciones.replicateCadena(" ", 1);
                        }

                        if (variable.Equals("OCUPACION PDT-0601"))
                        {
                            lineatitulo1 = lineatitulo1 + "------------------------------" + funciones.replicateCadena(" ", 1);
                            lineatitulo2 = lineatitulo2 + "      OCUPACION PDT-0601      " + funciones.replicateCadena(" ", 1);
                            lineatitulo3 = lineatitulo3 + "------------------------------" + funciones.replicateCadena(" ", 1);
                        }

                        if (variable.Equals("ESTABLECIMIENTO"))
                        {
                            lineatitulo1 = lineatitulo1 + "----------------------------------------" + funciones.replicateCadena(" ", 1);
                            lineatitulo2 = lineatitulo2 + "               ESTABLECIMIENTO          " + funciones.replicateCadena(" ", 1);
                            lineatitulo3 = lineatitulo3 + "----------------------------------------" + funciones.replicateCadena(" ", 1);
                        }

                        if (variable.Equals("EPS"))
                        {
                            lineatitulo1 = lineatitulo1 + "----------------------------------------" + funciones.replicateCadena(" ", 1);
                            lineatitulo2 = lineatitulo2 + "                    EPS                 " + funciones.replicateCadena(" ", 1);
                            lineatitulo3 = lineatitulo3 + "----------------------------------------" + funciones.replicateCadena(" ", 1);
                        }

                        if (variable.Equals("JORNAL BASICO / REMUNERACION"))
                        {
                            lineatitulo1 = lineatitulo1 + "---------------" + funciones.replicateCadena(" ", 1);
                            lineatitulo2 = lineatitulo2 + "  JORNAL/REMU. " + funciones.replicateCadena(" ", 1);
                            lineatitulo3 = lineatitulo3 + "---------------" + funciones.replicateCadena(" ", 1);
                        }


                    }

                    if (chkFirma.Checked)
                    {

                        lineatitulo1 = lineatitulo1 + "---------------" + funciones.replicateCadena(" ", 1);
                        lineatitulo2 = lineatitulo2 + "     FIRMA     " + funciones.replicateCadena(" ", 1);
                        lineatitulo3 = lineatitulo3 + "---------------" + funciones.replicateCadena(" ", 1);

                    }

                    lineatitulo1 = lineatitulo1 + "\n";
                    lineatitulo2 = lineatitulo2 + "\n";
                    lineatitulo3 = lineatitulo3 + "\n";

                    strPrinter = strPrinter + lineatitulo1 + lineatitulo2 + lineatitulo3;


                    int i = 0;
                    int fila = 0;

                    foreach (DataRow row_per in dtper.Rows)
                    {
                        fila++;

                        if (fila > 59)
                        {
                            strItem = strItem + '\f';
                            strItem = strItem + ciafile.ui_getDatosCiaFile(codcia, "DESCRIPCION") + "\n";
                            strItem = strItem + ciafile.ui_getDatosCiaFile(codcia, "RUC") + "\n";
                            strItem = strItem + "                                        R E P O R T E    D E   P E R S O N A L                                      " + "\n\n";
                            strItem = strItem + lineatitulo1;
                            strItem = strItem + lineatitulo2;
                            strItem = strItem + lineatitulo3;
                            fila = 0;
                        }

                        i++;

                        if (chkEnumera.Checked)
                        {
                            strItem = strItem + (i.ToString() + funciones.replicateCadena(" ", 4)).Substring(0, 4) + funciones.replicateCadena(" ", 1);
                        }

                        foreach (string variable in lstDestino.Items)
                        {

                            if (variable.Equals("CODIGO INTERNO"))
                            {
                                strItem = strItem + row_per["idperplan"].ToString() + funciones.replicateCadena(" ", 1);
                            }

                            if (variable.Equals("APELLIDO PATERNO"))
                            {
                                strItem = strItem + (row_per["apepat"].ToString() + funciones.replicateCadena(" ", 15)).Substring(0, 15) + funciones.replicateCadena(" ", 1);
                            }

                            if (variable.Equals("APELLIDO MATERNO"))
                            {
                                strItem = strItem + (row_per["apemat"].ToString() + funciones.replicateCadena(" ", 15)).Substring(0, 15) + funciones.replicateCadena(" ", 1);
                            }

                            if (variable.Equals("NOMBRES"))
                            {
                                strItem = strItem + (row_per["nombres"].ToString() + funciones.replicateCadena(" ", 25)).Substring(0, 25) + funciones.replicateCadena(" ", 1);
                            }

                            if (variable.Equals("DOCUMENTO DE IDENTIDAD"))
                            {
                                strItem = strItem + (row_per["DocIdentidad"].ToString() + funciones.replicateCadena(" ", 20)).Substring(0, 20) + funciones.replicateCadena(" ", 1);
                            }

                            if (variable.Equals("FECHA DE NACIMIENTO"))
                            {
                                strItem = strItem + row_per["fecnac"].ToString().Substring(0, 10) + funciones.replicateCadena(" ", 1);
                            }

                            if (variable.Equals("SEXO"))
                            {
                                strItem = strItem + (row_per["sexo"].ToString() + funciones.replicateCadena(" ", 10)).Substring(0, 10) + funciones.replicateCadena(" ", 1);
                            }

                            if (variable.Equals("TELEFONO FIJO"))
                            {
                                strItem = strItem + (row_per["telfijo"].ToString() + funciones.replicateCadena(" ", 15)).Substring(0, 15) + funciones.replicateCadena(" ", 1);
                            }

                            if (variable.Equals("CELULAR"))
                            {
                                strItem = strItem + (row_per["celular"].ToString() + funciones.replicateCadena(" ", 15)).Substring(0, 15) + funciones.replicateCadena(" ", 1);
                            }

                            if (variable.Equals("RPM"))
                            {
                                strItem = strItem + (row_per["rpm"].ToString() + funciones.replicateCadena(" ", 15)).Substring(0, 15) + funciones.replicateCadena(" ", 1);
                            }

                            if (variable.Equals("NACIONALIDAD"))
                            {
                                strItem = strItem + (row_per["nacionalidad"].ToString() + funciones.replicateCadena(" ", 20)).Substring(0, 20) + funciones.replicateCadena(" ", 1);
                            }

                            if (variable.Equals("ESTADO CIVIL"))
                            {
                                strItem = strItem + (row_per["EstadoCivil"].ToString() + funciones.replicateCadena(" ", 20)).Substring(0, 20) + funciones.replicateCadena(" ", 1);
                            }

                            if (variable.Equals("LICENCIA DE CONDUCIR"))
                            {
                                strItem = strItem + (row_per["LicenciaConducir"].ToString() + funciones.replicateCadena(" ", 20)).Substring(0, 20) + funciones.replicateCadena(" ", 1);
                            }

                            if (variable.Equals("DIRECCION"))
                            {
                                strItem = strItem + (row_per["Direccion"].ToString() + funciones.replicateCadena(" ", 60)).Substring(0, 60) + funciones.replicateCadena(" ", 1);
                            }

                            if (variable.Equals("TIPO DE TRABAJADOR"))
                            {
                                strItem = strItem + (row_per["TipoTrabajador"].ToString() + funciones.replicateCadena(" ", 15)).Substring(0, 15) + funciones.replicateCadena(" ", 1);
                            }

                            if (variable.Equals("NIVEL EDUCATIVO"))
                            {
                                strItem = strItem + (row_per["NivelEducativo"].ToString() + funciones.replicateCadena(" ", 45)).Substring(0, 45) + funciones.replicateCadena(" ", 1);
                            }

                            if (variable.Equals("SECCION"))
                            {
                                strItem = strItem + (row_per["Seccion"].ToString() + funciones.replicateCadena(" ", 45)).Substring(0, 45) + funciones.replicateCadena(" ", 1);
                            }

                            if (variable.Equals("OCUPACION"))
                            {
                                strItem = strItem + (row_per["ocupacion"].ToString() + funciones.replicateCadena(" ", 25)).Substring(0, 25) + funciones.replicateCadena(" ", 1);
                            }

                            if (variable.Equals("REGIMEN PENSIONARIO"))
                            {
                                strItem = strItem + (row_per["RegimenPensionario"].ToString() + funciones.replicateCadena(" ", 30)).Substring(0, 30) + funciones.replicateCadena(" ", 1);
                            }

                            if (variable.Equals("FECHA INSCRIPCION"))
                            {
                                strItem = strItem + row_per["fecregpen"].ToString().Substring(0, 10) + funciones.replicateCadena(" ", 1);
                            }

                            if (variable.Equals("CUSPP"))
                            {
                                strItem = strItem + (row_per["cuspp"].ToString() + funciones.replicateCadena(" ", 15)).Substring(0, 15) + funciones.replicateCadena(" ", 1);
                            }

                            if (variable.Equals("TIPO DE CONTRATO"))
                            {
                                strItem = strItem + (row_per["TipoContrato"].ToString() + funciones.replicateCadena(" ", 25)).Substring(0, 25) + funciones.replicateCadena(" ", 1);
                            }

                            if (variable.Equals("TIPO DE PAGO"))
                            {
                                strItem = strItem + (row_per["TipoPago"].ToString() + funciones.replicateCadena(" ", 20)).Substring(0, 20) + funciones.replicateCadena(" ", 1);
                            }

                            if (variable.Equals("PERIODICIDAD DEL INGRESO"))
                            {
                                strItem = strItem + (row_per["PeriodicidadIngreso"].ToString() + funciones.replicateCadena(" ", 20)).Substring(0, 20) + funciones.replicateCadena(" ", 1);
                            }

                            if (variable.Equals("SITUACION ESPECIAL"))
                            {
                                strItem = strItem + (row_per["SituacionEspecial"].ToString() + funciones.replicateCadena(" ", 20)).Substring(0, 20) + funciones.replicateCadena(" ", 1);
                            }

                            if (variable.Equals("CTA. REMUNERACIONES"))
                            {
                                strItem = strItem + (row_per["CtaRemuneracion"].ToString() + funciones.replicateCadena(" ", 60)).Substring(0, 60) + funciones.replicateCadena(" ", 1);
                            }

                            if (variable.Equals("CTA. CTS"))
                            {
                                strItem = strItem + (row_per["CtaCts"].ToString() + funciones.replicateCadena(" ", 60)).Substring(0, 60) + funciones.replicateCadena(" ", 1);
                            }

                            if (variable.Equals("FECHA DE INGRESO"))
                            {
                                strItem = strItem + (row_per["FechaInicio"].ToString() + funciones.replicateCadena(" ", 10)).Substring(0, 10) + funciones.replicateCadena(" ", 1);
                            }

                            if (variable.Equals("FECHA DE CESE"))
                            {
                                strItem = strItem + (row_per["FechaCese"].ToString() + funciones.replicateCadena(" ", 10)).Substring(0, 10) + funciones.replicateCadena(" ", 1);
                            }

                            if (variable.Equals("OCUPACION PDT-0601"))
                            {
                                strItem = strItem + (row_per["OcupacionPdt0601"].ToString() + funciones.replicateCadena(" ", 30)).Substring(0, 30) + funciones.replicateCadena(" ", 1);
                            }

                            if (variable.Equals("ESTABLECIMIENTO"))
                            {
                                strItem = strItem + (row_per["Establecimiento"].ToString() + funciones.replicateCadena(" ", 40)).Substring(0, 40) + funciones.replicateCadena(" ", 1);
                            }

                            if (variable.Equals("EPS"))
                            {
                                strItem = strItem + (row_per["Eps"].ToString() + funciones.replicateCadena(" ", 40)).Substring(0, 40) + funciones.replicateCadena(" ", 1);
                            }

                            if (variable.Equals("JORNAL BASICO / REMUNERACION"))
                            {
                                jorremu = row_per["Jornal_Remu"].ToString();
                                strItem = strItem + (float.Parse(jorremu).ToString("##,##0.00;(##,##0.00);Zero") + funciones.replicateCadena(" ", 15)).Substring(0, 15) + funciones.replicateCadena(" ", 1);
                            }

                        }

                        if (chkFirma.Checked)
                        {
                            strItem = strItem + funciones.replicateCadena("-", 15);
                        }

                        strItem = strItem + "\n";

                    }

                    strPrinter = strPrinter + strItem;

                    ui_reportetxt ui_reportetxt = new ui_reportetxt();
                    ui_reportetxt._texto = strPrinter;
                    ui_reportetxt.Activate();
                    ui_reportetxt.BringToFront();
                    ui_reportetxt.ShowDialog();
                    ui_reportetxt.Dispose();
                }
                else
                {
                    MessageBox.Show("No existen registros en el criterio de búsqueda seleccionado", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            ui_listapersonal();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < lstOrigen.SelectedItems.Count; i++)
            {
                lstDestino.Items.Add(lstOrigen.SelectedItems[i]);
                lstOrigen.Items.Remove(lstOrigen.SelectedItems[i]);
            }

        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < lstDestino.SelectedItems.Count; i++)
            {
                lstOrigen.Items.Add(lstDestino.SelectedItems[i]);
                lstDestino.Items.Remove(lstDestino.SelectedItems[i]);
            }
        }

        private void radioButtonSiEmp_CheckedChanged(object sender, EventArgs e)
        {
            Funciones funciones = new Funciones();
            string idcia = this._codcia;

            string query = "SELECT rucemp as clave,razonemp as descripcion FROM emplea WHERE idciafile='" + @idcia + "' order by rucemp asc;";
            funciones.listaComboBox(query, cmbEmpleador, "");
            cmbEmpleador.Enabled = true;

            string ruccia = funciones.getValorComboBox(cmbEmpleador, 11);
            string query_esta = "SELECT idestane as clave,desestane as descripcion FROM estane WHERE codemp='" + @ruccia + "' and idciafile='" + @idcia + "' order by idestane asc;";
            funciones.listaComboBox(query_esta, cmbEstablecimiento, "X");
            cmbEmpleador.Focus();

        }

        private void radioButtonNoEmp_CheckedChanged(object sender, EventArgs e)
        {
            Funciones funciones = new Funciones();
            GlobalVariables variables = new GlobalVariables();
            string idcia = this._codcia;
            string ruccia = variables.getValorRucCia();

            string squery = "SELECT ruccia as clave,descia as descripcion FROM ciafile WHERE idcia='" + @idcia + "';";
            funciones.listaComboBox(squery, cmbEmpleador, "");
            cmbEmpleador.Enabled = false;

            string squery_esta = "SELECT idestane as clave,desestane as descripcion FROM estane WHERE codemp='" + @ruccia + "' and idciafile='" + @idcia + "' order by idestane asc;";
            funciones.listaComboBox(squery_esta, cmbEstablecimiento, "X");
            cmbEstablecimiento.Focus();
        }

        private void cmbEmpleador_SelectedIndexChanged(object sender, EventArgs e)
        {
            Funciones funciones = new Funciones();
            GlobalVariables variables = new GlobalVariables();
            string idcia = variables.getValorCia();
            string ruccia = funciones.getValorComboBox(cmbEmpleador, 11);
            string squery_esta = "SELECT idestane as clave,desestane as descripcion FROM estane WHERE codemp='" + @ruccia + "' and idciafile='" + @idcia + "' order by idestane asc;";
            funciones.listaComboBox(squery_esta, cmbEstablecimiento, "X");
            cmbEstablecimiento.Focus();
        }
    }
}