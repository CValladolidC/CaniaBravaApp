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
using System.IO;
using System.Data.OleDb;

namespace CaniaBrava
{
    public partial class ui_asientos : Form
    {
        public ui_asientos()
        {
            InitializeComponent();
        }

        private string ui_validarDatos()
        {

            string valorValida = "G";

            if (txtAnio.Text == string.Empty && valorValida == "G")
            {
                valorValida = "B";
                MessageBox.Show("No ha ingresado Año", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtAnio.Focus();
            }

            if (txtSubdia.Text == string.Empty && valorValida == "G")
            {
                valorValida = "B";
                MessageBox.Show("No ha ingresado Subdiario", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtSubdia.Focus();
            }

            if (txtCompro.Text == string.Empty && valorValida == "G")
            {
                valorValida = "B";
                MessageBox.Show("No ha ingresado Comprobante", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtCompro.Focus();
            }

            if (UtileriasFechas.IsDate(txtFecha.Text) == false && valorValida == "G")
            {
                valorValida = "B";
                MessageBox.Show("Fecha no válida", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtFecha.Focus();
            }

            if (txtGlosa.Text == string.Empty && valorValida == "G")
            {
                valorValida = "B";
                MessageBox.Show("No ha ingresado Glosa", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtGlosa.Focus();
            }

            if (valorValida.Equals("G"))
            {
                try
                {
                    float numero = float.Parse(txtTC.Text);
                    if (numero <= 0)
                    {
                        valorValida = "B";
                        MessageBox.Show("Tipo de Cambio Inválido", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txtTC.Focus();
                    }
                }
                catch (FormatException)
                {

                    valorValida = "B";
                    MessageBox.Show("Tipo de Cambio Inválido", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtTC.Focus();
                }
            }

            if (txtDir.Text == string.Empty && valorValida == "G")
            {
                valorValida = "B";
                MessageBox.Show("No ha seleccionado directorio de destino", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtDir.Focus();
            }

            GlobalVariables gv = new GlobalVariables();
            string idcia = gv.getValorCia();
            if (valorValida.Equals("G"))
            {
                Funciones funciones = new Funciones();
                string idtipoper = funciones.getValorComboBox(cmbTipoTrabajador, 1);
                string idtipoplan = funciones.getValorComboBox(cmbTipoPlan, 3);
                valorValida = ui_validaCenCos(idcia, idtipoper, idtipoplan);
            }

            if (valorValida.Equals("G"))
            {
                CiaFile ciafile = new CiaFile();

                string codaux = ciafile.ui_getDatosCiaFile(idcia, "CODAUX");
                if (codaux == string.Empty)
                {
                    valorValida = "B";
                    MessageBox.Show("No ha sido asignado un código auxiliar CONCAR al empleador", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            return valorValida;

        }

        private void ui_asientos_Load(object sender, EventArgs e)
        {
            string query;
            Funciones funciones = new Funciones();
            GlobalVariables variables = new GlobalVariables();
            string idcia = variables.getValorCia();
            query = "SELECT idtipoplan as clave,destipoplan as descripcion FROM tipoplan ";
            query = query + " where idtipoplan in (select idtipoplan from reglabcia where ";
            query = query + " idcia='" + @variables.getValorCia() + "') order by 1 asc;";
            funciones.listaComboBox(query, cmbTipoPlan, "");
            query = "SELECT idtipoper as clave,destipoper as descripcion FROM tipoper order by 1 asc";
            funciones.listaComboBox(query, cmbTipoTrabajador, "");
            query = "SELECT idcencos as clave,descencos as descripcion FROM cencos ";
            query = query + " WHERE idcia='" + @idcia + "' and statecencos='V';";
            funciones.listaComboBox(query, cmbCentroCosto, "");
            cmbMes.Text = "01   ENERO";
            cmbListar.Text = "S    SIN CENTRO DE COSTO ASIGNADO";
            txtSubdia.Clear();
            txtCompro.Clear();
            txtFecha.Text = DateTime.Now.ToString("dd/MM/yyyy");
            txtGlosa.Clear();
            ui_listaPer();
            ui_listaCenCon();
        }

        private void ui_limpiarCenCon()
        {
            cmbCentroCosto.Text = string.Empty;
            txtPorcentaje.Text = string.Empty;
        }

        public void ui_listaCenCon()
        {
            GlobalVariables gv = new GlobalVariables();
            string idcia = gv.getValorCia();

            Funciones funciones = new Funciones();
            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();
            string query = " select A.idcencos,B.descencos,A.porcentaje,A.idcia ";
            query = query + " from cencon A left join cencos B on A.idcia=B.idcia ";
            query = query + " and A.idcencos=B.idcencos where A.idcia='" + idcia + "' ";
            query = query + " order by A.idcencos asc;";
            try
            {
                using (SqlDataAdapter myDataAdapter = new SqlDataAdapter(query, conexion))
                {
                    DataSet myDataSet = new DataSet();
                    myDataAdapter.Fill(myDataSet, "tblCenCon");
                    funciones.formatearDataGridView(dgvData);
                    dgvData.DataSource = myDataSet.Tables["tblCenCon"];
                    dgvData.Columns[0].HeaderText = "Código";
                    dgvData.Columns[1].HeaderText = "Centro de Costo";
                    dgvData.Columns[2].HeaderText = "Porcentaje";
                    dgvData.Columns["idcia"].Visible = false;
                    dgvData.Columns[0].Width = 100;
                    dgvData.Columns[1].Width = 300;
                    dgvData.Columns[2].Width = 100;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            conexion.Close();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            Close();
        }

        public void ui_listaPer()
        {
            GlobalVariables gv = new GlobalVariables();
            string idcia = gv.getValorCia();

            Funciones funciones = new Funciones();
            string idtipoper = funciones.getValorComboBox(cmbTipoTrabajador, 1);
            string idtipoplan = funciones.getValorComboBox(cmbTipoPlan, 3);
            string modo = funciones.getValorComboBox(cmbListar, 1);
            string cadmodo = string.Empty;
            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();
            if (modo.Equals("S"))
            {
                cadmodo = " having porcentaje<>100 ;";
            }
            else
            {
                cadmodo = " having porcentaje=100 ;";
            }

            string query = " select  A.idperplan,CONCAT(A.apepat,' ',A.apemat,' , ',A.nombres) as nombre,";
            query = query + " CASE ISNULL(SUM(B.porcentaje)) WHEN 1 THEN 0 ";
            query = query + " WHEN 0 THEN SUM(B.porcentaje) END as porcentaje from perplan ";
            query = query + " A left join cenper B on ";
            query = query + " A.idcia=B.idcia and A.idperplan=B.idperplan where A.idcia='" + @idcia + "' ";
            query = query + " and A.idtipoper='" + @idtipoper + "' and A.idtipoplan='" + @idtipoplan + "' ";
            query = query + " group by A.idperplan,A.apepat,A.apemat,A.nombres ";
            query = query + cadmodo;
            try
            {
                using (SqlDataAdapter myDataAdapter = new SqlDataAdapter(query, conexion))
                {
                    DataSet myDataSet = new DataSet();
                    myDataAdapter.Fill(myDataSet, "tblSinCenCon");
                    funciones.formatearDataGridView(dgvPersonal);
                    dgvPersonal.DataSource = myDataSet.Tables["tblSinCenCon"];
                    dgvPersonal.Columns[0].HeaderText = "Código";
                    dgvPersonal.Columns[1].HeaderText = "Apellidos y Nombres";
                    dgvPersonal.Columns[2].HeaderText = "Porcentaje en Centros de Costo";
                    dgvPersonal.Columns[0].Width = 100;
                    dgvPersonal.Columns[1].Width = 300;
                    dgvPersonal.Columns[2].Width = 100;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            conexion.Close();
        }

        public string ui_validaCenCos(string idcia, string idtipoper, string idtipoplan)
        {
            string valorValida = "G";
            DataTable dtcenper = new DataTable();
            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();
            string query = " select  A.idperplan,CONCAT(A.apepat,' ',A.apemat,' , ',A.nombres) as nombre,";
            query = query + " CASE ISNULL(SUM(B.porcentaje)) WHEN 1 THEN 0 ";
            query = query + " WHEN 0 THEN SUM(B.porcentaje) END as porcentaje from perplan ";
            query = query + " A left join cenper B on ";
            query = query + " A.idcia=B.idcia and A.idperplan=B.idperplan where A.idcia='" + @idcia + "' ";
            query = query + " and A.idtipoper='" + @idtipoper + "' and A.idtipoplan='" + @idtipoplan + "' ";
            query = query + " group by A.idperplan,A.apepat,A.apemat,A.nombres having porcentaje<>100 ;";
            SqlDataAdapter dadebe = new SqlDataAdapter();
            dadebe.SelectCommand = new SqlCommand(query, conexion);
            dadebe.Fill(dtcenper);
            int registros = dtcenper.Rows.Count;
            if (registros > 0)
            {
                MessageBox.Show("Existen " + registros.ToString() + " trabajadores con Centro de Costo no asignado o mal asignado", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                valorValida = "B";
            }

            return valorValida;

        }

        private void btnGenerar_Click(object sender, EventArgs e)
        {
            try
            {
                string valorValida = ui_validarDatos();
                if (valorValida.Equals("G"))
                {
                    Funciones funciones = new Funciones();
                    GlobalVariables globalVariable = new GlobalVariables();
                    CiaFile ciafile = new CiaFile();

                    string idtipoper = funciones.getValorComboBox(cmbTipoTrabajador, 1);
                    string idtipoplan = funciones.getValorComboBox(cmbTipoPlan, 3);
                    string idcia = globalVariable.getValorCia();
                    string anio = txtAnio.Text.Trim();
                    string mes = funciones.getValorComboBox(cmbMes, 2);
                    string nombremes = cmbMes.Text.Substring(3, (cmbMes.Text.Length - 3));
                    string subdia = txtSubdia.Text.Trim();
                    string compro = txtCompro.Text.Trim();
                    string fecha = txtFecha.Text;
                    string glosa = txtGlosa.Text;
                    string ruta = txtDir.Text.Trim();
                    float tc = float.Parse(txtTC.Text);
                    string fechatemp = txtFecha.Text + funciones.replicateCadena(" ", 10);
                    string cfecha = fechatemp.Substring(8, 2) + fechatemp.Substring(3, 2) + fechatemp.Substring(0, 2);
                    string codauxtmp = ciafile.ui_getDatosCiaFile(idcia, "CODAUX");
                    string codaux = funciones.replicateCadena("0", 4 - codauxtmp.Trim().Length) + codauxtmp.Trim();

                    string tablacabdbf = "CTC" + codaux.Substring(2, 2) + anio.Substring(2, 2);
                    string tabladetdbf = "CTD" + codaux.Substring(2, 2) + anio.Substring(2, 2);

                    string strConnDbase = @"Provider = Microsoft.Jet.OLEDB.4.0" +
                                           ";Data Source = " + ruta +
                                           ";Extended Properties = dBASE IV" +
                                           ";User ID=Admin;Password=;";

                    File.Delete(ruta + "\\" + tablacabdbf + ".DBF");
                    File.Delete(ruta + "\\" + tabladetdbf + ".DBF");

                    using (OleDbConnection cn = new OleDbConnection(strConnDbase))
                    {
                        //////////////////////////////////////////////////////////////////////////////////////////////////////
                        ////CREACION DE TABLAS DBF ///////////////////////////////////////////////////////////////////////////
                        /////////////////////////////////////////////////////////////////////////////////////////////////////

                        string sql = "CREATE TABLE " + tablacabdbf + " ( csubdia Char(4), ccompro Char(6) , cfeccom Char(6),";
                        sql += "ccodmon Char(2),csitua Char(1),ctipcam Single,cglosa Char(40),ctotal Numeric(14,2),ctipo Char(1),";
                        sql += "cflag Char(1),cdate date,chora Char(8),cuser Char(5),cfeccam Char(6),corig Char(2),ctipcom Char(2),";
                        sql += "cextor Char(1),cfeccom2 date,cfeccam2 date,copcion Char(1),cform Char(1)) ";

                        string sql2 = "CREATE TABLE " + tabladetdbf + " (dsubdia Char(4), dcompro Char(6) , dsecue Char(4), dfeccom Char(6),";
                        sql2 += "dcuenta Char(12),dcodane Char(18),dcencos Char(6),dcodmon Char(2),ddh Char(1),dimport Numeric(14,2),";
                        sql2 += "dtipdoc Char(2),dnumdoc Char(20),dfecdoc Char(6),dfecven Char(6),darea Char(3),dflag Char(1),ddate date,";
                        sql2 += "dxglosa Char(30),dusimpor Numeric(14,2),dmnimpor Numeric(14,2),dcodarc Char(2),dfeccom2 date,";
                        sql2 += "dfecdoc2 date,dfecven2 date,dcodane2 Char(18),dvanexo Char(1),dvanexo2 Char(1),dtipcam Numeric(11,6),dcantid Numeric(13,3),";
                        sql2 += "dmedpag Char(8),Dtidref Char(8),dndoref Char(30),dfecref date,dbimref Numeric(14,2),digvref Numeric(14,2),";
                        sql2 += "debe Numeric(14,1),haber Numeric(14,1)) ";

                        using (OleDbCommand cmd = new OleDbCommand(sql, cn))
                        {
                            cn.Open();
                            cmd.ExecuteNonQuery();
                            cn.Close();
                        }

                        using (OleDbCommand cmd2 = new OleDbCommand(sql2, cn))
                        {
                            cn.Open();
                            cmd2.ExecuteNonQuery();
                            cn.Close();
                        }

                        //////////////////////////////////////////////////////////////////////////////////////////////////////
                        ////AGREGA REGISTRO A LA TABLA DE DETALLE ////////////////////////////////////////////////////////////
                        /////////////////////////////////////////////////////////////////////////////////////////////////////
                        string query;
                        float ctotaldebe = 0;
                        float ctotalhaber = 0;
                        CuenCon cuencon = new CuenCon();
                        string detallado = string.Empty;
                        int secue = 0;
                        string secuetmp = string.Empty;
                        string csecue = string.Empty;


                        //////////////////////////////////////////////////////////////////////////////////////////////////////////////
                        //////////////////////////////////////C U E N T A S   D E L    D E B E////////////////////////////////////////
                        //////////////////////////////////////////////////////////////////////////////////////////////////////////////

                        DataTable dtdebe = new DataTable();
                        SqlConnection conexion = new SqlConnection();
                        conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
                        conexion.Open();

                        query = "  select  A.idtipocal,A.idconplan,B.ctadebe from conbol A ";
                        query += " inner join detconplan B on A.idcia=B.idcia and ";
                        query += " A.idtipoplan=B.idtipoplan and A.idtipocal=B.idtipocal and A.idtipoper=B.idtipoper ";
                        query += " and A.idconplan=B.idconplan ";
                        query += " inner join calplan E on A.idtipocal=E.idtipocal and A.idcia=E.idcia ";
                        query += " and A.messem=E.messem and A.anio=E.anio and A.idtipoper=E.idtipoper ";
                        query += " where E.aniopdt='" + @anio + "' and E.mespdt='" + @mes + "'  ";
                        query += " and A.idcia='" + @idcia + "' and A.idtipoper='" + @idtipoper + "' ";
                        query += " and A.idtipoplan='" + @idtipoplan + "' and B.ctadebe<>'' ";
                        query += " group by A.idtipocal,A.idconplan,B.ctadebe ";
                        query += " having SUM(valor)>0 order by A.idconplan asc;";

                        SqlDataAdapter dadebe = new SqlDataAdapter();
                        dadebe.SelectCommand = new SqlCommand(query, conexion);
                        dadebe.Fill(dtdebe);

                        foreach (DataRow row_dtdebe in dtdebe.Rows)
                        {
                            string ctadebe = row_dtdebe["ctadebe"].ToString();
                            string idconplan = row_dtdebe["idconplan"].ToString();
                            string idtipocal = row_dtdebe["idtipocal"].ToString();
                            detallado = cuencon.ui_getDatosCuenCon(idcia, ctadebe, "DETALLADO");
                            if (detallado == "S")
                            {
                                DataTable dtper = new DataTable();
                                query = "  select  A.idperplan,round(SUM(A.valor),2) as valor from conbol A ";
                                query += " inner join calplan E on A.idtipocal=E.idtipocal and A.idcia=E.idcia ";
                                query += " and A.messem=E.messem and A.anio=E.anio and A.idtipoper=E.idtipoper ";
                                query += " where E.aniopdt='" + @anio + "' and E.mespdt='" + @mes + "'  ";
                                query += " and A.idcia='" + @idcia + "' and A.idtipoper='" + @idtipoper + "' ";
                                query += " and A.idtipoplan='" + @idtipoplan + "' and A.idconplan='" + @idconplan + "' ";
                                query += " and A.idtipocal='" + @idtipocal + "' ";
                                query += " group by A.idperplan ";
                                query += " having valor>0 order by A.idconplan asc;";
                                SqlDataAdapter daPer = new SqlDataAdapter();
                                daPer.SelectCommand = new SqlCommand(query, conexion);
                                daPer.Fill(dtper);
                                foreach (DataRow row_dtper in dtper.Rows)
                                {
                                    string idperplan = row_dtper["idperplan"].ToString();
                                    float valor = float.Parse(row_dtper["valor"].ToString());
                                    DataTable dtcenper = new DataTable();
                                    query = "  select B.codaux,CASE ISNULL(A.porcentaje) WHEN 1 THEN 0 ";
                                    query += " WHEN 0 THEN A.porcentaje END as porcentaje from cenper A left join cencos B ";
                                    query += " on A.idcia=B.idcia and A.idcencos=B.idcencos ";
                                    query += " where A.idcia='" + @idcia + "' and ";
                                    query += " A.idperplan='" + @idperplan + "' order by  B.codaux asc";
                                    SqlDataAdapter daCenPer = new SqlDataAdapter();
                                    daCenPer.SelectCommand = new SqlCommand(query, conexion);
                                    daCenPer.Fill(dtcenper);
                                    foreach (DataRow row_dtcenper in dtcenper.Rows)
                                    {
                                        string cencos = row_dtcenper["codaux"].ToString();
                                        float importe = (valor * float.Parse(row_dtcenper["porcentaje"].ToString()) / 100);
                                        string numdoc = nombremes + " - " + anio;
                                        float mnimpor = importe;
                                        float usimpor = importe / tc;
                                        secue++;
                                        secuetmp = secue.ToString();
                                        csecue = funciones.replicateCadena("0", 4 - secuetmp.Trim().Length) + secuetmp.Trim();
                                        query = " Insert into " + tabladetdbf + "(dsubdia, dcompro, dsecue, dfeccom,";
                                        query += "dcuenta,dcodane,dcencos,dcodmon,ddh,dimport,";
                                        query += "dtipdoc,dnumdoc,dflag,ddate,";
                                        query += "dxglosa,dusimpor,dmnimpor,dfeccom2,dvanexo,dtipcam,debe,haber,dfecdoc2,dfecdoc) ";
                                        query += " VALUES('" + @subdia + "','" + @compro + "','" + @csecue + "','" + @cfecha + "',";
                                        query += " '" + @ctadebe + "','','" + @cencos + "','MN','D','" + @importe + "',";
                                        query += " 'PL','" + @numdoc + "','S','" + @fecha + "', ";
                                        query += " '" + @glosa + "','" + @usimpor + "','" + @mnimpor + "','" + @fecha + "', ";
                                        query += " '','" + @tc + "','" + @importe + "','0','" + @fecha + "','" + @cfecha + "')";
                                        ctotaldebe = ctotaldebe + importe;
                                        OleDbCommand sqlOleDbCommandDet = new OleDbCommand(query, cn);
                                        cn.Open();
                                        sqlOleDbCommandDet.ExecuteNonQuery();
                                        cn.Close();
                                    }
                                }
                            }
                            else
                            {
                                float importe;
                                float valor;
                                DataTable dtcenper = new DataTable();
                                query = "  SELECT idcencos,descencos,codaux from cencos ";
                                query += " WHERE idcia='" + @idcia + "' and idcencos in ";
                                query += " (SELECT idcencos from cenper where idcia='" + @idcia + "') ";
                                query += " order by codaux asc ";
                                SqlDataAdapter daCenPer = new SqlDataAdapter();
                                daCenPer.SelectCommand = new SqlCommand(query, conexion); ;
                                daCenPer.Fill(dtcenper);
                                foreach (DataRow row_dtcenper in dtcenper.Rows)
                                {
                                    string idcencos = row_dtcenper["idcencos"].ToString();
                                    string cencos = row_dtcenper["codaux"].ToString();
                                    DataTable dtper = new DataTable();
                                    query = "  SELECT  A.idperplan,SUM(A.valor) as valor,F.porcentaje from conbol A ";
                                    query += " inner join calplan E on A.idtipocal=E.idtipocal and A.idcia=E.idcia ";
                                    query += " and A.messem=E.messem and A.anio=E.anio and A.idtipoper=E.idtipoper ";
                                    query += " inner join cenper F on A.idcia=F.idcia and A.idperplan=F.idperplan ";
                                    query += " and F.idcencos='" + @idcencos + "' ";
                                    query += " where E.aniopdt='" + @anio + "' and E.mespdt='" + @mes + "'  ";
                                    query += " and A.idcia='" + @idcia + "' and A.idtipoper='" + @idtipoper + "' ";
                                    query += " and A.idtipoplan='" + @idtipoplan + "' and A.idconplan='" + @idconplan + "' ";
                                    query += " and A.idtipocal='" + @idtipocal + "' ";
                                    query += " group by A.idperplan,F.porcentaje ";
                                    query += " having valor>0 order by A.idconplan asc;";
                                    SqlDataAdapter daPer = new SqlDataAdapter();
                                    daPer.SelectCommand = new SqlCommand(query, conexion);
                                    daPer.Fill(dtper);
                                    importe = 0;
                                    foreach (DataRow row_dtper in dtper.Rows)
                                    {
                                        valor = float.Parse(row_dtper["valor"].ToString());
                                        importe = importe + (valor * float.Parse(row_dtper["porcentaje"].ToString()) / 100);
                                    }

                                    string numdoc = nombremes + " - " + anio;
                                    float mnimpor = importe;
                                    float usimpor = importe / tc;
                                    secue++;
                                    secuetmp = secue.ToString();
                                    csecue = funciones.replicateCadena("0", 4 - secuetmp.Trim().Length) + secuetmp.Trim();

                                    query = "SELECT COUNT(1) as cnt FROM " + tabladetdbf + " where dcuenta = '" + @ctadebe + "' and dcencos = '" + @cencos + "'; ";

                                    cn.Open();
                                    OleDbCommand sqlCommandConsult = new OleDbCommand(query, cn);
                                    OleDbDataReader reader = sqlCommandConsult.ExecuteReader();
                                    reader.Read();
                                    int contDato = int.Parse(reader[0].ToString());
                                    reader.Close();
                                    cn.Close();

                                    if (contDato > 0)
                                    {
                                        query = " UPDATE " + tabladetdbf + " SET dimport = dimport + '" + @importe + "',dusimpor = dusimpor + '" + @usimpor + "'";
                                        query += ",dmnimpor = dmnimpor + '" + @mnimpor + "',debe = debe + '" + @importe + "' ";
                                        query += "WHERE dcuenta = '" + @ctadebe + "' and dcencos = '" + @cencos + "'; ";
                                    }
                                    else
                                    {
                                        query = " Insert into " + tabladetdbf + "(dsubdia, dcompro, dsecue, dfeccom,";
                                        query += "dcuenta,dcodane,dcencos,dcodmon,ddh,dimport,";
                                        query += "dtipdoc,dnumdoc,dflag,ddate,";
                                        query += "dxglosa,dusimpor,dmnimpor,dfeccom2,dvanexo,dtipcam,debe,haber,dfecdoc2,dfecdoc) ";
                                        query += " VALUES('" + @subdia + "','" + @compro + "','" + @csecue + "','" + @cfecha + "',";
                                        query += " '" + @ctadebe + "','','" + @cencos + "','MN','D','" + @importe + "',";
                                        query += " 'PL','" + @numdoc + "','S','" + @fecha + "', ";
                                        query += " '" + @glosa + "','" + @usimpor + "','" + @mnimpor + "','" + @fecha + "', ";
                                        query += " '','" + @tc + "','" + @importe + "','0','" + @fecha + "','" + @cfecha + "');";
                                    }

                                    ctotaldebe = ctotaldebe + importe;
                                    OleDbCommand sqlOleDbCommandDet = new OleDbCommand(query, cn);
                                    cn.Open();
                                    sqlOleDbCommandDet.ExecuteNonQuery();
                                    cn.Close();
                                }
                            }
                        }
                        /*
                        query = "";
                        query = "Insert into " + tabladetdbf + "(dsecue, dsubdia, dcompro, dfeccom,";
                        query += "dcuenta,dcodane,dcencos,dcodmon,ddh,dimport,";
                        query += "dtipdoc,dnumdoc,dflag,ddate,";
                        query += "dxglosa,dusimpor,dmnimpor,dfeccom2,dvanexo,dtipcam,debe,haber,dfecdoc2,dfecdoc) ";
                        query += "select '' AS dsecue, x.dsubdia, x.dcompro, x.dfeccom, ";
                        query += "x.dcuenta,x.dcodane,x.dcencos,x.dcodmon,x.ddh,sum(x.dimport) as dimport, ";
                        query += "x.dtipdoc,x.dnumdoc,x.dflag,x.ddate, ";
                        query += "x.dxglosa,sum(x.dusimpor) as dusimpor,sum(x.dmnimpor) as dmnimpor,x.dfeccom2,x.dvanexo,x.dtipcam,sum(x.debe) as debe,sum(x.haber) as haber,x.dfecdoc2,x.dfecdoc ";
                        query += "from _" + tabladetdbf + " x ";
                        query += "group by x.dsubdia, x.dcompro, x.dfeccom, ";
                        query += "x.dcuenta,x.dcodane,x.dcencos,x.dcodmon,x.ddh, ";
                        query += "x.dtipdoc,x.dnumdoc,x.dflag,x.ddate, ";
                        query += "x.dxglosa,x.dfeccom2,x.dvanexo,x.dtipcam,x.dfecdoc2,x.dfecdoc ";
                        OleDbCommand sqlOleDbCommandDet2 = new OleDbCommand(query, cn);
                        cn.Open();
                        sqlOleDbCommandDet2.ExecuteNonQuery();
                        cn.Close();*/

                        //////////////////////////////////////////////////////////////////////////////////////////////////////////////
                        //////////////////////////////////////C U E N T A S   D E L    H A B E R//////////////////////////////////////
                        //////////////////////////////////////////////////////////////////////////////////////////////////////////////

                        DataTable dthaber = new DataTable();
                        query = " select  A.idtipocal,A.idconplan,B.ctahaber,C.modoane,C.modoaneref,C.tipane,";
                        query += " CASE WHEN C.ane = '' THEN '" + @anio + "' ELSE C.ane END AS ane,C.tipaneref,";
                        query += " C.aneref,round(SUM(A.valor),2) as valor from conbol A ";
                        query += " inner join detconplan B on A.idcia=B.idcia and ";
                        query += " A.idtipoplan=B.idtipoplan and A.idtipocal=B.idtipocal and A.idtipoper=B.idtipoper ";
                        query += " and A.idconplan=B.idconplan ";
                        query += " inner join cuencon C on B.ctahaber=C.codcuenta  and B.idcia=C.idcia /*and (C.ane = '" + @anio + "' OR C.ane = '')*/ and C.s_estado = 1 ";
                        query += " inner join calplan E on A.idtipocal=E.idtipocal and A.idcia=E.idcia ";
                        query += " and A.messem=E.messem and A.anio=E.anio and A.idtipoper=E.idtipoper ";
                        query += " where E.aniopdt='" + @anio + "' and E.mespdt='" + @mes + "'  ";
                        query += " and A.idcia='" + @idcia + "' and A.idtipoper='" + @idtipoper + "' ";
                        query += " and A.idtipoplan='" + @idtipoplan + "' and B.ctahaber<>'' ";
                        query += " group by A.idtipocal,A.idconplan,B.ctahaber,C.modoane,";
                        query += " C.modoaneref,C.tipane,C.ane,C.tipaneref,C.aneref ";
                        query += " having SUM(A.valor)>0 order by A.idconplan asc;";

                        SqlDataAdapter dahaber = new SqlDataAdapter();
                        dahaber.SelectCommand = new SqlCommand(query, conexion);
                        dahaber.Fill(dthaber);

                        foreach (DataRow row_dthaber in dthaber.Rows)
                        {
                            string ctahaber = row_dthaber["ctahaber"].ToString();
                            string idconplan = row_dthaber["idconplan"].ToString();
                            string idtipocal = row_dthaber["idtipocal"].ToString();
                            string modoane = row_dthaber["modoane"].ToString();
                            string modoaneref = row_dthaber["modoaneref"].ToString();

                            string tipanecta = row_dthaber["tipane"].ToString();
                            string anecta = row_dthaber["ane"].ToString();
                            string tipanerefcta = row_dthaber["tipaneref"].ToString();
                            string anerefcta = row_dthaber["aneref"].ToString();

                            detallado = cuencon.ui_getDatosCuenCon(idcia, ctahaber, "DETALLADO");

                            if (detallado.Equals("S"))
                            {
                                DataTable dtper = new DataTable();
                                query = "  select  A.idperplan,P.codaux,P.nrodoc,D.tipaneha,D.aneha,D.tipanerefha,D.anerefha,";
                                query += " SUM(A.valor) as valor from conbol A ";
                                query += " inner join calplan E on A.idtipocal=E.idtipocal and A.idcia=E.idcia ";
                                query += " and A.messem=E.messem and A.anio=E.anio and A.idtipoper=E.idtipoper ";
                                query += " inner join detconplan D on A.idtipocal=D.idtipocal and A.idcia=D.idcia ";
                                query += " and A.idtipoper=D.idtipoper and A.idtipoplan=D.idtipoplan ";
                                query += " and A.idconplan=D.idconplan ";
                                query += " inner join perplan P on A.idperplan=P.idperplan and A.idcia=P.idcia ";
                                query += " where E.aniopdt='" + @anio + "' and E.mespdt='" + @mes + "'  ";
                                query += " and A.idcia='" + @idcia + "' and A.idtipoper='" + @idtipoper + "' ";
                                query += " and A.idtipoplan='" + @idtipoplan + "' and A.idconplan='" + @idconplan + "' ";
                                query += " and A.idtipocal='" + @idtipocal + "' ";
                                query += " group by A.idperplan,P.codaux,P.nrodoc,D.tipaneha,D.aneha,D.tipanerefha,D.anerefha ";
                                query += " having valor>0 order by A.idconplan asc;";
                                SqlDataAdapter daPer = new SqlDataAdapter();
                                daPer.SelectCommand = new SqlCommand(query, conexion);
                                daPer.Fill(dtper);
                                foreach (DataRow row_dtper in dtper.Rows)
                                {
                                    string anexo = string.Empty;
                                    string tipoanexo = string.Empty;
                                    string anexoref = string.Empty;
                                    string tipoanexoref = string.Empty;

                                    string tipanecon = row_dtper["tipaneha"].ToString();
                                    string anecon = row_dtper["aneha"].ToString();
                                    string tipanerefcon = row_dtper["tipanerefha"].ToString();
                                    string anerefcon = row_dtper["anerefha"].ToString();

                                    if (modoane != "XX")
                                    {
                                        if (modoane.Equals("DO"))
                                        {
                                            tipoanexo = "T";
                                            anexo = row_dtper["nrodoc"].ToString();
                                        }

                                        if (modoane.Equals("CO"))
                                        {
                                            tipoanexo = "T";
                                            anexo = row_dtper["idperplan"].ToString();
                                        }

                                        if (modoane.Equals("CA"))
                                        {
                                            tipoanexo = "T";
                                            anexo = row_dtper["codaux"].ToString();
                                        }

                                        if (modoane.Equals("CU"))
                                        {
                                            tipoanexo = tipanecta;
                                            anexo = anecta;
                                        }

                                        if (modoane.Equals("MO"))
                                        {
                                            tipoanexo = tipanecon;
                                            anexo = anecon;
                                        }
                                    }
                                    else
                                    {
                                        tipoanexo = "";
                                        anexo = "";
                                    }

                                    if (modoaneref != "XX")
                                    {
                                        if (modoaneref.Equals("DO"))
                                        {
                                            tipoanexoref = "T";
                                            anexoref = row_dtper["nrodoc"].ToString();
                                        }

                                        if (modoaneref.Equals("CO"))
                                        {
                                            tipoanexoref = "T";
                                            anexoref = row_dtper["idperplan"].ToString();
                                        }

                                        if (modoaneref.Equals("CA"))
                                        {
                                            tipoanexoref = "T";
                                            anexoref = row_dtper["codaux"].ToString();
                                        }

                                        if (modoaneref.Equals("CU"))
                                        {
                                            tipoanexoref = tipanerefcta;
                                            anexoref = anerefcta;
                                        }
                                        if (modoaneref.Equals("MO"))
                                        {
                                            tipoanexoref = tipanerefcon;
                                            anexoref = anerefcon;
                                        }
                                    }
                                    else
                                    {
                                        tipoanexoref = "";
                                        anexoref = "";
                                    }

                                    float importe = float.Parse(row_dtper["valor"].ToString());
                                    string numdoc = nombremes + " - " + anio;
                                    float mnimpor = importe;
                                    float usimpor = importe / tc;
                                    secue++;
                                    secuetmp = secue.ToString();
                                    csecue = funciones.replicateCadena("0", 4 - secuetmp.Trim().Length) + secuetmp.Trim();
                                    query = "Insert into " + tabladetdbf + "(dsubdia, dcompro, dsecue, dfeccom,";
                                    query += "dcuenta,dcodane,dcencos,dcodmon,ddh,dimport,";
                                    query += "dtipdoc,dnumdoc,dflag,ddate,";
                                    query += "dxglosa,dusimpor,dmnimpor,dfeccom2,dvanexo,dtipcam,debe,haber,";
                                    query += " dcodane2,dvanexo2,dfecdoc2,dfecdoc) ";
                                    query += " VALUES('" + @subdia + "','" + @compro + "','" + @csecue + "','" + @cfecha + "',";
                                    query += " '" + @ctahaber + "','" + @anexo + "','','MN','H','" + @importe + "',";
                                    query += " 'PL','" + @numdoc + "','S','" + @fecha + "', ";
                                    query += " '" + @glosa + "','" + @usimpor + "','" + @mnimpor + "','" + @fecha + "', ";
                                    query += " '" + @tipoanexo + "','" + @tc + "','0','" + @importe + "',";
                                    query += " '" + @anexoref + "','" + @tipoanexoref + "','" + @fecha + "','" + @cfecha + "')";
                                    ctotalhaber = ctotalhaber + importe;
                                    OleDbCommand sqlOleDbCommandDet = new OleDbCommand(query, cn);
                                    cn.Open();
                                    sqlOleDbCommandDet.ExecuteNonQuery();
                                    cn.Close();
                                }
                            }
                            else
                            {
                                float importe = float.Parse(row_dthaber["valor"].ToString());
                                string numdoc = nombremes + " - " + anio;
                                float mnimpor = importe;
                                float usimpor = importe / tc;
                                secue++;
                                secuetmp = secue.ToString();
                                csecue = funciones.replicateCadena("0", 4 - secuetmp.Trim().Length) + secuetmp.Trim();

                                query = "";
                                query += "Insert into " + tabladetdbf + "(dsubdia, dcompro, dsecue, dfeccom,";
                                query += "dcuenta,dcodane,dcencos,dcodmon,ddh,dimport,";
                                query += "dtipdoc,dnumdoc,dflag,ddate,";
                                query += "dxglosa,dusimpor,dmnimpor,dfeccom2,dvanexo,dtipcam,debe,haber,";
                                query += " dcodane2,dvanexo2,dfecdoc2,dfecdoc) ";
                                query += " VALUES('" + @subdia + "','" + @compro + "','" + @csecue + "','" + @cfecha + "',";
                                query += " '" + @ctahaber + "','" + @anecta + "','','MN','H','" + @importe + "',";
                                query += " 'PL','" + @numdoc + "','S','" + @fecha + "', ";
                                query += " '" + @glosa + "','" + @usimpor + "','" + @mnimpor + "','" + @fecha + "', ";
                                query += " '" + @tipanecta + "','" + @tc + "','0','" + @importe + "','" + @anerefcta + "'";
                                query += " ,'" + @tipanerefcta + "','" + @fecha + "','" + @cfecha + "')";
                                ctotalhaber = ctotalhaber + importe;
                                OleDbCommand sqlOleDbCommandDet = new OleDbCommand(query, cn);
                                cn.Open();
                                sqlOleDbCommandDet.ExecuteNonQuery();
                                cn.Close();
                            }
                        }
                        /*
                        query = "";
                        query = "Insert into " + tabladetdbf + "(dsecue, dsubdia, dcompro, dfeccom,";
                        query = query + "dcuenta,dcodane,dcencos,dcodmon,ddh,dimport,";
                        query = query + "dtipdoc,dnumdoc,dflag,ddate,";
                        query = query + "dxglosa,dusimpor,dmnimpor,dfeccom2,dvanexo,dtipcam,debe,haber,";
                        query = query + " dcodane2,dvanexo2,dfecdoc2,dfecdoc) ";
                        query += "select '' AS dsecue, x.dsubdia, x.dcompro, x.dfeccom, ";
                        query += "x.dcuenta,x.dcodane,x.dcencos,x.dcodmon,x.ddh,sum(x.dimport) as dimport, ";
                        query += "x.dtipdoc,x.dnumdoc,x.dflag,x.ddate, ";
                        query += "x.dxglosa,sum(x.dusimpor) as dusimpor,sum(x.dmnimpor) as dmnimpor,x.dfeccom2,x.dvanexo,x.dtipcam,sum(x.debe) as debe,sum(x.haber) as haber,x.dcodane2,x.dvanexo2,x.dfecdoc2,x.dfecdoc ";
                        query += "from _" + tabladetdbf + " x ";
                        query += "group by x.dsubdia, x.dcompro, x.dfeccom, ";
                        query += "x.dcuenta,x.dcodane,x.dcencos,x.dcodmon,x.ddh, ";
                        query += "x.dtipdoc,x.dnumdoc,x.dflag,x.ddate, ";
                        query += "x.dxglosa,x.dfeccom2,x.dvanexo,x.dtipcam,x.dcodane2,x.dvanexo2,x.dfecdoc2,x.dfecdoc ";
                        OleDbCommand sqlOleDbCommandDet3 = new OleDbCommand(query, cn);
                        cn.Open();
                        sqlOleDbCommandDet3.ExecuteNonQuery();
                        cn.Close();*/

                        //////////////////////////////////////////////////////////////////////////////////////////////////////
                        ////AGREGA REGISTRO A LA TABLA DE CABECERA ///////////////////////////////////////////////////////////
                        /////////////////////////////////////////////////////////////////////////////////////////////////////
                        string chora = DateTime.Now.ToString("hh:mm:ss");
                        query = " Insert into " + tablacabdbf + "(csubdia,ccompro,cfeccom,ccodmon,csitua";
                        query += " ,ctipcam,cglosa,ctotal,ctipo,cflag,cdate,chora,cuser,cfeccom2,copcion) ";
                        query += " VALUES('" + @subdia + "','" + @compro + "',";
                        query += " '" + @cfecha + "','MN','F',";
                        query += " '" + @tc + "','" + @glosa + "','" + @ctotaldebe + "',";
                        query += " 'V','S','" + @fecha + "','" + @chora + "','PLAN','" + @fecha + "','S');";

                        OleDbCommand sqlOleDbCommand = new OleDbCommand(query, cn);
                        cn.Open();
                        sqlOleDbCommand.ExecuteNonQuery();
                        cn.Close();

                        //File.Delete(ruta + "\\_" + tabladetdbf + ".DBF");

                        MessageBox.Show("Archivos generados con éxito", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        ui_viewasiento ui_viewasiento = new ui_viewasiento();
                        ui_viewasiento._namefilecab = tablacabdbf;
                        ui_viewasiento._namefiledet = tabladetdbf;
                        ui_viewasiento._ruta = ruta;
                        ui_viewasiento._FormPadre = this;
                        ui_viewasiento.Activate();
                        ui_viewasiento.BringToFront();
                        ui_viewasiento.ShowDialog();
                        ui_viewasiento.Dispose();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtAnio_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                e.Handled = true;
                txtSubdia.Focus();
            }
        }

        private void txtSubdia_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                e.Handled = true;
                txtCompro.Focus();
            }
        }

        private void txtCompro_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                e.Handled = true;
                txtFecha.Focus();
            }
        }

        private void txtFecha_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                if (UtileriasFechas.IsDate(txtFecha.Text))
                {
                    e.Handled = true;
                    txtGlosa.Focus();

                }
                else
                {
                    MessageBox.Show("Fecha no válida", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    e.Handled = true;
                    txtFecha.Focus();
                }
            }
        }

        private void btnDir_Click(object sender, EventArgs e)
        {
            string rutaFile = string.Empty;
            FolderBrowserDialog dialogoRuta = new FolderBrowserDialog();
            if (dialogoRuta.ShowDialog() == DialogResult.OK)
            {
                rutaFile = dialogoRuta.SelectedPath;
            }
            if (rutaFile != string.Empty)
            {
                txtDir.Text = rutaFile;
            }
        }

        private void txtGlosa_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                e.Handled = true;
                txtTC.Focus();
            }
        }

        private void btnGrabar_Click(object sender, EventArgs e)
        {
            try
            {
                GlobalVariables globalvariables = new GlobalVariables();
                Funciones funciones = new Funciones();
                CenCon cencon = new CenCon();
                string idcia = globalvariables.getValorCia();
                string idcencos = funciones.getValorComboBox(cmbCentroCosto, 2);
                float porcentaje = float.Parse(txtPorcentaje.Text);
                string valorValida = "G";


                if (cmbCentroCosto.Text == string.Empty && valorValida == "G")
                {
                    valorValida = "B";
                    MessageBox.Show("No ha seleccionado Centro de Costo", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    cmbCentroCosto.Focus();
                }

                if (cmbCentroCosto.Text != string.Empty && valorValida == "G")
                {
                    string squery = "SELECT idcencos as clave,descencos as descripcion FROM cencos WHERE idcencos='" + @idcencos + "';";
                    string resultado = funciones.verificaItemComboBox(squery, cmbCentroCosto);

                    if (resultado.Trim() == string.Empty)
                    {
                        valorValida = "B";
                        MessageBox.Show("Dato incorrecto en Centro de Costo", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        cmbCentroCosto.Focus();
                    }
                }

                if (valorValida.Equals("G"))
                {
                    cencon.actualizarCenCon(idcia, idcencos, porcentaje);
                    ui_listaCenCon();
                    ui_limpiarCenCon();
                }
            }
            catch (FormatException ex)
            {
                MessageBox.Show(ex.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            CenCon cencon = new CenCon();
            Int32 selectedCellCount = dgvData.GetCellCount(DataGridViewElementStates.Selected);
            if (selectedCellCount > 0)
            {
                string idcencos = dgvData.Rows[dgvData.SelectedCells[1].RowIndex].Cells[0].Value.ToString();
                string descencos = dgvData.Rows[dgvData.SelectedCells[1].RowIndex].Cells[1].Value.ToString();
                string idcia = dgvData.Rows[dgvData.SelectedCells[1].RowIndex].Cells[3].Value.ToString();
                DialogResult resultado = MessageBox.Show("¿Desea eliminar el porcentaje asignado al Centro de Costo '" + @descencos + "'?",
                "Consulta Importante",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);
                if (resultado == DialogResult.Yes)
                {
                    cencon.eliminarCenCon(idcia, idcencos);
                    ui_listaCenCon();
                }
            }
            else
            {
                MessageBox.Show("No ha seleccionado registro a Eliminar", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void cmbTipoPlan_SelectedIndexChanged(object sender, EventArgs e)
        {
            ui_listaPer();
        }

        private void cmbTipoTrabajador_SelectedIndexChanged(object sender, EventArgs e)
        {
            ui_listaPer();
        }

        private void btnAsignar_Click(object sender, EventArgs e)
        {
            Int32 selectedCellCount = dgvPersonal.GetCellCount(DataGridViewElementStates.Selected);
            if (selectedCellCount > 0)
            {
                string idperplan = dgvPersonal.Rows[dgvPersonal.SelectedCells[1].RowIndex].Cells[0].Value.ToString();
                string nombre = dgvPersonal.Rows[dgvPersonal.SelectedCells[1].RowIndex].Cells[1].Value.ToString();
                GlobalVariables gv = new GlobalVariables();
                ui_cencosper ui_cencosper = new ui_cencosper();
                ui_cencosper._FormPadre = this;
                ui_cencosper._idcia = gv.getValorCia();
                ui_cencosper._idperplan = idperplan;
                ui_cencosper._nombre = nombre;
                ui_cencosper.Activate();
                ui_cencosper.BringToFront();
                ui_cencosper.ShowDialog();
                ui_cencosper.Dispose();
            }
            else
            {
                MessageBox.Show("No ha seleccionado registro", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }

        }

        private void cmbListar_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.ui_listaPer();
        }
    }
}