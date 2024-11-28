using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Data.SqlClient;

namespace CaniaBrava
{
    public class GlobalVariables
    {
        public static string idcia;
        public static string idusr;
        public static string typeusr;
        public static string ruccia;
        public static string namecia;
        public static string nroSerie;
        public static string nroSerieEncriptado;
        public static string email;
        public static string desusr;
        public static string nivelusr;

        public static string licruc;
        public static string licrazon;
        public static string licdireccion;
        public static string licemail;
        public static string autentica;
        public static string printerName;

        public GlobalVariables(){}

        public void setNroSerie(string nroSerie, string nroSerieEncriptado)
        {
            GlobalVariables.nroSerie = nroSerie;
            GlobalVariables.nroSerieEncriptado = nroSerieEncriptado;
        }

        public void setLicenciaUso(string licruc, string licrazon,string licdireccion,string licemail)
        {
            GlobalVariables.licruc = licruc;
            GlobalVariables.licrazon = licrazon;
            GlobalVariables.licdireccion = licdireccion;
            GlobalVariables.licemail = licemail;
        }

        public void setValores(string idcia, string idusr, string typeusr, string ruccia, string namecia, string mail, string desusr, string nivelusr)
        {
            GlobalVariables.idcia = idcia;
            GlobalVariables.idusr = idusr;
            GlobalVariables.typeusr = typeusr;
            GlobalVariables.ruccia = ruccia;
            GlobalVariables.namecia = namecia;
            GlobalVariables.email = mail;
            GlobalVariables.desusr = desusr;
            GlobalVariables.nivelusr = nivelusr;
        }

        public string getNroSerie()
        {
            return GlobalVariables.nroSerie;
        }

        public string getNroSerieEncriptado()
        {
            return GlobalVariables.nroSerieEncriptado;
        }

        public string getValorCia()
        {
            return GlobalVariables.idcia;
        }

        public string getValorUsr()
        {
            return GlobalVariables.idusr;
        }

        public string getValorUsrMail()
        {
            return GlobalVariables.email;
        }

        public string getValorTypeUsr()
        {
            return GlobalVariables.typeusr;
        }

        public string getValorRucCia()
        {
            return GlobalVariables.ruccia;
        }

        public string getValorNameCia()
        {
            return GlobalVariables.namecia;
        }

        public string getValorLicRuc()
        {
            return GlobalVariables.licruc;
        }

        public string getValorLicRazon()
        {
            return GlobalVariables.licrazon;
        }

        public string getValorLicDireccion()
        {
            return GlobalVariables.licdireccion;
        }

        public string getValorLicEmail()
        {
            return GlobalVariables.licemail;
        }

        public string getValorUsrName()
        {
            return GlobalVariables.desusr;
        }

        public void setAutentica(string autentica)
        {
            GlobalVariables.autentica = autentica;
        }

        public string getAutentica()
        {
            return GlobalVariables.autentica;
        }

        public string getPrinterName()
        {
            return GlobalVariables.printerName;
        }

        public void setPrinter(string printerName)
        {
            GlobalVariables.printerName = printerName;
        }

        public string getValorNivelUsr()
        {
            return GlobalVariables.nivelusr;
        }
    }
}