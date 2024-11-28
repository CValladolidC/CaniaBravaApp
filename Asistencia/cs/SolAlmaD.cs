using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace CaniaBrava
{
    class SolAlmaD
    {
        public string genCod(string codcia, string alma, string secsoli, string solalma)
        {
            Funciones funciones = new Funciones();
            string codigoInterno = string.Empty;
            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();
            string query = " select isnull(max(item),'1') as existencia,";
            query = query + " max(item)+1 as codigointerno from solalmad where alma='" + @alma + "' ";
            //query = query + " and codcia='" + @codcia + "' and secsoli='" + @secsoli + "' ";
            query = query + " and solalma='" + @solalma + "' ;";

            try
            {
                SqlCommand myCommand = new SqlCommand(query, conexion);
                SqlDataReader myReader = myCommand.ExecuteReader();
                if (myReader.Read())
                {
                    if (myReader.GetString(myReader.GetOrdinal("existencia")).Trim().Equals("1"))
                    {
                        codigoInterno = "001";
                    }
                    else
                    {
                        string codigo = myReader.GetInt32(myReader.GetOrdinal("codigointerno")).ToString();
                        codigoInterno = funciones.replicateCadena("0", 3 - codigo.Trim().Length) + codigo;
                    }
                }

                myReader.Close();
                myCommand.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            conexion.Close();
            return codigoInterno;
        }

        public void updSolalmaD(string operacion, string codcia, string alma, string secsoli, string solalma, string item,
            string codarti, string manual, float cantidad, string glosa1, string glosa2, string glosa3,
            string desarti, string unidad)
        {
            string query;
            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();
            if (operacion.Equals("AGREGAR"))
            {
                query = "INSERT INTO solalmad (codcia,alma,secsoli,solalma, ";
                query = query + " item,codarti,manual,cantidad,glosa1,glosa2,glosa3,desarti,unidad) ";
                query = query + " VALUES ('" + @codcia + "','" + @alma + "', '" + @secsoli + "', '" + @solalma + "', ";
                query = query + " '" + @item + "', '" + @codarti + "','" + @manual + "', '" + @cantidad + "', ";
                query = query + " '" + @glosa1 + "', '" + @glosa2 + "', '" + @glosa3 + "', ";
                query = query + " '" + @desarti + "','" + @unidad + "'); ";
            }
            else
            {
                query = "UPDATE solalmad SET codarti='" + @codarti + "',manual='" + @manual + "',";
                query = query + " cantidad='" + @cantidad + "',glosa1='" + @glosa1 + "', ";
                query = query + " glosa2='" + @glosa2 + "',glosa3='" + @glosa3 + "', ";
                query = query + " desarti='" + @desarti + "',unidad='" + @unidad + "' ";
                query = query + " WHERE codcia='" + @codcia + "' and alma='" + @alma + "' and secsoli='" + @secsoli + "' ";
                query = query + " and solalma='" + @solalma + "' and item='" + @item + "';";
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

        public void delSolAlmaD(string codcia, string alma, string secsoli, string solalma, string item)
        {
            string query;
            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();
            query = "DELETE from solalmad WHERE /*codcia='" + @codcia + "' and*/ alma='" + @alma + "' ";
            query = query + " /*and secsoli='" + @secsoli + "'*/ ";
            query = query + " and solalma='" + @solalma + "' and item='" + @item + "';";
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