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

namespace CaniaBrava
{
    public partial class ui_CambioPrecioDestajo : Form
    {
        string _idcia;

        public ui_CambioPrecioDestajo()
        {
            InitializeComponent();
        }

        private void ui_CambioPrecioDestajo_Load(object sender, EventArgs e)
        {
            Funciones funciones = new Funciones();
            GlobalVariables variables = new GlobalVariables();
            this._idcia = variables.getValorCia();
            string idcia = this._idcia;
            string query;
            query = "SELECT ruccia as clave,descia as descripcion FROM ciafile WHERE idcia='" + @idcia + "';";
            funciones.listaComboBox(query, cmbEmpleador, "");
            cmbTipoRegistro.Text = "R   PERSONAL DE RETENCIONES";
        }

        private void txtMesSem_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                Funciones funciones = new Funciones();
                CalPlan calplan = new CalPlan();
                string idcia = this._idcia;
                string idtipoper = "O";
                string periodo = txtMesSem.Text.Trim() + funciones.replicateCadena(" ", 6);
                string messem = periodo.Substring(0, 2);
                string anio = periodo.Substring(3, 4);
                string idtipocal = "D";
                if (calplan.getDatosCalPlan(messem, anio, idtipoper, idcia, idtipocal, "FECHAINI") != "")
                {
                    txtFechaIni.Text = calplan.getDatosCalPlan(messem, anio, idtipoper, idcia, idtipocal, "FECHAINI");
                    txtFechaFin.Text = calplan.getDatosCalPlan(messem, anio, idtipoper, idcia, idtipocal, "FECHAFIN");
                    lblPeriodo.Visible = true;

                    if (calplan.getDatosCalPlan(messem, anio, idtipoper, idcia, idtipocal, "ESTADO") == "V")
                    {
                        lblPeriodo.Text = "Periodo Laboral Vigente";

                    }
                    else
                    {
                        lblPeriodo.Text = "Periodo Laboral Cerrado";

                    }
                }
                else
                {
                    MessageBox.Show("El Periodo Laboral no existe", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    lblPeriodo.Visible = false;
                    txtFechaIni.Clear();
                    txtFechaFin.Clear();
                    e.Handled = true;
                    txtMesSem.Focus();
                }
                ui_listaDetalle();

            }
        }

