using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace CaniaBrava
{
    class NumDoc
    {
        public NumDoc() { }

        public void updNumDoc(string operacion, string codcaja,
            string codcia, string tipodoc, string serie, string nrodoc)
        {
            string query;
            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();

            if (operacion.Equals("AGREGAR"))
            {
                query = "INSERT INTO numdoc (codcaja,codcia,tipodoc,serie,nrodoc) VALUES ('" + @codcaja + "', ";
                query = query + "'" + @codcia + "', '" + @tipodoc + "','" + @serie + "','" + @nrodoc + "');";
            }
            else
            {
                query = "UPDATE numdoc SET serie='" + @serie + "',nrodoc='" + @nrodoc + "' ";
                query = query + " WHERE codcia='" + @codcia + "' ";
                query = query + " and codcaja='" + @codcaja + "' and tipodoc='" + @tipodoc + "';";
            }
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

        public void delNumDoc(string codcia, string codcaja, string tipodoc)
        {
            string query;
            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();
            query = "DELETE from numdoc WHERE codcia='" + @codcia + "' and ";
            query = query + " codcaja='" + @codcaja + "' and tipodoc='" + @tipodoc + "';";
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

        public string genNumDoc(string codcia, string codcaja, string tipodoc)
        {
            Funciones funciones = new Funciones();
            string rfserie = string.Empty;
            string rfnro = string.Empty;
            string nrodoc = string.Empty;
            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();
            string query = "SELECT serie,nrodoc from numdoc WHERE codcia='" + @codcia + "' and ";
            query = query + " codcaja='" + @codcaja + "' and tipodoc='" + @tipodoc + "';";
            try
            {
                SqlCommand myCommand = new SqlCommand(query, conexion);
                SqlDataReader myReader = myCommand.ExecuteReader();
                if (myReader.Read())
                {
                    rfserie = myReader.GetString(myReader.GetOrdinal("serie"));
                    rfnro = myReader.GetString(myReader.GetOrdinal("nrodoc"));
                    string signum=(int.Parse(rfnro)+1).ToString();
                    nrodoc = rfserie+funciones.replicateCadena("0", 8 - signum.Trim().Length) + signum.Trim();
                }
                myReader.Close();
                myCommand.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            conexion.Close();
            return nrodoc;
        }
    }
}
