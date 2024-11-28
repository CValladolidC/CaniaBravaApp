using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Data.SqlClient;
using System.Configuration;
using ExcelDataReader;
using CaniaBrava.cs;

namespace CaniaBrava.Interface
{
    public partial class ui_mqt_gastos : Form
    {
        List<string> listaArchivos = new List<string>();
        private int vers = 1;
        private int nColReal = 14, nColPBPY = 11;
        Funciones funciones = new Funciones();

        public ui_mqt_gastos()
        {
            InitializeComponent();
            txtAnio.Text = DateTime.Now.Year.ToString();
        }

        private void btnCargar_Click(object sender, EventArgs e)
        {
            string tipo = funciones.getValorComboBox(cmbTipo, 2);

            if (tipo == string.Empty)
            {
                MessageBox.Show("Debe ingresar Tipo de carga", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                cmbTipo.Focus();
            }
            else
            {
                UpdateProgressBar(0);

                listaArchivos.Clear();
                listBoxArchivos.Items.Clear();

                listBoxRegMont.Items.Clear();
                listBoxMntTotal.Items.Clear();

                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Multiselect = true;
                openFileDialog.Filter = "Excel Worbook|*.xls;*.xlsx";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    foreach (string archivo in openFileDialog.FileNames)
                    {
                        listaArchivos.Add(archivo);
                        MostrarRegYMont(archivo, tipo);
                        listBoxArchivos.Items.Add(archivo);
                    }
                }
            }
        }

        public void MostrarRegYMont(string archivo, string tipo)
        {
            DataTable tabla = new DataTable();
            DataSet dtsTabla = new DataSet();

            using (var stream = File.Open(archivo, FileMode.Open, FileAccess.Read))
            {
                using (var reader = ExcelReaderFactory.CreateReader(stream))
                {
                    dtsTabla = reader.AsDataSet(new ExcelDataSetConfiguration()
                    {
                        ConfigureDataTable = (tableReader) => new ExcelDataTableConfiguration() { UseHeaderRow = true }
                    });

                    tabla = dtsTabla.Tables[0];
                    string nReg = tabla.Rows.Count.ToString();
                    listBoxRegMont.Items.Add(nReg);

                    double montoTotal = 0.0;
                    if (tipo == "RE")
                    {
                        foreach (DataRow row in tabla.Rows)
                        {
                            if (row[4] != DBNull.Value && double.TryParse(row[4].ToString(), out double valor)) { montoTotal += valor; }
                        }
                    }
                    else
                    {
                        foreach (DataRow row in tabla.Rows)
                        {
                            if (row[3] != DBNull.Value && double.TryParse(row[3].ToString(), out double valor)) { montoTotal += valor; }
                        }
                    }
                    
                    listBoxMntTotal.Items.Add(montoTotal.ToString("N2"));
                }
            }
        }

        private async void btnRegData_Click(object sender, EventArgs e)
        {
            if (Verificar())
            {
                string tipo = funciones.getValorComboBox(cmbTipo, 2);
                int mes = tipo == "PY" ? Convert.ToInt32(cmbMes.Text.Trim().Substring(0, 2)) : 0;
                bool resul = false;

                progressBar.Maximum = (listaArchivos.Count * 100) / listaArchivos.Count;

                DataTable tabla = new DataTable();

                await Task.Run(() =>
                {
                    int totalArchivos = listaArchivos.Count;
                    for (int i = 0; i < totalArchivos; i++)
                    {
                        tabla = LeerTabla(listaArchivos[i], tipo);

                        if (tabla != null)
                        {
                            resul = SubirArchivo(tabla, tipo, mes);
                            if (resul) { UpdateProgressBar((i + 1) * 100 / totalArchivos); }
                        }
                    }
                });

                if (tabla != null)
                {
                    if (resul)
                    {
                        if (cmbTipo.Text != "RE") Escenario();
                        MessageBox.Show("Todos los archivos se han cargado correctamente.", "Exito", MessageBoxButtons.OK);
                    }
                }
            }
        }

