namespace CaniaBrava
{
    partial class ui_updcontrolling_recursos
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
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.cmbrubro = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.cmbcodigo = new System.Windows.Forms.ComboBox();
            this.cmbactividad = new System.Windows.Forms.ComboBox();
            this.cmbcuencon = new System.Windows.Forms.ComboBox();
            this.txtid = new System.Windows.Forms.TextBox();
            this.lblAbrevia = new System.Windows.Forms.Label();
            this.cmbUM = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtcodigo = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.cmbrecurso = new System.Windows.Forms.ComboBox();
            this.lblEstado = new System.Windows.Forms.Label();
            this.toolStripForm = new System.Windows.Forms.ToolStrip();
            this.btnAceptar = new System.Windows.Forms.ToolStripButton();
            this.btnCancelar = new System.Windows.Forms.ToolStripButton();
            this.groupBox2.SuspendLayout();
            this.toolStripForm.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.cmbrubro);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.cmbcodigo);
            this.groupBox2.Controls.Add(this.cmbactividad);
            this.groupBox2.Controls.Add(this.cmbcuencon);
            this.groupBox2.Controls.Add(this.txtid);
            this.groupBox2.Controls.Add(this.lblAbrevia);
            this.groupBox2.Controls.Add(this.cmbUM);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.txtcodigo);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.cmbrecurso);
            this.groupBox2.Controls.Add(this.lblEstado);
            this.groupBox2.Location = new System.Drawing.Point(12, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(498, 246);
            this.groupBox2.TabIndex = 82;
            this.groupBox2.TabStop = false;
            // 
            // cmbrubro
            // 
            this.cmbrubro.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbrubro.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbrubro.FormattingEnabled = true;
            this.cmbrubro.Location = new System.Drawing.Point(134, 136);
            this.cmbrubro.Name = "cmbrubro";
            this.cmbrubro.Size = new System.Drawing.Size(189, 20);
            this.cmbrubro.Sorted = true;
            this.cmbrubro.TabIndex = 89;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 143);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(45, 13);
            this.label5.TabIndex = 88;
            this.label5.Text = "Rubro : ";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 17);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(49, 13);
            this.label4.TabIndex = 87;
            this.label4.Text = "Codigo : ";
            // 
            // cmbcodigo
            // 
            this.cmbcodigo.FormattingEnabled = true;
            this.cmbcodigo.Location = new System.Drawing.Point(134, 14);
            this.cmbcodigo.Name = "cmbcodigo";
            this.cmbcodigo.Size = new System.Drawing.Size(358, 21);
            this.cmbcodigo.Sorted = true;
            this.cmbcodigo.TabIndex = 86;
            this.cmbcodigo.SelectedIndexChanged += new System.EventHandler(this.cmbcodigo_SelectedIndexChanged);
            this.cmbcodigo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cmbcodigo_KeyPress);
            // 
            // cmbactividad
            // 
            this.cmbactividad.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbactividad.FormattingEnabled = true;
            this.cmbactividad.Location = new System.Drawing.Point(134, 166);
            this.cmbactividad.Name = "cmbactividad";
            this.cmbactividad.Size = new System.Drawing.Size(280, 21);
            this.cmbactividad.Sorted = true;
            this.cmbactividad.TabIndex = 85;
            // 
            // cmbcuencon
            // 
            this.cmbcuencon.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbcuencon.FormattingEnabled = true;
            this.cmbcuencon.Location = new System.Drawing.Point(134, 76);
            this.cmbcuencon.Name = "cmbcuencon";
            this.cmbcuencon.Size = new System.Drawing.Size(280, 21);
            this.cmbcuencon.Sorted = true;
            this.cmbcuencon.TabIndex = 84;
            // 
            // txtid
            // 
            this.txtid.BackColor = System.Drawing.SystemColors.HighlightText;
            this.txtid.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtid.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtid.Location = new System.Drawing.Point(236, 45);
            this.txtid.MaxLength = 4;
            this.txtid.Name = "txtid";
            this.txtid.Size = new System.Drawing.Size(87, 18);
            this.txtid.TabIndex = 83;
            this.txtid.Visible = false;
            // 
            // lblAbrevia
            // 
            this.lblAbrevia.AutoSize = true;
            this.lblAbrevia.Location = new System.Drawing.Point(6, 48);
            this.lblAbrevia.Name = "lblAbrevia";
            this.lblAbrevia.Size = new System.Drawing.Size(33, 13);
            this.lblAbrevia.TabIndex = 81;
            this.lblAbrevia.Text = "UM : ";
            // 
            // cmbUM
            // 
            this.cmbUM.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbUM.FormattingEnabled = true;
            this.cmbUM.Location = new System.Drawing.Point(134, 45);
            this.cmbUM.Name = "cmbUM";
            this.cmbUM.Size = new System.Drawing.Size(96, 21);
            this.cmbUM.Sorted = true;
            this.cmbUM.TabIndex = 6;
            this.cmbUM.SelectedIndexChanged += new System.EventHandler(this.cmbUM_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 173);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(60, 13);
            this.label3.TabIndex = 77;
            this.label3.Text = "Actividad : ";
            // 
            // txtcodigo
            // 
            this.txtcodigo.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtcodigo.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtcodigo.Location = new System.Drawing.Point(134, 109);
            this.txtcodigo.MaxLength = 30;
            this.txtcodigo.Name = "txtcodigo";
            this.txtcodigo.ReadOnly = true;
            this.txtcodigo.Size = new System.Drawing.Size(96, 18);
            this.txtcodigo.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 110);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(39, 13);
            this.label2.TabIndex = 75;
            this.label2.Text = "COD : ";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 79);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(95, 13);
            this.label1.TabIndex = 73;
            this.label1.Text = "Cuenta Contable : ";
            // 
            // cmbrecurso
            // 
            this.cmbrecurso.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbrecurso.FormattingEnabled = true;
            this.cmbrecurso.Location = new System.Drawing.Point(134, 198);
            this.cmbrecurso.Name = "cmbrecurso";
            this.cmbrecurso.Size = new System.Drawing.Size(140, 21);
            this.cmbrecurso.Sorted = true;
            this.cmbrecurso.TabIndex = 7;
            this.cmbrecurso.SelectedIndexChanged += new System.EventHandler(this.cmbrecurso_SelectedIndexChanged);
            // 
            // lblEstado
            // 
            this.lblEstado.AutoSize = true;
            this.lblEstado.Location = new System.Drawing.Point(6, 201);
            this.lblEstado.Name = "lblEstado";
            this.lblEstado.Size = new System.Drawing.Size(56, 13);
            this.lblEstado.TabIndex = 65;
            this.lblEstado.Text = "Recurso : ";
            // 
            // toolStripForm
            // 
            this.toolStripForm.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.toolStripForm.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.toolStripForm.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnAceptar,
            this.btnCancelar});
            this.toolStripForm.Location = new System.Drawing.Point(0, 267);
            this.toolStripForm.Name = "toolStripForm";
            this.toolStripForm.ShowItemToolTips = false;
            this.toolStripForm.Size = new System.Drawing.Size(522, 25);
            this.toolStripForm.TabIndex = 83;
            // 
            // btnAceptar
            // 
            this.btnAceptar.Image = global::CaniaBrava.Properties.Resources.SAVE;
            this.btnAceptar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnAceptar.Name = "btnAceptar";
            this.btnAceptar.Size = new System.Drawing.Size(68, 22);
            this.btnAceptar.Text = "Aceptar";
            this.btnAceptar.Click += new System.EventHandler(this.btnAceptar_Click);
            // 
            // btnCancelar
            // 
            this.btnCancelar.Image = global::CaniaBrava.Properties.Resources.UNDO;
            this.btnCancelar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(73, 22);
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // ui_updcontrolling_recursos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(522, 292);
            this.Controls.Add(this.toolStripForm);
            this.Controls.Add(this.groupBox2);
            this.MaximumSize = new System.Drawing.Size(538, 331);
            this.MinimumSize = new System.Drawing.Size(538, 331);
            this.Name = "ui_updcontrolling_recursos";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Actualizacion de Registro";
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.toolStripForm.ResumeLayout(false);
            this.toolStripForm.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label lblAbrevia;
        private System.Windows.Forms.ComboBox cmbUM;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtcodigo;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbrecurso;
        private System.Windows.Forms.Label lblEstado;
        private System.Windows.Forms.ComboBox cmbcuencon;
        private System.Windows.Forms.TextBox txtid;
        private System.Windows.Forms.ToolStrip toolStripForm;
        private System.Windows.Forms.ToolStripButton btnAceptar;
        private System.Windows.Forms.ToolStripButton btnCancelar;
        private System.Windows.Forms.ComboBox cmbactividad;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cmbcodigo;
        private System.Windows.Forms.ComboBox cmbrubro;
        private System.Windows.Forms.Label label5;
    }
}