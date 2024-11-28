using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CaniaBrava
{
    public partial class ui_reprogramacionInicialAgri : Form
    {
        Funciones fn = new Funciones();

        private string Nromov { get; set; }
        private string Supervisor { get; set; }
        private string Fecha { get; set; }
        private string Fundo { get; set; }
        private string Descapataz { get; set; }
        private string Capataz { get; set; }
        private string Equipo { get; set; }
        private string Turno { get; set; }
        private string Desactividad { get; set; }
        private string Actividad { get; set; }
        private string Estado { get; set; }
        private string Jornal { get; set; }
        private string Area { get; set; }
        private string Comen { get; set; }
        private TextBox TextBoxActivo;
        public TextBox _TextBoxActivo
        {
            get { return TextBoxActivo; }
            set { TextBoxActivo = value; }
        }

        private Form FormPadre;
        public Form _FormPadre
        {
            get { return FormPadre; }
            set { FormPadre = value; }
        }

        public ui_reprogramacionInicialAgri()
        {
            InitializeComponent();

            if (fn.VersionAssembly()) Application.ExitThread();
        }

        public void _Load(string nromov, DateTime fec1, DateTime fec2)
        {
            this.Nromov = nromov;
            dtpFecha.MinDate = fec1;
            dtpFecha.MaxDate = fec2;
            dtpFechanew.MinDate = fec1;
            if (fec1.Date < DateTime.Now.Date) { dtpFechanew.MinDate = DateTime.Now/*.AddDays(1)*/.Date; }
            dtpFechanew.MaxDate = fec2;
            txtProg.Text = "Programación " + fec1.ToString("yyyy-MM-dd") + " al " + fec2.ToString("yyyy-MM-dd");

            Editar();
        }

        public void Editar()
        {
            this.Fecha = dtpFecha.Value.ToString("yyyy-MM-dd");
            string query = @"SELECT CONVERT(VARCHAR(10),a.Fecha,120) [Fecha],b.desusr [Tareador],a.Fundo,a.Equipo,a.Turno,
            c.desmaesgen [Actividad],CASE WHEN a.cantidadreal=-1 THEN a.cantidadprog ELSE a.cantidadreal END [#Trab.],
            a.actividad,a.capataz,a.chkVB,d.idusrins,ISNULL(a.area,0),ISNULL(a.comen,'') 
            FROM progagri_fecafueqtuac (NOLOCK) a 
            INNER JOIN usrfile (NOLOCK) b ON b.idusr=a.capataz 
            INNER JOIN maesgen (NOLOCK) c ON c.idmaesgen='162' AND c.clavemaesgen=a.actividad 
            INNER JOIN progagri (NOLOCK) d ON d.idprog=a.idprog 
            WHERE a.idprog='" + this.Nromov + "' AND a.fecha = '" + this.Fecha + "' ORDER BY a.Fecha";

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
                    funciones.formatearDataGridView(dgdetalle);

                    dgdetalle.DataSource = myDataSet.Tables["tabla"];

                    dgdetalle.Columns[0].Width = 58;
                    dgdetalle.Columns[1].Width = 140;
                    dgdetalle.Columns[2].Width = 44;
                    dgdetalle.Columns[3].Width = 44;
                    dgdetalle.Columns[4].Width = 44;
                    dgdetalle.Columns[5].Width = 200;
                    dgdetalle.Columns[6].Width = 50;
                    dgdetalle.Columns[7].Visible = false;
                    dgdetalle.Columns[8].Visible = false;
                    dgdetalle.Columns[9].Visible = false;
                    dgdetalle.Columns[10].Visible = false;

                    dgdetalle.AllowUserToResizeRows = false;
                    dgdetalle.AllowUserToResizeColumns = false;
                    foreach (DataGridViewColumn column in dgdetalle.Columns)
                    {
                        column.SortMode = DataGridViewColumnSortMode.NotSortable;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            finally
            {
                conexion.Close();
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            GlobalVariables variables = new GlobalVariables();
            MaesGen maes = new MaesGen();
            string fecha = dtpFechanew.Value.ToString("yyyy-MM-dd");
            string chkFec = (chkFecha.Checked ? "1" : "0");
            string estado = (fn.getValorComboBox(cmbEstado, 8).Trim() == "ACTIVO" ? "1" : "0");
            string jornales = txtjornales.Text.Trim();
            string area = txtarea.Text.Trim();
            string comen = txtcomen.Text.Trim();

            if (jornales == string.Empty)
            {
                MessageBox.Show("Debe ingresar nro de jornales", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                txtjornales.Clear(); txtjornales.Focus();
                return;
            }
            ValidarActividad();
            string acti = txtact.Text.Trim();
            if (acti == string.Empty)
            {
                MessageBox.Show("Debe ingresar una Actividad", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                txtact.Clear(); txtact.Focus();
                return;
            }

            ValidarEquipo();
            string equi = txteq.Text.Trim();
            if (equi == string.Empty)
            {
                MessageBox.Show("Debe ingresar un Equipo", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                txteq.Clear(); txteq.Focus();
                return;
            }

            ValidarTurno();
            string turn = txttu.Text.Trim();
            if (turn == string.Empty)
            {
                MessageBox.Show("Debe ingresar un turno", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                txttu.Clear(); txttu.Focus();
                return;
            }

            if (this.Fecha != fecha || this.Actividad != acti || this.Equipo != equi || this.Turno != turn || this.Jornal != jornales || this.Estado != estado || this.Area != area || this.Comen != comen)
            {
                string query = string.Empty;
                query = "EXECUTE SP_UPDATE_REPROGAGRI '" + this.Nromov + "','" +
                    this.Fecha + "','" + this.Capataz + "','" + this.Fundo + "','" + this.Equipo + "','" + this.Turno + "','" + this.Actividad + "','" + this.Jornal + "','" +
                    fecha + "','" + equi + "','" + turn + "','" + acti + "','" + jornales + "','" + estado + "','" + chkFec + "','" + variables.getValorUsr() + @"'
                    ,'" + this.Area + "','" + this.Comen + "'";

                SqlConnection conexion = new SqlConnection();
                conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
                conexion.Open();

                try
                {
                    SqlCommand myCommand = new SqlCommand(query, conexion);
                    myCommand.ExecuteNonQuery();
                    myCommand.Dispose();
                    MessageBox.Show("Registro actualizado..", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    Editar();
                }
                catch (SqlException ex)
                {
                    MessageBox.Show(ex.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
                conexion.Close();
            }
        }

        private bool ValidaDatos()
        {
            string idperplan = txtProg.Text.Trim();
            string sede = fn.getValorComboBox(cmbEq, 7);

            if (idperplan == string.Empty)
            {
                MessageBox.Show("Debe especificar un Trabajador.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtProg.Focus();
                return false;
            }

            if (sede.Trim() == string.Empty)
            {
                MessageBox.Show("Debe especificar una Sede.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cmbEq.Focus();
                return false;
            }

            return true;
        }

        private void dgdetalle_SelectionChanged(object sender, EventArgs e)
        {
            var rowsCount = dgdetalle.SelectedRows.Count;
            if (rowsCount == 0 || rowsCount > 1) return;

            var row = dgdetalle.SelectedRows[0];
            if (row == null) return;

            this.Descapataz = row.Cells[1].Value.ToString();
            this.Fundo = row.Cells[2].Value.ToString();
            this.Equipo = row.Cells[3].Value.ToString();
            this.Turno = row.Cells[4].Value.ToString();
            this.Desactividad = row.Cells[5].Value.ToString();
            this.Jornal = row.Cells[6].Value.ToString();
            this.Actividad = row.Cells[7].Value.ToString();
            this.Capataz = row.Cells[8].Value.ToString();
            this.Estado = row.Cells[9].Value.ToString();
            this.Supervisor = row.Cells[10].Value.ToString();
            this.Area = row.Cells[11].Value.ToString();
            this.Comen = row.Cells[12].Value.ToString();

            Get_Informacion();
        }

        private void Get_Informacion()
        {
            txtjornales.ReadOnly = true;
            txtact.ReadOnly = true;
            txteq.ReadOnly = true;
            txttu.ReadOnly = true;
            txtarea.ReadOnly = true;
            txtcomen.ReadOnly = true;
            cmbEstado.Enabled = false;
            btnactiv.Enabled = false;
            btnEqui.Enabled = false;
            btnTur.Enabled = false;
            if (DateTime.Parse(this.Fecha).Date >= DateTime.Now.Date)
            {
                string query = string.Empty;

                txtjornales.ReadOnly = false;
                txtact.ReadOnly = false;
                txteq.ReadOnly = false;
                txttu.ReadOnly = false;
                txtarea.ReadOnly = false;
                txtcomen.ReadOnly = false;
                cmbEstado.Enabled = true;
                btnactiv.Enabled = true;
                btnEqui.Enabled = true;
                btnTur.Enabled = true;

                chkFecha.Checked = false;
                dtpFechanew.Value = DateTime.Parse(this.Fecha);
                txtfundo.Text = this.Fundo;
                txtcapataz.Text = this.Descapataz;

                query = @"SELECT equipo [descripcion] FROM progagri_fecafueq (NOLOCK) 
                    WHERE idprog='" + this.Nromov + @"' AND fecha='" + this.Fecha + @"' 
                    AND capataz='" + this.Capataz + @"' AND fundo='" + this.Fundo + @"' ";
                fn.listaComboBoxUnCampo(query, cmbEq, string.Empty);
                cmbEq.Text = this.Equipo;
                cmbTu.Text = this.Turno;
                txtact.Text = this.Actividad;
                txtdesact.Text = this.Desactividad;
                txteq.Text = this.Equipo;
                txttu.Text = this.Turno;
                cmbEstado.Text = (this.Estado == "1" ? "ACTIVO" : "INACTIVO");
                txtjornales.Text = this.Jornal;
                txtarea.Text = this.Area;

                var jor = decimal.Parse(this.Jornal);
                var are = decimal.Parse(this.Area);
                if (jor > 0 && are > 0)
                {
                    txtratio.Text = (jor / are).ToString();
                }

                txtcomen.Text = this.Comen;
            }
        }

        private void dtpFecha_ValueChanged(object sender, EventArgs e)
        {
            Editar();
        }

        private void cmbEq_SelectedIndexChanged(object sender, EventArgs e)
        {
            string eq = fn.getValorComboBox(cmbEq, 4);
            string query = @"SELECT turno [descripcion] FROM progagri_fecafueqtu (NOLOCK) 
                    WHERE idprog='" + this.Nromov + @"' AND fecha='" + this.Fecha + @"' 
                    AND capataz='" + this.Capataz + @"' AND fundo='" + this.Fundo + @"' AND equipo='" + eq + @"' ";
            fn.listaComboBoxUnCampo(query, cmbTu, string.Empty);
        }

        private void cmbTu_SelectedIndexChanged(object sender, EventArgs e)
        {
            //string eq = fn.getValorComboBox(cmbEq, 4);
            //string tu = fn.getValorComboBox(cmbTu, 4);
            //string query = @"SELECT a.actividad [clave],c.desmaesgen [descripcion] FROM progagri_fecafueqtuac a (NOLOCK) 
            //        INNER JOIN maesgen (NOLOCK) c ON c.idmaesgen='162' AND c.clavemaesgen=a.actividad 
            //        WHERE idprog='" + this.Nromov + @"' AND fecha='" + this.Fecha + @"' 
            //        AND capataz='" + this.Capataz + @"' AND fundo='" + this.Fundo + @"' AND equipo='" + eq + @"' AND turno='" + tu + @"' ";

            //fn.listaComboBox(query, cmbAc, string.Empty);
        }

        private void cmbAc_SelectedIndexChanged(object sender, EventArgs e)
        {
            //string eq = fn.getValorComboBox(cmbEq, 4);
            //string tu = fn.getValorComboBox(cmbTu, 4);
            //string ac = fn.getValorComboBox(cmbAc, 4);
            //string query = @"SELECT CASE chkVB WHEN 1 THEN 'ACTIVO' ELSE 'INACTIVO' END [descripcion] 
            //        FROM progagri_fecafueqtuac (NOLOCK) 
            //        WHERE idprog='" + this.Nromov + @"' AND fecha='" + this.Fecha + @"' 
            //        AND capataz='" + this.Capataz + @"' AND fundo='" + this.Fundo + @"' 
            //        AND equipo='" + eq + @"' AND turno='" + tu + @"' AND actividad = '" + ac + @"' ";

            //fn.listaComboBoxUnCampo(query, cmbEstado, "B");
        }

        private void btnactiv_Click(object sender, EventArgs e)
        {
            ValidarActividad();
        }

        private void ValidarActividad()
        {
            if (txtact.Text != string.Empty)
            {
                MaesGen maes = new MaesGen();
                string descrip = maes.ui_getDatos("162", txtact.Text.Trim(), "DESCRIPCION");
                if (descrip != string.Empty)
                {
                    txtdesact.Text = maes.ui_getDatos("162", txtact.Text.Trim(), "DESCRIPCION");
                }
                else
                {
                    txtact.Clear(); txtdesact.Clear(); txtact.Focus();
                    MessageBox.Show("Actividad no registrada", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
            }
            else
            {
                ui_viewactividades ui_detalle = new ui_viewactividades();
                ui_detalle._FormPadre = this;
                ui_detalle.Load_Datos(this.Capataz, this.Fundo);
                ui_detalle.Activate();
                ui_detalle.BringToFront();
                ui_detalle.ShowDialog();
                ui_detalle.Dispose();
            }
        }

        private void btnEqui_Click(object sender, EventArgs e)
        {
            ValidarEquipo();
        }

        private void ValidarEquipo()
        {
            if (txteq.Text != string.Empty)
            {
                MaesGen maes = new MaesGen();
                string descrip = maes.ui_getDatos("163", string.Empty, this.Supervisor, this.Capataz, this.Fundo, txteq.Text, string.Empty, "PARM2");
                if (descrip == string.Empty)
                {
                    txteq.Clear(); txteq.Focus();
                    MessageBox.Show("Turno no registrado", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
            }
            else
            {
                ui_viewequipos ui_detalle = new ui_viewequipos();
                ui_detalle._FormPadre = this;
                ui_detalle.Load_Datos(string.Empty, string.Empty, this.Capataz, this.Fundo, 0);
                ui_detalle.Activate();
                ui_detalle.BringToFront();
                ui_detalle.ShowDialog();
                ui_detalle.Dispose();
            }
        }

        private void btnTur_Click(object sender, EventArgs e)
        {
            ValidarTurno();
        }

        private void ValidarTurno()
        {
            if (txttu.Text != string.Empty)
            {
                MaesGen maes = new MaesGen();
                string descrip = maes.ui_getDatos("163", string.Empty, this.Supervisor, this.Capataz, this.Fundo, this.Equipo, txttu.Text, "PARM3");
                if (descrip == string.Empty)
                {
                    txttu.Clear(); txttu.Focus();
                    MessageBox.Show("Turno no registrado", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
            }
            else
            {
                ui_viewturnos ui_detalle = new ui_viewturnos();
                ui_detalle._FormPadre = this;
                ui_detalle.Load_Datos(string.Empty, string.Empty, this.Capataz, this.Fundo, this.Equipo, 0);
                ui_detalle.Activate();
                ui_detalle.BringToFront();
                ui_detalle.ShowDialog();
                ui_detalle.Dispose();
            }
        }

        private void chkFecha_CheckedChanged(object sender, EventArgs e)
        {
            txtjornales.ReadOnly = true;
            txtact.ReadOnly = true;
            txteq.ReadOnly = true;
            txttu.ReadOnly = true;
            cmbEstado.Enabled = false;
            btnactiv.Enabled = false;
            btnEqui.Enabled = false;
            btnTur.Enabled = false;
            if (!chkFecha.Checked)
            {
                txtjornales.ReadOnly = false;
                txtact.ReadOnly = false;
                txteq.ReadOnly = false;
                txttu.ReadOnly = false;
                cmbEstado.Enabled = true;
                btnactiv.Enabled = true;
                btnEqui.Enabled = true;
                btnTur.Enabled = true;
            }
        }

        private void txtact_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsNumber(e.KeyChar)) && (e.KeyChar != (char)Keys.Back))
            {
                //MessageBox.Show("Solo se permiten numeros", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                e.Handled = true;
                return;
            }
        }

        private void txtjornales_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }

            if (e.KeyChar == '\r')
            {
                CalcularRatio();
                txtarea.Focus();
                e.Handled = true;
            }
        }

        private void txtarea_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }

            if (e.KeyChar == '\r')
            {
                CalcularRatio();
                txtcomen.Focus();
                e.Handled = true;
            }
        }

        private void txtjornales_KeyDown(object sender, KeyEventArgs e)
        {
            CalcularRatio();
        }

        private void txtarea_KeyDown(object sender, KeyEventArgs e)
        {
            CalcularRatio();
        }

        private void txtarea_KeyUp(object sender, KeyEventArgs e)
        {
            CalcularRatio();
        }

        private void txtjornales_KeyUp(object sender, KeyEventArgs e)
        {
            CalcularRatio();
        }

        private void CalcularRatio()
        {
            var jor = decimal.Parse(txtjornales.Text);
            var are = decimal.Parse(txtarea.Text);
            if (jor > 0 && are > 0)
            {
                txtratio.Text = (jor / are).ToString();
            }
        }
    }
}