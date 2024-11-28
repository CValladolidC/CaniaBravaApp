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
    public partial class ui_updcomensales : Form
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

        public ui_updcomensales()
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
                filtros.filtrarPerPlan("ui_updcomensales", this, txtCodigoInterno, cadenaBusqueda);
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
                        //dtFechaIni.Enabled = true;
                        btnAgregar.Enabled = true;

                        btnAgregar.Focus();
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
            btnAgregar.Enabled = false;
        }
        #endregion

        public void LoadComensal(string idperplan, string dni, string fecha, string comensal, string nromov, string servicio)
        {
            txtnromov.Text = nromov;
            dtFechaIni.Value = DateTime.Parse(fecha);
            txtDni.Text = dni;
            txtComensal.Text = comensal;
            txtTiposervicio.Text = servicio;
            cmbPerfil.Text = "T         TRABAJADOR DE LA EMPRESA";

            LimpiarFiltros();
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            try
            {
                string nrmov = txtnromov.Text.Trim();
                string idperplan = txtCodigoInterno.Text.Trim();
                string fechaini = dtFechaIni.Value.ToString("yyyy-MM-dd");
                string proveedor = txtProveedor.Text.Trim();
                string dni = txtDni.Text.Trim();
                string comensal = txtComensal.Text.Trim();
                string[] arrComen = comensal.Split(',');
                string apellidos = arrComen[0].Trim();
                string nombres = arrComen[1].Trim();

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
                        AddAsistenciaInvitado(idperplan, fechaini, fechaini, proveedor, nrmov, dni, apellidos, nombres);
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

        private void AddAsistenciaInvitado(string idperplan, string fechaini, string fechafin, string proveedor, string nromov,
            string dni, string apellidos, string nombres)
        {
            if (VerificarDatos(fechaini, fechafin) > 0)
            {
                MessageBox.Show("El invitado ya esta registrado en la fecha seleccionada!!", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            string query = "UPDATE control SET tipomov='T' WHERE nromov = '" + nromov + "';";
            query += "INSERT INTO prog_invitados VALUES ";
            DateTime fini = DateTime.Parse(fechaini);
            DateTime ffin = DateTime.Parse(fechafin);
            for (DateTime ff = fini; ff <= ffin; ff = ff.AddDays(1))
            {
                query += "('" + idperplan + "','T','" + ff.ToString("yyyy-MM-dd") + "','','" + ff.ToString("yyyy-MM-dd") + "','C','" + dni + "'";
                query += ",'" + ff.ToString("yyyy-MM-dd") + "','" + proveedor + "','','" + apellidos + "','" + nombres + "'),";
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
    }
}