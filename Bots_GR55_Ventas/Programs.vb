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
            Dim Usua As String = String.Empty, Pass As String = String.Empty, Ruta As String = String.Empty, Arc1 As String = String.Empty, Arc2 As String = String.Empty, Prog As String = String.Empty, Serv As String = String.Empty, Clie As String = String.Empty, Mate As String = String.Empty, Pedi As String = String.Empty, Stoc As String = String.Empty, Material As String = String.Empty, Clientes As String = String.Empty, Mess As String = String.Empty, MateGR55 As String = String.Empty, Serv2 As String = String.Empty
            Dim fa As FileInfo = New FileInfo(AppDomain.CurrentDomain.BaseDirectory & "directorio.txt")

            If fa.Exists Then
                Dim opeIO As OpeIO = New OpeIO(AppDomain.CurrentDomain.BaseDirectory & "/directorio.txt")
                Usua = opeIO.ReadLineByNum(1)
                Pass = opeIO.ReadLineByNum(2)
                Ruta = opeIO.ReadLineByNum(3)
                Arc1 = opeIO.ReadLineByNum(4)
                Arc2 = opeIO.ReadLineByNum(5)
                Prog = opeIO.ReadLineByNum(6)
                Serv = opeIO.ReadLineByNum(7)
                Clie = opeIO.ReadLineByNum(8)
                Mate = opeIO.ReadLineByNum(9)
                Pedi = opeIO.ReadLineByNum(10)
                Stoc = opeIO.ReadLineByNum(11)
                Material = opeIO.ReadLineByNum(12)
                Clientes = opeIO.ReadLineByNum(13)
                Mess = opeIO.ReadLineByNum(14)
                MateGR55 = opeIO.ReadLineByNum(15)
                Serv2 = opeIO.ReadLineByNum(16)

            End If
            Dim connString As String = "Data Source=" & Serv & ";Database=Asistencia;uid=ctcuser;pwd=ctcuser;"
            Dim connString2 As String = "Data Source=" & Serv2 & ";Database=PlanMo_New;uid=ctcuser;pwd=ctcuser;"

            If DateTime.Now.Day > 1 Then
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


#Region "Extraciòn de Materiales de Base de Datos"
                Console.WriteLine("Iniciamos Extracciòn Maestro de materiales de tabla MAESGEN")
                data = New DataTable()
                Dim da As SqlDataAdapter = New SqlDataAdapter()
                query = "SELECT clavemaesgen FROM maesgen WHERE idmaesgen = '186';"
                conexion = New SqlConnection()
                conexion.ConnectionString = connString
                conexion.Open()
                da.SelectCommand = New SqlCommand(query, conexion)
                da.Fill(data)
                fa = New FileInfo("D:/" & Material)

                If fa.Exists Then
                    File.Delete("D:/" & Material)
                End If

                Dim filename As String = "D:/" & Material
                Dim archivo_ide As StreamWriter = File.CreateText(filename)
                archivo_ide.Close()

                For Each row As DataRow In data.Rows
                    Dim opeIO As OpeIO = New OpeIO(filename)
                    opeIO.WriteNWL("*" & row("clavemaesgen").ToString() & "*")
                Next

#End Region

#Region "Descarga de Informaciòn Azucar"

                Console.WriteLine("Iniciamos Extracciòn de Informaciòn de Azucar")

                Session.findById("wnd[0]").maximize
                Session.findById("wnd[0]/tbar[0]/okcd").text = "GR55"
                Session.findById("wnd[0]/tbar[0]/btn[0]").press
                Session.findById("wnd[0]/usr/ctxtRGRWJ-JOB").text = "AC21"
                Session.findById("wnd[0]/usr/ctxtRGRWJ-JOB").caretPosition = 4
                Session.findById("wnd[0]/tbar[0]/btn[0]").press
                Session.findById("wnd[0]/tbar[1]/btn[8]").press
                Tiempo(5)
                Session.findById("wnd[0]/tbar[0]/btn[0]").press
                Session.findById("wnd[0]/tbar[1]/btn[8]").press
                Session.findById("wnd[0]/mbar/menu[2]/menu[0]").select
                Session.findById("wnd[1]/tbar[0]/btn[9]").press
                Session.findById("wnd[2]/usr/txtRSYSF-STRING").text = "P MES ACUMULADO"
                Session.findById("wnd[2]/usr/txtRSYSF-STRING").caretPosition = 15
                Session.findById("wnd[2]/tbar[0]/btn[0]").press
                Session.findById("wnd[3]/usr/lbl[1,2]").setFocus
                Session.findById("wnd[3]/usr/lbl[1,2]").caretPosition = 1
                Session.findById("wnd[3]/tbar[0]/btn[2]").press
                Session.findById("wnd[1]/tbar[0]/btn[0]").press
                Session.findById("wnd[0]/usr/lbl[65,7]").setFocus
                Session.findById("wnd[0]/usr/lbl[65,7]").caretPosition = 4
                Session.findById("wnd[0]").sendVKey(2)
                Session.findById("wnd[1]/usr/lbl[1,1]").caretPosition = 1
                Session.findById("wnd[1]/tbar[0]/btn[2]").press
                Session.findById("wnd[0]/tbar[0]/btn[0]").press
                Tiempo(3)
                Session.findById("wnd[0]/mbar/menu[4]/menu[2]/menu[1]").select
                Tiempo(3)
                Session.findById("wnd[1]/usr/ssubD0500_SUBSCREEN:SAPLSLVC_DIALOG:0501/cmbG51_USPEC_LBOX").setFocus
                Tiempo(2)
                Session.findById("wnd[1]/usr/ssubD0500_SUBSCREEN:SAPLSLVC_DIALOG:0501/cmbG51_USPEC_LBOX").key = "X"
                Tiempo(2)
                Session.findById("wnd[1]/tbar[0]/btn[0]").press

