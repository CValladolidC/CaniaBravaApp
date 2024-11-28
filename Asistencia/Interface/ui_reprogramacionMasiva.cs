using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;

namespace CaniaBrava
{
    public partial class ui_reprogramacionMasiva : Form
    {
        Funciones funciones = new Funciones();
        GlobalVariables variables = new GlobalVariables();

        #region Propiedades
        private Form FormPadre;
        private string Evento;
        private DateTime Fecha01;
        private DateTime Fecha02;
        #endregion

        #region Atributos
        List<ParametrosBE> Lista01;
        List<ParametrosBE> Lista02 = new List<ParametrosBE>();
        DataSet ListaDataSet;
        ParametrosBE obj01;

        public Form _FormPadre
        {
            get { return FormPadre; }
            set { FormPadre = value; }
        }
        #endregion

        public ui_reprogramacionMasiva()
        {
            InitializeComponent();

            if (funciones.VersionAssembly()) Application.ExitThread();
        }

        private void ui_reprogramacionMasiva_Load(object sender, EventArgs e)
        {
            ActualizaComboBox("1");
            string seccion = string.Empty;
            if (cmbCencos.Items.Count == 2) { seccion = cmbCencos.Items[1].ToString().Substring(0, 8); }

            btnGuardar.Enabled = false;
            btnAdd.Enabled = false;
            btnAddAll.Enabled = false;
            btnDel.Enabled = false;
            btnDelAll.Enabled = false;
            dtpFecini.Enabled = false;
            dtpFecfin.Enabled = false;
            if (seccion != string.Empty)
            {
                LoadReProgramaciones(seccion);

                btnAdd.Enabled = true;
                btnAddAll.Enabled = true;
                btnDel.Enabled = true;
                btnDelAll.Enabled = true;
                btnGuardar.Enabled = true;
                dtpFecini.Enabled = true;
                dtpFecfin.Enabled = true;
            }
        }

