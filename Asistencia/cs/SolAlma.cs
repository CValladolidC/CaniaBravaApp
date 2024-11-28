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
    class SolAlma
    {
        public SolAlma() { }

        public string genSolAlma(string codcia, string alma, string secsoli)
        {
            Funciones funciones = new Funciones();
            string codigoInterno = string.Empty;
            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();
            string query = " select isnull(max(solalma),'1') as existencia,";
            query += " max(solalma)+1 as codigointerno from solalmac ";
            query += " where alma='" + @alma + "' /*and codcia='" + @codcia + "' and secsoli='" + @secsoli + "'*/;";
            try
            {
                SqlCommand myCommand = new SqlCommand(query, conexion);
                SqlDataReader myReader = myCommand.ExecuteReader();
                if (myReader.Read())
                {
                    if (myReader.GetString(myReader.GetOrdinal("existencia")).Trim().Equals("1"))
                    {
                        codigoInterno = "000001";
                    }
                    else
                    {
                        string codigo = myReader.GetInt32(myReader.GetOrdinal("codigointerno")).ToString();
                        codigoInterno = funciones.replicateCadena("0", 6 - codigo.Trim().Length) + codigo;
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

        public string getNumeroRegistros(string codcia, string alma, string secsoli)
        {
            string numero = "0";
            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();
            string query = "select count(*) as numero from solalmac where alma='" + @alma + "' /*and codcia='" + @codcia + "' and secsoli='" + @secsoli + "'*/;";
            try
            {
                SqlCommand myCommand = new SqlCommand(query, conexion);
                SqlDataReader myReader = myCommand.ExecuteReader();

                if (myReader.Read())
                {
                    numero = myReader.GetInt32(myReader.GetOrdinal("numero")).ToString();
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            conexion.Close();
            return numero;
        }

        public void updSolAlma(string operacion, string codcia, string alma, string secsoli, string solalma,
            string fecha, string hora, string codsoli, string nomrec, string dessoli1, string dessoli2,
            string dessoli3, string usuario, string estado, string titulo)
        {
            string query;
            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();
            if (operacion.Equals("AGREGAR"))
            {
                query = " INSERT INTO solalmac (codcia,alma,secsoli,solalma,fecha,hora,codsoli,";
                query = query + " nomrec,dessoli1,dessoli2,dessoli3,usuario,estado,titulo ) ";
                query = query + " VALUES ('" + @codcia + "','" + @alma + "', '" + @secsoli + "', '" + @solalma + "', ";
                query = query + " '" + @fecha + "', '" + @hora + "', '" + @codsoli + "', ";
                query = query + " '" + @nomrec + "', '" + @dessoli1 + "', '" + @dessoli2 + "', ";
                query = query + " '" + @dessoli3 + "','" + @usuario + "','" + @estado + "','" + @titulo + "'); ";
            }
            else
            {
                query = " UPDATE solalmac SET codsoli='" + @codsoli + "',nomrec='" + @nomrec + "',";
                query += " dessoli1='" + @dessoli1 + "',dessoli2='" + @dessoli2 + "', ";
                query += " dessoli3='" + @dessoli3 + "',usuario='" + @usuario + "', ";
                query += " estado='" + @estado + "',titulo='" + @titulo + "' ";
                query += " WHERE codcia='" + @codcia + "' and alma='" + @alma + "' and secsoli='" + @secsoli + "' ";
                query += " and solalma='" + @solalma + "' ;";
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

        public void updFinSolAlma(string codcia, string alma, string secsoli, string solalma,
            string fecha, string hora, string usuario)
        {
            string query;
            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();
            query = " UPDATE solalmac SET fecha= '" + @fecha + "',";
            query += " hora='" + @hora + "',usuario='" + @usuario + "', estado='F' ";
            query += " WHERE alma='" + @alma + "' and solalma='" + @solalma + "' ";
            //query += " and codcia='" + @codcia + "' and secsoli='" + @secsoli + "' ;";
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

        public void updDesFinSolAlma(string codcia, string alma, string secsoli, string solalma)
        {
            string query;
            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();
            query = " UPDATE solalmac SET estado='V' ";
            query = query + " WHERE /*codcia='" + @codcia + "' and secsoli='" + @secsoli + "' and */alma='" + @alma + "' ";
            query = query + " and solalma='" + @solalma + "' ;";
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

        public void updVBSolAlma(string codcia, string alma, string secsoli, string solalma, string usuario,
            string fecha, string hora, string tipo)
        {
            string query;
            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();

            if (tipo.Equals("AGREGA"))
            {
                query = " UPDATE solalmac SET fecha_vb='" + @fecha + "', ";
                query = query + " hora_vb='" + @hora + "',vb='" + @usuario + "' ";
                query = query + " WHERE codcia='" + @codcia + "' and alma='" + @alma + "' and secsoli='" + @secsoli + "' ";
                query = query + " and solalma='" + @solalma + "' ;";
            }
            else
            {
                query = " UPDATE solalmac SET fecha_vb='',";
                query = query + " hora_vb='',vb='' ";
                query = query + " WHERE codcia='" + @codcia + "' and alma='" + @alma + "' and secsoli='" + @secsoli + "' ";
                query = query + " and solalma='" + @solalma + "' ;";
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

        public void delSolAlma(string codcia, string alma, string secsoli, string solalma)
        {
            string query;
            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();
            query = "DELETE from solalmad WHERE alma='" + @alma + "' and solalma='" + @solalma + "'; ";
            //query += "and codcia='" + @codcia + "' and secsoli='" + @secsoli + "' ;";
            query += "DELETE from solalmac WHERE solalma='" + @solalma + "'and alma='" + @alma + "'; ";
            //query += " and codcia='" + @codcia + "' and secsoli='" + @secsoli + "' ; ";
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

        public string imprime(string codcia, string alma, string secsoli, string solalma)
        {
            string lineatitulo1 = string.Empty;
            string lineatitulo2 = string.Empty;
            string lineatitulo3 = string.Empty;
            string strItem = string.Empty;
            string strPrinter = string.Empty;

            DataTable dtsol = new DataTable();

            string query = " SELECT C.descia,C.ruccia,F.desalma,D.desmaesgen as secsoli,E.dessoli as soli,";
            query = query + " A.solalma,A.fecha,A.hora,A.nomrec,A.dessoli1,A.dessoli2,A.dessoli3 ";
            query = query + " ,A.titulo,A.vb,A.fecha_vb,A.hora_vb,B.item,B.codarti,B.desarti,B.unidad,";
            query = query + " B.cantidad,B.glosa1,B.glosa2,B.glosa3 from solalmac A ";
            query = query + " left join solalmad B on /*A.codcia=B.codcia and */";
            query = query + " A.alma=B.alma and A.secsoli=B.secsoli and A.solalma=B.solalma left join";
            query = query + " ciafile C on A.codcia=C.idcia left join maesgen D on D.idmaesgen='150' and ";
            query = query + " A.secsoli=D.clavemaesgen left join solicita E on A.codcia=E.codcia";
            query = query + " and A.secsoli=E.secsoli and A.codsoli=E.codsoli";
            query = query + " left join alalma F on A.codcia=F.codcia and A.alma=F.codalma ";
            query = query + " WHERE /*A.codcia='" + @codcia + "' and A.secsoli='" + @secsoli + "' and*/ ";
            query = query + " A.alma='" + @alma + "' and A.solalma='" + @solalma + "' ";
            query = query + " ORDER BY B.item asc ";

            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();
            SqlDataAdapter da = new SqlDataAdapter();

            da.SelectCommand = new SqlCommand(query, conexion);
            da.Fill(dtsol);

            if (dtsol.Rows.Count > 0)
            {
                Funciones funciones = new Funciones();
                int fila = 0;

                foreach (DataRow row_sol in dtsol.Rows)
                {
                    fila++;
                    if (fila.Equals(1))
                    {
                        strPrinter = " " + row_sol["descia"].ToString() + "\n";
                        strPrinter = strPrinter + " " + row_sol["ruccia"].ToString() + "\n";
                        strPrinter = strPrinter + " S O L I C I T U D    D E   I N S U M O S " + "\n\n";
                        strPrinter = strPrinter + " AREA                :  " + row_sol["secsoli"].ToString() + "\n";
                        strPrinter = strPrinter + " COMEDOR             :  " + row_sol["desalma"].ToString() + "\n";
                        strPrinter = strPrinter + " SOLICITANTE         :  " + row_sol["nomrec"].ToString() + "\n";
                        strPrinter = strPrinter + " FECHA SOLICITUD     :  " + (row_sol["fecha"].ToString() + funciones.replicateCadena(" ", 10)).Substring(0, 10) + funciones.replicateCadena(" ", 2) + row_sol["hora"].ToString() + "\n";
                        strPrinter = strPrinter + " SOLICITAR AUT.      :  " + row_sol["soli"].ToString() + "\n";
                        strPrinter = strPrinter + " FECHA AUT.          :  " + row_sol["vb"].ToString() + "  " + (row_sol["fecha_vb"].ToString() + funciones.replicateCadena(" ", 10)).Substring(0, 10) + funciones.replicateCadena(" ", 2) + row_sol["hora_vb"].ToString() + "\n";
                        strPrinter = strPrinter + " GLOSA DESCRIPTIVA   :  " + row_sol["dessoli1"].ToString() + "\n";
                        if (row_sol["dessoli2"].ToString() != "")
                        {
                            strPrinter = strPrinter + "                        " + row_sol["dessoli2"].ToString() + "\n";
                        }
                        if (row_sol["dessoli3"].ToString() != "")
                        {
                            strPrinter = strPrinter + "                        " + row_sol["dessoli3"].ToString() + "\n";
                        }
                        strPrinter = strPrinter + funciones.replicateCadena("-", 95) + "\n";
                        strPrinter = strPrinter + "ITEM" + funciones.replicateCadena(" ", 1) + " CODIGO " + funciones.replicateCadena(" ", 1) + "                         DESCRIPCION                       " + funciones.replicateCadena(" ", 1) + "    CANTIDAD   " + funciones.replicateCadena(" ", 1) + "UND." + "\n";
                        strPrinter = strPrinter + funciones.replicateCadena("-", 95) + "\n\n";

                    }

                    strPrinter = strPrinter + funciones.longitudCadena(row_sol["item"].ToString(), 4) + funciones.replicateCadena(" ", 1) + funciones.longitudCadena(row_sol["codarti"].ToString(), 8) + funciones.replicateCadena(" ", 1) + funciones.longitudCadena(row_sol["desarti"].ToString(), 60) + funciones.replicateCadena(" ", 1) + funciones.alineacionNumero(float.Parse(row_sol["cantidad"].ToString()).ToString("###,###,##0.00;(###,###,##0.00);Zero"), 15) + funciones.replicateCadena(" ", 1) + funciones.longitudCadena(row_sol["unidad"].ToString(), 4) + "\n";

                    if (row_sol["glosa1"].ToString() != "")
                    {
                        strPrinter = strPrinter + funciones.replicateCadena(" ", 14) + funciones.longitudCadena(row_sol["glosa1"].ToString(), 60) + "\n";
                    }
                    if (row_sol["glosa2"].ToString() != "")
                    {
                        strPrinter = strPrinter + funciones.replicateCadena(" ", 14) + funciones.longitudCadena(row_sol["glosa2"].ToString(), 60) + "\n";
                    }
                    if (row_sol["glosa3"].ToString() != "")
                    {
                        strPrinter = strPrinter + funciones.replicateCadena(" ", 14) + funciones.longitudCadena(row_sol["glosa3"].ToString(), 60) + "\n";
                    }
                }

                strPrinter = strPrinter + funciones.replicateCadena("-", 95) + "\n\n\n\n\n\n";
                strPrinter = strPrinter + funciones.replicateCadena(" ", 10) + funciones.replicateCadena("-", 20) + funciones.replicateCadena(" ", 30) + funciones.replicateCadena("-", 20) + "\n";
                strPrinter = strPrinter + funciones.replicateCadena(" ", 10) + "     SOLICITANTE    " + funciones.replicateCadena(" ", 30) + "       AUTORIZA     " + "\n\n\f";
            }

            return strPrinter;
        }
    }
}