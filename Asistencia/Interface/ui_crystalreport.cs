using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CaniaBrava.cr;
using CrystalDecisions.CrystalReports.Engine;

namespace CaniaBrava
{
    public partial class ui_crystalreport : Form
    {
        public ui_crystalreport()
        {
            InitializeComponent();
        }

        public void asignaDataTable(ReportClass cr, DataTable dt)
        {
            try
            {
                cr.SetDataSource(dt);
                crvReporte.ReportSource = cr;
                //crvReporte.DataBindings();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
