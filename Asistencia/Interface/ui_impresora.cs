using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Printing;


namespace CaniaBrava
{
    public partial class ui_impresora : Form
    {
        public ui_impresora()
        {
            InitializeComponent();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public void GetPrinters()
        {
            string Default=string.Empty;
            
            for (int i = 0; i < PrinterSettings.InstalledPrinters.Count; i++)
            {
                PrinterSettings a = new PrinterSettings();
                a.PrinterName = PrinterSettings.InstalledPrinters[i].ToString();
                if (a.IsDefaultPrinter)
                {
                    Default = a.PrinterName;
                }
                cmbImpresora.Items.Add(a.PrinterName);
               
            }
            cmbImpresora.Text = Default;

        }

        private void ui_impresora_Load(object sender, EventArgs e)
        {
            GetPrinters();
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            string impresora = cmbImpresora.Text.Trim();
            GlobalVariables gv = new GlobalVariables();
            gv.setPrinter(impresora);
            this.Close();
        }
    }
}
