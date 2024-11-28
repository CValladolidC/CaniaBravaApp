namespace CaniaBrava.Interface
{
    partial class ui_controllingcarga
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
            this.btnRegistrarData = new System.Windows.Forms.Button();
            this.btnMostrar = new System.Windows.Forms.Button();
            this.cboHojas = new System.Windows.Forms.ComboBox();
            this.dgvDatos = new System.Windows.Forms.DataGridView();
            this.btnBuscar = new System.Windows.Forms.Button();
            this.txtRuta = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cmbMes = new System.Windows.Forms.ComboBox();
            this.lblMes = new System.Windows.Forms.Label();
            this.lblDato = new System.Windows.Forms.Label();
            this.lblTexto = new System.Windows.Forms.Label();
            this.cmbtipo = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtanio = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.toolStripForm = new System.Windows.Forms.ToolStrip();
            this.btnSalir = new System.Windows.Forms.ToolStripButton();
            this.lblnreg = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDatos)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.toolStripForm.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnRegistrarData
            // 
            this.btnRegistrarData.Enabled = false;
            this.btnRegistrarData.Location = new System.Drawing.Point(571, 97);
            this.btnRegistrarData.Margin = new System.Windows.Forms.Padding(4);
            this.btnRegistrarData.Name = "btnRegistrarData";
            this.btnRegistrarData.Size = new System.Drawing.Size(225, 28);
            this.btnRegistrarData.TabIndex = 22;
            this.btnRegistrarData.Text = "Registrar Data";
            this.btnRegistrarData.UseVisualStyleBackColor = true;
            this.btnRegistrarData.EnabledChanged += new System.EventHandler(this.btnRegistrarData_EnabledChanged);
            this.btnRegistrarData.Click += new System.EventHandler(this.btnRegistrarData_Click);
            // 
            // btnMostrar
            // 
            this.btnMostrar.Location = new System.Drawing.Point(448, 97);
            this.btnMostrar.Margin = new System.Windows.Forms.Padding(4);
            this.btnMostrar.Name = "btnMostrar";
            this.btnMostrar.Size = new System.Drawing.Size(100, 28);
            this.btnMostrar.TabIndex = 21;
            this.btnMostrar.Text = "Mostrar";
            this.btnMostrar.UseVisualStyleBackColor = true;
            this.btnMostrar.EnabledChanged += new System.EventHandler(this.btnMostrar_EnabledChanged);
            this.btnMostrar.Click += new System.EventHandler(this.btnMostrar_Click);
            // 
            // cboHojas
            // 
            this.cboHojas.FormattingEnabled = true;
            this.cboHojas.Location = new System.Drawing.Point(149, 100);
            this.cboHojas.Margin = new System.Windows.Forms.Padding(4);
            this.cboHojas.Name = "cboHojas";
            this.cboHojas.Size = new System.Drawing.Size(289, 24);
            this.cboHojas.TabIndex = 20;
            // 
            // dgvDatos
            // 
            this.dgvDatos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDatos.Location = new System.Drawing.Point(17, 145);
            this.dgvDatos.Margin = new System.Windows.Forms.Padding(4);
            this.dgvDatos.Name = "dgvDatos";
            this.dgvDatos.Size = new System.Drawing.Size(1897, 530);
            this.dgvDatos.TabIndex = 19;
            // 
            // btnBuscar
            // 
            this.btnBuscar.Location = new System.Drawing.Point(365, 46);
            this.btnBuscar.Margin = new System.Windows.Forms.Padding(4);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(77, 28);
            this.btnBuscar.TabIndex = 18;
            this.btnBuscar.Text = "...";
            this.btnBuscar.UseVisualStyleBackColor = true;
            this.btnBuscar.Click += new System.EventHandler(this.btnBuscar_Click);
            // 
            // txtRuta
            // 
            this.txtRuta.Location = new System.Drawing.Point(68, 48);
            this.txtRuta.Margin = new System.Windows.Forms.Padding(4);
            this.txtRuta.Name = "txtRuta";
            this.txtRuta.Size = new System.Drawing.Size(288, 22);
            this.txtRuta.TabIndex = 17;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 100);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(125, 17);
            this.label2.TabIndex = 15;
            this.label2.Text = "Hoja Encontradas:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 52);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(42, 17);
            this.label1.TabIndex = 16;
            this.label1.Text = "Ruta:";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cmbMes);
            this.groupBox1.Controls.Add(this.lblMes);
            this.groupBox1.Controls.Add(this.lblDato);
            this.groupBox1.Controls.Add(this.lblTexto);
            this.groupBox1.Controls.Add(this.cmbtipo);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.txtanio);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Location = new System.Drawing.Point(1071, 26);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox1.Size = new System.Drawing.Size(844, 100);
            this.groupBox1.TabIndex = 23;
            this.groupBox1.TabStop = false;
            // 
            // cmbMes
            // 
            this.cmbMes.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbMes.FormattingEnabled = true;
            this.cmbMes.Items.AddRange(new object[] {
            "01 Enero",
            "02 Febrero",
            "03 Marzo",
            "04 Abril",
            "05 Mayo",
            "06 Junio",
            "07 Julio",
            "08 Agosto",
            "09 Septiembre",
            "10 Octubre",
            "11 Noviembre",
            "12 Diciembre"});
            this.cmbMes.Location = new System.Drawing.Point(288, 57);
            this.cmbMes.Margin = new System.Windows.Forms.Padding(4);
            this.cmbMes.Name = "cmbMes";
            this.cmbMes.Size = new System.Drawing.Size(127, 24);
            this.cmbMes.TabIndex = 120;
            this.cmbMes.Visible = false;
            // 
            // lblMes
            // 
            this.lblMes.AutoSize = true;
            this.lblMes.Location = new System.Drawing.Point(209, 60);
            this.lblMes.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblMes.Name = "lblMes";
            this.lblMes.Size = new System.Drawing.Size(46, 17);
            this.lblMes.TabIndex = 119;
            this.lblMes.Text = "Mes : ";
            this.lblMes.Visible = false;
            // 
            // lblDato
            // 
            this.lblDato.AutoSize = true;
            this.lblDato.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDato.Location = new System.Drawing.Point(380, 27);
            this.lblDato.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblDato.Name = "lblDato";
            this.lblDato.Size = new System.Drawing.Size(0, 17);
            this.lblDato.TabIndex = 118;
            // 
            // lblTexto
            // 
            this.lblTexto.AutoSize = true;
            this.lblTexto.Location = new System.Drawing.Point(185, 27);
            this.lblTexto.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTexto.Name = "lblTexto";
            this.lblTexto.Size = new System.Drawing.Size(195, 17);
            this.lblTexto.TabIndex = 117;
            this.lblTexto.Text = "Se va a proceder a generar : ";
            this.lblTexto.Visible = false;
            this.lblTexto.Click += new System.EventHandler(this.lblTexto_Click);
            // 
            // cmbtipo
            // 
            this.cmbtipo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbtipo.FormattingEnabled = true;
            this.cmbtipo.Items.AddRange(new object[] {
            "PB",
            "PY"});
            this.cmbtipo.Location = new System.Drawing.Point(84, 23);
            this.cmbtipo.Margin = new System.Windows.Forms.Padding(4);
            this.cmbtipo.Name = "cmbtipo";
            this.cmbtipo.Size = new System.Drawing.Size(92, 24);
            this.cmbtipo.TabIndex = 113;
            this.cmbtipo.SelectedIndexChanged += new System.EventHandler(this.cmbtipo_SelectedIndexChanged);
            this.cmbtipo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cmbtipo_KeyPress);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(7, 27);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(48, 17);
            this.label3.TabIndex = 112;
            this.label3.Text = "Tipo : ";
            // 
            // txtanio
            // 
            this.txtanio.Location = new System.Drawing.Point(84, 57);
            this.txtanio.Margin = new System.Windows.Forms.Padding(4);
            this.txtanio.MaxLength = 4;
            this.txtanio.Name = "txtanio";
            this.txtanio.Size = new System.Drawing.Size(92, 22);
            this.txtanio.TabIndex = 1;
            this.txtanio.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtanio_KeyPress);
            this.txtanio.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtanio_KeyUp);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(7, 60);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(45, 17);
            this.label4.TabIndex = 0;
            this.label4.Text = "Año : ";
            // 
            // toolStripForm
            // 
            this.toolStripForm.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.toolStripForm.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStripForm.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnSalir});
            this.toolStripForm.Location = new System.Drawing.Point(0, 684);
            this.toolStripForm.Name = "toolStripForm";
            this.toolStripForm.ShowItemToolTips = false;
            this.toolStripForm.Size = new System.Drawing.Size(1924, 27);
            this.toolStripForm.TabIndex = 24;
            // 
            // btnSalir
            // 
            this.btnSalir.Image = global::CaniaBrava.Properties.Resources.CLOSE;
            this.btnSalir.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSalir.Name = "btnSalir";
            this.btnSalir.Size = new System.Drawing.Size(62, 24);
            this.btnSalir.Text = "Salir";
            this.btnSalir.Click += new System.EventHandler(this.btnSalir_Click);
            // 
            // lblnreg
            // 
            this.lblnreg.AutoSize = true;
            this.lblnreg.Enabled = false;
            this.lblnreg.Font = new System.Drawing.Font("Microsoft Tai Le", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblnreg.Location = new System.Drawing.Point(567, 52);
            this.lblnreg.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblnreg.Name = "lblnreg";
            this.lblnreg.Size = new System.Drawing.Size(15, 22);
            this.lblnreg.TabIndex = 25;
            this.lblnreg.Text = " ";
            // 
            // ui_controllingcarga
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1924, 711);
            this.ControlBox = false;
            this.Controls.Add(this.lblnreg);
            this.Controls.Add(this.toolStripForm);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnRegistrarData);
            this.Controls.Add(this.btnMostrar);
            this.Controls.Add(this.cboHojas);
            this.Controls.Add(this.dgvDatos);
            this.Controls.Add(this.btnBuscar);
            this.Controls.Add(this.txtRuta);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ui_controllingcarga";
            this.Text = "Carga Masiva de Presupuestos y Proyecciones";
            this.Load += new System.EventHandler(this.ui_controllingcarga_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDatos)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.toolStripForm.ResumeLayout(false);
            this.toolStripForm.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnRegistrarData;
        private System.Windows.Forms.Button btnMostrar;
        private System.Windows.Forms.ComboBox cboHojas;
        private System.Windows.Forms.DataGridView dgvDatos;
        private System.Windows.Forms.Button btnBuscar;
        private System.Windows.Forms.TextBox txtRuta;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox cmbMes;
        private System.Windows.Forms.Label lblMes;
        private System.Windows.Forms.Label lblDato;
        private System.Windows.Forms.Label lblTexto;
        private System.Windows.Forms.ComboBox cmbtipo;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtanio;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ToolStrip toolStripForm;
        private System.Windows.Forms.ToolStripButton btnSalir;
        private System.Windows.Forms.Label lblnreg;
    }
}