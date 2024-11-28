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
    public partial class ui_updpunclie : ui_form
    {
        
        public string _codclie;
        public string _desclie;
        public string _dniclie;
        public string _rucclie;
        string _clasePadre;

        string _opepunto;

        private Form FormPadre;

        public Form _FormPadre
        {
            get { return FormPadre; }
            set { FormPadre = value; }
        }

        public ui_updpunclie()
        {
            InitializeComponent();
        }

        public void setValores(string clasePadre)
        {
            this._clasePadre = clasePadre;
        }

        private void ui_updpunclie_Load(object sender, EventArgs e)
        {
            lblCodigo.Text = this._codclie;
            lblRazon.Text = this._desclie;
            lblDni.Text = this._dniclie;
            lblRuc.Text = this._rucclie;
            this.ui_listaPuntos(this._codclie);
        }
        
        public void ui_listaPuntos(string codclie)
        {
            Funciones funciones = new Funciones();
            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();
            string query = " select A.codpartida,A.despartida,A.codaux,A.codclie from punclie A ";
            query = query + " where A.codclie='" + @codclie + "' order by A.codpartida asc;";
            try
            {
                using (SqlDataAdapter myDataAdapter = new SqlDataAdapter(query, conexion))
                {
                    DataSet myDataSet = new DataSet();
                    myDataAdapter.Fill(myDataSet, "tblPuntos");
                    funciones.formatearDataGridView(dgvPuntos);
                    dgvPuntos.DataSource = myDataSet.Tables["tblPuntos"];
                    dgvPuntos.Columns[0].HeaderText = "Código";
                    dgvPuntos.Columns[1].HeaderText = "Descripción";
                    dgvPuntos.Columns[2].HeaderText = "Código según Cliente";
                    dgvPuntos.Columns["codclie"].Visible = false;
                    dgvPuntos.Columns[0].Width = 80;
                    dgvPuntos.Columns[1].Width = 450;
                    dgvPuntos.Columns[2].Width = 80;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            conexion.Close();
        }

       
        private void btnNuevo_Click(object sender, EventArgs e)
        {
            this._opepunto = "AGREGAR";
            txtCodPunto.Clear();
            txtDesPunto.Clear();
            txtCodPunto.Enabled = false;
            txtDesPunto.Enabled = true;
            txtCodAux.Enabled = true;
            txtDesPunto.Focus();
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            Int32 selectedCellCount = dgvPuntos.GetCellCount(DataGridViewElementStates.Selected);
            if (selectedCellCount > 0)
            {
                string codpartida = dgvPuntos.Rows[dgvPuntos.SelectedCells[1].RowIndex].Cells[0].Value.ToString();
                string despartida = dgvPuntos.Rows[dgvPuntos.SelectedCells[1].RowIndex].Cells[1].Value.ToString();
                string codaux = dgvPuntos.Rows[dgvPuntos.SelectedCells[1].RowIndex].Cells[2].Value.ToString();
                txtCodPunto.Enabled = false;
                txtDesPunto.Enabled = true;
                txtCodAux.Enabled = true;
                this._opepunto = "EDITAR";
                txtCodPunto.Text = codpartida;
                txtDesPunto.Text = despartida;
                txtCodAux.Text = codaux;
                txtDesPunto.Focus();
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
                string operacion = this._opepunto;
                string despartida = txtDesPunto.Text.Trim();
                string codclie = this._codclie;
                string codaux = txtCodAux.Text;

                string valorValida = "G";

                if (txtDesPunto.Text == string.Empty && valorValida == "G")
                {
                    valorValida = "B";
                    MessageBox.Show("No ha ingresado Nombre de Establecimiento", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtDesPunto.Focus();
                }

                if (valorValida.Equals("G"))
                {
                    PunClie punclie = new PunClie();
                    string codpartida = string.Empty;

                    if (this._opepunto.Equals("AGREGAR"))
                    {
                        codpartida = punclie.genCodPunto(codclie);
                    }
                    else
                    {
                        codpartida = txtCodPunto.Text.Trim();
                    }

                    punclie.updPunClie(operacion, codclie, codpartida, despartida,codaux);
                    ui_listaPuntos(codclie);
                    txtCodPunto.Clear();
                    txtDesPunto.Clear();
                    txtCodAux.Clear();
                    txtCodPunto.Enabled = false;
                    txtDesPunto.Enabled = false;
                    txtCodAux.Enabled = false;
                }
            }
            catch (FormatException ex)
            {
                MessageBox.Show(ex.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
            
            if (this._clasePadre.Equals("ui_updguiaremi"))
            {
                string codclie = this._codclie;
                ((ui_updguiaremi)FormPadre).ui_listarPunClie(codclie);
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            Int32 selectedCellCount = dgvPuntos.GetCellCount(DataGridViewElementStates.Selected);
            if (selectedCellCount > 0)
            {
                string codpartida = dgvPuntos.Rows[dgvPuntos.SelectedCells[1].RowIndex].Cells[0].Value.ToString();
                string despartida = dgvPuntos.Rows[dgvPuntos.SelectedCells[1].RowIndex].Cells[1].Value.ToString();
                string codclie = dgvPuntos.Rows[dgvPuntos.SelectedCells[1].RowIndex].Cells[3].Value.ToString();

                DialogResult resultado = MessageBox.Show("¿Desea eliminar el Establecimiento " + despartida + "?",
                "Consulta Importante",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);
                if (resultado == DialogResult.Yes)
                {
                    PunClie punclie = new PunClie();
                    punclie.delPunClie(codclie, codpartida);
                    ui_listaPuntos(codclie);
                }

            }
            else
            {
                MessageBox.Show("No ha seleccionado registro a Eliminar", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void txtDesPunto_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                e.Handled = true;
                txtCodAux.Focus();
            }
        }

        private void txtCodAux_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                e.Handled = true;
                toolstripform.Items[2].Select();
                toolstripform.Focus();
            }

       }

       

        
    }
}
