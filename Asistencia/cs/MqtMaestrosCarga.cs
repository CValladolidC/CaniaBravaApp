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
    class MqtMaestrosCarga
    {
        public bool CargaData_MqtMaestros(DataTable tabla, string tipoMaestro)
        {
            bool resul = true;
            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION_2");
            {
                conexion.Open();

                using (SqlBulkCopy s = new SqlBulkCopy(conexion))
                {
                    Console.WriteLine("Success");
                    switch (tipoMaestro)
                    {
                        case "ORD":
                            s.ColumnMappings.Add("Soc", "Soc");
                            s.ColumnMappings.Add("Orden_Ceco", "Orden_Ceco");
                            s.ColumnMappings.Add("Cta_Contable", "Cta_Contable");
                            s.ColumnMappings.Add("Desc_Ceco_Orden", "Desc_Ceco_Orden");
                            s.ColumnMappings.Add("CeBe", "CeBe");
                            s.ColumnMappings.Add("Tipo_Gasto", "Tipo_Gasto");
                            s.ColumnMappings.Add("Gerencia", "Gerencia");
                            s.ColumnMappings.Add("Jefe_Responsable", "Jefe_Responsable");
                            s.ColumnMappings.Add("Macro_Fundo", "Macro_Fundo");
                            s.ColumnMappings.Add("Area", "Area");
                            s.ColumnMappings.Add("Sistema", "Sistema");
                            s.ColumnMappings.Add("Subsistema", "Subsistema");
                            s.ColumnMappings.Add("Id_Agrupador", "Id_Agrupador");
                            s.ColumnMappings.Add("Agrupador", "Agrupador");
                            s.ColumnMappings.Add("Id_Jefe_Revisor", "Id_Jefe_Revisor");
                            s.ColumnMappings.Add("Jefe_Revisor", "Jefe_Revisor");
                            s.ColumnMappings.Add("Gasto", "Gasto");
                            s.ColumnMappings.Add("Estado", "Estado");

                            s.DestinationTableName = "TBL_Ordenes_Ceco";

                            s.BulkCopyTimeout = 2500;
                            try { s.WriteToServer(tabla); }
                            catch (SqlException e) { MessageBox.Show(e.Message, "Exception", MessageBoxButtons.OK, MessageBoxIcon.Error); }

                            //Insertamos el Id
                            try
                            {
                                string query = string.Empty;
                                query = "UPDATE TBL_Ordenes_Ceco SET Id = CONCAT(Orden_Ceco,Cta_Contable) WHERE Id IS NULL;";

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
                        case "CC":
                            s.ColumnMappings.Add("Cta_Contable", "Cta_Contable");
                            s.ColumnMappings.Add("Desc_Cta_Contable", "Desc_Cta_Contable");
                            s.ColumnMappings.Add("Estado", "Estado");

                            s.DestinationTableName = "TBL_Cta_Contables";

                            s.BulkCopyTimeout = 2500;
                            try { s.WriteToServer(tabla); }
                            catch (SqlException e)
                            {
                                resul = false;
                                MessageBox.Show(e.Message, "Exception", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }

                            break;
                        case "MAT":
                            s.ColumnMappings.Add("N_Material", "N_Material");
                            s.ColumnMappings.Add("Desc_Material", "Desc_Material");
                            s.ColumnMappings.Add("UM", "UM");
                            s.ColumnMappings.Add("Status", "Status");
                            s.ColumnMappings.Add("Estado", "Estado");

                            s.DestinationTableName = "TBL_Materiales";

                            s.BulkCopyTimeout = 2500;
                            try { s.WriteToServer(tabla); }
                            catch (SqlException e)
                            {
                                resul = false;
                                MessageBox.Show(e.Message, "Exception", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }

                            break;
                        case "AGR":
                            s.ColumnMappings.Add("Id_Agrupador", "Id_Agrupador");
                            s.ColumnMappings.Add("Cta_Contable", "Cta_Contable");
                            s.ColumnMappings.Add("Agrupador", "Agrupador");
                            s.ColumnMappings.Add("Estado", "Estado");

                            s.DestinationTableName = "TBL_Agrupadores";

                            s.BulkCopyTimeout = 2500;
                            try { s.WriteToServer(tabla); }
                            catch (SqlException e)
                            {
                                resul = false;
                                MessageBox.Show(e.Message, "Exception", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }

                            break;
                        case "TDM":
                            s.ColumnMappings.Add("Cta_Contable", "Cta_Contable");
                            s.ColumnMappings.Add("Desc_Tipo_Man", "Desc_Tipo_Man");
                            s.ColumnMappings.Add("Estado", "Estado");

                            s.DestinationTableName = "TBL_Tipo_Man";

                            s.BulkCopyTimeout = 2500;
                            try { s.WriteToServer(tabla); }
                            catch (SqlException e)
                            {
                                resul = false;
                                MessageBox.Show(e.Message, "Exception", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }

                            break;
                        case "JR":
                            s.ColumnMappings.Add("Id_Agrupador", "Id_Agrupador");
                            s.ColumnMappings.Add("Jefe_Revisor", "Jefe_Revisor");
                            s.ColumnMappings.Add("Estado", "Estado");

                            s.DestinationTableName = "TBL_Jefe_Revisor";

                            s.BulkCopyTimeout = 2500;
                            try { s.WriteToServer(tabla); }
                            catch (SqlException e)
                            {
                                resul = false;
                                MessageBox.Show(e.Message, "Exception", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }

                            break;
                        //default:
                        //    break;
                    }
                }
            }
            System.Threading.Thread.Sleep(3000);
            return resul;
        }
    }
}
