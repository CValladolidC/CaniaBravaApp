namespace CaniaBrava
{
    partial class ui_rep_asistencia
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.toolstripform = new System.Windows.Forms.ToolStrip();
            this.btnGenerar = new System.Windows.Forms.ToolStripButton();
            this.btnExcel = new System.Windows.Forms.ToolStripButton();
            this.btnNuevo = new System.Windows.Forms.ToolStripButton();
            this.btnEditar = new System.Windows.Forms.ToolStripButton();
            this.btnEliminar = new System.Windows.Forms.ToolStripButton();
            this.dgvdetalle = new System.Windows.Forms.DataGridView();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.chkMasivo = new System.Windows.Forms.CheckBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cmbCencos = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.cmbGerencia = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.cmbCia = new System.Windows.Forms.ComboBox();
            this.lblregistros = new System.Windows.Forms.Label();
            this.dtpFecfin = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.dtpFecini = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.loadingNext1 = new System.Windows.Forms.PictureBox();
            this.grTrabajador = new System.Windows.Forms.GroupBox();
            this.chkIndividual = new System.Windows.Forms.CheckBox();
            this.pictureBoxBuscar = new System.Windows.Forms.PictureBox();
            this.lblF2 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.txtNombres = new System.Windows.Forms.TextBox();
            this.txtNroDocIden = new System.Windows.Forms.TextBox();
            this.txtDocIdent = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtCodigoInterno = new System.Windows.Forms.TextBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.cmbNominas = new System.Windows.Forms.ComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.cmbSedes = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.toolstripform.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvdetalle)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.loadingNext1)).BeginInit();
            this.grTrabajador.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxBuscar)).BeginInit();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolstripform
            // 
            this.toolstripform.BackColor = System.Drawing.SystemColors.HighlightText;
            this.toolstripform.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.toolstripform.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnGenerar,
            this.btnExcel,
            this.btnNuevo,
            this.btnEditar,
            this.btnEliminar});
            this.toolstripform.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.toolstripform.Location = new System.Drawing.Point(0, 502);
            this.toolstripform.Name = "toolstripform";
            this.toolstripform.ShowItemToolTips = false;
            this.toolstripform.Size = new System.Drawing.Size(1187, 39);
            this.toolstripform.TabIndex = 8;
            // 
            // btnGenerar
            // 
            this.btnGenerar.Image = global::CaniaBrava.Properties.Resources.OK;
            this.btnGenerar.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnGenerar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnGenerar.Name = "btnGenerar";
            this.btnGenerar.Size = new System.Drawing.Size(86, 36);
            this.btnGenerar.Text = "Generar";
            this.btnGenerar.Click += new System.EventHandler(this.btnGenerar_Click);
            // 
            // btnExcel
            // 
            this.btnExcel.Image = global::CaniaBrava.Properties.Resources.EXCEL;
            this.btnExcel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnExcel.Name = "btnExcel";
            this.btnExcel.Size = new System.Drawing.Size(98, 36);
            this.btnExcel.Text = "Enviar a Excel";
            this.btnExcel.Click += new System.EventHandler(this.btnExcel_Click);
            // 
            // btnNuevo
            // 
            this.btnNuevo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnNuevo.Image = global::CaniaBrava.Properties.Resources.zoommas;
            this.btnNuevo.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnNuevo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnNuevo.Name = "btnNuevo";
            this.btnNuevo.Size = new System.Drawing.Size(40, 36);
            this.btnNuevo.Text = "Nuevo";
            this.btnNuevo.Click += new System.EventHandler(this.btnNuevo_Click);
            // 
            // btnEditar
            // 
            this.btnEditar.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnEditar.Image = global::CaniaBrava.Properties.Resources.pencil_icon;
            this.btnEditar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnEditar.Name = "btnEditar";
            this.btnEditar.Size = new System.Drawing.Size(23, 36);
            this.btnEditar.Text = "toolStripButton1";
            this.btnEditar.Click += new System.EventHandler(this.btnEditar_Click);
            // 
            // btnEliminar
            // 
            this.btnEliminar.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnEliminar.Image = global::CaniaBrava.Properties.Resources.zoommenos;
            this.btnEliminar.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnEliminar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnEliminar.Name = "btnEliminar";
            this.btnEliminar.Size = new System.Drawing.Size(36, 36);
            this.btnEliminar.Text = "Editar";
            this.btnEliminar.Click += new System.EventHandler(this.btnEliminar_Click);
            // 
            // dgvdetalle
            // 
            this.dgvdetalle.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvdetalle.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvdetalle.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvdetalle.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvdetalle.Location = new System.Drawing.Point(378, 12);
            this.dgvdetalle.Name = "dgvdetalle";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvdetalle.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dgvdetalle.Size = new System.Drawing.Size(800, 487);
            this.dgvdetalle.TabIndex = 7;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.chkMasivo);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.cmbCencos);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.cmbGerencia);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.cmbCia);
            this.groupBox1.Location = new System.Drawing.Point(11, 181);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(361, 105);
            this.groupBox1.TabIndex = 13;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Masivo";
            // 
            // chkMasivo
            // 
            this.chkMasivo.AutoSize = true;
            this.chkMasivo.Location = new System.Drawing.Point(46, 0);
            this.chkMasivo.Name = "chkMasivo";
            this.chkMasivo.Size = new System.Drawing.Size(15, 14);
            this.chkMasivo.TabIndex = 57;
            this.chkMasivo.UseVisualStyleBackColor = true;
            this.chkMasivo.CheckedChanged += new System.EventHandler(this.chkMasivo_CheckedChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(25, 76);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(32, 13);
            this.label4.TabIndex = 26;
            this.label4.Text = "Área:";
            // 
            // cmbCencos
            // 
            this.cmbCencos.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCencos.Enabled = false;
            this.cmbCencos.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbCencos.FormattingEnabled = true;
            this.cmbCencos.Location = new System.Drawing.Point(63, 74);
            this.cmbCencos.Name = "cmbCencos";
            this.cmbCencos.Size = new System.Drawing.Size(289, 20);
            this.cmbCencos.TabIndex = 25;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(4, 50);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(53, 13);
            this.label7.TabIndex = 24;
            this.label7.Text = "Gerencia:";
            // 
            // cmbGerencia
            // 
            this.cmbGerencia.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbGerencia.Enabled = false;
            this.cmbGerencia.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbGerencia.FormattingEnabled = true;
            this.cmbGerencia.Location = new System.Drawing.Point(63, 48);
            this.cmbGerencia.Name = "cmbGerencia";
            this.cmbGerencia.Size = new System.Drawing.Size(289, 20);
            this.cmbGerencia.TabIndex = 23;
            this.cmbGerencia.SelectedIndexChanged += new System.EventHandler(this.cmbGerencia_SelectedIndexChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 25);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(51, 13);
            this.label6.TabIndex = 22;
            this.label6.Text = "Empresa:";
            // 
            // cmbCia
            // 
            this.cmbCia.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCia.Enabled = false;
            this.cmbCia.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbCia.FormattingEnabled = true;
            this.cmbCia.Location = new System.Drawing.Point(63, 22);
            this.cmbCia.Name = "cmbCia";
            this.cmbCia.Size = new System.Drawing.Size(289, 20);
            this.cmbCia.TabIndex = 21;
            this.cmbCia.SelectedIndexChanged += new System.EventHandler(this.cmbCia_SelectedIndexChanged);
            // 
            // lblregistros
            // 
            this.lblregistros.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblregistros.Location = new System.Drawing.Point(8, 486);
            this.lblregistros.Name = "lblregistros";
            this.lblregistros.Size = new System.Drawing.Size(156, 13);
            this.lblregistros.TabIndex = 27;
            this.lblregistros.Text = "0 registrados encontrados...";
            this.lblregistros.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // dtpFecfin
            // 
            this.dtpFecfin.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFecfin.Location = new System.Drawing.Point(80, 48);
            this.dtpFecfin.Name = "dtpFecfin";
            this.dtpFecfin.Size = new System.Drawing.Size(95, 20);
            this.dtpFecfin.TabIndex = 19;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 52);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 13);
            this.label2.TabIndex = 18;
            this.label2.Text = "Fecha Final:";
            // 
            // dtpFecini
            // 
            this.dtpFecini.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFecini.Location = new System.Drawing.Point(80, 22);
            this.dtpFecini.Name = "dtpFecini";
            this.dtpFecini.Size = new System.Drawing.Size(95, 20);
            this.dtpFecini.TabIndex = 17;
            this.dtpFecini.ValueChanged += new System.EventHandler(this.dtpFecini_ValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(4, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(70, 13);
            this.label1.TabIndex = 16;
            this.label1.Text = "Fecha Inicial:";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.loadingNext1);
            this.groupBox2.Controls.Add(this.dtpFecfin);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.dtpFecini);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Location = new System.Drawing.Point(12, 96);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(360, 79);
            this.groupBox2.TabIndex = 14;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Rango: ";
            // 
            // loadingNext1
            // 
            this.loadingNext1.BackColor = System.Drawing.Color.Transparent;
            this.loadingNext1.ErrorImage = null;
            this.loadingNext1.Image = global::CaniaBrava.Properties.Resources.loading;
            this.loadingNext1.Location = new System.Drawing.Point(181, 20);
            this.loadingNext1.Name = "loadingNext1";
            this.loadingNext1.Size = new System.Drawing.Size(52, 49);
            this.loadingNext1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.loadingNext1.TabIndex = 101;
            this.loadingNext1.TabStop = false;
            this.loadingNext1.Visible = false;
            // 
            // grTrabajador
            // 
            this.grTrabajador.Controls.Add(this.chkIndividual);
            this.grTrabajador.Controls.Add(this.pictureBoxBuscar);
            this.grTrabajador.Controls.Add(this.lblF2);
            this.grTrabajador.Controls.Add(this.label5);
            this.grTrabajador.Controls.Add(this.label8);
            this.grTrabajador.Controls.Add(this.txtNombres);
            this.grTrabajador.Controls.Add(this.txtNroDocIden);
            this.grTrabajador.Controls.Add(this.txtDocIdent);
            this.grTrabajador.Controls.Add(this.label9);
            this.grTrabajador.Controls.Add(this.txtCodigoInterno);
            this.grTrabajador.Location = new System.Drawing.Point(12, 291);
            this.grTrabajador.Name = "grTrabajador";
            this.grTrabajador.Size = new System.Drawing.Size(360, 99);
            this.grTrabajador.TabIndex = 28;
            this.grTrabajador.TabStop = false;
            this.grTrabajador.Text = "Individual";
            // 
            // chkIndividual
            // 
            this.chkIndividual.AutoSize = true;
            this.chkIndividual.Location = new System.Drawing.Point(57, 1);
            this.chkIndividual.Name = "chkIndividual";
            this.chkIndividual.Size = new System.Drawing.Size(15, 14);
            this.chkIndividual.TabIndex = 56;
            this.chkIndividual.UseVisualStyleBackColor = true;
            this.chkIndividual.CheckedChanged += new System.EventHandler(this.chkIndividual_CheckedChanged);
            // 
            // pictureBoxBuscar
            // 
            this.pictureBoxBuscar.Image = global::CaniaBrava.Properties.Resources.LOCATE;
            this.pictureBoxBuscar.Location = new System.Drawing.Point(197, 23);
            this.pictureBoxBuscar.Name = "pictureBoxBuscar";
            this.pictureBoxBuscar.Size = new System.Drawing.Size(24, 21);
            this.pictureBoxBuscar.TabIndex = 55;
            this.pictureBoxBuscar.TabStop = false;
            // 
            // lblF2
            // 
            this.lblF2.AutoSize = true;
            this.lblF2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblF2.Location = new System.Drawing.Point(175, 27);
            this.lblF2.Name = "lblF2";
            this.lblF2.Size = new System.Drawing.Size(21, 13);
            this.lblF2.TabIndex = 54;
            this.lblF2.Text = "F2";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(9, 74);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(83, 13);
            this.label5.TabIndex = 6;
            this.label5.Text = "Doc. Identidad :";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(9, 48);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(64, 13);
            this.label8.TabIndex = 5;
            this.label8.Text = "Trabajador :";
            // 
            // txtNombres
            // 
            this.txtNombres.BackColor = System.Drawing.SystemColors.HighlightText;
            this.txtNombres.Enabled = false;
            this.txtNombres.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNombres.Location = new System.Drawing.Point(96, 46);
            this.txtNombres.Name = "txtNombres";
            this.txtNombres.Size = new System.Drawing.Size(255, 18);
            this.txtNombres.TabIndex = 1;
            // 
            // txtNroDocIden
            // 
            this.txtNroDocIden.BackColor = System.Drawing.SystemColors.HighlightText;
            this.txtNroDocIden.Enabled = false;
            this.txtNroDocIden.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNroDocIden.Location = new System.Drawing.Point(156, 70);
            this.txtNroDocIden.Name = "txtNroDocIden";
            this.txtNroDocIden.Size = new System.Drawing.Size(105, 18);
            this.txtNroDocIden.TabIndex = 3;
            // 
            // txtDocIdent
            // 
            this.txtDocIdent.BackColor = System.Drawing.SystemColors.HighlightText;
            this.txtDocIdent.Enabled = false;
            this.txtDocIdent.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDocIdent.Location = new System.Drawing.Point(96, 70);
            this.txtDocIdent.Name = "txtDocIdent";
            this.txtDocIdent.Size = new System.Drawing.Size(54, 18);
            this.txtDocIdent.TabIndex = 2;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(9, 26);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(82, 13);
            this.label9.TabIndex = 1;
            this.label9.Text = "Código Interno :";
            // 
            // txtCodigoInterno
            // 
            this.txtCodigoInterno.BackColor = System.Drawing.SystemColors.HighlightText;
            this.txtCodigoInterno.Enabled = false;
            this.txtCodigoInterno.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCodigoInterno.Location = new System.Drawing.Point(96, 23);
            this.txtCodigoInterno.MaxLength = 8;
            this.txtCodigoInterno.Name = "txtCodigoInterno";
            this.txtCodigoInterno.Size = new System.Drawing.Size(76, 18);
            this.txtCodigoInterno.TabIndex = 0;
            this.txtCodigoInterno.TextChanged += new System.EventHandler(this.txtCodigoInterno_TextChanged);
            this.txtCodigoInterno.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtCodigoInterno_KeyDown);
            this.txtCodigoInterno.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCodigoInterno_KeyPress);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.cmbNominas);
            this.groupBox3.Controls.Add(this.label10);
            this.groupBox3.Controls.Add(this.cmbSedes);
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Location = new System.Drawing.Point(12, 12);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(360, 78);
            this.groupBox3.TabIndex = 29;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Filtros: ";
            // 
            // cmbNominas
            // 
            this.cmbNominas.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbNominas.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbNominas.FormattingEnabled = true;
            this.cmbNominas.Location = new System.Drawing.Point(62, 45);
            this.cmbNominas.Name = "cmbNominas";
            this.cmbNominas.Size = new System.Drawing.Size(239, 20);
            this.cmbNominas.TabIndex = 19;
            this.cmbNominas.SelectedIndexChanged += new System.EventHandler(this.cmbNominas_SelectedIndexChanged);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(3, 48);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(57, 13);
            this.label10.TabIndex = 18;
            this.label10.Text = "Nominas : ";
            // 
            // cmbSedes
            // 
            this.cmbSedes.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSedes.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbSedes.FormattingEnabled = true;
            this.cmbSedes.Location = new System.Drawing.Point(62, 19);
            this.cmbSedes.Name = "cmbSedes";
            this.cmbSedes.Size = new System.Drawing.Size(239, 20);
            this.cmbSedes.TabIndex = 17;
            this.cmbSedes.SelectedIndexChanged += new System.EventHandler(this.cmbSedes_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(15, 22);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 13);
            this.label3.TabIndex = 16;
            this.label3.Text = "Sede : ";
            // 
            // ui_rep_asistencia
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.HighlightText;
            this.ClientSize = new System.Drawing.Size(1187, 541);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.lblregistros);
            this.Controls.Add(this.grTrabajador);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.toolstripform);
            this.Controls.Add(this.dgvdetalle);
            this.ForeColor = System.Drawing.SystemColors.ControlText;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(1203, 580);
            this.Name = "ui_rep_asistencia";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Reporte de Asistencias";
            this.Load += new System.EventHandler(this.ui_rep_asistencia_Load);
            this.toolstripform.ResumeLayout(false);
            this.toolstripform.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvdetalle)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.loadingNext1)).EndInit();
            this.grTrabajador.ResumeLayout(false);
            this.grTrabajador.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxBuscar)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolstripform;
        private System.Windows.Forms.DataGridView dgvdetalle;
        private System.Windows.Forms.GroupBox groupBox1;
        public System.Windows.Forms.ToolStripButton btnExcel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dtpFecfin;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dtpFecini;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cmbCia;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox cmbGerencia;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cmbCencos;
        private System.Windows.Forms.Label lblregistros;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox grTrabajador;
        private System.Windows.Forms.PictureBox pictureBoxBuscar;
        private System.Windows.Forms.Label lblF2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label8;
        public System.Windows.Forms.TextBox txtNombres;
        public System.Windows.Forms.TextBox txtNroDocIden;
        public System.Windows.Forms.TextBox txtDocIdent;
        private System.Windows.Forms.Label label9;
        public System.Windows.Forms.TextBox txtCodigoInterno;
        private System.Windows.Forms.ToolStripButton btnNuevo;
        private System.Windows.Forms.ToolStripButton btnEliminar;
        private System.Windows.Forms.ToolStripButton btnEditar;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.CheckBox chkIndividual;
        private System.Windows.Forms.ComboBox cmbNominas;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ComboBox cmbSedes;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox chkMasivo;
        public System.Windows.Forms.ToolStripButton btnGenerar;
        private System.Windows.Forms.PictureBox loadingNext1;
    }
}