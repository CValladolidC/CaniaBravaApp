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
    public partial class ui_viewlotes : Form
    {
        Funciones funciones = new Funciones();
        private Form FormPadre;
        string _query;
        string _clasePadre;

        public Form _FormPadre
        {
            get { return FormPadre; }
            set { FormPadre = value; }
        }
        public ui_viewlotes()
        {
            InitializeComponent();

            if (funciones.VersionAssembly()) Application.ExitThread();
        }

        public void setData(string query, string clasePadre, string titulo)
        {
            this._query = query;
            this._clasePadre = clasePadre;
            this.Text = titulo;
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
                string codigo = Convert.ToString(row.Cells["lote"].Value);
                string clasePadre = this._clasePadre;
                if (clasePadre.Equals("ui_movlote"))
                {
                    ((ui_movlote)FormPadre)._TextBoxActivo.Text = codigo;
                }
                else
                {
                    if (clasePadre.Equals("ui_lotegen"))
                    {
                        ((ui_lotegen)FormPadre)._TextBoxActivo.Text = codigo;
                    }
                }
                
            }
            this.DialogResult = DialogResult.OK;
            Close();
            return true;
        }

        private void ui_Lista()
        {
            Funciones funciones = new Funciones();
            string query = this._query;
            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();
            try
            {
                using (SqlDataAdapter myDataAdapter = new SqlDataAdapter(query, conexion))
                {
                    DataSet myDataSet = new DataSet();
                    myDataAdapter.Fill(myDataSet, "tblData");
                    funciones.formatearDataGridView(dgvdetalle);
                    dgvdetalle.DataSource = myDataSet.Tables["tblData"];
                    dgvdetalle.Columns[0].HeaderText = "Lote";
                    dgvdetalle.Columns[1].HeaderText = "Fecha de Ingreso a Almacén";
                  
                    dgvdetalle.Columns[0].Width = 150;
                    dgvdetalle.Columns[1].Width = 100;
                 

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            conexion.Close();
        }

        

        private void dgvdetalle_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void ui_viewlotes_Load(object sender, EventArgs e)
        {
            ui_Lista();
        }
    }
}