#Region "Filtrar Materiales"
                Tiempo(17)
                Tecla("^+{F2}")
                Tiempo(10)
                Tecla("+{TAB}")
                Tiempo(4)
                Tecla("+{TAB}")
                Tiempo(3)
                Tecla("+{TAB}")
                Tiempo(3)
                Tecla("+{TAB}")
                Tiempo(3)
                Tecla("+{TAB}")
                Tiempo(3)
                Tecla("+{TAB}")
                Tiempo(3)
                Tecla("+{F10}")
                Tiempo(3)
                Tecla("{DOWN}")
                Tiempo(3)
                Tecla("{ENTER}")
                Tiempo(4)
                Tecla("+{TAB}")
                Tiempo(4)
                Tecla("Material")
                Tiempo(6)
                Tecla("{ENTER}")
                Tiempo(4)
                Tecla("{F12}")
                Tiempo(4)
                Tecla("{F2}")
                Tiempo(4)
                Tecla("^{TAB}")
                Tiempo(3)
                Tecla("{TAB}")
                Tiempo(3)
                Tecla("{TAB}")
                Tiempo(3)
                Tecla("{ENTER}")
                Tiempo(3)
                Tecla("{TAB}")
                Tiempo(3)
                Tecla("{TAB}")
                Tiempo(3)
                Tecla("{ENTER}")
                Tiempo(4)
                Tecla("+{F4}")
                Tiempo(4)
                Tecla("+{F11}")
                Tiempo(4)
                Tecla("{ENTER}")
                Tiempo(5)
                Tecla("D:")
                Tiempo(6)
                Tecla("{ENTER}")
                Tiempo(5)
                Tecla(Material)
                Tiempo(6)
                Tecla("{ENTER}")
                Tiempo(4)
                Tecla("+{TAB}")
                Tiempo(4)
                Tecla("{ENTER}")
                Tiempo(6)
                Tecla("{F8}")
                Tiempo(5)
                Tecla("{ENTER}")

#End Region

#Region "Guardado de Fichero Txt"
                Tiempo(17)
                Tecla("{F9}")
                Tiempo(6)
                Tecla("{TAB}")
                Tiempo(6)
                Tecla("{ENTER}")
                Tiempo(9)
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
                Tiempo(7)
                Tecla("+{TAB}")
                Tiempo(7)
                Tecla("{ENTER}")
                Tiempo(8)
#End Region


                Session.findById("wnd[0]/tbar[0]/okcd").text = "/N"
                Session.findById("wnd[0]/tbar[0]/btn[0]").press

                Console.WriteLine("Iniciamos Extracciòn de Azucar")
                Dim fab As FileInfo = New FileInfo(Ruta + Arc1)
                data = New DataTable()

                If fab.Exists Then
                    Dim stringarry As String() = System.IO.File.ReadAllLines(Ruta + Arc1, Encoding.[Default])

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

                        For i As Integer = 11 To stringarry.Length - 3 - 1
                            stringarry(i) = stringarry(i).Replace("S/N|", "S/N")
                            Dim dat = stringarry(i).Substring(1, stringarry(i).Length - 2).Split("|")
                            dr = data.NewRow()

                            For y As Integer = 0 To dat.Length - 1
                                dr(y) = dat(y).Trim()
                            Next

                            data.Rows.Add(dr)
                        Next

                        data.AcceptChanges()

                        query = "DELETE FROM PlanMo_New.dbo.VENTAS_GR55 WHERE Soc='153' AND Anio = YEAR(GETDATE());" & vbCrLf
                        'query = "DELETE FROM PlanMo_New.dbo.VENTAS_GR55 WHERE Anio=YEAR(GETDATE()) AND Periodo=MONTH(GETDATE()) AND Soc='153';" & vbCrLf
                        Dim cccc As Integer = 0

                        For Each item As DataRow In data.Rows

                            If item.ItemArray(1).ToString() <> String.Empty Then
                                query += "INSERT INTO PlanMo_New.dbo.VENTAS_GR55 VALUES ("

                                For i As Integer = 0 To item.ItemArray.Length - 1

                                    If i = 11 OrElse i = 12 Then

                                        If item.ItemArray(i).ToString().Trim() <> String.Empty Then
                                            query += "'" & DateTime.Parse(item.ItemArray(i).ToString().Replace(".", "/")).ToString("yyyy-MM-dd") & "',"
                                        Else
                                            query += "'',"
                                        End If
                                    Else

                                        If i = 10 OrElse i = 15 OrElse i = 17 Then

                                            If item.ItemArray(i).ToString().Trim() <> String.Empty Then
                                                Dim val As String = item.ItemArray(i).ToString().Replace(",", "")
                                                Dim dat As String = If(val.Contains("-"), val.Substring(0, val.Length - 1), "-" & val)
                                                query += "'" & dat & "',"
                                            Else
                                                query += "'0',"
                                            End If
                                        Else

                                            If item.ItemArray(i).ToString().Trim() <> String.Empty Then
                                                query += "'" & item.ItemArray(i).ToString() & "',"
                                            Else
                                                query += "'',"
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
                        conexion.ConnectionString = connString2
                        conexion.Open()
                        myCommand = New SqlCommand(query, conexion)
                        myCommand.CommandTimeout = 900
                        myCommand.ExecuteNonQuery()
                        myCommand.Dispose()
                        conexion.Close()
                    End If
                End If


