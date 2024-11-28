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
    public partial class ui_buscarcalplan : Form
    {
        private Form FormPadre;

        string idtipoper;
        string idcia;
        string idtipocal;
        string clasePadre;

        protected override bool ProcessCmdKey(ref System.Windows.Forms.Message msg, System.Windows.Forms.Keys keyData)
        {
            if ((!dgvdetalle.Focused))
                return base.ProcessCmdKey(ref msg, keyData);
            if (keyData != Keys.Enter)
                return base.ProcessCmdKey(ref msg, keyData);

            if (dgvdetalle.RowCount > 0)
            {
                DataGridViewRow row = dgvdetalle.CurrentRow;
                string codigo = Convert.ToString(row.Cells["messem"].Value) + '/' + Convert.ToString(row.Cells["anio"].Value);
                string clasePadre = this.clasePadre;

                if (clasePadre.Equals("ui_destajo"))
                {
                    ((ui_destajo)FormPadre)._TextBoxActivo.Text = codigo;
                }
                else
                {
                    if (clasePadre.Equals("ui_planillaretenciones"))
                    {
                        ((ui_planillaretenciones)FormPadre)._TextBoxActivo.Text = codigo;
                    }
                    else
                    {
                        if (clasePadre.Equals("ui_resumendestajoperiodo"))
                        {
                            ((ui_resumendestajoperiodo)FormPadre)._TextBoxActivo.Text = codigo;
                        }
                        else
                        {
                            if (clasePadre.Equals("ui_datareten"))
                            {
                                ((ui_datareten)FormPadre)._TextBoxActivo.Text = codigo;
                            }
                            else
                            {
                                if (clasePadre.Equals("ui_destajoPeriodo"))
                                {
                                    ((ui_destajoPeriodo)FormPadre)._TextBoxActivo.Text = codigo;
                                }
                                else
                                {
                                    if (clasePadre.Equals("ui_emireten_partediario"))
                                    {
                                        ((ui_emireten_partediario)FormPadre)._TextBoxActivo.Text = codigo;
                                    }
                                    else
                                    {
                                        if (clasePadre.Equals("ui_rescuarta"))
                                        {
                                            ((ui_rescuarta)FormPadre)._TextBoxActivo.Text = codigo;
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

        public void setValores(string idtipoper, string idcia, string clasePadre, string idtipocal)
        {
            this.idcia = idcia;
            this.idtipoper = idtipoper;
            this.idtipocal = idtipocal;
            this.clasePadre = clasePadre;
        }

        public Form _FormPadre
        {
            get { return FormPadre; }
            set { FormPadre = value; }
        }

        public ui_buscarcalplan()
        {
            InitializeComponent();
        }

        private void ui_buscarcalplan_Load(object sender, EventArgs e)
        {
            ui_ListaCal();
        }

        private void ui_ListaCal()
        {
            Funciones funciones = new Funciones();
            string idcia = this.idcia;
            string idtipoper = this.idtipoper;
            string idtipocal = this.idtipocal;

            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();
            string query = "select A.messem,A.anio,A.fechaini,A.fechafin from calplan A where A.idcia='" + @idcia + "' and A.idtipoper='" + @idtipoper + "' and A.idtipocal='" + @idtipocal + "' order by A.messem desc,A.anio desc;";

            try
            {
                using (SqlDataAdapter myDataAdapter = new SqlDataAdapter(query, conexion))
                {
                    DataSet myDataSet = new DataSet();
                    myDataAdapter.Fill(myDataSet, "tblCalPlan");
                    funciones.formatearDataGridView(dgvdetalle);

                    dgvdetalle.DataSource = myDataSet.Tables["tblCalPlan"];
                    dgvdetalle.Columns[0].HeaderText = "Sem./Mes";
                    dgvdetalle.Columns[1].HeaderText = "Año";
                    dgvdetalle.Columns[2].HeaderText = "Fecha Inicio";
                    dgvdetalle.Columns[3].HeaderText = "Fecha Fin";


                    dgvdetalle.Columns[0].Width = 80;
                    dgvdetalle.Columns[1].Width = 80;
                    dgvdetalle.Columns[2].Width = 120;
                    dgvdetalle.Columns[3].Width = 120;

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
    }
}