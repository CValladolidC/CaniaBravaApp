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
            Dim conexion As SqlConnection
            Dim myCommand As SqlCommand
            Dim Usua As String = String.Empty, Pass As String = String.Empty, Agri1 As String = String.Empty, Agri2 As String = String.Empty, Sucr1 As String = String.Empty, Sucr2 As String = String.Empty, Bio1 As String = String.Empty, Bio2 As String = String.Empty, prog As String = String.Empty, serv As String = String.Empty, ruta As String = String.Empty, mes As String = String.Empty, anio As String = String.Empty, mesdetalle As String = String.Empty
            Dim fa As FileInfo = New FileInfo(AppDomain.CurrentDomain.BaseDirectory & "directorio.txt")

            If fa.Exists Then
                Dim opeIO As OpeIO = New OpeIO(AppDomain.CurrentDomain.BaseDirectory & "/directorio.txt")
                Usua = opeIO.ReadLineByNum(1)
                Pass = opeIO.ReadLineByNum(2)
                prog = opeIO.ReadLineByNum(3)
                serv = opeIO.ReadLineByNum(4)
                ruta = opeIO.ReadLineByNum(5)
                Agri1 = opeIO.ReadLineByNum(6)
                Agri2 = opeIO.ReadLineByNum(7)
                Sucr1 = opeIO.ReadLineByNum(8)
                Sucr2 = opeIO.ReadLineByNum(9)
                Bio1 = opeIO.ReadLineByNum(10)
                Bio2 = opeIO.ReadLineByNum(11)
                mes = opeIO.ReadLineByNum(12)
                anio = opeIO.ReadLineByNum(13)
                mesdetalle = opeIO.ReadLineByNum(14)

            End If
            Dim connString As String = "Data Source=" & serv & ";Database=PlanMo_New;uid=ctcuser;pwd=ctcuser;"

            If DateTime.Now.Day >= 6 Then
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

                Dim Monthe As DateTime = DateTime.Now
                Dim thisMonth As Integer = Month(Monthe) - 1
                Dim thisYear As Integer = Year(Monthe)
                Dim mesdet As String = String.Empty

#Region "Si el Mes Actual es Mayor a enero para descargar data del año actual"
                If DateTime.Now.Day >= 6 And DateTime.Now.Month > 1 Then

#Region "Meses"
                    If thisMonth = 1 Then
                        mesdet = "Ene"
                    End If
                    If thisMonth = 2 Then
                        mesdet = "Feb"
                    End If
                    If thisMonth = 3 Then
                        mesdet = "Mar"
                    End If
                    If thisMonth = 4 Then
                        mesdet = "Abr"
                    End If
                    If thisMonth = 5 Then
                        mesdet = "May"
                    End If
                    If thisMonth = 6 Then
                        mesdet = "Jun"
                    End If
                    If thisMonth = 7 Then
                        mesdet = "Jul"
                    End If
                    If thisMonth = 8 Then
                        mesdet = "Ago"
                    End If
                    If thisMonth = 9 Then
                        mesdet = "Set"
                    End If
                    If thisMonth = 10 Then
                        mesdet = "Oct"
                    End If
                    If thisMonth = 11 Then
                        mesdet = "Nov"
                    End If
                    If thisMonth = 12 Then
                        mesdet = "Dic"
                    End If
#End Region

#Region "Descarga de Informaciòn Gast. Operativo MO - Agricola"

                    Console.WriteLine("Iniciamos Extracciòn de Informaciòn Gast. Operativo MO")

                    Session.findById("wnd[0]").maximize
                    Session.findById("wnd[0]/tbar[0]/okcd").text = "GR55"
                    Session.findById("wnd[0]/tbar[0]/btn[0]").press
                    Session.findById("wnd[0]/usr/ctxtRGRWJ-JOB").text = "AC21"
                    Session.findById("wnd[0]/usr/ctxtRGRWJ-JOB").caretPosition = 2
                    Session.findById("wnd[0]/tbar[0]/btn[0]").press
                    Session.findById("wnd[0]/tbar[1]/btn[8]").press
                    Session.findById("wnd[0]/usr/ctxt$0BRYEAR").text = thisYear '"2022"
                    Session.findById("wnd[0]/usr/ctxt$GR00004").text = thisMonth '"1"
                    Session.findById("wnd[0]/usr/ctxt$GR00004").setFocus
                    Session.findById("wnd[0]/usr/ctxt$GR00004").caretPosition = 3
                    Session.findById("wnd[0]/tbar[1]/btn[8]").press
                    Session.findById("wnd[0]/mbar/menu[2]/menu[0]").select
                    Session.findById("wnd[1]/tbar[0]/btn[9]").press
                    Session.findById("wnd[2]/usr/txtRSYSF-STRING").text = "G. Operat."
                    Session.findById("wnd[2]/usr/txtRSYSF-STRING").caretPosition = 6
                    Session.findById("wnd[2]/tbar[0]/btn[0]").press
                    Session.findById("wnd[3]/usr/lbl[2,2]").setFocus
                    Session.findById("wnd[3]/usr/lbl[2,2]").caretPosition = 0
                    Session.findById("wnd[3]").sendVKey(2)
                    Session.findById("wnd[1]/tbar[0]/btn[0]").press
                    Session.findById("wnd[0]/tbar[1]/btn[32]").press
                    Session.findById("wnd[1]/tbar[0]/btn[9]").press
                    Session.findById("wnd[2]/usr/txtRSYSF-STRING").text = mesdet '"TOTAL"
                    Session.findById("wnd[2]/usr/txtRSYSF-STRING").caretPosition = 5
                    Session.findById("wnd[2]/tbar[0]/btn[0]").press
                    Session.findById("wnd[3]/usr/lbl[2,2]").setFocus
                    Session.findById("wnd[3]/usr/lbl[2,2]").caretPosition = 2
                    Session.findById("wnd[3]").sendVKey(2)
                    Session.findById("wnd[1]/tbar[0]/btn[0]").press
                    Session.findById("wnd[0]/mbar/menu[2]/menu[6]").select
                    Session.findById("wnd[1]/usr/txtRSYSF-STRING").text = "Salario"
                    Session.findById("wnd[1]/usr/txtRSYSF-STRING").caretPosition = 6
                    Session.findById("wnd[1]/tbar[0]/btn[0]").press
                    Session.findById("wnd[2]/usr/lbl[8,2]").setFocus
                    Session.findById("wnd[2]/usr/lbl[8,2]").caretPosition = 2
                    Session.findById("wnd[2]").sendVKey(2)
                    Session.findById("wnd[0]/usr/lbl[50,8]").setFocus
                    Session.findById("wnd[0]/usr/lbl[50,8]").caretPosition = 4
                    Session.findById("wnd[0]").sendVKey(2)
                    Session.findById("wnd[1]/usr/lbl[1,1]").caretPosition = 2
                    Session.findById("wnd[1]/tbar[0]/btn[2]").press
                    Tiempo(5)
                    Session.findById("wnd[0]/mbar/menu[4]/menu[2]/menu[1]").select
                    Session.findById("wnd[1]/usr/ssubD0500_SUBSCREEN:SAPLSLVC_DIALOG:0501/cmbG51_USPEC_LBOX").setFocus
                    Tiempo(4)
                    Session.findById("wnd[1]/usr/ssubD0500_SUBSCREEN:SAPLSLVC_DIALOG:0501/cmbG51_USPEC_LBOX").key = "X"
                    Session.findById("wnd[1]/usr/ssubD0500_SUBSCREEN:SAPLSLVC_DIALOG:0501/cntlG51_CONTAINER/shellcont/shell").currentCellRow = 1
                    Tiempo(4)
                    Session.findById("wnd[1]/usr/ssubD0500_SUBSCREEN:SAPLSLVC_DIALOG:0501/cntlG51_CONTAINER/shellcont/shell").selectedRows = "1"
                    Session.findById("wnd[1]/usr/ssubD0500_SUBSCREEN:SAPLSLVC_DIALOG:0501/cntlG51_CONTAINER/shellcont/shell").clickCurrentCell

#Region "Guardado de Fichero Txt"
                    Tiempo(17)
                    Tecla("{F9}")
                    Tiempo(30)
                    Tecla("{TAB}")
                    Tiempo(6)
                    Tecla("{ENTER}")
                    Tiempo(30)
                    Tecla("+{TAB}")
                    Tiempo(9)
                    Tecla(ruta)
                    Tiempo(9)
                    Tecla("{TAB}")
                    Tiempo(9)
                    Tecla(Agri1)
                    Tiempo(9)
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

                    Session.findById("wnd[0]/tbar[0]/btn[3]").press

                    Console.WriteLine("Iniciamos Extracciòn Mano de Obra Operativa Agricola")
                    Dim fab1 As FileInfo = New FileInfo(ruta + Agri1)
                    data = New DataTable()

                    If fab1.Exists Then
                        Dim stringarry As String() = System.IO.File.ReadAllLines(ruta + Agri1, Encoding.[Default])

                        If stringarry.Length > 0 Then
                            Dim cc As Integer = 0
                            Dim arr_ = stringarry(9).Substring(1, stringarry(9).Length - 2).Split("|")
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

                            data.AcceptChanges()

                            Dim dr As System.Data.DataRow = Nothing

                            For i As Integer = 11 To stringarry.Length - 2
                                stringarry(i) = stringarry(i).Replace("S/N|", "S/N")
                                Dim dat = stringarry(i).Substring(1, stringarry(i).Length - 2).Split("|")
                                dr = data.NewRow()

                                For y As Integer = 0 To dat.Length - 1
                                    dr(y) = dat(y).Trim()
                                Next

                                data.Rows.Add(dr)
                            Next

                            data.AcceptChanges()

                            query = "DELETE FROM PlanMo_New.dbo.VENTAS_GR55_MO_GASTO Where Tipo = 'AGR1' AND Periodo = MONTH(GETDATE()) - 1 AND Anio = YEAR(GETDATE());" & vbCrLf

                            Dim cccc As Integer = 0

                            For Each item As DataRow In data.Rows

                                If item.ItemArray(0).ToString() <> String.Empty Then
                                    query += "INSERT INTO PlanMo_New.dbo.VENTAS_GR55_MO_GASTO VALUES ('AGR1',"

                                    For i As Integer = 0 To item.ItemArray.Length - 1

                                        If i = 1 OrElse i = 2 OrElse i = 13 Then

                                            If item.ItemArray(i).ToString().Trim() <> String.Empty Then
                                                query += "'" & DateTime.Parse(item.ItemArray(i).ToString().Replace(".", "/")).ToString("yyyy-MM-dd") & "',"
                                            Else
                                                query += "'',"
                                            End If

                                        Else

                                            If i = 9 OrElse i = 11 Then

                                                If item.ItemArray(i).ToString().Trim() <> String.Empty Then
                                                    Dim val As String = item.ItemArray(i).ToString().Replace(",", "")
                                                    Dim dat As String = If(val.Contains("-"), val.Substring(0, val.Length - 1), "-" & val) * -1
                                                    query += "'" & dat & "',"
                                                Else
                                                    query += "'0',"
                                                End If
                                            Else

                                                If item.ItemArray(i).ToString().Trim() <> String.Empty Then
                                                    query += "'" & item.ItemArray(i).ToString() & "',"
                                                Else
                                                    query += "'0',"
                                                End If
                                            End If
                                        End If
                                    Next

                                    query += ");" & vbCrLf
                                End If

                                query = query.Replace(",);", ");")
                                cccc += 1
                                Console.WriteLine("Leyendo Informacion: " & cccc & " rows")
                            Next

                            conexion = New SqlConnection()
                            conexion.ConnectionString = connString
                            conexion.Open()
                            myCommand = New SqlCommand(query, conexion)
                            myCommand.CommandTimeout = 3000
                            myCommand.ExecuteNonQuery()
                            myCommand.Dispose()
                            conexion.Close()

                        End If

                    End If

#End Region

