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
    public partial class ui_updcompanias : Form
    {
        Funciones funciones = new Funciones();
        string _operacion;
        private Form FormPadre;

        public Form _FormPadre
        {
            get { return FormPadre; }
            set { FormPadre = value; }
        }

        private TextBox TextBoxActivo;

        public TextBox _TextBoxActivo
        {
            get { return TextBoxActivo; }
            set { TextBoxActivo = value; }
        }

        public ui_updcompanias()
        {
            InitializeComponent();

            if (funciones.VersionAssembly()) Application.ExitThread();
        }

        private void cmbTipo_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            string operacion = this._operacion;
            string idCia = txtCodigo.Text.Trim();
            string desCia = txtRazon.Text.Trim();
            string dirCia = txtDireccion.Text.Trim();
            string rucCia = txtRuc.Text.Trim();
            string repCia = txtRepresentante.Text.Trim();
            string regPatCia = txtRegistro.Text.Trim();
            string shortCia = txtCorto.Text.Trim();
            string typeCia = funciones.getValorComboBox(cmbTipo, 1);
            string stateCia = funciones.getValorComboBox(cmbEstado, 1);
            string codaux = txtCodAux.Text;
            string logo = txtRuta.Text;
            string reglab;
            string actividad;
            string desSi;
            string desNo;

            if (txtActividad.Text.Trim() != string.Empty)
            {
                actividad = txtActividad.Text.Substring(0, 6).Trim();
            }
            else
            {
                actividad = string.Empty;
            }


            if (radioButtonSi.Checked)
                desSi = "1";
            else
                desSi = "0";



            if (radioButtonNo.Checked)
                desNo = "1";
            else
                desNo = "0";


            if (radioButtonPri.Checked)
            {
                reglab = "1";
            }
            else
            {
                if (radioButtonPub.Checked)
                {
                    reglab = "2";
                }
                else
                {
                    reglab = "3";
                }

            }


            string valorValida = "G";

            if (txtCodigo.Text == string.Empty && valorValida == "G")
            {
                valorValida = "B";
                MessageBox.Show("No ha ingresado Código", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtCodigo.Focus();
            }

            if (txtRazon.Text == string.Empty && valorValida == "G")
            {
                valorValida = "B";
                MessageBox.Show("No ha ingresado Razón Social", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtRazon.Focus();
            }

            if (txtDireccion.Text == string.Empty && valorValida == "G")
            {
                valorValida = "B";
                MessageBox.Show("No ha ingresado Domicilio Fiscal", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtDireccion.Focus();
            }

            if (txtRuc.Text == string.Empty && valorValida == "G")
            {
                valorValida = "B";
                MessageBox.Show("No ha ingresado R.U.C.", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtRuc.Focus();
            }

            if (txtRepresentante.Text == string.Empty && valorValida == "G")
            {
                valorValida = "B";
                MessageBox.Show("No ha ingresado Representante Legal", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtRepresentante.Focus();
            }

            if (txtRegistro.Text == string.Empty && valorValida == "G")
            {
                valorValida = "B";
                MessageBox.Show("No ha ingresado Nro. Registro Patronal", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtRegistro.Focus();
            }

            if (txtCorto.Text == string.Empty && valorValida == "G")
            {
                valorValida = "B";
                MessageBox.Show("No ha ingresado nombre corto", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtCorto.Focus();
            }

            if (typeCia == string.Empty && valorValida == "G")
            {
                valorValida = "B";
                MessageBox.Show("No ha especificado Tipo", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                cmbTipo.Focus();
            }

            if (typeCia != "N" && typeCia != "J" && valorValida == "G")
            {
                valorValida = "B";
                MessageBox.Show("Dato incorrecto en Tipo", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                cmbTipo.Focus();
            }

            if (actividad == string.Empty && valorValida == "G")
            {
                valorValida = "B";
                MessageBox.Show("No ha seleccionado Actividad", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtActividad.Focus();
            }


            if (stateCia == string.Empty && valorValida == "G")
            {
                valorValida = "B";
                MessageBox.Show("No ha especificado Estado", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                cmbEstado.Focus();
            }

            if (stateCia != "V" && stateCia != "A" && valorValida == "G")
            {
                valorValida = "B";
                MessageBox.Show("Dato incorrecto en Estado", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                cmbEstado.Focus();
            }


            if (valorValida.Equals("G"))
            {
                CiaFile updciafile = new CiaFile();
                updciafile.actualizarCiafile(operacion, idCia, desCia, rucCia, repCia, shortCia, stateCia, typeCia,
                    dirCia, regPatCia, desSi, desNo, actividad, reglab, codaux, logo);
                ((ui_companias)FormPadre).btnActualizar.PerformClick();
                this._operacion = "EDITAR";
                tabPageEX2.Enabled = true;
                //tabPageEX4.Enabled = true;

                MessageBox.Show("Información registrada correctamente", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }




        }

        public void NewCompania()
        {
            this._operacion = "AGREGAR";
            txtCodigo.Enabled = true;
            txtRuc.Enabled = true;
            txtCodigo.Clear();
            txtCodAux.Clear();
            txtRazon.Clear();
            txtDireccion.Clear();
            txtRepresentante.Clear();
            txtRuc.Clear();
            txtRegistro.Clear();
            txtCorto.Clear();
            txtActividad.Clear();
            cmbTipo.Text = "J        JURIDICA";
            cmbEstado.Text = "V         VIGENTE";
            txtRuta.Clear();
            tabControlEmp.SelectTab(0);
            txtCodigo.Focus();
        }

        public void LoadCompania(string idcia)
        {
            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();
            string query = "SELECT * FROM ciafile where idcia='" + @idcia + "';";

            try
            {
                SqlCommand myCommand = new SqlCommand(query, conexion);
                SqlDataReader myReader = myCommand.ExecuteReader();
                if (myReader.Read())
                {
                    this._operacion = "EDITAR";
                    txtCodigo.Enabled = false;
                    txtRuc.Enabled = false;
                    tabPageEX2.Enabled = true;

                    txtCodigo.Text = myReader["idcia"].ToString();
                    txtCodAux.Text = myReader["codaux"].ToString();
                    txtRazon.Text = myReader["descia"].ToString();
                    txtDireccion.Text = myReader["dircia"].ToString();
                    txtRepresentante.Text = myReader["repcia"].ToString();
                    txtRegistro.Text = myReader["regpatcia"].ToString();
                    txtCorto.Text = myReader["shortcia"].ToString();
                    txtRuc.Text = myReader["ruccia"].ToString();
                    MaesPdt maespdt = new MaesPdt();
                    txtActividad.Text = myReader["actividad"] + "  " + maespdt.ui_getDatosMaesPdt("R1", myReader["actividad"].ToString(), "DESCRIPCION");
                    txtRuta.Text = myReader["logo"].ToString();
                    ui_cargaimagen(myReader["logo"].ToString());

                    if (myReader["dessi"].Equals("1"))
                    {
                        radioButtonSi.Checked = true;
                    }
                    else
                        radioButtonSi.Checked = false;


                    if (myReader["desno"].Equals("1"))
                    {
                        radioButtonNo.Checked = true;
                    }
                    else
                        radioButtonNo.Checked = false;


                    if (myReader["reglab"].Equals("1"))
                    {
                        radioButtonPri.Checked = true;
                        radioButtonPub.Checked = false;
                        radioButtonOtras.Checked = false;

                    }
                    else
                    {
                        if (myReader["reglab"].Equals("2"))
                        {
                            radioButtonPub.Checked = true;
                            radioButtonPri.Checked = false;
                            radioButtonOtras.Checked = false;
                        }
                        else
                        {
                            radioButtonPub.Checked = false;
                            radioButtonPri.Checked = false;
                            radioButtonOtras.Checked = true;
                        }
                    }

                    switch (myReader["typecia"].ToString())
                    {
                        case "J":
                            cmbTipo.Text = "J        JURIDICA";
                            break;
                        case "N":
                            cmbTipo.Text = "N        NATURAL";
                            break;
                        default:
                            cmbTipo.Text = "J        JURIDICA";
                            break;
                    }

                    switch (myReader["statecia"].ToString())
                    {
                        case "A":
                            cmbEstado.Text = "A        ANULADO";
                            break;
                        case "V":
                            cmbEstado.Text = "V         VIGENTE";
                            break;
                        default:
                            cmbEstado.Text = "V         VIGENTE";
                            break;
                    }

                    //tabPageEX4.Enabled = true;
                    tabControlEmp.SelectTab(0);
                    txtRazon.Focus();
                }

                myReader.Close();
                myCommand.Dispose();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }

            conexion.Close();
        }

        private void ui_updcompanias_Load(object sender, EventArgs e)
        {

            tabControlEmp.SelectTab(0);

        }

        public void ui_listarComboBox()
        {
            MaesGen maesgen = new MaesGen();
            maesgen.listaDetMaesGen("027", cmbTipoEsta, "B");
            //string query = "SELECT idtipoplan as clave,destipoplan as descripcion FROM tipoplan;";
            //funciones.listaComboBox(query, cmbTipoPlan, "B");
        }

        private void txtRazon_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtRazon_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                e.Handled = true;
                txtDireccion.Focus();
            }
        }

        private void txtRepresentante_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                e.Handled = true;
                txtRegistro.Focus();
            }
        }

        private void txtCorto_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                e.Handled = true;
                cmbTipo.Focus();
            }
        }

        private void cmbTipo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                if (cmbTipo.Text == String.Empty)
                {
                    MessageBox.Show("El campo no acepta valores vacíos", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    e.Handled = true;
                    txtRepresentante.Focus();

                }
                else
                {

                    switch (cmbTipo.Text.ToUpper().Substring(0, 1))
                    {
                        case "J":
                            cmbTipo.Text = "J        JURIDICA";
                            break;
                        case "N":
                            cmbTipo.Text = "N        NATURAL";
                            break;
                        default:
                            cmbTipo.Text = "J        JURIDICA";
                            break;
                    }

                    e.Handled = true;
                    txtRepresentante.Focus();
                }
            }
        }

        private void cmbEstado_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                if (cmbEstado.Text == String.Empty)
                {
                    MessageBox.Show("El campo no acepta valores vacíos", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    e.Handled = true;
                    cmbEstado.Focus();
                }
                else
                {
                    switch (cmbEstado.Text.ToUpper().Substring(0, 1))
                    {
                        case "A":
                            cmbEstado.Text = "A        ANULADO";
                            break;
                        case "V":
                            cmbEstado.Text = "V         VIGENTE";
                            break;
                        default:
                            cmbEstado.Text = "V         VIGENTE";
                            break;
                    }
                    e.Handled = true;
                    toolStripForm.Items[0].Select();
                    toolStripForm.Focus();
                }
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtCodigo_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtCodigo_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }

        private void txtCodigo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                String codigo = txtCodigo.Text.Trim();
                int longitud = codigo.Length;
                if (longitud == 1)
                {
                    txtCodigo.Text = '0' + txtCodigo.Text;
                }
                e.Handled = true;
                txtRuc.Focus();
            }
        }

        private void txtRuc_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                e.Handled = true;
                txtRazon.Focus();
            }
        }

        private void txtDireccion_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                e.Handled = true;
                cmbTipo.Focus();
            }
        }

        private void txtRegistro_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                e.Handled = true;
                txtCorto.Focus();

            }
        }

        private void tabPageEX2_Click(object sender, EventArgs e)
        {

        }

        private void tabPageEX1_Click(object sender, EventArgs e)
        {

        }

        private void txtRuc_TextChanged(object sender, EventArgs e)
        {
            string razon;
            razon = txtRuc.Text.Trim() + "   " + txtRazon.Text.Trim();
            lblNombre.Text = razon;
        }

        private void txtRazon_TextChanged_1(object sender, EventArgs e)
        {
            string razon;
            razon = txtRuc.Text.Trim() + "   " + txtRazon.Text.Trim();
            lblNombre.Text = razon;
        }

        private void radioButtonSi_CheckedChanged(object sender, EventArgs e)
        {
            if (this._operacion.Trim().Equals("EDITAR"))
            {
                
            }

        }

        private void radioButtonNo_CheckedChanged(object sender, EventArgs e)
        {
            if (this._operacion.Trim().Equals("EDITAR"))
            {
                ;
            }
        }

        private void txtNombreEsta_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                e.Handled = true;
                cmbTipoEsta.Focus();
            }
        }

        private void txtCodigoEsta_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                e.Handled = true;
                txtNombreEsta.Focus();
            }
        }

        private void btnNuevoEstAne_Click(object sender, EventArgs e)
        {
            txtOpeEsta.Text = "AGREGAR";
            txtCodigoEsta.Enabled = true;
            txtNombreEsta.Enabled = true;
            cmbTipoEsta.Enabled = true;
            radioButtonSiCR.Enabled = true;
            radioButtonNoCR.Enabled = true;
            txtCodigoEsta.Clear();
            txtNombreEsta.Clear();
            cmbTipoEsta.Text = string.Empty;
            radioButtonSiCR.Checked = false;
            radioButtonNoCR.Checked = true;
            txtCodigoEsta.Focus();
        }

        private void tabControlEX1_SelectedIndexChanging(object sender, Dotnetrix.Controls.TabPageChangeEventArgs e)
        {

        }

        private void btnGrabarEstAne_Click(object sender, EventArgs e)
        {
            if (this._operacion.Trim().Equals("EDITAR"))
            {
                try
                {
                    MaesGen maesgen = new MaesGen();
                    EstAne estane = new EstAne();

                    string operacion = txtOpeEsta.Text.Trim();
                    string idestane = txtCodigoEsta.Text.Trim();
                    string desestane = txtNombreEsta.Text.Trim();
                    string tipoestane = funciones.getValorComboBox(cmbTipoEsta, 4);
                    string idcia = txtCodigo.Text.Trim();
                    string codemp = txtRuc.Text.Trim();
                    string riesgo;
                    if (radioButtonNoCR.Checked)
                    {
                        riesgo = "0";
                    }
                    else
                    {
                        riesgo = "1";
                    }
                    
                    string valorValida = "G";

                    if (txtCodigoEsta.Text == string.Empty && valorValida == "G")
                    {
                        valorValida = "B";
                        MessageBox.Show("No ha ingresado Código de Establecimiento", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txtCodigoEsta.Focus();
                    }

                    if (txtNombreEsta.Text == string.Empty && valorValida == "G")
                    {
                        valorValida = "B";
                        MessageBox.Show("No ha ingresado Nombre de Establecimiento", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txtNombreEsta.Focus();
                    }
                    
                    if (cmbTipoEsta.Text == string.Empty && valorValida == "G")
                    {
                        valorValida = "B";
                        MessageBox.Show("No ha seleccionado Tipo de Establecimiento", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        cmbTipoEsta.Focus();
                    }

                    if (cmbTipoEsta.Text != string.Empty && valorValida == "G")
                    {
                        string resultado = maesgen.verificaComboBoxMaesGen("027", cmbTipoEsta.Text.Trim());
                        if (resultado.Trim() == string.Empty)
                        {
                            valorValida = "B";
                            MessageBox.Show("Dato incorrecto en Tipo de Establecimiento", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            tabControlEmp.SelectTab(1);
                            cmbTipoEsta.Focus();
                        }
                    }
                    
                    if (valorValida.Equals("G"))
                    {
                        estane.setEstAne(idestane, codemp, desestane, tipoestane, idcia, riesgo);
                        estane.actualizarEstAne(operacion);
                        ui_listaEstAne(idcia, codemp);
                        ui_limpiarEstAne();
                    }
                }
                catch (FormatException ex)
                {
                    MessageBox.Show(ex.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Primero debe de grabar la información del Empleador", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void ui_limpiarEstAne()
        {
            txtOpeEsta.Clear();
            txtCodigoEsta.Enabled = false;
            txtNombreEsta.Enabled = false;
            cmbTipoEsta.Enabled = false;
            radioButtonSiCR.Enabled = false;
            radioButtonNoCR.Enabled = false;
            txtCodigoEsta.Clear();
            txtNombreEsta.Clear();
            cmbTipoEsta.Text = string.Empty;
            radioButtonSiCR.Checked = false;
            radioButtonNoCR.Checked = true;

        }

        public void ui_limpiarEmp()
        {

        }

        public void ui_listaEstAne(string idcia, string codemp)
        {
            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();
            string query = "select A.idestane,B.desmaesgen,A.desestane,";
            query = query + " CASE A.riesgo WHEN '1' THEN 'SI' WHEN '0' THEN 'NO' END,";
            query = query + " A.riesgo,A.tipoestane,A.codemp,A.idciafile from estane A ";
            query = query + " left join maesgen B on A.tipoestane=B.clavemaesgen and B.idmaesgen='027' ";
            query = query + " where codemp='" + @codemp + "' and idciafile='" + @idcia + "' order by A.idestane asc;";
            try
            {
                using (SqlDataAdapter myDataAdapter = new SqlDataAdapter(query, conexion))
                {
                    DataSet myDataSet = new DataSet();
                    myDataAdapter.Fill(myDataSet, "tblEstAne");
                    funciones.formatearDataGridView(dgvEstAne);

                    dgvEstAne.DataSource = myDataSet.Tables["tblEstAne"];
                    dgvEstAne.Columns[0].HeaderText = "Código Estab.";
                    dgvEstAne.Columns[1].HeaderText = "Tipo de Establecimiento";
                    dgvEstAne.Columns[2].HeaderText = "Denominación";
                    dgvEstAne.Columns[3].HeaderText = "¿Centro de Riesgo?";

                    dgvEstAne.Columns["tipoestane"].Visible = false;
                    dgvEstAne.Columns["codemp"].Visible = false;
                    dgvEstAne.Columns["idciafile"].Visible = false;
                    dgvEstAne.Columns["riesgo"].Visible = false;

                    dgvEstAne.Columns[0].Width = 100;
                    dgvEstAne.Columns[1].Width = 150;
                    dgvEstAne.Columns[2].Width = 280;
                    dgvEstAne.Columns[3].Width = 70;

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }

            conexion.Close();
        }

        public void ui_listaRegLab(string idcia)
        {
            //SqlConnection conexion = new SqlConnection();
            //conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            //conexion.Open();
            //string query = "select A.idtipoplan,B.destipoplan,A.nppmemp,A.nppmobre,";
            //query = query + " A.idcia from reglabcia A left join tipoplan B on A.idtipoplan=B.idtipoplan ";
            //query = query + " where idcia='" + @idcia + "' order by A.idtipoplan asc;";
            //try
            //{
            //    using (SqlDataAdapter myDataAdapter = new SqlDataAdapter(query, conexion))
            //    {
            //        DataSet myDataSet = new DataSet();
            //        myDataAdapter.Fill(myDataSet, "tblRegLab");
            //        funciones.formatearDataGridView(dgvRegLab);

            //        dgvRegLab.DataSource = myDataSet.Tables["tblRegLab"];
            //        dgvRegLab.Columns[0].HeaderText = "Código";
            //        dgvRegLab.Columns[1].HeaderText = "Régimen Laboral";
            //        dgvRegLab.Columns[2].HeaderText = "Nro. Periodos por Mes Empleados";
            //        dgvRegLab.Columns[3].HeaderText = "Nro. Periodos por Mes Obreros";

            //        dgvRegLab.Columns["idcia"].Visible = false;

            //        dgvRegLab.Columns[0].Width = 100;
            //        dgvRegLab.Columns[1].Width = 300;
            //        dgvRegLab.Columns[2].Width = 100;
            //        dgvRegLab.Columns[3].Width = 100;

            //    }

            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            //}

            //conexion.Close();
        }

        public void ui_listaEmp(string idcia)
        {
            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();
            string query = " select A.rucemp,A.razonemp,rtrim(A.actividad)+' '+rtrim(B.rp_cdescri) as actividad,A.fini,A.ffin,A.idciafile ";
            query = query + " from emplea A left join tgrpts B on A.actividad=B.rp_ccodigo and B.rp_cindice='R1' ";
            query = query + " where idciafile='" + @idcia + "' order by A.rucemp asc;";
            try
            {
                using (SqlDataAdapter myDataAdapter = new SqlDataAdapter(query, conexion))
                {
                    DataSet myDataSet = new DataSet();
                    myDataAdapter.Fill(myDataSet, "tblEstAneEmp");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }



            conexion.Close();


        }

        private void btnEditarEstAne_Click(object sender, EventArgs e)
        {


            MaesGen maesgen = new MaesGen();
            Int32 selectedCellCount = dgvEstAne.GetCellCount(DataGridViewElementStates.Selected);
            if (selectedCellCount > 0)
            {


                string idestane = dgvEstAne.Rows[dgvEstAne.SelectedCells[1].RowIndex].Cells[0].Value.ToString();
                string desestane = dgvEstAne.Rows[dgvEstAne.SelectedCells[1].RowIndex].Cells[2].Value.ToString();
                string tasa = dgvEstAne.Rows[dgvEstAne.SelectedCells[1].RowIndex].Cells[3].Value.ToString();
                string riesgo = dgvEstAne.Rows[dgvEstAne.SelectedCells[1].RowIndex].Cells[4].Value.ToString();
                string tipoestane = dgvEstAne.Rows[dgvEstAne.SelectedCells[1].RowIndex].Cells[5].Value.ToString();
                string idcia = dgvEstAne.Rows[dgvEstAne.SelectedCells[1].RowIndex].Cells[7].Value.ToString();

                txtOpeEsta.Text = "EDITAR";
                txtCodigoEsta.Text = idestane;
                txtCodigoEsta.Enabled = false;
                txtNombreEsta.Text = desestane;
                txtNombreEsta.Enabled = true;
                cmbTipoEsta.Enabled = true;
                radioButtonSiCR.Enabled = true;
                radioButtonNoCR.Enabled = true;

                maesgen.consultaDetMaesGen("027", tipoestane, cmbTipoEsta);
                if (riesgo.Equals("1"))
                {
                    radioButtonSiCR.Checked = true;
                    radioButtonNoCR.Checked = false;

                }
                else
                {
                    radioButtonSiCR.Checked = false;
                    radioButtonNoCR.Checked = true;

                }

                txtNombreEsta.Focus();


            }
            else
            {
                MessageBox.Show("No ha seleccionado registro a Editar", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void btnEliminarEstAne_Click(object sender, EventArgs e)
        {

            EstAne estane = new EstAne();
            Int32 selectedCellCount = dgvEstAne.GetCellCount(DataGridViewElementStates.Selected);
            if (selectedCellCount > 0)
            {

                string idestane = dgvEstAne.Rows[dgvEstAne.SelectedCells[1].RowIndex].Cells[0].Value.ToString();
                string desestane = dgvEstAne.Rows[dgvEstAne.SelectedCells[1].RowIndex].Cells[2].Value.ToString();
                string codemp = dgvEstAne.Rows[dgvEstAne.SelectedCells[1].RowIndex].Cells[6].Value.ToString();
                string idcia = dgvEstAne.Rows[dgvEstAne.SelectedCells[1].RowIndex].Cells[7].Value.ToString();


                DialogResult resultado = MessageBox.Show("¿Desea eliminar el Establecimiento " + desestane + "?",
                "Consulta Importante",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);
                if (resultado == DialogResult.Yes)
                {
                    estane.eliminarEstAne(idestane, codemp, idcia);
                    ui_listaEstAne(idcia, codemp);
                }

            }
            else
            {
                MessageBox.Show("No ha seleccionado registro a Eliminar", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void txtRucEmp_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                e.Handled = true;

            }
        }

        private void txtRazonEmp_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                e.Handled = true;
            }
        }

        private void radioButtonSiCR_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void radioButtonNoCR_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {

            string cadenaBusqueda = string.Empty;
            FiltrosMaestros filtros = new FiltrosMaestros();
            this._TextBoxActivo = txtActividad;
            filtros.filtrarTgRpts("ui_updcompanias", this, txtActividad, "R1", "");


        }

        private void btnActividadEmp_Click(object sender, EventArgs e)
        {
            string cadenaBusqueda = string.Empty;
            FiltrosMaestros filtros = new FiltrosMaestros();
        }

        private void btnEstablecimientos_Click(object sender, EventArgs e)
        {

        }

        private void btnTasaEsta_Click(object sender, EventArgs e)
        {
            string idciafile;
            string rucemp;
            string razonemp;
            string idestane;
            string desestane;
            string tipoestane;
            string riesgo;

            Int32 selectedCellCount = dgvEstAne.GetCellCount(DataGridViewElementStates.Selected);
            if (selectedCellCount > Convert.ToInt32(0))
            {
                rucemp = txtRuc.Text.Trim();
                razonemp = txtRazon.Text.Trim();
                idestane = dgvEstAne.Rows[dgvEstAne.SelectedCells[1].RowIndex].Cells[0].Value.ToString();
                tipoestane = dgvEstAne.Rows[dgvEstAne.SelectedCells[1].RowIndex].Cells[1].Value.ToString();
                desestane = dgvEstAne.Rows[dgvEstAne.SelectedCells[1].RowIndex].Cells[2].Value.ToString();
                riesgo = dgvEstAne.Rows[dgvEstAne.SelectedCells[1].RowIndex].Cells[4].Value.ToString();
                idciafile = dgvEstAne.Rows[dgvEstAne.SelectedCells[1].RowIndex].Cells[7].Value.ToString();

                if (riesgo.Equals("1"))
                {
                    ui_tasaest ui_tasaest = new ui_tasaest();
                    ui_tasaest._FormPadre = this;
                    ui_tasaest.ui_loadTasasEstAne(idciafile, rucemp, razonemp, idestane, desestane, tipoestane);
                    ui_tasaest.Activate();
                    ui_tasaest.BringToFront();
                    ui_tasaest.ShowDialog();
                    ui_tasaest.Dispose();
                }
                else
                {
                    MessageBox.Show("El establecimiento no es un Centro de Riesgo", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

            }
            else
            {
                MessageBox.Show("No ha seleccionado Establecimiento para actualizar % Tasas", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }

        }

        private void cmbTipoPlan_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                //if (cmbTipoPlan.Text != String.Empty)
                //{
                //    string clave = funciones.getValorComboBox(cmbTipoPlan, 3);
                //    string query = "SELECT idtipoplan as clave,destipoplan as descripcion FROM tipoplan where idtipoplan='" + @clave + "';";
                //    funciones.validarCombobox(query, cmbTipoPlan);
                //}
            }
        }

        private void cmbTipoEsta_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                if (cmbTipoEsta.Text != String.Empty)
                {
                    MaesGen maesgen = new MaesGen();
                    maesgen.validarDetMaesGen("027", cmbTipoEsta, cmbTipoEsta.Text);
                }


            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            //if (this._operacion.Trim().Equals("EDITAR"))
            //{
            //    RegLabCia reglabcia = new RegLabCia();

            //    string valorValida = "G";

            //    string idtipoplan = funciones.getValorComboBox(cmbTipoPlan, 3);
            //    string idcia = txtCodigo.Text.Trim();
            //    int nppmemp = (int)nudNroPerEmp.Value;
            //    int nppmobre = (int)nudNroPerObr.Value;

            //    if (cmbTipoPlan.Text.Trim() == string.Empty && valorValida == "G")
            //    {
            //        valorValida = "B";
            //        MessageBox.Show("No ha seleccionado Tipo de Planilla", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //        cmbTipoPlan.Focus();
            //    }

            //    if (cmbTipoPlan.Text != string.Empty && valorValida == "G")
            //    {
            //        string query = "SELECT idtipoplan as clave,destipoplan as descripcion FROM tipoplan where idtipoplan='" + @idtipoplan + "' order by 1 asc";
            //        string resultado = funciones.verificaItemComboBox(query, cmbTipoPlan);

            //        if (resultado.Trim() == string.Empty)
            //        {
            //            valorValida = "B";
            //            MessageBox.Show("Dato incorrecto en Tipo de Planilla", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //            cmbTipoPlan.Focus();
            //        }
            //    }

            //    if (nppmemp == 0)
            //    {
            //        valorValida = "B";
            //        MessageBox.Show("No ha especificado el Nro. de Periodos laborales en un Mes de los Empleados", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //        nudNroPerEmp.Focus();
            //    }

            //    if (nppmobre == 0)
            //    {
            //        valorValida = "B";
            //        MessageBox.Show("No ha especificado el Nro. de Periodos laborales en un Mes de los Obreros", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //        nudNroPerObr.Focus();
            //    }

            //    if (valorValida.Equals("G"))
            //    {
            //        reglabcia.actualizarRegLabCia(idtipoplan, idcia, nppmobre, nppmemp);
            //        ui_listaRegLab(idcia);
            //        cmbTipoPlan.Text = string.Empty;
            //        nudNroPerEmp.Value = 0;
            //        nudNroPerObr.Value = 0;
            //    }
            //}
            //else
            //{
            //    MessageBox.Show("Primero debe de grabar la información del Empleador", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}
        }

        private void tabPageEX4_Click(object sender, EventArgs e)
        {

        }

        private void button7_Click(object sender, EventArgs e)
        {
            //string idcia;
            //string idtipoplan;
            //string destipoplan;

            //RegLabCia reglabcia = new RegLabCia();
            //Int32 selectedCellCount = dgvRegLab.GetCellCount(DataGridViewElementStates.Selected);
            //if (selectedCellCount > 0)
            //{

            //    idtipoplan = dgvRegLab.Rows[dgvRegLab.SelectedCells[1].RowIndex].Cells[0].Value.ToString();
            //    destipoplan = dgvRegLab.Rows[dgvRegLab.SelectedCells[1].RowIndex].Cells[1].Value.ToString();
            //    idcia = dgvRegLab.Rows[dgvRegLab.SelectedCells[1].RowIndex].Cells[4].Value.ToString();

            //    DialogResult resultado = MessageBox.Show("¿Desea eliminar el Régimen Laboral " + destipoplan + "?",
            //    "Consulta Importante",
            //    MessageBoxButtons.YesNo,
            //    MessageBoxIcon.Question);
            //    if (resultado == DialogResult.Yes)
            //    {
            //        reglabcia.eliminarRegLabCia(idcia, idtipoplan);
            //        ui_listaRegLab(idcia);
            //    }
            //}
            //else
            //{
            //    MessageBox.Show("No ha seleccionado registro a Eliminar", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            //}
        }

        private void btnTipoCal_Click(object sender, EventArgs e)
        {
            //string idcia;
            //string idtipoplan;
            //string destipoplan;

            //Int32 selectedCellCount = dgvRegLab.GetCellCount(DataGridViewElementStates.Selected);
            //if (selectedCellCount > Convert.ToInt32(0))
            //{

            //    idtipoplan = dgvRegLab.Rows[dgvRegLab.SelectedCells[1].RowIndex].Cells[0].Value.ToString();
            //    destipoplan = dgvRegLab.Rows[dgvRegLab.SelectedCells[1].RowIndex].Cells[1].Value.ToString();
            //    idcia = dgvRegLab.Rows[dgvRegLab.SelectedCells[1].RowIndex].Cells[4].Value.ToString();

            //    ui_TipoCalendario ui_tipocalendario = new ui_TipoCalendario();
            //    ui_tipocalendario._FormPadre = this;
            //    ui_tipocalendario.ui_listaTipoCal(idcia, idtipoplan);

            //    ui_tipocalendario.ui_loadTipoCal(idcia, idtipoplan, destipoplan);
            //    ui_tipocalendario.Activate();
            //    ui_tipocalendario.BringToFront();
            //    ui_tipocalendario.ShowDialog();
            //    ui_tipocalendario.Dispose();
            //}
            //else
            //{
            //    MessageBox.Show("No ha Tipo de Planilla", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            //}
        }

        private void groupBox11_Enter(object sender, EventArgs e)
        {

        }

        private void ui_cargaimagen(string Dir)
        {
            if (Dir != string.Empty)
            {
                FileInfo fa = new FileInfo(Dir);
                if (fa.Exists)
                {
                    Bitmap Picture = new Bitmap(Dir);
                    pictureBoxImg.SizeMode = PictureBoxSizeMode.StretchImage;
                    pictureBoxImg.Image = (Image)Picture;
                }
            }
        }

        private void btnCargar_Click(object sender, EventArgs e)
        {
            OpenFileDialog Abrir = new OpenFileDialog();
            Abrir.Title = "Seleccionar imagen";
            Abrir.Filter = "JPG(*.jpg)|*.jpg|PNG(*.png)|*.png|GIF(*.gif)|*.gif|Todos(*.Jpg, *.Png, *.Gif, *.Tiff, *.Jpeg, *.Bmp)|*.Jpg; *.Png; *.Gif; *.Tiff; *.Jpeg; *.Bmp";
            Abrir.FilterIndex = 4;
            Abrir.RestoreDirectory = true;

            if (Abrir.ShowDialog() == DialogResult.OK)
            {
                string Dir = Abrir.FileName;
                ui_cargaimagen(Dir);
                txtRuta.Text = Dir;
            }
            else
            {
                txtRuta.Clear();
            }
        }

        private void txtFechaIni_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {

            }
        }

        private void txtFechaFin_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {

            }
        }

        private void btnEliminaEmplea_Click(object sender, EventArgs e)
        {

        }

        private void btnAgregaEmplea_Click(object sender, EventArgs e)
        {

        }

        private void btnEditaEmplea_Click(object sender, EventArgs e)
        {

        }

        private void btnGrabarEmplea_Click(object sender, EventArgs e)
        {
            if (this._operacion.Trim().Equals("EDITAR"))
            {
                try
                {
                    MaesGen maesgen = new MaesGen();
                    Emplea emplea = new Emplea();
                    string actividad;
                    
                    string valorValida = "G";
                }
                catch (FormatException ex)
                {
                    MessageBox.Show(ex.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Primero debe de grabar la información del Empleador", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}