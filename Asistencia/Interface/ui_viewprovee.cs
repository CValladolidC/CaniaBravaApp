using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Configuration;
using System.Data.SqlClient;

namespace CaniaBrava
{
    public partial class ui_viewprovee : ui_form
    {
        Funciones funciones = new Funciones();
        private Form FormPadre;
        public string _clasePadre;
        public string _condicionAdicional;
        public string _cadenaBusqueda;

         public Form _FormPadre
        {
            get { return FormPadre; }
            set { FormPadre = value; }
        }

        public ui_viewprovee()
        {
            InitializeComponent();

            if (funciones.VersionAssembly()) Application.ExitThread();
        }

        private void ui_viewprovee_Load(object sender, EventArgs e)
        {
            ui_Lista("");
            txtBuscar.Clear();
            txtBuscar.Focus();
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
                string codigo = Convert.ToString(row.Cells["codprovee"].Value);
                string clasePadre = this._clasePadre;

                if (clasePadre.Equals("ui_updalmov"))
                {
                    ((ui_updalmov)FormPadre)._TextBoxActivo.Text = codigo;
                }
                else
                {
                    if (clasePadre.Equals("ui_updcabparte"))
                    {
                        ((ui_updcabparte)FormPadre)._TextBoxActivo.Text = codigo;
                    }
                    else
                    {
                        if (clasePadre.Equals("ui_movprovee"))
                        {
                            ((ui_movprovee)FormPadre)._TextBoxActivo.Text = codigo;
                        }
                        else
                        {
                            if (clasePadre.Equals("ui_emiprov"))
                            {
                                ((ui_emiprov)FormPadre)._TextBoxActivo.Text = codigo;
                            }
                            else
                            {
                                if (clasePadre.Equals("ui_updsolialma"))
                                {
                                    ((ui_updsolialma)FormPadre)._TextBoxActivo.Text = codigo;
                                }
                            }
                        }
                    }
                }
            }
            this.DialogResult = DialogResult.OK;
            Close();
            return true;
        }

       
        private void ui_Lista(string buscar)
        {
            string condicionAdicional = this._condicionAdicional;
            string condicionBusqueda = string.Empty;
            Funciones funciones = new Funciones();

            string query =  " Select A.codprovee,A.desprovee,A.ruc ";
            query = query + " from provee A ";
            query = query + " where A.desprovee like '%" + @buscar + "%' and A.estado='V'";
            query = query + condicionAdicional + " order by A.desprovee asc;";

            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();

            try
            {
                using (SqlDataAdapter myDataAdapter = new SqlDataAdapter(query, conexion))
                {
                    DataSet myDataSet = new DataSet();
                    myDataAdapter.Fill(myDataSet, "tblprovee");
                    funciones.formatearDataGridView(dgvdetalle);
                    dgvdetalle.DataSource = myDataSet.Tables["tblprovee"];
                    dgvdetalle.Columns[0].HeaderText = "Código";
                    dgvdetalle.Columns[1].HeaderText = "Razón Social";
                    dgvdetalle.Columns[2].HeaderText = "R.U.C.";

                    dgvdetalle.Columns[0].Width = 120;
                    dgvdetalle.Columns[1].Width = 450;
                    dgvdetalle.Columns[2].Width = 100;
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
            string buscar = txtBuscar.Text.Trim();
            string condicionAdicional = this._condicionAdicional;
            ui_Lista(buscar);
        }

        
        private void txtBuscar_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                e.Handled = true;
                dgvdetalle.Focus();
            }
        }
    }
}
