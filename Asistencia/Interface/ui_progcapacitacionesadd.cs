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
    public partial class ui_progcapacitacionesadd : Form
    {
        Funciones funciones = new Funciones();
        public string _idusr;
        public string _desusr;

        public ui_progcapacitacionesadd()
        {
            InitializeComponent();

            if (funciones.VersionAssembly()) Application.ExitThread();
        }

        private void ui_accesomenu_Load(object sender, EventArgs e)
        {
            this.Text = "Opciones de Menú del Usuario : " + _desusr;
            ui_listar();
        }

        private void ui_listar()
        {
            DataTable dtMenu = new DataTable();
            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();

            string query = "select  codmenu,desmenu from menu where estado = 1 order by orden asc,codmenu asc";
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = new SqlCommand(query, conexion);
            da.Fill(dtMenu);
            OptMenu optmenu = new OptMenu();
            int indoptmenu = 0;

            foreach (DataRow row_dtMenu in dtMenu.Rows)
            {
                string codmenu = row_dtMenu["codmenu"].ToString();
                string desmenu = row_dtMenu["desmenu"].ToString();
                string descripcion = funciones.replicateCadena(" ", (2 * codmenu.Length)) + (desmenu + funciones.replicateCadena(" ", 200)).Substring(0, 200) + codmenu;
                indoptmenu = optmenu.getOptMenu(_idusr, codmenu);
                if (indoptmenu > 0)
                {
                    chklisboxMenu.Items.Add(descripcion, CheckState.Checked);
                }
                else
                {
                    chklisboxMenu.Items.Add(descripcion, CheckState.Unchecked);
                }
            }
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnGrabar_Click(object sender, EventArgs e)
        {
            OptMenu optmenu = new OptMenu();
            string codmenu = string.Empty;
            optmenu.eliminarOptMenu(_idusr);

            string query = string.Empty;
            foreach (var item in chklisboxMenu.CheckedItems)
            {
                codmenu = item.ToString().Substring(200, item.ToString().Length - 200).Trim();
                query += "INSERT INTO optmenuapp VALUES ('"+ _idusr + "','"+ codmenu + "');";
                //optmenu.actualizarOptMenu(_idusr, codmenu);
            }
            optmenu.actualizarOptMenu(query);

            MessageBox.Show("Opciones de Menú actualizadas correctamente", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.Close();
        }
    }
}