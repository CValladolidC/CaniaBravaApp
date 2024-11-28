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
    public partial class ui_upddatareten : Form
    {
        string _operacion;
        string _messem;
        string _anio;
        string _idcia;
        string _idtipoper;
        string _idtipocal;
        string _idtipoplan;
        string _iddestajo;
        string _idproddes;
        string _idzontra;
        string _emplea;
        string _estane;
        string _fini;
        string _ffin;
        string _titulo;

        private TextBox TextBoxActivo;
        private Form FormPadre;

        public Form _FormPadre
        {
            get { return FormPadre; }
            set { FormPadre = value; }
        }

        public TextBox _TextBoxActivo
        {
            get { return TextBoxActivo; }
            set { TextBoxActivo = value; }
        }

        public ui_upddatareten()
        {
            InitializeComponent();
        }

        private void ui_upddatareten_Load(object sender, EventArgs e)
        {
            this.Text = "Serv. 4ta. y 5ta. Categoría - " + this._titulo;
            lblTitulo.Text = "Periodo : " + this._messem + "/" + this._anio + " Del " + this._fini.Substring(0, 10) + " al " + this._ffin.Substring(0, 10);
            GlobalVariables variables = new GlobalVariables();
            Funciones funciones = new Funciones();
            string idcia = variables.getValorCia();
            string query = "SELECT idlabret as clave,deslabret as descripcion FROM labret WHERE idcia='" + @idcia + "';";
            funciones.listaComboBox(query, cmbServicio, "B");
            query = "SELECT idcencos as clave,descencos as descripcion FROM cencos WHERE idcia='" + @idcia + "' and statecencos='V';";
            funciones.listaComboBox(query, cmbCenCos, "B");
            ui_detalle();
        }

        private void ui_detalle()
        {
            Funciones funciones = new Funciones();
            string idcia = this._idcia;
            string idproddes = this._idproddes;
            string idzontra = this._idzontra;
            string messem = this._messem;
            string anio = this._anio;
            string idtipocal = this._idtipocal;
            string idtipoper = this._idtipoper;
            string emplea = this._emplea;
            string estane = this._estane;
            string idperplan = txtCodigoInterno.Text;
            string idtipoplan = "";

            string query = " select A.fecha,E.deszontra,L.deslabret,C.descencos,A.cantidad,A.precio,A.subtotal,";
            query = query + " A.adicional,A.reten,A.total,A.iddestajo, ";
            query = query + " A.idlabret,A.idcia,A.idcencos from desret A  ";
            query = query + " left join perret B on A.idperplan=B.idperplan and A.idcia=B.idcia ";
            query = query + " left join zontra E on A.idzontra=E.idzontra and A.idcia=E.idcia ";
            query = query + " left join labret L on A.idlabret=L.idlabret and A.idcia=L.idcia ";
            query = query + " left join cencos C on A.idcencos=C.idcencos and A.idcia=C.idcia ";
            query = query + " where A.idcia='" + @idcia + "' and idtipocal='" + @idtipocal + "' ";
            query = query + " and A.idtipoper='" + @idtipoper + "' ";
            query = query + " and A.messem='" + @messem + "' and A.anio='" + @anio + "' ";
            query = query + " and idproddes='" + @idproddes + "' and A.idtipoplan='" + @idtipoplan + "' ";
            query = query + " and A.emplea='" + @emplea + "' and A.estane='" + @estane + "' ";
            query = query + " and A.idzontra='" + @idzontra + "' and A.idperplan='" + @idperplan + "'";
            query = query + " order by A.fecha asc;";

            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();

            try
            {
                using (SqlDataAdapter myDataAdapter = new SqlDataAdapter(query, conexion))
                {
                    DataSet myDataSet = new DataSet();
                    myDataAdapter.Fill(myDataSet, "tblData");
                    funciones.formatearDataGridView(dgvdetalle);
                    dgvdetalle.DataSource = myDataSet.Tables["tblData"];
                    dgvdetalle.Columns[0].HeaderText = "Fecha";
                    dgvdetalle.Columns[1].HeaderText = "Zona";
                    dgvdetalle.Columns[2].HeaderText = "Servicio";
                    dgvdetalle.Columns[3].HeaderText = "Centro de Costo";
                    dgvdetalle.Columns[4].HeaderText = "Cantidad";
                    dgvdetalle.Columns[5].HeaderText = "Precio";
                    dgvdetalle.Columns[6].HeaderText = "Sub Total";
                    dgvdetalle.Columns[7].HeaderText = "Adicional";
                    dgvdetalle.Columns[8].HeaderText = "Retención";
                    dgvdetalle.Columns[9].HeaderText = "Importe Total MN";

                    dgvdetalle.Columns["iddestajo"].Visible = false;
                    dgvdetalle.Columns["idlabret"].Visible = false;
                    dgvdetalle.Columns["idcia"].Visible = false;
                    dgvdetalle.Columns["idcencos"].Visible = false;

                    dgvdetalle.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    dgvdetalle.Columns[6].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    dgvdetalle.Columns[7].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    dgvdetalle.Columns[8].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    dgvdetalle.Columns[9].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

                    dgvdetalle.Columns[5].DefaultCellStyle.Format = "###,###.##";
                    dgvdetalle.Columns[6].DefaultCellStyle.Format = "###,###.##";
                    dgvdetalle.Columns[7].DefaultCellStyle.Format = "###,###.##";
                    dgvdetalle.Columns[8].DefaultCellStyle.Format = "###,###.##";
                    dgvdetalle.Columns[9].DefaultCellStyle.Format = "###,###.##";

                    dgvdetalle.Columns[0].Width = 75;
                    dgvdetalle.Columns[1].Width = 90;
                    dgvdetalle.Columns[2].Width = 90;
                    dgvdetalle.Columns[3].Width = 90;
                    dgvdetalle.Columns[4].Width = 60;
                    dgvdetalle.Columns[5].Width = 60;
                    dgvdetalle.Columns[6].Width = 60;
                    dgvdetalle.Columns[7].Width = 60;
                    dgvdetalle.Columns[8].Width = 60;
                    dgvdetalle.Columns[9].Width = 60;

                    dgvdetalle.RowHeadersVisible = true;
                    if (dgvdetalle.Rows.Count > 0)
                    {
                        for (int r = 0; r < dgvdetalle.Rows.Count; r++)
                        {
                            this.dgvdetalle.Rows[r].HeaderCell.Value = (r + 1).ToString();
                        }
                    }
                    dgvdetalle.AutoResizeRowHeadersWidth(DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders);

                }

                float importe = float.Parse(funciones.sumaColumnaDataGridView(dgvdetalle, 9));
                txtImporte.Text = Convert.ToString(importe);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            conexion.Close();
        }

        public void setValores(string messem, string anio, string idcia, string idtipoper, string idtipocal,
            string idtipoplan, string idproddes, string idzontra, string emplea, string estane, string fini, string ffin, string titulo)
        {

            this._anio = anio;
            this._messem = messem;
            this._idcia = idcia;
            this._idtipoper = idtipoper;
            this._idtipocal = idtipocal;
            this._idtipoplan = idtipoplan;
            this._idproddes = idproddes;
            this._idzontra = idzontra;
            this._emplea = emplea;
            this._estane = estane;
            this._fini = fini;
            this._ffin = ffin;
            this._titulo = titulo;

        }

        public void ui_iniPerPlan(string operacion, string idperplan)
        {
            if (operacion.Equals("AGREGAR"))
            {
                txtCodigoInterno.Clear();
                txtDocIdent.Clear();
                txtNroDocIden.Clear();
                txtNombres.Clear();
                lblF2.Visible = true;
                pbLocalizar.Visible = true;
                txtCodigoInterno.Enabled = true;
                txtCodigoInterno.Focus();

            }
            else
            {
                PerRet PerRet = new PerRet();
                txtCodigoInterno.Text = idperplan;
                txtDocIdent.Text = PerRet.ui_getDatosPerRet(this._idcia, idperplan, "1");
                txtNroDocIden.Text = PerRet.ui_getDatosPerRet(this._idcia, idperplan, "2");
                txtNombres.Text = PerRet.ui_getDatosPerRet(this._idcia, idperplan, "3");
                lblF2.Visible = false;
                pbLocalizar.Visible = false;
                txtCodigoInterno.Enabled = false;
            }
            ui_detalle();
        }

        public void agregarItem()
        {
            this._operacion = "AGREGAR";
            limpiarItem();
            habilitarItem(true);
            txtCantidad.Text = "1";
            txtPrecio.Text = "0";
            txtSubTotal.Text = "0";
            txtAdi.Text = "0";
            txtReten.Text = "0";
            txtTotal.Text = "0";
            txtFecha.Focus();
        }

        public void limpiarItem()
        {
            txtFecha.Clear();
            cmbServicio.Text = "";
            cmbCenCos.Text = "";
            txtCantidad.Clear();
            txtPrecio.Clear();
            txtSubTotal.Clear();
            txtAdi.Clear();
            txtReten.Clear();
            txtTotal.Clear();
        }

        public void habilitarItem(bool estado)
        {
            cmbServicio.Enabled = estado;
            cmbCenCos.Enabled = estado;
            txtFecha.Enabled = estado;
            txtPrecio.Enabled = estado;
            txtAdi.Enabled = estado;
            txtReten.Enabled = estado;
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            ((ui_datareten)FormPadre).btnActualizar.PerformClick();
            this.Close();
        }

        private void txtCodigoInterno_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                string idcia = this._idcia;
                string clasepadre = "ui_upddatareten";
                string cadenaBusqueda = string.Empty;
                this._TextBoxActivo = txtCodigoInterno;
                ui_buscarperret ui_buscarperret = new ui_buscarperret();
                ui_buscarperret._FormPadre = this;
                ui_buscarperret.setValores(idcia, cadenaBusqueda, clasepadre);
                ui_buscarperret.Activate();
                ui_buscarperret.BringToFront();
                ui_buscarperret.ShowDialog();
                ui_buscarperret.Dispose();
            }
        }

        private void txtCodigoInterno_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                if (txtCodigoInterno.Text.Trim() != string.Empty)
                {
                    string idcia = this._idcia;
                    string idperplan = txtCodigoInterno.Text.Trim();

                    PerRet perret = new PerRet();
                    string codigoInterno = perret.ui_getDatosPerRet(idcia, idperplan, "0");
                    if (codigoInterno == string.Empty)
                    {
                        MessageBox.Show("Código no registrado del trabajador", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txtCodigoInterno.Clear();
                        txtDocIdent.Clear();
                        txtNroDocIden.Clear();
                        txtNombres.Clear();
                        txtCodigoInterno.Focus();

                    }
                    else
                    {
                        txtCodigoInterno.Text = perret.ui_getDatosPerRet(idcia, idperplan, "0");
                        txtDocIdent.Text = perret.ui_getDatosPerRet(idcia, idperplan, "1");
                        txtNroDocIden.Text = perret.ui_getDatosPerRet(idcia, idperplan, "2");
                        txtNombres.Text = perret.ui_getDatosPerRet(idcia, idperplan, "3");
                        ui_detalle();
                        e.Handled = true;
                        btnToolNuevo.Select();
                        toolstripform.Focus();
                    }


                }
                else
                {
                    txtCodigoInterno.Clear();
                    txtDocIdent.Clear();
                    txtNroDocIden.Clear();
                    txtNombres.Clear();
                    txtCodigoInterno.Focus();
                }
            }
        }

        private void toolStripForm_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void txtCantidad_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void txtPrecio_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                try
                {
                    float cantidad = float.Parse(txtCantidad.Text);
                    float precio = float.Parse(txtPrecio.Text);
                    float adicional = float.Parse(txtAdi.Text);
                    float reten = float.Parse(txtReten.Text);
                    float subtotal = cantidad * precio;
                    txtSubTotal.Text = subtotal.ToString();
                    txtTotal.Text = (subtotal + adicional - reten).ToString();
                    e.Handled = true;
                    txtAdi.Focus();
                }

                catch (FormatException ex)
                {
                    MessageBox.Show(ex.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtPrecio.Text = "0";
                    e.Handled = true;
                    txtPrecio.Focus();
                }

            }
        }

        private void txtFecha_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                if (UtileriasFechas.IsDate(txtFecha.Text))
                {

                    if (UtileriasFechas.compararFecha(txtFecha.Text, ">=", this._fini) && UtileriasFechas.compararFecha(txtFecha.Text, "<=", this._ffin))
                    {
                        e.Handled = true;
                        cmbServicio.Focus();
                    }
                    else
                    {
                        MessageBox.Show("La fecha de proceso no se encuentra dentro del rango del periodo", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        e.Handled = true;
                        txtFecha.Focus();
                    }
                }
                else
                {
                    MessageBox.Show("Fecha de proceso no válida", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    e.Handled = true;
                    txtFecha.Focus();
                }
            }
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            agregarItem();
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {

        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            try
            {
                Funciones funciones = new Funciones();
                string operacion = this._operacion;
                string iddestajo = this._iddestajo;
                string idcia = this._idcia;
                string idproddes = this._idproddes;
                string messem = this._messem;
                string anio = this._anio;
                string idtipoper = this._idtipoper;
                string idtipocal = this._idtipocal;
                string idzontra = this._idzontra;
                string emplea = this._emplea;
                string estane = this._estane;
                string idtipoplan = this._idtipoplan;
                string idlabret = funciones.getValorComboBox(cmbServicio, 2);
                string idcencos = funciones.getValorComboBox(cmbCenCos, 2);
                string idperplan = txtCodigoInterno.Text;
                string fecha = txtFecha.Text;
                float cantidad = float.Parse(txtCantidad.Text);
                float precio = float.Parse(txtPrecio.Text);
                float subtotal = float.Parse(txtCantidad.Text) * float.Parse(txtPrecio.Text);
                float adicional = float.Parse(txtAdi.Text);
                float reten = float.Parse(txtReten.Text);
                float total = subtotal + adicional - reten;

                string valorValida = "G";

                if (idperplan == string.Empty && valorValida == "G")
                {
                    valorValida = "B";
                    MessageBox.Show("No ha especificado Código de Personal 4ta. y 5ta. Categorá", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtCodigoInterno.Focus();
                }

                if (total <= 0 && valorValida == "G")
                {
                    valorValida = "B";
                    MessageBox.Show("Importe Total no válido para el registro", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtPrecio.Focus();
                }

                if (cmbServicio.Text == string.Empty && valorValida == "G")
                {
                    valorValida = "B";
                    MessageBox.Show("No ha seleccionado Servicio", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    cmbServicio.Focus();
                }

                if (cmbServicio.Text != string.Empty && valorValida == "G")
                {
                    string servicio = funciones.getValorComboBox(cmbServicio, 2);
                    string query = "SELECT idlabret as clave,deslabret as descripcion FROM labret WHERE idcia='" + @idcia + "' and idlabret='" + @servicio + "';";
                    string resultado = funciones.verificaItemComboBox(query, cmbServicio);

                    if (resultado.Trim() == string.Empty)
                    {
                        valorValida = "B";
                        MessageBox.Show("Dato incorrecto en Servicio", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        cmbServicio.Focus();
                    }
                }

                if (cmbCenCos.Text == string.Empty && valorValida == "G")
                {
                    valorValida = "B";
                    MessageBox.Show("No ha seleccionado Centro de Costo", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    cmbCenCos.Focus();
                }

                if (cmbCenCos.Text != string.Empty && valorValida == "G")
                {
                    string cencos = funciones.getValorComboBox(cmbCenCos, 2);
                    string query = "SELECT idcencos as clave,descencos as descripcion FROM cencos WHERE idcia='" + @idcia + "' and idcencos='" + @cencos + "';";
                    string resultado = funciones.verificaItemComboBox(query, cmbCenCos);

                    if (resultado.Trim() == string.Empty)
                    {
                        valorValida = "B";
                        MessageBox.Show("Dato incorrecto en Centro de Costo", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        cmbCenCos.Focus();
                    }
                }

                if (valorValida.Equals("G"))
                {
                    Destajo destajo = new Destajo();
                    destajo.updDataReten(operacion, idcia, idperplan, messem, anio, idtipocal, idtipoper, fecha, idproddes, idzontra,
                        cantidad, precio, subtotal, adicional, reten, total, idtipoplan, emplea, estane, iddestajo, idlabret, idcencos);
                    ui_detalle();
                    this.agregarItem();
                }

            }
            catch (FormatException ex)
            {
                MessageBox.Show(ex.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            ((ui_datareten)FormPadre).btnActualizar.PerformClick();
            this.Close();
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {

            string operacion = this._operacion;
            string idcia = this._idcia;
            string idproddes = this._idproddes;
            string messem = this._messem;
            string anio = this._anio;
            string idtipoper = this._idtipoper;
            string idtipocal = this._idtipocal;
            string idzontra = this._idzontra;
            string emplea = this._emplea;
            string estane = this._estane;
            string idtipoplan = this._idtipoplan;
            string idperplan = txtCodigoInterno.Text.Trim();

            Int32 selectedCellCount =
            dgvdetalle.GetCellCount(DataGridViewElementStates.Selected);
            if (selectedCellCount > 0)
            {

                string fecha = dgvdetalle.Rows[dgvdetalle.SelectedCells[1].RowIndex].Cells[0].Value.ToString();
                string iddestajo = dgvdetalle.Rows[dgvdetalle.SelectedCells[1].RowIndex].Cells[10].Value.ToString();


                DialogResult resultado = MessageBox.Show("¿Desea eliminar la fecha " + fecha.Substring(0, 10) + "?",
                "Consulta Importante",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);
                if (resultado == DialogResult.Yes)
                {
                    Destajo destajo = new Destajo();
                    destajo.delDataReten(idcia, idperplan, messem, anio, idtipocal, idtipoper, idproddes, idzontra, iddestajo);
                    ui_detalle();
                }

            }
            else
            {
                MessageBox.Show("No ha seleccionado registro a Eliminar", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {

            Int32 selectedCellCount = dgvdetalle.GetCellCount(DataGridViewElementStates.Selected);
            if (selectedCellCount > 0)
            {
                Funciones funciones = new Funciones();
                MaesGen maesgen = new MaesGen();
                string query;
                this._operacion = "EDITAR";
                string fecha = dgvdetalle.Rows[dgvdetalle.SelectedCells[1].RowIndex].Cells[0].Value.ToString();
                string cantidad = dgvdetalle.Rows[dgvdetalle.SelectedCells[1].RowIndex].Cells[4].Value.ToString();
                string precio = dgvdetalle.Rows[dgvdetalle.SelectedCells[1].RowIndex].Cells[5].Value.ToString();
                string subtotal = dgvdetalle.Rows[dgvdetalle.SelectedCells[1].RowIndex].Cells[6].Value.ToString();
                string adicional = dgvdetalle.Rows[dgvdetalle.SelectedCells[1].RowIndex].Cells[7].Value.ToString();
                string reten = dgvdetalle.Rows[dgvdetalle.SelectedCells[1].RowIndex].Cells[8].Value.ToString();
                string total = dgvdetalle.Rows[dgvdetalle.SelectedCells[1].RowIndex].Cells[9].Value.ToString();
                string iddestajo = dgvdetalle.Rows[dgvdetalle.SelectedCells[1].RowIndex].Cells[10].Value.ToString();
                string idlabret = dgvdetalle.Rows[dgvdetalle.SelectedCells[1].RowIndex].Cells[11].Value.ToString();
                string idcia = dgvdetalle.Rows[dgvdetalle.SelectedCells[1].RowIndex].Cells[12].Value.ToString();
                string idcencos = dgvdetalle.Rows[dgvdetalle.SelectedCells[1].RowIndex].Cells[13].Value.ToString();
                this._iddestajo = iddestajo;

                txtFecha.Text = fecha;
                query = " Select idlabret as clave,deslabret as descripcion from labret ";
                query = query + " where idcia='" + @idcia + "' and idlabret='" + @idlabret + "';";
                funciones.consultaComboBox(query, cmbServicio);

                query = "SELECT idcencos as clave,descencos as descripcion FROM cencos WHERE idcia='" + @idcia + "' and idcencos='" + @idcencos + "';";
                funciones.consultaComboBox(query, cmbCenCos);
                txtCantidad.Text = cantidad;
                txtPrecio.Text = precio;
                txtSubTotal.Text = subtotal;
                txtAdi.Text = adicional;
                txtReten.Text = reten;
                txtTotal.Text = total;
                habilitarItem(true);
                txtFecha.Focus();
            }
            else
            {
                MessageBox.Show("No ha seleccionado registro a Editar", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void toolstripform_ItemClicked_1(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void cmbServicio_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                if (cmbServicio.Text != String.Empty)
                {
                    GlobalVariables variables = new GlobalVariables();
                    Funciones funciones = new Funciones();
                    string idcia = variables.getValorCia();
                    string idlabper = funciones.getValorComboBox(cmbServicio, 2);
                    string query = "SELECT idlabret as clave,deslabret as descripcion FROM labret WHERE idcia='" + @idcia + "' and idlabret='" + @idlabper + "';";
                    funciones.validarCombobox(query, cmbServicio);
                }
                e.Handled = true;
                cmbCenCos.Focus();
            }
        }

        private void txtSubTotal_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void txtAdi_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                try
                {
                    float cantidad = float.Parse(txtCantidad.Text);
                    float precio = float.Parse(txtPrecio.Text);
                    float adicional = float.Parse(txtAdi.Text);
                    float reten = float.Parse(txtReten.Text);
                    float subtotal = cantidad * precio;
                    txtSubTotal.Text = subtotal.ToString();
                    txtTotal.Text = (subtotal + adicional - reten).ToString();
                    e.Handled = true;
                    txtReten.Focus();
                }
                catch (FormatException ex)
                {
                    MessageBox.Show(ex.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtAdi.Text = "0";
                    e.Handled = true;
                    txtAdi.Focus();
                }

            }
        }

        private void txtReten_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                try
                {
                    float cantidad = float.Parse(txtCantidad.Text);
                    float precio = float.Parse(txtPrecio.Text);
                    float adicional = float.Parse(txtAdi.Text);
                    float reten = float.Parse(txtReten.Text);
                    float subtotal = cantidad * precio;
                    txtSubTotal.Text = subtotal.ToString();
                    txtTotal.Text = (subtotal + adicional - reten).ToString();
                    e.Handled = true;
                    btnToolGrabar.Select();
                    toolstripform.Focus();
                }
                catch (FormatException ex)
                {
                    MessageBox.Show(ex.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtReten.Text = "0";
                    e.Handled = true;
                    txtReten.Focus();
                }

            }
        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void cmbCenCos_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                if (cmbCenCos.Text != String.Empty)
                {
                    Funciones funciones = new Funciones();
                    string idcia = this._idcia;
                    string clave = funciones.getValorComboBox(cmbCenCos, 2);
                    string query = "SELECT idcencos as clave,descencos as descripcion FROM cencos WHERE idcia='" + @idcia + "' and idcencos='" + @clave + "';";
                    funciones.validarCombobox(query, cmbCenCos);
                }

                e.Handled = true;
                txtPrecio.Focus();

            }
        }
    }
}