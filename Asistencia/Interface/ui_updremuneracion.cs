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
    public partial class ui_updremuneracion : Form
    {
        string _idcia;
        string _idperplan;
        string _ope;

        private Form FormPadre;

        public Form _FormPadre
        {
            get { return FormPadre; }
            set { FormPadre = value; }
        }

        public ui_updremuneracion()
        {
            InitializeComponent();
        }

        private void ui_updremuneracion_Load(object sender, EventArgs e)
        {

        }

        private void ui_limpiar()
        {

            this._ope = string.Empty;
            txtInicio.Text = string.Empty;
            txtFin.Text = string.Empty;
            txtImporte.Text = string.Empty;
            txtInicio.Enabled = false;
            txtFin.Enabled = false;
            txtImporte.Enabled = false;
        }

        public void ui_load(string idcia, string idperplan)
        {
            PerPlan perplan = new PerPlan();
            txtTrabajador.Text = perplan.ui_getDatosPerPlan(idcia, idperplan, "1") + " " + perplan.ui_getDatosPerPlan(idcia, idperplan, "2") + " " + perplan.ui_getDatosPerPlan(idcia, idperplan, "3");
            txtPeriodicidad.Text = perplan.ui_getDatosPerPlan(idcia, idperplan, "PERIODICIDAD_INGRESOS");
            this._idperplan = idperplan;
            this._idcia = idcia;
            this._ope = string.Empty;
            this.ui_lista(idcia, idperplan);
        }

        internal void ui_lista(string idcia, string idperplan)
        {

            Funciones funciones = new Funciones();
            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();
            string query = "select fini,ffin,importe,idremu,idperplan,idcia,state from remu where idcia='" + @idcia + "' and idperplan='" + @idperplan + "' order by idremu desc;";
            try
            {
                using (SqlDataAdapter myDataAdapter = new SqlDataAdapter(query, conexion))
                {
                    DataSet myDataSet = new DataSet();
                    myDataAdapter.Fill(myDataSet, "tblRemu");
                    funciones.formatearDataGridView(dgvDetalle);

                    dgvDetalle.DataSource = myDataSet.Tables["tblRemu"];
                    dgvDetalle.Columns[0].HeaderText = "Fecha Inicio";
                    dgvDetalle.Columns[1].HeaderText = "Fecha Fin";
                    dgvDetalle.Columns[2].HeaderText = "Importe";

                    dgvDetalle.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    dgvDetalle.Columns[2].DefaultCellStyle.Format = "###,###.##";

                    dgvDetalle.Columns["idremu"].Visible = false;
                    dgvDetalle.Columns["idcia"].Visible = false;
                    dgvDetalle.Columns["state"].Visible = false;
                    dgvDetalle.Columns["idperplan"].Visible = false;

                    dgvDetalle.Columns[0].Width = 100;
                    dgvDetalle.Columns[1].Width = 100;
                    dgvDetalle.Columns[2].Width = 100;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }

            conexion.Close();
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            string query;
            string idcia = this._idcia;
            string idperplan = this._idperplan;

            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();

            query = "SELECT * from remu where state='V' and idcia='" + @idcia + "' and idperplan='" + @idperplan + "';";
            try
            {

                SqlCommand myCommand_table = new SqlCommand(query, conexion);
                DataTable dt_table = new DataTable();
                dt_table.Load(myCommand_table.ExecuteReader());
                myCommand_table.Dispose();

                if (dt_table.Rows.Count > 0)
                {
                    MessageBox.Show("Para ingresar nueva Remuneración debe finalizar el anterior", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    ui_limpiar();
                    this._ope = "AGREGAR";
                    txtInicio.Text = DateTime.Now.ToString("dd/MM/yyyy");
                    txtInicio.Enabled = true;
                    txtFin.Enabled = false;
                    txtImporte.Enabled = true;
                    txtInicio.Focus();
                }

            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);

            }

            conexion.Close();
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            string idremu;
            string fini;
            string ffin;
            string importe;

            Int32 selectedCellCount = dgvDetalle.GetCellCount(DataGridViewElementStates.Selected);
            if (selectedCellCount > 0)
            {
                fini = dgvDetalle.Rows[dgvDetalle.SelectedCells[1].RowIndex].Cells[0].Value.ToString();
                ffin = dgvDetalle.Rows[dgvDetalle.SelectedCells[1].RowIndex].Cells[1].Value.ToString();
                importe = dgvDetalle.Rows[dgvDetalle.SelectedCells[1].RowIndex].Cells[2].Value.ToString();
                idremu = dgvDetalle.Rows[dgvDetalle.SelectedCells[1].RowIndex].Cells[3].Value.ToString();

                this._ope = "EDITAR";
                txtCodigo.Text = idremu;
                txtInicio.Text = fini;
                txtFin.Text = ffin;

                txtInicio.Enabled = false;
                txtImporte.Enabled = false;
                txtFin.Enabled = true;
                txtImporte.Text = importe;
                txtFin.Focus();
            }
            else
            {
                MessageBox.Show("No ha seleccionado registro a Editar", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void btnGrabar_Click(object sender, EventArgs e)
        {
            try
            {
                Funciones funciones = new Funciones();
                Remu remu = new Remu();
                string operacion = this._ope;
                string idcia = this._idcia;
                string idperplan = this._idperplan;
                string idremu = txtCodigo.Text;
                string fini = txtInicio.Text;
                string ffin = txtFin.Text;
                float importe = 0;
                if (txtImporte.Text.Trim() != string.Empty)
                {
                    importe = float.Parse(txtImporte.Text);
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

                DateTime fecini = DateTime.Parse(fini);
                DateTime? fecfin = null;
                if (ffin != "  /  /") { fecfin = DateTime.Parse(ffin); }

                if (valorValida.Equals("G"))
                {
                    remu.actualizarRemu(operacion, idcia, idperplan, idremu, fecini.ToString("yyyy-MM-dd"), (fecfin != null ? fecfin.Value.ToString("yyyy-MM-dd") : string.Empty), importe);
                    ui_lista(idcia, idperplan);
                    ui_limpiar();
                }
            }
            catch (FormatException ex)
            {
                MessageBox.Show(ex.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            string idcia;
            string idperplan;
            string idremu;


            Remu remu = new Remu();
            Int32 selectedCellCount = dgvDetalle.GetCellCount(DataGridViewElementStates.Selected);
            if (selectedCellCount > Convert.ToInt32(0))
            {

                DialogResult resultado = MessageBox.Show("¿Desea eliminar la Remuneración?",
                "Consulta Importante",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);
                if (resultado == DialogResult.Yes)
                {
                    idremu = dgvDetalle.Rows[dgvDetalle.SelectedCells[1].RowIndex].Cells[3].Value.ToString();
                    idperplan = dgvDetalle.Rows[dgvDetalle.SelectedCells[1].RowIndex].Cells[4].Value.ToString();
                    idcia = dgvDetalle.Rows[dgvDetalle.SelectedCells[1].RowIndex].Cells[5].Value.ToString();
                    remu.eliminarRemu(idcia, idperplan, idremu);
                    ui_lista(idcia, idperplan);
                }


            }
            else
            {
                MessageBox.Show("No ha seleccionado registro a Eliminar", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void txtInicio_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                if (UtileriasFechas.IsDate(txtInicio.Text))
                {
                    e.Handled = true;
                    txtImporte.Focus();

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

        private void btnSalir_Click(object sender, EventArgs e)
        {
            ((ui_remuneraciones)FormPadre).btnActualizar.PerformClick();
            this.Close();

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
    }
}