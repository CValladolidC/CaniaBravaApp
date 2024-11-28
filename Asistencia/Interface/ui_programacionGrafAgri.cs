using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Linq;
using OfficeOpenXml;

namespace CaniaBrava
{
    public partial class ui_programacionGrafAgri : Form
    {
        Funciones funciones = new Funciones();
        private Form FormPadre;
        public Form _FormPadre
        {
            get { return FormPadre; }
            set { FormPadre = value; }
        }

        private TextBox TextBoxActivo;
        public TextBox _TextBoxActivo
        {
            get { return TextBoxActivo; }
            set { TextBoxActivo = value; }
        }

        public ui_programacionGrafAgri()
        {
            InitializeComponent();

            if (funciones.VersionAssembly()) Application.ExitThread();
        }

        private void ui_programacionGrafAgri_Load(object sender, EventArgs e)
        {
            ui_ListaProg();
        }

        private void ui_ListaProg()
        {
            GlobalVariables variables = new GlobalVariables();
            string query = " SELECT a.idprog [Cod.],a.desprog [Programación],CONVERT(VARCHAR(10), a.fechaini, 120) AS [Fecha Inicio],CONVERT(VARCHAR(10), a.fechafin, 120) AS [Fecha Fin],";
            query += "a.idusrins [Usuario],a.fechains [F.Registro],a.Estado/*,a.idusrupd [U.Actualizado],a.fechaupd [F.Actualizado]*/ ";
            query += "FROM progagri a (NOLOCK) ";

            if (variables.getValorTypeUsr() != "00")
            {
                query += @"WHERE a.idusrins IN (SELECT distinct idusr FROM cencosusr (NOLOCK) WHERE idcencos in (
                            SELECT idcencos FROM cencosusr (NOLOCK) WHERE idusr = '" + variables.getValorUsr() + "')) ";

                if (variables.getValorTypeUsr() == "03" && variables.getValorNivelUsr() == string.Empty)
                {
                    query += "AND a.idusrins = '" + variables.getValorUsr() + "'";
                }
            }

            query += "ORDER BY a.fechaini ";