        private void LoadReProgramaciones(string seccion)
        {
            string query = "SELECT DISTINCT TOP 1 CONVERT(VARCHAR(10),fechadiaria,120) AS fechadiaria,idprog FROM progdet (NOLOCK) ";
            query += "WHERE fechadiaria >= CONVERT(VARCHAR(10),GETDATE(),120) AND idprog IN ";
            query += "(SELECT DISTINCT idprog FROM progdet (NOLOCK) WHERE fechadiaria >= CONVERT(VARCHAR(10),GETDATE(),120) AND seccion = '" + @seccion + "') ";
            query += "ORDER BY fechadiaria;";
            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();

            string idprog = string.Empty;
            string fechaini = string.Empty;
            try
            {
                SqlCommand myCommand = new SqlCommand(query, conexion);
                SqlDataReader odr = myCommand.ExecuteReader();

                while (odr.Read())
                {
                    idprog = odr.GetInt32(odr.GetOrdinal("idprog")).ToString();
                    fechaini = odr.GetString(odr.GetOrdinal("fechadiaria"));
                }

                odr.Close();
                myCommand.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            finally { conexion.Close(); }

            if (idprog != string.Empty && fechaini != string.Empty)
            {
                EditarProg(idprog, fechaini);
            }
        }

        public void NuevoProg()
        {
            this.Evento = "NUEVO";
            var listaEmpleados = ListaPerPlan();
            
            LoadTrabajadoresIni(listaEmpleados);
            //lblTotalEmpleados.Text = Lista01.Count.ToString() + " registros";
            LoadAreasGerenciasEmpresas();
        }

        private void LoadTrabajadoresIni(List<programacion> list)
        {
            Lista01 = new List<ParametrosBE>();
            Lista01.AddRange(list.Select(x => new ParametrosBE()
            {
                Param1 = x.idperplan,
                Param2 = x.destrabajador,
                Param3 = x.seccion,
                Param4 = x.nrodoc,
                Param5 = x.idcia,
                Param6 = x.gerencia
            }));
            listEmpleados.DataSource = Lista01;
            listEmpleados.DisplayMember = "Param2";
            listEmpleados.ValueMember = "Param1";
        }

        public void ActualizaComboBox(string idprog)
        {
            MaesGen maesgen = new MaesGen();
            if (idprog == "0")
            {
                DateTime FechaMaxima = GetMaxFechaProgramacion();

                if (FechaMaxima <= DateTime.Now.Date) { FechaMaxima = DateTime.Now.Date; }
                dtpFecini.MinDate = DateTime.Parse(FechaMaxima.AddDays(1).ToString("yyyy-MM-dd"));
                dtpFecfin.MinDate = DateTime.Parse(FechaMaxima.AddDays(1).ToString("yyyy-MM-dd"));
            }

            string query = "SELECT idplantiphorario as clave, descripcion FROM plantiphorario (NOLOCK) order by idplantiphorario";
            funciones.listaComboBox(query, cmbTipoHorarios, "");

            LoadCmbTipoHorarios();

            query = " select a.idcia as clave, a.descia as descripcion from ciafile a (nolock) ";
            if (variables.getValorTypeUsr() != "00")
            {
                query += "inner join ciausrfile b (nolock) on b.idcia=a.idcia and b.idusr='" + variables.getValorUsr() + "' ";
            }
            funciones.listaComboBox(query, cmbCia, "");

            string cia = funciones.getValorComboBox(cmbCia, 2);
            query = " select distinct a.clavemaesgen as clave, a.desmaesgen as descripcion from maesgen a (nolock) ";
            query += "left join gerenciasusr b (nolock) on b.idgerencia=a.clavemaesgen where a.idmaesgen='040' and a.parm1maesgen = '" + cia + "' ";
            if (variables.getValorTypeUsr() != "00")
            {
                query += "and b.idcia = '" + cia + "' and b.idusr='" + variables.getValorUsr() + "' ";
            }
            funciones.listaComboBox(query, cmbGerencia, "X");
            cmbGerencia.Text = "X   TODOS";

            query = " select distinct a.clavemaesgen as clave, a.desmaesgen as descripcion from maesgen a (nolock) ";
            query += "left join cencosusr b (nolock) on b.idcencos=a.clavemaesgen where a.idmaesgen='008' ";
            if (variables.getValorTypeUsr() != "00") { query += "and b.idusr='" + variables.getValorUsr() + "' "; }
            funciones.listaComboBox(query, cmbCencos, "X");
            cmbCencos.Text = "X   TODOS";

            listTipoHorarios.DataSource = null;
        }

        public void EditarProg(string idprog, string fechaini)
        {
            var listaEmpleados = ListaPerPlan();
            var lista = ListaProgDet(idprog).Where(x => x.fechaini >= DateTime.Parse(fechaini)).ToList();

            this.Fecha01 = lista.Max(x => x.fechaini);
            this.Fecha02 = lista.Max(x => x.fechafin);

            foreach (var del in lista)
            {
                var d = listaEmpleados.Where(x => x.idperplan == del.idperplan).FirstOrDefault();
                listaEmpleados.Remove(d);
            }

            Lista01 = new List<ParametrosBE>();
            if (listaEmpleados.Count > 0) { LoadTrabajadoresIni(listaEmpleados); }

            if (lista.Count > 0)
            {
                var cab = lista.FirstOrDefault();
                if (cab != null)
                {
                    txtIdProg.Text = cab.idprog.ToString();
                    dtpFecini.Value = cab.fechaini;
                    dtpFecfin.Value = cab.fechafin;
                    dtpFecini.MinDate = cab.fechaini;
                    dtpFecfin.MinDate = cab.fechafin;
                }

                var groupTipH = lista.GroupBy(x => x.destipohorario).ToList();
                foreach (var item in groupTipH)
                {
                    var tabla = ListaDataSet.Tables[item.Key];
                    var det = lista.Where(x => x.destipohorario == item.Key).GroupBy(x => new { x.idperplan, x.destrabajador, x.seccion, x.nrodoc, x.idcia, x.gerencia })
                        .Select(x => new ParametrosBE()
                        {
                            Param1 = x.Key.idperplan,
                            Param2 = x.Key.destrabajador,
                            Param3 = x.Key.seccion,
                            Param4 = x.Key.nrodoc,
                            Param5 = x.Key.idcia,
                            Param6 = x.Key.gerencia
                        }).ToList();

                    if (tabla != null)
                    {
                        foreach (var d in det)
                        {
                            var row = tabla.NewRow();
                            row["Param1"] = d.Param1;
                            row["Param2"] = d.Param2;
                            row["Param3"] = d.Param3;
                            row["Param4"] = d.Param4;
                            row["Param5"] = d.Param5;
                            row["Param6"] = d.Param6;
                            ListaDataSet.Tables[item.Key].Rows.Add(row);
                        }
                    }
                }

                string tipohorario = funciones.getValorComboBox(cmbTipoHorarios, 100).Trim();
                listTipoHorarios.DataSource = null;
                listTipoHorarios.DataSource = ListaDataSet.Tables[tipohorario];
                listTipoHorarios.DisplayMember = "Param2";
                listTipoHorarios.ValueMember = "Param1";
            }

            LoadAreasGerenciasEmpresas();
        }

        private DateTime GetMaxFechaProgramacion()
        {
            DateTime fecha = new DateTime();

            string query = "SELECT ISNULL(MAX(fechafin),CONVERT(VARCHAR(10), GETDATE(), 120)) as fecha FROM prog ";
            if (variables.getValorTypeUsr() != "00")
            {
                query += "WHERE idusrins IN (select distinct idusr from cencosusr where idcencos in (";
                query += "select idcencos from cencosusr where idusr = '" + variables.getValorUsr() + "')) ";
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

        private void LoadCmbTipoHorarios()
        {
            string query = "SELECT idplantiphorario, descripcion, nominas FROM plantiphorario (NOLOCK) ORDER BY idplantiphorario";
            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();

            try
            {
                SqlCommand myCommand = new SqlCommand(query, conexion);
                SqlDataReader odr = myCommand.ExecuteReader();

                ListaDataSet = new DataSet();
                while (odr.Read())
                {
                    var Tab = new DataTable(odr.GetString(odr.GetOrdinal("idplantiphorario")) + "  " + odr.GetString(odr.GetOrdinal("descripcion")));
                    Tab.Prefix = odr.GetString(odr.GetOrdinal("nominas")).Replace(" / ", "-");
                    Tab.Columns.Add(new DataColumn("Param1"));
                    Tab.Columns.Add(new DataColumn("Param2"));
                    Tab.Columns.Add(new DataColumn("Param3"));
                    Tab.Columns.Add(new DataColumn("Param4"));
                    Tab.Columns.Add(new DataColumn("Param5"));
                    Tab.Columns.Add(new DataColumn("Param6"));
                    ListaDataSet.Tables.Add(Tab);
                }

                odr.Close();
                myCommand.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            finally { conexion.Close(); }

            //var cont = cmbTipoHorarios.Items.Count;
            //ListaDataSet = new DataSet();
            //foreach (var d in cmbTipoHorarios.Items)
            //{
            //    var Tab = new DataTable(d.ToString());
            //    //Tab.Prefix = d.ToString().Split('|')[1].Replace(" / ", "-");
            //    Tab.Columns.Add(new DataColumn("Param1"));
            //    Tab.Columns.Add(new DataColumn("Param2"));
            //    Tab.Columns.Add(new DataColumn("Param3"));
            //    Tab.Columns.Add(new DataColumn("Param4"));
            //    Tab.Columns.Add(new DataColumn("Param5"));
            //    Tab.Columns.Add(new DataColumn("Param6"));
            //    ListaDataSet.Tables.Add(Tab);
            //}
        }

        private List<programacion> ListaPerPlan()
        {
            List<programacion> lista = new List<programacion>();
            string usuario = variables.getValorUsr();
            string seccion = funciones.getValorComboBox(cmbCencos, 8);
            string cadenaseccion = string.Empty;
            if (seccion.Trim() != "X   TODO") { cadenaseccion = " WHERE seccion='" + @seccion + "' "; }
            else
            {
                cadenaseccion = " WHERE seccion IN (" + string.Join(",", cmbCencos.Items.Cast<String>().Select(x => "'" + x.Substring(0, 8) + "'").ToArray()) + ") ";
            }

            string query = " SELECT distinct a.idperplan,RTRIM(a.apepat)+' '+RTRIM(a.apemat)+', '+RTRIM(a.nombres) as nombre ";
            query += ",RTRIM(a.seccion) AS seccion,RTRIM(a.nrodoc) as nrodoc,a.idcia,a.codaux FROM perplan a (NOLOCK) ";

            if (variables.getValorTypeUsr() != "00") { query += "INNER JOIN cencosusr b (NOLOCK) ON b.idcencos = a.seccion "; }
            else { query += "LEFT JOIN cencosusr b (NOLOCK) ON b.idcencos = a.seccion "; }

            if (variables.getValorTypeUsr() != "00") { query += "AND b.idusr='" + variables.getValorUsr() + "' "; }
            query += "INNER JOIN tipoper c (NOLOCK) ON c.idtipoper=a.idtipoper AND c.destipoper NOT LIKE '%EMPLEADO%' ";
            query += @cadenaseccion + " ORDER BY 2 ASC;";

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
                    obj.idperplan = odr.GetString(odr.GetOrdinal("idperplan"));
                    obj.destrabajador = odr.GetString(odr.GetOrdinal("nombre"));
                    obj.seccion = odr.GetString(odr.GetOrdinal("seccion"));
                    obj.nrodoc = odr.GetString(odr.GetOrdinal("nrodoc"));
                    obj.idcia = odr.GetString(odr.GetOrdinal("idcia"));
                    obj.gerencia = odr.GetString(odr.GetOrdinal("codaux"));
                    lista.Add(obj);
                }

                odr.Close();
                myCommand.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            finally { conexion.Close(); }

            return lista;
        }

        private List<programacion> ListaProgDet(string id)
        {
            List<programacion> lista = new List<programacion>();

            string seccion = funciones.getValorComboBox(cmbCencos, 4);
            string cadenaseccion = string.Empty;
            if (seccion != "X") cadenaseccion = " where seccion='" + @seccion + "' ";

            string query = "SELECT a.idprog,a.desprog,a.fechaini,a.fechafin, b.idperplan,b.destrabajador,b.destipohorario, ";
            query += "b.seccion,b.nrodoc,b.idcia,b.gerencia FROM prog a (NOLOCK) INNER JOIN progdet b (NOLOCK) on b.idprog=a.idprog ";
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
                cmbGerencia.Text = "X   TODOS";

                query = " select distinct a.clavemaesgen as clave, a.desmaesgen as descripcion from maesgen a (nolock) ";
                query += "left join cencosusr b (nolock) on b.idcencos=a.clavemaesgen where a.idmaesgen='008' and a.parm2maesgen = '" + cia + "' ";
                if (variables.getValorTypeUsr() != "00")
                {
                    query += "and b.idcia = '" + cia + "' and b.idusr='" + variables.getValorUsr() + "' ";
                }
                funciones.listaComboBox(query, cmbCencos, "X");
                cmbCencos.Text = "X   TODOS";

                string likeNominas = string.Empty;
                string nominas_ = GetNominas(cia, ref likeNominas);

                query = "SELECT idplantiphorario as clave, descripcion FROM plantiphorario (NOLOCK) WHERE (" + likeNominas + ") order by idplantiphorario";
                funciones.listaComboBox(query, cmbTipoHorarios, "");

                List<DataTable> horarios_ = new List<DataTable>(ListaDataSet.Tables.Cast<DataTable>());
                var result_ = horarios_.Where(x => nominas_.Contains(x.Prefix)).ToList();

                if (result_.Count > 0)
                {
                    listTipoHorarios.DataSource = null;
                    listTipoHorarios.DataSource = result_;
                    //listTipoHorarios.DisplayMember = "Param2";
                    listTipoHorarios.ValueMember = "Param1";
                }

                cmbTipoHorarios_SelectedIndexChanged(sender, e);
            }
            else { cmbCencos_SelectedIndexChanged(sender, e); }
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
            string cia = funciones.getValorComboBox(cmbCia, 2);
            string ger = funciones.getValorComboBox(cmbGerencia, 8);

            string query = " select distinct a.clavemaesgen as clave, a.desmaesgen as descripcion from maesgen a (nolock) ";
            query += "left join cencosusr b (nolock) on b.idcencos=a.clavemaesgen where a.idmaesgen='008' ";
            if (ger != "X   TODO")
            {
                query += "and a.parm1maesgen = '" + ger + "' ";
            }
            //else { cmbCencos_SelectedIndexChanged(sender, e); }
            query += "and a.parm2maesgen = '" + cia + "' ";
            if (variables.getValorTypeUsr() != "00")
            {
                query += "and b.idcia = '" + cia + "' and b.idusr='" + variables.getValorUsr() + "' ";
            }
            funciones.listaComboBox(query, cmbCencos, "X");
            cmbCencos.Text = "X   TODOS";
        }

        private void cmbCencos_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadAreasGerenciasEmpresas();
        }

        private void LoadAreasGerenciasEmpresas()
        {
            string cia = funciones.getValorComboBox(cmbCia, 2);
            string ger = funciones.getValorComboBox(cmbGerencia, 8);
            string seccion = funciones.getValorComboBox(cmbCencos, 8);
            if (seccion != "X   TODO") { LoadReProgramaciones(seccion); }

            var lista = new List<ParametrosBE>();
            if (Lista01 != null)
            {
                lista = Lista01.Where(x => x.Param5 == cia).ToList();
            }
            else { lista = Lista01; }

            if (ger != "X   TODO")
            {
                if (lista != null) { lista = lista.Where(x => x.Param6 == ger.Trim()).ToList(); }
            }

            if (seccion != "X   TODO")
            {
                if (lista != null)
                {
                    if (seccion != "") { lista = lista.Where(x => x.Param3 == seccion).ToList(); }
                }
            }

            listEmpleados.DataSource = lista;
            listEmpleados.DisplayMember = "Param2";
            listEmpleados.ValueMember = "Param1";

            if (Lista01 != null)
            {
                lblTotalEmpleados.Text = lista.Count.ToString() + " registros";
            }
        }

        private void cmbTipoHorarios_SelectedIndexChanged(object sender, EventArgs e)
        {
            string tipohorario = funciones.getValorComboBox(cmbTipoHorarios, 100).Trim();

            if (ListaDataSet != null)
            {
                var lista = ListaDataSet.Tables[tipohorario];

                listTipoHorarios.DataSource = null;
                listTipoHorarios.DataSource = lista;
                listTipoHorarios.DisplayMember = "Param2";
                listTipoHorarios.ValueMember = "Param1";
            }
        }
        #endregion

        private void DataBinding_(string tipohorario_)
        {
            string cia = funciones.getValorComboBox(cmbCia, 2);
            string geren = funciones.getValorComboBox(cmbGerencia, 8);
            string seccion = funciones.getValorComboBox(cmbCencos, 8);
            listEmpleados.DataSource = null;
            if (seccion != "X   TODO" || geren != "X   TODO")
            {
                listEmpleados.DataSource = Lista01.Where(x => x.Param3 == (seccion != "X   TODO" ? seccion : x.Param3)
                                            && x.Param5 == cia 
                                            && x.Param6 == (geren != "X   TODO" ? geren : x.Param6)).ToList();
            }
            else { listEmpleados.DataSource = Lista01; }

            cmbCia.Text = cia;
            cmbGerencia.Text = geren;
            cmbCencos.Text = seccion;

            listEmpleados.DisplayMember = "Param2";
            listEmpleados.ValueMember = "Param1";

            listTipoHorarios.DataSource = null;
            listTipoHorarios.DataSource = ListaDataSet.Tables[tipohorario_];
            listTipoHorarios.DisplayMember = "Param2";
            listTipoHorarios.ValueMember = "Param1";

            if (Lista01 != null)
            {
                lblTotalEmpleados.Text = Lista01.Count.ToString() + " registros";
            }
        }

        public DataTable ConvertToDataTable<T>(IList<T> data)
        {
            PropertyDescriptorCollection properties =
               TypeDescriptor.GetProperties(typeof(T));
            DataTable table = new DataTable();
            foreach (PropertyDescriptor prop in properties)
                table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
            foreach (T item in data)
            {
                DataRow row = table.NewRow();
                foreach (PropertyDescriptor prop in properties)
                    row[prop.Name] = prop.GetValue(item) ?? DBNull.Value;
                table.Rows.Add(row);
            }
            return table;
        }

        #region Metodos Click
        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (listEmpleados.SelectedItem != null)
            {
                string tipohorario = funciones.getValorComboBox(cmbTipoHorarios, 100).Trim();
                obj01 = (ParametrosBE)listEmpleados.SelectedItem;

                var tabla = ListaDataSet.Tables[tipohorario];
                var row = tabla.NewRow();
                row["Param1"] = obj01.Param1;
                row["Param2"] = obj01.Param2;
                row["Param3"] = obj01.Param3;
                row["Param4"] = obj01.Param4;
                row["Param5"] = obj01.Param5;
                row["Param6"] = obj01.Param6;
                ListaDataSet.Tables[tipohorario].Rows.Add(row);

                if (listEmpleados != null)
                {
                    Lista01.Remove(obj01);
                }
                DataBinding_(tipohorario);
            }
        }

