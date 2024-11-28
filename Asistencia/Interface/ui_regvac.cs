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
    public partial class ui_regvac : Form
    {
        public ui_regvac()
        {
            InitializeComponent();
        }

        private void ui_regvac_Load(object sender, EventArgs e)
        {
            string squery;
            squery = "SELECT idtipoper as clave,destipoper as descripcion FROM tipoper order by 1 asc;";
            Funciones funciones = new Funciones();
            MaesGen maesgen = new MaesGen();
            funciones.listaToolStripComboBox(squery, cmbCategoria);
            cmbListar.Text = "G  GENERAL";
            txtAnio.Text = "";
            ui_ListaRegVac();
        }

        private void ui_ListaRegVac()
        {
            try
            {
                Funciones funciones = new Funciones();
                GlobalVariables variables = new GlobalVariables();
                string idcia = variables.getValorCia();
                string idtipoper = cmbCategoria.Text.Substring(0, 1);
                string tipo = cmbListar.Text.Substring(0, 1);
                string anio = txtAnio.Text;
                string cadenaAnio = string.Empty;
                if (tipo.Equals("A"))
                {
                    cadenaAnio = " and A.anio='" + @anio + "' ";
                }

                string query = " select B.idperplan,D.Parm1maesgen as cortotipodoc,B.nrodoc,";
                query = query + " CONCAT(B.apepat,' ',B.apemat,' , ',B.nombres) as nombre,";
                query = query + " A.anio,A.finivac,A.ffinvac,A.diasvac,A.idregvac ";
                query = query + " from view_regvac A  ";
                query = query + " left join perplan B on A.idperplan=B.idperplan and A.idcia=B.idcia ";
                query = query + " left join maesgen D on D.idmaesgen='002' and B.tipdoc=D.clavemaesgen ";
                query = query + " where A.idcia='" + @idcia + "' and B.idtipoper='" + @idtipoper + "' ";
                query = query + cadenaAnio;
                query = query + " order by A.anio,A.finivac desc;";
                SqlConnection conexion = new SqlConnection();
                conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
                conexion.Open();

                using (SqlDataAdapter myDataAdapter = new SqlDataAdapter(query, conexion))
                {
                    DataSet myDataSet = new DataSet();
                    myDataAdapter.Fill(myDataSet, "tblRegVac");
                    funciones.formatearDataGridView(dgvdetalle);

                    dgvdetalle.DataSource = myDataSet.Tables["tblRegVac"];
                    dgvdetalle.Columns[0].HeaderText = "Cód.Int.";
                    dgvdetalle.Columns[1].HeaderText = "Doc. Ident.";
                    dgvdetalle.Columns[2].HeaderText = "Nro.Doc.";
                    dgvdetalle.Columns[3].HeaderText = "Apellidos y Nombres";
                    dgvdetalle.Columns[4].HeaderText = "Año";
                    dgvdetalle.Columns[5].HeaderText = "F.Inicio Vac.";
                    dgvdetalle.Columns[6].HeaderText = "F.Fin Vac.";
                    dgvdetalle.Columns[7].HeaderText = "Días Vac.";
                    dgvdetalle.Columns[8].HeaderText = "Nro. Registro";

                    dgvdetalle.Columns["idperplan"].Frozen = true;
                    dgvdetalle.Columns["cortotipodoc"].Frozen = true;
                    dgvdetalle.Columns["nrodoc"].Frozen = true;
                    dgvdetalle.Columns["nombre"].Frozen = true;

                    dgvdetalle.Columns[0].Width = 50;
                    dgvdetalle.Columns[1].Width = 50;
                    dgvdetalle.Columns[2].Width = 75;
                    dgvdetalle.Columns[3].Width = 200;
                    dgvdetalle.Columns[4].Width = 75;
                    dgvdetalle.Columns[5].Width = 75;
                    dgvdetalle.Columns[6].Width = 75;
                    dgvdetalle.Columns[7].Width = 75;
                    dgvdetalle.Columns[8].Width = 75;


                }
                conexion.Close();
            }
            catch (Exception)
            {

            }



        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cmbCategoria_SelectedIndexChanged(object sender, EventArgs e)
        {
            ui_ListaRegVac();
        }

        private void txtAnio_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                ui_ListaRegVac();
            }
        }

        private void cmbCategoria_Click(object sender, EventArgs e)
        {

        }

        private void btnTodos_Click(object sender, EventArgs e)
        {
            ui_ListaRegVac();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            string buscar = txtBuscar.Text.Trim();

            if (buscar != string.Empty)
            {
                Funciones funciones = new Funciones();
                GlobalVariables variables = new GlobalVariables();
                string idcia = variables.getValorCia();
                string idtipoper = cmbCategoria.Text.Substring(0, 1);
                string anio = txtAnio.Text;
                string cadenaBusqueda = string.Empty;

                if (radioButtonCodigoInterno.Checked && txtBuscar.Text.Trim() != string.Empty)
                {
                    cadenaBusqueda = " and A.idperplan='" + @buscar + "' ";
                }
                else
                {
                    if (radioButtonNombre.Checked && txtBuscar.Text.Trim() != string.Empty)
                    {
                        cadenaBusqueda = " and CONCAT(B.apepat,' ',B.apemat,' , ',B.nombres) like '%" + @buscar + "%' ";
                    }
                    else
                    {
                        if (radioButtonNroDoc.Checked && txtBuscar.Text.Trim() != string.Empty)
                        {
                            cadenaBusqueda = " and B.nrodoc='" + @buscar + "' ";
                        }

                    }

                }
                string query = " select B.idperplan,D.Parm1maesgen as cortotipodoc,B.nrodoc,";
                query = query + " CONCAT(B.apepat,' ',B.apemat,' , ',B.nombres) as nombre,";
                query = query + " A.anio,A.finivac,A.ffinvac,A.diasvac,A.idregvac ";
                query = query + " from view_regvac A  ";
                query = query + " left join perplan B on A.idperplan=B.idperplan and A.idcia=B.idcia ";
                query = query + " left join maesgen D on D.idmaesgen='002' and B.tipdoc=D.clavemaesgen ";
                query = query + " where A.idcia='" + @idcia + "' and B.idtipoper='" + @idtipoper + "' ";
                query = query + " and A.anio='" + @anio + "' " + @cadenaBusqueda;
                query = query + " order by A.finivac desc;";
                SqlConnection conexion = new SqlConnection();
                conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
                conexion.Open();
                try
                {
                    using (SqlDataAdapter myDataAdapter = new SqlDataAdapter(query, conexion))
                    {
                        DataSet myDataSet = new DataSet();
                        myDataAdapter.Fill(myDataSet, "tblRegVac");
                        funciones.formatearDataGridView(dgvdetalle);

                        dgvdetalle.DataSource = myDataSet.Tables["tblRegVac"];
                        dgvdetalle.Columns[0].HeaderText = "Cód.Int.";
                        dgvdetalle.Columns[1].HeaderText = "Doc. Ident.";
                        dgvdetalle.Columns[2].HeaderText = "Nro.Doc.";
                        dgvdetalle.Columns[3].HeaderText = "Apellidos y Nombres";
                        dgvdetalle.Columns[4].HeaderText = "Año";
                        dgvdetalle.Columns[5].HeaderText = "F.Inicio Vac.";
                        dgvdetalle.Columns[6].HeaderText = "F.Fin Vac.";
                        dgvdetalle.Columns[7].HeaderText = "Días Vac.";
                        dgvdetalle.Columns[8].HeaderText = "Nro. Registro";

                        dgvdetalle.Columns["idperplan"].Frozen = true;
                        dgvdetalle.Columns["cortotipodoc"].Frozen = true;
                        dgvdetalle.Columns["nrodoc"].Frozen = true;
                        dgvdetalle.Columns["nombre"].Frozen = true;

                        dgvdetalle.Columns[0].Width = 50;
                        dgvdetalle.Columns[1].Width = 50;
                        dgvdetalle.Columns[2].Width = 75;
                        dgvdetalle.Columns[3].Width = 200;
                        dgvdetalle.Columns[4].Width = 75;
                        dgvdetalle.Columns[5].Width = 75;
                        dgvdetalle.Columns[6].Width = 75;
                        dgvdetalle.Columns[7].Width = 75;
                        dgvdetalle.Columns[8].Width = 75;


                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }

                conexion.Close();
            }
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            ui_ListaRegVac();
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            Exporta exporta = new Exporta();
            exporta.Excel_FromDataGridView(dgvdetalle);
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            GlobalVariables variables = new GlobalVariables();
            string idcia = variables.getValorCia();
            string idtipoper = cmbCategoria.Text.Substring(0, 1);
            ui_updregvac ui_detalle = new ui_updregvac();
            ui_detalle._FormPadre = this;
            ui_detalle.setValores(idcia, idtipoper);
            ui_detalle.ui_newRegVac();
            ui_detalle.Activate();
            ui_detalle.BringToFront();
            ui_detalle.ShowDialog();
            ui_detalle.Dispose();

        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            Int32 selectedCellCount =
            dgvdetalle.GetCellCount(DataGridViewElementStates.Selected);
            if (selectedCellCount > 0)
            {
                GlobalVariables variables = new GlobalVariables();
                Funciones funciones = new Funciones();
                string idcia = variables.getValorCia();
                string idtipoper = cmbCategoria.Text.Substring(0, 1);
                string idperplan = dgvdetalle.Rows[dgvdetalle.SelectedCells[1].RowIndex].Cells[0].Value.ToString();
                string anio = dgvdetalle.Rows[dgvdetalle.SelectedCells[1].RowIndex].Cells[4].Value.ToString();
                string finivac = dgvdetalle.Rows[dgvdetalle.SelectedCells[1].RowIndex].Cells[5].Value.ToString();
                string ffinvac = dgvdetalle.Rows[dgvdetalle.SelectedCells[1].RowIndex].Cells[6].Value.ToString();
                string diasvac = dgvdetalle.Rows[dgvdetalle.SelectedCells[1].RowIndex].Cells[7].Value.ToString();
                string idregvac = dgvdetalle.Rows[dgvdetalle.SelectedCells[1].RowIndex].Cells[8].Value.ToString();
                if (idregvac != string.Empty)
                {
                    ui_updregvac ui_detalle = new ui_updregvac();
                    ui_detalle._FormPadre = this;
                    ui_detalle.setValores(idcia, idtipoper);
                    ui_detalle.Activate();
                    ui_detalle.ui_loadDatosRegVac(idperplan, anio, finivac, ffinvac, diasvac, idregvac);
                    ui_detalle.BringToFront();
                    ui_detalle.ShowDialog();
                    ui_detalle.Dispose();
                }
                else
                {
                    MessageBox.Show("Item no modificable por esta ventana, ir a Parte de Planilla", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
            }
            else
            {
                MessageBox.Show("No ha seleccionado registro a Editar", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            RegVac regvac = new RegVac();
            Int32 selectedCellCount = dgvdetalle.GetCellCount(DataGridViewElementStates.Selected);
            if (selectedCellCount > 0)
            {
                GlobalVariables variables = new GlobalVariables();
                Funciones funciones = new Funciones();
                string idcia = variables.getValorCia();
                string idperplan = dgvdetalle.Rows[dgvdetalle.SelectedCells[1].RowIndex].Cells[0].Value.ToString();
                string trabajador = dgvdetalle.Rows[dgvdetalle.SelectedCells[1].RowIndex].Cells[3].Value.ToString();
                string idregvac = dgvdetalle.Rows[dgvdetalle.SelectedCells[1].RowIndex].Cells[8].Value.ToString();
                if (idregvac != string.Empty)
                {
                    DialogResult resultado = MessageBox.Show("¿Desea eliminar el registro vacacional Nro." + idregvac + " del trabajador " + trabajador + "?",
                    "Consulta Importante",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);
                    if (resultado == DialogResult.Yes)
                    {
                        regvac.eliminarRegVac(idcia, idregvac);
                        ui_ListaRegVac();
                    }
                }
                else
                {
                    MessageBox.Show("Item no se puede eliminar por esta ventana, ir a Parte de Planilla", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }

            }
            else
            {
                MessageBox.Show("No ha seleccionado registro a Eliminar", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void cmbListar_Click(object sender, EventArgs e)
        {

        }

        private void cmbListar_SelectedIndexChanged(object sender, EventArgs e)
        {
            string tipo = cmbListar.Text.Substring(0, 1);
            if (tipo.Equals("A"))
            {
                txtAnio.Enabled = true;
                txtAnio.Text = Convert.ToString(DateTime.Now.Year);
            }
            else
            {
                txtAnio.Enabled = false;
                txtAnio.Text = "";
            }
            ui_ListaRegVac();
        }
    }
}