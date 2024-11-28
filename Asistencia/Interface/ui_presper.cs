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
    public partial class ui_presper : Form
    {
        public ui_presper()
        {
            InitializeComponent();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ui_ListaPresPer()
        {
            try
            {
                Funciones funciones = new Funciones();
                GlobalVariables variables = new GlobalVariables();
                string idcia = variables.getValorCia();
                string idtipoper = cmbTipoPer.Text.Substring(0, 1);
                string estado = cmbEstado.Text.Substring(0, 1);
                string suspendido = cmbSuspendido.Text.Trim();

                string cadenaEstado = string.Empty;
                string cadenaSuspendido = string.Empty;

                if (estado.Equals("C"))
                {
                    cadenaEstado = " having saldo > 0 ";
                }
                else
                {
                    if (estado.Equals("S"))
                    {
                        cadenaEstado = " having (saldo = 0) ";
                    }
                }

                if (suspendido != "XX")
                {
                    if (suspendido.Equals("SI"))
                    {
                        cadenaSuspendido = " and A.suspendido='1' ";
                    }
                    else
                    {
                        cadenaSuspendido = " and A.suspendido='0' ";
                    }
                }

                string query = "select A.idpresper,CONCAT(B.apepat,' ',B.apemat,' ',B.nombres) as nombre,";
                query = query + "A.fecha,A.mon,A.importe,C.desmaesgen as MotivoPrestamo,A.cuota,CASE ISNULL(D.importe) WHEN 1 THEN A.importe WHEN 0 THEN A.importe+sum(CASE D.tipo WHEN '+' THEN D.importe WHEN '-' THEN D.importe*-1 END) END as saldo,CASE A.suspendido WHEN '1' THEN 'SI' WHEN '0' THEN 'NO' END,A.motivo, ";
                query = query + "A.comen,A.suspendido,A.idcia,A.idperplan,A.tipodocpres,A.nrodocpres from presper A left join perplan B ";
                query = query + "on A.idperplan=B.idperplan and A.idcia=B.idcia left join maesgen C on ";
                query = query + "A.motivo=C.clavemaesgen and C.idmaesgen='032' left join view_detpresper D on A.idpresper=D.idpresper and A.idcia=D.idcia ";
                query = query + " where A.idcia='" + @idcia + "' and B.idtipoper='" + @idtipoper + "' " + cadenaSuspendido;
                query = query + "group by A.idpresper,B.apepat,B.apemat,B.nombres,A.fecha,A.mon,A.importe,C.desmaesgen,A.cuota,A.suspendido,A.motivo, ";
                query = query + "A.comen,A.suspendido,A.idcia,A.idperplan,A.tipodocpres,A.nrodocpres ";
                query = query + cadenaEstado;
                query = query + " order by A.idperplan,A.idpresper asc;";

                SqlConnection conexion = new SqlConnection();
                conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
                conexion.Open();

                try
                {
                    using (SqlDataAdapter myDataAdapter = new SqlDataAdapter(query, conexion))
                    {
                        DataSet myDataSet = new DataSet();
                        myDataAdapter.Fill(myDataSet, "tblPresPer");

                        funciones.formatearDataGridView(dgvdetalle);

                        dgvdetalle.DataSource = myDataSet.Tables["tblPresPer"];
                        dgvdetalle.Columns[0].HeaderText = "Código";
                        dgvdetalle.Columns[1].HeaderText = "Trabajador";
                        dgvdetalle.Columns[2].HeaderText = "Fecha";
                        dgvdetalle.Columns[3].HeaderText = "Moneda";
                        dgvdetalle.Columns[4].HeaderText = "Importe";
                        dgvdetalle.Columns[5].HeaderText = "Motivo del Préstamo";
                        dgvdetalle.Columns[6].HeaderText = "Cuota Mensual";
                        dgvdetalle.Columns[7].HeaderText = "Saldo";
                        dgvdetalle.Columns[8].HeaderText = "¿Préstamo Suspendido?";
                        dgvdetalle.Columns["motivo"].Visible = false;
                        dgvdetalle.Columns["comen"].Visible = false;
                        dgvdetalle.Columns["suspendido"].Visible = false;
                        dgvdetalle.Columns["idcia"].Visible = false;
                        dgvdetalle.Columns["idperplan"].Visible = false;
                        dgvdetalle.Columns["tipodocpres"].Visible = false;
                        dgvdetalle.Columns["nrodocpres"].Visible = false;
                        dgvdetalle.Columns[6].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                        dgvdetalle.Columns[7].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                        dgvdetalle.Columns[6].DefaultCellStyle.Format = "###,###.##";
                        dgvdetalle.Columns[7].DefaultCellStyle.Format = "###,###.##";
                        dgvdetalle.Columns[0].Width = 50;
                        dgvdetalle.Columns[1].Width = 220;
                        dgvdetalle.Columns[2].Width = 75;
                        dgvdetalle.Columns[3].Width = 75;
                        dgvdetalle.Columns[4].Width = 50;
                        dgvdetalle.Columns[5].Width = 220;
                        dgvdetalle.Columns[6].Width = 75;
                        dgvdetalle.Columns[7].Width = 75;
                        dgvdetalle.Columns[8].Width = 80;

                    }
                    ui_calculaNumRegistros();
                    txtImpPrestamo.Text = Convert.ToString(funciones.sumaColumnaDataGridView(dgvdetalle, 4));
                    txtImpSaldo.Text = Convert.ToString(funciones.sumaColumnaDataGridView(dgvdetalle, 7));
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }

                conexion.Close();
            }
            catch (ArgumentOutOfRangeException)
            {

            }


        }

        private void ui_presper_Load(object sender, EventArgs e)
        {

            string squery;
            squery = "SELECT idtipoper as clave,destipoper as descripcion FROM tipoper order by 1 asc;";
            Funciones funciones = new Funciones();
            cmbEstado.Text = "C   CON SALDO";
            cmbSuspendido.Text = "NO";
            funciones.listaToolStripComboBox(squery, cmbTipoPer);
            ui_ListaPresPer();
        }

        internal void ui_calculaNumRegistros()
        {
            GlobalVariables variables = new GlobalVariables();
            PresPer presper = new PresPer();
            Funciones funciones = new Funciones();
            string idtipoper = cmbTipoPer.Text.Substring(0, 1);
            txtRegTotal.Text = string.Empty;
            string numreg = presper.getNumeroRegistros(variables.getValorCia(), idtipoper);
            txtRegTotal.Text = funciones.replicateCadena(" ", 15 - numreg.Trim().Length) + numreg.Trim() + funciones.replicateCadena(" ", 20) + "Registrados";
            string numregencontrados = Convert.ToString(dgvdetalle.RowCount);
            txtRegEncontrados.Text = funciones.replicateCadena(" ", 15 - numregencontrados.Trim().Length) + numregencontrados.Trim() + funciones.replicateCadena(" ", 20) + " De acuerdo al criterio de búsqueda";

        }

        private void cmbTipoPer_SelectedIndexChanged(object sender, EventArgs e)
        {
            ui_ListaPresPer();
        }

        private void btnTodos_Click(object sender, EventArgs e)
        {
            ui_ListaPresPer();
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            if (cmbTipoPer.Text.Trim() != string.Empty)
            {
                string idtipoper = cmbTipoPer.Text.Substring(0, 1);
                ui_updpresper ui_detalle = new ui_updpresper();
                ui_detalle._FormPadre = this;
                ui_detalle.ui_newPresPer(idtipoper);
                ui_detalle.Activate();
                ui_detalle.BringToFront();
                ui_detalle.ShowDialog();
                ui_detalle.Dispose();
            }
            else
            {
                MessageBox.Show("No ha seleccionado Tipo de Personal", "Avios", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            string idpresper;
            string idperplan;
            string fecha;
            float importe;
            float cuota;
            string mon;
            string motivo;
            string tipodocpres;
            string nrodocpres;
            string comen;
            string suspendido;
            string idcia;

            string idtipoper = cmbTipoPer.Text.Substring(0, 1);

            Int32 selectedCellCount = dgvdetalle.GetCellCount(DataGridViewElementStates.Selected);
            if (selectedCellCount > Convert.ToInt32(0))
            {
                idpresper = dgvdetalle.Rows[dgvdetalle.SelectedCells[1].RowIndex].Cells[0].Value.ToString();
                fecha = dgvdetalle.Rows[dgvdetalle.SelectedCells[1].RowIndex].Cells[2].Value.ToString();
                mon = dgvdetalle.Rows[dgvdetalle.SelectedCells[1].RowIndex].Cells[3].Value.ToString();
                importe = float.Parse(dgvdetalle.Rows[dgvdetalle.SelectedCells[1].RowIndex].Cells[4].Value.ToString());
                cuota = float.Parse(dgvdetalle.Rows[dgvdetalle.SelectedCells[1].RowIndex].Cells[6].Value.ToString());
                motivo = dgvdetalle.Rows[dgvdetalle.SelectedCells[1].RowIndex].Cells[9].Value.ToString();
                comen = dgvdetalle.Rows[dgvdetalle.SelectedCells[1].RowIndex].Cells[10].Value.ToString();
                suspendido = dgvdetalle.Rows[dgvdetalle.SelectedCells[1].RowIndex].Cells[11].Value.ToString();
                idcia = dgvdetalle.Rows[dgvdetalle.SelectedCells[1].RowIndex].Cells[12].Value.ToString();
                idperplan = dgvdetalle.Rows[dgvdetalle.SelectedCells[1].RowIndex].Cells[13].Value.ToString();
                tipodocpres = dgvdetalle.Rows[dgvdetalle.SelectedCells[1].RowIndex].Cells[14].Value.ToString();
                nrodocpres = dgvdetalle.Rows[dgvdetalle.SelectedCells[1].RowIndex].Cells[15].Value.ToString();

                ui_updpresper ui_detalle = new ui_updpresper();
                ui_detalle._FormPadre = this;
                ui_detalle.Activate();
                ui_detalle.ui_loadPresPer(idpresper, idperplan, fecha, importe, cuota, tipodocpres, nrodocpres, comen, motivo, mon, suspendido, idtipoper);
                ui_detalle.BringToFront();
                ui_detalle.ShowDialog();
                ui_detalle.Dispose();

            }
            else
            {
                MessageBox.Show("No ha seleccionado registro a Editar", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            ui_ListaPresPer();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            string idcia;
            string idpresper;

            Int32 selectedCellCount = dgvdetalle.GetCellCount(DataGridViewElementStates.Selected);
            if (selectedCellCount > Convert.ToInt32(0))
            {
                idpresper = dgvdetalle.Rows[dgvdetalle.SelectedCells[1].RowIndex].Cells[0].Value.ToString();
                idcia = dgvdetalle.Rows[dgvdetalle.SelectedCells[1].RowIndex].Cells[12].Value.ToString();

                DialogResult resultado = MessageBox.Show("¿Desea eliminar el Préstamo al Personal N° " + idpresper + "?",
                "Consulta Importante",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);
                if (resultado == DialogResult.Yes)
                {
                    PresPer presper = new PresPer();
                    presper.eliminarPresPer(idpresper, idcia);
                    ui_ListaPresPer();
                }

            }
            else
            {
                MessageBox.Show("No ha seleccionado registro a Eliminar", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            Exporta exporta = new Exporta();
            exporta.Excel_FromDataGridView(dgvdetalle);
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            string buscar = txtBuscar.Text.Trim();

            if (buscar != string.Empty)
            {
                try
                {
                    GlobalVariables variables = new GlobalVariables();
                    Funciones funciones = new Funciones();
                    PresPer presper = new PresPer();
                    txtRegEncontrados.Text = string.Empty;
                    string idcia = variables.getValorCia();
                    string idtipoper = cmbTipoPer.Text.Substring(0, 1);
                    string estado = cmbEstado.Text.Substring(0, 1);
                    string suspendido = cmbSuspendido.Text.Trim();

                    string cadenaBusqueda = string.Empty;


                    if (radioButtonCodigoPrestamo.Checked && txtBuscar.Text.Trim() != string.Empty)
                    {
                        cadenaBusqueda = " and A.idpresper='" + @buscar + "' ";
                    }
                    else
                    {
                        if (radioButtonNombre.Checked && txtBuscar.Text.Trim() != string.Empty)
                        {
                            cadenaBusqueda = " and CONCAT(B.apepat,' ',B.apemat,' ',B.nombres) like '%" + @buscar + "%' ";
                        }
                        else
                        {
                            if (radioButtonCodigoPersonal.Checked && txtBuscar.Text.Trim() != string.Empty)
                            {
                                cadenaBusqueda = " and B.idperplan='" + @buscar + "' ";
                            }

                        }

                    }

                    string cadenaEstado = string.Empty;
                    string cadenaSuspendido = string.Empty;

                    if (estado.Equals("C"))
                    {
                        cadenaEstado = " having saldo > 0 ";
                    }
                    else
                    {
                        if (estado.Equals("S"))
                        {
                            cadenaEstado = " having (saldo = 0) ";
                        }
                    }

                    if (suspendido != "XX")
                    {
                        if (suspendido.Equals("SI"))
                        {
                            cadenaSuspendido = " and A.suspendido='1' ";
                        }
                        else
                        {
                            cadenaSuspendido = " and A.suspendido='0' ";
                        }
                    }

                    string query = "select A.idpresper,CONCAT(B.apepat,' ',B.apemat,' ',B.nombres) as nombre,";
                    query = query + "A.fecha,A.mon,A.importe,C.desmaesgen as MotivoPrestamo,A.cuota,CASE ISNULL(D.importe) WHEN 1 THEN A.importe WHEN 0 THEN A.importe+sum(CASE D.tipo WHEN '+' THEN D.importe WHEN '-' THEN D.importe*-1 END) END as saldo,CASE A.suspendido WHEN '1' THEN 'SI' WHEN '0' THEN 'NO' END,A.motivo, ";
                    query = query + "A.comen,A.suspendido,A.idcia,A.idperplan,A.tipodocpres,A.nrodocpres from presper A left join perplan B ";
                    query = query + "on A.idperplan=B.idperplan and A.idcia=B.idcia left join maesgen C on ";
                    query = query + "A.motivo=C.clavemaesgen and C.idmaesgen='032' left join view_detpresper D on A.idpresper=D.idpresper and A.idcia=D.idcia ";
                    query = query + " where A.idcia='" + @idcia + "' and B.idtipoper='" + @idtipoper + "' " + @cadenaBusqueda + @cadenaSuspendido;
                    query = query + "group by A.idpresper,B.apepat,B.apemat,B.nombres,A.fecha,A.mon,A.importe,C.desmaesgen,A.cuota,A.suspendido,A.motivo, ";
                    query = query + "A.comen,A.suspendido,A.idcia,A.idperplan,A.tipodocpres,A.nrodocpres ";
                    query = query + cadenaEstado;
                    query = query + " order by A.idperplan,A.idpresper asc;";

                    SqlConnection conexion = new SqlConnection();
                    conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
                    conexion.Open();

                    try
                    {
                        using (SqlDataAdapter myDataAdapter = new SqlDataAdapter(query, conexion))
                        {
                            DataSet myDataSet = new DataSet();
                            myDataAdapter.Fill(myDataSet, "tblPresPer");

                            funciones.formatearDataGridView(dgvdetalle);

                            dgvdetalle.DataSource = myDataSet.Tables["tblPresPer"];
                            dgvdetalle.Columns[0].HeaderText = "Código";
                            dgvdetalle.Columns[1].HeaderText = "Trabajador";
                            dgvdetalle.Columns[2].HeaderText = "Fecha";
                            dgvdetalle.Columns[3].HeaderText = "Moneda";
                            dgvdetalle.Columns[4].HeaderText = "Importe";
                            dgvdetalle.Columns[5].HeaderText = "Motivo del Préstamo";
                            dgvdetalle.Columns[6].HeaderText = "Cuota Mensual";
                            dgvdetalle.Columns[7].HeaderText = "Saldo";
                            dgvdetalle.Columns[8].HeaderText = "¿Préstamo Suspendido?";
                            dgvdetalle.Columns["motivo"].Visible = false;
                            dgvdetalle.Columns["comen"].Visible = false;
                            dgvdetalle.Columns["suspendido"].Visible = false;
                            dgvdetalle.Columns["idcia"].Visible = false;
                            dgvdetalle.Columns["idperplan"].Visible = false;
                            dgvdetalle.Columns["tipodocpres"].Visible = false;
                            dgvdetalle.Columns["nrodocpres"].Visible = false;

                            dgvdetalle.Columns[0].Width = 50;
                            dgvdetalle.Columns[1].Width = 220;
                            dgvdetalle.Columns[2].Width = 75;
                            dgvdetalle.Columns[3].Width = 75;
                            dgvdetalle.Columns[4].Width = 50;
                            dgvdetalle.Columns[5].Width = 220;
                            dgvdetalle.Columns[6].Width = 75;
                            dgvdetalle.Columns[7].Width = 75;
                            dgvdetalle.Columns[8].Width = 80;

                        }
                        ui_calculaNumRegistros();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    }

                    conexion.Close();
                }
                catch (ArgumentOutOfRangeException) { }
            }
        }

        private void radioButtonCodigoPrestamo_CheckedChanged(object sender, EventArgs e)
        {
            txtBuscar.Text = string.Empty;
            txtBuscar.Focus();
        }

        private void radioButtonCodigoPersonal_CheckedChanged(object sender, EventArgs e)
        {
            txtBuscar.Text = string.Empty;
            txtBuscar.Focus();
        }

        private void radioButtonNombre_CheckedChanged(object sender, EventArgs e)
        {
            txtBuscar.Text = string.Empty;
            txtBuscar.Focus();
        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void cmbTipoPer_Click(object sender, EventArgs e)
        {

        }

        private void cmbEstado_Click(object sender, EventArgs e)
        {

        }

        private void cmbSuspendido_Click(object sender, EventArgs e)
        {

        }

        private void cmbEstado_SelectedIndexChanged(object sender, EventArgs e)
        {
            ui_ListaPresPer();
        }

        private void cmbSuspendido_SelectedIndexChanged(object sender, EventArgs e)
        {
            ui_ListaPresPer();
        }

        private void cmbEstado_TextChanged(object sender, EventArgs e)
        {

        }

        private void cmbTipoPer_TextChanged(object sender, EventArgs e)
        {

        }

        private void cmbSuspendido_TextChanged(object sender, EventArgs e)
        {

        }

        private void toolstripform_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void dgvdetalle_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}