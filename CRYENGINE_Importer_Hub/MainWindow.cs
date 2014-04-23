using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Drawing;

namespace CRYENGINE_ImportHub
{
    public partial class MainWindow : Form
    {
        private bool m_isImageInClipboard = false;
        private bool m_isCustomOutput = false;
        private string m_customOutput;
        private string[] m_customSlotData;
        private bool m_isLinksManagementPreviouslyOpen = false;
        private Image m_clipboardImage;

        const string APP_VERSION = "0.3";
        const string APP_TITLE_NAME = "Importer Hub for CRYENGINE - v" + APP_VERSION;

        private Framework m_framework;
        private CRegistryManager m_registryManager;

        public MainWindow()
        {
            InitializeComponent();

            m_framework = new Framework(consoleTextbox);
            m_registryManager = new CRegistryManager();

            this.AllowDrop = true;
            this.DragEnter += new DragEventHandler(MainWindow_DragEnter);
            this.DragDrop += new DragEventHandler(MainWindow_DragDrop);

            #if DEBUG
            this.TopMost = false;
            this.Text = APP_TITLE_NAME + " - DEV";
            #endif

            //Use registry settings
            if (m_registryManager.m_customPathInput == "")
                customPath.Text = Framework.GetCRYENGINELocation();
            else
                customPath.Text = m_registryManager.m_customPathInput;

            m_customSlotData = CRegistryManager.GetCustomSlots();
            SetCustomSlots();

            this.Text = APP_TITLE_NAME;

            Framework.Log(APP_TITLE_NAME + " ready!");
        }


        //After form load: BackgroundWorker for Update check
        private void MainWindow_Shown(object sender, EventArgs e)
        {
            System.ComponentModel.BackgroundWorker bw = new System.ComponentModel.BackgroundWorker();
            bw.WorkerSupportsCancellation = true;
            bw.WorkerReportsProgress = true;
            bw.DoWork += new System.ComponentModel.DoWorkEventHandler(bw_DoWork);

            bw.RunWorkerAsync();
        }

        private void bw_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            System.ComponentModel.BackgroundWorker worker = sender as System.ComponentModel.BackgroundWorker;

            new CUpdateNotification(APP_VERSION);
            worker.CancelAsync();
        }


