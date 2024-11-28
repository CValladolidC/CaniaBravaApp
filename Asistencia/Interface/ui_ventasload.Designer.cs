namespace CaniaBrava
{
    partial class ui_ventasload
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cmbMes = new System.Windows.Forms.ComboBox();
            this.lblMes = new System.Windows.Forms.Label();
            this.lblDato = new System.Windows.Forms.Label();
            this.lblTexto = new System.Windows.Forms.Label();
            this.loadingNext1 = new System.Windows.Forms.PictureBox();
            this.cmbtipo = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtanio = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnSalir = new System.Windows.Forms.Button();
            this.pgbAvance = new System.Windows.Forms.ProgressBar();
            this.btnCargar = new System.Windows.Forms.Button();
            this.btnUrl = new System.Windows.Forms.Button();
            this.txturl = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.loadingNext1)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cmbMes);
            this.groupBox1.Controls.Add(this.lblMes);
            this.groupBox1.Controls.Add(this.lblDato);
            this.groupBox1.Controls.Add(this.lblTexto);
            this.groupBox1.Controls.Add(this.loadingNext1);
            this.groupBox1.Controls.Add(this.cmbtipo);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.txtanio);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(633, 81);
            this.groupBox1.TabIndex = 1;
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
            this.cmbMes.Location = new System.Drawing.Point(216, 46);
            this.cmbMes.Name = "cmbMes";
            this.cmbMes.Size = new System.Drawing.Size(96, 21);
            this.cmbMes.TabIndex = 120;
            this.cmbMes.Visible = false;
            // 
            // lblMes
            // 
            this.lblMes.AutoSize = true;
            this.lblMes.Location = new System.Drawing.Point(157, 49);
            this.lblMes.Name = "lblMes";
            this.lblMes.Size = new System.Drawing.Size(36, 13);
            this.lblMes.TabIndex = 119;
            this.lblMes.Text = "Mes : ";
            this.lblMes.Visible = false;
            // 
            // lblDato
            // 
            this.lblDato.AutoSize = true;
            this.lblDato.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDato.Location = new System.Drawing.Point(285, 22);
            this.lblDato.Name = "lblDato";
            this.lblDato.Size = new System.Drawing.Size(0, 13);
            this.lblDato.TabIndex = 118;
            // 
            // lblTexto
            // 
            this.lblTexto.AutoSize = true;
            this.lblTexto.Location = new System.Drawing.Point(139, 22);
            this.lblTexto.Name = "lblTexto";
            this.lblTexto.Size = new System.Drawing.Size(146, 13);
            this.lblTexto.TabIndex = 117;
            this.lblTexto.Text = "Se va a proceder a generar : ";
            this.lblTexto.Visible = false;
            // 
            // loadingNext1
            // 
            this.loadingNext1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.loadingNext1.BackColor = System.Drawing.Color.Transparent;
            this.loadingNext1.ErrorImage = null;
            this.loadingNext1.Image = global::CaniaBrava.Properties.Resources.loading;
            this.loadingNext1.Location = new System.Drawing.Point(580, 13);
            this.loadingNext1.MaximumSize = new System.Drawing.Size(47, 42);
            this.loadingNext1.MinimumSize = new System.Drawing.Size(47, 42);
            this.loadingNext1.Name = "loadingNext1";
            this.loadingNext1.Size = new System.Drawing.Size(47, 42);
            this.loadingNext1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.loadingNext1.TabIndex = 116;
            this.loadingNext1.TabStop = false;
            this.loadingNext1.Visible = false;
            // 
            // cmbtipo
            // 
            this.cmbtipo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbtipo.FormattingEnabled = true;
            this.cmbtipo.Items.AddRange(new object[] {
            "PB",
            "PY"});
            this.cmbtipo.Location = new System.Drawing.Point(63, 19);
            this.cmbtipo.Name = "cmbtipo";
            this.cmbtipo.Size = new System.Drawing.Size(70, 21);
            this.cmbtipo.TabIndex = 113;
            this.cmbtipo.SelectedIndexChanged += new System.EventHandler(this.cmbtipo_SelectedIndexChanged);
            this.cmbtipo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cmbtipo_KeyPress);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(5, 22);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(37, 13);
            this.label3.TabIndex = 112;
            this.label3.Text = "Tipo : ";
            // 
            // txtanio
            // 
            this.txtanio.Location = new System.Drawing.Point(63, 46);
            this.txtanio.MaxLength = 4;
            this.txtanio.Name = "txtanio";
            this.txtanio.Size = new System.Drawing.Size(70, 20);
            this.txtanio.TabIndex = 1;
            this.txtanio.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtanio_KeyPress);
            this.txtanio.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtanio_KeyUp);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(5, 49);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Año : ";
            // 
            // btnSalir
            // 
            this.btnSalir.Location = new System.Drawing.Point(315, 49);
            this.btnSalir.Name = "btnSalir";
            this.btnSalir.Size = new System.Drawing.Size(92, 51);
            this.btnSalir.TabIndex = 115;
            this.btnSalir.Text = "Salir";
            this.btnSalir.UseVisualStyleBackColor = true;
            this.btnSalir.Click += new System.EventHandler(this.btnSalir_Click);
            // 
            // pgbAvance
            // 
            this.pgbAvance.Location = new System.Drawing.Point(581, 77);
            this.pgbAvance.Name = "pgbAvance";
            this.pgbAvance.Size = new System.Drawing.Size(47, 23);
            this.pgbAvance.TabIndex = 114;
            this.pgbAvance.Visible = false;
            // 
            // btnCargar
            // 
            this.btnCargar.Location = new System.Drawing.Point(216, 49);
            this.btnCargar.Name = "btnCargar";
            this.btnCargar.Size = new System.Drawing.Size(92, 51);
            this.btnCargar.TabIndex = 5;
            this.btnCargar.Text = "Cargar Excel";
            this.btnCargar.UseVisualStyleBackColor = true;
            this.btnCargar.Click += new System.EventHandler(this.btnCargar_Click);
            // 
            // btnUrl
            // 
            this.btnUrl.Location = new System.Drawing.Point(598, 21);
            this.btnUrl.Name = "btnUrl";
            this.btnUrl.Size = new System.Drawing.Size(29, 23);
            this.btnUrl.TabIndex = 4;
            this.btnUrl.Text = "...";
            this.btnUrl.UseVisualStyleBackColor = true;
            this.btnUrl.Click += new System.EventHandler(this.btnUrl_Click);
            // 
            // txturl
            // 
            this.txturl.Location = new System.Drawing.Point(64, 23);
            this.txturl.Name = "txturl";
            this.txturl.ReadOnly = true;
            this.txturl.Size = new System.Drawing.Size(533, 20);
            this.txturl.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 26);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(52, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Archivo : ";
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.WorkerReportsProgress = true;
            this.backgroundWorker1.WorkerSupportsCancellation = true;
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
            this.backgroundWorker1.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundWorker1_ProgressChanged);
            this.backgroundWorker1.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker1_RunWorkerCompleted);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.txturl);
            this.groupBox2.Controls.Add(this.btnUrl);
            this.groupBox2.Controls.Add(this.btnCargar);
            this.groupBox2.Controls.Add(this.pgbAvance);
            this.groupBox2.Controls.Add(this.btnSalir);
            this.groupBox2.Location = new System.Drawing.Point(12, 99);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(633, 110);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            // 
            // ui_controllingload
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(657, 221);
            this.ControlBox = false;
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.MaximumSize = new System.Drawing.Size(673, 260);
            this.MinimumSize = new System.Drawing.Size(673, 260);
            this.Name = "ui_controllingload";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Cargar Informacion PB o PY";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.loadingNext1)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnCargar;
        private System.Windows.Forms.Button btnUrl;
        private System.Windows.Forms.TextBox txturl;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtanio;
        private System.Windows.Forms.Label label1;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.ComboBox cmbtipo;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ProgressBar pgbAvance;
        private System.Windows.Forms.Button btnSalir;
        private System.Windows.Forms.PictureBox loadingNext1;
        private System.Windows.Forms.Label lblTexto;
        private System.Windows.Forms.Label lblDato;
        private System.Windows.Forms.ComboBox cmbMes;
        private System.Windows.Forms.Label lblMes;
        private System.Windows.Forms.GroupBox groupBox2;
    }
}