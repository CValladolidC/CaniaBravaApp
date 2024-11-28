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
    public partial class ui_cencosper : Form
    {
        public string _idcia;
        public string _idperplan;
        public string _nombre;

        private Form FormPadre;

        public Form _FormPadre
        {
            get { return FormPadre; }
            set { FormPadre = value; }
        }

        public ui_cencosper()
        {
            InitializeComponent();
        }

        private void ui_cencosper_Load(object sender, EventArgs e)
        {
            lblNombre.Text = _idperplan + " - " + _nombre;
            Funciones funciones = new Funciones();
            string query = "SELECT idcencos as clave,descencos as descripcion FROM cencos WHERE idcia='" + @_idcia + "' and statecencos='V';";
            funciones.listaComboBox(query, cmbCentroCosto, "");
            ui_listaCenPer();
        }

        private void ui_limpiarCenPer()
        {
            cmbCentroCosto.Text = string.Empty;
            txtPorcentaje.Text = string.Empty;
        }

        public void ui_listaCenPer()
        {
            Funciones funciones = new Funciones();
            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();
            string query = "select A.idcencos,B.descencos,A.porcentaje,A.idperplan,A.idcia ";
            query = query + " from cenper A left join cencos B on A.idcia=B.idcia and ";
            query = query + " A.idcencos=B.idcencos where A.idcia='" + _idcia + "' ";
            query = query + " and A.idperplan='" + _idperplan + "' order by A.idcencos asc;";
            try
            {
                using (SqlDataAdapter myDataAdapter = new SqlDataAdapter(query, conexion))
                {
                    DataSet myDataSet = new DataSet();
                    myDataAdapter.Fill(myDataSet, "tblCenPer");
                    funciones.formatearDataGridView(dgvCenPer);
                    dgvCenPer.DataSource = myDataSet.Tables["tblCenPer"];
                    dgvCenPer.Columns[0].HeaderText = "Código";
                    dgvCenPer.Columns[1].HeaderText = "Centro de Costo";
                    dgvCenPer.Columns[2].HeaderText = "Porcentaje";
                    dgvCenPer.Columns["idcia"].Visible = false;
                    dgvCenPer.Columns["idperplan"].Visible = false;
                    dgvCenPer.Columns[0].Width = 100;
                    dgvCenPer.Columns[1].Width = 300;
                    dgvCenPer.Columns[2].Width = 100;

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }



            conexion.Close();
        }

        private void btnGrabar_Click(object sender, EventArgs e)
        {
            try
            {
                GlobalVariables globalvariables = new GlobalVariables();
                Funciones funciones = new Funciones();
                CenPer cenper = new CenPer();
                string idcencos = funciones.getValorComboBox(cmbCentroCosto, 2);
                float porcentaje = float.Parse(txtPorcentaje.Text);
                string valorValida = "G";

                if (cmbCentroCosto.Text == string.Empty && valorValida == "G")
                {
                    valorValida = "B";
                    MessageBox.Show("No ha seleccionado Centro de Costo", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    cmbCentroCosto.Focus();
                }

                if (cmbCentroCosto.Text != string.Empty && valorValida == "G")
                {
                    string squery = "SELECT idcencos as clave,descencos as descripcion FROM cencos WHERE idcencos='" + @idcencos + "' and idcia='" + _idcia + "';";
                    string resultado = funciones.verificaItemComboBox(squery, cmbCentroCosto);

                    if (resultado.Trim() == string.Empty)
                    {
                        valorValida = "B";
                        MessageBox.Show("Dato incorrecto en Centro de Costo", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        cmbCentroCosto.Focus();
                    }
                }

                if (valorValida.Equals("G"))
                {
                    cenper.setCenPer(_idperplan, _idcia, idcencos, porcentaje);
                    cenper.actualizarCenPer();
                    ui_listaCenPer();
                    ui_limpiarCenPer();
                }
            }
            catch (FormatException ex)
            {
                MessageBox.Show(ex.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            string idcencos;
            string idcia;
            string idperplan;
            string descencos;

            CenPer cenper = new CenPer();
            Int32 selectedCellCount = dgvCenPer.GetCellCount(DataGridViewElementStates.Selected);
            if (selectedCellCount > Convert.ToInt32(0))
            {
                idcencos = dgvCenPer.Rows[dgvCenPer.SelectedCells[1].RowIndex].Cells[0].Value.ToString();
                descencos = dgvCenPer.Rows[dgvCenPer.SelectedCells[1].RowIndex].Cells[1].Value.ToString();
                idperplan = dgvCenPer.Rows[dgvCenPer.SelectedCells[1].RowIndex].Cells[3].Value.ToString();
                idcia = dgvCenPer.Rows[dgvCenPer.SelectedCells[1].RowIndex].Cells[4].Value.ToString();

                DialogResult resultado = MessageBox.Show("¿Desea eliminar el porcentaje asignado al Centro de Costo '" + @descencos + "'?",
                "Consulta Importante",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);
                if (resultado == DialogResult.Yes)
                {
                    cenper.eliminarCenPer(idperplan, idcia, idcencos);
                    ui_listaCenPer();
                }


            }
            else
            {
                MessageBox.Show("No ha seleccionado registro a Eliminar", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            ((ui_asientos)FormPadre).ui_listaPer();
            this.Close();
        }
    }
}