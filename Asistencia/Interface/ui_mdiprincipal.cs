using System;
using System.Linq;
using System.Windows.Forms;
using System.Configuration;
using System.IO;
using System.Drawing;
using System.Globalization;
using Excel = Microsoft.Office.Interop.Excel;
using System.Data.SqlClient;
//using SAP.Middleware.Connector;
using System.Threading;
using System.Diagnostics;
using OfficeOpenXml;
using System.Data;
using System.Collections.Generic;
using System.Text;
using CaniaBrava.Interface;
//using Timer = System.Windows.Forms.Timer;

namespace CaniaBrava
{
    public partial class ui_mdiprincipal : Form
    {
        //private int idleTime = 0;
        //private const int MAX_IDLE_TIME = 120000; // 15 minutos en milisegundos

        public ui_mdiprincipal()
        {
            InitializeComponent();

            // Configurar el temporizador
            //var timer = new Timer();
            //timer.Interval = 1000; // el temporizador se ejecutará cada segundo
            //timer.Tick += new EventHandler(Timer_Tick);
            //timer.Start();
        }

        //private void Timer_Tick(object sender, EventArgs e)
        //{
        //    // Incrementar el tiempo de inactividad
        //    idleTime += 1000;

        //    // Verificar si el tiempo de inactividad ha superado el límite máximo
        //    if (idleTime >= MAX_IDLE_TIME)
        //    {
        //        // Si ha pasado el tiempo límite, cerrar la aplicación
        //        Application.Exit();
        //    }
        //}

        //private void ResetIdleTime()
        //{
        //    // Restablecer el tiempo de inactividad a cero
        //    idleTime = 0;
        //}

        //private void ui_mdiprincipal_MouseClick(object sender, MouseEventArgs e)
        //{
        //    // El usuario hizo clic en la ventana, restablecer el tiempo de inactividad
        //    ResetIdleTime();
        //}

        //private void ui_mdiprincipal_KeyPress(object sender, KeyPressEventArgs e)
        //{
        //    // El usuario presionó una tecla, restablecer el tiempo de inactividad
        //    ResetIdleTime();
        //}




        private void Esperar(int tiempo)
        {
            Thread.Sleep(tiempo);
        }

        private void UTIL09_Click(object sender, EventArgs e)
        {
            ////System.Diagnostics.Process process = new System.Diagnostics.Process();
            ////System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
            ////startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
            ////startInfo.FileName = "cmd.exe";
            ////startInfo.Arguments = @"C:\Program Files (x86)\SAP\FrontEnd\SAPgui\saplogon.exe";
            ////process.StartInfo = startInfo;
            ////process.Start();

            //// Start the child process.
            //Process p = new Process();
            //// Redirect the output stream of the child process.
            //p.StartInfo.UseShellExecute = false;
            //p.StartInfo.RedirectStandardOutput = true;
            //p.StartInfo.FileName = @"C:\Program Files (x86)\SAP\FrontEnd\SAPgui\saplogon.exe";
            //p.Start();

            ////Call Shell("C:\Program Files (x86)\SAP\FrontEnd\SAPgui\saplogon.exe", vbMaximizedFocus) //PARA EL CASO DE VB NET

            //SendKeys.SendWait("{HOME} +D");
            //Esperar(6000);

            //SendKeys.SendWait("{ENTER}");
            //Esperar(3000);
            //SendKeys.SendWait("ASSESSMENT");
            //Esperar(3000);
            //SendKeys.SendWait("{TAB}");
            //Esperar(1000);
            //SendKeys.SendWait("Sistemas$5");
            //Esperar(3000);
            //SendKeys.SendWait("{ENTER}");
            //Esperar(3000);
            //SendKeys.SendWait("ZHRP1234");
            //Esperar(3000);
            //SendKeys.SendWait("{ENTER}");
            //Esperar(1000);
            //SendKeys.SendWait("+{F5}");
            //Esperar(1000);
            //SendKeys.SendWait("{F8}");

            //// Do not wait for the child process to exit before
            //// reading to the end of its redirected stream.
            //// p.WaitForExit();
            //// Read the output stream first and then wait.
            //string output = p.StandardOutput.ReadToEnd();
            //p.WaitForExit();

            Sincronizacion();
            //ECCDestinationConfig cfg = null;
            //RfcDestination dest = null;

            //try
            //{
            //    cfg = new ECCDestinationConfig();
            //    RfcDestinationManager.RegisterDestinationConfiguration(cfg);
            //    dest = RfcDestinationManager.GetDestination("A) SAP ECC 6.0 - PRODUCTIVO");

            //    RfcRepository repo = dest.Repository;

            //    IRfcFunction fnpush = repo.CreateFunction("RFC_READ_TABLE");

            //    // Send Data With RFC Structure  
            //    IRfcStructure data = fnpush.GetStructure("IM_STRUCTURE");
            //    data.SetValue("ITEM1", "VALUE1");
            //    data.SetValue("ITEM2", "VALUE2");
            //    data.SetValue("ITEM3", "VALUE3");
            //    data.SetValue("ITEM4", "VALUE4");

            //    fnpush.SetValue("IM_STRUCTURE", data);

            //    // Send Data With RFC Table  
            //    IRfcTable dataTbl = fnpush.GetTable("IM_TABLE");

            //    //foreach (var item in ListItem)
            //    //{
            //    //    dataTbl.Append();
            //    //    dataTbl.SetValue("ITEM1", item.VALUE1);
            //    //    dataTbl.SetValue("ITEM2", item.VALUE2);
            //    //    dataTbl.SetValue("ITEM3", item.VALUE3);
            //    //    dataTbl.SetValue("ITEM4", item.VALUE4);
            //    //}

            //    fnpush.Invoke(dest);

            //    var exObject = fnpush.GetObject("EX_OBJECT");
            //    IRfcStructure exStructure = fnpush.GetStructure("EX_STRUCTURE");


            //    RfcSessionManager.EndContext(dest);
            //    RfcDestinationManager.UnregisterDestinationConfiguration(cfg);
            //}
            //catch (Exception ex)
            //{
            //    RfcSessionManager.EndContext(dest);
            //    RfcDestinationManager.UnregisterDestinationConfiguration(cfg);
            //    Thread.Sleep(1000);
            //}
        }

        private void salirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.ExitThread();
        }

        internal void usuarioActivo(string usuarioActivo)
        {
            this.Text = usuarioActivo;
        }

