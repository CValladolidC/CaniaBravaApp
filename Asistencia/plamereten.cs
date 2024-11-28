using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Configuration;
using System.IO;

namespace CaniaBrava
{
    public partial class plamereten : ui_form
    {
        string bd_prov = ConfigurationManager.AppSettings.Get("BD_PROV");

        public plamereten() { InitializeComponent(); }

        private void plamereten_Load(object sender, EventArgs e) { cmbMes.Text = "01   ENERO"; }

        private void btnNuevo_Click(object sender, EventArgs e) { ui_DatosPlanilla(); }

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
                        query = " SELECT A.idperplan,C.tipdoc,C.nrodoc,ROUND(SUM(A.reten),2) as v_0605,";
                        query += "ROUND(SUM(A.total),2) as v_0924 ";
                        query += "FROM desret A INNER JOIN perret C on A.idperplan=C.idperplan and A.idcia=C.idcia ";
                        query += "INNER JOIN calplan E on A.idtipocal=E.idtipocal and A.idcia=E.idcia ";
                        query += "AND A.messem=E.messem and A.anio=E.anio and A.idtipoper=E.idtipoper ";
                        query += "WHERE E.aniopdt='" + @aniopdt + "' AND E.mespdt='" + @mespdt + "' AND A.idcia='" + @idcia + "' ";
                        query += "GROUP BY A.idperplan,C.tipdoc,C.nrodoc; ";

                        if (bd_prov.Equals("accounting"))
                        {
                            if (idcia.Equals("03") || idcia.Equals("04"))
                            {
                                query = "CALL sp_4ta5tacatplame('" + @aniopdt + "','" + @mespdt + "','" + @idcia + "')";
                            }
                        }

                        SqlDataAdapter da = new SqlDataAdapter();
                        da.SelectCommand = new SqlCommand(query, conexion);
                        da.Fill(dtIngTribDesc);
                        string filename = rutaFile + "/" + "0601" + aniopdt + mespdt + ruc + ".rem";
                        StreamWriter archivo = File.CreateText(filename);
                        archivo.Close();
                        foreach (DataRow row_dtIngTribDesc in dtIngTribDesc.Rows)
                        {
                            string tipdoc = funciones.formatoLongitudPdt(row_dtIngTribDesc["tipdoc"].ToString(), 2).Trim();
                            string nrodoc = funciones.formatoLongitudPdt(row_dtIngTribDesc["nrodoc"].ToString(), 15).Trim();
                            string pdt_0605 = funciones.formatoLongitudPdt("0605", 4);
                            string pdt_0924 = funciones.formatoLongitudPdt("0924", 4);
                            string valor_0605 = funciones.formatoPdtNumerico(row_dtIngTribDesc["v_0605"].ToString()).Trim();
                            string valor_0924 = funciones.formatoPdtNumerico(row_dtIngTribDesc["v_0924"].ToString()).Trim();
                            string devengado_0605 = funciones.formatoLongitudPdt(valor_0605, 9).Trim();
                            string pagado_0605 = funciones.formatoLongitudPdt(valor_0605, 9).Trim();
                            string devengado_0924 = funciones.formatoLongitudPdt(valor_0924, 9).Trim();
                            string pagado_0924 = funciones.formatoLongitudPdt(valor_0924, 9).Trim();

                            OpeIO opeIO = new OpeIO(filename);
                            opeIO.WriteNWL(tipdoc + "|" + nrodoc + "|" + pdt_0605 + "|" + devengado_0605 + "|" + pagado_0605 + "|");
                            opeIO.WriteNWL(tipdoc + "|" + nrodoc + "|" + pdt_0924 + "|" + devengado_0924 + "|" + pagado_0924 + "|");
                        }
                    }
                }
                conexion.Close();
                MessageBox.Show("Exportación SUNAT PLAME Finalizada", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnSalir_Click(object sender, EventArgs e) { this.Close(); }
    }
}