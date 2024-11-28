using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data.Common;
using System.Configuration;

namespace CaniaBrava
{
    public partial class ui_comensales : Form
    {
        Funciones funciones = new Funciones();
        private string cadenaConexion;

        public ui_comensales()
        {
            InitializeComponent();

            if (funciones.VersionAssembly()) Application.ExitThread();
        }

        private ui_updcomensales form = null;
        private ui_accesousrcia formusrcia = null;

        private ui_updcomensales FormInstance
        {
            get
            {
                if (form == null)
                {
                    form = new ui_updcomensales();
                    form.Disposed += new EventHandler(form_Disposed);

                }

                return form;
            }
        }

        void form_Disposed(object sender, EventArgs e)
        {
            form = null;
        }

        private ui_accesousrcia FormInstanceUsrCia
        {
            get
            {
                if (formusrcia == null)
                {
                    formusrcia = new ui_accesousrcia();
                    formusrcia.Disposed += new EventHandler(formusrcia_Disposed);
                }

                return formusrcia;
            }
        }

        void formusrcia_Disposed(object sender, EventArgs e)
        {
            formusrcia = null;
        }

        private void ui_usuarios_Load(object sender, EventArgs e)
        {
            ui_Listausuarios();
        }

        private void ui_Listausuarios()
        {
            SqlConnection conexion = new SqlConnection();
            this.cadenaConexion = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.ConnectionString = cadenaConexion;
            conexion.Open();

            string buscar = txtBuscar.Text;
            string query = "SELECT a.idperplan [Cod.Trabajador],a.DNI, CONVERT(VARCHAR(10),a.fecha,120) [Fecha], a.Comensal, ";
            query += "CASE a.tipomov WHEN 'I' THEN 'COMENSAL' ELSE  SUBSTRING(e.destipoper, 0, CHARINDEX (' ', e.destipoper)) END AS Perfil, ";
            query += "CASE CAST(LEFT(CAST(a.regEntrada AS TIME), 2) AS INT) ";
            query += "WHEN 6 THEN 'DESAYUNO' WHEN 7 THEN 'DESAYUNO' WHEN 8 THEN 'DESAYUNO' ";
            query += "WHEN 12 THEN 'ALMUERZO' WHEN 13 THEN 'ALMUERZO' WHEN 14 THEN 'ALMUERZO' WHEN 15 THEN 'ALMUERZO' ";
            query += "WHEN 19 THEN 'CENA' WHEN 20 THEN 'CENA' WHEN 21 THEN 'CENA' ";
            query += "END AS [T. Servicio],a.nromov ";
            query += "FROM control_comensales a (NOLOCK) ";
            query += "INNER JOIN perplan b (NOLOCK) ON b.nrodoc = a.dni ";
            query += "INNER JOIN tipoper e (NOLOCK) ON e.idtipoper = b.idtipoper ";
            query += "WHERE a.fecha = '" + dtpFecha.Value.ToString("yyyy-MM-dd") + "' AND a.tipomov=(CASE b.idtipoper WHEN 'Y9' THEN 'T' WHEN 'Y2' THEN 'T' WHEN 'Y4' THEN 'T' ELSE 'I' END) ";
            if (buscar.Trim() != string.Empty) { query += "AND a.comensal LIKE '%" + buscar + "%' "; }
            query += "ORDER BY a.regEntrada;";

            try
            {
                using (SqlDataAdapter myDataAdapter = new SqlDataAdapter(query, conexion))
                {
                    DataSet myDataSet = new DataSet();
                    myDataAdapter.Fill(myDataSet);
                    funciones.formatearDataGridView(dgvUsuarios);
                    dgvUsuarios.DataSource = myDataSet.Tables[0];
                    dgvUsuarios.Columns[0].Width = 100;
                    dgvUsuarios.Columns[1].Width = 80;
                    dgvUsuarios.Columns[2].Width = 80;
                    dgvUsuarios.Columns[3].Width = 250;
                    dgvUsuarios.Columns[4].Width = 80;
                    dgvUsuarios.Columns[6].Visible = false;

                    dgvUsuarios.AllowUserToResizeRows = false;
                    dgvUsuarios.AllowUserToResizeColumns = false;
                    foreach (DataGridViewColumn column in dgvUsuarios.Columns)
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

        private void btnEditar_Click(object sender, EventArgs e)
        {
            Int32 selectedCellCount = dgvUsuarios.GetCellCount(DataGridViewElementStates.Selected);
            if (selectedCellCount > Convert.ToInt32(0))
            {
                string idperplan = dgvUsuarios.Rows[dgvUsuarios.SelectedCells[1].RowIndex].Cells[0].Value.ToString();
                string dni = dgvUsuarios.Rows[dgvUsuarios.SelectedCells[1].RowIndex].Cells[1].Value.ToString();
                string fecha = dgvUsuarios.Rows[dgvUsuarios.SelectedCells[1].RowIndex].Cells[2].Value.ToString();
                string comensal = dgvUsuarios.Rows[dgvUsuarios.SelectedCells[1].RowIndex].Cells[3].Value.ToString();
                string servicio = dgvUsuarios.Rows[dgvUsuarios.SelectedCells[1].RowIndex].Cells[5].Value.ToString();
                string nromov = dgvUsuarios.Rows[dgvUsuarios.SelectedCells[1].RowIndex].Cells[6].Value.ToString();

                ui_updcomensales ui_detalle = this.FormInstance;
                ui_detalle._FormPadre = this;
                //ui_detalle.LimpiarFiltros();
                ui_detalle.LoadComensal(idperplan, dni, fecha, comensal, nromov, servicio);
                ui_detalle.Activate();
                ui_detalle.BringToFront();
                ui_detalle.ShowDialog();
            }
            else
            {
                MessageBox.Show("No ha seleccionado registro a Editar", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            ui_updcomensales ui_detalle = this.FormInstance;
            ui_detalle._FormPadre = this;
            //ui_detalle.LoadCombos();
            //ui_detalle.NewUsuario();
            ui_detalle.Activate();
            ui_detalle.BringToFront();
            ui_detalle.ShowDialog();
            ui_detalle.Dispose();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            ui_Listausuarios();
        }

        private void btnMenu_Click(object sender, EventArgs e)
        {
            Int32 selectedCellCount = dgvUsuarios.GetCellCount(DataGridViewElementStates.Selected);
            if (selectedCellCount > 0)
            {
                string idusr = dgvUsuarios.Rows[dgvUsuarios.SelectedCells[1].RowIndex].Cells[0].Value.ToString();
                string desusr = dgvUsuarios.Rows[dgvUsuarios.SelectedCells[1].RowIndex].Cells[1].Value.ToString();
                UsrFile usrfile = new UsrFile();
                string tipoUsuario = usrfile.ui_getDatos(idusr, "TIPO");
                if (!tipoUsuario.Equals("00"))
                {
                    ui_accesomenu ui_accesomenu = new ui_accesomenu();
                    ui_accesomenu._idusr = idusr;
                    ui_accesomenu._desusr = desusr;
                    ui_accesomenu.Activate();
                    ui_accesomenu.BringToFront();
                    ui_accesomenu.ShowDialog();
                    ui_accesomenu.Dispose();
                }
                else
                {
                    MessageBox.Show("Opción solo para usuarios distintos al perfil \"ADMINISTRADOR\"", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("No ha seleccionado Usuario a asignar Opciones de Menú", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void cmbTipo_SelectedIndexChanged(object sender, EventArgs e)
        {
            ui_Listausuarios();
        }

        private void btnClean_Click(object sender, EventArgs e)
        {
            ui_Listausuarios();
            txtBuscar.Clear();
            txtBuscar.Focus();
        }

        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            ui_Listausuarios();
        }

        private void dtpFecha_ValueChanged(object sender, EventArgs e)
        {
            ui_Listausuarios();
        }
    }
}