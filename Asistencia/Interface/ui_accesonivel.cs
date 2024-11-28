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
    public partial class ui_accesonivel : Form
    {
        public string _idusr;
        public string _desusr;

        public ui_accesonivel()
        {
            InitializeComponent();
        }

        private void ui_accesomenu_Load(object sender, EventArgs e)
        {
            lblUsuario.Text = this._desusr;

            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();

            string query = "SELECT * FROM usrfilenivel WHERE idusr='" + this._idusr + "'";
            int nivel = 0;

            try
            {
                SqlCommand myCommand = new SqlCommand(query, conexion);
                SqlDataReader odr = myCommand.ExecuteReader();

                while (odr.Read())
                {
                    nivel = odr.GetInt32(odr.GetOrdinal("nivel"));
                }

                odr.Close();
                myCommand.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            finally { conexion.Close(); }

            switch (nivel)
            {
                case 1: RD01.Checked = true; break;
                case 2: RD02.Checked = true; break;
                case 3: RD03.Checked = true; break;
                case 4: RD04.Checked = true; break;
                default: RD01.Checked = true; break;
            }
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnGrabar_Click(object sender, EventArgs e)
        {
            int valor = 0;
            if (RD01.Checked) { valor = 1; }
            if (RD02.Checked) { valor = 2; }
            if (RD03.Checked) { valor = 3; }
            if (RD04.Checked) { valor = 4; }

            string query = "DELETE FROM usrfilenivel WHERE idusr = '" + this._idusr + "'";
            query += "INSERT INTO usrfilenivel VALUES ('" + this._idusr + "', " + valor + ")";

            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();
            
            try
            {
                SqlCommand myCommand = new SqlCommand(query, conexion);
                myCommand.ExecuteNonQuery();
                myCommand.Dispose();
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            conexion.Close();

            MessageBox.Show("Asignacion de nivel de aprobacion exitoso", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.Close();
        }
    }
}