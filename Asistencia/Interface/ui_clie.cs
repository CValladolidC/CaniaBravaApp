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
    public partial class ui_clie : Form
    {
        public ui_clie()
        {
            InitializeComponent();
        }

        private void ui_clie_Load(object sender, EventArgs e)
        {
            ui_ListaClie();
        }
        
        public void ui_calculaNumRegistros()
        {
            GlobalVariables variables = new GlobalVariables();
            Clie clie = new Clie();
            Funciones funciones = new Funciones();

            txtRegTotal.Text = string.Empty;
            string numreg = clie.getNumeroRegistrosClie();
            txtRegTotal.Text = funciones.replicateCadena(" ", 15 - numreg.Trim().Length) + numreg.Trim() + funciones.replicateCadena(" ", 20) + "Registrados";
            string numregencontrados = Convert.ToString(dgvData.RowCount);
            txtRegEncontrados.Text = funciones.replicateCadena(" ", 15 - numregencontrados.Trim().Length) + numregencontrados.Trim() + funciones.replicateCadena(" ", 20) + " De acuerdo al criterio de búsqueda";
        }

        private void ui_ListaClie()
        {
            Funciones funciones = new Funciones();
            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();
            string query =  " select codclie,desclie,rucclie,dniclie,dirclie1,dirclie2,";
            query = query + " dirclie3,nomcontac,localidad,pais,telcontac1,telcontac2,";
            query = query + " telcontac3,cargocontac,comentario,faxcontac,mailcontac,estado,fcrea,fmod,";
            query = query + " usuario,tippreven from clie ";
            query = query + " order by codclie asc;";
            try
            {
                using (SqlDataAdapter myDataAdapter = new SqlDataAdapter(query, conexion))
                {
                    DataSet myDataSet = new DataSet();
                    myDataAdapter.Fill(myDataSet, "tblData");
                    funciones.formatearDataGridView(dgvData);
                    dgvData.DataSource = myDataSet.Tables["tblData"];
                    dgvData.Columns[0].HeaderText = "Codigo";
                    dgvData.Columns[1].HeaderText = "Apellidos y Nombres ó Razón Social";
                    dgvData.Columns[2].HeaderText = "R.U.C.";
                    dgvData.Columns[3].HeaderText = "Estado";
                    dgvData.Columns["dniclie"].Visible = false;
                    dgvData.Columns["dirclie1"].Visible = false;
                    dgvData.Columns["dirclie2"].Visible = false;
                    dgvData.Columns["dirclie3"].Visible = false;
                    dgvData.Columns["nomcontac"].Visible = false;
                    dgvData.Columns["localidad"].Visible = false;
                    dgvData.Columns["pais"].Visible = false;
                    dgvData.Columns["telcontac1"].Visible = false;
                    dgvData.Columns["telcontac2"].Visible = false;
                    dgvData.Columns["telcontac3"].Visible = false;
                    dgvData.Columns["cargocontac"].Visible = false;
                    dgvData.Columns["mailcontac"].Visible = false;
                    dgvData.Columns["comentario"].Visible = false;
                    dgvData.Columns["faxcontac"].Visible = false;
                    dgvData.Columns["fcrea"].Visible = false;
                    dgvData.Columns["fmod"].Visible = false;
                    dgvData.Columns["usuario"].Visible = false;
                    dgvData.Columns["tippreven"].Visible = false;
                    
                    dgvData.Columns[0].Width = 70;
                    dgvData.Columns[1].Width = 350;
                    dgvData.Columns[2].Width = 100;
                    dgvData.Columns[3].Width = 100;
                    dgvData.Columns[17].Width = 100;
                }
                ui_calculaNumRegistros();

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
            ui_ListaClie();
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            ui_updclie ui_updclie = new ui_updclie();
            ui_updclie._FormPadre = this;
            ui_updclie.Activate();
            ui_updclie.setValores("ui_clie");
            ui_updclie.agregar();
            ui_updclie.ui_actualizaComboBox();
            ui_updclie.BringToFront();
            ui_updclie.ShowDialog();
            ui_updclie.Dispose();
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            Int32 selectedCellCount =dgvData.GetCellCount(DataGridViewElementStates.Selected);
            if (selectedCellCount > 0)
            {
                string codclie = dgvData.Rows[dgvData.SelectedCells[1].RowIndex].Cells[0].Value.ToString();
                ui_updclie ui_updclie = new ui_updclie();
                ui_updclie._FormPadre = this;
                ui_updclie.Activate();
                ui_updclie.setValores("ui_clie");
                ui_updclie.ui_actualizaComboBox();
                ui_updclie.BringToFront();
                ui_updclie.editar(codclie);
                ui_updclie.ShowDialog();
                ui_updclie.Dispose();
            }
            else
            {
                MessageBox.Show("No ha seleccionado registro a Editar", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            string buscar = txtBuscar.Text.Trim();

            if (buscar != string.Empty)
            {
                Funciones funciones = new Funciones();
                txtRegEncontrados.Text = string.Empty;
                string cadenaBusqueda = string.Empty;

                if (radioButtonCodigoInterno.Checked && txtBuscar.Text.Trim() != string.Empty)
                {
                    cadenaBusqueda = " codclie='" + @buscar + "' ";
                }
                else
                {
                    if (radioButtonNombre.Checked && txtBuscar.Text.Trim() != string.Empty)
                    {
                        cadenaBusqueda = " desclie like '%" + @buscar + "%' ";
                    }
                    else
                    {
                        cadenaBusqueda = " rucclie='" + @buscar + "' ";
                    }

                }

                string query = " select codclie,desclie,rucclie,dniclie,dirclie1,dirclie2,";
                query = query + " dirclie3,nomcontac,localidad,pais,telcontac1,telcontac2,";
                query = query + " telcontac3,cargocontac,comentario,faxcontac,mailcontac,estado,fcrea,fmod,";
                query = query + " usuario,tippreven from clie ";
                query = query + " where " + @cadenaBusqueda + "order by codclie asc;";
           
                SqlConnection conexion = new SqlConnection();
                conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
                conexion.Open();
                try
                {
                    using (SqlDataAdapter myDataAdapter = new SqlDataAdapter(query, conexion))
                    {
                        DataSet myDataSet = new DataSet();
                        myDataAdapter.Fill(myDataSet, "tblData");
                        funciones.formatearDataGridView(dgvData);
                        dgvData.DataSource = myDataSet.Tables["tblData"];
                        dgvData.Columns[0].HeaderText = "Codigo";
                        dgvData.Columns[1].HeaderText = "Apellidos y Nombres ó Razón Social";
                        dgvData.Columns[2].HeaderText = "R.U.C.";
                        dgvData.Columns[3].HeaderText = "Estado";
                        dgvData.Columns["dniclie"].Visible = false;
                        dgvData.Columns["dirclie1"].Visible = false;
                        dgvData.Columns["dirclie2"].Visible = false;
                        dgvData.Columns["dirclie3"].Visible = false;
                        dgvData.Columns["nomcontac"].Visible = false;
                        dgvData.Columns["localidad"].Visible = false;
                        dgvData.Columns["pais"].Visible = false;
                        dgvData.Columns["telcontac1"].Visible = false;
                        dgvData.Columns["telcontac2"].Visible = false;
                        dgvData.Columns["telcontac3"].Visible = false;
                        dgvData.Columns["cargocontac"].Visible = false;
                        dgvData.Columns["mailcontac"].Visible = false;
                        dgvData.Columns["comentario"].Visible = false;
                        dgvData.Columns["faxcontac"].Visible = false;
                        dgvData.Columns["fcrea"].Visible = false;
                        dgvData.Columns["fmod"].Visible = false;
                        dgvData.Columns["usuario"].Visible = false;
                        dgvData.Columns["tippreven"].Visible = false;

                        dgvData.Columns[0].Width = 70;
                        dgvData.Columns[1].Width = 350;
                        dgvData.Columns[2].Width = 100;
                        dgvData.Columns[3].Width = 100;
                        dgvData.Columns[17].Width = 100;
                    }

                    ui_calculaNumRegistros();

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }

                conexion.Close();
            }
        }

        private void btnTodos_Click(object sender, EventArgs e)
        {
            ui_ListaClie();
        }

        private void radioButtonCodigoInterno_CheckedChanged(object sender, EventArgs e)
        {
            txtBuscar.Text = string.Empty;
            txtBuscar.Focus();
        }

        private void radioButtonRuc_CheckedChanged(object sender, EventArgs e)
        {
            txtBuscar.Text = string.Empty;
            txtBuscar.Focus();
        }

        private void radioButtonNombre_CheckedChanged(object sender, EventArgs e)
        {
            txtBuscar.Text = string.Empty;
            txtBuscar.Focus();
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            Exporta exporta = new Exporta();
            exporta.Excel_FromDataGridView(dgvData);
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            Int32 selectedCellCount =
            dgvData.GetCellCount(DataGridViewElementStates.Selected);
            if (selectedCellCount > 0)
            {
                string codclie = dgvData.Rows[dgvData.SelectedCells[1].RowIndex].Cells[0].Value.ToString();
                string desclie = dgvData.Rows[dgvData.SelectedCells[1].RowIndex].Cells[1].Value.ToString();
                DialogResult resultado = MessageBox.Show("¿Desea eliminar el Cliente " + @desclie + "?",
                "Consulta Importante",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);
                if (resultado == DialogResult.Yes)
                {
                    Clie clie = new Clie();
                    clie.delClie(codclie);
                    this.ui_ListaClie();
                }


            }
            else
            {
                MessageBox.Show("No ha seleccionado registro a Eliminar", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }
    }
}

