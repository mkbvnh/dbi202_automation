﻿namespace DBI202_Creator.UI.CandidateUI
{
    partial class CandidatePanel
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.Button deleteCandidateBtn;
            this.browseImgBtn = new System.Windows.Forms.Button();
            this.imgLabel = new System.Windows.Forms.Label();
            this.contentTxt = new System.Windows.Forms.RichTextBox();
            this.contentLabel = new System.Windows.Forms.Label();
            this.questionTypeComboBox = new System.Windows.Forms.ComboBox();
            this.typeLabel = new System.Windows.Forms.Label();
            this.imgPreview = new System.Windows.Forms.LinkLabel();
            this.browseImgDialog = new System.Windows.Forms.OpenFileDialog();
            this.label1 = new System.Windows.Forms.Label();
            this.solutionTxt = new System.Windows.Forms.RichTextBox();
            this.testQueryTxt = new System.Windows.Forms.RichTextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.requireSortCheckBox = new System.Windows.Forms.CheckBox();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.checkColumnNameCheckbox = new System.Windows.Forms.CheckBox();
            this.separator1 = new System.Windows.Forms.Label();
            this.checkDistinctCheckbox = new System.Windows.Forms.CheckBox();
            this.insertTcBtn = new System.Windows.Forms.Button();
            this.validateTcBtn = new System.Windows.Forms.Button();
            deleteCandidateBtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // deleteCandidateBtn
            // 
            deleteCandidateBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            deleteCandidateBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            deleteCandidateBtn.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            deleteCandidateBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            deleteCandidateBtn.Location = new System.Drawing.Point(808, 18);
            deleteCandidateBtn.Name = "deleteCandidateBtn";
            deleteCandidateBtn.Size = new System.Drawing.Size(115, 23);
            deleteCandidateBtn.TabIndex = 32;
            deleteCandidateBtn.Text = "Remove Candidate";
            deleteCandidateBtn.UseVisualStyleBackColor = false;
            deleteCandidateBtn.Click += new System.EventHandler(this.DeleteCandidateBtn_Click);
            // 
            // browseImgBtn
            // 
            this.browseImgBtn.Location = new System.Drawing.Point(83, 136);
            this.browseImgBtn.Name = "browseImgBtn";
            this.browseImgBtn.Size = new System.Drawing.Size(99, 23);
            this.browseImgBtn.TabIndex = 23;
            this.browseImgBtn.Text = "Browse";
            this.browseImgBtn.UseVisualStyleBackColor = true;
            this.browseImgBtn.Click += new System.EventHandler(this.BrowseImgBtn_Click);
            // 
            // imgLabel
            // 
            this.imgLabel.AutoSize = true;
            this.imgLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.imgLabel.Location = new System.Drawing.Point(3, 142);
            this.imgLabel.Name = "imgLabel";
            this.imgLabel.Size = new System.Drawing.Size(54, 13);
            this.imgLabel.TabIndex = 24;
            this.imgLabel.Text = "Illustration";
            // 
            // contentTxt
            // 
            this.contentTxt.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.contentTxt.Location = new System.Drawing.Point(83, 51);
            this.contentTxt.Name = "contentTxt";
            this.contentTxt.Size = new System.Drawing.Size(853, 79);
            this.contentTxt.TabIndex = 22;
            this.contentTxt.Text = "";
            // 
            // contentLabel
            // 
            this.contentLabel.AutoSize = true;
            this.contentLabel.Location = new System.Drawing.Point(3, 54);
            this.contentLabel.Name = "contentLabel";
            this.contentLabel.Size = new System.Drawing.Size(67, 26);
            this.contentLabel.TabIndex = 21;
            this.contentLabel.Text = " Question\r\nRequirement";
            // 
            // questionTypeComboBox
            // 
            this.questionTypeComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.questionTypeComboBox.Items.AddRange(new object[] {
            "1",
            "2",
            "3"});
            this.questionTypeComboBox.Location = new System.Drawing.Point(83, 20);
            this.questionTypeComboBox.Name = "questionTypeComboBox";
            this.questionTypeComboBox.Size = new System.Drawing.Size(118, 21);
            this.questionTypeComboBox.TabIndex = 20;
            this.questionTypeComboBox.SelectedValueChanged += new System.EventHandler(this.QuestionTypeComboBox_SelectedValueChanged);
            // 
            // typeLabel
            // 
            this.typeLabel.AutoSize = true;
            this.typeLabel.Location = new System.Drawing.Point(3, 23);
            this.typeLabel.Name = "typeLabel";
            this.typeLabel.Size = new System.Drawing.Size(31, 13);
            this.typeLabel.TabIndex = 19;
            this.typeLabel.Text = "Type";
            // 
            // imgPreview
            // 
            this.imgPreview.AutoSize = true;
            this.imgPreview.Location = new System.Drawing.Point(199, 142);
            this.imgPreview.Name = "imgPreview";
            this.imgPreview.Size = new System.Drawing.Size(0, 13);
            this.imgPreview.TabIndex = 29;
            this.imgPreview.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.ImgPreview_LinkClicked);
            // 
            // browseImgDialog
            // 
            this.browseImgDialog.FileName = "img";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 173);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(45, 13);
            this.label1.TabIndex = 33;
            this.label1.Text = "Solution";
            // 
            // solutionTxt
            // 
            this.solutionTxt.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.solutionTxt.Location = new System.Drawing.Point(83, 169);
            this.solutionTxt.Name = "solutionTxt";
            this.solutionTxt.Size = new System.Drawing.Size(853, 113);
            this.solutionTxt.TabIndex = 34;
            this.solutionTxt.Text = "";
            // 
            // testQueryTxt
            // 
            this.testQueryTxt.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.testQueryTxt.Enabled = false;
            this.testQueryTxt.Location = new System.Drawing.Point(83, 307);
            this.testQueryTxt.Name = "testQueryTxt";
            this.testQueryTxt.Size = new System.Drawing.Size(719, 136);
            this.testQueryTxt.TabIndex = 35;
            this.testQueryTxt.Text = "";
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 307);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(67, 13);
            this.label2.TabIndex = 36;
            this.label2.Text = "Test Queries";
            // 
            // requireSortCheckBox
            // 
            this.requireSortCheckBox.AutoSize = true;
            this.requireSortCheckBox.Location = new System.Drawing.Point(232, 18);
            this.requireSortCheckBox.Name = "requireSortCheckBox";
            this.requireSortCheckBox.Size = new System.Drawing.Size(85, 17);
            this.requireSortCheckBox.TabIndex = 41;
            this.requireSortCheckBox.Text = "Require Sort";
            this.requireSortCheckBox.UseVisualStyleBackColor = true;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            // 
            // checkColumnNameCheckbox
            // 
            this.checkColumnNameCheckbox.AutoSize = true;
            this.checkColumnNameCheckbox.Location = new System.Drawing.Point(338, 18);
            this.checkColumnNameCheckbox.Name = "checkColumnNameCheckbox";
            this.checkColumnNameCheckbox.Size = new System.Drawing.Size(126, 17);
            this.checkColumnNameCheckbox.TabIndex = 45;
            this.checkColumnNameCheckbox.Text = "Check Column Name";
            this.checkColumnNameCheckbox.UseVisualStyleBackColor = true;
            // 
            // separator1
            // 
            this.separator1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.separator1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.separator1.Location = new System.Drawing.Point(-1, 294);
            this.separator1.MaximumSize = new System.Drawing.Size(2000, 2);
            this.separator1.MinimumSize = new System.Drawing.Size(1000, 0);
            this.separator1.Name = "separator1";
            this.separator1.Size = new System.Drawing.Size(2000, 2);
            this.separator1.TabIndex = 37;
            this.separator1.Text = "label3";
            // 
            // checkDistinctCheckbox
            // 
            this.checkDistinctCheckbox.AutoSize = true;
            this.checkDistinctCheckbox.Location = new System.Drawing.Point(485, 18);
            this.checkDistinctCheckbox.Name = "checkDistinctCheckbox";
            this.checkDistinctCheckbox.Size = new System.Drawing.Size(95, 17);
            this.checkDistinctCheckbox.TabIndex = 45;
            this.checkDistinctCheckbox.Text = "Check Distinct";
            this.checkDistinctCheckbox.UseVisualStyleBackColor = true;
            // 
            // insertTcBtn
            // 
            this.insertTcBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.insertTcBtn.Location = new System.Drawing.Point(808, 307);
            this.insertTcBtn.Name = "insertTcBtn";
            this.insertTcBtn.Size = new System.Drawing.Size(115, 23);
            this.insertTcBtn.TabIndex = 46;
            this.insertTcBtn.Text = "Insert Testcase";
            this.insertTcBtn.UseVisualStyleBackColor = true;
            this.insertTcBtn.Click += new System.EventHandler(this.InsertTcBtn_Click);
            // 
            // validateTcBtn
            // 
            this.validateTcBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.validateTcBtn.Location = new System.Drawing.Point(808, 336);
            this.validateTcBtn.Name = "validateTcBtn";
            this.validateTcBtn.Size = new System.Drawing.Size(115, 23);
            this.validateTcBtn.TabIndex = 46;
            this.validateTcBtn.Text = "Validate TestQueries";
            this.validateTcBtn.UseVisualStyleBackColor = true;
            this.validateTcBtn.Click += new System.EventHandler(this.ValidateTcBtn_Click);
            // 
            // CandidatePanel
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.Color.White;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.validateTcBtn);
            this.Controls.Add(this.insertTcBtn);
            this.Controls.Add(this.browseImgBtn);
            this.Controls.Add(this.checkDistinctCheckbox);
            this.Controls.Add(this.checkColumnNameCheckbox);
            this.Controls.Add(this.requireSortCheckBox);
            this.Controls.Add(this.separator1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.testQueryTxt);
            this.Controls.Add(this.solutionTxt);
            this.Controls.Add(this.label1);
            this.Controls.Add(deleteCandidateBtn);
            this.Controls.Add(this.imgPreview);
            this.Controls.Add(this.imgLabel);
            this.Controls.Add(this.contentTxt);
            this.Controls.Add(this.contentLabel);
            this.Controls.Add(this.questionTypeComboBox);
            this.Controls.Add(this.typeLabel);
            this.DoubleBuffered = true;
            this.MinimumSize = new System.Drawing.Size(941, 451);
            this.Name = "CandidatePanel";
            this.Size = new System.Drawing.Size(939, 451);
            this.Load += new System.EventHandler(this.CandidatePanel_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button browseImgBtn;
        private System.Windows.Forms.Label imgLabel;
        private System.Windows.Forms.RichTextBox contentTxt;
        private System.Windows.Forms.Label contentLabel;
        private System.Windows.Forms.ComboBox questionTypeComboBox;
        private System.Windows.Forms.Label typeLabel;
        private System.Windows.Forms.LinkLabel imgPreview;
        private System.Windows.Forms.OpenFileDialog browseImgDialog;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RichTextBox solutionTxt;
        private System.Windows.Forms.RichTextBox testQueryTxt;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox requireSortCheckBox;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.CheckBox checkColumnNameCheckbox;
        private System.Windows.Forms.Label separator1;
        private System.Windows.Forms.CheckBox checkDistinctCheckbox;
        private System.Windows.Forms.Button insertTcBtn;
        private System.Windows.Forms.Button validateTcBtn;
    }
}
