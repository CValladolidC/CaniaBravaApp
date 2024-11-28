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
        Dim garitas As DataTable
        'Dim usuarios As DataTable

        Dim connString As String = "Data Source=CHISULSQL1;Database=Asistencia;uid=ctcuser;pwd=ctcuser;"


#Region "Reporte por Garitas"

        query = "SELECT [FECHA], [GARITA], [AREA] , [AGRICOLA], [SUCROALCOLERA], [BIOENERGIA]
                    FROM (
                    	SELECT          CONVERT(varchar(10), c.fecha,103) fecha, u.desusr [garita],  m.desmaesgen [area],
						CASE WHEN h.idcia = 01 THEN 'AGRICOLA'
						WHEN h.idcia = 02 THEN 'SUCROALCOLERA'
						WHEN h.idcia = 03 THEN 'BIOENERGIA'
						END AS empresa
						, 1 AS cantidad
						FROM           [Asistencia].[dbo].[control] AS c 
						LEFT OUTER JOIN [Asistencia].[dbo].[usrfile] AS u ON u.idusr = c.idlogin 
						LEFT OUTER JOIN [Asistencia].[dbo].[perplan] AS h ON h.idperplan = c.idperplan 
						LEFT OUTER JOIN [Asistencia].[dbo].[maesgen] AS m ON m.clavemaesgen = h.seccion
						WHERE fecha = CONVERT(VARCHAR(10),GETDATE() - 1, 20)
                    )
                    AS SourceTable  
                    PIVOT  
                    ( SUM(cantidad)
                      FOR empresa IN ([AGRICOLA], [SUCROALCOLERA], [BIOENERGIA])
                    ) AS PivotTable
					ORDER BY GARITA, AREA;"

        conexion = New SqlConnection()
        conexion.ConnectionString = connString
        conexion.Open()
        garitas = New DataTable

        Using myDataAdapter As SqlDataAdapter = New SqlDataAdapter(query, conexion)
            myDataAdapter.SelectCommand.CommandTimeout = 60
            Dim myDataSet As DataSet = New DataSet()
            myDataAdapter.Fill(myDataSet, "tblData")
            garitas = myDataSet.Tables(0)
        End Using
        conexion.Close()

        correo = ""

        Dim Array(garitas.Rows.Count, 6) As String

        For i As Integer = 0 To garitas.Rows.Count - 1
            Array(i, 0) = garitas.Rows(i)("FECHA").ToString()
            Array(i, 1) = garitas.Rows(i)("GARITA").ToString()
            Array(i, 2) = garitas.Rows(i)("AREA").ToString()
            Array(i, 3) = garitas.Rows(i)("AGRICOLA").ToString()
            Array(i, 4) = garitas.Rows(i)("SUCROALCOLERA").ToString()
            Array(i, 5) = garitas.Rows(i)("BIOENERGIA").ToString()
        Next i

