using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Configuration;
using System.Text.RegularExpressions;

namespace CaniaBrava
{
    public partial class ui_constructor : Form
    {
        private Form FormPadre;
        public Form _FormPadre
        {
            get { return FormPadre; }
            set { FormPadre = value; }
        }

        #region Local Variables


        #endregion

        public ui_constructor()
        {
            InitializeComponent();
        }

        private void ui_listaConstantes()
        {
            TreeNode mainNode = new TreeNode();
            mainNode.Name = "constantesNode";
            mainNode.Text = "VARIABLES CONSTANTES";
            this.treeViewVariables.Nodes.Add(mainNode);
            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();

            string query = " select A.constante,A.dessisparm from sisparm A inner join detsisparm B ";
            query = query + " on A.idsisparm=B.idsisparm and B.state='V' ";
            query = query + " where tipo='C' order by constante asc;";

            try
            {
                SqlCommand myCommand = new SqlCommand(query, conexion);
                SqlDataReader myReader = myCommand.ExecuteReader();

                while (myReader.Read())
                {
                    ui_generaarbol(mainNode, myReader["constante"].ToString(), myReader["constante"].ToString() + " - " + myReader["dessisparm"]);

                }

                myReader.Close();
                myCommand.Dispose();

            }

            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }

            conexion.Close();

        }

        private void ui_listaPersonalizadas()
        {
            TreeNode mainNode = new TreeNode();
            mainNode.Name = "constantesNode";
            mainNode.Text = "VARIABLES PERSONALIZADAS";
            this.treeViewVariables.Nodes.Add(mainNode);


            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();
            string query = "select constante,dessisparm from sisparm where tipo='P' order by constante asc;";

            try
            {
                SqlCommand myCommand = new SqlCommand(query, conexion);
                SqlDataReader myReader = myCommand.ExecuteReader();

                while (myReader.Read())
                {
                    ui_generaarbol(mainNode, myReader["constante"].ToString(), myReader["constante"].ToString() + " - " + myReader["dessisparm"]);

                }

                myReader.Close();
                myCommand.Dispose();

            }

            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }

