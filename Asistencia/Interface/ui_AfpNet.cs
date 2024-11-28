using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Reflection;

namespace CaniaBrava
{
    public partial class ui_AfpNet : Form
    {
        public ui_AfpNet()
        {
            InitializeComponent();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ui_AfpNet_Load(object sender, EventArgs e)
        {
            Funciones funciones = new Funciones();
            GlobalVariables variables = new GlobalVariables();
            cmbMes.Text = "01   ENERO";
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {


            Funciones funciones = new Funciones();
            GlobalVariables globalVariable = new GlobalVariables();
            string idcia = globalVariable.getValorCia();
            string aniopdt = txtAnio.Text;
            string mespdt = funciones.getValorComboBox(cmbMes, 2);
            string ruc = globalVariable.getValorRucCia();
            string valida = "G";
            string query;
            string rutaFile = string.Empty;

            if (aniopdt.Trim() == string.Empty && valida == "G")
            {
                MessageBox.Show("El Año del Periodo Tributario no es válido", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                valida = "B";
            }

            if (valida.Equals("G"))
            {

                SqlConnection conexion = new SqlConnection();
                conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
                conexion.Open();

                FolderBrowserDialog dialogoRuta = new FolderBrowserDialog();
                if (dialogoRuta.ShowDialog() == DialogResult.OK)
                {
                    rutaFile = dialogoRuta.SelectedPath;
                }

                if (rutaFile != string.Empty)
                {

                    if (chkConsultaCuspp.Checked)
                    {
                        DataTable dt = new DataTable();
                        query = "Select C.Parm2maesgen as tipodoc,A.nrodoc,A.apepat,A.apemat,A.nombres ";
                        query = query + " from perplan A left join maesgen C on C.idmaesgen='002' and A.tipdoc=C.clavemaesgen ";
                        query = query + " where A.idcia='" + @idcia + "' ";
                        query = query + " order by A.apepat,A.apemat,A.nombres asc;";
                        SqlDataAdapter da = new SqlDataAdapter();
                        da.SelectCommand = new SqlCommand(query, conexion);
                        da.Fill(dt);
                        string filename = rutaFile + "/ConsultaMasivaDni.xls";
                        ExportarExcelConsultaMasiva(dt, filename);
                    }

                    /////////////////////////////////////////////////////////////////////////////////////////////////////////////
                    ///////////////////////////////////////////PLANILLA UNICA////////////////////////////////////////////////////
                    /////////////////////////////////////////////////////////////////////////////////////////////////////////////

                    if (chkPlanilla.Checked)
                    {
                        DataTable dt = new DataTable();
                        query = "select A.idcia,E.aniopdt,E.mespdt,A.idperplan,F.cuspp,F.nrodoc,F.apepat,F.apemat,F.nombres, ";
                        query = query + " round(sum(A.remasegura),2) as remasegura, ";
                        query = query + " round(sum(B.sppao),2) as sppao,round(sum(B.sppav),2) as sppav,round(sum(B.sppcp),2) as sppcp,";
                        query = query + " round(sum(B.sppps),2) as sppps, ";
                        query = query + " round(sum(B.snp),2) as snp,round(sum(B.sppavemp),2) as  sppavemp,H.codnet ";
                        query = query + " from view_remasegurable A left join plan_ B on A.idcia=B.idcia and  ";
                        query = query + " A.idperplan=B.idperplan and A.idtipoper=B.idtipoper and A.messem=B.messem ";
                        query = query + " and A.anio=B.anio and A.idtipocal=B.idtipocal and A.idtipoplan=B.idtipoplan ";
                        query = query + " inner join calplan E on A.idtipocal=E.idtipocal and A.idcia=E.idcia  ";
                        query = query + " and A.messem=E.messem and A.anio=E.anio and A.idtipoper=E.idtipoper  ";
                        query = query + " left join perplan F on A.idcia=F.idcia and A.idperplan=F.idperplan ";
                        query = query + " left join fonpenper G on A.idperplan=G.idperplan and A.idcia=G.idcia ";
                        query = query + " left join fonpen H on G.idfonpen=H.idfonpen ";
                        query = query + " where A.idcia='" + @idcia + "' and E.aniopdt='" + @aniopdt + "' and E.mespdt='" + @mespdt + "' and H.codnet<>'' ";
                        query = query + " group by A.idcia,E.aniopdt,E.mespdt,A.idperplan,F.cuspp,F.nrodoc,F.apepat,F.apemat,F.nombres,H.codnet";
                        query = query + " order by A.idcia,E.aniopdt,E.mespdt,F.apepat,F.apemat,F.nombres; ";

                        SqlDataAdapter da = new SqlDataAdapter();
                        da.SelectCommand = new SqlCommand(query, conexion);
                        da.Fill(dt);

                        string filename = rutaFile + "/" + "AFP" + aniopdt + mespdt + ruc + ".txt";
                        StreamWriter archivo = File.CreateText(filename);
                        archivo.Close();
                        int n = 1;
                        foreach (DataRow row_dt in dt.Rows)
                        {
                            string secuencia = funciones.replicateCadena("0", 5 - n.ToString().Trim().Length) + n.ToString().Trim();
                            string cussp = funciones.formatoLongitudPdt(row_dt["cuspp"].ToString(), 12);
                            string nrodoc = funciones.formatoLongitudPdt(row_dt["nrodoc"].ToString(), 10);
                            string apepat = funciones.formatoLongitudPdt(row_dt["apepat"].ToString(), 20);
                            string apemat = funciones.formatoLongitudPdt(row_dt["apemat"].ToString(), 20);
                            string nombres = funciones.formatoLongitudPdt(row_dt["nombres"].ToString(), 20);
                            string tipmov = " ";
                            string fechamov = "          ";
                            string remasegura = funciones.formatoLongitudPdt(row_dt["remasegura"].ToString(), 9);
                            string sppavcf = funciones.formatoLongitudPdt(row_dt["sppav"].ToString(), 9);
                            string sppavsf = funciones.formatoLongitudPdt("0.00", 9);
                            string sppavemp = funciones.formatoLongitudPdt(row_dt["sppavemp"].ToString(), 9);
                            string rubro = " ";
                            string afp = funciones.formatoLongitudPdt(row_dt["codnet"].ToString(), 2);
                            OpeIO opeIO = new OpeIO(filename);
                            opeIO.WriteNWL(secuencia + cussp + nrodoc + apepat + apemat + nombres + tipmov + fechamov + remasegura + sppavcf + sppavsf + sppavemp + rubro + afp);
                            n++;
                        }

                    }

                }

                conexion.Close();
                MessageBox.Show("Exportación AFP Net Finalizada", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }

        public static void ExportarExcelConsultaMasiva(DataTable dt, string RutaExcel)
        {
            Excel.Application oXL;
            Excel._Workbook oWB;
            Excel._Worksheet oSheet;
            Excel.Range oRng;
            try
            {
                oXL = new Excel.Application();
                oXL.Visible = true;

                oWB = (Excel._Workbook)(oXL.Workbooks.Add(Missing.Value));
                oSheet = (Excel._Worksheet)oWB.ActiveSheet;

                /*oSheet.Cells[1, 1] = "Texto 1";
                  oSheet.Cells[1, 2] = "Texto 2";
                  oSheet.Cells[1, 3] = "Texto 3";
                  oSheet.Cells[1, 4] = "Texto 4";
                  oSheet.get_Range("A1", "D1").Font.Bold = true;
                  oSheet.get_Range("A1", "D1").VerticalAlignment =Excel.XlVAlign.xlVAlignCenter;*/

                int reg = dt.Rows.Count;
                string[,] saNames = new string[reg, 5];
                oRng = oSheet.get_Range("A1", "E" + reg.ToString());
                oRng.NumberFormat = "@";
                int x = 0;

                foreach (DataRow row_dt in dt.Rows)
                {
                    saNames[x, 0] = row_dt["tipodoc"].ToString();
                    saNames[x, 1] = row_dt["nrodoc"].ToString();
                    saNames[x, 2] = row_dt["apepat"].ToString();
                    saNames[x, 3] = row_dt["apemat"].ToString();
                    saNames[x, 4] = row_dt["nombres"].ToString();
                    x++;
                }

                oRng.Value2 = saNames;
                //oRng.NumberFormat = "$0.00";
                oRng.EntireColumn.AutoFit();
                oXL.Visible = true;
                oXL.UserControl = true;
            }
            catch (Exception theException)
            {
                String errorMessage;
                errorMessage = "Error: ";
                errorMessage = String.Concat(errorMessage, theException.Message);
                errorMessage = String.Concat(errorMessage, " Line: ");
                errorMessage = String.Concat(errorMessage, theException.Source);
                MessageBox.Show(errorMessage, "Error");
            }
        }
    }
}