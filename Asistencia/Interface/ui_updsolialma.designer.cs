namespace CaniaBrava
{
    partial class ui_updsolialma
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.tabControl = new Dotnetrix.Controls.TabControlEX();
            this.tabPageEX1 = new Dotnetrix.Controls.TabPageEX();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtEstado = new System.Windows.Forms.TextBox();
            this.txtUsuario = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.txtRucProvee = new System.Windows.Forms.TextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.btnAddProvee = new System.Windows.Forms.Button();
            this.txtProveedor = new System.Windows.Forms.TextBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.label20 = new System.Windows.Forms.Label();
            this.txtTitulo = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtHora = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtDscProveedor = new System.Windows.Forms.TextBox();
            this.txtGlosa3 = new System.Windows.Forms.TextBox();
            this.txtGlosa2 = new System.Windows.Forms.TextBox();
            this.txtGlosa1 = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.txtFecha = new System.Windows.Forms.MaskedTextBox();
            this.cmbSolicitante = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.lblRazon = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txtCodigo = new System.Windows.Forms.MaskedTextBox();
            this.lblUsuario = new System.Windows.Forms.Label();
            this.tabPageEX2 = new Dotnetrix.Controls.TabPageEX();
            this.btnEliminar = new System.Windows.Forms.Button();
            this.btnEditar = new System.Windows.Forms.Button();
            this.btnNuevo = new System.Windows.Forms.Button();
            this.dgvData = new System.Windows.Forms.DataGridView();
            this.toolStripForm = new System.Windows.Forms.ToolStrip();
            this.btnAceptar = new System.Windows.Forms.ToolStripButton();
            this.btnFinaliza = new System.Windows.Forms.ToolStripButton();
            this.btnCancelar = new System.Windows.Forms.ToolStripButton();
            this.tabControl.SuspendLayout();
            this.tabPageEX1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
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
            this.tabControl.Location = new System.Drawing.Point(10, 10);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 1;
            this.tabControl.SelectedTabFontStyle = System.Drawing.FontStyle.Regular;
            this.tabControl.Size = new System.Drawing.Size(596, 339);
            this.tabControl.TabIndex = 0;
            this.tabControl.UseVisualStyles = false;
            // 
            // tabPageEX1
            // 
            this.tabPageEX1.BackColor = System.Drawing.SystemColors.HighlightText;
            this.tabPageEX1.Controls.Add(this.groupBox1);
            this.tabPageEX1.Controls.Add(this.groupBox3);
            this.tabPageEX1.Controls.Add(this.groupBox2);
            this.tabPageEX1.Location = new System.Drawing.Point(4, 25);
            this.tabPageEX1.Name = "tabPageEX1";
            this.tabPageEX1.Size = new System.Drawing.Size(588, 310);
            this.tabPageEX1.TabIndex = 0;
            this.tabPageEX1.Text = "Información General";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtEstado);
            this.groupBox1.Controls.Add(this.txtUsuario);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Location = new System.Drawing.Point(16, 240);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(557, 46);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            // 
            // txtEstado
            // 
            this.txtEstado.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.txtEstado.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtEstado.Enabled = false;
            this.txtEstado.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtEstado.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtEstado.Location = new System.Drawing.Point(388, 17);
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
            this.txtUsuario.Location = new System.Drawing.Point(127, 16);
            this.txtUsuario.MaxLength = 120;
            this.txtUsuario.Name = "txtUsuario";
            this.txtUsuario.Size = new System.Drawing.Size(141, 20);
            this.txtUsuario.TabIndex = 3;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(14, 20);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(82, 13);
            this.label7.TabIndex = 60;
            this.label7.Text = "Registrado por :";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(334, 20);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(46, 13);
            this.label2.TabIndex = 58;
            this.label2.Text = "Estado :";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.txtRucProvee);
            this.groupBox3.Controls.Add(this.label16);
            this.groupBox3.Controls.Add(this.btnAddProvee);
            this.groupBox3.Controls.Add(this.txtProveedor);
            this.groupBox3.Controls.Add(this.pictureBox2);
            this.groupBox3.Controls.Add(this.label20);
            this.groupBox3.Controls.Add(this.txtTitulo);
            this.groupBox3.Controls.Add(this.label5);
            this.groupBox3.Controls.Add(this.txtHora);
            this.groupBox3.Controls.Add(this.label1);
            this.groupBox3.Controls.Add(this.txtDscProveedor);
            this.groupBox3.Controls.Add(this.txtGlosa3);
            this.groupBox3.Controls.Add(this.txtGlosa2);
            this.groupBox3.Controls.Add(this.txtGlosa1);
            this.groupBox3.Controls.Add(this.label14);
            this.groupBox3.Controls.Add(this.label9);
            this.groupBox3.Controls.Add(this.txtFecha);
            this.groupBox3.Controls.Add(this.cmbSolicitante);
            this.groupBox3.Controls.Add(this.label6);
            this.groupBox3.Controls.Add(this.lblRazon);
            this.groupBox3.Location = new System.Drawing.Point(16, 65);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(557, 169);
            this.groupBox3.TabIndex = 0;
            this.groupBox3.TabStop = false;
            // 
            // txtRucProvee
            // 
            this.txtRucProvee.BackColor = System.Drawing.SystemColors.Window;
            this.txtRucProvee.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtRucProvee.Enabled = false;
            this.txtRucProvee.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtRucProvee.Location = new System.Drawing.Point(409, 94);
            this.txtRucProvee.MaxLength = 20;
            this.txtRucProvee.Name = "txtRucProvee";
            this.txtRucProvee.Size = new System.Drawing.Size(120, 20);
            this.txtRucProvee.TabIndex = 136;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(358, 98);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(45, 13);
            this.label16.TabIndex = 135;
            this.label16.Text = "R.U.C. :";
            // 
            // btnAddProvee
            // 
            this.btnAddProvee.Image = global::CaniaBrava.Properties.Resources.NEW;
            this.btnAddProvee.Location = new System.Drawing.Point(299, 94);
            this.btnAddProvee.Name = "btnAddProvee";
            this.btnAddProvee.Size = new System.Drawing.Size(44, 23);
            this.btnAddProvee.TabIndex = 134;
            this.btnAddProvee.UseVisualStyleBackColor = true;
            this.btnAddProvee.Click += new System.EventHandler(this.btnAddProvee_Click);
            // 
            // txtProveedor
            // 
            this.txtProveedor.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtProveedor.Location = new System.Drawing.Point(156, 95);
            this.txtProveedor.MaxLength = 20;
            this.txtProveedor.Name = "txtProveedor";
            this.txtProveedor.Size = new System.Drawing.Size(141, 20);
            this.txtProveedor.TabIndex = 3;
            this.txtProveedor.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtProveedor_KeyDown);
            this.txtProveedor.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtProveedor_KeyPress);
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = global::CaniaBrava.Properties.Resources.FILE;
            this.pictureBox2.Location = new System.Drawing.Point(126, 95);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(24, 21);
            this.pictureBox2.TabIndex = 96;
            this.pictureBox2.TabStop = false;
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label20.Location = new System.Drawing.Point(107, 99);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(21, 13);
            this.label20.TabIndex = 95;
            this.label20.Text = "F2";
            // 
            // txtTitulo
            // 
            this.txtTitulo.BackColor = System.Drawing.SystemColors.Window;
            this.txtTitulo.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtTitulo.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtTitulo.Location = new System.Drawing.Point(127, 63);
            this.txtTitulo.MaxLength = 120;
            this.txtTitulo.Name = "txtTitulo";
            this.txtTitulo.Size = new System.Drawing.Size(402, 20);
            this.txtTitulo.TabIndex = 0;
            this.txtTitulo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtTitulo_KeyPress);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(15, 67);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(42, 13);
            this.label5.TabIndex = 94;
            this.label5.Text = "Titulo  :";
            // 
            // txtHora
            // 
            this.txtHora.BackColor = System.Drawing.SystemColors.Window;
            this.txtHora.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtHora.Enabled = false;
            this.txtHora.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtHora.Location = new System.Drawing.Point(349, 31);
            this.txtHora.MaxLength = 120;
            this.txtHora.Name = "txtHora";
            this.txtHora.Size = new System.Drawing.Size(180, 20);
            this.txtHora.TabIndex = 91;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(305, 35);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(36, 13);
            this.label1.TabIndex = 92;
            this.label1.Text = "Hora :";
            // 
            // txtDscProveedor
            // 
            this.txtDscProveedor.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtDscProveedor.Enabled = false;
            this.txtDscProveedor.Location = new System.Drawing.Point(126, 126);
            this.txtDscProveedor.MaxLength = 20;
            this.txtDscProveedor.Name = "txtDscProveedor";
            this.txtDscProveedor.Size = new System.Drawing.Size(403, 20);
            this.txtDscProveedor.TabIndex = 137;
            this.txtDscProveedor.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtNomRec_KeyPress_1);
            // 
            // txtGlosa3
            // 
            this.txtGlosa3.BackColor = System.Drawing.SystemColors.Window;
            this.txtGlosa3.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtGlosa3.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtGlosa3.Location = new System.Drawing.Point(533, 60);
            this.txtGlosa3.MaxLength = 120;
            this.txtGlosa3.Name = "txtGlosa3";
            this.txtGlosa3.Size = new System.Drawing.Size(24, 20);
            this.txtGlosa3.TabIndex = 6;
            this.txtGlosa3.Visible = false;
            this.txtGlosa3.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtGlosa3_KeyPress);
            // 
            // txtGlosa2
            // 
            this.txtGlosa2.BackColor = System.Drawing.SystemColors.Window;
            this.txtGlosa2.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtGlosa2.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtGlosa2.Location = new System.Drawing.Point(533, 36);
            this.txtGlosa2.MaxLength = 120;
            this.txtGlosa2.Name = "txtGlosa2";
            this.txtGlosa2.Size = new System.Drawing.Size(24, 20);
            this.txtGlosa2.TabIndex = 5;
            this.txtGlosa2.Visible = false;
            this.txtGlosa2.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtGlosa2_KeyPress);
            // 
            // txtGlosa1
            // 
            this.txtGlosa1.BackColor = System.Drawing.SystemColors.Window;
            this.txtGlosa1.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtGlosa1.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtGlosa1.Location = new System.Drawing.Point(533, 12);
            this.txtGlosa1.MaxLength = 120;
            this.txtGlosa1.Name = "txtGlosa1";
            this.txtGlosa1.Size = new System.Drawing.Size(24, 20);
            this.txtGlosa1.TabIndex = 4;
            this.txtGlosa1.Visible = false;
            this.txtGlosa1.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtGlosa1_KeyPress);
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(478, 15);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(40, 13);
            this.label14.TabIndex = 90;
            this.label14.Text = "Glosa :";
            this.label14.Visible = false;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(14, 34);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(46, 13);
            this.label9.TabIndex = 81;
            this.label9.Text = "Fecha  :";
            // 
            // txtFecha
            // 
            this.txtFecha.BackColor = System.Drawing.SystemColors.Window;
            this.txtFecha.Enabled = false;
            this.txtFecha.Location = new System.Drawing.Point(126, 30);
            this.txtFecha.Mask = "0000-00-00";
            this.txtFecha.Name = "txtFecha";
            this.txtFecha.Size = new System.Drawing.Size(171, 20);
            this.txtFecha.TabIndex = 0;
            // 
            // cmbSolicitante
            // 
            this.cmbSolicitante.BackColor = System.Drawing.SystemColors.Window;
            this.cmbSolicitante.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSolicitante.FormattingEnabled = true;
            this.cmbSolicitante.Location = new System.Drawing.Point(531, 87);
            this.cmbSolicitante.Name = "cmbSolicitante";
            this.cmbSolicitante.Size = new System.Drawing.Size(89, 21);
            this.cmbSolicitante.TabIndex = 1;
            this.cmbSolicitante.Visible = false;
            this.cmbSolicitante.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cmbSolicitante_KeyPress);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(14, 99);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(87, 13);
            this.label6.TabIndex = 65;
            this.label6.Text = "Cod. Proveedor :";
            // 
            // lblRazon
            // 
            this.lblRazon.AutoSize = true;
            this.lblRazon.Location = new System.Drawing.Point(14, 130);
            this.lblRazon.Name = "lblRazon";
            this.lblRazon.Size = new System.Drawing.Size(76, 13);
            this.lblRazon.TabIndex = 43;
            this.lblRazon.Text = "Razon Social :";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.txtCodigo);
            this.groupBox2.Controls.Add(this.lblUsuario);
            this.groupBox2.Location = new System.Drawing.Point(16, 18);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(557, 41);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            // 
            // txtCodigo
            // 
            this.txtCodigo.BackColor = System.Drawing.SystemColors.HighlightText;
            this.txtCodigo.Enabled = false;
            this.txtCodigo.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtCodigo.Location = new System.Drawing.Point(126, 12);
            this.txtCodigo.Mask = "999999";
            this.txtCodigo.Name = "txtCodigo";
            this.txtCodigo.PromptChar = ' ';
            this.txtCodigo.Size = new System.Drawing.Size(170, 20);
            this.txtCodigo.TabIndex = 1;
            this.txtCodigo.ValidatingType = typeof(int);
            // 
            // lblUsuario
            // 
            this.lblUsuario.AutoSize = true;
            this.lblUsuario.Location = new System.Drawing.Point(14, 16);
            this.lblUsuario.Name = "lblUsuario";
            this.lblUsuario.Size = new System.Drawing.Size(46, 13);
            this.lblUsuario.TabIndex = 12;
            this.lblUsuario.Text = "Código :";
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
            this.tabPageEX2.Size = new System.Drawing.Size(588, 310);
            this.tabPageEX2.TabIndex = 1;
            this.tabPageEX2.Text = "Detalle de la Solicitud";
            // 
            // btnEliminar
            // 
            this.btnEliminar.Image = global::CaniaBrava.Properties.Resources.DELETE;
            this.btnEliminar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnEliminar.Location = new System.Drawing.Point(508, 283);
            this.btnEliminar.Name = "btnEliminar";
            this.btnEliminar.Size = new System.Drawing.Size(75, 23);
            this.btnEliminar.TabIndex = 12;
            this.btnEliminar.Text = "      Eliminar";
            this.btnEliminar.UseVisualStyleBackColor = true;
            this.btnEliminar.Click += new System.EventHandler(this.btnEliminar_Click);
            // 
            // btnEditar
            // 
            this.btnEditar.Image = global::CaniaBrava.Properties.Resources.REPORT;
            this.btnEditar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnEditar.Location = new System.Drawing.Point(427, 283);
            this.btnEditar.Name = "btnEditar";
            this.btnEditar.Size = new System.Drawing.Size(75, 23);
            this.btnEditar.TabIndex = 11;
            this.btnEditar.Text = "    Editar";
            this.btnEditar.UseVisualStyleBackColor = true;
            this.btnEditar.Click += new System.EventHandler(this.btnEditar_Click);
            // 
            // btnNuevo
            // 
            this.btnNuevo.Image = global::CaniaBrava.Properties.Resources.NEW;
            this.btnNuevo.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnNuevo.Location = new System.Drawing.Point(346, 283);
            this.btnNuevo.Name = "btnNuevo";
            this.btnNuevo.Size = new System.Drawing.Size(75, 23);
            this.btnNuevo.TabIndex = 10;
            this.btnNuevo.Text = "      Nuevo";
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
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvData.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvData.Location = new System.Drawing.Point(3, 3);
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
            this.dgvData.Size = new System.Drawing.Size(582, 275);
            this.dgvData.TabIndex = 9;
            // 
            // toolStripForm
            // 
            this.toolStripForm.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.toolStripForm.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.toolStripForm.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnAceptar,
            this.btnFinaliza,
            this.btnCancelar});
            this.toolStripForm.Location = new System.Drawing.Point(0, 353);
            this.toolStripForm.Name = "toolStripForm";
            this.toolStripForm.ShowItemToolTips = false;
            this.toolStripForm.Size = new System.Drawing.Size(616, 32);
            this.toolStripForm.TabIndex = 1;
            // 
            // btnAceptar
            // 
            this.btnAceptar.Image = global::CaniaBrava.Properties.Resources.SAVE;
            this.btnAceptar.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnAceptar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnAceptar.Name = "btnAceptar";
            this.btnAceptar.Size = new System.Drawing.Size(74, 29);
            this.btnAceptar.Text = "Aceptar";
            this.btnAceptar.Click += new System.EventHandler(this.btnAceptar_Click);
            // 
            // btnFinaliza
            // 
            this.btnFinaliza.Image = global::CaniaBrava.Properties.Resources.FINALIZA;
            this.btnFinaliza.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnFinaliza.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnFinaliza.Name = "btnFinaliza";
            this.btnFinaliza.Size = new System.Drawing.Size(75, 29);
            this.btnFinaliza.Text = "Finalizar";
            this.btnFinaliza.Click += new System.EventHandler(this.btnFinaliza_Click);
            // 
            // btnCancelar
            // 
            this.btnCancelar.Image = global::CaniaBrava.Properties.Resources.CLOSE;
            this.btnCancelar.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnCancelar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(51, 29);
            this.btnCancelar.Text = "Salir";
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // ui_updsolialma
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.HighlightText;
            this.ClientSize = new System.Drawing.Size(616, 385);
            this.Controls.Add(this.tabControl);
            this.Controls.Add(this.toolStripForm);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(632, 424);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(632, 424);
            this.Name = "ui_updsolialma";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Actualización de Datos";
            this.tabControl.ResumeLayout(false);
            this.tabPageEX1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
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
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TextBox txtDscProveedor;
        private System.Windows.Forms.TextBox txtGlosa3;
        private System.Windows.Forms.TextBox txtGlosa2;
        private System.Windows.Forms.TextBox txtGlosa1;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.MaskedTextBox txtFecha;
        private System.Windows.Forms.ComboBox cmbSolicitante;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label lblRazon;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.MaskedTextBox txtCodigo;
        private System.Windows.Forms.Label lblUsuario;
        private Dotnetrix.Controls.TabPageEX tabPageEX2;
        private System.Windows.Forms.Button btnEliminar;
        private System.Windows.Forms.Button btnEditar;
        private System.Windows.Forms.Button btnNuevo;
        private System.Windows.Forms.DataGridView dgvData;
        private System.Windows.Forms.ToolStrip toolStripForm;
        private System.Windows.Forms.ToolStripButton btnAceptar;
        private System.Windows.Forms.ToolStripButton btnFinaliza;
        private System.Windows.Forms.ToolStripButton btnCancelar;
        private System.Windows.Forms.TextBox txtHora;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtTitulo;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Label label20;
        public System.Windows.Forms.TextBox txtProveedor;
        private System.Windows.Forms.Button btnAddProvee;
        private System.Windows.Forms.TextBox txtRucProvee;
        private System.Windows.Forms.Label label16;
    }
}