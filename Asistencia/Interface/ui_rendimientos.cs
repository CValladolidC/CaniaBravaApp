using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CaniaBrava
{
    public partial class ui_rendimientos : Form
    {
        private DateTime? _RegEntrada { get; set; }
        private DateTime? _RegSalida { get; set; }
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

        public ui_rendimientos()
        {
            InitializeComponent();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            GlobalVariables variable = new GlobalVariables();
            Funciones fn = new Funciones();
            string iop = txtIOP.Text.Trim();
            string mes = txtmes.Text.Trim();
            string anio = txtanio.Text.Trim();
            string valor = txtvalor.Text.Trim();

            if (ValidaDatos())
            {
                string query = string.Empty;

                if (mes != string.Empty)
                {
                    query = @"UPDATE[CHISULSQL1].[database_indicadores].[dbo].[MoliendaMasa] SET VALOR = '" + valor + @"'
WHERE CODIGOIOP = '" + iop + "' AND ANHO = '" + anio + "' AND MES = '" + mes + "';";
                }
                else
                {
                    query = @" UPDATE [CHISULSQL1].[database_indicadores].[dbo].[MoliendaMasa] SET VALOR = '" + valor + @"'
WHERE CODIGOIOP = '" + iop + "' AND ANHO = '" + anio + @"' AND MES IS NULL;";
                }

                SqlConnection conexion = new SqlConnection();
                conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
                conexion.Open();

                try
                {
                    SqlCommand myCommand = new SqlCommand(query, conexion);
                    myCommand.ExecuteNonQuery();
                    myCommand.Dispose();
                    MessageBox.Show("Informacion exitosa..", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (SqlException ex)
                {
                    MessageBox.Show(ex.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
                conexion.Close();
            }

            ui_rendimientos_Load(sender, e);
        }

        private bool ValidaDatos()
        {
            Funciones fn = new Funciones();
            decimal val = -1;
            string valor = txtvalor.Text.Trim();
            decimal.TryParse(valor, out val);
            if (val == -1)
            {
                MessageBox.Show("Valor ingresado es incorrecto.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtvalor.Focus();
                return false;
            }

            return true;
        }

        private void dgdetalle_SelectionChanged(object sender, EventArgs e)
        {
            //_continua = false;
            var rowsCount = dgdetalle.SelectedRows.Count;
            if (rowsCount == 0 || rowsCount > 1) return;

            var row = dgdetalle.SelectedRows[0];
            if (row == null) return;

            string descrip = row.Cells[0].Value.ToString();
            string mes = row.Cells[3].Value.ToString();
            string anio = row.Cells[4].Value.ToString();
            string iop = row.Cells[5].Value.ToString();
            string val = row.Cells[6].Value.ToString();
            txtvalor.Enabled = false;
            Get_Informacion(descrip, mes, anio, iop, val);
        }

        private void Get_Informacion(string descrip, string mes, string anio, string iop, string val)
        {
            txtdescripcion.Text = descrip;
            txtmes.Text = mes;
            txtanio.Text = anio;
            txtIOP.Text = iop;
            txtvalor.Text = val;
        }

        private void ui_rendimientos_Load(object sender, EventArgs e)
        {
            listarendimientos();
        }


        private void listarendimientos()
            {
            string query = @"SELECT Descripcion,CODIGO,FECHA,Mes,ANHO AS [Año],CODIGOIOP,VALOR 
                            FROM [CHISULSQL1].[database_indicadores].[dbo].[MoliendaMasa] WHERE CODIGOIOP IN ('IOP-50010','IOP-50020','IOP-88888') ORDER BY AÑO, MES DESC";
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

                    dgdetalle.Columns[0].Width = 180;
                    dgdetalle.Columns[3].Width = 70;
                    dgdetalle.Columns[4].Width = 70;

                    dgdetalle.Columns[1].Visible = false;
                    dgdetalle.Columns[2].Visible = false;
                    dgdetalle.Columns[5].Visible = false;

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


        private void btnEdit_Click(object sender, EventArgs e)
        {
            txtvalor.Enabled = true;
            txtvalor.Focus();
            txtvalor.SelectAll();
        }

        private void AddRendimiento(string addmes, string addanio)
        {
            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();

            string query = "INSERT INTO [CHISULSQL1].[database_indicadores].[dbo].[MoliendaMasa] VALUES ('Rendimiento de Azucar','110570',NULL,'" + addmes + "','" + @addanio + "','IOP-50010',0.0);";
                        query += "INSERT INTO [CHISULSQL1].[database_indicadores].[dbo].[MoliendaMasa] VALUES ('Rendimiento de Etanol','110571',NULL,'" + addmes + "','" + @addanio + "','IOP-50020',0.0);";
                            query += "INSERT INTO [CHISULSQL1].[database_indicadores].[dbo].[MoliendaMasa] VALUES ('Recuperado Total','88888',NULL,'" + addmes + "','" + @addanio + "','IOP-88888',0.0);";
            try
            {
                SqlCommand myCommand = new SqlCommand(query, conexion);
                myCommand.ExecuteNonQuery();
                myCommand.Dispose();
            }
            catch (SqlException e)
            {
                MessageBox.Show(e.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }

            conexion.Close();
        }

        private void txtaddmes_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
                (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            // punto decimal
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }

        private void txtaddanio_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
                (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            // punto decimal
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }

        private const char SignoDecimal = '.'; // Carácter separador decimal
        private string _prevTextBoxValue; // Variable que almacena el valor anterior del Textbox
        private void txtvalor_TextChanged(object sender, EventArgs e)
        {
            var textBox = (TextBox)sender;
            // Comprueba si el valor del TextBox se ajusta a un valor válido
            if (Regex.IsMatch(textBox.Text, @"^(?:\d+\.?\d*)?$"))
            {
                // Si es válido se almacena el valor actual en la variable privada
                _prevTextBoxValue = textBox.Text;
            }
            else
            {
                // Si no es válido se recupera el valor de la variable privada con el valor anterior
                // Calcula el nº de caracteres después del cursor para dejar el cursor en la misma posición
                var charsAfterCursor = textBox.TextLength - textBox.SelectionStart - textBox.SelectionLength;
                // Recupera el valor anterior
                textBox.Text = _prevTextBoxValue;
                // Posiciona el cursor en la misma posición
                textBox.SelectionStart = Math.Max(0, textBox.TextLength - charsAfterCursor);
            }
        }

        private void txtvalor_KeyPress(object sender, KeyPressEventArgs e)
        {
            var textBox = (TextBox)sender;
            // Si el carácter pulsado no es un carácter válido se anula
            e.Handled = !char.IsDigit(e.KeyChar) // No es dígito
                        && !char.IsControl(e.KeyChar) // No es carácter de control (backspace)
                        && (e.KeyChar != SignoDecimal // No es signo decimal o es la 1ª posición o ya hay un signo decimal
                            || textBox.SelectionStart == 0
                            || textBox.Text.Contains(SignoDecimal));
        }

        private void btnupd_Click(object sender, EventArgs e)
        {
            txtaddmes.Enabled = true;
            txtaddanio.Enabled = true;
            txtaddmes.Focus();
        }

        private void btnguardar_Click(object sender, EventArgs e)
        {
            
                Funciones funciones = new Funciones();
                string addmes = txtaddmes.Text.Trim();
                string addanio = txtaddanio.Text.Trim();

                if (addmes == "")
                {
                    MessageBox.Show("Debe Completar el Mes", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }

                else
                {
                    if (addanio == "")
                    {
                        MessageBox.Show("Debe Completar el Año", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    }
                    else
                    {
                        AddRendimiento(addmes, addanio);
                        txtaddmes.Text = "";
                        txtaddanio.Text = "";
                        txtaddmes.Enabled = false;
                        txtaddanio.Enabled = false;
                        listarendimientos();

                    }
                }
            
        }
    }
           
}