namespace CaniaBrava
{
    partial class ui_updsalcenpro
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
            this.tabControl = new Dotnetrix.Controls.TabControlEX();
            this.tabPageEX1 = new Dotnetrix.Controls.TabPageEX();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtEstado = new System.Windows.Forms.TextBox();
            this.txtUsuario = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtFecMod = new System.Windows.Forms.MaskedTextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtFecCrea = new System.Windows.Forms.MaskedTextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.txtNomRec = new System.Windows.Forms.TextBox();
            this.txtGlosa3 = new System.Windows.Forms.TextBox();
            this.txtGlosa2 = new System.Windows.Forms.TextBox();
            this.txtGlosa1 = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.cmbCenCos = new System.Windows.Forms.ComboBox();
            this.label12 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.txtFecha = new System.Windows.Forms.MaskedTextBox();
            this.cmbMov = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.lblRazon = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.cmbTipo = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtNumDoc = new System.Windows.Forms.MaskedTextBox();
            this.lblUsuario = new System.Windows.Forms.Label();
            this.tabPageEX2 = new Dotnetrix.Controls.TabPageEX();
            this.btnEliminar = new System.Windows.Forms.Button();
            this.btnEditar = new System.Windows.Forms.Button();
            this.btnNuevo = new System.Windows.Forms.Button();
            this.dgvData = new System.Windows.Forms.DataGridView();
            this.toolStripForm = new System.Windows.Forms.ToolStrip();
            this.btnAceptar = new System.Windows.Forms.ToolStripButton();
            this.btnFinaliza = new System.Windows.Forms.ToolStripButton();
            this.btnImprimir = new System.Windows.Forms.ToolStripButton();
            this.btnCancelar = new System.Windows.Forms.ToolStripButton();
            this.tabControl.SuspendLayout();
            this.tabPageEX1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.tabPageEX2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvData)).BeginInit();
            this.toolStripForm.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl
            // 
            this.tabControl.Appearance = Dotnetrix.Controls.TabAppearanceEX.Bevel;
            this.tabControl.Controls.Add(this.tabPageEX1);
            this.tabControl.Controls.Add(this.tabPageEX2);
            this.tabControl.Location = new System.Drawing.Point(6, 10);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.SelectedTabFontStyle = System.Drawing.FontStyle.Regular;
            this.tabControl.Size = new System.Drawing.Size(880, 430);
            this.tabControl.TabIndex = 2;
            this.tabControl.UseVisualStyles = false;
            // 
            // tabPageEX1
            // 
            this.tabPageEX1.BackColor = System.Drawing.SystemColors.Control;
            this.tabPageEX1.Controls.Add(this.groupBox1);
            this.tabPageEX1.Controls.Add(this.groupBox3);
            this.tabPageEX1.Controls.Add(this.groupBox2);
            this.tabPageEX1.Location = new System.Drawing.Point(4, 25);
            this.tabPageEX1.Name = "tabPageEX1";
            this.tabPageEX1.Size = new System.Drawing.Size(872, 401);
            this.tabPageEX1.TabIndex = 0;
            this.tabPageEX1.Text = "Información General";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtEstado);
            this.groupBox1.Controls.Add(this.txtUsuario);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.txtFecMod);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.txtFecCrea);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Location = new System.Drawing.Point(35, 299);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(620, 67);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            // 
            // txtEstado
            // 
            this.txtEstado.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.txtEstado.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtEstado.Enabled = false;
            this.txtEstado.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtEstado.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtEstado.Location = new System.Drawing.Point(385, 39);
            this.txtEstado.MaxLength = 120;
            this.txtEstado.Name = "txtEstado";
            this.txtEstado.Size = new System.Drawing.Size(141, 20);
            this.txtEstado.TabIndex = 5;
            // 
            // txtUsuario
            // 
            this.txtUsuario.BackColor = System.Drawing.SystemColors.HighlightText;
            this.txtUsuario.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtUsuario.Enabled = false;
            this.txtUsuario.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtUsuario.Location = new System.Drawing.Point(124, 39);
            this.txtUsuario.MaxLength = 120;
            this.txtUsuario.Name = "txtUsuario";
            this.txtUsuario.Size = new System.Drawing.Size(141, 20);
            this.txtUsuario.TabIndex = 3;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(14, 43);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(82, 13);
            this.label7.TabIndex = 60;
            this.label7.Text = "Registrado por :";
            // 
            // txtFecMod
            // 
            this.txtFecMod.BackColor = System.Drawing.SystemColors.HighlightText;
            this.txtFecMod.Enabled = false;
            this.txtFecMod.Location = new System.Drawing.Point(385, 13);
            this.txtFecMod.Mask = "00/00/0000";
            this.txtFecMod.Name = "txtFecMod";
            this.txtFecMod.Size = new System.Drawing.Size(141, 20);
            this.txtFecMod.TabIndex = 4;
            this.txtFecMod.ValidatingType = typeof(System.DateTime);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(274, 17);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 13);
            this.label3.TabIndex = 54;
            this.label3.Text = "F. Modifica :";
            // 
            // txtFecCrea
            // 
            this.txtFecCrea.BackColor = System.Drawing.SystemColors.HighlightText;
            this.txtFecCrea.Enabled = false;
            this.txtFecCrea.Location = new System.Drawing.Point(123, 13);
            this.txtFecCrea.Mask = "00/00/0000";
            this.txtFecCrea.Name = "txtFecCrea";
            this.txtFecCrea.Size = new System.Drawing.Size(141, 20);
            this.txtFecCrea.TabIndex = 2;
            this.txtFecCrea.ValidatingType = typeof(System.DateTime);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(14, 17);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(67, 13);
            this.label4.TabIndex = 52;
            this.label4.Text = "F. Creación :";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(274, 43);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(46, 13);
            this.label2.TabIndex = 58;
            this.label2.Text = "Estado :";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.txtNomRec);
            this.groupBox3.Controls.Add(this.txtGlosa3);
            this.groupBox3.Controls.Add(this.txtGlosa2);
            this.groupBox3.Controls.Add(this.txtGlosa1);
            this.groupBox3.Controls.Add(this.label14);
            this.groupBox3.Controls.Add(this.cmbCenCos);
            this.groupBox3.Controls.Add(this.label12);
            this.groupBox3.Controls.Add(this.label9);
            this.groupBox3.Controls.Add(this.txtFecha);
            this.groupBox3.Controls.Add(this.cmbMov);
            this.groupBox3.Controls.Add(this.label5);
            this.groupBox3.Controls.Add(this.lblRazon);
            this.groupBox3.Location = new System.Drawing.Point(34, 82);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(622, 208);
            this.groupBox3.TabIndex = 1;
            this.groupBox3.TabStop = false;
            // 
            // txtNomRec
            // 
            this.txtNomRec.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtNomRec.Location = new System.Drawing.Point(126, 70);
            this.txtNomRec.MaxLength = 20;
            this.txtNomRec.Name = "txtNomRec";
            this.txtNomRec.Size = new System.Drawing.Size(471, 20);
            this.txtNomRec.TabIndex = 8;
            // 
            // txtGlosa3
            // 
            this.txtGlosa3.BackColor = System.Drawing.SystemColors.Window;
            this.txtGlosa3.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtGlosa3.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtGlosa3.Location = new System.Drawing.Point(125, 169);
            this.txtGlosa3.MaxLength = 120;
            this.txtGlosa3.Name = "txtGlosa3";
            this.txtGlosa3.Size = new System.Drawing.Size(473, 20);
            this.txtGlosa3.TabIndex = 14;
            this.txtGlosa3.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtGlosa3_KeyPress);
            // 
            // txtGlosa2
            // 
            this.txtGlosa2.BackColor = System.Drawing.SystemColors.Window;
            this.txtGlosa2.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtGlosa2.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtGlosa2.Location = new System.Drawing.Point(125, 145);
            this.txtGlosa2.MaxLength = 120;
            this.txtGlosa2.Name = "txtGlosa2";
            this.txtGlosa2.Size = new System.Drawing.Size(473, 20);
            this.txtGlosa2.TabIndex = 13;
            this.txtGlosa2.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtGlosa2_KeyPress);
            // 
            // txtGlosa1
            // 
            this.txtGlosa1.BackColor = System.Drawing.SystemColors.Window;
            this.txtGlosa1.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtGlosa1.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtGlosa1.Location = new System.Drawing.Point(125, 121);
            this.txtGlosa1.MaxLength = 120;
            this.txtGlosa1.Name = "txtGlosa1";
            this.txtGlosa1.Size = new System.Drawing.Size(473, 20);
            this.txtGlosa1.TabIndex = 12;
            this.txtGlosa1.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtGlosa1_KeyPress);
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(14, 125);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(40, 13);
            this.label14.TabIndex = 90;
            this.label14.Text = "Glosa :";
            // 
            // cmbCenCos
            // 
            this.cmbCenCos.BackColor = System.Drawing.SystemColors.Window;
            this.cmbCenCos.FormattingEnabled = true;
            this.cmbCenCos.Location = new System.Drawing.Point(125, 95);
            this.cmbCenCos.Name = "cmbCenCos";
            this.cmbCenCos.Size = new System.Drawing.Size(473, 21);
            this.cmbCenCos.TabIndex = 9;
            this.cmbCenCos.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cmbCenCos_KeyPress);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(14, 99);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(89, 13);
            this.label12.TabIndex = 86;
            this.label12.Text = "Centro de Costo :";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(14, 24);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(46, 13);
            this.label9.TabIndex = 81;
            this.label9.Text = "Fecha  :";
            // 
            // txtFecha
            // 
            this.txtFecha.BackColor = System.Drawing.SystemColors.Window;
            this.txtFecha.Location = new System.Drawing.Point(125, 20);
            this.txtFecha.Mask = "00/00/0000";
            this.txtFecha.Name = "txtFecha";
            this.txtFecha.Size = new System.Drawing.Size(171, 20);
            this.txtFecha.TabIndex = 0;
            this.txtFecha.ValidatingType = typeof(System.DateTime);
            this.txtFecha.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtFecha_KeyPress);
            // 
            // cmbMov
            // 
            this.cmbMov.BackColor = System.Drawing.SystemColors.Window;
            this.cmbMov.FormattingEnabled = true;
            this.cmbMov.Location = new System.Drawing.Point(125, 44);
            this.cmbMov.Name = "cmbMov";
            this.cmbMov.Size = new System.Drawing.Size(171, 21);
            this.cmbMov.TabIndex = 1;
            this.cmbMov.SelectedIndexChanged += new System.EventHandler(this.cmbMov_SelectedIndexChanged);
            this.cmbMov.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cmbMov_KeyPress);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(14, 48);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(62, 13);
            this.label5.TabIndex = 79;
            this.label5.Text = "Cod. Mov. :";
            // 
            // lblRazon
            // 
            this.lblRazon.AutoSize = true;
            this.lblRazon.Location = new System.Drawing.Point(14, 74);
            this.lblRazon.Name = "lblRazon";
            this.lblRazon.Size = new System.Drawing.Size(101, 13);
            this.lblRazon.TabIndex = 43;
            this.lblRazon.Text = "Recepcionado por :";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.cmbTipo);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.txtNumDoc);
            this.groupBox2.Controls.Add(this.lblUsuario);
            this.groupBox2.Location = new System.Drawing.Point(32, 35);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(624, 41);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            // 
            // cmbTipo
            // 
            this.cmbTipo.BackColor = System.Drawing.SystemColors.HighlightText;
            this.cmbTipo.FormattingEnabled = true;
            this.cmbTipo.Items.AddRange(new object[] {
            "PS  PARTE DE SALIDA"});
            this.cmbTipo.Location = new System.Drawing.Point(126, 12);
            this.cmbTipo.Name = "cmbTipo";
            this.cmbTipo.Size = new System.Drawing.Size(171, 21);
            this.cmbTipo.TabIndex = 0;
            this.cmbTipo.SelectedIndexChanged += new System.EventHandler(this.cmbTipo_SelectedIndexChanged);
            this.cmbTipo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cmbTipo_KeyPress);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(15, 16);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(37, 13);
            this.label8.TabIndex = 81;
            this.label8.Text = "Tipo  :";
            // 
            // txtNumDoc
            // 
            this.txtNumDoc.BackColor = System.Drawing.SystemColors.HighlightText;
            this.txtNumDoc.Enabled = false;
            this.txtNumDoc.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtNumDoc.Location = new System.Drawing.Point(412, 12);
            this.txtNumDoc.Mask = "9999999999";
            this.txtNumDoc.Name = "txtNumDoc";
            this.txtNumDoc.PromptChar = ' ';
            this.txtNumDoc.Size = new System.Drawing.Size(187, 20);
            this.txtNumDoc.TabIndex = 1;
            this.txtNumDoc.ValidatingType = typeof(int);
            // 
            // lblUsuario
            // 
            this.lblUsuario.AutoSize = true;
            this.lblUsuario.Location = new System.Drawing.Point(317, 16);
            this.lblUsuario.Name = "lblUsuario";
            this.lblUsuario.Size = new System.Drawing.Size(76, 13);
            this.lblUsuario.TabIndex = 12;
            this.lblUsuario.Text = "Número Doc. :";
            // 
            // tabPageEX2
            // 
            this.tabPageEX2.Controls.Add(this.btnEliminar);
            this.tabPageEX2.Controls.Add(this.btnEditar);
            this.tabPageEX2.Controls.Add(this.btnNuevo);
            this.tabPageEX2.Controls.Add(this.dgvData);
            this.tabPageEX2.Enabled = false;
            this.tabPageEX2.Location = new System.Drawing.Point(4, 25);
            this.tabPageEX2.Name = "tabPageEX2";
            this.tabPageEX2.Size = new System.Drawing.Size(872, 401);
            this.tabPageEX2.TabIndex = 1;
            this.tabPageEX2.Text = "Detalle de la Operación";
            // 
            // btnEliminar
            // 
            this.btnEliminar.Location = new System.Drawing.Point(579, 293);
            this.btnEliminar.Name = "btnEliminar";
            this.btnEliminar.Size = new System.Drawing.Size(75, 23);
            this.btnEliminar.TabIndex = 12;
            this.btnEliminar.Text = "Eliminar";
            this.btnEliminar.UseVisualStyleBackColor = true;
            this.btnEliminar.Click += new System.EventHandler(this.btnEliminar_Click);
            // 
            // btnEditar
            // 
            this.btnEditar.Location = new System.Drawing.Point(480, 293);
            this.btnEditar.Name = "btnEditar";
            this.btnEditar.Size = new System.Drawing.Size(75, 23);
            this.btnEditar.TabIndex = 11;
            this.btnEditar.Text = "Visualizar";
            this.btnEditar.UseVisualStyleBackColor = true;
            this.btnEditar.Click += new System.EventHandler(this.btnEditar_Click);
            // 
            // btnNuevo
            // 
            this.btnNuevo.Location = new System.Drawing.Point(382, 293);
            this.btnNuevo.Name = "btnNuevo";
            this.btnNuevo.Size = new System.Drawing.Size(75, 23);
            this.btnNuevo.TabIndex = 10;
            this.btnNuevo.Text = "Nuevo";
            this.btnNuevo.UseVisualStyleBackColor = true;
            this.btnNuevo.Click += new System.EventHandler(this.btnNuevo_Click);
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
            this.dgvData.Location = new System.Drawing.Point(3, 13);
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
            this.dgvData.Size = new System.Drawing.Size(866, 270);
            this.dgvData.TabIndex = 9;
            // 
            // toolStripForm
            // 
            this.toolStripForm.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.toolStripForm.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.toolStripForm.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnAceptar,
            this.btnFinaliza,
            this.btnImprimir,
            this.btnCancelar});
            this.toolStripForm.Location = new System.Drawing.Point(0, 454);
            this.toolStripForm.Name = "toolStripForm";
            this.toolStripForm.ShowItemToolTips = false;
            this.toolStripForm.Size = new System.Drawing.Size(893, 33);
            this.toolStripForm.TabIndex = 3;
            // 
            // btnAceptar
            // 
            this.btnAceptar.Image = global::CaniaBrava.Properties.Resources.SAVE;
            this.btnAceptar.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnAceptar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnAceptar.Name = "btnAceptar";
            this.btnAceptar.Size = new System.Drawing.Size(74, 30);
            this.btnAceptar.Text = "Aceptar";
            this.btnAceptar.Click += new System.EventHandler(this.btnAceptar_Click);
            // 
            // btnFinaliza
            // 
            this.btnFinaliza.Image = global::CaniaBrava.Properties.Resources.FINALIZA;
            this.btnFinaliza.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnFinaliza.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnFinaliza.Name = "btnFinaliza";
            this.btnFinaliza.Size = new System.Drawing.Size(75, 30);
            this.btnFinaliza.Text = "Finalizar";
            this.btnFinaliza.Click += new System.EventHandler(this.btnFinaliza_Click);
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
            // btnCancelar
            // 
            this.btnCancelar.Image = global::CaniaBrava.Properties.Resources.CLOSE;
            this.btnCancelar.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnCancelar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(51, 30);
            this.btnCancelar.Text = "Salir";
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // ui_updsalcenpro
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(893, 487);
            this.Controls.Add(this.tabControl);
            this.Controls.Add(this.toolStripForm);
            this.Name = "ui_updsalcenpro";
            this.Text = "Salida por Centro Productivo";
            this.Load += new System.EventHandler(this.ui_updsalcenpro_Load);
            this.tabControl.ResumeLayout(false);
            this.tabPageEX1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.tabPageEX2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvData)).EndInit();
            this.toolStripForm.ResumeLayout(false);
            this.toolStripForm.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Dotnetrix.Controls.TabControlEX tabControl;
        private Dotnetrix.Controls.TabPageEX tabPageEX1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtEstado;
        private System.Windows.Forms.TextBox txtUsuario;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.MaskedTextBox txtFecMod;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.MaskedTextBox txtFecCrea;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TextBox txtNomRec;
        private System.Windows.Forms.TextBox txtGlosa3;
        private System.Windows.Forms.TextBox txtGlosa2;
        private System.Windows.Forms.TextBox txtGlosa1;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.ComboBox cmbCenCos;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.MaskedTextBox txtFecha;
        private System.Windows.Forms.ComboBox cmbMov;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lblRazon;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ComboBox cmbTipo;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.MaskedTextBox txtNumDoc;
        private System.Windows.Forms.Label lblUsuario;
        private Dotnetrix.Controls.TabPageEX tabPageEX2;
        private System.Windows.Forms.Button btnEliminar;
        private System.Windows.Forms.Button btnEditar;
        private System.Windows.Forms.Button btnNuevo;
        private System.Windows.Forms.DataGridView dgvData;
        private System.Windows.Forms.ToolStrip toolStripForm;
        private System.Windows.Forms.ToolStripButton btnAceptar;
        private System.Windows.Forms.ToolStripButton btnFinaliza;
        private System.Windows.Forms.ToolStripButton btnImprimir;
        private System.Windows.Forms.ToolStripButton btnCancelar;
    }
}
