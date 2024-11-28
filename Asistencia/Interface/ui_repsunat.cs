using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace CaniaBrava
{
    public partial class ui_repsunat : Form
    {
        GlobalVariables gv = new GlobalVariables();
        Funciones fn = new Funciones();
        string _codcia;
        String query = String.Empty;

        private TextBox TextBoxActivo;
        private Label LabelActivo;

        public TextBox _TextBoxActivo
        {
            get { return TextBoxActivo; }
            set { TextBoxActivo = value; }
        }

        public Label _LabelActivo
        {
            get { return LabelActivo; }
            set { LabelActivo = value; }
        }

        public ui_repsunat()
        {
            InitializeComponent();
            load_ui_repsunat();
        }

        private void load_ui_repsunat()
        {
            this._codcia = gv.getValorCia();
            query = " SELECT codalma AS clave,desalma AS descripcion ";
            query += "FROM alalma WHERE codcia='" + _codcia + "' AND estado='V' ORDER BY 1 ASC;";
            fn.listaComboBox(query, cmbAlmacen, "");
            txtCodigo.Focus();
        }

        private void btnVisualiza_Click(object sender, EventArgs e)
        {
            string alma = fn.getValorComboBox(cmbAlmacen, 2);
            Microsoft.Office.Interop.Excel.Application excelApplication = new Microsoft.Office.Interop.Excel.Application();
            excelApplication.DisplayAlerts = false;
            Microsoft.Office.Interop.Excel.Workbook workBook = excelApplication.Workbooks.Add();

            Microsoft.Office.Interop.Excel.Worksheet vstoWorksheet = (Microsoft.Office.Interop.Excel.Worksheet)workBook.Worksheets[1];

            proccButtons(false);
            string fileExtension = fn.GetDefaultExtension(excelApplication);

            string filename = string.Format("{0}\\Libro1{1}", Environment.CurrentDirectory, fileExtension);
            if (!File.Exists(filename))
            {
                fn.form_excel_kardex(_codcia, alma, txtCodigo.Text, txtFechaIni.Text, txtFechaFin.Text, vstoWorksheet);

                workBook.SaveAs(filename);
                excelApplication.Quit();
                MessageBox.Show("Exportación Correcta.. Favor de Guardar File", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);

                FileInfo fi = new FileInfo(filename);
                System.Diagnostics.Process.Start(filename);
            }
            else
            {
                FileStream fs = null;
                try
                {
                    using (fs = File.OpenRead(filename))
                    {
                        if (!fs.Equals(null))
                        {
                            fs.Close();
                            fn.form_excel_kardex(_codcia, alma, txtCodigo.Text, txtFechaIni.Text, txtFechaFin.Text, vstoWorksheet);

                            workBook.SaveAs(filename);
                            excelApplication.Quit();
                            MessageBox.Show("Exportación Correcta.. Favor de Guardar File", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            FileInfo fi = new FileInfo(filename);
                            System.Diagnostics.Process.Start(filename);
                        }
                    }
                }
                catch
                {
                    if (fs == null)
                    {
                        MessageBox.Show("Archivo Excel en Ejecucion... Cerrar Excel Formato 13.1 Sunat",
                            "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }

            proccButtons(true);
        }

        private void proccButtons(bool x)
        {
            cmbAlmacen.Enabled = x;
            txtCodigo.Enabled = x;
            txtFechaIni.Enabled = x;
            txtFechaFin.Enabled = x;
            lblDescripArticulo.Visible = x;
            lblcargando.Visible = (x.Equals(false)) ? true : false;
            btnVisualiza.Enabled = x;
            btnSalir.Enabled = x;
        }

        private void txtCodigo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                lblDescripArticulo.Visible = false;
                this._TextBoxActivo = txtCodigo;
                this._LabelActivo = lblDescripArticulo;

                ui_viewarti ui_viewarti = new ui_viewarti();
                ui_viewarti._FormPadre = this;
                ui_viewarti._codcia = this._codcia;
                ui_viewarti._clasePadre = "ui_repsunat";
                ui_viewarti._condicionAdicional = string.Empty;
                ui_viewarti.BringToFront();
                ui_viewarti.ShowDialog();
                ui_viewarti.Dispose();
            }
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtCodigo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                if (!txtCodigo.Text.Equals(String.Empty))
                {
                    lblDescripArticulo.Visible = true;
                    e.Handled = true;
                    SendKeys.Send("{TAB}");
                }
            }
        }

        private void txtFechaIni_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                if (UtiFechas.IsDate(txtFechaIni.Text))
                {
                    e.Handled = true;
                    SendKeys.Send("{TAB}");
                }
                else
                {
                    MessageBox.Show("Fecha Final no válida", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    e.Handled = true;
                    txtFechaIni.Clear();
                    txtFechaIni.Focus();
                }
            }
        }

        private void txtFechaFin_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                if (UtiFechas.IsDate(txtFechaFin.Text))
                {
                    e.Handled = true;
                    btnVisualiza.Tag = 0;
                }
                else
                {
                    MessageBox.Show("Fecha Final no válida", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    e.Handled = true;
                    txtFechaFin.Clear();
                    txtFechaFin.Focus();
                }
            }
        }
    }
}