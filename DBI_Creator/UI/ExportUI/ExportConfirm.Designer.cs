namespace DBI202_Creator.UI.ExportUI
{
    partial class ExportConfirm
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
            this.label2 = new System.Windows.Forms.Label();
            this.papersNumberInput = new System.Windows.Forms.NumericUpDown();
            this.exportBtn = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.browseBtn = new System.Windows.Forms.Button();
            this.locationTxt = new System.Windows.Forms.TextBox();
            this.folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.newBtn = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.papersNumberInput)).BeginInit();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 14);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(95, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Number of Papers:";
            // 
            // papersNumberInput
            // 
            this.papersNumberInput.Location = new System.Drawing.Point(108, 12);
            this.papersNumberInput.Name = "papersNumberInput";
            this.papersNumberInput.Size = new System.Drawing.Size(87, 20);
            this.papersNumberInput.TabIndex = 2;
            this.papersNumberInput.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // exportBtn
            // 
            this.exportBtn.Location = new System.Drawing.Point(221, 86);
            this.exportBtn.Name = "exportBtn";
            this.exportBtn.Size = new System.Drawing.Size(75, 23);
            this.exportBtn.TabIndex = 3;
            this.exportBtn.Text = "Export";
            this.exportBtn.UseVisualStyleBackColor = true;
            this.exportBtn.Click += new System.EventHandler(this.exportBtn_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(55, 54);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Save to:";
            // 
            // browseBtn
            // 
            this.browseBtn.Location = new System.Drawing.Point(108, 86);
            this.browseBtn.Name = "browseBtn";
            this.browseBtn.Size = new System.Drawing.Size(75, 23);
            this.browseBtn.TabIndex = 5;
            this.browseBtn.Text = "Browse";
            this.browseBtn.UseVisualStyleBackColor = true;
            this.browseBtn.Click += new System.EventHandler(this.browseBtn_Click);
            // 
            // locationTxt
            // 
            this.locationTxt.Enabled = false;
            this.locationTxt.Location = new System.Drawing.Point(108, 51);
            this.locationTxt.Name = "locationTxt";
            this.locationTxt.Size = new System.Drawing.Size(188, 20);
            this.locationTxt.TabIndex = 6;
            // 
            // newBtn
            // 
            this.newBtn.Location = new System.Drawing.Point(221, 12);
            this.newBtn.Name = "newBtn";
            this.newBtn.Size = new System.Drawing.Size(75, 20);
            this.newBtn.TabIndex = 7;
            this.newBtn.Text = "New";
            this.newBtn.UseVisualStyleBackColor = true;
            this.newBtn.Click += new System.EventHandler(this.newBtn_Click);
            // 
            // ExportConfirm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(319, 126);
            this.Controls.Add(this.newBtn);
            this.Controls.Add(this.locationTxt);
            this.Controls.Add(this.browseBtn);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.exportBtn);
            this.Controls.Add(this.papersNumberInput);
            this.Controls.Add(this.label2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "ExportConfirm";
            this.ShowInTaskbar = false;
            this.Text = "ExportConfirm";
            ((System.ComponentModel.ISupportInitialize)(this.papersNumberInput)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button exportBtn;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button browseBtn;
        private System.Windows.Forms.TextBox locationTxt;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog;
        internal System.Windows.Forms.NumericUpDown papersNumberInput;
        internal System.Windows.Forms.Button newBtn;
    }
}