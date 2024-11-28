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
    public partial class ui_asgHorUpd : Form
    {
        Funciones fn = new Funciones();

        public Form _FormPadre { get; set; }
        public TextBox _TextBoxActivo { get; set; }
        string msj = " Horario de Noche ", AM = "a. m.", PM = "p. m.";

        public ui_asgHorUpd()
        {
            InitializeComponent();

            if (fn.VersionAssembly()) Application.ExitThread();
        }

        private void llenacombo(ComboBox cbo, string query, string vtexto, string vvalor)
        {
            // query = "select Descripcion,idplanhorario from plan_horario where id_sede='"+@sede+"'";
            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();
            try
            {
                using (SqlDataAdapter myDataAdapter = new SqlDataAdapter(query, conexion))
                {
                    DataTable dspardia = new DataTable();
                    myDataAdapter.Fill(dspardia);
                    if (dspardia.Rows.Count > 0)
                    {
                        cbo.Enabled = true;
                        cbo.ValueMember = vvalor;
                        cbo.DisplayMember = vtexto;
                        cbo.DataSource = dspardia;
                    }
                    else { cbo.Enabled = false; }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }

            conexion.Close();
        }

        private void fn_LlenaHorarioPer(HorarioPer ListHorPerBE)
        {
            switch (ListHorPerBE.dias_semana)
            {
                case 1: chkLu.Checked = true; dtpLu1.Value = DateTime.Parse(ListHorPerBE.h_entrada);
                    dtpLu2.Value = DateTime.Parse(ListHorPerBE.h_salida); evntClickChkDias(0, true);
                    if (dtpLu1.Value.ToString().Contains(PM) && dtpLu2.Value.ToString().Contains(AM)) { AddMsj(lbl_Lu, msj); }
                    break;

                case 2: chkMa.Checked = true; dtpMa1.Value = DateTime.Parse(ListHorPerBE.h_entrada);
                    dtpMa2.Value = DateTime.Parse(ListHorPerBE.h_salida); evntClickChkDias(1, true);
                    if (dtpMa1.Value.ToString().Contains(PM) && dtpMa2.Value.ToString().Contains(AM)) { AddMsj(lbl_Ma, msj); }
                    break;

                case 3: chkMi.Checked = true; dtpMi1.Value = DateTime.Parse(ListHorPerBE.h_entrada);
                    dtpMi2.Value = DateTime.Parse(ListHorPerBE.h_salida); evntClickChkDias(2, true);
                    if (dtpMi1.Value.ToString().Contains(PM) && dtpMi2.Value.ToString().Contains(AM)) { AddMsj(lbl_Mi, msj); }
                    break;

                case 4: chkJu.Checked = true; dtpJu1.Value = DateTime.Parse(ListHorPerBE.h_entrada);
                    dtpJu2.Value = DateTime.Parse(ListHorPerBE.h_salida); evntClickChkDias(3, true);
                    if (dtpJu1.Value.ToString().Contains(PM) && dtpJu2.Value.ToString().Contains(AM)) { AddMsj(lbl_Ju, msj); }
                    break;

                case 5: chkVi.Checked = true; dtpVi1.Value = DateTime.Parse(ListHorPerBE.h_entrada);
                    dtpVi2.Value = DateTime.Parse(ListHorPerBE.h_salida); evntClickChkDias(4, true);
                    if (dtpVi1.Value.ToString().Contains(PM) && dtpVi2.Value.ToString().Contains(AM)) { AddMsj(lbl_Vi, msj); }
                    break;

                case 6: chkSa.Checked = true; dtpSa1.Value = DateTime.Parse(ListHorPerBE.h_entrada);
                    dtpSa2.Value = DateTime.Parse(ListHorPerBE.h_salida); evntClickChkDias(5, true);
                    if (dtpSa1.Value.ToString().Contains(PM) && dtpSa2.Value.ToString().Contains(AM)) { AddMsj(lbl_Sa, msj); }
                    break;

                case 7: chkDo.Checked = true; dtpDo1.Value = DateTime.Parse(ListHorPerBE.h_entrada);
                    dtpDo2.Value = DateTime.Parse(ListHorPerBE.h_salida); evntClickChkDias(6, true);
                    if (dtpDo1.Value.ToString().Contains(PM) && dtpDo2.Value.ToString().Contains(AM)) { AddMsj(lbl_Do, msj); }
                    break;
            }
        }

        private List<HorarioPer> pr_ListHorarioPer(string idperplan, ref string idAsigHorPer, ref string fInicio, ref string fFinal)
        {
            HorarioPer HorPerBE = null;
            List<HorarioPer> ListHorPerBE = new List<HorarioPer>();
            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");

            conexion.Open();
            try
            {
                using (SqlCommand cmd = new SqlCommand("sp_ListHorPer", conexion))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("?v_idperplan", idperplan);
                    cmd.Parameters["?v_idperplan"].Direction = ParameterDirection.Input;

                    cmd.Parameters.Add(new SqlParameter("?v_idAsigHorPer", SqlDbType.VarChar));
                    cmd.Parameters["?v_idAsigHorPer"].Direction = ParameterDirection.Output;

                    cmd.Parameters.Add(new SqlParameter("?v_fInicio", SqlDbType.VarChar));
                    cmd.Parameters["?v_fInicio"].Direction = ParameterDirection.Output;

                    cmd.Parameters.Add(new SqlParameter("?v_fFinal", SqlDbType.VarChar));
                    cmd.Parameters["?v_fFinal"].Direction = ParameterDirection.Output;

                    using (SqlDataReader odr = cmd.ExecuteReader())
                    {
                        if (odr.HasRows)
                        {
                            while (odr.Read())
                            {
                                HorPerBE = new HorarioPer();
                                HorPerBE.dias_semana = int.Parse(odr["iddias_semana"].ToString());
                                HorPerBE.h_entrada = odr["hor_entrada"].ToString();
                                HorPerBE.h_salida = odr["hor_salida"].ToString();
                                ListHorPerBE.Add(HorPerBE);
                            }
                        }
                    }
                    idAsigHorPer = (string)cmd.Parameters["?v_idAsigHorPer"].Value;
                    fInicio = (string)cmd.Parameters["?v_fInicio"].Value;
                    fFinal = (string)cmd.Parameters["?v_fFinal"].Value;
                }
            }
            catch
            {
                MessageBox.Show("Error al visualizar Horario de Trabajador", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }

            return ListHorPerBE;
        }

        public void NuevoTipHor()
        {
            txtIdTipHor.Text = GetMaxIdTipoHorario();
            txtIdTipHor.Enabled = false;
            Get_Nominas(string.Empty);
        }

        private int validaCampos()
        {
            int result = 1;
            if (chkListNominas.CheckedItems.Count == 0) { return result = 12; }
            if (txtIdTipHor.Text.Equals(string.Empty)) { return result = 11; }
            if (txtNameTipHor.Text.Equals(string.Empty)) { return result = 10; }
            if (chkLu.Checked || chkMa.Checked || chkMi.Checked || chkJu.Checked || chkVi.Checked || chkSa.Checked || chkDo.Checked)
            {
                if (chkLu.Checked) result = fn.valCampoEmpty(dtpLu1, dtpLu2); if (result != 1) return result;
                if (chkMa.Checked) result = fn.valCampoEmpty(dtpMa1, dtpMa2); if (result != 1) return result;
                if (chkMi.Checked) result = fn.valCampoEmpty(dtpMi1, dtpMi2); if (result != 1) return result;
                if (chkJu.Checked) result = fn.valCampoEmpty(dtpJu1, dtpJu2); if (result != 1) return result;
                if (chkVi.Checked) result = fn.valCampoEmpty(dtpVi1, dtpVi2); if (result != 1) return result;
                if (chkSa.Checked) result = fn.valCampoEmpty(dtpSa1, dtpSa2); if (result != 1) return result;
                if (chkDo.Checked) result = fn.valCampoEmpty(dtpDo1, dtpDo2); if (result != 1) return result;
            }
            else { return result = 0; }
            return result;
        }

        private void chkLu_Click(object sender, EventArgs e)
        {
            AddMsj(lbl_Lu, string.Empty);
            chkAll.Checked = false;
            if (chkLu.Checked) { evntClickChkDias(0, true); }
            else
            {
                evntClickChkDias(0, false);
                if (chkMa.Checked) evntClickChkDias(1, true);
                if (chkMi.Checked) evntClickChkDias(2, true);
                if (chkJu.Checked) evntClickChkDias(3, true);
                if (chkVi.Checked) evntClickChkDias(4, true);
                if (chkSa.Checked) evntClickChkDias(5, true);
                if (chkDo.Checked) evntClickChkDias(6, true);
            }
        }

        private void chkMa_Click(object sender, EventArgs e)
        {
            AddMsj(lbl_Ma, string.Empty);
            chkAll.Checked = false;
            if (chkMa.Checked) { evntClickChkDias(1, true); }
            else
            {
                evntClickChkDias(1, false);
                if (chkMa.Checked) evntClickChkDias(1, true);
                if (chkMi.Checked) evntClickChkDias(2, true);
                if (chkJu.Checked) evntClickChkDias(3, true);
                if (chkVi.Checked) evntClickChkDias(4, true);
                if (chkSa.Checked) evntClickChkDias(5, true);
                if (chkDo.Checked) evntClickChkDias(6, true);
            }
        }

        private void chkMi_Click(object sender, EventArgs e)
        {
            AddMsj(lbl_Mi, string.Empty);
            chkAll.Checked = false;
            if (chkMi.Checked) { evntClickChkDias(2, true); }
            else
            {
                evntClickChkDias(2, false);
                if (chkLu.Checked) evntClickChkDias(0, true);
                if (chkMa.Checked) evntClickChkDias(1, true);
                if (chkJu.Checked) evntClickChkDias(3, true);
                if (chkVi.Checked) evntClickChkDias(4, true);
                if (chkSa.Checked) evntClickChkDias(5, true);
                if (chkDo.Checked) evntClickChkDias(6, true);
            }
        }

        private void chkJu_Click(object sender, EventArgs e)
        {
            AddMsj(lbl_Ju, string.Empty);
            chkAll.Checked = false;
            if (chkJu.Checked) { evntClickChkDias(3, true); }
            else
            {
                evntClickChkDias(3, false);
                if (chkLu.Checked) evntClickChkDias(0, true);
                if (chkMa.Checked) evntClickChkDias(1, true);
                if (chkMi.Checked) evntClickChkDias(2, true);
                if (chkVi.Checked) evntClickChkDias(4, true);
                if (chkSa.Checked) evntClickChkDias(5, true);
                if (chkDo.Checked) evntClickChkDias(6, true);
            }
        }

        private void chkVi_Click(object sender, EventArgs e)
        {
            AddMsj(lbl_Vi, string.Empty);
            chkAll.Checked = false;
            if (chkVi.Checked) { evntClickChkDias(4, true); }
            else
            {
                evntClickChkDias(4, false);
                if (chkLu.Checked) evntClickChkDias(0, true);
                if (chkMa.Checked) evntClickChkDias(1, true);
                if (chkMi.Checked) evntClickChkDias(2, true);
                if (chkJu.Checked) evntClickChkDias(3, true);
                if (chkSa.Checked) evntClickChkDias(5, true);
                if (chkDo.Checked) evntClickChkDias(6, true);
            }
        }

        private void chkSa_Click(object sender, EventArgs e)
        {
            AddMsj(lbl_Sa, string.Empty);
            chkAll.Checked = false;
            if (chkSa.Checked) { evntClickChkDias(5, true); }
            else
            {
                AddMsj(lbl_Sa, string.Empty);
                evntClickChkDias(5, false);
                if (chkLu.Checked) evntClickChkDias(0, true);
                if (chkMa.Checked) evntClickChkDias(1, true);
                if (chkMi.Checked) evntClickChkDias(2, true);
                if (chkJu.Checked) evntClickChkDias(3, true);
                if (chkVi.Checked) evntClickChkDias(4, true);
                if (chkDo.Checked) evntClickChkDias(6, true);
            }
        }

        private void chkDo_Click(object sender, EventArgs e)
        {
            AddMsj(lbl_Do, string.Empty);
            chkAll.Checked = false;
            if (chkDo.Checked) { evntClickChkDias(6, true); }
            else
            {
                evntClickChkDias(6, false);
                if (chkLu.Checked) evntClickChkDias(0, true);
                if (chkMa.Checked) evntClickChkDias(1, true);
                if (chkMi.Checked) evntClickChkDias(2, true);
                if (chkJu.Checked) evntClickChkDias(3, true);
                if (chkVi.Checked) evntClickChkDias(4, true);
                if (chkSa.Checked) evntClickChkDias(5, true);
            }
        }

        private void chkAll_Click(object sender, EventArgs e)
        {
            chkLu.Checked = (chkAll.Checked) ? true : false;
            chkMa.Checked = (chkAll.Checked) ? true : false;
            chkMi.Checked = (chkAll.Checked) ? true : false;
            chkJu.Checked = (chkAll.Checked) ? true : false;
            chkVi.Checked = (chkAll.Checked) ? true : false;
            chkSa.Checked = (chkAll.Checked) ? true : false;
            chkDo.Checked = (chkAll.Checked) ? true : false;

            if (chkAll.Checked)
            {
                ProcessItems(false, true);
                RefreshChk(false);
                RefreshMsj(string.Empty);
                if (dtpLu2.Value.ToString().Contains(AM))
                {
                    RefreshMsj(msj);
                }
            }
            else
            {
                ProcessItems(false, false);
                RefreshChk(true);
                RefreshMsj(string.Empty);

                //if (dtpLu2.Value.ToString().Contains("a. m.")) AddMsj(lbl_Lu, msj);
                //if (dtpMa2.Value.ToString().Contains("a. m.")) AddMsj(lbl_Ma, msj);
                //if (dtpMi2.Value.ToString().Contains("a. m.")) AddMsj(lbl_Mi, msj);
                //if (dtpJu2.Value.ToString().Contains("a. m.")) AddMsj(lbl_Ju, msj);
                //if (dtpVi2.Value.ToString().Contains("a. m.")) AddMsj(lbl_Vi, msj);
                //if (dtpSa2.Value.ToString().Contains("a. m.")) AddMsj(lbl_Sa, msj);
                //if (dtpDo2.Value.ToString().Contains("a. m.")) AddMsj(lbl_Do, msj);
            }
        }

        private void evntClickChkDias(int x, bool y)
        {
            switch (x)
            {
                case 0: dtpLu1.Enabled = y;
                    dtpLu2.Enabled = y;
                    ActualizarMsjTurnoChk(lbl_Lu, dtpLu1, dtpLu2, chkLu);
                    break;

                case 1: dtpMa1.Enabled = y;
                    dtpMa2.Enabled = y;
                    ActualizarMsjTurnoChk(lbl_Ma, dtpMa1, dtpMa2, chkMa);
                    break;

                case 2: dtpMi1.Enabled = y;
                    dtpMi2.Enabled = y;
                    ActualizarMsjTurnoChk(lbl_Mi, dtpMi1, dtpMi2, chkMi);
                    break;

                case 3: dtpJu1.Enabled = y;
                    dtpJu2.Enabled = y;
                    ActualizarMsjTurnoChk(lbl_Ju, dtpJu1, dtpJu2, chkJu);
                    break;

                case 4: dtpVi1.Enabled = y;
                    dtpVi2.Enabled = y;
                    ActualizarMsjTurnoChk(lbl_Vi, dtpVi1, dtpVi2, chkVi);
                    break;

                case 5: dtpSa1.Enabled = y;
                    dtpSa2.Enabled = y;
                    ActualizarMsjTurnoChk(lbl_Sa, dtpSa1, dtpSa2, chkSa);
                    break;

                case 6: dtpDo1.Enabled = y;
                    dtpDo2.Enabled = y;
                    ActualizarMsjTurnoChk(lbl_Do, dtpDo1, dtpDo2, chkDo);
                    break;
            }
        }

        private void ProcessItems(bool x, bool y)
        {
            dtpLu1.Enabled = (y) ? !x : x;
            dtpMa1.Enabled = x;
            dtpMi1.Enabled = x;
            dtpJu1.Enabled = x;
            dtpVi1.Enabled = x;
            dtpSa1.Enabled = x;
            dtpDo1.Enabled = x;

            dtpLu2.Enabled = (y) ? !x : x;
            dtpMa2.Enabled = x;
            dtpMi2.Enabled = x;
            dtpJu2.Enabled = x;
            dtpVi2.Enabled = x;
            dtpSa2.Enabled = x;
            dtpDo2.Enabled = x;
        }

        public void asgHorUpdLoad(string codTipHor, string fechaevent, string event_, bool histo, string nominas)
        {
            txtIdTipHor.Enabled = false;
            ProcessItems(false, false);
            string nameTipHor = string.Empty, rotativo = string.Empty;
            List<HorarioPer> ListHorBE = new List<HorarioPer>();

            if (histo) { ListHorBE = pr_ListHorarioHisto(codTipHor, fechaevent, event_, ref nameTipHor); }
            else { ListHorBE = pr_ListHorario(codTipHor, ref nameTipHor, ref rotativo); }
            
            if (ListHorBE.Count > 0)
            {
                RefreshMsj(string.Empty);
                txtNameTipHor.Text = nameTipHor;
                grTipo.Enabled = false;

                if (rotativo == "1") { rdRotativo.Checked = true; }

                txtIdTipHor.Text = codTipHor.ToString();

                for (int x = 0; x < ListHorBE.Count; x++)
                {
                    fn_LlenaHorarioPer(ListHorBE[x]);
                }
            }

            Get_Nominas(nominas);

            txtNameTipHor.Focus();

            if (histo) { btnAsignar.Visible = false; AllReadOnly(); chkListNominas.Enabled = false; }
        }

        private string GetMaxIdTipoHorario()
        {
            string resultado = string.Empty;
            string query = "SELECT MAX(idplantiphorario) + 1  AS resultado FROM plantiphorario (NOLOCK) ";
            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();

            try
            {
                SqlCommand myCommand = new SqlCommand(query, conexion);
                SqlDataReader odr = myCommand.ExecuteReader();

                while (odr.Read())
                {
                    resultado = odr.GetInt32(odr.GetOrdinal("resultado")).ToString();
                }

                odr.Close();
                myCommand.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            finally { conexion.Close(); }

            return resultado;
        }

        private void Get_Nominas(string nominas)
        {
            DataTable dt = new DataTable();
            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();

            string query = "select idtipoper as id,cortotipoper as descripcion from tipoper (NOLOCK) ";
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = new SqlCommand(query, conexion);
            da.Fill(dt);
            Funciones funciones = new Funciones();
            OptMenu optmenu = new OptMenu();

            foreach (DataRow row_dtMenu in dt.Rows)
            {
                string cod = row_dtMenu["id"].ToString();
                string des = row_dtMenu["descripcion"].ToString();
                string descripcion = funciones.replicateCadena(" ", (2 * cod.Length)) + (des + funciones.replicateCadena(" ", 200)).Substring(0, 200) + cod;

                chkListNominas.Items.Add(descripcion, (nominas.Contains(cod) ? CheckState.Checked : CheckState.Unchecked));
            }
        }

        private void AllReadOnly()
        {
            txtNameTipHor.ReadOnly = true;
            chkAll.Enabled = false;
            chkLu.Enabled = false;
            chkMa.Enabled = false;
            chkMi.Enabled = false;
            chkJu.Enabled = false;
            chkVi.Enabled = false;
            chkSa.Enabled = false;
            chkDo.Enabled = false;
            ProcessItems(false, false);
        }

        private List<HorarioPer> pr_ListHorario(string codTipHor, ref string nameTipHor, ref string rotativo)
        {
            HorarioPer HorarioBE = null;
            List<HorarioPer> ListHorario = new List<HorarioPer>();
            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");

            conexion.Open();
            try
            {
                using (SqlCommand cmd = new SqlCommand("sp_ListarHorarios", conexion))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@v_idplantiphorario", codTipHor);

                    cmd.Parameters.Add("@v_descripcion", SqlDbType.VarChar, 100);
                    cmd.Parameters["@v_descripcion"].Direction = ParameterDirection.Output;

                    cmd.Parameters.Add("@v_rotativo", SqlDbType.VarChar, 2);
                    cmd.Parameters["@v_rotativo"].Direction = ParameterDirection.Output;

                    using (SqlDataReader odr = cmd.ExecuteReader())
                    {
                        if (odr.HasRows)
                        {
                            while (odr.Read())
                            {
                                HorarioBE = new HorarioPer();
                                HorarioBE.dias_semana = int.Parse(odr["iddias_semana"].ToString());
                                HorarioBE.h_entrada = odr["hor_entrada"].ToString();
                                HorarioBE.h_salida = odr["hor_salida"].ToString();
                                ListHorario.Add(HorarioBE);
                            }
                        }
                    }
                    nameTipHor = (string)cmd.Parameters["@v_descripcion"].Value;
                    rotativo = (string)cmd.Parameters["@v_rotativo"].Value;
                }
            }
            catch
            {
                MessageBox.Show("Error al visualizar Horario del Personal", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }

            return ListHorario;
        }

        private List<HorarioPer> pr_ListHorarioHisto(string codTipHor, string fechaevent, string event_, ref string nameTipHor)
        {
            HorarioPer HorarioBE = null;
            List<HorarioPer> ListHorario = new List<HorarioPer>();
            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");

            conexion.Open();
            try
            {
                using (SqlCommand cmd = new SqlCommand("sp_ListarHorarios_Histo", conexion))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@v_idplantiphorario", codTipHor);
                    cmd.Parameters.AddWithValue("@v_fechaevento", fechaevent);
                    cmd.Parameters.AddWithValue("@v_evento", event_);

                    cmd.Parameters.Add("@v_descripcion", SqlDbType.VarChar, 100);
                    cmd.Parameters["@v_descripcion"].Direction = ParameterDirection.Output;

                    using (SqlDataReader odr = cmd.ExecuteReader())
                    {
                        if (odr.HasRows)
                        {
                            while (odr.Read())
                            {
                                HorarioBE = new HorarioPer();
                                HorarioBE.dias_semana = int.Parse(odr["iddias_semana"].ToString());
                                HorarioBE.h_entrada = odr["hor_entrada"].ToString();
                                HorarioBE.h_salida = odr["hor_salida"].ToString();
                                ListHorario.Add(HorarioBE);
                            }
                        }
                    }
                    nameTipHor = (string)cmd.Parameters["@v_descripcion"].Value;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al visualizar Horario del Personal", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }

            return ListHorario;
        }

        private void ActualizarMsjTurnoChk(Label lbl_, DateTimePicker dtp1, DateTimePicker dtp2, CheckBox chk)
        {
            if (dtp1.Value.ToString().Contains(AM) && dtp2.Value.ToString().Contains(AM) && chk.Checked)
            {
                lbl_.BackColor = Color.Green;
                AddMsj(lbl_, " Horario de Dia      ");
            }
            if (dtp1.Value.ToString().Contains(PM) && dtp2.Value.ToString().Contains(PM) && chk.Checked)
            {
                lbl_.BackColor = Color.Orange;
                AddMsj(lbl_, " Horario de Tarde  ");
            }
            if (dtp1.Value.ToString().Contains(PM) && dtp2.Value.ToString().Contains(AM) && chk.Checked)
            {
                lbl_.BackColor = Color.Red;
                AddMsj(lbl_, msj);
            }
        }

        private void ActualizarMsjTurno(Label lbl_, DateTimePicker dtp1, DateTimePicker dtp2)
        {
            if (!chkAll.Checked)
            {
                AddMsj(lbl_, string.Empty);
                if (dtp1.Value.ToString().Contains(AM) && dtp2.Value.ToString().Contains(AM) ||
                    dtp1.Value.ToString().Contains(AM) && dtp2.Value.ToString().Contains(PM))
                {
                    lbl_.BackColor = Color.Green;
                    AddMsj(lbl_, " Horario de Dia      ");
                }
                if (dtp1.Value.ToString().Contains(PM) && dtp2.Value.ToString().Contains(PM))
                {
                    lbl_.BackColor = Color.Orange;
                    AddMsj(lbl_, " Horario de Tarde  ");
                }
                if (dtp1.Value.ToString().Contains(PM) && dtp2.Value.ToString().Contains(AM))
                {
                    lbl_.BackColor = Color.Red;
                    AddMsj(lbl_, msj);
                }
            }
        }

        private void dtpLu1_ValueChanged(object sender, EventArgs e)
        {
            //if (chkAll.Checked)
            //{
            //    RefreshMsj(string.Empty);
            //    dtpMa1.Value = dtpLu1.Value;
            //    dtpMi1.Value = dtpLu1.Value;
            //    dtpJu1.Value = dtpLu1.Value;
            //    dtpVi1.Value = dtpLu1.Value;
            //    dtpSa1.Value = dtpLu1.Value;
            //    dtpDo1.Value = dtpLu1.Value;
            //    ChangeColorLabel(dtpLu1, dtpLu2);
            //    if (dtpLu1.Value.ToString().Contains(PM) && dtpLu2.Value.ToString().Contains(AM))
            //    {
            //        AddMsj(lbl_Lu, msj);
            //    }
            //}
            //else
            //{
            //    AddMsj(lbl_Lu, string.Empty);
            //    if (dtpLu1.Value.ToString().Contains(PM) && dtpLu2.Value.ToString().Contains(AM))
            //    {
            //        AddMsj(lbl_Lu, msj);
            //    }
            //}
            dtpLu2.Value = dtpLu1.Value;
            ActualizarMsjTurno(lbl_Lu, dtpLu1, dtpLu2);
        }

        private void dtpLu2_ValueChanged(object sender, EventArgs e)
        {
            ActualizarMsjTurno(lbl_Lu, dtpLu1, dtpLu2);
            //if (chkAll.Checked)
            //{
            //    RefreshMsj(string.Empty);
            //    dtpMa2.Value = dtpLu2.Value;
            //    dtpMi2.Value = dtpLu2.Value;
            //    dtpJu2.Value = dtpLu2.Value;
            //    dtpVi2.Value = dtpLu2.Value;
            //    dtpSa2.Value = dtpLu2.Value;
            //    dtpDo2.Value = dtpLu2.Value;
            //    if (dtpLu1.Value.ToString().Contains(PM) && dtpLu2.Value.ToString().Contains(AM))
            //    {
            //        RefreshMsj(msj);
            //    }
            //}
            //else
            //{
            //    AddMsj(lbl_Lu, string.Empty);
            //    if (dtpLu1.Value.ToString().Contains(PM) && dtpLu2.Value.ToString().Contains(AM))
            //    {
            //        AddMsj(lbl_Lu, msj);
            //    }
            //}
        }

        private void dtpMa1_ValueChanged(object sender, EventArgs e)
        {
            dtpMa2.Value = dtpMa1.Value;
            ActualizarMsjTurno(lbl_Ma, dtpMa1, dtpMa2);
        }

        private void dtpMa2_ValueChanged(object sender, EventArgs e)
        {
            ActualizarMsjTurno(lbl_Ma, dtpMa1, dtpMa2);
        }

        private void dtpMi1_ValueChanged(object sender, EventArgs e)
        {
            dtpMi2.Value = dtpMi1.Value;
            ActualizarMsjTurno(lbl_Mi, dtpMi1, dtpMi2);
        }

        private void dtpMi2_ValueChanged(object sender, EventArgs e)
        {
            ActualizarMsjTurno(lbl_Mi, dtpMi1, dtpMi2);
        }

        private void dtpJu1_ValueChanged(object sender, EventArgs e)
        {
            dtpJu2.Value = dtpJu1.Value;
            ActualizarMsjTurno(lbl_Ju, dtpJu1, dtpJu2);
        }

        private void dtpJu2_ValueChanged(object sender, EventArgs e)
        {
            ActualizarMsjTurno(lbl_Ju, dtpJu1, dtpJu2);
        }

        private void dtpVi1_ValueChanged(object sender, EventArgs e)
        {
            dtpVi2.Value = dtpVi1.Value;
            ActualizarMsjTurno(lbl_Vi, dtpVi1, dtpVi2);
        }

        private void dtpVi2_ValueChanged(object sender, EventArgs e)
        {
            ActualizarMsjTurno(lbl_Vi, dtpVi1, dtpVi2);
        }

        private void dtpSa1_ValueChanged(object sender, EventArgs e)
        {
            dtpSa2.Value = dtpSa1.Value;
            ActualizarMsjTurno(lbl_Sa, dtpSa1, dtpSa2);
        }

        private void dtpSa2_ValueChanged(object sender, EventArgs e)
        {
            ActualizarMsjTurno(lbl_Sa, dtpSa1, dtpSa2);
        }

        private void dtpDo1_ValueChanged(object sender, EventArgs e)
        {
            dtpDo2.Value = dtpDo1.Value;
            ActualizarMsjTurno(lbl_Do, dtpDo1, dtpDo2);
        }

        private void dtpDo2_ValueChanged(object sender, EventArgs e)
        {
            ActualizarMsjTurno(lbl_Do, dtpDo1, dtpDo2);
        }

        private void RefreshMsj(string msj)
        {
            lbl_Lu.Text = msj;
            lbl_Ma.Text = msj;
            lbl_Mi.Text = msj;
            lbl_Ju.Text = msj;
            lbl_Vi.Text = msj;
            lbl_Sa.Text = msj;
            lbl_Do.Text = msj;
        }

        private void RefreshChk(bool ena)
        {
            chkLu.Enabled = ena;
            chkMa.Enabled = ena;
            chkMi.Enabled = ena;
            chkJu.Enabled = ena;
            chkVi.Enabled = ena;
            chkSa.Enabled = ena;
            chkDo.Enabled = ena;
        }

        private void AddMsj(Label lbl, string msj)
        {
            lbl.Text = msj;
        }

        private void txtIdTipHor_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                e.Handled = true;
                txtNameTipHor.Focus();
            }
        }

        private void txtNameTipHor_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                e.Handled = true;
                chkAll.Focus();
            }
        }

        private void btnAsignar_Click(object sender, EventArgs e)
        {
            GlobalVariables variables = new GlobalVariables();
            string bd_prov = ConfigurationManager.AppSettings.Get("BD_PROV");
            string tipo = "0";
            if (rdRotativo.Checked) { tipo = "1"; }

            string nominas=string.Empty;
            int result = validaCampos();
            if (result == 1)
            {
                foreach (var item in chkListNominas.CheckedItems)
                {
                    nominas += item.ToString().Substring(200, item.ToString().Length - 200).Trim() + " / ";
                }

                string Lu = (chkLu.Checked) ? dtpLu1.Value.ToString("HH:mm:ss") + "/" + dtpLu2.Value.ToString("HH:mm:ss") + "|" + lbl_Lu.Text.Length : string.Empty;
                string Ma = (chkMa.Checked) ? dtpMa1.Value.ToString("HH:mm:ss") + "/" + dtpMa2.Value.ToString("HH:mm:ss") + "|" + lbl_Ma.Text.Length : string.Empty;
                string Mi = (chkMi.Checked) ? dtpMi1.Value.ToString("HH:mm:ss") + "/" + dtpMi2.Value.ToString("HH:mm:ss") + "|" + lbl_Mi.Text.Length : string.Empty;
                string Ju = (chkJu.Checked) ? dtpJu1.Value.ToString("HH:mm:ss") + "/" + dtpJu2.Value.ToString("HH:mm:ss") + "|" + lbl_Ju.Text.Length : string.Empty;
                string Vi = (chkVi.Checked) ? dtpVi1.Value.ToString("HH:mm:ss") + "/" + dtpVi2.Value.ToString("HH:mm:ss") + "|" + lbl_Vi.Text.Length : string.Empty;
                string Sa = (chkSa.Checked) ? dtpSa1.Value.ToString("HH:mm:ss") + "/" + dtpSa2.Value.ToString("HH:mm:ss") + "|" + lbl_Sa.Text.Length : string.Empty;
                string Do = (chkDo.Checked) ? dtpDo1.Value.ToString("HH:mm:ss") + "/" + dtpDo2.Value.ToString("HH:mm:ss") + "|" + lbl_Do.Text.Length : string.Empty;

                SqlConnection conexion = new SqlConnection();
                conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
                string query = "EXEC sp_TipHorariosUpd '" + tipo + "','" + txtIdTipHor.Text + "','" + txtNameTipHor.Text + "','" + nominas.Substring(0, nominas.Length - 3)
                    + "','" + Lu + "','" + Ma + "','" + Mi + "','" + Ju + "','" + Vi + "','" + Sa + "','" + Do + "' ;";
                conexion.Open();

                try
                {
                    SqlCommand myCommand = new SqlCommand(query, conexion);
                    myCommand.ExecuteNonQuery();
                    myCommand.Dispose();
                    MessageBox.Show("Proceso completo....", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ((ui_mntHorario)_FormPadre).btnActualizar.PerformClick();
                    this.Close();
                }
                catch (SqlException ex) { }
                conexion.Close();
            }
            else
            {
                if (result == 12) { MessageBox.Show("Debe elegir al menos una Nomina", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop); }
                if (result == 11) { MessageBox.Show("Codigo Tipo de Horario Obligatorio", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop); }
                if (result == 10) { MessageBox.Show("Nombre Tipo de Horario Obligatorio", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop); }
                if (result == 2) { MessageBox.Show("La Hora de Entrada debe ser menor a la Hora de Salida", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop); }
                if (result == 0)
                {
                    MessageBox.Show("Se debe seleccionar como minimo un Dia de la Semana...", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
            }
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ui_asgHorUpd_Load(object sender, EventArgs e)
        {
            dtpLu1.Format = DateTimePickerFormat.Custom;
            dtpLu1.CustomFormat = "HH:mm";
        }

        private void rdRotativo_CheckedChanged(object sender, EventArgs e)
        {
            chkLu.Text = "Dia 01";
            chkMa.Text = "Dia 02";
            chkMi.Text = "Dia 03";
            chkJu.Text = "Dia 04";
            chkVi.Text = "Dia 05";
            chkSa.Text = "Dia 06";
            chkDo.Text = "Dia 07";
            chkAll.Visible = false;
        }

        private void rdNormal_CheckedChanged(object sender, EventArgs e)
        {
            chkLu.Text = "Lunes";
            chkMa.Text = "Martes";
            chkMi.Text = "Miercoles";
            chkJu.Text = "Jueves";
            chkVi.Text = "Viernes";
            chkSa.Text = "Sabado";
            chkDo.Text = "Domingo";
            chkAll.Visible = true;
        }
    }
}