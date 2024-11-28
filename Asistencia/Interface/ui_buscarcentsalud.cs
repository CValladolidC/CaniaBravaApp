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
    public partial class ui_buscarcentsalud : Form
    {
        Funciones funciones = new Funciones();
        private Form FormPadre;

        string idtipoper;
        string idcia;
        string clasePadre;
        string estadoTrabajador;
        string condicionAdicional;
        string cadenaBusqueda;

        public Form _FormPadre
        {
            get { return FormPadre; }
            set { FormPadre = value; }
        }

        public ui_buscarcentsalud()
        {
            InitializeComponent();

            if (funciones.VersionAssembly()) Application.ExitThread();
        }

        public void setValoresBuscarPerPlan(string clasePadre, string cadenaBusqueda)
        {
            this.clasePadre = clasePadre;
            //this.estadoTrabajador = estadoTrabajador;
            this.cadenaBusqueda = cadenaBusqueda;
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
                string codigo = Convert.ToString(row.Cells["descensalud"].Value);
                string clasePadre = this.clasePadre;

                if (clasePadre.Equals("ui_updregdescanso"))
                {
                    ((ui_updregdescanso)FormPadre).txtEstSalud.Text = codigo;
                }
            }
            this.DialogResult = DialogResult.OK;
            Close();
            return true;
        }

        public void ui_LoadCentroSalud()
        {
            string idtipoper = this.idtipoper;
            string idcia = this.idcia;
            string clasePadre = this.clasePadre;
            string estadoTrabajador = this.estadoTrabajador;
            string condicionAdicional = this.condicionAdicional;
            string cadenaBusqueda = this.cadenaBusqueda;
            ui_Lista(idcia, idtipoper, estadoTrabajador, cadenaBusqueda, condicionAdicional);
        }

        private void ui_buscarperplan_Load(object sender, EventArgs e)
        {
            txtBuscar.Clear();
            txtBuscar.Focus();
        }

        private void ui_Lista(string idcia, string idtipoper, string estadoTrabajador, string cadenaBusqueda, string condicionAdicional)
        {
            string condicionBusqueda = string.Empty;
            string condicionIdTipoPer = string.Empty;
            string condicionEstadoTrabajador = string.Empty;

            Funciones funciones = new Funciones();

            string query = "Select descensalud FROM censalud (NOLOCK) ";
            if (cadenaBusqueda != string.Empty)
            {
                query += "WHERE descensalud LIKE '%" + cadenaBusqueda + "%' ";
            }

            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();

            try
            {
                using (SqlDataAdapter myDataAdapter = new SqlDataAdapter(query, conexion))
                {
                    DataSet myDataSet = new DataSet();
                    myDataAdapter.Fill(myDataSet, "tabla");
                    funciones.formatearDataGridView(dgvdetalle);
                    dgvdetalle.DataSource = myDataSet.Tables["tabla"];
                    dgvdetalle.Columns[0].HeaderText = "Centro de Salud";
                    dgvdetalle.Columns[0].Width = 400;

                    dgvdetalle.AllowUserToResizeRows = false;
                    dgvdetalle.AllowUserToResizeColumns = false;
                    foreach (DataGridViewColumn column in dgvdetalle.Columns)
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
        }

        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            string idcia = this.idcia;
            string idtipoper = this.idtipoper;
            string estadoTrabajador = this.estadoTrabajador;
            string cadenaBusqueda = txtBuscar.Text.Trim();
            string condicionAdicional = this.condicionAdicional;
            ui_Lista(idcia, idtipoper, estadoTrabajador, cadenaBusqueda, condicionAdicional);
        }

        private void dgvdetalle_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void txtBuscar_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                e.Handled = true;
                dgvdetalle.Focus();
            }
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            ui_updcensalud ui_updprovee = new ui_updcensalud();
            ui_updprovee._FormPadre = this;
            ui_updprovee.setValores("ui_buscarcentsalud");
            ui_updprovee.Activate();
            ui_updprovee.New_();
            ui_updprovee.BringToFront();
            ui_updprovee.ShowDialog();
            ui_updprovee.Dispose();

            ui_LoadCentroSalud();
        }
    }
}