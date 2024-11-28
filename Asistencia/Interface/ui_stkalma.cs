using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Configuration;
using System.Data.SqlClient;

namespace CaniaBrava
{
    public partial class ui_stkalma : ui_form
    {
        Funciones funciones = new Funciones();
        string _codcia;

        public ui_stkalma()
        {
            InitializeComponent();

            if (funciones.VersionAssembly()) Application.ExitThread();
        }

        private void ui_stkalma_Load(object sender, EventArgs e)
        {
            GlobalVariables gv = new GlobalVariables();
            MaesGen maesgen = new MaesGen();
            this._codcia = gv.getValorCia();
            string codcia = this._codcia;
            string query = "Select codalma as clave,desalma as descripcion ";
            query = query + "from alalma where codcia='" + @codcia + "' and estado='V' order by 1 asc;";
            Funciones funciones = new Funciones();
            funciones.listaToolStripComboBox(query, cmbAlmacen);
            maesgen.listaDetMaesGenToolStrip("110", cmbFamilia, "X");
            cmbListar.Text = "ARTICULOS CON STOCK";
            cmbFamilia.Text = "X   TODOS";
            ui_ListaStock();
        }

        private void ui_ListaStock()
        {
            if (cmbAlmacen.Text == string.Empty)
            {
                MessageBox.Show("No ha definido Almacén", "Aviso Importante", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                Funciones funciones = new Funciones();
                string codcia = this._codcia;
                string alma = funciones.getValorToolStripComboBox(cmbAlmacen, 2);
                string condicion = cmbListar.Text.Trim();
                string cadlistar = string.Empty;
                string famarti = funciones.getValorToolStripComboBox(cmbFamilia, 4);
                string cadfam = string.Empty;
                SqlConnection conexion = new SqlConnection();
                conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
                conexion.Open();

                if (condicion.Equals("ARTICULOS CON STOCK"))
                {
                    cadlistar = " and A.stock>0 ";
                }

                if (famarti != "X")
                {
                    cadfam = " and B.famarti='" + @famarti + "' ";
                }
                string query = " select A.codarti,B.desarti,B.unidad,A.stock ";
                query = query + "from alstock A left join alarti B on ";
                query = query + "/*A.codcia=B.codcia and */A.codarti=B.codarti ";
                query = query + "where A.codcia='" + @codcia + "' and A.alma='" + @alma + "' " + cadlistar + cadfam;
                query = query + " order by A.codarti asc";

                try
                {
                    using (SqlDataAdapter myDataAdapter = new SqlDataAdapter(query, conexion))
                    {
                        DataSet myDataSet = new DataSet();
                        myDataAdapter.Fill(myDataSet, "tblStock");
                        funciones.formatearDataGridView(dgvdetalle);
                        dgvdetalle.DataSource = myDataSet.Tables["tblStock"];
                        dgvdetalle.Columns[0].HeaderText = "Código";
                        dgvdetalle.Columns[1].HeaderText = "Descripción";
                        dgvdetalle.Columns[2].HeaderText = "Unidad";
                        dgvdetalle.Columns[3].HeaderText = "Stock Disponible";
                        dgvdetalle.Columns[0].Width = 100;
                        dgvdetalle.Columns[1].Width = 350;
                        dgvdetalle.Columns[2].Width = 100;
                        dgvdetalle.Columns[3].Width = 100;

                        dgvdetalle.AllowUserToResizeRows = false;
                        dgvdetalle.AllowUserToResizeColumns = false;
                        foreach (DataGridViewColumn column in dgvdetalle.Columns)
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
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cmbAlmacen_SelectedIndexChanged(object sender, EventArgs e)
        {
            ui_ListaStock();
        }

        private void cmbListar_SelectedIndexChanged(object sender, EventArgs e)
        {
            ui_ListaStock();
        }

        private void dgvdetalle_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            if (dgvdetalle.Rows.Count > 0)
            {
                Exporta exporta = new Exporta();
                exporta.Excel_FromDataGridView(dgvdetalle);
            }
            else { MessageBox.Show("No existe informacion a exportar", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information); }
        }

        private void btnPDF_Click(object sender, EventArgs e)
        {
            if (dgvdetalle.Rows.Count > 0)
            {
                Exporta exporta = new Exporta();
                exporta.Pdf_FromDataGridView(dgvdetalle, 2);
            }
            else { MessageBox.Show("No existe informacion a exportar", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information); }
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            ui_ListaStock();
        }

        private void cmbFamilia_SelectedIndexChanged(object sender, EventArgs e)
        {
            ui_ListaStock();
        }

        private void cmbFamilia_Click(object sender, EventArgs e)
        {

        }

        private void cmbAlmacen_Click(object sender, EventArgs e)
        {

        }

    }
}