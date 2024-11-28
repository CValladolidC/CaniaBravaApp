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
    public partial class ui_updsisparm : Form
    {
        string _ope;
        string _idsisparm;

        private Form FormPadre;

        public Form _FormPadre
        {
            get { return FormPadre; }
            set { FormPadre = value; }
        }

        public ui_updsisparm()
        {
            InitializeComponent();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            ((ui_sisparm)FormPadre).btnActualizar.PerformClick();
            this.Close();
        }

        private void txtInicio_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                if (UtileriasFechas.IsDate(txtInicio.Text))
                {
                    e.Handled = true;
                    txtPorcentaje.Focus();
                }
                else
                {
                    MessageBox.Show("Fecha de Inicio no válida", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    e.Handled = true;
                    txtInicio.Focus();
                }
            }
        }

        private void txtFin_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                if (UtileriasFechas.IsDate(txtFin.Text))
                {
                    e.Handled = true;
                    btnGrabar.Focus();
                }
                else
                {
                    MessageBox.Show("Fecha de Fin no válida", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    e.Handled = true;
                    txtFin.Focus();
                }
            }
        }

        private void ui_limpiar()
        {
            
            this._ope = string.Empty;
            txtInicio.Text = string.Empty;
            txtFin.Text = string.Empty;
            radioButtonImp.Checked = false;
            radioButtonPor.Checked = false;
            txtImporte.Text = string.Empty;
            txtPorcentaje.Text = string.Empty;
            txtInicio.Enabled = false;
            txtFin.Enabled = false;
            txtImporte.Enabled = false;
            txtPorcentaje.Enabled = false;

        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            string query;
            string idsisparm = this._idsisparm;

            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();

            query = "SELECT * from detsisparm where state='V' and idsisparm='" + @idsisparm + "'";
            try
            {

                SqlCommand myCommand_table = new SqlCommand(query, conexion);
                DataTable dt_table = new DataTable();
                dt_table.Load(myCommand_table.ExecuteReader());
                myCommand_table.Dispose();

                if (dt_table.Rows.Count > Convert.ToInt16(0))
                {
                    MessageBox.Show("Para ingresar valor a la Variable de Planilla debe finalizar el anterior", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    ui_limpiar();
                    this._ope = "AGREGAR";
                    txtInicio.Text = DateTime.Now.ToString("dd/MM/yyyy"); 
                    txtInicio.Enabled = true;
                    txtFin.Enabled = false;
                    radioButtonImp.Enabled = true;
                    radioButtonPor.Enabled = true;
                    radioButtonImp.Checked = false;
                    radioButtonPor.Checked = true;
                    txtImporte.Enabled = false;
                    txtPorcentaje.Enabled = true;
                    txtInicio.Focus();
                }

            }
            catch (SqlException ex)
            {
                MessageBox.Show("A ocurrido un error en la consulta [" + ex.Message + "]", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);

            }

            conexion.Close();
        }

        internal void ui_load(string idsisparm,string dessisparm)
        {
            this.Text = idsisparm + " - " + dessisparm;
            this._idsisparm = idsisparm;
            this._ope = string.Empty;
            this.ui_lista(idsisparm);
        }
        
        internal void ui_lista(string idsisparm)
        {

            Funciones funciones = new Funciones();
            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();
            string query = "select fini,ffin,porcentaje,importe,idsisparm,iddetsisparm,state,ispor from detsisparm where idsisparm='" + @idsisparm + "' order by fini desc;";
            try
            {
                using (SqlDataAdapter myDataAdapter = new SqlDataAdapter(query, conexion))
                {
                    DataSet myDataSet = new DataSet();
                    myDataAdapter.Fill(myDataSet, "tblSisParm");
                    funciones.formatearDataGridView(dgvDetalle);

                    dgvDetalle.DataSource = myDataSet.Tables["tblSisParm"];
                    dgvDetalle.Columns[0].HeaderText = "Fecha Inicio";
                    dgvDetalle.Columns[1].HeaderText = "Fecha Fin";
                    dgvDetalle.Columns[2].HeaderText = "Porcentaje";
                    dgvDetalle.Columns[3].HeaderText = "Importe";

                    dgvDetalle.Columns["idsisparm"].Visible = false;
                    dgvDetalle.Columns["iddetsisparm"].Visible = false;
                    dgvDetalle.Columns["state"].Visible = false;
                    dgvDetalle.Columns["ispor"].Visible = false;

                    dgvDetalle.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    dgvDetalle.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    dgvDetalle.Columns[2].DefaultCellStyle.Format = "###,###.####";
                    dgvDetalle.Columns[3].DefaultCellStyle.Format = "###,###.####";
        

                    dgvDetalle.Columns[0].Width = 100;
                    dgvDetalle.Columns[1].Width = 100;
                    dgvDetalle.Columns[2].Width = 100;
                    dgvDetalle.Columns[3].Width = 100;

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }



            conexion.Close();
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            string iddetsisparm;
            string fini;
            string ffin;
            string importe;
            string porcentaje;
            string ispor;
           
            Int32 selectedCellCount = dgvDetalle.GetCellCount(DataGridViewElementStates.Selected);
            if (selectedCellCount > Convert.ToInt32(0))
            {

                fini = dgvDetalle.Rows[dgvDetalle.SelectedCells[1].RowIndex].Cells[0].Value.ToString();
                ffin = dgvDetalle.Rows[dgvDetalle.SelectedCells[1].RowIndex].Cells[1].Value.ToString();
                porcentaje = dgvDetalle.Rows[dgvDetalle.SelectedCells[1].RowIndex].Cells[2].Value.ToString();
                importe = dgvDetalle.Rows[dgvDetalle.SelectedCells[1].RowIndex].Cells[3].Value.ToString();
                iddetsisparm = dgvDetalle.Rows[dgvDetalle.SelectedCells[1].RowIndex].Cells[5].Value.ToString();
                ispor = dgvDetalle.Rows[dgvDetalle.SelectedCells[1].RowIndex].Cells[7].Value.ToString();
                
                this._ope = "EDITAR";
                txtCodigo.Text = iddetsisparm;
                txtInicio.Text = fini;
                txtFin.Text = ffin;
                txtInicio.Enabled = false;
                txtFin.Enabled = true;
                
                if (ispor.Equals("1"))
                {

                    radioButtonImp.Checked = false;
                    radioButtonPor.Checked = true;
                    txtImporte.Text = "0";
                    txtPorcentaje.Text = porcentaje;
                }
                else
                {
                    radioButtonImp.Checked = true;
                    radioButtonPor.Checked = false;
                    txtImporte.Text = importe;
                    txtPorcentaje.Text = "0";
                }
                
                radioButtonImp.Enabled = false;
                radioButtonPor.Enabled = false;
                txtImporte.Enabled = false;
                txtPorcentaje.Enabled = false;
                txtFin.Focus();


            }
            else
            {
                MessageBox.Show("No ha seleccionado registro a Editar", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void ui_updsisparm_Load(object sender, EventArgs e)
        {
            
        }

        private void btnGrabar_Click(object sender, EventArgs e)
        {

            try
            {
                Funciones funciones = new Funciones();
                DetSisParm detsisparm = new DetSisParm();

                string operacion = this._ope;
                string idsisparm = this._idsisparm;
                string iddetsisparm = txtCodigo.Text;
                string fini = txtInicio.Text;
                string ffin = txtFin.Text;
                float importe;
                float porcentaje;
                string ispor;

                if (radioButtonImp.Checked)
                {
                    ispor = "0";
                    importe = float.Parse(txtImporte.Text);
                    porcentaje = float.Parse("0");
                }
                else
                {
                    ispor = "1";
                    importe = float.Parse("0");
                    porcentaje = float.Parse(txtPorcentaje.Text);
                }

                string valorValida = "G";

                if (UtileriasFechas.IsDate(txtInicio.Text) == false && operacion == "AGREGAR" && valorValida == "G")
                {
                    valorValida = "B";
                    MessageBox.Show("Fecha de Inicio no válida", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtInicio.Focus();
                }

                if (UtileriasFechas.IsDate(txtFin.Text) == false && operacion == "EDITAR" && valorValida == "G")
                {
                    valorValida = "B";
                    MessageBox.Show("Fecha de Fin no válida", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtFin.Focus();
                }

                if (UtileriasFechas.compararFecha(txtInicio.Text, ">", txtFin.Text))
                {
                    valorValida = "B";
                    MessageBox.Show("La Fecha de Fin no puede ser menor que la Fecha de Inicio", "Avios", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtInicio.Focus();
                }
                

                if (valorValida.Equals("G"))
                {
                    detsisparm.actualizarDetSisParm(operacion, iddetsisparm, idsisparm, fini, ffin, ispor, importe, porcentaje);
                    ui_lista(idsisparm);
                    ui_limpiar();
                 
                }
            }
            catch (FormatException ex)
            {
                MessageBox.Show(ex.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            

        }

        private void txtPorcentaje_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                try
                {
                    float numero = float.Parse(txtPorcentaje.Text);
                    if (numero <= 100)
                    {
                        e.Handled = true;
                        btnGrabar.Focus();
                    }
                    else
                    {
                        MessageBox.Show("El valor no puede ser mayor a 100", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtPorcentaje.Clear();
                        txtPorcentaje.Focus();
                    }
                }
                catch (FormatException ex)
                {
                    MessageBox.Show(ex.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtPorcentaje.Clear();
                    txtPorcentaje.Focus();
                }

            }
        }

        private void txtImporte_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                try
                {
                    float numero = float.Parse(txtImporte.Text);
                    e.Handled = true;
                    btnGrabar.Focus();
                }
                catch (FormatException ex)
                {
                    MessageBox.Show(ex.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtImporte.Clear();
                    txtImporte.Focus();
                }

            }
        }

        private void radioButtonImp_CheckedChanged(object sender, EventArgs e)
        {
            txtImporte.Clear();
            txtPorcentaje.Clear();
            txtImporte.Enabled = true;
            txtPorcentaje.Enabled = false;
            txtImporte.Focus();
        }

        private void radioButtonPor_CheckedChanged(object sender, EventArgs e)
        {
            txtImporte.Clear();
            txtPorcentaje.Clear();
            txtImporte.Enabled = false;
            txtPorcentaje.Enabled = true;
            txtPorcentaje.Focus();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            string idsisparm;
            string iddetsisparm;
    


            DetSisParm detsisparm = new DetSisParm();
            Int32 selectedCellCount = dgvDetalle.GetCellCount(DataGridViewElementStates.Selected);
            if (selectedCellCount > Convert.ToInt32(0))
            {

                DialogResult resultado = MessageBox.Show("¿Desea eliminar el valor de la Variable?",
                "Consulta Importante",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);
                if (resultado == DialogResult.Yes)
                {
                    idsisparm = dgvDetalle.Rows[dgvDetalle.SelectedCells[1].RowIndex].Cells[4].Value.ToString();
                    iddetsisparm = dgvDetalle.Rows[dgvDetalle.SelectedCells[1].RowIndex].Cells[5].Value.ToString();
                    detsisparm.eliminarDetSisParm(idsisparm, iddetsisparm);
                    ui_lista(idsisparm);
                }


            }
            else
            {
                MessageBox.Show("No ha seleccionado registro a Eliminar", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }
    }
}