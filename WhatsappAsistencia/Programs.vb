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
        Dim cantidad As Integer
        Dim fecha As String = DateTime.Now.AddDays(-1).ToLongDateString()
        Dim conexion As SqlConnection
        Dim datosderegistros As String
        'Dim valordato As String

        Try



            'Dim connectionString As String = "Data Source=CHISULSQL1;Database=Asistencia;uid=ctcuser;pwd=ctcuser;"
            'Dim query1 As String = "SELECT  idlogin, COUNT(idperplan) cantidad FROM control WHERE fecha = CONVERT(DATE,GETDATE(),103) GROUP BY idlogin;"

            'Using connection As New SqlConnection(connectionString)
            '    Dim command As New SqlCommand(query1, connection)
            '    'command.Parameters.AddWithValue("@id", 1)

            '    connection.Open()
            '    Dim reader As SqlDataReader = command.ExecuteReader()

            '    Dim result As String = ""

            '    If reader.HasRows Then
            '        While reader.Read()
            '            Console.WriteLine(reader("idlogin") & ": " & reader("cantidad"))
            '            'valordato = Console.ReadLine()
            '            'result += reader("idlogin").ToString() & vbLf
            '        End While
            '    Else
            '        Console.WriteLine("No se han encontrado filas")
            '    End If
            'End Using


            ''Tecla(valordato)
            'Tecla("{ENTER}")


#Region "Garita Fabrica"

            Tiempo(10)

            Dim connString As String = "Data Source=CHISULSQL1;Database=Asistencia;uid=ctcuser;pwd=ctcuser;"
            Console.WriteLine("Extrayendo cantidad de registros de garita Fabrica...")
            query = "SELECT COUNT(idperplan) cantidad FROM control WHERE idlogin = 'GMTLPF' AND fecha = CONVERT(DATE,GETDATE()-1,103);"
            conexion = New SqlConnection()
            conexion.ConnectionString = connString
            conexion.Open()
            Dim cmd As SqlCommand = New SqlCommand(query, conexion)
            cantidad = CType(cmd.ExecuteScalar(), Int32)
            conexion.Close()

            If cantidad > 0 Then
                Dim valor As String = cantidad
                datosderegistros = "Fueron: " + valor + " registros 😊"
            Else
                datosderegistros = "Fueron: 0 registros, ['No se Sincronizo 😔']"
            End If

            Console.WriteLine("Activando Whatsapp...")

            'Call Shell("C:\Program Files\Google\Chrome\Application\chrome.exe", vbMaximizedFocus)
            Call Shell("C:\Program Files (x86)\Google\Chrome\Application\chrome.exe", vbMaximizedFocus)
            Tiempo(180)
            Tecla("{TAB}")
            Tiempo(3)
            Tecla("%{f4}")
            Tiempo(30)

            Call Shell("C:\Program Files (x86)\Google\Chrome\Application\chrome.exe", vbMaximizedFocus)

            Tiempo(60)
            Tecla("https://web.whatsapp.com/")
            Tiempo(15)
            Tecla("{ENTER}")
            Tiempo(35)

            ''>>>>>>>>>>>>>>>>

