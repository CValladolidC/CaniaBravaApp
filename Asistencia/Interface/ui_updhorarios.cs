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
using System.Globalization;

namespace CaniaBrava
{
    public partial class ui_updhorarios : Form
    {
        Funciones fn = new Funciones();

        private string Evento { get; set; }
        private bool _continua { get; set; }
        DataTable datos = new DataTable();
        public Form _FormPadre { get; set; }
        public TextBox _TextBoxActivo { get; set; }

        public ui_updhorarios()
        {
            InitializeComponent();

            if (fn.VersionAssembly()) Application.ExitThread();
        }

        public void NuevoTipHor()
        {
            this.Evento = "NUEVO";
            //txtIdTipHor.Text = GetMaxIdTipoHorario();
            //txtIdTipHor.Enabled = false;
            txtIdTipHor.Focus();
            Get_Nominas(string.Empty);
        }

        public void asgHorUpdLoad(string codTipHor, string descripcion, string nominas)
        {
            txtIdTipHor.Enabled = false;

            txtIdTipHor.Text = codTipHor;
            txtNameTipHor.Text = descripcion;

            GetHorarios();

            txtNameTipHor.Focus();
            btnMore.Visible = true;
            dgvHorarios.Enabled = true;
        }

        private string GetMaxIdTipoHorario()
        {
            string resultado = string.Empty;
            string query = "SELECT MAX(idplantiphorario) + 1  AS resultado FROM plantiphorario (NOLOCK) ";
            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();

            try
            {
                SqlCommand myCommand = new SqlCommand(query, conexion);
                SqlDataReader odr = myCommand.ExecuteReader();

                while (odr.Read())
                {
                    resultado = odr.GetInt32(odr.GetOrdinal("resultado")).ToString();
                }

                odr.Close();
                myCommand.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            finally { conexion.Close(); }

            return resultado;
        }

        private void Get_Nominas(string nominas)
        {
            DataTable dt = new DataTable();
            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();

            string query = "select idtipoper as id,destipoper as descripcion from tipoper (NOLOCK) ";
            //query += "WHERE destipoper NOT LIKE '%EMPLEADOS%'";
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = new SqlCommand(query, conexion);
            da.Fill(dt);
            Funciones funciones = new Funciones();
            OptMenu optmenu = new OptMenu();
            chkListNominas.Items.Clear();

            foreach (DataRow row_dtMenu in dt.Rows)
            {
                string cod = row_dtMenu["id"].ToString();
                string des = row_dtMenu["descripcion"].ToString();
                string descripcion = funciones.replicateCadena(" ", (2 * cod.Length)) + (des + funciones.replicateCadena(" ", 200)).Substring(0, 200) + cod;

                chkListNominas.Items.Add(descripcion, (nominas.Contains(cod) ? CheckState.Checked : CheckState.Unchecked));
            }
        }

        private List<HorarioPer> pr_ListHorario(string codTipHor, ref string nameTipHor, ref string rotativo)
        {
            HorarioPer HorarioBE = null;
            List<HorarioPer> ListHorario = new List<HorarioPer>();
            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");

            conexion.Open();
            try
            {
                using (SqlCommand cmd = new SqlCommand("sp_ListarHorarios", conexion))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@v_idplantiphorario", codTipHor);

                    cmd.Parameters.Add("@v_descripcion", SqlDbType.VarChar, 100);
                    cmd.Parameters["@v_descripcion"].Direction = ParameterDirection.Output;

                    cmd.Parameters.Add("@v_rotativo", SqlDbType.VarChar, 2);
                    cmd.Parameters["@v_rotativo"].Direction = ParameterDirection.Output;

                    using (SqlDataReader odr = cmd.ExecuteReader())
                    {
                        if (odr.HasRows)
                        {
                            while (odr.Read())
                            {
                                HorarioBE = new HorarioPer();
                                HorarioBE.dias_semana = int.Parse(odr["iddias_semana"].ToString());
                                HorarioBE.h_entrada = odr["hor_entrada"].ToString();
                                HorarioBE.h_salida = odr["hor_salida"].ToString();
                                HorarioBE.nominas = odr["nominas"].ToString();
                                ListHorario.Add(HorarioBE);
                            }
                        }
                    }
                    nameTipHor = (string)cmd.Parameters["@v_descripcion"].Value;
                    rotativo = (string)cmd.Parameters["@v_rotativo"].Value;
                }
            }
            catch
            {
                MessageBox.Show("Error al visualizar Horario del Personal", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }

            return ListHorario;
        }

        private void txtIdTipHor_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                e.Handled = true;
                txtNameTipHor.Focus();
            }
        }

