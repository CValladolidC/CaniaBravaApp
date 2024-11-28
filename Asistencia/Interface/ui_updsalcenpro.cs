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
    public partial class ui_updsalcenpro : ui_form
    {
        string _operacion;
        string _codcia;
        string _codalma;
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


        public ui_updsalcenpro()
        {
            InitializeComponent();
        }

        private void ui_updsalcenpro_Load(object sender, EventArgs e)
        {
       
        }

        public void ui_ActualizaComboBox()
        {

            MaesGen maesgen = new MaesGen();
            Funciones funciones = new Funciones();
            string query;
            string tipomov = "PS";
            string codalma = this._codalma;
            string codcia = this._codcia;
            query = "SELECT codmov as clave,desmov as descripcion FROM tipomov WHERE ";
            query = query + " tipomov='" + @tipomov + "' and salcenpro='SI' and estado='V';";
            funciones.listaComboBox(query, cmbMov, "");
            query = "SELECT codcencos as clave,descencos as descripcion FROM cencos WHERE codcia='" + @codcia + "' ";
            query = query + " and codalma='" + @codalma + "' and estado='V';";
            funciones.listaComboBox(query, cmbCenCos, "");
     
        }

               
        public void setData(string codcia, string codalma, string desalmacen)
        {
            this._codcia = codcia;
            this._codalma = codalma;
            this.Text = "Salida de Productos por Centro Productivo - " + desalmacen;
        }
        
        public void agregar()
        {

            GlobalVariables variables = new GlobalVariables();
            this._operacion = "AGREGAR";
            this._finaliza = "N";
            cmbTipo.Text = "PS  PARTE DE SALIDA";
            txtNumDoc.Clear();
            txtFecha.Text = DateTime.Now.ToString("dd/MM/yyyy");
           
            txtNomRec.Clear();
            cmbCenCos.Text = "";
            txtGlosa1.Clear();
            txtGlosa2.Clear();
            txtGlosa3.Clear();
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
            query = "SELECT * FROM almovc where codcia='" + @codcia + "' and alma='" + @alma + "' and td='" + @td + "' and numdoc='" + @numdoc + "'; ";

            try
            {
                SqlCommand myCommand = new SqlCommand(query, conexion);
                SqlDataReader myReader = myCommand.ExecuteReader();

                if (myReader.Read())
                {
                    Funciones funciones = new Funciones();
                    MaesGen maesgen = new MaesGen();

                    if (myReader.GetString(myReader.GetOrdinal("td")).Equals("PE"))
                    {
                        cmbTipo.Text = "PE  PARTE DE ENTRADA";
                    }
                    else
                    {
                        cmbTipo.Text = "PS  PARTE DE SALIDA";
                    }

                    txtNumDoc.Text = myReader.GetString(myReader.GetOrdinal("numdoc"));
                    txtFecha.Text = myReader.GetString(myReader.GetOrdinal("fecdoc"));
                    query = " SELECT codmov as clave,desmov as descripcion FROM tipomov WHERE ";
                    query = query + " tipomov='" + @myReader.GetString(myReader.GetOrdinal("td")) + "' and codmov='" + @myReader.GetString(myReader.GetOrdinal("codmov")) + "';";
                    funciones.consultaComboBox(query, cmbMov);
                    txtNomRec.Text = myReader.GetString(myReader.GetOrdinal("nomrec"));
                    query = "SELECT codcencos as clave,descencos as descripcion FROM cencos WHERE codcia='" + @codcia + "' and ";
                    query = query + " codalma='" + @myReader.GetString(myReader.GetOrdinal("alma")) + "' and ";
                    query = query + " codcencos='" + @myReader.GetString(myReader.GetOrdinal("cencos")) + "';";
                    funciones.consultaComboBox(query, cmbCenCos);
                    txtGlosa1.Text = myReader.GetString(myReader.GetOrdinal("glosa1"));
                    txtGlosa2.Text = myReader.GetString(myReader.GetOrdinal("glosa2"));
                    txtGlosa3.Text = myReader.GetString(myReader.GetOrdinal("glosa3"));
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

                }
                
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
            Funciones funciones = new Funciones();
            string td = funciones.getValorComboBox(cmbTipo, 2);
            string codmov = funciones.getValorComboBox(cmbMov, 2);
            string valorValida = validaOpciones();
            if (valorValida.Equals("G"))
            {
                try
                {
                    AlmovC almovc = new AlmovC();
                    GlobalVariables variables = new GlobalVariables();
                    string operacion = this._operacion;
                    string codcia = this._codcia;
                    string alma = this._codalma;
                    string fecdoc = txtFecha.Text;
                    string rftdoc = "";
                    string rfndoc = "";
                    string secsoli="";
                    string persoli = "";
                    string rfnsoli = "";
                    string codpro = "";
                    string rfalma = "";
                    string codclie = "";
                    string nomrec = txtNomRec.Text;
                    string cencos = funciones.getValorComboBox(cmbCenCos, 2);
                    string glosa1 = txtGlosa1.Text.Trim();
                    string glosa2 = txtGlosa2.Text.Trim();
                    string glosa3 = txtGlosa3.Text.Trim();
                    string situa = txtEstado.Text.Substring(0, 1);
                    string fcrea = txtFecCrea.Text;
                    string fmod = DateTime.Now.ToString("dd/MM/yyyy");
                    string usuario = variables.getValorUsr();
                    
                    if (operacion.Equals("AGREGAR"))
                    {
                        txtNumDoc.Text = almovc.genCodAlmov(codcia, alma, td);
                    }
                    string numdoc = txtNumDoc.Text;
                    
                    almovc.updAlmovC(operacion,codcia,alma,td,numdoc,fecdoc,codmov,situa,rftdoc,rfndoc,secsoli,persoli,rfnsoli,nomrec,
                        codpro,cencos,rfalma,glosa1,glosa2,glosa3,codclie,fcrea,fmod,usuario,"PSCP");

                    MessageBox.Show("Información registrada correctamente", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ((ui_salcenpro)FormPadre).btnActualizar.PerformClick();
                    
                    this._finaliza = "S";
                    if (operacion.Equals("AGREGAR"))
                    {
                        this._operacion = "EDITAR";
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

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Close();
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
                    e.Handled = true;
                    SendKeys.Send("{TAB}");
                }


            }
        }



        private void cmbCenCos_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(Keys.Enter))
            {
                if (cmbCenCos.Text != String.Empty)
                {
                    Funciones funciones = new Funciones();
                    string codcia = this._codcia;
                    string codalma = this._codalma;
                    string codcencos = funciones.getValorComboBox(cmbCenCos, 2);
                    string query = "SELECT codcencos as clave,descencos as descripcion FROM cencos ";
                    query = query + "WHERE codcia='" + @codcia + "' and codalma='" + @codalma + "' and codcencos='" + @codcencos + "';";
                    funciones.validarCombobox(query, cmbCenCos);
                }
                e.Handled = true;
                SendKeys.Send("{TAB}");

            }
        }

        private string validaOpciones()
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
                string tipomov = funciones.getValorComboBox(cmbTipo, 2);
                string codmov = funciones.getValorComboBox(cmbMov, 2);
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

            if (txtNomRec.Text == string.Empty && valorValida == "G")
            {
                valorValida = "B";
                MessageBox.Show("No ha ingresado Nombre del Receptor", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                tabControl.SelectTab(0);
                txtNomRec.Focus();
            }

            if (cmbCenCos.Text == string.Empty  && valorValida == "G")
            {
                valorValida = "B";
                MessageBox.Show("No ha seleccionado Centro de Costo", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                tabControl.SelectTab(0);
                cmbCenCos.Focus();
            }


            if (cmbCenCos.Text != string.Empty && valorValida == "G")
            {
                string codalma = this._codalma;
                string codcencos = funciones.getValorComboBox(cmbCenCos, 2);
                query = "SELECT codcencos as clave,descencos as descripcion FROM cencos WHERE ";
                query = query + " codcia='" + @codcia + "' and codalma='" + @codalma + "' and ";
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

            if (txtGlosa1.Text == string.Empty && valorValida == "G")
            {
                valorValida = "B";
                MessageBox.Show("No ha ingresado Glosa", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                tabControl.SelectTab(0);
                txtGlosa1.Focus();
            }

            return valorValida;


        }


        public void ui_listaItem()
        {
            Funciones funciones = new Funciones();
            string codcia = this._codcia;
            string alma = this._codalma;
            string td = funciones.getValorComboBox(cmbTipo, 2);
            string numdoc = txtNumDoc.Text.Trim();
            string query = string.Empty;

            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();

            query = " select A.item,A.codarti,B.desarti,A.lote,B.unidad,A.cantidad,C.descenpro,A.codseccion,A.glosana1 ";
            query = query + " from almovd A left join alarti B on A.codcia=B.codcia and A.codarti = B.codarti";
            query = query + " left join cenpro C on A.codcia=C.codcia and A.codcenpro=C.codcenpro "; 
            query = query + " where A.codcia='" + @codcia + "' and A.alma='" + @alma + "' ";
            query = query + " and A.td='" + @td + "' and A.numdoc='" + @numdoc + "' ";
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
                    dgvData.Columns[6].HeaderText = "Centro Productivo";
                    dgvData.Columns[7].HeaderText = "Sección";
                    dgvData.Columns[8].HeaderText = "Glosa";

                    if (dgvData.Rows.Count > 6)
                    {
                        dgvData.CurrentCell = dgvData.Rows[dgvData.Rows.Count - 1].Cells[0];
                    }

                    dgvData.Columns[0].Width = 30;
                    dgvData.Columns[1].Width = 60;
                    dgvData.Columns[2].Width = 200;
                    dgvData.Columns[3].Width = 100;
                    dgvData.Columns[4].Width = 50;
                    dgvData.Columns[5].Width = 60;
                    dgvData.Columns[6].Width = 150;
                    dgvData.Columns[7].Width = 60;
                    dgvData.Columns[8].Width = 150;


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
                    string query = "SELECT codmov as clave,desmov as descripcion FROM tipomov WHERE tipomov='" + @tipomov + "' and salcenpro='SI' and estado='V';";
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
            string query = "SELECT codmov as clave,desmov as descripcion FROM tipomov WHERE tipomov='" + @tipomov + "' and salcenpro='SI' and estado='V';";
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

       


        private void txtNomRec_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(Keys.Enter))
            {
                e.Handled = true;
                SendKeys.Send("{TAB}");
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

       
        private void cmbMov_SelectedIndexChanged(object sender, EventArgs e)
        {

            Funciones funciones = new Funciones();
            string tipomov = funciones.getValorComboBox(cmbTipo, 2);
            string codmov = funciones.getValorComboBox(cmbMov, 2);
            string query = "SELECT codmov as clave,desmov as descripcion FROM tipomov ";
            query = query + "WHERE tipomov='" + @tipomov + "' and codmov='" + @codmov + "';";
            funciones.validarCombobox(query, cmbMov);

        }

        private void txtProveedor_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }



        private void btnFinaliza_Click(object sender, EventArgs e)
        {
            string situa = txtEstado.Text.Substring(0, 1);
            if (situa.Equals("V"))
            {
                Funciones funciones = new Funciones();
                string td = funciones.getValorComboBox(cmbTipo, 2);
                string codmov = funciones.getValorComboBox(cmbMov, 2);
                string valorValida = validaOpciones();
                if (valorValida.Equals("G"))
                {
                    try
                    {
                        if (this._finaliza.Equals("S"))
                        {
                            AlmovC almovc = new AlmovC();
                            GlobalVariables variables = new GlobalVariables();
                            string codcia = this._codcia;
                            string alma = this._codalma;
                            string fmod = DateTime.Now.ToString("dd/MM/yyyy");
                            string usuario = variables.getValorUsr();
                            string numdoc = txtNumDoc.Text;
                            almovc.updFinAlmovC(codcia, alma, td, numdoc, fmod, usuario);
                            txtEstado.Text = "FINALIZADO";
                            ((ui_salcenpro)FormPadre).btnActualizar.PerformClick();
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

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            if (this._operacion.Equals("EDITAR"))
            {

                ui_almovdpsmulticenpro ui_almovd = new ui_almovdpsmulticenpro();
                ui_almovd._FormPadre = this;
                ui_almovd._formpadre = "ui_updsalcenpro";
                ui_almovd._codcia = this._codcia;
                ui_almovd._alma = this._codalma;
                ui_almovd._td = cmbTipo.Text;
                ui_almovd._numdoc = txtNumDoc.Text.Trim();
                ui_almovd.ui_ActualizaComboBox();
                ui_almovd.Activate();
                ui_almovd.BringToFront();
                ui_almovd.agregar();
                ui_almovd.ShowDialog();
                ui_almovd.Dispose();


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
            Funciones funciones = new Funciones();
            Int32 selectedCellCount = dgvData.GetCellCount(DataGridViewElementStates.Selected);
            if (selectedCellCount > 0)
            {
                string codcia = this._codcia;
                string alma = this._codalma;
                string tipo = funciones.getValorComboBox(cmbTipo, 2);
                string td = cmbTipo.Text;
                string numdoc = txtNumDoc.Text.Trim();
                string item = dgvData.Rows[dgvData.SelectedCells[1].RowIndex].Cells[0].Value.ToString();

                ui_almovdpscenpro ui_almovd = new ui_almovdpscenpro();
                ui_almovd._FormPadre = this;
                ui_almovd._codcia = codcia;
                ui_almovd._alma = alma;
                ui_almovd._numdoc = numdoc;
                ui_almovd._td = td;
                ui_almovd.ui_ActualizaComboBox();
                ui_almovd.Activate();
                ui_almovd.BringToFront();
                ui_almovd.editar(item);
                ui_almovd.ShowDialog();
                ui_almovd.Dispose();

            }
            else
            {
                MessageBox.Show("No ha seleccionado registro a Editar", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            Funciones funciones = new Funciones();
            Int32 selectedCellCount =
            dgvData.GetCellCount(DataGridViewElementStates.Selected);
            if (selectedCellCount > 0)
            {
                string codcia = this._codcia;
                string alma = this._codalma;
                string td = funciones.getValorComboBox(cmbTipo, 2);
                string numdoc = txtNumDoc.Text.Trim();
                string item = dgvData.Rows[dgvData.SelectedCells[1].RowIndex].Cells[0].Value.ToString();
                DialogResult resultado = MessageBox.Show("¿Desea eliminar el Item " + @item + "?",
                "Consulta Importante",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);
                if (resultado == DialogResult.Yes)
                {
                    AlmovD almovd = new AlmovD();
                    almovd.delAlmovD(codcia, alma, td, numdoc, item);
                    ui_listaItem();
                }
            }
            else
            {
                MessageBox.Show("No ha seleccionado registro a Eliminar", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            Funciones funciones = new Funciones();
            string codcia = this._codcia;
            string alma = this._codalma;
            string td = funciones.getValorComboBox(cmbTipo, 2);
            string numdoc = txtNumDoc.Text.Trim();
            string situa = txtEstado.Text.Substring(0, 1);
            string mensaje = string.Empty;
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
                if (situa.Equals("V"))
                {
                    mensaje = "No se puede imprimir Parte no Finalizado";

                }
                else
                {
                    mensaje = "No se puede imprimir Parte Anulado";
                }
                MessageBox.Show(mensaje, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }

    }
}
