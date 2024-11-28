Imports System
Imports System.Collections.Generic
Imports System.Data
Imports System.Data.SqlClient
Imports System.Diagnostics
Imports System.IO
Imports System.Linq
Imports System.Net
Imports System.Net.Mail
Imports System.Text
Imports System.Threading
Imports System.Threading.Tasks
Imports System.Windows.Forms

Module Programs

    Dim SapGuiAuto
    Dim SetApp
    Dim Connection
    Dim Session

    Sub Main()

        Dim data As DataTable = New DataTable()
        Dim query As String = String.Empty

        Try
            Dim Usua As String = String.Empty, Pass As String = String.Empty, Ruta As String = String.Empty, Arc1 As String = String.Empty, Arc2 As String = String.Empty, Arc3 As String = String.Empty
            Dim fa As FileInfo = New FileInfo(AppDomain.CurrentDomain.BaseDirectory & "directorio.txt")

            If fa.Exists Then
                Dim opeIO As OpeIO = New OpeIO(AppDomain.CurrentDomain.BaseDirectory & "/directorio.txt")
                Usua = opeIO.ReadLineByNum(1)
                Pass = opeIO.ReadLineByNum(2)
                Ruta = opeIO.ReadLineByNum(3)
                Arc1 = opeIO.ReadLineByNum(4)
                Arc2 = opeIO.ReadLineByNum(5)
                Arc3 = opeIO.ReadLineByNum(6)


            End If

            If DateTime.Now.Day >= 1 Then
                Call Shell("C:\Program Files (x86)\SAP\FrontEnd\SAPgui\saplogon.exe", vbMaximizedFocus)
                Tiempo(5)
                AppActivate("SAP Logon 760")
                Tiempo(5)

                Tecla("{ENTER}")

                Tiempo(5)
                '= Variables para la conexión =====================

                SapGuiAuto = GetObject("SAPGUI")
                SetApp = SapGuiAuto.GetScriptingEngine
                Connection = SetApp.Children(0)
                Session = Connection.Children(0)

                '= Session SAP ====================================

                Session.findById("wnd[0]").maximize
                Session.findById("wnd[0]/usr/txtRSYST-MANDT").Text = "300"        'mandante
                Session.findById("wnd[0]/usr/txtRSYST-BNAME").Text = Usua 'usuario
                Session.findById("wnd[0]/usr/pwdRSYST-BCODE").Text = Pass 'pass
                Session.findById("wnd[0]/usr/txtRSYST-LANGU").Text = "ES"         'idioma
                Session.findById("wnd[0]/tbar[0]/btn[0]").press

                Tiempo(10)

#Region "Descargando Información de Materiales Datos Centro"

                Console.WriteLine("Iniciamos Extracciòn de Información Materiales Datos Centro")

                Session.findById("wnd[0]").maximize
                Session.findById("wnd[0]/tbar[0]/okcd").text = "ZMM420"
                Session.findById("wnd[0]/tbar[0]/btn[0]").press
                Session.findById("wnd[0]/tbar[1]/btn[17]").press
                Session.findById("wnd[1]/usr/txtENAME-LOW").text = "JHIDALGOS"
                Session.findById("wnd[1]/usr/txtENAME-LOW").setFocus
                Session.findById("wnd[1]/usr/txtENAME-LOW").caretPosition = 9
                Session.findById("wnd[1]/tbar[0]/btn[8]").press
                Session.findById("wnd[1]/usr/cntlALV_CONTAINER_1/shellcont/shell").currentCellColumn = "TEXT"
                Session.findById("wnd[1]/usr/cntlALV_CONTAINER_1/shellcont/shell").selectedRows = "0"
                Session.findById("wnd[1]/tbar[0]/btn[2]").press
                Session.findById("wnd[0]/usr/ctxtS_MATNR-LOW").text = ""
                Session.findById("wnd[0]/usr/ctxtS_LGORT-LOW").text = ""
                Session.findById("wnd[0]/usr/txtS_EAN11-LOW").text = ""
                Session.findById("wnd[0]/usr/ctxtS_MATKL-LOW").text = ""
                Session.findById("wnd[0]/usr/ctxtS_LVORM-LOW").text = ""
                Session.findById("wnd[0]/usr/ctxtS_MEINS-LOW").text = ""
                Session.findById("wnd[0]/usr/ctxtS_BKLAS-LOW").text = ""
                Session.findById("wnd[0]/usr/ctxtS_VKORG-LOW").text = ""
                Session.findById("wnd[0]/usr/ctxtS_VTWEG-LOW").text = ""
                Session.findById("wnd[0]/tbar[1]/btn[8]").press
                Tiempo(5)
                Session.findById("wnd[0]/mbar/menu[4]/menu[0]/menu[1]").select
                Tiempo(3)
                Session.findById("wnd[1]/usr/ssubD0500_SUBSCREEN:SAPLSLVC_DIALOG:0501/cmbG51_USPEC_LBOX").key = "X"
                Tiempo(3)
                Session.findById("wnd[1]/usr/ssubD0500_SUBSCREEN:SAPLSLVC_DIALOG:0501/cntlG51_CONTAINER/shellcont/shell").currentCellColumn = "TEXT"
                Tiempo(3)
                Session.findById("wnd[1]/usr/ssubD0500_SUBSCREEN:SAPLSLVC_DIALOG:0501/cntlG51_CONTAINER/shellcont/shell").selectedRows = "0"
                Tiempo(3)
                Session.findById("wnd[1]/usr/ssubD0500_SUBSCREEN:SAPLSLVC_DIALOG:0501/cntlG51_CONTAINER/shellcont/shell").clickCurrentCell

