using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CaniaBrava
{
    public partial class ui_manteTablas : Form
    {
        public ui_manteTablas()
        {
            InitializeComponent();
        }

        private void ui_manteTablas_Load(object sender, EventArgs e)
        {

        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            string mensaje = string.Empty;
            ManteTablas mantetablas = new ManteTablas();
            ProcPlan procplan = new ProcPlan();
            Exporta exporta = new Exporta();

            procplan.eliminarProcPlanGeneral();
            mensaje = mensaje + mantetablas.mantenimientoTabla("conbol", "Conceptos de Boleta") + "\n"; ;
            txtMensaje.Text = mensaje;
            mensaje = mensaje + mantetablas.mantenimientoTabla("condataplan", "Conceptos de Parte Diario") + "\n"; ;
            txtMensaje.Text = mensaje;
            mensaje = mensaje + mantetablas.mantenimientoTabla("conplan", "Conceptos de Planilla") + "\n"; ;
            txtMensaje.Text = mensaje;
            mensaje = mensaje + mantetablas.mantenimientoTabla("dataplan", "Parte Diario") + "\n"; ;
            txtMensaje.Text = mensaje;
            mensaje = mensaje + mantetablas.mantenimientoTabla("desplan", "Parte Destajo Planilla") + "\n"; ;
            txtMensaje.Text = mensaje;
            mensaje = mensaje + mantetablas.mantenimientoTabla("desret", "Parte Destajo Retenciones") + "\n"; ;
            txtMensaje.Text = mensaje;
            mensaje = mensaje + mantetablas.mantenimientoTabla("detconplan", "Detalle Conceptos Planilla") + "\n"; ;
            txtMensaje.Text = mensaje;
            mensaje = mensaje + mantetablas.mantenimientoTabla("diassubsi", "Dias Subsidiados y No Subsidiados") + "\n"; ;
            txtMensaje.Text = mensaje;
            mensaje = mensaje + mantetablas.mantenimientoTabla("maesgen", "Tablas Generales") + "\n"; ;
            txtMensaje.Text = mensaje;
            mensaje = mensaje + mantetablas.mantenimientoTabla("perlab", "Periodos Laborales") + "\n"; ;
            txtMensaje.Text = mensaje;
            mensaje = mensaje + mantetablas.mantenimientoTabla("perplan", "Maestro de Personal") + "\n"; ;
            txtMensaje.Text = mensaje;
            mensaje = mensaje + mantetablas.mantenimientoTabla("perret", "Personal de Retenciones") + "\n"; ;
            txtMensaje.Text = mensaje;
            mensaje = mensaje + mantetablas.mantenimientoTabla("plan", "Planilla") + "\n"; ;
            txtMensaje.Text = mensaje;
            mensaje = mensaje + mantetablas.mantenimientoTabla("predesplan", "Pre-planilla Destajo") + "\n"; ;
            txtMensaje.Text = mensaje;
            mensaje = mensaje + mantetablas.mantenimientoTabla("presper", "Prestamos al Personal") + "\n"; ;
            txtMensaje.Text = mensaje;
            mensaje = mensaje + mantetablas.mantenimientoTabla("procplan", "Proceso de Planilla") + "\n"; ;
            txtMensaje.Text = mensaje;
            mensaje = mensaje + mantetablas.mantenimientoTabla("quicat", "Acum. Quinta Categoria") + "\n"; ;
            txtMensaje.Text = mensaje;
            mensaje = mensaje + mantetablas.mantenimientoTabla("remu", "Remuneraciones") + "\n"; ;
            txtMensaje.Text = mensaje;
            mensaje = mensaje + mantetablas.mantenimientoTabla("tareo", "Tareo o Destajo") + "\n"; ;
            txtMensaje.Text = mensaje;
            mensaje = mensaje + mantetablas.mantenimientoTabla("tgrpts", "Tablas Generales PDT") + "\n"; ;

            txtMensaje.Text = exporta.Ascii_FromCadena(mensaje);

            MessageBox.Show("Operación completada", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}