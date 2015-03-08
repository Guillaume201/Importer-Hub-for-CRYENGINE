//-----------------------------------------------------------------------
// <copyright file="QuixelSuiteSetupDialog.cs" company="Guillaume Puyal">
//  The Importer Hub for CRYENGINE is free for any use and open source
//  under Creative Common license (CC BY 4.0).
//
//  In short, you are free to do whatever you want
//  as long as you leave the credits.
//
//  This tool uses the Crytek's Resource Compiler and the Magick.NET
//  library with the following license: http://magick.codeplex.com/license
//  You can access to the full source code on GitHub.
// </copyright>
//-----------------------------------------------------------------------
namespace CRYENGINE_ImportHub
{
    using System;
    using System.IO;
    using System.Windows.Forms;

    public partial class QuixelSuiteSetupDialog : Form
    {
        private Button m_manWindowDDOButton;
        private CQuixelSuiteDDOLiveConvert m_quixelSuiteDDOLiveConvert;

        public QuixelSuiteSetupDialog(Button manWindowDDOButton)
        {
            InitializeComponent();

            startButton.Select();
            m_manWindowDDOButton = manWindowDDOButton;
        }

        private void CloseButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void browseFolderButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Title = "Open DDO Project";
            dialog.Filter = "DDO Project xml|*.xml";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    string file = dialog.FileName;

                    if (file != null)
                    {
                        Framework.Log("File selected from dialog: " + file);
                        ddoProject.Text = dialog.FileName;
                    }
                }
                catch (Exception)
                {
                    Framework.ShowError("Cannot read the file from the disk");
                }
            }
        }

        private void browseTargetDirectory_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            dialog.Description = "Select target directory";

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                string path = dialog.SelectedPath;
                Framework.Log("Path selected from dialog: " + path);

                targetDirectoryPath.Text = path;
            }
        }

        private void startButton_Click(object sender, EventArgs e)
        {
            if (File.Exists(ddoProject.Text))
            {
                if (Directory.Exists(targetDirectoryPath.Text))
                {
                    m_quixelSuiteDDOLiveConvert = new CQuixelSuiteDDOLiveConvert(ddoProject.Text, targetDirectoryPath.Text, crossAppCheckbox.Checked);

                    this.Close();

                    m_manWindowDDOButton.Text = "Stop DDO Connexion";
                    m_manWindowDDOButton.ForeColor = System.Drawing.Color.Red;
                    m_manWindowDDOButton.Width = 140;

                    Framework.FocusProcess("Photoshop");
                }
                else
                {
                    Framework.ShowError("Target directory dosen't exist");
                }
            }
            else
            {
                Framework.ShowError("Unable to find the DDO project xml");
            }
        }

        public void Stop()
        {
            m_quixelSuiteDDOLiveConvert.Stop();
            m_quixelSuiteDDOLiveConvert = null;
        }
    }
}