#Region "Abriendo Whastapp"
            Tecla("{TAB}")
            Tiempo(1)
            Tecla("{TAB}")
            Tiempo(1)
            Tecla("{TAB}")
            Tiempo(1)
            Tecla("{TAB}")
            Tiempo(1)
            Tecla("{TAB}")
            Tiempo(1)
            Tecla("FAB-SISASIS-Tareos de MO")
            Tiempo(7)
            Tecla("{DOWN}") 'down
            'Tiempo(1)
            'Tecla("{TAB}") 'eliminar

            Tiempo(1)
            Tecla("{UP}") ' up
            Tiempo(1)
            Tecla("+{TAB}")
            Tiempo(1)
            Tecla("+{TAB}")
            Tiempo(1)
            Tecla("+{TAB}")
            Tiempo(1)
            Tecla("+{TAB}")
            Tiempo(1)
            Tecla("+{TAB}")
            Tiempo(1)
            Tecla("+{TAB}")
            Tiempo(1)
            'Tecla("+{TAB}") 'eliminar
            'Tiempo(1)

            Tecla("Buen dia estimados, los registros de asistencias enviados del")
            Tiempo(3)
            Tecla("%{ENTER}")
            Tiempo(1)
            Tecla("%{ENTER}")
            Tiempo(1)
            Tecla("Dia: " + fecha)
            Tiempo(3)
            Tecla("%{ENTER}")
            Tiempo(1)
            Tecla("Garita: Patio Fabrica")
            Tiempo(2)
            Tecla("%{ENTER}")
            Tiempo(1)
            Tecla(datosderegistros)
            Tiempo(3)
            Tecla("%{ENTER}")
            Tiempo(1)
            Tecla("%{ENTER}")
            Tiempo(1)
            Tecla("Gracias, Atte. Sistemas Caña")
            Tiempo(2)
            'Tecla("{TAB}") 'eliminar
            'Tiempo(1)
            Tecla("{ENTER}")
#End Region
#End Region

#Region "Garita San Vicente"

            Tiempo(10)
            Dim connString1 As String = "Data Source=CHISULSQL1;Database=Asistencia;uid=ctcuser;pwd=ctcuser;"
            Console.WriteLine("Extrayendo cantidad de registros de garita San Vicente...")
            query = "SELECT COUNT(idperplan) cantidad FROM control WHERE idlogin = 'GSV3C' AND fecha = CONVERT(DATE,GETDATE()-1,103);"
            conexion = New SqlConnection()
            conexion.ConnectionString = connString1
            conexion.Open()
            Dim cmd1 As SqlCommand = New SqlCommand(query, conexion)
            cantidad = CType(cmd1.ExecuteScalar(), Int32)
            conexion.Close()

            If cantidad > 0 Then
                Dim valor As String = cantidad
                datosderegistros = "Fueron: " + valor + " registros 😊"
            Else
                datosderegistros = "Fueron: 0 registros, ['No se Sincronizo 😔']"
            End If

            Console.WriteLine("Recargando Whatsapp...")

            Tiempo(3)
            Tecla("{f5}")
            Tiempo(30)

            ''>>>>>>>>>>>>>>>>

#Region "Abriendo Whastapp"
            Tecla("{TAB}")
            Tiempo(1)
            Tecla("{TAB}")
            Tiempo(1)
            Tecla("{TAB}")
            Tiempo(1)
            Tecla("{TAB}")
            Tiempo(1)
            Tecla("{TAB}")
            Tiempo(1)
            Tecla("SV-SISASIS-Tareos de MO")
            Tiempo(7)
            Tecla("{DOWN}") 'down
            'Tiempo(1)
            'Tecla("{TAB}") 'eliminar

            Tiempo(1)
            Tecla("{UP}") ' up
            Tiempo(1)
            Tecla("+{TAB}")
            Tiempo(1)
            Tecla("+{TAB}")
            Tiempo(1)
            Tecla("+{TAB}")
            Tiempo(1)
            Tecla("+{TAB}")
            Tiempo(1)
            Tecla("+{TAB}")
            Tiempo(1)
            Tecla("+{TAB}")
            Tiempo(1)
            'Tecla("+{TAB}") 'eliminar
            'Tiempo(1)

            Tecla("Buen dia estimados, los registros de asistencias enviados del")
            Tiempo(3)
            Tecla("%{ENTER}")
            Tiempo(1)
            Tecla("%{ENTER}")
            Tiempo(1)
            Tecla("Dia: " + fecha)
            Tiempo(3)
            Tecla("%{ENTER}")
            Tiempo(1)
            Tecla("Garita: San Vicente")
            Tiempo(2)
            Tecla("%{ENTER}")
            Tiempo(1)
            Tecla(datosderegistros)
            Tiempo(3)
            Tecla("%{ENTER}")
            Tiempo(1)
            Tecla("%{ENTER}")
            Tiempo(1)
            Tecla("Gracias, Atte. Sistemas Caña")
            Tiempo(2)
            'Tecla("{TAB}") 'eliminar
            'Tiempo(1)
            Tecla("{ENTER}")

