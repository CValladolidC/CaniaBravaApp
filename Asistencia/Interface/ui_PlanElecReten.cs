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
    public partial class ui_PlanElecReten : Form
    {
        public ui_PlanElecReten()
        {
            InitializeComponent();
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            ui_DatosPlanilla();
        }

        private void ui_DatosPlanilla()
        {
            DataTable dtIngTribDesc = new DataTable();

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

                /////////////////////////////////////////////////////////////////////////////////////////////////////////////
                //////////////////////DETALLE DE INGRESOS, DESCUENTOS Y APORTACIONES DEL TRABAJADOR//////////////////////////
                /////////////////////////////////////////////////////////////////////////////////////////////////////////////

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

                    if (chkIngTribDesc.Checked)
                    {

                        query = "select    A.idperplan,C.tipdoc,C.nrodoc,ROUND(SUM(A.reten),2) as v_0605,";
                        query = query + "  ROUND(SUM(A.total),2) as v_0924 ";
                        query = query + "  from desret A  inner join perret C on A.idperplan=C.idperplan and A.idcia=C.idcia ";
                        query = query + "  inner join calplan E on A.idtipocal=E.idtipocal and A.idcia=E.idcia ";
                        query = query + "  and A.messem=E.messem and A.anio=E.anio and A.idtipoper=E.idtipoper ";
                        query = query + "  where E.aniopdt='" + @aniopdt + "' and E.mespdt='" + @mespdt + "'  ";
                        query = query + "  and A.idcia='" + @idcia + "' ";
                        query = query + "  group by A.idperplan,C.tipdoc,C.nrodoc; ";

                        SqlDataAdapter da = new SqlDataAdapter();
                        da.SelectCommand = new SqlCommand(query, conexion);
                        da.Fill(dtIngTribDesc);

                        string filename = rutaFile + "/" + "0601" + aniopdt + mespdt + ruc + ".rem";
                        StreamWriter archivo = File.CreateText(filename);
                        archivo.Close();
                        foreach (DataRow row_dtIngTribDesc in dtIngTribDesc.Rows)
                        {

                            string tipdoc = funciones.formatoLongitudPdt(row_dtIngTribDesc["tipdoc"].ToString(), 2);
                            string nrodoc = funciones.formatoLongitudPdt(row_dtIngTribDesc["nrodoc"].ToString(), 15);
                            string pdt_0605 = funciones.formatoLongitudPdt("0605", 4);
                            string pdt_0924 = funciones.formatoLongitudPdt("0924", 4);
                            string valor_0605 = funciones.formatoPdtNumerico(row_dtIngTribDesc["v_0605"].ToString());
                            string valor_0924 = funciones.formatoPdtNumerico(row_dtIngTribDesc["v_0924"].ToString());
                            string devengado_0605 = funciones.formatoLongitudPdt(valor_0605, 9);
                            string pagado_0605 = funciones.formatoLongitudPdt(valor_0605, 9);
                            string devengado_0924 = funciones.formatoLongitudPdt(valor_0924, 9);
                            string pagado_0924 = funciones.formatoLongitudPdt(valor_0924, 9);

                            OpeIO opeIO = new OpeIO(filename);
                            opeIO.WriteNWL(tipdoc + "|" + nrodoc + "|" + pdt_0605 + "|" + devengado_0605 + "|"
                                + pagado_0605 + "|");
                            opeIO.WriteNWL(tipdoc + "|" + nrodoc + "|" + pdt_0924 + "|" + devengado_0924 + "|"
                                + pagado_0924 + "|");

                        }

                    }

                }
                conexion.Close();
                MessageBox.Show("Exportación SUNAT Finalizada", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }

        private void ui_PlanElecReten_Load(object sender, EventArgs e)
        {
            cmbMes.Text = "01   ENERO";
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}