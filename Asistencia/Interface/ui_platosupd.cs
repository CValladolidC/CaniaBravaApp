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
using System.Configuration;
using System.Data.SqlClient;

namespace CaniaBrava.Interface
{
    public partial class ui_platosupd : Form
    {
        public ui_platosupd()
        {
            InitializeComponent();
        }

        private Form FormPadre;
        public Form _FormPadre3
        {
            get { return FormPadre; }
            set { FormPadre = value; }
        }
        //string _operacion;


        private void btnSalir_Click(object sender, EventArgs e) { Close(); }

        //public void ui_listarComboBox()
        //{
        //    platos comedor = new platos();
        //    comedor.listacomedor(cmbcomedor);
        //}

        //public void editar(string id)
        //{
        //    this._operacion = "EDITAR";
        //    SqlConnection conexion = new SqlConnection();
        //    conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
        //    conexion.Open();
        //    string query = "select * FROM Asistencia.dbo.platos where id ='" + @id + "'; ";
        //    try
        //    {
        //        SqlCommand myCommand = new SqlCommand(query, conexion);
        //        SqlDataReader myReader = myCommand.ExecuteReader();

        //        if (myReader.Read())
        //        {
        //            //MaesGen maesgen = new MaesGen();
        //            //0string titulo = myReader.GetInt32(myReader.GetOrdinal("id"));
        //            //lblid.Text = titulo.Trim();
        //            //dtpFecha.Text = myReader.GetDateTime(myReader.GetDateTime("fecha"));
        //            txtcantidad.Text = myReader.GetString(myReader.GetOrdinal("cantidad"));
        //            txtplato.Text = myReader.GetString(myReader.GetOrdinal("plato"));

        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
        //    }
        //}

        private void ui_platosupd_Load(object sender, EventArgs e)
        {
     
        }
    }
}
