using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CaniaBrava
{
    public partial class ui_updcontrolling_recursos : Form
    {
        Funciones funciones = new Funciones();
        MaesGen maesgen = new MaesGen();

        public ui_updcontrolling_recursos()
        {
            InitializeComponent();

            cmbcodigo.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            cmbcodigo.AutoCompleteSource = AutoCompleteSource.ListItems;

            string query = "SELECT clavemaesgen AS descripcion FROM maesgen WHERE idmaesgen='101'";
            funciones.listaComboBoxUnCampo(query, cmbUM, "B");
            //query = "SELECT clavemaesgen AS descripcion FROM maesgen WHERE idmaesgen='102'";
            //funciones.listaComboBoxUnCampo(query, cmbrubro, "B");
            maesgen.listaDetMaesGen("102", cmbrubro, "B");
            maesgen.listaDetMaesGen("103", cmbcuencon, "B");
            maesgen.listaDetMaesGen("104", cmbactividad, "B");
            query = "SELECT desmaesgen+' | '+abrevia AS descripcion FROM maesgen WHERE idmaesgen='105'";
            funciones.listaComboBoxUnCampo(query, cmbcodigo, "B");
            maesgen.listaDetMaesGen("106", cmbrecurso, "B");
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cmbcodigo_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetCodigo();
        }

        private void cmbUM_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetCodigo();
        }

        private void SetCodigo()
        {
            string cod = funciones.getValorComboBox(cmbcodigo, 200);
            string um = funciones.getValorComboBox(cmbUM, 5);
            if (cod != string.Empty && um != string.Empty)
            {
                txtcodigo.Text = cod.Split('|')[1] + um;
            }
        }

        private void cmbcodigo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                e.Handled = true;
                cmbUM.Focus();
            }
        }

        private void cmbrecurso_SelectedIndexChanged(object sender, EventArgs e)
        {
            toolStripForm.Items[0].Select();
            toolStripForm.Focus();
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            string maescod = funciones.getValorComboBox(cmbcodigo, 200).Split('|')[1].Trim();
            string um = funciones.getValorComboBox(cmbUM, 5).Trim();
            string cuenco = funciones.getValorComboBox(cmbcuencon, 10).Trim();
            string codi = txtcodigo.Text;
            string rubro = funciones.getValorComboBox(cmbrubro, 3).Trim();
            string activi = funciones.getValorComboBox(cmbactividad, 2).Trim();
            string recurso = funciones.getValorComboBox(cmbrecurso, 2).Trim();

            if (maescod != "" && um != "" && cuenco != "" && codi != "" && rubro != "" && activi != "" && recurso != "")
            {
                string id = txtid.Text;

                SqlConnection conexion = new SqlConnection();
                conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
                conexion.Open();

                string query = " UPDATE maescontrolling SET idmaescon='" + maescod + "',um='" + um + "', cuencon='" + cuenco + "', ";
                query += "cod ='" + codi + "', rubro ='" + rubro + "', actividad ='" + activi + "', recurso ='" + recurso + "' WHERE id='" + id + "';";

                try
                {
                    SqlCommand myCommand = new SqlCommand(query, conexion);
                    myCommand.ExecuteNonQuery();
                    myCommand.Dispose();

                    MessageBox.Show("Actualizacion exitosa", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (SqlException ex)
                {
                    MessageBox.Show(ex.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }

                conexion.Close();
            }
        }

        public void LoadDatos(string id, string idmaescon, string desmaescon, string um, string cuencon, string descuencon, string cod, string rubro, string actividad, string recurso)
        {
            txtid.Text = id;
            txtcodigo.Text = cod;
            string query = "SELECT clavemaesgen AS descripcion FROM maesgen WHERE idmaesgen='101' AND clavemaesgen='" + um + "'";
            funciones.consultaComboBoxUnCampo(query, cmbUM);
            //query = "SELECT clavemaesgen AS descripcion FROM maesgen WHERE idmaesgen='102' AND clavemaesgen='" + rubro + "'";
            //funciones.consultaComboBoxUnCampo(query, cmbrubro);
            maesgen.consultaDetMaesGen("102", rubro, cmbrubro);
            maesgen.consultaDetMaesGen("103", cuencon, cmbcuencon);
            maesgen.consultaDetMaesGen("104", actividad, cmbactividad);
            query = "SELECT desmaesgen+' | '+abrevia AS descripcion FROM maesgen WHERE idmaesgen='105' AND clavemaesgen='" + idmaescon + "'";
            funciones.consultaComboBoxUnCampo(query, cmbcodigo);
            maesgen.consultaDetMaesGen("105", idmaescon, cmbcodigo);
            maesgen.consultaDetMaesGen("106", recurso, cmbrecurso);
        }
    }
}

