using ExcelDataReader;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CaniaBrava.cs;
using System.Data.SqlClient;
using System.Configuration;

namespace CaniaBrava.Interface
{
    public partial class ui_mqt_gastos_maestros : Form
    {
        private DataSet dtsTabla = new DataSet();
        private int nColOrd = 18, nColCc = 3, nColMat = 5, nColAgr = 4, nColTdm = 3, nColJr = 3;
        private List<TabPage> pgOcultas = new List<TabPage>();
        private List<TabPage> pgOcultasDic = new List<TabPage>();
        private int indDic;

        public ui_mqt_gastos_maestros()
        {
            InitializeComponent();
            cmbHojas.Enabled = false;
            Diccionario();

            if (tabControl.TabPages.Count == 7)
            {
                OcultarPgs(new int[] { 1, 2, 3, 4, 5 }); //antes -> 3, 5
                OcultarPgsDic(new int[] { 3, 5 });
                indDic = 1;
            }

            //if (tabControl.TabPages.Count == 2)
            //{
            //    MostrarPgs();
            //    MostrarPgsDic();
            //    indDic = 6;
            //}
        }

        private void OcultarPgs(int[] indicesPgs)
        {
            Array.Sort(indicesPgs);
            Array.Reverse(indicesPgs);

            foreach (int indice in indicesPgs)
            {
                if (indice >= 0 && indice < tabControl.TabPages.Count)
                {
                    pgOcultas.Add(tabControl.TabPages[indice]);
                    tabControl.TabPages.RemoveAt(indice);
                }
            }
        }

        private void MostrarPgs()
        {
            foreach (var pag in pgOcultas) { tabControl.TabPages.Add(pag); }
            pgOcultas.Clear();
        }
        //DIC
        private void OcultarPgsDic(int[] indicesPgs)
        {
            Array.Sort(indicesPgs);
            Array.Reverse(indicesPgs);
            
            foreach (int indice in indicesPgs)
            {
                if (indice >= 0 && indice < tabDicControl.TabPages.Count)
                {
                    pgOcultasDic.Add(tabDicControl.TabPages[indice]);
                    tabDicControl.TabPages.RemoveAt(indice);
                }
            }
        }

        private void MostrarPgsDic()
        {
            foreach (var pag in pgOcultasDic) { tabDicControl.TabPages.Add(pag); }
            pgOcultasDic.Clear();
        }

        //PAGINA MAESTROS
        private async void btnCargar_Click(object sender, EventArgs e)
        {
            cmbHojas.Items.Clear();

            lbPorcentaje.Text = 0 + "%";
            progressBar.Value = 0;

            txtRuta.Clear();

            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "Excel Worbook|*.xls;*.xlsx"
            };

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                dgvDatos.DataSource = null;

                txtRuta.Text = openFileDialog.FileName;
                
                try
                {
                    GifLoading.Visible = true;
                    await Task.Run(() =>
                    {
                        using (var stream = File.Open(openFileDialog.FileName, FileMode.Open, FileAccess.Read))
                        {
                            using (var reader = ExcelReaderFactory.CreateReader(stream))
                            {
                                dtsTabla = reader.AsDataSet(new ExcelDataSetConfiguration()
                                {
                                    ConfigureDataTable = (tableReader) => new ExcelDataTableConfiguration()
                                    {
                                        UseHeaderRow = true
                                    }
                                });
                            }
                        }
                    });
                    //using (var stream = File.Open(openFileDialog.FileName, FileMode.Open, FileAccess.Read))
                    //{
                    //    using (var reader = ExcelReaderFactory.CreateReader(stream))
                    //    {
                    //        dtsTabla = reader.AsDataSet(new ExcelDataSetConfiguration()
                    //        {
                    //            ConfigureDataTable = (tableReader) => new ExcelDataTableConfiguration()
                    //            {
                    //                UseHeaderRow = true
                    //            }
                    //        });

                    foreach (DataTable tabla in dtsTabla.Tables)
                    {
                        cmbHojas.Items.Add(tabla.TableName);
                    }
                    cmbHojas.Enabled = true;
                    cmbHojas.SelectedIndex = 0;

                    // Liberar memoria del DataSet una vez transferido al DataGridView
                    //dtsTabla.Dispose();

                    //dgvDatos.DataSource = dtsTabla.Tables[cmbHojas.SelectedIndex];
                    //dgvDatos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                    //    }
                    //}
                    //GC.Collect();
                    //GC.WaitForPendingFinalizers();

                }
                catch (IOException) { MessageBox.Show("El archivo está siendo utilizado por otro proceso.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                //finally
                //{
                //    GifLoading.Visible = false;
                //}
            }
        }

