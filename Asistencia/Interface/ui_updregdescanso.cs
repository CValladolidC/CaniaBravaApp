using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CaniaBrava
{
    public partial class ui_updregdescanso : Form
    {
        Funciones funciones = new Funciones();
        string _idcia;
        string _idtipoper;
        string _operacion;
        string _idregvac;

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

        public void setValores(string idcia, string idtipoper)
        {
            MaesGen maesgen = new MaesGen();
            this._operacion = string.Empty;
            this._idcia = idcia;
            this._idtipoper = idtipoper;

            maesgen.listaDetMaesGen("171", cmbContingencia, "B");
            maesgen.listaDetMaesGen("035", cmbMesconting, "");
            maesgen.listaDetMaesGen("174", cmbEstCIT, "B");
            maesgen.listaDetMaesGen("173", cmbconcepsap, "B");
        }

        public ui_updregdescanso()
        {
            InitializeComponent();

            if (funciones.VersionAssembly()) Application.ExitThread();
        }

        public void ui_newRegVac()
        {
            UtileriasFechas utileriasfechas = new UtileriasFechas();
            MaesGen maesgen = new MaesGen();
            maesgen.listaDetMaesGen("171", cmbContingencia, "B");
            this._operacion = "AGREGAR";
            txtCodigoInterno.Enabled = true;
            lblF2.Visible = true;
            pictureBoxBuscar.Visible = true;
            txtCodigoInterno.Clear();
            txtNombres.Clear();
            txtDocIdent.Clear();
            txtNroDocIden.Clear();
            txtFecIniPerLab.Clear();
            txtCelular.Clear();
            txtAnio.Text = Convert.ToString(DateTime.Now.Year);
            txtInicio.Text = DateTime.Now.ToString("dd/MM/yyyy");
            txtFin.Text = DateTime.Now.ToString("dd/MM/yyyy");
            txtDiasVaca.Text = Convert.ToString(utileriasfechas.diferenciaEntreFechas(txtInicio.Text, txtFin.Text));
            txtCodigoInterno.Focus();
        }

        public void ui_loadDatosRegVac(string idperplan, string anio, string finivac, string ffinvac, string diasvac, string idregvac,
            string certi, string medic, string espec, string cmp, string estac, string diagn, string conti, string tipo, string mes, string totdias,
            string alerta, string statusCIT, string celu, string consap, string fecemision)
        {
            MaesGen maesgen = new MaesGen();
            UtileriasFechas utileriasfechas = new UtileriasFechas();
            Funciones funciones = new Funciones();
            PerPlan perplan = new PerPlan();
            this._operacion = "EDITAR";
            txtCodigoInterno.Enabled = false;
            lblF2.Visible = false;
            pictureBoxBuscar.Visible = false;
            txtCodigoInterno.Text = idperplan;
            txtDocIdent.Text = perplan.ui_getDatosPerPlan(idperplan, "1");
            txtNroDocIden.Text = perplan.ui_getDatosPerPlan(idperplan, "2");
            txtNombres.Text = perplan.ui_getDatosPerPlan(idperplan, "3");
            txtCelular.Text = celu;
            txtFecIniPerLab.Text = totdias;
            lbltotdias.ForeColor = Color.White;
            if (int.Parse(totdias) >= 20) { lbltotdias.BackColor = Color.Red; } else { lbltotdias.BackColor = Color.Green; }
            this._idregvac = idregvac;
            txtAnio.Text = anio;
            txtInicio.Value = DateTime.Parse(finivac);
            txtFin.Value = DateTime.Parse(ffinvac);
            dtpFecemision.Value = DateTime.Parse(fecemision);
            txtDiasVaca.Text = diasvac;

            rbSI.Checked = false; rbNO.Checked = false;
            if (alerta == "1") { rbSI.Checked = true; } else { rbNO.Checked = true; }

            rdCMP.Checked = false;
            rdCOP.Checked = false;
            if (tipo == "CMP") { rdCMP.Checked = true; } else { rdCOP.Checked = true; }
            txtCertificado.Text = certi;
            txtMedico.Text = medic;
            txtEspecialidad.Text = espec;

            txtCMP.Text = cmp;
            txtEstSalud.Text = estac;
            maesgen.consultaDetMaesGen("171", conti, cmbContingencia);
            maesgen.consultaDetMaesGen("035", mes, cmbMesconting);
            maesgen.consultaDetMaesGen("174", statusCIT, cmbEstCIT);
            maesgen.consultaDetMaesGen("173", consap, cmbconcepsap);
            txtdiagnostico.Text = diagn;
            txtAnio.Focus();
        }

        private void OpenRegMedico()
        {
            ui_updmedicos ui_updprovee = new ui_updmedicos();
            ui_updprovee._FormPadre = this;
            ui_updprovee.setValores("ui_updregdescanso");
            ui_updprovee.Activate();
            ui_updprovee.New_();
            ui_updprovee.BringToFront();
            ui_updprovee.ShowDialog();
            ui_updprovee.Dispose();
        }

        private void OpenRegCenSalud()
        {
            ui_updcensalud ui_updprovee = new ui_updcensalud();
            ui_updprovee._FormPadre = this;
            ui_updprovee.setValores("ui_updregdescanso");
            ui_updprovee.Activate();
            ui_updprovee.New_();
            ui_updprovee.BringToFront();
            ui_updprovee.ShowDialog();
            ui_updprovee.Dispose();
            txtdiagnostico.Focus();
        }

        #region Eventos KeyDown
        private void txtCodigoInterno_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                string idcia = this._idcia;
                string idtipoper = this._idtipoper;
                string cadenaBusqueda = string.Empty;
                string condicionAdicional = string.Empty;
                this._TextBoxActivo = txtCodigoInterno;
                FiltrosMaestros filtros = new FiltrosMaestros();
                filtros.filtrarPerPlan("ui_updregdescanso", this, txtCodigoInterno, null, null, "V", cadenaBusqueda, condicionAdicional);
            }
        }
        #endregion

        #region Eventos Click
        private void btnAceptar_Click(object sender, EventArgs e)
        {
            try
            {
                string idcia = this._idcia;
                string anio = txtAnio.Text;
                string idtipoper = this._idtipoper;
                string idperplan = txtCodigoInterno.Text.Trim();

                if (idperplan != string.Empty)
                {
                    Funciones funciones = new Funciones();
                    Descansos obj = new Descansos();
                    string finivac = txtInicio.Value.ToString("yyyy-MM-dd");
                    string ffinvac = txtFin.Value.ToString("yyyy-MM-dd");
                    string certif = txtCertificado.Text;
                    string medico = txtMedico.Text.Trim();
                    string especi = txtEspecialidad.Text;
                    string tipo = (rdCMP.Checked ? "CMP" : "COP");
                    string cmp = obj.ui_getDatosMedico(tipo, txtCMP.Text.Trim(), "1");
                    string estsal = txtEstSalud.Text;
                    string diagnostico = txtdiagnostico.Text;
                    string conting = funciones.getValorComboBox(cmbContingencia, 3);
                    string mesconting = funciones.getValorComboBox(cmbMesconting, 2);
                    string estadoCit = funciones.getValorComboBox(cmbEstCIT, 2);
                    string concepsap = funciones.getValorComboBox(cmbconcepsap, 3);
                    string celu = txtCelular.Text.Trim();
                    string femision = dtpFecemision.Value.ToString("yyyy-MM-dd");
                    int alerta = (rbSI.Checked ? 1 : 0);
                    //UtileriasFechas utileriasfechas = new UtileriasFechas();
                    int diasvac = (txtFin.Value.Date - txtInicio.Value.Date).Days + 1;//utileriasfechas.diferenciaEntreFechas(txtInicio.Text, txtFin.Text);
                    string valorValida = "G";

                    if (txtCodigoInterno.Text == string.Empty && valorValida == "G")
                    {
                        valorValida = "B";
                        MessageBox.Show("No ha seleccionado Trabajador", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txtCodigoInterno.Focus();
                    }
                    if (diasvac == 0 && valorValida == "G")
                    {
                        valorValida = "B";
                        MessageBox.Show("Periodo dias de Descanso médico no válido", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txtDiasVaca.Focus();
                    }
                    if (mesconting == string.Empty && valorValida == "G")
                    {
                        valorValida = "B";
                        MessageBox.Show("Debe ingresar mes de Contingencia", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txtDiasVaca.Focus();
                    }
                    if (estadoCit == string.Empty && valorValida == "G")
                    {
                        valorValida = "B";
                        MessageBox.Show("Debe ingresar estado de CIT", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txtDiasVaca.Focus();
                    }
                    if (celu == string.Empty && valorValida == "G")
                    {
                        valorValida = "B";
                        MessageBox.Show("Debe ingresar un numero de contacto", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txtDiasVaca.Focus();
                    }
                    if (concepsap == string.Empty && valorValida == "G")
                    {
                        valorValida = "B";
                        MessageBox.Show("Debe ingresar un Concepto SAP", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txtDiasVaca.Focus();
                    }
                    //if (certif == string.Empty && valorValida == "G")
                    //{
                    //    valorValida = "B";
                    //    MessageBox.Show("Debe ingresar un CIT", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    //    txtCertificado.Focus();
                    //}
                    //if (cmp == string.Empty && valorValida == "G")
                    //{
                    //    valorValida = "B";
                    //    MessageBox.Show("Debe ingresar un CMP valido", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    //    txtCMP.Focus();
                    //}
                    //if (medico == string.Empty && valorValida == "G")
                    //{
                    //    valorValida = "B";
                    //    MessageBox.Show("Debe ingresar un medico", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    //    txtMedico.Focus();
                    //}
                    //if (estsal == string.Empty && valorValida == "G")
                    //{
                    //    valorValida = "B";
                    //    MessageBox.Show("Debe ingresar un Centro de Salud", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    //    txtEstSalud.Focus();
                    //}
                    //if (diagnostico == string.Empty && valorValida == "G")
                    //{
                    //    valorValida = "B";
                    //    MessageBox.Show("Debe ingresar Diagnostico del CIT", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    //    txtInicio.Focus();
                    //}
                    if (conting == string.Empty && valorValida == "G")
                    {
                        valorValida = "B";
                        MessageBox.Show("Debe ingresar una Contingencia", "Validación de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        cmbContingencia.Focus();
                    }

                    if (valorValida.Equals("G"))
                    {
                        Descansos regvac = new Descansos();
                        GlobalVariables variables = new GlobalVariables();
                        string idregvac;
                        if (this._operacion.Equals("AGREGAR"))
                        {
                            idregvac = regvac.GeneraCodigoRegVac();
                        }
                        else
                        {
                            idregvac = this._idregvac;
                        }
                        regvac.actualizarRegDescanso(this._operacion, idperplan, idcia, anio, finivac, ffinvac
                            , diasvac, idregvac, certif, medico, especi, cmp, estsal, diagnostico, tipo, conting
                            , celu, variables.getValorUsr(), estadoCit, mesconting, femision, alerta, concepsap);

                        ((ui_regdescanso)FormPadre).btnActualizar.PerformClick();
                         Close();
                        //MessageBox.Show("eVENO DE CLOSE "); 
                    }
                }
                else
                {
                    MessageBox.Show("No ha seleccionado trabajador", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtCodigoInterno.Focus();
                }   
            }
            catch (FormatException ex)
            {
                MessageBox.Show(ex.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            ((ui_regdescanso)FormPadre).btnActualizar.PerformClick();
            Close();
        }

        private void btnVeriCIT_Click(object sender, EventArgs e)
        {
            //txtMedico.Clear();
            //txtEspecialidad.Clear();
            //string tipo = (rdCMP.Checked ? "CMP" : "COP");
            //string id = txtCMP.Text.Trim();
            //if (id != String.Empty)
            //{
            //    Descansos perplan = new Descansos();
            //    string codigoInterno = perplan.ui_getDatosMedico(tipo, id, "0");
            //    if (codigoInterno == string.Empty)
            //    {
            //        MessageBox.Show("Medico no registrado..", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //        txtCMP.Clear();
            //        txtCMP.Focus();
            //        OpenRegMedico();
            //    }
            //    else
            //    {
            //        rdCMP.Checked = false;
            //        rdCOP.Checked = false;
            //        if (codigoInterno == "CMP") { rdCMP.Checked = true; }
            //        else { rdCOP.Checked = true; }
            //        txtCMP.Text = perplan.ui_getDatosMedico(tipo, id, "1");
            //        txtMedico.Text = perplan.ui_getDatosMedico(tipo, id, "2");
            //        txtEspecialidad.Text = perplan.ui_getDatosMedico(tipo, id, "3");
            //    }
            //}
            string cadenaBusqueda = string.Empty;
            string condicionAdicional = string.Empty;
            FiltrosMaestros filtros = new FiltrosMaestros();
            filtros.filtrarMedico("ui_updregdescanso", this, txtCMP, cadenaBusqueda);
            if (txtCMP.Text == string.Empty)
            {
                txtMedico.Clear();
                txtEspecialidad.Clear();
            }
            btnVeriCenSalud.Focus();
        }

        private void btnVeriCenSalud_Click(object sender, EventArgs e)
        {
            //txtEstSalud.Clear();
            //Descansos perplan = new Descansos();
            //string codigoInterno = perplan.ui_getDatosCenSalud("0");
            //if (codigoInterno == string.Empty)
            //{
            //    MessageBox.Show("Centro de Salud no registrado..", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    OpenRegCenSalud();
            //}
            //else
            //{
            //    txtEstSalud.Text = perplan.ui_getDatosCenSalud("1");
            //    toolStripForm.Items[0].Select();
            //    toolStripForm.Focus();
            //}
            string cadenaBusqueda = string.Empty;
            string condicionAdicional = string.Empty;
            FiltrosMaestros filtros = new FiltrosMaestros();
            filtros.filtrarCentroSalud("ui_updregdescanso", this, txtEstSalud, cadenaBusqueda);
            txtdiagnostico.Focus();
        }
        #endregion

        #region Eventos KeyPress
        private void txtCodigoInterno_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                if (txtCodigoInterno.Text.Trim() != string.Empty)
                {
                    string idcia = this._idcia;
                    string idperplan = txtCodigoInterno.Text.Trim();

                    PerPlan perplan = new PerPlan();
                    string codigoInterno = perplan.ui_getDatosPerPlan(idperplan, "0");
                    if (codigoInterno == string.Empty)
                    {
                        MessageBox.Show("Código no registrado del trabajador", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txtCodigoInterno.Clear();
                        txtDocIdent.Clear();
                        txtNroDocIden.Clear();
                        txtNombres.Clear();
                        txtFecIniPerLab.Clear();
                        txtCelular.Clear();
                    }
                    else
                    {
                        txtCodigoInterno.Text = perplan.ui_getDatosPerPlan(idperplan, "0");
                        txtDocIdent.Text = perplan.ui_getDatosPerPlan(idperplan, "1");
                        txtNroDocIden.Text = perplan.ui_getDatosPerPlan(idperplan, "2");
                        txtNombres.Text = perplan.ui_getDatosPerPlan(idperplan, "3");
                        txtCelular.Text = perplan.ui_getDatosPerPlan(idperplan, "celular");

                        string query = " SELECT ISNULL(SUM(diasvac),0) as dias FROM regdescanso (NOLOCK) WHERE idperplan='" + @idperplan + "' AND anio='" + txtAnio.Text + "' ";
                        SqlConnection conexion = new SqlConnection();
                        conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
                        conexion.Open();

                        try
                        {
                            SqlCommand myCommand = new SqlCommand(query, conexion);
                            SqlDataReader odr = myCommand.ExecuteReader();

                            while (odr.Read())
                            {
                                int totdias = odr.GetInt32(odr.GetOrdinal("dias"));
                                txtFecIniPerLab.Text = totdias.ToString();
                                lbltotdias.ForeColor = Color.White;
                                if (totdias >= 20) { lbltotdias.BackColor = Color.Red; } else { lbltotdias.BackColor = Color.Green; }
                            }

                            odr.Close();
                            myCommand.Dispose();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        }
                        finally { conexion.Close(); }

                        e.Handled = true;
                        txtAnio.Focus();
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

        private void txtInicio_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                if (UtileriasFechas.IsDate(txtInicio.Text))
                {
                    UtileriasFechas utileriasfechas = new UtileriasFechas();
                    txtDiasVaca.Text = Convert.ToString(utileriasfechas.diferenciaEntreFechas(txtInicio.Text, txtFin.Text));
                    e.Handled = true;
                    txtFin.Focus();
                }
                else
                {
                    MessageBox.Show("Fecha de Inicio no válida", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    e.Handled = true;
                    txtDiasVaca.Clear();
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
                    if (UtileriasFechas.compararFecha(txtInicio.Text, "<=", txtFin.Text))
                    {
                        UtileriasFechas utileriasfechas = new UtileriasFechas();
                        txtDiasVaca.Text = Convert.ToString(utileriasfechas.diferenciaEntreFechas(txtInicio.Text, txtFin.Text));
                        e.Handled = true;
                        //toolStripForm.Items[0].Select();
                        //toolStripForm.Focus();
                        txtCertificado.Focus();
                    }
                    else
                    {
                        MessageBox.Show("Fecha de Fin no puede ser menor que la Fecha de Inicio", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        e.Handled = true;
                        txtDiasVaca.Clear();
                        txtFin.Focus();
                    }
                }
                else
                {
                    MessageBox.Show("Fecha de Fin no válida", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    e.Handled = true;
                    txtDiasVaca.Clear();
                    txtFin.Focus();
                }
            }
        }

        private void txtAnio_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                e.Handled = true;
                txtInicio.Focus();
            }
        }

        private void txtCertificado_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                e.Handled = true;
                txtCMP.Focus();
            }
        }

        private void txtCMP_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                string tipo = (rdCMP.Checked ? "CMP" : "COP");
                string cmp = txtCMP.Text.Trim();

                Descansos perplan = new Descansos();
                string codigoInterno = perplan.ui_getDatosMedico(tipo, cmp, "0");
                if (codigoInterno == string.Empty)
                {
                    MessageBox.Show("Medico no registrado..", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtMedico.Clear();
                    txtEspecialidad.Clear();
                    OpenRegMedico();
                }
                else
                {
                    rdCMP.Checked = false;
                    rdCOP.Checked = false;
                    if (codigoInterno == "CMP") { rdCMP.Checked = true; }
                    else { rdCOP.Checked = true; }
                    txtCMP.Text = perplan.ui_getDatosMedico(tipo, cmp, "1");
                    txtMedico.Text = perplan.ui_getDatosMedico(tipo, cmp, "2");
                    txtEspecialidad.Text = perplan.ui_getDatosMedico(tipo, cmp, "3");
                    e.Handled = true;
                }
            }
        }
        #endregion

        private void cmbContingencia_SelectedIndexChanged(object sender, EventArgs e)
        {
            Funciones funciones = new Funciones();
            string tipcap = funciones.getValorComboBox(cmbContingencia, 3);
            if (tipcap != string.Empty)
            {
                toolStripForm.Items[1].Select();
                toolStripForm.Focus();
            }
        }

        private void txtInicio_ValueChanged(object sender, EventArgs e)
        {
            //UtileriasFechas utileriasfechas = new UtileriasFechas();
            txtFin.MinDate = txtInicio.Value;
            int totaldias = (txtFin.Value.Date - txtInicio.Value.Date).Days + 1;//utileriasfechas.diferenciaEntreFechas(txtInicio.Text, txtFin.Text);
            txtDiasVaca.Text = totaldias.ToString();
            txtFin.Focus();
        }

        private void txtFin_ValueChanged(object sender, EventArgs e)
        {
            //if (UtileriasFechas.compararFecha(txtInicio.Value.ToString("dd/MM/yyyy"), "<=", txtFin.Value.ToString("dd/MM/yyyy")))
            //{
            //UtileriasFechas utileriasfechas = new UtileriasFechas();
            int totaldias = (txtFin.Value.Date - txtInicio.Value.Date).Days + 1;//utileriasfechas.diferenciaEntreFechas(txtInicio.Text, txtFin.Text);

            rbSI.Checked = false; rbNO.Checked = false;
            if (totaldias >= 10) { rbSI.Checked = true; } else { rbNO.Checked = true; }

            txtDiasVaca.Text = Convert.ToString(totaldias);
            txtCertificado.Focus();
            //}
            //else
            //{
            //    MessageBox.Show("Fecha de Fin no puede ser menor que la Fecha de Inicio", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    txtDiasVaca.Clear();
            //    txtFin.Focus();
            //}
        }

        private void rdCOP_CheckedChanged(object sender, EventArgs e)
        {
            if (rdCOP.Checked)
            {
                txtCMP.Clear();
                txtMedico.Clear();
                txtEspecialidad.Clear();
            }
        }

        private void rdCMP_CheckedChanged(object sender, EventArgs e)
        {
            if (rdCMP.Checked)
            {
                txtCMP.Clear();
                txtMedico.Clear();
                txtEspecialidad.Clear();
            }
        }
    }
}