#End Region

#Region "Descarga de Informaciòn Alcohol"

                Console.WriteLine("Iniciamos Extracciòn de Informaciòn de Alcohol")

                Session.findById("wnd[0]").maximize
                Session.findById("wnd[0]/tbar[0]/okcd").text = "GR55"
                Session.findById("wnd[0]/tbar[0]/btn[0]").press
                Session.findById("wnd[0]/usr/ctxtRGRWJ-JOB").text = "SC21"
                Session.findById("wnd[0]/usr/ctxtRGRWJ-JOB").caretPosition = 4
                Session.findById("wnd[0]/tbar[0]/btn[0]").press
                Session.findById("wnd[0]/tbar[1]/btn[8]").press
                Tiempo(5)
                Session.findById("wnd[0]/tbar[0]/btn[0]").press
                Session.findById("wnd[0]/tbar[1]/btn[8]").press
                Session.findById("wnd[0]/mbar/menu[2]/menu[0]").select
                Session.findById("wnd[1]/tbar[0]/btn[9]").press
                Session.findById("wnd[2]/usr/txtRSYSF-STRING").text = "P MES ACUMULADO"
                Session.findById("wnd[2]/usr/txtRSYSF-STRING").caretPosition = 15
                Session.findById("wnd[2]/tbar[0]/btn[0]").press
                Session.findById("wnd[3]/usr/lbl[1,2]").setFocus
                Session.findById("wnd[3]/usr/lbl[1,2]").caretPosition = 1
                Session.findById("wnd[3]/tbar[0]/btn[2]").press
                Session.findById("wnd[1]/tbar[0]/btn[0]").press
                Session.findById("wnd[0]/usr/lbl[68,7]").setFocus
                Session.findById("wnd[0]/usr/lbl[68,7]").caretPosition = 4
                Session.findById("wnd[0]").sendVKey(2)
                Session.findById("wnd[1]/usr/lbl[1,1]").caretPosition = 1
                Session.findById("wnd[1]/tbar[0]/btn[2]").press
                Session.findById("wnd[0]/tbar[0]/btn[0]").press
                Tiempo(3)
                Session.findById("wnd[0]/mbar/menu[4]/menu[2]/menu[1]").select
                Tiempo(3)
                Session.findById("wnd[1]/usr/ssubD0500_SUBSCREEN:SAPLSLVC_DIALOG:0501/cmbG51_USPEC_LBOX").setFocus
                Tiempo(2)
                Session.findById("wnd[1]/usr/ssubD0500_SUBSCREEN:SAPLSLVC_DIALOG:0501/cmbG51_USPEC_LBOX").key = "X"
                Tiempo(2)
                Session.findById("wnd[1]/tbar[0]/btn[0]").press


#Region "Filtrar Materiales"
                Tiempo(17)
                Tecla("^+{F2}")
                Tiempo(10)
                Tecla("+{TAB}")
                Tiempo(4)
                Tecla("+{TAB}")
                Tiempo(3)
                Tecla("+{TAB}")
                Tiempo(3)
                Tecla("+{TAB}")
                Tiempo(3)
                Tecla("+{TAB}")
                Tiempo(3)
                Tecla("+{TAB}")
                Tiempo(3)
                Tecla("+{F10}")
                Tiempo(3)
                Tecla("{DOWN}")
                Tiempo(3)
                Tecla("{ENTER}")
                Tiempo(4)
                Tecla("+{TAB}")
                Tiempo(4)
                Tecla("Material")
                Tiempo(6)
                Tecla("{ENTER}")
                Tiempo(4)
                Tecla("{F12}")
                Tiempo(4)
                Tecla("{F2}")
                Tiempo(4)
                Tecla("^{TAB}")
                Tiempo(3)
                Tecla("{TAB}")
                Tiempo(3)
                Tecla("{TAB}")
                Tiempo(3)
                Tecla("{ENTER}")
                Tiempo(4)
                Tecla("{TAB}")
                Tiempo(3)
                Tecla("{TAB}")
                Tiempo(3)
                Tecla("{ENTER}")
                Tiempo(4)
                Tecla("+{F4}")
                Tiempo(4)
                Tecla("+{F11}")
                Tiempo(4)
                Tecla("{ENTER}")
                Tiempo(5)
                Tecla("D:")
                Tiempo(6)
                Tecla("{ENTER}")
                Tiempo(5)
                Tecla(Material)
                Tiempo(6)
                Tecla("{ENTER}")
                Tiempo(4)
                Tecla("+{TAB}")
                Tiempo(4)
                Tecla("{ENTER}")
                Tiempo(6)
                Tecla("{F8}")
                Tiempo(5)
                Tecla("{ENTER}")

#End Region

#Region "Guardado de Fichero Txt"
                Tiempo(17)
                Tecla("{F9}")
                Tiempo(6)
                Tecla("{TAB}")
                Tiempo(6)
                Tecla("{ENTER}")
                Tiempo(9)
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
                Tiempo(7)
                Tecla("+{TAB}")
                Tiempo(7)
                Tecla("{ENTER}")
                Tiempo(8)
