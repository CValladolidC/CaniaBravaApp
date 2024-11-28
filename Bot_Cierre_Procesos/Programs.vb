Imports System.Runtime.InteropServices

Module Programs

    Sub Main()

        Dim processesToKill() As String = {"saplogon", "chrome", "notepad", "sieca", "excel"}

        For Each processName As String In processesToKill
            Dim processes() As Process = Process.GetProcessesByName(processName)
            If processes.Length > 0 Then
                For Each process As Process In processes
                    process.Kill()
                Next
                Console.WriteLine("Se han cerrado todos los procesos " & processName & ".")
            Else
                Console.WriteLine("No se encontró ningún proceso " & processName & " en ejecución.")
            End If
        Next

        'Console.WriteLine("Presione cualquier tecla para salir.")
        'Console.ReadKey()

    End Sub

End Module
