using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace CaniaBrava
{
    class FuncionDatos
    {
        public DataTable Union(DataTable First, DataTable Second)
        {

            DataTable table = new DataTable("Union");

            //Build new columns

            DataColumn[] newcolumns = new DataColumn[First.Columns.Count];

            for (int i = 0; i < First.Columns.Count; i++)
            {

                newcolumns[i] = new DataColumn(First.Columns[i].ColumnName, First.Columns[i].DataType);

            }

            //add new columns to result table

            table.Columns.AddRange(newcolumns);

            table.BeginLoadData();

            //Load data from first table

            foreach (DataRow row in First.Rows)
            {

                table.LoadDataRow(row.ItemArray, true);

            }

            //Load data from second table

            foreach (DataRow row in Second.Rows)
            {

                table.LoadDataRow(row.ItemArray, true);

            }

            table.EndLoadData();

            return table;

        }

        public DataTable UneTablas(DataTable First, DataTable Second)
        {
            //tabla resultante
            DataTable table = new DataTable("Union");
            //crear las nuevas columnas
            DataColumn[] newcolumns = new DataColumn[First.Columns.Count];
            DataColumn[] newcolumns2 = new DataColumn[Second.Columns.Count];
            for (int i = 0; i < First.Columns.Count; i++)
            {
                newcolumns[i] = new DataColumn(First.Columns[i].ColumnName, First.Columns[i].DataType);
            }
            for (int i = 0; i < Second.Columns.Count; i++)
            {
                newcolumns2[i] = new DataColumn(Second.Columns[i].ColumnName, Second.Columns[i].DataType);
            }

            //agregar las columnas vacias (esquema o estructura) a la tabla resultante
            table.Columns.AddRange(newcolumns);
            table.Columns.AddRange(newcolumns2);
            table.BeginLoadData();
            //cargar los datos desde la primera tabla
            foreach (DataRow row in First.Rows)
            {
                table.LoadDataRow(row.ItemArray, true);
            }
            //cargar los datos desde la segunda tabla
            for (int r = 0; r < Second.Rows.Count; r++)
            {
                for (int c = 0; c < Second.Columns.Count; c++)
                {
                    string cn = Second.Columns[c].ColumnName.ToString();
                    table.Rows[r][cn] = Second.Rows[r][cn];
                }
            }
            table.EndLoadData();
            return table;
        }
    }
}