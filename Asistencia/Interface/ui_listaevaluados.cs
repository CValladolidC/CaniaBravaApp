using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using CaniaBrava.cs;

namespace CaniaBrava.Interface
{
    public partial class ui_listaevaluados : Form
    {

        string query = "";

        Funciones funciones = new Funciones();
        SqlConnection conexion = new SqlConnection();


        public ui_listaevaluados()
        {
            InitializeComponent();

        }

        private Form FormPadre;
        public Form _FormPadre2
        {
            get { return FormPadre; }
            set { FormPadre = value; }
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void ui_listaevaluados_Load(object sender, EventArgs e)
        {

            ui_Lista();
        }

        private void ui_Lista()
        {

            query = " SELECT idregistro, dni, idempleado, nombres, feinicioproceso, niveleducativo,tipodocumento, celular, telefono, " +
                    "nacionalidad, distrito, provincia, departamento, referencia, fechanacimieto, sexo, estadocivil, categoria, licencia," +
                    "gerencia,unidorganizativa, posicion, sociedad, nomsociedad, email, jefatura, responsablerrhh, nivelorganizacional, sede," +
                    "origen, reemplazode,tipoproceso, medioatencion, fuentepostulacion, modalidad,tipocontrato,vacantes, cantevaluados, estproceso," +
                    "prioridad, proveedor, estatusocupacion, nrolong, satisfaccion, comentarios, cartaoferta, induccion, feestimacion," +
                    "fecierre, feincorporacion,Totaldias FROM Asistencia.dbo.gestiontalento ORDER BY idregistro; ";

            loadqueryDatos(query);
        }

        private void loadqueryDatos(string query)
        {
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();

            try
            {
                using (SqlDataAdapter myDataAdapter = new SqlDataAdapter(query, conexion))
                {
                    DataSet myDataSet = new DataSet();
                    myDataAdapter.Fill(myDataSet, "tblPerPlan");
                    funciones.formatearDataGridView(dgvdetalle);

                    dgvdetalle.DataSource = myDataSet.Tables["tblPerPlan"];
                    //dgvdetalle.Columns[0].HeaderText = "Id";
                    dgvdetalle.Columns[1].HeaderText = "Dni";
                    dgvdetalle.Columns[2].HeaderText = "Cod Trab";
                    dgvdetalle.Columns[3].HeaderText = "Nombres";
                    dgvdetalle.Columns[4].HeaderText = "Fecha inicio";
                    dgvdetalle.Columns[5].HeaderText = "Nivel Educativo";

                    //dgvdetalle.Columns[0].Width = 0;
                    dgvdetalle.Columns[1].Width = 80;
                    dgvdetalle.Columns[2].Width = 80;
                    dgvdetalle.Columns[3].Width = 350;
                    dgvdetalle.Columns[4].Width = 90;
                    dgvdetalle.Columns[5].Width = 200;


                    dgvdetalle.Columns[0].Visible = false;
                    dgvdetalle.Columns[6].Visible = false;
                    dgvdetalle.Columns[7].Visible = false;
                    dgvdetalle.Columns[8].Visible = false;
                    dgvdetalle.Columns[9].Visible = false;
                    dgvdetalle.Columns[10].Visible = false;
                    dgvdetalle.Columns[11].Visible = false;
                    dgvdetalle.Columns[12].Visible = false;
                    dgvdetalle.Columns[13].Visible = false;
                    dgvdetalle.Columns[14].Visible = false;
                    dgvdetalle.Columns[15].Visible = false;
                    dgvdetalle.Columns[16].Visible = false;
                    dgvdetalle.Columns[17].Visible = false;
                    dgvdetalle.Columns[18].Visible = false;
                    dgvdetalle.Columns[19].Visible = false;
                    dgvdetalle.Columns[20].Visible = false;
                    dgvdetalle.Columns[21].Visible = false;
                    dgvdetalle.Columns[22].Visible = false;
                    dgvdetalle.Columns[23].Visible = false;
                    dgvdetalle.Columns[24].Visible = false;
                    dgvdetalle.Columns[25].Visible = false;
                    dgvdetalle.Columns[26].Visible = false;
                    dgvdetalle.Columns[27].Visible = false;
                    dgvdetalle.Columns[28].Visible = false;
                    dgvdetalle.Columns[29].Visible = false;
                    dgvdetalle.Columns[30].Visible = false;
                    dgvdetalle.Columns[31].Visible = false;
                    dgvdetalle.Columns[32].Visible = false;
                    dgvdetalle.Columns[33].Visible = false;
                    dgvdetalle.Columns[34].Visible = false;
                    dgvdetalle.Columns[35].Visible = false;
                    dgvdetalle.Columns[36].Visible = false;
                    dgvdetalle.Columns[37].Visible = false;
                    dgvdetalle.Columns[38].Visible = false;
                    dgvdetalle.Columns[39].Visible = false;
                    dgvdetalle.Columns[40].Visible = false;
                    dgvdetalle.Columns[41].Visible = false;
                    dgvdetalle.Columns[42].Visible = false;
                    dgvdetalle.Columns[43].Visible = false;
                    dgvdetalle.Columns[44].Visible = false;
                    dgvdetalle.Columns[45].Visible = false;
                    dgvdetalle.Columns[46].Visible = false;
                    dgvdetalle.Columns[47].Visible = false;
                    dgvdetalle.Columns[48].Visible = false;
                    dgvdetalle.Columns[49].Visible = false;
                    dgvdetalle.Columns[50].Visible = false;

                }


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }

            conexion.Close();
        }

        private void dgvdetalle_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {


        }

        private void dgvdetalle_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            ui_atraccion_seleccion r = new ui_atraccion_seleccion(dgvdetalle);
            r.Show();
            this.Close();
           
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            Int32 selectedCellCount =
            dgvdetalle.GetCellCount(DataGridViewElementStates.Selected);
            if (selectedCellCount > 0)
            {
                string idusr = dgvdetalle.Rows[dgvdetalle.SelectedCells[1].RowIndex].Cells[0].Value.ToString();
                string Nomb = dgvdetalle.Rows[dgvdetalle.SelectedCells[1].RowIndex].Cells[3].Value.ToString();

                DialogResult resultado = MessageBox.Show("¿Desea eliminar el usuario " + Nomb + "?",
                "Consulta Importante",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);
                if (resultado == DialogResult.Yes)
                {
                    atraccion_seleccion atraccion_seleccion = new atraccion_seleccion();
                    atraccion_seleccion.eliminar_user_atraccion_seleccion(idusr);
                    this.ui_Lista();
                }
            }
            else
            {
                MessageBox.Show("No ha seleccionado registro a Eliminar", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }
    }
}
