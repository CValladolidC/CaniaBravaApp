using System;
using System.Data;
using System.Windows.Forms;
using System.Configuration;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace CaniaBrava
{
    public partial class ui_programacion : Form
    {
        //Oliver Cruz Tuanama
        string bd_prov = ConfigurationManager.AppSettings.Get("BD_PROV");
        string query = "";
        GlobalVariables variables = new GlobalVariables();
        Funciones funciones = new Funciones();
        SqlConnection conexion = new SqlConnection();

        private ui_updprogramacion form = null;

        private ui_updprogramacion FormInstance
        {
            get
            {
                if (form == null)
                {
                    form = new ui_updprogramacion();
                    form.Disposed += new EventHandler(form_Disposed);
                }

                return form;
            }
        }

        void form_Disposed(object sender, EventArgs e) { form = null; }

        public ui_programacion()
        {
            InitializeComponent();

            if (funciones.VersionAssembly()) Application.ExitThread();
        }

        private void ui_programacion_Load(object sender, EventArgs e)
        {
            btnAprobar.Visible = false;
            btnRechazar.Visible = false;
            btnVerificar.Visible = false;
            if (variables.getValorTypeUsr() == "03") { btnVerificar.Visible = true; }
            if (variables.getValorTypeUsr() == "02") { btnAprobar.Visible = true; btnRechazar.Visible = true; btnVerificar.Visible = true; }
            if (variables.getValorTypeUsr() == "00") { btnAprobar.Visible = true; btnRechazar.Visible = true; btnVerificar.Visible = true; }

            query = "SELECT idtipoper as clave,destipoper as descripcion FROM tipoper order by 1 asc;";
            MaesGen maesgen = new MaesGen();
            cmbSituacion.Text = "X   TODOS";
            ui_ListaProg();
        }

        private void ui_ListaProgExcel(string id)
        {
            string situacion = funciones.getValorComboBox(cmbSituacion, 1);
            string cadenasituacion = string.Empty;

            if (situacion != "X") cadenasituacion = " and estado = '" + @situacion + "' ";

            query = " SELECT ''''+idperplan AS Codigo,''''+nrodoc AS DNI,destrabajador AS Trabajador,destipohorario AS [Tipo Horario], ";
            query += "CONVERT(VARCHAR(10),fechadiaria,120) AS Fecha,LOWER(RIGHT(CONVERT(VARCHAR, fechaini ,100), 7)) AS [Hora Ingreso],";
            query += "LOWER(RIGHT(CONVERT(VARCHAR, fechafin ,100), 7)) AS [Hora Salida], ";
            query += "(CASE turnonoche WHEN 1 THEN 'NOCTURNO' ELSE 'DIURNO' END) AS Turno ";
            query += "FROM progdet WHERE idprog = '" + @id + "' ORDER BY fechadiaria ";

            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();

            try
            {
                using (SqlDataAdapter myDataAdapter = new SqlDataAdapter(query, conexion))
                {
                    DataSet myDataSet = new DataSet();
                    myDataAdapter.Fill(myDataSet, "tblProgramacion");
                    funciones.formatearDataGridView(dgvdetalleExcel);

                    dgvdetalleExcel.DataSource = myDataSet.Tables["tblProgramacion"];
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }

            conexion.Close();
        }

        private void ui_ListaProg()
        {
            string situacion = funciones.getValorComboBox(cmbSituacion, 1);
            string cadenasituacion = string.Empty;

            if (situacion != "X") cadenasituacion = " and estado = '" + @situacion + "' ";

            query = " SELECT a.idprog,a.desprog,CONVERT(VARCHAR(10), a.fechaini, 120) AS [Fecha Inicio],CONVERT(VARCHAR(10), a.fechafin, 120) AS [Fecha Fin],";
            query += "(CASE a.estado WHEN 'P' THEN 'PENDIENTE DE APROBACION' WHEN 'V' THEN 'VIGENTE' WHEN 'A' THEN 'APROBADO' WHEN 'R' THEN 'RECHAZADO' END) AS Estado";
            query += ",a.idusr_chk,a.idusrins,a.fechains,a.idusrupd,a.fechaupd,a.estado as idestado,ISNULL(c.desusr,'') AS Programador,ISNULL(b.desusr,'') AS Jefatura ";
            query += "FROM prog a (NOLOCK)";
            query += "LEFT JOIN usrfile b (NOLOCK) ON b.idusr = a.idusr_chk AND b.stateusr = 'V' ";
            query += "LEFT JOIN usrfile c (NOLOCK) ON c.idusr = a.idusrins AND c.stateusr = 'V' ";
            query += "where 1 = 1 " + @cadenasituacion;
            if (variables.getValorTypeUsr() != "00")
            {
                query += " and a.idusrins IN (select distinct idusr from cencosusr where idcencos in (";
                query += "select idcencos from cencosusr where idusr = '" + variables.getValorUsr() + "')) ";
            }
            query += "ORDER BY a.fechaini ";

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
                    myDataAdapter.Fill(myDataSet, "tblProgramacion");
                    funciones.formatearDataGridView(dgvdetalle);

                    dgvdetalle.DataSource = myDataSet.Tables["tblProgramacion"];
                    dgvdetalle.Columns[1].HeaderText = "Programación";

                    dgvdetalle.Columns[1].Width = 230;
                    dgvdetalle.Columns[2].Width = 75;
                    dgvdetalle.Columns[3].Width = 69;
                    dgvdetalle.Columns[4].Width = 170;
                    dgvdetalle.Columns[11].Width = 220;
                    dgvdetalle.Columns[12].Width = 220;

                    dgvdetalle.Columns[0].Visible = false;
                    dgvdetalle.Columns[5].Visible = false;
                    dgvdetalle.Columns[6].Visible = false;
                    dgvdetalle.Columns[7].Visible = false;
                    dgvdetalle.Columns[8].Visible = false;
                    dgvdetalle.Columns[9].Visible = false;
                    dgvdetalle.Columns[10].Visible = false;

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
        }

        private void cmbSituacion_SelectedIndexChanged(object sender, EventArgs e) { ui_ListaProg(); }

        #region _Click
        private void btnSalir_Click(object sender, EventArgs e) { Close(); }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            ui_updprogramacion ui_detalle = this.FormInstance;
            ui_detalle._FormPadre = this;
            ui_detalle.ActualizaComboBox("0");
            ui_detalle.NuevoProg();
            ui_detalle.Activate();
            ui_detalle.BringToFront();
            ui_detalle.ShowDialog();
            ui_detalle.Dispose();
        }

        private void btnActualizar_Click(object sender, EventArgs e) { ui_ListaProg(); }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            GlobalVariables variables = new GlobalVariables();
            string idcia = variables.getValorCia();
            Int32 selectedCellCount = dgvdetalle.GetCellCount(DataGridViewElementStates.Selected);
            if (selectedCellCount > 0)
            {
                string idprog = dgvdetalle.Rows[dgvdetalle.SelectedCells[1].RowIndex].Cells[0].Value.ToString();
                string fechaini = dgvdetalle.Rows[dgvdetalle.SelectedCells[1].RowIndex].Cells[2].Value.ToString();
                string fechafin = dgvdetalle.Rows[dgvdetalle.SelectedCells[1].RowIndex].Cells[3].Value.ToString();
                string estado = dgvdetalle.Rows[dgvdetalle.SelectedCells[1].RowIndex].Cells[4].Value.ToString();
                string idestado = dgvdetalle.Rows[dgvdetalle.SelectedCells[1].RowIndex].Cells[10].Value.ToString();

                if (DateTime.Parse(fechaini) <= DateTime.Now.AddDays(1) || DateTime.Parse(fechafin) <= DateTime.Now.AddDays(1))
                {
                    if (DateTime.Parse(fechaini) == DateTime.Now.AddDays(1) || DateTime.Parse(fechafin) == DateTime.Now.AddDays(1))
                    {
                        MessageBox.Show("No se puede editar el registro. La programacion se ejecutará mañana.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }

                    if (DateTime.Parse(fechaini) == DateTime.Now || DateTime.Parse(fechafin) == DateTime.Now)
                    {
                        MessageBox.Show("No se puede editar el registro. La programacion ya se está ejecutando.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                }

                if (variables.getValorTypeUsr() != "00")
                {
                    if (idestado == "P" || idestado == "A")
                    {
                        MessageBox.Show("El registro esta en estado \"" + estado + "\". Solo lectura.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }

                ui_updprogramacion ui_detalle = this.FormInstance;
                ui_detalle._FormPadre = this;
                ui_detalle.ActualizaComboBox(idprog);
                ui_detalle.Activate();
                ui_detalle.EditarProg(idprog, idestado);
                ui_detalle.BringToFront();
                ui_detalle.ShowDialog();
                ui_detalle.Dispose();
            }
            else
            {
                MessageBox.Show("No ha seleccionado registro a Editar", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            Int32 selectedCellCount = dgvdetalle.GetCellCount(DataGridViewElementStates.Selected);
            if (selectedCellCount > 0)
            {
                string idprog = dgvdetalle.Rows[dgvdetalle.SelectedCells[1].RowIndex].Cells[0].Value.ToString();
                ui_ListaProgExcel(idprog);

                Exporta exporta = new Exporta();
                exporta.Excel_FromDataGridView(dgvdetalleExcel);
            }
            else
            {
                MessageBox.Show("No existe ningun registro a exportar.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnVerificar_Click(object sender, EventArgs e)
        {
            Int32 selectedCellCount = dgvdetalle.GetCellCount(DataGridViewElementStates.Selected);
            if (selectedCellCount > 0)
            {
                string idprog = dgvdetalle.Rows[dgvdetalle.SelectedCells[1].RowIndex].Cells[0].Value.ToString();
                string desprog = dgvdetalle.Rows[dgvdetalle.SelectedCells[1].RowIndex].Cells[1].Value.ToString();
                string fecini = dgvdetalle.Rows[dgvdetalle.SelectedCells[1].RowIndex].Cells[2].Value.ToString();
                string fecfin = dgvdetalle.Rows[dgvdetalle.SelectedCells[1].RowIndex].Cells[3].Value.ToString();
                string estado = dgvdetalle.Rows[dgvdetalle.SelectedCells[1].RowIndex].Cells[4].Value.ToString();
                string idestado = dgvdetalle.Rows[dgvdetalle.SelectedCells[1].RowIndex].Cells[10].Value.ToString();
                string to = dgvdetalle.Rows[dgvdetalle.SelectedCells[1].RowIndex].Cells[11].Value.ToString();

                if (idestado == "V" || idestado == "R")
                {
                    var confirmResult = MessageBox.Show("¿Esta seguro en \"Solicitar Aprobación\" del registro seleccionado?", "Confirmación", MessageBoxButtons.YesNo);
                    if (confirmResult == DialogResult.Yes)
                    {
                        string query = "UPDATE prog SET estado='P' WHERE idprog='" + idprog + "';";
                        query += "UPDATE progdet SET estado='P' WHERE idprog='" + idprog + "';";
                        try
                        {
                            if (variables.getValorTypeUsr() != "00")
                            {
                                SendEnviarCorreo(variables.getValorUsr(), variables.getValorTypeUsr(), variables.getValorUsrMail(),
                                    "SISASIS - Solicitud de Aprobación de Programación", "Se solicita aprobación de la siguiente Programación." +
                                    "<br><br><b>Programación:</b> " + desprog
                                    + "<br><b>Desde:</b> " + fecini + "<br><b>Hasta:</b> " + fecfin +
                                    "<br><b>Comentario:</b> Ingresar a SISASIS-Desktop, para realizar su \"Aprobación o Rechazo\"",
                                    variables.getValorUsrName(), to, false);
                            }

                            SqlConnection conexion = new SqlConnection();
                            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
                            conexion.Open();
                            SqlCommand myCommand = new SqlCommand(query, conexion);
                            myCommand.ExecuteNonQuery();
                            myCommand.Dispose();
                        }
                        catch (SqlException ex)
                        {
                            MessageBox.Show(ex.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        }
                        conexion.Close();
                        ui_ListaProg();
                    }
                }
                else
                {
                    MessageBox.Show("Debe seleccionar un registro en estado \"VIGENTE\"", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
            }
            else
            {
                MessageBox.Show("No ha seleccionado registro a Solicitar Aprobación", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void btnAprobar_Click(object sender, EventArgs e)
        {
            Int32 selectedCellCount = dgvdetalle.GetCellCount(DataGridViewElementStates.Selected);
            if (selectedCellCount > 0)
            {
                string idprog = dgvdetalle.Rows[dgvdetalle.SelectedCells[1].RowIndex].Cells[0].Value.ToString();
                string desprog = dgvdetalle.Rows[dgvdetalle.SelectedCells[1].RowIndex].Cells[1].Value.ToString();
                string fecini = dgvdetalle.Rows[dgvdetalle.SelectedCells[1].RowIndex].Cells[2].Value.ToString();
                string fecfin = dgvdetalle.Rows[dgvdetalle.SelectedCells[1].RowIndex].Cells[3].Value.ToString();
                string estado = dgvdetalle.Rows[dgvdetalle.SelectedCells[1].RowIndex].Cells[4].Value.ToString();
                string idestado = dgvdetalle.Rows[dgvdetalle.SelectedCells[1].RowIndex].Cells[10].Value.ToString();
                string to = dgvdetalle.Rows[dgvdetalle.SelectedCells[1].RowIndex].Cells[11].Value.ToString();

                if (idestado == "P")
                {
                    var confirmResult = MessageBox.Show("¿Esta seguro en \"APROBAR\" el registro seleccionado?", "Confirmación", MessageBoxButtons.YesNo);
                    if (confirmResult == DialogResult.Yes)
                    {
                        string query = "update prog set idusr_chk = '" + variables.getValorUsr() + "', estado='A', ";
                        query += " fechachk=GETDATE() where idprog='" + idprog + "';";
                        query += "UPDATE progdet SET estado='A' WHERE idprog='" + idprog + "';";
                        try
                        {
                            if (variables.getValorTypeUsr() != "00")
                            {
                                SendEnviarCorreo(variables.getValorUsr(), variables.getValorTypeUsr(), variables.getValorUsrMail(),
                                    "SISASIS - Solicitud de Aprobación Programación", "<b>Programación:</b> " + desprog +
                                    "<br><b>Desde:</b> " + fecini + "<br><b>Hasta:</b> " + fecfin +
                                    "<br><b>Solicitud de Programación:</b> <span style='color:#009E41'><b>APROBADA</b></span>",
                                    variables.getValorUsrName(), to, true);
                            }

                            SqlConnection conexion = new SqlConnection();
                            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
                            conexion.Open();
                            SqlCommand myCommand = new SqlCommand(query, conexion);
                            myCommand.ExecuteNonQuery();
                            myCommand.Dispose();
                        }
                        catch (SqlException ex)
                        {
                            MessageBox.Show(ex.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        }
                        conexion.Close();
                        ui_ListaProg();
                    }
                }
                else
                {
                    MessageBox.Show("Debe seleccionar un registro en estado \"PENDIENTE DE APROBACION\"", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
            }
            else
            {
                MessageBox.Show("No ha seleccionado registro para Aprobar", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void btnRechazar_Click(object sender, EventArgs e)
        {
            Int32 selectedCellCount = dgvdetalle.GetCellCount(DataGridViewElementStates.Selected);
            if (selectedCellCount > 0)
            {
                string idprog = dgvdetalle.Rows[dgvdetalle.SelectedCells[1].RowIndex].Cells[0].Value.ToString();
                string desprog = dgvdetalle.Rows[dgvdetalle.SelectedCells[1].RowIndex].Cells[1].Value.ToString();
                string fecini = dgvdetalle.Rows[dgvdetalle.SelectedCells[1].RowIndex].Cells[2].Value.ToString();
                string fecfin = dgvdetalle.Rows[dgvdetalle.SelectedCells[1].RowIndex].Cells[3].Value.ToString();
                string estado = dgvdetalle.Rows[dgvdetalle.SelectedCells[1].RowIndex].Cells[4].Value.ToString();
                string idestado = dgvdetalle.Rows[dgvdetalle.SelectedCells[1].RowIndex].Cells[10].Value.ToString();
                string to = dgvdetalle.Rows[dgvdetalle.SelectedCells[1].RowIndex].Cells[11].Value.ToString();

                if (idestado == "P")
                {
                    var confirmResult = MessageBox.Show("¿Esta seguro en \"RECHAZAR\" el registro seleccionado?", "Confirmación", MessageBoxButtons.YesNo);
                    if (confirmResult == DialogResult.Yes)
                    {
                        string sTextFromUser = PopupShow.GetUserInput("Deje su Comentario", "Mensaje de Cancelación");
                        if (!string.IsNullOrEmpty(sTextFromUser.Trim()))
                        {
                            string query = "update prog set idusr_chk = '" + variables.getValorUsr() + "', estado='R', ";
                            query += " fechachk=GETDATE() where idprog='" + idprog + "';";
                            query += "UPDATE progdet SET estado='R' WHERE idprog='" + idprog + "';";
                            try
                            {
                                if (variables.getValorTypeUsr() != "00")
                                {
                                    SendEnviarCorreo(variables.getValorUsr(), variables.getValorTypeUsr(), variables.getValorUsrMail(),
                                        "SISASIS - Solicitud de Aprobación de Programación", "<b>Programación:</b> " + desprog +
                                        "<br><b>Desde:</b> " + fecini + "<br><b>Hasta:</b> " + fecfin +
                                        "<br><b>Solicitud de Programación:</b> <span style='color:red'><b>RECHAZADA</b></span>" +
                                        "<br><b>Observación:</b> " + sTextFromUser.Trim(), variables.getValorUsrName(), to, true);
                                }

                                SqlConnection conexion = new SqlConnection();
                                conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
                                conexion.Open();
                                SqlCommand myCommand = new SqlCommand(query, conexion);
                                myCommand.ExecuteNonQuery();
                                myCommand.Dispose();
                            }
                            catch (SqlException ex)
                            {
                                MessageBox.Show(ex.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                            }
                            conexion.Close();
                            ui_ListaProg();
                        }
                        else
                        {
                            if (string.IsNullOrEmpty(sTextFromUser.Trim()))
                            {
                                MessageBox.Show("Operacion Cancelada..", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            }
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Debe seleccionar un registro en estado \"PENDIENTE DE APROBACION\"", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
            }
            else
            {
                MessageBox.Show("No ha seleccionado registro para Aprobar", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            GlobalVariables variables = new GlobalVariables();
            string idcia = variables.getValorCia();
            Int32 selectedCellCount = dgvdetalle.GetCellCount(DataGridViewElementStates.Selected);
            if (selectedCellCount > 0)
            {
                string idprog = dgvdetalle.Rows[dgvdetalle.SelectedCells[1].RowIndex].Cells[0].Value.ToString();
                string estado = dgvdetalle.Rows[dgvdetalle.SelectedCells[1].RowIndex].Cells[4].Value.ToString();
                string idestado = dgvdetalle.Rows[dgvdetalle.SelectedCells[1].RowIndex].Cells[10].Value.ToString();

                if (idestado == "V" || idestado == "R")
                {
                    var confirmResult = MessageBox.Show("¿Esta seguro en Eliminar del registro seleccionado?", "Confirmación", MessageBoxButtons.YesNo);
                    if (confirmResult == DialogResult.Yes)
                    {
                        string query = string.Empty;

                        SqlConnection conexion = new SqlConnection();
                        conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
                        conexion.Open();

                        query = "DELETE FROM progdet WHERE idprog = '" + idprog + "';";
                        query += "  DELETE FROM prog WHERE idprog = '" + idprog + "';";

                        try
                        {
                            SqlCommand myCommand = new SqlCommand(query, conexion);
                            myCommand.ExecuteNonQuery();
                            myCommand.Dispose();
                            MessageBox.Show("Registro Eliminado..", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            ui_ListaProg();
                        }
                        catch (SqlException ex)
                        {
                            MessageBox.Show(ex.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        }
                        conexion.Close();
                    }
                }
                else { MessageBox.Show("El registro esta en estado \"" + estado + "\". No se puede eliminar.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information); }
            }
            else
            {
                MessageBox.Show("No ha seleccionado registro a Eliminar", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void SendEnviarCorreo(string usuario, string tipoUsuario, string email, string asunto, string cuerpo,
            string nameusu, string to, bool evento)
        {
            List<UsrFile> listPara = new List<UsrFile>();

            if (to.Equals(nameusu) && tipoUsuario == "02")
            {
                listPara.Add(new UsrFile()
                {
                    desusr = nameusu,
                    email = email
                });
            }

            listPara.AddRange(GetEmailPara(usuario, tipoUsuario));

            //if (btnverificar && tipoUsuario == "02")
            //{
            //    if (listPara.Count == 0)
            //    {
            //        listPara.Add(new UsrFile()
            //        {
            //            desusr = nameusu,
            //            email = email
            //        });
            //    }
            //}

            if (listPara.Count == 0)
            {
                MessageBox.Show("El o los supervisor(es) no tiene un correo configurado.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }

            if (string.IsNullOrEmpty(email))
            {
                MessageBox.Show("No tienes un correo configurado.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }

            if (to.Equals(nameusu) && tipoUsuario == "02")
            {
                PopupShow.SendCorreoSolProg(asunto, cuerpo, email, listPara, nameusu, to);
            }
            else
            {
                if (evento) { PopupShow.SendCorreoSolProg(asunto, cuerpo, email, listPara, nameusu, to); }
                else { PopupShow.SendCorreoSolProg(asunto, cuerpo, email, listPara, nameusu, string.Empty); }
            }
        }

        private List<UsrFile> GetEmailPara(string usuario, string tipo)
        {
            List<UsrFile> listPara = new List<UsrFile>();
            string query = "select * from usrfile where idusr in (select idusr from cencosusr ";
            query += "where idcencos in (select idcencos from cencosusr where idusr = '"+ @usuario + "')) ";
            query += "AND typeusr = (CASE '" + @tipo + "' WHEN '03' THEN '02' WHEN '02' THEN '03' END) AND email IS NOT NULL";

            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();

            try
            {
                SqlCommand myCommand = new SqlCommand(query, conexion);
                SqlDataReader odr = myCommand.ExecuteReader();

                while (odr.Read())
                {
                    listPara.Add(new UsrFile()
                    {
                        desusr = odr.GetString(odr.GetOrdinal("desusr")),
                        email = odr.GetString(odr.GetOrdinal("email"))
                    });
                }

                odr.Close();
                myCommand.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            finally { conexion.Close(); }

            return listPara;
        }
        #endregion
    }
}