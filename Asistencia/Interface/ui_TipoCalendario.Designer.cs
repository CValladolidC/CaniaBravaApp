namespace CaniaBrava
{
    partial class ui_TipoCalendario
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
            this.toolStripForm = new System.Windows.Forms.ToolStrip();
            this.btnSalir = new System.Windows.Forms.ToolStripButton();
            this.btnGrabarEstAne = new System.Windows.Forms.Button();
            this.btnEliminarEstAne = new System.Windows.Forms.Button();
            this.dgvCal = new System.Windows.Forms.DataGridView();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.txtCodTipoPlan = new System.Windows.Forms.MaskedTextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.txtDesTipoPlan = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.cmbTipoCal = new System.Windows.Forms.ComboBox();
            this.toolStripForm.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCal)).BeginInit();
            this.groupBox5.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStripForm
            // 
            this.toolStripForm.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.toolStripForm.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.toolStripForm.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnSalir});
            this.toolStripForm.Location = new System.Drawing.Point(0, 275);
            this.toolStripForm.Name = "toolStripForm";
            this.toolStripForm.ShowItemToolTips = false;
            this.toolStripForm.Size = new System.Drawing.Size(471, 25);
            this.toolStripForm.TabIndex = 83;
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
            // btnGrabarEstAne
            // 
            this.btnGrabarEstAne.Location = new System.Drawing.Point(302, 214);
            this.btnGrabarEstAne.Name = "btnGrabarEstAne";
            this.btnGrabarEstAne.Size = new System.Drawing.Size(75, 27);
            this.btnGrabarEstAne.TabIndex = 81;
            this.btnGrabarEstAne.Text = "Grabar";
            this.btnGrabarEstAne.UseVisualStyleBackColor = true;
            this.btnGrabarEstAne.Click += new System.EventHandler(this.btnGrabarEstAne_Click);
            // 
            // btnEliminarEstAne
            // 
            this.btnEliminarEstAne.Location = new System.Drawing.Point(383, 214);
            this.btnEliminarEstAne.Name = "btnEliminarEstAne";
            this.btnEliminarEstAne.Size = new System.Drawing.Size(75, 27);
            this.btnEliminarEstAne.TabIndex = 82;
            this.btnEliminarEstAne.Text = "Eliminar";
            this.btnEliminarEstAne.UseVisualStyleBackColor = true;
            this.btnEliminarEstAne.Click += new System.EventHandler(this.btnEliminarEstAne_Click);
            // 
            // dgvCal
            // 
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvCal.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvCal.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvCal.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvCal.Location = new System.Drawing.Point(12, 97);
            this.dgvCal.Name = "dgvCal";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvCal.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dgvCal.Size = new System.Drawing.Size(446, 111);
            this.dgvCal.TabIndex = 86;
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.txtCodTipoPlan);
            this.groupBox5.Controls.Add(this.label7);
            this.groupBox5.Controls.Add(this.label8);
            this.groupBox5.Controls.Add(this.txtDesTipoPlan);
            this.groupBox5.Location = new System.Drawing.Point(12, 12);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(446, 79);
            this.groupBox5.TabIndex = 84;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Tipo de Planilla";
            // 
            // txtCodTipoPlan
            // 
            this.txtCodTipoPlan.BackColor = System.Drawing.SystemColors.HighlightText;
            this.txtCodTipoPlan.Enabled = false;
            this.txtCodTipoPlan.Location = new System.Drawing.Point(143, 22);
            this.txtCodTipoPlan.Mask = "9999";
            this.txtCodTipoPlan.Name = "txtCodTipoPlan";
            this.txtCodTipoPlan.PromptChar = ' ';
            this.txtCodTipoPlan.Size = new System.Drawing.Size(77, 20);
            this.txtCodTipoPlan.TabIndex = 1;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(12, 52);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(69, 13);
            this.label7.TabIndex = 41;
            this.label7.Text = "Descripción :";
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
            // txtDesTipoPlan
            // 
            this.txtDesTipoPlan.BackColor = System.Drawing.SystemColors.HighlightText;
            this.txtDesTipoPlan.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtDesTipoPlan.Enabled = false;
            this.txtDesTipoPlan.Location = new System.Drawing.Point(143, 48);
            this.txtDesTipoPlan.MaxLength = 50;
            this.txtDesTipoPlan.Name = "txtDesTipoPlan";
            this.txtDesTipoPlan.Size = new System.Drawing.Size(297, 20);
            this.txtDesTipoPlan.TabIndex = 2;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(13, 221);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(87, 13);
            this.label5.TabIndex = 94;
            this.label5.Text = "Tipo Calendario :";
            // 
            // cmbTipoCal
            // 
            this.cmbTipoCal.BackColor = System.Drawing.SystemColors.HighlightText;
            this.cmbTipoCal.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTipoCal.FormattingEnabled = true;
            this.cmbTipoCal.Location = new System.Drawing.Point(109, 217);
            this.cmbTipoCal.Name = "cmbTipoCal";
            this.cmbTipoCal.Size = new System.Drawing.Size(187, 21);
            this.cmbTipoCal.TabIndex = 93;
            // 
            // ui_TipoCalendario
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.HighlightText;
            this.ClientSize = new System.Drawing.Size(471, 300);
            this.ControlBox = false;
            this.Controls.Add(this.label5);
            this.Controls.Add(this.cmbTipoCal);
            this.Controls.Add(this.toolStripForm);
            this.Controls.Add(this.btnGrabarEstAne);
            this.Controls.Add(this.btnEliminarEstAne);
            this.Controls.Add(this.dgvCal);
            this.Controls.Add(this.groupBox5);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ui_TipoCalendario";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "  ";
            this.Load += new System.EventHandler(this.ui_TipoCalendario_Load);
            this.toolStripForm.ResumeLayout(false);
            this.toolStripForm.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCal)).EndInit();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStripForm;
        private System.Windows.Forms.ToolStripButton btnSalir;
        private System.Windows.Forms.Button btnGrabarEstAne;
        private System.Windows.Forms.Button btnEliminarEstAne;
        private System.Windows.Forms.DataGridView dgvCal;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.MaskedTextBox txtCodTipoPlan;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtDesTipoPlan;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cmbTipoCal;
    }
}