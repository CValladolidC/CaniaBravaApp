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
    public partial class ui_reprogramacionInicial : Form
    {
        Funciones fn = new Funciones();

        private string Nromov { get; set; }
        private string CodTrabajador { get; set; }
        private string Trabajador { get; set; }
        private string Horario { get; set; }
        private string Fecha { get; set; }
        private string Deshorario { get; set; }
        private TextBox TextBoxActivo;
        public TextBox _TextBoxActivo
        {
            get { return TextBoxActivo; }
            set { TextBoxActivo = value; }
        }

        private Form FormPadre;
        public Form _FormPadre
        {
            get { return FormPadre; }
            set { FormPadre = value; }
        }

        public ui_reprogramacionInicial()
        {
            InitializeComponent();

            if (fn.VersionAssembly()) Application.ExitThread();
        }

        public void _Load(string nromov, DateTime fec1, DateTime fec2)
        {
            this.Nromov = nromov;
            txtProg.Text = "Programación " + fec1.ToString("yyyy-MM-dd") + " al " + fec2.ToString("yyyy-MM-dd");

            string query = "SELECT RTRIM(b.idplantiphorario)+RTRIM(CAST(a.iddias_semana AS CHAR)) [clave],";
            query += "b.descripcion +' ['+a.hor_entrada+' - '+a.hor_salida+']' [descripcion] ";
            query += "FROM plantiphorariodet a (NOLOCK) ";
            query += "INNER JOIN plantiphorario b (NOLOCK) ON b.idplantiphorario = a.idplantiphorario ORDER BY b.idcia ";
            fn.listaComboBox(query, cmbHorario, string.Empty);

            Editar();
        }

        public void Editar()
        {
            string query = @"SELECT a.idperplan [Codigo],a.destrabajador [Trabajador],a.destipohorario [Horario],CONVERT(VARCHAR(10),a.fechadiaria,120) [Fecha],
                            LEFT(CONVERT(TIME, a.fechaini,120),5) [H.Inicial],LEFT(CONVERT(TIME, a.fechafin,120),5) [H.Final],
                            a.fechaini,a.fechafin,a.idsap
                            FROM progdet a (NOLOCK) WHERE a.idprog='" + this.Nromov + "' ORDER BY a.fechadiaria";

            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();

            try
            {
                using (SqlDataAdapter myDataAdapter = new SqlDataAdapter(query, conexion))
                {
                    DataSet myDataSet = new DataSet();
                    myDataAdapter.Fill(myDataSet, "tabla");
                    Funciones funciones = new Funciones();
                    funciones.formatearDataGridView(dgdetalle);

                    dgdetalle.DataSource = myDataSet.Tables["tabla"];

                    dgdetalle.Columns[0].Width = 50;
                    dgdetalle.Columns[1].Width = 180;
                    dgdetalle.Columns[2].Width = 180;
                    dgdetalle.Columns[3].Width = 60;
                    dgdetalle.Columns[4].Width = 44;
                    dgdetalle.Columns[5].Width = 44;
                    dgdetalle.Columns[6].Visible = false;
                    dgdetalle.Columns[7].Visible = false;
                    dgdetalle.Columns[8].Visible = false;

                    dgdetalle.AllowUserToResizeRows = false;
                    dgdetalle.AllowUserToResizeColumns = false;
                    foreach (DataGridViewColumn column in dgdetalle.Columns)
                    {
                        column.SortMode = DataGridViewColumnSortMode.NotSortable;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            finally
            {
                conexion.Close();
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            GlobalVariables variables = new GlobalVariables();
            MaesGen maes = new MaesGen();
            string fecha = dtpFecha.Value.ToString("yyyy-MM-dd");
            string horario = fn.getValorComboBox(cmbHorario, 2).Trim();
            string deshorario = fn.getValorComboBox(cmbHorario, 50).Trim();
            string[] arr = deshorario.Split(' ');

            if (this.Fecha != fecha || this.Horario != horario)
            {
                string query = "EXECUTE SP_UPDATE_REPROG '" + this.Nromov + "','" +
                    this.CodTrabajador + "','" + this.Fecha + "','" + this.Horario + "','" +
                    fecha + "','" + horario + "','" + deshorario + "','" + arr[5].Replace("[", "") + "','" + arr[7].Replace("]", "") + "' ";

                SqlConnection conexion = new SqlConnection();
                conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
                conexion.Open();

                try
                {
                    SqlCommand myCommand = new SqlCommand(query, conexion);
                    myCommand.ExecuteNonQuery();
                    myCommand.Dispose();
                    MessageBox.Show("Registro actualizado..", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    Editar();
                }
                catch (SqlException ex)
                {
                    MessageBox.Show(ex.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
                conexion.Close();
            }
        }

        private void dgdetalle_SelectionChanged(object sender, EventArgs e)
        {
            var rowsCount = dgdetalle.SelectedRows.Count;
            if (rowsCount == 0 || rowsCount > 1) return;

            var row = dgdetalle.SelectedRows[0];
            if (row == null) return;

            this.CodTrabajador = row.Cells[0].Value.ToString();
            this.Trabajador = row.Cells[1].Value.ToString();
            this.Deshorario = row.Cells[2].Value.ToString();
            this.Fecha = row.Cells[3].Value.ToString();
            this.Horario = row.Cells[8].Value.ToString();

            Get_Informacion();
        }

        private void Get_Informacion()
        {
            cmbHorario.Enabled = false;
            dtpFecha.Enabled = false;
            if (DateTime.Parse(this.Fecha).Date >= DateTime.Now.Date)
            {
                string query = string.Empty;

                cmbHorario.Enabled = true;
                dtpFecha.Enabled = true;

                txttrab.Text = this.Trabajador;
                string[] arr = this.Deshorario.Split(' ');
                dtpFecha.Value = DateTime.Parse(this.Fecha);
                cmbHorario.Text = arr[0] + "  " + arr[1] + " " + arr[2] + " " + arr[3] + " " + arr[4] + " - " + arr[6];
            }
        }

        private void txtbuscar_TextChanged(object sender, EventArgs e)
        {

        }
    }
}