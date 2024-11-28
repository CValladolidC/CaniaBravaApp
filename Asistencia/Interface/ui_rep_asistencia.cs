using System;
using System.Data;
using System.Windows.Forms;
using System.Configuration;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Globalization;

namespace CaniaBrava
{
    public partial class ui_rep_asistencia : Form
    {
        //Oliver Cruz Tuanama
        GlobalVariables variables = new GlobalVariables();
        Funciones funciones = new Funciones();
        SqlConnection conexion = new SqlConnection();

        private TextBox TextBoxActivo;
        public TextBox _TextBoxActivo
        {
            get { return TextBoxActivo; }
            set { TextBoxActivo = value; }
        }

        public ui_rep_asistencia()
        {
            InitializeComponent();

            if (funciones.VersionAssembly()) Application.ExitThread();
        }

        private void ui_rep_asistencia_Load(object sender, EventArgs e)
        {
            string query = " select a.idcia as clave, a.descia as descripcion from ciafile a (nolock) ";
            funciones.listaComboBox(query, cmbCia, "X");

            query = "select idtipoper as clave,destipoper as descripcion from tipoper (NOLOCK) ";
            funciones.listaComboBox(query, cmbNominas, "X");

            query = "SELECT idusr [clave],desusr [descripcion] FROM usrfile (NOLOCK) WHERE typeusr='05' AND desusr NOT LIKE '%COMEDOR%'";
            funciones.listaComboBox(query, cmbSedes, "X");

            dtpFecfin.MinDate = dtpFecini.Value;

            btnNuevo.Visible = false;
            btnEditar.Visible = false;
            btnEliminar.Visible = false;
            switch (variables.getValorNivelUsr())
            {
                case "1": btnNuevo.Visible = true; btnEditar.Visible = true; btnEliminar.Visible = true; break;
            }

            if (variables.getValorTypeUsr() == "00")
            {
                btnNuevo.Visible = true; btnEditar.Visible = true; btnEliminar.Visible = true;
            }

            //ui_Lista();
        }

