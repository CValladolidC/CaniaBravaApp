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
    public partial class ui_stkalmacenes : ui_form
    {
        string _codcia;

        private TextBox TextBoxActivo;

        public TextBox _TextBoxActivo
        {
            get { return TextBoxActivo; }
            set { TextBoxActivo = value; }
        }


        public ui_stkalmacenes()
        {
            InitializeComponent();
        }

        private void txtCodigo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                Funciones funciones = new Funciones();
                this._TextBoxActivo = txtCodigo;
                ui_viewarti ui_viewarti = new ui_viewarti();
                ui_viewarti._FormPadre = this;
                ui_viewarti._codcia = this._codcia;
                ui_viewarti._clasePadre = "ui_stkalmacenes";
                ui_viewarti._condicionAdicional = string.Empty;
                ui_viewarti.BringToFront();
                ui_viewarti.ShowDialog();
                ui_viewarti.Dispose();
            }
        }

        private string ui_valida()
        {
            string valorValida = "G";

            if (txtCodigo.Text.Trim() == string.Empty)
            {

                MessageBox.Show("No ha especificado artículo", "Aviso Importante", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                valorValida = "B";

            }
            return valorValida;
        }


        private void ui_ListaStock()
        {
            Funciones funciones = new Funciones();
            string codcia = this._codcia;
            string codarti = txtCodigo.Text.Trim();

            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();

            string query = " select A.alma,B.desalma,A.stock ";
            query = query + " from alstock A left join alalma B on A.codcia=B.codcia and A.alma=B.codalma  ";
            query = query + " where A.codcia='"+@codcia+"' and A.codarti='" + @codarti + "' and A.stock>0";
            query = query + " order by A.alma asc";

            try
            {
                using (SqlDataAdapter myDataAdapter = new SqlDataAdapter(query, conexion))
                {
                    DataSet myDataSet = new DataSet();
                    myDataAdapter.Fill(myDataSet, "tblStock");
                    funciones.formatearDataGridView(dgvdetalle);

                    dgvdetalle.DataSource = myDataSet.Tables["tblStock"];
                    dgvdetalle.Columns[0].HeaderText = "Código Almacén";
                    dgvdetalle.Columns[1].HeaderText = "Descripción";
                    dgvdetalle.Columns[2].HeaderText = "Stock Disponible";
                  
                    dgvdetalle.Columns[0].Width = 100;
                    dgvdetalle.Columns[1].Width = 300;
                    dgvdetalle.Columns[2].Width = 120;

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }

            conexion.Close();

        }


        private void ui_limpiar()
        {
            txtCodigo.Clear();
            txtDescripcion.Clear();
            txtUnidad.Clear();
            txtFamilia.Clear();
            txtGrupo.Clear();

        }

        private void txtCodigo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(Keys.Enter))
            {
                AlArti alarti = new AlArti();
                string codcia = this._codcia;
                string codarti = txtCodigo.Text.Trim();
                string nombre = alarti.ui_getDatos(codcia,codarti, "NOMBRE");
                if (nombre == string.Empty || codarti == string.Empty)
                {
                    ui_limpiar();
                    e.Handled = true;
                    txtCodigo.Focus();
                }
                else
                {
                    txtCodigo.Text = alarti.ui_getDatos(codcia, codarti, "CODIGO");
                    txtDescripcion.Text = alarti.ui_getDatos(codcia, codarti, "NOMBRE");
                    txtUnidad.Text = alarti.ui_getDatos(codcia, codarti, "UNIDAD");
                    txtFamilia.Text = alarti.ui_getDatos(codcia, codarti, "FAMILIA");
                    txtGrupo.Text = alarti.ui_getDatos(codcia, codarti, "GRUPO");
                    e.Handled = true;
                    btnConsultar.Focus();
                }

            }
        }

        private void btnConsultar_Click(object sender, EventArgs e)
        {
            string valorValida = ui_valida();
            if (valorValida.Equals("G"))
            {
                ui_ListaStock();
            }
        }

        private void ui_stkalmacenes_Load(object sender, EventArgs e)
        {
            GlobalVariables gv = new GlobalVariables();
            this._codcia = gv.getValorCia();
            ui_limpiar();
            ui_ListaStock();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            Exporta exporta = new Exporta();
            exporta.Excel_FromDataGridView(dgvdetalle);
        }

       
        private void groupBox3_Enter(object sender, EventArgs e)
        {

        }
    }
}
