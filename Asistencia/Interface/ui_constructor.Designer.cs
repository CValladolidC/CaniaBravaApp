namespace CaniaBrava
{
    partial class ui_constructor
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
            this.components = new System.ComponentModel.Container();
            this.txtFormula = new System.Windows.Forms.TextBox();
            this.treeViewVariables = new System.Windows.Forms.TreeView();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.cmdAgregar = new System.Windows.Forms.ToolStripMenuItem();
            this.btnAceptar = new System.Windows.Forms.Button();
            this.gbxSearchByText = new System.Windows.Forms.GroupBox();
            this.btnNodeTextSearch = new System.Windows.Forms.Button();
            this.txtNodeTextSearch = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            this.button7 = new System.Windows.Forms.Button();
            this.button8 = new System.Windows.Forms.Button();
            this.button9 = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.button11 = new System.Windows.Forms.Button();
            this.button12 = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.button13 = new System.Windows.Forms.Button();
            this.button16 = new System.Windows.Forms.Button();
            this.button19 = new System.Windows.Forms.Button();
            this.button20 = new System.Windows.Forms.Button();
            this.button21 = new System.Windows.Forms.Button();
            this.button10 = new System.Windows.Forms.Button();
            this.button22 = new System.Windows.Forms.Button();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.pictureValidAsk = new System.Windows.Forms.PictureBox();
            this.pictureValidBad = new System.Windows.Forms.PictureBox();
            this.pictureValidOk = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.lblNombre = new System.Windows.Forms.Label();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.button14 = new System.Windows.Forms.Button();
            this.contextMenuStrip1.SuspendLayout();
            this.gbxSearchByText.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox7.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureValidAsk)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureValidBad)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureValidOk)).BeginInit();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtFormula
            // 
            this.txtFormula.BackColor = System.Drawing.SystemColors.HighlightText;
            this.txtFormula.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtFormula.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFormula.Location = new System.Drawing.Point(486, 280);
            this.txtFormula.MaxLength = 500;
            this.txtFormula.Multiline = true;
            this.txtFormula.Name = "txtFormula";
            this.txtFormula.ReadOnly = true;
            this.txtFormula.Size = new System.Drawing.Size(399, 145);
            this.txtFormula.TabIndex = 2;
            // 
            // treeViewVariables
            // 
            this.treeViewVariables.ContextMenuStrip = this.contextMenuStrip1;
            this.treeViewVariables.Location = new System.Drawing.Point(11, 71);
            this.treeViewVariables.Name = "treeViewVariables";
            this.treeViewVariables.Size = new System.Drawing.Size(469, 508);
            this.treeViewVariables.TabIndex = 3;
            this.treeViewVariables.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeViewVariables_AfterSelect);
            this.treeViewVariables.Click += new System.EventHandler(this.treeViewVariables_Click);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cmdAgregar});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(196, 26);
            // 
            // cmdAgregar
            // 
            this.cmdAgregar.Name = "cmdAgregar";
            this.cmdAgregar.Size = new System.Drawing.Size(195, 22);
            this.cmdAgregar.Text = "AGREGAR A FORMULA";
            this.cmdAgregar.Click += new System.EventHandler(this.cmdAgregar_Click);
            // 
            // btnAceptar
            // 
            this.btnAceptar.Location = new System.Drawing.Point(163, 19);
            this.btnAceptar.Name = "btnAceptar";
            this.btnAceptar.Size = new System.Drawing.Size(75, 23);
            this.btnAceptar.TabIndex = 4;
            this.btnAceptar.Text = "Aceptar";
            this.btnAceptar.UseVisualStyleBackColor = true;
            this.btnAceptar.Click += new System.EventHandler(this.button1_Click);
            // 
            // gbxSearchByText
            // 
            this.gbxSearchByText.Controls.Add(this.btnNodeTextSearch);
            this.gbxSearchByText.Controls.Add(this.txtNodeTextSearch);
            this.gbxSearchByText.Controls.Add(this.label3);
            this.gbxSearchByText.Location = new System.Drawing.Point(486, 71);
            this.gbxSearchByText.Name = "gbxSearchByText";
            this.gbxSearchByText.Size = new System.Drawing.Size(399, 64);
            this.gbxSearchByText.TabIndex = 6;
            this.gbxSearchByText.TabStop = false;
            this.gbxSearchByText.Text = "Buscar por Nombre Descriptivo";
            // 
            // btnNodeTextSearch
            // 
            this.btnNodeTextSearch.Location = new System.Drawing.Point(335, 26);
            this.btnNodeTextSearch.Name = "btnNodeTextSearch";
            this.btnNodeTextSearch.Size = new System.Drawing.Size(55, 23);
            this.btnNodeTextSearch.TabIndex = 7;
            this.btnNodeTextSearch.Text = "Buscar";
            this.btnNodeTextSearch.UseVisualStyleBackColor = true;
            this.btnNodeTextSearch.Click += new System.EventHandler(this.btnNodeTextSearch_Click);
            // 
            // txtNodeTextSearch
            // 
            this.txtNodeTextSearch.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtNodeTextSearch.Location = new System.Drawing.Point(121, 28);
            this.txtNodeTextSearch.Name = "txtNodeTextSearch";
            this.txtNodeTextSearch.Size = new System.Drawing.Size(207, 20);
            this.txtNodeTextSearch.TabIndex = 6;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 31);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(106, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Nombre Descriptivo :";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(7, 14);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(43, 23);
            this.button1.TabIndex = 8;
            this.button1.Text = "1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(56, 14);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(43, 23);
            this.button2.TabIndex = 9;
            this.button2.Text = "2";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(105, 14);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(43, 23);
            this.button3.TabIndex = 10;
            this.button3.Text = "3";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(105, 43);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(43, 23);
            this.button4.TabIndex = 13;
            this.button4.Text = "6";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(56, 43);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(43, 23);
            this.button5.TabIndex = 12;
            this.button5.Text = "5";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // button6
            // 
            this.button6.Location = new System.Drawing.Point(7, 43);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(43, 23);
            this.button6.TabIndex = 11;
            this.button6.Text = "4";
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.button6_Click);
            // 
            // button7
            // 
            this.button7.Location = new System.Drawing.Point(105, 72);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(43, 23);
            this.button7.TabIndex = 16;
            this.button7.Text = "9";
            this.button7.UseVisualStyleBackColor = true;
            this.button7.Click += new System.EventHandler(this.button7_Click);
            // 
            // button8
            // 
            this.button8.Location = new System.Drawing.Point(56, 72);
            this.button8.Name = "button8";
            this.button8.Size = new System.Drawing.Size(43, 23);
            this.button8.TabIndex = 15;
            this.button8.Text = "8";
            this.button8.UseVisualStyleBackColor = true;
            this.button8.Click += new System.EventHandler(this.button8_Click);
            // 
            // button9
            // 
            this.button9.Location = new System.Drawing.Point(7, 72);
            this.button9.Name = "button9";
            this.button9.Size = new System.Drawing.Size(43, 23);
            this.button9.TabIndex = 14;
            this.button9.Text = "7";
            this.button9.UseVisualStyleBackColor = true;
            this.button9.Click += new System.EventHandler(this.button9_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.button7);
            this.groupBox1.Controls.Add(this.button8);
            this.groupBox1.Controls.Add(this.button9);
            this.groupBox1.Controls.Add(this.button4);
            this.groupBox1.Controls.Add(this.button5);
            this.groupBox1.Controls.Add(this.button6);
            this.groupBox1.Controls.Add(this.button3);
            this.groupBox1.Controls.Add(this.button2);
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Location = new System.Drawing.Point(486, 141);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(155, 109);
            this.groupBox1.TabIndex = 17;
            this.groupBox1.TabStop = false;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.button14);
            this.groupBox2.Controls.Add(this.button11);
            this.groupBox2.Controls.Add(this.button12);
            this.groupBox2.Location = new System.Drawing.Point(675, 141);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(64, 109);
            this.groupBox2.TabIndex = 18;
            this.groupBox2.TabStop = false;
            // 
            // button11
            // 
            this.button11.Location = new System.Drawing.Point(11, 71);
            this.button11.Name = "button11";
            this.button11.Size = new System.Drawing.Size(43, 23);
            this.button11.TabIndex = 15;
            this.button11.Text = ")";
            this.button11.UseVisualStyleBackColor = true;
            this.button11.Click += new System.EventHandler(this.button11_Click);
            // 
            // button12
            // 
            this.button12.Location = new System.Drawing.Point(11, 42);
            this.button12.Name = "button12";
            this.button12.Size = new System.Drawing.Size(43, 23);
            this.button12.TabIndex = 14;
            this.button12.Text = "(";
            this.button12.UseVisualStyleBackColor = true;
            this.button12.Click += new System.EventHandler(this.button12_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.button13);
            this.groupBox3.Controls.Add(this.button16);
            this.groupBox3.Controls.Add(this.button19);
            this.groupBox3.Controls.Add(this.button20);
            this.groupBox3.Controls.Add(this.button21);
            this.groupBox3.Location = new System.Drawing.Point(773, 141);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(110, 109);
            this.groupBox3.TabIndex = 19;
            this.groupBox3.TabStop = false;
            // 
            // button13
            // 
            this.button13.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button13.Location = new System.Drawing.Point(9, 72);
            this.button13.Name = "button13";
            this.button13.Size = new System.Drawing.Size(92, 23);
            this.button13.TabIndex = 13;
            this.button13.Text = "Punto Decimal";
            this.button13.UseVisualStyleBackColor = true;
            this.button13.Click += new System.EventHandler(this.button13_Click);
            // 
            // button16
            // 
            this.button16.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button16.Location = new System.Drawing.Point(58, 43);
            this.button16.Name = "button16";
            this.button16.Size = new System.Drawing.Size(43, 23);
            this.button16.TabIndex = 12;
            this.button16.Text = "X";
            this.button16.UseVisualStyleBackColor = true;
            this.button16.Click += new System.EventHandler(this.button16_Click);
            // 
            // button19
            // 
            this.button19.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button19.Location = new System.Drawing.Point(9, 43);
            this.button19.Name = "button19";
            this.button19.Size = new System.Drawing.Size(43, 23);
            this.button19.TabIndex = 11;
            this.button19.Text = "+";
            this.button19.UseVisualStyleBackColor = true;
            this.button19.Click += new System.EventHandler(this.button19_Click);
            // 
            // button20
            // 
            this.button20.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button20.Location = new System.Drawing.Point(58, 14);
            this.button20.Name = "button20";
            this.button20.Size = new System.Drawing.Size(43, 23);
            this.button20.TabIndex = 9;
            this.button20.Text = "-";
            this.button20.UseVisualStyleBackColor = true;
            this.button20.Click += new System.EventHandler(this.button20_Click);
            // 
            // button21
            // 
            this.button21.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button21.Location = new System.Drawing.Point(9, 14);
            this.button21.Name = "button21";
            this.button21.Size = new System.Drawing.Size(43, 23);
            this.button21.TabIndex = 8;
            this.button21.Text = "/";
            this.button21.UseVisualStyleBackColor = true;
            this.button21.Click += new System.EventHandler(this.button21_Click);
            // 
            // button10
            // 
            this.button10.Location = new System.Drawing.Point(491, 431);
            this.button10.Name = "button10";
            this.button10.Size = new System.Drawing.Size(112, 23);
            this.button10.TabIndex = 21;
            this.button10.Text = "Borrar Caracteres";
            this.button10.UseVisualStyleBackColor = true;
            this.button10.Click += new System.EventHandler(this.button10_Click);
            // 
            // button22
            // 
            this.button22.Location = new System.Drawing.Point(607, 431);
            this.button22.Name = "button22";
            this.button22.Size = new System.Drawing.Size(112, 23);
            this.button22.TabIndex = 22;
            this.button22.Text = "Limpiar Fórmula";
            this.button22.UseVisualStyleBackColor = true;
            this.button22.Click += new System.EventHandler(this.button22_Click);
            // 
            // groupBox7
            // 
            this.groupBox7.Controls.Add(this.btnCancelar);
            this.groupBox7.Controls.Add(this.btnAceptar);
            this.groupBox7.Location = new System.Drawing.Point(486, 527);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(399, 52);
            this.groupBox7.TabIndex = 29;
            this.groupBox7.TabStop = false;
            // 
            // btnCancelar
            // 
            this.btnCancelar.Location = new System.Drawing.Point(261, 19);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(75, 23);
            this.btnCancelar.TabIndex = 28;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = true;
            this.btnCancelar.Click += new System.EventHandler(this.button14_Click_1);
            // 
            // pictureValidAsk
            // 
            this.pictureValidAsk.ErrorImage = null;
            this.pictureValidAsk.Image = global::CaniaBrava.Properties.Resources.ASK;
            this.pictureValidAsk.InitialImage = null;
            this.pictureValidAsk.Location = new System.Drawing.Point(855, 431);
            this.pictureValidAsk.Name = "pictureValidAsk";
            this.pictureValidAsk.Size = new System.Drawing.Size(23, 17);
            this.pictureValidAsk.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureValidAsk.TabIndex = 30;
            this.pictureValidAsk.TabStop = false;
            // 
            // pictureValidBad
            // 
            this.pictureValidBad.ErrorImage = null;
            this.pictureValidBad.Image = global::CaniaBrava.Properties.Resources.BAD;
            this.pictureValidBad.InitialImage = null;
            this.pictureValidBad.Location = new System.Drawing.Point(850, 430);
            this.pictureValidBad.Name = "pictureValidBad";
            this.pictureValidBad.Size = new System.Drawing.Size(32, 18);
            this.pictureValidBad.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureValidBad.TabIndex = 29;
            this.pictureValidBad.TabStop = false;
            this.pictureValidBad.Visible = false;
            // 
            // pictureValidOk
            // 
            this.pictureValidOk.ErrorImage = null;
            this.pictureValidOk.Image = global::CaniaBrava.Properties.Resources.OK;
            this.pictureValidOk.InitialImage = null;
            this.pictureValidOk.Location = new System.Drawing.Point(849, 431);
            this.pictureValidOk.Name = "pictureValidOk";
            this.pictureValidOk.Size = new System.Drawing.Size(34, 17);
            this.pictureValidOk.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureValidOk.TabIndex = 28;
            this.pictureValidOk.TabStop = false;
            this.pictureValidOk.Visible = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(486, 264);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(50, 13);
            this.label1.TabIndex = 8;
            this.label1.Text = "Fórmula :";
            // 
            // lblNombre
            // 
            this.lblNombre.AutoSize = true;
            this.lblNombre.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNombre.Location = new System.Drawing.Point(164, 20);
            this.lblNombre.Name = "lblNombre";
            this.lblNombre.Size = new System.Drawing.Size(189, 16);
            this.lblNombre.TabIndex = 27;
            this.lblNombre.Text = "CONCEPTO DE PLANILLA";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.lblNombre);
            this.groupBox4.Controls.Add(this.label4);
            this.groupBox4.Location = new System.Drawing.Point(11, 12);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(874, 52);
            this.groupBox4.TabIndex = 30;
            this.groupBox4.TabStop = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(5, 20);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(160, 16);
            this.label4.TabIndex = 26;
            this.label4.Text = "Concepto de Planilla :";
            // 
            // button14
            // 
            this.button14.Location = new System.Drawing.Point(11, 15);
            this.button14.Name = "button14";
            this.button14.Size = new System.Drawing.Size(43, 23);
            this.button14.TabIndex = 16;
            this.button14.Text = "0";
            this.button14.UseVisualStyleBackColor = true;
            this.button14.Click += new System.EventHandler(this.button14_Click_2);
            // 
            // ui_constructor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.HighlightText;
            this.ClientSize = new System.Drawing.Size(897, 591);
            this.ControlBox = false;
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pictureValidOk);
            this.Controls.Add(this.groupBox7);
            this.Controls.Add(this.pictureValidBad);
            this.Controls.Add(this.pictureValidAsk);
            this.Controls.Add(this.button22);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.button10);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.gbxSearchByText);
            this.Controls.Add(this.treeViewVariables);
            this.Controls.Add(this.txtFormula);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ui_constructor";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Constructor de Fórmulas";
            this.Load += new System.EventHandler(this.ui_constructor_Load);
            this.contextMenuStrip1.ResumeLayout(false);
            this.gbxSearchByText.ResumeLayout(false);
            this.gbxSearchByText.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox7.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureValidAsk)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureValidBad)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureValidOk)).EndInit();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtFormula;
        private System.Windows.Forms.TreeView treeViewVariables;
        private System.Windows.Forms.Button btnAceptar;
        private System.Windows.Forms.GroupBox gbxSearchByText;
        private System.Windows.Forms.Button btnNodeTextSearch;
        private System.Windows.Forms.TextBox txtNodeTextSearch;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.Button button7;
        private System.Windows.Forms.Button button8;
        private System.Windows.Forms.Button button9;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button button11;
        private System.Windows.Forms.Button button12;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button button16;
        private System.Windows.Forms.Button button19;
        private System.Windows.Forms.Button button20;
        private System.Windows.Forms.Button button21;
        private System.Windows.Forms.Button button13;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem cmdAgregar;
        private System.Windows.Forms.Button button10;
        private System.Windows.Forms.Button button22;
        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.PictureBox pictureValidAsk;
        private System.Windows.Forms.PictureBox pictureValidBad;
        private System.Windows.Forms.PictureBox pictureValidOk;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.Label lblNombre;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button button14;
    }
}