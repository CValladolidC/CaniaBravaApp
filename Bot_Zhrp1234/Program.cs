using System;
using System.Collections.Generic;
using System.Configuration;
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

namespace Bot_Zhrp1234
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
                            string query = string.Empty;
                DataTable data = new DataTable();
                try
                {
                    String Usu = string.Empty, Pas = string.Empty, Ruta = string.Empty, Arch = string.Empty, Prog = string.Empty, Serv = string.Empty;
                    FileInfo fa = new FileInfo(AppDomain.CurrentDomain.BaseDirectory + "/directorio.txt");
                    if (fa.Exists)
                    {
                        OpeIO opeIO = new OpeIO(AppDomain.CurrentDomain.BaseDirectory + "/directorio.txt");
                        Usu = opeIO.ReadLineByNum(1);
                        Pas = opeIO.ReadLineByNum(2);
                        Ruta = opeIO.ReadLineByNum(3);
                        Arch = opeIO.ReadLineByNum(4);
                        Prog = opeIO.ReadLineByNum(5);
                        Serv = opeIO.ReadLineByNum(6);
                    }

                    // Start the child process.
                    // Redirect the output stream of the child process.
                    p = new Process();
                    p.StartInfo.UseShellExecute = false;
                    p.StartInfo.RedirectStandardOutput = true;
                    p.StartInfo.FileName = Prog;
                    p.Start();

                    Presionar(40, "{ENTER}");//OPEN SAP LOGIN

                    Presionar(5, Usu); //ASSESSMENT
                    Presionar(5, "{TAB}"); //$5Sistemas
                    Presionar(5, Pas); //$5Sistemas
                    Presionar(3, "{ENTER}");

                    Presionar(3, "ZHRP1234");
                    Presionar(3, "{ENTER}");

                    Presionar(10, "+{F5}");
                    Presionar(2, "{F8}");
                    Presionar(6, "{F8}");

                    Presionar(400, "^+{F9}");
                    Presionar(10, "{TAB}");
                    Presionar(3, "{ENTER}");

                    Presionar(11, "+{TAB}");
                    Presionar(3, Ruta);//D:/
                    Presionar(3, "{TAB}");
                    Presionar(3, Arch); //Maestro.txt
                    Presionar(3, "{TAB}");
                    Presionar(3, "{TAB}");
                    Presionar(1, "{ENTER}");

                    Presionar(5, "+{TAB}");
                    Presionar(1, "{ENTER}");

                    Presionar(6, "{ESC}");
                    Presionar(3, "{ESC}");
                    Presionar(4, "(%{F4})");
                    Presionar(2, "{TAB}");
                    Presionar(1, "{ENTER}");

                    Presionar(3, "(%{F4})");

                    Console.WriteLine("Leyendo datos SAP");
                    FileInfo fab = new FileInfo(Ruta + Arch);
                    data = new DataTable();
                    if (fab.Exists)
                    {
                        string[] stringarry = System.IO.File.ReadAllLines(Ruta + Arch, Encoding.Default);

                        if (stringarry.Length > 0)
                        {
                            int cc = 0;
                            var arr_ = stringarry[7].Split('|');
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
                            arr_ = stringarry[8].Split('|');
                            int datOld = stringarry[7].Split('|').Length;
                            for (int i = 0; i < arr_.Length; i++)
                            {
                                if (arr_[i].Trim().Length > 0)
                                {
                                    data.Columns.Add("Col" + cc, typeof(string));
                                    cc++;
                                }
                                else
                                {
                                    data.Columns.Add("Vacio" + (i + datOld), typeof(string));
                                    LColumns.Add("Vacio" + (i + datOld));
                                }
                            }
                            data.AcceptChanges();

                            System.Data.DataRow dr = null;
                            for (int i = 10; i < stringarry.Length; i++)
                            {
                                stringarry[i] = stringarry[i].Replace("S/N|", "S/N");
                                var dat = stringarry[i].Split('|');
                                if ((i % 2) == 0)
                                {
                                    dr = data.NewRow();
                                }
                                datOld = stringarry[i - 1].Split('|').Length;
                                for (int y = 0; y < dat.Length; y++)
                                {
                                    if ((i % 2) == 0)
                                    {
                                        dr[y] = dat[y].Trim();
                                    }
                                    else
                                    {
                                        if (dr.ItemArray.Length > (y + datOld))
                                        {
                                            dr[y + datOld] = dat[y].Trim();
                                        }
                                    }
                                }
                                if ((i % 2) != 0)
                                {
                                    data.Rows.Add(dr);
                                }
                            }
                            data.AcceptChanges();

                            Console.WriteLine("Insertando datos SAP en SISASIS");

                            foreach (var item in LColumns)
                            {
                                data.Columns.Remove(item);
                            }

                            int cccc = 0;
                            query = @"IF EXISTS(SELECT * FROM sys.tables WHERE name='perplan_old') BEGIN DROP TABLE perplan_old; END;
SELECT idperplan,idcia,codaux,apepat,apemat,nombres,fecnac,tipdoc,nrodoc,telfijo,celular,rpm,estcivil,nacion,email,catlic,nrolic,tipotrab,idtipoper,nivedu,idlabper,seccion,regpen,cuspp
,contrab,tippag,pering,sitesp,entfinrem,nroctarem,monrem,tipctarem,entfincts,nroctacts,moncts,tipctacts,tipvia,nomvia,nrovia,intvia,tipzona,nomzona,refzona,ubigeo,dscubigeo,sexo,ocurpts
,afiliaeps,eps,discapa,sindica,situatrab,sctrsanin,sctrsaessa,sctrsaeps,sctrpennin,sctrpenonp,sctrpenseg,asigemplea,rucemp,estane,foto,idtipoplan,domicilia,esvida,fecregpen,regalterna,trabmax
,trabnoc,quicat,renexo,asepen,apliconve,exoquicat,paisemi,disnac,deparvia,manzavia,lotevia,kmvia,block,etapa,reglab,ruc,0 alta_tregistro,baja_tregistro,fecharegistro,fechaupdate INTO perplan_old FROM perplan; 
DELETE FROM perplan;";
                            foreach (DataRow item in data.Rows)
                            {
                                query += "INSERT INTO MaestroSAP VALUES (";
                                for (int i = 0; i < item.ItemArray.Length; i++)
                                {
                                    //if (cccc == 332) { Console.WriteLine(item.ItemArray[i].ToString().Replace("'", "") + " Nro: " + item.ItemArray[i].ToString().Length); }
                                    if (i == 2) { query += "'" + int.Parse(item.ItemArray[i].ToString()) + "',"; }
                                    else
                                    {
                                        if (i == 28 || i == 65 || i == 66 || i == 67 || i == 68 || i == 83)
                                        {
                                            if (item.ItemArray[i].ToString().Trim() != string.Empty)
                                            {
                                                query += "'" + DateTime.Parse(item.ItemArray[i].ToString().Replace(".", "/")).ToString("yyyy-MM-dd") + "',";
                                            }
                                            else { query += "'',"; }
                                        }
                                        else
                                        {
                                            query += "'" + item.ItemArray[i].ToString().Replace("'", "") + "',";
                                        }
                                    }
                                }
                                query += ");\r\n";
                                query = query.Replace(",);", ");");
                                cccc++;
                                Console.WriteLine("Leyendo Informacion: " + cccc + " rows");
                            }

                            query += @"INSERT INTO perplan 
SELECT idperplan,idcia,codaux,apepat,apemat,nombres,fecnac,tipdoc,nrodoc,telfijo,celular,rpm,estcivil,nacion,email,catlic,nrolic,tipotrab,idtipoper,nivedu,idlabper,seccion,regpen,cuspp
,contrab,tippag,pering,sitesp,entfinrem,nroctarem,monrem,tipctarem,entfincts,nroctacts,moncts,tipctacts,tipvia,nomvia,nrovia,intvia,tipzona,nomzona,refzona,ubigeo,dscubigeo,sexo,ocurpts
,afiliaeps,eps,discapa,sindica,situatrab,sctrsanin,sctrsaessa,sctrsaeps,sctrpennin,sctrpenonp,sctrpenseg,asigemplea,rucemp,estane,foto,idtipoplan,domicilia,esvida,fecregpen,regalterna,trabmax
,trabnoc,quicat,renexo,asepen,apliconve,exoquicat,paisemi,disnac,deparvia,manzavia,lotevia,kmvia,block,etapa,reglab,ruc,0,baja_tregistro,fecharegistro,fechaupdate
FROM perplan_old WHERE idperplan NOT IN (SELECT idperplan FROM perplan); ";
                            query += "DROP TABLE perplan_old;";

                            string connString = @"Data Source=" + Serv + ";Database=Asistencia;uid=usr_asistencia;pwd=4Sist3nc14@21;";
                            SqlConnection conexion = new SqlConnection();
                            conexion.ConnectionString = connString;
                            conexion.Open();

                            SqlCommand myCommand = new SqlCommand(query, conexion);
                            myCommand.CommandTimeout = 360;
                            myCommand.ExecuteNonQuery();
                            myCommand.Dispose();
                            //MessageBox.Show("Sincronización de datos correcto..!!", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            conexion.Close();

                            Console.WriteLine("Proceso finalizado..!!");


                            //correo de verificacion

                            DateTime fechaact = DateTime.Now;

                            MailMessage msg = new MailMessage();
                            msg.From = new MailAddress("Sistemas TI<sistemasti@agricolachira.com.pe>");

                            msg.To.Add("Junior Alexander Hidalgo Socola<jhidalgos@agricolachira.com.pe>");
                           
                            msg.Subject = "Sincronizacion Correcta - Maestro de Personal - SAP - SISASIS";

                            string htmlString = @"<html>
                              <body style='font-family: Calibri, Candara, Segoe, Segoe UI, Optima, Arial, sans-serif;'>
                              <p></p>
                              <p>" + fechaact + @"</p>
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

                            //string sqlConnectionString = @"Data Source=" + Serv + ";Database=Asistencia;uid=usr_asistencia;pwd=4Sist3nc14@21;";
                            //using (SqlBulkCopy bulkCopy = new SqlBulkCopy(sqlConnectionString))
                            //{
                            //    bulkCopy.ColumnMappings.Add("Col0", "Codigo");
                            //    bulkCopy.ColumnMappings.Add("Col1", "RUCcia");
                            //    bulkCopy.ColumnMappings.Add("Col2", "Codcia");
                            //    bulkCopy.ColumnMappings.Add("Col3", "DescripcionCia");
                            //    bulkCopy.ColumnMappings.Add("Col4", "CodAreaPersonal");
                            //    bulkCopy.ColumnMappings.Add("Col5", "AreaPersonal");
                            //    bulkCopy.ColumnMappings.Add("Col6", "Posicion");
                            //    bulkCopy.ColumnMappings.Add("Col7", "DescripcionPosicion");
                            //    bulkCopy.ColumnMappings.Add("Col8", "CenCosto");
                            //    bulkCopy.ColumnMappings.Add("Col9", "CentroCosto");
                            //    bulkCopy.ColumnMappings.Add("Col10", "ANom");
                            //    bulkCopy.ColumnMappings.Add("Col11", "AreaNomina");
                            //    bulkCopy.ColumnMappings.Add("Col12", "CodFuncion");
                            //    bulkCopy.ColumnMappings.Add("Col13", "Funcion");
                            //    bulkCopy.ColumnMappings.Add("Col14", "CodOcup");
                            //    bulkCopy.ColumnMappings.Add("Col15", "Ocupacion");
                            //    bulkCopy.ColumnMappings.Add("Col16", "DirGer");
                            //    bulkCopy.ColumnMappings.Add("Col17", "DirGerCorta");
                            //    bulkCopy.ColumnMappings.Add("Col18", "DirGerLarga");
                            //    bulkCopy.ColumnMappings.Add("Col19", "UnidadOrg");
                            //    bulkCopy.ColumnMappings.Add("Col20", "UnidadOrganizativa");
                            //    bulkCopy.ColumnMappings.Add("Col21", "CodJefe");
                            //    bulkCopy.ColumnMappings.Add("Col22", "NombreJefe");
                            //    bulkCopy.ColumnMappings.Add("Col23", "ApellidoPaterno");
                            //    bulkCopy.ColumnMappings.Add("Col24", "ApellidoMaterno");
                            //    bulkCopy.ColumnMappings.Add("Col25", "NombreTrabajador");
                            //    bulkCopy.ColumnMappings.Add("Col26", "CodSexo");
                            //    bulkCopy.ColumnMappings.Add("Col27", "Sexo");
                            //    bulkCopy.ColumnMappings.Add("Col28", "FecNac");
                            //    bulkCopy.ColumnMappings.Add("Col29", "EstadoCivil");
                            //    bulkCopy.ColumnMappings.Add("Col30", "DireccionTrab");
                            //    bulkCopy.ColumnMappings.Add("Col31", "Poblacion");
                            //    bulkCopy.ColumnMappings.Add("Col32", "Distrito1");
                            //    bulkCopy.ColumnMappings.Add("Col33", "CodPostal");
                            //    bulkCopy.ColumnMappings.Add("Col34", "CiudadCO");
                            //    bulkCopy.ColumnMappings.Add("Col35", "Ubigeo");
                            //    bulkCopy.ColumnMappings.Add("Col36", "Departamento");
                            //    bulkCopy.ColumnMappings.Add("Col37", "Provincia");
                            //    bulkCopy.ColumnMappings.Add("Col38", "Distrito");
                            //    bulkCopy.ColumnMappings.Add("Col39", "Telefono");
                            //    bulkCopy.ColumnMappings.Add("Col40", "TipoZona");
                            //    bulkCopy.ColumnMappings.Add("Col41", "Zona");
                            //    bulkCopy.ColumnMappings.Add("Col42", "NroCuenta");
                            //    bulkCopy.ColumnMappings.Add("Col43", "TipoCuenta");
                            //    bulkCopy.ColumnMappings.Add("Col44", "CodBanco");
                            //    bulkCopy.ColumnMappings.Add("Col45", "Banco");
                            //    bulkCopy.ColumnMappings.Add("Col46", "Moneda");
                            //    bulkCopy.ColumnMappings.Add("Col47", "CodBancoCI");
                            //    bulkCopy.ColumnMappings.Add("Col48", "BancoCI");
                            //    bulkCopy.ColumnMappings.Add("Col49", "CuentaSUNAT");
                            //    bulkCopy.ColumnMappings.Add("Col50", "NroCuentaCTS");
                            //    bulkCopy.ColumnMappings.Add("Col51", "TipoCuentaCTS");
                            //    bulkCopy.ColumnMappings.Add("Col52", "CodBancoCTS");
                            //    bulkCopy.ColumnMappings.Add("Col53", "BancoCTS");
                            //    bulkCopy.ColumnMappings.Add("Col54", "MonedaCTS");
                            //    bulkCopy.ColumnMappings.Add("Col55", "CCIcts");
                            //    bulkCopy.ColumnMappings.Add("Col56", "CenCosteDist");
                            //    bulkCopy.ColumnMappings.Add("Col57", "DescCentroCosteDistrib");
                            //    bulkCopy.ColumnMappings.Add("Col58", "PorcenCenCosteDistrib");
                            //    bulkCopy.ColumnMappings.Add("Col59", "NivelEducativo");
                            //    bulkCopy.ColumnMappings.Add("Col60", "DescNivelEduc");
                            //    bulkCopy.ColumnMappings.Add("Col61", "InstitucionEducativa");
                            //    bulkCopy.ColumnMappings.Add("Col62", "DescInstEduc");
                            //    bulkCopy.ColumnMappings.Add("Col63", "Carrera");
                            //    bulkCopy.ColumnMappings.Add("Col64", "DescCarrera");
                            //    bulkCopy.ColumnMappings.Add("Col65", "FecAlta");
                            //    bulkCopy.ColumnMappings.Add("Col66", "FIPlanilla");
                            //    bulkCopy.ColumnMappings.Add("Col67", "FecBaja");
                            //    bulkCopy.ColumnMappings.Add("Col68", "FIGrupo");
                            //    bulkCopy.ColumnMappings.Add("Col69", "email");
                            //    bulkCopy.ColumnMappings.Add("Col70", "Celular");
                            //    bulkCopy.ColumnMappings.Add("Col71", "UsuarioSAP");
                            //    bulkCopy.ColumnMappings.Add("Col72", "CodSSFF");
                            //    bulkCopy.ColumnMappings.Add("Col73", "NumeroMOVISTAR");
                            //    bulkCopy.ColumnMappings.Add("Col74", "CorreoPrivado");
                            //    bulkCopy.ColumnMappings.Add("Col75", "CellPhone");
                            //    bulkCopy.ColumnMappings.Add("Col76", "DNI");
                            //    bulkCopy.ColumnMappings.Add("Col77", "CodEdificio");
                            //    bulkCopy.ColumnMappings.Add("Col78", "Edificio");
                            //    bulkCopy.ColumnMappings.Add("Col79", "CodOficina");
                            //    bulkCopy.ColumnMappings.Add("Col80", "Oficina");
                            //    bulkCopy.ColumnMappings.Add("Col81", "CodSistPrevPens");
                            //    bulkCopy.ColumnMappings.Add("Col82", "SistPrevPens");
                            //    bulkCopy.ColumnMappings.Add("Col83", "FAfilSPP");
                            //    bulkCopy.ColumnMappings.Add("Col84", "CodUnicoSPP");
                            //    bulkCopy.DestinationTableName = "MaestroSAP";
                            //    bulkCopy.WriteToServer(data);
                            //    bulkCopy.BulkCopyTimeout = 1360;
                            //}
                        }
                        //OpeIO opeIO = new OpeIO(AppDomain.CurrentDomain.BaseDirectory + "/directorio.txt");
                        //servidor = opeIO.ReadLineByNum(1);
                        //path = opeIO.ReadLineByNum(2);
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

                        msg.Subject = "Sincronizacion SAP - SISASIS";

                        string htmlString = @"<html>
                      <body style='font-family: Calibri, Candara, Segoe, Segoe UI, Optima, Arial, sans-serif;'>
                      <p></p>
                      <p>" + ex.Message + @"</p>
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
