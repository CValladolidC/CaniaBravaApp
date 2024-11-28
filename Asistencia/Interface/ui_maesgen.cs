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
    public partial class ui_maesgen : Form
    {
        Funciones funciones = new Funciones();
        string cadenaConexion;

        private ui_detmaesgen form = null;

        private ui_detmaesgen FormInstance
        {
            get
            {
                if (form == null)
                {
                    form = new ui_detmaesgen();
                    form.Disposed += new EventHandler(form_Disposed);

                }

                return form;
            }
        }

        void form_Disposed(object sender, EventArgs e)
        {
            form = null;
        }

        public ui_maesgen()
        {
            InitializeComponent();

            if (funciones.VersionAssembly()) Application.ExitThread();
        }

        private void ui_maesgen_Load(object sender, EventArgs e)
        {
            ui_listaMaesGen();
        }

        private void ui_listaMaesGen()
        {
            string idmaesgen = "000";
            SqlConnection conexion = new SqlConnection();
            this.cadenaConexion = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.ConnectionString = cadenaConexion;
            conexion.Open();
            string query = "select clavemaesgen,desmaesgen,parm1maesgen,parm2maesgen,parm3maesgen,tipomaesgen,idmaesgen from maesgen ";
            query += "where idmaesgen='" + @idmaesgen + "' order by clavemaesgen asc;";

            try
            {
                using (SqlDataAdapter myDataAdapter = new SqlDataAdapter(query, conexion))
                {
                    DataSet myDataSet = new DataSet();
                    myDataAdapter.Fill(myDataSet, "tblMaesGen");
                    funciones.formatearDataGridView(dgvMaesGen);

                    dgvMaesGen.DataSource = myDataSet.Tables["tblMaesGen"];
                    dgvMaesGen.Columns[0].HeaderText = "Código";
                    dgvMaesGen.Columns[1].HeaderText = "Descripción";
                    dgvMaesGen.Columns["parm1maesgen"].Visible = false;
                    dgvMaesGen.Columns["parm2maesgen"].Visible = false;
                    dgvMaesGen.Columns["parm3maesgen"].Visible = false;
                    dgvMaesGen.Columns["idmaesgen"].Visible = false;
                    dgvMaesGen.Columns["tipomaesgen"].Visible = false;
                    dgvMaesGen.Columns[0].Width = 50;
                    dgvMaesGen.Columns[1].Width = 600;

                    dgvMaesGen.AllowUserToResizeRows = false;
                    dgvMaesGen.AllowUserToResizeColumns = false;
                    foreach (DataGridViewColumn column in dgvMaesGen.Columns)
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

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            ui_listaMaesGen();
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            GlobalVariables variables = new GlobalVariables();
            string clavemaesgen;
            string desmaesgen;
            string tipomaesgen;

            Int32 selectedCellCount = dgvMaesGen.GetCellCount(DataGridViewElementStates.Selected);
            if (selectedCellCount > Convert.ToInt32(0))
            {
                clavemaesgen = dgvMaesGen.Rows[dgvMaesGen.SelectedCells[1].RowIndex].Cells[0].Value.ToString();
                desmaesgen = dgvMaesGen.Rows[dgvMaesGen.SelectedCells[1].RowIndex].Cells[1].Value.ToString();
                tipomaesgen = dgvMaesGen.Rows[dgvMaesGen.SelectedCells[1].RowIndex].Cells[5].Value.ToString();

                if (variables.getValorTypeUsr() != "00" && clavemaesgen == "000")
                {
                    MessageBox.Show("La Tabla Común 000 MAESTROS COMUNES sólo puede ser modificada por un usuario de tipo Master", "Aviso Importante", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
                else
                {
                    ui_detmaesgen ui_detalle = this.FormInstance;
                    ui_detalle._FormPadre = this;
                    ui_detalle.maestroActivo(clavemaesgen, desmaesgen);
                    ui_detalle.Activate();

                    if (tipomaesgen.Equals("S"))
                    {
                        ui_detalle.btnNuevo.Enabled = false;
                        ui_detalle.btnEditar.Enabled = false;
                        ui_detalle.btnEliminar.Enabled = false;
                    }
                    else
                    {
                        ui_detalle.btnNuevo.Enabled = true;
                        ui_detalle.btnEditar.Enabled = true;
                        ui_detalle.btnEliminar.Enabled = true;
                    }
                    ui_detalle.BringToFront();
                    ui_detalle.ShowDialog();
                    ui_detalle.Dispose();
                }

            }
            else
            {
                MessageBox.Show("No ha seleccionado Maestro a Editar", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void toolStripForm_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void dgvMaesGen_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}