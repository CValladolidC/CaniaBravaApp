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
    public partial class ui_lotegen : ui_form
    {
        string _codcia;

        private TextBox TextBoxActivo;

        public TextBox _TextBoxActivo
        {
            get { return TextBoxActivo; }
            set { TextBoxActivo = value; }
        }

        public ui_lotegen()
        {
            InitializeComponent();
        }

        private void ui_lotegen_Load(object sender, EventArgs e)
        {
            GlobalVariables gv = new GlobalVariables();
            this._codcia = gv.getValorCia();
            string codcia = this._codcia;
            string query = "Select codalma as clave,desalma as descripcion ";
            query = query + "from alalma where codcia='"+@codcia+"' and estado='V' order by 1 asc;";
            Funciones funciones = new Funciones();
            funciones.listaComboBox(query, cmbAlmacen, "");
            ui_listaReporte();
        }

        private void ui_listaReporte()
        {
            Funciones funciones = new Funciones();
            string codcia = this._codcia;
            string alma = funciones.getValorComboBox(cmbAlmacen, 2);
            string lote = txtLote.Text.Trim();
            ui_generaReporte(codcia,alma, lote);
        }


        private float getCantidadPorMovimiento(string codcia,string alma, string td, string numdoc, string item, string codarti)
        {
            DataTable dt = new DataTable();

            string query = string.Empty;

            float resultado = 0;

            query = " Select CASE ISNULL(sum(cantidad)) WHEN 1 THEN 0 WHEN 0 THEN sum(cantidad) END as cantidad ";
            query = query + " from almovd where codcia='" + @codcia + "' and alma='" + @alma + "' ";
            query = query + " and td='" + @td + "' and numdoc='" + @numdoc + "' and item='"+@item+"' ";
            query = query + " and codarti='" + @codarti + "' ";

            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();

            SqlDataAdapter da = new SqlDataAdapter();

            da.SelectCommand = new SqlCommand(query, conexion);
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row_dt in dt.Rows)
                {
                    resultado = float.Parse(row_dt["cantidad"].ToString());
                }
            }
            else
            {
                resultado = 0;
            }

            conexion.Close();
            return resultado;

        }
        
        private void ui_generaReporte(string codcia,string alma, string grupolote)
        {
            string query;
      
            
            DataTable dtreporte = new DataTable();

            DataTable dtlotes = new DataTable(); 
            
         
            dtreporte.Columns.Add("fecdoc", typeof(string));
            dtreporte.Columns.Add("td", typeof(string));
            dtreporte.Columns.Add("numdoc", typeof(string));
            dtreporte.Columns.Add("rftdoc", typeof(string));
            dtreporte.Columns.Add("rfndoc", typeof(string));
            dtreporte.Columns.Add("descri", typeof(string));
            dtreporte.Columns.Add("codmon", typeof(string));
            dtreporte.Columns.Add("preuni", typeof(string));
            dtreporte.Columns.Add("mov1", typeof(float));
            dtreporte.Columns.Add("mov2", typeof(float));
            dtreporte.Columns.Add("mov3", typeof(float));
            dtreporte.Columns.Add("mov4", typeof(float));
            dtreporte.Columns.Add("mov5", typeof(float));
            dtreporte.Columns.Add("saldo1", typeof(float));
            dtreporte.Columns.Add("saldo2", typeof(float));
            dtreporte.Columns.Add("saldo3", typeof(float));
            dtreporte.Columns.Add("saldo4", typeof(float));
            dtreporte.Columns.Add("saldo5", typeof(float));

            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();
            SqlDataAdapter da = new SqlDataAdapter();

            query = "select A.alma,A.td,A.numdoc,A.item,A.lote from almovd A  ";
            query = query + " where A.codcia='" + @codcia + "' and A.alma='" + @alma + "' ";
            query = query + " and A.td='PE' and A.numdoc='" + @grupolote + "' order by A.td,A.numdoc,A.item asc ; ";
            da.SelectCommand = new SqlCommand(query, conexion);
            da.Fill(dtlotes);
                    

            string lote = string.Empty;

            foreach (DataRow row_lotes in dtlotes.Rows)
            {

                DataTable dteventos = new DataTable();

                lote = row_lotes["lote"].ToString();

                query = "select A.alma,A.td,A.numdoc,A.item,B.fecdoc,B.rftdoc,B.rfndoc,B.glosa1,B.codmon,A.preuni ";
                query = query + " from almovd A left join almovc B  ";
                query = query + " on A.codcia=B.codcia and A.alma=B.alma and A.td=B.td and A.numdoc=B.numdoc ";
                query = query + " where A.codcia='" + @codcia + "' and A.alma='" + @alma + "' and A.lote='" + @lote + "' ";
                query = query + " order by B.td,B.fecdoc,B.rftdoc,B.rfndoc,B.glosa1,B.codmon ;";

                da.SelectCommand = new SqlCommand(query, conexion);
                da.Fill(dteventos);



                int factor = 0;
                float saldo1 = 0, saldo2 = 0, saldo3 = 0, saldo4 = 0, saldo5 = 0;
                float mov1 = 0, mov2 = 0, mov3 = 0, mov4 = 0, mov5 = 0;

                foreach (DataRow row_eventos in dteventos.Rows)
                {

                    string td = row_eventos["td"].ToString();
                    string numdoc = row_eventos["numdoc"].ToString();
                    string item = row_eventos["item"].ToString();

                    DataRow dr;
                    dr = dtreporte.NewRow();
                    dr[0] = row_eventos["fecdoc"].ToString();
                    dr[1] = row_eventos["td"].ToString();
                    dr[2] = row_eventos["numdoc"].ToString();
                    dr[3] = row_eventos["rftdoc"].ToString();
                    dr[4] = row_eventos["rfndoc"].ToString();
                    dr[5] = row_eventos["glosa1"].ToString();
                    dr[6] = row_eventos["codmon"].ToString();
                    dr[7] = row_eventos["preuni"].ToString();

                    if (td.Equals("PE"))
                    {
                        factor = 1;
                    }
                    else
                    {
                        factor = -1;
                    }

                    mov1 = getCantidadPorMovimiento(codcia, alma, td, numdoc, item, "01210001") * factor;
                    dr[8] = mov1;
                    saldo1 = saldo1 + mov1;

                    mov2 = getCantidadPorMovimiento(codcia, alma, td, numdoc, item, "01210002") * factor;
                    dr[9] = mov2;
                    saldo2 = saldo2 + mov2;

                    mov3 = getCantidadPorMovimiento(codcia, alma, td, numdoc, item, "01210003") * factor;
                    dr[10] = mov3;
                    saldo3 = saldo3 + mov3;

                    mov4 = getCantidadPorMovimiento(codcia, alma, td, numdoc, item, "01210004") * factor;
                    dr[11] = mov4;
                    saldo4 = saldo4 + mov4;

                    mov5 = getCantidadPorMovimiento(codcia, alma, td, numdoc, item, "01210005") * factor;
                    dr[12] = mov5;
                    saldo5 = saldo5 + mov5;

                    dr[13] = saldo1;
                    dr[14] = saldo2;
                    dr[15] = saldo3;
                    dr[16] = saldo4;
                    dr[17] = saldo5;

                    dtreporte.Rows.Add(dr);
                }

            }
                       
           
            ui_Lista(dtreporte);
        }

        private string ui_valida()
        {
            string valorValida = "G";

            if (cmbAlmacen.Text == string.Empty)
            {
                MessageBox.Show("No ha definido Almacén", "Aviso Importante", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                valorValida = "B";
            }

            if (txtLote.Text.Trim() == string.Empty)
            {
                MessageBox.Show("No ha especificado Lote", "Aviso Importante", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                valorValida = "B";
                txtLote.Focus();
            }

            return valorValida;
        }

        private void ui_Lista(DataTable dt)
        {
           
                    Funciones funciones = new Funciones();
                    funciones.formatearDataGridView(dgvdetalle);
                    dgvdetalle.DataSource = dt;
                    dgvdetalle.Columns[0].HeaderText = "Fecha Doc.";
                    dgvdetalle.Columns[1].HeaderText = "Mov";
                    dgvdetalle.Columns[2].HeaderText = "Nro.Mov.";
                    dgvdetalle.Columns[3].HeaderText = "T. Doc.";
                    dgvdetalle.Columns[4].HeaderText = "Nro.Doc.";
                    dgvdetalle.Columns[5].HeaderText = "Glosa";
                    dgvdetalle.Columns[6].HeaderText = "Mon.";
                    dgvdetalle.Columns[7].HeaderText = "P.Unit.";
                    dgvdetalle.Columns[8].HeaderText =  "Mango + Maracuyá";
                    dgvdetalle.Columns[9].HeaderText =  "Mango + Durazno";
                    dgvdetalle.Columns[10].HeaderText = "Mango + Sandía";
                    dgvdetalle.Columns[11].HeaderText = "Piña + Maracuyá";
                    dgvdetalle.Columns[12].HeaderText = "Durazno + Maracuyá";
                    dgvdetalle.Columns[13].HeaderText = "Mango + Maracuyá";
                    dgvdetalle.Columns[14].HeaderText = "Mango + Durazno";
                    dgvdetalle.Columns[15].HeaderText = "Mango + Sandía";
                    dgvdetalle.Columns[16].HeaderText = "Piña + Maracuyá";
                    dgvdetalle.Columns[17].HeaderText = "Durazno + Maracuyá";
                  


                    dgvdetalle.Columns[0].Width  = 75;
                    dgvdetalle.Columns[1].Width  = 30;
                    dgvdetalle.Columns[2].Width  = 80;
                    dgvdetalle.Columns[3].Width  = 30;
                    dgvdetalle.Columns[4].Width  = 90;
                    dgvdetalle.Columns[5].Width  = 180;
                    dgvdetalle.Columns[6].Width  = 50;
                    dgvdetalle.Columns[7].Width  = 60;
                    dgvdetalle.Columns[8].Width  = 60;
                    dgvdetalle.Columns[9].Width  = 60;
                    dgvdetalle.Columns[10].Width = 60;
                    dgvdetalle.Columns[11].Width = 60;
                    dgvdetalle.Columns[12].Width = 60;
                    dgvdetalle.Columns[13].Width = 60;
                    dgvdetalle.Columns[14].Width = 60;
                    dgvdetalle.Columns[15].Width = 60;
                    dgvdetalle.Columns[16].Width = 60;
                    dgvdetalle.Columns[17].Width = 60;
                  
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        
        private void btnConsultar_Click(object sender, EventArgs e)
        {
            string valorValida = ui_valida();

            if (valorValida.Equals("G"))
            {
                ui_listaReporte();
            }
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            Exporta exporta = new Exporta();
            exporta.Excel_FromDataGridView(dgvdetalle);
        }

        private void btnPDF_Click(object sender, EventArgs e)
        {
            Exporta exporta = new Exporta();
            exporta.Pdf_FromDataGridView(dgvdetalle,2);
        }

        private void txtLote_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(Keys.Enter))
            {
                    e.Handled = true;
                    btnConsultar.Focus();
            }
        }

        private void txtLote_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.F2)
            {
                    

                    Funciones funciones=new Funciones();
                    string codcia = this._codcia;
                    string alma = funciones.getValorComboBox(cmbAlmacen, 2);
                    
                    string query = " select distinct A.numdoc as codigo,A.glosa1 as descripcion from almovc A ";
                    query = query + " where A.td='PE' and A.codcia='" + @codcia + "' and A.alma='" + @alma + "' ";
                    query = query + " order by A.fecdoc desc;";

                    this._TextBoxActivo = txtLote;
                    ui_viewmaestros ui_viewmaestros = new ui_viewmaestros();
                    ui_viewmaestros.setData(query, "ui_lotegen", "Seleccionar un Grupo de Lotes");
                    ui_viewmaestros._FormPadre = this;
                    ui_viewmaestros.BringToFront();
                    ui_viewmaestros.ShowDialog();
                    ui_viewmaestros.Dispose();
            }
        }

       
        private void btnVisualizar_Click(object sender, EventArgs e)
        {
            Int32 selectedCellCount = dgvdetalle.GetCellCount(DataGridViewElementStates.Selected);
            if (selectedCellCount > 0)
            {
                Funciones funciones = new Funciones();

                string desalma = cmbAlmacen.Text;
                string codcia = this._codcia;
                string alma = funciones.getValorComboBox(cmbAlmacen, 2);
                string td = dgvdetalle.Rows[dgvdetalle.SelectedCells[1].RowIndex].Cells[1].Value.ToString();
                string numdoc = dgvdetalle.Rows[dgvdetalle.SelectedCells[1].RowIndex].Cells[2].Value.ToString();
                ui_updalmov ui_updalmov = new ui_updalmov();
                ui_updalmov._FormPadre = this;
                ui_updalmov.Activate();
                ui_updalmov.BringToFront();
                ui_updalmov.setData(codcia,alma, desalma);
                ui_updalmov.ui_ActualizaComboBox();
                ui_updalmov.editar(codcia,alma, td, numdoc);
                ui_updalmov.ShowDialog();
                ui_updalmov.Dispose();
            }
            else
            {
                MessageBox.Show("No ha seleccionado registro a Valorizar", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void dgvdetalle_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if ((this.dgvdetalle.Rows[e.RowIndex].Cells["td"].Value).ToString().Equals("PE"))
            {
                foreach (DataGridViewCell celda in

                this.dgvdetalle.Rows[e.RowIndex].Cells)
                {

                    celda.Style.BackColor = Color.Yellow;
                    celda.Style.ForeColor = Color.Black;

                }
            }
        }

        

      
    }
}
