namespace CaniaBrava
{
    partial class ui_rep_asistencia_sunafil
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
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnGenerar = new System.Windows.Forms.Button();
            this.dtpFecfin = new System.Windows.Forms.DateTimePicker();
            this.label6 = new System.Windows.Forms.Label();
            this.cmbCia = new System.Windows.Forms.ComboBox();
            this.loadingNext1 = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.dtpFecini = new System.Windows.Forms.DateTimePicker();
            this.cmbSedes = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.loadingNext1)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.Controls.Add(this.label2);
            this.groupBox3.Controls.Add(this.btnGenerar);
            this.groupBox3.Controls.Add(this.dtpFecfin);
            this.groupBox3.Controls.Add(this.label6);
            this.groupBox3.Controls.Add(this.cmbCia);
            this.groupBox3.Controls.Add(this.loadingNext1);
            this.groupBox3.Controls.Add(this.label1);
            this.groupBox3.Controls.Add(this.dtpFecini);
            this.groupBox3.Controls.Add(this.cmbSedes);
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Location = new System.Drawing.Point(12, 12);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(455, 155);
            this.groupBox3.TabIndex = 30;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Filtros: ";
            this.groupBox3.Enter += new System.EventHandler(this.groupBox3_Enter);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(190, 51);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(74, 13);
            this.label2.TabIndex = 105;
            this.label2.Text = "Fecha Hasta :";
            // 
            // btnGenerar
            // 
            this.btnGenerar.Location = new System.Drawing.Point(19, 126);
            this.btnGenerar.Name = "btnGenerar";
            this.btnGenerar.Size = new System.Drawing.Size(75, 23);
            this.btnGenerar.TabIndex = 22;
            this.btnGenerar.Text = "Generar";
            this.btnGenerar.UseVisualStyleBackColor = true;
            this.btnGenerar.Click += new System.EventHandler(this.btnGenerar_Click);
            // 
            // dtpFecfin
            // 
            this.dtpFecfin.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFecfin.Location = new System.Drawing.Point(267, 48);
            this.dtpFecfin.Name = "dtpFecfin";
            this.dtpFecfin.Size = new System.Drawing.Size(80, 20);
            this.dtpFecfin.TabIndex = 106;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(16, 81);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(51, 13);
            this.label6.TabIndex = 32;
            this.label6.Text = "Empresa:";
            // 
            // cmbCia
            // 
            this.cmbCia.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCia.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbCia.FormattingEnabled = true;
            this.cmbCia.Location = new System.Drawing.Point(91, 77);
            this.cmbCia.Name = "cmbCia";
            this.cmbCia.Size = new System.Drawing.Size(256, 20);
            this.cmbCia.TabIndex = 31;
            this.cmbCia.SelectedIndexChanged += new System.EventHandler(this.cmbCia_SelectedIndexChanged);
            // 
            // loadingNext1
            // 
            this.loadingNext1.BackColor = System.Drawing.Color.Transparent;
            this.loadingNext1.ErrorImage = null;
            this.loadingNext1.Image = global::CaniaBrava.Properties.Resources.loading;
            this.loadingNext1.Location = new System.Drawing.Point(369, 19);
            this.loadingNext1.Name = "loadingNext1";
            this.loadingNext1.Size = new System.Drawing.Size(74, 61);
            this.loadingNext1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.loadingNext1.TabIndex = 104;
            this.loadingNext1.TabStop = false;
            this.loadingNext1.Visible = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 52);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(72, 13);
            this.label1.TabIndex = 20;
            this.label1.Text = "Fecha desde:";
            // 
            // dtpFecini
            // 
            this.dtpFecini.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFecini.Location = new System.Drawing.Point(91, 48);
            this.dtpFecini.Name = "dtpFecini";
            this.dtpFecini.Size = new System.Drawing.Size(80, 20);
            this.dtpFecini.TabIndex = 21;
            // 
            // cmbSedes
            // 
            this.cmbSedes.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSedes.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbSedes.FormattingEnabled = true;
            this.cmbSedes.Location = new System.Drawing.Point(91, 19);
            this.cmbSedes.Name = "cmbSedes";
            this.cmbSedes.Size = new System.Drawing.Size(256, 20);
            this.cmbSedes.TabIndex = 17;
            this.cmbSedes.SelectedIndexChanged += new System.EventHandler(this.cmbSedes_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(16, 23);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 13);
            this.label3.TabIndex = 16;
            this.label3.Text = "Sede : ";
            // 
            // ui_rep_asistencia_sunafil
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(479, 174);
            this.Controls.Add(this.groupBox3);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ui_rep_asistencia_sunafil";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Reporte Asistencia SUNAFIL";
            this.Load += new System.EventHandler(this.ui_rep_asistencia_sunafil_Load);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.loadingNext1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.ComboBox cmbSedes;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dtpFecini;
        private System.Windows.Forms.Button btnGenerar;
        private System.Windows.Forms.PictureBox loadingNext1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cmbCia;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dtpFecfin;
    }
}