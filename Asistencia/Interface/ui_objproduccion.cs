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

namespace CaniaBrava.Interface
{
    public partial class ui_objproduccion : Form
    {
        /* Interfaz de objetivos de producción */
        public ui_objproduccion()
        {
            InitializeComponent();
            //Al iniciar formulario se cargan los datos de la grid view
            updateGridView(); //Mostrar datos en la grilla al iniciar la interfaz de objetivo de producción

            //Al editar una celda, llama al metodo dgIndicadores_EditingControlShowing (Actualizar datos de grilla)
            dgIndicadores.EditingControlShowing += new DataGridViewEditingControlShowingEventHandler(dgIndicadores_EditingControlShowing);

            dtpickerFechaObjetivo.Text = DateTime.Now.ToString("dd/MM/yyyy"); //Asignar fecha actual por defecto en data picker

            activarActualizar(); //Por defecto siempre se mostrara data del dia actual, por eso siempre sera actualizar y no guardar


            //Ocultar tab de query y manejo de errores
            tabControl1.TabPages.Remove(tbQuery);
            tabControl1.TabPages.Remove(tbManejoErrores);


        }
        /* Fin de interfaz de objetivos de producción */

        /*------------------------------------------------------------------------------------*/

        /* Método de actualizar grilla de indicadores */
        private void updateGridView() {
            //Obteniendo Fecha de datepicker y asignarla a variable fechaSeleccionada
            string fechaSeleccionada = dtpickerFechaObjetivo.Value.ToString("yyyy-MM-dd");

            //Query para Seleccionar los 10 IOP pre definidos con sus objetivos
            string query = @"
                    SELECT id ,  codigoIOP, 
                        CASE 
                        WHEN codigoIOP = 'IOP-20010' THEN 'Molienda'
                        WHEN codigoIOP = 'IOP-20710' THEN 'Propia'
                        WHEN codigoIOP = 'IOP-20720' THEN 'Caña larga T'
                        WHEN codigoIOP = 'IOP-37270' THEN 'Azúcar'
                        WHEN codigoIOP = 'IOP-20810' THEN 'Mix'
                        WHEN codigoIOP = 'IOP-20470' THEN 'Pol de caña'
                        WHEN codigoIOP = 'IOP-20310' THEN 'PNP Molienda'
                        WHEN codigoIOP = 'IOP-23730' THEN 'Etanol (m3)'
                        WHEN codigoIOP = 'IOP-22740' THEN 'Melaza(TM/dia)'
                        WHEN codigoIOP = 'IOP-06050' THEN 'Brix Alimentación'
                    END AS [Reporte], 
                    FORMAT(valor, '#,##0.00') AS [Objetivo] 
                  FROM [database_IOP].[dbo].[tbl_iop_objetivos]
                  where fecha = '" + fechaSeleccionada + "'"; // Buscar con fecha variable del data picker
            txtQuery.Text = "Se encontraron datos con la fecha : "+ fechaSeleccionada+", query usada: \r\n ";
            txtQuery.Text += query;
            //Usar conexión con cadena del app.config
            using (SqlConnection conexion = new SqlConnection(ConfigurationManager.AppSettings.Get("CADENA_CONEXION")))
            {
                conexion.Open(); //abrir conexión
                try
                {
                    using (SqlDataAdapter myDataAdapter = new SqlDataAdapter(query, conexion)) //Intentar ejecutar la query de selección en la conexión
                    {
                        SqlCommand comandoverificacion = new SqlCommand(query, conexion); //Se ejecuta nuevamente la query pero se almacena la primera columna de la primera fila del resultado mediante ExecuteScalar
                        object validación = comandoverificacion.ExecuteScalar(); //Se guarda el valor de esta fila en variable validación

                        /*Se crea data set para llenar el grid view*/
                        DataSet myDataSet = new DataSet(); 
                        myDataAdapter.Fill(myDataSet, "tabla");
                        dgIndicadores.DataSource = myDataSet.Tables["tabla"];
                        /*--*/

                        if (validación == null)//Si la consulta devuelve nulo es porque no se tienen registros del dia seleccionado
                        {
                            //Activar boton de guardar y bloquear boton actualizar
                            activarGuardar();

                            //Consulta para extraer los datos del dia actual (dateTimeNow) y asignar 0 
                            string query2 = @"
                                SELECT id , codigoIOP,
                                    CASE 
                                    WHEN codigoIOP = 'IOP-20010' THEN 'Molienda'
                                    WHEN codigoIOP = 'IOP-20710' THEN 'Propia'
                                    WHEN codigoIOP = 'IOP-20720' THEN 'Caña larga T'
                                    WHEN codigoIOP = 'IOP-37270' THEN 'Azúcar'
                                    WHEN codigoIOP = 'IOP-20810' THEN 'Mix'
                                    WHEN codigoIOP = 'IOP-20470' THEN 'Pol de caña'
                                    WHEN codigoIOP = 'IOP-20310' THEN 'PNP Molienda'
                                    WHEN codigoIOP = 'IOP-23730' THEN 'Etanol (m3)'
                                    WHEN codigoIOP = 'IOP-22740' THEN 'Melaza(TM/dia)'
                                    WHEN codigoIOP = 'IOP-06050' THEN 'Brix Alimentación'
                                END AS [Reporte], 
                                0 AS [Objetivo] 
                              FROM [database_IOP].[dbo].[tbl_iop_objetivos]
                              where fecha = '" + DateTime.Now.ToString("yyyy-MM-dd") + "'"; //Seleccionar fecha actual
                            using (SqlDataAdapter myDataAdapter2 = new SqlDataAdapter(query2, conexion)) //Ejecutar la query para mostrar datos con indicador 0 
                            {
                                /*Se crea data set para llenar el grid view*/
                                DataSet myDataSet2 = new DataSet();
                                myDataAdapter2.Fill(myDataSet2, "tabla2");

                                dgIndicadores.DataSource = myDataSet2.Tables["tabla2"];
                                /*--*/
                            }
                            txtQuery.Text = "No se encontraron datos, mostrando el dia de hoy. Consulta usada \r\n ";
                            txtQuery.Text += query2;

                        }
                        else { activarActualizar(); }//Activar boton de actualizar y bloquear boton guardar

                        AdjustGridSize(); //Autosize de grilla y quitar el autosize de columnas
                        blockEditGrid(); //Bloquear columna de nombre indicadores y solo permitir editar la columna de objetivos
                        conexion.Close();

                    }
                }
                catch (Exception ex) //Error cuando se haga la consulta de select indicadores 
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    txtManejoDeErrores.Text = "Error " + ex;

                }
            }
        }
        /* Fin de método de actualizar grilla de indicadores */