#Region "Guardado de Fichero Txt"
                Tiempo(17)
                Tecla("^+{F9}")
                Tiempo(100)
                Tecla("{TAB}")
                Tiempo(6)
                Tecla("{ENTER}")
                Tiempo(30)
                Tecla("+{TAB}")
                Tiempo(9)
                Tecla(Ruta)
                Tiempo(9)
                Tecla("{TAB}")
                Tiempo(9)
                Tecla(Arc1)
                Tiempo(9)
                Tecla("{TAB}")
                Tiempo(7)
                Tecla("{TAB}")
                Tiempo(7)
                Tecla("{ENTER}")
                Tiempo(120)
                Tecla("+{TAB}")
                Tiempo(7)
                Tecla("{ENTER}")
                Tiempo(30)
#End Region

                Session.findById("wnd[0]/tbar[0]/okcd").text = "/N"
                Session.findById("wnd[0]/tbar[0]/btn[0]").press


#End Region

#Region "Descargando Información de Materiales Datos Basicos"

                Console.WriteLine("Iniciamos Extracciòn de Información Materiales Datos Basicos")

                Session.findById("wnd[0]").maximize
                Session.findById("wnd[0]/tbar[0]/okcd").text = "ZMM420"
                Session.findById("wnd[0]/tbar[0]/btn[0]").press
                Session.findById("wnd[0]/tbar[1]/btn[17]").press
                Session.findById("wnd[1]/usr/txtENAME-LOW").text = "JHIDALGOS"
                Session.findById("wnd[1]/usr/txtENAME-LOW").setFocus
                Session.findById("wnd[1]/usr/txtENAME-LOW").caretPosition = 9
                Session.findById("wnd[1]/tbar[0]/btn[8]").press
                Session.findById("wnd[1]/usr/cntlALV_CONTAINER_1/shellcont/shell").currentCellColumn = "TEXT"
                Session.findById("wnd[1]/usr/cntlALV_CONTAINER_1/shellcont/shell").selectedRows = "1"
                Session.findById("wnd[1]/tbar[0]/btn[2]").press
                Session.findById("wnd[0]/usr/ctxtS_MATNR-LOW").text = ""
                Session.findById("wnd[0]/usr/ctxtS_LGORT-LOW").text = ""
                Session.findById("wnd[0]/usr/txtS_EAN11-LOW").text = ""
                Session.findById("wnd[0]/usr/ctxtS_MATKL-LOW").text = ""
                Session.findById("wnd[0]/usr/ctxtS_LVORM-LOW").text = ""
                Session.findById("wnd[0]/usr/ctxtS_MEINS-LOW").text = ""
                Session.findById("wnd[0]/usr/ctxtS_BKLAS-LOW").text = ""
                Session.findById("wnd[0]/usr/ctxtS_VKORG-LOW").text = ""
                Session.findById("wnd[0]/usr/ctxtS_VTWEG-LOW").text = ""

                Session.findById("wnd[0]/usr/btnADD").press
                Session.findById("wnd[1]/shellcont/shell").expandNode("          1")
                Session.findById("wnd[1]/shellcont/shell").selectNode("         10")
                Session.findById("wnd[1]/shellcont/shell").topNode = "          1"
                Session.findById("wnd[1]/shellcont/shell").doubleClickNode("         10")
                Session.findById("wnd[1]/usr/ssub%_SUBSCREEN_FREESEL:SAPLSSEL:1105/ctxt%%DYN001-LOW").text = "ERSA"
                Session.findById("wnd[1]/usr/ssub%_SUBSCREEN_FREESEL:SAPLSSEL:1105/ctxt%%DYN001-LOW").setFocus
                Session.findById("wnd[1]/usr/ssub%_SUBSCREEN_FREESEL:SAPLSSEL:1105/ctxt%%DYN001-LOW").caretPosition = 4
                Session.findById("wnd[1]/tbar[0]/btn[11]").press


                Session.findById("wnd[0]/tbar[1]/btn[8]").press
                Session.findById("wnd[1]/tbar[0]/btn[0]").press
                Tiempo(5)
                Session.findById("wnd[0]/mbar/menu[4]/menu[0]/menu[1]").select
                Tiempo(3)
                Session.findById("wnd[1]/usr/ssubD0500_SUBSCREEN:SAPLSLVC_DIALOG:0501/cmbG51_USPEC_LBOX").key = "X"
                Tiempo(3)
                Session.findById("wnd[1]/usr/ssubD0500_SUBSCREEN:SAPLSLVC_DIALOG:0501/cntlG51_CONTAINER/shellcont/shell").currentCellColumn = "TEXT"
                Tiempo(3)
                Session.findById("wnd[1]/usr/ssubD0500_SUBSCREEN:SAPLSLVC_DIALOG:0501/cntlG51_CONTAINER/shellcont/shell").selectedRows = "0"
                Tiempo(3)
                Session.findById("wnd[1]/usr/ssubD0500_SUBSCREEN:SAPLSLVC_DIALOG:0501/cntlG51_CONTAINER/shellcont/shell").clickCurrentCell

