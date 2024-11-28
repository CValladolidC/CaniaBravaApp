using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Bot_GR55
{
    class Program
    {
        private static Process p;

        private static void Presionar(int tiempo, string tecla)
        {
            Thread.Sleep(tiempo * 1000);
            SendKeys.SendWait(tecla);
        }

        static void Main(string[] args)
        {
            if (p == null || p.HasExited)
            {
                DataTable data = new DataTable();
                string query = string.Empty;
                try
                {
                    SqlConnection conexion;
                    SqlCommand myCommand;
                    String Usua = string.Empty, Pass = string.Empty, Ruta = string.Empty, Arc1 = string.Empty, Arc2 = string.Empty, Prog = string.Empty, Serv = string.Empty, Clie = string.Empty, Mate = string.Empty, Pedi = string.Empty, Stoc = string.Empty, Material = string.Empty, Clientes = string.Empty, Mess = string.Empty;
                    FileInfo fa = new FileInfo(AppDomain.CurrentDomain.BaseDirectory + "/directorio.txt");
                    if (fa.Exists)
                    {
                        OpeIO opeIO = new OpeIO(AppDomain.CurrentDomain.BaseDirectory + "/directorio.txt");
                        Usua = opeIO.ReadLineByNum(1);
                        Pass = opeIO.ReadLineByNum(2);
                        Ruta = opeIO.ReadLineByNum(3);
                        Arc1 = opeIO.ReadLineByNum(4);  
                        Arc2 = opeIO.ReadLineByNum(5);
                        Prog = opeIO.ReadLineByNum(6);
                        Serv = opeIO.ReadLineByNum(7);
                        Clie = opeIO.ReadLineByNum(8);
                        Mate = opeIO.ReadLineByNum(9);
                        Pedi = opeIO.ReadLineByNum(10);
                        Stoc = opeIO.ReadLineByNum(11);
                        Material = opeIO.ReadLineByNum(12);
                        Clientes = opeIO.ReadLineByNum(13);
                        Mess= opeIO.ReadLineByNum(14);
                    }
                    string connString = @"Data Source=" + Serv + ";Database=Asistencia;uid=ctcuser;pwd=ctcuser;";

                    if (DateTime.Now.Day > 1)
                    {
                        // Start the child process.
                        // Redirect the output stream of the child process.
                        p = new Process();
                        p.StartInfo.UseShellExecute = false;
                        p.StartInfo.RedirectStandardOutput = true;
                        p.StartInfo.FileName = Prog;
                        p.Start();

                        Presionar(16, "{ENTER}");//OPEN SAP LOGIN

                        Presionar(5, Usua); //ASSESSMENT
                        Presionar(3, "{TAB}"); //$5Sistemas
                        Presionar(5, Pass); //$5Sistemas

                        Presionar(3, "{ENTER}");

                        //Presionar(3, "GR55");
                        //Presionar(3, "{ENTER}");

                        #region Materiales de tabla MAESGEN
                        //CARGAR MAESTRO DE MATERIALES EN TXT
                        Console.WriteLine("Iniciamos Extracciòn Maestro de materiales de tabla MAESGEN");
                        data = new DataTable();
                        SqlDataAdapter da = new SqlDataAdapter();
                        query = "SELECT clavemaesgen FROM maesgen WHERE idmaesgen = '186';";
                        conexion = new SqlConnection();
                        conexion.ConnectionString = connString;
                        conexion.Open();
                        da.SelectCommand = new SqlCommand(query, conexion);
                        da.Fill(data);

                        fa = new FileInfo("D:/" + Material);
                        if (fa.Exists)
                        {
                            File.Delete("D:/" + Material);
                        }
                        string filename = "D:/" + Material;
                        StreamWriter archivo_ide = File.CreateText(filename);
                        archivo_ide.Close();

                        foreach (DataRow row in data.Rows)
                        {
                            OpeIO opeIO = new OpeIO(filename);
                            opeIO.WriteNWL("*" + row["clavemaesgen"].ToString() + "*");
                        }
                        #endregion

                        #region PROCESO DE AZUCAR
                        Console.WriteLine("TX AC21");
                        Presionar(4, "AC21");//AZUCAR AGRICOLA
                        Presionar(3, "{ENTER}");
                        Presionar(10, "{F8}");
                        //Presionar(6, "{TAB}");
                        //Presionar(8, Mess);//CODIGO TEMPORAL PARA LOS MESES
                        Presionar(15, "{F8}");

                        Presionar(600, "+{TAB}");//10 minutos
                        Console.WriteLine("Tecla Ctrl+Tab");
                        Presionar(5, "+{TAB}");
                        for (int i = 0; i < 13; i++)
                        {
                            Presionar(1, "{DOWN}");
                        }
                        Presionar(5, "{F2}");

                        Presionar(10, "^{f}");
                        Console.WriteLine("Tecla Ctrl + F");
                        Presionar(5, "VENTA PRODUCTOS");//VENTA DE BUSQUEDA
                        Presionar(3, "{ENTER}");
                        Presionar(5, "{TAB}");
                        Console.WriteLine("Tecla Ctrl + Tab");
                        Presionar(1, "+{TAB}");
                        for (int i = 0; i < 16; i++)
                        {
                            Presionar(1, "{LEFT}");
                        }
                        Presionar(5, "{F2}");
                        Presionar(5, "{F2}");
                        Presionar(3, "{ENTER}");

                        //Presionar(600, "{DOWN}");//10 min 
                        Console.WriteLine("Tecla Ctrl + F9");
                        Presionar(1380, "^{F9}");//20 min 
                        Presionar(5, "1");
                        Presionar(3, "{DOWN}");
                        Presionar(3, "{DOWN}");
                        Presionar(3, "{ENTER}");
                        Presionar(3, "{TAB}");
                        Presionar(3, "{ENTER}");

                        #region FILTRAR COLUMNA Material
                        Presionar(10, "^+{F2}");//Ctrl+Shit+F2
                        Presionar(3, "+{TAB}");
                        for (int i = 0; i < 6; i++)
                        {
                            Presionar(1, "+{TAB}");
                        }
                        Presionar(3, "{TAB}");
                        Presionar(3, "+{F10}");
                        Presionar(3, "{DOWN}");
                        Presionar(3, "{ENTER}");
                        Presionar(3, "+{TAB}");
                        Presionar(3, "Material");
                        Presionar(3, "{ENTER}");
                        Presionar(3, "{F12}");
                        Presionar(3, "{F2}");
                        Presionar(3, "^{TAB}");//Ctrl+Tab
                        Presionar(3, "{TAB}");
                        Presionar(3, "{TAB}");
                        Presionar(3, "{ENTER}");
                        Presionar(3, "{TAB}");
                        Presionar(3, "{TAB}");
                        Presionar(3, "{ENTER}");
                        Presionar(5, "+{F4}");
                        Presionar(3, "+{F11}");
                        Presionar(3, "{ENTER}");
                        Presionar(3, "D:");
                        Presionar(3, "{ENTER}");
                        Presionar(5, Material);
                        Presionar(3, "{ENTER}");
                        Presionar(5, "+{TAB}");
                        Presionar(3, "{ENTER}");
                        Presionar(3, "{F8}");
                        Presionar(5, "{ENTER}");
                        #endregion

                        Console.WriteLine("Tecla F9");
                        Presionar(10, "{F9}");
                        Presionar(3, "{TAB}");
                        Presionar(3, "{ENTER}");
                        Presionar(10, "+{TAB}");
                        Presionar(3, Ruta);//D:/
                        Presionar(3, "{TAB}");
                        Presionar(3, Arc1); //Azucar.txt
                        Presionar(3, "{TAB}");
                        Presionar(3, "{TAB}");
                        Presionar(3, "{ENTER}");
                        Presionar(3, "+{TAB}");
                        Presionar(1, "{ENTER}");
                        Presionar(15, "{ESC}");
                        Presionar(5, "{ESC}");
                        Presionar(5, "{ENTER}");
                        Presionar(5, "{ESC}");
                        Presionar(20, "{ESC}");

                        Console.WriteLine("Iniciamos Extracciòn de Azucar");
                        FileInfo fab = new FileInfo(Ruta + Arc1);
                        data = new DataTable();
                        if (fab.Exists)
                        {

                            string[] stringarry = System.IO.File.ReadAllLines(Ruta + Arc1, Encoding.Default);

                            if (stringarry.Length > 0)
                            {
                                int cc = 0;
                                var arr_ = stringarry[9].Substring(1, stringarry[9].Length - 2).Split('|');
                                List<string> LColumns = new List<string>();
                                for (int i = 0; i < arr_.Length; i++)
                                {
                                    if (arr_[i].Trim().Length > 0)
                                    {
                                        data.Columns.Add("Col" + cc, typeof(string));
                                        cc++;
                                    }
                                    else
                                    {
                                        data.Columns.Add("Vacio" + i, typeof(string));
                                        LColumns.Add("Vacio" + i);
                                    }
                                }
                                data.AcceptChanges();

                                System.Data.DataRow dr = null;
                                for (int i = 11; i < stringarry.Length - 3; i++)
                                {
                                    stringarry[i] = stringarry[i].Replace("S/N|", "S/N");
                                    var dat = stringarry[i].Substring(1, stringarry[i].Length - 2).Split('|');
                                    dr = data.NewRow();
                                    for (int y = 0; y < dat.Length; y++)
                                    {
                                        dr[y] = dat[y].Trim();
                                    }
                                    data.Rows.Add(dr);
                                }
                                data.AcceptChanges();
                                //data.Columns.Remove("Col27");
                                //data.AcceptChanges();

                                query = "DELETE FROM PlanMo_New.dbo.VENTAS_GR55 WHERE Anio=YEAR(GETDATE()) AND Periodo=MONTH(GETDATE()) AND Soc='153';\r\n";
                                int cccc = 0;
                                foreach (DataRow item in data.Rows)
                                {
                                    if (item.ItemArray[1].ToString() != string.Empty)
                                    {
                                        query += "INSERT INTO PlanMo_New.dbo.VENTAS_GR55 VALUES (";
                                        for (int i = 0; i < item.ItemArray.Length; i++)
                                        {
                                            if (i == 11 || i == 12)
                                            {
                                                if (item.ItemArray[i].ToString().Trim() != string.Empty)
                                                {
                                                    query += "'" + DateTime.Parse(item.ItemArray[i].ToString().Replace(".", "/")).ToString("yyyy-MM-dd") + "',";
                                                }
                                                else { query += "'',"; }
                                            }
                                            else
                                            {
                                                if (i == 10 || i == 15 || i == 17)
                                                {
                                                    if (item.ItemArray[i].ToString().Trim() != string.Empty)
                                                    {
                                                        string val = item.ItemArray[i].ToString().Replace(",", "");
                                                        string dat = val.Contains("-") ? val.Substring(0, val.Length - 1) : "-" + val;
                                                        query += "'" + dat + "',";
                                                    }
                                                    else { query += "'0',"; }
                                                }
                                                else
                                                {
                                                    if (item.ItemArray[i].ToString().Trim() != string.Empty)
                                                    {
                                                        query += "'" + item.ItemArray[i].ToString() + "',";
                                                    }
                                                    else { query += "'',"; }
                                                }
                                            }
                                        }
                                        query += ");\r\n";
                                    }
                                    query = query.Replace(",);", ");");
                                    cccc++;
                                    Console.WriteLine("Leyendo Informacion: " + cccc + " rows");
                                }

                                conexion = new SqlConnection();
                                conexion.ConnectionString = connString;
                                conexion.Open();

                                myCommand = new SqlCommand(query, conexion);
                                myCommand.CommandTimeout = 360;
                                myCommand.ExecuteNonQuery();
                                myCommand.Dispose();
                                conexion.Close();
                            }
                        }


                        #endregion

                        #region PROCESO DE ALCOHOL
                        Console.WriteLine("TX SC21");
                        Presionar(4, "GR55");
                        Presionar(3, "{ENTER}");
                        Presionar(5, "SC21");//ALCOHOL SUCRO
                        Presionar(3, "{ENTER}");
                        Presionar(20, "{F8}");
                        //Presionar(6, "{TAB}");
                        //Presionar(8, Mess);//CODIGO TEMPORAL PARA LOS MESES
                        Presionar(30, "{F8}");

                        Presionar(3, "{ENTER}");
                        Presionar(50, "^+{F1}");
                        Presionar(2, "+{TAB}");
                        Presionar(3, "{ENTER}");
                        Presionar(4, "G y P Mes Acumulado");
                        Presionar(2, "+{TAB}");
                        Presionar(2, "+{TAB}");
                        Presionar(2, "{ENTER}");
                        Presionar(2, "{DOWN}");
                        Presionar(5, "{F2}");
                        Presionar(7, "{F2}");
                        //Presionar(60, "+{TAB}");//15 segundos ----





                        //Presionar(1, "+{TAB}"); //----
                        //for (int i = 0; i < 13; i++) //----
                        //{ //----
                        //    Presionar(1, "{DOWN}"); //----
                        //} //----





                        //Presionar(5, "{F2}");

                        Presionar(60, "^{f}");
                        Presionar(5, "VENTA PRODUCTOS");//VENTA DE BUSQUEDA
                        Presionar(3, "{ENTER}");
                        Presionar(2, "{TAB}");
                        Presionar(2, "+{TAB}");
                        Presionar(2, "{LEFT}");
                        Presionar(2, "{LEFT}");
                        Presionar(5, "{F2}");
                        Presionar(5, "{F2}");
                        Presionar(3, "{ENTER}");

                        Presionar(50, "^{F9}");
                        Presionar(5, "1");
                        Presionar(2, "{DOWN}");
                        Presionar(2, "{DOWN}");
                        Presionar(2, "{ENTER}");
                        Presionar(2, "{TAB}");
                        Presionar(2, "{ENTER}");

                        #region FILTRAR COLUMNA Material
                        Presionar(20, "^+{F2}");//Ctrl+Shit+F2
                        Presionar(3, "+{TAB}");
                        for (int i = 0; i < 6; i++)
                        {
                            Presionar(1, "+{TAB}");
                        }
                        Presionar(2, "{TAB}");
                        Presionar(2, "+{F10}");
                        Presionar(2, "{DOWN}");
                        Presionar(2, "{ENTER}");
                        Presionar(2, "+{TAB}");
                        Presionar(2, "Material");
                        Presionar(2, "{ENTER}");
                        Presionar(2, "{F12}");
                        Presionar(3, "{F2}");
                        Presionar(2, "^{TAB}");//Ctrl+Tab
                        Presionar(2, "{TAB}");
                        Presionar(2, "{TAB}");
                        Presionar(2, "{ENTER}");
                        Presionar(2, "{TAB}");
                        Presionar(2, "{TAB}");
                        Presionar(2, "{ENTER}");
                        Presionar(5, "+{F4}");
                        Presionar(3, "+{F11}");
                        Presionar(1, "{ENTER}");
                        Presionar(2, "D:");
                        Presionar(2, "{ENTER}");
                        Presionar(5, Material);
                        Presionar(1, "{ENTER}");
                        Presionar(5, "+{TAB}");
                        Presionar(2, "{ENTER}");
                        Presionar(3, "{F8}");
                        Presionar(5, "{ENTER}");
                        #endregion

                        Presionar(10, "{F9}");
                        Presionar(3, "{TAB}");
                        Presionar(2, "{ENTER}");
                        Presionar(10, "+{TAB}");
                        Presionar(3, Ruta);//D:/
                        Presionar(3, "{TAB}");
                        Presionar(3, Arc2); //Alcohol.txt
                        Presionar(2, "{TAB}");
                        Presionar(2, "{TAB}");
                        Presionar(2, "{ENTER}");
                        Presionar(4, "+{TAB}");
                        Presionar(1, "{ENTER}");

                        Presionar(15, "{ESC}");
                        Presionar(5, "{ESC}");
                        Presionar(2, "{ENTER}");
                        Presionar(3, "{ESC}");
                        Presionar(5, "{ESC}");
                        Presionar(5, "{ESC}");

                        Console.WriteLine("Iniciamos Extracciòn de Alcohol");
                        FileInfo fab2= new FileInfo(Ruta + Arc2);
                        data = new DataTable();
                        if (fab2.Exists)
                        {
                            string[] stringarry = System.IO.File.ReadAllLines(Ruta + Arc2, Encoding.Default);

                            if (stringarry.Length > 0)
                            {
                                int cc = 0;
                                var arr_ = stringarry[9].Substring(1, stringarry[9].Length - 2).Split('|');
                                List<string> LColumns = new List<string>();
                                for (int i = 0; i < arr_.Length; i++)
                                {
                                    if (arr_[i].Trim().Length > 0)
                                    {
                                        data.Columns.Add("Col" + cc, typeof(string));
                                        cc++;
                                    }
                                    else
                                    {
                                        data.Columns.Add("Vacio" + i, typeof(string));
                                        LColumns.Add("Vacio" + i);
                                    }
                                }
                                data.AcceptChanges();

                                System.Data.DataRow dr = null;
                                for (int i = 11; i < stringarry.Length - 3; i++)
                                {
                                    stringarry[i] = stringarry[i].Replace("S/N|", "S/N");
                                    var dat = stringarry[i].Substring(1, stringarry[i].Length - 2).Split('|');
                                    dr = data.NewRow();
                                    for (int y = 0; y < dat.Length; y++)
                                    {
                                        dr[y] = dat[y].Trim();
                                    }
                                    data.Rows.Add(dr);
                                }
                                data.AcceptChanges();
                                //data.Columns.Remove("Col27");
                                //data.AcceptChanges();

                                query = "DELETE FROM PlanMo_New.dbo.VENTAS_GR55 WHERE Anio=YEAR(GETDATE()) AND Periodo=MONTH(GETDATE()) AND Soc='157';\r\n";
                                int cccc = 0;
                                foreach (DataRow item in data.Rows)
                                {
                                    if (item.ItemArray[1].ToString() != string.Empty)
                                    {
                                        query += "INSERT INTO PlanMo_New.dbo.VENTAS_GR55 VALUES (";
                                        for (int i = 0; i < item.ItemArray.Length; i++)
                                        {
                                            if (i == 11 || i == 12)
                                            {
                                                if (item.ItemArray[i].ToString().Trim() != string.Empty)
                                                {
                                                    query += "'" + DateTime.Parse(item.ItemArray[i].ToString().Replace(".", "/")).ToString("yyyy-MM-dd") + "',";
                                                }
                                                else { query += "'',"; }
                                            }
                                            else
                                            {
                                                if (i == 10 || i == 15 || i == 17)
                                                {
                                                    if (item.ItemArray[i].ToString().Trim() != string.Empty)
                                                    {
                                                        string val = item.ItemArray[i].ToString().Replace(",", "");
                                                        string dat = val.Contains("-") ? val.Substring(0, val.Length - 1) : "-" + val;
                                                        query += "'" + dat + "',";
                                                    }
                                                    else { query += "'0',"; }
                                                }
                                                else
                                                {
                                                    if (item.ItemArray[i].ToString().Trim() != string.Empty)
                                                    {
                                                        query += "'" + item.ItemArray[i].ToString() + "',";
                                                    }
                                                    else { query += "'',"; }
                                                }
                                            }
                                        }
                                        query += ");\r\n";
                                    }
                                    query = query.Replace(",);", ");");
                                    cccc++;
                                    Console.WriteLine("Leyendo Informacion VENTAS_GR55: " + cccc + " rows");
                                }

                                conexion = new SqlConnection();
                                conexion.ConnectionString = connString;
                                conexion.Open();

                                myCommand = new SqlCommand(query, conexion);
                                myCommand.CommandTimeout = 360;
                                myCommand.ExecuteNonQuery();
                                myCommand.Dispose();
                                conexion.Close();
                            }
                        }


                        #endregion

                        #region PROCESO DE GUARDAR CLIENTES Y MATERIALES
                        //CLIENTES
                        Console.WriteLine("Iniciamos Extracciòn Clientes de GR55");
                        data = new DataTable();
                        da = new SqlDataAdapter();
                        query = "SELECT Cliente FROM PlanMo_New.dbo.VENTAS_GR55 GROUP BY Cliente;";
                        conexion = new SqlConnection();
                        conexion.ConnectionString = connString;
                        conexion.Open();
                        da.SelectCommand = new SqlCommand(query, conexion);
                        da.Fill(data);

                        fa = new FileInfo("D:/" + Clie);
                        if (fa.Exists)
                        {
                            File.Delete("D:/" + Clie);
                        }
                        filename = "D:/" + Clie;
                        archivo_ide = File.CreateText(filename);
                        archivo_ide.Close();

                        foreach (DataRow row in data.Rows)
                        {
                            OpeIO opeIO = new OpeIO(filename);
                            opeIO.WriteNWL(row["Cliente"].ToString());
                        }

                        //MATERIALES
                        //Console.WriteLine("Iniciamos Extracciòn Materiales de GR55");
                        //data = new DataTable();
                        //da = new SqlDataAdapter();
                        //query = "SELECT Material FROM PlanMo_New.dbo.VENTAS_GR55 GROUP BY Material;";
                        //conexion = new SqlConnection();
                        //conexion.ConnectionString = connString;
                        //conexion.Open();
                        //da.SelectCommand = new SqlCommand(query, conexion);
                        //da.Fill(data);

                        //fa = new FileInfo("D:/" + Mate);
                        //if (fa.Exists)
                        //{
                        //    File.Delete("D:/" + Mate);
                        //}
                        //filename = "D:/" + Mate;
                        //archivo_ide = File.CreateText(filename);
                        //archivo_ide.Close();

                        //foreach (DataRow row in data.Rows)
                        //{
                        //    OpeIO opeIO = new OpeIO(filename);
                        //    opeIO.WriteNWL(row["Material"].ToString());
                        //}

                        #endregion

                        #region EXTRACCION DE CLIENTES
                        Presionar(10, "XD03");//TX para obtener descripcion de clientes 
                        Presionar(3, "{ENTER}");
                        Presionar(2, "{F4}");
                        Presionar(2, "{DOWN}");
                        Presionar(2, "{DOWN}");
                        Presionar(2, "{DOWN}");
                        Presionar(2, "{DOWN}");
                        Presionar(2, "{TAB}");
                        Presionar(2, "{ENTER}");
                        Presionar(5, "+{F11}");
                        Presionar(5, "{ENTER}");
                        Presionar(2, "D:");
                        Presionar(2, "{ENTER}");
                        Presionar(5, Clie);
                        Presionar(2, "{ENTER}");
                        Presionar(5, "+{TAB}");
                        Presionar(1, "{ENTER}");
                        Presionar(5, "{F8}");
                        Presionar(5, "{ENTER}");
                        Presionar(5, "+{F2}");
                        Presionar(2, "{TAB}");
                        Presionar(2, "{ENTER}");

                        Presionar(5, "+{TAB}");
                        //Presionar(1, "{ENTER}");
                        Presionar(5, Ruta);//D:/
                        Presionar(2, "{TAB}");
                        Presionar(2, Clientes); //Clientes.txt
                        Presionar(2, "{TAB}");
                        Presionar(2, "{TAB}");
                        Presionar(2, "{ENTER}");
                        Presionar(2, "+{TAB}");
                        Presionar(2, "{ENTER}");
                        Presionar(2, "{ESC}");
                        Presionar(2, "{ESC}");

                        Console.WriteLine("Iniciamos Actualizacion de BD Clientes");
                        fa = new FileInfo(Ruta + Clientes);
                        data = new DataTable();
                        if (fa.Exists)
                        {
                            string[] stringarry = System.IO.File.ReadAllLines(Ruta + Clientes, Encoding.Default);

                            if (stringarry.Length > 0)
                            {
                                int cc = 0;
                                var arr_ = stringarry[1].Substring(1, stringarry[1].Length - 2).Split('|');
                                List<string> LColumns = new List<string>();
                                for (int i = 0; i < arr_.Length; i++)
                                {
                                    if (arr_[i].Trim().Length > 0)
                                    {
                                        data.Columns.Add("Col" + cc, typeof(string));
                                        cc++;
                                    }
                                    else
                                    {
                                        data.Columns.Add("Vacio" + i, typeof(string));
                                        LColumns.Add("Vacio" + i);
                                    }
                                }
                                data.AcceptChanges();

                                System.Data.DataRow dr = null;
                                for (int i = 3; i < stringarry.Length - 1; i++)
                                {
                                    stringarry[i] = stringarry[i].Replace("S/N|", "S/N");
                                    var dat = stringarry[i].Substring(1, stringarry[i].Length - 2).Split('|');
                                    dr = data.NewRow();
                                    for (int y = 0; y < dat.Length; y++)
                                    {
                                        dr[y] = dat[y].Trim();
                                    }
                                    data.Rows.Add(dr);
                                }
                                data.AcceptChanges();

                                data.Columns.Remove("Col0");
                                data.Columns.Remove("Col1");
                                data.AcceptChanges();

                                query = "DELETE FROM PlanMo_New.dbo.VENTAS_GR55_CLIENTES;\r\n";
                                int cccc = 0;
                                foreach (DataRow item in data.Rows)
                                {
                                    query += "INSERT INTO PlanMo_New.dbo.VENTAS_GR55_CLIENTES (POBLACION,NOMBRE,CLIENTE) VALUES (";
                                    for (int i = 0; i < item.ItemArray.Length; i++)
                                    {
                                        query += "'" + item.ItemArray[i].ToString().Replace("'", "") + "',";
                                    }
                                    query += ");\r\n";
                                    cccc++;
                                    Console.WriteLine("Leyendo Informacion VENTAS_GR55_CLIENTES: " + cccc + " rows");
                                }

                                query = query.Replace(",);", ");");

                                conexion = new SqlConnection();
                                conexion.ConnectionString = connString;
                                conexion.Open();

                                myCommand = new SqlCommand(query, conexion);
                                myCommand.CommandTimeout = 360;
                                myCommand.ExecuteNonQuery();
                                myCommand.Dispose();
                                conexion.Close();
                            }
                        }
                        #endregion

                        #region "Extraccion de Stock de ventas"
                        Presionar(7, "MB52");//TX para obtener descripcion de materiales 
                        Presionar(3, "{ENTER}");
                        Presionar(3, "+{F5}");
                        Presionar(3, "{F8}");
                        Presionar(2, "{TAB}");
                        Presionar(2, "{TAB}");
                        Presionar(2, "{ENTER}");

                        Presionar(5, "+{F4}");
                        Presionar(5, "+{F11}");
                        Presionar(3, "{ENTER}");
                        Presionar(3, "D:");
                        Presionar(3, "{ENTER}");
                        Presionar(6, Material);//maesmateriales.txt
                        Presionar(3, "{ENTER}");
                        Presionar(5, "+{TAB}");
                        Presionar(2, "{ENTER}");
                        Presionar(3, "{F8}");
                        Presionar(5, "{F8}");

                        Presionar(10, "^+{F9}");
                        Presionar(3, "{TAB}");
                        Presionar(2, "{ENTER}");
                        Presionar(5, "+{TAB}");
                        //Presionar(2, "{ENTER}");
                        Presionar(5, Ruta);//D:/
                        Presionar(3, "{TAB}");
                        Presionar(3, Stoc); //Stock.txt
                        Presionar(3, "{TAB}");
                        Presionar(2, "{TAB}");
                        Presionar(2, "{ENTER}");
                        Presionar(2, "+{TAB}");
                        Presionar(2, "{ENTER}");
                        Presionar(2, "{ESC}");
                        Presionar(3, "{ESC}");

                        Console.WriteLine("Iniciamos Actualizacion de BD Stock de ventas");
                        fa = new FileInfo(Ruta + Stoc);
                        data = new DataTable();
                        if (fa.Exists)
                        {
                            string[] stringarry = System.IO.File.ReadAllLines(Ruta + Stoc, Encoding.Default);

                            if (stringarry.Length > 0)
                            {
                                int cc = 0;
                                var arr_ = stringarry[1].Substring(1, stringarry[1].Length - 2).Split('|');
                                List<string> LColumns = new List<string>();
                                for (int i = 0; i < arr_.Length; i++)
                                {
                                    if (arr_[i].Trim().Length > 0)
                                    {
                                        data.Columns.Add("Col" + cc, typeof(string));
                                        cc++;
                                    }
                                    else
                                    {
                                        data.Columns.Add("Vacio" + i, typeof(string));
                                        LColumns.Add("Vacio" + i);
                                    }
                                }
                                data.AcceptChanges();

                                System.Data.DataRow dr = null;
                                for (int i = 3; i < stringarry.Length - 1; i++)
                                {
                                    stringarry[i] = stringarry[i].Replace("S/N|", "S/N");
                                    var dat = stringarry[i].Substring(1, stringarry[i].Length - 2).Split('|');
                                    dr = data.NewRow();
                                    for (int y = 0; y < dat.Length; y++)
                                    {
                                        dr[y] = dat[y].Trim();
                                    }
                                    data.Rows.Add(dr);
                                }
                                data.AcceptChanges();

                                query = "DELETE FROM PlanMo_New.dbo.STOCK_MB52;\r\n";
                                int cccc = 0;
                                foreach (DataRow item in data.Rows)
                                {
                                    query += "INSERT INTO PlanMo_New.dbo.STOCK_MB52 VALUES (";
                                    for (int i = 0; i < item.ItemArray.Length; i++)
                                    {
                                        if (i == 5 || i == 7)
                                        {
                                            query += "'" + item.ItemArray[i].ToString().Replace("'", "").Replace(",", "").Replace("*", "-") + "',";
                                        }
                                        else
                                        {
                                            query += "'" + item.ItemArray[i].ToString().Replace("'", "") + "',";
                                        }
                                    }
                                    query += ");\r\n";
                                    cccc++;
                                    Console.WriteLine("Leyendo Informacion STOCK_MB52: " + cccc + " rows");
                                }

                                query = query.Replace(",);", ");");

                                conexion = new SqlConnection();
                                conexion.ConnectionString = connString;
                                conexion.Open();

                                myCommand = new SqlCommand(query, conexion);
                                myCommand.CommandTimeout = 360;
                                myCommand.ExecuteNonQuery();
                                myCommand.Dispose();
                                conexion.Close();
                            }
                        }
                        #endregion

                        #region EXTRACCION DE MATERIALES
                        Presionar(8, "MB52");
                        Presionar(3, "{ENTER}");
                        Presionar(2, "{F4}");
                        Presionar(2, "{DOWN}");
                        Presionar(2, "{DOWN}");
                        Presionar(2, "{TAB}");
                        Presionar(2, "{ENTER}");
                        Presionar(3, "+{F4}");
                        Presionar(5, "+{F11}");
                        Presionar(5, "{ENTER}");
                        Presionar(3, "D:");
                        Presionar(3, "{ENTER}");
                        Presionar(5, Material);
                        Presionar(3, "{ENTER}");
                        Presionar(5, "+{TAB}");
                        Presionar(2, "{ENTER}");
                        Presionar(5, "{F8}");
                        Presionar(5, "{ENTER}");
                        Presionar(5, "+{F2}");
                        Presionar(2, "{TAB}");
                        Presionar(2, "{ENTER}");

                        Presionar(5, "+{TAB}");
                        //Presionar(1, "{ENTER}");
                        Presionar(5, Ruta);//D:/
                        Presionar(3, "{TAB}");
                        Presionar(5, Mate); //Materiales.txt
                        Presionar(2, "{TAB}");
                        Presionar(2, "{TAB}");
                        Presionar(2, "{ENTER}");
                        Presionar(2, "+{TAB}");
                        Presionar(2, "{ENTER}");
                        Presionar(2, "{ESC}");

                        Presionar(2, "{ESC}");
                        Presionar(3, "%{F4}");
                        Presionar(3, "+{TAB}");
                        Presionar(3, "{ENTER}");


                        Console.WriteLine("Iniciamos Actualizacion de BD Materiales");
                        fa = new FileInfo(Ruta + Mate);
                        data = new DataTable();
                        if (fa.Exists)
                        {
                            string[] stringarry = System.IO.File.ReadAllLines(Ruta + Mate, Encoding.Default);

                            if (stringarry.Length > 0)
                            {
                                int cc = 0;
                                var arr_ = stringarry[1].Substring(1, stringarry[1].Length - 2).Split('|');
                                List<string> LColumns = new List<string>();
                                for (int i = 0; i < arr_.Length; i++)
                                {
                                    if (arr_[i].Trim().Length > 0)
                                    {
                                        data.Columns.Add("Col" + cc, typeof(string));
                                        cc++;
                                    }
                                    else
                                    {
                                        data.Columns.Add("Vacio" + i, typeof(string));
                                        LColumns.Add("Vacio" + i);
                                    }
                                }
                                data.AcceptChanges();

                                System.Data.DataRow dr = null;
                                for (int i = 3; i < stringarry.Length - 1; i++)
                                {
                                    stringarry[i] = stringarry[i].Replace("S/N|", "S/N");
                                    var dat = stringarry[i].Substring(1, stringarry[i].Length - 2).Split('|');
                                    dr = data.NewRow();
                                    for (int y = 0; y < dat.Length; y++)
                                    {
                                        dr[y] = dat[y].Trim();
                                    }
                                    data.Rows.Add(dr);
                                }
                                data.AcceptChanges();

                                query = "DELETE FROM PlanMo_New.dbo.VENTAS_GR55_MATERIALES;\r\n";
                                int cccc = 0;
                                foreach (DataRow item in data.Rows)
                                {
                                    query += "INSERT INTO PlanMo_New.dbo.VENTAS_GR55_MATERIALES (DESCRIPCION,IDIOMA,MATERIAL) VALUES (";
                                    for (int i = 0; i < item.ItemArray.Length; i++)
                                    {
                                        query += "'" + item.ItemArray[i].ToString().Replace("'", "") + "',";
                                    }
                                    query += ");\r\n";
                                    cccc++;
                                    Console.WriteLine("Leyendo Informacion VENTAS_GR55_MATERIALES: " + cccc + " rows");
                                }

                                query = query.Replace(",);", ");");

                                conexion = new SqlConnection();
                                conexion.ConnectionString = connString;
                                conexion.Open();

                                myCommand = new SqlCommand(query, conexion);
                                myCommand.CommandTimeout = 360;
                                myCommand.ExecuteNonQuery();
                                myCommand.Dispose();
                                conexion.Close();
                            }
                        }
                        #endregion





                        #region "Extraccion de Pedidos"
                        Presionar(5, "ZSD023");//TX Reporte de seguimiento de pedidos
                        Presionar(1, "{ENTER}");
                        Presionar(5, "+{F5}");
                        Presionar(5, "{F8}");
                        Presionar(1, "{DOWN}");
                        Presionar(1, "{DOWN}");
                        Presionar(1, "{DOWN}");
                        Presionar(1, "{DOWN}");
                        Presionar(1, "+{TAB}");
                        Presionar(1, "{TAB}");
                        Presionar(1, DateTime.Now.AddDays(-150).ToString("dd.MM.yyyy"));//FALTA DEFINIR RANGO DE FECHAS CON ALESSANDRA
                        Presionar(1, "{TAB}");
                        Presionar(1, DateTime.Now.ToString("dd.MM.yyyy"));
                        Presionar(5, "{F8}");

                        #region FILTRAR COLUMNA Material
                        Presionar(240, "^{F5}");//Ctrl+F5
                        Presionar(1, "^{TAB}");//Ctrl+Tab
                        Presionar(1, "^{TAB}");//Ctrl+Tab
                        Presionar(1, "^{TAB}");//Ctrl+Tab
                        Presionar(1, "{ENTER}");
                        Presionar(1, "{TAB}");
                        Presionar(1, "{TAB}");
                        Presionar(1, "{ENTER}");
                        Presionar(1, "+{F4}");
                        Presionar(1, "+{F11}");
                        Presionar(1, "{ENTER}");
                        Presionar(2, "D:");
                        Presionar(2, "{ENTER}");
                        Presionar(5, Material);
                        Presionar(1, "{ENTER}");
                        Presionar(5, "+{TAB}");
                        Presionar(1, "{ENTER}");
                        Presionar(3, "{F8}");
                        Presionar(5, "{ENTER}");
                        #endregion

                        Presionar(5, "^+{F9}");
                        Presionar(5, "{TAB}");
                        Presionar(1, "{ENTER}");

                        Presionar(5, "+{TAB}");
                        Presionar(1, "{ENTER}");
                        Presionar(5, Ruta);//D:/
                        Presionar(2, "{TAB}");
                        Presionar(2, Pedi); //Pedidos.txt
                        Presionar(2, "{TAB}");
                        Presionar(2, "{TAB}");
                        Presionar(2, "{ENTER}");
                        Presionar(2, "+{TAB}");
                        Presionar(2, "{ENTER}");

                        Presionar(2, "{F3}");
                        Presionar(2, "{F3}");
                        Presionar(2, "{ESC}");

                        Console.WriteLine("Iniciamos Actualizacion de BD Pedidos");
                        fa = new FileInfo(Ruta + Pedi);
                        data = new DataTable();
                        if (fa.Exists)
                        {
                            string[] stringarry = System.IO.File.ReadAllLines(Ruta + Pedi, Encoding.Default);

                            if (stringarry.Length > 0)
                            {
                                int cc = 0;
                                var arr_ = stringarry[8].Substring(1, stringarry[8].Length - 2).Split('|');
                                List<string> LColumns = new List<string>();
                                for (int i = 0; i < arr_.Length; i++)
                                {
                                    if (arr_[i].Trim().Length > 0)
                                    {
                                        data.Columns.Add("Col" + cc, typeof(string));
                                        cc++;
                                    }
                                    else
                                    {
                                        data.Columns.Add("Vacio" + i, typeof(string));
                                        LColumns.Add("Vacio" + i);
                                    }
                                }
                                data.AcceptChanges();

                                System.Data.DataRow dr = null;
                                for (int i = 10; i < stringarry.Length - 1; i++)
                                {
                                    stringarry[i] = stringarry[i].Replace("S/N|", "S/N");
                                    var dat = stringarry[i].Substring(1, stringarry[i].Length - 2).Split('|');
                                    dr = data.NewRow();
                                    for (int y = 0; y < dat.Length; y++)
                                    {
                                        dr[y] = dat[y].Trim();
                                    }
                                    data.Rows.Add(dr);
                                }
                                data.AcceptChanges();

                                data.Columns.Remove("Col22");
                                data.Columns.Remove("Col23");
                                data.Columns.Remove("Col24");
                                data.Columns.Remove("Col25");
                                data.AcceptChanges();

                                query = "DELETE FROM PlanMo_New.dbo.PEDIDOS_ZSD023;\r\n";
                                int cccc = 0;
                                foreach (DataRow item in data.Rows)
                                {
                                    query += "INSERT INTO PlanMo_New.dbo.PEDIDOS_ZSD023 VALUES (";
                                    for (int i = 0; i < item.ItemArray.Length; i++)
                                    {
                                        if (i == 3 || i == 18)
                                        {
                                            query += "'" + DateTime.Parse(item.ItemArray[i].ToString().Replace("'", "").Replace(".", "/").ToString()).ToString("yyyy-MM-dd") + "',";
                                        }
                                        else
                                        {
                                            if (i == 15 || i == 17 || i == 19 || i == 20)
                                            {
                                                query += "'" + item.ItemArray[i].ToString().Replace("'", "").Replace(",", "") + "',";
                                            }
                                            else
                                            {
                                                if (i == 4)
                                                {
                                                    query += "'" + item.ItemArray[i].ToString().Replace("'", "").Replace(".", "").Replace(" ", "") + "',";
                                                }
                                                else
                                                {
                                                    query += "'" + item.ItemArray[i].ToString().Replace("'", "") + "',";
                                                }
                                            }
                                        }
                                    }
                                    query += ");\r\n";
                                    cccc++;
                                    Console.WriteLine("Leyendo Informacion PEDIDOS_ZSD023: " + cccc + " rows");
                                }

                                query = query.Replace(",);", ");");

                                conexion = new SqlConnection();
                                conexion.ConnectionString = connString;
                                conexion.Open();

                                myCommand = new SqlCommand(query, conexion);
                                myCommand.CommandTimeout = 360;
                                myCommand.ExecuteNonQuery();
                                myCommand.Dispose();
                                conexion.Close();
                            }
                        }
                        #endregion

                        Presionar(2, "(%{F4})");
                        Presionar(2, "{TAB}");
                        Presionar(2, "{ENTER}");

                        Presionar(2, "(%{F4})");

                        query = @"DELETE FROM PlanMo_New.dbo.VENTASySTOCK_Facturaciones WHERE YEAR(Fecha)=YEAR(GETDATE()) AND MONTH(Fecha)=MONTH(GETDATE()); 
                        INSERT INTO PlanMo_New.dbo.VENTASySTOCK_Facturaciones 
                        SELECT	a.OrgVt,a.Ce,a.CDis,a.Doccompr,a.Docventas,'00' Pos,'000000000' Nro_Entrega,'000000000' Nro_Transporte,a.Ndocref,a.Cliente,
                        c.NOMBRE,'Concluido' StatusEntrega,a.Material,b.DESCRIPCION,a.UM,a.Mon,'' DocAnulacion,a.Registrado,0 CantidadPedido,a.Cantidad,
                        a.Registrado,a.Soles,0 MontoPedido,a.Fecontab,'','',a.CeBe,a.Dolares,a.Periodo,a.Anio,a.Usuario,a.Ndoc,a.ClFac,a.Clase,a.Ncuenta,a.Denominacion,a.Texto 
                        FROM PlanMo_New.dbo.VENTAS_GR55 a 
                        INNER JOIN PlanMo_New.dbo.VENTAS_GR55_MATERIALES b ON b.Material=a.Material 
                        INNER JOIN PlanMo_New.dbo.VENTAS_GR55_CLIENTES c ON c.Cliente=a.Cliente 
                        WHERE a.ClFac NOT IN ('ZS1','ZRE');";

                        conexion = new SqlConnection();
                        conexion.ConnectionString = connString;
                        conexion.Open();

                        myCommand = new SqlCommand(query, conexion);
                        myCommand.CommandTimeout = 360;
                        myCommand.ExecuteNonQuery();
                        myCommand.Dispose();
                        conexion.Close();
                    }
                }
                catch (Exception ex)
                {
                    try
                    {
                        MailMessage msg = new MailMessage();
                        msg.From = new MailAddress("Sistemas TI<sistemasti@agricolachira.com.pe>");

                        msg.To.Add("Junior Alexander Hidalgo Socola<jhidalgos@agricolachira.com.pe>");
                        //msg.To.Add("Jimmy Vasquez Castro<jvasquezc@agricolachira.com.pe>");

                        msg.Subject = "SAP TX GR55 - VENTAS SIPLAN";

                        string htmlString = @"<html>
                      <body style='font-family: Calibri, Candara, Segoe, Segoe UI, Optima, Arial, sans-serif;'>
                      <p></p>
                      <p>" + ex.Message + @"</p>
                      <p></p>
                      <p>" + query + @"</p>
                      <p></p>
                      </body>
                      </html>";

                        msg.Body = htmlString;
                        msg.IsBodyHtml = true;

                        SmtpClient smt = new SmtpClient();
                        smt.Host = "10.72.1.71";

                        NetworkCredential ntcd = new NetworkCredential();
                        smt.Port = 25;
                        smt.Credentials = ntcd;
                        smt.Send(msg);
                    }
                    catch (Exception ex_)
                    {
                    }
                }
            }
            else
            {
                try
                {
                    //  Send app instruction to close itself
                    if (!p.CloseMainWindow())
                    {
                        //  Unable to comply - has to be put to death
                        //  Merciful people might give it a few retries 
                        //  before execution
                        p.Kill();
                    }
                }
                catch (Exception ex)
                {
                    //  Inform user about error
                }
                finally
                {
                    //  So the cycle of life can start again
                    p = null;
                }
            }
        }
    }
}