#End Region


                Session.findById("wnd[0]/tbar[0]/okcd").text = "/N"
                Session.findById("wnd[0]/tbar[0]/btn[0]").press

                Console.WriteLine("Iniciamos Extracciòn de Alcohol")
                Dim fab2 As FileInfo = New FileInfo(Ruta + Arc2)
                data = New DataTable()

                If fab2.Exists Then
                    Dim stringarry As String() = System.IO.File.ReadAllLines(Ruta + Arc2, Encoding.[Default])

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

                        For i As Integer = 11 To stringarry.Length - 3 - 1
                            stringarry(i) = stringarry(i).Replace("S/N|", "S/N")
                            Dim dat = stringarry(i).Substring(1, stringarry(i).Length - 2).Split("|")
                            dr = data.NewRow()

                            For y As Integer = 0 To dat.Length - 1
                                dr(y) = dat(y).Trim()
                            Next

                            data.Rows.Add(dr)
                        Next

                        data.AcceptChanges()

                        query = "DELETE FROM PlanMo_New.dbo.VENTAS_GR55 WHERE Soc='157' AND Anio = YEAR(GETDATE());" & vbCrLf
                        'query = "DELETE FROM PlanMo_New.dbo.VENTAS_GR55 WHERE Anio=YEAR(GETDATE()) AND Periodo=MONTH(GETDATE()) AND Soc='157';" & vbCrLf
                        Dim cccc As Integer = 0

                        For Each item As DataRow In data.Rows

                            If item.ItemArray(1).ToString() <> String.Empty Then
                                query += "INSERT INTO PlanMo_New.dbo.VENTAS_GR55 VALUES ("

                                For i As Integer = 0 To item.ItemArray.Length - 1

                                    If i = 11 OrElse i = 12 Then

                                        If item.ItemArray(i).ToString().Trim() <> String.Empty Then
                                            query += "'" & DateTime.Parse(item.ItemArray(i).ToString().Replace(".", "/")).ToString("yyyy-MM-dd") & "',"
                                        Else
                                            query += "'',"
                                        End If
                                    Else

                                        If i = 10 OrElse i = 15 OrElse i = 17 Then

                                            If item.ItemArray(i).ToString().Trim() <> String.Empty Then
                                                Dim val As String = item.ItemArray(i).ToString().Replace(",", "")
                                                Dim dat As String = If(val.Contains("-"), val.Substring(0, val.Length - 1), "-" & val)
                                                query += "'" & dat & "',"
                                            Else
                                                query += "'0',"
                                            End If
                                        Else

                                            If item.ItemArray(i).ToString().Trim() <> String.Empty Then
                                                query += "'" & item.ItemArray(i).ToString() & "',"
                                            Else
                                                query += "'',"
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
                        conexion.ConnectionString = connString2
                        conexion.Open()
                        myCommand = New SqlCommand(query, conexion)
                        myCommand.CommandTimeout = 900
                        myCommand.ExecuteNonQuery()
                        myCommand.Dispose()
                        conexion.Close()
                    End If
                End If


#End Region

#Region "Extraciòn de Clientes de Base de Datos"
                Console.WriteLine("Iniciamos Extracciòn Clientes de GR55")
                data = New DataTable()
                da = New SqlDataAdapter()
                query = "SELECT Cliente FROM PlanMo_New.dbo.VENTAS_GR55 GROUP BY Cliente;"
                conexion = New SqlConnection()
                conexion.ConnectionString = connString2
                conexion.Open()
                da.SelectCommand = New SqlCommand(query, conexion)
                da.Fill(data)
                fa = New FileInfo("D:/" & Clie)

                If fa.Exists Then
                    File.Delete("D:/" & Clie)
                End If

                filename = "D:/" & Clie
                archivo_ide = File.CreateText(filename)
                archivo_ide.Close()

                For Each row As DataRow In data.Rows
                    Dim opeIO As OpeIO = New OpeIO(filename)
                    opeIO.WriteNWL(row("Cliente").ToString())
                Next
#End Region

#Region "Extraciòn de Materiales de Base de Datos"

                Console.WriteLine("Iniciamos Extracciòn Materiales de GR55")
                data = New DataTable()
                da = New SqlDataAdapter()
                query = "SELECT Material FROM PlanMo_New.dbo.VENTAS_GR55 GROUP BY Material;"
                conexion = New SqlConnection()
                conexion.ConnectionString = connString2
                conexion.Open()
                da.SelectCommand = New SqlCommand(query, conexion)
                da.Fill(data)
                fa = New FileInfo("D:/" & MateGR55)

                If fa.Exists Then
                    File.Delete("D:/" & MateGR55)
                End If

                filename = "D:/" & MateGR55
                archivo_ide = File.CreateText(filename)
                archivo_ide.Close()

                For Each row As DataRow In data.Rows
                    Dim opeIO As OpeIO = New OpeIO(filename)
                    opeIO.WriteNWL(row("Material").ToString())
                Next
#End Region

