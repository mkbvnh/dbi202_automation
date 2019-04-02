namespace DBI202_Creator.UI
{
    partial class CreatorForm
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
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.importPaperSetToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportPaperSetToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.questionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addQuestionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.removeQuestionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.candidateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addCandidateToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.removeCandidateToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.previewBtn = new System.Windows.Forms.Button();
            this.questionTabControl = new System.Windows.Forms.TabControl();
            this.saveQuestionSetDialog = new System.Windows.Forms.SaveFileDialog();
            this.removeQuestionBtn = new System.Windows.Forms.Button();
            this.addQuestionBtn = new System.Windows.Forms.Button();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.scriptBtn = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.menuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip
            // 
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.questionToolStripMenuItem,
            this.candidateToolStripMenuItem});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(1079, 24);
            this.menuStrip.TabIndex = 1;
            this.menuStrip.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripMenuItem,
            this.saveToolStripMenuItem,
            this.toolStripSeparator1,
            this.importPaperSetToolStripMenuItem,
            this.exportPaperSetToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.openToolStripMenuItem.Text = "Open Question Set";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.saveToolStripMenuItem.Text = "Save Question Set";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(177, 6);
            // 
            // importPaperSetToolStripMenuItem
            // 
            this.importPaperSetToolStripMenuItem.Name = "importPaperSetToolStripMenuItem";
            this.importPaperSetToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.importPaperSetToolStripMenuItem.Text = "Import Paper Set";
            this.importPaperSetToolStripMenuItem.Click += new System.EventHandler(this.importPaperSetToolStripMenuItem_Click);
            // 
            // exportPaperSetToolStripMenuItem
            // 
            this.exportPaperSetToolStripMenuItem.Name = "exportPaperSetToolStripMenuItem";
            this.exportPaperSetToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.exportPaperSetToolStripMenuItem.Text = "Export Papers Folder";
            this.exportPaperSetToolStripMenuItem.Click += new System.EventHandler(this.exportPaperSetToolStripMenuItem_Click);
            // 
            // questionToolStripMenuItem
            // 
            this.questionToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addQuestionToolStripMenuItem,
            this.removeQuestionToolStripMenuItem});
            this.questionToolStripMenuItem.Name = "questionToolStripMenuItem";
            this.questionToolStripMenuItem.Size = new System.Drawing.Size(67, 20);
            this.questionToolStripMenuItem.Text = "Question";
            // 
            // addQuestionToolStripMenuItem
            // 
            this.addQuestionToolStripMenuItem.Name = "addQuestionToolStripMenuItem";
            this.addQuestionToolStripMenuItem.Size = new System.Drawing.Size(117, 22);
            this.addQuestionToolStripMenuItem.Text = "Add";
            this.addQuestionToolStripMenuItem.Click += new System.EventHandler(this.addQuestionToolStripMenuItem_Click);
            // 
            // removeQuestionToolStripMenuItem
            // 
            this.removeQuestionToolStripMenuItem.Enabled = false;
            this.removeQuestionToolStripMenuItem.Name = "removeQuestionToolStripMenuItem";
            this.removeQuestionToolStripMenuItem.Size = new System.Drawing.Size(117, 22);
            this.removeQuestionToolStripMenuItem.Text = "Remove";
            // 
            // candidateToolStripMenuItem
            // 
            this.candidateToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addCandidateToolStripMenuItem1,
            this.removeCandidateToolStripMenuItem1});
            this.candidateToolStripMenuItem.Enabled = false;
            this.candidateToolStripMenuItem.Name = "candidateToolStripMenuItem";
            this.candidateToolStripMenuItem.Size = new System.Drawing.Size(73, 20);
            this.candidateToolStripMenuItem.Text = "Candidate";
            // 
            // addCandidateToolStripMenuItem1
            // 
            this.addCandidateToolStripMenuItem1.Name = "addCandidateToolStripMenuItem1";
            this.addCandidateToolStripMenuItem1.Size = new System.Drawing.Size(117, 22);
            this.addCandidateToolStripMenuItem1.Text = "Add";
            // 
            // removeCandidateToolStripMenuItem1
            // 
            this.removeCandidateToolStripMenuItem1.Name = "removeCandidateToolStripMenuItem1";
            this.removeCandidateToolStripMenuItem1.Size = new System.Drawing.Size(117, 22);
            this.removeCandidateToolStripMenuItem1.Text = "Remove";
            // 
            // previewBtn
            // 
            this.previewBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.previewBtn.Location = new System.Drawing.Point(996, 29);
            this.previewBtn.Name = "previewBtn";
            this.previewBtn.Size = new System.Drawing.Size(75, 23);
            this.previewBtn.TabIndex = 2;
            this.previewBtn.Text = "Preview";
            this.previewBtn.UseVisualStyleBackColor = true;
            this.previewBtn.Click += new System.EventHandler(this.previewBtn_Click);
            // 
            // questionTabControl
            // 
            this.questionTabControl.Alignment = System.Windows.Forms.TabAlignment.Left;
            this.questionTabControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.questionTabControl.DrawMode = System.Windows.Forms.TabDrawMode.OwnerDrawFixed;
            this.questionTabControl.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.questionTabControl.ItemSize = new System.Drawing.Size(25, 100);
            this.questionTabControl.Location = new System.Drawing.Point(12, 58);
            this.questionTabControl.Multiline = true;
            this.questionTabControl.Name = "questionTabControl";
            this.questionTabControl.SelectedIndex = 0;
            this.questionTabControl.Size = new System.Drawing.Size(1059, 562);
            this.questionTabControl.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.questionTabControl.TabIndex = 0;
            this.questionTabControl.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.questionTabControl_DrawItem);
            this.questionTabControl.Selected += new System.Windows.Forms.TabControlEventHandler(this.questionTabControl_Selected);
            // 
            // removeQuestionBtn
            // 
            this.removeQuestionBtn.Image = global::DBI202_Creator.Properties.Resources.minus1;
            this.removeQuestionBtn.Location = new System.Drawing.Point(59, 25);
            this.removeQuestionBtn.Name = "removeQuestionBtn";
            this.removeQuestionBtn.Size = new System.Drawing.Size(31, 31);
            this.removeQuestionBtn.TabIndex = 5;
            this.removeQuestionBtn.UseVisualStyleBackColor = true;
            this.removeQuestionBtn.Click += new System.EventHandler(this.removeQuestionBtn_Click);
            // 
            // addQuestionBtn
            // 
            this.addQuestionBtn.Image = global::DBI202_Creator.Properties.Resources.add;
            this.addQuestionBtn.Location = new System.Drawing.Point(15, 25);
            this.addQuestionBtn.Name = "addQuestionBtn";
            this.addQuestionBtn.Size = new System.Drawing.Size(31, 31);
            this.addQuestionBtn.TabIndex = 4;
            this.addQuestionBtn.UseVisualStyleBackColor = true;
            this.addQuestionBtn.Click += new System.EventHandler(this.addQuestionBtn_Click);
            // 
            // importDialog
            // 
            this.openFileDialog.FileName = "openFileDialog";
            // 
            // scriptBtn
            // 
            this.scriptBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.scriptBtn.Location = new System.Drawing.Point(897, 29);
            this.scriptBtn.Name = "scriptBtn";
            this.scriptBtn.Size = new System.Drawing.Size(93, 23);
            this.scriptBtn.TabIndex = 6;
            this.scriptBtn.Text = "Database Script";
            this.scriptBtn.UseVisualStyleBackColor = true;
            this.scriptBtn.Click += new System.EventHandler(this.scriptBtn_Click);
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.Enabled = false;
            this.button1.Location = new System.Drawing.Point(798, 29);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(93, 23);
            this.button1.TabIndex = 6;
            this.button1.Text = "Verify Solution";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.scriptBtn_Click);
            // 
            // CreatorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(1079, 632);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.scriptBtn);
            this.Controls.Add(this.removeQuestionBtn);
            this.Controls.Add(this.addQuestionBtn);
            this.Controls.Add(this.previewBtn);
            this.Controls.Add(this.questionTabControl);
            this.Controls.Add(this.menuStrip);
            this.MainMenuStrip = this.menuStrip;
            this.MinimumSize = new System.Drawing.Size(1095, 671);
            this.Name = "CreatorForm";
            this.Text = "CreatorForm";
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem questionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addQuestionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem removeQuestionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem candidateToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addCandidateToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem removeCandidateToolStripMenuItem1;
        private System.Windows.Forms.Button previewBtn;
        private System.Windows.Forms.TabControl questionTabControl;
        private System.Windows.Forms.SaveFileDialog saveQuestionSetDialog;
        private System.Windows.Forms.Button addQuestionBtn;
        private System.Windows.Forms.Button removeQuestionBtn;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.Button scriptBtn;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ToolStripMenuItem exportPaperSetToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem importPaperSetToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
    }
}