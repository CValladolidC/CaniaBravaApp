namespace CaniaBrava
{
    partial class ui_AccesoSis
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ui_AccesoSis));
            this.btnIniciar = new System.Windows.Forms.ToolStripButton();
            this.btnSalir = new System.Windows.Forms.ToolStripButton();
            this.toolStripForm = new System.Windows.Forms.ToolStrip();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtusuario = new System.Windows.Forms.TextBox();
            this.txtclave = new System.Windows.Forms.TextBox();
            this.txtpropietario = new System.Windows.Forms.TextBox();
            this.lblPropietario = new System.Windows.Forms.Label();
            this.lblCompania = new System.Windows.Forms.Label();
            this.cmbCompania = new System.Windows.Forms.ComboBox();
            this.txtTypeUsr = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lblregistrado = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.lblVersion = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.toolStripForm.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // btnIniciar
            // 
            this.btnIniciar.Image = global::CaniaBrava.Properties.Resources.PASS;
            this.btnIniciar.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnIniciar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnIniciar.Name = "btnIniciar";
            this.btnIniciar.Size = new System.Drawing.Size(62, 22);
            this.btnIniciar.Text = "Iniciar";
            this.btnIniciar.Click += new System.EventHandler(this.btnIniciar_Click);
            // 
            // btnSalir
            // 
            this.btnSalir.Image = global::CaniaBrava.Properties.Resources.CLOSE;
            this.btnSalir.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnSalir.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSalir.Name = "btnSalir";
            this.btnSalir.Size = new System.Drawing.Size(51, 22);
            this.btnSalir.Text = "Salir";
            this.btnSalir.Click += new System.EventHandler(this.btnSalir_Click);
            // 
            // toolStripForm
            // 
            this.toolStripForm.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.toolStripForm.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.toolStripForm.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnIniciar,
            this.btnSalir});
            this.toolStripForm.Location = new System.Drawing.Point(0, 270);
            this.toolStripForm.Name = "toolStripForm";
            this.toolStripForm.ShowItemToolTips = false;
            this.toolStripForm.Size = new System.Drawing.Size(430, 25);
            this.toolStripForm.TabIndex = 22;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(17, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(49, 13);
            this.label1.TabIndex = 11;
            this.label1.Text = "Usuario :";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(17, 47);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(67, 13);
            this.label2.TabIndex = 12;
            this.label2.Text = "Contraseña :";
            // 
            // txtusuario
            // 
            this.txtusuario.BackColor = System.Drawing.SystemColors.HighlightText;
            this.txtusuario.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtusuario.Location = new System.Drawing.Point(85, 16);
            this.txtusuario.MaxLength = 20;
            this.txtusuario.Name = "txtusuario";
            this.txtusuario.ShortcutsEnabled = false;
            this.txtusuario.Size = new System.Drawing.Size(214, 20);
            this.txtusuario.TabIndex = 1;
            this.txtusuario.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtusuario_KeyPress);
            // 
            // txtclave
            // 
            this.txtclave.BackColor = System.Drawing.SystemColors.HighlightText;
            this.txtclave.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtclave.Location = new System.Drawing.Point(85, 43);
            this.txtclave.MaxLength = 15;
            this.txtclave.Name = "txtclave";
            this.txtclave.Size = new System.Drawing.Size(214, 20);
            this.txtclave.TabIndex = 2;
            this.txtclave.UseSystemPasswordChar = true;
            this.txtclave.TextChanged += new System.EventHandler(this.txtclave_TextChanged);
            this.txtclave.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtclave_KeyPress);
            // 
            // txtpropietario
            // 
            this.txtpropietario.BackColor = System.Drawing.SystemColors.HighlightText;
            this.txtpropietario.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtpropietario.Enabled = false;
            this.txtpropietario.Location = new System.Drawing.Point(85, 70);
            this.txtpropietario.Name = "txtpropietario";
            this.txtpropietario.Size = new System.Drawing.Size(321, 20);
            this.txtpropietario.TabIndex = 3;
            this.txtpropietario.Visible = false;
            // 
            // lblPropietario
            // 
            this.lblPropietario.AutoSize = true;
            this.lblPropietario.Location = new System.Drawing.Point(17, 74);
            this.lblPropietario.Name = "lblPropietario";
            this.lblPropietario.Size = new System.Drawing.Size(63, 13);
            this.lblPropietario.TabIndex = 16;
            this.lblPropietario.Text = "Propietario :";
            this.lblPropietario.Visible = false;
            // 
            // lblCompania
            // 
            this.lblCompania.AutoSize = true;
            this.lblCompania.Location = new System.Drawing.Point(17, 81);
            this.lblCompania.Name = "lblCompania";
            this.lblCompania.Size = new System.Drawing.Size(62, 13);
            this.lblCompania.TabIndex = 17;
            this.lblCompania.Text = "Compañía :";
            this.lblCompania.Visible = false;
            // 
            // cmbCompania
            // 
            this.cmbCompania.BackColor = System.Drawing.SystemColors.HighlightText;
            this.cmbCompania.FormattingEnabled = true;
            this.cmbCompania.Location = new System.Drawing.Point(85, 75);
            this.cmbCompania.Name = "cmbCompania";
            this.cmbCompania.Size = new System.Drawing.Size(321, 21);
            this.cmbCompania.TabIndex = 4;
            this.cmbCompania.Visible = false;
            this.cmbCompania.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cmbCompania_KeyPress);
            // 
            // txtTypeUsr
            // 
            this.txtTypeUsr.BackColor = System.Drawing.SystemColors.HighlightText;
            this.txtTypeUsr.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtTypeUsr.Enabled = false;
            this.txtTypeUsr.Location = new System.Drawing.Point(341, 42);
            this.txtTypeUsr.MaxLength = 6;
            this.txtTypeUsr.Name = "txtTypeUsr";
            this.txtTypeUsr.ShortcutsEnabled = false;
            this.txtTypeUsr.Size = new System.Drawing.Size(65, 20);
            this.txtTypeUsr.TabIndex = 18;
            this.txtTypeUsr.Visible = false;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtTypeUsr);
            this.groupBox1.Controls.Add(this.cmbCompania);
            this.groupBox1.Controls.Add(this.lblCompania);
            this.groupBox1.Controls.Add(this.lblPropietario);
            this.groupBox1.Controls.Add(this.txtpropietario);
            this.groupBox1.Controls.Add(this.txtclave);
            this.groupBox1.Controls.Add(this.txtusuario);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(7, 166);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(417, 100);
            this.groupBox1.TabIndex = 23;
            this.groupBox1.TabStop = false;
            // 
            // lblregistrado
            // 
            this.lblregistrado.AutoSize = true;
            this.lblregistrado.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblregistrado.Location = new System.Drawing.Point(7, 19);
            this.lblregistrado.Name = "lblregistrado";
            this.lblregistrado.Size = new System.Drawing.Size(59, 13);
            this.lblregistrado.TabIndex = 19;
            this.lblregistrado.Text = "EMPRESA";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.lblregistrado);
            this.groupBox2.Location = new System.Drawing.Point(7, 119);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(417, 49);
            this.groupBox2.TabIndex = 25;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Licencia registrada a nombre de";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(84, 6);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(325, 12);
            this.label4.TabIndex = 24;
            this.label4.Text = "Propiedad Intelectual Protegida y Registrada como se describe en Acerca de...";
            // 
            // lblVersion
            // 
            this.lblVersion.AutoSize = true;
            this.lblVersion.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblVersion.Location = new System.Drawing.Point(332, 273);
            this.lblVersion.Name = "lblVersion";
            this.lblVersion.Size = new System.Drawing.Size(92, 15);
            this.lblVersion.TabIndex = 20;
            this.lblVersion.Text = "Versión X.X.X.X";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(27, 17);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(380, 103);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 26;
            this.pictureBox1.TabStop = false;
            // 
            // ui_AccesoSis
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.HighlightText;
            this.ClientSize = new System.Drawing.Size(430, 295);
            this.ControlBox = false;
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.lblVersion);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.toolStripForm);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(446, 334);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(446, 334);
            this.Name = "ui_AccesoSis";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Sistema de Asistencia - Seguridad de Acceso";
            this.Load += new System.EventHandler(this.ui_AccesoSis_Load);
            this.toolStripForm.ResumeLayout(false);
            this.toolStripForm.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStripButton btnIniciar;
        private System.Windows.Forms.ToolStripButton btnSalir;
        private System.Windows.Forms.ToolStrip toolStripForm;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtusuario;
        private System.Windows.Forms.TextBox txtclave;
        private System.Windows.Forms.TextBox txtpropietario;
        private System.Windows.Forms.Label lblPropietario;
        private System.Windows.Forms.Label lblCompania;
        private System.Windows.Forms.ComboBox cmbCompania;
        private System.Windows.Forms.TextBox txtTypeUsr;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label lblregistrado;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblVersion;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}