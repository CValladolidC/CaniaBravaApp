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
using System.Collections;
using System.Diagnostics;
using System.IO;

namespace CaniaBrava
{
    public partial class ui_trabajadores : Form
    {
        //Oliver Cruz Tuanama
        string bd_prov = ConfigurationManager.AppSettings.Get("BD_PROV");
        string query = "";
        GlobalVariables variables = new GlobalVariables();
        Funciones funciones = new Funciones();
        SqlConnection conexion = new SqlConnection();

        private ui_updtrabajadores form = null;

        private ui_updtrabajadores FormInstance
        {
            get
            {
                if (form == null)
                {
                    form = new ui_updtrabajadores();
                    form.Disposed += new EventHandler(form_Disposed);
                }

                return form;
            }
        }

        void form_Disposed(object sender, EventArgs e) { form = null; }

        public ui_trabajadores()
        {
            InitializeComponent();

            if (funciones.VersionAssembly()) Application.ExitThread();
        }

        private void ui_trabajadores_Load(object sender, EventArgs e)
        {
            query = "SELECT idtipoper as clave,destipoper as descripcion FROM tipoper order by 1 asc;";
            MaesGen maesgen = new MaesGen();
            funciones.listaComboBox(query, cmbCategoria, "X");
            maesgen.listaDetMaesGen("008", cmbSeccion, "X");
            cmbSeccion.Text = "X   TODOS";
            cmbCategoria.Text = "X   TODOS";
            ui_ListaPerPlan();
        }

        private void ui_ListaPerPlan()
        {
            string idcia = variables.getValorCia();
            string idtipoper = funciones.getValorComboBox(cmbCategoria, 2);
            string seccion = funciones.getValorComboBox(cmbSeccion, 8);
            string cadenaidtipoper = string.Empty;
            string cadenasituacion = string.Empty;
            string cadenaseccion = string.Empty;

            if (idtipoper != "X") cadenaidtipoper = " and A.idtipoper='" + @idtipoper + "' ";
            if (seccion != "X   TODO") cadenaseccion = " and A.seccion='" + @seccion + "' ";

            query = " Select A.idperplan as [Cod.Int.],(rtrim(A.apepat)+' '+rtrim(A.apemat)+', '+rtrim(A.nombres)) as [Apellidos y Nombres],";
            query += "H.abrevia as [Gerencia],D.desmaesgen as [Área],E.deslabper as [Ocupación],";
            query += "rtrim(B.destipoper) as [Nomina],C.Parm1maesgen as [T. Doc],A.nrodoc as [Nro. Doc.],";
            query += "G.fechaini as [F.Ini.Labores],G.fechafin as [F.Fin/Cese],A.fecharegistro [F.Actualizacion] from perplan A (NOLOCK) ";
            query += "left join tipoper (NOLOCK) B on A.idtipoper=B.idtipoper ";
            query += "left join maesgen (NOLOCK) C on C.idmaesgen='002' and A.tipdoc=C.clavemaesgen ";
            query += "left join maesgen (NOLOCK) D on D.idmaesgen='008' and A.seccion=D.clavemaesgen ";
            query += "left join labper (NOLOCK) E on A.idcia=E.idcia and A.idlabper=E.idlabper and A.idtipoper=E.idtipoper ";
            query += "left join view_perlab F on A.idcia=F.idcia collate Modern_Spanish_CI_AI and A.idperplan=F.idperplan collate Modern_Spanish_CI_AI ";
            query += "left join perlab (NOLOCK) G on F.idcia=G.idcia and F.idperplan=G.idperplan and F.idperlab=G.idperlab ";
            query += "left join maesgen (NOLOCK) H on H.idmaesgen='040' and H.clavemaesgen=A.codaux and H.parm1maesgen=A.idcia ";
            //query += "where A.idcia='" + @idcia + "'" + @cadenaidtipoper + @cadenasituacion + @cadenaseccion + " order by a.idperplan asc;";
            query += "where alta_tregistro = 1 " + @cadenaidtipoper + @cadenasituacion + @cadenaseccion + " order by a.idperplan asc;";

            loadqueryDatos(query);
        }

