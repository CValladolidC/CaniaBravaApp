namespace CaniaBrava
{
    partial class ui_agrupaturnos
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
            this.dgvdetalle = new System.Windows.Forms.DataGridView();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cmbGrupos = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnGrupoB = new System.Windows.Forms.Button();
            this.btnGrupoA = new System.Windows.Forms.Button();
            this.btnGrupoD = new System.Windows.Forms.Button();
            this.btnGrupoC = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txtBuscar = new System.Windows.Forms.TextBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label6 = new System.Windows.Forms.Label();
            this.cmbCencos = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.cmbGerencia = new System.Windows.Forms.ComboBox();
            this.cmbCia = new System.Windows.Forms.ComboBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgvdetalle)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgvdetalle
            // 
            this.dgvdetalle.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.dgvdetalle.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvdetalle.Enabled = false;
            this.dgvdetalle.Location = new System.Drawing.Point(1, 162);
            this.dgvdetalle.Name = "dgvdetalle";
            this.dgvdetalle.Size = new System.Drawing.Size(583, 329);
            this.dgvdetalle.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cmbGrupos);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(430, 1);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(153, 52);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Filtrar por";
            // 
            // cmbGrupos
            // 
            this.cmbGrupos.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbGrupos.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbGrupos.FormattingEnabled = true;
            this.cmbGrupos.Location = new System.Drawing.Point(52, 19);
            this.cmbGrupos.Name = "cmbGrupos";
            this.cmbGrupos.Size = new System.Drawing.Size(95, 20);
            this.cmbGrupos.TabIndex = 1;
            this.cmbGrupos.SelectedIndexChanged += new System.EventHandler(this.cmbGrupos_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(42, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Grupo :";
            // 
            // btnGrupoB
            // 
            this.btnGrupoB.Location = new System.Drawing.Point(77, 26);
            this.btnGrupoB.Name = "btnGrupoB";
            this.btnGrupoB.Size = new System.Drawing.Size(51, 26);
            this.btnGrupoB.TabIndex = 2;
            this.btnGrupoB.Text = "B";
            this.btnGrupoB.UseVisualStyleBackColor = true;
            this.btnGrupoB.Click += new System.EventHandler(this.btnGrupoB_Click);
            // 
            // btnGrupoA
            // 
            this.btnGrupoA.Location = new System.Drawing.Point(26, 26);
            this.btnGrupoA.Name = "btnGrupoA";
            this.btnGrupoA.Size = new System.Drawing.Size(51, 26);
            this.btnGrupoA.TabIndex = 3;
            this.btnGrupoA.Text = "A";
            this.btnGrupoA.UseVisualStyleBackColor = true;
            this.btnGrupoA.Click += new System.EventHandler(this.btnGrupoA_Click);
            // 
            // btnGrupoD
            // 
            this.btnGrupoD.Location = new System.Drawing.Point(77, 52);
            this.btnGrupoD.Name = "btnGrupoD";
            this.btnGrupoD.Size = new System.Drawing.Size(51, 26);
            this.btnGrupoD.TabIndex = 4;
            this.btnGrupoD.Text = "D";
            this.btnGrupoD.UseVisualStyleBackColor = true;
            this.btnGrupoD.Click += new System.EventHandler(this.btnGrupoD_Click);
            // 
            // btnGrupoC
            // 
            this.btnGrupoC.Location = new System.Drawing.Point(26, 52);
            this.btnGrupoC.Name = "btnGrupoC";
            this.btnGrupoC.Size = new System.Drawing.Size(51, 26);
            this.btnGrupoC.TabIndex = 5;
            this.btnGrupoC.Text = "C";
            this.btnGrupoC.UseVisualStyleBackColor = true;
            this.btnGrupoC.Click += new System.EventHandler(this.btnGrupoC_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.txtBuscar);
            this.groupBox2.Location = new System.Drawing.Point(12, 109);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(413, 47);
            this.groupBox2.TabIndex = 6;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Buscar por Trabajador";
            // 
            // txtBuscar
            // 
            this.txtBuscar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtBuscar.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtBuscar.Location = new System.Drawing.Point(6, 17);
            this.txtBuscar.Name = "txtBuscar";
            this.txtBuscar.Size = new System.Drawing.Size(401, 20);
            this.txtBuscar.TabIndex = 0;
            this.txtBuscar.TextChanged += new System.EventHandler(this.txtBuscar_TextChanged);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label6);
            this.groupBox3.Controls.Add(this.cmbCencos);
            this.groupBox3.Controls.Add(this.label7);
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Controls.Add(this.cmbGerencia);
            this.groupBox3.Controls.Add(this.cmbCia);
            this.groupBox3.Location = new System.Drawing.Point(12, 1);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(412, 102);
            this.groupBox3.TabIndex = 96;
            this.groupBox3.TabStop = false;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 22);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(51, 13);
            this.label6.TabIndex = 16;
            this.label6.Text = "Empresa:";
            // 
            // cmbCencos
            // 
            this.cmbCencos.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCencos.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbCencos.FormattingEnabled = true;
            this.cmbCencos.Location = new System.Drawing.Point(61, 73);
            this.cmbCencos.Name = "cmbCencos";
            this.cmbCencos.Size = new System.Drawing.Size(336, 20);
            this.cmbCencos.TabIndex = 6;
            this.cmbCencos.SelectedIndexChanged += new System.EventHandler(this.cmbCencos_SelectedIndexChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(6, 49);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(53, 13);
            this.label7.TabIndex = 18;
            this.label7.Text = "Gerencia:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 76);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(32, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Área:";
            // 
            // cmbGerencia
            // 
            this.cmbGerencia.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbGerencia.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbGerencia.FormattingEnabled = true;
            this.cmbGerencia.Location = new System.Drawing.Point(61, 46);
            this.cmbGerencia.Name = "cmbGerencia";
            this.cmbGerencia.Size = new System.Drawing.Size(336, 20);
            this.cmbGerencia.TabIndex = 17;
            this.cmbGerencia.SelectedIndexChanged += new System.EventHandler(this.cmbGerencia_SelectedIndexChanged);
            // 
            // cmbCia
            // 
            this.cmbCia.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCia.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbCia.FormattingEnabled = true;
            this.cmbCia.Location = new System.Drawing.Point(61, 19);
            this.cmbCia.Name = "cmbCia";
            this.cmbCia.Size = new System.Drawing.Size(336, 20);
            this.cmbCia.TabIndex = 15;
            this.cmbCia.SelectedIndexChanged += new System.EventHandler(this.cmbCia_SelectedIndexChanged);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.btnGrupoD);
            this.groupBox4.Controls.Add(this.btnGrupoC);
            this.groupBox4.Controls.Add(this.btnGrupoB);
            this.groupBox4.Controls.Add(this.btnGrupoA);
            this.groupBox4.Location = new System.Drawing.Point(431, 59);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(153, 97);
            this.groupBox4.TabIndex = 97;
            this.groupBox4.TabStop = false;
            // 
            // ui_agrupaturnos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.HighlightText;
            this.ClientSize = new System.Drawing.Size(585, 492);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.dgvdetalle);
            this.MinimizeBox = false;
            this.Name = "ui_agrupaturnos";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Agrupamiento de Trabajadores para Programacion de Horarios";
            this.Load += new System.EventHandler(this.ui_agrupaturnos_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvdetalle)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvdetalle;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox cmbGrupos;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnGrupoB;
        private System.Windows.Forms.Button btnGrupoA;
        private System.Windows.Forms.Button btnGrupoD;
        private System.Windows.Forms.Button btnGrupoC;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox txtBuscar;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cmbCencos;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cmbGerencia;
        private System.Windows.Forms.ComboBox cmbCia;
        private System.Windows.Forms.GroupBox groupBox4;
    }
}