Imports Microsoft.VisualBasic
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

Public Class ConxSap

    Dim SapGuiAuto
    Dim SetApp
    Dim Connection
    Dim Session

    Public Sub ConexionSap()
        Dim data As DataTable = New DataTable()
        Dim query As String = String.Empty

        Dim conexion As SqlConnection
        Dim myCommand As SqlCommand

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

        Dim connString As String = "Data Source=" & Serv & ";Database=Asistencia;uid=ctcuser;pwd=ctcuser;"

        '= Variables para la conexión SAP =====================

        SapGuiAuto = GetObject("SAPGUI")
        SetApp = SapGuiAuto.GetScriptingEngine
        Connection = SetApp.Children(0)
        Session = Connection.Children(0)

#Region "Descarga de Informacion PCO0"

        Console.WriteLine("Iniciamos Descarga de Informacion PCO0")

        Session.findById("wnd[0]").maximize()
            Session.findById("wnd[0]/tbar[0]/okcd").Text = "MB52"
            Session.findById("wnd[0]/tbar[0]/btn[0]").press()
            Session.findById("wnd[0]/tbar[1]/btn[17]").press()
            Session.findById("wnd[1]/usr/txtENAME-LOW").Text = "JHIDALGOS"
            Session.findById("wnd[1]/usr/txtENAME-LOW").SetFocus()
            Session.findById("wnd[1]/usr/txtENAME-LOW").caretPosition = 1
            Session.findById("wnd[1]/tbar[0]/btn[8]").press()
        Session.findById("wnd[0]/tbar[1]/btn[8]").press()


        Dim fab As FileInfo = New FileInfo("D:\" & "PCP0.txt")
        data = New DataTable()

        If fab.Exists Then
            Dim stringarry As String() = System.IO.File.ReadAllLines("D:\" & "PCP0.txt", Encoding.[Default])

            If stringarry.Length > 0 Then
                Dim cc As Integer = 0
                Dim arr_ = stringarry(19).Substring(1, stringarry(19).Length - 2).Split("|")
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

                For i As Integer = 21 To stringarry.Length - 25 - 1
                    stringarry(i) = stringarry(i).Replace("S/N|", "S/N")
                    Dim dat = stringarry(i).Substring(1, stringarry(i).Length - 2).Split("|")
                    dr = data.NewRow()

                    For y As Integer = 0 To dat.Length - 1
                        dr(y) = dat(y).Trim()
                    Next

                    data.Rows.Add(dr)
                Next

                data.AcceptChanges()
                query = "DELETE FROM PlanMo_New.dbo.PCP0_CUENTAS;" & vbCrLf
                Dim cccc As Integer = 0

                For Each item As DataRow In data.Rows
                    query += "INSERT INTO PlanMo_New.dbo.PCP0_CUENTAS VALUES ("

                    For i As Integer = 0 To item.ItemArray.Length - 1

                        If True Then

                            If i = 7 OrElse i = 8 Then

                                If item.ItemArray(i).ToString() <> String.Empty Then
                                    query += "'" & item.ItemArray(i).ToString().Replace("'", "").Replace(",", "") & "',"
                                Else
                                    query += "'0',"
                                End If
                            Else
                                query += "'" & item.ItemArray(i).ToString().Replace("'", "") & "',"
                            End If
                        End If
                    Next

                    query += ");" & vbCrLf
                    cccc += 1
                Next

                query = query.Replace(",);", ");")
                conexion = New SqlConnection()
                conexion.ConnectionString = connString
                conexion.Open()
                myCommand = New SqlCommand(query, conexion)
                myCommand.CommandTimeout = 1500
                myCommand.ExecuteNonQuery()
                myCommand.Dispose()
                conexion.Close()


            End If
        End If

#End Region


    End Sub

    Sub Tiempo(ByVal tiempo As Integer)
        Thread.Sleep(tiempo * 1000)
    End Sub

    Sub Tecla(ByVal tecla As String)
        SendKeys.SendWait(tecla)
    End Sub

End Class
