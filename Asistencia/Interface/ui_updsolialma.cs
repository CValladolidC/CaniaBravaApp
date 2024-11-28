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
    public partial class ui_updsolialma : Form
    {
        Funciones funciones = new Funciones();
        string _operacion;
        string _codcia;
        string _alma;
        string _secsoli;
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

        public ui_updsolialma()
        {
            InitializeComponent();

            if (funciones.VersionAssembly()) Application.ExitThread();
        }

        public void ui_ActualizaComboBox()
        {
            string secsoli = this._secsoli;
            string codcia = this._codcia;
            string query = "SELECT codsoli as clave,dessoli as descripcion FROM solicita ";
            //query = query + " WHERE codcia='" + @codcia + "' and secsoli='" + @secsoli + "' ;";
            funciones.listaComboBox(query, cmbSolicitante, "");
        }

        public void setData(string codcia, string alma, string secsoli, string desalmacen, string desseccion)
        {
            this._alma = alma;
            this._codcia = codcia;
            this._secsoli = secsoli;
            this.Text = "Solicitud - Pedido de Insumos";
            txtTitulo.Text = this.Text + " (" + DateTime.Now.ToString("dd/MM/yyyy") + ")";
        }

        public void agregar()
        {
            GlobalVariables variables = new GlobalVariables();
            this._operacion = "AGREGAR";
            this._finaliza = "N";
            txtFecha.Text = DateTime.Now.ToString("yyyy-MM-dd");
            txtHora.Text = DateTime.Now.ToString("hh:mm:ss");
            //txtTitulo.Clear();
            cmbSolicitante.Text = "";
            txtDscProveedor.Clear();
            txtGlosa1.Clear();
            txtGlosa2.Clear();
            txtGlosa3.Clear();
            txtEstado.Text = "VIGENTE";
            txtUsuario.Text = variables.getValorUsr();
            tabPageEX2.Enabled = false;
            tabControl.SelectTab(0);
            txtTitulo.Focus();
        }

        public void editar(string codcia, string alma, string secsoli, string solalma)
        {
            string query;
            this._operacion = "EDITAR";
            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();
            query = " SELECT * FROM solalmac where solalma='" + @solalma + "'and alma='" + @alma + "' ";
            //query += "and codcia='" + @codcia + "' and secsoli='" + @secsoli + "' ; ";
            try
            {
                SqlCommand myCommand = new SqlCommand(query, conexion);
                SqlDataReader myReader = myCommand.ExecuteReader();

                if (myReader.Read())
                {
                    txtCodigo.Text = myReader.GetString(myReader.GetOrdinal("solalma"));
                    txtFecha.Text = myReader.GetDateTime(myReader.GetOrdinal("fecha")).ToString("yyyy-MM-dd");
                    txtHora.Text = myReader.GetString(myReader.GetOrdinal("hora"));
                    txtTitulo.Text = myReader.GetString(myReader.GetOrdinal("titulo"));
                    query = "SELECT codsoli as clave,dessoli as descripcion FROM solicita ";
                    query += "WHERE /*codcia='" + @codcia + "' and secsoli='" + @myReader.GetString(myReader.GetOrdinal("secsoli")) + "' and*/ codsoli='" + @myReader.GetString(myReader.GetOrdinal("codsoli")) + "';";
                    //funciones.validarCombobox(query, cmbSolicitante);
                    txtProveedor.Text = myReader.GetString(myReader.GetOrdinal("codsoli"));
                    txtDscProveedor.Text = myReader.GetString(myReader.GetOrdinal("nomrec"));
                    Provee provee = new Provee();
                    txtRucProvee.Text = provee.ui_getDatos(txtProveedor.Text, "RUC");
                    txtGlosa1.Text = myReader.GetString(myReader.GetOrdinal("dessoli1"));
                    txtGlosa2.Text = myReader.GetString(myReader.GetOrdinal("dessoli2"));
                    txtGlosa3.Text = myReader.GetString(myReader.GetOrdinal("dessoli3"));
                    txtUsuario.Text = myReader.GetString(myReader.GetOrdinal("usuario"));
                    if (myReader.GetString(myReader.GetOrdinal("estado")).Equals("V"))
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
                        txtEstado.Text = "FINALIZADO";
                        this._finaliza = "S";
                        btnAceptar.Enabled = false;
                        btnFinaliza.Enabled = false;
                        btnNuevo.Enabled = false;
                        btnEditar.Enabled = false;
                        btnEliminar.Enabled = false;
                    }

                }
                ui_listaItem();
                tabPageEX2.Enabled = true;
                tabControl.SelectTab(0);
                txtTitulo.Focus();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            conexion.Close();
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            string alma = this._alma;
            string codcia = this._codcia;
            string secsoli = this._secsoli;
            string valorValida = valida();
            if (valorValida.Equals("G"))
            {
                try
                {
                    SolAlma solalma = new SolAlma();
                    GlobalVariables variables = new GlobalVariables();
                    string operacion = this._operacion;
                    string fecha = txtFecha.Text;
                    string hora = txtHora.Text;
                    string titulo = txtTitulo.Text.Trim();
                    string codsoli = txtProveedor.Text;//funciones.getValorComboBox(cmbSolicitante, 2);
                    string nomrec = txtDscProveedor.Text;
                    string dessoli1 = txtGlosa1.Text.Trim();
                    string dessoli2 = txtGlosa2.Text.Trim();
                    string dessoli3 = txtGlosa3.Text.Trim();
                    string estado = txtEstado.Text.Substring(0, 1);
                    string usuario = variables.getValorUsr();
                    if (operacion.Equals("AGREGAR"))
                    {
                        txtCodigo.Text = solalma.genSolAlma(codcia, alma, secsoli);
                    }
                    string codigo = txtCodigo.Text;
                    solalma.updSolAlma(operacion, codcia, alma, secsoli, codigo, fecha, hora, codsoli, nomrec, dessoli1, dessoli2, dessoli3, usuario, estado, titulo);
                    MessageBox.Show("Información registrada correctamente", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ((ui_solialma)FormPadre).btnActualizar.PerformClick();
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

        private void cmbSolicitante_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(Keys.Enter))
            {
                if (cmbSolicitante.Text != String.Empty)
                {
                    string secsoli = this._secsoli;
                    string codcia = this._codcia;
                    string codsoli = funciones.getValorComboBox(cmbSolicitante, 2);
                    string query = "SELECT codsoli as clave,dessoli as descripcion FROM solicita ";
                    //query = query + "WHERE codcia='" + @codcia + "' and secsoli='" + @secsoli + "' and codsoli='" + @codsoli + "';";
                    funciones.validarCombobox(query, cmbSolicitante);
                }
                e.Handled = true;
                SendKeys.Send("{TAB}");
            }
        }

        private string valida()
        {
            string query;
            string valorValida = "G";
            MaesGen maesgen = new MaesGen();

            if (txtTitulo.Text == string.Empty && valorValida == "G")
            {
                valorValida = "B";
                MessageBox.Show("No ha ingresado Título de la Solicitud de Consumo", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                tabControl.SelectTab(0);
                txtTitulo.Focus();
            }

            if (txtProveedor.Text == string.Empty && valorValida == "G")
            {
                valorValida = "B";
                MessageBox.Show("No ha seleccionado un Proveedor.", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                tabControl.SelectTab(0);
                txtProveedor.Focus();
            }

            //if (cmbSolicitante.Text != string.Empty && valorValida == "G")
            //{
            //    string secsoli = this._secsoli;
            //    string codcia = this._codcia;
            //    string codsoli = funciones.getValorComboBox(cmbSolicitante, 2);
            //    query = "SELECT codsoli as clave,dessoli as descripcion FROM solicita ";
            //    //query = query + "WHERE codcia='" + @codcia + "' and secsoli='" + @secsoli + "' and codsoli='" + @codsoli + "';";
            //    string resultado = funciones.verificaItemComboBox(query, cmbSolicitante);
            //    if (resultado.Trim() == string.Empty)
            //    {
            //        valorValida = "B";
            //        MessageBox.Show("Dato incorrecto en Solicitar V°B°", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //        tabControl.SelectTab(0);
            //        cmbSolicitante.Focus();
            //    }
            //}

            //if (txtDscProveedor.Text == string.Empty && valorValida == "G")
            //{
            //    valorValida = "B";
            //    MessageBox.Show("No ha ingresado Proveedor", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    tabControl.SelectTab(0);
            //    txtDscProveedor.Focus();
            //}

            //if (txtGlosa1.Text == string.Empty && valorValida == "G")
            //{
            //    valorValida = "B";
            //    MessageBox.Show("No ha ingresado Glosa", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    tabControl.SelectTab(0);
            //    txtGlosa1.Focus();
            //}
            return valorValida;
        }

        public void ui_listaItem()
        {
            string codcia = this._codcia;
            string alma = this._alma;
            string secsoli = this._secsoli;
            string solalma = txtCodigo.Text.Trim();

            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();

            string query = " SELECT A.item,A.codarti,A.desarti,A.unidad,A.cantidad/*,A.glosa1*/ ";
            query += "FROM solalmad A ";
            query += "WHERE A.alma='" + @alma + "' and A.solalma='" + @solalma + "' /*and A.codcia='" + @codcia + "' and A.secsoli='" + @secsoli + "'*/  ";
            query += "ORDER BY A.item asc ;";

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
                    dgvData.Columns[3].HeaderText = "Unidad";
                    dgvData.Columns[4].HeaderText = "Cantidad";
                    //dgvData.Columns[5].HeaderText = "Glosa";

                    dgvData.Columns[0].Width = 40;
                    dgvData.Columns[1].Width = 70;
                    dgvData.Columns[2].Width = 305;
                    dgvData.Columns[3].Width = 60;
                    dgvData.Columns[4].Width = 60;
                    //dgvData.Columns[5].Width = 300;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            conexion.Close();
        }

        private void txtFecha_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                if (UtiFechas.IsDate(txtFecha.Text))
                {
                    e.Handled = true;
                    SendKeys.Send("{TAB}");
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

        private void btnFinaliza_Click(object sender, EventArgs e)
        {
            string estado = txtEstado.Text.Substring(0, 1);
            if (estado.Equals("V"))
            {
                string valorValida = valida();
                if (valorValida.Equals("G"))
                {
                    try
                    {
                        if (this._finaliza.Equals("S"))
                        {
                            SolAlma solalma = new SolAlma();
                            GlobalVariables variables = new GlobalVariables();
                            string alma = this._alma;
                            string codcia = this._codcia;
                            string secsoli = this._secsoli;
                            string codsol = txtCodigo.Text.Trim();
                            string fecha = DateTime.Now.ToString("yyyy-MM-dd");
                            string hora = DateTime.Now.ToString("hh:mm:ss");
                            string usuario = variables.getValorUsr();
                            solalma.updFinSolAlma(codcia, alma, secsoli, codsol, fecha, hora, usuario);
                            txtEstado.Text = "FINALIZADO";
                            ((ui_solialma)FormPadre).btnActualizar.PerformClick();
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
                ui_solalmad ui_solalmad = new ui_solalmad();
                ui_solalmad._FormPadre = this;
                ui_solalmad._codcia = this._codcia;
                ui_solalmad._alma = this._alma;
                ui_solalmad._secsoli = this._secsoli;
                ui_solalmad._solalma = txtCodigo.Text.Trim();
                ui_solalmad.Activate();
                ui_solalmad.BringToFront();
                ui_solalmad.agregar();
                ui_solalmad.ShowDialog();
                ui_solalmad.Dispose();
            }
            else
            {
                MessageBox.Show("Primero debe de grabar el registro de cabecera", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            Int32 selectedCellCount = dgvData.GetCellCount(DataGridViewElementStates.Selected);
            if (selectedCellCount > 0)
            {
                string solalma = txtCodigo.Text.Trim();
                string item = dgvData.Rows[dgvData.SelectedCells[1].RowIndex].Cells[0].Value.ToString();
                ui_solalmad ui_solalmad = new ui_solalmad();
                ui_solalmad._FormPadre = this;
                ui_solalmad._codcia = this._codcia;
                ui_solalmad._alma = this._alma;
                ui_solalmad._secsoli = this._secsoli;
                ui_solalmad._solalma = solalma;
                ui_solalmad.Activate();
                ui_solalmad.BringToFront();
                ui_solalmad.editar(item);
                ui_solalmad.ShowDialog();
                ui_solalmad.Dispose();
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
                string codcia = this._codcia;
                string alma = this._alma;
                string secsoli = this._secsoli;
                string solalma = txtCodigo.Text.Trim();
                string item = dgvData.Rows[dgvData.SelectedCells[1].RowIndex].Cells[0].Value.ToString();
                DialogResult resultado = MessageBox.Show("¿Desea eliminar el Item " + @item + "?",
                "Consulta Importante",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);
                if (resultado == DialogResult.Yes)
                {
                    SolAlmaD solalmad = new SolAlmaD();
                    solalmad.delSolAlmaD(codcia, alma, secsoli, solalma, item);
                    ui_listaItem();
                }
            }
            else
            {
                MessageBox.Show("No ha seleccionado registro a Eliminar", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void txtNomRec_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(Keys.Enter))
            {
                e.Handled = true;
                SendKeys.Send("{TAB}");
            }
        }

        private void txtTitulo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(Keys.Enter))
            {
                e.Handled = true;
                SendKeys.Send("{TAB}");
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

        private void txtProveedor_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                this._TextBoxActivo = txtProveedor;
                ui_viewprovee ui_viewprovee = new ui_viewprovee();
                ui_viewprovee._FormPadre = this;
                ui_viewprovee._clasePadre = "ui_updsolialma";
                ui_viewprovee._condicionAdicional = string.Empty;
                ui_viewprovee.BringToFront();
                ui_viewprovee.ShowDialog();
                ui_viewprovee.Dispose();
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
                    //SendKeys.Send("{TAB}");

                    toolStripForm.Items[0].Select();
                    toolStripForm.Focus();
                }
            }
        }
    }
}