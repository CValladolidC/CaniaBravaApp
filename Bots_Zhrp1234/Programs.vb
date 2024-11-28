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
            Dim Usu As String = String.Empty, Pas As String = String.Empty, Ruta As String = String.Empty, Arch As String = String.Empty, Prog As String = String.Empty, Serv As String = String.Empty
            Dim fa As FileInfo = New FileInfo(AppDomain.CurrentDomain.BaseDirectory & "directorio.txt")

            If fa.Exists Then
                Dim opeIO As OpeIO = New OpeIO(AppDomain.CurrentDomain.BaseDirectory & "/directorio.txt")
                Usu = opeIO.ReadLineByNum(1)
                Pas = opeIO.ReadLineByNum(2)
                Ruta = opeIO.ReadLineByNum(3)
                Arch = opeIO.ReadLineByNum(4)
                Prog = opeIO.ReadLineByNum(5)
                Serv = opeIO.ReadLineByNum(6)

            End If

            Call Shell("C:\Program Files (x86)\SAP\FrontEnd\SAPgui\saplogon.exe", vbMaximizedFocus)
            Tiempo(70)
            AppActivate("SAP Logon 760")
            Tiempo(30)

            Tecla("{ENTER}")

            Tiempo(40)
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

            Console.WriteLine("Iniciamos Extracciòn de Informaciòn Maestro de Personal")
#Region "Extraciòn de informaciòn de SAP"
            Session.findById("wnd[0]").maximize
            Session.findById("wnd[0]/tbar[0]/okcd").text = "ZHRP1234"
            Tiempo(1)
            Session.findById("wnd[0]/tbar[0]/btn[0]").press
            Session.findById("wnd[0]/tbar[1]/btn[17]").press
            Tiempo(2)
            Session.findById("wnd[1]/usr/txtENAME-LOW").text = "ASSESSMENT"
            Session.findById("wnd[1]/usr/txtENAME-LOW").setFocus
            Session.findById("wnd[1]/usr/txtENAME-LOW").caretPosition = 2
            Tiempo(2)
            Session.findById("wnd[1]/tbar[0]/btn[8]").press
            Session.findById("wnd[0]/tbar[1]/btn[8]").press
#End Region