        private void btnAddAll_Click(object sender, EventArgs e)
        {
            string tipohorario = funciones.getValorComboBox(cmbTipoHorarios, 100).Trim();
            foreach (ParametrosBE item in listEmpleados.Items)
            {
                var tabla = ListaDataSet.Tables[tipohorario];
                var row = tabla.NewRow();
                row["Param1"] = item.Param1;
                row["Param2"] = item.Param2;
                row["Param3"] = item.Param3;
                row["Param4"] = item.Param4;
                row["Param5"] = item.Param5;
                row["Param6"] = item.Param6;
                ListaDataSet.Tables[tipohorario].Rows.Add(row);
            }

            listEmpleados.DataSource = null;
            listTipoHorarios.DataSource = null;
            listTipoHorarios.DataSource = ListaDataSet.Tables[tipohorario];
            listTipoHorarios.DisplayMember = "Param2";
            listTipoHorarios.ValueMember = "Param1";

            foreach (DataRowView del in listTipoHorarios.Items)
            {
                Lista01.RemoveAll(x => x.Param1 == del.Row.ItemArray[0].ToString());
            }

            if (Lista01 != null)
            {
                cmbCencos_SelectedIndexChanged(sender, e);
            }
        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            string tipohorario = funciones.getValorComboBox(cmbTipoHorarios, 100).Trim();
            var tabla = ListaDataSet.Tables[tipohorario];
            var detalle = (from DataRow row in tabla.Rows
                           select new ParametrosBE
                           {
                               Param1 = row["Param1"].ToString(),
                               Param2 = row["Param2"].ToString(),
                               Param3 = row["Param3"].ToString(),
                               Param4 = row["Param4"].ToString(),
                               Param5 = row["Param5"].ToString(),
                               Param6 = row["Param6"].ToString()
                           }).ToList();
            if ((DataRowView)listTipoHorarios.SelectedItem != null)
            {
                var select_ = ((DataRowView)listTipoHorarios.SelectedItem).Row.ItemArray[0];
                obj01 = detalle.Where(x => x.Param1 == select_.ToString()).FirstOrDefault();

                if (obj01 != null)
                {
                    Lista01.Add(obj01);

                    for (int i = ListaDataSet.Tables[tipohorario].Rows.Count - 1; i >= 0; i--)
                    {
                        DataRow dr = ListaDataSet.Tables[tipohorario].Rows[i];
                        if (dr["Param1"].ToString() == obj01.Param1) dr.Delete();
                    }
                }
                DataBinding_(tipohorario);
            }

            //cmbCia_SelectedIndexChanged(sender, e);
        } 

