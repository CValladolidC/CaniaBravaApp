namespace CaniaBrava
{
    partial class ui_updhorarios
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
            this.txtIdTipHor = new System.Windows.Forms.TextBox();
            this.btnAsignar = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtNameTipHor = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.chkListNominas = new System.Windows.Forms.CheckedListBox();
            this.btnSalir = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.dgvHorarios = new System.Windows.Forms.DataGridView();
            this.btnMore = new System.Windows.Forms.Button();
            this.groupBox3.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvHorarios)).BeginInit();
            this.SuspendLayout();
            // 
            // txtIdTipHor
            // 
            this.txtIdTipHor.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtIdTipHor.Location = new System.Drawing.Point(84, 19);
            this.txtIdTipHor.MaxLength = 3;
            this.txtIdTipHor.Name = "txtIdTipHor";
            this.txtIdTipHor.Size = new System.Drawing.Size(40, 20);
            this.txtIdTipHor.TabIndex = 1;
            this.txtIdTipHor.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtIdTipHor_KeyPress);
            // 
            // btnAsignar
            // 
            this.btnAsignar.Image = global::CaniaBrava.Properties.Resources.SAVE;
            this.btnAsignar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAsignar.Location = new System.Drawing.Point(313, 351);
            this.btnAsignar.Name = "btnAsignar";
            this.btnAsignar.Size = new System.Drawing.Size(90, 33);
            this.btnAsignar.TabIndex = 25;
            this.btnAsignar.Text = "       Grabar";
            this.btnAsignar.UseVisualStyleBackColor = true;
            this.btnAsignar.Click += new System.EventHandler(this.btnAsignar_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label1);
            this.groupBox3.Controls.Add(this.txtIdTipHor);
            this.groupBox3.Controls.Add(this.txtNameTipHor);
            this.groupBox3.Controls.Add(this.label9);
            this.groupBox3.Location = new System.Drawing.Point(4, 6);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(231, 76);
            this.groupBox3.TabIndex = 118;
            this.groupBox3.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 49);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(72, 13);
            this.label1.TabIndex = 121;
            this.label1.Text = "Descripcion  :";
            // 
            // txtNameTipHor
            // 
            this.txtNameTipHor.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtNameTipHor.Location = new System.Drawing.Point(84, 46);
            this.txtNameTipHor.MaxLength = 60;
            this.txtNameTipHor.Name = "txtNameTipHor";
            this.txtNameTipHor.ReadOnly = true;
            this.txtNameTipHor.Size = new System.Drawing.Size(127, 20);
            this.txtNameTipHor.TabIndex = 2;
            this.txtNameTipHor.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtNameTipHor_KeyPress);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(28, 22);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(49, 13);
            this.label9.TabIndex = 0;
            this.label9.Text = "Codigo  :";
            // 
            // chkListNominas
            // 
            this.chkListNominas.FormattingEnabled = true;
            this.chkListNominas.Location = new System.Drawing.Point(4, 94);
            this.chkListNominas.Name = "chkListNominas";
            this.chkListNominas.Size = new System.Drawing.Size(231, 244);
            this.chkListNominas.TabIndex = 120;
            this.chkListNominas.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.chkListNominas_ItemCheck);
            // 
            // btnSalir
            // 
            this.btnSalir.Image = global::CaniaBrava.Properties.Resources.CLOSE;
            this.btnSalir.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSalir.Location = new System.Drawing.Point(409, 351);
            this.btnSalir.Name = "btnSalir";
            this.btnSalir.Size = new System.Drawing.Size(90, 33);
            this.btnSalir.TabIndex = 121;
            this.btnSalir.Text = "   Salir";
            this.btnSalir.UseVisualStyleBackColor = true;
            this.btnSalir.Click += new System.EventHandler(this.btnSalir_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.dgvHorarios);
            this.groupBox1.Location = new System.Drawing.Point(241, 6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(258, 339);
            this.groupBox1.TabIndex = 122;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Horarios";
            // 
            // dgvHorarios
            // 
            this.dgvHorarios.AllowUserToAddRows = false;
            this.dgvHorarios.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvHorarios.Enabled = false;
            this.dgvHorarios.Location = new System.Drawing.Point(6, 19);
            this.dgvHorarios.Name = "dgvHorarios";
            this.dgvHorarios.Size = new System.Drawing.Size(246, 313);
            this.dgvHorarios.TabIndex = 0;
            this.dgvHorarios.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvHorarios_CellValueChanged);
            this.dgvHorarios.SelectionChanged += new System.EventHandler(this.dgvHorarios_SelectionChanged);
            this.dgvHorarios.UserDeletingRow += new System.Windows.Forms.DataGridViewRowCancelEventHandler(this.dgvHorarios_UserDeletingRow);
            // 
            // btnMore
            // 
            this.btnMore.Image = global::CaniaBrava.Properties.Resources.zoommas;
            this.btnMore.Location = new System.Drawing.Point(274, 351);
            this.btnMore.Name = "btnMore";
            this.btnMore.Size = new System.Drawing.Size(33, 33);
            this.btnMore.TabIndex = 124;
            this.btnMore.UseVisualStyleBackColor = true;
            this.btnMore.Visible = false;
            this.btnMore.Click += new System.EventHandler(this.btnMore_Click);
            // 
            // ui_updhorarios
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(508, 390);
            this.Controls.Add(this.btnMore);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnSalir);
            this.Controls.Add(this.chkListNominas);
            this.Controls.Add(this.btnAsignar);
            this.Controls.Add(this.groupBox3);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(524, 429);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(524, 429);
            this.Name = "ui_updhorarios";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Tipo de Horario";
            this.Load += new System.EventHandler(this.ui_updhorarios_Load);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvHorarios)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox txtIdTipHor;
        private System.Windows.Forms.Button btnAsignar;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TextBox txtNameTipHor;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckedListBox chkListNominas;
        private System.Windows.Forms.Button btnSalir;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DataGridView dgvHorarios;
        private System.Windows.Forms.Button btnMore;
    }
}