using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Configuration;
using System.Data.SqlClient;

namespace CaniaBrava
{
    public partial class ui_recaneto : Form
    {
        public ui_recaneto()
        {
            InitializeComponent();
        }

        private void ui_recaneto_Load(object sender, EventArgs e)
        {
            Funciones funciones = new Funciones();
            GlobalVariables variables = new GlobalVariables();
            string idcia = variables.getValorCia();
            string squery = "SELECT idtipoper as clave,destipoper as descripcion FROM tipoper order by 1 asc";
            funciones.listaComboBox(squery, cmbTipoTrabajador, "");
            squery = "SELECT idtipoplan as clave,destipoplan as descripcion FROM tipoplan where idtipoplan in (select idtipoplan from reglabcia where idcia='" + @idcia + "') order by 1 asc;";
            funciones.listaComboBox(squery, cmbTipoPlan, "");

        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            try
            {
                Funciones funciones = new Funciones();
                GlobalVariables globalVariables = new GlobalVariables();
                CalPlan calplan = new CalPlan();
                string idcia = globalVariables.getValorCia();
                string idtipoper = funciones.getValorComboBox(cmbTipoTrabajador, 1);
                string idtipoplan = funciones.getValorComboBox(cmbTipoPlan, 3);
                string anio = txtAnio.Text;
                string idtipocal = funciones.getValorComboBox(cmbTipoCal, 1);
                string idconplan = txtCodigo.Text;

                string squery;

                SqlConnection conexion = new SqlConnection();
                conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
                conexion.Open();

                squery = "DELETE from conbol where idcia='" + @idcia + "' and anio='" + @anio + "' and idtipocal='" + @idtipocal + "'";
                squery = squery + " and idtipoper='" + @idtipoper + "' and idtipoplan='" + @idtipoplan + "' and idconplan='" + @idconplan + "' ;";
                squery = squery + " insert into conbol ";
                squery = squery + " select A.idperplan,A.idcia,A.anio,A.messem,A.idtipoper, ";
                squery = squery + " A.idtipocal,A.idtipoplan,'" + @idconplan + "',A.valor-(CASE ISNULL(B.valor) WHEN 1 THEN 0 WHEN 0 THEN B.valor END) as neto  ";
                squery = squery + " from view_toting A left join view_totdesc B ";
                squery = squery + " on A.idperplan=B.idperplan and A.idcia=B.idcia and A.anio=B.anio  ";
                squery = squery + " and A.messem=B.messem and A.idtipoper=B.idtipoper  ";
                squery = squery + " and A.idtipocal=B.idtipocal and A.idtipoplan=B.idtipoplan ";
                squery = squery + " where A.idcia='" + @idcia + "' and A.anio='" + @anio + "' and A.idtipocal='" + @idtipocal + "'";
                squery = squery + " and A.idtipoper='" + @idtipoper + "' and A.idtipoplan='" + @idtipoplan + "' ;";

                try
                {
                    SqlCommand myCommand = new SqlCommand(squery, conexion);
                    myCommand.ExecuteNonQuery();
                    myCommand.Dispose();
                    MessageBox.Show("Proceso concluído", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                catch (SqlException ex)
                {
                    MessageBox.Show(ex.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);

                }

                conexion.Close();


            }
            catch (FormatException ex)
            {
                MessageBox.Show(ex.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ui_listaTipoCal(string idcia, string idtipoplan)
        {
            Funciones funciones = new Funciones();
            string query;
            query = " SELECT idtipocal as clave,destipocal as descripcion ";
            query = query + " FROM tipocal where idtipocal in (select idtipocal ";
            query = query + " from calcia where idcia='" + @idcia + "' and idtipoplan='" + @idtipoplan + "') ";
            query = query + " order by ordentipocal asc;";
            funciones.listaComboBox(query, cmbTipoCal, "");
        }

        private void cmbTipoPlan_SelectedIndexChanged(object sender, EventArgs e)
        {
            Funciones funciones = new Funciones();
            GlobalVariables variables = new GlobalVariables();
            string idcia = variables.getValorCia();
            string idtipoplan = funciones.getValorComboBox(cmbTipoPlan, 3);
            ui_listaTipoCal(idcia, idtipoplan);

        }
    }
}