#Region "Descarga de Informaciòn Gast. Acumulado MO - Agricola"

                    Console.WriteLine("Iniciamos Extracciòn de Informaciòn Gast. Acumulado MO")


                    Session.findById("wnd[0]/mbar/menu[2]/menu[0]").select
                    Session.findById("wnd[1]/tbar[0]/btn[9]").press
                    Session.findById("wnd[2]/usr/txtRSYSF-STRING").text = "GRUP.CHIRA G.A"
                    Session.findById("wnd[2]/usr/txtRSYSF-STRING").caretPosition = 6
                    Session.findById("wnd[2]/tbar[0]/btn[0]").press
                    Session.findById("wnd[3]/usr/lbl[2,2]").setFocus
                    Session.findById("wnd[3]/usr/lbl[2,2]").caretPosition = 0
                    Session.findById("wnd[3]").sendVKey(2)
                    Session.findById("wnd[1]/tbar[0]/btn[0]").press
                    Session.findById("wnd[0]/tbar[1]/btn[32]").press
                    Session.findById("wnd[1]/tbar[0]/btn[9]").press
                    Session.findById("wnd[2]/usr/txtRSYSF-STRING").text = mesdet '"TOTAL"
                    Session.findById("wnd[2]/usr/txtRSYSF-STRING").caretPosition = 5
                    Session.findById("wnd[2]/tbar[0]/btn[0]").press
                    Session.findById("wnd[3]/usr/lbl[2,2]").setFocus
                    Session.findById("wnd[3]/usr/lbl[2,2]").caretPosition = 2
                    Session.findById("wnd[3]").sendVKey(2)
                    Session.findById("wnd[1]/tbar[0]/btn[0]").press
                    Session.findById("wnd[0]/mbar/menu[2]/menu[6]").select
                    Session.findById("wnd[1]/usr/txtRSYSF-STRING").text = "Salario"
                    Session.findById("wnd[1]/usr/txtRSYSF-STRING").caretPosition = 6
                    Session.findById("wnd[1]/tbar[0]/btn[0]").press
                    Session.findById("wnd[2]/usr/lbl[8,2]").setFocus
                    Session.findById("wnd[2]/usr/lbl[8,2]").caretPosition = 2
                    Session.findById("wnd[2]").sendVKey(2)
                    Session.findById("wnd[0]/usr/lbl[50,8]").setFocus
                    Session.findById("wnd[0]/usr/lbl[50,8]").caretPosition = 4
                    Session.findById("wnd[0]").sendVKey(2)
                    Session.findById("wnd[1]/usr/lbl[1,1]").caretPosition = 2
                    Session.findById("wnd[1]/tbar[0]/btn[2]").press
                    Tiempo(5)
                    Session.findById("wnd[0]/mbar/menu[4]/menu[2]/menu[1]").select
                    Session.findById("wnd[1]/usr/ssubD0500_SUBSCREEN:SAPLSLVC_DIALOG:0501/cmbG51_USPEC_LBOX").setFocus
                    Tiempo(4)
                    Session.findById("wnd[1]/usr/ssubD0500_SUBSCREEN:SAPLSLVC_DIALOG:0501/cmbG51_USPEC_LBOX").key = "X"
                    Session.findById("wnd[1]/usr/ssubD0500_SUBSCREEN:SAPLSLVC_DIALOG:0501/cntlG51_CONTAINER/shellcont/shell").currentCellRow = 1
                    Tiempo(4)
                    Session.findById("wnd[1]/usr/ssubD0500_SUBSCREEN:SAPLSLVC_DIALOG:0501/cntlG51_CONTAINER/shellcont/shell").selectedRows = "1"
                    Session.findById("wnd[1]/usr/ssubD0500_SUBSCREEN:SAPLSLVC_DIALOG:0501/cntlG51_CONTAINER/shellcont/shell").clickCurrentCell

#Region "Guardado de Fichero Txt"
                    Tiempo(17)
                    Tecla("{F9}")
                    Tiempo(30)
                    Tecla("{TAB}")
                    Tiempo(6)
                    Tecla("{ENTER}")
                    Tiempo(30)
                    Tecla("+{TAB}")
                    Tiempo(9)
                    Tecla(ruta)
                    Tiempo(9)
                    Tecla("{TAB}")
                    Tiempo(9)
                    Tecla(Agri2)
                    Tiempo(9)
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

                    Console.WriteLine("Iniciamos Extracciòn Mano de Obra Acumulado Agricola")
                    Dim fab2 As FileInfo = New FileInfo(ruta + Agri2)
                    data = New DataTable()

                    If fab2.Exists Then
                        Dim stringarry As String() = System.IO.File.ReadAllLines(ruta + Agri2, Encoding.[Default])

                        If stringarry.Length > 0 Then
                            Dim cc As Integer = 0
                            Dim arr_ = stringarry(9).Substring(1, stringarry(9).Length - 2).Split("|")
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

                            data.AcceptChanges()

                            Dim dr As System.Data.DataRow = Nothing

                            For i As Integer = 11 To stringarry.Length - 2
                                stringarry(i) = stringarry(i).Replace("S/N|", "S/N")
                                Dim dat = stringarry(i).Substring(1, stringarry(i).Length - 2).Split("|")
                                dr = data.NewRow()

                                For y As Integer = 0 To dat.Length - 1
                                    dr(y) = dat(y).Trim()
                                Next

                                data.Rows.Add(dr)
                            Next

                            data.AcceptChanges()

                            query = "DELETE FROM PlanMo_New.dbo.VENTAS_GR55_MO_GASTO Where Tipo = 'AGR2' AND Periodo = MONTH(GETDATE()) - 1 AND Anio = YEAR(GETDATE());" & vbCrLf

                            Dim cccc As Integer = 0

                            For Each item As DataRow In data.Rows

                                If item.ItemArray(0).ToString() <> String.Empty Then
                                    query += "INSERT INTO PlanMo_New.dbo.VENTAS_GR55_MO_GASTO VALUES ('AGR2',"

                                    For i As Integer = 0 To item.ItemArray.Length - 1

                                        If i = 1 OrElse i = 2 OrElse i = 13 Then

                                            If item.ItemArray(i).ToString().Trim() <> String.Empty Then
                                                query += "'" & DateTime.Parse(item.ItemArray(i).ToString().Replace(".", "/")).ToString("yyyy-MM-dd") & "',"
                                            Else
                                                query += "'',"
                                            End If

                                        Else

                                            If i = 9 OrElse i = 11 Then

                                                If item.ItemArray(i).ToString().Trim() <> String.Empty Then
                                                    Dim val As String = item.ItemArray(i).ToString().Replace(",", "")
                                                    Dim dat As String = If(val.Contains("-"), val.Substring(0, val.Length - 1), "-" & val) * -1
                                                    query += "'" & dat & "',"
                                                Else
                                                    query += "'0',"
                                                End If
                                            Else

                                                If item.ItemArray(i).ToString().Trim() <> String.Empty Then
                                                    query += "'" & item.ItemArray(i).ToString() & "',"
                                                Else
                                                    query += "'0',"
                                                End If
                                            End If
                                        End If
                                    Next

                                    query += ");" & vbCrLf
                                End If

                                query = query.Replace(",);", ");")
                                cccc += 1
                                Console.WriteLine("Leyendo Informacion: " & cccc & " rows")
                            Next

                            conexion = New SqlConnection()
                            conexion.ConnectionString = connString
                            conexion.Open()
                            myCommand = New SqlCommand(query, conexion)
                            myCommand.CommandTimeout = 3000
                            myCommand.ExecuteNonQuery()
                            myCommand.Dispose()
                            conexion.Close()

                        End If

                    End If

#End Region

#Region "Descarga de Informaciòn Gast. Operativo MO - Sucroalcolera"

                    Console.WriteLine("Iniciamos Extracciòn de Informaciòn Gast. Operativa MO")

                    Session.findById("wnd[0]").maximize
                    Session.findById("wnd[0]/tbar[0]/okcd").text = "GR55"
                    Session.findById("wnd[0]/tbar[0]/btn[0]").press
                    Session.findById("wnd[0]/usr/ctxtRGRWJ-JOB").text = "SC21"
                    Session.findById("wnd[0]/usr/ctxtRGRWJ-JOB").caretPosition = 2
                    Session.findById("wnd[0]/tbar[0]/btn[0]").press
                    Session.findById("wnd[0]/tbar[1]/btn[8]").press
                    Session.findById("wnd[0]/usr/ctxt$0BRYEAR").text = thisYear '"2022"
                    Session.findById("wnd[0]/usr/ctxt$GR00004").text = thisMonth '"1"
                    Session.findById("wnd[0]/usr/ctxt$GR00004").setFocus
                    Session.findById("wnd[0]/usr/ctxt$GR00004").caretPosition = 3
                    Session.findById("wnd[0]/tbar[1]/btn[8]").press
                    Session.findById("wnd[0]/mbar/menu[2]/menu[0]").select
                    Session.findById("wnd[1]/tbar[0]/btn[9]").press
                    Session.findById("wnd[2]/usr/txtRSYSF-STRING").text = "G. Fabrica"
                    Session.findById("wnd[2]/usr/txtRSYSF-STRING").caretPosition = 6
                    Session.findById("wnd[2]/tbar[0]/btn[0]").press
                    Session.findById("wnd[3]/usr/lbl[2,2]").setFocus
                    Session.findById("wnd[3]/usr/lbl[2,2]").caretPosition = 0
                    Session.findById("wnd[3]").sendVKey(2)
                    Session.findById("wnd[1]/tbar[0]/btn[0]").press
                    Session.findById("wnd[0]/tbar[1]/btn[32]").press
                    Session.findById("wnd[1]/tbar[0]/btn[9]").press
                    Session.findById("wnd[2]/usr/txtRSYSF-STRING").text = mesdet '"TOTAL"
                    Session.findById("wnd[2]/usr/txtRSYSF-STRING").caretPosition = 5
                    Session.findById("wnd[2]/tbar[0]/btn[0]").press
                    Session.findById("wnd[3]/usr/lbl[2,2]").setFocus
                    Session.findById("wnd[3]/usr/lbl[2,2]").caretPosition = 2
                    Session.findById("wnd[3]").sendVKey(2)
                    Session.findById("wnd[1]/tbar[0]/btn[0]").press
                    Session.findById("wnd[0]/mbar/menu[2]/menu[6]").select
                    Session.findById("wnd[1]/usr/txtRSYSF-STRING").text = "Salario"
                    Session.findById("wnd[1]/usr/txtRSYSF-STRING").caretPosition = 6
                    Session.findById("wnd[1]/tbar[0]/btn[0]").press
                    Session.findById("wnd[2]/usr/lbl[8,2]").setFocus
                    Session.findById("wnd[2]/usr/lbl[8,2]").caretPosition = 2
                    Session.findById("wnd[2]").sendVKey(2)
                    Session.findById("wnd[0]/usr/lbl[50,8]").setFocus
                    Session.findById("wnd[0]/usr/lbl[50,8]").caretPosition = 4
                    Session.findById("wnd[0]").sendVKey(2)
                    Session.findById("wnd[1]/usr/lbl[1,1]").caretPosition = 2
                    Session.findById("wnd[1]/tbar[0]/btn[2]").press
                    Tiempo(5)
                    Session.findById("wnd[0]/mbar/menu[4]/menu[2]/menu[1]").select
                    Session.findById("wnd[1]/usr/ssubD0500_SUBSCREEN:SAPLSLVC_DIALOG:0501/cmbG51_USPEC_LBOX").setFocus
                    Tiempo(4)
                    Session.findById("wnd[1]/usr/ssubD0500_SUBSCREEN:SAPLSLVC_DIALOG:0501/cmbG51_USPEC_LBOX").key = "X"
                    Session.findById("wnd[1]/usr/ssubD0500_SUBSCREEN:SAPLSLVC_DIALOG:0501/cntlG51_CONTAINER/shellcont/shell").currentCellRow = 1
                    Tiempo(4)
                    Session.findById("wnd[1]/usr/ssubD0500_SUBSCREEN:SAPLSLVC_DIALOG:0501/cntlG51_CONTAINER/shellcont/shell").selectedRows = "1"
                    Session.findById("wnd[1]/usr/ssubD0500_SUBSCREEN:SAPLSLVC_DIALOG:0501/cntlG51_CONTAINER/shellcont/shell").clickCurrentCell

