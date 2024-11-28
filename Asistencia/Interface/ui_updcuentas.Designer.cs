namespace CaniaBrava
{
    partial class ui_updcuentas
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
            this.radioButtonResumen = new System.Windows.Forms.RadioButton();
            this.radioButtonDetallado = new System.Windows.Forms.RadioButton();
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
            this.cmbAnexo = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.txtAneRef = new System.Windows.Forms.TextBox();
            this.txtTipoAneRef = new System.Windows.Forms.TextBox();
            this.label21 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.txtAne = new System.Windows.Forms.TextBox();
            this.txtTipoAne = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cmbAnexoRef = new System.Windows.Forms.ComboBox();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.toolStripForm.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.radioButtonResumen);
            this.groupBox2.Controls.Add(this.radioButtonDetallado);
            this.groupBox2.Controls.Add(this.txtCodigo);
            this.groupBox2.Controls.Add(this.txtDescripcion);
            this.groupBox2.Controls.Add(this.lblRazon);
            this.groupBox2.Controls.Add(this.lblUsuario);
            this.groupBox2.Location = new System.Drawing.Point(9, 57);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(558, 115);
            this.groupBox2.TabIndex = 71;
            this.groupBox2.TabStop = false;
            // 
            // radioButtonResumen
            // 
            this.radioButtonResumen.AutoSize = true;
            this.radioButtonResumen.Checked = true;
            this.radioButtonResumen.Location = new System.Drawing.Point(133, 89);
            this.radioButtonResumen.Name = "radioButtonResumen";
            this.radioButtonResumen.Size = new System.Drawing.Size(130, 17);
            this.radioButtonResumen.TabIndex = 65;
            this.radioButtonResumen.TabStop = true;
            this.radioButtonResumen.Text = "Información Resumida";
            this.radioButtonResumen.UseVisualStyleBackColor = true;
            // 
            // radioButtonDetallado
            // 
            this.radioButtonDetallado.AutoSize = true;
            this.radioButtonDetallado.Location = new System.Drawing.Point(133, 66);
            this.radioButtonDetallado.Name = "radioButtonDetallado";
            this.radioButtonDetallado.Size = new System.Drawing.Size(128, 17);
            this.radioButtonDetallado.TabIndex = 64;
            this.radioButtonDetallado.Text = "Información Detallada";
            this.radioButtonDetallado.UseVisualStyleBackColor = true;
            this.radioButtonDetallado.CheckedChanged += new System.EventHandler(this.radioButtonDetallado_CheckedChanged);
            // 
            // txtCodigo
            // 
            this.txtCodigo.BackColor = System.Drawing.SystemColors.HighlightText;
            this.txtCodigo.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtCodigo.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtCodigo.Location = new System.Drawing.Point(134, 14);
            this.txtCodigo.MaxLength = 10;
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
            this.txtDescripcion.Location = new System.Drawing.Point(134, 40);
            this.txtDescripcion.MaxLength = 80;
            this.txtDescripcion.Name = "txtDescripcion";
            this.txtDescripcion.Size = new System.Drawing.Size(396, 20);
            this.txtDescripcion.TabIndex = 2;
            this.txtDescripcion.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtDescripcion_KeyPress);
            // 
            // lblRazon
            // 
            this.lblRazon.AutoSize = true;
            this.lblRazon.Location = new System.Drawing.Point(20, 44);
            this.lblRazon.Name = "lblRazon";
            this.lblRazon.Size = new System.Drawing.Size(106, 13);
            this.lblRazon.TabIndex = 63;
            this.lblRazon.Text = "Nombre Descriptivo :";
            // 
            // lblUsuario
            // 
            this.lblUsuario.AutoSize = true;
            this.lblUsuario.Location = new System.Drawing.Point(20, 17);
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
            this.groupBox1.Size = new System.Drawing.Size(560, 50);
            this.groupBox1.TabIndex = 70;
            this.groupBox1.TabStop = false;
            // 
            // txtOperacion
            // 
            this.txtOperacion.BackColor = System.Drawing.SystemColors.HighlightText;
            this.txtOperacion.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtOperacion.Enabled = false;
            this.txtOperacion.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtOperacion.Location = new System.Drawing.Point(134, 15);
            this.txtOperacion.MaxLength = 6;
            this.txtOperacion.Name = "txtOperacion";
            this.txtOperacion.ReadOnly = true;
            this.txtOperacion.Size = new System.Drawing.Size(171, 20);
            this.txtOperacion.TabIndex = 64;
            this.txtOperacion.TextChanged += new System.EventHandler(this.txtOperacion_TextChanged);
            // 
            // lblOperacion
            // 
            this.lblOperacion.AutoSize = true;
            this.lblOperacion.Location = new System.Drawing.Point(20, 19);
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
            this.toolStripForm.Size = new System.Drawing.Size(579, 25);
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
            // cmbAnexo
            // 
            this.cmbAnexo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbAnexo.FormattingEnabled = true;
            this.cmbAnexo.Items.AddRange(new object[] {
            "MO   DEL CONCEPTO DE PLANILLA",
            "CU    DE LA CUENTA CONTABLE",
            "DO    DOCUMENTO DE IDENTIDAD DEL TRABAJADOR",
            "CO    CODIGO INTERNO DEL TRABAJADOR",
            "CA    CODIGO AUXILIAR DEL TRABAJADOR",
            "XX     NO POSEE"});
            this.cmbAnexo.Location = new System.Drawing.Point(134, 14);
            this.cmbAnexo.Name = "cmbAnexo";
            this.cmbAnexo.Size = new System.Drawing.Size(389, 21);
            this.cmbAnexo.TabIndex = 72;
            this.cmbAnexo.SelectedIndexChanged += new System.EventHandler(this.cmbAnexo_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(20, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(43, 13);
            this.label1.TabIndex = 66;
            this.label1.Text = "Anexo :";
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(176, 67);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(52, 13);
            this.label20.TabIndex = 139;
            this.label20.Text = "Ane.Ref :";
            // 
            // txtAneRef
            // 
            this.txtAneRef.BackColor = System.Drawing.SystemColors.HighlightText;
            this.txtAneRef.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtAneRef.Enabled = false;
            this.txtAneRef.Location = new System.Drawing.Point(228, 63);
            this.txtAneRef.MaxLength = 18;
            this.txtAneRef.Name = "txtAneRef";
            this.txtAneRef.Size = new System.Drawing.Size(156, 20);
            this.txtAneRef.TabIndex = 138;
            // 
            // txtTipoAneRef
            // 
            this.txtTipoAneRef.BackColor = System.Drawing.SystemColors.HighlightText;
            this.txtTipoAneRef.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtTipoAneRef.Enabled = false;
            this.txtTipoAneRef.Location = new System.Drawing.Point(134, 63);
            this.txtTipoAneRef.MaxLength = 1;
            this.txtTipoAneRef.Name = "txtTipoAneRef";
            this.txtTipoAneRef.Size = new System.Drawing.Size(33, 20);
            this.txtTipoAneRef.TabIndex = 136;
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(20, 67);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(97, 13);
            this.label21.TabIndex = 137;
            this.label21.Text = "Tipo de Ane. Ref. :";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(176, 30);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(43, 13);
            this.label15.TabIndex = 135;
            this.label15.Text = "Anexo :";
            // 
            // txtAne
            // 
            this.txtAne.BackColor = System.Drawing.SystemColors.HighlightText;
            this.txtAne.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtAne.Enabled = false;
            this.txtAne.Location = new System.Drawing.Point(228, 26);
            this.txtAne.MaxLength = 18;
            this.txtAne.Name = "txtAne";
            this.txtAne.Size = new System.Drawing.Size(156, 20);
            this.txtAne.TabIndex = 134;
            // 
            // txtTipoAne
            // 
            this.txtTipoAne.BackColor = System.Drawing.SystemColors.HighlightText;
            this.txtTipoAne.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtTipoAne.Enabled = false;
            this.txtTipoAne.Location = new System.Drawing.Point(134, 26);
            this.txtTipoAne.MaxLength = 1;
            this.txtTipoAne.Name = "txtTipoAne";
            this.txtTipoAne.Size = new System.Drawing.Size(33, 20);
            this.txtTipoAne.TabIndex = 132;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(20, 30);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(82, 13);
            this.label13.TabIndex = 133;
            this.label13.Text = "Tipo de Anexo :";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label20);
            this.groupBox3.Controls.Add(this.txtAneRef);
            this.groupBox3.Controls.Add(this.txtTipoAneRef);
            this.groupBox3.Controls.Add(this.label21);
            this.groupBox3.Controls.Add(this.label15);
            this.groupBox3.Controls.Add(this.txtAne);
            this.groupBox3.Controls.Add(this.txtTipoAne);
            this.groupBox3.Controls.Add(this.label13);
            this.groupBox3.Location = new System.Drawing.Point(12, 259);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(556, 109);
            this.groupBox3.TabIndex = 140;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Anexos pertenecientes a la Cuenta";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.label2);
            this.groupBox4.Controls.Add(this.cmbAnexoRef);
            this.groupBox4.Controls.Add(this.label1);
            this.groupBox4.Controls.Add(this.cmbAnexo);
            this.groupBox4.Location = new System.Drawing.Point(11, 177);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(555, 76);
            this.groupBox4.TabIndex = 141;
            this.groupBox4.TabStop = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(20, 44);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(100, 13);
            this.label2.TabIndex = 73;
            this.label2.Text = "Anexo Referencial :";
            // 
            // cmbAnexoRef
            // 
            this.cmbAnexoRef.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbAnexoRef.FormattingEnabled = true;
            this.cmbAnexoRef.Items.AddRange(new object[] {
            "MO   DEL CONCEPTO DE PLANILLA",
            "CU    DE LA CUENTA CONTABLE",
            "DO    DOCUMENTO DE IDENTIDAD DEL TRABAJADOR",
            "CO    CODIGO INTERNO DEL TRABAJADOR",
            "CA    CODIGO AUXILIAR DEL TRABAJADOR",
            "XX     NO POSEE"});
            this.cmbAnexoRef.Location = new System.Drawing.Point(134, 41);
            this.cmbAnexoRef.Name = "cmbAnexoRef";
            this.cmbAnexoRef.Size = new System.Drawing.Size(389, 21);
            this.cmbAnexoRef.TabIndex = 74;
            this.cmbAnexoRef.SelectedIndexChanged += new System.EventHandler(this.cmbAnexoRef_SelectedIndexChanged);
            // 
            // ui_updcuentas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.HighlightText;
            this.ClientSize = new System.Drawing.Size(579, 404);
            this.ControlBox = false;
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.toolStripForm);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ui_updcuentas";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Actualización de Datos";
            this.Load += new System.EventHandler(this.ui_updcuentas_Load);
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
        private System.Windows.Forms.RadioButton radioButtonResumen;
        private System.Windows.Forms.RadioButton radioButtonDetallado;
        private System.Windows.Forms.ComboBox cmbAnexo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.TextBox txtAneRef;
        private System.Windows.Forms.TextBox txtTipoAneRef;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TextBox txtAne;
        private System.Windows.Forms.TextBox txtTipoAne;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cmbAnexoRef;
    }
}