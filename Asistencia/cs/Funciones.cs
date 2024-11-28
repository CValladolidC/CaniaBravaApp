using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Data;
using System.Windows.Forms;
using System.Drawing;
using System.Globalization;
using System.Text.RegularExpressions;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using CaniaBrava.ds;
using System.Data.SqlClient;
using System.IO;
using Excel = Microsoft.Office.Interop.Excel;
using System.Net;
using System.Net.Mail;


namespace CaniaBrava
{
    class Funciones
    {
        String query = String.Empty;
        GlobalVariables variables = new GlobalVariables();


        public bool VersionAssembly()
        {
            bool result = false;
            var vActual = "version: " + Application.ProductVersion;
            FileInfo fa = new FileInfo(Application.StartupPath + "/license.txt");
            if (fa.Exists)
            {
                OpeIO opeIO = new OpeIO(Application.StartupPath + "/license.txt");
                if (opeIO.ReadLineByNum(10) != vActual)
                {
                    MessageBox.Show("El sistema se actualizará automaticamente. Vuelva a abrir SISASIS", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    result = true;
                }
            }

            return result;
        }

        public string longitudCadena(string texto, int longitud)
        {
            texto = texto.Trim() + replicateCadena(" ", longitud);
            texto = texto.Substring(0, longitud);
            return texto;
        }

        public string generaNumeroAleatorio()
        {
            Random r;
            r = new Random();
            return r.Next(10000000, 99999999).ToString();
        }

        public string formatoPdtNumerico(string texto)
        {
            texto = texto.Replace(@",", "");
            return texto;
        }

        public decimal getDecimalRound(decimal Argument, int Digits)
        {
            return decimal.Round(Argument, Digits);
        }

        public string formatoLongitudPdt(string texto, int longitud)
        {
            texto = texto.Trim() + replicateCadena(" ", longitud);
            texto = texto.Substring(0, longitud);
            return texto;
        }

        public string sumaColumnaDataGridView(DataGridView dgvdetalle, int columna)
        {
            double sum = 0;
            int i = dgvdetalle.RowCount;
            for (int x = 0; x < i; x++)
            {
                sum = sum + double.Parse(dgvdetalle[columna, x].Value.ToString());
            }
            return sum.ToString();
        }

        public string replicateCadena(string cadenaARepetir, int numeroVeces)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < numeroVeces; i++)
                sb.Append(cadenaARepetir);
            return sb.ToString();

        }

        public void listaComboBox(string squery, ComboBox cb, string tipo)
        {
            string valorItem;
            string valorItemIni = string.Empty;

            SqlConnection conexion = new SqlConnection();
            try
            {
                conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
                conexion.Open();

                SqlCommand myCommand_table = new SqlCommand(squery, conexion);
                System.Data.DataTable dt = new System.Data.DataTable();
                dt.Load(myCommand_table.ExecuteReader());
                myCommand_table.Dispose();

                cb.Items.Clear();

                if (tipo.Equals("B"))
                {
                    valorItem = replicateCadena(" ", 30);
                    cb.Items.Add(valorItem);
                }

                if (tipo.Equals("X"))
                {
                    cb.Items.Add("X   TODOS");
                }

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    valorItem = (String)dt.Rows[i]["clave"];
                    valorItem = valorItem + replicateCadena(" ", 2) + (String)dt.Rows[i]["descripcion"];
                    if (i == 0)
                    {
                        valorItemIni = valorItem;
                    }
                    cb.Items.Add(valorItem);

                }
                if (tipo.Equals("B"))
                {
                    cb.Text = replicateCadena(" ", 30);
                }
                else
                {
                    if (tipo.Equals("X"))
                    {
                        cb.Text = "X   TODOS";
                    }
                    else
                    {
                        cb.Text = valorItemIni;
                    }
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            conexion.Close();
        }

        public void listaComboBoxUnCampo(string squery, ComboBox cb, string tipo)
        {
            string valorItem;
            string valorItemIni = string.Empty;

            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();
            try
            {
                SqlCommand myCommand_table = new SqlCommand(squery, conexion);
                System.Data.DataTable dt = new System.Data.DataTable();
                dt.Load(myCommand_table.ExecuteReader());
                myCommand_table.Dispose();
                Funciones funcionusr = new Funciones();
                cb.Items.Clear();

                if (tipo.Equals("B"))
                {
                    valorItem = funcionusr.replicateCadena(" ", 30);
                    cb.Items.Add(valorItem);
                }

                if (tipo.Equals("X"))
                {
                    cb.Items.Add("X   TODOS");
                }

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    valorItem = (String)dt.Rows[i]["descripcion"];
                    if (i == 0)
                    {
                        valorItemIni = valorItem;
                    }
                    cb.Items.Add(valorItem);

                }
                if (tipo.Equals("B"))
                {
                    cb.Text = funcionusr.replicateCadena(" ", 30);
                }
                else
                {
                    if (tipo.Equals("X"))
                    {
                        cb.Text = "X   TODOS";
                    }
                    else
                    {
                        cb.Text = valorItemIni;
                    }
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);

            }
            conexion.Close();
        }

        public void clearComboBox(ComboBox cb)
        {
            cb.Items.Clear();
        }

        public void listaToolStripComboBox(string squery, ToolStripComboBox cb)
        {
            string valorItem;
            string valorItemIni = string.Empty;

            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();
            try
            {
                SqlCommand myCommand_table = new SqlCommand(squery, conexion);
                System.Data.DataTable dt = new System.Data.DataTable();
                dt.Load(myCommand_table.ExecuteReader());
                myCommand_table.Dispose();
                Funciones funcionusr = new Funciones();
                cb.Items.Clear();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    valorItem = (String)dt.Rows[i]["clave"];
                    valorItem = valorItem + funcionusr.replicateCadena(" ", 2) + (String)dt.Rows[i]["descripcion"];

                    if (i == 0)
                    {
                        valorItemIni = valorItem;
                    }
                    cb.Items.Add(valorItem);
                }
                cb.Text = valorItemIni;
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            conexion.Close();
        }

        public void listaToolStripComboBoxUnCampo(string squery, ToolStripComboBox cb)
        {
            string valorItem;
            string valorItemIni = string.Empty;

            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();
            try
            {
                SqlCommand myCommand_table = new SqlCommand(squery, conexion);
                System.Data.DataTable dt = new System.Data.DataTable();
                dt.Load(myCommand_table.ExecuteReader());
                myCommand_table.Dispose();
                Funciones funcionusr = new Funciones();
                cb.Items.Clear();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    valorItem = (String)dt.Rows[i]["descripcion"];

                    if (i == 0)
                    {
                        valorItemIni = valorItem;
                    }
                    cb.Items.Add(valorItem);
                }
                cb.Text = valorItemIni;
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            conexion.Close();
        }

