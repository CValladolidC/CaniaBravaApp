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
    public partial class ui_zonatrabajo : Form
    {
        public ui_zonatrabajo()
        {

            InitializeComponent();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            Close();
        }

        public void ui_newZonTra()
        {

            txtOperacion.Text = "AGREGAR";
            txtCodigo.Enabled = true;
            txtDescripcion.Enabled = true;
            cmbEstado.Enabled = true;
            txtCodigo.Clear();
            txtDescripcion.Clear();
            cmbEstado.Text = "V         VIGENTE";
            txtCodigo.Focus();

        }

        private void ui_ListaZonTra()
        {

            Funciones funciones = new Funciones();
            GlobalVariables variables = new GlobalVariables();
            string idcia = variables.getValorCia();
            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();
            string query = "select idzontra,deszontra,statezontra,idcia from zontra where idcia='" + @idcia + "' order by 1 asc;";

            try
            {
                using (SqlDataAdapter myDataAdapter = new SqlDataAdapter(query, conexion))
                {
                    DataSet myDataSet = new DataSet();
                    myDataAdapter.Fill(myDataSet, "tblZonTra");
                    funciones.formatearDataGridView(dgvdetalle);
                    dgvdetalle.DataSource = myDataSet.Tables["tblZonTra"];
                    dgvdetalle.Columns[0].HeaderText = "Código";
                    dgvdetalle.Columns[1].HeaderText = "Descripción";
                    dgvdetalle.Columns[2].HeaderText = "Estado";
                    dgvdetalle.Columns["idcia"].Visible = false;
                    dgvdetalle.Columns[0].Width = 60;
                    dgvdetalle.Columns[1].Width = 300;
                    dgvdetalle.Columns[2].Width = 80;

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }



            conexion.Close();
        }

        private void ui_zonatrabajo_Load(object sender, EventArgs e)
        {
            ui_ListaZonTra();
        }

        private void ui_loadZonTra(string idzontra, string deszontra, string statezontra)
        {
            Funciones funciones = new Funciones();
            txtOperacion.Text = "EDITAR";

            txtCodigo.Enabled = false;
            txtDescripcion.Enabled = true;
            cmbEstado.Enabled = true;

            txtCodigo.Text = idzontra;
            txtDescripcion.Text = deszontra;

            switch (statezontra)
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

            txtDescripcion.Focus();
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            string idzontra;
            string deszontra;
            string statezontra;


            Int32 selectedCellCount =
            dgvdetalle.GetCellCount(DataGridViewElementStates.Selected);
            if (selectedCellCount > Convert.ToInt32(0))
            {
                idzontra = dgvdetalle.Rows[dgvdetalle.SelectedCells[1].RowIndex].Cells[0].Value.ToString();
                deszontra = dgvdetalle.Rows[dgvdetalle.SelectedCells[1].RowIndex].Cells[1].Value.ToString();
                statezontra = dgvdetalle.Rows[dgvdetalle.SelectedCells[1].RowIndex].Cells[2].Value.ToString();
                this.ui_loadZonTra(idzontra, deszontra, statezontra);
            }
            else
            {
                MessageBox.Show("No ha seleccionado registro a Editar", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
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
                    toolstripform.Items[2].Select();
                    toolstripform.Focus();
                }
            }
        }

        private void cmbEstado_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void txtCodigo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                e.Handled = true;
                txtDescripcion.Focus();
            }
        }

        private void txtDescripcion_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                e.Handled = true;
                cmbEstado.Focus();
            }
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            ui_newZonTra();
        }

        private void btnGrabar_Click(object sender, EventArgs e)
        {
            try
            {

                Funciones funciones = new Funciones();
                GlobalVariables variables = new GlobalVariables();
                string idcia = variables.getValorCia();
                string operacion = txtOperacion.Text.Trim();
                string idzontra = txtCodigo.Text.Trim();
                string deszontra = txtDescripcion.Text.Trim();
                string statezontra = funciones.getValorComboBox(cmbEstado, 1);
                string valida = "G";

                if (idzontra == string.Empty && valida == "G")
                {
                    valida = "B";
                    MessageBox.Show("No ha ingresado Código", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                if (deszontra == string.Empty && valida == "G")
                {
                    valida = "B";
                    MessageBox.Show("No ha ingresado Zona de Trabajo", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                if (statezontra == string.Empty)
                {
                    valida = "B";
                    MessageBox.Show("No ha especificado Estado", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    cmbEstado.Focus();
                }

                if (statezontra != "V" && statezontra != "A")
                {
                    valida = "B";
                    MessageBox.Show("Dato incorrecto en Estado", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    cmbEstado.Focus();
                }

                if (valida.Equals("G"))
                {
                    ZonTra zontra = new ZonTra();
                    zontra.setZonTra(idzontra, idcia, deszontra, statezontra);
                    zontra.actualizarSisParm(operacion);
                    ui_ListaZonTra();
                    txtOperacion.Clear();
                    txtCodigo.Clear();
                    txtDescripcion.Clear();
                    cmbEstado.Text = "";
                    txtCodigo.Enabled = false;
                    txtDescripcion.Enabled = false;
                    cmbEstado.Enabled = false;
                }


            }
            catch (FormatException ex)
            {
                MessageBox.Show(ex.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            string idzontra;
            string deszontra;
            string idcia;


            Int32 selectedCellCount =
            dgvdetalle.GetCellCount(DataGridViewElementStates.Selected);
            if (selectedCellCount > Convert.ToInt32(0))
            {
                idzontra = dgvdetalle.Rows[dgvdetalle.SelectedCells[1].RowIndex].Cells[0].Value.ToString();
                deszontra = dgvdetalle.Rows[dgvdetalle.SelectedCells[1].RowIndex].Cells[1].Value.ToString();
                idcia = dgvdetalle.Rows[dgvdetalle.SelectedCells[1].RowIndex].Cells[3].Value.ToString();

                DialogResult resultado = MessageBox.Show("¿Desea eliminar la Zona de Trabajo " + deszontra + "?",
                "Consulta Importante",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);
                if (resultado == DialogResult.Yes)
                {
                    ZonTra zontra = new ZonTra();
                    zontra.eliminarZonTra(idzontra, idcia);
                    this.ui_ListaZonTra();
                }
            }
            else
            {
                MessageBox.Show("No ha seleccionado registro a Eliminar", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }
    }
}