        private void loadqueryDatos(string query)
        {
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();

            try
            {
                using (SqlDataAdapter myDataAdapter = new SqlDataAdapter(query, conexion))
                {
                    DataSet myDataSet = new DataSet();
                    myDataAdapter.Fill(myDataSet, "tblPerPlan");
                    funciones.formatearDataGridView(dgvdetalle);

                    dgvdetalle.DataSource = myDataSet.Tables["tblPerPlan"];

                    dgvdetalle.Columns[0].Width = 70;
                    dgvdetalle.Columns[1].Width = 250;
                    dgvdetalle.Columns[2].Width = 160;
                    dgvdetalle.Columns[3].Width = 160;
                    dgvdetalle.Columns[4].Width = 120;
                    dgvdetalle.Columns[5].Width = 120;
                    dgvdetalle.Columns[6].Width = 70;
                    dgvdetalle.Columns[7].Width = 70;
                    dgvdetalle.Columns[8].Width = 75;
                    dgvdetalle.Columns[9].Width = 75;

                    dgvdetalle.AllowUserToResizeRows = false;
                    dgvdetalle.AllowUserToResizeColumns = false;
                    foreach (DataGridViewColumn column in dgvdetalle.Columns)
                    {
                        column.SortMode = DataGridViewColumnSortMode.NotSortable;
                    }
                }

                ui_calculaNumRegistros();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }

            conexion.Close();
        }

        public void ui_calculaNumRegistros()
        {
            PerPlan perplan = new PerPlan();

            //txtRegTotal.Text = string.Empty;
            //string numreg = perplan.getNumeroRegistrosPerPlan(variables.getValorCia());
            //txtRegTotal.Text = funciones.replicateCadena(" ", 15 - numreg.Trim().Length) + numreg.Trim() + funciones.replicateCadena(" ", 20) + "Registrados";
            string numregencontrados = Convert.ToString(dgvdetalle.RowCount);
            txtRegEncontrados.Text = funciones.replicateCadena(" ", 15 - numregencontrados.Trim().Length) + numregencontrados.Trim() + funciones.replicateCadena(" ", 20) + " Registrados";
        }

        private void btnSalir_Click(object sender, EventArgs e) { Close(); }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            ui_updtrabajadores ui_detalle = this.FormInstance;
            ui_detalle._FormPadre = this;
            ui_detalle.ui_ActualizaComboBox();
            ui_detalle.newPerPlan();
            ui_detalle.Activate();
            ui_detalle.BringToFront();
            ui_detalle.ShowDialog();
            ui_detalle.Dispose();
        }

