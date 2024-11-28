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
    class PerPlan
    {   
        public PerPlan() { }

        public string generaCodigoInterno(string idcia){

            Funciones funciones = new Funciones();
            string codigoInterno = "00001";
            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();
            string query = "select isnull(max(idperplan)) as existencia,max(idperplan)+1 as codigointerno from perplan where (idcia='" + @idcia + "');";

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

        public string getNumeroRegistrosPerPlan(string idcia)
        {
            string numero="0";
            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();
            //string squery = "select count(*) as numero from perplan where idcia='" + @idcia + "';";
            string squery = "select count(*) as numero from perplan (nolock) where 1=1;";
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

        public string verificaPerPlanxDoc(string idcia,string tipdoc,string nrodoc)
        {
            string numero = "0";
            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();
            string squery = "select count(*) as numero from perplan where idcia='" + @idcia + "' and tipdoc='"+@tipdoc+"' and nrodoc='"+@nrodoc+"';";
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

        public void actualizaPerPlan(string soperacion,string idperplan, string idcia, string apepat, string apemat,
        string nombres, string fecnac, string tipdoc, string nrodoc,string paisemi, string disnac,string telfijo, string celular,
        string rpm, string estcivil, string nacion, string email, string catlic, string nrolic, string tipotrab,
        string idtipoper, string nivedu, string idlabper, string seccion, string regpen, string cuspp,
        string contrab, string tippag, string pering, string sitesp, string entfinrem, string nroctarem, string monrem,
        string tipctarem, string entfincts, string nroctacts, string moncts, string tipctacts, string tipvia, string nomvia,
        string nrovia,string deparvia ,string intvia,string manzavia,string lotevia,string kmvia,string block,string etapa, 
        string tipzona, string nomzona, string refzona, string ubigeo, string dscubigeo,
        string sexo,string ocurpts,string afiliaeps,string eps,string discapa,string sindica,string situatrab,
        string sctrsanin,string sctrsaessa,string sctrsaeps,string sctrpennin,string sctrpenonp,string sctrpenseg,
        string asigemplea,string rucemp,string estane,string idtipoplan,string esvida,string domicilia,string fecregpen,
        string regalterna,string trabmax,string trabnoc,string quicat,string renexo,string asepen,string apliconve,string exoquicat,
            string reglab, string ruc, string codaux, string chk_alta_sunat, string chk_baja_sunat)
        {
            string squery;

            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();

            if (soperacion.Equals("AGREGAR"))
            {
                squery = "INSERT INTO perplan (idperplan,  idcia,  apepat,  apemat,";
                squery = squery + " nombres,  fecnac,  tipdoc,  nrodoc,  telfijo,  celular,";
                squery = squery + " rpm,  estcivil,  nacion,  email,  catlic,  nrolic,  tipotrab,";
                squery = squery + " idtipoper,  nivedu, idlabper,  seccion,  regpen,  cuspp,";
                squery = squery + " contrab,  tippag,  pering,  sitesp,  entfinrem,  nroctarem,  monrem,";
                squery = squery + " tipctarem,  entfincts,  nroctacts,  moncts,  tipctacts,  tipvia,  nomvia,";
                squery = squery + " nrovia,  intvia,  tipzona,  nomzona,  refzona,  ubigeo,  dscubigeo,";
                squery = squery + " sexo,ocurpts,afiliaeps,eps,discapa,sindica,situatrab,sctrsanin,sctrsaessa, ";
                squery = squery + " sctrsaeps,sctrpennin,sctrpenonp,sctrpenseg,asigemplea,rucemp,estane,idtipoplan, ";
                squery = squery + " esvida,domicilia,fecregpen,regalterna,trabmax,trabnoc,quicat,renexo,";
                squery = squery + " asepen,apliconve,exoquicat,paisemi,disnac,deparvia,manzavia,lotevia,kmvia,";
                squery = squery + " block,etapa,reglab,ruc,codaux,alta_tregistro,baja_tregistro) ";
                squery = squery + " VALUES ('" + @idperplan + "','" + @idcia + "','" + @apepat + "','" + @apemat;
                squery = squery + "','" + @nombres + "'," + " STR_TO_DATE('" + @fecnac + "', '%d/%m/%Y') " + ",'" + @tipdoc + "','" + @nrodoc + "','" + @telfijo + "','" + @celular;
                squery = squery + "','" + @rpm + "','" + @estcivil + "','" + @nacion + "','" + @email + "','" + @catlic + "','" + @nrolic + "','" + @tipotrab;
                squery = squery + "','" + @idtipoper + "','" + @nivedu + "','" + @idlabper + "','" + @seccion + "','" + @regpen + "','" + @cuspp;
                squery = squery + "','" + @contrab + "','" + @tippag + "','" + @pering + "','" + @sitesp + "','" + @entfinrem + "','" + @nroctarem + "','" + @monrem;
                squery = squery + "','" + @tipctarem + "','" + @entfincts + "','" + @nroctacts + "','" + @moncts + "','" + @tipctacts + "','" + @tipvia + "','" + @nomvia;
                squery = squery + "','" + @nrovia + "','" + @intvia + "','" + @tipzona + "','" + @nomzona + "','" + @refzona + "','" + @ubigeo + "','" + @dscubigeo;
                squery = squery + "','" + @sexo + "','" + @ocurpts + "','" + @afiliaeps + "','" + @eps + "','" + @discapa + "','" + @sindica + "','" + @situatrab;
                squery = squery + "','" + @sctrsanin + "','" + @sctrsaessa + "','" + @sctrsaeps + "','" + @sctrpennin + "','" + @sctrpenonp;
                squery = squery + "','" + @sctrpenseg + "','" + @asigemplea + "','" + @rucemp + "','" + @estane + "','" + @idtipoplan;
                squery = squery + "','" + @esvida + "','" + @domicilia + "', STR_TO_DATE('" + @fecregpen + "', '%d/%m/%Y'),'" + @regalterna;
                squery = squery + "','" + @trabmax + "','" + @trabnoc + "','" + @quicat + "','" + @renexo + "','" + @asepen + "',";
                squery = squery + " '" + @apliconve + "','" + @exoquicat + "','" + @paisemi + "','" + @disnac + "',";
                squery = squery + " '" + @deparvia + "','" + @manzavia + "','" + @lotevia + "','" + @kmvia + "',";
                squery = squery + " '" + @block + "','" + @etapa + "','" + @reglab + "','" + @ruc + "','" + @codaux + "'," + chk_alta_sunat + "," + chk_baja_sunat + ");";
            }
            else
            {
                squery = "UPDATE perplan SET apepat='" + @apepat + "',apemat='" + @apemat + "',";
                squery = squery + "nombres='" + @nombres + "',fecnac=STR_TO_DATE('" + @fecnac + "', '%d/%m/%Y')" + ",tipdoc='" + @tipdoc + "',nrodoc='" + @nrodoc + "',telfijo='" + @telfijo + "',celular='" + @celular + "',";
                squery = squery + "rpm='" + @rpm + "',estcivil='" + @estcivil + "',nacion='" + @nacion + "',email='" + @email + "',catlic='" + @catlic + "',nrolic='" + @nrolic + "',tipotrab='" + @tipotrab + "',";
                squery = squery + "idtipoper='" + @idtipoper + "',nivedu='" + @nivedu + "',idlabper='" + @idlabper + "',seccion='" + @seccion + "',regpen='" + @regpen + "',cuspp='" + @cuspp + "',";
                squery = squery + "contrab='" + @contrab + "',tippag='" + @tippag + "',pering='" + @pering + "',sitesp='" + @sitesp + "',entfinrem='" + @entfinrem + "',nroctarem='" + @nroctarem + "',monrem='" + @monrem + "',";
                squery = squery + "tipctarem='" + @tipctarem + "',entfincts='" + @entfincts + "',nroctacts='" + @nroctacts + "',moncts='" + @moncts + "',tipctacts='" + @tipctacts + "',tipvia='" + @tipvia + "',nomvia='" + @nomvia + "',";
                squery = squery + "nrovia='" + @nrovia + "',intvia='" + @intvia + "',tipzona='" + @tipzona + "',nomzona='" + @nomzona + "',refzona='" + @refzona + "',ubigeo='" + @ubigeo + "',dscubigeo='" + @dscubigeo + "',";
                squery = squery + "esvida='" + @esvida + "',domicilia='" + @domicilia + "',sexo='" + @sexo + "',ocurpts='" + @ocurpts + "',afiliaeps='" + @afiliaeps + "',eps='" + @eps + "',discapa='" + @discapa + "',";
                squery = squery + "sindica='" + @sindica + "',situatrab='" + @situatrab + "',sctrsanin='" + @sctrsanin + "',sctrsaessa='" + @sctrsaessa + "',sctrsaeps='" + @sctrsaeps + "',sctrpennin='" + @sctrpennin + "',";
                squery = squery + "sctrpenonp='" + @sctrpenonp + "',sctrpenseg='" + @sctrpenseg + "',asigemplea='" + @asigemplea + "',rucemp='" + @rucemp + "',estane='" + @estane + "',idtipoplan='" + @idtipoplan + "',";
                squery = squery + "fecregpen=STR_TO_DATE('" + @fecregpen + "', '%d/%m/%Y'),regalterna='" + @regalterna + "',trabmax='" + @trabmax + "',trabnoc='" + @trabnoc + "',quicat='" + @quicat + "',";
                squery = squery + "renexo='" + @renexo + "',asepen='" + @asepen + "',paisemi='" + @paisemi + "',disnac='" + @disnac + "',deparvia='" + @deparvia + "',manzavia='" + @manzavia + "',lotevia='" + @lotevia + "',kmvia='" + @kmvia + "',block='" + @block + "',etapa='" + @etapa + "',";
                squery = squery + "apliconve='" + @apliconve + "',exoquicat='" + @exoquicat + "',reglab='" + @reglab + "',ruc='" + @ruc + "',codaux='" + @codaux + "', alta_tregistro=" + chk_alta_sunat + ", baja_tregistro=" + chk_baja_sunat + " ";
                squery = squery + " WHERE idcia='" + @idcia + "' and idperplan='" + @idperplan + "';"; ;
            }

            try
            {
                SqlCommand myCommand = new SqlCommand(squery, conexion);
                myCommand.ExecuteNonQuery();
                myCommand.Dispose();
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            conexion.Close();
        }

        public void eliminarPerPlan(string idcia, string idperplan)
        {
            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();

            string squery = " DELETE from confijos WHERE idcia='" + @idcia + "' and idperplan='" + @idperplan + "';";
            //squery += " DELETE from asiste WHERE idcia='" + @idcia + "' and idperplan='" + @idperplan + "';";
            squery += " DELETE from datasis WHERE idcia='" + @idcia + "' and idperplan='" + @idperplan + "';";
            squery += " DELETE from dataplan WHERE idcia='" + @idcia + "' and idperplan='" + @idperplan + "';";
            squery += " DELETE from derhab WHERE idcia='" + @idcia + "' and idperplan='" + @idperplan + "';";
            squery += " DELETE from desjud WHERE idcia='" + @idcia + "' and idperplan='" + @idperplan + "';";
            squery += " DELETE from desplan WHERE idcia='" + @idcia + "' and idperplan='" + @idperplan + "';";
            squery += " DELETE from diassubsi WHERE idcia='" + @idcia + "' and idperplan='" + @idperplan + "';";
            squery += " DELETE from fonpenper WHERE idcia='" + @idcia + "' and idperplan='" + @idperplan + "';";
            squery += " DELETE from plan_ WHERE idcia='" + @idcia + "' and idperplan='" + @idperplan + "';";
            squery += " DELETE from presper WHERE idcia='" + @idcia + "' and idperplan='" + @idperplan + "';";
            squery += " DELETE from remu WHERE idcia='" + @idcia + "' and idperplan='" + @idperplan + "';";
            squery += " DELETE from condataplan WHERE idcia='" + @idcia + "' and idperplan='" + @idperplan + "';";
            squery += " DELETE from perlab WHERE idcia='" + @idcia + "' and idperplan='" + @idperplan + "';";
            squery += " DELETE from cenper WHERE idcia='" + @idcia + "' and idperplan='" + @idperplan + "';";
            squery += " DELETE from quicat WHERE idcia='" + @idcia + "' and idperplan='" + @idperplan + "';";
            squery += " DELETE from conbol WHERE idcia='" + @idcia + "' and idperplan='" + @idperplan + "';";
            squery += " DELETE from perplan WHERE idcia='" + @idcia + "' and idperplan='" + @idperplan + "';";
            squery += " DELETE from tareo WHERE idcia='" + @idcia + "' and idperplan='" + @idperplan + "';";
            squery += " DELETE from regvac WHERE idcia='" + @idcia + "' and idperplan='" + @idperplan + "';";
            squery += " DELETE from predesplan WHERE idcia='" + @idcia + "' and idperplan='" + @idperplan + "';";

            try
            {
                SqlCommand myCommand = new SqlCommand(squery, conexion);
                myCommand.ExecuteNonQuery();
                myCommand.Dispose();

            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            conexion.Close();
        }

        public string ui_getDatosPerPlan(string idcia, string idperplan, string datossolicitado)
        {
            string query;
            string resultado=string.Empty;
            
            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();

            query = " Select A.idperplan,B.cortotipoper,C.Parm1maesgen as cortotipodoc,A.nrodoc,";
            query += "RTRIM(A.apepat)+' '+RTRIM(A.apemat)+', '+RTRIM(A.nombres) as nombre,";
            query += "G.fechaini,CASE WHEN G.fechafin IS NULL THEN '' ELSE G.fechafin END as fechafin,";
            query += "A.idcia,H.rucemp,H.razonemp,J.idestane,J.desestane,J.riesgo,A.sexo,A.fecnac,A.afiliaeps,A.exoquicat, ";
            query += "K.desmaesgen as despering from perplan A (NOLOCK) left join tipoper B (NOLOCK) on A.idtipoper=B.idtipoper ";
            query += "left join maesgen C (NOLOCK) on C.idmaesgen='002' and A.tipdoc=C.clavemaesgen ";
            query += "left join view_perlab F (NOLOCK) on A.idcia=F.idcia and A.idperplan=F.idperplan  ";
            query += "left join perlab G (NOLOCK) on F.idcia=G.idcia and F.idperplan=G.idperplan and F.idperlab=G.idperlab ";
            query += "left join emplea H (NOLOCK) on A.rucemp=H.rucemp and A.idcia=H.idciafile ";
            query += "left join estane J (NOLOCK) on A.rucemp=J.codemp and A.estane=J.idestane ";
            query += "left join maesgen K (NOLOCK) on K.idmaesgen='011' and A.pering=K.clavemaesgen ";
            query += "where A.idcia='" + @idcia + "' and A.idperplan='" + @idperplan + "' order by idperplan asc;";

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
                    if (datossolicitado.Equals("4"))
                    {
                        resultado = myReader["fechaini"].ToString();
                    }

                    if (datossolicitado.Equals("5"))
                    {
                        resultado = myReader["idestane"] + "  " + myReader["desestane"];
                    }
                    
                    if (datossolicitado.Equals("6"))
                    {
                        if (myReader["riesgo"].Equals("1"))
                        {
                            resultado = "SI";
                        }
                        else
                        {
                            resultado = "NO";
                        }
                    }
                    if (datossolicitado.Equals("7"))
                    {
                        resultado = myReader["sexo"].ToString();
                    }
                    
                    if (datossolicitado.Equals("8"))
                    {
                        resultado = myReader["fechafin"].ToString();
                    }

                    if (datossolicitado.Equals("9"))
                    {
                        resultado = myReader["fecnac"].ToString();
                    }

                    if (datossolicitado.Equals("AFILIAEPS"))
                    {
                        resultado = myReader["afiliaeps"].ToString();
                    }

                    if (datossolicitado.Equals("EXOQUICAT"))
                    {
                        resultado = myReader["exoquicat"].ToString();
                    }

                    if (datossolicitado.Equals("PERIODICIDAD_INGRESOS"))
                    {
                        resultado = myReader["despering"].ToString();
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

        public string ui_getDatosPerPlan(string idperplan, string datossolicitado)
        {
            string query;
            string resultado = string.Empty;

            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();

            query = " Select A.idperplan,B.cortotipoper,C.Parm1maesgen as cortotipodoc,A.nrodoc,";
            query += "RTRIM(A.apepat)+' '+RTRIM(A.apemat)+', '+RTRIM(A.nombres) as nombre,";
            query += "G.fechaini,G.fechafin,A.idcia,H.rucemp,H.razonemp,J.idestane,J.desestane,J.riesgo,A.sexo,A.fecnac,A.afiliaeps,A.exoquicat, ";
            query += "K.desmaesgen as despering,RTRIM(A.celular) celular from perplan A left join tipoper B on A.idtipoper=B.idtipoper ";
            query += "left join maesgen C on C.idmaesgen='002' and A.tipdoc=C.clavemaesgen ";
            query += "left join view_perlab F on A.idcia=F.idcia collate Modern_Spanish_CI_AI and A.idperplan=F.idperplan collate Modern_Spanish_CI_AI ";
            query += "left join perlab G on F.idcia=G.idcia and F.idperplan=G.idperplan and F.idperlab=G.idperlab ";
            query += "left join emplea H on A.rucemp=H.rucemp and A.idcia=H.idciafile ";
            query += "left join estane J on A.rucemp=J.codemp and A.estane=J.idestane ";
            query += "left join maesgen K on K.idmaesgen='011' and A.pering=K.clavemaesgen ";
            query += "where (A.idperplan='" + @idperplan + "' OR A.nrodoc='" + @idperplan + "') order by idperplan asc;";

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
                    if (datossolicitado.Equals("4"))
                    {
                        resultado = myReader["fechaini"].ToString();
                    }

                    if (datossolicitado.Equals("5"))
                    {
                        resultado = myReader["idestane"] + "  " + myReader["desestane"];
                    }

                    if (datossolicitado.Equals("6"))
                    {
                        if (myReader["riesgo"].Equals("1"))
                        {
                            resultado = "SI";
                        }
                        else
                        {
                            resultado = "NO";
                        }
                    }
                    if (datossolicitado.Equals("7"))
                    {
                        resultado = myReader["sexo"].ToString();
                    }

                    if (datossolicitado.Equals("8"))
                    {
                        resultado = myReader["fechafin"].ToString();
                    }

                    if (datossolicitado.Equals("9"))
                    {
                        resultado = myReader["fecnac"].ToString();
                    }

                    if (datossolicitado.Equals("AFILIAEPS"))
                    {
                        resultado = myReader["afiliaeps"].ToString();
                    }

                    if (datossolicitado.Equals("EXOQUICAT"))
                    {
                        resultado = myReader["exoquicat"].ToString();
                    }

                    if (datossolicitado.Equals("PERIODICIDAD_INGRESOS"))
                    {
                        resultado = myReader["despering"].ToString();
                    }

                    if (datossolicitado.Equals("celular"))
                    {
                        resultado = myReader["celular"].ToString();
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

        public string ui_getDatosPerPlan2(string idperplan, string datossolicitado)
        {
            string query;
            string resultado = string.Empty;

            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();

            query = " Select A.idperplan,B.cortotipoper,C.Parm1maesgen as cortotipodoc,A.nrodoc,";
            query += "RTRIM(A.apepat) + ' ' + RTRIM(A.apemat) + ', ' + RTRIM(A.nombres) as nombre,";
            query += "G.fechaini,G.fechafin,A.idcia,H.rucemp,H.razonemp";
            query += "from perplan_historia A left join tipoper B on A.idtipoper = B.idtipoper";
            query += "left join maesgen C on C.idmaesgen = '002' and A.tipdoc = C.clavemaesgen";
            query += "left join view_perlab F on A.idcia = F.idcia collate Modern_Spanish_CI_AI and A.idperplan = F.idperplan collate Modern_Spanish_CI_AI";
            query += "left join perlab G on F.idcia = G.idcia and F.idperplan = G.idperplan and F.idperlab = G.idperlab";
            query += "left join emplea H on A.idcia = H.idciafile";
            query += "left join maesgen K on K.idmaesgen = '011'";
            query += "where(A.idperplan ='" + @idperplan + "' OR A.nrodoc ='" + @idperplan + "')";
            query += "GROUP by A.idperplan,B.cortotipoper,C.Parm1maesgen,A.nrodoc,apepat,apemat,nombres,";
            query += "G.fechaini,G.fechafin,A.idcia,H.rucemp,H.razonemp";
            query += "order by idperplan asc;";



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
                    if (datossolicitado.Equals("4"))
                    {
                        resultado = myReader["fechaini"].ToString();
                    }
                    if (datossolicitado.Equals("8"))
                    {
                        resultado = myReader["fechafin"].ToString();
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


        public string ui_verificaInformacionPerPlan(string idcia, string idperplan, string datossolicitado)
        {
            string query=string.Empty;
            string resultado = "1";

            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();
            if (datossolicitado.Equals("PL"))
            {
                query = "Select idperlab from perlab where idcia='" + @idcia + "' and  idperplan='" + @idperplan + "' ;";
            }
            else
            {
                if (datossolicitado.Equals("FP"))
                {
                    query = "Select  idfonpenper from fonpenper where idcia='" + @idcia + "' and idperplan='" + @idperplan + "' and statefonpenper='V';";
                }
                else
                {
                    if (datossolicitado.Equals("RB"))
                    {
                        query = "Select idremu from remu where idcia='" + @idcia + "' and idperplan='" + @idperplan + "' and state='V';";
                    }
                }
            }
            try
            {
                SqlCommand myCommand = new SqlCommand(query, conexion);
                SqlDataReader myReader = myCommand.ExecuteReader();
                if (myReader.Read())
                {
                    resultado = "0";

                }
                
                myReader.Close();
                myCommand.Dispose();
            }
            catch (Exception) { }
            conexion.Close();
            return resultado;
        }

        /////////////////////////////////////// 0 EXISTE   1 NO EXISTE //////////////////////////////////////////
        public string ui_verificaPerPlan_DataPlan(string idcia, string idtipocal, string idtipoper, string messem, string anio, string rucemp, string estane, string idtipoplan)
        {
            string query;
            string resultado = "0";
            string idperplan;
            string nombre;
            string condicionEstablecimiento = string.Empty;

            if (estane!="X")
            {
                condicionEstablecimiento = " and A.estane='" + @estane + "' ";
            }
            DataTable dataplan = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();
            query = "select A.idcia,A.idperplan,CONCAT(B.apepat,' ',B.apemat,' , ',B.nombres) as nombre ";
            query = query + " from dataplan A inner join perplan B on ";
            query = query + " A.idcia=B.idcia and A.idperplan=B.idperplan ";
            query = query + " where A.idcia='" + @idcia + "' and A.idtipocal='" + @idtipocal + "' ";
            query = query + " and A.idtipoper='" + @idtipoper + "' and A.messem='" + @messem + "' ";
            query = query + " and A.anio='" + @anio + "' and A.emplea='" + @rucemp + "' ";
            query = query + condicionEstablecimiento + " and A.idtipoplan='" + @idtipoplan + "'  ;";
            da.SelectCommand = new SqlCommand(query, conexion);
            da.Fill(dataplan);

            int i = 0;
            while (i < dataplan.Rows.Count)
            {
                idperplan = dataplan.Rows[i]["idperplan"].ToString();
                nombre = dataplan.Rows[i]["nombre"].ToString();
                PerPlan perplan = new PerPlan();
                resultado = perplan.ui_verificaInformacionPerPlan(idcia, idperplan, "PL");
                if (resultado.Equals("0"))
                {
                    resultado = perplan.ui_verificaInformacionPerPlan(idcia, idperplan, "FP");
                    if (resultado.Equals("1"))
                    {
                        MessageBox.Show("No ha especificado un Fondo de Pensiones para el trabajador con Código " + idperplan + " - " + nombre, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                    }
                    else
                    {
                        resultado = perplan.ui_verificaInformacionPerPlan(idcia, idperplan, "RB");
                        if (resultado.Equals("1"))
                        {
                            MessageBox.Show("No ha especificado Remuneración o Jornal Básico para el trabajador con Código " + idperplan + " - " + nombre, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;
                        }
                    }
                }
                else
                {
                    MessageBox.Show("No ha especificado un Periodo Laboral para el trabajador con Código " + idperplan + " " + nombre, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
                }

                i++;

            }
            return resultado;
        }

        public string ui_verificaPerPlan(string idcia, string idperplan)
        {
            string resultado = "0";
                
            PerPlan perplan = new PerPlan();
            resultado = perplan.ui_verificaInformacionPerPlan(idcia, idperplan, "PL");
            if (resultado.Equals("0"))
            {
                resultado = perplan.ui_verificaInformacionPerPlan(idcia, idperplan, "FP");
                if (resultado.Equals("1"))
                {
                    MessageBox.Show("No ha especificado un Fondo de Pensiones para el trabajador ", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    resultado = perplan.ui_verificaInformacionPerPlan(idcia, idperplan, "RB");
                    if (resultado.Equals("1"))
                    {
                        MessageBox.Show("No ha especificado Remuneración o Jornal Básico para el trabajador ", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
              
           return resultado;
        }

        public void asignaHorario(string idcia, string bd, string idperplan, string idTipHorario, int idAsigTipHorPer)
        {
            string squery;
            idTipHorario = (idTipHorario == "") ? "0" : idTipHorario.Substring(0, 1);
            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();
            squery = "EXEC sp_AsignaHorPer '" + idcia + "','" + bd + "','" + idperplan + "'," + idAsigTipHorPer + ", " + idTipHorario + " ";
            try
            {
                using (SqlCommand myCommand = new SqlCommand(squery, conexion))
                {
                    myCommand.ExecuteNonQuery();
                    myCommand.Dispose();
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            conexion.Close();
        }

        public void actualizaTrabajador(string operacion, string idperplan, string idcia, string apepat, string apemat, string nombres, 
            string fecnac, string tipdoc, string nrodoc, string telfijo, string celular, string estcivil, string email, string codaux, 
            string sexo, string idtipoper, string idlabper, string seccion)
        {
            string squery;

            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();

            if (operacion.Equals("AGREGAR"))
            {
                squery = "INSERT INTO perplan (idperplan,  idcia,  apepat,  apemat,";
                squery += " nombres,  fecnac,  tipdoc,  nrodoc,  telfijo,  celular,";
                squery += " rpm,  estcivil,  nacion,  email,  catlic,  nrolic,  tipotrab,";
                squery += " idtipoper,  nivedu, idlabper,  seccion,  regpen,  cuspp,";
                squery += " contrab,  tippag,  pering,  sitesp,  entfinrem,  nroctarem,  monrem,";
                squery += " tipctarem,  entfincts,  nroctacts,  moncts,  tipctacts,  tipvia,  nomvia,";
                squery += " nrovia,  intvia,  tipzona,  nomzona,  refzona,  ubigeo,  dscubigeo,";
                squery += " sexo,ocurpts,afiliaeps,eps,discapa,sindica,situatrab,sctrsanin,sctrsaessa, ";
                squery += " sctrsaeps,sctrpennin,sctrpenonp,sctrpenseg,asigemplea,rucemp,estane,idtipoplan, ";
                squery += " esvida,domicilia,fecregpen,regalterna,trabmax,trabnoc,quicat,renexo,";
                squery += " asepen,apliconve,exoquicat,paisemi,disnac,deparvia,manzavia,lotevia,kmvia,";
                squery += " block,etapa,reglab,ruc,codaux,alta_tregistro,baja_tregistro) ";
                squery += " VALUES ('" + @idperplan + "','" + @idcia + "','" + @apepat + "','" + @apemat;
                squery += "','" + @nombres + "'," + "'" + @fecnac + "' " + ",'" + @tipdoc + "','" + @nrodoc + "','" + @telfijo + "','" + @celular;
                squery += "','','','','" + @email + "','','',''";
                squery += ",'" + @idtipoper + "','','" + @idlabper + "','" + @seccion + "','',''";
                squery += ",'','','','','','','','','','','','" + @sexo + "','','','','','','','','','','','','','','','',''";
                squery += ",'','','','','','','','','','','','', '',''";
                squery += ",'','','','','','','','','','','','','','','','','','" + @codaux + "',0,0);";
            }
            else
            {
                squery = "UPDATE perplan SET apepat='" + @apepat + "',apemat='" + @apemat + "',";
                squery += "nombres='" + @nombres + "',fecnac= cast('" + @fecnac + "' as date),tipdoc='" + @tipdoc + "',nrodoc='" + @nrodoc + "',telfijo='" + @telfijo + "',celular='" + @celular + "',";
                squery += "rpm='',estcivil='" + @estcivil + "',nacion='',email='" + @email + "',catlic='',nrolic='',tipotrab='',";
                squery += "idtipoper='" + @idtipoper + "',nivedu='',idlabper='" + @idlabper + "',seccion='" + @seccion + "',regpen='',cuspp='',";
                squery += "contrab='',tippag='',pering='',sitesp='',entfinrem='',nroctarem='',monrem='',";
                squery += "tipctarem='',entfincts='',nroctacts='',moncts='',tipctacts='',tipvia='',nomvia='',";
                squery += "nrovia='',intvia='',tipzona='',nomzona='',refzona='',ubigeo='',dscubigeo='',";
                squery += "esvida='',domicilia='',sexo='" + @sexo + "',ocurpts='',afiliaeps='',eps='',discapa='',";
                squery += "sindica='',situatrab='',sctrsanin='',sctrsaessa='',sctrsaeps='',sctrpennin='',";
                squery += "sctrpenonp='',sctrpenseg='',asigemplea='',rucemp='',estane='',idtipoplan='',";
                squery += "fecregpen='',regalterna='',trabmax='',trabnoc='',quicat='',";
                squery += "renexo='',asepen='',paisemi='',disnac='',deparvia='',manzavia='',lotevia='',kmvia='',block='',etapa='',";
                squery += "apliconve='',exoquicat='',reglab='',ruc='',codaux='" + @codaux + "', alta_tregistro=0, baja_tregistro=0 ";
                squery += " WHERE idcia='" + @idcia + "' and idperplan='" + @idperplan + "';"; ;
            }

            try
            {
                SqlCommand myCommand = new SqlCommand(squery, conexion);
                myCommand.ExecuteNonQuery();
                myCommand.Dispose();
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            conexion.Close();
        }

        public void consultaHorario(string campo, ComboBox cb)
        {
            string valorItem;
            Funciones fn = new Funciones();
            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();
            string query = "SELECT idplantiphorario as clave, descripcion FROM plantiphorario WHERE idplantiphorario='" + @campo + "';";
            try
            {
                SqlCommand cmd = new SqlCommand(query, conexion);
                DataTable dt = new DataTable();
                dt.Load(cmd.ExecuteReader());
                cmd.Dispose();
                if (dt.Rows.Count > 0)
                {
                    valorItem = (String)dt.Rows[0]["clave"];
                    cb.Text = valorItem + fn.replicateCadena(" ", (5 - valorItem.Length)) + (String)dt.Rows[0]["descripcion"];
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
    }
}