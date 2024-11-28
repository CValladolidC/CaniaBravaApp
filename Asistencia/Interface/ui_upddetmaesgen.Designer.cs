namespace CaniaBrava
{
    partial class ui_upddetmaesgen
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
            this.txtCodigo = new System.Windows.Forms.TextBox();
            this.txtOperacion = new System.Windows.Forms.TextBox();
            this.lblOperacion = new System.Windows.Forms.Label();
            this.cmbEstado = new System.Windows.Forms.ComboBox();
            this.lblEstado = new System.Windows.Forms.Label();
            this.txtDescripcion = new System.Windows.Forms.TextBox();
            this.lblDescripcion = new System.Windows.Forms.Label();
            this.lblUsuario = new System.Windows.Forms.Label();
            this.toolStripForm = new System.Windows.Forms.ToolStrip();
            this.btnAceptar = new System.Windows.Forms.ToolStripButton();
            this.btnCancelar = new System.Windows.Forms.ToolStripButton();
            this.txtMaestro = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtParametro1 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtParametro2 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtParametro3 = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cmbTipo = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txtAbrevia = new System.Windows.Forms.TextBox();
            this.lblAbrevia = new System.Windows.Forms.Label();
            this.lblTR = new System.Windows.Forms.Label();
            this.toolStripForm.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtCodigo
            // 
            this.txtCodigo.BackColor = System.Drawing.SystemColors.HighlightText;
            this.txtCodigo.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtCodigo.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCodigo.Location = new System.Drawing.Point(136, 14);
            this.txtCodigo.MaxLength = 4;
            this.txtCodigo.Name = "txtCodigo";
            this.txtCodigo.Size = new System.Drawing.Size(171, 18);
            this.txtCodigo.TabIndex = 1;
            this.txtCodigo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCodigo_KeyPress);
            // 
            // txtOperacion
            // 
            this.txtOperacion.BackColor = System.Drawing.SystemColors.HighlightText;
            this.txtOperacion.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtOperacion.Enabled = false;
            this.txtOperacion.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtOperacion.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtOperacion.Location = new System.Drawing.Point(136, 20);
            this.txtOperacion.MaxLength = 6;
            this.txtOperacion.Name = "txtOperacion";
            this.txtOperacion.ReadOnly = true;
            this.txtOperacion.Size = new System.Drawing.Size(171, 18);
            this.txtOperacion.TabIndex = 64;
            // 
            // lblOperacion
            // 
            this.lblOperacion.AutoSize = true;
            this.lblOperacion.Location = new System.Drawing.Point(12, 24);
            this.lblOperacion.Name = "lblOperacion";
            this.lblOperacion.Size = new System.Drawing.Size(59, 13);
            this.lblOperacion.TabIndex = 66;
            this.lblOperacion.Text = "Operación:";
            // 
            // cmbEstado
            // 
            this.cmbEstado.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbEstado.FormattingEnabled = true;
            this.cmbEstado.Items.AddRange(new object[] {
            "A        ANULADO  ",
            "V         VIGENTE"});
            this.cmbEstado.Location = new System.Drawing.Point(136, 199);
            this.cmbEstado.Name = "cmbEstado";
            this.cmbEstado.Size = new System.Drawing.Size(171, 21);
            this.cmbEstado.Sorted = true;
            this.cmbEstado.TabIndex = 7;
            this.cmbEstado.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cmbEstado_KeyPress);
            // 
            // lblEstado
            // 
            this.lblEstado.AutoSize = true;
            this.lblEstado.Location = new System.Drawing.Point(8, 203);
            this.lblEstado.Name = "lblEstado";
            this.lblEstado.Size = new System.Drawing.Size(46, 13);
            this.lblEstado.TabIndex = 65;
            this.lblEstado.Text = "Estado :";
            // 
            // txtDescripcion
            // 
            this.txtDescripcion.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtDescripcion.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDescripcion.Location = new System.Drawing.Point(136, 40);
            this.txtDescripcion.MaxLength = 120;
            this.txtDescripcion.Name = "txtDescripcion";
            this.txtDescripcion.Size = new System.Drawing.Size(437, 18);
            this.txtDescripcion.TabIndex = 2;
            this.txtDescripcion.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtDescripcion_KeyPress);
            // 
            // lblDescripcion
            // 
            this.lblDescripcion.AutoSize = true;
            this.lblDescripcion.Location = new System.Drawing.Point(8, 44);
            this.lblDescripcion.Name = "lblDescripcion";
            this.lblDescripcion.Size = new System.Drawing.Size(69, 13);
            this.lblDescripcion.TabIndex = 63;
            this.lblDescripcion.Text = "Descripción :";
            // 
            // lblUsuario
            // 
            this.lblUsuario.AutoSize = true;
            this.lblUsuario.Location = new System.Drawing.Point(8, 17);
            this.lblUsuario.Name = "lblUsuario";
            this.lblUsuario.Size = new System.Drawing.Size(46, 13);
            this.lblUsuario.TabIndex = 62;
            this.lblUsuario.Text = "Código :";
            // 
            // toolStripForm
            // 
            this.toolStripForm.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.toolStripForm.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.toolStripForm.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnAceptar,
            this.btnCancelar});
            this.toolStripForm.Location = new System.Drawing.Point(0, 358);
            this.toolStripForm.Name = "toolStripForm";
            this.toolStripForm.ShowItemToolTips = false;
            this.toolStripForm.Size = new System.Drawing.Size(596, 25);
            this.toolStripForm.TabIndex = 7;
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
            // txtMaestro
            // 
            this.txtMaestro.BackColor = System.Drawing.SystemColors.HighlightText;
            this.txtMaestro.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtMaestro.Enabled = false;
            this.txtMaestro.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMaestro.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtMaestro.Location = new System.Drawing.Point(136, 46);
            this.txtMaestro.MaxLength = 6;
            this.txtMaestro.Name = "txtMaestro";
            this.txtMaestro.ReadOnly = true;
            this.txtMaestro.Size = new System.Drawing.Size(437, 18);
            this.txtMaestro.TabIndex = 70;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 50);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(87, 13);
            this.label4.TabIndex = 71;
            this.label4.Text = "Maestro Común :";
            // 
            // txtParametro1
            // 
            this.txtParametro1.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtParametro1.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtParametro1.Location = new System.Drawing.Point(136, 94);
            this.txtParametro1.MaxLength = 30;
            this.txtParametro1.Name = "txtParametro1";
            this.txtParametro1.Size = new System.Drawing.Size(171, 18);
            this.txtParametro1.TabIndex = 3;
            this.txtParametro1.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtParametro1_KeyPress);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 98);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(70, 13);
            this.label1.TabIndex = 73;
            this.label1.Text = "Parámetro 1 :";
            // 
            // txtParametro2
            // 
            this.txtParametro2.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtParametro2.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtParametro2.Location = new System.Drawing.Point(136, 120);
            this.txtParametro2.MaxLength = 30;
            this.txtParametro2.Name = "txtParametro2";
            this.txtParametro2.Size = new System.Drawing.Size(171, 18);
            this.txtParametro2.TabIndex = 4;
            this.txtParametro2.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtParametro2_KeyPress);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 124);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(70, 13);
            this.label2.TabIndex = 75;
            this.label2.Text = "Parámetro 2 :";
            // 
            // txtParametro3
            // 
            this.txtParametro3.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtParametro3.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtParametro3.Location = new System.Drawing.Point(136, 146);
            this.txtParametro3.MaxLength = 30;
            this.txtParametro3.Name = "txtParametro3";
            this.txtParametro3.Size = new System.Drawing.Size(171, 18);
            this.txtParametro3.TabIndex = 5;
            this.txtParametro3.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtParametro3_KeyPress);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(8, 150);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(70, 13);
            this.label3.TabIndex = 77;
            this.label3.Text = "Parámetro 3 :";
            // 
            // cmbTipo
            // 
            this.cmbTipo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTipo.FormattingEnabled = true;
            this.cmbTipo.Items.AddRange(new object[] {
            "S         SISTEMA",
            "U        USUARIO"});
            this.cmbTipo.Location = new System.Drawing.Point(136, 172);
            this.cmbTipo.Name = "cmbTipo";
            this.cmbTipo.Size = new System.Drawing.Size(171, 21);
            this.cmbTipo.Sorted = true;
            this.cmbTipo.TabIndex = 6;
            this.cmbTipo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cmbTipo_KeyPress);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(8, 176);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(34, 13);
            this.label5.TabIndex = 79;
            this.label5.Text = "Tipo :";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtMaestro);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.txtOperacion);
            this.groupBox1.Controls.Add(this.lblOperacion);
            this.groupBox1.Location = new System.Drawing.Point(8, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(582, 87);
            this.groupBox1.TabIndex = 80;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Maestro Común";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.lblTR);
            this.groupBox2.Controls.Add(this.txtAbrevia);
            this.groupBox2.Controls.Add(this.lblAbrevia);
            this.groupBox2.Controls.Add(this.cmbTipo);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.txtParametro3);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.txtParametro2);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.txtParametro1);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.txtCodigo);
            this.groupBox2.Controls.Add(this.cmbEstado);
            this.groupBox2.Controls.Add(this.lblEstado);
            this.groupBox2.Controls.Add(this.txtDescripcion);
            this.groupBox2.Controls.Add(this.lblDescripcion);
            this.groupBox2.Controls.Add(this.lblUsuario);
            this.groupBox2.Location = new System.Drawing.Point(8, 105);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(582, 246);
            this.groupBox2.TabIndex = 81;
            this.groupBox2.TabStop = false;
            // 
            // txtAbrevia
            // 
            this.txtAbrevia.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtAbrevia.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAbrevia.Location = new System.Drawing.Point(136, 66);
            this.txtAbrevia.MaxLength = 50;
            this.txtAbrevia.Name = "txtAbrevia";
            this.txtAbrevia.Size = new System.Drawing.Size(437, 18);
            this.txtAbrevia.TabIndex = 80;
            this.txtAbrevia.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtAbrevia_KeyPress);
            // 
            // lblAbrevia
            // 
            this.lblAbrevia.AutoSize = true;
            this.lblAbrevia.Location = new System.Drawing.Point(8, 70);
            this.lblAbrevia.Name = "lblAbrevia";
            this.lblAbrevia.Size = new System.Drawing.Size(120, 13);
            this.lblAbrevia.TabIndex = 81;
            this.lblAbrevia.Text = "Descripción Abreviada :";
            // 
            // lblTR
            // 
            this.lblTR.AutoSize = true;
            this.lblTR.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTR.ForeColor = System.Drawing.Color.Red;
            this.lblTR.Location = new System.Drawing.Point(313, 148);
            this.lblTR.Name = "lblTR";
            this.lblTR.Size = new System.Drawing.Size(0, 12);
            this.lblTR.TabIndex = 82;
            // 
            // ui_upddetmaesgen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.HighlightText;
            this.ClientSize = new System.Drawing.Size(596, 383);
            this.ControlBox = false;
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.toolStripForm);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(612, 422);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(612, 422);
            this.Name = "ui_upddetmaesgen";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Actualización de Datos";
            this.Load += new System.EventHandler(this.ui_upddetmaesgen_Load);
            this.toolStripForm.ResumeLayout(false);
            this.toolStripForm.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtCodigo;
        private System.Windows.Forms.TextBox txtOperacion;
        private System.Windows.Forms.Label lblOperacion;
        private System.Windows.Forms.ComboBox cmbEstado;
        private System.Windows.Forms.Label lblEstado;
        private System.Windows.Forms.TextBox txtDescripcion;
        private System.Windows.Forms.Label lblDescripcion;
        private System.Windows.Forms.Label lblUsuario;
        private System.Windows.Forms.ToolStrip toolStripForm;
        private System.Windows.Forms.ToolStripButton btnAceptar;
        private System.Windows.Forms.ToolStripButton btnCancelar;
        private System.Windows.Forms.TextBox txtMaestro;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtParametro1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtParametro2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtParametro3;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cmbTipo;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox txtAbrevia;
        private System.Windows.Forms.Label lblAbrevia;
        private System.Windows.Forms.Label lblTR;
    }
}