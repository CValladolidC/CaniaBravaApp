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
    public partial class ui_asgHorPer : Form
    {
        Funciones fn = new Funciones();

        public TextBox _TextBoxActivo { get; set; }

        public ui_asgHorPer()
        {
            InitializeComponent();
            ProcessItems(false, false);
        }

        private void pictureBoxBuscar_Click(object sender, EventArgs e)
        {

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

        private void txtCodigoInterno_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                if (txtCodigoInterno.Text.Trim() != string.Empty)
                {
                    GlobalVariables gv = new GlobalVariables();
                    List<HorarioPer> ListHorPerBE = new List<HorarioPer>();
                    string idcia = gv.getValorCia();
                    string idperplan = txtCodigoInterno.Text.Trim();
                    string idAsigHorPer = string.Empty, fInicio = string.Empty, fFinal = string.Empty;

                    PerPlan perplan = new PerPlan();
                    string codigoInterno = perplan.ui_getDatosPerPlan(idcia, idperplan, "0");
                    if (codigoInterno == string.Empty)
                    {
                        MessageBox.Show("Código no registrado del trabajador", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txtCodigoInterno.Clear();
                        txtDocIdent.Clear();
                        txtNroDocIden.Clear();
                        txtNombres.Clear();
                    }
                    else
                    {
                        txtCodigoInterno.Text = perplan.ui_getDatosPerPlan(idcia, idperplan, "0");
                        txtDocIdent.Text = perplan.ui_getDatosPerPlan(idcia, idperplan, "1");
                        txtNroDocIden.Text = perplan.ui_getDatosPerPlan(idcia, idperplan, "2");
                        txtNombres.Text = perplan.ui_getDatosPerPlan(idcia, idperplan, "3");
                        ListHorPerBE = pr_ListHorarioPer(idperplan, ref idAsigHorPer, ref fInicio, ref fFinal);
                        if (ListHorPerBE.Count > 0)
                        {
                            txtIdAsigHor.Text = idAsigHorPer;
                            dTimeFecInicio.Text = fInicio;
                            dTimeFecFin.Text = fFinal;

                            for (int x = 0; x < ListHorPerBE.Count; x++)
                            {
                                fn_LlenaHorarioPer(ListHorPerBE[x]);
                            }
                        }
                    }
                }
            }
        }

        private void fn_LlenaHorarioPer(HorarioPer ListHorPerBE)
        {
            string[] arrHorEnt = ListHorPerBE.h_entrada.Split(':'), arrHorSal = ListHorPerBE.h_salida.Split(':');
            switch (ListHorPerBE.dias_semana)
            {
                case 1: chkLu.Checked = true; nLuHor1.Value = int.Parse(arrHorEnt[0]);
                    nLuMin1.Value = int.Parse(arrHorEnt[1].Split(' ')[0]);
                    cbAmPmLu1.Text = arrHorEnt[1].Split(' ')[1];
                    nLuHor2.Value = int.Parse(arrHorSal[0]);
                    nLuMin2.Value = int.Parse(arrHorSal[1].Split(' ')[0]);
                    cbAmPmLu2.Text = arrHorSal[1].Split(' ')[1]; evntClickChkDias(0, true); break;

                case 2: chkMa.Checked = true; nMaHor1.Value = int.Parse(arrHorEnt[0]);
                    nMaMin1.Value = int.Parse(arrHorEnt[1].Split(' ')[0]);
                    cbAmPmMa1.Text = arrHorEnt[1].Split(' ')[1];
                    nMaHor2.Value = int.Parse(arrHorSal[0]);
                    nMaMin2.Value = int.Parse(arrHorSal[1].Split(' ')[0]);
                    cbAmPmMa2.Text = arrHorSal[1].Split(' ')[1]; evntClickChkDias(1, true); break;

                case 3: chkMi.Checked = true; nMiHor1.Value = int.Parse(arrHorEnt[0]);
                    nMiMin1.Value = int.Parse(arrHorEnt[1].Split(' ')[0]);
                    cbAmPmMi1.Text = arrHorEnt[1].Split(' ')[1];
                    nMiHor2.Value = int.Parse(arrHorSal[0]);
                    nMiMin2.Value = int.Parse(arrHorSal[1].Split(' ')[0]);
                    cbAmPmMi2.Text = arrHorSal[1].Split(' ')[1]; evntClickChkDias(2, true); break;

                case 4: chkJu.Checked = true; nJuHor1.Value = int.Parse(arrHorEnt[0]);
                    nJuMin1.Value = int.Parse(arrHorEnt[1].Split(' ')[0]);
                    cbAmPmJu1.Text = arrHorEnt[1].Split(' ')[1];
                    nJuHor2.Value = int.Parse(arrHorSal[0]);
                    nJuMin2.Value = int.Parse(arrHorSal[1].Split(' ')[0]);
                    cbAmPmJu2.Text = arrHorSal[1].Split(' ')[1]; evntClickChkDias(3, true); break;

                case 5: chkVi.Checked = true; nViHor1.Value = int.Parse(arrHorEnt[0]);
                    nViMin1.Value = int.Parse(arrHorEnt[1].Split(' ')[0]);
                    cbAmPmVi1.Text = arrHorEnt[1].Split(' ')[1];
                    nViHor2.Value = int.Parse(arrHorSal[0]);
                    nViMin2.Value = int.Parse(arrHorSal[1].Split(' ')[0]);
                    cbAmPmVi2.Text = arrHorSal[1].Split(' ')[1]; evntClickChkDias(4, true); break;

                case 6: chkSa.Checked = true; nSaHor1.Value = int.Parse(arrHorEnt[0]);
                    nSaMin1.Value = int.Parse(arrHorEnt[1].Split(' ')[0]);
                    cbAmPmSa1.Text = arrHorEnt[1].Split(' ')[1];
                    nSaHor2.Value = int.Parse(arrHorSal[0]);
                    nSaMin2.Value = int.Parse(arrHorSal[1].Split(' ')[0]);
                    cbAmPmSa2.Text = arrHorSal[1].Split(' ')[1]; evntClickChkDias(5, true); break;

                case 7: chkDo.Checked = true; nDoHor1.Value = int.Parse(arrHorEnt[0]);
                    nDoMin1.Value = int.Parse(arrHorEnt[1].Split(' ')[0]);
                    cbAmPmDo1.Text = arrHorEnt[1].Split(' ')[1];
                    nDoHor2.Value = int.Parse(arrHorSal[0]);
                    nDoMin2.Value = int.Parse(arrHorSal[1].Split(' ')[0]);
                    cbAmPmDo2.Text = arrHorSal[1].Split(' ')[1]; evntClickChkDias(6, true); break;
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

        private void txtCodigoInterno_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                GlobalVariables gv = new GlobalVariables();
                string idcia = gv.getValorCia();
                this._TextBoxActivo = txtCodigoInterno;
                string cadenaBusqueda = string.Empty;
                string condicionAdicional = " ";
                FiltrosMaestros filtros = new FiltrosMaestros();
                filtros.filtrarPerPlan2("ui_asgplan", this, txtCodigoInterno, idcia, cadenaBusqueda, condicionAdicional);
            }
        }

        private void txtCodigoInterno_TextChanged(object sender, EventArgs e)
        {

        }

        private void ui_asgplan_Load(object sender, EventArgs e)
        {
            //llenacombo(cmb_sedes, "call sp_devuelveCia2", "Descripcion", "idvalue");
        }

        private void plan_horario_SelectedIndexChanged(object sender, EventArgs e)
        {
            //llenacombo(Perfiles, "call co_perfiles('" + plan_horario.SelectedValue + "'", "Descripcion", "idvalue");
        }

        private void btnAsignar_Click(object sender, EventArgs e)
        {
            GlobalVariables variables = new GlobalVariables();
            string bd_prov = ConfigurationManager.AppSettings.Get("BD_PROV");
            string idcia = variables.getValorCia();
            string bd = "A";
            if (bd_prov.Equals("agromango")) bd = "M";
            if (bd_prov.Equals("planilla")) bd = "P";
            if (bd_prov.Equals("planlima")) bd = "L";
            if (bd_prov.Equals("planprueba")) bd = "D";
            int result = validaCampos();
            if (result == 1)
            {
                string Lu = (chkLu.Checked) ? fn.RightTxt("0" + nLuHor1.Value, 2) + ":" + fn.RightTxt("0" + nLuMin1.Value, 2) + " " + cbAmPmLu1.Text + "/" + fn.RightTxt("0" + nLuHor2.Value, 2) + ":" + fn.RightTxt("0" + nLuMin2.Value, 2) + " " + cbAmPmLu2.Text : string.Empty;
                string Ma = (chkMa.Checked) ? fn.RightTxt("0" + nMaHor1.Value, 2) + ":" + fn.RightTxt("0" + nMaMin1.Value, 2) + " " + cbAmPmMa1.Text + "/" + fn.RightTxt("0" + nMaHor2.Value, 2) + ":" + fn.RightTxt("0" + nMaMin2.Value, 2) + " " + cbAmPmMa2.Text : string.Empty;
                string Mi = (chkMi.Checked) ? fn.RightTxt("0" + nMiHor1.Value, 2) + ":" + fn.RightTxt("0" + nMiMin1.Value, 2) + " " + cbAmPmMi1.Text + "/" + fn.RightTxt("0" + nMiHor2.Value, 2) + ":" + fn.RightTxt("0" + nMiMin2.Value, 2) + " " + cbAmPmMi2.Text : string.Empty;
                string Ju = (chkJu.Checked) ? fn.RightTxt("0" + nJuHor1.Value, 2) + ":" + fn.RightTxt("0" + nJuMin1.Value, 2) + " " + cbAmPmJu1.Text + "/" + fn.RightTxt("0" + nJuHor2.Value, 2) + ":" + fn.RightTxt("0" + nJuMin2.Value, 2) + " " + cbAmPmJu2.Text : string.Empty;
                string Vi = (chkVi.Checked) ? fn.RightTxt("0" + nViHor1.Value, 2) + ":" + fn.RightTxt("0" + nViMin1.Value, 2) + " " + cbAmPmVi1.Text + "/" + fn.RightTxt("0" + nViHor2.Value, 2) + ":" + fn.RightTxt("0" + nViMin2.Value, 2) + " " + cbAmPmVi2.Text : string.Empty;
                string Sa = (chkSa.Checked) ? fn.RightTxt("0" + nSaHor1.Value, 2) + ":" + fn.RightTxt("0" + nSaMin1.Value, 2) + " " + cbAmPmSa1.Text + "/" + fn.RightTxt("0" + nSaHor2.Value, 2) + ":" + fn.RightTxt("0" + nSaMin2.Value, 2) + " " + cbAmPmSa2.Text : string.Empty;
                string Do = (chkDo.Checked) ? fn.RightTxt("0" + nDoHor1.Value, 2) + ":" + fn.RightTxt("0" + nDoMin1.Value, 2) + " " + cbAmPmDo1.Text + "/" + fn.RightTxt("0" + nDoHor2.Value, 2) + ":" + fn.RightTxt("0" + nDoMin2.Value, 2) + " " + cbAmPmDo2.Text : string.Empty;

                fn.AsigHoraPer(idcia, bd, int.Parse(txtIdAsigHor.Text), txtCodigoInterno.Text, dTimeFecInicio.Text, dTimeFecFin.Text, Lu, Ma, Mi, Ju, Vi, Sa, Do);
            }
            else
            {
                if (result == 2) { MessageBox.Show("La Hora de Entrada debe ser menor a la Hora de Entrada", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop); }
                else
                {
                    MessageBox.Show("Campos obligatorios para un determinado dia seleccionado", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
            }

        }

        private int validaCampos()
        {
            int result = 1;
            if (chkLu.Checked || chkMa.Checked || chkMi.Checked || chkJu.Checked || chkVi.Checked || chkSa.Checked || chkDo.Checked)
            {/*
                if (chkLu.Checked) result = fn.valCampoEmpty(dtpLu1, dtpLu2); if (result != 1) return result;
                if (chkMa.Checked) result = fn.valCampoEmpty(dtpMa1, dtpMa2); if (result != 1) return result;
                if (chkMi.Checked) result = fn.valCampoEmpty(dtpMi1, dtpMi2); if (result != 1) return result;
                if (chkJu.Checked) result = fn.valCampoEmpty(dtpJu1, dtpJu2); if (result != 1) return result;
                if (chkVi.Checked) result = fn.valCampoEmpty(dtpVi1, dtpVi2); if (result != 1) return result;
                if (chkSa.Checked) result = fn.valCampoEmpty(dtpSa1, dtpSa2); if (result != 1) return result;
                if (chkDo.Checked) result = fn.valCampoEmpty(dtpDo1, dtpDo2); if (result != 1) return result;*/
            }
            return result;
        }

        private void chkLu_Click(object sender, EventArgs e)
        {
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
            chkAll.Checked = false;
            if (chkSa.Checked) { evntClickChkDias(5, true); }
            else
            {
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

            if (chkAll.Checked) { ProcessItems(false, true); } else { ProcessItems(false, false); }
        }

        private void evntClickChkDias(int x, bool y)
        {
            switch (x)
            {
                case 0: nLuHor1.Enabled = y;
                    nLuMin1.Enabled = y;
                    nLuHor2.Enabled = y;
                    nLuMin2.Enabled = y;
                    cbAmPmLu1.Enabled = y;
                    cbAmPmLu2.Enabled = y;
                    break;

                case 1: nMaHor1.Enabled = y;
                    nMaMin1.Enabled = y;
                    nMaHor2.Enabled = y;
                    nMaMin2.Enabled = y;
                    cbAmPmMa1.Enabled = y;
                    cbAmPmMa2.Enabled = y;
                    break;

                case 2: nMiHor1.Enabled = y;
                    nMiMin1.Enabled = y;
                    nMiHor2.Enabled = y;
                    nMiMin2.Enabled = y;
                    cbAmPmMi1.Enabled = y;
                    cbAmPmMi2.Enabled = y;
                    break;

                case 3: nJuHor1.Enabled = y;
                    nJuMin1.Enabled = y;
                    nJuHor2.Enabled = y;
                    nJuMin2.Enabled = y;
                    cbAmPmJu1.Enabled = y;
                    cbAmPmJu2.Enabled = y;
                    break;

                case 4: nViHor1.Enabled = y;
                    nViMin1.Enabled = y;
                    nViHor2.Enabled = y;
                    nViMin2.Enabled = y;
                    cbAmPmVi1.Enabled = y;
                    cbAmPmVi2.Enabled = y;
                    break;

                case 5: nSaHor1.Enabled = y;
                    nSaMin1.Enabled = y;
                    nSaHor2.Enabled = y;
                    nSaMin2.Enabled = y;
                    cbAmPmSa1.Enabled = y;
                    cbAmPmSa2.Enabled = y;
                    break;

                case 6: nDoHor1.Enabled = y;
                    nDoMin1.Enabled = y;
                    nDoHor2.Enabled = y;
                    nDoMin2.Enabled = y;
                    cbAmPmDo1.Enabled = y;
                    cbAmPmDo2.Enabled = y;
                    break;
            }
        }

        private void ProcessItems(bool x, bool y)
        {
            nLuHor1.Enabled = (y) ? !x : x;
            nMaHor1.Enabled = x;
            nMiHor1.Enabled = x;
            nJuHor1.Enabled = x;
            nViHor1.Enabled = x;
            nSaHor1.Enabled = x;
            nDoHor1.Enabled = x;

            nLuMin1.Enabled = (y) ? !x : x;
            nMaMin1.Enabled = x;
            nMiMin1.Enabled = x;
            nJuMin1.Enabled = x;
            nViMin1.Enabled = x;
            nSaMin1.Enabled = x;
            nDoMin1.Enabled = x;

            nLuHor2.Enabled = (y) ? !x : x;
            nMaHor2.Enabled = x;
            nMiHor2.Enabled = x;
            nJuHor2.Enabled = x;
            nViHor2.Enabled = x;
            nSaHor2.Enabled = x;
            nDoHor2.Enabled = x;

            nLuMin2.Enabled = (y) ? !x : x;
            nMaMin2.Enabled = x;
            nMiMin2.Enabled = x;
            nJuMin2.Enabled = x;
            nViMin2.Enabled = x;
            nSaMin2.Enabled = x;
            nDoMin2.Enabled = x;

            cbAmPmLu1.Enabled = (y) ? !x : x;
            cbAmPmMa1.Enabled = x;
            cbAmPmMi1.Enabled = x;
            cbAmPmJu1.Enabled = x;
            cbAmPmVi1.Enabled = x;
            cbAmPmSa1.Enabled = x;
            cbAmPmDo1.Enabled = x;

            cbAmPmLu2.Enabled = (y) ? !x : x;
            cbAmPmMa2.Enabled = x;
            cbAmPmMi2.Enabled = x;
            cbAmPmJu2.Enabled = x;
            cbAmPmVi2.Enabled = x;
            cbAmPmSa2.Enabled = x;
            cbAmPmDo2.Enabled = x;
        }

        private void nLuHor1_Click(object sender, EventArgs e)
        {
            if (chkAll.Checked)
            {
                nMaHor1.Value = nLuHor1.Value;
                nMiHor1.Value = nLuHor1.Value;
                nJuHor1.Value = nLuHor1.Value;
                nViHor1.Value = nLuHor1.Value;
                nSaHor1.Value = nLuHor1.Value;
                nDoHor1.Value = nLuHor1.Value;
            }
        }

        private void nLuMin1_Click(object sender, EventArgs e)
        {
            if (chkAll.Checked)
            {
                nMaMin1.Value = nLuMin1.Value;
                nMiMin1.Value = nLuMin1.Value;
                nJuMin1.Value = nLuMin1.Value;
                nViMin1.Value = nLuMin1.Value;
                nSaMin1.Value = nLuMin1.Value;
                nDoMin1.Value = nLuMin1.Value;
            }
        }

        private void nLuHor2_Click(object sender, EventArgs e)
        {
            if (chkAll.Checked)
            {
                nMaHor2.Value = nLuHor2.Value;
                nMiHor2.Value = nLuHor2.Value;
                nJuHor2.Value = nLuHor2.Value;
                nViHor2.Value = nLuHor2.Value;
                nSaHor2.Value = nLuHor2.Value;
                nDoHor2.Value = nLuHor2.Value;
            }
        }

        private void nLuMin2_Click(object sender, EventArgs e)
        {
            if (chkAll.Checked)
            {
                nMaMin2.Value = nLuMin2.Value;
                nMiMin2.Value = nLuMin2.Value;
                nJuMin2.Value = nLuMin2.Value;
                nViMin2.Value = nLuMin2.Value;
                nSaMin2.Value = nLuMin2.Value;
                nDoMin2.Value = nLuMin2.Value;
            }
        }

        private void cbAmPmLu1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (chkAll.Checked)
            {
                cbAmPmMa1.SelectedItem = cbAmPmLu1.SelectedItem;
                cbAmPmMi1.SelectedItem = cbAmPmLu1.SelectedItem;
                cbAmPmJu1.SelectedItem = cbAmPmLu1.SelectedItem;
                cbAmPmVi1.SelectedItem = cbAmPmLu1.SelectedItem;
                cbAmPmSa1.SelectedItem = cbAmPmLu1.SelectedItem;
                cbAmPmDo1.SelectedItem = cbAmPmLu1.SelectedItem;
            }
        }

        private void cbAmPmLu2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (chkAll.Checked)
            {
                cbAmPmMa2.SelectedItem = cbAmPmLu2.SelectedItem;
                cbAmPmMi2.SelectedItem = cbAmPmLu2.SelectedItem;
                cbAmPmJu2.SelectedItem = cbAmPmLu2.SelectedItem;
                cbAmPmVi2.SelectedItem = cbAmPmLu2.SelectedItem;
                cbAmPmSa2.SelectedItem = cbAmPmLu2.SelectedItem;
                cbAmPmDo2.SelectedItem = cbAmPmLu2.SelectedItem;
            }
        }
    }
}