using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bot_Zhrp1234
{
    class OpeIO
    {
        string ErrNum1 = "La linea es invalida";
        public string errNum1
        {
            get { return ErrNum1; }
            set { ErrNum1 = value; }
        }
        string ErrNum2 = "No se pudo encontrar la palabra";
        public string errNum2
        {
            get { return ErrNum2; }
            set { ErrNum2 = value; }
        }
        string file;

        public OpeIO(string rnkFile)
        {
            file = rnkFile;
        }

        public void WriteNWL(string text)
        {
            StreamReader rnkk = new StreamReader(file);
            string temp = rnkk.ReadToEnd();
            rnkk.Close();
            StreamWriter rnk = new StreamWriter(file);
            if (temp == "")
            {
                rnk.Write(text);
            }
            else
            {
                rnk.Write(temp);
                rnk.WriteLine();
                rnk.Write(text);
            }
            rnk.Close();
        }

        public void WriteN(string text)
        {
            StreamReader rnkk = new StreamReader(file);
            string temp = rnkk.ReadToEnd();
            rnkk.Close();
            StreamWriter rnk = new StreamWriter(file);
            rnk.Write(temp);
            rnk.Write(text);
            rnk.Close();
        }

        public void Write(string text)
        {
            StreamWriter rnk = new StreamWriter(file);
            rnk.Write(text);
            rnk.Close();
        }

        public string ReadAll()
        {
            StreamReader rnk = new StreamReader(file);
            string text = rnk.ReadToEnd();
            rnk.Close();
            return text;
        }

        public string ReadFirstLine()
        {
            StreamReader rnk = new StreamReader(file);
            string text = rnk.ReadLine();
            rnk.Close();
            return text;
        }

        public string ReadLineByNum(int num)
        {
            string text = "";
            StreamReader rnk = new StreamReader(file);
            for (int i = 1; i <= num; i++)
            {
                text = rnk.ReadLine();
                if (text == null)
                {
                    rnk.Close();
                    return ErrNum1;
                }
            }
            rnk.Close();
            return text;
        }

        public string ReadLineByWord(string word)
        {
            string text = " ";
            StreamReader rnk = new StreamReader(file);
            while (text != null)
            {
                text = rnk.ReadLine();
                if (text == null)
                {
                    rnk.Close();
                    return ErrNum2;
                }
                else
                {
                    if (text.Contains(word) == true)
                    {
                        rnk.Close();
                        return text;
                    }
                }
            }
            rnk.Close();
            return text;
        }

        public string ReadLineByWordWUL(string word)
        {
            string text = " ";
            StreamReader rnk = new StreamReader(file);
            while (text != null)
            {
                text = rnk.ReadLine();
                if (text == null)
                {
                    rnk.Close();
                    return ErrNum2;
                }
                else
                {
                    if (text.ToUpper().Contains(word.ToUpper()) == true)
                    {
                        rnk.Close();
                        return text;
                    }
                }
            }
            rnk.Close();
            return text;
        }

        public string ReadAllLinesByWordWUL(string word)
        {
            string text = "";
            string textM = " ";
            StreamReader rnk = new StreamReader(file);
            while (textM != null)
            {
                textM = rnk.ReadLine();
                if (textM == null && text == "")
                {
                    rnk.Close();
                    return ErrNum2;
                }
                else if (textM != null)
                {
                    if (textM.ToUpper().Contains(word.ToUpper()) == true)
                    {
                        if (text != "")
                        {
                            text += ";";
                            text += textM;
                        }
                        else
                        {
                            text += textM;
                        }
                    }
                }
            }
            rnk.Close();
            return text;
        }

        public string ReadAllLinesByWord(string word)
        {
            string text = "";
            string textM = " ";
            StreamReader rnk = new StreamReader(file);
            while (textM != null)
            {
                textM = rnk.ReadLine();
                if (textM == null && text == "")
                {
                    rnk.Close();
                    return ErrNum2;
                }
                else if (textM != null)
                {
                    if (textM.Contains(word) == true)
                    {
                        if (text != "")
                        {
                            text += ";";
                            text += textM;
                        }
                        else
                        {
                            text += textM;
                        }
                    }
                }
            }
            rnk.Close();
            return text;
        }
    }
}
