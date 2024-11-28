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
using System.Collections;
using System.Diagnostics;
using System.IO;

namespace CaniaBrava
{
    public partial class ui_rep_ausentes : Form
    {
        GlobalVariables variables = new GlobalVariables();
        Funciones funciones = new Funciones();
        SqlConnection conexion = new SqlConnection();

        public ui_rep_ausentes()
        {
            InitializeComponent();

            if (funciones.VersionAssembly()) Application.ExitThread();
        }

        private void ui_trabajadores_Load(object sender, EventArgs e)
        {
            string query = "SELECT idtipoper as clave,destipoper as descripcion FROM tipoper order by 1 asc;";
            MaesGen maesgen = new MaesGen();
            funciones.listaComboBox(query, cmbCategoria, "X");
            maesgen.listaDetMaesGen("008", cmbSeccion, "X");
            cmbSeccion.Text = "X   TODOS";
            cmbCategoria.Text = "X   TODOS";
            DateTime hoy = DateTime.Today;
            fechaini.Value = hoy;
            fechaini.MaxDate = hoy;
            ui_ListaPerPlan();
        }

        private void ui_ListaPerPlan()
        {
            string buscar = txtBuscar.Text.Trim();
            string idtipoper = funciones.getValorComboBox(cmbCategoria, 2);
            string seccion = funciones.getValorComboBox(cmbSeccion, 8);
            string cadenaidtipoper = string.Empty;
            string cadenaseccion = string.Empty;
            string cadenaBusqueda = string.Empty;

            if (idtipoper != "X") cadenaidtipoper = " AND A.idtipoper='" + @idtipoper + "' ";
            if (seccion != "X   TODO") cadenaseccion = " AND A.seccion='" + @seccion + "' ";

            if (buscar != string.Empty)
            {
                if (radioButtonCodigoInterno.Checked && txtBuscar.Text.Trim() != string.Empty)
                {
                    cadenaBusqueda = " AND A.idperplan LIKE '%" + @buscar + "%' ";
                }
                else
                {
                    if (radioButtonNombre.Checked && txtBuscar.Text.Trim() != string.Empty)
                    {
                        cadenaBusqueda = " AND RTRIM(A.apepat)+' '+RTRIM(A.apemat)+', '+RTRIM(A.nombres) LIKE '%" + @buscar + "%' ";
                    }
                    else
                    {
                        cadenaBusqueda = " AND A.nrodoc LIKE '%" + @buscar + "%' ";
                    }
                }
            }

            string query = @"SELECT '"+ fechaini.Value.ToShortDateString() + @"' AS Fecha,A.idperplan as [Cod.Int.],(RTRIM(A.apepat)+' '+RTRIM(A.apemat)+', '+RTRIM(A.nombres)) as [Apellidos y Nombres],
H.abrevia as [Gerencia],D.desmaesgen as [Área],E.deslabper as [Ocupación],RTRIM(B.destipoper) as [Nomina],C.Parm1maesgen as [T. Doc],A.nrodoc as [Nro. Doc.] 
FROM perplan A (NOLOCK) 
LEFT JOIN tipoper (NOLOCK) B on A.idtipoper=B.idtipoper 
LEFT JOIN maesgen (NOLOCK) C on C.idmaesgen='002' and A.tipdoc=C.clavemaesgen 
LEFT JOIN maesgen (NOLOCK) D on D.idmaesgen='008' and A.seccion=D.clavemaesgen 
LEFT JOIN labper (NOLOCK) E on A.idcia=E.idcia and A.idlabper=E.idlabper and A.idtipoper=E.idtipoper 
LEFT JOIN view_perlab F on A.idcia=F.idcia collate Modern_Spanish_CI_AI and A.idperplan=F.idperplan collate Modern_Spanish_CI_AI 
LEFT JOIN maesgen (NOLOCK) H on H.idmaesgen='040' and H.clavemaesgen=A.codaux and H.parm1maesgen=A.idcia 
WHERE A.alta_tregistro=1 AND A.idperplan NOT IN (SELECT idperplan FROM control (NOLOCK) WHERE fecha = '" + fechaini.Value.ToString("yyyy-MM-dd") + "' ) " + @cadenaidtipoper + @cadenaseccion + @cadenaBusqueda + @" 
ORDER BY RTRIM(A.apepat)+' '+RTRIM(A.apemat)+', '+RTRIM(A.nombres); ";

            if (query != String.Empty)
            {
                loadqueryDatos(query);
            }
        }

