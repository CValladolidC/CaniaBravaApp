using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Configuration;
using System.Windows.Forms;
using System.Data;
using System.Data.Common;
using System.Security.Cryptography;
using System.IO;

namespace CaniaBrava
{
    public class UsrFile
    {
        public string usuario { get; set; }
        public string desusr { get; set; }
        public string email { get; set; }

        public UsrFile() { }
        public byte[] Clave = Encoding.ASCII.GetBytes("agromar123");
        public byte[] IV = Encoding.ASCII.GetBytes("Devjoker7.37hAES");

        private string getPassUsr(string passUsr)
        {
            string passPhrase = ConfigurationManager.AppSettings.Get("PASS_PHRASE");
            string saltValue = ConfigurationManager.AppSettings.Get("SALT_VALUE");
            string hashAlgorithm = ConfigurationManager.AppSettings.Get("HASH_ALGORITHM");
            int passwordIterations = Convert.ToInt32(ConfigurationManager.AppSettings.Get("PASSWORD_ITERATIONS"));
            string initVector = ConfigurationManager.AppSettings.Get("INIT_VECTOR");
            int keySize = Convert.ToInt32(ConfigurationManager.AppSettings.Get("KEY_SIZE"));

            RijndaelSimple RijndaelSimple = new RijndaelSimple();
            string passEncriptUsrFile = RijndaelSimple.Encrypt(passUsr, passPhrase, saltValue, hashAlgorithm, passwordIterations, initVector, keySize);
            return passEncriptUsrFile;
        }

