namespace CaniaBrava
{
    partial class ui_kardex
    {
        /// <summary>
        /// Variable del diseñador requerida.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén utilizando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben eliminar; false en caso contrario, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido del método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.toolstripform = new System.Windows.Forms.ToolStrip();
            this.btnVisualiza = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnSalir = new System.Windows.Forms.ToolStripButton();
            this.txtCodigoIni = new System.Windows.Forms.TextBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.textBox7 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.cmbAgrupar = new System.Windows.Forms.ComboBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.txtCodigoFin = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtFechaFin = new System.Windows.Forms.MaskedTextBox();
            this.txtFechaIni = new System.Windows.Forms.MaskedTextBox();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.textBox5 = new System.Windows.Forms.TextBox();
            this.cmbAlmacen = new System.Windows.Forms.ComboBox();
            this.textBox6 = new System.Windows.Forms.TextBox();
            this.cmbTipoKardex = new System.Windows.Forms.ComboBox();
            this.chkSinStock = new System.Windows.Forms.CheckBox();
            this.chkStock = new System.Windows.Forms.CheckBox();
            this.toolstripform.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolstripform
            // 
            this.toolstripform.BackColor = System.Drawing.SystemColors.HighlightText;
            this.toolstripform.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.toolstripform.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnVisualiza,
            this.toolStripSeparator1,
            this.btnSalir});
            this.toolstripform.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.toolstripform.Location = new System.Drawing.Point(0, 380);
            this.toolstripform.Name = "toolstripform";
            this.toolstripform.ShowItemToolTips = false;
            this.toolstripform.Size = new System.Drawing.Size(616, 25);
            this.toolstripform.TabIndex = 5;
            // 
            // btnVisualiza
            // 
            this.btnVisualiza.Image = global::CaniaBrava.Properties.Resources.REPORT;
            this.btnVisualiza.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnVisualiza.Name = "btnVisualiza";
            this.btnVisualiza.Size = new System.Drawing.Size(76, 22);
            this.btnVisualiza.Text = "Visualizar";
            this.btnVisualiza.Click += new System.EventHandler(this.btnVisualiza_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
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
            // txtCodigoIni
            // 
            this.txtCodigoIni.BackColor = System.Drawing.SystemColors.HighlightText;
            this.txtCodigoIni.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtCodigoIni.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtCodigoIni.Location = new System.Drawing.Point(112, 43);
            this.txtCodigoIni.MaxLength = 20;
            this.txtCodigoIni.Name = "txtCodigoIni";
            this.txtCodigoIni.Size = new System.Drawing.Size(139, 20);
            this.txtCodigoIni.TabIndex = 0;
            this.txtCodigoIni.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtCodigoIni_KeyDown);
            this.txtCodigoIni.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCodigoIni_KeyPress);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.textBox7);
            this.groupBox3.Controls.Add(this.textBox2);
            this.groupBox3.Controls.Add(this.cmbAgrupar);
            this.groupBox3.Controls.Add(this.textBox1);
            this.groupBox3.Controls.Add(this.txtCodigoFin);
            this.groupBox3.Controls.Add(this.txtCodigoIni);
            this.groupBox3.Location = new System.Drawing.Point(12, 58);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(596, 101);
            this.groupBox3.TabIndex = 1;
            this.groupBox3.TabStop = false;
            // 
            // textBox7
            // 
            this.textBox7.BackColor = System.Drawing.SystemColors.Control;
            this.textBox7.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox7.Enabled = false;
            this.textBox7.ForeColor = System.Drawing.Color.Black;
            this.textBox7.Location = new System.Drawing.Point(6, 16);
            this.textBox7.Name = "textBox7";
            this.textBox7.ReadOnly = true;
            this.textBox7.Size = new System.Drawing.Size(100, 13);
            this.textBox7.TabIndex = 100;
            this.textBox7.Text = "Agrupar por :";
            // 
            // textBox2
            // 
            this.textBox2.BackColor = System.Drawing.SystemColors.Control;
            this.textBox2.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox2.Enabled = false;
            this.textBox2.ForeColor = System.Drawing.Color.Black;
            this.textBox2.Location = new System.Drawing.Point(6, 69);
            this.textBox2.Name = "textBox2";
            this.textBox2.ReadOnly = true;
            this.textBox2.Size = new System.Drawing.Size(100, 13);
            this.textBox2.TabIndex = 97;
            this.textBox2.Text = "Código Final";
            // 
            // cmbAgrupar
            // 
            this.cmbAgrupar.BackColor = System.Drawing.SystemColors.HighlightText;
            this.cmbAgrupar.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbAgrupar.FormattingEnabled = true;
            this.cmbAgrupar.Items.AddRange(new object[] {
            "CODIGO DE ARTICULO",
            "FAMILIA DE ARTICULOS"});
            this.cmbAgrupar.Location = new System.Drawing.Point(112, 16);
            this.cmbAgrupar.Name = "cmbAgrupar";
            this.cmbAgrupar.Size = new System.Drawing.Size(249, 21);
            this.cmbAgrupar.TabIndex = 99;
            // 
            // textBox1
            // 
            this.textBox1.BackColor = System.Drawing.SystemColors.Control;
            this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox1.Enabled = false;
            this.textBox1.ForeColor = System.Drawing.Color.Black;
            this.textBox1.Location = new System.Drawing.Point(6, 43);
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(100, 13);
            this.textBox1.TabIndex = 96;
            this.textBox1.Text = "Código Inicial";
            // 
            // txtCodigoFin
            // 
            this.txtCodigoFin.BackColor = System.Drawing.SystemColors.HighlightText;
            this.txtCodigoFin.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtCodigoFin.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtCodigoFin.Location = new System.Drawing.Point(112, 69);
            this.txtCodigoFin.MaxLength = 20;
            this.txtCodigoFin.Name = "txtCodigoFin";
            this.txtCodigoFin.Size = new System.Drawing.Size(139, 20);
            this.txtCodigoFin.TabIndex = 1;
            this.txtCodigoFin.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtCodigoFin_KeyDown);
            this.txtCodigoFin.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCodigoFin_KeyPress);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtFechaFin);
            this.groupBox1.Controls.Add(this.txtFechaIni);
            this.groupBox1.Controls.Add(this.textBox3);
            this.groupBox1.Controls.Add(this.textBox4);
            this.groupBox1.Location = new System.Drawing.Point(12, 168);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(596, 104);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Rango de Fechas";
            // 
            // txtFechaFin
            // 
            this.txtFechaFin.Location = new System.Drawing.Point(112, 55);
            this.txtFechaFin.Mask = "00/00/0000";
            this.txtFechaFin.Name = "txtFechaFin";
            this.txtFechaFin.Size = new System.Drawing.Size(139, 20);
            this.txtFechaFin.TabIndex = 1;
            this.txtFechaFin.ValidatingType = typeof(System.DateTime);
            this.txtFechaFin.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtFechaFin_KeyPress);
            // 
            // txtFechaIni
            // 
            this.txtFechaIni.Location = new System.Drawing.Point(112, 29);
            this.txtFechaIni.Mask = "00/00/0000";
            this.txtFechaIni.Name = "txtFechaIni";
            this.txtFechaIni.Size = new System.Drawing.Size(139, 20);
            this.txtFechaIni.TabIndex = 0;
            this.txtFechaIni.ValidatingType = typeof(System.DateTime);
            this.txtFechaIni.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtFechaIni_KeyPress);
            // 
            // textBox3
            // 
            this.textBox3.BackColor = System.Drawing.SystemColors.Control;
            this.textBox3.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox3.Enabled = false;
            this.textBox3.ForeColor = System.Drawing.Color.Black;
            this.textBox3.Location = new System.Drawing.Point(6, 55);
            this.textBox3.Name = "textBox3";
            this.textBox3.ReadOnly = true;
            this.textBox3.Size = new System.Drawing.Size(100, 13);
            this.textBox3.TabIndex = 97;
            this.textBox3.Text = "Fecha Final";
            // 
            // textBox4
            // 
            this.textBox4.BackColor = System.Drawing.SystemColors.Control;
            this.textBox4.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox4.Enabled = false;
            this.textBox4.ForeColor = System.Drawing.Color.Black;
            this.textBox4.Location = new System.Drawing.Point(6, 29);
            this.textBox4.Name = "textBox4";
            this.textBox4.ReadOnly = true;
            this.textBox4.Size = new System.Drawing.Size(100, 13);
            this.textBox4.TabIndex = 96;
            this.textBox4.Text = "Fecha Inicial";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.textBox5);
            this.groupBox2.Controls.Add(this.cmbAlmacen);
            this.groupBox2.Location = new System.Drawing.Point(12, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(596, 43);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            // 
            // textBox5
            // 
            this.textBox5.BackColor = System.Drawing.SystemColors.Control;
            this.textBox5.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox5.Enabled = false;
            this.textBox5.ForeColor = System.Drawing.Color.Black;
            this.textBox5.Location = new System.Drawing.Point(6, 12);
            this.textBox5.Name = "textBox5";
            this.textBox5.ReadOnly = true;
            this.textBox5.Size = new System.Drawing.Size(100, 13);
            this.textBox5.TabIndex = 98;
            this.textBox5.Text = "Almacén :";
            // 
            // cmbAlmacen
            // 
            this.cmbAlmacen.BackColor = System.Drawing.SystemColors.HighlightText;
            this.cmbAlmacen.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbAlmacen.FormattingEnabled = true;
            this.cmbAlmacen.Location = new System.Drawing.Point(112, 12);
            this.cmbAlmacen.Name = "cmbAlmacen";
            this.cmbAlmacen.Size = new System.Drawing.Size(249, 21);
            this.cmbAlmacen.TabIndex = 0;
            // 
            // textBox6
            // 
            this.textBox6.BackColor = System.Drawing.SystemColors.Control;
            this.textBox6.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox6.Enabled = false;
            this.textBox6.ForeColor = System.Drawing.Color.Black;
            this.textBox6.Location = new System.Drawing.Point(18, 289);
            this.textBox6.Name = "textBox6";
            this.textBox6.ReadOnly = true;
            this.textBox6.Size = new System.Drawing.Size(100, 13);
            this.textBox6.TabIndex = 100;
            this.textBox6.Text = "Tipo de Kardex :";
            // 
            // cmbTipoKardex
            // 
            this.cmbTipoKardex.BackColor = System.Drawing.SystemColors.HighlightText;
            this.cmbTipoKardex.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTipoKardex.FormattingEnabled = true;
            this.cmbTipoKardex.Items.AddRange(new object[] {
            "Resumen",
            "Detallado"});
            this.cmbTipoKardex.Location = new System.Drawing.Point(124, 289);
            this.cmbTipoKardex.Name = "cmbTipoKardex";
            this.cmbTipoKardex.Size = new System.Drawing.Size(249, 21);
            this.cmbTipoKardex.TabIndex = 3;
            // 
            // chkSinStock
            // 
            this.chkSinStock.AutoSize = true;
            this.chkSinStock.Location = new System.Drawing.Point(124, 322);
            this.chkSinStock.Name = "chkSinStock";
            this.chkSinStock.Size = new System.Drawing.Size(143, 17);
            this.chkSinStock.TabIndex = 4;
            this.chkSinStock.Text = "Incluir artículos sin stock";
            this.chkSinStock.UseVisualStyleBackColor = true;
            // 
            // chkStock
            // 
            this.chkStock.AutoSize = true;
            this.chkStock.Location = new System.Drawing.Point(124, 345);
            this.chkStock.Name = "chkStock";
            this.chkStock.Size = new System.Drawing.Size(252, 17);
            this.chkStock.TabIndex = 101;
            this.chkStock.Text = "Antes de Visualizar, proceder a recalcular Stock";
            this.chkStock.UseVisualStyleBackColor = true;
            // 
            // ui_kardex
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(616, 405);
            this.Controls.Add(this.chkStock);
            this.Controls.Add(this.chkSinStock);
            this.Controls.Add(this.textBox6);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.cmbTipoKardex);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.toolstripform);
            this.Name = "ui_kardex";
            this.Text = "Kardex del Almacén";
            this.Load += new System.EventHandler(this.ui_kardex_Load);
            this.toolstripform.ResumeLayout(false);
            this.toolstripform.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolstripform;
        public System.Windows.Forms.ToolStripButton btnVisualiza;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton btnSalir;
        private System.Windows.Forms.TextBox txtCodigoIni;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox txtCodigoFin;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.TextBox textBox4;
        private System.Windows.Forms.MaskedTextBox txtFechaFin;
        private System.Windows.Forms.MaskedTextBox txtFechaIni;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox textBox5;
        private System.Windows.Forms.ComboBox cmbAlmacen;
        private System.Windows.Forms.TextBox textBox6;
        private System.Windows.Forms.ComboBox cmbTipoKardex;
        private System.Windows.Forms.CheckBox chkSinStock;
        private System.Windows.Forms.TextBox textBox7;
        private System.Windows.Forms.ComboBox cmbAgrupar;
        private System.Windows.Forms.CheckBox chkStock;
    }
}
