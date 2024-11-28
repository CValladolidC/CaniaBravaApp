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
    class Alstock
    {
        public void recalcularStockAlmacen(string codcia, string alma)
        {
            string query;
            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();
            query = " DELETE from alstock WHERE /*codcia='" + @codcia + "' and */alma='" + @alma + "' ;";
            query = query + " DELETE from alsklote WHERE /*codcia='" + @codcia + "' and */alma='" + @alma + "' ;";
            query = query + " INSERT INTO alstock (codcia,alma,codarti,stock) ";
            query = query + " SELECT codcia,alma,codarti,round(SUM(cantidad),3) as cantidad from vista_almov ";
            query = query + " WHERE /*codcia='" + @codcia + "' and */alma='" + @alma + "' and situa='F' group by codcia,alma,codarti ;";
            query = query + " INSERT INTO alsklote (codcia,alma,codarti,lote,stock) ";
            query = query + " SELECT codcia,alma,codarti,lote,round(SUM(cantidad),3) as cantidad from vista_almov ";
            query = query + " WHERE /*codcia='" + @codcia + "' and */alma='" + @alma + "' and situa='F' group by codcia,alma,codarti,lote ;";
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
            //MessageBox.Show("Recálculo Finalizado", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            conexion.Close();
        }

        public void recalcularStockProducto(string codcia, string alma, string codarti)
        {
            string query;
            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();
            query = " DELETE from alstock WHERE /*codcia='" + @codcia + "' and */alma='" + @alma + "' and codarti='" + @codarti + "' ;";
            query = query + " DELETE from alsklote WHERE /*codcia='" + @codcia + "' and */alma='" + @alma + "' and codarti='" + @codarti + "';";
            query = query + " INSERT INTO alstock (codcia,alma,codarti,stock) ";
            query = query + " SELECT codcia,alma,codarti,SUM(round(cantidad,3)) from vista_almov ";
            query = query + " WHERE /*codcia='" + @codcia + "' and */alma='" + @alma + "' and codarti='" + @codarti + "' and situa='F' group by codcia,alma,codarti ;";
            query = query + " INSERT INTO alsklote (codcia,alma,codarti,lote,stock) ";
            query = query + " SELECT codcia,alma,codarti,lote,SUM(round(cantidad,3)) from vista_almov ";
            query = query + " WHERE /*codcia='" + @codcia + "' and */alma='" + @alma + "' and codarti='" + @codarti + "' and situa='F' group by codcia,alma,codarti,lote ;";
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

        public string ui_getStock(string codcia, string alma, string codarti)
        {
            string query;
            string resultado = "0";
            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();
            query = "EXEC ui_getStock '" + @codcia + "','" + @alma + "','" + @codarti + "';";
            //query = "Select CASE ISNULL(SUM(stock)) WHEN 1 THEN 0 WHEN 0 THEN SUM(round(stock,3)) END ";
            //query = query + " as stock from alstock where codcia='"+@codcia+"' and alma='" + @alma + "' and codarti='" + @codarti + "';";
            try
            {
                SqlCommand myCommand = new SqlCommand(query, conexion);
                SqlDataReader myReader = myCommand.ExecuteReader();
                if (myReader.Read())
                {
                    resultado = myReader.GetDouble(myReader.GetOrdinal("stock")).ToString();
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

        public string ui_getStockLote(string codcia, string alma, string codarti, string lote)
        {
            string query;
            string resultado = "0";
            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();

            query = "Select CASE ISNULL(SUM(stock)) WHEN 1 THEN 0 WHEN 0 THEN SUM(round(stock,3)) END ";
            query = query + " as stock from alsklote where codcia='" + @codcia + "' and alma='" + @alma + "' ";
            query = query + " and codarti='" + @codarti + "' and lote='" + @lote + "';";

            try
            {
                SqlCommand myCommand = new SqlCommand(query, conexion);
                SqlDataReader myReader = myCommand.ExecuteReader();
                if (myReader.Read())
                {
                    resultado = myReader.GetString(myReader.GetOrdinal("stock"));
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
        public double ui_getStockLotef(string codcia, string alma, string codarti, string lote)
        {
            string query;
            double resultado = 0.000;
            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();

            query = "Select CASE ISNULL(SUM(stock),0) WHEN 0 THEN 0 ELSE SUM(round(stock,3)) END ";
            query = query + " as stock from alsklote where /*codcia='" + @codcia + "' and */alma='" + @alma + "' ";
            query = query + " and codarti='" + @codarti + "' and lote='" + @lote + "';";

            try
            {
                SqlCommand myCommand = new SqlCommand(query, conexion);
                SqlDataReader myReader = myCommand.ExecuteReader();
                if (myReader.Read())
                {
                    resultado = myReader.GetDouble(myReader.GetOrdinal("stock"));
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

        public string ui_validaMetodoValoracion(string codcia, string alma, string codarti, string lote, string metodo)
        {
            string query;
            string loteSeleccionado = string.Empty;
            DataTable dtLote = new DataTable();

            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();
            string funcionsql = string.Empty;

            if (metodo.Equals("FIFO"))
            {
                funcionsql = " min(A.fecdoc) ";
            }
            else
            {
                if (metodo.Equals("LIFO"))
                {
                    funcionsql = " max(A.fecdoc) ";
                }

            }

            query = " Select A.lote from vista_lotes A left join alsklote B on A.alma=B.alma ";
            query = query + " and A.codcia=B.codcia and A.alma=B.alma and A.codarti=B.codarti and A.lote=B.lote ";
            query = query + " where A.codcia='" + @codcia + "' and A.alma='" + @alma + "'  ";
            query = query + " and A.codarti='" + @codarti + "' and A.fecdoc in ( Select " + funcionsql;
            query = query + " from vista_lotes A left join alsklote B on A.alma=B.alma ";
            query = query + " and A.codcia=B.codcia and A.alma=B.alma and A.codarti=B.codarti and A.lote=B.lote ";
            query = query + " where A.codcia='" + @codcia + "' and A.alma='" + @alma + "' ";
            query = query + " and A.codarti='" + @codarti + "' and B.stock>0 )";


            try
            {
                SqlDataAdapter dadetalle = new SqlDataAdapter();
                dadetalle.SelectCommand = new SqlCommand(query, conexion);
                dadetalle.Fill(dtLote);
                if (dtLote.Rows.Count > 0)
                {
                    foreach (DataRow row_lote in dtLote.Rows)
                    {
                        loteSeleccionado = row_lote["lote"].ToString();
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            conexion.Close();
            return loteSeleccionado;
        }


        public string ui_getStockAnterior(string codcia, string alma, string codarti, string fecha)
        {
            string query;
            string resultado = "0";

            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();
            query = "Select CASE ISNULL(SUM(cantidad)) WHEN 1 THEN 0 WHEN 0 THEN SUM(round(cantidad,3)) END ";
            query = query + " as stock from vista_almov where codcia='" + @codcia + "' and alma='" + @alma + "' and situa='F' ";
            query = query + " and codarti='" + @codarti + "' and fecdoc<STR_TO_DATE('" + @fecha + "', '%d/%m/%Y');";
            try
            {
                SqlCommand myCommand = new SqlCommand(query, conexion);
                SqlDataReader myReader = myCommand.ExecuteReader();
                if (myReader.Read())
                {
                    resultado = myReader.GetString(myReader.GetOrdinal("stock"));
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

        public string ui_getStockEnFecha(string codcia, string alma, string codarti, string fecha)
        {
            string query;
            string resultado = "0";
            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();
            query = "call ui_getStockEnFecha('" + @codcia + "','" + @alma + "','" + @codarti + "','" + @fecha + "')";
            //query = "Select CASE ISNULL(SUM(cantidad)) WHEN 1 THEN 0 WHEN 0 THEN SUM(round(cantidad,3)) END ";
            //query = query + " as stock from vista_almov where codcia='"+@codcia+"' and alma='" + @alma + "' and situa<>'A' ";
            //query = query + " and codarti='" + @codarti + "' and fecdoc<=STR_TO_DATE('" + @fecha + "', '%d/%m/%Y');";
            try
            {
                SqlCommand myCommand = new SqlCommand(query, conexion);
                SqlDataReader myReader = myCommand.ExecuteReader();
                if (myReader.Read())
                {
                    resultado = myReader.GetString(myReader.GetOrdinal("stock"));
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


        public string ui_getResMovEnRangoFecha(string codcia, string alma, string td, string codarti,
            string fechaini, string fechafin)
        {
            string query;
            string resultado = "0";

            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();
            query = "call alks_getResMovEnRangoFecha('" + @codcia + "','" + @alma + "','" + @codarti + "','" + @td + "','" + @fechaini + "','" + @fechafin + "')";
            //query = " Select CASE ISNULL(SUM(cantidad)) WHEN 1 THEN 0 WHEN 0 THEN SUM(round(cantidad,3)) END  as stock";
            //query = query + " from almovc A inner join almovd B ";
            //query = query + " on A.codcia=B.codcia and A.alma=B.alma and A.td=B.td and A.numdoc=B.numdoc ";
            //query = query + " left join alarti C on ";
            //query = query + " B.codcia=C.codcia and B.codarti=C.codarti left join provee D on A.codpro=D.codprovee ";
            //query = query + " where A.codcia='"+@codcia+"' and A.alma='" + @alma + "' and B.codarti='" + @codarti + "' ";
            //query = query + " and A.td='"+@td+"' and A.fecdoc >= ";
            //query = query + " STR_TO_DATE('" + @fechaini + "', '%d/%m/%Y')  and  ";
            //query = query + " A.fecdoc <= STR_TO_DATE('" + @fechafin + "', '%d/%m/%Y'); ";

            try
            {
                SqlCommand myCommand = new SqlCommand(query, conexion);
                SqlDataReader myReader = myCommand.ExecuteReader();
                if (myReader.Read())
                {
                    resultado = myReader.GetString(myReader.GetOrdinal("stock"));
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

        public string ui_getResTotCosEnRangoFecha(string codcia, string alma, string td, string codarti,
            string fechaini, string fechafin)
        {
            string query;
            string resultado = "0";

            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();

            query = " Select CASE ISNULL(SUM(totcosuni)) WHEN 1 THEN 0 WHEN 0 THEN SUM(round(totcosuni,3)) END  as totcosuni";
            query = query + " from almovc A inner join almovd B ";
            query = query + " on A.codcia=B.codcia and A.alma=B.alma and A.td=B.td and A.numdoc=B.numdoc ";
            query = query + " left join alarti C on ";
            query = query + " B.codcia=C.codcia and B.codarti=C.codarti left join provee D on A.codpro=D.codprovee ";
            query = query + " where A.codcia='" + @codcia + "' and A.alma='" + @alma + "' and B.codarti='" + @codarti + "' ";
            query = query + " and A.td='" + @td + "' and A.fecdoc >= ";
            query = query + " STR_TO_DATE('" + @fechaini + "', '%d/%m/%Y')  and  ";
            query = query + " A.fecdoc <= STR_TO_DATE('" + @fechafin + "', '%d/%m/%Y'); ";

            try
            {
                SqlCommand myCommand = new SqlCommand(query, conexion);
                SqlDataReader myReader = myCommand.ExecuteReader();
                if (myReader.Read())
                {
                    resultado = myReader.GetString(myReader.GetOrdinal("totcosuni"));
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