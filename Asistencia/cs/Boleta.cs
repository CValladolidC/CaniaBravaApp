using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Windows.Forms;
using System.IO;

namespace CaniaBrava
{
    class Boleta
    {
        string descia_va = "";
        public string generaBoletaTabular(string idperplan, string anio, string messem, string idcia, string idtipoper, string idtipoplan, string idtipocal)
        {
            Funciones fn = new Funciones();
            Configsis configsis = new Configsis();
            DataSet dataSet = new DataSet();
            DataTable dtpar = new DataTable();
            DataTable dting = new DataTable();
            DataTable dtdes = new DataTable();
            DataTable dtapor = new DataTable();
            DataRow row;
            DataColumn column;
            DataColumn[] PrimaryKeyColumns;

            //////PARAMETROS/////////////////////////////////////
            column = new DataColumn();
            column.DataType = System.Type.GetType("System.Int32");
            column.ColumnName = "idpar";
            column.ReadOnly = true;
            column.Unique = true;
            column.AutoIncrement = true;
            dtpar.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "idconplanpar";
            column.ReadOnly = true;
            dtpar.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "desboletapar";
            column.ReadOnly = true;
            dtpar.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "valorpar";
            column.ReadOnly = true;
            dtpar.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "idclascolpar";
            column.ReadOnly = true;
            dtpar.Columns.Add(column);

            PrimaryKeyColumns = new DataColumn[1];
            PrimaryKeyColumns[0] = dting.Columns["idpar"];
            dtpar.PrimaryKey = PrimaryKeyColumns;

            //////INGRESOS///////////////////////////////////////
            column = new DataColumn();
            column.DataType = System.Type.GetType("System.Int32");
            column.ColumnName = "iding";
            column.ReadOnly = true;
            column.Unique = true;
            column.AutoIncrement = true;
            dting.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "idconplaning";
            column.ReadOnly = true;
            dting.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "desboletaing";
            column.ReadOnly = true;
            dting.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "valoring";
            column.ReadOnly = true;
            dting.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "idclascoling";
            column.ReadOnly = true;
            dting.Columns.Add(column);

            PrimaryKeyColumns = new DataColumn[1];
            PrimaryKeyColumns[0] = dting.Columns["iding"];
            dting.PrimaryKey = PrimaryKeyColumns;


            //////DESCUENTOS/////////////////////////////////////
            column = new DataColumn();
            column.DataType = System.Type.GetType("System.Int32");
            column.ColumnName = "iddes";
            column.ReadOnly = true;
            column.Unique = true;
            column.AutoIncrement = true;
            dtdes.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "idconplandes";
            column.ReadOnly = true;
            dtdes.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "desboletades";
            column.ReadOnly = true;
            dtdes.Columns.Add(column);


            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "valordes";
            column.ReadOnly = true;
            dtdes.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "idclascoldes";
            column.ReadOnly = true;
            dtdes.Columns.Add(column);

            PrimaryKeyColumns = new DataColumn[1];
            PrimaryKeyColumns[0] = dting.Columns["iddes"];
            dtdes.PrimaryKey = PrimaryKeyColumns;

            /////APORTACIONES////////////////////////////////
            column = new DataColumn();
            column.DataType = System.Type.GetType("System.Int32");
            column.ColumnName = "idapor";
            column.ReadOnly = true;
            column.Unique = true;
            column.AutoIncrement = true;
            dtapor.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "idconplanapor";
            column.ReadOnly = true;
            dtapor.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "desboletaapor";
            column.ReadOnly = true;
            dtapor.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "valorapor";
            column.ReadOnly = true;
            dtapor.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "idclascolapor";
            column.ReadOnly = true;
            dtapor.Columns.Add(column);

            PrimaryKeyColumns = new DataColumn[1];
            PrimaryKeyColumns[0] = dting.Columns["idapor"];
            dtapor.PrimaryKey = PrimaryKeyColumns;

            string strPrinter = "";
            string idclascol = "";
            string total_ingresos = "0.00";
            string total_descuentos = "0.00";
            string total_aporta = "0.00";
            string neto = "0.00";

            string desboletapar;
            string valorpar;
            string desboletaing;
            string valoring;
            string desboletades;
            string valordes;
            string desboletaapor;
            string valorapor;

            int nrodatarow;
            int nrofiladetalle;
            int nrofilapar;

            DataTable boleta = new DataTable();
            //string query = " Select A.idperplan,B.destipoper,C.Parm1maesgen as cortotipodoc,A.nrodoc,A.cuspp, ";
            //query += " CONCAT(CONCAT(CONCAT(A.apepat,' '),CONCAT(A.apemat,' , ')),A.nombres) as nombre,D.desmaesgen as seccion, ";
            //query += " E.deslabper as ocupacion,P.finilab,P.ffinlab,round(P.total_ingresos,2) as total_ingresos,round(P.total_descuentos,2) as total_descuentos, ";
            //query += " round(P.total_aporta,2) as total_aporta,round(P.neto,2) as neto,P.finivac,P.ffinvac,P.diasvaca, ";
            //query += " X.idconplan,Z.desboleta,M.idclascol,Y.desclascol,round(X.valor,2) as valor,A.idcia,Q.descia,Q.ruccia,S.fechaini,S.fechafin, ";
            //query += " T.desfonpen,U.destipoplan,U.decreto,P.messem,P.anio,T.snpfonpen, ";
            //query += " Q.regpatcia,CONCAT(F.Parm1maesgen,' ',G.Parm1maesgen,' ',A.monrem) ";
            //query += " as tipocuentarem,A.nroctarem, ifnull(dtp.pervac,' ') AS anio_vaca from plan_ P inner join conbol X on "; 
            //query += " P.idperplan=X.idperplan and P.idcia=X.idcia ";
            //query += " and P.anio=X.anio and P.messem=X.messem and P.idtipoper=X.idtipoper ";
            //query += " and P.idtipocal=X.idtipocal and P.idtipoplan=X.idtipoplan ";
            //query += " inner join detconplan Z on X.idcia=Z.idcia and ";
            //query += " X.idtipoplan=Z.idtipoplan and X.idtipocal=Z.idtipocal and X.idtipoper=Z.idtipoper ";
            //query += " and X.idconplan=Z.idconplan inner join conplan M on M.idcia=Z.idcia and ";
            //query += " M.idtipoplan=Z.idtipoplan and M.idtipocal=Z.idtipocal and M.idconplan=Z.idconplan";
            //query += " left join clascol Y on M.idclascol=Y.idclascol ";
            //query += " left join labper E on P.idcia=E.idcia and P.idlabper=E.idlabper and P.idtipoper=E.idtipoper";
            //query += " inner join ciafile Q on P.idcia=Q.idcia ";
            //query += " inner join calplan S on P.anio=S.anio and P.messem=S.messem and P.idtipoper=S.idtipoper ";
            //query += " and P.idtipocal=S.idtipocal and P.idcia=S.idcia left join fonpen T on P.idfonpen=T.idfonpen";
            //query += " left join maesgen D on D.idmaesgen='008' and P.seccion=D.clavemaesgen ";
            //query += " inner join tipoplan U on P.idtipoplan=U.idtipoplan  ";
            //query += " inner join perplan A on P.idcia=A.idcia and P.idperplan=A.idperplan ";
            //query += " left join tipoper B on A.idtipoper=B.idtipoper ";
            //query += " left join maesgen C on C.idmaesgen='002' and A.tipdoc=C.clavemaesgen  ";
            //query += " left join maesgen F on F.idmaesgen='007' and A.entfinrem=F.clavemaesgen left join maesgen G on G.idmaesgen='014' and A.tipctarem=G.clavemaesgen";
            //query += " left join dataplan dtp on dtp.finivac=P.finivac and dtp.ffinvac=P.ffinvac and dtp.idperplan=P.idperplan and dtp.idcia=P.idcia and  P.anio=dtp.anio and P.messem=dtp.messem";
            //query += " where A.idperplan='" + @idperplan + "' and P.anio='" + @anio + "' and P.messem='" + @messem + "' and P.idcia='" + @idcia + "' and P.idtipoper='" + @idtipoper + "' ";
            //query += " and P.idtipoplan='" + @idtipoplan + "' and P.idtipocal='" + @idtipocal + "' and Z.imprime='SI' and X.valor>0 order by P.idperplan asc,Y.orden asc,X.idconplan asc;";
            String query = " call generaBoletaTabular('" + @idperplan + "','" + @anio + "','" + @messem + "','" + @idcia + "','" + @idtipoper + "','" + @idtipoplan + "','" + @idtipocal + "', '')";
            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();
            SqlDataAdapter da = new SqlDataAdapter();

            da.SelectCommand = new SqlCommand(query, conexion);
            da.Fill(boleta);

            if (boleta.Rows.Count > 0)
            {
                nrodatarow = 0;
                nrofilapar = 0;

                foreach (DataRow row_boleta in boleta.Rows)
                {
                    string descia, nombre, seccion, ocupacion, finilab, ffinlab, fechaini, fechafin, ctarem, finivac, ffinvac, anid_p;
                    int diasvaca;
                    if (nrodatarow.Equals(0))
                    {
                        ////////////////////////////////////////////////////////
                        /////////CABECERA DE BOLETA DE PAGO/////////////////////
                        ////////////////////////////////////////////////////////
                        descia = row_boleta["descia"].ToString() + fn.replicateCadena(" ", 40);
                        descia_va = descia;
                        nombre = row_boleta["nombre"].ToString() + fn.replicateCadena(" ", 40);
                        seccion = row_boleta["seccion"].ToString() + fn.replicateCadena(" ", 40);
                        ocupacion = row_boleta["ocupacion"].ToString() + fn.replicateCadena(" ", 40);
                        finilab = row_boleta["finilab"].ToString() + fn.replicateCadena(" ", 10);
                        ffinlab = row_boleta["ffinlab"].ToString() + fn.replicateCadena(" ", 10);
                        fechaini = row_boleta["fechaini"].ToString() + fn.replicateCadena(" ", 10);
                        fechafin = row_boleta["fechafin"].ToString() + fn.replicateCadena(" ", 10);
                        ctarem = row_boleta["tipocuentarem"].ToString() + fn.replicateCadena(" ", 1) + row_boleta["nroctarem"].ToString() + fn.replicateCadena(" ", 40);
                        finivac = row_boleta["finivac"].ToString() + fn.replicateCadena(" ", 10);
                        ffinvac = row_boleta["ffinvac"].ToString() + fn.replicateCadena(" ", 10);
                        diasvaca = int.Parse(row_boleta["diasvaca"].ToString());
                        anid_p = row_boleta["anio_vaca"].ToString();

                        strPrinter += "B O L E T A   D E   R E M U N E R A C I O N E S     " + fn.replicateCadena(" ", 5) + row_boleta["destipoplan"].ToString() + fn.replicateCadena(" ", 2) + row_boleta["decreto"].ToString() + "\n";
                        strPrinter += row_boleta["ruccia"].ToString() + fn.replicateCadena(" ", 1) + descia.Substring(0, 40) + fn.replicateCadena(" ", 5) + "PERIODO: " + row_boleta["messem"].ToString() + "/" + row_boleta["anio"].ToString() + " DEL " + fechaini.Substring(0, 10) + " AL " + fechafin.Substring(0, 10) + "\n";
                        strPrinter += "CODIGO    : " + row_boleta["idperplan"].ToString() + fn.replicateCadena(" ", 40) + "F.INGRESO : " + finilab.Substring(0, 10) + fn.replicateCadena(" ", 2) + "F.CESE : " + ffinlab.Substring(0, 10) + "\n";
                        strPrinter += "NOMBRE    : " + nombre.Substring(0, 40) + fn.replicateCadena(" ", 5) + "DOC.IDENT. :" + row_boleta["cortotipodoc"].ToString() + fn.replicateCadena(" ", 2) + row_boleta["nrodoc"].ToString() + "\n";
                        strPrinter += "SECCION   : " + seccion.Substring(0, 40) + fn.replicateCadena(" ", 5) + "FONDO PEN. :" + row_boleta["desfonpen"].ToString();

                        if (float.Parse(row_boleta["snpfonpen"].ToString()) > 0)
                        {
                            strPrinter = strPrinter + "\n";
                        }
                        else
                        {
                            strPrinter = strPrinter + fn.replicateCadena(" ", 2) + "CUSPP :" + row_boleta["cuspp"].ToString() + "\n";
                        }

                        strPrinter = strPrinter + "OCUPACION : " + ocupacion.Substring(0, 40) + fn.replicateCadena(" ", 5) + "REG.EMPLEADOR :" + row_boleta["regpatcia"].ToString() + "\n";
                        strPrinter = strPrinter + "CTA.REM   : " + ctarem.Substring(0, 40) + fn.replicateCadena(" ", 5);

                        if (diasvaca > 0)
                        {
                            strPrinter = strPrinter + "F.INI.VACA:" + finivac.Substring(0, 10) + fn.replicateCadena(" ", 1) + "F.FIN VACA: " + ffinvac.Substring(0, 10) + " P.VAC :" + anid_p.Trim() + "\n";
                        }
                        else
                        {
                            strPrinter = strPrinter + "\n";
                        }

                        strPrinter = strPrinter + fn.replicateCadena("-", 110) + "\n";
                        strPrinter = strPrinter + "H a b e r e s                                             D e s c u e n t o s              Aportaciones" + "\n";
                        strPrinter = strPrinter + fn.replicateCadena("-", 110) + "\n";


                        total_ingresos = row_boleta["total_ingresos"].ToString();
                        total_descuentos = row_boleta["total_descuentos"].ToString();
                        total_aporta = row_boleta["total_aporta"].ToString();
                        neto = row_boleta["neto"].ToString();

                        ////////////////////////////////////////////////////////
                        /////////FIN CABECERA DE BOLETA DE PAGO/////////////////////
                        ////////////////////////////////////////////////////////
                    }

                    ////////////////////////////////////////////////////////
                    /////////DETALLE BOLETA DE PAGO/////////////////////////
                    ////////////////////////////////////////////////////////
                    idclascol = row_boleta["idclascol"].ToString();
                    if (idclascol == "P")
                    {
                        row = dtpar.NewRow();
                        row["idconplanpar"] = row_boleta["idconplan"].ToString();
                        row["desboletapar"] = row_boleta["desboleta"].ToString();
                        row["valorpar"] = row_boleta["valor"].ToString();
                        row["idclascolpar"] = row_boleta["idclascol"].ToString();
                        dtpar.Rows.Add(row);
                        nrofilapar++;
                    }
                    else
                    {
                        if (idclascol == "I")
                        {
                            row = dting.NewRow();
                            row["idconplaning"] = row_boleta["idconplan"].ToString();
                            row["desboletaing"] = row_boleta["desboleta"].ToString();
                            row["valoring"] = row_boleta["valor"].ToString();
                            row["idclascoling"] = row_boleta["idclascol"].ToString();
                            dting.Rows.Add(row);
                        }
                        else
                        {
                            if (idclascol == "D")
                            {
                                row = dtdes.NewRow();
                                row["idconplandes"] = row_boleta["idconplan"].ToString();
                                row["desboletades"] = row_boleta["desboleta"].ToString();
                                row["valordes"] = row_boleta["valor"].ToString();
                                row["idclascoldes"] = row_boleta["idclascol"].ToString();
                                dtdes.Rows.Add(row);
                            }
                            else
                            {
                                row = dtapor.NewRow();
                                row["idconplanapor"] = row_boleta["idconplan"].ToString();
                                row["desboletaapor"] = row_boleta["desboleta"].ToString();
                                row["valorapor"] = row_boleta["valor"].ToString();
                                row["idclascolapor"] = row_boleta["idclascol"].ToString();
                                dtapor.Rows.Add(row);
                            }
                        }
                    }
                    nrodatarow++;
                }
                int maxFilaBol = 0;
                string bd_prov = ConfigurationManager.AppSettings.Get("BD_PROV");
                if (bd_prov.Equals("agromango")) maxFilaBol = int.Parse(configsis.consultaConfigSis("MAXFILABOL")) - 5;
                else maxFilaBol = int.Parse(configsis.consultaConfigSis("MAXFILABOL"));
                for (int x = 0; x <= (maxFilaBol - nrofilapar); x++)
                {
                    row = dtpar.NewRow();
                    row["idconplanpar"] = "";
                    row["desboletapar"] = "";
                    row["valorpar"] = "";
                    row["idclascolpar"] = "";
                    dtpar.Rows.Add(row);
                }

                DataTable dtunion01 = new DataTable();
                DataTable dtunion02 = new DataTable();
                DataTable dtdetalle = new DataTable();
                FuncionDatos funciondatos = new FuncionDatos();
                dtunion01 = funciondatos.UneTablas(dtpar, dting);
                dtunion02 = funciondatos.UneTablas(dtunion01, dtdes);
                dtdetalle = funciondatos.UneTablas(dtunion02, dtapor);

                if (dtdetalle.Rows.Count > 0)
                {
                    nrofiladetalle = 0;

                    foreach (DataRow row_dtdetalle in dtdetalle.Rows)
                    {
                        valorpar = fn.replicateCadena(" ", 8);
                        valoring = fn.replicateCadena(" ", 8);
                        valordes = fn.replicateCadena(" ", 8);
                        valorapor = fn.replicateCadena(" ", 8);

                        desboletapar = row_dtdetalle["desboletapar"].ToString() + fn.replicateCadena(" ", 18);
                        if (row_dtdetalle["valorpar"].ToString() != string.Empty)
                        {
                            valorpar = row_dtdetalle["valorpar"].ToString() + fn.replicateCadena(" ", 8);
                        }
                        desboletaing = row_dtdetalle["desboletaing"].ToString() + fn.replicateCadena(" ", 18);
                        if (row_dtdetalle["valoring"].ToString() != string.Empty)
                        {
                            valoring = float.Parse(row_dtdetalle["valoring"].ToString()).ToString("##,##0.00;(##,##0.00);Zero") + fn.replicateCadena(" ", 8);
                        }
                        desboletades = row_dtdetalle["desboletades"].ToString() + fn.replicateCadena(" ", 18);
                        if (row_dtdetalle["valordes"].ToString() != string.Empty)
                        {
                            valordes = float.Parse(row_dtdetalle["valordes"].ToString()).ToString("##,##0.00;(##,##0.00);Zero") + fn.replicateCadena(" ", 8);
                        }
                        desboletaapor = row_dtdetalle["desboletaapor"].ToString() + fn.replicateCadena(" ", 18);
                        if (row_dtdetalle["valorapor"].ToString() != string.Empty)
                        {
                            valorapor = float.Parse(row_dtdetalle["valorapor"].ToString()).ToString("##,##0.00;(##,##0.00);Zero") + fn.replicateCadena(" ", 8);
                        }
                        strPrinter = strPrinter + desboletapar.Substring(0, 18) + fn.replicateCadena(" ", 1) + valorpar.Substring(0, 8);
                        strPrinter = strPrinter + fn.replicateCadena(" ", 2) + desboletaing.Substring(0, 18) + fn.replicateCadena(" ", 1) + valoring.Substring(0, 8);
                        strPrinter = strPrinter + fn.replicateCadena(" ", 2) + desboletades.Substring(0, 18) + fn.replicateCadena(" ", 1) + valordes.Substring(0, 8);
                        strPrinter = strPrinter + fn.replicateCadena(" ", 2) + desboletaapor.Substring(0, 18) + fn.replicateCadena(" ", 1) + valorapor.Substring(0, 8) + "\n";

                        nrofiladetalle++;
                    }
                }

                strPrinter = strPrinter + fn.replicateCadena("-", 110) + "\n";
                strPrinter = strPrinter + fn.replicateCadena(" ", 27) + "Total Haberes    : " + float.Parse(total_ingresos).ToString("#,##0.00;(#,##0.00);Zero") + fn.replicateCadena(" ", 7);
                strPrinter = strPrinter + "Total Dscts     : " + float.Parse(total_descuentos).ToString("##,##0.00;(##,##0.00);Zero") + fn.replicateCadena(" ", 5);
                strPrinter = strPrinter + "Neto            : " + float.Parse(neto).ToString("##,##0.00;(##,##0.00);Zero") + "\n";
                strPrinter = strPrinter + fn.replicateCadena("-", 110) + "\n"; //+ "\n";
                strPrinter = strPrinter + fn.replicateCadena(" ", 20) + descia_va + fn.replicateCadena(" ", 15) + "\n";
                strPrinter = strPrinter + fn.replicateCadena(" ", 15) + fn.replicateCadena("-", 40) + fn.replicateCadena(" ", 15) + fn.replicateCadena("-", 15) + "\n";
                strPrinter = strPrinter + fn.replicateCadena(" ", 30) + "EMPLEADOR" + fn.replicateCadena(" ", 36) + "TRABAJADOR" + "\n";
            }

            return strPrinter;
        }

