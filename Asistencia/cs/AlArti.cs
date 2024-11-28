using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace CaniaBrava
{
    class AlArti
    {
        public AlArti() { }

        public string getAlfa(string letra)
        {
            Funciones funciones = new Funciones();
            string codigo = "00";
            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();
            string query = " select valor from alfa where letra='" + @letra + "' ";
            try
            {
                SqlCommand myCommand = new SqlCommand(query, conexion);
                SqlDataReader myReader = myCommand.ExecuteReader();
                if (myReader.Read())
                {
                    codigo = myReader.GetString(myReader.GetOrdinal("valor"));
                }
                myReader.Close();
                myCommand.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            conexion.Close();
            return codigo;
        }

        public string genCodArti(string codcia, string grparti, string letra)
        {
            Funciones funciones = new Funciones();
            string codigoInterno = "0001";
            string alfa = getAlfa(letra);
            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();
            string query = " select isnull(max(right(codarti,4)),'1') as existencia, ";
            query += "max(right(codarti,4))+1 as codigointerno from alarti ";
            query += "where codcia='" + @codcia + "' and left(codarti,2)='" + @grparti + "' and SUBSTRING(codarti,3,2)='" + @alfa + "' ";
            try
            {
                SqlCommand myCommand = new SqlCommand(query, conexion);
                SqlDataReader myReader = myCommand.ExecuteReader();
                if (myReader.Read())
                {
                    if (myReader.GetString(myReader.GetOrdinal("existencia")).Trim().Equals("1"))
                    {
                        codigoInterno = grparti + alfa + "0001";
                    }
                    else
                    {
                        string codigo = myReader.GetInt32(myReader.GetOrdinal("codigointerno")).ToString();
                        codigoInterno = grparti + alfa + funciones.replicateCadena("0", 4 - codigo.Trim().Length) + codigo;
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

        public void updAlArti(string operacion, string codcia, string codarti, string codartiba,
            string nombre, string especie, string codigo, string marca, string desarti,
            string unidad, float ancho, float largo, float peso, float altura, float diametro,
            string clasarti, string famarti, string grparti, string tipoarti, string fcrea,
            string fmod, string usuario, string estado, float prepubli, float predistri, float pordesc,
            string controlstk, string prelibre, string exoigv, string monpreven, string esporigv, string porigv,
            string ulargo, string uancho, string upeso, string ualtura, string udiametro, string isespecie,
            string isserie, string ismarca, string islargo, string isaltura, string isancho, string ispeso, string isdiametro, float pesokg)
        {
            string query;
            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();

            if (operacion.Equals("AGREGAR"))
            {
                query = "INSERT INTO alarti (codcia,codarti,codartiba,nombre,especie,codigo,marca,desarti,ancho,largo,peso,";
                query = query + " altura,diametro,clasarti,famarti,grparti,tipoarti,fcrea,";
                query = query + " fmod,usuario,estado,unidad,prepubli,predistri,pordesc,";
                query = query + " controlstk,prelibre,exoigv,monpreven,esporigv,porigv,uancho,ulargo,upeso,ualtura,udiametro,";
                query = query + " isespecie,isserie,ismarca,islargo,isaltura,isancho,ispeso,isdiametro,pesokg) ";
                query = query + " VALUES ('" + @codcia + "','" + @codarti + "', ";
                query = query + " '" + @codartiba + "', '" + @nombre + "','" + @especie + "','" + @codigo + "','" + @marca + "',";
                query = query + " '" + @desarti + "', '" + @ancho + "','" + @largo + "',";
                query = query + " '" + @peso + "','" + @altura + "','" + @diametro + "','" + @clasarti + "',";
                query = query + " '" + @famarti + "','" + @grparti + "','" + @tipoarti + "','" + @fcrea + "','" + @fmod + "',";
                query = query + " '" + @usuario + "','" + @estado + "','" + @unidad + "','" + @prepubli + "',";
                query = query + " '" + @predistri + "','" + @pordesc + "','" + @controlstk + "',";
                query = query + " '" + @prelibre + "','" + @exoigv + "','" + @monpreven + "','" + @esporigv + "','" + @porigv + "',";
                query = query + " '" + @uancho + "','" + @ulargo + "','" + @upeso + "','" + @ualtura + "','" + @udiametro + "','" + @isespecie + "',";
                query = query + " '" + @isserie + "','" + @ismarca + "','" + @islargo + "','" + @isaltura + "','" + @isancho + "','" + @ispeso + "','" + @isdiametro + "','" + @pesokg + "');";
            }
            else
            {
                query = " UPDATE alarti SET codartiba='" + @codartiba + "',desarti='" + @desarti + "',ancho='" + @ancho + "', ";
                query = query + " largo='" + @largo + "',peso='" + @peso + "',altura='" + @altura + "',diametro='" + @diametro + "',";
                query = query + " clasarti='" + @clasarti + "',famarti='" + @famarti + "',grparti='" + @grparti + "',";
                query = query + " tipoarti='" + @tipoarti + "',fmod='" + @fmod + "',usuario='" + @usuario + "',";
                query = query + " estado='" + @estado + "',unidad='" + @unidad + "',";
                query = query + " prepubli='" + @prepubli + "',predistri='" + @predistri + "',pordesc='" + @pordesc + "',";
                query = query + " controlstk='" + @controlstk + "',prelibre='" + @prelibre + "',exoigv='" + @exoigv + "',";
                query = query + " monpreven='" + @monpreven + "',esporigv='" + @esporigv + "',porigv='" + @porigv + "',";
                query = query + " nombre='" + @nombre + "',especie='" + @especie + "',codigo='" + @codigo + "',marca='" + @marca + "', ";
                query = query + " uancho='" + @uancho + "',ulargo='" + @ulargo + "',upeso='" + @upeso + "',ualtura='" + @ualtura + "',udiametro='" + @udiametro + "',";
                query = query + " isespecie='" + @isespecie + "',isserie='" + @isserie + "',";
                query = query + " ismarca='" + @ismarca + "',islargo='" + @islargo + "',isaltura='" + @isaltura + "',";
                query = query + " isancho='" + @isancho + "',ispeso='" + @ispeso + "',isdiametro='" + @isdiametro + "',pesokg='" + @pesokg + "' ";
                query = query + " WHERE /*codcia='" + @codcia + "' and */codarti='" + @codarti + "';";
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

        public void delArti(string codcia, string codarti)
        {
            string query;
            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();
            query = "DELETE from alarti WHERE /*codcia='" + @codcia + "' and */codarti='" + @codarti + "' ;";
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

        public void reasignaCodigo(string codcia, string old_codarti, string new_codarti)
        {
            string query;
            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();
            query = "update almovd set codarti='" + @new_codarti + "' WHERE codcia='" + @codcia + "' and codarti='" + @old_codarti + "' ;";
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

        public string getNumeroRegistrosAlarti(string codcia)
        {
            string numero = "0";
            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();
            string squery = "select count(*) as numero from alarti where codcia='" + @codcia + "';";
            try
            {
                SqlCommand myCommand = new SqlCommand(squery, conexion);
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

        public string ui_getDatos(string codcia, string codarti, string dato)
        {
            string resultado = string.Empty;

            if (codarti != string.Empty)
            {
                SqlConnection conexion = new SqlConnection();
                conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
                conexion.Open();

                string query = " Select A.codarti,A.codartiba,A.desarti,B.desmaesgen as familia, ";
                query += " C.desgrparti as grupo,A.unidad,A.controlstk,A.prelibre,";
                query += " A.prepubli,A.predistri,A.exoigv,A.esporigv,A.porigv,A.peso,A.pesokg ";
                query += " from alarti A left join ";
                query += " maesgen B on B.idmaesgen='110' and A.famarti=B.clavemaesgen ";
                query += " left join grparti C on A.famarti=C.famarti and A.grparti=C.grparti ";
                query += " where /*A.codcia='" + @codcia + "' and */(A.codarti='" + @codarti + "' OR A.codartiba='" + @codarti + "')";
                try
                {
                    SqlCommand myCommand = new SqlCommand(query, conexion);
                    SqlDataReader myReader = myCommand.ExecuteReader();

                    if (myReader.Read())
                    {
                        if (dato.Equals("CODIGO"))
                        {
                            resultado = myReader.GetString(myReader.GetOrdinal("codarti"));
                        }

                        if (dato.Equals("BARRAS"))
                        {
                            resultado = myReader.GetString(myReader.GetOrdinal("codartiba"));
                        }

                        if (dato.Equals("NOMBRE"))
                        {
                            resultado = myReader.GetString(myReader.GetOrdinal("desarti"));
                        }

                        if (dato.Equals("FAMILIA"))
                        {
                            resultado = myReader.GetString(myReader.GetOrdinal("familia"));
                        }

                        if (dato.Equals("GRUPO"))
                        {
                            resultado = myReader.GetString(myReader.GetOrdinal("grupo"));
                        }

                        if (dato.Equals("UNIDAD"))
                        {
                            resultado = myReader.GetString(myReader.GetOrdinal("unidad"));
                        }

                        if (dato.Equals("CONTROLSTK"))
                        {
                            resultado = myReader.GetString(myReader.GetOrdinal("controlstk"));
                        }

                        if (dato.Equals("PRECIOLIBRE"))
                        {
                            resultado = myReader.GetString(myReader.GetOrdinal("prelibre"));
                        }

                        if (dato.Equals("PRECIOPUBLICO"))
                        {
                            resultado = myReader.GetString(myReader.GetOrdinal("prepubli"));
                        }

                        if (dato.Equals("PRECIODISTRIBUIDOR"))
                        {
                            resultado = myReader.GetString(myReader.GetOrdinal("predistri"));
                        }
                        if (dato.Equals("EXOIGV"))
                        {
                            resultado = myReader.GetString(myReader.GetOrdinal("exoigv"));
                        }

                        if (dato.Equals("ESPORIGV"))
                        {
                            resultado = myReader.GetString(myReader.GetOrdinal("esporigv"));
                        }

                        if (dato.Equals("PORIGV"))
                        {
                            resultado = myReader.GetString(myReader.GetOrdinal("porigv"));
                        }

                        if (dato.Equals("PESO"))
                        {
                            resultado = myReader.GetString(myReader.GetOrdinal("peso"));
                        }

                        if (dato.Equals("PESOKG"))
                        {
                            resultado = myReader.GetString(myReader.GetOrdinal("pesokg"));
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

            return resultado;
        }
    }
}