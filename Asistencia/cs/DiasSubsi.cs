using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace CaniaBrava
{
    class DiasSubsi
    {
        string idperplan;
        string idcia;
        string anio;
        string messem;
        string idtipoper;
        string idtipocal;
        string idtipoplan;
        string tipo;
        string motivo;
        string citt;
        string fechaini;
        string fechafin;
        int dias;
        int diascitt;
        string iddiassubsi;

        public DiasSubsi() { }

        public void setDiasSubsi(string idperplan, string idcia, string anio, string messem, string idtipoper, string idtipocal, string tipo,string motivo,string citt, string fechaini,string fechafin,int dias, int diascitt,string idtipoplan,string iddiassubsi)
        {
            
            this.idperplan=idperplan;
            this.idcia=idcia;
            this.anio=anio;
            this.messem=messem;
            this.idtipoper=idtipoper;
            this.idtipocal=idtipocal;
            this.tipo = tipo;
            this.motivo = motivo;
            this.fechaini = fechaini;
            this.fechafin = fechafin;
            this.dias = dias;
            this.diascitt = diascitt;
            this.citt = citt;
            this.idtipoplan = idtipoplan;
            this.iddiassubsi = iddiassubsi;
            
        }

        public void actualizarDiasSubsi(string soperacion)
        {
            string squery;
            string idperplan=this.idperplan;
            string idcia=this.idcia;
            string anio=this.anio;
            string messem=this.messem;
            string idtipoper=this.idtipoper;
            string idtipocal=this.idtipocal;
            string tipo = this.tipo;
            string motivo = this.motivo;
            string citt = this.citt;
            string fechaini = this.fechaini;
            string fechafin = this.fechafin;
            int dias = this.dias;
            int diascitt = this.diascitt;


            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();

            if (soperacion.Equals("AGREGAR"))
            {
                squery = "INSERT INTO diassubsi(idperplan,idcia,anio,messem,idtipoper,idtipocal,tipo,motivo,fechaini,fechafin,dias,diascitt,citt,idtipoplan) VALUES ('" + @idperplan + "', '" + @idcia + "', '" + @anio + "', '" + @messem + "','" + @idtipoper + "','" + @idtipocal + "','" + @tipo + "','" + @motivo + "'," + "STR_TO_DATE('" + @fechaini + "', '%d/%m/%Y')" + "," + "STR_TO_DATE('" + @fechafin + "', '%d/%m/%Y')" + ",'" + @dias + "','" + @diascitt + "','"+@citt+"','"+@idtipoplan+"');";
            }
            else
            {
                squery = "UPDATE diassubsi SET fechaini=STR_TO_DATE('" + @fechaini + "', '%d/%m/%Y'),fechafin=STR_TO_DATE('" + @fechafin + "', '%d/%m/%Y'),dias='" + @dias + "',diascitt='" + @diascitt + "',citt='"+@citt+"' WHERE idcia='" + @idcia + "' and idperplan='" + @idperplan + "' and anio='" + @anio + "' and messem='" + @messem + "' and idtipoper='" + @idtipoper + "' and idtipocal='" + @idtipocal + "' and tipo='" + @tipo + "' and motivo='" + @motivo + "' and idtipoplan='"+@idtipoplan+"' and iddiassubsi='"+@iddiassubsi+"';";
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

        public void eliminarDiasSubsi(string idperplan, string idcia, string anio, string messem, string idtipoper, string idtipocal,string tipo,string motivo,string idtipoplan,string iddiassubsi)
        {
            string squery;
            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();

            squery = "DELETE from diassubsi WHERE idcia='" + @idcia + "' and idperplan='" + @idperplan + "' and anio='" + @anio + "' and messem='" + @messem + "' and idtipoper='" + @idtipoper + "' and idtipocal='" + @idtipocal + "' and tipo='" + @tipo + "' and motivo='" + @motivo + "' and idtipoplan='"+@idtipoplan+"' and iddiassubsi='"+@iddiassubsi+"';";

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