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
    public partial class ui_partediariodestajo : Form
    {
        string _idtipocal;
        string _idtipoper;
        string _idcia;

        public ui_partediariodestajo()
        {
            InitializeComponent();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ui_partediariodestajo_Load(object sender, EventArgs e)
        {
            Funciones funciones = new Funciones();
            GlobalVariables variables = new GlobalVariables();
            string squery;
            this._idtipocal = "D";
            this._idtipoper = "O";
            string idcia = variables.getValorCia();
            this._idcia = idcia;
            squery = "SELECT ruccia as clave,descia as descripcion FROM ciafile ";
            squery = squery + " WHERE idcia='" + @idcia + "';";
            funciones.listaComboBox(squery, cmbEmpleador, "");
            squery = "SELECT idproddes as clave,desproddes as descripcion FROM ";
            squery = squery + " proddes where idcia='" + @idcia + "' order by 1 asc";
            funciones.listaComboBox(squery, cmbProducto, "");
            cmbTipoRegistro.Text = "R   PERSONAL DE RETENCIONES";
        }

        private void cmbTipoRegistro_SelectedIndexChanged(object sender, EventArgs e)
        {
            Funciones fn = new Funciones();

            if (fn.getValorComboBox(cmbTipoRegistro, 1).Equals("P"))
            {
                string idcia = this._idcia;
                string query = "SELECT idtipoplan as clave,destipoplan as descripcion FROM tipoplan where idtipoplan in (select idtipoplan from reglabcia where idcia='" + @idcia + "') order by 1 asc;";
                fn.listaComboBox(query, cmbTipoPlan, "");
                lblTipoPlanilla.Visible = true;
                cmbTipoPlan.Visible = true;
                cmbTipoPlan.Focus();
            }
            else
            {
                lblTipoPlanilla.Visible = false;
                cmbTipoPlan.Visible = false;
            }


        }

        private void radioButtonNoEmp_CheckedChanged(object sender, EventArgs e)
        {
            Funciones funciones = new Funciones();
            GlobalVariables variables = new GlobalVariables();
            string idcia = variables.getValorCia();
            string ruccia = variables.getValorRucCia();

            string squery = "SELECT ruccia as clave,descia as descripcion FROM ciafile WHERE idcia='" + @idcia + "';";
            funciones.listaComboBox(squery, cmbEmpleador, "");
            cmbEmpleador.Enabled = false;

            string squery_esta = "SELECT idestane as clave,desestane as descripcion FROM estane WHERE codemp='" + @ruccia + "' and idciafile='" + @idcia + "' order by idestane asc;";
            funciones.listaComboBox(squery_esta, cmbEstablecimiento, "");
            cmbEstablecimiento.Focus();
        }

        private void radioButtonSiEmp_CheckedChanged(object sender, EventArgs e)
        {
            Funciones funciones = new Funciones();
            GlobalVariables variables = new GlobalVariables();
            string idcia = variables.getValorCia();

            string squery = "SELECT rucemp as clave,razonemp as descripcion FROM emplea WHERE idciafile='" + @idcia + "' order by rucemp asc;";
            funciones.listaComboBox(squery, cmbEmpleador, "");
            cmbEmpleador.Enabled = true;

            string ruccia = funciones.getValorComboBox(cmbEmpleador, 11);
            string squery_esta = "SELECT idestane as clave,desestane as descripcion FROM estane WHERE codemp='" + @ruccia + "' and idciafile='" + @idcia + "' order by idestane asc;";
            funciones.listaComboBox(squery_esta, cmbEstablecimiento, "");
            cmbEmpleador.Focus();
        }

        private void cmbEmpleador_SelectedIndexChanged(object sender, EventArgs e)
        {
            Funciones funciones = new Funciones();
            string idcia = this._idcia;
            string ruccia = funciones.getValorComboBox(cmbEmpleador, 11);
            string squery_esta = "SELECT idestane as clave,desestane as descripcion FROM estane WHERE codemp='" + @ruccia + "' and idciafile='" + @idcia + "' order by idestane asc;";
            funciones.listaComboBox(squery_esta, cmbEstablecimiento, "");
            cmbEstablecimiento.Focus();
        }

        private void txtFechaProceso_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                if (UtileriasFechas.IsDate(txtFechaProceso.Text))
                {

                    e.Handled = true;
                    toolstripform.Items[0].Select();
                    toolstripform.Focus();

                }
                else
                {
                    MessageBox.Show("Fecha de proceso no válida", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    e.Handled = true;
                    txtFechaProceso.Clear();
                    txtFechaProceso.Focus();

                }
            }
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            Funciones funciones = new Funciones();
            string idcia = this._idcia;
            string idproddes = funciones.getValorComboBox(cmbProducto, 2);
            string fecha = txtFechaProceso.Text;
            string idtipocal = this._idtipocal;
            string idtipoper = this._idtipoper;
            string tiporegistro = funciones.getValorComboBox(cmbTipoRegistro, 1);
            string emplea = funciones.getValorComboBox(cmbEmpleador, 11);
            string estane = funciones.getValorComboBox(cmbEstablecimiento, 4);
            string idtipoplan;
            if (tiporegistro.Equals("P"))
            {
                idtipoplan = funciones.getValorComboBox(cmbTipoPlan, 3);
            }
            else
            {
                idtipoplan = "";
            }

            string nombreTablaRet = string.Empty;
            string nombreTablaPer = string.Empty;

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

            string query = " select F.descia,F.ruccia,A.fecha,B.idperplan,D.Parm1maesgen as cortotipodoc,B.nrodoc,";
            query = query + " CONCAT(B.apepat,' ',B.apemat,' , ',B.nombres) as nombre,E.deszontra,A.cantidad,A.precio,A.subtotal,";
            query = query + " A.movilidad,A.refrigerio,A.adicional,A.total ";
            query = query + " from " + @nombreTablaRet + " A  ";
            query = query + " left join " + @nombreTablaPer + " B on A.idperplan=B.idperplan and A.idcia=B.idcia ";
            query = query + " left join maesgen D on D.idmaesgen='002' and B.tipdoc=D.clavemaesgen ";
            query = query + " left join zontra E on A.idzontra=E.idzontra and A.idcia=E.idcia ";
            query = query + " left join ciafile F on A.idcia=F.idcia ";
            query = query + " where A.idcia='" + @idcia + "' and idtipocal='" + @idtipocal + "' and A.idtipoper='" + @idtipoper + "' ";
            query = query + " and idproddes='" + @idproddes + "' and A.idtipoplan='" + @idtipoplan + "' and A.emplea='" + @emplea + "' and A.estane='" + @estane + "'";
            query = query + " and fecha=" + " STR_TO_DATE('" + @fecha + "', '%d/%m/%Y') ";
            query = query + " order by F.descia,E.idzontra,B.apepat asc,B.apemat asc ,B.nombres asc;";

            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();
            try
            {
                using (SqlDataAdapter myDataAdapter = new SqlDataAdapter(query, conexion))
                {
                    DataSet dspardiades = new DataSet();
                    cr.crpardiades cr = new cr.crpardiades();
                    myDataAdapter.Fill(dspardiades, "dtpardiades");
                    ui_reporte ui_reporte = new ui_reporte();
                    ui_reporte.asignaDataSet(cr, dspardiades);
                    ui_reporte.Activate();
                    ui_reporte.BringToFront();
                    ui_reporte.ShowDialog();
                    ui_reporte.Dispose();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            conexion.Close();
        }
    }
}