namespace CaniaBrava
{
    partial class ui_updcensalud
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
            this.lblUsuario = new System.Windows.Forms.Label();
            this.lblPropietario = new System.Windows.Forms.Label();
            this.lblClave = new System.Windows.Forms.Label();
            this.txtDNI = new System.Windows.Forms.TextBox();
            this.txtNombres = new System.Windows.Forms.TextBox();
            this.txtApellidos = new System.Windows.Forms.TextBox();
            this.toolStripForm = new System.Windows.Forms.ToolStrip();
            this.btnAceptar = new System.Windows.Forms.ToolStripButton();
            this.btnCancelar = new System.Windows.Forms.ToolStripButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txtcodUbigeo = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnUbigeo = new System.Windows.Forms.Button();
            this.txtDscUbigeo = new System.Windows.Forms.TextBox();
            this.txtdireccion = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.pictureBoxImg = new System.Windows.Forms.PictureBox();
            this.toolStripForm.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxImg)).BeginInit();
            this.SuspendLayout();
            // 
            // lblUsuario
            // 
            this.lblUsuario.AutoSize = true;
            this.lblUsuario.Location = new System.Drawing.Point(188, 38);
            this.lblUsuario.Name = "lblUsuario";
            this.lblUsuario.Size = new System.Drawing.Size(46, 13);
            this.lblUsuario.TabIndex = 0;
            this.lblUsuario.Text = "Codigo :";
            // 
            // lblPropietario
            // 
            this.lblPropietario.AutoSize = true;
            this.lblPropietario.Location = new System.Drawing.Point(188, 78);
            this.lblPropietario.Name = "lblPropietario";
            this.lblPropietario.Size = new System.Drawing.Size(89, 13);
            this.lblPropietario.TabIndex = 1;
            this.lblPropietario.Text = "Centro de Salud :";
            // 
            // lblClave
            // 
            this.lblClave.AutoSize = true;
            this.lblClave.Location = new System.Drawing.Point(188, 117);
            this.lblClave.Name = "lblClave";
            this.lblClave.Size = new System.Drawing.Size(55, 13);
            this.lblClave.TabIndex = 2;
            this.lblClave.Text = "Telefono :";
            // 
            // txtDNI
            // 
            this.txtDNI.BackColor = System.Drawing.SystemColors.HighlightText;
            this.txtDNI.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtDNI.Enabled = false;
            this.txtDNI.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtDNI.Location = new System.Drawing.Point(283, 35);
            this.txtDNI.MaxLength = 10;
            this.txtDNI.Name = "txtDNI";
            this.txtDNI.Size = new System.Drawing.Size(109, 20);
            this.txtDNI.TabIndex = 1;
            this.txtDNI.Enter += new System.EventHandler(this.txtDNI_Enter);
            this.txtDNI.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtDNI_KeyPress);
            // 
            // txtNombres
            // 
            this.txtNombres.BackColor = System.Drawing.SystemColors.HighlightText;
            this.txtNombres.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtNombres.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtNombres.Location = new System.Drawing.Point(283, 75);
            this.txtNombres.MaxLength = 20;
            this.txtNombres.Name = "txtNombres";
            this.txtNombres.Size = new System.Drawing.Size(324, 20);
            this.txtNombres.TabIndex = 2;
            this.txtNombres.Enter += new System.EventHandler(this.txtPropietario_Enter);
            this.txtNombres.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtPropietario_KeyPress);
            // 
            // txtApellidos
            // 
            this.txtApellidos.BackColor = System.Drawing.SystemColors.HighlightText;
            this.txtApellidos.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtApellidos.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtApellidos.Location = new System.Drawing.Point(283, 114);
            this.txtApellidos.MaxLength = 50;
            this.txtApellidos.Name = "txtApellidos";
            this.txtApellidos.Size = new System.Drawing.Size(109, 20);
            this.txtApellidos.TabIndex = 3;
            this.txtApellidos.Enter += new System.EventHandler(this.txtClave_Enter);
            this.txtApellidos.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtClave_KeyPress);
            // 
            // toolStripForm
            // 
            this.toolStripForm.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.toolStripForm.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.toolStripForm.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnAceptar,
            this.btnCancelar});
            this.toolStripForm.Location = new System.Drawing.Point(0, 242);
            this.toolStripForm.Name = "toolStripForm";
            this.toolStripForm.ShowItemToolTips = false;
            this.toolStripForm.Size = new System.Drawing.Size(638, 25);
            this.toolStripForm.TabIndex = 6;
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
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.txtcodUbigeo);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.btnUbigeo);
            this.groupBox2.Controls.Add(this.txtDscUbigeo);
            this.groupBox2.Controls.Add(this.txtdireccion);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.groupBox1);
            this.groupBox2.Controls.Add(this.txtApellidos);
            this.groupBox2.Controls.Add(this.txtNombres);
            this.groupBox2.Controls.Add(this.txtDNI);
            this.groupBox2.Controls.Add(this.lblClave);
            this.groupBox2.Controls.Add(this.lblPropietario);
            this.groupBox2.Controls.Add(this.lblUsuario);
            this.groupBox2.Location = new System.Drawing.Point(9, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(617, 227);
            this.groupBox2.TabIndex = 13;
            this.groupBox2.TabStop = false;
            // 
            // txtcodUbigeo
            // 
            this.txtcodUbigeo.BackColor = System.Drawing.SystemColors.HighlightText;
            this.txtcodUbigeo.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtcodUbigeo.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtcodUbigeo.Location = new System.Drawing.Point(532, 153);
            this.txtcodUbigeo.MaxLength = 50;
            this.txtcodUbigeo.Name = "txtcodUbigeo";
            this.txtcodUbigeo.Size = new System.Drawing.Size(75, 20);
            this.txtcodUbigeo.TabIndex = 53;
            this.txtcodUbigeo.Visible = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(188, 156);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(61, 13);
            this.label2.TabIndex = 52;
            this.label2.Text = "Ubicación :";
            // 
            // btnUbigeo
            // 
            this.btnUbigeo.Location = new System.Drawing.Point(502, 152);
            this.btnUbigeo.Name = "btnUbigeo";
            this.btnUbigeo.Size = new System.Drawing.Size(24, 20);
            this.btnUbigeo.TabIndex = 51;
            this.btnUbigeo.Text = "---";
            this.btnUbigeo.UseVisualStyleBackColor = true;
            this.btnUbigeo.Click += new System.EventHandler(this.btnUbigeo_Click);
            // 
            // txtDscUbigeo
            // 
            this.txtDscUbigeo.BackColor = System.Drawing.SystemColors.HighlightText;
            this.txtDscUbigeo.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtDscUbigeo.Enabled = false;
            this.txtDscUbigeo.Location = new System.Drawing.Point(283, 152);
            this.txtDscUbigeo.MaxLength = 6;
            this.txtDscUbigeo.Name = "txtDscUbigeo";
            this.txtDscUbigeo.Size = new System.Drawing.Size(218, 20);
            this.txtDscUbigeo.TabIndex = 50;
            // 
            // txtdireccion
            // 
            this.txtdireccion.BackColor = System.Drawing.SystemColors.HighlightText;
            this.txtdireccion.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtdireccion.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtdireccion.Location = new System.Drawing.Point(283, 189);
            this.txtdireccion.MaxLength = 50;
            this.txtdireccion.Name = "txtdireccion";
            this.txtdireccion.Size = new System.Drawing.Size(324, 20);
            this.txtdireccion.TabIndex = 12;
            this.txtdireccion.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtespecialidad_KeyPress);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(188, 192);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(58, 13);
            this.label1.TabIndex = 11;
            this.label1.Text = "Dirección :";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.pictureBoxImg);
            this.groupBox1.Location = new System.Drawing.Point(10, 9);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(169, 176);
            this.groupBox1.TabIndex = 10;
            this.groupBox1.TabStop = false;
            // 
            // pictureBoxImg
            // 
            this.pictureBoxImg.Image = global::CaniaBrava.Properties.Resources.censalud;
            this.pictureBoxImg.Location = new System.Drawing.Point(6, 13);
            this.pictureBoxImg.Name = "pictureBoxImg";
            this.pictureBoxImg.Size = new System.Drawing.Size(157, 155);
            this.pictureBoxImg.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxImg.TabIndex = 31;
            this.pictureBoxImg.TabStop = false;
            // 
            // ui_updcensalud
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.HighlightText;
            this.ClientSize = new System.Drawing.Size(638, 267);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.toolStripForm);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(654, 306);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(654, 306);
            this.Name = "ui_updcensalud";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Registro de Invitado";
            this.toolStripForm.ResumeLayout(false);
            this.toolStripForm.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxImg)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblUsuario;
        private System.Windows.Forms.Label lblPropietario;
        private System.Windows.Forms.Label lblClave;
        private System.Windows.Forms.TextBox txtDNI;
        private System.Windows.Forms.TextBox txtNombres;
        private System.Windows.Forms.TextBox txtApellidos;
        private System.Windows.Forms.ToolStrip toolStripForm;
        private System.Windows.Forms.ToolStripButton btnAceptar;
        private System.Windows.Forms.ToolStripButton btnCancelar;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.PictureBox pictureBoxImg;
        private System.Windows.Forms.TextBox txtdireccion;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnUbigeo;
        private System.Windows.Forms.TextBox txtDscUbigeo;
        private System.Windows.Forms.TextBox txtcodUbigeo;
    }
}