#Region "Descarga de Informaciòn Clientes"

                Console.WriteLine("Iniciamos Extracciòn de Informaciòn de Clientes")

                Session.findById("wnd[0]").maximize
                Session.findById("wnd[0]/tbar[0]/okcd").text = "XD03"
                Session.findById("wnd[0]/tbar[0]/btn[0]").press
                Tiempo(2)
                Session.findById("wnd[1]").sendVKey(4)
                Session.findById("wnd[2]/usr/tabsG_SELONETABSTRIP/tabpTAB001/ssubSUBSCR_PRESEL:SAPLSDH4:0220/sub:SAPLSDH4:0220/btnG_SELFLD_TAB-MORE[4,56]").press
                Tiempo(1)
                Session.findById("wnd[3]/tbar[0]/btn[16]").press

#Region "Filtrar Clientes"
                Tiempo(15)
                Tecla("+{F11}")
                Tiempo(3)
                Tecla("{ENTER}")
                Tiempo(4)
                Tecla("D:")
                Tiempo(5)
                Tecla("{ENTER}")
                Tiempo(4)
                Tecla(Clie)
                Tiempo(5)
                Tecla("{ENTER}")
                Tiempo(3)
                Tecla("+{TAB}")
                Tiempo(3)
                Tecla("{ENTER}")
                Tiempo(5)
                Tecla("{F8}")
                Tiempo(4)
                Tecla("{ENTER}")
#End Region

#Region "Guardado de Fichero Txt"
                Tiempo(15)
                Tecla("+{F2}")
                Tiempo(5)
                Tecla("{TAB}")
                Tiempo(5)
                Tecla("{ENTER}")
                Tiempo(8)
                Tecla("+{TAB}")
                Tiempo(9)
                Tecla(Ruta)
                Tiempo(8)
                Tecla("{TAB}")
                Tiempo(8)
                Tecla(Clientes)
                Tiempo(8)
                Tecla("{TAB}")
                Tiempo(6)
                Tecla("{TAB}")
                Tiempo(6)
                Tecla("{ENTER}")
                Tiempo(6)
                Tecla("+{TAB}")
                Tiempo(6)
                Tecla("{ENTER}")
                Tiempo(7)
#End Region

                Session.findById("wnd[2]/tbar[0]/btn[12]").press
                Session.findById("wnd[1]/tbar[0]/btn[12]").press

                Console.WriteLine("Iniciamos Actualizacion de BD Clientes")
                fa = New FileInfo(Ruta + Clientes)
                data = New DataTable()

                If fa.Exists Then
                    Dim stringarry As String() = System.IO.File.ReadAllLines(Ruta + Clientes, Encoding.[Default])

                    If stringarry.Length > 0 Then
                        Dim cc As Integer = 0
                        Dim arr_ = stringarry(1).Substring(1, stringarry(1).Length - 2).Split("|")
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

                        For i As Integer = 3 To stringarry.Length - 1 - 1
                            stringarry(i) = stringarry(i).Replace("S/N|", "S/N")
                            Dim dat = stringarry(i).Substring(1, stringarry(i).Length - 2).Split("|")
                            dr = data.NewRow()

                            For y As Integer = 0 To dat.Length - 1
                                dr(y) = dat(y).Trim()
                            Next

                            data.Rows.Add(dr)
                        Next

                        data.AcceptChanges()
                        data.Columns.Remove("Col0")
                        data.Columns.Remove("Col1")
                        data.AcceptChanges()
                        query = "DELETE FROM PlanMo_New.dbo.VENTAS_GR55_CLIENTES;" & vbCrLf
                        Dim cccc As Integer = 0

                        For Each item As DataRow In data.Rows
                            query += "INSERT INTO PlanMo_New.dbo.VENTAS_GR55_CLIENTES (POBLACION,NOMBRE,CLIENTE) VALUES ("

                            For i As Integer = 0 To item.ItemArray.Length - 1
                                query += "'" & item.ItemArray(i).ToString().Replace("'", "") & "',"
                            Next

                            query += ");" & vbCrLf
                            cccc += 1
                            Console.WriteLine("Leyendo Informacion VENTAS_GR55_CLIENTES: " & cccc & " rows")
                        Next

                        query = query.Replace(",);", ");")
                        conexion = New SqlConnection()
                        conexion.ConnectionString = connString2
                        conexion.Open()
                        myCommand = New SqlCommand(query, conexion)
                        myCommand.CommandTimeout = 360
                        myCommand.ExecuteNonQuery()
                        myCommand.Dispose()
                        conexion.Close()
                    End If
                End If
#End Region

#Region "Descarga de Informaciòn Materiales"

                Console.WriteLine("Iniciamos Extracciòn de Informaciòn de Materiales")

                Session.findById("wnd[0]").maximize
                Session.findById("wnd[0]/tbar[0]/okcd").text = "MM03"
                Session.findById("wnd[0]/tbar[0]/btn[0]").press
                Tiempo(2)
                Session.findById("wnd[0]").sendVKey(4)
                Session.findById("wnd[1]/usr/tabsG_SELONETABSTRIP/tabpTAB001/ssubSUBSCR_PRESEL:SAPLSDH4:0220/sub:SAPLSDH4:0220/btnG_SELFLD_TAB-MORE[2,56]").press
                Tiempo(1)
                Session.findById("wnd[2]/tbar[0]/btn[16]").press

