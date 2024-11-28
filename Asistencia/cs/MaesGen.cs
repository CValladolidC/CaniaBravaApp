using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Data;

namespace CaniaBrava
{
    class MaesGen
    {
        public MaesGen() { }

        public void actualizarMaesGen(string operacion,string idmaesgen, string clavemaesgen, string desmaesgen, string abrevia,
            string parm1maesgen, string parm2maesgen, string parm3maesgen, string statemaesgen, string tipomaesgen)
        {
            string query = string.Empty;
            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();
            if (operacion.Equals("AGREGAR"))
            {
                string[] arr = parm3maesgen.Split(',');
                if (arr.Length > 1)
                {
                    foreach (var item in arr)
                    {
                        query += "INSERT INTO maesgen (idmaesgen,clavemaesgen,desmaesgen,abrevia,parm1maesgen,parm2maesgen,";
                        query += "parm3maesgen,statemaesgen,tipomaesgen) VALUES ('" + @idmaesgen + "', '" + @clavemaesgen + "',";
                        query += "'" + @desmaesgen + "','" + @abrevia + "', '" + @parm1maesgen + "', '" + @parm2maesgen + "', '" + item + "',";
                        query += "'" + @statemaesgen + "','" + @tipomaesgen + "');";
                    }
                }
                else
                {
                    query += "INSERT INTO maesgen (idmaesgen,clavemaesgen,desmaesgen,abrevia,parm1maesgen,parm2maesgen,";
                    query += "parm3maesgen,statemaesgen,tipomaesgen) VALUES ('" + @idmaesgen + "', '" + @clavemaesgen + "',";
                    query += "'" + @desmaesgen + "','" + @abrevia + "', '" + @parm1maesgen + "', '" + @parm2maesgen + "', '" + @parm3maesgen + "',";
                    query += "'" + @statemaesgen + "','" + @tipomaesgen + "');";
                }
            }
            else
            {
                query += "UPDATE maesgen SET desmaesgen='" + @desmaesgen + "',abrevia='" + @abrevia + "',parm1maesgen='" + @parm1maesgen + "',";
                query += "parm2maesgen='" + @parm2maesgen + "', parm3maesgen='" + @parm3maesgen + "',";
                query += "statemaesgen='" + @statemaesgen + "',tipomaesgen='" + @tipomaesgen + "' ";
                query += "WHERE idmaesgen='" + @idmaesgen + "' and clavemaesgen='" + @clavemaesgen + "';";
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

        public void eliminarMaesGen(string idmaesgen,string clavemaesgen)
        {
            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();

            string query = "DELETE from maesgen WHERE idmaesgen='" + @idmaesgen + "' and clavemaesgen='"+@clavemaesgen+"';";

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

        public void listaDetMaesGen(string sidmaesgen, ComboBox cb, string tipo)
        {
            string valorItem;
            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();
            string squery_table= "SELECT clavemaesgen,desmaesgen FROM maesgen WHERE idmaesgen='000' AND clavemaesgen='"+@sidmaesgen+"';";
            string squery_detalle = "SELECT clavemaesgen,desmaesgen,parm1maesgen,parm2maesgen,parm3maesgen," +
                      "statemaesgen,idmaesgen FROM maesgen WHERE statemaesgen='V' "+
                      "and idmaesgen='" + @sidmaesgen + "' ORDER BY clavemaesgen;";

            try
            {
                ToolTip tooltip = new ToolTip();
                tooltip.SetToolTip(cb, "Tabla Común : " + sidmaesgen);
                SqlCommand myCommand_table = new SqlCommand(squery_table, conexion);
                DataTable dt_table = new DataTable();
                dt_table.Load(myCommand_table.ExecuteReader());
                myCommand_table.Dispose();

                if (dt_table.Rows.Count > 0)
                {
                    Funciones fn = new Funciones();
                    SqlCommand myCommand = new SqlCommand(squery_detalle, conexion);
                    DataTable dt = new DataTable();
                    dt.Load(myCommand.ExecuteReader());
                    myCommand.Dispose();

                    cb.Items.Clear();

                    if (tipo.Equals("B"))
                    {
                        valorItem = fn.replicateCadena(" ", 30);
                        cb.Items.Add(valorItem);
                    }

                    if (tipo.Equals("X"))
                    {
                        cb.Items.Add("X   TODOS");
                    }

                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            valorItem = (String)dt.Rows[i]["clavemaesgen"];
                            valorItem = valorItem + fn.replicateCadena(" ", ((valorItem.Length > 5 ? 13 : 6) - valorItem.Length)) + (String)dt.Rows[i]["desmaesgen"];
                            cb.Items.Add(valorItem);
                        }
                    }
                    else
                    {
                        MessageBox.Show("No existen registros en la Tabla Común " + sidmaesgen, "Aviso Importante", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    }
                }
                else
                {
                    MessageBox.Show("No existe la Tabla Común " + sidmaesgen, "Aviso Importante", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);

            }
            conexion.Close();
        }

        public void listaDetMaesGenToolStrip(string sidmaesgen, ToolStripComboBox cb, string tipo)
        {
            string squery_table;
            string squery_detalle;
            string valorItem;

            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();

            squery_table = "SELECT clavemaesgen,desmaesgen FROM maesgen WHERE idmaesgen='000' AND clavemaesgen='" + @sidmaesgen + "';";

            squery_detalle = "SELECT clavemaesgen,desmaesgen,parm1maesgen,parm2maesgen,parm3maesgen," +
                      "statemaesgen,idmaesgen FROM maesgen WHERE statemaesgen='V' " +
                      "and idmaesgen='" + @sidmaesgen + "';";
            try
            {

                SqlCommand myCommand_table = new SqlCommand(squery_table, conexion);
                DataTable dt_table = new DataTable();
                dt_table.Load(myCommand_table.ExecuteReader());
                myCommand_table.Dispose();

                if (dt_table.Rows.Count > 0)
                {
                    Funciones funcionusr = new Funciones();
                    SqlCommand myCommand = new SqlCommand(squery_detalle, conexion);
                    DataTable dt = new DataTable();
                    dt.Load(myCommand.ExecuteReader());
                    myCommand.Dispose();

                    cb.Items.Clear();

                    if (tipo.Equals("B"))
                    {
                        valorItem = funcionusr.replicateCadena(" ", 30);
                        cb.Items.Add(valorItem);
                    }

                    if (tipo.Equals("X"))
                    {
                        cb.Items.Add("X   TODOS");
                    }

                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            valorItem = (String)dt.Rows[i]["clavemaesgen"];
                            valorItem = valorItem + funcionusr.replicateCadena(" ", (5 - valorItem.Length)) + (String)dt.Rows[i]["desmaesgen"];
                            cb.Items.Add(valorItem);

                        }
                    }
                    else
                    {
                        MessageBox.Show("No existen registros en la Tabla Común " + sidmaesgen, "Aviso Importante", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    }
                }
                else
                {
                    MessageBox.Show("No existe la Tabla Común " + sidmaesgen, "Aviso Importante", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);

            }

            conexion.Close();

        }

        public void listaDetMaesGenQuery(string sidmaesgen,string squery_detalle, ComboBox cb, string tipo)
        {
            string squery_table;
            string valorItem;
            string valorItemIni = string.Empty;

            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();

            squery_table = "SELECT clavemaesgen,desmaesgen FROM maesgen WHERE idmaesgen='000' AND clavemaesgen='" + @sidmaesgen + "';";
                        
            try
            {

                ToolTip tooltip = new ToolTip();
                tooltip.SetToolTip(cb, "Tabla Común : " + sidmaesgen);

                SqlCommand myCommand_table = new SqlCommand(squery_table, conexion);
                DataTable dt_table = new DataTable();
                dt_table.Load(myCommand_table.ExecuteReader());
                myCommand_table.Dispose();

                if (dt_table.Rows.Count > 0)
                {
                    Funciones funcionusr = new Funciones();
                    SqlCommand myCommand = new SqlCommand(squery_detalle, conexion);
                    DataTable dt = new DataTable();
                    dt.Load(myCommand.ExecuteReader());
                    myCommand.Dispose();

                    cb.Items.Clear();

                    if (tipo.Equals("B"))
                    {
                        valorItem = funcionusr.replicateCadena(" ", 30);
                        cb.Items.Add(valorItem);
                    }

                    if (tipo.Equals("X"))
                    {
                        cb.Items.Add("X   TODOS");
                    }

                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            valorItem = (String)dt.Rows[i]["clavemaesgen"];
                            valorItem = valorItem + funcionusr.replicateCadena(" ", (5 - valorItem.Length)) + (String)dt.Rows[i]["desmaesgen"];
                            if (i == 0)
                            {
                                valorItemIni = valorItem;
                            }           
                            cb.Items.Add(valorItem);

                        }
                    }
                                                                
                    else
                    {
                        MessageBox.Show("No existen registros en la Tabla Común " + sidmaesgen, "Aviso Importante", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    }
                    if (tipo.Equals("B"))
                    {
                        cb.Text = funcionusr.replicateCadena(" ", 30);
                    }
                    else
                    {
                        if (tipo.Equals("X"))
                        {
                            cb.Text = "X   TODOS";
                        }
                        else
                        {
                            cb.Text = valorItemIni;
                        }
                    }     
                }
                else
                {
                    MessageBox.Show("No existe la Tabla Común " + sidmaesgen, "Aviso Importante", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);

            }

            conexion.Close();

        }

        public bool validarDetMaesGen(string sidmaesgen, ComboBox cb, string clavemaesgen)
        {
            bool resultado=false;
            string valorItem;
            Funciones fn = new Funciones();
            string clave = fn.longitudCadena(clavemaesgen, 4).Trim();
            if (clave != String.Empty)
            {
                SqlConnection conexion = new SqlConnection();
                conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
                conexion.Open();
                string query = "SELECT clavemaesgen,desmaesgen,parm1maesgen,parm2maesgen,parm3maesgen," +
                          "statemaesgen,idmaesgen FROM maesgen WHERE statemaesgen='V' " +
                          "and idmaesgen='" + @sidmaesgen + "' and clavemaesgen='" + @clave + "';";
                try
                {
                    SqlCommand cmd = new SqlCommand(query, conexion);
                    DataTable dt = new DataTable();
                    dt.Load(cmd.ExecuteReader());
                    cmd.Dispose();

                    if (dt.Rows.Count > 0)
                    {
                        valorItem = (String)dt.Rows[0]["clavemaesgen"];
                        cb.Text = valorItem + fn.replicateCadena(" ", ((valorItem.Length > 5 ? 13 : 5) - valorItem.Length)) + (String)dt.Rows[0]["desmaesgen"];
                        resultado = true;
                    }
                    else
                    {
                        MessageBox.Show("Registro no encontrado", "Aviso Importante", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        cb.Text = "";
                        resultado = false;
                    }
                }
                catch (SqlException ex)
                {
                    MessageBox.Show(ex.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }

                conexion.Close();
            }
            return resultado;
        }

        public void consultaDetMaesGen(string idmaesgen, string clavemaesgen, ComboBox cb)
        {
            string valorItem;
            Funciones fn = new Funciones();
            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();
            string query = "SELECT clavemaesgen,desmaesgen FROM maesgen WHERE idmaesgen='" + @idmaesgen +
                "' and clavemaesgen='" + @clavemaesgen + "';";
            try
            {
                SqlCommand cmd = new SqlCommand(query, conexion);
                DataTable dt = new DataTable();
                dt.Load(cmd.ExecuteReader());
                cmd.Dispose();
                if (dt.Rows.Count > 0)
                {
                    valorItem = (String)dt.Rows[0]["clavemaesgen"];
                    cb.Text = valorItem + fn.replicateCadena(" ", ((valorItem.Length > 5 ? 13 : 6) - valorItem.Length)) + (String)dt.Rows[0]["desmaesgen"];
                }
                else
                {
                    cb.Text = "";
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            conexion.Close();
        }

        public string verificaComboBoxMaesGen(string sidmaesgen, string cadena)
        {
            string resultado = "";
            string valorItem;
            Funciones fn = new Funciones();
            string clave = fn.longitudCadena(cadena, 4).Trim();
            if (clave != String.Empty)
            {
                SqlConnection conexion = new SqlConnection();
                conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
                conexion.Open();
                string query = "SELECT clavemaesgen,desmaesgen,parm1maesgen,parm2maesgen,parm3maesgen," +
                          "statemaesgen,idmaesgen FROM maesgen WHERE statemaesgen='V' " +
                          "and idmaesgen='" + @sidmaesgen + "' and clavemaesgen='" + @clave + "';";
                try
                {
                    SqlCommand cmd = new SqlCommand(query, conexion);
                    DataTable dt = new DataTable();
                    dt.Load(cmd.ExecuteReader());
                    cmd.Dispose();
                    if (dt.Rows.Count > 0)
                    {
                        valorItem = (String)dt.Rows[0]["clavemaesgen"];
                        resultado = valorItem + fn.replicateCadena(" ", (5 - valorItem.Length)) + (String)dt.Rows[0]["desmaesgen"];
                    }
                    else
                    {
                        resultado = "";
                    }
                }
                catch (SqlException ex)
                {
                    MessageBox.Show(ex.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);

                }
                conexion.Close();
            }
            else
            {
                resultado = "";
            }
            return resultado;
        }

        public string ui_getDatos(string sidmaesgen, string sclavemaesgen, string dato)
        {
            string query;
            string resultado = string.Empty;

            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();

            query = "SELECT clavemaesgen,desmaesgen,parm1maesgen,parm2maesgen,parm3maesgen ";
            query = query + " FROM maesgen WHERE idmaesgen='" + @sidmaesgen +
                  "' and clavemaesgen='" + @sclavemaesgen + "';";

            try
            {
                SqlCommand myCommand = new SqlCommand(query, conexion);
                SqlDataReader myReader = myCommand.ExecuteReader();

                if (myReader.Read())
                {

                    if (dato.Equals("DESCRIPCION"))
                    {
                        resultado = myReader.GetString(myReader.GetOrdinal("desmaesgen"));
                    }

                    if (dato.Equals("PARM1"))
                    {
                        resultado = myReader.GetString(myReader.GetOrdinal("parm1maesgen"));
                    }

                    if (dato.Equals("PARM2"))
                    {
                        resultado = myReader.GetString(myReader.GetOrdinal("parm2maesgen"));
                    }

                    if (dato.Equals("PARM3"))
                    {
                        resultado = myReader.GetString(myReader.GetOrdinal("parm3maesgen"));
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
            return resultado;

        }

        public string ui_getDatos(string sidmaesgen, string sclavemaesgen, string desmaesgen, string abrevia, string parm1maesgen, string parm2maesgen, string parm3maesgen, string dato)
        {
            string query;
            string resultado = string.Empty;

            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();

            query = " SELECT clavemaesgen,desmaesgen,parm1maesgen,parm2maesgen,parm3maesgen ";
            query += "FROM maesgen WHERE idmaesgen='" + @sidmaesgen + "'";
            if (sclavemaesgen != string.Empty)
            {
                query += "AND clavemaesgen='" + @sclavemaesgen + "'";
            }
            if (desmaesgen != string.Empty)
            {
                query += "AND desmaesgen='" + @desmaesgen + "'";
            }
            if (abrevia != string.Empty)
            {
                query += "AND abrevia='" + @abrevia + "'";
            }
            if (parm1maesgen != string.Empty)
            {
                query += "AND parm1maesgen='" + @parm1maesgen + "'";
            }
            if (parm2maesgen != string.Empty)
            {
                query += "AND parm2maesgen='" + @parm2maesgen + "'";
            }
            if (parm3maesgen != string.Empty)
            {
                query += "AND parm3maesgen='" + @parm3maesgen + "'";
            }

            try
            {
                SqlCommand myCommand = new SqlCommand(query, conexion);
                SqlDataReader myReader = myCommand.ExecuteReader();

                if (myReader.Read())
                {

                    if (dato.Equals("DESCRIPCION"))
                    {
                        resultado = myReader.GetString(myReader.GetOrdinal("desmaesgen"));
                    }

                    if (dato.Equals("PARM1"))
                    {
                        resultado = myReader.GetString(myReader.GetOrdinal("parm1maesgen"));
                    }

                    if (dato.Equals("PARM2"))
                    {
                        resultado = myReader.GetString(myReader.GetOrdinal("parm2maesgen"));
                    }

                    if (dato.Equals("PARM3"))
                    {
                        resultado = myReader.GetString(myReader.GetOrdinal("parm3maesgen"));
                    }

                    if (dato.Equals("ABREV"))
                    {
                        resultado = myReader.GetString(myReader.GetOrdinal("abrevia"));
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
            return resultado;

        }
    }
}