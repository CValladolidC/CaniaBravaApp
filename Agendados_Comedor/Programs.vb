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


Module Programs

    Sub Main()


        Dim data As DataTable = New DataTable()
        Dim query As String = String.Empty
        Dim query2 As String = String.Empty
        Dim correo As String = String.Empty

        Dim conexion As SqlConnection
        Dim usuarios As DataTable
        Dim usuarios2 As DataTable

        Dim connString As String = "Data Source=CHISULSQL1;Database=Asistencia;uid=ctcuser;pwd=ctcuser;"


#Region "Reporte por Perfiles"

        query = "SELECT fecha,perfil,Comedor, [DESAYUNO], [ALMUERZO], [CENA]
                    FROM (
                    SELECT fecha,Servicio, perfil,Comedor,cantidad
                    FROM Asistencia.dbo.view_comedores
                    WHERE Comedor = 'C. MONTELIMA' AND Fecha = CONVERT(VARCHAR(10),GETDATE() - 1, 20)
                    )
                    AS SourceTable  
                    PIVOT  
                    ( SUM(Cantidad)
                      FOR Servicio IN ([DESAYUNO], [ALMUERZO], [CENA])
                    ) AS PivotTable;;"

        conexion = New SqlConnection()
        conexion.ConnectionString = connString
        conexion.Open()
        usuarios = New DataTable

        Using myDataAdapter As SqlDataAdapter = New SqlDataAdapter(query, conexion)
                Dim myDataSet As DataSet = New DataSet()
                myDataAdapter.Fill(myDataSet, "tblData")
            usuarios = myDataSet.Tables(0)
        End Using
        conexion.Close()

        correo = ""

        Dim Array(usuarios.Rows.Count, 6) As String

        For i As Integer = 0 To usuarios.Rows.Count - 1
            Array(i, 0) = usuarios.Rows(i)("fecha").ToString()
            Array(i, 1) = usuarios.Rows(i)("perfil").ToString()
            Array(i, 2) = usuarios.Rows(i)("Comedor").ToString()
            Array(i, 3) = usuarios.Rows(i)("DESAYUNO").ToString()
            Array(i, 4) = usuarios.Rows(i)("ALMUERZO").ToString()
            Array(i, 5) = usuarios.Rows(i)("CENA").ToString()
        Next i

#End Region

#Region "Reporte por Usuarios"

        query2 = "SELECT codigo, dni, comensal,fecha,perfil,Comedor,[DESAYUNO], [ALMUERZO], [CENA]
                    FROM (
                    SELECT codigo, dni, comensal,fecha,Servicio, perfil,Comedor,cantidad
                    FROM Asistencia.dbo.view_comedores
                    WHERE Comedor = 'C. MONTELIMA' AND Fecha = CONVERT(VARCHAR(10),GETDATE() - 1,20)
                    )
                    AS SourceTable  
                    PIVOT  
                    ( SUM(Cantidad)
                      FOR Servicio IN ([DESAYUNO], [ALMUERZO], [CENA])
                    ) AS PivotTable;;"

        conexion = New SqlConnection()
        conexion.ConnectionString = connString
        conexion.Open()
        usuarios2 = New DataTable

        Using myDataAdapter As SqlDataAdapter = New SqlDataAdapter(query2, conexion)
            Dim myDataSet As DataSet = New DataSet()
            myDataAdapter.Fill(myDataSet, "tblData2")
            usuarios2 = myDataSet.Tables(0)
        End Using
        conexion.Close()

        correo = ""

        Dim Array2(usuarios2.Rows.Count, 9) As String

        For i As Integer = 0 To usuarios2.Rows.Count - 1
            Array2(i, 0) = usuarios2.Rows(i)("codigo").ToString()
            Array2(i, 1) = usuarios2.Rows(i)("dni").ToString()
            Array2(i, 2) = usuarios2.Rows(i)("comensal").ToString()
            Array2(i, 3) = usuarios2.Rows(i)("fecha").ToString()
            Array2(i, 4) = usuarios2.Rows(i)("perfil").ToString()
            Array2(i, 5) = usuarios2.Rows(i)("comedor").ToString()
            Array2(i, 6) = usuarios2.Rows(i)("DESAYUNO").ToString()
            Array2(i, 7) = usuarios2.Rows(i)("ALMUERZO").ToString()
            Array2(i, 8) = usuarios2.Rows(i)("CENA").ToString()
        Next i

#End Region

