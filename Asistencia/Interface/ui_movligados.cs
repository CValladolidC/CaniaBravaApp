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
    public partial class ui_movligados : ui_form
    {
        public string _codcia;
        public string _alma;
        public string _td;
        public string _numdoc;
        public string _item;
        public string _reporte;
        public string _desalma;

        public ui_movligados()
        {
            InitializeComponent();
        }

        private void ui_movligados_Load(object sender, EventArgs e)
        {
            ui_ListaEnlazados();
        }

       

        private void ui_ListaEnlazados()
        {

            try
            {
                DataTable dtmov = new DataTable();
                string codcia = this._codcia;
                string alma = this._alma;
                string td = this._td;
                string numdoc = this._numdoc;
                string item = this._item;
                string reporte = this._reporte;
                string query;

                SqlConnection conexion = new SqlConnection();
                conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
                conexion.Open();

                if (reporte.Equals("D"))
                {
                    query = " select A.alma,A.td,A.numdoc,A.fecdoc,A.codmov,B.item,C.desarti,B.lote,";
                    query = query + " B.cantidad from almovc A left join almovd B on ";
                    query = query + " A.codcia=B.codcia and A.alma=B.alma and A.td=B.td and A.numdoc=B.numdoc ";
                    query = query + " left join alarti C on B.codcia=C.codcia and B.codarti=C.codarti ";
                    query = query + " where A.codcia='" + @codcia + "' and A.alma='" + @alma + "' and A.td='PS' and B.lote in ";
                    query = query + " (Select lote from almovd where codcia='" + @codcia + "' and alma='" + @alma + "' ";
                    query = query + " and td='" + @td + "' and numdoc='" + @numdoc + "' and item='" + @item + "') ";
                    query = query + " order by A.alma asc ,A.td asc ,A.numdoc asc;";
                }
                else
                {
                    query = " select A.alma,A.td,A.numdoc,A.fecdoc,A.codmov,B.item,C.desarti,B.lote,";
                    query = query + " B.cantidad from almovc A left join almovd B on ";
                    query = query + " A.codcia=B.codcia and A.alma=B.alma and A.td=B.td and A.numdoc=B.numdoc ";
                    query = query + " left join alarti C on B.codcia=C.codcia and B.codarti=C.codarti ";
                    query = query + " where A.codcia='" + @codcia + "' and A.alma='" + @alma + "' and A.td='PS' and B.lote in ";
                    query = query + " (Select lote from almovd where codcia='" + @codcia + "' and alma='" + @alma + "' ";
                    query = query + " and td='" + @td + "' and numdoc='" + @numdoc + "') ";
                    query = query + " order by A.alma asc ,A.td asc ,A.numdoc asc;";
                }

                SqlDataAdapter damov = new SqlDataAdapter();
                damov.SelectCommand = new SqlCommand(query, conexion);
                damov.Fill(dtmov);
                ui_mostrar(dtmov);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        
        }

        private void ui_mostrar(DataTable dt)
        {
            try
            {
                Funciones funciones = new Funciones();
                funciones.formatearDataGridView(dgvData);
                dgvData.DataSource = dt;
                dgvData.Columns[0].HeaderText = "Almacén";
                dgvData.Columns[1].HeaderText = "Tipo";
                dgvData.Columns[2].HeaderText = "Num.Ope.";
                dgvData.Columns[3].HeaderText = "Fecha";
                dgvData.Columns[4].HeaderText = "Cod. Mov.";
                dgvData.Columns[5].HeaderText = "Item";
                dgvData.Columns[6].HeaderText = "Existencia";
                dgvData.Columns[7].HeaderText = "Lote";
                dgvData.Columns[8].HeaderText = "Cantidad";


                dgvData.Columns[0].Width = 50;
                dgvData.Columns[1].Width = 40;
                dgvData.Columns[2].Width = 80;
                dgvData.Columns[3].Width = 75;
                dgvData.Columns[4].Width = 50;
                dgvData.Columns[5].Width = 50;
                dgvData.Columns[6].Width = 350;
                dgvData.Columns[7].Width = 120;
                dgvData.Columns[8].Width = 75;


                string numReg = Convert.ToString(dgvData.RowCount);
                txtRegTotal.Text = funciones.replicateCadena(" ", 15 - numReg.Trim().Length) + numReg.Trim() + funciones.replicateCadena(" ", 20) + " Movimientos Enlazados.";

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
    
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            ui_ListaEnlazados();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            Int32 selectedCellCount = dgvData.GetCellCount(DataGridViewElementStates.Selected);
            if (selectedCellCount > 0)
            {
                string codcia = this._codcia;
                string desalma = this._desalma;
                string alma = dgvData.Rows[dgvData.SelectedCells[1].RowIndex].Cells[0].Value.ToString();
                string td = dgvData.Rows[dgvData.SelectedCells[1].RowIndex].Cells[1].Value.ToString();
                string numdoc = dgvData.Rows[dgvData.SelectedCells[1].RowIndex].Cells[2].Value.ToString();
                ui_updalmov ui_updalmov = new ui_updalmov();
                ui_updalmov._FormPadre = this;
                ui_updalmov.Activate();
                ui_updalmov.BringToFront();
                ui_updalmov.setData(codcia, alma, desalma);
                ui_updalmov.ui_ActualizaComboBox();
                ui_updalmov.editar(codcia, alma, td, numdoc);
                ui_updalmov.ShowDialog();
                ui_updalmov.Dispose();
            }
            else
            {
                MessageBox.Show("No ha seleccionado registro a Visualizar", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }
    }
}
