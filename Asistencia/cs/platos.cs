using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Data;

namespace CaniaBrava.cs
{
    class platos
    {

        public void listacomedor(ComboBox cb)
        {
            
            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();
            string squerye = "Select comedor from Asistencia.dbo.platos WHERE comedor='" + @cb + "';";

            try
            {
                SqlCommand myCommand_table = new SqlCommand(squerye, conexion);
                DataTable dt_table = new DataTable();
                dt_table.Load(myCommand_table.ExecuteReader());
                myCommand_table.Dispose();

                
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);

            }
            conexion.Close();
        }
    }
}
