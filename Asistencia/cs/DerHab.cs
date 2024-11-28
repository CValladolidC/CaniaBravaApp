using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace CaniaBrava
{
    class DerHab
    {
        public DerHab() { }

        public void actualizaDerHab(string soperacion, string idperplan, string idcia,string idderhab, string apepat, string apemat,
        string nombres, string fecnac, string tipdoc, string nrodoc, string sexo,string vinfam,string docpat,
        string sitdh,string fecalta,string motbaja,string fecbaja,string domtra,string otrodom,string tipvia, string nomvia,
        string nrovia, string intvia, string tipzona, string nomzona, string refzona, string ubigeo, string dscubigeo, string discapa,string rddisca)
        {
            string squery;

            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();

            if (soperacion.Equals("AGREGAR"))
            {
                if (motbaja.Trim()!=string.Empty)
                {
                    squery = "INSERT INTO derhab (idperplan,  idcia,  apepat,  apemat,";
                    squery = squery + "nombres,  fecnac,  tipdoc,  nrodoc,";
                    squery = squery + "vinfam,docpat,sitdh,fecalta,motbaja,fecbaja,domtra,otrodom,";
                    squery = squery + "tipvia,  nomvia,nrovia,  intvia,  tipzona,";
                    squery = squery + "nomzona,  refzona,  ubigeo,  dscubigeo,";
                    squery = squery + "sexo,discapa,rddisca) VALUES ('" + @idperplan + "','" + @idcia + "','" + @apepat + "','" + @apemat;
                    squery = squery + "','" + @nombres + "'," + " STR_TO_DATE('" + @fecnac + "', '%d/%m/%Y') " + ",'" + @tipdoc + "','" + @nrodoc;
                    squery = squery + "','" + @vinfam + "','" + @docpat + "','" + @sitdh + "'," + " STR_TO_DATE('" + @fecalta + "', '%d/%m/%Y') " + ",'" + @motbaja + "'," + " STR_TO_DATE('" + @fecbaja + "', '%d/%m/%Y'),'" + @domtra + "','" + @otrodom;
                    squery = squery + "','" + @tipvia + "','" + @nomvia + "','" + @nrovia + "','" + @intvia + "','" + @tipzona + "','" + @nomzona + "','" + @refzona + "','" + @ubigeo + "','" + @dscubigeo;
                    squery = squery + "','" + @sexo + "','"+@discapa+"','"+@rddisca+"');";
                }
                else
                {
                    squery = "INSERT INTO derhab (idperplan,  idcia,  apepat,  apemat,";
                    squery = squery + "nombres,  fecnac,  tipdoc,  nrodoc,";
                    squery = squery + "vinfam,docpat,sitdh,fecalta,domtra,otrodom,";
                    squery = squery + "tipvia,  nomvia,nrovia,  intvia,  tipzona,";
                    squery = squery + "nomzona,  refzona,  ubigeo,  dscubigeo,";
                    squery = squery + "sexo,discapa,rddisca) VALUES ('" + @idperplan + "','" + @idcia + "','" + @apepat + "','" + @apemat;
                    squery = squery + "','" + @nombres + "'," + " STR_TO_DATE('" + @fecnac + "', '%d/%m/%Y') " + ",'" + @tipdoc + "','" + @nrodoc;
                    squery = squery + "','" + @vinfam + "','" + @docpat + "','" + @sitdh + "'," + " STR_TO_DATE('" + @fecalta + "', '%d/%m/%Y') " + ",'" + @domtra + "','" + @otrodom;
                    squery = squery + "','" + @tipvia + "','" + @nomvia + "','" + @nrovia + "','" + @intvia + "','" + @tipzona + "','" + @nomzona + "','" + @refzona + "','" + @ubigeo + "','" + @dscubigeo;
                    squery = squery + "','" + @sexo + "','"+@discapa+"','"+@rddisca+"');";
                }
            }
            else
            {
                if (motbaja.Trim() != string.Empty)
                {
                    squery = "UPDATE derhab SET apepat='" + @apepat + "',apemat='" + @apemat + "',";
                    squery = squery + "nombres='" + @nombres + "',fecnac=STR_TO_DATE('" + @fecnac + "', '%d/%m/%Y')" + ",tipdoc='" + @tipdoc + "',nrodoc='" + @nrodoc + "',";
                    squery = squery + "vinfam='" + @vinfam + "',docpat='" + @docpat + "',sitdh='" + @sitdh + "',fecalta=STR_TO_DATE('" + @fecalta + "', '%d/%m/%Y'),motbaja='" + @motbaja + "',fecbaja=STR_TO_DATE('" + @fecbaja + "', '%d/%m/%Y')" + ",tipdoc='" + @tipdoc + "',nrodoc='" + @nrodoc + "',";
                    squery = squery + "tipvia='" + @tipvia + "',nomvia='" + @nomvia + "',domtra='" + @domtra + "',otrodom='" + @otrodom + "',";
                    squery = squery + "nrovia='" + @nrovia + "',intvia='" + @intvia + "',tipzona='" + @tipzona + "',nomzona='" + @nomzona + "',refzona='" + @refzona + "',ubigeo='" + @ubigeo + "',dscubigeo='" + @dscubigeo + "',";
                    squery = squery + "sexo='" + @sexo + "',discapa='"+@discapa+"',rddisca='"+@rddisca+"' WHERE idcia='" + @idcia + "' and idperplan='" + @idperplan + "' and idderhab='" + @idderhab + "';";
                }
                else
                {
                    squery = "UPDATE derhab SET apepat='" + @apepat + "',apemat='" + @apemat + "',";
                    squery = squery + "nombres='" + @nombres + "',fecnac=STR_TO_DATE('" + @fecnac + "', '%d/%m/%Y')" + ",tipdoc='" + @tipdoc + "',nrodoc='" + @nrodoc + "',";
                    squery = squery + "vinfam='" + @vinfam + "',docpat='" + @docpat + "',sitdh='" + @sitdh + "',fecalta=STR_TO_DATE('" + @fecalta + "', '%d/%m/%Y'),tipdoc='" + @tipdoc + "',nrodoc='" + @nrodoc + "',motbaja='" + @motbaja +"',fecbaja=NULL,";
                    squery = squery + "tipvia='" + @tipvia + "',nomvia='" + @nomvia + "',domtra='" + @domtra + "',otrodom='" + @otrodom + "',";
                    squery = squery + "nrovia='" + @nrovia + "',intvia='" + @intvia + "',tipzona='" + @tipzona + "',nomzona='" + @nomzona + "',refzona='" + @refzona + "',ubigeo='" + @ubigeo + "',dscubigeo='" + @dscubigeo + "',";
                    squery = squery + "sexo='" + @sexo + "',discapa='" + @discapa + "',rddisca='" + @rddisca + "' WHERE idcia='" + @idcia + "' and idperplan='" + @idperplan + "' and idderhab='" + @idderhab + "';";
                }

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

        public void eliminarDerHab(string idcia, string idperplan,string idderhab)
        {
            string squery;

            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();

            squery = "DELETE from derhab WHERE idcia='" + @idcia + "' and idperplan='" + @idperplan + "' and idderhab='"+@idderhab+"';";

            try
            {
                SqlCommand myCommand = new SqlCommand(squery, conexion);
                myCommand.ExecuteNonQuery();
                myCommand.Dispose();

            }
            catch (SqlException)
            {
                MessageBox.Show("A ocurrido un error en el proceso de Eliminación", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);

            }

            conexion.Close();

        }

        public string getNumeroDerHab(string idcia, string idperplan,string vinfam)
        {
            string numero = "0";
            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();
            string squery = "select count(*) as numero from derhab where idcia='" + @idcia + "' and idperplan='"+@idperplan+"' and vinfam='" + @vinfam + "' and sitdh='10';"; 
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
    }
}