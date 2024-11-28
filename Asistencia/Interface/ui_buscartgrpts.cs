using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Configuration;

namespace CaniaBrava
{
    public partial class ui_buscartgrpts : Form
    {
        private Form FormPadre;

        string codigotg;
        string clasePadre;
        string cadenaBusqueda;

        public void setValores(string codigotg, string clasePadre, string cadenaBusqueda)
        {
            this.codigotg = codigotg;
            this.clasePadre = clasePadre;
            this.cadenaBusqueda = cadenaBusqueda;
        }

        public ui_buscartgrpts()
        {
            InitializeComponent();
        }

        public Form _FormPadre
        {
            get { return FormPadre; }
            set { FormPadre = value; }
        }

        private void ui_buscartgrpts_Load(object sender, EventArgs e)
        {

        }

        public void ui_LoadTgRpts()
        {
            string rp_cindice = this.codigotg;
            string cadenaBusqueda = this.cadenaBusqueda;
            ui_ListaTgRpts(rp_cindice, cadenaBusqueda);
        }

        private void ui_ListaTgRpts(string rp_cindice, string cadenaBusqueda)
        {
            string condicionBusqueda = string.Empty;
            Funciones funciones = new Funciones();

            if (cadenaBusqueda != string.Empty)
            {
                condicionBusqueda = " (rp_cdescri like '%" + @cadenaBusqueda + "%' or  rp_ccodigo like '%" + @cadenaBusqueda + "%')";
            }

            string query = "Select rp_ccodigo,rp_cdescri from tgrpts where rp_cindice='" + @rp_cindice + "' and (rp_cdescri like '%" + @cadenaBusqueda + "%' or  rp_ccodigo like '%" + @cadenaBusqueda + "%') order by rp_ccodigo asc;";


            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();

            try
            {
                using (SqlDataAdapter myDataAdapter = new SqlDataAdapter(query, conexion))
                {
                    DataSet myDataSet = new DataSet();
                    myDataAdapter.Fill(myDataSet, "tbltgrpts");
                    funciones.formatearDataGridView(dgvdetalle);
                    dgvdetalle.DataSource = myDataSet.Tables["tbltgrpts"];
                    dgvdetalle.Columns[0].HeaderText = "Código";
                    dgvdetalle.Columns[1].HeaderText = "Descripción";
                    dgvdetalle.Columns[0].Width = 100;
                    dgvdetalle.Columns[1].Width = 450;


                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }

            conexion.Close();
        }

        protected override bool ProcessCmdKey(ref System.Windows.Forms.Message msg, System.Windows.Forms.Keys keyData)
        {
            if ((!dgvdetalle.Focused))
                return base.ProcessCmdKey(ref msg, keyData);
            if (keyData != Keys.Enter)
                return base.ProcessCmdKey(ref msg, keyData);

            if (dgvdetalle.RowCount > 0)
            {
                DataGridViewRow row = dgvdetalle.CurrentRow;
                string codigo = Convert.ToString(row.Cells["rp_ccodigo"].Value) + "   " + Convert.ToString(row.Cells["rp_cdescri"].Value);
                string clasePadre = this.clasePadre;

                if (clasePadre.Equals("ui_updcompanias"))
                {
                    ((ui_updcompanias)FormPadre)._TextBoxActivo.Text = codigo;
                }

                if (clasePadre.Equals("ui_updpersonal"))
                {
                    ((ui_updpersonal)FormPadre)._TextBoxActivo.Text = codigo;
                }
            }
            this.DialogResult = DialogResult.OK;
            Close();
            return true;
        }

        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            string rp_cindice = this.codigotg;
            string cadenaBusqueda = txtBuscar.Text.Trim();
            ui_ListaTgRpts(rp_cindice, cadenaBusqueda);

        }

        private void txtBuscar_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                e.Handled = true;
                dgvdetalle.Focus();
            }
        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }
    }
}