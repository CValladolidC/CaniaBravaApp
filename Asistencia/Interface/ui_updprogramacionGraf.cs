using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;

namespace CaniaBrava
{
    public partial class ui_updprogramacionGraf : Form
    {
        Funciones funciones = new Funciones();
        GlobalVariables variables = new GlobalVariables();

        #region Propiedades
        private Form FormPadre;
        private List<ParametrosBE> ListaHorarios;
        private string Evento;
        private string Estado;
        #endregion

        #region Atributos
        List<programacion> ListaProg;
        List<ParametrosBE> ListaTrab;
        DataGridView ListaDataGrid = new DataGridView();
        DataTable dt;

        public Form _FormPadre
        {
            get { return FormPadre; }
            set { FormPadre = value; }
        }
        #endregion

        public ui_updprogramacionGraf()
        {
            InitializeComponent();

            if (funciones.VersionAssembly()) Application.ExitThread();
        }

        public void NuevoProg()
        {
            this.Evento = "NUEVO";
            this.Estado = "V";

            LoadAreasGerenciasEmpresas();
        }

        public void ActualizaComboBox(string idprog)
        {
            MaesGen maesgen = new MaesGen();
            if (idprog == "0")
            {
                DateTime FechaMaxima = GetMaxFechaProgramacion();

                if (FechaMaxima <= DateTime.Now.Date) { FechaMaxima = DateTime.Now.AddDays(-1).Date; }
                dtpFecini.MinDate = DateTime.Parse(FechaMaxima.AddDays(1).ToString("yyyy-MM-dd"));
                dtpFecfin.MinDate = DateTime.Parse(FechaMaxima.AddDays(1).ToString("yyyy-MM-dd"));

                txtProg.Text = "Programación " + dtpFecini.Value.ToString("yyyy-MM-dd") + " al " + dtpFecfin.Value.ToString("yyyy-MM-dd");
            }

            LoadDataTipoHorarios();

            string query = " select a.idcia as clave, a.descia as descripcion from ciafile a (nolock) ";
            if (variables.getValorTypeUsr() != "00")
            {
                query += "inner join ciausrfile b (nolock) on b.idcia=a.idcia and b.idusr='" + variables.getValorUsr() + "' ";
            }
            funciones.listaComboBox(query, cmbCia, "X");

            if (cmbCia.Items.Count == 2)
            {
                cmbCia.SelectedIndex = 1;
            }
        }