            loadqueryDatos(query, dgvdetalle);
        }

        private void loadqueryDatos(string query, DataGridView dgv)
        {
            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();

            try
            {
                using (SqlDataAdapter myDataAdapter = new SqlDataAdapter(query, conexion))
                {
                    DataSet myDataSet = new DataSet();
                    myDataAdapter.Fill(myDataSet, "tblProgramacion");
                    funciones.formatearDataGridView(dgv);

                    dgv.DataSource = myDataSet.Tables["tblProgramacion"];
                    dgv.Columns[1].Width = 230;
                    dgv.Columns[2].Width = 75;
                    dgv.Columns[3].Width = 69;
                    dgv.Columns[5].Width = 120;

                    dgv.Columns[0].Visible = false;

                    dgv.AllowUserToResizeRows = false;
                    dgv.AllowUserToResizeColumns = false;
                    foreach (DataGridViewColumn column in dgv.Columns)
                    {
                        column.SortMode = DataGridViewColumnSortMode.NotSortable;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }

            conexion.Close();
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            ui_programacionInicialAgri ui_detalle = new ui_programacionInicialAgri();
            this._TextBoxActivo = new TextBox();
            ui_detalle._FormPadre = this;
            ui_detalle.Nuevo(string.Empty, 0, false);
            ui_detalle.Activate();
            ui_detalle.BringToFront();
            ui_detalle.ShowDialog();
            ui_detalle.Dispose();

            if (this._TextBoxActivo.Text != string.Empty)
            {
                ui_updprogramacionGrafAgri ui_detall = new ui_updprogramacionGrafAgri();
                string[] arr = this._TextBoxActivo.Text.Split('|');
                ui_detall.LoadDatos(arr[0], arr[1], arr[2], arr[3]);
                ui_detall.Activate();
                ui_detall.BringToFront();
                ui_detall.ShowDialog();
                ui_detall.Dispose();
            }
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            Int32 selectedCellCount = dgvdetalle.GetCellCount(DataGridViewElementStates.Selected);
            if (selectedCellCount > Convert.ToInt32(0))
            {
                string id = dgvdetalle.Rows[dgvdetalle.SelectedCells[1].RowIndex].Cells[0].Value.ToString();
                string desc = dgvdetalle.Rows[dgvdetalle.SelectedCells[1].RowIndex].Cells[1].Value.ToString();
                string fec1 = dgvdetalle.Rows[dgvdetalle.SelectedCells[1].RowIndex].Cells[2].Value.ToString();
                string fec2 = dgvdetalle.Rows[dgvdetalle.SelectedCells[1].RowIndex].Cells[3].Value.ToString();

                ui_updprogramacionGrafAgri ui_detall = new ui_updprogramacionGrafAgri();
                ui_detall.LoadDatos(id, fec1, fec2, desc);
                ui_detall.Activate();
                ui_detall.BringToFront();
                ui_detall.ShowDialog();
                ui_detall.Dispose();
            }
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            ui_ListaProg();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            Int32 selectedCellCount = dgvdetalle.GetCellCount(DataGridViewElementStates.Selected);
            if (selectedCellCount > Convert.ToInt32(0))
            {
                string id = dgvdetalle.Rows[dgvdetalle.SelectedCells[1].RowIndex].Cells[0].Value.ToString();
                string query = @"SELECT CONVERT(VARCHAR(10),a.Fecha,120) [Fecha],b.desusr [Tareador],a.Fundo,a.Equipo,a.Turno,c.desmaesgen [Actividad],a.cantidadprog [#Trab.], 
a.cantidadreal [Jor.Real],a.Area, 
CASE WHEN a.Area>0 THEN (a.cantidadprog/a.Area) ELSE 0 END [R.Prog], 
CASE WHEN a.Area>0 THEN (a.cantidadreal/a.Area) ELSE 0 END [R.Real] 
FROM progagri_fecafueqtuac (NOLOCK) a 
INNER JOIN usrfile (NOLOCK) b ON b.idusr=a.capataz 
INNER JOIN maesgen (NOLOCK) c ON c.idmaesgen='162' AND c.clavemaesgen=a.actividad 
WHERE a.idprog='" + id + "' ORDER BY a.Fecha";

                loadqueryDatos(query, dgvreporte);

                Exporta exporta = new Exporta();
                exporta.Excel_FromDataGridView(dgvreporte);
            }
        }

        private void btnCopiar_Click(object sender, EventArgs e)
        {
            Int32 selectedCellCount = dgvdetalle.GetCellCount(DataGridViewElementStates.Selected);
            if (selectedCellCount > Convert.ToInt32(0))
            {
                string idprog = dgvdetalle.Rows[dgvdetalle.SelectedCells[1].RowIndex].Cells[0].Value.ToString();
                string desprog = dgvdetalle.Rows[dgvdetalle.SelectedCells[1].RowIndex].Cells[1].Value.ToString();
                string fec1 = dgvdetalle.Rows[dgvdetalle.SelectedCells[1].RowIndex].Cells[2].Value.ToString();
                string fec2 = dgvdetalle.Rows[dgvdetalle.SelectedCells[1].RowIndex].Cells[3].Value.ToString();
                DateTime Fec1 = DateTime.Parse(fec1);
                DateTime Fec2 = DateTime.Parse(fec2);
                int dias = (int)Fec2.Subtract(Fec1).TotalDays;
                var confirmResult = MessageBox.Show("¿Desea realizar copia de: \"" + desprog + "\" ?", "Confirmación", MessageBoxButtons.YesNo);
                if (confirmResult == DialogResult.Yes)
                {
                    ui_programacionInicialAgri ui_detalle = new ui_programacionInicialAgri();
                    this._TextBoxActivo = new TextBox();
                    ui_detalle._FormPadre = this;
                    ui_detalle.Nuevo(idprog, dias, false);
                    ui_detalle.Activate();
                    ui_detalle.BringToFront();
                    ui_detalle.ShowDialog();
                    ui_detalle.Dispose();

                    //if (this._TextBoxActivo.Text != string.Empty)
                    //{
                    //    ui_updprogramacionGrafAgri ui_detall = new ui_updprogramacionGrafAgri();
                    //    string[] arr = this._TextBoxActivo.Text.Split('|');
                    //    ui_detall.LoadDatos(arr[0], arr[1], arr[2], arr[3]);
                    //    ui_detall.Activate();
                    //    ui_detall.BringToFront();
                    //    ui_detall.ShowDialog();
                    //    ui_detall.Dispose();
                    //}
                }
            }
        }

        private void btnReprogramar_Click(object sender, EventArgs e)
        {
            Int32 selectedCellCount = dgvdetalle.GetCellCount(DataGridViewElementStates.Selected);
            if (selectedCellCount > Convert.ToInt32(0))
            {
                string idprog = dgvdetalle.Rows[dgvdetalle.SelectedCells[1].RowIndex].Cells[0].Value.ToString();
                string desprog = dgvdetalle.Rows[dgvdetalle.SelectedCells[1].RowIndex].Cells[1].Value.ToString();
                string fec1 = dgvdetalle.Rows[dgvdetalle.SelectedCells[1].RowIndex].Cells[2].Value.ToString();
                string fec2 = dgvdetalle.Rows[dgvdetalle.SelectedCells[1].RowIndex].Cells[3].Value.ToString();
                DateTime Fec1 = DateTime.Parse(fec1);
                DateTime Fec2 = DateTime.Parse(fec2);
                int dias = (int)Fec2.Subtract(Fec1).TotalDays;

                if (Fec2.Date >= DateTime.Now.Date)
                {
                    var confirmResult = MessageBox.Show("¿Desea re-programar la: \"" + desprog + "\" ?", "Confirmación", MessageBoxButtons.YesNo);
                    if (confirmResult == DialogResult.Yes)
                    {
                        ui_reprogramacionInicialAgri ui_detalle = new ui_reprogramacionInicialAgri();
                        this._TextBoxActivo = new TextBox();
                        ui_detalle._FormPadre = this;
                        ui_detalle._Load(idprog, Fec1, Fec2);
                        ui_detalle.Activate();
                        ui_detalle.BringToFront();
                        ui_detalle.ShowDialog();
                        ui_detalle.Dispose();
                    }
                }
                else
                {
                    MessageBox.Show("No se puede reprogramar. Programación fuera de rango", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            Int32 selectedCellCount = dgvdetalle.GetCellCount(DataGridViewElementStates.Selected);
            if (selectedCellCount > 0)
            {
                GlobalVariables variables = new GlobalVariables();
                string idprog = dgvdetalle.Rows[dgvdetalle.SelectedCells[1].RowIndex].Cells[0].Value.ToString();
                string fec1 = dgvdetalle.Rows[dgvdetalle.SelectedCells[1].RowIndex].Cells[2].Value.ToString();
                string fec2 = dgvdetalle.Rows[dgvdetalle.SelectedCells[1].RowIndex].Cells[3].Value.ToString();
                DateTime Fec1 = DateTime.Parse(fec1);
                DateTime Fec2 = DateTime.Parse(fec2);

                bool sigue = true;
                if (variables.getValorUsr() != "00")
                {
                    sigue = false;
                    if (Fec1.Date > DateTime.Now.Date) { sigue = true; }
                }

                if (sigue)
                {
                    var confirmResult = MessageBox.Show("¿Esta seguro en Eliminar del registro seleccionado?", "Confirmación", MessageBoxButtons.YesNo);
                    if (confirmResult == DialogResult.Yes)
                    {
                        string query = string.Empty;

                        SqlConnection conexion = new SqlConnection();
                        conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
                        conexion.Open();

                        query = " DELETE FROM progagri_fecafueqtuac WHERE idprog = '" + idprog + "';";
                        query += "DELETE FROM progagri_fecafueqtu WHERE idprog = '" + idprog + "';";
                        query += "DELETE FROM progagri_fecafueq WHERE idprog = '" + idprog + "';";
                        query += "DELETE FROM progagri_fecafu WHERE idprog = '" + idprog + "';";
                        query += "DELETE FROM progagri_feca WHERE idprog = '" + idprog + "';";
                        query += "DELETE FROM progagri WHERE idprog = '" + idprog + "';";

                        try
                        {
                            SqlCommand myCommand = new SqlCommand(query, conexion);
                            myCommand.ExecuteNonQuery();
                            myCommand.Dispose();
                            MessageBox.Show("Registro Eliminado..", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            ui_ListaProg();
                        }
                        catch (SqlException ex)
                        {
                            MessageBox.Show(ex.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        }
                        conexion.Close();
                    }
                }
            }
            else
            {
                MessageBox.Show("No ha seleccionado registro a Eliminar", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void btnFormato_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process[] process = System.Diagnostics.Process.GetProcessesByName("Excel");
            foreach (System.Diagnostics.Process p in process)
            {
                if (!string.IsNullOrEmpty(p.ProcessName))
                {
                    try
                    {
                        p.Kill();
                    }
                    catch { }
                }
            }

            SaveFileDialog CuadroDialogo = new SaveFileDialog();
            CuadroDialogo.DefaultExt = "xlsx";
            CuadroDialogo.Filter = "xlsx file(*.xlsx)|*.xlsx";
            CuadroDialogo.AddExtension = true;
            CuadroDialogo.RestoreDirectory = true;
            CuadroDialogo.FileName = "FormatoProgActividades";
            CuadroDialogo.Title = "Guardar";
            CuadroDialogo.InitialDirectory = @"c:\";
            if (CuadroDialogo.ShowDialog() == DialogResult.OK)
            {
                CreateExcel(CuadroDialogo.FileName);
            }
        }

        public void CreateExcel(string filename)
        {
            GlobalVariables variables = new GlobalVariables();
            Microsoft.Office.Interop.Excel.Application xlApp = new Microsoft.Office.Interop.Excel.Application();

            if (xlApp == null)
            {
                MessageBox.Show("MS Excel no esta instalado!!");
                return;
            }

            Microsoft.Office.Interop.Excel.Workbook xlWorkBook;
            Microsoft.Office.Interop.Excel.Worksheet xlWorkSheet;
            object misValue = System.Reflection.Missing.Value;

            xlWorkBook = xlApp.Workbooks.Add(misValue);
            xlWorkSheet = (Microsoft.Office.Interop.Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);

            xlWorkSheet.Cells[1, 1] = "Tareador";
            xlWorkSheet.Cells[1, 2] = "FDO";
            xlWorkSheet.Cells[1, 3] = "EQ";
            xlWorkSheet.Cells[1, 4] = "TR";
            xlWorkSheet.Cells[1, 5] = "COD.ACT";
            xlWorkSheet.Cells[1, 6] = DateTime.Now.ToString("yyyy-MM-dd");

            xlWorkSheet.Cells[2, 1] = "Ejm: \r\n " + variables.getValorUsr();
            xlWorkSheet.Cells[2, 2] = "Ejm: \r\n LB01";
            xlWorkSheet.Cells[2, 3] = "Ejm: \r\n EQ01";
            xlWorkSheet.Cells[2, 4] = "Ejm: \r\n TR01";
            xlWorkSheet.Cells[2, 5] = "Ejm: \r\n 2203";
            xlWorkSheet.Cells[2, 6] = "Ejm: \r\n 10";

            //Here saving the file in xlsx
            xlWorkBook.SaveAs(filename, Microsoft.Office.Interop.Excel.XlFileFormat.xlOpenXMLWorkbook, misValue,
            misValue, misValue, misValue, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue);

            xlWorkBook.Close(true, misValue, misValue);
            xlApp.Quit();

            Marshal.ReleaseComObject(xlWorkSheet);
            Marshal.ReleaseComObject(xlWorkBook);
            Marshal.ReleaseComObject(xlApp);

            MessageBox.Show("Archivo excel creado. La ruta es: " + filename);
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Excel files | *.xlsx;.xls"; // file types, that will be allowed to upload
            dialog.ValidateNames = true;
            dialog.Multiselect = false; // allow/deny user to upload more than one file at a time
            if (dialog.ShowDialog() == DialogResult.OK) // if user clicked OK
            {
                bool continue_ = true;
                String path = dialog.FileName; // get name of file

                int row_ = 2, column_ = 1, row_data1 = 0, row_data2 = 0, colum_data = 6;
                List<Progagridet> lista = new List<Progagridet>();
                FileInfo fileInfo = new FileInfo(path);
                using (ExcelPackage xlPackage = new ExcelPackage(fileInfo))
                {
                    ExcelWorksheet ws = null;

                    ws = xlPackage.Workbook.Worksheets[1];
                    if (ws != null)
                    {
                        #region "Find data start"
                        bool valide = true;
                        row_ = 2;
                        column_ = 1;
                        while (valide)
                        {
                            if (ws.Cells[row_, column_].Value != null)
                            {
                                row_data1 = row_; break;
                            }
                            else { valide = false; }
                        }
                        #endregion

                        #region "Find data end"
                        valide = true;
                        row_ = 2;
                        column_ = 1;
                        row_data2 = row_data1;
                        while (valide)
                        {
                            if (ws.Cells[row_, column_].Value != null)
                            {
                                row_data2++;
                                row_++;
                            }
                            else { valide = false; }
                        }
                        #endregion

                        #region "Find column end"
                        valide = true;
                        row_ = 1;
                        column_ = 6;
                        while (valide)
                        {
                            if (ws.Cells[row_, column_].Value != null)
                            {
                                DateTime value;
                                if (DateTime.TryParse(ws.Cells[row_, column_].Value.ToString(), out value))
                                {
                                    colum_data++;
                                    column_++;
                                }
                                else { valide = false; }
                            }
                            else { valide = false; }
                        }
                        #endregion

                        if (row_data1 > 0 && row_data2 > 0 && colum_data > 7)
                        {
                            for (int i = row_data1; i < row_data2; i++)
                            {
                                for (int y = 6; y <= colum_data; y++)
                                {
                                    if (ws.Cells[i, y].Value != null)
                                    {
                                        lista.Add(new Progagridet()
                                        {
                                            capataz = ws.Cells[i, 1].Value.ToString(),
                                            fundo = ws.Cells[i, 2].Value.ToString(),
                                            equipo = ws.Cells[i, 3].Value.ToString(),
                                            turno = ws.Cells[i, 4].Value.ToString(),
                                            actividad = ws.Cells[i, 5].Value.ToString(),
                                            cantidadprog = int.Parse(ws.Cells[i, y].Value.ToString()),
                                            fecha = DateTime.Parse(ws.Cells[1, y].Value.ToString())
                                        });
                                    }
                                }
                            }
                        }
                        else
                        {
                            MessageBox.Show("El archivo Excel no contiene informacion correcta... Favor de verificar", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            continue_ = false;
                        }
                    }
                }

                if (continue_)
                {
                    if (lista.Count > 0)
                    {
                        GlobalVariables variables = new GlobalVariables();
                        string query = string.Empty;

                        string usu = variables.getValorUsr();
                        var fecini = lista.Min(x => x.fecha);
                        var fecfin = lista.Max(x => x.fecha);

                        if (fecini.Date > DateTime.Now.Date)
                        {
                            bool continua = true;
                            if (ExisteProgari(fecini))
                            {
                                var confirmResult = MessageBox.Show("Existe fecha(s) en la carga masiva de Programación de Actividades.\n¿Desea continuar?", "Confirmación", MessageBoxButtons.YesNo);
                                if (confirmResult == DialogResult.No)
                                {
                                    continua = false;
                                }
                            }

                            if(continua)
                            {
                                query += @"INSERT INTO progagri VALUES ((SELECT ISNULL(MAX(idprog),1)+1 FROM progagri (NOLOCK)),'PROGRAMACION " +
                                fecini.ToString("yyyy-MM-dd") + " AL " + fecfin.ToString("yyyy-MM-dd") + "','" + fecini.ToString("yyyy-MM-dd") + "','" +
                                fecfin.ToString("yyyy-MM-dd") + "','V','" + usu + "',GETDATE(),'" + usu + "',GETDATE());";

                                var data_feca = lista.GroupBy(x => new { x.fecha, x.capataz }).ToList();
                                foreach (var item in data_feca)
                                {
                                    query += @"INSERT INTO progagri_feca VALUES ((SELECT ISNULL(MAX(idprog),1) FROM progagri (NOLOCK)),'" + item.Key.fecha.ToString("yyyy-MM-dd") + "','" +
            item.Key.capataz + "');\r\n";
                                }

                                var data_fecafu = lista.GroupBy(x => new { x.fecha, x.capataz, x.fundo }).ToList();
                                foreach (var item in data_fecafu)
                                {
                                    query += @"INSERT INTO progagri_fecafu VALUES ((SELECT ISNULL(MAX(idprog),1) FROM progagri (NOLOCK)),'" + item.Key.fecha.ToString("yyyy-MM-dd") + "','" +
            item.Key.capataz + "','" + item.Key.fundo + "');\r\n";
                                }

                                var data_fecafueq = lista.GroupBy(x => new { x.fecha, x.capataz, x.fundo, x.equipo }).ToList();
                                foreach (var item in data_fecafueq)
                                {
                                    query += @"INSERT INTO progagri_fecafueq VALUES ((SELECT ISNULL(MAX(idprog),1) FROM progagri (NOLOCK)),'" + item.Key.fecha.ToString("yyyy-MM-dd") + "','" +
            item.Key.capataz + "','" + item.Key.fundo + "','" + item.Key.equipo + "');\r\n";
                                }

                                var data_fecafueqtu = lista.GroupBy(x => new { x.fecha, x.capataz, x.fundo, x.equipo, x.turno }).ToList();
                                foreach (var item in data_fecafueqtu)
                                {
                                    query += @"INSERT INTO progagri_fecafueqtu VALUES ((SELECT ISNULL(MAX(idprog),1) FROM progagri (NOLOCK)),'" + item.Key.fecha.ToString("yyyy-MM-dd") + "','" +
            item.Key.capataz + "','" + item.Key.fundo + "','" + item.Key.equipo + "','" + item.Key.turno + "');\r\n";
                                }

                                foreach (var item in lista)
                                {
                                    query += @"INSERT INTO progagri_fecafueqtuac VALUES ((SELECT ISNULL(MAX(idprog),1) FROM progagri (NOLOCK)),'" + item.fecha.ToString("yyyy-MM-dd") + "','" +
            item.capataz + "','" + item.fundo + "','" + item.equipo + "','" + item.turno + "','" + item.actividad + "','" + item.cantidadprog + "',-1,1,0,'');\r\n";
                                }

                                SqlConnection conexion = new SqlConnection();
                                conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
                                conexion.Open();

                                try
                                {
                                    SqlCommand myCommand = new SqlCommand(query, conexion);
                                    myCommand.ExecuteNonQuery();
                                    myCommand.Dispose();
                                    MessageBox.Show("Carga exitosa de Programacion de actividades..!!", "Exito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                                catch (SqlException ex)
                                {
                                    MessageBox.Show(ex.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                                }
                                conexion.Close();
                            }
                        }
                    }
                }
            }

            ui_ListaProg();
        }

        private bool ExisteProgari(DateTime fecinicial)
        {
            bool valida = false;
            GlobalVariables variables = new GlobalVariables();
            string query = "SELECT COUNT(1) FROM progagri (NOLOCK) WHERE idusrins = '" + variables.getValorUsr() + "' ";
            query += "AND '" + fecinicial.ToString("yyyy-MM-dd") + "' BETWEEN fechaini AND fechafin ";

            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();

            try
            {
                SqlCommand myCommand = new SqlCommand(query, conexion);
                SqlDataReader odr = myCommand.ExecuteReader();

                while (odr.Read())
                {
                    valida = true;
                }

                odr.Close();
                myCommand.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            finally { conexion.Close(); }

            return valida;
        }
    }
}