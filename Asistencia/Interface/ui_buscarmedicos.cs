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
    public partial class ui_buscarmedicos : Form
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

        public ui_buscarmedicos()
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
                string tipo = Convert.ToString(row.Cells["Tipo"].Value);
                string codigo = Convert.ToString(row.Cells["Codigo"].Value);
                string medico = Convert.ToString(row.Cells["Medico"].Value);
                string especia = Convert.ToString(row.Cells["Especialidad"].Value);
                string clasePadre = this.clasePadre;

                if (clasePadre.Equals("ui_updregdescanso"))
                {
                    ((ui_updregdescanso)FormPadre).rdCMP.Checked = false;
                    ((ui_updregdescanso)FormPadre).rdCOP.Checked = false;
                    if (tipo == "CMP") { ((ui_updregdescanso)FormPadre).rdCMP.Checked = true; }
                    else { ((ui_updregdescanso)FormPadre).rdCOP.Checked = true; }
                   
                    ((ui_updregdescanso)FormPadre).txtCMP.Text = codigo;
                    ((ui_updregdescanso)FormPadre).txtMedico.Text = medico;
                    ((ui_updregdescanso)FormPadre).txtEspecialidad.Text = especia;
                }
            }
            this.DialogResult = DialogResult.OK;
            Close();
            return true;
        }

        public void ui_Load()
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

            string query = "Select Tipo, id AS Codigo, RTRIM(nombres)+' '+RTRIM(apellidos) AS Medico, Especialidad FROM medicos (NOLOCK) ";
            if (cadenaBusqueda != string.Empty)
            {
                query += "WHERE RTRIM(nombres)+' '+RTRIM(apellidos) LIKE '%" + cadenaBusqueda + "%' ";
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
                    dgvdetalle.Columns[0].Width = 75;
                    dgvdetalle.Columns[1].Width = 75;
                    dgvdetalle.Columns[2].Width = 250;
                    dgvdetalle.Columns[3].Width = 200;

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
            ui_updmedicos ui_updprovee = new ui_updmedicos();
            ui_updprovee._FormPadre = this;
            ui_updprovee.setValores("ui_buscarmedico");
            ui_updprovee.Activate();
            ui_updprovee.New_();
            ui_updprovee.BringToFront();
            ui_updprovee.ShowDialog();
            ui_updprovee.Dispose();

            ui_Load();
        }
    }
}