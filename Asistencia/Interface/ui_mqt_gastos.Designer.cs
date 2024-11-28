namespace CaniaBrava.Interface
{
    partial class ui_mqt_gastos
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
            this.btnCargar = new System.Windows.Forms.Button();
            this.listBoxArchivos = new System.Windows.Forms.ListBox();
            this.btnRegData = new System.Windows.Forms.Button();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.lbPorcentaje = new System.Windows.Forms.Label();
            this.lbRuta = new System.Windows.Forms.Label();
            this.grpBox = new System.Windows.Forms.GroupBox();
            this.lbDato = new System.Windows.Forms.Label();
            this.cmbMes = new System.Windows.Forms.ComboBox();
            this.lbMes = new System.Windows.Forms.Label();
            this.lbTexto = new System.Windows.Forms.Label();
            this.txtAnio = new System.Windows.Forms.TextBox();
            this.cmbTipo = new System.Windows.Forms.ComboBox();
            this.lbAnio = new System.Windows.Forms.Label();
            this.lbTipo = new System.Windows.Forms.Label();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.listBoxRegMont = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.listBoxMntTotal = new System.Windows.Forms.ListBox();
            this.label2 = new System.Windows.Forms.Label();
            this.grpBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnCargar
            // 
            this.btnCargar.Location = new System.Drawing.Point(914, 311);
            this.btnCargar.Name = "btnCargar";
            this.btnCargar.Size = new System.Drawing.Size(162, 31);
            this.btnCargar.TabIndex = 0;
            this.btnCargar.Text = "Cargar Archivos";
            this.btnCargar.UseVisualStyleBackColor = true;
            this.btnCargar.Click += new System.EventHandler(this.btnCargar_Click);
            // 
            // listBoxArchivos
            // 
            this.listBoxArchivos.FormattingEnabled = true;
            this.listBoxArchivos.ItemHeight = 16;
            this.listBoxArchivos.Location = new System.Drawing.Point(43, 248);
            this.listBoxArchivos.Name = "listBoxArchivos";
            this.listBoxArchivos.ScrollAlwaysVisible = true;
            this.listBoxArchivos.Size = new System.Drawing.Size(518, 180);
            this.listBoxArchivos.TabIndex = 1;
            this.listBoxArchivos.SelectedIndexChanged += new System.EventHandler(this.listBoxArchivos_SelectedIndexChanged);
            // 
            // btnRegData
            // 
            this.btnRegData.Location = new System.Drawing.Point(923, 490);
            this.btnRegData.Name = "btnRegData";
            this.btnRegData.Size = new System.Drawing.Size(153, 31);
            this.btnRegData.TabIndex = 2;
            this.btnRegData.Text = "Registrar Data";
            this.btnRegData.UseVisualStyleBackColor = true;
            this.btnRegData.Click += new System.EventHandler(this.btnRegData_Click);
            // 
            // progressBar
            // 
            this.progressBar.Location = new System.Drawing.Point(43, 498);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(774, 23);
            this.progressBar.TabIndex = 3;
            // 
            // lbPorcentaje
            // 
            this.lbPorcentaje.AutoSize = true;
            this.lbPorcentaje.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbPorcentaje.Location = new System.Drawing.Point(823, 501);
            this.lbPorcentaje.Name = "lbPorcentaje";
            this.lbPorcentaje.Size = new System.Drawing.Size(30, 17);
            this.lbPorcentaje.TabIndex = 4;
            this.lbPorcentaje.Text = "0%";
            // 
            // lbRuta
            // 
            this.lbRuta.AutoSize = true;
            this.lbRuta.Location = new System.Drawing.Point(40, 219);
            this.lbRuta.Name = "lbRuta";
            this.lbRuta.Size = new System.Drawing.Size(59, 17);
            this.lbRuta.TabIndex = 5;
            this.lbRuta.Text = "Ruta(s):";
            // 
            // grpBox
            // 
            this.grpBox.Controls.Add(this.lbDato);
            this.grpBox.Controls.Add(this.cmbMes);
            this.grpBox.Controls.Add(this.lbMes);
            this.grpBox.Controls.Add(this.lbTexto);
            this.grpBox.Controls.Add(this.txtAnio);
            this.grpBox.Controls.Add(this.cmbTipo);
            this.grpBox.Controls.Add(this.lbAnio);
            this.grpBox.Controls.Add(this.lbTipo);
            this.grpBox.Location = new System.Drawing.Point(43, 44);
            this.grpBox.Name = "grpBox";
            this.grpBox.Size = new System.Drawing.Size(1033, 111);
            this.grpBox.TabIndex = 6;
            this.grpBox.TabStop = false;
            // 
            // lbDato
            // 
            this.lbDato.AutoSize = true;
            this.lbDato.Location = new System.Drawing.Point(452, 29);
            this.lbDato.Name = "lbDato";
            this.lbDato.Size = new System.Drawing.Size(0, 17);
            this.lbDato.TabIndex = 7;
            // 
            // cmbMes
            // 
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
            "09 Setiembre",
            "10 Octubre",
            "11 Noviembre",
            "12 Diciembre"});
            this.cmbMes.Location = new System.Drawing.Point(253, 64);
            this.cmbMes.Name = "cmbMes";
            this.cmbMes.Size = new System.Drawing.Size(122, 24);
            this.cmbMes.TabIndex = 6;
            this.cmbMes.SelectedIndexChanged += new System.EventHandler(this.cmbMes_SelectedIndexChanged);
            // 
            // lbMes
            // 
            this.lbMes.AutoSize = true;
            this.lbMes.Location = new System.Drawing.Point(205, 69);
            this.lbMes.Name = "lbMes";
            this.lbMes.Size = new System.Drawing.Size(38, 17);
            this.lbMes.TabIndex = 5;
            this.lbMes.Text = "Mes:";
            // 
            // lbTexto
            // 
            this.lbTexto.AutoSize = true;
            this.lbTexto.Location = new System.Drawing.Point(250, 29);
            this.lbTexto.Name = "lbTexto";
            this.lbTexto.Size = new System.Drawing.Size(195, 17);
            this.lbTexto.TabIndex = 4;
            this.lbTexto.Text = "Se va a proceder a generar : ";
            // 
            // txtAnio
            // 
            this.txtAnio.Location = new System.Drawing.Point(62, 64);
            this.txtAnio.MaxLength = 4;
            this.txtAnio.Name = "txtAnio";
            this.txtAnio.Size = new System.Drawing.Size(116, 22);
            this.txtAnio.TabIndex = 3;
            this.txtAnio.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtAnio_KeyPress);
            // 
            // cmbTipo
            // 
            this.cmbTipo.FormattingEnabled = true;
            this.cmbTipo.Items.AddRange(new object[] {
            "PB",
            "PY",
            "RE"});
            this.cmbTipo.Location = new System.Drawing.Point(62, 26);
            this.cmbTipo.Name = "cmbTipo";
            this.cmbTipo.Size = new System.Drawing.Size(181, 24);
            this.cmbTipo.TabIndex = 2;
            this.cmbTipo.SelectedIndexChanged += new System.EventHandler(this.cmbTipo_SelectedIndexChanged);
            // 
            // lbAnio
            // 
            this.lbAnio.AutoSize = true;
            this.lbAnio.Location = new System.Drawing.Point(7, 67);
            this.lbAnio.Name = "lbAnio";
            this.lbAnio.Size = new System.Drawing.Size(37, 17);
            this.lbAnio.TabIndex = 1;
            this.lbAnio.Text = "Año:";
            // 
            // lbTipo
            // 
            this.lbTipo.AutoSize = true;
            this.lbTipo.Location = new System.Drawing.Point(7, 29);
            this.lbTipo.Name = "lbTipo";
            this.lbTipo.Size = new System.Drawing.Size(40, 17);
            this.lbTipo.TabIndex = 0;
            this.lbTipo.Text = "Tipo:";
            // 
            // listBoxRegMont
            // 
            this.listBoxRegMont.BackColor = System.Drawing.SystemColors.Control;
            this.listBoxRegMont.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.listBoxRegMont.FormattingEnabled = true;
            this.listBoxRegMont.ItemHeight = 16;
            this.listBoxRegMont.Location = new System.Drawing.Point(581, 251);
            this.listBoxRegMont.Name = "listBoxRegMont";
            this.listBoxRegMont.Size = new System.Drawing.Size(109, 176);
            this.listBoxRegMont.TabIndex = 7;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(578, 219);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(54, 17);
            this.label1.TabIndex = 8;
            this.label1.Text = "N° Reg";
            // 
            // listBoxMntTotal
            // 
            this.listBoxMntTotal.BackColor = System.Drawing.SystemColors.Control;
            this.listBoxMntTotal.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.listBoxMntTotal.FormattingEnabled = true;
            this.listBoxMntTotal.ItemHeight = 16;
            this.listBoxMntTotal.Location = new System.Drawing.Point(713, 251);
            this.listBoxMntTotal.Name = "listBoxMntTotal";
            this.listBoxMntTotal.Size = new System.Drawing.Size(117, 176);
            this.listBoxMntTotal.TabIndex = 9;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(681, 219);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(109, 17);
            this.label2.TabIndex = 10;
            this.label2.Text = "|       Monto total";
            // 
            // ui_mqt_gastos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1122, 578);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.listBoxMntTotal);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.listBoxRegMont);
            this.Controls.Add(this.grpBox);
            this.Controls.Add(this.lbRuta);
            this.Controls.Add(this.lbPorcentaje);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.btnRegData);
            this.Controls.Add(this.listBoxArchivos);
            this.Controls.Add(this.btnCargar);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ui_mqt_gastos";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Carga Masiva de PB, PY y Real";
            this.grpBox.ResumeLayout(false);
            this.grpBox.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnCargar;
        private System.Windows.Forms.ListBox listBoxArchivos;
        private System.Windows.Forms.Button btnRegData;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.Label lbPorcentaje;
        private System.Windows.Forms.Label lbRuta;
        private System.Windows.Forms.GroupBox grpBox;
        private System.Windows.Forms.Label lbTipo;
        private System.Windows.Forms.Label lbAnio;
        private System.Windows.Forms.ComboBox cmbTipo;
        private System.Windows.Forms.Label lbTexto;
        private System.Windows.Forms.TextBox txtAnio;
        private System.Windows.Forms.Label lbMes;
        private System.Windows.Forms.ComboBox cmbMes;
        private System.Windows.Forms.Label lbDato;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.ListBox listBoxRegMont;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListBox listBoxMntTotal;
        private System.Windows.Forms.Label label2;
    }
}