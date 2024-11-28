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

namespace CaniaBrava
{
    public partial class ui_res5tacat : Form
    {
        string query = string.Empty;
        string bd_prov = ConfigurationManager.AppSettings.Get("BD_PROV");

        public ui_res5tacat() { InitializeComponent(); }

        private void ui_res5tacat_Load(object sender, EventArgs e) { cmbMes.Text = "01   ENERO"; }

        private void ui_ListaPlan()
        {
            Funciones funciones = new Funciones();
            GlobalVariables globalVariable = new GlobalVariables();
            DataTable dtplan = new DataTable();
            string idcia = globalVariable.getValorCia();
            string aniopdt = txtAnio.Text;
            string mespdt = funciones.getValorComboBox(cmbMes, 2);

            query = " SELECT A.idperplan,C.Parm1maesgen as cortotipodoc,A.nrodoc, ";
            query += "CONCAT(A.apepat,' ',A.apemat,', ',A.nombres) as nombre, A.fecnac,min(fecha), ";
            query += "max(fecha),SUM(P.cantidad) as cantidad,SUM(P.subtotal) as subtotal,";
            query += "SUM(P.adicional) as adicional,SUM(P.reten) as reten,SUM(P.total) as total, '' as v_theend ";
            query += "FROM desret P ";
            query += "INNER JOIN calplan S on P.anio=S.anio and P.messem=S.messem and ";
            query += "P.idtipoper=S.idtipoper and P.idtipocal=S.idtipocal and P.idcia=S.idcia ";
            query += "INNER JOIN perret A on P.idcia=A.idcia and P.idperplan=A.idperplan ";
            query += "LEFT JOIN maesgen C on C.idmaesgen='002' AND A.tipdoc=C.clavemaesgen  ";
            query += "WHERE S.aniopdt='" + @aniopdt + "' AND S.mespdt='" + @mespdt + "' AND P.idcia='" + @idcia + "' ";
            query += "GROUP BY A.idperplan,C.Parm1maesgen,A.nrodoc,A.apepat,A.apemat,A.nombres,A.fecnac ";

            if (bd_prov.Equals("accounting"))
            {
                if (idcia.Equals("03") || idcia.Equals("04"))
                {
                    query = "CALL sp_4ta5tacat('" + @aniopdt + "','" + @mespdt + "','" + @idcia + "')";
                }
            }

            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();
            SqlDataAdapter da = new SqlDataAdapter();

            da.SelectCommand = new SqlCommand(query, conexion);
            da.Fill(dtplan);
            funciones.formatearDataGridView(dgvdetalle);
            dgvdetalle.DataSource = dtplan;

            if (dgvdetalle.Rows.Count > 0)
            {
                if (!dgvdetalle.Rows[0].Cells["v_theend"].Value.ToString().Equals("0"))
                {
                    MessageBox.Show("Existe un monto restante de " + dgvdetalle.Rows[0].Cells["v_theend"].Value, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            dgvdetalle.Columns[0].HeaderText = "Cód. Int.";
            dgvdetalle.Columns[1].HeaderText = "Doc. Identidad";
            dgvdetalle.Columns[2].HeaderText = "Nro. Doc.";
            dgvdetalle.Columns[3].HeaderText = "Apellidos y Nombres";
            dgvdetalle.Columns[4].HeaderText = "Fecha Nac.";
            dgvdetalle.Columns[5].HeaderText = "De";
            dgvdetalle.Columns[6].HeaderText = "Hasta";
            dgvdetalle.Columns[7].HeaderText = "Cantidad";
            dgvdetalle.Columns[8].HeaderText = "Sub Total";
            dgvdetalle.Columns[9].HeaderText = "Adicional";
            dgvdetalle.Columns[10].HeaderText = "Retención";
            dgvdetalle.Columns[11].HeaderText = "Importe Total";
            dgvdetalle.Columns["idperplan"].Frozen = true;
            dgvdetalle.Columns["cortotipodoc"].Frozen = true;
            dgvdetalle.Columns["nrodoc"].Frozen = true;
            dgvdetalle.Columns["nombre"].Frozen = true;
            dgvdetalle.Columns["v_theend"].Visible = false;

            int i;

            for (i = 7; i < 12; i++)
            {
                dgvdetalle.Columns[i].DefaultCellStyle.Format = "###,###.##";
                dgvdetalle.Columns[i].DefaultCellStyle.Alignment = DataGridViewContentAlignment.BottomRight;
                dgvdetalle.Columns[i].Width = 75;
            }

            dgvdetalle.Columns[0].Width = 50;
            dgvdetalle.Columns[1].Width = 50;
            dgvdetalle.Columns[2].Width = 60;
            dgvdetalle.Columns[3].Width = 200;
            dgvdetalle.Columns[4].Width = 75;
            dgvdetalle.Columns[5].Width = 75;
            dgvdetalle.Columns[6].Width = 75;
            conexion.Close();
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            Exporta exporta = new Exporta();
            dgvdetalle.Columns.Remove("v_theend");
            exporta.Excel_FromDataGridView(dgvdetalle);
        }

        private void btnSalir_Click(object sender, EventArgs e) { this.Close(); }

        private void cmbMes_SelectedIndexChanged(object sender, EventArgs e) { ui_ListaPlan(); }

        private void txtAnio_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r') { ui_ListaPlan(); }
        }
    }
}