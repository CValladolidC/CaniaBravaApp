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
    public partial class ui_programacionInicialAgri : Form
    {
        Funciones funciones = new Funciones();
        private string Id { get; set; }
        private int Dias { get; set; }
        private bool Reprog { get; set; }
        private Form FormPadre;

        public Form _FormPadre
        {
            get { return FormPadre; }
            set { FormPadre = value; }
        }

        public ui_programacionInicialAgri()
        {
            InitializeComponent();

            if (funciones.VersionAssembly()) Application.ExitThread();
        }

        public void Nuevo(string idprog, int dias, bool reprog)
        {
            this.Id = idprog;
            this.Dias = dias;
            this.Reprog = reprog;

            DateTime FechaMaxima = GetMaxFechaProgramacion();

            if (FechaMaxima <= DateTime.Now.Date) { FechaMaxima = DateTime.Now.Date; }
            dtpFecini.MinDate = DateTime.Parse(FechaMaxima.AddDays(1).ToString("yyyy-MM-dd"));
            dtpFecfin.MinDate = DateTime.Parse(FechaMaxima.AddDays(1).ToString("yyyy-MM-dd"));

            GetTitulo();

            if (this.Id != string.Empty)
            {
                dtpFecfin.Value = dtpFecini.Value.AddDays(dias);
                dtpFecfin.Enabled = false;
            }
        }

        private void GetTitulo()
        {
            txtProg.Text = "Programación " + dtpFecini.Value.ToString("yyyy-MM-dd") + " al " + dtpFecfin.Value.ToString("yyyy-MM-dd");
        }

        private DateTime GetMaxFechaProgramacion()
        {
            GlobalVariables variables = new GlobalVariables();
            DateTime fecha = new DateTime();

            string query = "SELECT ISNULL(MAX(fechafin),CONVERT(VARCHAR(10), GETDATE(), 120)) as fecha FROM progagri (NOLOCK) ";
            if (variables.getValorTypeUsr() != "00")
            {
                query += @"WHERE idusrins IN (SELECT distinct idusr FROM cencosusr (NOLOCK) WHERE idcencos in (
                        SELECT idcencos FROM cencosusr (NOLOCK) WHERE idusr = '" + variables.getValorUsr() + "')) ";

                if (variables.getValorTypeUsr() == "03" && variables.getValorNivelUsr() == string.Empty)
                {
                    query += "AND idusrins = '" + variables.getValorUsr() + "'";
                }
            }

            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();

            try
            {
                SqlCommand myCommand = new SqlCommand(query, conexion);
                SqlDataReader odr = myCommand.ExecuteReader();

                while (odr.Read())
                {
                    fecha = odr.GetDateTime(odr.GetOrdinal("fecha"));
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

            return fecha;
        }

        private void btnGrabar_Click(object sender, EventArgs e)
        {
            GlobalVariables variables = new GlobalVariables();
            programacion be = new programacion()
            {
                desprog = txtProg.Text.ToUpper(),
                fechaini = dtpFecini.Value,
                fechafin = dtpFecfin.Value,
                estado = "V",
                idusrins = variables.getValorUsr(),
                idusrupd = variables.getValorUsr()
            };

            if (this.Reprog)
            {
                ReProg(this.Id, be.fechaini, this.Dias);
                MessageBox.Show("Se reprogramó registro de actividades agricolas.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                int idprog = UpdateProg(be, false);

                if (this.Id != string.Empty)
                {
                    CopyProg(this.Id, idprog, be.fechaini);
                    MessageBox.Show("Se copio con exito registro de actividades agricolas.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                ((ui_programacionGrafAgri)FormPadre)._TextBoxActivo.Text = idprog + "|" + dtpFecini.Value.ToString("yyyy-MM-dd") + "|" +
                    dtpFecfin.Value.ToString("yyyy-MM-dd") + "|" + txtProg.Text;
            }

            ((ui_programacionGrafAgri)FormPadre).btnActualizar.PerformClick();
            this.Close();
        }

        private void dtpFecini_ValueChanged(object sender, EventArgs e)
        {
            if (this.Id != string.Empty) { dtpFecfin.Value = dtpFecini.Value.AddDays(this.Dias); }
            else { dtpFecfin.Value = dtpFecini.Value; }
            GetTitulo();
        }

        private void dtpFecfin_ValueChanged(object sender, EventArgs e)
        {
            GetTitulo();
        }

        private int UpdateProg(programacion be, bool evt)
        {
            string query = string.Empty;

            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();
            if (!evt)
            {
                be.idprog = GetMaxId();
                query = " INSERT INTO progagri (idprog,desprog,fechaini,fechafin,estado,idusrins,fechains,idusrupd,fechaupd) ";
                query += "VALUES ('" + be.idprog + "', '" + be.desprog + "', '" + be.fechaini.ToString("yyyy-MM-dd") + "', '" + be.fechafin.ToString("yyyy-MM-dd") + "',";
                query += "'" + be.estado + "','" + be.idusrins + "', GETDATE(),'" + be.idusrins + "', GETDATE());";
            }
            else
            {
                query = " UPDATE progagri SET desprog='" + be.desprog + "',idusrupd='" + be.idusrupd + "',fechaupd = GETDATE(),estado='" + be.estado + "',";
                query += "fechaini='" + be.fechaini.ToString("yyyy-MM-dd") + "',fechafin='" + be.fechafin.ToString("yyyy-MM-dd") + "', ";
                query += "WHERE idprog = " + be.idprog + ";";
            }
            try
            {
                SqlCommand myCommand = new SqlCommand(query, conexion);
                myCommand.ExecuteNonQuery();

                //UpdateProgagridet(be.idprog, be.fechaini, be.fechafin);
                myCommand.Dispose();
            }
            catch (SqlException e)
            {
                MessageBox.Show(e.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            conexion.Close();

            return be.idprog;
        }

        private void CopyProg(string id, int idnew, DateTime fe1)
        {
            string query = string.Empty;

            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();


            query = "EXECUTE SP_COPYPROGAGRI '" + id + "','" + idnew + "','" + fe1.ToString("yyyy-MM-dd") + "';";

            try
            {
                SqlCommand myCommand = new SqlCommand(query, conexion);
                myCommand.ExecuteNonQuery();
                myCommand.Dispose();
            }
            catch (SqlException e)
            {
                MessageBox.Show(e.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            conexion.Close();
        }

        private void ReProg(string id, DateTime fe1, int dias)
        {
            string query = string.Empty;

            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();

            query = "UPDATE progagri SET fechaini='" + fe1.ToString("yyyy-MM-dd") + "',fechafin='" + fe1.AddDays(dias).ToString("yyyy-MM-dd") + "' WHERE idprog= '" + id + "';";
            query += "EXECUTE SP_REPROGAGRI '" + id + "','" + fe1.ToString("yyyy-MM-dd") + "';";

            try
            {
                SqlCommand myCommand = new SqlCommand(query, conexion);
                myCommand.ExecuteNonQuery();
                myCommand.Dispose();
            }
            catch (SqlException e)
            {
                MessageBox.Show(e.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            conexion.Close();
        }

        private void UpdateProgagridet(int idprog, DateTime fechaini, DateTime fechafin)
        {
            string query = string.Empty;

            for (DateTime i = fechaini.Date; i.Date <= fechafin.Date; i = i.AddDays(1))
            {
                query += "INSERT INTO progagri_feca (idprog, fecha) values ('" + idprog + "','" + i.ToString("yyyy-MM-dd") + "');\n";
            }

            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();
            try
            {
                SqlCommand myCommand = new SqlCommand(query, conexion);
                myCommand.ExecuteNonQuery();
                myCommand.Dispose();
            }
            catch (SqlException e)
            {
                MessageBox.Show(e.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            conexion.Close();
        }

        public int GetMaxId()
        {
            int resultado = 0;
            string query = "SELECT ISNULL(MAX(idprog),0)+1 AS contador FROM progagri (NOLOCK)";
            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();

            try
            {
                SqlCommand myCommand = new SqlCommand(query, conexion);
                SqlDataReader odr = myCommand.ExecuteReader();

                if (odr.Read())
                {
                    resultado = odr.GetInt32(odr.GetOrdinal("contador"));
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

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}