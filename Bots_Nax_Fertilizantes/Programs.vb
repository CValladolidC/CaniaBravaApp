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

    Sub Main()

        Dim query As String = String.Empty

        Try

            Dim Procesos As Process = New Process()
            Dim rutaarch As String = "D:\_BOTs\NAX_FERTILIZANTE\999_NAX_APLICACIONES.qvw"

            Procesos.Start(rutaarch)

            Tiempo(7)
            Tecla("^{r}")
            Tiempo(300)
            Tecla("^{s}")
            Tiempo(6)
            Tecla("%{f4}")
            Tiempo(1)

            Dim Procesosbcp As Process = New Process()
            Dim rutaarchi As String = "D:\_BOTs\NAX_FERTILIZANTE\bcp_Sql.bat"

            Procesosbcp.Start(rutaarchi)


#Region "Correo de Sincronizaciòn Correcta"
            '=== Correo de verificacion

            Dim fechaact As DateTime = DateTime.Now
            Dim msg As MailMessage = New MailMessage()
            msg.From = New MailAddress("Fertilización NAX<sistemastiNax@agricolachira.com.pe>")
            msg.[To].Add("Junior Alexander Hidalgo Socola<jhidalgos@agricolachira.com.pe>")
            msg.Subject = "Sincronizacion Correcta - Informaciòn de Fertilización NAX"
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



        Catch ex As Exception

            query = "Error al ingresar Informacion"
            Dim msg As MailMessage = New MailMessage()
            msg.From = New MailAddress("Fertilización NAX<sistemastiNax@agricolachira.com.pe>")
            msg.[To].Add("Junior Alexander Hidalgo Socola<jhidalgos@agricolachira.com.pe>")
            msg.Subject = "Sincronizacion Fallida - Informaciòn de Fertilización NAX"
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
