namespace CaniaBrava
{
    partial class BackgroundWorker
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
            this.Star = new System.Windows.Forms.Button();
            this.Cancel = new System.Windows.Forms.Button();
            this.SourceFile = new System.Windows.Forms.TextBox();
            this.CompareString = new System.Windows.Forms.TextBox();
            this.WordsCounted = new System.Windows.Forms.TextBox();
            this.LinesCounted = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.SuspendLayout();
            // 
            // Star
            // 
            this.Star.Location = new System.Drawing.Point(300, 448);
            this.Star.Name = "Star";
            this.Star.Size = new System.Drawing.Size(75, 23);
            this.Star.TabIndex = 0;
            this.Star.Text = "Star";
            this.Star.UseVisualStyleBackColor = true;
            this.Star.Click += new System.EventHandler(this.Star_Click);
            // 
            // Cancel
            // 
            this.Cancel.Location = new System.Drawing.Point(546, 448);
            this.Cancel.Name = "Cancel";
            this.Cancel.Size = new System.Drawing.Size(75, 23);
            this.Cancel.TabIndex = 1;
            this.Cancel.Text = "Cancel";
            this.Cancel.UseVisualStyleBackColor = true;
            this.Cancel.Click += new System.EventHandler(this.Cancel_Click);
            // 
            // SourceFile
            // 
            this.SourceFile.Location = new System.Drawing.Point(43, 22);
            this.SourceFile.Multiline = true;
            this.SourceFile.Name = "SourceFile";
            this.SourceFile.Size = new System.Drawing.Size(198, 374);
            this.SourceFile.TabIndex = 2;
            // 
            // CompareString
            // 
            this.CompareString.Location = new System.Drawing.Point(247, 22);
            this.CompareString.Multiline = true;
            this.CompareString.Name = "CompareString";
            this.CompareString.Size = new System.Drawing.Size(198, 374);
            this.CompareString.TabIndex = 3;
            // 
            // WordsCounted
            // 
            this.WordsCounted.Location = new System.Drawing.Point(451, 22);
            this.WordsCounted.Multiline = true;
            this.WordsCounted.Name = "WordsCounted";
            this.WordsCounted.Size = new System.Drawing.Size(198, 374);
            this.WordsCounted.TabIndex = 4;
            this.WordsCounted.Text = "0";
            // 
            // LinesCounted
            // 
            this.LinesCounted.Location = new System.Drawing.Point(655, 22);
            this.LinesCounted.Multiline = true;
            this.LinesCounted.Name = "LinesCounted";
            this.LinesCounted.Size = new System.Drawing.Size(198, 374);
            this.LinesCounted.TabIndex = 5;
            this.LinesCounted.Text = "0";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(103, 408);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(92, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Archivo de Origen";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(297, 408);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(79, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "Compare String";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(501, 408);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(85, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "Matching Words";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(714, 408);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(75, 13);
            this.label4.TabIndex = 9;
            this.label4.Text = "Lines Counted";
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.WorkerReportsProgress = true;
            this.backgroundWorker1.WorkerSupportsCancellation = true;
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
            this.backgroundWorker1.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker1_RunWorkerCompleted);
            this.backgroundWorker1.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundWorker1_ProgressChanged);
            // 
            // BackgroundWorker
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(896, 525);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.LinesCounted);
            this.Controls.Add(this.WordsCounted);
            this.Controls.Add(this.CompareString);
            this.Controls.Add(this.SourceFile);
            this.Controls.Add(this.Cancel);
            this.Controls.Add(this.Star);
            this.Name = "BackgroundWorker";
            this.Text = "BackgroundWorker";
            this.Load += new System.EventHandler(this.BackgroundWorker_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button Star;
        private System.Windows.Forms.Button Cancel;
        private System.Windows.Forms.TextBox SourceFile;
        private System.Windows.Forms.TextBox CompareString;
        private System.Windows.Forms.TextBox WordsCounted;
        private System.Windows.Forms.TextBox LinesCounted;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
    }
}