        public void validarCombobox(string squery, ComboBox cb)
        {
            string valorItem;

            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();
            try
            {
                Funciones funcionusr = new Funciones();
                SqlCommand myCommand_table = new SqlCommand(squery, conexion);
                System.Data.DataTable dt_table = new System.Data.DataTable();
                dt_table.Load(myCommand_table.ExecuteReader());
                myCommand_table.Dispose();
                if (dt_table.Rows.Count > Convert.ToInt16(0))
                {
                    valorItem = (String)dt_table.Rows[0]["clave"];
                    cb.Text = valorItem + funcionusr.replicateCadena(" ", 2) + (String)dt_table.Rows[0]["descripcion"];
                }
                else
                {
                    MessageBox.Show("Registro no encontrado", "Aviso Importante", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    cb.Text = "";
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            conexion.Close();
        }

        public string getValorComboBox(ComboBox cb, int caracteres)
        {
            string valor;
            string valortmp;

            valortmp = cb.Text + replicateCadena(" ", caracteres);
            valor = valortmp.Substring(0, caracteres).Trim();
            return valor;
        }

        public string getValorToolStripComboBox(ToolStripComboBox cb, int caracteres)
        {
            string valor;
            string valortmp;

            valortmp = cb.Text + replicateCadena(" ", caracteres);
            valor = valortmp.Substring(0, caracteres).Trim();
            return valor;
        }

        public void consultaComboBox(string squery, ComboBox cb)
        {
            string valorItem;
            Funciones funcionusr = new Funciones();

            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();
            try
            {
                SqlCommand myCommand_table = new SqlCommand(squery, conexion);
                System.Data.DataTable dt_table = new System.Data.DataTable();
                dt_table.Load(myCommand_table.ExecuteReader());
                myCommand_table.Dispose();

                if (dt_table.Rows.Count > Convert.ToInt16(0))
                {
                    valorItem = (String)dt_table.Rows[0]["clave"];
                    cb.Text = valorItem + funcionusr.replicateCadena(" ", 2) + (String)dt_table.Rows[0]["descripcion"];
                }
                else
                {
                    cb.Text = "";
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            conexion.Close();
        }

        public void consultaComboBoxUnCampo(string squery, ComboBox cb)
        {
            string valorItem;
            Funciones funcionusr = new Funciones();

            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();
            try
            {
                SqlCommand myCommand_table = new SqlCommand(squery, conexion);
                System.Data.DataTable dt_table = new System.Data.DataTable();
                dt_table.Load(myCommand_table.ExecuteReader());
                myCommand_table.Dispose();

                if (dt_table.Rows.Count > Convert.ToInt16(0))
                {
                    valorItem = (String)dt_table.Rows[0]["descripcion"];
                    cb.Text = valorItem;
                }
                else
                {
                    cb.Text = "";
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            conexion.Close();
        }

        public void consultaRpts(string rp_cindice, string rp_ccodigo, System.Windows.Forms.TextBox tx)
        {
            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();
            string query = "Select rtrim(rp_ccodigo)+'   '+rtrim(rp_cdescri) as descripcion from tgrpts where rp_cindice='" + @rp_cindice + "' and rp_ccodigo='" + @rp_ccodigo + "';";
            try
            {
                SqlCommand myCommand = new SqlCommand(query, conexion);
                System.Data.DataTable dt = new System.Data.DataTable();
                dt.Load(myCommand.ExecuteReader());
                myCommand.Dispose();
                if (dt.Rows.Count > 0)
                {
                    tx.Text = (String)dt.Rows[0]["descripcion"];
                }
                else
                {
                    tx.Text = string.Empty;
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            conexion.Close();
        }

        public string verificaItemComboBox(string squery, ComboBox cb)
        {
            string valorItem;
            string resultado = string.Empty;
            Funciones funcionusr = new Funciones();

            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();
            try
            {
                SqlCommand myCommand_table = new SqlCommand(squery, conexion);
                System.Data.DataTable dt_table = new System.Data.DataTable();
                dt_table.Load(myCommand_table.ExecuteReader());
                myCommand_table.Dispose();
                if (dt_table.Rows.Count > 0)
                {
                    valorItem = (String)dt_table.Rows[0]["clave"];
                    resultado = valorItem + funcionusr.replicateCadena(" ", 2) + (String)dt_table.Rows[0]["descripcion"];

                }
                else
                {
                    resultado = string.Empty;
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            conexion.Close();
            return resultado;
        }

        public void formatearDataGridView(System.Windows.Forms.DataGridView dgvdetalle)
        {
            dgvdetalle.RowHeadersVisible = false;
            dgvdetalle.AllowUserToAddRows = false;
            dgvdetalle.DefaultCellStyle.SelectionBackColor = Color.Black;
            dgvdetalle.DefaultCellStyle.SelectionForeColor = Color.White;
            dgvdetalle.RowHeadersDefaultCellStyle.SelectionBackColor = Color.Empty;
            dgvdetalle.RowsDefaultCellStyle.BackColor = Color.LightGray;
            dgvdetalle.AlternatingRowsDefaultCellStyle.BackColor = Color.DarkGray;
            dgvdetalle.AutoGenerateColumns = true;
            dgvdetalle.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvdetalle.MultiSelect = false;
            dgvdetalle.ReadOnly = true;
            //dgvdetalle.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
        }

        public void formatearDataGridViewCheck(System.Windows.Forms.DataGridView dgvdetalle)
        {
            dgvdetalle.RowHeadersVisible = false;
            dgvdetalle.AllowUserToAddRows = false;
            dgvdetalle.DefaultCellStyle.SelectionBackColor = Color.Black;
            dgvdetalle.DefaultCellStyle.SelectionForeColor = Color.White;
            dgvdetalle.RowHeadersDefaultCellStyle.SelectionBackColor = Color.Empty;
            dgvdetalle.RowsDefaultCellStyle.BackColor = Color.White;
            dgvdetalle.AutoGenerateColumns = true;
            dgvdetalle.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvdetalle.MultiSelect = false;
            dgvdetalle.RowTemplate.Height = 35;
            dgvdetalle.RowTemplate.MinimumHeight = 25;


        }

        public void formatearDataGridViewWhite(System.Windows.Forms.DataGridView dgvdetalle)
        {
            dgvdetalle.RowHeadersVisible = false;
            dgvdetalle.AllowUserToAddRows = false;
            //dgvdetalle.DefaultCellStyle.SelectionBackColor = Color.Black;
            //dgvdetalle.DefaultCellStyle.SelectionForeColor = Color.White;
            dgvdetalle.RowHeadersDefaultCellStyle.SelectionBackColor = Color.Empty;
            //dgvdetalle.RowsDefaultCellStyle.BackColor = Color.LightGray;
            //dgvdetalle.AlternatingRowsDefaultCellStyle.BackColor = Color.DarkGray;
            dgvdetalle.AutoGenerateColumns = true;
            dgvdetalle.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvdetalle.MultiSelect = false;
            dgvdetalle.ReadOnly = true;
            //dgvdetalle.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
        }

        public void formatearDataGridViewWhite2(System.Windows.Forms.DataGridView dgvdetalle)
        {
            dgvdetalle.RowHeadersVisible = false;
            dgvdetalle.AllowUserToAddRows = false;
            //dgvdetalle.DefaultCellStyle.SelectionBackColor = Color.Black;
            //dgvdetalle.DefaultCellStyle.SelectionForeColor = Color.White;
            dgvdetalle.RowHeadersDefaultCellStyle.SelectionBackColor = Color.Empty;
            //dgvdetalle.RowsDefaultCellStyle.BackColor = Color.LightGray;
            //dgvdetalle.AlternatingRowsDefaultCellStyle.BackColor = Color.DarkGray;
            dgvdetalle.AutoGenerateColumns = true;
            //dgvdetalle.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvdetalle.MultiSelect = false;
            dgvdetalle.ReadOnly = true;
            //dgvdetalle.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            dgvdetalle.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
            dgvdetalle.AllowUserToResizeColumns = false;
            dgvdetalle.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;
            dgvdetalle.AllowUserToResizeRows = false;
        }

        static public Boolean IsFecha(string cadenaFecha) { CultureInfo es = new CultureInfo("es-ES"); DateTime fechaTemp; return DateTime.TryParseExact(cadenaFecha, "dd/MM/yyyy", es, DateTimeStyles.AdjustToUniversal, out fechaTemp); }

        static public string generaNumAleatorio()
        {
            Random r = new Random();
            string numero = r.Next(100000, 999999).ToString();
            return numero;
        }

        public Boolean email_bien_escrito(String email)
        {
            String expresion;
            expresion = "\\w+([-+.']\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*";
            if (Regex.IsMatch(email, expresion))
            {
                if (Regex.Replace(email, expresion, String.Empty).Length == 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        public ReportDocument pr_GeneraCrPayHorasExtra(DataTable HorasExtra)
        {
            String nombreCrystal = String.Empty;
            String nombreDataTable = String.Empty;
            nombreCrystal = "crHorasExtra";

            String rtnProcess = String.Empty;
            ReportDocument oRDoc = new ReportDocument();
            string strCrystalReportFilePath = null;
            DiskFileDestinationOptions oDfDopt = new DiskFileDestinationOptions();
            ExportOptions expo = new ExportOptions();
            //strCrystalReportFilePath = System.Web.Hosting.HostingEnvironment.MapPath("~\\cr\\" + nombreCrystal + ".rpt");
            strCrystalReportFilePath = System.IO.Path.GetFullPath("../../cr/crHorasExtra.rpt");

            oRDoc.Load(strCrystalReportFilePath);

            dtHorasExtra dsHorasExtra = new dtHorasExtra();
            DataSet ds = new DataSet();
            System.Data.DataTable dtReclamo = new DataTable();
            try
            {
                //dtHorasExtra = fr_FormCrHorasExtra(PayHorasExtra);
                HorasExtra.TableName = "HorasExtra";
                ds.Tables.Add(HorasExtra);
                dsHorasExtra.Merge(ds);

                oRDoc.SetDataSource(dsHorasExtra);
                oDfDopt.DiskFileName = @"C:\pdfHorasExtra.pdf";
                expo = oRDoc.ExportOptions;
                expo.ExportDestinationType = ExportDestinationType.DiskFile;
                expo.ExportFormatType = ExportFormatType.PortableDocFormat;
                expo.DestinationOptions = oDfDopt;

                oRDoc.Export();
            }
            catch (Exception ex) { }

            return oRDoc;
        }

        public DataTable fr_FormCrHorasExtra(string PayHorasExtra)
        {
            try
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("TicketPayHorasExtra", Type.GetType("System.String"));

                DataRow fila = dt.NewRow();
                fila[0] = PayHorasExtra;

                dt.Rows.Add(fila);

                return dt;
            }
            catch (Exception ex) { throw ex; }
        }

        public int valCampoEmpty(DateTimePicker dtpHor1, DateTimePicker dtpHor2)
        {
            int result = 1;
            if (dtpHor1.Value.Equals("") || dtpHor2.Value.Equals("")) { result = 0; }
            //result = valHor(dtpHor1, dtpHor2);
            return result;
        }

        private int valHor(DateTimePicker dtpHor1, DateTimePicker dtpHor2)
        {
            int result = 1;

            DateTime hora1 = Convert.ToDateTime(dtpHor1.Value);
            DateTime hora2 = Convert.ToDateTime(dtpHor2.Value);
            if (hora1 <= hora2)
            {
                if (hora1 < hora2) { result = 1; } else { result = 2; }
            }
            else { result = 2; }
            return result;
        }

        public string RightTxt(string original, int numberCharacters)
        {
            return original.Substring(original.Length - numberCharacters);
        }

        public void AsigHoraPer(string idcia, string bd, int IdAsigHor, string idPerPlan, string FecInicio, string FecFin, string Lu, string Ma, string Mi, string Ju, string Vi, string Sa, string Do)
        {
            string squery;

            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();
            squery = "CALL sp_AsignaHorPer('" + idcia + "', '" + bd + "', '" + IdAsigHor + "', '" + idPerPlan + "','" + FecInicio + "','" + FecFin + "','"
                + Lu + "','" + Ma + "','" + Mi + "','" + Ju + "','" + Vi + "','" + Sa + "','" + Do + "')";
            try
            {
                SqlCommand myCommand = new SqlCommand(squery, conexion);
                myCommand.ExecuteNonQuery();
                myCommand.Dispose();
                MessageBox.Show("Asignacion de Horario Correcto..");
            }
            catch (SqlException)
            {
                MessageBox.Show("No se ha podido realizar el proceso de Actualizacion", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            conexion.Close();
        }

        public string GetDefaultExtension(Microsoft.Office.Interop.Excel.Application application)
        {
            double version = Convert.ToDouble(application.Version);
            if (version >= 120.00) return ".xlsx";
            else return ".xls";
        }

        public void form_excel_kardex_4_1(string idcia, string idtipocal, string idtipoper, string messem, string anio, string fecini, string fecfin, Microsoft.Office.Interop.Excel.Worksheet vstoWorksheet)
        {
            DateTime lastFechaMes = last_fecha_mes(messem, anio);

            vstoWorksheet.Application.ActiveWindow.Zoom = 50;
            vstoWorksheet.Application.ActiveWindow.DisplayGridlines = false;
            vstoWorksheet.get_Range("A1").Value = "FORMATO 4.1: LIBRO DE RETENCIONES INCISOS E) Y F) DEL ART. 34° DE LA LEY DEL IMPUESTO A LA RENTA";
            vstoWorksheet.get_Range("A3").Value = "PERÍODO: " + DateTime.Parse(fecini).ToString("MMMM").ToUpper() + " - " + anio;
            vstoWorksheet.get_Range("A4").Value = "RUC: " + variables.getValorRucCia();
            vstoWorksheet.get_Range("A5").Value = "APELLIDOS Y NOMBRES, DENOMINACIÓN O RAZÓN SOCIAL: " + variables.getValorNameCia();

            //Inicio Bordes del Excel Kardex Sunat 13.1 --------------/
            vstoWorksheet.get_Range("A7").Borders[Microsoft.Office.Interop.Excel.XlBordersIndex.xlEdgeTop].LineStyle = 1;
            vstoWorksheet.get_Range("A8").Borders[Microsoft.Office.Interop.Excel.XlBordersIndex.xlEdgeTop].LineStyle = 1;

            vstoWorksheet.get_Range("A7").Borders[Microsoft.Office.Interop.Excel.XlBordersIndex.xlEdgeLeft].LineStyle = 1;
            vstoWorksheet.get_Range("A8").Borders[Microsoft.Office.Interop.Excel.XlBordersIndex.xlEdgeLeft].LineStyle = 1;
            vstoWorksheet.get_Range("A9").Borders[Microsoft.Office.Interop.Excel.XlBordersIndex.xlEdgeLeft].LineStyle = 1;
            vstoWorksheet.get_Range("A10").Borders[Microsoft.Office.Interop.Excel.XlBordersIndex.xlEdgeLeft].LineStyle = 1;

            vstoWorksheet.get_Range("B7:D7").Borders.LineStyle = 1;

            vstoWorksheet.get_Range("B8").Borders[Microsoft.Office.Interop.Excel.XlBordersIndex.xlEdgeLeft].LineStyle = 1;
            vstoWorksheet.get_Range("B9").Borders[Microsoft.Office.Interop.Excel.XlBordersIndex.xlEdgeLeft].LineStyle = 1;
            vstoWorksheet.get_Range("B10").Borders[Microsoft.Office.Interop.Excel.XlBordersIndex.xlEdgeLeft].LineStyle = 1;

            vstoWorksheet.get_Range("C8").Borders[Microsoft.Office.Interop.Excel.XlBordersIndex.xlEdgeLeft].LineStyle = 1;
            vstoWorksheet.get_Range("C9").Borders[Microsoft.Office.Interop.Excel.XlBordersIndex.xlEdgeLeft].LineStyle = 1;
            vstoWorksheet.get_Range("C10").Borders[Microsoft.Office.Interop.Excel.XlBordersIndex.xlEdgeLeft].LineStyle = 1;

            vstoWorksheet.get_Range("D8").Borders[Microsoft.Office.Interop.Excel.XlBordersIndex.xlEdgeLeft].LineStyle = 1;
            vstoWorksheet.get_Range("D9").Borders[Microsoft.Office.Interop.Excel.XlBordersIndex.xlEdgeLeft].LineStyle = 1;
            vstoWorksheet.get_Range("D10").Borders[Microsoft.Office.Interop.Excel.XlBordersIndex.xlEdgeLeft].LineStyle = 1;

            vstoWorksheet.get_Range("E7:G7").Borders.LineStyle = 1;

            vstoWorksheet.get_Range("E8").Borders[Microsoft.Office.Interop.Excel.XlBordersIndex.xlEdgeLeft].LineStyle = 1;
            vstoWorksheet.get_Range("E9").Borders[Microsoft.Office.Interop.Excel.XlBordersIndex.xlEdgeLeft].LineStyle = 1;
            vstoWorksheet.get_Range("E10").Borders[Microsoft.Office.Interop.Excel.XlBordersIndex.xlEdgeLeft].LineStyle = 1;

            vstoWorksheet.get_Range("F8").Borders[Microsoft.Office.Interop.Excel.XlBordersIndex.xlEdgeLeft].LineStyle = 1;
            vstoWorksheet.get_Range("F9").Borders[Microsoft.Office.Interop.Excel.XlBordersIndex.xlEdgeLeft].LineStyle = 1;
            vstoWorksheet.get_Range("F10").Borders[Microsoft.Office.Interop.Excel.XlBordersIndex.xlEdgeLeft].LineStyle = 1;

            vstoWorksheet.get_Range("G8").Borders[Microsoft.Office.Interop.Excel.XlBordersIndex.xlEdgeLeft].LineStyle = 1;
            vstoWorksheet.get_Range("G9").Borders[Microsoft.Office.Interop.Excel.XlBordersIndex.xlEdgeLeft].LineStyle = 1;
            vstoWorksheet.get_Range("G10").Borders[Microsoft.Office.Interop.Excel.XlBordersIndex.xlEdgeLeft].LineStyle = 1;

            vstoWorksheet.get_Range("G7").Borders[Microsoft.Office.Interop.Excel.XlBordersIndex.xlEdgeRight].LineStyle = 1;
            vstoWorksheet.get_Range("G8").Borders[Microsoft.Office.Interop.Excel.XlBordersIndex.xlEdgeRight].LineStyle = 1;
            vstoWorksheet.get_Range("G9").Borders[Microsoft.Office.Interop.Excel.XlBordersIndex.xlEdgeRight].LineStyle = 1;
            vstoWorksheet.get_Range("G10").Borders[Microsoft.Office.Interop.Excel.XlBordersIndex.xlEdgeRight].LineStyle = 1;

            vstoWorksheet.get_Range("A10:G10").Borders[Microsoft.Office.Interop.Excel.XlBordersIndex.xlEdgeBottom].LineStyle = 1;

            vstoWorksheet.get_Range("A7").ColumnWidth = 22;
            vstoWorksheet.get_Range("B8").ColumnWidth = 25;
            vstoWorksheet.get_Range("C8").ColumnWidth = 25;
            vstoWorksheet.get_Range("D9").ColumnWidth = 48;
            vstoWorksheet.get_Range("E9").ColumnWidth = 19;
            vstoWorksheet.get_Range("F9").ColumnWidth = 19;
            vstoWorksheet.get_Range("G9").ColumnWidth = 19;

            vstoWorksheet.get_Range("A7").RowHeight = 50;
            //Fin Bordes del Excel Kardex Sunat 13.1 --------------/

            //Inicio Estilo del Excel Kardex Sunat 13.1 ***************************
            vstoWorksheet.get_Range("A1").Font.Name = "Arial";
            vstoWorksheet.get_Range("A1").Font.Size = 14;
            vstoWorksheet.get_Range("A1").Font.Bold = 1;

            vstoWorksheet.get_Range("A3:A5").Font.Name = "Arial";
            vstoWorksheet.get_Range("A3:A5").Font.Size = 12;
            vstoWorksheet.get_Range("A3:A5").Font.Bold = 1;

            vstoWorksheet.get_Range("A7:G7").Font.Name = "Arial";
            vstoWorksheet.get_Range("A8:G8").Font.Name = "Arial";
            vstoWorksheet.get_Range("A9:G9").Font.Name = "Arial";
            vstoWorksheet.get_Range("A10:G10").Font.Name = "Arial";
            vstoWorksheet.get_Range("A7:G7").Font.Size = 12;
            vstoWorksheet.get_Range("A8:G8").Font.Size = 12;
            vstoWorksheet.get_Range("A9:G9").Font.Size = 12;
            vstoWorksheet.get_Range("A10:G10").Font.Size = 12;
            vstoWorksheet.get_Range("A7:G7").Font.Bold = 1;
            vstoWorksheet.get_Range("A8:G8").Font.Bold = 1;
            vstoWorksheet.get_Range("A9:G9").Font.Bold = 1;
            vstoWorksheet.get_Range("A10:G10").Font.Bold = 1;
            //Fin Estilo del Excel Kardex Sunat 13.1 ***************************

            //Inicio Estructura del detalle del Kardex 4.1 ***********************************************
            vstoWorksheet.get_Range("A7").Value = "        FECHA DE        ";
            vstoWorksheet.get_Range("A8").Value = "       PAGO O        ";
            vstoWorksheet.get_Range("A9").Value = "    RETENCION        ";
            vstoWorksheet.get_Range("A10").Value = "   (dd/mm/aaaa)        ";

            vstoWorksheet.get_Range("B7").Value = "                                  PERSONA QUE BRINDÓ EL SERVICIO        ";
            vstoWorksheet.get_Range("B8").Value = "          TIPO        ";
            vstoWorksheet.get_Range("B9").Value = " DE DOCUMENTO        ";
            vstoWorksheet.get_Range("B10").Value = "     (TABLA 2)      ";

            vstoWorksheet.get_Range("C8").Value = "         N° DE        ";
            vstoWorksheet.get_Range("C9").Value = "    DOCUMENTO        ";
            vstoWorksheet.get_Range("C10").Value = "   DE IDENTIDAD  ";

            vstoWorksheet.get_Range("D9").Value = "          APELLIDOS Y NOMBRES          ";

            vstoWorksheet.get_Range("E7").Value = "            MONTO DE LA RETRIBUCIÓN    ";
            vstoWorksheet.get_Range("E8").Value = "     MONTO        ";
            vstoWorksheet.get_Range("E9").Value = "     BRUTO        ";

            vstoWorksheet.get_Range("F8").Value = " RETENCIÓN        ";
            vstoWorksheet.get_Range("F9").Value = " EFECTUADA        ";

            vstoWorksheet.get_Range("G8").Value = "    MONTO   ";
            vstoWorksheet.get_Range("G9").Value = "      NETO   ";
            //Fin Estructura del detalle del Kardex 4.1 ***********************************************

            string[] fecini_ = fecini.Split('/');
            string[] fecfin_ = fecfin.Split('/');
            query = " SELECT 	a.fecha, b.tipdoc, b.nrodoc, rtrim(b.apepat)+' '+rtrim(b.apemat)+' '+rtrim(b.nombres) as nombre, ";
            query += "          SUM(a.subtotal + a.movilidad + a.refrigerio + adicional) AS monto, SUM(a.reten), ";
            query += "          SUM(a.subtotal + a.movilidad + a.refrigerio + adicional) - SUM(reten) AS total ";
            query += "FROM desret a ";
            query += "LEFT JOIN perret b ON b.idperplan = a.idperplan AND b.idcia = a.idcia ";
            query += "WHERE a.idcia = '" + idcia + "' AND a.idtipocal = '" + idtipocal + "' ";
            query += "AND a.idtipoper = '" + idtipoper + "' AND a.anio = '" + anio + "' ";
            query += "AND a.fecha BETWEEN '" + fecini_[2] + "-" + fecini_[1] + "-" + fecini_[0] + "' AND '" + fecfin_[2] + "-" + fecfin_[1] + "-" + fecfin_[0] + "' ";
            query += "GROUP BY a.idperplan";


            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();
            try
            {
                using (SqlCommand myCommand = new SqlCommand(query, conexion))
                {
                    System.Data.DataTable dt = new System.Data.DataTable();
                    dt.Load(myCommand.ExecuteReader());
                    myCommand.Dispose();
                    if (dt.Rows.Count > 0)
                    {
                        float tot_montobruto = 0;
                        float tot_retencion = 0;
                        float tot_montoneto = 0;
                        for (int x = 0; x < dt.Rows.Count; x++)
                        {
                            int cont = 11;
                            vstoWorksheet.get_Range("B" + (cont + x).ToString()).HorizontalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;

                            vstoWorksheet.get_Range("A" + (cont + x).ToString()).Borders.LineStyle = 1;
                            vstoWorksheet.get_Range("B" + (cont + x).ToString()).Borders.LineStyle = 1;
                            vstoWorksheet.get_Range("C" + (cont + x).ToString()).Borders.LineStyle = 1;
                            vstoWorksheet.get_Range("D" + (cont + x).ToString()).Borders.LineStyle = 1;
                            vstoWorksheet.get_Range("E" + (cont + x).ToString()).Borders.LineStyle = 1;
                            vstoWorksheet.get_Range("F" + (cont + x).ToString()).Borders.LineStyle = 1;
                            vstoWorksheet.get_Range("G" + (cont + x).ToString()).Borders.LineStyle = 1;

                            vstoWorksheet.get_Range("A" + (cont + x).ToString()).Value = lastFechaMes.ToShortDateString();
                            vstoWorksheet.get_Range("A" + (cont + x).ToString()).NumberFormat = "dd/mm/yyyy";
                            vstoWorksheet.get_Range("B" + (cont + x).ToString()).Value = dt.Rows[x][1];
                            vstoWorksheet.get_Range("C" + (cont + x).ToString()).Value = "'" + dt.Rows[x][2];
                            vstoWorksheet.get_Range("D" + (cont + x).ToString()).Value = dt.Rows[x][3];
                            vstoWorksheet.get_Range("E" + (cont + x).ToString()).Value = dt.Rows[x][4];
                            vstoWorksheet.get_Range("E" + (cont + x).ToString()).NumberFormat = "#,##0.00";
                            vstoWorksheet.get_Range("F" + (cont + x).ToString()).Value = dt.Rows[x][5];
                            vstoWorksheet.get_Range("F" + (cont + x).ToString()).NumberFormat = "#,##0.00";
                            vstoWorksheet.get_Range("G" + (cont + x).ToString()).Value = dt.Rows[x][6];
                            vstoWorksheet.get_Range("G" + (cont + x).ToString()).NumberFormat = "#,##0.00";

                            tot_montobruto += float.Parse(dt.Rows[x][4].ToString());
                            tot_retencion += float.Parse(dt.Rows[x][5].ToString());
                            tot_montoneto += float.Parse(dt.Rows[x][6].ToString());
                            if (x + 1 == dt.Rows.Count)
                            {
                                vstoWorksheet.get_Range("E" + (cont + x + 1).ToString()).Borders.LineStyle = 1;
                                vstoWorksheet.get_Range("F" + (cont + x + 1).ToString()).Borders.LineStyle = 1;
                                vstoWorksheet.get_Range("G" + (cont + x + 1).ToString()).Borders.LineStyle = 1;

                                vstoWorksheet.get_Range("D" + (cont + x + 1).ToString()).Value = "          TOTALES          ";
                                vstoWorksheet.get_Range("E" + (cont + x + 1).ToString()).Value = tot_montobruto;
                                vstoWorksheet.get_Range("F" + (cont + x + 1).ToString()).Value = tot_retencion;
                                vstoWorksheet.get_Range("G" + (cont + x + 1).ToString()).Value = tot_montoneto;
                            }
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            conexion.Close();
        }

        private DateTime last_fecha_mes(string messem, string anio)
        {
            String fecha_ = "01/" + messem + "/" + anio;

            DateTime firstDayOfTheMonth = new DateTime(DateTime.Parse(fecha_).Year, DateTime.Parse(fecha_).Month, 1);

            return firstDayOfTheMonth.AddMonths(1).AddDays(-1);
        }

        public string alineacionNumero(string texto, int longitud)
        {
            texto = replicateCadena(" ", longitud) + texto.Trim();
            texto = texto.Substring(texto.Length - longitud, longitud);
            return texto;
        }

        public void form_excel_kardex(string codcia, string alma, string txtArticulo, string txtfecini, string txtfecfin, Excel.Worksheet vstoWorksheet)
        {
            string cCodArt = txtArticulo.Trim();
            string[] _fecini = txtfecini.Split('/');
            string[] _fecfin = txtfecfin.Split('/');
            string cPeriodo = "( Del " + txtfecini + " al " + txtfecfin + ") ";

            vstoWorksheet.Application.ActiveWindow.Zoom = 75;
            vstoWorksheet.Application.ActiveWindow.DisplayGridlines = false;
            vstoWorksheet.get_Range("A1").Value = "FORMATO 13.1: REGISTRO DE INVENTARIO PERMANENTE VALORIZADO - DETALLE DEL INVENTARIO VALORIZADO";
            vstoWorksheet.get_Range("A3").Value = "PERÍODO:";
            vstoWorksheet.get_Range("A4").Value = "RUC:";
            vstoWorksheet.get_Range("A5").Value = "APELLIDOS Y NOMBRES, DENOMINACIÓN O RAZÓN SOCIAL:";
            vstoWorksheet.get_Range("A6").Value = "ESTABLECIMIENTO (1):";
            vstoWorksheet.get_Range("A7").Value = "CÓDIGO DE LA EXISTENCIA:";
            vstoWorksheet.get_Range("A8").Value = "TIPO (TABLA 5):";
            vstoWorksheet.get_Range("A9").Value = "DESCRIPCIÓN:";
            vstoWorksheet.get_Range("A10").Value = "CÓDIGO DE LA UNIDAD DE MEDIDA (TABLA 6):";
            vstoWorksheet.get_Range("A11").Value = "MÉTODO DE VALUACIÓN:";

            //Inicio Bordes del Excel Kardex Sunat 13.1 --------------/
            vstoWorksheet.get_Range("A14:E14").Borders[Excel.XlBordersIndex.xlEdgeLeft].LineStyle = 1;
            vstoWorksheet.get_Range("A14:E14").Borders[Excel.XlBordersIndex.xlEdgeTop].LineStyle = 1;
            vstoWorksheet.get_Range("A14:E14").Borders[Excel.XlBordersIndex.xlEdgeRight].LineStyle = 1;
            vstoWorksheet.get_Range("A15:E14").Borders[Excel.XlBordersIndex.xlEdgeLeft].LineStyle = 1;
            vstoWorksheet.get_Range("A15:E14").Borders[Excel.XlBordersIndex.xlEdgeBottom].LineStyle = 1;
            vstoWorksheet.get_Range("A15:E14").Borders[Excel.XlBordersIndex.xlEdgeRight].LineStyle = 1;

            vstoWorksheet.get_Range("A16:E16").Borders.LineStyle = 1;

            vstoWorksheet.get_Range("F14").Borders[Excel.XlBordersIndex.xlEdgeLeft].LineStyle = 1;
            vstoWorksheet.get_Range("F14").Borders[Excel.XlBordersIndex.xlEdgeTop].LineStyle = 1;
            vstoWorksheet.get_Range("F14").Borders[Excel.XlBordersIndex.xlEdgeRight].LineStyle = 1;
            vstoWorksheet.get_Range("F15").Borders[Excel.XlBordersIndex.xlEdgeLeft].LineStyle = 1;
            vstoWorksheet.get_Range("F15").Borders[Excel.XlBordersIndex.xlEdgeRight].LineStyle = 1;
            vstoWorksheet.get_Range("F16").Borders[Excel.XlBordersIndex.xlEdgeLeft].LineStyle = 1;
            vstoWorksheet.get_Range("F16").Borders[Excel.XlBordersIndex.xlEdgeBottom].LineStyle = 1;
            vstoWorksheet.get_Range("F16").Borders[Excel.XlBordersIndex.xlEdgeRight].LineStyle = 1;

            vstoWorksheet.get_Range("G14").Borders[Excel.XlBordersIndex.xlEdgeLeft].LineStyle = 1;
            vstoWorksheet.get_Range("G14").Borders[Excel.XlBordersIndex.xlEdgeTop].LineStyle = 1;
            vstoWorksheet.get_Range("G14").Borders[Excel.XlBordersIndex.xlEdgeRight].LineStyle = 1;
            vstoWorksheet.get_Range("G15").Borders[Excel.XlBordersIndex.xlEdgeLeft].LineStyle = 1;
            vstoWorksheet.get_Range("G15").Borders[Excel.XlBordersIndex.xlEdgeRight].LineStyle = 1;
            vstoWorksheet.get_Range("G16").Borders[Excel.XlBordersIndex.xlEdgeLeft].LineStyle = 1;
            vstoWorksheet.get_Range("G16").Borders[Excel.XlBordersIndex.xlEdgeBottom].LineStyle = 1;
            vstoWorksheet.get_Range("G16").Borders[Excel.XlBordersIndex.xlEdgeRight].LineStyle = 1;

            vstoWorksheet.get_Range("H14").Borders[Excel.XlBordersIndex.xlEdgeLeft].LineStyle = 1;
            vstoWorksheet.get_Range("H14:J14").Borders[Excel.XlBordersIndex.xlEdgeTop].LineStyle = 1;
            vstoWorksheet.get_Range("J14").Borders[Excel.XlBordersIndex.xlEdgeRight].LineStyle = 1;
            vstoWorksheet.get_Range("H15").Borders[Excel.XlBordersIndex.xlEdgeLeft].LineStyle = 1;
            vstoWorksheet.get_Range("J15").Borders[Excel.XlBordersIndex.xlEdgeRight].LineStyle = 1;

            vstoWorksheet.get_Range("K14").Borders[Excel.XlBordersIndex.xlEdgeLeft].LineStyle = 1;
            vstoWorksheet.get_Range("K14:M14").Borders[Excel.XlBordersIndex.xlEdgeTop].LineStyle = 1;
            vstoWorksheet.get_Range("M14").Borders[Excel.XlBordersIndex.xlEdgeRight].LineStyle = 1;
            vstoWorksheet.get_Range("K15").Borders[Excel.XlBordersIndex.xlEdgeLeft].LineStyle = 1;
            vstoWorksheet.get_Range("M15").Borders[Excel.XlBordersIndex.xlEdgeRight].LineStyle = 1;

            vstoWorksheet.get_Range("N14").Borders[Excel.XlBordersIndex.xlEdgeLeft].LineStyle = 1;
            vstoWorksheet.get_Range("N14:P14").Borders[Excel.XlBordersIndex.xlEdgeTop].LineStyle = 1;
            vstoWorksheet.get_Range("P14").Borders[Excel.XlBordersIndex.xlEdgeRight].LineStyle = 1;
            vstoWorksheet.get_Range("N15").Borders[Excel.XlBordersIndex.xlEdgeLeft].LineStyle = 1;
            vstoWorksheet.get_Range("P15").Borders[Excel.XlBordersIndex.xlEdgeRight].LineStyle = 1;

            vstoWorksheet.get_Range("H16:P16").Borders.LineStyle = 1;

            vstoWorksheet.get_Range("A16").ColumnWidth = 13;
            vstoWorksheet.get_Range("B16").ColumnWidth = 18;
            vstoWorksheet.get_Range("C16").ColumnWidth = 22;
            vstoWorksheet.get_Range("D16").ColumnWidth = 14;
            vstoWorksheet.get_Range("E16").ColumnWidth = 18;
            vstoWorksheet.get_Range("F14").ColumnWidth = 20;
            vstoWorksheet.get_Range("G15").ColumnWidth = 60;
            vstoWorksheet.get_Range("H16").ColumnWidth = 18;
            vstoWorksheet.get_Range("I16").ColumnWidth = 22;
            vstoWorksheet.get_Range("J16").ColumnWidth = 18;
            vstoWorksheet.get_Range("K16").ColumnWidth = 18;
            vstoWorksheet.get_Range("L16").ColumnWidth = 22;
            vstoWorksheet.get_Range("M16").ColumnWidth = 18;
            vstoWorksheet.get_Range("N16").ColumnWidth = 18;
            vstoWorksheet.get_Range("O16").ColumnWidth = 22;
            vstoWorksheet.get_Range("P16").ColumnWidth = 18;
            //Fin Bordes del Excel Kardex Sunat 13.1 --------------/

            //Inicio Estilo del Excel Kardex Sunat 13.1 ***************************
            vstoWorksheet.get_Range("A1").Font.Name = "Arial";
            vstoWorksheet.get_Range("A1").Font.Size = 14;
            vstoWorksheet.get_Range("A1").Font.Bold = 1;

            vstoWorksheet.get_Range("A3:A11").Font.Name = "Arial";
            vstoWorksheet.get_Range("A3:A11").Font.Size = 12;
            vstoWorksheet.get_Range("A3:A11").Font.Bold = 1;

            vstoWorksheet.get_Range("A14:P14").Font.Name = "Arial";
            vstoWorksheet.get_Range("A15:P15").Font.Name = "Arial";
            vstoWorksheet.get_Range("A16:P16").Font.Name = "Arial";
            vstoWorksheet.get_Range("A14:P14").Font.Size = 12;
            vstoWorksheet.get_Range("A15:P15").Font.Size = 12;
            vstoWorksheet.get_Range("A16:P16").Font.Size = 12;
            vstoWorksheet.get_Range("A14:P14").Font.Bold = 1;
            vstoWorksheet.get_Range("A15:P15").Font.Bold = 1;
            vstoWorksheet.get_Range("A16:P16").Font.Bold = 1;
            //Fin Estilo del Excel Kardex Sunat 13.1 ***************************

            vstoWorksheet.get_Range("A14").Value = "     DOCUMENTO DE TRASLADO, COMPROBANTE DE PAGO        ";
            vstoWorksheet.get_Range("A15").Value = "                           DOCUMENTO INTERNO O SIMILAR";
            vstoWorksheet.get_Range("A16").Value = "    FECHA";
            vstoWorksheet.get_Range("B16").Value = "    PE/PS";
            vstoWorksheet.get_Range("C16").Value = "      TIPO (TABLA 10)";
            vstoWorksheet.get_Range("D16").Value = "     SERIE";
            vstoWorksheet.get_Range("E16").Value = "       NÚMERO";

            vstoWorksheet.get_Range("F14").Value = "          TIPO DE ";
            vstoWorksheet.get_Range("F15").Value = "      OPERACIÓN";
            vstoWorksheet.get_Range("F16").Value = "       (TABLA 12)";

            vstoWorksheet.get_Range("G15").Value = "            CLIENTE/PROVEEDOR";

            vstoWorksheet.get_Range("H14").Value = "                                          ENTRADAS";
            vstoWorksheet.get_Range("H16").Value = "       CANTIDAD";
            vstoWorksheet.get_Range("I16").Value = "  COSTO UNITARIO";
            vstoWorksheet.get_Range("J16").Value = "   COSTO TOTAL";

            vstoWorksheet.get_Range("K14").Value = "                                     SALIDAS";
            vstoWorksheet.get_Range("K16").Value = "       CANTIDAD";
            vstoWorksheet.get_Range("L16").Value = "  COSTO UNITARIO";
            vstoWorksheet.get_Range("M16").Value = "   COSTO TOTAL";

            vstoWorksheet.get_Range("N14").Value = "                             SALDO FINAL";
            vstoWorksheet.get_Range("N16").Value = "       CANTIDAD";
            vstoWorksheet.get_Range("O16").Value = "  COSTO UNITARIO";
            vstoWorksheet.get_Range("P16").Value = "   COSTO TOTAL";

            string v_fecini = _fecini[2] + "-" + _fecini[1] + "-" + _fecini[0];
            string v_fecfin = _fecfin[2] + "-" + _fecfin[1] + "-" + _fecfin[0];
            query = "CALL sp_KardexSunat('" + codcia + "','" + alma + "','" + cCodArt + "','" + v_fecini + "','" + v_fecfin + "')";

            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();
            try
            {
                using (SqlCommand myCommand = new SqlCommand(query, conexion))
                {
                    System.Data.DataTable dt = new System.Data.DataTable();
                    dt.Load(myCommand.ExecuteReader());
                    myCommand.Dispose();
                    if (dt.Rows.Count > 0)
                    {
                        vstoWorksheet.get_Range("B3").Value = dt.Rows[0][1];
                        vstoWorksheet.get_Range("B4").Value = dt.Rows[0][2];
                        vstoWorksheet.get_Range("E5").Value = dt.Rows[0][3];
                        vstoWorksheet.get_Range("C6").Value = dt.Rows[0][4];
                        vstoWorksheet.get_Range("C7").Value = dt.Rows[0][5];
                        vstoWorksheet.get_Range("C8").Value = dt.Rows[0][6];
                        vstoWorksheet.get_Range("C9").Value = dt.Rows[0][7];
                        vstoWorksheet.get_Range("D10").Value = dt.Rows[0][8];
                        vstoWorksheet.get_Range("C11").Value = dt.Rows[0][9];
                        float saldo_anterior = float.Parse(dt.Rows[0][20].ToString());

                        for (int x = 0; x < dt.Rows.Count; x++)
                        {
                            int cont = 17;
                            vstoWorksheet.get_Range("A" + (cont + x).ToString()).Rows.RowHeight = 24;
                            vstoWorksheet.get_Range("A" + (cont + x).ToString()).HorizontalAlignment = Excel.XlVAlign.xlVAlignCenter;

                            vstoWorksheet.get_Range("A" + (cont + x).ToString()).Borders.LineStyle = 1;
                            vstoWorksheet.get_Range("B" + (cont + x).ToString()).Borders.LineStyle = 1;
                            vstoWorksheet.get_Range("C" + (cont + x).ToString()).Borders.LineStyle = 1;
                            vstoWorksheet.get_Range("D" + (cont + x).ToString()).Borders.LineStyle = 1;
                            vstoWorksheet.get_Range("E" + (cont + x).ToString()).Borders.LineStyle = 1;
                            vstoWorksheet.get_Range("F" + (cont + x).ToString()).Borders.LineStyle = 1;
                            vstoWorksheet.get_Range("G" + (cont + x).ToString()).Borders.LineStyle = 1;
                            vstoWorksheet.get_Range("H" + (cont + x).ToString()).Borders.LineStyle = 1;
                            vstoWorksheet.get_Range("I" + (cont + x).ToString()).Borders.LineStyle = 1;
                            vstoWorksheet.get_Range("J" + (cont + x).ToString()).Borders.LineStyle = 1;
                            vstoWorksheet.get_Range("K" + (cont + x).ToString()).Borders.LineStyle = 1;
                            vstoWorksheet.get_Range("L" + (cont + x).ToString()).Borders.LineStyle = 1;
                            vstoWorksheet.get_Range("M" + (cont + x).ToString()).Borders.LineStyle = 1;
                            vstoWorksheet.get_Range("N" + (cont + x).ToString()).Borders.LineStyle = 1;
                            vstoWorksheet.get_Range("O" + (cont + x).ToString()).Borders.LineStyle = 1;
                            vstoWorksheet.get_Range("P" + (cont + x).ToString()).Borders.LineStyle = 1;

                            vstoWorksheet.get_Range("A" + (cont + x).ToString()).Value = dt.Rows[x][10];
                            vstoWorksheet.get_Range("A" + (cont + x).ToString()).NumberFormat = "dd-mmm-yy";
                            vstoWorksheet.get_Range("B" + (cont + x).ToString()).Value = dt.Rows[x][11];
                            vstoWorksheet.get_Range("C" + (cont + x).ToString()).Value = dt.Rows[x][12];
                            vstoWorksheet.get_Range("D" + (cont + x).ToString()).Value = dt.Rows[x][13];
                            vstoWorksheet.get_Range("E" + (cont + x).ToString()).Value = dt.Rows[x][14];
                            vstoWorksheet.get_Range("F" + (cont + x).ToString()).Value = dt.Rows[x][15];
                            vstoWorksheet.get_Range("G" + (cont + x).ToString()).Value = dt.Rows[x][16];

                            if (dt.Rows[x][15].ToString().Trim().Equals("PE"))
                            {
                                vstoWorksheet.get_Range("H" + (cont + x).ToString()).Value = dt.Rows[x][17];
                                vstoWorksheet.get_Range("I" + (cont + x).ToString()).Value = dt.Rows[x][18];
                                vstoWorksheet.get_Range("J" + (cont + x).ToString()).Value = dt.Rows[x][19];
                                vstoWorksheet.get_Range("K" + (cont + x).ToString()).Value = "0";
                                vstoWorksheet.get_Range("L" + (cont + x).ToString()).Value = "0.00";
                                vstoWorksheet.get_Range("L" + (cont + x).ToString()).NumberFormat = "#,##0.00";
                                vstoWorksheet.get_Range("M" + (cont + x).ToString()).Value = "0.00";
                                vstoWorksheet.get_Range("M" + (cont + x).ToString()).NumberFormat = "#,##0.00";
                                saldo_anterior = saldo_anterior + float.Parse(dt.Rows[x][17].ToString());
                            }
                            else
                            {
                                vstoWorksheet.get_Range("J" + (cont + x).ToString()).Value = "0";
                                vstoWorksheet.get_Range("I" + (cont + x).ToString()).Value = "0.00";
                                vstoWorksheet.get_Range("I" + (cont + x).ToString()).NumberFormat = "#,##0.00";
                                vstoWorksheet.get_Range("J" + (cont + x).ToString()).Value = "0.00";
                                vstoWorksheet.get_Range("J" + (cont + x).ToString()).NumberFormat = "#,##0.00";
                                vstoWorksheet.get_Range("K" + (cont + x).ToString()).Value = "-" + dt.Rows[x][17];
                                vstoWorksheet.get_Range("K" + (cont + x).ToString()).Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Red);
                                vstoWorksheet.get_Range("L" + (cont + x).ToString()).NumberFormat = "#,##0.00";
                                vstoWorksheet.get_Range("L" + (cont + x).ToString()).Value = dt.Rows[x][18];
                                vstoWorksheet.get_Range("M" + (cont + x).ToString()).NumberFormat = "#,##0.00";
                                vstoWorksheet.get_Range("M" + (cont + x).ToString()).Value = dt.Rows[x][19];
                                saldo_anterior = saldo_anterior - float.Parse(dt.Rows[x][17].ToString());
                            }

                            vstoWorksheet.get_Range("N" + (cont + x).ToString()).Value = saldo_anterior.ToString();
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            conexion.Close();
        }

        public static bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        public void cerrarMensaje() //Mensaje tipo TOAST , dura 1 segundo
        {
            try
            {
                System.Threading.Thread.Sleep(1070);
                Microsoft.VisualBasic.Interaction.AppActivate(
                System.Diagnostics.Process.GetCurrentProcess().Id);
                System.Windows.Forms.SendKeys.SendWait(" ");
            }
            catch (Exception)
            {
                MessageBox.Show("Ha cerrado la ventana muy rapido, vuelva a intentarlo.");
                throw;
            }
           
        }

        //Para estos metodos se retorna un 1 si se envio correo o se valido el metodo y 0 si hubo algun error 
        //Método para enviar mensaje de error al programador (Envia la excepción, query y el usuario afectado)
        public void submitErrorMessageToProgrammer(String moduloError, String queryUsada, String errorText) {
            GlobalVariables variables = new GlobalVariables();
            SqlCommand command = new SqlCommand();

            String userName = variables.getValorUsr();

            DateTime fechaact = DateTime.Now;

            MailMessage msg = new MailMessage();
            msg.From = new MailAddress("Sistemas TI<sistemasti@agricolachira.com.pe>");

            msg.To.Add("Cristhian Martin Valladolid Chero <cvalladolidc@agricolachira.com.pe>");

            msg.Subject = "SISASIS, Reporte de error - Usuario: " + userName;

            string htmlString = @"<html>
                              <body style='font-family: Calibri, Candara, Segoe, Segoe UI, Optima, Arial, sans-serif;'>
                              <p></p>
                              <p>El error reportado se encuentra en el modulo: " + moduloError + @"</p>
                              <p></p>

                              <p>
                              " + errorText + @"</p>
                              <p></p>
                              " + queryUsada + @"  
                              <p></p>
                              <p>" + fechaact + @"</p>



                              </body>
                              </html>";

            msg.Body = htmlString;
            msg.IsBodyHtml = true;

            SmtpClient smt = new SmtpClient();
            smt.Host = "10.72.1.71";

            NetworkCredential ntcd = new NetworkCredential();
            smt.Port = 25;
            smt.Credentials = ntcd;
            smt.Send(msg);

            MessageBox.Show("Se envio correctamente el error presentando","SISASIS");
        }

        //Metodo que informa al usuario que el error ha sido reportado
        public void reportUserSubmit(String numeroTicket, String moduloError, String mensajeUsuario ) {
            try
            {
                GlobalVariables gv = new GlobalVariables();
                string email = gv.getValorUsrMail();
                string nombre = gv.getValorUsrName();



                DateTime fechaact = DateTime.Now;

                MailMessage msg = new MailMessage();
                msg.From = new MailAddress("Sistemas TI<sistemasti@agricolachira.com.pe>");

                //   msg.To.Add(nombre +" <"+email + ">");
                msg.To.Add(nombre + " <" + email + ">");

                msg.Subject = "SISASIS, Reporte de error - Número de ticket : " + numeroTicket;

                string htmlString = @"<html>
                              <body style='font-family: Calibri, Candara, Segoe, Segoe UI, Optima, Arial, sans-serif;'>
                              <p>Hola " + nombre + @"</p>
                              <p>El error presentado en el modulo: " + moduloError + @", ha sido reportado con el número de ticket: " + numeroTicket + @"</p>
                              <p></p>                          
                              <p></p>
                              <p>Detalle del error reportado: </p>
                              <p> " + mensajeUsuario + @"</p>
                              <p>" + fechaact + @"</p>

                              </body>
                              </html>";

                msg.Body = htmlString;
                msg.IsBodyHtml = true;

                SmtpClient smt = new SmtpClient();
                smt.Host = "10.72.1.71";

                NetworkCredential ntcd = new NetworkCredential();
                smt.Port = 25;
                smt.Credentials = ntcd;
                smt.Send(msg);
            }
            catch (Exception)
            {
                MessageBox.Show("Error al reconocer correo del usuario. Contactar con sistemas.","SISASIS");
              //  throw;
            }   
        }

        //Método para generar ticket, basandose en el ultimo número de ticket ( actualizar con @scope ) 
        public void crearTicket(string detalleError, string moduloError)
        {
            //
            string SQL = @"
                            Declare @LastID INT
                            SET @LastID = SCOPE_IDENTITY() + 1
                            INSERT INTO[Tickets_SISASIS].[dbo].[TBL.Tickets]
                                    (numTicket, idUsuarioAfectado, idUsuarioAsignado, detalleTicket, modulo)
                            values('SL000000'+ CAST(@LastID as VARCHAR(1)),'CVALLADOLIDC','SQL','Se presento un problema a la hora que descarga datos','Modulo de descansos medicos')";

            //Impresión de prueba de parametros
            //idUsuarioAfectado: 
            GlobalVariables variables = new GlobalVariables();
            string userName = variables.getValorUsr();
            //  MessageBox.Show("El usuario afectado es : " + userName);
            MessageBox.Show("El mensaje del usuario es : " + detalleError, "SISASIS - MODO DE TICKETS");
           // MessageBox.Show("El modulo donde se reporte el error es " + moduloError);
        }
    }
}