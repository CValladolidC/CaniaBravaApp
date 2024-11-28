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
    public partial class ui_diassubsi : Form
    {
        string _operacion;
        string _operacionpadre;
        string _idcia;
        string _empleador;
        string _idtipoper;
        string _idperplan;
        string _nombretrabajador;
        string _anio;
        string _messem;
        string _idtipocal;
        string _fechaini;
        string _fechafin;
        string _sexo;
        string _idtipoplan;

        private Form FormPadre;

        public Form _FormPadre
        {
            get { return FormPadre; }
            set { FormPadre = value; }
        }

        public void setValores(string _idcia, string _empleador, string _idtipoper, string _idperplan, string _nombretrabajador, string _anio, string _messem, string _idtipocal, string _fechaini, string _fechafin, string _sexo, string _operacionpadre, string idtipoplan)
        {

            this._operacion = string.Empty;
            this._idcia = _idcia;
            this._empleador = _empleador;
            this._idtipoper = _idtipoper;
            this._idperplan = _idperplan;
            this._nombretrabajador = _nombretrabajador;
            this._anio = _anio;
            this._messem = _messem;
            this._idtipocal = _idtipocal;
            this._fechaini = _fechaini;
            this._fechafin = _fechafin;
            this._sexo = _sexo;
            this._operacionpadre = _operacionpadre;
            this._idtipoplan = idtipoplan;


        }

        public ui_diassubsi()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void ui_diassubsi_Load(object sender, EventArgs e)
        {
            this.Text = "Días Subsidiados del Periodo " + this._messem + "/" + this._anio + "  " + "Del " + this._fechaini + " al " + this._fechafin;
            MaesGen maesgen = new MaesGen();
            maesgen.listaDetMaesGen("030", cmbTipo, "B");
            txtEmpleador.Text = this._empleador;
            txtCodigoInterno.Text = this._idperplan;
            txtNombres.Text = this._nombretrabajador;
            ui_ListaDiasSubsi();

        }

        private void ui_ListaDiasSubsi()
        {


            Funciones funciones = new Funciones();
            string idcia = this._idcia;
            string idperplan = this._idperplan;
            string anio = this._anio;
            string messem = this._messem;
            string idtipoper = this._idtipoper;
            string idtipocal = this._idtipocal;
            string tipo = "S";


            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();
            string query = "select B.desmaesgen,A.dias,A.fechaini,A.fechafin,A.citt,A.diascitt,A.motivo,A.iddiassubsi from diassubsi A left join maesgen B on A.motivo=B.clavemaesgen and B.idmaesgen='030' where idcia='" + @idcia + "' and idperplan='" + @idperplan + "' and anio='" + @anio + "' and messem='" + @messem + "' and idtipoper='" + @idtipoper + "' and idtipocal='" + @idtipocal + "' and tipo='" + @tipo + "';";

            try
            {
                using (SqlDataAdapter myDataAdapter = new SqlDataAdapter(query, conexion))
                {
                    DataSet myDataSet = new DataSet();
                    myDataAdapter.Fill(myDataSet, "tblDiasSubsi");
                    funciones.formatearDataGridView(dgvdetalle);
                    dgvdetalle.DataSource = myDataSet.Tables["tblDiasSubsi"];
                    dgvdetalle.Columns[0].HeaderText = "Tipo en Dias Subsi.";
                    dgvdetalle.Columns[1].HeaderText = "Días Subsi.";
                    dgvdetalle.Columns[2].HeaderText = "F.Ini.";
                    dgvdetalle.Columns[3].HeaderText = "F.Fin.";
                    dgvdetalle.Columns[4].HeaderText = "CITT";
                    dgvdetalle.Columns[5].HeaderText = "Días Subsi. CITT";

                    dgvdetalle.Columns["motivo"].Visible = false;
                    dgvdetalle.Columns["iddiassubsi"].Visible = false;

                    dgvdetalle.Columns[0].Width = 250;
                    dgvdetalle.Columns[1].Width = 50;
                    dgvdetalle.Columns[2].Width = 75;
                    dgvdetalle.Columns[3].Width = 75;
                    dgvdetalle.Columns[4].Width = 100;
                    dgvdetalle.Columns[5].Width = 50;

                    double sum = 0;
                    int i = this.dgvdetalle.RowCount;
                    for (int x = 0; x < i; x++)
                    {
                        sum = sum + double.Parse(dgvdetalle[1, x].Value.ToString());
                    }

                    txtDias.Text = sum.ToString();

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }


            conexion.Close();
        }

        public void ui_limpiarForm()
        {

            cmbTipo.Text = string.Empty;
            cmbTipo.Enabled = false;
            txtCITT.Clear();
            txtCITT.Enabled = false;
            txtIdDiasSubsi.Clear();
            txtInicio.Clear();
            txtInicio.Enabled = false;
            txtFin.Clear();
            txtFin.Enabled = false;
            txtDiasSubsi.Clear();
            txtDiasSubsi.Enabled = false;
            txtDiasSubsiCITT.Clear();
            this._operacion = string.Empty;

        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            ((ui_upddatosplanilla)FormPadre).txtDiasSubsi.Text = txtDias.Text;

            if (this._operacionpadre.Equals("EDITAR"))
            {
                ((ui_upddatosplanilla)FormPadre).ui_actualizar();
            }

            Close();
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            ui_limpiarForm();
            this._operacion = "AGREGAR";
            cmbTipo.Enabled = true;
            txtCITT.Enabled = true;
            txtInicio.Enabled = true;
            txtFin.Enabled = true;
            txtDiasSubsi.Enabled = true;
            cmbTipo.Focus();
        }

        private void btnGrabar_Click(object sender, EventArgs e)
        {

            try
            {
                Funciones funciones = new Funciones();
                DiasSubsi diassubsi = new DiasSubsi();
                MaesGen maesgen = new MaesGen();
                UtileriasFechas utileriasfechas = new UtileriasFechas();

                string idcia = this._idcia;
                string idtipoper = this._idtipoper;
                string anio = this._anio;
                string messem = this._messem;
                string idtipocal = this._idtipocal;
                string idperplan = this._idperplan;
                string idtipoplan = this._idtipoplan;
                string fechaini = txtInicio.Text;
                string fechafin = txtFin.Text;
                string tipo = "S";
                string motivo = funciones.getValorComboBox(cmbTipo, 4);
                string citt = txtCITT.Text.Trim();
                string iddiassubsi = txtIdDiasSubsi.Text;
                string valorValida = "G";

                if (cmbTipo.Text == string.Empty && valorValida == "G")
                {
                    valorValida = "B";
                    MessageBox.Show("No ha seleccionado Tipo en días subsidiados", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    cmbTipo.Focus();
                }

                if (txtCITT.Text == string.Empty && valorValida == "G")
                {
                    valorValida = "B";
                    MessageBox.Show("CITT no ingresado", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtCITT.Focus();
                }


                if (cmbTipo.Text != string.Empty && valorValida == "G")
                {
                    string resultado = maesgen.verificaComboBoxMaesGen("030", cmbTipo.Text.Trim());
                    if (resultado.Trim() == string.Empty)
                    {
                        valorValida = "B";
                        MessageBox.Show("Dato incorrecto en Tipo en días subsidiados", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        cmbTipo.Focus();
                    }
                }

                if (this._sexo == "M" && valorValida == "G" && motivo == "22")
                {
                    valorValida = "B";
                    MessageBox.Show("No puede registrar días subsidiados por Maternidad. El trabajador es de sexo masculino", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    cmbTipo.Focus();
                }

                if (UtileriasFechas.IsDate(txtInicio.Text) == false && valorValida == "G")
                {
                    valorValida = "B";
                    MessageBox.Show("Fecha de Inicio no válida", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtInicio.Focus();
                }

                if (UtileriasFechas.IsDate(txtFin.Text) == false && valorValida == "G")
                {
                    valorValida = "B";
                    MessageBox.Show("Fecha de Fin no válida", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtFin.Focus();
                }

                if (UtileriasFechas.compararFecha(txtFin.Text, "<", txtInicio.Text) && valorValida == "G")
                {
                    valorValida = "B";
                    MessageBox.Show("Fecha de Fin no válida", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtFin.Focus();
                }

                if (int.Parse(txtDiasSubsi.Text) <= 0 && valorValida == "G")
                {
                    valorValida = "B";
                    MessageBox.Show("Dato incorrecto en días subsidiados", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtDiasSubsi.Focus();
                }

                txtDiasSubsiCITT.Text = Convert.ToString(utileriasfechas.diferenciaEntreFechas(txtInicio.Text, txtFin.Text));
                int dias = int.Parse(txtDiasSubsi.Text);
                int diascitt = int.Parse(txtDiasSubsiCITT.Text);

                if (int.Parse(txtDiasSubsi.Text) > int.Parse(txtDiasSubsiCITT.Text) && valorValida == "G")
                {
                    valorValida = "B";
                    MessageBox.Show("La cantidad de días subsidiados no puede ser mayor a " + txtDiasSubsiCITT.Text.Trim(), "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtDiasSubsi.Focus();
                }



                if (valorValida.Equals("G"))
                {

                    diassubsi.setDiasSubsi(idperplan, idcia, anio, messem, idtipoper, idtipocal, tipo, motivo, citt, fechaini, fechafin, dias, diascitt, idtipoplan, iddiassubsi);
                    diassubsi.actualizarDiasSubsi(this._operacion);
                    ui_limpiarForm();
                    ui_ListaDiasSubsi();

                }

            }
            catch (FormatException ex)
            {

                MessageBox.Show(ex.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void txtInicio_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                if (UtileriasFechas.IsDate(txtInicio.Text))
                {

                    if (UtileriasFechas.compararFecha(txtInicio.Text, ">=", this._fechaini) && UtileriasFechas.compararFecha(txtInicio.Text, "<=", this._fechafin))
                    {


                        txtFin.Text = txtInicio.Text;
                        UtileriasFechas utileriasfechas = new UtileriasFechas();
                        txtDiasSubsi.Text = Convert.ToString(utileriasfechas.diferenciaEntreFechas(txtInicio.Text, txtFin.Text));
                        txtDiasSubsiCITT.Text = Convert.ToString(utileriasfechas.diferenciaEntreFechas(txtInicio.Text, txtFin.Text));
                        e.Handled = true;
                        txtFin.Focus();

                    }
                    else
                    {

                        MessageBox.Show("La fecha de inicio no se encuentra dentro del rango del periodo", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        e.Handled = true;
                        txtInicio.Clear();
                        txtFin.Clear();
                        txtDiasSubsi.Clear();
                        txtDiasSubsiCITT.Clear();
                        txtInicio.Focus();
                    }
                }
                else
                {
                    MessageBox.Show("Fecha de Inicio no válida", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    e.Handled = true;
                    txtInicio.Clear();
                    txtFin.Clear();
                    txtDiasSubsi.Clear();
                    txtDiasSubsiCITT.Clear();
                    txtInicio.Focus();

                }
            }


        }

        private void txtFin_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                if (UtileriasFechas.IsDate(txtFin.Text))
                {
                    if (UtileriasFechas.compararFecha(txtFin.Text, ">=", this._fechaini) && UtileriasFechas.compararFecha(txtFin.Text, "<=", this._fechafin))
                    {

                        if (UtileriasFechas.compararFecha(txtInicio.Text, "<=", txtFin.Text))
                        {
                            UtileriasFechas utileriasfechas = new UtileriasFechas();
                            txtDiasSubsi.Text = Convert.ToString(utileriasfechas.diferenciaEntreFechas(txtInicio.Text, txtFin.Text));
                            txtDiasSubsiCITT.Text = Convert.ToString(utileriasfechas.diferenciaEntreFechas(txtInicio.Text, txtFin.Text));
                            e.Handled = true;
                            txtDiasSubsi.Focus();
                        }
                        else
                        {
                            MessageBox.Show("Fecha de Fin no puede ser menor que la Fecha de Inicio", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            e.Handled = true;
                            txtFin.Clear();
                            txtDiasSubsi.Clear();
                            txtDiasSubsiCITT.Clear();
                            txtFin.Focus();
                        }

                    }
                    else
                    {

                        MessageBox.Show("La fecha de inicio no se encuentra dentro del rango del periodo", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        e.Handled = true;
                        txtFin.Clear();
                        txtDiasSubsi.Clear();
                        txtDiasSubsiCITT.Clear();
                        txtFin.Focus();
                    }
                }
                else
                {
                    MessageBox.Show("Fecha de Fin no válida", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    e.Handled = true;
                    txtFin.Clear();
                    txtDiasSubsi.Clear();
                    txtDiasSubsiCITT.Clear();
                    txtFin.Focus();

                }
            }

        }

        private void cmbTipo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                if (cmbTipo.Text != String.Empty)
                {
                    MaesGen maesgen = new MaesGen();
                    maesgen.validarDetMaesGen("030", cmbTipo, cmbTipo.Text);
                }
                e.Handled = true;
                txtCITT.Focus();

            }
        }

        private void txtCITT_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {

                e.Handled = true;
                txtInicio.Focus();


            }
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {

            MaesGen maesgen = new MaesGen();
            Int32 selectedCellCount = dgvdetalle.GetCellCount(DataGridViewElementStates.Selected);
            if (selectedCellCount > Convert.ToInt32(0))
            {
                this._operacion = "EDITAR";
                cmbTipo.Enabled = false;
                txtCITT.Enabled = true;
                txtInicio.Enabled = true;
                txtFin.Enabled = true;
                txtDiasSubsi.Enabled = true;
                maesgen.consultaDetMaesGen("030", dgvdetalle.Rows[dgvdetalle.SelectedCells[1].RowIndex].Cells[6].Value.ToString(), cmbTipo);
                txtDiasSubsiCITT.Text = dgvdetalle.Rows[dgvdetalle.SelectedCells[1].RowIndex].Cells[5].Value.ToString();
                txtCITT.Text = dgvdetalle.Rows[dgvdetalle.SelectedCells[1].RowIndex].Cells[4].Value.ToString();
                txtInicio.Text = dgvdetalle.Rows[dgvdetalle.SelectedCells[1].RowIndex].Cells[2].Value.ToString();
                txtFin.Text = dgvdetalle.Rows[dgvdetalle.SelectedCells[1].RowIndex].Cells[3].Value.ToString();
                txtDiasSubsi.Text = dgvdetalle.Rows[dgvdetalle.SelectedCells[1].RowIndex].Cells[1].Value.ToString();
                txtIdDiasSubsi.Text = dgvdetalle.Rows[dgvdetalle.SelectedCells[1].RowIndex].Cells[7].Value.ToString();

                txtCITT.Focus();


            }
            else
            {
                MessageBox.Show("No ha seleccionado registro a Editar", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            DiasSubsi diassubsi = new DiasSubsi();
            Int32 selectedCellCount = dgvdetalle.GetCellCount(DataGridViewElementStates.Selected);
            if (selectedCellCount > Convert.ToInt32(0))
            {

                DialogResult resultado = MessageBox.Show("¿Desea eliminar el Tipo en Días Subsidiados?",
                "Consulta Importante",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);
                if (resultado == DialogResult.Yes)
                {
                    string motivo = dgvdetalle.Rows[dgvdetalle.SelectedCells[1].RowIndex].Cells[6].Value.ToString();
                    string iddiassubsi = dgvdetalle.Rows[dgvdetalle.SelectedCells[1].RowIndex].Cells[7].Value.ToString();

                    diassubsi.eliminarDiasSubsi(this._idperplan, this._idcia, this._anio, this._messem, this._idtipoper, this._idtipocal, "S", motivo, this._idtipoplan, iddiassubsi);
                    ui_ListaDiasSubsi();
                }


            }
            else
            {
                MessageBox.Show("No ha seleccionado registro a Eliminar", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }

        }

        private void txtInicio_TextChanged(object sender, EventArgs e)
        {

        }
    }
}