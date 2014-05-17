namespace CRYENGINE_ImportHub
{
    partial class MainWindow
    {
        /// <summary>
        /// Variable nécessaire au concepteur.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Nettoyage des ressources utilisées.
        /// </summary>
        /// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Code généré par le Concepteur Windows Form

        /// <summary>
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.showCryTif = new System.Windows.Forms.CheckBox();
            this.browseFolderButton = new System.Windows.Forms.Button();
            this.customPath = new System.Windows.Forms.TextBox();
            this.useCustomOutput = new System.Windows.Forms.CheckBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.browseFilesButton = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.pasteTextureButton = new System.Windows.Forms.Button();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.consoleTextbox = new System.Windows.Forms.RichTextBox();
            this.customSlotButton2 = new System.Windows.Forms.Button();
            this.customSlotButton1 = new System.Windows.Forms.Button();
            this.customSlotButton3 = new System.Windows.Forms.Button();
            this.customSlotButton6 = new System.Windows.Forms.Button();
            this.customSlotButton5 = new System.Windows.Forms.Button();
            this.customSlotButton4 = new System.Windows.Forms.Button();
            this.manageButton = new System.Windows.Forms.Button();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(40, 50);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(283, 28);
            this.label1.TabIndex = 1;
            this.label1.Text = "Drop your textures and meshes";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.showCryTif);
            this.groupBox1.Controls.Add(this.browseFolderButton);
            this.groupBox1.Controls.Add(this.customPath);
            this.groupBox1.Controls.Add(this.useCustomOutput);
            this.groupBox1.ForeColor = System.Drawing.Color.White;
            this.groupBox1.Location = new System.Drawing.Point(15, 306);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(339, 70);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Options";
            // 
            // showCryTif
            // 
            this.showCryTif.AutoSize = true;
            this.showCryTif.Checked = true;
            this.showCryTif.CheckState = System.Windows.Forms.CheckState.Checked;
            this.showCryTif.Location = new System.Drawing.Point(224, 19);
            this.showCryTif.Margin = new System.Windows.Forms.Padding(3, 3, 0, 3);
            this.showCryTif.Name = "showCryTif";
            this.showCryTif.Size = new System.Drawing.Size(114, 17);
            this.showCryTif.TabIndex = 8;
            this.showCryTif.Text = "Show CryTif dialog";
            this.showCryTif.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.showCryTif.UseVisualStyleBackColor = true;
            // 
            // browseFolderButton
            // 
            this.browseFolderButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(41)))), ((int)(((byte)(53)))));
            this.browseFolderButton.Enabled = false;
            this.browseFolderButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.browseFolderButton.Font = new System.Drawing.Font("Segoe UI", 7.5F);
            this.browseFolderButton.ForeColor = System.Drawing.Color.White;
            this.browseFolderButton.Location = new System.Drawing.Point(268, 42);
            this.browseFolderButton.Name = "browseFolderButton";
            this.browseFolderButton.Size = new System.Drawing.Size(65, 20);
            this.browseFolderButton.TabIndex = 7;
            this.browseFolderButton.Text = "Browse";
            this.browseFolderButton.UseVisualStyleBackColor = false;
            this.browseFolderButton.Click += new System.EventHandler(this.browseFolderButton_Click);
            // 
            // customPath
            // 
            this.customPath.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(34)))), ((int)(((byte)(44)))));
            this.customPath.Enabled = false;
            this.customPath.ForeColor = System.Drawing.Color.White;
            this.customPath.Location = new System.Drawing.Point(6, 42);
            this.customPath.Name = "customPath";
            this.customPath.Size = new System.Drawing.Size(256, 20);
            this.customPath.TabIndex = 7;
            this.customPath.TextChanged += new System.EventHandler(this.customPath_TextChanged);
            // 
            // useCustomOutput
            // 
            this.useCustomOutput.AutoSize = true;
            this.useCustomOutput.Location = new System.Drawing.Point(6, 19);
            this.useCustomOutput.Name = "useCustomOutput";
            this.useCustomOutput.Size = new System.Drawing.Size(144, 17);
            this.useCustomOutput.TabIndex = 7;
            this.useCustomOutput.Text = "Use custom output folder";
            this.useCustomOutput.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.useCustomOutput.UseVisualStyleBackColor = true;
            this.useCustomOutput.CheckedChanged += new System.EventHandler(this.useCustomOutput_CheckedChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.Color.DarkGray;
            this.label4.Location = new System.Drawing.Point(221, 626);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(58, 13);
            this.label4.TabIndex = 15;
            this.label4.Text = "Created by";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.DarkGray;
            this.label5.Location = new System.Drawing.Point(185, 641);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(172, 13);
            this.label5.TabIndex = 17;
            this.label5.Text = "Use the Crytek\'s Resource Compiler";
            this.label5.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // browseFilesButton
            // 
            this.browseFilesButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(41)))), ((int)(((byte)(53)))));
            this.browseFilesButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.browseFilesButton.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.browseFilesButton.ForeColor = System.Drawing.Color.White;
            this.browseFilesButton.Location = new System.Drawing.Point(15, 145);
            this.browseFilesButton.Name = "browseFilesButton";
            this.browseFilesButton.Size = new System.Drawing.Size(339, 54);
            this.browseFilesButton.TabIndex = 2;
            this.browseFilesButton.Text = "Browse textures and meshes";
            this.browseFilesButton.UseVisualStyleBackColor = false;
            this.browseFilesButton.Click += new System.EventHandler(this.browseFilesButton_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(171, 118);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(25, 21);
            this.label2.TabIndex = 3;
            this.label2.Text = "or";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(171, 206);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(25, 21);
            this.label3.TabIndex = 5;
            this.label3.Text = "or";
            // 
            // pasteTextureButton
            // 
            this.pasteTextureButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(41)))), ((int)(((byte)(53)))));
            this.pasteTextureButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.pasteTextureButton.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pasteTextureButton.ForeColor = System.Drawing.Color.White;
            this.pasteTextureButton.Location = new System.Drawing.Point(15, 233);
            this.pasteTextureButton.Name = "pasteTextureButton";
            this.pasteTextureButton.Size = new System.Drawing.Size(339, 54);
            this.pasteTextureButton.TabIndex = 4;
            this.pasteTextureButton.Text = "Paste texture from clipboard";
            this.pasteTextureButton.UseVisualStyleBackColor = false;
            this.pasteTextureButton.Click += new System.EventHandler(this.pasteTextureButton_Click);
            // 
            // openFileDialog
            // 
            this.openFileDialog.FileName = "openFileDialog";
            // 
            // consoleTextbox
            // 
            this.consoleTextbox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(49)))), ((int)(((byte)(65)))));
            this.consoleTextbox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.consoleTextbox.DetectUrls = false;
            this.consoleTextbox.ForeColor = System.Drawing.Color.White;
            this.consoleTextbox.Location = new System.Drawing.Point(12, 545);
            this.consoleTextbox.Name = "consoleTextbox";
            this.consoleTextbox.ReadOnly = true;
            this.consoleTextbox.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.consoleTextbox.Size = new System.Drawing.Size(345, 74);
            this.consoleTextbox.TabIndex = 7;
            this.consoleTextbox.Text = "";
            // 
            // customSlotButton2
            // 
            this.customSlotButton2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(41)))), ((int)(((byte)(53)))));
            this.customSlotButton2.Enabled = false;
            this.customSlotButton2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.customSlotButton2.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.customSlotButton2.ForeColor = System.Drawing.Color.White;
            this.customSlotButton2.Location = new System.Drawing.Point(132, 396);
            this.customSlotButton2.Name = "customSlotButton2";
            this.customSlotButton2.Size = new System.Drawing.Size(109, 47);
            this.customSlotButton2.TabIndex = 9;
            this.customSlotButton2.Text = "Custom slot";
            this.customSlotButton2.UseVisualStyleBackColor = false;
            this.customSlotButton2.Click += new System.EventHandler(this.customSlotButton2_Click);
            // 
            // customSlotButton1
            // 
            this.customSlotButton1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(41)))), ((int)(((byte)(53)))));
            this.customSlotButton1.Enabled = false;
            this.customSlotButton1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.customSlotButton1.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.customSlotButton1.ForeColor = System.Drawing.Color.White;
            this.customSlotButton1.Location = new System.Drawing.Point(15, 396);
            this.customSlotButton1.Name = "customSlotButton1";
            this.customSlotButton1.Size = new System.Drawing.Size(109, 47);
            this.customSlotButton1.TabIndex = 8;
            this.customSlotButton1.Text = "Custom slot";
            this.customSlotButton1.UseVisualStyleBackColor = false;
            this.customSlotButton1.Click += new System.EventHandler(this.customSlotButton1_Click);
            // 
            // customSlotButton3
            // 
            this.customSlotButton3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(41)))), ((int)(((byte)(53)))));
            this.customSlotButton3.Enabled = false;
            this.customSlotButton3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.customSlotButton3.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.customSlotButton3.ForeColor = System.Drawing.Color.White;
            this.customSlotButton3.Location = new System.Drawing.Point(248, 396);
            this.customSlotButton3.Name = "customSlotButton3";
            this.customSlotButton3.Size = new System.Drawing.Size(109, 47);
            this.customSlotButton3.TabIndex = 10;
            this.customSlotButton3.Text = "Custom slot";
            this.customSlotButton3.UseVisualStyleBackColor = false;
            this.customSlotButton3.Click += new System.EventHandler(this.customSlotButton3_Click);
            // 
            // customSlotButton6
            // 
            this.customSlotButton6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(41)))), ((int)(((byte)(53)))));
            this.customSlotButton6.Enabled = false;
            this.customSlotButton6.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.customSlotButton6.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.customSlotButton6.ForeColor = System.Drawing.Color.White;
            this.customSlotButton6.Location = new System.Drawing.Point(248, 450);
            this.customSlotButton6.Name = "customSlotButton6";
            this.customSlotButton6.Size = new System.Drawing.Size(109, 47);
            this.customSlotButton6.TabIndex = 13;
            this.customSlotButton6.Text = "Custom slot";
            this.customSlotButton6.UseVisualStyleBackColor = false;
            this.customSlotButton6.Click += new System.EventHandler(this.customSlotButton6_Click);
            // 
            // customSlotButton5
            // 
            this.customSlotButton5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(41)))), ((int)(((byte)(53)))));
            this.customSlotButton5.Enabled = false;
            this.customSlotButton5.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.customSlotButton5.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.customSlotButton5.ForeColor = System.Drawing.Color.White;
            this.customSlotButton5.Location = new System.Drawing.Point(132, 450);
            this.customSlotButton5.Name = "customSlotButton5";
            this.customSlotButton5.Size = new System.Drawing.Size(109, 47);
            this.customSlotButton5.TabIndex = 12;
            this.customSlotButton5.Text = "Custom slot";
            this.customSlotButton5.UseVisualStyleBackColor = false;
            this.customSlotButton5.Click += new System.EventHandler(this.customSlotButton5_Click);
            // 
            // customSlotButton4
            // 
            this.customSlotButton4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(41)))), ((int)(((byte)(53)))));
            this.customSlotButton4.Enabled = false;
            this.customSlotButton4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.customSlotButton4.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.customSlotButton4.ForeColor = System.Drawing.Color.White;
            this.customSlotButton4.Location = new System.Drawing.Point(15, 450);
            this.customSlotButton4.Name = "customSlotButton4";
            this.customSlotButton4.Size = new System.Drawing.Size(109, 47);
            this.customSlotButton4.TabIndex = 11;
            this.customSlotButton4.Text = "Custom slot";
            this.customSlotButton4.UseVisualStyleBackColor = false;
            this.customSlotButton4.Click += new System.EventHandler(this.customSlotButton4_Click);
            // 
            // manageButton
            // 
            this.manageButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(41)))), ((int)(((byte)(53)))));
            this.manageButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.manageButton.Font = new System.Drawing.Font("Segoe UI", 7.5F);
            this.manageButton.ForeColor = System.Drawing.Color.White;
            this.manageButton.Location = new System.Drawing.Point(261, 508);
            this.manageButton.Name = "manageButton";
            this.manageButton.Size = new System.Drawing.Size(96, 23);
            this.manageButton.TabIndex = 14;
            this.manageButton.Text = "Customize links";
            this.manageButton.UseVisualStyleBackColor = false;
            this.manageButton.Click += new System.EventHandler(this.manageButton_Click);
            // 
            // linkLabel1
            // 
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.LinkColor = System.Drawing.Color.DarkGray;
            this.linkLabel1.Location = new System.Drawing.Point(275, 626);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(82, 13);
            this.linkLabel1.TabIndex = 16;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "Guillaume Puyal";
            this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // pictureBox1
            // 
            this.pictureBox1.ErrorImage = global::CRYENGINE_Importer_Hub.Properties.Resources.border;
            this.pictureBox1.Image = global::CRYENGINE_Importer_Hub.Properties.Resources.border;
            this.pictureBox1.InitialImage = global::CRYENGINE_Importer_Hub.Properties.Resources.border;
            this.pictureBox1.Location = new System.Drawing.Point(12, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(348, 108);
            this.pictureBox1.TabIndex = 18;
            this.pictureBox1.TabStop = false;
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(34)))), ((int)(((byte)(44)))));
            this.CausesValidation = false;
            this.ClientSize = new System.Drawing.Size(369, 663);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.linkLabel1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.manageButton);
            this.Controls.Add(this.customSlotButton6);
            this.Controls.Add(this.customSlotButton5);
            this.Controls.Add(this.customSlotButton4);
            this.Controls.Add(this.customSlotButton3);
            this.Controls.Add(this.customSlotButton2);
            this.Controls.Add(this.customSlotButton1);
            this.Controls.Add(this.consoleTextbox);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.pasteTextureButton);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.browseFilesButton);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pictureBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = global::CRYENGINE_Importer_Hub.Resource1.CEI;
            this.MaximizeBox = false;
            this.Name = "MainWindow";
            this.Text = "CRYENGINE Quick Import";
            this.TopMost = true;
            this.Activated += new System.EventHandler(this.MainWindow_Activated);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainWindow_FormClosing);
            this.Shown += new System.EventHandler(this.MainWindow_Shown);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button browseFilesButton;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button pasteTextureButton;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.SaveFileDialog saveFileDialog;
        private System.Windows.Forms.TextBox customPath;
        private System.Windows.Forms.CheckBox useCustomOutput;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog;
        private System.Windows.Forms.Button browseFolderButton;
        public System.Windows.Forms.RichTextBox consoleTextbox;
        private System.Windows.Forms.Button manageButton;
        public System.Windows.Forms.Button customSlotButton1;
        public System.Windows.Forms.Button customSlotButton2;
        public System.Windows.Forms.Button customSlotButton3;
        public System.Windows.Forms.Button customSlotButton6;
        public System.Windows.Forms.Button customSlotButton5;
        public System.Windows.Forms.Button customSlotButton4;
        private System.Windows.Forms.LinkLabel linkLabel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.CheckBox showCryTif;
    }
}

