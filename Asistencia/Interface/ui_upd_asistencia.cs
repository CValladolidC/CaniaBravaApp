using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CaniaBrava
{
    public partial class ui_upd_asistencia : Form
    {
        private DateTime? _RegEntrada { get; set; }
        private DateTime? _RegSalida { get; set; }
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

        public ui_upd_asistencia()
        {
            InitializeComponent();

            Funciones funciones = new Funciones();
            string query = "SELECT idusr [clave],desusr [descripcion] FROM usrfile (NOLOCK) WHERE typeusr='05' AND desusr NOT LIKE '%COMEDOR%'";
            funciones.listaComboBox(query, cmbSedes, "B");
            btnEliminar.Visible = false;
        }

        public void Editar(string nromov, string idperplan, string fecha)
        {
            txtCodigoInterno.Enabled = false;
            cmbSedes.Enabled = false;

            PerPlan perplan = new PerPlan();
            txtCodigoInterno.Text = perplan.ui_getDatosPerPlan(idperplan, "0");
            txtDocIdent.Text = perplan.ui_getDatosPerPlan(idperplan, "1");
            txtNroDocIden.Text = perplan.ui_getDatosPerPlan(idperplan, "2");
            txtNombres.Text = perplan.ui_getDatosPerPlan(idperplan, "3");

            string query = "SELECT a.nromov,a.fecha,LEFT(CONVERT(TIME,a.regEntrada,120),5),LEFT(CONVERT(TIME,a.regSalida,120),5),";
            query += "b.desusr,CONVERT(VARCHAR(16),a.regEntrada,120),CONVERT(VARCHAR(16),a.regSalida,120),a.idlogin+'  '+b.desusr ";
            query += "FROM control a (NOLOCK) INNER JOIN usrfile b (NOLOCK) ON b.idusr=a.idlogin ";
            query += "WHERE a.fecha = '" + fecha + "' AND a.idperplan = '" + idperplan + "' ORDER BY a.regEntrada,a.hora";
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
                    dgdetalle.Columns[0].HeaderText = "Movimiento";
                    dgdetalle.Columns[1].HeaderText = "Fecha";
                    dgdetalle.Columns[2].HeaderText = "H. Ingreso";
                    dgdetalle.Columns[3].HeaderText = "H. Salida";
                    dgdetalle.Columns[4].HeaderText = "Sede";

                    dgdetalle.Columns[0].Width = 80;
                    dgdetalle.Columns[1].Width = 70;
                    dgdetalle.Columns[2].Width = 65;
                    dgdetalle.Columns[3].Width = 65;
                    dgdetalle.Columns[4].Width = 100;

                    dgdetalle.Columns[5].Visible = false;
                    dgdetalle.Columns[6].Visible = false;
                    dgdetalle.Columns[7].Visible = false;

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

            if (dgdetalle.Rows.Count > 1)
            {
                btnEliminar.Visible = true;
            }
        }

        private void txtCodigoInterno_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                GlobalVariables gv = new GlobalVariables();
                Funciones fn = new Funciones();
                this._TextBoxActivo = txtCodigoInterno;
                string cadenaBusqueda = string.Empty;
                string condicionAdicional = string.Empty;

                FiltrosMaestros filtros = new FiltrosMaestros();
                filtros.filtrarPerPlan2("ui_upd_asistencia", this, txtCodigoInterno, null, cadenaBusqueda, condicionAdicional);
            }
        }

        private void txtCodigoInterno_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                if (txtCodigoInterno.Text.Trim() != string.Empty)
                {
                    GlobalVariables gv = new GlobalVariables();
                    PerPlan perplan = new PerPlan();
                    string idperplan = txtCodigoInterno.Text.Trim();
                    string codigoInterno = perplan.ui_getDatosPerPlan(idperplan, "0");

                    if (codigoInterno == string.Empty)
                    {
                        MessageBox.Show("Código no registrado del trabajador", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txtCodigoInterno.Clear();
                        txtDocIdent.Clear();
                        txtNroDocIden.Clear();
                        txtNombres.Clear();
                        e.Handled = true;
                        txtCodigoInterno.Focus();
                    }
                    else
                    {
                        txtCodigoInterno.Text = perplan.ui_getDatosPerPlan(idperplan, "0");
                        txtDocIdent.Text = perplan.ui_getDatosPerPlan(idperplan, "1");
                        txtNroDocIden.Text = perplan.ui_getDatosPerPlan(idperplan, "2");
                        txtNombres.Text = perplan.ui_getDatosPerPlan(idperplan, "3");
                        e.Handled = true;
                    }
                }
                else
                {
                    txtCodigoInterno.Clear();
                    txtDocIdent.Clear();
                    txtNroDocIden.Clear();
                    txtNombres.Clear();
                    e.Handled = true;
                    txtCodigoInterno.Focus();
                }
            }
        }

        private void FillFechafin()
        {
            dtpfecfin.Value = dtpfecini.Value;
            if (chkNoche.Checked)
            {
                dtpfecfin.Value = dtpfecini.Value.AddDays(1);
            }
        }

        private void chkNoche_CheckedChanged(object sender, EventArgs e)
        {
            FillFechafin();
        }

        private void dtpfecini_ValueChanged(object sender, EventArgs e)
        {
            FillFechafin();
        }

        private void dtpfecfin_ValueChanged(object sender, EventArgs e)
        {
            if (chkNoche.Checked)
            {
                if (txtnromov.Text == string.Empty && dtpfecini.Value.AddDays(1) != dtpfecfin.Value)
                {
                    MessageBox.Show("La fecha de salida debe ser: " + dtpfecini.Value.AddDays(1).ToShortDateString(),
                        "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    dtpfecfin.Value = dtpfecini.Value.AddDays(1);
                    return;
                }
            }
            else
            {
                if (txtnromov.Text == string.Empty && dtpfecini.Value != dtpfecfin.Value)
                {
                    MessageBox.Show("La fecha de salida debe ser: " + dtpfecini.Value.ToShortDateString(),
                        "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    dtpfecfin.Value = dtpfecini.Value;
                    return;
                }
            }
        }

        private void chkfecini_CheckedChanged(object sender, EventArgs e)
        {
            dtpfecini.Enabled = false;
            dtphoraini.Enabled = false;
            if (chkfecini.Checked)
            {
                if (txtnromov.Text.Trim() == string.Empty)
                {
                    dtpfecini.Enabled = true;
                }
                dtphoraini.Enabled = true;
            }
        }

        private void chkfecfin_CheckedChanged(object sender, EventArgs e)
        {
            dtpfecfin.Enabled = false;
            dtphorafin.Enabled = false;
            if (chkfecfin.Checked)
            {
                dtpfecfin.Enabled = true;
                dtphorafin.Enabled = true;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            GlobalVariables variable = new GlobalVariables();
            Funciones fn = new Funciones();
            string idperplan = txtCodigoInterno.Text.Trim();
            string dni = txtNroDocIden.Text.Trim();
            string sede = fn.getValorComboBox(cmbSedes, 7);
            DateTime regentrada = DateTime.Parse(dtpfecini.Value.ToString("yyyy-MM-dd") + " " + dtphoraini.Value.ToString("HH:mm:ss"));
            DateTime regsalida = DateTime.Parse(dtpfecfin.Value.ToString("yyyy-MM-dd") + " " + dtphorafin.Value.ToString("HH:mm:ss"));
            bool _chkfecini = chkfecini.Checked;
            bool _chkfecfin = chkfecfin.Checked;
            
            if (ValidaDatos())
            {
                string query = string.Empty;
                if (txtnromov.Text.Trim() == string.Empty)
                {
                    query = " INSERT INTO control VALUES ((SELECT ISNULL(MAX(nromov),0)+1 FROM control (NOLOCK)),'" + idperplan + "','I',";
                    query += "'" + regentrada.ToString("yyyy-MM-dd") + "','" + regentrada.ToString("HH:mm") + "','" + regentrada.ToString("yyyy-MM-dd HH:mm") + "','V',";
                    query += "'" + dni + "'," + (_chkfecfin ? "'" + regsalida.ToString("yyyy-MM-dd HH:mm") + "'" : "null") + ",'','" + sede + "','S'); ";
                    query += "INSERT INTO controlhisto VALUES ((SELECT ISNULL(MAX(id),0)+1 FROM controlhisto (NOLOCK))," +
                        "(SELECT MAX(nromov) FROM control (NOLOCK)),'" + idperplan + "','" + regentrada.ToString("yyyy-MM-dd") + "','" +
                        regentrada.ToString("yyyy-MM-dd HH:mm") + "'," +
                        (_chkfecfin ? "'" + regsalida.ToString("yyyy-MM-dd HH:mm") + "'" : "null") + ",'NUEVO REGISTRO','" + variable.getValorUsr() + "');";
                }
                else
                {
                    if (txtnromov.Text.Trim() != "0")
                    {
                        query = " UPDATE control SET hora='" + regentrada.ToString("HH:mm") + "',regEntrada='" + regentrada.ToString("yyyy-MM-dd HH:mm") + "',";
                        query += "regSalida=" + (_chkfecfin ? "'" + regsalida.ToString("yyyy-MM-dd HH:mm") + "'" : "null") + ", ";
                        query += "teclado=" + (_chkfecfin ? "SUBSTRING(teclado,0,2)+'/X'" : "SUBSTRING(teclado,0,2)") + ", ";
                        query += "mensaje=" + (_chkfecfin ? "SUBSTRING(mensaje, 0, CHARINDEX('/',mensaje))+'/Marcacion de Salida'" : "SUBSTRING(mensaje, 0, CHARINDEX('/',mensaje))") + " ";
                        query += "WHERE nromov = '" + txtnromov.Text.Trim() + "';";
                        query += "INSERT INTO controlhisto VALUES ((SELECT ISNULL(MAX(id),0)+1 FROM controlhisto (NOLOCK)),'" + txtnromov.Text.Trim() + "','" +
                            idperplan + "','" + (this._RegEntrada != null ? this._RegEntrada.Value.ToString("yyyy-MM-dd") : this._RegSalida.Value.ToString("yyyy-MM-dd")) + "'," +
                            (this._RegEntrada != null ? "'" + this._RegEntrada.Value.ToString("yyyy-MM-dd HH:mm") + "'" : "null") + "," +
                            (this._RegSalida != null ? "'" + this._RegSalida.Value.ToString("yyyy-MM-dd HH:mm") + "'" : "null") + ",'REGISTRO MODIFICADO','" + variable.getValorUsr() + "');";
                    }
                }

                SqlConnection conexion = new SqlConnection();
                conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
                conexion.Open();

                try
                {
                    SqlCommand myCommand = new SqlCommand(query, conexion);
                    myCommand.ExecuteNonQuery();
                    myCommand.Dispose();
                    MessageBox.Show("Informacion exitosa..", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    ((ui_rep_asistencia)FormPadre).btnGenerar.PerformClick();
                    if (dgdetalle.Rows.Count <= 1)
                    {
                        this.Close();
                    }
                    else { Editar(txtnromov.Text.Trim(), idperplan, regentrada.ToString("yyyy-MM-dd")); }
                }
                catch (SqlException ex)
                {
                    MessageBox.Show(ex.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
                conexion.Close();
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            GlobalVariables variable = new GlobalVariables();
            string idperplan = txtCodigoInterno.Text.Trim();

            if (ValidaDatos())
            {
                string query = "DELETE FROM control WHERE nromov = '" + txtnromov.Text.Trim() + "';";
                query += "INSERT INTO controlhisto VALUES ((SELECT ISNULL(MAX(id),0)+1 FROM controlhisto (NOLOCK)),'" + txtnromov.Text.Trim() + "','" +
                    idperplan + "','" + this._RegEntrada.Value.ToString("yyyy-MM-dd") + "','" +
                     this._RegEntrada.Value.ToString("yyyy-MM-dd HH:mm") + "'," +
                    (this._RegSalida.Value != null ? "'" + this._RegSalida.Value.ToString("yyyy-MM-dd HH:mm") + "'" : "null") + ",'REGISTRO ELIMINADO','" + variable.getValorUsr() + "');";

                SqlConnection conexion = new SqlConnection();
                conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
                conexion.Open();

                try
                {
                    SqlCommand myCommand = new SqlCommand(query, conexion);
                    myCommand.ExecuteNonQuery();
                    myCommand.Dispose();
                    MessageBox.Show("Registro Eliminado..", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    ((ui_rep_asistencia)FormPadre).btnGenerar.PerformClick();
                    if (dgdetalle.Rows.Count <= 1)
                    {
                        this.Close();
                    }
                    else { Editar(txtnromov.Text.Trim(), idperplan, this._RegEntrada.Value.ToString("yyyy-MM-dd")); }
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
            Funciones fn = new Funciones();
            string idperplan = txtCodigoInterno.Text.Trim();
            string dni = txtNroDocIden.Text.Trim();
            string sede = fn.getValorComboBox(cmbSedes, 7);
            DateTime regentrada = DateTime.Parse(dtpfecini.Value.ToString("yyyy-MM-dd") + " " + dtphoraini.Value.ToString("HH:mm:ss"));
            DateTime regsalida = DateTime.Parse(dtpfecfin.Value.ToString("yyyy-MM-dd") + " " + dtphorafin.Value.ToString("HH:mm:ss"));
            bool _chkfecini = chkfecini.Checked;
            bool _chkfecfin = chkfecfin.Checked;

            if (idperplan == string.Empty)
            {
                MessageBox.Show("Debe especificar un Trabajador.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtCodigoInterno.Focus();
                return false;
            }

            if (sede.Trim() == string.Empty)
            {
                MessageBox.Show("Debe especificar una Sede.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cmbSedes.Focus();
                return false;
            }

            if (!_chkfecini || !_chkfecfin)
            {
                if (!_chkfecini)
                {
                    MessageBox.Show("Debe especificar fecha y hora de ingreso.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return false;
                }
            }

            return true;
        }

        private void dgdetalle_SelectionChanged(object sender, EventArgs e)
        {
            //_continua = false;
            var rowsCount = dgdetalle.SelectedRows.Count;
            if (rowsCount == 0 || rowsCount > 1) return;

            var row = dgdetalle.SelectedRows[0];
            if (row == null) return;

            DateTime? regentrada = null, regsalida = null;
            string nromov = row.Cells[0].Value.ToString();
            if (row.Cells[5].Value.ToString() != "")
            {
                regentrada = DateTime.Parse(row.Cells[5].Value.ToString());
            }
            if (row.Cells[6].Value.ToString() != "")
            {
                regsalida = DateTime.Parse(row.Cells[6].Value.ToString());
            }
            string sede = row.Cells[7].Value.ToString();
            Get_Informacion(nromov, regentrada, regsalida, sede);
        }

        private void Get_Informacion(string nromov, DateTime? RegEntrada, DateTime? RegSalida, string sede)
        {
            txtnromov.Text = nromov;
            cmbSedes.Text = sede;
            chkNoche.Checked = false;
            if (RegEntrada != null && RegSalida != null)
            {
                if (RegEntrada.Value.Date != RegSalida.Value.Date) { chkNoche.Checked = true; }
            }
            if (RegEntrada != null)
            {
                chkfecini.Checked = true;
                //dtpfecini.Enabled = true;
                dtphoraini.Enabled = true;
                dtpfecini.Value = RegEntrada.Value;
                dtphoraini.Value = RegEntrada.Value;
                this._RegEntrada = RegEntrada;
                dtphorafin.Value = DateTime.Parse(RegEntrada.Value.ToString("yyyy-MM-dd"));
            }
            chkfecfin.Checked = false;
            dtpfecfin.Enabled = false;
            dtphorafin.Enabled = false;
            if (RegSalida != null)
            {
                if (RegEntrada == null)
                {
                    dtpfecini.Value = RegSalida.Value;
                    //dtphoraini.Value = RegSalida.Value;
                }
                chkfecfin.Checked = true;
                dtpfecfin.Enabled = true;
                dtphorafin.Enabled = true;
                dtpfecfin.Value = RegSalida.Value;
                dtphorafin.Value = RegSalida.Value;
                this._RegSalida = RegSalida;
            }
        }
    }
}