#End Region

        '#Region "Reporte por Usuarios"

        '        query2 = "SELECT p.fecha [FECHA], p.garita [GARITA], p.empresa [EMPRESA], p.area [AREA], p.codigo [CODIGO], p.nombreapellido [NOMBRES Y APELLIDOS], p.horaini [HORA INGRESO]
        '						, p.horafin [HORA SALIDA], p.total [TOTAL HORAS]
        '						FROM (
        '						SELECT CONVERT(varchar(10), c.fecha,103) fecha, c.idperplan [codigo], 
        '						RTRIM(h.apepat)+' '+RTRIM(h.apemat)+', '+RTRIM(h.nombres) [nombreapellido],
        '						CONVERT(VARCHAR(10),c.regEntrada,8) [horaini],
        '						CONVERT(VARCHAR(10),c.regSalida,8) [horafin],
        '						u.desusr [garita], m.desmaesgen [area],  
        '						DATEDIFF(HOUR,c.regEntrada,c.regSalida) as total,
        '						CASE WHEN h.idcia = 01 THEN 'AGRICOLA'
        '						WHEN h.idcia = 02 THEN 'SUCROALCOLERA'
        '						WHEN h.idcia = 03 THEN 'BIOENERGIA'
        '						END AS empresa
        '						, 1 AS cantidad
        '						FROM            dbo.control AS c 
        '						LEFT OUTER JOIN dbo.usrfile AS u ON u.idusr = c.idlogin 
        '						LEFT OUTER JOIN dbo.perplan AS h ON h.idperplan = c.idperplan 
        '						LEFT OUTER JOIN dbo.maesgen AS m ON m.clavemaesgen = h.seccion
        '						WHERE fecha = CONVERT(VARCHAR(10),GETDATE() - 1, 20) 

        '						) as p
        '						ORDER BY GARITA, EMPRESA, AREA;"

        '        conexion = New SqlConnection()
        '        conexion.ConnectionString = connString
        '        conexion.Open()
        '        usuarios = New DataTable

        '        Using myDataAdapter As SqlDataAdapter = New SqlDataAdapter(query2, conexion)
        '            Dim myDataSet As DataSet = New DataSet()
        '            myDataAdapter.Fill(myDataSet, "tblData2")
        '            usuarios = myDataSet.Tables(0)
        '        End Using
        '        conexion.Close()

        '        correo = ""

        '        Dim Array2(usuarios.Rows.Count, 9) As String

        '        For i As Integer = 0 To usuarios.Rows.Count - 1
        '            Array2(i, 0) = usuarios.Rows(i)("FECHA").ToString()
        '            Array2(i, 1) = usuarios.Rows(i)("GARITA").ToString()
        '            Array2(i, 2) = usuarios.Rows(i)("EMPRESA").ToString()
        '            Array2(i, 3) = usuarios.Rows(i)("AREA").ToString()
        '            Array2(i, 4) = usuarios.Rows(i)("CODIGO").ToString()
        '            Array2(i, 5) = usuarios.Rows(i)("NOMBRES Y APELLIDOS").ToString()
        '            Array2(i, 6) = usuarios.Rows(i)("HORA INGRESO").ToString()
        '            Array2(i, 7) = usuarios.Rows(i)("HORA SALIDA").ToString()
        '            Array2(i, 8) = usuarios.Rows(i)("TOTAL HORAS").ToString()

        '        Next i
        '#End Region

