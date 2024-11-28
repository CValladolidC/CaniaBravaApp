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
    class Destajo
    {
        public Destajo() { }
                   
        public void actualizarDestajo(string tipopregistro,string soperacion,string idcia,string idperplan,string messem,
        string anio,string idtipocal,string idtipoper,
        string fecha,string idproddes,string idzontra,float cantidad,float precio,
            float subtotal,float movilidad,float refrigerio,float adicional,float total,
            string idtipoplan,string emplea,string estane,string iddestajo,string glosa,string codvar)
        {
            string query;
            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();
            string nombreDes = string.Empty;
            
            if (tipopregistro.Equals("P"))
            {
                nombreDes = "desplan";
            }
            else
            {
                nombreDes = "desret";
            }

            if (soperacion.Equals("AGREGAR"))
            {
                query = "INSERT INTO "+@nombreDes+"(idcia,idperplan,messem,anio,idtipocal,idtipoper,fecha ";
                query = query + ",idproddes,idzontra,cantidad,precio,subtotal,movilidad,refrigerio,adicional,total,idtipoplan,emplea,";
                query=query+" estane,glosa,codvar) VALUES (";
                query = query + "'" + @idcia + "', '" + @idperplan + "', '" + @messem + "','" + @anio + "',";
                query = query + "'" + @idtipocal + "','" + @idtipoper + "',STR_TO_DATE('" + @fecha + "', '%d/%m/%Y'),";
                query = query + "'" + @idproddes + "','" + @idzontra + "','" + @cantidad + "','" + @precio + "','" + @subtotal + "','" + @movilidad + "',";
                query = query + "'" + @refrigerio + "','" + @adicional + "','" + @total + "','" + @idtipoplan + "',";
                query = query + "'" + @emplea + "','" + @estane + "','" + @glosa + "','"+@codvar+"');";
            }
            else
            {
                query = "UPDATE "+@nombreDes+" SET cantidad='" + @cantidad + "',precio='" + @precio + "',subtotal='"+@subtotal+"',movilidad='"+@movilidad+"',";
                query = query + "refrigerio='" + @refrigerio + "',adicional='" + @adicional + "',total='" + @total + "',";
                query = query + " emplea='" + @emplea + "',estane='" + @estane + "',glosa='"+@glosa+"',codvar='"+@codvar+"' where iddestajo='" + @iddestajo + "';";
                MessageBox.Show(query);
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

        public void eliminarDestajo(string tipopregistro,string iddestajo)
        {
            string squery;

            string nombreDes = string.Empty;
            
            if (tipopregistro.Equals("P"))
            {
                nombreDes = "desplan";
            }
            else
            {
                nombreDes = "desret";
            }

            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();

            squery = "DELETE from " + @nombreDes + " where iddestajo='"+@iddestajo+"';";

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

        public string ui_getProduccionDestajo(string idcia,string idproddes,string idzontra,string messem,string anio,string fecha,string idtipocal,string idtipoper,string tiporegistro,string dato,string emplea,string estane)
        {
            string nombreTablaRet = string.Empty;
            string nombreTablaPer = string.Empty;
            string resultado = "0";

            if (tiporegistro.Equals("R"))
            {
                nombreTablaRet = "desret";
                nombreTablaPer = "perret";
            }
            else
            {
                nombreTablaRet = "desplan";
                nombreTablaPer = "perplan";
            }

            string query_resumen = "select A.idzontra,B.deszontra,SUM(A.cantidad) as cantidad,SUM(A.total) as total from " + @nombreTablaRet + " A left join zontra B on A.idzontra=B.idzontra and A.idcia=B.idcia ";
            query_resumen = query_resumen + " where A.idcia='" + @idcia + "' and idtipocal='" + @idtipocal + "' and A.idtipoper='" + @idtipoper + "' and A.idzontra='"+@idzontra+"'";
            query_resumen = query_resumen + " and A.messem='" + @messem + "' and A.anio='" + @anio + "' and A.idproddes='" + @idproddes + "' and A.emplea='"+@emplea+"' and A.estane='"+@estane+"' " ;
            query_resumen = query_resumen + " and A.fecha=" + " STR_TO_DATE('" + @fecha + "', '%d/%m/%Y') ";
            query_resumen = query_resumen + " group by A.idzontra,B.deszontra; ";

            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();

            try
            {
                SqlCommand myCommand = new SqlCommand(query_resumen, conexion);
                SqlDataReader myReader = myCommand.ExecuteReader();

                if (myReader.Read())
                {
                    if (dato.Equals("0"))
                    {
                        resultado = myReader["cantidad"].ToString();
                    }
                    if (dato.Equals("1"))
                    {
                        resultado = myReader["total"].ToString();
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

        public string ui_getValorConceptoDestajo(string idcia, string idperplan, string messem, 
            string anio, string idtipocal, string idtipoper, string idtipoplan,string idconplan, 
            string tipo,string emplea,string estane)
        {
            
            string resultado = "0";
            string nombreCampo;

            if (tipo.Equals("C"))
            {
                nombreCampo = "B.conplancan";
            }
            else
            {
                nombreCampo = "B.conplanimp";
            }

            string query = " select SUM(A.cantidad) as cantidad,SUM(A.total) as total,";
            query = query + " B.conplancan,";
            query = query + " B.conplanimp from tareo A inner join detproddes B on ";
            query = query + " A.idproddes=B.idproddes ";
            query = query + " and A.idcia=B.idcia and A.idtipoplan=B.idtipoplan and A.idtipocal=B.idtipocal ";
            query = query + " where A.idcia='" + @idcia + "' and A.idtipocal='" + @idtipocal + "' ";
            query = query + " and A.idtipoper='" + @idtipoper + "' and A.idtipoplan='" + @idtipoplan + "' ";
            query = query + " and " + nombreCampo + "='" + @idconplan + "'";
            query = query + " and A.idperplan='" + @idperplan + "' and A.messem='" + @messem + "' ";
            query = query + " and A.anio='" + @anio + "' ";
            query = query + " group by B.conplancan,B.conplanimp; ";

            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();

            try
            {
                SqlCommand myCommand = new SqlCommand(query, conexion);
                SqlDataReader myReader = myCommand.ExecuteReader();

                if (myReader.Read())
                {
                    if (tipo.Equals("C"))
                    {
                        resultado = myReader["cantidad"].ToString();
                    }
                    else
                    {
                        resultado = myReader["total"].ToString();
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

        public void eliminarDestajoPlan(string idcia, string idperplan, string messem,
        string anio, string idtipocal, string idtipoper, string idproddes, string idzontra, string iddestajo)
        {
            string squery;

            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();

            squery = "DELETE from desplan where idcia='" + @idcia + "' and idperplan='" + @idperplan;
            squery = squery + "' and messem='" + @messem + "' and anio='" + @anio + "' and idtipocal='" + @idtipocal + "'";
            squery = squery + " and idtipoper='" + @idtipoper + "' and idproddes='" + @idproddes + "'";
            squery = squery + " and idzontra='" + @idzontra + "' and iddestajo='" + @iddestajo + "';";

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

        public void ui_predesplan_to_dataplan(string idcia, string messem, string anio, string idtipocal, string idtipoper, string idtipoplan,string emplea,string estane)

        {
            string idperplan=string.Empty;
            string idconplan=string.Empty;
            string tipocalculo=string.Empty;
            string idproddes=string.Empty;
            string idconplandes = string.Empty;
            string tipocalculodes = string.Empty;
            string idzontra=string.Empty;
            string glosa=string.Empty;
            string tipo=string.Empty;
            string idpresper = string.Empty;
            string comen=string.Empty;
           
            int diasefelab,diassubsi,diasnosubsi,diastotal,diasdom,diurno,diascitt;
            string subsi,motivosubsi,citt,fechainisubsi,fechafinsubsi;
            string nosubsi, motivonosubsi, fechaininosubsi, fechafinnosubsi;
            float cantidad,precio,total;
            int riesgo=0,hext25=0,hext35=0,hext100=0,diasvac=0,nocturno=0;
            string finivac = "", ffinvac = "", regvac = "N",pervac="";
            float valor=0;
            float valordes = 0;

            string query;
            DataTable dtpredesplan = new DataTable();
            DataTable dtdesplan = new DataTable();

            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();

            query = "  select A.idperplan,A.diasefelab,A.diassubsi,A.diasnosubsi,A.diastotal,A.diasdom, ";
            query += " SUM(B.cantidad) as cantidad,SUM(B.total) as total,A.idconplan,A.tipocalculo,";
            query += " A.valor,A.subsi,A.motivosubsi,A.citt,A.fechainisubsi,A.fechafinsubsi,";
            query += " A.diascitt,A.nosubsi,A.motivonosubsi,A.fechaininosubsi,A.fechafinnosubsi, ";
            query += " A.idconplandes,A.tipocalculodes,A.valordes from predesplan A ";
            query += " inner join desplan B on A.idcia=B.idcia and A.idperplan=B.idperplan ";
            query += " and A.anio=B.anio and A.messem=B.messem and A.idtipoper=B.idtipoper ";
            query += " and A.idtipocal=B.idtipocal and A.idtipoplan=B.idtipoplan ";
            query += " where A.idcia='" + @idcia + "' and A.idtipocal='" + @idtipocal + "' ";
            query += " and A.idtipoper='" + @idtipoper + "' and A.idtipoplan='" + @idtipoplan + "' ";
            query += " and A.messem='" + @messem + "' and A.anio='" + @anio + "' ";
            query += " group by A.idperplan,A.diasefelab,A.diassubsi,A.diasnosubsi,A.diastotal,A.diasdom,";
            query += " A.idconplan,A.tipocalculo,A.valor,A.subsi,A.motivosubsi,A.citt,";
            query += " A.fechainisubsi,A.fechafinsubsi,A.diascitt,A.nosubsi,";
            query += " A.motivonosubsi,A.fechaininosubsi,A.fechafinnosubsi";

            SqlDataAdapter da_dtpredesplan = new SqlDataAdapter();
            da_dtpredesplan.SelectCommand = new SqlCommand(query, conexion);
            da_dtpredesplan.Fill(dtpredesplan);

            foreach (DataRow row_dtpredesplan in dtpredesplan.Rows)
            {
                idperplan = row_dtpredesplan["idperplan"].ToString();
                diasefelab = int.Parse(row_dtpredesplan["diasefelab"].ToString());
                diasdom = int.Parse(row_dtpredesplan["diasdom"].ToString());
                diassubsi = int.Parse(row_dtpredesplan["diassubsi"].ToString());
                diasnosubsi = int.Parse(row_dtpredesplan["diasnosubsi"].ToString());
                diastotal = int.Parse(row_dtpredesplan["diastotal"].ToString());
                cantidad = float.Parse(row_dtpredesplan["cantidad"].ToString());
                total = float.Parse(row_dtpredesplan["total"].ToString());
                diurno = int.Parse(row_dtpredesplan["diasefelab"].ToString());

                DataPlan dataplan = new DataPlan();
                dataplan.actualizarDataPlan("AGREGAR", idperplan, idcia, anio, messem, idtipoper, idtipocal, diasefelab, diassubsi,
                    diasnosubsi, diastotal, emplea, estane, riesgo, idtipoplan, hext25, hext35, hext100, finivac,
                    ffinvac, diasvac, total, cantidad, diurno, nocturno, diasdom,regvac,pervac);

                idconplan = row_dtpredesplan["idconplan"].ToString();
                tipocalculo = row_dtpredesplan["tipocalculo"].ToString();
                valor = float.Parse(row_dtpredesplan["valor"].ToString());

                if (idconplan != string.Empty && valor > 0)
                {
                    ConDataPlan condataplan = new ConDataPlan();
                    condataplan.actualizarConDataPlan("AGREGAR", idperplan, idcia, anio,
                        messem, idtipoper, idtipocal, idtipoplan, idconplan, tipocalculo, valor,idpresper,comen);
                }


                idconplandes = row_dtpredesplan["idconplandes"].ToString();
                tipocalculodes = row_dtpredesplan["tipocalculodes"].ToString();
                valordes = float.Parse(row_dtpredesplan["valordes"].ToString());

                if (idconplandes != string.Empty && valor > 0)
                {
                    ConDataPlan condataplan = new ConDataPlan();
                    condataplan.actualizarConDataPlan("AGREGAR", idperplan, idcia, anio,
                        messem, idtipoper, idtipocal, idtipoplan, idconplandes, tipocalculodes, valordes, idpresper, comen);
                }

                subsi = row_dtpredesplan["subsi"].ToString();
                if (subsi.Equals("1"))
                {
                    motivosubsi = row_dtpredesplan["motivosubsi"].ToString();
                    citt = row_dtpredesplan["citt"].ToString();
                    fechainisubsi = row_dtpredesplan["fechainisubsi"].ToString();
                    fechafinsubsi = row_dtpredesplan["fechafinsubsi"].ToString();
                    diascitt = int.Parse(row_dtpredesplan["diascitt"].ToString());
                    DiasSubsi diassubsidiados = new DiasSubsi();
                    tipo = "S";
                    diassubsidiados.setDiasSubsi(idperplan, idcia, anio, messem, idtipoper, idtipocal, tipo, motivosubsi, citt,
                        fechainisubsi, fechafinsubsi, diassubsi, diascitt, idtipoplan, "");
                    diassubsidiados.actualizarDiasSubsi("AGREGAR");
                }

                nosubsi = row_dtpredesplan["nosubsi"].ToString();
                if (nosubsi.Equals("1"))
                {
                    motivonosubsi = row_dtpredesplan["motivonosubsi"].ToString();
                    fechaininosubsi = row_dtpredesplan["fechaininosubsi"].ToString();
                    fechafinnosubsi = row_dtpredesplan["fechafinnosubsi"].ToString();
                    DiasSubsi diassubsidiados = new DiasSubsi();
                    tipo = "N";
                    diassubsidiados.setDiasSubsi(idperplan, idcia, anio, messem, idtipoper, idtipocal, tipo, motivonosubsi, "",
                        fechaininosubsi, fechafinnosubsi, diasnosubsi, 0, idtipoplan, "");
                    diassubsidiados.actualizarDiasSubsi("AGREGAR");
                }
            }

            query =         " select idperplan,idproddes,idzontra,cantidad,precio,total,glosa ";
            query = query + " from desplan  ";
            query = query + " where idcia='" + @idcia + "' and idtipocal='" + @idtipocal + "' ";
            query = query + " and idtipoper='" + @idtipoper + "' and idtipoplan='" + @idtipoplan + "' ";
            query = query + " and messem='" + @messem + "' and anio='" + @anio + "' ";

            SqlDataAdapter da_dtdesplan = new SqlDataAdapter();
            da_dtdesplan.SelectCommand = new SqlCommand(query, conexion);
            da_dtdesplan.Fill(dtdesplan);
            
            foreach (DataRow row_dtdesplan in dtdesplan.Rows)
            {
                idperplan = row_dtdesplan["idperplan"].ToString();
                idproddes = row_dtdesplan["idproddes"].ToString();
                idzontra=row_dtdesplan["idzontra"].ToString();
                cantidad=float.Parse(row_dtdesplan["cantidad"].ToString());
                precio=float.Parse(row_dtdesplan["precio"].ToString());
                total=float.Parse(row_dtdesplan["total"].ToString());
                glosa=row_dtdesplan["glosa"].ToString();

                Tareo tareo = new Tareo();
                tareo.actualizarTareo("AGREGAR", idcia, idperplan, messem, anio, idtipocal,
                    idtipoper, idproddes, idzontra, cantidad, precio, total, glosa, idtipoplan);
            }

            conexion.Close();
        }

        public void actualizarPrecioDestajo(string tipopregistro,string idcia, string messem,
        string anio, string idtipocal, string idtipoper,
        string idproddes, string idzontra, float precio, string idtipoplan, string emplea, string estane,string codvar)
        {
            string squery;
            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();
            string nombreDes = string.Empty;
            
            if (tipopregistro.Equals("P"))
            {
                nombreDes = "desplan";
            }
            else
            {
                nombreDes = "desret";
            }

            squery = "UPDATE " + @nombreDes + " SET precio='" + @precio + "', ";
            squery = squery + " total=cantidad*precio where idcia='" + @idcia + "' ";
            squery = squery + " and messem='" + @messem + "' and anio='" + @anio + "' and idtipocal='" + @idtipocal + "' ";
            squery = squery + " and idtipoper='" + @idtipoper + "' and idproddes='" + @idproddes + "' ";
            squery = squery + " and idzontra='" + @idzontra + "' and emplea='"+@emplea+"' and estane='"+@estane+"' and idtipoplan='"+@idtipoplan+"' and codvar='"+@codvar+"'; ";

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

        public void updDataReten(string operacion, string idcia, string idperplan, string messem,
        string anio, string idtipocal, string idtipoper,
        string fecha, string idproddes, string idzontra, float cantidad, float precio, float subtotal,
            float adicional, float reten, float total, string idtipoplan, string emplea, string estane, 
            string iddestajo,string idlabret,string idcencos)

        {
            string squery;
            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();

            if (operacion.Equals("AGREGAR"))
            {
                squery = " INSERT INTO desret(idcia,idperplan,messem,anio,idtipocal,idtipoper,fecha ";
                squery += ",idproddes,idzontra,cantidad,precio,subtotal,adicional,reten,total,";
                squery += "idtipoplan,emplea,estane,idlabret,idcencos) VALUES (";
                squery += "'" + @idcia + "', '" + @idperplan + "', '" + @messem + "','" + @anio + "',";
                squery += "'" + @idtipocal + "','" + @idtipoper + "',STR_TO_DATE('" + @fecha + "', '%d/%m/%Y'),";
                squery += "'" + @idproddes + "','" + @idzontra + "','" + @cantidad + "','" + @precio + "',";
                squery += "'" + @subtotal + "','" + @adicional + "','" + @reten + "',";
                squery += "'" + @total + "','" + @idtipoplan + "','" + @emplea + "','" + @estane + "','" + @idlabret + "','" + @idcencos + "');";
            }
            else
            {
                squery = " UPDATE desret SET cantidad='" + @cantidad + "',precio='" + @precio + "',subtotal='" + @subtotal + "',adicional='" + @adicional + "',reten='" + @reten + "',";
                squery += "total='" + @total + "',emplea='" + @emplea + "',estane='" + @estane + "',idlabret='" + @idlabret + "',idcencos='" + @idcencos + "' where idcia='" + @idcia + "' and idperplan='" + @idperplan + "'";
                squery += "and messem='" + @messem + "' and anio='" + @anio + "' and idtipocal='" + @idtipocal + "'";
                squery += "and idtipoper='" + @idtipoper + "' and fecha=STR_TO_DATE('" + @fecha + "', '%d/%m/%Y') and idproddes='" + @idproddes + "'";
                squery += "and idzontra='" + @idzontra + "'  and iddestajo='" + @iddestajo + "';";
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

        public void delDataReten(string idcia, string idperplan, string messem,
        string anio, string idtipocal, string idtipoper, string idproddes, string idzontra, string iddestajo)
        {
            string squery;
            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();
            squery = "DELETE from desret where idcia='" + @idcia + "' and idperplan='" + @idperplan;
            squery = squery + "' and messem='" + @messem + "' and anio='" + @anio + "' and idtipocal='" + @idtipocal + "'";
            squery = squery + " and idtipoper='" + @idtipoper + "' and idproddes='" + @idproddes + "'";
            squery = squery + " and idzontra='" + @idzontra + "' and iddestajo='" + @iddestajo + "';";
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
    }
}