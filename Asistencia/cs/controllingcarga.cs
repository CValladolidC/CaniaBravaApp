using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Data;
using CaniaBrava.Interface;

namespace CaniaBrava.cs
{
    class controllingcarga
    {
        SqlCommand cmd;
        SqlDataReader dr;

        public bool CargarDatacontrolling(DataTable tbData)
        {
            bool resultado = true;
            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            {
                conexion.Open();
                using (SqlBulkCopy s = new SqlBulkCopy(conexion))
                {

                    //ingresamos COLUMNAS ORIGEN | COLUMNAS DESTINOS
                    s.ColumnMappings.Add("escenario", "escenario");
                    s.ColumnMappings.Add("Tipo", "Tipo");
                    s.ColumnMappings.Add("Orden", "Orden");
                    s.ColumnMappings.Add("Cecoste", "Cecoste");
                    s.ColumnMappings.Add("Macrofundo", "Macrofundo");
                    s.ColumnMappings.Add("Fundo", "Fundo");
                    s.ColumnMappings.Add("Sector", "Sector");
                    s.ColumnMappings.Add("Turno", "Turno");
                    s.ColumnMappings.Add("Objagrico", "Objagrico");
                    s.ColumnMappings.Add("Zafra", "Zafra");
                    s.ColumnMappings.Add("Fcultivo", "Fcultivo");
                    s.ColumnMappings.Add("Mes", "Mes");
                    s.ColumnMappings.Add("Anio", "Anio");
                    s.ColumnMappings.Add("Mes_Cosecha", "Mes_Cosecha");
                    s.ColumnMappings.Add("Anio_Cosecha", "Anio_Cosecha");
                    s.ColumnMappings.Add("Labores", "Labores");
                    s.ColumnMappings.Add("Grp_Act", "Grp_Act");
                    s.ColumnMappings.Add("ActividadMaterial", "ActividadMaterial");
                    s.ColumnMappings.Add("Denominacion", "Denominacion");
                    //s.ColumnMappings.Add("Material", "Material");
                    s.ColumnMappings.Add("UMB", "UMB");
                    s.ColumnMappings.Add("Cantidad", "Cantidad");
                    s.ColumnMappings.Add("MonSol", "MonSol");
                    s.ColumnMappings.Add("ValMonObj", "ValMonObj");
                    s.ColumnMappings.Add("MonUSD", "MonUSD");
                    s.ColumnMappings.Add("ValMScCO", "ValMScCO");
                    s.ColumnMappings.Add("Actividades", "Actividades");
                    s.ColumnMappings.Add("Rubro", "Rubro");
                    s.ColumnMappings.Add("Recurso", "Recurso");


                    //definimos la tabla a cargar
                    s.DestinationTableName = "controlling_final";


                    s.BulkCopyTimeout = 2500;
                    try
                    {
                        s.WriteToServer(tbData);
                    }
                    catch (Exception e)
                    {
                        string st = e.Message;
                        resultado = false;
                    }


                }
            }

            return resultado;

        }

    }
}

