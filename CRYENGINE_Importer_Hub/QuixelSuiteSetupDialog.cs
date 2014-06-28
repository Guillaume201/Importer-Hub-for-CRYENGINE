using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace CRYENGINE_ImportHub
{
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
                catch (Exception ex)
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

                    m_manWindowDDOButton.Text = "Stop";
                    m_manWindowDDOButton.ForeColor = System.Drawing.Color.Red;

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
