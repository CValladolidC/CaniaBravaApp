namespace CaniaBrava
{
    partial class ui_updcalplan
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
            this.txtOperacion = new System.Windows.Forms.TextBox();
            this.lblOperacion = new System.Windows.Forms.Label();
            this.lblRazon = new System.Windows.Forms.Label();
            this.lblUsuario = new System.Windows.Forms.Label();
            this.toolStripForm = new System.Windows.Forms.ToolStrip();
            this.btnAceptar = new System.Windows.Forms.ToolStripButton();
            this.btnCancelar = new System.Windows.Forms.ToolStripButton();
            this.txtMesSem = new System.Windows.Forms.MaskedTextBox();
            this.txtAnio = new System.Windows.Forms.MaskedTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtTipo = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cmbMes = new System.Windows.Forms.ComboBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.cmbMesPDT = new System.Windows.Forms.ComboBox();
            this.txtAnioPDT = new System.Windows.Forms.MaskedTextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtDiasDom = new System.Windows.Forms.TextBox();
            this.label28 = new System.Windows.Forms.Label();
            this.grpDias = new System.Windows.Forms.GroupBox();
            this.grpQuinta = new System.Windows.Forms.GroupBox();
            this.chkSalda = new System.Windows.Forms.CheckBox();
            this.dtpFechaIni = new System.Windows.Forms.DateTimePicker();
            this.dtpFechaFin = new System.Windows.Forms.DateTimePicker();
            this.label6 = new System.Windows.Forms.Label();
            this.toolStripForm.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.grpDias.SuspendLayout();
            this.grpQuinta.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtOperacion
            // 
            this.txtOperacion.BackColor = System.Drawing.SystemColors.HighlightText;
            this.txtOperacion.Enabled = false;
            this.txtOperacion.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.txtOperacion.Location = new System.Drawing.Point(173, 15);
            this.txtOperacion.MaxLength = 6;
            this.txtOperacion.Name = "txtOperacion";
            this.txtOperacion.ReadOnly = true;
            this.txtOperacion.Size = new System.Drawing.Size(171, 20);
            this.txtOperacion.TabIndex = 0;
            // 
            // lblOperacion
            // 
            this.lblOperacion.AutoSize = true;
            this.lblOperacion.Location = new System.Drawing.Point(22, 19);
            this.lblOperacion.Name = "lblOperacion";
            this.lblOperacion.Size = new System.Drawing.Size(59, 13);
            this.lblOperacion.TabIndex = 75;
            this.lblOperacion.Text = "Operación:";
            // 
            // lblRazon
            // 
            this.lblRazon.AutoSize = true;
            this.lblRazon.Location = new System.Drawing.Point(16, 72);
            this.lblRazon.Name = "lblRazon";
            this.lblRazon.Size = new System.Drawing.Size(32, 13);
            this.lblRazon.TabIndex = 72;
            this.lblRazon.Text = "Año :";
            // 
            // lblUsuario
            // 
            this.lblUsuario.AutoSize = true;
            this.lblUsuario.Location = new System.Drawing.Point(16, 19);
            this.lblUsuario.Name = "lblUsuario";
            this.lblUsuario.Size = new System.Drawing.Size(140, 13);
            this.lblUsuario.TabIndex = 71;
            this.lblUsuario.Text = "Semana / Mes / Quincena :";
            // 
            // toolStripForm
            // 
            this.toolStripForm.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.toolStripForm.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.toolStripForm.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnAceptar,
            this.btnCancelar});
            this.toolStripForm.Location = new System.Drawing.Point(0, 468);
            this.toolStripForm.Name = "toolStripForm";
            this.toolStripForm.ShowItemToolTips = false;
            this.toolStripForm.Size = new System.Drawing.Size(491, 25);
            this.toolStripForm.TabIndex = 3;
            // 
            // btnAceptar
            // 
            this.btnAceptar.Image = global::CaniaBrava.Properties.Resources.SAVE;
            this.btnAceptar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnAceptar.Name = "btnAceptar";
            this.btnAceptar.Size = new System.Drawing.Size(68, 22);
            this.btnAceptar.Text = "Aceptar";
            this.btnAceptar.Click += new System.EventHandler(this.btnAceptar_Click);
            // 
            // btnCancelar
            // 
            this.btnCancelar.Image = global::CaniaBrava.Properties.Resources.UNDO;
            this.btnCancelar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(73, 22);
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // txtMesSem
            // 
            this.txtMesSem.BackColor = System.Drawing.SystemColors.HighlightText;
            this.txtMesSem.Location = new System.Drawing.Point(167, 15);
            this.txtMesSem.Mask = "99";
            this.txtMesSem.Name = "txtMesSem";
            this.txtMesSem.PromptChar = ' ';
            this.txtMesSem.Size = new System.Drawing.Size(171, 20);
            this.txtMesSem.TabIndex = 0;
            this.txtMesSem.MaskInputRejected += new System.Windows.Forms.MaskInputRejectedEventHandler(this.txtMesSem_MaskInputRejected);
            this.txtMesSem.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtMesSem_KeyPress);
            // 
            // txtAnio
            // 
            this.txtAnio.BackColor = System.Drawing.SystemColors.HighlightText;
            this.txtAnio.Location = new System.Drawing.Point(167, 68);
            this.txtAnio.Mask = "9999";
            this.txtAnio.Name = "txtAnio";
            this.txtAnio.PromptChar = ' ';
            this.txtAnio.Size = new System.Drawing.Size(171, 20);
            this.txtAnio.TabIndex = 2;
            this.txtAnio.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtAnio_KeyPress);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 98);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(71, 13);
            this.label1.TabIndex = 79;
            this.label1.Text = "Fecha Inicio :";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(16, 123);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(60, 13);
            this.label2.TabIndex = 81;
            this.label2.Text = "Fecha Fin :";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(22, 43);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(78, 13);
            this.label4.TabIndex = 86;
            this.label4.Text = "Tipo Personal :";
            // 
            // txtTipo
            // 
            this.txtTipo.BackColor = System.Drawing.SystemColors.HighlightText;
            this.txtTipo.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtTipo.Enabled = false;
            this.txtTipo.Location = new System.Drawing.Point(173, 39);
            this.txtTipo.MaxLength = 2;
            this.txtTipo.Name = "txtTipo";
            this.txtTipo.Size = new System.Drawing.Size(171, 20);
            this.txtTipo.TabIndex = 1;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.txtTipo);
            this.groupBox1.Controls.Add(this.txtOperacion);
            this.groupBox1.Controls.Add(this.lblOperacion);
            this.groupBox1.Location = new System.Drawing.Point(13, 34);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(466, 71);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.dtpFechaFin);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.cmbMes);
            this.groupBox2.Controls.Add(this.dtpFechaIni);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.txtAnio);
            this.groupBox2.Controls.Add(this.txtMesSem);
            this.groupBox2.Controls.Add(this.lblRazon);
            this.groupBox2.Controls.Add(this.lblUsuario);
            this.groupBox2.Location = new System.Drawing.Point(14, 106);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(465, 151);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(16, 44);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(33, 13);
            this.label3.TabIndex = 90;
            this.label3.Text = "Mes :";
            // 
            // cmbMes
            // 
            this.cmbMes.FormattingEnabled = true;
            this.cmbMes.Location = new System.Drawing.Point(167, 41);
            this.cmbMes.Name = "cmbMes";
            this.cmbMes.Size = new System.Drawing.Size(171, 21);
            this.cmbMes.TabIndex = 1;
            this.cmbMes.SelectedIndexChanged += new System.EventHandler(this.cmbMes_SelectedIndexChanged);
            this.cmbMes.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cmbMes_KeyPress);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label5);
            this.groupBox3.Controls.Add(this.cmbMesPDT);
            this.groupBox3.Controls.Add(this.txtAnioPDT);
            this.groupBox3.Controls.Add(this.label8);
            this.groupBox3.Location = new System.Drawing.Point(15, 321);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(465, 82);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Declaración PDT - 0601 Planilla Electrónica";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(16, 26);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(33, 13);
            this.label5.TabIndex = 90;
            this.label5.Text = "Mes :";
            // 
            // cmbMesPDT
            // 
            this.cmbMesPDT.FormattingEnabled = true;
            this.cmbMesPDT.Location = new System.Drawing.Point(167, 23);
            this.cmbMesPDT.Name = "cmbMesPDT";
            this.cmbMesPDT.Size = new System.Drawing.Size(171, 21);
            this.cmbMesPDT.TabIndex = 0;
            this.cmbMesPDT.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cmbMesPDT_KeyPress);
            // 
            // txtAnioPDT
            // 
            this.txtAnioPDT.BackColor = System.Drawing.SystemColors.HighlightText;
            this.txtAnioPDT.Location = new System.Drawing.Point(167, 50);
            this.txtAnioPDT.Mask = "9999";
            this.txtAnioPDT.Name = "txtAnioPDT";
            this.txtAnioPDT.PromptChar = ' ';
            this.txtAnioPDT.Size = new System.Drawing.Size(171, 20);
            this.txtAnioPDT.TabIndex = 1;
            this.txtAnioPDT.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtAnioPDT_KeyPress);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(16, 54);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(32, 13);
            this.label8.TabIndex = 72;
            this.label8.Text = "Año :";
            // 
            // txtDiasDom
            // 
            this.txtDiasDom.Location = new System.Drawing.Point(166, 19);
            this.txtDiasDom.Name = "txtDiasDom";
            this.txtDiasDom.Size = new System.Drawing.Size(80, 20);
            this.txtDiasDom.TabIndex = 59;
            this.txtDiasDom.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtDiasDom.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtDiasDom_KeyPress);
            // 
            // label28
            // 
            this.label28.AutoSize = true;
            this.label28.Location = new System.Drawing.Point(15, 19);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(94, 13);
            this.label28.TabIndex = 64;
            this.label28.Text = "Dias Dominicales :";
            // 
            // grpDias
            // 
            this.grpDias.Controls.Add(this.txtDiasDom);
            this.grpDias.Controls.Add(this.label28);
            this.grpDias.Location = new System.Drawing.Point(15, 262);
            this.grpDias.Name = "grpDias";
            this.grpDias.Size = new System.Drawing.Size(464, 50);
            this.grpDias.TabIndex = 58;
            this.grpDias.TabStop = false;
            // 
            // grpQuinta
            // 
            this.grpQuinta.Controls.Add(this.chkSalda);
            this.grpQuinta.Location = new System.Drawing.Point(15, 409);
            this.grpQuinta.Name = "grpQuinta";
            this.grpQuinta.Size = new System.Drawing.Size(464, 50);
            this.grpQuinta.TabIndex = 59;
            this.grpQuinta.TabStop = false;
            // 
            // chkSalda
            // 
            this.chkSalda.AutoSize = true;
            this.chkSalda.Location = new System.Drawing.Point(18, 19);
            this.chkSalda.Name = "chkSalda";
            this.chkSalda.Size = new System.Drawing.Size(295, 17);
            this.chkSalda.TabIndex = 0;
            this.chkSalda.Text = "El Periodo Laboral finaliza el Cálculo de Quinta Categoría";
            this.chkSalda.UseVisualStyleBackColor = true;
            // 
            // dtpFechaIni
            // 
            this.dtpFechaIni.CustomFormat = "";
            this.dtpFechaIni.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFechaIni.Location = new System.Drawing.Point(167, 94);
            this.dtpFechaIni.Name = "dtpFechaIni";
            this.dtpFechaIni.Size = new System.Drawing.Size(102, 20);
            this.dtpFechaIni.TabIndex = 60;
            // 
            // dtpFechaFin
            // 
            this.dtpFechaFin.CustomFormat = "";
            this.dtpFechaFin.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFechaFin.Location = new System.Drawing.Point(166, 120);
            this.dtpFechaFin.Name = "dtpFechaFin";
            this.dtpFechaFin.Size = new System.Drawing.Size(103, 20);
            this.dtpFechaFin.TabIndex = 61;
            // 
            // label6
            // 
            this.label6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.White;
            this.label6.Location = new System.Drawing.Point(10, 7);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(470, 24);
            this.label6.TabIndex = 60;
            this.label6.Text = "Calendario de Planilla";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ui_updcalplan
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.HighlightText;
            this.ClientSize = new System.Drawing.Size(491, 493);
            this.ControlBox = false;
            this.Controls.Add(this.label6);
            this.Controls.Add(this.grpQuinta);
            this.Controls.Add(this.grpDias);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.toolStripForm);
            this.ForeColor = System.Drawing.SystemColors.ControlText;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ui_updcalplan";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = " ";
            this.Load += new System.EventHandler(this.ui_updcalplan_Load);
            this.toolStripForm.ResumeLayout(false);
            this.toolStripForm.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.grpDias.ResumeLayout(false);
            this.grpDias.PerformLayout();
            this.grpQuinta.ResumeLayout(false);
            this.grpQuinta.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtOperacion;
        private System.Windows.Forms.Label lblOperacion;
        private System.Windows.Forms.Label lblRazon;
        private System.Windows.Forms.Label lblUsuario;
        private System.Windows.Forms.ToolStrip toolStripForm;
        private System.Windows.Forms.ToolStripButton btnAceptar;
        private System.Windows.Forms.ToolStripButton btnCancelar;
        private System.Windows.Forms.MaskedTextBox txtMesSem;
        private System.Windows.Forms.MaskedTextBox txtAnio;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtTipo;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cmbMes;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cmbMesPDT;
        private System.Windows.Forms.MaskedTextBox txtAnioPDT;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtDiasDom;
        private System.Windows.Forms.Label label28;
        private System.Windows.Forms.GroupBox grpDias;
        private System.Windows.Forms.GroupBox grpQuinta;
        private System.Windows.Forms.CheckBox chkSalda;
        private System.Windows.Forms.DateTimePicker dtpFechaIni;
        private System.Windows.Forms.DateTimePicker dtpFechaFin;
        private System.Windows.Forms.Label label6;
    }
}