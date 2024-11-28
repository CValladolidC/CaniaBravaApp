namespace CaniaBrava
{
    partial class ui_UpdConPdt
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
            this.chkCero = new System.Windows.Forms.CheckBox();
            this.chkPagado = new System.Windows.Forms.CheckBox();
            this.chkDevengado = new System.Windows.Forms.CheckBox();
            this.txtCodigo = new System.Windows.Forms.TextBox();
            this.txtDescripcion = new System.Windows.Forms.TextBox();
            this.lblRazon = new System.Windows.Forms.Label();
            this.lblUsuario = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtOperacion = new System.Windows.Forms.TextBox();
            this.lblOperacion = new System.Windows.Forms.Label();
            this.toolStripForm = new System.Windows.Forms.ToolStrip();
            this.btnAceptar = new System.Windows.Forms.ToolStripButton();
            this.btnCancelar = new System.Windows.Forms.ToolStripButton();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.chkDevengadoPlame = new System.Windows.Forms.CheckBox();
            this.chkCeroPlame = new System.Windows.Forms.CheckBox();
            this.chkPagadoPlame = new System.Windows.Forms.CheckBox();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.toolStripForm.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.txtCodigo);
            this.groupBox2.Controls.Add(this.txtDescripcion);
            this.groupBox2.Controls.Add(this.lblRazon);
            this.groupBox2.Controls.Add(this.lblUsuario);
            this.groupBox2.Location = new System.Drawing.Point(9, 69);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(558, 77);
            this.groupBox2.TabIndex = 71;
            this.groupBox2.TabStop = false;
            // 
            // chkCero
            // 
            this.chkCero.AutoSize = true;
            this.chkCero.Location = new System.Drawing.Point(134, 65);
            this.chkCero.Name = "chkCero";
            this.chkCero.Size = new System.Drawing.Size(244, 17);
            this.chkCero.TabIndex = 66;
            this.chkCero.Text = "Registrar motivo aún cuando el valor sea Cero";
            this.chkCero.UseVisualStyleBackColor = true;
            // 
            // chkPagado
            // 
            this.chkPagado.AutoSize = true;
            this.chkPagado.Location = new System.Drawing.Point(134, 42);
            this.chkPagado.Name = "chkPagado";
            this.chkPagado.Size = new System.Drawing.Size(250, 17);
            this.chkPagado.TabIndex = 65;
            this.chkPagado.Text = "Se considera el Monto Pagado / Descontado ?";
            this.chkPagado.UseVisualStyleBackColor = true;
            // 
            // chkDevengado
            // 
            this.chkDevengado.AutoSize = true;
            this.chkDevengado.Location = new System.Drawing.Point(134, 19);
            this.chkDevengado.Name = "chkDevengado";
            this.chkDevengado.Size = new System.Drawing.Size(197, 17);
            this.chkDevengado.TabIndex = 64;
            this.chkDevengado.Text = "Se considera el Monto Devengado?";
            this.chkDevengado.UseVisualStyleBackColor = true;
            // 
            // txtCodigo
            // 
            this.txtCodigo.BackColor = System.Drawing.SystemColors.HighlightText;
            this.txtCodigo.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtCodigo.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtCodigo.Location = new System.Drawing.Point(133, 17);
            this.txtCodigo.MaxLength = 4;
            this.txtCodigo.Name = "txtCodigo";
            this.txtCodigo.Size = new System.Drawing.Size(171, 20);
            this.txtCodigo.TabIndex = 1;
            this.txtCodigo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCodigo_KeyPress);
            // 
            // txtDescripcion
            // 
            this.txtDescripcion.BackColor = System.Drawing.SystemColors.HighlightText;
            this.txtDescripcion.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtDescripcion.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtDescripcion.Location = new System.Drawing.Point(133, 42);
            this.txtDescripcion.MaxLength = 80;
            this.txtDescripcion.Name = "txtDescripcion";
            this.txtDescripcion.Size = new System.Drawing.Size(396, 20);
            this.txtDescripcion.TabIndex = 2;
            // 
            // lblRazon
            // 
            this.lblRazon.AutoSize = true;
            this.lblRazon.Location = new System.Drawing.Point(17, 46);
            this.lblRazon.Name = "lblRazon";
            this.lblRazon.Size = new System.Drawing.Size(69, 13);
            this.lblRazon.TabIndex = 63;
            this.lblRazon.Text = "Descripción :";
            // 
            // lblUsuario
            // 
            this.lblUsuario.AutoSize = true;
            this.lblUsuario.Location = new System.Drawing.Point(17, 20);
            this.lblUsuario.Name = "lblUsuario";
            this.lblUsuario.Size = new System.Drawing.Size(46, 13);
            this.lblUsuario.TabIndex = 62;
            this.lblUsuario.Text = "Código :";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtOperacion);
            this.groupBox1.Controls.Add(this.lblOperacion);
            this.groupBox1.Location = new System.Drawing.Point(9, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(560, 60);
            this.groupBox1.TabIndex = 70;
            this.groupBox1.TabStop = false;
            // 
            // txtOperacion
            // 
            this.txtOperacion.BackColor = System.Drawing.SystemColors.HighlightText;
            this.txtOperacion.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtOperacion.Enabled = false;
            this.txtOperacion.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtOperacion.Location = new System.Drawing.Point(134, 20);
            this.txtOperacion.MaxLength = 6;
            this.txtOperacion.Name = "txtOperacion";
            this.txtOperacion.ReadOnly = true;
            this.txtOperacion.Size = new System.Drawing.Size(171, 20);
            this.txtOperacion.TabIndex = 64;
            // 
            // lblOperacion
            // 
            this.lblOperacion.AutoSize = true;
            this.lblOperacion.Location = new System.Drawing.Point(18, 24);
            this.lblOperacion.Name = "lblOperacion";
            this.lblOperacion.Size = new System.Drawing.Size(59, 13);
            this.lblOperacion.TabIndex = 66;
            this.lblOperacion.Text = "Operación:";
            // 
            // toolStripForm
            // 
            this.toolStripForm.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.toolStripForm.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.toolStripForm.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnAceptar,
            this.btnCancelar});
            this.toolStripForm.Location = new System.Drawing.Point(0, 379);
            this.toolStripForm.Name = "toolStripForm";
            this.toolStripForm.ShowItemToolTips = false;
            this.toolStripForm.Size = new System.Drawing.Size(576, 25);
            this.toolStripForm.TabIndex = 69;
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
            this.groupBox3.Controls.Add(this.chkDevengado);
            this.groupBox3.Controls.Add(this.chkCero);
            this.groupBox3.Controls.Add(this.chkPagado);
            this.groupBox3.Location = new System.Drawing.Point(9, 152);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(558, 98);
            this.groupBox3.TabIndex = 72;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "PDT Planilla Electrónica";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.chkDevengadoPlame);
            this.groupBox4.Controls.Add(this.chkCeroPlame);
            this.groupBox4.Controls.Add(this.chkPagadoPlame);
            this.groupBox4.ForeColor = System.Drawing.SystemColors.ControlText;
            this.groupBox4.Location = new System.Drawing.Point(9, 256);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(558, 98);
            this.groupBox4.TabIndex = 73;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "PDT Planilla Electrónica PLAME";
            // 
            // chkDevengadoPlame
            // 
            this.chkDevengadoPlame.AutoSize = true;
            this.chkDevengadoPlame.Location = new System.Drawing.Point(134, 19);
            this.chkDevengadoPlame.Name = "chkDevengadoPlame";
            this.chkDevengadoPlame.Size = new System.Drawing.Size(197, 17);
            this.chkDevengadoPlame.TabIndex = 64;
            this.chkDevengadoPlame.Text = "Se considera el Monto Devengado?";
            this.chkDevengadoPlame.UseVisualStyleBackColor = true;
            // 
            // chkCeroPlame
            // 
            this.chkCeroPlame.AutoSize = true;
            this.chkCeroPlame.Location = new System.Drawing.Point(134, 65);
            this.chkCeroPlame.Name = "chkCeroPlame";
            this.chkCeroPlame.Size = new System.Drawing.Size(244, 17);
            this.chkCeroPlame.TabIndex = 66;
            this.chkCeroPlame.Text = "Registrar motivo aún cuando el valor sea Cero";
            this.chkCeroPlame.UseVisualStyleBackColor = true;
            // 
            // chkPagadoPlame
            // 
            this.chkPagadoPlame.AutoSize = true;
            this.chkPagadoPlame.Location = new System.Drawing.Point(134, 42);
            this.chkPagadoPlame.Name = "chkPagadoPlame";
            this.chkPagadoPlame.Size = new System.Drawing.Size(250, 17);
            this.chkPagadoPlame.TabIndex = 65;
            this.chkPagadoPlame.Text = "Se considera el Monto Pagado / Descontado ?";
            this.chkPagadoPlame.UseVisualStyleBackColor = true;
            // 
            // ui_UpdConPdt
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.HighlightText;
            this.ClientSize = new System.Drawing.Size(576, 404);
            this.ControlBox = false;
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.toolStripForm);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ui_UpdConPdt";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Actualización de Datos";
            this.Load += new System.EventHandler(this.ui_UpdConPdt_Load);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.toolStripForm.ResumeLayout(false);
            this.toolStripForm.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.CheckBox chkPagado;
        private System.Windows.Forms.CheckBox chkDevengado;
        private System.Windows.Forms.TextBox txtCodigo;
        private System.Windows.Forms.TextBox txtDescripcion;
        private System.Windows.Forms.Label lblRazon;
        private System.Windows.Forms.Label lblUsuario;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtOperacion;
        private System.Windows.Forms.Label lblOperacion;
        private System.Windows.Forms.ToolStrip toolStripForm;
        private System.Windows.Forms.ToolStripButton btnAceptar;
        private System.Windows.Forms.ToolStripButton btnCancelar;
        private System.Windows.Forms.CheckBox chkCero;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.CheckBox chkDevengadoPlame;
        private System.Windows.Forms.CheckBox chkCeroPlame;
        private System.Windows.Forms.CheckBox chkPagadoPlame;
    }
}