#Region "Guardado de Fichero Txt"
                    Tiempo(17)
                    Tecla("{F9}")
                    Tiempo(20)
                    Tecla("{TAB}")
                    Tiempo(6)
                    Tecla("{ENTER}")
                    Tiempo(20)
                    Tecla("+{TAB}")
                    Tiempo(9)
                    Tecla(ruta)
                    Tiempo(9)
                    Tecla("{TAB}")
                    Tiempo(9)
                    Tecla(Sucr1)
                    Tiempo(9)
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

                    Session.findById("wnd[0]/tbar[0]/btn[3]").press

                    Console.WriteLine("Iniciamos Extracciòn Mano de Obra Operativa Sucro")
                    Dim fab3 As FileInfo = New FileInfo(ruta + Sucr1)
                    data = New DataTable()

                    If fab3.Exists Then
                        Dim stringarry As String() = System.IO.File.ReadAllLines(ruta + Sucr1, Encoding.[Default])

                        If stringarry.Length > 0 Then
                            Dim cc As Integer = 0
                            Dim arr_ = stringarry(9).Substring(1, stringarry(9).Length - 2).Split("|")
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

                            data.AcceptChanges()

                            Dim dr As System.Data.DataRow = Nothing

                            For i As Integer = 11 To stringarry.Length - 2
                                stringarry(i) = stringarry(i).Replace("S/N|", "S/N")
                                Dim dat = stringarry(i).Substring(1, stringarry(i).Length - 2).Split("|")
                                dr = data.NewRow()

                                For y As Integer = 0 To dat.Length - 1
                                    dr(y) = dat(y).Trim()
                                Next

                                data.Rows.Add(dr)
                            Next

                            data.AcceptChanges()

                            query = "DELETE FROM PlanMo_New.dbo.VENTAS_GR55_MO_GASTO Where Tipo = 'SUC1' AND Periodo = MONTH(GETDATE()) - 1 AND Anio = YEAR(GETDATE());" & vbCrLf

                            Dim cccc As Integer = 0

                            For Each item As DataRow In data.Rows

                                If item.ItemArray(0).ToString() <> String.Empty Then
                                    query += "INSERT INTO PlanMo_New.dbo.VENTAS_GR55_MO_GASTO VALUES ('SUC1',"

                                    For i As Integer = 0 To item.ItemArray.Length - 1

                                        If i = 1 OrElse i = 2 OrElse i = 13 Then

                                            If item.ItemArray(i).ToString().Trim() <> String.Empty Then
                                                query += "'" & DateTime.Parse(item.ItemArray(i).ToString().Replace(".", "/")).ToString("yyyy-MM-dd") & "',"
                                            Else
                                                query += "'',"
                                            End If

                                        Else

                                            If i = 9 OrElse i = 11 Then

                                                If item.ItemArray(i).ToString().Trim() <> String.Empty Then
                                                    Dim val As String = item.ItemArray(i).ToString().Replace(",", "")
                                                    Dim dat As String = If(val.Contains("-"), val.Substring(0, val.Length - 1), "-" & val) * -1
                                                    query += "'" & dat & "',"
                                                Else
                                                    query += "'0',"
                                                End If
                                            Else

                                                If item.ItemArray(i).ToString().Trim() <> String.Empty Then
                                                    query += "'" & item.ItemArray(i).ToString() & "',"
                                                Else
                                                    query += "'0',"
                                                End If
                                            End If
                                        End If
                                    Next

                                    query += ");" & vbCrLf
                                End If

                                query = query.Replace(",);", ");")
                                cccc += 1
                                Console.WriteLine("Leyendo Informacion: " & cccc & " rows")
                            Next

                            conexion = New SqlConnection()
                            conexion.ConnectionString = connString
                            conexion.Open()
                            myCommand = New SqlCommand(query, conexion)
                            myCommand.CommandTimeout = 3000
                            myCommand.ExecuteNonQuery()
                            myCommand.Dispose()
                            conexion.Close()

                        End If

                    End If

#End Region

#Region "Descarga de Informaciòn Costo. Acumulado MO - Sucroalcolera"

                    Console.WriteLine("Iniciamos Extracciòn de Informaciòn Costo. Acumulado MO")


                    Session.findById("wnd[0]/mbar/menu[2]/menu[0]").select
                    Session.findById("wnd[1]/tbar[0]/btn[9]").press
                    Session.findById("wnd[2]/usr/txtRSYSF-STRING").text = "Analisis Subreparto"
                    Session.findById("wnd[2]/usr/txtRSYSF-STRING").caretPosition = 6
                    Session.findById("wnd[2]/tbar[0]/btn[0]").press
                    Session.findById("wnd[3]/usr/lbl[2,2]").setFocus
                    Session.findById("wnd[3]/usr/lbl[2,2]").caretPosition = 0
                    Session.findById("wnd[3]").sendVKey(2)
                    Session.findById("wnd[1]/tbar[0]/btn[0]").press
                    Session.findById("wnd[0]/tbar[1]/btn[32]").press
                    Session.findById("wnd[1]/tbar[0]/btn[9]").press
                    Session.findById("wnd[2]/usr/txtRSYSF-STRING").text = mesdet '"TOTAL"
                    Session.findById("wnd[2]/usr/txtRSYSF-STRING").caretPosition = 5
                    Session.findById("wnd[2]/tbar[0]/btn[0]").press
                    Session.findById("wnd[3]/usr/lbl[2,2]").setFocus
                    Session.findById("wnd[3]/usr/lbl[2,2]").caretPosition = 2
                    Session.findById("wnd[3]").sendVKey(2)
                    Session.findById("wnd[1]/tbar[0]/btn[0]").press
                    Session.findById("wnd[0]/mbar/menu[2]/menu[6]").select
                    Session.findById("wnd[1]/usr/txtRSYSF-STRING").text = "Salario"
                    Session.findById("wnd[1]/usr/txtRSYSF-STRING").caretPosition = 6
                    Session.findById("wnd[1]/tbar[0]/btn[0]").press
                    Session.findById("wnd[2]/usr/lbl[8,2]").setFocus
                    Session.findById("wnd[2]/usr/lbl[8,2]").caretPosition = 2
                    Session.findById("wnd[2]").sendVKey(2)
                    Session.findById("wnd[0]/usr/lbl[50,6]").setFocus
                    Session.findById("wnd[0]/usr/lbl[50,6]").caretPosition = 4
                    Session.findById("wnd[0]").sendVKey(2)
                    Session.findById("wnd[1]/usr/lbl[1,1]").caretPosition = 2
                    Session.findById("wnd[1]/tbar[0]/btn[2]").press
                    Tiempo(5)
                    Session.findById("wnd[0]/mbar/menu[4]/menu[2]/menu[1]").select
                    Session.findById("wnd[1]/usr/ssubD0500_SUBSCREEN:SAPLSLVC_DIALOG:0501/cmbG51_USPEC_LBOX").setFocus
                    Tiempo(4)
                    Session.findById("wnd[1]/usr/ssubD0500_SUBSCREEN:SAPLSLVC_DIALOG:0501/cmbG51_USPEC_LBOX").key = "X"
                    Session.findById("wnd[1]/usr/ssubD0500_SUBSCREEN:SAPLSLVC_DIALOG:0501/cntlG51_CONTAINER/shellcont/shell").currentCellRow = 1
                    Tiempo(4)
                    Session.findById("wnd[1]/usr/ssubD0500_SUBSCREEN:SAPLSLVC_DIALOG:0501/cntlG51_CONTAINER/shellcont/shell").selectedRows = "1"
                    Session.findById("wnd[1]/usr/ssubD0500_SUBSCREEN:SAPLSLVC_DIALOG:0501/cntlG51_CONTAINER/shellcont/shell").clickCurrentCell

#Region "Guardado de Fichero Txt"
                    Tiempo(17)
                    Tecla("{F9}")
                    Tiempo(20)
                    Tecla("{TAB}")
                    Tiempo(6)
                    Tecla("{ENTER}")
                    Tiempo(20)
                    Tecla("+{TAB}")
                    Tiempo(9)
                    Tecla(ruta)
                    Tiempo(9)
                    Tecla("{TAB}")
                    Tiempo(9)
                    Tecla(Sucr2)
                    Tiempo(9)
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

                    Console.WriteLine("Iniciamos Extracciòn Mano de Obra Acumulado Sucro")
                    Dim fab4 As FileInfo = New FileInfo(ruta + Sucr2)
                    data = New DataTable()

                    If fab4.Exists Then
                        Dim stringarry As String() = System.IO.File.ReadAllLines(ruta + Sucr2, Encoding.[Default])

                        If stringarry.Length > 0 Then
                            Dim cc As Integer = 0
                            Dim arr_ = stringarry(9).Substring(1, stringarry(9).Length - 2).Split("|")
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

                            data.AcceptChanges()

                            Dim dr As System.Data.DataRow = Nothing

                            For i As Integer = 11 To stringarry.Length - 2
                                stringarry(i) = stringarry(i).Replace("S/N|", "S/N")
                                Dim dat = stringarry(i).Substring(1, stringarry(i).Length - 2).Split("|")
                                dr = data.NewRow()

                                For y As Integer = 0 To dat.Length - 1
                                    dr(y) = dat(y).Trim()
                                Next

                                data.Rows.Add(dr)
                            Next

                            data.AcceptChanges()

                            query = "DELETE FROM PlanMo_New.dbo.VENTAS_GR55_MO_COSTO Where Tipo = 'SUC2' AND Periodo = MONTH(GETDATE()) - 1 AND Anio = YEAR(GETDATE());" & vbCrLf

                            Dim cccc As Integer = 0

                            For Each item As DataRow In data.Rows

                                If item.ItemArray(0).ToString() <> String.Empty Then
                                    query += "INSERT INTO PlanMo_New.dbo.VENTAS_GR55_MO_COSTO VALUES ('SUC2',"

                                    For i As Integer = 0 To item.ItemArray.Length - 1

                                        If i = 1 OrElse i = 2 OrElse i = 13 Then

                                            If item.ItemArray(i).ToString().Trim() <> String.Empty Then
                                                query += "'" & DateTime.Parse(item.ItemArray(i).ToString().Replace(".", "/")).ToString("yyyy-MM-dd") & "',"
                                            Else
                                                query += "'',"
                                            End If

                                        Else

                                            If i = 9 OrElse i = 11 Then

                                                If item.ItemArray(i).ToString().Trim() <> String.Empty Then
                                                    Dim val As String = item.ItemArray(i).ToString().Replace(",", "")
                                                    Dim dat As String = If(val.Contains("-"), val.Substring(0, val.Length - 1), "-" & val) * -1
                                                    query += "'" & dat & "',"
                                                Else
                                                    query += "'0',"
                                                End If
                                            Else

                                                If item.ItemArray(i).ToString().Trim() <> String.Empty Then
                                                    query += "'" & item.ItemArray(i).ToString() & "',"
                                                Else
                                                    query += "'0',"
                                                End If
                                            End If
                                        End If
                                    Next

                                    query += ");" & vbCrLf
                                End If

                                query = query.Replace(",);", ");")
                                cccc += 1
                                Console.WriteLine("Leyendo Informacion: " & cccc & " rows")
                            Next

                            conexion = New SqlConnection()
                            conexion.ConnectionString = connString
                            conexion.Open()
                            myCommand = New SqlCommand(query, conexion)
                            myCommand.CommandTimeout = 3000
                            myCommand.ExecuteNonQuery()
                            myCommand.Dispose()
                            conexion.Close()

                        End If

                    End If

#End Region

#Region "Descarga de Informaciòn Gast. Operativos MO - Bioenergia"

                    Console.WriteLine("Iniciamos Extracciòn de Informaciòn Gast. Operativos MO")

                    Session.findById("wnd[0]").maximize
                    Session.findById("wnd[0]/tbar[0]/okcd").text = "GR55"
                    Session.findById("wnd[0]/tbar[0]/btn[0]").press
                    Session.findById("wnd[0]/usr/ctxtRGRWJ-JOB").text = "BC21"
                    Session.findById("wnd[0]/usr/ctxtRGRWJ-JOB").caretPosition = 2
                    Session.findById("wnd[0]/tbar[0]/btn[0]").press
                    Session.findById("wnd[0]/tbar[1]/btn[8]").press
                    Session.findById("wnd[0]/usr/ctxt$0BRYEAR").text = thisYear '"2022"
                    Session.findById("wnd[0]/usr/ctxt$GR00004").text = thisMonth '"1"
                    Session.findById("wnd[0]/usr/ctxt$GR00004").setFocus
                    Session.findById("wnd[0]/usr/ctxt$GR00004").caretPosition = 3
                    Session.findById("wnd[0]/tbar[1]/btn[8]").press
                    Session.findById("wnd[0]/mbar/menu[2]/menu[0]").select
                    Session.findById("wnd[1]/tbar[0]/btn[9]").press
                    Session.findById("wnd[2]/usr/txtRSYSF-STRING").text = "G. FAbrica"
                    Session.findById("wnd[2]/usr/txtRSYSF-STRING").caretPosition = 6
                    Session.findById("wnd[2]/tbar[0]/btn[0]").press
                    Session.findById("wnd[3]/usr/lbl[2,2]").setFocus
                    Session.findById("wnd[3]/usr/lbl[2,2]").caretPosition = 0
                    Session.findById("wnd[3]").sendVKey(2)
                    Session.findById("wnd[1]/tbar[0]/btn[0]").press
                    Session.findById("wnd[0]/tbar[1]/btn[32]").press
                    Session.findById("wnd[1]/tbar[0]/btn[9]").press
                    Session.findById("wnd[2]/usr/txtRSYSF-STRING").text = mesdet '"TOTAL"
                    Session.findById("wnd[2]/usr/txtRSYSF-STRING").caretPosition = 5
                    Session.findById("wnd[2]/tbar[0]/btn[0]").press
                    Session.findById("wnd[3]/usr/lbl[2,2]").setFocus
                    Session.findById("wnd[3]/usr/lbl[2,2]").caretPosition = 2
                    Session.findById("wnd[3]").sendVKey(2)
                    Session.findById("wnd[1]/tbar[0]/btn[0]").press
                    Session.findById("wnd[0]/mbar/menu[2]/menu[6]").select
                    Session.findById("wnd[1]/usr/txtRSYSF-STRING").text = "Salario"
                    Session.findById("wnd[1]/usr/txtRSYSF-STRING").caretPosition = 6
                    Session.findById("wnd[1]/tbar[0]/btn[0]").press
                    Session.findById("wnd[2]/usr/lbl[8,2]").setFocus
                    Session.findById("wnd[2]/usr/lbl[8,2]").caretPosition = 2
                    Session.findById("wnd[2]").sendVKey(2)
                    Session.findById("wnd[0]/usr/lbl[50,8]").setFocus
                    Session.findById("wnd[0]/usr/lbl[50,8]").caretPosition = 4
                    Session.findById("wnd[0]").sendVKey(2)
                    Session.findById("wnd[1]/usr/lbl[1,1]").caretPosition = 2
                    Session.findById("wnd[1]/tbar[0]/btn[2]").press
                    Tiempo(5)
                    Session.findById("wnd[0]/mbar/menu[4]/menu[2]/menu[1]").select
                    Session.findById("wnd[1]/usr/ssubD0500_SUBSCREEN:SAPLSLVC_DIALOG:0501/cmbG51_USPEC_LBOX").setFocus
                    Tiempo(4)
                    Session.findById("wnd[1]/usr/ssubD0500_SUBSCREEN:SAPLSLVC_DIALOG:0501/cmbG51_USPEC_LBOX").key = "X"
                    Session.findById("wnd[1]/usr/ssubD0500_SUBSCREEN:SAPLSLVC_DIALOG:0501/cntlG51_CONTAINER/shellcont/shell").currentCellRow = 1
                    Tiempo(4)
                    Session.findById("wnd[1]/usr/ssubD0500_SUBSCREEN:SAPLSLVC_DIALOG:0501/cntlG51_CONTAINER/shellcont/shell").selectedRows = "1"
                    Session.findById("wnd[1]/usr/ssubD0500_SUBSCREEN:SAPLSLVC_DIALOG:0501/cntlG51_CONTAINER/shellcont/shell").clickCurrentCell

