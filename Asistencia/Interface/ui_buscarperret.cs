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
    public partial class ui_buscarperret : Form
    {
        string idcia;
        string cadenaBusqueda;
        string clasePadre;

        private Form FormPadre;

        public Form _FormPadre
        {
            get { return FormPadre; }
            set { FormPadre = value; }
        }

        public ui_buscarperret()
        {
            InitializeComponent();
        }

        public void setValores(string idcia, string cadenaBusqueda, string clasePadre)
        {
            this.idcia = idcia;
            this.cadenaBusqueda = cadenaBusqueda;
            this.clasePadre = clasePadre;
        }

        private void ui_buscarperret_Load(object sender, EventArgs e)
        {
            string idcia = this.idcia;
            string cadenaBusqueda = this.cadenaBusqueda;
            ui_ListaPerRet(idcia, cadenaBusqueda);
        }

        private void ui_ListaPerRet(string idcia, string cadenaBusqueda)
        {

            string condicionBusqueda = string.Empty;

            Funciones funciones = new Funciones();

            if (cadenaBusqueda != string.Empty)
            {
                condicionBusqueda = " and CONCAT(A.apepat,' ',A.apemat,' ',A.nombres) like '%" + @cadenaBusqueda + "%' ";
            }

            string query = "select A.idperplan,C.Parm1maesgen as cortotipodoc,A.nrodoc, ";
            query = query + "CONCAT(A.apepat,' ',A.apemat,' ',A.nombres) as nombre from perret A ";
            query = query + "left join maesgen C on C.idmaesgen='002' and A.tipdoc=C.clavemaesgen ";
            query = query + "where A.idcia='" + @idcia + "' " + condicionBusqueda + " order by A.idperplan asc;";


            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();

            try
            {
                using (SqlDataAdapter myDataAdapter = new SqlDataAdapter(query, conexion))
                {
                    DataSet myDataSet = new DataSet();
                    myDataAdapter.Fill(myDataSet, "tblPerRet");
                    funciones.formatearDataGridView(dgvdetalle);

                    dgvdetalle.DataSource = myDataSet.Tables["tblPerRet"];
                    dgvdetalle.Columns[0].HeaderText = "Cód.Int.";
                    dgvdetalle.Columns[1].HeaderText = "Doc.Ident.";
                    dgvdetalle.Columns[2].HeaderText = "Nro.Doc.";
                    dgvdetalle.Columns[3].HeaderText = "Apellidos y Nombres";

                    dgvdetalle.Columns[0].Width = 50;
                    dgvdetalle.Columns[1].Width = 60;
                    dgvdetalle.Columns[2].Width = 70;
                    dgvdetalle.Columns[3].Width = 350;

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }

            conexion.Close();
        }

        private void txtBuscar_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                e.Handled = true;
                dgvdetalle.Focus();
            }
        }

        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            string idcia = this.idcia;
            string cadenaBusqueda = txtBuscar.Text.Trim();
            ui_ListaPerRet(idcia, cadenaBusqueda);
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
                string codigo = Convert.ToString(row.Cells["idperplan"].Value);
                string clasePadre = this.clasePadre;

                if (clasePadre.Equals("ui_upddestajo"))
                {
                    ((ui_upddestajo)FormPadre)._TextBoxActivo.Text = codigo;
                }
                else
                {
                    if (clasePadre.Equals("ui_EmisionDestajoPorTrab_Periodo"))
                    {
                        ((ui_EmisionDestajoPorTrab_Periodo)FormPadre)._TextBoxActivo.Text = codigo;
                    }
                    else
                    {
                        if (clasePadre.Equals("ui_upddatareten"))
                        {
                            ((ui_upddatareten)FormPadre)._TextBoxActivo.Text = codigo;
                        }

                    }

                }

            }
            this.DialogResult = DialogResult.OK;
            Close();
            return true;
        }
    }
}