#Region "Correo de Sincronizaciòn Correcta"
        '=== Correo de verificacion

        Dim msg As MailMessage = New MailMessage()
        msg.From = New MailAddress("Marcaciones de Asistencias<marcacionesdiarias@agricolachira.com.pe>")
        msg.[To].Add("Katherine Sofia Sanchez Maldonado<ksanchezm@agricolachira.com.pe>")
        msg.[To].Add("Milagros del Pilar Navarro Navarro<mnavarron@agricolachira.com.pe>")
        msg.[To].Add("Lesly Yanira Mogollon Jimenez<lmogollonj@agricolachira.com.pe>")
        msg.[To].Add("Alex Eduardo Casariego Palomino<auxiliarlb@agricolachira.com.pe>")
        msg.[To].Add("Frank Luis Arica Nole<auxiliarhc@agricolachira.com.pe>")
        msg.[To].Add("Aracelly Violeta Mercedes Javier Nevado<ajaviern@agricolachira.com.pe>")
        msg.[To].Add("Elvis Giron Alama<egirona@agricolachira.com.pe>")
        msg.[To].Add("Antony Darwin Sernaque Villegas<asernaquev@agricolachira.com.pe>")
        msg.[To].Add("Robert Estiwar Villegas Criollo<rvillegasc@agricolachira.com.pe>")
        'msg.[To].Add("Junior Alexander Hidalgo Socola<jhidalgos@agricolachira.com.pe>")
        'msg.[To].Add("Jimmy Vasquez Castro<jvasquezc@agricolachira.com.pe>")
        'msg.[To].Add("David Emmanuel Inga Checa<dingach@agricolachira.com.pe>")
        msg.[To].Add("LD Sistemas<sistemascb@agricolachira.com.pe>")
        msg.[To].Add("Jaime Mendoza Garay<jmendozag@agricolachira.com.pe>")
        msg.[To].Add("Seguridad Huaca<seguridadhuaca_lobo@agricolachira.com.pe>")
        msg.[To].Add("Seguridadplanta<seguridadplanta@agricolachira.com.pe>")
        msg.[To].Add("Seguridadmontelima<seguridadmontelima@agricolachira.com.pe>")
        msg.[To].Add("Seguridad San Vicente<seguridadsanvicente@agricolachira.com.pe>")

        msg.Subject = "Marcaciones de asistencias por Garitas dia: " & DateTime.Now.AddDays(-1).ToString("dd.MM.yyyy")


        Dim htmlString As String
        htmlString = "<html>
                                <h2> REPORTE DE MARCACIONES POR GARITAS </h2>
                                <table>
	                            <tr style = 'background-color: #cdcdcd; font-weight: bold;'>
                                    <td style = 'padding: 4px; background-color: #cdcdcd;width:100px; text-align: center;'>FECHA</td>
                                    <td style = 'padding: 4px; background-color: #cdcdcd;width:250px; text-align: left;'>GARITA</td>
                                    <td style = 'padding: 4px; background-color: #cdcdcd;width:330px; text-align: left;'>AREA</td>
                                    <td style = 'padding: 4px; background-color: #cdcdcd;width:130px; text-align: center;'>AGRICOLA</td>
                                    <td style = 'padding: 4px; background-color: #cdcdcd;width:130px; text-align: center;'>SUCROALCOLERA</td>
                                    <td style = 'padding: 4px; background-color: #cdcdcd;width:130px; text-align: center;'>BIOENERGIA</td>
                                </tr>
	              "
        For j As Integer = 0 To garitas.Rows.Count - 1
            htmlString = htmlString & "      

                                <tr style = 'font-size: 12;'>
                                    <td style = 'border-bottom: solid black 1px; width:100px; text-align: center;'>" & Array(j, 0) & "</td>
                                    <td style = 'border-bottom: solid black 1px; width:250px; text-align: left;'>" & Array(j, 1) & "</td>
                                    <td style = 'border-bottom: solid black 1px; width:330px; text-align: left;'>" & Array(j, 2) & "</td>
                                    <td style = 'border-bottom: solid black 1px; width:130px; text-align: center;'>" & Array(j, 3) & "</td>
                                    <td style = 'border-bottom: solid black 1px; width:130px; text-align: center;'>" & Array(j, 4) & "</td>
                                    <td style = 'border-bottom: solid black 1px; width:130px; text-align: center;'>" & Array(j, 5) & "</td>
                                </tr>"
        Next j

        'htmlString = htmlString & " </table>
        '                            <h2> REPORTE POR TRABAJADORES </h2>
        '                            <table>
        '                            <tr style = 'background-color: #cdcdcd; font-weight: bold;'>
        '                                <td style = 'padding: 4px; background-color: #cdcdcd;width:100px; text-align: center;'>FECHA</td>
        '                                <td style = 'padding: 4px; background-color: #cdcdcd;width:250px; text-align: left;'>GARITA</td>
        '                                <td style = 'padding: 4px; background-color: #cdcdcd;width:150px; text-align: left;'>EMPRESA</td>
        '                                <td style = 'padding: 4px; background-color: #cdcdcd;width:200px; text-align: left;'>AREA</td>
        '                                <td style = 'padding: 4px; background-color: #cdcdcd;width:100px; text-align: center;'>CODIGO</td>
        '                                <td style = 'padding: 4px; background-color: #cdcdcd;width:400px; text-align: left;'>NOMBRES Y APELLIDOS</td>
        '                                <td style = 'padding: 4px; background-color: #cdcdcd;width:100px; text-align: center;'>HORA INGRESO</td>
        '                                <td style = 'padding: 4px; background-color: #cdcdcd;width:100px; text-align: center;'>HORA SALIDA</td>
        '                                <td style = 'padding: 4px; background-color: #cdcdcd;width:100px; text-align: center;'>TOTAL HORAS</td>
        '                             </tr>"

        'For j As Integer = 0 To usuarios.Rows.Count - 1
        '    htmlString = htmlString & "      

        '                        <tr style = 'font-size: 12;'>
        '                            <td style = 'border-bottom: solid black 1px; width:100px; text-align: center;'>" & Array2(j, 0) & "</td>
        '                            <td style = 'border-bottom: solid black 1px; width:250px; text-align: left;'>" & Array2(j, 1) & "</td>
        '                            <td style = 'border-bottom: solid black 1px; width:150px; text-align: left;'>" & Array2(j, 2) & "</td>
        '                            <td style = 'border-bottom: solid black 1px; width:200px; text-align: left;'>" & Array2(j, 3) & "</td>
        '                            <td style = 'border-bottom: solid black 1px; width:100px; text-align: center;'>" & Array2(j, 4) & "</td>
        '                            <td style = 'border-bottom: solid black 1px; width:400px; text-align: left;'>" & Array2(j, 5) & "</td>
        '                            <td style = 'border-bottom: solid black 1px; width:100px; text-align: center;'>" & Array2(j, 6) & "</td>
        '                            <td style = 'border-bottom: solid black 1px; width:100px; text-align: center;'>" & Array2(j, 7) & "</td>
        '                            <td style = 'border-bottom: solid black 1px; width:100px; text-align: center;'>" & Array2(j, 8) & "</td>
        '                        </tr>"
        'Next j

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