#Region "Guardado de Fichero Txt"
                    Tiempo(17)
                    Tecla("{F9}")
                    Tiempo(20)
                    Tecla("{TAB}")
                    Tiempo(6)
                    Tecla("{ENTER}")
                    Tiempo(20)
                    Tecla("+{TAB}")
                    Tiempo(9)
                    Tecla(ruta)
                    Tiempo(9)
                    Tecla("{TAB}")
                    Tiempo(9)
                    Tecla(Bio1)
                    Tiempo(9)
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

                    Session.findById("wnd[0]/tbar[0]/btn[3]").press

                    Console.WriteLine("Iniciamos Extracciòn Mano de Obra Operativo Bio")
                    Dim fab5 As FileInfo = New FileInfo(ruta + Bio1)
                    data = New DataTable()

                    If fab5.Exists Then
                        Dim stringarry As String() = System.IO.File.ReadAllLines(ruta + Bio1, Encoding.[Default])

                        If stringarry.Length > 0 Then
                            Dim cc As Integer = 0
                            Dim arr_ = stringarry(9).Substring(1, stringarry(9).Length - 2).Split("|")
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

                            data.AcceptChanges()

                            Dim dr As System.Data.DataRow = Nothing

                            For i As Integer = 11 To stringarry.Length - 2
                                stringarry(i) = stringarry(i).Replace("S/N|", "S/N")
                                Dim dat = stringarry(i).Substring(1, stringarry(i).Length - 2).Split("|")
                                dr = data.NewRow()

                                For y As Integer = 0 To dat.Length - 1
                                    dr(y) = dat(y).Trim()
                                Next

                                data.Rows.Add(dr)
                            Next

                            data.AcceptChanges()

                            query = "DELETE FROM PlanMo_New.dbo.VENTAS_GR55_MO_GASTO Where Tipo = 'BIO1' AND Periodo = MONTH(GETDATE()) - 1 AND Anio = YEAR(GETDATE());" & vbCrLf

                            Dim cccc As Integer = 0

                            For Each item As DataRow In data.Rows

                                If item.ItemArray(0).ToString() <> String.Empty Then
                                    query += "INSERT INTO PlanMo_New.dbo.VENTAS_GR55_MO_GASTO VALUES ('BIO1',"

                                    For i As Integer = 0 To item.ItemArray.Length - 1

                                        If i = 1 OrElse i = 2 OrElse i = 13 Then

                                            If item.ItemArray(i).ToString().Trim() <> String.Empty Then
                                                query += "'" & DateTime.Parse(item.ItemArray(i).ToString().Replace(".", "/")).ToString("yyyy-MM-dd") & "',"
                                            Else
                                                query += "'',"
                                            End If

                                        Else

                                            If i = 9 OrElse i = 11 Then

                                                If item.ItemArray(i).ToString().Trim() <> String.Empty Then
                                                    Dim val As String = item.ItemArray(i).ToString().Replace(",", "")
                                                    Dim dat As String = If(val.Contains("-"), val.Substring(0, val.Length - 1), "-" & val) * -1
                                                    query += "'" & dat & "',"
                                                Else
                                                    query += "'0',"
                                                End If
                                            Else

                                                If item.ItemArray(i).ToString().Trim() <> String.Empty Then
                                                    query += "'" & item.ItemArray(i).ToString() & "',"
                                                Else
                                                    query += "'0',"
                                                End If
                                            End If
                                        End If
                                    Next

                                    query += ");" & vbCrLf
                                End If

                                query = query.Replace(",);", ");")
                                cccc += 1
                                Console.WriteLine("Leyendo Informacion: " & cccc & " rows")
                            Next

                            conexion = New SqlConnection()
                            conexion.ConnectionString = connString
                            conexion.Open()
                            myCommand = New SqlCommand(query, conexion)
                            myCommand.CommandTimeout = 3000
                            myCommand.ExecuteNonQuery()
                            myCommand.Dispose()
                            conexion.Close()

                        End If

                    End If

#End Region

#Region "Descarga de Informaciòn Costo. Acumulado MO - Bioenergia"

                    Console.WriteLine("Iniciamos Extracciòn de Informaciòn Costo. Acumulado MO")

                    Session.findById("wnd[0]/mbar/menu[2]/menu[0]").select
                    Session.findById("wnd[1]/tbar[0]/btn[9]").press
                    Session.findById("wnd[2]/usr/txtRSYSF-STRING").text = "G y P Mes a Mes"
                    Session.findById("wnd[2]/usr/txtRSYSF-STRING").caretPosition = 15
                    Session.findById("wnd[2]/tbar[0]/btn[0]").press
                    Session.findById("wnd[3]/usr/lbl[2,2]").setFocus
                    Session.findById("wnd[3]/usr/lbl[2,2]").caretPosition = 4
                    Session.findById("wnd[3]").sendVKey(2)
                    Session.findById("wnd[1]/tbar[0]/btn[0]").press
                    Session.findById("wnd[0]/tbar[1]/btn[32]").press
                    Session.findById("wnd[1]/tbar[0]/btn[9]").press
                    Session.findById("wnd[2]/usr/txtRSYSF-STRING").text = mesdet '"Ene"
                    Session.findById("wnd[2]/usr/txtRSYSF-STRING").caretPosition = 3
                    Session.findById("wnd[2]/tbar[0]/btn[0]").press
                    Session.findById("wnd[3]/usr/lbl[2,2]").setFocus
                    Session.findById("wnd[3]/usr/lbl[2,2]").caretPosition = 1
                    Session.findById("wnd[3]").sendVKey(2)
                    Session.findById("wnd[1]/tbar[0]/btn[0]").press
                    Session.findById("wnd[0]/usr/lbl[5,4]").setFocus
                    Session.findById("wnd[0]/usr/lbl[5,4]").caretPosition = 8
                    Session.findById("wnd[0]/mbar/menu[2]/menu[6]").select
                    Session.findById("wnd[1]/usr/txtRSYSF-STRING").text = "COSTO DE FABRICA"
                    Session.findById("wnd[1]/usr/txtRSYSF-STRING").caretPosition = 16
                    Session.findById("wnd[1]/tbar[0]/btn[0]").press
                    Session.findById("wnd[2]/usr/lbl[13,2]").setFocus
                    Session.findById("wnd[2]/usr/lbl[13,2]").caretPosition = 5
                    Session.findById("wnd[2]").sendVKey(2)
                    Session.findById("wnd[0]/usr/lbl[52,11]").setFocus
                    Session.findById("wnd[0]/usr/lbl[52,11]").caretPosition = 5
                    Session.findById("wnd[0]").sendVKey(2)
                    Session.findById("wnd[1]/usr/lbl[1,1]").caretPosition = 1
                    Session.findById("wnd[1]/tbar[0]/btn[2]").press
                    Tiempo(5)
                    Session.findById("wnd[0]/mbar/menu[4]/menu[2]/menu[1]").select
                    Session.findById("wnd[1]/usr/ssubD0500_SUBSCREEN:SAPLSLVC_DIALOG:0501/cmbG51_USPEC_LBOX").setFocus
                    Tiempo(4)
                    Session.findById("wnd[1]/usr/ssubD0500_SUBSCREEN:SAPLSLVC_DIALOG:0501/cmbG51_USPEC_LBOX").key = "X"
                    Session.findById("wnd[1]/usr/ssubD0500_SUBSCREEN:SAPLSLVC_DIALOG:0501/cntlG51_CONTAINER/shellcont/shell").currentCellRow = 1
                    Tiempo(4)
                    Session.findById("wnd[1]/usr/ssubD0500_SUBSCREEN:SAPLSLVC_DIALOG:0501/cntlG51_CONTAINER/shellcont/shell").selectedRows = "1"
                    Session.findById("wnd[1]/usr/ssubD0500_SUBSCREEN:SAPLSLVC_DIALOG:0501/cntlG51_CONTAINER/shellcont/shell").clickCurrentCell

#Region "Guardado de Fichero Txt"
                    Tiempo(17)
                    Tecla("{F9}")
                    Tiempo(20)
                    Tecla("{TAB}")
                    Tiempo(6)
                    Tecla("{ENTER}")
                    Tiempo(20)
                    Tecla("+{TAB}")
                    Tiempo(9)
                    Tecla(ruta)
                    Tiempo(9)
                    Tecla("{TAB}")
                    Tiempo(9)
                    Tecla(Bio2)
                    Tiempo(9)
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

                    Session.findById("wnd[0]/tbar[0]/btn[3]").press
                    Tiempo(8)
                    Session.findById("wnd[0]").close
                    Session.findById("wnd[1]/usr/btnSPOP-OPTION1").press

                    Tiempo(8)
                    AppActivate("SAP Logon 760")
                    Tiempo(8)
                    Tecla("%{F4}")
                    Tiempo(8)

                    Console.WriteLine("Iniciamos Extracciòn Mano de Obra Acumulado Sucro")
                    Dim fab6 As FileInfo = New FileInfo(ruta + Bio2)
                    data = New DataTable()

                    If fab6.Exists Then
                        Dim stringarry As String() = System.IO.File.ReadAllLines(ruta + Bio2, Encoding.[Default])

                        If stringarry.Length > 0 Then
                            Dim cc As Integer = 0
                            Dim arr_ = stringarry(9).Substring(1, stringarry(9).Length - 2).Split("|")
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

                            data.AcceptChanges()

                            Dim dr As System.Data.DataRow = Nothing

                            For i As Integer = 11 To stringarry.Length - 2
                                stringarry(i) = stringarry(i).Replace("S/N|", "S/N")
                                Dim dat = stringarry(i).Substring(1, stringarry(i).Length - 2).Split("|")
                                dr = data.NewRow()

                                For y As Integer = 0 To dat.Length - 1
                                    dr(y) = dat(y).Trim()
                                Next

                                data.Rows.Add(dr)
                            Next

                            data.AcceptChanges()

                            query = "DELETE FROM PlanMo_New.dbo.VENTAS_GR55_MO_COSTO Where Tipo = 'BIO2' AND Periodo = MONTH(GETDATE()) - 1 AND Anio = YEAR(GETDATE());" & vbCrLf

                            Dim cccc As Integer = 0

                            For Each item As DataRow In data.Rows

                                If item.ItemArray(0).ToString() <> String.Empty Then
                                    query += "INSERT INTO PlanMo_New.dbo.VENTAS_GR55_MO_COSTO VALUES ('BIO2',"

                                    For i As Integer = 0 To item.ItemArray.Length - 1

                                        If i = 1 OrElse i = 2 OrElse i = 13 Then

                                            If item.ItemArray(i).ToString().Trim() <> String.Empty Then
                                                query += "'" & DateTime.Parse(item.ItemArray(i).ToString().Replace(".", "/")).ToString("yyyy-MM-dd") & "',"
                                            Else
                                                query += "'',"
                                            End If

                                        Else

                                            If i = 9 OrElse i = 11 Then

                                                If item.ItemArray(i).ToString().Trim() <> String.Empty Then
                                                    Dim val As String = item.ItemArray(i).ToString().Replace(",", "")
                                                    Dim dat As String = If(val.Contains("-"), val.Substring(0, val.Length - 1), "-" & val) * -1
                                                    query += "'" & dat & "',"
                                                Else
                                                    query += "'0',"
                                                End If
                                            Else

                                                If item.ItemArray(i).ToString().Trim() <> String.Empty Then
                                                    query += "'" & item.ItemArray(i).ToString() & "',"
                                                Else
                                                    query += "'0',"
                                                End If
                                            End If
                                        End If
                                    Next

                                    query += ");" & vbCrLf
                                End If

                                query = query.Replace(",);", ");")
                                cccc += 1
                                Console.WriteLine("Leyendo Informacion: " & cccc & " rows")
                            Next

                            conexion = New SqlConnection()
                            conexion.ConnectionString = connString
                            conexion.Open()
                            myCommand = New SqlCommand(query, conexion)
                            myCommand.CommandTimeout = 3000
                            myCommand.ExecuteNonQuery()
                            myCommand.Dispose()
                            conexion.Close()

                        End If

                    End If

