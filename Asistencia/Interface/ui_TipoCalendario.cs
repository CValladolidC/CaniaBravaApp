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
    public partial class ui_TipoCalendario : Form
    {
        string _idcia;
        string _idtipoplan;
        private Form FormPadre;

        public Form _FormPadre
        {
            get { return FormPadre; }
            set { FormPadre = value; }
        }

        public ui_TipoCalendario()
        {
            InitializeComponent();
        }

        private void ui_TipoCalendario_Load(object sender, EventArgs e)
        {

            ui_listaTipoCalendario(this._idcia, this._idtipoplan);

        }

        public void ui_listaTipoCal(string idcia, string idtipoplan)
        {

            Funciones funciones = new Funciones();
            string query;
            query = " SELECT idtipocal as clave,destipocal as descripcion FROM tipocal ";
            query = query + " where idtipocal not in (select idtipocal from calcia ";
            query = query + " where idcia='" + @idcia + "' and idtipoplan='" + @idtipoplan + "');";
            funciones.listaComboBox(query, cmbTipoCal, "");


        }

        public void ui_listaTipoCalendario(string idcia, string idtipoplan)
        {
            Funciones funciones = new Funciones();
            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();
            string query = " select A.idtipocal,B.destipocal,A.idcia,A.idtipoplan from calcia A ";
            query = query + " inner join tipocal B on A.idtipocal=B.idtipocal ";
            query = query + " where A.idcia='" + @idcia + "' and A.idtipoplan='" + @idtipoplan + "' ;";

            try
            {
                using (SqlDataAdapter myDataAdapter = new SqlDataAdapter(query, conexion))
                {
                    DataSet myDataSet = new DataSet();
                    myDataAdapter.Fill(myDataSet, "tblCal");
                    funciones.formatearDataGridView(dgvCal);
                    dgvCal.DataSource = myDataSet.Tables["tblCal"];
                    dgvCal.Columns[0].HeaderText = "Tipo";
                    dgvCal.Columns[1].HeaderText = "Descripción";
                    dgvCal.Columns["idcia"].Visible = false;
                    dgvCal.Columns["idtipoplan"].Visible = false;
                    dgvCal.Columns[0].Width = 50;
                    dgvCal.Columns[1].Width = 150;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            conexion.Close();
        }

        public void ui_loadTipoCal(string idcia, string idtipoplan, string destipoplan)
        {
            this._idcia = idcia;
            this._idtipoplan = idtipoplan;
            txtCodTipoPlan.Text = idtipoplan;
            txtDesTipoPlan.Text = destipoplan;
            ui_listaTipoCalendario(idcia, idtipoplan);
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnEliminarEstAne_Click(object sender, EventArgs e)
        {
            string idcia;
            string idtipoplan;
            string idtipocal;

            Int32 selectedCellCount = dgvCal.GetCellCount(DataGridViewElementStates.Selected);
            if (selectedCellCount > 0)
            {
                idtipocal = dgvCal.Rows[dgvCal.SelectedCells[1].RowIndex].Cells[0].Value.ToString();
                idcia = dgvCal.Rows[dgvCal.SelectedCells[1].RowIndex].Cells[2].Value.ToString();
                idtipoplan = dgvCal.Rows[dgvCal.SelectedCells[1].RowIndex].Cells[3].Value.ToString();
                DialogResult resultado = MessageBox.Show("¿Desea eliminar el Tipo de Calendario " + idtipocal + " ?",
                "Consulta Importante",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);
                if (resultado == DialogResult.Yes)
                {
                    CalCia calcia = new CalCia();
                    calcia.eliminarCalCia(idcia, idtipoplan, idtipocal);
                    ui_listaTipoCalendario(idcia, idtipoplan);
                    ui_listaTipoCal(idcia, idtipoplan);
                }

            }
            else
            {
                MessageBox.Show("No ha seleccionado registro a Eliminar", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void btnGrabarEstAne_Click(object sender, EventArgs e)
        {
            try
            {

                CalCia calcia = new CalCia();
                Funciones funciones = new Funciones();
                string idcia = this._idcia;
                string idtipoplan = this._idtipoplan;
                string idtipocal = funciones.getValorComboBox(cmbTipoCal, 1);
                calcia.actualizarCalCia(idcia, idtipoplan, idtipocal);
                ui_listaTipoCalendario(idcia, idtipoplan);
                ui_listaTipoCal(idcia, idtipoplan);

            }
            catch (FormatException ex)
            {
                MessageBox.Show(ex.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}