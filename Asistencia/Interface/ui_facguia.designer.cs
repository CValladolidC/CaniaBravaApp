namespace CaniaBrava
{
    partial class ui_facguia
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dgvData = new System.Windows.Forms.DataGridView();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cmbFecha = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.cmbListar = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cmbCaja = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtFechaFin = new System.Windows.Forms.MaskedTextBox();
            this.txtFechaIni = new System.Windows.Forms.MaskedTextBox();
            this.radioButtonMov = new System.Windows.Forms.RadioButton();
            this.radioButtonRazon = new System.Windows.Forms.RadioButton();
            this.radioButtonNroGuia = new System.Windows.Forms.RadioButton();
            this.btnBuscar = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnTodos = new System.Windows.Forms.Button();
            this.txtBuscar = new System.Windows.Forms.TextBox();
            this.radioButtonRuc = new System.Windows.Forms.RadioButton();
            this.txtRegEncontrados = new System.Windows.Forms.TextBox();
            this.toolstripform = new System.Windows.Forms.ToolStrip();
            this.btnEditar = new System.Windows.Forms.ToolStripButton();
            this.btnDesFinaliza = new System.Windows.Forms.ToolStripButton();
            this.btnExcel = new System.Windows.Forms.ToolStripButton();
            this.btnImprimir = new System.Windows.Forms.ToolStripButton();
            this.btnActualizar = new System.Windows.Forms.ToolStripButton();
            this.btnSalir = new System.Windows.Forms.ToolStripButton();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgvData)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.toolstripform.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgvData
            // 
            this.dgvData.AllowUserToAddRows = false;
            this.dgvData.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvData.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvData.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvData.Location = new System.Drawing.Point(4, 76);
            this.dgvData.Name = "dgvData";
            this.dgvData.ReadOnly = true;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvData.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dgvData.Size = new System.Drawing.Size(1114, 330);
            this.dgvData.TabIndex = 84;
            this.dgvData.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvData_CellContentClick);
            this.dgvData.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgvData_CellFormatting);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 33);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cmbFecha);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.cmbListar);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.cmbCaja);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.txtFechaFin);
            this.groupBox1.Controls.Add(this.txtFechaIni);
            this.groupBox1.Location = new System.Drawing.Point(4, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1114, 70);
            this.groupBox1.TabIndex = 88;
            this.groupBox1.TabStop = false;
            // 
            // cmbFecha
            // 
            this.cmbFecha.BackColor = System.Drawing.SystemColors.HighlightText;
            this.cmbFecha.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbFecha.FormattingEnabled = true;
            this.cmbFecha.Items.AddRange(new object[] {
            "F   DE LA FACTURA",
            "G   DE LA GUÍA DE REMISION"});
            this.cmbFecha.Location = new System.Drawing.Point(483, 14);
            this.cmbFecha.Name = "cmbFecha";
            this.cmbFecha.Size = new System.Drawing.Size(221, 21);
            this.cmbFecha.TabIndex = 89;
            this.cmbFecha.SelectedIndexChanged += new System.EventHandler(this.cmbFecha_SelectedIndexChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(404, 18);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(80, 13);
            this.label5.TabIndex = 88;
            this.label5.Text = "Fecha a Filtrar :";
            // 
            // cmbListar
            // 
            this.cmbListar.BackColor = System.Drawing.SystemColors.HighlightText;
            this.cmbListar.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbListar.FormattingEnabled = true;
            this.cmbListar.Items.AddRange(new object[] {
            "P   PENDIENTES",
            "F   FACTURADAS",
            "X   TODAS"});
            this.cmbListar.Location = new System.Drawing.Point(909, 14);
            this.cmbListar.Name = "cmbListar";
            this.cmbListar.Size = new System.Drawing.Size(168, 21);
            this.cmbListar.TabIndex = 86;
            this.cmbListar.SelectedIndexChanged += new System.EventHandler(this.cmbListar_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(865, 18);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(38, 13);
            this.label4.TabIndex = 87;
            this.label4.Text = "Listar :";
            // 
            // cmbCaja
            // 
            this.cmbCaja.BackColor = System.Drawing.SystemColors.HighlightText;
            this.cmbCaja.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCaja.FormattingEnabled = true;
            this.cmbCaja.Location = new System.Drawing.Point(75, 14);
            this.cmbCaja.Name = "cmbCaja";
            this.cmbCaja.Size = new System.Drawing.Size(320, 21);
            this.cmbCaja.TabIndex = 84;
            this.cmbCaja.SelectedIndexChanged += new System.EventHandler(this.cmbCaja_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(10, 18);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(37, 13);
            this.label3.TabIndex = 85;
            this.label3.Text = "Caja  :";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(628, 44);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(68, 13);
            this.label2.TabIndex = 67;
            this.label2.Text = "Fecha Final :";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(404, 44);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(73, 13);
            this.label1.TabIndex = 66;
            this.label1.Text = "Fecha Inicial :";
            // 
            // txtFechaFin
            // 
            this.txtFechaFin.Location = new System.Drawing.Point(702, 40);
            this.txtFechaFin.Mask = "00/00/0000";
            this.txtFechaFin.Name = "txtFechaFin";
            this.txtFechaFin.Size = new System.Drawing.Size(139, 20);
            this.txtFechaFin.TabIndex = 65;
            this.txtFechaFin.ValidatingType = typeof(System.DateTime);
            this.txtFechaFin.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtFechaFin_KeyPress);
            // 
            // txtFechaIni
            // 
            this.txtFechaIni.Location = new System.Drawing.Point(483, 40);
            this.txtFechaIni.Mask = "00/00/0000";
            this.txtFechaIni.Name = "txtFechaIni";
            this.txtFechaIni.Size = new System.Drawing.Size(139, 20);
            this.txtFechaIni.TabIndex = 64;
            this.txtFechaIni.ValidatingType = typeof(System.DateTime);
            this.txtFechaIni.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtFechaIni_KeyPress);
            // 
            // radioButtonMov
            // 
            this.radioButtonMov.AutoSize = true;
            this.radioButtonMov.Location = new System.Drawing.Point(369, 46);
            this.radioButtonMov.Name = "radioButtonMov";
            this.radioButtonMov.Size = new System.Drawing.Size(93, 17);
            this.radioButtonMov.TabIndex = 21;
            this.radioButtonMov.Text = "Mov. Almacén";
            this.radioButtonMov.UseVisualStyleBackColor = true;
            this.radioButtonMov.CheckedChanged += new System.EventHandler(this.radioButtonMov_CheckedChanged);
            // 
            // radioButtonRazon
            // 
            this.radioButtonRazon.AutoSize = true;
            this.radioButtonRazon.Location = new System.Drawing.Point(268, 46);
            this.radioButtonRazon.Name = "radioButtonRazon";
            this.radioButtonRazon.Size = new System.Drawing.Size(88, 17);
            this.radioButtonRazon.TabIndex = 20;
            this.radioButtonRazon.Text = "Razón Social";
            this.radioButtonRazon.UseVisualStyleBackColor = true;
            this.radioButtonRazon.CheckedChanged += new System.EventHandler(this.radioButtonRazon_CheckedChanged);
            // 
            // radioButtonNroGuia
            // 
            this.radioButtonNroGuia.AutoSize = true;
            this.radioButtonNroGuia.Checked = true;
            this.radioButtonNroGuia.Location = new System.Drawing.Point(21, 46);
            this.radioButtonNroGuia.Name = "radioButtonNroGuia";
            this.radioButtonNroGuia.Size = new System.Drawing.Size(165, 17);
            this.radioButtonNroGuia.TabIndex = 19;
            this.radioButtonNroGuia.TabStop = true;
            this.radioButtonNroGuia.Text = "Número de Guía de Remisión";
            this.radioButtonNroGuia.UseVisualStyleBackColor = true;
            this.radioButtonNroGuia.CheckedChanged += new System.EventHandler(this.radioButtonNroGuia_CheckedChanged);
            // 
            // btnBuscar
            // 
            this.btnBuscar.Location = new System.Drawing.Point(470, 50);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(75, 23);
            this.btnBuscar.TabIndex = 18;
            this.btnBuscar.Text = "Buscar";
            this.btnBuscar.UseVisualStyleBackColor = true;
            this.btnBuscar.Click += new System.EventHandler(this.btnBuscar_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.radioButtonMov);
            this.groupBox2.Controls.Add(this.radioButtonRazon);
            this.groupBox2.Controls.Add(this.radioButtonNroGuia);
            this.groupBox2.Controls.Add(this.btnBuscar);
            this.groupBox2.Controls.Add(this.btnTodos);
            this.groupBox2.Controls.Add(this.txtBuscar);
            this.groupBox2.Controls.Add(this.radioButtonRuc);
            this.groupBox2.Location = new System.Drawing.Point(419, 407);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(557, 90);
            this.groupBox2.TabIndex = 86;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Buscar por";
            // 
            // btnTodos
            // 
            this.btnTodos.Location = new System.Drawing.Point(470, 17);
            this.btnTodos.Name = "btnTodos";
            this.btnTodos.Size = new System.Drawing.Size(75, 23);
            this.btnTodos.TabIndex = 17;
            this.btnTodos.Text = "Todos";
            this.btnTodos.UseVisualStyleBackColor = true;
            this.btnTodos.Click += new System.EventHandler(this.btnTodos_Click);
            // 
            // txtBuscar
            // 
            this.txtBuscar.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtBuscar.Location = new System.Drawing.Point(21, 20);
            this.txtBuscar.Name = "txtBuscar";
            this.txtBuscar.Size = new System.Drawing.Size(443, 20);
            this.txtBuscar.TabIndex = 16;
            // 
            // radioButtonRuc
            // 
            this.radioButtonRuc.AutoSize = true;
            this.radioButtonRuc.Location = new System.Drawing.Point(195, 46);
            this.radioButtonRuc.Name = "radioButtonRuc";
            this.radioButtonRuc.Size = new System.Drawing.Size(57, 17);
            this.radioButtonRuc.TabIndex = 14;
            this.radioButtonRuc.Text = "R.U.C.";
            this.radioButtonRuc.UseVisualStyleBackColor = true;
            this.radioButtonRuc.CheckedChanged += new System.EventHandler(this.radioButtonRuc_CheckedChanged);
            // 
            // txtRegEncontrados
            // 
            this.txtRegEncontrados.BackColor = System.Drawing.SystemColors.HighlightText;
            this.txtRegEncontrados.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRegEncontrados.Location = new System.Drawing.Point(20, 31);
            this.txtRegEncontrados.Multiline = true;
            this.txtRegEncontrados.Name = "txtRegEncontrados";
            this.txtRegEncontrados.ReadOnly = true;
            this.txtRegEncontrados.Size = new System.Drawing.Size(364, 23);
            this.txtRegEncontrados.TabIndex = 21;
            // 
            // toolstripform
            // 
            this.toolstripform.BackColor = System.Drawing.SystemColors.HighlightText;
            this.toolstripform.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.toolstripform.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnEditar,
            this.btnDesFinaliza,
            this.btnExcel,
            this.btnImprimir,
            this.btnActualizar,
            this.toolStripSeparator1,
            this.btnSalir});
            this.toolstripform.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.toolstripform.Location = new System.Drawing.Point(0, 509);
            this.toolstripform.Name = "toolstripform";
            this.toolstripform.ShowItemToolTips = false;
            this.toolstripform.Size = new System.Drawing.Size(1123, 33);
            this.toolstripform.TabIndex = 85;
            // 
            // btnEditar
            // 
            this.btnEditar.Image = global::CaniaBrava.Properties.Resources.OPEN;
            this.btnEditar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnEditar.Name = "btnEditar";
            this.btnEditar.Size = new System.Drawing.Size(57, 30);
            this.btnEditar.Text = "Editar";
            this.btnEditar.Click += new System.EventHandler(this.btnEditar_Click);
            // 
            // btnDesFinaliza
            // 
            this.btnDesFinaliza.Image = global::CaniaBrava.Properties.Resources.DESFIN;
            this.btnDesFinaliza.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnDesFinaliza.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnDesFinaliza.Name = "btnDesFinaliza";
            this.btnDesFinaliza.Size = new System.Drawing.Size(87, 30);
            this.btnDesFinaliza.Text = "Desfinalizar";
            this.btnDesFinaliza.Click += new System.EventHandler(this.btnDesFinaliza_Click);
            // 
            // btnExcel
            // 
            this.btnExcel.Image = global::CaniaBrava.Properties.Resources.EXCEL;
            this.btnExcel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnExcel.Name = "btnExcel";
            this.btnExcel.Size = new System.Drawing.Size(53, 30);
            this.btnExcel.Text = "Excel";
            this.btnExcel.Click += new System.EventHandler(this.btnExcel_Click);
            // 
            // btnImprimir
            // 
            this.btnImprimir.Image = global::CaniaBrava.Properties.Resources.PRINT;
            this.btnImprimir.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnImprimir.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnImprimir.Name = "btnImprimir";
            this.btnImprimir.Size = new System.Drawing.Size(85, 30);
            this.btnImprimir.Text = "Imprimir";
            this.btnImprimir.Click += new System.EventHandler(this.btnImprimir_Click);
            // 
            // btnActualizar
            // 
            this.btnActualizar.Image = global::CaniaBrava.Properties.Resources.refresh;
            this.btnActualizar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnActualizar.Name = "btnActualizar";
            this.btnActualizar.Size = new System.Drawing.Size(79, 30);
            this.btnActualizar.Text = "Actualizar";
            this.btnActualizar.Click += new System.EventHandler(this.btnActualizar_Click);
            // 
            // btnSalir
            // 
            this.btnSalir.Image = global::CaniaBrava.Properties.Resources.CLOSE;
            this.btnSalir.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSalir.Name = "btnSalir";
            this.btnSalir.Size = new System.Drawing.Size(49, 30);
            this.btnSalir.Text = "Salir";
            this.btnSalir.Click += new System.EventHandler(this.btnSalir_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.txtRegEncontrados);
            this.groupBox3.Location = new System.Drawing.Point(9, 412);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(404, 85);
            this.groupBox3.TabIndex = 87;
            this.groupBox3.TabStop = false;
            // 
            // ui_facguia
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(1123, 542);
            this.Controls.Add(this.dgvData);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.toolstripform);
            this.Controls.Add(this.groupBox3);
            this.Name = "ui_facguia";
            this.Text = "Facturación de Guías de Remisión";
            this.Load += new System.EventHandler(this.ui_facguia_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvData)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.toolstripform.ResumeLayout(false);
            this.toolstripform.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvData;
        private System.Windows.Forms.ToolStripButton btnSalir;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        public System.Windows.Forms.ToolStripButton btnActualizar;
        public System.Windows.Forms.ToolStripButton btnImprimir;
        public System.Windows.Forms.ToolStripButton btnExcel;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox cmbCaja;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.MaskedTextBox txtFechaFin;
        private System.Windows.Forms.MaskedTextBox txtFechaIni;
        private System.Windows.Forms.RadioButton radioButtonMov;
        private System.Windows.Forms.RadioButton radioButtonRazon;
        private System.Windows.Forms.ToolStripButton btnEditar;
        private System.Windows.Forms.RadioButton radioButtonNroGuia;
        private System.Windows.Forms.Button btnBuscar;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnTodos;
        private System.Windows.Forms.TextBox txtBuscar;
        private System.Windows.Forms.RadioButton radioButtonRuc;
        private System.Windows.Forms.TextBox txtRegEncontrados;
        private System.Windows.Forms.ToolStrip toolstripform;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.ComboBox cmbListar;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cmbFecha;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ToolStripButton btnDesFinaliza;
    }
}
