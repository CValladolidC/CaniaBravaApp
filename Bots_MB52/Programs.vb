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
            Dim Usu As String = String.Empty, Pas As String = String.Empty, Ruta As String = String.Empty, Arch As String = String.Empty, Prog As String = String.Empty, Serv As String = String.Empty, Arch2 As String = String.Empty
            Dim fa As FileInfo = New FileInfo(AppDomain.CurrentDomain.BaseDirectory & "directorio.txt")

            If fa.Exists Then
                Dim opeIO As OpeIO = New OpeIO(AppDomain.CurrentDomain.BaseDirectory & "/directorio.txt")
                Usu = opeIO.ReadLineByNum(1)
                Pas = opeIO.ReadLineByNum(2)
                Ruta = opeIO.ReadLineByNum(3)
                Arch = opeIO.ReadLineByNum(4)
                Arch2 = opeIO.ReadLineByNum(5)
                Prog = opeIO.ReadLineByNum(6)
                Serv = opeIO.ReadLineByNum(7)

            End If

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
            Session.findById("wnd[0]/usr/txtRSYST-BNAME").Text = Usu 'usuario
            Session.findById("wnd[0]/usr/pwdRSYST-BCODE").Text = Pas 'pass
            Session.findById("wnd[0]/usr/txtRSYST-LANGU").Text = "ES"         'idioma
            Session.findById("wnd[0]/tbar[0]/btn[0]").press

            Tiempo(10)

            Console.WriteLine("Iniciamos Extracciòn de Informaciòn Stock")

            ''>>>>>>>>>>>>>>>>

#Region "Extracciòn de data MB52"

            Session.findById("wnd[0]").maximize()
            Session.findById("wnd[0]/tbar[0]/okcd").Text = "MB52"
            Session.findById("wnd[0]/tbar[0]/btn[0]").press()
            Session.findById("wnd[0]/tbar[1]/btn[17]").press()
            Session.findById("wnd[1]/usr/txtENAME-LOW").Text = "JHIDALGOS"
            Session.findById("wnd[1]/usr/txtENAME-LOW").SetFocus()
            Session.findById("wnd[1]/usr/txtENAME-LOW").caretPosition = 1
            Session.findById("wnd[1]/tbar[0]/btn[8]").press()
            Session.findById("wnd[0]/tbar[1]/btn[8]").press()

#Region "Guardado de Fichero Txt"
            Tiempo(17)
            Tecla("^+{F9}")
            Tiempo(6)
            Tecla("{DOWN}")
            Tiempo(6)
            Tecla("{TAB}")
            Tiempo(6)
            Tecla("{ENTER}")
            Tiempo(20)
            Tecla("+{TAB}")
            Tiempo(9)
            Tecla(Ruta)
            Tiempo(9)
            Tecla("{TAB}")
            Tiempo(9)
            Tecla(Arch)
            Tiempo(9)
            Tecla("{TAB}")
            Tiempo(7)
            Tecla("{TAB}")
            Tiempo(7)
            Tecla("{TAB}")
            Tiempo(7)
            Tecla("{ENTER}")
            Tiempo(7)
            Tecla("+{TAB}")
            Tiempo(7)
            Tecla("{ENTER}")
            Tiempo(8)

#End Region

            Session.findById("wnd[0]/tbar[0]/okcd").Text = "/N"
            Session.findById("wnd[0]/tbar[0]/btn[0]").press()

#End Region

            ''>>>>>>>>>>>>>>>>   