#End Region
#End Region

#Region "Garita Lobo"

            Tiempo(10)
            Dim connString2 As String = "Data Source=CHISULSQL1;Database=Asistencia;uid=ctcuser;pwd=ctcuser;"
            Console.WriteLine("Extrayendo cantidad de registros de garita lobo...")
            query = "SELECT COUNT(idperplan) cantidad FROM control WHERE idlogin = 'GLB01' AND fecha = CONVERT(DATE,GETDATE()-1,103);"
            conexion = New SqlConnection()
            conexion.ConnectionString = connString2
            conexion.Open()
            Dim cmd2 As SqlCommand = New SqlCommand(query, conexion)
            cantidad = CType(cmd2.ExecuteScalar(), Int32)
            conexion.Close()

            If cantidad > 0 Then
                Dim valor As String = cantidad
                datosderegistros = "Fueron: " + valor + " registros 😊"
            Else
                datosderegistros = "Fueron: 0 registros, ['No se Sincronizo 😔']"
            End If

            Console.WriteLine("Recargando Whatsapp...")

            Tiempo(3)
            Tecla("{f5}")
            Tiempo(30)

            ''>>>>>>>>>>>>>>>>

#Region "Abriendo Whastapp"
            Tecla("{TAB}")
            Tiempo(1)
            Tecla("{TAB}")
            Tiempo(1)
            Tecla("{TAB}")
            Tiempo(1)
            Tecla("{TAB}")
            Tiempo(1)
            Tecla("{TAB}")
            Tiempo(1)
            Tecla("LB-SISASIS-Tareos de MO")
            Tiempo(7)
            Tecla("{DOWN}") 'down
            'Tiempo(1)
            'Tecla("{TAB}") 'eliminar

            Tiempo(1)
            Tecla("{UP}") ' up
            Tiempo(1)
            Tecla("+{TAB}")
            Tiempo(1)
            Tecla("+{TAB}")
            Tiempo(1)
            Tecla("+{TAB}")
            Tiempo(1)
            Tecla("+{TAB}")
            Tiempo(1)
            Tecla("+{TAB}")
            Tiempo(1)
            Tecla("+{TAB}")
            Tiempo(1)
            'Tecla("+{TAB}") 'eliminar
            'Tiempo(1)

            Tecla("Buen dia estimados, los registros de asistencias enviados del")
            Tiempo(3)
            Tecla("%{ENTER}")
            Tiempo(1)
            Tecla("%{ENTER}")
            Tiempo(1)
            Tecla("Dia: " + fecha)
            Tiempo(3)
            Tecla("%{ENTER}")
            Tiempo(1)
            Tecla("Garita: Lobo")
            Tiempo(2)
            Tecla("%{ENTER}")
            Tiempo(1)
            Tecla(datosderegistros)
            Tiempo(3)
            Tecla("%{ENTER}")
            Tiempo(1)
            Tecla("%{ENTER}")
            Tiempo(1)
            Tecla("Gracias, Atte. Sistemas Caña")
            Tiempo(2)
            'Tecla("{TAB}") 'eliminar
            'Tiempo(1)
            Tecla("{ENTER}")

#End Region
#End Region

