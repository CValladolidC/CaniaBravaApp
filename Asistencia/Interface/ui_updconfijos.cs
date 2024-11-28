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
    public partial class ui_updconfijos : Form
    {
        string _idtipoper;
        string _idtipoplan;
        string _idtipocal;
        string _idcia;
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

        public ui_updconfijos()
        {
            InitializeComponent();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtValor_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                try
                {
                    float numero = float.Parse(txtValor.Text);
                    e.Handled = true;
                    toolStripForm.Items[0].Select();
                    toolStripForm.Focus();
              
                }
                catch (FormatException ex)
                {
                    MessageBox.Show(ex.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtValor.Clear();
                    txtValor.Focus();
                }

            }
        }
        
        private void txtCodigoInterno_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                string idcia = this._idcia;
                string idtipoplan=this._idtipoplan;
                string idtipoper = this._idtipoper;
                this._TextBoxActivo = txtCodigoInterno;
                string condicionAdicional = " and A.idtipoplan='" + @idtipoplan + "' ";
                string cadenaBusqueda = string.Empty;
                FiltrosMaestros filtros = new FiltrosMaestros();
                filtros.filtrarPerPlan("ui_updconfijos", this, txtCodigoInterno, idcia, idtipoper, "V", cadenaBusqueda, condicionAdicional);
            }
        }

        internal void ui_loadForm(string idperplan,string idconplan,string tipocalculo,float valor)
        {
            PerPlan perplan = new PerPlan();
            Funciones funciones = new Funciones();
            
            string idcia = this._idcia;
            string idtipoplan = this._idtipoplan;
            string idtipoper = this._idtipoper;
            string idtipocal = this._idtipocal;

            this._operacion = "EDITAR";
            txtCodigoInterno.Enabled = false;
            cmbConcepto.Enabled = false;
            txtCodigoInterno.Text = idperplan;
            txtDocIdent.Text = perplan.ui_getDatosPerPlan(idcia, idperplan, "1");
            txtNroDocIden.Text = perplan.ui_getDatosPerPlan(idcia, idperplan, "2");
            txtNombres.Text = perplan.ui_getDatosPerPlan(idcia, idperplan, "3");
            txtFecIniPerLab.Text = perplan.ui_getDatosPerPlan(idcia, idperplan, "4");

            string query = " select A.idconplan as clave,A.desboleta as descripcion from detconplan";
            query = query + " A left join conplan B on A.idtipoplan=B.idtipoplan ";
            query = query + " and A.idconplan=B.idconplan and A.idcia=B.idcia and A.idtipocal=B.idtipocal";
            query = query + " where A.idtipocal='" + @idtipocal + "' and A.idtipoplan='" + @idtipoplan+"' ";
            query = query + " and A.idtipoper='" + @idtipoper + "' and B.tipo='O' ";
            query = query + " and A.idconplan='" + @idconplan + "' and A.idcia='" + @idcia + "' ";
            query = query + " and A.destajo='NO'";

            funciones.consultaComboBox(query, cmbConcepto);

            if (tipocalculo.Equals("F"))
            {
                cmbTipoCalculo.Text = "F   FORMULA";
                txtValor.Text = "0";
                txtValor.Enabled = false;
            }
            else
            {
                cmbTipoCalculo.Text = "V   VALOR FIJO";
                txtValor.Text = Convert.ToString(valor);
                txtValor.Enabled = true;
            }
                        
            txtCodigoInterno.Focus();


        }

        internal void ui_actualizaComboBox()
        {
            Funciones funciones = new Funciones();
            string idtipoplan = this._idtipoplan;
            string idtipoper = this._idtipoper;
            string idcia = this._idcia;
            string idtipocal = this._idtipocal;

            string query = " select A.idconplan as clave,A.desboleta as descripcion from detconplan";
            query = query + " A left join conplan B on A.idtipoplan=B.idtipoplan ";
            query = query + " and A.idconplan=B.idconplan and A.idcia=B.idcia and A.idtipocal=B.idtipocal";
            query = query + " where A.idtipocal='" + @idtipocal + "' and A.idtipoplan='" + @idtipoplan + "' ";
            query = query + " and A.idtipoper='" + @idtipoper + "' and B.tipo='O' ";
            query = query + " and A.idcia='" + @idcia + "' ";
            query = query + " and A.destajo='NO' order by 1 asc";
                   
            funciones.listaComboBox(query, cmbConcepto, "");
        }

        private void ui_updconfijos_Load(object sender, EventArgs e)
        {
           
     
        }

        internal void ui_setConFijo(string idtipoper, string idtipoplan, string idcia,string idtipocal)
        {

            this._idtipoplan = idtipoplan;
            this._idtipoper = idtipoper;
            this._idcia = idcia;
            this._idtipocal = idtipocal;
        }

        internal void ui_newConFijo()
        {
            
            this._operacion="AGREGAR";
            txtCodigoInterno.Enabled = true;
            cmbConcepto.Enabled = true;
            txtCodigoInterno.Clear();
            txtDocIdent.Clear();
            txtNroDocIden.Clear();
            txtNombres.Clear();
            txtFecIniPerLab.Clear();
            txtCodigoInterno.Focus();
            cmbTipoCalculo.Text = "F   FORMULA";
            txtValor.Text = "0";         

        }

        private void txtCodigoInterno_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                if (txtCodigoInterno.Text.Trim() != string.Empty)
                {

                    string idcia = this._idcia;
                    string idperplan = txtCodigoInterno.Text.Trim();

                    PerPlan perplan = new PerPlan();
                    string codigoInterno = perplan.ui_getDatosPerPlan(idcia, idperplan, "0");
                    if (codigoInterno == string.Empty)
                    {
                        MessageBox.Show("Código no registrado del trabajador", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txtCodigoInterno.Clear();
                        txtDocIdent.Clear();
                        txtNroDocIden.Clear();
                        txtNombres.Clear();
                        txtFecIniPerLab.Clear();
                        txtCodigoInterno.Focus();

                    }
                    else
                    {
                        txtCodigoInterno.Text = perplan.ui_getDatosPerPlan(idcia, idperplan, "0");
                        txtDocIdent.Text = perplan.ui_getDatosPerPlan(idcia, idperplan, "1");
                        txtNroDocIden.Text = perplan.ui_getDatosPerPlan(idcia, idperplan, "2");
                        txtNombres.Text = perplan.ui_getDatosPerPlan(idcia, idperplan, "3");
                        txtFecIniPerLab.Text = perplan.ui_getDatosPerPlan(idcia, idperplan, "4");
                        e.Handled = true;
                        cmbConcepto.Focus();
                    }

                }
                else
                {

                    txtCodigoInterno.Clear();
                    txtDocIdent.Clear();
                    txtNroDocIden.Clear();
                    txtNombres.Clear();
                    txtFecIniPerLab.Clear();
                    txtCodigoInterno.Focus();
                }



            }
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            try
            {
                Funciones funciones = new Funciones();
                DetConPlan detconplan = new DetConPlan();
                          
                string operacion = this._operacion;
                string idtipoper = this._idtipoper;
                string idtipoplan = this._idtipoplan;
                string idtipocal = this._idtipocal;
                string idcia = this._idcia;
                string idperplan = txtCodigoInterno.Text.Trim();
                string tipocalculo = funciones.getValorComboBox(cmbTipoCalculo, 1);
                string idconplan = funciones.getValorComboBox(cmbConcepto, 4);
                float valor = float.Parse(txtValor.Text);
                string automatico = detconplan.getDetConPlan(idtipoplan, idconplan, idtipoper, idcia, idtipocal, "AUTOMATICO");

                if (automatico == "N" && tipocalculo == "F")
                {
                    
                    MessageBox.Show("No existe Fórmula asociada al concepto seleccionado", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    cmbTipoCalculo.Text = "V   VALOR FIJO"; 
                }
                else
                {
                    if (tipocalculo == "V" && valor == 0)
                    {
                        MessageBox.Show("El valor no puede ser cero", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtValor.Focus();
                    }
                    else
                    {
                        ConFijos confijos = new ConFijos();
                        confijos.actualizarConFijos(_operacion, idconplan, idtipoplan, idtipoper, idcia, idperplan, tipocalculo, valor,idtipocal);
                        ((ui_conceptosfijos)FormPadre).btnActualizar.PerformClick();
                        this.Close();
                    }

                }

            }
            catch (FormatException ex)
            {
                MessageBox.Show(ex.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cmbTipoCalculo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbTipoCalculo.Text.Substring(0,1).Equals("F"))
            {
                txtValor.Text = "0";
                txtValor.Enabled = false;
            }
            else
            {
                txtValor.Enabled = true;
            }

        }
    }
}