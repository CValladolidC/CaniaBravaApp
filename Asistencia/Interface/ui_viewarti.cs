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
    public partial class ui_viewarti : CaniaBrava.ui_form
    {
        private Form FormPadre;
        public string _codcia;
        public string _clasePadre;
        public string _condicionAdicional;
        public string _cadenaBusqueda;

        public Form _FormPadre
        {
            get { return FormPadre; }
            set { FormPadre = value; }
        }

        public ui_viewarti()
        {
            InitializeComponent();
        }

        private void ui_viewarti_Load(object sender, EventArgs e)
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
                string codigo = row.Cells["codarti"].Value.ToString();
                string descodigo = row.Cells["desarti"].Value.ToString();

                string clasePadre = this._clasePadre;

                if (clasePadre.Equals("ui_almovd"))
                {
                    ((ui_almovd)FormPadre)._TextBoxActivo.Text = codigo;
                }
                else
                {
                    if (clasePadre.Equals("ui_almovdps"))
                    {
                        ((ui_almovdps)FormPadre)._TextBoxActivo.Text = codigo;
                    }
                    else
                    {
                        if (clasePadre.Equals("ui_movarti"))
                        {
                            ((ui_movarti)FormPadre)._TextBoxActivo.Text = codigo;
                        }
                        else
                        {
                            if (clasePadre.Equals("ui_updfactura"))
                            {
                                ((ui_updfactura)FormPadre)._TextBoxActivo.Text = codigo;
                            }
                            else
                            {
                                if (clasePadre.Equals("ui_updguiaremi"))
                                {
                                    ((ui_updguiaremi)FormPadre)._TextBoxActivo.Text = codigo;
                                }
                                else
                                {
                                    if (clasePadre.Equals("ui_delalarti"))
                                    {
                                        ((ui_delalarti)FormPadre)._TextBoxActivo.Text = codigo;
                                    }
                                    else
                                    {
                                        if (clasePadre.Equals("ui_solalmad"))
                                        {
                                            ((ui_solalmad)FormPadre)._TextBoxActivo.Text = codigo;
                                        }
                                        else
                                        {
                                            if (clasePadre.Equals("ui_almovdpscenpro"))
                                            {
                                                ((ui_almovdpscenpro)FormPadre)._TextBoxActivo.Text = codigo;
                                            }
                                            else
                                            {
                                                if (clasePadre.Equals("ui_movlote"))
                                                {
                                                    ((ui_movlote)FormPadre)._TextBoxActivo.Text = codigo;
                                                }
                                                else
                                                {
                                                    if (clasePadre.Equals("ui_movprovee"))
                                                    {
                                                        ((ui_movprovee)FormPadre)._TextBoxActivo.Text = codigo;
                                                    }
                                                    else
                                                    {
                                                        if (clasePadre.Equals("ui_stkalmacenes"))
                                                        {
                                                            ((ui_stkalmacenes)FormPadre)._TextBoxActivo.Text = codigo;
                                                        }
                                                        else
                                                        {
                                                            if (clasePadre.Equals("ui_kardex"))
                                                            {
                                                                ((ui_kardex)FormPadre)._TextBoxActivo.Text = codigo;
                                                            }
                                                            else
                                                            {
                                                                if (clasePadre.Equals("ui_almovdpsmulticenpro"))
                                                                {
                                                                    ((ui_almovdpsmulticenpro)FormPadre)._TextBoxActivo.Text = codigo;
                                                                }
                                                                else
                                                                {
                                                                    if (clasePadre.Equals("ui_repsunat"))
                                                                    {
                                                                        ((ui_repsunat)FormPadre)._TextBoxActivo.Text = codigo;
                                                                        ((ui_repsunat)FormPadre)._LabelActivo.Text = descodigo;
                                                                    }
                                                                }
                                                            }
                                                        }
                                                    }
                                                }

                                            }
                                        }
                                    }
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
            string codcia = this._codcia;
            string condicionAdicional = this._condicionAdicional;
            string condicionBusqueda = string.Empty;
            Funciones funciones = new Funciones();

            string query = " Select A.codarti,A.desarti,A.unidad ";
            query = query + " from alarti A ";
            query = query + " where A.desarti like '%" + @buscar + "%' ";
            query = query + condicionAdicional + " order by A.desarti asc;";

            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();

            try
            {
                using (SqlDataAdapter myDataAdapter = new SqlDataAdapter(query, conexion))
                {
                    DataSet myDataSet = new DataSet();
                    myDataAdapter.Fill(myDataSet, "tblarti");
                    funciones.formatearDataGridView(dgvdetalle);
                    dgvdetalle.DataSource = myDataSet.Tables["tblarti"];
                    dgvdetalle.Columns[0].HeaderText = "Código";
                    dgvdetalle.Columns[1].HeaderText = "Nombre Descriptivo";
                    dgvdetalle.Columns[2].HeaderText = "Unidad";
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

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }
    }
}