using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CaniaBrava.Interface
{
    public partial class ui_controlling_ctascontables : Form
    {
        string query = "";
        public delegate void pasar(string cta, string descuenta);
        public event pasar pasado;
        Funciones funciones = new Funciones();
        SqlConnection conexion = new SqlConnection();

        public ui_controlling_ctascontables()
        {
            InitializeComponent();
        }

        private void ui_controlling_ctascontables_Load(object sender, EventArgs e)
        {
            ui_Lista();
        }

        private void ui_Lista()
        {

            query = " SELECT DISTINCT(cuencon), descuencon FROM Asistencia.dbo.maescontrolling WHERE cuencon <> ''; ";

            loadqueryDatos(query);
        }

        private void loadqueryDatos(string query)
        {
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();

            try
            {
                using (SqlDataAdapter myDataAdapter = new SqlDataAdapter(query, conexion))
                {
                    DataSet myDataSet = new DataSet();
                    myDataAdapter.Fill(myDataSet, "tblPerPlan");
                    funciones.formatearDataGridView(dgvdetalle);

                    dgvdetalle.DataSource = myDataSet.Tables["tblPerPlan"];
                    dgvdetalle.Columns[0].HeaderText = "Cuenta Contable";
                    dgvdetalle.Columns[1].HeaderText = "Descripcion Cuenta";

                    //dgvdetalle.Columns[0].Width = 0;
                    dgvdetalle.Columns[0].Width = 120;
                    dgvdetalle.Columns[1].Width = 250;

                }


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }

            conexion.Close();
        }

        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            BindingSource bs = new BindingSource();

            bs.DataSource = dgvdetalle.DataSource;
            bs.Filter = "descuencon Like '%" + txtBuscar.Text + "%'";
            dgvdetalle.DataSource = bs;
        }

        private void dgvdetalle_DoubleClick(object sender, EventArgs e)
        {
            Int32 selectedCellCount =
            dgvdetalle.GetCellCount(DataGridViewElementStates.Selected);
            if (selectedCellCount > 0)
            {
                pasado(dgvdetalle.CurrentRow.Cells[0].Value.ToString(), dgvdetalle.CurrentRow.Cells[1].Value.ToString());
                this.Dispose();
            }
            else
            {
                MessageBox.Show("No ha seleccionado registro", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void dgvdetalle_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            Int32 selectedCellCount =
           dgvdetalle.GetCellCount(DataGridViewElementStates.Selected);
            if (selectedCellCount > 0)
            {
                pasado(dgvdetalle.CurrentRow.Cells[0].Value.ToString(), dgvdetalle.CurrentRow.Cells[1].Value.ToString());
            }
            else
            {
                MessageBox.Show("No ha seleccionado registro", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
