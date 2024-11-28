using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Printing;
using System.Drawing.Text;

namespace CaniaBrava
{
    public partial class ui_setupprint : ui_form
    {
        public string _ruta;
        public string _strPrinter;

        public ui_setupprint()
        {
            InitializeComponent();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ui_setupprint_Load(object sender, EventArgs e)
        {
            GetPrinters(cmbImpresora);
            GetFont(cmbTipoLetra);
            nudTamaño.Value = 7;
            txtMargenIzq.Text = "3";
            txtMargenSup.Text = "1";
        }

        public void GetPrinters(ComboBox cmb)
        {
            string Default = string.Empty;

            for (int i = 0; i < PrinterSettings.InstalledPrinters.Count; i++)
            {
                PrinterSettings a = new PrinterSettings();
                a.PrinterName = PrinterSettings.InstalledPrinters[i].ToString();
                if (a.IsDefaultPrinter)
                {
                    Default = a.PrinterName;
                }
                cmb.Items.Add(a.PrinterName);

            }
            cmb.Text = Default;

        }

        public void GetFont(ComboBox cmb)
        {
            string familyName;
            FontFamily[] fontFamilies;
            InstalledFontCollection installedFontCollection = new InstalledFontCollection();
            fontFamilies = installedFontCollection.Families;
            int count = fontFamilies.Length;
            for (int j = 0; j < count; ++j)
            {
                familyName = fontFamilies[j].Name;
                cmb.Items.Add(familyName);
            }
            cmbTipoLetra.Text = "Courier New";
        }

        private void rbGrafico_CheckedChanged(object sender, EventArgs e)
        {
            if (rbGrafico.Checked)
            {
                cmbTipoLetra.Enabled = true;
                nudTamaño.Enabled = true;
                cmbTipoLetra.Text = "Courier New";
                nudTamaño.Value = 7;
                grpMargen.Enabled = true;
                grpOrientacion.Enabled = true;
                btnPrevio.Enabled = true;

            }
            else
            {
                cmbTipoLetra.Enabled = false;
                nudTamaño.Enabled = false;
                cmbTipoLetra.Text = "";
                nudTamaño.Value = 7;
                grpMargen.Enabled = false;
                grpOrientacion.Enabled = false;
                btnPrevio.Enabled = false;
            }

        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            string impresora = cmbImpresora.Text.Trim();

            if (rbGrafico.Checked)
            {
                string tipoletra = cmbTipoLetra.Text.Trim();
                int tamaño = int.Parse(nudTamaño.Value.ToString());
                int margenizq = int.Parse(txtMargenIzq.Text);
                int margensup = int.Parse(txtMargenSup.Text);
                bool orientacion;
                if (rbVertical.Checked)
                {
                    orientacion = false;
                }
                else
                {
                    orientacion = true;
                }

                PrintingText printer = new PrintingText();
                printer.Printing("IMPRESORA", impresora, this._ruta, tipoletra, tamaño, margenizq, margensup, orientacion);
            }
            else
            {
                string strPrinter = this._strPrinter;
                RawPrinterHelper.SendStringToPrinter(impresora, strPrinter);
            }

        }

        private void rbTexto_CheckedChanged(object sender, EventArgs e)
        {
            if (rbTexto.Checked)
            {
                cmbTipoLetra.Enabled = false;
                nudTamaño.Enabled = false;
                cmbTipoLetra.Text = "";
                nudTamaño.Value = 7;
                grpMargen.Enabled = false;
                grpOrientacion.Enabled = false;
                btnPrevio.Enabled = false;


            }
            else
            {
                cmbTipoLetra.Enabled = true;
                nudTamaño.Enabled = true;
                cmbTipoLetra.Text = "Courier New";
                nudTamaño.Value = 7;
                grpMargen.Enabled = true;
                grpOrientacion.Enabled = true;
                btnPrevio.Enabled = true;
            }
        }

        private void rbVertical_CheckedChanged(object sender, EventArgs e)
        {
            if (rbVertical.Checked)
            {
                pbVertical.Visible = true;
                pbHorizontal.Visible = false;
            }
            else
            {
                pbVertical.Visible = false;
                pbHorizontal.Visible = true;
            }
        }

        private void rbHorizontal_CheckedChanged(object sender, EventArgs e)
        {
            if (rbHorizontal.Checked)
            {
                pbVertical.Visible = false;
                pbHorizontal.Visible = true;

            }
            else
            {
                pbVertical.Visible = true;
                pbHorizontal.Visible = false;
            }
        }

        private void btnPrevio_Click(object sender, EventArgs e)
        {
            string impresora = cmbImpresora.Text.Trim();
            string tipoletra = cmbTipoLetra.Text.Trim();
            int tamaño = int.Parse(nudTamaño.Value.ToString());
            int margenizq = int.Parse(txtMargenIzq.Text);
            int margensup = int.Parse(txtMargenSup.Text);
            bool orientacion;
            if (rbVertical.Checked)
            {
                orientacion = false;
            }
            else
            {
                orientacion = true;
            }

            PrintingText printer = new PrintingText();
            printer.Printing("PREVIO", impresora, this._ruta, tipoletra, tamaño, margenizq, margensup, orientacion);
        }
    }
}