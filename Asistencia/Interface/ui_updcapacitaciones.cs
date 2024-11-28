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
    public partial class ui_updcapacitaciones : Form
    {
        Funciones funciones = new Funciones();
        GlobalVariables variables = new GlobalVariables();

        private bool Evento = true;

        public ui_updcapacitaciones()
        {
            InitializeComponent();
        }

        private void ui_accesomenu_Load(object sender, EventArgs e)
        {
            Load_Inicial();
        }

        public void Load_Inicial()
        {
            MaesGen maesgen = new MaesGen();
            string query = "SELECT idcia as clave, descia as descripcion FROM ciafile (NOLOCK) ";
            funciones.listaComboBox(query, cmbCia, "X");

            maesgen.listaDetMaesGen("180", cmbNecesidad, "B");
            maesgen.listaDetMaesGen("181", cmbIndicadores, "B");
            maesgen.listaDetMaesGen("182", cmbTipCapacita, "B");
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnGrabar_Click(object sender, EventArgs e)
        {
            Capacitaciones objCls = new Capacitaciones();

            string query = string.Empty;
            string mensaje = "Registro exitoso..!!";
            string idcapacita = txtID.Text;

            if (ValidaCampos())
            {
                string descapacita = txtTema.Text.Trim();
                string desproblema = txtProblematica.Text.Trim();
                string desobjetivo = txtObjetivo.Text.Trim();
                string idcia = funciones.getValorComboBox(cmbCia, 2);
                string geren = funciones.getValorComboBox(cmbGerencia, 8);
                string secci = funciones.getValorComboBox(cmbCencos, 8);
                string posic = "";// funciones.getValorComboBox(cmbPosiciones, 3);
                string tipcapacita = funciones.getValorComboBox(cmbTipCapacita, 3);
                string necesidad = funciones.getValorComboBox(cmbNecesidad, 3);
                string indicador = funciones.getValorComboBox(cmbIndicadores, 3);
                string usuario = variables.getValorUsr();

                if (this.Evento)
                {
                    idcapacita = objCls.GetMaxId();

                    query = " INSERT INTO capacita (idcapacita,descapacita,desproblema,desobjetivo,idcia,geren,secci,posic,tipcapacita,necesidad,indicador, ";
                    query += " userregistro,fecharegistro,userupdate,fechaupdate,estado) VALUES ";
                    query += "('" + idcapacita + "','" + descapacita + "','" + desproblema + "','" + desobjetivo + "',";
                    query += " '" + idcia + "','" + geren + "','" + secci + "','" + posic + "',";
                    query += " '" + tipcapacita + "','" + necesidad + "','" + indicador + "',";
                    query += " '" + usuario + "',GETDATE(),'" + usuario + "',GETDATE(),'0')";
                }
                else
                {
                    query = " UPDATE capacita SET descapacita='" + descapacita + "',desproblema='" + desproblema + "',desobjetivo='" + desobjetivo + "',";
                    query += "tipcapacita='" + tipcapacita + "',necesidad='" + necesidad + "',indicador='" + indicador + "',";
                    query += "userupdate='" + usuario + "',fechaupdate=GETDATE() ";
                    query += "WHERE idcapacita = '" + idcapacita + "'";

                    mensaje = "Actualizacion exitosa..!!";
                }

                objCls.UpdateCapacitacion(query, mensaje);

                this.Close();
            }
        }

        private bool ValidaCampos()
        {
            string descapacita = txtTema.Text.Trim();
            string desproblema = txtProblematica.Text.Trim();
            string desobjetivo = txtObjetivo.Text.Trim();

            if (descapacita == string.Empty)
            {
                MessageBox.Show("Debe ingresar Tema de Capacitacion", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }
            if (desproblema == string.Empty)
            {
                MessageBox.Show("Debe ingresar Problematica de la Capacitacion", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }
            if (desobjetivo == string.Empty)
            {
                MessageBox.Show("Debe ingresar Objetivo de la Capacitacion", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }

            return true;
        }

        private void cmbCia_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnGrabar.Enabled = false;
            gr02.Enabled = false;
            string cia = funciones.getValorComboBox(cmbCia, 2);
            if (cia != "X")
            {
                string query = "SELECT clavemaesgen as clave, desmaesgen as descripcion FROM maesgen (NOLOCK) ";
                query += "WHERE idmaesgen='040' and parm1maesgen = '" + cia + "' ";
                funciones.listaComboBox(query, cmbGerencia, "X");
            }
            else
            {
                cmbGerencia.Items.Clear();
                cmbGerencia.Items.Add("X   TODOS");
            }
            cmbGerencia.Text = "X   TODOS";
        }

        private void cmbGerencia_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnGrabar.Enabled = false;
            gr02.Enabled = false;
            string cia = funciones.getValorComboBox(cmbCia, 2);
            string ger = funciones.getValorComboBox(cmbGerencia, 8);
            if (ger != "X   TODO")
            {
                string query = "SELECT clavemaesgen as clave, desmaesgen as descripcion FROM maesgen (NOLOCK) ";
                query += "WHERE idmaesgen='008' AND parm1maesgen = '" + ger + "' AND parm2maesgen = '" + cia + "' ";
                funciones.listaComboBox(query, cmbCencos, "X");
            }
            else
            {
                cmbCencos.Items.Clear();
                cmbCencos.Items.Add("X   TODOS");
            }
            cmbCencos.Text = "X   TODOS";
        }

        private void cmbCencos_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnGrabar.Enabled = false;
            gr02.Enabled = false;
            string cia = funciones.getValorComboBox(cmbCia, 2);
            string ger = funciones.getValorComboBox(cmbGerencia, 8);
            string sec = funciones.getValorComboBox(cmbCencos, 8);
            if (sec != "X   TODO")
            {
                //string query = "SELECT clavemaesgen as clave, desmaesgen as descripcion FROM maesgen (NOLOCK)WHERE idmaesgen='050'  ";
                //query += "AND parm1maesgen = '" + sec + "' AND parm2maesgen = '" + ger + "' AND parm3maesgen = '" + cia + "' ";
                //funciones.listaComboBox(query, cmbPosiciones, "X");
                Get_Posiciones(string.Empty, sec, ger, cia);
            }
            else
            {
                chkPosiciones.Items.Clear();
            }
        }

        private void cmbTipCapacita_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnGrabar.Enabled = false;
            gr03.Enabled = false;
            string tipcap = funciones.getValorComboBox(cmbTipCapacita, 3);
            if (tipcap != string.Empty)
            {
                gr03.Enabled = true;
                btnGrabar.Enabled = true;
            }
        }

        private void cmbIndicadores_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblOtrosIndica.Visible = false;
            txtOtrosIndica.Visible = false;
            string indica = funciones.getValorComboBox(cmbIndicadores, 3);
            if (indica.Trim() != string.Empty)
            {
                if (indica.Trim() == "010")
                {
                    lblOtrosIndica.Visible = true;
                    txtOtrosIndica.Visible = true;
                    txtOtrosIndica.Focus();
                }
            }
        }

        private void Get_Posiciones(string posi, string sec, string ger, string cia)
        {
            DataTable dt = new DataTable();
            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();

            string query = "SELECT clavemaesgen as clave, desmaesgen as descripcion FROM maesgen (NOLOCK)WHERE idmaesgen='050'  ";
            query += "AND parm1maesgen = '" + sec + "' AND parm2maesgen = '" + ger + "' AND parm3maesgen = '" + cia + "' ";
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = new SqlCommand(query, conexion);
            da.Fill(dt);
            Funciones funciones = new Funciones();
            OptMenu optmenu = new OptMenu();
            chkPosiciones.Items.Clear();

            foreach (DataRow row_dtMenu in dt.Rows)
            {
                string cod = row_dtMenu["clave"].ToString();
                string des = row_dtMenu["descripcion"].ToString();
                string descripcion = funciones.replicateCadena(" ", (2 * cod.Length)) + (des + funciones.replicateCadena(" ", 200)).Substring(0, 200) + cod;

                chkPosiciones.Items.Add(descripcion, (posi.Contains(cod) ? CheckState.Checked : CheckState.Unchecked));
            }
        }

        public void Editar(string id, string estado)
        {
            this.Evento = false;
            string query = string.Empty;
            query = " SELECT * FROM capacita (NOLOCK) WHERE idcapacita = '" + id + "'";

            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();

            try
            {
                using (SqlCommand myCommand = new SqlCommand(query, conexion))
                {
                    SqlDataReader myReader = myCommand.ExecuteReader();

                    if (myReader.Read())
                    {
                        MaesGen maesgen = new MaesGen();

                        string cia_ = myReader["idcia"].ToString();
                        string ger_ = myReader["geren"].ToString();
                        string sec_ = myReader["secci"].ToString();
                        string pos_ = myReader["posic"].ToString();

                        query = "Select idcia as clave,descia as descripcion from ciafile where idcia='" + cia_ + "';";
                        funciones.consultaComboBox(query, cmbCia);
                        cmbCia.Text = cia_;

                        query = "SELECT clavemaesgen as clave,desmaesgen as descripcion FROM maesgen WHERE idmaesgen='040' and clavemaesgen='" + ger_ + "' and parm1maesgen='" + cia_ + "';";
                        funciones.consultaComboBox(query, cmbGerencia);

                        query = "SELECT clavemaesgen as clave,desmaesgen as descripcion FROM maesgen WHERE idmaesgen='008' and clavemaesgen='" + sec_ + "' and parm1maesgen='" + ger_ + "' and parm2maesgen='" + cia_ + "';";
                        funciones.consultaComboBox(query, cmbCencos);

                        //query = "SELECT clavemaesgen as clave,desmaesgen as descripcion FROM maesgen WHERE idmaesgen='050' and clavemaesgen='" + pos_+ "' and parm1maesgen='" + sec_ + "' and parm2maesgen='" + ger_ + "' and parm3maesgen='" + cia_ + "';";
                        //funciones.consultaComboBox(query, cmbPosiciones);
                        Get_Posiciones(pos_, sec_, ger_, cia_);

                        maesgen.consultaDetMaesGen("180", myReader["necesidad"].ToString(), cmbNecesidad);
                        maesgen.consultaDetMaesGen("181", myReader["indicador"].ToString(), cmbIndicadores);
                        maesgen.consultaDetMaesGen("182", myReader["tipcapacita"].ToString(), cmbTipCapacita);

                        txtID.Text = myReader["idcapacita"].ToString();
                        txtTema.Text = myReader["descapacita"].ToString();
                        txtProblematica.Text = myReader["desproblema"].ToString();
                        txtObjetivo.Text = myReader["desobjetivo"].ToString();
                        chkProgramada.Checked = (myReader["programada"].ToString() == "1" ? true : false);

                        cmbCia.Enabled = false;
                        cmbGerencia.Enabled = false;
                        cmbCencos.Enabled = false;
                        //chkPosiciones.Enabled = false;
                        chkPosiciones.SelectionMode = SelectionMode.None;
                    }

                    myReader.Close();
                    myCommand.Dispose();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }

            conexion.Close();

            if (estado == "PENDIENTE")
            {
                btnGrabar.Visible = false;
                gr02.Enabled = false;
                gr03.Enabled = false;
            }
        }

        //private void cmbPosiciones_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    btnGrabar.Enabled = false;
        //    gr02.Enabled = false;
        //    string pos = funciones.getValorComboBox(cmbPosiciones, 3);
        //    if (pos != "X")
        //    {
        //        gr02.Enabled = true;
        //    }
        //}

        private void txtTema_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                e.Handled = true;
                txtProblematica.Focus();
            }
        }

        private void txtProblematica_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                e.Handled = true;
                txtObjetivo.Focus();
            }
        }

        private void txtObjetivo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                e.Handled = true;
                toolStrip1.Items[0].Select();
                toolStrip1.Focus();
            }
        }

        private void chkPosiciones_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            var control = (CheckedListBox)sender;
            if (control.SelectedItem != null)
            {
                List<string> checkedItems = new List<string>();
                foreach (var item in chkPosiciones.CheckedItems) { checkedItems.Add(item.ToString()); }

                if (e.NewValue == CheckState.Checked) { checkedItems.Add(chkPosiciones.Items[e.Index].ToString()); }
                else { checkedItems.Remove(chkPosiciones.Items[e.Index].ToString()); }

                string posic = string.Empty;
                foreach (var item in checkedItems)
                {
                    posic += item.ToString().Substring(200, item.ToString().Length - 200).Trim() + " / ";
                }

                gr02.Enabled = false;
                if (posic.Length > 0)
                {
                    posic = posic.Substring(0, posic.Length - 3);
                    gr02.Enabled = true;
                }
            }
        }
    }
}