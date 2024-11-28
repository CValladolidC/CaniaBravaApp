using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CaniaBrava
{
    class SapPcp0
    {
        Process p;
        public string Usu;
        public string Pas;

        private void Esperar(int tiempo)
        {
            Thread.Sleep(tiempo);
        }

        public void procesa(System.ComponentModel.BackgroundWorker worker, DoWorkEventArgs e)
        {
            if (p == null || p.HasExited)
            {
                try
                {
                    // Start the child process.
                    // Redirect the output stream of the child process.
                    p = new Process();
                    p.StartInfo.UseShellExecute = false;
                    p.StartInfo.RedirectStandardOutput = true;
                    p.StartInfo.FileName = @"C:\Program Files (x86)\SAP\FrontEnd\SAPgui\saplogon.exe";
                    p.Start();

                    Esperar(16000);

                    SendKeys.SendWait("{ENTER}");
                    Esperar(3000);
                    SendKeys.SendWait(Usu);
                    Esperar(3000);
                    SendKeys.SendWait("{TAB}");
                    Esperar(1000);
                    SendKeys.SendWait(Pas);
                    Esperar(3000);
                    SendKeys.SendWait("{ENTER}");

                    Esperar(3000);
                    SendKeys.SendWait("ZHRP1234");
                    Esperar(3000);
                    SendKeys.SendWait("{ENTER}");

                    Esperar(1000);
                    SendKeys.SendWait("+{F5}");
                    Esperar(1000);
                    SendKeys.SendWait("{F8}");
                    Esperar(1000);
                    SendKeys.SendWait("{F8}");

                    Esperar(180000);
                    SendKeys.SendWait("^+{F9}");
                    Esperar(5000);
                    SendKeys.SendWait("{DOWN}");
                    Esperar(1000);
                    SendKeys.SendWait("{TAB}");
                    Esperar(1000);
                    SendKeys.SendWait("{ENTER}");

                    Esperar(10000);
                    SendKeys.SendWait("+{TAB}");//CHISULBIO1
                    Esperar(1000);
                    SendKeys.SendWait(@"\\10.183.104.4\Biosalc\documentacion\Sistemas_Integrado_Caña\Maestro SAP\");//CHISULBIO1
                    Esperar(1000);
                    SendKeys.SendWait("{TAB}");
                    Esperar(1000);
                    SendKeys.SendWait("Maestro.txt");
                    Esperar(1000);
                    SendKeys.SendWait("{TAB}");
                    Esperar(1000);
                    SendKeys.SendWait("{TAB}");
                    Esperar(1000);
                    SendKeys.SendWait("{ENTER}");

                    Esperar(5000);
                    SendKeys.SendWait("{TAB}");
                    Esperar(1000);
                    SendKeys.SendWait("{TAB}");
                    Esperar(1000);
                    SendKeys.SendWait("{TAB}");
                    Esperar(1000);
                    SendKeys.SendWait("{ENTER}");

                    Esperar(1000);
                    SendKeys.SendWait("{ESC}");
                    Esperar(1000);
                    SendKeys.SendWait("{ESC}");
                    Esperar(1000);
                    SendKeys.SendWait("(%{F4})");
                    Esperar(1000);
                    SendKeys.SendWait("{TAB}");
                    Esperar(1000);
                    SendKeys.SendWait("{ENTER}");

                    Esperar(1000);
                    SendKeys.SendWait("(%{F4})");
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
    }
}