        private void LoadDataTrabajadores()
        {
            ListaTrab = new List<ParametrosBE>();
            string query = "SELECT a.idperplan, RTRIM(a.apepat)+' '+RTRIM(a.apemat)+', '+RTRIM(a.nombres) AS trabajador, ";
            query += "a.nrodoc, a.block FROM perplan a (NOLOCK) ";
            query += "WHERE a.block <> '' ";

            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();

            try
            {
                SqlCommand myCommand = new SqlCommand(query, conexion);
                SqlDataReader odr = myCommand.ExecuteReader();

                while (odr.Read())
                {
                    ListaTrab.Add(new ParametrosBE()
                    {
                        Param1 = odr.GetString(odr.GetOrdinal("idperplan")),
                        Param2 = odr.GetString(odr.GetOrdinal("trabajador")),
                        Param3 = odr.GetString(odr.GetOrdinal("nrodoc")),
                        Param4 = odr.GetString(odr.GetOrdinal("block"))
                    });
                }

                odr.Close();
                myCommand.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            finally { conexion.Close(); }
        }

        private void LoadDataTipoHorarios()
        {
            ListaHorarios = new List<ParametrosBE>();
            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();

            string query = @"SELECT RTRIM(idplantiphorario) [id],'VACACIONES' [dd],idcia,descripcion,'00:00' hor_entrada,'00:00' hor_salida FROM plantiphorario (NOLOCK) WHERE idcia=0 UNION 
SELECT RTRIM(b.idplantiphorario)+RTRIM(CAST(a.iddias_semana AS CHAR)),a.hor_entrada+' - '+a.hor_salida [dd],
b.idcia,b.descripcion,a.hor_entrada,a.hor_salida 
FROM plantiphorariodet a (NOLOCK) 
INNER JOIN plantiphorario b (NOLOCK) ON b.idplantiphorario = a.idplantiphorario ORDER BY idcia,dd ";

            try
            {
                using (SqlDataAdapter myDataAdapter = new SqlDataAdapter(query, conexion))
                {
                    DataSet myDataSet = new DataSet();
                    myDataAdapter.Fill(myDataSet);
                    funciones.formatearDataGridViewWhite2(dgvHorarios);

                    dgvHorarios.DataSource = myDataSet.Tables[0];
                    dgvHorarios.Columns[0].HeaderText = "Cod";
                    dgvHorarios.Columns[1].HeaderText = "Horario";

                    dgvHorarios.Columns[0].Width = 40;
                    dgvHorarios.Columns[1].Width = 100;

                    dgvHorarios.Columns[2].Visible = false;
                    dgvHorarios.Columns[3].Visible = false;
                    dgvHorarios.Columns[4].Visible = false;
                    dgvHorarios.Columns[5].Visible = false;

                    dgvHorarios.AllowUserToResizeRows = false;
                    dgvHorarios.AllowUserToResizeColumns = false;
                    foreach (DataGridViewColumn column in dgvHorarios.Columns)
                    {
                        column.SortMode = DataGridViewColumnSortMode.NotSortable;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }

            conexion.Close();

            for (int x = 0; x < dgvHorarios.Rows.Count; x++)
            {
                ListaHorarios.Add(new ParametrosBE()
                {
                    Param1 = dgvHorarios[0, x].Value.ToString(),
                    Param2 = dgvHorarios[1, x].Value.ToString(),
                    Param3 = dgvHorarios[2, x].Value.ToString(),
                    Param4 = dgvHorarios[3, x].Value.ToString(),
                    Param5 = dgvHorarios[4, x].Value.ToString(),
                    Param6 = dgvHorarios[5, x].Value.ToString()
                });
            }
        }

        public void EditarProg(string idprog, string idestado)
        {
            dgvdetalle.Enabled = true;
            dtpFecini.Enabled = false;
            dtpFecfin.Enabled = false;
            this.Estado = idestado;

            if (variables.getValorTypeUsr() == "00")
            {
                dtpFecfin.Enabled = true;
            }

            if ((idestado == "V" || idestado == "R") && (variables.getValorTypeUsr() == "02" || variables.getValorTypeUsr() == "03"))
            {
                dtpFecfin.Enabled = true;
            }

            ListaProg = new List<programacion>();
            ListaProg = ListaProgDet(idprog);

            if (ListaProg.Count > 0)
            {
                var cab = ListaProg.FirstOrDefault();
                if (cab != null)
                {
                    txtIdProg.Text = cab.idprog.ToString();
                    txtProg.Text = cab.desprog;
                    dtpFecini.Value = ListaProg.Min(x => x.fechaini);
                    dtpFecfin.Value = ListaProg.Max(x => x.fechafin);
                    dtpFecini.MinDate = cab.fechaini;
                    dtpFecfin.MinDate = cab.fechafin;
                    //chkAgrupar.Checked = (cab.homeoffice != string.Empty ? true : false);
                }
            }

            LoadAreasGerenciasEmpresas();
        }

        private void LoadAreasGerenciasEmpresas()
        {
            string query = string.Empty;
            string cia = funciones.getValorComboBox(cmbCia, 2);
            string ger = funciones.getValorComboBox(cmbGerencia, 8);
            string seccion = funciones.getValorComboBox(cmbCencos, 8);
            string cadenaseccion = string.Empty;
            if (seccion.Trim() != "X   TODO") { cadenaseccion = " WHERE a.seccion='" + @seccion + "' "; }
            else
            {
                cadenaseccion = " WHERE a.seccion IN (" + string.Join(",", cmbCencos.Items.Cast<String>().Select(x => "'" + x.Substring(0, 8) + "'").ToArray()) + ") ";
            }

            query = " SELECT DISTINCT a.idperplan AS Codigo, RTRIM(a.apepat)+' '+RTRIM(a.apemat)+', '+RTRIM(a.nombres) AS Trabajador, ";
            query += "a.nrodoc, a.seccion, a.codaux, a.idcia, a.block FROM perplan a (NOLOCK) ";

            if (variables.getValorTypeUsr() != "00") { query += "INNER JOIN cencosusr b (NOLOCK) ON b.idcencos = a.seccion "; }
            else { query += "LEFT JOIN cencosusr b (NOLOCK) ON b.idcencos = a.seccion "; }

            if (variables.getValorTypeUsr() != "00") { query += "AND b.idusr='" + variables.getValorUsr() + "' "; }

            query += "INNER JOIN tipoper c (NOLOCK) ON c.idtipoper=a.idtipoper AND c.destipoper NOT LIKE '%EMPLEADO%' ";
            query += @cadenaseccion + " AND a.idcia = '" + @cia + "' ";

            if (ger != "X   TODO") { query += "AND a.codaux = '" + @ger + "' "; }

            query += " ORDER BY 2 ASC;";

            loadqueryDatos(query);
        }

        private void loadqueryDatos(string query)
        {
            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();

            try
            {
                using (SqlDataAdapter myDataAdapter = new SqlDataAdapter(query, conexion))
                {
                    DataTable dtper = new DataTable();
                    myDataAdapter.Fill(dtper);

                    dt = GeneraGrid(dtper);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }

            conexion.Close();
        }

        private DataTable GeneraGrid(DataTable dtper)
        {
            UtileriasFechas utilfechas = new UtileriasFechas();
            DataTable dt = new DataTable();
            dt.Columns.Add("Codigo", typeof(string));
            dt.Columns.Add("Trabajador", typeof(string));
            dt.Columns.Add("nrodoc", typeof(string));
            dt.Columns.Add("seccion", typeof(string));
            dt.Columns.Add("codaux", typeof(string));
            dt.Columns.Add("idcia", typeof(string));
            dt.Columns.Add("block", typeof(string));
            int dias = (dtpFecfin.Value.Date - dtpFecini.Value.Date).Days;
            for (int i = 0; i <= dias; i++)
            {
                dt.Columns.Add(utilfechas.dayOfWeek(utilfechas.incrementarFecha(dtpFecini.Value.ToString("dd-MM-yyyy"), i)) + "  " +
                    utilfechas.incrementarFecha(dtpFecini.Value.ToString("dd-MM-yyyy"), i).Substring(0, 2), typeof(string));
            }

            dgvdetalle.RowHeadersVisible = false;
            dgvdetalle.AllowUserToAddRows = false;
            dgvdetalle.MultiSelect = false;

            if (dtper.Rows.Count > 0)
            {
                foreach (DataRow row_per in dtper.Rows)
                {
                    DataRow dr;
                    dr = dt.NewRow();
                    dr[0] = row_per["Codigo"].ToString();
                    dr[1] = row_per["Trabajador"].ToString();
                    dr[2] = row_per["nrodoc"].ToString();
                    dr[3] = row_per["seccion"].ToString();
                    dr[4] = row_per["codaux"].ToString();
                    dr[5] = row_per["idcia"].ToString();
                    dr[6] = row_per["block"].ToString();
                    for (int i = 0; i <= dias; i++)
                    {
                        if (ListaProg != null)
                        {
                            var cod = ListaProg.Find(x => x.fechaini.ToString("yyyy-MM-dd") == dtpFecini.Value.AddDays(i).ToString("yyyy-MM-dd")
                            && x.idperplan == row_per["Codigo"].ToString());
                            dr[i + 7] = (cod != null ? cod.idsap.ToString() : string.Empty);
                        }
                        else { dr[i + 7] = string.Empty; }
                    }
                    dt.Rows.Add(dr);
                }
            }

            dgvdetalle.DataSource = null;
            dgvdetalle.DataSource = dt;

            DataGridViewCellStyle style = new DataGridViewCellStyle();
            dgvdetalle.Columns[1].Width = 270;
            for (int i = 0; i <= dias; i++)
            {
                style.BackColor = Color.LightGray;
                dgvdetalle.Columns[i + 7].DefaultCellStyle = style;

                dgvdetalle.Columns[i + 7].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgvdetalle.Columns[i + 7].Width = 30;
                ((DataGridViewTextBoxColumn)dgvdetalle.Columns[i + 7]).MaxInputLength = 3;
            }

            dgvdetalle.Columns[0].Visible = false;
            dgvdetalle.Columns[2].Visible = false;
            dgvdetalle.Columns[3].Visible = false;
            dgvdetalle.Columns[4].Visible = false;
            dgvdetalle.Columns[5].Visible = false;
            dgvdetalle.Columns[6].Visible = false;

            dgvdetalle.Columns[1].ReadOnly = true;
            dgvdetalle.Columns[0].Frozen = true;
            dgvdetalle.Columns[1].Frozen = true;

            dgvdetalle.AllowUserToResizeRows = false;
            dgvdetalle.AllowUserToResizeColumns = false;
            foreach (DataGridViewColumn column in dgvdetalle.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
        
            return dt;
        }

        private void loadqueryDatosChk(DataTable data)
        {
            string cbgrupo = funciones.getValorComboBox(cmbGrupos, 1);
            try
            {
                var tempo = (from row in data.AsEnumerable()
                             where row.Field<string>("block").Trim() != string.Empty
                             where row.Field<string>("block").Trim() == (cbgrupo != "X" ? cbgrupo : row.Field<string>("block").Trim())
                             group row by new
                             {
                                 grupo = row.Field<string>("block"),
                                 seccion = row.Field<string>("seccion"),
                                 codaux = row.Field<string>("codaux"),
                                 idcia = row.Field<string>("idcia")
                             } into grp
                             select new
                             {
                                 grp.Key.grupo,
                                 grp.Key.seccion,
                                 grp.Key.codaux,
                                 grp.Key.idcia
                                 //Sum = grp.Sum(r => r.Field<Decimal>("actual_hrs"))
                             }).ToList();

                UtileriasFechas utilfechas = new UtileriasFechas();
                var dt = new DataTable();
                dt.Columns.Add("Grupo", typeof(string));
                dt.Columns.Add("seccion", typeof(string));
                dt.Columns.Add("codaux", typeof(string));
                dt.Columns.Add("idcia", typeof(string));
                int dias = (dtpFecfin.Value - dtpFecini.Value).Days + 1;
                for (int i = 0; i < dias; i++)
                {
                    dt.Columns.Add(utilfechas.dayOfWeek(utilfechas.incrementarFecha(dtpFecini.Value.ToString("dd-MM-yyyy"), i)) + "  " +
                        utilfechas.incrementarFecha(dtpFecini.Value.ToString("dd-MM-yyyy"), i).Substring(0, 2), typeof(string));
                }

                //dgvdetalle.Columns[0].ReadOnly = false;
                //dgvdetalle.Columns[0].Frozen = false;

                dgvdetalle.RowHeadersVisible = false;
                dgvdetalle.AllowUserToAddRows = false;
                dgvdetalle.MultiSelect = false;

                if (tempo.Count > 0)
                {
                    foreach (var row_per in tempo)
                    {
                        DataRow dr;
                        dr = dt.NewRow();
                        dr[0] = row_per.grupo;
                        dr[1] = row_per.seccion;
                        dr[2] = row_per.codaux;
                        dr[3] = row_per.idcia;
                        for (int i = 0; i < dias; i++)
                        {
                            if (ListaProg != null)
                            {
                                var cod = ListaProg.Find(x => x.fechaini.ToString("yyyy-MM-dd") == dtpFecini.Value.AddDays(i).ToString("yyyy-MM-dd")
                                && x.homeoffice == row_per.grupo.Trim());
                                dr[i + 4] = (cod != null ? cod.idsap.ToString() : string.Empty);
                            }
                            else { dr[i + 4] = string.Empty; }
                        }
                        dt.Rows.Add(dr);
                    }
                }

                dgvdetalle.DataSource = null;
                dgvdetalle.DataSource = dt;

                DataGridViewCellStyle style = new DataGridViewCellStyle();
                dgvdetalle.Columns[0].Width = 270;
                for (int i = 0; i < dias; i++)
                {
                    style.BackColor = Color.LightGray;
                    dgvdetalle.Columns[i + 4].DefaultCellStyle = style;

                    dgvdetalle.Columns[i + 4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    dgvdetalle.Columns[i + 4].Width = 30;
                    ((DataGridViewTextBoxColumn)dgvdetalle.Columns[i + 4]).MaxInputLength = 3;
                }

                dgvdetalle.Columns[1].Visible = false;
                dgvdetalle.Columns[2].Visible = false;
                dgvdetalle.Columns[3].Visible = false;

                dgvdetalle.Columns[0].ReadOnly = true;
                dgvdetalle.Columns[0].Frozen = true;

                dgvdetalle.AllowUserToResizeRows = false;
                dgvdetalle.AllowUserToResizeColumns = false;
                foreach (DataGridViewColumn column in dgvdetalle.Columns)
                {
                    column.SortMode = DataGridViewColumnSortMode.NotSortable;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private DateTime GetMaxFechaProgramacion()
        {
            DateTime fecha = new DateTime();

            string query = "SELECT ISNULL(MAX(fechafin),CONVERT(VARCHAR(10), GETDATE(), 120)) as fecha FROM prog ";
            if (variables.getValorTypeUsr() != "00")
            {
                query += @"WHERE idusrins IN (SELECT DISTINCT idusr FROM cencosusr (NOLOCK) WHERE idcencos in (
                        SELECT idcencos FROM cencosusr (NOLOCK) WHERE idusr = '" + variables.getValorUsr() + "')) AND idusrins = '" + variables.getValorUsr() + "'";
            }

            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();

            try
            {
                SqlCommand myCommand = new SqlCommand(query, conexion);
                SqlDataReader odr = myCommand.ExecuteReader();

                while (odr.Read())
                {
                    fecha = odr.GetDateTime(odr.GetOrdinal("fecha"));
                }

                odr.Close();
                myCommand.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            finally
            {
                conexion.Close();
            }

            return fecha;
        }

        private List<programacion> ListaProgDet(string id)
        {
            List<programacion> lista = new List<programacion>();

            string seccion = funciones.getValorComboBox(cmbCencos, 4);
            string cadenaseccion = string.Empty;
            if (seccion != "X") cadenaseccion = " where seccion='" + @seccion + "' ";

            string query = "SELECT a.idprog,a.desprog,b.fechaini,b.fechafin, b.idperplan,b.destrabajador,b.destipohorario, ";
            query += "b.seccion,b.nrodoc,b.idcia,b.gerencia,b.idsap,b.homeoffice FROM prog a (NOLOCK) INNER JOIN progdet b (NOLOCK) on b.idprog=a.idprog ";
            query += "WHERE a.idprog='" + id + "'";

            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();

            try
            {
                SqlCommand myCommand = new SqlCommand(query, conexion);
                SqlDataReader odr = myCommand.ExecuteReader();

                programacion obj = null;
                while (odr.Read())
                {
                    obj = new programacion();
                    obj.idprog = odr.GetInt32(odr.GetOrdinal("idprog"));
                    obj.desprog = odr.GetString(odr.GetOrdinal("desprog"));
                    obj.fechaini = odr.GetDateTime(odr.GetOrdinal("fechaini"));
                    obj.fechafin = odr.GetDateTime(odr.GetOrdinal("fechafin"));
                    obj.idperplan = odr.GetString(odr.GetOrdinal("idperplan"));
                    obj.destrabajador = odr.GetString(odr.GetOrdinal("destrabajador"));
                    obj.destipohorario = odr.GetString(odr.GetOrdinal("destipohorario"));
                    obj.seccion = odr.GetString(odr.GetOrdinal("seccion"));
                    obj.nrodoc = odr.GetString(odr.GetOrdinal("nrodoc"));
                    obj.idcia = odr.GetString(odr.GetOrdinal("idcia"));
                    obj.gerencia = odr.GetString(odr.GetOrdinal("gerencia"));
                    obj.idsap = odr.GetString(odr.GetOrdinal("idsap"));
                    obj.homeoffice = odr.GetString(odr.GetOrdinal("homeoffice"));
                    lista.Add(obj);
                }

                odr.Close();
                myCommand.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            finally
            {
                conexion.Close();
            }

            return lista;
        }

        #region Metodos SelectedIndexChanged
        private void cmbCia_SelectedIndexChanged(object sender, EventArgs e)
        {
            dgvdetalle.DataSource = null;
            string cia = funciones.getValorComboBox(cmbCia, 2);
            if (cia != "X")
            {
                string query = " select distinct a.clavemaesgen as clave, a.desmaesgen as descripcion from maesgen a (nolock) ";
                query += "left join gerenciasusr b (nolock) on b.idgerencia=a.clavemaesgen where a.idmaesgen='040' and a.parm1maesgen = '" + cia + "' ";
                if (variables.getValorTypeUsr() != "00")
                {
                    query += "and b.idcia = '" + cia + "' and b.idusr='" + variables.getValorUsr() + "' ";
                }
                funciones.listaComboBox(query, cmbGerencia, "X");

                if (cmbGerencia.Items.Count == 2)
                {
                    cmbGerencia.SelectedIndex = 1;
                }
                else { cmbGerencia.Text = "X   TODOS"; }
            }
            //else { cmbCencos_SelectedIndexChanged(sender, e); }
            else
            {
                cmbGerencia.Items.Clear();
                cmbGerencia.Items.Add("X   TODOS");
            }
        }

        private string GetNominas(string cia, ref string likeNominas)
        {
            string query = "select distinct idtipoper,idcia from labper where idcia = '" + cia + "'";

            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();
            string resultado = string.Empty;

            try
            {
                SqlCommand myCommand = new SqlCommand(query, conexion);
                SqlDataReader odr = myCommand.ExecuteReader();
                while (odr.Read())
                {
                    resultado += odr.GetString(odr.GetOrdinal("idtipoper")) + "-";
                    likeNominas += "nominas LIKE '%" + odr.GetString(odr.GetOrdinal("idtipoper")) + "%' OR ";
                }

                likeNominas = likeNominas.Substring(0, likeNominas.Length - 3);

                odr.Close();
                myCommand.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            finally { conexion.Close(); }

            return resultado.Substring(0, resultado.Length - 1);
        }

        private void cmbGerencia_SelectedIndexChanged(object sender, EventArgs e)
        {
            dgvdetalle.DataSource = null;
            string cia = funciones.getValorComboBox(cmbCia, 2);
            string ger = funciones.getValorComboBox(cmbGerencia, 8);
            if (ger != "X   TODO")
            {
                string query = " select distinct a.clavemaesgen as clave, a.desmaesgen as descripcion from maesgen a (nolock) ";
                query += "left join cencosusr b (nolock) on b.idcencos=a.clavemaesgen where a.idmaesgen='008' ";
                query += "and a.parm1maesgen = '" + ger + "' ";

                query += "and a.parm2maesgen = '" + cia + "' ";
                if (variables.getValorTypeUsr() != "00")
                {
                    query += "and b.idcia = '" + cia + "' and b.idusr='" + variables.getValorUsr() + "' ";
                }
                funciones.listaComboBox(query, cmbCencos, "X");

                if (cmbCencos.Items.Count == 2)
                {
                    cmbCencos.SelectedIndex = 1;
                }
                else { cmbCencos.Text = "X   TODOS"; }
            }
            else
            {
                cmbCencos.Items.Clear();
                cmbCencos.Items.Add("X   TODOS");
            }
        }

        private void cmbCencos_SelectedIndexChanged(object sender, EventArgs e)
        {
            dgvdetalle.DataSource = null;
            string cia = funciones.getValorComboBox(cmbCia, 2);
            string ger = funciones.getValorComboBox(cmbGerencia, 8);
            string cen = funciones.getValorComboBox(cmbCencos, 8);
            if (cen != "X   TODO")
            {
                string query = string.Empty;
                if (cen == "50234202")
                {
                    query = "SELECT DISTINCT block [clave],";
                    query += "(CASE block WHEN 'A' THEN 'Fundo LB/HC' ELSE 'Fundo ML/SV' END) ";
                    query += "[descripcion] FROM perplan (NOLOCK) WHERE block <> ''";
                    query += "AND idcia = '" + cia + "' AND codaux = '" + ger + "' ";
                    query += "AND seccion = '" + @cen + "'";
                    funciones.listaComboBox(query, cmbGrupos, "X");
                }
                else
                {
                    query = "SELECT DISTINCT block [descripcion] FROM perplan (NOLOCK) WHERE block <> ''";
                    query += "AND idcia = '" + cia + "' AND codaux = '" + ger + "' ";
                    query += "AND seccion = '" + @cen + "'";
                    funciones.listaComboBoxUnCampo(query, cmbGrupos, "X");
                }

                LoadAreasGerenciasEmpresas();
            }
            else
            {
                cmbGrupos.Items.Clear();
                cmbGrupos.Items.Add("X   TODOS");
            }
            cmbGrupos.Text = "X   TODOS";
        }

        private void cmbGrupos_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable data = new DataTable();
            List<DataRow> lista = new List<DataRow>();

            bool checkgrupos = chkAgrupar.Checked;
            string grupo = funciones.getValorComboBox(cmbGrupos, 1);
            dgvdetalle.DataSource = null;
            string cen = funciones.getValorComboBox(cmbCencos, 8);
            if (cen != "X   TODO")
            {
                if (dt != null)
                {
                    data = dt.Copy();

                    if (checkgrupos)
                    {
                        loadqueryDatosChk(data);
                    }
                    else
                    {
                        if (grupo != "X")
                        {
                            lista = (from row in data.AsEnumerable()
                                     where row.Field<string>("block").Trim() == grupo
                                     select row).ToList();

                            if (lista.Count > 0)
                            {
                                //dgvdetalle.DataSource = lista.CopyToDataTable();
                                GeneraGrid(lista.CopyToDataTable());
                            }
                            else { dgvdetalle.DataSource = null; }
                        }
                        else { GeneraGrid(data); }
                    }
                }
            }
        }
        #endregion

        #region Metodos Click

        private void btnExcel_Click(object sender, EventArgs e)
        {
            if (dgvdetalle.Rows.Count > 0)
            {
                var data = ((DataTable)dgvdetalle.DataSource).Copy();
                data.Columns.Remove("Codigo");
                data.Columns.Remove("nrodoc");
                data.Columns.Remove("seccion");
                data.Columns.Remove("codaux");
                data.Columns.Remove("idcia");
                data.Columns.Remove("block");
                dgvdetalleExcel.DataSource = data;

                Exporta exporta = new Exporta();
                exporta.Excel_FromDataGridView(dgvdetalleExcel, true);
            }
            else
            {
                MessageBox.Show("No existe ningun registro a exportar.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnGrabar_Click(object sender, EventArgs e)
        {
            List<programacion> listdet = new List<programacion>();
            bool checkgrupos = chkAgrupar.Checked;

            programacion be = new programacion()
            {
                desprog = txtProg.Text.ToUpper(),
                fechaini = dtpFecini.Value,
                fechafin = dtpFecfin.Value,
                estado = this.Estado,
                idusrins = variables.getValorUsr(),
                idusrupd = variables.getValorUsr()
            };

            programacion be_det = null;
            int dias = (dtpFecfin.Value.Date - dtpFecini.Value.Date).Days;
            foreach (DataGridViewRow item in dgvdetalle.Rows)
            {
                for (int i = 0; i <= dias; i++)
                {
                    if (checkgrupos)
                    {
                        var hor = ListaHorarios.Find(x => x.Param1 == item.Cells[i + 4].Value.ToString());
                        if (hor != null)
                        {
                            var persons = ListaTrab.Where(x => x.Param4 == item.Cells[0].Value.ToString()).ToList();
                            foreach (var per in persons)
                            {
                                be_det = new programacion()
                                {
                                    idperplan = per.Param1,
                                    destrabajador = per.Param2,
                                    nrodoc = per.Param3,
                                    seccion = item.Cells[1].Value.ToString(),
                                    gerencia = item.Cells[2].Value.ToString(),
                                    idcia = item.Cells[3].Value.ToString(),
                                    fechadiaria = dtpFecini.Value.AddDays(i),
                                    idsap = item.Cells[i + 4].Value.ToString(),
                                    fechaini = DateTime.Parse(dtpFecini.Value.AddDays(i).ToString("yyyy-MM-dd") + " " + hor.Param5),
                                    fechafin = DateTime.Parse(dtpFecini.Value.AddDays(i).ToString("yyyy-MM-dd") + " " + hor.Param6),
                                    destipohorario = hor.Param1 + " " + hor.Param4 + " [" + hor.Param2 + "]",
                                    estado = this.Estado,
                                    homeoffice = item.Cells[0].Value.ToString()
                                };
                                listdet.Add(be_det);
                            }
                        }
                    }
                    else
                    {
                        var hor = ListaHorarios.Find(x => x.Param1 == item.Cells[i + 7].Value.ToString());
                        if (hor != null)
                        {
                            be_det = new programacion()
                            {
                                idperplan = item.Cells[0].Value.ToString(),
                                destrabajador = item.Cells[1].Value.ToString(),
                                nrodoc = item.Cells[2].Value.ToString(),
                                seccion = item.Cells[3].Value.ToString(),
                                gerencia = item.Cells[4].Value.ToString(),
                                idcia = item.Cells[5].Value.ToString(),
                                fechadiaria = dtpFecini.Value.AddDays(i),
                                idsap = item.Cells[i + 7].Value.ToString(),
                                fechaini = DateTime.Parse(dtpFecini.Value.AddDays(i).ToString("yyyy-MM-dd") + " " + hor.Param5),
                                fechafin = DateTime.Parse(dtpFecini.Value.AddDays(i).ToString("yyyy-MM-dd") + " " + hor.Param6),
                                destipohorario = hor.Param1 + " " + hor.Param4 + " [" + hor.Param2 + "]",
                                estado = this.Estado
                            };
                            listdet.Add(be_det);
                        }
                    }
                }
            }

            if (listdet.Count > 0)
            {
                if (this.Evento != "NUEVO")
                {
                    be.idprog = int.Parse(txtIdProg.Text);
                    be.UpdateProg(be, true, listdet);
                }
                else { be.UpdateProg(be, false, listdet); }

                MessageBox.Show("Programacion exitosa..!!", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);

                ((ui_programacionGraf)FormPadre).btnActualizar.PerformClick();
                //this.Close();
            }
            else
            {
                MessageBox.Show("No ha programado ningun horario..!!", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        #endregion

        private void dtpFecini_ValueChanged(object sender, EventArgs e)
        {
            dtpFecfin.MinDate = dtpFecini.Value;
            txtProg.Text = "Programación " + dtpFecini.Value.ToString("yyyy-MM-dd") + " al " + dtpFecfin.Value.ToString("yyyy-MM-dd");
        }

        private void dtpFecfin_ValueChanged(object sender, EventArgs e)
        {
            txtProg.Text = "Programación " + dtpFecini.Value.ToString("yyyy-MM-dd") + " al " + dtpFecfin.Value.ToString("yyyy-MM-dd");
            //LoadAreasGerenciasEmpresas();
            LoadTempoGrid();

            if (dtpFecfin.Value != dtpFecini.Value) { dgvdetalle.Enabled = true; }
        }

        #region Eventos dgvHorarios_
        private void dgvHorarios_SelectionChanged(object sender, EventArgs e)
        {
            dgvHorarios.ClearSelection();
        }

        private void dgvHorarios_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            switch (dgvHorarios[2, e.RowIndex].Value.ToString())
            {
                case "0":
                    dgvHorarios.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.SteelBlue;
                    dgvHorarios.Rows[e.RowIndex].DefaultCellStyle.ForeColor = Color.White;
                    break;
                case "1":
                    dgvHorarios.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.Green;
                    dgvHorarios.Rows[e.RowIndex].DefaultCellStyle.ForeColor = Color.White;
                    break;
                case "2":
                    dgvHorarios.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.Orange;
                    dgvHorarios.Rows[e.RowIndex].DefaultCellStyle.ForeColor = Color.White;
                    break;
                case "3":
                    dgvHorarios.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.Red;
                    dgvHorarios.Rows[e.RowIndex].DefaultCellStyle.ForeColor = Color.White;
                    break;
            }
        }
        #endregion

        #region Eventos dgvdetalle_
        private void dgvdetalle_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (dgvdetalle.RowCount > 0)
                {
                    dgvdetalle.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = dgvdetalle.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString().ToUpper();
                    string valor = dgvdetalle.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();

                    if (valor.Trim() != string.Empty)
                    {
                        bool valida = true;
                        string color = string.Empty;
                        foreach (var item in ListaHorarios)
                        {
                            if (item.Param1 == valor) { valida = false; color = item.Param3; }
                        }

                        if (valida)
                        {
                            dgvdetalle.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = string.Empty;
                            MessageBox.Show("Codigo incorrecto", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        else
                        {
                            DataGridViewCellStyle style = new DataGridViewCellStyle();
                            switch (color)
                            {
                                case "0":
                                    style.BackColor = Color.SteelBlue;
                                    style.ForeColor = Color.White; break;
                                case "1":
                                    style.BackColor = Color.Green;
                                    style.ForeColor = Color.White; break;
                                case "2":
                                    style.BackColor = Color.Orange;
                                    style.ForeColor = Color.White; break;
                                case "3":
                                    style.BackColor = Color.Red;
                                    style.ForeColor = Color.White; break;
                            }
                            dgvdetalle.Rows[e.RowIndex].Cells[e.ColumnIndex].Style = style;
                        }
                    }
                }
            }
            catch (FormatException ex)
            {
                MessageBox.Show(ex.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvdetalle_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            int column = 6;
            bool checkgrupos = chkAgrupar.Checked;
            if (checkgrupos) { column = 3; }

            if (e.ColumnIndex > column)
            {
                DataGridViewCellStyle style = new DataGridViewCellStyle();
                var dato = ListaHorarios.Find(x => x.Param1 == dgvdetalle[e.ColumnIndex, e.RowIndex].Value.ToString());
                if (dato != null)
                {
                    switch (dato.Param3)
                    {
                        case "0":
                            style.BackColor = Color.SteelBlue;
                            style.ForeColor = Color.White; break;
                        case "1":
                            style.BackColor = Color.Green;
                            style.ForeColor = Color.White; break;
                        case "2":
                            style.BackColor = Color.Orange;
                            style.ForeColor = Color.White; break;
                        case "3":
                            style.BackColor = Color.Red;
                            style.ForeColor = Color.White; break;
                    }
                }
                else
                {
                    style.BackColor = Color.LightGray;
                }
                dgvdetalle[e.ColumnIndex, e.RowIndex].Style = style;
                dgvdetalle.Rows[e.RowIndex].Cells[e.ColumnIndex].Style = style;
            }
        }
        #endregion

        private void chkAgrupar_CheckedChanged(object sender, EventArgs e)
        {
            LoadTempoGrid();
        }

        private void LoadTempoGrid()
        {
            if (chkAgrupar.Checked)
            {
                if (cmbGrupos.Items.Count > 0)
                {
                    var data = dt.Copy();
                    loadqueryDatosChk(data);
                }
            }
            else
            {
                if (cmbGrupos.Items.Count > 0)
                {
                    cmbGrupos.SelectedIndex = 0;
                    if (dt != null)
                    {
                        GeneraGrid(dt.Copy());
                    }
                }
            }
        }
    }
}