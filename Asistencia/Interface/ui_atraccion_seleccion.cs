using CaniaBrava.cs;
using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Linq;
using System.IO;
using System.Drawing;
using System.Globalization;
using Excel = Microsoft.Office.Interop.Excel;
//using SAP.Middleware.Connector;
using System.Threading;
using System.Diagnostics;
using OfficeOpenXml;
using System.Data;
using System.Collections.Generic;
using System.Text;
using CaniaBrava.Interface;

namespace CaniaBrava.Interface
{
    public partial class ui_atraccion_seleccion : Form

    {
        Funciones funciones = new Funciones();

        public ui_atraccion_seleccion()
        {
            InitializeComponent();
            if (funciones.VersionAssembly()) Application.ExitThread();
        }

        private void nombrecadena()
        {
            lblNombre.Text = txtNombres.Text.Trim();
        }

        private void calchoras()
        {

            DateTime feini = Convert.ToDateTime(txtfeinicioproceso.Value.Date);

            DateTime fefin = Convert.ToDateTime(txtfecierre.Value.Date);

            TimeSpan diasDiferencia = fefin - feini;
            int dias = diasDiferencia.Days + 1;
            string numString = dias + "";

            txttotaldias.Text = numString;
        }

        public ui_atraccion_seleccion(DataGridView dgvdetalle)
        {
            InitializeComponent();
            if (funciones.VersionAssembly()) Application.ExitThread();

            foreach (DataGridViewRow row in dgvdetalle.Rows)
            {


                txtregistro.Text = dgvdetalle.CurrentRow.Cells[0].Value.ToString();
                txtNroDoc.Text = dgvdetalle.CurrentRow.Cells[1].Value.ToString();
                txtcodempleado.Text = dgvdetalle.CurrentRow.Cells[2].Value.ToString();
                txtNombres.Text = dgvdetalle.CurrentRow.Cells[3].Value.ToString();
                txtfeinicioproceso.Text = dgvdetalle.CurrentRow.Cells[4].Value.ToString();
                cmbNivelEducativo.Items.Add(dgvdetalle.CurrentRow.Cells[5].Value.ToString());
                cmbTipoDocumento.Items.Add(dgvdetalle.CurrentRow.Cells[6].Value.ToString());
                txtCelular.Text = dgvdetalle.CurrentRow.Cells[7].Value.ToString();
                txtTelFijo.Text = dgvdetalle.CurrentRow.Cells[8].Value.ToString();
                cmbNacionalidad.Items.Add(dgvdetalle.CurrentRow.Cells[9].Value.ToString());
                txtdistrito.Text = dgvdetalle.CurrentRow.Cells[10].Value.ToString();
                txtprovincia.Text = dgvdetalle.CurrentRow.Cells[11].Value.ToString();
                txtdepartamento.Text = dgvdetalle.CurrentRow.Cells[12].Value.ToString();
                txtreferencia.Text = dgvdetalle.CurrentRow.Cells[13].Value.ToString();
                txtFechaNac.Text = dgvdetalle.CurrentRow.Cells[14].Value.ToString();
                cmbSexo.Items.Add(dgvdetalle.CurrentRow.Cells[15].Value.ToString());
                cmbEstadoCivil.Items.Add(dgvdetalle.CurrentRow.Cells[16].Value.ToString());
                cmbCategoriaBrevete.Items.Add(dgvdetalle.CurrentRow.Cells[17].Value.ToString());
                txtNroLicenciaConductor.Text = dgvdetalle.CurrentRow.Cells[18].Value.ToString();
                cmbGerencia.Items.Add(dgvdetalle.CurrentRow.Cells[19].Value.ToString());
                cmbunidorg.Items.Add(dgvdetalle.CurrentRow.Cells[20].Value.ToString());
                cmbPosicion.Items.Add(dgvdetalle.CurrentRow.Cells[21].Value.ToString());
                cmbsoc.Items.Add(dgvdetalle.CurrentRow.Cells[22].Value.ToString());
                txtnomsoc.Text = dgvdetalle.CurrentRow.Cells[23].Value.ToString();
                txtEmail.Text = dgvdetalle.CurrentRow.Cells[24].Value.ToString();
                txtjefatura.Text = dgvdetalle.CurrentRow.Cells[25].Value.ToString();
                txtrrhh.Text = dgvdetalle.CurrentRow.Cells[26].Value.ToString();
                cmbnivorganizacional.Items.Add(dgvdetalle.CurrentRow.Cells[27].Value.ToString());
                cmbsede.Items.Add(dgvdetalle.CurrentRow.Cells[28].Value.ToString());
                cmborigen.Items.Add(dgvdetalle.CurrentRow.Cells[29].Value.ToString());
                txtreemplazo.Text = dgvdetalle.CurrentRow.Cells[30].Value.ToString();
                cmbtipoproceso.Items.Add(dgvdetalle.CurrentRow.Cells[31].Value.ToString());
                cmbmedioate.Items.Add(dgvdetalle.CurrentRow.Cells[32].Value.ToString());
                txtfuentepostulacion.Text = dgvdetalle.CurrentRow.Cells[33].Value.ToString();
                cmbmodalidad.Items.Add(dgvdetalle.CurrentRow.Cells[34].Value.ToString());
                cmbtipocontrato.Items.Add(dgvdetalle.CurrentRow.Cells[35].Value.ToString());
                txtvacantes.Text = dgvdetalle.CurrentRow.Cells[36].Value.ToString();
                txtcantevaluados.Text = dgvdetalle.CurrentRow.Cells[37].Value.ToString();
                cmbestadoproceso.Items.Add(dgvdetalle.CurrentRow.Cells[38].Value.ToString());
                cmbprioridad.Items.Add(dgvdetalle.CurrentRow.Cells[39].Value.ToString());
                txtproveedor.Text = dgvdetalle.CurrentRow.Cells[40].Value.ToString();
                cmbestatusocupacion.Items.Add(dgvdetalle.CurrentRow.Cells[41].Value.ToString());
                txtnrolistenvi.Text = dgvdetalle.CurrentRow.Cells[42].Value.ToString();
                txtsatisfaccion.Text = dgvdetalle.CurrentRow.Cells[43].Value.ToString();
                txtcomentarios.Text = dgvdetalle.CurrentRow.Cells[44].Value.ToString();


                string carta = dgvdetalle.CurrentRow.Cells[45].Value.ToString();
                if (carta == "SI") { rdbcartaSi.Checked = true; }
                else { rdbcartaNo.Checked = true; }

                string inducciones = dgvdetalle.CurrentRow.Cells[46].Value.ToString();
                if (inducciones == "SI") { rbdInduccionSI.Checked = true; }
                else { rbdInduccionNo.Checked = true; }



                txtfeestimacion.Text = dgvdetalle.CurrentRow.Cells[47].Value.ToString();
                txtfecierre.Text = dgvdetalle.CurrentRow.Cells[48].Value.ToString();
                txtfeincorporacion.Text = dgvdetalle.CurrentRow.Cells[49].Value.ToString();
                txttotaldias.Text = dgvdetalle.CurrentRow.Cells[50].Value.ToString();
                //toolStripButton1.Visible = false;
                //toolStripButton2.Visible = true;

                lblNombre.Text = dgvdetalle.CurrentRow.Cells[3].Value.ToString();


            }
        }


