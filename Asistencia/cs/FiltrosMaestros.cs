using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CaniaBrava
{
    class FiltrosMaestros
    {
        public void filtrarPerPlan(string clasePadre, Form ui_FormPadre, TextBox txtText, string idcia, string idtipoper, string estadoTrabajador, string cadenaBusqueda, string condicionAdicional)
        {
            ui_buscarperplan ui_buscarperplan = new ui_buscarperplan();
            ui_buscarperplan._FormPadre = ui_FormPadre;
            ui_buscarperplan.setValoresBuscarPerPlan(idtipoper, idcia, clasePadre, estadoTrabajador, cadenaBusqueda, condicionAdicional);
            ui_buscarperplan.ui_LoadPerPlan(false);
            if (ui_buscarperplan.ShowDialog(ui_FormPadre) == DialogResult.OK)
            {
                txtText.Focus();
            }
            else
            {
                txtText.Clear();
                txtText.Focus();
            }
            ui_buscarperplan.Dispose();
        }

        public void filtrarTgRpts(string clasePadre, Form ui_FormPadre, TextBox txtText, string codigotg, string cadenaBusqueda)
        {

            ui_buscartgrpts ui_buscartgrpts = new ui_buscartgrpts();
            ui_buscartgrpts._FormPadre = ui_FormPadre;
            ui_buscartgrpts.setValores(codigotg, clasePadre, cadenaBusqueda);
            ui_buscartgrpts.ui_LoadTgRpts();
            if (ui_buscartgrpts.ShowDialog(ui_FormPadre) == DialogResult.OK)
            {
                txtText.Focus();

            }
            else
            {
                txtText.Clear();
                txtText.Focus();
            }
            ui_buscartgrpts.Dispose();
        }

        public void filtrarPresPer(string clasePadre, Form ui_FormPadre, TextBox txtText, string idcia, string idperplan)
        {
            ui_buscarpresper ui_buscarpresper = new ui_buscarpresper();
            ui_buscarpresper._FormPadre = ui_FormPadre;
            ui_buscarpresper.ui_setValores(idcia, idperplan, clasePadre);
            ui_buscarpresper.ui_LoadPresPer();
            if (ui_buscarpresper.ShowDialog(ui_FormPadre) == DialogResult.OK)
            {
                txtText.Focus();

            }
            else
            {
                txtText.Clear();
                txtText.Focus();
            }
            ui_buscarpresper.Dispose();
        }

        public void filtrarPerPlan2(string clasePadre, Form ui_FormPadre, TextBox txtText, string idcia, string cadenaBusqueda, string condicionAdicional)
        {
            ui_buscarperplan ui_buscarperplan = new ui_buscarperplan();
            ui_buscarperplan._FormPadre = ui_FormPadre;
            ui_buscarperplan.setValoresBuscarPerPlan2(idcia, clasePadre, cadenaBusqueda, condicionAdicional);
            ui_buscarperplan.ui_LoadPerPlan(true);
            if (ui_buscarperplan.ShowDialog(ui_FormPadre) == DialogResult.OK)
            {
                txtText.Focus();
            }
            else
            {
                txtText.Clear();
                txtText.Focus();
            }
            ui_buscarperplan.Dispose();
        }

        public void filtrarPerPlan(string clasePadre, Form ui_FormPadre, TextBox txtText, string cadenaBusqueda)
        {
            ui_buscarperplan ui_buscarperplan = new ui_buscarperplan();
            ui_buscarperplan._FormPadre = ui_FormPadre;
            ui_buscarperplan.setValoresBuscarPerPlan(clasePadre, cadenaBusqueda);
            ui_buscarperplan.ui_LoadPerPlan(true);
            if (ui_buscarperplan.ShowDialog(ui_FormPadre) == DialogResult.OK)
            {
                txtText.Focus();
            }
            else
            {
                txtText.Clear();
                txtText.Focus();
            }
            ui_buscarperplan.Dispose();
        }

        public void filtrarCentroSalud(string clasePadre, Form ui_FormPadre, TextBox txtText, string cadenaBusqueda)
        {
            ui_buscarcentsalud ui_buscarperplan = new ui_buscarcentsalud();
            ui_buscarperplan._FormPadre = ui_FormPadre;
            ui_buscarperplan.setValoresBuscarPerPlan(clasePadre, cadenaBusqueda);
            ui_buscarperplan.ui_LoadCentroSalud();
            if (ui_buscarperplan.ShowDialog(ui_FormPadre) == DialogResult.OK)
            {
                txtText.Focus();
            }
            else
            {
                txtText.Clear();
                txtText.Focus();
            }
            ui_buscarperplan.Dispose();
        }

        public void filtrarMedico(string clasePadre, Form ui_FormPadre, TextBox txtText, string cadenaBusqueda)
        {
            ui_buscarmedicos ui_buscarperplan = new ui_buscarmedicos();
            ui_buscarperplan._FormPadre = ui_FormPadre;
            ui_buscarperplan.setValoresBuscarPerPlan(clasePadre, cadenaBusqueda);
            ui_buscarperplan.ui_Load();
            if (ui_buscarperplan.ShowDialog(ui_FormPadre) == DialogResult.OK)
            {
                txtText.Focus();
            }
            else
            {
                txtText.Clear();
                txtText.Focus();
            }
            ui_buscarperplan.Dispose();
        }
    }
}