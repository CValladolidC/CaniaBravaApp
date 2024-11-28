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
    public partial class ui_asisdia : ui_form
    {
        string _idcia;

        public ui_asisdia()
        {
            InitializeComponent();
        }

        private void ui_asisdia_Load(object sender, EventArgs e)
        {

            Funciones funciones = new Funciones();
            GlobalVariables variables = new GlobalVariables();
            MaesGen maesgen = new MaesGen();
            this._idcia = variables.getValorCia();
            string idcia = this._idcia;
            string query;
            query = "SELECT idtipoper as clave,destipoper as descripcion FROM tipoper order by 1 asc";
            funciones.listaComboBox(query, cmbTipoTrabajador, "");
            query = "SELECT ruccia as clave,descia as descripcion FROM ciafile WHERE idcia='" + @idcia + "';";
            funciones.listaComboBox(query, cmbEmpleador, "");
            query = "SELECT idtipoplan as clave,destipoplan as descripcion FROM tipoplan ";
            query = query + " where idtipoplan in (select idtipoplan from reglabcia where idcia='" + @idcia + "') order by 1 asc;";
            funciones.listaComboBox(query, cmbTipoPlan, "");
            maesgen.listaDetMaesGen("008", cmbSeccion, "X");
            cmbSeccion.Text = "X   TODOS";
            cmbHoras.Text = "H.Ext. 100%";
            string idtipoplan = funciones.getValorComboBox(cmbTipoPlan, 3);
            ui_listaTipoCal(idcia, idtipoplan);
            ui_ListaDataPlan();

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

        private void ui_ListaDataPlan()
        {

            Funciones funciones = new Funciones();
            CalPlan calplan = new CalPlan();
            UtileriasFechas utilfechas = new UtileriasFechas();
            string idcia = this._idcia;

            string idtipoper = funciones.getValorComboBox(cmbTipoTrabajador, 1);
            string idtipoplan = funciones.getValorComboBox(cmbTipoPlan, 3);
            string periodo = txtMesSem.Text.Trim() + funciones.replicateCadena(" ", 6);
            string messem = periodo.Substring(0, 2);
            string anio = periodo.Substring(3, 4);
            string idtipocal = funciones.getValorComboBox(cmbTipoCal, 1);
            string fechaini = calplan.getDatosCalPlan(messem, anio, idtipoper, idcia, idtipocal, "FECHAINI");
            string fechafin = calplan.getDatosCalPlan(messem, anio, idtipoper, idcia, idtipocal, "FECHAFIN");
            string seccion = funciones.getValorComboBox(cmbSeccion, 4);

            int dias = utilfechas.diferenciaEntreFechas(fechaini, fechafin);

            string cadenaseccion = string.Empty;
            string cadenaCese = string.Empty;


            if (seccion != "X")
            {
                cadenaseccion = " and A.seccion='" + @seccion + "' ";

            }

            if (chkCese.Checked)
            {
                cadenaCese = " and G.stateperlab='V' ";
            }

            if (dias > 0)
            {
                Asiste asiste = new Asiste();
                DataTable dt = new DataTable();
                dt.Columns.Add("codigo", typeof(string));
                dt.Columns.Add("nombre", typeof(string));
                dt.Columns.Add("fechaini", typeof(string));
                dt.Columns.Add("fechafin", typeof(string));

                for (int i = 0; i < dias; i++)
                {
                    dt.Columns.Add("fecha" + i.ToString(), typeof(bool));
                }

                dt.Columns.Add("diurno", typeof(int));
                dt.Columns.Add("nocturno", typeof(int));
                dt.Columns.Add("dias", typeof(int));
                dt.Columns.Add("he25", typeof(decimal));
                dt.Columns.Add("he35", typeof(decimal));
                dt.Columns.Add("he100", typeof(decimal));

                DataTable dtper = new DataTable();
                string query = " SELECT A.idperplan,CONCAT(A.apepat,' ',A.apemat,' , ',A.nombres) as nombre,";
                query = query + " D.desmaesgen as seccion,G.fechaini,G.fechafin from perplan A ";
                query = query + " left join maesgen D on D.idmaesgen='008' and A.seccion=D.clavemaesgen ";
                query = query + " left join view_perlab F on A.idcia=F.idcia and A.idperplan=F.idperplan ";
                query = query + " left join perlab G on F.idcia=G.idcia and F.idperplan=G.idperplan and F.idperlab=G.idperlab";
                query = query + " WHERE A.idcia='" + @idcia + "' and A.idtipoper='" + @idtipoper + "' ";
                query = query + " and A.idtipoplan='" + @idtipoplan + "' ";
                query = query + @cadenaseccion + cadenaCese + " order by nombre asc ";

                SqlConnection conexion = new SqlConnection();
                conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
                conexion.Open();
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = new SqlCommand(query, conexion);
                da.Fill(dtper);

                string idperplan = string.Empty;

                if (dtper.Rows.Count > 0)
                {

                    foreach (DataRow row_per in dtper.Rows)
                    {
                        DataRow dr;
                        dr = dt.NewRow();
                        idperplan = row_per["idperplan"].ToString();
                        dr[0] = row_per["idperplan"].ToString();
                        dr[1] = row_per["nombre"].ToString();
                        dr[2] = (row_per["fechaini"].ToString() + funciones.replicateCadena(" ", 10)).Substring(0, 10);
                        dr[3] = (row_per["fechafin"].ToString() + funciones.replicateCadena(" ", 10)).Substring(0, 10);
                        string fecha = string.Empty;
                        for (int i = 0; i < dias; i++)
                        {
                            fecha = utilfechas.incrementarFecha(fechaini, i);
                            dr[i + 4] = asiste.getAsiste(idperplan, idcia, anio, messem, idtipoper, idtipoplan, idtipocal, fecha);
                        }
                        Datasis datasis = new Datasis();
                        dr[dias + 4] = int.Parse(datasis.getDatasis(idperplan, idcia, anio, messem, idtipoper, idtipoplan, idtipocal, "DIURNO"));
                        dr[dias + 5] = int.Parse(datasis.getDatasis(idperplan, idcia, anio, messem, idtipoper, idtipoplan, idtipocal, "NOCTURNO"));
                        dr[dias + 6] = int.Parse(datasis.getDatasis(idperplan, idcia, anio, messem, idtipoper, idtipoplan, idtipocal, "DIASLAB"));
                        dr[dias + 7] = decimal.Parse(datasis.getDatasis(idperplan, idcia, anio, messem, idtipoper, idtipoplan, idtipocal, "HE25"));
                        dr[dias + 8] = decimal.Parse(datasis.getDatasis(idperplan, idcia, anio, messem, idtipoper, idtipoplan, idtipocal, "HE35"));
                        dr[dias + 9] = decimal.Parse(datasis.getDatasis(idperplan, idcia, anio, messem, idtipoper, idtipoplan, idtipocal, "HE100"));
                        dt.Rows.Add(dr);
                    }
                }

                dgvdetalle.RowHeadersVisible = false;
                dgvdetalle.AllowUserToAddRows = false;
                dgvdetalle.DefaultCellStyle.SelectionBackColor = Color.Black;
                dgvdetalle.DefaultCellStyle.SelectionForeColor = Color.White;
                dgvdetalle.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dgvdetalle.MultiSelect = false;

                dgvdetalle.DataSource = dt;
                dgvdetalle.Columns[0].HeaderText = "Código";
                dgvdetalle.Columns[1].HeaderText = "Apellidos y Nombres";
                dgvdetalle.Columns[2].HeaderText = "F.Ini.Lab.";
                dgvdetalle.Columns[3].HeaderText = "F.Fin.Lab.";

                dgvdetalle.Columns[0].DefaultCellStyle.BackColor = Color.LightGray;
                dgvdetalle.Columns[1].DefaultCellStyle.BackColor = Color.LightGray;
                dgvdetalle.Columns[2].DefaultCellStyle.BackColor = Color.LightGray;
                dgvdetalle.Columns[3].DefaultCellStyle.BackColor = Color.LightGray;

                dgvdetalle.Columns[0].ReadOnly = true;
                dgvdetalle.Columns[1].ReadOnly = true;
                dgvdetalle.Columns[2].ReadOnly = true;
                dgvdetalle.Columns[3].ReadOnly = true;

                dgvdetalle.Columns[0].Frozen = true;
                dgvdetalle.Columns[1].Frozen = true;
                dgvdetalle.Columns[2].Frozen = true;
                dgvdetalle.Columns[3].Frozen = true;

                dgvdetalle.Columns[0].Width = 50;
                dgvdetalle.Columns[1].Width = 230;
                dgvdetalle.Columns[2].Width = 70;
                dgvdetalle.Columns[3].Width = 70;

                for (int i = 0; i < dias; i++)
                {

                    if (utilfechas.dayOfWeek(utilfechas.incrementarFecha(fechaini, i)).Equals("dom"))
                    {
                        dgvdetalle.Columns[i + 4].DefaultCellStyle.BackColor = Color.Red;
                    }

                    dgvdetalle.Columns[i + 4].HeaderText = utilfechas.dayOfWeek(utilfechas.incrementarFecha(fechaini, i)) + "  " + utilfechas.incrementarFecha(fechaini, i).Substring(0, 2);
                    dgvdetalle.Columns[i + 4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    dgvdetalle.Columns[i + 4].Width = 28;

                }

                dgvdetalle.Columns[dias + 4].HeaderText = "Dias Diurnos";
                dgvdetalle.Columns[dias + 4].Width = 50;
                dgvdetalle.Columns[dias + 4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgvdetalle.Columns[dias + 4].ReadOnly = true;
                dgvdetalle.Columns[dias + 4].DefaultCellStyle.BackColor = Color.LightGray;


                dgvdetalle.Columns[dias + 5].HeaderText = "Dias Nocturnos";
                dgvdetalle.Columns[dias + 5].Width = 55;
                dgvdetalle.Columns[dias + 5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;


                dgvdetalle.Columns[dias + 6].HeaderText = "Dias Lab.";
                dgvdetalle.Columns[dias + 6].Width = 50;
                dgvdetalle.Columns[dias + 6].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgvdetalle.Columns[dias + 6].DefaultCellStyle.BackColor = Color.Blue;
                dgvdetalle.Columns[dias + 6].DefaultCellStyle.ForeColor = Color.White;
                dgvdetalle.Columns[dias + 6].ReadOnly = true;


                dgvdetalle.Columns[dias + 7].HeaderText = "H. Ext. 25%";
                dgvdetalle.Columns[dias + 7].Width = 50;
                dgvdetalle.Columns[dias + 7].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;


                dgvdetalle.Columns[dias + 8].HeaderText = "H. Ext. 35%";
                dgvdetalle.Columns[dias + 8].Width = 50;
                dgvdetalle.Columns[dias + 8].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;


                dgvdetalle.Columns[dias + 9].HeaderText = "H. Ext. 100%";
                dgvdetalle.Columns[dias + 9].Width = 50;
                dgvdetalle.Columns[dias + 9].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;



            }
        }

        private void cmbTipoTrabajador_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                Funciones funciones = new Funciones();
                string clave = funciones.getValorComboBox(cmbTipoTrabajador, 1);
                string query;
                if (cmbTipoTrabajador.Text != String.Empty)
                {
                    query = "SELECT idtipoper as clave,destipoper as descripcion FROM tipoper WHERE idtipoper='" + @clave + "';";
                    funciones.validarCombobox(query, cmbTipoTrabajador);
                }
                lblPeriodo.Visible = false;
                txtMesSem.Clear();
                txtFechaIni.Clear();
                txtFechaFin.Clear();
                e.Handled = true;
                txtMesSem.Focus();
            }
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
                        btnGrabar.Enabled = true;

                    }
                    else
                    {
                        lblPeriodo.Text = "Periodo Laboral Cerrado";
                        btnGrabar.Enabled = false;

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
                ui_ListaDataPlan();

            }
        }

        private void radioButtonNoEmp_CheckedChanged(object sender, EventArgs e)
        {
            Funciones funciones = new Funciones();
            string idcia = this._idcia;
            string squery = "SELECT ruccia as clave,descia as descripcion FROM ciafile WHERE idcia='" + @idcia + "';";
            funciones.listaComboBox(squery, cmbEmpleador, "");
            cmbEmpleador.Enabled = false;
            ui_ListaDataPlan();
        }

        private void radioButtonSiEmp_CheckedChanged(object sender, EventArgs e)
        {
            Funciones funciones = new Funciones();
            string idcia = this._idcia;

            string squery = "SELECT rucemp as clave,razonemp as descripcion FROM emplea WHERE idciafile='" + @idcia + "' order by rucemp asc;";
            funciones.listaComboBox(squery, cmbEmpleador, "");
            cmbEmpleador.Enabled = true;
            ui_ListaDataPlan();
            cmbEmpleador.Focus();
        }

        private void cmbTipoTrabajador_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtMesSem.Clear();
            txtFechaIni.Clear();
            txtFechaFin.Clear();
            txtMesSem.Focus();
            ui_ListaDataPlan();
        }

        private void cmbEmpleador_SelectedIndexChanged(object sender, EventArgs e)
        {
            ui_ListaDataPlan();
        }

        private void cmbTipoPlan_SelectedIndexChanged(object sender, EventArgs e)
        {
            Funciones funciones = new Funciones();
            string idtipoplan = funciones.getValorComboBox(cmbTipoPlan, 3);
            string idcia = this._idcia;
            lblPeriodo.Visible = false;
            txtMesSem.Clear();
            txtFechaIni.Clear();
            txtFechaFin.Clear();
            txtMesSem.Focus();
            ui_listaTipoCal(idcia, idtipoplan);
            ui_ListaDataPlan();
        }

        private void cmbTipoCal_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblPeriodo.Visible = false;
            txtMesSem.Clear();
            txtFechaIni.Clear();
            txtFechaFin.Clear();
            txtMesSem.Focus();
            ui_ListaDataPlan();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnGrabar_Click(object sender, EventArgs e)
        {
            Funciones funciones = new Funciones();
            CalPlan calplan = new CalPlan();
            Asiste asiste = new Asiste();
            Datasis datasis = new Datasis();
            string idcia = this._idcia;
            string idtipoper = funciones.getValorComboBox(cmbTipoTrabajador, 1);
            string idtipoplan = funciones.getValorComboBox(cmbTipoPlan, 3);
            string periodo = txtMesSem.Text.Trim() + funciones.replicateCadena(" ", 6);
            string messem = periodo.Substring(0, 2);
            string anio = periodo.Substring(3, 4);
            string idtipocal = funciones.getValorComboBox(cmbTipoCal, 1);
            string fechaini = calplan.getDatosCalPlan(messem, anio, idtipoper, idcia, idtipocal, "FECHAINI");
            string fechafin = calplan.getDatosCalPlan(messem, anio, idtipoper, idcia, idtipocal, "FECHAFIN");


            UtileriasFechas utilfechas = new UtileriasFechas();
            int dias = utilfechas.diferenciaEntreFechas(fechaini, fechafin);
            dgvdetalle.EndEdit();
            string codigo = string.Empty;
            string nombre = string.Empty;
            int diurno = 0;
            int nocturno = 0;
            int diaslab = 0;
            decimal he25 = 0;
            decimal he35 = 0;
            decimal he100 = 0;

            foreach (DataGridViewRow row in dgvdetalle.Rows)
            {
                codigo = row.Cells["codigo"].Value.ToString();
                nombre = row.Cells["nombre"].Value.ToString();

                for (int i = 0; i < dias; i++)
                {
                    asiste.delAsiste(codigo, idcia, anio, messem, idtipoper, idtipoplan, idtipocal, utilfechas.incrementarFecha(fechaini, i));

                    if (row.Cells["fecha" + i.ToString()].Value.ToString().Equals("True"))
                    {
                        asiste.updAsiste(codigo, idcia, anio, messem, idtipoper, idtipoplan, idtipocal, utilfechas.incrementarFecha(fechaini, i));
                    }
                }

                diurno = (int)(row.Cells["diurno"].Value);
                nocturno = (int)(row.Cells["nocturno"].Value);
                diaslab = (int)(row.Cells["dias"].Value);
                he25 = decimal.Parse(row.Cells["he25"].Value.ToString());
                he35 = decimal.Parse(row.Cells["he35"].Value.ToString());
                he100 = decimal.Parse(row.Cells["he100"].Value.ToString());

                datasis.delDatasis(codigo, idcia, anio, messem, idtipoper, idtipoplan, idtipocal);

                if (diaslab + he25 + he35 + he100 > 0)
                {
                    datasis.updDatasis(codigo, idcia, anio, messem, idtipoper, idtipoplan, idtipocal, diurno, nocturno, diaslab, he25, he35, he100);
                }
            }

            MessageBox.Show("Información guardada exitosamente", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void dgvdetalle_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                string fechaini = txtFechaIni.Text;
                string fechafin = txtFechaFin.Text;
                string horas = cmbHoras.Text.Trim();
                UtileriasFechas utilfechas = new UtileriasFechas();
                int dias = utilfechas.diferenciaEntreFechas(fechaini, fechafin);
                dgvdetalle.EndEdit();

                foreach (DataGridViewRow row in dgvdetalle.Rows)
                {
                    int diaslab = 0;
                    int diasdom = 0;
                    for (int i = 0; i < dias; i++)
                    {
                        if (row.Cells["fecha" + i.ToString()].Value.ToString().Equals("True"))
                        {
                            if (utilfechas.dayOfWeek(utilfechas.incrementarFecha(fechaini, i)).Equals("dom"))
                            {
                                diasdom++;
                            }
                            else
                            {
                                diaslab++;
                            }
                        }
                    }


                    row.Cells["dias"].Value = diaslab;

                    if (horas.Equals("H.Ext. 100%"))
                    {
                        row.Cells["he100"].Value = diasdom * 8;
                        row.Cells["he35"].Value = 0;
                        row.Cells["he25"].Value = 0;
                    }
                    else
                    {
                        if (horas.Equals("H.Ext. 35%"))
                        {
                            row.Cells["he35"].Value = diasdom * 8;
                            row.Cells["he100"].Value = 0;
                            row.Cells["he25"].Value = 0;
                        }
                        else
                        {
                            row.Cells["he25"].Value = diasdom * 8;
                            row.Cells["he100"].Value = 0;
                            row.Cells["he35"].Value = 0;
                        }
                    }

                }

                for (int i = 0; i < dias; i++)
                {
                    dgvdetalle.Columns[i + 4].HeaderText = utilfechas.dayOfWeek(utilfechas.incrementarFecha(fechaini, i)) + "  " + utilfechas.incrementarFecha(fechaini, i).Substring(0, 2);
                    dgvdetalle.Columns[i + 4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    dgvdetalle.Columns[i + 4].Width = 30;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvdetalle_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void dgvdetalle_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (dgvdetalle.RowCount > 0)
                {
                    string fechaini = txtFechaIni.Text;
                    string fechafin = txtFechaFin.Text;
                    UtileriasFechas utilfechas = new UtileriasFechas();
                    int dias = utilfechas.diferenciaEntreFechas(fechaini, fechafin);
                    int nocturno = (int)(dgvdetalle.Rows[dgvdetalle.CurrentRow.Index].Cells[dias + 5].Value);
                    int diaslab = (int)(dgvdetalle.Rows[dgvdetalle.CurrentRow.Index].Cells[dias + 6].Value);
                    if (nocturno > diaslab)
                    {
                        nocturno = diaslab;
                        dgvdetalle.Rows[dgvdetalle.CurrentRow.Index].Cells[dias + 5].Value = nocturno;

                    }
                    else
                    {
                        dgvdetalle.Rows[dgvdetalle.CurrentRow.Index].Cells[dias + 4].Value = diaslab - nocturno;
                    }
                }
            }
            catch (FormatException ex)
            {
                MessageBox.Show(ex.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cmbHoras_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void cmbSeccion_SelectedIndexChanged(object sender, EventArgs e)
        {
            ui_ListaDataPlan();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            string texto = txtBuscar.Text.Trim();
            Busquedas busquedas = new Busquedas();
            busquedas.buscarDataGridView(texto, 1, dgvdetalle);

        }

        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {

        }

        private void chkCese_CheckedChanged(object sender, EventArgs e)
        {
            ui_ListaDataPlan();
        }

        private void groupBox5_Enter(object sender, EventArgs e)
        {

        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            Exporta exporta = new Exporta();
            exporta.Excel_FromDataGridView(dgvdetalle);
        }
    }
}