        private void cmbHojas_SelectedIndexChanged(object sender, EventArgs e)
        {
            dgvDatos.DataSource = null;
            lbNReg.Text = "N° de registros: " + 0;

            lbNReg.Invoke((MethodInvoker)delegate { GifLoading.Visible = true; });
            Application.DoEvents();

            dgvDatos.DataSource = dtsTabla.Tables[cmbHojas.SelectedIndex];
            dgvDatos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;

            int nReg = dgvDatos.RowCount;
            lbNReg.Text = "N° de registros: " + nReg;

            lbNReg.Invoke((MethodInvoker)delegate { GifLoading.Visible = false; });
            Application.DoEvents();
        }

        private void btnRegData_Click(object sender, EventArgs e)
        {
            int[] colVerifOrd = { 0, 1, 2, 3, 5, 6, 7, 10, 11, 12, 13, 14, 15, 16, 17 };
            int[] colVerifCc = { 0, 1, 2 };
            int[] colVerifMat = { 0, 1, 2, 4 };
            int[] colVerifTdm = { 0, 1, 2 };
            //DataTable data;
            int[] colVerif = {};
            string tipoMaestro = string.Empty, tablaMaestro = string.Empty, nomMaestro = string.Empty, nomColTabla = string.Empty;
            int numCol = 0;

            if (string.IsNullOrEmpty(txtRuta.Text))
            {
                MessageBox.Show("Debe cargar algún archivo", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtRuta.Focus();
            }
            else
            {
                DataTable data = (DataTable)(dgvDatos.DataSource);

                switch (cmbHojas.SelectedIndex)
                {
                    case 0: //Ord
                        tipoMaestro = "ORD";
                        colVerif = colVerifOrd;
                        tablaMaestro = "TBL_Ordenes_Ceco";
                        numCol = 1;
                        nomMaestro = "Ordenes - CeCo";
                        nomColTabla = "Id";
                        break;
                    case 1: //Cuent_Cont
                        tipoMaestro = "CC";
                        colVerif = colVerifCc;
                        tablaMaestro = "TBL_Cta_Contables";
                        numCol = 0;
                        nomMaestro = "Cuentas Contables";
                        nomColTabla = "Cta_Contable";
                        break;
                    case 2: //Mat
                        tipoMaestro = "MAT";
                        colVerif = colVerifMat;
                        tablaMaestro = "TBL_Materiales";
                        numCol = 0;
                        nomMaestro = "Materiales";
                        nomColTabla = "N_Material";
                        break;
                    case 3: //Tipo_Mant
                        tipoMaestro = "TDM";
                        colVerif = colVerifTdm;
                        tablaMaestro = "TBL_Tipo_Man";
                        numCol = 0;
                        nomMaestro = "Tipos de Manteminiento";
                        nomColTabla = "Cta_Contable";
                        break;
                }

                if (Verificar(data, tipoMaestro))
                {
                    if (VerifColNull(data, colVerif))
                    {
                        EliminarFilas(data, tablaMaestro, nomMaestro, numCol, nomColTabla);
                        new MqtMaestrosCarga().CargaData_MqtMaestros(data, tipoMaestro);
                        lbPorcentaje.Text = 100 + "%";
                        progressBar.Value = 100;

                        MessageBox.Show("El archivo se ha cargado correctamente.");
                    }
                }
            }
        }


        //PAGINA CUENTAS CONTABLES
        //private void btnCargar_Cc_Click(object sender, EventArgs e)
        //{
        //    cmbHojas_Cc.Items.Clear();

        //    lbPorcentaje_Cc.Text = 0 + "%";
        //    progressBar_Cc.Value = 0;

        //    txtRuta_Cc.Clear();

        //    OpenFileDialog openFileDialog = new OpenFileDialog
        //    {
        //        Filter = "Excel Worbook|*.xls;*.xlsx"
        //    };

        //    if (openFileDialog.ShowDialog() == DialogResult.OK)
        //    {
        //        dgvDatos_Cc.DataSource = null;

        //        txtRuta_Cc.Text = openFileDialog.FileName;

        //        try
        //        {
        //            using (var stream = File.Open(openFileDialog.FileName, FileMode.Open, FileAccess.Read))
        //            {
        //                using (var reader = ExcelReaderFactory.CreateReader(stream))
        //                {
        //                    dtsTabla = reader.AsDataSet(new ExcelDataSetConfiguration()
        //                    {
        //                        ConfigureDataTable = (tableReader) => new ExcelDataTableConfiguration()
        //                        {
        //                            UseHeaderRow = true
        //                        }
        //                    });

        //                    foreach (DataTable tabla in dtsTabla.Tables)
        //                    {
        //                        cmbHojas_Cc.Items.Add(tabla.TableName);
        //                    }
        //                    cmbHojas_Cc.SelectedIndex = 0;

        //                    dgvDatos_Cc.DataSource = dtsTabla.Tables[cmbHojas_Cc.SelectedIndex];
        //                    dgvDatos_Cc.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
        //                }
        //            }
        //        }
        //        catch (IOException) { MessageBox.Show("El archivo está siendo utilizado por otro proceso.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        //    }
        //}

        //private void cmbHojas_Cc_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    dgvDatos_Cc.DataSource = dtsTabla.Tables[cmbHojas_Cc.SelectedIndex];
        //    dgvDatos_Cc.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
        //}

        //private void btnRegData_Cc_Click(object sender, EventArgs e)
        //{
        //    int[] colVerif = { 0, 1, 2 };

        //    if (string.IsNullOrEmpty(txtRuta_Cc.Text))
        //    {
        //        MessageBox.Show("Debe cargar algún archivo", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        //        txtRuta_Cc.Focus();
        //    }
        //    else
        //    {
        //        DataTable data = (DataTable)(dgvDatos_Cc.DataSource);

        //        if (Verificar(data, "CC"))
        //        {
        //            if (VerifColNull(data, colVerif))
        //            {
        //                EliminarFilas(data, "TBL_Cta_Contables", "Cuentas Contables", 0, "Cta_Contable");
        //                new MqtMaestrosCarga().CargaData_MqtMaestros(data, "CC");
        //                lbPorcentaje_Cc.Text = 100 + "%";
        //                progressBar_Cc.Value = 100;

        //                MessageBox.Show("El archivo se ha cargado correctamente.");
        //            }
        //        }
        //    }
        //}

        //PAGINA MATERIALES
        //private void btnCargar_Mat_Click(object sender, EventArgs e)
        //{
        //    cmbHojas_Mat.Items.Clear();

        //    lbPorcentaje_Mat.Text = 0 + "%";
        //    progressBar_Mat.Value = 0;

        //    txtRuta_Mat.Clear();

        //    OpenFileDialog openFileDialog = new OpenFileDialog
        //    {
        //        Filter = "Excel Worbook|*.xls;*.xlsx"
        //    };

        //    if (openFileDialog.ShowDialog() == DialogResult.OK)
        //    {
        //        dgvDatos_Mat.DataSource = null;

        //        txtRuta_Mat.Text = openFileDialog.FileName;

        //        try
        //        {
        //            using (var stream = File.Open(openFileDialog.FileName, FileMode.Open, FileAccess.Read))
        //            {
        //                using (var reader = ExcelReaderFactory.CreateReader(stream))
        //                {
        //                    dtsTabla = reader.AsDataSet(new ExcelDataSetConfiguration()
        //                    {
        //                        ConfigureDataTable = (tableReader) => new ExcelDataTableConfiguration()
        //                        {
        //                            UseHeaderRow = true
        //                        }
        //                    });

        //                    foreach (DataTable tabla in dtsTabla.Tables)
        //                    {
        //                        cmbHojas_Mat.Items.Add(tabla.TableName);
        //                    }
        //                    cmbHojas_Mat.SelectedIndex = 0;

        //                    dgvDatos_Mat.DataSource = dtsTabla.Tables[cmbHojas_Mat.SelectedIndex];
        //                    dgvDatos_Mat.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
        //                }
        //            }
        //        }
        //        catch (IOException) { MessageBox.Show("El archivo está siendo utilizado por otro proceso.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        //    }
        //}

        //private void cmbHojas_Mat_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    dgvDatos_Mat.DataSource = dtsTabla.Tables[cmbHojas_Mat.SelectedIndex];
        //    dgvDatos_Mat.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
        //}

        //private void btnRegData_Mat_Click(object sender, EventArgs e)
        //{
        //    int[] colVerif = { 0, 1, 2, 4 };

        //    if (string.IsNullOrEmpty(txtRuta_Mat.Text))
        //    {
        //        MessageBox.Show("Debe cargar algún archivo", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        //        txtRuta_Mat.Focus();
        //    }
        //    else
        //    {
        //        DataTable data = (DataTable)(dgvDatos_Mat.DataSource);

        //        if (Verificar(data, "MAT"))
        //        {
        //            if (VerifColNull(data, colVerif))
        //            {
        //                EliminarFilas(data, "TBL_Materiales", "Materiales", 0, "N_Material");
        //                new MqtMaestrosCarga().CargaData_MqtMaestros(data, "MAT");
        //                lbPorcentaje_Mat.Text = 100 + "%";
        //                progressBar_Mat.Value = 100;

        //                MessageBox.Show("El archivo se ha cargado correctamente.");
        //            }
        //        }
        //    }
        //}

        //PAGINA AGRUPADORES
        //private void btnCargar_Agr_Click(object sender, EventArgs e)
        //{
        //    lbPorcentaje_Agr.Text = 0 + "%";
        //    progressBar_Agr.Value = 0;

        //    txtRuta_Agr.Clear();

        //    OpenFileDialog openFileDialog = new OpenFileDialog
        //    {
        //        Filter = "Excel Worbook|*.xls;*.xlsx"
        //    };

        //    if (openFileDialog.ShowDialog() == DialogResult.OK)
        //    {
        //        dgvDatos_Agr.DataSource = null;

        //        txtRuta_Agr.Text = openFileDialog.FileName;

        //        try
        //        {
        //            using (var stream = File.Open(openFileDialog.FileName, FileMode.Open, FileAccess.Read))
        //            {
        //                using (var reader = ExcelReaderFactory.CreateReader(stream))
        //                {
        //                    dtsTabla = reader.AsDataSet(new ExcelDataSetConfiguration()
        //                    {
        //                        ConfigureDataTable = (tableReader) => new ExcelDataTableConfiguration()
        //                        {
        //                            UseHeaderRow = true
        //                        }
        //                    });

        //                    dgvDatos_Agr.DataSource = dtsTabla.Tables[0];
        //                    dgvDatos_Agr.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
        //                }
        //            }
        //        }
        //        catch (IOException) { MessageBox.Show("El archivo está siendo utilizado por otro proceso.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        //    }
        //}

        //private void btnRegData_Agr_Click(object sender, EventArgs e)
        //{
        //    int[] colVerif = { 0, 1, 2, 3 };

        //    if (string.IsNullOrEmpty(txtRuta_Agr.Text))
        //    {
        //        MessageBox.Show("Debe cargar algún archivo", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        //        txtRuta_Agr.Focus();
        //    }
        //    else
        //    {
        //        DataTable data = (DataTable)(dgvDatos_Agr.DataSource);

        //        if (Verificar(data, "AGR"))
        //        {
        //            if (VerifColNull(data, colVerif))
        //            {
        //                EliminarFilas(data, "TBL_Agrupadores", "Agrupadores", 0, "Id_Agrupador");
        //                new MqtMaestrosCarga().CargaData_MqtMaestros(data, "AGR");
        //                lbPorcentaje_Agr.Text = 100 + "%";
        //                progressBar_Agr.Value = 100;

        //                MessageBox.Show("El archivo se ha cargado correctamente.");
        //            }
        //        }
        //    }
        //}

        //PAGINA TIPOS DE MANTENIMIENTO
        //private void btnCargar_Tdm_Click(object sender, EventArgs e)
        //{
        //    cmbHojas_Tdm.Items.Clear();

        //    lbPorcentaje_Tdm.Text = 0 + "%";
        //    progressBar_Tdm.Value = 0;

        //    txtRuta_Tdm.Clear();

        //    OpenFileDialog openFileDialog = new OpenFileDialog
        //    {
        //        Filter = "Excel Worbook|*.xls;*.xlsx"
        //    };

        //    if (openFileDialog.ShowDialog() == DialogResult.OK)
        //    {
        //        dgvDatos_Tdm.DataSource = null;

        //        txtRuta_Tdm.Text = openFileDialog.FileName;

        //        try
        //        {
        //            using (var stream = File.Open(openFileDialog.FileName, FileMode.Open, FileAccess.Read))
        //            {
        //                using (var reader = ExcelReaderFactory.CreateReader(stream))
        //                {
        //                    dtsTabla = reader.AsDataSet(new ExcelDataSetConfiguration()
        //                    {
        //                        ConfigureDataTable = (tableReader) => new ExcelDataTableConfiguration()
        //                        {
        //                            UseHeaderRow = true
        //                        }
        //                    });

        //                    foreach (DataTable tabla in dtsTabla.Tables)
        //                    {
        //                        cmbHojas_Tdm.Items.Add(tabla.TableName);
        //                    }
        //                    cmbHojas_Tdm.SelectedIndex = 0;

        //                    dgvDatos_Tdm.DataSource = dtsTabla.Tables[cmbHojas_Tdm.SelectedIndex];
        //                    dgvDatos_Tdm.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
        //                }
        //            }
        //        }
        //        catch (IOException) { MessageBox.Show("El archivo está siendo utilizado por otro proceso.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        //    }
        //}

        //private void cmbHojas_Tdm_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    dgvDatos_Tdm.DataSource = dtsTabla.Tables[cmbHojas_Tdm.SelectedIndex];
        //    dgvDatos_Tdm.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
        //}

        //private void btnRegData_Tdm_Click(object sender, EventArgs e)
        //{
        //    int[] colVerif = { 0, 1, 2 };

        //    if (string.IsNullOrEmpty(txtRuta_Tdm.Text))
        //    {
        //        MessageBox.Show("Debe cargar algún archivo", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        //        txtRuta_Tdm.Focus();
        //    }
        //    else
        //    {
        //        DataTable data = (DataTable)(dgvDatos_Tdm.DataSource);

        //        if (Verificar(data, "TDM"))
        //        {
        //            if (VerifColNull(data, colVerif))
        //            {
        //                EliminarFilas(data, "TBL_Tipo_Man", "Tipos de Mantenimiento", 0, "Cta_Contable");
        //                new MqtMaestrosCarga().CargaData_MqtMaestros(data, "TDM");
        //                lbPorcentaje_Tdm.Text = 100 + "%";
        //                progressBar_Tdm.Value = 100;

        //                MessageBox.Show("El archivo se ha cargado correctamente.");
        //            }
        //        }
        //    }
        //}

        //PAGINA JEFES REVISORES
        //private void btnCargar_Jr_Click(object sender, EventArgs e)
        //{
        //    lbPorcentaje_Jr.Text = 0 + "%";
        //    progressBar_Jr.Value = 0;

        //    txtRuta_Jr.Clear();

        //    OpenFileDialog openFileDialog = new OpenFileDialog
        //    {
        //        Filter = "Excel Worbook|*.xls;*.xlsx"
        //    };

        //    if (openFileDialog.ShowDialog() == DialogResult.OK)
        //    {
        //        dgvDatos_Jr.DataSource = null;

        //        txtRuta_Jr.Text = openFileDialog.FileName;

        //        try
        //        {
        //            using (var stream = File.Open(openFileDialog.FileName, FileMode.Open, FileAccess.Read))
        //            {
        //                using (var reader = ExcelReaderFactory.CreateReader(stream))
        //                {
        //                    dtsTabla = reader.AsDataSet(new ExcelDataSetConfiguration()
        //                    {
        //                        ConfigureDataTable = (tableReader) => new ExcelDataTableConfiguration()
        //                        {
        //                            UseHeaderRow = true
        //                        }
        //                    });

        //                    dgvDatos_Jr.DataSource = dtsTabla.Tables[0];
        //                    dgvDatos_Jr.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
        //                }
        //            }
        //        }
        //        catch (IOException) { MessageBox.Show("El archivo está siendo utilizado por otro proceso.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        //    }
        //}

        //private void btnRegData_Jr_Click(object sender, EventArgs e)
        //{
        //    int[] colVerif = { 0, 1, 2 };

        //    if (string.IsNullOrEmpty(txtRuta_Jr.Text))
        //    {
        //        MessageBox.Show("Debe cargar algún archivo", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        //        txtRuta_Jr.Focus();
        //    }
        //    else
        //    {
        //        DataTable data = (DataTable)(dgvDatos_Jr.DataSource);

        //        if (Verificar(data, "JR"))
        //        {
        //            if (VerifColNull(data, colVerif))
        //            {
        //                EliminarFilas(data, "TBL_Jefe_Revisor", "Jefes Revisores", 0, "Id_Agrupador");
        //                new MqtMaestrosCarga().CargaData_MqtMaestros(data, "JR");
        //                lbPorcentaje_Jr.Text = 100 + "%";
        //                progressBar_Jr.Value = 100;

        //                MessageBox.Show("El archivo se ha cargado correctamente.");
        //            }
        //        }
        //    }
        //}


        private void tabControl_DrawItem(object sender, DrawItemEventArgs e)
        {
            Font font;
            Brush brushB, brushF;

            if (e.Index == indDic)
            {
                font = e.Font;
                brushB = new SolidBrush(Color.LightSkyBlue);
                brushF = new SolidBrush(Color.Black);
            }
            else
            {
                font = e.Font;
                brushB = new SolidBrush(Color.White);
                brushF = new SolidBrush(Color.Black);
            }

            if (e.Index == this.tabControl.SelectedIndex)
            {
                font = new Font(e.Font, FontStyle.Bold);
            }

            string tabName = this.tabControl.TabPages[e.Index].Text;
            StringFormat sftTab = new StringFormat(StringFormatFlags.NoWrap);
            sftTab.Alignment = StringAlignment.Center;
            sftTab.LineAlignment = StringAlignment.Center;
            e.Graphics.FillRectangle(brushB, e.Bounds);
            Rectangle recTab = e.Bounds;
            recTab = new Rectangle(recTab.X - 2, recTab.Y, recTab.Width + 4, recTab.Height + 4);
            e.Graphics.DrawString(tabName, font, brushF, recTab, sftTab);
        }
        
        private void tabDicControl_DrawItem(object sender, DrawItemEventArgs e)
        {
            Font font;
            Brush brushB, brushF;

            if (e.Index == this.tabDicControl.SelectedIndex)
            {
                font = new Font(e.Font, FontStyle.Bold);
                brushB = new SolidBrush(Color.LightBlue);
                brushF = new SolidBrush(Color.Black);
            }
            else
            {
                font = e.Font;
                brushB = new SolidBrush(Color.Azure);
                brushF = new SolidBrush(Color.Black);
            }

            string tabName = this.tabDicControl.TabPages[e.Index].Text;
            StringFormat sftTab = new StringFormat(StringFormatFlags.NoWrap);
            sftTab.Alignment = StringAlignment.Center;
            sftTab.LineAlignment = StringAlignment.Center;
            e.Graphics.FillRectangle(brushB, e.Bounds);
            Rectangle recTab = e.Bounds;
            recTab = new Rectangle(recTab.X - 2, recTab.Y, recTab.Width + 4, recTab.Height + 4);
            e.Graphics.DrawString(tabName, font, brushF, recTab, sftTab);
        }

        private void ui_mqt_gastos_maestros_Load(object sender, EventArgs e)
        {
            tabControl.DrawMode = TabDrawMode.OwnerDrawFixed;
            tabDicControl.DrawMode = TabDrawMode.OwnerDrawFixed;
        }

        //FUNCIONES
        public bool VerifColNull(DataTable tabla, int[] colVerif)
        {
            // Filtrar las filas que contienen valores nulos en las columnas especificadas
            var filasValNull = tabla.AsEnumerable()
                .Where(fila => colVerif.Any(colIndex => fila.IsNull(colIndex)));

            // Recorrer las filas con valores nulos
            foreach (DataRow fila in filasValNull)
            {
                foreach (int colIndex in colVerif)
                {
                    if (fila.IsNull(colIndex))
                    {
                        MessageBox.Show($"Se detectó una celda vacía en la columna '{tabla.Columns[colIndex]}', fila {fila.Table.Rows.IndexOf(fila) + 2}.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        //Console.WriteLine($"La fila {tabla.Rows.IndexOf(fila) + 1} tiene un valor nulo en la columna {colIndex + 1}");
                        return false;
                    }
                }
            }
            return true;
        }
        
        public void EliminarFilas(DataTable tabla, string nomTabla, string nomMaestro, int numCol, string nomColTabla)
        {
            int nRows = 0;

            SqlConnection conexion = new SqlConnection { ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION_2") };
            conexion.Open();

            try
            {
                using (SqlCommand createTempTableCommand = new SqlCommand("CREATE TABLE #TempToDelete (Value NVARCHAR(MAX));", conexion))
                {
                    createTempTableCommand.ExecuteNonQuery();
                }

                string query = string.Empty;
                string query2 = string.Empty;
                query = $"SELECT COUNT(*) AS N_Rows FROM {nomTabla};";
                Console.WriteLine(query);
                SqlCommand myCommand = new SqlCommand(query, conexion);
                SqlDataReader dr = myCommand.ExecuteReader();

                if (dr.Read()) nRows = dr.GetInt32(dr.GetOrdinal("N_Rows"));

                dr.Close();
                myCommand.Dispose();

                if (nRows != 0)
                {
                    MessageBox.Show($"Ya se tiene información del 'Maestro de {nomMaestro}', se procederá a actualizar", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                    //var valCol = tabla.AsEnumerable().Select(row => row.Field<tpDato>(numCol)).ToArray();
                    string[] valCol = new string[tabla.Rows.Count];
                    if (nomMaestro == "Ordenes - CeCo") { for (int i = 0; i < tabla.Rows.Count; i++) { valCol[i] = tabla.Rows[i][numCol].ToString() + tabla.Rows[i][numCol + 1].ToString(); } }
                    else { for (int i = 0; i < tabla.Rows.Count; i++) { valCol[i] = tabla.Rows[i][numCol].ToString(); } }

                    // Insertar valores en la tabla temporal
                    using (SqlBulkCopy bulkCopy = new SqlBulkCopy(conexion))
                    {
                        DataTable tempTable = new DataTable();
                        tempTable.Columns.Add("Value", typeof(string));
                        foreach (string val in valCol)
                        {
                            tempTable.Rows.Add(val);
                        }

                        bulkCopy.DestinationTableName = "#TempToDelete";
                        bulkCopy.WriteToServer(tempTable);
                    }

                    // Eliminar usando la tabla temporal
                    query2 = $@"
                    DELETE FROM {nomTabla}
                    WHERE {nomColTabla} IN (SELECT Value FROM #TempToDelete);";

                    using (SqlCommand myCommand2 = new SqlCommand(query2, conexion))
                    {
                        int filasAfectadas = myCommand2.ExecuteNonQuery();
                        Console.WriteLine($"{filasAfectadas} filas eliminadas.");
                    }
                    //query2 = $"DELETE FROM {nomTabla} WHERE {nomColTabla} IN ('";

                    //// Crear la consulta DELETE con los valores obtenidos
                    //string regElimnar = string.Join("', '", valCol);
                    //query2 = $"DELETE FROM {nomTabla} WHERE {nomColTabla} IN ('{regElimnar}');";
                    //Console.WriteLine(query2);

                    //conexion.Open();
                    //SqlCommand myCommand2 = new SqlCommand(query2, conexion);
                    //int filasAfectadas = myCommand2.ExecuteNonQuery(); // Ejecutar la consulta DELETE
                    //Console.WriteLine($"{filasAfectadas} filas eliminadas.");
                    //myCommand2.Dispose();

                    //for (int i = 0; i < valCol.Length; i++)
                    //{
                    //    string valor = valCol[i].ToString();

                    //    if (i != valCol.Length - 1) { query2 += valor + "', '"; }
                    //    else { query2 += valor + "')"; }
                    //}
                    //Console.WriteLine(query2);
                    //SqlCommand myCommand2 = new SqlCommand(query2, conexion);
                    //myCommand2.ExecuteNonQuery();
                    //myCommand2.Dispose();
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("Exception:" + ex.Message);
            }
            finally {
                // Eliminar la tabla temporal
                using (SqlCommand dropTempTableCommand = new SqlCommand("DROP TABLE #TempToDelete;", conexion))
                {
                    dropTempTableCommand.ExecuteNonQuery();
                }
                conexion.Close();
            }
        }

        public bool Verificar(DataTable tabla, string tipoMaestro)
        {
            switch (tipoMaestro)
            {
                case "ORD":
                    if (tabla.Columns.Count == nColOrd) { if (VerifNomCol(tabla, tipoMaestro) == false) return false; }
                    else
                    {
                        MessageBox.Show("Faltan columnas en el archivo.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return false;
                    }
                    break;
                case "CC":
                    if (tabla.Columns.Count == nColCc) { if (VerifNomCol(tabla, tipoMaestro) == false) return false; }
                    else
                    {
                        MessageBox.Show("Faltan columnas en el archivo.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return false;
                    }
                    break;
                case "MAT":
                    if (tabla.Columns.Count == nColMat) { if (VerifNomCol(tabla, tipoMaestro) == false) return false; }
                    else
                    {
                        MessageBox.Show("Faltan columnas en el archivo.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return false;
                    }
                    break;
                case "AGR":
                    if (tabla.Columns.Count == nColAgr) { if (VerifNomCol(tabla, tipoMaestro) == false) return false; }
                    else
                    {
                        MessageBox.Show("Faltan columnas en el archivo.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return false;
                    }
                    break;
                case "TDM":
                    if (tabla.Columns.Count == nColTdm) { if (VerifNomCol(tabla, tipoMaestro) == false) return false; }
                    else
                    {
                        MessageBox.Show("Faltan columnas en el archivo.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return false;
                    }
                    break;
                case "JR":
                    if (tabla.Columns.Count == nColJr) { if (VerifNomCol(tabla, tipoMaestro) == false) return false; }
                    else
                    {
                        MessageBox.Show("Faltan columnas en el archivo.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return false;
                    }
                    break;
            }

            return true;
        }

        public bool VerifNomCol(DataTable tabla, string tipoMaestro)
        {
            for (int i = 0; i < tabla.Columns.Count; i++)
            {
                string nombreColumna = tabla.Columns[i].ToString();
                if (!string.IsNullOrEmpty(nombreColumna))
                {
                    switch (tipoMaestro)
                    {
                        case "ORD":
                            if (nombreColumna != NomCol(i, tipoMaestro))
                            {
                                MessageBox.Show($"El nombre de la columna '{nombreColumna}' no coincide con el esperado: '{NomCol(i, tipoMaestro)}'.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return false;
                            }
                            break;
                        case "CC":
                            if (nombreColumna != NomCol(i, tipoMaestro))
                            {
                                MessageBox.Show($"El nombre de la columna '{nombreColumna}' no coincide con el esperado: '{NomCol(i, tipoMaestro)}'.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return false;
                            }
                            break;
                        case "MAT":
                            if (nombreColumna != NomCol(i, tipoMaestro))
                            {
                                MessageBox.Show($"El nombre de la columna '{nombreColumna}' no coincide con el esperado: '{NomCol(i, tipoMaestro)}'.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return false;
                            }
                            break;
                        case "AGR":
                            if (nombreColumna != NomCol(i, tipoMaestro))
                            {
                                MessageBox.Show($"El nombre de la columna '{nombreColumna}' no coincide con el esperado: '{NomCol(i, tipoMaestro)}'.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return false;
                            }
                            break;
                        case "TDM":
                            if (nombreColumna != NomCol(i, tipoMaestro))
                            {
                                MessageBox.Show($"El nombre de la columna '{nombreColumna}' no coincide con el esperado: '{NomCol(i, tipoMaestro)}'.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return false;
                            }
                            break;
                        case "JR":
                            if (nombreColumna != NomCol(i, tipoMaestro))
                            {
                                MessageBox.Show($"El nombre de la columna '{nombreColumna}' no coincide con el esperado: '{NomCol(i, tipoMaestro)}'.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return false;
                            }
                            break;
                    }
                }
                else
                {
                    MessageBox.Show($"La columna esta vacía, se espereba: '{NomCol(i, tipoMaestro)}'.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
            }

            return true;
        }

        public string NomCol(int indice, string tipoMaestro)
        {
            switch (tipoMaestro)
            {
                case "ORD":
                    string[] nomColOrd = { "Soc", "Orden_Ceco", "Cta_Contable", "Desc_Ceco_Orden", "CeBe", "Tipo_Gasto", "Gerencia", "Jefe_Responsable", "Macro_Fundo", "Area", "Sistema", "Subsistema", "Id_Agrupador", "Agrupador", "Id_Jefe_Revisor", "Jefe_Revisor", "Gasto", "Estado" };
                    return nomColOrd[indice];
                case "CC":
                    string[] nomColCc = { "Cta_Contable", "Desc_Cta_Contable", "Estado" };
                    return nomColCc[indice];
                case "MAT":
                    string[] nomColMat = { "N_Material", "Desc_Material", "UM", "Status", "Estado" };
                    return nomColMat[indice];
                case "AGR":
                    string[] nomColAgr = { "Id_Agrupador", "Cta_Contable", "Agrupador", "Estado" };
                    return nomColAgr[indice];
                case "TDM":
                    string[] nomColTdm = { "Cta_Contable", "Desc_Tipo_Man", "Estado" };
                    return nomColTdm[indice];
                case "JR":
                    string[] nomColJr = { "Id_Agrupador", "Jefe_Revisor", "Estado" };
                    return nomColJr[indice];
            }

            return string.Empty;
        }

        public void Diccionario()
        {
            string[] nomTablas = { "TBL_Ordenes_Ceco", "TBL_Cta_Contables", "TBL_Materiales", "TBL_Tipo_Man" };
            DataGridView[] tablas = { dgvDic_Ord, dgvDic_Cc, dgvDic_Mat, dgvDic_Tdm };

            for (int i = 0; i < 4; i++)
            {
                SqlConnection conexion = new SqlConnection { ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION_2") };
                conexion.Open();
                try
                {
                    string query = string.Empty;
                    query = $@"
                            SELECT 
                                COLUMN_NAME AS Columna,
                                CASE WHEN DATA_TYPE = 'int' THEN 'Número' ELSE 'Texto' END AS 'Tipo de Dato',
                                CASE
                                    WHEN COLUMN_NAME = 'Estado' THEN '1 dígito'
                                    WHEN DATA_TYPE IN('int', 'smallint', 'tinyint', 'bigint') THEN CAST(COALESCE(NUMERIC_PRECISION, CHARACTER_MAXIMUM_LENGTH) AS VARCHAR) +' dígitos'
                                    WHEN DATA_TYPE IN('varchar', 'char', 'text', 'nvarchar', 'nchar', 'ntext') THEN
                                        CASE
                                            WHEN CHARACTER_MAXIMUM_LENGTH = -1 THEN 'máxima' ELSE CAST(CHARACTER_MAXIMUM_LENGTH AS VARCHAR) +' caracteres'
                                        END
                                    ELSE CAST(COALESCE(CHARACTER_MAXIMUM_LENGTH, NUMERIC_PRECISION) AS VARCHAR)
                                END AS Longitud,
                                CASE WHEN IS_NULLABLE = 'YES' THEN 'Sí' ELSE 'No' END AS 'Valores Nulos'
                            FROM INFORMATION_SCHEMA.COLUMNS
                            WHERE TABLE_NAME = '{nomTablas[i]}' AND COLUMN_NAME != 'Id'";

                    SqlCommand myCommand = new SqlCommand(query, conexion);
                    SqlDataAdapter adapter = new SqlDataAdapter(myCommand);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    tablas[i].DataSource = dataTable;
                    tablas[i].AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                }
                catch (SqlException ex)
                {
                    Console.WriteLine("Exception:" + ex.Message);
                }
                finally { conexion.Close(); }
            }
        }
    }
}
