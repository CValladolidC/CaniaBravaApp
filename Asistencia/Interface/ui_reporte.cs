using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CrystalDecisions.CrystalReports.Engine;

namespace CaniaBrava
{
    public partial class ui_reporte : Form
    {
        public ui_reporte()
        {
            InitializeComponent();
        }

        private void crvReporte_Load(object sender, EventArgs e)
        {

        }

        public void asignaDataSet(ReportClass cr, DataSet ds)
        {
            cr.SetDataSource(ds);
            crvReporte.ReportSource = cr;

        }

        public void asignaDataTable(ReportClass cr, DataTable dt)
        {
            try
            {
                cr.SetDataSource(dt);
                crvReporte.ReportSource = cr;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}