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
    public partial class ui_updprogramacionGrafAgri : Form
    {
        Funciones funciones = new Funciones();
        DataTable dt;
        string Desposition { get; set; }
        int Id { get; set; }
        DateTime Fecha { get; set; }
        string Usuario { get; set; }
        string Capataz { get; set; }
        string Fundo { get; set; }
        string Equipo { get; set; }
        string Turno { get; set; }
        string Actividad { get; set; }
        List<Progagridet> ListaProg { get; set; }

        public ui_updprogramacionGrafAgri()
        {
            InitializeComponent();

            if (funciones.VersionAssembly()) Application.ExitThread();
        }

        public void LoadDatos(string idprog, string fecha1, string fecha2, string texto)
        {
            this.Id = int.Parse(idprog);
            txtProg.Text = texto;
            dtpFecini.Value = DateTime.Parse(fecha1);
            dtpFecfin.Value = DateTime.Parse(fecha2);

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

                    if (dtper.Rows.Count > 0)
                    {
                        this.Usuario = dtper.Rows[0].ItemArray[5].ToString();
                    }

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
            int dias = (dtpFecfin.Value - dtpFecini.Value).Days + 1;
            for (int i = 0; i < dias; i++)
            {
                dt.Columns.Add(utilfechas.dayOfWeek(utilfechas.incrementarFecha(dtpFecini.Value.ToString("dd-MM-yyyy"), i)) + "  " +
                    utilfechas.incrementarFecha(dtpFecini.Value.ToString("dd-MM-yyyy"), i), typeof(string));
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

        private void EjecutarQuery(string query, DataGridView dgv, int adi)
        {
            GlobalVariables variables = new GlobalVariables();
            DataTable dt = new DataTable();
            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();

            try
            {
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = new SqlCommand(query, conexion);
                da.SelectCommand.CommandTimeout = 360;
                da.Fill(dt);

                funciones.formatearDataGridView(dgv);
                dgv.DataSource = dt;
                da.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            finally
            {
                conexion.Close();
            }

            dgv.ColumnHeadersVisible = false;

            switch (adi)
            {
                case 1:
                    dgv.Columns[0].Visible = false;
                    dgv.Columns[1].Width = 248;
                    break;
                case 2:
                case 3:
                case 4:
                    dgv.Columns[0].Visible = false;
                    dgv.Columns[1].Width = 88;
                    break;
                case 5:
                    dgv.Columns[0].Width = 30;
                    dgv.Columns[1].Width = 220;
                    dgv.Columns[2].Width = 50;
                    break;
                default:
                    dgv.Columns[0].Visible = false;
                    dgv.Columns[1].Width = 106;
                    break;
            }

            dgv.AllowUserToResizeRows = false;
            dgv.AllowUserToResizeColumns = false;
            foreach (DataGridViewColumn column in dgv.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
        }

        private void dgvdetalle_SelectionChanged(object sender, EventArgs e)
        {
            GlobalVariables variables = new GlobalVariables();
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

            Totalizados();
            LoadCapataces();
            IniBotones(false);

            bool sigue = true;
            if (variables.getValorUsr() != "00")
            {
                sigue = false;
                if (this.Fecha.Date > DateTime.Now.Date) { sigue = true; }
            }

            if (sigue)
            {
                IniBotones(true);
            }
        }

        private void IniBotones(bool x)
        {
            addCap.Visible = x;
            delcap.Visible = x;
            addFun.Visible = x;
            delFun.Visible = x;
            addEqui.Visible = x;
            delEqui.Visible = x;
            addTur.Visible = x;
            delTur.Visible = x;
            addAct.Visible = x;
            delAct.Visible = x;
        }

        private void Totalizados()
        {
            string query = "SELECT (SELECT ISNULL(SUM(cantidadprog),0) FROM progagri_fecafueqtuac (NOLOCK) WHERE idprog='" + this.Id + "') AS totalprog,";
            query += "(SELECT ISNULL(SUM(cantidadprog),0) FROM progagri_fecafueqtuac (NOLOCK) ";
            query += "WHERE idprog='" + this.Id + "' AND fecha='" + this.Fecha.ToString("yyyy-MM-dd") + "') AS totaldia ";

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
                    txttotalprog.Text = odr.GetDecimal(odr.GetOrdinal("totalprog")).ToString();
                    txttotaldia.Text = odr.GetDecimal(odr.GetOrdinal("totaldia")).ToString();
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
        }

        #region Capataces
        private void LoadCapataces()
        {
            string query = "SELECT idusr,desusr [Capataz] FROM usrfile (NOLOCK) WHERE idusr IN (SELECT capataz FROM progagri_feca (NOLOCK) ";
            query += "WHERE idprog='" + this.Id + "' AND fecha='" + this.Fecha.ToString("yyyy-MM-dd") + "') ";

            EjecutarQuery(query, dgvCapataz, 1);
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
                    query += "WHERE idprog='" + this.Id + "' AND fecha='" + this.Fecha.ToString("yyyy-MM-dd") + "';";
                }
                else
                {
                    query += "SET capataz=CASE WHEN RTRIM(capataz)<>'' THEN RTRIM(capataz)+'|'+" + control.SelectedItem.ToString() + " ELSE '' END  ";
                    query += "WHERE idprog='" + this.Id + "' AND fecha='" + this.Fecha.ToString("yyyy-MM-dd") + "' AND capataz='" + control.SelectedItem.ToString() + "';";
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
            }
        }

        private void addCap_Click(object sender, EventArgs e)
        {
            ui_viewcapataces ui_detalle = new ui_viewcapataces();
            ui_detalle.Load_Datos(this.Usuario, this.Fecha.ToString("yyyy-MM-dd"), this.Id);
            ui_detalle.Activate();
            ui_detalle.BringToFront();

            if (ui_detalle.dgvdetalle.Rows.Count > 1)
            {
                ui_detalle.ShowDialog();
            }
            LoadCapataces();
            Totalizados();
        }

        private void delcap_Click(object sender, EventArgs e)
        {
            Int32 selectedCellCount = dgvCapataz.GetCellCount(DataGridViewElementStates.Selected);
            if (selectedCellCount > 0)
            {
                string capataz = dgvCapataz.Rows[dgvCapataz.SelectedCells[1].RowIndex].Cells[0].Value.ToString();
                string des = dgvCapataz.Rows[dgvCapataz.SelectedCells[1].RowIndex].Cells[1].Value.ToString();
                DialogResult resultado = MessageBox.Show("¿Desea eliminar el Capataz: " + des + "?", "Consulta Importante",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (resultado == DialogResult.Yes)
                {
                    string query = "DELETE FROM progagri_fecafueqtuac WHERE idprog='" + this.Id + "' AND fecha='" + this.Fecha.ToString("yyyy-MM-dd") + "' AND capataz='" + capataz + "';";
                    query += "DELETE FROM progagri_fecafueqtu WHERE idprog='" + this.Id + "' AND fecha='" + this.Fecha.ToString("yyyy-MM-dd") + "' AND capataz='" + capataz + "';";
                    query += "DELETE FROM progagri_fecafueq WHERE idprog='" + this.Id + "' AND fecha='" + this.Fecha.ToString("yyyy-MM-dd") + "' AND capataz='" + capataz + "';";
                    query += "DELETE FROM progagri_fecafu WHERE idprog='" + this.Id + "' AND fecha='" + this.Fecha.ToString("yyyy-MM-dd") + "' AND capataz='" + capataz + "';";
                    query += "DELETE FROM progagri_feca WHERE idprog='" + this.Id + "' AND fecha='" + this.Fecha.ToString("yyyy-MM-dd") + "' AND capataz='" + capataz + "';";

                    EjecutarQuery(query);
                    LoadCapataces();
                    Totalizados();
                }
            }
            else
            {
                MessageBox.Show("No ha seleccionado registro a Eliminar", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void dgvCapataz_SelectionChanged(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dgvCapataz.Rows)
            {
                if (row.Selected)
                {
                    this.Capataz = row.Cells[0].Value.ToString();
                }
            }
            LoadFundos();
        }
        #endregion

        #region Fundos
        private void LoadFundos()
        {
            string query = "SELECT fundo [id],fundo FROM progagri_fecafu (NOLOCK) ";
            query += "WHERE idprog='" + this.Id + "' AND fecha='" + this.Fecha.ToString("yyyy-MM-dd") + "' AND capataz='" + this.Capataz + "' ";

            EjecutarQuery(query, dgvFundos, 2);
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
            }
        }

        private void addFun_Click(object sender, EventArgs e)
        {
            ui_viewfundos ui_detalle = new ui_viewfundos();
            ui_detalle.Load_Datos(this.Usuario, dtpFecini.Value.ToString("yyyy-MM-dd"), dtpFecfin.Value.ToString("yyyy-MM-dd"), this.Capataz, this.Id);
            ui_detalle.Activate();
            ui_detalle.BringToFront();

            if (ui_detalle.dgvdetalle.Rows.Count > 1)
            {
                ui_detalle.ShowDialog();
            }
            LoadFundos();
            Totalizados();
        }

        private void delFun_Click(object sender, EventArgs e)
        {
            Int32 selectedCellCount = dgvFundos.GetCellCount(DataGridViewElementStates.Selected);
            if (selectedCellCount > 0)
            {
                string fundo = dgvFundos.Rows[dgvFundos.SelectedCells[1].RowIndex].Cells[0].Value.ToString();
                DialogResult resultado = MessageBox.Show("¿Desea eliminar el Fundo: " + fundo + "?", "Consulta Importante",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (resultado == DialogResult.Yes)
                {
                    string query = "DELETE FROM progagri_fecafueqtuac WHERE idprog='" + this.Id + "' AND fecha='" + this.Fecha.ToString("yyyy-MM-dd") + "' AND capataz='" +
                        this.Capataz + "' AND fundo='" + fundo + "';";
                    query += "DELETE FROM progagri_fecafueqtu WHERE idprog='" + this.Id + "' AND fecha='" + this.Fecha.ToString("yyyy-MM-dd") + "' AND capataz='" +
                        this.Capataz + "' AND fundo='" + fundo + "';";
                    query += "DELETE FROM progagri_fecafueq WHERE idprog='" + this.Id + "' AND fecha='" + this.Fecha.ToString("yyyy-MM-dd") + "' AND capataz='" +
                        this.Capataz + "' AND fundo='" + fundo + "';";
                    query += "DELETE FROM progagri_fecafu WHERE idprog='" + this.Id + "' AND fecha='" + this.Fecha.ToString("yyyy-MM-dd") + "' AND capataz='" +
                        this.Capataz + "' AND fundo='" + fundo + "';";

                    EjecutarQuery(query);
                    LoadFundos();
                    Totalizados();
                }
            }
            else
            {
                MessageBox.Show("No ha seleccionado registro a Eliminar", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void dgvFundos_SelectionChanged(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dgvFundos.Rows)
            {
                if (row.Selected)
                {
                    this.Fundo = row.Cells[0].Value.ToString();
                }
            }
            LoadEquipos();
        }
        #endregion

        #region Equipos
        private void LoadEquipos()
        {
            string query = "SELECT equipo [id],equipo FROM progagri_fecafueq (NOLOCK) ";
            query += "WHERE idprog='" + this.Id + "' AND fecha='" + this.Fecha.ToString("yyyy-MM-dd") +
                "' AND capataz='" + this.Capataz + "' AND fundo='" + this.Fundo + "' ";

            EjecutarQuery(query, dgvEquipos, 3);
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
            }
        }

        private void addEqui_Click(object sender, EventArgs e)
        {
            ui_viewequipos ui_detalle = new ui_viewequipos();
            ui_detalle.Load_Datos(this.Usuario, this.Fecha.ToString("yyyy-MM-dd"), this.Capataz, this.Fundo, this.Id);
            ui_detalle.Activate();
            ui_detalle.BringToFront();

            if (ui_detalle.dgvdetalle.Rows.Count > 1)
            {
                ui_detalle.ShowDialog();
            }
            LoadEquipos();
            Totalizados();
        }

        private void delEqui_Click(object sender, EventArgs e)
        {
            Int32 selectedCellCount = dgvEquipos.GetCellCount(DataGridViewElementStates.Selected);
            if (selectedCellCount > 0)
            {
                string equipo = dgvEquipos.Rows[dgvEquipos.SelectedCells[1].RowIndex].Cells[0].Value.ToString();
                DialogResult resultado = MessageBox.Show("¿Desea eliminar el Equipo: " + equipo + "?", "Consulta Importante",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (resultado == DialogResult.Yes)
                {
                    string query = "DELETE FROM progagri_fecafueqtuac WHERE idprog='" + this.Id + "' AND fecha='" + this.Fecha.ToString("yyyy-MM-dd") + "' AND capataz='" +
                        this.Capataz + "' AND fundo='" + this.Fundo + "' AND equipo='" + equipo + "';";
                    query += "DELETE FROM progagri_fecafueqtu WHERE idprog='" + this.Id + "' AND fecha='" + this.Fecha.ToString("yyyy-MM-dd") + "' AND capataz='" +
                        this.Capataz + "' AND fundo='" + this.Fundo + "' AND equipo='" + equipo + "';";
                    query += "DELETE FROM progagri_fecafueq WHERE idprog='" + this.Id + "' AND fecha='" + this.Fecha.ToString("yyyy-MM-dd") + "' AND capataz='" +
                        this.Capataz + "' AND fundo='" + this.Fundo + "' AND equipo='" + equipo + "';";

                    EjecutarQuery(query);
                    LoadEquipos();
                    Totalizados();
                }
            }
            else
            {
                MessageBox.Show("No ha seleccionado registro a Eliminar", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void dgvEquipos_SelectionChanged(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dgvEquipos.Rows)
            {
                if (row.Selected)
                {
                    this.Equipo = row.Cells[0].Value.ToString();
                }
            }
            LoadTurnos();
        }
        #endregion

        #region Turnos
        private void LoadTurnos()
        {
            string query = "SELECT turno [id],turno FROM progagri_fecafueqtu (NOLOCK) ";
            query += "WHERE idprog='" + this.Id + "' AND fecha='" + this.Fecha.ToString("yyyy-MM-dd") +
                "' AND capataz='" + this.Capataz + "' AND fundo='" + this.Fundo + "' AND equipo='" + this.Equipo + "' " +
                "ORDER BY turno";

            EjecutarQuery(query, dgvTurnos, 4);
        }

        private void chkTurnos_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            var control = (CheckedListBox)sender;
            if (control.SelectedItem != null)
            {
                if (e.NewValue == CheckState.Checked)
                {
                    string item = control.SelectedItem.ToString();
                    this.Turno = item.ToString().Substring(200, item.ToString().Length - 200).Trim();
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
            }
        }

        private void addTur_Click(object sender, EventArgs e)
        {
            ui_viewturnos ui_detalle = new ui_viewturnos();
            ui_detalle.Load_Datos(this.Usuario, this.Fecha.ToString("yyyy-MM-dd"), this.Capataz, this.Fundo, this.Equipo, this.Id);
            ui_detalle.Activate();
            ui_detalle.BringToFront();

            if (ui_detalle.dgvdetalle.Rows.Count > 1)
            {
                ui_detalle.ShowDialog();
            }
            LoadTurnos();
            Totalizados();
        }

        private void delTur_Click(object sender, EventArgs e)
        {
            Int32 selectedCellCount = dgvTurnos.GetCellCount(DataGridViewElementStates.Selected);
            if (selectedCellCount > 0)
            {
                string turno = dgvTurnos.Rows[dgvTurnos.SelectedCells[1].RowIndex].Cells[0].Value.ToString();
                DialogResult resultado = MessageBox.Show("¿Desea eliminar el Turno: " + turno + "?", "Consulta Importante",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (resultado == DialogResult.Yes)
                {
                    string query = "DELETE FROM progagri_fecafueqtuac WHERE idprog='" + this.Id + "' AND fecha='" + this.Fecha.ToString("yyyy-MM-dd") + "' AND capataz='" +
                        this.Capataz + "' AND fundo='" + this.Fundo + "' AND equipo='" + this.Equipo + "' AND turno='" + turno + "';";
                    query += "DELETE FROM progagri_fecafueqtu WHERE idprog='" + this.Id + "' AND fecha='" + this.Fecha.ToString("yyyy-MM-dd") + "' AND capataz='" +
                        this.Capataz + "' AND fundo='" + this.Fundo + "' AND equipo='" + this.Equipo + "' AND turno='" + turno + "';";

                    EjecutarQuery(query);
                    LoadTurnos();
                    Totalizados();
                }
            }
            else
            {
                MessageBox.Show("No ha seleccionado registro a Eliminar", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void dgvTurnos_SelectionChanged(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dgvTurnos.Rows)
            {
                if (row.Selected)
                {
                    this.Turno = row.Cells[0].Value.ToString();
                }
            }
            LoadActividades();
        }
        #endregion

        #region Actividades
        private void LoadActividades()
        {
            string query = "SELECT a.actividad,b.desmaesgen,a.cantidadprog FROM progagri_fecafueqtuac a (NOLOCK) ";
            query += "INNER JOIN maesgen b (NOLOCK) ON b.idmaesgen='162' AND b.clavemaesgen=a.actividad ";
            query += "WHERE a.idprog='" + this.Id + "' AND a.fecha='" + this.Fecha.ToString("yyyy-MM-dd") +
                "' AND a.capataz='" + this.Capataz + "' AND a.fundo='" + this.Fundo +
                "' AND a.equipo='" + this.Equipo + "' AND a.turno='" + this.Turno + "' ";

            EjecutarQuery(query, dgvActividades, 5);
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

        private void txtnrotrab_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsNumber(e.KeyChar)) && (e.KeyChar != (char)Keys.Back))
            {
                //MessageBox.Show("Solo se permiten numeros", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                e.Handled = true;
                return;
            }
        }

        private void addAct_Click(object sender, EventArgs e)
        {
            ui_viewactividadesjornales ui_detalle = new ui_viewactividadesjornales();
            ui_detalle.Load_Datos(this.Usuario, this.Fecha.ToString("yyyy-MM-dd"), this.Capataz, this.Fundo, this.Equipo, this.Turno, this.Id);
            ui_detalle.Activate();
            ui_detalle.BringToFront();
            ui_detalle.ShowDialog();

            LoadActividades();
            Totalizados();
        }

        private void delAct_Click(object sender, EventArgs e)
        {
            Int32 selectedCellCount = dgvActividades.GetCellCount(DataGridViewElementStates.Selected);
            if (selectedCellCount > 0)
            {
                string actividad = dgvActividades.Rows[dgvActividades.SelectedCells[1].RowIndex].Cells[0].Value.ToString();
                DialogResult resultado = MessageBox.Show("¿Desea eliminar la Actividad: " + actividad + "?", "Consulta Importante",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (resultado == DialogResult.Yes)
                {
                    string query = "DELETE FROM progagri_fecafueqtuac WHERE idprog='" + this.Id + "' AND fecha='" + this.Fecha.ToString("yyyy-MM-dd") + "' AND capataz='" +
                        this.Capataz + "' AND fundo='" + this.Fundo + "' AND equipo='" + this.Equipo + "' AND turno='" + this.Turno + "' AND actividad='" + actividad + "';";

                    EjecutarQuery(query);
                    LoadActividades();
                    Totalizados();
                }
            }
            else
            {
                MessageBox.Show("No ha seleccionado registro a Eliminar", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }
        #endregion

        private void btnSipplan_Click(object sender, EventArgs e)
        {
            Int32 selectedCellCount = dgvCapataz.GetCellCount(DataGridViewElementStates.Selected);
            if (selectedCellCount > 0)
            {
                selectedCellCount = dgvFundos.GetCellCount(DataGridViewElementStates.Selected);
                if (selectedCellCount > 0)
                {
                    string fundo = dgvFundos.Rows[dgvFundos.SelectedCells[1].RowIndex].Cells[1].Value.ToString();

                    ui_viewsipplan ui_detalle = new ui_viewsipplan();
                    ui_detalle.Load_Datos(this.Id, this.Capataz, dtpFecini.Value.ToString("yyyy-MM-dd"), dtpFecfin.Value.ToString("yyyy-MM-dd"), fundo);
                    ui_detalle.Activate();
                    ui_detalle.BringToFront();
                    ui_detalle.ShowDialog();

                    LoadFundos();
                    Totalizados();
                }
                else
                {
                    MessageBox.Show("No ha ingresado Fundo", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
            }
            else
            {
                MessageBox.Show("No ha ingresado Controlador", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }
    }
}