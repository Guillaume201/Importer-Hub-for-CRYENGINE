namespace CRYENGINE_ImportHub
{
    partial class LinksManagementWindow
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
            System.Windows.Forms.Label label1;
            System.Windows.Forms.Label label2;
            System.Windows.Forms.Label label3;
            this.CloseButton = new System.Windows.Forms.Button();
            this.linksListBox = new System.Windows.Forms.ListBox();
            this.LinkTitle = new System.Windows.Forms.TextBox();
            this.LinkPath = new System.Windows.Forms.TextBox();
            this.LinkArgs = new System.Windows.Forms.TextBox();
            label1 = new System.Windows.Forms.Label();
            label2 = new System.Windows.Forms.Label();
            label3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.ForeColor = System.Drawing.Color.White;
            label1.Location = new System.Drawing.Point(189, 12);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(46, 13);
            label1.TabIndex = 19;
            label1.Text = "Link title";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.ForeColor = System.Drawing.Color.White;
            label2.Location = new System.Drawing.Point(189, 62);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(197, 13);
            label2.TabIndex = 20;
            label2.Text = "Target path (exe, bat, file, folder or URL)";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.ForeColor = System.Drawing.Color.White;
            label3.Location = new System.Drawing.Point(189, 112);
            label3.Name = "label3";
            label3.Size = new System.Drawing.Size(125, 13);
            label3.TabIndex = 21;
            label3.Text = "Command line arguments";
            // 
            // CloseButton
            // 
            this.CloseButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(41)))), ((int)(((byte)(53)))));
            this.CloseButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.CloseButton.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.CloseButton.ForeColor = System.Drawing.Color.White;
            this.CloseButton.Location = new System.Drawing.Point(12, 185);
            this.CloseButton.Name = "CloseButton";
            this.CloseButton.Size = new System.Drawing.Size(81, 32);
            this.CloseButton.TabIndex = 14;
            this.CloseButton.Text = "Close";
            this.CloseButton.UseVisualStyleBackColor = false;
            this.CloseButton.Click += new System.EventHandler(this.CloseButton_Click);
            // 
            // linksListBox
            // 
            this.linksListBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(34)))), ((int)(((byte)(44)))));
            this.linksListBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.linksListBox.ForeColor = System.Drawing.Color.White;
            this.linksListBox.FormattingEnabled = true;
            this.linksListBox.Location = new System.Drawing.Point(12, 12);
            this.linksListBox.Name = "linksListBox";
            this.linksListBox.Size = new System.Drawing.Size(171, 158);
            this.linksListBox.TabIndex = 15;
            this.linksListBox.SelectedIndexChanged += new System.EventHandler(this.linksListBox_SelectedIndexChanged);
            // 
            // LinkTitle
            // 
            this.LinkTitle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(34)))), ((int)(((byte)(44)))));
            this.LinkTitle.ForeColor = System.Drawing.Color.White;
            this.LinkTitle.Location = new System.Drawing.Point(192, 28);
            this.LinkTitle.Name = "LinkTitle";
            this.LinkTitle.Size = new System.Drawing.Size(432, 20);
            this.LinkTitle.TabIndex = 16;
            this.LinkTitle.Leave += new System.EventHandler(this.LinkTitle_Leave);
            // 
            // LinkPath
            // 
            this.LinkPath.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(34)))), ((int)(((byte)(44)))));
            this.LinkPath.ForeColor = System.Drawing.Color.White;
            this.LinkPath.Location = new System.Drawing.Point(192, 78);
            this.LinkPath.Name = "LinkPath";
            this.LinkPath.Size = new System.Drawing.Size(432, 20);
            this.LinkPath.TabIndex = 17;
            this.LinkPath.Leave += new System.EventHandler(this.LinkPath_Leave);
            // 
            // LinkArgs
            // 
            this.LinkArgs.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(34)))), ((int)(((byte)(44)))));
            this.LinkArgs.ForeColor = System.Drawing.Color.White;
            this.LinkArgs.Location = new System.Drawing.Point(192, 128);
            this.LinkArgs.Name = "LinkArgs";
            this.LinkArgs.Size = new System.Drawing.Size(432, 20);
            this.LinkArgs.TabIndex = 18;
            this.LinkArgs.Leave += new System.EventHandler(this.LinkArgs_Leave);
            // 
            // LinksManagementWindow
            // 
            this.AcceptButton = this.CloseButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(34)))), ((int)(((byte)(44)))));
            this.ClientSize = new System.Drawing.Size(637, 230);
            this.Controls.Add(label3);
            this.Controls.Add(label2);
            this.Controls.Add(label1);
            this.Controls.Add(this.LinkArgs);
            this.Controls.Add(this.LinkPath);
            this.Controls.Add(this.LinkTitle);
            this.Controls.Add(this.linksListBox);
            this.Controls.Add(this.CloseButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "LinksManagementWindow";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Customize links";
            this.TopMost = true;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        public System.Windows.Forms.Button CloseButton;
        private System.Windows.Forms.ListBox linksListBox;
        private System.Windows.Forms.TextBox LinkTitle;
        private System.Windows.Forms.TextBox LinkPath;
        private System.Windows.Forms.TextBox LinkArgs;
    }
}