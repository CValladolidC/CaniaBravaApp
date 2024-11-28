namespace CaniaBrava
{
    partial class ui_configsis
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
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btnGrabar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.txtMaxFilaBol = new System.Windows.Forms.MaskedTextBox();
            this.txtBetweenBol = new System.Windows.Forms.MaskedTextBox();
            this.txtNroBolPag = new System.Windows.Forms.MaskedTextBox();
            this.txtMaxFilaBolWin = new System.Windows.Forms.MaskedTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.toolStrip1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.BackColor = System.Drawing.SystemColors.HighlightText;
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnGrabar,
            this.toolStripSeparator1,
            this.toolStripButton1});
            this.toolStrip1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.toolStrip1.Location = new System.Drawing.Point(0, 286);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.ShowItemToolTips = false;
            this.toolStrip1.Size = new System.Drawing.Size(654, 29);
            this.toolStrip1.TabIndex = 101;
            // 
            // btnGrabar
            // 
            this.btnGrabar.Image = global::CaniaBrava.Properties.Resources.SAVE;
            this.btnGrabar.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnGrabar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnGrabar.Name = "btnGrabar";
            this.btnGrabar.Size = new System.Drawing.Size(68, 26);
            this.btnGrabar.Text = "Grabar";
            this.btnGrabar.Click += new System.EventHandler(this.btnGrabar_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 29);
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.Image = global::CaniaBrava.Properties.Resources.CLOSE;
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(49, 26);
            this.toolStripButton1.Text = "Salir";
            this.toolStripButton1.Click += new System.EventHandler(this.toolStripButton1_Click);
            // 
            // txtMaxFilaBol
            // 
            this.txtMaxFilaBol.Location = new System.Drawing.Point(211, 31);
            this.txtMaxFilaBol.Mask = "99";
            this.txtMaxFilaBol.Name = "txtMaxFilaBol";
            this.txtMaxFilaBol.PromptChar = ' ';
            this.txtMaxFilaBol.Size = new System.Drawing.Size(100, 20);
            this.txtMaxFilaBol.TabIndex = 102;
            // 
            // txtBetweenBol
            // 
            this.txtBetweenBol.Location = new System.Drawing.Point(211, 71);
            this.txtBetweenBol.Mask = "99";
            this.txtBetweenBol.Name = "txtBetweenBol";
            this.txtBetweenBol.PromptChar = ' ';
            this.txtBetweenBol.Size = new System.Drawing.Size(100, 20);
            this.txtBetweenBol.TabIndex = 103;
            // 
            // txtNroBolPag
            // 
            this.txtNroBolPag.Location = new System.Drawing.Point(211, 115);
            this.txtNroBolPag.Mask = "99";
            this.txtNroBolPag.Name = "txtNroBolPag";
            this.txtNroBolPag.PromptChar = ' ';
            this.txtNroBolPag.Size = new System.Drawing.Size(100, 20);
            this.txtNroBolPag.TabIndex = 104;
            // 
            // txtMaxFilaBolWin
            // 
            this.txtMaxFilaBolWin.Location = new System.Drawing.Point(211, 37);
            this.txtMaxFilaBolWin.Mask = "99";
            this.txtMaxFilaBolWin.Name = "txtMaxFilaBolWin";
            this.txtMaxFilaBolWin.PromptChar = ' ';
            this.txtMaxFilaBolWin.Size = new System.Drawing.Size(100, 20);
            this.txtMaxFilaBolWin.TabIndex = 105;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(30, 35);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(168, 13);
            this.label1.TabIndex = 106;
            this.label1.Text = "Nro. Filas de Conceptos en Boleta";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(30, 75);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(119, 13);
            this.label2.TabIndex = 107;
            this.label2.Text = "Nro. Filas  entre Boletas";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(30, 119);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(139, 13);
            this.label3.TabIndex = 108;
            this.label3.Text = "Nro. de Boletas por página :";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(30, 40);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(174, 13);
            this.label4.TabIndex = 109;
            this.label4.Text = "Nro. Filas de Conceptos en Boleta :";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtMaxFilaBol);
            this.groupBox1.Controls.Add(this.txtBetweenBol);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.txtNroBolPag);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(25, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(605, 152);
            this.groupBox1.TabIndex = 110;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Impresión modo Texto";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.txtMaxFilaBolWin);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Location = new System.Drawing.Point(25, 170);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(605, 84);
            this.groupBox2.TabIndex = 111;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Impresión Gráfica";
            // 
            // ui_configsis
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.HighlightText;
            this.ClientSize = new System.Drawing.Size(654, 315);
            this.ControlBox = false;
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.toolStrip1);
            this.Name = "ui_configsis";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Configuración de Parámetros de Impresión";
            this.Load += new System.EventHandler(this.ui_configsis_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton btnGrabar;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.MaskedTextBox txtMaxFilaBol;
        private System.Windows.Forms.MaskedTextBox txtBetweenBol;
        private System.Windows.Forms.MaskedTextBox txtNroBolPag;
        private System.Windows.Forms.MaskedTextBox txtMaxFilaBolWin;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
    }
}