#Region "Filtrar Materiales"
                Tiempo(15)
                Tecla("+{F11}")
                Tiempo(5)
                Tecla("{ENTER}")
                Tiempo(5)
                Tecla("D:")
                Tiempo(6)
                Tecla("{ENTER}")
                Tiempo(5)
                Tecla(MateGR55)
                Tiempo(6)
                Tecla("{ENTER}")
                Tiempo(5)
                Tecla("+{TAB}")
                Tiempo(5)
                Tecla("{ENTER}")
                Tiempo(6)
                Tecla("{F8}")
                Tiempo(5)
                Tecla("{ENTER}")
#End Region

#Region "Guardado de Fichero Txt"
                Tiempo(17)
                Tecla("+{F2}")
                Tiempo(6)
                Tecla("{TAB}")
                Tiempo(6)
                Tecla("{ENTER}")
                Tiempo(9)
                Tecla("+{TAB}")
                Tiempo(9)
                Tecla(Ruta)
                Tiempo(9)
                Tecla("{TAB}")
                Tiempo(9)
                Tecla(Mate)
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

                Session.findById("wnd[1]/tbar[0]/btn[12]").press
                Session.findById("wnd[0]/tbar[0]/okcd").text = "/N"
                Session.findById("wnd[0]/tbar[0]/btn[0]").press

                Console.WriteLine("Iniciamos Actualizacion de BD Materiales")
                fa = New FileInfo(Ruta + Mate)
                data = New DataTable()

                If fa.Exists Then
                    Dim stringarry As String() = System.IO.File.ReadAllLines(Ruta + Mate, Encoding.[Default])

                    If stringarry.Length > 0 Then
                        Dim cc As Integer = 0
                        Dim arr_ = stringarry(1).Substring(1, stringarry(1).Length - 2).Split("|")
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

                        For i As Integer = 3 To stringarry.Length - 1 - 1
                            stringarry(i) = stringarry(i).Replace("S/N|", "S/N")
                            Dim dat = stringarry(i).Substring(1, stringarry(i).Length - 2).Split("|")
                            dr = data.NewRow()

                            For y As Integer = 0 To dat.Length - 1
                                dr(y) = dat(y).Trim()
                            Next

                            data.Rows.Add(dr)
                        Next

                        data.AcceptChanges()
                        data.Columns.Remove("Col3")
                        data.Columns.Remove("Col4")
                        data.AcceptChanges()
                        query = "DELETE FROM PlanMo_New.dbo.VENTAS_GR55_MATERIALES;" & vbCrLf
                        Dim cccc As Integer = 0

                        For Each item As DataRow In data.Rows
                            query += "INSERT INTO PlanMo_New.dbo.VENTAS_GR55_MATERIALES (DESCRIPCION,IDIOMA,MATERIAL) VALUES ("

                            For i As Integer = 0 To item.ItemArray.Length - 1
                                query += "'" & item.ItemArray(i).ToString().Replace("'", "") & "',"
                            Next

                            query += ");" & vbCrLf
                            cccc += 1
                            Console.WriteLine("Leyendo Informacion VENTAS_GR55_MATERIALES: " & cccc & " rows")
                        Next

                        query = query.Replace(",);", ");")
                        conexion = New SqlConnection()
                        conexion.ConnectionString = connString2
                        conexion.Open()
                        myCommand = New SqlCommand(query, conexion)
                        myCommand.CommandTimeout = 360
                        myCommand.ExecuteNonQuery()
                        myCommand.Dispose()
                        conexion.Close()
                    End If
                End If
#End Region

#Region "Descarga de Informaciòn de Stock"

                Console.WriteLine("Iniciamos Extracciòn de Informaciòn de Stock")

                Session.findById("wnd[0]").maximize
                Session.findById("wnd[0]/tbar[0]/okcd").text = "MB52"
                Session.findById("wnd[0]/tbar[0]/btn[0]").press
                Tiempo(2)
                Session.findById("wnd[0]/tbar[1]/btn[17]").press
                Tiempo(2)
                Session.findById("wnd[1]/usr/txtENAME-LOW").text = "ASSESSMENT"
                Session.findById("wnd[1]/tbar[0]/btn[8]").press
                Tiempo(2)
                Session.findById("wnd[0]/usr/btn%_MATNR_%_APP_%-VALU_PUSH").press
                Tiempo(2)
                Session.findById("wnd[1]/tbar[0]/btn[16]").press

#Region "Filtrar Material"
                Tiempo(15)
                Tecla("+{F11}")
                Tiempo(7)
                Tecla("{ENTER}")
                Tiempo(7)
                Tecla("D:")
                Tiempo(7)
                Tecla("{ENTER}")
                Tiempo(7)
                Tecla(Material)
                Tiempo(8)
                Tecla("{ENTER}")
                Tiempo(6)
                Tecla("+{TAB}")
                Tiempo(5)
                Tecla("{ENTER}")
                Tiempo(6)
                Tecla("{F8}")
                Tiempo(6)
                Tecla("{F8}")
#End Region

#Region "Guardado de Fichero Txt"
                Tiempo(17)
                Tecla("^+{F9}")
                Tiempo(7)
                Tecla("{TAB}")
                Tiempo(7)
                Tecla("{ENTER}")
                Tiempo(9)
                Tecla("+{TAB}")
                Tiempo(9)
                Tecla(Ruta)
                Tiempo(9)
                Tecla("{TAB}")
                Tiempo(9)
                Tecla(Stoc)
                Tiempo(9)
                Tecla("{TAB}")
                Tiempo(8)
                Tecla("{TAB}")
                Tiempo(8)
                Tecla("{ENTER}")
                Tiempo(7)
                Tecla("+{TAB}")
                Tiempo(8)
                Tecla("{ENTER}")
                Tiempo(8)
