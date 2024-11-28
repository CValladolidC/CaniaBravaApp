using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Configuration;

namespace CaniaBrava
{
    public partial class ui_accesocencos : Form
    {
        public string _idusr, _idger;
        public string _desusr;
        public string _idcia;
        public string _descia;

        private Form FormPadre;
        public Form _FormPadre
        {
            get { return FormPadre; }
            set { FormPadre = value; }
        }

        public ui_accesocencos()
        {
            InitializeComponent();
        }

        private void ui_accesocencos_Load(object sender, EventArgs e)
        {
            this.Text = this._desusr + " / " + this._descia;
            string idcia = this._idcia;
            string idger = this._idger;
            string query = "Select clavemaesgen as clave,desmaesgen as descripcion from maesgen (NOLOCK) ";
            query += "where idmaesgen='008' and parm1maesgen='" + @idger + "' and parm2maesgen='" + @idcia + "' and statemaesgen='V' order by 1 asc;";
            Funciones funciones = new Funciones();
            funciones.listaToolStripComboBox(query, cmbAlmacen);
            ui_listaCencosUsr();
        }

        private void ui_listaCencosUsr()
        {
            string idusr = this._idusr;
            string idcia = this._idcia;
            string idger = this._idger;
            Funciones funciones = new Funciones();
            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();
            string query = " Select A.idcencos as [Código],B.desmaesgen as [Áreas] from cencosusr A ";
            query = query + ", maesgen B where idmaesgen='008' and A.idcencos=B.clavemaesgen ";
            query = query + " and A.idusr='" + @idusr + "' and A.idgerencia='" + @idger + "' and A.idcia='" + @idcia + "'";
            query = query + " order by 1 asc;";
            try
            {
                using (SqlDataAdapter myDataAdapter = new SqlDataAdapter(query, conexion))
                {
                    DataSet myDataSet = new DataSet();
                    myDataAdapter.Fill(myDataSet, "tabla");
                    funciones.formatearDataGridView(dgvDetalle);
                    dgvDetalle.DataSource = myDataSet.Tables["tabla"];
                    dgvDetalle.Columns[0].Width = 100;
                    dgvDetalle.Columns[1].Width = 400;

                    dgvDetalle.AllowUserToResizeRows = false;
                    dgvDetalle.AllowUserToResizeColumns = false;
                    foreach (DataGridViewColumn column in dgvDetalle.Columns)
                    {
                        column.SortMode = DataGridViewColumnSortMode.NotSortable;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            conexion.Close();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnGrabar_Click(object sender, EventArgs e)
        {
            if (cmbAlmacen.Text == String.Empty)
            {
                MessageBox.Show("No ha especificado el Área", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                cmbAlmacen.Focus();
            }
            else
            {
                Funciones funciones = new Funciones();
                string idcencos = funciones.getValorToolStripComboBox(cmbAlmacen, 8);
                string idusr = this._idusr;
                string idcia = this._idcia;
                string idger = this._idger;
                UsrFile usrfile = new UsrFile();
                bool existe = usrfile.verificaCencosUsr(idcia, idcencos, idger, idusr);
                if (existe) { MessageBox.Show("El Área ya se encuentra asignado..", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information); return; }
                usrfile.actualizarCencosUsr(idcia, idcencos, idger, idusr);
                ui_listaCencosUsr();
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            Int32 selectedCellCount =
            dgvDetalle.GetCellCount(DataGridViewElementStates.Selected);
            if (selectedCellCount > 0)
            {
                string idcencos = dgvDetalle.Rows[dgvDetalle.SelectedCells[1].RowIndex].Cells[0].Value.ToString();
                string descencos = dgvDetalle.Rows[dgvDetalle.SelectedCells[1].RowIndex].Cells[1].Value.ToString();
                string idusr = this._idusr;
                string idcia = this._idcia;
                string idger = this._idger;

                DialogResult resultado = MessageBox.Show("¿Desea eliminar el Área " + descencos + "?",
                "Consulta Importante",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);
                if (resultado == DialogResult.Yes)
                {
                    UsrFile usrfile = new UsrFile();
                    usrfile.eliminarCencosUsr(idcia, idcencos, idger, idusr);
                    this.ui_listaCencosUsr();
                }
            }
            else
            {
                MessageBox.Show("No ha seleccionado registro a Eliminar", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }
    }
}