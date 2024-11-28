using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using System.Data.SqlClient;
using System.Threading;
using System.Data.Sql;


namespace CaniaBrava.Interface
{
    public partial class ui_platos : Form
    {

        public ui_platos()
        {
            InitializeComponent();

        }

        public void Tiempo(int tiempo)
        {
            Thread.Sleep(tiempo * 1000);
        }

        private void sumarid()
        {

           
            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();
            string query = "Select max(id) reg from Asistencia.dbo.platos";
            try
            {
                SqlCommand myCommand = new SqlCommand(query, conexion);

                string codmax = Convert.ToString(myCommand.ExecuteScalar());
                int cod = Convert.ToInt32(codmax) + 1;
                lblid.Text = Convert.ToString(cod);
            }
            catch (SqlException e)
            {
                MessageBox.Show(e.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }

            conexion.Close();

        }

        private Form FormPadre;
        public Form _FormPadre2
        {
            get { return FormPadre; }
            set { FormPadre = value; }
        }

        private void AddPlato(string id, string comedor, string fecha, string hora, string tipo, string cantidad, string plato)
        {
            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();

            string query = " INSERT INTO platos VALUES ";
            query += "('" + @id + "','" + @comedor + "','" + @fecha + "','" + @hora + "','" + @tipo + "','" + @cantidad + "','" + @plato + "');";

            try
            {
                SqlCommand myCommand = new SqlCommand(query, conexion);
                myCommand.ExecuteNonQuery();
                myCommand.Dispose();
            }
            catch (SqlException e)
            {
                MessageBox.Show(e.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }

            conexion.Close();
        }


        private void ui_platos_Load(object sender, EventArgs e)
        {
            cmbServicio.Items.Add("");
            cmbServicio.Items.Add("DESAYUNO");
            cmbServicio.Items.Add("ALMUERZO");
            cmbServicio.Items.Add("CENA");
            cmbServicio.SelectedIndex = 0;

            cmbcomedor.Items.Add("");
            cmbcomedor.Items.Add("MONTELIMA");
            cmbcomedor.Items.Add("LOBO");
            cmbcomedor.Items.Add("SAN VICENTE");
            cmbcomedor.SelectedIndex = 0;
            sumarid();

        }

        private void btnSalir_Click(object sender, EventArgs e) { Close(); }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            Funciones funciones = new Funciones();
            string id = lblid.Text.Trim();
            string comedor = funciones.getValorComboBox(cmbcomedor, 11);
            string fecha = dtpFecha.Value.ToString("yyyy-MM-dd");
            string hora = dtpFecha.Value.ToString("hh:mm:ss");
            string tipo = funciones.getValorComboBox(cmbServicio, 10);
            string cantidad = txtcantidad.Text.Trim();
            string plato = txtplato.Text.Trim();


            if (comedor == "")
            {
                MessageBox.Show("Debe Elegir un Comedor de Servicio", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }

            else
            {
                if (tipo == "")
            {
                MessageBox.Show("Debe Elegir un Tipo de Servicio", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }

            else
            {
                if (cantidad == String.Empty)
                {
                    MessageBox.Show("Debe Ingresar Cantidad", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                   txtcantidad.Focus();
                }
                    else
                    {
                        if (plato == String.Empty)
                        {
                            MessageBox.Show("Debe Ingresar Entrada", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                            txtplato.Focus();
                        }
                            else
                            {
                                AddPlato(id,comedor, fecha, hora, tipo, cantidad, plato);
                                cmbServicio.SelectedIndex = 0;
                                cmbcomedor.SelectedIndex = 0;
                                txtcantidad.Text = "";
                                txtplato.Text = "";
                                lblmensaje.Visible= true;
                                dtpFecha.Enabled = false;
                                cmbServicio.Enabled = false;
                                cmbcomedor.Enabled = false;
                                txtcantidad.Enabled = false;
                                txtplato.Enabled = false;
                                btnGuardar.Enabled = false;
                                btnNew.Enabled = true;
                                sumarid();
                            //Tiempo(2);
                            //ui_platos.btnGuardar.PerformClick();
                            //this.Close();
                        }
                    }
                }
            }

        }
        private void txtcantidad_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
                (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            // punto decimal
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            dtpFecha.Enabled = true;
            cmbServicio.Enabled = true;
            cmbcomedor.Enabled = true;
            txtcantidad.Enabled = true;
            txtplato.Enabled = true;
            cmbcomedor.SelectedIndex = 0;
            cmbServicio.SelectedIndex = 0;
            txtcantidad.Text = "";
            txtplato.Text = "";
            txtcantidad.Focus();
            lblmensaje.Visible = false;
            btnNew.Enabled = false;
            btnGuardar.Enabled = true;
            sumarid();
        }
    }
    }