        private void cmbTipoRegistro_SelectedIndexChanged(object sender, EventArgs e)
        {
            Funciones fn = new Funciones();

            if (fn.getValorComboBox(cmbTipoRegistro, 1).Equals("P"))
            {
                string idcia = this._idcia;
                string query = "SELECT idtipoplan as clave,destipoplan as descripcion FROM tipoplan where idtipoplan in (select idtipoplan from reglabcia where idcia='" + @idcia + "') order by 1 asc;";
                fn.listaComboBox(query, cmbTipoPlan, "");
                lblTipoPlanilla.Visible = true;
                cmbTipoPlan.Visible = true;
                cmbTipoPlan.Focus();
            }
            else
            {
                lblTipoPlanilla.Visible = false;
                cmbTipoPlan.Visible = false;
            }
            ui_listaDetalle();
        }
        private void ui_listaDetalle()
        {
            Funciones funciones = new Funciones();
            string idcia = this._idcia;
            string periodo = txtMesSem.Text.Trim() + funciones.replicateCadena(" ", 6);
            string messem = periodo.Substring(0, 2);
            string anio = periodo.Substring(3, 4);
            string idtipocal = "D";
            string idtipoper = "O";
            string tiporegistro = funciones.getValorComboBox(cmbTipoRegistro, 1);
            string emplea = funciones.getValorComboBox(cmbEmpleador, 11);
            string estane = funciones.getValorComboBox(cmbEstablecimiento, 4);

            string idtipoplan;
            if (tiporegistro.Equals("P"))
            {
                idtipoplan = funciones.getValorComboBox(cmbTipoPlan, 3);
            }
            else
            {
                idtipoplan = "";
            }

            string nombreTablaRet = string.Empty;
            string nombreTablaPer = string.Empty;

            if (tiporegistro.Equals("R"))
            {
                nombreTablaRet = "desret";
                nombreTablaPer = "perret";
            }
            else
            {
                nombreTablaRet = "desplan";
                nombreTablaPer = "perplan";
            }


            string query = " select F.desproddes,G.desvar,E.deszontra,SUM(A.cantidad) as cantidad,SUM(A.total)/SUM(A.cantidad) as precio,";
            query = query + " SUM(A.total) as total,A.idproddes,A.idzontra,A.codvar from " + @nombreTablaRet + " A ";
            query = query + " left join zontra E on A.idzontra=E.idzontra and A.idcia=E.idcia  ";
            query = query + " left join proddes F on A.idproddes=F.idproddes and A.idcia=F.idcia ";
            query = query + " left join varproddes G on A.idproddes=G.idproddes and A.idcia=G.idcia and A.codvar=G.codvar ";
            query = query + " where A.idcia='" + @idcia + "' and A.idtipocal='" + @idtipocal + "' and A.idtipoper='" + @idtipoper + "' ";
            query = query + " and A.messem='" + @messem + "' and A.anio='" + @anio + "' and A.idtipoplan='" + @idtipoplan + "' and A.emplea='" + @emplea + "' and A.estane='" + @estane + "' ";
            query = query + " group by F.desproddes,G.desvar,E.deszontra,A.idproddes,A.idzontra,A.codvar order by F.desproddes,E.deszontra,A.codvar asc;";


            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();

            try
            {
                using (SqlDataAdapter myDataAdapter = new SqlDataAdapter(query, conexion))
                {
                    DataSet myDataSet = new DataSet();

                    myDataAdapter.Fill(myDataSet, "tblDestajo");
                    funciones.formatearDataGridView(dgvdetalle);

                    dgvdetalle.DataSource = myDataSet.Tables["tblDestajo"];
                    dgvdetalle.Columns[0].HeaderText = "Sección";
                    dgvdetalle.Columns[1].HeaderText = "Clasificacion";
                    dgvdetalle.Columns[2].HeaderText = "Zona";
                    dgvdetalle.Columns[3].HeaderText = "Cantidad Procesada";
                    dgvdetalle.Columns[4].HeaderText = "Precio por Unidad";
                    dgvdetalle.Columns[5].HeaderText = "Importe Total MN";

                    dgvdetalle.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    dgvdetalle.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    dgvdetalle.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;


                    dgvdetalle.Columns[3].DefaultCellStyle.Format = "###,###.##";
                    dgvdetalle.Columns[4].DefaultCellStyle.Format = "###,###.##";
                    dgvdetalle.Columns[5].DefaultCellStyle.Format = "###,###.##";

                    dgvdetalle.Columns["idproddes"].Visible = false;
                    dgvdetalle.Columns["idzontra"].Visible = false;
                    dgvdetalle.Columns["codvar"].Visible = false;

                    dgvdetalle.Columns[0].Width = 150;
                    dgvdetalle.Columns[1].Width = 150;
                    dgvdetalle.Columns[2].Width = 150;
                    dgvdetalle.Columns[3].Width = 70;
                    dgvdetalle.Columns[4].Width = 70;
                    dgvdetalle.Columns[5].Width = 70;


                }

                float cantidad = float.Parse(funciones.sumaColumnaDataGridView(dgvdetalle, 3));
                float importe = float.Parse(funciones.sumaColumnaDataGridView(dgvdetalle, 4));
                txtCantidad.Text = Convert.ToString(cantidad);
                txtImporte.Text = Convert.ToString(importe);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }


            conexion.Close();
        }

