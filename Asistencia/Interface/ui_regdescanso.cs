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
    public partial class ui_regdescanso : Form
    {
        Funciones funciones = new Funciones();
        DataTable dt = new DataTable();

        public ui_regdescanso()
        {
            InitializeComponent();

            if (funciones.VersionAssembly()) Application.ExitThread();

            //Ocultar tab de query y manejo de errores
            tabControlRegDescanso.TabPages.Remove(tabConexion);
            tabControlRegDescanso.TabPages.Remove(tabManejoDeErrores);

        
        }

        private void ui_regvac_Load(object sender, EventArgs e)
        {
            GlobalVariables variables = new GlobalVariables();
            MaesGen maesgen = new MaesGen();
            string squery;
            squery = " SELECT 'X' as clave,'TODOS' as descripcion  UNION ALL ";
            squery += "SELECT a.idcia as clave, a.descia as descripcion from ciafile a (nolock) ";
            funciones.listaToolStripComboBox(squery, cmbcia);
            dtpFecfin.MinDate = dtpFecini.Value;

            maesgen.listaDetMaesGen("171", cmbcontingencia, "X");
            maesgen.listaDetMaesGen("173", cmbConcepSap, "X");
            cmbcontingencia.Text = "X   TODOS";
            cmbConcepSap.Text = "X   TODOS";

            btnEliminar.Visible = false;
            if (variables.getValorTypeUsr() == "00" || variables.getValorNivelUsr() == "1") { btnEliminar.Visible = true; }
        }

        private void ui_ListaRegVac()
        {
            Funciones funciones = new Funciones();
            string idcia = funciones.getValorToolStripComboBox(cmbcia, 2);
            string idtipoper = funciones.getValorToolStripComboBox(cmbCategoria, 2);
            string contingencia = funciones.getValorComboBox(cmbcontingencia, 3);
            string consap = funciones.getValorComboBox(cmbConcepSap, 3);
            string buscar = txtBuscar.Text.Trim();

            try
            {
                if (consap != string.Empty)
                {
                    string cadenaAnio = string.Empty;
                    string cadenaBusqueda = string.Empty;
                    string cadenaBusqueda2 = string.Empty;
                    if (radioButtonNombre.Checked && txtBuscar.Text.Trim() != string.Empty)
                    {
                        cadenaBusqueda = "and RTRIM(t.apepat)+' '+RTRIM(t.apemat)+', '+RTRIM(t.nombres) like '%" + @buscar + "%' ";
                    }
                    else
                    {
                        if (radioButtonNroDoc.Checked && txtBuscar.Text.Trim() != string.Empty)
                        {
                            cadenaBusqueda = "and t.nrodoc LIKE '%" + @buscar + "%' ";
                        }
                    }

                    string query = @" SELECT t.idperplan AS [Cod.Interno],t.Parm1maesgen [T.Doc.],t.nrodoc [Nro.Documento],
RTRIM(t.apepat)+' '+RTRIM(t.apemat)+', '+RTRIM(t.nombres) AS [Apellidos y Nombres],t.codaux [Sociedad],
t.Empresa,t.Gerencia,t.Area,t.idtipoper [Nomina],t.destipoper [Nomb.Nomina],t.desestane [Sede],t.anio AS [Año],
CONVERT(VARCHAR(10),t.finivac,103) AS [F.Ini Descanso],CONVERT(VARCHAR(10),t.ffinvac,103) AS [F.Fin Descanso],t.diasvac AS [D.Descanso],t.mescontin [Mes.Contingencia],
CONVERT(VARCHAR(10),t.fechaemision,103) AS [Fec.Emision],t.idusrfecha [Fecha Reg.],t.idusr [Usuario Reg.],t.concesap AS ConceptoSAP,t.estadocit AS [Estado CIT],
t.certificado [CIT],t.Diagnostico,t.Contingencia,t.tipocolegiatura [T.Colegiatura],t.Colegiatura,t.medico [Doctor(a)],t.Especialidad,t.estacionsalud [Est. de Salud],
t.totaldias,t.alerta,t.idregvac,t.mesconting,t.statusCIT,t.contingencia AS conting,ISNULL(t.celular,'') CelularTrabajador,t.consap FROM ( 
SELECT B.idperplan,D.Parm1maesgen,B.nrodoc,B.apepat,B.apemat,B.nombres,E.codaux,
E.descia [Empresa],F.desmaesgen [Gerencia],F.desmaesgen [Area],B.idtipoper,L.destipoper,C.desestane,A.anio,
A.finivac,A.ffinvac,A.diasvac,I.desmaesgen mescontin,
a.fechaemision,a.idusrfecha,a.idusr,J.desmaesgen AS concesap,K.desmaesgen estadocit,
a.certificado,a.Diagnostico,H.desmaesgen Contingencia,a.tipocolegiatura,a.Colegiatura,a.medico,a.Especialidad,a.estacionsalud,
(SELECT ISNULL(SUM(diasvac),0) FROM regdescanso (NOLOCK) WHERE anio=A.anio AND idperplan=A.idperplan) AS totaldias,a.alerta,a.idregvac,a.mesconting,
a.statusCIT,A.contingencia AS conting,A.celular,a.consap 
FROM regdescanso A (NOLOCK) 
INNER JOIN perplan B (NOLOCK) ON A.idperplan=B.idperplan 
INNER JOIN estane C (NOLOCK) on C.idestane=B.estane AND C.codemp=B.rucemp 
INNER JOIN tipoper L (NOLOCK) on L.idtipoper=B.idtipoper 
LEFT JOIN maesgen D (NOLOCK) ON D.idmaesgen='002' and B.tipdoc=D.clavemaesgen 
INNER JOIN ciafile E (NOLOCK) on E.idcia=B.idcia 
LEFT JOIN maesgen F (NOLOCK) on F.idmaesgen='040' and F.clavemaesgen=B.codaux AND F.parm1maesgen=B.idcia 
LEFT JOIN maesgen G (NOLOCK) on G.idmaesgen='008' and G.clavemaesgen=B.seccion 
LEFT JOIN maesgen H (NOLOCK) on H.idmaesgen='171' and H.clavemaesgen=A.contingencia 
LEFT JOIN maesgen I (NOLOCK) on I.idmaesgen='035' and I.clavemaesgen=A.mesconting 
LEFT JOIN maesgen J (NOLOCK) on J.idmaesgen='173' and J.clavemaesgen=A.consap 
LEFT JOIN maesgen K (NOLOCK) on K.idmaesgen='174' and K.clavemaesgen=A.statusCIT ) t
WHERE t.fechaemision BETWEEN '" + dtpFecini.Value.ToString("yyyy-MM-dd") + "' AND '" + dtpFecfin.Value.ToString("yyyy-MM-dd") + "' ";


                    //validacion para combo de Tipo de sociedad en "Empresa"
                    if (idcia == "01") { idcia = "153"; }
                    if (idcia == "02") { idcia = "157"; }
                    if (idcia == "03") { idcia = "158"; }
                    //Fin validacion 

                    if (idtipoper != "X") { query += " AND t.idtipoper='" + @idtipoper + "' "; }
                    if (idcia != "X") { query += " AND t.codaux='" + @idcia + "' "; }

                    //validacion para combo de tipo de contingencia en "Contingencia" 
                    if (contingencia == "001") { contingencia = "Accidente Comun"; }
                    if (contingencia == "002") { contingencia = "Accidente de trafico"; }

                    if (contingencia == "003") { contingencia = "Accidente laboral"; }
                    if (contingencia == "004") { contingencia = "Enfermedad comun"; }
                    if (contingencia == "005") { contingencia = "Maternidad"; }
                    if (contingencia == "006") { contingencia = "COVID-19"; }

                if (contingencia != "X") { query += " AND t.contingencia='" + @contingencia + "' "; }
                if (consap != "X") { query += " AND t.consap='" + consap + "' "; }

                query += cadenaAnio + cadenaBusqueda;
                query += " ORDER BY t.ffinvac DESC;";

                LoadQuery(query);
                //Mostrar la query para la grilla 
                txtQuery.Text = query;


                calcularTotalDiasDescanso();
                }
            }
            catch (Exception ex_)
            {
                txtManejoErrores.Text = "Error en la grilla " + ex_;
                throw;
            }
        }
        private void LoadQuery(string query)
        {
            try
            {
                Funciones funciones = new Funciones();
                SqlConnection conexion = new SqlConnection();
                conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
                conexion.Open();

                using (SqlDataAdapter myDataAdapter = new SqlDataAdapter(query, conexion))
                {
                    DataSet myDataSet = new DataSet();
                    dt = new DataTable();
                    myDataAdapter.Fill(dt);
                    funciones.formatearDataGridView(dgvdetalle);

                    dgvdetalle.DataSource = dt;

                    AutoFormatGrid();

                    //Cambiar formato de grilla
                    //dgvdetalle.AllowUserToResizeRows = false;
                    //dgvdetalle.AllowUserToResizeColumns = false;
                    //foreach (DataGridViewColumn column in dgvdetalle.Columns)
                    //{
                    //    column.SortMode = DataGridViewColumnSortMode.NotSortable;
                    //}
                }
                conexion.Close();
            }
            catch (Exception ex)
            {

            }
        }

        private void AutoFormatGrid()
        {
            dgvdetalle.Columns[0].Frozen = true;
            dgvdetalle.Columns[1].Frozen = true;
            dgvdetalle.Columns[2].Frozen = true;
            dgvdetalle.Columns[3].Frozen = true;

            dgvdetalle.Columns[0].Width = 70;
            dgvdetalle.Columns[1].Width = 50;
            dgvdetalle.Columns[2].Width = 95;
            dgvdetalle.Columns[3].Width = 270;
            dgvdetalle.Columns[4].Width = 70;
            dgvdetalle.Columns[5].Width = 200;
            dgvdetalle.Columns[6].Width = 200;
            dgvdetalle.Columns[7].Width = 200;
            dgvdetalle.Columns[8].Width = 50;
            dgvdetalle.Columns[9].Width = 100;
            dgvdetalle.Columns[10].Width = 100;
            dgvdetalle.Columns[11].Width = 50;
            dgvdetalle.Columns[12].Width = 90;
            dgvdetalle.Columns[13].Width = 90;
            dgvdetalle.Columns[14].Width = 70;
            dgvdetalle.Columns[15].Width = 90;
            dgvdetalle.Columns[16].Width = 90;
            dgvdetalle.Columns[17].Width = 90;
            dgvdetalle.Columns[18].Width = 90;
            dgvdetalle.Columns[19].Width = 90;
            dgvdetalle.Columns[20].Width = 80;
            dgvdetalle.Columns[21].Width = 200;
            dgvdetalle.Columns[22].Width = 140;
            dgvdetalle.Columns[23].Width = 140;
            dgvdetalle.Columns[24].Width = 75;
            dgvdetalle.Columns[25].Width = 200;
            dgvdetalle.Columns[26].Width = 200;
            dgvdetalle.Columns[27].Width = 75;
            dgvdetalle.Columns[28].Width = 200;

            dgvdetalle.Columns[29].Visible = false;
            dgvdetalle.Columns[30].Visible = false;
            dgvdetalle.Columns[31].Visible = false;
            dgvdetalle.Columns[32].Visible = false;
            dgvdetalle.Columns[33].Visible = false;
            dgvdetalle.Columns[34].Visible = false;
            dgvdetalle.Columns[36].Visible = false;
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cmbCategoria_SelectedIndexChanged(object sender, EventArgs e)
        {
            string idtipoper = funciones.getValorToolStripComboBox(cmbCategoria, 2);
            if (idtipoper != string.Empty)
            {
                ui_ListaRegVac();
            }
        }

        private void btnTodos_Click(object sender, EventArgs e)
        {
            txtBuscar.Clear();
            txtBuscar.Focus();
            ui_ListaRegVac();
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            ui_ListaRegVac();
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            //Exporta exporta = new Exporta();
            //exporta.Excel_FromDataGridView(dgvdetalle);
            if (dgvdetalle.Rows.Count > 0)
            {
                var data = ((DataTable)dgvdetalle.DataSource).Copy();
                data.Columns.Remove("consap");
                data.Columns.Remove("conting");
                data.Columns.Remove("statusCIT");
                data.Columns.Remove("mesconting");
                data.Columns.Remove("idregvac");
                data.Columns.Remove("alerta");
                data.Columns.Remove("totaldias");
                dgvdetalleExcel.DataSource = data;

                Exporta exporta = new Exporta();
                exporta.Excel_FromDataGridView(dgvdetalleExcel);
            }
            else
            {
                MessageBox.Show("No existe ningun registro a exportar.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            GlobalVariables variables = new GlobalVariables();
            string idcia = variables.getValorCia();
            string idtipoper = cmbCategoria.Text.Substring(0, 2);

            ui_updregdescanso ui_detalle = new ui_updregdescanso();
            ui_detalle._FormPadre = this;
            ui_detalle.setValores(idcia, idtipoper);
            ui_detalle.ui_newRegVac();
            ui_detalle.Activate();
            ui_detalle.BringToFront();
            ui_detalle.ShowDialog();
            ui_detalle.Dispose();
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            Int32 selectedCellCount =
            dgvdetalle.GetCellCount(DataGridViewElementStates.Selected);
            if (selectedCellCount > 0)
            {
                GlobalVariables variables = new GlobalVariables();
                Funciones funciones = new Funciones();
                string idcia = variables.getValorCia();
                string idtipoper = cmbCategoria.Text.Substring(0, 2);
                string idperplan = dgvdetalle.Rows[dgvdetalle.SelectedCells[1].RowIndex].Cells[0].Value.ToString();
                string anio = dgvdetalle.Rows[dgvdetalle.SelectedCells[1].RowIndex].Cells[11].Value.ToString();
                string finivac = dgvdetalle.Rows[dgvdetalle.SelectedCells[1].RowIndex].Cells[12].Value.ToString();
                string ffinvac = dgvdetalle.Rows[dgvdetalle.SelectedCells[1].RowIndex].Cells[13].Value.ToString();
                string diasvac = dgvdetalle.Rows[dgvdetalle.SelectedCells[1].RowIndex].Cells[14].Value.ToString();
                string mes = dgvdetalle.Rows[dgvdetalle.SelectedCells[1].RowIndex].Cells[15].Value.ToString();
                string fcontingencia = dgvdetalle.Rows[dgvdetalle.SelectedCells[1].RowIndex].Cells[16].Value.ToString();
                string cer = dgvdetalle.Rows[dgvdetalle.SelectedCells[1].RowIndex].Cells[21].Value.ToString();
                string diagn = dgvdetalle.Rows[dgvdetalle.SelectedCells[1].RowIndex].Cells[22].Value.ToString();
                string tipo = dgvdetalle.Rows[dgvdetalle.SelectedCells[1].RowIndex].Cells[24].Value.ToString();
                string cmp = dgvdetalle.Rows[dgvdetalle.SelectedCells[1].RowIndex].Cells[25].Value.ToString();
                string med = dgvdetalle.Rows[dgvdetalle.SelectedCells[1].RowIndex].Cells[26].Value.ToString();
                string esp = dgvdetalle.Rows[dgvdetalle.SelectedCells[1].RowIndex].Cells[27].Value.ToString();
                string estac = dgvdetalle.Rows[dgvdetalle.SelectedCells[1].RowIndex].Cells[28].Value.ToString();
                string totdias = dgvdetalle.Rows[dgvdetalle.SelectedCells[1].RowIndex].Cells[29].Value.ToString();
                string alerta = dgvdetalle.Rows[dgvdetalle.SelectedCells[1].RowIndex].Cells[30].Value.ToString();
                string idregvac = dgvdetalle.Rows[dgvdetalle.SelectedCells[1].RowIndex].Cells[31].Value.ToString();
                string mesconting = dgvdetalle.Rows[dgvdetalle.SelectedCells[1].RowIndex].Cells[32].Value.ToString();
                string status = dgvdetalle.Rows[dgvdetalle.SelectedCells[1].RowIndex].Cells[33].Value.ToString();
                string conti = dgvdetalle.Rows[dgvdetalle.SelectedCells[1].RowIndex].Cells[34].Value.ToString();
                string celu = dgvdetalle.Rows[dgvdetalle.SelectedCells[1].RowIndex].Cells[35].Value.ToString();
                string consap = dgvdetalle.Rows[dgvdetalle.SelectedCells[1].RowIndex].Cells[36].Value.ToString();

                ui_updregdescanso ui_detalle = new ui_updregdescanso();
                ui_detalle._FormPadre = this;
                ui_detalle.setValores(idcia, idtipoper);
                ui_detalle.Activate();
                ui_detalle.ui_loadDatosRegVac(idperplan, anio, finivac, ffinvac, diasvac, idregvac,
                    cer, med, esp, cmp, estac, diagn, conti, tipo, mesconting, totdias, alerta, status, celu, consap, fcontingencia);
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
            Descansos regvac = new Descansos();
            Int32 selectedCellCount = dgvdetalle.GetCellCount(DataGridViewElementStates.Selected);
            if (selectedCellCount > 0)
            {
                GlobalVariables variables = new GlobalVariables();
                Funciones funciones = new Funciones();
                string id = variables.getValorCia();
                string idperplan = dgvdetalle.Rows[dgvdetalle.SelectedCells[1].RowIndex].Cells[0].Value.ToString();
                string trabajador = dgvdetalle.Rows[dgvdetalle.SelectedCells[1].RowIndex].Cells[3].Value.ToString();
                string idregvac = dgvdetalle.Rows[dgvdetalle.SelectedCells[1].RowIndex].Cells[31].Value.ToString();
                if (idregvac != string.Empty)
                {
                    DialogResult resultado = MessageBox.Show("¿Desea eliminar el registro vacacional Nro." + idregvac + " del trabajador " + trabajador + "?",
                    "Consulta Importante",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);
                    if (resultado == DialogResult.Yes)
                    {
                        regvac.EliminarDescanso(idregvac);
                        ui_ListaRegVac();
                    }
                }
                else
                {
                    MessageBox.Show("Item no se puede eliminar por esta ventana, ir a Parte de Planilla", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
            }
            else
            {
                MessageBox.Show("No ha seleccionado registro a Eliminar", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void cmbcia_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbCategoria.Items.Clear();
            dgvdetalle.DataSource = null;
            Funciones funciones = new Funciones();
            string idcia = funciones.getValorToolStripComboBox(cmbcia, 2);
            string squery;
            squery = "SELECT 'X' as clave,'TODOS' as descripcion  UNION ALL ";
            squery += "SELECT idtipoper as clave,destipoper as descripcion FROM tipoper (NOLOCK) ";
            squery += "WHERE idtipoper <> 'X3' ";
            if (idcia != "X")
            {   
                squery += "AND idtipoper IN (SELECT idtipoper FROM perplan (NOLOCK) WHERE idcia='" + idcia + "' GROUP BY idtipoper)";
            }

            funciones.listaToolStripComboBox(squery, cmbCategoria);
        }

        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {
                try
            {
                if (txtBuscar.Text.Trim() != string.Empty)
                {
                    var query = dt.AsEnumerable().Where(x => x.Field<string>((radioButtonNombre.Checked ? "Apellidos y Nombres" : "Nro.Documento")).Contains(txtBuscar.Text.ToUpper())).CopyToDataTable();
                    dgvdetalle.DataSource = query;
                    calcularTotalDiasDescanso();
                    AutoFormatGrid();
                }
                else { btnTodos_Click(sender, e); }
            }
            catch (Exception ex_)
            {
                lblTotalDiasDescanso.Text = "--";
                txtManejoErrores.Text = "Error en el evento txtBuscar_TextChanged : " + ex_;
                dgvdetalle.DataSource = null;
            }
        }

        private void radioButtonNroDoc_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonNroDoc.Checked)
            {
                //ui_ListaRegVac();
                //txtBuscar.Focus();
                try
                {
                    var query = dt.AsEnumerable().Where(x => x.Field<string>((radioButtonNombre.Checked ? "Apellidos y Nombres" : "Nro.Documento")).Contains(txtBuscar.Text.ToUpper())).CopyToDataTable();
                    dgvdetalle.DataSource = query;

                    AutoFormatGrid();
                }
                catch (Exception ex_)
                {
                    dgvdetalle.DataSource = null;
                }
            }
        }

        private void radioButtonNombre_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonNombre.Checked)
            {
                //ui_ListaRegVac();
                //txtBuscar.Focus();
                try
                {
                    var query = dt.AsEnumerable().Where(x => x.Field<string>((radioButtonNombre.Checked ? "Apellidos y Nombres" : "Nro.Documento")).Contains(txtBuscar.Text.ToUpper())).CopyToDataTable();
                    dgvdetalle.DataSource = query;
                    AutoFormatGrid();
                }
                catch (Exception ex_)
                {
                    dgvdetalle.DataSource = null;
                }
            }
        }

        private void cmbcontingencia_SelectedIndexChanged(object sender, EventArgs e)
        {
            string contingencia = funciones.getValorComboBox(cmbcontingencia, 3);
            if (contingencia != string.Empty)
            {
                ui_ListaRegVac();
            }
        }

        private void dtpFecini_ValueChanged(object sender, EventArgs e)
        {
            dtpFecfin.MinDate = dtpFecini.Value;
            if (dtpFecini.Value > dtpFecfin.Value)
            {
                dtpFecfin.Value = dtpFecini.Value;
            }

            ui_ListaRegVac();
        }

        private void dtpFecfin_ValueChanged(object sender, EventArgs e)
        {
            ui_ListaRegVac();
        }

        private void cmbConcepSap_SelectedIndexChanged(object sender, EventArgs e)
        {
            string consap = funciones.getValorComboBox(cmbConcepSap, 3);
            if (consap != string.Empty)
            {
                ui_ListaRegVac();
            }
        }

        private void toolSBtnCode_Click(object sender, EventArgs e)
        {
            if (tabControlRegDescanso.TabPages.Contains(tabConexion) && tabControlRegDescanso.TabPages.Contains(tabManejoDeErrores)) //Si existen los tab -> ocultarlos con remove
            {
                tabControlRegDescanso.TabPages.Remove(tabConexion);
                tabControlRegDescanso.TabPages.Remove(tabManejoDeErrores);
            }
            else
            {
                var resultado = MessageBox.Show("¿Está seguro que quiere activar la opción de programación?", "Confirmación", MessageBoxButtons.YesNo);
                if (resultado == DialogResult.Yes) 
                {
                    tabControlRegDescanso.TabPages.Add(tabConexion);
                    tabControlRegDescanso.TabPages.Add(tabManejoDeErrores);
                    tabControlRegDescanso.SelectedTab = tabConexion; // Seleccionar conexión donde esta la query
                }
            }
        }

        private void calcularTotalDiasDescanso() {
            int suma = 0;
            foreach (DataGridViewRow row in dgvdetalle.Rows)
            {
                if (row.Cells["D.Descanso"].Value != null)
                    suma += (Int32)row.Cells["D.Descanso"].Value;
            }
            lblTotalDiasDescanso.Text = suma.ToString();
        }

        private void BtnReportarError_Click(object sender, EventArgs e)
        {
          


            //if (txtManejoErrores.Text == "")
            //{
            //    //(new System.Threading.Thread(funciones.cerrarMensaje)).Start(); // Intento para que se cierre el MessageBox (Implementar tipo toast)
            //    MessageBox.Show("No se encontro ningun error para reportar. Contactar con sistemas en caso no se detecte el error automaticamente.");
            //}
            //else { 
            //    MessageBox.Show("No ha seleccionado registro a Editar", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            //}
            ////funciones.submitErrorMessage("RegDescanso", txtQuery.Text, txtManejoErrores.Text);

            ////funciones.reportUserSubmit("Ticket 0001", "Bienestar Social - Registros desscanso", txtErrorReportado.Text);
            //funciones.crearTicket("txtErrorReportado.Text", "Bienestar social - Descanso medicos");



            string input = "";
            ShowInputDialog(ref input);
            //MessageBox.Show("Texto reportado es : ", ;

        }

        private  DialogResult ShowInputDialog(ref string input)
        {
            
            if (txtManejoErrores.Text == "")
            {
                //(new System.Threading.Thread(funciones.cerrarMensaje)).Start(); // Intento para que se cierre el MessageBox (Implementar tipo toast)
                MessageBox.Show("No se encontro ningun error para reportar. Contactar con sistemas en caso no se detecte el error automaticamente.","SISASIS");
            }
            else
            {

                Form inputBox = new Form();
             
              
                //txtErrorReportado.Text



                System.Drawing.Size size = new System.Drawing.Size(200, 70);
                //Form inputBox = new Form();

                inputBox.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
                inputBox.ClientSize = size;
                inputBox.Text = "Ingrese el detalle de su error ";

                System.Windows.Forms.TextBox textBox = new TextBox();
                textBox.Size = new System.Drawing.Size(size.Width - 100, 90);
                //// 10, 23
                textBox.Location = new System.Drawing.Point(5, 5);
                textBox.Text = input;
                inputBox.Controls.Add(textBox);

                Button okButton = new Button();
                okButton.DialogResult = System.Windows.Forms.DialogResult.OK;
                okButton.Name = "okButton";
                okButton.Size = new System.Drawing.Size(75, 23);
                okButton.Text = "&OK";
                okButton.Location = new System.Drawing.Point(size.Width - 80 - 80, 39);
                inputBox.Controls.Add(okButton);

                Button cancelButton = new Button();
                cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
                cancelButton.Name = "cancelButton";
                cancelButton.Size = new System.Drawing.Size(75, 23);
                cancelButton.Text = "&Cancel";
                cancelButton.Location = new System.Drawing.Point(size.Width - 80, 39);
                inputBox.Controls.Add(cancelButton);

                inputBox.AcceptButton = okButton;
                inputBox.CancelButton = cancelButton;

                DialogResult result2 = inputBox.ShowDialog();
                input = textBox.Text;
                //funciones.crearTicket(textBox.Text, "Bienestar social - Descanso medicos");



                
              //  funciones.submitErrorMessageToProgrammer("RegDescanso", txtQuery.Text, txtManejoErrores.Text);
               // funciones.reportUserSubmit("Ticket 0001", "Bienestar Social - Registros desscanso", input);
                //Previo a ejecutar creac ticket
                return result2;
            }



            return DialogResult;


        }

    }
}
