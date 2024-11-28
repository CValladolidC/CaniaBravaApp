using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;

namespace CaniaBrava
{
    class Factura
    {
        public DataTable generaFacturasWin(string codcia, string rftdoc, string rfserie, string rfnroini, string rfnrofin)
        {
            DataTable dtfacturas = new DataTable();
            DataTable dtcompro = new DataTable();
            DataRow row;
            DataColumn column;

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "alma";
            column.ReadOnly = true;
            dtcompro.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "td";
            column.ReadOnly = true;
            dtcompro.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "numdoc";
            column.ReadOnly = true;
            dtcompro.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "fecdoc";
            column.ReadOnly = true;
            dtcompro.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "rftdoc";
            column.ReadOnly = true;
            dtcompro.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "rfndoc";
            column.ReadOnly = true;
            dtcompro.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "codmon";
            column.ReadOnly = true;
            dtcompro.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "glosa1";
            column.ReadOnly = true;
            dtcompro.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "codclie";
            column.ReadOnly = true;
            dtcompro.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "desclie";
            column.ReadOnly = true;
            dtcompro.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "rucclie";
            column.ReadOnly = true;
            dtcompro.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "dniclie";
            column.ReadOnly = true;
            dtcompro.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "dirclie1";
            column.ReadOnly = true;
            dtcompro.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "dirclie2";
            column.ReadOnly = true;
            dtcompro.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "dirclie3";
            column.ReadOnly = true;
            dtcompro.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.Decimal");
            column.ColumnName = "tipcam";
            column.ReadOnly = true;
            dtcompro.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "usuario";
            column.ReadOnly = true;
            dtcompro.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "item";
            column.ReadOnly = true;
            dtcompro.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "codarti";
            column.ReadOnly = true;
            dtcompro.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "desarti";
            column.ReadOnly = true;
            dtcompro.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.Decimal");
            column.ColumnName = "cantidad";
            column.ReadOnly = true;
            dtcompro.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.Decimal");
            column.ColumnName = "preuni";
            column.ReadOnly = true;
            dtcompro.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.Decimal");
            column.ColumnName = "descuento";
            column.ReadOnly = true;
            dtcompro.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.Decimal");
            column.ColumnName = "neto";
            column.ReadOnly = true;
            dtcompro.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.Decimal");
            column.ColumnName = "igv";
            column.ReadOnly = true;
            dtcompro.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.Decimal");
            column.ColumnName = "total";
            column.ReadOnly = true;
            dtcompro.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "unidad";
            column.ReadOnly = true;
            dtcompro.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.Decimal");
            column.ColumnName = "totneto";
            column.ReadOnly = true;
            dtcompro.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.Decimal");
            column.ColumnName = "totigv";
            column.ReadOnly = true;
            dtcompro.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.Decimal");
            column.ColumnName = "tottotal";
            column.ReadOnly = true;
            dtcompro.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "cadtotal";
            column.ReadOnly = true;
            dtcompro.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "rfnrooc";
            column.ReadOnly = true;
            dtcompro.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "rfnguia";
            column.ReadOnly = true;
            dtcompro.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "fven";
            column.ReadOnly = true;
            dtcompro.Columns.Add(column);

            //string query = " select A.codcia,A.rftdoc,A.rfnro,A.rfserie from almovc A ";
            //query = query + " WHERE A.codcia='" + @codcia + "' and A.situafac='F' and A.flag in ('FTCS','GRCS') and ";
            //query = query + " A.rftdoc='" + @rftdoc + "' ";
            //query = query + " and A.rfserie='" + @rfserie + "' ";
            //query = query + " and A.rfnro between '" + @rfnroini + "' and '" + @rfnrofin + "' ";
            //query = query + " order by A.rftdoc asc,A.rfnro asc,A.rfserie asc ; ";

            string query = "call SelecFacturasWin('" + @codcia + "','" + @rfserie + "','" + @rftdoc + "','" + @rfnroini + "','" + @rfnrofin + "')";
            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = new SqlCommand(query, conexion);
            da.Fill(dtfacturas);

            if (dtfacturas.Rows.Count > 0)
            {
                foreach (DataRow row_factura in dtfacturas.Rows)
                {
                    string rfnro = row_factura["rfnro"].ToString();

                    DataTable dttmp = new DataTable();
                    dttmp = generaDataFactura(codcia, rftdoc, rfserie, rfnro);

                    foreach (DataRow row_tmp in dttmp.Rows)
                    {
                        row = dtcompro.NewRow();
                        row["alma"] = row_tmp["alma"].ToString();
                        row["td"] = row_tmp["td"].ToString();
                        row["numdoc"] = row_tmp["numdoc"].ToString();
                        row["fecdoc"] = row_tmp["fecdoc"].ToString();
                        row["rftdoc"] = row_tmp["rftdoc"].ToString();
                        row["rfndoc"] = row_tmp["rfndoc"].ToString();
                        row["codmon"] = row_tmp["codmon"].ToString();
                        row["glosa1"] = row_tmp["glosa1"].ToString();
                        row["codclie"] = row_tmp["codclie"].ToString();
                        row["desclie"] = row_tmp["desclie"].ToString();
                        row["rucclie"] = row_tmp["rucclie"].ToString();
                        row["dniclie"] = row_tmp["dniclie"].ToString();
                        row["dirclie1"] = row_tmp["dirclie1"].ToString();
                        row["dirclie2"] = row_tmp["dirclie2"].ToString();
                        row["dirclie3"] = row_tmp["dirclie3"].ToString();
                        row["tipcam"] = float.Parse(row_tmp["tipcam"].ToString());
                        row["usuario"] = row_tmp["usuario"].ToString();

                        row["rfnrooc"] = row_tmp["rfnrooc"].ToString();
                        row["rfnguia"] = row_tmp["rfnguia"].ToString();

                        row["item"] = row_tmp["item"].ToString();
                        row["codarti"] = row_tmp["codarti"].ToString();
                        row["desarti"] = row_tmp["desarti"].ToString();
                        row["cantidad"] = float.Parse(row_tmp["cantidad"].ToString());
                        row["preuni"] = float.Parse(row_tmp["preuni"].ToString());
                        row["descuento"] = float.Parse(row_tmp["descuento"].ToString());
                        row["neto"] = float.Parse(row_tmp["neto"].ToString());
                        row["igv"] = float.Parse(row_tmp["igv"].ToString());
                        row["total"] = float.Parse(row_tmp["total"].ToString());
                        row["unidad"] = row_tmp["unidad"].ToString();
                        row["totneto"] = float.Parse(row_tmp["totneto"].ToString());
                        row["totigv"] = float.Parse(row_tmp["totigv"].ToString());
                        row["tottotal"] = float.Parse(row_tmp["tottotal"].ToString());
                        row["cadtotal"] = row_tmp["cadtotal"].ToString();

                        dtcompro.Rows.Add(row);
                    }
                }
            }
            return dtcompro;

        }

       


