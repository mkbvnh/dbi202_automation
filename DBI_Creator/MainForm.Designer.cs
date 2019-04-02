namespace DBI_Exam_Creator_Tool
{
    partial class MainForm
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
            this.questionPanel = new System.Windows.Forms.Panel();
            this.addQuestionBtn = new System.Windows.Forms.Button();
            this.importBtn = new System.Windows.Forms.Button();
            this.exportBtn = new System.Windows.Forms.Button();
            this.editQuestionPanel = new System.Windows.Forms.Panel();
            this.questionIdTxt = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.deleteQuestionBtn = new System.Windows.Forms.Button();
            this.addCandidateBtn = new System.Windows.Forms.Button();
            this.candidateControl = new System.Windows.Forms.TabControl();
            this.pointLabel = new System.Windows.Forms.Label();
            this.pointTxt = new System.Windows.Forms.TextBox();
            this.candidatePanel = new System.Windows.Forms.Panel();
            this.addRequirementBtn = new System.Windows.Forms.Button();
            this.dataGridView = new System.Windows.Forms.DataGridView();
            this.RequirementId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CandidateId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Type = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ResultQuery = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.EffectTable = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CheckEffectQuery = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.requirementsLabel = new System.Windows.Forms.Label();
            this.pictureBox = new System.Windows.Forms.PictureBox();
            this.browseImgBtn = new System.Windows.Forms.Button();
            this.imgLabel = new System.Windows.Forms.Label();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.contentLabel = new System.Windows.Forms.Label();
            this.questionTypeComboBox = new System.Windows.Forms.ComboBox();
            this.typeLabel = new System.Windows.Forms.Label();
            this.browseImgDialog = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.sqlScriptBtn = new System.Windows.Forms.Button();
            this.separator = new System.Windows.Forms.Label();
            this.editQuestionPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // questionPanel
            // 
            this.questionPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.questionPanel.AutoScroll = true;
            this.questionPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.questionPanel.Location = new System.Drawing.Point(12, 81);
            this.questionPanel.Name = "questionPanel";
            this.questionPanel.Size = new System.Drawing.Size(112, 467);
            this.questionPanel.TabIndex = 0;
            // 
            // addQuestionBtn
            // 
            this.addQuestionBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.addQuestionBtn.Location = new System.Drawing.Point(12, 52);
            this.addQuestionBtn.Name = "addQuestionBtn";
            this.addQuestionBtn.Size = new System.Drawing.Size(112, 23);
            this.addQuestionBtn.TabIndex = 1;
            this.addQuestionBtn.Text = "Add Question";
            this.addQuestionBtn.UseVisualStyleBackColor = true;
            this.addQuestionBtn.Click += new System.EventHandler(this.addQuestionBtn_Click);
            // 
            // importBtn
            // 
            this.importBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.importBtn.Location = new System.Drawing.Point(12, 13);
            this.importBtn.Name = "importBtn";
            this.importBtn.Size = new System.Drawing.Size(79, 23);
            this.importBtn.TabIndex = 1;
            this.importBtn.Text = "Import";
            this.importBtn.UseVisualStyleBackColor = true;
            // 
            // exportBtn
            // 
            this.exportBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.exportBtn.Location = new System.Drawing.Point(97, 13);
            this.exportBtn.Name = "exportBtn";
            this.exportBtn.Size = new System.Drawing.Size(79, 23);
            this.exportBtn.TabIndex = 2;
            this.exportBtn.Text = "Export";
            this.exportBtn.UseVisualStyleBackColor = true;
            this.exportBtn.Click += new System.EventHandler(this.exportBtn_Click);
            // 
            // editQuestionPanel
            // 
            this.editQuestionPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.editQuestionPanel.Controls.Add(this.questionIdTxt);
            this.editQuestionPanel.Controls.Add(this.label1);
            this.editQuestionPanel.Controls.Add(this.deleteQuestionBtn);
            this.editQuestionPanel.Controls.Add(this.addCandidateBtn);
            this.editQuestionPanel.Controls.Add(this.candidateControl);
            this.editQuestionPanel.Controls.Add(this.pointLabel);
            this.editQuestionPanel.Controls.Add(this.pointTxt);
            this.editQuestionPanel.Location = new System.Drawing.Point(130, 52);
            this.editQuestionPanel.Name = "editQuestionPanel";
            this.editQuestionPanel.Size = new System.Drawing.Size(875, 496);
            this.editQuestionPanel.TabIndex = 3;
            // 
            // questionIdTxt
            // 
            this.questionIdTxt.Location = new System.Drawing.Point(313, 6);
            this.questionIdTxt.Name = "questionIdTxt";
            this.questionIdTxt.ReadOnly = true;
            this.questionIdTxt.Size = new System.Drawing.Size(99, 20);
            this.questionIdTxt.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(225, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(82, 16);
            this.label1.TabIndex = 3;
            this.label1.Text = "QuestionId";
            // 
            // deleteQuestionBtn
            // 
            this.deleteQuestionBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.deleteQuestionBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.deleteQuestionBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.deleteQuestionBtn.Location = new System.Drawing.Point(727, 4);
            this.deleteQuestionBtn.Name = "deleteQuestionBtn";
            this.deleteQuestionBtn.Size = new System.Drawing.Size(144, 23);
            this.deleteQuestionBtn.TabIndex = 2;
            this.deleteQuestionBtn.Text = "Delete Question";
            this.deleteQuestionBtn.UseVisualStyleBackColor = false;
            this.deleteQuestionBtn.Click += new System.EventHandler(this.deleteQuestionBtn_Click);
            // 
            // addCandidateBtn
            // 
            this.addCandidateBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.addCandidateBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.addCandidateBtn.Location = new System.Drawing.Point(596, 4);
            this.addCandidateBtn.Name = "addCandidateBtn";
            this.addCandidateBtn.Size = new System.Drawing.Size(125, 23);
            this.addCandidateBtn.TabIndex = 0;
            this.addCandidateBtn.Text = "Add Candidate";
            this.addCandidateBtn.UseVisualStyleBackColor = true;
            this.addCandidateBtn.Click += new System.EventHandler(this.addCandidateBtn_Click);
            // 
            // candidateControl
            // 
            this.candidateControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.candidateControl.Location = new System.Drawing.Point(0, 33);
            this.candidateControl.Name = "candidateControl";
            this.candidateControl.SelectedIndex = 0;
            this.candidateControl.Size = new System.Drawing.Size(875, 463);
            this.candidateControl.TabIndex = 0;
            // 
            // pointLabel
            // 
            this.pointLabel.AutoSize = true;
            this.pointLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pointLabel.Location = new System.Drawing.Point(12, 9);
            this.pointLabel.Name = "pointLabel";
            this.pointLabel.Size = new System.Drawing.Size(43, 16);
            this.pointLabel.TabIndex = 1;
            this.pointLabel.Text = "Point";
            // 
            // pointTxt
            // 
            this.pointTxt.Location = new System.Drawing.Point(61, 7);
            this.pointTxt.Name = "pointTxt";
            this.pointTxt.Size = new System.Drawing.Size(99, 20);
            this.pointTxt.TabIndex = 0;
            // 
            // candidatePanel
            // 
            this.candidatePanel.Location = new System.Drawing.Point(0, 0);
            this.candidatePanel.Name = "candidatePanel";
            this.candidatePanel.Size = new System.Drawing.Size(200, 100);
            this.candidatePanel.TabIndex = 0;
            // 
            // addRequirementBtn
            // 
            this.addRequirementBtn.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.addRequirementBtn.Location = new System.Drawing.Point(86, 193);
            this.addRequirementBtn.Name = "addRequirementBtn";
            this.addRequirementBtn.Size = new System.Drawing.Size(99, 23);
            this.addRequirementBtn.TabIndex = 18;
            this.addRequirementBtn.Text = "Add Requirement";
            this.addRequirementBtn.UseVisualStyleBackColor = true;
            // 
            // dataGridView
            // 
            this.dataGridView.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.RequirementId,
            this.CandidateId,
            this.Type,
            this.ResultQuery,
            this.EffectTable,
            this.CheckEffectQuery});
            this.dataGridView.Location = new System.Drawing.Point(86, 222);
            this.dataGridView.Name = "dataGridView";
            this.dataGridView.Size = new System.Drawing.Size(644, 171);
            this.dataGridView.TabIndex = 17;
            // 
            // RequirementId
            // 
            this.RequirementId.DataPropertyName = "requirementId";
            this.RequirementId.HeaderText = "RequirementId";
            this.RequirementId.Name = "RequirementId";
            this.RequirementId.ReadOnly = true;
            // 
            // CandidateId
            // 
            this.CandidateId.DataPropertyName = "CandidateId";
            this.CandidateId.HeaderText = "CandidateId";
            this.CandidateId.Name = "CandidateId";
            // 
            // Type
            // 
            this.Type.DataPropertyName = "Type";
            this.Type.HeaderText = "Type";
            this.Type.Name = "Type";
            // 
            // ResultQuery
            // 
            this.ResultQuery.DataPropertyName = "ResultQuery";
            this.ResultQuery.HeaderText = "ResultQuery";
            this.ResultQuery.Name = "ResultQuery";
            // 
            // EffectTable
            // 
            this.EffectTable.DataPropertyName = "EffectTable";
            this.EffectTable.HeaderText = "EffectTable";
            this.EffectTable.Name = "EffectTable";
            // 
            // CheckEffectQuery
            // 
            this.CheckEffectQuery.DataPropertyName = "CheckEffectQuery";
            this.CheckEffectQuery.HeaderText = "CheckEffectQuery";
            this.CheckEffectQuery.Name = "CheckEffectQuery";
            // 
            // requirementsLabel
            // 
            this.requirementsLabel.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.requirementsLabel.AutoSize = true;
            this.requirementsLabel.Location = new System.Drawing.Point(6, 198);
            this.requirementsLabel.Name = "requirementsLabel";
            this.requirementsLabel.Size = new System.Drawing.Size(72, 13);
            this.requirementsLabel.TabIndex = 16;
            this.requirementsLabel.Text = "Requirements";
            // 
            // pictureBox
            // 
            this.pictureBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox.Location = new System.Drawing.Point(86, 124);
            this.pictureBox.Name = "pictureBox";
            this.pictureBox.Size = new System.Drawing.Size(646, 63);
            this.pictureBox.TabIndex = 15;
            this.pictureBox.TabStop = false;
            // 
            // browseImgBtn
            // 
            this.browseImgBtn.Location = new System.Drawing.Point(86, 95);
            this.browseImgBtn.Name = "browseImgBtn";
            this.browseImgBtn.Size = new System.Drawing.Size(99, 23);
            this.browseImgBtn.TabIndex = 13;
            this.browseImgBtn.Text = "Browse";
            this.browseImgBtn.UseVisualStyleBackColor = true;
            // 
            // imgLabel
            // 
            this.imgLabel.AutoSize = true;
            this.imgLabel.Location = new System.Drawing.Point(6, 100);
            this.imgLabel.Name = "imgLabel";
            this.imgLabel.Size = new System.Drawing.Size(36, 13);
            this.imgLabel.TabIndex = 14;
            this.imgLabel.Text = "Image";
            // 
            // textBox2
            // 
            this.textBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox2.Location = new System.Drawing.Point(86, 39);
            this.textBox2.Multiline = true;
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(646, 46);
            this.textBox2.TabIndex = 12;
            // 
            // contentLabel
            // 
            this.contentLabel.AutoSize = true;
            this.contentLabel.Location = new System.Drawing.Point(6, 42);
            this.contentLabel.Name = "contentLabel";
            this.contentLabel.Size = new System.Drawing.Size(44, 13);
            this.contentLabel.TabIndex = 11;
            this.contentLabel.Text = "Content";
            // 
            // questionTypeComboBox
            // 
            this.questionTypeComboBox.FormattingEnabled = true;
            this.questionTypeComboBox.Location = new System.Drawing.Point(86, 8);
            this.questionTypeComboBox.Name = "questionTypeComboBox";
            this.questionTypeComboBox.Size = new System.Drawing.Size(99, 21);
            this.questionTypeComboBox.TabIndex = 10;
            // 
            // typeLabel
            // 
            this.typeLabel.AutoSize = true;
            this.typeLabel.Location = new System.Drawing.Point(6, 11);
            this.typeLabel.Name = "typeLabel";
            this.typeLabel.Size = new System.Drawing.Size(31, 13);
            this.typeLabel.TabIndex = 9;
            this.typeLabel.Text = "Type";
            // 
            // browseImgDialog
            // 
            this.browseImgDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;";
            // 
            // sqlScriptBtn
            // 
            this.sqlScriptBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.sqlScriptBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.sqlScriptBtn.Location = new System.Drawing.Point(880, 13);
            this.sqlScriptBtn.Name = "sqlScriptBtn";
            this.sqlScriptBtn.Size = new System.Drawing.Size(125, 23);
            this.sqlScriptBtn.TabIndex = 4;
            this.sqlScriptBtn.Text = "SQL Script";
            this.sqlScriptBtn.UseVisualStyleBackColor = true;
            // 
            // separator
            // 
            this.separator.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.separator.Location = new System.Drawing.Point(-2, 42);
            this.separator.MaximumSize = new System.Drawing.Size(2000, 3);
            this.separator.Name = "separator";
            this.separator.Size = new System.Drawing.Size(1023, 3);
            this.separator.TabIndex = 5;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1017, 560);
            this.Controls.Add(this.separator);
            this.Controls.Add(this.sqlScriptBtn);
            this.Controls.Add(this.editQuestionPanel);
            this.Controls.Add(this.addQuestionBtn);
            this.Controls.Add(this.exportBtn);
            this.Controls.Add(this.importBtn);
            this.Controls.Add(this.questionPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.Text = "DBI Exam Creator";
            this.editQuestionPanel.ResumeLayout(false);
            this.editQuestionPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel questionPanel;
        private System.Windows.Forms.Button addQuestionBtn;
        private System.Windows.Forms.Button importBtn;
        private System.Windows.Forms.Button exportBtn;
        private System.Windows.Forms.Panel editQuestionPanel;
        private System.Windows.Forms.Label pointLabel;
        private System.Windows.Forms.TextBox pointTxt;
        private System.Windows.Forms.OpenFileDialog browseImgDialog;
        private System.Windows.Forms.Button addCandidateBtn;
        private System.Windows.Forms.Panel candidatePanel;
        private System.Windows.Forms.Button addRequirementBtn;
        private System.Windows.Forms.DataGridView dataGridView;
        private System.Windows.Forms.DataGridViewTextBoxColumn RequirementId;
        private System.Windows.Forms.DataGridViewTextBoxColumn CandidateId;
        private System.Windows.Forms.DataGridViewTextBoxColumn Type;
        private System.Windows.Forms.DataGridViewTextBoxColumn ResultQuery;
        private System.Windows.Forms.DataGridViewTextBoxColumn EffectTable;
        private System.Windows.Forms.DataGridViewTextBoxColumn CheckEffectQuery;
        private System.Windows.Forms.Label requirementsLabel;
        private System.Windows.Forms.PictureBox pictureBox;
        private System.Windows.Forms.Button browseImgBtn;
        private System.Windows.Forms.Label imgLabel;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Label contentLabel;
        private System.Windows.Forms.ComboBox questionTypeComboBox;
        private System.Windows.Forms.Label typeLabel;
        private System.Windows.Forms.TabControl candidateControl;
        private System.Windows.Forms.SaveFileDialog saveFileDialog;
        private System.Windows.Forms.Button sqlScriptBtn;
        private System.Windows.Forms.Button deleteQuestionBtn;
        private System.Windows.Forms.TextBox questionIdTxt;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label separator;
    }
}

