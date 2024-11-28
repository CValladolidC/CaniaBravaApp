using System;
using System.Collections.Generic;
using System.Drawing;
using System.Net;
using System.Net.Mail;
using System.Windows.Forms;

namespace CaniaBrava
{
    public class PopupShow
    {
        private static Form prompt { get; set; }

        public static string GetUserInput(string instructions, string caption)
        {
            string sUserInput = "";
            prompt = new Form() //create a new form at run time
            {
                Width = 400,
                Height = 240,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                Text = caption,
                StartPosition = FormStartPosition.CenterScreen,
                TopMost = true
            };
            //create a label for the form which will have instructions for user input
            Label lblTitle = new Label() { Left = 50, Top = 20, Padding = new Padding(0, 10, 0, 0), Text = instructions, Dock = DockStyle.Top, TextAlign = ContentAlignment.TopCenter };
            TextBox txtTextInput = new TextBox() { Left = 50, Top = 50, Width = 300, Height = 100, Multiline = true };

            ////////////////////////////OK button
            Button btnOK = new Button() { Text = "Enviar", Left = 100, Width = 100, Top = 170, DialogResult = DialogResult.OK };
            btnOK.Click += (sender, e) =>
            {
                if (string.IsNullOrEmpty(txtTextInput.Text))
                {
                    MessageBox.Show("Debe ingresar un comentario..", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); return;
                }

                sUserInput = txtTextInput.Text;
                prompt.Close();
            };
            prompt.Controls.Add(txtTextInput);
            prompt.Controls.Add(btnOK);
            prompt.Controls.Add(lblTitle);
            prompt.AcceptButton = btnOK;
            ///////////////////////////////////////

            //////////////////////////Cancel button
            Button btnCancel = new Button() { Text = "Cancelar", Left = 200, Width = 100, Top = 170, DialogResult = DialogResult.Cancel };
            btnCancel.Click += (sender, e) =>
            {
                sUserInput = String.Empty;
                prompt.Close();
            };
            prompt.Controls.Add(btnCancel);
            prompt.CancelButton = btnCancel;
            ///////////////////////////////////////

            prompt.ShowDialog();
            return sUserInput;
        }

        public void Dispose() { prompt.Dispose(); }

        public static void SendCorreoSolProg(string asunto, string cuerpo, string de, List<UsrFile> para, 
            string nameusr, string to)
        {
            try
            {
                MailMessage msg = new MailMessage();
                msg.From = new MailAddress(FirsLetter(nameusr) + "<" + de.ToLower() + ">");

                foreach (var item in para)
                {
                    msg.To.Add(FirsLetter(item.desusr) + "<" + item.email.ToLower() + ">");
                }

                msg.Subject = asunto;

                string htmlString = @"<html>
                      <body style='font-family: Calibri, Candara, Segoe, Segoe UI, Optima, Arial, sans-serif;'>
                      <p>Estimado " + (to == string.Empty ? FirsLetter(para[0].desusr) : to) + @",</p>
                      <p></p>
                      <p>" + cuerpo + @"</p>
                      <p></p>
                      <p>Saludos,<br>" + FirsLetter(nameusr) + @"</br></p>
                      </body>
                      </html>";

                msg.Body = htmlString;
                msg.IsBodyHtml = true;

                SmtpClient smt = new SmtpClient();
                smt.Host = "10.72.1.71";
                System.Net.NetworkCredential ntcd = new NetworkCredential();
                smt.Port = 25;
                smt.Credentials = ntcd;
                smt.Send(msg);
                MessageBox.Show("Mensaje Enviado a: " + (to == string.Empty ? FirsLetter(para[0].desusr) : to), "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Exception caught in CreateTestMessage2(): {0}", ex.ToString()));
            }
        }

        private static string FirsLetter(string cadena)
        {
            string cadena_ = string.Empty;
            var cad = cadena.Split(' ');

            if (cad.Length > 0)
            {
                foreach (var item in cad)
                {
                    string firs = item.Substring(0, 1);
                    cadena_ += firs + item.Substring(1, item.Length - 1).ToLower() + " ";
                }
            }
            return cadena_.Trim();
        }
    }
}
