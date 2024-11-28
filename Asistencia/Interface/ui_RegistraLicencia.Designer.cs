namespace CaniaBrava
{
    partial class ui_RegistraLicencia
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ui_RegistraLicencia));
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txtSerie = new System.Windows.Forms.TextBox();
            this.lbllicencia = new System.Windows.Forms.Label();
            this.txtDireccion = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtRuc = new System.Windows.Forms.MaskedTextBox();
            this.txtRazon = new System.Windows.Forms.TextBox();
            this.lblClave = new System.Windows.Forms.Label();
            this.lblRazon = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtEmail = new System.Windows.Forms.TextBox();
            this.groupBoxEX1 = new Dotnetrix.Controls.GroupBoxEX();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.toolStripForm = new System.Windows.Forms.ToolStrip();
            this.btnSalir = new System.Windows.Forms.ToolStripButton();
            this.groupBox2.SuspendLayout();
            this.groupBoxEX1.SuspendLayout();
            this.toolStripForm.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.txtSerie);
            this.groupBox2.Controls.Add(this.lbllicencia);
            this.groupBox2.Location = new System.Drawing.Point(8, 6);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(548, 39);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            this.groupBox2.Enter += new System.EventHandler(this.groupBox2_Enter);
            // 
            // txtSerie
            // 
            this.txtSerie.BackColor = System.Drawing.SystemColors.HighlightText;
            this.txtSerie.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSerie.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtSerie.Location = new System.Drawing.Point(138, 11);
            this.txtSerie.MaxLength = 120;
            this.txtSerie.Name = "txtSerie";
            this.txtSerie.ReadOnly = true;
            this.txtSerie.Size = new System.Drawing.Size(396, 24);
            this.txtSerie.TabIndex = 65;
            // 
            // lbllicencia
            // 
            this.lbllicencia.AutoSize = true;
            this.lbllicencia.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbllicencia.Location = new System.Drawing.Point(17, 12);
            this.lbllicencia.Name = "lbllicencia";
            this.lbllicencia.Size = new System.Drawing.Size(83, 18);
            this.lbllicencia.TabIndex = 0;
            this.lbllicencia.Text = "Nro. Serie :";
            // 
            // txtDireccion
            // 
            this.txtDireccion.BackColor = System.Drawing.SystemColors.HighlightText;
            this.txtDireccion.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtDireccion.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtDireccion.Location = new System.Drawing.Point(138, 69);
            this.txtDireccion.MaxLength = 120;
            this.txtDireccion.Name = "txtDireccion";
            this.txtDireccion.ReadOnly = true;
            this.txtDireccion.Size = new System.Drawing.Size(396, 20);
            this.txtDireccion.TabIndex = 2;
            this.txtDireccion.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtDireccion_KeyPress);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(19, 73);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(58, 13);
            this.label3.TabIndex = 63;
            this.label3.Text = "Dirección :";
            // 
            // txtRuc
            // 
            this.txtRuc.BackColor = System.Drawing.SystemColors.HighlightText;
            this.txtRuc.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtRuc.Location = new System.Drawing.Point(138, 19);
            this.txtRuc.Mask = "99999999999";
            this.txtRuc.Name = "txtRuc";
            this.txtRuc.PromptChar = ' ';
            this.txtRuc.ReadOnly = true;
            this.txtRuc.Size = new System.Drawing.Size(171, 20);
            this.txtRuc.TabIndex = 0;
            this.txtRuc.ValidatingType = typeof(int);
            this.txtRuc.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtRuc_KeyPress);
            // 
            // txtRazon
            // 
            this.txtRazon.BackColor = System.Drawing.SystemColors.HighlightText;
            this.txtRazon.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtRazon.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtRazon.Location = new System.Drawing.Point(138, 44);
            this.txtRazon.MaxLength = 120;
            this.txtRazon.Name = "txtRazon";
            this.txtRazon.ReadOnly = true;
            this.txtRazon.Size = new System.Drawing.Size(396, 20);
            this.txtRazon.TabIndex = 1;
            this.txtRazon.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtRazon_KeyPress);
            // 
            // lblClave
            // 
            this.lblClave.AutoSize = true;
            this.lblClave.Location = new System.Drawing.Point(19, 23);
            this.lblClave.Name = "lblClave";
            this.lblClave.Size = new System.Drawing.Size(45, 13);
            this.lblClave.TabIndex = 61;
            this.lblClave.Text = "R.U.C. :";
            // 
            // lblRazon
            // 
            this.lblRazon.AutoSize = true;
            this.lblRazon.Location = new System.Drawing.Point(19, 48);
            this.lblRazon.Name = "lblRazon";
            this.lblRazon.Size = new System.Drawing.Size(76, 13);
            this.lblRazon.TabIndex = 60;
            this.lblRazon.Text = "Razón Social :";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(19, 94);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 13);
            this.label1.TabIndex = 64;
            this.label1.Text = "Email :";
            // 
            // txtEmail
            // 
            this.txtEmail.BackColor = System.Drawing.SystemColors.HighlightText;
            this.txtEmail.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtEmail.Location = new System.Drawing.Point(138, 94);
            this.txtEmail.MaxLength = 120;
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.ReadOnly = true;
            this.txtEmail.Size = new System.Drawing.Size(396, 20);
            this.txtEmail.TabIndex = 3;
            this.txtEmail.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtEmail_KeyPress);
            // 
            // groupBoxEX1
            // 
            this.groupBoxEX1.Controls.Add(this.txtRuc);
            this.groupBoxEX1.Controls.Add(this.txtEmail);
            this.groupBoxEX1.Controls.Add(this.lblRazon);
            this.groupBoxEX1.Controls.Add(this.label1);
            this.groupBoxEX1.Controls.Add(this.lblClave);
            this.groupBoxEX1.Controls.Add(this.txtDireccion);
            this.groupBoxEX1.Controls.Add(this.txtRazon);
            this.groupBoxEX1.Controls.Add(this.label3);
            this.groupBoxEX1.Location = new System.Drawing.Point(8, 51);
            this.groupBoxEX1.Name = "groupBoxEX1";
            this.groupBoxEX1.Size = new System.Drawing.Size(548, 123);
            this.groupBoxEX1.TabIndex = 1;
            this.groupBoxEX1.TabStop = false;
            this.groupBoxEX1.Text = "Datos de Registro";
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(8, 179);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.ReadOnly = true;
            this.richTextBox1.Size = new System.Drawing.Size(548, 223);
            this.richTextBox1.TabIndex = 2;
            this.richTextBox1.Text = resources.GetString("richTextBox1.Text");
            this.richTextBox1.TextChanged += new System.EventHandler(this.richTextBox1_TextChanged);
            // 
            // toolStripForm
            // 
            this.toolStripForm.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.toolStripForm.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.toolStripForm.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnSalir});
            this.toolStripForm.Location = new System.Drawing.Point(0, 414);
            this.toolStripForm.Name = "toolStripForm";
            this.toolStripForm.ShowItemToolTips = false;
            this.toolStripForm.Size = new System.Drawing.Size(566, 25);
            this.toolStripForm.TabIndex = 5;
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
            // ui_RegistraLicencia
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.HighlightText;
            this.ClientSize = new System.Drawing.Size(566, 439);
            this.ControlBox = false;
            this.Controls.Add(this.toolStripForm);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.groupBoxEX1);
            this.Controls.Add(this.groupBox2);
            this.MaximumSize = new System.Drawing.Size(582, 478);
            this.MinimumSize = new System.Drawing.Size(582, 478);
            this.Name = "ui_RegistraLicencia";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Registro del Software mySQLPlan";
            this.Load += new System.EventHandler(this.ui_RegistraLicencia_Load);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBoxEX1.ResumeLayout(false);
            this.groupBoxEX1.PerformLayout();
            this.toolStripForm.ResumeLayout(false);
            this.toolStripForm.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label lbllicencia;
        private System.Windows.Forms.TextBox txtDireccion;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.MaskedTextBox txtRuc;
        private System.Windows.Forms.TextBox txtRazon;
        private System.Windows.Forms.Label lblClave;
        private System.Windows.Forms.Label lblRazon;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtEmail;
        private Dotnetrix.Controls.GroupBoxEX groupBoxEX1;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.ToolStrip toolStripForm;
        private System.Windows.Forms.ToolStripButton btnSalir;
        private System.Windows.Forms.TextBox txtSerie;
    }
}