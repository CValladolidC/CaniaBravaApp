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
    public partial class ui_estaclie : ui_form
    {
        public string _codclie;
        public string _desclie;
        public string _dniclie;
        public string _rucclie;
        string _clasePadre;

        string _opeesta;

        private Form FormPadre;

        public Form _FormPadre
        {
            get { return FormPadre; }
            set { FormPadre = value; }
        }


        public ui_estaclie()
        {
            InitializeComponent();
        }

        private void ui_estaclie_Load(object sender, EventArgs e)
        {
            lblCodigo.Text = this._codclie;
            lblRazon.Text = this._desclie;
            lblDni.Text = this._dniclie;
            lblRuc.Text = this._rucclie;
            this.ui_listaEsta(this._codclie);
        }

       
        
        public void setValores(string clasePadre)
        {
            this._clasePadre = clasePadre;
        }

       
        
        public void ui_listaEsta(string codclie)
        {
            Funciones funciones = new Funciones();
            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();
            string query = " select A.codesta,A.desesta,A.codclie from estaclie A ";
            query = query + " where A.codclie='" + @codclie + "' order by A.codesta asc;";
            try
            {
                using (SqlDataAdapter myDataAdapter = new SqlDataAdapter(query, conexion))
                {
                    DataSet myDataSet = new DataSet();
                    myDataAdapter.Fill(myDataSet, "tblEsta");
                    funciones.formatearDataGridView(dgvPuntos);
                    dgvPuntos.DataSource = myDataSet.Tables["tblEsta"];
                    dgvPuntos.Columns[0].HeaderText = "Código";
                    dgvPuntos.Columns[1].HeaderText = "Descripción";
                    dgvPuntos.Columns["codclie"].Visible = false;
                    dgvPuntos.Columns[0].Width = 80;
                    dgvPuntos.Columns[1].Width = 450;
      
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
            this._opeesta = "AGREGAR";
            txtCodigo.Clear();
            txtDescripcion.Clear();
            txtCodigo.Enabled = true;
            txtDescripcion.Enabled = true;
            txtCodigo.Focus();
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            Int32 selectedCellCount = dgvPuntos.GetCellCount(DataGridViewElementStates.Selected);
            if (selectedCellCount > 0)
            {
                string codesta = dgvPuntos.Rows[dgvPuntos.SelectedCells[1].RowIndex].Cells[0].Value.ToString();
                string desesta = dgvPuntos.Rows[dgvPuntos.SelectedCells[1].RowIndex].Cells[1].Value.ToString();

                txtCodigo.Enabled = false;
                txtDescripcion.Enabled = true;
                this._opeesta = "EDITAR";
                txtCodigo.Text = codesta;
                txtDescripcion.Text = desesta;
                txtDescripcion.Focus();
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
                string operacion = this._opeesta;
                
                string codclie = this._codclie;

                string valorValida = "G";

                if (txtCodigo.Text == string.Empty && valorValida == "G")
                {
                    valorValida = "B";
                    MessageBox.Show("No ha ingresado Código de Establecimiento y/o Tienda", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtCodigo.Focus();
                }

                if (txtDescripcion.Text == string.Empty && valorValida == "G")
                {
                    valorValida = "B";
                    MessageBox.Show("No ha ingresado Nombre de Establecimiento y/o Tienda", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtDescripcion.Focus();
                }

                if (valorValida.Equals("G"))
                {

                    if (txtCodigo.Text.Length < 5)
                    {
                        txtCodigo.Text = funciones.replicateCadena("0", 5 - txtCodigo.Text.Trim().Length) + txtCodigo.Text.Trim();
                    }

                    EstaClie estaclie = new EstaClie();
                    string codesta = txtCodigo.Text.Trim();
                    string desesta = txtDescripcion.Text.Trim();
                    estaclie.updEstaClie(operacion, codclie, codesta, desesta);
                    ui_listaEsta(codclie);
                    txtCodigo.Clear();
                    txtDescripcion.Clear();
                    txtCodigo.Enabled = false;
                    txtDescripcion.Enabled = false;
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
                ((ui_updguiaremi)FormPadre).ui_listarEstaClie(codclie);
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            Int32 selectedCellCount = dgvPuntos.GetCellCount(DataGridViewElementStates.Selected);
            if (selectedCellCount > 0)
            {
                string codesta = dgvPuntos.Rows[dgvPuntos.SelectedCells[1].RowIndex].Cells[0].Value.ToString();
                string desesta = dgvPuntos.Rows[dgvPuntos.SelectedCells[1].RowIndex].Cells[1].Value.ToString();
                string codclie = dgvPuntos.Rows[dgvPuntos.SelectedCells[1].RowIndex].Cells[2].Value.ToString();

                DialogResult resultado = MessageBox.Show("¿Desea eliminar el Establecimiento y/o Tienda" + desesta + "?",
                "Consulta Importante",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);
                if (resultado == DialogResult.Yes)
                {
                    EstaClie estaclie = new EstaClie();
                    estaclie.delEstaClie(codclie, codesta);
                    ui_listaEsta(codclie);
                }

            }
            else
            {
                MessageBox.Show("No ha seleccionado registro a Eliminar", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void txtDescripcion_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                e.Handled = true;
                toolstripform.Items[2].Select();
                toolstripform.Focus();
            }
        }

        private void txtCodigo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                Funciones funciones = new Funciones();
                if (txtCodigo.Text.Trim() != string.Empty)
                {
                    if (txtCodigo.Text.Length == 5)
                    {
                        e.Handled = true;
                        txtDescripcion.Focus();
                    }
                    else
                    {
                        txtCodigo.Text = funciones.replicateCadena("0", 5 - txtCodigo.Text.Trim().Length) + txtCodigo.Text.Trim();
                        e.Handled = true;
                        txtDescripcion.Focus();

                    }
                }
                else
                {
                    MessageBox.Show("Deberá asignar un Código Único al Establecimiento", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    e.Handled = true;
                    txtDescripcion.Focus();
                }

            }

           
        }

       

       

    }
}
