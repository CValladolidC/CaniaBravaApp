using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Configuration;

namespace CaniaBrava
{
    public partial class ui_delalarti : ui_form
    {
        public string _codcia;
        public string _codarti;
        public string _desarti;

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

        public ui_delalarti()
        {
            InitializeComponent();
        }

        private void ui_delalarti_Load(object sender, EventArgs e)
        {
            this.Text = "Movimientos Enlazados de la Existencia " + this._codarti + " - " + this._desarti;

            ui_ListaMovArti();
        }

        private void ui_ListaMovArti()
        {
            Funciones funciones = new Funciones();
            string codcia = this._codcia;
            string codarti = this._codarti;

            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();

            string query = " select A.fecdoc,A.td,A.numdoc,A.codmov,A.rftdoc,A.rfndoc,";
            query = query + " D.ruc,A.glosa1,A.codmon,B.preuni,B.cantidad,B.lote,A.alma ";
            query = query + " from almovc A inner join almovd B ";
            query = query + " on A.codcia=B.codcia and A.alma=B.alma and A.td=B.td and A.numdoc=B.numdoc ";
            query = query + " left join alarti C on ";
            query = query + " B.codcia=C.codcia and B.codarti=C.codarti left join provee D on A.codpro=D.codprovee ";
            query = query + " where A.codcia='" + @codcia + "' and B.codarti='" + @codarti + "' and B.cantidad>0 ";
            query = query + " order by A.fecdoc desc,A.td asc,A.numdoc asc";

            try
            {
                using (SqlDataAdapter myDataAdapter = new SqlDataAdapter(query, conexion))
                {
                    DataSet myDataSet = new DataSet();
                    myDataAdapter.Fill(myDataSet, "tblMov");
                    funciones.formatearDataGridView(dgvData);

                    dgvData.DataSource = myDataSet.Tables["tblMov"];
                    dgvData.Columns[0].HeaderText = "Fecha Doc.";
                    dgvData.Columns[1].HeaderText = "Mov";
                    dgvData.Columns[2].HeaderText = "Nro.Mov.";
                    dgvData.Columns[3].HeaderText = "Mov.";
                    dgvData.Columns[4].HeaderText = "T.Doc.";
                    dgvData.Columns[5].HeaderText = "Nro.Doc.";
                    dgvData.Columns[6].HeaderText = "RUC Proveedor";
                    dgvData.Columns[7].HeaderText = "Glosa";
                    dgvData.Columns[8].HeaderText = "Mon.";
                    dgvData.Columns[9].HeaderText = "P.Unit.";
                    dgvData.Columns[10].HeaderText = "Cantidad";
                    dgvData.Columns[11].HeaderText = "Lote";
                    dgvData.Columns[12].HeaderText = "Almacén";

                    dgvData.Columns[0].Width = 70;
                    dgvData.Columns[1].Width = 30;
                    dgvData.Columns[2].Width = 70;
                    dgvData.Columns[3].Width = 30;
                    dgvData.Columns[4].Width = 40;
                    dgvData.Columns[5].Width = 90;
                    dgvData.Columns[6].Width = 80;
                    dgvData.Columns[7].Width = 200;
                    dgvData.Columns[8].Width = 30;
                    dgvData.Columns[9].Width = 60;
                    dgvData.Columns[10].Width = 60;
                    dgvData.Columns[11].Width = 120;
                    dgvData.Columns[12].Width = 70;

                }
                
                string numReg = Convert.ToString(dgvData.RowCount);
                txtRegTotal.Text = funciones.replicateCadena(" ", 15 - numReg.Trim().Length) + numReg.Trim() + funciones.replicateCadena(" ", 20) + " Movimientos Enlazados.";

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }

            conexion.Close();

        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            ((ui_alarti)FormPadre).btnActualizar.PerformClick();
            this.Close();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            int numReg = dgvData.RowCount;
            if (numReg > 0)
            {
                MessageBox.Show("No se puede eliminar la Existencia debido a que existen registros enlazados al mismo", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            else
            {

                GlobalVariables gv = new GlobalVariables();
                string codcia = this._codcia;
                string codarti = this._codarti;
                string desarti = this._desarti;

                DialogResult resultado = MessageBox.Show("¿Realmente desea eliminar la Existencia " + @desarti + "?",
       "Consulta Importante",
       MessageBoxButtons.YesNo,
       MessageBoxIcon.Question);
                if (resultado == DialogResult.Yes)
                {
                    AlArti alarti = new AlArti();
                    alarti.delArti(codcia, codarti);
                    ((ui_alarti)FormPadre).btnActualizar.PerformClick();
                    this.Close();
                }
            }
        }

        private void txtCodigo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                this._TextBoxActivo = txtCodigo;
                ui_viewarti ui_viewarti = new ui_viewarti();
                ui_viewarti._FormPadre = this;
                ui_viewarti._codcia = this._codcia;
                ui_viewarti._clasePadre = "ui_delalarti";
                ui_viewarti._condicionAdicional = string.Empty;
                ui_viewarti.BringToFront();
                ui_viewarti.ShowDialog();
                ui_viewarti.Dispose();
            }
        }

        private void txtCodigo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(Keys.Enter))
            {
                AlArti alarti = new AlArti();
                string codcia = this._codcia;
                string codarti = txtCodigo.Text.Trim();
                string nombre = alarti.ui_getDatos(codcia, codarti, "NOMBRE");
                if (nombre == string.Empty || codarti == string.Empty)
                {
                    txtCodigo.Text = "";
                    lblDescri.Text = "";
                    e.Handled = true;
                    txtCodigo.Focus();
                }
                else
                {
                   string codigo = alarti.ui_getDatos(codcia, codarti, "CODIGO");
                    txtCodigo.Text = codigo;
                    lblDescri.Text = alarti.ui_getDatos(codcia, codarti, "NOMBRE");
                    e.Handled = true;
                    btnAsigna.Focus();
                }

            }
        }

        private void btnAsigna_Click(object sender, EventArgs e)
        {
            int numReg = dgvData.RowCount;
            if (numReg == 0)
            {
                MessageBox.Show("No existen registros enlazados", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            else
            {

                GlobalVariables gv = new GlobalVariables();
                string codcia = this._codcia;
                string old_codarti = this._codarti;
                string new_codarti = txtCodigo.Text;
                string desarti = this._desarti;
                ui_autentica ui_autentica = new ui_autentica();
                ui_autentica._tipousr = "M";
                ui_autentica.Activate();
                ui_autentica.BringToFront();
                ui_autentica.ShowDialog();
                ui_autentica.Dispose();
                string autentica = gv.getAutentica();
                if (autentica.Equals("S"))
                {
                    DialogResult resultado = MessageBox.Show("¿Realmente desea reasignar los registros enlazados a la existencia " + @old_codarti + " a la existencia "+@new_codarti+" ?",
           "Consulta Importante",
           MessageBoxButtons.YesNo,
           MessageBoxIcon.Question);
                    if (resultado == DialogResult.Yes)
                    {
                        AlArti alarti = new AlArti();
                        alarti.reasignaCodigo(codcia, old_codarti, new_codarti);
                        ui_ListaMovArti();
                    }
                }
            }
        }

        
    }
}
