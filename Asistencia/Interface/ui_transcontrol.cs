using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Configuration;
using System.Data.SqlClient;

namespace CaniaBrava
{
    public partial class ui_transcontrol : ui_form
    {
        string _idcia;

        public ui_transcontrol()
        {
            InitializeComponent();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ui_transcontrol_Load(object sender, EventArgs e)
        {
            Funciones funciones = new Funciones();
            GlobalVariables variables = new GlobalVariables();
            string query;
            this._idcia = variables.getValorCia();
            string idcia = this._idcia;
            query = "SELECT idtipoper as clave,destipoper as descripcion FROM tipoper order by 1 asc";
            funciones.listaComboBox(query, cmbTipoTrabajador, "");
            query = "SELECT ruccia as clave,descia as descripcion FROM ciafile WHERE idcia='" + @idcia + "';";
            funciones.listaComboBox(query, cmbEmpleador, "");
            query = "SELECT idtipoplan as clave,destipoplan as descripcion FROM tipoplan ";
            query = query + " where idtipoplan in (select idtipoplan from reglabcia where idcia='" + @idcia + "') order by 1 asc;";
            funciones.listaComboBox(query, cmbTipoPlan, "");
            string idtipoplan = funciones.getValorComboBox(cmbTipoPlan, 3);
            ui_listaTipoCal(idcia, idtipoplan);
            MaesGen maesgen = new MaesGen();
            maesgen.listaDetMaesGen("031", cmbTipo, "B");
        }

        private void ui_listaTipoCal(string idcia, string idtipoplan)
        {
            Funciones funciones = new Funciones();
            string query;
            query = " SELECT idtipocal as clave,destipocal as descripcion ";
            query = query + " FROM tipocal where idtipocal in ('N') and idtipocal in (select idtipocal ";
            query = query + " from calcia where idcia='" + @idcia + "' and idtipoplan='" + @idtipoplan + "') ";
            query = query + " order by ordentipocal asc;";
            funciones.listaComboBox(query, cmbTipoCal, "");
        }

        private void cmbTipoTrabajador_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtMesSem.Clear();
            txtFechaIni.Clear();
            txtFechaFin.Clear();
            txtMesSem.Focus();
            lblPeriodo.Visible = false;
        }

        private void cmbTipoPlan_SelectedIndexChanged(object sender, EventArgs e)
        {
            Funciones funciones = new Funciones();
            string idcia = this._idcia;
            string idtipoplan = funciones.getValorComboBox(cmbTipoPlan, 3);
            ui_listaTipoCal(idcia, idtipoplan);
            txtMesSem.Clear();
            txtFechaIni.Clear();
            txtFechaFin.Clear();
            txtMesSem.Focus();
            lblPeriodo.Visible = false;
        }

        private void cmbTipoCal_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtMesSem.Clear();
            txtFechaIni.Clear();
            txtFechaFin.Clear();
            txtMesSem.Focus();
            lblPeriodo.Visible = false;
        }

        private void txtMesSem_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                Funciones funciones = new Funciones();
                CalPlan calplan = new CalPlan();
                string idcia = this._idcia;
                string idtipoper = funciones.getValorComboBox(cmbTipoTrabajador, 1);
                string periodo = txtMesSem.Text.Trim() + funciones.replicateCadena(" ", 6);
                string messem = periodo.Substring(0, 2);
                string anio = periodo.Substring(3, 4);
                string idtipocal = funciones.getValorComboBox(cmbTipoCal, 1);

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

        private void radioButtonSiEmp_CheckedChanged(object sender, EventArgs e)
        {
            Funciones funciones = new Funciones();
            string idcia = this._idcia;
            string query = "SELECT rucemp as clave,razonemp as descripcion ";
            query = query + " FROM emplea WHERE idciafile='" + @idcia + "' order by rucemp asc;";
            funciones.listaComboBox(query, cmbEmpleador, "");
            cmbEmpleador.Focus();
        }