#Region "Guardado de Fichero Txt"
                Tiempo(17)
                Tecla("^+{F9}")
                Tiempo(180)
                Tecla("{TAB}")
                Tiempo(6)
                Tecla("{ENTER}")
                Tiempo(30)
                Tecla("+{TAB}")
                Tiempo(9)
                Tecla(Ruta)
                Tiempo(9)
                Tecla("{TAB}")
                Tiempo(9)
                Tecla(Arc2)
                Tiempo(9)
                Tecla("{TAB}")
                Tiempo(7)
                Tecla("{TAB}")
                Tiempo(7)
                Tecla("{ENTER}")
                Tiempo(180)
                Tecla("+{TAB}")
                Tiempo(7)
                Tecla("{ENTER}")
                Tiempo(30)
#End Region

                Session.findById("wnd[0]/tbar[0]/okcd").text = "/N"
                Session.findById("wnd[0]/tbar[0]/btn[0]").press


#End Region

#Region "Descargando Información de Texto Pedido de Compras"

                Console.WriteLine("Iniciamos Extracciòn de Información de Texto Pedido de Compras")

                Session.findById("wnd[0]").maximize
                Session.findById("wnd[0]/tbar[0]/okcd").text = "ZMM420"
                Session.findById("wnd[0]/tbar[0]/btn[0]").press
                Session.findById("wnd[0]/tbar[1]/btn[17]").press
                Session.findById("wnd[1]/usr/txtENAME-LOW").text = "JHIDALGOS"
                Session.findById("wnd[1]/tbar[0]/btn[8]").press
                Session.findById("wnd[0]/usr/ctxtS_MATNR-LOW").text = ""
                Session.findById("wnd[0]/usr/ctxtS_LGORT-LOW").text = ""
                Session.findById("wnd[0]/usr/txtS_EAN11-LOW").text = ""
                Session.findById("wnd[0]/usr/ctxtS_MATKL-LOW").text = ""
                Session.findById("wnd[0]/usr/ctxtS_LVORM-LOW").text = ""
                Session.findById("wnd[0]/usr/ctxtS_MEINS-LOW").text = ""
                Session.findById("wnd[0]/usr/ctxtS_BKLAS-LOW").text = ""
                Session.findById("wnd[0]/usr/ctxtS_VKORG-LOW").text = ""
                Session.findById("wnd[0]/usr/ctxtS_VTWEG-LOW").text = ""
                Session.findById("wnd[0]/tbar[1]/btn[8]").press
                Tiempo(5)
                Session.findById("wnd[0]/mbar/menu[4]/menu[0]/menu[1]").select
                Tiempo(3)
                Session.findById("wnd[1]/usr/ssubD0500_SUBSCREEN:SAPLSLVC_DIALOG:0501/cmbG51_USPEC_LBOX").key = "X"
                Tiempo(3)
                Session.findById("wnd[1]/usr/ssubD0500_SUBSCREEN:SAPLSLVC_DIALOG:0501/cntlG51_CONTAINER/shellcont/shell").currentCellColumn = "TEXT"
                Tiempo(3)
                Session.findById("wnd[1]/usr/ssubD0500_SUBSCREEN:SAPLSLVC_DIALOG:0501/cntlG51_CONTAINER/shellcont/shell").selectedRows = "0"
                Tiempo(3)
                Session.findById("wnd[1]/usr/ssubD0500_SUBSCREEN:SAPLSLVC_DIALOG:0501/cntlG51_CONTAINER/shellcont/shell").clickCurrentCell

