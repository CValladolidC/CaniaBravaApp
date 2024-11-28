using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.ComponentModel;
using System.Drawing;

namespace CaniaBrava
{
    class Descansos : Form
    {
        public string ui_getDatosMedico(string tipo, string id, string datossolicitado)
        {
            string query;
            string resultado = string.Empty;

            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();

            query = " SELECT * FROM medicos (NOLOCK) WHERE tipo = '" + @tipo + "' AND id = '" + @id + "';";

            try
            {
                SqlCommand myCommand = new SqlCommand(query, conexion);
                SqlDataReader myReader = myCommand.ExecuteReader();

                if (myReader.Read())
                {
                    if (datossolicitado.Equals("0"))
                    {
                        resultado = myReader["tipo"].ToString();
                    }
                    if (datossolicitado.Equals("1"))
                    {
                        resultado = myReader["id"].ToString();
                    }
                    if (datossolicitado.Equals("2"))
                    {
                        resultado = myReader["nombres"].ToString() + " " + myReader["apellidos"].ToString();
                    }
                    if (datossolicitado.Equals("3"))
                    {
                        resultado = myReader["especialidad"].ToString();
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

        public string ui_getDatosCenSalud(string datossolicitado)
        {
            string query;
            string resultado = string.Empty;

            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();

            query = " SELECT * FROM censalud (NOLOCK);";

            try
            {
                SqlCommand myCommand = new SqlCommand(query, conexion);
                SqlDataReader myReader = myCommand.ExecuteReader();

                if (myReader.Read())
                {
                    if (datossolicitado.Equals("0"))
                    {
                        resultado = myReader["ruc"].ToString();
                    }
                    if (datossolicitado.Equals("1"))
                    {
                        resultado = myReader["descensalud"].ToString();
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

        public void EliminarDescanso(string id)
        {
            string query;

            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();

            query = "DELETE FROM regdescanso WHERE idregvac = '" + @id + "' ;";

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
        public void cerrarMensaje() //Mensaje tipo TOAST , dura 1 segundo
        {
            System.Threading.Thread.Sleep(1070);
            Microsoft.VisualBasic.Interaction.AppActivate(
                 System.Diagnostics.Process.GetCurrentProcess().Id);
            System.Windows.Forms.SendKeys.SendWait(" ");
        }
      


        public void actualizarRegDescanso(string soperacion, string idperplan, string idcia, string anio, string finivac,
            string ffinvac, int diasvac, string idregvac, string certificado, string medico, string especialidad,
            string cmp, string estacionsalud, string diagnostico, string tipo, string conting, string celu, string usuario, 
            string statusCit, string mesconti, string fecemision, int alerta, string concepsap)
        {

            string squery;
            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();

            string nombreDes = string.Empty;

            if (soperacion.Equals("AGREGAR"))
            {
                squery = @" 
                   IF EXISTS (
                    SELECT 1
                    FROM regdescanso (NOLOCK) 
                    WHERE idperplan = " + @idperplan + "  AND (finivac <=  '" + @ffinvac + "' AND ffinvac >= '" + @finivac + "') ) " +
                    "BEGIN SELECT '1' END " + 
                    "ELSE BEGIN SELECT '2' END;";
            }

            else
            {

                //squery = @" Select CASE WHEN (SELECT TOP 1 idregvac FROM regdescanso WITH (NOLOCK)  where idregvac = '" + @idregvac + "') = " +
                //    "(SELECT TOP 1 idregvac FROM regdescanso WHERE idperplan = " + @idperplan + " ORDER BY idregvac DESC) THEN '4'" +
                //    "WHEN EXISTS ( SELECT 1 FROM regdescanso WITH (NOLOCK) " +
                //    "WHERE idperplan = " + @idperplan + " AND finivac <= '" + @ffinvac + "' AND ffinvac >= '" + @finivac + "') THEN '3' ELSE '4' END AS resultado ";
                squery = @"IF EXISTS ( SELECT 1 FROM regdescanso (NOLOCK) WHERE idperplan = '" + @idperplan + "' " +
                    "AND idregvac != "+ @idregvac + "   AND (finivac <= '" + @ffinvac + "' AND ffinvac >= '" + @finivac + "') ) BEGIN " +
                    "SELECT '3'" +
                    "END ELSE BEGIN   SELECT '4'; END; ";

            }

            try
            {
                SqlCommand myCommand = new SqlCommand(squery, conexion);
                //myCommand.ExecuteNonQuery();
                //myCommand.Dispose();

                //Validación cuando se ingresar 
                object resultado = myCommand.ExecuteScalar();

                try
                {
                    if (resultado.ToString() == "1")
                    {
                        string queryAdvertencia1 = "";
                        string queryAdvertencia2 = "";

                        //Mostrar mensaje de advertencia mostrando fechas de colisión actuales
                        queryAdvertencia1 = @" SELECT top 1 'Error al ingresar: El usuario ya presenta un descanso médico del ' + " +
                        "CONVERT(VARCHAR, DAY(finivac)) + '/' + CONVERT(VARCHAR, MONTH(finivac)) + '/' + CONVERT(VARCHAR, YEAR(finivac)) + ' al ' + " +
                        "CONVERT(VARCHAR, DAY(ffinvac)) + '/' + CONVERT(VARCHAR, MONTH(ffinvac)) + '/' + CONVERT(VARCHAR, YEAR(ffinvac)) AS Mensaje " +
                        "FROM regdescanso (NOLOCK)  WHERE idperplan = " + @idperplan + "  AND (finivac <= '" + @ffinvac + "' AND ffinvac >= '" + @finivac + "') order by ffinvac desc ";

                        //Mostrar mensaje de sugerencia con el proximo dia 
                        queryAdvertencia2 = @" select top 1 'Proximo dia de registro es : ' + CONVERT(VARCHAR,DAY(CONVERT(datetime, ffinvac,101)+1))+ '/' +CONVERT(VARCHAR,MONTH(CONVERT(datetime, ffinvac,101)+1)) +'/'+ CONVERT(VARCHAR,YEAR(CONVERT(datetime, ffinvac,101)+1)) + '.' as Mensaje2 from regdescanso (NOLOCK) 
                        WHERE idperplan = " + @idperplan + "  AND (finivac <= '" + @ffinvac + "' AND ffinvac >= '" + @finivac + "') order by ffinvac desc ";

                        SqlCommand myCommandAdvertencia1 = new SqlCommand(queryAdvertencia1, conexion);
                        SqlCommand myCommandAdvertencia2 = new SqlCommand(queryAdvertencia2, conexion);
                        object advertencia1 = myCommandAdvertencia1.ExecuteScalar();
                        object advertencia2 = myCommandAdvertencia2.ExecuteScalar();

                        MessageBox.Show(advertencia1.ToString() + "\r\n" + "\r\n" + advertencia2.ToString(), "Advertencia de registro");

                        //Evitar cierre de formulario al haber cruces de inserción 

                    }
                    else if (resultado.ToString() == "2")
                    { //Si el dia no colapsa con un registro actual, se inserta con nueva query
                        try
                        {
                            string queryInserccion = "";
                            queryInserccion = @"INSERT INTO regdescanso(idperplan,idcia,anio,finivac,ffinvac,diasvac,idregvac,certificado,medico,especialidad,tipocolegiatura,colegiatura,estacionsalud," +
                          "diagnostico,contingencia,celular,idusr,idusrfecha,statusCIT,mesconting,fechaemision,alerta,consap) VALUES (";
                            queryInserccion += "'" + @idperplan + "', '" + @idcia + "', '" + @anio + "',";
                            queryInserccion += "'" + @finivac + "','" + @ffinvac + "','" + @diasvac + "','" + @idregvac + "',";
                            queryInserccion += "'" + @certificado + "','" + @medico + "','" + @especialidad + "','" + @tipo + "',";
                            queryInserccion += "'" + @cmp + "','" + @estacionsalud + "','" + @diagnostico + "','" + @conting + "','" + @celu + "',";
                            queryInserccion += "'" + @usuario + "',GETDATE(),'" + @statusCit + "','" + @mesconti + "','" + @fecemision + "'," + alerta + ",'" + @concepsap + "');";
                            SqlCommand myCommandInserccion = new SqlCommand(queryInserccion, conexion);
                            myCommandInserccion.ExecuteNonQuery();
                            myCommandInserccion.Dispose();
                            //MessageBox.Show("Se registro correctamente el descanso médico.");

                            (new System.Threading.Thread(cerrarMensaje)).Start();
                            MessageBox.Show("Se registro correctamente el descanso médico");
                        }
                        catch (Exception e)
                        {
                            MessageBox.Show("El error al guardar el dato, esta en " + e) ;
                            throw;
                        }
                     
                    }

                    else if (resultado.ToString() == "3")
                    {
                        string queryAdvertencia1 = "";
                        string queryAdvertencia2 = "";
                        try
                        {
                            //Mostrar mensaje de advertencia mostrando fechas de colisión actuales
                            queryAdvertencia1 = @"SELECT top 1  'Error: El descanso ingresado colisiona con el descanso del ' + CONVERT(VARCHAR, DAY(finivac)) + '/' + CONVERT(VARCHAR, MONTH(finivac)) + '/' + CONVERT(VARCHAR, YEAR(finivac)) + ' al ' + CONVERT(VARCHAR, DAY(ffinvac)) + '/' + CONVERT(VARCHAR, MONTH(ffinvac)) + '/' + CONVERT(VARCHAR, YEAR(ffinvac)) +  '.' AS Mensaje FROM regdescanso (NOLOCK) " +
                        "WHERE idperplan = '" + @idperplan + "' AND idregvac != " + @idregvac + " AND (finivac <= '" + @ffinvac + "' AND ffinvac >= '" + @finivac + "')  ORDER BY ffinvac DESC; ";
                            //Mostrar mensaje de sugerencia con el proximo dia 
                            queryAdvertencia2 = @"Select 'Se recomienda actualizar las fechas a partir de la fecha de inicio actual de este registro: ' + CONVERT(VARCHAR, DAY((SELECT finivac FROM regdescanso WHERE idregvac = " + @idregvac + "))) + '/' + CONVERT(VARCHAR, MONTH((SELECT finivac FROM regdescanso WHERE idregvac = " + @idregvac + "))) + '/' + CONVERT(VARCHAR, YEAR((SELECT finivac FROM regdescanso WHERE idregvac = " + @idregvac + "))) AS Mensaje FROM regdescanso (NOLOCK) " +
                         "WHERE idperplan = '" + @idperplan + "' AND idregvac != " + @idregvac + " AND (finivac <= '" + @ffinvac + "' AND ffinvac >= '" + @finivac + "')  ORDER BY ffinvac DESC; ";

                            SqlCommand myCommandAdvertencia1 = new SqlCommand(queryAdvertencia1, conexion);
                            SqlCommand myCommandAdvertencia2 = new SqlCommand(queryAdvertencia2, conexion);
                            object advertencia1 = myCommandAdvertencia1.ExecuteScalar();
                            object advertencia2 = myCommandAdvertencia2.ExecuteScalar();

                            MessageBox.Show(advertencia1.ToString() + "\r\n" + "\r\n" + advertencia2.ToString(), "Advertencia de actualización");

                            //ui_updregdescanso.close();
                            
                            
                        }
                        catch (Exception e)
                        {
                            MessageBox.Show("El error ren la consua de actualizar es " + e);
                            throw;
                        }
                        
                    }

                    else if (resultado.ToString() == "4") {
                        string queryActualizar = "";
                        queryActualizar = "UPDATE regdescanso SET finivac='" + @finivac + "',ffinvac='" + @ffinvac + "',";
                        queryActualizar += "diasvac='" + @diasvac + "',anio='" + @anio + "',certificado='" + @certificado + "',contingencia='" + @conting + "',";
                        queryActualizar += "medico='" + @medico + "',especialidad='" + @especialidad + "',tipocolegiatura='" + @tipo + "',";
                        queryActualizar += "colegiatura='" + @cmp + "',estacionsalud='" + @estacionsalud + "',diagnostico='" + @diagnostico + "',celular='" + @celu + "',";
                        queryActualizar += "statusCIT='" + @statusCit + "',mesconting='" + @mesconti + "',fechaemision='" + @fecemision + "',alerta=" + alerta + ",consap='" + @concepsap + "' ";
                        queryActualizar += "WHERE idregvac='" + @idregvac + "';";

                        SqlCommand myCommandActualizar = new SqlCommand(queryActualizar, conexion);
                        myCommandActualizar.ExecuteNonQuery();
                        myCommandActualizar.Dispose();
                        //MessageBox.Show("Se registro correctamente el descanso médico.");

                        (new System.Threading.Thread(cerrarMensaje)).Start();
                        MessageBox.Show("Se actualizo correctamente el descanso médico");
                    }
                    else
                    {
                        MessageBox.Show("No se reconocio el comando solicitado");
                    }
                }
                catch (Exception)
                {

                    //MessageBox.Show("Manejo de errore al actualizar "); 
                }
                
                
            }
            catch (SqlException e)
            {
                MessageBox.Show(e.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            conexion.Close();
        }

        public string GeneraCodigoRegVac()
        {
            Funciones funciones = new Funciones();
            string codigoInterno = string.Empty;
            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();
            string query = " select isnull(max(idregvac),0) as existencia,isnull(max(idregvac),0)+1 as codigo from regdescanso ;";
            try
            {
                SqlCommand myCommand = new SqlCommand(query, conexion);
                SqlDataReader myReader = myCommand.ExecuteReader();
                if (myReader.Read())
                {
                    if (myReader.GetInt32(myReader.GetOrdinal("existencia")).ToString().Trim().Equals(0))
                    {
                        codigoInterno = "00001";
                    }
                    else
                    {
                        string codigo = myReader.GetInt32(myReader.GetOrdinal("codigo")).ToString();
                        codigoInterno = funciones.replicateCadena("0", 5 - codigo.Trim().Length) + codigo;
                    }
                }
                else
                {
                    codigoInterno = "0001";
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
    }
}