        private void btnDelAll_Click(object sender, EventArgs e)
        {
            string cia = funciones.getValorComboBox(cmbCia, 2);
            string geren = funciones.getValorComboBox(cmbGerencia, 8);
            string seccion = funciones.getValorComboBox(cmbCencos, 8);
            string tipohorario = funciones.getValorComboBox(cmbTipoHorarios, 100).Trim();
            var tabla = ListaDataSet.Tables[tipohorario];
            var detalle = (from DataRow row in tabla.Rows
                           select new ParametrosBE
                           {
                               Param1 = row["Param1"].ToString(),
                               Param2 = row["Param2"].ToString(),
                               Param3 = row["Param3"].ToString(),
                               Param4 = row["Param4"].ToString(),
                               Param5 = row["Param5"].ToString(),
                               Param6 = row["Param6"].ToString()
                           }).ToList();

            Lista01.AddRange(detalle);

            listEmpleados.DataSource = null;
            //listEmpleados.DataSource = (seccion != "X" ? Lista01.Where(x => x.Param3 == seccion).ToList() : Lista01);
            if (seccion != "X   TODO" || geren != "X   TODO")
            {
                listEmpleados.DataSource = Lista01.Where(x => x.Param3 == (seccion != "X   TODO" ? seccion : x.Param3)
                                            && x.Param5 == cia
                                            && x.Param6 == (geren != "X   TODO" ? geren : x.Param6)).ToList();
            }
            else { listEmpleados.DataSource = Lista01; }

            cmbCia.Text = cia;
            cmbGerencia.Text = geren;
            cmbCencos.Text = seccion;

            listEmpleados.DisplayMember = "Param2";
            listEmpleados.ValueMember = "Param1";

            listTipoHorarios.DataSource = null;
            ListaDataSet.Tables[tipohorario].Clear();

            if (Lista01 != null)
            {
                cmbCencos_SelectedIndexChanged(sender, e);
            }
        }

