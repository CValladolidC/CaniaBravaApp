using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Configuration;
using System.Data.SqlClient;


namespace CaniaBrava
{
    public partial class ui_updrecguia : Form
    {
        public string _codcia;
        public string _descia;
        public string _usuario;

        private string _alma;
        private string _numdoc;
        private string _item = string.Empty;
        private string _recibe;
        private string _tipomov;

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

        public ui_updrecguia()
        {
            InitializeComponent();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ui_updrecguia_Load(object sender, EventArgs e)
        {
            
            this.lblEmpresa.Text = this._descia;
            this._tipomov = "PE";
            
        }

      
        private void ui_habilitarItem(bool estado)
        {
            txtCantidadRecibida.Enabled = estado;
        }

        private void ui_limpiarItem()
        {
            txtCodigo.Text = string.Empty;
            txtDescri.Text = string.Empty;
            txtUnidad.Text = string.Empty;
            txtLote.Text = string.Empty;
            txtCantidad.Text = string.Empty;
            txtCantidadRecibida.Text = string.Empty;
        }

        public void editar(string codcia,string alma, string td, string numdoc)
        {
            string query;

            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();

            query = "SELECT * FROM almovc where codcia='" + @codcia + "' and alma='" + @alma + "' and td='" + @td + "' and numdoc='" + @numdoc + "'; ";
            try
            {
                SqlCommand myCommand = new SqlCommand(query, conexion);
                SqlDataReader myReader = myCommand.ExecuteReader();

                if (myReader.Read())
                {
                    Funciones funciones = new Funciones();
                    MaesGen maesgen = new MaesGen();
                    this._alma = myReader.GetString(myReader.GetOrdinal("alma"));
                    this._numdoc = myReader.GetString(myReader.GetOrdinal("numdoc"));

                    ////////////////////GUIA DE REMISION////////////////////

                    txtSerieGuia.Text = myReader.GetString(myReader.GetOrdinal("rfserieguia"));
                    txtNumeroGuia.Text = myReader.GetString(myReader.GetOrdinal("rfnroguia"));
                    txtFechaDocGuia.Text = myReader.GetString(myReader.GetOrdinal("fecguia"));
                   
                    txtFechaTraslado.Text = myReader.GetString(myReader.GetOrdinal("fectras"));
                    string codpartida = myReader.GetString(myReader.GetOrdinal("partida"));
                    query = "Select codpartida as clave,despartida as descripcion ";
                    query = query + "from punpar where idcia='" + @codcia + "' and ";
                    query = query + " codpartida='" + @codpartida + "'order by 1 asc;";
                    funciones.consultaComboBox(query, cmbPartida);

                    string llegada = myReader.GetString(myReader.GetOrdinal("llegada"));
                    string codclie = myReader.GetString(myReader.GetOrdinal("codclie"));
                    query = "Select codpartida as clave,despartida as descripcion ";
                    query = query + "from punclie where codclie='" + @codclie + "' and ";
                    query = query + " codpartida='" + @llegada + "'order by 1 asc;";
                    funciones.consultaComboBox(query, cmbLlegada);

                    maesgen.consultaDetMaesGen("210", myReader.GetString(myReader.GetOrdinal("mottras")), cmbMotivoTraslado);

                    this._recibe = myReader.GetString(myReader.GetOrdinal("recibe"));
                   
                    if (myReader.GetString(myReader.GetOrdinal("recibe")).Equals("S"))
                    {
                        lblMensaje.Text = "Guía de Remisión recepcionada por destinatario";
                        btnFinaliza.Enabled = false;
                        btnEditar.Enabled = false;
                        btnGrabar.Enabled = false;
                    }
                    else
                    {
                        lblMensaje.Text = "Guía de Remisión pendiente de recepción por destinatario";
                        btnFinaliza.Enabled = true;
                        btnEditar.Enabled = true;
                        btnGrabar.Enabled = true;
                    }

                    ////////////////////PARTE DE ENTRADA DE ALMACEN DESTINATARIO////////////////////
                    txtFecha.Text = myReader.GetString(myReader.GetOrdinal("fecguia"));

                    query = " SELECT codmov as clave,desmov as descripcion FROM tipomov WHERE ";
                    query = query + " tipomov='" + @myReader.GetString(myReader.GetOrdinal("td")) + "' and codmov='" + @myReader.GetString(myReader.GetOrdinal("codmov")) + "';";
                    funciones.consultaComboBox(query, cmbMov);


                }
                ui_listaItem(codcia, alma, td, numdoc);
                ui_listaParteEntrada(codcia, alma, td, numdoc);
                btnEditar.Focus();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }

            conexion.Close();
        }

        public void ui_listaItem(string codcia, string alma, string td, string numdoc)
        {
            Funciones funciones = new Funciones();

            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();
            string query =  " select A.item,A.codarti,B.desarti,A.lote,B.unidad,A.cantidad,A.cantrecibe ";
            query = query + " from almovd A left join alarti B on A.codcia=B.codcia and A.codarti = B.codarti";
            query = query + " where A.alma='" + @alma + "' and A.td='" + @td + "' and A.numdoc='" + @numdoc + "' ";
            query = query + " order by A.item asc ;";
            try
            {
                using (SqlDataAdapter myDataAdapter = new SqlDataAdapter(query, conexion))
                {
                    DataSet myDataSet = new DataSet();
                    myDataAdapter.Fill(myDataSet, "tblData");
                    funciones.formatearDataGridView(dgvData);
                    dgvData.DataSource = myDataSet.Tables["tblData"];
                    dgvData.Columns[0].HeaderText = "Item";
                    dgvData.Columns[1].HeaderText = "Código";
                    dgvData.Columns[2].HeaderText = "Descripción del Producto";
                    dgvData.Columns[3].HeaderText = "Lote";
                    dgvData.Columns[4].HeaderText = "Unidad";
                    dgvData.Columns[5].HeaderText = "Cantidad";
                    dgvData.Columns[6].HeaderText = "Cantidad Recibida";

                    if (dgvData.Rows.Count > 2)
                    {
                        dgvData.CurrentCell = dgvData.Rows[dgvData.Rows.Count - 1].Cells[0];
                    }

                    dgvData.Columns[0].Width = 40;
                    dgvData.Columns[1].Width = 70;
                    dgvData.Columns[2].Width = 350;
                    dgvData.Columns[3].Width = 120;
                    dgvData.Columns[4].Width = 75;
                    dgvData.Columns[5].Width = 75;
                    dgvData.Columns[6].Width = 75;
                 
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            conexion.Close();
        }


        public void ui_listaParteEntrada(string codcia, string alma, string td, string numdoc)
        {
            Funciones funciones = new Funciones();
            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();
            string query =  " select A.alma,A.td,A.numdoc,A.fecdoc,A.codmov,A.rftdoc,A.rfndoc from almovc A";
            query = query + " where A.codcia='" + @codcia + "' and A.almaori='" + @alma + "' ";
            query = query + " and A.tdori='" + @td + "' and A.numdocori='" + @numdoc + "'";
           
            try
            {
                using (SqlDataAdapter myDataAdapter = new SqlDataAdapter(query, conexion))
                {
                    DataSet myDataSet = new DataSet();
                    myDataAdapter.Fill(myDataSet, "tblData");
                    funciones.formatearDataGridView(dgvParteEntrada);
                    dgvParteEntrada.DataSource = myDataSet.Tables["tblData"];
                    dgvParteEntrada.Columns[0].HeaderText = "Almacén";
                    dgvParteEntrada.Columns[1].HeaderText = "Tipo";
                    dgvParteEntrada.Columns[2].HeaderText = "Num.Ope.";
                    dgvParteEntrada.Columns[3].HeaderText = "Fecha";
                    dgvParteEntrada.Columns[4].HeaderText = "Cod. Mov.";
                    dgvParteEntrada.Columns[5].HeaderText = "Doc.Ref.";
                    dgvParteEntrada.Columns[6].HeaderText = "Nro.Doc Ref.";

                    dgvParteEntrada.Columns[0].Width = 50;
                    dgvParteEntrada.Columns[1].Width = 40;
                    dgvParteEntrada.Columns[2].Width = 80;
                    dgvParteEntrada.Columns[3].Width = 75;
                    dgvParteEntrada.Columns[4].Width = 50;
                    dgvParteEntrada.Columns[5].Width = 50;
                    dgvParteEntrada.Columns[6].Width = 100;

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            conexion.Close();
        }

        public void ui_listacombobox()
        {
            Funciones funciones = new Funciones();
            
            string codcia = this._codcia;
            string usuario = this._usuario;
            string tipomov = "PE";
            string query = " Select A.codalma as clave,A.desalma as descripcion ";
            query = query + " from alalma A inner join almausr B on A.codcia=B.codcia and A.codalma=B.codalma ";
            query = query + " where A.codcia='" + @codcia + "' and A.estado='V' and B.idusr='" + @usuario + "' order by 1 asc; ";
            funciones.listaComboBox(query, cmbAlmacen, "");
            query = "SELECT codmov as clave,desmov as descripcion FROM tipomov WHERE tipomov='" + @tipomov + "' and estado='V';";
            funciones.listaComboBox(query, cmbMov, "");

        }

        private void dgvData_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if ((this.dgvData.Rows[e.RowIndex].Cells["cantrecibe"].Value).ToString().Equals("N"))
            {
                foreach (DataGridViewCell celda in

                this.dgvData.Rows[e.RowIndex].Cells)
                {

                    celda.Style.BackColor = Color.Yellow;
                    celda.Style.ForeColor = Color.Black;

                }
            }
        }

        private void ui_generaParteEntrada(string codcia, string alma, string td, string numdoc, string almadestino, string codmovdestino, string fecdocdestino)
        {

            AlmovC almovc = new AlmovC();
            string tddestino = "PE";
            string numdocdestino = almovc.genCodAlmov(codcia, almadestino, tddestino);
            

            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();
            string query = "INSERT INTO almovc (codcia,alma,td,numdoc, ";
            query = query + " fecdoc,codmov,situa,rftdoc,rfndoc, ";
            query = query + " secsoli,persoli,rfnsoli,nomrec,codpro, ";
            query = query + " cencos,rfalma,glosa1,glosa2,glosa3, ";
            query = query + " codclie,fcrea,fmod,usuario,flag,almaori,tdori,numdocori) ";
            query = query + " SELECT codcia,'" + @almadestino + "','" + @tddestino + "','" + @numdocdestino + "', ";
            query = query + " STR_TO_DATE('" + @fecdocdestino + "', '%d/%m/%Y'),'" + @codmovdestino + "',situa,rftguia,rfnguia, ";
            query = query + " secsoli,persoli,rfnsoli,nomrec,codpro, ";
            query = query + " cencos,alma,glosa1,glosa2,glosa3, ";
            query = query + " codclie,fcrea,fmod,usuario,'ALMA',alma,td,numdoc FROM almovc WHERE codcia='" + @codcia + "' ";
            query = query + " and alma='" + @alma + "' and td='" + @td + "' and numdoc='" + @numdoc + "' ; ";

            query = query + " INSERT INTO almovd (codcia,alma,td,numdoc, ";
            query = query + " item,codarti,cantidad,certificado,lote,fprod,fven,";
            query = query + " analisis,calidad,glosana1,glosana2,glosana3) ";
            query = query + " SELECT codcia,'" + @almadestino + "','" + @tddestino + "','" + @numdocdestino + "', ";
            query = query + " item,codarti,cantrecibe,certificado,lote,fprod,fven,";
            query = query + " analisis,calidad,glosana1,glosana2,glosana3 FROM almovd WHERE codcia='" + @codcia + "' ";
            query = query + " and alma='" + @alma + "' and td='" + @td + "' and numdoc='" + @numdoc + "' ; ";

            query = query + " UPDATE alalma SET nrope='" + @numdocdestino + "' where codcia='" + @codcia + "' and codalma='" + @almadestino + "'; ";

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

        private void ui_recalcularstock(string codcia, string alma, string td, string numdoc)
        {

            Alstock alstock = new Alstock();
            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();
            DataTable dtdetalle = new DataTable();
            string query = " select codarti from almovd ";
            query = query + " where codcia='" + @codcia + "' ";
            query = query + " and alma='" + @alma + "' and td='" + @td + "' and numdoc='" + @numdoc + "' ; ";
            SqlDataAdapter dadetalle = new SqlDataAdapter();
            dadetalle.SelectCommand = new SqlCommand(query, conexion);
            dadetalle.Fill(dtdetalle);
            foreach (DataRow row_detalle in dtdetalle.Rows)
            {
                string codarti = row_detalle["codarti"].ToString();
                alstock.recalcularStockProducto(codcia, alma, codarti);

            }

        }



        private string ui_valida(string codcia, string alma, string td, string numdoc)
        {
            string valorValida = "G";

            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();
            DataTable dtdetalle = new DataTable();
            string query = " select item,codarti,cantidad,cantrecibe,certificado,lote,fprod,fven,";
            query = query + " analisis,calidad,glosana1,glosana2,glosana3 from almovd ";
            query = query + " where codcia='" + @codcia + "' ";
            query = query + " and alma='" + @alma + "' and td='" + @td + "' and numdoc='" + @numdoc + "' ; ";
            SqlDataAdapter dadetalle = new SqlDataAdapter();
            dadetalle.SelectCommand = new SqlCommand(query, conexion);
            dadetalle.Fill(dtdetalle);
            foreach (DataRow row_detalle in dtdetalle.Rows)
            {
                string item = row_detalle["item"].ToString();
                float cantidad = float.Parse(row_detalle["cantidad"].ToString());
                float cantidadrecibe = float.Parse(row_detalle["cantrecibe"].ToString());
                if (cantidadrecibe == 0)
                {
                    valorValida = "B";
                    MessageBox.Show("No ha especificado cantidad recibida en item " + item, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                }else
                {
                    if (cantidad != cantidadrecibe)
                    {
                        valorValida = "B";
                        MessageBox.Show("Cantidad enviada no coincide con la recepcionada en el item "+item+", por favor solicitar anulación de la Guía de Remisión al Almacén de Origen para su reemplazo respectivo ", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                    }

                }

            }

            return valorValida;

        }
        private void btnEditar_Click(object sender, EventArgs e)
        {
            string query;
            string codcia = this._codcia;
            string alma = this._alma;
            string td = "PS";
            string numdoc = this._numdoc;

            Int32 selectedCellCount =
            dgvData.GetCellCount(DataGridViewElementStates.Selected);
            if (selectedCellCount > 0)
            {
                string item = dgvData.Rows[dgvData.SelectedCells[1].RowIndex].Cells[0].Value.ToString();

                SqlConnection conexion = new SqlConnection();
                conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
                conexion.Open();
                query = "SELECT * FROM almovd where codcia='" + @codcia + "' and alma='" + @alma + "' and td='" + @td + "' and ";
                query = query + " numdoc='" + @numdoc + "' and item='" + @item + "'; ";
                try
                {
                    SqlCommand myCommand = new SqlCommand(query, conexion);
                    SqlDataReader myReader = myCommand.ExecuteReader();

                    if (myReader.Read())
                    {
                        AlArti alarti = new AlArti();
                        this._item = myReader.GetString(myReader.GetOrdinal("item"));
                        txtCodigo.Text = myReader.GetString(myReader.GetOrdinal("codarti"));
                        txtDescri.Text = alarti.ui_getDatos(codcia, myReader.GetString(myReader.GetOrdinal("codarti")), "NOMBRE");
                        txtCantidad.Text = myReader.GetString(myReader.GetOrdinal("cantidad"));
                        txtUnidad.Text = alarti.ui_getDatos(codcia, myReader.GetString(myReader.GetOrdinal("codarti")), "UNIDAD");
                        txtLote.Text = myReader.GetString(myReader.GetOrdinal("lote"));
                        txtCantidadRecibida.Text = myReader.GetString(myReader.GetOrdinal("cantrecibe"));
                        ui_habilitarItem(true);

                    }

                    txtCantidadRecibida.Focus();
                }
                catch (Exception ex)
                {

                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }

                conexion.Close();
            }
        }


        private string validaItem()
        {
            string valorValida = "G";

            if (valorValida == "G")
            {
                try
                {
                    float cantidad = float.Parse(txtCantidad.Text);

                    if (cantidad <= 0)
                    {
                        valorValida = "B";
                        MessageBox.Show("Cantidad no válida", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txtCantidad.Text = "0";
                        txtCantidad.Focus();
                    }

                }
                catch (FormatException)
                {
                    valorValida = "B";
                    MessageBox.Show("Cantidad no válida", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtCantidad.Clear();
                    txtCantidad.Focus();
                }
            }

            if (valorValida == "G")
            {
                try
                {
                    float cantidadRecibida = float.Parse(txtCantidadRecibida.Text);

                    if (cantidadRecibida <= 0)
                    {
                        valorValida = "B";
                        MessageBox.Show("Cantidad Recibida no válida", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txtCantidadRecibida.Text = "0";
                        txtCantidadRecibida.Focus();
                    }

                }
                catch (FormatException)
                {
                    valorValida = "B";
                    MessageBox.Show("Cantidad Recibida no válida", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtCantidadRecibida.Clear();
                    txtCantidadRecibida.Focus();
                }
            }

            if (valorValida == "G")
            {
                float cantidad = float.Parse(txtCantidad.Text);
                float cantidadRecibida = float.Parse(txtCantidadRecibida.Text);
                if (cantidad < cantidadRecibida)
                {
                    valorValida = "B";
                    MessageBox.Show("Cantidad Recibida no puede ser mayor a la cantidad enviada", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtCantidadRecibida.Clear();
                    txtCantidadRecibida.Focus();
                }
            }

            return valorValida;
        }

        private void btnGrabar_Click(object sender, EventArgs e)
        {
            string valorValida = validaItem();
            if (valorValida.Equals("G"))
            {
                try
                {
                    string codcia = this._codcia;
                    string alma = this._alma;
                    string td = "PS";
                    string numdoc = this._numdoc;
                    string cantidadRecibida = txtCantidadRecibida.Text;
                    AlmovD almovd = new AlmovD();
                    string item = this._item;
                    almovd.updAlmovDRecibe(codcia, alma, td, numdoc, item, cantidadRecibida);
                    this.ui_limpiarItem();
                    this.ui_habilitarItem(false);
                    this.ui_listaItem(codcia, alma, td, numdoc);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnFinaliza_Click(object sender, EventArgs e)
        {
            string recibe = this._recibe;

            if (recibe.Equals("N"))
            {
                    try
                    {
                        string td = "PS";
                        string codcia = this._codcia;
                        string alma = this._alma;
                        string numdoc = this._numdoc;
                        string valorValida = ui_valida(codcia, alma, td, numdoc);
                        if (valorValida.Equals("G"))
                        {
                            
                            AlmovC almovc = new AlmovC();
                            Funciones funciones = new Funciones();
                            string frecibe = DateTime.Now.ToString("dd/MM/yyyy");
                            string almadestino = funciones.getValorComboBox(cmbAlmacen, 2);
                            string codmovdestino = funciones.getValorComboBox(cmbMov, 2);
                            string fecdocdestino = txtFecha.Text;
                            string usurec = this._usuario;

                            if (almadestino != "")
                            {
                                if (almadestino.Equals(alma))
                                {
                                    MessageBox.Show("El almacén de destino no puede ser igual que el almacén de origen", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                                }

                                else
                                {
                                    if (codmovdestino != "")
                                    {

                                        ui_generaParteEntrada(codcia, alma, td, numdoc, almadestino, codmovdestino, fecdocdestino);
                                        ui_listaParteEntrada(codcia, alma, td, numdoc);
                                        almovc.updRecAlmovC(codcia, alma, td, numdoc, frecibe, usurec);
                           
                                        lblMensaje.Text = "Guía de Remisión recepcionada por destinatario";
                                        ((ui_recguia)FormPadre).btnActualizar.PerformClick();
                                        MessageBox.Show("Recepción de Productos Finalizada", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        btnFinaliza.Enabled = false;
                                    }
                                    else
                                    {
                                        MessageBox.Show("No ha especificado Tipo de Movimiento", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                                    }
                                }
                            }
                            else
                            {
                                MessageBox.Show("No ha especificado Almacén de Recepción", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                            }
                          

                        }
                            
                    }
                    catch (FormatException ex)
                    {
                        MessageBox.Show(ex.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                
            }
            else
            {
                MessageBox.Show("La Guía de Remisión ya ha sido recepcionada", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void txtFecha_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                if (UtiFechas.IsDate(txtFecha.Text))
                {
                    e.Handled = true;
                    SendKeys.Send("{TAB}");
                }
                else
                {
                    MessageBox.Show("Fecha no válida", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    e.Handled = true;
                    txtFecha.Focus();
                }
            }
        }

        private void btnVisualiza_Click(object sender, EventArgs e)
        {
            Int32 selectedCellCount = dgvParteEntrada.GetCellCount(DataGridViewElementStates.Selected);
            if (selectedCellCount > 0)
            {
                Funciones funciones = new Funciones();
                Alalma alalma = new Alalma();
                
                string codcia = this._codcia;
                string alma = dgvParteEntrada.Rows[dgvParteEntrada.SelectedCells[1].RowIndex].Cells[0].Value.ToString();
                string td = dgvParteEntrada.Rows[dgvParteEntrada.SelectedCells[1].RowIndex].Cells[1].Value.ToString();
                string numdoc = dgvParteEntrada.Rows[dgvParteEntrada.SelectedCells[1].RowIndex].Cells[2].Value.ToString();
                string desalma = alalma.ui_getDatos(codcia, alma, "DESCRIPCION");
                ui_updalmov ui_updalmov = new ui_updalmov();
                ui_updalmov._FormPadre = this;
                ui_updalmov.Activate();
                ui_updalmov.BringToFront();
                ui_updalmov.setData(codcia, alma, desalma);
                ui_updalmov.ui_ActualizaComboBox();
                ui_updalmov.editar(codcia, alma, td, numdoc);
                ui_updalmov.ShowDialog();
                ui_updalmov.Dispose();
            }
            else
            {
                MessageBox.Show("No ha seleccionado registro a Visualizar", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void txtCantidadRecibida_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                e.Handled = true;
                btnGrabar.Focus();
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            Int32 selectedCellCount =
           dgvData.GetCellCount(DataGridViewElementStates.Selected);
            if (selectedCellCount > 0)
            {
               
                string codcia = this._codcia;
                string alma = dgvParteEntrada.Rows[dgvParteEntrada.SelectedCells[1].RowIndex].Cells[0].Value.ToString();
                string td = dgvParteEntrada.Rows[dgvParteEntrada.SelectedCells[1].RowIndex].Cells[1].Value.ToString();
                string numdoc = dgvParteEntrada.Rows[dgvParteEntrada.SelectedCells[1].RowIndex].Cells[2].Value.ToString();
                ui_autentica ui_autentica = new ui_autentica();
                ui_autentica._tipousr = "M";
                ui_autentica.Activate();
                ui_autentica.BringToFront();
                ui_autentica.ShowDialog();
                ui_autentica.Dispose();
                GlobalVariables gv = new GlobalVariables();
                string autentica = gv.getAutentica();
                if (autentica.Equals("S"))
                {
                    DialogResult resultado = MessageBox.Show("¿Desea eliminar el movimiento " + @td + "/" + @numdoc + "?",
                "Consulta Importante",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);
                    if (resultado == DialogResult.Yes)
                    {
                        AlmovC almovc = new AlmovC();
                        almovc.delAlmovC(codcia, alma, td, numdoc);
                        ui_listaParteEntrada(codcia, alma, td, numdoc);
                    }
                }

            }
            else
            {
                MessageBox.Show("No ha seleccionado registro a Eliminar", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }

        }


    }
}