#End Region

                End If

#End Region

#Region "Si el Mes Actual es Igual a enero para descargar data de diciembre del año anterior"
                If DateTime.Now.Day >= 6 And DateTime.Now.Month = 1 Then

#Region "Meses"
                    If thisMonth = 1 Then
                        mesdet = "Dic"
                    End If
                    If thisMonth = 3 Then
                        thisYear = Year(Monthe) - 1
                    End If
#End Region

#Region "Descarga de Informaciòn Gast. Operativo MO - Agricola"

                    Console.WriteLine("Iniciamos Extracciòn de Informaciòn Gast. Operativo MO")

                    Session.findById("wnd[0]").maximize
                    Session.findById("wnd[0]/tbar[0]/okcd").text = "GR55"
                    Session.findById("wnd[0]/tbar[0]/btn[0]").press
                    Session.findById("wnd[0]/usr/ctxtRGRWJ-JOB").text = "AC21"
                    Session.findById("wnd[0]/usr/ctxtRGRWJ-JOB").caretPosition = 2
                    Session.findById("wnd[0]/tbar[0]/btn[0]").press
                    Session.findById("wnd[0]/tbar[1]/btn[8]").press
                    Session.findById("wnd[0]/usr/ctxt$0BRYEAR").text = thisYear '"2022"
                    Session.findById("wnd[0]/usr/ctxt$GR00004").text = "12"
                    Session.findById("wnd[0]/usr/ctxt$GR00004").setFocus
                    Session.findById("wnd[0]/usr/ctxt$GR00004").caretPosition = 3
                    Session.findById("wnd[0]/tbar[1]/btn[8]").press
                    Session.findById("wnd[0]/mbar/menu[2]/menu[0]").select
                    Session.findById("wnd[1]/tbar[0]/btn[9]").press
                    Session.findById("wnd[2]/usr/txtRSYSF-STRING").text = "G. Operat."
                    Session.findById("wnd[2]/usr/txtRSYSF-STRING").caretPosition = 6
                    Session.findById("wnd[2]/tbar[0]/btn[0]").press
                    Session.findById("wnd[3]/usr/lbl[2,2]").setFocus
                    Session.findById("wnd[3]/usr/lbl[2,2]").caretPosition = 0
                    Session.findById("wnd[3]").sendVKey(2)
                    Session.findById("wnd[1]/tbar[0]/btn[0]").press
                    Session.findById("wnd[0]/tbar[1]/btn[32]").press
                    Session.findById("wnd[1]/tbar[0]/btn[9]").press
                    Session.findById("wnd[2]/usr/txtRSYSF-STRING").text = mesdet '"TOTAL"
                    Session.findById("wnd[2]/usr/txtRSYSF-STRING").caretPosition = 5
                    Session.findById("wnd[2]/tbar[0]/btn[0]").press
                    Session.findById("wnd[3]/usr/lbl[2,2]").setFocus
                    Session.findById("wnd[3]/usr/lbl[2,2]").caretPosition = 2
                    Session.findById("wnd[3]").sendVKey(2)
                    Session.findById("wnd[1]/tbar[0]/btn[0]").press
                    Session.findById("wnd[0]/mbar/menu[2]/menu[6]").select
                    Session.findById("wnd[1]/usr/txtRSYSF-STRING").text = "Salario"
                    Session.findById("wnd[1]/usr/txtRSYSF-STRING").caretPosition = 6
                    Session.findById("wnd[1]/tbar[0]/btn[0]").press
                    Session.findById("wnd[2]/usr/lbl[8,2]").setFocus
                    Session.findById("wnd[2]/usr/lbl[8,2]").caretPosition = 2
                    Session.findById("wnd[2]").sendVKey(2)
                    Session.findById("wnd[0]/usr/lbl[50,8]").setFocus
                    Session.findById("wnd[0]/usr/lbl[50,8]").caretPosition = 4
                    Session.findById("wnd[0]").sendVKey(2)
                    Session.findById("wnd[1]/usr/lbl[1,1]").caretPosition = 2
                    Session.findById("wnd[1]/tbar[0]/btn[2]").press
                    Tiempo(5)
                    Session.findById("wnd[0]/mbar/menu[4]/menu[2]/menu[1]").select
                    Session.findById("wnd[1]/usr/ssubD0500_SUBSCREEN:SAPLSLVC_DIALOG:0501/cmbG51_USPEC_LBOX").setFocus
                    Tiempo(4)
                    Session.findById("wnd[1]/usr/ssubD0500_SUBSCREEN:SAPLSLVC_DIALOG:0501/cmbG51_USPEC_LBOX").key = "X"
                    Session.findById("wnd[1]/usr/ssubD0500_SUBSCREEN:SAPLSLVC_DIALOG:0501/cntlG51_CONTAINER/shellcont/shell").currentCellRow = 1
                    Tiempo(4)
                    Session.findById("wnd[1]/usr/ssubD0500_SUBSCREEN:SAPLSLVC_DIALOG:0501/cntlG51_CONTAINER/shellcont/shell").selectedRows = "1"
                    Session.findById("wnd[1]/usr/ssubD0500_SUBSCREEN:SAPLSLVC_DIALOG:0501/cntlG51_CONTAINER/shellcont/shell").clickCurrentCell

#Region "Guardado de Fichero Txt"
                    Tiempo(17)
                    Tecla("{F9}")
                    Tiempo(30)
                    Tecla("{TAB}")
                    Tiempo(6)
                    Tecla("{ENTER}")
                    Tiempo(30)
                    Tecla("+{TAB}")
                    Tiempo(9)
                    Tecla(ruta)
                    Tiempo(9)
                    Tecla("{TAB}")
                    Tiempo(9)
                    Tecla(Agri1)
                    Tiempo(9)
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

                    Session.findById("wnd[0]/tbar[0]/btn[3]").press

                    Console.WriteLine("Iniciamos Extracciòn Mano de Obra Operativa Agricola")
                    Dim fab1 As FileInfo = New FileInfo(ruta + Agri1)
                    data = New DataTable()

                    If fab1.Exists Then
                        Dim stringarry As String() = System.IO.File.ReadAllLines(ruta + Agri1, Encoding.[Default])

                        If stringarry.Length > 0 Then
                            Dim cc As Integer = 0
                            Dim arr_ = stringarry(9).Substring(1, stringarry(9).Length - 2).Split("|")
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

                            data.AcceptChanges()

                            Dim dr As System.Data.DataRow = Nothing

                            For i As Integer = 11 To stringarry.Length - 2
                                stringarry(i) = stringarry(i).Replace("S/N|", "S/N")
                                Dim dat = stringarry(i).Substring(1, stringarry(i).Length - 2).Split("|")
                                dr = data.NewRow()

                                For y As Integer = 0 To dat.Length - 1
                                    dr(y) = dat(y).Trim()
                                Next

                                data.Rows.Add(dr)
                            Next

                            data.AcceptChanges()

                            query = "DELETE FROM PlanMo_New.dbo.VENTAS_GR55_MO_GASTO Where Tipo = 'AGR1' AND Periodo = 12 AND Anio = YEAR(GETDATE()) - 1 ;" & vbCrLf

                            Dim cccc As Integer = 0

                            For Each item As DataRow In data.Rows

                                If item.ItemArray(0).ToString() <> String.Empty Then
                                    query += "INSERT INTO PlanMo_New.dbo.VENTAS_GR55_MO_GASTO VALUES ('AGR1',"

                                    For i As Integer = 0 To item.ItemArray.Length - 1

                                        If i = 1 OrElse i = 2 OrElse i = 13 Then

                                            If item.ItemArray(i).ToString().Trim() <> String.Empty Then
                                                query += "'" & DateTime.Parse(item.ItemArray(i).ToString().Replace(".", "/")).ToString("yyyy-MM-dd") & "',"
                                            Else
                                                query += "'',"
                                            End If

                                        Else

                                            If i = 9 OrElse i = 11 Then

                                                If item.ItemArray(i).ToString().Trim() <> String.Empty Then
                                                    Dim val As String = item.ItemArray(i).ToString().Replace(",", "")
                                                    Dim dat As String = If(val.Contains("-"), val.Substring(0, val.Length - 1), "-" & val) * -1
                                                    query += "'" & dat & "',"
                                                Else
                                                    query += "'0',"
                                                End If
                                            Else

                                                If item.ItemArray(i).ToString().Trim() <> String.Empty Then
                                                    query += "'" & item.ItemArray(i).ToString() & "',"
                                                Else
                                                    query += "'0',"
                                                End If
                                            End If
                                        End If
                                    Next

                                    query += ");" & vbCrLf
                                End If

                                query = query.Replace(",);", ");")
                                cccc += 1
                                Console.WriteLine("Leyendo Informacion: " & cccc & " rows")
                            Next

                            conexion = New SqlConnection()
                            conexion.ConnectionString = connString
                            conexion.Open()
                            myCommand = New SqlCommand(query, conexion)
                            myCommand.CommandTimeout = 3000
                            myCommand.ExecuteNonQuery()
                            myCommand.Dispose()
                            conexion.Close()

                        End If

                    End If

#End Region

#Region "Descarga de Informaciòn Gast. Acumulado MO - Agricola"

                    Console.WriteLine("Iniciamos Extracciòn de Informaciòn Gast. Acumulado MO")


                    Session.findById("wnd[0]/mbar/menu[2]/menu[0]").select
                    Session.findById("wnd[1]/tbar[0]/btn[9]").press
                    Session.findById("wnd[2]/usr/txtRSYSF-STRING").text = "GRUP.CHIRA G.A"
                    Session.findById("wnd[2]/usr/txtRSYSF-STRING").caretPosition = 6
                    Session.findById("wnd[2]/tbar[0]/btn[0]").press
                    Session.findById("wnd[3]/usr/lbl[2,2]").setFocus
                    Session.findById("wnd[3]/usr/lbl[2,2]").caretPosition = 0
                    Session.findById("wnd[3]").sendVKey(2)
                    Session.findById("wnd[1]/tbar[0]/btn[0]").press
                    Session.findById("wnd[0]/tbar[1]/btn[32]").press
                    Session.findById("wnd[1]/tbar[0]/btn[9]").press
                    Session.findById("wnd[2]/usr/txtRSYSF-STRING").text = mesdet '"TOTAL"
                    Session.findById("wnd[2]/usr/txtRSYSF-STRING").caretPosition = 5
                    Session.findById("wnd[2]/tbar[0]/btn[0]").press
                    Session.findById("wnd[3]/usr/lbl[2,2]").setFocus
                    Session.findById("wnd[3]/usr/lbl[2,2]").caretPosition = 2
                    Session.findById("wnd[3]").sendVKey(2)
                    Session.findById("wnd[1]/tbar[0]/btn[0]").press
                    Session.findById("wnd[0]/mbar/menu[2]/menu[6]").select
                    Session.findById("wnd[1]/usr/txtRSYSF-STRING").text = "Salario"
                    Session.findById("wnd[1]/usr/txtRSYSF-STRING").caretPosition = 6
                    Session.findById("wnd[1]/tbar[0]/btn[0]").press
                    Session.findById("wnd[2]/usr/lbl[8,2]").setFocus
                    Session.findById("wnd[2]/usr/lbl[8,2]").caretPosition = 2
                    Session.findById("wnd[2]").sendVKey(2)
                    Session.findById("wnd[0]/usr/lbl[50,8]").setFocus
                    Session.findById("wnd[0]/usr/lbl[50,8]").caretPosition = 4
                    Session.findById("wnd[0]").sendVKey(2)
                    Session.findById("wnd[1]/usr/lbl[1,1]").caretPosition = 2
                    Session.findById("wnd[1]/tbar[0]/btn[2]").press
                    Tiempo(5)
                    Session.findById("wnd[0]/mbar/menu[4]/menu[2]/menu[1]").select
                    Session.findById("wnd[1]/usr/ssubD0500_SUBSCREEN:SAPLSLVC_DIALOG:0501/cmbG51_USPEC_LBOX").setFocus
                    Tiempo(4)
                    Session.findById("wnd[1]/usr/ssubD0500_SUBSCREEN:SAPLSLVC_DIALOG:0501/cmbG51_USPEC_LBOX").key = "X"
                    Session.findById("wnd[1]/usr/ssubD0500_SUBSCREEN:SAPLSLVC_DIALOG:0501/cntlG51_CONTAINER/shellcont/shell").currentCellRow = 1
                    Tiempo(4)
                    Session.findById("wnd[1]/usr/ssubD0500_SUBSCREEN:SAPLSLVC_DIALOG:0501/cntlG51_CONTAINER/shellcont/shell").selectedRows = "1"
                    Session.findById("wnd[1]/usr/ssubD0500_SUBSCREEN:SAPLSLVC_DIALOG:0501/cntlG51_CONTAINER/shellcont/shell").clickCurrentCell

