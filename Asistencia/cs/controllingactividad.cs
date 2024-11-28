using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Data;
using CaniaBrava.Interface;

namespace CaniaBrava.cs
{
    class controllingactividad
    {
        SqlCommand cmd;
        SqlDataReader dr;

        public bool Cargarcontrollingactividad(DataTable tbData)
        {
            bool resultado = true;
            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            {
                conexion.Open();
                using (SqlBulkCopy s = new SqlBulkCopy(conexion))
                {

                    //ingresamos COLUMNAS ORIGEN | COLUMNAS DESTINOS
                    s.ColumnMappings.Add("Codigo", "idmaescon");
                    s.ColumnMappings.Add("DescripcionCodigo", "desmaesgen");
                    s.ColumnMappings.Add("Um", "um");
                    s.ColumnMappings.Add("CuentaContable", "cuencon");
                    s.ColumnMappings.Add("DescripcionCuentaContable", "descuencon");
                    s.ColumnMappings.Add("CodigoUm", "cod");
                    s.ColumnMappings.Add("Rubro", "rubro");
                    s.ColumnMappings.Add("CodActividad", "actividad");
                    s.ColumnMappings.Add("DescripcionActividad", "desactividad");
                    s.ColumnMappings.Add("CodRecursos", "recurso");
                    s.ColumnMappings.Add("DescripcionRecurso", "desrecurso");
                    
                    //definimos la tabla a cargar
                    s.DestinationTableName = "maescontrolling";


                    s.BulkCopyTimeout = 1500;
                    try
                    {
                        s.WriteToServer(tbData);
                    }
                    catch (Exception e)
                    {
                        string st = e.Message;
                        resultado = false;
                    }

                }
            }

            return resultado;
        }

        public void um(ComboBox cb)
        {

            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();
            string squerye = "SELECT DISTINCT(um) um FROM Asistencia.dbo.maescontrolling ";

            try
            {
                cmd = new SqlCommand(squerye, conexion);
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    cb.Items.Add(dr["um"].ToString());
                }
                cb.SelectedIndex = 0;
                dr.Close();

            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);

            }
        }

        public void rubro(ComboBox cb)
        {

            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();
            string squerye = "SELECT DISTINCT(rubro) rubro FROM Asistencia.dbo.maescontrolling ";

            try
            {
                cmd = new SqlCommand(squerye, conexion);
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    cb.Items.Add(dr["rubro"].ToString());
                }
                cb.SelectedIndex = 0;
                dr.Close();

            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);

            }
        }

        public void eliminaractividad(string idact)
        {
            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();
            string query = "DELETE FROM Asistencia.dbo.maescontrolling WHERE idregistro ='" + @idact + "';";

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

    }
}