#Region "Garita Reservorio 1"

            Tiempo(10)
            Dim connString3 As String = "Data Source=CHISULSQL1;Database=Asistencia;uid=ctcuser;pwd=ctcuser;"
            Console.WriteLine("Extrayendo cantidad de registros de garita reservorio 1...")
            query = "SELECT COUNT(idperplan) cantidad FROM control WHERE idlogin = 'GMTLR1' AND fecha = CONVERT(DATE,GETDATE()-1,103);"
            conexion = New SqlConnection()
            conexion.ConnectionString = connString3
            conexion.Open()
            Dim cmd3 As SqlCommand = New SqlCommand(query, conexion)
            cantidad = CType(cmd3.ExecuteScalar(), Int32)
            conexion.Close()

            If cantidad > 0 Then
                Dim valor As String = cantidad
                datosderegistros = "Fueron: " + valor + " registros 😊"
            Else
                datosderegistros = "Fueron: 0 registros, ['No se Sincronizo 😔']"
            End If

            Console.WriteLine("Recargando Whatsapp...")

            Tiempo(3)
            Tecla("{f5}")
            Tiempo(30)

            ''>>>>>>>>>>>>>>>>

#Region "Abriendo Whastapp"
            Tecla("{TAB}")
            Tiempo(1)
            Tecla("{TAB}")
            Tiempo(1)
            Tecla("{TAB}")
            Tiempo(1)
            Tecla("{TAB}")
            Tiempo(1)
            Tecla("{TAB}")
            Tiempo(1)
            Tecla("ML-SISASIS-Tareos de MO")
            Tiempo(7)
            Tecla("{DOWN}") 'down
            'Tiempo(1)
            'Tecla("{TAB}") 'eliminar

            Tiempo(1)
            Tecla("{UP}") ' up
            Tiempo(1)
            Tecla("+{TAB}")
            Tiempo(1)
            Tecla("+{TAB}")
            Tiempo(1)
            Tecla("+{TAB}")
            Tiempo(1)
            Tecla("+{TAB}")
            Tiempo(1)
            Tecla("+{TAB}")
            Tiempo(1)
            Tecla("+{TAB}")
            Tiempo(1)
            'Tecla("+{TAB}") 'eliminar
            'Tiempo(1)

            Tecla("Buen dia estimados, los registros de asistencias enviados del")
            Tiempo(3)
            Tecla("%{ENTER}")
            Tiempo(1)
            Tecla("%{ENTER}")
            Tiempo(1)
            Tecla("Dia: " + fecha)
            Tiempo(3)
            Tecla("%{ENTER}")
            Tiempo(1)
            Tecla("Garita: Reservorio 1")
            Tiempo(2)
            Tecla("%{ENTER}")
            Tiempo(1)
            Tecla(datosderegistros)
            Tiempo(3)
            Tecla("%{ENTER}")
            Tiempo(1)
            Tecla("%{ENTER}")
            Tiempo(1)
            Tecla("Gracias, Atte. Sistemas Caña")
            Tiempo(2)
            'Tecla("{TAB}") 'eliminar
            'Tiempo(1)
            Tecla("{ENTER}")

#End Region
#End Region

#Region "Garita La Huaca"

            Tiempo(10)
            Dim connString4 As String = "Data Source=CHISULSQL1;Database=Asistencia;uid=ctcuser;pwd=ctcuser;"
            Console.WriteLine("Extrayendo cantidad de registros de garita la huaca...")
            query = "SELECT COUNT(idperplan) cantidad FROM control WHERE idlogin = 'GHUACA' AND fecha = CONVERT(DATE,GETDATE()-1,103);"
            conexion = New SqlConnection()
            conexion.ConnectionString = connString4
            conexion.Open()
            Dim cmd4 As SqlCommand = New SqlCommand(query, conexion)
            cantidad = CType(cmd4.ExecuteScalar(), Int32)
            conexion.Close()

            If cantidad > 0 Then
                Dim valor As String = cantidad
                datosderegistros = "Fueron: " + valor + " registros 😊"
            Else
                datosderegistros = "Fueron: 0 registros, ['No se Sincronizo 😔']"
            End If

            Console.WriteLine("Recargando Whatsapp...")

            Tiempo(3)
            Tecla("{f5}")
            Tiempo(30)

            ''>>>>>>>>>>>>>>>>