        private void ui_Lista()
        {
            string sede = funciones.getValorComboBox(cmbSedes, 7);
            string nomina = funciones.getValorComboBox(cmbNominas, 2);
            string cia = funciones.getValorComboBox(cmbCia, 2);
            string geren = funciones.getValorComboBox(cmbGerencia, 8);
            string area = funciones.getValorComboBox(cmbCencos, 8);
            string idperplan = txtCodigoInterno.Text.Trim();

            if (sede == "X   TOD") { sede = string.Empty; }
            if (nomina == "X") { nomina = string.Empty; }
            if (cia == "X") { cia = string.Empty; }
            if (geren == "X   TODO") { geren = string.Empty; }
            if (area == "X   TODO") { area = string.Empty; }

            string fecini = dtpFecini.Value.ToString("yyyy-MM-dd");
            string fecfin = dtpFecfin.Value.ToString("yyyy-MM-dd");
            string query = "EXEC SP_REPORTE_ASIS '" + fecini + "', '" + fecfin + "', '" + cia + "', '" + geren + "', '" + area + "'";
            query += " ,'" + idperplan + "','" + sede + "','" + nomina + "' ";

            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();

            try
            {
                using (SqlCommand myCommand = new SqlCommand(query, conexion))
                {
                    myCommand.CommandTimeout = 360;
                    using (SqlDataReader odr = myCommand.ExecuteReader())
                    {
                        List<Asiste> lista = new List<Asiste>();
                        while (odr.Read())
                        {
                            lista.Add(new Asiste()
                            {
                                Codigo = odr["Codigo"].ToString(),
                                Trabajador = odr["Trabajador"].ToString(),
                                Empresa = odr["Empresa"].ToString(),
                                Gerencia = odr["Gerencia"].ToString(),
                                Area = odr["Area"].ToString(),
                                Nomina = odr["Nomina"].ToString(),
                                TipHorario = odr["TipHorario"].ToString(),
                                Fecha = odr["Fecha"].ToString(),
                                IngresoProg = odr["IngresoProg"].ToString(),
                                SalidaProg = odr["SalidaProg"].ToString(),
                                IngresoReal = odr["IngresoReal"].ToString(),
                                SalidaReal = odr["SalidaReal"].ToString(),
                                Movimientos = odr["Movimientos"].ToString(),
                                Sedes = odr["Sedes"].ToString(),
                                HrTrab = odr["HrTrab"].ToString(),
                                HrExt = odr["HrExt"].ToString(),
                                Comentarios = odr["Comentarios"].ToString(),
                                Comentarios2 = odr["Comentarios2"].ToString(),
                                nromov = odr["nromov"].ToString()
                            });
                        }
                        //    DataSet myDataSet = new DataSet();
                        //odr.Fill(myDataSet, "tabla");
                        funciones.formatearDataGridView(dgvdetalle);

                        dgvdetalle.DataSource = lista;
                        //dgvdetalle.DataSource = myDataSet.Tables["tabla"];

                        dgvdetalle.Columns[0].Width = 70;
                        dgvdetalle.Columns[1].Width = 240;
                        dgvdetalle.Columns[2].Width = 180;
                        dgvdetalle.Columns[3].Width = 160;
                        dgvdetalle.Columns[4].Width = 200;
                        dgvdetalle.Columns[5].Width = 140;
                        dgvdetalle.Columns[6].Width = 250;
                        dgvdetalle.Columns[7].Width = 80;
                        dgvdetalle.Columns[8].Width = 90;
                        dgvdetalle.Columns[9].Width = 90;
                        dgvdetalle.Columns[10].Width = 90;
                        dgvdetalle.Columns[11].Width = 90;
                        dgvdetalle.Columns[12].Width = 360;
                        dgvdetalle.Columns[13].Width = 360;
                        dgvdetalle.Columns[14].Width = 80;
                        dgvdetalle.Columns[15].Width = 70;
                        dgvdetalle.Columns[16].Width = 170;
                        dgvdetalle.Columns[17].Width = 220;
                        dgvdetalle.Columns[18].Visible = false;

                        dgvdetalle.AllowUserToResizeRows = false;
                        dgvdetalle.AllowUserToResizeColumns = false;
                        foreach (DataGridViewColumn column in dgvdetalle.Columns)
                        {
                            column.SortMode = DataGridViewColumnSortMode.NotSortable;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }

            conexion.Close();

            lblregistros.Text = "0 registros encontrados";
            if (dgvdetalle.Rows.Count == 1) { lblregistros.Text = dgvdetalle.Rows.Count + " registro encontrado"; }
            else { lblregistros.Text = dgvdetalle.Rows.Count + " registros encontrados"; }
        }

        #region _Click
        private void btnSalir_Click(object sender, EventArgs e) { Close(); }

        private void btnActualizar_Click(object sender, EventArgs e) { ui_Lista(); }
        
        private void btnExcel_Click(object sender, EventArgs e)
        {
            Exporta exporta = new Exporta();
            exporta.Excel_FromDataGridView(dgvdetalle);
        }

        private void btnGenerar_Click(object sender, EventArgs e)
        {
            loadingNext1.Visible = true;
            Application.DoEvents();

            ui_Lista();

            loadingNext1.Visible = false;
            Application.DoEvents();
        }
        #endregion

        private void dtpFecini_ValueChanged(object sender, EventArgs e)
        {
            dtpFecfin.MinDate = dtpFecini.Value;
            if (dtpFecini.Value > dtpFecfin.Value)
            {
                dtpFecfin.Value = dtpFecini.Value;
            }
            //ui_Lista();
        }

        private void cmbCia_SelectedIndexChanged(object sender, EventArgs e)
        {
            string cia = funciones.getValorComboBox(cmbCia, 2);
            //if (cia != "X")
            {
                string query = " select distinct a.clavemaesgen as clave, a.desmaesgen as descripcion from maesgen a (nolock) ";
                query += "left join gerenciasusr b (nolock) on b.idgerencia=a.clavemaesgen where a.idmaesgen='040' and a.parm1maesgen = '" + cia + "' ";
                funciones.listaComboBox(query, cmbGerencia, "X");
                cmbGerencia.Text = "X   TODOS";
            }
        }

        private void cmbGerencia_SelectedIndexChanged(object sender, EventArgs e)
        {
            string cia = funciones.getValorComboBox(cmbCia, 2);
            string geren = funciones.getValorComboBox(cmbGerencia, 8);
            //if (geren != "X")
            {
                string query = " select distinct a.clavemaesgen as clave, a.desmaesgen as descripcion from maesgen a (nolock) ";
                query += "left join cencosusr b (nolock) on b.idcencos=a.clavemaesgen where a.idmaesgen='008' and a.parm1maesgen = '" + geren + "' and a.parm2maesgen = '" + cia + "' ";
                funciones.listaComboBox(query, cmbCencos, "X");
                cmbCencos.Text = "X   TODOS";
            }
        }

        private void txtCodigoInterno_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                GlobalVariables gv = new GlobalVariables();
                Funciones fn = new Funciones();
                string idcia = fn.getValorComboBox(cmbCia, 2);
                string idgerencia = fn.getValorComboBox(cmbGerencia, 8);
                string idarea = fn.getValorComboBox(cmbCencos, 8);
                this._TextBoxActivo = txtCodigoInterno;
                string cadenaBusqueda = string.Empty;
                string condicionAdicional = string.Empty;

                if (idcia != "X") condicionAdicional += " and A.idcia='" + @idcia + "' ";
                if (idgerencia != "X   TODO") condicionAdicional += " and A.codaux='" + @idgerencia + "' ";
                if (idarea != "X   TODO") condicionAdicional += " and A.seccion='" + @idgerencia + "' ";

                FiltrosMaestros filtros = new FiltrosMaestros();
                filtros.filtrarPerPlan2("ui_rep_asistencia", this, txtCodigoInterno, null, cadenaBusqueda, condicionAdicional);
            }
        }

        private void txtCodigoInterno_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                dgvdetalle.DataSource = null;
                if (txtCodigoInterno.Text.Trim() != string.Empty)
                {
                    GlobalVariables gv = new GlobalVariables();
                    PerPlan perplan = new PerPlan();
                    string idcia = gv.getValorCia();
                    string idperplan = txtCodigoInterno.Text.Trim();
                    string codigoInterno = perplan.ui_getDatosPerPlan(idperplan, "0");

                    if (codigoInterno == string.Empty)
                    {
                        MessageBox.Show("Código no registrado del trabajador", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txtCodigoInterno.Clear();
                        txtDocIdent.Clear();
                        txtNroDocIden.Clear();
                        txtNombres.Clear();
                        e.Handled = true;
                        txtCodigoInterno.Focus();
                    }
                    else
                    {
                        txtCodigoInterno.Text = perplan.ui_getDatosPerPlan(idperplan, "0");
                        txtDocIdent.Text = perplan.ui_getDatosPerPlan(idperplan, "1");
                        txtNroDocIden.Text = perplan.ui_getDatosPerPlan(idperplan, "2");
                        txtNombres.Text = perplan.ui_getDatosPerPlan(idperplan, "3");
                        e.Handled = true;
                        toolstripform.Items[0].Select();
                        toolstripform.Focus();
                    }
                }
                else
                {
                    txtCodigoInterno.Clear();
                    txtDocIdent.Clear();
                    txtNroDocIden.Clear();
                    txtNombres.Clear();
                    e.Handled = true;
                    txtCodigoInterno.Focus();
                }
            }
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            ui_upd_asistencia ui_detalle = new ui_upd_asistencia();
            ui_detalle._FormPadre = this;
            ui_detalle.Activate();
            ui_detalle.BringToFront();
            ui_detalle.ShowDialog();
            ui_detalle.Dispose();
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            Int32 selectedCellCount = dgvdetalle.GetCellCount(DataGridViewElementStates.Selected);
            if (selectedCellCount > 0)
            {
                string id = dgvdetalle.Rows[dgvdetalle.SelectedCells[1].RowIndex].Cells[18].Value.ToString();
                string idperplan = dgvdetalle.Rows[dgvdetalle.SelectedCells[1].RowIndex].Cells[0].Value.ToString();
                string fecha = dgvdetalle.Rows[dgvdetalle.SelectedCells[1].RowIndex].Cells[7].Value.ToString();

                ui_upd_asistencia ui_detalle = new ui_upd_asistencia();
                ui_detalle._FormPadre = this;
                ui_detalle.Editar(id, idperplan, fecha);
                ui_detalle.Activate();
                ui_detalle.BringToFront();
                ui_detalle.ShowDialog();
                ui_detalle.Dispose();
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            Int32 selectedCellCount = dgvdetalle.GetCellCount(DataGridViewElementStates.Selected);
            if (selectedCellCount > 0)
            {
                string id = dgvdetalle.Rows[dgvdetalle.SelectedCells[1].RowIndex].Cells[18].Value.ToString();
                string idperplan = dgvdetalle.Rows[dgvdetalle.SelectedCells[1].RowIndex].Cells[0].Value.ToString();
                string fecha = dgvdetalle.Rows[dgvdetalle.SelectedCells[1].RowIndex].Cells[7].Value.ToString();
                string regentrad = dgvdetalle.Rows[dgvdetalle.SelectedCells[1].RowIndex].Cells[10].Value.ToString();
                string regsalid = dgvdetalle.Rows[dgvdetalle.SelectedCells[1].RowIndex].Cells[11].Value.ToString();

                string query = "SELECT a.nromov,a.fecha,LEFT(CONVERT(TIME,a.regEntrada,120),5),LEFT(CONVERT(TIME,a.regSalida,120),5),";
                query += "b.desusr,CONVERT(VARCHAR(16),a.regEntrada,120),CONVERT(VARCHAR(16),a.regSalida,120),a.idlogin+'  '+b.desusr ";
                query += "FROM control a (NOLOCK) INNER JOIN usrfile b (NOLOCK) ON b.idusr=a.idlogin ";
                query += "WHERE a.fecha = '" + fecha + "' AND a.idperplan = '" + idperplan + "' ORDER BY a.regEntrada,a.hora";
                SqlConnection conexion = new SqlConnection();
                conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
                conexion.Open();
                int contador = 0;

                try
                {
                    SqlCommand myCommand = new SqlCommand(query, conexion);
                    SqlDataReader odr = myCommand.ExecuteReader();

                    while (odr.Read())
                    {
                        contador++;
                    }

                    odr.Close();
                    myCommand.Dispose();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
                finally { conexion.Close(); }

                if (contador == 1)
                {
                    var confirmResult = MessageBox.Show("¿Esta seguro en Eliminar del registro seleccionado?", "Confirmación", MessageBoxButtons.YesNo);
                    if (confirmResult == DialogResult.Yes)
                    {
                        GlobalVariables variable = new GlobalVariables();
                        DateTime? entrada = null, salida = null;
                        entrada = DateTime.ParseExact(fecha + " " + ObtenerHora24(regentrad), "yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture);
                        if (regsalid != string.Empty)
                        {
                            salida = DateTime.ParseExact(fecha + " " + ObtenerHora24(regsalid), "yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture);
                        }

                        query = "DELETE FROM control WHERE nromov = '" + id + "';";
                        query += "INSERT INTO controlhisto VALUES ((SELECT ISNULL(MAX(id),0)+1 FROM controlhisto (NOLOCK)),'" + id + "','" +
                            idperplan + "','" + fecha + "','" + entrada.Value.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                            (salida != null ? "'" + salida.Value.ToString("yyyy-MM-dd HH:mm:ss") + "'" : "null") + ",'REGISTRO ELIMINADO','" + variable.getValorUsr() + "');";


                        conexion = new SqlConnection();
                        conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
                        conexion.Open();

                        try
                        {
                            SqlCommand myCommand = new SqlCommand(query, conexion);
                            myCommand.ExecuteNonQuery();
                            myCommand.Dispose();
                            MessageBox.Show("Registro Eliminado..", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            ui_Lista();
                        }
                        catch (SqlException ex)
                        {
                            MessageBox.Show(ex.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        }
                        conexion.Close();
                    }
                }
                else
                {
                    ui_upd_asistencia ui_detalle = new ui_upd_asistencia();
                    ui_detalle._FormPadre = this;
                    ui_detalle.Editar(id, idperplan, fecha);
                    ui_detalle.Activate();
                    ui_detalle.BringToFront();
                    ui_detalle.ShowDialog();
                    ui_detalle.Dispose();
                }
            }
        }

        private string ObtenerHora24(string hora)
        {
            string[] cad = hora.Split(':');
            string min = cad[1].Substring(0, 2);
            string format = cad[1].Substring(cad[1].Length - 2, 2);
            int hor = int.Parse(cad[0]);
            string resultado = string.Empty;
            if (format == "pm")
            {
                resultado = (hor + 12).ToString();
            }
            else { resultado = "0" + cad[0].Trim(); resultado = resultado.Substring(resultado.Length - 2, 2); }

            return resultado + ":" + min;
        }

        private void chkMasivo_CheckedChanged(object sender, EventArgs e)
        {
            cmbCia.Text = "X   TODOS";
            cmbCia.Enabled = false;
            cmbGerencia.Enabled = false;
            cmbCencos.Enabled = false;
            if (chkMasivo.Checked)
            {
                chkIndividual.Checked = false;
                cmbCia.Enabled = true;
                cmbGerencia.Enabled = true;
                cmbCencos.Enabled = true;
            }
        }

        private void chkIndividual_CheckedChanged(object sender, EventArgs e)
        {
            txtCodigoInterno.Enabled = false;
            txtCodigoInterno.Clear();
            txtDocIdent.Clear();
            txtNroDocIden.Clear();
            txtNombres.Clear();
            if (chkIndividual.Checked)
            {
                chkMasivo.Checked = false;
                txtCodigoInterno.Enabled = true;
                txtCodigoInterno.Focus();
            }
        }

        private void cmbSedes_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void cmbNominas_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void txtCodigoInterno_TextChanged(object sender, EventArgs e)
        {

        }
    }
}