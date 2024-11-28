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

    public partial class ui_rep_asistencia_sunafil : Form
    {

        Funciones funciones = new Funciones();
        public ui_rep_asistencia_sunafil()
        {
            InitializeComponent();

            if (funciones.VersionAssembly()) Application.ExitThread();

            string query = " select a.idcia as clave, a.descia as descripcion from ciafile a (nolock) ";  //nuevo
            funciones.listaComboBox(query, cmbCia, "X");

            query = "SELECT idusr [clave],desusr [descripcion] FROM usrfile (NOLOCK) WHERE typeusr='05' AND desusr NOT LIKE '%COMEDOR%'";
            funciones.listaComboBox(query, cmbSedes, "X");
        }

        private void ui_Lista()
        {

            string sede = funciones.getValorComboBox(cmbSedes, 6);
            string fechaini = dtpFecini.Value.ToString("yyyy-MM-dd");
            string fechafin = dtpFecfin.Value.ToString("yyyy-MM-dd");
            string Cia = funciones.getValorComboBox(cmbCia, 2);

            //MessageBox.Show(sede);
            //MessageBox.Show(Cia);


            if (sede == "X   TO")
            {
                MessageBox.Show("Debe elegir una Sector(Fundo)", "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }

            if (Cia == "X")
            {
                MessageBox.Show("Debe elegir una Empresa", "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }

            //string query = "EXEC SP_REPORTE_SUNAFIL '" + fecha + "','" + sede + "'";
            

            //Mensaje de prueba para saber que filtro tienen
            //MessageBox.Show("La sede es : " + sede + ", la fecha inicio es : " + fechaini + ", la fecha fin es " + fechafin + " y empresa es: " + Cia);        
            
            
            
            string query = "EXECUTE SUNAFIL_PRUEBA '" + sede + "','" + fechaini + "','" + fechafin + "','" + Cia + "'";


            DataSet dssunafil = new DataSet();
            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();

            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = new SqlCommand(query, conexion);
            da.SelectCommand.CommandTimeout = 360;
            da.Fill(dssunafil, "dtsunafil2");

            ui_reporte ui_reporte = new ui_reporte();
            cr.crsunafilv2 cre = new cr.crsunafilv2();
            ui_reporte.asignaDataSet(cre, dssunafil);
            ui_reporte.Activate();
            ui_reporte.BringToFront();
            ui_reporte.ShowDialog();
            ui_reporte.Dispose();

        }

        private void btnGenerar_Click(object sender, EventArgs e)
        {
            btnGenerar.Enabled = false;
            loadingNext1.Visible = true;


            ui_Lista();

            loadingNext1.Visible = false;
            btnGenerar.Enabled = true;
        }

        private void cmbCia_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void cmbSedes_SelectedIndexChanged(object sender, EventArgs e)
        {
          
        }

        private void ui_rep_asistencia_sunafil_Load(object sender, EventArgs e)
        {

        }

        private void groupBox3_Enter(object sender, EventArgs e)
        {

        }
    }
}
