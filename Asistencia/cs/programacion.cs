using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CaniaBrava
{
    public class programacion
    {
        #region Atributos
        public int idprog { get; set; }
        public string desprog { get; set; }
        public DateTime fechaini { get; set; }
        public DateTime fechafin { get; set; }
        public string estado { get; set; }
        public string idusr_chk { get; set; }
        public string idusrins { get; set; }
        public DateTime fechains { get; set; }
        public string idusrupd { get; set; }
        public DateTime fechaupd { get; set; }

        public string idperplan { get; set; }
        public string nrodoc { get; set; }
        public DateTime fechadiaria { get; set; }
        public string idsap { get; set; }
        public string destrabajador { get; set; }
        public string destipohorario { get; set; }
        public int turnonoche { get; set; }
        public string homeoffice { get; set; }
        public string seccion { get; set; }
        public string gerencia { get; set; }
        public string idcia { get; set; }
        public bool SinHorario { get; set; }
        public int CuentaFec { get; set; }
        #endregion

        #region Metodos
        public int GetMaxId()
        {
            int resultado = 0;
            string query = "SELECT ISNULL(MAX(idprog),0)+1 AS contador FROM prog (NOLOCK)";
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

        public void UpdateProg(programacion be, bool evt, List<programacion> lista)
        {
            string query = string.Empty;

            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();
            if (!evt)
            {
                be.idprog = GetMaxId();
                query = " INSERT INTO prog (idprog,desprog,fechaini,fechafin,estado,idusrins,fechains,idusrupd,fechaupd) ";
                query += "VALUES (" + be.idprog + ", '" + be.desprog + "', '" + be.fechaini.ToString("yyyy-MM-dd") + "', '" + be.fechafin.ToString("yyyy-MM-dd") + "',";
                query += "'" + be.estado + "','" + be.idusrins + "', GETDATE(),'" + be.idusrins + "', GETDATE());";
            }
            else
            {
                query = " UPDATE prog SET desprog='" + be.desprog + "',idusrupd='" + be.idusrupd + "',fechaupd = GETDATE(),estado='" + be.estado + "',";
                query += "fechaini='" + be.fechaini.ToString("yyyy-MM-dd") + "',fechafin='" + be.fechafin.ToString("yyyy-MM-dd") + "', ";
                query += "idusr_chk='', fechachk=null WHERE idprog = " + be.idprog + ";";
            }
            try
            {
                SqlCommand myCommand = new SqlCommand(query, conexion);
                myCommand.ExecuteNonQuery();

                if (evt) { DeleteProgDet(be.idprog); }

                UpdateProgdet(lista, be.idprog);

                myCommand.Dispose();
            }
            catch (SqlException e)
            {
                MessageBox.Show(e.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            conexion.Close();
        }

        private void UpdateProgdet(List<programacion> listdet, int resultado)
        {
            string query = string.Empty;
            foreach (programacion be in listdet)
            {
                query += "INSERT INTO progdet ";
                query += "VALUES (" + resultado + ",'" + be.idperplan + "','" + be.nrodoc + "','" + be.fechadiaria.ToString("yyyy-MM-dd") + "'";
                query += ",'" + be.idsap + "','" + be.destrabajador + "','" + be.destipohorario + "','" + be.fechaini.ToString("yyyy-MM-dd HH:mm:ss") + "'";
                query += ",'" + be.fechafin.ToString("yyyy-MM-dd HH:mm:ss") + "','" + be.turnonoche + "','" + be.homeoffice + "','" + be.estado + "'";
                query += ",'" + be.seccion + "','" + be.gerencia + "','" + be.idcia + "');";
            }
            BulkInsertarProgDet(query);
        }

        private void BulkInsertarProgDet(string query)
        {
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

        private void InsertarProgDet(programacion be)
        {
            string query = " INSERT INTO progdet (idprog,idperplan,nrodoc,fechadiaria,idsap,destrabajador,destipohorario,fechaini,fechafin,turnonoche,homeoffice,estado,seccion,gerencia,idcia) ";
            query += "VALUES (" + be.idprog + ",'" + be.idperplan + "','" + be.nrodoc + "','" + be.fechadiaria.ToString("yyyy-MM-dd") + "',0,'" + be.destrabajador + "'";
            query += ",'" + be.destipohorario + "','" + be.fechaini.ToString("yyyy-MM-dd HH:mm:ss") + "'";
            query += ",'" + be.fechafin.ToString("yyyy-MM-dd HH:mm:ss") + "','" + be.turnonoche + "',0,'" + be.estado + "','" + be.seccion + "','" + be.gerencia + "','" + be.idcia + "');";

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

        public void DeleteProgDet(int idprog)
        {
            string query = "delete from progdet where idprog = " + idprog;
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
        #endregion
    }
}