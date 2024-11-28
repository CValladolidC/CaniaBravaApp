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
    public partial class ui_updproddes : Form
    {
        string _idcia;

        private Form FormPadre;

        public Form _FormPadre
        {
            get { return FormPadre; }
            set { FormPadre = value; }
        }

        public ui_updproddes()
        {
            InitializeComponent();
        }

        public void ui_setVariableForm(string idcia)
        {
            this._idcia = idcia;
        }

        private void txtNombreCorto_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                e.Handled = true;
                cmbUnidad.Focus();
            }
        }

        private void cmbUnidad_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                if (cmbUnidad.Text != String.Empty)
                {
                    MaesGen maesgen = new MaesGen();
                    maesgen.validarDetMaesGen("026", cmbUnidad, cmbUnidad.Text);
                }
                e.Handled = true;
                cmbEstado.Focus();

            }
        }

        private void txtDescripcion_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                e.Handled = true;
                txtNombreCorto.Focus();
            }
        }

        private void ui_updproddes_Load(object sender, EventArgs e)
        {
            ui_ActualizaComboBox(this._idcia);
        }

        public void ui_ActualizaComboBox(string idcia)
        {

            MaesGen maesgen = new MaesGen();
            Funciones funciones = new Funciones();
            string query;
            maesgen.listaDetMaesGen("026", cmbUnidad, "B");
            query = "SELECT idtipoplan as clave,destipoplan as descripcion FROM tipoplan where idtipoplan in (select idtipoplan from reglabcia where idcia='" + @idcia + "') order by 1 asc;";
            funciones.listaComboBox(query, cmbTipoPlan, "");
            query = "SELECT idtipocal as clave,destipocal as descripcion FROM tipocal where idtipocal in ('N','D') order by ordentipocal asc;";
            funciones.listaComboBox(query, cmbTipoCal, "");


        }

        internal void ui_newProdDes()
        {
            txtCodigo.Enabled = true;
            txtOperacion.Text = "AGREGAR";
            txtCodigo.Clear();
            txtDescripcion.Clear();
            txtNombreCorto.Clear();
            cmbUnidad.Text = string.Empty;
            cmbEstado.Text = "V         VIGENTE";
            this.ui_ListaDetProdDes("");
            txtCodigo.Focus();
        }

        internal void ui_loadProdDes(string idproddes, string idcia, string desproddes, string cortoproddes, string unidad, string stateproddes)
        {

            MaesGen maesgen = new MaesGen();
            txtOperacion.Text = "EDITAR";
            txtCodigo.Enabled = false;
            txtCodigo.Text = idproddes;
            txtDescripcion.Text = desproddes;
            txtNombreCorto.Text = cortoproddes;
            maesgen.consultaDetMaesGen("026", unidad, cmbUnidad);

            switch (stateproddes)
            {
                case "A":
                    cmbEstado.Text = "A        ANULADO";
                    break;
                case "V":
                    cmbEstado.Text = "V         VIGENTE";
                    break;
                default:
                    cmbEstado.Text = "V         VIGENTE";
                    break;
            }

            this.ui_ListaDetProdDes(idproddes);
            txtDescripcion.Focus();

        }

        internal void ui_actualizaConceptos(string idtipoplan, string idtipoper, string idcia, string idtipocal)
        {
            Funciones funciones = new Funciones();
            string query;
            query = " select A.idconplan as clave,A.desboleta as descripcion ";
            query = query + " from detconplan  A  left join conplan B on A.idtipoplan=B.idtipoplan ";
            query = query + " and A.idconplan=B.idconplan and A.idcia=B.idcia and A.idtipocal=B.idtipocal ";
            query = query + " where A.idtipoplan='" + @idtipoplan + "' and A.idtipoper='" + @idtipoper + "' ";
            query = query + " and A.idcia='" + @idcia + "' and B.tipo='O' and A.destajo='SI' and A.idtipocal='" + @idtipocal + "' ";
            query = query + " order by 1 asc";
            funciones.listaComboBox(query, cmbConceptoCantidad, "");
            funciones.listaComboBox(query, cmbConceptoImporte, "");
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {

            Funciones funciones = new Funciones();
            MaesGen maesgen = new MaesGen();
            string operacion = txtOperacion.Text.Trim();
            string idproddes = txtCodigo.Text.Trim();
            string desproddes = txtDescripcion.Text.Trim();
            string cortoproddes = txtNombreCorto.Text.Trim();
            string unidad = funciones.getValorComboBox(cmbUnidad, 4);
            string stateproddes = cmbEstado.Text.Substring(0, 1);
            string idcia = this._idcia;

            string valorValida = "G";

            if (idproddes == string.Empty && valorValida == "G")
            {
                valorValida = "B";
                MessageBox.Show("No ha especificado Código", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                txtCodigo.Focus();

            }

            if (desproddes == String.Empty && valorValida == "G")
            {
                valorValida = "B";
                MessageBox.Show("No ha especificado descripción del Producto", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                txtDescripcion.Focus();
            }


            if (cmbUnidad.Text == string.Empty && valorValida == "G")
            {
                valorValida = "B";
                MessageBox.Show("No ha seleccionado Unidad de Medida", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cmbUnidad.Focus();
            }

            if (cmbUnidad.Text != string.Empty && valorValida == "G")
            {
                string resultado = maesgen.verificaComboBoxMaesGen("026", cmbUnidad.Text.Trim());
                if (resultado.Trim() == string.Empty)
                {
                    valorValida = "B";
                    MessageBox.Show("Dato incorrecto en Unidad de Medida", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    cmbUnidad.Focus();
                }
            }

            if (stateproddes == string.Empty && valorValida == "G")
            {
                valorValida = "B";
                MessageBox.Show("No ha especificado Estado", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                cmbEstado.Focus();
            }

            if (stateproddes != "V" && stateproddes != "A" && valorValida == "G")
            {
                valorValida = "B";
                MessageBox.Show("Dato incorrecto en Estado", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                cmbEstado.Focus();
            }

            if (valorValida == "G")
            {
                ProdDes updproddes = new ProdDes();
                updproddes.setProdDes(idproddes, idcia, desproddes, cortoproddes, unidad, stateproddes);
                updproddes.actualizarProdDes(operacion);
                txtOperacion.Text = "EDITAR";
            }


        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            ((ui_proddestajo)FormPadre).btnActualizar.PerformClick();
            Close();
        }

        private void cmbEstado_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                if (cmbEstado.Text == String.Empty)
                {
                    MessageBox.Show("El campo no acepta valores vacíos", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    e.Handled = true;
                    cmbEstado.Focus();
                }
                else
                {
                    switch (cmbEstado.Text.ToUpper().Substring(0, 1))
                    {
                        case "A":
                            cmbEstado.Text = "A        ANULADO";
                            break;
                        case "V":
                            cmbEstado.Text = "V         VIGENTE";
                            break;
                        default:
                            cmbEstado.Text = "V         VIGENTE";
                            break;
                    }
                    e.Handled = true;
                    toolStripForm.Items[0].Select();
                    toolStripForm.Focus();
                }
            }
        }

        private void txtCodigo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                e.Handled = true;
                txtDescripcion.Focus();
            }
        }

        private void cmbTipoPlan_SelectedIndexChanged(object sender, EventArgs e)
        {
            Funciones funciones = new Funciones();
            string idtipoplan = funciones.getValorComboBox(cmbTipoPlan, 3);
            string idtipocal = funciones.getValorComboBox(cmbTipoCal, 1);
            string idcia = this._idcia;
            ui_actualizaConceptos(idtipoplan, "O", idcia, idtipocal);
        }

        private void ui_ListaDetProdDes(string idproddes)
        {
            Funciones funciones = new Funciones();
            string idcia = this._idcia;
            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();
            string query = "select B.destipoplan,A.idtipocal,A.conplancan,A.conplanimp,A.idproddes,A.idcia,A.idtipoplan from detproddes A left join tipoplan B on A.idtipoplan=B.idtipoplan where A.idcia='" + @idcia + "' and A.idproddes='" + @idproddes + "' order by A.idproddes asc;";

            try
            {
                using (SqlDataAdapter myDataAdapter = new SqlDataAdapter(query, conexion))
                {
                    DataSet myDataSet = new DataSet();
                    myDataAdapter.Fill(myDataSet, "tblDetProdDes");
                    funciones.formatearDataGridView(dgvdetalle);
                    dgvdetalle.DataSource = myDataSet.Tables["tblDetProdDes"];
                    dgvdetalle.Columns[0].HeaderText = "Planilla";
                    dgvdetalle.Columns[1].HeaderText = "Tipo";
                    dgvdetalle.Columns[2].HeaderText = "Concepto Cantidad";
                    dgvdetalle.Columns[3].HeaderText = "Concepto Importe";

                    dgvdetalle.Columns["idproddes"].Visible = false;
                    dgvdetalle.Columns["idcia"].Visible = false;
                    dgvdetalle.Columns["idtipoplan"].Visible = false;



                    dgvdetalle.Columns[0].Width = 120;
                    dgvdetalle.Columns[1].Width = 50;
                    dgvdetalle.Columns[2].Width = 120;
                    dgvdetalle.Columns[3].Width = 120;

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }

            conexion.Close();
        }
 
        private void btnAgregar_Click(object sender, EventArgs e)
        {
            if (txtOperacion.Text.Trim().Equals("EDITAR"))
            {
                Funciones funciones = new Funciones();
                string idcia = this._idcia;
                string idproddes = txtCodigo.Text.Trim();
                string idtipoplan = funciones.getValorComboBox(cmbTipoPlan, 3);
                string idtipocal = funciones.getValorComboBox(cmbTipoCal, 1);
                string conplancan = funciones.getValorComboBox(cmbConceptoCantidad, 4);
                string conplanimp = funciones.getValorComboBox(cmbConceptoImporte, 4);

                if (idtipoplan.Trim().Equals(""))
                {
                    MessageBox.Show("No ha seleccionado Tipo de Planilla", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    cmbTipoPlan.Focus();
                }
                else
                {
                    if (conplancan.Trim().Equals(""))
                    {
                        MessageBox.Show("No ha seleccionado Concepto de Planilla para Cantidad", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        cmbConceptoCantidad.Focus();

                    }
                    else
                    {
                        if (conplanimp.Trim().Equals(""))
                        {
                            MessageBox.Show("No ha seleccionado Concepto de Planilla para Importe", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                            cmbConceptoImporte.Focus();

                        }
                        else
                        {

                            DetProdDes detproddes = new DetProdDes();
                            detproddes.actualizarDetProdDes(idproddes, idcia, idtipoplan, conplancan, conplanimp, idtipocal);
                            this.ui_ListaDetProdDes(idproddes);
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Primero debe guardar información del Producto de Destajo", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }


        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            string idcia;
            string idproddes;
            string idtipoplan;
            string idtipocal;
            Int32 selectedCellCount =
            dgvdetalle.GetCellCount(DataGridViewElementStates.Selected);
            if (selectedCellCount > Convert.ToInt32(1))
            {

                idtipocal = dgvdetalle.Rows[dgvdetalle.SelectedCells[1].RowIndex].Cells[1].Value.ToString();
                idproddes = dgvdetalle.Rows[dgvdetalle.SelectedCells[1].RowIndex].Cells[4].Value.ToString();
                idcia = dgvdetalle.Rows[dgvdetalle.SelectedCells[1].RowIndex].Cells[5].Value.ToString();
                idtipoplan = dgvdetalle.Rows[dgvdetalle.SelectedCells[1].RowIndex].Cells[6].Value.ToString();

                DialogResult resultado = MessageBox.Show("¿Desea eliminar la definición del tipo de planilla " + idtipoplan + "?",
                "Consulta Importante",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);
                if (resultado == DialogResult.Yes)
                {
                    DetProdDes detproddes = new DetProdDes();
                    detproddes.eliminarDetProdDes(idproddes, idcia, idtipoplan, idtipocal);
                    this.ui_ListaDetProdDes(idproddes);
                }

            }
            else
            {
                MessageBox.Show("No ha seleccionado registro a Eliminar", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void cmbTipoCal_SelectedIndexChanged(object sender, EventArgs e)
        {
            Funciones funciones = new Funciones();
            string idtipoplan = funciones.getValorComboBox(cmbTipoPlan, 3);
            string idtipocal = funciones.getValorComboBox(cmbTipoCal, 1);
            string idcia = this._idcia;
            ui_actualizaConceptos(idtipoplan, "O", idcia, idtipocal);
        }

        private void toolStripForm_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }
    }
}