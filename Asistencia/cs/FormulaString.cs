using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CaniaBrava
{
    class FormulaString
    {
        public static double Check_Parenthesis(string strOrigin)
        {
            if (strOrigin.Length < 1)
            {
                return 0;
            }

            int i = 0, j = 0;
            int strLen = strOrigin.Length;
            int startPos = 0, EndPos = 0;
            string startStr = "", endStr = "";

            for (i = 0; i < strLen; i++)
            {
                string gbn1 = strOrigin.Substring(i, 1);
                if (gbn1 == "(")
                {
                    startPos = i;
                    if (i == 0)
                    {
                        startStr = "";
                    }
                    else
                    {
                        startStr = strOrigin.Substring(0, i);
                    }

                    for (j = i + 1; j <= strLen; j++)
                    {
                        string gbn2 = strOrigin.Substring(j, 1);
                        if (gbn2 == ")")
                        {
                            EndPos = j;
                            if (j == strLen - 1)
                            {
                                endStr = "";
                            }
                            else
                            {
                                endStr = strOrigin.Substring(j + 1, strLen - j - 1);
                            }

                            string tempCalc = strOrigin.Substring(i + 1, j - i - 1);
                            strOrigin = startStr + Calc_String(tempCalc).ToString() + endStr;
                            strLen = strOrigin.Length;
                            i = -1;
                            break;
                        }
                        if (gbn2 == "(")
                        {
                            startStr = strOrigin.Substring(0, j);
                            strOrigin = strOrigin.Substring(j + 1, strLen - j - 1);
                            strLen = strOrigin.Length;
                            j = -1;
                            i = -1;
                        }
                    }
                }
            }

            strOrigin = Calc_String(strOrigin).ToString();

            return Convert.ToDouble(strOrigin);

        }

        public static double Calc_String(string strOrigin)
        {

            if (strOrigin.Length < 1)
            {
                return 0;
            }


            string tempOperation, tempStr, tempStr1 = "", tempStr2 = "";
            double tempValue1 = 0, tempValue2 = 0, tempResult = 0;


            int strLen = strOrigin.Length;

            for (int i = 0; i < strLen; i++)
            {
                tempOperation = strOrigin.Substring(i, 1);
                if (tempOperation == "*" | tempOperation == "/" | tempOperation == "^")
                {
                    bool firstCheck = false;
                    for (int j = i - 1; j >= 0; j--)
                    {
                        tempStr = strOrigin.Substring(j, 1);
                        if (tempStr == "*" | tempStr == "/" | tempStr == "+" | tempStr == "-" | tempStr == "^")
                        {
                            tempValue1 = Convert.ToDouble(strOrigin.Substring(j + 1, i - j - 1).Trim());
                            tempStr1 = strOrigin.Substring(0, j + 1);
                            firstCheck = true;
                            break;
                        }
                        if (j == 0 & firstCheck == false)
                        {
                            tempValue1 = Convert.ToDouble(strOrigin.Substring(0, i).Trim());
                        }
                    }

                    bool lastCheck = false;
                    for (int j = i + 1; j < strLen; j++)
                    {
                        tempStr = strOrigin.Substring(j, 1);
                        if (tempStr == "*" | tempStr == "/" | tempStr == "+" | tempStr == "-" | tempStr == "^")
                        {
                            tempValue2 = Convert.ToDouble(strOrigin.Substring(i + 1, j - i - 1).Trim());
                            tempStr2 = strOrigin.Substring(j, strLen - j);
                            lastCheck = true;
                            break;
                        }
                        if (j == strLen - 1 & lastCheck == false)
                        {
                            tempValue2 = Convert.ToDouble(strOrigin.Substring(i + 1, strLen - i - 1).Trim());
                            tempStr2 = "";
                        }
                    }

                    if (tempOperation == "*")
                    {
                        tempResult = tempValue1 * tempValue2;
                    }

                    if (tempOperation == "/")
                    {
                        tempResult = tempValue1 / tempValue2;
                    }

                    if (tempOperation == "^")
                    {
                        tempResult = Math.Pow(tempValue1, tempValue2);
                    }

                    strOrigin = tempStr1 + tempResult.ToString() + tempStr2;
                    i = -1;
                    strLen = strOrigin.Length;
                }

            }


            strLen = strOrigin.Length;

            for (int i = 0; i < strLen; i++)
            {
                tempOperation = strOrigin.Substring(i, 1);
                if (tempOperation == "+" | tempOperation == "-")
                {
                    bool firstCheck = false;
                    for (int j = i - 1; j >= 0; j--)
                    {
                        tempStr = strOrigin.Substring(j, 1);
                        if (tempStr == "*" | tempStr == "/" | tempStr == "+" | tempStr == "-" | tempStr == "^")
                        {
                            tempValue1 = Convert.ToDouble(strOrigin.Substring(j + 1, i - j - 1).Trim());
                            tempStr1 = strOrigin.Substring(0, j + 1);
                            firstCheck = true;
                            break;
                        }
                        if (j == 0 & firstCheck == false)
                        {
                            tempValue1 = Convert.ToDouble(strOrigin.Substring(0, i).Trim());
                            tempStr1 = "";
                        }
                    }

                    bool lastCheck = false;
                    for (int j = i + 1; j < strLen; j++)
                    {
                        tempStr = strOrigin.Substring(j, 1);
                        if (tempStr == "*" | tempStr == "/" | tempStr == "+" | tempStr == "-" | tempStr == "^")
                        {
                            tempValue2 = Convert.ToDouble(strOrigin.Substring(i + 1, j - i - 1).Trim());
                            tempStr2 = strOrigin.Substring(j, strLen - j);
                            lastCheck = true;
                            break;
                        }
                        if (j == strLen - 1 & lastCheck == false)
                        {
                            tempValue2 = Convert.ToDouble(strOrigin.Substring(i + 1, strLen - i - 1).Trim());
                            tempStr2 = "";
                        }
                    }

                    if (tempOperation == "+")
                    {
                        tempResult = tempValue1 + tempValue2;
                    }

                    if (tempOperation == "-")
                    {
                        tempResult = tempValue1 - tempValue2;
                    }

                    strOrigin = tempStr1 + tempResult.ToString() + tempStr2;
                    i = -1;
                    strLen = strOrigin.Length;
                }

            }

            return Convert.ToDouble(strOrigin);

        }
    }
}