#Region "Extracciòn de data MB52"

            Session.findById("wnd[0]").maximize()
            Session.findById("wnd[0]/tbar[0]/okcd").Text = "ZMM441"
            Session.findById("wnd[0]/tbar[0]/btn[0]").press()
            Session.findById("wnd[0]/usr/ctxtS_WERKS-LOW").Text = "3202"
            Session.findById("wnd[0]/usr/ctxtS_LGORT-LOW").Text = "*"
            Session.findById("wnd[0]/usr/ctxtS_MATNR-LOW").Text = "*"
            Session.findById("wnd[0]/usr/txtS_ESTA-LOW").Text = "*"
            Session.findById("wnd[0]/usr/txtS_PISO-LOW").Text = "*"
            Session.findById("wnd[0]/usr/txtS_GABE-LOW").Text = "*"
            Session.findById("wnd[0]/usr/txtS_GABE-LOW").SetFocus()
            Session.findById("wnd[0]/usr/txtS_GABE-LOW").caretPosition = 1
            Session.findById("wnd[0]/tbar[1]/btn[8]").press()
            Session.findById("wnd[0]/tbar[1]/btn[32]").press()
            Session.findById("wnd[1]/usr/tabsTS_LINES/tabpLI01/ssubSUB810:SAPLSKBH:0810/tblSAPLSKBHTC_WRITE_LIST").getAbsoluteRow(1).Selected = True
            Session.findById("wnd[1]/usr/tabsTS_LINES/tabpLI01/ssubSUB810:SAPLSKBH:0810/tblSAPLSKBHTC_WRITE_LIST").getAbsoluteRow(2).Selected = True
            Session.findById("wnd[1]/usr/tabsTS_LINES/tabpLI01/ssubSUB810:SAPLSKBH:0810/tblSAPLSKBHTC_WRITE_LIST").getAbsoluteRow(3).Selected = True
            Session.findById("wnd[1]/usr/tabsTS_LINES/tabpLI01/ssubSUB810:SAPLSKBH:0810/tblSAPLSKBHTC_WRITE_LIST").getAbsoluteRow(4).Selected = True
            Session.findById("wnd[1]/usr/tabsTS_LINES/tabpLI01/ssubSUB810:SAPLSKBH:0810/tblSAPLSKBHTC_WRITE_LIST").getAbsoluteRow(5).Selected = True
            Session.findById("wnd[1]/usr/tabsTS_LINES/tabpLI01/ssubSUB810:SAPLSKBH:0810/tblSAPLSKBHTC_WRITE_LIST").getAbsoluteRow(6).Selected = True
            Session.findById("wnd[1]/usr/tabsTS_LINES/tabpLI01/ssubSUB810:SAPLSKBH:0810/tblSAPLSKBHTC_WRITE_LIST").getAbsoluteRow(7).Selected = True
            Session.findById("wnd[1]/usr/tabsTS_LINES/tabpLI01/ssubSUB810:SAPLSKBH:0810/tblSAPLSKBHTC_WRITE_LIST").getAbsoluteRow(8).Selected = True
            Session.findById("wnd[1]/usr/tabsTS_LINES/tabpLI01/ssubSUB810:SAPLSKBH:0810/tblSAPLSKBHTC_WRITE_LIST/txtGT_WRITE_LIST-SELTEXT[0,8]").SetFocus()
            Session.findById("wnd[1]/usr/tabsTS_LINES/tabpLI01/ssubSUB810:SAPLSKBH:0810/tblSAPLSKBHTC_WRITE_LIST/txtGT_WRITE_LIST-SELTEXT[0,8]").caretPosition = 0
            Session.findById("wnd[1]/usr/btnAPP_FL_SING").press()
            Session.findById("wnd[1]/usr/tblSAPLSKBHTC_FIELD_LIST").getAbsoluteRow(2).Selected = True
            Session.findById("wnd[1]/usr/tblSAPLSKBHTC_FIELD_LIST/txtGT_FIELD_LIST-SELTEXT[0,2]").SetFocus()
            Session.findById("wnd[1]/usr/tblSAPLSKBHTC_FIELD_LIST/txtGT_FIELD_LIST-SELTEXT[0,2]").caretPosition = 0
            Session.findById("wnd[1]/usr/btnAPP_WL_SING").press()
            Session.findById("wnd[1]/usr/tblSAPLSKBHTC_FIELD_LIST").getAbsoluteRow(2).Selected = True
            Session.findById("wnd[1]/usr/tblSAPLSKBHTC_FIELD_LIST/txtGT_FIELD_LIST-SELTEXT[0,2]").SetFocus()
            Session.findById("wnd[1]/usr/tblSAPLSKBHTC_FIELD_LIST/txtGT_FIELD_LIST-SELTEXT[0,2]").caretPosition = 0
            Session.findById("wnd[1]/usr/btnAPP_WL_SING").press()
            Session.findById("wnd[1]/usr/tblSAPLSKBHTC_FIELD_LIST").getAbsoluteRow(0).Selected = True
            Session.findById("wnd[1]/usr/btnAPP_WL_SING").press()
            Session.findById("wnd[1]/usr/tblSAPLSKBHTC_FIELD_LIST").getAbsoluteRow(1).Selected = True
            Session.findById("wnd[1]/usr/tblSAPLSKBHTC_FIELD_LIST/txtGT_FIELD_LIST-SELTEXT[0,1]").SetFocus()
            Session.findById("wnd[1]/usr/tblSAPLSKBHTC_FIELD_LIST/txtGT_FIELD_LIST-SELTEXT[0,1]").caretPosition = 0
            Session.findById("wnd[1]/usr/btnAPP_WL_SING").press()
            Session.findById("wnd[1]/usr/tblSAPLSKBHTC_FIELD_LIST").getAbsoluteRow(7).Selected = True
            Session.findById("wnd[1]/usr/tblSAPLSKBHTC_FIELD_LIST/txtGT_FIELD_LIST-SELTEXT[0,7]").SetFocus()
            Session.findById("wnd[1]/usr/tblSAPLSKBHTC_FIELD_LIST/txtGT_FIELD_LIST-SELTEXT[0,7]").caretPosition = 0
            Session.findById("wnd[1]/usr/btnAPP_WL_SING").press()
            Session.findById("wnd[1]/tbar[0]/btn[0]").press()

