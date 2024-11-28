using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Configuration;
using System.Data.SqlClient;

namespace CaniaBrava
{
    public partial class ui_buscarpresper : Form
    {
        private Form FormPadre;
        string _idcia;
        string _idperplan;
        string clasePadre;

        public Form _FormPadre
        {
            get { return FormPadre; }
            set { FormPadre = value; }
        }

        public ui_buscarpresper()
        {
            InitializeComponent();
        }

        public void ui_LoadPresPer()
        {
            ui_ListaPresPer();
        }

        private void ui_buscarpresper_Load(object sender, EventArgs e)
        {
            ui_ListaPresPer();
        }

        protected override bool ProcessCmdKey(ref System.Windows.Forms.Message msg, System.Windows.Forms.Keys keyData)
        {
            if ((!dgvdetalle.Focused))
                return base.ProcessCmdKey(ref msg, keyData);
            if (keyData != Keys.Enter)
                return base.ProcessCmdKey(ref msg, keyData);

            if (dgvdetalle.RowCount > 0)
            {
                DataGridViewRow row = dgvdetalle.CurrentRow;
                string codigo = Convert.ToString(row.Cells["idpresper"].Value);
                string clasePadre = this.clasePadre;

                if (clasePadre.Equals("ui_upddatosplanilla"))
                {
                    ((ui_upddatosplanilla)FormPadre)._TextBoxActivo.Text = codigo;
                }

            }
            this.DialogResult = DialogResult.OK;
            Close();
            return true;
        }

        public void ui_setValores(string idcia, string idperplan, string clasePadre)
        {
            this._idcia = idcia;
            this._idperplan = idperplan;
            this.clasePadre = clasePadre;
        }

        private void ui_ListaPresPer()
        {
            try
            {
                string idcia = this._idcia;
                string idperplan = this._idperplan;
                Funciones funciones = new Funciones();
                string query = " select A.idpresper,CONCAT(B.apepat,' ',B.apemat,' ',B.nombres) as nombre,";
                query = query + " A.fecha,A.mon,A.importe,C.desmaesgen as MotivoPrestamo,A.cuota,CASE ISNULL(D.importe) WHEN 1 THEN A.importe WHEN 0 THEN A.importe+sum(CASE D.tipo WHEN '+' THEN D.importe WHEN '-' THEN D.importe*-1 END) END as saldo,CASE A.suspendido WHEN '1' THEN 'SI' WHEN '0' THEN 'NO' END,A.motivo, ";
                query = query + " A.comen,A.suspendido,A.idcia,A.idperplan,A.tipodocpres,A.nrodocpres from presper A left join perplan B ";
                query = query + " on A.idperplan=B.idperplan and A.idcia=B.idcia left join maesgen C on ";
                query = query + " A.motivo=C.clavemaesgen and C.idmaesgen='032' left join view_detpresper D on A.idpresper=D.idpresper and A.idcia=D.idcia ";
                query = query + " where A.idcia='" + @idcia + "' and A.idperplan='" + @idperplan + "' and A.suspendido='0'";
                query = query + " group by A.idpresper,B.apepat,B.apemat,B.nombres,A.fecha,A.mon,A.importe,C.desmaesgen,A.cuota,A.suspendido,A.motivo, ";
                query = query + " A.comen,A.suspendido,A.idcia,A.idperplan,A.tipodocpres,A.nrodocpres ";
                query = query + " having saldo > 0 ";
                query = query + " order by A.idperplan,A.idpresper asc;";

                SqlConnection conexion = new SqlConnection();
                conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
                conexion.Open();

                try
                {
                    using (SqlDataAdapter myDataAdapter = new SqlDataAdapter(query, conexion))
                    {
                        DataSet myDataSet = new DataSet();
                        myDataAdapter.Fill(myDataSet, "tblPresPer");
                        funciones.formatearDataGridView(dgvdetalle);
                        dgvdetalle.DataSource = myDataSet.Tables["tblPresPer"];
                        dgvdetalle.Columns[0].HeaderText = "Código";
                        dgvdetalle.Columns[1].HeaderText = "Trabajador";
                        dgvdetalle.Columns[2].HeaderText = "Fecha";
                        dgvdetalle.Columns[3].HeaderText = "Moneda";
                        dgvdetalle.Columns[4].HeaderText = "Importe";
                        dgvdetalle.Columns[5].HeaderText = "Motivo del Préstamo";
                        dgvdetalle.Columns[6].HeaderText = "Cuota Mensual";
                        dgvdetalle.Columns[7].HeaderText = "Saldo";
                        dgvdetalle.Columns[8].HeaderText = "¿Préstamo Suspendido?";
                        dgvdetalle.Columns["motivo"].Visible = false;
                        dgvdetalle.Columns["comen"].Visible = false;
                        dgvdetalle.Columns["suspendido"].Visible = false;
                        dgvdetalle.Columns["idcia"].Visible = false;
                        dgvdetalle.Columns["idperplan"].Visible = false;
                        dgvdetalle.Columns["tipodocpres"].Visible = false;
                        dgvdetalle.Columns["nrodocpres"].Visible = false;
                        dgvdetalle.Columns[6].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                        dgvdetalle.Columns[7].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                        dgvdetalle.Columns[6].DefaultCellStyle.Format = "###,###.##";
                        dgvdetalle.Columns[7].DefaultCellStyle.Format = "###,###.##";
                        dgvdetalle.Columns[0].Width = 50;
                        dgvdetalle.Columns[1].Width = 220;
                        dgvdetalle.Columns[2].Width = 75;
                        dgvdetalle.Columns[3].Width = 75;
                        dgvdetalle.Columns[4].Width = 50;
                        dgvdetalle.Columns[5].Width = 220;
                        dgvdetalle.Columns[6].Width = 75;
                        dgvdetalle.Columns[7].Width = 75;
                        dgvdetalle.Columns[8].Width = 80;

                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
                conexion.Close();
            }
            catch (ArgumentOutOfRangeException)
            {

            }


        }
    }
}