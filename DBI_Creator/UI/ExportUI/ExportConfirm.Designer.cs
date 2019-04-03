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
            this.browseBtn = new System.Windows.Forms.Button();
            this.locationTxt = new System.Windows.Forms.TextBox();
            this.folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.newBtn = new System.Windows.Forms.Button();
            this.DocCheckBox = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.importFirstPageBtn = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.firstPagePathTextBox = new System.Windows.Forms.TextBox();
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
            this.papersNumberInput.Size = new System.Drawing.Size(148, 20);
            this.papersNumberInput.TabIndex = 2;
            this.papersNumberInput.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // exportBtn
            // 
            this.exportBtn.Location = new System.Drawing.Point(285, 125);
            this.exportBtn.Name = "exportBtn";
            this.exportBtn.Size = new System.Drawing.Size(75, 23);
            this.exportBtn.TabIndex = 3;
            this.exportBtn.Text = "Export";
            this.exportBtn.UseVisualStyleBackColor = true;
            this.exportBtn.Click += new System.EventHandler(this.exportBtn_Click);
            // 
            // browseBtn
            // 
            this.browseBtn.Location = new System.Drawing.Point(285, 48);
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
            this.locationTxt.Size = new System.Drawing.Size(148, 20);
            this.locationTxt.TabIndex = 6;
            // 
            // newBtn
            // 
            this.newBtn.Location = new System.Drawing.Point(285, 12);
            this.newBtn.Name = "newBtn";
            this.newBtn.Size = new System.Drawing.Size(75, 20);
            this.newBtn.TabIndex = 7;
            this.newBtn.Text = "New";
            this.newBtn.UseVisualStyleBackColor = true;
            this.newBtn.Click += new System.EventHandler(this.newBtn_Click);
            // 
            // DocCheckBox
            // 
            this.DocCheckBox.AutoSize = true;
            this.DocCheckBox.Location = new System.Drawing.Point(166, 129);
            this.DocCheckBox.Name = "DocCheckBox";
            this.DocCheckBox.Size = new System.Drawing.Size(90, 17);
            this.DocCheckBox.TabIndex = 8;
            this.DocCheckBox.Text = "Export Docx?";
            this.DocCheckBox.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(55, 53);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Save to:";
            // 
            // importFirstPageBtn
            // 
            this.importFirstPageBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.importFirstPageBtn.Location = new System.Drawing.Point(285, 88);
            this.importFirstPageBtn.Name = "importFirstPageBtn";
            this.importFirstPageBtn.Size = new System.Drawing.Size(75, 23);
            this.importFirstPageBtn.TabIndex = 9;
            this.importFirstPageBtn.Text = "Browse";
            this.importFirstPageBtn.UseVisualStyleBackColor = true;
            this.importFirstPageBtn.Click += new System.EventHandler(this.importFirstPageBtn_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(45, 92);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(57, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "First Page:";
            // 
            // firstPagePathTextBox
            // 
            this.firstPagePathTextBox.Enabled = false;
            this.firstPagePathTextBox.Location = new System.Drawing.Point(108, 90);
            this.firstPagePathTextBox.Name = "firstPagePathTextBox";
            this.firstPagePathTextBox.Size = new System.Drawing.Size(148, 20);
            this.firstPagePathTextBox.TabIndex = 6;
            // 
            // ExportConfirm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(389, 169);
            this.Controls.Add(this.importFirstPageBtn);
            this.Controls.Add(this.DocCheckBox);
            this.Controls.Add(this.newBtn);
            this.Controls.Add(this.firstPagePathTextBox);
            this.Controls.Add(this.locationTxt);
            this.Controls.Add(this.browseBtn);
            this.Controls.Add(this.label3);
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
        private System.Windows.Forms.Button browseBtn;
        private System.Windows.Forms.TextBox locationTxt;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog;
        internal System.Windows.Forms.NumericUpDown papersNumberInput;
        internal System.Windows.Forms.Button newBtn;
        private System.Windows.Forms.CheckBox DocCheckBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button importFirstPageBtn;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox firstPagePathTextBox;
    }
}