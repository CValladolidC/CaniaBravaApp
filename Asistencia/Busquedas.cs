using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CaniaBrava
{
    class Busquedas
    {
        public bool buscarDataGridView(string TextoABuscar, int Columna, DataGridView grid)
        {
            Funciones funciones = new Funciones();
            bool encontrado = false;
            if (TextoABuscar == string.Empty) return false;
            if (grid.RowCount == 0) return false;
            grid.ClearSelection();

            int longitudCadena = TextoABuscar.Length;

            for (int i = 0; i < grid.Rows.Count; i++)
            {
                if (funciones.longitudCadena(grid.Rows[i].Cells[Columna].Value.ToString(), longitudCadena) == TextoABuscar)
                {
                    grid.FirstDisplayedScrollingRowIndex = grid.Rows[i].Index;
                    grid.Refresh();
                    grid.CurrentCell = grid.Rows[grid.Rows[i].Index].Cells[0];
                    grid.Rows[grid.Rows[i].Index].Selected = true;
                    return true;
                }
            }
            return encontrado;
        }
    }
}