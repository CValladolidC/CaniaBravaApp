using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Drawing;
using System.Drawing.Printing;
using System.Windows.Forms;

namespace CaniaBrava
{
    public class PrintingText
    {
        private Font printFont;
        private StreamReader streamToPrint;

        private void pd_PrintPage(object sender, PrintPageEventArgs ev)
        {
            Exporta exporta = new Exporta();
            float linesPerPage = 0;
            float yPos = 0;
            int count = 0;
            float leftMargin = ev.MarginBounds.Left;
            float topMargin = ev.MarginBounds.Top;
            String line = null;
            linesPerPage = (int)(ev.MarginBounds.Height /
               printFont.GetHeight(ev.Graphics));

            while (count < linesPerPage &&
               ((line = streamToPrint.ReadLine()) != null))
            {
                if (line.Length > 0)
                {
                    if (exporta.Cadena_FromAscii(line.Substring(0, 1)).Equals(12))
                    {
                        count = (int)linesPerPage;
                    }
                }

                yPos = topMargin + (count * printFont.GetHeight(ev.Graphics));
                ev.Graphics.DrawString(line, printFont, Brushes.Black,
                   leftMargin, yPos, new StringFormat());
                count++;
            }

            if (line != null)
                ev.HasMorePages = true;
            else
                ev.HasMorePages = false;

        }

        // Print the file.
        public void Printing(string tipovisualizacion, string impresora, string filePath, string tipoletra, int tamaño, int margenizq, int margensup, bool orientacion)
        {
            try
            {
                streamToPrint = new StreamReader(filePath);
                try
                {
                    printFont = new Font(tipoletra, tamaño);
                    PrintDocument pd = new PrintDocument();
                    pd.PrinterSettings.PrinterName = impresora;

                    //pd.PrinterSettings.DefaultPageSettings.Landscape = orientacion;
                    pd.PrintPage += new PrintPageEventHandler(pd_PrintPage);
                    Margins margins = new Margins(margenizq, 0, margensup, 0);
                    pd.DefaultPageSettings.Margins = margins;

                    if (tipovisualizacion.Equals("PREVIO"))
                    {
                        PrintPreviewDialog ppd = new PrintPreviewDialog();
                        ppd.Document = pd;
                        ppd.ShowDialog();
                    }
                    else
                    {
                        pd.Print();
                    }

                }
                finally
                {
                    streamToPrint.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}