        public DataTable generaDataFactura(string codcia, string rftdoc, string rfserie, string rfnro)
        {
            string query;
            int nrodatarow;
            float totneto = 0;
            float totigv = 0;
            decimal tottotal = 0;
            string cadtotal = string.Empty;

            SisParm sisparm = new SisParm();
            DataTable dtfactura = new DataTable();
            DataTable dtcompro = new DataTable();
            DataRow row;
            DataColumn column;

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "alma";
            column.ReadOnly = true;
            dtcompro.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "td";
            column.ReadOnly = true;
            dtcompro.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "numdoc";
            column.ReadOnly = true;
            dtcompro.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "fecdoc";
            column.ReadOnly = true;
            dtcompro.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "rftdoc";
            column.ReadOnly = true;
            dtcompro.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "rfndoc";
            column.ReadOnly = true;
            dtcompro.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "codmon";
            column.ReadOnly = true;
            dtcompro.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "glosa1";
            column.ReadOnly = true;
            dtcompro.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "codclie";
            column.ReadOnly = true;
            dtcompro.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "desclie";
            column.ReadOnly = true;
            dtcompro.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "rucclie";
            column.ReadOnly = true;
            dtcompro.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "dniclie";
            column.ReadOnly = true;
            dtcompro.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "dirclie1";
            column.ReadOnly = true;
            dtcompro.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "dirclie2";
            column.ReadOnly = true;
            dtcompro.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "dirclie3";
            column.ReadOnly = true;
            dtcompro.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.Decimal");
            column.ColumnName = "tipcam";
            column.ReadOnly = true;
            dtcompro.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "usuario";
            column.ReadOnly = true;
            dtcompro.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "item";
            column.ReadOnly = true;
            dtcompro.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "codarti";
            column.ReadOnly = true;
            dtcompro.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "desarti";
            column.ReadOnly = true;
            dtcompro.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.Decimal");
            column.ColumnName = "cantidad";
            column.ReadOnly = true;
            dtcompro.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.Decimal");
            column.ColumnName = "preuni";
            column.ReadOnly = true;
            dtcompro.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.Decimal");
            column.ColumnName = "descuento";
            column.ReadOnly = true;
            dtcompro.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.Decimal");
            column.ColumnName = "neto";
            column.ReadOnly = true;
            dtcompro.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.Decimal");
            column.ColumnName = "igv";
            column.ReadOnly = true;
            dtcompro.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.Decimal");
            column.ColumnName = "total";
            column.ReadOnly = true;
            dtcompro.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "unidad";
            column.ReadOnly = true;
            dtcompro.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.Decimal");
            column.ColumnName = "totneto";
            column.ReadOnly = true;
            dtcompro.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.Decimal");
            column.ColumnName = "totigv";
            column.ReadOnly = true;
            dtcompro.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.Decimal");
            column.ColumnName = "tottotal";
            column.ReadOnly = true;
            dtcompro.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "cadtotal";
            column.ReadOnly = true;
            dtcompro.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "rfnrooc";
            column.ReadOnly = true;
            dtcompro.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "rfnguia";
            column.ReadOnly = true;
            dtcompro.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "fven";
            column.ReadOnly = true;
            dtcompro.Columns.Add(column);

            string almafac = string.Empty;
            string tdfac = string.Empty;
            string numdocfac = string.Empty;
            string fecdoc = string.Empty;
            string rfndoc = string.Empty;
            string codmon = string.Empty;
            string glosa1 = string.Empty;
            string codclie = string.Empty;
            string desclie = string.Empty;
            string rucclie = string.Empty;
            string dniclie = string.Empty;
            string dirclie1 = string.Empty;
            string dirclie2 = string.Empty;
            string dirclie3 = string.Empty;
            string rfnrooc = string.Empty;
            string rfnguia = string.Empty;
            float tipcam = 0;
            string usuario = string.Empty;
            string fecven = string.Empty; 

            //query = " select A.alma,A.td,A.numdoc,A.fecdoc,A.rftdoc,A.rfndoc, ";
            //query = query + " A.codmon,A.glosa1,A.codclie,H.desclie,H.rucclie,H.dniclie,";
            //query = query + " H.dirclie1,H.dirclie2,H.dirclie3,A.tipcam,A.usuario,J.item,J.codarti,K.desarti, ";
            //query = query + " J.cantidad,J.preuni,J.descuento,J.neto,J.igv,J.total,K.unidad,A.rfnrooc,A.rfnguia,J.fven from almovc A ";
            //query = query + " left join clie H on A.codclie=H.codclie ";
            //query = query + " left join almovd J on A.codcia=J.codcia and A.alma=J.alma and A.td=J.td and A.numdoc=J.numdoc ";
            //query = query + " left join alarti K on J.codcia=K.codcia and J.codarti=K.codarti ";
            //query = query + " WHERE A.codcia='" + @codcia + "' and A.rftdoc='" + @rftdoc + "' ";
            //query = query + " and A.rfserie='" + @rfserie + "' and A.rfnro='" + @rfnro + "' ";
            //query = query + " order by J.item asc ; ";


            query = "call GeneraDatafactura('" + @codcia + "','" + @rftdoc + "','" + @rfserie + "','" + @rfnro + "')";
            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();

            SqlDataAdapter da = new SqlDataAdapter();


            da.SelectCommand = new SqlCommand(query, conexion);
            da.Fill(dtfactura);

            if (dtfactura.Rows.Count > 0)
            {

                totneto = 0;
                totigv = 0;
                tottotal = 0;
                foreach (DataRow row_factura in dtfactura.Rows)
                {
                    totneto += float.Parse(row_factura["neto"].ToString());
                    totigv += float.Parse(row_factura["igv"].ToString());
                    tottotal += decimal.Parse(row_factura["total"].ToString());
                    codmon = row_factura["codmon"].ToString();
                }

                
                string stotal = ((int)(tottotal)).ToString();
                int ntotal = (int)(tottotal);
                NumLetras numletras = new NumLetras();
                string cadentero = numletras.Convierte(stotal, NumLetras.Tipo.Pronombre);
                Funciones funciones = new Funciones();
                decimal ndecimal= funciones.getDecimalRound(tottotal - ntotal, 2);
                string caddecimal = ((int)((ndecimal) * 100)).ToString() + "/100";

                MaesGen maesgen = new MaesGen();
                string cadmoneda = maesgen.ui_getDatos("170", codmon, "PARM1");
                cadtotal = cadentero.ToUpper() + " y " + caddecimal + " " + cadmoneda;


                nrodatarow = 0;
                foreach (DataRow row_factura in dtfactura.Rows)
                {
                    if (nrodatarow.Equals(0))
                    {
                        almafac = row_factura["alma"].ToString();
                        tdfac = row_factura["td"].ToString();
                        numdocfac = row_factura["numdoc"].ToString();
                        fecdoc = row_factura["fecdoc"].ToString();
                        rfndoc = row_factura["rfndoc"].ToString();
                        codmon = row_factura["codmon"].ToString();
                        glosa1 = row_factura["glosa1"].ToString();
                        codclie = row_factura["codclie"].ToString();
                        desclie = row_factura["desclie"].ToString();
                        rucclie = row_factura["rucclie"].ToString();
                        dniclie = row_factura["dniclie"].ToString();
                        dirclie1 = row_factura["dirclie1"].ToString();
                        dirclie2 = row_factura["dirclie2"].ToString();
                        dirclie3 = row_factura["dirclie3"].ToString();
                        tipcam = float.Parse(row_factura["tipcam"].ToString());
                        usuario = row_factura["usuario"].ToString();
                        rfnrooc = row_factura["rfnrooc"].ToString();
                        rfnguia = row_factura["rfnguia"].ToString();
                       
                     
                    }
                    
                    row = dtcompro.NewRow();
                    row["alma"] = almafac;
                    row["td"] = tdfac;
                    row["numdoc"] = numdocfac;
                    row["fecdoc"] = fecdoc;
                    row["rftdoc"] = rftdoc;
                    row["rfndoc"] = rfndoc;
                    row["codmon"] = codmon;
                    row["glosa1"] = glosa1;
                    row["codclie"] = codclie;
                    row["desclie"] = desclie;
                    row["rucclie"] = rucclie;
                    row["dniclie"] = dniclie;
                    row["dirclie1"] = dirclie1;
                    row["dirclie2"] = dirclie2;
                    row["dirclie3"] = dirclie3;
                    row["tipcam"] = tipcam;
                    row["usuario"] = usuario;
                    row["rfnrooc"] = rfnrooc;
                    row["rfnguia"] = rfnguia;
                  //  row["fven"] = fecven;
                    row["item"] = row_factura["item"].ToString();
                    row["codarti"] = row_factura["codarti"].ToString();
                    row["desarti"] = row_factura["desarti"].ToString(); //+"- "+ row_factura["fven"].ToString(); ;
                    row["cantidad"] = float.Parse(row_factura["cantidad"].ToString());
                    row["preuni"] = float.Parse(row_factura["preuni"].ToString());
                    row["descuento"] = float.Parse(row_factura["descuento"].ToString());
                    row["neto"] = float.Parse(row_factura["neto"].ToString());
                    row["igv"] = float.Parse(row_factura["igv"].ToString());
                    row["total"] = float.Parse(row_factura["total"].ToString());
                    row["unidad"] = row_factura["unidad"].ToString();
                    row["fven"] = row_factura["fven"].ToString();
                    row["totneto"] = totneto;
                    row["totigv"] = totigv;
                    row["tottotal"] = tottotal;
                    row["cadtotal"] = cadtotal;
               
                    dtcompro.Rows.Add(row);
                    nrodatarow++;
                }

                for (int x = 0; x <= (int.Parse(sisparm.ui_getDatos("MAXFILAFAC")) - nrodatarow); x++)
                {
                    row = dtcompro.NewRow();
                    row["alma"] = almafac;
                    row["td"] = tdfac;
                    row["numdoc"] = numdocfac;
                    row["fecdoc"] = fecdoc;
                    row["rftdoc"] = rftdoc;
                    row["rfndoc"] = rfndoc;
                    row["codmon"] = codmon;
                    row["glosa1"] = glosa1;
                    row["codclie"] = codclie;
                    row["desclie"] = desclie;
                    row["rucclie"] = rucclie;
                    row["dniclie"] = dniclie;
                    row["dirclie1"] = dirclie1;
                    row["dirclie2"] = dirclie2;
                    row["dirclie3"] = dirclie3;
                    row["tipcam"] = tipcam;
                    row["usuario"] = usuario;
                    row["rfnrooc"] = rfnrooc;
                    row["rfnguia"] = rfnguia;
                    row["fven"] = fecven;

                    row["item"] = string.Empty;
                    row["codarti"] = string.Empty;
                    row["desarti"] = string.Empty;
                    row["cantidad"] = 0;
                    row["preuni"] = 0;
                    row["descuento"] = 0;
                    row["neto"] = 0;
                    row["igv"] = 0;
                    row["total"] = 0;
                    row["unidad"] = string.Empty;

                    row["totneto"] = totneto;
                    row["totigv"] = totigv;
                    row["tottotal"] = tottotal;
                    row["cadtotal"] = cadtotal;

                    dtcompro.Rows.Add(row);
                }

            }

            conexion.Close();
            return dtcompro;
        }

    }
}
