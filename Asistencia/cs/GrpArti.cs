using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace CaniaBrava
{
    class GrpArti
    {
        public GrpArti() { }

        public void updGrpArti(string operacion, string famarti,string grparti,
            string desgrparti,string estado)
        {
            string query;
            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();

            if (operacion.Equals("AGREGAR"))
            {
                query = "INSERT INTO grparti (famarti,grparti,desgrparti,estado) VALUES ('" + @famarti + "', ";
                query=query+"'" + @grparti + "', '" + @desgrparti + "', '" + @estado + "');";
            }
            else
            {
                query = "UPDATE grparti SET desgrparti='" + @desgrparti+ "',estado='" + @estado + "' ";
                query = query + "WHERE famarti='" + @famarti + "' and grparti='" + @grparti + "';";
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

        public void delGrpArti(string famarti,string grparti)
        {
            string query;
            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();
            query = "DELETE from grparti WHERE famarti='" + @famarti + "' and grparti='"+@grparti+"';";
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