        private void radioButtonNoEmp_CheckedChanged(object sender, EventArgs e)
        {
            Funciones funciones = new Funciones();
            string idcia = this._idcia;
            string query = "SELECT ruccia as clave,descia as descripcion FROM ciafile WHERE idcia='" + @idcia + "';";
            funciones.listaComboBox(query, cmbEmpleador, "");
            cmbEmpleador.Enabled = false;
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            try
            {
                Funciones funciones = new Funciones();
                CalPlan calplan = new CalPlan();

                string idcia = this._idcia;
                string idtipoper = funciones.getValorComboBox(cmbTipoTrabajador, 1);
                string idtipoplan = funciones.getValorComboBox(cmbTipoPlan, 3);
                string periodo = txtMesSem.Text.Trim() + funciones.replicateCadena(" ", 6);
                string messem = periodo.Substring(0, 2);
                string anio = periodo.Substring(3, 4);
                string idtipocal = funciones.getValorComboBox(cmbTipoCal, 1);
                string rucemp = funciones.getValorComboBox(cmbEmpleador, 11);
                string motivonosubsi = funciones.getValorComboBox(cmbTipo, 4);
                string valorValida = "G";
                if (calplan.getDatosCalPlan(messem, anio, idtipoper, idcia, idtipocal, "ESTADO") != "C")
                {

                    if (cmbTipo.Text == string.Empty && valorValida == "G")
                    {
                        valorValida = "B";
                        MessageBox.Show("No ha seleccionado Tipo en Días No Laborados", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        cmbTipo.Focus();
                    }

                    if (cmbTipo.Text != string.Empty && valorValida == "G")
                    {
                        MaesGen maesgen = new MaesGen();
                        string resultado = maesgen.verificaComboBoxMaesGen("031", cmbTipo.Text.Trim());
                        if (resultado.Trim() == string.Empty)
                        {
                            valorValida = "B";
                            MessageBox.Show("Dato incorrecto en Tipo en Días No Laborados", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            cmbTipo.Focus();
                        }
                    }

                    if (valorValida.Equals("G"))
                    {
                        lblprocesando.Visible = true;

                        DataPlan dataplan = new DataPlan();
                        dataplan.eliminarDataPlanPorPeriodo(idcia, anio, messem, idtipoper, idtipocal, idtipoplan);
                        ControlToParteDiario(idcia, messem, anio, idtipocal, idtipoper, idtipoplan, rucemp, motivonosubsi);
                        MessageBox.Show("Proceso concluído", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        lblprocesando.Visible = false;
                    }

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

        public void ControlToParteDiario(string idcia, string messem, string anio, string idtipocal,
            string idtipoper, string idtipoplan, string emplea, string motivonosubsi)
        {
            CalPlan calplan = new CalPlan();
            UtileriasFechas utilfechas = new UtileriasFechas();
            string fechaini = calplan.getDatosCalPlan(messem, anio, idtipoper, idcia, idtipocal, "FECHAINI");
            string fechafin = calplan.getDatosCalPlan(messem, anio, idtipoper, idcia, idtipocal, "FECHAFIN");
            int dias = utilfechas.diferenciaEntreFechas(fechaini, fechafin);

            DataTable dtasis = new DataTable();
            string query = " select A.idperplan,A.diurno,A.nocturno,A.diaslab,A.he25,A.he35, ";
            query = query + " A.he100,B.estane from datasis A left join perplan B ";
            query = query + " on A.idcia=B.idcia and A.idperplan=B.idperplan ";
            query = query + " where A.idcia='" + @idcia + "' and A.idtipocal='" + @idtipocal + "' ";
            query = query + " and A.idtipoper='" + @idtipoper + "' and A.idtipoplan='" + @idtipoplan + "' ";
            query = query + " and A.messem='" + @messem + "' and A.anio='" + @anio + "'; ";
            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = new SqlCommand(query, conexion);
            da.Fill(dtasis);

            string idperplan = string.Empty;
            int diurno = 0, nocturno = 0, diasefelab = 0, diassubsi = 0, diasnosubsi = 0, diasdom = 0;
            float he25 = 0, he35, he100 = 0;
            string estane = string.Empty;

            for (int i = 0; i < dias; i++)
            {
                if (utilfechas.dayOfWeek(utilfechas.incrementarFecha(fechaini, i)).Equals("dom"))
                {
                    diasdom++;
                }
            }

            string tipo, fechaininosubsi, fechafinnosubsi;
            int riesgo = 0, diasvac = 0;
            string finivac = "", ffinvac = "", regvac = "N", pervac = "";
            float total = 0, cantidad = 0;
            string fecha;

            if (dtasis.Rows.Count > 0)
            {
                Asiste asiste = new Asiste();
                foreach (DataRow row_dtasis in dtasis.Rows)
                {
                    idperplan = row_dtasis["idperplan"].ToString();
                    diurno = int.Parse(row_dtasis["diurno"].ToString());
                    nocturno = int.Parse(row_dtasis["nocturno"].ToString());
                    diasefelab = int.Parse(row_dtasis["diaslab"].ToString());
                    diasnosubsi = dias - (diasefelab + diasdom);
                    he25 = float.Parse(row_dtasis["he25"].ToString());
                    he35 = float.Parse(row_dtasis["he35"].ToString());
                    he100 = float.Parse(row_dtasis["he100"].ToString());
                    estane = row_dtasis["estane"].ToString();

                    if ((diasefelab + he25 + he35 + he100) > 0)
                    {
                        DataPlan dataplan = new DataPlan();
                        dataplan.actualizarDataPlan("AGREGAR", idperplan, idcia, anio, messem, idtipoper, idtipocal, diasefelab, diassubsi,
                            diasnosubsi, dias, emplea, estane, riesgo, idtipoplan, he25, he35, he100, finivac,
                            ffinvac, diasvac, total, cantidad, diurno, nocturno, diasdom, regvac, pervac);

                        for (int i = 0; i < dias; i++)
                        {

                            fecha = utilfechas.incrementarFecha(fechaini, i);
                            if (!asiste.getAsiste(idperplan, idcia, anio, messem, idtipoper, idtipoplan, idtipocal, fecha))
                            {
                                if (utilfechas.dayOfWeek(utilfechas.incrementarFecha(fechaini, i)) != "dom")
                                {
                                    fechaininosubsi = utilfechas.incrementarFecha(fechaini, i);
                                    fechafinnosubsi = utilfechas.incrementarFecha(fechaini, i);
                                    DiasSubsi diassubsidiados = new DiasSubsi();
                                    tipo = "N";
                                    diassubsidiados.setDiasSubsi(idperplan, idcia, anio, messem, idtipoper, idtipocal, tipo, motivonosubsi, "",
                                        fechaininosubsi, fechafinnosubsi, 1, 0, idtipoplan, "");
                                    diassubsidiados.actualizarDiasSubsi("AGREGAR");
                                }
                            }
                        }
                    }

                }
            }

            conexion.Close();
        }

        private void cmbTipo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                if (cmbTipo.Text != String.Empty)
                {
                    MaesGen maesgen = new MaesGen();
                    maesgen.validarDetMaesGen("031", cmbTipo, cmbTipo.Text);
                }
                e.Handled = true;
                toolstripform.Items[0].Select();
                toolstripform.Focus();

            }
        }
    }
}