#Region "Guardado de Fichero Txt"
                    Tiempo(17)
                    Tecla("{F9}")
                    Tiempo(30)
                    Tecla("{TAB}")
                    Tiempo(6)
                    Tecla("{ENTER}")
                    Tiempo(30)
                    Tecla("+{TAB}")
                    Tiempo(9)
                    Tecla(ruta)
                    Tiempo(9)
                    Tecla("{TAB}")
                    Tiempo(9)
                    Tecla(Agri2)
                    Tiempo(9)
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

                    Console.WriteLine("Iniciamos Extracciòn Mano de Obra Acumulado Agricola")
                    Dim fab2 As FileInfo = New FileInfo(ruta + Agri2)
                    data = New DataTable()

                    If fab2.Exists Then
                        Dim stringarry As String() = System.IO.File.ReadAllLines(ruta + Agri2, Encoding.[Default])

                        If stringarry.Length > 0 Then
                            Dim cc As Integer = 0
                            Dim arr_ = stringarry(9).Substring(1, stringarry(9).Length - 2).Split("|")
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

                            data.AcceptChanges()

                            Dim dr As System.Data.DataRow = Nothing

                            For i As Integer = 11 To stringarry.Length - 2
                                stringarry(i) = stringarry(i).Replace("S/N|", "S/N")
                                Dim dat = stringarry(i).Substring(1, stringarry(i).Length - 2).Split("|")
                                dr = data.NewRow()

                                For y As Integer = 0 To dat.Length - 1
                                    dr(y) = dat(y).Trim()
                                Next

                                data.Rows.Add(dr)
                            Next

                            data.AcceptChanges()

                            query = "DELETE FROM PlanMo_New.dbo.VENTAS_GR55_MO_GASTO Where Tipo = 'AGR2' AND Periodo = 12 AND Anio = YEAR(GETDATE()) - 1 ;" & vbCrLf

                            Dim cccc As Integer = 0

                            For Each item As DataRow In data.Rows

                                If item.ItemArray(0).ToString() <> String.Empty Then
                                    query += "INSERT INTO PlanMo_New.dbo.VENTAS_GR55_MO_GASTO VALUES ('AGR2',"

                                    For i As Integer = 0 To item.ItemArray.Length - 1

                                        If i = 1 OrElse i = 2 OrElse i = 13 Then

                                            If item.ItemArray(i).ToString().Trim() <> String.Empty Then
                                                query += "'" & DateTime.Parse(item.ItemArray(i).ToString().Replace(".", "/")).ToString("yyyy-MM-dd") & "',"
                                            Else
                                                query += "'',"
                                            End If

                                        Else

                                            If i = 9 OrElse i = 11 Then

                                                If item.ItemArray(i).ToString().Trim() <> String.Empty Then
                                                    Dim val As String = item.ItemArray(i).ToString().Replace(",", "")
                                                    Dim dat As String = If(val.Contains("-"), val.Substring(0, val.Length - 1), "-" & val) * -1
                                                    query += "'" & dat & "',"
                                                Else
                                                    query += "'0',"
                                                End If
                                            Else

                                                If item.ItemArray(i).ToString().Trim() <> String.Empty Then
                                                    query += "'" & item.ItemArray(i).ToString() & "',"
                                                Else
                                                    query += "'0',"
                                                End If
                                            End If
                                        End If
                                    Next

                                    query += ");" & vbCrLf
                                End If

                                query = query.Replace(",);", ");")
                                cccc += 1
                                Console.WriteLine("Leyendo Informacion: " & cccc & " rows")
                            Next

                            conexion = New SqlConnection()
                            conexion.ConnectionString = connString
                            conexion.Open()
                            myCommand = New SqlCommand(query, conexion)
                            myCommand.CommandTimeout = 3000
                            myCommand.ExecuteNonQuery()
                            myCommand.Dispose()
                            conexion.Close()

                        End If

                    End If

#End Region

#Region "Descarga de Informaciòn Gast. Operativo MO - Sucroalcolera"

                    Console.WriteLine("Iniciamos Extracciòn de Informaciòn Gast. Operativa MO")

                    Session.findById("wnd[0]").maximize
                    Session.findById("wnd[0]/tbar[0]/okcd").text = "GR55"
                    Session.findById("wnd[0]/tbar[0]/btn[0]").press
                    Session.findById("wnd[0]/usr/ctxtRGRWJ-JOB").text = "SC21"
                    Session.findById("wnd[0]/usr/ctxtRGRWJ-JOB").caretPosition = 2
                    Session.findById("wnd[0]/tbar[0]/btn[0]").press
                    Session.findById("wnd[0]/tbar[1]/btn[8]").press
                    Session.findById("wnd[0]/usr/ctxt$0BRYEAR").text = thisYear '"2022"
                    Session.findById("wnd[0]/usr/ctxt$GR00004").text = "12"
                    Session.findById("wnd[0]/usr/ctxt$GR00004").setFocus
                    Session.findById("wnd[0]/usr/ctxt$GR00004").caretPosition = 3
                    Session.findById("wnd[0]/tbar[1]/btn[8]").press
                    Session.findById("wnd[0]/mbar/menu[2]/menu[0]").select
                    Session.findById("wnd[1]/tbar[0]/btn[9]").press
                    Session.findById("wnd[2]/usr/txtRSYSF-STRING").text = "G. Fabrica"
                    Session.findById("wnd[2]/usr/txtRSYSF-STRING").caretPosition = 6
                    Session.findById("wnd[2]/tbar[0]/btn[0]").press
                    Session.findById("wnd[3]/usr/lbl[2,2]").setFocus
                    Session.findById("wnd[3]/usr/lbl[2,2]").caretPosition = 0
                    Session.findById("wnd[3]").sendVKey(2)
                    Session.findById("wnd[1]/tbar[0]/btn[0]").press
                    Session.findById("wnd[0]/tbar[1]/btn[32]").press
                    Session.findById("wnd[1]/tbar[0]/btn[9]").press
                    Session.findById("wnd[2]/usr/txtRSYSF-STRING").text = mesdet '"TOTAL"
                    Session.findById("wnd[2]/usr/txtRSYSF-STRING").caretPosition = 5
                    Session.findById("wnd[2]/tbar[0]/btn[0]").press
                    Session.findById("wnd[3]/usr/lbl[2,2]").setFocus
                    Session.findById("wnd[3]/usr/lbl[2,2]").caretPosition = 2
                    Session.findById("wnd[3]").sendVKey(2)
                    Session.findById("wnd[1]/tbar[0]/btn[0]").press
                    Session.findById("wnd[0]/mbar/menu[2]/menu[6]").select
                    Session.findById("wnd[1]/usr/txtRSYSF-STRING").text = "Salario"
                    Session.findById("wnd[1]/usr/txtRSYSF-STRING").caretPosition = 6
                    Session.findById("wnd[1]/tbar[0]/btn[0]").press
                    Session.findById("wnd[2]/usr/lbl[8,2]").setFocus
                    Session.findById("wnd[2]/usr/lbl[8,2]").caretPosition = 2
                    Session.findById("wnd[2]").sendVKey(2)
                    Session.findById("wnd[0]/usr/lbl[50,8]").setFocus
                    Session.findById("wnd[0]/usr/lbl[50,8]").caretPosition = 4
                    Session.findById("wnd[0]").sendVKey(2)
                    Session.findById("wnd[1]/usr/lbl[1,1]").caretPosition = 2
                    Session.findById("wnd[1]/tbar[0]/btn[2]").press
                    Tiempo(5)
                    Session.findById("wnd[0]/mbar/menu[4]/menu[2]/menu[1]").select
                    Session.findById("wnd[1]/usr/ssubD0500_SUBSCREEN:SAPLSLVC_DIALOG:0501/cmbG51_USPEC_LBOX").setFocus
                    Tiempo(4)
                    Session.findById("wnd[1]/usr/ssubD0500_SUBSCREEN:SAPLSLVC_DIALOG:0501/cmbG51_USPEC_LBOX").key = "X"
                    Session.findById("wnd[1]/usr/ssubD0500_SUBSCREEN:SAPLSLVC_DIALOG:0501/cntlG51_CONTAINER/shellcont/shell").currentCellRow = 1
                    Tiempo(4)
                    Session.findById("wnd[1]/usr/ssubD0500_SUBSCREEN:SAPLSLVC_DIALOG:0501/cntlG51_CONTAINER/shellcont/shell").selectedRows = "1"
                    Session.findById("wnd[1]/usr/ssubD0500_SUBSCREEN:SAPLSLVC_DIALOG:0501/cntlG51_CONTAINER/shellcont/shell").clickCurrentCell

#Region "Guardado de Fichero Txt"
                    Tiempo(17)
                    Tecla("{F9}")
                    Tiempo(20)
                    Tecla("{TAB}")
                    Tiempo(6)
                    Tecla("{ENTER}")
                    Tiempo(20)
                    Tecla("+{TAB}")
                    Tiempo(9)
                    Tecla(ruta)
                    Tiempo(9)
                    Tecla("{TAB}")
                    Tiempo(9)
                    Tecla(Sucr1)
                    Tiempo(9)
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

                    Session.findById("wnd[0]/tbar[0]/btn[3]").press

                    Console.WriteLine("Iniciamos Extracciòn Mano de Obra Operativa Sucro")
                    Dim fab3 As FileInfo = New FileInfo(ruta + Sucr1)
                    data = New DataTable()

                    If fab3.Exists Then
                        Dim stringarry As String() = System.IO.File.ReadAllLines(ruta + Sucr1, Encoding.[Default])

                        If stringarry.Length > 0 Then
                            Dim cc As Integer = 0
                            Dim arr_ = stringarry(9).Substring(1, stringarry(9).Length - 2).Split("|")
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

                            data.AcceptChanges()

                            Dim dr As System.Data.DataRow = Nothing

                            For i As Integer = 11 To stringarry.Length - 2
                                stringarry(i) = stringarry(i).Replace("S/N|", "S/N")
                                Dim dat = stringarry(i).Substring(1, stringarry(i).Length - 2).Split("|")
                                dr = data.NewRow()

                                For y As Integer = 0 To dat.Length - 1
                                    dr(y) = dat(y).Trim()
                                Next

                                data.Rows.Add(dr)
                            Next

                            data.AcceptChanges()

                            query = "DELETE FROM PlanMo_New.dbo.VENTAS_GR55_MO_GASTO Where Tipo = 'SUC1' AND Periodo = 12 AND Anio = YEAR(GETDATE()) - 1 ;" & vbCrLf

                            Dim cccc As Integer = 0

                            For Each item As DataRow In data.Rows

                                If item.ItemArray(0).ToString() <> String.Empty Then
                                    query += "INSERT INTO PlanMo_New.dbo.VENTAS_GR55_MO_GASTO VALUES ('SUC1',"

                                    For i As Integer = 0 To item.ItemArray.Length - 1

                                        If i = 1 OrElse i = 2 OrElse i = 13 Then

                                            If item.ItemArray(i).ToString().Trim() <> String.Empty Then
                                                query += "'" & DateTime.Parse(item.ItemArray(i).ToString().Replace(".", "/")).ToString("yyyy-MM-dd") & "',"
                                            Else
                                                query += "'',"
                                            End If

                                        Else

                                            If i = 9 OrElse i = 11 Then

                                                If item.ItemArray(i).ToString().Trim() <> String.Empty Then
                                                    Dim val As String = item.ItemArray(i).ToString().Replace(",", "")
                                                    Dim dat As String = If(val.Contains("-"), val.Substring(0, val.Length - 1), "-" & val) * -1
                                                    query += "'" & dat & "',"
                                                Else
                                                    query += "'0',"
                                                End If
                                            Else

                                                If item.ItemArray(i).ToString().Trim() <> String.Empty Then
                                                    query += "'" & item.ItemArray(i).ToString() & "',"
                                                Else
                                                    query += "'0',"
                                                End If
                                            End If
                                        End If
                                    Next

                                    query += ");" & vbCrLf
                                End If

                                query = query.Replace(",);", ");")
                                cccc += 1
                                Console.WriteLine("Leyendo Informacion: " & cccc & " rows")
                            Next

                            conexion = New SqlConnection()
                            conexion.ConnectionString = connString
                            conexion.Open()
                            myCommand = New SqlCommand(query, conexion)
                            myCommand.CommandTimeout = 3000
                            myCommand.ExecuteNonQuery()
                            myCommand.Dispose()
                            conexion.Close()

                        End If

                    End If

