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

namespace Repprogactagri
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
            string connString = @"Data Source=" + servidor + ";Database=Asistencia;uid=ctcuser;pwd=ctcuser;";
            string query = "SELECT a.*,b.codmenu FROM usrfile a (NOLOCK) INNER JOIN optmenu b (NOLOCK) ON b.idusr=a.idusr ";
            query += "AND b.codmenu IN ('MAPL0998','MAPL0999') ORDER BY typeusr;";

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

            var gSedes = usuarios.AsEnumerable().GroupBy(x => x.Field<string>("codmenu")).Select(x => x.First()).ToList();

            string sedes = string.Empty;
            string fundos = string.Empty;
            foreach (DataRow item in gSedes)
            {
                string codmenu = item.ItemArray[7].ToString().Trim();
                switch (codmenu)
                {
                    case "MAPL0998": sedes = "'ML01','SV01','SJ01'"; fundos = "Montelima / San Vicente"; break;
                    case "MAPL0999": sedes = "'LB01','LB02','HC01','BT01'"; fundos = "Lobo / La Huaca"; break;
                }

                var gUsers = usuarios.AsEnumerable().Where(x => x.Field<string>("codmenu").Trim() == codmenu).ToList();

                query = @"  SELECT a.idprog,CONVERT(VARCHAR(10),a.fecha,120) [fecha],a.capataz,a.fundo,a.equipo,a.turno,a.actividad,
                            a.cantidadprog,a.cantidadreal,a.chkVB,b.desusr,c.desmaesgen FROM progagri_fecafueqtuac a (NOLOCK) 
                            INNER JOIN usrfile b (NOLOCK) ON b.idusr=a.capataz 
                            INNER JOIN maesgen c (NOLOCK) ON c.idmaesgen='162' AND c.clavemaesgen=a.actividad
                            WHERE fecha = '" + DateTime.Now.ToString("yyyy-MM-dd") + "' " +
                            "AND a.fundo IN (" + sedes + @") AND a.chkVB=1 UNION ALL
                            SELECT a.idprog,CONVERT(VARCHAR(10),a.fecha,120) [fecha],a.capataz,a.fundo,a.equipo,a.turno,a.actividad,
                            a.cantidadprog,a.cantidadreal,a.chkVB,b.desusr,c.desmaesgen FROM progagri_fecafueqtuac a (NOLOCK) 
                            INNER JOIN usrfile b (NOLOCK) ON b.idusr=a.capataz 
                            INNER JOIN maesgen c (NOLOCK) ON c.idmaesgen='162' AND c.clavemaesgen=a.actividad
                            WHERE a.fecha = '" + DateTime.Now.ToString("yyyy-MM-dd") + "' " +
                            "AND a.fundo IN ('INFR') AND a.equipo IN (" + sedes + ") AND a.chkVB=1";

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
                        MailMessage msg = new MailMessage();
                        msg.IsBodyHtml = true;
                        msg.From = new MailAddress("Programacion de Actividades Agricolas<actividadesagricolas@agricolachira.com.pe>");

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
                                //msg.CC.Add(FirsLetter(usr.Field<string>("desusr")) + "<" + usr.Field<string>("email").ToLower() + ">");
                            }
                            cc++;
                        }

                        string fecha = string.Empty;
                        if (dt.Rows.Count > 0) { fecha = " (" + dt.Rows[0].ItemArray[1].ToString() + ")"; }
                        msg.Subject = "Programacion de Actividades Agricolas de Fundos " + fundos + fecha;

                        cuerpo = @"<table id='customers'><tr>
                            <th>Fundo</th>
                            <th>Tareador</th>
                            <th>Actividad</th>
                            <th>Equipo</th>
                            <th>Turno</th>
                            <th>N° Jornales</th>
                            <th>Total</th>
                            </tr>";

                        var Fundos = dt.AsEnumerable().GroupBy(x => x.Field<string>("fundo")).ToList();

                        foreach (var fu in Fundos)
                        {
                            var Capataces = dt.AsEnumerable().Where(x => x.Field<string>("fundo") == fu.Key.ToString()).ToList();

                            foreach (var ca in Capataces.GroupBy(x => x.Field<string>("capataz")).ToList())
                            {
                                var Actividades = dt.AsEnumerable()
                                    .Where(x => x.Field<string>("fundo") == fu.Key.ToString() && x.Field<string>("capataz") == ca.Key.ToString())
                                    .ToList();

                                foreach (var ac in Actividades.GroupBy(x => x.Field<string>("actividad")).ToList())
                                {
                                    var det = dt.AsEnumerable()
                                        .Where(x => x.Field<string>("fundo") == fu.Key.ToString() && x.Field<string>("capataz") == ca.Key.ToString()
                                        && x.Field<string>("actividad") == ac.Key.ToString()).ToList();
                                    int ccc = 1;
                                    foreach (DataRow ro in det)
                                    {
                                        cuerpo += "<tr>";
                                        if (ccc == 1)
                                        {
                                            cuerpo += "<td rowspan='" + Capataces.Count + "' style='vertical-align: top'>" + fu.Key.ToString() + "</td>";
                                            cuerpo += "<td rowspan='" + Actividades.Count + "' style='vertical-align: top'>" + ro.ItemArray[10].ToString() + "</td>";
                                            cuerpo += "<td rowspan='" + det.Count + "' style='vertical-align: top'>" + ro.ItemArray[11].ToString() + "</td>";
                                        }
                                        cuerpo += "<td>" + ro.ItemArray[4].ToString() + "</td>";
                                        cuerpo += "<td>" + ro.ItemArray[5].ToString() + "</td>";
                                        cuerpo += "<td>" + ro.ItemArray[8].ToString() + "</td>";
                                        if (ccc == 1)
                                        {
                                            cuerpo += "<td rowspan='" + Capataces.Count + "'>" + Actividades.Sum(x => x.Field<int>("cantidadReal")) + "</td>";
                                        }
                                        cuerpo += "</tr>";

                                        ccc++;
                                    }
                                }
                            }
                        }

                        //foreach (DataRow ro in dt.Rows)
                        //{
                        //    cuerpo += "<tr>";
                        //    cuerpo += "<td>" + ro.ItemArray[3].ToString() + "</td>";
                        //    cuerpo += "<td>" + ro.ItemArray[10].ToString() + "</td>";
                        //    cuerpo += "<td>" + ro.ItemArray[11].ToString() + "</td>";
                        //    cuerpo += "<td>" + ro.ItemArray[4].ToString() + "</td>";
                        //    cuerpo += "<td>" + ro.ItemArray[5].ToString() + "</td>";
                        //    cuerpo += "<td>" + ro.ItemArray[8].ToString() + "</td>";
                        //    cuerpo += "</tr>";
                        //}
                        cuerpo += "<tr><td colspan='6'>Total General: </td><td>" + dt.Rows.Count + "</td></tr>";
                        cuerpo += "</table>";

                        msg.AlternateViews.Add(Mail_Body(para, fundos, cuerpo, fecha));
                        msg.BodyEncoding = Encoding.Default;

                        SmtpClient smt = new SmtpClient();
                        smt.Host = "10.72.1.71";
                        System.Net.NetworkCredential ntcd = new NetworkCredential();
                        smt.Port = 25;
                        smt.EnableSsl = false;
                        smt.DeliveryMethod = SmtpDeliveryMethod.Network;
                        smt.Credentials = ntcd;

                        if (dt.Rows.Count > 0) { smt.Send(msg); }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                conexion.Close();
            }
        }

        private static AlternateView Mail_Body(string para, string fundos, string cuerpo, string fecha)
        {
            LinkedResource Img = new LinkedResource(AppDomain.CurrentDomain.BaseDirectory + "/firma.jpeg", MediaTypeNames.Image.Jpeg);
            Img.ContentId = "MyImage";
            string str = @"<html><head>
                          <style> #customers {font-family:Arial,Helvetica,sans-serif;border-collapse:collapse;width:100%;} 
                          #customers td, #customers th {border:1px solid #ddd;padding:4px;font-size:10px}
                          #customers tr:nth-child(even){background-color: #f2f2f2;}
                          #customers tr:hover {background-color: #ddd;} 
                          #customers th {padding-top:10px;padding-bottom:10px;text-align:left;background-color:#4CAF50;color:white;} </style></head>
                          <body style='font-family: Calibri,Candara,Segoe,Segoe UI,Optima,Arial,sans-serif;'>
                          <p>Estimado " + para + @",</p>
                          <p></p>
                          <p>Se envía Reporte diario de Actividades Agrícolas de fundos" + fecha + ": <b>" + fundos + @"</b></p>
                          <p></p>
                          <p>" + cuerpo + @"</p>
                          <p></p>
<img><a href='http://chisulapp01/intranet'>ACEPTAR</a></img>
<img><a href='http://chisulapp01/conformidad/parm=0'>CANCELAR</a></img>
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
