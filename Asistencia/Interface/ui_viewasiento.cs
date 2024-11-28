using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;

namespace CaniaBrava
{
    public partial class ui_viewasiento : Form
    {
        public string _namefilecab;
        public string _namefiledet;
        public string _ruta;

        private Form FormPadre;

        public Form _FormPadre
        {
            get { return FormPadre; }
            set { FormPadre = value; }
        }

        public ui_viewasiento()
        {
            InitializeComponent();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ui_viewasiento_Load(object sender, EventArgs e)
        {
            this.ui_Listar();
        }

        private void ui_Listar()
        {
            string query;
            Funciones funciones = new Funciones();
            string strConnDbase = @"Provider = Microsoft.Jet.OLEDB.4.0" +
                                       ";Data Source = " + _ruta +
                                       ";Extended Properties = dBASE IV" +
                                       ";User ID=Admin;Password=;";

            query = "SELECT csubdia,ccompro,cfeccom2,flag,cglosa,csitua,ccodmon,";
            query = query + " ctipo,ctipcam FROM  " + @_namefilecab;
            OleDbConnection oledbCnn;
            OleDbCommand oledbCmd;
            oledbCnn = new OleDbConnection(strConnDbase);
            try
            {
                oledbCnn.Open();
                oledbCmd = new OleDbCommand(query, oledbCnn);
                oledbCmd.Parameters.AddWithValue("@_namefilecab", _namefilecab);
                OleDbDataReader oledbReader = oledbCmd.ExecuteReader();
                while (oledbReader.Read())
                {
                    txtCompro.Text = oledbReader.GetString(0) + "-" + oledbReader.GetString(1);
                    txtFecha.Text = oledbReader.GetValue(2).ToString().Substring(0, 10);
                    txtConver.Text = oledbReader.GetString(3);
                    txtGlosa.Text = oledbReader.GetString(4);
                    if (oledbReader.GetString(5).Equals("F"))
                    {
                        txtSitua.Text = "Finalizado";
                    }
                    else
                    {
                        txtSitua.Text = "Vigente";
                    }
                    txtMoneda.Text = oledbReader.GetString(6);
                    txtTipoConver.Text = oledbReader.GetString(7);
                    txtTC.Text = oledbReader.GetValue(8).ToString();
                }
                oledbReader.Close();
                oledbCmd.Dispose();
                oledbCnn.Close();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }

            try
            {
                using (OleDbConnection cn = new OleDbConnection(strConnDbase))
                {
                    query = "SELECT dsubdia, dcompro, dsecue, dfeccom,dcuenta,dcodane,";
                    query = query + " dcencos,dcodmon,debe,haber,";
                    query = query + " dtipdoc,dnumdoc,dcodane2,dflag,";
                    query = query + " ddate,dxglosa,dusimpor,dmnimpor,dfeccom2,";
                    query = query + " dvanexo,dtipcam from  " + _namefiledet + " ; ";

                    using (OleDbDataAdapter myDataAdapter = new OleDbDataAdapter(query, cn))
                    {
                        DataSet myDataSet = new DataSet();
                        myDataAdapter.Fill(myDataSet, "tblDetalle");
                        funciones.formatearDataGridView(dgvData);
                        dgvData.DataSource = myDataSet.Tables["tblDetalle"];
                        dgvData.Columns[0].HeaderText = "Subdia";
                        dgvData.Columns[1].HeaderText = "Compro";
                        dgvData.Columns[2].HeaderText = "Secue";
                        dgvData.Columns[3].HeaderText = "Fecha Comp.";
                        dgvData.Columns[4].HeaderText = "Cuenta";
                        dgvData.Columns[5].HeaderText = "Anexo";
                        dgvData.Columns[6].HeaderText = "Centro de Costo";
                        dgvData.Columns[7].HeaderText = "Moneda";
                        dgvData.Columns[8].HeaderText = "Debe";
                        dgvData.Columns[9].HeaderText = "Haber";
                        dgvData.Columns[10].HeaderText = "Tipo. Doc.";
                        dgvData.Columns[11].HeaderText = "Nro. Doc.";
                        dgvData.Columns[12].HeaderText = "Anexo Ref.";
                        dgvData.Columns[13].HeaderText = "Conversión";
                        dgvData.Columns[14].HeaderText = "Fecha";
                        dgvData.Columns[15].HeaderText = "Glosa";
                        dgvData.Columns[16].HeaderText = "Importe US";
                        dgvData.Columns[17].HeaderText = "Importe MN";
                        dgvData.Columns[18].HeaderText = "Fecha Comp.";
                        dgvData.Columns[19].HeaderText = "Tipo Anexo";
                        dgvData.Columns[20].HeaderText = "Tipo de Cambio";

                        dgvData.Columns[0].Width = 50;
                        dgvData.Columns[1].Width = 60;
                        dgvData.Columns[2].Width = 45;
                        dgvData.Columns[3].Width = 60;
                        dgvData.Columns[4].Width = 60;
                        dgvData.Columns[5].Width = 70;
                        dgvData.Columns[6].Width = 80;
                        dgvData.Columns[7].Width = 50;
                        dgvData.Columns[8].Width = 75;
                        dgvData.Columns[9].Width = 75;
                        dgvData.Columns[10].Width = 45;
                        dgvData.Columns[11].Width = 80;
                        dgvData.Columns[12].Width = 70;
                        dgvData.Columns[13].Width = 40;
                        dgvData.Columns[14].Width = 75;
                        dgvData.Columns[15].Width = 200;
                        dgvData.Columns[16].Width = 75;
                        dgvData.Columns[17].Width = 75;
                        dgvData.Columns[18].Width = 75;
                        dgvData.Columns[19].Width = 60;
                        dgvData.Columns[20].Width = 60;
                    }
                    decimal debe = funciones.getDecimalRound(decimal.Parse(funciones.sumaColumnaDataGridView(dgvData, 8)), 1);
                    decimal haber = funciones.getDecimalRound(decimal.Parse(funciones.sumaColumnaDataGridView(dgvData, 9)), 1);

                    txtDebe.Text = debe.ToString();
                    txtHaber.Text = haber.ToString();

                    if (debe != haber)
                    {
                        lblMensaje.Visible = true;
                    }
                    else
                    {
                        lblMensaje.Visible = false;
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }

            try
            {
                using (OleDbConnection cn = new OleDbConnection(strConnDbase))
                {
                    query = "SELECT dsubdia, dcompro, dcuenta,dcencos,dcodmon,SUM(debe) as debe,";
                    query = query + " SUM(haber) as haber from  " + _namefiledet;
                    query = query + " group by  dsubdia, dcompro, dcuenta,dcencos,dcodmon ; ";

                    using (OleDbDataAdapter myDataAdapterRes = new OleDbDataAdapter(query, cn))
                    {
                        DataSet myDataSetRes = new DataSet();
                        myDataAdapterRes.Fill(myDataSetRes, "tblResumen");
                        funciones.formatearDataGridView(dgvResumen);
                        dgvResumen.DataSource = myDataSetRes.Tables["tblResumen"];
                        dgvResumen.Columns[0].HeaderText = "Subdia";
                        dgvResumen.Columns[1].HeaderText = "Compro";
                        dgvResumen.Columns[2].HeaderText = "Cuenta";
                        dgvResumen.Columns[3].HeaderText = "Centro de Costo";
                        dgvResumen.Columns[4].HeaderText = "Moneda";
                        dgvResumen.Columns[5].HeaderText = "Debe";
                        dgvResumen.Columns[6].HeaderText = "Haber";

                        dgvResumen.Columns[0].Width = 50;
                        dgvResumen.Columns[1].Width = 60;
                        dgvResumen.Columns[2].Width = 45;
                        dgvResumen.Columns[3].Width = 60;
                        dgvResumen.Columns[4].Width = 60;
                        dgvResumen.Columns[5].Width = 80;
                        dgvResumen.Columns[6].Width = 80;

                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void btnExcelDet_Click(object sender, EventArgs e)
        {
            Exporta exporta = new Exporta();
            exporta.Excel_FromDataGridView(dgvData);
        }

        private void btnExcelRes_Click(object sender, EventArgs e)
        {
            Exporta exporta = new Exporta();
            exporta.Excel_FromDataGridView(dgvResumen);
        }
    }
}