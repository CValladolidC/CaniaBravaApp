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
    public partial class ui_UpdQuiCat : Form
    {
        string _tipoper;
        string _anio;
        string _idcia;
        string _operacion;

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

        public ui_UpdQuiCat()
        {
            InitializeComponent();
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
                
                ui_ListaDetalle();


            }
        }

        private void txtCodigoInterno_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                FiltrosMaestros filtros = new FiltrosMaestros();
                string idcia = this._idcia;
                string idtipoper = this._tipoper;
                this._TextBoxActivo = txtCodigoInterno;
                string condicionAdicional = string.Empty;
                string cadenaBusqueda = string.Empty;
                filtros.filtrarPerPlan("ui_UpdQuiCat", this, txtCodigoInterno, idcia, idtipoper, "V", cadenaBusqueda, condicionAdicional);
            }
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            ((ui_QuiCat)FormPadre).btnActualizar.PerformClick();
            this.Close();
        }

        private void ui_UpdQuiCat_Load(object sender, EventArgs e)
        {
            lblTitulo.Text = "Impuesto a la Renta - Quinta Categoría - " + this._anio;
            Funciones funciones = new Funciones();
            MaesGen maesgen = new MaesGen();
            maesgen.listaDetMaesGen("035", cmbMes, "");
            ui_ListaDetalle();
        }

        public void ui_setValores(string tipoper,string anio,string idcia)
        {
            this._anio = anio;
            this._idcia = idcia;
            this._tipoper = tipoper;

        }

        public void ui_iniPerPlan(string operacion,string idperplan)
        {
            if (operacion.Equals("AGREGAR"))
            {
                txtCodigoInterno.Clear();
                txtDocIdent.Clear();
                txtNroDocIden.Clear();
                txtNombres.Clear();
                txtFecIniPerLab.Clear();
                lblF2.Visible = true;
                pbLocalizar.Visible = true;
                txtCodigoInterno.Enabled = true;
                txtCodigoInterno.Focus();

            }
            else
            {
                PerPlan perplan = new PerPlan();
                txtCodigoInterno.Text = idperplan;
                txtDocIdent.Text = perplan.ui_getDatosPerPlan(this._idcia, idperplan, "1");
                txtNroDocIden.Text = perplan.ui_getDatosPerPlan(this._idcia, idperplan, "2");
                txtNombres.Text = perplan.ui_getDatosPerPlan(this._idcia, idperplan, "3");
                txtFecIniPerLab.Text = perplan.ui_getDatosPerPlan(this._idcia, idperplan, "4");
                lblF2.Visible = false;
                pbLocalizar.Visible = false;
                txtCodigoInterno.Enabled = false;
            }
        }

        public void ui_newQuiCat()
        {
            this._operacion = "AGREGAR";
            ui_limpiar();
            cmbTipoCalculo.Text = "A  AUTOMATICO";
            ui_habilitar(false);
            cmbMes.Enabled = true;
            cmbTipoCalculo.Enabled = true;
            txtImporteExterno.Enabled = true;
            cmbMes.Focus();
        }

        public void ui_habilitar(bool estado)
        {
            cmbMes.Enabled = estado;
            cmbTipoCalculo.Enabled = estado;
            txtManual.Enabled = estado;
            txtImporte.Enabled = estado;
            txtImporteExterno.Enabled = estado;
            txtRemuneracion.Enabled = estado;
        }
        
        public void ui_limpiar()
        {
            this._operacion = "";
            cmbMes.Text = "";
            cmbTipoCalculo.Text = "";
            txtManual.Text = "0";
            txtImporte.Text = "0";
            txtImporteExterno.Text = "0";
            txtRemuneracion.Text = "0";
        }

        private void ui_ListaDetalle()
        {
            Funciones funciones = new Funciones();
            string idcia = this._idcia;
            string anio = this._anio;
            string idperplan = txtCodigoInterno.Text.Trim();
            string query = " select G.mes,G.anio,G.remafecta,G.tipcal,G.impman,G.impcal,G.impext,G.impcal+G.impext as total ";
            query = query + " from quicat G ";
            query = query + " where G.idcia='" + @idcia + "' and G.Anio='" + @anio + "' and ";
            query = query + " G.idperplan='" + @idperplan + "'";
            query = query + " order by G.mes asc ";
            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();
            try
            {
                using (SqlDataAdapter myDataAdapter = new SqlDataAdapter(query, conexion))
                {
                    DataSet myDataSet = new DataSet();
                    myDataAdapter.Fill(myDataSet, "tblQuinta");
                    funciones.formatearDataGridView(dgvdetalle);
                    dgvdetalle.DataSource = myDataSet.Tables["tblQuinta"];
                    dgvdetalle.Columns[0].HeaderText = "Mes";
                    dgvdetalle.Columns[1].HeaderText = "Año";
                    dgvdetalle.Columns[2].HeaderText = "Remuneración Afecta a Dscto.";
                    dgvdetalle.Columns[3].HeaderText = "Tipo Cálc.";
                    dgvdetalle.Columns[4].HeaderText = "Imp. Manual a Descontar en el mes";
                    dgvdetalle.Columns[5].HeaderText = "Dscto. Aut. Renta 5ta. Cat.";
                    dgvdetalle.Columns[6].HeaderText = "Otros Dsctos. 5ta.Cat.";
                    dgvdetalle.Columns[7].HeaderText = "Total descuento";
                    dgvdetalle.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    dgvdetalle.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    dgvdetalle.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    dgvdetalle.Columns[6].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    dgvdetalle.Columns[7].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    dgvdetalle.Columns[2].DefaultCellStyle.Format = "###,###.##";
                    dgvdetalle.Columns[4].DefaultCellStyle.Format = "###,###.##";
                    dgvdetalle.Columns[5].DefaultCellStyle.Format = "###,###.##";
                    dgvdetalle.Columns[6].DefaultCellStyle.Format = "###,###.##";
                    dgvdetalle.Columns[7].DefaultCellStyle.Format = "###,###.##";
                    dgvdetalle.Columns[0].Width = 50;
                    dgvdetalle.Columns[1].Width = 50;
                    dgvdetalle.Columns[2].Width = 100;
                    dgvdetalle.Columns[3].Width = 50;
                    dgvdetalle.Columns[4].Width = 100;
                    dgvdetalle.Columns[5].Width = 100;
                    dgvdetalle.Columns[6].Width = 100;
                    dgvdetalle.Columns[7].Width = 100;
                }
                txtTotalDescAnual.Text = funciones.getDecimalRound(decimal.Parse(funciones.sumaColumnaDataGridView(dgvdetalle, 7)), 2).ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            conexion.Close();
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            this.ui_newQuiCat();
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            Int32 selectedCellCount = dgvdetalle.GetCellCount(DataGridViewElementStates.Selected);
            if (selectedCellCount > 0)
            {
                Funciones funciones = new Funciones();
                MaesGen maesgen = new MaesGen();
                this._operacion = "EDITAR";
                string mes = dgvdetalle.Rows[dgvdetalle.SelectedCells[1].RowIndex].Cells[0].Value.ToString();
                string anio = dgvdetalle.Rows[dgvdetalle.SelectedCells[1].RowIndex].Cells[1].Value.ToString();
                string remafecta = dgvdetalle.Rows[dgvdetalle.SelectedCells[1].RowIndex].Cells[2].Value.ToString();
                string tipcal = dgvdetalle.Rows[dgvdetalle.SelectedCells[1].RowIndex].Cells[3].Value.ToString();
                string impman = dgvdetalle.Rows[dgvdetalle.SelectedCells[1].RowIndex].Cells[4].Value.ToString();
                string impcal = dgvdetalle.Rows[dgvdetalle.SelectedCells[1].RowIndex].Cells[5].Value.ToString();
                string impext = dgvdetalle.Rows[dgvdetalle.SelectedCells[1].RowIndex].Cells[6].Value.ToString();
                string total = dgvdetalle.Rows[dgvdetalle.SelectedCells[1].RowIndex].Cells[7].Value.ToString();
                ui_habilitar(false);
                maesgen.consultaDetMaesGen("035", mes, cmbMes);
                switch (tipcal)
                {
                    case "A":
                        cmbTipoCalculo.Text = "A  AUTOMATICO";
               
                        break;
                    case "M":
                        cmbTipoCalculo.Text = "M  MANUAL";
                        txtManual.Enabled = true;
                        break;
                    default:
                        cmbTipoCalculo.Text = "A  AUTOMATICO";
                        break;
                }
                txtImporte.Text = impcal;
                txtManual.Text = impman;
                txtImporteExterno.Text = impext;
                txtTotal.Text = total;
                txtRemuneracion.Text = remafecta;
                cmbTipoCalculo.Enabled = true;
                txtImporteExterno.Enabled = true;
             }
            else
            {
                MessageBox.Show("No ha seleccionado registro a Editar", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            
        }

        private void btnGrabar_Click(object sender, EventArgs e)
        {
            try
            {
                Funciones funciones = new Funciones();
                string idcia = this._idcia;
                string idperplan = txtCodigoInterno.Text.Trim();
                string operacion = this._operacion;
                string mes = funciones.getValorComboBox(cmbMes, 2);
                string anio = this._anio;
                string tipcal = funciones.getValorComboBox(cmbTipoCalculo, 1);
                float impcal = float.Parse(txtImporte.Text);
                float impman = float.Parse(txtManual.Text);
                float impext = float.Parse(txtImporteExterno.Text);
                float remafecta = float.Parse(txtRemuneracion.Text);
                string valorValida = "G";

                if (valorValida == "G" && idperplan == string.Empty)
                {
                    valorValida = "B";
                    MessageBox.Show("No ha seleccionado Trabajador", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtCodigoInterno.Focus();
                }
                
                if (valorValida == "G" && mes == string.Empty)
                {
                    valorValida = "B";
                    MessageBox.Show("No ha seleccionado Mes", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    cmbMes.Focus();
                }
                                
                if (valorValida.Equals("G"))
                {
                    QuiCat quicat = new QuiCat();
                    quicat.actualizarQuiCat(operacion, idcia, idperplan, mes, anio, impcal, impext, remafecta,tipcal,impman);
                    ui_limpiar();
                    ui_ListaDetalle();
                }


            }
            catch (FormatException ex)
            {
                MessageBox.Show(ex.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtMesSem_KeyPress(object sender, KeyPressEventArgs e)
        {
            
        }

        private void txtImporte_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                e.Handled = true;
                txtImporteExterno.Focus();
            }
        }

        private void txtImporteExterno_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                e.Handled = true;
                txtRemuneracion.Focus();
            }
        }

        private void txtRemuneracion_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                e.Handled = true;
                toolstripform.Items[2].Select();
                toolstripform.Focus();
            }
        }

        private void groupBox3_Enter(object sender, EventArgs e)
        {

        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            string idcia = this._idcia;
            string idperplan = txtCodigoInterno.Text.Trim();

            Int32 selectedCellCount =
            dgvdetalle.GetCellCount(DataGridViewElementStates.Selected);
            if (selectedCellCount > 0)
            {
                string mes = dgvdetalle.Rows[dgvdetalle.SelectedCells[1].RowIndex].Cells[0].Value.ToString();
                string anio = dgvdetalle.Rows[dgvdetalle.SelectedCells[1].RowIndex].Cells[1].Value.ToString();
  
                DialogResult resultado = MessageBox.Show("¿Desea eliminar el Cálculo del mes " + mes + "?",
                "Consulta Importante",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);
                if (resultado == DialogResult.Yes)
                {
                    QuiCat quicat = new QuiCat();
                    quicat.eliminarQuiCat(idcia, idperplan, mes, anio);
                    ui_ListaDetalle();

                }

            }
            else
            {
                MessageBox.Show("No ha seleccionado registro a Eliminar", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void cmbTipoCal_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void txtMesSem_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }

        private void cmbTipoCalculo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                if (cmbTipoCalculo.Text == String.Empty)
                {
                    MessageBox.Show("El campo no acepta valores vacíos", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    e.Handled = true;
                    cmbTipoCalculo.Focus();
                }
                else
                {
                    e.Handled = true;
                    Funciones funciones =new Funciones();
                    switch (funciones.getValorComboBox(cmbTipoCalculo,1))
                    {
                        case "A":
                            cmbTipoCalculo.Text = "A  AUTOMATICO";
                            break;
                        case "M":
                            cmbTipoCalculo.Text = "M  MANUAL";
                            break;
                        default:
                            cmbTipoCalculo.Text = "A  AUTOMATICO";
                            break;
                    }
                    
                   
                    if (funciones.getValorComboBox(cmbTipoCalculo, 1).Equals("A"))
                    {
                        txtManual.Enabled = false;
                        txtManual.Text = "0";
                        txtImporteExterno.Focus();
                    }
                    else
                    {
                        txtManual.Enabled = true;
                        txtManual.Focus();
                    }
                }
            }
        }

        private void cmbTipoCalculo_SelectedIndexChanged(object sender, EventArgs e)
        {
            Funciones funciones = new Funciones();
            if (funciones.getValorComboBox(cmbTipoCalculo, 1).Equals("A"))
            {
                txtManual.Enabled = false;
                txtManual.Text = "0";
                txtImporteExterno.Focus();
            }
            else
            {
                txtManual.Enabled = true;
                txtManual.Focus();
            }
        }
    }
}