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
    public partial class ui_buscarperplan : Form
    {
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

        public ui_buscarperplan()
        {
            InitializeComponent();
        }

        public void setValoresBuscarPerPlan(string idtipoper, string idcia, string clasePadre, string estadoTrabajador, string cadenaBusqueda, string condicionAdicional)
        {
            this.idcia = idcia;
            this.idtipoper = idtipoper;
            this.condicionAdicional = condicionAdicional;
            this.clasePadre = clasePadre;
            this.estadoTrabajador = estadoTrabajador;
            this.cadenaBusqueda = cadenaBusqueda;
        }

        public void setValoresBuscarPerPlan(string clasePadre, string cadenaBusqueda)
        {
            this.clasePadre = clasePadre;
            //this.estadoTrabajador = estadoTrabajador;
            this.cadenaBusqueda = cadenaBusqueda;
        }

        public void setValoresBuscarPerPlan2(string idcia, string clasePadre, string cadenaBusqueda, string condicionAdicional)
        {
            this.idcia = idcia;
            this.condicionAdicional = condicionAdicional;
            this.clasePadre = clasePadre;
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
                string codigo = Convert.ToString(row.Cells["idperplan"].Value);
                string clasePadre = this.clasePadre;

                if (clasePadre.Equals("ui_descjudicial"))
                {
                    ((ui_descjudicial)FormPadre)._TextBoxActivo.Text = codigo;
                }
                else
                {
                    if (clasePadre.Equals("ui_upddatosplanilla"))
                    {
                        ((ui_upddatosplanilla)FormPadre)._TextBoxActivo.Text = codigo;
                    }
                    else
                        if (clasePadre.Equals("ui_updpresper"))
                    {
                        ((ui_updpresper)FormPadre)._TextBoxActivo.Text = codigo;
                    }
                    else
                    {
                        if (clasePadre.Equals("ui_updconfijos"))
                        {
                            ((ui_updconfijos)FormPadre)._TextBoxActivo.Text = codigo;
                        }
                        else
                        {
                            if (clasePadre.Equals("ui_Boleta"))
                            {
                                ((ui_Boleta)FormPadre)._TextBoxActivo.Text = codigo;
                            }
                            else
                            {
                                if (clasePadre.Equals("ui_upddestajo"))
                                {
                                    ((ui_upddestajo)FormPadre)._TextBoxActivo.Text = codigo;
                                }
                                else
                                {
                                    if (clasePadre.Equals("ui_ConceptosCalculados"))
                                    {
                                        ((ui_ConceptosCalculados)FormPadre)._TextBoxActivo.Text = codigo;
                                    }
                                    else
                                    {
                                        if (clasePadre.Equals("ui_EmisionDestajoPorTrab_Periodo"))
                                        {
                                            ((ui_EmisionDestajoPorTrab_Periodo)FormPadre)._TextBoxActivo.Text = codigo;
                                        }
                                        else
                                        {
                                            if (clasePadre.Equals("ui_UpdQuiCat"))
                                            {
                                                ((ui_UpdQuiCat)FormPadre)._TextBoxActivo.Text = codigo;
                                            }
                                            else
                                            {
                                                if (clasePadre.Equals("ui_CalculaPlanPersonal"))
                                                {
                                                    ((ui_CalculaPlanPersonal)FormPadre)._TextBoxActivo.Text = codigo;
                                                }
                                                else
                                                {
                                                    if (clasePadre.Equals("ui_BoletaWin"))
                                                    {
                                                        ((ui_BoletaWin)FormPadre)._TextBoxActivo.Text = codigo;
                                                    }
                                                    else
                                                    {
                                                        if (clasePadre.Equals("ui_upddestajoplan"))
                                                        {
                                                            ((ui_upddestajoplan)FormPadre)._TextBoxActivo.Text = codigo;
                                                        }
                                                        else
                                                        {
                                                            if (clasePadre.Equals("ui_updgratifica"))
                                                            {
                                                                ((ui_updgratifica)FormPadre)._TextBoxActivo.Text = codigo;
                                                            }
                                                            else
                                                            {
                                                                if (clasePadre.Equals("ui_updregvac"))
                                                                {
                                                                    ((ui_updregvac)FormPadre)._TextBoxActivo.Text = codigo;
                                                                }
                                                                else
                                                                {
                                                                    if (clasePadre.Equals("ui_historialvaca"))
                                                                    {
                                                                        ((ui_historialvaca)FormPadre)._TextBoxActivo.Text = codigo;
                                                                    }
                                                                    else
                                                                    {
                                                                        if (clasePadre.Equals("ui_BoletaxEmpleadorxRango"))
                                                                        {
                                                                            ((ui_BoletaxEmpleadorxRango)FormPadre)._TextBoxActivo.Text = codigo;
                                                                        }
                                                                        else
                                                                        {
                                                                            if (clasePadre.Equals("ui_plantrab"))
                                                                            {
                                                                                ((ui_plantrab)FormPadre)._TextBoxActivo.Text = codigo;
                                                                            }
                                                                            else
                                                                            {
                                                                                if (clasePadre.Equals("ui_updbonificacion"))
                                                                                {
                                                                                    ((ui_updbonificacion)FormPadre)._TextBoxActivo.Text = codigo;
                                                                                }
                                                                                else
                                                                                {
                                                                                    if (clasePadre.Equals("reporte_subsidiados"))
                                                                                    {
                                                                                        ((reporte_subsidiados)FormPadre)._TextBoxActivo.Text = codigo;
                                                                                    }
                                                                                    else
                                                                                    {
                                                                                        if (clasePadre.Equals("ui_updinvitados"))
                                                                                        {
                                                                                            ((ui_updinvitados)FormPadre)._TextBoxActivo.Text = codigo;
                                                                                        }
                                                                                        else
                                                                                        {
                                                                                            if (clasePadre.Equals("ui_updinvitadosMasivo"))
                                                                                            {
                                                                                                ((ui_updinvitadosMasivo)FormPadre)._TextBoxActivo.Text = codigo;
                                                                                            }
                                                                                            else
                                                                                            {
                                                                                                if (clasePadre.Equals("ui_rep_comedor"))
                                                                                                {
                                                                                                    ((ui_rep_comedor)FormPadre)._TextBoxActivo.Text = codigo;
                                                                                                }
                                                                                                else
                                                                                                {
                                                                                                    if (clasePadre.Equals("ui_insinvitados"))
                                                                                                    {
                                                                                                        ((ui_insinvitados)FormPadre)._TextBoxActivo.Text = codigo;
                                                                                                    }
                                                                                                    else
                                                                                                    {
                                                                                                        if (clasePadre.Equals("ui_updregdescanso"))
                                                                                                        {
                                                                                                            ((ui_updregdescanso)FormPadre)._TextBoxActivo.Text = codigo;
                                                                                                        }
                                                                                                        else
                                                                                                        {
                                                                                                            if (clasePadre.Equals("ui_historialdescanso"))
                                                                                                            {
                                                                                                                ((ui_historialdescanso)FormPadre)._TextBoxActivo.Text = codigo;
                                                                                                            }
                                                                                                            else
                                                                                                            {
                                                                                                                if (clasePadre.Equals("ui_rep_asistencia"))
                                                                                                                {
                                                                                                                    ((ui_rep_asistencia)FormPadre)._TextBoxActivo.Text = codigo;
                                                                                                                }
                                                                                                                else
                                                                                                                {
                                                                                                                    if (clasePadre.Equals("ui_upd_asistencia"))
                                                                                                                    {
                                                                                                                        ((ui_upd_asistencia)FormPadre)._TextBoxActivo.Text = codigo;
                                                                                                                    }
                                                                                                                    else
                                                                                                                    {
                                                                                                                        if (clasePadre.Equals("ui_updcomensales"))
                                                                                                                        {
                                                                                                                            ((ui_updcomensales)FormPadre)._TextBoxActivo.Text = codigo;
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

        public void ui_LoadPerPlan(bool x)
        {
            string idtipoper = this.idtipoper;
            string idcia = this.idcia;
            string clasePadre = this.clasePadre;
            string estadoTrabajador = this.estadoTrabajador;
            string condicionAdicional = this.condicionAdicional;
            string cadenaBusqueda = this.cadenaBusqueda;
            ui_ListaPerPlan(idcia, idtipoper, estadoTrabajador, cadenaBusqueda, condicionAdicional, x);
        }

        private void ui_buscarperplan_Load(object sender, EventArgs e)
        {
            txtBuscar.Clear();
            txtBuscar.Focus();
        }

        private void ui_ListaPerPlan(string idcia, string idtipoper, string estadoTrabajador, string cadenaBusqueda, string condicionAdicional, bool x)
        {
            string condicionBusqueda = string.Empty;
            string condicionIdTipoPer = string.Empty;
            string condicionEstadoTrabajador = string.Empty;

            Funciones funciones = new Funciones();

            if (!x)
            {
                if (idtipoper != null && idtipoper != string.Empty)
                {
                    condicionIdTipoPer = " and A.idtipoper='" + @idtipoper + "' ";
                }

                if (cadenaBusqueda != string.Empty)
                {
                    condicionBusqueda = " and rtrim(A.apepat)+' '+rtrim(A.apemat)+', '+rtrim(A.nombres) like '%" + @cadenaBusqueda + "%' ";
                }

                if (estadoTrabajador != null && estadoTrabajador != string.Empty)
                {
                    condicionEstadoTrabajador = " and G.stateperlab='" + estadoTrabajador + "' ";
                }
            }

            string query = "Select A.idperplan,B.cortotipoper,C.Parm1maesgen as cortotipodoc,A.nrodoc,";
            query += "rtrim(A.apepat)+' '+rtrim(A.apemat)+', '+rtrim(A.nombres) as nombre,";
            query += " G.fechaini,G.fechafin,A.idcia from perplan_historia A left join tipoper B on A.idtipoper=B.idtipoper ";
            query += "left join maesgen C on C.idmaesgen='002' and A.tipdoc=C.clavemaesgen ";
            query += "left join view_perlab F on A.idcia=F.idcia collate Modern_Spanish_CI_AI and A.idperplan=F.idperplan collate Modern_Spanish_CI_AI ";
            query += "left join perlab G on F.idcia=G.idcia and F.idperplan=G.idperplan and F.idperlab=G.idperlab ";
            string idcia_ = "1 = 1";
            if (idcia != null) { idcia_ = "A.idcia = '" + @idcia + "' "; }
            query += "where " + idcia_ + condicionIdTipoPer + condicionEstadoTrabajador + condicionBusqueda + condicionAdicional + " order by idperplan asc;";

            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();

            try
            {
                using (SqlDataAdapter myDataAdapter = new SqlDataAdapter(query, conexion))
                {
                    DataSet myDataSet = new DataSet();
                    myDataAdapter.Fill(myDataSet, "tblPerPlan");
                    funciones.formatearDataGridView(dgvdetalle);
                    dgvdetalle.DataSource = myDataSet.Tables["tblPerPlan"];
                    dgvdetalle.Columns[0].HeaderText = "Cód.Int.";
                    dgvdetalle.Columns[1].HeaderText = "Nomina";
                    dgvdetalle.Columns[2].HeaderText = "Doc.Ident.";
                    dgvdetalle.Columns[3].HeaderText = "Nro.Doc.";
                    dgvdetalle.Columns[4].HeaderText = "Apellidos y Nombres";
                    dgvdetalle.Columns[5].HeaderText = "F.Ini.Labores";
                    dgvdetalle.Columns[6].HeaderText = "F.Fin.Labores";
                    dgvdetalle.Columns["idcia"].Visible = false;
                    dgvdetalle.Columns[5].Visible = false;
                    dgvdetalle.Columns[6].Visible = false;
                    dgvdetalle.Columns[0].Width = 60;
                    dgvdetalle.Columns[1].Width = 100;
                    dgvdetalle.Columns[2].Width = 60;
                    dgvdetalle.Columns[3].Width = 70;
                    dgvdetalle.Columns[4].Width = 280;

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
            ui_ListaPerPlan(idcia, idtipoper, estadoTrabajador, cadenaBusqueda, condicionAdicional, false);
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