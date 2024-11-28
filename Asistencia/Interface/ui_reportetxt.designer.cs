namespace CaniaBrava
{
    partial class ui_reportetxt
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
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.btnImprimir = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.btnZoomMenos = new System.Windows.Forms.ToolStripButton();
            this.btnZoomMas = new System.Windows.Forms.ToolStripButton();
            this.btnZoomNormal = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.btnSeleccionar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnSalir = new System.Windows.Forms.ToolStripButton();
            this.rtxtDoc = new System.Windows.Forms.RichTextBox();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.BackColor = System.Drawing.Color.White;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton1,
            this.btnImprimir,
            this.toolStripSeparator2,
            this.btnZoomMenos,
            this.btnZoomMas,
            this.btnZoomNormal,
            this.toolStripSeparator3,
            this.btnSeleccionar,
            this.toolStripSeparator1,
            this.btnSalir});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1163, 41);
            this.toolStrip1.TabIndex = 84;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton1.Image = global::CaniaBrava.Properties.Resources.SAVE;
            this.toolStripButton1.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(26, 38);
            this.toolStripButton1.Text = "Guardar";
            this.toolStripButton1.Click += new System.EventHandler(this.toolStripButton1_Click);
            // 
            // btnImprimir
            // 
            this.btnImprimir.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnImprimir.Image = global::CaniaBrava.Properties.Resources.PRINT;
            this.btnImprimir.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnImprimir.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnImprimir.Name = "btnImprimir";
            this.btnImprimir.Size = new System.Drawing.Size(32, 38);
            this.btnImprimir.Text = "Imprimir";
            this.btnImprimir.Click += new System.EventHandler(this.btnImprimir_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 41);
            // 
            // btnZoomMenos
            // 
            this.btnZoomMenos.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnZoomMenos.Image = global::CaniaBrava.Properties.Resources.zoommenos;
            this.btnZoomMenos.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnZoomMenos.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnZoomMenos.Name = "btnZoomMenos";
            this.btnZoomMenos.Size = new System.Drawing.Size(36, 38);
            this.btnZoomMenos.Text = "Disminuir";
            this.btnZoomMenos.Click += new System.EventHandler(this.btnZoomMenos_Click);
            // 
            // btnZoomMas
            // 
            this.btnZoomMas.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnZoomMas.Image = global::CaniaBrava.Properties.Resources.zoommas;
            this.btnZoomMas.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnZoomMas.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnZoomMas.Name = "btnZoomMas";
            this.btnZoomMas.Size = new System.Drawing.Size(40, 38);
            this.btnZoomMas.Text = "  Aumentar";
            this.btnZoomMas.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnZoomMas.Click += new System.EventHandler(this.btnZoomMas_Click);
            // 
            // btnZoomNormal
            // 
            this.btnZoomNormal.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnZoomNormal.Image = global::CaniaBrava.Properties.Resources.NEW;
            this.btnZoomNormal.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnZoomNormal.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnZoomNormal.Name = "btnZoomNormal";
            this.btnZoomNormal.Size = new System.Drawing.Size(38, 38);
            this.btnZoomNormal.Text = "100%";
            this.btnZoomNormal.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnZoomNormal.Click += new System.EventHandler(this.btnZoomNormal_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 41);
            // 
            // btnSeleccionar
            // 
            this.btnSeleccionar.Image = global::CaniaBrava.Properties.Resources.seleccionar;
            this.btnSeleccionar.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnSeleccionar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSeleccionar.Name = "btnSeleccionar";
            this.btnSeleccionar.Size = new System.Drawing.Size(130, 38);
            this.btnSeleccionar.Text = "Seleccionar todo";
            this.btnSeleccionar.Click += new System.EventHandler(this.btnSeleccionar_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 41);
            // 
            // btnSalir
            // 
            this.btnSalir.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnSalir.Image = global::CaniaBrava.Properties.Resources.CLOSE;
            this.btnSalir.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnSalir.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSalir.Name = "btnSalir";
            this.btnSalir.Size = new System.Drawing.Size(23, 38);
            this.btnSalir.Text = "Salir";
            this.btnSalir.Click += new System.EventHandler(this.btnSalir_Click);
            // 
            // rtxtDoc
            // 
            this.rtxtDoc.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.rtxtDoc.BackColor = System.Drawing.SystemColors.Window;
            this.rtxtDoc.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rtxtDoc.Location = new System.Drawing.Point(0, 43);
            this.rtxtDoc.Name = "rtxtDoc";
            this.rtxtDoc.ReadOnly = true;
            this.rtxtDoc.Size = new System.Drawing.Size(1163, 458);
            this.rtxtDoc.TabIndex = 83;
            this.rtxtDoc.Text = "";
            this.rtxtDoc.WordWrap = false;
            // 
            // ui_reportetxt
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(1163, 501);
            this.ControlBox = true;
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.rtxtDoc);
            this.Name = "ui_reportetxt";
            this.ShowIcon = false;
            this.Text = "Visualizador de Reportes";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.ui_reportetxt_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox rtxtDoc;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.ToolStripButton btnImprimir;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton btnSalir;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.ToolStripButton btnZoomMenos;
        private System.Windows.Forms.ToolStripButton btnZoomMas;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton btnZoomNormal;
        private System.Windows.Forms.ToolStripButton btnSeleccionar;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
    }
}
