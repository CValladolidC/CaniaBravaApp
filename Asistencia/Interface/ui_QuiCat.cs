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
    public partial class ui_QuiCat : Form
    {
        GlobalVariables globalVariables = new GlobalVariables();
        Funciones funciones = new Funciones();

        public ui_QuiCat()
        {
            InitializeComponent();
        }

        private void ui_QuiCat_Load(object sender, EventArgs e)
        {
            string squery;
            squery = "SELECT idtipoper as clave,destipoper as descripcion FROM tipoper order by 1 asc;";
            Funciones funciones = new Funciones();
            MaesGen maesgen = new MaesGen();
            funciones.listaToolStripComboBox(squery, cmbCategoria);
            txtAnio.Text = Convert.ToString(DateTime.Now.Year);
            ui_ListaPerPlan();
        }

        private void ui_ListaPerPlan()
        {

            if (cmbCategoria.Text == string.Empty)
            {
                MessageBox.Show("No ha definido Tipo de Personal", "Aviso Importante", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }
            else
            {
                string idcia = globalVariables.getValorCia();
                string idtipoper = cmbCategoria.Text.Substring(0, 1);
                string anio = txtAnio.Text;

                string query = " Select A.idperplan,B.cortotipoper,C.Parm1maesgen as cortotipodoc,A.nrodoc,";
                query = query + " CONCAT(CONCAT(CONCAT(A.apepat,' '),CONCAT(A.apemat,' , ')),A.nombres) as nombre, ";
                query = query + " SUM(G.impcal) as impcal,SUM(G.impext) as impext,SUM(G.remafecta) as remafecta, ";
                query = query + " A.idcia from perplan A left join tipoper B on A.idtipoper=B.idtipoper ";
                query = query + " left join quicat G on A.idcia=G.idcia and A.idperplan=G.idperplan ";
                query = query + " left join maesgen C on C.idmaesgen='002' and A.tipdoc=C.clavemaesgen ";
                query = query + " where A.idcia='" + @idcia + "' and G.Anio='" + @anio + "' and ";
                query = query + " A.idtipoper='" + @idtipoper + "' ";
                query = query + " group by A.idperplan,B.cortotipoper,C.Parm1maesgen,A.apepat,";
                query = query + " A.apemat,A.nombres,A.idcia ";
                query = query + " order by A.apepat,A.apemat,A.nombres asc";


                SqlConnection conexion = new SqlConnection();
                conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
                conexion.Open();

                try
                {
                    using (SqlDataAdapter myDataAdapter = new SqlDataAdapter(query, conexion))
                    {
                        DataSet myDataSet = new DataSet();
                        myDataAdapter.Fill(myDataSet, "tblPerPlan");
                        funciones.formatearDataGridView(dgvdetalle);

                        dgvdetalle.DataSource = myDataSet.Tables["tblPerPlan"];
                        dgvdetalle.Columns[0].HeaderText = "Cód.Int.";
                        dgvdetalle.Columns[1].HeaderText = "Cat.";
                        dgvdetalle.Columns[2].HeaderText = "Doc.Ident.";
                        dgvdetalle.Columns[3].HeaderText = "Nro.Doc.";
                        dgvdetalle.Columns[4].HeaderText = "Apellidos y Nombres";
                        dgvdetalle.Columns[5].HeaderText = "Cálculo Renta 5ta. Cat.";
                        dgvdetalle.Columns[6].HeaderText = "Otros descuentos 5ta. Cat.";
                        dgvdetalle.Columns[7].HeaderText = "Remuneración Afecta";


                        dgvdetalle.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                        dgvdetalle.Columns[6].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                        dgvdetalle.Columns[7].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

                        dgvdetalle.Columns[5].DefaultCellStyle.Format = "###,###.##";
                        dgvdetalle.Columns[6].DefaultCellStyle.Format = "###,###.##";
                        dgvdetalle.Columns[7].DefaultCellStyle.Format = "###,###.##";

                        dgvdetalle.Columns["idcia"].Visible = false;


                        dgvdetalle.Columns[0].Width = 50;
                        dgvdetalle.Columns[1].Width = 40;
                        dgvdetalle.Columns[2].Width = 60;
                        dgvdetalle.Columns[3].Width = 70;
                        dgvdetalle.Columns[4].Width = 220;
                        dgvdetalle.Columns[5].Width = 75;
                        dgvdetalle.Columns[6].Width = 75;
                        dgvdetalle.Columns[7].Width = 75;


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

        private void cmbListar_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.ui_ListaPerPlan();
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (cmbCategoria.Text == string.Empty)
            {
                MessageBox.Show("No ha definido Tipo de Personal", "Aviso Importante", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }
            else
            {
                string idcia = globalVariables.getValorCia();
                string idtipoper = cmbCategoria.Text.Substring(0, 1);
                string anio = txtAnio.Text;
                string idperplan;

                Int32 selectedCellCount = dgvdetalle.GetCellCount(DataGridViewElementStates.Selected);
                if (selectedCellCount > Convert.ToInt32(0))
                {
                    idperplan = dgvdetalle.Rows[dgvdetalle.SelectedCells[1].RowIndex].Cells[0].Value.ToString();

                    ui_UpdQuiCat ui_detalle = new ui_UpdQuiCat();
                    ui_detalle._FormPadre = this;
                    ui_detalle.ui_setValores(idtipoper, anio, idcia);
                    ui_detalle.ui_iniPerPlan("EDITAR", idperplan);
                    ui_detalle.Activate();
                    ui_detalle.BringToFront();
                    ui_detalle.ShowDialog();
                    ui_detalle.Dispose();

                }
                else
                {
                    MessageBox.Show("No ha seleccionado registro a Editar", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
            }
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            if (cmbCategoria.Text == string.Empty)
            {
                MessageBox.Show("No ha definido Tipo de Personal", "Aviso Importante", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                string idcia = globalVariables.getValorCia();
                string idtipoper = cmbCategoria.Text.Substring(0, 1);
                string anio = txtAnio.Text;

                if (anio != string.Empty)
                {
                    ui_UpdQuiCat ui_detalle = new ui_UpdQuiCat();
                    ui_detalle._FormPadre = this;
                    ui_detalle.ui_setValores(idtipoper, anio, idcia);
                    ui_detalle.ui_iniPerPlan("AGREGAR", "");
                    ui_detalle.Activate();
                    ui_detalle.BringToFront();
                    ui_detalle.ShowDialog();
                    ui_detalle.Dispose();
                }
                else
                {
                    MessageBox.Show("No ha especificado Año", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    txtAnio.Focus();
                }
            }
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            this.ui_ListaPerPlan();
        }

        private void cmbTipoPer_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.ui_ListaPerPlan();
        }

        private void txtAnio_TextChanged(object sender, EventArgs e)
        {
            this.ui_ListaPerPlan();
        }

        private void txtAnio_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                this.ui_ListaPerPlan();
            }
        }

        private void dgvdetalle_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void cmbCertificadoquicat_Click(object sender, EventArgs e)
        {
            if (cmbCategoria.Text == string.Empty)
            {
                MessageBox.Show("No ha definido Tipo de Personal", "Aviso Importante", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }
            else
            {
                string idcia = globalVariables.getValorCia();
                string idtipoper = cmbCategoria.Text.Substring(0, 1);
                string anio = txtAnio.Text;
                string idperplan;

                Int32 selectedCellCount = dgvdetalle.GetCellCount(DataGridViewElementStates.Selected);
                if (selectedCellCount > Convert.ToInt32(0))
                {
                    idperplan = dgvdetalle.Rows[dgvdetalle.SelectedCells[1].RowIndex].Cells[0].Value.ToString();

                    SqlConnection conexion = new SqlConnection();
                    conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
                    conexion.Open();
                    SqlDataAdapter da = new SqlDataAdapter();
                    DataSet ds = new DataSet();

                    string query = "CALL sp_certificadoquicat('" + idperplan + "','" + idcia + "','" + anio + "')";
                    da.SelectCommand = new SqlCommand(query, conexion);
                    da.Fill(ds, "quicat");

                    cr.crcertquicat cr = new cr.crcertquicat();
                    ui_reporte ui_reporte = new ui_reporte();
                    ui_reporte.asignaDataSet(cr, ds);
                    ui_reporte.Activate();
                    ui_reporte.BringToFront();
                    ui_reporte.ShowDialog();
                    ui_reporte.Dispose();
                }
                else
                {
                    MessageBox.Show("No ha seleccionado registro a Editar", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
            }
        }
    }
}