        private void cmbTipoPlan_SelectedIndexChanged(object sender, EventArgs e)
        {
            ui_listaDetalle();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void radioButtonNoEmp_CheckedChanged(object sender, EventArgs e)
        {
            Funciones funciones = new Funciones();
            GlobalVariables variables = new GlobalVariables();
            string idcia = this._idcia;
            string ruccia = variables.getValorRucCia();

            string squery = "SELECT ruccia as clave,descia as descripcion FROM ciafile WHERE idcia='" + @idcia + "';";
            funciones.listaComboBox(squery, cmbEmpleador, "");
            cmbEmpleador.Enabled = false;

            string squery_esta = "SELECT idestane as clave,desestane as descripcion FROM estane WHERE codemp='" + @ruccia + "' and idciafile='" + @idcia + "' order by idestane asc;";
            funciones.listaComboBox(squery_esta, cmbEstablecimiento, "");
            ui_listaDetalle();
            cmbEstablecimiento.Focus();
        }

        private void radioButtonSiEmp_CheckedChanged(object sender, EventArgs e)
        {
            Funciones funciones = new Funciones();
            string idcia = this._idcia;

            string squery = "SELECT rucemp as clave,razonemp as descripcion FROM emplea WHERE idciafile='" + @idcia + "' order by rucemp asc;";
            funciones.listaComboBox(squery, cmbEmpleador, "");
            cmbEmpleador.Enabled = true;

            string ruccia = funciones.getValorComboBox(cmbEmpleador, 11);
            string squery_esta = "SELECT idestane as clave,desestane as descripcion FROM estane WHERE codemp='" + @ruccia + "' and idciafile='" + @idcia + "' order by idestane asc;";
            funciones.listaComboBox(squery_esta, cmbEstablecimiento, "");

            ui_listaDetalle();
            cmbEmpleador.Focus();
        }

        private void cmbEmpleador_SelectedIndexChanged(object sender, EventArgs e)
        {
            Funciones funciones = new Funciones();
            string idcia = this._idcia;
            string ruccia = funciones.getValorComboBox(cmbEmpleador, 11);
            string squery_esta = "SELECT idestane as clave,desestane as descripcion FROM estane WHERE codemp='" + @ruccia + "' and idciafile='" + @idcia + "' order by idestane asc;";
            funciones.listaComboBox(squery_esta, cmbEstablecimiento, "");
            ui_listaDetalle();
            cmbEstablecimiento.Focus();
        }

        private void cmbEstablecimiento_SelectedIndexChanged(object sender, EventArgs e)
        {
            ui_listaDetalle();
        }

        private void btnCambiar_Click(object sender, EventArgs e)
        {
            Int32 selectedCellCount =
            dgvdetalle.GetCellCount(DataGridViewElementStates.Selected);
            if (selectedCellCount > 0)
            {

                Funciones funciones = new Funciones();
                GlobalVariables globlaVariables = new GlobalVariables();
                CalPlan calplan = new CalPlan();
                string idcia = globlaVariables.getValorCia();
                string periodo = txtMesSem.Text.Trim() + funciones.replicateCadena(" ", 6);
                string messem = periodo.Substring(0, 2);
                string anio = periodo.Substring(3, 4);
                string idtipocal = "D";
                string idtipoper = "O";
                string tiporegistro = funciones.getValorComboBox(cmbTipoRegistro, 1);
                string emplea = funciones.getValorComboBox(cmbEmpleador, 11);
                string estane = funciones.getValorComboBox(cmbEstablecimiento, 4);
                string idtipoplan;
                if (tiporegistro.Equals("P"))
                {
                    idtipoplan = funciones.getValorComboBox(cmbTipoPlan, 3);
                }
                else
                {
                    idtipoplan = "";
                }

                string idproddes = dgvdetalle.Rows[dgvdetalle.SelectedCells[1].RowIndex].Cells[6].Value.ToString();
                string idzontra = dgvdetalle.Rows[dgvdetalle.SelectedCells[1].RowIndex].Cells[7].Value.ToString();
                string codvar = dgvdetalle.Rows[dgvdetalle.SelectedCells[1].RowIndex].Cells[8].Value.ToString();

                float cantidad = float.Parse(dgvdetalle.Rows[dgvdetalle.SelectedCells[1].RowIndex].Cells[3].Value.ToString());
                float precio = float.Parse(dgvdetalle.Rows[dgvdetalle.SelectedCells[1].RowIndex].Cells[4].Value.ToString());
                float importe = float.Parse(dgvdetalle.Rows[dgvdetalle.SelectedCells[1].RowIndex].Cells[5].Value.ToString());
                string nomproddes = dgvdetalle.Rows[dgvdetalle.SelectedCells[1].RowIndex].Cells[0].Value.ToString();
                string nomzontra = dgvdetalle.Rows[dgvdetalle.SelectedCells[1].RowIndex].Cells[1].Value.ToString();
                string nomvar = dgvdetalle.Rows[dgvdetalle.SelectedCells[1].RowIndex].Cells[2].Value.ToString();

                if (calplan.getDatosCalPlan(messem, anio, idtipoper, idcia, idtipocal, "ESTADO") != "C")
                {
                    ui_UpdCambioPrecio ui_detalle = new ui_UpdCambioPrecio();
                    ui_detalle._FormPadre = this;
                    ui_detalle.ui_setValores(idcia, messem, anio, idtipocal, tiporegistro, idtipoplan, idtipoper, emplea,
                        estane, idzontra, idproddes, nomzontra, nomproddes, cantidad, precio, importe, nomvar, codvar);
                    ui_detalle.Activate();
                    ui_detalle.BringToFront();
                    ui_detalle.ShowDialog();
                    ui_detalle.Dispose();
                }
                else
                {
                    MessageBox.Show("El Periodo Laboral ya se encuentra cerrado", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
            else
            {
                MessageBox.Show("No ha seleccionado registro a Editar", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            ui_listaDetalle();
        }
    }
}