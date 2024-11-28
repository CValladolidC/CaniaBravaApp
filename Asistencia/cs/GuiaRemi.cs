using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;

namespace CaniaBrava
{
    class GuiaRemi
    {
        public DataTable generaDataGuiaRemi(string codcia, string rftguia, string rfserieguia, string rfnroguia)
        {
            string query;
            int nrodatarow;

            SisParm sisparm = new SisParm();
            DataTable dtguiaremi = new DataTable();
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
            column.ColumnName = "fectras";
            column.ReadOnly = true;
            dtcompro.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "rftguia";
            column.ReadOnly = true;
            dtcompro.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "rfnguia";
            column.ReadOnly = true;
            dtcompro.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "mottras";
            column.ReadOnly = true;
            dtcompro.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "partida";
            column.ReadOnly = true;
            dtcompro.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "llegada";
            column.ReadOnly = true;
            dtcompro.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "rucdesti";
            column.ReadOnly = true;
            dtcompro.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "razondesti";
            column.ReadOnly = true;
            dtcompro.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "desdocidendesti";
            column.ReadOnly = true;
            dtcompro.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "nrodocdesti";
            column.ReadOnly = true;
            dtcompro.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "razontrans";
            column.ReadOnly = true;
            dtcompro.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "marcatrans";
            column.ReadOnly = true;
            dtcompro.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "certrans";
            column.ReadOnly = true;
            dtcompro.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "lictrans";
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
            column.ColumnName = "pesototal";
            column.ReadOnly = true;
            dtcompro.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "unidad";
            column.ReadOnly = true;
            dtcompro.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "rfnrooc";
            column.ReadOnly = true;
            dtcompro.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "albaran";
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
            column.ColumnName = "codesta";
            column.ReadOnly = true;
            dtcompro.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "codartiba";
            column.ReadOnly = true;
            dtcompro.Columns.Add(column);

            string almagr = string.Empty;
            string tdgr = string.Empty;
            string numdocgr = string.Empty;
            string fecdoc = string.Empty;
            string fectras = string.Empty;
            string rfnguia = string.Empty;
            string mottras = string.Empty;
            string partida = string.Empty;
            string llegada = string.Empty;
            string rucdesti = string.Empty;
            string razondesti = string.Empty;
            string desdocidendesti = string.Empty;
            string nrodocdesti = string.Empty;
            string razontrans = string.Empty;
            string marcatrans = string.Empty;
            string certrans = string.Empty;
            string lictrans = string.Empty;
            string usuario = string.Empty;
            
            string rfnrooc = string.Empty;
            string albaran = string.Empty;
            string rftdoc = string.Empty;
            string rfndoc = string.Empty;
            string codesta = string.Empty;
            

            query = " select A.alma,A.td,A.numdoc,A.fecdoc,A.fectras,A.rftguia,A.rfnguia, ";
            query = query + " A.mottras,E.despartida as partida,P.despartida as llegada,";
            query = query + " C.rucclie as rucdesti,C.desclie as razondesti,";
            query = query + " D.parm1maesgen as desdocidendesti,";
            query = query + " A.nrodocdesti,A.razontrans,A.marcatrans,A.certrans,A.certrans,";
            query = query + " A.lictrans,A.usuario,A.rfnrooc,A.albaran,A.rftdoc,A.rfndoc,A.codesta,";
            query = query + " CASE ISNULL(J.item) WHEN 1 THEN '' WHEN 0 THEN J.item END as item,";
            query = query + " CASE ISNULL(J.codarti) WHEN 1 THEN '' WHEN 0 THEN J.codarti END as codarti,";
            query = query + " CASE ISNULL(K.codartiba) WHEN 1 THEN '' WHEN 0 THEN K.codartiba END as codartiba,";
            query = query + " CASE ISNULL(K.desarti) WHEN 1 THEN '' WHEN 0 THEN K.desarti END as desarti, ";
            query = query + " CASE ISNULL(J.cantidad) WHEN 1 THEN 0 WHEN 0 THEN J.cantidad END as cantidad ,";
            query = query + " CASE ISNULL(J.pesototal) WHEN 1 THEN 0 WHEN 0 THEN J.pesototal END as pesototal,";
            query = query + " CASE ISNULL(K.unidad) WHEN 1 THEN '' WHEN 0 THEN K.unidad END as unidad from almovc A ";
            query = query + " left join maesgen D on D.idmaesgen='220' and A.docidendesti=D.clavemaesgen ";
            query = query + " left join almovd J on A.codcia=J.codcia and A.alma=J.alma and A.td=J.td and A.numdoc=J.numdoc ";
            query = query + " left join alarti K on J.codcia=K.codcia and J.codarti=K.codarti ";
            query = query + " left join clie C on A.codclie=C.codclie ";
            query = query + " left join punpar E on A.partida=E.codpartida and A.codcia=E.idcia ";
            query = query + " left join punclie P on A.llegada=P.codpartida and A.codclie=P.codclie ";
            query = query + " WHERE A.codcia='" + @codcia + "' and A.rftguia='" + @rftguia + "' ";
            query = query + " and A.rfserieguia='" + @rfserieguia + "' and A.rfnroguia='" + @rfnroguia + "' ";
            query = query + " order by J.item asc ; ";

            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();

            SqlDataAdapter da = new SqlDataAdapter();


            da.SelectCommand = new SqlCommand(query, conexion);
            da.Fill(dtguiaremi);

            if (dtguiaremi.Rows.Count > 0)
            {

                nrodatarow = 0;
                foreach (DataRow row_guiaremi in dtguiaremi.Rows)
                {
                    if (nrodatarow.Equals(0))
                    {
                        almagr = row_guiaremi["alma"].ToString();
                        tdgr = row_guiaremi["td"].ToString();
                        numdocgr = row_guiaremi["numdoc"].ToString();
                        fecdoc = row_guiaremi["fecdoc"].ToString().Substring(0, 10);
                        fectras = row_guiaremi["fectras"].ToString();
                        rftguia = row_guiaremi["rftguia"].ToString();
                        rfnguia = row_guiaremi["rfnguia"].ToString();
                        mottras = row_guiaremi["mottras"].ToString();
                        partida = row_guiaremi["partida"].ToString();
                        llegada = row_guiaremi["llegada"].ToString();
                        rucdesti = row_guiaremi["rucdesti"].ToString();
                        razondesti = row_guiaremi["razondesti"].ToString();
                        desdocidendesti = row_guiaremi["desdocidendesti"].ToString();
                        nrodocdesti = row_guiaremi["nrodocdesti"].ToString();
                        razontrans = row_guiaremi["razontrans"].ToString();
                        marcatrans = row_guiaremi["marcatrans"].ToString();
                        certrans = row_guiaremi["certrans"].ToString();
                        lictrans = row_guiaremi["lictrans"].ToString();
                        usuario = row_guiaremi["usuario"].ToString();

                        rfnrooc = row_guiaremi["rfnrooc"].ToString();
                        albaran = row_guiaremi["albaran"].ToString();
                        rftdoc = row_guiaremi["rftdoc"].ToString();
                        rfndoc = row_guiaremi["rfndoc"].ToString();
                        codesta = row_guiaremi["codesta"].ToString();
                    }

                    row = dtcompro.NewRow();
                    row["alma"] = almagr;
                    row["td"] = tdgr;
                    row["numdoc"] = numdocgr;
                    row["fecdoc"] = fecdoc;
                    row["fectras"] = fectras;
                    row["rftguia"] = rftguia;
                    row["rfnguia"] = rfnguia;
                    row["mottras"] = mottras;
                    row["partida"] = partida;
                    row["llegada"] = llegada;
                    row["rucdesti"] = rucdesti;
                    row["razondesti"] = razondesti;
                    row["desdocidendesti"] = desdocidendesti;
                    row["nrodocdesti"] = nrodocdesti;
                    row["razontrans"] = razontrans;
                    row["marcatrans"] = marcatrans;
                    row["certrans"] = certrans;
                    row["lictrans"] = lictrans;
                    row["usuario"] = usuario;

                    row["rfnrooc"] = rfnrooc;
                    row["albaran"]=albaran;
                    row["rftdoc"]=rftdoc;
                    row["rfndoc"]=rfndoc;
                    row["codesta"]=codesta;
                    
                    row["item"] = row_guiaremi["item"].ToString();
                    row["codarti"] = row_guiaremi["codarti"].ToString();
                    row["codartiba"] = row_guiaremi["codartiba"].ToString();
                    row["desarti"] = row_guiaremi["desarti"].ToString();
                    row["cantidad"] = float.Parse(row_guiaremi["cantidad"].ToString());
                    row["pesototal"] = float.Parse(row_guiaremi["pesototal"].ToString());
                    row["unidad"] = row_guiaremi["unidad"].ToString();
                    dtcompro.Rows.Add(row);
                    nrodatarow++;
                }

                for (int x = 0; x <= (int.Parse(sisparm.ui_getDatos("MAXFILAGUIA")) - nrodatarow); x++)
                {
                    row = dtcompro.NewRow();
                    row["alma"] = almagr;
                    row["td"] = tdgr;
                    row["numdoc"] = numdocgr;
                    row["fecdoc"] = fecdoc;
                    row["fectras"] = fectras;
                    row["rftguia"] = rftguia;
                    row["rfnguia"] = rfnguia;
                    row["mottras"] = mottras;
                    row["partida"] = partida;
                    row["llegada"] = llegada;
                    row["rucdesti"] = rucdesti;
                    row["razondesti"] = razondesti;
                    row["desdocidendesti"] = desdocidendesti;
                    row["nrodocdesti"] = nrodocdesti;
                    row["razontrans"] = razontrans;
                    row["marcatrans"] = marcatrans;
                    row["certrans"] = certrans;
                    row["lictrans"] = lictrans;
                    row["usuario"] = usuario;

                    row["rfnrooc"] = rfnrooc;
                    row["albaran"] = albaran;
                    row["rftdoc"] = rftdoc;
                    row["rfndoc"] = rfndoc;
                    row["codesta"] = codesta;

                    row["item"] = string.Empty;
                    row["codarti"] = string.Empty;
                    row["codartiba"] = string.Empty;
                    row["desarti"] = string.Empty;
                    row["cantidad"] = 0;
                    row["pesototal"] = 0;
                    row["unidad"] = string.Empty;
                    dtcompro.Rows.Add(row);
                }

            }

            conexion.Close();
            return dtcompro;
        }

