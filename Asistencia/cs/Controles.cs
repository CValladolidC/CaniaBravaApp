using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CaniaBrava
{
    class Controles
    {
        public void CleanerControlSimple(Form formulario)
        {
            foreach (Control c in formulario.Controls)
            {
                if (c is TextBox)
                {
                    c.Text = "";
                }

                if (c is ComboBox)
                {
                    c.Text = string.Empty;
                }
                
                if (c is MaskedTextBox)
                {
                    c.Text = "";
                }
            }
        }

        public void CleanerControlParent(Control parent)
        {
            foreach (Control c in parent.Controls)
            {
                if (c is TextBox)
                {
                    c.Text = "";
                }

                if (c is ComboBox)
                {
                    c.Text = string.Empty;
                }

                if (c is MaskedTextBox)
                {
                    c.Text = "";
                }


                if (c.Controls.Count > 0)
                {

                    CleanerControlParent(c);

                }

                
                
            }
        }

        public void EnabledControlParent(Control parent)
        {
            foreach (Control c in parent.Controls)
            {
                if (c is TextBox)
                {
                    c.Enabled = true;
                }

                if (c is ComboBox)
                {
                    c.Enabled = true;
                }

                if (c is MaskedTextBox)
                {
                    c.Enabled = true;
                }

                if (c.Controls.Count > 0)
                {

                    EnabledControlParent(c);

                }
            }
        }

        public void DisabledControlParent(Control parent)
        {
            foreach (Control c in parent.Controls)
            {
                if (c is TextBox)
                {
                    c.Enabled = false;
                }

                if (c is ComboBox)
                {
                    c.Enabled = false;
                }

                if (c is MaskedTextBox)
                {
                    c.Enabled = false;
                }

                if (c.Controls.Count > 0)
                {

                    DisabledControlParent(c);

                }
            }
        }
    }
}