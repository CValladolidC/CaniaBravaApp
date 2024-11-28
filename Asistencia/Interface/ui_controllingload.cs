using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CaniaBrava
{
    public partial class ui_controllingload : Form
    {
        Funciones funciones = new Funciones();
        private int vers = 1;
        public ui_controllingload()
        {
            InitializeComponent();

            if (funciones.VersionAssembly()) Application.ExitThread();

            txtanio.Text = DateTime.Now.Year.ToString();
        }

        private void btnUrl_Click(object sender, EventArgs e)
        {
            OpenFileDialog Abrir = new OpenFileDialog();
            Abrir.Title = "Seleccionar Excel";
            Abrir.Filter = "Excel files | *.xlsx;.xls"; // file types, that will be allowed to upload
            Abrir.FilterIndex = 4;
            Abrir.RestoreDirectory = true;

            if (Abrir.ShowDialog() == DialogResult.OK)
            {
                txturl.Text = Abrir.FileName;
            }
        }

        private void txtanio_KeyPress(object sender, KeyPressEventArgs e)
        {
            //Para obligar a que sólo se introduzcan números
            if (Char.IsDigit(e.KeyChar))
            {
                e.Handled = false;
            }
            else
            {
                if (Char.IsControl(e.KeyChar)) //permitir teclas de control como retroceso
                {
                    e.Handled = false;
                }
                else
                {
                    //el resto de teclas pulsadas se desactivan
                    e.Handled = true;
                }
            }
        }

        private void cmbtipo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                e.Handled = true;
                btnCargar.Focus();
            }
        }

        private void btnCargar_Click(object sender, EventArgs e)
        {
            ReadExcel();
        }

        private void ReadExcel()
        {
            Controlling obj = new Controlling();

            if (txtanio.Text.Trim().Length != 4)
            {
                MessageBox.Show("Debe ingresar un Año correcto", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtanio.Focus();
            }
            else
            {
                string tipo = funciones.getValorComboBox(cmbtipo, 2);
                if (tipo == string.Empty)
                {
                    MessageBox.Show("Debe ingresar Tipo de carga", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    cmbtipo.Focus();
                }
                else
                {
                    string mes = funciones.getValorComboBox(cmbMes, 2);
                    if (tipo == "PY")
                    {
                        if (mes == string.Empty)
                        {
                            MessageBox.Show("Debe ingresar un Mes correcto", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            cmbMes.Focus();
                        }
                    }
                    if (File.Exists(txturl.Text))
                    {
                        txtanio.Enabled = false;
                        cmbtipo.Enabled = false;
                        btnCargar.Enabled = false;
                        btnSalir.Enabled = false;
                        btnUrl.Enabled = false;
                        pgbAvance.Visible = true;

                        obj.Path = txturl.Text;
                        obj.Tipo = tipo;
                        obj.Anio = txtanio.Text;
                        obj.Mes = mes;
                        obj.Versi = vers;
                        loadingNext1.Visible = true;
                        backgroundWorker1.RunWorkerAsync(obj);
                    }
                    else
                    {
                        MessageBox.Show("No existen datos en Excel..!!" + txturl.Text, "Información", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }
            }
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            // Get the BackgroundWorker object that raised this event.
            System.ComponentModel.BackgroundWorker worker;
            worker = (System.ComponentModel.BackgroundWorker)sender;

            // Get the Words object and call the main method.
            Controlling WC = (Controlling)e.Argument;
            WC.procesa(worker, e);
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            // This event handler is called when the background thread finishes.
            // This method runs on the main thread.
            if (e.Error != null)
            {
                MessageBox.Show("Error: " + e.Error.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtanio.Enabled = true;
                cmbtipo.Enabled = true;
                btnCargar.Enabled = true;
                btnSalir.Enabled = true;
                btnUrl.Enabled = true;
                pgbAvance.Visible = false;
                loadingNext1.Visible = false;
            }
            else
            {
                if (e.Cancelled)
                {
                    MessageBox.Show("Proceso Cancelado", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                    txtanio.Enabled = true;
                    cmbtipo.Enabled = true;
                    btnCargar.Enabled = true;
                    btnSalir.Enabled = true;
                    btnUrl.Enabled = true;
                    pgbAvance.Visible = false;
                    loadingNext1.Visible = false;
                }
                else
                {
                    loadingNext1.Visible = false;
                    MessageBox.Show("Proceso Finalizado con éxito", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
            }

            pgbAvance.Value = 0;
            pgbAvance.Visible = false;
        }

        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            // This method runs on the main thread.
            Controlling.CurrentState state = (Controlling.CurrentState)e.UserState;
            pgbAvance.Visible = true;
            pgbAvance.Value = state.LinesCounted;
            pgbAvance.Maximum = state.TotalLines;
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cmbtipo_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblMes.Visible = false;
            cmbMes.Visible = false;
            GetVersion();
        }

        private void GetVersion()
        {
            lblTexto.Visible = false;
            string tipo = funciones.getValorComboBox(cmbtipo, 2);
            string anio = txtanio.Text.Trim();
            string query = "SELECT ISNULL(MAX(versi),0)+1 AS total FROM controlling_version (NOLOCK) WHERE tipo='" + tipo + "' AND anio='" + anio + "' ";

            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();

            try
            {
                SqlCommand myCommand = new SqlCommand(query, conexion);
                SqlDataReader odr = myCommand.ExecuteReader();

                vers = 1;
                if (odr.Read())
                {
                    vers = odr.GetInt32(odr.GetOrdinal("total"));
                }

                lblDato.Text = tipo + anio + " - version " + vers;
                lblTexto.Visible = true;

                if (tipo == "PY")
                {
                    lblMes.Visible = true;
                    cmbMes.Visible = true;
                }

                odr.Close();
                myCommand.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            finally { conexion.Close(); }
        }

        private void txtanio_KeyUp(object sender, KeyEventArgs e)
        {
            GetVersion();
        }

        private void ui_controllingload_Load(object sender, EventArgs e)
        {

        }
    }
}