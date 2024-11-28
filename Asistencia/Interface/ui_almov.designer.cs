namespace CaniaBrava
{
    partial class ui_almov
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            this.txtRegEncontrados = new System.Windows.Forms.TextBox();
            this.txtRegTotal = new System.Windows.Forms.TextBox();
            this.radioButtonCodigo = new System.Windows.Forms.RadioButton();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.btnTodos = new System.Windows.Forms.Button();
            this.txtBuscar = new System.Windows.Forms.TextBox();
            this.radioButtonGlosa = new System.Windows.Forms.RadioButton();
            this.btnSalir = new System.Windows.Forms.ToolStripButton();
            this.btnNuevo = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolstripform = new System.Windows.Forms.ToolStrip();
            this.btnEditar = new System.Windows.Forms.ToolStripButton();
            this.btnDesFina = new System.Windows.Forms.ToolStripButton();
            this.btnEliminar = new System.Windows.Forms.ToolStripButton();
            this.btnExcel = new System.Windows.Forms.ToolStripButton();
            this.btnImprimir = new System.Windows.Forms.ToolStripButton();
            this.btnActualizar = new System.Windows.Forms.ToolStripButton();
            this.radioButtonRuc = new System.Windows.Forms.RadioButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.radioButtonNroDocRef = new System.Windows.Forms.RadioButton();
            this.dgvData = new System.Windows.Forms.DataGridView();
            this.cmbComedor = new System.Windows.Forms.ComboBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtFechaFin = new System.Windows.Forms.DateTimePicker();
            this.txtFechaIni = new System.Windows.Forms.DateTimePicker();
            this.groupBox3.SuspendLayout();
            this.toolstripform.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvData)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtRegEncontrados
            // 
            this.txtRegEncontrados.BackColor = System.Drawing.SystemColors.HighlightText;
            this.txtRegEncontrados.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRegEncontrados.Location = new System.Drawing.Point(20, 46);
            this.txtRegEncontrados.Multiline = true;
            this.txtRegEncontrados.Name = "txtRegEncontrados";
            this.txtRegEncontrados.ReadOnly = true;
            this.txtRegEncontrados.Size = new System.Drawing.Size(364, 23);
            this.txtRegEncontrados.TabIndex = 21;
            // 
            // txtRegTotal
            // 
            this.txtRegTotal.BackColor = System.Drawing.SystemColors.HighlightText;
            this.txtRegTotal.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRegTotal.Location = new System.Drawing.Point(20, 16);
            this.txtRegTotal.Multiline = true;
            this.txtRegTotal.Name = "txtRegTotal";
            this.txtRegTotal.ReadOnly = true;
            this.txtRegTotal.Size = new System.Drawing.Size(364, 23);
            this.txtRegTotal.TabIndex = 20;
            // 
            // radioButtonCodigo
            // 
            this.radioButtonCodigo.AutoSize = true;
            this.radioButtonCodigo.Checked = true;
            this.radioButtonCodigo.Location = new System.Drawing.Point(24, 46);
            this.radioButtonCodigo.Name = "radioButtonCodigo";
            this.radioButtonCodigo.Size = new System.Drawing.Size(97, 17);
            this.radioButtonCodigo.TabIndex = 19;
            this.radioButtonCodigo.TabStop = true;
            this.radioButtonCodigo.Text = "Nro. Operacion";
            this.radioButtonCodigo.UseVisualStyleBackColor = true;
            this.radioButtonCodigo.CheckedChanged += new System.EventHandler(this.radioButtonCodigo_CheckedChanged);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.txtRegEncontrados);
            this.groupBox3.Controls.Add(this.txtRegTotal);
            this.groupBox3.Location = new System.Drawing.Point(7, 417);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(404, 85);
            this.groupBox3.TabIndex = 28;
            this.groupBox3.TabStop = false;
            // 
            // btnTodos
            // 
            this.btnTodos.Location = new System.Drawing.Point(407, 17);
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
            this.txtBuscar.Size = new System.Drawing.Size(370, 20);
            this.txtBuscar.TabIndex = 16;
            this.txtBuscar.TextChanged += new System.EventHandler(this.txtBuscar_TextChanged);
            // 
            // radioButtonGlosa
            // 
            this.radioButtonGlosa.AutoSize = true;
            this.radioButtonGlosa.Location = new System.Drawing.Point(307, 46);
            this.radioButtonGlosa.Name = "radioButtonGlosa";
            this.radioButtonGlosa.Size = new System.Drawing.Size(52, 17);
            this.radioButtonGlosa.TabIndex = 15;
            this.radioButtonGlosa.Text = "Glosa";
            this.radioButtonGlosa.UseVisualStyleBackColor = true;
            this.radioButtonGlosa.Visible = false;
            this.radioButtonGlosa.CheckedChanged += new System.EventHandler(this.radioButtonGlosa_CheckedChanged);
            // 
            // btnSalir
            // 
            this.btnSalir.Image = global::CaniaBrava.Properties.Resources.CLOSE;
            this.btnSalir.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSalir.Name = "btnSalir";
            this.btnSalir.Size = new System.Drawing.Size(49, 27);
            this.btnSalir.Text = "Salir";
            this.btnSalir.Click += new System.EventHandler(this.btnSalir_Click);
            // 
            // btnNuevo
            // 
            this.btnNuevo.Image = global::CaniaBrava.Properties.Resources.NEW;
            this.btnNuevo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnNuevo.Name = "btnNuevo";
            this.btnNuevo.Size = new System.Drawing.Size(62, 27);
            this.btnNuevo.Text = "Nuevo";
            this.btnNuevo.Click += new System.EventHandler(this.btnNuevo_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 30);
            // 
            // toolstripform
            // 
            this.toolstripform.BackColor = System.Drawing.SystemColors.HighlightText;
            this.toolstripform.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.toolstripform.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnNuevo,
            this.btnEditar,
            this.btnDesFina,
            this.btnEliminar,
            this.btnExcel,
            this.btnImprimir,
            this.btnActualizar,
            this.toolStripSeparator1,
            this.btnSalir});
            this.toolstripform.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.toolstripform.Location = new System.Drawing.Point(0, 507);
            this.toolstripform.Name = "toolstripform";
            this.toolstripform.ShowItemToolTips = false;
            this.toolstripform.Size = new System.Drawing.Size(926, 30);
            this.toolstripform.TabIndex = 26;
            this.toolstripform.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.toolstripform_ItemClicked);
            // 
            // btnEditar
            // 
            this.btnEditar.Image = global::CaniaBrava.Properties.Resources.OPEN;
            this.btnEditar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnEditar.Name = "btnEditar";
            this.btnEditar.Size = new System.Drawing.Size(57, 27);
            this.btnEditar.Text = "Editar";
            this.btnEditar.Click += new System.EventHandler(this.btnEditar_Click);
            // 
            // btnDesFina
            // 
            this.btnDesFina.Image = global::CaniaBrava.Properties.Resources.DESFIN;
            this.btnDesFina.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnDesFina.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnDesFina.Name = "btnDesFina";
            this.btnDesFina.Size = new System.Drawing.Size(83, 27);
            this.btnDesFina.Text = "Desfinaliza";
            this.btnDesFina.Click += new System.EventHandler(this.btnDesFina_Click);
            // 
            // btnEliminar
            // 
            this.btnEliminar.Image = global::CaniaBrava.Properties.Resources.DELETE;
            this.btnEliminar.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnEliminar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnEliminar.Name = "btnEliminar";
            this.btnEliminar.Size = new System.Drawing.Size(75, 27);
            this.btnEliminar.Text = "Eliminar";
            this.btnEliminar.Click += new System.EventHandler(this.btnEliminar_Click);
            // 
            // btnExcel
            // 
            this.btnExcel.Image = global::CaniaBrava.Properties.Resources.EXCEL;
            this.btnExcel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnExcel.Name = "btnExcel";
            this.btnExcel.Size = new System.Drawing.Size(54, 27);
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
            this.btnImprimir.Visible = false;
            this.btnImprimir.Click += new System.EventHandler(this.btnImprimir_Click);
            // 
            // btnActualizar
            // 
            this.btnActualizar.Image = global::CaniaBrava.Properties.Resources.refresh;
            this.btnActualizar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnActualizar.Name = "btnActualizar";
            this.btnActualizar.Size = new System.Drawing.Size(79, 27);
            this.btnActualizar.Text = "Actualizar";
            this.btnActualizar.Click += new System.EventHandler(this.btnActualizar_Click);
            // 
            // radioButtonRuc
            // 
            this.radioButtonRuc.AutoSize = true;
            this.radioButtonRuc.Location = new System.Drawing.Point(127, 46);
            this.radioButtonRuc.Name = "radioButtonRuc";
            this.radioButtonRuc.Size = new System.Drawing.Size(74, 17);
            this.radioButtonRuc.TabIndex = 14;
            this.radioButtonRuc.Text = "Proveedor";
            this.radioButtonRuc.UseVisualStyleBackColor = true;
            this.radioButtonRuc.CheckedChanged += new System.EventHandler(this.radioButtonRuc_CheckedChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.radioButtonNroDocRef);
            this.groupBox2.Controls.Add(this.radioButtonCodigo);
            this.groupBox2.Controls.Add(this.btnTodos);
            this.groupBox2.Controls.Add(this.txtBuscar);
            this.groupBox2.Controls.Add(this.radioButtonGlosa);
            this.groupBox2.Controls.Add(this.radioButtonRuc);
            this.groupBox2.Location = new System.Drawing.Point(417, 417);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(497, 85);
            this.groupBox2.TabIndex = 27;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Buscar por";
            // 
            // radioButtonNroDocRef
            // 
            this.radioButtonNroDocRef.AutoSize = true;
            this.radioButtonNroDocRef.Location = new System.Drawing.Point(207, 46);
            this.radioButtonNroDocRef.Name = "radioButtonNroDocRef";
            this.radioButtonNroDocRef.Size = new System.Drawing.Size(94, 17);
            this.radioButtonNroDocRef.TabIndex = 20;
            this.radioButtonNroDocRef.Text = "Nro. Doc. Ref.";
            this.radioButtonNroDocRef.UseVisualStyleBackColor = true;
            this.radioButtonNroDocRef.CheckedChanged += new System.EventHandler(this.radioButtonNroDocRef_CheckedChanged);
            // 
            // dgvData
            // 
            this.dgvData.AllowUserToAddRows = false;
            this.dgvData.AllowUserToDeleteRows = false;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvData.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.dgvData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvData.DefaultCellStyle = dataGridViewCellStyle5;
            this.dgvData.Location = new System.Drawing.Point(0, 59);
            this.dgvData.Name = "dgvData";
            this.dgvData.ReadOnly = true;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvData.RowHeadersDefaultCellStyle = dataGridViewCellStyle6;
            this.dgvData.Size = new System.Drawing.Size(926, 352);
            this.dgvData.TabIndex = 25;
            this.dgvData.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgvData_CellFormatting);
            // 
            // cmbComedor
            // 
            this.cmbComedor.BackColor = System.Drawing.SystemColors.HighlightText;
            this.cmbComedor.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbComedor.FormattingEnabled = true;
            this.cmbComedor.Location = new System.Drawing.Point(89, 15);
            this.cmbComedor.Name = "cmbComedor";
            this.cmbComedor.Size = new System.Drawing.Size(265, 21);
            this.cmbComedor.TabIndex = 62;
            this.cmbComedor.SelectedIndexChanged += new System.EventHandler(this.cmbAlmacen_SelectedIndexChanged);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(28, 18);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(55, 13);
            this.label11.TabIndex = 63;
            this.label11.Text = "Comedor :";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(381, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(73, 13);
            this.label1.TabIndex = 66;
            this.label1.Text = "Fecha Inicial :";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(589, 19);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(68, 13);
            this.label2.TabIndex = 67;
            this.label2.Text = "Fecha Final :";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtFechaFin);
            this.groupBox1.Controls.Add(this.txtFechaIni);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.cmbComedor);
            this.groupBox1.Controls.Add(this.label11);
            this.groupBox1.Location = new System.Drawing.Point(12, 5);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(902, 48);
            this.groupBox1.TabIndex = 68;
            this.groupBox1.TabStop = false;
            this.groupBox1.Enter += new System.EventHandler(this.groupBox1_Enter);
            // 
            // txtFechaFin
            // 
            this.txtFechaFin.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.txtFechaFin.Location = new System.Drawing.Point(668, 15);
            this.txtFechaFin.Name = "txtFechaFin";
            this.txtFechaFin.Size = new System.Drawing.Size(95, 20);
            this.txtFechaFin.TabIndex = 69;
            this.txtFechaFin.ValueChanged += new System.EventHandler(this.txtFechaFin_ValueChanged);
            this.txtFechaFin.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtFechaFin_KeyPress);
            // 
            // txtFechaIni
            // 
            this.txtFechaIni.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.txtFechaIni.Location = new System.Drawing.Point(460, 16);
            this.txtFechaIni.Name = "txtFechaIni";
            this.txtFechaIni.Size = new System.Drawing.Size(95, 20);
            this.txtFechaIni.TabIndex = 68;
            this.txtFechaIni.ValueChanged += new System.EventHandler(this.txtFechaIni_ValueChanged);
            this.txtFechaIni.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtFechaIni_KeyPress);
            // 
            // ui_almov
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(926, 537);
            this.ControlBox = false;
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.toolstripform);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.dgvData);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(942, 576);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(942, 576);
            this.Name = "ui_almov";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Ingreso y Consumo de Insumos de Comedores";
            this.Load += new System.EventHandler(this.ui_almov_Load);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.toolstripform.ResumeLayout(false);
            this.toolstripform.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvData)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtRegEncontrados;
        private System.Windows.Forms.TextBox txtRegTotal;
        private System.Windows.Forms.RadioButton radioButtonCodigo;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button btnTodos;
        private System.Windows.Forms.TextBox txtBuscar;
        private System.Windows.Forms.RadioButton radioButtonGlosa;
        private System.Windows.Forms.ToolStripButton btnSalir;
        private System.Windows.Forms.ToolStripButton btnNuevo;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStrip toolstripform;
        private System.Windows.Forms.ToolStripButton btnEditar;
        public System.Windows.Forms.ToolStripButton btnExcel;
        public System.Windows.Forms.ToolStripButton btnActualizar;
        private System.Windows.Forms.RadioButton radioButtonRuc;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.DataGridView dgvData;
        private System.Windows.Forms.ComboBox cmbComedor;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox1;
        public System.Windows.Forms.ToolStripButton btnImprimir;
        private System.Windows.Forms.ToolStripButton btnEliminar;
        private System.Windows.Forms.ToolStripButton btnDesFina;
        private System.Windows.Forms.RadioButton radioButtonNroDocRef;
        private System.Windows.Forms.DateTimePicker txtFechaIni;
        private System.Windows.Forms.DateTimePicker txtFechaFin;
    }
}