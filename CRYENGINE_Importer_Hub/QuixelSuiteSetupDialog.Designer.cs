namespace CRYENGINE_ImportHub
{
    partial class QuixelSuiteSetupDialog
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
            this.CloseButton = new System.Windows.Forms.Button();
            this.ddoProjectBrowse = new System.Windows.Forms.Button();
            this.ddoProject = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.browseTargetDirectory = new System.Windows.Forms.Button();
            this.targetDirectoryPath = new System.Windows.Forms.TextBox();
            this.startButton = new System.Windows.Forms.Button();
            this.crossAppCheckbox = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // CloseButton
            // 
            this.CloseButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(41)))), ((int)(((byte)(53)))));
            this.CloseButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.CloseButton.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.CloseButton.ForeColor = System.Drawing.Color.White;
            this.CloseButton.Location = new System.Drawing.Point(97, 148);
            this.CloseButton.Name = "CloseButton";
            this.CloseButton.Size = new System.Drawing.Size(81, 32);
            this.CloseButton.TabIndex = 15;
            this.CloseButton.Text = "Cancel";
            this.CloseButton.UseVisualStyleBackColor = false;
            this.CloseButton.Click += new System.EventHandler(this.CloseButton_Click);
            // 
            // ddoProjectBrowse
            // 
            this.ddoProjectBrowse.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(41)))), ((int)(((byte)(53)))));
            this.ddoProjectBrowse.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ddoProjectBrowse.Font = new System.Drawing.Font("Segoe UI", 7.5F);
            this.ddoProjectBrowse.ForeColor = System.Drawing.Color.White;
            this.ddoProjectBrowse.Location = new System.Drawing.Point(326, 34);
            this.ddoProjectBrowse.Name = "ddoProjectBrowse";
            this.ddoProjectBrowse.Size = new System.Drawing.Size(65, 20);
            this.ddoProjectBrowse.TabIndex = 16;
            this.ddoProjectBrowse.Text = "Browse";
            this.ddoProjectBrowse.UseVisualStyleBackColor = false;
            this.ddoProjectBrowse.Click += new System.EventHandler(this.browseFolderButton_Click);
            // 
            // ddoProject
            // 
            this.ddoProject.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(34)))), ((int)(((byte)(44)))));
            this.ddoProject.ForeColor = System.Drawing.Color.White;
            this.ddoProject.Location = new System.Drawing.Point(12, 34);
            this.ddoProject.Name = "ddoProject";
            this.ddoProject.Size = new System.Drawing.Size(308, 20);
            this.ddoProject.TabIndex = 17;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(12, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 13);
            this.label1.TabIndex = 20;
            this.label1.Text = "DDO Project";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(12, 64);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(81, 13);
            this.label2.TabIndex = 23;
            this.label2.Text = "Target directory";
            // 
            // browseTargetDirectory
            // 
            this.browseTargetDirectory.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(41)))), ((int)(((byte)(53)))));
            this.browseTargetDirectory.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.browseTargetDirectory.Font = new System.Drawing.Font("Segoe UI", 7.5F);
            this.browseTargetDirectory.ForeColor = System.Drawing.Color.White;
            this.browseTargetDirectory.Location = new System.Drawing.Point(326, 80);
            this.browseTargetDirectory.Name = "browseTargetDirectory";
            this.browseTargetDirectory.Size = new System.Drawing.Size(65, 20);
            this.browseTargetDirectory.TabIndex = 21;
            this.browseTargetDirectory.Text = "Browse";
            this.browseTargetDirectory.UseVisualStyleBackColor = false;
            this.browseTargetDirectory.Click += new System.EventHandler(this.browseTargetDirectory_Click);
            // 
            // targetDirectoryPath
            // 
            this.targetDirectoryPath.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(34)))), ((int)(((byte)(44)))));
            this.targetDirectoryPath.ForeColor = System.Drawing.Color.White;
            this.targetDirectoryPath.Location = new System.Drawing.Point(12, 80);
            this.targetDirectoryPath.Name = "targetDirectoryPath";
            this.targetDirectoryPath.Size = new System.Drawing.Size(308, 20);
            this.targetDirectoryPath.TabIndex = 22;
            // 
            // startButton
            // 
            this.startButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(41)))), ((int)(((byte)(53)))));
            this.startButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.startButton.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.startButton.ForeColor = System.Drawing.Color.White;
            this.startButton.Location = new System.Drawing.Point(10, 148);
            this.startButton.Name = "startButton";
            this.startButton.Size = new System.Drawing.Size(81, 32);
            this.startButton.TabIndex = 24;
            this.startButton.Text = "Start";
            this.startButton.UseVisualStyleBackColor = false;
            this.startButton.Click += new System.EventHandler(this.startButton_Click);
            // 
            // crossAppCheckbox
            // 
            this.crossAppCheckbox.AutoSize = true;
            this.crossAppCheckbox.ForeColor = System.Drawing.Color.White;
            this.crossAppCheckbox.Location = new System.Drawing.Point(12, 116);
            this.crossAppCheckbox.Margin = new System.Windows.Forms.Padding(3, 3, 0, 3);
            this.crossAppCheckbox.Name = "crossAppCheckbox";
            this.crossAppCheckbox.Size = new System.Drawing.Size(121, 17);
            this.crossAppCheckbox.TabIndex = 25;
            this.crossAppCheckbox.Text = "For Cross-App mode";
            this.crossAppCheckbox.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.crossAppCheckbox.UseVisualStyleBackColor = true;
            // 
            // QuixelSuiteSetupDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(34)))), ((int)(((byte)(44)))));
            this.ClientSize = new System.Drawing.Size(403, 189);
            this.Controls.Add(this.crossAppCheckbox);
            this.Controls.Add(this.startButton);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.browseTargetDirectory);
            this.Controls.Add(this.targetDirectoryPath);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ddoProjectBrowse);
            this.Controls.Add(this.ddoProject);
            this.Controls.Add(this.CloseButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "QuixelSuiteSetupDialog";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Start DDO Live Connexion";
            this.TopMost = true;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.Button CloseButton;
        private System.Windows.Forms.Button ddoProjectBrowse;
        private System.Windows.Forms.TextBox ddoProject;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button browseTargetDirectory;
        private System.Windows.Forms.TextBox targetDirectoryPath;
        public System.Windows.Forms.Button startButton;
        private System.Windows.Forms.CheckBox crossAppCheckbox;
    }
}