using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ExcelDataReader;
//using FuncionalidadExcel.DATA;
using System.IO;
using CaniaBrava.cs;
using System.Data.SqlClient;
using System.Configuration;

namespace CaniaBrava.Interface
{
    public partial class ui_controllingcarga : Form
    {
        Funciones funciones = new Funciones();
        private int vers = 1;

        //Un DataSet es un objeto que almacena n número de DataTables, estas tablas puedes estar conectadas dentro del dataset.
        private DataSet dtsTablas = new DataSet();
        public ui_controllingcarga()
        {
            InitializeComponent();
            if (funciones.VersionAssembly()) Application.ExitThread();

            txtanio.Text = DateTime.Now.Year.ToString();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            //configuracion de ventana para seleccionar un archivo
            OpenFileDialog oOpenFileDialog = new OpenFileDialog();
            oOpenFileDialog.Filter = "Excel Worbook|*.xlsx";

            if (oOpenFileDialog.ShowDialog() == DialogResult.OK)
            {
                cboHojas.Items.Clear();
                dgvDatos.DataSource = null;

                txtRuta.Text = oOpenFileDialog.FileName;

                //FileStream nos permite leer, escribir, abrir y cerrar archivos en un sistema de archivos, como matrices de bytes
                FileStream fsSource = new FileStream(oOpenFileDialog.FileName, FileMode.Open, FileAccess.Read);


                //ExcelReaderFactory.CreateBinaryReader = formato XLS
                //ExcelReaderFactory.CreateOpenXmlReader = formato XLSX
                //ExcelReaderFactory.CreateReader = XLS o XLSX
                IExcelDataReader reader = ExcelReaderFactory.CreateReader(fsSource);

                //convierte todas las hojas a un DataSet
                dtsTablas = reader.AsDataSet(new ExcelDataSetConfiguration()
                {
                    ConfigureDataTable = (tableReader) => new ExcelDataTableConfiguration()
                    {
                        UseHeaderRow = true
                    }
                });

                //obtenemos las tablas y añadimos sus nombres en el desplegable de hojas
                foreach (DataTable tabla in dtsTablas.Tables)
                {
                    cboHojas.Items.Add(tabla.TableName);
                }
                cboHojas.SelectedIndex = 0;

                reader.Close();
            }

            btnRegistrarData.Enabled = false;
        }

        private void btnMostrar_Click(object sender, EventArgs e)
        {
            
            if (lblDato.Text == "")

            {
                MessageBox.Show("Debe Seleccionar un Tipo", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                dgvDatos.DataSource = dtsTablas.Tables[cboHojas.SelectedIndex];

                int cont = this.dgvDatos.RowCount;

                for (int i = 0; i < cont - 1; i++)
                {
                    dgvDatos[0, i].Value = lblDato.Text;
                }

                dgvDatos.Columns["Fcultivo"].DefaultCellStyle.Format = "yyyy-MM-dd";

                lblnreg.Enabled = true;
                lblnreg.Text = (cont - 1).ToString() + " Total Registros";
                btnRegistrarData.Enabled = true;
            }

        }

        private void ui_controllingcarga_Load(object sender, EventArgs e)
        {
            DiseñoInicial();
        }

        private void DiseñoInicial()
        {
            btnBuscar.Cursor = Cursors.Hand;
            btnMostrar.Cursor = Cursors.Hand;
            btnRegistrarData.Cursor = Cursors.Hand;
            txtRuta.Enabled = true;

            //dgvDatos.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            //dgvDatos.MultiSelect = false;
            //dgvDatos.ReadOnly = true;
            //dgvDatos.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            //dgvDatos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            //dgvDatos.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
        }

        private void btnRegistrarData_Click(object sender, EventArgs e)
        {
            Escenario();
            ReadExcel();

            btnRegistrarData.Enabled = false;

        }


        private void ReadExcel()
        {
            controllingcarga obj = new controllingcarga();

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

                    DataTable data = (DataTable)(dgvDatos.DataSource);

                    bool resultado = new controllingcarga().CargarDatacontrolling(data);


                    if (resultado)
                    {
                        int cont = this.dgvDatos.RowCount;
                        MessageBox.Show("Se procedio a registrar la información - Total Registros " + (cont - 1).ToString());
                    }
                    else
                    {
                        MessageBox.Show("Hubo un problema al registrar la información");
                    }
                    
                }
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
                btnRegistrarData.Focus();
            }
        }

        private void cmbtipo_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblMes.Visible = false;
            cmbMes.Visible = false;
            btnMostrar.Enabled = true;
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

        private void Escenario()

        {
           
            string tipo = funciones.getValorComboBox(cmbtipo, 2);
            string anio = txtanio.Text.Trim();
            string mes = funciones.getValorComboBox(cmbMes, 2);
            string escenario = lblDato.Text;
            string version = lblDato.Text.Trim().Substring(17, 1).Replace(" ", "");
            int indicador = 1;

            //insertando la version
            

            SqlConnection conexion = new SqlConnection();
            try
            {
                string query = string.Empty;
                query = "UPDATE Asistencia.dbo.controlling_version SET indicador = 0 WHERE tipo = '" + tipo + "' ";
                query += "INSERT INTO controlling_version VALUES ('" + tipo + "','" + anio + "','" + anio + "-" + (mes == string.Empty ? "01" : mes) + "-01" + "','" + version + "','" + escenario + "','" + indicador + "'); ";

                conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
                conexion.Open();
                SqlCommand myCommand = new SqlCommand(query, conexion);
                myCommand.ExecuteNonQuery();
                myCommand.Dispose();
            }
            catch (SqlException ex)
            {

            }
            finally { conexion.Close(); }
            
        }

        private void lblTexto_Click(object sender, EventArgs e)
        {

        }

        private void btnRegistrarData_EnabledChanged(object sender, EventArgs e)
        {

        }

        private void btnMostrar_EnabledChanged(object sender, EventArgs e)
        {
            
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