        private void ui_mdiprincipal_Load(object sender, EventArgs e)
        {
            GlobalVariables gv = new GlobalVariables();
            string tipoUsuario = gv.getValorTypeUsr();

            if (ActiveForm != null)
            {
                lblCargando.Location = new Point((ActiveForm.Width / 2) - (lblCargando.Width / 2), (ActiveForm.Height / 2) - (lblCargando.Height / 2));
                loadingNext1.Location = new Point((ActiveForm.Width / 2) - (loadingNext1.Width / 2), (ActiveForm.Height / 2) - (loadingNext1.Height / 2));
                //menuMDI.ForeColor = Color.MediumBlue;
            }

            if (!tipoUsuario.Equals("00"))
            {
                OptMenu optmenu = new OptMenu();
                int indOptMenu = 0;

                ///////////////////////////////////////////////////////////////////////////////////////////////
                ///////////////////////////TABLAS MAESTRAS/////////////////////////////////////////////////////
                ///////////////////////////////////////////////////////////////////////////////////////////////

                indOptMenu = optmenu.getOptMenu(gv.getValorUsr(), "TAMA");
                if (indOptMenu < 1)
                {
                    TAMA.Visible = false;
                }
                else
                {
                    TAMA.Visible = true;
                    indOptMenu = optmenu.getOptMenu(gv.getValorUsr(), "TAMA01");
                    if (indOptMenu < 1)
                    {
                        TAMA01.Enabled = false;
                    }
                    else
                    {
                        TAMA01.Enabled = true;
                    }

                    indOptMenu = optmenu.getOptMenu(gv.getValorUsr(), "TAMA02");
                    if (indOptMenu < 1)
                    {
                        TAMA02.Enabled = false;
                    }
                    else
                    {
                        TAMA02.Enabled = true;
                    }

                    indOptMenu = optmenu.getOptMenu(gv.getValorUsr(), "TAMA05");
                    if (indOptMenu < 1)
                    {
                        TAMA05.Enabled = false;
                    }
                    else
                    {
                        TAMA05.Enabled = true;
                    }

                    indOptMenu = optmenu.getOptMenu(gv.getValorUsr(), "TAMA06");
                    if (indOptMenu < 1)
                    {
                        TAMA06.Enabled = false;
                    }
                    else
                    {
                        TAMA06.Enabled = true;
                    }

                    indOptMenu = optmenu.getOptMenu(gv.getValorUsr(), "TAMA07");
                    if (indOptMenu < 1)
                    {
                        TAMA07.Enabled = false;
                    }
                    else
                    {
                        TAMA07.Enabled = true;

                        indOptMenu = optmenu.getOptMenu(gv.getValorUsr(), "TAMA0701");
                        if (indOptMenu < 1)
                        {
                            TAMA0701.Enabled = false;
                        }
                        else
                        {
                            TAMA0701.Enabled = true;
                        }

                        indOptMenu = optmenu.getOptMenu(gv.getValorUsr(), "TAMA0702");
                        if (indOptMenu < 1)
                        {
                            TAMA0702.Enabled = false;
                        }
                        else
                        {
                            TAMA0702.Enabled = true;
                        }

                    }
                }

                #region Maestros de Planilla
                ///////////////////////////////////////////////////////////////////////////////////////////////
                ///////////////////////////Maestros de Planilla////////////////////////////////////////////////
                ///////////////////////////////////////////////////////////////////////////////////////////////
                /*MAPL	Maestros del Sistema
                MAPL01	Maestro del Personal
                MAPL0101	Información del Personal
                MAPL0102	Descuentos Judiciales
                MAPL0103	Reporte de Personal
                MAPL02	Conceptos de Planilla
                MAPL03	Conceptos de Planilla Fijos
                MAPL04	Jornal y Remuneración Básica
                MAPL05	Registro Vacacional
                MAPL0501	Registro de Vacaciones
                MAPL0502	Historial de Vacaciones
                MAPL06	Préstamos al Personal
                MAPL07	Acumulados Quinta Categoría
                MAPL08	Bonificaciones Extraordinarias*/
                #endregion

                indOptMenu = optmenu.getOptMenu(gv.getValorUsr(), "MAPL");
                if (indOptMenu < 1)
                {
                    MAPL.Visible = false;
                    mTrabajadores.Enabled = false;
                    mTipoHorarios.Enabled = false;
                    btnReporteAsis.Enabled = false;
                    btnProgramacion.Enabled = false;
                }
                else
                {
                    MAPL.Visible = true;
                    indOptMenu = optmenu.getOptMenu(gv.getValorUsr(), "MAPL01");
                    if (indOptMenu < 1)
                    {
                        MAPL01.Enabled = false;
                        mTrabajadores.Enabled = false;
                    }
                    else
                    {
                        MAPL01.Enabled = true;
                        indOptMenu = optmenu.getOptMenu(gv.getValorUsr(), "MAPL0101");
                        if (indOptMenu < 1)
                        {
                            MAPL0101.Enabled = false;
                            mTrabajadores.Enabled = false;
                        }
                        else
                        {
                            MAPL0101.Enabled = true;
                            mTrabajadores.Enabled = true;
                        }

                        indOptMenu = optmenu.getOptMenu(gv.getValorUsr(), "MAPL0102");
                        if (indOptMenu < 1)
                        {
                            MAPL0102.Enabled = false;
                        }
                        else
                        {
                            MAPL0102.Enabled = true;
                        }

                        indOptMenu = optmenu.getOptMenu(gv.getValorUsr(), "MAPL0103");
                        if (indOptMenu < 1)
                        {
                            MAPL0103.Enabled = false;
                        }
                        else
                        {
                            MAPL0103.Enabled = true;
                        }

                    }

                    indOptMenu = optmenu.getOptMenu(gv.getValorUsr(), "MAPL05");
                    if (indOptMenu < 1)
                    {
                        MAPL05.Enabled = false;
                    }
                    else
                    {
                        MAPL05.Enabled = true;
                        indOptMenu = optmenu.getOptMenu(gv.getValorUsr(), "MAPL0501");
                        if (indOptMenu < 1)
                        {
                            MAPL0501.Enabled = false;
                        }
                        else
                        {
                            MAPL0501.Enabled = true;
                        }

                        indOptMenu = optmenu.getOptMenu(gv.getValorUsr(), "MAPL0502");
                        if (indOptMenu < 1)
                        {
                            MAPL0502.Enabled = false;
                        }
                        else
                        {
                            MAPL0502.Enabled = true;
                        }
                    }

                    indOptMenu = optmenu.getOptMenu(gv.getValorUsr(), "MAPL09");
                    if (indOptMenu < 1)
                    {
                        MAPL09.Enabled = false;
                        mTipoHorarios.Enabled = false;
                        btnReporteAsis.Enabled = false;
                    }
                    else
                    {
                        MAPL09.Enabled = true;
                        indOptMenu = optmenu.getOptMenu(gv.getValorUsr(), "MAPL0901");
                        if (indOptMenu < 1)
                        {
                            MAPL0901.Enabled = false;
                        }
                        else
                        {
                            MAPL0901.Enabled = true;
                        }

                        indOptMenu = optmenu.getOptMenu(gv.getValorUsr(), "MAPL0902");
                        if (indOptMenu < 1)
                        {
                            MAPL0902.Enabled = false;
                        }
                        else
                        {
                            MAPL0902.Enabled = true;
                        }

                        indOptMenu = optmenu.getOptMenu(gv.getValorUsr(), "MAPL0903");
                        if (indOptMenu < 1)
                        {
                            MAPL0903.Enabled = false;
                            mTipoHorarios.Enabled = false;
                        }
                        else
                        {
                            MAPL0903.Enabled = true;
                            mTipoHorarios.Enabled = true;
                        }

                        indOptMenu = optmenu.getOptMenu(gv.getValorUsr(), "MAPL0904");
                        if (indOptMenu < 1)
                        {
                            MAPL0904.Enabled = false;
                            btnReporteAsis.Enabled = false;
                        }
                        else
                        {
                            MAPL0904.Enabled = true;
                            btnReporteAsis.Enabled = true;
                        }

                        indOptMenu = optmenu.getOptMenu(gv.getValorUsr(), "MAPL0905");
                        if (indOptMenu < 1)
                        {
                            MAPL0905.Enabled = false;
                        }
                        else
                        {
                            MAPL0905.Enabled = true;
                        }

                        indOptMenu = optmenu.getOptMenu(gv.getValorUsr(), "MAPL0906");
                        if (indOptMenu < 1)
                        {
                            MAPL0906.Enabled = false;
                        }
                        else
                        {
                            MAPL0906.Enabled = true;
                        }
                    }

                    indOptMenu = optmenu.getOptMenu(gv.getValorUsr(), "MAPL10");
                    if (indOptMenu < 1)
                    {
                        MAPL10.Visible = false;
                        btnProgramacion.Enabled = false;
                    }
                    else
                    {
                        MAPL10.Visible = true;
                        indOptMenu = optmenu.getOptMenu(gv.getValorUsr(), "MAPL1001");
                        if (indOptMenu < 1)
                        {
                            MAPL1001.Enabled = false;
                            btnProgramacion.Enabled = false;
                        }
                        else
                        {
                            MAPL1001.Enabled = true;
                            btnProgramacion.Enabled = true;
                        }

                        indOptMenu = optmenu.getOptMenu(gv.getValorUsr(), "MAPL1002");
                        if (indOptMenu < 1)
                        {
                            MAPL1002.Enabled = false;
                        }
                        else
                        {
                            MAPL1002.Enabled = true;
                        }

                        indOptMenu = optmenu.getOptMenu(gv.getValorUsr(), "MAPL1003");
                        if (indOptMenu < 1)
                        {
                            MAPL1003.Enabled = false;
                        }
                        else
                        {
                            MAPL1003.Enabled = true;
                        }

                        indOptMenu = optmenu.getOptMenu(gv.getValorUsr(), "MAPL1004");
                        if (indOptMenu < 1)
                        {
                            MAPL1004.Enabled = false;
                        }
                        else
                        {
                            MAPL1004.Enabled = true;
                        }
                    }
                }

                #region G.O.
                indOptMenu = optmenu.getOptMenu(gv.getValorUsr(), "GO");
                if (indOptMenu < 1)
                {
                    GO.Visible = false;
                }
                else
                {
                    GO.Visible = true;
                    indOptMenu = optmenu.getOptMenu(gv.getValorUsr(), "GO01");
                    if (indOptMenu < 1)
                    {
                        GO01.Enabled = false;
                    }
                    else
                    {
                        GO01.Enabled = true;
                        indOptMenu = optmenu.getOptMenu(gv.getValorUsr(), "GO0101");
                        if (indOptMenu < 1)
                        {
                            GO0101.Enabled = false;
                        }
                        else
                        {
                            GO0101.Enabled = true;
                        }
                    }

                    indOptMenu = optmenu.getOptMenu(gv.getValorUsr(), "GO02");
                    if (indOptMenu < 1)
                    {
                        GO02.Enabled = false;
                    }
                    else
                    {
                        GO02.Enabled = true;
                        indOptMenu = optmenu.getOptMenu(gv.getValorUsr(), "GO0201");
                        if (indOptMenu < 1)
                        {
                            GO0201.Enabled = false;
                        }
                        else
                        {
                            GO0201.Enabled = true;
                        }
                    }

                    indOptMenu = optmenu.getOptMenu(gv.getValorUsr(), "GO03");
                    if (indOptMenu < 1)
                    {
                        GO03.Enabled = false;
                    }
                    else
                    {
                        GO03.Enabled = true;
                        indOptMenu = optmenu.getOptMenu(gv.getValorUsr(), "GO0301");
                        if (indOptMenu < 1)
                        {
                            GO0301.Enabled = false;
                        }
                        else
                        {
                            GO0301.Enabled = true;
                        }
                    }
                }
                #endregion

                #region G.I.
                indOptMenu = optmenu.getOptMenu(gv.getValorUsr(), "GI");
                if (indOptMenu < 1)
                {
                    GI.Visible = false;
                }
                else
                {
                    GI.Visible = true;
                    indOptMenu = optmenu.getOptMenu(gv.getValorUsr(), "GO01");
                    if (indOptMenu < 1)
                    {
                        GO01.Enabled = false;
                    }
                    else
                    {
                        GO01.Enabled = true;
                        indOptMenu = optmenu.getOptMenu(gv.getValorUsr(), "GO0101");
                        if (indOptMenu < 1)
                        {
                            GO0101.Enabled = false;
                        }
                        else
                        {
                            GO0101.Enabled = true;
                        }
                    }
                }
                #endregion

                #region G.F.A. y C.I.
                indOptMenu = optmenu.getOptMenu(gv.getValorUsr(), "GFACI");
                if (indOptMenu < 1)
                {
                    GFACI.Visible = false;
                }
                else
                {
                    GFACI.Visible = true;
                    indOptMenu = optmenu.getOptMenu(gv.getValorUsr(), "GFACI01");
                    if (indOptMenu < 1)
                    {
                        GFACI01.Enabled = false;
                    }
                    else
                    {
                        GFACI01.Enabled = true;
                        indOptMenu = optmenu.getOptMenu(gv.getValorUsr(), "GFACI0101");
                        if (indOptMenu < 1)
                        {
                            GFACI0101.Enabled = false;
                        }
                        else
                        {
                            GFACI0101.Enabled = true;
                        }

                        indOptMenu = optmenu.getOptMenu(gv.getValorUsr(), "GFACI0102");
                        if (indOptMenu < 1)
                        {
                            GFACI0102.Enabled = false;
                        }
                        else
                        {
                            GFACI0102.Enabled = true;
                        }
                    }
                }
                #endregion

                #region G.G.H. y S.
                indOptMenu = optmenu.getOptMenu(gv.getValorUsr(), "GGHS");
                if (indOptMenu < 1)
                {
                    GGHS.Visible = false;
                }
                else
                {
                    GGHS.Visible = true;
                    indOptMenu = optmenu.getOptMenu(gv.getValorUsr(), "GGHS01");
                    if (indOptMenu < 1)
                    {
                        GGHS01.Enabled = false;
                    }
                    else
                    {
                        GGHS01.Enabled = true;
                    }

                    indOptMenu = optmenu.getOptMenu(gv.getValorUsr(), "GGHS02");
                    if (indOptMenu < 1)
                    {
                        GGHS02.Enabled = false;
                    }
                    else
                    {
                        GGHS02.Enabled = true;
                        indOptMenu = optmenu.getOptMenu(gv.getValorUsr(), "GGHS0201");
                        if (indOptMenu < 1)
                        {
                            GGHS0201.Enabled = false;
                        }
                        else
                        {
                            GGHS0201.Enabled = true;
                            indOptMenu = optmenu.getOptMenu(gv.getValorUsr(), "GGHS020101");
                            if (indOptMenu < 1)
                            {
                                GGHS020101.Enabled = false;
                            }
                            else
                            {
                                GGHS020101.Enabled = true;
                            }

                            indOptMenu = optmenu.getOptMenu(gv.getValorUsr(), "GGHS020102");
                            if (indOptMenu < 1)
                            {
                                GGHS020102.Enabled = false;
                            }
                            else
                            {
                                GGHS020102.Enabled = true;
                            }
                        }
                    }

                    indOptMenu = optmenu.getOptMenu(gv.getValorUsr(), "GGHS03");
                    if (indOptMenu < 1)
                    {
                        GGHS03.Enabled = false;
                    }
                    else
                    {
                        GGHS03.Enabled = true;
                        indOptMenu = optmenu.getOptMenu(gv.getValorUsr(), "GGHS0301");
                        if (indOptMenu < 1)
                        {
                            GGHS0301.Enabled = false;
                        }
                        else
                        {
                            GGHS0301.Enabled = true;
                        }
                    }

                    indOptMenu = optmenu.getOptMenu(gv.getValorUsr(), "GGHS04");
                    if (indOptMenu < 1)
                    {
                        GGHS04.Enabled = false;
                    }
                    else
                    {
                        GGHS04.Enabled = true;
                        indOptMenu = optmenu.getOptMenu(gv.getValorUsr(), "GGHS0401");
                        if (indOptMenu < 1)
                        {
                            GGHS0401.Enabled = false;
                        }
                        else
                        {
                            GGHS0401.Enabled = true;
                        }

                        indOptMenu = optmenu.getOptMenu(gv.getValorUsr(), "GGHS0402");
                        if (indOptMenu < 1)
                        {
                            GGHS0402.Enabled = false;
                        }
                        else
                        {
                            GGHS0402.Enabled = true;
                        }
                    }

                    indOptMenu = optmenu.getOptMenu(gv.getValorUsr(), "GGHS05");
                    if (indOptMenu < 1)
                    {
                        GGHS05.Enabled = false;
                    }
                    else
                    {
                        GGHS05.Enabled = true;
                    }

                    indOptMenu = optmenu.getOptMenu(gv.getValorUsr(), "GGHS06");
                    if (indOptMenu < 1)
                    {
                        GGHS06.Enabled = false;
                    }
                    else
                    {
                        GGHS06.Enabled = true;
                    }
                }
                #endregion

                #region COMEDORES
                /*
                   COMED	Comedores
                    COMED01	Registro de Proveedores
                    COMED02	Registro de Insumos
                    COMED03	Ingresos y Salidas (Insumos)
                    COMED04	Registar Pedidos de Insumos
                    COMED05	Registro de Invitados
                    COMED06	Reportes
                    COMED0601	Reportes de Insumos
                    COMED0602	Reportes de Comensales
                 */
                #endregion

                indOptMenu = optmenu.getOptMenu(gv.getValorUsr(), "COMED");
                if (indOptMenu < 1)
                {
                    COMED.Visible = false;
                }
                else
                {
                    COMED.Visible = true;
                    indOptMenu = optmenu.getOptMenu(gv.getValorUsr(), "COMED01");
                    if (indOptMenu < 1)
                    {
                        COMED01.Enabled = false;
                    }
                    else
                    {
                        COMED01.Enabled = true;
                    }

                    indOptMenu = optmenu.getOptMenu(gv.getValorUsr(), "COMED02");
                    if (indOptMenu < 1)
                    {
                        COMED02.Enabled = false;
                    }
                    else
                    {
                        COMED02.Enabled = true;
                    }

                    indOptMenu = optmenu.getOptMenu(gv.getValorUsr(), "COMED03");
                    if (indOptMenu < 1)
                    {
                        COMED04.Enabled = false;
                    }
                    else
                    {
                        COMED04.Enabled = true;
                    }

                    indOptMenu = optmenu.getOptMenu(gv.getValorUsr(), "COMED04");
                    if (indOptMenu < 1)
                    {
                        COMED03.Enabled = false;
                    }
                    else
                    {
                        COMED03.Enabled = true;
                    }

                    indOptMenu = optmenu.getOptMenu(gv.getValorUsr(), "COMED05");
                    if (indOptMenu < 1)
                    {
                        COMED05.Enabled = false;
                    }
                    else
                    {
                        COMED05.Enabled = true;
                    }

                    indOptMenu = optmenu.getOptMenu(gv.getValorUsr(), "COMED06");
                    if (indOptMenu < 1)
                    {
                        COMED06.Enabled = false;
                    }
                    else
                    {
                        COMED06.Enabled = true;
                        indOptMenu = optmenu.getOptMenu(gv.getValorUsr(), "COMED0601");
                        if (indOptMenu < 1)
                        {
                            COMED0601.Enabled = false;
                        }
                        else
                        {
                            COMED0601.Enabled = true;
                        }

                        indOptMenu = optmenu.getOptMenu(gv.getValorUsr(), "COMED0602");
                        if (indOptMenu < 1)
                        {
                            COMED0602.Enabled = false;
                        }
                        else
                        {
                            COMED0602.Enabled = true;
                        }
                    }
                }

                #region Utilitarios
                ///////////////////////////////////////////////////////////////////////////////////////////////
                ///////////////////////////Utilitarios/////////////////////////////////////////////////////////
                ///////////////////////////////////////////////////////////////////////////////////////////////
                /*
                   UTIL	Utilitarios
                    UTIL01	Empleadores
                    UTIL02	Usuarios del Sistema
                    UTIL03	Copia de Seguridad
                    UTIL04	Conceptos Calculados
                    UTIL05	Optimizar Tablas
                    UTIL06	Parámetros de Impresión
                    UTIL07	Ejecutar Script SQL
                    UTIL08	Cambiar Contraseña
                 */
                #endregion

                indOptMenu = optmenu.getOptMenu(gv.getValorUsr(), "UTIL");
                if (indOptMenu < 1)
                {
                    UTIL.Enabled = false;
                }
                else
                {
                    UTIL.Enabled = true;
                    indOptMenu = optmenu.getOptMenu(gv.getValorUsr(), "UTIL01");
                    if (indOptMenu < 1)
                    {
                        UTIL01.Enabled = false;
                    }
                    else
                    {
                        UTIL01.Enabled = true;
                    }

                    indOptMenu = optmenu.getOptMenu(gv.getValorUsr(), "UTIL02");
                    if (indOptMenu < 1)
                    {
                        UTIL02.Enabled = false;
                    }
                    else
                    {
                        UTIL02.Enabled = true;
                    }

                    indOptMenu = optmenu.getOptMenu(gv.getValorUsr(), "UTIL03");
                    if (indOptMenu < 1)
                    {
                        UTIL03.Enabled = false;
                    }
                    else
                    {
                        UTIL03.Enabled = true;
                    }

                    indOptMenu = optmenu.getOptMenu(gv.getValorUsr(), "UTIL06");
                    if (indOptMenu < 1)
                    {
                        UTIL06.Enabled = false;
                    }
                    else
                    {
                        UTIL06.Enabled = true;
                    }

                    indOptMenu = optmenu.getOptMenu(gv.getValorUsr(), "UTIL08");
                    if (indOptMenu < 1)
                    {
                        UTIL08.Enabled = false;
                    }
                    else
                    {
                        UTIL08.Enabled = true;
                    }

                    indOptMenu = optmenu.getOptMenu(gv.getValorUsr(), "UTIL09");
                    if (indOptMenu < 1)
                    {
                        UTIL09.Enabled = false;
                        btnSincronizar.Enabled = false;
                    }
                    else
                    {
                        UTIL09.Enabled = true;
                        btnSincronizar.Enabled = true;
                    }

                    indOptMenu = optmenu.getOptMenu(gv.getValorUsr(), "UTIL10");
                    if (indOptMenu < 1)
                    {
                        UTIL10.Enabled = false;
                    }
                    else
                    {
                        UTIL10.Enabled = true;
                    }
                }
            }
        }

        private void actualizaciónDeUsuariosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ui_usuarios ui_usuarios = new ui_usuarios();
            ui_usuarios.MdiParent = this;
            if (ui_usuarios.WindowState == FormWindowState.Minimized)
                ui_usuarios.WindowState = FormWindowState.Normal;
            ui_usuarios.Activate();
            ui_usuarios.Show();
            ui_usuarios.BringToFront();

        }

        private void mantenimientoDeCompañíasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.Controls.Find("ui_companias", true).Count() == 0)
            {
                ui_companias ui_companias = new ui_companias();
                ui_companias.MdiParent = this;
                if (ui_companias.WindowState == FormWindowState.Minimized)
                    ui_companias.WindowState = FormWindowState.Normal;
                ui_companias.Activate();
                ui_companias.Show();
                ui_companias.BringToFront();
            }
        }

        private void herramientasToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void documentosToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void calendarioDePlanillaToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void acercaDeToolStripMenuItem1_Click(object sender, EventArgs e)
        {

        }

        private void procesosToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void productosDeDestajoToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void zonasDeTrabajoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ui_zonatrabajo ui_zonatrabajo = new ui_zonatrabajo();
            ui_zonatrabajo.MdiParent = this;
            if (ui_zonatrabajo.WindowState == FormWindowState.Minimized)
                ui_zonatrabajo.WindowState = FormWindowState.Normal;
            ui_zonatrabajo.Activate();
            ui_zonatrabajo.Show();
            ui_zonatrabajo.BringToFront();
        }

        private void reportesToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void informaciónDePlanillaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ui_ingdatosplanilla ui_ingdatosplanilla = new ui_ingdatosplanilla();
            ui_ingdatosplanilla.MdiParent = this;
            if (ui_ingdatosplanilla.WindowState == FormWindowState.Minimized)
                ui_ingdatosplanilla.WindowState = FormWindowState.Normal;
            ui_ingdatosplanilla.Activate();
            ui_ingdatosplanilla.Show();
            ui_ingdatosplanilla.BringToFront();
        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            ui_motivos ui_motivos = new ui_motivos();
            ui_motivos.MdiParent = this;
            if (ui_motivos.WindowState == FormWindowState.Minimized)
                ui_motivos.WindowState = FormWindowState.Normal;
            ui_motivos.Activate();
            ui_motivos.Show();
            ui_motivos.BringToFront();
        }

        private void toolStripMenuItem4_Click(object sender, EventArgs e)
        {
            ui_remuneraciones ui_remuneraciones = new ui_remuneraciones();
            ui_remuneraciones.MdiParent = this;
            if (ui_remuneraciones.WindowState == FormWindowState.Minimized)
                ui_remuneraciones.WindowState = FormWindowState.Normal;
            ui_remuneraciones.Activate();
            ui_remuneraciones.Show();
            ui_remuneraciones.BringToFront();
        }

        private void toolStripMenuItem5_Click(object sender, EventArgs e)
        {
            ui_conceptosfijos ui_conceptosfijos = new ui_conceptosfijos();
            ui_conceptosfijos.MdiParent = this;
            if (ui_conceptosfijos.WindowState == FormWindowState.Minimized)
                ui_conceptosfijos.WindowState = FormWindowState.Normal;
            ui_conceptosfijos.Activate();
            ui_conceptosfijos.Show();
            ui_conceptosfijos.BringToFront();
        }

        private void informaciónDeDestajoToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void actualizaciónDelPersonalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //if (this.Controls.Find("ui_perplan", true).Count() == 0)
            //{
            //    ui_perplan ui_perplan = new ui_perplan();
            //    ui_perplan.MdiParent = this;
            //    if (ui_perplan.WindowState == FormWindowState.Minimized)
            //        ui_perplan.WindowState = FormWindowState.Normal;
            //    ui_perplan.Activate();
            //    ui_perplan.Show();
            //    ui_perplan.BringToFront();
            //}
            if (this.Controls.Find("ui_trabajadores", true).Count() == 0)
            {
                ui_trabajadores ui_perplan = new ui_trabajadores();
                ui_perplan.MdiParent = this;
                if (ui_perplan.WindowState == FormWindowState.Minimized)
                    ui_perplan.WindowState = FormWindowState.Normal;
                ui_perplan.Activate();
                ui_perplan.Show();
                ui_perplan.BringToFront();
            }
        }

        private void descuentosJudicialesToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            ui_DesJud ui_desjud = new ui_DesJud();
            ui_desjud.MdiParent = this;
            if (ui_desjud.WindowState == FormWindowState.Minimized)
                ui_desjud.WindowState = FormWindowState.Normal;
            ui_desjud.Activate();
            ui_desjud.Show();
            ui_desjud.BringToFront();
        }

        private void préstamosAlPersonalToolStripMenuItem1_Click(object sender, EventArgs e)
        {

        }

        private void cálculoDePlanillaToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void consultaDeBoletasToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void personalDeRetencionesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ui_PerRet ui_PerRet = new ui_PerRet();
            ui_PerRet.MdiParent = this;
            if (ui_PerRet.WindowState == FormWindowState.Minimized)
                ui_PerRet.WindowState = FormWindowState.Normal;
            ui_PerRet.Activate();
            ui_PerRet.Show();
            ui_PerRet.BringToFront();
        }

        private void libroDeRetencionesDeDestajoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ui_planillaretenciones ui_planillaretenciones = new ui_planillaretenciones();
            ui_planillaretenciones.MdiParent = this;
            if (ui_planillaretenciones.WindowState == FormWindowState.Minimized)
                ui_planillaretenciones.WindowState = FormWindowState.Normal;
            ui_planillaretenciones.Activate();
            ui_planillaretenciones.Show();
            ui_planillaretenciones.BringToFront();
        }

        private void emisiónDeParteDiarioToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ui_partediariodestajo ui_partediariodestajo = new ui_partediariodestajo();
            ui_partediariodestajo.MdiParent = this;
            if (ui_partediariodestajo.WindowState == FormWindowState.Minimized)
                ui_partediariodestajo.WindowState = FormWindowState.Normal;
            ui_partediariodestajo.Activate();
            ui_partediariodestajo.Show();
            ui_partediariodestajo.BringToFront();
        }

        private void resumenPorPeriodoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ui_resumendestajoperiodo ui_resumendestajoperiodo = new ui_resumendestajoperiodo();
            ui_resumendestajoperiodo.MdiParent = this;
            if (ui_resumendestajoperiodo.WindowState == FormWindowState.Minimized)
                ui_resumendestajoperiodo.WindowState = FormWindowState.Normal;
            ui_resumendestajoperiodo.Activate();
            ui_resumendestajoperiodo.Show();
            ui_resumendestajoperiodo.BringToFront();
        }

        private void boletaPorTrabajadorToolStripMenuItem_Click(object sender, EventArgs e)
        {


        }

        private void boletasPorEmpleadorToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void toolStripMenuItem9_Click(object sender, EventArgs e)
        {
            ui_ingdatosplanilla ui_dataplan = new ui_ingdatosplanilla();
            ui_dataplan.MdiParent = this;
            if (ui_dataplan.WindowState == FormWindowState.Minimized)
                ui_dataplan.WindowState = FormWindowState.Normal;
            ui_dataplan.Activate();
            ui_dataplan.Show();
            ui_dataplan.BringToFront();
        }

        private void administraciónDeUsuariosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.Controls.Find("ui_usuarios", true).Count() == 0)
            {
                ui_usuarios ui_usuarios = new ui_usuarios();
                ui_usuarios.MdiParent = this;
                if (ui_usuarios.WindowState == FormWindowState.Minimized)
                    ui_usuarios.WindowState = FormWindowState.Normal;
                ui_usuarios.Activate();
                ui_usuarios.Show();
                ui_usuarios.BringToFront();
            }
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            ui_maesgen ui_maesgen = new ui_maesgen();
            ui_maesgen.MdiParent = this;
            if (ui_maesgen.WindowState == FormWindowState.Minimized)
                ui_maesgen.WindowState = FormWindowState.Normal;
            ui_maesgen.Activate();
            ui_maesgen.Show();
            ui_maesgen.BringToFront();
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            ui_calplan ui_calplan = new ui_calplan();
            ui_calplan.MdiParent = this;
            if (ui_calplan.WindowState == FormWindowState.Minimized)
                ui_calplan.WindowState = FormWindowState.Normal;
            ui_calplan.Activate();
            ui_calplan.Show();
            ui_calplan.BringToFront();
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            ui_laborper ui_laborper = new ui_laborper();
            ui_laborper.MdiParent = this;
            if (ui_laborper.WindowState == FormWindowState.Minimized)
                ui_laborper.WindowState = FormWindowState.Normal;
            ui_laborper.Activate();
            ui_laborper.Show();
            ui_laborper.BringToFront();
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            ui_sisparm ui_sisparm = new ui_sisparm();
            ui_sisparm.MdiParent = this;
            if (ui_sisparm.WindowState == FormWindowState.Minimized)
                ui_sisparm.WindowState = FormWindowState.Normal;
            ui_sisparm.Activate();
            ui_sisparm.Show();
            ui_sisparm.BringToFront();
        }

        private void toolStripSeparator16_Click(object sender, EventArgs e)
        {

        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            ui_perplan ui_perplan = new ui_perplan();
            ui_perplan.MdiParent = this;
            if (ui_perplan.WindowState == FormWindowState.Minimized)
                ui_perplan.WindowState = FormWindowState.Normal;
            ui_perplan.Activate();
            ui_perplan.Show();
            ui_perplan.BringToFront();
        }

        private void reportesToolStripMenuItem1_Click(object sender, EventArgs e)
        {

        }

        private void toolStripButton6_Click(object sender, EventArgs e)
        {
            ui_destajo ui_destajo = new ui_destajo();
            ui_destajo.MdiParent = this;
            if (ui_destajo.WindowState == FormWindowState.Minimized)
                ui_destajo.WindowState = FormWindowState.Normal;
            ui_destajo.Activate();
            ui_destajo.Show();
            ui_destajo.BringToFront();
        }

        private void toolStripButton7_Click(object sender, EventArgs e)
        {
            ui_planillaretenciones ui_planillaretenciones = new ui_planillaretenciones();
            ui_planillaretenciones.MdiParent = this;
            if (ui_planillaretenciones.WindowState == FormWindowState.Minimized)
                ui_planillaretenciones.WindowState = FormWindowState.Normal;
            ui_planillaretenciones.Activate();
            ui_planillaretenciones.Show();
            ui_planillaretenciones.BringToFront();
        }

        private void toolStripButton8_Click(object sender, EventArgs e)
        {
            ui_motivos ui_motivos = new ui_motivos();
            ui_motivos.MdiParent = this;
            if (ui_motivos.WindowState == FormWindowState.Minimized)
                ui_motivos.WindowState = FormWindowState.Normal;
            ui_motivos.Activate();
            ui_motivos.Show();
            ui_motivos.BringToFront();
        }

        private void toolStripButton9_Click(object sender, EventArgs e)
        {
            ui_ingdatosplanilla ui_dataplan = new ui_ingdatosplanilla();
            ui_dataplan.MdiParent = this;
            if (ui_dataplan.WindowState == FormWindowState.Minimized)
                ui_dataplan.WindowState = FormWindowState.Normal;
            ui_dataplan.Activate();
            ui_dataplan.Show();
            ui_dataplan.BringToFront();
        }

        private void toolStripButton10_Click(object sender, EventArgs e)
        {
            ui_procesaplan ui_procesaplan = new ui_procesaplan();
            ui_procesaplan.MdiParent = this;
            if (ui_procesaplan.WindowState == FormWindowState.Minimized)
                ui_procesaplan.WindowState = FormWindowState.Normal;
            ui_procesaplan.Activate();
            ui_procesaplan.Show();
            ui_procesaplan.BringToFront();
        }

        private void toolStripButton11_Click(object sender, EventArgs e)
        {
            ui_BoletaxEmpleador ui_boleta = new ui_BoletaxEmpleador();
            ui_boleta.MdiParent = this;
            if (ui_boleta.WindowState == FormWindowState.Minimized)
                ui_boleta.WindowState = FormWindowState.Normal;
            ui_boleta.Activate();
            ui_boleta.Show();
            ui_boleta.BringToFront();
        }

        private void toolStripButton12_Click(object sender, EventArgs e)
        {
            Application.ExitThread();
        }

        private void toolStripMenuItem10_Click(object sender, EventArgs e)
        {
            ui_DestajoToPlan ui_destajotoplan = new ui_DestajoToPlan();
            ui_destajotoplan.MdiParent = this;
            if (ui_destajotoplan.WindowState == FormWindowState.Minimized)
                ui_destajotoplan.WindowState = FormWindowState.Normal;
            ui_destajotoplan.Activate();
            ui_destajotoplan.Show();
            ui_destajotoplan.BringToFront();
        }

        private void libroDePlanillaToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void registroDelProductoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.Controls.Find("ui_RegistraLicencia", true).Count() == 0)
            {
                ui_RegistraLicencia ui_licencia = new ui_RegistraLicencia();
                ui_licencia.MdiParent = this;
                if (ui_licencia.WindowState == FormWindowState.Minimized)
                    ui_licencia.WindowState = FormWindowState.Normal;
                ui_licencia.Activate();
                ui_licencia.Show();
                ui_licencia.BringToFront();
            }
        }

        private void acercaDeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.Controls.Find("ui_acerca", true).Count() == 0)
            {
                ui_acerca ui_acerca = new ui_acerca();
                ui_acerca.MdiParent = this;
                if (ui_acerca.WindowState == FormWindowState.Minimized)
                    ui_acerca.WindowState = FormWindowState.Normal;
                ui_acerca.Activate();
                ui_acerca.Show();
                ui_acerca.BringToFront();
            }
        }

        private void pDT0601PlanillaElectrónicaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ui_PlanElecSunat ui_planelec = new ui_PlanElecSunat();
            ui_planelec.MdiParent = this;
            if (ui_planelec.WindowState == FormWindowState.Minimized)
                ui_planelec.WindowState = FormWindowState.Normal;
            ui_planelec.Activate();
            ui_planelec.Show();
            ui_planelec.BringToFront();
        }

        private void toolStripMenuItem12_Click(object sender, EventArgs e)
        {

        }

        private void resumenPorTrabajadorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ui_EmisionDestajoPorTrab_Periodo ui_resumendestajotrab = new ui_EmisionDestajoPorTrab_Periodo();
            ui_resumendestajotrab.MdiParent = this;
            if (ui_resumendestajotrab.WindowState == FormWindowState.Minimized)
                ui_resumendestajotrab.WindowState = FormWindowState.Normal;
            ui_resumendestajotrab.Activate();
            ui_resumendestajotrab.Show();
            ui_resumendestajotrab.BringToFront();
        }

        private void toolStripMenuItem13_Click(object sender, EventArgs e)
        {

        }

        private void toolStripMenuItem14_Click(object sender, EventArgs e)
        {
            ui_ConPdt ui_conpdt = new ui_ConPdt();
            ui_conpdt.MdiParent = this;
            if (ui_conpdt.WindowState == FormWindowState.Minimized)
                ui_conpdt.WindowState = FormWindowState.Normal;
            ui_conpdt.Activate();
            ui_conpdt.Show();
            ui_conpdt.BringToFront();
        }

        private void acumuladosQuintaCategoríaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ui_QuiCat ui_quicat = new ui_QuiCat();
            ui_quicat.MdiParent = this;
            if (ui_quicat.WindowState == FormWindowState.Minimized)
                ui_quicat.WindowState = FormWindowState.Normal;
            ui_quicat.Activate();
            ui_quicat.Show();
            ui_quicat.BringToFront();
        }

        private void copiaDeSeguridadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ui_Backup ui_backup = new ui_Backup();
            ui_backup.MdiParent = this;
            if (ui_backup.WindowState == FormWindowState.Minimized)
                ui_backup.WindowState = FormWindowState.Normal;
            ui_backup.Activate();
            ui_backup.Show();
            ui_backup.BringToFront();
        }

        private void aFPNetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ui_AfpNet ui_afpnet = new ui_AfpNet();
            ui_afpnet.MdiParent = this;
            if (ui_afpnet.WindowState == FormWindowState.Minimized)
                ui_afpnet.WindowState = FormWindowState.Normal;
            ui_afpnet.Activate();
            ui_afpnet.Show();
            ui_afpnet.BringToFront();
        }

        private void resumenMensuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ui_ResPlan ui_resplan = new ui_ResPlan();
            ui_resplan.MdiParent = this;
            if (ui_resplan.WindowState == FormWindowState.Minimized)
                ui_resplan.WindowState = FormWindowState.Normal;
            ui_resplan.Activate();
            ui_resplan.Show();
            ui_resplan.BringToFront();

        }

        private void cálculoGeneralToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ui_procesaplan ui_procesaplan = new ui_procesaplan();
            ui_procesaplan.MdiParent = this;
            if (ui_procesaplan.WindowState == FormWindowState.Minimized)
                ui_procesaplan.WindowState = FormWindowState.Normal;
            ui_procesaplan.Activate();
            ui_procesaplan.Show();
            ui_procesaplan.BringToFront();
        }

        private void cálculoPorTrabajadorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ui_CalculaPlanPersonal ui_procesaplanper = new ui_CalculaPlanPersonal();
            ui_procesaplanper.MdiParent = this;
            if (ui_procesaplanper.WindowState == FormWindowState.Minimized)
                ui_procesaplanper.WindowState = FormWindowState.Normal;
            ui_procesaplanper.Activate();
            ui_procesaplanper.Show();
            ui_procesaplanper.BringToFront();
        }

        private void formatoPlanillaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ui_BoletaWin ui_boletawin = new ui_BoletaWin();
            ui_boletawin.MdiParent = this;
            if (ui_boletawin.WindowState == FormWindowState.Minimized)
                ui_boletawin.WindowState = FormWindowState.Normal;
            ui_boletawin.Activate();
            ui_boletawin.Show();
            ui_boletawin.BringToFront();
        }

        private void detallePorConceptosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ui_Boleta ui_boleta = new ui_Boleta();
            ui_boleta.MdiParent = this;
            if (ui_boleta.WindowState == FormWindowState.Minimized)
                ui_boleta.WindowState = FormWindowState.Normal;
            ui_boleta.Activate();
            ui_boleta.Show();
            ui_boleta.BringToFront();
        }

        private void toolStripMenuItem8_Click(object sender, EventArgs e)
        {

        }

        private void formatoDePlanillaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ui_BoletaxEmpleadorWin ui_boletaempwin = new ui_BoletaxEmpleadorWin();
            ui_boletaempwin.MdiParent = this;
            if (ui_boletaempwin.WindowState == FormWindowState.Minimized)
                ui_boletaempwin.WindowState = FormWindowState.Normal;
            ui_boletaempwin.Activate();
            ui_boletaempwin.Show();
            ui_boletaempwin.BringToFront();
        }

        private void detallePorConceptosToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            ui_BoletaxEmpleador ui_boletaemp = new ui_BoletaxEmpleador();
            ui_boletaemp.MdiParent = this;
            if (ui_boletaemp.WindowState == FormWindowState.Minimized)
                ui_boletaemp.WindowState = FormWindowState.Normal;
            ui_boletaemp.Activate();
            ui_boletaemp.Show();
            ui_boletaemp.BringToFront();
        }

        private void xxxToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BackgroundWorker ui_resplan = new BackgroundWorker();
            ui_resplan.MdiParent = this;
            if (ui_resplan.WindowState == FormWindowState.Minimized)
                ui_resplan.WindowState = FormWindowState.Normal;
            ui_resplan.Activate();
            ui_resplan.Show();
            ui_resplan.BringToFront();
        }

        private void parteDiarioToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ui_destajo ui_destajo = new ui_destajo();
            ui_destajo.MdiParent = this;
            if (ui_destajo.WindowState == FormWindowState.Minimized)
                ui_destajo.WindowState = FormWindowState.Normal;
            ui_destajo.Activate();
            ui_destajo.Show();
            ui_destajo.BringToFront();
        }

        private void partePorPeriodoLaboralToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ui_destajoPeriodo ui_destajoperiodo = new ui_destajoPeriodo();
            ui_destajoperiodo.MdiParent = this;
            if (ui_destajoperiodo.WindowState == FormWindowState.Minimized)
                ui_destajoperiodo.WindowState = FormWindowState.Normal;
            ui_destajoperiodo.Activate();
            ui_destajoperiodo.Show();
            ui_destajoperiodo.BringToFront();
        }

        private void distribuciónAutomáticaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ui_procesadetajoautomatico ui_procesades = new ui_procesadetajoautomatico();
            ui_procesades.MdiParent = this;
            if (ui_procesades.WindowState == FormWindowState.Minimized)
                ui_procesades.WindowState = FormWindowState.Normal;
            ui_procesades.Activate();
            ui_procesades.Show();
            ui_procesades.BringToFront();
        }

        private void cambioDePrecioPorZonasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ui_CambioPrecioDestajo ui_cambioprecio = new ui_CambioPrecioDestajo();
            ui_cambioprecio.MdiParent = this;
            if (ui_cambioprecio.WindowState == FormWindowState.Minimized)
                ui_cambioprecio.WindowState = FormWindowState.Normal;
            ui_cambioprecio.Activate();
            ui_cambioprecio.Show();
            ui_cambioprecio.BringToFront();
        }

        private void productosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ui_proddestajo ui_proddestajo = new ui_proddestajo();
            ui_proddestajo.MdiParent = this;
            if (ui_proddestajo.WindowState == FormWindowState.Minimized)
                ui_proddestajo.WindowState = FormWindowState.Normal;
            ui_proddestajo.Activate();
            ui_proddestajo.Show();
            ui_proddestajo.BringToFront();
        }

        private void zonasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ui_zonatrabajo ui_zonatrabajo = new ui_zonatrabajo();
            ui_zonatrabajo.MdiParent = this;
            if (ui_zonatrabajo.WindowState == FormWindowState.Minimized)
                ui_zonatrabajo.WindowState = FormWindowState.Normal;
            ui_zonatrabajo.Activate();
            ui_zonatrabajo.Show();
            ui_zonatrabajo.BringToFront();
        }

        private void conceptosCalculadosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ui_ConceptosCalculados ui_conceptoscalculados = new ui_ConceptosCalculados();
            ui_conceptoscalculados.MdiParent = this;
            if (ui_conceptoscalculados.WindowState == FormWindowState.Minimized)
                ui_conceptoscalculados.WindowState = FormWindowState.Normal;
            ui_conceptoscalculados.Activate();
            ui_conceptoscalculados.Show();
            ui_conceptoscalculados.BringToFront();
        }

        private void toolStripMenuItem13_Click_1(object sender, EventArgs e)
        {
            ui_gratifica ui_gratifica = new ui_gratifica();
            ui_gratifica.MdiParent = this;
            if (ui_gratifica.WindowState == FormWindowState.Minimized)
                ui_gratifica.WindowState = FormWindowState.Normal;
            ui_gratifica.Activate();
            ui_gratifica.Show();
            ui_gratifica.BringToFront();
        }

        private void optimizarTablasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ui_manteTablas ui_mantetablas = new ui_manteTablas();
            ui_mantetablas.MdiParent = this;
            if (ui_mantetablas.WindowState == FormWindowState.Minimized)
                ui_mantetablas.WindowState = FormWindowState.Normal;
            ui_mantetablas.Activate();
            ui_mantetablas.Show();
            ui_mantetablas.BringToFront();
        }

        private void toolStripMenuItem12_Click_1(object sender, EventArgs e)
        {

        }

        private void mantenimientoDePréstamosToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void toolStripMenuItem15_Click(object sender, EventArgs e)
        {
            ui_presper ui_presper = new ui_presper();
            ui_presper.MdiParent = this;
            if (ui_presper.WindowState == FormWindowState.Minimized)
                ui_presper.WindowState = FormWindowState.Normal;
            ui_presper.Activate();
            ui_presper.Show();
            ui_presper.BringToFront();
        }

        private void registroDeVacacionesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ui_regvac ui_regvac = new ui_regvac();
            ui_regvac.MdiParent = this;
            if (ui_regvac.WindowState == FormWindowState.Minimized)
                ui_regvac.WindowState = FormWindowState.Normal;
            ui_regvac.Activate();
            ui_regvac.Show();
            ui_regvac.BringToFront();
        }

        private void historialDeVacacionesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ui_historialvaca ui_historialvaca = new ui_historialvaca();
            ui_historialvaca.MdiParent = this;
            if (ui_historialvaca.WindowState == FormWindowState.Minimized)
                ui_historialvaca.WindowState = FormWindowState.Normal;
            ui_historialvaca.Activate();
            ui_historialvaca.Show();
            ui_historialvaca.BringToFront();
        }

        private void sistemaConcarSQLToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void detallePorConceptosPorRangoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ui_BoletaxEmpleadorxRango ui_BoletaxEmpleadorxRango = new ui_BoletaxEmpleadorxRango();
            ui_BoletaxEmpleadorxRango.MdiParent = this;
            if (ui_BoletaxEmpleadorxRango.WindowState == FormWindowState.Minimized)
                ui_BoletaxEmpleadorxRango.WindowState = FormWindowState.Normal;
            ui_BoletaxEmpleadorxRango.Activate();
            ui_BoletaxEmpleadorxRango.Show();
            ui_BoletaxEmpleadorxRango.BringToFront();
        }

        private void porEmpleadorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ui_Plan ui_plan = new ui_Plan();
            ui_plan.MdiParent = this;
            if (ui_plan.WindowState == FormWindowState.Minimized)
                ui_plan.WindowState = FormWindowState.Normal;
            ui_plan.Activate();
            ui_plan.Show();
            ui_plan.BringToFront();
        }

        private void porTrabajadorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ui_plantrab ui_plantrab = new ui_plantrab();
            ui_plantrab.MdiParent = this;
            if (ui_plantrab.WindowState == FormWindowState.Minimized)
                ui_plantrab.WindowState = FormWindowState.Normal;
            ui_plantrab.Activate();
            ui_plantrab.Show();
            ui_plantrab.BringToFront();
        }

        private void toolStripMenuItem16_Click(object sender, EventArgs e)
        {
            ui_datareten ui_datareten = new ui_datareten();
            ui_datareten.MdiParent = this;
            if (ui_datareten.WindowState == FormWindowState.Minimized)
                ui_datareten.WindowState = FormWindowState.Normal;
            ui_datareten.Activate();
            ui_datareten.Show();
            ui_datareten.BringToFront();
        }

        private void toolStripMenuItem17_Click(object sender, EventArgs e)
        {
            ui_labret ui_labret = new ui_labret();
            ui_labret.MdiParent = this;
            if (ui_labret.WindowState == FormWindowState.Minimized)
                ui_labret.WindowState = FormWindowState.Normal;
            ui_labret.Activate();
            ui_labret.Show();
            ui_labret.BringToFront();
        }

        private void parteDiario4taY5taCategoríaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ui_emireten_partediario ui_emireten_partediario = new ui_emireten_partediario();
            ui_emireten_partediario.MdiParent = this;
            if (ui_emireten_partediario.WindowState == FormWindowState.Minimized)
                ui_emireten_partediario.WindowState = FormWindowState.Normal;
            ui_emireten_partediario.Activate();
            ui_emireten_partediario.Show();
            ui_emireten_partediario.BringToFront();
        }

        private void pDTPlanillaElectrónicaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ui_PlanElecReten ui_PlanElecReten = new ui_PlanElecReten();
            ui_PlanElecReten.MdiParent = this;
            if (ui_PlanElecReten.WindowState == FormWindowState.Minimized)
                ui_PlanElecReten.WindowState = FormWindowState.Normal;
            ui_PlanElecReten.Activate();
            ui_PlanElecReten.Show();
            ui_PlanElecReten.BringToFront();
        }

        private void resumenPorPeriodoTributarioToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ui_res5tacat ui_res5tacat = new ui_res5tacat();
            ui_res5tacat.MdiParent = this;
            if (ui_res5tacat.WindowState == FormWindowState.Minimized)
                ui_res5tacat.WindowState = FormWindowState.Normal;
            ui_res5tacat.Activate();
            ui_res5tacat.Show();
            ui_res5tacat.BringToFront();
        }

        private void generarAsientoDePlanillaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ui_asientos ui_asientos = new ui_asientos();
            ui_asientos.MdiParent = this;
            if (ui_asientos.WindowState == FormWindowState.Minimized)
                ui_asientos.WindowState = FormWindowState.Normal;
            ui_asientos.Activate();
            ui_asientos.Show();
            ui_asientos.BringToFront();
        }

        private void cuentasContablesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ui_cuentas ui_cuentas = new ui_cuentas();
            ui_cuentas.MdiParent = this;
            if (ui_cuentas.WindowState == FormWindowState.Minimized)
                ui_cuentas.WindowState = FormWindowState.Normal;
            ui_cuentas.Activate();
            ui_cuentas.Show();
            ui_cuentas.BringToFront();
        }

        private void resumenDeParteDiarioPorCentroDeCostoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ui_rescuarta ui_rescuarta = new ui_rescuarta();
            ui_rescuarta.MdiParent = this;
            if (ui_rescuarta.WindowState == FormWindowState.Minimized)
                ui_rescuarta.WindowState = FormWindowState.Normal;
            ui_rescuarta.Activate();
            ui_rescuarta.Show();
            ui_rescuarta.BringToFront();
        }

        private void impresiónTextoPorRangoAlfabéticoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ui_BoletaxEmpleadorxAlfa ui_boletaxempleadorxalfa = new ui_BoletaxEmpleadorxAlfa();
            ui_boletaxempleadorxalfa.MdiParent = this;
            if (ui_boletaxempleadorxalfa.WindowState == FormWindowState.Minimized)
                ui_boletaxempleadorxalfa.WindowState = FormWindowState.Normal;
            ui_boletaxempleadorxalfa.Activate();
            ui_boletaxempleadorxalfa.Show();
            ui_boletaxempleadorxalfa.BringToFront();
        }

        private void bonificacionesExtraordinariasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ui_bonificaciones ui_bonificaciones = new ui_bonificaciones();
            ui_bonificaciones.MdiParent = this;
            if (ui_bonificaciones.WindowState == FormWindowState.Minimized)
                ui_bonificaciones.WindowState = FormWindowState.Normal;
            ui_bonificaciones.Activate();
            ui_bonificaciones.Show();
            ui_bonificaciones.BringToFront();
        }

        private void TAMA01_Click(object sender, EventArgs e)
        {
            if (this.Controls.Find("ui_maesgen", true).Count() == 0)
            {
                ui_maesgen ui_maesgen = new ui_maesgen();
                ui_maesgen.MdiParent = this;
                if (ui_maesgen.WindowState == FormWindowState.Minimized)
                    ui_maesgen.WindowState = FormWindowState.Normal;
                ui_maesgen.Activate();
                ui_maesgen.Show();
                ui_maesgen.BringToFront();
            }
        }

        private void TAMA02_Click(object sender, EventArgs e)
        {
            ui_maespdt ui_maespdt = new ui_maespdt();
            ui_maespdt.MdiParent = this;
            if (ui_maespdt.WindowState == FormWindowState.Minimized)
                ui_maespdt.WindowState = FormWindowState.Normal;
            ui_maespdt.Activate();
            ui_maespdt.Show();
            ui_maespdt.BringToFront();
        }

        private void TAMA03_Click(object sender, EventArgs e)
        {
            ui_sisparm ui_sisparm = new ui_sisparm();
            ui_sisparm.MdiParent = this;
            if (ui_sisparm.WindowState == FormWindowState.Minimized)
                ui_sisparm.WindowState = FormWindowState.Normal;
            ui_sisparm.Activate();
            ui_sisparm.Show();
            ui_sisparm.BringToFront();
        }

        private void TAMA04_Click(object sender, EventArgs e)
        {
            ui_fonpen ui_fonpen = new ui_fonpen();
            ui_fonpen.MdiParent = this;
            if (ui_fonpen.WindowState == FormWindowState.Minimized)
                ui_fonpen.WindowState = FormWindowState.Normal;
            ui_fonpen.Activate();
            ui_fonpen.Show();
            ui_fonpen.BringToFront();
        }

        private void TAMA05_Click(object sender, EventArgs e)
        {
            if (this.Controls.Find("ui_cencos", true).Count() == 0)
            {
                ui_cencos ui_cencos = new ui_cencos();
                ui_cencos.MdiParent = this;
                if (ui_cencos.WindowState == FormWindowState.Minimized)
                    ui_cencos.WindowState = FormWindowState.Normal;
                ui_cencos.Activate();
                ui_cencos.Show();
                ui_cencos.BringToFront();
            }
        }

        private void TAMA06_Click(object sender, EventArgs e)
        {
            ui_laborper ui_laborper = new ui_laborper();
            ui_laborper.MdiParent = this;
            if (ui_laborper.WindowState == FormWindowState.Minimized)
                ui_laborper.WindowState = FormWindowState.Normal;
            ui_laborper.Activate();
            ui_laborper.Show();
            ui_laborper.BringToFront();
        }

        private void TAMA0701_Click(object sender, EventArgs e)
        {
            ui_calplan ui_calplan = new ui_calplan();
            ui_calplan.MdiParent = this;
            if (ui_calplan.WindowState == FormWindowState.Minimized)
                ui_calplan.WindowState = FormWindowState.Normal;
            ui_calplan.Activate();
            ui_calplan.Show();
            ui_calplan.BringToFront();
        }

        private void TAMA0702_Click(object sender, EventArgs e)
        {
            ui_EstadoCalPlan ui_estadocalplan = new ui_EstadoCalPlan();
            ui_estadocalplan.MdiParent = this;
            if (ui_estadocalplan.WindowState == FormWindowState.Minimized)
                ui_estadocalplan.WindowState = FormWindowState.Normal;
            ui_estadocalplan.Activate();
            ui_estadocalplan.Show();
            ui_estadocalplan.BringToFront();
        }

        private void TAMA08_Click(object sender, EventArgs e)
        {
            if (this.Controls.Find("ui_alalma", true).Count() == 0)
            {
                ui_alalma ui_alalma = new ui_alalma();
                ui_alalma.MdiParent = this;
                if (ui_alalma.WindowState == FormWindowState.Minimized)
                    ui_alalma.WindowState = FormWindowState.Normal;
                ui_alalma.Activate();
                ui_alalma.Show();
                ui_alalma.BringToFront();
            }
        }

        private void TAMA09_Click(object sender, EventArgs e)
        {
            if (this.Controls.Find("ui_grupoarti", true).Count() == 0)
            {
                ui_grupoarti ui_grupoarti = new ui_grupoarti();
                ui_grupoarti.MdiParent = this;
                if (ui_grupoarti.WindowState == FormWindowState.Minimized)
                    ui_grupoarti.WindowState = FormWindowState.Normal;
                ui_grupoarti.Activate();
                ui_grupoarti.Show();
                ui_grupoarti.BringToFront();
            }
        }

        private void parámetrosDeImpresiónToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ui_configsis ui_configsis = new ui_configsis();
            ui_configsis.MdiParent = this;
            if (ui_configsis.WindowState == FormWindowState.Minimized)
                ui_configsis.WindowState = FormWindowState.Normal;
            ui_configsis.Activate();
            ui_configsis.Show();
            ui_configsis.BringToFront();
        }

        private void reporteDelPersonalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ui_emiper ui_emiper = new ui_emiper();
            ui_emiper.MdiParent = this;
            if (ui_emiper.WindowState == FormWindowState.Minimized)
                ui_emiper.WindowState = FormWindowState.Normal;
            ui_emiper.Activate();
            ui_emiper.Show();
            ui_emiper.BringToFront();
        }

        private void pDTPlanillaElectrónicaPLAMEToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ui_pdtplame ui_pdtplame = new ui_pdtplame();
            ui_pdtplame.MdiParent = this;
            if (ui_pdtplame.WindowState == FormWindowState.Minimized)
                ui_pdtplame.WindowState = FormWindowState.Normal;
            ui_pdtplame.Activate();
            ui_pdtplame.Show();
            ui_pdtplame.BringToFront();
        }

        private void controlDiarioDeAsistenciaToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void controlDiarioDeAsistenciaToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            ui_asisdia ui_asisdia = new ui_asisdia();
            ui_asisdia.MdiParent = this;
            if (ui_asisdia.WindowState == FormWindowState.Minimized)
                ui_asisdia.WindowState = FormWindowState.Normal;
            ui_asisdia.Activate();
            ui_asisdia.Show();
            ui_asisdia.BringToFront();
        }

        private void transferenciaDeInformaciónControlDiarioAParteDePlanillaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ui_transcontrol ui_transcontrol = new ui_transcontrol();
            ui_transcontrol.MdiParent = this;
            if (ui_transcontrol.WindowState == FormWindowState.Minimized)
                ui_transcontrol.WindowState = FormWindowState.Normal;
            ui_transcontrol.Activate();
            ui_transcontrol.Show();
            ui_transcontrol.BringToFront();
        }

        private void ejecutarScripSQLToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.Controls.Find("ui_pack", true).Count() == 0)
            {
                ui_pack ui_pack = new ui_pack();
                ui_pack.MdiParent = this;
                if (ui_pack.WindowState == FormWindowState.Minimized)
                    ui_pack.WindowState = FormWindowState.Normal;
                ui_pack.Activate();
                ui_pack.Show();
                ui_pack.BringToFront();
            }
        }

        private void menuMDI_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void recalcularImporteNetoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ui_recaneto ui_recaneto = new ui_recaneto();
            ui_recaneto.MdiParent = this;
            if (ui_recaneto.WindowState == FormWindowState.Minimized)
                ui_recaneto.WindowState = FormWindowState.Normal;
            ui_recaneto.Activate();
            ui_recaneto.Show();
            ui_recaneto.BringToFront();
        }

        private void pDTPlanillaElectrónicaPLAMEToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            plamereten plamereten = new plamereten();
            plamereten.MdiParent = this;
            if (plamereten.WindowState == FormWindowState.Minimized)
                plamereten.WindowState = FormWindowState.Normal;
            plamereten.Activate();
            plamereten.Show();
            plamereten.BringToFront();
        }

        private void consolidadoDiasSubsidiadosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            reporte_subsidiados ui_form = new reporte_subsidiados();
            ui_form.MdiParent = this;
            if (ui_form.WindowState == FormWindowState.Minimized)
                ui_form.WindowState = FormWindowState.Normal;
            ui_form.Activate();
            ui_form.Show();
            ui_form.BringToFront();
        }

        private void toolStripButton13_Click(object sender, EventArgs e)
        {
            ui_BoletaxEmpleadorHorasExtras ui_form = new ui_BoletaxEmpleadorHorasExtras();
            ui_form.MdiParent = this;
            if (ui_form.WindowState == FormWindowState.Minimized)
                ui_form.WindowState = FormWindowState.Normal;
            ui_form.Activate();
            ui_form.Show();
            ui_form.BringToFront();
        }

        private void tiposDeHorariosAsistenciaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.Controls.Find("ui_horarios", true).Count() == 0)
            {
                ui_horarios ui_form = new ui_horarios();
                ui_form.MdiParent = this;
                if (ui_form.WindowState == FormWindowState.Minimized)
                    ui_form.WindowState = FormWindowState.Normal;
                ui_form.Activate();
                ui_form.Show();
                ui_form.BringToFront();
            }
        }

        private void mTrabajadores_Click(object sender, EventArgs e)
        {
            if (this.Controls.Find("ui_trabajadores", true).Count() == 0)
            {
                ui_trabajadores ui_form = new ui_trabajadores();
                ui_form.MdiParent = this;
                if (ui_form.WindowState == FormWindowState.Minimized)
                    ui_form.WindowState = FormWindowState.Normal;
                ui_form.Activate();
                ui_form.Show();
                ui_form.BringToFront();
            }
        }

        private void mTipoHorarios_Click(object sender, EventArgs e)
        {
            if (this.Controls.Find("ui_horarios", true).Count() == 0)
            {
                ui_horarios ui_form = new ui_horarios();
                ui_form.MdiParent = this;
                if (ui_form.WindowState == FormWindowState.Minimized)
                    ui_form.WindowState = FormWindowState.Normal;
                ui_form.Activate();
                ui_form.Show();
                ui_form.BringToFront();
            }
        }

        private void btnProgramacion_Click(object sender, EventArgs e)
        {
            if (UsuarioAgri())
            {
                if (this.Controls.Find("ui_programacionGrafAgri", true).Count() == 0)
                {
                    ui_programacionGrafAgri ui_form = new ui_programacionGrafAgri();
                    ui_form.MdiParent = this;
                    if (ui_form.WindowState == FormWindowState.Minimized)
                        ui_form.WindowState = FormWindowState.Normal;
                    ui_form.Activate();
                    ui_form.Show();
                    ui_form.BringToFront();
                }
            }
            else
            {
                if (this.Controls.Find("ui_programacionGraf", true).Count() == 0)
                {
                    ui_programacionGraf ui_form = new ui_programacionGraf();
                    ui_form.MdiParent = this;
                    if (ui_form.WindowState == FormWindowState.Minimized)
                        ui_form.WindowState = FormWindowState.Normal;
                    ui_form.Activate();
                    ui_form.Show();
                    ui_form.BringToFront();
                }
            }
        }

        private bool UsuarioAgri()
        {
            GlobalVariables variables = new GlobalVariables();
            bool resultado = false;
            string query = "SELECT COUNT(1) [result] FROM maesgen (NOLOCK) WHERE idmaesgen='161' AND parm1maesgen='" + variables.getValorUsr() + "'";

            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();

            try
            {
                SqlCommand myCommand = new SqlCommand(query, conexion);
                SqlDataReader odr = myCommand.ExecuteReader();

                while (odr.Read())
                {
                    if (odr.GetInt32(odr.GetOrdinal("result")) > 0)
                    {
                        resultado = true;
                    }
                }

                odr.Close();
                myCommand.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            finally { conexion.Close(); }

            return resultado;
        }

        private void btnReporteAsis_Click(object sender, EventArgs e)
        {
            if (this.Controls.Find("ui_rep_asistencia", true).Count() == 0)
            {
                ui_rep_asistencia ui_form = new ui_rep_asistencia();
                ui_form.MdiParent = this;
                if (ui_form.WindowState == FormWindowState.Minimized)
                    ui_form.WindowState = FormWindowState.Maximized;
                //ui_form.WindowState = FormWindowState.Maximized;
                ui_form.Activate();
                ui_form.Show();
                ui_form.BringToFront();
            }
        }

        private void MAPL0904_Click(object sender, EventArgs e)
        {
            if (this.Controls.Find("ui_rep_asistencia", true).Count() == 0)
            {
                ui_rep_asistencia ui_form = new ui_rep_asistencia();
                ui_form.MdiParent = this;
                if (ui_form.WindowState == FormWindowState.Minimized)
                    ui_form.WindowState = FormWindowState.Normal;
                ui_form.Activate();
                ui_form.Show();
                ui_form.BringToFront();
            }
        }

        private void MAPL0905_Click(object sender, EventArgs e)
        {
            if (this.Controls.Find("ui_rep_asistencia_sunafil", true).Count() == 0)
            {
                ui_rep_asistencia_sunafil ui_form = new ui_rep_asistencia_sunafil();
                ui_form.MdiParent = this;
                if (ui_form.WindowState == FormWindowState.Minimized)
                    ui_form.WindowState = FormWindowState.Normal;
                ui_form.Activate();
                ui_form.Show();
                ui_form.BringToFront();
            }
        }

        private void MAPL0906_Click(object sender, EventArgs e)
        {
            if (this.Controls.Find("ui_rep_sisasis_biosalc", true).Count() == 0)
            {
                ui_rep_sisasis_biosalc ui_form = new ui_rep_sisasis_biosalc();
                ui_form.MdiParent = this;
                if (ui_form.WindowState == FormWindowState.Minimized)
                    ui_form.WindowState = FormWindowState.Normal;
                ui_form.Activate();
                ui_form.Show();
                ui_form.BringToFront();
            }
        }

        private void MAPL0907_Click(object sender, EventArgs e)
        {
            if (this.Controls.Find("ui_rep_ausentes", true).Count() == 0)
            {
                ui_rep_ausentes ui_form = new ui_rep_ausentes();
                ui_form.MdiParent = this;
                if (ui_form.WindowState == FormWindowState.Minimized)
                    ui_form.WindowState = FormWindowState.Normal;
                ui_form.Activate();
                ui_form.Show();
                ui_form.BringToFront();
            }
        }

        private void MAPL1001_Click(object sender, EventArgs e)
        {
            if (UsuarioAgri())
            {
                if (this.Controls.Find("ui_programacionGrafAgri", true).Count() == 0)
                {
                    ui_programacionGrafAgri ui_form = new ui_programacionGrafAgri();
                    ui_form.MdiParent = this;
                    if (ui_form.WindowState == FormWindowState.Minimized)
                        ui_form.WindowState = FormWindowState.Normal;
                    ui_form.Activate();
                    ui_form.Show();
                    ui_form.BringToFront();
                }
            }
            else
            {
                if (this.Controls.Find("ui_programacionGraf", true).Count() == 0)
                {
                    ui_programacionGraf ui_form = new ui_programacionGraf();
                    ui_form.MdiParent = this;
                    if (ui_form.WindowState == FormWindowState.Minimized)
                        ui_form.WindowState = FormWindowState.Normal;
                    ui_form.Activate();
                    ui_form.Show();
                    ui_form.BringToFront();
                }
            }
        }

        private void MAPL1002_Click(object sender, EventArgs e)
        {
            if (this.Controls.Find("ui_programacionInd", true).Count() == 0)
            {
                ui_programacionInd ui_form = new ui_programacionInd();
                ui_form.MdiParent = this;
                if (ui_form.WindowState == FormWindowState.Minimized)
                    ui_form.WindowState = FormWindowState.Normal;
                ui_form.Activate();
                ui_form.Show();
                ui_form.BringToFront();
            }
        }

        private void MAPL1003_Click(object sender, EventArgs e)
        {
            if (this.Controls.Find("ui_asismanual", true).Count() == 0)
            {
                ui_asismanual ui_form = new ui_asismanual();
                ui_form.MdiParent = this;
                if (ui_form.WindowState == FormWindowState.Minimized)
                    ui_form.WindowState = FormWindowState.Normal;
                ui_form.Activate();
                ui_form.Show();
                ui_form.BringToFront();
            }
        }

        private void MAPL1004_Click(object sender, EventArgs e)
        {
            if (this.Controls.Find("ui_agrupaturnos", true).Count() == 0)
            {
                ui_agrupaturnos ui_form = new ui_agrupaturnos();
                ui_form.MdiParent = this;
                if (ui_form.WindowState == FormWindowState.Minimized)
                    ui_form.WindowState = FormWindowState.Normal;
                ui_form.Activate();
                ui_form.Show();
                ui_form.BringToFront();
            }
        }

        private void btnSincronizar_Click(object sender, EventArgs e)
        {
            Sincronizacion();
        }

        private Process p;

        private void Presionar(int tiempo, string tecla)
        {
            Thread.Sleep(tiempo * 1000);
            SendKeys.SendWait(tecla);
        }
        public void Sincronizacion()
        {
            #region Old Code
            //String path = string.Empty;// @"C:\Caña Brava\_Asistencia\Asistencia\Maestro Caña Brava 13.08.2020.xlsx";// dialog.FileName; // get name of file
            //String servidor = string.Empty;

            //FileInfo fa = new FileInfo(AppDomain.CurrentDomain.BaseDirectory + "/directorio.txt");
            //if (fa.Exists)
            //{
            //    OpeIO opeIO = new OpeIO(AppDomain.CurrentDomain.BaseDirectory + "/directorio.txt");
            //    servidor = opeIO.ReadLineByNum(1);
            //    path = opeIO.ReadLineByNum(2);
            //}

            //int row_ = 1, column_ = 1;
            //int iniRow = 2, finRow = row_;
            //int iniCol = 1, finCol = 0;

            //MaestroSAP objSAP = new MaestroSAP();
            //if (File.Exists(path))
            //{
            //    FileInfo fileInfo = new FileInfo(path);
            //    using (ExcelPackage xlPackage = new ExcelPackage(fileInfo))
            //    {
            //        lblCargando.Visible = true;
            //        loadingNext1.Visible = true;
            //        Application.DoEvents();

            //        ExcelWorksheet ws = null;

            //        ws = xlPackage.Workbook.Worksheets[1];
            //        if (ws != null)
            //        {
            //            #region Obtenemos Total Columnas
            //            bool valide = true;
            //            while (valide)
            //            {
            //                if (ws.Cells[row_, column_].Value != null) { column_++; }
            //                else { valide = false; }
            //                finCol = column_;
            //            }
            //            #endregion

            //            #region Obtenemos Total Filas
            //            valide = true;
            //            row_ = 2;
            //            column_ = 1;
            //            while (valide)
            //            {
            //                if (ws.Cells[row_, column_].Value != null) { row_++; }
            //                else { valide = false; }
            //                finRow = row_;
            //            }
            //            #endregion

            //            string cadena = string.Empty;
            //            string query = string.Empty;
            //            for (int i = iniRow; i < finRow; i++)
            //            {
            //                query += "INSERT INTO MaestroSAP VALUES (";
            //                column_ = 1;
            //                valide = true;

            //                for (int x = iniCol; x < finCol; x++)
            //                {
            //                    cadena = (ws.Cells[i, x].Value != null ? ws.Cells[i, x].Value.ToString() : string.Empty);
            //                    if (x == 213 & cadena == string.Empty) { cadena = "0000"; }
            //                    switch (x)
            //                    {
            //                        case 1:
            //                        case 12://16
            //                        case 173://213
            //                            if (cadena != string.Empty)
            //                            {
            //                                cadena = "0000" + cadena;
            //                                query += "'" + cadena.Substring(cadena.Length - 8) + "',";
            //                            }
            //                            else { query += "'0',"; }
            //                            break;
            //                        case 78://82
            //                        //case 129://114
            //                        case 130://119
            //                        case 131://133
            //                        case 132://134
            //                        case 133://168
            //                        case 193://169
            //                        //case 170:
            //                        //case 171:
            //                        //case 238:
            //                            if (cadena.Length > 0)
            //                            {
            //                                cadena = DateTime.ParseExact(cadena.Substring(0, 10), "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd");
            //                            }
            //                            query += "'" + cadena + "',";
            //                            break;
            //                        default:
            //                            query += "'" + cadena.Replace("'", "") + "',";
            //                            break;
            //                    }
            //                }

            //                query += ");\r\n";
            //                //var arrTemp = query.Split(',');
            //                //datosSAP.Add(new MaestroSAP()
            //                //{
            //                //});
            //            }

            //            query = query.Replace("|", "").Replace(",);", ");");

            //            objSAP.UpdateSincronizacion(query);
            //        }
            //    }
            //    lblCargando.Visible = false;
            //    loadingNext1.Visible = false;
            //}
            //else
            //{
            //    MessageBox.Show("No existen datos por actualizar..!!" + path + " / " + AppDomain.CurrentDomain.BaseDirectory
            //        , "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //}
            #endregion

            if (p == null || p.HasExited)
            {
                DataTable data = new DataTable();
                try
                {
                    String Usu = string.Empty, Pas = string.Empty, Ruta = string.Empty, Arch = string.Empty, Prog = string.Empty, Serv = string.Empty;
                    FileInfo fa = new FileInfo(AppDomain.CurrentDomain.BaseDirectory + "/directorio.txt");
                    if (fa.Exists)
                    {
                        OpeIO opeIO = new OpeIO(AppDomain.CurrentDomain.BaseDirectory + "/directorio.txt");
                        Usu = opeIO.ReadLineByNum(1);
                        Pas = opeIO.ReadLineByNum(2);
                        Ruta = opeIO.ReadLineByNum(3);
                        Arch = opeIO.ReadLineByNum(4);
                        Prog = opeIO.ReadLineByNum(5);
                        Serv = opeIO.ReadLineByNum(6);
                    }

                    // Start the child process.
                    // Redirect the output stream of the child process.
                    p = new Process();
                    p.StartInfo.UseShellExecute = false;
                    p.StartInfo.RedirectStandardOutput = true;
                    p.StartInfo.FileName = Prog;
                    p.Start();

                    Presionar(30, "{ENTER}");//OPEN SAP LOGIN

                    Presionar(3, Usu); //ASSESSMENT
                    Presionar(3, "{TAB}"); //$5Sistemas
                    Presionar(2, Pas); //$5Sistemas
                    Presionar(3, "{ENTER}");

                    Presionar(3, "ZHRP1234");
                    Presionar(3, "{ENTER}");

                    Presionar(10, "+{F5}");
                    Presionar(2, "{F8}");
                    Presionar(6, "{F8}");

                    Presionar(400, "^+{F9}");
                    Presionar(10, "{TAB}");
                    Presionar(2, "{ENTER}");

                    Presionar(10, "+{TAB}");
                    Presionar(2, Ruta);//D:/
                    Presionar(2, "{TAB}");
                    Presionar(2, Arch); //Maestro.txt
                    Presionar(2, "{TAB}");
                    Presionar(2, "{TAB}");
                    Presionar(1, "{ENTER}");

                    Presionar(5, "+{TAB}");
                    Presionar(2, "{ENTER}");

                    Presionar(6, "{ESC}");
                    Presionar(2, "{ESC}");
                    Presionar(4, "(%{F4})");
                    Presionar(2, "{TAB}");
                    Presionar(2, "{ENTER}");

                    Presionar(2, "(%{F4})");

                    lblCargando.Visible = true;
                    loadingNext1.Visible = true;
                    Application.DoEvents();

                    FileInfo fab = new FileInfo(Ruta + Arch);
                    if (fab.Exists)
                    {
                        string[] stringarry = System.IO.File.ReadAllLines(Ruta + Arch, Encoding.Default);

                        if (stringarry.Length > 0)
                        {
                            int cc = 0;
                            var arr_ = stringarry[7].Split('|');
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
                            arr_ = stringarry[8].Split('|');
                            int datOld = stringarry[7].Split('|').Length;
                            for (int i = 0; i < arr_.Length; i++)
                            {
                                if (arr_[i].Trim().Length > 0)
                                {
                                    data.Columns.Add("Col" + cc, typeof(string));
                                    cc++;
                                }
                                else
                                {
                                    data.Columns.Add("Vacio" + (i + datOld), typeof(string));
                                    LColumns.Add("Vacio" + (i + datOld));
                                }
                            }
                            data.AcceptChanges();

                            System.Data.DataRow dr = null;
                            for (int i = 10; i < stringarry.Length; i++)
                            {
                                stringarry[i] = stringarry[i].Replace("S/N|", "S/N");
                                var dat = stringarry[i].Split('|');
                                if ((i % 2) == 0)
                                {
                                    dr = data.NewRow();
                                }
                                datOld = stringarry[i - 1].Split('|').Length;
                                for (int y = 0; y < dat.Length; y++)
                                {
                                    if ((i % 2) == 0)
                                    {
                                        dr[y] = dat[y].Trim();
                                    }
                                    else
                                    {
                                        if (dr.ItemArray.Length > (y + datOld))
                                        {
                                            dr[y + datOld] = dat[y].Trim();
                                        }
                                    }
                                }
                                if ((i % 2) != 0)
                                {
                                    data.Rows.Add(dr);
                                }
                            }
                            data.AcceptChanges();

                            //Console.WriteLine("Insertando datos SAP en SISASIS");

                            foreach (var item in LColumns)
                            {
                                data.Columns.Remove(item);
                            }
                            string query = @"IF EXISTS(SELECT * FROM sys.tables WHERE name='perplan_old') BEGIN DROP TABLE perplan_old; END;
SELECT idperplan,idcia,codaux,apepat,apemat,nombres,fecnac,tipdoc,nrodoc,telfijo,celular,rpm,estcivil,nacion,email,catlic,nrolic,tipotrab,idtipoper,nivedu,idlabper,seccion,regpen,cuspp
,contrab,tippag,pering,sitesp,entfinrem,nroctarem,monrem,tipctarem,entfincts,nroctacts,moncts,tipctacts,tipvia,nomvia,nrovia,intvia,tipzona,nomzona,refzona,ubigeo,dscubigeo,sexo,ocurpts
,afiliaeps,eps,discapa,sindica,situatrab,sctrsanin,sctrsaessa,sctrsaeps,sctrpennin,sctrpenonp,sctrpenseg,asigemplea,rucemp,estane,foto,idtipoplan,domicilia,esvida,fecregpen,regalterna,trabmax
,trabnoc,quicat,renexo,asepen,apliconve,exoquicat,paisemi,disnac,deparvia,manzavia,lotevia,kmvia,block,etapa,reglab,ruc,0 alta_tregistro,baja_tregistro,fecharegistro,fechaupdate INTO perplan_old FROM perplan; 
DELETE FROM perplan;";
                            //int cccc = 0;
                            foreach (DataRow item in data.Rows)
                            {
                                query += "INSERT INTO MaestroSAP VALUES (";
                                for (int i = 0; i < item.ItemArray.Length; i++)
                                {
                                    //if (cccc == 332) { Console.WriteLine(item.ItemArray[i].ToString().Replace("'", "") + " Nro: " + item.ItemArray[i].ToString().Length); }
                                    if (i == 2) { query += "'" + int.Parse(item.ItemArray[i].ToString()) + "',"; }
                                    else
                                    {
                                        if (i == 28 || i == 65 || i == 66 || i == 67 || i == 68 || i == 83)
                                        {
                                            if (item.ItemArray[i].ToString().Trim() != string.Empty)
                                            {
                                                query += "'" + DateTime.Parse(item.ItemArray[i].ToString().Replace(".", "/")).ToString("yyyy-MM-dd") + "',";
                                            }
                                            else { query += "'',"; }
                                        }
                                        else
                                        {
                                            query += "'" + item.ItemArray[i].ToString().Replace("'", "") + "',";
                                        }
                                    }
                                }
                                query += ");\r\n";
                                query = query.Replace(",);", ");");
                                //cccc++;
                                //Console.WriteLine("Leyendo Informacion: " + cccc + " rows");
                            }

                            query += @"INSERT INTO perplan 
SELECT idperplan,idcia,codaux,apepat,apemat,nombres,fecnac,tipdoc,nrodoc,telfijo,celular,rpm,estcivil,nacion,email,catlic,nrolic,tipotrab,idtipoper,nivedu,idlabper,seccion,regpen,cuspp
,contrab,tippag,pering,sitesp,entfinrem,nroctarem,monrem,tipctarem,entfincts,nroctacts,moncts,tipctacts,tipvia,nomvia,nrovia,intvia,tipzona,nomzona,refzona,ubigeo,dscubigeo,sexo,ocurpts
,afiliaeps,eps,discapa,sindica,situatrab,sctrsanin,sctrsaessa,sctrsaeps,sctrpennin,sctrpenonp,sctrpenseg,asigemplea,rucemp,estane,foto,idtipoplan,domicilia,esvida,fecregpen,regalterna,trabmax
,trabnoc,quicat,renexo,asepen,apliconve,exoquicat,paisemi,disnac,deparvia,manzavia,lotevia,kmvia,block,etapa,reglab,ruc,0,baja_tregistro,fecharegistro,fechaupdate
FROM perplan_old WHERE idperplan NOT IN (SELECT idperplan FROM perplan); ";
                            query += "DROP TABLE perplan_old;";

                            string connString = @"Data Source=" + Serv + ";Database=Asistencia;uid=usr_asistencia;pwd=4Sist3nc14@21;";
                            SqlConnection conexion = new SqlConnection();
                            conexion.ConnectionString = connString;
                            conexion.Open();

                            SqlCommand myCommand = new SqlCommand(query, conexion);
                            myCommand.CommandTimeout = 360;
                            myCommand.ExecuteNonQuery();
                            myCommand.Dispose();
                            conexion.Close();

                            lblCargando.Visible = false;
                            loadingNext1.Visible = false;
                            MessageBox.Show("Sincronización de datos correcto..!!", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void btnCambiarPass_Click(object sender, EventArgs e)
        {
            if (this.Controls.Find("ui_ChangePass", true).Count() == 0)
            {
                ui_ChangePass ui_form = new ui_ChangePass();
                ui_form.MdiParent = this;
                if (ui_form.WindowState == FormWindowState.Minimized)
                    ui_form.WindowState = FormWindowState.Normal;
                ui_form.Activate();
                ui_form.Show();
                ui_form.BringToFront();
            }
        }

        #region Menu Comedores
        private void COMED01_Click(object sender, EventArgs e)
        {
            if (this.Controls.Find("ui_provee", true).Count() == 0)
            {
                ui_provee ui_provee = new ui_provee();
                ui_provee.MdiParent = this;
                if (ui_provee.WindowState == FormWindowState.Minimized)
                    ui_provee.WindowState = FormWindowState.Normal;
                ui_provee.Activate();
                ui_provee.Show();
                ui_provee.BringToFront();
            }
        }

        private void COMED02_Click(object sender, EventArgs e)
        {
            if (this.Controls.Find("ui_alarti", true).Count() == 0)
            {
                ui_alarti ui_form = new ui_alarti();
                ui_form.MdiParent = this;
                if (ui_form.WindowState == FormWindowState.Minimized)
                    ui_form.WindowState = FormWindowState.Normal;
                ui_form.Activate();
                ui_form.Show();
                ui_form.BringToFront();
            }
        }

        private void COMED03_Click(object sender, EventArgs e)
        {
            if (this.Controls.Find("ui_solialma", true).Count() == 0)
            {
                ui_solialma ui_solialma = new ui_solialma();
                ui_solialma.MdiParent = this;
                if (ui_solialma.WindowState == FormWindowState.Minimized)
                    ui_solialma.WindowState = FormWindowState.Normal;
                ui_solialma.Activate();
                ui_solialma.Show();
                ui_solialma.BringToFront();
            }
        }

        private void COMED04_Click(object sender, EventArgs e)
        {
            if (this.Controls.Find("ui_almov", true).Count() == 0)
            {
                ui_almov ui_almov = new ui_almov();
                ui_almov.MdiParent = this;
                if (ui_almov.WindowState == FormWindowState.Minimized)
                    ui_almov.WindowState = FormWindowState.Normal;
                ui_almov.Activate();
                ui_almov.Show();
                ui_almov.BringToFront();
            }
        }

        private void COMED0501_Click(object sender, EventArgs e)
        {
            if (this.Controls.Find("ui_invitados", true).Count() == 0)
            {
                ui_invitados ui_form = new ui_invitados();
                ui_form.MdiParent = this;
                if (ui_form.WindowState == FormWindowState.Minimized)
                    ui_form.WindowState = FormWindowState.Normal;
                ui_form.Activate();
                ui_form.Show();
                ui_form.BringToFront();
            }
        }

        private void COMED0502_Click(object sender, EventArgs e)
        {
            if (this.Controls.Find("ui_comensales", true).Count() == 0)
            {
                ui_comensales ui_form = new ui_comensales();
                ui_form.MdiParent = this;
                if (ui_form.WindowState == FormWindowState.Minimized)
                    ui_form.WindowState = FormWindowState.Normal;
                ui_form.Activate();
                ui_form.Show();
                ui_form.BringToFront();
            }
        }

        private void COMED0601_Click(object sender, EventArgs e)
        {
            if (this.Controls.Find("ui_stkalma", true).Count() == 0)
            {
                ui_stkalma ui_stkalma = new ui_stkalma();
                ui_stkalma.MdiParent = this;
                if (ui_stkalma.WindowState == FormWindowState.Minimized)
                    ui_stkalma.WindowState = FormWindowState.Normal;
                ui_stkalma.Activate();
                ui_stkalma.Show();
                ui_stkalma.BringToFront();
            }
        }

        private void COMED0602_Click(object sender, EventArgs e)
        {
            if (this.Controls.Find("ui_rep_comedor", true).Count() == 0)
            {
                ui_rep_comedor ui_alalma = new ui_rep_comedor();
                ui_alalma.MdiParent = this;
                if (ui_alalma.WindowState == FormWindowState.Minimized)
                    ui_alalma.WindowState = FormWindowState.Normal;
                ui_alalma.Activate();
                ui_alalma.Show();
                ui_alalma.BringToFront();
            }
        }
        #endregion

        #region Menu GGHyS
        private void GGHS020101_Click(object sender, EventArgs e)
        {
            if (this.Controls.Find("ui_capacitaciones", true).Count() == 0)
            {
                ui_capacitaciones ui_form = new ui_capacitaciones();
                ui_form.MdiParent = this;
                if (ui_form.WindowState == FormWindowState.Minimized)
                    ui_form.WindowState = FormWindowState.Normal;
                ui_form.Activate();
                ui_form.Show();
                ui_form.BringToFront();
            }
        }

        private void GGHS020102_Click(object sender, EventArgs e)
        {
            if (this.Controls.Find("ui_solicapacitaciones", true).Count() == 0)
            {
                ui_solicapacitaciones ui_form = new ui_solicapacitaciones();
                ui_form.MdiParent = this;
                if (ui_form.WindowState == FormWindowState.Minimized)
                    ui_form.WindowState = FormWindowState.Normal;
                ui_form.Activate();
                ui_form.Show();
                ui_form.BringToFront();
            }
        }

        private void ATRAC_SELEC_Click(object sender, EventArgs e)
        {
            if (this.Controls.Find("ui_atraccion_seleccion", true).Count() == 0)
            {
                ui_atraccion_seleccion ui_form = new ui_atraccion_seleccion();
                ui_form.MdiParent = this;
                if (ui_form.WindowState == FormWindowState.Minimized)
                    ui_form.WindowState = FormWindowState.Normal;
                ui_form.Activate();
                ui_form.Show();
                ui_form.BringToFront();
            }
        }

        private void GGHS0301_Click(object sender, EventArgs e)
        {
            if (this.Controls.Find("ui_biosalc_mao", true).Count() == 0)
            {
                //ui_biosalc_mao ui_form = new ui_biosalc_mao();
                ui_rrhhgastos ui_form = new ui_rrhhgastos();
                ui_form.MdiParent = this;
                if (ui_form.WindowState == FormWindowState.Minimized)
                    ui_form.WindowState = FormWindowState.Normal;
                ui_form.Activate();
                ui_form.Show();
                ui_form.BringToFront();
            }
        }

        private void GGHS0401_Click(object sender, EventArgs e)
        {
            if (this.Controls.Find("ui_regdescanso", true).Count() == 0)
            {
                ui_regdescanso ui_form = new ui_regdescanso();
                ui_form.MdiParent = this;
                if (ui_form.WindowState == FormWindowState.Minimized)
                    ui_form.WindowState = FormWindowState.Normal;
                ui_form.Activate();
                ui_form.Show();
                ui_form.BringToFront();
            }
        }

        private void GGHS0402_Click(object sender, EventArgs e)
        {
            if (this.Controls.Find("ui_historialdescanso", true).Count() == 0)
            {
                ui_historialdescanso ui_form = new ui_historialdescanso();
                ui_form.MdiParent = this;
                if (ui_form.WindowState == FormWindowState.Minimized)
                    ui_form.WindowState = FormWindowState.Normal;
                ui_form.Activate();
                ui_form.Show();
                ui_form.BringToFront();
            }
        }
        #endregion

        #region Menu GO
        private void GO0101_Click(object sender, EventArgs e)
        {
            if (this.Controls.Find("ui_ventas", true).Count() == 0)
            {
                ui_ventas ui_form = new ui_ventas();
                ui_form.MdiParent = this;
                if (ui_form.WindowState == FormWindowState.Minimized)
                    ui_form.WindowState = FormWindowState.Normal;
                ui_form.Activate();
                ui_form.Show();
                ui_form.BringToFront();
            }
        }

        private void GO0201_Click(object sender, EventArgs e)
        {
            if (this.Controls.Find("ui_oeecat", true).Count() == 0)
            {
                ui_oeecat ui_form = new ui_oeecat();
                ui_form.MdiParent = this;
                if (ui_form.WindowState == FormWindowState.Minimized)
                    ui_form.WindowState = FormWindowState.Normal;
                ui_form.Activate();
                ui_form.Show();
                ui_form.BringToFront();
            }
        }

        private void GO0301_Click(object sender, EventArgs e)
        {
            if (this.Controls.Find("ui_checklistneuma", true).Count() == 0)
            {
                ui_checklistneuma ui_form = new ui_checklistneuma();
                ui_form.MdiParent = this;
                if (ui_form.WindowState == FormWindowState.Minimized)
                    ui_form.WindowState = FormWindowState.Normal;
                ui_form.Activate();
                ui_form.Show();
                ui_form.BringToFront();
            }

        }
        #endregion

        #region Menu GI
        private void GI0101_Click(object sender, EventArgs e)
        {
            if (this.Controls.Find("ui_rendimientos", true).Count() == 0)
            {
                ui_rendimientos ui_form = new ui_rendimientos();
                ui_form.MdiParent = this;
                if (ui_form.WindowState == FormWindowState.Minimized)
                    ui_form.WindowState = FormWindowState.Normal;
                ui_form.Activate();
                ui_form.Show();
                ui_form.BringToFront();
            }
        }
        private void GI0102_Click(object sender, EventArgs e)
        {
            if (this.Controls.Find("ui_objproducccion", true).Count() == 0){
                ui_objproduccion ui_form = new ui_objproduccion();
                ui_form.MdiParent = this;
                if (ui_form.WindowState == FormWindowState.Minimized)
                    ui_form.WindowState = FormWindowState.Normal;
                ui_form.Activate();
                ui_form.Show();
                ui_form.BringToFront();
            }
        }
        #endregion

        #region Menu GFACI
        private void GFACI0101_Click(object sender, EventArgs e)
        {
            if (this.Controls.Find("ui_controlling", true).Count() == 0)
            {
                ui_controlling ui_form = new ui_controlling();
                ui_form.MdiParent = this;
                if (ui_form.WindowState == FormWindowState.Minimized)
                    ui_form.WindowState = FormWindowState.Normal;
                ui_form.Activate();
                ui_form.Show();
                ui_form.BringToFront();
            }
        }

        private void GFACI0102_Click(object sender, EventArgs e)
        {
            if (this.Controls.Find("ui_controlling_recursos", true).Count() == 0)
            {
                ui_controlling_recursos ui_form = new ui_controlling_recursos();
                ui_form.MdiParent = this;
                if (ui_form.WindowState == FormWindowState.Minimized)
                    ui_form.WindowState = FormWindowState.Normal;
                ui_form.Activate();
                ui_form.Show();
                ui_form.BringToFront();
            }
        }

        private void GFACI0103_Click(object sender, EventArgs e)
        {
            if (this.Controls.Find("ui_mqt_gastos", true).Count() == 0)
            {
                ui_mqt_gastos ui_form = new ui_mqt_gastos();
                ui_form.MdiParent = this;
                if (ui_form.WindowState == FormWindowState.Minimized)
                    ui_form.WindowState = FormWindowState.Normal;
                ui_form.Activate();
                ui_form.Show();
                ui_form.BringToFront();
            }
        }

        private void GFACI0104_Click(object sender, EventArgs e)
        {
            if (this.Controls.Find("ui_mqt_gastos_maestros", true).Count() == 0)
            {
                ui_mqt_gastos_maestros ui_form = new ui_mqt_gastos_maestros();
                ui_form.MdiParent = this;
                if (ui_form.WindowState == FormWindowState.Minimized)
                    ui_form.WindowState = FormWindowState.Normal;
                ui_form.Activate();
                ui_form.Show();
                ui_form.BringToFront();
            }
        }
        #endregion

        private void UTIL10_Click(object sender, EventArgs e)
        {
            if (this.Controls.Find("ui_balanza", true).Count() == 0)
            {
                ui_regplatoproducidos ui_form = new ui_regplatoproducidos();
                ui_form.MdiParent = this;
                if (ui_form.WindowState == FormWindowState.Minimized)
                    ui_form.WindowState = FormWindowState.Normal;
                ui_form.Activate();
                ui_form.Show();
                ui_form.BringToFront();
            }
        }

        private void PLATOS_Click(object sender, EventArgs e)
        {
            if (this.Controls.Find("ui_platoslista", true).Count() == 0)
            {
                ui_platoslista ui_platoslista = new ui_platoslista();
                ui_platoslista.MdiParent = this;
                if (ui_platoslista.WindowState == FormWindowState.Minimized)
                    ui_platoslista.WindowState = FormWindowState.Normal;
                ui_platoslista.Activate();
                ui_platoslista.Show();
                ui_platoslista.BringToFront();
            }
        }

      
    }

   
}