#Region "Correo de Sincronizaciòn Correcta"
        '=== Correo de verificacion

        Dim msg As MailMessage = New MailMessage()
        msg.From = New MailAddress("Comedores<Comedores@agricolachira.com.pe>")
        msg.[To].Add("Ana Lucía Burneo López<Aburneol@agricolachira.com.pe>")
        msg.[To].Add("Jorge Quevedo Arbulu<jquevedoa@agricolachira.com.pe>")
        msg.[To].Add("Armando Jesus Mondoñedo Zapata<amondonedoz@agricolachira.com.pe>")
        msg.[To].Add("Junior Alexander Hidalgo Socola<jhidalgos@agricolachira.com.pe>")

        msg.Subject = "Prueba de Agendado de Comedores Montelima dia: " & DateTime.Now.AddDays(-1).ToString("dd.MM.yyyy")


        Dim htmlString As String
            htmlString = "<html>
                                <h2> REPORTE DE COMEDORES POR PERFILES </h2>
                                <table>
	                            <tr style = 'background-color: #cdcdcd; font-weight: bold;'>
                                    <td style = 'padding: 4px; background-color: #cdcdcd;width:100px; text-align: center;'>FECHA</td>
                                    <td style = 'padding: 4px; background-color: #cdcdcd;width:130px; text-align: center;'>PERFIL</td>
                                    <td style = 'padding: 4px; background-color: #cdcdcd;width:150px; text-align: center;'>COMEDOR</td>
                                    <td style = 'padding: 4px; background-color: #cdcdcd;width:130px; text-align: center;'>DESAYUNO</td>
                                    <td style = 'padding: 4px; background-color: #cdcdcd;width:130px; text-align: center;'>ALMUERZO</td>
                                    <td style = 'padding: 4px; background-color: #cdcdcd;width:130px; text-align: center;'>CENA</td>
                                </tr>
	              "
        For j As Integer = 0 To usuarios.Rows.Count - 1
            htmlString = htmlString & "      

                                <tr style = 'font-size: 12;'>
                                    <td style = 'border-bottom: solid black 1px; width:100px; text-align: center;'>" & Array(j, 0) & "</td>
                                    <td style = 'border-bottom: solid black 1px; width:130px; text-align: center;'>" & Array(j, 1) & "</td>
                                    <td style = 'border-bottom: solid black 1px; width:150px; text-align: center;'>" & Array(j, 2) & "</td>
                                    <td style = 'border-bottom: solid black 1px; width:130px; text-align: center;'>" & Array(j, 3) & "</td>
                                    <td style = 'border-bottom: solid black 1px; width:130px; text-align: center;'>" & Array(j, 4) & "</td>
                                    <td style = 'border-bottom: solid black 1px; width:130px; text-align: center;'>" & Array(j, 5) & "</td>
                                </tr>"
        Next j

        htmlString = htmlString & " </table>
                                    <h2> REPORTE POR USUARIOS </h2>
                                    <table>
                                    <tr style = 'background-color: #cdcdcd; font-weight: bold;'>
                                        <td style = 'padding: 4px; background-color: #cdcdcd;width:70px; text-align: center;'>CODIGO</td>
                                        <td style = 'padding: 4px; background-color: #cdcdcd;width:70px; text-align: center;'>DNI</td>
                                        <td style = 'padding: 4px; background-color: #cdcdcd;width:350px;'>COMENSAL</td>
                                        <td style = 'padding: 4px; background-color: #cdcdcd;width:100px; text-align: center;'>FECHA</td>
                                        <td style = 'padding: 4px; background-color: #cdcdcd;width:130px; text-align: center;'>PERFIL</td>
                                        <td style = 'padding: 4px; background-color: #cdcdcd;width:150px; text-align: center;'>COMEDOR</td>
                                        <td style = 'padding: 4px; background-color: #cdcdcd;width:130px; text-align: center;'>DESAYUNO</td>
                                        <td style = 'padding: 4px; background-color: #cdcdcd;width:130px; text-align: center;'>ALMUERZO</td>
                                        <td style = 'padding: 4px; background-color: #cdcdcd;width:130px; text-align: center;'>CENA</td>
                                     </tr>"
        For j As Integer = 0 To usuarios2.Rows.Count - 1
            htmlString = htmlString & "      

                                <tr style = 'font-size: 12;'>
                                    <td style = 'border-bottom: solid black 1px; width:70px; text-align: center;'>" & Array2(j, 0) & "</td>
                                    <td style = 'border-bottom: solid black 1px; width:70px; text-align: center;'>" & Array2(j, 1) & "</td>
                                    <td style = 'border-bottom: solid black 1px; width:350px;'>" & Array2(j, 2) & "</td>
                                    <td style = 'border-bottom: solid black 1px; width:100px; text-align: center;'>" & Array2(j, 3) & "</td>
                                    <td style = 'border-bottom: solid black 1px; width:130px; text-align: center;'>" & Array2(j, 4) & "</td>
                                    <td style = 'border-bottom: solid black 1px; width:150px; text-align: center;'>" & Array2(j, 5) & "</td>
                                    <td style = 'border-bottom: solid black 1px; width:130px; text-align: center;'>" & Array2(j, 6) & "</td>
                                    <td style = 'border-bottom: solid black 1px; width:130px; text-align: center;'>" & Array2(j, 7) & "</td>
                                    <td style = 'border-bottom: solid black 1px; width:130px; text-align: center;'>" & Array2(j, 8) & "</td>
                                </tr>"
        Next j

        htmlString = htmlString & " </table> </html>"
        msg.Body = htmlString
        msg.IsBodyHtml = True
        Dim smt As SmtpClient = New SmtpClient()
        smt.Host = "10.72.1.71"
        Dim ntcd As NetworkCredential = New NetworkCredential()
        smt.Port = 25
        smt.Credentials = ntcd
        smt.Send(msg)

        Console.WriteLine("Proceso finalizado..!!")

#End Region

    End Sub

End Module
