using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace CaniaBrava
{
    class AlmovD
    {
        public string genCod(string codcia, string alma, string td, string numdoc)
        {
            Funciones funciones = new Funciones();
            string codigoInterno = string.Empty;
            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();
            string query = "select isnull(max(item),'1') as existencia,";
            query = query + "max(item)+1 as codigointerno from almovd where /*codcia='" + @codcia + "' and */alma='" + @alma + "' and ";
            query = query + "td='" + @td + "' and numdoc='" + @numdoc + "' ;";
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

        public void updAlmovD(string operacion, string codcia, string alma, string td, string numdoc,
        string item, string codarti, float cantidad, string certificado, string lote, string fprod,
            string fven, string analisis, string calidad, string glosana1, string glosana2,
            string glosana3, string codcenpro, string tiposec, string codseccion, string loteProduccion)
        {
            string query;
            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();
            if (operacion.Equals("AGREGAR"))
            {
                query = "INSERT INTO almovd (codcia,alma,td,numdoc, ";
                query = query + " item,codarti,cantidad,certificado,lote,fprod,fven,";
                query = query + " analisis,calidad,glosana1,glosana2,glosana3,codcenpro,tiposec,codseccion,lotep) ";
                query = query + " VALUES ('" + @codcia + "','" + @alma + "', '" + @td + "', '" + @numdoc + "', ";
                query = query + " '" + @item + "', '" + @codarti + "', '" + @cantidad + "', ";
                query = query + " '" + @certificado + "', '" + @lote + "', '" + @fprod + "', ";
                query = query + " '" + @fven + "','" + @analisis + "','" + @calidad + "', ";
                query = query + " '" + @glosana1 + "', '" + @glosana2 + "', '" + @glosana3 + "','" + @codcenpro + "',";
                query = query + " '" + @tiposec + "','" + @codseccion + "','" + @loteProduccion + "'); ";
            }
            else
            {
                query = "UPDATE almovd SET fprod='" + @fprod + "',codarti='" + @codarti + "',";
                query = query + " cantidad='" + @cantidad + "',certificado='" + @certificado + "', ";
                query = query + " lote='" + @lote + "',fven='" + @fven + "', ";
                query = query + " analisis='" + @analisis + "',calidad='" + @calidad + "', ";
                query = query + " glosana1='" + @glosana1 + "',glosana2='" + @glosana2 + "', ";
                query = query + " glosana3='" + @glosana3 + "',codcenpro='" + @codcenpro + "',";
                query = query + " tiposec='" + @tiposec + "',codseccion='" + @codseccion + "', ";
                query = query + " lotep='" + @loteProduccion + "' ";
                query = query + " WHERE /*codcia='" + @codcia + "' and */alma='" + @alma + "' and td='" + @td + "' ";
                query = query + " and numdoc='" + @numdoc + "' and item='" + @item + "';";
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

        public string getCosUniAlmovD(string codcia, string alma, string lote)
        {
            string resultado = "0";
            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();
            string query = " Select A.precosuni from almovd A ";
            query = query + " where A.codcia='" + @codcia + "' and A.alma='" + @alma + "' ";
            query = query + " and A.td='PE' and A.lote='" + @lote + "' ";

            try
            {
                SqlCommand myCommand = new SqlCommand(query, conexion);
                SqlDataReader myReader = myCommand.ExecuteReader();

                if (myReader.Read())
                {
                    resultado = myReader.GetString(myReader.GetOrdinal("precosuni"));
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


        public void updCosUniAlmovD(string codcia, string alma, string td, string lote, string precosuni)
        {
            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();
            string query = "UPDATE almovd SET precosuni='" + @precosuni + "' ,totcosuni=(cantidad * '" + @precosuni + "') ";
            query = query + " WHERE codcia='" + @codcia + "' and alma='" + @alma + "' and td='" + @td + "' and lote='" + @lote + "'; ";

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

        public void updCosUni(string codcia, string alma, string td, string numdoc, string item, string precosuni)
        {
            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();
            string query = " UPDATE almovd SET precosuni='" + @precosuni + "' ,totcosuni=(cantidad * '" + @precosuni + "') ";
            query = query + " WHERE codcia='" + @codcia + "' and alma='" + @alma + "' and ";
            query = query + " td='" + @td + "' and numdoc='" + @numdoc + "' and item='" + @item + "'; ";
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


        public void updAlmovDFac(string operacion, string codcia, string alma, string td, string numdoc,
        string item, string codarti, string cantidad, string preuni, string preneto,
            string desc, string neto, string igv, string total, string lote)
        {
            string query;
            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();
            if (operacion.Equals("AGREGAR"))
            {
                query = "INSERT INTO almovd (codcia,alma,td,numdoc, ";
                query = query + " item,codarti,cantidad,preuni,preneto,descuento,neto,igv,total,lote) ";
                query = query + " VALUES ('" + @codcia + "','" + @alma + "', '" + @td + "', '" + @numdoc + "', ";
                query = query + " '" + @item + "', '" + @codarti + "', '" + @cantidad + "', ";
                query = query + " '" + @preuni + "', '" + @preneto + "', '" + @desc + "', ";
                query = query + " '" + @neto + "','" + @igv + "','" + @total + "','" + @lote + "'); ";
            }
            else
            {
                query = "UPDATE almovd SET cantidad='" + @cantidad + "',preuni='" + @preuni + "',";
                query = query + " preneto='" + @preneto + "',descuento='" + @desc + "', ";
                query = query + " neto='" + @neto + "',igv='" + @igv + "', ";
                query = query + " total='" + @total + "' ";
                query = query + " WHERE codcia='" + @codcia + "' and alma='" + @alma + "' and td='" + @td + "' ";
                query = query + " and numdoc='" + @numdoc + "' and item='" + @item + "';";
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


        public void updAlmovDRecibe(string codcia, string alma, string td, string numdoc,
        string item, string cantrecibe)
        {
            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();
            string query = " UPDATE almovd SET cantrecibe='" + @cantrecibe + "' ";
            query = query + " WHERE codcia='" + @codcia + "' and alma='" + @alma + "' and td='" + @td + "' ";
            query = query + " and numdoc='" + @numdoc + "' and item='" + @item + "';";
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


        public void updAlmovDGuia(string operacion, string codcia, string alma, string td, string numdoc,
        string item, string codarti, string cantidad, string pesouni, string pesototal, string lote)
        {
            string query;
            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();
            if (operacion.Equals("AGREGAR"))
            {
                query = "INSERT INTO almovd (codcia,alma,td,numdoc, ";
                query = query + " item,codarti,cantidad,pesouni,pesototal,lote) ";
                query = query + " VALUES ('" + @codcia + "','" + @alma + "', '" + @td + "', '" + @numdoc + "', ";
                query = query + " '" + @item + "', '" + @codarti + "', '" + @cantidad + "', ";
                query = query + " '" + @pesouni + "', '" + @pesototal + "','" + @lote + "'); ";
            }
            else
            {
                query = "UPDATE almovd SET cantidad='" + @cantidad + "',pesouni='" + @pesouni + "',";
                query = query + " pesototal='" + @pesototal + "' ";
                query = query + " WHERE codcia='" + @codcia + "' and alma='" + @alma + "' and td='" + @td + "' ";
                query = query + " and numdoc='" + @numdoc + "' and item='" + @item + "';";
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

        public void updValAlmovD(string codcia, string alma, string td, string numdoc,
        string item, float preuni, float neto, float igv, float total)
        {
            string query;
            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();

            query = "UPDATE almovd SET preuni='" + @preuni + "', ";
            query = query + " neto='" + @neto + "',igv='" + @igv + "', ";
            query = query + " total='" + @total + "' ";
            query = query + " WHERE codcia='" + @codcia + "' and alma='" + @alma + "' and td='" + @td + "' ";
            query = query + " and numdoc='" + @numdoc + "' and item='" + @item + "';";

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

        public void delAlmovD(string codcia, string alma, string td, string numdoc, string item)
        {
            string query;
            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();
            query = "DELETE from almovd WHERE /*codcia='" + @codcia + "' and */alma='" + @alma + "' and td='" + @td + "' ";
            query = query + "and numdoc='" + @numdoc + "' and item='" + @item + "';";
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