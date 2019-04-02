namespace DBI202_Creator.UI
{
    partial class QuestionPanel
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
            this.pointLabel = new System.Windows.Forms.Label();
            this.candidateTabControl = new System.Windows.Forms.TabControl();
            this.addCandidateBtn = new System.Windows.Forms.Button();
            this.removeQuestionBtn = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.questionIdTxt = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.editQuestionPanel = new System.Windows.Forms.Panel();
            this.pointNumeric = new System.Windows.Forms.NumericUpDown();
            this.editQuestionPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pointNumeric)).BeginInit();
            this.SuspendLayout();
            // 
            // pointLabel
            // 
            this.pointLabel.AutoSize = true;
            this.pointLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pointLabel.Location = new System.Drawing.Point(365, 9);
            this.pointLabel.Name = "pointLabel";
            this.pointLabel.Size = new System.Drawing.Size(43, 16);
            this.pointLabel.TabIndex = 1;
            this.pointLabel.Text = "Point";
            // 
            // candidateTabControl
            // 
            this.candidateTabControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.candidateTabControl.Location = new System.Drawing.Point(0, 69);
            this.candidateTabControl.Name = "candidateTabControl";
            this.candidateTabControl.SelectedIndex = 0;
            this.candidateTabControl.Size = new System.Drawing.Size(943, 472);
            this.candidateTabControl.TabIndex = 0;
            // 
            // addCandidateBtn
            // 
            this.addCandidateBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.addCandidateBtn.Location = new System.Drawing.Point(102, 40);
            this.addCandidateBtn.Name = "addCandidateBtn";
            this.addCandidateBtn.Size = new System.Drawing.Size(99, 23);
            this.addCandidateBtn.TabIndex = 0;
            this.addCandidateBtn.Text = "Add Candidate";
            this.addCandidateBtn.UseVisualStyleBackColor = true;
            this.addCandidateBtn.Click += new System.EventHandler(this.addCandidateBtn_Click);
            // 
            // removeQuestionBtn
            // 
            this.removeQuestionBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.removeQuestionBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.removeQuestionBtn.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.removeQuestionBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.removeQuestionBtn.Image = global::DBI202_Creator.Properties.Resources.delete;
            this.removeQuestionBtn.Location = new System.Drawing.Point(905, 0);
            this.removeQuestionBtn.Name = "removeQuestionBtn";
            this.removeQuestionBtn.Size = new System.Drawing.Size(38, 36);
            this.removeQuestionBtn.TabIndex = 2;
            this.removeQuestionBtn.UseVisualStyleBackColor = false;
            this.removeQuestionBtn.Click += new System.EventHandler(this.removeQuestionBtn_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(10, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(88, 16);
            this.label1.TabIndex = 3;
            this.label1.Text = "Question ID";
            // 
            // questionIdTxt
            // 
            this.questionIdTxt.Enabled = false;
            this.questionIdTxt.Location = new System.Drawing.Point(102, 7);
            this.questionIdTxt.Name = "questionIdTxt";
            this.questionIdTxt.ReadOnly = true;
            this.questionIdTxt.Size = new System.Drawing.Size(236, 20);
            this.questionIdTxt.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(12, 43);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(87, 16);
            this.label2.TabIndex = 5;
            this.label2.Text = "Candidates";
            // 
            // editQuestionPanel
            // 
            this.editQuestionPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.editQuestionPanel.Controls.Add(this.pointNumeric);
            this.editQuestionPanel.Controls.Add(this.label2);
            this.editQuestionPanel.Controls.Add(this.questionIdTxt);
            this.editQuestionPanel.Controls.Add(this.label1);
            this.editQuestionPanel.Controls.Add(this.removeQuestionBtn);
            this.editQuestionPanel.Controls.Add(this.addCandidateBtn);
            this.editQuestionPanel.Controls.Add(this.candidateTabControl);
            this.editQuestionPanel.Controls.Add(this.pointLabel);
            this.editQuestionPanel.Location = new System.Drawing.Point(3, 3);
            this.editQuestionPanel.Name = "editQuestionPanel";
            this.editQuestionPanel.Size = new System.Drawing.Size(943, 544);
            this.editQuestionPanel.TabIndex = 4;
            // 
            // pointNumeric
            // 
            this.pointNumeric.DecimalPlaces = 1;
            this.pointNumeric.Increment = new decimal(new int[] {
            5,
            0,
            0,
            65536});
            this.pointNumeric.Location = new System.Drawing.Point(414, 8);
            this.pointNumeric.Name = "pointNumeric";
            this.pointNumeric.Size = new System.Drawing.Size(66, 20);
            this.pointNumeric.TabIndex = 6;
            // 
            // QuestionPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.editQuestionPanel);
            this.Name = "QuestionPanel";
            this.Size = new System.Drawing.Size(949, 550);
            this.editQuestionPanel.ResumeLayout(false);
            this.editQuestionPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pointNumeric)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Label pointLabel;
        private System.Windows.Forms.TabControl candidateTabControl;
        private System.Windows.Forms.Button addCandidateBtn;
        private System.Windows.Forms.Button removeQuestionBtn;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox questionIdTxt;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel editQuestionPanel;
        private System.Windows.Forms.NumericUpDown pointNumeric;
    }
}