#End Region

#Region "Descarga de Informaciòn Costo. Acumulado MO - Sucroalcolera"

                    Console.WriteLine("Iniciamos Extracciòn de Informaciòn Costo. Acumulado MO")


                    Session.findById("wnd[0]/mbar/menu[2]/menu[0]").select
                    Session.findById("wnd[1]/tbar[0]/btn[9]").press
                    Session.findById("wnd[2]/usr/txtRSYSF-STRING").text = "Analisis Subreparto"
                    Session.findById("wnd[2]/usr/txtRSYSF-STRING").caretPosition = 6
                    Session.findById("wnd[2]/tbar[0]/btn[0]").press
                    Session.findById("wnd[3]/usr/lbl[2,2]").setFocus
                    Session.findById("wnd[3]/usr/lbl[2,2]").caretPosition = 0
                    Session.findById("wnd[3]").sendVKey(2)
                    Session.findById("wnd[1]/tbar[0]/btn[0]").press
                    Session.findById("wnd[0]/tbar[1]/btn[32]").press
                    Session.findById("wnd[1]/tbar[0]/btn[9]").press
                    Session.findById("wnd[2]/usr/txtRSYSF-STRING").text = mesdet '"TOTAL"
                    Session.findById("wnd[2]/usr/txtRSYSF-STRING").caretPosition = 5
                    Session.findById("wnd[2]/tbar[0]/btn[0]").press
                    Session.findById("wnd[3]/usr/lbl[2,2]").setFocus
                    Session.findById("wnd[3]/usr/lbl[2,2]").caretPosition = 2
                    Session.findById("wnd[3]").sendVKey(2)
                    Session.findById("wnd[1]/tbar[0]/btn[0]").press
                    Session.findById("wnd[0]/mbar/menu[2]/menu[6]").select
                    Session.findById("wnd[1]/usr/txtRSYSF-STRING").text = "Salario"
                    Session.findById("wnd[1]/usr/txtRSYSF-STRING").caretPosition = 6
                    Session.findById("wnd[1]/tbar[0]/btn[0]").press
                    Session.findById("wnd[2]/usr/lbl[8,2]").setFocus
                    Session.findById("wnd[2]/usr/lbl[8,2]").caretPosition = 2
                    Session.findById("wnd[2]").sendVKey(2)
                    Session.findById("wnd[0]/usr/lbl[50,6]").setFocus
                    Session.findById("wnd[0]/usr/lbl[50,6]").caretPosition = 4
                    Session.findById("wnd[0]").sendVKey(2)
                    Session.findById("wnd[1]/usr/lbl[1,1]").caretPosition = 2
                    Session.findById("wnd[1]/tbar[0]/btn[2]").press
                    Tiempo(5)
                    Session.findById("wnd[0]/mbar/menu[4]/menu[2]/menu[1]").select
                    Session.findById("wnd[1]/usr/ssubD0500_SUBSCREEN:SAPLSLVC_DIALOG:0501/cmbG51_USPEC_LBOX").setFocus
                    Tiempo(4)
                    Session.findById("wnd[1]/usr/ssubD0500_SUBSCREEN:SAPLSLVC_DIALOG:0501/cmbG51_USPEC_LBOX").key = "X"
                    Session.findById("wnd[1]/usr/ssubD0500_SUBSCREEN:SAPLSLVC_DIALOG:0501/cntlG51_CONTAINER/shellcont/shell").currentCellRow = 1
                    Tiempo(4)
                    Session.findById("wnd[1]/usr/ssubD0500_SUBSCREEN:SAPLSLVC_DIALOG:0501/cntlG51_CONTAINER/shellcont/shell").selectedRows = "1"
                    Session.findById("wnd[1]/usr/ssubD0500_SUBSCREEN:SAPLSLVC_DIALOG:0501/cntlG51_CONTAINER/shellcont/shell").clickCurrentCell

#Region "Guardado de Fichero Txt"
                    Tiempo(17)
                    Tecla("{F9}")
                    Tiempo(20)
                    Tecla("{TAB}")
                    Tiempo(6)
                    Tecla("{ENTER}")
                    Tiempo(20)
                    Tecla("+{TAB}")
                    Tiempo(9)
                    Tecla(ruta)
                    Tiempo(9)
                    Tecla("{TAB}")
                    Tiempo(9)
                    Tecla(Sucr2)
                    Tiempo(9)
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

                    Console.WriteLine("Iniciamos Extracciòn Mano de Obra Acumulado Sucro")
                    Dim fab4 As FileInfo = New FileInfo(ruta + Sucr2)
                    data = New DataTable()

                    If fab4.Exists Then
                        Dim stringarry As String() = System.IO.File.ReadAllLines(ruta + Sucr2, Encoding.[Default])

                        If stringarry.Length > 0 Then
                            Dim cc As Integer = 0
                            Dim arr_ = stringarry(9).Substring(1, stringarry(9).Length - 2).Split("|")
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

                            data.AcceptChanges()

                            Dim dr As System.Data.DataRow = Nothing

                            For i As Integer = 11 To stringarry.Length - 2
                                stringarry(i) = stringarry(i).Replace("S/N|", "S/N")
                                Dim dat = stringarry(i).Substring(1, stringarry(i).Length - 2).Split("|")
                                dr = data.NewRow()

                                For y As Integer = 0 To dat.Length - 1
                                    dr(y) = dat(y).Trim()
                                Next

                                data.Rows.Add(dr)
                            Next

                            data.AcceptChanges()

                            query = "DELETE FROM PlanMo_New.dbo.VENTAS_GR55_MO_COSTO Where Tipo = 'SUC2' AND Periodo = 12 AND Anio = YEAR(GETDATE()) - 1 ;" & vbCrLf

                            Dim cccc As Integer = 0

                            For Each item As DataRow In data.Rows

                                If item.ItemArray(0).ToString() <> String.Empty Then
                                    query += "INSERT INTO PlanMo_New.dbo.VENTAS_GR55_MO_COSTO VALUES ('SUC2',"

                                    For i As Integer = 0 To item.ItemArray.Length - 1

                                        If i = 1 OrElse i = 2 OrElse i = 13 Then

                                            If item.ItemArray(i).ToString().Trim() <> String.Empty Then
                                                query += "'" & DateTime.Parse(item.ItemArray(i).ToString().Replace(".", "/")).ToString("yyyy-MM-dd") & "',"
                                            Else
                                                query += "'',"
                                            End If

                                        Else

                                            If i = 9 OrElse i = 11 Then

                                                If item.ItemArray(i).ToString().Trim() <> String.Empty Then
                                                    Dim val As String = item.ItemArray(i).ToString().Replace(",", "")
                                                    Dim dat As String = If(val.Contains("-"), val.Substring(0, val.Length - 1), "-" & val) * -1
                                                    query += "'" & dat & "',"
                                                Else
                                                    query += "'0',"
                                                End If
                                            Else

                                                If item.ItemArray(i).ToString().Trim() <> String.Empty Then
                                                    query += "'" & item.ItemArray(i).ToString() & "',"
                                                Else
                                                    query += "'0',"
                                                End If
                                            End If
                                        End If
                                    Next

                                    query += ");" & vbCrLf
                                End If

                                query = query.Replace(",);", ");")
                                cccc += 1
                                Console.WriteLine("Leyendo Informacion: " & cccc & " rows")
                            Next

                            conexion = New SqlConnection()
                            conexion.ConnectionString = connString
                            conexion.Open()
                            myCommand = New SqlCommand(query, conexion)
                            myCommand.CommandTimeout = 3000
                            myCommand.ExecuteNonQuery()
                            myCommand.Dispose()
                            conexion.Close()

                        End If

                    End If

#End Region

#Region "Descarga de Informaciòn Gast. Operativos MO - Bioenergia"

                    Console.WriteLine("Iniciamos Extracciòn de Informaciòn Gast. Operativos MO")

                    Session.findById("wnd[0]").maximize
                    Session.findById("wnd[0]/tbar[0]/okcd").text = "GR55"
                    Session.findById("wnd[0]/tbar[0]/btn[0]").press
                    Session.findById("wnd[0]/usr/ctxtRGRWJ-JOB").text = "BC21"
                    Session.findById("wnd[0]/usr/ctxtRGRWJ-JOB").caretPosition = 2
                    Session.findById("wnd[0]/tbar[0]/btn[0]").press
                    Session.findById("wnd[0]/tbar[1]/btn[8]").press
                    Session.findById("wnd[0]/usr/ctxt$0BRYEAR").text = thisYear '"2022"
                    Session.findById("wnd[0]/usr/ctxt$GR00004").text = "12"
                    Session.findById("wnd[0]/usr/ctxt$GR00004").setFocus
                    Session.findById("wnd[0]/usr/ctxt$GR00004").caretPosition = 3
                    Session.findById("wnd[0]/tbar[1]/btn[8]").press
                    Session.findById("wnd[0]/mbar/menu[2]/menu[0]").select
                    Session.findById("wnd[1]/tbar[0]/btn[9]").press
                    Session.findById("wnd[2]/usr/txtRSYSF-STRING").text = "G. FAbrica"
                    Session.findById("wnd[2]/usr/txtRSYSF-STRING").caretPosition = 6
                    Session.findById("wnd[2]/tbar[0]/btn[0]").press
                    Session.findById("wnd[3]/usr/lbl[2,2]").setFocus
                    Session.findById("wnd[3]/usr/lbl[2,2]").caretPosition = 0
                    Session.findById("wnd[3]").sendVKey(2)
                    Session.findById("wnd[1]/tbar[0]/btn[0]").press
                    Session.findById("wnd[0]/tbar[1]/btn[32]").press
                    Session.findById("wnd[1]/tbar[0]/btn[9]").press
                    Session.findById("wnd[2]/usr/txtRSYSF-STRING").text = mesdet '"TOTAL"
                    Session.findById("wnd[2]/usr/txtRSYSF-STRING").caretPosition = 5
                    Session.findById("wnd[2]/tbar[0]/btn[0]").press
                    Session.findById("wnd[3]/usr/lbl[2,2]").setFocus
                    Session.findById("wnd[3]/usr/lbl[2,2]").caretPosition = 2
                    Session.findById("wnd[3]").sendVKey(2)
                    Session.findById("wnd[1]/tbar[0]/btn[0]").press
                    Session.findById("wnd[0]/mbar/menu[2]/menu[6]").select
                    Session.findById("wnd[1]/usr/txtRSYSF-STRING").text = "Salario"
                    Session.findById("wnd[1]/usr/txtRSYSF-STRING").caretPosition = 6
                    Session.findById("wnd[1]/tbar[0]/btn[0]").press
                    Session.findById("wnd[2]/usr/lbl[8,2]").setFocus
                    Session.findById("wnd[2]/usr/lbl[8,2]").caretPosition = 2
                    Session.findById("wnd[2]").sendVKey(2)
                    Session.findById("wnd[0]/usr/lbl[50,8]").setFocus
                    Session.findById("wnd[0]/usr/lbl[50,8]").caretPosition = 4
                    Session.findById("wnd[0]").sendVKey(2)
                    Session.findById("wnd[1]/usr/lbl[1,1]").caretPosition = 2
                    Session.findById("wnd[1]/tbar[0]/btn[2]").press
                    Tiempo(5)
                    Session.findById("wnd[0]/mbar/menu[4]/menu[2]/menu[1]").select
                    Session.findById("wnd[1]/usr/ssubD0500_SUBSCREEN:SAPLSLVC_DIALOG:0501/cmbG51_USPEC_LBOX").setFocus
                    Tiempo(4)
                    Session.findById("wnd[1]/usr/ssubD0500_SUBSCREEN:SAPLSLVC_DIALOG:0501/cmbG51_USPEC_LBOX").key = "X"
                    Session.findById("wnd[1]/usr/ssubD0500_SUBSCREEN:SAPLSLVC_DIALOG:0501/cntlG51_CONTAINER/shellcont/shell").currentCellRow = 1
                    Tiempo(4)
                    Session.findById("wnd[1]/usr/ssubD0500_SUBSCREEN:SAPLSLVC_DIALOG:0501/cntlG51_CONTAINER/shellcont/shell").selectedRows = "1"
                    Session.findById("wnd[1]/usr/ssubD0500_SUBSCREEN:SAPLSLVC_DIALOG:0501/cntlG51_CONTAINER/shellcont/shell").clickCurrentCell