        private void sumarid()
        {


            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();
            string query = "Select max(idregistro) id from Asistencia.dbo.gestiontalento";
            try
            {
                SqlCommand myCommand = new SqlCommand(query, conexion);

                string codmax = Convert.ToString(myCommand.ExecuteScalar());
                int cod = Convert.ToInt32(codmax) + 1;
                txtidmax.Text = Convert.ToString(cod);
            }
            catch (SqlException e)
            {
                MessageBox.Show(e.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }

            conexion.Close();

        }


        private void ui_atraccion_seleccion_Load(object sender, EventArgs e)
        {
            TopMost = true;
            tabControlPer.SelectTab(0);
            atraccion_seleccion c = new atraccion_seleccion();
            cmbNivelEducativo.Items.Add("SELECCIONAR");
            c.niveleducativo(cmbNivelEducativo);
            cmbTipoDocumento.Items.Add("SELECCIONAR");
            c.tipodocumento(cmbTipoDocumento);
            cmbSexo.Items.Add("SELECCIONAR");
            c.sexo(cmbSexo);
            cmbEstadoCivil.Items.Add("SELECCIONAR");
            c.estadocivil(cmbEstadoCivil);
            cmbCategoriaBrevete.Items.Add("SELECCIONAR");
            c.licenciaconducir(cmbCategoriaBrevete);
            cmbGerencia.Items.Add("SELECCIONAR");
            c.gerencia(cmbGerencia);
            cmbunidorg.Items.Add("SELECCIONAR");
            c.unidadorganizativa(cmbunidorg);
            cmbPosicion.Items.Add("SELECCIONAR");
            c.posicion(cmbPosicion);
            cmbsoc.Items.Add("SELECCIONAR");
            c.sociedad(cmbsoc);
            cbpredeterminados();
            sumarid();
            calchoras();

            if (txtregistro.Text != ""){
                tabControlPer.Enabled = true;
                toolStripButton2.Visible = true;
                toolStripSeparator2.Visible = true;
                //btnGuardar.Visible = false;
                btnNuevo.Visible=false;
            }
            else
            {
                tabControlPer.Enabled = false;
                toolStripButton2.Visible = false;
                //btnGuardar.Visible = true;
                btnNuevo.Visible = true;
            }
            
            //toolStripButton2.Visible = true;
        }

        public void cbpredeterminados()
        {
            cmbNacionalidad.Items.Add("SELECCIONAR");
            cmbNacionalidad.Items.Add("PERUANO(A)");
            cmbNacionalidad.Items.Add("EXTRANJERO(A)");
            cmbNacionalidad.SelectedIndex = 0;

            cmbnivorganizacional.Items.Add("SELECCIONAR");
            cmbnivorganizacional.Items.Add("EMPLEADO(A)");
            cmbnivorganizacional.Items.Add("PRACTICANTE");
            cmbnivorganizacional.Items.Add("OPERARIO(A)");
            cmbnivorganizacional.Items.Add("OBRERO(A)");
            cmbnivorganizacional.SelectedIndex = 0;

            cmbsede.Items.Add("SELECCIONAR");
            cmbsede.Items.Add("MONTELIMA");
            cmbsede.Items.Add("LOBO");
            cmbsede.Items.Add("LA HUACA");
            cmbsede.Items.Add("SAN VICENTE");
            cmbsede.Items.Add("PIURA");
            cmbsede.Items.Add("LIMA");
            cmbsede.SelectedIndex = 0;

            cmborigen.Items.Add("SELECCIONAR");
            cmborigen.Items.Add("NUEVO");
            cmborigen.Items.Add("REEMPLAZO");
            cmborigen.SelectedIndex = 0;

            cmbtipoproceso.Items.Add("SELECCIONAR");
            cmbtipoproceso.Items.Add("PUBLICO");
            cmbtipoproceso.Items.Add("PRIVADO");
            cmbtipoproceso.SelectedIndex = 0;

            cmbmedioate.Items.Add("SELECCIONAR");
            cmbmedioate.Items.Add("VIRTUAL");
            cmbmedioate.Items.Add("PRESENCIAL");
            cmbmedioate.SelectedIndex = 0;

            cmbestadoproceso.Items.Add("SELECCIONAR");
            cmbestadoproceso.Items.Add("CERRADO");
            cmbestadoproceso.Items.Add("ABIERTO");
            cmbestadoproceso.SelectedIndex = 0;

            cmbprioridad.Items.Add("SELECCIONAR");
            cmbprioridad.Items.Add("ALTA");
            cmbprioridad.Items.Add("MEDIA");
            cmbprioridad.Items.Add("BAJA");
            cmbprioridad.SelectedIndex = 0;

            cmbestatusocupacion.Items.Add("SELECCIONAR");
            cmbestatusocupacion.Items.Add("ACTIVO");
            cmbestatusocupacion.Items.Add("RENUNCIA");
            cmbestatusocupacion.Items.Add("OTRO");
            cmbestatusocupacion.SelectedIndex = 0;

            cmbmodalidad.Items.Add("SELECCIONAR");
            cmbmodalidad.Items.Add("CONTRATADO");
            cmbmodalidad.Items.Add("EN PROCESO");
            cmbmodalidad.Items.Add("CERRADO");
            cmbmodalidad.SelectedIndex = 0;

            cmbtipocontrato.Items.Add("SELECCIONAR");
            cmbtipocontrato.Items.Add("CONVENIO PRACTICAS");
            cmbtipocontrato.Items.Add("INDETERMINADO");
            cmbtipocontrato.Items.Add("SERVICIOS ESPECIFICOS");
            cmbtipocontrato.SelectedIndex = 0;


        }

        private void txtNroLicenciaConductor_TextChanged(object sender, EventArgs e)
        {
        }

        private void cmbCategoriaBrevete_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void cmbCategoriaBrevete_SelectedValueChanged(object sender, EventArgs e)
        {
            string brevete = funciones.getValorComboBox(cmbCategoriaBrevete, 8);
            if (brevete == "NO POSEE")
            {
                txtNroLicenciaConductor.Text = "";
                txtNroLicenciaConductor.Enabled = false;
            }
            else { txtNroLicenciaConductor.Enabled = true; }
        }

        private void cmbsoc_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void cmbsoc_SelectedValueChanged(object sender, EventArgs e)
        {
            string Soc = funciones.getValorComboBox(cmbsoc, 11);
            if (Soc == "153") { txtnomsoc.Text = "AGRICOLA DEL CHIRA"; }
            if (Soc == "157") { txtnomsoc.Text = "SUCROALCOLERA DEL CHIRA"; }
            if (Soc == "158") { txtnomsoc.Text = "BIOENERGIA DEL CHIRA"; }
            if (Soc == "SELECCIONAR") { txtnomsoc.Text = "DEBE SELECCIONAR UNA SOCIEDAD"; }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string texto = Microsoft.VisualBasic.Interaction.InputBox(
            "Posiciòn", "Nueva Posiciòn", "");
            cmbPosicion.Items.Add(texto);
            cmbPosicion.SelectedIndex = 0;
        }

        private void tabPageEX1_Click(object sender, EventArgs e)
        {

        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            Funciones funciones = new Funciones();


            //string idregistro1 = txtidmax.Text.Trim();
            string idempleado1 = txtcodempleado.Text.Trim();
            string nombres1 = txtNombres.Text.Trim();
            string niveleducativo1 = funciones.getValorComboBox(cmbNivelEducativo, 100);
            string tipodocumento1 = funciones.getValorComboBox(cmbTipoDocumento, 30);
            string dni1 = txtNroDoc.Text.Trim();
            string celular1 = txtCelular.Text.Trim();
            string telefono1 = txtTelFijo.Text.Trim();
            string nacionalidad1 = funciones.getValorComboBox(cmbNacionalidad, 30);
            string distrito1 = txtdistrito.Text.Trim();
            string provincia1 = txtprovincia.Text.Trim();
            string departamento1 = txtdepartamento.Text.Trim();
            string referencia1 = txtreferencia.Text.Trim();
            string fechanacimieto1 = txtFechaNac.Value.ToString("yyyy-MM-dd");
            string sexo1 = funciones.getValorComboBox(cmbSexo, 20);
            string estadocivil1 = funciones.getValorComboBox(cmbEstadoCivil, 30);
            string categoria1 = funciones.getValorComboBox(cmbCategoriaBrevete, 20);
            string licencia1 = txtNroLicenciaConductor.Text.Trim();
            string gerencia1 = funciones.getValorComboBox(cmbGerencia, 50);
            string unidorganizativa1 = funciones.getValorComboBox(cmbunidorg, 50);
            string posicion1 = funciones.getValorComboBox(cmbPosicion, 50);
            string sociedad1 = funciones.getValorComboBox(cmbsoc, 11);
            string nomsociedad1 = txtnomsoc.Text.Trim();
            string email1 = txtEmail.Text.Trim();
            string jefatura1 = txtjefatura.Text.Trim();
            string responsablerrhh1 = txtrrhh.Text.Trim();
            string nivelorganizacional1 = funciones.getValorComboBox(cmbnivorganizacional, 20);
            string sede1 = funciones.getValorComboBox(cmbsede, 15);
            string origen1 = funciones.getValorComboBox(cmborigen, 15);
            string reemplazode1 = txtreemplazo.Text.Trim();
            string tipoproceso1 = funciones.getValorComboBox(cmbtipoproceso, 20);
            string medioatencion1 = funciones.getValorComboBox(cmbmedioate, 20);
            string fuentepostulacion1 = txtfuentepostulacion.Text.Trim();
            string modalidad1 = funciones.getValorComboBox(cmbmodalidad, 20);
            string tipocontrato1 = funciones.getValorComboBox(cmbtipocontrato, 20);
            string vacantes1 = txtvacantes.Text.Trim();
            string cantevaluados1 = txtcantevaluados.Text.Trim();
            string estproceso1 = funciones.getValorComboBox(cmbestadoproceso, 20);
            string prioridad1 = funciones.getValorComboBox(cmbprioridad, 20);
            string proveedor1 = txtproveedor.Text.Trim();
            string estatusocupacion1 = funciones.getValorComboBox(cmbestatusocupacion, 20);
            string nrolong1 = txtnrolistenvi.Text.Trim();
            string satisfaccion1 = txtsatisfaccion.Text.Trim();
            string comentarios1 = txtcomentarios.Text.Trim();

            string carta;
            string inducciones;

            if (rdbcartaSi.Checked == true) { carta = "SI"; }
            else { carta = "NO"; }
            string cartaoferta1 = carta;

            if (rdbcartaSi.Checked == true) { inducciones = "SI"; }
            else { inducciones = "NO"; }
            string induccion1 = inducciones;

            string feinicioproceso1 = txtfeinicioproceso.Value.ToString("yyyy-MM-dd");
            string feestimacion1 = txtfeestimacion.Value.ToString("yyyy-MM-dd");
            string fecierre1 = txtfecierre.Value.ToString("yyyy-MM-dd");
            string feincorporacion1 = txtfeincorporacion.Value.ToString("yyyy-MM-dd");
            string Totaldias1 = txttotaldias.Text.Trim();


            if (txtNombres.Text == "")
            {
                MessageBox.Show("Debe completar campo nombres", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
                txtNombres.Focus();
            }
            if (txtNroDoc.Text == "")
            {
                MessageBox.Show("Debe completar campo dni", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
                txtNroDoc.Focus();
            }
            if (txtjefatura.Text == "")
            {
                MessageBox.Show("Debe completar campo jefatura", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
                txtjefatura.Focus();
            }
            if (txtrrhh.Text == "")
            {
                MessageBox.Show("Debe completar campo responsable de RR.HH", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
                txtrrhh.Focus();
            }

            //idregistro1,

            else
            {
                Addregistros( idempleado1, nombres1, niveleducativo1, tipodocumento1, dni1, celular1, telefono1,
                nacionalidad1, distrito1, provincia1, departamento1, referencia1, fechanacimieto1, sexo1, estadocivil1, categoria1, licencia1,
                gerencia1, unidorganizativa1, posicion1, sociedad1, nomsociedad1, email1, jefatura1, responsablerrhh1, nivelorganizacional1, sede1,
                origen1, reemplazode1, tipoproceso1, medioatencion1, fuentepostulacion1, modalidad1, tipocontrato1, vacantes1, cantevaluados1, estproceso1,
                prioridad1, proveedor1, estatusocupacion1, nrolong1, satisfaccion1, comentarios1, cartaoferta1, induccion1, feinicioproceso1, feestimacion1,
                fecierre1, feincorporacion1, Totaldias1);
            sumarid();
            tabControlPer.Enabled = false;
            limpiarcampos();
            btnGuardar.Visible = false;
            toolStripSeparator2.Visible = false;
            btnNuevo.Enabled = true;
            MessageBox.Show("Informacion Guardada con Exito..", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }

        //string idregistro1, 

        #region Adicionar Registros SQL
        private void Addregistros(string idempleado1, string nombres1, string niveleducativo1, string tipodocumento1,
                                    string dni1, string celular1, string telefono1, string nacionalidad1, string distrito1, string provincia1, string departamento1, string referencia1,
                                     string fechanacimieto1, string sexo1, string estadocivil1, string categoria1, string licencia1, string gerencia1, string unidorganizativa1, string posicion1,
                                     string sociedad1, string nomsociedad1, string email1, string jefatura1, string responsablerrhh1, string nivelorganizacional1, string sede1, string origen1,
                                     string reemplazode1, string tipoproceso1, string medioatencion1, string fuentepostulacion1, string modalidad1, string tipocontrato1, string vacantes1,
                                     string cantevaluados1, string estproceso1, string prioridad1, string proveedor1, string estatusocupacion1, string nrolong1, string satisfaccion1, string comentarios1,
                                     string cartaoferta1, string induccion1, string feinicioproceso1, string feestimacion1, string fecierre1, string feincorporacion1, string Totaldias1)
        {
            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();

            //idregistro, 

            string query = " INSERT INTO gestiontalento (idempleado, nombres, niveleducativo, tipodocumento, dni, celular, " +
                            "telefono, nacionalidad, distrito, provincia, departamento, referencia, fechanacimieto, sexo, estadocivil, categoria, licencia, gerencia, " +
                            "unidorganizativa, posicion, sociedad, nomsociedad, email, jefatura, responsablerrhh, nivelorganizacional, sede, origen, reemplazode, " +
                            "tipoproceso, medioatencion, fuentepostulacion, modalidad, tipocontrato, vacantes, cantevaluados, estproceso, prioridad, proveedor, " +
                            "estatusocupacion, nrolong, satisfaccion, comentarios, cartaoferta, induccion, feinicioproceso, feestimacion, fecierre, feincorporacion, Totaldias) VALUES ";
            //query += "('" + @idregistro1 + "'," +
            query += "('" + idempleado1 + "'," +
            //"'" + @idempleado1 + "'," +
                        "'" + @nombres1 + "'," +
                        "'" + @niveleducativo1 + "'," +
                        "'" + @tipodocumento1 + "'," +
                        "'" + @dni1 + "'," +
                        "'" + @celular1 + "'," +
                        "'" + @telefono1 + "'," +
                        "'" + @nacionalidad1 + "'," +
                        "'" + @distrito1 + "'," +
                        "'" + @provincia1 + "'," +
                        "'" + @departamento1 + "'," +
                        "'" + @referencia1 + "'," +
                        "'" + @fechanacimieto1 + "'," +
                        "'" + @sexo1 + "'," +
                        "'" + @estadocivil1 + "'," +
                        "'" + @categoria1 + "'," +
                        "'" + @licencia1 + "'," +
                        "'" + @gerencia1 + "'," +
                        "'" + @unidorganizativa1 + "'," +
                        "'" + @posicion1 + "'," +
                        "'" + @sociedad1 + "'," +
                        "'" + @nomsociedad1 + "'," +
                        "'" + @email1 + "'," +
                        "'" + @jefatura1 + "'," +
                        "'" + @responsablerrhh1 + "'," +
                        "'" + @nivelorganizacional1 + "'," +
                        "'" + @sede1 + "'," +
                        "'" + @origen1 + "'," +
                        "'" + @reemplazode1 + "'," +
                        "'" + @tipoproceso1 + "'," +
                        "'" + @medioatencion1 + "'," +
                        "'" + @fuentepostulacion1 + "'," +
                        "'" + @modalidad1 + "'," +
                        "'" + @tipocontrato1 + "'," +
                        "'" + vacantes1 + "'," +
                        "'" + cantevaluados1 + "'," +
                        "'" + @estproceso1 + "'," +
                        "'" + @prioridad1 + "'," +
                        "'" + @proveedor1 + "'," +
                        "'" + @estatusocupacion1 + "'," +
                        "'" + nrolong1 + "'," +
                        "'" + satisfaccion1 + "'," +
                        "'" + comentarios1 + "'," +
                        "'" + @cartaoferta1 + "'," +
                        "'" + @induccion1 + "'," +
                        "'" + @feinicioproceso1 + "'," +
                        "'" + @feestimacion1 + "'," +
                        "'" + @fecierre1 + "'," +
                        "'" + @feincorporacion1 + "'," +
                        "'" + Totaldias1 + "');";

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

            conexion.Close();
        }

        #endregion

        private void limpiarcampos()
        {
            txtregistro.Text = "";
            txtNroDoc.Text = "";
            txtcodempleado.Text = "";
            txtNombres.Text = "";
            txtfeinicioproceso.Text = DateTime.Now.ToString();
            cmbNivelEducativo.SelectedIndex = 0;
            cmbTipoDocumento.SelectedIndex = 0;
            txtCelular.Text = "";
            txtTelFijo.Text = "";
            cmbNacionalidad.SelectedIndex = 0;
            txtdistrito.Text = "";
            txtprovincia.Text = "";
            txtdepartamento.Text = "";
            txtreferencia.Text = "";
            txtFechaNac.Text = "";
            cmbSexo.SelectedIndex = 0;
            cmbEstadoCivil.SelectedIndex = 0;
            cmbCategoriaBrevete.SelectedIndex = 0;
            txtNroLicenciaConductor.Text = "";
            cmbGerencia.SelectedIndex = 0;
            cmbunidorg.SelectedIndex = 0;
            cmbPosicion.SelectedIndex = 0;
            cmbsoc.SelectedIndex = 0;
            txtnomsoc.Text = "";
            txtEmail.Text = "";
            txtjefatura.Text = "";
            txtrrhh.Text = "";
            cmbnivorganizacional.SelectedIndex = 0;
            cmbsede.SelectedIndex = 0;
            cmborigen.SelectedIndex = 0;
            txtreemplazo.Text = "";
            cmbtipoproceso.SelectedIndex = 0;
            cmbmedioate.SelectedIndex = 0;
            txtfuentepostulacion.Text = "";
            cmbmodalidad.SelectedIndex = 0;
            cmbtipocontrato.SelectedIndex = 0;
            txtvacantes.Text = "";
            txtcantevaluados.Text = "";
            cmbestadoproceso.SelectedIndex = 0;
            cmbprioridad.SelectedIndex = 0;
            txtproveedor.Text = "";
            cmbestatusocupacion.SelectedIndex = 0;
            txtnrolistenvi.Text = "";
            txtsatisfaccion.Text = "";
            txtcomentarios.Text = "";
            rdbcartaSi.Checked = false;
            rdbcartaNo.Checked = true; 
            rbdInduccionSI.Checked = false; 
            rbdInduccionNo.Checked = true;
            txtfeestimacion.Text = DateTime.Now.ToString();
            txtfecierre.Text = DateTime.Now.ToString();
            txtfeincorporacion.Text = DateTime.Now.ToString();
            calchoras();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            this.Close();
            ui_listaevaluados ui_Listaevaluados = new ui_listaevaluados();
            ui_Listaevaluados._FormPadre2 = this;
            ui_Listaevaluados.Activate();
            ui_Listaevaluados.BringToFront();
            ui_Listaevaluados.ShowDialog();
            ui_Listaevaluados.Dispose();
            toolStripButton2.Enabled = true;
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            string query = string.Empty;
            Funciones funciones = new Funciones();

            string idregistro1 = txtregistro.Text.Trim();
            string idempleado1 = txtcodempleado.Text.Trim();
            string nombres1 = txtNombres.Text.Trim();
            string niveleducativo1 = funciones.getValorComboBox(cmbNivelEducativo, 100);
            string tipodocumento1 = funciones.getValorComboBox(cmbTipoDocumento, 30);
            string dni1 = txtNroDoc.Text.Trim();
            string celular1 = txtCelular.Text.Trim();
            string telefono1 = txtTelFijo.Text.Trim();
            string nacionalidad1 = funciones.getValorComboBox(cmbNacionalidad, 30);
            string distrito1 = txtdistrito.Text.Trim();
            string provincia1 = txtprovincia.Text.Trim();
            string departamento1 = txtdepartamento.Text.Trim();
            string referencia1 = txtreferencia.Text.Trim();
            string fechanacimieto1 = txtFechaNac.Value.ToString("yyyy-MM-dd");
            string sexo1 = funciones.getValorComboBox(cmbSexo, 20);
            string estadocivil1 = funciones.getValorComboBox(cmbEstadoCivil, 30);
            string categoria1 = funciones.getValorComboBox(cmbCategoriaBrevete, 20);
            string licencia1 = txtNroLicenciaConductor.Text.Trim();
            string gerencia1 = funciones.getValorComboBox(cmbGerencia, 50);
            string unidorganizativa1 = funciones.getValorComboBox(cmbunidorg, 50);
            string posicion1 = funciones.getValorComboBox(cmbPosicion, 50);
            string sociedad1 = funciones.getValorComboBox(cmbsoc, 3);
            string nomsociedad1 = txtnomsoc.Text.Trim();
            string email1 = txtEmail.Text.Trim();
            string jefatura1 = txtjefatura.Text.Trim();
            string responsablerrhh1 = txtrrhh.Text.Trim();
            string nivelorganizacional1 = funciones.getValorComboBox(cmbnivorganizacional, 20);
            string sede1 = funciones.getValorComboBox(cmbsede, 15);
            string origen1 = funciones.getValorComboBox(cmborigen, 15);
            string reemplazode1 = txtreemplazo.Text.Trim();
            string tipoproceso1 = funciones.getValorComboBox(cmbtipoproceso, 20);
            string medioatencion1 = funciones.getValorComboBox(cmbmedioate, 20);
            string fuentepostulacion1 = txtfuentepostulacion.Text.Trim();
            string modalidad1 = funciones.getValorComboBox(cmbmodalidad, 20);
            string tipocontrato1 = funciones.getValorComboBox(cmbtipocontrato, 20);
            string vacantes1 = txtvacantes.Text.Trim();
            string cantevaluados1 = txtcantevaluados.Text.Trim();
            string estproceso1 = funciones.getValorComboBox(cmbestadoproceso, 20);
            string prioridad1 = funciones.getValorComboBox(cmbprioridad, 20);
            string proveedor1 = txtproveedor.Text.Trim();
            string estatusocupacion1 = funciones.getValorComboBox(cmbestatusocupacion, 20);
            string nrolong1 = txtnrolistenvi.Text.Trim();
            string satisfaccion1 = txtsatisfaccion.Text.Trim();
            string comentarios1 = txtcomentarios.Text.Trim();

            string carta;
            string inducciones;

            if (rdbcartaSi.Checked == true) { carta = "SI"; }
            else { carta = "NO"; }
            string cartaoferta1 = carta;

            if (rdbcartaSi.Checked == true) { inducciones = "SI"; }
            else { inducciones = "NO"; }
            string induccion1 = inducciones;

            string feinicioproceso1 = txtfeinicioproceso.Value.ToString("yyyy-MM-dd");
            string feestimacion1 = txtfeestimacion.Value.ToString("yyyy-MM-dd");
            string fecierre1 = txtfecierre.Value.ToString("yyyy-MM-dd");
            string feincorporacion1 = txtfeincorporacion.Value.ToString("yyyy-MM-dd");
            string Totaldias1 = txttotaldias.Text.Trim();

            query = "UPDATE Asistencia.dbo.gestiontalento SET " +
                            "idempleado = '" + @idempleado1 + "'," +
                            "nombres = '" + @nombres1 + "'," +
                            "niveleducativo = '" + @niveleducativo1 + "'," +
                            "tipodocumento = '" + @tipodocumento1 + "'," +
                            "dni = '" + @dni1 + "'," +
                            "celular = '" + @celular1 + "'," +
                            "telefono = '" + @telefono1 + "'," +
                            "nacionalidad = '" + @nacionalidad1 + "'," +
                            "distrito = '" + @distrito1 + "'," +
                            "provincia = '" + @provincia1 + "'," +
                            "departamento = '" + @departamento1 + "'," +
                            "referencia = '" + @referencia1 + "'," +
                            "fechanacimieto = '" + @fechanacimieto1 + "'," +
                            "sexo = '" + @sexo1 + "'," +
                            "estadocivil = '" + @estadocivil1 + "'," +
                            "categoria = '" + @categoria1 + "'," +
                            "licencia = '" + @licencia1 + "'," +
                            "gerencia = '" + @gerencia1 + "'," +
                            "unidorganizativa = '" + @unidorganizativa1 + "'," +
                            "posicion = '" + @posicion1 + "'," +
                            "sociedad = '" + @sociedad1 + "'," +
                            "nomsociedad = '" + @nomsociedad1 + "'," +
                            "email = '" + @email1 + "'," +
                            "jefatura = '" + @jefatura1 + "'," +
                            "responsablerrhh = '" + @responsablerrhh1 + "'," +
                            "nivelorganizacional = '" + @nivelorganizacional1 + "'," +
                            "sede = '" + @sede1 + "'," +
                            "origen = '" + @origen1 + "'," +
                            "reemplazode = '" + @reemplazode1 + "'," +
                            "tipoproceso = '" + @tipoproceso1 + "'," +
                            "medioatencion = '" + @medioatencion1 + "'," +
                            "fuentepostulacion = '" + @fuentepostulacion1 + "'," +
                            "modalidad = '" + @modalidad1 + "'," +
                            "tipocontrato = '" + @tipocontrato1 + "'," +
                            "vacantes = '" + vacantes1 + "'," +
                            "cantevaluados = '" + cantevaluados1 + "'," +
                            "estproceso = '" + @estproceso1 + "'," +
                            "prioridad = '" + @prioridad1 + "'," +
                            "proveedor = '" + @proveedor1 + "'," +
                            "estatusocupacion = '" + @estatusocupacion1 + "'," +
                            "nrolong = '" + nrolong1 + "'," +
                            "satisfaccion = '" + satisfaccion1 + "'," +
                            "comentarios = '" + comentarios1 + "'," +
                            "cartaoferta = '" + @cartaoferta1 + "'," +
                            "induccion = '" + @induccion1 + "'," +
                            "feinicioproceso = '" + @feinicioproceso1 + "'," +
                            "feestimacion = '" + @feestimacion1 + "'," +
                            "fecierre = '" + @fecierre1 + "'," +
                            "feincorporacion = '" + @feincorporacion1 + "'," +
                            "Totaldias = '" + Totaldias1 + "'" +
                            "WHERE idregistro = '" + @idregistro1 + "';";

            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();

            try
            {
                SqlCommand myCommand = new SqlCommand(query, conexion);
                myCommand.ExecuteNonQuery();
                myCommand.Dispose();
                MessageBox.Show("Informacion Actualizada con exito..", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            conexion.Close();

            this.Close();
            ui_listaevaluados l = new ui_listaevaluados();
            l.Show();

        }

        private void lblNombre_Click(object sender, EventArgs e)
        {

        }

        private void lblNombre_TextChanged(object sender, EventArgs e)
        {

            //lblNombre.Text = txtApPat.Text.Trim() + " " + txtApMat.Text.Trim() + " " + txtNombres.Text.Trim();

        }

        private void txtfeinicioproceso_ValueChanged(object sender, EventArgs e)
        {
            calchoras();
        }

        private void txtfecierre_ValueChanged(object sender, EventArgs e)
        {
            calchoras();
        }

        private void txtApPat_TextChanged(object sender, EventArgs e)
        {
            nombrecadena();
        }

        private void txtApMat_TextChanged(object sender, EventArgs e)
        {
            nombrecadena();
        }

        private void txtNombres_TextChanged(object sender, EventArgs e)
        {
            nombrecadena();
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            tabControlPer.Enabled=true;
            limpiarcampos();
            btnGuardar.Visible = true;
            btnNuevo.Enabled = false;
            toolStripSeparator2.Visible = true;
        }

        private void cmborigen_SelectedValueChanged(object sender, EventArgs e)
        {
            string origen = funciones.getValorComboBox(cmborigen, 5);
            if (origen == "NUEVO")
            {
                txtreemplazo.Text = "";
                txtreemplazo.Enabled = false;
            }
            else { txtreemplazo.Enabled = true; }
        }

        private void excel_Click(object sender, EventArgs e)
        {
            
            this.Close();
            if (this.Controls.Find("ui_atrac_selecc_cargar", true).Count() == 0)
            {
                ui_atrac_selecc_cargar ui_atrac_selecc_cargar = new ui_atrac_selecc_cargar();
                ui_atrac_selecc_cargar.Activate();
                ui_atrac_selecc_cargar.Show();
                ui_atrac_selecc_cargar.BringToFront();
            }
        }
    }
}