        public static byte[] ConversionImagen(string nombrearchivo)
        {

            //Declaramos fs para poder abrir la imagen.
            FileStream fs = new FileStream(nombrearchivo, FileMode.Open);

            // Declaramos un lector binario para pasar la imagen
            // a bytes
            BinaryReader br = new BinaryReader(fs);
            byte[] imagen = new byte[(int)fs.Length];
            br.Read(imagen, 0, (int)fs.Length);
            br.Close();
            fs.Close();

            return imagen;
        }

        public DataTable generaBoletasWin(string idtipoper, string messem, string anio, string idtipocal, string idtipoplan, string idcia, string rucemp, string estane)
        {
            Boleta boleta = new Boleta();
            DataTable dtbol = new DataTable();
            DataTable dtper = new DataTable();
            DataRow row;
            DataColumn column;
            ////////////////////////CABECERA ///////////////////////////////////////
            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "idperplan";
            column.ReadOnly = true;
            dtbol.Columns.Add(column);
            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "idcia";
            column.ReadOnly = true;
            dtbol.Columns.Add(column);
            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "descia";
            column.ReadOnly = true;
            dtbol.Columns.Add(column);
            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "nombre";
            column.ReadOnly = true;
            dtbol.Columns.Add(column);
            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "seccion";
            column.ReadOnly = true;
            dtbol.Columns.Add(column);
            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "ocupacion";
            column.ReadOnly = true;
            dtbol.Columns.Add(column);
            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "finilab";
            column.ReadOnly = true;
            dtbol.Columns.Add(column);
            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "ffinlab";
            column.ReadOnly = true;
            dtbol.Columns.Add(column);
            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "finivac";
            column.ReadOnly = true;
            dtbol.Columns.Add(column);
            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "ffinvac";
            column.ReadOnly = true;
            dtbol.Columns.Add(column);
            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "fechaini";
            column.ReadOnly = true;
            dtbol.Columns.Add(column);
            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "fechafin";
            column.ReadOnly = true;
            dtbol.Columns.Add(column);
            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "diasvaca";
            column.ReadOnly = true;
            dtbol.Columns.Add(column);
            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "ctarem";
            column.ReadOnly = true;
            dtbol.Columns.Add(column);
            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "nroctarem";
            column.ReadOnly = true;
            dtbol.Columns.Add(column);
            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "destipoper";
            column.ReadOnly = true;
            dtbol.Columns.Add(column);
            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "cortotipodoc";
            column.ReadOnly = true;
            dtbol.Columns.Add(column);
            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "nrodoc";
            column.ReadOnly = true;
            dtbol.Columns.Add(column);
            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "anio";
            column.ReadOnly = true;
            dtbol.Columns.Add(column);
            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "messem";
            column.ReadOnly = true;
            dtbol.Columns.Add(column);
            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "ruccia";
            column.ReadOnly = true;
            dtbol.Columns.Add(column);
            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "desfonpen";
            column.ReadOnly = true;
            dtbol.Columns.Add(column);
            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "cuspp";
            column.ReadOnly = true;
            dtbol.Columns.Add(column);
            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "destipoplan";
            column.ReadOnly = true;
            dtbol.Columns.Add(column);
            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "decreto";
            column.ReadOnly = true;
            dtbol.Columns.Add(column);
            column = new DataColumn();
            column.DataType = System.Type.GetType("System.Byte[]");
            column.ColumnName = "logo";
            column.ReadOnly = true;
            dtbol.Columns.Add(column);

            ///////////////////////////PARAMETROS/////////////////////////////////////
            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "idconplanpar";
            column.ReadOnly = true;
            dtbol.Columns.Add(column);
            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "desboletapar";
            column.ReadOnly = true;
            dtbol.Columns.Add(column);
            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "valorpar";
            column.ReadOnly = true;
            dtbol.Columns.Add(column);
            ///////////////////////////INGRESOS///////////////////////////////////////
            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "idconplaning";
            column.ReadOnly = true;
            dtbol.Columns.Add(column);
            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "desboletaing";
            column.ReadOnly = true;
            dtbol.Columns.Add(column);
            column = new DataColumn();
            column.DataType = System.Type.GetType("System.Decimal");
            column.ColumnName = "valoring";
            column.ReadOnly = true;
            dtbol.Columns.Add(column);
            ////////////////////////DESCUENTOS/////////////////////////////////////
            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "idconplandes";
            column.ReadOnly = true;
            dtbol.Columns.Add(column);
            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "desboletades";
            column.ReadOnly = true;
            dtbol.Columns.Add(column);
            column = new DataColumn();
            column.DataType = System.Type.GetType("System.Decimal");
            column.ColumnName = "valordes";
            column.ReadOnly = true;
            dtbol.Columns.Add(column);
            ///////////////////////APORTACIONES////////////////////////////////
            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "idconplanapor";
            column.ReadOnly = true;
            dtbol.Columns.Add(column);
            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "desboletaapor";
            column.ReadOnly = true;
            dtbol.Columns.Add(column);
            column = new DataColumn();
            column.DataType = System.Type.GetType("System.Decimal");
            column.ColumnName = "valorapor";
            column.ReadOnly = true;
            dtbol.Columns.Add(column);
            column = new DataColumn();
            column.DataType = System.Type.GetType("System.Decimal");
            column.ColumnName = "neto";
            column.ReadOnly = true;
            dtbol.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "anio_vaca";
            column.ReadOnly = true;
            dtbol.Columns.Add(column);

            string query = " Select P.idperplan,CONCAT(A.apepat,' ',A.apemat,' , ',A.nombres) as nombre,P.neto ";
            query = query + " from plan_ P inner join perplan A on P.idcia=A.idcia ";
            query = query + " and P.idperplan=A.idperplan where P.rucemp='" + @rucemp + "' and ";
            query = query + " P.estane='" + @estane + "' and P.anio='" + @anio + "' and ";
            query = query + " P.messem='" + @messem + "' and P.idcia='" + @idcia + "' ";
            query = query + " and P.idtipoper='" + @idtipoper + "' ";
            query = query + " and P.idtipoplan='" + @idtipoplan + "' and P.idtipocal='" + @idtipocal + "' ";
            query = query + " order by nombre asc;";
            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = new SqlCommand(query, conexion);
            da.Fill(dtper);

            if (dtper.Rows.Count > 0)
            {
                foreach (DataRow row_dtper in dtper.Rows)
                {
                    string idperplan = row_dtper["idperplan"].ToString();

                    DataTable dtconper = new DataTable();
                    dtconper = boleta.generaDataBoletaWin(idperplan, anio, messem, idcia, idtipoper, idtipoplan, idtipocal);

                    foreach (DataRow row_dtconper in dtconper.Rows)
                    {


                        row = dtbol.NewRow();
                        row["idperplan"] = row_dtconper["idperplan"].ToString();
                        row["idcia"] = row_dtconper["idcia"].ToString();
                        row["descia"] = row_dtconper["descia"].ToString();
                        row["nombre"] = row_dtconper["nombre"].ToString();
                        row["seccion"] = row_dtconper["seccion"].ToString();
                        row["ocupacion"] = row_dtconper["ocupacion"].ToString();
                        row["finilab"] = row_dtconper["finilab"].ToString();
                        row["ffinlab"] = row_dtconper["ffinlab"].ToString();
                        row["finivac"] = row_dtconper["finivac"].ToString();
                        row["ffinvac"] = row_dtconper["ffinvac"].ToString();
                        row["fechaini"] = row_dtconper["fechaini"].ToString();
                        row["fechafin"] = row_dtconper["fechafin"].ToString();
                        row["diasvaca"] = row_dtconper["diasvaca"].ToString();
                        row["ctarem"] = row_dtconper["ctarem"].ToString();
                        row["nroctarem"] = row_dtconper["nroctarem"].ToString();
                        row["destipoper"] = row_dtconper["destipoper"].ToString();
                        row["cortotipodoc"] = row_dtconper["cortotipodoc"].ToString();
                        row["nrodoc"] = row_dtconper["nrodoc"].ToString();
                        row["anio"] = row_dtconper["anio"].ToString();
                        row["messem"] = row_dtconper["messem"].ToString();
                        row["ruccia"] = row_dtconper["ruccia"].ToString();
                        row["desfonpen"] = row_dtconper["desfonpen"].ToString();
                        row["cuspp"] = row_dtconper["cuspp"].ToString();
                        row["destipoplan"] = row_dtconper["destipoplan"].ToString();
                        row["decreto"] = row_dtconper["decreto"].ToString();
                        row["idconplanpar"] = row_dtconper["idconplanpar"].ToString();
                        row["desboletapar"] = row_dtconper["desboletapar"].ToString();
                        row["valorpar"] = row_dtconper["valorpar"].ToString();
                        row["idconplaning"] = row_dtconper["idconplaning"].ToString();
                        row["desboletaing"] = row_dtconper["desboletaing"].ToString();
                        row["valoring"] = float.Parse(row_dtconper["valoring"].ToString());
                        row["idconplandes"] = row_dtconper["idconplandes"].ToString();
                        row["desboletades"] = row_dtconper["desboletades"].ToString();
                        row["valordes"] = float.Parse(row_dtconper["valordes"].ToString());
                        row["idconplanapor"] = row_dtconper["idconplanapor"].ToString();
                        row["desboletaapor"] = row_dtconper["desboletaapor"].ToString();
                        row["valorapor"] = float.Parse(row_dtconper["valorapor"].ToString());
                        row["neto"] = float.Parse(row_dtconper["neto"].ToString());
                        row["logo"] = row_dtconper["logo"];
                        row["anio_vaca"] = row_dtconper["anio_vaca"].ToString();

                        dtbol.Rows.Add(row);

                    }
                }
            }
            return dtbol;
        }

