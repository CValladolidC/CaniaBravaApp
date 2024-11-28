using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.OleDb;
using System.IO;
using System.Text.RegularExpressions;

/*
 * DatabaseToDbf.cs
 * Servicios que permiten el traspaso de información de una base de datos a un fichero .dbf
 * 
 * alskare, Jul/09
 */

namespace CaniaBrava
{
    internal class DatabaseToDbf
    {
        private DbProviderFactory factoria;
        private string cadenaConexion;
        private string sql;
        private string pathSalida;
        private string ficheroSalida;
        private string nombreTabla;

        private List<string> campos = new List<string>();
        private List<string> parametros = new List<string>();
        private List<string> nombreCampos = new List<string>();

        private string strConnDbf;

        /// <summary>
        /// Constructor de la clase
        /// </summary>
        /// <param name="factoria">Factoría del origen de los datos</param>
        /// <param name="cadenaConexion">Cadena de conexión a la base de datos de origen</param>
        /// <param name="sql">Instrucción Select SQL para exportar a dbf</param>
        /// <param name="ficheroSalida">Nombre completo del fichero de salida</param>
        public DatabaseToDbf(DbProviderFactory factoria, string cadenaConexion, string sql, string ficheroSalida)
        {
            this.factoria = factoria;
            this.cadenaConexion = cadenaConexion;
            this.sql = sql;

            // En base al fichero, se desglosa la información para usarla más tarde.
            FileInfo f = new FileInfo(ficheroSalida);
            pathSalida = f.DirectoryName;
            this.ficheroSalida = f.FullName;
            nombreTabla = new Regex(f.Extension + "$").Replace(f.Name, "");

            // Creación de la cadena de conexión al fichero .dbf
            strConnDbf = @"Provider = Microsoft.Jet.OLEDB.4.0" +
                                   ";Data Source = " + pathSalida +
                                   ";Extended Properties = dBASE IV" +
                                   ";User ID=Admin;Password=;";
        }

        /// <summary>
        /// Realiza la exportación de la base de datos de origen a un fichero con formato .dbf.
        /// </summary>
        /// <returns>True si el traspaso ha sido correcto</returns>
        public bool Exporta()
        {
            bool retVal = false;
            try
            {
                LeeEstructura();
                CreaTabla();
                InsertaDatos();

                // Se ha llegado hasta aquí, se supone que está todo bien
                retVal = true;
            }
            catch
            {
                retVal = false;
            }
            return retVal;
        }

        /// <summary>
        /// Lectura de la estructura de la tabla de origen
        /// </summary>
        private void LeeEstructura()
        {
            using (DbConnection cn = factoria.CreateConnection())
            {
                cn.ConnectionString = cadenaConexion;
                using (DbCommand cmd = cn.CreateCommand())
                {
                    cmd.Connection = cn;
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = sql;

                    DbDataAdapter da = factoria.CreateDataAdapter();
                    da.SelectCommand = cmd;

                    DataTable dt = new DataTable();
                    da.FillSchema(dt, SchemaType.Mapped);

                    foreach (DataColumn columna in dt.Columns)
                    {
                        Campo c = new Campo(columna);
                        // Se crea la lista de parámetros
                        campos.Add(c.Tabla());
                        parametros.Add(c.Parametro());
                        nombreCampos.Add(c.Nombre());

                        c = null;
                    }
                }
            }
        }

        /// <summary>
        /// Creación del fichero DBF.
        /// </summary>
        private void CreaTabla()
        {
            // Se borra el fichero en el caso de que exista
            File.Delete(ficheroSalida);

            using (OleDbConnection cn = new OleDbConnection(strConnDbf))
            {
                using (OleDbCommand cmd = new OleDbCommand())
                {
                    cmd.Connection = cn;
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = GetSql_Create();

                    cn.Open();
                    cmd.ExecuteNonQuery();
                    cn.Close();
                }
            }
        }

        /// <summary>
        /// Realiza el traspaso de datos de la base de datos de origen al fichero dbf.
        /// </summary>
        private void InsertaDatos()
        {
            // Preparación del comando de inserción
            OleDbConnection cnDestino = new OleDbConnection(strConnDbf);

            OleDbCommand cmdInserta = new OleDbCommand();
            cmdInserta.CommandType = CommandType.Text;
            cmdInserta.CommandText = GetSql_Insert();
            cmdInserta.Connection = cnDestino;

            // Parámetros 
            for (int i = 0; i < parametros.Count; i++)
            {
                // Añado los parámetros con un valor nulo. Luego se cambiará. 
                cmdInserta.Parameters.Add(new OleDbParameter(parametros[i], DBNull.Value));
            }

            // Se hace un recorrido por los datos de origen
            using (DbConnection cnOrigen = factoria.CreateConnection())
            {
                cnOrigen.ConnectionString = cadenaConexion;
                using (DbCommand cmd = cnOrigen.CreateCommand())
                {
                    cmd.Connection = cnOrigen;
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = sql;

                    cnOrigen.Open();
                    DbDataReader dr = cmd.ExecuteReader();
                    cnDestino.Open();

                    while (dr.Read())
                    {
                        for (int i = 0; i < nombreCampos.Count; i++)
                        {
                            // Se establece el valor del parámetro
                            cmdInserta.Parameters[i].Value = dr[i];
                        }

                        // Ejecución de la inserción en dbf
                        cmdInserta.ExecuteNonQuery();
                    }
                    dr.Close();
                    cnDestino.Close();
                    cnOrigen.Close();
                }
            }
        }

        /// <summary>
        /// Devuelve la instrucción SQL que permitirá la inserción de los datos
        /// </summary>
        private string GetSql_Insert()
        {
            return "INSERT INTO " + nombreTabla +
                    string.Concat("(", string.Join(",", nombreCampos.ToArray()), ")")
                    + " VALUES " + string.Concat("(", string.Join(",", parametros.ToArray()), ")")
                    ;
        }

        /// <summary>
        /// Obtiene la instrucción SQL que permitirá la creación de la tabla
        /// </summary>
        private string GetSql_Create()
        {
            return "CREATE TABLE " + nombreTabla +
                string.Concat("(", string.Join(", ", campos.ToArray()), ")");

        }
    }
}