using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CaniaBrava
{
    class UtiFechas
    {
        public static bool IsDate(string strDate)
        {
            DateTime dtDate;

            bool bValida = true;

            try
            {
                dtDate = DateTime.Parse(strDate);
            }
            catch (FormatException)
            {
                bValida = false;
            }

            return bValida;
        }

        public int diferenciaEntreFechas(string fechaini, string fechafin)
        {
            int differenceInDays = 0;
            try
            {
                int diaini = int.Parse(fechaini.Substring(0, 2));
                int mesini = int.Parse(fechaini.Substring(3, 2));
                int anioini = int.Parse(fechaini.Substring(6, 4));
                int diafin = int.Parse(fechafin.Substring(0, 2));
                int mesfin = int.Parse(fechafin.Substring(3, 2));
                int aniofin = int.Parse(fechafin.Substring(6, 4));
                DateTime oldDate = new DateTime(anioini, mesini, diaini);
                DateTime newDate = new DateTime(aniofin, mesfin, diafin);
                // Difference in days, hours, and minutes.
                TimeSpan ts = newDate - oldDate;
                // Difference in days.
                differenceInDays = ts.Days + 1;
            }
            catch (Exception) { }

            return differenceInDays;
        }

        public static bool compararFecha(string Fecha1, string operadorComparacion, string Fecha2)
        {
            int diferencia;
            DateTime fechaParametro1;
            DateTime fechaParametro2;
            bool resultado = false;

            try
            {
                fechaParametro1 = DateTime.Parse(Fecha1);
                fechaParametro2 = DateTime.Parse(Fecha2);

                TimeSpan ts = fechaParametro1 - fechaParametro2;
                diferencia = ts.Days;

                if (operadorComparacion.Equals(">"))
                {
                    if (diferencia > 0)
                    {
                        resultado = true;
                    }
                    else { resultado = false; }
                }


                if (operadorComparacion.Equals(">="))
                {
                    if (diferencia >= 0)
                    {
                        resultado = true;
                    }
                    else { resultado = false; }
                }


                if (operadorComparacion.Equals("<"))
                {
                    if (diferencia < 0)
                    {
                        resultado = true;
                    }
                    else { resultado = false; }
                }

                if (operadorComparacion.Equals("<="))
                {
                    if (diferencia <= 0)
                    {
                        resultado = true;
                    }
                    else { resultado = false; }
                }

                if (operadorComparacion.Equals("="))
                {
                    if (diferencia == 0)
                    {
                        resultado = true;
                    }
                    else { resultado = false; }
                }

            }
            catch { resultado = false; }

            return resultado;
        }

        public string incrementarFecha(string fechaini, int incremento)
        {
            int diaini = int.Parse(fechaini.Substring(0, 2));
            int mesini = int.Parse(fechaini.Substring(3, 2));
            int anioini = int.Parse(fechaini.Substring(6, 4));

            DateTime oldDate = new DateTime(anioini, mesini, diaini);
            DateTime newDate = oldDate.AddDays(incremento);
            string newdia = Convert.ToString(newDate.Day);
            string newmes = Convert.ToString(newDate.Month);
            if (newdia.Length.Equals(1))
            {
                newdia = "0" + newdia;
            }
            if (newmes.Length.Equals(1))
            {
                newmes = "0" + newmes;
            }
            string fecha = newdia + "/" + newmes + '/' + newDate.Year;

            return fecha;
        }
    }
}