using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Configuration;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using CaniaBrava.Interface;

namespace CaniaBrava
{
    static class Program
    {
                
        /// <summary>
        /// Punto de entrada principal para la aplicación.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            SoftSecurity ss = new SoftSecurity();
            GlobalVariables globalVariable = new GlobalVariables();
          //   string nroSerie = ss.GetUniqueID();
          //  string nroSerieEncriptada = ui_EncriptCadena(nroSerie);
          //  globalVariable.setNroSerie(nroSerie, nroSerieEncriptada);

            FileInfo fa = new FileInfo(Application.StartupPath + "/license.txt");
            if (fa.Exists)
            {
                OpeIO opeIO = new OpeIO(Application.StartupPath + "/license.txt");
              //  if (nroSerieEncriptada.Equals(opeIO.ReadLineByNum(5)))
               // {
                    /*string licruc=ui_DecriptCadena(opeIO.ReadLineByNum(6));
                    string licrazon=ui_DecriptCadena(opeIO.ReadLineByNum(7)); 
                    string licdireccion=ui_DecriptCadena(opeIO.ReadLineByNum(8)); 
                    string licemail=ui_DecriptCadena(opeIO.ReadLineByNum(9));
                    globalVariable.setLicenciaUso(licruc,licrazon,licdireccion,licemail);
                    Application.Run(new ui_AccesoSis());*/
              //  }
               // else
                //{
                  //  MessageBox.Show("Licencia de Uso Inválida para el Nro. " + nroSerie + ". Por favor registre su Software", "mySQlPlan", MessageBoxButtons.OK, MessageBoxIcon.Error);
                   // ui_ActivarLicencia(Application.StartupPath + "/activar.txt", nroSerie);
                    
                //}
                string licruc = ui_DecriptCadena(opeIO.ReadLineByNum(6));
                string licrazon = ui_DecriptCadena(opeIO.ReadLineByNum(7));
                string licdireccion = ui_DecriptCadena(opeIO.ReadLineByNum(8));
                string licemail = ui_DecriptCadena(opeIO.ReadLineByNum(9));
                globalVariable.setLicenciaUso(licruc, licrazon, licdireccion, licemail);
                Application.Run(new ui_AccesoSis());
                //Application.Run(new ui_mqt_gastos());
                //Application.Run(new ui_mqt_gastos_maestros());
            }
            else
            {
                MessageBox.Show("No existe archivo de Licencia");
                //de Uso para el Nro. " + nroSerie+". Por favor registre su Software", "mySQlPlan", MessageBoxButtons.OK, MessageBoxIcon.Error);
               // ui_ActivarLicencia(Application.StartupPath + "/activar.txt", nroSerie);
            }
        }

        private static void ui_ActivarLicencia(string fileName,string nroSerie)
        {
            StreamWriter archivo = File.CreateText(fileName);
            archivo.Close();

            OpeIO opeIOActivar = new OpeIO(fileName);
            opeIOActivar.WriteNWL(nroSerie);
        }

        private static string ui_EncriptCadena(string cadena)
        {

            string passPhrase = ConfigurationManager.AppSettings.Get("PASS_PHRASE");
            string saltValue = ConfigurationManager.AppSettings.Get("SALT_VALUE");
            string hashAlgorithm = ConfigurationManager.AppSettings.Get("HASH_ALGORITHM");
            int passwordIterations = Convert.ToInt32(ConfigurationManager.AppSettings.Get("PASSWORD_ITERATIONS"));
            string initVector = ConfigurationManager.AppSettings.Get("INIT_VECTOR");
            int keySize = Convert.ToInt32(ConfigurationManager.AppSettings.Get("KEY_SIZE"));

            RijndaelSimple RijndaelSimple = new RijndaelSimple();
            string passEncriptUsrFile = RijndaelSimple.Encrypt(cadena, passPhrase, saltValue, hashAlgorithm, passwordIterations, initVector, keySize);
            return passEncriptUsrFile;

        }
        
        private static string ui_DecriptCadena(string cadenaEncriptada)
        {
            string passPhrase = ConfigurationManager.AppSettings.Get("PASS_PHRASE");
            string saltValue = ConfigurationManager.AppSettings.Get("SALT_VALUE");
            string hashAlgorithm = ConfigurationManager.AppSettings.Get("HASH_ALGORITHM");
            int passwordIterations = Convert.ToInt32(ConfigurationManager.AppSettings.Get("PASSWORD_ITERATIONS"));
            string initVector = ConfigurationManager.AppSettings.Get("INIT_VECTOR");
            int keySize = Convert.ToInt32(ConfigurationManager.AppSettings.Get("KEY_SIZE"));

            RijndaelSimple RijndaelSimple = new RijndaelSimple();
            return RijndaelSimple.Decrypt(cadenaEncriptada, passPhrase, saltValue, hashAlgorithm, passwordIterations, initVector, keySize);
        }

    }
}