        public DataTable generaGuiasRemiWin(string codcia, string rftguia, string rfserieguia, string rfnroguiaini, string rfnroguiafin)
        {
            DataTable dtguias = new DataTable();
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
            column.ColumnName = "fectras";
            column.ReadOnly = true;
            dtcompro.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "rftguia";
            column.ReadOnly = true;
            dtcompro.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "rfnguia";
            column.ReadOnly = true;
            dtcompro.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "mottras";
            column.ReadOnly = true;
            dtcompro.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "partida";
            column.ReadOnly = true;
            dtcompro.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "llegada";
            column.ReadOnly = true;
            dtcompro.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "rucdesti";
            column.ReadOnly = true;
            dtcompro.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "razondesti";
            column.ReadOnly = true;
            dtcompro.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "desdocidendesti";
            column.ReadOnly = true;
            dtcompro.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "nrodocdesti";
            column.ReadOnly = true;
            dtcompro.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "razontrans";
            column.ReadOnly = true;
            dtcompro.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "marcatrans";
            column.ReadOnly = true;
            dtcompro.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "certrans";
            column.ReadOnly = true;
            dtcompro.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "lictrans";
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
            column.ColumnName = "pesototal";
            column.ReadOnly = true;
            dtcompro.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "unidad";
            column.ReadOnly = true;
            dtcompro.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "rfnrooc";
            column.ReadOnly = true;
            dtcompro.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "albaran";
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
            column.ColumnName = "codesta";
            column.ReadOnly = true;
            dtcompro.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "codartiba";
            column.ReadOnly = true;
            dtcompro.Columns.Add(column);

            string query = " select A.codcia,A.rftguia,A.rfserieguia,A.rfnroguia from almovc A ";
            query = query + " WHERE A.codcia='" + @codcia + "' and A.flag='GRCS' and ";
            query = query + " A.rftguia='" + @rftguia + "' ";
            query = query + " and A.rfserieguia='" + @rfserieguia + "' ";
            query = query + " and A.rfnroguia between '" + @rfnroguiaini + "' and '" + @rfnroguiafin + "' ";
            query = query + " order by A.rftguia asc,A.rfserieguia asc,A.rfnroguia ; ";

            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = new SqlCommand(query, conexion);
            da.Fill(dtguias);

            if (dtguias.Rows.Count > 0)
            {
                foreach (DataRow row_guias in dtguias.Rows)
                {
                    string rfnroguia = row_guias["rfnroguia"].ToString();

                    DataTable dttmp = new DataTable();
                    dttmp = generaDataGuiaRemi(codcia, rftguia, rfserieguia, rfnroguia);

                    foreach (DataRow row_tmp in dttmp.Rows)
                    {
                        row = dtcompro.NewRow();
                        row["alma"] = row_tmp["alma"].ToString();
                        row["td"] = row_tmp["td"].ToString();
                        row["numdoc"] = row_tmp["numdoc"].ToString();
                        row["fecdoc"] = row_tmp["fecdoc"].ToString();
                        row["fectras"] = row_tmp["fectras"].ToString();
                        row["rftguia"] = row_tmp["rftguia"].ToString();
                        row["rfnguia"] = row_tmp["rfnguia"].ToString();
                        row["mottras"] = row_tmp["mottras"].ToString();
                        row["partida"] = row_tmp["partida"].ToString();
                        row["llegada"] = row_tmp["llegada"].ToString();
                        row["rucdesti"] = row_tmp["rucdesti"].ToString();
                        row["razondesti"] = row_tmp["razondesti"].ToString();
                        row["desdocidendesti"] = row_tmp["desdocidendesti"].ToString();
                        row["nrodocdesti"] = row_tmp["nrodocdesti"].ToString();
                        row["razontrans"] = row_tmp["razontrans"].ToString();
                        row["marcatrans"] = row_tmp["marcatrans"].ToString();
                        row["certrans"] = row_tmp["certrans"].ToString();
                        row["lictrans"] = row_tmp["lictrans"].ToString();
                        row["usuario"] = row_tmp["usuario"].ToString();

                        row["rfnrooc"] = row_tmp["rfnrooc"].ToString();
                        row["albaran"] = row_tmp["albaran"].ToString();
                        row["rftdoc"] = row_tmp["rftdoc"].ToString();
                        row["rfndoc"] = row_tmp["rfndoc"].ToString();
                        row["codesta"] = row_tmp["codesta"].ToString();

                        row["item"] = row_tmp["item"].ToString();
                        row["codarti"] = row_tmp["codarti"].ToString();
                        row["codartiba"] = row_tmp["codartiba"].ToString();
                        row["desarti"] = row_tmp["desarti"].ToString();
                        row["cantidad"] = float.Parse(row_tmp["cantidad"].ToString());
                        row["pesototal"] = float.Parse(row_tmp["pesototal"].ToString());
                        row["unidad"] = row_tmp["unidad"].ToString();
                        dtcompro.Rows.Add(row);
                    }
                }
            }
            return dtcompro;

        }
    }
}
