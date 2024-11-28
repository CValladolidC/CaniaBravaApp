using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Excel = Microsoft.Office.Interop.Excel;
using System.IO;
using System.Data;
using System.Collections;
using System.Windows.Forms;
using System.Diagnostics;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace CaniaBrava
{
    class Exporta
    {
        /// <summary>
        /// Exporta a un archivo Excel desde un Datatable
        /// </summary>
        /// <param name="dt">Datatable</param>
        public void Excel_FromDataTable(System.Data.DataTable dt)
        {
            Excel.Application excel = new Excel.Application();
            Excel.Workbook workbook = excel.Application.Workbooks.Add(true);

            int iCol = 0;
            foreach (DataColumn c in dt.Columns)
            {
                iCol++;
                excel.Cells[1, iCol] = c.ColumnName;
            }

            int iRow = 0;
            foreach (DataRow r in dt.Rows)
            {
                iRow++;
                iCol = 0;
                foreach (DataColumn c in dt.Columns)
                {
                    iCol++;
                    excel.Cells[iRow + 1, iCol] = r[c.ColumnName];
                }
            }

            object missing = System.Reflection.Missing.Value;

            workbook.SaveAs("MyExcelWorkBook.xls",
                Excel.XlFileFormat.xlXMLSpreadsheet, missing, missing,
                false, false, Excel.XlSaveAsAccessMode.xlNoChange,
                missing, missing, missing, missing, missing);

            excel.Visible = true;
            Excel.Worksheet worksheet = (Excel.Worksheet)excel.ActiveSheet;
            ((Excel._Worksheet)worksheet).Activate();
            
            //((Excel._Application)excel).Quit();
        }

        /// <summary>
        /// Exporta a un archivo Excel con una ruta especificada a partir de un Datatable
        /// </summary>
        /// <param name="dt">Datatable</param>
        /// <param name="filename">Ruta y Nombre del Archivo</param>
        public void Excel_FromDataTable(DataTable dt, string filename)
        {
            Excel.Application excel = new Excel.Application();
            Excel.Workbook workbook = excel.Application.Workbooks.Add(true);

            int iCol = 0;
            foreach (DataColumn c in dt.Columns)
            {
                iCol++;
                excel.Cells[1, iCol] = c.ColumnName;
            }

            int iRow = 0;
            foreach (DataRow r in dt.Rows)
            {
                iRow++;
                iCol = 0;

                foreach (DataColumn c in dt.Columns)
                {
                    iCol++;

                    excel.Cells[iRow + 1, iCol] = r[c.ColumnName];
                }
            }

            object missing = System.Reflection.Missing.Value;

            workbook.SaveAs(filename,
                Excel.XlFileFormat.xlXMLSpreadsheet, missing, missing,
                false, false, Excel.XlSaveAsAccessMode.xlNoChange,
                missing, missing, missing, missing, missing);

            //excel.Visible = true;
            //Excel.Worksheet worksheet = (Excel.Worksheet)excel.ActiveSheet;
            //((Excel._Worksheet)worksheet).Activate();

            ((Excel._Application)excel).Quit();
        }

        public void Excel_FromDataGridView(System.Windows.Forms.DataGridView dgvdetalle)
        {
            try
            {
                ArrayList titulos = new ArrayList();
                System.Data.DataTable datosTabla = new System.Data.DataTable();

                SaveFileDialog CuadroDialogo = new SaveFileDialog();
                CuadroDialogo.DefaultExt = "xls";
                CuadroDialogo.Filter = "xls file(*.xls)|*.xls";
                CuadroDialogo.AddExtension = true;
                CuadroDialogo.RestoreDirectory = true;
                CuadroDialogo.Title = "Guardar";
                CuadroDialogo.InitialDirectory = @"c:\";
                if (CuadroDialogo.ShowDialog() == DialogResult.OK)
                {
                    OtrosFormatos OF = new OtrosFormatos(CuadroDialogo.FileName);
                    foreach (DataGridViewColumn item in dgvdetalle.Columns)
                    {
                        titulos.Add(item.HeaderText);
                        datosTabla.Columns.Add();
                    }

                    foreach (DataGridViewRow item in dgvdetalle.Rows)
                    {
                        DataRow rowx = datosTabla.NewRow();
                        datosTabla.Rows.Add(rowx);
                    }

                    foreach (DataGridViewColumn item in dgvdetalle.Columns)
                    {
                        foreach (DataGridViewRow itemx in dgvdetalle.Rows)
                        {
                            datosTabla.Rows[itemx.Index][item.Index] = dgvdetalle[item.Index, itemx.Index].Value;
                        }
                    }

                    OF.Export(titulos, datosTabla);

                    Process.Start(OF.xpath);
                }
                else
                {
                    MessageBox.Show("No se pudo guardar Datos .. ", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void Excel_FromDataGridView(System.Windows.Forms.DataGridView dgvdetalle, bool estilo)
        {
            try
            {
                ArrayList titulos = new ArrayList();
                System.Data.DataTable datosTabla = new System.Data.DataTable();

                SaveFileDialog CuadroDialogo = new SaveFileDialog();
                CuadroDialogo.DefaultExt = "xls";
                CuadroDialogo.Filter = "xls file(*.xls)|*.xls";
                CuadroDialogo.AddExtension = true;
                CuadroDialogo.RestoreDirectory = true;
                CuadroDialogo.Title = "Guardar";
                CuadroDialogo.InitialDirectory = @"c:\";
                if (CuadroDialogo.ShowDialog() == DialogResult.OK)
                {
                    OtrosFormatos OF = new OtrosFormatos(CuadroDialogo.FileName);
                    foreach (DataGridViewColumn item in dgvdetalle.Columns)
                    {
                        titulos.Add(item.HeaderText);
                        datosTabla.Columns.Add();
                    }

                    foreach (DataGridViewRow item in dgvdetalle.Rows)
                    {
                        DataRow rowx = datosTabla.NewRow();
                        datosTabla.Rows.Add(rowx);
                    }

                    foreach (DataGridViewColumn item in dgvdetalle.Columns)
                    {
                        foreach (DataGridViewRow itemx in dgvdetalle.Rows)
                        {
                            datosTabla.Rows[itemx.Index][item.Index] = dgvdetalle[item.Index, itemx.Index].Value;
                        }
                    }

                    OF.Export(titulos, datosTabla, estilo);

                    Process.Start(OF.xpath);
                }
                else
                {
                    MessageBox.Show("No se pudo guardar Datos .. ", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public int Cadena_FromAscii(string s)
        {
            return Encoding.ASCII.GetBytes(s)[0];
        }

        public string Ascii_FromCadena(string str)
        {
            Encoding ascii = Encoding.ASCII;
            Byte[] arrayBytes = ascii.GetBytes(str);
            string cadena = "";
            foreach (Char c in arrayBytes)
            {
                if (c.ToString().Equals("\n"))
                {
                    cadena += "\r\n";

                }

                else
                {
                    if (c.ToString().Equals("\f"))
                    {
                        cadena += "\r\n" + "\r\n" + "\r\n" + "\r\f";
                    }
                    else
                    {
                        cadena += c.ToString();
                    }

                }
            }

            return cadena;
        }

        public void Pdf_FromDataGridView(System.Windows.Forms.DataGridView dgvDatos, int nrocolnoreq)
        {
            try
            {

                SaveFileDialog CuadroDialogo = new SaveFileDialog();
                CuadroDialogo.DefaultExt = "pdf";
                CuadroDialogo.Filter = "pdf file(*.pdf)|*.pdf";
                CuadroDialogo.AddExtension = true;
                CuadroDialogo.RestoreDirectory = true;
                CuadroDialogo.Title = "Guardar";
                CuadroDialogo.InitialDirectory = @"c:\";
                if (CuadroDialogo.ShowDialog() == DialogResult.OK)
                {
                    Document doc = new Document(PageSize.A4.Rotate(), 5, 5, 5, 5);
                    string filename = CuadroDialogo.FileName;
                    FileStream file = new FileStream(filename, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite);
                    PdfWriter.GetInstance(doc, file); doc.Open();
                    GenerarDocumento(doc, dgvDatos, nrocolnoreq);
                    doc.Close();
                    Process.Start(filename);
                }
                else
                {
                    MessageBox.Show("No se pudo guardar Datos .. ", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void GenerarDocumento(Document document, System.Windows.Forms.DataGridView dataGridView1, int nrocolnoreq)
        {
            //se crea un objeto PdfTable con el numero de columnas del   
            //dataGridView  
            PdfPTable datatable = new PdfPTable(dataGridView1.ColumnCount - nrocolnoreq);
            //asignamos algunas propiedades para el diseño del pdf  
            datatable.DefaultCell.Padding = 3;
            float[] headerwidths = GetTamañoColumnas(dataGridView1, nrocolnoreq);
            datatable.SetWidths(headerwidths);
            datatable.WidthPercentage = 100;
            datatable.DefaultCell.BorderWidth = 2;
            datatable.DefaultCell.HorizontalAlignment = Element.ALIGN_CENTER;
            //SE GENERA EL ENCABEZADO DE LA TABLA EN EL PDF  
            for (int i = 0; i < dataGridView1.ColumnCount - nrocolnoreq; i++)
            {
                datatable.AddCell(dataGridView1.Columns[i].HeaderText);
            }
            datatable.HeaderRows = 1;
            datatable.DefaultCell.BorderWidth = 1;
            //SE GENERA EL CUERPO DEL PDF  
            for (int i = 0; i < dataGridView1.RowCount; i++)
            {
                for (int j = 0; j < dataGridView1.ColumnCount - nrocolnoreq; j++)
                {
                    datatable.AddCell(dataGridView1[j, i].Value.ToString());
                }
                datatable.CompleteRow();
            }
            //SE AGREGAR LA PDFPTABLE AL DOCUMENTO 
            document.Add(datatable);
        }

        private float[] GetTamañoColumnas(DataGridView dg, int nrocolnoreq)
        {
            float[] values = new float[dg.ColumnCount - nrocolnoreq];
            for (int i = 0; i < dg.ColumnCount - nrocolnoreq; i++)
            {
                values[i] = (float)dg.Columns[i].Width;
            } return values;
        }
    }
}