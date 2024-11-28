using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Configuration;
using System.IO;

namespace CaniaBrava
{
    public partial class ui_planillaretenciones : Form
    {
        Funciones funciones = new Funciones();
        GlobalVariables variables = new GlobalVariables();
        string idtipocal = "N";
        string idtipoper = "O";

        private MaskedTextBox TextBoxActivo;

        public MaskedTextBox _TextBoxActivo
        {
            get { return TextBoxActivo; }
            set { TextBoxActivo = value; }
        }

        public ui_planillaretenciones()
        {
            InitializeComponent();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            string idcia = variables.getValorCia();
            string clasepadre = "ui_planillaretenciones";
            this._TextBoxActivo = txtMesSem;

            ui_buscarcalplan ui_buscarcalplan = new ui_buscarcalplan();
            ui_buscarcalplan._FormPadre = this;
            ui_buscarcalplan.setValores(idtipoper, idcia, clasepadre, idtipocal);
            ui_buscarcalplan.Activate();
            ui_buscarcalplan.BringToFront();
            ui_buscarcalplan.ShowDialog();
            ui_buscarcalplan.Dispose();
        }

        private void txtMesSem_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                CalPlan calplan = new CalPlan();
                string idcia = variables.getValorCia();
                string idtipoper = "O";
                string periodo = txtMesSem.Text.Trim() + funciones.replicateCadena(" ", 6);
                string messem = periodo.Substring(0, 2);
                string anio = periodo.Substring(3, 4);
                if (calplan.getDatosCalPlanMensual(messem, anio, idtipoper, idcia, idtipocal, "FECHAINI") != "")
                {
                    txtFechaIni.Text = calplan.getDatosCalPlanMensual(messem, anio, idtipoper, idcia, idtipocal, "FECHAINI");
                    txtFechaFin.Text = calplan.getDatosCalPlanMensual(messem, anio, idtipoper, idcia, idtipocal, "FECHAFIN");

                    e.Handled = true;
                    toolstripform.Items[2].Select();
                    toolstripform.Focus();
                }
                else
                {
                    MessageBox.Show("El Periodo Laboral no existe", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtFechaIni.Clear();
                    txtFechaFin.Clear();
                    e.Handled = true;
                    txtMesSem.Focus();
                }

            }
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            string idcia = variables.getValorCia();
            string periodo = txtMesSem.Text.Trim() + funciones.replicateCadena(" ", 6);
            string messem = periodo.Substring(0, 2);
            string anio = periodo.Substring(3, 4);

            string query = " select E.descia,E.ruccia,F.fechaini,F.fechafin,A.messem,A.anio,A.idperplan,D.Parm1maesgen as cortotipodoc,B.nrodoc, ";
            query = query + " CONCAT(B.apepat,' ',B.apemat,' , ',B.nombres) as nombre,SUM(A.cantidad) as cantidad,SUM(A.total) as importe  from desret A  left join perret B on A.idperplan=B.idperplan and A.idcia=B.idcia  ";
            query = query + " left join maesgen D on D.idmaesgen='002' and B.tipdoc=D.clavemaesgen  ";
            query = query + " left join ciafile E on A.idcia=E.idcia  ";
            query = query + " left join calplan F on  A.idcia=F.idcia and A.idtipocal=F.idtipocal and A.messem=F.messem and  ";
            query = query + " A.anio=F.anio and A.idtipoper=F.idtipoper  ";
            query = query + " where A.idcia='" + @idcia + "' and A.idtipocal='" + @idtipocal + "' and A.idtipoper='" + @idtipoper + "' ";
            query = query + " and A.messem='" + @messem + "' and A.anio='" + @anio + "' ";
            query = query + " group by E.descia,E.ruccia,F.fechaini,F.fechafin,A.messem,A.anio,A.idperplan,D.Parm1maesgen,B.nrodoc,B.apepat,B.apemat,B.nombres  ";
            query = query + " order by B.apepat asc,B.apemat asc,B.nombres asc  ";


            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();

            try
            {
                using (SqlDataAdapter myDataAdapter = new SqlDataAdapter(query, conexion))
                {
                    DataSet dsplanret = new DataSet();
                    cr.crplanret cr = new cr.crplanret();
                    myDataAdapter.Fill(dsplanret, "dtplanret");
                    ui_reporte ui_reporte = new ui_reporte();
                    ui_reporte.asignaDataSet(cr, dsplanret);
                    ui_reporte.Activate();
                    ui_reporte.BringToFront();
                    ui_reporte.ShowDialog();
                    ui_reporte.Dispose();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }

            conexion.Close();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ui_planillaretenciones_Load(object sender, EventArgs e)
        {

        }

        private void btnExportar_Click(object sender, EventArgs e)
        {
            string idcia = variables.getValorCia();
            string periodo = txtMesSem.Text.Trim() + funciones.replicateCadena(" ", 6);
            string messem = periodo.Substring(0, 2);
            string anio = periodo.Substring(3, 4);
            string fecini = txtFechaIni.Text;
            string fecfin = txtFechaFin.Text;

            proccButtons(false);
            Microsoft.Office.Interop.Excel.Application excelApplication = new Microsoft.Office.Interop.Excel.Application();
            excelApplication.DisplayAlerts = false;
            Microsoft.Office.Interop.Excel.Workbook workBook = excelApplication.Workbooks.Add();

            Microsoft.Office.Interop.Excel.Worksheet vstoWorksheet = (Microsoft.Office.Interop.Excel.Worksheet)workBook.Worksheets[1];

            string fileExtension = funciones.GetDefaultExtension(excelApplication);

            string filename = string.Format("{0}\\Libro1{1}", Environment.CurrentDirectory, fileExtension);
            if (!File.Exists(filename))
            {
                funciones.form_excel_kardex_4_1(idcia, idtipocal, idtipoper, messem, anio, fecini, fecfin, vstoWorksheet);

                workBook.SaveAs(filename);
                excelApplication.Quit();
                MessageBox.Show("Exportación Correcta.. Favor de Guardar File", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);

                FileInfo fi = new FileInfo(filename);
                System.Diagnostics.Process.Start(filename);
            }
            else
            {
                FileStream fs = null;
                try
                {
                    using (fs = File.OpenRead(filename))
                    {
                        if (!fs.Equals(null))
                        {
                            fs.Close();
                            funciones.form_excel_kardex_4_1(idcia, idtipocal, idtipoper, messem, anio, fecini, fecfin, vstoWorksheet);

                            workBook.SaveAs(filename);
                            excelApplication.Quit();
                            MessageBox.Show("Exportación Correcta.. Favor de Guardar File", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            FileInfo fi = new FileInfo(filename);
                            System.Diagnostics.Process.Start(filename);
                        }
                    }
                }
                catch
                {
                    if (fs == null)
                    {
                        MessageBox.Show("Archivo Excel en Ejecucion... Cerrar Excel Formato 4.1 Sunat",
                            "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            proccButtons(true);
        }

        private void proccButtons(bool p)
        {
            lblcargando.Visible = (p == false ? true : false);
            txtMesSem.Enabled = p;
            btnExportar.Enabled = p;
            btnSalir.Enabled = p;
        }
    }
}