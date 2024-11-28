using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CaniaBrava
{
    public partial class ui_configsis : Form
    {
        public ui_configsis()
        {
            InitializeComponent();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnGrabar_Click(object sender, EventArgs e)
        {
            string maxfilabol = txtMaxFilaBol.Text;
            string betweenbol = txtBetweenBol.Text;
            string nrobolpag = txtNroBolPag.Text;
            string maxfilabolwin = txtMaxFilaBolWin.Text;

            Configsis configsis = new Configsis();
            configsis.updConfigsis(maxfilabol, betweenbol, nrobolpag, maxfilabolwin);
            ui_load();
            MessageBox.Show("Parámetros guardados ..", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void ui_configsis_Load(object sender, EventArgs e)
        {
            ui_load();
        }

        private void ui_load()
        {
            Configsis configsis = new Configsis();
            txtMaxFilaBol.Text = configsis.consultaConfigSis("MAXFILABOL");
            txtBetweenBol.Text = configsis.consultaConfigSis("BETWEENBOL");
            txtNroBolPag.Text = configsis.consultaConfigSis("NROBOLPAG");
            txtMaxFilaBolWin.Text = configsis.consultaConfigSis("MAXFILABOLWIN");

        }
    }
}