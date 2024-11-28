using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace CaniaBrava
{   
    public enum Parameters
    {
        KA, KB, KC, KD, KE, KF, KG, KH, KI, KJ, KK, KL, KM, KN, KO, KP, KQ, KR, KS, KT, KU, KV, KW, KX, KY, KZ,
        CA, CB, CC, CD, CE, CF, CG, CH, CI, CJ, CK, CL, CM, CN, CO, CP, CQ, CR, CS, CT, CU, CV, CW, CX, CY, CZ,
        AA, AB, AC, AD, AE, AF, AG, AH, AI, AJ, AK, AL, AM, AN, AO, AP, AQ, AR, AS, AT, AU, AV, AW, AX, AY, AZ,
        BA, BB, BC, BD, BE, BF, BG, BH, BI, BJ, BK, BL, BM, BN, BO, BP, BQ, BR, BS, BT, BU, BV, BW, BX, BY, BZ,
        TA, TB, TC, TD, TE, TF, TG, TH, TI, TJ
    }

    public class MathParser
    {
        private Dictionary<Parameters, decimal> _Parameters = new Dictionary<Parameters, decimal>();
        private List<String> OperationOrder = new List<string>();

        public Dictionary<Parameters, decimal> Parameters
        {
            get { return _Parameters; }
            set { _Parameters = value; }
        }

        public MathParser()
        {
            OperationOrder.Add("/");
            OperationOrder.Add("*");
            OperationOrder.Add("-");
            OperationOrder.Add("+");
        }

        public decimal Calculate(string Formula)
        {
            decimal resultado;

            try
            {
                string[] arr = Formula.Split("/+-*()".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                foreach (KeyValuePair<Parameters, decimal> de in _Parameters)
                {
                    foreach (string s in arr)
                    {
                        if (s != de.Key.ToString() && s.EndsWith(de.Key.ToString()))
                        {
                            Formula = Formula.Replace(s, (Convert.ToDecimal(s.Replace(de.Key.ToString(), "")) * de.Value).ToString());
                        }
                    }
                    Formula = Formula.Replace(de.Key.ToString(), de.Value.ToString());
                }
                while (Formula.LastIndexOf("(") > -1)
                {
                    int lastOpenPhrantesisIndex = Formula.LastIndexOf("(");
                    int firstClosePhrantesisIndexAfterLastOpened = Formula.IndexOf(")", lastOpenPhrantesisIndex);
                    decimal result = ProcessOperation(Formula.Substring(lastOpenPhrantesisIndex + 1, firstClosePhrantesisIndexAfterLastOpened - lastOpenPhrantesisIndex - 1));
                    bool AppendAsterix = false;
                    if (lastOpenPhrantesisIndex > 0)
                    {
                        if (Formula.Substring(lastOpenPhrantesisIndex - 1, 1) != "(" && !OperationOrder.Contains(Formula.Substring(lastOpenPhrantesisIndex - 1, 1)))
                        { AppendAsterix = true; }
                    }

                    Formula = Formula.Substring(0, lastOpenPhrantesisIndex) + (AppendAsterix ? "*" : "") + result.ToString() + Formula.Substring(firstClosePhrantesisIndexAfterLastOpened + 1);
                }
                resultado = ProcessOperation(Formula);
            }
            catch (Exception) { resultado = -99999999; }

            return resultado;
        }

        private decimal ProcessOperation(string operation)
        {
            ArrayList arr = new ArrayList();
            string s = "";
            for (int i = 0; i < operation.Length; i++)
            {
                string currentCharacter = operation.Substring(i, 1);
                if (OperationOrder.IndexOf(currentCharacter) > -1)
                {
                    if (s != "")
                    {
                        arr.Add(s);
                    }
                    arr.Add(currentCharacter);
                    s = "";
                }
                else { s += currentCharacter; }
            }
            arr.Add(s);
            s = "";
            foreach (string op in OperationOrder)
            {
                while (arr.IndexOf(op) > -1)
                {
                    int operatorIndex = arr.IndexOf(op);
                    decimal digitBeforeOperator = Convert.ToDecimal(arr[operatorIndex - 1]);
                    decimal digitAfterOperator = 0;

                    if (arr[operatorIndex + 1].ToString() == "-")
                    {
                        arr.RemoveAt(operatorIndex + 1);
                        digitAfterOperator = Convert.ToDecimal(arr[operatorIndex + 1]) * -1;
                    }
                    else
                    { digitAfterOperator = Convert.ToDecimal(arr[operatorIndex + 1]); }

                    arr[operatorIndex] = CalculateByOperator(digitBeforeOperator, digitAfterOperator, op);
                    arr.RemoveAt(operatorIndex - 1);
                    arr.RemoveAt(operatorIndex);
                }
            }
            return Convert.ToDecimal(arr[0]);
        }

        private decimal CalculateByOperator(decimal number1, decimal number2, string op)
        {
            if (op == "/") { return number1 / number2; }
            else if (op == "*") { return number1 * number2; }
            else if (op == "-") { return number1 - number2; }
            else if (op == "+") { return number1 + number2; }
            else { return 0; }
        }
    }
}