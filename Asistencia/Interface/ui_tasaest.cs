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
    public partial class ui_tasaest : Form
    {
        private Form FormPadre;

        public Form _FormPadre
        {
            get { return FormPadre; }
            set { FormPadre = value; }
        }

        public ui_tasaest()
        {
            InitializeComponent();
        }

        private void ui_tasaest_Load(object sender, EventArgs e)
        {

        }

        public void ui_listaTasasEstAne(string idcia, string codemp, string idestane)
        {
            Funciones funciones = new Funciones();
            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();
            string query = "select tasa,idestane,codemp,idciafile from tasaest where codemp='" + @codemp + "' and idciafile='" + @idcia + "' and idestane='" + @idestane + "' order by tasa asc;";
            try
            {
                using (SqlDataAdapter myDataAdapter = new SqlDataAdapter(query, conexion))
                {
                    DataSet myDataSet = new DataSet();
                    myDataAdapter.Fill(myDataSet, "tblTasa");
                    funciones.formatearDataGridView(dgvTasa);
                    dgvTasa.DataSource = myDataSet.Tables["tblTasa"];
                    dgvTasa.Columns[0].HeaderText = "% Tasa SCTR - ESSALUD";
                    dgvTasa.Columns["idestane"].Visible = false;
                    dgvTasa.Columns["codemp"].Visible = false;
                    dgvTasa.Columns["idciafile"].Visible = false;
                    dgvTasa.Columns[0].Width = 150;

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }

            conexion.Close();
        }

        public void ui_loadTasasEstAne(string idciafile, string rucEmp, string RazonEmp, string idestane, string desestane, string tipoestane)
        {
            txtIdCiaFile.Text = idciafile;
            txtRucEmp.Text = rucEmp;
            txtRazonEmp.Text = RazonEmp;
            txtCodigoEstEmp.Text = idestane;
            txtNombreEstEmp.Text = desestane;
            txtTipoEstEmp.Text = tipoestane;
            ui_listaTasasEstAne(idciafile, rucEmp, idestane);
            txtTasa.Text = "0";
            txtTasa.Focus();
        }

        private void btnGrabarEstAne_Click(object sender, EventArgs e)
        {
            try
            {
                TasaEst tasaest = new TasaEst();

                string idcia = txtIdCiaFile.Text.Trim();
                string codemp = txtRucEmp.Text.Trim();
                string idestane = txtCodigoEstEmp.Text.Trim();
                float tasa = float.Parse(txtTasa.Text);
                string valorValida = "G";


                if (txtTasa.Text == string.Empty && valorValida == "G")
                {
                    valorValida = "B";
                    MessageBox.Show("No ha ingresado Tasa de Riesgo", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtTasa.Focus();
                }

                if ((tasa > 100 || tasa < 0) && valorValida == "G")
                {
                    valorValida = "B";
                    MessageBox.Show("Tasa de Riesgo fuera del intervalo aceptado [0-100]", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtTasa.Focus();
                }

                if (tasa == 0 && valorValida == "G")
                {
                    valorValida = "B";
                    MessageBox.Show("Tasa de Riesgo no puede tener valor cero", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtTasa.Focus();
                }

                if (valorValida.Equals("G"))
                {
                    tasaest.setTasaEst(idestane, codemp, idcia, txtTasa.Text.Trim());
                    tasaest.actualizarTasaEst();
                    ui_listaTasasEstAne(idcia, codemp, idestane);
                    txtTasa.Text = "0";
                    txtTasa.Focus();
                }
            }
            catch (FormatException ex)
            {
                MessageBox.Show(ex.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtTasa.Text = "0";
                txtTasa.Focus();
            }
        }

        private void btnEliminarEstAne_Click(object sender, EventArgs e)
        {
            string idestane;
            string codemp;
            string idciafile;
            string tasa;

            TasaEst tasaest = new TasaEst();
            Int32 selectedCellCount = dgvTasa.GetCellCount(DataGridViewElementStates.Selected);
            if (selectedCellCount > Convert.ToInt32(0))
            {
                tasa = dgvTasa.Rows[dgvTasa.SelectedCells[1].RowIndex].Cells[0].Value.ToString();
                idestane = dgvTasa.Rows[dgvTasa.SelectedCells[1].RowIndex].Cells[1].Value.ToString();
                codemp = dgvTasa.Rows[dgvTasa.SelectedCells[1].RowIndex].Cells[2].Value.ToString();
                idciafile = dgvTasa.Rows[dgvTasa.SelectedCells[1].RowIndex].Cells[3].Value.ToString();

                DialogResult resultado = MessageBox.Show("¿Desea eliminar la tasa " + tasa + " % ?",
                "Consulta Importante",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);
                if (resultado == DialogResult.Yes)
                {
                    tasaest.eliminarTasaEst(idestane, codemp, idciafile, tasa);
                    ui_listaTasasEstAne(idciafile, codemp, idestane);
                }

            }
            else
            {
                MessageBox.Show("No ha seleccionado registro a Eliminar", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}