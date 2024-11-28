using System;
using System.Data;
using System.Windows.Forms;
using System.Configuration;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace CaniaBrava
{
    public partial class ui_solicapacitaciones : Form
    {
        GlobalVariables variables = new GlobalVariables();
        Funciones funciones = new Funciones();

        private ui_updsolicapacitaciones form = null;

        private ui_updsolicapacitaciones FormInstance
        {
            get
            {
                if (form == null)
                {
                    form = new ui_updsolicapacitaciones();
                    form.Disposed += new EventHandler(form_Disposed);
                }

                return form;
            }
        }

        void form_Disposed(object sender, EventArgs e) { form = null; }

        public ui_solicapacitaciones()
        {
            InitializeComponent();

            if (funciones.VersionAssembly()) Application.ExitThread();
        }

        private void ui_programacion_Load(object sender, EventArgs e)
        {
            btnNuevo.Visible = false;
            btnEliminar.Visible = false;
            btnAprobar.Visible = false;
            btnRechazar.Visible = false;
            btnVerificar.Visible = false;
            switch (variables.getValorNivelUsr())
            {
                case "1": btnNuevo.Visible = true; btnEliminar.Visible = true; btnVerificar.Visible = true; break;
                case "2":
                case "3":
                case "4": btnEditar.Text = "Visualizar"; btnAprobar.Visible = true; btnRechazar.Visible = true; break;
            }

            string query = "SELECT desmaesgen as descripcion FROM maesgen (NOLOCK) WHERE idmaesgen='183'";
            funciones.listaComboBoxUnCampo(query, cmbSituacion, "X");
            cmbSituacion.Text = "X";
            ui_ListaProg();
        }

        private void ui_ListaProg()
        {
            string buscar = txtBuscar.Text.Trim();
            bool area = rdarea.Checked;
            bool posi = rdposicion.Checked;
            string situacion = funciones.getValorComboBox(cmbSituacion, 1);
            string cadenasituacion = string.Empty;

            if (situacion != "X") cadenasituacion = " and estado = '" + @situacion + "' ";

            string query = "";
            query = " SELECT a.idcapacita,a.descapacita [Tema],b.desmaesgen [Posicion],c.desmaesgen [Area],d.desmaesgen [Gerencia],e.descia [Empresa],";
            query += "a.desproblema [Problematica],a.desobjetivo [Objetivo],";
            query += "l.desmaesgen+' por '+o.desusr [AP1 - Gerencia],";
            query += "m.desmaesgen+' por '+p.desusr [AP2 - G.Talento],";
            query += "n.desmaesgen+' por '+q.desusr [AP3 - RRHH],";
            query += "h.desmaesgen [Tip.Capacitacion],g.desmaesgen [Indicador],f.desmaesgen [Necesidad],i.desusr [Creado por],j.desusr [Modificado por], ";
            query += "k.desmaesgen AS Estado FROM capacita a (NOLOCK) ";
            query += "INNER JOIN maesgen b (NOLOCK) ON b.idmaesgen='050' AND b.clavemaesgen=a.posic AND b.parm1maesgen=a.secci AND b.parm2maesgen=a.geren AND b.parm3maesgen=a.idcia ";
            query += "INNER JOIN maesgen c (NOLOCK) ON c.idmaesgen='008' AND c.clavemaesgen=a.secci AND c.parm1maesgen=a.geren AND c.parm2maesgen=a.idcia ";
            query += "INNER JOIN maesgen d (NOLOCK) ON d.idmaesgen='040' AND d.clavemaesgen=a.geren AND d.parm1maesgen=a.idcia ";
            query += "INNER JOIN ciafile e (NOLOCK) ON e.idcia=a.idcia ";
            query += "INNER JOIN maesgen f (NOLOCK) ON f.idmaesgen='180' AND f.clavemaesgen=a.necesidad ";
            query += "INNER JOIN maesgen g (NOLOCK) ON g.idmaesgen='181' AND g.clavemaesgen=a.indicador ";
            query += "INNER JOIN maesgen h (NOLOCK) ON h.idmaesgen='182' AND h.clavemaesgen=a.tipcapacita ";
            query += "INNER JOIN usrfile i (NOLOCK) ON i.idusr=a.userregistro ";
            query += "INNER JOIN usrfile j (NOLOCK) ON j.idusr=a.userupdate ";
            query += "INNER JOIN maesgen k (NOLOCK) ON k.idmaesgen='183' AND k.clavemaesgen=a.estado ";
            query += "LEFT JOIN maesgen l (NOLOCK) ON l.idmaesgen='183' AND l.clavemaesgen=a.estaprobador1 ";
            query += "LEFT JOIN maesgen m (NOLOCK) ON m.idmaesgen='183' AND m.clavemaesgen=a.estaprobador2 ";
            query += "LEFT JOIN maesgen n (NOLOCK) ON n.idmaesgen='183' AND n.clavemaesgen=a.estaprobador3 ";
            query += "LEFT JOIN usrfile o (NOLOCK) ON o.idusr = a.aprobador1 ";
            query += "LEFT JOIN usrfile p (NOLOCK) ON p.idusr = a.aprobador2 ";
            query += "LEFT JOIN usrfile q (NOLOCK) ON q.idusr = a.aprobador3 ";
            query += "WHERE a.estado='3' ";

            if (buscar != string.Empty)
            {
                if (area) { query += "WHERE c.desmaesgen LIKE '%" + buscar + "%'"; }
                if (posi) { query += "WHERE b.desmaesgen LIKE '%" + buscar + "%'"; }
            }

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
                    DataSet myDataSet = new DataSet();
                    myDataAdapter.Fill(myDataSet);
                    funciones.formatearDataGridViewWhite(dgvdetalle);

                    dgvdetalle.DataSource = myDataSet.Tables[0];

                    dgvdetalle.Columns[1].Width = 150;
                    dgvdetalle.Columns[2].Width = 220;
                    dgvdetalle.Columns[3].Width = 200;
                    dgvdetalle.Columns[4].Width = 200;
                    dgvdetalle.Columns[5].Width = 220;
                    dgvdetalle.Columns[6].Width = 220;
                    dgvdetalle.Columns[7].Width = 220;
                    dgvdetalle.Columns[8].Width = 220;
                    dgvdetalle.Columns[9].Width = 220;
                    dgvdetalle.Columns[10].Width = 220;
                    dgvdetalle.Columns[11].Width = 200;
                    dgvdetalle.Columns[12].Width = 200;
                    dgvdetalle.Columns[13].Width = 200;
                    dgvdetalle.Columns[14].Width = 200;
                    dgvdetalle.Columns[15].Width = 200;

                    dgvdetalle.Columns[0].Visible = false;
                    dgvdetalle.Columns[0].Frozen = true;
                    dgvdetalle.Columns[1].Frozen = true;
                    dgvdetalle.Columns[2].Frozen = true;
                    dgvdetalle.Columns[3].Frozen = true;

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
            ui_updsolicapacitaciones ui_detalle = this.FormInstance;
            ui_detalle.Load_Inicial();
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
                string id = dgvdetalle.Rows[dgvdetalle.SelectedCells[1].RowIndex].Cells[0].Value.ToString();
                string estado = dgvdetalle.Rows[dgvdetalle.SelectedCells[1].RowIndex].Cells[16].Value.ToString();

                ui_updsolicapacitaciones ui_detalle = this.FormInstance;
                ui_detalle.Activate();
                ui_detalle.Load_Inicial();
                ui_detalle.Editar(id, estado);
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
                var datos = ((DataTable)dgvdetalle.DataSource).Copy();
                datos.Columns.Remove("idcapacita");
                dgvdetalleExcel.DataSource = datos;

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
                string id = dgvdetalle.Rows[dgvdetalle.SelectedCells[1].RowIndex].Cells[0].Value.ToString();
                string desprog = dgvdetalle.Rows[dgvdetalle.SelectedCells[1].RowIndex].Cells[1].Value.ToString();
                string fecini = dgvdetalle.Rows[dgvdetalle.SelectedCells[1].RowIndex].Cells[2].Value.ToString();
                string fecfin = dgvdetalle.Rows[dgvdetalle.SelectedCells[1].RowIndex].Cells[3].Value.ToString();
                string estado = dgvdetalle.Rows[dgvdetalle.SelectedCells[1].RowIndex].Cells[16].Value.ToString();

                if (estado == "ACTIVO" || estado == "RECHAZADO")
                {
                    var confirmResult = MessageBox.Show("¿Esta seguro en \"Solicitar Aprobación\" del registro seleccionado?", "Confirmación", MessageBoxButtons.YesNo);
                    if (confirmResult == DialogResult.Yes)
                    {
                        string query = "UPDATE capacita SET estado='1' WHERE idcapacita='" + id + "';";
                        SqlConnection conexion = new SqlConnection();

                        try
                        {
                            //if (variables.getValorTypeUsr() != "00")
                            //{
                            //    SendEnviarCorreo(variables.getValorUsr(), variables.getValorTypeUsr(), variables.getValorUsrMail(),
                            //        "SISASIS - Solicitud de Aprobación de Programación", "Se solicita aprobación de la siguiente Programación." +
                            //        "<br><br><b>Programación:</b> " + desprog
                            //        + "<br><b>Desde:</b> " + fecini + "<br><b>Hasta:</b> " + fecfin +
                            //        "<br><b>Comentario:</b> Ingresar a SISASIS-Desktop, para realizar su \"Aprobación o Rechazo\"",
                            //        variables.getValorUsrName(), to, false);
                            //}

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
                    MessageBox.Show("El registro seleccionado está en estado \"" + estado + "\"", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
            }
            else
            {
                MessageBox.Show("No ha seleccionado ningun registro", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void btnAprobar_Click(object sender, EventArgs e)
        {
            Int32 selectedCellCount = dgvdetalle.GetCellCount(DataGridViewElementStates.Selected);
            if (selectedCellCount > 0)
            {
                string id = dgvdetalle.Rows[dgvdetalle.SelectedCells[1].RowIndex].Cells[0].Value.ToString();
                string desprog = dgvdetalle.Rows[dgvdetalle.SelectedCells[1].RowIndex].Cells[1].Value.ToString();
                string fecini = dgvdetalle.Rows[dgvdetalle.SelectedCells[1].RowIndex].Cells[2].Value.ToString();
                string fecfin = dgvdetalle.Rows[dgvdetalle.SelectedCells[1].RowIndex].Cells[3].Value.ToString();
                string estado = dgvdetalle.Rows[dgvdetalle.SelectedCells[1].RowIndex].Cells[16].Value.ToString();
                string to = dgvdetalle.Rows[dgvdetalle.SelectedCells[1].RowIndex].Cells[11].Value.ToString();

                if (estado == "PENDIENTE")
                {
                    var confirmResult = MessageBox.Show("¿Esta seguro en \"APROBAR\" el registro seleccionado?", "Confirmación", MessageBoxButtons.YesNo);
                    if (confirmResult == DialogResult.Yes)
                    {
                        string query = "UPDATE capacita SET ";
                        switch (variables.getValorNivelUsr())
                        {
                            case "2":
                                query += "aprobador1='" + variables.getValorUsr() + "', estadoaprobador1='3',fechaaprobador1=GETDATE() ";
                                break;
                            case "3":
                                query += "aprobador2='" + variables.getValorUsr() + "', estadoaprobador2='3',fechaaprobador2=GETDATE() ";
                                break;
                            case "4":
                                query += "aprobador3='" + variables.getValorUsr() + "', estadoaprobador3='3',estado='3',fechaaprobador3=GETDATE() ";
                                break;
                        }
                        query += "WHERE idcapacita='" + id + "';";
                        SqlConnection conexion = new SqlConnection();
                        try
                        {
                            if (variables.getValorTypeUsr() != "00")
                            {
                                //    SendEnviarCorreo(variables.getValorUsr(), variables.getValorTypeUsr(), variables.getValorUsrMail(),
                                //        "SISASIS - Solicitud de Aprobación Programación", "<b>Programación:</b> " + desprog +
                                //        "<br><b>Desde:</b> " + fecini + "<br><b>Hasta:</b> " + fecfin +
                                //        "<br><b>Solicitud de Programación:</b> <span style='color:#009E41'><b>APROBADA</b></span>",
                                //        variables.getValorUsrName(), to, true);
                            }

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
                    MessageBox.Show("El registro seleccionado esta en estado \"" + estado + "\"", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
            }
            else
            {
                MessageBox.Show("No ha seleccionado registro", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void btnRechazar_Click(object sender, EventArgs e)
        {
            Int32 selectedCellCount = dgvdetalle.GetCellCount(DataGridViewElementStates.Selected);
            if (selectedCellCount > 0)
            {
                string id = dgvdetalle.Rows[dgvdetalle.SelectedCells[1].RowIndex].Cells[0].Value.ToString();
                string desprog = dgvdetalle.Rows[dgvdetalle.SelectedCells[1].RowIndex].Cells[1].Value.ToString();
                string fecini = dgvdetalle.Rows[dgvdetalle.SelectedCells[1].RowIndex].Cells[2].Value.ToString();
                string fecfin = dgvdetalle.Rows[dgvdetalle.SelectedCells[1].RowIndex].Cells[3].Value.ToString();
                string estado = dgvdetalle.Rows[dgvdetalle.SelectedCells[1].RowIndex].Cells[16].Value.ToString();
                string to = dgvdetalle.Rows[dgvdetalle.SelectedCells[1].RowIndex].Cells[11].Value.ToString();

                if (estado == "PENDIENTE")
                {
                    var confirmResult = MessageBox.Show("¿Esta seguro en \"RECHAZAR\" el registro seleccionado?", "Confirmación", MessageBoxButtons.YesNo);
                    if (confirmResult == DialogResult.Yes)
                    {
                        string sTextFromUser = PopupShow.GetUserInput("Deje su Comentario", "Mensaje de Cancelación");
                        if (!string.IsNullOrEmpty(sTextFromUser.Trim()))
                        {
                            string query = "UPDATE capacita SET ";
                            switch (variables.getValorNivelUsr())
                            {
                                case "2":
                                    query += "aprobador1='" + variables.getValorUsr() + "', estadoaprobador1='2',fechaaprobador1=GETDATE() ";
                                    break;
                                case "3":
                                    query += "aprobador2='" + variables.getValorUsr() + "', estadoaprobador2='2',fechaaprobador2=GETDATE() ";
                                    break;
                                case "4":
                                    query += "aprobador3='" + variables.getValorUsr() + "', estadoaprobador3='2',estado='2',fechaaprobador3=GETDATE() ";
                                    break;
                            }
                            query += "WHERE idcapacita='" + id + "';";
                            SqlConnection conexion = new SqlConnection();
                            try
                            {
                                if (variables.getValorTypeUsr() != "00")
                                {
                                    //SendEnviarCorreo(variables.getValorUsr(), variables.getValorTypeUsr(), variables.getValorUsrMail(),
                                    //    "SISASIS - Solicitud de Aprobación de Programación", "<b>Programación:</b> " + desprog +
                                    //    "<br><b>Desde:</b> " + fecini + "<br><b>Hasta:</b> " + fecfin +
                                    //    "<br><b>Solicitud de Programación:</b> <span style='color:red'><b>RECHAZADA</b></span>" +
                                    //    "<br><b>Observación:</b> " + sTextFromUser.Trim(), variables.getValorUsrName(), to, true);
                                }

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
                    MessageBox.Show("El registro seleccionado esta en estado \"" + estado + "\"", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
            }
            else
            {
                MessageBox.Show("No ha seleccionado registro", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            GlobalVariables variables = new GlobalVariables();
            string idcia = variables.getValorCia();
            Int32 selectedCellCount = dgvdetalle.GetCellCount(DataGridViewElementStates.Selected);
            if (selectedCellCount > 0)
            {
                string id = dgvdetalle.Rows[dgvdetalle.SelectedCells[1].RowIndex].Cells[0].Value.ToString();
                string estado = dgvdetalle.Rows[dgvdetalle.SelectedCells[1].RowIndex].Cells[4].Value.ToString();

                if (estado == "ACTIVO")
                {
                    var confirmResult = MessageBox.Show("¿Esta seguro en Eliminar del registro seleccionado?", "Confirmación", MessageBoxButtons.YesNo);
                    if (confirmResult == DialogResult.Yes)
                    {
                        string query = string.Empty;

                        SqlConnection conexion = new SqlConnection();
                        conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
                        conexion.Open();

                        query = "DELETE FROM capacita WHERE idcapacita = '" + id + "';";

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
            query += "where idcencos in (select idcencos from cencosusr where idusr = '" + @usuario + "')) ";
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

        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            ui_ListaProg();
        }

        private void btnGeneSolicitud_Click(object sender, EventArgs e)
        {

        }
    }
}