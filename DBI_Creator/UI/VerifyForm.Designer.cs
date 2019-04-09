namespace DBI202_Creator.UI
{
    partial class VerifyForm
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
            this.verifyText = new System.Windows.Forms.RichTextBox();
            this.checkConnectionButton = new System.Windows.Forms.Button();
            this.passwordTextBox = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.usernameTextBox = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.serverNameTextBox = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.startBtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // verifyText
            // 
            this.verifyText.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.verifyText.BackColor = System.Drawing.Color.White;
            this.verifyText.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.verifyText.Location = new System.Drawing.Point(12, 100);
            this.verifyText.Name = "verifyText";
            this.verifyText.ReadOnly = true;
            this.verifyText.Size = new System.Drawing.Size(514, 338);
            this.verifyText.TabIndex = 0;
            this.verifyText.Text = "";
            this.verifyText.TextChanged += new System.EventHandler(this.VerifyText_TextChanged);
            // 
            // checkConnectionButton
            // 
            this.checkConnectionButton.Location = new System.Drawing.Point(409, 14);
            this.checkConnectionButton.Name = "checkConnectionButton";
            this.checkConnectionButton.Size = new System.Drawing.Size(117, 39);
            this.checkConnectionButton.TabIndex = 49;
            this.checkConnectionButton.Text = "Check Connection";
            this.checkConnectionButton.UseVisualStyleBackColor = true;
            this.checkConnectionButton.Click += new System.EventHandler(this.CheckConnectionButton_Click);
            // 
            // passwordTextBox
            // 
            this.passwordTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.passwordTextBox.Location = new System.Drawing.Point(135, 74);
            this.passwordTextBox.Name = "passwordTextBox";
            this.passwordTextBox.Size = new System.Drawing.Size(255, 20);
            this.passwordTextBox.TabIndex = 44;
            this.passwordTextBox.Text = "123456";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(12, 75);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(104, 15);
            this.label9.TabIndex = 48;
            this.label9.Text = "Password(Server)";
            // 
            // usernameTextBox
            // 
            this.usernameTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.usernameTextBox.Location = new System.Drawing.Point(135, 43);
            this.usernameTextBox.Name = "usernameTextBox";
            this.usernameTextBox.Size = new System.Drawing.Size(255, 20);
            this.usernameTextBox.TabIndex = 43;
            this.usernameTextBox.Text = "sa";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(12, 44);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(108, 15);
            this.label8.TabIndex = 47;
            this.label8.Text = "Username(Server)";
            // 
            // serverNameTextBox
            // 
            this.serverNameTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.serverNameTextBox.Location = new System.Drawing.Point(135, 13);
            this.serverNameTextBox.Name = "serverNameTextBox";
            this.serverNameTextBox.Size = new System.Drawing.Size(255, 20);
            this.serverNameTextBox.TabIndex = 42;
            this.serverNameTextBox.Text = "localhost";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(12, 13);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(79, 15);
            this.label6.TabIndex = 46;
            this.label6.Text = "Server Name";
            // 
            // startBtn
            // 
            this.startBtn.AccessibleName = "";
            this.startBtn.Enabled = false;
            this.startBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.startBtn.Location = new System.Drawing.Point(410, 62);
            this.startBtn.Name = "startBtn";
            this.startBtn.Size = new System.Drawing.Size(117, 31);
            this.startBtn.TabIndex = 45;
            this.startBtn.Text = "START";
            this.startBtn.UseVisualStyleBackColor = true;
            this.startBtn.Click += new System.EventHandler(this.StartBtn_Click);
            // 
            // VerifyForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(539, 450);
            this.Controls.Add(this.checkConnectionButton);
            this.Controls.Add(this.passwordTextBox);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.usernameTextBox);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.serverNameTextBox);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.startBtn);
            this.Controls.Add(this.verifyText);
            this.MinimumSize = new System.Drawing.Size(555, 489);
            this.Name = "VerifyForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "VerifyForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button checkConnectionButton;
        private System.Windows.Forms.TextBox passwordTextBox;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox usernameTextBox;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox serverNameTextBox;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button startBtn;
        internal System.Windows.Forms.RichTextBox verifyText;
    }
}