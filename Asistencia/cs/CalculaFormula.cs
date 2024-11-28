using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Windows.Forms;
using System.Configuration;
using System.Data.SqlClient;

namespace CaniaBrava
{
    class CalculaFormula
    {
        public string calculaFormula(DataTable dt, string formula, string idcia, string idtipoplan, string idtipoper, string idtipocal, string anio, string messem, string idperplan, string idconplan)
        {
            string constante;
            decimal valor_constante;
            string tipo;
            string grupo;

            MathParser mp = new MathParser();

            ////////////////////////////////////////////////////////////////////////////////////
            ///ASIGNA LOS VALORES A LAS CONSTANTES RESPECTIVAS PARA EL CALCULO DE LA FORMULA////
            ////////////////////////////////////////////////////////////////////////////////////

            foreach (DataRow row in dt.Rows)
            {
                constante = row["constante"].ToString();
                valor_constante = decimal.Parse(row["valor"].ToString());
                tipo = row["tipo"].ToString();
                grupo = row["grupo"].ToString();

                if (constante.Substring(0, 1).Equals("K"))
                {
                    if (grupo.Equals("1"))
                    {
                        if (constante.Equals("KA"))
                        { mp.Parameters.Add(Parameters.KA, valor_constante); }
                        else
                        {
                            if (constante.Equals("KB"))
                            { mp.Parameters.Add(Parameters.KB, valor_constante); }
                            else
                            {
                                if (constante.Equals("KC"))
                                { mp.Parameters.Add(Parameters.KC, valor_constante); }
                                else
                                {
                                    if (constante.Equals("KD"))
                                    { mp.Parameters.Add(Parameters.KD, valor_constante); }
                                    else
                                    {
                                        if (constante.Equals("KE"))
                                        { mp.Parameters.Add(Parameters.KE, valor_constante); }
                                    }
                                }
                            }
                        }
                    }

                    if (grupo.Equals("2"))
                    {
                        if (constante.Equals("KF"))
                        { mp.Parameters.Add(Parameters.KF, valor_constante); }
                        else
                        {
                            if (constante.Equals("KG"))
                            { mp.Parameters.Add(Parameters.KG, valor_constante); }
                            else
                            {
                                if (constante.Equals("KH"))
                                { mp.Parameters.Add(Parameters.KH, valor_constante); }
                                else
                                {
                                    if (constante.Equals("KI"))
                                    { mp.Parameters.Add(Parameters.KI, valor_constante); }
                                    else
                                    {
                                        if (constante.Equals("KJ"))
                                        { mp.Parameters.Add(Parameters.KJ, valor_constante); }
                                    }
                                }
                            }
                        }
                    }
                    if (grupo.Equals("3"))
                    {
                        if (constante.Equals("KK"))
                        { mp.Parameters.Add(Parameters.KK, valor_constante); }
                        else
                        {
                            if (constante.Equals("KL"))
                            { mp.Parameters.Add(Parameters.KL, valor_constante); }
                            else
                            {
                                if (constante.Equals("KM"))
                                { mp.Parameters.Add(Parameters.KM, valor_constante); }
                                else
                                {
                                    if (constante.Equals("KN"))
                                    { mp.Parameters.Add(Parameters.KN, valor_constante); }
                                    else
                                    {
                                        if (constante.Equals("KO"))
                                        { mp.Parameters.Add(Parameters.KO, valor_constante); }
                                    }
                                }
                            }
                        }
                    }
                    if (grupo.Equals("4"))
                    {
                        if (constante.Equals("KP"))
                        { mp.Parameters.Add(Parameters.KP, valor_constante); }
                        else
                        {
                            if (constante.Equals("KQ"))
                            { mp.Parameters.Add(Parameters.KQ, valor_constante); }
                            else
                            {
                                if (constante.Equals("KR"))
                                { mp.Parameters.Add(Parameters.KR, valor_constante); }
                                else
                                {
                                    if (constante.Equals("KS"))
                                    { mp.Parameters.Add(Parameters.KS, valor_constante); }
                                    else
                                    {
                                        if (constante.Equals("KT"))
                                        { mp.Parameters.Add(Parameters.KT, valor_constante); }
                                    }
                                }
                            }
                        }
                    }
                    if (grupo.Equals("5"))
                    {
                        if (constante.Equals("KU"))
                        { mp.Parameters.Add(Parameters.KU, valor_constante); }
                        else
                        {
                            if (constante.Equals("KV"))
                            { mp.Parameters.Add(Parameters.KV, valor_constante); }
                            else
                            {
                                if (constante.Equals("KW"))
                                { mp.Parameters.Add(Parameters.KW, valor_constante); }
                                else
                                {
                                    if (constante.Equals("KX"))
                                    { mp.Parameters.Add(Parameters.KX, valor_constante); }
                                    else
                                    {
                                        if (constante.Equals("KY"))
                                        { mp.Parameters.Add(Parameters.KY, valor_constante); }
                                        else
                                        {
                                            if (constante.Equals("KZ"))
                                            { mp.Parameters.Add(Parameters.KZ, valor_constante); }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }

                if (constante.Substring(0, 1).Equals("C"))
                {

                    if (grupo.Equals("1"))
                    {
                        if (constante.Equals("CA"))
                        { mp.Parameters.Add(Parameters.CA, valor_constante); }
                        else
                        {
                            if (constante.Equals("CB"))
                            { mp.Parameters.Add(Parameters.CB, valor_constante); }
                            else
                            {
                                if (constante.Equals("CC"))
                                { mp.Parameters.Add(Parameters.CC, valor_constante); }
                                else
                                {
                                    if (constante.Equals("CD"))
                                    { mp.Parameters.Add(Parameters.CD, valor_constante); }
                                    else
                                    {
                                        if (constante.Equals("CE"))
                                        { mp.Parameters.Add(Parameters.CE, valor_constante); }
                                    }
                                }
                            }
                        }
                    }
                    if (grupo.Equals("2"))
                    {
                        if (constante.Equals("CF"))
                        { mp.Parameters.Add(Parameters.CF, valor_constante); }
                        else
                        {
                            if (constante.Equals("CG"))
                            { mp.Parameters.Add(Parameters.CG, valor_constante); }
                            else
                            {
                                if (constante.Equals("CH"))
                                { mp.Parameters.Add(Parameters.CH, valor_constante); }
                                else
                                {
                                    if (constante.Equals("CI"))
                                    { mp.Parameters.Add(Parameters.CI, valor_constante); }
                                    else
                                    {
                                        if (constante.Equals("CJ"))
                                        { mp.Parameters.Add(Parameters.CJ, valor_constante); }
                                    }
                                }
                            }
                        }
                    }
                    if (grupo.Equals("3"))
                    {
                        if (constante.Equals("CK"))
                        { mp.Parameters.Add(Parameters.CK, valor_constante); }
                        else
                        {
                            if (constante.Equals("CL"))
                            { mp.Parameters.Add(Parameters.CL, valor_constante); }
                            else
                            {
                                if (constante.Equals("CM"))
                                { mp.Parameters.Add(Parameters.CM, valor_constante); }
                                else
                                {
                                    if (constante.Equals("CN"))
                                    { mp.Parameters.Add(Parameters.CN, valor_constante); }
                                    else
                                    {
                                        if (constante.Equals("CO"))
                                        { mp.Parameters.Add(Parameters.CO, valor_constante); }
                                    }
                                }
                            }
                        }
                    }
                    if (grupo.Equals("4"))
                    {
                        if (constante.Equals("CP"))
                        { mp.Parameters.Add(Parameters.CP, valor_constante); }
                        else
                        {
                            if (constante.Equals("CQ"))
                            { mp.Parameters.Add(Parameters.CQ, valor_constante); }
                            else
                            {
                                if (constante.Equals("CR"))
                                { mp.Parameters.Add(Parameters.CR, valor_constante); }
                                else
                                {
                                    if (constante.Equals("CS"))
                                    { mp.Parameters.Add(Parameters.CS, valor_constante); }
                                    else
                                    {
                                        if (constante.Equals("CT"))
                                        { mp.Parameters.Add(Parameters.CT, valor_constante); }
                                    }
                                }
                            }
                        }
                    }
                    if (grupo.Equals("5"))
                    {
                        if (constante.Equals("CU"))
                        { mp.Parameters.Add(Parameters.CU, valor_constante); }
                        else
                        {
                            if (constante.Equals("CV"))
                            { mp.Parameters.Add(Parameters.CV, valor_constante); }
                            else
                            {
                                if (constante.Equals("CW"))
                                { mp.Parameters.Add(Parameters.CW, valor_constante); }
                                else
                                {
                                    if (constante.Equals("CX"))
                                    { mp.Parameters.Add(Parameters.CX, valor_constante); }
                                    else
                                    {
                                        if (constante.Equals("CY"))
                                        { mp.Parameters.Add(Parameters.CY, valor_constante); }
                                        else
                                        {
                                            if (constante.Equals("CZ"))
                                            { mp.Parameters.Add(Parameters.CZ, valor_constante); }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }

                if (constante.Substring(0, 1).Equals("A"))
                {
                    if (grupo.Equals("1"))
                    {
                        if (constante.Equals("AA"))
                        { mp.Parameters.Add(Parameters.AA, valor_constante); }
                        else
                        {
                            if (constante.Equals("AB"))
                            { mp.Parameters.Add(Parameters.AB, valor_constante); }
                            else
                            {
                                if (constante.Equals("AC"))
                                { mp.Parameters.Add(Parameters.AC, valor_constante); }
                                else
                                {
                                    if (constante.Equals("AD"))
                                    { mp.Parameters.Add(Parameters.AD, valor_constante); }
                                    else
                                    {
                                        if (constante.Equals("AE"))
                                        { mp.Parameters.Add(Parameters.AE, valor_constante); }
                                    }
                                }
                            }
                        }
                    }
                    if (grupo.Equals("2"))
                    {
                        if (constante.Equals("AF"))
                        { mp.Parameters.Add(Parameters.AF, valor_constante); }
                        else
                        {
                            if (constante.Equals("AG"))
                            { mp.Parameters.Add(Parameters.AG, valor_constante); }
                            else
                            {
                                if (constante.Equals("AH"))
                                { mp.Parameters.Add(Parameters.AH, valor_constante); }
                                else
                                {
                                    if (constante.Equals("AI"))
                                    { mp.Parameters.Add(Parameters.AI, valor_constante); }
                                    else
                                    {
                                        if (constante.Equals("AJ"))
                                        { mp.Parameters.Add(Parameters.AJ, valor_constante); }
                                    }
                                }
                            }
                        }
                    }
                    if (grupo.Equals("3"))
                    {
                        if (constante.Equals("AK"))
                        { mp.Parameters.Add(Parameters.AK, valor_constante); }
                        else
                        {
                            if (constante.Equals("AL"))
                            { mp.Parameters.Add(Parameters.AL, valor_constante); }
                            else
                            {
                                if (constante.Equals("AM"))
                                { mp.Parameters.Add(Parameters.AM, valor_constante); }
                                else
                                {
                                    if (constante.Equals("AN"))
                                    { mp.Parameters.Add(Parameters.AN, valor_constante); }
                                    else
                                    {
                                        if (constante.Equals("AO"))
                                        { mp.Parameters.Add(Parameters.AO, valor_constante); }
                                    }
                                }
                            }
                        }
                    }
                    if (grupo.Equals("4"))
                    {
                        if (constante.Equals("AP"))
                        { mp.Parameters.Add(Parameters.AP, valor_constante); }
                        else
                        {
                            if (constante.Equals("AQ"))
                            { mp.Parameters.Add(Parameters.AQ, valor_constante); }
                            else
                            {
                                if (constante.Equals("AR"))
                                { mp.Parameters.Add(Parameters.AR, valor_constante); }
                                else
                                {
                                    if (constante.Equals("AS"))
                                    { mp.Parameters.Add(Parameters.AS, valor_constante); }
                                    else
                                    {
                                        if (constante.Equals("AT"))
                                        { mp.Parameters.Add(Parameters.AT, valor_constante); }
                                    }
                                }
                            }
                        }
                    }
                    if (grupo.Equals("5"))
                    {
                        if (constante.Equals("AU"))
                        { mp.Parameters.Add(Parameters.AU, valor_constante); }
                        else
                        {
                            if (constante.Equals("AV"))
                            { mp.Parameters.Add(Parameters.AV, valor_constante); }
                            else
                            {
                                if (constante.Equals("AW"))
                                { mp.Parameters.Add(Parameters.AW, valor_constante); }
                                else
                                {
                                    if (constante.Equals("AX"))
                                    { mp.Parameters.Add(Parameters.AX, valor_constante); }
                                    else
                                    {
                                        if (constante.Equals("AY"))
                                        { mp.Parameters.Add(Parameters.AY, valor_constante); }
                                        else
                                        {
                                            if (constante.Equals("AZ"))
                                            { mp.Parameters.Add(Parameters.AZ, valor_constante); }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }

                if (constante.Substring(0, 1).Equals("B"))
                {
                    if (grupo.Equals("1"))
                    {
                        if (constante.Equals("BA"))
                        { mp.Parameters.Add(Parameters.BA, valor_constante); }
                        else
                        {
                            if (constante.Equals("BB"))
                            { mp.Parameters.Add(Parameters.BB, valor_constante); }
                            else
                            {
                                if (constante.Equals("BC"))
                                { mp.Parameters.Add(Parameters.BC, valor_constante); }
                                else
                                {
                                    if (constante.Equals("BD"))
                                    { mp.Parameters.Add(Parameters.BD, valor_constante); }
                                    else
                                    {
                                        if (constante.Equals("BE"))
                                        { mp.Parameters.Add(Parameters.BE, valor_constante); }
                                    }
                                }
                            }
                        }
                    }
                    if (grupo.Equals("2"))
                    {
                        if (constante.Equals("BF"))
                        { mp.Parameters.Add(Parameters.BF, valor_constante); }
                        else
                        {
                            if (constante.Equals("BG"))
                            { mp.Parameters.Add(Parameters.BG, valor_constante); }
                            else
                            {
                                if (constante.Equals("BH"))
                                { mp.Parameters.Add(Parameters.BH, valor_constante); }
                                else
                                {
                                    if (constante.Equals("BI"))
                                    { mp.Parameters.Add(Parameters.BI, valor_constante); }
                                    else
                                    {
                                        if (constante.Equals("BJ"))
                                        { mp.Parameters.Add(Parameters.BJ, valor_constante); }
                                    }
                                }
                            }
                        }
                    }
                    if (grupo.Equals("3"))
                    {
                        if (constante.Equals("BK"))
                        { mp.Parameters.Add(Parameters.BK, valor_constante); }
                        else
                        {
                            if (constante.Equals("BL"))
                            { mp.Parameters.Add(Parameters.BL, valor_constante); }
                            else
                            {
                                if (constante.Equals("BM"))
                                { mp.Parameters.Add(Parameters.BM, valor_constante); }
                                else
                                {
                                    if (constante.Equals("BN"))
                                    { mp.Parameters.Add(Parameters.BN, valor_constante); }
                                    else
                                    {
                                        if (constante.Equals("BO"))
                                        { mp.Parameters.Add(Parameters.BO, valor_constante); }
                                    }
                                }
                            }
                        }
                    }
                    if (grupo.Equals("4"))
                    {
                        if (constante.Equals("BP"))
                        { mp.Parameters.Add(Parameters.BP, valor_constante); }
                        else
                        {
                            if (constante.Equals("BQ"))
                            { mp.Parameters.Add(Parameters.BQ, valor_constante); }
                            else
                            {
                                if (constante.Equals("BR"))
                                { mp.Parameters.Add(Parameters.BR, valor_constante); }
                                else
                                {
                                    if (constante.Equals("BS"))
                                    { mp.Parameters.Add(Parameters.BS, valor_constante); }
                                    else
                                    {
                                        if (constante.Equals("BT"))
                                        { mp.Parameters.Add(Parameters.BT, valor_constante); }
                                    }
                                }
                            }
                        }
                    }
                    if (grupo.Equals("5"))
                    {
                        if (constante.Equals("BU"))
                        { mp.Parameters.Add(Parameters.BU, valor_constante); }
                        else
                        {
                            if (constante.Equals("BV"))
                            { mp.Parameters.Add(Parameters.BV, valor_constante); }
                            else
                            {
                                if (constante.Equals("BW"))
                                { mp.Parameters.Add(Parameters.BW, valor_constante); }
                                else
                                {
                                    if (constante.Equals("BX"))
                                    { mp.Parameters.Add(Parameters.BX, valor_constante); }
                                    else
                                    {
                                        if (constante.Equals("BY"))
                                        { mp.Parameters.Add(Parameters.BY, valor_constante); }
                                        else
                                        {
                                            if (constante.Equals("BZ"))
                                            { mp.Parameters.Add(Parameters.BZ, valor_constante); }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }

                if (constante.Substring(0, 1).Equals("T"))
                {
                    if (grupo.Equals("1"))
                    {
                        if (constante.Equals("TA"))
                        { mp.Parameters.Add(Parameters.TA, valor_constante); }
                        else
                        {
                            if (constante.Equals("TB"))
                            { mp.Parameters.Add(Parameters.TB, valor_constante); }
                            else
                            {
                                if (constante.Equals("TC"))
                                { mp.Parameters.Add(Parameters.TC, valor_constante); }
                                else
                                {
                                    if (constante.Equals("TD"))
                                    { mp.Parameters.Add(Parameters.TD, valor_constante); }
                                    else
                                    {
                                        if (constante.Equals("TE"))
                                        { mp.Parameters.Add(Parameters.TE, valor_constante); }
                                    }
                                }
                            }
                        }
                    }
                    if (grupo.Equals("2"))
                    {
                        if (constante.Equals("TF"))
                        { mp.Parameters.Add(Parameters.TF, valor_constante); }
                        else
                        {
                            if (constante.Equals("TG"))
                            { mp.Parameters.Add(Parameters.TG, valor_constante); }
                            else
                            {
                                if (constante.Equals("TH"))
                                { mp.Parameters.Add(Parameters.TH, valor_constante); }
                                else
                                {
                                    if (constante.Equals("TI"))
                                    { mp.Parameters.Add(Parameters.TI, valor_constante); }
                                    else
                                    {
                                        if (constante.Equals("TJ"))
                                        { mp.Parameters.Add(Parameters.TJ, valor_constante); }
                                    }
                                }
                            }
                        }
                    }
                }

            }
            try
            {
                DataTable dtconstantes = new DataTable();
                SqlConnection conexion = new SqlConnection();
                conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
                conexion.Open();

                SqlDataAdapter da = new SqlDataAdapter();

                string query = " select idconstante,tipo,grupo from constante where ";
                query = query + " idconstante not in (select constante from procplan where idcia='" + @idcia + "' ";
                query = query + " and idtipoplan='" + @idtipoplan + "' and idtipocal='" + @idtipocal + "' and ";
                query = query + " messem='" + @messem + "' and anio='" + @anio + "' and idtipoper='" + @idtipoper + "'); ";

                /*string query =  " select idconstante,tipo,grupo from constante where ";
                query = query + " idconstante not in (select constante from procplan) ";
                query = query + " and idconstante in (select constante from conplan where idcia='" + @idcia + "' ";
                query = query + " and idtipoplan='" + @idtipoplan + "' and idtipocal='" + @idtipocal + "' ";
                query = query + " and idconplan<'" + @idconplan + "');";*/
                da.SelectCommand = new SqlCommand(query, conexion);
                da.Fill(dtconstantes);

                foreach (DataRow row_dtconstantes in dtconstantes.Rows)
                {
                    int valor_ini = 0;
                    constante = row_dtconstantes["idconstante"].ToString();
                    tipo = row_dtconstantes["tipo"].ToString();
                    grupo = row_dtconstantes["grupo"].ToString();

                    if (constante.Substring(0, 1).Equals("K"))
                    {
                        if (grupo.Equals("1"))
                        {
                            if (constante.Equals("KA"))
                            { mp.Parameters.Add(Parameters.KA, valor_ini); }
                            else
                            {
                                if (constante.Equals("KB"))
                                { mp.Parameters.Add(Parameters.KB, valor_ini); }
                                else
                                {
                                    if (constante.Equals("KC"))
                                    { mp.Parameters.Add(Parameters.KC, valor_ini); }
                                    else
                                    {
                                        if (constante.Equals("KD"))
                                        { mp.Parameters.Add(Parameters.KD, valor_ini); }
                                        else
                                        {
                                            if (constante.Equals("KE"))
                                            { mp.Parameters.Add(Parameters.KE, valor_ini); }
                                        }
                                    }
                                }
                            }
                        }
                        if (grupo.Equals("2"))
                        {
                            if (constante.Equals("KF"))
                            { mp.Parameters.Add(Parameters.KF, valor_ini); }
                            else
                            {
                                if (constante.Equals("KG"))
                                { mp.Parameters.Add(Parameters.KG, valor_ini); }
                                else
                                {
                                    if (constante.Equals("KH"))
                                    { mp.Parameters.Add(Parameters.KH, valor_ini); }
                                    else
                                    {
                                        if (constante.Equals("KI"))
                                        { mp.Parameters.Add(Parameters.KI, valor_ini); }
                                        else
                                        {
                                            if (constante.Equals("KJ"))
                                            { mp.Parameters.Add(Parameters.KJ, valor_ini); }
                                        }
                                    }
                                }
                            }
                        }
                        if (grupo.Equals("3"))
                        {
                            if (constante.Equals("KK"))
                            { mp.Parameters.Add(Parameters.KK, valor_ini); }
                            else
                            {
                                if (constante.Equals("KL"))
                                { mp.Parameters.Add(Parameters.KL, valor_ini); }
                                else
                                {
                                    if (constante.Equals("KM"))
                                    { mp.Parameters.Add(Parameters.KM, valor_ini); }
                                    else
                                    {
                                        if (constante.Equals("KN"))
                                        { mp.Parameters.Add(Parameters.KN, valor_ini); }
                                        else
                                        {
                                            if (constante.Equals("KO"))
                                            { mp.Parameters.Add(Parameters.KO, valor_ini); }
                                        }
                                    }
                                }
                            }
                        }
                        if (grupo.Equals("4"))
                        {
                            if (constante.Equals("KP"))
                            {
                                mp.Parameters.Add(Parameters.KP, valor_ini);
                            }
                            else
                            {
                                if (constante.Equals("KQ"))
                                { mp.Parameters.Add(Parameters.KQ, valor_ini); }
                                else
                                {
                                    if (constante.Equals("KR"))
                                    { mp.Parameters.Add(Parameters.KR, valor_ini); }
                                    else
                                    {
                                        if (constante.Equals("KS"))
                                        { mp.Parameters.Add(Parameters.KS, valor_ini); }
                                        else
                                        {
                                            if (constante.Equals("KT"))
                                            { mp.Parameters.Add(Parameters.KT, valor_ini); }
                                        }
                                    }
                                }
                            }
                        }
                        if (grupo.Equals("5"))
                        {
                            if (constante.Equals("KU"))
                            { mp.Parameters.Add(Parameters.KU, valor_ini); }
                            else
                            {
                                if (constante.Equals("KV"))
                                { mp.Parameters.Add(Parameters.KV, valor_ini); }
                                else
                                {
                                    if (constante.Equals("KW"))
                                    { mp.Parameters.Add(Parameters.KW, valor_ini); }
                                    else
                                    {
                                        if (constante.Equals("KX"))
                                        { mp.Parameters.Add(Parameters.KX, valor_ini); }
                                        else
                                        {
                                            if (constante.Equals("KY"))
                                            { mp.Parameters.Add(Parameters.KY, valor_ini); }
                                            else
                                            {
                                                if (constante.Equals("KZ"))
                                                { mp.Parameters.Add(Parameters.KZ, valor_ini); }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                    if (constante.Substring(0, 1).Equals("C"))
                    {
                        if (grupo.Equals("1"))
                        {
                            if (constante.Equals("CA"))
                            { mp.Parameters.Add(Parameters.CA, valor_ini); }
                            else
                            {
                                if (constante.Equals("CB"))
                                { mp.Parameters.Add(Parameters.CB, valor_ini); }
                                else
                                {
                                    if (constante.Equals("CC"))
                                    { mp.Parameters.Add(Parameters.CC, valor_ini); }
                                    else
                                    {
                                        if (constante.Equals("CD"))
                                        { mp.Parameters.Add(Parameters.CD, valor_ini); }
                                        else
                                        {
                                            if (constante.Equals("CE"))
                                            { mp.Parameters.Add(Parameters.CE, valor_ini); }
                                        }
                                    }
                                }
                            }
                        }
                        if (grupo.Equals("2"))
                        {
                            if (constante.Equals("CF"))
                            { mp.Parameters.Add(Parameters.CF, valor_ini); }
                            else
                            {
                                if (constante.Equals("CG"))
                                { mp.Parameters.Add(Parameters.CG, valor_ini); }
                                else
                                {
                                    if (constante.Equals("CH"))
                                    { mp.Parameters.Add(Parameters.CH, valor_ini); }
                                    else
                                    {
                                        if (constante.Equals("CI"))
                                        { mp.Parameters.Add(Parameters.CI, valor_ini); }
                                        else
                                        {
                                            if (constante.Equals("CJ"))
                                            { mp.Parameters.Add(Parameters.CJ, valor_ini); }
                                        }
                                    }
                                }
                            }
                        }
                        if (grupo.Equals("3"))
                        {
                            if (constante.Equals("CK"))
                            { mp.Parameters.Add(Parameters.CK, valor_ini); }
                            else
                            {
                                if (constante.Equals("CL"))
                                { mp.Parameters.Add(Parameters.CL, valor_ini); }
                                else
                                {
                                    if (constante.Equals("CM"))
                                    { mp.Parameters.Add(Parameters.CM, valor_ini); }
                                    else
                                    {
                                        if (constante.Equals("CN"))
                                        { mp.Parameters.Add(Parameters.CN, valor_ini); }
                                        else
                                        {
                                            if (constante.Equals("CO"))
                                            { mp.Parameters.Add(Parameters.CO, valor_ini); }
                                        }
                                    }
                                }
                            }
                        }
                        if (grupo.Equals("4"))
                        {
                            if (constante.Equals("CP"))
                            { mp.Parameters.Add(Parameters.CP, valor_ini); }
                            else
                            {
                                if (constante.Equals("CQ"))
                                { mp.Parameters.Add(Parameters.CQ, valor_ini); }
                                else
                                {
                                    if (constante.Equals("CR"))
                                    { mp.Parameters.Add(Parameters.CR, valor_ini); }
                                    else
                                    {
                                        if (constante.Equals("CS"))
                                        { mp.Parameters.Add(Parameters.CS, valor_ini); }
                                        else
                                        {
                                            if (constante.Equals("CT"))
                                            { mp.Parameters.Add(Parameters.CT, valor_ini); }
                                        }
                                    }
                                }
                            }
                        }
                        if (grupo.Equals("5"))
                        {
                            if (constante.Equals("CU"))
                            { mp.Parameters.Add(Parameters.CU, valor_ini); }
                            else
                            {
                                if (constante.Equals("CV"))
                                { mp.Parameters.Add(Parameters.CV, valor_ini); }
                                else
                                {
                                    if (constante.Equals("CW"))
                                    { mp.Parameters.Add(Parameters.CW, valor_ini); }
                                    else
                                    {
                                        if (constante.Equals("CX"))
                                        { mp.Parameters.Add(Parameters.CX, valor_ini); }
                                        else
                                        {
                                            if (constante.Equals("CY"))
                                            { mp.Parameters.Add(Parameters.CY, valor_ini); }
                                            else
                                            {
                                                if (constante.Equals("CZ"))
                                                { mp.Parameters.Add(Parameters.CZ, valor_ini); }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }

                    if (constante.Substring(0, 1).Equals("A"))
                    {
                        if (grupo.Equals("1"))
                        {
                            if (constante.Equals("AA"))
                            { mp.Parameters.Add(Parameters.AA, valor_ini); }
                            else
                            {
                                if (constante.Equals("AB"))
                                { mp.Parameters.Add(Parameters.AB, valor_ini); }
                                else
                                {
                                    if (constante.Equals("AC"))
                                    { mp.Parameters.Add(Parameters.AC, valor_ini); }
                                    else
                                    {
                                        if (constante.Equals("AD"))
                                        { mp.Parameters.Add(Parameters.AD, valor_ini); }
                                        else
                                        {
                                            if (constante.Equals("AE"))
                                            { mp.Parameters.Add(Parameters.AE, valor_ini); }
                                        }
                                    }
                                }
                            }
                        }
                        if (grupo.Equals("2"))
                        {
                            if (constante.Equals("AF"))
                            { mp.Parameters.Add(Parameters.AF, valor_ini); }
                            else
                            {
                                if (constante.Equals("AG"))
                                { mp.Parameters.Add(Parameters.AG, valor_ini); }
                                else
                                {
                                    if (constante.Equals("AH"))
                                    { mp.Parameters.Add(Parameters.AH, valor_ini); }
                                    else
                                    {
                                        if (constante.Equals("AI"))
                                        { mp.Parameters.Add(Parameters.AI, valor_ini); }
                                        else
                                        {
                                            if (constante.Equals("AJ"))
                                            { mp.Parameters.Add(Parameters.AJ, valor_ini); }
                                        }
                                    }
                                }
                            }
                        }
                        if (grupo.Equals("3"))
                        {
                            if (constante.Equals("AK"))
                            { mp.Parameters.Add(Parameters.AK, valor_ini); }
                            else
                            {
                                if (constante.Equals("AL"))
                                { mp.Parameters.Add(Parameters.AL, valor_ini); }
                                else
                                {
                                    if (constante.Equals("AM"))
                                    { mp.Parameters.Add(Parameters.AM, valor_ini); }
                                    else
                                    {
                                        if (constante.Equals("AN"))
                                        { mp.Parameters.Add(Parameters.AN, valor_ini); }
                                        else
                                        {
                                            if (constante.Equals("AO"))
                                            { mp.Parameters.Add(Parameters.AO, valor_ini); }
                                        }
                                    }
                                }
                            }
                        }
                        if (grupo.Equals("4"))
                        {
                            if (constante.Equals("AP"))
                            { mp.Parameters.Add(Parameters.AP, valor_ini); }
                            else
                            {
                                if (constante.Equals("AQ"))
                                { mp.Parameters.Add(Parameters.AQ, valor_ini); }
                                else
                                {
                                    if (constante.Equals("AR"))
                                    { mp.Parameters.Add(Parameters.AR, valor_ini); }
                                    else
                                    {
                                        if (constante.Equals("AS"))
                                        { mp.Parameters.Add(Parameters.AS, valor_ini); }
                                        else
                                        {
                                            if (constante.Equals("AT"))
                                            { mp.Parameters.Add(Parameters.AT, valor_ini); }
                                        }
                                    }
                                }
                            }
                        }
                        if (grupo.Equals("5"))
                        {
                            if (constante.Equals("AU"))
                            { mp.Parameters.Add(Parameters.AU, valor_ini); }
                            else
                            {
                                if (constante.Equals("AV"))
                                { mp.Parameters.Add(Parameters.AV, valor_ini); }
                                else
                                {
                                    if (constante.Equals("AW"))
                                    { mp.Parameters.Add(Parameters.AW, valor_ini); }
                                    else
                                    {
                                        if (constante.Equals("AX"))
                                        { mp.Parameters.Add(Parameters.AX, valor_ini); }
                                        else
                                        {
                                            if (constante.Equals("AY"))
                                            { mp.Parameters.Add(Parameters.AY, valor_ini); }
                                            else
                                            {
                                                if (constante.Equals("AZ"))
                                                { mp.Parameters.Add(Parameters.AZ, valor_ini); }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                    if (constante.Substring(0, 1).Equals("B"))
                    {
                        if (grupo.Equals("1"))
                        {
                            if (constante.Equals("BA"))
                            { mp.Parameters.Add(Parameters.BA, valor_ini); }
                            else
                            {
                                if (constante.Equals("BB"))
                                { mp.Parameters.Add(Parameters.BB, valor_ini); }
                                else
                                {
                                    if (constante.Equals("BC"))
                                    { mp.Parameters.Add(Parameters.BC, valor_ini); }
                                    else
                                    {
                                        if (constante.Equals("BD"))
                                        { mp.Parameters.Add(Parameters.BD, valor_ini); }
                                        else
                                        {
                                            if (constante.Equals("BE"))
                                            { mp.Parameters.Add(Parameters.BE, valor_ini); }
                                        }
                                    }
                                }
                            }
                        }
                        if (grupo.Equals("2"))
                        {
                            if (constante.Equals("BF"))
                            { mp.Parameters.Add(Parameters.BF, valor_ini); }
                            else
                            {
                                if (constante.Equals("BG"))
                                { mp.Parameters.Add(Parameters.BG, valor_ini); }
                                else
                                {
                                    if (constante.Equals("BH"))
                                    { mp.Parameters.Add(Parameters.BH, valor_ini); }
                                    else
                                    {
                                        if (constante.Equals("BI"))
                                        { mp.Parameters.Add(Parameters.BI, valor_ini); }
                                        else
                                        {
                                            if (constante.Equals("BJ"))
                                            { mp.Parameters.Add(Parameters.BJ, valor_ini); }
                                        }
                                    }
                                }
                            }
                        }
                        if (grupo.Equals("3"))
                        {
                            if (constante.Equals("BK"))
                            { mp.Parameters.Add(Parameters.BK, valor_ini); }
                            else
                            {
                                if (constante.Equals("BL"))
                                { mp.Parameters.Add(Parameters.BL, valor_ini); }
                                else
                                {
                                    if (constante.Equals("BM"))
                                    { mp.Parameters.Add(Parameters.BM, valor_ini); }
                                    else
                                    {
                                        if (constante.Equals("BN"))
                                        { mp.Parameters.Add(Parameters.BN, valor_ini); }
                                        else
                                        {
                                            if (constante.Equals("BO"))
                                            { mp.Parameters.Add(Parameters.BO, valor_ini); }
                                        }
                                    }
                                }
                            }
                        }
                        if (grupo.Equals("4"))
                        {
                            if (constante.Equals("BP"))
                            { mp.Parameters.Add(Parameters.BP, valor_ini); }
                            else
                            {
                                if (constante.Equals("BQ"))
                                { mp.Parameters.Add(Parameters.BQ, valor_ini); }
                                else
                                {
                                    if (constante.Equals("BR"))
                                    { mp.Parameters.Add(Parameters.BR, valor_ini); }
                                    else
                                    {
                                        if (constante.Equals("BS"))
                                        { mp.Parameters.Add(Parameters.BS, valor_ini); }
                                        else
                                        {
                                            if (constante.Equals("BT"))
                                            { mp.Parameters.Add(Parameters.BT, valor_ini); }
                                        }
                                    }
                                }
                            }
                        }
                        if (grupo.Equals("5"))
                        {
                            if (constante.Equals("BU"))
                            { mp.Parameters.Add(Parameters.BU, valor_ini); }
                            else
                            {
                                if (constante.Equals("BV"))
                                { mp.Parameters.Add(Parameters.BV, valor_ini); }
                                else
                                {
                                    if (constante.Equals("BW"))
                                    { mp.Parameters.Add(Parameters.BW, valor_ini); }
                                    else
                                    {
                                        if (constante.Equals("BX"))
                                        { mp.Parameters.Add(Parameters.BX, valor_ini); }
                                        else
                                        {
                                            if (constante.Equals("BY"))
                                            { mp.Parameters.Add(Parameters.BY, valor_ini); }
                                            else
                                            {
                                                if (constante.Equals("BZ"))
                                                { mp.Parameters.Add(Parameters.BZ, valor_ini); }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                    if (constante.Substring(0, 1).Equals("T"))
                    {
                        if (grupo.Equals("1"))
                        {
                            if (constante.Equals("TA"))
                            { mp.Parameters.Add(Parameters.TA, valor_ini); }
                            else
                            {
                                if (constante.Equals("TB"))
                                { mp.Parameters.Add(Parameters.TB, valor_ini); }
                                else
                                {
                                    if (constante.Equals("TC"))
                                    { mp.Parameters.Add(Parameters.TC, valor_ini); }
                                    else
                                    {
                                        if (constante.Equals("TD"))
                                        { mp.Parameters.Add(Parameters.TD, valor_ini); }
                                        else
                                        {
                                            if (constante.Equals("TE"))
                                            { mp.Parameters.Add(Parameters.TE, valor_ini); }
                                        }
                                    }
                                }
                            }
                        }
                        if (grupo.Equals("2"))
                        {
                            if (constante.Equals("TF"))
                            { mp.Parameters.Add(Parameters.TF, valor_ini); }
                            else
                            {
                                if (constante.Equals("TG"))
                                { mp.Parameters.Add(Parameters.TG, valor_ini); }
                                else
                                {
                                    if (constante.Equals("TH"))
                                    { mp.Parameters.Add(Parameters.TH, valor_ini); }
                                    else
                                    {
                                        if (constante.Equals("TI"))
                                        { mp.Parameters.Add(Parameters.TI, valor_ini); }
                                        else
                                        {
                                            if (constante.Equals("TJ"))
                                            { mp.Parameters.Add(Parameters.TJ, valor_ini); }
                                        }
                                    }
                                }
                            }
                        }

                    }
                }
            }
            catch { }

            decimal resultado = mp.Calculate(formula);

            if (resultado.Equals(-99999999))
            {
                MessageBox.Show("No se ha podido calcular el valor de la fórmula " + formula, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return Convert.ToString(resultado);
        }
    }
}