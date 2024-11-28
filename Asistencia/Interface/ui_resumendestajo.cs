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
    public partial class ui_resumendestajo : Form
    {
        string idcia;
        string messem;
        string anio;
        string fechaini;
        string fechafin;
        string idproddes;
        string idtipoper;
        string idtipocal;
        string tiporegistro;
        string idzontra;
        string emplea;
        string estane;

        public ui_resumendestajo()
        {
            InitializeComponent();
        }

        internal void setValores(string idcia, string idproddes, string messem, string anio, string idtipoper, string idtipocal, string idzontra, string tiporegistro, string fechaini, string fechafin, string emplea, string estane)
        {
            this.idcia = idcia;
            this.idproddes = idproddes;
            this.messem = messem;
            this.anio = anio;
            this.idtipoper = idtipoper;
            this.idtipocal = idtipocal;
            this.tiporegistro = tiporegistro;
            this.idzontra = idzontra;
            this.fechaini = fechaini;
            this.fechafin = fechafin;
            this.emplea = emplea;
            this.estane = estane;
        }

        private void ui_generaCuadro(string idcia, string idproddes, string messem, string anio, string idtipoper, string idtipocal, string tiporegistro, string fechaini, string tiporesumen, string emplea, string estane)
        {
            string query;
            DataTable zontra = new DataTable();
            DataTable dt = new DataTable();
            UtileriasFechas uf = new UtileriasFechas();
            Destajo destajo = new Destajo();
            string fecha1, fecha2, fecha3, fecha4, fecha5, fecha6, fecha7;
            dt.Columns.Add("idzontra", typeof(string));
            dt.Columns.Add("deszontra", typeof(string));
            dt.Columns.Add("fecha1", typeof(float));
            dt.Columns.Add("fecha2", typeof(float));
            dt.Columns.Add("fecha3", typeof(float));
            dt.Columns.Add("fecha4", typeof(float));
            dt.Columns.Add("fecha5", typeof(float));
            dt.Columns.Add("fecha6", typeof(float));
            dt.Columns.Add("fecha7", typeof(float));

            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();
            SqlDataAdapter da = new SqlDataAdapter();

            query = "select idzontra,deszontra from zontra where statezontra='V' and idcia='" + @idcia + "';";
            da.SelectCommand = new SqlCommand(query, conexion);
            da.Fill(zontra);
            foreach (DataRow row_zontra in zontra.Rows)
            {
                DataRow dr;
                dr = dt.NewRow();
                dr[0] = row_zontra["idzontra"].ToString();
                dr[1] = row_zontra["deszontra"].ToString();
                fecha1 = uf.incrementarFecha(fechaini, 0);
                if (tiporesumen.Equals("C"))
                {
                    dr[2] = destajo.ui_getProduccionDestajo(idcia, idproddes, row_zontra["idzontra"].ToString(), messem, anio, fecha1, idtipocal, idtipoper, tiporegistro, "0", emplea, estane);

                }
                else
                {
                    dr[2] = destajo.ui_getProduccionDestajo(idcia, idproddes, row_zontra["idzontra"].ToString(), messem, anio, fecha1, idtipocal, idtipoper, tiporegistro, "1", emplea, estane);
                }

                fecha2 = uf.incrementarFecha(fechaini, 1);
                if (tiporesumen.Equals("C"))
                {
                    dr[3] = destajo.ui_getProduccionDestajo(idcia, idproddes, row_zontra["idzontra"].ToString(), messem, anio, fecha2, idtipocal, idtipoper, tiporegistro, "0", emplea, estane);

                }
                else
                {
                    dr[3] = destajo.ui_getProduccionDestajo(idcia, idproddes, row_zontra["idzontra"].ToString(), messem, anio, fecha2, idtipocal, idtipoper, tiporegistro, "1", emplea, estane);
                }

                fecha3 = uf.incrementarFecha(fechaini, 2);
                if (tiporesumen.Equals("C"))
                {
                    dr[4] = destajo.ui_getProduccionDestajo(idcia, idproddes, row_zontra["idzontra"].ToString(), messem, anio, fecha3, idtipocal, idtipoper, tiporegistro, "0", emplea, estane);

                }
                else
                {
                    dr[4] = destajo.ui_getProduccionDestajo(idcia, idproddes, row_zontra["idzontra"].ToString(), messem, anio, fecha3, idtipocal, idtipoper, tiporegistro, "1", emplea, estane);
                }

                fecha4 = uf.incrementarFecha(fechaini, 3);
                if (tiporesumen.Equals("C"))
                {
                    dr[5] = destajo.ui_getProduccionDestajo(idcia, idproddes, row_zontra["idzontra"].ToString(), messem, anio, fecha4, idtipocal, idtipoper, tiporegistro, "0", emplea, estane);

                }
                else
                {
                    dr[5] = destajo.ui_getProduccionDestajo(idcia, idproddes, row_zontra["idzontra"].ToString(), messem, anio, fecha4, idtipocal, idtipoper, tiporegistro, "1", emplea, estane);
                }


                fecha5 = uf.incrementarFecha(fechaini, 4);
                if (tiporesumen.Equals("C"))
                {
                    dr[6] = destajo.ui_getProduccionDestajo(idcia, idproddes, row_zontra["idzontra"].ToString(), messem, anio, fecha5, idtipocal, idtipoper, tiporegistro, "0", emplea, estane);

                }
                else
                {
                    dr[6] = destajo.ui_getProduccionDestajo(idcia, idproddes, row_zontra["idzontra"].ToString(), messem, anio, fecha5, idtipocal, idtipoper, tiporegistro, "1", emplea, estane);
                }


                fecha6 = uf.incrementarFecha(fechaini, 5);
                if (tiporesumen.Equals("C"))
                {
                    dr[7] = destajo.ui_getProduccionDestajo(idcia, idproddes, row_zontra["idzontra"].ToString(), messem, anio, fecha6, idtipocal, idtipoper, tiporegistro, "0", emplea, estane);

                }
                else
                {
                    dr[7] = destajo.ui_getProduccionDestajo(idcia, idproddes, row_zontra["idzontra"].ToString(), messem, anio, fecha6, idtipocal, idtipoper, tiporegistro, "1", emplea, estane);
                }

                fecha7 = uf.incrementarFecha(fechaini, 6);
                if (tiporesumen.Equals("C"))
                {
                    dr[8] = destajo.ui_getProduccionDestajo(idcia, idproddes, row_zontra["idzontra"].ToString(), messem, anio, fecha7, idtipocal, idtipoper, tiporegistro, "0", emplea, estane);

                }
                else
                {
                    dr[8] = destajo.ui_getProduccionDestajo(idcia, idproddes, row_zontra["idzontra"].ToString(), messem, anio, fecha7, idtipocal, idtipoper, tiporegistro, "1", emplea, estane);
                }


                dt.Rows.Add(dr);

            }
            fecha1 = uf.incrementarFecha(fechaini, 0);
            fecha2 = uf.incrementarFecha(fechaini, 1);
            fecha3 = uf.incrementarFecha(fechaini, 2);
            fecha4 = uf.incrementarFecha(fechaini, 3);
            fecha5 = uf.incrementarFecha(fechaini, 4);
            fecha6 = uf.incrementarFecha(fechaini, 5);
            fecha7 = uf.incrementarFecha(fechaini, 6);

            ui_ListaResumen(dt, fecha1, fecha2, fecha3, fecha4, fecha5, fecha6, fecha7);

        }

        private void ui_ListaResumen(DataTable dt, string fecha1, string fecha2, string fecha3, string fecha4,
        string fecha5, string fecha6, string fecha7)
        {

            Funciones funciones = new Funciones();
            funciones.formatearDataGridView(dgvdetalle);
            dgvdetalle.DataSource = dt;
            dgvdetalle.Columns[0].HeaderText = "Código";
            dgvdetalle.Columns[1].HeaderText = "Zona de Trabajo";
            dgvdetalle.Columns[2].HeaderText = fecha1;
            dgvdetalle.Columns[3].HeaderText = fecha2;
            dgvdetalle.Columns[4].HeaderText = fecha3;
            dgvdetalle.Columns[5].HeaderText = fecha4;
            dgvdetalle.Columns[6].HeaderText = fecha5;
            dgvdetalle.Columns[7].HeaderText = fecha6;
            dgvdetalle.Columns[8].HeaderText = fecha7;


            dgvdetalle.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvdetalle.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvdetalle.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvdetalle.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvdetalle.Columns[6].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvdetalle.Columns[7].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvdetalle.Columns[8].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            dgvdetalle.Columns[2].DefaultCellStyle.Format = "###,###.##";
            dgvdetalle.Columns[3].DefaultCellStyle.Format = "###,###.##";
            dgvdetalle.Columns[4].DefaultCellStyle.Format = "###,###.##";
            dgvdetalle.Columns[5].DefaultCellStyle.Format = "###,###.##";
            dgvdetalle.Columns[6].DefaultCellStyle.Format = "###,###.##";
            dgvdetalle.Columns[7].DefaultCellStyle.Format = "###,###.##";
            dgvdetalle.Columns[8].DefaultCellStyle.Format = "###,###.##";

            dgvdetalle.Columns[0].Width = 50;
            dgvdetalle.Columns[1].Width = 200;
            dgvdetalle.Columns[2].Width = 75;
            dgvdetalle.Columns[3].Width = 75;
            dgvdetalle.Columns[4].Width = 75;
            dgvdetalle.Columns[5].Width = 75;
            dgvdetalle.Columns[6].Width = 75;
            dgvdetalle.Columns[7].Width = 75;
            dgvdetalle.Columns[8].Width = 75;
        }

        private void ui_resumendestajo_Load(object sender, EventArgs e)
        {
            Funciones fn = new Funciones();
            txtMesSem.Text = this.messem + this.anio;
            txtFechaIni.Text = this.fechaini;
            txtFechaFin.Text = this.fechafin;
            cmbTipoRegistro.Text = "I    IMPORTE TOTAL";
            string tiporesumen = fn.getValorComboBox(cmbTipoRegistro, 1);
            ui_generaCuadro(this.idcia, this.idproddes, this.messem, this.anio, this.idtipoper, this.idtipocal, this.tiporegistro, this.fechaini, tiporesumen, this.emplea, this.estane);


        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cmbTipoRegistro_SelectedIndexChanged(object sender, EventArgs e)
        {
            Funciones fn = new Funciones();
            string tiporesumen = fn.getValorComboBox(cmbTipoRegistro, 1);
            ui_generaCuadro(this.idcia, this.idproddes, this.messem, this.anio, this.idtipoper, this.idtipocal, this.tiporegistro, this.fechaini, tiporesumen, this.emplea, this.estane);
        }
    }
}