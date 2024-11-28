using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Configuration;

namespace CaniaBrava
{
    public partial class ui_procesadetajoautomatico : Form
    {
        public ui_procesadetajoautomatico()
        {
            InitializeComponent();
        }

        private void txtMesSem_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                Funciones funciones = new Funciones();
                CalPlan calplan = new CalPlan();
                GlobalVariables variables = new GlobalVariables();
                string idcia = variables.getValorCia();
                string idtipoper = "O";
                string periodo = txtMesSem.Text.Trim() + funciones.replicateCadena(" ", 6);
                string messem = periodo.Substring(0, 2);
                string anio = periodo.Substring(3, 4);
                string idtipocal = "D";
                if (calplan.getDatosCalPlan(messem, anio, idtipoper, idcia, idtipocal, "FECHAINI") != "")
                {
                    txtFechaIni.Text = calplan.getDatosCalPlan(messem, anio, idtipoper, idcia, idtipocal, "FECHAINI");
                    txtFechaFin.Text = calplan.getDatosCalPlan(messem, anio, idtipoper, idcia, idtipocal, "FECHAFIN");

                    lblPeriodo.Visible = true;

                    if (calplan.getDatosCalPlan(messem, anio, idtipoper, idcia, idtipocal, "ESTADO") == "V")
                    {
                        lblPeriodo.Text = "Periodo Laboral Vigente";

                    }
                    else
                    {
                        lblPeriodo.Text = "Periodo Laboral Cerrado";

                    }
                }
                else
                {
                    MessageBox.Show("El Periodo Laboral no existe", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    lblPeriodo.Visible = false;
                    txtFechaIni.Clear();
                    txtFechaFin.Clear();
                    e.Handled = true;
                    txtMesSem.Focus();
                }

            }
        }

        private void ui_procesadetajoautomatico_Load(object sender, EventArgs e)
        {
            Funciones funciones = new Funciones();
            GlobalVariables variables = new GlobalVariables();
            string squery;
            string idcia = variables.getValorCia();
            squery = "SELECT ruccia as clave,descia as descripcion FROM ciafile WHERE idcia='" + @idcia + "';";
            funciones.listaComboBox(squery, cmbEmpleador, "");
            cmbTipoRegistro.Text = "R   PERSONAL DE RETENCIONES";
            squery = "SELECT idproddes as clave,desproddes as descripcion FROM proddes where idcia='" + @idcia + "' and stateproddes='V' order by 1 asc";
            funciones.listaComboBox(squery, cmbProducto, "");
            squery = "SELECT idzontra as clave,deszontra as descripcion FROM zontra WHERE idcia='" + @idcia + "' order by 1 asc;";
            funciones.listaComboBox(squery, cmbZona, "");
            radioButtonNoEmp.Checked = true;
            txtCantidad.Text = "0";
            txtPrecio.Text = "0";
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

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                try
                {
                    float cantidad = float.Parse(txtCantidad.Text);
                    e.Handled = true;
                    txtPrecio.Focus();

                }

                catch (FormatException ex)
                {
                    MessageBox.Show(ex.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    e.Handled = true;
                    txtCantidad.Focus();
                }

            }
        }