        public void actualizarUsr(string operacion, string idusr, string desusr, string passusr, string typeusr,
            string stateusr, string mail)
        {
            string query;

            string passmd5 = getPassUsrMD5(passusr);
            passusr = getPassUsr(passusr);

            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();

            if (operacion.Equals("AGREGAR"))
            {
                query = " INSERT INTO usrfile (idusr, desusr,passusr, typeusr, stateusr, email, passmd5) VALUES ";
                query += "('" + @idusr + "','" + @desusr + "','" + @passusr + "','" + @typeusr + "','" + @stateusr + "','" + @mail + "','" + @passmd5 + "');";
            }
            else
            {
                query = " UPDATE usrfile SET desusr='" + @desusr + "',passusr='" + @passusr + "', typeusr='" + @typeusr + "', ";
                query += "stateusr ='" + @stateusr + "', email ='" + @mail + "', passmd5 ='" + @passmd5 + "' WHERE idusr='" + @idusr + "';";
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

        private string getPassUsrMD5(string texto)
        {
            string passMD5 = string.Empty;

            // Creates an instance of the default implementation of the MD5 hash algorithm.
            using (var md5Hash = MD5.Create())
            {
                // Byte array representation of source string
                var sourceBytes = Encoding.UTF8.GetBytes(texto);

                // Generate hash value(Byte Array) for input data
                var hashBytes = md5Hash.ComputeHash(sourceBytes);

                // Convert hash byte array to string
                var hash = BitConverter.ToString(hashBytes).Replace("-", string.Empty);

                // Output the MD5 hash
                //Console.WriteLine("The MD5 hash of " + source + " is: " + hash);
                passMD5 = hash;
            }

            return passMD5;
        }

        public void eliminarUsr(string idusr)
        {
            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();
            string query = "DELETE from cencosusr WHERE idusr='" + @idusr + "';";
            query += " DELETE from optmenu WHERE idusr='" + @idusr + "';";
            query += " DELETE from ciausrfile WHERE idusr='" + @idusr + "';";
            query += " DELETE from usrfile WHERE idusr='" + @idusr + "';";
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

        public void actualizarCiaUsrFile(string idcia, string idusr)
        {
            string squery;
            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();
            squery = "INSERT INTO ciausrfile (idusr,idcia) VALUES ('" + @idusr + "', '" + @idcia + "');";

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

        public void eliminarCiaUsrFile(string idcia, string idusr)
        {
            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();
            string query = "DELETE FROM cencosusr WHERE idusr='" + @idusr + "' and idcia='" + @idcia + "';";
            query += "DELETE FROM ciausrfile WHERE idusr='" + @idusr + "' and idcia='" + @idcia + "';";
            query += "DELETE FROM gerenciasusr WHERE idusr='" + @idusr + "' and idcia='" + @idcia + "';";

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

        public string validaUsrFile(string usuario, string clave, string opcion)
        {
            string encrip_clave;
            string usuarioActivo = string.Empty;
            string typeUsr = string.Empty;
            string nivelUsr = string.Empty;

            try
            {
                SqlConnection conexion = new SqlConnection();
                conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
                string query = "SELECT a.*,b.nivel FROM usrfile a (nolock) ";
                query += "left join usrfilenivel b on b.idusr=a.idusr ";
                query += "where (a.idusr='" + @usuario + "') AND a.typeusr <> '05';";
                try
                {
                    conexion.Open();
                    SqlCommand myCommand = new SqlCommand(query, conexion);
                    SqlDataReader myReader = myCommand.ExecuteReader();
                    if (myReader.Read())
                    {
                        encrip_clave = myReader["passusr"].ToString();
                        string passPhrase = ConfigurationManager.AppSettings.Get("PASS_PHRASE");
                        string saltValue = ConfigurationManager.AppSettings.Get("SALT_VALUE");
                        string hashAlgorithm = ConfigurationManager.AppSettings.Get("HASH_ALGORITHM");
                        int passwordIterations = Convert.ToInt32(ConfigurationManager.AppSettings.Get("PASSWORD_ITERATIONS"));
                        string initVector = ConfigurationManager.AppSettings.Get("INIT_VECTOR");
                        int keySize = Convert.ToInt32(ConfigurationManager.AppSettings.Get("KEY_SIZE"));
                        RijndaelSimple RijndaelSimple = new RijndaelSimple();
                        string passDecryptUsrFile = RijndaelSimple.Decrypt(encrip_clave, passPhrase, saltValue, hashAlgorithm, passwordIterations, initVector, keySize);
                        if (clave.Equals(passDecryptUsrFile))
                        {
                            usuarioActivo = myReader["desusr"].ToString();
                            typeUsr = myReader["typeusr"].ToString();
                            nivelUsr = myReader["nivel"].ToString();
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
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error de Conexión con Servidor mySQL", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }

            if (opcion.Equals("USUARIO"))
            {
                return usuarioActivo;
            }
            else
            {
                if (opcion.Equals("TIPO_USUARIO"))
                {
                    return typeUsr;
                }
                else
                {
                    if (opcion.Equals("NIVEL_USUARIO"))
                    {
                        return nivelUsr;
                    }
                    else
                    {
                        return string.Empty;
                    }
                }
            }
        }

        public string ui_getDatos(string usuario, string dato)
        {
            string query;
            string resultado = string.Empty;

            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();

            query = "Select * from usrfile where idusr='" + @usuario + "' ;";

            try
            {
                SqlCommand myCommand = new SqlCommand(query, conexion);
                SqlDataReader myReader = myCommand.ExecuteReader();

                if (myReader.Read())
                {
                    if (dato.Equals("TIPO"))
                    {
                        resultado = myReader["typeusr"].ToString();
                    }
                    if (dato.Equals("EMAIL"))
                    {
                        resultado = myReader["email"].ToString();
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

        public string Encripta(string Cadena)
        {
            byte[] inputBytes = Encoding.ASCII.GetBytes(Cadena);
            byte[] encripted;
            RijndaelManaged cripto = new RijndaelManaged();
            using (MemoryStream ms = new MemoryStream(inputBytes.Length))
            {
                using (CryptoStream objCryptoStream = new CryptoStream(ms, cripto.CreateEncryptor(Clave, IV), CryptoStreamMode.Write))
                {
                    objCryptoStream.Write(inputBytes, 0, inputBytes.Length);
                    objCryptoStream.FlushFinalBlock();
                    objCryptoStream.Close();
                }
                encripted = ms.ToArray();
            }
            return Convert.ToBase64String(encripted);
        }

        public string Desencripta(string Cadena)
        {
            byte[] inputBytes = Convert.FromBase64String(Cadena);
            byte[] resultBytes = new byte[inputBytes.Length];
            string textoLimpio = String.Empty;
            RijndaelManaged cripto = new RijndaelManaged();
            using (MemoryStream ms = new MemoryStream(inputBytes))
            {
                using (CryptoStream objCryptoStream = new CryptoStream(ms, cripto.CreateDecryptor(Clave, IV), CryptoStreamMode.Read))
                {
                    using (StreamReader sr = new StreamReader(objCryptoStream, true))
                    {
                        textoLimpio = sr.ReadToEnd();
                    }
                }
            }
            return textoLimpio;
        }

        public bool VerificaCiaUsrFile(string idcia, string idusr)
        {
            bool retorno = true;
            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            string query = "SELECT isnull(count(1),0) as resultado FROM ciausrfile (nolock) where idcia='" + @idcia + "' and idusr='" + @idusr + "' ;";
            try
            {
                conexion.Open();
                SqlCommand myCommand = new SqlCommand(query, conexion);
                SqlDataReader myReader = myCommand.ExecuteReader();
                if (myReader.Read())
                {
                    retorno = myReader.GetInt32(myReader.GetOrdinal("resultado")) > 0 ? true : false;
                }
                myReader.Close();
                myCommand.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            conexion.Close();

            return retorno;
        }

        #region Compañias y Gerencias con Usuarios
        public bool verificaGerenciasUsr(string idcia, string idcencos, string idusr)
        {
            bool retorno = true;
            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            string query = "SELECT isnull(count(1),0) as resultado FROM gerenciasusr (nolock) where idcia='" + @idcia + "' and idusr='" + @idusr + "' and idgerencia='" + @idcencos + "';";
            try
            {
                conexion.Open();
                SqlCommand myCommand = new SqlCommand(query, conexion);
                SqlDataReader myReader = myCommand.ExecuteReader();
                if (myReader.Read())
                {
                    retorno = myReader.GetInt32(myReader.GetOrdinal("resultado")) > 0 ? true : false;
                }
                myReader.Close();
                myCommand.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            conexion.Close();

            return retorno;
        }

        public void actualizarGerenciasUsr(string idcia, string idcencos, string idusr)
        {
            string query;
            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();
            query = "INSERT INTO gerenciasusr (idcia,idusr,idgerencia) VALUES ('" + @idcia + "','" + @idusr + "', '" + @idcencos + "');";
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

        public void eliminarGerenciasUsr(string idcia, string idcencos, string idusr)
        {
            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();
            string query = "DELETE FROM gerenciasusr WHERE idcia='" + @idcia + "' and idusr='" + @idusr + "' ";
            query += "and idgerencia='" + @idcencos + "';";
            query += "DELETE FROM cencosusr WHERE idcia='" + @idcia + "' and idusr='" + @idusr + "' and idgerencia='" + @idcencos + "';";
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
        #endregion

        #region Compañias y Areas con Usuarios
        public bool verificaCencosUsr(string idcia, string idcencos, string idgeren, string idusr)
        {
            bool retorno = true;
            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            string query = "SELECT isnull(count(1),0) as resultado FROM cencosusr (nolock) where idcia='" + @idcia + "' ";
            query += "and idusr='" + @idusr + "' and idcencos='" + @idcencos + "' and idgerencia='" + @idgeren + "';";
            try
            {
                conexion.Open();
                SqlCommand myCommand = new SqlCommand(query, conexion);
                SqlDataReader myReader = myCommand.ExecuteReader();
                if (myReader.Read())
                {
                    retorno = myReader.GetInt32(myReader.GetOrdinal("resultado")) > 0 ? true : false;
                }
                myReader.Close();
                myCommand.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            conexion.Close();

            return retorno;
        }

        public void actualizarCencosUsr(string idcia, string idcencos, string idgeren, string idusr)
        {
            string query;
            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();
            query = "INSERT INTO cencosusr VALUES ('" + @idcia + "','" + @idgeren + "','" + @idcencos + "', '" + @idusr + "');";
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

        public void eliminarCencosUsr(string idcia, string idcencos, string idgeren, string idusr)
        {
            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();
            string query = "DELETE FROM cencosusr WHERE idcia='" + @idcia + "' and idusr='" + @idusr + "' ";
            query = query + "and idcencos='" + @idcencos + "' and idgerencia='" + @idgeren + "';";
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
        #endregion

        public void ChangePassword(string idusr, string passusr)
        {
            passusr = getPassUsr(passusr);

            string query = " UPDATE usrfile SET passusr='" + @passusr + "' WHERE idusr='" + @idusr + "';";

            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();
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