        /*-------Métodos para personalizar grilla-------*/
        private void AdjustGridSize(){ //Ajustar tamaño de grilla
            dgIndicadores.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgIndicadores.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;
        }

        private void blockEditGrid(){ //Bloquear campos de edición en grilla
            //Recorrer todas las columnas de la grilla Indicadores
            foreach (DataGridViewColumn column in dgIndicadores.Columns)
            {
                if (column.Name != "Objetivo") //A cualquier columna menos objetivo seran de solo lectura
                {
                    column.ReadOnly = true; //solo lectura
                    //column
                    
                }
                if (column.Name == "id") {
                    column.Visible = false;
                }
                if (column.Name == "codigoIOP") {
                    column.Visible = false;
                }
            }
        }
        /*-------Fin de métodos para personalizar grilla-------*/

        //Cuando el usuario edite un celda llamar al metodo del keypress(cuando se teclea) 
        private void dgIndicadores_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (dgIndicadores.CurrentCell.ColumnIndex == dgIndicadores.Columns["Objetivo"].Index)
            {
                TextBox editingControl = e.Control as TextBox;
                if (editingControl != null)
                {
                    editingControl.KeyPress -= new KeyPressEventHandler(Objetivo_KeyPress);
                    editingControl.KeyPress += new KeyPressEventHandler(Objetivo_KeyPress);
                }
            }
        }
        
        //Método para restringir caracteres en la columna de la grilla (números y crt de teclas)
        private void Objetivo_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Solo permitir números, el punto "." y control de teclas como enter y el backspace
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true;
            }

            // Solo permitir un punto decimal
            TextBox textBox = sender as TextBox;
            if (e.KeyChar == '.' && (textBox.Text.IndexOf('.') > -1 || textBox.Text.Length == 0))
            {
                
                e.Handled = true;
            }

            //Solo permitir dos decimalesy long de numero
            
        }
        
        private void btnSave_Click(object sender, EventArgs e)
        {            
            //Obteniendo Fecha de datepicker
            string fechaSeleccionada = dtpickerFechaObjetivo.Value.ToString("yyyy-MM-dd");

            string connectionString = "Data Source=CHISULSQL1;Database=database_IOP;uid=ctcuser;pwd=ctcuser";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                txtQuery2.Text = "Se ingresaron datos, consultas de insercción. \r\n ";
                connection.Open();
                foreach (DataGridViewRow row in dgIndicadores.Rows) //Recorrer grilla y repetir insert into
                {
                    string valorIndicador="", valorObjetivo="",usuario ="";
                    valorIndicador=(row.Cells["codigoIOP"].Value.ToString());
                    valorObjetivo=(row.Cells["Objetivo"].Value.ToString());
                    GlobalVariables variables = new GlobalVariables();
                    usuario = variables.getValorUsr();
                    
                    // query de update (apunta a la bd y tabla)
                    SqlCommand command = new SqlCommand("Insert into [database_IOP].[dbo].[tbl_iop_objetivos] (codigoIOP, fecha, valor, usuario) values ('"+valorIndicador+"', '"+fechaSeleccionada+"', '"+valorObjetivo+"','"+ usuario+"')", connection);
                    command.ExecuteNonQuery();
                    //    MessageBox.Show("Insert into [database_IOP].[dbo].[tbl_iop_objetivos] (codigoIOP, fecha, valor) values (" + valorIndicador + ", " + fechaSeleccionada + ", " + valorObjetivo + ")");
                    txtQuery2.Text += "Insert into [database_IOP].[dbo].[tbl_iop_objetivos] (codigoIOP, fecha, valor, usuario) values ('" + valorIndicador + "', '" + fechaSeleccionada + "', '" + valorObjetivo + "','" + usuario + "') \r\n ";

                } //fin de insert for each 
                updateGridView(); //Actulizar la grilla 
                activarActualizar();
                MessageBox.Show("Se guardaron los objetivos del día " + fechaSeleccionada + " correctamente .", "IOP Objetivos");
                connection.Close();

            }
        }


        private void dgvAlumnos_SelectionChanged(object sender, EventArgs e)
        {
            //MessageBox.Show("Conectado a BD IOP");
        }


        //Aun no esta implementado, se hace con query normal  (metodo -> btnSave_Click)
        private void Btnaar42ConProcedimiento(object sender, EventArgs e)
        {
            string connectionString = "tu cadena de conexión aquí";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("ActualizarIndicadores", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add("@id_indicador", SqlDbType.Int);
                command.Parameters.Add("@monto", SqlDbType.Decimal);
                bool hasError = false;
                foreach (DataGridViewRow row in dgIndicadores.Rows)
                {
                    if (!row.IsNewRow)
                    {
                        if (int.TryParse(row.Cells["id_indicador"].Value.ToString(), out int idIndicador) &&
                            decimal.TryParse(row.Cells["monto"].Value.ToString(), out decimal monto))
                        {
                            try
                            {
                                command.Parameters["@id_indicador"].Value = idIndicador;
                                command.Parameters["@monto"].Value = monto;
                                command.ExecuteNonQuery();
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show($"Error al guardar en la fila {row.Index + 1}: {ex.Message}", "Error de Base de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                hasError = true;
                                txtManejoDeErrores.Text = "Error " + ex;
                            }
                        }
                        else
                        {
                            MessageBox.Show($"Datos no válidos en la fila {row.Index + 1}.", "Error de Validación", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            hasError = true;
                        }
                    }
                }
                if (!hasError)
                {
                    MessageBox.Show("Cambios guardados correctamente.");
                }
            }
        }

        private void dtpickerFechaObjetivo_ValueChanged(object sender, EventArgs e)
        {
            updateGridView(); //Mostrar datos en la grilla al iniciar la interfaz de objetivo de producción
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            //Obteniendo Fecha de datepicker
            string fechaSeleccionada = dtpickerFechaObjetivo.Value.ToString("yyyy-MM-dd");

            string connectionString = "Data Source=CHISULSQL1;Database=database_IOP;uid=ctcuser;pwd=ctcuser";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {

                connection.Open();
                // query de update (apunta a la bd y tabla)
                SqlCommand command = new SqlCommand("UPDATE [database_IOP].[dbo].[tbl_iop_objetivos] set valor = @valor,usuario = @usuario, fechaActualización = GETDATE()  WHERE id = @id", connection);
                command.Parameters.Add("@valor", SqlDbType.Decimal);
                command.Parameters.Add("@id", SqlDbType.Int);
                command.Parameters.Add("@usuario", SqlDbType.VarChar);
                txtQuery2.Text = "Se actualizaron datos, consultas de actualización. \r\n ";
                foreach (DataGridViewRow row in dgIndicadores.Rows)
                {
                    if (!row.IsNewRow)
                    {
                        int idIndicador = int.Parse(row.Cells["id"].Value.ToString());
                        decimal monto = decimal.Parse(row.Cells["Objetivo"].Value.ToString());
                        // Asigna los valores a los parámetros del comando
                        //command.Parameters["@id"].Value = label1.Text;
                        //command.Parameters["@valor"].Value = txtObjetivo.Text;
                        command.Parameters["@id"].Value = idIndicador;
                        command.Parameters["@valor"].Value = monto;

                        GlobalVariables variables = new GlobalVariables();
                        command.Parameters["@usuario"].Value = variables.getValorUsr();
                        //MessageBox.Show("El id seleccionado es " + idIndicador + "y el monto es : " + monto);  
                        command.ExecuteNonQuery();
                        txtQuery2.Text += "UPDATE [database_IOP].[dbo].[tbl_iop_objetivos] set valor = "+ monto+ ",usuario = '"+variables.getValorUsr()+"', fechaActualización = GETDATE()  WHERE id = "+idIndicador+ " \r\n";
                    }
                }
                updateGridView(); //Actulizar la grilla 
                MessageBox.Show("Se actualizaron los objetivos del día " + fechaSeleccionada + " correctamente .", "IOP Objetivos");
                connection.Close();

            }
        }

        
        //Métodos de manejo de controles

        //Método para habilitar guardar y bloquear actualizar
        public void activarGuardar() {
            btnUpdate.Enabled = false; //Bloquear actualizar
            btnSave.Enabled = true; //Habilitar guardar
        }

        //Método para habilitar actualizar y bloquear guardar
        public void activarActualizar() {
            btnSave.Enabled = false; //Bloquear guardar
            btnUpdate.Enabled = true; //Habilitar actualizar
        }

        private void toolSBtnCode_Click(object sender, EventArgs e)
        {
            if (tabControl1.TabPages.Contains(tbQuery) && tabControl1.TabPages.Contains(tbManejoErrores)) //Si existen los tab -> ocultarlos con remove
            {
                tabControl1.TabPages.Remove(tbQuery);
                tabControl1.TabPages.Remove(tbManejoErrores);
            }
            else
            {
                var resultado = MessageBox.Show("¿Está seguro que quiere activar la opción de programación?", "Confirmación", MessageBoxButtons.YesNo);
                if (resultado == DialogResult.Yes)
                {
                    tabControl1.TabPages.Add(tbQuery);
                    tabControl1.TabPages.Add(tbManejoErrores);
                    tabControl1.SelectedTab = tbQuery; // Seleccionar conexión donde esta la query
                }
            }
        }

        //Fin de métodos para manejo de errores
    }




}