#End Region

                Session.findById("wnd[0]/tbar[0]/okcd").text = "/N"
                Session.findById("wnd[0]/tbar[0]/btn[0]").press

                Console.WriteLine("Iniciamos Actualizacion de BD Stock de ventas")
                'fa = New FileInfo(Ruta + Stoc)
                'data = New DataTable()


                Dim fab3 As FileInfo = New FileInfo(Ruta + Stoc)
                data = New DataTable()


                If fab3.Exists Then
                    Dim stringarry As String() = System.IO.File.ReadAllLines(Ruta + Stoc, Encoding.[Default])

                    If stringarry.Length > 0 Then
                        Dim cc As Integer = 0
                        Dim arr_ = stringarry(1).Substring(1, stringarry(1).Length - 2).Split("|")
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

                        For i As Integer = 3 To stringarry.Length - 1 - 1
                            stringarry(i) = stringarry(i).Replace("S/N|", "S/N")
                            Dim dat = stringarry(i).Substring(1, stringarry(i).Length - 2).Split("|")
                            dr = data.NewRow()

                            For y As Integer = 0 To dat.Length - 1
                                dr(y) = dat(y).Trim()
                            Next

                            data.Rows.Add(dr)
                        Next

                        data.AcceptChanges()
                        query = "DELETE FROM PlanMo_New.dbo.STOCK_MB52;" & vbCrLf
                        Dim cccc As Integer = 0

                        For Each item As DataRow In data.Rows
                            query += "INSERT INTO PlanMo_New.dbo.STOCK_MB52 VALUES ("

                            For i As Integer = 0 To item.ItemArray.Length - 1

                                If i = 5 OrElse i = 7 Then
                                    query += "'" & item.ItemArray(i).ToString().Replace("'", "").Replace(",", "").Replace("*", "-") & "',"
                                Else
                                    query += "'" & item.ItemArray(i).ToString().Replace("'", "") & "',"
                                End If
                            Next

                            query += ");" & vbCrLf
                            cccc += 1
                            Console.WriteLine("Leyendo Informacion STOCK_MB52: " & cccc & " rows")
                        Next

                        query = query.Replace(",);", ");")
                        conexion = New SqlConnection()
                        conexion.ConnectionString = connString2
                        conexion.Open()
                        myCommand = New SqlCommand(query, conexion)
                        myCommand.CommandTimeout = 600
                        myCommand.ExecuteNonQuery()
                        myCommand.Dispose()
                        conexion.Close()
                    End If
                End If
#End Region

#Region "Descarga de Pedidos ZSD023"

                Console.WriteLine("Iniciamos Extracciòn de pedidos de ventas")

                Tiempo(5)
                Session.findById("wnd[0]").maximize
                Session.findById("wnd[0]/tbar[0]/okcd").text = "ZSD023"
                Session.findById("wnd[0]/tbar[0]/btn[0]").press
                Session.findById("wnd[0]/tbar[1]/btn[17]").press
                Session.findById("wnd[1]/usr/txtENAME-LOW").text = "ASSESSMENT"
                Session.findById("wnd[1]/usr/txtENAME-LOW").setFocus
                Session.findById("wnd[1]/usr/txtENAME-LOW").caretPosition = 1
                Session.findById("wnd[1]/tbar[0]/btn[8]").press
                Tiempo(5)
                Session.findById("wnd[0]/usr/ctxtS_ERDAT-LOW").text = DateTime.Now.AddDays(-180).ToString("dd.MM.yyyy") '"01.12.2021"
                Session.findById("wnd[0]/usr/ctxtS_ERDAT-HIGH").text = DateTime.Now.ToString("dd.MM.yyyy") '"31.12.9999"
                Session.findById("wnd[0]/usr/ctxtS_ERDAT-HIGH").setFocus
                Session.findById("wnd[0]/usr/ctxtS_ERDAT-HIGH").caretPosition = 10
                Session.findById("wnd[0]/tbar[1]/btn[8]").press
                Tiempo(8)
                Session.findById("wnd[0]/mbar/menu[3]/menu[2]/menu[1]").select
                Tiempo(7)
                Session.findById("wnd[1]/usr/ssubD0500_SUBSCREEN:SAPLSLVC_DIALOG:0501/cmbG51_USPEC_LBOX").key = "X"
                Tiempo(6)
                Session.findById("wnd[1]/usr/ssubD0500_SUBSCREEN:SAPLSLVC_DIALOG:0501/cntlG51_CONTAINER/shellcont/shell").currentCellColumn = "TEXT"
                Tiempo(7)
                Session.findById("wnd[1]/usr/ssubD0500_SUBSCREEN:SAPLSLVC_DIALOG:0501/cntlG51_CONTAINER/shellcont/shell").selectedRows = "0"
                Tiempo(7)
                Session.findById("wnd[1]/usr/ssubD0500_SUBSCREEN:SAPLSLVC_DIALOG:0501/cntlG51_CONTAINER/shellcont/shell").clickCurrentCell

