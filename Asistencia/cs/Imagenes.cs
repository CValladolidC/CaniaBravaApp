using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.IO;
using System.Drawing;
using System.Windows.Forms;

namespace CaniaBrava
{
    class Imagenes
    {
        public Image GetImage(string fileName)
        {
            Assembly asm = Assembly.GetExecutingAssembly();

            string resourceName = asm.GetName().Name + ".Resources." + fileName;

            Stream file = asm.GetManifestResourceStream(resourceName);

            return Image.FromStream(file);
        }
    }
}