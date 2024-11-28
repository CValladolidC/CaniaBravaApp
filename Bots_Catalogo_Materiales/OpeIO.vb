Imports System
Imports System.Collections.Generic
Imports System.IO
Imports System.Linq
Imports System.Text
Imports System.Threading.Tasks

Friend Class OpeIO

    Private ErrNum1 As String = "La linea es invalida"


    Private file As String

    Public Sub New(ByVal rnkFile As String)
        file = rnkFile
    End Sub

    Public Sub WriteNWL(ByVal text As String)
        Dim rnkk As StreamReader = New StreamReader(file)
        Dim temp As String = rnkk.ReadToEnd()
        rnkk.Close()
        Dim rnk As StreamWriter = New StreamWriter(file)

        If temp = "" Then
            rnk.Write(text)
        Else
            rnk.Write(temp)
            rnk.WriteLine()
            rnk.Write(text)
        End If

        rnk.Close()
    End Sub

    Public Sub WriteN(ByVal text As String)
        Dim rnkk As StreamReader = New StreamReader(file)
        Dim temp As String = rnkk.ReadToEnd()
        rnkk.Close()
        Dim rnk As StreamWriter = New StreamWriter(file)
        rnk.Write(temp)
        rnk.Write(text)
        rnk.Close()
    End Sub

    Public Sub Write(ByVal text As String)
        Dim rnk As StreamWriter = New StreamWriter(file)
        rnk.Write(text)
        rnk.Close()
    End Sub

    Public Function ReadAll() As String
        Dim rnk As StreamReader = New StreamReader(file)
        Dim text As String = rnk.ReadToEnd()
        rnk.Close()
        Return text
    End Function

    Public Function ReadFirstLine() As String
        Dim rnk As StreamReader = New StreamReader(file)
        Dim text As String = rnk.ReadLine()
        rnk.Close()
        Return text
    End Function

    Public Function ReadLineByNum(ByVal num As Integer) As String
        Dim text As String = ""
        Dim rnk As StreamReader = New StreamReader(file)

        For i As Integer = 1 To num
            text = rnk.ReadLine()

            If text Is Nothing Then
                rnk.Close()
                Return ErrNum1
            End If
        Next

        rnk.Close()
        Return text
    End Function


End Class

