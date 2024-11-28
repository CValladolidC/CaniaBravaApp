namespace CaniaBrava
{
    partial class ui_PlanElecSunat
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ui_PlanElecSunat));
            this.txtAnio = new System.Windows.Forms.MaskedTextBox();
            this.lblUsuario = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cmbTipoPlan = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cmbTipoTrabajador = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btnNuevo = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.btnProceso = new System.Windows.Forms.ToolStripButton();
            this.btnSalir = new System.Windows.Forms.ToolStripButton();
            this.cmbMes = new System.Windows.Forms.ComboBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.chkEstPropios = new System.Windows.Forms.CheckBox();
            this.chkIngTribDesc = new System.Windows.Forms.CheckBox();
            this.chkEmpDestacoPer = new System.Windows.Forms.CheckBox();
            this.chkTrabPen = new System.Windows.Forms.CheckBox();
            this.chkDerHab = new System.Windows.Forms.CheckBox();
            this.chkJorTrab = new System.Windows.Forms.CheckBox();
            this.chkDiasSubsi = new System.Windows.Forms.CheckBox();
            this.chkDiasNoSubsi = new System.Windows.Forms.CheckBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.chkPeriodos = new System.Windows.Forms.CheckBox();
            this.chkDatosTrabajador = new System.Windows.Forms.CheckBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.chkEstaTrab = new System.Windows.Forms.CheckBox();
            this.groupBox1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtAnio
            // 
            this.txtAnio.BackColor = System.Drawing.SystemColors.HighlightText;
            this.txtAnio.Location = new System.Drawing.Point(92, 48);
            this.txtAnio.Mask = "9999";
            this.txtAnio.Name = "txtAnio";
            this.txtAnio.Size = new System.Drawing.Size(77, 20);
            this.txtAnio.TabIndex = 0;
            // 
            // lblUsuario
            // 
            this.lblUsuario.AutoSize = true;
            this.lblUsuario.Location = new System.Drawing.Point(6, 51);
            this.lblUsuario.Name = "lblUsuario";
            this.lblUsuario.Size = new System.Drawing.Size(32, 13);
            this.lblUsuario.TabIndex = 96;
            this.lblUsuario.Text = "Año :";
            this.lblUsuario.Click += new System.EventHandler(this.lblUsuario_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cmbTipoPlan);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.cmbTipoTrabajador);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Location = new System.Drawing.Point(249, -2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(299, 74);
            this.groupBox1.TabIndex = 69;
            this.groupBox1.TabStop = false;
            this.groupBox1.Enter += new System.EventHandler(this.groupBox1_Enter);
            // 
            // cmbTipoPlan
            // 
            this.cmbTipoPlan.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTipoPlan.FormattingEnabled = true;
            this.cmbTipoPlan.Location = new System.Drawing.Point(108, 42);
            this.cmbTipoPlan.Name = "cmbTipoPlan";
            this.cmbTipoPlan.Size = new System.Drawing.Size(164, 21);
            this.cmbTipoPlan.TabIndex = 1;
            this.cmbTipoPlan.SelectedIndexChanged += new System.EventHandler(this.cmbTipoPlan_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 46);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(88, 13);
            this.label4.TabIndex = 90;
            this.label4.Text = "Tipo de Planilla  :";
            // 
            // cmbTipoTrabajador
            // 
            this.cmbTipoTrabajador.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTipoTrabajador.FormattingEnabled = true;
            this.cmbTipoTrabajador.Location = new System.Drawing.Point(108, 14);
            this.cmbTipoTrabajador.Name = "cmbTipoTrabajador";
            this.cmbTipoTrabajador.Size = new System.Drawing.Size(164, 21);
            this.cmbTipoTrabajador.TabIndex = 0;
            this.cmbTipoTrabajador.SelectedIndexChanged += new System.EventHandler(this.cmbTipoTrabajador_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 18);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(96, 13);
            this.label3.TabIndex = 88;
            this.label3.Text = "Tipo de Personal  :";
            // 
            // toolStrip1
            // 
            this.toolStrip1.BackColor = System.Drawing.SystemColors.HighlightText;
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnNuevo,
            this.toolStripSeparator1,
            this.toolStripButton1});
            this.toolStrip1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.toolStrip1.Location = new System.Drawing.Point(0, 479);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.ShowItemToolTips = false;
            this.toolStrip1.Size = new System.Drawing.Size(558, 29);
            this.toolStrip1.TabIndex = 71;
            // 
            // btnNuevo
            // 
            this.btnNuevo.Image = global::CaniaBrava.Properties.Resources.FILE;
            this.btnNuevo.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnNuevo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnNuevo.Name = "btnNuevo";
            this.btnNuevo.Size = new System.Drawing.Size(82, 26);
            this.btnNuevo.Text = "Exportar";
            this.btnNuevo.Click += new System.EventHandler(this.btnNuevo_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 29);
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.Image = global::CaniaBrava.Properties.Resources.CLOSE;
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(49, 26);
            this.toolStripButton1.Text = "Salir";
            this.toolStripButton1.Click += new System.EventHandler(this.toolStripButton1_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(21, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(202, 57);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // btnProceso
            // 
            this.btnProceso.Image = global::CaniaBrava.Properties.Resources.SETUP;
            this.btnProceso.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnProceso.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnProceso.Name = "btnProceso";
            this.btnProceso.Size = new System.Drawing.Size(77, 25);
            this.btnProceso.Text = "Procesar";
            // 
            // btnSalir
            // 
            this.btnSalir.Image = global::CaniaBrava.Properties.Resources.CLOSE;
            this.btnSalir.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnSalir.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSalir.Name = "btnSalir";
            this.btnSalir.Size = new System.Drawing.Size(51, 25);
            this.btnSalir.Text = "Salir";
            this.btnSalir.Click += new System.EventHandler(this.btnSalir_Click);
            // 
            // cmbMes
            // 
            this.cmbMes.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbMes.FormattingEnabled = true;
            this.cmbMes.Items.AddRange(new object[] {
            "01   ENERO",
            "02   FEBRERO",
            "03   MARZO",
            "04   ABRIL",
            "05   MAYO",
            "06   JUNIO",
            "07   JULIO",
            "08   AGOSTO",
            "09   SETIEMBRE",
            "10   OCTUBRE",
            "11   NOVIEMBRE",
            "12   DICIEMBRE",
            ""});
            this.cmbMes.Location = new System.Drawing.Point(92, 21);
            this.cmbMes.Name = "cmbMes";
            this.cmbMes.Size = new System.Drawing.Size(181, 21);
            this.cmbMes.TabIndex = 91;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label5);
            this.groupBox3.Controls.Add(this.cmbMes);
            this.groupBox3.Controls.Add(this.lblUsuario);
            this.groupBox3.Controls.Add(this.txtAnio);
            this.groupBox3.Location = new System.Drawing.Point(10, 78);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(538, 78);
            this.groupBox3.TabIndex = 93;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Periodo Tributario";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 24);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(33, 13);
            this.label5.TabIndex = 103;
            this.label5.Text = "Mes :";
            // 
            // chkEstPropios
            // 
            this.chkEstPropios.AutoSize = true;
            this.chkEstPropios.Location = new System.Drawing.Point(22, 14);
            this.chkEstPropios.Name = "chkEstPropios";
            this.chkEstPropios.Size = new System.Drawing.Size(189, 17);
            this.chkEstPropios.TabIndex = 94;
            this.chkEstPropios.Text = "Datos de Establecimientos Propios";
            this.chkEstPropios.UseVisualStyleBackColor = true;
            this.chkEstPropios.CheckedChanged += new System.EventHandler(this.chkEstPropios_CheckedChanged);
            // 
            // chkIngTribDesc
            // 
            this.chkIngTribDesc.AutoSize = true;
            this.chkIngTribDesc.Location = new System.Drawing.Point(22, 39);
            this.chkIngTribDesc.Name = "chkIngTribDesc";
            this.chkIngTribDesc.Size = new System.Drawing.Size(368, 17);
            this.chkIngTribDesc.TabIndex = 95;
            this.chkIngTribDesc.Text = "Datos del detalle de los Ingresos , Tributos y  Descuentos del Trabajador";
            this.chkIngTribDesc.UseVisualStyleBackColor = true;
            // 
            // chkEmpDestacoPer
            // 
            this.chkEmpDestacoPer.AutoSize = true;
            this.chkEmpDestacoPer.Location = new System.Drawing.Point(22, 37);
            this.chkEmpDestacoPer.Name = "chkEmpDestacoPer";
            this.chkEmpDestacoPer.Size = new System.Drawing.Size(306, 17);
            this.chkEmpDestacoPer.TabIndex = 96;
            this.chkEmpDestacoPer.Text = "Datos del empresas a quienes destado o desplazo personal";
            this.chkEmpDestacoPer.UseVisualStyleBackColor = true;
            // 
            // chkTrabPen
            // 
            this.chkTrabPen.AutoSize = true;
            this.chkTrabPen.Location = new System.Drawing.Point(22, 60);
            this.chkTrabPen.Name = "chkTrabPen";
            this.chkTrabPen.Size = new System.Drawing.Size(390, 17);
            this.chkTrabPen.TabIndex = 97;
            this.chkTrabPen.Text = "Datos principales del trabajador , pensionista o prestador de servicios - cuarta";
            this.chkTrabPen.UseVisualStyleBackColor = true;
            // 
            // chkDerHab
            // 
            this.chkDerHab.AutoSize = true;
            this.chkDerHab.Location = new System.Drawing.Point(22, 106);
            this.chkDerHab.Name = "chkDerHab";
            this.chkDerHab.Size = new System.Drawing.Size(159, 17);
            this.chkDerHab.TabIndex = 98;
            this.chkDerHab.Text = "Datos de Derechohabientes";
            this.chkDerHab.UseVisualStyleBackColor = true;
            this.chkDerHab.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // chkJorTrab
            // 
            this.chkJorTrab.AutoSize = true;
            this.chkJorTrab.Location = new System.Drawing.Point(22, 19);
            this.chkJorTrab.Name = "chkJorTrab";
            this.chkJorTrab.Size = new System.Drawing.Size(226, 17);
            this.chkJorTrab.TabIndex = 99;
            this.chkJorTrab.Text = "Datos de la Jornada Laboral del trabajador";
            this.chkJorTrab.UseVisualStyleBackColor = true;
            // 
            // chkDiasSubsi
            // 
            this.chkDiasSubsi.AutoSize = true;
            this.chkDiasSubsi.Location = new System.Drawing.Point(22, 62);
            this.chkDiasSubsi.Name = "chkDiasSubsi";
            this.chkDiasSubsi.Size = new System.Drawing.Size(240, 17);
            this.chkDiasSubsi.TabIndex = 100;
            this.chkDiasSubsi.Text = "Datos de los días Subsidiados del Trabajador";
            this.chkDiasSubsi.UseVisualStyleBackColor = true;
            // 
            // chkDiasNoSubsi
            // 
            this.chkDiasNoSubsi.AutoSize = true;
            this.chkDiasNoSubsi.Location = new System.Drawing.Point(22, 85);
            this.chkDiasNoSubsi.Name = "chkDiasNoSubsi";
            this.chkDiasNoSubsi.Size = new System.Drawing.Size(338, 17);
            this.chkDiasNoSubsi.TabIndex = 101;
            this.chkDiasNoSubsi.Text = "Datos de los días No Trabajados y No Subsidiados del Trabajador";
            this.chkDiasNoSubsi.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.chkPeriodos);
            this.groupBox2.Controls.Add(this.chkDatosTrabajador);
            this.groupBox2.Controls.Add(this.chkEstPropios);
            this.groupBox2.Controls.Add(this.chkEmpDestacoPer);
            this.groupBox2.Controls.Add(this.chkTrabPen);
            this.groupBox2.Controls.Add(this.chkDerHab);
            this.groupBox2.Location = new System.Drawing.Point(10, 158);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(538, 174);
            this.groupBox2.TabIndex = 102;
            this.groupBox2.TabStop = false;
            // 
            // chkPeriodos
            // 
            this.chkPeriodos.AutoSize = true;
            this.chkPeriodos.Location = new System.Drawing.Point(22, 129);
            this.chkPeriodos.Name = "chkPeriodos";
            this.chkPeriodos.Size = new System.Drawing.Size(113, 17);
            this.chkPeriodos.TabIndex = 100;
            this.chkPeriodos.Text = "Datos de Periodos";
            this.chkPeriodos.UseVisualStyleBackColor = true;
            // 
            // chkDatosTrabajador
            // 
            this.chkDatosTrabajador.AutoSize = true;
            this.chkDatosTrabajador.Location = new System.Drawing.Point(22, 83);
            this.chkDatosTrabajador.Name = "chkDatosTrabajador";
            this.chkDatosTrabajador.Size = new System.Drawing.Size(125, 17);
            this.chkDatosTrabajador.TabIndex = 99;
            this.chkDatosTrabajador.Text = "Datos del Trabajador";
            this.chkDatosTrabajador.UseVisualStyleBackColor = true;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.chkJorTrab);
            this.groupBox4.Controls.Add(this.chkIngTribDesc);
            this.groupBox4.Controls.Add(this.chkDiasNoSubsi);
            this.groupBox4.Controls.Add(this.chkDiasSubsi);
            this.groupBox4.Location = new System.Drawing.Point(10, 338);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(538, 117);
            this.groupBox4.TabIndex = 103;
            this.groupBox4.TabStop = false;
            // 
            // chkEstaTrab
            // 
            this.chkEstaTrab.AutoSize = true;
            this.chkEstaTrab.Location = new System.Drawing.Point(32, 310);
            this.chkEstaTrab.Name = "chkEstaTrab";
            this.chkEstaTrab.Size = new System.Drawing.Size(281, 17);
            this.chkEstaTrab.TabIndex = 101;
            this.chkEstaTrab.Text = "Datos de Establecimientos donde labora el Trabajador";
            this.chkEstaTrab.UseVisualStyleBackColor = true;
            // 
            // ui_PlanElecSunat
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.HighlightText;
            this.ClientSize = new System.Drawing.Size(558, 508);
            this.ControlBox = false;
            this.Controls.Add(this.chkEstaTrab);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.pictureBox1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ui_PlanElecSunat";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Exportación de Archivos para PDT 0601 Planilla Electrónica";
            this.Load += new System.EventHandler(this.ui_PlanElecSunat_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.ToolStripButton btnProceso;
        private System.Windows.Forms.ToolStripButton btnSalir;
        private System.Windows.Forms.MaskedTextBox txtAnio;
        private System.Windows.Forms.Label lblUsuario;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox cmbTipoPlan;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cmbTipoTrabajador;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton btnNuevo;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.ComboBox cmbMes;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.CheckBox chkEstPropios;
        private System.Windows.Forms.CheckBox chkIngTribDesc;
        private System.Windows.Forms.CheckBox chkEmpDestacoPer;
        private System.Windows.Forms.CheckBox chkTrabPen;
        private System.Windows.Forms.CheckBox chkDerHab;
        private System.Windows.Forms.CheckBox chkJorTrab;
        private System.Windows.Forms.CheckBox chkDiasSubsi;
        private System.Windows.Forms.CheckBox chkDiasNoSubsi;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.CheckBox chkDatosTrabajador;
        private System.Windows.Forms.CheckBox chkPeriodos;
        private System.Windows.Forms.CheckBox chkEstaTrab;
    }
}