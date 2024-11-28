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
    public partial class ui_usuarios : Form
    {
        Funciones funciones = new Funciones();
        private string cadenaConexion;

        public ui_usuarios()
        {
            InitializeComponent();

            if (funciones.VersionAssembly()) Application.ExitThread();
        }

        private ui_updusuarios form = null;
        private ui_accesousrcia formusrcia = null;

        private ui_updusuarios FormInstance
        {
            get
            {
                if (form == null)
                {
                    form = new ui_updusuarios();
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
            string query = "select a.idusr,a.desusr,a.typeusr,a.stateusr,a.passusr,isnull(a.email,'') as email";
            query += ",b.desmaesgen AS type ";
            query += ",(CASE a.stateusr WHEN 'V' THEN 'VIGENTE' ELSE 'ANULADO' END) AS state ";
            query += "FROM usrfile a (NOLOCK) INNER JOIN maesgen b (NOLOCK) ON b.idmaesgen='039' ";
            query += "AND b.clavemaesgen=a.typeusr ";
            if (buscar.Trim() != string.Empty) { query += "AND a.desusr LIKE '%" + buscar + "%' "; }
            query += "ORDER BY a.typeusr asc;";

            try
            {
                using (SqlDataAdapter myDataAdapter = new SqlDataAdapter(query, conexion))
                {
                    DataSet myDataSet = new DataSet();
                    myDataAdapter.Fill(myDataSet, "tblUsuarios");
                    funciones.formatearDataGridView(dgvUsuarios);
                    dgvUsuarios.DataSource = myDataSet.Tables["tblUsuarios"];
                    dgvUsuarios.Columns[0].HeaderText = "Usuario";
                    dgvUsuarios.Columns[1].HeaderText = "Propietario";
                    dgvUsuarios.Columns[5].HeaderText = "E-mail";
                    dgvUsuarios.Columns[6].HeaderText = "Perfil";
                    dgvUsuarios.Columns[7].HeaderText = "Estado";
                    dgvUsuarios.Columns["passusr"].Visible = false;
                    dgvUsuarios.Columns["stateusr"].Visible = false;
                    dgvUsuarios.Columns["typeusr"].Visible = false;
                    dgvUsuarios.Columns[0].Width = 100;
                    dgvUsuarios.Columns[1].Width = 250;
                    dgvUsuarios.Columns[2].Width = 60;
                    dgvUsuarios.Columns[3].Width = 60;
                    dgvUsuarios.Columns[5].Width = 230;
                    dgvUsuarios.Columns[7].Width = 80;

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
            string idUsr, desUsr, passDecryptUsr, passEncryptUsr, typeUsr, stateUsr, mail;

            Int32 selectedCellCount =
            dgvUsuarios.GetCellCount(DataGridViewElementStates.Selected);
            if (selectedCellCount > Convert.ToInt32(0))
            {
                idUsr = dgvUsuarios.Rows[dgvUsuarios.SelectedCells[1].RowIndex].Cells[0].Value.ToString();
                desUsr = dgvUsuarios.Rows[dgvUsuarios.SelectedCells[1].RowIndex].Cells[1].Value.ToString();
                typeUsr = dgvUsuarios.Rows[dgvUsuarios.SelectedCells[1].RowIndex].Cells[2].Value.ToString();
                stateUsr = dgvUsuarios.Rows[dgvUsuarios.SelectedCells[1].RowIndex].Cells[3].Value.ToString();
                passEncryptUsr = dgvUsuarios.Rows[dgvUsuarios.SelectedCells[1].RowIndex].Cells[4].Value.ToString();
                mail = dgvUsuarios.Rows[dgvUsuarios.SelectedCells[1].RowIndex].Cells[5].Value.ToString();

                String passPhrase = ConfigurationManager.AppSettings.Get("PASS_PHRASE");
                String saltValue = ConfigurationManager.AppSettings.Get("SALT_VALUE");
                String hashAlgorithm = ConfigurationManager.AppSettings.Get("HASH_ALGORITHM");
                int passwordIterations = Convert.ToInt32(ConfigurationManager.AppSettings.Get("PASSWORD_ITERATIONS"));
                String initVector = ConfigurationManager.AppSettings.Get("INIT_VECTOR");
                int keySize = Convert.ToInt32(ConfigurationManager.AppSettings.Get("KEY_SIZE"));

                RijndaelSimple RijndaelSimple = new RijndaelSimple();
                passDecryptUsr = RijndaelSimple.Decrypt(passEncryptUsr, passPhrase, saltValue, hashAlgorithm, passwordIterations, initVector, keySize);

                ui_updusuarios ui_detalle = this.FormInstance;
                ui_detalle._FormPadre = this;
                ui_detalle.LoadCombos();
                ui_detalle.LoadUsuarios(idUsr, desUsr, passDecryptUsr, typeUsr, stateUsr, mail);
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
            ui_updusuarios ui_detalle = this.FormInstance;
            ui_detalle._FormPadre = this;
            ui_detalle.LoadCombos();
            ui_detalle.NewUsuario();
            ui_detalle.Activate();
            ui_detalle.BringToFront();
            ui_detalle.ShowDialog();
            ui_detalle.Dispose();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            Int32 selectedCellCount =
            dgvUsuarios.GetCellCount(DataGridViewElementStates.Selected);
            if (selectedCellCount > 0)
            {
                string sidusr = dgvUsuarios.Rows[dgvUsuarios.SelectedCells[1].RowIndex].Cells[0].Value.ToString();
                string sdesusr = dgvUsuarios.Rows[dgvUsuarios.SelectedCells[1].RowIndex].Cells[1].Value.ToString();
                DialogResult resultado = MessageBox.Show("¿Desea eliminar el usuario " + sdesusr + "?",
                "Consulta Importante",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);
                if (resultado == DialogResult.Yes)
                {
                    UsrFile usrfile = new UsrFile();
                    usrfile.eliminarUsr(sidusr);
                    this.ui_Listausuarios();
                }
            }
            else
            {
                MessageBox.Show("No ha seleccionado registro a Eliminar", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            ui_Listausuarios();
        }

        private void btncompanias_Click(object sender, EventArgs e)
        {
            Int32 selectedCellCount = dgvUsuarios.GetCellCount(DataGridViewElementStates.Selected);
            if (selectedCellCount > 0)
            {
                string idusr = dgvUsuarios.Rows[dgvUsuarios.SelectedCells[1].RowIndex].Cells[0].Value.ToString();
                string desusr = dgvUsuarios.Rows[dgvUsuarios.SelectedCells[1].RowIndex].Cells[1].Value.ToString();

                ui_accesousrcia ui_accesousrcia = this.FormInstanceUsrCia;
                ui_accesousrcia._FormPadre = this;
                ui_accesousrcia.usuarioActivo(idusr, desusr);
                ui_accesousrcia.Activate();
                ui_accesousrcia.BringToFront();
                ui_accesousrcia.ShowDialog();
                ui_accesousrcia.Dispose();
            }
            else
            {
                MessageBox.Show("No ha seleccionado Usuario a asignar Compañías", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
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

        private void btnClean_Click(object sender, EventArgs e)
        {
            ui_Listausuarios();
            txtBuscar.Clear();
            txtBuscar.Focus();
        }

        private void btnMenuApp_Click(object sender, EventArgs e)
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
                    ui_accesomenuapp ui_accesomenu = new ui_accesomenuapp();
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

        private void btnNivel_Click(object sender, EventArgs e)
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
                    ui_accesonivel ui_accesomenu = new ui_accesonivel();
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

        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            ui_Listausuarios();
        }
    }
}