        private void btnActualizar_Click(object sender, EventArgs e) { ui_ListaPerPlan(); }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            GlobalVariables variables = new GlobalVariables();
            string idcia = variables.getValorCia();
            Int32 selectedCellCount = dgvdetalle.GetCellCount(DataGridViewElementStates.Selected);
            if (selectedCellCount > 0)
            {
                string idperplan = dgvdetalle.Rows[dgvdetalle.SelectedCells[1].RowIndex].Cells[0].Value.ToString();
                //string idcia = dgvdetalle.Rows[dgvdetalle.SelectedCells[1].RowIndex].Cells[11].Value.ToString();

                ui_updtrabajadores ui_detalle = this.FormInstance;
                ui_detalle._FormPadre = this;
                ui_detalle.ui_ActualizaComboBox();
                ui_detalle.Activate();
                ui_detalle.ui_loadPerPlan(idcia, idperplan);
                //ui_detalle.ui_listaDerechohabientes(idcia, idperplan);
                ui_detalle.ui_listaPeriodosLaborales(idcia, idperplan);
                //ui_detalle.ui_listaFonPen(idcia, idperplan);
                //ui_detalle.ui_listaCenPer(idcia, idperplan);
                ui_detalle.BringToFront();
                ui_detalle.ShowDialog();
                ui_detalle.Dispose();
            }
            else
            {
                MessageBox.Show("No ha seleccionado registro a Editar", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            Int32 selectedCellCount =
            dgvdetalle.GetCellCount(DataGridViewElementStates.Selected);
            if (selectedCellCount > 0)
            {
                string idperplan = dgvdetalle.Rows[dgvdetalle.SelectedCells[1].RowIndex].Cells[0].Value.ToString();
                string idcia = dgvdetalle.Rows[dgvdetalle.SelectedCells[1].RowIndex].Cells[11].Value.ToString();
                string nombre = dgvdetalle.Rows[dgvdetalle.SelectedCells[1].RowIndex].Cells[5].Value.ToString();
                DialogResult resultado = MessageBox.Show("¿Desea eliminar el Trabajador " + @nombre + "?",
                "Consulta Importante",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);
                if (resultado == DialogResult.Yes)
                {
                    PerPlan perplan = new PerPlan();
                    perplan.eliminarPerPlan(idcia, idperplan);
                    this.ui_ListaPerPlan();
                }
            }
            else
            {
                MessageBox.Show("No ha seleccionado registro a Eliminar", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void cmbCategoria_Click(object sender, EventArgs e) { }

        private void cmbSituacion_Click(object sender, EventArgs e) { }

        private void cmbSeccion_Click(object sender, EventArgs e) { }

        private void cmbCategoria_SelectedIndexChanged(object sender, EventArgs e) { ui_ListaPerPlan(); }

        private void cmbSituacion_SelectedIndexChanged(object sender, EventArgs e) { ui_ListaPerPlan(); }

        private void cmbSeccion_SelectedIndexChanged(object sender, EventArgs e) { ui_ListaPerPlan(); }

        private void radioButtonCodigoInterno_CheckedChanged(object sender, EventArgs e)
        {
            txtBuscar.Text = string.Empty;
            txtBuscar.Focus();
        }

        private void radioButtonNroDoc_CheckedChanged(object sender, EventArgs e)
        {
            txtBuscar.Text = string.Empty;
            txtBuscar.Focus();
        }

        private void radioButtonNombre_CheckedChanged(object sender, EventArgs e)
        {
            txtBuscar.Text = string.Empty;
            txtBuscar.Focus();
        }

        private void btnTodos_Click(object sender, EventArgs e)
        {
            cmbSeccion.Text = "X   TODOS";
            cmbCategoria.Text = "X   TODOS";
            txtBuscar.Clear();
            txtBuscar.Focus();
            ui_ListaPerPlan();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            string buscar = txtBuscar.Text.Trim();

            if (buscar != string.Empty)
            {
                PerPlan perplan = new PerPlan();

                cmbSeccion.Text = "X   TODOS";
                cmbCategoria.Text = "X   TODOS";

                txtRegEncontrados.Text = string.Empty;
                string idcia = variables.getValorCia();
                string cadenaBusqueda = string.Empty;

                if (radioButtonCodigoInterno.Checked && txtBuscar.Text.Trim() != string.Empty)
                {
                    cadenaBusqueda = " and A.idperplan like '%" + @buscar + "' ";
                }
                else
                {
                    if (radioButtonNombre.Checked && txtBuscar.Text.Trim() != string.Empty)
                    {
                        cadenaBusqueda = " and rtrim(A.apepat)+' '+rtrim(A.apemat)+', '+rtrim(A.nombres) like '%" + @buscar + "%' ";
                    }
                    else
                    {
                        cadenaBusqueda = " and A.nrodoc='" + @buscar + "' ";
                    }
                }

                query = " Select A.idperplan as [Cod.Int.],(rtrim(A.apepat)+' '+rtrim(A.apemat)+', '+rtrim(A.nombres)) as [Apellidos y Nombres],";
                query += "H.desmaesgen as [Gerencia],D.desmaesgen as [Área],E.deslabper as [Ocupación],";
                query += "rtrim(B.destipoper) as [Nomina],C.Parm1maesgen as [T. Doc],A.nrodoc as [Nro. Doc.],";
                query += "G.fechaini as [F.Ini.Labores],G.fechafin as [F.Fin/Cese] from perplan A (NOLOCK) ";
                query += "left join tipoper (NOLOCK) B on A.idtipoper=B.idtipoper ";
                query += "left join maesgen (NOLOCK) C on C.idmaesgen='002' and A.tipdoc=C.clavemaesgen ";
                query += "left join maesgen (NOLOCK) D on D.idmaesgen='008' and A.seccion=D.clavemaesgen ";
                query += "left join labper (NOLOCK) E on A.idcia=E.idcia and A.idlabper=E.idlabper and A.idtipoper=E.idtipoper ";
                query += "left join view_perlab F on A.idcia=F.idcia collate Modern_Spanish_CI_AI and A.idperplan=F.idperplan collate Modern_Spanish_CI_AI ";
                query += "left join perlab (NOLOCK) G on F.idcia=G.idcia and F.idperplan=G.idperplan and F.idperlab=G.idperlab ";
                query += "left join maesgen (NOLOCK) H on H.idmaesgen='040' and H.clavemaesgen=A.codaux and H.parm1maesgen=A.idcia ";
                query += "where 1=1 " + @cadenaBusqueda + " order by a.idperplan asc;";

                loadqueryDatos(query);
            }
        }

        private void btnPdf_Click(object sender, EventArgs e)
        {
            if (dgvdetalle.RowCount > 0)
            {
                Exporta exporta = new Exporta();
                exporta.Pdf_FromDataGridView(dgvdetalle, 1);
            }
            else
            {
                MessageBox.Show("No se puede exportar a PDF", "Aviso Importante", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            Exporta exporta = new Exporta();
            foreach (DataGridViewRow row in dgvdetalle.Rows)
            {
                foreach (DataGridViewCell cell in row.Cells)
                {
                    if (cell.ColumnIndex == 0 || cell.ColumnIndex == 7)
                    {
                        cell.Value = "'" + cell.Value.ToString();
                    }
                }
            }
            exporta.Excel_FromDataGridView(dgvdetalle);
            ui_ListaPerPlan();
        }

        private void toolstripform_ItemClicked(object sender, ToolStripItemClickedEventArgs e) { }

        private void groupBox2_Enter(object sender, EventArgs e) { }

        private void groupBox1_Enter(object sender, EventArgs e) { }

        private void btnTRegistroalta_Click(object sender, EventArgs e)
        {
            string rucemp = variables.getValorRucCia();
            string idcia = variables.getValorCia();
            string rutaFile = string.Empty;

            FolderBrowserDialog dialogoRuta = new FolderBrowserDialog();
            if (dialogoRuta.ShowDialog() == DialogResult.OK)
            {
                rutaFile = dialogoRuta.SelectedPath;
            }

            if (rutaFile != string.Empty)
            {
                /*Ini Variables Globales*/
                string tipdoc = string.Empty;
                string nrodoc = string.Empty;
                string paisemi = string.Empty;
                /*Fin Variables Globales*/

                string filename = "";
                DataTable dtTregistro = null;
                SqlDataAdapter da_est = new SqlDataAdapter();

                /******************************************************/
                //GENERA ARCHIVO *.ide
                /******************************************************/
                query = "CALL sp_tregistroide('" + idcia + "');";
                da_est.SelectCommand = new SqlCommand(query, conexion);
                dtTregistro = new DataTable();
                da_est.Fill(dtTregistro);

                filename = rutaFile + "/RP_" + rucemp + ".ide";
                StreamWriter archivo_ide = File.CreateText(filename);
                archivo_ide.Close();
                foreach (DataRow row_dtTregistro in dtTregistro.Rows)
                {
                    tipdoc = row_dtTregistro["tipdoc"].ToString();
                    nrodoc = row_dtTregistro["nrodoc"].ToString();
                    paisemi = row_dtTregistro["paisemi"].ToString();
                    string fecnac = row_dtTregistro["fecnac"].ToString();
                    string apepat = row_dtTregistro["apepat"].ToString();
                    string apemat = row_dtTregistro["apemat"].ToString();
                    string nombres = row_dtTregistro["nombres"].ToString();
                    string sexo = row_dtTregistro["sexo"].ToString();
                    string nacion = row_dtTregistro["nacion"].ToString();
                    string disnac = row_dtTregistro["disnac"].ToString();
                    string telfijo = row_dtTregistro["telfijo"].ToString();
                    string email = row_dtTregistro["email"].ToString();
                    string tipvia = row_dtTregistro["tipvia"].ToString();
                    string nomvia = row_dtTregistro["nomvia"].ToString();
                    string nrovia = row_dtTregistro["nrovia"].ToString();
                    string deparvia = row_dtTregistro["deparvia"].ToString();
                    string intvia = row_dtTregistro["intvia"].ToString();
                    string manzavia = row_dtTregistro["manzavia"].ToString();
                    string lotevia = row_dtTregistro["lotevia"].ToString();
                    string kmvia = row_dtTregistro["kmvia"].ToString();
                    string block = row_dtTregistro["block"].ToString();
                    string etapa = row_dtTregistro["etapa"].ToString();
                    string tipzona = row_dtTregistro["tipzona"].ToString();
                    string nomzona = row_dtTregistro["nomzona"].ToString();
                    string refzona = row_dtTregistro["refzona"].ToString();
                    string ubigeo = row_dtTregistro["ubigeo"].ToString();
                    string tipvia2 = row_dtTregistro["tipvia2"].ToString();
                    string nomvia2 = row_dtTregistro["nomvia2"].ToString();
                    string nrovia2 = row_dtTregistro["nrovia2"].ToString();
                    string deparvia2 = row_dtTregistro["deparvia2"].ToString();
                    string intvia2 = row_dtTregistro["intvia2"].ToString();
                    string manzavia2 = row_dtTregistro["manzavia2"].ToString();
                    string lotevia2 = row_dtTregistro["lotevia2"].ToString();
                    string kmvia2 = row_dtTregistro["kmvia2"].ToString();
                    string block2 = row_dtTregistro["block2"].ToString();
                    string etapa2 = row_dtTregistro["etapa2"].ToString();
                    string tipzona2 = row_dtTregistro["tipzona2"].ToString();
                    string nomzona2 = row_dtTregistro["nomzona2"].ToString();
                    string refzona2 = row_dtTregistro["refzona2"].ToString();
                    string ubigeo2 = row_dtTregistro["ubigeo2"].ToString();
                    string essalud = row_dtTregistro["essalud"].ToString();

                    OpeIO opeIO = new OpeIO(filename);
                    opeIO.WriteNWL(tipdoc + "|" + nrodoc + "|" + paisemi + "|" + fecnac + "|" + apepat + "|" + apemat + "|" + nombres
                        + "|" + sexo + "|" + nacion + "|" + disnac + "|" + telfijo + "|" + email + "|" + tipvia + "|" + nomvia + "|" + nrovia
                        + "|" + deparvia + "|" + intvia + "|" + manzavia + "|" + lotevia + "|" + kmvia + "|" + block + "|" + etapa + "|" + tipzona
                        + "|" + nomzona + "|" + refzona + "|" + ubigeo + "|" + tipvia2 + "|" + nomvia2 + "|" + nrovia2 + "|" + deparvia2 
                        + "|" + intvia2 + "|" + manzavia2 + "|" + lotevia2 + "|" + kmvia2 + "|" + block2 + "|" + etapa2 + "|" + tipzona2
                        + "|" + nomzona2 + "|" + refzona2 + "|" + ubigeo2 + "|" + essalud + "|");
                }

                /******************************************************/
                //GENERA ARCHIVO *.tra
                /******************************************************/
                query = "CALL sp_tregistrotra('" + idcia + "');";
                da_est.SelectCommand = new SqlCommand(query, conexion);
                dtTregistro = new DataTable();
                da_est.Fill(dtTregistro);

                filename = rutaFile + "/RP_" + rucemp + ".tra";
                StreamWriter archivo_tra = File.CreateText(filename);
                archivo_tra.Close();
                foreach (DataRow row_dtTregistro in dtTregistro.Rows)
                {
                    tipdoc = row_dtTregistro["tipdoc"].ToString();
                    nrodoc = row_dtTregistro["nrodoc"].ToString();
                    paisemi = row_dtTregistro["paisemi"].ToString();
                    string reglab = row_dtTregistro["reglab"].ToString();
                    string nivedu = row_dtTregistro["nivedu"].ToString();
                    string ocurpts = row_dtTregistro["ocurpts"].ToString();
                    string discapa = row_dtTregistro["discapa"].ToString();
                    string cuspp = row_dtTregistro["cuspp"].ToString();
                    string sctrpension = row_dtTregistro["sctrpension"].ToString();
                    string contrab = row_dtTregistro["contrab"].ToString();
                    string regalterna = row_dtTregistro["regalterna"].ToString();
                    string trabmax = row_dtTregistro["trabmax"].ToString();
                    string trabnoc = row_dtTregistro["trabnoc"].ToString();
                    string sindica = row_dtTregistro["sindica"].ToString();
                    string pering = row_dtTregistro["pering"].ToString();
                    string monrem = row_dtTregistro["monrem"].ToString();
                    string situatrab = row_dtTregistro["situatrab"].ToString();
                    string exoquicat = row_dtTregistro["exoquicat"].ToString();
                    string sitesp = row_dtTregistro["sitesp"].ToString();
                    string tippag = row_dtTregistro["tippag"].ToString();
                    string pdt = row_dtTregistro["pdt"].ToString();
                    string apliconve = row_dtTregistro["apliconve"].ToString();
                    string ruc = row_dtTregistro["ruc"].ToString();

                    OpeIO opeIO = new OpeIO(filename);
                    opeIO.WriteNWL(tipdoc + "|" + nrodoc + "|" + paisemi + "|" + reglab + "|" + nivedu + "|" + ocurpts + "|" + discapa + "|" + cuspp 
                        + "|" + sctrpension + "|" + contrab + "|" + regalterna + "|" + trabmax + "|" + trabnoc + "|" + sindica + "|" + pering 
                        + "|" + monrem + "|" + situatrab + "|" + exoquicat + "|" + sitesp + "|" + tippag + "|" + pdt + "|" + apliconve + "|" + ruc + "|");
                }

                /******************************************************/
                //GENERA ARCHIVO *.per
                /******************************************************/
                query = "CALL sp_tregistroper('" + idcia + "');";
                da_est.SelectCommand = new SqlCommand(query, conexion);
                dtTregistro = new DataTable();
                da_est.Fill(dtTregistro);

                filename = rutaFile + "/RP_" + rucemp + ".per";
                StreamWriter archivo_per = File.CreateText(filename);
                archivo_per.Close();
                foreach (DataRow row_dtTregistro in dtTregistro.Rows)
                {
                    tipdoc = row_dtTregistro["tipdoc"].ToString();
                    nrodoc = row_dtTregistro["nrodoc"].ToString();
                    paisemi = row_dtTregistro["paisemi"].ToString();
                    string categ = row_dtTregistro["categ"].ToString();
                    string tipreg = row_dtTregistro["tipreg"].ToString();
                    string fechaini = row_dtTregistro["fechaini"].ToString();
                    string fechafin = row_dtTregistro["fechafin"].ToString();
                    string contrab = row_dtTregistro["indtipreg"].ToString();
                    string servpropios = row_dtTregistro["servpropios"].ToString();

                    OpeIO opeIO = new OpeIO(filename);
                    opeIO.WriteNWL(tipdoc + "|" + nrodoc + "|" + paisemi + "|" + categ + "|" + tipreg + "|" + fechaini + "|" + fechafin + "|" + contrab + "|" + servpropios + "|");
                }

                /******************************************************/
                //GENERA ARCHIVO *.est
                /******************************************************/
                query = "CALL sp_tregistroest('" + idcia + "');";
                da_est.SelectCommand = new SqlCommand(query, conexion);
                dtTregistro = new DataTable();
                da_est.Fill(dtTregistro);

                filename = rutaFile + "/RP_" + rucemp + ".est";
                StreamWriter archivo_est = File.CreateText(filename);
                archivo_est.Close();
                foreach (DataRow row_dtTregistro in dtTregistro.Rows)
                {
                    tipdoc = row_dtTregistro["tipdoc"].ToString();
                    nrodoc = row_dtTregistro["nrodoc"].ToString();
                    paisemi = row_dtTregistro["paisemi"].ToString();
                    string rucempresa = row_dtTregistro["rucemp"].ToString();
                    string idestane = row_dtTregistro["idestane"].ToString();

                    OpeIO opeIO = new OpeIO(filename);
                    opeIO.WriteNWL(tipdoc + "|" + nrodoc + "|" + paisemi + "|" + rucempresa + "|" + idestane + "|");
                }

                MessageBox.Show("Exportacion de datos con exito");
            }
        }

        private void btnTRegistrobaja_Click(object sender, EventArgs e)
        {
            string rucemp = variables.getValorRucCia();
            string idcia = variables.getValorCia();
            string rutaFile = string.Empty;

            FolderBrowserDialog dialogoRuta = new FolderBrowserDialog();
            if (dialogoRuta.ShowDialog() == DialogResult.OK)
            {
                rutaFile = dialogoRuta.SelectedPath;
            }

            if (rutaFile != string.Empty)
            {
                /*Ini Variables Globales*/
                string tipdoc = string.Empty;
                string nrodoc = string.Empty;
                string paisemi = string.Empty;
                /*Fin Variables Globales*/

                string filename = "";
                DataTable dtTregistro = null;
                SqlDataAdapter da_est = new SqlDataAdapter();

                /******************************************************/
                //GENERA ARCHIVO *.per
                /******************************************************/
                query = "CALL sp_tregistrobaja('" + idcia + "');";
                da_est.SelectCommand = new SqlCommand(query, conexion);
                dtTregistro = new DataTable();
                da_est.Fill(dtTregistro);

                filename = rutaFile + "/RP_" + rucemp + ".per";
                StreamWriter archivo_per = File.CreateText(filename);
                archivo_per.Close();
                foreach (DataRow row_dtTregistro in dtTregistro.Rows)
                {
                    tipdoc = row_dtTregistro["tipdoc"].ToString();
                    nrodoc = row_dtTregistro["nrodoc"].ToString();
                    paisemi = row_dtTregistro["paisemi"].ToString();
                    string categ = row_dtTregistro["categ"].ToString();
                    string tipreg = row_dtTregistro["tipreg"].ToString();
                    string fechaini = row_dtTregistro["fechaini"].ToString();
                    string fechafin = row_dtTregistro["fechafin"].ToString();
                    string contrab = row_dtTregistro["motivo"].ToString();
                    string servpropios = row_dtTregistro["servpropios"].ToString();

                    OpeIO opeIO = new OpeIO(filename);
                    opeIO.WriteNWL(tipdoc + "|" + nrodoc + "|" + paisemi + "|" + categ + "|" + tipreg + "|" + fechaini + "|" + fechafin + "|" + contrab + "|" + servpropios + "|");
                }

                MessageBox.Show("Exportacion de datos con exito");
            }
        }

        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            string buscar = txtBuscar.Text.Trim();

            if (buscar != string.Empty)
            {
                PerPlan perplan = new PerPlan();

                string idcia = variables.getValorCia();
                string idtipoper = funciones.getValorComboBox(cmbCategoria, 2);
                string seccion = funciones.getValorComboBox(cmbSeccion, 8);
                string cadenaidtipoper = string.Empty;
                string cadenasituacion = string.Empty;
                string cadenaseccion = string.Empty;

                if (idtipoper != "X") cadenaidtipoper = " and A.idtipoper='" + @idtipoper + "' ";
                if (seccion != "X   TODO") cadenaseccion = " and A.seccion='" + @seccion + "' ";
                string cadenaBusqueda = string.Empty;

                if (radioButtonCodigoInterno.Checked && txtBuscar.Text.Trim() != string.Empty)
                {
                    cadenaBusqueda = " and A.idperplan like '%" + @buscar + "' ";
                }
                else
                {
                    if (radioButtonNombre.Checked && txtBuscar.Text.Trim() != string.Empty)
                    {
                        cadenaBusqueda = " and rtrim(A.apepat)+' '+rtrim(A.apemat)+', '+rtrim(A.nombres) like '%" + @buscar + "%' ";
                    }
                    else
                    {
                        cadenaBusqueda = " and A.nrodoc like '%" + @buscar + "%' ";
                    }
                }

                query = " Select A.idperplan as [Cod.Int.],(rtrim(A.apepat)+' '+rtrim(A.apemat)+', '+rtrim(A.nombres)) as [Apellidos y Nombres],";
                query += "H.desmaesgen as [Gerencia],D.desmaesgen as [Área],E.deslabper as [Ocupación],";
                query += "rtrim(B.destipoper) as [Nomina],C.Parm1maesgen as [T. Doc],A.nrodoc as [Nro. Doc.],";
                query += "G.fechaini as [F.Ini.Labores],G.fechafin as [F.Fin/Cese] from perplan A (NOLOCK) ";
                query += "left join tipoper (NOLOCK) B on A.idtipoper=B.idtipoper ";
                query += "left join maesgen (NOLOCK) C on C.idmaesgen='002' and A.tipdoc=C.clavemaesgen ";
                query += "left join maesgen (NOLOCK) D on D.idmaesgen='008' and A.seccion=D.clavemaesgen ";
                query += "left join labper (NOLOCK) E on A.idcia=E.idcia and A.idlabper=E.idlabper and A.idtipoper=E.idtipoper ";
                query += "left join view_perlab F on A.idcia=F.idcia collate Modern_Spanish_CI_AI and A.idperplan=F.idperplan collate Modern_Spanish_CI_AI ";
                query += "left join perlab (NOLOCK) G on F.idcia=G.idcia and F.idperplan=G.idperplan and F.idperlab=G.idperlab ";
                query += "left join maesgen (NOLOCK) H on H.idmaesgen='040' and H.clavemaesgen=A.codaux and H.parm1maesgen=A.idcia ";
                query += "where alta_tregistro = 1 " + @cadenaidtipoper + @cadenasituacion + @cadenaseccion + @cadenaBusqueda + " order by a.idperplan asc;";

                loadqueryDatos(query);
            }
        }
    }
}