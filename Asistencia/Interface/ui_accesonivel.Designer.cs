namespace CaniaBrava
{
    partial class ui_accesonivel
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
            this.btnSalir = new System.Windows.Forms.ToolStripButton();
            this.lblUsuario = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.RD01 = new System.Windows.Forms.RadioButton();
            this.RD02 = new System.Windows.Forms.RadioButton();
            this.RD03 = new System.Windows.Forms.RadioButton();
            this.RD04 = new System.Windows.Forms.RadioButton();
            this.toolStrip1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.BackColor = System.Drawing.SystemColors.HighlightText;
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnGrabar,
            this.toolStripSeparator1,
            this.btnSalir});
            this.toolStrip1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.toolStrip1.Location = new System.Drawing.Point(0, 167);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.ShowItemToolTips = false;
            this.toolStrip1.Size = new System.Drawing.Size(238, 29);
            this.toolStrip1.TabIndex = 100;
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
            // btnSalir
            // 
            this.btnSalir.Image = global::CaniaBrava.Properties.Resources.CLOSE;
            this.btnSalir.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSalir.Name = "btnSalir";
            this.btnSalir.Size = new System.Drawing.Size(49, 26);
            this.btnSalir.Text = "Salir";
            this.btnSalir.Click += new System.EventHandler(this.toolStripButton1_Click);
            // 
            // lblUsuario
            // 
            this.lblUsuario.AutoSize = true;
            this.lblUsuario.Location = new System.Drawing.Point(12, 9);
            this.lblUsuario.Name = "lblUsuario";
            this.lblUsuario.Size = new System.Drawing.Size(35, 13);
            this.lblUsuario.TabIndex = 101;
            this.lblUsuario.Text = "label1";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.RD04);
            this.groupBox1.Controls.Add(this.RD03);
            this.groupBox1.Controls.Add(this.RD02);
            this.groupBox1.Controls.Add(this.RD01);
            this.groupBox1.Location = new System.Drawing.Point(12, 37);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(214, 127);
            this.groupBox1.TabIndex = 102;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Niveles de Aprobacion";
            // 
            // RD01
            // 
            this.RD01.AutoSize = true;
            this.RD01.Location = new System.Drawing.Point(23, 26);
            this.RD01.Name = "RD01";
            this.RD01.Size = new System.Drawing.Size(93, 17);
            this.RD01.TabIndex = 0;
            this.RD01.TabStop = true;
            this.RD01.Text = "  Nivel 1 (AP1)";
            this.RD01.UseVisualStyleBackColor = true;
            // 
            // RD02
            // 
            this.RD02.AutoSize = true;
            this.RD02.Location = new System.Drawing.Point(23, 49);
            this.RD02.Name = "RD02";
            this.RD02.Size = new System.Drawing.Size(93, 17);
            this.RD02.TabIndex = 1;
            this.RD02.TabStop = true;
            this.RD02.Text = "  Nivel 2 (AP2)";
            this.RD02.UseVisualStyleBackColor = true;
            // 
            // RD03
            // 
            this.RD03.AutoSize = true;
            this.RD03.Location = new System.Drawing.Point(23, 72);
            this.RD03.Name = "RD03";
            this.RD03.Size = new System.Drawing.Size(93, 17);
            this.RD03.TabIndex = 2;
            this.RD03.TabStop = true;
            this.RD03.Text = "  Nivel 3 (AP3)";
            this.RD03.UseVisualStyleBackColor = true;
            // 
            // RD04
            // 
            this.RD04.AutoSize = true;
            this.RD04.Location = new System.Drawing.Point(23, 95);
            this.RD04.Name = "RD04";
            this.RD04.Size = new System.Drawing.Size(93, 17);
            this.RD04.TabIndex = 3;
            this.RD04.TabStop = true;
            this.RD04.Text = "  Nivel 4 (AP4)";
            this.RD04.UseVisualStyleBackColor = true;
            // 
            // ui_accesonivel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.HighlightText;
            this.ClientSize = new System.Drawing.Size(238, 196);
            this.ControlBox = false;
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.lblUsuario);
            this.Controls.Add(this.toolStrip1);
            this.MaximumSize = new System.Drawing.Size(254, 235);
            this.MinimumSize = new System.Drawing.Size(254, 235);
            this.Name = "ui_accesonivel";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Opciones de Niveles";
            this.Load += new System.EventHandler(this.ui_accesomenu_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton btnGrabar;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton btnSalir;
        private System.Windows.Forms.Label lblUsuario;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton RD03;
        private System.Windows.Forms.RadioButton RD02;
        private System.Windows.Forms.RadioButton RD01;
        private System.Windows.Forms.RadioButton RD04;
    }
}