        //Drag and Drop events
        void MainWindow_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop)) e.Effect = DragDropEffects.Copy;
        }

        void MainWindow_DragDrop(object sender, DragEventArgs e)
        {
            //Multi-files support
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);

            foreach (string file in files)
            {
                Framework.BeginImport(file, m_customOutput);
            }
        }


        private void MainWindow_Activated(object sender, EventArgs e)
        {
            //Clipboard functions
            if (Clipboard.ContainsImage())
            {
                m_isImageInClipboard = true;
                pasteTextureButton.Enabled = true;
            }
            else
            {
                m_isImageInClipboard = false;
                pasteTextureButton.Enabled = false;
            }

            //Refresh custom links if the links management form was open
            if (m_isLinksManagementPreviouslyOpen)
            {
                m_registryManager = null;
                m_registryManager = new CRegistryManager();
                m_customSlotData = CRegistryManager.GetCustomSlots();
                SetCustomSlots();

                m_isLinksManagementPreviouslyOpen = false;
            }
        }

        private void pasteTextureButton_Click(object sender, EventArgs e)
        {
            //Clipboard functions
            if (m_isImageInClipboard)
            {
                SaveFileDialog dialog = new SaveFileDialog();
                dialog.Title = "Set path and texture name";
                dialog.CheckPathExists = true;

                if (m_customOutput != null && Directory.Exists(m_customOutput))
                    dialog.InitialDirectory = m_customOutput;

                Framework.Log("Image in clipboard: saving in memory");
                m_clipboardImage = (Image)Clipboard.GetDataObject().GetData(DataFormats.Bitmap, true);

                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    string file = dialog.FileName;
                    Framework.Log("Save path selected from dialog: " + file);

                    new CTextureFromClipboard(file, m_customOutput, m_clipboardImage);
                }

                m_clipboardImage.Dispose();
            }
        }


        //Browse files functions
        private void browseFilesButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Title = "Open textures or meshes";
            dialog.Filter = "Textures and Meshes|*.tif; *.jpg; *.png; *.bmp; *.fbx; *.dae; *.tga; *.psd";  //Supported files
            dialog.Multiselect = true;
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    string[] files = dialog.FileNames;
                    foreach (string file in files)
                    {
                        if (file != null)
                        {
                            Framework.Log("File selected from dialog: " + file);
                            Framework.BeginImport(file, m_customOutput);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Framework.ShowError("Cannot read the file from the disk");
                }
            }
        }


        //Custom output folder
        private void useCustomOutput_CheckedChanged(object sender, EventArgs e)
        {
            if (useCustomOutput.Checked)
            {
                m_isCustomOutput = true;

                #if DEBUG
                Framework.Log("Custom folder enabled: activate controls");
                #endif

                customPath.Enabled = true;
                browseFolderButton.Enabled = true;
                m_customOutput = customPath.Text;
            }
            else
            {
                m_isCustomOutput = false;
                customPath.Enabled = false;
                browseFolderButton.Enabled = false;
                m_customOutput = null;
            }
        }

        private void browseFolderButton_Click(object sender, EventArgs e)
        {
            if (m_isCustomOutput)
            {
                FolderBrowserDialog dialog = new FolderBrowserDialog();
                dialog.Description = "Select custom output folder";

                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    string path = dialog.SelectedPath;
                    Framework.Log("Path selected from dialog: " + path);

                    customPath.Text = path;
                    m_customOutput = path;
                }
            }
        }

        private void customPath_TextChanged(object sender, EventArgs e)
        {
            if (useCustomOutput.Checked)
                m_customOutput = customPath.Text;
        }


        //Save settings on app close
        private void MainWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            CRegistryManager.SaveAllSettings(customPath.Text);
        }


        //Custom slots
        private void SetCustomSlots()
        {
            Framework.Log("Set custom links");

            ResetCustomSlots();

            int i = 0;
            foreach (string noFormat in m_customSlotData)
            {
                string[] values = CCustomSlot.GetDataFromId(m_customSlotData, i);
                if (values != null)
                {
                    Button currentButton = CCustomSlot.GetButtonFromId(this, i);

                    currentButton.Text = values[0];
                    currentButton.Enabled = true;
                }

                i++;
            }
        }

        private void ResetCustomSlots()
        {
            string defaultText = "Custom link";

            customSlotButton1.Enabled = false;
            customSlotButton1.Text = defaultText;
            customSlotButton2.Enabled = false;
            customSlotButton2.Text = defaultText;
            customSlotButton3.Enabled = false;
            customSlotButton3.Text = defaultText;
            customSlotButton4.Enabled = false;
            customSlotButton4.Text = defaultText;
            customSlotButton5.Enabled = false;
            customSlotButton5.Text = defaultText;
            customSlotButton6.Enabled = false;
            customSlotButton6.Text = defaultText;
        }

        private void manageButton_Click(object sender, EventArgs e)
        {
            LinksManagementWindow managementWindow = new LinksManagementWindow(m_customSlotData);
            Dock = DockStyle.Fill;
            if (!Application.OpenForms.OfType<LinksManagementWindow>().Any())
            {
                managementWindow.Show();
                m_isLinksManagementPreviouslyOpen = true;
            }
        }


        //Custom slots events
        private void customSlotButton1_Click(object sender, EventArgs e)
        {
            CCustomSlot.ExeSlotAction(1, m_customSlotData);
        }

        private void customSlotButton2_Click(object sender, EventArgs e)
        {
            CCustomSlot.ExeSlotAction(2, m_customSlotData);
        }

        private void customSlotButton3_Click(object sender, EventArgs e)
        {
            CCustomSlot.ExeSlotAction(3, m_customSlotData);
        }

        private void customSlotButton4_Click(object sender, EventArgs e)
        {
            CCustomSlot.ExeSlotAction(4, m_customSlotData);
        }

        private void customSlotButton5_Click(object sender, EventArgs e)
        {
            CCustomSlot.ExeSlotAction(5, m_customSlotData);
        }

        private void customSlotButton6_Click(object sender, EventArgs e)
        {
            CCustomSlot.ExeSlotAction(6, m_customSlotData);
        }


        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("http://www.guillaume-puyal.com/");
        }
    }
}