        private void txtPrecio_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                e.Handled = true;
                toolStripForm.Items[0].Select();
                toolStripForm.Focus();
            }
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {

            Funciones funciones = new Funciones();
            GlobalVariables variables = new GlobalVariables();
            CalPlan calplan = new CalPlan();

            try
            {
                string idcia = variables.getValorCia();
                string idproddes = funciones.getValorComboBox(cmbProducto, 2);
                string idzontra = funciones.getValorComboBox(cmbZona, 2);
                string periodo = txtMesSem.Text.Trim() + funciones.replicateCadena(" ", 6);
                string messem = periodo.Substring(0, 2);
                string anio = periodo.Substring(3, 4);
                string idtipocal = "D";
                string idtipoper = "O";
                string emplea = funciones.getValorComboBox(cmbEmpleador, 11);
                string estane = funciones.getValorComboBox(cmbEstablecimiento, 4);
                string tiporegistro = funciones.getValorComboBox(cmbTipoRegistro, 1);
                string idtipoplan = funciones.getValorComboBox(cmbTipoPlan, 3);
                string codvar = string.Empty;

                float cantidad = float.Parse(txtCantidad.Text);
                float precio = float.Parse(txtPrecio.Text);
                string fecha = txtFechaIni.Text;

                if (calplan.getDatosCalPlan(messem, anio, idtipoper, idcia, idtipocal, "ESTADO") != "C")
                {

                    string glosa = string.Empty;
                    string idperplan, iddestajo;
                    string nombreDes = string.Empty;
                    float cantidad_individual, subtotal, movilidad, refrigerio, adicional, total = 0;

                    if (tiporegistro.Equals("P"))
                    {
                        nombreDes = "desplan";
                    }
                    else
                    {
                        nombreDes = "desret";
                    }

                    string query = "  select idperplan,iddestajo,movilidad,refrigerio,adicional from " + @nombreDes;
                    query = query + " where idcia='" + @idcia + "' and idtipocal='" + @idtipocal + "' ";
                    query = query + " and idtipoper='" + @idtipoper + "' ";
                    query = query + " and messem='" + @messem + "' and anio='" + @anio + "' ";
                    query = query + " and idproddes='" + @idproddes + "' ";
                    query = query + " and idtipoplan='" + @idtipoplan + "' and emplea='" + @emplea + "' ";
                    query = query + " and estane='" + @estane + "' and idzontra= '" + @idzontra + "' ";

                    SqlConnection conexion = new SqlConnection();
                    conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
                    conexion.Open();

                    DataTable dtdes = new DataTable();
                    SqlDataAdapter da_dtdes = new SqlDataAdapter();
                    da_dtdes.SelectCommand = new SqlCommand(query, conexion);
                    da_dtdes.Fill(dtdes);

                    int nrofilas = dtdes.Rows.Count;
                    if (nrofilas > 0)
                    {
                        cantidad_individual = cantidad / nrofilas;
                        subtotal = cantidad_individual * precio;
                        glosa = " CANT.: " + Convert.ToString(cantidad) + funciones.replicateCadena(" ", 2) + "PERS.: " + Convert.ToString(nrofilas);
                        foreach (DataRow row_dtdes in dtdes.Rows)
                        {
                            idperplan = row_dtdes["idperplan"].ToString();
                            iddestajo = row_dtdes["iddestajo"].ToString();
                            movilidad = float.Parse(row_dtdes["movilidad"].ToString());
                            refrigerio = float.Parse(row_dtdes["refrigerio"].ToString());
                            adicional = float.Parse(row_dtdes["adicional"].ToString());
                            total = subtotal + movilidad + refrigerio + adicional;
                            Destajo destajo = new Destajo();
                            destajo.actualizarDestajo(tiporegistro, "EDITAR", idcia, idperplan, messem, anio, idtipocal, idtipoper,
                                fecha, idproddes, idzontra, cantidad_individual, precio, subtotal, movilidad, refrigerio, adicional, total,
                                idtipoplan, emplea, estane, iddestajo, glosa, codvar);


                        }
                    }
                    conexion.Close();
                    MessageBox.Show("Distribución Completada", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
                else
                {
                    MessageBox.Show("El Periodo Laboral ya se encuentra cerrado", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (FormatException ex)
            {
                MessageBox.Show(ex.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cmbTipoRegistro_SelectedIndexChanged(object sender, EventArgs e)
        {

            Funciones fn = new Funciones();

            if (fn.getValorComboBox(cmbTipoRegistro, 1).Equals("P"))
            {
                GlobalVariables gv = new GlobalVariables();
                string idcia = gv.getValorCia();
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
    }
}