#Region "Guardado de Fichero Txt"
                Tiempo(17)
                Tecla("^+{F9}")
                Tiempo(180)
                Tecla("{TAB}")
                Tiempo(6)
                Tecla("{ENTER}")
                Tiempo(30)
                Tecla("+{TAB}")
                Tiempo(9)
                Tecla(Ruta)
                Tiempo(9)
                Tecla("{TAB}")
                Tiempo(9)
                Tecla(Arc2)
                Tiempo(9)
                Tecla("{TAB}")
                Tiempo(7)
                Tecla("{TAB}")
                Tiempo(7)
                Tecla("{ENTER}")
                Tiempo(180)
                Tecla("+{TAB}")
                Tiempo(7)
                Tecla("{ENTER}")
                Tiempo(30)
#End Region

                Session.findById("wnd[0]/tbar[0]/okcd").text = "/N"
                Session.findById("wnd[0]/tbar[0]/btn[0]").press
                Tiempo(8)
                Session.findById("wnd[0]").close
                Session.findById("wnd[1]/usr/btnSPOP-OPTION1").press


#End Region

                Tiempo(8)
                AppActivate("SAP Logon 760")
                Tiempo(8)
                Tecla("%{F4}")
                Tiempo(8)

                Console.WriteLine("Proceso finalizado..!!")

#Region "Correo de Sincronizaciòn Correcta"
                '=== Correo de verificacion

                Dim fechaact As DateTime = DateTime.Now
                Dim msg As MailMessage = New MailMessage()
                msg.From = New MailAddress("Sistemas TI<sistemasti@agricolachira.com.pe>")
                msg.[To].Add("Junior Alexander Hidalgo Socola<jhidalgos@agricolachira.com.pe>")
                msg.Subject = "Sincronizacion Correcta - Información Catalogo de Materiales"
                Dim htmlString As String = "<html>
                              <body style='font-family: Calibri, Candara, Segoe, Segoe UI, Optima, Arial, sans-serif;'>
                              <p></p>
                              <p>" & fechaact & "</p>
                              <p></p>
                              </body>
                              </html>"
                msg.Body = htmlString
                msg.IsBodyHtml = True
                Dim smt As SmtpClient = New SmtpClient()
                smt.Host = "10.72.1.71"
                Dim ntcd As NetworkCredential = New NetworkCredential()
                smt.Port = 25
                smt.Credentials = ntcd
                smt.Send(msg)
#End Region

            End If

        Catch ex As Exception


            Dim msg As MailMessage = New MailMessage()
            msg.From = New MailAddress("Sistemas TI<sistemasti@agricolachira.com.pe>")
            msg.[To].Add("Junior Alexander Hidalgo Socola<jhidalgos@agricolachira.com.pe>")
            msg.Subject = "Sincronizacion Fallida - Información Catalogo de Materiales"
            Dim htmlString As String = "<html>
                      <body style='font-family: Calibri, Candara, Segoe, Segoe UI, Optima, Arial, sans-serif;'>
                      <p></p>
                      <p>" & ex.Message & "</p>
                      <p></p>
                      <p>" + query & "</p>
                      <p></p>
                      </body>
                      </html>"
            msg.Body = htmlString
            msg.IsBodyHtml = True
            Dim smt As SmtpClient = New SmtpClient()
            smt.Host = "10.72.1.71"
            Dim ntcd As NetworkCredential = New NetworkCredential()
            smt.Port = 25
            smt.Credentials = ntcd
            smt.Send(msg)


        End Try

    End Sub


    Private p As Process

    Sub Tiempo(ByVal tiempo As Integer)
        Thread.Sleep(tiempo * 1000)
    End Sub

    Sub Tecla(ByVal tecla As String)
        SendKeys.SendWait(tecla)
    End Sub

End Module
