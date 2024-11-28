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
    public partial class ui_agrupaturnos : Form
    {
        GlobalVariables variables = new GlobalVariables();
        Funciones funciones = new Funciones();

        public ui_agrupaturnos()
        {
            InitializeComponent();
        }

        private void ui_agrupaturnos_Load(object sender, EventArgs e)
        {
            string query = " select a.idcia as clave, a.descia as descripcion from ciafile a (nolock) ";
            if (variables.getValorTypeUsr() != "00")
            {
                query += "inner join ciausrfile b (nolock) on b.idcia=a.idcia and b.idusr='" + variables.getValorUsr() + "' ";
            }
            funciones.listaComboBox(query, cmbCia, "X");
            cmbCia.Text = "X";

            query = "SELECT DISTINCT block as descripcion FROM perplan (nolock) WHERE block <> ''";
            funciones.listaComboBoxUnCampo(query, cmbGrupos, "X");
            cmbGrupos.Text = "X";
        }

        private void Load_Datos()
        {
            string query = string.Empty;
            string buscar = txtBuscar.Text.Trim();
            string grupo = funciones.getValorComboBox(cmbGrupos, 1);
            string cia = funciones.getValorComboBox(cmbCia, 2);
            string ger = funciones.getValorComboBox(cmbGerencia, 8);
            string seccion = funciones.getValorComboBox(cmbCencos, 8);
            string cadenaseccion = string.Empty;
            if (seccion.Trim() != "X   TODO")
            {
                cadenaseccion = " WHERE a.seccion='" + @seccion + "' ";

                if (seccion.Trim() == "50234202")
                {
                    btnGrupoA.Text = "Fundo LB/HC";
                    btnGrupoA.Height = 50;
                    btnGrupoB.Text = "Fundo ML/SV";
                    btnGrupoB.Height = 50;
                    btnGrupoC.Visible = false;
                    btnGrupoD.Visible = false;
                }
            }
            else
            {
                cadenaseccion = " WHERE a.seccion IN (" + string.Join(",", cmbCencos.Items.Cast<String>().Select(x => "'" + x.Substring(0, 8) + "'").ToArray()) + ") ";
            }

            query = " SELECT DISTINCT a.idperplan, a.nrodoc [DNI], RTRIM(a.apepat)+' '+RTRIM(a.apemat)+', '+RTRIM(a.nombres) AS Trabajador, ";
            query += "a.seccion, a.codaux, a.idcia, a.block [GRUPO] FROM perplan a (NOLOCK) ";

            if (variables.getValorTypeUsr() != "00") { query += "INNER JOIN cencosusr b (NOLOCK) ON b.idcencos = a.seccion "; }
            else { query += "LEFT JOIN cencosusr b (NOLOCK) ON b.idcencos = a.seccion "; }

            if (variables.getValorTypeUsr() != "00") { query += "AND b.idusr='" + variables.getValorUsr() + "' "; }

            query += "INNER JOIN tipoper c (NOLOCK) ON c.idtipoper=a.idtipoper AND c.destipoper NOT LIKE '%EMPLEADO%' ";
            query += @cadenaseccion + " AND a.idcia = '" + @cia + "' ";

            if (ger != "X   TODO") { query += "AND a.codaux = '" + @ger + "' "; }

            if (grupo != "X") { query += "AND a.block = '" + @grupo + "' "; }

            query += "AND RTRIM(a.apepat)+' '+RTRIM(a.apemat)+', '+RTRIM(a.nombres) LIKE '%" + @buscar + "%' ";
            query += " ORDER BY 2 ASC;";

            loadqueryDatos(query);
        }

        private void loadqueryDatos(string query)
        {
            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();

            try
            {
                using (SqlDataAdapter myDataAdapter = new SqlDataAdapter(query, conexion))
                {
                    UtileriasFechas utilfechas = new UtileriasFechas();
                    DataTable dt = new DataTable();

                    DataSet myDataSet = new DataSet();
                    myDataAdapter.Fill(dt);

                    funciones.formatearDataGridViewWhite(dgvdetalle);
                    dgvdetalle.MultiSelect = true;

                    dgvdetalle.DataSource = dt;

                    dgvdetalle.Columns[0].Visible = false;
                    dgvdetalle.Columns[3].Visible = false;
                    dgvdetalle.Columns[4].Visible = false;
                    dgvdetalle.Columns[5].Visible = false;

                    dgvdetalle.Columns[2].Width = 300;

                    dgvdetalle.AllowUserToResizeRows = false;
                    dgvdetalle.AllowUserToResizeColumns = false;
                    foreach (DataGridViewColumn column in dgvdetalle.Columns)
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
            dgvdetalle.Enabled = true;
        }

        #region Eventos Click
        private void UpdateGrupo(string idperplans, string grupo)
        {
            string query = string.Empty;

            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();

            query = "UPDATE perplan SET block = '" + @grupo + "' WHERE idperplan IN (" + idperplans + ");";

            try
            {
                SqlCommand myCommand = new SqlCommand(query, conexion);
                myCommand.ExecuteNonQuery();
                myCommand.Dispose();
                MessageBox.Show("Actualizacion exitosa..", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            conexion.Close();

            Load_Datos();
        }

        private string GetDatosGrid()
        {
            var selectedRows = dgvdetalle.SelectedRows
            .OfType<DataGridViewRow>()
            .Where(row => !row.IsNewRow)
            .ToArray();

            string idperplans = string.Empty;
            foreach (var row in selectedRows)
            {
                idperplans += "'" + row.Cells[0].Value + "',";
            }
            idperplans = idperplans.Substring(0, idperplans.Length - 1);

            return idperplans;
        }

        private void btnGrupoA_Click(object sender, EventArgs e)
        {
            string idperplans = GetDatosGrid();

            UpdateGrupo(idperplans, "A");
        }

        private void btnGrupoB_Click(object sender, EventArgs e)
        {
            string idperplans = GetDatosGrid();

            UpdateGrupo(idperplans, "B");
        }

        private void btnGrupoC_Click(object sender, EventArgs e)
        {
            string idperplans = GetDatosGrid();

            UpdateGrupo(idperplans, "C");
        }

        private void btnGrupoD_Click(object sender, EventArgs e)
        {
            string idperplans = GetDatosGrid();

            UpdateGrupo(idperplans, "D");
        }
        #endregion

        #region Eventos SelectedIndexChanged
        private void cmbCia_SelectedIndexChanged(object sender, EventArgs e)
        {
            dgvdetalle.DataSource = null;
            string cia = funciones.getValorComboBox(cmbCia, 2);
            if (cia != "X")
            {
                string query = " select distinct a.clavemaesgen as clave, a.desmaesgen as descripcion from maesgen a (nolock) ";
                query += "left join gerenciasusr b (nolock) on b.idgerencia=a.clavemaesgen where a.idmaesgen='040' and a.parm1maesgen = '" + cia + "' ";
                if (variables.getValorTypeUsr() != "00")
                {
                    query += "and b.idcia = '" + cia + "' and b.idusr='" + variables.getValorUsr() + "' ";
                }
                funciones.listaComboBox(query, cmbGerencia, "X");
            }
            else
            {
                cmbGerencia.Items.Clear();
                cmbGerencia.Items.Add("X   TODOS");
            }
            cmbGerencia.Text = "X   TODOS";
        }

        private void cmbGerencia_SelectedIndexChanged(object sender, EventArgs e)
        {
            dgvdetalle.DataSource = null;
            string cia = funciones.getValorComboBox(cmbCia, 2);
            string ger = funciones.getValorComboBox(cmbGerencia, 8);
            if (ger != "X   TODO")
            {
                string query = " select distinct a.clavemaesgen as clave, a.desmaesgen as descripcion from maesgen a (nolock) ";
                query += "left join cencosusr b (nolock) on b.idcencos=a.clavemaesgen where a.idmaesgen='008' and a.parm2maesgen = '" + cia + "' ";
                query += "and a.parm1maesgen = '" + ger + "' ";
                if (variables.getValorTypeUsr() != "00")
                {
                    query += "and b.idcia = '" + cia + "' and b.idusr='" + variables.getValorUsr() + "' ";
                }
                funciones.listaComboBox(query, cmbCencos, "X");
            }
            else
            {
                cmbCencos.Items.Clear();
                cmbCencos.Items.Add("X   TODOS");
            }
            cmbCencos.Text = "X   TODOS";
        }

        private void cmbCencos_SelectedIndexChanged(object sender, EventArgs e)
        {
            dgvdetalle.DataSource = null;
            string area = funciones.getValorComboBox(cmbCencos, 8);
            if (area != "X   TODO")
            {
                Load_Datos();

                string cia = funciones.getValorComboBox(cmbCia, 2);
                string ger = funciones.getValorComboBox(cmbGerencia, 8);
                {
                    string query = string.Empty;
                    if (area == "50234202")
                    {
                        query = "SELECT DISTINCT block [clave],";
                        query += "(CASE block WHEN 'A' THEN 'Fundo LB/HC' ELSE 'Fundo ML/SV' END) ";
                        query += "[descripcion] FROM perplan (NOLOCK) WHERE block <> ''";
                        query += "AND idcia = '" + cia + "' AND codaux = '" + ger + "' ";
                        query += "AND seccion = '" + area + "'";
                        funciones.listaComboBox(query, cmbGrupos, "X");
                    }
                    else
                    {
                        query = "SELECT DISTINCT block as descripcion FROM perplan (nolock) WHERE block <> ''";
                        query += "AND idcia = '" + cia + "' AND codaux = '" + ger + "' ";
                        query += "AND seccion = '" + area + "'";
                        funciones.listaComboBoxUnCampo(query, cmbGrupos, "X");
                    }
                    cmbGrupos.Text = "X";
                }
            }
        }

        private void cmbGrupos_SelectedIndexChanged(object sender, EventArgs e)
        {
            Load_Datos();
        }
        #endregion

        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            Load_Datos();
        }
    }
}
