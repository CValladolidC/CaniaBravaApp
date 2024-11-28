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
    public partial class ui_UpdConPdt : Form
    {
        string _tipconpdt;

        private Form FormPadre;

        public Form _FormPadre
        {
            get { return FormPadre; }
            set { FormPadre = value; }
        }

        public ui_UpdConPdt()
        {
            InitializeComponent();
        }

        private void ui_UpdConPdt_Load(object sender, EventArgs e)
        {

        }

        public void setValoresConPdt(string tipconpdt)
        {
            this._tipconpdt = tipconpdt;
        }

        public void newConPdt()
        {
            txtOperacion.Text = "AGREGAR";
            txtCodigo.Enabled = true;
            txtCodigo.Clear();
            txtDescripcion.Clear();
            chkDevengado.Checked = true;
            chkPagado.Checked = true;
            chkCero.Checked = false;
            chkDevengadoPlame.Checked = true;
            chkPagadoPlame.Checked = true;
            chkCeroPlame.Checked = false;
            txtCodigo.Focus();
        }

        public void loadConPdt(string idconpdt, string desconpdt, string devengado, string pagado, string regcero)
        {
            txtOperacion.Text = "EDITAR";
            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();
            string query = "SELECT * FROM conpdt where idconpdt='" + @idconpdt + "';";
            try
            {
                SqlCommand myCommand = new SqlCommand(query, conexion);
                SqlDataReader myReader = myCommand.ExecuteReader();

                if (myReader.Read())
                {
                    txtCodigo.Enabled = false;
                    txtCodigo.Text = myReader["idconpdt"].ToString();
                    txtDescripcion.Text = myReader["desconpdt"].ToString();

                    if (myReader["devengado"].Equals("S"))
                    {
                        chkDevengado.Checked = true;
                    }
                    else
                    {
                        chkDevengado.Checked = false;
                    }

                    if (myReader["pagado"].Equals("S"))
                    {
                        chkPagado.Checked = true;
                    }
                    else
                    {
                        chkPagado.Checked = false;
                    }

                    if (myReader["regcero"].Equals("S"))
                    {
                        chkCero.Checked = true;
                    }
                    else
                    {
                        chkCero.Checked = false;
                    }
                    if (myReader["devenplame"].Equals("S"))
                    {
                        chkDevengadoPlame.Checked = true;
                    }
                    else
                    {
                        chkDevengadoPlame.Checked = false;
                    }

                    if (myReader["pagaplame"].Equals("S"))
                    {
                        chkPagadoPlame.Checked = true;
                    }
                    else
                    {
                        chkPagadoPlame.Checked = false;
                    }

                    if (myReader["regceroplame"].Equals("S"))
                    {
                        chkCeroPlame.Checked = true;
                    }
                    else
                    {
                        chkCeroPlame.Checked = false;
                    }

                    txtDescripcion.Focus();
                }

                myReader.Close();
                myCommand.Dispose();

            }

            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }

            conexion.Close();

        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            try
            {
                string operacion = txtOperacion.Text.Trim();
                string idconpdt = txtCodigo.Text.Trim();
                string desconpdt = txtDescripcion.Text.Trim();
                string tipconpdt = this._tipconpdt;
                string devengado;
                string pagado;
                string regcero;
                string devenplame;
                string pagaplame;
                string regceroplame;

                if (chkDevengado.Checked)
                {
                    devengado = "S";
                }
                else
                {
                    devengado = "N";
                }

                if (chkPagado.Checked)
                {
                    pagado = "S";
                }
                else
                {
                    pagado = "N";
                }

                if (chkCero.Checked)
                {
                    regcero = "S";
                }
                else
                {
                    regcero = "N";
                }

                if (chkDevengadoPlame.Checked)
                {
                    devenplame = "S";
                }
                else
                {
                    devenplame = "N";
                }

                if (chkPagadoPlame.Checked)
                {
                    pagaplame = "S";
                }
                else
                {
                    pagaplame = "N";
                }

                if (chkCeroPlame.Checked)
                {
                    regceroplame = "S";
                }
                else
                {
                    regceroplame = "N";
                }

                if (idconpdt == string.Empty)
                {
                    MessageBox.Show("No ha especificado Código", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    txtCodigo.Focus();

                }
                else
                {

                    if (desconpdt == String.Empty)
                    {
                        MessageBox.Show("No ha especificado Descripción del Concepto", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        txtDescripcion.Focus();
                    }
                    else
                    {
                        ConPdt conpdt = new ConPdt();
                        conpdt.actualizarConPdt(operacion, idconpdt, desconpdt, tipconpdt, devengado, pagado, regcero, devenplame, pagaplame, regceroplame);
                        ((ui_ConPdt)FormPadre).btnActualizar.PerformClick();
                        this.Close();
                    }

                }

            }
            catch (FormatException ex)
            {
                MessageBox.Show(ex.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtCodigo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                e.Handled = true;
                txtDescripcion.Focus();
            }
        }
    }
}