        public DataTable generaDataBoletaWin(string idperplan, string anio, string messem, string idcia, string idtipoper, string idtipoplan, string idtipocal)
        {
            Funciones fn = new Funciones();
            Configsis configsis = new Configsis();
            DataTable dtdetalle = new DataTable();
            DataTable dtpar = new DataTable();
            DataTable dting = new DataTable();
            DataTable dtdes = new DataTable();
            DataTable dtapor = new DataTable();
            DataTable dtcab = new DataTable();
            DataRow row;
            DataColumn column;

            ///////////////////////////PARAMETROS/////////////////////////////////////
            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "idconplanpar";
            column.ReadOnly = true;
            dtpar.Columns.Add(column);
            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "desboletapar";
            column.ReadOnly = true;
            dtpar.Columns.Add(column);
            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "valorpar";
            column.ReadOnly = true;
            dtpar.Columns.Add(column);
            ///////////////////////////INGRESOS///////////////////////////////////////
            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "idconplaning";
            column.ReadOnly = true;
            dting.Columns.Add(column);
            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "desboletaing";
            column.ReadOnly = true;
            dting.Columns.Add(column);
            column = new DataColumn();
            column.DataType = System.Type.GetType("System.Decimal");
            column.ColumnName = "valoring";
            column.ReadOnly = true;
            dting.Columns.Add(column);
            ////////////////////////DESCUENTOS/////////////////////////////////////
            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "idconplandes";
            column.ReadOnly = true;
            dtdes.Columns.Add(column);
            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "desboletades";
            column.ReadOnly = true;
            dtdes.Columns.Add(column);
            column = new DataColumn();
            column.DataType = System.Type.GetType("System.Decimal");
            column.ColumnName = "valordes";
            column.ReadOnly = true;
            dtdes.Columns.Add(column);
            ///////////////////////APORTACIONES////////////////////////////////
            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "idconplanapor";
            column.ReadOnly = true;
            dtapor.Columns.Add(column);
            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "desboletaapor";
            column.ReadOnly = true;
            dtapor.Columns.Add(column);
            column = new DataColumn();
            column.DataType = System.Type.GetType("System.Decimal");
            column.ColumnName = "valorapor";
            column.ReadOnly = true;
            dtapor.Columns.Add(column);

            ////////////////////////CABECERA ///////////////////////////////////////
            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "idperplan";
            column.ReadOnly = true;
            dtcab.Columns.Add(column);
            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "idcia";
            column.ReadOnly = true;
            dtcab.Columns.Add(column);
            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "descia";
            column.ReadOnly = true;
            dtcab.Columns.Add(column);
            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "nombre";
            column.ReadOnly = true;
            dtcab.Columns.Add(column);
            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "seccion";
            column.ReadOnly = true;
            dtcab.Columns.Add(column);
            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "ocupacion";
            column.ReadOnly = true;
            dtcab.Columns.Add(column);
            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "finilab";
            column.ReadOnly = true;
            dtcab.Columns.Add(column);
            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "ffinlab";
            column.ReadOnly = true;
            dtcab.Columns.Add(column);
            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "finivac";
            column.ReadOnly = true;
            dtcab.Columns.Add(column);
            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "ffinvac";
            column.ReadOnly = true;
            dtcab.Columns.Add(column);
            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "fechaini";
            column.ReadOnly = true;
            dtcab.Columns.Add(column);
            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "fechafin";
            column.ReadOnly = true;
            dtcab.Columns.Add(column);
            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "diasvaca";
            column.ReadOnly = true;
            dtcab.Columns.Add(column);
            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "ctarem";
            column.ReadOnly = true;
            dtcab.Columns.Add(column);
            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "nroctarem";
            column.ReadOnly = true;
            dtcab.Columns.Add(column);
            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "destipoper";
            column.ReadOnly = true;
            dtcab.Columns.Add(column);
            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "cortotipodoc";
            column.ReadOnly = true;
            dtcab.Columns.Add(column);
            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "nrodoc";
            column.ReadOnly = true;
            dtcab.Columns.Add(column);
            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "anio";
            column.ReadOnly = true;
            dtcab.Columns.Add(column);
            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "messem";
            column.ReadOnly = true;
            dtcab.Columns.Add(column);
            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "ruccia";
            column.ReadOnly = true;
            dtcab.Columns.Add(column);
            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "desfonpen";
            column.ReadOnly = true;
            dtcab.Columns.Add(column);
            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "cuspp";
            column.ReadOnly = true;
            dtcab.Columns.Add(column);
            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "destipoplan";
            column.ReadOnly = true;
            dtcab.Columns.Add(column);
            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "decreto";
            column.ReadOnly = true;
            dtcab.Columns.Add(column);
            column = new DataColumn();
            column.DataType = System.Type.GetType("System.Decimal");
            column.ColumnName = "neto";
            column.ReadOnly = true;
            dtcab.Columns.Add(column);
            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "ruta";
            column.ReadOnly = true;
            dtcab.Columns.Add(column);
            column = new DataColumn();
            column.DataType = System.Type.GetType("System.Byte[]");
            column.ColumnName = "logo";
            column.ReadOnly = true;
            dtcab.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "anio_vaca";
            column.ReadOnly = true;
            dtcab.Columns.Add(column);


            DataTable boleta = new DataTable();

            //string query = " Select A.idperplan,B.destipoper,C.Parm1maesgen as cortotipodoc,A.nrodoc,A.cuspp, ";
            //query = query + " CONCAT(CONCAT(CONCAT(A.apepat,' '),CONCAT(A.apemat,' , ')),A.nombres) as nombre,D.desmaesgen as seccion, ";
            //query = query + " E.deslabper as ocupacion,P.finilab,P.ffinlab,round(P.total_ingresos,2) as total_ingresos,round(P.total_descuentos,2) as total_descuentos, ";
            //query = query + " round(P.total_aporta,2) as total_aporta,round(P.neto,2) as neto,P.finivac,P.ffinvac,P.diasvaca, ";
            //query = query + " X.idconplan,Z.desboleta,M.idclascol,Y.desclascol,round(X.valor,2) as valor,A.idcia,Q.descia,Q.ruccia,S.fechaini,S.fechafin, ";
            //query = query + " T.desfonpen,U.destipoplan,U.decreto,P.messem,P.anio,T.snpfonpen, ";
            //query = query + " Q.regpatcia,CONCAT(F.Parm1maesgen,' ',G.Parm1maesgen,' ',A.monrem) ";
            //query = query + " as tipocuentarem,A.nroctarem,P.neto,Q.logo,ifnull(dtp.pervac,' ') as anio_vaca from plan_ P  inner join conbol X on ";
            //query = query + " P.idperplan=X.idperplan and P.idcia=X.idcia ";
            //query = query + " and P.anio=X.anio and P.messem=X.messem and P.idtipoper=X.idtipoper ";
            //query = query + " and P.idtipocal=X.idtipocal and P.idtipoplan=X.idtipoplan ";
            //query = query + " inner join detconplan Z on X.idcia=Z.idcia and ";
            //query = query + " X.idtipoplan=Z.idtipoplan and X.idtipocal=Z.idtipocal and X.idtipoper=Z.idtipoper ";
            //query = query + " and X.idconplan=Z.idconplan";
            //query = query + " inner join conplan M on M.idcia=Z.idcia and ";
            //query = query + " M.idtipoplan=Z.idtipoplan and M.idtipocal=Z.idtipocal  ";
            //query = query + " and M.idconplan=Z.idconplan";
            //query = query + " left join clascol Y on M.idclascol=Y.idclascol ";
            //query = query + " left join labper E on P.idcia=E.idcia and P.idlabper=E.idlabper and P.idtipoper=E.idtipoper ";
            //query = query + " inner join ciafile Q on P.idcia=Q.idcia ";
            //query = query + " inner join calplan S on P.anio=S.anio and P.messem=S.messem and P.idtipoper=S.idtipoper ";
            //query = query + " and P.idtipocal=S.idtipocal and P.idcia=S.idcia ";
            //query = query + " left join fonpen T on P.idfonpen=T.idfonpen ";
            //query = query + " left join maesgen D on D.idmaesgen='008' and P.seccion=D.clavemaesgen ";
            //query = query + " inner join tipoplan U on P.idtipoplan=U.idtipoplan  ";
            //query = query + " inner join perplan A on P.idcia=A.idcia and P.idperplan=A.idperplan ";
            //query = query + " left join tipoper B on A.idtipoper=B.idtipoper ";
            //query = query + " left join maesgen C on C.idmaesgen='002' and A.tipdoc=C.clavemaesgen  ";
            //query = query + " left join maesgen F on F.idmaesgen='007' and A.entfinrem=F.clavemaesgen ";
            //query = query + " left join maesgen G on G.idmaesgen='014' and A.tipctarem=G.clavemaesgen ";
            //query = query + " left join dataplan dtp on dtp.finivac=P.finivac and dtp.ffinvac=P.ffinvac and dtp.idperplan=P.idperplan and dtp.idcia=P.idcia and  P.anio=dtp.anio and P.messem=dtp.messem";
            //query = query + " where A.idperplan='" + @idperplan + "' and P.anio='" + @anio + "' and P.messem='" + @messem + "' and P.idcia='" + @idcia + "' and P.idtipoper='" + @idtipoper + "' ";
            //query = query + " and P.idtipoplan='" + @idtipoplan + "' and P.idtipocal='" + @idtipocal + "' and Z.imprime='SI' and X.valor>0 order by P.idperplan asc,Y.orden asc,X.idconplan asc;";
            String query = " call generaBoletaTabular('" + @idperplan + "','" + @anio + "','" + @messem + "','" + @idcia + "','" + @idtipoper + "','" + @idtipoplan + "','" + @idtipocal + "','')";
            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = new SqlCommand(query, conexion);
            da.Fill(boleta);

            int nrofilapar = 0;
            int nrofilaing = 0;
            int nrofilades = 0;
            int nrofilaapor = 0;

            if (boleta.Rows.Count > 0)
            {
                int nrodatarow = 0;

                foreach (DataRow row_boleta in boleta.Rows)
                {

                    if (nrodatarow.Equals(0))
                    {
                        for (int x = 0; x <= int.Parse(configsis.consultaConfigSis("MAXFILABOLWIN")); x++)
                        {

                            Byte[] logo = new Byte[10];
                            string ruta = string.Empty;

                            ruta = row_boleta["logo"].ToString();
                            if (ruta.Trim() != string.Empty)
                            {
                                FileInfo fa = new FileInfo(ruta);
                                if (fa.Exists)
                                {
                                    logo = ConversionImagen(ruta);
                                }
                            }

                            row = dtcab.NewRow();
                            row["idperplan"] = row_boleta["idperplan"].ToString();
                            row["idcia"] = row_boleta["idcia"].ToString();
                            row["descia"] = row_boleta["descia"].ToString();
                            row["nombre"] = row_boleta["nombre"].ToString();
                            row["seccion"] = row_boleta["seccion"].ToString();
                            row["ocupacion"] = row_boleta["ocupacion"].ToString();
                            row["finilab"] = row_boleta["finilab"].ToString();
                            row["ffinlab"] = row_boleta["ffinlab"].ToString();
                            row["fechaini"] = row_boleta["fechaini"].ToString();
                            row["fechafin"] = row_boleta["fechafin"].ToString();
                            row["ctarem"] = row_boleta["tipocuentarem"].ToString();
                            row["nroctarem"] = row_boleta["nroctarem"].ToString();
                            row["finivac"] = row_boleta["finivac"].ToString();
                            row["ffinvac"] = row_boleta["ffinvac"].ToString();
                            row["diasvaca"] = row_boleta["diasvaca"].ToString();
                            row["destipoper"] = row_boleta["destipoper"].ToString();
                            row["cortotipodoc"] = row_boleta["cortotipodoc"].ToString();
                            row["nrodoc"] = row_boleta["nrodoc"].ToString();
                            row["anio"] = row_boleta["anio"].ToString();
                            row["messem"] = row_boleta["messem"].ToString();
                            row["ruccia"] = row_boleta["ruccia"].ToString();
                            row["desfonpen"] = row_boleta["desfonpen"].ToString();
                            row["cuspp"] = row_boleta["cuspp"].ToString();
                            row["destipoplan"] = row_boleta["destipoplan"].ToString();
                            row["decreto"] = row_boleta["decreto"].ToString();
                            row["neto"] = row_boleta["neto"].ToString();
                            row["ruta"] = row_boleta["logo"].ToString();
                            row["logo"] = logo;
                            row["anio_vaca"] = row_boleta["anio_vaca"].ToString();

                            dtcab.Rows.Add(row);
                        }
                    }

                    ////////////////////////////////////////////////////////
                    /////////DETALLE BOLETA DE PAGO/////////////////////////
                    ////////////////////////////////////////////////////////

                    string idclascol = row_boleta["idclascol"].ToString();
                    if (idclascol == "P")
                    {
                        row = dtpar.NewRow();
                        row["idconplanpar"] = row_boleta["idconplan"].ToString();
                        row["desboletapar"] = row_boleta["desboleta"].ToString();
                        row["valorpar"] = row_boleta["valor"].ToString();
                        dtpar.Rows.Add(row);
                        nrofilapar++;
                    }
                    else
                    {
                        if (idclascol == "I")
                        {
                            row = dting.NewRow();
                            row["idconplaning"] = row_boleta["idconplan"].ToString();
                            row["desboletaing"] = row_boleta["desboleta"].ToString();
                            row["valoring"] = float.Parse(row_boleta["valor"].ToString());
                            dting.Rows.Add(row);
                            nrofilaing++;
                        }
                        else
                        {
                            if (idclascol == "D")
                            {
                                row = dtdes.NewRow();
                                row["idconplandes"] = row_boleta["idconplan"].ToString();
                                row["desboletades"] = row_boleta["desboleta"].ToString();
                                row["valordes"] = float.Parse(row_boleta["valor"].ToString());
                                dtdes.Rows.Add(row);
                                nrofilades++;
                            }
                            else
                            {
                                row = dtapor.NewRow();
                                row["idconplanapor"] = row_boleta["idconplan"].ToString();
                                row["desboletaapor"] = row_boleta["desboleta"].ToString();
                                row["valorapor"] = float.Parse(row_boleta["valor"].ToString());
                                dtapor.Rows.Add(row);
                                nrofilaapor++;
                            }
                        }
                    }
                    nrodatarow++;
                }

                for (int x = 0; x <= (int.Parse(configsis.consultaConfigSis("MAXFILABOLWIN")) - nrofilapar); x++)
                {
                    row = dtpar.NewRow();
                    row["idconplanpar"] = "";
                    row["desboletapar"] = "";
                    row["valorpar"] = "";
                    dtpar.Rows.Add(row);
                }

                for (int x = 0; x <= (int.Parse(configsis.consultaConfigSis("MAXFILABOLWIN")) - nrofilaing); x++)
                {
                    row = dting.NewRow();
                    row["idconplaning"] = "";
                    row["desboletaing"] = "";
                    row["valoring"] = 0;
                    dting.Rows.Add(row);
                }

                for (int x = 0; x <= (int.Parse(configsis.consultaConfigSis("MAXFILABOLWIN")) - nrofilades); x++)
                {
                    row = dtdes.NewRow();
                    row["idconplandes"] = "";
                    row["desboletades"] = "";
                    row["valordes"] = 0;
                    dtdes.Rows.Add(row);
                }

                for (int x = 0; x <= (int.Parse(configsis.consultaConfigSis("MAXFILABOLWIN")) - nrofilaapor); x++)
                {
                    row = dtapor.NewRow();
                    row["idconplanapor"] = "";
                    row["desboletaapor"] = "";
                    row["valorapor"] = 0;
                    dtapor.Rows.Add(row);
                }

                DataTable dtunion00 = new DataTable();
                DataTable dtunion01 = new DataTable();
                DataTable dtunion02 = new DataTable();

                FuncionDatos funciondatos = new FuncionDatos();
                dtunion00 = funciondatos.UneTablas(dtcab, dtpar);
                dtunion01 = funciondatos.UneTablas(dtunion00, dting);
                dtunion02 = funciondatos.UneTablas(dtunion01, dtdes);
                dtdetalle = funciondatos.UneTablas(dtunion02, dtapor);


            }

            return dtdetalle;
        }
    }
}