        private bool Verificar()
        {
            bool resul = false;

            if (!listaArchivos.Any())
            {
                MessageBox.Show("Debe cargar algún archivo", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                btnCargar.Focus();
                return resul;
            }

            if (txtAnio.Text.Trim().Length != 4)
            {
                MessageBox.Show("Debe ingresar un Año correcto", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtAnio.Focus();
            }
            else
            {
                string tipo = funciones.getValorComboBox(cmbTipo, 2);
                if (tipo == string.Empty)
                {
                    MessageBox.Show("Debe ingresar Tipo de carga", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    cmbTipo.Focus();
                }
                else
                {
                    string mes = funciones.getValorComboBox(cmbMes, 2); ;
                    if (cmbTipo.Text == "PY")
                    {
                        if (mes == string.Empty)
                        {
                            MessageBox.Show("Debe ingresar un Mes correcto", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            cmbMes.Focus();
                        }
                        else resul = true;
                    }
                    else resul = true;
                }
            }

            return resul;
        }

        private void Escenario()
        {
            string tipo = funciones.getValorComboBox(cmbTipo, 2);
            string anio = txtAnio.Text.Trim();
            string mes = tipo == "PY" ? funciones.getValorComboBox(cmbMes, 2) : "";
            string escenario = lbDato.Text;
            string version = lbDato.Text.Trim().Substring(17, 2).Replace(" ", "");
            
            int indicador = 1;

            //insertando la version
            SqlConnection conexion = new SqlConnection();
            try
            {
                string query = string.Empty;
                query = "UPDATE MQT_Gastos_Chira.dbo.Mqt_Version SET Indicador = 0 WHERE Tipo = '" + tipo + "' ";
                query += "INSERT INTO Mqt_Version VALUES ('" + tipo + "','" + anio + "','" + anio + "-" + (mes == string.Empty ? "01" : mes) + "-01" + "','" + version + "','" + escenario + "','" + indicador + "'); ";

                conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION_2");
                conexion.Open();
                SqlCommand myCommand = new SqlCommand(query, conexion);
                myCommand.ExecuteNonQuery();
                myCommand.Dispose();
            }
            catch (SqlException ex)
            {
                Console.WriteLine("Exception:" + ex.Message);
            }
            finally { conexion.Close(); }
        }

        private DataTable LeerTabla(string archivo, string tipo)
        {
            DataTable tabla = new DataTable();
            DataSet dtsTabla = new DataSet();
            string tipoArch = string.Empty;
            string tpAr = string.Empty;

            try
            {
                using (var stream = File.Open(archivo, FileMode.Open, FileAccess.Read))
                {
                    using (var reader = ExcelReaderFactory.CreateReader(stream))
                    {
                        dtsTabla = reader.AsDataSet(new ExcelDataSetConfiguration()
                        {
                            ConfigureDataTable = (tableReader) => new ExcelDataTableConfiguration()
                            {
                                UseHeaderRow = true
                            }
                        });
                        tabla = dtsTabla.Tables[0];

                        if (tipo == "RE")
                        {
                            int[] colVerifNull = { 0, 3, 4, 5, 6, 7 };
                            int[] colVerifFloat = { 4, 7 };

                            if (tabla.Columns.Count == nColReal)
                            {
                                if (VerifNomCol(tabla, tipo) == false) return null;
                                if (VerifColNull(tabla, colVerifNull, colVerifFloat) == false) return null;

                                tipoArch = tabla.Rows[0][12].ToString();

                                SqlConnection conexion = new SqlConnection();
                                conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION_2");
                                conexion.Open();

                                try
                                {
                                    string query = string.Empty;
                                    string query2 = string.Empty;
                                    query = "SELECT Tipo_Arch FROM TBL_Gastos_Reales WHERE Tipo_Arch = '" + tipoArch + "' GROUP BY Tipo_Arch;";

                                    SqlCommand myCommand = new SqlCommand(query, conexion);
                                    SqlDataReader dr = myCommand.ExecuteReader();

                                    if (dr.Read()) tpAr = dr.GetString(dr.GetOrdinal("Tipo_Arch"));

                                    dr.Close();
                                    myCommand.Dispose();

                                    if (tpAr != "")
                                    {
                                        MessageBox.Show($"Ya se tiene información de '{Path.GetFileNameWithoutExtension(archivo)}', se procederá a reemplazar", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                        query2 = "DELETE FROM TBL_Gastos_Reales WHERE Tipo_Arch = '" + tpAr + "';";

                                        SqlCommand myCommand2 = new SqlCommand(query2, conexion);
                                        myCommand2.ExecuteNonQuery();
                                        myCommand2.Dispose();
                                    }
                                }
                                catch (SqlException e)
                                {
                                    MessageBox.Show(e.Message, "Exception", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    return null;
                                }
                                finally { conexion.Close(); }
                            }
                            else
                            {
                                MessageBox.Show("Faltan columnas en el archivo.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return null;
                            }
                        }
                        else
                        {
                            if (tabla.Columns.Count == nColPBPY)
                            {
                                int[] colVerifNull = { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
                                int[] colVerifFloat = { 3, 4, 8 };

                                if (VerifNomCol(tabla, tipo) == false) return null;
                                if (VerifColNull(tabla, colVerifNull, colVerifFloat) == false) return null;
                            }
                            else
                            {
                                MessageBox.Show("Faltan columnas en el archivo.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return null;
                            }
                        }
                    }
                }
            }
            catch (IOException)
            {
                MessageBox.Show("El archivo está siendo utilizado por otro proceso.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
            
            return tabla;
        }

        private bool VerifNomCol(DataTable tabla, string tipo)
        {
            for (int i = 0; i < tabla.Columns.Count; i++)
            {
                string nombreColumna = tabla.Columns[i].ToString();
                if (!string.IsNullOrEmpty(nombreColumna))
                {
                    if (tipo == "RE")
                    {
                        if (nombreColumna != NomCol(i, tipo))
                        {
                            MessageBox.Show($"El nombre de la columna '{nombreColumna}' no coincide con el esperado: '{NomCol(i, tipo)}'.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return false;
                        }
                    }
                    else
                    {
                        if (nombreColumna != NomCol(i, tipo))
                        {
                            MessageBox.Show($"El nombre de la columna '{nombreColumna}' no coincide con el esperado: '{NomCol(i, tipo)}'.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return false;
                        }
                    }
                }
                else
                {
                    MessageBox.Show($"La columna esta vacía, se espereba: '{NomCol(i, tipo)}'.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }

            return true;
        }

        private string NomCol(int indice, string tipo)
        {
            if (tipo == "RE")
            {
                string[] nomColReal = { "Soc", "Orden_Ceco", "Orden_PM", "N_Cuenta", "En_mon_so", "FeContab", "Material", "Cantidad", "Texto", "N_doc", "N_doc_ref", "Doc_compras", "Tipo_Arch", "Ope" };
                return nomColReal[indice];
            }
            else
            {
                string[] noColPBPY = { "Soc", "Orden_Ceco", "N_Cuenta", "En_mon_so", "USD", "Mes", "Anio", "Material", "Cantidad", "Texto", "UM" };
                return noColPBPY[indice];
            }
        }

        public bool VerifColNull(DataTable tabla, int[] colVerifNull, int[] colVerifFloat)
        {
            // Filtrar las filas que contienen valores nulos en las columnas especificadas
            var filasValNull = tabla.AsEnumerable()
                .Where(fila => colVerifNull.Any(colIndex => fila.IsNull(colIndex)));

            // Recorrer las filas con valores nulos
            foreach (DataRow fila in filasValNull)
            {
                foreach (int colIndex in colVerifNull)
                {
                    if (fila.IsNull(colIndex))
                    {
                        MessageBox.Show($"Se detectó una celda vacía en la columna '{tabla.Columns[colIndex]}', fila {fila.Table.Rows.IndexOf(fila) + 2}.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return false;
                    }
                }
            }

            if (VerifColFloat(tabla, colVerifFloat) == false) return false;

            return true;
        }

        public bool VerifColFloat(DataTable tabla, int[] colVerifFloat)
        {
            foreach (DataRow row in tabla.Rows)
            {
                foreach (int column in colVerifFloat)
                {
                    if (tabla.Columns[column].DataType == typeof(double))
                    {
                        string valor = row[column].ToString();
                        
                        string[] partes = valor.Split('.');
                        
                        if (partes.Length == 2 && partes[1].Length > 9)
                        {
                            MessageBox.Show($"El valor en la columna '{tabla.Columns[column].ColumnName}', fila {row.Table.Rows.IndexOf(row) + 2} tiene más de 9 decimales.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return false;
                        }
                    }
                    else
                    {
                        MessageBox.Show($"La columna {tabla.Columns[column].ColumnName} solo acepta valores decimales.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return false;
                    }
                }
            }
            return true;
        }

        public bool SubirArchivo(DataTable tabla, string tipo, int mes)
        {
            if (tipo == "RE") { return new MqtCarga().CargaDataMqt(tabla, tipo, lbDato.Text, 0); }
            if (tipo == "PB") { return new MqtCarga().CargaDataMqt(tabla, tipo, lbDato.Text, 1); }
            if (tipo == "PY") { return new MqtCarga().CargaDataMqt(tabla, tipo, lbDato.Text, mes); }

            return false;
        }

        private void UpdateProgressBar(int porcentaje)
        {
            if (progressBar.InvokeRequired)
            {
                progressBar.Invoke(new MethodInvoker(delegate
                {
                    lbPorcentaje.Text = porcentaje.ToString() + "%";
                    progressBar.Value = porcentaje;
                }));
            }
            else
            {
                lbPorcentaje.Text = porcentaje.ToString() + "%";
                progressBar.Value = porcentaje;
            }
        }

        private void cmbTipo_SelectedIndexChanged(object sender, EventArgs e)
        {
            lbMes.Visible = false;
            cmbMes.Visible = false;
            if (cmbTipo.Text != "RE")
            {
                GetVersion();
            }
            else
            {
                lbDato.Text = funciones.getValorComboBox(cmbTipo, 2) + "-" + txtAnio.Text.Trim() + " - version 1";
                lbTexto.Visible = false;
                lbDato.Visible = false;
            }
        }

        private void txtAnio_KeyPress(object sender, KeyPressEventArgs e)
        {
            //Para obligar a que sólo se introduzcan números
            if (Char.IsDigit(e.KeyChar)) e.Handled = false;
            else
            {
                if (Char.IsControl(e.KeyChar)) e.Handled = false; //permitir teclas de control como retroceso
                else e.Handled = true; //el resto de teclas pulsadas se desactivan
            }
        }

        private void listBoxArchivos_SelectedIndexChanged(object sender, EventArgs e)
        {
            int indSelec = listBoxArchivos.SelectedIndex;
            
            if (indSelec >= 0)
            {
                listBoxRegMont.SetSelected(indSelec, true);
                listBoxMntTotal.SetSelected(indSelec, true);
            }
        }

        private void cmbMes_SelectedIndexChanged(object sender, EventArgs e)
        {
             GetVersion();
                

        }

        private void GetVersion()
        {
            lbTexto.Visible = false;
            lbDato.Visible = true;
            string tipo = funciones.getValorComboBox(cmbTipo, 2);
            string anio = txtAnio.Text.Trim();
            string mes = funciones.getValorComboBox(cmbMes, 2);
            string query = "SELECT ISNULL(MAX(Versi),0)+1 AS total FROM Mqt_Version (NOLOCK) WHERE Tipo='" + tipo + "' AND Anio='" + anio + "' AND MONTH(Fecha) = '"+ mes+" '";
            

            //MessageBox.Show("El mes seleccionado es  " + mes);


            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION_2");
            conexion.Open();

            try
            {
                SqlCommand myCommand = new SqlCommand(query, conexion);
                SqlDataReader dr = myCommand.ExecuteReader();

                vers = 1;
                if (dr.Read()) vers = dr.GetInt32(dr.GetOrdinal("total"));

                lbDato.Text = tipo + "-" + anio + " - version " + vers;
                lbTexto.Visible = true;
                
                if (tipo == "PY")
                {
                    lbMes.Visible = true;
                    cmbMes.Visible = true;
                }

                dr.Close();
                myCommand.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally { conexion.Close(); }
        }
    }
}
