using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;

namespace ReporteIngSISASIS
{
    class Program
    {
        static void Main(string[] args)
        {
            String path = string.Empty;
            String servidor = string.Empty;
            FileInfo fa = new FileInfo(AppDomain.CurrentDomain.BaseDirectory + "/directorio.txt");
            if (fa.Exists)
            {
                OpeIO opeIO = new OpeIO(AppDomain.CurrentDomain.BaseDirectory + "/directorio.txt");
                servidor = opeIO.ReadLineByNum(1);
                path = opeIO.ReadLineByNum(2);
            }
            string connString = @"Data Source=CHISULSQL1;Database=Asistencia;uid=ctcuser;pwd=ctcuser;";
            //string query = "SELECT DISTINCT idestane,RTRIM(desestane) AS desestane FROM estane (NOLOCK) WHERE idestane<>'0006'";
            string query = "SELECT a.*,b.codmenu FROM usrfile a (NOLOCK) INNER JOIN optmenu b (NOLOCK) ON b.idusr=a.idusr ";
            query+= "AND b.codmenu IN ('MAPL0998','MAPL0999') ORDER BY typeusr;";

            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = connString;
            conexion.Open();

            DataTable usuarios = new DataTable();
            try
            {
                using (SqlDataAdapter myDataAdapter = new SqlDataAdapter(query, conexion))
                {
                    DataSet myDataSet = new DataSet();
                    myDataAdapter.Fill(myDataSet, "tblData");

                    usuarios = myDataSet.Tables[0];
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            conexion.Close();

            //var results = from myRow in usuarios.AsEnumerable()
            //              where myRow.Field<int>("RowNo") == 1
            //              select myRow;
            var gSedes = usuarios.AsEnumerable().GroupBy(x => x.Field<string>("codmenu")).Select(x => x.First()).ToList();

            string sedes = string.Empty;
            string fundos = string.Empty;
            foreach (DataRow item in gSedes)
            {
                string codmenu = item.ItemArray[6].ToString().Trim();
                switch (item.ItemArray[6].ToString().Trim())
                {
                    case "MAPL0998": sedes = "'0001','0004'"; fundos = "Montelima / San Vicente"; break;
                    case "MAPL0999": sedes = "'0003','0005'"; fundos = "Lobo / La Huaca"; break;
                }

                var gUsers = usuarios.AsEnumerable().Where(x => x.Field<string>("codmenu").Trim() == codmenu).ToList();

                query = " SELECT X.desusr,X.idtipoper,C.destipoper, X.total FROM (";
                query += "SELECT T.desusr,T.idtipoper, COUNT(1) AS total FROM (";
                query += "SELECT c.desusr,b.idtipoper,a.fecha,a.idperplan FROM CONTROL a (NOLOCK) ";
                query += "INNER JOIN perplan b(NOLOCK) ON b.idperplan = a.idperplan ";
                query += "INNER JOIN usrfile c (NOLOCK) ON c.idusr=a.idlogin AND c.email IN (" + sedes + ") ";
                query += "INNER JOIN estane d (NOLOCK) ON d.idestane=c.email ";
                query += "WHERE a.fecha = '"+ DateTime.Now.ToString("yyyy-MM-dd") + "' ";
                query += "GROUP BY c.desusr,b.idtipoper,a.fecha,a.idperplan) T ";
                query += "GROUP BY T.desusr,T.idtipoper) X ";
                query += "INNER JOIN tipoper C (NOLOCK) ON C.idtipoper=X.idtipoper ORDER BY 1 DESC;";
            
                conexion = new SqlConnection();
                conexion.ConnectionString = connString;
                conexion.Open();

                try
                {
                    using (SqlDataAdapter myDataAdapter = new SqlDataAdapter(query, conexion))
                    {
                        DataSet myDataSet = new DataSet();
                        myDataAdapter.Fill(myDataSet, "tblData");

                        DataTable dt = myDataSet.Tables[0];

                        //Exporta exporta = new Exporta();
                        //exporta.Excel_FromDataTable(myDataSet.Tables[0], "Reporte " + DateTime.Now.ToString("dd-MM-yyyy") + ".xls");

                        //MailMessage mail = new MailMessage();
                        //SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");
                        //mail.From = new MailAddress("sisasismobile@agricolachira.com.pe");
                        //mail.To.Add("to_mail@gmail.com");
                        //mail.Subject = "Reporte Fundo ";
                        //mail.Body = "Reporte de Ingreso de Personal en Fundo";

                        //System.Net.Mail.Attachment attachment;
                        //attachment = new System.Net.Mail.Attachment("c:/textfile.txt");
                        //mail.Attachments.Add(attachment);

                        MailMessage msg = new MailMessage();
                        msg.IsBodyHtml = true;
                        msg.From = new MailAddress("Reporte de ingresos diarios del Personal<sisasismobile@agricolachira.com.pe>");

                        int cc = 0;
                        string para = string.Empty;
                        string cuerpo = string.Empty;
                        foreach (var usr in gUsers)
                        {
                            if (cc == 0)
                            {
                                para = FirsLetter(usr.Field<string>("desusr"));
                                msg.To.Add(para + "<jhidalgos@agricolachira.com.pe>");
                                //msg.To.Add(para + "<" + usr.Field<string>("email").ToLower() + ">");
                            }
                            else
                            {
                                msg.CC.Add(FirsLetter(usr.Field<string>("desusr")) + "<" + usr.Field<string>("email").ToLower() + ">");
                            }
                            cc++;
                        }

                        msg.Subject = "Reporte de ingresos diarios del Personal en Fundo " + fundos;

                        cuerpo = @"<table id='customers'>
                            <tr><th>Sede</th>
                            <th>Nro. Ingresos</th>
                            <th>Nomina</th></tr>";

                        foreach (DataRow ro in dt.Rows)
                        {
                            cuerpo += "<tr><td>" + ro.ItemArray[0].ToString() + "</td>" +
                                "<td>" + ro.ItemArray[3].ToString() + "</td>" +
                                "<td>" + ro.ItemArray[2].ToString() + "</td></tr>";
                        }
                        cuerpo += "</table>";

                        //msg.Body = htmlString;
                        msg.AlternateViews.Add(Mail_Body(para, fundos, cuerpo));
                        msg.BodyEncoding = Encoding.Default;
                        //msg.Priority = MailPriority.High;

                        SmtpClient smt = new SmtpClient();
                        smt.Host = "10.72.1.71";
                        System.Net.NetworkCredential ntcd = new NetworkCredential();
                        smt.Port = 25;
                        smt.EnableSsl = false;
                        smt.DeliveryMethod = SmtpDeliveryMethod.Network;
                        smt.Credentials = ntcd;

                        smt.Send(msg);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                conexion.Close();
            }
        }

        private static AlternateView Mail_Body(string para, string fundos, string cuerpo)
        {
            //LinkedResource Img = new LinkedResource("firma.jpeg", MediaTypeNames.Image.Jpeg);
            LinkedResource Img = new LinkedResource(AppDomain.CurrentDomain.BaseDirectory + "/firma.jpeg", MediaTypeNames.Image.Jpeg);
            Img.ContentId = "MyImage";
            string str = @"<html><head>
                          <style> #customers {font-family:Arial,Helvetica,sans-serif;border-collapse:collapse;width:100%;} 
                          #customers td, #customers th {border:1px solid #ddd;padding:8px;}
                          #customers tr:nth-child(even){background-color: #f2f2f2;}
                          #customers tr:hover {background-color: #ddd;} 
                          #customers th {padding-top:12px;padding-bottom:12px;text-align:left;background-color:#4CAF50;color:white;} </style></head>
                          <body style='font-family: Calibri,Candara,Segoe,Segoe UI,Optima,Arial,sans-serif;'>
                          <p>Estimado " + para + @",</p>
                          <p></p>
                          <p>Se envia Reporte diario de fundos: <b>" + fundos + @"</b></p>
                          <p></p>
                          <p>" + cuerpo + @"</p>
                          <p></p>
                          <p>Saludos,</p>
                          <img src=cid:MyImage  id='img' alt='' />
                          </body>
                          </html>";
            
            AlternateView AV =
            AlternateView.CreateAlternateViewFromString(str, null, MediaTypeNames.Text.Html);
            AV.LinkedResources.Add(Img);
            return AV;
        }

        private static string FirsLetter(string cadena)
        {
            string cadena_ = string.Empty;
            var cad = cadena.Split(' ');

            if (cad.Length > 0)
            {
                foreach (var item in cad)
                {
                    string firs = item.Substring(0, 1);
                    cadena_ += firs + item.Substring(1, item.Length - 1).ToLower() + " ";
                }
            }
            return cadena_.Trim();
        }
    }
}
