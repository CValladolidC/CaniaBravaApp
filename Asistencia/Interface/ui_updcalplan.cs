using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CaniaBrava
{
    public partial class ui_updcalplan : Form
    {
        string _idtipocal;

        private Form FormPadre;

        public Form _FormPadre
        {
            get { return FormPadre; }
            set { FormPadre = value; }
        }

        public ui_updcalplan()
        {
            InitializeComponent();
        }

        public void ui_ActualizaComboBox()
        {

            MaesGen maesgen = new MaesGen();
            maesgen.listaDetMaesGen("035", cmbMes, "");
            maesgen.listaDetMaesGen("035", cmbMesPDT, "");
          
        }

        public void newCalPlan(string destipoper,string idtipoper, string idtipocal)
        {
            txtOperacion.Text= "AGREGAR";
            this._idtipocal = idtipocal;
            txtTipo.Text = destipoper;
            txtMesSem.Enabled = true;
            txtMesSem.Clear();
            txtAnio.Enabled = true;
            txtAnio.Text=Convert.ToString(DateTime.Now.Year);
            txtAnioPDT.Text = Convert.ToString(DateTime.Now.Year);
            dtpFechaIni.Text = DateTime.Now.ToString("dd/MM/yyyy");
            dtpFechaFin.Text = DateTime.Now.ToString("dd/MM/yyyy");
            chkSalda.Checked = false;

            if (idtipoper.Equals("O"))
            {
                txtDiasDom.Text = "1";
                txtDiasDom.Enabled = true;
            }
            else
            {
                txtDiasDom.Text = "0";
                txtDiasDom.Enabled = false;
            }
            
            txtMesSem.Focus();
        }

        public void loadCalPlan(string destipoper,string idtipoper, string messem, string mes,string anio, string fechaini, string fechafin, string idtipocal,string mespdt,string aniopdt,string diasdom,string saldaquinta)
        {
            Funciones funciones = new Funciones();
            MaesGen maesgen = new MaesGen();
            txtOperacion.Text = "EDITAR";
            this._idtipocal = idtipocal;
            txtTipo.Text = destipoper;
            txtMesSem.Enabled = false;
            txtMesSem.Text = messem;
            txtAnio.Enabled = false;
            maesgen.consultaDetMaesGen("035", mes, cmbMes);
            maesgen.consultaDetMaesGen("035", mespdt, cmbMesPDT);
            txtAnio.Text = anio;
            txtAnioPDT.Text = aniopdt;
            dtpFechaIni.Text = fechaini;
            dtpFechaFin.Text = fechafin;
            if (idtipoper.Equals("O"))
            {
                txtDiasDom.Text = diasdom;
                txtDiasDom.Enabled = true;
            }
            else
            {
                txtDiasDom.Text = "0";
                txtDiasDom.Enabled = false;
            }

            if (saldaquinta.Equals("S"))
            {
                chkSalda.Checked = true;
            }
            else
            {
                chkSalda.Checked = false;
            }
            
            dtpFechaIni.Focus();

        }

        private void txtMesSem_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void ui_updcalplan_Load(object sender, EventArgs e)
        {
            ui_ActualizaComboBox();
            
        }

        private void txtMesSem_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                e.Handled = true;
                cmbMes.Focus();
            }
        }

        private void txtAnio_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                e.Handled = true;
                dtpFechaIni.Focus();
            }
        }

        private void txtFechaIni_KeyPress(object sender, KeyPressEventArgs e)
        {

            

            
        }

        private void txtFechaFin_KeyPress(object sender, KeyPressEventArgs e)
        {
           
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            try
            {

                Funciones funciones = new Funciones();
                MaesGen maesgen = new MaesGen();
                string operacion = txtOperacion.Text.Trim();
                string idtipoper = txtTipo.Text.Substring(0, 1);
                string messem = txtMesSem.Text.Trim();
                string anio = txtAnio.Text.Trim();
                string fechaini = dtpFechaIni.Text;
                string fechafin = dtpFechaFin.Text;
                string mes = funciones.getValorComboBox(cmbMes, 2);
                string idtipocal = this._idtipocal;
                string aniopdt = txtAnioPDT.Text.Trim();
                string mespdt = funciones.getValorComboBox(cmbMesPDT, 2);
                string saldaquinta;

                if (chkSalda.Checked)
                {
                    saldaquinta = "S";
                }
                else
                {
                    saldaquinta = "N";
                }

                int diasdom = int.Parse(txtDiasDom.Text);              
                GlobalVariables variables = new GlobalVariables();
                string idcia = variables.getValorCia();
                string valorValida = "G";

                if (messem == string.Empty && valorValida == "G")
                {
                    valorValida = "B";
                    MessageBox.Show("No ha especificado Mes / Sem.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    txtMesSem.Focus();

                }
                
                if (cmbMes.Text == string.Empty && valorValida == "G")
                {
                    valorValida = "B";
                    MessageBox.Show("No ha seleccionado Mes", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    cmbMes.Focus();
                }

                if (cmbMes.Text != string.Empty && valorValida == "G")
                {
                    string resultado = maesgen.verificaComboBoxMaesGen("035", cmbMes.Text.Trim());
                    if (resultado.Trim() == string.Empty)
                    {
                        valorValida = "B";
                        MessageBox.Show("Dato incorrecto en Mes", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        cmbMes.Focus();
                    }
                }
                
                 if (anio == String.Empty && valorValida == "G")
                    {
                     valorValida = "B";   
                     MessageBox.Show("No ha especificado Año", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                     txtAnio.Focus();
                 }


                 if (cmbMesPDT.Text == string.Empty && valorValida == "G")
                 {
                     valorValida = "B";
                     MessageBox.Show("No ha seleccionado Mes PDT", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                     cmbMesPDT.Focus();
                 }

                 if (cmbMesPDT.Text != string.Empty && valorValida == "G")
                 {
                     string resultado = maesgen.verificaComboBoxMaesGen("035", cmbMesPDT.Text.Trim());
                     if (resultado.Trim() == string.Empty)
                     {
                         valorValida = "B";
                         MessageBox.Show("Dato incorrecto en Mes PDT", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                         cmbMesPDT.Focus();
                     }
                 }

                 if (aniopdt == String.Empty && valorValida == "G")
                 {
                     valorValida = "B";
                     MessageBox.Show("No ha especificado Año PDT", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                     txtAnioPDT.Focus();
                 }

                if (valorValida.Equals("G"))
                 {
                     if (UtileriasFechas.IsDate(dtpFechaIni.Text))
                     {

                         if (UtileriasFechas.IsDate(dtpFechaFin.Text))
                         {

                             if (UtileriasFechas.compararFecha(dtpFechaIni.Text, "<=", dtpFechaFin.Text))
                             {
                                 CalPlan updcalplan = new CalPlan();
                                 updcalplan.actualizaCalPlan(operacion, mes, anio, messem, idtipoper, idtipocal, idcia, fechaini, fechafin, mespdt, aniopdt, diasdom,saldaquinta);
                                 ((ui_calplan)FormPadre).btnActualizar.PerformClick();
                                 this.Close();

                            }
                             else
                             {
                                 MessageBox.Show("La Fecha de Fin no puede ser menor que la Fecha de Inicio", "Avios", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                 dtpFechaFin.Focus();
                             }

                         }
                         else
                         {
                             MessageBox.Show("Fecha de Fin no válida", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                             dtpFechaFin.Focus();
                         }
                     }
                     else
                     {
                         MessageBox.Show("Fecha de Inicio no válida", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                         dtpFechaIni.Focus();
                     }

                 }

            }
            catch (FormatException ex)
            {
                MessageBox.Show(ex.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtFechaIni_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }

        private void txtAnioPDT_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                e.Handled = true;
                toolStripForm.Items[0].Select();
                toolStripForm.Focus();
            }
        }

        private void txtDiasDom_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                try
                {
                    int diasdom = int.Parse(txtDiasDom.Text);
                    e.Handled = true;
                    cmbMesPDT.Focus();

                }

                catch (FormatException ex)
                {
                    MessageBox.Show(ex.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    e.Handled = true;
                    txtDiasDom.Focus();
                }

            }
        }

        private void cmbMes_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                if (cmbMes.Text != String.Empty)
                {
                    MaesGen maesgen = new MaesGen();
                    maesgen.validarDetMaesGen("035", cmbMes, cmbMes.Text);
                }
                e.Handled = true;
                txtAnio.Focus();
            }
        }

        private void cmbMes_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void cmbMesPDT_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                if (cmbMesPDT.Text != String.Empty)
                {
                    MaesGen maesgen = new MaesGen();
                    maesgen.validarDetMaesGen("035", cmbMesPDT, cmbMesPDT.Text);
                }
                e.Handled = true;
                txtAnioPDT.Focus();
            }
        }
    }
}