namespace CaniaBrava
{
    partial class ui_regdescanso
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ui_regdescanso));
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.dtpFecfin = new System.Windows.Forms.DateTimePicker();
            this.dtpFecini = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cmbcontingencia = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnTodos = new System.Windows.Forms.Button();
            this.txtBuscar = new System.Windows.Forms.TextBox();
            this.radioButtonNombre = new System.Windows.Forms.RadioButton();
            this.radioButtonNroDoc = new System.Windows.Forms.RadioButton();
            this.toolstripform = new System.Windows.Forms.ToolStrip();
            this.btnNuevo = new System.Windows.Forms.ToolStripButton();
            this.btnEditar = new System.Windows.Forms.ToolStripButton();
            this.btnEliminar = new System.Windows.Forms.ToolStripButton();
            this.btnExcel = new System.Windows.Forms.ToolStripButton();
            this.btnActualizar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnSalir = new System.Windows.Forms.ToolStripButton();
            this.dgvdetalle = new System.Windows.Forms.DataGridView();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.cmbCategoria = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripLabel4 = new System.Windows.Forms.ToolStripLabel();
            this.cmbcia = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolSBtnCode = new System.Windows.Forms.ToolStripButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cmbConcepSap = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.lblTotalDiasDescanso = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.dgvdetalleExcel = new System.Windows.Forms.DataGridView();
            this.tabControlRegDescanso = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabConexion = new System.Windows.Forms.TabPage();
            this.txtQuery = new System.Windows.Forms.TextBox();
            this.tabManejoDeErrores = new System.Windows.Forms.TabPage();
            this.BtnReportarError = new System.Windows.Forms.Button();
            this.txtManejoErrores = new System.Windows.Forms.TextBox();
            this.groupBox2.SuspendLayout();
            this.toolstripform.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvdetalle)).BeginInit();
            this.toolStrip1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvdetalleExcel)).BeginInit();
            this.tabControlRegDescanso.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabConexion.SuspendLayout();
            this.tabManejoDeErrores.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.dtpFecfin);
            this.groupBox2.Controls.Add(this.dtpFecini);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Location = new System.Drawing.Point(16, 34);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox2.Size = new System.Drawing.Size(248, 96);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            // 
            // dtpFecfin
            // 
            this.dtpFecfin.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFecfin.Location = new System.Drawing.Point(108, 50);
            this.dtpFecfin.Margin = new System.Windows.Forms.Padding(4);
            this.dtpFecfin.Name = "dtpFecfin";
            this.dtpFecfin.Size = new System.Drawing.Size(128, 22);
            this.dtpFecfin.TabIndex = 25;
            this.dtpFecfin.ValueChanged += new System.EventHandler(this.dtpFecfin_ValueChanged);
            // 
            // dtpFecini
            // 
            this.dtpFecini.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFecini.Location = new System.Drawing.Point(108, 21);
            this.dtpFecini.Margin = new System.Windows.Forms.Padding(4);
            this.dtpFecini.Name = "dtpFecini";
            this.dtpFecini.Size = new System.Drawing.Size(128, 22);
            this.dtpFecini.TabIndex = 23;
            this.dtpFecini.ValueChanged += new System.EventHandler(this.dtpFecini_ValueChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(16, 54);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(89, 17);
            this.label3.TabIndex = 24;
            this.label3.Text = "Fecha Final :";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 25);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(94, 17);
            this.label2.TabIndex = 22;
            this.label2.Text = "Fecha Inicial :";
            // 
            // cmbcontingencia
            // 
            this.cmbcontingencia.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbcontingencia.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbcontingencia.FormattingEnabled = true;
            this.cmbcontingencia.Location = new System.Drawing.Point(135, 18);
            this.cmbcontingencia.Margin = new System.Windows.Forms.Padding(4);
            this.cmbcontingencia.Name = "cmbcontingencia";
            this.cmbcontingencia.Size = new System.Drawing.Size(237, 21);
            this.cmbcontingencia.TabIndex = 21;
            this.cmbcontingencia.SelectedIndexChanged += new System.EventHandler(this.cmbcontingencia_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(27, 21);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(98, 17);
            this.label1.TabIndex = 20;
            this.label1.Text = "Contingencia :";
            // 
            // btnTodos
            // 
            this.btnTodos.Location = new System.Drawing.Point(457, 49);
            this.btnTodos.Margin = new System.Windows.Forms.Padding(4);
            this.btnTodos.Name = "btnTodos";
            this.btnTodos.Size = new System.Drawing.Size(92, 28);
            this.btnTodos.TabIndex = 17;
            this.btnTodos.Text = "Quitar Filtro";
            this.btnTodos.UseVisualStyleBackColor = true;
            this.btnTodos.Click += new System.EventHandler(this.btnTodos_Click);
            // 
            // txtBuscar
            // 
            this.txtBuscar.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtBuscar.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBuscar.Location = new System.Drawing.Point(17, 52);
            this.txtBuscar.Margin = new System.Windows.Forms.Padding(4);
            this.txtBuscar.Name = "txtBuscar";
            this.txtBuscar.Size = new System.Drawing.Size(431, 20);
            this.txtBuscar.TabIndex = 1;
            this.txtBuscar.TextChanged += new System.EventHandler(this.txtBuscar_TextChanged);
            // 
            // radioButtonNombre
            // 
            this.radioButtonNombre.AutoSize = true;
            this.radioButtonNombre.Checked = true;
            this.radioButtonNombre.Location = new System.Drawing.Point(29, 25);
            this.radioButtonNombre.Margin = new System.Windows.Forms.Padding(4);
            this.radioButtonNombre.Name = "radioButtonNombre";
            this.radioButtonNombre.Size = new System.Drawing.Size(79, 21);
            this.radioButtonNombre.TabIndex = 15;
            this.radioButtonNombre.TabStop = true;
            this.radioButtonNombre.Text = "Nombre";
            this.radioButtonNombre.UseVisualStyleBackColor = true;
            this.radioButtonNombre.CheckedChanged += new System.EventHandler(this.radioButtonNombre_CheckedChanged);
            // 
            // radioButtonNroDoc
            // 
            this.radioButtonNroDoc.AutoSize = true;
            this.radioButtonNroDoc.Location = new System.Drawing.Point(113, 25);
            this.radioButtonNroDoc.Margin = new System.Windows.Forms.Padding(4);
            this.radioButtonNroDoc.Name = "radioButtonNroDoc";
            this.radioButtonNroDoc.Size = new System.Drawing.Size(52, 21);
            this.radioButtonNroDoc.TabIndex = 14;
            this.radioButtonNroDoc.Text = "DNI";
            this.radioButtonNroDoc.UseVisualStyleBackColor = true;
            this.radioButtonNroDoc.CheckedChanged += new System.EventHandler(this.radioButtonNroDoc_CheckedChanged);
            // 
            // toolstripform
            // 
            this.toolstripform.BackColor = System.Drawing.SystemColors.HighlightText;
            this.toolstripform.Dock = System.Windows.Forms.DockStyle.None;
            this.toolstripform.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolstripform.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnNuevo,
            this.btnEditar,
            this.btnEliminar,
            this.btnExcel,
            this.btnActualizar,
            this.toolStripSeparator1,
            this.btnSalir});
            this.toolstripform.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.toolstripform.Location = new System.Drawing.Point(3, 662);
            this.toolstripform.Name = "toolstripform";
            this.toolstripform.ShowItemToolTips = false;
            this.toolstripform.Size = new System.Drawing.Size(537, 27);
            this.toolstripform.TabIndex = 93;
            // 
            // btnNuevo
            // 
            this.btnNuevo.Image = global::CaniaBrava.Properties.Resources.NEW;
            this.btnNuevo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnNuevo.Name = "btnNuevo";
            this.btnNuevo.Size = new System.Drawing.Size(76, 24);
            this.btnNuevo.Text = "Nuevo";
            this.btnNuevo.Click += new System.EventHandler(this.btnNuevo_Click);
            // 
            // btnEditar
            // 
            this.btnEditar.Image = global::CaniaBrava.Properties.Resources.OPEN;
            this.btnEditar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnEditar.Name = "btnEditar";
            this.btnEditar.Size = new System.Drawing.Size(72, 24);
            this.btnEditar.Text = "Editar";
            this.btnEditar.Click += new System.EventHandler(this.btnEditar_Click);
            // 
            // btnEliminar
            // 
            this.btnEliminar.Image = global::CaniaBrava.Properties.Resources.DELETE;
            this.btnEliminar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnEliminar.Name = "btnEliminar";
            this.btnEliminar.Size = new System.Drawing.Size(87, 24);
            this.btnEliminar.Text = "Eliminar";
            this.btnEliminar.Click += new System.EventHandler(this.btnEliminar_Click);
            // 
            // btnExcel
            // 
            this.btnExcel.Image = global::CaniaBrava.Properties.Resources.EXCEL;
            this.btnExcel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnExcel.Name = "btnExcel";
            this.btnExcel.Size = new System.Drawing.Size(123, 24);
            this.btnExcel.Text = "Enviar a Excel";
            this.btnExcel.Click += new System.EventHandler(this.btnExcel_Click);
            // 
            // btnActualizar
            // 
            this.btnActualizar.Image = global::CaniaBrava.Properties.Resources.refresh;
            this.btnActualizar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnActualizar.Name = "btnActualizar";
            this.btnActualizar.Size = new System.Drawing.Size(99, 24);
            this.btnActualizar.Text = "Actualizar";
            this.btnActualizar.Click += new System.EventHandler(this.btnActualizar_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 27);
            // 
            // btnSalir
            // 
            this.btnSalir.Image = global::CaniaBrava.Properties.Resources.CLOSE;
            this.btnSalir.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSalir.Name = "btnSalir";
            this.btnSalir.Size = new System.Drawing.Size(62, 24);
            this.btnSalir.Text = "Salir";
            this.btnSalir.Click += new System.EventHandler(this.btnSalir_Click);
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
            this.dgvdetalle.Location = new System.Drawing.Point(4, 4);
            this.dgvdetalle.Margin = new System.Windows.Forms.Padding(4);
            this.dgvdetalle.Name = "dgvdetalle";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvdetalle.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dgvdetalle.Size = new System.Drawing.Size(1202, 484);
            this.dgvdetalle.TabIndex = 94;
            // 
            // toolStrip1
            // 
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel1,
            this.cmbCategoria,
            this.toolStripLabel4,
            this.cmbcia,
            this.toolStripSeparator3,
            this.toolStripSeparator2,
            this.toolSBtnCode});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1228, 27);
            this.toolStrip1.TabIndex = 96;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(126, 24);
            this.toolStripLabel1.Text = "Tipo de Personal :";
            // 
            // cmbCategoria
            // 
            this.cmbCategoria.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCategoria.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.cmbCategoria.Name = "cmbCategoria";
            this.cmbCategoria.Size = new System.Drawing.Size(225, 27);
            this.cmbCategoria.SelectedIndexChanged += new System.EventHandler(this.cmbCategoria_SelectedIndexChanged);
            // 
            // toolStripLabel4
            // 
            this.toolStripLabel4.Name = "toolStripLabel4";
            this.toolStripLabel4.Size = new System.Drawing.Size(73, 24);
            this.toolStripLabel4.Text = "Empresa :";
            // 
            // cmbcia
            // 
            this.cmbcia.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbcia.DropDownWidth = 200;
            this.cmbcia.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbcia.Name = "cmbcia";
            this.cmbcia.Size = new System.Drawing.Size(252, 27);
            this.cmbcia.SelectedIndexChanged += new System.EventHandler(this.cmbcia_SelectedIndexChanged);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 27);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 27);
            // 
            // toolSBtnCode
            // 
            this.toolSBtnCode.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolSBtnCode.Image = ((System.Drawing.Image)(resources.GetObject("toolSBtnCode.Image")));
            this.toolSBtnCode.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolSBtnCode.Name = "toolSBtnCode";
            this.toolSBtnCode.Size = new System.Drawing.Size(24, 24);
            this.toolSBtnCode.Text = "Programación";
            this.toolSBtnCode.Click += new System.EventHandler(this.toolSBtnCode_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cmbConcepSap);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.cmbcontingencia);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(272, 34);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox1.Size = new System.Drawing.Size(381, 96);
            this.groupBox1.TabIndex = 97;
            this.groupBox1.TabStop = false;
            // 
            // cmbConcepSap
            // 
            this.cmbConcepSap.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbConcepSap.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbConcepSap.FormattingEnabled = true;
            this.cmbConcepSap.Location = new System.Drawing.Point(135, 50);
            this.cmbConcepSap.Margin = new System.Windows.Forms.Padding(4);
            this.cmbConcepSap.Name = "cmbConcepSap";
            this.cmbConcepSap.Size = new System.Drawing.Size(237, 21);
            this.cmbConcepSap.TabIndex = 23;
            this.cmbConcepSap.SelectedIndexChanged += new System.EventHandler(this.cmbConcepSap_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(9, 54);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(114, 17);
            this.label4.TabIndex = 22;
            this.label4.Text = "Conceptos SAP :";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.lblTotalDiasDescanso);
            this.groupBox3.Controls.Add(this.label5);
            this.groupBox3.Controls.Add(this.btnTodos);
            this.groupBox3.Controls.Add(this.txtBuscar);
            this.groupBox3.Controls.Add(this.radioButtonNroDoc);
            this.groupBox3.Controls.Add(this.radioButtonNombre);
            this.groupBox3.Location = new System.Drawing.Point(661, 34);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox3.Size = new System.Drawing.Size(560, 96);
            this.groupBox3.TabIndex = 98;
            this.groupBox3.TabStop = false;
            // 
            // lblTotalDiasDescanso
            // 
            this.lblTotalDiasDescanso.AutoSize = true;
            this.lblTotalDiasDescanso.Location = new System.Drawing.Point(499, 18);
            this.lblTotalDiasDescanso.Name = "lblTotalDiasDescanso";
            this.lblTotalDiasDescanso.Size = new System.Drawing.Size(46, 17);
            this.lblTotalDiasDescanso.TabIndex = 19;
            this.lblTotalDiasDescanso.Text = "label6";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(314, 18);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(179, 17);
            this.label5.TabIndex = 18;
            this.label5.Text = "Total de dias de descanso:";
            // 
            // dgvdetalleExcel
            // 
            this.dgvdetalleExcel.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvdetalleExcel.Location = new System.Drawing.Point(827, 270);
            this.dgvdetalleExcel.Margin = new System.Windows.Forms.Padding(4);
            this.dgvdetalleExcel.Name = "dgvdetalleExcel";
            this.dgvdetalleExcel.Size = new System.Drawing.Size(320, 185);
            this.dgvdetalleExcel.TabIndex = 99;
            this.dgvdetalleExcel.Visible = false;
            // 
            // tabControlRegDescanso
            // 
            this.tabControlRegDescanso.Controls.Add(this.tabPage1);
            this.tabControlRegDescanso.Controls.Add(this.tabConexion);
            this.tabControlRegDescanso.Controls.Add(this.tabManejoDeErrores);
            this.tabControlRegDescanso.Location = new System.Drawing.Point(3, 138);
            this.tabControlRegDescanso.Name = "tabControlRegDescanso";
            this.tabControlRegDescanso.SelectedIndex = 0;
            this.tabControlRegDescanso.Size = new System.Drawing.Size(1218, 521);
            this.tabControlRegDescanso.TabIndex = 100;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.dgvdetalleExcel);
            this.tabPage1.Controls.Add(this.dgvdetalle);
            this.tabPage1.Location = new System.Drawing.Point(4, 25);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1210, 492);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Tabla";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabConexion
            // 
            this.tabConexion.Controls.Add(this.txtQuery);
            this.tabConexion.Location = new System.Drawing.Point(4, 25);
            this.tabConexion.Name = "tabConexion";
            this.tabConexion.Padding = new System.Windows.Forms.Padding(3);
            this.tabConexion.Size = new System.Drawing.Size(1210, 492);
            this.tabConexion.TabIndex = 1;
            this.tabConexion.Text = "Query";
            this.tabConexion.UseVisualStyleBackColor = true;
            // 
            // txtQuery
            // 
            this.txtQuery.Location = new System.Drawing.Point(6, 6);
            this.txtQuery.Multiline = true;
            this.txtQuery.Name = "txtQuery";
            this.txtQuery.ReadOnly = true;
            this.txtQuery.Size = new System.Drawing.Size(1198, 531);
            this.txtQuery.TabIndex = 0;
            // 
            // tabManejoDeErrores
            // 
            this.tabManejoDeErrores.Controls.Add(this.BtnReportarError);
            this.tabManejoDeErrores.Controls.Add(this.txtManejoErrores);
            this.tabManejoDeErrores.Location = new System.Drawing.Point(4, 25);
            this.tabManejoDeErrores.Name = "tabManejoDeErrores";
            this.tabManejoDeErrores.Padding = new System.Windows.Forms.Padding(3);
            this.tabManejoDeErrores.Size = new System.Drawing.Size(1210, 492);
            this.tabManejoDeErrores.TabIndex = 2;
            this.tabManejoDeErrores.Text = "Manejo de errores";
            this.tabManejoDeErrores.UseVisualStyleBackColor = true;
            // 
            // BtnReportarError
            // 
            this.BtnReportarError.Location = new System.Drawing.Point(1085, 16);
            this.BtnReportarError.Name = "BtnReportarError";
            this.BtnReportarError.Size = new System.Drawing.Size(114, 39);
            this.BtnReportarError.TabIndex = 1;
            this.BtnReportarError.Text = "Reportar Error";
            this.BtnReportarError.UseVisualStyleBackColor = true;
            this.BtnReportarError.Click += new System.EventHandler(this.BtnReportarError_Click);
            // 
            // txtManejoErrores
            // 
            this.txtManejoErrores.Dock = System.Windows.Forms.DockStyle.Top;
            this.txtManejoErrores.Location = new System.Drawing.Point(3, 3);
            this.txtManejoErrores.Multiline = true;
            this.txtManejoErrores.Name = "txtManejoErrores";
            this.txtManejoErrores.ReadOnly = true;
            this.txtManejoErrores.Size = new System.Drawing.Size(1204, 534);
            this.txtManejoErrores.TabIndex = 0;
            // 
            // ui_regdescanso
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.HighlightText;
            this.ClientSize = new System.Drawing.Size(1228, 699);
            this.Controls.Add(this.tabControlRegDescanso);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.toolstripform);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(1243, 746);
            this.Name = "ui_regdescanso";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Registro - Descansos Medicos";
            this.Load += new System.EventHandler(this.ui_regvac_Load);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.toolstripform.ResumeLayout(false);
            this.toolstripform.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvdetalle)).EndInit();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvdetalleExcel)).EndInit();
            this.tabControlRegDescanso.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabConexion.ResumeLayout(false);
            this.tabConexion.PerformLayout();
            this.tabManejoDeErrores.ResumeLayout(false);
            this.tabManejoDeErrores.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnTodos;
        private System.Windows.Forms.TextBox txtBuscar;
        private System.Windows.Forms.RadioButton radioButtonNombre;
        private System.Windows.Forms.RadioButton radioButtonNroDoc;
        private System.Windows.Forms.ToolStrip toolstripform;
        private System.Windows.Forms.ToolStripButton btnNuevo;
        private System.Windows.Forms.ToolStripButton btnEditar;
        private System.Windows.Forms.ToolStripButton btnEliminar;
        public System.Windows.Forms.ToolStripButton btnExcel;
        public System.Windows.Forms.ToolStripButton btnActualizar;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton btnSalir;
        private System.Windows.Forms.DataGridView dgvdetalle;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripComboBox cmbCategoria;
        private System.Windows.Forms.ToolStripLabel toolStripLabel4;
        private System.Windows.Forms.ToolStripComboBox cmbcia;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbcontingencia;
        private System.Windows.Forms.DateTimePicker dtpFecfin;
        private System.Windows.Forms.DateTimePicker dtpFecini;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox cmbConcepSap;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.DataGridView dgvdetalleExcel;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lblTotalDiasDescanso;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton toolSBtnCode;
        private System.Windows.Forms.TabControl tabControlRegDescanso;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabConexion;
        private System.Windows.Forms.TextBox txtQuery;
        private System.Windows.Forms.TabPage tabManejoDeErrores;
        private System.Windows.Forms.TextBox txtManejoErrores;
        private System.Windows.Forms.Button BtnReportarError;
    }
}