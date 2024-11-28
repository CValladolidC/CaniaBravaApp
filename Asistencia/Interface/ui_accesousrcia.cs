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
    public partial class ui_accesousrcia : Form
    {
        string cadenaConexion;

        private Form FormPadre;

        public Form _FormPadre
        {
            get { return FormPadre; }
            set { FormPadre = value; }
        }

        public ui_accesousrcia()
        {
            InitializeComponent();
        }

        private void ui_accesousrcia_Load(object sender, EventArgs e)
        {
            string squery = "Select idcia as clave,descia as descripcion from ciafile where statecia='V' order by 1 asc;";
            Funciones funciones = new Funciones();
            funciones.listaToolStripComboBox(squery, cmbCompania);
            listaCiaUsrFile();
        }

        public void usuarioActivo(string idusr, string desusr)
        {
            this.Text = desusr;
            txtIdUsr.Text = idusr;
        }

        private void listaCiaUsrFile()
        {
            string idusr = txtIdUsr.Text;
            Funciones funciones = new Funciones();
            SqlConnection conexion = new SqlConnection();
            this.cadenaConexion = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.ConnectionString = cadenaConexion;
            conexion.Open();
            string query = "Select A.idcia,B.descia,A.idusr from ciausrfile A, ciafile B where A.idcia=B.idcia and idusr='" + @idusr + "' order by 1 asc;";

            try
            {
                using (SqlDataAdapter myDataAdapter = new SqlDataAdapter(query, conexion))
                {
                    DataSet myDataSet = new DataSet();
                    myDataAdapter.Fill(myDataSet, "tblCiaUsrFile");
                    funciones.formatearDataGridView(dgvDetalle);
                    dgvDetalle.DataSource = myDataSet.Tables["tblCiaUsrFile"];
                    dgvDetalle.Columns[0].HeaderText = "Código";
                    dgvDetalle.Columns[1].HeaderText = "Compañía";
                    dgvDetalle.Columns["idusr"].Visible = false;
                    dgvDetalle.Columns[0].Width = 50;
                    dgvDetalle.Columns[1].Width = 400;

                    dgvDetalle.AllowUserToResizeRows = false;
                    dgvDetalle.AllowUserToResizeColumns = false;
                    foreach (DataGridViewColumn column in dgvDetalle.Columns)
                    {
                        column.SortMode = DataGridViewColumnSortMode.NotSortable;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }

            conexion.Close();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnGrabar_Click(object sender, EventArgs e)
        {
            string sidcia;
            string sidusr;
            if (cmbCompania.Text == String.Empty)
            {
                MessageBox.Show("No ha especificado Compañía", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                cmbCompania.Focus();
            }
            else
            {
                sidcia = cmbCompania.Text.Substring(0, 2);
                sidusr = txtIdUsr.Text;
                UsrFile usrfile = new UsrFile();
                bool existe = usrfile.VerificaCiaUsrFile(sidcia, sidusr);
                if (existe) { MessageBox.Show("La Empresa ya está asignado al Usuario.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information); return; }
                usrfile.actualizarCiaUsrFile(sidcia, sidusr);
                listaCiaUsrFile();
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            string sidcia;
            string sdescia;
            string sidusr;

            Int32 selectedCellCount =
            dgvDetalle.GetCellCount(DataGridViewElementStates.Selected);
            if (selectedCellCount > Convert.ToInt32(1))
            {
                sidcia = dgvDetalle.Rows[dgvDetalle.SelectedCells[1].RowIndex].Cells[0].Value.ToString();
                sdescia = dgvDetalle.Rows[dgvDetalle.SelectedCells[1].RowIndex].Cells[1].Value.ToString();
                sidusr = dgvDetalle.Rows[dgvDetalle.SelectedCells[1].RowIndex].Cells[2].Value.ToString();

                DialogResult resultado = MessageBox.Show("¿Desea eliminar el acceso a la Compañía " + sdescia + "?",
                "Consulta Importante",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);
                if (resultado == DialogResult.Yes)
                {
                    UsrFile usrfile = new UsrFile();
                    usrfile.eliminarCiaUsrFile(sidcia, sidusr);
                    listaCiaUsrFile();
                }
            }
            else
            {
                MessageBox.Show("No ha seleccionado registro a Eliminar", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void btnCencos_Click(object sender, EventArgs e)
        {
            Int32 selectedCellCount = dgvDetalle.GetCellCount(DataGridViewElementStates.Selected);
            if (selectedCellCount > 0)
            {
                string idcia = dgvDetalle.Rows[dgvDetalle.SelectedCells[1].RowIndex].Cells[0].Value.ToString();
                string descia = dgvDetalle.Rows[dgvDetalle.SelectedCells[1].RowIndex].Cells[1].Value.ToString();
                string idusr = txtIdUsr.Text;
                string desusr = this.Text;

                ui_accesogerencias ui_accesocencos = new ui_accesogerencias();
                ui_accesocencos._FormPadre = this;
                ui_accesocencos._descia = descia;
                ui_accesocencos._idcia = idcia;
                ui_accesocencos._idusr = idusr;
                ui_accesocencos._desusr = desusr;
                ui_accesocencos.Activate();
                ui_accesocencos.BringToFront();
                ui_accesocencos.ShowDialog();
                ui_accesocencos.Dispose();
            }
            else
            {
                MessageBox.Show("No ha seleccionado Compañía para asignar una Gerencia", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }
    }
}