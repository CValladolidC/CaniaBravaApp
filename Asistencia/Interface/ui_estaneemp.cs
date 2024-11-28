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
    public partial class ui_estaneemp : Form
    {
        private Form FormPadre;

        public Form _FormPadre
        {
            get { return FormPadre; }
            set { FormPadre = value; }
        }

        public ui_estaneemp()
        {
            InitializeComponent();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void txtCodigoEstEmp_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                e.Handled = true;
                txtNombreEstEmp.Focus();
            }
        }

        private void txtNombreEstEmp_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                e.Handled = true;
                cmbTipoEstEmp.Focus();
            }
        }

        private void ui_estaneemp_Load(object sender, EventArgs e)
        {


        }

        public void ui_loadEstEmp() { }

        public void ui_listaEstAne(string idcia, string codemp)
        {
            Funciones funciones = new Funciones();
            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();
            string query = "select A.idestane,B.desmaesgen,A.desestane,CASE A.riesgo WHEN '1' THEN 'SI' WHEN '0' THEN 'NO' END,A.riesgo,A.tipoestane,A.codemp,A.idciafile from estane A left join maesgen B on A.tipoestane=B.clavemaesgen and B.idmaesgen='027' where codemp='" + @codemp + "' and idciafile='" + @idcia + "' order by A.idestane asc;";
            try
            {
                using (SqlDataAdapter myDataAdapter = new SqlDataAdapter(query, conexion))
                {
                    DataSet myDataSet = new DataSet();
                    myDataAdapter.Fill(myDataSet, "tblEstAne");
                    funciones.formatearDataGridView(dgvEstAne);

                    dgvEstAne.DataSource = myDataSet.Tables["tblEstAne"];
                    dgvEstAne.Columns[0].HeaderText = "Código Estab.";
                    dgvEstAne.Columns[1].HeaderText = "Tipo de Establecimiento";
                    dgvEstAne.Columns[2].HeaderText = "Denominación";
                    dgvEstAne.Columns[3].HeaderText = "¿Centro de Riesgo?";

                    dgvEstAne.Columns["tipoestane"].Visible = false;
                    dgvEstAne.Columns["codemp"].Visible = false;
                    dgvEstAne.Columns["idciafile"].Visible = false;
                    dgvEstAne.Columns["riesgo"].Visible = false;

                    dgvEstAne.Columns[0].Width = 100;
                    dgvEstAne.Columns[1].Width = 150;
                    dgvEstAne.Columns[2].Width = 280;
                    dgvEstAne.Columns[3].Width = 70;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }

            conexion.Close();
        }

        public void ui_loadEstEmp(string idciafile, string rucEmp, string RazonEmp)
        {
            txtOpeEstEmp.Text = "AGREGAR";
            txtIdCiaFile.Text = idciafile;
            txtRucEmp.Text = rucEmp;
            txtRazonEmp.Text = RazonEmp;
            ui_listaEstAne(idciafile, rucEmp);
            ui_limpiarEstEmp();

        }

        public void ui_limpiarEstEmp()
        {
            txtOpeEstEmp.Clear();
            txtCodigoEstEmp.Clear();
            txtNombreEstEmp.Clear();
            cmbTipoEstEmp.Text = string.Empty;
            radioButtonNoCR.Checked = true;
            radioButtonSiCR.Checked = false;
            txtCodigoEstEmp.Enabled = false;
            txtNombreEstEmp.Enabled = false;
            radioButtonNoCR.Enabled = false;
            radioButtonSiCR.Enabled = false;
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            MaesGen maesgen = new MaesGen();
            string valorValida = "G";


            if (cmbTipoEstEmp.Text == string.Empty && valorValida == "G")
            {
                valorValida = "B";
                MessageBox.Show("No ha seleccionado Tipo de Establecimiento", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cmbTipoEstEmp.Focus();
            }

            if (cmbTipoEstEmp.Text != string.Empty && valorValida == "G")
            {

                string resultado = maesgen.verificaComboBoxMaesGen("027", cmbTipoEstEmp.Text.Trim());
                if (resultado.Trim() == string.Empty)
                {
                    valorValida = "B";
                    MessageBox.Show("Dato incorrecto en Tipo de Establecimiento", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    cmbTipoEstEmp.Focus();
                }
            }
        }

        private void radioButtonSiCR_CheckedChanged(object sender, EventArgs e)
        {


        }

        private void radioButtonNoCR_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void btnNuevoEstAne_Click(object sender, EventArgs e)
        {
            txtOpeEstEmp.Text = "AGREGAR";
            txtCodigoEstEmp.Clear();
            txtNombreEstEmp.Clear();
            cmbTipoEstEmp.Text = "08    ESTABLECIMIENTO DE TERCEROS";
            radioButtonNoCR.Checked = true;
            radioButtonSiCR.Checked = false;
            txtCodigoEstEmp.Enabled = true;
            txtNombreEstEmp.Enabled = true;
            radioButtonNoCR.Enabled = true;
            radioButtonSiCR.Enabled = true;
            txtCodigoEstEmp.Focus();

        }

        private void btnGrabarEstAne_Click(object sender, EventArgs e)
        {


            try
            {
                Funciones funciones = new Funciones();
                MaesGen maesgen = new MaesGen();
                EstAne estane = new EstAne();

                string operacion = txtOpeEstEmp.Text.Trim();
                string idestane = txtCodigoEstEmp.Text.Trim();
                string desestane = txtNombreEstEmp.Text.Trim();
                string tipoestane = funciones.getValorComboBox(cmbTipoEstEmp, 4);
                string idcia = txtIdCiaFile.Text.Trim();
                string codemp = txtRucEmp.Text.Trim();
                string riesgo;
                if (radioButtonNoCR.Checked)
                {
                    riesgo = "0";
                }
                else
                {
                    riesgo = "1";
                }


                string valorValida = "G";

                if (txtCodigoEstEmp.Text == string.Empty && valorValida == "G")
                {
                    valorValida = "B";
                    MessageBox.Show("No ha ingresado Código de Establecimiento", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtCodigoEstEmp.Focus();
                }

                if (txtNombreEstEmp.Text == string.Empty && valorValida == "G")
                {
                    valorValida = "B";
                    MessageBox.Show("No ha ingresado Nombre de Establecimiento", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtNombreEstEmp.Focus();
                }



                if (valorValida.Equals("G"))
                {
                    estane.setEstAne(idestane, codemp, desestane, tipoestane, idcia, riesgo);
                    estane.actualizarEstAne(operacion);
                    ui_listaEstAne(idcia, codemp);
                    ui_limpiarEstEmp();
                }
            }
            catch (FormatException ex)
            {
                MessageBox.Show(ex.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnEditarEstAne_Click(object sender, EventArgs e)
        {
            string idestane;
            string desestane;
            string tipoestane;
            string riesgo;


            MaesGen maesgen = new MaesGen();
            Int32 selectedCellCount = dgvEstAne.GetCellCount(DataGridViewElementStates.Selected);
            if (selectedCellCount > Convert.ToInt32(0))
            {

                idestane = dgvEstAne.Rows[dgvEstAne.SelectedCells[1].RowIndex].Cells[0].Value.ToString();
                desestane = dgvEstAne.Rows[dgvEstAne.SelectedCells[1].RowIndex].Cells[2].Value.ToString();
                riesgo = dgvEstAne.Rows[dgvEstAne.SelectedCells[1].RowIndex].Cells[4].Value.ToString();
                tipoestane = dgvEstAne.Rows[dgvEstAne.SelectedCells[1].RowIndex].Cells[5].Value.ToString();

                txtOpeEstEmp.Text = "EDITAR";
                txtCodigoEstEmp.Text = idestane;
                txtCodigoEstEmp.Enabled = false;
                txtNombreEstEmp.Text = desestane;
                txtNombreEstEmp.Enabled = true;
                radioButtonSiCR.Enabled = true;
                radioButtonNoCR.Enabled = true;

                maesgen.consultaDetMaesGen("027", tipoestane, cmbTipoEstEmp);
                if (riesgo.Equals("1"))
                {
                    radioButtonSiCR.Checked = true;
                    radioButtonNoCR.Checked = false;

                }
                else
                {
                    radioButtonSiCR.Checked = false;
                    radioButtonNoCR.Checked = true;

                }

                txtNombreEstEmp.Focus();


            }
            else
            {
                MessageBox.Show("No ha seleccionado registro a Editar", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnEliminarEstAne_Click(object sender, EventArgs e)
        {
            string idestane;
            string desestane;
            string codemp;
            string idcia;

            EstAne estane = new EstAne();
            Int32 selectedCellCount = dgvEstAne.GetCellCount(DataGridViewElementStates.Selected);
            if (selectedCellCount > Convert.ToInt32(0))
            {

                idestane = dgvEstAne.Rows[dgvEstAne.SelectedCells[1].RowIndex].Cells[0].Value.ToString();
                desestane = dgvEstAne.Rows[dgvEstAne.SelectedCells[1].RowIndex].Cells[2].Value.ToString();
                codemp = dgvEstAne.Rows[dgvEstAne.SelectedCells[1].RowIndex].Cells[6].Value.ToString();
                idcia = dgvEstAne.Rows[dgvEstAne.SelectedCells[1].RowIndex].Cells[7].Value.ToString();


                DialogResult resultado = MessageBox.Show("¿Desea eliminar el Establecimiento " + desestane + "?",
                "Consulta Importante",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);
                if (resultado == DialogResult.Yes)
                {
                    estane.eliminarEstAne(idestane, codemp, idcia);
                    ui_listaEstAne(idcia, codemp);
                }

            }
            else
            {
                MessageBox.Show("No ha seleccionado registro a Eliminar", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void btnTasaEsta_Click(object sender, EventArgs e)
        {
            string idciafile;
            string rucemp;
            string razonemp;
            string idestane;
            string desestane;
            string tipoestane;
            string riesgo;

            Int32 selectedCellCount = dgvEstAne.GetCellCount(DataGridViewElementStates.Selected);
            if (selectedCellCount > Convert.ToInt32(0))
            {
                rucemp = txtRucEmp.Text.Trim();
                razonemp = txtRazonEmp.Text.Trim();
                idestane = dgvEstAne.Rows[dgvEstAne.SelectedCells[1].RowIndex].Cells[0].Value.ToString();
                tipoestane = dgvEstAne.Rows[dgvEstAne.SelectedCells[1].RowIndex].Cells[1].Value.ToString();
                desestane = dgvEstAne.Rows[dgvEstAne.SelectedCells[1].RowIndex].Cells[2].Value.ToString();
                riesgo = dgvEstAne.Rows[dgvEstAne.SelectedCells[1].RowIndex].Cells[4].Value.ToString();
                idciafile = dgvEstAne.Rows[dgvEstAne.SelectedCells[1].RowIndex].Cells[7].Value.ToString();

                if (riesgo.Equals("1"))
                {
                    ui_tasaest ui_tasaest = new ui_tasaest();
                    ui_tasaest._FormPadre = this;
                    ui_tasaest.ui_loadTasasEstAne(idciafile, rucemp, razonemp, idestane, desestane, tipoestane);
                    ui_tasaest.Activate();
                    ui_tasaest.BringToFront();
                    ui_tasaest.ShowDialog();
                    ui_tasaest.Dispose();
                }
                else
                {
                    MessageBox.Show("El establecimiento no es un Centro de Riesgo", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

            }
            else
            {
                MessageBox.Show("No ha seleccionado Establecimiento para actualizar % Tasas", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }
    }
}