using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CaniaBrava.cs
{
    class MqtCarga
    {
        public bool CargaDataMqt(DataTable tabla, string tipo, string lbDato, int cmbMes)
        {
            bool resul = true;
            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION_2");
            {
                conexion.Open();

                using (SqlBulkCopy s = new SqlBulkCopy(conexion))
                {
                    Console.WriteLine("Success");
                    switch (tipo)
                    {
                        case "RE":
                            //s.ColumnMappings.Add("Escenario", "Escenario");
                            s.ColumnMappings.Add("Soc", "Soc");
                            s.ColumnMappings.Add("Orden_Ceco", "Orden_Ceco");
                            s.ColumnMappings.Add("Orden_PM", "Orden_PM");
                            s.ColumnMappings.Add("N_Cuenta", "N_Cuenta");
                            s.ColumnMappings.Add("En_mon_so", "En_mon_so");
                            s.ColumnMappings.Add("FeContab", "FeContab");
                            s.ColumnMappings.Add("Material", "Material");
                            s.ColumnMappings.Add("Cantidad", "Cantidad");
                            s.ColumnMappings.Add("Texto", "Texto");
                            s.ColumnMappings.Add("N_doc", "N_doc");
                            s.ColumnMappings.Add("N_doc_ref", "N_doc_ref");
                            s.ColumnMappings.Add("Doc_Compras", "Doc_Compras");
                            s.ColumnMappings.Add("Tipo_arch", "Tipo_Arch");
                            s.ColumnMappings.Add("Ope", "Ope");

                            s.DestinationTableName = "TBL_Gastos_Reales";

                            s.BulkCopyTimeout = 2500;
                            try { s.WriteToServer(tabla); }
                            catch (SqlException e)
                            {
                                resul = false;
                                MessageBox.Show(e.Message, "Exception", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }

                            //Insertamos el escenario del Real
                            try
                            {
                                string query = string.Empty;
                                query = "UPDATE MQT_Gastos_Chira.dbo.TBL_Gastos_Reales SET Escenario = '" + lbDato + "' WHERE Escenario IS NULL;";

                                SqlCommand myCommand = new SqlCommand(query, conexion);
                                myCommand.ExecuteNonQuery();
                                myCommand.Dispose();
                            }
                            catch (SqlException e)
                            {
                                resul = false;
                                MessageBox.Show(e.Message, "Exception", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                            finally { conexion.Close(); }

                            break;
                        default:
                            //s.ColumnMappings.Add(lbDato, "Escenario");
                            s.ColumnMappings.Add("Soc", "Soc");
                            s.ColumnMappings.Add("Orden_Ceco", "Orden_Ceco");
                            s.ColumnMappings.Add("N_Cuenta", "N_Cuenta");
                            s.ColumnMappings.Add("En_mon_so", "En_mon_so");
                            s.ColumnMappings.Add("USD", "USD");
                            s.ColumnMappings.Add("Mes", "Mes");
                            s.ColumnMappings.Add("Anio", "Anio");
                            s.ColumnMappings.Add("Material", "Material");
                            s.ColumnMappings.Add("Cantidad", "Cantidad");
                            s.ColumnMappings.Add("Texto", "Texto");
                            s.ColumnMappings.Add("UM", "UM");
                            //s.ColumnMappings.Add("Mes_PB_PY", "Mes_PB_PY");

                            s.DestinationTableName = "TBL_Gastos_PB_PY";

                            s.BulkCopyTimeout = 2500;
                            try { s.WriteToServer(tabla); }
                            catch (SqlException e)
                            {
                                resul = false;
                                MessageBox.Show(e.Message, "Exception", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }

                            //Insertamos el escenario del PB o PY
                            try
                            {
                                string query = string.Empty;
                                
                                query = "UPDATE MQT_Gastos_Chira.dbo.TBL_Gastos_PB_PY SET Escenario = '" + lbDato + "' WHERE Escenario IS NULL;";
                                query += "UPDATE MQT_Gastos_Chira.dbo.TBL_Gastos_PB_PY SET Mes_PB_PY = " + cmbMes + " WHERE Mes_PB_PY IS NULL;";

                                SqlCommand myCommand = new SqlCommand(query, conexion);
                                myCommand.ExecuteNonQuery();
                                myCommand.Dispose();
                            }
                            catch (SqlException e)
                            {
                                resul = false;
                                MessageBox.Show(e.Message, "Exception", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                            finally { conexion.Close(); }

                            break;
                    }
                }
            }

            System.Threading.Thread.Sleep(3000);
            return resul;
        }
    }
}
