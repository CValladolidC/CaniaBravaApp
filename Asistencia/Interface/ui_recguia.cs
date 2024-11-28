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
    public partial class ui_recguia : Form
    {
        string _codcia;
        string _usuario;

        private TextBox TextBoxActivo;
        private Form FormPadre;

        public Form _FormPadre
        {
            get { return FormPadre; }
            set { FormPadre = value; }
        }

        public TextBox _TextBoxActivo
        {
            get { return TextBoxActivo; }
            set { TextBoxActivo = value; }
        }

        public ui_recguia()
        {
            InitializeComponent();
        }

        private void txtCliente_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                this._TextBoxActivo = txtCliente;
                ui_viewclientes ui_viewclientes = new ui_viewclientes();
                ui_viewclientes._FormPadre = this;
                ui_viewclientes._clasePadre = "ui_recguia";
                ui_viewclientes._condicionAdicional = string.Empty;
                ui_viewclientes.BringToFront();
                ui_viewclientes.ShowDialog();
                ui_viewclientes.Dispose();
            }
        }

        private void txtCliente_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(Keys.Enter))
            {
                Clie clie = new Clie();
                string codclie = txtCliente.Text.Trim();
                string nombre = clie.ui_getDatos(codclie, "NOMBRE");
                if (nombre == string.Empty)
                {
                    txtCliente.Text = string.Empty;
                    txtRazonDesti.Text = string.Empty;
                    txtRucDesti.Text = string.Empty;
                    e.Handled = true;
                    txtCliente.Focus();
                }
                else
                {
                    string rucclie = clie.ui_getDatos(codclie, "RUC");
                    string docidendesti = string.Empty;
                    string nrodocdesti = string.Empty;
                    txtCliente.Text = clie.ui_getDatos(codclie, "CODIGO");
                    txtRazonDesti.Text = clie.ui_getDatos(codclie, "NOMBRE");
                    txtRucDesti.Text = rucclie;
                    if (rucclie == string.Empty)
                    {
                        nrodocdesti = clie.ui_getDatos(codclie, "DNI");
                        if (nrodocdesti != string.Empty)
                        {
                            docidendesti = "01";
                        }
                    }
                    else
                    {
                        nrodocdesti = rucclie;
                        docidendesti = "06";
                    }
                    e.Handled = true;
                    cmbListar.Focus();
                }

                ui_listarPunClie(codclie);
                ui_ListaGuiasRemi();
     
            }
        }

        public void ui_listarPunClie(string codclie)
        {

            string query = "Select codpartida as clave,despartida as descripcion ";
            query = query + "from punclie where codclie='" + @codclie + "' order by 1 asc;";
            Funciones funciones = new Funciones();
            funciones.listaComboBox(query, cmbLlegada, "");

        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ui_recguia_Load(object sender, EventArgs e)
        {
            GlobalVariables gv = new GlobalVariables();
            this._codcia = gv.getValorCia();
            this._usuario = gv.getValorUsr();
            cmbListar.Text = "X   TODAS";
            ui_ListaGuiasRemi();

        }

        private void ui_ListaGuiasRemi()
        {
            Funciones funciones = new Funciones();
            string codcia = this._codcia;
            string codclie = txtCliente.Text;
            string llegada = funciones.getValorComboBox(cmbLlegada, 3);
            string listar = funciones.getValorComboBox(cmbListar, 1);
            string condicionListar;
  
            if (listar.Equals("X"))
            {
                condicionListar = string.Empty;
            }
            else
            {
                if (listar.Equals("P"))
                {
                    condicionListar = " and A.recibe='N' ";
                }
                else
                {
                    condicionListar = " and A.recibe='S' ";
                }
            }

            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();
            string query = "  select A.alma,A.td,A.numdoc,A.rftguia,A.rfnguia,A.fecguia,";
            query = query + " A.fectras,B.rucclie,B.desclie,";
            query = query + " A.recibe,A.rfserie,A.rfnro from almovc A left join clie B on A.codclie=B.codclie ";
            query = query + " where A.codcia='" + @codcia + "' and A.codclie='" + @codclie + "' and llegada='"+@llegada+"' ";
            query = query + " and A.flag='GRCS' and A.situagr='F'  " + condicionListar;
            query = query + " order by A.fecguia asc ;";

            try
            {
                using (SqlDataAdapter myDataAdapter = new SqlDataAdapter(query, conexion))
                {
                    DataSet myDataSet = new DataSet();
                    myDataAdapter.Fill(myDataSet, "tblData");
                    funciones.formatearDataGridView(dgvData);
                    dgvData.DataSource = myDataSet.Tables["tblData"];
                    dgvData.Columns[0].HeaderText = "Almacén";
                    dgvData.Columns[1].HeaderText = "TD";
                    dgvData.Columns[2].HeaderText = "Mov.Almacén";
                    dgvData.Columns[3].HeaderText = "Tipo Doc. Guía";
                    dgvData.Columns[4].HeaderText = "Nro.Doc. Guía";
                    dgvData.Columns[5].HeaderText = "Fecha Guía";
                    dgvData.Columns[6].HeaderText = "Fecha Inicio Traslado";
                    dgvData.Columns[7].HeaderText = "RUC Destinatario";
                    dgvData.Columns[8].HeaderText = "Razón Social";
                    dgvData.Columns[9].HeaderText = "¿Recepcionado? [S]Si  [N]No ";

                    dgvData.Columns["rfserie"].Visible = false;
                    dgvData.Columns["rfnro"].Visible = false;

                    dgvData.Columns[0].Width = 50;
                    dgvData.Columns[1].Width = 30;
                    dgvData.Columns[2].Width = 80;
                    dgvData.Columns[3].Width = 50;
                    dgvData.Columns[4].Width = 100;
                    dgvData.Columns[5].Width = 75;
                    dgvData.Columns[6].Width = 75;
                    dgvData.Columns[7].Width = 90;
                    dgvData.Columns[8].Width = 250;
                    dgvData.Columns[9].Width = 100;

                    if (dgvData.Rows.Count > 7)
                    {
                        dgvData.CurrentCell = dgvData.Rows[dgvData.Rows.Count - 1].Cells[0];
                    }
                }

                ui_calculaNumRegistros();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            conexion.Close();
        }


        private void dgvData_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if ((this.dgvData.Rows[e.RowIndex].Cells["recibe"].Value).ToString().Equals("N"))
            {
                foreach (DataGridViewCell celda in

                this.dgvData.Rows[e.RowIndex].Cells)
                {

                    celda.Style.BackColor = Color.Yellow;
                    celda.Style.ForeColor = Color.Black;

                }
            }
        }

        public void ui_calculaNumRegistros()
        {
            Funciones funciones = new Funciones();
            string numregencontrados = Convert.ToString(dgvData.RowCount);
            txtRegEncontrados.Text = funciones.replicateCadena(" ", 15 - numregencontrados.Trim().Length) + numregencontrados.Trim() + funciones.replicateCadena(" ", 20) + " De acuerdo al criterio de búsqueda";
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            Exporta exporta = new Exporta();
            exporta.Excel_FromDataGridView(dgvData);
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            ui_ListaGuiasRemi();
        }

        private void cmbLlegada_SelectedIndexChanged(object sender, EventArgs e)
        {
            ui_ListaGuiasRemi();
        }

        private void cmbListar_SelectedIndexChanged(object sender, EventArgs e)
        {
            ui_ListaGuiasRemi();
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            Int32 selectedCellCount = dgvData.GetCellCount(DataGridViewElementStates.Selected);
            if (selectedCellCount > 0)
            {
                Funciones funciones = new Funciones();
                CiaFile ciafile = new CiaFile();
                string codcia = this._codcia;
                string usuario = this._usuario;
                string alma = dgvData.Rows[dgvData.SelectedCells[1].RowIndex].Cells[0].Value.ToString();
                string td = dgvData.Rows[dgvData.SelectedCells[1].RowIndex].Cells[1].Value.ToString();
                string numdoc = dgvData.Rows[dgvData.SelectedCells[1].RowIndex].Cells[2].Value.ToString();
                string descia = ciafile.ui_getDatosCiaFile(codcia, "DESCRIPCION");
                ui_updrecguia ui_updrecguia = new ui_updrecguia();
                ui_updrecguia._FormPadre = this;
                ui_updrecguia.Activate();
                ui_updrecguia.BringToFront();
                ui_updrecguia._codcia = codcia;
                ui_updrecguia._descia = descia;
                ui_updrecguia._usuario = usuario;
                ui_updrecguia.ui_listacombobox();
                ui_updrecguia.editar(codcia,alma, td, numdoc);
                ui_updrecguia.ShowDialog();
                ui_updrecguia.Dispose();
            }
            else
            {
                MessageBox.Show("No ha seleccionado registro a Editar", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void btnDesRec_Click(object sender, EventArgs e)
        {
            Int32 selectedCellCount =
          dgvData.GetCellCount(DataGridViewElementStates.Selected);
            if (selectedCellCount > 0)
            {
                GlobalVariables gv = new GlobalVariables();
                string codcia = this._codcia;
                string alma = dgvData.Rows[dgvData.SelectedCells[1].RowIndex].Cells[0].Value.ToString();
                string td = dgvData.Rows[dgvData.SelectedCells[1].RowIndex].Cells[1].Value.ToString();
                string numdoc = dgvData.Rows[dgvData.SelectedCells[1].RowIndex].Cells[2].Value.ToString();
                string fmod = DateTime.Now.ToString("dd/MM/yyyy");
                string usuario = gv.getValorUsr();
                ui_autentica ui_autentica = new ui_autentica();
                ui_autentica._tipousr = "M";
                ui_autentica.Activate();
                ui_autentica.BringToFront();
                ui_autentica.ShowDialog();
                ui_autentica.Dispose();

                string autentica = gv.getAutentica();
                if (autentica.Equals("S"))
                {
                    DialogResult resultado = MessageBox.Show("¿Desea DESFINALIZAR el documento " + @td + "/" + @numdoc + "?. Se eliminará el Parte de Ingreso generado en dicha recepción de materiales",
           "Consulta Importante",
           MessageBoxButtons.YesNo,
           MessageBoxIcon.Question);
                    if (resultado == DialogResult.Yes)
                    {
                        AlmovC almovc = new AlmovC();
                        almovc.updDesFinRec(codcia, alma, td, numdoc, fmod, usuario);
                        ui_ListaGuiasRemi();
                    }
                }

            }
            else
            {
                MessageBox.Show("No ha seleccionado registro a DESFINALIZAR", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

    }
}