#Region "Guardado de Fichero Txt"
            Tiempo(17)
            Tecla("^+{F9}")
            Tiempo(6)
            Tecla("{TAB}")
            Tiempo(6)
            Tecla("{ENTER}")
            Tiempo(9)
            Tecla("+{TAB}")
            Tiempo(6)
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

            Session.findById("wnd[0]/tbar[0]/okcd").text = "/N"
            Session.findById("wnd[0]/tbar[0]/btn[0]").press


            Console.WriteLine("Leyendo datos SAP")
                Dim fab As FileInfo = New FileInfo(Ruta + Arch)
                data = New DataTable()

                If fab.Exists Then
                    Dim stringarry As String() = System.IO.File.ReadAllLines(Ruta + Arch, Encoding.[Default])

                    If stringarry.Length > 0 Then
                        Dim cc As Integer = 0
                        Dim arr_ = stringarry(7).Split("|")
                        Dim LColumns As List(Of String) = New List(Of String)()

                        For i As Integer = 0 To arr_.Length - 1

                            If arr_(i).Trim().Length > 0 Then
                                data.Columns.Add("Col" & cc, GetType(String))
                                cc += 1
                            Else
                                data.Columns.Add("Vacio" & i, GetType(String))
                                LColumns.Add("Vacio" & i)
                            End If
                        Next

                        arr_ = stringarry(8).Split("|"c)
                        Dim datOld As Integer = stringarry(7).Split("|").Length

                        For i As Integer = 0 To arr_.Length - 1

                            If arr_(i).Trim().Length > 0 Then
                                data.Columns.Add("Col" & cc, GetType(String))
                                cc += 1
                            Else
                                data.Columns.Add("Vacio" & (i + datOld), GetType(String))
                                LColumns.Add("Vacio" & (i + datOld))
                            End If
                        Next

                        data.AcceptChanges()
                        Dim dr As System.Data.DataRow = Nothing

                        For i As Integer = 10 To stringarry.Length - 1
                            stringarry(i) = stringarry(i).Replace("S/N|", "S/N")
                        Dim dat = stringarry(i).Split("|")

                        If (i Mod 2) = 0 Then
                                dr = data.NewRow()
                            End If

                        datOld = stringarry(i - 1).Split("|").Length

                        For y As Integer = 0 To dat.Length - 1

                                If (i Mod 2) = 0 Then
                                    dr(y) = dat(y).Trim()
                                Else

                                    If dr.ItemArray.Length > (y + datOld) Then
                                        dr(y + datOld) = dat(y).Trim()
                                    End If
                                End If
                            Next

                            If (i Mod 2) <> 0 Then
                                data.Rows.Add(dr)
                            End If
                        Next

                        data.AcceptChanges()
                        Console.WriteLine("Insertando datos SAP en SISASIS")

                        For Each item In LColumns
                            data.Columns.Remove(item)
                        Next

                        Dim cccc As Integer = 0
                        query = "IF EXISTS(SELECT * FROM sys.tables WHERE name='perplan_old') BEGIN DROP TABLE perplan_old; END;
                                SELECT idperplan,idcia,codaux,apepat,apemat,nombres,fecnac,tipdoc,nrodoc,telfijo,celular,rpm,estcivil,nacion,email,catlic,nrolic,tipotrab,idtipoper,nivedu,idlabper,seccion,regpen,cuspp
                                ,contrab,tippag,pering,sitesp,entfinrem,nroctarem,monrem,tipctarem,entfincts,nroctacts,moncts,tipctacts,tipvia,nomvia,nrovia,intvia,tipzona,nomzona,refzona,ubigeo,dscubigeo,sexo,ocurpts
                                ,afiliaeps,eps,discapa,sindica,situatrab,sctrsanin,sctrsaessa,sctrsaeps,sctrpennin,sctrpenonp,sctrpenseg,asigemplea,rucemp,estane,foto,idtipoplan,domicilia,esvida,fecregpen,regalterna,trabmax
                                ,trabnoc,quicat,renexo,asepen,apliconve,exoquicat,paisemi,disnac,deparvia,manzavia,lotevia,kmvia,block,etapa,reglab,ruc,0 alta_tregistro,baja_tregistro,fecharegistro,fechaupdate INTO perplan_old FROM perplan; 
                                DELETE FROM perplan;"

                        For Each item As DataRow In data.Rows
                            query += "INSERT INTO MaestroSAP VALUES ("

                            For i As Integer = 0 To item.ItemArray.Length - 1

                                If i = 2 Then
                                    query += "'" & Integer.Parse(item.ItemArray(i).ToString()) & "',"
                                Else

                                    If i = 28 OrElse i = 65 OrElse i = 66 OrElse i = 67 OrElse i = 68 OrElse i = 83 Then

                                        If item.ItemArray(i).ToString().Trim() <> String.Empty Then
                                            query += "'" & DateTime.Parse(item.ItemArray(i).ToString().Replace(".", "/")).ToString("yyyy-MM-dd") & "',"
                                        Else
                                            query += "'',"
                                        End If
                                    Else
                                        query += "'" & item.ItemArray(i).ToString().Replace("'", "") & "',"
                                    End If
                                End If
                            Next

                            query += ");" & vbCrLf
                            query = query.Replace(",);", ");")
                            cccc += 1
                            Console.WriteLine("Leyendo Informacion: " & cccc & " rows")
                        Next

                        query += "INSERT INTO perplan 
                                SELECT idperplan,idcia,codaux,apepat,apemat,nombres,fecnac,tipdoc,nrodoc,telfijo,celular,rpm,estcivil,nacion,email,catlic,nrolic,tipotrab,idtipoper,nivedu,idlabper,seccion,regpen,cuspp
                                ,contrab,tippag,pering,sitesp,entfinrem,nroctarem,monrem,tipctarem,entfincts,nroctacts,moncts,tipctacts,tipvia,nomvia,nrovia,intvia,tipzona,nomzona,refzona,ubigeo,dscubigeo,sexo,ocurpts
                                ,afiliaeps,eps,discapa,sindica,situatrab,sctrsanin,sctrsaessa,sctrsaeps,sctrpennin,sctrpenonp,sctrpenseg,asigemplea,rucemp,estane,foto,idtipoplan,domicilia,esvida,fecregpen,regalterna,trabmax
                                ,trabnoc,quicat,renexo,asepen,apliconve,exoquicat,paisemi,disnac,deparvia,manzavia,lotevia,kmvia,block,etapa,reglab,ruc,0,baja_tregistro,fecharegistro,fechaupdate
                                FROM perplan_old WHERE idperplan NOT IN (SELECT idperplan FROM perplan); "
                        query += "DROP TABLE perplan_old;"
                    Dim connString As String = "Data Source=" & Serv & ";Database=Asistencia;uid=ctcuser;pwd=ctcuser;"
                    Dim conexion As SqlConnection = New SqlConnection()
                        conexion.ConnectionString = connString
                        conexion.Open()
                        Dim myCommand As SqlCommand = New SqlCommand(query, conexion)
                    myCommand.CommandTimeout = 900
                    myCommand.ExecuteNonQuery()
                        myCommand.Dispose()
                        conexion.Close()
                    End If



                End If

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

                Dim fechaact As DateTime = DateTime.Now
                Dim msg As MailMessage = New MailMessage()
                msg.From = New MailAddress("Sistemas TI<sistemasti@agricolachira.com.pe>")
            msg.[To].Add("LD Sistemas Caña Brava<sistemascb@agricolachira.com.pe>")
            msg.[To].Add("Junior Alexander Hidalgo Socola<jhidalgos@agricolachira.com.pe>")
            msg.Subject = "Sincronizacion Correcta - Informaciòn de Personal - Zhrp1234"
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


            Dim msg As MailMessage = New MailMessage()
            msg.From = New MailAddress("Sistemas TI<sistemasti@agricolachira.com.pe>")
            msg.[To].Add("LD Sistemas Caña Brava<sistemascb@agricolachira.com.pe>")
            msg.[To].Add("Junior Alexander Hidalgo Socola<jhidalgos@agricolachira.com.pe>")
            msg.Subject = "Sincronizacion Fallida - Informaciòn de Personal - Zhrp1234"
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

