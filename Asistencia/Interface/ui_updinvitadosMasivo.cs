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
    public partial class ui_updinvitadosMasivo : Form
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
        List<Invitados> Lista01;
        Invitados obj01;

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
            this.Text = "Programación masiva de atenciones en Comedor";

            dtFechaIni.MinDate = DateTime.Now;
            dtFechaFin.MinDate = DateTime.Now;

            var listaInvitados = Load_Datos();

            LoadIni(listaInvitados);
        }

        private void LoadIni(List<Invitados> list)
        {
            Lista01 = new List<Invitados>();
            Lista01.AddRange(list);
            //Lista01.AddRange(list.Select(x => new Invitados()
            //{
            //    Param1 = x.idperplan,
            //    Param2 = x.destrabajador,
            //    Param3 = x.seccion,
            //    Param4 = x.nrodoc,
            //    Param5 = x.idcia,
            //    Param6 = x.gerencia
            //}));
            listInvitados.DataSource = list;
            listInvitados.DisplayMember = "invitado";
            listInvitados.ValueMember = "dni";
        }

        private List<Invitados> Load_Datos()
        {
            string query = " SELECT dni,apellidos,nombres,estado ";
            query += "FROM invitados (NOLOCK) WHERE estado = 'V' ORDER BY apellidos; ";

            List<Invitados> lista = new List<Invitados>();
            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();

            try
            {
                SqlCommand myCommand = new SqlCommand(query, conexion);
                SqlDataReader odr = myCommand.ExecuteReader();

                Invitados obj = null;
                while (odr.Read())
                {
                    obj = new Invitados();
                    obj.dni = odr.GetString(odr.GetOrdinal("dni"));
                    obj.nombres = odr.GetString(odr.GetOrdinal("nombres"));
                    obj.apellidos = odr.GetString(odr.GetOrdinal("apellidos"));
                    obj.invitado = obj.apellidos.Trim() + ", " + obj.nombres.Trim();
                    obj.estado = odr.GetString(odr.GetOrdinal("estado"));
                    lista.Add(obj);
                }

                odr.Close();
                myCommand.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            finally { conexion.Close(); }

            return lista;
        }

        public ui_updinvitadosMasivo()
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
                filtros.filtrarPerPlan("ui_updinvitadosMasivo", this, txtCodigoInterno, cadenaBusqueda);
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

                    if (listProg.Items.Count == 0 && valorValida == "G")
                    {
                        valorValida = "B";
                        MessageBox.Show("No ha agregado ningun Invitado", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            for (int i = 0; i < listProg.Items.Count; i++)
            {
                DateTime fini = DateTime.Parse(fechaini);
                DateTime ffin = DateTime.Parse(fechafin);
                for (DateTime ff = fini; ff <= ffin; ff = ff.AddDays(1))
                {
                    query += "('" + idperplan + "','T','" + ff.ToString("yyyy-MM-dd") + "','','" + ff.ToString("yyyy-MM-dd") + "','C','" + ((Invitados)listProg.Items[i]).dni + "'";
                    query += ",'" + ff.ToString("yyyy-MM-dd") + "','" + @proveedor + "','','" + ((Invitados)listProg.Items[i]).nombres + "','" + ((Invitados)listProg.Items[i]).apellidos + "'),";
                }
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
                this.Close();
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
            string dnis = string.Empty;
            for (int i = 0; i < listProg.Items.Count; i++)
            {
                dnis += "'" + ((Invitados)listProg.Items[i]).dni + "',";
            }
            this._dni = dnis.Substring(0, dnis.Length - 1);
            string query = "SELECT COUNT(1) as result FROM prog_invitados (NOLOCK) WHERE dni in (" + this._dni + ") AND fecha BETWEEN '" + fechaini + "' AND '" + fechafin + "' ";

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

        #region Eventos Botones de lista
        private void DataBinding_()
        {
            listInvitados.DataSource = null;

            listProg.DisplayMember = "invitado";
            listProg.ValueMember = "dni";

            listInvitados.DataSource = Lista01;
            listInvitados.DisplayMember = "invitado";
            listInvitados.ValueMember = "dni";
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (listInvitados.SelectedItem != null)
            {
                obj01 = (Invitados)listInvitados.SelectedItem;

                listProg.Items.Add(obj01);
                if (listProg != null)
                {
                    Lista01.Remove(obj01);
                }
                DataBinding_();
            }
        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            obj01 = (Invitados)listProg.SelectedItem;

            if (obj01 != null)
            {
                Lista01.Add(obj01);
                listProg.Items.Remove(obj01);
            }
            DataBinding_();
        }
        #endregion
    }
}