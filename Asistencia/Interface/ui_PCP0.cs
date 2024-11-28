using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CaniaBrava
{
    public partial class ui_PCP0 : Form
    {
        Process p;

        public ui_PCP0()
        {
            InitializeComponent();
        }

        private void Presionar(int tiempo, string tecla)
        {
            Thread.Sleep(tiempo * 1000);
            SendKeys.SendWait(tecla);
        }

        private void SAP()
        {
            if (p == null || p.HasExited)
            {
                string query = string.Empty;
                try
                {
                    // Start the child process.
                    // Redirect the output stream of the child process.



                    p = new Process();
                    p.StartInfo.UseShellExecute = false;
                    p.StartInfo.RedirectStandardOutput = true;
                    p.StartInfo.FileName = @"C:\Program Files (x86)\SAP\FrontEnd\SAPgui\saplogon.exe";
                    p.Start();

                    Presionar(16, "{ENTER}");
                    Presionar(3, txtUsu.Text);
                    Presionar(3, "{TAB}");
                    Presionar(2, txtPas.Text);
                    Presionar(3, "{ENTER}");
                    Presionar(3, "PCP0");
                    Presionar(3, "{ENTER}");

                    Presionar(45, "^{F5}");//Crtl + F5
                    Presionar(3, "+{TAB}");//Shit + TAB
                    Presionar(3, "{ENTER}");
                    Presionar(1, "{TAB}");
                    Presionar(1, "{TAB}");
                    Presionar(1, "{TAB}");
                    Presionar(1, "{TAB}");
                    Presionar(1, "{TAB}");
                    Presionar(2, "{UP}");
                    Presionar(1, "{UP}");
                    Presionar(1, "{UP}");
                    Presionar(1, "{F2}");
                    Presionar(1, "{F7}");
                    Presionar(3, "{ENTER}");

                    Presionar(1, "{TAB}");
                    Presionar(1, "{TAB}");
                    Presionar(1, "{TAB}");
                    Presionar(1, "{TAB}");
                    Presionar(1, "{TAB}");
                    Presionar(3, "{ENTER}");
                    Presionar(3, txtstatus.Text);
                    Presionar(1, "+{TAB}");//Shit + TAB
                    Presionar(1, "+{TAB}");//Shit + TAB
                    Presionar(1, "+{TAB}");//Shit + TAB
                    Presionar(1, "{RIGHT}");
                    Presionar(1, "{RIGHT}");
                    Presionar(3, "{ENTER}");
                    Presionar(1, "{DELETE}");
                    Presionar(1, "{TAB}");
                    Presionar(1, "{TAB}");
                    Presionar(1, "{DELETE}");
                    Presionar(1, "{TAB}");
                    Presionar(1, "{TAB}");
                    Presionar(1, "{DELETE}");
                    Presionar(1, "{TAB}");
                    Presionar(1, "{TAB}");
                    Presionar(1, "{DELETE}");
                    Presionar(1, "{TAB}");
                    Presionar(1, "{TAB}");
                    Presionar(1, "{DELETE}");
                    Presionar(1, "{TAB}");
                    Presionar(1, "{TAB}");
                    Presionar(1, "{DELETE}");
                    Presionar(3, "{F8}");
                    Presionar(1, "{TAB}");
                    Presionar(1, "{TAB}");
                    Presionar(3, txtUsu.Text);
                    Presionar(3, "{ENTER}");

                    Presionar(4, "{F2}");
                    Presionar(2, "{F2}");
                    Presionar(2, "{F5}");
                    Presionar(5, "+{F1}");
                    Presionar(40, "^+{F9}");//Ctrl + Shit + F9
                    Presionar(4, "{ENTER}");

                    Presionar(10, "+{TAB}");
                    Presionar(2, @"D:\");//D:/
                    Presionar(2, "{TAB}");
                    Presionar(2, "PCP0.txt"); //PCP0.txt
                    Presionar(2, "{TAB}");
                    Presionar(2, "{TAB}");
                    Presionar(2, "{ENTER}");
                    Presionar(5, "+{TAB}");
                    Presionar(2, "{ENTER}");

                    Presionar(2, "(%{F4})");
                    Presionar(1, "+{TAB}");
                    Presionar(2, "{ENTER}");
                    Presionar(2, "(%{F4})");

                    FileInfo fab = new FileInfo(@"D:\" + "PCP0.txt");
                    DataTable data = new DataTable();
                    if (fab.Exists)
                    {
                        string[] stringarry = System.IO.File.ReadAllLines(@"D:\" + "PCP0.txt", Encoding.Default);

                        if (stringarry.Length > 0)
                        {
                            int cc = 0;
                            var arr_ = stringarry[19].Substring(1, stringarry[19].Length - 2).Split('|');
                            List<string> LColumns = new List<string>();
                            for (int i = 0; i < arr_.Length; i++)
                            {
                                if (arr_[i].Trim().Length > 0)
                                {
                                    data.Columns.Add("Col" + cc, typeof(string));
                                    cc++;
                                }
                                else
                                {
                                    data.Columns.Add("Vacio" + i, typeof(string));
                                    LColumns.Add("Vacio" + i);
                                }
                            }
                            data.AcceptChanges();

                            System.Data.DataRow dr = null;
                            for (int i = 21; i < stringarry.Length - 25; i++)
                            {
                                stringarry[i] = stringarry[i].Replace("S/N|", "S/N");
                                var dat = stringarry[i].Substring(1, stringarry[i].Length - 2).Split('|');
                                dr = data.NewRow();
                                for (int y = 0; y < dat.Length; y++)
                                {
                                    dr[y] = dat[y].Trim();
                                }
                                data.Rows.Add(dr);
                            }
                            data.AcceptChanges();

                            query = "DELETE FROM PlanMo_New.dbo.PCP0_CUENTAS;\r\n";
                            int cccc = 0;
                            foreach (DataRow item in data.Rows)
                            {
                                query += "INSERT INTO PlanMo_New.dbo.PCP0_CUENTAS VALUES (";
                                for (int i = 0; i < item.ItemArray.Length; i++)
                                {
                                    //if (i == 7)
                                    //{
                                    //    query += "'" + DateTime.Parse(item.ItemArray[i].ToString().Replace("'", "").Replace(".", "/").ToString()).ToString("yyyy-MM-dd") + "',";
                                    //}
                                    //else
                                    {
                                        if (i == 7 || i == 8)
                                        {
                                            if (item.ItemArray[i].ToString() != string.Empty)
                                            {
                                                query += "'" + item.ItemArray[i].ToString().Replace("'", "").Replace(",", "") + "',";
                                            }
                                            else { query += "'0',"; }
                                        }
                                        else
                                        {
                                            query += "'" + item.ItemArray[i].ToString().Replace("'", "") + "',";
                                        }
                                    }
                                }
                                query += ");\r\n";
                                cccc++;
                            }

                            query = query.Replace(",);", ");");

                            SqlConnection conexion = new SqlConnection();
                            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
                            conexion.Open();

                            SqlCommand myCommand = new SqlCommand(query, conexion);
                            myCommand.CommandTimeout = 360;
                            myCommand.ExecuteNonQuery();
                            myCommand.Dispose();
                            conexion.Close();
                        }
                    }
                }
                catch (Exception ex)
                {
                    //p.Close();
                }
            }
            else
            {
                try
                {
                    //  Send app instruction to close itself
                    if (!p.CloseMainWindow())
                    {
                        //  Unable to comply - has to be put to death
                        //  Merciful people might give it a few retries 
                        //  before execution
                        p.Kill();
                    }
                }
                catch (Exception ex)
                {
                    //  Inform user about error
                }
                finally
                {
                    //  So the cycle of life can start again
                    p = null;
                }
            }
        }

        private void btnProcesar_Click(object sender, EventArgs e)
        {
            Funciones funciones = new Funciones();
            if (txtUsu.Text.Trim() == string.Empty)
            {
                MessageBox.Show("No ha especificado Usuario", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            else
            {
                if (txtPas.Text.Trim() == string.Empty)
                {
                    MessageBox.Show("No ha especificado Password", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
                else
                {
                    if (txtstatus.Text.Trim() == string.Empty)
                    {
                        MessageBox.Show("No ha especificado Estado", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    }
                    else
                    {
                        //SAP();
                        var Prueba = new SapMoPcp0.ConxSap();
                        Prueba.ConexionSap();

                    }
                }
            }
        }


        private void SAP3()
        {
            SapPcp0 WC = new SapPcp0();
            WC.Usu = txtUsu.Text.Trim();
            WC.Pas = txtPas.Text;
            backgroundWorker1.RunWorkerAsync(WC);
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            backgroundWorker1.CancelAsync();
            this.Close();
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            // Get the BackgroundWorker object that raised this event.
            System.ComponentModel.BackgroundWorker worker;
            worker = (System.ComponentModel.BackgroundWorker)sender;

            // Get the Words object and call the main method.
            SapPcp0 WC = (SapPcp0)e.Argument;
            WC.procesa(worker, e);
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            // This event handler is called when the background thread finishes.
            // This method runs on the main thread.
            if (e.Error != null)
            {
                MessageBox.Show("Error: " + e.Error.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                if (e.Cancelled)
                {
                    MessageBox.Show("Proceso Cancelado", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                }
                else
                {
                    MessageBox.Show("Proceso Finalizado con éxito", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    btnProcesar.Enabled = true; btnCancelar.Enabled = true; 
                }
            }
        }

     
    }
}
