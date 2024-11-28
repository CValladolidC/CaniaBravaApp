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
    public partial class ui_EstadoCalPlan : Form
    {
        public ui_EstadoCalPlan()
        {
            InitializeComponent();
        }

        private void ui_EstadoCalPlan_Load(object sender, EventArgs e)
        {
            string squery;
            squery = "SELECT idtipoper as clave,destipoper as descripcion FROM tipoper order by 1 asc;";
            Funciones funciones = new Funciones();
            funciones.listaToolStripComboBox(squery, cmbTipoPer);
            squery = "SELECT idtipocal as clave,destipocal as descripcion FROM tipocal order by ordentipocal asc;";
            funciones.listaToolStripComboBox(squery, cmbTipoCal);
            cmbEstado.Text = "X  TODOS";
            ui_ListaCalPlan();
        }

        private void ui_ListaCalPlan()
        {
            try
            {
                Funciones funciones = new Funciones();
                GlobalVariables variables = new GlobalVariables();
                string idcia = variables.getValorCia();
                string idtipoper = cmbTipoPer.Text.Substring(0, 1);
                string idtipocal = cmbTipoCal.Text.Substring(0, 1);
                string estado = cmbEstado.Text.Substring(0, 1);

                string cadenaEstado = string.Empty;

                if (estado != "X")
                {
                    cadenaEstado = " and A.estado='" + @estado + "'";
                }

                SqlConnection conexion = new SqlConnection();
                conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
                conexion.Open();
                string query = " select A.messem,C.desmaesgen,A.anio,A.idtipocal,A.fechaini,A.fechafin, ";
                query = query + " A.idcia,A.idtipoper,CONCAT(CONCAT(A.idtipocal,'  '),B.destipocal) as tipocal,A.mes,A.mespdt,A.aniopdt,CASE A.estado WHEN 'C' THEN 'CERRADO' WHEN 'V' THEN 'VIGENTE' END ";
                query = query + " from calplan A left join tipocal B on A.idtipocal=B.idtipocal left join maesgen C on ";
                query = query + " A.mes=C.clavemaesgen and C.idmaesgen='035' ";
                query = query + " where A.idcia='" + @idcia + "' and A.idtipoper='" + @idtipoper + "' ";
                query = query + " and A.idtipocal='" + @idtipocal + "' " + @cadenaEstado + "  order by A.anio desc,A.mes desc,A.messem desc;";

                try
                {
                    using (SqlDataAdapter myDataAdapter = new SqlDataAdapter(query, conexion))
                    {
                        DataSet myDataSet = new DataSet();
                        myDataAdapter.Fill(myDataSet, "tblCalPlan");
                        funciones.formatearDataGridView(dgvdetalle);

                        dgvdetalle.DataSource = myDataSet.Tables["tblCalPlan"];
                        dgvdetalle.Columns[0].HeaderText = "Sem./Mes";
                        dgvdetalle.Columns[1].HeaderText = "Mes";
                        dgvdetalle.Columns[2].HeaderText = "Año";
                        dgvdetalle.Columns[3].HeaderText = "Tipo";
                        dgvdetalle.Columns[4].HeaderText = "Fecha Inicio";
                        dgvdetalle.Columns[5].HeaderText = "Fecha Fin";
                        dgvdetalle.Columns[10].HeaderText = "Mes PDT";
                        dgvdetalle.Columns[11].HeaderText = "Año PDT";
                        dgvdetalle.Columns[12].HeaderText = "Estado";

                        dgvdetalle.Columns["idtipoper"].Visible = false;
                        dgvdetalle.Columns["idcia"].Visible = false;
                        dgvdetalle.Columns["tipocal"].Visible = false;
                        dgvdetalle.Columns["mes"].Visible = false;

                        dgvdetalle.Columns[0].Width = 80;
                        dgvdetalle.Columns[1].Width = 80;
                        dgvdetalle.Columns[2].Width = 50;
                        dgvdetalle.Columns[3].Width = 50;
                        dgvdetalle.Columns[4].Width = 100;
                        dgvdetalle.Columns[5].Width = 100;
                        dgvdetalle.Columns[10].Width = 50;
                        dgvdetalle.Columns[11].Width = 50;
                        dgvdetalle.Columns[12].Width = 100;

                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
                conexion.Close();
            }
            catch (Exception) { }


        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cmbTipoPer_SelectedIndexChanged(object sender, EventArgs e)
        {
            ui_ListaCalPlan();
        }

        private void cmbTipoCal_SelectedIndexChanged(object sender, EventArgs e)
        {
            ui_ListaCalPlan();
        }

        private void cmbEstado_SelectedIndexChanged(object sender, EventArgs e)
        {
            ui_ListaCalPlan();
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            string idtipoper;
            string idcia;
            string anio;
            string messem;
            string idtipocal;

            Int32 selectedCellCount =
            dgvdetalle.GetCellCount(DataGridViewElementStates.Selected);
            if (selectedCellCount > Convert.ToInt32(1))
            {

                messem = dgvdetalle.Rows[dgvdetalle.SelectedCells[1].RowIndex].Cells[0].Value.ToString();
                anio = dgvdetalle.Rows[dgvdetalle.SelectedCells[1].RowIndex].Cells[2].Value.ToString();
                idtipocal = dgvdetalle.Rows[dgvdetalle.SelectedCells[1].RowIndex].Cells[3].Value.ToString();
                idcia = dgvdetalle.Rows[dgvdetalle.SelectedCells[1].RowIndex].Cells[6].Value.ToString();
                idtipoper = dgvdetalle.Rows[dgvdetalle.SelectedCells[1].RowIndex].Cells[7].Value.ToString();


                DialogResult resultado = MessageBox.Show("¿Realmente Desea ACTIVAR el Calendario " + messem + '-' + anio + "?",
                "Consulta Importante",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);
                if (resultado == DialogResult.Yes)
                {
                    CalPlan calplan = new CalPlan();
                    calplan.cambiaEstadoCalPlan(messem, anio, idtipoper, idcia, idtipocal, "V");
                    this.ui_ListaCalPlan();
                }

            }
            else
            {
                MessageBox.Show("No ha seleccionado Periodo Laboral a ACTIVAR", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }
    }
}