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
    public partial class ui_updmotivos : Form
    {
        string _operacion;
        string _idtipoplan;
        string _idcia;
        string _idtipocal;
        
        private Form FormPadre;

        public Form _FormPadre
        {
            get { return FormPadre; }
            set { FormPadre = value; }
        }

        public ui_updmotivos()
        {
            InitializeComponent();
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            try
            {

                Funciones funciones = new Funciones();
                string operacion = this._operacion;
                string idtipoplan = this._idtipoplan;
                string idcia = this._idcia;
                string idtipocal = this._idtipocal;
                string idconplan = txtCodigo.Text.Trim();
                string desconplan = txtDescripcion.Text.Trim();
                string constante = funciones.getValorComboBox(cmbVariable, 2);
                string idclascol = funciones.getValorComboBox(cmbClasificacion, 1);
                string tipo = funciones.getValorComboBox(cmbTipo, 1);
                string valorValida = "G";
                string exisCons = "0";

                if (idconplan == string.Empty && valorValida == "G")
                {
                    MessageBox.Show("No ha especificado Código", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    valorValida = "B";
                    txtCodigo.Focus();

                }

                if (desconplan == String.Empty && valorValida == "G")
                {
                    MessageBox.Show("No ha especificado Nombre Descriptivo", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    valorValida = "B";
                    txtDescripcion.Focus();
                }

                Constante cons = new Constante();
                exisCons = cons.buscarConstante(idcia, idtipoplan, idtipocal,constante);
                if (exisCons=="0" && valorValida == "G")
                {
                    ConPlan updconplan = new ConPlan();
                    updconplan.actualizarConPlan(operacion, idconplan, idtipoplan, desconplan, constante, idclascol, tipo, idcia, idtipocal);
                    ((ui_motivos)FormPadre).btnActualizar.PerformClick();
                    this.Close();

                }
                else
                {
                    MessageBox.Show("Constante ya utilizada", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }

            }
            catch (FormatException ex)
            {
                MessageBox.Show(ex.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtDescripcion_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                e.Handled = true;
                cmbClasificacion.Focus();
            }
        }
        
        private void cmbVariable_Click(object sender, EventArgs e)
        {
           
        }

        private void txtCodigo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                e.Handled = true;
                txtDescripcion.Focus();
            }
        }

        private void ui_updmotivos_Load(object sender, EventArgs e)
        {
            
        }

        public void ui_newConPlan(string idtipoplan,string idcia,string idtipocal)
        {
            this._operacion = "AGREGAR";
            this._idtipoplan = idtipoplan;
            this._idcia = idcia;
            this._idtipocal = idtipocal;
            txtCodigo.Enabled = true;
            cmbVariable.Enabled = true;
            cmbTipo.Enabled = true;
            txtCodigo.Clear();
            txtDescripcion.Clear();
            cmbClasificacion.Text = "I     INGRESOS";
            cmbTipo.Text = "C    CONFORMANTE";
            txtCodigo.Focus();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cmbTipo_SelectedIndexChanged(object sender, EventArgs e)
        {
            Funciones funciones = new Funciones();
            string idtipoplan = this._idtipoplan;
            string idtipocal = this._idtipocal;
            string idcia = this._idcia;
            string tipo = funciones.getValorComboBox(cmbTipo, 1);
            string query;
            
            if (tipo.Equals("T"))
            {
                query = "SELECT idconstante as clave,'' as descripcion FROM constante ";
                query = query + " WHERE tipo='T' and idconstante not in (select constante ";
                query = query + " from conplan where idtipoplan='" + @idtipoplan + "' ";
                query = query + " and idcia='" + @idcia + "' and idtipocal='" + @idtipocal + "') ";
                query = query + " order by idconstante asc";
            }
            else
            {
                query = "SELECT idconstante as clave,'' as descripcion FROM constante ";
                query = query + " WHERE tipo='C' and idconstante not in (select constante ";
                query = query + " from conplan where idtipoplan='" + @idtipoplan + "' ";
                query = query + " and idcia='" + @idcia + "' and idtipocal='" + @idtipocal + "') ";
                query = query + " order by idconstante asc";
            }

            funciones.listaComboBox(query, cmbVariable, "");
        }
    }
}