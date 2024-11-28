namespace CaniaBrava
{
    partial class ui_DestajoToPlan
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
            this.lblprocesando = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txtFechaFin = new System.Windows.Forms.MaskedTextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtFechaIni = new System.Windows.Forms.MaskedTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtMesSem = new System.Windows.Forms.MaskedTextBox();
            this.lblUsuario = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.cmbTipoCal = new System.Windows.Forms.ComboBox();
            this.cmbTipoPlan = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cmbTipoTrabajador = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.toolstripform = new System.Windows.Forms.ToolStrip();
            this.btnActualizar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnSalir = new System.Windows.Forms.ToolStripButton();
            this.groupBox46 = new System.Windows.Forms.GroupBox();
            this.label62 = new System.Windows.Forms.Label();
            this.label61 = new System.Windows.Forms.Label();
            this.cmbEstablecimiento = new System.Windows.Forms.ComboBox();
            this.cmbEmpleador = new System.Windows.Forms.ComboBox();
            this.label60 = new System.Windows.Forms.Label();
            this.radioButtonSiEmp = new System.Windows.Forms.RadioButton();
            this.radioButtonNoEmp = new System.Windows.Forms.RadioButton();
            this.lblPeriodo = new System.Windows.Forms.Label();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.toolstripform.SuspendLayout();
            this.groupBox46.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblprocesando
            // 
            this.lblprocesando.AutoSize = true;
            this.lblprocesando.BackColor = System.Drawing.Color.Blue;
            this.lblprocesando.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblprocesando.ForeColor = System.Drawing.Color.White;
            this.lblprocesando.Location = new System.Drawing.Point(401, 259);
            this.lblprocesando.Name = "lblprocesando";
            this.lblprocesando.Size = new System.Drawing.Size(84, 15);
            this.lblprocesando.TabIndex = 8;
            this.lblprocesando.Text = "PROCESANDO";
            this.lblprocesando.Visible = false;
            this.lblprocesando.Click += new System.EventHandler(this.lblprocesando_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.txtFechaFin);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.txtFechaIni);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.txtMesSem);
            this.groupBox2.Controls.Add(this.lblUsuario);
            this.groupBox2.Location = new System.Drawing.Point(302, 14);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(183, 120);
            this.groupBox2.TabIndex = 6;
            this.groupBox2.TabStop = false;
            this.groupBox2.Enter += new System.EventHandler(this.groupBox2_Enter);
            // 
            // txtFechaFin
            // 
            this.txtFechaFin.BackColor = System.Drawing.SystemColors.HighlightText;
            this.txtFechaFin.Enabled = false;
            this.txtFechaFin.Location = new System.Drawing.Point(80, 76);
            this.txtFechaFin.Mask = "00/00/0000";
            this.txtFechaFin.Name = "txtFechaFin";
            this.txtFechaFin.Size = new System.Drawing.Size(88, 20);
            this.txtFechaFin.TabIndex = 2;
            this.txtFechaFin.ValidatingType = typeof(System.DateTime);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 80);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(60, 13);
            this.label2.TabIndex = 98;
            this.label2.Text = "Fecha Fin :";
            // 
            // txtFechaIni
            // 
            this.txtFechaIni.BackColor = System.Drawing.SystemColors.HighlightText;
            this.txtFechaIni.Enabled = false;
            this.txtFechaIni.Location = new System.Drawing.Point(80, 50);
            this.txtFechaIni.Mask = "00/00/0000";
            this.txtFechaIni.Name = "txtFechaIni";
            this.txtFechaIni.Size = new System.Drawing.Size(88, 20);
            this.txtFechaIni.TabIndex = 1;
            this.txtFechaIni.ValidatingType = typeof(System.DateTime);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 54);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(71, 13);
            this.label1.TabIndex = 97;
            this.label1.Text = "Fecha Inicio :";
            // 
            // txtMesSem
            // 
            this.txtMesSem.BackColor = System.Drawing.SystemColors.HighlightText;
            this.txtMesSem.Location = new System.Drawing.Point(80, 24);
            this.txtMesSem.Mask = "99/9999";
            this.txtMesSem.Name = "txtMesSem";
            this.txtMesSem.Size = new System.Drawing.Size(88, 20);
            this.txtMesSem.TabIndex = 0;
            this.txtMesSem.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtMesSem_KeyPress);
            // 
            // lblUsuario
            // 
            this.lblUsuario.AutoSize = true;
            this.lblUsuario.Location = new System.Drawing.Point(13, 28);
            this.lblUsuario.Name = "lblUsuario";
            this.lblUsuario.Size = new System.Drawing.Size(52, 13);
            this.lblUsuario.TabIndex = 96;
            this.lblUsuario.Text = "Periodo  :";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.cmbTipoCal);
            this.groupBox1.Controls.Add(this.cmbTipoPlan);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.cmbTipoTrabajador);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Location = new System.Drawing.Point(10, 14);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(286, 120);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(4, 80);
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
            this.cmbTipoCal.Location = new System.Drawing.Point(106, 77);
            this.cmbTipoCal.Name = "cmbTipoCal";
            this.cmbTipoCal.Size = new System.Drawing.Size(164, 21);
            this.cmbTipoCal.TabIndex = 2;
            this.cmbTipoCal.SelectedIndexChanged += new System.EventHandler(this.cmbTipoCal_SelectedIndexChanged);
            // 
            // cmbTipoPlan
            // 
            this.cmbTipoPlan.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTipoPlan.FormattingEnabled = true;
            this.cmbTipoPlan.Location = new System.Drawing.Point(106, 49);
            this.cmbTipoPlan.Name = "cmbTipoPlan";
            this.cmbTipoPlan.Size = new System.Drawing.Size(164, 21);
            this.cmbTipoPlan.TabIndex = 1;
            this.cmbTipoPlan.SelectedIndexChanged += new System.EventHandler(this.cmbTipoPlan_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(4, 53);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(88, 13);
            this.label4.TabIndex = 90;
            this.label4.Text = "Tipo de Planilla  :";
            // 
            // cmbTipoTrabajador
            // 
            this.cmbTipoTrabajador.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTipoTrabajador.FormattingEnabled = true;
            this.cmbTipoTrabajador.Location = new System.Drawing.Point(106, 22);
            this.cmbTipoTrabajador.Name = "cmbTipoTrabajador";
            this.cmbTipoTrabajador.Size = new System.Drawing.Size(164, 21);
            this.cmbTipoTrabajador.TabIndex = 0;
            this.cmbTipoTrabajador.SelectedIndexChanged += new System.EventHandler(this.cmbTipoTrabajador_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(4, 26);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(96, 13);
            this.label3.TabIndex = 88;
            this.label3.Text = "Tipo de Personal  :";
            // 
            // toolstripform
            // 
            this.toolstripform.BackColor = System.Drawing.SystemColors.HighlightText;
            this.toolstripform.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.toolstripform.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnActualizar,
            this.toolStripSeparator1,
            this.btnSalir});
            this.toolstripform.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.toolstripform.Location = new System.Drawing.Point(0, 317);
            this.toolstripform.Name = "toolstripform";
            this.toolstripform.ShowItemToolTips = false;
            this.toolstripform.Size = new System.Drawing.Size(495, 25);
            this.toolstripform.TabIndex = 9;
            // 
            // btnActualizar
            // 
            this.btnActualizar.Image = global::CaniaBrava.Properties.Resources.refresh;
            this.btnActualizar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnActualizar.Name = "btnActualizar";
            this.btnActualizar.Size = new System.Drawing.Size(72, 22);
            this.btnActualizar.Text = "Procesar";
            this.btnActualizar.Click += new System.EventHandler(this.btnActualizar_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // btnSalir
            // 
            this.btnSalir.Image = global::CaniaBrava.Properties.Resources.CLOSE;
            this.btnSalir.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSalir.Name = "btnSalir";
            this.btnSalir.Size = new System.Drawing.Size(49, 22);
            this.btnSalir.Text = "Salir";
            this.btnSalir.Click += new System.EventHandler(this.btnSalir_Click);
            // 
            // groupBox46
            // 
            this.groupBox46.Controls.Add(this.label62);
            this.groupBox46.Controls.Add(this.label61);
            this.groupBox46.Controls.Add(this.cmbEstablecimiento);
            this.groupBox46.Controls.Add(this.cmbEmpleador);
            this.groupBox46.Controls.Add(this.label60);
            this.groupBox46.Controls.Add(this.radioButtonSiEmp);
            this.groupBox46.Controls.Add(this.radioButtonNoEmp);
            this.groupBox46.Location = new System.Drawing.Point(10, 136);
            this.groupBox46.Name = "groupBox46";
            this.groupBox46.Size = new System.Drawing.Size(475, 120);
            this.groupBox46.TabIndex = 7;
            this.groupBox46.TabStop = false;
            // 
            // label62
            // 
            this.label62.AutoSize = true;
            this.label62.Location = new System.Drawing.Point(10, 74);
            this.label62.Name = "label62";
            this.label62.Size = new System.Drawing.Size(128, 13);
            this.label62.TabIndex = 107;
            this.label62.Text = "Establecimiento Asignado";
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
            // cmbEstablecimiento
            // 
            this.cmbEstablecimiento.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbEstablecimiento.FormattingEnabled = true;
            this.cmbEstablecimiento.Location = new System.Drawing.Point(13, 90);
            this.cmbEstablecimiento.Name = "cmbEstablecimiento";
            this.cmbEstablecimiento.Size = new System.Drawing.Size(407, 21);
            this.cmbEstablecimiento.TabIndex = 3;
            // 
            // cmbEmpleador
            // 
            this.cmbEmpleador.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbEmpleador.Enabled = false;
            this.cmbEmpleador.FormattingEnabled = true;
            this.cmbEmpleador.Location = new System.Drawing.Point(13, 49);
            this.cmbEmpleador.Name = "cmbEmpleador";
            this.cmbEmpleador.Size = new System.Drawing.Size(407, 21);
            this.cmbEmpleador.TabIndex = 2;
            this.cmbEmpleador.SelectedIndexChanged += new System.EventHandler(this.cmbEmpleador_SelectedIndexChanged);
            // 
            // label60
            // 
            this.label60.AutoSize = true;
            this.label60.Location = new System.Drawing.Point(10, 14);
            this.label60.Name = "label60";
            this.label60.Size = new System.Drawing.Size(292, 13);
            this.label60.TabIndex = 65;
            this.label60.Text = "¿ Trabajadores asignados o desplazados a otro Empleador ?";
            // 
            // radioButtonSiEmp
            // 
            this.radioButtonSiEmp.AutoSize = true;
            this.radioButtonSiEmp.BackColor = System.Drawing.SystemColors.HighlightText;
            this.radioButtonSiEmp.Location = new System.Drawing.Point(344, 12);
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
            this.radioButtonNoEmp.Location = new System.Drawing.Point(387, 12);
            this.radioButtonNoEmp.Name = "radioButtonNoEmp";
            this.radioButtonNoEmp.Size = new System.Drawing.Size(39, 17);
            this.radioButtonNoEmp.TabIndex = 1;
            this.radioButtonNoEmp.TabStop = true;
            this.radioButtonNoEmp.Text = "No";
            this.radioButtonNoEmp.UseVisualStyleBackColor = false;
            this.radioButtonNoEmp.CheckedChanged += new System.EventHandler(this.radioButtonNoEmp_CheckedChanged);
            // 
            // lblPeriodo
            // 
            this.lblPeriodo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblPeriodo.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPeriodo.ForeColor = System.Drawing.Color.White;
            this.lblPeriodo.Location = new System.Drawing.Point(20, 269);
            this.lblPeriodo.Name = "lblPeriodo";
            this.lblPeriodo.Size = new System.Drawing.Size(204, 24);
            this.lblPeriodo.TabIndex = 85;
            this.lblPeriodo.Text = "Periodo Laboral Cerrado";
            this.lblPeriodo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblPeriodo.Visible = false;
            // 
            // ui_DestajoToPlan
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.HighlightText;
            this.ClientSize = new System.Drawing.Size(495, 342);
            this.ControlBox = false;
            this.Controls.Add(this.lblPeriodo);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.lblprocesando);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.toolstripform);
            this.Controls.Add(this.groupBox46);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ui_DestajoToPlan";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Transfiere Información de Destajo a Parte de Planilla";
            this.Load += new System.EventHandler(this.ui_DestajoToPlan_Load);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.toolstripform.ResumeLayout(false);
            this.toolstripform.PerformLayout();
            this.groupBox46.ResumeLayout(false);
            this.groupBox46.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblprocesando;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.MaskedTextBox txtFechaFin;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.MaskedTextBox txtFechaIni;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.MaskedTextBox txtMesSem;
        private System.Windows.Forms.Label lblUsuario;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cmbTipoCal;
        private System.Windows.Forms.ComboBox cmbTipoPlan;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cmbTipoTrabajador;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ToolStrip toolstripform;
        public System.Windows.Forms.ToolStripButton btnActualizar;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton btnSalir;
        private System.Windows.Forms.GroupBox groupBox46;
        private System.Windows.Forms.Label label62;
        private System.Windows.Forms.Label label61;
        private System.Windows.Forms.ComboBox cmbEstablecimiento;
        private System.Windows.Forms.ComboBox cmbEmpleador;
        private System.Windows.Forms.Label label60;
        private System.Windows.Forms.RadioButton radioButtonSiEmp;
        private System.Windows.Forms.RadioButton radioButtonNoEmp;
        private System.Windows.Forms.Label lblPeriodo;
    }
}