        private List<HorarioBE> GetHorariosRotativos()
        {
            List<HorarioBE> lista = new List<HorarioBE>();

            string query = "SELECT C.idplantiphorario as clave, C.descripcion FROM ( ";
            query += "SELECT idcia, idtipoper FROM perplan ";
            query += "GROUP BY idcia, idtipoper ) T ";
            query += "INNER JOIN cencosusr B (NOLOCK) ON B.idcia = T.idcia ";
            query += "INNER JOIN plantiphorario C (NOLOCK) ON C.idcia='1' AND C.nominas LIKE '%' + T.idtipoper + '%' ";
            query += "WHERE B.idusr = '" + variables.getValorUsr() + "' ";
            query += "ORDER BY C.descripcion ";

            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();

            try
            {
                SqlCommand myCommand = new SqlCommand(query, conexion);
                SqlDataReader odr = myCommand.ExecuteReader();

                while (odr.Read())
                {
                    lista.Add(new HorarioBE()
                    {
                        codigo = odr.GetString(odr.GetOrdinal("clave")),
                        descripcion = odr.GetString(odr.GetOrdinal("descripcion"))
                    });
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

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            programacion be = new programacion()
            {
                fechaini = dtpFecini.Value,
                fechafin = dtpFecfin.Value,
                estado = "V",
                idusrins = variables.getValorUsr(),
                idusrupd = variables.getValorUsr()
            };

            List<programacion> listdet = new List<programacion>();
            programacion be_det = null;
            foreach (DataTable tb in ListaDataSet.Tables)
            {
                foreach (DataRow row in tb.Rows)
                {
                    int CuentaFechas = 1;
                    for (DateTime i = dtpFecini.Value.Date; i <= dtpFecfin.Value.Date; i = i.AddDays(1))
                    {
                        if (CuentaFechas == 8) { CuentaFechas = 1; }

                        be_det = new programacion()
                        {
                            idperplan = row.ItemArray[0].ToString(),
                            fechadiaria = DateTime.Parse(i.ToString("yyyy-MM-dd")),
                            fechaini = i,
                            fechafin = i,
                            destrabajador = row.ItemArray[1].ToString(),
                            seccion = row.ItemArray[2].ToString(),
                            nrodoc = row.ItemArray[3].ToString(),
                            gerencia = row.ItemArray[5].ToString(),
                            idcia = row.ItemArray[4].ToString(),
                            destipohorario = row.Table.TableName,
                            estado = "V",
                            SinHorario = true,
                            CuentaFec = CuentaFechas
                        };
                        listdet.Add(be_det);

                        CuentaFechas++;
                    }
                }
            }

            #region ASIGNAR HORARIOS A TRABAJADORES DE ACUERDO A SU TIPO DE HORARIO
            string HorRotativos = string.Empty;
            List<HorarioBE> HorariosRotativos = GetHorariosRotativos();
            if (HorariosRotativos.Count > 0) { HorRotativos = string.Join("|", HorariosRotativos.Select(x => x.codigo + "  " + x.descripcion).ToList()); }

            plantiphorario tiphor = new plantiphorario();
            string horarios = string.Empty;
            for (int x = 0; x < cmbTipoHorarios.Items.Count; x++)
            {
                horarios += "'" + cmbTipoHorarios.Items[x].ToString().Substring(0, 3).Trim() + "',";
            }
            horarios = horarios.Substring(0, horarios.Length - 1);

            var listaTipHorario = tiphor.GetPlantiphorario(horarios);
            for (int x = 0; x < cmbTipoHorarios.Items.Count; x++)
            {
                var lista = listaTipHorario.Where(y => y.idplantiphorario == cmbTipoHorarios.Items[x].ToString().Substring(0, 3).Trim()).ToList();

                foreach (var item in lista)
                {
                    var det = listdet.Where(c => c.fechadiaria.DayOfWeek.ToString().ToUpper() == item.dias_en &&
                    c.destipohorario == cmbTipoHorarios.Items[x].ToString()).ToList();

                    if (HorRotativos.Contains(cmbTipoHorarios.Items[x].ToString()))
                    {
                        det = listdet.Where(c => c.CuentaFec == item.iddias_semana && c.destipohorario == cmbTipoHorarios.Items[x].ToString()).ToList();
                    }

                    foreach (var upd in det)
                    {
                        upd.fechaini = DateTime.ParseExact(upd.fechadiaria.ToString("yyyy-MM-dd") + " " + item.hor_entrada,
                            "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);
                        upd.fechafin = DateTime.Parse(upd.fechadiaria.ToString("yyyy-MM-dd") + " " + item.hor_salida);
                        if (item.mensaje == 1) { upd.fechafin = upd.fechafin.AddDays(1); }
                        upd.turnonoche = item.mensaje;
                        upd.SinHorario = false;
                    }
                }
            }

            listdet.RemoveAll(x => x.SinHorario == true);
            #endregion

            if (listdet.Count == 0)
            {
                MessageBox.Show("No se ha programado horario de ningun trabajador. Seleccionar al menos uno.!!", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            DateTime Fecha1 = listdet.Min(x => x.fechaini);
            DateTime Fecha2 = listdet.Max(x => x.fechafin);
            if (VerificarDatosDupli(listdet) > 0 && this.Evento != null/*Fecha1 != this.Fecha01 && Fecha2 != this.Fecha02*/)
            {
                MessageBox.Show("Existen trabajadores que ya estan registrados en otra programación!!", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (this.Evento != "NUEVO")
            {
                be.idprog = int.Parse(txtIdProg.Text);
                be.UpdateProg(be, true, listdet);
            }
            else { be.UpdateProg(be, false, listdet); }

            MessageBox.Show("Programacion exitosa..!!", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);

            ((ui_programacion)FormPadre).btnActualizar.PerformClick();
            this.Close();
        }

        private int VerificarDatosDupli(List<programacion> listdet)
        {
            int resultado = 0;
            string ids = string.Join("','", listdet.Select(x => x.idperplan).Distinct().ToList());
            string fechas = string.Join("','", listdet.Select(x => x.fechadiaria.ToString("yyyy-MM-dd")).Distinct().ToList());
            string query = " SELECT COUNT(1) AS contador FROM progdet a (NOLOCK) ";
            query += "INNER JOIN prog b (NOLOCK) on b.idprog=a.idprog and b.idusrins<>'"+ variables.getValorUsr() + "' ";
            query += "WHERE a.idperplan in ('" + @ids + "') and a.fechadiaria in ('" + @fechas + "');";

            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();

            try
            {
                SqlCommand myCommand = new SqlCommand(query, conexion);
                SqlDataReader odr = myCommand.ExecuteReader();

                if (odr.Read())
                {
                    resultado = odr.GetInt32(odr.GetOrdinal("contador"));
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

            return resultado;
        }
        #endregion

        private void dtpFecini_ValueChanged(object sender, EventArgs e)
        {
            dtpFecfin.MinDate = dtpFecini.Value;
        }

        private void dtpFecfin_ValueChanged(object sender, EventArgs e)
        {
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}