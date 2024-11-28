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
    class AlmovC
    {
        public AlmovC() { }

        public string genCodAlmov(string codcia, string codalma, string tipomov)
        {
            Funciones funciones = new Funciones();
            string codigo = string.Empty;
            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();
            string query = " Select nrope,nrops from alalma where /*codcia='" + @codcia + "' and */codalma='" + @codalma + "' ";

            try
            {
                SqlCommand myCommand = new SqlCommand(query, conexion);
                SqlDataReader myReader = myCommand.ExecuteReader();
                if (myReader.Read())
                {
                    if (tipomov.Equals("PE"))
                    {
                        if (myReader.GetString(myReader.GetOrdinal("nrope")).Trim() != "")
                        {
                            codigo = (long.Parse(myReader.GetString(myReader.GetOrdinal("nrope"))) + 1).ToString();
                        }
                        else
                        {
                            codigo = "1";
                        }
                    }
                    else
                    {
                        if (myReader.GetString(myReader.GetOrdinal("nrops")).Trim() != "")
                        {
                            codigo = (long.Parse(myReader.GetString(myReader.GetOrdinal("nrops"))) + 1).ToString();
                        }
                        else
                        {
                            codigo = "1";
                        }
                    }
                }
                myReader.Close();
                myCommand.Dispose();

                codigo = (funciones.replicateCadena("0", 10 - codigo.Length) + codigo);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            conexion.Close();
            return codigo;
        }

        public string getNumeroRegistros(string codcia, string alma)
        {
            string numero = "0";
            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();
            string query = "select count(*) as numero from almovc where codcia='" + @codcia + "' and alma='" + @alma + "' ";
            query = query + " and flag='ALMA';";
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


        public string getNumeroFacturas(string codcia, string codcaja)
        {
            string numero = "0";
            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();
            string query = "select count(*) as numero from almovc where codcia='" + @codcia + "' ";
            query = query + " and codcaja='" + @codcaja + "' and flag='FTCS';";
            try
            {
                SqlCommand myCommand = new SqlCommand(query, conexion);
                SqlDataReader myReader = myCommand.ExecuteReader();

                if (myReader.Read())
                {
                    numero = myReader.GetString(myReader.GetOrdinal("numero"));
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);

            }
            conexion.Close();
            return numero;
        }

        public string getNumeroGuiasRemi(string codcia, string codcaja)
        {
            string numero = "0";
            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();
            string query = "select count(*) as numero from almovc where codcia='" + @codcia + "' ";
            query = query + " and codcaja='" + @codcaja + "' and flag='GRCS';";
            try
            {
                SqlCommand myCommand = new SqlCommand(query, conexion);
                SqlDataReader myReader = myCommand.ExecuteReader();

                if (myReader.Read())
                {
                    numero = myReader.GetString(myReader.GetOrdinal("numero"));
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);

            }
            conexion.Close();
            return numero;
        }

        public string getNumeroRegistrosVal(string codcia, string alma)
        {
            string numero = "0";
            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();
            string query = " select count(*) as numero from almovc A";
            query = query + " left join tipomov B on A.td=B.tipomov and A.codmov=B.codmov ";
            query = query + " where A.codcia='" + @codcia + "' and A.alma='" + @alma + "' and A.flag='ALMA' ";
            query = query + " and A.situa='F' and B.costeo='M';";
            try
            {
                SqlCommand myCommand = new SqlCommand(query, conexion);
                SqlDataReader myReader = myCommand.ExecuteReader();

                if (myReader.Read())
                {
                    numero = myReader.GetString(myReader.GetOrdinal("numero"));
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);

            }
            conexion.Close();
            return numero;
        }

        public string getNumeroRegistrosPenVal(string codcia, string alma)
        {
            string numero = "0";
            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();
            string query = " select count(*) as numero from almovc A";
            query = query + " left join tipomov B on A.td=B.tipomov and A.codmov=B.codmov ";
            query = query + " where A.codcia='" + @codcia + "' and A.alma='" + @alma + "' and A.flag='ALMA' ";
            query = query + " and A.situa='F' and B.costeo='M' and A.situaval='V';";
            try
            {
                SqlCommand myCommand = new SqlCommand(query, conexion);
                SqlDataReader myReader = myCommand.ExecuteReader();

                if (myReader.Read())
                {
                    numero = myReader.GetString(myReader.GetOrdinal("numero"));
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);

            }
            conexion.Close();
            return numero;
        }

        public void updAlmovC(string operacion, string codcia, string alma, string td, string numdoc,
        string fecdoc, string codmov, string situa, string rftdoc, string rfndoc,
        string secsoli, string persoli, string rfnsoli, string nomrec, string codpro,
        string cencos, string rfalma, string glosa1, string glosa2, string glosa3,
        string codclie, string fcrea, string fmod, string usuario, string flag)
        {
            string query;
            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();
            if (operacion.Equals("AGREGAR"))
            {
                query = "INSERT INTO almovc (codcia,alma,td,numdoc, ";
                query = query + " fecdoc,codmov,situa,rftdoc,rfndoc, ";
                query = query + " secsoli,persoli,rfnsoli,nomrec,codpro, ";
                query = query + " cencos,rfalma,glosa1,glosa2,glosa3, ";
                query = query + " codclie,fcrea,fmod,usuario,flag) ";
                query = query + " VALUES ('" + @codcia + "','" + @alma + "', '" + @td + "', '" + @numdoc + "', ";
                query = query + " STR_TO_DATE('" + @fecdoc + "', '%d/%m/%Y'),'" + @codmov + "','" + @situa + "','" + @rftdoc + "', ";
                query = query + "'" + @rfndoc + "','" + @secsoli + "','" + @persoli + "','" + @rfnsoli + "', ";
                query = query + "'" + @nomrec + "','" + @codpro + "','" + @cencos + "','" + @rfalma + "', ";
                query = query + "'" + @glosa1 + "','" + @glosa2 + "','" + @glosa3 + "','" + @codclie + "', ";
                query = query + "'" + @fcrea + "','" + @fmod + "','" + @usuario + "','" + @flag + "') ;";

                if (td.Equals("PE"))
                {
                    query = query + " UPDATE alalma SET nrope='" + @numdoc + "' where codcia='" + @codcia + "' and codalma='" + @alma + "'; ";
                }
                else
                {
                    query = query + " UPDATE alalma SET nrops='" + @numdoc + "' where codcia='" + @codcia + "' and codalma='" + @alma + "'; ";
                }
            }
            else
            {
                query = "UPDATE almovc SET fecdoc=STR_TO_DATE('" + @fecdoc + "', '%d/%m/%Y'),codmov='" + @codmov + "',";
                query = query + " situa='" + @situa + "',rftdoc='" + @rftdoc + "', ";
                query = query + " rfndoc='" + @rfndoc + "',secsoli='" + @secsoli + "', ";
                query = query + " persoli='" + @persoli + "',rfnsoli='" + @rfnsoli + "', ";
                query = query + " nomrec='" + @nomrec + "',codpro='" + @codpro + "', ";
                query = query + " cencos='" + @cencos + "',rfalma='" + @rfalma + "', ";
                query = query + " glosa1='" + @glosa1 + "',glosa2='" + @glosa2 + "', ";
                query = query + " glosa3='" + @glosa3 + "',codclie='" + @codclie + "', ";
                query = query + " fmod='" + @fmod + "',usuario='" + @usuario + "' ";
                query = query + " WHERE codcia='" + @codcia + "' and alma='" + @alma + "' and td='" + @td + "' and numdoc='" + @numdoc + "';";
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

        public void updAlmovC(string operacion, string codcia, string alma, string td, string numdoc,
       string fecdoc, string codmov, string situa, string rftdoc, string rfndoc,
       string secsoli, string persoli, string rfnsoli, string nomrec, string codpro,
       string cencos, string rfalma, string glosa1, string glosa2, string glosa3,
       string codclie, string fcrea, string fmod, string usuario, string produccion, string año, string flag)
        {
            string query;
            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();
            if (operacion.Equals("AGREGAR"))
            {
                query = "INSERT INTO almovc (codcia,alma,td,numdoc, ";
                query = query + " fecdoc,codmov,situa,rftdoc,rfndoc, ";
                query = query + " secsoli,persoli,rfnsoli,nomrec,codpro, ";
                query = query + " cencos,rfalma,glosa1,glosa2,glosa3, ";
                query = query + " codclie,fcrea,fmod,usuario,flag,produccion,ano) ";
                query = query + " VALUES ('" + @codcia + "','" + @alma + "', '" + @td + "', '" + @numdoc + "', ";
                query = query + " '" + @fecdoc + "','" + @codmov + "','" + @situa + "','" + @rftdoc + "', ";
                query = query + "'" + @rfndoc + "','" + @secsoli + "','" + @persoli + "','" + @rfnsoli + "', ";
                query = query + "'" + @nomrec + "','" + @codpro + "','" + @cencos + "','" + @rfalma + "', ";
                query = query + "'" + @glosa1 + "','" + @glosa2 + "','" + @glosa3 + "','" + @codclie + "', ";
                query = query + "'" + @fcrea + "','" + @fmod + "','" + @usuario + "','" + @flag + "','" + @produccion + "','" + @año + "') ;";

                if (td.Equals("PE"))
                {
                    query = query + " UPDATE alalma SET nrope='" + @numdoc + "' where /*codcia='" + @codcia + "' and */codalma='" + @alma + "'; ";
                }
                else
                {
                    query = query + " UPDATE alalma SET nrops='" + @numdoc + "' where /*codcia='" + @codcia + "' and */codalma='" + @alma + "'; ";
                }
            }
            else
            {
                query = "UPDATE almovc SET fecdoc='" + @fecdoc + "',codmov='" + @codmov + "',";
                query = query + " situa='" + @situa + "',rftdoc='" + @rftdoc + "', ";
                query = query + " rfndoc='" + @rfndoc + "',secsoli='" + @secsoli + "', ";
                query = query + " persoli='" + @persoli + "',rfnsoli='" + @rfnsoli + "', ";
                query = query + " nomrec='" + @nomrec + "',codpro='" + @codpro + "', ";
                query = query + " cencos='" + @cencos + "',rfalma='" + @rfalma + "', ";
                query = query + " glosa1='" + @glosa1 + "',glosa2='" + @glosa2 + "', ";
                query = query + " glosa3='" + @glosa3 + "',codclie='" + @codclie + "', ";
                query = query + " fmod='" + @fmod + "',usuario='" + @usuario + "' ";
                query = query + " WHERE /*codcia='" + @codcia + "' and */alma='" + @alma + "' and td='" + @td + "' and numdoc='" + @numdoc + "';";
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

        public void updFactura(string operacion, string codcia, string codcaja,
            string alma, string numdoc,
            string fecdoc, string codmov, string situa, string rftdoc, string rfndoc,
            string glosa1, string codmon, string codclie, string situaval,
            string fcrea, string fmod, string usuario, string situafac, string rfserie,
            string rfnro, string tipconver, string tipcam, string flag)
        {
            string query;
            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();
            if (operacion.Equals("AGREGAR"))
            {
                query = "INSERT INTO almovc (codcia,codcaja,alma,td,numdoc, ";
                query = query + " fecdoc,codmov,situa,rftdoc,rfndoc, ";
                query = query + " glosa1,codmon,codclie,situaval,fcrea,fmod,usuario,";
                query = query + " situafac,rfserie,rfnro,tipconver,tipcam,flag) ";
                query = query + " VALUES ('" + @codcia + "','" + @codcaja + "','" + @alma + "', 'PS', '" + @numdoc + "', ";
                query = query + " STR_TO_DATE('" + @fecdoc + "', '%d/%m/%Y'),'" + @codmov + "','" + @situa + "','" + @rftdoc + "', ";
                query = query + "'" + @rfndoc + "','" + @glosa1 + "','" + @codmon + "','" + @codclie + "', ";
                query = query + "'" + @situaval + "','" + @fcrea + "','" + @fmod + "','" + @usuario + "','" + @situafac + "',";
                query = query + " '" + @rfserie + "','" + @rfnro + "','" + @tipconver + "','" + @tipcam + "','" + @flag + "') ;";

                query = query + " UPDATE alalma SET nrops='" + @numdoc + "' where  codcia='" + @codcia + "' and codalma='" + @alma + "'; ";
                query = query + " UPDATE numdoc SET nrodoc='" + @rfnro + "' where codcia='" + @codcia + "' ";
                query = query + " and codcaja='" + @codcaja + "' and tipodoc='" + @rftdoc + "'; ";
            }
            else
            {
                query = "UPDATE almovc SET fecdoc=STR_TO_DATE('" + @fecdoc + "', '%d/%m/%Y'),";
                query = query + " codmov='" + @codmov + "',";
                query = query + " situa='" + @situa + "',rftdoc='" + @rftdoc + "', ";
                query = query + " rfndoc='" + @rfndoc + "', ";
                query = query + " glosa1='" + @glosa1 + "',codmon='" + @codmon + "', ";
                query = query + " codclie='" + @codclie + "',situaval='" + @situaval + "', ";
                query = query + " fmod='" + @fmod + "',usuario='" + @usuario + "',situafac='" + @situafac + "',";
                query = query + " rfserie='" + @rfserie + "',rfnro='" + @rfnro + "',";
                query = query + " tipconver='" + @tipconver + "',tipcam='" + @tipcam + "' ";
                query = query + " WHERE codcia='" + @codcia + "' and alma='" + @alma + "' and td='PS' and numdoc='" + @numdoc + "';";
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

        public void updFacGuia(string codcia, string alma, string numdoc, string fecdoc,
            string codmov, string rftdoc, string rfndoc,
            string glosa1, string codmon, string codclie,
            string fcreafac, string usuariofac, string situafac, string rfserie,
            string rfnro, string tipconver, string tipcam)
        {
            string query;
            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();

            query = "UPDATE almovc SET fecdoc=STR_TO_DATE('" + @fecdoc + "', '%d/%m/%Y'),";
            query = query + " codmov='" + @codmov + "',rftdoc='" + @rftdoc + "', ";
            query = query + " rfndoc='" + @rfndoc + "', ";
            query = query + " glosa1='" + @glosa1 + "',codmon='" + @codmon + "', ";
            query = query + " codclie='" + @codclie + "',fcreafac='" + @fcreafac + "', ";
            query = query + " usuariofac='" + @usuariofac + "',situafac='" + @situafac + "',";
            query = query + " rfserie='" + @rfserie + "',rfnro='" + @rfnro + "',";
            query = query + " tipconver='" + @tipconver + "',tipcam='" + @tipcam + "' ";
            query = query + " WHERE codcia='" + @codcia + "' and alma='" + @alma + "' and td='PS' and numdoc='" + @numdoc + "';";

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


        public void updGuiaRemision(string operacion, string codcia, string codcaja,
           string alma, string numdoc, string codmov, string rftguia, string rfnguia, string rfserieguia,
           string rfnroguia, string fecguia, string fectras, string partida, string llegada, string codclie,
           string docidendesti, string nrodocdesti, string razontrans,
           string marcatrans, string certrans, string lictrans, string mottras, string glosa1, string situa,
           string situaval, string situagr, string fcrea, string fmod, string usuario, string flag, string situafac,
            string orden, string rfserieOC, string rfnumeroOC, string fechaOC, string codesta, string albaran)
        {
            string query;
            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();
            if (operacion.Equals("AGREGAR"))
            {
                query = "INSERT INTO almovc (codcia,codcaja,alma,td,numdoc,codmov,rftguia,rfnguia,rfserieguia,rfnroguia, ";
                query = query + " fecguia,fectras,partida,llegada,codclie,docidendesti, ";
                query = query + " nrodocdesti,marcatrans,certrans,lictrans,mottras,glosa1,situa,situaval,";
                query = query + " situagr,fcrea,fmod,usuario,flag,razontrans,situafac,fecdoc,orden,rfserieOC,rfnroOC,fechaOC,codesta,albaran) ";
                query = query + " VALUES ('" + @codcia + "','" + @codcaja + "','" + @alma + "', 'PS', '" + @numdoc + "', '" + @codmov + "', ";
                query = query + " '" + @rftguia + "','" + @rfnguia + "','" + @rfserieguia + "','" + @rfnroguia + "', ";
                query = query + " STR_TO_DATE('" + @fecguia + "', '%d/%m/%Y'),'" + @fectras + "','" + @partida + "', ";
                query = query + " '" + @llegada + "','" + @codclie + "','" + @docidendesti + "', ";
                query = query + "'" + @nrodocdesti + "','" + @marcatrans + "','" + @certrans + "','" + @lictrans + "', ";
                query = query + "'" + @mottras + "','" + @glosa1 + "','" + @situa + "','" + @situaval + "','" + @situagr + "',";
                query = query + " '" + @fcrea + "','" + @fmod + "','" + @usuario + "','" + @flag + "','" + @razontrans + "',";
                query = query + " '" + @situafac + "',STR_TO_DATE('" + @fecguia + "', '%d/%m/%Y'),'" + @orden + "','" + @rfserieOC + "','" + @rfnumeroOC + "','" + @fechaOC + "','" + @codesta + "','" + @albaran + "') ;";

                query = query + " UPDATE alalma SET nrops='" + @numdoc + "' where codcia='" + @codcia + "' and codalma='" + @alma + "'; ";
                query = query + " UPDATE numdoc SET nrodoc='" + @rfnroguia + "' where codcia='" + @codcia + "' ";
                query = query + " and codcaja='" + @codcaja + "' and tipodoc='" + @rftguia + "'; ";
            }
            else
            {
                query = "UPDATE almovc SET fecguia=STR_TO_DATE('" + @fecguia + "', '%d/%m/%Y'),fectras='" + @fectras + "',";
                query = query + " codmov='" + @codmov + "',rftguia='" + @rftguia + "',rfnguia='" + @rfnguia + "', ";
                query = query + " rfserieguia='" + @rfserieguia + "',rfnroguia='" + @rfnroguia + "',fectras='" + @fectras + "',";
                query = query + " partida='" + @partida + "',llegada='" + @llegada + "',codclie='" + @codclie + "',";
                query = query + " docidendesti='" + @docidendesti + "',";
                query = query + " nrodocdesti='" + @nrodocdesti + "',marcatrans='" + @marcatrans + "',certrans='" + @certrans + "',";
                query = query + " lictrans='" + @lictrans + "',mottras='" + @mottras + "',glosa1='" + @glosa1 + "',";
                query = query + " situaval='" + @situaval + "', situagr='" + @situagr + "',razontrans='" + @razontrans + "',";
                query = query + " orden='" + @orden + "',rfserieOC='" + @rfserieOC + "' , rfnroOC='" + @rfnumeroOC + "', fechaOC='" + @fechaOC + "', ";
                query = query + " fmod='" + @fmod + "',usuario='" + @usuario + "',codesta='" + @codesta + "',albaran='" + @albaran + "' ";
                query = query + " WHERE codcia='" + @codcia + "' and alma='" + @alma + "' and td='PS' and numdoc='" + @numdoc + "';";
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

        public void updValAlmovC(string codcia, string alma, string td, string numdoc,
        string codmon, float tipcam, string situaval, string fmod, string usuario)
        {
            string query;
            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();

            query = "UPDATE almovc SET codmon='" + @codmon + "',tipcam='" + @tipcam + "', situa='F', ";
            query = query + " situaval='" + @situaval + "',";
            query = query + " fmod='" + @fmod + "',usuario='" + @usuario + "' ";
            query = query + " WHERE codcia='" + @codcia + "' and alma='" + @alma + "' and td='" + @td + "' and numdoc='" + @numdoc + "';";

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

        public void updFinAlmovC(string codcia, string alma, string td, string numdoc, string fmod, string usuario)
        {
            string query;
            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();
            query = "UPDATE almovc SET fmod='" + @fmod + "',usuario='" + @usuario + "' ,situa='F'";
            query = query + " WHERE /*codcia='" + @codcia + "' and */alma='" + @alma + "' and td='" + @td + "' and numdoc='" + @numdoc + "';";
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

        public void updRecAlmovC(string codcia, string alma, string td, string numdoc, string frecibe, string usurec)
        {
            string query;
            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();
            query = "UPDATE almovc SET frecibe='" + @frecibe + "',usurec='" + @usurec + "' ,recibe='S'";
            query = query + " WHERE codcia='" + @codcia + "' and alma='" + @alma + "' and td='" + @td + "' and numdoc='" + @numdoc + "';";
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

        public void updAnuAlmovC(string codcia, string alma, string td, string numdoc)
        {
            string query;
            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();
            query = "UPDATE almovc SET situa='A' ";
            query = query + " WHERE codcia='" + @codcia + "' and alma='" + @alma + "' and td='" + @td + "' and numdoc='" + @numdoc + "';";
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

        public void updAnuFactura(string codcia, string alma, string td, string numdoc)
        {
            string query;
            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();
            query = "UPDATE almovc SET situa='A' ,situafac='A' ";
            query = query + " WHERE codcia='" + @codcia + "' and alma='" + @alma + "' and td='" + @td + "' and numdoc='" + @numdoc + "';";
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

        public void updReasignarLote(string codcia, string alma, string td, string numdoc, string item, string lote)
        {
            string query;
            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();
            query = "UPDATE almovd SET lote='" + @lote + "' ";
            query = query + " WHERE codcia='" + @codcia + "' and alma='" + @alma + "' ";
            query = query + " and td='" + @td + "' and numdoc='" + @numdoc + "' and item='" + @item + "';";
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

        public void updAnuGuia(string codcia, string alma, string td, string numdoc)
        {
            string query;
            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();
            query = "UPDATE almovc SET situa='A' ,situagr='A' ";
            query = query + " WHERE codcia='" + @codcia + "' and alma='" + @alma + "' and td='" + @td + "' and numdoc='" + @numdoc + "';";
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

        public void updFinFac(string codcia, string alma, string td, string numdoc, string fmod, string usuario)
        {
            string query;
            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();
            query = "UPDATE almovc SET fmod='" + @fmod + "',usuario='" + @usuario + "' ,situafac='F'";
            query = query + " WHERE codcia='" + @codcia + "' and alma='" + @alma + "' and td='" + @td + "' and numdoc='" + @numdoc + "';";
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

        public void updFinFacGuia(string codcia, string alma, string td, string numdoc, string fcreafac, string usuariofac)
        {
            string query;
            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();
            query = "UPDATE almovc SET fcreafac='" + @fcreafac + "',usuariofac='" + @usuariofac + "' ,situafac='F'";
            query = query + " WHERE codcia='" + @codcia + "' and alma='" + @alma + "' and td='" + @td + "' and numdoc='" + @numdoc + "';";

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

        public void updDesFinFacGuia(string codcia, string alma, string td, string numdoc, string fcreafac, string usuariofac)
        {
            string query;
            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();
            query = "UPDATE almovc SET fcreafac='" + @fcreafac + "',usuariofac='" + @usuariofac + "' ,situafac='V'";
            query = query + " WHERE codcia='" + @codcia + "' and alma='" + @alma + "' and td='" + @td + "' and numdoc='" + @numdoc + "';";
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

        public void updDesFinRec(string codcia, string alma, string td, string numdoc, string fcrea, string usuario)
        {
            string query;
            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();
            query = " DELETE from almovd WHERE CONCAT(codcia,alma,td,numdoc) in (Select CONCAT(codcia,alma,td,numdoc) from ";
            query = query + " almovc where codcia='" + @codcia + "' and almaori='" + @alma + "' ";
            query = query + " and tdori='" + @td + "' and numdocori='" + @numdoc + "') ; ";
            query = query + " DELETE from almovc WHERE codcia='" + @codcia + "' and almaori='" + @alma + "' and tdori='" + @td + "' ";
            query = query + " and numdocori='" + @numdoc + "'; ";
            query = query + " UPDATE almovd SET cantrecibe=0 ";
            query = query + " WHERE codcia='" + @codcia + "' and alma='" + @alma + "' and td='" + @td + "' and numdoc='" + @numdoc + "';";
            query = query + " UPDATE almovc SET frecibe='" + @fcrea + "',usurec='" + @usuario + "' ,recibe='N'";
            query = query + " WHERE codcia='" + @codcia + "' and alma='" + @alma + "' and td='" + @td + "' and numdoc='" + @numdoc + "';";
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

        public void updFinGuia(string codcia, string alma, string td, string numdoc, string fmod, string usuario)
        {
            string query;
            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();
            query = "UPDATE almovc SET fmod='" + @fmod + "',usuario='" + @usuario + "' ,situagr='F'";
            query = query + " WHERE codcia='" + @codcia + "' and alma='" + @alma + "' and td='" + @td + "' and numdoc='" + @numdoc + "';";
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

        public void updDesFinGuia(string codcia, string alma, string td, string numdoc, string fmod, string usuario)
        {
            string query;
            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();

            query = " UPDATE almovc SET fmod='" + @fmod + "',usuario='" + @usuario + "' ,situagr='V'";
            query = query + " WHERE codcia='" + @codcia + "' and alma='" + @alma + "' and td='" + @td + "' and numdoc='" + @numdoc + "';";

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

        public void updDesFinMov(string codcia, string alma, string td, string numdoc, string fmod, string usuario)
        {
            string query;
            SqlConnection conexion = new SqlConnection();
            string dfd = "V";
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();

            query = "EXEC updDesFinMov '" + @fmod + "','" + @usuario + "','" + @dfd + "','" + @codcia + "','" + @alma + "','" + @td + "','" + @numdoc + "' ";
            //query = "UPDATE almovc SET fmod='" + @fmod + "',usuario='" + @usuario + "' ,situa='V' ";
            //query = query + " WHERE codcia='" + @codcia + "' and alma='" + @alma + "' and td='" + @td + "' and numdoc='" + @numdoc + "';";
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


        public void updDesFinVal(string codcia, string alma, string td, string numdoc, string fmod, string usuario)
        {
            string query;
            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();
            query = "UPDATE almovc SET fmod='" + @fmod + "',usuario='" + @usuario + "' ,situaval='V' ";
            query = query + " WHERE codcia='" + @codcia + "' and alma='" + @alma + "' and td='" + @td + "' and numdoc='" + @numdoc + "';";
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

        public void delAlmovC(string codcia, string alma, string td, string numdoc)
        {
            string query;
            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();
            query = "DELETE from almovc WHERE /*codcia='" + @codcia + "' and */alma='" + @alma + "' and td='" + @td + "' ";
            query = query + " and numdoc='" + @numdoc + "'; ";
            query = query + " DELETE from almovd WHERE /*codcia='" + @codcia + "' and */alma='" + @alma + "' and td='" + @td + "' ";
            query = query + " and numdoc='" + @numdoc + "';";
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

        public string printAlmov(string codcia, string alma, string td, string numdoc)
        {
            Funciones funciones = new Funciones();
            DataTable dtmov = new DataTable();
            string strPrinter = string.Empty;

            string query;
            query = " select A.alma,A.td,A.numdoc,A.fecdoc,A.codmov,B.desmov,C.desalma,A.rftdoc, ";
            query = query + " A.rfndoc,A.rfnsoli,A.secsoli,D.desmaesgen as dessecsoli,A.persoli,E.dessoli, ";
            query = query + " A.nomrec,A.cencos,F.descencos,A.codmon,A.rfalma,G.desalma as desrfalma, ";
            query = query + " A.glosa1,A.glosa2,A.glosa3,A.codclie,H.desclie,A.codpro,I.desprovee,I.ruc, ";
            query = query + " A.tipcam,A.fcrea,A.fmod,A.usuario,CASE ISNULL(J.item,1) WHEN 1 THEN '' WHEN 0 THEN J.item END as item,";
            query = query + " CASE ISNULL(J.codarti,1) WHEN 1 THEN '' WHEN 0 THEN J.codarti END as codarti,";
            query = query + " CASE ISNULL(K.desarti,1) WHEN 1 THEN '' WHEN 0 THEN K.desarti END as desarti, ";
            query = query + " CASE ISNULL(J.cantidad,1) WHEN 1 THEN 0  WHEN 0 THEN J.cantidad END as cantidad,";
            query = query + " CASE ISNULL(J.lote,1) WHEN 1 THEN '' WHEN 0 THEN J.lote END as lote,";
            query = query + " CASE ISNULL(J.preuni,1) WHEN 1 THEN 0 WHEN 0 THEN J.preuni END as preuni,";
            query = query + " CASE ISNULL(J.neto,1) WHEN 1 THEN 0 WHEN 0 THEN J.neto END as neto,";
            query = query + " CASE ISNULL(J.igv,1) WHEN 1 THEN 0 WHEN 0 THEN J.igv END as igv,";
            query = query + " CASE ISNULL(J.total,1) WHEN 1 THEN 0  WHEN 0 THEN J.total END as total,";
            query = query + " CASE ISNULL(K.unidad,1) WHEN 1 THEN '' WHEN 0 THEN K.unidad END as unidad,";
            query = query + " CASE ISNULL(K.famarti,1) WHEN 1 THEN '' WHEN 0 THEN K.famarti END as famarti, ";
            query = query + " CASE ISNULL(J.codcenpro,1) WHEN 1 THEN '' WHEN 0 THEN J.codcenpro END as codcenpro,";
            query = query + " L.descenpro,J.codseccion,M.desseccion,N.desmaesgen as destiposec from almovc A ";
            query = query + " left join tipomov B on A.td=B.tipomov and A.codmov=B.codmov ";
            query = query + " left join alalma C on A.alma=C.codalma and A.codcia=C.codcia ";
            query = query + " left join maesgen D on D.idmaesgen='150' and A.secsoli=D.clavemaesgen ";
            query = query + " left join solicita E on A.codcia=E.codcia and A.secsoli=E.secsoli and A.persoli=E.codsoli ";
            query = query + " left join cencos F on A.cencos=F.idcencos /*and A.alma=F.codalma */and A.codcia=F.idcia ";
            query = query + " left join alalma G on A.rfalma=G.codalma and A.codcia=G.codcia";
            query = query + " left join clie H on A.codclie=H.codclie ";
            query = query + " left join provee I on A.codpro=I.codprovee ";
            query = query + " left join almovd J on A.codcia=J.codcia and A.alma=J.alma and A.td=J.td and A.numdoc=J.numdoc ";
            query = query + " left join alarti K on J.codcia=K.codcia and J.codarti=K.codarti ";
            query = query + " left join cenpro L on J.codcia=L.codcia and J.codcenpro=L.codcenpro ";
            query = query + " left join secciones M on J.codcia=M.codcia and J.codcenpro=M.codcenpro and J.tiposec=M.tiposec and J.codseccion=M.codseccion ";
            query = query + " left join maesgen N on N.idmaesgen='700' and J.tiposec=N.clavemaesgen ";
            query = query + " WHERE A.codcia='" + @codcia + "' and A.alma='" + @alma + "' and A.td='" + @td + "' ";
            query = query + " and A.numdoc='" + @numdoc + "' order by A.alma asc , A.td asc , ";
            query = query + " A.numdoc asc, J.item asc ; ";
            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();

            SqlDataAdapter dasec = new SqlDataAdapter();
            dasec.SelectCommand = new SqlCommand(query, conexion);
            dasec.Fill(dtmov);
            int fila = 0;
            float valorventa = 0;
            float igv = 0;
            float total = 0;
            string fcrea = string.Empty;
            string fmod = string.Empty;
            string usuario = string.Empty;

            if (dtmov.Rows.Count > 0)
            {
                foreach (DataRow row_mov in dtmov.Rows)
                {
                    fila++;

                    if (fila.Equals(1))
                    {
                        fcrea = row_mov["fcrea"].ToString();
                        fmod = row_mov["fmod"].ToString();
                        usuario = row_mov["usuario"].ToString();

                        strPrinter = strPrinter + "\n\n" + " ALMACEN            :  " + row_mov["alma"].ToString() + " " + row_mov["desalma"].ToString() + "\n";
                        strPrinter = strPrinter + " NRO. PARTE         :  " + row_mov["td"].ToString() + " " + row_mov["numdoc"].ToString() + "\n";
                        strPrinter = strPrinter + " DOC. REFERENCIA    :  " + row_mov["rftdoc"].ToString() + " " + row_mov["rfndoc"].ToString() + "\n";
                        strPrinter = strPrinter + " FECHA              :  " + funciones.longitudCadena(row_mov["fecdoc"].ToString(), 10) + "\n";
                        strPrinter = strPrinter + " MOV. DE ALMACEN    :  " + row_mov["codmov"].ToString() + " " + row_mov["desmov"].ToString() + "\n";
                        strPrinter = strPrinter + " ------------------------------------------------------------------------------------------------------------------------------------" + "\n";
                        if (row_mov["rfnsoli"].ToString() != string.Empty)
                        {
                            strPrinter = strPrinter + " ORDEN DE CONSUMO   :  " + row_mov["rfnsoli"].ToString() + "\n";
                        }
                        if (row_mov["secsoli"].ToString() != string.Empty)
                        {
                            strPrinter = strPrinter + " AREA SOLICITANTE   :  " + row_mov["secsoli"].ToString() + " " + row_mov["dessecsoli"].ToString() + "\n";
                        }
                        if (row_mov["dessoli"].ToString() != string.Empty)
                        {
                            strPrinter = strPrinter + " SOLICITADO POR     :  " + row_mov["dessoli"].ToString() + "\n";
                        }
                        if (row_mov["persoli"].ToString() != string.Empty)
                        {
                            strPrinter = strPrinter + " RECEPCIONADO POR   :  " + row_mov["persoli"].ToString() + "\n";
                        }
                        if (row_mov["cencos"].ToString() != string.Empty)
                        {
                            strPrinter = strPrinter + " CENTRO DE COSTO    :  " + row_mov["cencos"].ToString() + " " + row_mov["descencos"].ToString() + "\n";
                        }

                        if (row_mov["rfalma"].ToString() != string.Empty)
                        {
                            strPrinter = strPrinter + " ALMACEN REF.       :  " + row_mov["rfalma"].ToString() + " " + row_mov["desrfalma"].ToString() + "\n";
                        }
                        strPrinter = strPrinter + " MONEDA             :  " + row_mov["codmon"].ToString() + funciones.replicateCadena(" ", 10) + "T.C.: " + funciones.alineacionNumero(float.Parse(row_mov["tipcam"].ToString()).ToString("###,###,##0.0000;(###,###,##0.0000);0.0000"), 8) + "\n";
                        if (row_mov["codclie"].ToString() != string.Empty)
                        {
                            strPrinter = strPrinter + " CLIENTE            :  " + row_mov["codclie"].ToString() + " " + row_mov["desclie"].ToString() + "\n";
                        }
                        if (row_mov["ruc"].ToString() != string.Empty)
                        {
                            strPrinter = strPrinter + " PROVEEDOR          :  " + row_mov["ruc"].ToString() + " " + row_mov["desprovee"].ToString() + "\n";
                        }
                        if (row_mov["glosa1"].ToString() != string.Empty)
                        {
                            strPrinter = strPrinter + " GLOSA              :  " + row_mov["glosa1"].ToString() + "\n";
                        }
                        if (row_mov["glosa2"].ToString() != string.Empty)
                        {
                            strPrinter = strPrinter + "                       " + row_mov["glosa2"].ToString() + "\n";
                        }
                        if (row_mov["glosa3"].ToString() != string.Empty)
                        {
                            strPrinter = strPrinter + "                       " + row_mov["glosa3"].ToString() + "\n";
                        }

                        strPrinter = strPrinter + "\n\n" + "D E T A L L E    D E L   M O V I M I E N T O" + "\n\n";
                        strPrinter = strPrinter + " ------------------------------------------------------------------------------------------------------------------------------------" + "\n";
                        strPrinter = strPrinter + "ITEM" + " " + " CODIGO " + " " + "                     DESCRIPCION                  " + " " + "UND." + " " + "     LOTE      " + " " + "   CANTIDAD    " + " " + "  P.UNIT  " + " " + "   IMP.NETO    " + " " + "FAM." + "\n";
                        strPrinter = strPrinter + " ------------------------------------------------------------------------------------------------------------------------------------" + "\n";
                    }

                    strPrinter = strPrinter + funciones.longitudCadena(row_mov["item"].ToString(), 4) + " " + funciones.longitudCadena(row_mov["codarti"].ToString(), 8) + " ";
                    strPrinter = strPrinter + funciones.longitudCadena(row_mov["desarti"].ToString(), 50) + " ";
                    strPrinter = strPrinter + funciones.longitudCadena(row_mov["unidad"].ToString(), 4) + " " + funciones.longitudCadena(row_mov["lote"].ToString(), 15) + " ";
                    strPrinter = strPrinter + funciones.alineacionNumero(float.Parse(row_mov["cantidad"].ToString()).ToString("###,###,##0.00;(###,###,##0.00);0.00"), 15) + " ";
                    strPrinter = strPrinter + funciones.alineacionNumero(float.Parse(row_mov["preuni"].ToString()).ToString("###,###,##0.00;(###,###,##0.00);0.00"), 10) + " ";
                    strPrinter = strPrinter + funciones.alineacionNumero(float.Parse(row_mov["neto"].ToString()).ToString("###,###,##0.00;(###,###,##0.00);0.00"), 15) + " ";
                    strPrinter = strPrinter + funciones.longitudCadena(row_mov["famarti"].ToString(), 4) + "\n";

                    if (row_mov["descenpro"].ToString() != string.Empty)
                    {
                        strPrinter = strPrinter + "              " + row_mov["codcenpro"].ToString() + " " + row_mov["descenpro"].ToString();
                    }
                    if (row_mov["codseccion"].ToString() != string.Empty)
                    {
                        strPrinter = strPrinter + " / " + row_mov["destiposec"].ToString() + " " + row_mov["codseccion"].ToString() + " " + row_mov["desseccion"].ToString() + "\n";
                    }

                    valorventa = valorventa + float.Parse(row_mov["neto"].ToString());
                    igv = igv + float.Parse(row_mov["igv"].ToString());
                    total = total + float.Parse(row_mov["total"].ToString());
                }
                strPrinter = strPrinter + " ------------------------------------------------------------------------------------------------------------------------------------" + "\n";
                strPrinter = strPrinter + "F.CREA: " + funciones.longitudCadena(fcrea, 10) + " F.MOD.: " + funciones.longitudCadena(fmod, 10) + " USUARIO: " + usuario + "\n";

                strPrinter = strPrinter + funciones.replicateCadena(" ", 98) + "VALOR VENTA :  " + funciones.alineacionNumero(valorventa.ToString("###,###,##0.00;(###,###,##0.00);0.00"), 15) + "\n";
                strPrinter = strPrinter + funciones.replicateCadena(" ", 98) + "IGV         :  " + funciones.alineacionNumero(igv.ToString("###,###,##0.00;(###,###,##0.00);0.00"), 15) + "\n";
                strPrinter = strPrinter + funciones.replicateCadena(" ", 98) + "TOTAL       :  " + funciones.alineacionNumero(total.ToString("###,###,##0.00;(###,###,##0.00);0.00"), 15) + "\n\n\n\n";

                strPrinter = strPrinter + funciones.replicateCadena("-", 25) + " " + funciones.replicateCadena("-", 25) + " " + funciones.replicateCadena("-", 25) + " " + funciones.replicateCadena("-", 25) + "\n";
                strPrinter = strPrinter + "        VB ALMACEN       " + " " + "      VB CONTABILIDAD    " + " " + "       VB PRODUCCION     " + " " + "      VB ADMINISTRACION  " + "\n\n";
            }
            conexion.Close();
            return strPrinter;
        }
    }
}