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
    public partial class ui_programacionInd : Form
    {
        string _usuario;
        string query = string.Empty;
        Funciones funciones = new Funciones();
        GlobalVariables variables = new GlobalVariables();

        private TextBox TextBoxActivo;
        private Form FormPadre;

        public Form _FormPadre
        {
            get { return FormPadre; }
            set { FormPadre = value; }
        }

        public TextBox _TextBoxActivo
        {
            get { return TextBoxActivo; }
            set { TextBoxActivo = value; }
        }

        public ui_programacionInd()
        {
            InitializeComponent();

            if (funciones.VersionAssembly()) Application.ExitThread();
        }

        //public void setValores(string usuario)
        //{
        //    this._usuario = usuario;
        //    this.Text = "Re-Programación de Horarios";

        //    string query = "SELECT C.idplantiphorario as clave, C.descripcion FROM( ";
        //    query += "SELECT idcia, idtipoper FROM perplan ";
        //    query += "GROUP BY idcia, idtipoper ) T ";
        //    query += "INNER JOIN cencosusr B (NOLOCK) ON B.idcia = T.idcia ";
        //    query += "INNER JOIN plantiphorario C (NOLOCK) ON C.nominas LIKE '%' + T.idtipoper + '%' ";
        //    query += "WHERE B.idusr = '" + @usuario + "' ";
        //    query += "ORDER BY C.descripcion ";
        //    funciones.listaComboBox(query, cmbTipoHorario, "");

        //    Load_Datos(string.Empty);
        //}

        private void Load_Datos(string buscar)
        {
            string query = @"SELECT a.idperplan AS Codigo,a.destrabajador AS Trabajador,
                        a.destipohorario AS Horario,CONVERT(VARCHAR(10),a.fechadiaria,120) AS Fecha, ISNULL(c.mensaje,''), 
                        CONVERT(VARCHAR(20), a.fechaini, 120), CONVERT(VARCHAR(20), a.fechafin, 120) 
                        FROM progdet a (NOLOCK) INNER JOIN cencosusr b (NOLOCK) ON b.idcencos = a.seccion 
                        AND b.idgerencia = a.gerencia AND b.idcia = a.idcia AND b.idusr = '" + this._usuario + @"' 
                        AND a.destrabajador LIKE '%" + @buscar + @"%' 
                        AND a.fechadiaria >= CONVERT(VARCHAR(10),GETDATE(),120) 
                        LEFT JOIN progdet_msj c (NOLOCK) ON c.idprog = a.idprog AND c.idperplan=a.idperplan AND c.fecha=a.fechadiaria 
                        ORDER BY a.fechadiaria";

            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();

            try
            {
                using (SqlDataAdapter myDataAdapter = new SqlDataAdapter(query, conexion))
                {
                    DataSet myDataSet = new DataSet();
                    myDataAdapter.Fill(myDataSet, "tabla");
                    Funciones funciones = new Funciones();
                    funciones.formatearDataGridView(dgvdetalle);

                    dgvdetalle.DataSource = myDataSet.Tables["tabla"];

                    dgvdetalle.Columns[0].Width = 80;
                    dgvdetalle.Columns[1].Width = 250;
                    dgvdetalle.Columns[2].Width = 250;
                    dgvdetalle.Columns[3].Width = 80;
                    dgvdetalle.Columns[4].Visible = false;
                    dgvdetalle.Columns[5].Visible = false;
                    dgvdetalle.Columns[6].Visible = false;

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

        #region Eventos KeyPress

        #endregion

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            try
            {
                string idperplan = txtCodigoInterno.Text.Trim();
                string fecha = lblFecha.Text;
                string horario = funciones.getValorComboBox(cmbTipoHorario, 3);
                string motivo = txtMotivo.Text.Trim();
                string horaini = dtHoraIni.Value.ToString("HH:mm");
                string horafin = dtHoraFin.Value.ToString("HH:mm");

                string valorValida = "G";

                if (horario.Trim() == string.Empty && valorValida == "G")
                {
                    valorValida = "B";
                    MessageBox.Show("No ha especificado un horario", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    cmbTipoHorario.Focus();
                }

                if (motivo == string.Empty && valorValida == "G")
                {
                    valorValida = "B";
                    MessageBox.Show("Debe especificar un motivo para actualizar registro", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    cmbTipoHorario.Focus();
                }

                if (valorValida.Equals("G"))
                {
                    UpdateProgramacionHorario(idperplan, horario, fecha, motivo, horaini, horafin);

                    Load_Datos(string.Empty);

                    txtBuscar.Clear();
                    txtMotivo.Clear();
                    dtHoraIni.Enabled = false;
                    dtHoraFin.Enabled = false;
                    btnAgregar.Enabled = false;
                }
            }
            catch (FormatException ex)
            {
                MessageBox.Show(ex.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void UpdateProgramacionHorario(string idperplan, string horario, string fecha, string motivo, string horaini, string horafin)
        {
            string query = "EXECUTE SP_UPDATE_PROGDET '" + @idperplan + "','" + @horario + "','" + @fecha + "','" + @motivo + "','" + @horaini + "','" + @horafin + "' ";

            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();

            try
            {
                SqlCommand myCommand = new SqlCommand(query, conexion);
                myCommand.ExecuteNonQuery();
                myCommand.Dispose();

                MessageBox.Show("Actualización exitosa..!!", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LimpiarFiltros();
            }
            catch (SqlException e)
            {
                MessageBox.Show(e.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }

            conexion.Close();
        }

        private void LimpiarFiltros()
        {
            lblTrabajador.Clear();
            lblFecha.Clear();
            cmbTipoHorario.Enabled = false;
            btnAgregar.Enabled = false;
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            Int32 selectedCellCount = dgvdetalle.GetCellCount(DataGridViewElementStates.Selected);
            if (selectedCellCount > 0)
            {
                txtCodigoInterno.Text = dgvdetalle.Rows[dgvdetalle.SelectedCells[1].RowIndex].Cells[0].Value.ToString();
                lblTrabajador.Text = dgvdetalle.Rows[dgvdetalle.SelectedCells[1].RowIndex].Cells[1].Value.ToString();
                cmbTipoHorario.Text = dgvdetalle.Rows[dgvdetalle.SelectedCells[1].RowIndex].Cells[2].Value.ToString();
                lblFecha.Text = dgvdetalle.Rows[dgvdetalle.SelectedCells[1].RowIndex].Cells[3].Value.ToString();
                txtMotivo.Text = dgvdetalle.Rows[dgvdetalle.SelectedCells[1].RowIndex].Cells[4].Value.ToString();
                dtHoraIni.Value = DateTime.Parse(dgvdetalle.Rows[dgvdetalle.SelectedCells[1].RowIndex].Cells[5].Value.ToString());
                dtHoraFin.Value = DateTime.Parse(dgvdetalle.Rows[dgvdetalle.SelectedCells[1].RowIndex].Cells[6].Value.ToString());

                cmbTipoHorario.Enabled = true;
                grFechas.Enabled = true;
                btnAgregar.Enabled = true;
                dtHoraIni.Enabled = true;
                dtHoraFin.Enabled = true;
            }
        }

        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            string buscar = txtBuscar.Text.Trim();
            Load_Datos(buscar);
        }

        private void ui_programacionInd_Load(object sender, EventArgs e)
        {
            this._usuario = variables.getValorUsr();
            this.Text = "Re-Programación de Horarios";

            string query = @"SELECT DISTINCT C.idplantiphorario as clave, D.descripcion FROM ( 
                            SELECT idcia, idtipoper FROM perplan (NOLOCK) 
                            GROUP BY idcia, idtipoper ) T 
                            INNER JOIN cencosusr B (NOLOCK) ON B.idcia = T.idcia 
                            INNER JOIN plantiphorariodet C (NOLOCK) ON C.nominas LIKE '%' + T.idtipoper + '%' 
                            INNER JOIN plantiphorario D (NOLOCK) ON D.idplantiphorario=C.idplantiphorario 
                            WHERE B.idusr = '" + this._usuario + @"' 
                            ORDER BY D.descripcion ";
            funciones.listaComboBox(query, cmbTipoHorario, "");

            Load_Datos(string.Empty);
        }
    }
}