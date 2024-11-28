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
    public partial class ui_detallePlan : Form
    {
        string _anio;
        string _messem;
        string _idcia;
        string _idtipoper;
        string _idtipoplan;
        string _idtipocal;
        string _idperplan;
        string _nombre;

        public ui_detallePlan()
        {
            InitializeComponent();
        }

        public void ui_setVariables(string anio, string messem, string idcia, string idtipoper, string idtipoplan, string idtipocal, string idperplan, string nombre)
        {
            this._anio = anio;
            this._messem = messem;
            this._idcia = idcia;
            this._idtipoper = idtipoper;
            this._idtipoplan = idtipoplan;
            this._idtipocal = idtipocal;
            this._idperplan = idperplan;
            this._nombre = nombre;
        }

        private void ui_detallePlan_Load(object sender, EventArgs e)
        {
            this.Text = "Libro de Planilla  Periodo Laboral : " + this._messem + "/" + this._anio;
            lblnombre.Text = this._nombre;

            ui_ListaDetallePlan();
        }

        public void ui_ListaDetallePlan()
        {

            DataTable dtdetalleplan = new DataTable();
            Funciones funciones = new Funciones();
            string anio = this._anio;
            string messem = this._messem;
            string idcia = this._idcia;
            string idtipoper = this._idtipoper;
            string idtipoplan = this._idtipoplan;
            string idtipocal = this._idtipocal;
            string idperplan = this._idperplan;

            string query = " select B.idcolplan,C.descolplan,SUM(A.valor) as valor from conbol A ";
            query = query + " inner join detconplan B on A.idcia=B.idcia ";
            query = query + " and A.idtipoper=B.idtipoper and A.idtipocal=B.idtipocal ";
            query = query + " and A.idtipoplan=B.idtipoplan and A.idconplan=B.idconplan ";
            query = query + " inner join colplan C on B.idcolplan=C.idcolplan";
            query = query + " and B.idtipoplan=C.idtipoplan ";
            query = query + " where A.anio='" + @anio + "' and A.messem='" + @messem + "' ";
            query = query + " and A.idcia='" + @idcia + "' and A.idtipoper='" + @idtipoper + "' ";
            query = query + " and A.idtipoplan='" + @idtipoplan + "' and A.idtipocal='" + @idtipocal + "' ";
            query = query + " and A.idperplan='" + @idperplan + "' ";
            query = query + " group by B.idcolplan,C.descolplan ;";

            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();
            SqlDataAdapter da = new SqlDataAdapter();

            da.SelectCommand = new SqlCommand(query, conexion);
            da.Fill(dtdetalleplan);

            funciones.formatearDataGridView(dgvdetalle);
            dgvdetalle.DataSource = dtdetalleplan;

            dgvdetalle.Columns[0].HeaderText = "Código";
            dgvdetalle.Columns[1].HeaderText = "Columna de Planilla";
            dgvdetalle.Columns[2].HeaderText = "Valor";

            dgvdetalle.Columns[2].DefaultCellStyle.Format = "###,###.##";
            dgvdetalle.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.BottomRight;

            dgvdetalle.Columns[0].Width = 60;
            dgvdetalle.Columns[1].Width = 250;
            dgvdetalle.Columns[2].Width = 80;

            conexion.Close();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void btnConceptos_Click(object sender, EventArgs e)
        {
            string idtipoper = this._idtipoper;
            string messem = this._messem;
            string anio = this._anio;
            string idtipocal = this._idtipocal;
            string idtipoplan = this._idtipoplan;
            string idcia = this._idcia;
            string idperplan = this._idperplan;
            string nombre = this._nombre;

            Int32 selectedCellCount =
            dgvdetalle.GetCellCount(DataGridViewElementStates.Selected);
            if (selectedCellCount > Convert.ToInt32(0))
            {
                string idcolplan = dgvdetalle.Rows[dgvdetalle.SelectedCells[1].RowIndex].Cells[0].Value.ToString();
                string descolplan = dgvdetalle.Rows[dgvdetalle.SelectedCells[1].RowIndex].Cells[1].Value.ToString();
                ui_ConceptosPlan ui_conceptosplan = new ui_ConceptosPlan();
                ui_conceptosplan.ui_setVariables(anio, messem, idcia, idtipoper, idtipoplan, idtipocal, idperplan, nombre, idcolplan, descolplan);
                ui_conceptosplan.Activate();
                ui_conceptosplan.BringToFront();
                ui_conceptosplan.ShowDialog();
                ui_conceptosplan.Dispose();

            }
            else
            {
                MessageBox.Show("No ha seleccionado registro a visualizar", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }
    }
}