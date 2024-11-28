namespace CaniaBrava
{
    partial class ui_asisdia
    {
        /// <summary>
        /// Variable del diseñador requerida.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén utilizando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben eliminar; false en caso contrario, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido del método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            this.lblPeriodo = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.txtFechaFin = new System.Windows.Forms.MaskedTextBox();
            this.lblUsuario = new System.Windows.Forms.Label();
            this.txtMesSem = new System.Windows.Forms.MaskedTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtFechaIni = new System.Windows.Forms.MaskedTextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.cmbTipoCal = new System.Windows.Forms.ComboBox();
            this.cmbTipoPlan = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cmbTipoTrabajador = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox46 = new System.Windows.Forms.GroupBox();
            this.label61 = new System.Windows.Forms.Label();
            this.cmbEmpleador = new System.Windows.Forms.ComboBox();
            this.label60 = new System.Windows.Forms.Label();
            this.radioButtonSiEmp = new System.Windows.Forms.RadioButton();
            this.radioButtonNoEmp = new System.Windows.Forms.RadioButton();
            this.dgvdetalle = new System.Windows.Forms.DataGridView();
            this.toolstripform = new System.Windows.Forms.ToolStrip();
            this.btnGrabar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnSalir = new System.Windows.Forms.ToolStripButton();
            this.cmbSeccion = new System.Windows.Forms.ComboBox();
            this.cmbHoras = new System.Windows.Forms.ComboBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.chkCese = new System.Windows.Forms.CheckBox();
            this.txtBuscar = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.btnBuscar = new System.Windows.Forms.Button();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.btnExcel = new System.Windows.Forms.ToolStripButton();
            this.groupBox3.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox46.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvdetalle)).BeginInit();
            this.toolstripform.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblPeriodo
            // 
            this.lblPeriodo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblPeriodo.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPeriodo.ForeColor = System.Drawing.Color.White;
            this.lblPeriodo.Location = new System.Drawing.Point(807, 118);
            this.lblPeriodo.Name = "lblPeriodo";
            this.lblPeriodo.Size = new System.Drawing.Size(204, 24);
            this.lblPeriodo.TabIndex = 96;
            this.lblPeriodo.Text = "Periodo Laboral Cerrado";
            this.lblPeriodo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblPeriodo.Visible = false;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.txtFechaFin);
            this.groupBox3.Controls.Add(this.lblUsuario);
            this.groupBox3.Controls.Add(this.txtMesSem);
            this.groupBox3.Controls.Add(this.label1);
            this.groupBox3.Controls.Add(this.txtFechaIni);
            this.groupBox3.Controls.Add(this.label2);
            this.groupBox3.Location = new System.Drawing.Point(270, 1);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(496, 49);
            this.groupBox3.TabIndex = 95;
            this.groupBox3.TabStop = false;
            // 
            // txtFechaFin
            // 
            this.txtFechaFin.BackColor = System.Drawing.SystemColors.HighlightText;
            this.txtFechaFin.Enabled = false;
            this.txtFechaFin.Location = new System.Drawing.Point(336, 14);
            this.txtFechaFin.Mask = "00/00/0000";
            this.txtFechaFin.Name = "txtFechaFin";
            this.txtFechaFin.Size = new System.Drawing.Size(78, 20);
            this.txtFechaFin.TabIndex = 3;
            this.txtFechaFin.ValidatingType = typeof(System.DateTime);
            // 
            // lblUsuario
            // 
            this.lblUsuario.AutoSize = true;
            this.lblUsuario.Location = new System.Drawing.Point(6, 18);
            this.lblUsuario.Name = "lblUsuario";
            this.lblUsuario.Size = new System.Drawing.Size(52, 13);
            this.lblUsuario.TabIndex = 85;
            this.lblUsuario.Text = "Periodo  :";
            // 
            // txtMesSem
            // 
            this.txtMesSem.BackColor = System.Drawing.SystemColors.HighlightText;
            this.txtMesSem.Location = new System.Drawing.Point(59, 14);
            this.txtMesSem.Mask = "99/9999";
            this.txtMesSem.Name = "txtMesSem";
            this.txtMesSem.Size = new System.Drawing.Size(65, 20);
            this.txtMesSem.TabIndex = 1;
            this.txtMesSem.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtMesSem_KeyPress);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(125, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(71, 13);
            this.label1.TabIndex = 86;
            this.label1.Text = "Fecha Inicio :";
            // 
            // txtFechaIni
            // 
            this.txtFechaIni.BackColor = System.Drawing.SystemColors.HighlightText;
            this.txtFechaIni.Enabled = false;
            this.txtFechaIni.Location = new System.Drawing.Point(198, 14);
            this.txtFechaIni.Mask = "00/00/0000";
            this.txtFechaIni.Name = "txtFechaIni";
            this.txtFechaIni.Size = new System.Drawing.Size(74, 20);
            this.txtFechaIni.TabIndex = 2;
            this.txtFechaIni.ValidatingType = typeof(System.DateTime);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(274, 18);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(60, 13);
            this.label2.TabIndex = 87;
            this.label2.Text = "Fecha Fin :";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.cmbTipoCal);
            this.groupBox1.Controls.Add(this.cmbTipoPlan);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.cmbTipoTrabajador);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Location = new System.Drawing.Point(3, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(258, 107);
            this.groupBox1.TabIndex = 91;
            this.groupBox1.TabStop = false;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(4, 73);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(87, 13);
            this.label5.TabIndex = 92;
            this.label5.Text = "Tipo Calendario :";
            // 
            // cmbTipoCal
            // 
            this.cmbTipoCal.BackColor = System.Drawing.SystemColors.HighlightText;
            this.cmbTipoCal.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTipoCal.FormattingEnabled = true;
            this.cmbTipoCal.Location = new System.Drawing.Point(100, 70);
            this.cmbTipoCal.Name = "cmbTipoCal";
            this.cmbTipoCal.Size = new System.Drawing.Size(148, 21);
            this.cmbTipoCal.TabIndex = 2;
            this.cmbTipoCal.SelectedIndexChanged += new System.EventHandler(this.cmbTipoCal_SelectedIndexChanged);
            // 
            // cmbTipoPlan
            // 
            this.cmbTipoPlan.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTipoPlan.FormattingEnabled = true;
            this.cmbTipoPlan.Location = new System.Drawing.Point(100, 42);
            this.cmbTipoPlan.Name = "cmbTipoPlan";
            this.cmbTipoPlan.Size = new System.Drawing.Size(148, 21);
            this.cmbTipoPlan.TabIndex = 1;
            this.cmbTipoPlan.SelectedIndexChanged += new System.EventHandler(this.cmbTipoPlan_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(4, 46);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(88, 13);
            this.label4.TabIndex = 90;
            this.label4.Text = "Tipo de Planilla  :";
            // 
            // cmbTipoTrabajador
            // 
            this.cmbTipoTrabajador.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTipoTrabajador.FormattingEnabled = true;
            this.cmbTipoTrabajador.Location = new System.Drawing.Point(100, 16);
            this.cmbTipoTrabajador.Name = "cmbTipoTrabajador";
            this.cmbTipoTrabajador.Size = new System.Drawing.Size(148, 21);
            this.cmbTipoTrabajador.TabIndex = 0;
            this.cmbTipoTrabajador.SelectedIndexChanged += new System.EventHandler(this.cmbTipoTrabajador_SelectedIndexChanged);
            this.cmbTipoTrabajador.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cmbTipoTrabajador_KeyPress);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(4, 20);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(96, 13);
            this.label3.TabIndex = 88;
            this.label3.Text = "Tipo de Personal  :";
            // 
            // groupBox46
            // 
            this.groupBox46.Controls.Add(this.label61);
            this.groupBox46.Controls.Add(this.cmbEmpleador);
            this.groupBox46.Controls.Add(this.label60);
            this.groupBox46.Controls.Add(this.radioButtonSiEmp);
            this.groupBox46.Controls.Add(this.radioButtonNoEmp);
            this.groupBox46.Location = new System.Drawing.Point(270, 46);
            this.groupBox46.Name = "groupBox46";
            this.groupBox46.Size = new System.Drawing.Size(496, 61);
            this.groupBox46.TabIndex = 92;
            this.groupBox46.TabStop = false;
            // 
            // label61
            // 
            this.label61.AutoSize = true;
            this.label61.Location = new System.Drawing.Point(10, 33);
            this.label61.Name = "label61";
            this.label61.Size = new System.Drawing.Size(60, 13);
            this.label61.TabIndex = 72;
            this.label61.Text = "Empleador ";
            // 
            // cmbEmpleador
            // 
            this.cmbEmpleador.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbEmpleador.Enabled = false;
            this.cmbEmpleador.FormattingEnabled = true;
            this.cmbEmpleador.Location = new System.Drawing.Point(76, 29);
            this.cmbEmpleador.Name = "cmbEmpleador";
            this.cmbEmpleador.Size = new System.Drawing.Size(382, 21);
            this.cmbEmpleador.TabIndex = 2;
            this.cmbEmpleador.SelectedIndexChanged += new System.EventHandler(this.cmbEmpleador_SelectedIndexChanged);
            // 
            // label60
            // 
            this.label60.AutoSize = true;
            this.label60.Location = new System.Drawing.Point(10, 13);
            this.label60.Name = "label60";
            this.label60.Size = new System.Drawing.Size(385, 13);
            this.label60.TabIndex = 65;
            this.label60.Text = "¿ Ingreso de datos de trabajadores asignados o desplazados a otro Empleador ?";
            // 
            // radioButtonSiEmp
            // 
            this.radioButtonSiEmp.AutoSize = true;
            this.radioButtonSiEmp.BackColor = System.Drawing.SystemColors.HighlightText;
            this.radioButtonSiEmp.Location = new System.Drawing.Point(408, 11);
            this.radioButtonSiEmp.Name = "radioButtonSiEmp";
            this.radioButtonSiEmp.Size = new System.Drawing.Size(34, 17);
            this.radioButtonSiEmp.TabIndex = 0;
            this.radioButtonSiEmp.Text = "Si";
            this.radioButtonSiEmp.UseVisualStyleBackColor = false;
            this.radioButtonSiEmp.CheckedChanged += new System.EventHandler(this.radioButtonSiEmp_CheckedChanged);
            // 
            // radioButtonNoEmp
            // 
            this.radioButtonNoEmp.AutoSize = true;
            this.radioButtonNoEmp.BackColor = System.Drawing.SystemColors.HighlightText;
            this.radioButtonNoEmp.Checked = true;
            this.radioButtonNoEmp.Location = new System.Drawing.Point(451, 11);
            this.radioButtonNoEmp.Name = "radioButtonNoEmp";
            this.radioButtonNoEmp.Size = new System.Drawing.Size(39, 17);
            this.radioButtonNoEmp.TabIndex = 1;
            this.radioButtonNoEmp.TabStop = true;
            this.radioButtonNoEmp.Text = "No";
            this.radioButtonNoEmp.UseVisualStyleBackColor = false;
            this.radioButtonNoEmp.CheckedChanged += new System.EventHandler(this.radioButtonNoEmp_CheckedChanged);
            // 
            // dgvdetalle
            // 
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvdetalle.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.dgvdetalle.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvdetalle.DefaultCellStyle = dataGridViewCellStyle5;
            this.dgvdetalle.Location = new System.Drawing.Point(3, 160);
            this.dgvdetalle.Name = "dgvdetalle";
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvdetalle.RowHeadersDefaultCellStyle = dataGridViewCellStyle6;
            this.dgvdetalle.Size = new System.Drawing.Size(1160, 369);
            this.dgvdetalle.TabIndex = 93;
            this.dgvdetalle.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvdetalle_CellContentClick);
            this.dgvdetalle.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvdetalle_CellValueChanged);
            this.dgvdetalle.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.dgvdetalle_KeyPress);
            // 
            // toolstripform
            // 
            this.toolstripform.BackColor = System.Drawing.SystemColors.HighlightText;
            this.toolstripform.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.toolstripform.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnGrabar,
            this.toolStripSeparator1,
            this.btnExcel,
            this.btnSalir});
            this.toolstripform.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.toolstripform.Location = new System.Drawing.Point(0, 532);
            this.toolstripform.Name = "toolstripform";
            this.toolstripform.ShowItemToolTips = false;
            this.toolstripform.Size = new System.Drawing.Size(1163, 29);
            this.toolstripform.TabIndex = 97;
            // 
            // btnGrabar
            // 
            this.btnGrabar.Image = global::CaniaBrava.Properties.Resources.SAVE;
            this.btnGrabar.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnGrabar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnGrabar.Name = "btnGrabar";
            this.btnGrabar.Size = new System.Drawing.Size(68, 26);
            this.btnGrabar.Text = "Grabar";
            this.btnGrabar.Click += new System.EventHandler(this.btnGrabar_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 29);
            // 
            // btnSalir
            // 
            this.btnSalir.Image = global::CaniaBrava.Properties.Resources.CLOSE;
            this.btnSalir.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSalir.Name = "btnSalir";
            this.btnSalir.Size = new System.Drawing.Size(49, 26);
            this.btnSalir.Text = "Salir";
            this.btnSalir.Click += new System.EventHandler(this.btnSalir_Click);
            // 
            // cmbSeccion
            // 
            this.cmbSeccion.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSeccion.FormattingEnabled = true;
            this.cmbSeccion.Location = new System.Drawing.Point(25, 15);
            this.cmbSeccion.Name = "cmbSeccion";
            this.cmbSeccion.Size = new System.Drawing.Size(214, 21);
            this.cmbSeccion.TabIndex = 99;
            this.cmbSeccion.SelectedIndexChanged += new System.EventHandler(this.cmbSeccion_SelectedIndexChanged);
            // 
            // cmbHoras
            // 
            this.cmbHoras.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbHoras.FormattingEnabled = true;
            this.cmbHoras.Items.AddRange(new object[] {
            "H.Ext. 100%",
            "H.Ext. 35%",
            "H.Ext. 25%"});
            this.cmbHoras.Location = new System.Drawing.Point(25, 16);
            this.cmbHoras.Name = "cmbHoras";
            this.cmbHoras.Size = new System.Drawing.Size(214, 21);
            this.cmbHoras.TabIndex = 101;
            this.cmbHoras.SelectedIndexChanged += new System.EventHandler(this.cmbHoras_SelectedIndexChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.cmbHoras);
            this.groupBox2.Location = new System.Drawing.Point(772, 70);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(258, 45);
            this.groupBox2.TabIndex = 102;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Considerar día Domingo laborado como";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.cmbSeccion);
            this.groupBox4.Controls.Add(this.chkCese);
            this.groupBox4.Location = new System.Drawing.Point(772, 1);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(258, 62);
            this.groupBox4.TabIndex = 103;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Sección";
            // 
            // chkCese
            // 
            this.chkCese.AutoSize = true;
            this.chkCese.Checked = true;
            this.chkCese.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkCese.Location = new System.Drawing.Point(25, 39);
            this.chkCese.Name = "chkCese";
            this.chkCese.Size = new System.Drawing.Size(142, 17);
            this.chkCese.TabIndex = 106;
            this.chkCese.Text = "Ocultar personal Cesado";
            this.chkCese.UseVisualStyleBackColor = true;
            this.chkCese.CheckedChanged += new System.EventHandler(this.chkCese_CheckedChanged);
            // 
            // txtBuscar
            // 
            this.txtBuscar.Location = new System.Drawing.Point(172, 15);
            this.txtBuscar.Name = "txtBuscar";
            this.txtBuscar.Size = new System.Drawing.Size(399, 20);
            this.txtBuscar.TabIndex = 104;
            this.txtBuscar.TextChanged += new System.EventHandler(this.txtBuscar_TextChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(4, 19);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(162, 13);
            this.label6.TabIndex = 93;
            this.label6.Text = "Buscar por Apellidos y Nombres :";
            // 
            // btnBuscar
            // 
            this.btnBuscar.Location = new System.Drawing.Point(577, 14);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(75, 23);
            this.btnBuscar.TabIndex = 105;
            this.btnBuscar.Text = "Buscar";
            this.btnBuscar.UseVisualStyleBackColor = true;
            this.btnBuscar.Click += new System.EventHandler(this.btnBuscar_Click);
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.txtBuscar);
            this.groupBox5.Controls.Add(this.label6);
            this.groupBox5.Controls.Add(this.btnBuscar);
            this.groupBox5.Location = new System.Drawing.Point(3, 110);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(763, 44);
            this.groupBox5.TabIndex = 107;
            this.groupBox5.TabStop = false;
            this.groupBox5.Enter += new System.EventHandler(this.groupBox5_Enter);
            // 
            // btnExcel
            // 
            this.btnExcel.Image = global::CaniaBrava.Properties.Resources.EXCEL;
            this.btnExcel.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnExcel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnExcel.Name = "btnExcel";
            this.btnExcel.Size = new System.Drawing.Size(52, 26);
            this.btnExcel.Text = "Excel";
            this.btnExcel.Click += new System.EventHandler(this.btnExcel_Click);
            // 
            // ui_asisdia
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(1163, 561);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.toolstripform);
            this.Controls.Add(this.lblPeriodo);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox46);
            this.Controls.Add(this.dgvdetalle);
            this.Name = "ui_asisdia";
            this.Text = "Control de Asistencia Diaria del Personal";
            this.Load += new System.EventHandler(this.ui_asisdia_Load);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox46.ResumeLayout(false);
            this.groupBox46.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvdetalle)).EndInit();
            this.toolstripform.ResumeLayout(false);
            this.toolstripform.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblPeriodo;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.MaskedTextBox txtFechaFin;
        private System.Windows.Forms.Label lblUsuario;
        private System.Windows.Forms.MaskedTextBox txtMesSem;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.MaskedTextBox txtFechaIni;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cmbTipoCal;
        private System.Windows.Forms.ComboBox cmbTipoPlan;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cmbTipoTrabajador;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBox46;
        private System.Windows.Forms.Label label61;
        private System.Windows.Forms.ComboBox cmbEmpleador;
        private System.Windows.Forms.Label label60;
        private System.Windows.Forms.RadioButton radioButtonSiEmp;
        private System.Windows.Forms.RadioButton radioButtonNoEmp;
        private System.Windows.Forms.DataGridView dgvdetalle;
        private System.Windows.Forms.ToolStrip toolstripform;
        private System.Windows.Forms.ToolStripButton btnGrabar;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton btnSalir;
        private System.Windows.Forms.ComboBox cmbSeccion;
        private System.Windows.Forms.ComboBox cmbHoras;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.TextBox txtBuscar;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btnBuscar;
        private System.Windows.Forms.CheckBox chkCese;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.ToolStripButton btnExcel;
    }
}