#Region "Guardado de Fichero Txt"
                    Tiempo(17)
                    Tecla("{F9}")
                    Tiempo(20)
                    Tecla("{TAB}")
                    Tiempo(6)
                    Tecla("{ENTER}")
                    Tiempo(20)
                    Tecla("+{TAB}")
                    Tiempo(9)
                    Tecla(ruta)
                    Tiempo(9)
                    Tecla("{TAB}")
                    Tiempo(9)
                    Tecla(Bio1)
                    Tiempo(9)
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

                    Session.findById("wnd[0]/tbar[0]/btn[3]").press

                    Console.WriteLine("Iniciamos Extracciòn Mano de Obra Operativo Bio")
                    Dim fab5 As FileInfo = New FileInfo(ruta + Bio1)
                    data = New DataTable()

                    If fab5.Exists Then
                        Dim stringarry As String() = System.IO.File.ReadAllLines(ruta + Bio1, Encoding.[Default])

                        If stringarry.Length > 0 Then
                            Dim cc As Integer = 0
                            Dim arr_ = stringarry(9).Substring(1, stringarry(9).Length - 2).Split("|")
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

                            data.AcceptChanges()

                            Dim dr As System.Data.DataRow = Nothing

                            For i As Integer = 11 To stringarry.Length - 2
                                stringarry(i) = stringarry(i).Replace("S/N|", "S/N")
                                Dim dat = stringarry(i).Substring(1, stringarry(i).Length - 2).Split("|")
                                dr = data.NewRow()

                                For y As Integer = 0 To dat.Length - 1
                                    dr(y) = dat(y).Trim()
                                Next

                                data.Rows.Add(dr)
                            Next

                            data.AcceptChanges()

                            query = "DELETE FROM PlanMo_New.dbo.VENTAS_GR55_MO_GASTO Where Tipo = 'BIO1' AND Periodo = 12 AND Anio = YEAR(GETDATE()) - 1 ;" & vbCrLf

                            Dim cccc As Integer = 0

                            For Each item As DataRow In data.Rows

                                If item.ItemArray(0).ToString() <> String.Empty Then
                                    query += "INSERT INTO PlanMo_New.dbo.VENTAS_GR55_MO_GASTO VALUES ('BIO1',"

                                    For i As Integer = 0 To item.ItemArray.Length - 1

                                        If i = 1 OrElse i = 2 OrElse i = 13 Then

                                            If item.ItemArray(i).ToString().Trim() <> String.Empty Then
                                                query += "'" & DateTime.Parse(item.ItemArray(i).ToString().Replace(".", "/")).ToString("yyyy-MM-dd") & "',"
                                            Else
                                                query += "'',"
                                            End If

                                        Else

                                            If i = 9 OrElse i = 11 Then

                                                If item.ItemArray(i).ToString().Trim() <> String.Empty Then
                                                    Dim val As String = item.ItemArray(i).ToString().Replace(",", "")
                                                    Dim dat As String = If(val.Contains("-"), val.Substring(0, val.Length - 1), "-" & val) * -1
                                                    query += "'" & dat & "',"
                                                Else
                                                    query += "'0',"
                                                End If
                                            Else

                                                If item.ItemArray(i).ToString().Trim() <> String.Empty Then
                                                    query += "'" & item.ItemArray(i).ToString() & "',"
                                                Else
                                                    query += "'0',"
                                                End If
                                            End If
                                        End If
                                    Next

                                    query += ");" & vbCrLf
                                End If

                                query = query.Replace(",);", ");")
                                cccc += 1
                                Console.WriteLine("Leyendo Informacion: " & cccc & " rows")
                            Next

                            conexion = New SqlConnection()
                            conexion.ConnectionString = connString
                            conexion.Open()
                            myCommand = New SqlCommand(query, conexion)
                            myCommand.CommandTimeout = 3000
                            myCommand.ExecuteNonQuery()
                            myCommand.Dispose()
                            conexion.Close()

                        End If

                    End If

#End Region

#Region "Descarga de Informaciòn Costo. Acumulado MO - Bioenergia"

                    Console.WriteLine("Iniciamos Extracciòn de Informaciòn Costo. Acumulado MO")

                    Session.findById("wnd[0]/mbar/menu[2]/menu[0]").select
                    Session.findById("wnd[1]/tbar[0]/btn[9]").press
                    Session.findById("wnd[2]/usr/txtRSYSF-STRING").text = "G y P Mes a Mes"
                    Session.findById("wnd[2]/usr/txtRSYSF-STRING").caretPosition = 15
                    Session.findById("wnd[2]/tbar[0]/btn[0]").press
                    Session.findById("wnd[3]/usr/lbl[2,2]").setFocus
                    Session.findById("wnd[3]/usr/lbl[2,2]").caretPosition = 4
                    Session.findById("wnd[3]").sendVKey(2)
                    Session.findById("wnd[1]/tbar[0]/btn[0]").press
                    Session.findById("wnd[0]/tbar[1]/btn[32]").press
                    Session.findById("wnd[1]/tbar[0]/btn[9]").press
                    Session.findById("wnd[2]/usr/txtRSYSF-STRING").text = mesdet '"Ene"
                    Session.findById("wnd[2]/usr/txtRSYSF-STRING").caretPosition = 3
                    Session.findById("wnd[2]/tbar[0]/btn[0]").press
                    Session.findById("wnd[3]/usr/lbl[2,2]").setFocus
                    Session.findById("wnd[3]/usr/lbl[2,2]").caretPosition = 1
                    Session.findById("wnd[3]").sendVKey(2)
                    Session.findById("wnd[1]/tbar[0]/btn[0]").press
                    Session.findById("wnd[0]/usr/lbl[5,4]").setFocus
                    Session.findById("wnd[0]/usr/lbl[5,4]").caretPosition = 8
                    Session.findById("wnd[0]/mbar/menu[2]/menu[6]").select
                    Session.findById("wnd[1]/usr/txtRSYSF-STRING").text = "COSTO DE FABRICA"
                    Session.findById("wnd[1]/usr/txtRSYSF-STRING").caretPosition = 16
                    Session.findById("wnd[1]/tbar[0]/btn[0]").press
                    Session.findById("wnd[2]/usr/lbl[13,2]").setFocus
                    Session.findById("wnd[2]/usr/lbl[13,2]").caretPosition = 5
                    Session.findById("wnd[2]").sendVKey(2)
                    Session.findById("wnd[0]/usr/lbl[52,11]").setFocus
                    Session.findById("wnd[0]/usr/lbl[52,11]").caretPosition = 5
                    Session.findById("wnd[0]").sendVKey(2)
                    Session.findById("wnd[1]/usr/lbl[1,1]").caretPosition = 1
                    Session.findById("wnd[1]/tbar[0]/btn[2]").press
                    Tiempo(5)
                    Session.findById("wnd[0]/mbar/menu[4]/menu[2]/menu[1]").select
                    Session.findById("wnd[1]/usr/ssubD0500_SUBSCREEN:SAPLSLVC_DIALOG:0501/cmbG51_USPEC_LBOX").setFocus
                    Tiempo(4)
                    Session.findById("wnd[1]/usr/ssubD0500_SUBSCREEN:SAPLSLVC_DIALOG:0501/cmbG51_USPEC_LBOX").key = "X"
                    Session.findById("wnd[1]/usr/ssubD0500_SUBSCREEN:SAPLSLVC_DIALOG:0501/cntlG51_CONTAINER/shellcont/shell").currentCellRow = 1
                    Tiempo(4)
                    Session.findById("wnd[1]/usr/ssubD0500_SUBSCREEN:SAPLSLVC_DIALOG:0501/cntlG51_CONTAINER/shellcont/shell").selectedRows = "1"
                    Session.findById("wnd[1]/usr/ssubD0500_SUBSCREEN:SAPLSLVC_DIALOG:0501/cntlG51_CONTAINER/shellcont/shell").clickCurrentCell

#Region "Guardado de Fichero Txt"
                    Tiempo(17)
                    Tecla("{F9}")
                    Tiempo(20)
                    Tecla("{TAB}")
                    Tiempo(6)
                    Tecla("{ENTER}")
                    Tiempo(20)
                    Tecla("+{TAB}")
                    Tiempo(9)
                    Tecla(ruta)
                    Tiempo(9)
                    Tecla("{TAB}")
                    Tiempo(9)
                    Tecla(Bio2)
                    Tiempo(9)
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

                    Session.findById("wnd[0]/tbar[0]/btn[3]").press
                    Tiempo(8)
                    Session.findById("wnd[0]").close
                    Session.findById("wnd[1]/usr/btnSPOP-OPTION1").press

                    Tiempo(8)
                    AppActivate("SAP Logon 760")
                    Tiempo(8)
                    Tecla("%{F4}")
                    Tiempo(8)

                    Console.WriteLine("Iniciamos Extracciòn Mano de Obra Acumulado Sucro")
                    Dim fab6 As FileInfo = New FileInfo(ruta + Bio2)
                    data = New DataTable()

                    If fab6.Exists Then
                        Dim stringarry As String() = System.IO.File.ReadAllLines(ruta + Bio2, Encoding.[Default])

                        If stringarry.Length > 0 Then
                            Dim cc As Integer = 0
                            Dim arr_ = stringarry(9).Substring(1, stringarry(9).Length - 2).Split("|")
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

                            data.AcceptChanges()

                            Dim dr As System.Data.DataRow = Nothing

                            For i As Integer = 11 To stringarry.Length - 2
                                stringarry(i) = stringarry(i).Replace("S/N|", "S/N")
                                Dim dat = stringarry(i).Substring(1, stringarry(i).Length - 2).Split("|")
                                dr = data.NewRow()

                                For y As Integer = 0 To dat.Length - 1
                                    dr(y) = dat(y).Trim()
                                Next

                                data.Rows.Add(dr)
                            Next

                            data.AcceptChanges()

                            query = "DELETE FROM PlanMo_New.dbo.VENTAS_GR55_MO_COSTO Where Tipo = 'BIO2' AND Periodo = 12 AND Anio = YEAR(GETDATE()) - 1 ;" & vbCrLf

                            Dim cccc As Integer = 0

                            For Each item As DataRow In data.Rows

                                If item.ItemArray(0).ToString() <> String.Empty Then
                                    query += "INSERT INTO PlanMo_New.dbo.VENTAS_GR55_MO_COSTO VALUES ('BIO2',"

                                    For i As Integer = 0 To item.ItemArray.Length - 1

                                        If i = 1 OrElse i = 2 OrElse i = 13 Then

                                            If item.ItemArray(i).ToString().Trim() <> String.Empty Then
                                                query += "'" & DateTime.Parse(item.ItemArray(i).ToString().Replace(".", "/")).ToString("yyyy-MM-dd") & "',"
                                            Else
                                                query += "'',"
                                            End If

                                        Else

                                            If i = 9 OrElse i = 11 Then

                                                If item.ItemArray(i).ToString().Trim() <> String.Empty Then
                                                    Dim val As String = item.ItemArray(i).ToString().Replace(",", "")
                                                    Dim dat As String = If(val.Contains("-"), val.Substring(0, val.Length - 1), "-" & val) * -1
                                                    query += "'" & dat & "',"
                                                Else
                                                    query += "'0',"
                                                End If
                                            Else

                                                If item.ItemArray(i).ToString().Trim() <> String.Empty Then
                                                    query += "'" & item.ItemArray(i).ToString() & "',"
                                                Else
                                                    query += "'0',"
                                                End If
                                            End If
                                        End If
                                    Next

                                    query += ");" & vbCrLf
                                End If

                                query = query.Replace(",);", ");")
                                cccc += 1
                                Console.WriteLine("Leyendo Informacion: " & cccc & " rows")
                            Next

                            conexion = New SqlConnection()
                            conexion.ConnectionString = connString
                            conexion.Open()
                            myCommand = New SqlCommand(query, conexion)
                            myCommand.CommandTimeout = 3000
                            myCommand.ExecuteNonQuery()
                            myCommand.Dispose()
                            conexion.Close()

                        End If

                    End If

#End Region

                End If
#End Region

                Console.WriteLine("Proceso finalizado..!!")

#Region "Correo de Sincronizaciòn Correcta"
                '=== Correo de verificacion

                Dim fechaact As DateTime = DateTime.Now
                Dim msg As MailMessage = New MailMessage()
                msg.From = New MailAddress("Sistemas TI<sistemasti@agricolachira.com.pe>")
                msg.[To].Add("Junior Alexander Hidalgo Socola<jhidalgos@agricolachira.com.pe>")
                msg.Subject = "Sincronizacion Correcta - Informaciòn de Mano de Obra - GR55"
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
            msg.Subject = "Sincronizacion Fallida - Informaciòn de Mano de Obra - GR55"
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

    Sub Tiempo(ByVal tiempo As Integer)
        Thread.Sleep(tiempo * 1000)
    End Sub

    Sub Tecla(ByVal tecla As String)
        SendKeys.SendWait(tecla)
    End Sub

End Module
