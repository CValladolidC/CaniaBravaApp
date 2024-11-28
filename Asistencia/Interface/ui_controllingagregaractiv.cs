using CaniaBrava.cs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CaniaBrava.Interface;
using System.Data.SqlClient;
using System.Configuration;

namespace CaniaBrava.Interface
{
    public partial class ui_controllingagregaractiv : Form
    {
        Funciones funciones = new Funciones();
        public ui_controllingagregaractiv()
        {
            InitializeComponent();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Close();
        }


        public void desrecurso(string idrecurso)
        {
            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();
            string squerye = "SELECT DISTINCT(desrecurso) desrecurso FROM Asistencia.dbo.maescontrolling WHERE recurso='" + idrecurso + "'";
            DataSet das = new DataSet();
            SqlDataAdapter adp = new SqlDataAdapter(squerye, conexion);
            adp.Fill(das, "maescontrolling");
            cmbdesrecurso.DataSource = das.Tables[0].DefaultView;
            cmbdesrecurso.DisplayMember = "desrecurso";
        }


        public void recurso()
        {
            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();
            string squerye = "SELECT DISTINCT(recurso) recurso FROM Asistencia.dbo.maescontrolling ";
            DataSet das = new DataSet();
            SqlDataAdapter adp = new SqlDataAdapter(squerye, conexion);
            adp.Fill(das, "maescontrolling");
            cmbrecurso.DataSource = das.Tables[0].DefaultView;
            cmbrecurso.DisplayMember = "idrecurso";
            cmbrecurso.ValueMember = "recurso";
        }

       
        private void ui_controllingagregaractiv_Load(object sender, EventArgs e)
        {
            controllingactividad c = new controllingactividad();
            c.um(cmbUM);
            c.rubro(cmbrubro);
            recurso();
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            Funciones funciones = new Funciones();

            string idmaescon = txtcod.Text.Trim();
            string desmaesgen = txtdescod.Text.Trim();
            string um = funciones.getValorComboBox(cmbUM, 3);
            string cuencon = txtcuencon.Text.Trim();
            string descuencon = txtdescuencon.Text.Trim();
            string cod = txtcodigo.Text.Trim();
            string rubro = funciones.getValorComboBox(cmbrubro, 3);
            string actividad = txtactividad.Text.Trim();
            string desactividad = txtdesactividad.Text.Trim();
            string recurso = funciones.getValorComboBox(cmbrecurso, 2);
            string desrecurso = funciones.getValorComboBox(cmbdesrecurso, 20);

            if (txtcod.Text == "")
            {
                MessageBox.Show("Debe completar campo codigo", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
                txtcod.Focus();
            }
            if (txtdescod.Text == "")
            {
                MessageBox.Show("Debe completar campo descripcion de codigo", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
                txtdescod.Focus();
            }
            if (txtcuencon.Text == "")
            {
                MessageBox.Show("Debe completar campo jefatura", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
                txtcuencon.Focus();
            }
            if (txtdescuencon.Text == "")
            {
                MessageBox.Show("Debe completar campo responsable de RR.HH", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
                txtdescuencon.Focus();
            }
            if (txtactividad.Text == "")
            {
                MessageBox.Show("Debe completar campo jefatura", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
                txtactividad.Focus();
            }
            if (txtdesactividad.Text == "")
            {
                MessageBox.Show("Debe completar campo responsable de RR.HH", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
                txtdesactividad.Focus();
            }
            else
            {
                Addregistros(idmaescon, desmaesgen, um, cuencon, descuencon, cod, rubro,
                actividad, desactividad, recurso, desrecurso);
                limpiarcampos();
                MessageBox.Show("Informacion Guardada con Exito..", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void limpiarcampos()
        {
            txtcod.Text = "";
            txtdescod.Text = "";
            txtcuencon.Text = "";
            txtdescuencon.Text = "";
            txtcodigo.Text = "";
            txtactividad.Text = "";
            txtdesactividad.Text = "";
            
        }

        #region Adicionar Registros SQL
        private void Addregistros(string idmaescon, string desmaesgen, string um, string cuencon, string descuencon,
            string cod, string rubro, string actividad, string desactividad, string recurso, string desrecurso)
        {
            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();

            string query = " INSERT INTO maescontrolling (idmaescon, desmaesgen, um, cuencon, descuencon, cod, " +
                            "rubro, actividad, desactividad, recurso, desrecurso) VALUES ";
            query += "('" + @idmaescon + "'," +
                        "'" + @desmaesgen + "'," +
                        "'" + @um + "'," +
                        "'" + @cuencon + "'," +
                        "'" + @descuencon + "'," +
                        "'" + @cod + "'," +
                        "'" + @rubro + "'," +
                        "'" + @actividad + "'," +
                        "'" + @desactividad + "'," +
                        "'" + @recurso + "'," +
                        "'" + @desrecurso + "');";

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

        #endregion


        private void nombrecadena()
        {
            txtcodigo.Text = txtcod.Text.Trim() + cmbUM.Text.Trim();
        }

        private void txtcod_TextChanged(object sender, EventArgs e)
        {
            nombrecadena();
        }

        private void cmbUM_TextChanged(object sender, EventArgs e)
        {
            nombrecadena();
        }

        private void cmbdesrecurso_SelectionChangeCommitted(object sender, EventArgs e)
        {
            
        }

        private void cmbdesrecurso_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void cmbrecurso_SelectionChangeCommitted(object sender, EventArgs e)
        {
            desrecurso(Convert.ToString(cmbrecurso.SelectedValue));
        }

        private void btnbuscar_Click(object sender, EventArgs e)
        {
            ui_controlling_ctascontables ui_controlling_ctascontables = new ui_controlling_ctascontables();
            ui_controlling_ctascontables.pasado += new ui_controlling_ctascontables.pasar(ejecutar);
            ui_controlling_ctascontables.ShowDialog();
        }

        public void ejecutar (string cta, string descuenta)
        {
            txtcuencon.Text = cta;
            txtdescuencon.Text = descuenta; 
        }
    }
}
