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
    public partial class ui_ConceptosPlan : Form
    {
        string _anio;
        string _messem;
        string _idcia;
        string _idtipoper;
        string _idtipoplan;
        string _idtipocal;
        string _idperplan;
        string _idcolplan;
        string _nombre;
        string _descolplan;

        public ui_ConceptosPlan()
        {
            InitializeComponent();
        }

        private void ui_ConceptosPlan_Load(object sender, EventArgs e)
        {
            this.Text = "Conceptos de Planilla  Periodo Laboral: " + this._messem + "/" + this._anio;
            lblnombre.Text = this._nombre;
            lblcolumna.Text = this._idcolplan + " - " + this._descolplan;
            ui_ListaConBol();
        }

        public void ui_setVariables(string anio, string messem, string idcia, string idtipoper, string idtipoplan, string idtipocal, string idperplan, string nombre, string idcolplan, string descolplan)
        {
            this._anio = anio;
            this._messem = messem;
            this._idcia = idcia;
            this._idtipoper = idtipoper;
            this._idtipoplan = idtipoplan;
            this._idtipocal = idtipocal;
            this._idperplan = idperplan;
            this._nombre = nombre;
            this._idcolplan = idcolplan;
            this._descolplan = descolplan;
        }

        public void ui_ListaConBol()
        {

            DataTable dtconbol = new DataTable();
            Funciones funciones = new Funciones();
            string anio = this._anio;
            string messem = this._messem;
            string idcia = this._idcia;
            string idtipoper = this._idtipoper;
            string idtipoplan = this._idtipoplan;
            string idtipocal = this._idtipocal;
            string idperplan = this._idperplan;
            string idcolplan = this._idcolplan;

            string query = " select A.idconplan,B.desboleta,A.valor,B.idcolplan,B.pdt from conbol A ";
            query = query + " inner join detconplan B on A.idcia=B.idcia and ";
            query = query + " A.idtipoplan=B.idtipoplan and A.idtipocal=B.idtipocal and A.idtipoper=B.idtipoper ";
            query = query + " and A.idconplan=B.idconplan";
            query = query + " where A.anio='" + @anio + "' and A.messem='" + @messem + "' ";
            query = query + " and A.idcia='" + @idcia + "' and A.idtipoper='" + @idtipoper + "' ";
            query = query + " and A.idtipoplan='" + @idtipoplan + "' and A.idtipocal='" + @idtipocal + "' ";
            query = query + " and A.idperplan='" + @idperplan + "' and B.idcolplan='" + @idcolplan + "' order by A.idconplan asc;";


            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();
            SqlDataAdapter da = new SqlDataAdapter();

            da.SelectCommand = new SqlCommand(query, conexion);
            da.Fill(dtconbol);

            funciones.formatearDataGridView(dgvdetalle);
            dgvdetalle.DataSource = dtconbol;

            dgvdetalle.Columns[0].HeaderText = "Código";
            dgvdetalle.Columns[1].HeaderText = "Concepto de Planilla";
            dgvdetalle.Columns[2].HeaderText = "Valor";

            dgvdetalle.Columns["idcolplan"].Visible = false;
            dgvdetalle.Columns["pdt"].Visible = false;

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
    }
}