#Region "Guardado de Fichero Txt"
            Tiempo(17)
            Tecla("^+{F9}")
            Tiempo(6)
            Tecla("{DOWN}")
            Tiempo(6)
            Tecla("{TAB}")
            Tiempo(6)
            Tecla("{ENTER}")
            Tiempo(40)
            Tecla("+{TAB}")
            Tiempo(9)
            Tecla(Ruta)
            Tiempo(9)
            Tecla("{TAB}")
            Tiempo(9)
            Tecla(Arch2)
            Tiempo(9)
            Tecla("{TAB}")
            Tiempo(7)
            Tecla("{TAB}")
            Tiempo(7)
            Tecla("{TAB}")
            Tiempo(7)
            Tecla("{ENTER}")
            Tiempo(20)
            Tecla("+{TAB}")
            Tiempo(7)
            Tecla("{ENTER}")
            Tiempo(10)

#End Region

            Session.findById("wnd[0]/tbar[0]/okcd").Text = "/N"
            Session.findById("wnd[0]/tbar[0]/btn[0]").press()

#End Region

            Tiempo(8)
            Session.findById("wnd[0]").close
            Session.findById("wnd[1]/usr/btnSPOP-OPTION1").press
            Tiempo(8)
            AppActivate("SAP Logon 760")
            Tiempo(8)
            Tecla("%{F4}")
            Tiempo(8)

            Console.WriteLine("Proceso finalizado..!!")

#Region "Correo de Sincronizaciòn Correcta"
            '=== Correo de verificacion
            Dim textos As String
            textos = "NotiActualizacionVB.net"

            Dim fechaact As DateTime = DateTime.Now
            Dim msg As MailMessage = New MailMessage()
            msg.From = New MailAddress("Sistemas TI<sistemasti@agricolachira.com.pe>")
            msg.[To].Add("Junior Alexander Hidalgo Socola<jhidalgos@agricolachira.com.pe>")
            msg.Subject = "NotiActualizacionVB.net"
            Dim htmlString As String = "<html>
                              <body style='font-family: Calibri, Candara, Segoe, Segoe UI, Optima, Arial, sans-serif;'>
                              <p></p>
                              <p>" & textos & "</p>
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

        Catch ex As Exception

            Dim msg As MailMessage = New MailMessage()
            msg.From = New MailAddress("Sistemas TI<sistemasti@agricolachira.com.pe>")
            msg.[To].Add("Junior Alexander Hidalgo Socola<jhidalgos@agricolachira.com.pe>")
            msg.Subject = "Sincronizacion SAP - Stock Critico"
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
