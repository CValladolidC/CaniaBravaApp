using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CaniaBrava
{
    public partial class ui_regplatoproducidos : Form
    {
        public ui_regplatoproducidos()
        {
            InitializeComponent();
        }

        private void btnEjecutar_Click(object sender, EventArgs e)
        {
            Funciones fn = new Funciones();
            string tipo = fn.getValorComboBox(cmbTipo, 2);

            if (tipo != string.Empty)
            {
                Encoding.ASCII.GetBytes("$$53cur17ykeyp4ss$$");
                byte[] bytes1 = Encoding.ASCII.GetBytes("$$53cur17ykeyp4ss$$");
                string str = "#d3vf0rC3_N0C0PYC0D3$";

                if (tipo == "01")
                {
                    string CryptoTextValue = txtValor.Text.Trim();
                    try
                    {
                        byte[] bytes2 = Encoding.UTF8.GetBytes(str.Substring(0, 8));
                        DESCryptoServiceProvider cryptoServiceProvider = new DESCryptoServiceProvider();
                        byte[] bytes3 = Encoding.UTF8.GetBytes(CryptoTextValue);
                        MemoryStream memoryStream = new MemoryStream();
                        CryptoStream cryptoStream = new CryptoStream((Stream)memoryStream, cryptoServiceProvider.CreateEncryptor(bytes2, bytes1), CryptoStreamMode.Write);
                        cryptoStream.Write(bytes3, 0, bytes3.Length);
                        cryptoStream.FlushFinalBlock();
                        txtResultado.Text = Convert.ToBase64String(memoryStream.ToArray());
                    }
                    catch (Exception ex)
                    {
                        txtResultado.Text = ex.Message;
                    }
                }
                else
                {
                    string TextValue = txtValor.Text.Trim();
                    byte[] numArray = new byte[TextValue.Length + 1];
                    try
                    {
                        byte[] bytes2 = Encoding.UTF8.GetBytes(str.Substring(0, 8));
                        DESCryptoServiceProvider cryptoServiceProvider = new DESCryptoServiceProvider();
                        byte[] buffer = Convert.FromBase64String(TextValue);
                        MemoryStream memoryStream = new MemoryStream();
                        CryptoStream cryptoStream = new CryptoStream((Stream)memoryStream, cryptoServiceProvider.CreateDecryptor(bytes2, bytes1), CryptoStreamMode.Write);
                        cryptoStream.Write(buffer, 0, buffer.Length);
                        cryptoStream.FlushFinalBlock();
                        txtResultado.Text = Encoding.UTF8.GetString(memoryStream.ToArray());
                    }
                    catch (Exception ex)
                    {
                        txtResultado.Text = ex.Message;
                    }
                }

                //Encoding.ASCII.GetBytes("$$53cur17ykeyp4ss$$");
                //byte[] bytes1 = Encoding.ASCII.GetBytes("$$53cur17ykeyp4ss$$");
                //string str = "#d3vf0rC3_N0C0PYC0D3$";SERHISTORI
            }
            else
            {
                MessageBox.Show("No ha especificado Tipo", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                cmbTipo.Focus();
            }
        }

        private void btnClean_Click(object sender, EventArgs e)
        {
            txtValor.Clear();
            txtResultado.Clear();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
