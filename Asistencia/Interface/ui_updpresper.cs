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
    public partial class ui_updpresper : Form
    {
        string _operacion;
        string _validacion;
        string _tipoper;
        string _opedetalle;

        private Form FormPadre;

        public Form _FormPadre
        {
            get { return FormPadre; }
            set { FormPadre = value; }
        }

        private TextBox TextBoxActivo;

        public TextBox _TextBoxActivo
        {
            get { return TextBoxActivo; }
            set { TextBoxActivo = value; }
        }

        public ui_updpresper()
        {
            InitializeComponent();
        }

        private void txtCodigoInterno_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                GlobalVariables variables = new GlobalVariables();
                FiltrosMaestros filtros = new FiltrosMaestros();
                string idcia = variables.getValorCia();
                string idtipoper = this._tipoper;
                this._TextBoxActivo = txtCodigoInterno;
                string condicionAdicional = string.Empty;
                string cadenaBusqueda = string.Empty;
                filtros.filtrarPerPlan("ui_updpresper", this, txtCodigoInterno, idcia, idtipoper, "V", cadenaBusqueda, condicionAdicional);
            }
        }

        private void ui_calculaSaldo()
        {
            Funciones funciones = new Funciones();
            if (this._operacion.Equals("EDITAR"))
            {
                float impprestamo = float.Parse(txtImporte.Text);
                float impcargoabono = float.Parse(funciones.sumaColumnaDataGridView(dgvDetalle, 5));
                float impsaldo = (impprestamo + impcargoabono);
                txtImpPrestamo.Text = impprestamo.ToString();
                txtCargosAbonos.Text = impcargoabono.ToString();
                txtSaldo.Text = impsaldo.ToString();
            }
            else
            {
                txtImpPrestamo.Text = "0";
                txtCargosAbonos.Text = "0";
                txtSaldo.Text = "0";
            }

        }

        private void ui_updpresper_Load(object sender, EventArgs e)
        {
            ui_ActualizaComboBox();
        }

        private void tabPageEX1_Click(object sender, EventArgs e)
        {

        }

        public void ui_ActualizaComboBox()
        {

            MaesGen maesgen = new MaesGen();
            maesgen.listaDetMaesGen("015", cmbMoneda, "B");
            maesgen.listaDetMaesGen("032", cmbMotivo, "B");
            maesgen.listaDetMaesGen("033", cmbTipoDoc, "B");
            maesgen.listaDetMaesGen("034", cmbOpeCarAbo, "B");
      
        }

        internal void ui_newPresPer(string idtipoper)
        {
            GlobalVariables variables = new GlobalVariables();
            PresPer presper = new PresPer();
            this._operacion = "AGREGAR";
            this._validacion = "Q";
            this._tipoper = idtipoper;
            txtCodigoPrestamo.Text = presper.generaCodigoInterno(variables.getValorCia());
            txtCodigoInterno.Clear();
            txtDocIdent.Clear();
            txtNroDocIden.Clear();
            txtNombres.Clear();
            txtFecIniPerLab.Clear();
            txtFecha.Text = DateTime.Now.ToString("dd/MM/yyyy");
            txtImporte.Clear();
            txtCuota.Clear();
            txtNroDoc.Clear();
            txtComentarios.Clear();
            cmbMotivo.Text = string.Empty;
            cmbTipoDoc.Text = string.Empty;
            cmbMoneda.Text = string.Empty;
            chkSuspendido.Checked = false;
            tabPageEX2.Enabled = false;
            tabControlPres.SelectTab(0);
            txtCodigoInterno.Focus();

        }

        internal void ui_loadPresPer(string idpresper,string idperplan,string fecha,float importe,float cuota,string tipodocpres,string nrodocpres,string comen,string motivo,string mon,string suspendido,string idtipoper)
        {
            PerPlan perplan = new PerPlan();
            GlobalVariables variables = new GlobalVariables();
            MaesGen maesgen = new MaesGen();

            string idcia = variables.getValorCia();
            this._operacion = "EDITAR";
            this._validacion = "Q";
            this._tipoper = idtipoper;

            txtCodigoPrestamo.Text = idpresper;
            
            txtCodigoInterno.Text = idperplan;
            txtDocIdent.Text = perplan.ui_getDatosPerPlan(idcia,idperplan, "1");
            txtNroDocIden.Text = perplan.ui_getDatosPerPlan(idcia, idperplan, "2");
            txtNombres.Text = perplan.ui_getDatosPerPlan(idcia, idperplan, "3");
            txtFecIniPerLab.Text = perplan.ui_getDatosPerPlan(idcia, idperplan, "4");

            txtFecha.Text = fecha;
            txtImporte.Text = Convert.ToString(importe);
            txtCuota.Text = Convert.ToString(cuota);
            txtNroDoc.Text = nrodocpres;
            txtComentarios.Text = comen;

            if (suspendido.Equals("1"))
            {
                chkSuspendido.Checked = true;
            }
            else
            {
                chkSuspendido.Checked = false;
            }

            
            maesgen.consultaDetMaesGen("015", mon, cmbMoneda);
            maesgen.consultaDetMaesGen("032",motivo, cmbMotivo);
            maesgen.consultaDetMaesGen("033",tipodocpres ,cmbTipoDoc);
            ui_listaDetPresPer(idcia, idpresper);
            tabPageEX2.Enabled = true;
            tabControlPres.SelectTab(0);
            txtCodigoInterno.Focus();

        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
            ((ui_presper)FormPadre).btnActualizar.PerformClick();
            this.Close();
        }

        private void txtFecha_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                if (UtileriasFechas.IsDate(txtFecha.Text))
                {
                    e.Handled = true;
                    cmbMoneda.Focus();

                }
                else
                {
                    MessageBox.Show("Fecha no válida", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    e.Handled = true;
                    txtFecha.Focus();
                }
            }
        }

        private void cmbMotivo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                if (cmbMotivo.Text != String.Empty)
                {
                    Funciones funciones = new Funciones();
                    MaesGen maesgen = new MaesGen();
                    maesgen.validarDetMaesGen("032", cmbMotivo, cmbMotivo.Text);
                                        
                }
                e.Handled = true;
                cmbTipoDoc.Focus();

            }
        }

        private void cmbTipoDoc_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                if (cmbTipoDoc.Text != String.Empty)
                {
                    Funciones funciones = new Funciones();
                    MaesGen maesgen = new MaesGen();
                    maesgen.validarDetMaesGen("033", cmbTipoDoc, cmbTipoDoc.Text);

                }
                e.Handled = true;
                txtNroDoc.Focus();
        
            }
        }

        private void txtImporte_KeyPress(object sender, KeyPressEventArgs e)
        {

            if (e.KeyChar == '\r')
            {
                try
                {
                    float numero = float.Parse(txtImporte.Text);
                    e.Handled = true;
                    txtCuota.Focus();
                }
                catch (FormatException ex)
                {
                    MessageBox.Show(ex.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtImporte.Clear();
                    txtImporte.Focus();
                }

            }
           
        }

        private void txtCuota_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                try
                {
                    float numero = float.Parse(txtCuota.Text);
                    e.Handled = true;
                    cmbMotivo.Focus();
                }
                catch (FormatException ex)
                {
                    MessageBox.Show(ex.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtCuota.Clear();
                    txtCuota.Focus();
                }

            }

           
        }

        private void txtNroDoc_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                e.Handled = true;
                txtComentarios.Focus();
            }
        }

        private void cmbMotivo_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void cmbMoneda_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                if (cmbMoneda.Text != String.Empty)
                {
                    Funciones funciones = new Funciones();
                    MaesGen maesgen = new MaesGen();
                    maesgen.validarDetMaesGen("015", cmbMoneda, cmbMoneda.Text);

                }
                e.Handled = true;
                txtImporte.Focus();

            }
        }

        public void ui_validarDatos()
        {
            MaesGen maesgen = new MaesGen();
            Funciones funciones = new Funciones();
            GlobalVariables globalVariables = new GlobalVariables();

            string valorValida = "G";

            if (txtCodigoInterno.Text == string.Empty && valorValida == "G")
            {
                valorValida = "B";
                MessageBox.Show("No ha ingresado trabajador", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                tabControlPres.SelectTab(0);
                txtCodigoInterno.Focus();
            }


            if (UtileriasFechas.IsDate(txtFecha.Text) == false && valorValida == "G")
            {
                valorValida = "B";
                MessageBox.Show("Fecha no válida", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                tabControlPres.SelectTab(0);
                txtFecha.Focus();
            }

            if (cmbMoneda.Text == string.Empty && valorValida == "G")
            {
                valorValida = "B";
                MessageBox.Show("No ha seleccionado Moneda", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                tabControlPres.SelectTab(0);
                cmbMoneda.Focus();
            }

            if (cmbMoneda.Text != string.Empty && valorValida == "G")
            {
                string resultado = maesgen.verificaComboBoxMaesGen("015", cmbMoneda.Text.Trim());
                if (resultado.Trim() == string.Empty)
                {
                    valorValida = "B";
                    MessageBox.Show("Dato incorrecto en Moneda", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    tabControlPres.SelectTab(0);
                    cmbMoneda.Focus();
                }
            }
            
            if (cmbMotivo.Text == string.Empty && valorValida == "G")
            {
                valorValida = "B";
                MessageBox.Show("No ha seleccionado Motivo", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                tabControlPres.SelectTab(0);
                cmbMotivo.Focus();
            }

            if (cmbMotivo.Text != string.Empty && valorValida == "G")
            {
                string resultado = maesgen.verificaComboBoxMaesGen("032", cmbMotivo.Text.Trim());
                if (resultado.Trim() == string.Empty)
                {
                    valorValida = "B";
                    MessageBox.Show("Dato incorrecto en Motivo", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    tabControlPres.SelectTab(0);
                    cmbMotivo.Focus();
                }
            }


            if (cmbTipoDoc.Text == string.Empty && valorValida == "G")
            {
                valorValida = "B";
                MessageBox.Show("No ha seleccionado Tipo de Documento", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                tabControlPres.SelectTab(0);
                cmbTipoDoc.Focus();
            }

            if (cmbTipoDoc.Text != string.Empty && valorValida == "G")
            {
                string resultado = maesgen.verificaComboBoxMaesGen("033", cmbTipoDoc.Text.Trim());
                if (resultado.Trim() == string.Empty)
                {
                    valorValida = "B";
                    MessageBox.Show("Dato incorrecto en Tipo de Documento", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    tabControlPres.SelectTab(0);
                    cmbTipoDoc.Focus();
                }
            }

            if (txtNroDoc.Text == string.Empty && valorValida == "G")
            {
                valorValida = "B";
                MessageBox.Show("No ha ingresado Número de Documento", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                tabControlPres.SelectTab(0);
                txtNroDoc.Focus();
            }

            if (valorValida.Equals("G")) //GOOD 
            {
                this._validacion = "G";
                MessageBox.Show("Registro validado exitosamente", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                if (valorValida.Equals("B")) //BAD
                {
                    this._validacion = "B";

                }
                else
                {
                    this._validacion = "Q"; //QUESTION
                }
            }
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            try
            {
                ui_validarDatos();
                if (this._validacion.Equals("G"))
                {
                    Funciones funciones = new Funciones();
                    GlobalVariables variables = new GlobalVariables();
                    PresPer presper = new PresPer();
                    string idcia = variables.getValorCia();
                    string operacion = this._operacion;
                    string idpresper = txtCodigoPrestamo.Text.Trim();
                    string idperplan = txtCodigoInterno.Text.Trim();
                    string fecha = txtFecha.Text.Trim();
                    string mon = funciones.getValorComboBox(cmbMoneda, 4);
                    float importe = float.Parse(txtImporte.Text);
                    float cuota = float.Parse(txtCuota.Text);
                    string motivo = funciones.getValorComboBox(cmbMotivo, 4);
                    string tipodocpres = funciones.getValorComboBox(cmbTipoDoc, 4);
                    string nrodocpres = txtNroDoc.Text.Trim();
                    
                    string comen = txtComentarios.Text;

                    string suspendido;
                    
                    if (chkSuspendido.Checked)
                    {
                        suspendido = "1";
                    }
                    else
                    {
                        suspendido = "0";
                    }

                    presper.actualizarPresPer(operacion, idpresper, idcia, idperplan, fecha, importe, cuota, motivo, comen, mon, suspendido, tipodocpres, nrodocpres);
                    ui_listaDetPresPer(idcia, idpresper);
                    this._operacion= "EDITAR";
                    tabPageEX2.Enabled = true;

                }
            }
            catch (FormatException ex)
            {
                MessageBox.Show(ex.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtCodigoInterno_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                if (txtCodigoInterno.Text.Trim() != string.Empty)
                {
                    GlobalVariables variables = new GlobalVariables();
                    string idcia = variables.getValorCia();
                    string idperplan = txtCodigoInterno.Text.Trim();

                    PerPlan perplan = new PerPlan();
                    string codigoInterno = perplan.ui_getDatosPerPlan(idcia, idperplan, "0");
                    if (codigoInterno == string.Empty)
                    {
                        MessageBox.Show("Código no registrado del trabajador", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txtCodigoInterno.Clear();
                        txtDocIdent.Clear();
                        txtNroDocIden.Clear();
                        txtNombres.Clear();
                        txtFecIniPerLab.Clear();
                        txtCodigoInterno.Focus();
                    }
                    else
                    {
                        txtCodigoInterno.Text = perplan.ui_getDatosPerPlan(idcia, idperplan, "0");
                        txtDocIdent.Text = perplan.ui_getDatosPerPlan(idcia, idperplan, "1");
                        txtNroDocIden.Text = perplan.ui_getDatosPerPlan(idcia, idperplan, "2");
                        txtNombres.Text = perplan.ui_getDatosPerPlan(idcia, idperplan, "3");
                        txtFecIniPerLab.Text = perplan.ui_getDatosPerPlan(idcia, idperplan, "4");
                        e.Handled = true;
                        txtFecha.Focus();
                    }

                }
                else
                {

                    txtCodigoInterno.Clear();
                    txtDocIdent.Clear();
                    txtNroDocIden.Clear();
                    txtNombres.Clear();
                    txtFecIniPerLab.Clear();
                    txtCodigoInterno.Focus();
                }



            }
        }

        private void txtImpCarAbo_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }

        public void ui_listaDetPresPer(string idcia, string idpresper)
        {
            Funciones funciones = new Funciones();
            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();
            string query =  " Select iddetpresper,fecha,tipo,importe,comen,";
            query = query + " CASE tipo WHEN '+' THEN importe WHEN '-' THEN importe*-1 END as valor,";
            query = query + " idpresper,idcia from view_detpresper  where idcia='" + @idcia + "' ";
            query = query + " and idpresper='" + @idpresper + "' order by fecha asc,iddetpresper asc;";
            try
            {
                using (SqlDataAdapter myDataAdapter = new SqlDataAdapter(query, conexion))
                {
                    DataSet myDataSet = new DataSet();
                    myDataAdapter.Fill(myDataSet, "tblDetPresPer");
                    funciones.formatearDataGridView(dgvDetalle);

                    dgvDetalle.DataSource = myDataSet.Tables["tblDetPresPer"];
                    dgvDetalle.Columns[0].HeaderText = "Código";
                    dgvDetalle.Columns[1].HeaderText = "Fecha";
                    dgvDetalle.Columns[2].HeaderText = "Operación";
                    dgvDetalle.Columns[3].HeaderText = "Importe";
                    dgvDetalle.Columns[4].HeaderText = "Comentario";

                    dgvDetalle.Columns["valor"].Visible = false;
                    dgvDetalle.Columns["idpresper"].Visible = false;
                    dgvDetalle.Columns["idcia"].Visible = false;

                    dgvDetalle.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    dgvDetalle.Columns[3].DefaultCellStyle.Format = "###,###.##";
            
                    dgvDetalle.Columns[0].Width = 60;
                    dgvDetalle.Columns[1].Width = 75;
                    dgvDetalle.Columns[2].Width = 60;
                    dgvDetalle.Columns[3].Width = 75;
                    dgvDetalle.Columns[4].Width = 300;

                }
                ui_calculaSaldo();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }

            
            conexion.Close();


        }

        private void btnNuevoEstAne_Click(object sender, EventArgs e)
        {
            DetPresPer detpresper = new DetPresPer();
            GlobalVariables variables=new GlobalVariables();
            string idpresper=txtCodigoPrestamo.Text.Trim();
            this._opedetalle = "AGREGAR";
            txtCodigoCarAbo.Enabled = false;
            cmbOpeCarAbo.Enabled = true;
            txtFechaCarAbo.Enabled = true;
            txtImpCarAbo.Enabled = true;
            txtComenCarAbo.Enabled = true;
            txtCodigoCarAbo.Text = detpresper.generaCodigo(variables.getValorCia(), idpresper);
            cmbOpeCarAbo.Text = string.Empty;
            txtFechaCarAbo.Text = DateTime.Now.ToString("dd/MM/yyyy"); 
            txtImpCarAbo.Clear();
            txtComenCarAbo.Clear();
            cmbOpeCarAbo.Focus();
        }

        private void ui_limpiarDetPresPer()
        {
            this._opedetalle = "";
            txtCodigoCarAbo.Enabled = false;
            cmbOpeCarAbo.Enabled = false;
            txtFechaCarAbo.Enabled = false;
            txtImpCarAbo.Enabled = false;
            txtComenCarAbo.Enabled = false;
            txtCodigoCarAbo.Clear();
            cmbOpeCarAbo.Text = string.Empty;
            txtFechaCarAbo.Clear();
            txtImpCarAbo.Clear();
            txtComenCarAbo.Clear();
        }

        private void btnEditarEstAne_Click(object sender, EventArgs e)
        {
            Int32 selectedCellCount = dgvDetalle.GetCellCount(DataGridViewElementStates.Selected);
            if (selectedCellCount > 0)
            {
                MaesGen maesgen = new MaesGen();
                this._opedetalle = "EDITAR";
                string iddetpresper = dgvDetalle.Rows[dgvDetalle.SelectedCells[0].RowIndex].Cells[0].Value.ToString();
                string fecha=dgvDetalle.Rows[dgvDetalle.SelectedCells[0].RowIndex].Cells[1].Value.ToString();
                string tipo=dgvDetalle.Rows[dgvDetalle.SelectedCells[0].RowIndex].Cells[2].Value.ToString();
                float importe=float.Parse(dgvDetalle.Rows[dgvDetalle.SelectedCells[0].RowIndex].Cells[3].Value.ToString());
                string comen=dgvDetalle.Rows[dgvDetalle.SelectedCells[0].RowIndex].Cells[4].Value.ToString();
                string idpresper=dgvDetalle.Rows[dgvDetalle.SelectedCells[0].RowIndex].Cells[6].Value.ToString();
                string idcia = dgvDetalle.Rows[dgvDetalle.SelectedCells[0].RowIndex].Cells[7].Value.ToString();
                if (iddetpresper != string.Empty)
                {
                    txtCodigoCarAbo.Text = iddetpresper;
                    txtFechaCarAbo.Text = fecha;
                    txtImpCarAbo.Text = importe.ToString();
                    txtComenCarAbo.Text = comen;
                    maesgen.consultaDetMaesGen("034", tipo, cmbOpeCarAbo);
                    txtCodigoCarAbo.Enabled = false;
                    txtFechaCarAbo.Enabled = true;
                    txtImpCarAbo.Enabled = true;
                    txtComenCarAbo.Enabled = true;
                    cmbOpeCarAbo.Enabled = true;
                    cmbOpeCarAbo.Focus();
                }
                else
                {
                    MessageBox.Show("Item no es editable por esta ventana, ir a Parte de Planilla", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }

            }
            else
            {
                MessageBox.Show("No ha seleccionado registro a Editar", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void btnGrabarEstAne_Click(object sender, EventArgs e)
        {
            if (this._operacion.Equals("EDITAR"))
            {

                try
                {
                    Funciones funciones = new Funciones();
                    MaesGen maesgen = new MaesGen();
                    DetPresPer detpresper = new DetPresPer();
                    GlobalVariables variables=new GlobalVariables();
                    string operacion = this._opedetalle;
                    string iddetpresper = txtCodigoCarAbo.Text.Trim();
                    string idpresper=txtCodigoPrestamo.Text.Trim();
                    string idcia=variables.getValorCia();
                    string tipo = funciones.getValorComboBox(cmbOpeCarAbo,4);
                    string fecha = txtFechaCarAbo.Text;
                    float  importe = float.Parse(txtImpCarAbo.Text);
                    string comen = txtComenCarAbo.Text.Trim();
                                      
                    string valorValida = "G";

                    if (txtCodigoCarAbo.Text == string.Empty && valorValida == "G")
                    {
                        valorValida = "B";
                        MessageBox.Show("No ha ingresado Código", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txtCodigoCarAbo.Focus();
                    }

                    if (cmbOpeCarAbo.Text == string.Empty && valorValida == "G")
                    {
                        valorValida = "B";
                        MessageBox.Show("No ha seleccionado Operación a realizar", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        cmbOpeCarAbo.Focus();
                    }

                    if (cmbOpeCarAbo.Text != string.Empty && valorValida == "G")
                    {

                        string resultado = maesgen.verificaComboBoxMaesGen("034", cmbOpeCarAbo.Text.Trim());
                        if (resultado.Trim() == string.Empty)
                        {
                            valorValida = "B";
                            MessageBox.Show("Dato incorrecto en Operación", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            tabControlPres.SelectTab(1);
                            cmbOpeCarAbo.Focus();
                        }
                    }

                    if (UtileriasFechas.IsDate(txtFechaCarAbo.Text) == false && valorValida == "G")
                    {
                        valorValida = "B";
                        MessageBox.Show("Fecha no válida", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        tabControlPres.SelectTab(1);
                        txtFechaCarAbo.Focus();
                    }
                                   
                    if (valorValida.Equals("G"))
                    {
                        detpresper.actualizarDetPresPer(operacion, iddetpresper, idpresper, idcia, tipo, importe, fecha, comen);
                        ui_listaDetPresPer(idcia, idpresper);
                        ui_limpiarDetPresPer();
                    }
                }
                catch (FormatException ex)
                {
                    MessageBox.Show(ex.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Primero debe de grabar la información del Préstamo Personal", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cmbOpeCarAbo_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void txtFechaCarAbo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                if (UtileriasFechas.IsDate(txtFechaCarAbo.Text))
                {
                    e.Handled = true;
                    txtImpCarAbo.Focus();

                }
                else
                {
                    MessageBox.Show("Fecha no válida", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    e.Handled = true;
                    txtFechaCarAbo.Focus();
                }
            }
        }

        private void txtImpCarAbo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                try
                {
                    float numero = float.Parse(txtImpCarAbo.Text);
                    e.Handled = true;
                    txtComenCarAbo.Focus();
                }
                catch (FormatException ex)
                {
                    MessageBox.Show(ex.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtImpCarAbo.Clear();
                    txtImpCarAbo.Focus();
                }

            }
        }

        private void cmbOpeCarAbo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                if (cmbOpeCarAbo.Text != String.Empty)
                {
                    Funciones funciones = new Funciones();
                    MaesGen maesgen = new MaesGen();
                    maesgen.validarDetMaesGen("034", cmbOpeCarAbo, cmbOpeCarAbo.Text);

                }
                e.Handled = true;
                txtFechaCarAbo.Focus();

            }
        }

        private void tabPageEX2_Click(object sender, EventArgs e)
        {

        }

        private void btnEliminarEstAne_Click(object sender, EventArgs e)
        {
            Int32 selectedCellCount = dgvDetalle.GetCellCount(DataGridViewElementStates.Selected);
            if (selectedCellCount > 0)
            {
                string iddetpresper = dgvDetalle.Rows[dgvDetalle.SelectedCells[0].RowIndex].Cells[0].Value.ToString();
                string idpresper = dgvDetalle.Rows[dgvDetalle.SelectedCells[0].RowIndex].Cells[6].Value.ToString();
                string idcia = dgvDetalle.Rows[dgvDetalle.SelectedCells[0].RowIndex].Cells[7].Value.ToString();
                if (iddetpresper != string.Empty)
                {
                    DialogResult resultado = MessageBox.Show("¿Desea eliminar el Item N° " + iddetpresper + "?",
                    "Consulta Importante",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);
                    if (resultado == DialogResult.Yes)
                    {
                        DetPresPer detpresper = new DetPresPer();
                        detpresper.eliminarDetPresPer(iddetpresper, idpresper, idcia);
                        ui_listaDetPresPer(idcia, idpresper);
                    }
                }
                else
                {
                    MessageBox.Show("Item no se puede eliminar por esta ventana, ir a Parte de Planilla", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }

            }
            else
            {
                MessageBox.Show("No ha seleccionado registro a Eliminar", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            Exporta exporta = new Exporta();
            exporta.Excel_FromDataGridView(dgvDetalle);
        }

        private void btnPdf_Click(object sender, EventArgs e)
        {
            Exporta exporta = new Exporta();
            exporta.Pdf_FromDataGridView(dgvDetalle,2);
        }
    }
}