#Region "Filtrar Material"
                Tiempo(15)
                Tecla("^{F5}")
                Tiempo(10)
                Tecla("+{TAB}")
                Tiempo(4)
                Tecla("+{TAB}")
                Tiempo(5)
                Tecla("+{TAB}")
                Tiempo(5)
                Tecla("+{TAB}")
                Tiempo(5)
                Tecla("+{TAB}")
                Tiempo(5)
                Tecla("+{TAB}")
                Tiempo(5)
                Tecla("+{F10}")
                Tiempo(5)
                Tecla("{DOWN}")
                Tiempo(5)
                Tecla("{ENTER}")
                Tiempo(6)
                Tecla("+{TAB}")
                Tiempo(7)
                Tecla("Material")
                Tiempo(7)
                Tecla("{ENTER}")
                Tiempo(6)
                Tecla("{F12}")
                Tiempo(6)
                Tecla("{F2}")
                Tiempo(6)
                Tecla("^{TAB}")
                Tiempo(5)
                Tecla("{TAB}")
                Tiempo(5)
                Tecla("{TAB}")
                Tiempo(5)
                Tecla("{ENTER}")
                Tiempo(5)
                Tecla("{TAB}")
                Tiempo(5)
                Tecla("{TAB}")
                Tiempo(5)
                Tecla("{ENTER}")
                Tiempo(6)
                Tecla("+{F4}")
                Tiempo(6)
                Tecla("+{F11}")
                Tiempo(6)
                Tecla("{ENTER}")
                Tiempo(6)
                Tecla("D:")
                Tiempo(7)
                Tecla("{ENTER}")
                Tiempo(6)
                Tecla(Material)
                Tiempo(7)
                Tecla("{ENTER}")
                Tiempo(6)
                Tecla("+{TAB}")
                Tiempo(6)
                Tecla("{ENTER}")
                Tiempo(7)
                Tecla("{F8}")
                Tiempo(6)
                Tecla("{ENTER}")
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
                Tiempo(9)
                Tecla(Ruta)
                Tiempo(9)
                Tecla("{TAB}")
                Tiempo(9)
                Tecla(Pedi)
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
                Tiempo(4)
#End Region

                Session.findById("wnd[0]/tbar[0]/okcd").text = "/N"
                Session.findById("wnd[0]/tbar[0]/btn[0]").press
                Tiempo(8)
                Session.findById("wnd[0]").close
                Session.findById("wnd[1]/usr/btnSPOP-OPTION1").press

                Console.WriteLine("Iniciamos Actualizacion de BD Pedidos")
                'fa = New FileInfo(Ruta + Pedi)
                'data = New DataTable()

                Dim fab4 As FileInfo = New FileInfo(Ruta + Pedi)
                data = New DataTable()

                If fab4.Exists Then
                    Dim stringarry As String() = System.IO.File.ReadAllLines(Ruta + Pedi, Encoding.[Default])

                    If stringarry.Length > 0 Then
                        Dim cc As Integer = 0
                        Dim arr_ = stringarry(8).Substring(1, stringarry(8).Length - 2).Split("|")
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

                        For i As Integer = 10 To stringarry.Length - 1 - 1
                            stringarry(i) = stringarry(i).Replace("S/N|", "S/N")
                            Dim dat = stringarry(i).Substring(1, stringarry(i).Length - 2).Split("|")
                            dr = data.NewRow()

                            For y As Integer = 0 To dat.Length - 1
                                dr(y) = dat(y).Trim()
                            Next

                            data.Rows.Add(dr)
                        Next
                        data.AcceptChanges()

                        query = "DELETE FROM PlanMo_New.dbo.PEDIDOS_ZSD023 WHERE Fecdoc BETWEEN DATEADD(DAY,-181,GETDATE()) AND GETDATE();" & vbCrLf
                        Dim cccc As Integer = 0

                        For Each item As DataRow In data.Rows
                            query += "INSERT INTO PlanMo_New.dbo.PEDIDOS_ZSD023 VALUES ("

                            For i As Integer = 0 To item.ItemArray.Length - 1

                                If i = 3 OrElse i = 18 Then
                                    query += "'" & DateTime.Parse(item.ItemArray(i).ToString().Replace("'", "").Replace(".", "/").ToString()).ToString("yyyy-MM-dd") & "',"
                                Else

                                    If i = 15 OrElse i = 17 OrElse i = 19 OrElse i = 20 Then
                                        query += "'" & item.ItemArray(i).ToString().Replace("'", "").Replace(",", "").Replace("-", "") & "',"
                                    Else

                                        If i = 4 Then
                                            query += "'" & item.ItemArray(i).ToString().Replace("'", "").Replace(".", "").Replace(" ", "") & "',"
                                        Else
                                            query += "'" & item.ItemArray(i).ToString().Replace("'", "") & "',"
                                        End If
                                    End If
                                End If
                            Next

                            query += ");" & vbCrLf
                            cccc += 1
                            Console.WriteLine("Leyendo Informacion PEDIDOS_ZSD023: " & cccc & " rows")
                        Next

                        query = query.Replace(",);", ");")
                        conexion = New SqlConnection()
                        conexion.ConnectionString = connString2
                        conexion.Open()
                        myCommand = New SqlCommand(query, conexion)
                        myCommand.CommandTimeout = 1500
                        myCommand.ExecuteNonQuery()
                        myCommand.Dispose()
                        conexion.Close()
                    End If
                End If

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
                msg.Subject = "Sincronizacion Correcta - Informaciòn de Ventas - GR55"
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
            msg.Subject = "Sincronizacion Fallida - Informaciòn de Ventas - GR55"
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
