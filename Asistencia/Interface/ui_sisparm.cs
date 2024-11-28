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
    public partial class ui_sisparm : Form
    {
        public ui_sisparm()
        {
            InitializeComponent();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void ui_ListaSisParm()
        {

            Funciones funciones = new Funciones();
            GlobalVariables variables = new GlobalVariables();
            string idcia = variables.getValorCia();
            string tipo = cmbTipo.Text.Substring(0, 1);
            string query;

            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();
            if (tipo.Equals("C"))
            {
                query = "select A.idsisparm,A.dessisparm,A.constante,B.fini,B.importe,B.porcentaje from sisparm A left join detsisparm B on A.idsisparm=B.idsisparm and B.state='V' where A.tipo='" + @tipo + "' order by 1 asc;";
            }
            else
            {
                query = "select A.idsisparm,A.dessisparm,A.constante from sisparm A where A.tipo='" + @tipo + "' order by 1 asc;";
            }

            try
            {
                using (SqlDataAdapter myDataAdapter = new SqlDataAdapter(query, conexion))
                {

                    DataSet myDataSet = new DataSet();
                    myDataAdapter.Fill(myDataSet, "tblSisParm");
                    funciones.formatearDataGridView(dgvdetalle);

                    dgvdetalle.DataSource = myDataSet.Tables["tblSisParm"];

                    if (tipo.Equals("C"))
                    {
                        dgvdetalle.Columns[0].HeaderText = "Código";
                        dgvdetalle.Columns[1].HeaderText = "Descripción";
                        dgvdetalle.Columns[2].HeaderText = "Variable en Fórmula";
                        dgvdetalle.Columns[3].HeaderText = "F.Inicio";
                        dgvdetalle.Columns[4].HeaderText = "Importe";
                        dgvdetalle.Columns[5].HeaderText = "Porcentaje";

                        dgvdetalle.Columns[0].Width = 60;
                        dgvdetalle.Columns[1].Width = 300;
                        dgvdetalle.Columns[2].Width = 80;
                        dgvdetalle.Columns[3].Width = 80;
                        dgvdetalle.Columns[4].Width = 80;
                        dgvdetalle.Columns[5].Width = 80;

                        dgvdetalle.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                        dgvdetalle.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                        dgvdetalle.Columns[4].DefaultCellStyle.Format = "###,###.####";
                        dgvdetalle.Columns[5].DefaultCellStyle.Format = "###,###.####";


                    }
                    else
                    {
                        dgvdetalle.Columns[0].HeaderText = "Código";
                        dgvdetalle.Columns[1].HeaderText = "Descripción";
                        dgvdetalle.Columns[2].HeaderText = "Variable en Fórmula";

                        dgvdetalle.Columns[0].Width = 60;
                        dgvdetalle.Columns[1].Width = 300;
                        dgvdetalle.Columns[2].Width = 80;


                    }



                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }



            conexion.Close();
        }

        private void ui_sisparm_Load(object sender, EventArgs e)
        {
            cmbTipo.Text = "C   CONSTANTES";
            ui_ListaSisParm();
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            string idsisparm;
            string dessisparm;
            string tipo = cmbTipo.Text.Substring(0, 1);
            Int32 selectedCellCount =
            dgvdetalle.GetCellCount(DataGridViewElementStates.Selected);
            if (selectedCellCount > Convert.ToInt32(0))
            {
                if (tipo.Equals("C"))
                {
                    idsisparm = dgvdetalle.Rows[dgvdetalle.SelectedCells[1].RowIndex].Cells[0].Value.ToString();
                    dessisparm = dgvdetalle.Rows[dgvdetalle.SelectedCells[1].RowIndex].Cells[1].Value.ToString();

                    ui_updsisparm ui_detalle = new ui_updsisparm();
                    ui_detalle._FormPadre = this;
                    ui_detalle.ui_load(idsisparm, dessisparm);
                    ui_detalle.Activate();
                    ui_detalle.BringToFront();
                    ui_detalle.ShowDialog();
                    ui_detalle.Dispose();
                }
                else
                { MessageBox.Show("Opción utilizada sólo para Variables de Tipo Constante", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information); }

            }
            else
            {
                MessageBox.Show("No ha seleccionado registro a Editar", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            Exporta exporta = new Exporta();
            exporta.Excel_FromDataGridView(dgvdetalle);
        }

        private void toolstripform_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            ui_ListaSisParm();
        }

        private void dgvdetalle_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void cmbTipo_SelectedIndexChanged(object sender, EventArgs e)
        {
            ui_ListaSisParm();
        }
    }
}