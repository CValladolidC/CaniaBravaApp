namespace CaniaBrava
{
    partial class ui_maespdt
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
            this.toolStripForm = new System.Windows.Forms.ToolStrip();
            this.btnEditar = new System.Windows.Forms.ToolStripButton();
            this.btnActualizar = new System.Windows.Forms.ToolStripButton();
            this.btnSalir = new System.Windows.Forms.ToolStripButton();
            this.dgvMaesGen = new System.Windows.Forms.DataGridView();
            this.toolStripForm.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMaesGen)).BeginInit();
            this.SuspendLayout();
            // 
            // toolStripForm
            // 
            this.toolStripForm.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.toolStripForm.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnEditar,
            this.btnActualizar,
            this.btnSalir});
            this.toolStripForm.Location = new System.Drawing.Point(0, 463);
            this.toolStripForm.Name = "toolStripForm";
            this.toolStripForm.ShowItemToolTips = false;
            this.toolStripForm.Size = new System.Drawing.Size(738, 25);
            this.toolStripForm.TabIndex = 5;
            this.toolStripForm.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.toolStripForm_ItemClicked);
            // 
            // btnEditar
            // 
            this.btnEditar.Image = global::CaniaBrava.Properties.Resources.OPEN;
            this.btnEditar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnEditar.Name = "btnEditar";
            this.btnEditar.Size = new System.Drawing.Size(57, 22);
            this.btnEditar.Text = "Editar";
            this.btnEditar.Click += new System.EventHandler(this.btnEditar_Click);
            // 
            // btnActualizar
            // 
            this.btnActualizar.Image = global::CaniaBrava.Properties.Resources.refresh;
            this.btnActualizar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnActualizar.Name = "btnActualizar";
            this.btnActualizar.Size = new System.Drawing.Size(79, 22);
            this.btnActualizar.Text = "Actualizar";
            this.btnActualizar.Click += new System.EventHandler(this.btnActualizar_Click);
            // 
            // btnSalir
            // 
            this.btnSalir.Image = global::CaniaBrava.Properties.Resources.CLOSE;
            this.btnSalir.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSalir.Name = "btnSalir";
            this.btnSalir.Size = new System.Drawing.Size(49, 22);
            this.btnSalir.Text = "Salir";
            this.btnSalir.Click += new System.EventHandler(this.btnSalir_Click);
            // 
            // dgvMaesGen
            // 
            this.dgvMaesGen.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvMaesGen.Location = new System.Drawing.Point(0, 0);
            this.dgvMaesGen.Name = "dgvMaesGen";
            this.dgvMaesGen.Size = new System.Drawing.Size(738, 460);
            this.dgvMaesGen.TabIndex = 4;
            this.dgvMaesGen.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvMaesGen_CellContentClick);
            // 
            // ui_maespdt
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.HighlightText;
            this.ClientSize = new System.Drawing.Size(738, 488);
            this.ControlBox = false;
            this.Controls.Add(this.toolStripForm);
            this.Controls.Add(this.dgvMaesGen);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ui_maespdt";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Tablas Maestras PDT - Planilla Electrónica";
            this.Load += new System.EventHandler(this.ui_maespdt_Load);
            this.toolStripForm.ResumeLayout(false);
            this.toolStripForm.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMaesGen)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStripForm;
        private System.Windows.Forms.ToolStripButton btnEditar;
        public System.Windows.Forms.ToolStripButton btnActualizar;
        private System.Windows.Forms.ToolStripButton btnSalir;
        private System.Windows.Forms.DataGridView dgvMaesGen;
    }
}