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
    public partial class ui_viewsipplan : Form
    {
        DataTable dt;
        GlobalVariables variables = new GlobalVariables();
        Funciones funciones = new Funciones();
        private Form FormPadre;
        public Form _FormPadre
        {
            get { return FormPadre; }
            set { FormPadre = value; }
        }
        private int Id { get; set; }
        private string Fecha1 { get; set; }
        private string Fecha2 { get; set; }
        private string Usuario { get; set; }
        private string Capataz { get; set; }
        private string Fundo { get; set; }
        private string Equipo { get; set; }

        public ui_viewsipplan()
        {
            InitializeComponent();

            if (funciones.VersionAssembly()) Application.ExitThread();
        }

        public void Load_Datos(int idprog, string capataz, string fecini, string fecfin, string fundo)
        {
            this.Id = idprog;
            this.Capataz = capataz;
            this.Fecha1 = fecini;
            this.Fecha2 = fecfin;
            this.Fundo = fundo;
            txtanio.Text = DateTime.Now.Year.ToString();

            cmbFundo.Enabled = false;
            switch (fundo.Trim())
            {
                case "INFR": fundo = "Infraestructura"; cmbFundo.Enabled = true; break;
                case "HC01": fundo = "La Huaca"; break;
                case "SV01": fundo = "San Vicente"; break;
                case "LB01":
                case "LB02": fundo = "Lobo"; break;
                case "BT01": fundo = "Buenaventura"; break;
                case "CT01":
                case "CT02": fundo = "Gravedad"; break;
                case "ML01":
                case "ML02":
                case "ML03":
                case "ML04":
                case "ML05":
                case "ML06":
                case "SJ01": fundo = "Montelima"; break;
            }

            //if (fundo == "Infraestructura")
            {
                cmbFundo.Text = "X Todos";
            }

            txtFundo.Text = fundo;
        }

        private void LoadDatos()
        {
            string query = string.Empty;
            string FD = funciones.getValorComboBox(cmbFundo, 15);
            string fundo = "AND Tipo_Proy <> 'Calendario' AND Cod_Fun='" + this.Fundo + "'";
            if (this.Fundo.Trim() == "INFR")
            {
                fundo = "AND Cod_McFun='" + this.Fundo + "' ";
                switch (FD.Trim())
                {
                    case "La Huaca": fundo += "AND Cod_Eq='HC'"; break;
                    case "San Vicente": fundo += "AND Cod_Eq='SV'"; break;
                    case "Lobo": fundo += "AND Cod_Eq IN ('LB','LB2')"; break;
                    case "Gravedad": fundo += "AND Cod_Eq='GR'"; break;
                    case "Montelima": fundo += "AND Cod_Eq='ML'"; break;
                }
            }
            string anio = txtanio.Text;

            query = @"SELECT Cod_Fun [Fundo],(CASE Cod_Eq WHEN 'LB' THEN 'LB01' WHEN 'LB2' THEN 'LB02' ELSE Cod_Eq END) [Equipo],Cod_Tr [Turno],Area,Cod_Est [Corte],Safra,
FCultivo [F.Cultivo],FAgoste [F.Agoste],FCosecha [F.Cosecha],Cod_Cjn,Descripcion_Cjn [Cj.Actividad],Cod_Act,Descripcion_Act [Actividad],FAplicacion [F.Aplicacion],Total 
FROM [PlanMo_New].[dbo].[TEMP_PROYECCION_DIA_MODIFY] WHERE Cod_Escenario='2021-04' AND Anho = " + anio + @"  
AND FAplicacion BETWEEN '" + this.Fecha1 + @"' AND '" + this.Fecha2 + @"' AND Tipo_Recurso='MO' " + fundo + " ORDER BY FAplicacion";

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
                    dt = new DataTable();

                    DataSet myDataSet = new DataSet();
                    myDataAdapter.Fill(dt);
                    dgvdetalle.DataSource = dt;
                    AutoFormatGrid();
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
        private void BtnAdd_Click(object sender, EventArgs e)
        {
            string query = GetDatosGrid();

            EjecutarQuery(query); 
        }

        private string GetDatosGrid()
        {
            var selectedRows = dgvdetalle.SelectedRows
            .OfType<DataGridViewRow>()
            .Where(row => !row.IsNewRow)
            .ToArray();

            string cadena = string.Empty;

            foreach (var row in selectedRows)
            {
                cadena += @"
IF (SELECT COUNT(1) FROM progagri_fecafueq WHERE idprog='" + this.Id + "' AND fecha='" + DateTime.Parse(row.Cells[13].Value.ToString()).ToString("yyyy-MM-dd") + "' AND capataz='" + this.Capataz + "' AND fundo='" + this.Fundo + "' AND equipo='" + row.Cells[1].Value + @"')=0 BEGIN
    INSERT INTO progagri_fecafueq VALUES ('" + this.Id + "','" + DateTime.Parse(row.Cells[13].Value.ToString()).ToString("yyyy-MM-dd") + "','" + this.Capataz + "','" + this.Fundo + "','" + row.Cells[1].Value + @"');
END;
IF (SELECT COUNT(1) FROM progagri_fecafueqtu WHERE idprog='" + this.Id + "' AND fecha='" + DateTime.Parse(row.Cells[13].Value.ToString()).ToString("yyyy-MM-dd") + "' AND capataz='" + this.Capataz + "' AND fundo='" + this.Fundo + "' AND equipo='" + row.Cells[1].Value + "' AND turno='"+ row.Cells[2].Value + @"')=0 BEGIN
    INSERT INTO progagri_fecafueqtu VALUES ('" + this.Id + "','" + DateTime.Parse(row.Cells[13].Value.ToString()).ToString("yyyy-MM-dd") + "','" + this.Capataz + "','" + this.Fundo + "','" + row.Cells[1].Value + "','" + row.Cells[2].Value + @"');
END;
IF (SELECT COUNT(1) FROM progagri_fecafueqtuac WHERE idprog='" + this.Id + "' AND fecha='" + DateTime.Parse(row.Cells[13].Value.ToString()).ToString("yyyy-MM-dd") + "' AND capataz='" + this.Capataz + "' AND fundo='" + this.Fundo + "' AND equipo='" + row.Cells[1].Value + "' AND turno='" + row.Cells[2].Value + "' AND actividad='" + row.Cells[11].Value + @"')=0 BEGIN
INSERT INTO progagri_fecafueqtuac VALUES ('" + this.Id + "','" + DateTime.Parse(row.Cells[13].Value.ToString()).ToString("yyyy-MM-dd") + "','" + this.Capataz + "','" + this.Fundo + "','" + row.Cells[1].Value + "','" + row.Cells[2].Value +
"','" + row.Cells[11].Value + "','" + row.Cells[14].Value + "',-1,1,0,'') END;";
            }

            return cadena;
        }

        private void EjecutarQuery(string query)
        {
            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();

            try
            {
                SqlCommand myCommand = new SqlCommand(query, conexion);
                myCommand.ExecuteNonQuery();
                myCommand.Dispose();
            }
            catch (SqlException ex)
            {
                if (ex.Message.Contains("PRIMARY KEY"))
                {
                    MessageBox.Show("Ya ingreso Equipo", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
                else
                {
                    MessageBox.Show(ex.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
            }
            conexion.Close();
            this.Close();
        }

        #endregion

        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtBuscar.Text.Trim() != string.Empty)
                {
                    var query = dt.AsEnumerable().Where(x => x.Field<string>("Actividad").ToUpper().Contains(txtBuscar.Text.ToUpper())).CopyToDataTable();
                    dgvdetalle.DataSource = query;

                    AutoFormatGrid();
                }
                else { btnCargar_Click(sender, e); }
            }
            catch (Exception ex_)
            {
                dgvdetalle.DataSource = null;
            }
        }

        private void AutoFormatGrid()
        {
            funciones.formatearDataGridViewWhite(dgvdetalle);
            dgvdetalle.MultiSelect = true;

            dgvdetalle.Columns[0].Width = 50;
            dgvdetalle.Columns[1].Width = 50;
            dgvdetalle.Columns[2].Width = 50;
            dgvdetalle.Columns[3].Width = 50;
            dgvdetalle.Columns[4].Width = 50;
            dgvdetalle.Columns[5].Width = 50;
            dgvdetalle.Columns[6].Width = 65;
            dgvdetalle.Columns[7].Width = 65;
            dgvdetalle.Columns[8].Width = 65;
            dgvdetalle.Columns[9].Visible = false;
            dgvdetalle.Columns[10].Width = 200;
            dgvdetalle.Columns[11].Visible = false;
            dgvdetalle.Columns[12].Width = 200;
            dgvdetalle.Columns[13].Width = 65;
            dgvdetalle.Columns[14].Width = 75;

            dgvdetalle.AllowUserToResizeRows = false;
            dgvdetalle.AllowUserToResizeColumns = false;
            foreach (DataGridViewColumn column in dgvdetalle.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
        }

        private void btnCargar_Click(object sender, EventArgs e)
        {
            string fundo = funciones.getValorComboBox(cmbFundo, 11);
            if (txtanio.Text.Trim().Length < 4)
            {
                MessageBox.Show("Debe ingresar el año de la proyeccion", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            else
            {
                //if (fundo.Trim() == string.Empty)
                //{
                //    MessageBox.Show("Debe ingresar el Fundo", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                //}
                //else
                {
                    LoadDatos();
                }
            }
        }
    }
}