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
    public partial class ui_updsolicapacitaciones : Form
    {
        Funciones funciones = new Funciones();
        GlobalVariables variables = new GlobalVariables();

        private bool Evento = true;
        private TextBox TextBoxUbigeo;
        private TextBox TextBoxDscUbigeo;

        public TextBox _TextBoxUbigeo
        {
            get { return TextBoxUbigeo; }
            set { TextBoxUbigeo = value; }
        }

        public TextBox _TextBoxDscUbigeo
        {
            get { return TextBoxDscUbigeo; }
            set { TextBoxDscUbigeo = value; }
        }

        public ui_updsolicapacitaciones()
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
            //string query = "SELECT idcia as clave, descia as descripcion FROM ciafile (NOLOCK) ";
            //funciones.listaComboBox(query, cmbCia, "X");

            maesgen.listaDetMaesGen("184", cmbModalidad, "B");
            maesgen.listaDetMaesGen("185", cmbTipoEvento, "B");
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
                string desproblema = "";//txtProblematica.Text.Trim();
                string desobjetivo = "";//txtObjetivo.Text.Trim();
                //string idcia = funciones.getValorComboBox(cmbCia, 2);
                //string geren = funciones.getValorComboBox(cmbGerencia, 8);
                //string secci = funciones.getValorComboBox(cmbCencos, 8);
                //string posic = funciones.getValorComboBox(cmbPosiciones, 3);
                //string tipcapacita = funciones.getValorComboBox(cmbTipCapacita, 3);
                string necesidad = "";// funciones.getValorComboBox(cmbNecesidad, 3);
                string indicador = "";// funciones.getValorComboBox(cmbIndicadores, 3);
                string usuario = variables.getValorUsr();

                //if (this.Evento)
                //{
                //    idcapacita = objCls.GetMaxId();

                //    query = " INSERT INTO capacita (idcapacita,descapacita,desproblema,desobjetivo,idcia,geren,secci,posic,tipcapacita,necesidad,indicador, ";
                //    query += " userregistro,fecharegistro,userupdate,fechaupdate,estado) VALUES ";
                //    query += "('" + idcapacita + "','" + descapacita + "','" + desproblema + "','" + desobjetivo + "',";
                //    query += " '" + idcia + "','" + geren + "','" + secci + "','" + posic + "',";
                //    query += " '" + tipcapacita + "','" + necesidad + "','" + indicador + "',";
                //    query += " '" + usuario + "',GETDATE(),'" + usuario + "',GETDATE(),'0')";
                //}
                //else
                //{
                //    query = " UPDATE capacita SET descapacita='" + descapacita + "',desproblema='" + desproblema + "',desobjetivo='" + desobjetivo + "',";
                //    query += "tipcapacita='" + tipcapacita + "',necesidad='" + necesidad + "',indicador='" + indicador + "',";
                //    query += "userupdate='" + usuario + "',fechaupdate=GETDATE() ";
                //    query += "WHERE idcapacita = '" + idcapacita + "'";

                //    mensaje = "Actualizacion exitosa..!!";
                //}

                objCls.UpdateCapacitacion(query, mensaje);

                this.Close();
            }
        }

        private bool ValidaCampos()
        {
            string descapacita = txtTema.Text.Trim();
            //string desproblema = txtProblematica.Text.Trim();
            //string desobjetivo = txtObjetivo.Text.Trim();

            if (descapacita != string.Empty)
            {
                MessageBox.Show("Debe ingresar Tema de Capacitacion", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }
            if (descapacita != string.Empty)
            {
                MessageBox.Show("Debe ingresar Problematica de la Capacitacion", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }
            if (descapacita != string.Empty)
            {
                MessageBox.Show("Debe ingresar Objetivo de la Capacitacion", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }

            return true;
        }

        private void txtProblematica_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                e.Handled = true;
                //txtObjetivo.Focus();
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

        public void Editar(string id, string estado)
        {
            this.Evento = false;
            string query = string.Empty;
            query = " SELECT a.idcapacita,a.descapacita [Tema],b.desmaesgen [Posicion],c.desmaesgen [Area],d.desmaesgen [Gerencia],e.descia [Empresa],";
            query += "a.desproblema [Problematica],a.desobjetivo [Objetivo],";
            query += "h.desmaesgen [Tip.Capacitacion],i.desusr ";
            query += "FROM capacita a (NOLOCK) ";
            query += "INNER JOIN maesgen b (NOLOCK) ON b.idmaesgen='050' AND b.clavemaesgen=a.posic AND b.parm1maesgen=a.secci AND b.parm2maesgen=a.geren AND b.parm3maesgen=a.idcia ";
            query += "INNER JOIN maesgen c (NOLOCK) ON c.idmaesgen='008' AND c.clavemaesgen=a.secci AND c.parm1maesgen=a.geren AND c.parm2maesgen=a.idcia ";
            query += "INNER JOIN maesgen d (NOLOCK) ON d.idmaesgen='040' AND d.clavemaesgen=a.geren AND d.parm1maesgen=a.idcia ";
            query += "INNER JOIN ciafile e (NOLOCK) ON e.idcia=a.idcia ";
            query += "INNER JOIN maesgen h (NOLOCK) ON h.idmaesgen='182' AND h.clavemaesgen=a.tipcapacita ";
            query += "INNER JOIN usrfile i (NOLOCK) ON i.idusr=a.userregistro ";
            query += "WHERE a.idcapacita = '" + id + "'";

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

                        string _id = "0000000" + myReader["idcapacita"].ToString();
                        txtID.Text = _id.Substring(_id.Length - 7, 7);
                        txtnombre.Text = myReader["desusr"].ToString();
                        txtTema.Text = myReader["Tema"].ToString();
                        txtempresa.Text = myReader["Empresa"].ToString();
                        txtgerencia.Text = myReader["Gerencia"].ToString();
                        txtarea.Text = myReader["Area"].ToString();
                        txtnrotrabajadores.Text = myReader["Posicion"].ToString();
                        txttipcapacita.Text = myReader["Tip.Capacitacion"].ToString();

                        //txtProblematica.Text = myReader["desproblema"].ToString();
                        //txtObjetivo.Text = myReader["desobjetivo"].ToString();
                        //chkProgramada.Checked = (myReader["programada"].ToString() == "1" ? true : false);
                        txtentidad.Focus();
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
                gr03.Enabled = false;
            }
        }

        private void chkInscripcion_CheckedChanged(object sender, EventArgs e)
        {
            txtvalinscripcion.Enabled = false;
            if (chkInscripcion.Checked)
            {
                txtvalinscripcion.Enabled = true;
                txtvalinscripcion.Focus();
            }
        }

        private void chkmatricula_CheckedChanged(object sender, EventArgs e)
        {
            txtvalmatricula.Enabled = false;
            if (chkmatricula.Checked)
            {
                txtvalmatricula.Enabled = true;
                txtvalmatricula.Focus();
            }
        }

        private void chkapoyo_CheckedChanged(object sender, EventArgs e)
        {
            txtvalapoyo.Enabled = false;
            if (chkapoyo.Checked)
            {
                txtvalapoyo.Enabled = true;
                txtvalapoyo.Focus();
            }
        }

        private void btnUbigeo_Click(object sender, EventArgs e)
        {
            _TextBoxDscUbigeo = txtDscUbigeo;
            _TextBoxUbigeo = txtcodUbigeo;
            ui_ubigeo ui_ubigeo = new ui_ubigeo();
            ui_ubigeo._FormPadre = this;
            ui_ubigeo._clasePadre = "ui_updsolicapacitaciones";

            ui_ubigeo.ui_nuevaSeleccion();

            if (ui_ubigeo.ShowDialog(this) == DialogResult.OK)
            {
                dtpfechacapacita.Focus();
            }
            else
            {
                dtpfechacapacita.Focus();
            }
            ui_ubigeo.Dispose();
        }

        private void txtentidad_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                e.Handled = true;
                btnUbigeo.Focus();
            }
        }

        private void cmbTipoEvento_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtTipEvento.Visible = false;
            string tipevento = funciones.getValorComboBox(cmbTipoEvento, 3);
            if (tipevento.Trim() != string.Empty)
            {
                if (tipevento.Trim() == "006")
                {
                    txtTipEvento.Visible = true;
                    txtTipEvento.Focus();
                }
            }
        }
    }
}