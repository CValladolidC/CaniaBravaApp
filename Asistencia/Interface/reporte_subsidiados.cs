using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

using System.Configuration;

namespace CaniaBrava
{
    public partial class reporte_subsidiados : Form
    {
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

        public reporte_subsidiados()
        {
            InitializeComponent();
        }

        private void reporte_subsidiados_Load(object sender, EventArgs e)
        {
            CiaFile usefil = new CiaFile();
            cmbcia.Text = usefil.ui_getDatosCiaFile(GlobalVariables.idcia, "DESCRIPCION");
            txtpersonal.Enabled = false;
            checkBox1.Checked = false;

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                txtpersonal.Enabled = true;
            }
            else
            {

                txtpersonal.Enabled = false;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string idcia = GlobalVariables.idcia;
            string anio = txtanio.Text;
            string idpersonal = txtpersonal.Text.Trim();
            string query = string.Empty;

            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();
            SqlDataAdapter da = new SqlDataAdapter();
            DataTable regsubs = new DataTable();
            if (radioButton1.Checked)
            {
                query = "select  d.idperplan,d.anio,d.messem ,concat(apepat,' ',apemat,' ',nombres) as vnombres,d.diassubsi as diasum from dataplan d inner join perplan p";
                query = query + " on d.idperplan=p.idperplan and d.idtipoper=p.idtipoper and d.idcia=p.idcia";
                query = query + " where d.diassubsi>0 and d.idperplan like concat('%','" + @idpersonal + "','%') and d.anio like concat('%','" + @anio + "','%') and d.idcia='" + @idcia + "'";
                query = query + " order by d.anio,d.idcia,d.idperplan,d.idtipoper";
                da.SelectCommand = new SqlCommand(query, conexion);
                da.Fill(regsubs);
                cr.crsubsidiados cr = new cr.crsubsidiados();
                ui_reporte ui_reporte = new ui_reporte();
                ui_reporte.asignaDataTable(cr, regsubs);
                ui_reporte.Activate();
                ui_reporte.BringToFront();
                ui_reporte.ShowDialog();
                ui_reporte.Dispose();

            }

            else
            {
                query = "select  d.idperplan,d.anio,d.messem ,concat(apepat,' ',apemat,' ',nombres) as vnombres,ifnull(sum(d.diassubsi),0) as diasum from dataplan d inner join perplan p";
                query = query + " on d.idperplan=p.idperplan and d.idtipoper=p.idtipoper and d.idcia=p.idcia";
                query = query + " where d.diassubsi>0 and d.idperplan like concat('%','" + @idpersonal + "','%') and d.anio like concat('%','" + @anio + "','%') and d.idcia='" + @idcia + "' group by  d.anio,d.idcia,d.idperplan,d.idtipoper";
                query = query + " order by d.anio,d.idcia,d.idperplan,d.idtipoper";

                da.SelectCommand = new SqlCommand(query, conexion);
                da.Fill(regsubs);
                cr.crsubsidiados cr = new cr.crsubsidiados();
                ui_reporte ui_reporte = new ui_reporte();
                ui_reporte.asignaDataTable(cr, regsubs);
                ui_reporte.Activate();
                ui_reporte.BringToFront();
                ui_reporte.ShowDialog();
                ui_reporte.Dispose();
            }











        }

        private void txtpersonal_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                GlobalVariables gv = new GlobalVariables();
                Funciones fn = new Funciones();
                string idcia = gv.getValorCia();
                string cadenaBusqueda = string.Empty;
                this._TextBoxActivo = txtpersonal;


                FiltrosMaestros filtros = new FiltrosMaestros();
                filtros.filtrarPerPlan("reporte_subsidiados", this, txtpersonal, idcia, "", "", "", "");
            }

        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {
                radioButton2.Checked = false;
            }
            else
            {

                radioButton2.Checked = true;

            }
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton2.Checked)
            {
                radioButton1.Checked = false;
            }
            else
            {

                radioButton1.Checked = true;

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}