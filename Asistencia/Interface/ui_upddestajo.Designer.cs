namespace CaniaBrava
{
    partial class ui_upddestajo
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
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txtSubTotal = new System.Windows.Forms.TextBox();
            this.txtPrecio = new System.Windows.Forms.TextBox();
            this.txtCantidad = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.toolStripForm = new System.Windows.Forms.ToolStrip();
            this.btnAceptar = new System.Windows.Forms.ToolStripButton();
            this.btnCancelar = new System.Windows.Forms.ToolStripButton();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.pictureBoxBuscar = new System.Windows.Forms.PictureBox();
            this.lblF2 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.txtNombres = new System.Windows.Forms.TextBox();
            this.txtNroDocIden = new System.Windows.Forms.TextBox();
            this.txtDocIdent = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtCodigoInterno = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtAdicional = new System.Windows.Forms.TextBox();
            this.txtRefrigerio = new System.Windows.Forms.TextBox();
            this.txtMovilidad = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.txtTotal = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.lblTipoPlanilla = new System.Windows.Forms.Label();
            this.cmbVariedad = new System.Windows.Forms.ComboBox();
            this.groupBox2.SuspendLayout();
            this.toolStripForm.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxBuscar)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.lblTipoPlanilla);
            this.groupBox2.Controls.Add(this.cmbVariedad);
            this.groupBox2.Controls.Add(this.txtSubTotal);
            this.groupBox2.Controls.Add(this.txtPrecio);
            this.groupBox2.Controls.Add(this.txtCantidad);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Location = new System.Drawing.Point(9, 114);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(565, 127);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Producción";
            // 
            // txtSubTotal
            // 
            this.txtSubTotal.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtSubTotal.Enabled = false;
            this.txtSubTotal.Location = new System.Drawing.Point(169, 95);
            this.txtSubTotal.MaxLength = 80;
            this.txtSubTotal.Name = "txtSubTotal";
            this.txtSubTotal.Size = new System.Drawing.Size(140, 20);
            this.txtSubTotal.TabIndex = 2;
            this.txtSubTotal.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtPrecio
            // 
            this.txtPrecio.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtPrecio.Location = new System.Drawing.Point(169, 71);
            this.txtPrecio.MaxLength = 80;
            this.txtPrecio.Name = "txtPrecio";
            this.txtPrecio.Size = new System.Drawing.Size(140, 20);
            this.txtPrecio.TabIndex = 1;
            this.txtPrecio.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtPrecio.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtPrecio_KeyPress);
            // 
            // txtCantidad
            // 
            this.txtCantidad.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtCantidad.Location = new System.Drawing.Point(169, 47);
            this.txtCantidad.MaxLength = 80;
            this.txtCantidad.Name = "txtCantidad";
            this.txtCantidad.Size = new System.Drawing.Size(140, 20);
            this.txtCantidad.TabIndex = 0;
            this.txtCantidad.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtCantidad.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCantidad_KeyPress);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(34, 99);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(75, 13);
            this.label3.TabIndex = 54;
            this.label3.Text = "Importe Total :";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(34, 75);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(43, 13);
            this.label2.TabIndex = 52;
            this.label2.Text = "Precio :";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(34, 51);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(58, 13);
            this.label1.TabIndex = 50;
            this.label1.Text = "Cantidad  :";
            // 
            // toolStripForm
            // 
            this.toolStripForm.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.toolStripForm.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.toolStripForm.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnAceptar,
            this.btnCancelar});
            this.toolStripForm.Location = new System.Drawing.Point(0, 439);
            this.toolStripForm.Name = "toolStripForm";
            this.toolStripForm.ShowItemToolTips = false;
            this.toolStripForm.Size = new System.Drawing.Size(582, 25);
            this.toolStripForm.TabIndex = 4;
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
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.pictureBoxBuscar);
            this.groupBox3.Controls.Add(this.lblF2);
            this.groupBox3.Controls.Add(this.label5);
            this.groupBox3.Controls.Add(this.label6);
            this.groupBox3.Controls.Add(this.txtNombres);
            this.groupBox3.Controls.Add(this.txtNroDocIden);
            this.groupBox3.Controls.Add(this.txtDocIdent);
            this.groupBox3.Controls.Add(this.label7);
            this.groupBox3.Controls.Add(this.txtCodigoInterno);
            this.groupBox3.Location = new System.Drawing.Point(8, 6);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(566, 102);
            this.groupBox3.TabIndex = 0;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Trabajador";
            // 
            // pictureBoxBuscar
            // 
            this.pictureBoxBuscar.Image = global::CaniaBrava.Properties.Resources.LOCATE;
            this.pictureBoxBuscar.Location = new System.Drawing.Point(338, 22);
            this.pictureBoxBuscar.Name = "pictureBoxBuscar";
            this.pictureBoxBuscar.Size = new System.Drawing.Size(24, 21);
            this.pictureBoxBuscar.TabIndex = 55;
            this.pictureBoxBuscar.TabStop = false;
            // 
            // lblF2
            // 
            this.lblF2.AutoSize = true;
            this.lblF2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblF2.Location = new System.Drawing.Point(316, 26);
            this.lblF2.Name = "lblF2";
            this.lblF2.Size = new System.Drawing.Size(21, 13);
            this.lblF2.TabIndex = 54;
            this.lblF2.Text = "F2";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(34, 75);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(130, 13);
            this.label5.TabIndex = 6;
            this.label5.Text = "Documento de Identidad :";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(34, 51);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(108, 13);
            this.label6.TabIndex = 5;
            this.label6.Text = "Apellidos y Nombres :";
            // 
            // txtNombres
            // 
            this.txtNombres.BackColor = System.Drawing.SystemColors.HighlightText;
            this.txtNombres.Enabled = false;
            this.txtNombres.Location = new System.Drawing.Point(169, 47);
            this.txtNombres.Name = "txtNombres";
            this.txtNombres.Size = new System.Drawing.Size(374, 20);
            this.txtNombres.TabIndex = 1;
            // 
            // txtNroDocIden
            // 
            this.txtNroDocIden.BackColor = System.Drawing.SystemColors.HighlightText;
            this.txtNroDocIden.Enabled = false;
            this.txtNroDocIden.Location = new System.Drawing.Point(316, 71);
            this.txtNroDocIden.Name = "txtNroDocIden";
            this.txtNroDocIden.Size = new System.Drawing.Size(227, 20);
            this.txtNroDocIden.TabIndex = 3;
            // 
            // txtDocIdent
            // 
            this.txtDocIdent.BackColor = System.Drawing.SystemColors.HighlightText;
            this.txtDocIdent.Enabled = false;
            this.txtDocIdent.Location = new System.Drawing.Point(169, 72);
            this.txtDocIdent.Name = "txtDocIdent";
            this.txtDocIdent.Size = new System.Drawing.Size(141, 20);
            this.txtDocIdent.TabIndex = 2;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(34, 26);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(82, 13);
            this.label7.TabIndex = 1;
            this.label7.Text = "Código Interno :";
            // 
            // txtCodigoInterno
            // 
            this.txtCodigoInterno.BackColor = System.Drawing.SystemColors.HighlightText;
            this.txtCodigoInterno.Location = new System.Drawing.Point(169, 22);
            this.txtCodigoInterno.MaxLength = 5;
            this.txtCodigoInterno.Name = "txtCodigoInterno";
            this.txtCodigoInterno.ReadOnly = true;
            this.txtCodigoInterno.Size = new System.Drawing.Size(141, 20);
            this.txtCodigoInterno.TabIndex = 0;
            this.txtCodigoInterno.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtCodigoInterno_KeyDown);
            this.txtCodigoInterno.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCodigoInterno_KeyPress);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtAdicional);
            this.groupBox1.Controls.Add(this.txtRefrigerio);
            this.groupBox1.Controls.Add(this.txtMovilidad);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Location = new System.Drawing.Point(8, 259);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(565, 107);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Otros Conceptos";
            // 
            // txtAdicional
            // 
            this.txtAdicional.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtAdicional.Location = new System.Drawing.Point(169, 67);
            this.txtAdicional.MaxLength = 80;
            this.txtAdicional.Name = "txtAdicional";
            this.txtAdicional.Size = new System.Drawing.Size(140, 20);
            this.txtAdicional.TabIndex = 2;
            this.txtAdicional.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtAdicional.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtAdicional_KeyPress);
            // 
            // txtRefrigerio
            // 
            this.txtRefrigerio.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtRefrigerio.Location = new System.Drawing.Point(169, 43);
            this.txtRefrigerio.MaxLength = 80;
            this.txtRefrigerio.Name = "txtRefrigerio";
            this.txtRefrigerio.Size = new System.Drawing.Size(140, 20);
            this.txtRefrigerio.TabIndex = 1;
            this.txtRefrigerio.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtRefrigerio.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtRefrigerio_KeyPress);
            // 
            // txtMovilidad
            // 
            this.txtMovilidad.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtMovilidad.Location = new System.Drawing.Point(169, 19);
            this.txtMovilidad.MaxLength = 80;
            this.txtMovilidad.Name = "txtMovilidad";
            this.txtMovilidad.Size = new System.Drawing.Size(140, 20);
            this.txtMovilidad.TabIndex = 0;
            this.txtMovilidad.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtMovilidad.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtMovilidad_KeyPress);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(34, 71);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(56, 13);
            this.label4.TabIndex = 54;
            this.label4.Text = "Adicional :";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(34, 47);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(58, 13);
            this.label8.TabIndex = 52;
            this.label8.Text = "Refrigerio :";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(34, 23);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(58, 13);
            this.label9.TabIndex = 50;
            this.label9.Text = "Movilidad :";
            // 
            // txtTotal
            // 
            this.txtTotal.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtTotal.Enabled = false;
            this.txtTotal.Location = new System.Drawing.Point(169, 18);
            this.txtTotal.MaxLength = 80;
            this.txtTotal.Name = "txtTotal";
            this.txtTotal.Size = new System.Drawing.Size(140, 20);
            this.txtTotal.TabIndex = 0;
            this.txtTotal.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(34, 22);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(75, 13);
            this.label10.TabIndex = 56;
            this.label10.Text = "Importe Total :";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.txtTotal);
            this.groupBox4.Controls.Add(this.label10);
            this.groupBox4.Location = new System.Drawing.Point(8, 373);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(565, 56);
            this.groupBox4.TabIndex = 3;
            this.groupBox4.TabStop = false;
            // 
            // lblTipoPlanilla
            // 
            this.lblTipoPlanilla.AutoSize = true;
            this.lblTipoPlanilla.Location = new System.Drawing.Point(33, 22);
            this.lblTipoPlanilla.Name = "lblTipoPlanilla";
            this.lblTipoPlanilla.Size = new System.Drawing.Size(55, 13);
            this.lblTipoPlanilla.TabIndex = 82;
            this.lblTipoPlanilla.Text = "Variedad :";
            // 
            // cmbVariedad
            // 
            this.cmbVariedad.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbVariedad.FormattingEnabled = true;
            this.cmbVariedad.Location = new System.Drawing.Point(169, 19);
            this.cmbVariedad.Name = "cmbVariedad";
            this.cmbVariedad.Size = new System.Drawing.Size(373, 21);
            this.cmbVariedad.TabIndex = 81;
            this.cmbVariedad.SelectedIndexChanged += new System.EventHandler(this.cmbVariedad_SelectedIndexChanged);
            this.cmbVariedad.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cmbVariedad_KeyPress);
            // 
            // ui_upddestajo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.HighlightText;
            this.ClientSize = new System.Drawing.Size(582, 464);
            this.ControlBox = false;
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.toolStripForm);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ui_upddestajo";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Parte Diario Destajo";
            this.Load += new System.EventHandler(this.ui_upddestajo_Load);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.toolStripForm.ResumeLayout(false);
            this.toolStripForm.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxBuscar)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox txtSubTotal;
        private System.Windows.Forms.TextBox txtPrecio;
        private System.Windows.Forms.TextBox txtCantidad;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ToolStrip toolStripForm;
        private System.Windows.Forms.ToolStripButton btnAceptar;
        private System.Windows.Forms.ToolStripButton btnCancelar;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.PictureBox pictureBoxBuscar;
        private System.Windows.Forms.Label lblF2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        public System.Windows.Forms.TextBox txtNombres;
        public System.Windows.Forms.TextBox txtNroDocIden;
        public System.Windows.Forms.TextBox txtDocIdent;
        private System.Windows.Forms.Label label7;
        public System.Windows.Forms.TextBox txtCodigoInterno;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtAdicional;
        private System.Windows.Forms.TextBox txtRefrigerio;
        private System.Windows.Forms.TextBox txtMovilidad;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtTotal;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Label lblTipoPlanilla;
        private System.Windows.Forms.ComboBox cmbVariedad;
    }
}