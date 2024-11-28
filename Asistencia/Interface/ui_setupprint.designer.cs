namespace CaniaBrava
{
    partial class ui_setupprint
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
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.nudTamaño = new System.Windows.Forms.NumericUpDown();
            this.cmbTipoLetra = new System.Windows.Forms.ComboBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.cmbImpresora = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnImprimir = new System.Windows.Forms.Button();
            this.btnSalir = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rbGrafico = new System.Windows.Forms.RadioButton();
            this.rbTexto = new System.Windows.Forms.RadioButton();
            this.grpMargen = new System.Windows.Forms.GroupBox();
            this.txtMargenSup = new System.Windows.Forms.MaskedTextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtMargenIzq = new System.Windows.Forms.MaskedTextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.grpOrientacion = new System.Windows.Forms.GroupBox();
            this.pbHorizontal = new System.Windows.Forms.PictureBox();
            this.pbVertical = new System.Windows.Forms.PictureBox();
            this.rbHorizontal = new System.Windows.Forms.RadioButton();
            this.rbVertical = new System.Windows.Forms.RadioButton();
            this.btnPrevio = new System.Windows.Forms.Button();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudTamaño)).BeginInit();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.grpMargen.SuspendLayout();
            this.grpOrientacion.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbHorizontal)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbVertical)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.label2);
            this.groupBox4.Controls.Add(this.nudTamaño);
            this.groupBox4.Controls.Add(this.cmbTipoLetra);
            this.groupBox4.Location = new System.Drawing.Point(21, 158);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(508, 56);
            this.groupBox4.TabIndex = 5;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Tipo de Letra";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(393, 24);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(46, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Tamaño";
            // 
            // nudTamaño
            // 
            this.nudTamaño.Location = new System.Drawing.Point(446, 20);
            this.nudTamaño.Maximum = new decimal(new int[] {
            15,
            0,
            0,
            0});
            this.nudTamaño.Minimum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.nudTamaño.Name = "nudTamaño";
            this.nudTamaño.Size = new System.Drawing.Size(56, 20);
            this.nudTamaño.TabIndex = 6;
            this.nudTamaño.Value = new decimal(new int[] {
            9,
            0,
            0,
            0});
            // 
            // cmbTipoLetra
            // 
            this.cmbTipoLetra.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTipoLetra.DropDownWidth = 200;
            this.cmbTipoLetra.FormattingEnabled = true;
            this.cmbTipoLetra.Location = new System.Drawing.Point(34, 20);
            this.cmbTipoLetra.Name = "cmbTipoLetra";
            this.cmbTipoLetra.Size = new System.Drawing.Size(353, 21);
            this.cmbTipoLetra.TabIndex = 1;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.cmbImpresora);
            this.groupBox3.Controls.Add(this.label1);
            this.groupBox3.Location = new System.Drawing.Point(21, 73);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(508, 79);
            this.groupBox3.TabIndex = 4;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Impresora";
            // 
            // cmbImpresora
            // 
            this.cmbImpresora.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbImpresora.FormattingEnabled = true;
            this.cmbImpresora.Location = new System.Drawing.Point(34, 43);
            this.cmbImpresora.Name = "cmbImpresora";
            this.cmbImpresora.Size = new System.Drawing.Size(353, 21);
            this.cmbImpresora.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(31, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(44, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Nombre";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnPrevio);
            this.groupBox2.Controls.Add(this.btnImprimir);
            this.groupBox2.Controls.Add(this.btnSalir);
            this.groupBox2.Location = new System.Drawing.Point(419, 219);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(110, 175);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            // 
            // btnImprimir
            // 
            this.btnImprimir.Location = new System.Drawing.Point(16, 78);
            this.btnImprimir.Name = "btnImprimir";
            this.btnImprimir.Size = new System.Drawing.Size(75, 23);
            this.btnImprimir.TabIndex = 1;
            this.btnImprimir.Text = "Imprimir";
            this.btnImprimir.UseVisualStyleBackColor = true;
            this.btnImprimir.Click += new System.EventHandler(this.btnImprimir_Click);
            // 
            // btnSalir
            // 
            this.btnSalir.Location = new System.Drawing.Point(16, 117);
            this.btnSalir.Name = "btnSalir";
            this.btnSalir.Size = new System.Drawing.Size(75, 23);
            this.btnSalir.TabIndex = 2;
            this.btnSalir.Text = "Salir";
            this.btnSalir.UseVisualStyleBackColor = true;
            this.btnSalir.Click += new System.EventHandler(this.btnSalir_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rbGrafico);
            this.groupBox1.Controls.Add(this.rbTexto);
            this.groupBox1.Location = new System.Drawing.Point(21, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(508, 55);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Modo de Impresión";
            // 
            // rbGrafico
            // 
            this.rbGrafico.AutoSize = true;
            this.rbGrafico.Checked = true;
            this.rbGrafico.Location = new System.Drawing.Point(135, 19);
            this.rbGrafico.Name = "rbGrafico";
            this.rbGrafico.Size = new System.Drawing.Size(59, 17);
            this.rbGrafico.TabIndex = 2;
            this.rbGrafico.TabStop = true;
            this.rbGrafico.Text = "Gráfico";
            this.rbGrafico.UseVisualStyleBackColor = true;
            this.rbGrafico.CheckedChanged += new System.EventHandler(this.rbGrafico_CheckedChanged);
            // 
            // rbTexto
            // 
            this.rbTexto.AutoSize = true;
            this.rbTexto.Location = new System.Drawing.Point(34, 19);
            this.rbTexto.Name = "rbTexto";
            this.rbTexto.Size = new System.Drawing.Size(52, 17);
            this.rbTexto.TabIndex = 1;
            this.rbTexto.Text = "Texto";
            this.rbTexto.UseVisualStyleBackColor = true;
            this.rbTexto.CheckedChanged += new System.EventHandler(this.rbTexto_CheckedChanged);
            // 
            // grpMargen
            // 
            this.grpMargen.Controls.Add(this.txtMargenSup);
            this.grpMargen.Controls.Add(this.label4);
            this.grpMargen.Controls.Add(this.txtMargenIzq);
            this.grpMargen.Controls.Add(this.label3);
            this.grpMargen.Location = new System.Drawing.Point(21, 220);
            this.grpMargen.Name = "grpMargen";
            this.grpMargen.Size = new System.Drawing.Size(279, 56);
            this.grpMargen.TabIndex = 7;
            this.grpMargen.TabStop = false;
            this.grpMargen.Text = "Margen";
            // 
            // txtMargenSup
            // 
            this.txtMargenSup.Location = new System.Drawing.Point(196, 19);
            this.txtMargenSup.Mask = "999";
            this.txtMargenSup.Name = "txtMargenSup";
            this.txtMargenSup.PromptChar = ' ';
            this.txtMargenSup.Size = new System.Drawing.Size(66, 20);
            this.txtMargenSup.TabIndex = 9;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(144, 23);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(46, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "Superior";
            // 
            // txtMargenIzq
            // 
            this.txtMargenIzq.Location = new System.Drawing.Point(71, 19);
            this.txtMargenIzq.Mask = "999";
            this.txtMargenIzq.Name = "txtMargenIzq";
            this.txtMargenIzq.PromptChar = ' ';
            this.txtMargenIzq.Size = new System.Drawing.Size(66, 20);
            this.txtMargenIzq.TabIndex = 8;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(15, 23);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(50, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Izquierdo";
            // 
            // grpOrientacion
            // 
            this.grpOrientacion.Controls.Add(this.pbHorizontal);
            this.grpOrientacion.Controls.Add(this.pbVertical);
            this.grpOrientacion.Controls.Add(this.rbHorizontal);
            this.grpOrientacion.Controls.Add(this.rbVertical);
            this.grpOrientacion.Location = new System.Drawing.Point(21, 294);
            this.grpOrientacion.Name = "grpOrientacion";
            this.grpOrientacion.Size = new System.Drawing.Size(279, 100);
            this.grpOrientacion.TabIndex = 3;
            this.grpOrientacion.TabStop = false;
            this.grpOrientacion.Text = "Orientación";
            // 
            // pbHorizontal
            // 
            this.pbHorizontal.Image = global::CaniaBrava.Properties.Resources.horizontal;
            this.pbHorizontal.Location = new System.Drawing.Point(40, 23);
            this.pbHorizontal.Name = "pbHorizontal";
            this.pbHorizontal.Size = new System.Drawing.Size(58, 49);
            this.pbHorizontal.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pbHorizontal.TabIndex = 9;
            this.pbHorizontal.TabStop = false;
            this.pbHorizontal.Visible = false;
            // 
            // pbVertical
            // 
            this.pbVertical.Image = global::CaniaBrava.Properties.Resources.vertical;
            this.pbVertical.Location = new System.Drawing.Point(45, 18);
            this.pbVertical.Name = "pbVertical";
            this.pbVertical.Size = new System.Drawing.Size(48, 58);
            this.pbVertical.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pbVertical.TabIndex = 8;
            this.pbVertical.TabStop = false;
            // 
            // rbHorizontal
            // 
            this.rbHorizontal.AutoSize = true;
            this.rbHorizontal.Location = new System.Drawing.Point(147, 53);
            this.rbHorizontal.Name = "rbHorizontal";
            this.rbHorizontal.Size = new System.Drawing.Size(72, 17);
            this.rbHorizontal.TabIndex = 2;
            this.rbHorizontal.Text = "Horizontal";
            this.rbHorizontal.UseVisualStyleBackColor = true;
            this.rbHorizontal.CheckedChanged += new System.EventHandler(this.rbHorizontal_CheckedChanged);
            // 
            // rbVertical
            // 
            this.rbVertical.AutoSize = true;
            this.rbVertical.Checked = true;
            this.rbVertical.Location = new System.Drawing.Point(147, 19);
            this.rbVertical.Name = "rbVertical";
            this.rbVertical.Size = new System.Drawing.Size(60, 17);
            this.rbVertical.TabIndex = 1;
            this.rbVertical.TabStop = true;
            this.rbVertical.Text = "Vertical";
            this.rbVertical.UseVisualStyleBackColor = true;
            this.rbVertical.CheckedChanged += new System.EventHandler(this.rbVertical_CheckedChanged);
            // 
            // btnPrevio
            // 
            this.btnPrevio.Location = new System.Drawing.Point(16, 34);
            this.btnPrevio.Name = "btnPrevio";
            this.btnPrevio.Size = new System.Drawing.Size(75, 23);
            this.btnPrevio.TabIndex = 3;
            this.btnPrevio.Text = "Vista Previa";
            this.btnPrevio.UseVisualStyleBackColor = true;
            this.btnPrevio.Click += new System.EventHandler(this.btnPrevio_Click);
            // 
            // ui_setupprint
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(550, 406);
            this.Controls.Add(this.grpOrientacion);
            this.Controls.Add(this.grpMargen);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "ui_setupprint";
            this.Text = "Configuración Impresora";
            this.Load += new System.EventHandler(this.ui_setupprint_Load);
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudTamaño)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.grpMargen.ResumeLayout(false);
            this.grpMargen.PerformLayout();
            this.grpOrientacion.ResumeLayout(false);
            this.grpOrientacion.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbHorizontal)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbVertical)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rbTexto;
        private System.Windows.Forms.RadioButton rbGrafico;
        private System.Windows.Forms.Button btnImprimir;
        private System.Windows.Forms.Button btnSalir;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.ComboBox cmbImpresora;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown nudTamaño;
        private System.Windows.Forms.ComboBox cmbTipoLetra;
        private System.Windows.Forms.GroupBox grpMargen;
        private System.Windows.Forms.MaskedTextBox txtMargenSup;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.MaskedTextBox txtMargenIzq;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox grpOrientacion;
        private System.Windows.Forms.RadioButton rbHorizontal;
        private System.Windows.Forms.RadioButton rbVertical;
        private System.Windows.Forms.PictureBox pbVertical;
        private System.Windows.Forms.PictureBox pbHorizontal;
        private System.Windows.Forms.Button btnPrevio;
    }
}
