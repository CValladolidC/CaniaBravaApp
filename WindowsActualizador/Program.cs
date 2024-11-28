using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace WindowsActualizador
{
    class Program
    {

        [DllImport("user32.dll")]
        private static extern int SendMessage(int hWnd, int wMsg, int wParam, int lParam);

        static void Main(string[] args)
        {
            Process[] windows = Process.GetProcesses();

            // Cerrar todas las ventanas
            foreach (Process window in windows)
            {
                SendMessage(window.MainWindowHandle.ToInt32(), 0x0010, 0, 0);
            }

            // Actualizar la pantalla
            SendMessage(0xFFFF, 0x0112, 0xF140, 2);
        }
    }
}