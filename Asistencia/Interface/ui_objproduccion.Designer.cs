namespace CaniaBrava.Interface
{
    partial class ui_objproduccion
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ui_objproduccion));
            this.btnSave = new System.Windows.Forms.Button();
            this.dgIndicadores = new System.Windows.Forms.DataGridView();
            this.dtpickerFechaObjetivo = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tPIndicadores = new System.Windows.Forms.TabPage();
            this.tbQuery = new System.Windows.Forms.TabPage();
            this.txtQuery = new System.Windows.Forms.TextBox();
            this.tbManejoErrores = new System.Windows.Forms.TabPage();
            this.txtManejoDeErrores = new System.Windows.Forms.TextBox();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolSBtnCode = new System.Windows.Forms.ToolStripButton();
            this.txtQuery2 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgIndicadores)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.tPIndicadores.SuspendLayout();
            this.tbQuery.SuspendLayout();
            this.tbManejoErrores.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnSave
            // 
            this.btnSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F);
            this.btnSave.Image = global::CaniaBrava.Properties.Resources.SAVE;
            this.btnSave.Location = new System.Drawing.Point(691, 163);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(55, 52);
            this.btnSave.TabIndex = 2;
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // dgIndicadores
            // 
            this.dgIndicadores.AllowUserToAddRows = false;
            this.dgIndicadores.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgIndicadores.Location = new System.Drawing.Point(3, 6);
            this.dgIndicadores.Name = "dgIndicadores";
            this.dgIndicadores.RowTemplate.Height = 24;
            this.dgIndicadores.Size = new System.Drawing.Size(616, 330);
            this.dgIndicadores.TabIndex = 3;
            // 
            // dtpickerFechaObjetivo
            // 
            this.dtpickerFechaObjetivo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpickerFechaObjetivo.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpickerFechaObjetivo.Location = new System.Drawing.Point(203, 47);
            this.dtpickerFechaObjetivo.Name = "dtpickerFechaObjetivo";
            this.dtpickerFechaObjetivo.Size = new System.Drawing.Size(187, 26);
            this.dtpickerFechaObjetivo.TabIndex = 4;
            this.dtpickerFechaObjetivo.ValueChanged += new System.EventHandler(this.dtpickerFechaObjetivo_ValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(15, 53);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(166, 20);
            this.label1.TabIndex = 5;
            this.label1.Text = "Seleccionar la fecha:";
            // 
            // btnUpdate
            // 
            this.btnUpdate.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F);
            this.btnUpdate.Image = global::CaniaBrava.Properties.Resources.synchronize;
            this.btnUpdate.Location = new System.Drawing.Point(691, 318);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(55, 52);
            this.btnUpdate.TabIndex = 6;
            this.btnUpdate.UseVisualStyleBackColor = true;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tPIndicadores);
            this.tabControl1.Controls.Add(this.tbQuery);
            this.tabControl1.Controls.Add(this.tbManejoErrores);
            this.tabControl1.Location = new System.Drawing.Point(12, 97);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(632, 373);
            this.tabControl1.TabIndex = 7;
            // 
            // tPIndicadores
            // 
            this.tPIndicadores.Controls.Add(this.dgIndicadores);
            this.tPIndicadores.Location = new System.Drawing.Point(4, 25);
            this.tPIndicadores.Name = "tPIndicadores";
            this.tPIndicadores.Padding = new System.Windows.Forms.Padding(3);
            this.tPIndicadores.Size = new System.Drawing.Size(624, 344);
            this.tPIndicadores.TabIndex = 0;
            this.tPIndicadores.Text = "Indicadores IOP";
            this.tPIndicadores.UseVisualStyleBackColor = true;
            // 
            // tbQuery
            // 
            this.tbQuery.Controls.Add(this.label3);
            this.tbQuery.Controls.Add(this.label2);
            this.tbQuery.Controls.Add(this.txtQuery2);
            this.tbQuery.Controls.Add(this.txtQuery);
            this.tbQuery.Location = new System.Drawing.Point(4, 25);
            this.tbQuery.Name = "tbQuery";
            this.tbQuery.Padding = new System.Windows.Forms.Padding(3);
            this.tbQuery.Size = new System.Drawing.Size(624, 344);
            this.tbQuery.TabIndex = 1;
            this.tbQuery.Text = "Query";
            this.tbQuery.UseVisualStyleBackColor = true;
            // 
            // txtQuery
            // 
            this.txtQuery.Location = new System.Drawing.Point(6, 25);
            this.txtQuery.Multiline = true;
            this.txtQuery.Name = "txtQuery";
            this.txtQuery.Size = new System.Drawing.Size(612, 122);
            this.txtQuery.TabIndex = 0;
            // 
            // tbManejoErrores
            // 
            this.tbManejoErrores.Controls.Add(this.txtManejoDeErrores);
            this.tbManejoErrores.Location = new System.Drawing.Point(4, 25);
            this.tbManejoErrores.Name = "tbManejoErrores";
            this.tbManejoErrores.Size = new System.Drawing.Size(624, 338);
            this.tbManejoErrores.TabIndex = 2;
            this.tbManejoErrores.Text = "Manejo de errores";
            this.tbManejoErrores.UseVisualStyleBackColor = true;
            // 
            // txtManejoDeErrores
            // 
            this.txtManejoDeErrores.Location = new System.Drawing.Point(4, 4);
            this.txtManejoDeErrores.Multiline = true;
            this.txtManejoDeErrores.Name = "txtManejoDeErrores";
            this.txtManejoDeErrores.Size = new System.Drawing.Size(617, 331);
            this.txtManejoDeErrores.TabIndex = 0;
            // 
            // toolStrip1
            // 
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolSBtnCode});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(814, 27);
            this.toolStrip1.TabIndex = 9;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolSBtnCode
            // 
            this.toolSBtnCode.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolSBtnCode.Image = ((System.Drawing.Image)(resources.GetObject("toolSBtnCode.Image")));
            this.toolSBtnCode.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolSBtnCode.Name = "toolSBtnCode";
            this.toolSBtnCode.Size = new System.Drawing.Size(24, 24);
            this.toolSBtnCode.Text = "Programación";
            this.toolSBtnCode.Click += new System.EventHandler(this.toolSBtnCode_Click);
            // 
            // txtQuery2
            // 
            this.txtQuery2.Location = new System.Drawing.Point(6, 196);
            this.txtQuery2.Multiline = true;
            this.txtQuery2.Name = "txtQuery2";
            this.txtQuery2.Size = new System.Drawing.Size(612, 142);
            this.txtQuery2.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(250, 5);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(149, 17);
            this.label2.TabIndex = 2;
            this.label2.Text = "Querys de vista actual";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(194, 166);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(269, 17);
            this.label3.TabIndex = 3;
            this.label3.Text = "Ultima Query de insercción /Actualización";
            // 
            // ui_objproduccion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(814, 493);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.btnUpdate);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dtpickerFechaObjetivo);
            this.Controls.Add(this.btnSave);
            this.Name = "ui_objproduccion";
            this.Text = "Objetivos diarios de producción";
            ((System.ComponentModel.ISupportInitialize)(this.dgIndicadores)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.tPIndicadores.ResumeLayout(false);
            this.tbQuery.ResumeLayout(false);
            this.tbQuery.PerformLayout();
            this.tbManejoErrores.ResumeLayout(false);
            this.tbManejoErrores.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.DataGridView dgIndicadores;
        private System.Windows.Forms.DateTimePicker dtpickerFechaObjetivo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tPIndicadores;
        private System.Windows.Forms.TabPage tbQuery;
        private System.Windows.Forms.TabPage tbManejoErrores;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton toolSBtnCode;
        private System.Windows.Forms.TextBox txtQuery;
        private System.Windows.Forms.TextBox txtManejoDeErrores;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtQuery2;
    }
}