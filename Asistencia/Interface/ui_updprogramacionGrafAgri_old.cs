using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CaniaBrava
{
    public partial class ui_updprogramacionGrafAgri_old : Form
    {
        DataTable dt;
        int Idprog { get; set; }
        DateTime Fecha { get; set; }
        string Desposition { get; set; }
        string Capataz { get; set; }
        string Fundo { get; set; }
        string Equipo { get; set; }
        string Turno { get; set; }
        string Actividad { get; set; }
        List<Progagridet> ListaProg { get; set; }

        public ui_updprogramacionGrafAgri_old()
        {
            InitializeComponent();
        }

        public void LoadDatos(string idprog, string fecha1, string fecha2, string texto)
        {
            this.Idprog = int.Parse(idprog);
            txtProg.Text = texto;
            dtpFecini.Value = DateTime.Parse(fecha1);
            dtpFecfin.Value = DateTime.Parse(fecha2);

            ListaProg = ListaProgAgriDet(idprog);
            string query = "SELECT * FROM progagri (NOLOCK) WHERE idprog='" + idprog + "' ";

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

        private List<Progagridet> ListaProgAgriDet(string id)
        {
            List<Progagridet> lista = new List<Progagridet>();
            string query = "SELECT * FROM progagri_feca (NOLOCK) WHERE idprog='" + id + "' ";

            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();

            try
            {
                SqlCommand myCommand = new SqlCommand(query, conexion);
                SqlDataReader odr = myCommand.ExecuteReader();

                Progagridet obj = null;
                while (odr.Read())
                {
                    obj = new Progagridet();
                    obj.idprog = odr.GetInt32(odr.GetOrdinal("idprog"));
                    obj.fecha = odr.GetDateTime(odr.GetOrdinal("fecha"));
                    if (!odr.IsDBNull(odr.GetOrdinal("capataz")))
                        obj.capataz = odr.GetString(odr.GetOrdinal("capataz"));
                    //if (!odr.IsDBNull(odr.GetOrdinal("fundo")))
                    //    obj.fundo = odr.GetString(odr.GetOrdinal("fundo"));
                    //if (!odr.IsDBNull(odr.GetOrdinal("equipo")))
                    //    obj.equipo = odr.GetString(odr.GetOrdinal("equipo"));
                    //if (!odr.IsDBNull(odr.GetOrdinal("turno")))
                    //    obj.turno = odr.GetString(odr.GetOrdinal("turno"));
                    //if (!odr.IsDBNull(odr.GetOrdinal("actividad")))
                    //    obj.actividad = odr.GetString(odr.GetOrdinal("actividad"));
                    //if (!odr.IsDBNull(odr.GetOrdinal("cantidadprog")))
                    //    obj.cantidadprog = odr.GetInt32(odr.GetOrdinal("cantidadprog"));
                    //if (!odr.IsDBNull(odr.GetOrdinal("cantidadreal")))
                    //    obj.cantidadreal = odr.GetInt32(odr.GetOrdinal("cantidadreal"));
                    //if (!odr.IsDBNull(odr.GetOrdinal("chkVB")))
                    //    obj.chkVB = odr.GetInt32(odr.GetOrdinal("chkVB"));
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

        private DataTable GeneraGrid(DataTable dtper)
        {
            UtileriasFechas utilfechas = new UtileriasFechas();
            DataTable dt = new DataTable();
            int dias = (dtpFecfin.Value - dtpFecini.Value).Days + 1;
            for (int i = 0; i < dias; i++)
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
                    for (int i = 0; i < dias; i++)
                    {
                        dr[i] = string.Empty;
                        if (ListaProg != null)
                        {
                            var cod = ListaProg.Find(x => x.fecha.ToString("yyyy-MM-dd") == dtpFecini.Value.ToString("yyyy-MM-dd"));
                            if (cod.capataz != null) { dr[i] = cod.capataz.ToString(); }
                        }
                    }
                    dt.Rows.Add(dr);
                }
            }

            dgvdetalle.DataSource = null;
            dgvdetalle.DataSource = dt;

            DataGridViewCellStyle style = new DataGridViewCellStyle();

            for (int i = 0; i < dias; i++)
            {
                style.BackColor = Color.LightGray;
                dgvdetalle.Columns[i].DefaultCellStyle = style;

                dgvdetalle.Columns[i].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgvdetalle.Columns[i].Width = 40;
                //    ((DataGridViewTextBoxColumn)dgvdetalle.Columns[i + 7]).MaxInputLength = 3;
            }

            if (dias > 19) { dgvdetalle.Height = 75; }
            else { dgvdetalle.Width = (40 * dias) + 5; }

            dgvdetalle.AllowUserToResizeRows = false;
            dgvdetalle.AllowUserToResizeColumns = false;
            foreach (DataGridViewColumn column in dgvdetalle.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }

            return dt;
        }

        private void EjecutarQuery(string query)
        {
            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();

            try
            {
                SqlCommand myCommand = new SqlCommand(query, conexion);
                myCommand.ExecuteNonQuery();
                myCommand.Dispose();
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            conexion.Close();
        }

        private void dgvdetalle_SelectionChanged(object sender, EventArgs e)
        {
            string id = string.Empty;
            foreach (DataGridViewRow row in dgvdetalle.Rows)
            {
                foreach (DataGridViewCell col in row.Cells)
                {
                    if (row.Cells[col.ColumnIndex].Selected)
                    {
                        this.Fecha = dtpFecini.Value.AddDays(col.ColumnIndex);
                        this.Desposition = row.Cells[row.Index].Value.ToString(); break;
                    }
                }
            }

            chkCapataz.Items.Clear();
            chkFundos.Items.Clear();
            chkEquipos.Items.Clear();
            chkTurnos.Items.Clear();
            chkActividades.Items.Clear();
            txtnrotrab.Clear();
            GetCapataces(this.Desposition);
        }

        private void GetCapataces(string id)
        {
            GlobalVariables variables = new GlobalVariables();
            DataTable dt = new DataTable();
            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();

            string query = "SELECT idusr [id],desusr [descripcion] FROM usrfile WHERE idusr IN ( ";
            query += "SELECT abrevia FROM maesgen (NOLOCK) WHERE idmaesgen='163' and RTRIM(desmaesgen)=RTRIM('" + variables.getValorUsr() + "') GROUP BY abrevia) ";

            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = new SqlCommand(query, conexion);
            da.Fill(dt);
            Funciones funciones = new Funciones();
            OptMenu optmenu = new OptMenu();
            chkCapataz.Items.Clear();

            foreach (DataRow row_dtMenu in dt.Rows)
            {
                string cod = row_dtMenu["id"].ToString();
                string des = row_dtMenu["descripcion"].ToString();
                string descripcion = funciones.replicateCadena(" ", 2) + (des + funciones.replicateCadena(" ", 200)).Substring(0, 200) + cod;

                chkCapataz.Items.Add(descripcion, (id.Contains(cod) ? CheckState.Checked : CheckState.Unchecked));
            }
        }

        private void chkCapataz_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            var control = (CheckedListBox)sender;
            if (control.SelectedItem != null)
            {
                //chkFundos.Items.Clear();
                string query = "UPDATE progagridet ";
                if (e.NewValue == CheckState.Checked)
                {
                    query = "INSERT INTO progagridet ";
                    query += "SET capataz=CASE WHEN RTRIM(capataz)<>'' THEN RTRIM(capataz)+'|'+" + control.SelectedItem.ToString() + " ELSE '' END  ";
                    query += "WHERE idprog='" + this.Idprog + "' AND fecha='" + this.Fecha.ToString("yyyy-MM-dd") + "';";
                }
                else
                {
                    query += "SET capataz=CASE WHEN RTRIM(capataz)<>'' THEN RTRIM(capataz)+'|'+" + control.SelectedItem.ToString() + " ELSE '' END  ";
                    query += "WHERE idprog='" + this.Idprog + "' AND fecha='" + this.Fecha.ToString("yyyy-MM-dd") + "' AND capataz='" + control.SelectedItem.ToString() + "';";
                }

                //EjecutarQuery(query);
            }
        }

        private void chkCapataz_Click(object sender, EventArgs e)
        {
            var control = (CheckedListBox)sender;
            if (control.SelectedItem != null)
            {
                string item = control.SelectedItem.ToString();
                this.Capataz = item.ToString().Substring(200, item.ToString().Length - 200).Trim();
                chkFundos.Items.Clear();
                chkEquipos.Items.Clear();
                chkTurnos.Items.Clear();
                chkActividades.Items.Clear();
                txtnrotrab.Clear();
                GetFundos(this.Desposition);
            }
        }

        private void GetFundos(string id)
        {
            GlobalVariables variables = new GlobalVariables();
            DataTable dt = new DataTable();
            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();

            string query = "SELECT parm1maesgen [id] FROM maesgen (NOLOCK) WHERE idmaesgen='163' and desmaesgen='" + variables.getValorUsr() + "' ";
            query += "AND abrevia='" + this.Capataz + "' GROUP BY parm1maesgen";

            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = new SqlCommand(query, conexion);
            da.Fill(dt);
            Funciones funciones = new Funciones();
            OptMenu optmenu = new OptMenu();
            chkFundos.Items.Clear();

            foreach (DataRow row_dtMenu in dt.Rows)
            {
                string cod = row_dtMenu["id"].ToString();
                string des = row_dtMenu["id"].ToString();
                string descripcion = funciones.replicateCadena(" ", 2) + (des + funciones.replicateCadena(" ", 300)).Substring(0, 300) + cod;

                chkFundos.Items.Add(descripcion, (id.Contains(cod) ? CheckState.Checked : CheckState.Unchecked));
            }
        }

        private void chkFundos_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            var control = (CheckedListBox)sender;
            if (control.SelectedItem != null)
            {
                if (e.NewValue == CheckState.Checked)
                {
                }
                else
                {

                }
            }
        }

        private void chkFundos_Click(object sender, EventArgs e)
        {
            var control = (CheckedListBox)sender;
            if (control.SelectedItem != null)
            {
                string item = control.SelectedItem.ToString();
                this.Fundo = item.ToString().Substring(200, item.ToString().Length - 200).Trim();
                chkEquipos.Items.Clear();
                chkTurnos.Items.Clear();
                chkActividades.Items.Clear();
                txtnrotrab.Clear();
                GetEquipos(this.Desposition);
            }
        }

        private void GetEquipos(string id)
        {
            GlobalVariables variables = new GlobalVariables();
            DataTable dt = new DataTable();
            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();

            string query = "SELECT parm2maesgen [id] FROM maesgen (NOLOCK) WHERE idmaesgen='163' and desmaesgen='" + variables.getValorUsr() + "' ";
            query += "AND abrevia='" + this.Capataz + "' AND parm1maesgen='" + this.Fundo + "' GROUP BY parm2maesgen";

            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = new SqlCommand(query, conexion);
            da.Fill(dt);
            Funciones funciones = new Funciones();
            OptMenu optmenu = new OptMenu();
            chkEquipos.Items.Clear();

            foreach (DataRow row_dtMenu in dt.Rows)
            {
                string cod = row_dtMenu["id"].ToString();
                string des = row_dtMenu["id"].ToString();
                string descripcion = funciones.replicateCadena(" ", 2) + (des + funciones.replicateCadena(" ", 200)).Substring(0, 200) + cod;

                chkEquipos.Items.Add(descripcion, (id.Contains(cod) ? CheckState.Checked : CheckState.Unchecked));
            }
        }

        private void chkEquipos_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            var control = (CheckedListBox)sender;
            if (control.SelectedItem != null)
            {
                if (e.NewValue == CheckState.Checked)
                {
                }
                else
                {

                }
            }
        }

        private void chkEquipos_Click(object sender, EventArgs e)
        {
            var control = (CheckedListBox)sender;
            if (control.SelectedItem != null)
            {
                string item = control.SelectedItem.ToString();
                this.Equipo = item.ToString().Substring(200, item.ToString().Length - 200).Trim();
                chkTurnos.Items.Clear();
                chkActividades.Items.Clear();
                txtnrotrab.Clear();
                GetTurnos(this.Desposition);
            }
        }

        private void GetTurnos(string id)
        {
            GlobalVariables variables = new GlobalVariables();
            DataTable dt = new DataTable();
            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();

            string query = "SELECT parm3maesgen [id] FROM maesgen (NOLOCK) WHERE idmaesgen='163' and desmaesgen='" + variables.getValorUsr() + "' ";
            query += "AND abrevia='" + this.Capataz + "' AND parm1maesgen='" + this.Fundo + "' AND parm2maesgen='" + this.Equipo + "' GROUP BY parm3maesgen";
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = new SqlCommand(query, conexion);
            da.Fill(dt);
            Funciones funciones = new Funciones();
            OptMenu optmenu = new OptMenu();
            chkTurnos.Items.Clear();

            foreach (DataRow row_dtMenu in dt.Rows)
            {
                string cod = row_dtMenu["id"].ToString();
                string des = row_dtMenu["id"].ToString();
                string descripcion = funciones.replicateCadena(" ", 2) + (des + funciones.replicateCadena(" ", 200)).Substring(0, 200) + cod;

                chkTurnos.Items.Add(descripcion, (id.Contains(cod) ? CheckState.Checked : CheckState.Unchecked));
            }
        }

        private void chkTurnos_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            var control = (CheckedListBox)sender;
            if (control.SelectedItem != null)
            {
                chkActividades.Items.Clear();
                txtnrotrab.Clear();
                if (e.NewValue == CheckState.Checked)
                {
                    string item = control.SelectedItem.ToString();
                    this.Turno = item.ToString().Substring(200, item.ToString().Length - 200).Trim();
                    GetActividades(this.Desposition);
                }
                else
                {

                }
            }
        }

        private void chkTurnos_Click(object sender, EventArgs e)
        {
            var control = (CheckedListBox)sender;
            if (control.SelectedItem != null)
            {
                string item = control.SelectedItem.ToString();
                this.Turno = item.ToString().Substring(200, item.ToString().Length - 200).Trim();
                chkActividades.Items.Clear();
                txtnrotrab.Clear();
                GetActividades(this.Desposition);
                txtnrotrab.Focus();
            }
        }

        private void GetActividades(string id)
        {
            GlobalVariables variables = new GlobalVariables();
            DataTable dt = new DataTable();
            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();

            string query = "SELECT clavemaesgen [id],desmaesgen [descripcion] FROM maesgen (NOLOCK) WHERE idmaesgen='162' AND clavemaesgen IN (";
            query += "SELECT clavemaesgen FROM maesgen (NOLOCK) WHERE idmaesgen='163' and desmaesgen='" + variables.getValorUsr() + "' ";
            query += "AND abrevia='" + this.Capataz + "' AND parm1maesgen='" + this.Fundo + "' AND parm2maesgen='" + this.Equipo + "' ";
            query += "AND parm3maesgen='" + this.Turno + "') ORDER BY clavemaesgen";
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = new SqlCommand(query, conexion);
            da.Fill(dt);
            Funciones funciones = new Funciones();
            OptMenu optmenu = new OptMenu();
            chkActividades.Items.Clear();

            foreach (DataRow row_dtMenu in dt.Rows)
            {
                string cod = row_dtMenu["id"].ToString();
                string des = row_dtMenu["descripcion"].ToString();
                string descripcion = cod + funciones.replicateCadena(" ", 2) + (des + funciones.replicateCadena(" ", 200)).Substring(0, 200) + cod;

                chkActividades.Items.Add(descripcion, (id.Contains(cod) ? CheckState.Checked : CheckState.Unchecked));
            }
        }

        private void chkActividades_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            var control = (CheckedListBox)sender;
            if (control.SelectedItem != null)
            {
                if (e.NewValue == CheckState.Checked)
                {
                    //if (txtnrotrab.Text == string.Empty)
                    //{
                    //    MessageBox.Show("Debe ingresar cantidad de trabajadores por actividad", "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    //    txtnrotrab.Focus();
                    //    e.NewValue = CheckState.Unchecked;
                    //    return;
                    //}
                    //this.Actividad = control.SelectedItem.ToString();
                    txtnrotrab.Focus();
                }
                else
                {
                    //txtnrotrab.Clear();
                }
            }
        }

        private void chkActividades_Click(object sender, EventArgs e)
        {

        }

        //private void dgvdetalle_CellClick(object sender, DataGridViewCellEventArgs e)
        //{
        //    string content = dgvdetalle.Rows[e.RowIndex].Cells[0].Value.ToString();
        //}

        private void txtnrotrab_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsNumber(e.KeyChar)) && (e.KeyChar != (char)Keys.Back))
            {
                //MessageBox.Show("Solo se permiten numeros", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                e.Handled = true;
                return;
            }
        }
    }
}