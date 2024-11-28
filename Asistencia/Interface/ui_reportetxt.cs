using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;
using System.Drawing.Printing;

namespace CaniaBrava
{
    public partial class ui_reportetxt : ui_form
    {
        public string _texto;
        string _filename;
        int _tamañofuente;

        public ui_reportetxt()
        {
            InitializeComponent();
        }

        private void ui_reportetxt_Load(object sender, EventArgs e)
        {
            try
            {
                this._tamañofuente = 9;
                Funciones funciones = new Funciones();
                string ruta = Application.StartupPath + "/TEMP/";
                if (!Directory.Exists(@ruta))
                {
                    Directory.CreateDirectory(@ruta);
                }
                this._filename = ruta + funciones.generaNumeroAleatorio() + ".prn";
                Exporta exporta = new Exporta();
                rtxtDoc.Text = exporta.Ascii_FromCadena(this._texto);
                rtxtDoc.SaveFile(this._filename,
                        RichTextBoxStreamType.PlainText);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            saveFileDialog1.Filter = "txt files (*.txt)|*.txt";

            if (saveFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK
                && saveFileDialog1.FileName.Length > 0)
            {
                rtxtDoc.SaveFile(saveFileDialog1.FileName,
                    RichTextBoxStreamType.PlainText);
            }

        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {

            ui_setupprint ui_impresora = new ui_setupprint();
            ui_impresora._ruta = this._filename;
            ui_impresora._strPrinter = this._texto;
            ui_impresora.Activate();
            ui_impresora.BringToFront();
            ui_impresora.ShowDialog();
            ui_impresora.Dispose();

        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnZoomMenos_Click(object sender, EventArgs e)
        {
            if (this._tamañofuente > 5)
            {
                this._tamañofuente--;
                rtxtDoc.Font = new Font("Courier New", this._tamañofuente);
            }
        }

        private void btnZoomMas_Click(object sender, EventArgs e)
        {
            if (this._tamañofuente < 16)
            {
                this._tamañofuente++;
                rtxtDoc.Font = new Font("Courier New", this._tamañofuente);
            }
        }

        private void btnZoomNormal_Click(object sender, EventArgs e)
        {
            this._tamañofuente = 9;
            rtxtDoc.Font = new Font("Courier New", this._tamañofuente);
        }

        private void btnSeleccionar_Click(object sender, EventArgs e)
        {
            rtxtDoc.SelectAll();
        }
    }
}