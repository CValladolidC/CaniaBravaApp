using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using System.Configuration;
using System.Data.SqlClient;

namespace CaniaBrava
{
    public partial class ui_Backup : Form
    {
        public ui_Backup()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {


        }

        private void button2_Click(object sender, EventArgs e)
        {


        }

        private void ui_Backup_Load(object sender, EventArgs e)
        {
            ui_ListaCopias();
        }

        /* private void createGraphicsColumn()
         {
             Icon treeIcon = new Icon(this.GetType(), "SISTEMA.ICO");
             DataGridViewImageColumn iconColumn = new DataGridViewImageColumn();
             iconColumn.Image = treeIcon.ToBitmap();
             iconColumn.Name = "Tree";
             iconColumn.HeaderText = "Nice tree";
             dgvdetalle.Columns.Insert(3, iconColumn);
         }*/

        private void ui_ListaCopias()
        {
            Funciones funciones = new Funciones();
            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();
            string query = "select usuario,fecha,file_ from hisbak order by fecha desc;";

            try
            {
                using (SqlDataAdapter myDataAdapter = new SqlDataAdapter(query, conexion))
                {

                    DataSet myDataSet = new DataSet();
                    myDataAdapter.Fill(myDataSet, "tblBak");
                    funciones.formatearDataGridView(dgvdetalle);
                    dgvdetalle.DataSource = myDataSet.Tables["tblBak"];
                    dgvdetalle.Columns[0].HeaderText = "Generado por";
                    dgvdetalle.Columns[1].HeaderText = "Fecha";
                    dgvdetalle.Columns[2].HeaderText = "Archivo Generado";
                    dgvdetalle.Columns[0].Width = 100;
                    dgvdetalle.Columns[1].Width = 100;
                    dgvdetalle.Columns[2].Width = 150;

                }
                //createGraphicsColumn();


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }

            conexion.Close();
        }

        private void copia_de_seguridad()
        {
            try
            {
                string rutaFile = string.Empty; ;

                FolderBrowserDialog dialogoRuta = new FolderBrowserDialog();
                if (dialogoRuta.ShowDialog() == DialogResult.OK)
                {
                    rutaFile = dialogoRuta.SelectedPath;
                }

                if (rutaFile != string.Empty)
                {

                    GlobalVariables globalVariables = new GlobalVariables();
                    DateTime backupTime = DateTime.Now;
                    int year = backupTime.Year;
                    int month = backupTime.Month;
                    int day = backupTime.Day;
                    int hour = backupTime.Hour;
                    int minute = backupTime.Minute;
                    string fecha = DateTime.Now.ToString("dd/MM/yyyy");
                    string usuario = globalVariables.getValorUsr();

                    //string filename = backupTime.ToString();
                    string filename = year + "-" + month + "-" + day + "-" + hour + "-" + minute;
                    String linea;
                    string fichero = rutaFile + "/" + filename + ".sql";
                    string ficherozip = rutaFile + "/" + filename + ".zip";
                    System.Diagnostics.Process proc = new System.Diagnostics.Process();
                    proc.EnableRaisingEvents = false;
                    proc.StartInfo.UseShellExecute = false;
                    proc.StartInfo.RedirectStandardOutput = true;
                    proc.StartInfo.FileName = "mysqldump";
                    proc.StartInfo.Arguments = ConfigurationManager.AppSettings.Get("CADENA_BACKUP");
                    Process miProceso;
                    miProceso = Process.Start(proc.StartInfo);
                    StreamReader sr = miProceso.StandardOutput;
                    TextWriter tw = new StreamWriter(fichero, false, Encoding.Default);
                    while ((linea = sr.ReadLine()) != null)
                    {
                        tw.WriteLine(linea);
                    }
                    tw.Close();

                    ZipUtil _zipUtil = new ZipUtil();
                    _zipUtil.CompressFile(fichero, ficherozip);

                    if (System.IO.File.Exists(fichero))
                    {
                        System.IO.File.Delete(fichero);
                    }

                    MessageBox.Show("Copia de seguridad realizada con éxito");
                    HisBak hisbak = new HisBak();
                    hisbak.actualizarHisBak(usuario, fecha, ficherozip);
                    ui_ListaCopias();

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Se ha producido un error al realizar la copia de seguridad");
            }
        }

        private void button2_Click_1(object sender, EventArgs e)
        {

        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            copia_de_seguridad();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }
    }
}