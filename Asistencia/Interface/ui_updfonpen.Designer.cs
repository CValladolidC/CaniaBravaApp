namespace CaniaBrava
{
    partial class ui_updfonpen
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
            this.cmbEstado = new System.Windows.Forms.ComboBox();
            this.lblEstado = new System.Windows.Forms.Label();
            this.txtFondo = new System.Windows.Forms.TextBox();
            this.lblRazon = new System.Windows.Forms.Label();
            this.lblUsuario = new System.Windows.Forms.Label();
            this.toolStripForm = new System.Windows.Forms.ToolStrip();
            this.btnAceptar = new System.Windows.Forms.ToolStripButton();
            this.btnCancelar = new System.Windows.Forms.ToolStripButton();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtCodigo = new System.Windows.Forms.TextBox();
            this.txtPS = new System.Windows.Forms.TextBox();
            this.txtCV = new System.Windows.Forms.TextBox();
            this.txtCF = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txtSNP = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtCodNet = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.toolStripForm.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtOperacion
            // 
            this.txtOperacion.BackColor = System.Drawing.SystemColors.HighlightText;
            this.txtOperacion.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtOperacion.Enabled = false;
            this.txtOperacion.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.txtOperacion.Location = new System.Drawing.Point(122, 16);
            this.txtOperacion.MaxLength = 6;
            this.txtOperacion.Name = "txtOperacion";
            this.txtOperacion.ReadOnly = true;
            this.txtOperacion.Size = new System.Drawing.Size(171, 20);
            this.txtOperacion.TabIndex = 0;
            // 
            // lblOperacion
            // 
            this.lblOperacion.AutoSize = true;
            this.lblOperacion.Location = new System.Drawing.Point(11, 20);
            this.lblOperacion.Name = "lblOperacion";
            this.lblOperacion.Size = new System.Drawing.Size(59, 13);
            this.lblOperacion.TabIndex = 48;
            this.lblOperacion.Text = "Operación:";
            // 
            // cmbEstado
            // 
            this.cmbEstado.FormattingEnabled = true;
            this.cmbEstado.Items.AddRange(new object[] {
            "A        ANULADO  ",
            "V         VIGENTE"});
            this.cmbEstado.Location = new System.Drawing.Point(122, 186);
            this.cmbEstado.Name = "cmbEstado";
            this.cmbEstado.Size = new System.Drawing.Size(171, 21);
            this.cmbEstado.Sorted = true;
            this.cmbEstado.TabIndex = 7;
            this.cmbEstado.SelectedIndexChanged += new System.EventHandler(this.cmbEstado_SelectedIndexChanged);
            this.cmbEstado.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cmbEstado_KeyPress);
            // 
            // lblEstado
            // 
            this.lblEstado.AutoSize = true;
            this.lblEstado.Location = new System.Drawing.Point(6, 190);
            this.lblEstado.Name = "lblEstado";
            this.lblEstado.Size = new System.Drawing.Size(46, 13);
            this.lblEstado.TabIndex = 47;
            this.lblEstado.Text = "Estado :";
            // 
            // txtFondo
            // 
            this.txtFondo.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtFondo.Location = new System.Drawing.Point(122, 37);
            this.txtFondo.MaxLength = 80;
            this.txtFondo.Name = "txtFondo";
            this.txtFondo.Size = new System.Drawing.Size(396, 20);
            this.txtFondo.TabIndex = 1;
            this.txtFondo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtFondo_KeyPress);
            // 
            // lblRazon
            // 
            this.lblRazon.AutoSize = true;
            this.lblRazon.Location = new System.Drawing.Point(6, 41);
            this.lblRazon.Name = "lblRazon";
            this.lblRazon.Size = new System.Drawing.Size(110, 13);
            this.lblRazon.TabIndex = 43;
            this.lblRazon.Text = "Fondo de Pensiones :";
            // 
            // lblUsuario
            // 
            this.lblUsuario.AutoSize = true;
            this.lblUsuario.Location = new System.Drawing.Point(6, 15);
            this.lblUsuario.Name = "lblUsuario";
            this.lblUsuario.Size = new System.Drawing.Size(46, 13);
            this.lblUsuario.TabIndex = 42;
            this.lblUsuario.Text = "Código :";
            // 
            // toolStripForm
            // 
            this.toolStripForm.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.toolStripForm.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.toolStripForm.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnAceptar,
            this.btnCancelar});
            this.toolStripForm.Location = new System.Drawing.Point(0, 287);
            this.toolStripForm.Name = "toolStripForm";
            this.toolStripForm.ShowItemToolTips = false;
            this.toolStripForm.Size = new System.Drawing.Size(574, 25);
            this.toolStripForm.TabIndex = 2;
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
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 91);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(102, 13);
            this.label1.TabIndex = 50;
            this.label1.Text = "% Prima de Seguro :";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(5, 116);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(107, 13);
            this.label2.TabIndex = 52;
            this.label2.Text = "% Comisión Variable :";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 141);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(85, 13);
            this.label3.TabIndex = 54;
            this.label3.Text = "% Comisión Fija :";
            // 
            // txtCodigo
            // 
            this.txtCodigo.BackColor = System.Drawing.SystemColors.HighlightText;
            this.txtCodigo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtCodigo.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtCodigo.Location = new System.Drawing.Point(122, 13);
            this.txtCodigo.MaxLength = 3;
            this.txtCodigo.Name = "txtCodigo";
            this.txtCodigo.Size = new System.Drawing.Size(171, 20);
            this.txtCodigo.TabIndex = 0;
            this.txtCodigo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCodigo_KeyPress);
            // 
            // txtPS
            // 
            this.txtPS.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtPS.Location = new System.Drawing.Point(122, 87);
            this.txtPS.MaxLength = 80;
            this.txtPS.Name = "txtPS";
            this.txtPS.Size = new System.Drawing.Size(171, 20);
            this.txtPS.TabIndex = 3;
            this.txtPS.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtPS.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtPS_KeyPress);
            // 
            // txtCV
            // 
            this.txtCV.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtCV.Location = new System.Drawing.Point(122, 112);
            this.txtCV.MaxLength = 80;
            this.txtCV.Name = "txtCV";
            this.txtCV.Size = new System.Drawing.Size(171, 20);
            this.txtCV.TabIndex = 4;
            this.txtCV.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtCV.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCV_KeyPress);
            // 
            // txtCF
            // 
            this.txtCF.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtCF.Location = new System.Drawing.Point(122, 137);
            this.txtCF.MaxLength = 80;
            this.txtCF.Name = "txtCF";
            this.txtCF.Size = new System.Drawing.Size(171, 20);
            this.txtCF.TabIndex = 5;
            this.txtCF.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtCF.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCF_KeyPress);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtOperacion);
            this.groupBox1.Controls.Add(this.lblOperacion);
            this.groupBox1.Location = new System.Drawing.Point(6, 2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(561, 53);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.txtCodNet);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.txtSNP);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.txtCF);
            this.groupBox2.Controls.Add(this.txtCV);
            this.groupBox2.Controls.Add(this.txtPS);
            this.groupBox2.Controls.Add(this.txtCodigo);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.cmbEstado);
            this.groupBox2.Controls.Add(this.lblEstado);
            this.groupBox2.Controls.Add(this.txtFondo);
            this.groupBox2.Controls.Add(this.lblRazon);
            this.groupBox2.Controls.Add(this.lblUsuario);
            this.groupBox2.Location = new System.Drawing.Point(7, 61);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(560, 216);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            // 
            // txtSNP
            // 
            this.txtSNP.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtSNP.Location = new System.Drawing.Point(122, 161);
            this.txtSNP.MaxLength = 80;
            this.txtSNP.Name = "txtSNP";
            this.txtSNP.Size = new System.Drawing.Size(171, 20);
            this.txtSNP.TabIndex = 6;
            this.txtSNP.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtSNP.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtSNP_KeyPress);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 165);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(46, 13);
            this.label4.TabIndex = 56;
            this.label4.Text = "% SNP :";
            // 
            // txtCodNet
            // 
            this.txtCodNet.BackColor = System.Drawing.SystemColors.HighlightText;
            this.txtCodNet.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtCodNet.Location = new System.Drawing.Point(121, 62);
            this.txtCodNet.MaxLength = 3;
            this.txtCodNet.Name = "txtCodNet";
            this.txtCodNet.Size = new System.Drawing.Size(171, 20);
            this.txtCodNet.TabIndex = 2;
            this.txtCodNet.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCodNet_KeyPress);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(5, 64);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(86, 13);
            this.label5.TabIndex = 58;
            this.label5.Text = "Código AFP Net:";
            // 
            // ui_updfonpen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.HighlightText;
            this.ClientSize = new System.Drawing.Size(574, 312);
            this.ControlBox = false;
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.toolStripForm);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ui_updfonpen";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Actualización de Datos";
            this.Load += new System.EventHandler(this.ui_updfonpen_Load);
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

        private System.Windows.Forms.TextBox txtOperacion;
        private System.Windows.Forms.Label lblOperacion;
        private System.Windows.Forms.ComboBox cmbEstado;
        private System.Windows.Forms.Label lblEstado;
        private System.Windows.Forms.TextBox txtFondo;
        private System.Windows.Forms.Label lblRazon;
        private System.Windows.Forms.Label lblUsuario;
        private System.Windows.Forms.ToolStrip toolStripForm;
        private System.Windows.Forms.ToolStripButton btnAceptar;
        private System.Windows.Forms.ToolStripButton btnCancelar;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtCodigo;
        private System.Windows.Forms.TextBox txtPS;
        private System.Windows.Forms.TextBox txtCV;
        private System.Windows.Forms.TextBox txtCF;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox txtSNP;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtCodNet;
        private System.Windows.Forms.Label label5;
    }
}