            conexion.Close();

        }

        private void ui_listaConceptos(string idcia, string idtipoplan, string idconplan, string idclascol, string desclascol, string idtipocal)
        {
            TreeNode mainNode = new TreeNode();
            mainNode.Name = "conceptosNode";
            mainNode.Text = "CONCEPTOS DE PLANILLA - " + desclascol;
            this.treeViewVariables.Nodes.Add(mainNode);

            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();

            string query = "select constante,desconplan from conplan where ";
            query += " idcia='" + @idcia + "' and idtipoplan='" + @idtipoplan + "' ";
            query += " and idclascol='" + @idclascol + "' and idtipocal='" + @idtipocal + "' ";
            query += " and idconplan  in ( Select idconplan from conplan where ";
            query += " idtipocal='" + @idtipocal + "' and idtipoplan='" + @idtipoplan + "' ";
            query += " and (idconplan <'" + idconplan + "' or idconplan ='6200')) order by idconplan asc;";

            try
            {
                SqlCommand myCommand = new SqlCommand(query, conexion);
                SqlDataReader myReader = myCommand.ExecuteReader();

                while (myReader.Read())
                {
                    ui_generaarbol(mainNode, myReader["constante"].ToString(), myReader["constante"].ToString() + " - " + myReader["desconplan"].ToString());

                }

                myReader.Close();
                myCommand.Dispose();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }

            conexion.Close();
        }

        #region Remove BackColor

        // recursively move through the treeview nodes
        // and reset backcolors to white
        private void ClearBackColor(TreeView tw)
        {
            TreeNodeCollection nodes = tw.Nodes;
            foreach (TreeNode n in nodes)
            {
                ClearRecursive(n);
            }
        }

        // called by ClearBackColor function
        private void ClearRecursive(TreeNode treeNode)
        {
            foreach (TreeNode tn in treeNode.Nodes)
            {
                tn.BackColor = Color.White;
                ClearRecursive(tn);
            }
        }

        #endregion

        public void ui_loadform(string idtipoplan, string idconplan, string desconplan, string formula, string idtipocal)
        {
            GlobalVariables globalVariables = new GlobalVariables();
            string idcia = globalVariables.getValorCia();
            txtFormula.Text = formula;
            this.ui_evaluarExpresion(formula);
            lblNombre.Text = idconplan + " - " + desconplan;

            ui_listaConstantes();
            ui_listaPersonalizadas();
            ui_listaConceptos(idcia, idtipoplan, idconplan, "P", "PARAMETROS", idtipocal);
            ui_listaConceptos(idcia, idtipoplan, idconplan, "I", "INGRESOS", idtipocal);
            ui_listaConceptos(idcia, idtipoplan, idconplan, "D", "DESCUENTOS", idtipocal);
            ui_listaConceptos(idcia, idtipoplan, idconplan, "A", "APORTACIONES", idtipocal);
        }

        private void ui_constructor_Load(object sender, EventArgs e)
        {

        }

        private void ui_generaarbol(TreeNode nodopadre, string NewNodeName, string NewNodeText)
        {
            TreeNode nod = new TreeNode();
            nod.Name = NewNodeName;
            nod.Text = NewNodeText;
            treeViewVariables.SelectedNode = nodopadre;
            treeViewVariables.SelectedNode.Nodes.Add(nod);
            treeViewVariables.SelectedNode.ExpandAll();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (pictureValidOk.Visible == true)
            {
                string formula = txtFormula.Text.Trim();
                ((ui_confmotivos)FormPadre)._TextBoxActivo.Text = formula;
                Close();
            }
            else
            {
                MessageBox.Show("Fórmula no reconocible por el Sistema", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void treeViewVariables_Click(object sender, EventArgs e)
        {
            ClearBackColor(treeViewVariables);
        }

        private void treeViewVariables_AfterSelect(object sender, TreeViewEventArgs e)
        {

        }

        private void FindByText(TreeView tw)
        {
            TreeNodeCollection nodes = tw.Nodes;
            foreach (TreeNode n in nodes)
            {
                FindRecursive(n);
            }
        }

        private void FindRecursive(TreeNode treeNode)
        {
            foreach (TreeNode tn in treeNode.Nodes)
            {
                string regexPattern = string.Format(@"\b{0}\b", Regex.Escape(this.txtNodeTextSearch.Text));
                if (Regex.IsMatch(tn.Text, regexPattern))
                {
                    tn.BackColor = Color.Yellow;
                }

                FindRecursive(tn);
            }
        }

        private void btnNodeTextSearch_Click(object sender, EventArgs e)
        {
            ClearBackColor(treeViewVariables);
            FindByText(treeViewVariables);
            txtNodeTextSearch.Clear();
        }

        private void ui_constructorFormula(string segmento)
        {
            string formula = txtFormula.Text.Trim();
            formula = formula + segmento;
            txtFormula.Text = formula;
            this.ui_evaluarExpresion(txtFormula.Text.Trim());
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            ui_constructorFormula("1");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ui_constructorFormula("2");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ui_constructorFormula("3");
        }

        private void button6_Click(object sender, EventArgs e)
        {
            ui_constructorFormula("4");
        }

        private void button5_Click(object sender, EventArgs e)
        {
            ui_constructorFormula("5");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            ui_constructorFormula("6");
        }

        private void button9_Click(object sender, EventArgs e)
        {
            ui_constructorFormula("7");
        }

        private void button8_Click(object sender, EventArgs e)
        {
            ui_constructorFormula("8");
        }

        private void button7_Click(object sender, EventArgs e)
        {
            ui_constructorFormula("9");
        }

        private void button18_Click(object sender, EventArgs e)
        {
            ui_constructorFormula("[");
        }

        private void button17_Click(object sender, EventArgs e)
        {
            ui_constructorFormula("]");
        }

        private void button15_Click(object sender, EventArgs e)
        {
            ui_constructorFormula("{");
        }

        private void button14_Click(object sender, EventArgs e)
        {
            ui_constructorFormula("}");
        }

        private void button12_Click(object sender, EventArgs e)
        {
            ui_constructorFormula("(");
        }

        private void button11_Click(object sender, EventArgs e)
        {
            ui_constructorFormula(")");
        }

        private void button21_Click(object sender, EventArgs e)
        {
            ui_constructorFormula("/");
        }

        private void button20_Click(object sender, EventArgs e)
        {
            ui_constructorFormula("-");
        }

        private void button19_Click(object sender, EventArgs e)
        {
            ui_constructorFormula("+");
        }

        private void button16_Click(object sender, EventArgs e)
        {
            ui_constructorFormula("*");
        }

        private void button13_Click(object sender, EventArgs e)
        {
            ui_constructorFormula(".");
        }

        private void cmdAgregar_Click(object sender, EventArgs e)
        {
            try
            {
                string sName = treeViewVariables.SelectedNode.Name.ToString();

                this.ui_constructorFormula(sName);
            }
            catch { }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            if (txtFormula.Text.Trim() != string.Empty)
            {

                txtFormula.Text = txtFormula.Text.Trim().Substring(0, txtFormula.Text.Length - 1);
                this.ui_evaluarExpresion(txtFormula.Text.Trim());
            }
        }

        private void button22_Click(object sender, EventArgs e)
        {
            txtFormula.Text = string.Empty;
            pictureValidAsk.Visible = true;
            pictureValidBad.Visible = false;
            pictureValidOk.Visible = false;
        }

        private void ui_evaluarExpresion(string formula)
        {
            MathParser mp = new MathParser();

            mp.Parameters.Add(Parameters.AA, 1);
            mp.Parameters.Add(Parameters.AB, 1);
            mp.Parameters.Add(Parameters.AC, 1);
            mp.Parameters.Add(Parameters.AD, 1);
            mp.Parameters.Add(Parameters.AE, 1);
            mp.Parameters.Add(Parameters.AF, 1);
            mp.Parameters.Add(Parameters.AG, 1);
            mp.Parameters.Add(Parameters.AH, 1);
            mp.Parameters.Add(Parameters.AI, 1);
            mp.Parameters.Add(Parameters.AJ, 1);
            mp.Parameters.Add(Parameters.AL, 1);
            mp.Parameters.Add(Parameters.AK, 1);
            mp.Parameters.Add(Parameters.AM, 1);
            mp.Parameters.Add(Parameters.AN, 1);
            mp.Parameters.Add(Parameters.AO, 1);
            mp.Parameters.Add(Parameters.AP, 1);
            mp.Parameters.Add(Parameters.AQ, 1);
            mp.Parameters.Add(Parameters.AR, 1);
            mp.Parameters.Add(Parameters.AS, 1);
            mp.Parameters.Add(Parameters.AT, 1);
            mp.Parameters.Add(Parameters.AU, 1);
            mp.Parameters.Add(Parameters.AV, 1);
            mp.Parameters.Add(Parameters.AW, 1);
            mp.Parameters.Add(Parameters.AX, 1);
            mp.Parameters.Add(Parameters.AY, 1);
            mp.Parameters.Add(Parameters.AZ, 1);

            mp.Parameters.Add(Parameters.BA, 1);
            mp.Parameters.Add(Parameters.BB, 1);
            mp.Parameters.Add(Parameters.BC, 1);
            mp.Parameters.Add(Parameters.BD, 1);
            mp.Parameters.Add(Parameters.BE, 1);
            mp.Parameters.Add(Parameters.BF, 1);
            mp.Parameters.Add(Parameters.BG, 1);
            mp.Parameters.Add(Parameters.BH, 1);
            mp.Parameters.Add(Parameters.BI, 1);
            mp.Parameters.Add(Parameters.BJ, 1);
            mp.Parameters.Add(Parameters.BK, 1);
            mp.Parameters.Add(Parameters.BL, 1);
            mp.Parameters.Add(Parameters.BM, 1);
            mp.Parameters.Add(Parameters.BN, 1);
            mp.Parameters.Add(Parameters.BO, 1);
            mp.Parameters.Add(Parameters.BP, 1);
            mp.Parameters.Add(Parameters.BQ, 1);
            mp.Parameters.Add(Parameters.BR, 1);
            mp.Parameters.Add(Parameters.BS, 1);
            mp.Parameters.Add(Parameters.BT, 1);
            mp.Parameters.Add(Parameters.BU, 1);
            mp.Parameters.Add(Parameters.BV, 1);
            mp.Parameters.Add(Parameters.BW, 1);
            mp.Parameters.Add(Parameters.BX, 1);
            mp.Parameters.Add(Parameters.BY, 1);
            mp.Parameters.Add(Parameters.BZ, 1);

            mp.Parameters.Add(Parameters.KA, 1);
            mp.Parameters.Add(Parameters.KB, 1);
            mp.Parameters.Add(Parameters.KC, 1);
            mp.Parameters.Add(Parameters.KD, 1);
            mp.Parameters.Add(Parameters.KE, 1);
            mp.Parameters.Add(Parameters.KF, 1);
            mp.Parameters.Add(Parameters.KG, 1);
            mp.Parameters.Add(Parameters.KH, 1);
            mp.Parameters.Add(Parameters.KI, 1);
            mp.Parameters.Add(Parameters.KJ, 1);
            mp.Parameters.Add(Parameters.KL, 1);
            mp.Parameters.Add(Parameters.KK, 1);
            mp.Parameters.Add(Parameters.KM, 1);
            mp.Parameters.Add(Parameters.KN, 1);
            mp.Parameters.Add(Parameters.KO, 1);
            mp.Parameters.Add(Parameters.KP, 1);
            mp.Parameters.Add(Parameters.KQ, 1);
            mp.Parameters.Add(Parameters.KR, 1);
            mp.Parameters.Add(Parameters.KS, 1);
            mp.Parameters.Add(Parameters.KT, 1);
            mp.Parameters.Add(Parameters.KU, 1);
            mp.Parameters.Add(Parameters.KV, 1);
            mp.Parameters.Add(Parameters.KW, 1);
            mp.Parameters.Add(Parameters.KX, 1);
            mp.Parameters.Add(Parameters.KY, 1);
            mp.Parameters.Add(Parameters.KZ, 1);

            mp.Parameters.Add(Parameters.CA, 1);
            mp.Parameters.Add(Parameters.CB, 1);
            mp.Parameters.Add(Parameters.CC, 1);
            mp.Parameters.Add(Parameters.CD, 1);
            mp.Parameters.Add(Parameters.CE, 1);
            mp.Parameters.Add(Parameters.CF, 1);
            mp.Parameters.Add(Parameters.CG, 1);
            mp.Parameters.Add(Parameters.CH, 1);
            mp.Parameters.Add(Parameters.CI, 1);
            mp.Parameters.Add(Parameters.CJ, 1);
            mp.Parameters.Add(Parameters.CL, 1);
            mp.Parameters.Add(Parameters.CK, 1);
            mp.Parameters.Add(Parameters.CM, 1);
            mp.Parameters.Add(Parameters.CN, 1);
            mp.Parameters.Add(Parameters.CO, 1);
            mp.Parameters.Add(Parameters.CP, 1);
            mp.Parameters.Add(Parameters.CQ, 1);
            mp.Parameters.Add(Parameters.CR, 1);
            mp.Parameters.Add(Parameters.CS, 1);
            mp.Parameters.Add(Parameters.CT, 1);
            mp.Parameters.Add(Parameters.CU, 1);
            mp.Parameters.Add(Parameters.CV, 1);
            mp.Parameters.Add(Parameters.CW, 1);
            mp.Parameters.Add(Parameters.CX, 1);
            mp.Parameters.Add(Parameters.CY, 1);
            mp.Parameters.Add(Parameters.CZ, 1);

            mp.Parameters.Add(Parameters.TA, 1);
            mp.Parameters.Add(Parameters.TB, 1);
            mp.Parameters.Add(Parameters.TC, 1);
            mp.Parameters.Add(Parameters.TD, 1);
            mp.Parameters.Add(Parameters.TE, 1);
            mp.Parameters.Add(Parameters.TF, 1);
            mp.Parameters.Add(Parameters.TG, 1);
            mp.Parameters.Add(Parameters.TH, 1);
            mp.Parameters.Add(Parameters.TI, 1);
            mp.Parameters.Add(Parameters.TJ, 1);

            decimal resultado = mp.Calculate(formula);

            if (resultado.Equals(-99999999))
            {
                pictureValidAsk.Visible = false;
                pictureValidBad.Visible = true;
                pictureValidOk.Visible = false;
            }
            else
            {
                pictureValidAsk.Visible = false;
                pictureValidBad.Visible = false;
                pictureValidOk.Visible = true;
            }
        }

        private void button23_Click(object sender, EventArgs e)
        {

        }

        private void button14_Click_1(object sender, EventArgs e)
        {
            Close();
        }

        private void button14_Click_2(object sender, EventArgs e)
        {
            ui_constructorFormula("0");
        }
    }
}