        private void txtNameTipHor_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                e.Handled = true;
            }
        }

        private int validaCampos()
        {
            int result = 1;
            //if (chkListNominas.CheckedItems.Count == 0) { return result = 12; }
            if (txtIdTipHor.Text.Equals(string.Empty)) { return result = 11; }
            if (txtNameTipHor.Text.Equals(string.Empty)) { return result = 10; }
            return result;
        }

        private void btnMore_Click(object sender, EventArgs e)
        {
            datos = ((DataTable)dgvHorarios.DataSource).Copy();
            datos.Rows.Add(new object[] { datos.Rows.Count + 1, "", "", "" });

            dgvHorarios.DataSource = datos;
        }

        private void ui_updhorarios_Load(object sender, EventArgs e)
        {
            if (dgvHorarios.Rows.Count > 0)
            {
                dgvHorarios.Rows[0].Selected = true;
            }

            _continua = true;
        }

        private void GetHorarios()
        {
            string codigo = txtIdTipHor.Text;
            Funciones funciones = new Funciones();
            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();

            string query = "SELECT iddias_semana [Codigo], LEFT(hor_entrada,5) [Entrada], LEFT(hor_salida,5) [Salida],nominas FROM plantiphorariodet (NOLOCK) ";
            query += "WHERE idplantiphorario = '" + @codigo + "' ORDER BY Entrada,Salida ";
            using (SqlDataAdapter myDataAdapter = new SqlDataAdapter(query, conexion))
            {
                try
                {
                    DataSet myDataSet = new DataSet();
                    myDataAdapter.Fill(myDataSet);
                    //funciones.formatearDataGridView(dgvHorarios);
                    dgvHorarios.DataSource = myDataSet.Tables[0];
                    dgvHorarios.Columns[0].Width = 50;
                    dgvHorarios.Columns[1].Width = 65;
                    dgvHorarios.Columns[2].Width = 65;

                    //dgvHorarios.RowHeadersVisible = false;
                    dgvHorarios.AllowUserToAddRows = false;
                    dgvHorarios.MultiSelect = false;
                    dgvHorarios.Columns[0].ReadOnly = true;
                    dgvHorarios.Columns[3].Visible = false;

                    DataGridViewCellStyle style = new DataGridViewCellStyle();
                    style.BackColor = Color.LightGray;
                    dgvHorarios.Columns[1].DefaultCellStyle = style;
                    dgvHorarios.Columns[2].DefaultCellStyle = style;
                    dgvHorarios.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    dgvHorarios.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    ((DataGridViewTextBoxColumn)dgvHorarios.Columns[1]).MaxInputLength = 5;
                    ((DataGridViewTextBoxColumn)dgvHorarios.Columns[2]).MaxInputLength = 5;

                    dgvHorarios.AllowUserToResizeRows = false;
                    dgvHorarios.AllowUserToResizeColumns = false;
                    foreach (DataGridViewColumn column in dgvHorarios.Columns)
                    {
                        column.SortMode = DataGridViewColumnSortMode.NotSortable;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
                conexion.Close();
            }
        }

        private void SaveHorariodet(string codigo, string id, DateTime hora01, DateTime hora02, string nominas)
        {
            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            string query = string.Empty;

            query = "EXEC SP_UPDATEHORARIOSDET '" + codigo.Trim() + "','" + id + "','" + hora01.ToString("HH:mm") + "','" + hora02.ToString("HH:mm") + "','" + nominas + "';";
            conexion.Open();

            try
            {
                SqlCommand myCommand = new SqlCommand(query, conexion);
                myCommand.ExecuteNonQuery();
                myCommand.Dispose();
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            conexion.Close();
        }

        private void dgvHorarios_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (dgvHorarios.RowCount > 0)
                {
                    string codigo = txtIdTipHor.Text.Trim();
                    string id = dgvHorarios.Rows[e.RowIndex].Cells[0].Value.ToString();
                    string hora1 = string.Empty, hora2 = string.Empty;
                    string reg1 = dgvHorarios.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
                    string reg2 = dgvHorarios.Rows[e.RowIndex].Cells[(e.ColumnIndex == 1 ? 2 : 1)].Value.ToString();

                    if (e.ColumnIndex == 1) { hora1 = reg1; hora2 = reg2; }
                    else { hora1 = reg2; hora2 = reg1; }

                    if (reg1.Trim() != string.Empty)
                    {
                        DateTime Hora01, Hora02;
                        DateTime.TryParseExact(hora1, "HH:mm", CultureInfo.InvariantCulture, DateTimeStyles.None, out Hora01);
                        DateTime.TryParseExact(hora2, "HH:mm", CultureInfo.InvariantCulture, DateTimeStyles.None, out Hora02);

                        if (Hora01.ToString("HH:mm") == "00:00")
                        {
                            dgvHorarios.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = string.Empty;
                            MessageBox.Show("Hora de inicio invalido.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }

                        switch (codigo)
                        {
                            case "D":
                                if (Hora01.Hour > 11)
                                {
                                    dgvHorarios.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = string.Empty;
                                    MessageBox.Show("Los Horarios de Dia son entre 00:00am a 11:59am", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    return;
                                }
                                break;
                            case "T":
                                if (Hora01.Hour < 12 || Hora01.Hour > 16)
                                {
                                    dgvHorarios.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = string.Empty;
                                    MessageBox.Show("Los Horarios de Tarde son entre 12:00pm a 05:59pm", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    return;
                                }
                                break;
                            case "N":
                                if (Hora01.Hour < 18)
                                {
                                    dgvHorarios.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = string.Empty;
                                    MessageBox.Show("Los Horarios de Noche son entre 06:00pm a 12:59pm", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    return;
                                }
                                break;
                        }

                        if (hora2 != string.Empty)
                        {
                            if (Hora02.ToString("HH:mm") == "00:00")
                            {
                                dgvHorarios.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = string.Empty;
                                MessageBox.Show("Valor de hora invalida.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }

                            switch (codigo)
                            {
                                case "D":
                                case "T":
                                    if (Hora01 > Hora02)
                                    {
                                        dgvHorarios.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = string.Empty;
                                        MessageBox.Show("La hora inicial debe ser menor a la hora final", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                        return;
                                    }
                                    break;
                                case "N":
                                    if (Hora01 < Hora02)
                                    {
                                        dgvHorarios.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = string.Empty;
                                        MessageBox.Show("La hora inicial debe ser mayor a la hora final", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                        return;
                                    }
                                    break;
                            }

                            if (VerificarDupliRowsDB(codigo, hora1, hora2, e.ColumnIndex) > 0)
                            {
                                dgvHorarios.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = string.Empty;
                                MessageBox.Show("Horario duplicado. Favor de Verificar.!!", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }

                            SaveHorariodet(codigo, id, Hora01, Hora02, SetNominas());
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvHorarios_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            DialogResult response = MessageBox.Show("¿Desea eliminar Horario?", "Accion", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
            if (response == DialogResult.Yes)
            {
                Int32 selectedCellCount = dgvHorarios.GetCellCount(DataGridViewElementStates.Selected);
                if (selectedCellCount > 0)
                {
                    string codigo = txtIdTipHor.Text.Trim();
                    string id = dgvHorarios.Rows[dgvHorarios.SelectedCells[1].RowIndex].Cells[0].Value.ToString();

                    string query = string.Empty;

                    SqlConnection conexion = new SqlConnection();
                    conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
                    conexion.Open();

                    if (ValidaUspHorario(codigo, id))
                    {
                        query = "DELETE FROM plantiphorariodet WHERE idplantiphorario = '" + @codigo + "' AND iddias_semana = '" + @id + "';";

                        try
                        {
                            SqlCommand myCommand = new SqlCommand(query, conexion);
                            myCommand.ExecuteNonQuery();
                            myCommand.Dispose();
                            GetHorarios();
                        }
                        catch (SqlException ex)
                        {
                            MessageBox.Show(ex.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        }
                        conexion.Close();
                    }
                    else { MessageBox.Show("El Horario ya está asignado a una programación", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop); }
                }
            }
            e.Cancel = true;
        }

        private void dgvHorarios_SelectionChanged(object sender, EventArgs e)
        {
            _continua = false;
            var rowsCount = dgvHorarios.SelectedRows.Count;
            if (rowsCount == 0 || rowsCount > 1) return;

            var row = dgvHorarios.SelectedRows[0];
            if (row == null) return;
            Get_Nominas(row.Cells[3].Value.ToString());
        }

        private bool ValidaUspHorario(string codigo, string id)
        {
            bool resultado = true;
            string query = "SELECT COUNT(1) AS result FROM progdet (NOLOCK) ";
            query += "WHERE idsap='" + codigo + id + "'";

            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();

            try
            {
                SqlCommand myCommand = new SqlCommand(query, conexion);
                SqlDataReader odr = myCommand.ExecuteReader();

                while (odr.Read())
                {
                    resultado = (odr.GetInt32(odr.GetOrdinal("result")) > 0 ? false : true);
                }

                odr.Close();
                myCommand.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            finally
            {
                conexion.Close();
            }

            return resultado;
        }

        private int VerificarDupliRowsDB(string codigo, string hora1, string hora2, int param)
        {
            int resultado = 0;
            string query = "SELECT COUNT(1) AS result FROM plantiphorariodet (NOLOCK) ";
            query += "WHERE idplantiphorario='" + codigo + "' ";
            query += "AND hor_entrada='" + hora1 + "' AND hor_salida='" + hora2 + "'";

            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();

            try
            {
                SqlCommand myCommand = new SqlCommand(query, conexion);
                SqlDataReader odr = myCommand.ExecuteReader();

                while (odr.Read())
                {
                    resultado = odr.GetInt32(odr.GetOrdinal("result"));
                }

                odr.Close();
                myCommand.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            finally
            {
                conexion.Close();
            }

            return resultado;
        }

        private int VerificarDupliRows(string id, string hora1, string hora2, bool param)
        {
            int resultado = 0;

            foreach (DataRow item in datos.Rows)
            {
                if (param)
                {
                    if (item[0].ToString() != id && item[2].ToString() == hora1 && item[1].ToString() == hora2)
                    {
                        resultado = 1;
                        break;
                    }
                }
                else
                {
                    if (item[0].ToString() != id && item[1].ToString() == hora1 && item[2].ToString() == hora2)
                    {
                        resultado = 1;
                        break;
                    }
                }
            }

            return resultado;
        }

        private void btnAsignar_Click(object sender, EventArgs e)
        {
            GlobalVariables variables = new GlobalVariables();

            int result = validaCampos();
            if (result == 1)
            {
                SqlConnection conexion = new SqlConnection();
                conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
                string query = "EXEC SP_UPDATEHORARIOS '" + txtIdTipHor.Text + "','" + txtNameTipHor.Text + "','';";

                conexion.Open();

                try
                {
                    SqlCommand myCommand = new SqlCommand(query, conexion);
                    myCommand.ExecuteNonQuery();
                    myCommand.Dispose();

                    string mensaje = "Proceso completo....";
                    if (this.Evento == "NUEVO")
                    {
                        mensaje += " Puede agregar horarios";
                    }
                    MessageBox.Show(mensaje, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ((ui_horarios)_FormPadre).btnActualizar.PerformClick();

                    if (this.Evento != "NUEVO")
                    {
                        this.Close();
                    }
                    btnMore.Visible = true;
                    dgvHorarios.Enabled = true;
                }
                catch (SqlException ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
                conexion.Close();
            }
            else
            {
                if (result == 12) { MessageBox.Show("Debe elegir al menos una Nomina", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop); }
                if (result == 11) { MessageBox.Show("Codigo Tipo de Horario Obligatorio", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop); }
                if (result == 10) { MessageBox.Show("Nombre Tipo de Horario Obligatorio", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop); }
            }
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void chkListNominas_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            Int32 selectedCellCount = dgvHorarios.GetCellCount(DataGridViewElementStates.Selected);
            if (selectedCellCount > 0)
            {
                var control = (CheckedListBox)sender;
                if (control.SelectedItem != null)
                {
                    List<string> checkedItems = new List<string>();
                    foreach (var item in chkListNominas.CheckedItems) { checkedItems.Add(item.ToString()); }

                    if (e.NewValue == CheckState.Checked) { checkedItems.Add(chkListNominas.Items[e.Index].ToString()); }
                    else { checkedItems.Remove(chkListNominas.Items[e.Index].ToString()); }

                    string nominas = string.Empty;
                    foreach (var item in checkedItems)
                    {
                        nominas += item.ToString().Substring(200, item.ToString().Length - 200).Trim() + " / ";
                    }

                    if (nominas.Length > 0)
                    {
                        nominas = nominas.Substring(0, nominas.Length - 3);
                    }

                    string codigo = txtIdTipHor.Text.Trim();
                    string id = dgvHorarios.Rows[dgvHorarios.SelectedCells[1].RowIndex].Cells[0].Value.ToString();
                    string hora1 = dgvHorarios.Rows[dgvHorarios.SelectedCells[1].RowIndex].Cells[1].Value.ToString();
                    string hora2 = dgvHorarios.Rows[dgvHorarios.SelectedCells[1].RowIndex].Cells[2].Value.ToString();
                    string nomin = dgvHorarios.Rows[dgvHorarios.SelectedCells[1].RowIndex].Cells[3].Value.ToString();

                    DateTime Hora01, Hora02;
                    DateTime.TryParseExact(hora1, "HH:mm", CultureInfo.InvariantCulture, DateTimeStyles.None, out Hora01);
                    DateTime.TryParseExact(hora2, "HH:mm", CultureInfo.InvariantCulture, DateTimeStyles.None, out Hora02);

                    SaveHorariodet(codigo, id, Hora01, Hora02, nominas);
                }
            }
        }

        private string SetNominas()
        {
            string nominas = string.Empty;
            foreach (var item in chkListNominas.CheckedItems)
            {
                nominas += item.ToString().Substring(200, item.ToString().Length - 200).Trim() + " / ";
            }

            if (nominas.Length > 0)
            {
                nominas = nominas.Substring(0, nominas.Length - 3);
            }

            return nominas;
        }
    }
}