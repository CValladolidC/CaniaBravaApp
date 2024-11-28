namespace CaniaBrava
{
    partial class ui_resumendestajoperiodo
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
            this.toolstripform = new System.Windows.Forms.ToolStrip();
            this.btnVisualizar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnSalir = new System.Windows.Forms.ToolStripButton();
            this.groupBox46 = new System.Windows.Forms.GroupBox();
            this.label62 = new System.Windows.Forms.Label();
            this.cmbEstablecimiento = new System.Windows.Forms.ComboBox();
            this.label61 = new System.Windows.Forms.Label();
            this.cmbEmpleador = new System.Windows.Forms.ComboBox();
            this.label60 = new System.Windows.Forms.Label();
            this.radioButtonSiEmp = new System.Windows.Forms.RadioButton();
            this.radioButtonNoEmp = new System.Windows.Forms.RadioButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.lblTipoPlanilla = new System.Windows.Forms.Label();
            this.cmbTipoPlan = new System.Windows.Forms.ComboBox();
            this.cmbTipoRegistro = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.btnBuscar = new System.Windows.Forms.Button();
            this.txtFechaFin = new System.Windows.Forms.MaskedTextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtFechaIni = new System.Windows.Forms.MaskedTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtMesSem = new System.Windows.Forms.MaskedTextBox();
            this.lblUsuario = new System.Windows.Forms.Label();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.cmbTipoReporte = new System.Windows.Forms.ComboBox();
            this.toolstripform.SuspendLayout();
            this.groupBox46.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolstripform
            // 
            this.toolstripform.BackColor = System.Drawing.SystemColors.HighlightText;
            this.toolstripform.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.toolstripform.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnVisualizar,
            this.toolStripSeparator1,
            this.btnSalir});
            this.toolstripform.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.toolstripform.Location = new System.Drawing.Point(0, 319);
            this.toolstripform.Name = "toolstripform";
            this.toolstripform.ShowItemToolTips = false;
            this.toolstripform.Size = new System.Drawing.Size(574, 33);
            this.toolstripform.TabIndex = 110;
            // 
            // btnVisualizar
            // 
            this.btnVisualizar.Image = global::CaniaBrava.Properties.Resources.PRINT;
            this.btnVisualizar.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnVisualizar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnVisualizar.Name = "btnVisualizar";
            this.btnVisualizar.Size = new System.Drawing.Size(83, 30);
            this.btnVisualizar.Text = "Visualizar";
            this.btnVisualizar.Click += new System.EventHandler(this.btnVisualizar_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 33);
            // 
            // btnSalir
            // 
            this.btnSalir.Image = global::CaniaBrava.Properties.Resources.CLOSE;
            this.btnSalir.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnSalir.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSalir.Name = "btnSalir";
            this.btnSalir.Size = new System.Drawing.Size(49, 30);
            this.btnSalir.Text = "Salir";
            this.btnSalir.Click += new System.EventHandler(this.btnSalir_Click);
            // 
            // groupBox46
            // 
            this.groupBox46.Controls.Add(this.label62);
            this.groupBox46.Controls.Add(this.cmbEstablecimiento);
            this.groupBox46.Controls.Add(this.label61);
            this.groupBox46.Controls.Add(this.cmbEmpleador);
            this.groupBox46.Controls.Add(this.label60);
            this.groupBox46.Controls.Add(this.radioButtonSiEmp);
            this.groupBox46.Controls.Add(this.radioButtonNoEmp);
            this.groupBox46.Location = new System.Drawing.Point(32, 112);
            this.groupBox46.Name = "groupBox46";
            this.groupBox46.Size = new System.Drawing.Size(510, 120);
            this.groupBox46.TabIndex = 108;
            this.groupBox46.TabStop = false;
            this.groupBox46.Text = "Empleador";
            // 
            // label62
            // 
            this.label62.AutoSize = true;
            this.label62.Location = new System.Drawing.Point(11, 74);
            this.label62.Name = "label62";
            this.label62.Size = new System.Drawing.Size(128, 13);
            this.label62.TabIndex = 74;
            this.label62.Text = "Establecimiento Asignado";
            // 
            // cmbEstablecimiento
            // 
            this.cmbEstablecimiento.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbEstablecimiento.FormattingEnabled = true;
            this.cmbEstablecimiento.Location = new System.Drawing.Point(14, 90);
            this.cmbEstablecimiento.Name = "cmbEstablecimiento";
            this.cmbEstablecimiento.Size = new System.Drawing.Size(407, 21);
            this.cmbEstablecimiento.TabIndex = 73;
            // 
            // label61
            // 
            this.label61.AutoSize = true;
            this.label61.Location = new System.Drawing.Point(10, 35);
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
            this.cmbEmpleador.Location = new System.Drawing.Point(13, 51);
            this.cmbEmpleador.Name = "cmbEmpleador";
            this.cmbEmpleador.Size = new System.Drawing.Size(407, 21);
            this.cmbEmpleador.TabIndex = 71;
            this.cmbEmpleador.SelectedIndexChanged += new System.EventHandler(this.cmbEmpleador_SelectedIndexChanged);
            // 
            // label60
            // 
            this.label60.AutoSize = true;
            this.label60.Location = new System.Drawing.Point(10, 17);
            this.label60.Name = "label60";
            this.label60.Size = new System.Drawing.Size(293, 13);
            this.label60.TabIndex = 65;
            this.label60.Text = "¿ El trabajador es asignado o desplazado a otro Empleador ?";
            // 
            // radioButtonSiEmp
            // 
            this.radioButtonSiEmp.AutoSize = true;
            this.radioButtonSiEmp.BackColor = System.Drawing.SystemColors.HighlightText;
            this.radioButtonSiEmp.Location = new System.Drawing.Point(337, 15);
            this.radioButtonSiEmp.Name = "radioButtonSiEmp";
            this.radioButtonSiEmp.Size = new System.Drawing.Size(34, 17);
            this.radioButtonSiEmp.TabIndex = 63;
            this.radioButtonSiEmp.Text = "Si";
            this.radioButtonSiEmp.UseVisualStyleBackColor = false;
            this.radioButtonSiEmp.CheckedChanged += new System.EventHandler(this.radioButtonSiEmp_CheckedChanged);
            // 
            // radioButtonNoEmp
            // 
            this.radioButtonNoEmp.AutoSize = true;
            this.radioButtonNoEmp.BackColor = System.Drawing.SystemColors.HighlightText;
            this.radioButtonNoEmp.Checked = true;
            this.radioButtonNoEmp.Location = new System.Drawing.Point(408, 15);
            this.radioButtonNoEmp.Name = "radioButtonNoEmp";
            this.radioButtonNoEmp.Size = new System.Drawing.Size(39, 17);
            this.radioButtonNoEmp.TabIndex = 64;
            this.radioButtonNoEmp.TabStop = true;
            this.radioButtonNoEmp.Text = "No";
            this.radioButtonNoEmp.UseVisualStyleBackColor = false;
            this.radioButtonNoEmp.CheckedChanged += new System.EventHandler(this.radioButtonNoEmp_CheckedChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.lblTipoPlanilla);
            this.groupBox2.Controls.Add(this.cmbTipoPlan);
            this.groupBox2.Controls.Add(this.cmbTipoRegistro);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Location = new System.Drawing.Point(32, 57);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(510, 52);
            this.groupBox2.TabIndex = 106;
            this.groupBox2.TabStop = false;
            // 
            // lblTipoPlanilla
            // 
            this.lblTipoPlanilla.AutoSize = true;
            this.lblTipoPlanilla.Location = new System.Drawing.Point(291, 20);
            this.lblTipoPlanilla.Name = "lblTipoPlanilla";
            this.lblTipoPlanilla.Size = new System.Drawing.Size(70, 13);
            this.lblTipoPlanilla.TabIndex = 80;
            this.lblTipoPlanilla.Text = "Tipo Planilla :";
            this.lblTipoPlanilla.Visible = false;
            // 
            // cmbTipoPlan
            // 
            this.cmbTipoPlan.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTipoPlan.FormattingEnabled = true;
            this.cmbTipoPlan.Location = new System.Drawing.Point(361, 16);
            this.cmbTipoPlan.Name = "cmbTipoPlan";
            this.cmbTipoPlan.Size = new System.Drawing.Size(137, 21);
            this.cmbTipoPlan.TabIndex = 79;
            this.cmbTipoPlan.Visible = false;
            // 
            // cmbTipoRegistro
            // 
            this.cmbTipoRegistro.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTipoRegistro.FormattingEnabled = true;
            this.cmbTipoRegistro.Items.AddRange(new object[] {
            "P   PERSONAL DE PLANILLA",
            "R   PERSONAL DE RETENCIONES"});
            this.cmbTipoRegistro.Location = new System.Drawing.Point(97, 16);
            this.cmbTipoRegistro.Name = "cmbTipoRegistro";
            this.cmbTipoRegistro.Size = new System.Drawing.Size(183, 21);
            this.cmbTipoRegistro.TabIndex = 3;
            this.cmbTipoRegistro.SelectedIndexChanged += new System.EventHandler(this.cmbTipoRegistro_SelectedIndexChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(5, 20);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(91, 13);
            this.label6.TabIndex = 103;
            this.label6.Text = "Tipo de Registro :";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.btnBuscar);
            this.groupBox3.Controls.Add(this.txtFechaFin);
            this.groupBox3.Controls.Add(this.label2);
            this.groupBox3.Controls.Add(this.txtFechaIni);
            this.groupBox3.Controls.Add(this.label1);
            this.groupBox3.Controls.Add(this.txtMesSem);
            this.groupBox3.Controls.Add(this.lblUsuario);
            this.groupBox3.Location = new System.Drawing.Point(32, 12);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(510, 42);
            this.groupBox3.TabIndex = 111;
            this.groupBox3.TabStop = false;
            // 
            // btnBuscar
            // 
            this.btnBuscar.Image = global::CaniaBrava.Properties.Resources.LOCATE;
            this.btnBuscar.Location = new System.Drawing.Point(180, 12);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(30, 23);
            this.btnBuscar.TabIndex = 104;
            this.btnBuscar.UseVisualStyleBackColor = true;
            this.btnBuscar.Click += new System.EventHandler(this.btnBuscar_Click);
            // 
            // txtFechaFin
            // 
            this.txtFechaFin.BackColor = System.Drawing.SystemColors.HighlightText;
            this.txtFechaFin.Enabled = false;
            this.txtFechaFin.Location = new System.Drawing.Point(428, 14);
            this.txtFechaFin.Mask = "00/00/0000";
            this.txtFechaFin.Name = "txtFechaFin";
            this.txtFechaFin.Size = new System.Drawing.Size(70, 20);
            this.txtFechaFin.TabIndex = 3;
            this.txtFechaFin.ValidatingType = typeof(System.DateTime);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(367, 18);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(60, 13);
            this.label2.TabIndex = 97;
            this.label2.Text = "Fecha Fin :";
            // 
            // txtFechaIni
            // 
            this.txtFechaIni.BackColor = System.Drawing.SystemColors.HighlightText;
            this.txtFechaIni.Enabled = false;
            this.txtFechaIni.Location = new System.Drawing.Point(294, 14);
            this.txtFechaIni.Mask = "00/00/0000";
            this.txtFechaIni.Name = "txtFechaIni";
            this.txtFechaIni.Size = new System.Drawing.Size(70, 20);
            this.txtFechaIni.TabIndex = 2;
            this.txtFechaIni.ValidatingType = typeof(System.DateTime);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(217, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(71, 13);
            this.label1.TabIndex = 96;
            this.label1.Text = "Fecha Inicio :";
            // 
            // txtMesSem
            // 
            this.txtMesSem.BackColor = System.Drawing.SystemColors.HighlightText;
            this.txtMesSem.Location = new System.Drawing.Point(97, 14);
            this.txtMesSem.Mask = "99/9999";
            this.txtMesSem.Name = "txtMesSem";
            this.txtMesSem.Size = new System.Drawing.Size(77, 20);
            this.txtMesSem.TabIndex = 1;
            this.txtMesSem.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtMesSem_KeyPress);
            // 
            // lblUsuario
            // 
            this.lblUsuario.AutoSize = true;
            this.lblUsuario.Location = new System.Drawing.Point(5, 18);
            this.lblUsuario.Name = "lblUsuario";
            this.lblUsuario.Size = new System.Drawing.Size(52, 13);
            this.lblUsuario.TabIndex = 95;
            this.lblUsuario.Text = "Periodo  :";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.cmbTipoReporte);
            this.groupBox4.Location = new System.Drawing.Point(32, 236);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(510, 54);
            this.groupBox4.TabIndex = 108;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Tipo de Reporte";
            // 
            // cmbTipoReporte
            // 
            this.cmbTipoReporte.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTipoReporte.FormattingEnabled = true;
            this.cmbTipoReporte.Items.AddRange(new object[] {
            "1    DETALLADO POR ZONAS",
            "2    RESUMEN GENERAL",
            "3    COMPARATIVO CON CALCULO DE PLANILLA"});
            this.cmbTipoReporte.Location = new System.Drawing.Point(14, 21);
            this.cmbTipoReporte.Name = "cmbTipoReporte";
            this.cmbTipoReporte.Size = new System.Drawing.Size(407, 21);
            this.cmbTipoReporte.TabIndex = 0;
            this.cmbTipoReporte.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // ui_resumendestajoperiodo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.HighlightText;
            this.ClientSize = new System.Drawing.Size(574, 352);
            this.ControlBox = false;
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.toolstripform);
            this.Controls.Add(this.groupBox46);
            this.Controls.Add(this.groupBox2);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ui_resumendestajoperiodo";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Resumen Destajo por Periodo";
            this.Load += new System.EventHandler(this.ui_resumendestajoperiodo_Load);
            this.toolstripform.ResumeLayout(false);
            this.toolstripform.PerformLayout();
            this.groupBox46.ResumeLayout(false);
            this.groupBox46.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.ToolStrip toolstripform;
        private System.Windows.Forms.ToolStripButton btnVisualizar;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton btnSalir;
        private System.Windows.Forms.GroupBox groupBox46;
        private System.Windows.Forms.Label label62;
        private System.Windows.Forms.ComboBox cmbEstablecimiento;
        private System.Windows.Forms.Label label61;
        private System.Windows.Forms.ComboBox cmbEmpleador;
        private System.Windows.Forms.Label label60;
        private System.Windows.Forms.RadioButton radioButtonSiEmp;
        private System.Windows.Forms.RadioButton radioButtonNoEmp;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label lblTipoPlanilla;
        private System.Windows.Forms.ComboBox cmbTipoPlan;
        private System.Windows.Forms.ComboBox cmbTipoRegistro;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button btnBuscar;
        private System.Windows.Forms.MaskedTextBox txtFechaFin;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.MaskedTextBox txtFechaIni;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.MaskedTextBox txtMesSem;
        private System.Windows.Forms.Label lblUsuario;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.ComboBox cmbTipoReporte;
    }
}