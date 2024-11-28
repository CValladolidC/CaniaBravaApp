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
    public partial class ui_confmotivos : Form
    {
        string _operacion;
        string _idtipoplan;
        string _idconplan;
        string _idtipocal;
        string _idclascol;
        string _desconplan;
        string _tipo;
        string _idcia;

        private TextBox TextBoxActivo;

        public TextBox _TextBoxActivo
        {
            get { return TextBoxActivo; }
            set { TextBoxActivo = value; }
        }

        private Form FormPadre;

        public Form _FormPadre
        {
            get { return FormPadre; }
            set { FormPadre = value; }
        }

        public ui_confmotivos()
        {
            InitializeComponent();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ui_confmotivos_Load(object sender, EventArgs e)
        {

            string idconplan = this._idconplan;
            string idtipoplan = this._idtipoplan;
            string idtipocal = this._idtipocal;
            string idcia = this._idcia;
            ui_ListaDetConPlan(idconplan, idtipoplan, idcia, idtipocal);

        }

        public void ui_ListaComboBox()
        {
            Funciones funciones = new Funciones();
            string query;
            string idtipoplan = this._idtipoplan;
            string idclascol = this._idclascol;

            query = "SELECT idtipoper as clave,destipoper as descripcion FROM tipoper order by 1 asc;";
            funciones.listaComboBox(query, cmbTipoPer, "");

            query = "SELECT idcolplan as clave,descolplan as descripcion FROM colplan where idtipoplan='" + @idtipoplan + "' and idclascol='" + @idclascol + "' order by 1 asc;";
            funciones.listaComboBox(query, cmbColPlan, "B");

            query = "SELECT idconpdt as clave,desconpdt as descripcion FROM conpdt order by 1 asc;";
            funciones.listaComboBox(query, cmbConceptoPdt, "B");

        }

        public void ui_asignaparametros(string idtipoplan, string idclascol, string idconplan, string desconplan, string tipo, string idcia, string idtipocal)
        {
            this._idclascol = idclascol;
            this._idconplan = idconplan;
            this._idtipoplan = idtipoplan;
            this._desconplan = desconplan;
            this._idtipocal = idtipocal;
            this._tipo = tipo;
            this._idcia = idcia;
            this.Text = "Configuración del Concepto de Planilla : " + idconplan + " - " + desconplan;
        }

        public void ui_NewDetConPlan(string idtipoplan, string idclascol, string idtipocal)
        {
            Controles controles = new Controles();
            this._operacion = "AGREGAR";
            this._idtipoplan = idtipoplan;
            this._idclascol = idclascol;
            this._idtipocal = idtipocal;

            controles.CleanerControlSimple(this);
            controles.CleanerControlParent(grp1);
            controles.CleanerControlParent(grp2);
            controles.CleanerControlParent(grp3);
            controles.CleanerControlParent(grp4);
            controles.CleanerControlParent(grp5);
            controles.CleanerControlParent(grp6);


            if (this._tipo.Equals("C"))
            {
                chkDestajo.Enabled = false;
                chkDestajo.Checked = false;
                chkQuinta.Enabled = true;
                chkQuinta.Checked = false;
            }
            else
            {
                chkDestajo.Enabled = true;
                chkQuinta.Enabled = false;
                chkQuinta.Checked = false;
            }
            chkGeneracion.Enabled = true;
            chkProy5taCat.Enabled = true;
            chkAsegurable.Enabled = true;
            chkCero.Enabled = true;
            chkCero.Checked = false;
            chkPresta.Enabled = true;
            chkPresta.Checked = false;
            chkRemProm.Checked = false;
            if (this._idclascol.Equals("I"))
            {
                chkRemProm.Enabled = true;
            }
            else
            {
                chkRemProm.Enabled = false;
            }

            radioButtonSi.Checked = true;
            radioButtonNo.Checked = false;
            radioButtonSi.Enabled = true;
            radioButtonNo.Enabled = true;

            controles.EnabledControlParent(grp1);
            controles.EnabledControlParent(grp2);
            controles.EnabledControlParent(grp3);
            controles.EnabledControlParent(grp4);
            controles.EnabledControlParent(grp5);
            controles.EnabledControlParent(grp6);
            ui_ListaComboBox();
            cmbEstado.Text = "V    VIGENTE";
            cmbTipoPer.Focus();
        }

        private void btnNuevoEstAne_Click(object sender, EventArgs e)
        {
            string idtipoplan = this._idtipoplan;
            string idclascol = this._idclascol;
            string idtipocal = this._idtipocal;
            ui_NewDetConPlan(idtipoplan, idclascol, idtipocal);
        }

        private void chkGeneracion_CheckedChanged(object sender, EventArgs e)
        {
            Controles controles = new Controles();

            controles.CleanerControlParent(grp2);

            if (chkGeneracion.Checked == true)
            {
                controles.EnabledControlParent(grp2);
                btnFormula.Enabled = true;
                cmbTopeMin.Text = "NO";
                cmbTopeMax.Text = "NO";

            }
            else
            {
                controles.DisabledControlParent(grp2);
                btnFormula.Enabled = false;
                cmbTopeMin.Text = "  ";
                cmbTopeMax.Text = "  ";
            }


            txtValorTopeMax.Enabled = false;
            txtValorTopeMin.Enabled = false;
            btnFormulaTopeMin.Enabled = false;
            btnFormulaTopeMax.Enabled = false;

        }

        private void cmbTopeMin_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbTopeMin.Text.Trim().Equals("SI"))
            {

                txtValorTopeMin.Enabled = true;
                btnFormulaTopeMin.Enabled = true;
                txtValorTopeMin.Focus();

            }
            else
            {
                txtValorTopeMin.Clear();
                txtFormulaTopeMin.Clear();
                txtValorTopeMin.Enabled = false;
                btnFormulaTopeMin.Enabled = false;

            }
        }

        private void cmbTopeMax_SelectionChangeCommitted(object sender, EventArgs e)
        {

        }

        private void cmbTopeMax_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbTopeMax.Text.Trim().Equals("SI"))
            {
                txtValorTopeMax.Enabled = true;
                btnFormulaTopeMax.Enabled = true;
                txtValorTopeMax.Focus();
            }
            else
            {
                txtValorTopeMax.Clear();
                txtFormulaTopeMax.Clear();
                txtValorTopeMax.Enabled = false;
                btnFormulaTopeMax.Enabled = false;
            }
        }

        private void btnGrabar_Click(object sender, EventArgs e)
        {
            string valorValida = "G";


            if (cmbTipoPer.Text == string.Empty && valorValida == "G")
            {
                valorValida = "B";
                MessageBox.Show("No ha seleccionado Tipo de Personal", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cmbTipoPer.Focus();
            }

            if (txtDesBol.Text == string.Empty && valorValida == "G")
            {
                valorValida = "B";
                MessageBox.Show("No ha ingresado descripción en Boleta de Pago", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtDesBol.Focus();
            }

            /// INICIO GENERACION AUTOMATICA ///
            if (chkGeneracion.Checked)
            {

                if (txtFormula.Text == string.Empty && valorValida == "G")
                {
                    valorValida = "B";
                    MessageBox.Show("No ha construido Fórmula para el cálculo automático", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    btnFormula.Focus();
                }

                if (cmbTopeMin.Text.Trim().Equals("SI"))
                {

                    if (txtValorTopeMin.Text == string.Empty && valorValida == "G")
                    {
                        valorValida = "B";
                        MessageBox.Show("No definido el Tope Mínimo", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txtValorTopeMin.Focus();
                    }

                    try
                    {
                        float topemin = float.Parse(txtValorTopeMin.Text);
                    }
                    catch (FormatException)
                    {
                        valorValida = "B";
                        MessageBox.Show("Tope Mínimo con formato inválido", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txtValorTopeMin.Clear();
                        txtValorTopeMin.Focus();
                    }

                    if (txtFormulaTopeMin.Text == string.Empty && valorValida == "G")
                    {
                        valorValida = "B";
                        MessageBox.Show("No ha construido Fórmula para Tope Mínimo para el cálculo automático", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        btnFormulaTopeMin.Focus();
                    }

                }

                if (cmbTopeMax.Text.Trim().Equals("SI"))
                {

                    if (txtValorTopeMax.Text == string.Empty && valorValida == "G")
                    {
                        valorValida = "B";
                        MessageBox.Show("No definido el Tope Máximo", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txtValorTopeMax.Focus();
                    }

                    try
                    {
                        float topemax = float.Parse(txtValorTopeMax.Text);
                    }
                    catch (FormatException)
                    {
                        valorValida = "B";
                        MessageBox.Show("Tope Máximo con formato inválido", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txtValorTopeMax.Clear();
                        txtValorTopeMax.Focus();
                    }

                    if (txtFormulaTopeMax.Text == string.Empty && valorValida == "G")
                    {
                        valorValida = "B";
                        MessageBox.Show("No ha construido Fórmula para Tope Máximo para el cálculo automático", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        btnFormulaTopeMax.Focus();
                    }

                }

            }

            /// FIN GENERACION AUTOMATICA ////

            string con5tacat;

            if (chkQuinta.Checked)
            {
                con5tacat = "S";
            }
            else
            {
                con5tacat = "N";
            }

            string automatico;

            if (chkGeneracion.Checked)
            {
                automatico = "S";
            }
            else
            {
                automatico = "N";
            }


            if (this._tipo.Equals("C") || this._tipo.Equals("T"))
            {
                if (automatico.Equals("N"))
                {
                    if (con5tacat.Equals("N"))
                    {
                        valorValida = "B";
                        MessageBox.Show("Todo concepto CONFORMANTE o TOTALIZADOR debe de poseer Generación Automática mediante Fórmula", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }


            if (txtTipoAneDe.Text != string.Empty && valorValida == "G")
            {
                if (txtAneDe.Text == string.Empty)
                {
                    valorValida = "B";
                    MessageBox.Show("No ha ingresado Anexo para Cta. Debe", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtAneDe.Focus();
                }
            }

            if (txtTipoAneRefDe.Text != string.Empty && valorValida == "G")
            {
                if (txtAneRefDe.Text == string.Empty)
                {
                    valorValida = "B";
                    MessageBox.Show("No ha ingresado Anexo Ref. para Cta. Debe", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtAneRefDe.Focus();
                }
            }

            if (txtTipoAneHa.Text != string.Empty && valorValida == "G")
            {
                if (txtAneHa.Text == string.Empty)
                {
                    valorValida = "B";
                    MessageBox.Show("No ha ingresado Anexo para Cta. Haber", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtAneHa.Focus();
                }
            }

            if (txtTipoAneRefHa.Text != string.Empty && valorValida == "G")
            {
                if (txtAneRefHa.Text == string.Empty)
                {
                    valorValida = "B";
                    MessageBox.Show("No ha ingresado Anexo Ref. para Cta. Haber", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtAneRefHa.Focus();
                }
            }


            if (txtAneDe.Text != string.Empty && valorValida == "G")
            {
                if (txtTipoAneDe.Text == string.Empty)
                {
                    valorValida = "B";
                    MessageBox.Show("No ha ingresado Tipo de Anexo para Cta. Debe", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtTipoAneDe.Focus();
                }
            }

            if (txtAneRefDe.Text != string.Empty && valorValida == "G")
            {
                if (txtTipoAneRefDe.Text == string.Empty)
                {
                    valorValida = "B";
                    MessageBox.Show("No ha ingresado Tipo de Anexo Ref. para Cta. Debe", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtTipoAneRefDe.Focus();
                }
            }

            if (txtAneHa.Text != string.Empty && valorValida == "G")
            {
                if (txtTipoAneHa.Text == string.Empty)
                {
                    valorValida = "B";
                    MessageBox.Show("No ha ingresado Tipo de Anexo para Cta. Haber", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtTipoAneHa.Focus();
                }
            }

            if (txtAneRefHa.Text != string.Empty && valorValida == "G")
            {
                if (txtTipoAneRefHa.Text == string.Empty)
                {
                    valorValida = "B";
                    MessageBox.Show("No ha ingresado Tipo de Anexo Ref. para Cta. Haber", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtTipoAneRefHa.Focus();
                }
            }


            string destajo;

            if (chkDestajo.Checked)
            {
                destajo = "SI";
            }
            else
            {
                destajo = "NO";
            }

            if (this._tipo.Equals("C"))
            {
                if (destajo.Equals("SI"))
                {
                    valorValida = "B";
                    MessageBox.Show("No se puede marcar como concepto de Destajo a un concepto CONFORMANTE", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            if (this._tipo.Equals("O"))
            {
                if (con5tacat.Equals("S"))
                {
                    valorValida = "B";
                    MessageBox.Show("No se puede marcar como concepto para el cálculo de Quinta Categoría a un concepto OPCIONAL", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            string asegurable;

            if (chkAsegurable.Checked)
            {
                asegurable = "S";
            }
            else
            {
                asegurable = "N";
            }


            string proy5tacat;

            if (chkProy5taCat.Checked)
            {
                proy5tacat = "S";
            }
            else
            {
                proy5tacat = "N";
            }

            if (con5tacat.Equals("S"))
            {
                if (proy5tacat.Equals("S"))
                {
                    valorValida = "B";
                    MessageBox.Show("No se puede marcar como concepto proyectable para el cálculo de Quinta Categoría a un concepto perteneciente a Quinta Categoría", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }



            string remprom;
            if (chkRemProm.Checked)
            {
                remprom = "S";
            }
            else
            {
                remprom = "N";
            }
            if (this._idclascol != "I" && remprom == "S")
            {
                valorValida = "B";
                MessageBox.Show("Concepto para Remuneración Promedio sólo es válido para INGRESOS", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            string regcero;
            if (chkCero.Checked)
            {
                regcero = "S";
            }
            else
            {
                regcero = "N";
            }

            string presta;
            if (chkPresta.Checked)
            {
                presta = "S";
            }
            else
            {
                presta = "N";
            }


            if (valorValida.Equals("G"))
            {

                Funciones funciones = new Funciones();
                string operacion = this._operacion;
                string idconplan = this._idconplan;
                string idtipoplan = this._idtipoplan;
                string idtipocal = this._idtipocal;
                string idtipoper = funciones.getValorComboBox(cmbTipoPer, 1);
                string desboleta = txtDesBol.Text.Trim();
                string idcolplan = funciones.getValorComboBox(cmbColPlan, 3);
                string pdt = funciones.getValorComboBox(cmbConceptoPdt, 4);
                string formula = txtFormula.Text.Trim();
                string poseetmin = cmbTopeMin.Text.Trim();
                float topemin;
                float topemax;
                try
                {
                    topemin = float.Parse(txtValorTopeMin.Text);
                    topemax = float.Parse(txtValorTopeMax.Text);
                }

                catch (FormatException)
                {
                    topemin = 0;
                    topemax = 0;
                }

                string si_topemin = txtFormulaTopeMin.Text.Trim();
                string poseetmax = cmbTopeMax.Text.Trim();
                string si_topemax = txtFormulaTopeMax.Text.Trim();
                string imprime;
                if (radioButtonSi.Checked)
                {
                    imprime = "SI";
                }
                else
                {
                    imprime = "NO";
                }

                string ctadebe = txtDebe.Text.Trim();
                string ctahaber = txtHaber.Text.Trim();
                string tipanede;
                string anede;
                string tipanerefde;
                string anerefde;
                string tipaneha;
                string aneha;
                string tipanerefha;
                string anerefha;

                if (ctadebe == string.Empty)
                {
                    tipanede = string.Empty;
                    anede = string.Empty;
                    tipanerefde = string.Empty;
                    anerefde = string.Empty;
                }
                else
                {
                    tipanede = txtTipoAneDe.Text;
                    anede = txtAneDe.Text;
                    tipanerefde = txtTipoAneRefDe.Text;
                    anerefde = txtAneRefDe.Text;
                }

                if (ctahaber == string.Empty)
                {
                    tipaneha = string.Empty;
                    aneha = string.Empty;
                    tipanerefha = string.Empty;
                    anerefha = string.Empty;
                }
                else
                {
                    tipaneha = txtTipoAneHa.Text;
                    aneha = txtAneHa.Text;
                    tipanerefha = txtTipoAneRefHa.Text;
                    anerefha = txtAneRefHa.Text;
                }

                string state = funciones.getValorComboBox(cmbEstado, 1);
                string idcia = this._idcia;


                DetConPlan detconplan = new DetConPlan();
                detconplan.actualizarDetConPlan(operacion, idconplan, idtipoplan, idtipoper,
                    desboleta, idcolplan, pdt, automatico, formula, poseetmin, topemin,
                    si_topemin, poseetmax, topemax, si_topemax, proy5tacat, ctadebe, ctahaber,
                    state, destajo, idcia, imprime, idtipocal, con5tacat, remprom, regcero,
                    presta, tipanede, anede, tipanerefde, anerefde, tipaneha, aneha,
                    tipanerefha, anerefha, asegurable);
                this.ui_ListaDetConPlan(idconplan, idtipoplan, idcia, idtipocal);
                Controles controles = new Controles();
                controles.CleanerControlParent(grp1);
                controles.CleanerControlParent(grp2);
                controles.CleanerControlParent(grp3);
                controles.CleanerControlParent(grp4);
                controles.CleanerControlParent(grp5);
                controles.CleanerControlParent(grp6);

                controles.DisabledControlParent(grp1);
                chkDestajo.Enabled = false;
                chkGeneracion.Enabled = false;
                chkProy5taCat.Enabled = false;
                chkRemProm.Enabled = false;
                chkCero.Enabled = false;
                chkPresta.Enabled = false;
                controles.DisabledControlParent(grp3);
                controles.DisabledControlParent(grp4);
                controles.DisabledControlParent(grp5);
                controles.DisabledControlParent(grp6);
                chkDestajo.Checked = false;
                radioButtonSi.Enabled = false;
                radioButtonNo.Enabled = false;
                radioButtonSi.Checked = false;
                radioButtonNo.Checked = false;
                chkGeneracion.Checked = false;
                chkAsegurable.Checked = false;
                chkProy5taCat.Checked = false;
                chkRemProm.Checked = false;
                chkCero.Checked = false;
                chkPresta.Checked = false;

            }



        }

        private void ui_ListaDetConPlan(string idconplan, string idtipoplan, string idcia, string idtipocal)
        {

            try
            {
                Funciones funciones = new Funciones();
                string query = " select idtipoper,desboleta,idcolplan,pdt,automatico,formula,poseetmin,poseetmax,";
                query = query + " proy5tacat,ctadebe,ctahaber,destajo,imprime from detconplan ";
                query = query + " where idtipoplan='" + @idtipoplan + "' ";
                query = query + " and idconplan='" + @idconplan + "' and idcia='" + @idcia + "' ";
                query = query + " and idtipocal='" + @idtipocal + "' order by idtipoper asc";

                SqlConnection conexion = new SqlConnection();
                conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
                conexion.Open();

                try
                {
                    using (SqlDataAdapter myDataAdapter = new SqlDataAdapter(query, conexion))
                    {
                        DataSet myDataSet = new DataSet();
                        myDataAdapter.Fill(myDataSet, "tblDetConPlan");
                        funciones.formatearDataGridView(dgvDetalle);
                        dgvDetalle.DataSource = myDataSet.Tables["tblDetConPlan"];
                        dgvDetalle.Columns[0].HeaderText = "T.P.";
                        dgvDetalle.Columns[1].HeaderText = "Descripción en Boleta de Pago";
                        dgvDetalle.Columns[2].HeaderText = "Columna en Planilla";
                        dgvDetalle.Columns[3].HeaderText = "PDT";
                        dgvDetalle.Columns[4].HeaderText = "Aut.";
                        dgvDetalle.Columns[5].HeaderText = "Fórmula";
                        dgvDetalle.Columns[6].HeaderText = "Posee Tope Mínimo?";
                        dgvDetalle.Columns[7].HeaderText = "Posee Tope Máximo?";
                        dgvDetalle.Columns[8].HeaderText = "Proy. 5ta. Cat.";
                        dgvDetalle.Columns[9].HeaderText = "Cta. Debe";
                        dgvDetalle.Columns[10].HeaderText = "Cta. Haber";
                        dgvDetalle.Columns[11].HeaderText = "Motivo de Destajo?";
                        dgvDetalle.Columns[12].HeaderText = "Imprimible?";

                        dgvDetalle.Columns[0].Width = 35;
                        dgvDetalle.Columns[1].Width = 150;
                        dgvDetalle.Columns[2].Width = 60;
                        dgvDetalle.Columns[3].Width = 50;
                        dgvDetalle.Columns[4].Width = 30;
                        dgvDetalle.Columns[5].Width = 250;
                        dgvDetalle.Columns[6].Width = 70;
                        dgvDetalle.Columns[7].Width = 70;
                        dgvDetalle.Columns[8].Width = 70;
                        dgvDetalle.Columns[9].Width = 50;
                        dgvDetalle.Columns[10].Width = 50;
                        dgvDetalle.Columns[11].Width = 70;
                        dgvDetalle.Columns[12].Width = 70;


                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }

                conexion.Close();
            }
            catch (ArgumentOutOfRangeException)
            {

            }

        }

        private void btnEditarEstAne_Click(object sender, EventArgs e)
        {

            Controles controles = new Controles();
            Funciones funciones = new Funciones();
            this._operacion = "EDITAR";
            controles.EnabledControlParent(grp1);
            chkGeneracion.Enabled = true;
            chkProy5taCat.Enabled = true;
            controles.EnabledControlParent(grp3);
            controles.EnabledControlParent(grp4);
            controles.EnabledControlParent(grp5);
            controles.EnabledControlParent(grp6);
            radioButtonSi.Enabled = true;
            radioButtonNo.Enabled = true;

            Int32 selectedCellCount =
            dgvDetalle.GetCellCount(DataGridViewElementStates.Selected);
            if (selectedCellCount > 0)
            {
                string idcia = this._idcia;
                string idtipoplan = this._idtipoplan;
                string idconplan = this._idconplan;
                string idtipocal = this._idtipocal;
                string idclascol = this._idclascol;

                string idtipoper = dgvDetalle.Rows[dgvDetalle.SelectedCells[1].RowIndex].Cells[0].Value.ToString();
                SqlConnection conexion = new SqlConnection();
                conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
                conexion.Open();
                string query = "SELECT * FROM detconplan where (idcia='" + @idcia + "' and idtipoplan='" + @idtipoplan + "' ";
                query = query + " and idtipoper='" + @idtipoper + "' and idtipocal='" + @idtipocal + "' and idconplan='" + @idconplan + "');";
                try
                {
                    SqlCommand myCommand = new SqlCommand(query, conexion);
                    SqlDataReader myReader = myCommand.ExecuteReader();

                    if (myReader.Read())
                    {
                        string idcolplan = myReader["idcolplan"].ToString();
                        string pdt = myReader["pdt"].ToString();

                        txtDesBol.Text = myReader["desboleta"].ToString();
                        cmbTopeMin.Text = myReader["poseetmin"].ToString();
                        txtValorTopeMin.Text = myReader["topemin"].ToString();
                        txtFormulaTopeMin.Text = myReader["si_topemin"].ToString();
                        cmbTopeMax.Text = myReader["poseetmax"].ToString();
                        txtValorTopeMax.Text = myReader["topemax"].ToString();
                        txtFormulaTopeMax.Text = myReader["si_topemax"].ToString();

                        txtDebe.Text = myReader["ctadebe"].ToString();
                        txtHaber.Text = myReader["ctahaber"].ToString();
                        txtTipoAneDe.Text = myReader["tipanede"].ToString();
                        txtAneDe.Text = myReader["anede"].ToString();
                        txtTipoAneRefDe.Text = myReader["tipanerefde"].ToString();
                        txtAneRefDe.Text = myReader["anerefde"].ToString();
                        txtTipoAneHa.Text = myReader["tipaneha"].ToString();
                        txtAneHa.Text = myReader["aneha"].ToString();
                        txtTipoAneRefHa.Text = myReader["tipanerefha"].ToString();
                        txtAneRefHa.Text = myReader["anerefha"].ToString();

                        ui_ListaComboBox();
                        query = "Select idtipoper as clave,destipoper as descripcion FROM tipoper where idtipoper='" + @idtipoper + "';";
                        funciones.consultaComboBox(query, cmbTipoPer);

                        query = "Select idcolplan as clave,descolplan as descripcion FROM colplan where idtipoplan='" + @idtipoplan + "' ";
                        query = query + " and idclascol='" + @idclascol + "' and idcolplan='" + @idcolplan + "';";
                        funciones.consultaComboBox(query, cmbColPlan);
                        query = "Select idconpdt as clave,desconpdt as descripcion FROM conpdt where idconpdt='" + @pdt + "'; ";
                        funciones.consultaComboBox(query, cmbConceptoPdt);


                        if (this._tipo.Equals("C"))
                        {
                            chkDestajo.Enabled = false;
                            chkDestajo.Checked = false;
                            chkQuinta.Enabled = true;
                            if (myReader["con5tacat"].Equals("S"))
                            {
                                chkQuinta.Checked = true;
                                chkProy5taCat.Checked = false;
                                chkProy5taCat.Enabled = false;
                            }
                            else
                            {
                                chkQuinta.Checked = false;
                                chkProy5taCat.Enabled = true;
                                if (myReader["proy5tacat"].Equals("S"))
                                {
                                    chkProy5taCat.Checked = true;
                                }
                                else
                                {
                                    chkProy5taCat.Checked = false;
                                }
                            }
                        }
                        else
                        {
                            chkDestajo.Enabled = true;
                            chkQuinta.Enabled = false;
                            chkQuinta.Checked = false;
                            chkProy5taCat.Enabled = true;

                            if (myReader["destajo"].Equals("SI"))
                            {
                                chkDestajo.Checked = true;
                            }
                            else
                            {
                                chkDestajo.Checked = false;
                            }

                            if (myReader["proy5tacat"].Equals("S"))
                            {
                                chkProy5taCat.Checked = true;
                            }
                            else
                            {
                                chkProy5taCat.Checked = false;
                            }
                        }

                        if (myReader["imprime"].Equals("SI"))
                        {
                            radioButtonSi.Checked = true;
                            radioButtonNo.Checked = false;
                        }
                        else
                        {
                            radioButtonSi.Checked = false;
                            radioButtonNo.Checked = true;
                        }

                        if (myReader["automatico"].Equals("S"))
                        {
                            chkGeneracion.Checked = true;
                            txtFormula.Text = myReader["formula"].ToString();
                        }
                        else
                        {
                            chkGeneracion.Checked = false;
                            txtFormula.Text = string.Empty;
                        }
                        
                        chkPresta.Enabled = true;
                        if (myReader["presta"].Equals("S"))
                        {
                            chkPresta.Checked = true;
                        }
                        else
                        {
                            chkPresta.Checked = false;
                        }

                        chkAsegurable.Enabled = true;
                        if (myReader["asegura"].Equals("S"))
                        {
                            chkAsegurable.Checked = true;
                        }
                        else
                        {
                            chkAsegurable.Checked = false;
                        }

                        chkCero.Enabled = true;
                        if (myReader["regcero"].Equals("S"))
                        {
                            chkCero.Checked = true;
                        }
                        else
                        {
                            chkCero.Checked = false;
                        }

                        if (this._idclascol.Equals("I"))
                        {
                            chkRemProm.Enabled = true;

                            if (myReader["remprom"].Equals("S"))
                            {
                                chkRemProm.Checked = true;
                            }
                            else
                            {
                                chkRemProm.Checked = false;
                            }
                        }
                        else
                        {
                            chkRemProm.Enabled = false;
                            chkRemProm.Checked = false;
                        }

                        if (myReader["state"].Equals("V"))
                        {
                            cmbEstado.Text = "V    VIGENTE";
                        }
                        else
                        {
                            cmbEstado.Text = "A    ANULADO";
                        }
                    }
                }
                catch (Exception theException)
                {
                    String errorMessage;
                    errorMessage = "Error: ";
                    errorMessage = String.Concat(errorMessage, theException.Message);
                    errorMessage = String.Concat(errorMessage, " Line: ");
                    errorMessage = String.Concat(errorMessage, theException.Source);
                    MessageBox.Show(errorMessage, "Error");
                }
                conexion.Close();
            }
            else
            {
                MessageBox.Show("No ha seleccionado registro a Editar", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void btnEliminarEstAne_Click(object sender, EventArgs e)
        {
            Int32 selectedCellCount =
            dgvDetalle.GetCellCount(DataGridViewElementStates.Selected);
            if (selectedCellCount > 0)
            {
                string idcia = this._idcia;
                string idtipoplan = this._idtipoplan;
                string idconplan = this._idconplan;
                string idtipocal = this._idtipocal;
                string idtipoper = dgvDetalle.Rows[dgvDetalle.SelectedCells[1].RowIndex].Cells[0].Value.ToString();
                string desboleta = dgvDetalle.Rows[dgvDetalle.SelectedCells[1].RowIndex].Cells[1].Value.ToString();

                DialogResult resultado = MessageBox.Show("¿Desea eliminar la configuración del concepto " + desboleta + "-" + idtipoper + "?",
                "Consulta Importante",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);
                if (resultado == DialogResult.Yes)
                {
                    DetConPlan detconplan = new DetConPlan();
                    detconplan.eliminarDetConPlan(idconplan, idtipoplan, idtipoper, idcia, idtipocal);
                    this.ui_ListaDetConPlan(idconplan, idtipoplan, idcia, idtipocal);
                }
            }
            else
            {
                MessageBox.Show("No ha seleccionado registro a Eliminar", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void btnFormula_Click(object sender, EventArgs e)
        {

            string idtipoplan = this._idtipoplan;
            string idconplan = this._idconplan;
            string desconplan = this._desconplan;
            string idtipocal = this._idtipocal;
            string formula = txtFormula.Text.Trim();
            this._TextBoxActivo = txtFormula;
            ui_constructor constructor = new ui_constructor();
            constructor._FormPadre = this;
            constructor.ui_loadform(idtipoplan, idconplan, desconplan, formula, idtipocal);
            constructor.Activate();
            constructor.BringToFront();
            constructor.ShowDialog();
            constructor.Dispose();
        }

        private void btnFormulaTopeMin_Click(object sender, EventArgs e)
        {
            string idtipoplan = this._idtipoplan;
            string idconplan = this._idconplan;
            string desconplan = this._desconplan;
            string idtipocal = this._idtipocal;
            string formula = txtFormulaTopeMin.Text.Trim();
            this._TextBoxActivo = txtFormulaTopeMin;
            ui_constructor constructor = new ui_constructor();
            constructor._FormPadre = this;
            constructor.ui_loadform(idtipoplan, idconplan, desconplan, formula, idtipocal);
            constructor.Activate();
            constructor.BringToFront();
            constructor.ShowDialog();
            constructor.Dispose();
        }

        private void btnFormulaTopeMax_Click(object sender, EventArgs e)
        {
            string idtipoplan = this._idtipoplan;
            string idconplan = this._idconplan;
            string desconplan = this._desconplan;
            string idtipocal = this._idtipocal;
            string formula = txtFormulaTopeMax.Text.Trim();
            this._TextBoxActivo = txtFormulaTopeMax;
            ui_constructor constructor = new ui_constructor();
            constructor._FormPadre = this;
            constructor.ui_loadform(idtipoplan, idconplan, desconplan, formula, idtipocal);
            constructor.Activate();
            constructor.BringToFront();
            constructor.ShowDialog();
            constructor.Dispose();
        }

        private void chkDestajo_CheckedChanged(object sender, EventArgs e)
        {
            if (chkDestajo.Checked == true)
            {
                chkGeneracion.Checked = false;
                chkGeneracion.Enabled = false;
                chkQuinta.Checked = false;
                chkQuinta.Enabled = false;
            }
            else
            {
                chkGeneracion.Enabled = true;
                chkQuinta.Enabled = true;
            }
        }

        private void chkQuinta_CheckedChanged(object sender, EventArgs e)
        {
            if (chkQuinta.Checked == true)
            {
                chkGeneracion.Checked = false;
                chkGeneracion.Enabled = false;
                chkDestajo.Checked = false;
                chkDestajo.Enabled = false;
                chkProy5taCat.Enabled = false;
                chkProy5taCat.Checked = false;
            }
            else
            {
                chkGeneracion.Enabled = true;
                chkDestajo.Enabled = true;
                chkProy5taCat.Enabled = true;
            }
        }

        private void txtDebe_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                GlobalVariables gv = new GlobalVariables();
                string idcia = gv.getValorCia();
                string query = "SELECT codcuenta as codigo,descuenta as descripcion from cuencon ";
                query = query + " where idcia='" + @idcia + "' and s_estado = 1 order by codcuenta asc;";
                this._TextBoxActivo = txtDebe;
                ui_viewmaestros ui_viewmaestros = new ui_viewmaestros();
                ui_viewmaestros.setData(query, "ui_confmotivos", "Seleccionar una Cuenta Contable");
                ui_viewmaestros._FormPadre = this;
                ui_viewmaestros.BringToFront();
                ui_viewmaestros.ShowDialog();
                ui_viewmaestros.Dispose();
            }
        }

        private void txtHaber_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                GlobalVariables gv = new GlobalVariables();
                string idcia = gv.getValorCia();
                string query = "SELECT codcuenta as codigo,descuenta as descripcion from cuencon ";
                query = query + " where idcia='" + @idcia + "' and s_estado = 1 order by codcuenta asc;";
                this._TextBoxActivo = txtHaber;
                ui_viewmaestros ui_viewmaestros = new ui_viewmaestros();
                ui_viewmaestros.setData(query, "ui_confmotivos", "Seleccionar una Cuenta Contable");
                ui_viewmaestros._FormPadre = this;
                ui_viewmaestros.BringToFront();
                ui_viewmaestros.ShowDialog();
                ui_viewmaestros.Dispose();
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label13_Click(object sender, EventArgs e)
        {

        }
    }
}