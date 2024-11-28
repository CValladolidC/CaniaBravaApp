namespace CaniaBrava
{
    partial class ui_estaneemp
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.txtRucEmp = new System.Windows.Forms.MaskedTextBox();
            this.txtIdCiaFile = new System.Windows.Forms.TextBox();
            this.txtRazonEmp = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.txtOpeEstEmp = new System.Windows.Forms.TextBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.txtCodigoEstEmp = new System.Windows.Forms.MaskedTextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.txtNombreEstEmp = new System.Windows.Forms.TextBox();
            this.cmbTipoEstEmp = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.groupBox8 = new System.Windows.Forms.GroupBox();
            this.radioButtonSiCR = new System.Windows.Forms.RadioButton();
            this.radioButtonNoCR = new System.Windows.Forms.RadioButton();
            this.dgvEstAne = new System.Windows.Forms.DataGridView();
            this.btnEditarEstAne = new System.Windows.Forms.Button();
            this.btnGrabarEstAne = new System.Windows.Forms.Button();
            this.btnEliminarEstAne = new System.Windows.Forms.Button();
            this.btnNuevoEstAne = new System.Windows.Forms.Button();
            this.toolStripForm = new System.Windows.Forms.ToolStrip();
            this.btnSalir = new System.Windows.Forms.ToolStripButton();
            this.btnTasaEsta = new System.Windows.Forms.Button();
            this.groupBox6.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox8.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvEstAne)).BeginInit();
            this.toolStripForm.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.txtRucEmp);
            this.groupBox6.Controls.Add(this.txtIdCiaFile);
            this.groupBox6.Controls.Add(this.txtRazonEmp);
            this.groupBox6.Controls.Add(this.label10);
            this.groupBox6.Controls.Add(this.label12);
            this.groupBox6.Location = new System.Drawing.Point(20, 24);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(627, 82);
            this.groupBox6.TabIndex = 69;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "Empleador a quien desplazo o destaco personal";
            // 
            // txtRucEmp
            // 
            this.txtRucEmp.BackColor = System.Drawing.SystemColors.HighlightText;
            this.txtRucEmp.Enabled = false;
            this.txtRucEmp.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtRucEmp.Location = new System.Drawing.Point(143, 20);
            this.txtRucEmp.Mask = "99999999999";
            this.txtRucEmp.Name = "txtRucEmp";
            this.txtRucEmp.PromptChar = ' ';
            this.txtRucEmp.Size = new System.Drawing.Size(171, 20);
            this.txtRucEmp.TabIndex = 64;
            this.txtRucEmp.ValidatingType = typeof(int);
            // 
            // txtIdCiaFile
            // 
            this.txtIdCiaFile.Enabled = false;
            this.txtIdCiaFile.Location = new System.Drawing.Point(439, 20);
            this.txtIdCiaFile.MaxLength = 2;
            this.txtIdCiaFile.Name = "txtIdCiaFile";
            this.txtIdCiaFile.Size = new System.Drawing.Size(100, 20);
            this.txtIdCiaFile.TabIndex = 67;
            this.txtIdCiaFile.Visible = false;
            // 
            // txtRazonEmp
            // 
            this.txtRazonEmp.BackColor = System.Drawing.SystemColors.HighlightText;
            this.txtRazonEmp.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtRazonEmp.Enabled = false;
            this.txtRazonEmp.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtRazonEmp.Location = new System.Drawing.Point(143, 46);
            this.txtRazonEmp.MaxLength = 120;
            this.txtRazonEmp.Name = "txtRazonEmp";
            this.txtRazonEmp.Size = new System.Drawing.Size(396, 20);
            this.txtRazonEmp.TabIndex = 63;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(12, 23);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(45, 13);
            this.label10.TabIndex = 66;
            this.label10.Text = "R.U.C. :";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(12, 49);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(76, 13);
            this.label12.TabIndex = 65;
            this.label12.Text = "Razón Social :";
            // 
            // txtOpeEstEmp
            // 
            this.txtOpeEstEmp.Enabled = false;
            this.txtOpeEstEmp.Location = new System.Drawing.Point(439, 19);
            this.txtOpeEstEmp.MaxLength = 2;
            this.txtOpeEstEmp.Name = "txtOpeEstEmp";
            this.txtOpeEstEmp.Size = new System.Drawing.Size(100, 20);
            this.txtOpeEstEmp.TabIndex = 38;
            this.txtOpeEstEmp.Visible = false;
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.txtCodigoEstEmp);
            this.groupBox5.Controls.Add(this.label7);
            this.groupBox5.Controls.Add(this.label8);
            this.groupBox5.Controls.Add(this.txtNombreEstEmp);
            this.groupBox5.Controls.Add(this.txtOpeEstEmp);
            this.groupBox5.Controls.Add(this.cmbTipoEstEmp);
            this.groupBox5.Controls.Add(this.label9);
            this.groupBox5.Location = new System.Drawing.Point(20, 270);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(628, 110);
            this.groupBox5.TabIndex = 0;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Domicilio Fiscal y Establecimientos Anexos";
            // 
            // txtCodigoEstEmp
            // 
            this.txtCodigoEstEmp.BackColor = System.Drawing.SystemColors.HighlightText;
            this.txtCodigoEstEmp.Enabled = false;
            this.txtCodigoEstEmp.Location = new System.Drawing.Point(143, 22);
            this.txtCodigoEstEmp.Mask = "9999";
            this.txtCodigoEstEmp.Name = "txtCodigoEstEmp";
            this.txtCodigoEstEmp.PromptChar = ' ';
            this.txtCodigoEstEmp.Size = new System.Drawing.Size(77, 20);
            this.txtCodigoEstEmp.TabIndex = 1;
            this.txtCodigoEstEmp.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCodigoEstEmp_KeyPress);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(12, 52);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(50, 13);
            this.label7.TabIndex = 41;
            this.label7.Text = "Nombre :";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(12, 26);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(46, 13);
            this.label8.TabIndex = 40;
            this.label8.Text = "Código :";
            // 
            // txtNombreEstEmp
            // 
            this.txtNombreEstEmp.BackColor = System.Drawing.SystemColors.HighlightText;
            this.txtNombreEstEmp.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtNombreEstEmp.Enabled = false;
            this.txtNombreEstEmp.Location = new System.Drawing.Point(143, 48);
            this.txtNombreEstEmp.MaxLength = 50;
            this.txtNombreEstEmp.Name = "txtNombreEstEmp";
            this.txtNombreEstEmp.Size = new System.Drawing.Size(396, 20);
            this.txtNombreEstEmp.TabIndex = 2;
            this.txtNombreEstEmp.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtNombreEstEmp_KeyPress);
            // 
            // cmbTipoEstEmp
            // 
            this.cmbTipoEstEmp.BackColor = System.Drawing.SystemColors.HighlightText;
            this.cmbTipoEstEmp.Enabled = false;
            this.cmbTipoEstEmp.FormattingEnabled = true;
            this.cmbTipoEstEmp.Location = new System.Drawing.Point(143, 74);
            this.cmbTipoEstEmp.Name = "cmbTipoEstEmp";
            this.cmbTipoEstEmp.Size = new System.Drawing.Size(396, 21);
            this.cmbTipoEstEmp.TabIndex = 3;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(12, 78);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(126, 13);
            this.label9.TabIndex = 23;
            this.label9.Text = "Tipo de Establecimiento :";
            // 
            // groupBox8
            // 
            this.groupBox8.Controls.Add(this.radioButtonSiCR);
            this.groupBox8.Controls.Add(this.radioButtonNoCR);
            this.groupBox8.Location = new System.Drawing.Point(20, 387);
            this.groupBox8.Name = "groupBox8";
            this.groupBox8.Size = new System.Drawing.Size(220, 55);
            this.groupBox8.TabIndex = 1;
            this.groupBox8.TabStop = false;
            this.groupBox8.Text = "Centro de Riesgo";
            // 
            // radioButtonSiCR
            // 
            this.radioButtonSiCR.AutoSize = true;
            this.radioButtonSiCR.BackColor = System.Drawing.SystemColors.HighlightText;
            this.radioButtonSiCR.Enabled = false;
            this.radioButtonSiCR.Location = new System.Drawing.Point(36, 19);
            this.radioButtonSiCR.Name = "radioButtonSiCR";
            this.radioButtonSiCR.Size = new System.Drawing.Size(34, 17);
            this.radioButtonSiCR.TabIndex = 0;
            this.radioButtonSiCR.Text = "Si";
            this.radioButtonSiCR.UseVisualStyleBackColor = false;
            this.radioButtonSiCR.CheckedChanged += new System.EventHandler(this.radioButtonSiCR_CheckedChanged);
            // 
            // radioButtonNoCR
            // 
            this.radioButtonNoCR.AutoSize = true;
            this.radioButtonNoCR.BackColor = System.Drawing.SystemColors.HighlightText;
            this.radioButtonNoCR.Checked = true;
            this.radioButtonNoCR.Enabled = false;
            this.radioButtonNoCR.Location = new System.Drawing.Point(107, 19);
            this.radioButtonNoCR.Name = "radioButtonNoCR";
            this.radioButtonNoCR.Size = new System.Drawing.Size(39, 17);
            this.radioButtonNoCR.TabIndex = 1;
            this.radioButtonNoCR.TabStop = true;
            this.radioButtonNoCR.Text = "No";
            this.radioButtonNoCR.UseVisualStyleBackColor = false;
            this.radioButtonNoCR.CheckedChanged += new System.EventHandler(this.radioButtonNoCR_CheckedChanged);
            // 
            // dgvEstAne
            // 
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvEstAne.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.dgvEstAne.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvEstAne.DefaultCellStyle = dataGridViewCellStyle5;
            this.dgvEstAne.Location = new System.Drawing.Point(19, 112);
            this.dgvEstAne.Name = "dgvEstAne";
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvEstAne.RowHeadersDefaultCellStyle = dataGridViewCellStyle6;
            this.dgvEstAne.Size = new System.Drawing.Size(628, 111);
            this.dgvEstAne.TabIndex = 70;
            // 
            // btnEditarEstAne
            // 
            this.btnEditarEstAne.Location = new System.Drawing.Point(383, 415);
            this.btnEditarEstAne.Name = "btnEditarEstAne";
            this.btnEditarEstAne.Size = new System.Drawing.Size(75, 27);
            this.btnEditarEstAne.TabIndex = 74;
            this.btnEditarEstAne.Text = "Modificar";
            this.btnEditarEstAne.UseVisualStyleBackColor = true;
            this.btnEditarEstAne.Click += new System.EventHandler(this.btnEditarEstAne_Click);
            // 
            // btnGrabarEstAne
            // 
            this.btnGrabarEstAne.Location = new System.Drawing.Point(464, 415);
            this.btnGrabarEstAne.Name = "btnGrabarEstAne";
            this.btnGrabarEstAne.Size = new System.Drawing.Size(75, 27);
            this.btnGrabarEstAne.TabIndex = 73;
            this.btnGrabarEstAne.Text = "Grabar";
            this.btnGrabarEstAne.UseVisualStyleBackColor = true;
            this.btnGrabarEstAne.Click += new System.EventHandler(this.btnGrabarEstAne_Click);
            // 
            // btnEliminarEstAne
            // 
            this.btnEliminarEstAne.Location = new System.Drawing.Point(549, 415);
            this.btnEliminarEstAne.Name = "btnEliminarEstAne";
            this.btnEliminarEstAne.Size = new System.Drawing.Size(75, 27);
            this.btnEliminarEstAne.TabIndex = 72;
            this.btnEliminarEstAne.Text = "Eliminar";
            this.btnEliminarEstAne.UseVisualStyleBackColor = true;
            this.btnEliminarEstAne.Click += new System.EventHandler(this.btnEliminarEstAne_Click);
            // 
            // btnNuevoEstAne
            // 
            this.btnNuevoEstAne.Location = new System.Drawing.Point(298, 415);
            this.btnNuevoEstAne.Name = "btnNuevoEstAne";
            this.btnNuevoEstAne.Size = new System.Drawing.Size(75, 27);
            this.btnNuevoEstAne.TabIndex = 71;
            this.btnNuevoEstAne.Text = "Nuevo";
            this.btnNuevoEstAne.UseVisualStyleBackColor = true;
            this.btnNuevoEstAne.Click += new System.EventHandler(this.btnNuevoEstAne_Click);
            // 
            // toolStripForm
            // 
            this.toolStripForm.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.toolStripForm.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.toolStripForm.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnSalir});
            this.toolStripForm.Location = new System.Drawing.Point(0, 457);
            this.toolStripForm.Name = "toolStripForm";
            this.toolStripForm.ShowItemToolTips = false;
            this.toolStripForm.Size = new System.Drawing.Size(667, 25);
            this.toolStripForm.TabIndex = 75;
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
            // btnTasaEsta
            // 
            this.btnTasaEsta.Location = new System.Drawing.Point(484, 229);
            this.btnTasaEsta.Name = "btnTasaEsta";
            this.btnTasaEsta.Size = new System.Drawing.Size(164, 23);
            this.btnTasaEsta.TabIndex = 76;
            this.btnTasaEsta.Text = "% Tasa SCTR - ESSALUD";
            this.btnTasaEsta.UseVisualStyleBackColor = true;
            this.btnTasaEsta.Click += new System.EventHandler(this.btnTasaEsta_Click);
            // 
            // ui_estaneemp
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.HighlightText;
            this.ClientSize = new System.Drawing.Size(667, 482);
            this.ControlBox = false;
            this.Controls.Add(this.btnTasaEsta);
            this.Controls.Add(this.toolStripForm);
            this.Controls.Add(this.btnEditarEstAne);
            this.Controls.Add(this.btnGrabarEstAne);
            this.Controls.Add(this.btnEliminarEstAne);
            this.Controls.Add(this.btnNuevoEstAne);
            this.Controls.Add(this.dgvEstAne);
            this.Controls.Add(this.groupBox8);
            this.Controls.Add(this.groupBox6);
            this.Controls.Add(this.groupBox5);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ui_estaneemp";
            this.Text = "Empleador a quienes destaco o desplazo personal";
            this.Load += new System.EventHandler(this.ui_estaneemp_Load);
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.groupBox8.ResumeLayout(false);
            this.groupBox8.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvEstAne)).EndInit();
            this.toolStripForm.ResumeLayout(false);
            this.toolStripForm.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.MaskedTextBox txtRucEmp;
        private System.Windows.Forms.TextBox txtRazonEmp;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.MaskedTextBox txtCodigoEstEmp;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtNombreEstEmp;
        private System.Windows.Forms.TextBox txtOpeEstEmp;
        private System.Windows.Forms.ComboBox cmbTipoEstEmp;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.GroupBox groupBox8;
        private System.Windows.Forms.RadioButton radioButtonSiCR;
        private System.Windows.Forms.RadioButton radioButtonNoCR;
        private System.Windows.Forms.TextBox txtIdCiaFile;
        private System.Windows.Forms.DataGridView dgvEstAne;
        private System.Windows.Forms.Button btnEditarEstAne;
        private System.Windows.Forms.Button btnGrabarEstAne;
        private System.Windows.Forms.Button btnEliminarEstAne;
        private System.Windows.Forms.Button btnNuevoEstAne;
        private System.Windows.Forms.ToolStrip toolStripForm;
        private System.Windows.Forms.ToolStripButton btnSalir;
        private System.Windows.Forms.Button btnTasaEsta;
    }
}