        private void loadqueryDatos(string query)
        {
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

                    dgvdetalle.Columns[0].Width = 70;
                    dgvdetalle.Columns[1].Width = 70;
                    dgvdetalle.Columns[2].Width = 250;
                    dgvdetalle.Columns[3].Width = 160;
                    dgvdetalle.Columns[4].Width = 160;
                    dgvdetalle.Columns[5].Width = 120;
                    dgvdetalle.Columns[6].Width = 120;
                    dgvdetalle.Columns[7].Width = 70;
                    dgvdetalle.Columns[8].Width = 70;

                    dgvdetalle.AllowUserToResizeRows = false;
                    dgvdetalle.AllowUserToResizeColumns = false;
                    foreach (DataGridViewColumn column in dgvdetalle.Columns)
                    {
                        column.SortMode = DataGridViewColumnSortMode.NotSortable;
                    }
                }

                ui_calculaNumRegistros();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }

            conexion.Close();
        }

        public void ui_calculaNumRegistros()
        {
            string numregencontrados = Convert.ToString(dgvdetalle.RowCount);
            txtRegEncontrados.Text = funciones.replicateCadena(" ", 15 - numregencontrados.Trim().Length) + numregencontrados.Trim() + funciones.replicateCadena(" ", 20) + " De acuerdo al criterio de búsqueda";
        }

        private void btnSalir_Click(object sender, EventArgs e) { Close(); }

        private void btnActualizar_Click(object sender, EventArgs e) { ui_ListaPerPlan(); }

        private void cmbCategoria_Click(object sender, EventArgs e) { }

        private void cmbSituacion_Click(object sender, EventArgs e) { }

        private void cmbSeccion_Click(object sender, EventArgs e) { }

        private void cmbCategoria_SelectedIndexChanged(object sender, EventArgs e) { ui_ListaPerPlan(); }

        private void cmbSeccion_SelectedIndexChanged(object sender, EventArgs e) { ui_ListaPerPlan(); }

        private void radioButtonCodigoInterno_CheckedChanged(object sender, EventArgs e)
        {
            txtBuscar.Text = string.Empty;
            txtBuscar.Focus();
        }

        private void radioButtonNroDoc_CheckedChanged(object sender, EventArgs e)
        {
            txtBuscar.Text = string.Empty;
            txtBuscar.Focus();
        }

        private void radioButtonNombre_CheckedChanged(object sender, EventArgs e)
        {
            txtBuscar.Text = string.Empty;
            txtBuscar.Focus();
        }

        private void btnTodos_Click(object sender, EventArgs e)
        {
            cmbSeccion.Text = "X   TODOS";
            cmbCategoria.Text = "X   TODOS";
            txtBuscar.Clear();
            txtBuscar.Focus();
            ui_ListaPerPlan();
        }

        private void txtBuscar_TextChanged(object sender, EventArgs e) { ui_ListaPerPlan(); }

        private void btnPdf_Click(object sender, EventArgs e)
        {
            if (dgvdetalle.RowCount > 0)
            {
                Exporta exporta = new Exporta();
                exporta.Pdf_FromDataGridView(dgvdetalle, 1);
            }
            else
            {
                MessageBox.Show("No se puede exportar a PDF", "Aviso Importante", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            Exporta exporta = new Exporta();
            foreach (DataGridViewRow row in dgvdetalle.Rows)
            {
                foreach (DataGridViewCell cell in row.Cells)
                {
                    if (cell.ColumnIndex == 0 || cell.ColumnIndex == 7)
                    {
                        cell.Value = "'" + cell.Value.ToString();
                    }
                }
            }
            exporta.Excel_FromDataGridView(dgvdetalle);
            ui_ListaPerPlan();
        }

        private void fechaini_ValueChanged(object sender, EventArgs e)
        {
            ui_ListaPerPlan();
        }
    }
}