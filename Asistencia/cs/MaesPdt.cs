using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Configuration;
using System.Windows.Forms;
using System.Data;

namespace CaniaBrava
{
    class MaesPdt
    {
        public MaesPdt() { }

        public void actualizarMaesPdt(string operacion, string rp_cindice, string rp_ccodigo, string rp_cdescri)
        {
            string squery;
            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();

            if (operacion.Equals("AGREGAR"))
            {
                squery = "INSERT INTO tgrpts (rp_cindice,rp_ccodigo,rp_cdescri) VALUES ('" + @rp_cindice + "', '" + @rp_ccodigo + "', '" + @rp_cdescri + "');";
            }
            else
            {
                squery = "UPDATE tgrpts SET rp_cdescri='" + @rp_cdescri + "' WHERE rp_cindice='" + @rp_cindice + "' and rp_ccodigo='"+@rp_ccodigo+"';";
            }
            try
            {
                SqlCommand myCommand = new SqlCommand(squery, conexion);
                myCommand.ExecuteNonQuery();
                myCommand.Dispose();

            }
            catch (SqlException e)
            {
                MessageBox.Show(e.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            conexion.Close();
        }

        public void eliminarMaesPdt(string rp_cindice,string rp_ccodigo)
        {
            string squery;
            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();

            squery = "DELETE from tgrpts WHERE rp_cindice='" + @rp_cindice + "' and rp_ccodigo='"+@rp_ccodigo+"';";

            try
            {
                SqlCommand myCommand = new SqlCommand(squery, conexion);
                myCommand.ExecuteNonQuery();
                myCommand.Dispose();

            }
            catch (SqlException e)
            {
                MessageBox.Show(e.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);

            }

            conexion.Close();

        }

        public string ui_getDatosMaesPdt(string rp_cindice, string rp_ccodigo,string dato)
        {
            string resultado = string.Empty;
            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();
            string query = "SELECT * from tgrpts WHERE rp_cindice='" + @rp_cindice + "' and rp_ccodigo='" + @rp_ccodigo + "';";
            try
            {
                SqlCommand myCommand = new SqlCommand(query, conexion);
                SqlDataReader myReader = myCommand.ExecuteReader();

                if (myReader.Read())
                {
                    if (dato.Equals("DESCRIPCION"))
                    {
                        resultado = myReader["rp_cdescri"].ToString();
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

        public void validarDetMaesPdt(string rp_cindice, ComboBox cb, string rp_ccodigo)
        {
            string valorItem=string.Empty;
            Funciones fn = new Funciones();
            string clave = fn.longitudCadena(rp_ccodigo,4).Trim();
            if (clave != String.Empty)
            {
                SqlConnection conexion = new SqlConnection();
                conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
                conexion.Open();
                string query = "SELECT rp_ccodigo,rp_cdescri,rp_cindice FROM tgrpts WHERE " +
                          "rp_cindice='" + @rp_cindice + "' and rp_ccodigo='" + @clave + "';";
                try
                {
                    SqlCommand cmd = new SqlCommand(query, conexion);
                    DataTable dt = new DataTable();
                    dt.Load(cmd.ExecuteReader());
                    cmd.Dispose();
                    if (dt.Rows.Count > 0)
                    {
                        valorItem = (String)dt.Rows[0]["rp_ccodigo"];
                        cb.Text = valorItem + fn.replicateCadena(" ", (5 - valorItem.Length)) + (String)dt.Rows[0]["rp_cdescri"];
                    }
                    else
                    {
                        MessageBox.Show("Registro no encontrado", "Aviso Importante", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        cb.Text = "";
                    }
                }
                catch (SqlException ex)
                {
                    MessageBox.Show(ex.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
                conexion.Close();
            }
        }

        public bool consultaDetMaesPdt(string rp_cindice, string rp_ccodigo, ComboBox cb)
        {
            bool resultado = false;
            string valorItem;
            Funciones fn = new Funciones();
            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();
            string query = "SELECT rp_ccodigo,rp_cdescri,rp_cindice FROM tgrpts WHERE rp_cindice='" + @rp_cindice+"' ";
            query = query + " and rp_ccodigo='" + @rp_ccodigo + "';";
            try
            {
                SqlCommand cmd = new SqlCommand(query, conexion);
                DataTable dt = new DataTable();
                dt.Load(cmd.ExecuteReader());
                cmd.Dispose();
                if (dt.Rows.Count > 0)
                {
                    valorItem = (String)dt.Rows[0]["rp_ccodigo"];
                    cb.Text = valorItem + fn.replicateCadena(" ", (5 - valorItem.Length)) + (String)dt.Rows[0]["rp_cdescri"];
                    resultado = true;
                }
                else
                {
                    cb.Text = "";
                    resultado = false;
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                resultado = false;
            }
            conexion.Close();

            return resultado;
        }

        public void listaDetMaesPdt(string rp_cindice, ComboBox cb, string tipo)
        {
            string valorItem;
            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();
            string squery_table = "SELECT rp_ccodigo,rp_cdescri FROM tgrpts WHERE rp_cindice='00' ";
            squery_table = squery_table + " AND rp_ccodigo='" + @rp_cindice + "';";
            string squery_detalle = "SELECT rp_ccodigo,rp_cdescri FROM tgrpts WHERE " +
                      " rp_cindice='" + @rp_cindice + "';";
            try
            {
                ToolTip tooltip = new ToolTip();
                tooltip.SetToolTip(cb, "Tabla PDT : " + rp_cindice);
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
                            valorItem = (String)dt.Rows[i]["rp_ccodigo"];
                            valorItem = valorItem + fn.replicateCadena(" ", (5 - valorItem.Length)) + (String)dt.Rows[i]["rp_cdescri"];
                            cb.Items.Add(valorItem);
                        }
                    }
                    else
                    {
                        MessageBox.Show("No existen registros en la Tabla PDT " + rp_cindice, "Aviso Importante", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    }
                }
                else
                {
                    MessageBox.Show("No existe la Tabla PDT " + rp_cindice, "Aviso Importante", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);

            }
            conexion.Close();
        }

        public string verificaComboBoxMaesPdt(string rp_cindice, string cadena)
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
                string query = "SELECT rp_ccodigo,rp_cdescri,rp_cindice FROM tgrpts WHERE rp_cindice='" + @rp_cindice + "' ";
                query = query + " and rp_ccodigo='" + @clave + "';";
                try
                {
                    SqlCommand cmd = new SqlCommand(query, conexion);
                    DataTable dt = new DataTable();
                    dt.Load(cmd.ExecuteReader());
                    cmd.Dispose();
                    if (dt.Rows.Count > 0)
                    {
                        valorItem = (String)dt.Rows[0]["rp_ccodigo"];
                        resultado = valorItem + fn.replicateCadena(" ", (5 - valorItem.Length)) + (String)dt.Rows[0]["rp_cdescri"];
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
    }
}