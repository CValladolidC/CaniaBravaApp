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
    public partial class ui_updinvitados : Form
    {
        string _operacion;
        string _dni;
        string _nombres;
        string _apellidos;
        string query = string.Empty;
        CalPlan calplan = new CalPlan();
        Funciones funciones = new Funciones();

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

        public void setValores(string dni, string nombres, string apellidos)
        {
            this._operacion = string.Empty;
            this._dni = dni;
            this._nombres = nombres;
            this._apellidos = apellidos;
            this.Text = "Programación de atenciones en Comedor";

            lblNombres.Text = "Nombres: " + this._nombres;
            lblApellidos.Text = "Apellidos: " + this._apellidos;
            lblDNI.Text = "Documento: " + this._dni;

            dtFechaIni.MinDate = DateTime.Now;
            dtFechaFin.MinDate = DateTime.Now;

            Load_Datos();
        }

        private void Load_Datos()
        {
            string query = " SELECT a.nromov as Item,a.dni as Documento,rtrim(a.apellidos)+', '+rtrim(a.nombres) as Invitado,CONVERT(VARCHAR(10),a.fecha,120) ";
            query += "as [Fecha de Ingreso], a.idperplan as [Cod. Trab.], rtrim(b.apepat)+' '+rtrim(b.apemat)+' '+rtrim(b.nombres) as Trabajador ";
            query += "FROM prog_invitados a (NOLOCK) INNER JOIN perplan b (NOLOCK) ON b.idperplan=a.idperplan WHERE a.dni = '" + this._dni + "' ORDER BY a.fecha desc ";

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
                    dgvdetalle.Columns[1].Width = 80;
                    dgvdetalle.Columns[2].Width = 250;
                    dgvdetalle.Columns[3].Width = 80;
                    dgvdetalle.Columns[4].Width = 80;
                    dgvdetalle.Columns[5].Width = 250;

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

        public ui_updinvitados()
        {
            InitializeComponent();

            if (funciones.VersionAssembly()) Application.ExitThread();
        }

        private void txtCodigoInterno_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                string cadenaBusqueda = string.Empty;
                this._TextBoxActivo = txtCodigoInterno;
                FiltrosMaestros filtros = new FiltrosMaestros();
                filtros.filtrarPerPlan("ui_updinvitados", this, txtCodigoInterno, cadenaBusqueda);
            }
        }

        #region Eventos KeyPress
        private void txtCodigoInterno_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                if (txtCodigoInterno.Text.Trim() != string.Empty)
                {
                    string idperplan = txtCodigoInterno.Text.Trim();

                    PerPlan perplan = new PerPlan();
                    string codigoInterno = perplan.ui_getDatosPerPlan(idperplan, "0");
                    if (codigoInterno == string.Empty)
                    {
                        MessageBox.Show("Código del trabajador no registrado", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);

                        LimpiarFiltros();
                    }
                    else
                    {
                        txtCodigoInterno.Text = perplan.ui_getDatosPerPlan(idperplan, "0");
                        txtDocIdent.Text = perplan.ui_getDatosPerPlan(idperplan, "1");
                        txtNroDocIden.Text = perplan.ui_getDatosPerPlan(idperplan, "2");
                        txtNombres.Text = perplan.ui_getDatosPerPlan(idperplan, "3");

                        e.Handled = true;
                        dtFechaIni.Enabled = true;
                        dtFechaFin.Enabled = true;
                        btnAgregar.Enabled = true;

                        dtFechaIni.Focus();
                    }
                }
                else { LimpiarFiltros(); }
            }
        }

        private void LimpiarFiltros()
        {
            txtCodigoInterno.Clear();
            txtDocIdent.Clear();
            txtNroDocIden.Clear();
            txtNombres.Clear();
            txtCodigoInterno.Focus();
            dtFechaIni.Enabled = false;
            dtFechaFin.Enabled = false;
            btnAgregar.Enabled = false;
        }

        private void dtFechaIni_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                dtFechaFin.MinDate = dtFechaIni.Value;
                dtFechaFin.Value = dtFechaIni.Value;
                dtFechaFin.Focus();
            }
        }

        private void dtFechaFin_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                btnAgregar.Focus();
            }
        } 
        #endregion

        private void txtCodigoInterno_TextChanged(object sender, EventArgs e)
        {

        }

        private void dtFechaIni_ValueChanged(object sender, EventArgs e)
        {
            dtFechaFin.MinDate = dtFechaIni.Value;
            dtFechaFin.Value = dtFechaIni.Value;
            dtFechaFin.Focus();
        }

        private void dtFechaFin_ValueChanged(object sender, EventArgs e)
        {
            btnAgregar.Focus();
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            try
            {
                string idperplan = txtCodigoInterno.Text.Trim();
                string fechaini = dtFechaIni.Value.ToString("yyyy-MM-dd");
                string fechafin = dtFechaFin.Value.ToString("yyyy-MM-dd");
                string proveedor = txtProveedor.Text.Trim();

                if (idperplan != string.Empty)
                {
                    string valorValida = "G";

                    if (txtCodigoInterno.Text == string.Empty && valorValida == "G")
                    {
                        valorValida = "B";
                        MessageBox.Show("No ha seleccionado Trabajador", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txtCodigoInterno.Focus();
                    }

                    if (valorValida.Equals("G"))
                    {
                        AddAsistenciaInvitado(idperplan, fechaini, fechafin, proveedor);

                        Load_Datos();
                    }
                }
                else
                {
                    MessageBox.Show("No ha seleccionado trabajador", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtCodigoInterno.Focus();
                }
            }
            catch (FormatException ex)
            {
                MessageBox.Show(ex.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void AddAsistenciaInvitado(string idperplan, string fechaini, string fechafin, string proveedor)
        {
            if (VerificarDatos(fechaini, fechafin) > 0)
            {
                MessageBox.Show("El invitado ya esta registrado en la fecha seleccionada!!", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            string query = "INSERT INTO prog_invitados VALUES ";
            DateTime fini = DateTime.Parse(fechaini);
            DateTime ffin = DateTime.Parse(fechafin);
            for (DateTime ff = fini; ff <= ffin; ff = ff.AddDays(1))
            {
                query += "('" + idperplan + "','T','" + ff.ToString("yyyy-MM-dd") + "','','" + ff.ToString("yyyy-MM-dd") + "','C','" + this._dni + "'";
                query += ",'" + ff.ToString("yyyy-MM-dd") + "','" + proveedor + "','','" + this._apellidos + "','" + this._nombres + "'),";
            }

            query = query.Substring(0, query.Length - 1) + ";";

            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();

            try
            {
                SqlCommand myCommand = new SqlCommand(query, conexion);
                myCommand.ExecuteNonQuery();
                myCommand.Dispose();

                MessageBox.Show("Datos guardados exitosamente", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LimpiarFiltros();
            }
            catch (SqlException e)
            {
                MessageBox.Show(e.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }

            conexion.Close();
        }

        private int VerificarDatos(string fechaini, string fechafin)
        {
            int resultado = 0;
            string query = "SELECT COUNT(1) as result FROM prog_invitados (NOLOCK) WHERE dni = '" + this._dni + "' AND fecha BETWEEN '" + fechaini + "' AND '" + fechafin + "' ";

            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();

            try
            {
                SqlCommand myCommand = new SqlCommand(query, conexion);
                SqlDataReader odr = myCommand.ExecuteReader();

                if (odr.Read())
                {
                    resultado = odr.GetInt32(odr.GetOrdinal("result"));
                }

                odr.Close();
                myCommand.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            finally
            {
                conexion.Close();
            }

            return resultado;
        }

        private void dgvdetalle_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            DialogResult response = MessageBox.Show("¿Desea eliminar el registro?", "Accion", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
            if (response == DialogResult.Yes)
            {
                Int32 selectedCellCount = dgvdetalle.GetCellCount(DataGridViewElementStates.Selected);
                if (selectedCellCount > 0)
                {
                    string dni = dgvdetalle.Rows[dgvdetalle.SelectedCells[1].RowIndex].Cells[1].Value.ToString();
                    string fecha = dgvdetalle.Rows[dgvdetalle.SelectedCells[1].RowIndex].Cells[3].Value.ToString();

                    string query = string.Empty;

                    SqlConnection conexion = new SqlConnection();
                    conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
                    conexion.Open();

                    query = "DELETE FROM prog_invitados WHERE dni = '" + @dni + "' AND fecha = '" + @fecha + "';";

                    try
                    {
                        SqlCommand myCommand = new SqlCommand(query, conexion);
                        myCommand.ExecuteNonQuery();
                        myCommand.Dispose();
                        MessageBox.Show("Registro Eliminado..", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Load_Datos();
                        LimpiarFiltros();
                    }
                    catch (SqlException ex)
                    {
                        MessageBox.Show(ex.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    }
                    conexion.Close();
                }
            }
            e.Cancel = true;
        }
    }
}