#Region "Abriendo Whastapp"
            Tecla("{TAB}")
            Tiempo(1)
            Tecla("{TAB}")
            Tiempo(1)
            Tecla("{TAB}")
            Tiempo(1)
            Tecla("{TAB}")
            Tiempo(1)
            Tecla("{TAB}")
            Tiempo(1)
            Tecla("LB-SISASIS-Tareos de MO")
            Tiempo(7)
            Tecla("{DOWN}") 'down
            'Tiempo(1)
            'Tecla("{TAB}") 'eliminar

            Tiempo(1)
            Tecla("{UP}") ' up
            Tiempo(1)
            Tecla("+{TAB}")
            Tiempo(1)
            Tecla("+{TAB}")
            Tiempo(1)
            Tecla("+{TAB}")
            Tiempo(1)
            Tecla("+{TAB}")
            Tiempo(1)
            Tecla("+{TAB}")
            Tiempo(1)
            Tecla("+{TAB}")
            Tiempo(1)
            'Tecla("+{TAB}") 'eliminar
            'Tiempo(1)

            Tecla("Buen dia estimados, los registros de asistencias enviados del")
            Tiempo(3)
            Tecla("%{ENTER}")
            Tiempo(1)
            Tecla("%{ENTER}")
            Tiempo(1)
            Tecla("Dia: " + fecha)
            Tiempo(3)
            Tecla("%{ENTER}")
            Tiempo(1)
            Tecla("Garita: La Huaca")
            Tiempo(2)
            Tecla("%{ENTER}")
            Tiempo(1)
            Tecla(datosderegistros)
            Tiempo(3)
            Tecla("%{ENTER}")
            Tiempo(1)
            Tecla("%{ENTER}")
            Tiempo(1)
            Tecla("Gracias, Atte. Sistemas Caña")
            Tiempo(2)
            'Tecla("{TAB}") 'eliminar
            'Tiempo(1)
            Tecla("{ENTER}")
            Tiempo(3)
            Tecla("%{f4}")
            Tiempo(5)
            Console.WriteLine("Proceso finalizado..!!")

#End Region
#End Region

#Region "Correo de Sincronizaciòn Correcta"
            '=== Correo de verificacion
            Dim textos As String
            textos = "Asistencias por Whatsapp enviadas correctamente"

            Dim fechaact As DateTime = DateTime.Now
            Dim msg As MailMessage = New MailMessage()
            msg.From = New MailAddress("Notificación Asistencias Whatsapp<notiwhatsapp@agricolachira.com.pe>")
            msg.[To].Add("Junior Alexander Hidalgo Socola<jhidalgos@agricolachira.com.pe>")
            msg.[To].Add("Jimmy Vasquez Castro<jvasquezc@agricolachira.com.pe>")
            msg.[To].Add("David Emmanuel Inga Checa<dingach@agricolachira.com.pe>")
            msg.Subject = "Correcto - Notificaciones de asistencias por Whatsapp "
            Dim htmlString As String = "<html>
                              <body style ='font-family: Calibri, Candara, Segoe, Segoe UI, Optima, Arial, sans-serif;'>
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

#Region "Error de envio"

        Catch ex As Exception

            Dim msg As MailMessage = New MailMessage()
            msg.From = New MailAddress("Notificación Asistencias Whatsapp<notiwhatsapp@agricolachira.com.pe>")
            msg.[To].Add("Junior Alexander Hidalgo Socola<jhidalgos@agricolachira.com.pe>")
            msg.[To].Add("Jimmy Vasquez Castro<jvasquezc@agricolachira.com.pe>")
            msg.[To].Add("David Emmanuel Inga Checa<dingach@agricolachira.com.pe>")
            msg.Subject = "Falla - Notificaciones de asistencias por Whatsapp"
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

#End Region

    End Sub

    Private p As Process

    Sub Tiempo(ByVal tiempo As Integer)
        Thread.Sleep(tiempo * 1000)
    End Sub

    Sub Tecla(ByVal tecla As String)
        SendKeys.SendWait(tecla)
    End Sub

End Module
