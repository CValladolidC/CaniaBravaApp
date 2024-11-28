using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace CaniaBrava
{
    class PerRet
    {
        public PerRet() { }

        public string generaCodigoInterno(string idcia)
        {

            Funciones funciones = new Funciones();
            string codigoInterno = "00001";
            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();
            string query = "select isnull(max(idperplan)) as existencia,max(idperplan)+1 as codigointerno from perret where (idcia='" + @idcia + "');";

            try
            {
                SqlCommand myCommand = new SqlCommand(query, conexion);
                SqlDataReader myReader = myCommand.ExecuteReader();

                if (myReader.Read())
                {

                    if (myReader["existencia"].Equals("1"))
                    {
                        codigoInterno = "00001";
                    }
                    else
                    {
                        string codigo = myReader["codigointerno"].ToString();
                        codigoInterno = funciones.replicateCadena("0", 5 - codigo.Trim().Length) + codigo;
                    }
                }
                else
                {
                    codigoInterno = "00001";
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

        public string verificaPerRetxDoc(string idcia, string tipdoc, string nrodoc)
        {
            string numero = "0";
            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();
            string squery = "select count(*) as numero from perret where idcia='" + @idcia + "' and tipdoc='" + @tipdoc + "' and nrodoc='" + @nrodoc + "';";
            try
            {
                SqlCommand myCommand = new SqlCommand(squery, conexion);
                SqlDataReader myReader = myCommand.ExecuteReader();

                if (myReader.Read())
                {
                    numero = myReader["numero"].ToString();
                }

            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            conexion.Close();
            return numero;
        }

        public string getNumeroRegistrosPerRet(string idcia)
        {
            string numero = "0";
            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();
            string squery = "select count(*) as numero from perret where idcia='" + @idcia + "';";
            try
            {
                SqlCommand myCommand = new SqlCommand(squery, conexion);
                SqlDataReader myReader = myCommand.ExecuteReader();

                if (myReader.Read())
                {
                    numero = myReader["numero"].ToString();
                }

            }
            catch (SqlException)
            {
                MessageBox.Show("No se ha podido realizar el proceso de Actualización", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);

            }

            conexion.Close();
            return numero;
        }

        public void actualizaPerRet(string soperacion, string idperplan, string idcia, string apepat, string apemat,
        string nombres, string fecnac, string tipdoc, string nrodoc, string telfijo, string celular,
        string rpm, string estcivil, string nacion, string email, string catlic, string nrolic, string tipvia, string nomvia,
        string nrovia, string intvia, string tipzona, string nomzona, string refzona, string ubigeo, string dscubigeo,
        string sexo, string ruc)
        {

            string squery;

            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();



            if (soperacion.Equals("AGREGAR"))
            {
                squery = "INSERT INTO perret (idperplan,  idcia,  apepat,  apemat,";
                squery = squery + "nombres,  fecnac,  tipdoc,  nrodoc,  telfijo,  celular,";
                squery = squery + "rpm,  estcivil,  nacion,  email,  catlic,  nrolic,  ";
                squery = squery + "tipvia,  nomvia,nrovia,  intvia,  tipzona,  nomzona,  refzona,  ubigeo,  dscubigeo,";
                squery = squery + "sexo,ruc) VALUES ('" + @idperplan + "','" + @idcia + "','" + @apepat + "','" + @apemat;
                squery = squery + "','" + @nombres + "'," + " STR_TO_DATE('" + @fecnac + "', '%d/%m/%Y') " + ",'" + @tipdoc + "','" + @nrodoc + "','" + @telfijo + "','" + @celular;
                squery = squery + "','" + @rpm + "','" + @estcivil + "','" + @nacion + "','" + @email + "','" + @catlic + "','" + @nrolic;
                squery = squery + "','" + @tipvia + "','" + @nomvia;
                squery = squery + "','" + @nrovia + "','" + @intvia + "','" + @tipzona + "','" + @nomzona + "','" + @refzona + "','" + @ubigeo + "','" + @dscubigeo;
                squery = squery + "','" + @sexo + "','" + @ruc + "');";
            }
            else
            {
                squery = "UPDATE perret SET apepat='" + @apepat + "',apemat='" + @apemat + "',";
                squery = squery + "nombres='" + @nombres + "',fecnac=STR_TO_DATE('" + @fecnac + "', '%d/%m/%Y')" + ",tipdoc='" + @tipdoc + "',nrodoc='" + @nrodoc + "',telfijo='" + @telfijo + "',celular='" + @celular + "',";
                squery = squery + "rpm='" + @rpm + "',estcivil='" + @estcivil + "',nacion='" + @nacion + "',email='" + @email + "',catlic='" + @catlic + "',nrolic='" + @nrolic + "',";
                squery = squery + "tipvia='" + @tipvia + "',nomvia='" + @nomvia + "',";
                squery = squery + "nrovia='" + @nrovia + "',intvia='" + @intvia + "',tipzona='" + @tipzona + "',nomzona='" + @nomzona + "',refzona='" + @refzona + "',ubigeo='" + @ubigeo + "',dscubigeo='" + @dscubigeo + "',";
                squery = squery + "sexo='" + @sexo + "',ruc='" + @ruc + "' WHERE idcia='" + @idcia + "' and idperplan='" + @idperplan + "';";
            }


            try
            {
                SqlCommand myCommand = new SqlCommand(squery, conexion);
                myCommand.ExecuteNonQuery();
                myCommand.Dispose();

            }
            catch (SqlException ex)
            {
                MessageBox.Show("A ocurrido un error en el proceso de Actualización [ " + ex.Message + " ]", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);

            }

            conexion.Close();


        }

        public void eliminarPerRet(string idcia, string idperplan)
        {
            string query;
            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();
            query = "DELETE from desret WHERE idcia='" + @idcia + "' and idperplan='" + @idperplan + "';";
            query = query + " DELETE from perret WHERE idcia='" + @idcia + "' and idperplan='" + @idperplan + "';";
            try
            {
                SqlCommand myCommand = new SqlCommand(query, conexion);
                myCommand.ExecuteNonQuery();
                myCommand.Dispose();
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            conexion.Close();
        }

        public string ui_getDatosPerRet(string idcia, string idperplan, string datossolicitado)
        {
            string query;
            string resultado = string.Empty;

            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();

            query = "Select A.idperplan,C.Parm1maesgen as cortotipodoc,A.nrodoc,";
            query = query + "CONCAT(CONCAT(CONCAT(A.apepat,' '),CONCAT(A.apemat,' , ')),A.nombres) as nombre,";
            query = query + " A.sexo from perret A  ";
            query = query + "left join maesgen C on C.idmaesgen='002' and A.tipdoc=C.clavemaesgen ";
            query = query + "where A.idcia='" + @idcia + "' and A.idperplan='" + @idperplan + "' order by idperplan asc;";

            try
            {
                SqlCommand myCommand = new SqlCommand(query, conexion);
                SqlDataReader myReader = myCommand.ExecuteReader();

                if (myReader.Read())
                {
                    if (datossolicitado.Equals("0"))
                    {
                        resultado = myReader["idperplan"].ToString();
                    }
                    if (datossolicitado.Equals("1"))
                    {
                        resultado = myReader["cortotipodoc"].ToString();
                    }
                    if (datossolicitado.Equals("2"))
                    {
                        resultado = myReader["nrodoc"].ToString();
                    }
                    if (datossolicitado.Equals("3"))
                    {
                        resultado = myReader["nombre"].ToString();
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