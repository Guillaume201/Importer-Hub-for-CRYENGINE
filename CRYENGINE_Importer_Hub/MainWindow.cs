//-----------------------------------------------------------------------
// <copyright file="MainWindow.cs" company="Guillaume Puyal">
//  The Importer Hub for CRYENGINE is free for any use and open source
//  under Creative Common license (CC BY 4.0).
//
//  In short, you are free to do whatever you want
//  as long as you leave the credits.
//
//  This tool uses the Crytek's Resource Compiler and the Magick.NET
//  library with the following license: http:// magick.codeplex.com/license
//  You can access to the full source code on GitHub.
// </copyright>
//-----------------------------------------------------------------------
namespace CRYENGINE_ImportHub
{
    using System;
    using System.Diagnostics;
    using System.Drawing;
    using System.IO;
    using System.Linq;
    using System.Windows.Forms;

    public partial class MainWindow : Form
    {
        private bool isImageInClipboard = false;
        private bool isCustomOutput = false;
        private string customOutput;
        private string[] customSlotData;
        private bool isLinksManagementPreviouslyOpen = false;
        private Image clipboardImage;

        private string Version;
        private string APP_TITLE_NAME;

        private Framework framework;
        private RegistryManager registryManager;
        private QuixelSuiteSetupDialog quixelSuiteDialog;

        public MainWindow()
        {
            InitializeComponent();

            framework = new Framework(consoleTextbox, showCryTif);
            registryManager = new RegistryManager();

            this.AllowDrop = true;
            this.DragEnter += new DragEventHandler(MainWindow_DragEnter);
            this.DragDrop += new DragEventHandler(MainWindow_DragDrop);

            this.Version = this.GetAssemblyVersion().ToString();
            this.APP_TITLE_NAME = "Importer Hub for CRYENGINE - v" + this.Version;

            this.Text = APP_TITLE_NAME;
#if DEBUG
            this.TopMost = false;
            this.Text = APP_TITLE_NAME + " - DEV";
#endif

            customPath.Text = string.IsNullOrEmpty(registryManager.CustomPathInput) ? Framework.GetCRYENGINELocation() : registryManager.CustomPathInput;
            showCryTif.Checked = registryManager.UseCryTifDialog;

            customSlotData = RegistryManager.CustomSlots;
            SetCustomSlots();

            Framework.Log(APP_TITLE_NAME + " ready!");
        }

        // After form load: BackgroundWorker for Update check
        private void MainWindow_Shown(object sender, EventArgs e)
        {
#if !DEBUG
            System.ComponentModel.BackgroundWorker bw = new System.ComponentModel.BackgroundWorker();
            bw.WorkerSupportsCancellation = true;
            bw.WorkerReportsProgress = true;
            bw.DoWork += new System.ComponentModel.DoWorkEventHandler(bw_DoWork);

            bw.RunWorkerAsync();
#endif
        }

        private void bw_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            System.ComponentModel.BackgroundWorker worker = sender as System.ComponentModel.BackgroundWorker;

            new UpdateNotification(this.Version);
            worker.CancelAsync();
        }

        // Drag and Drop events
        private void MainWindow_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop)) e.Effect = DragDropEffects.Copy;
        }

        private void MainWindow_DragDrop(object sender, DragEventArgs e)
        {
            // Multi-files support
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);

            foreach (string file in files)
            {
                Framework.BeginImport(file, customOutput);
            }
        }

        private void MainWindow_Activated(object sender, EventArgs e)
        {
            // Clipboard functions
            if (Clipboard.ContainsImage())
            {
                isImageInClipboard = true;
                pasteTextureButton.Enabled = true;
            }
            else
            {
                isImageInClipboard = false;
                pasteTextureButton.Enabled = false;
            }

            // Refresh custom links if the links management form was open
            if (isLinksManagementPreviouslyOpen)
            {
                registryManager = null;
                registryManager = new RegistryManager();
                customSlotData = RegistryManager.GetCustomSlots();
                SetCustomSlots();

                isLinksManagementPreviouslyOpen = false;
            }
        }

        private void pasteTextureButton_Click(object sender, EventArgs e)
        {
            // Clipboard functions
            if (isImageInClipboard)
            {
                SaveFileDialog dialog = new SaveFileDialog();
                dialog.Title = "Set path and texture name";
                dialog.CheckPathExists = true;
                dialog.Filter = "Textures|*.tif; *.dds";

                if (customOutput != null && Directory.Exists(customOutput))
                    dialog.InitialDirectory = customOutput;

                Framework.Log("Image in clipboard: saving in memory");
                clipboardImage = (Image)Clipboard.GetDataObject().GetData(DataFormats.Bitmap, true);

                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    string file = dialog.FileName;
                    Framework.Log("Save path selected from dialog: " + file);

                    new TextureFromClipboard(file, clipboardImage);
                }

                clipboardImage.Dispose();
            }
        }

        // Browse files functions
        private void browseFilesButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Title = "Open textures or meshes";
            dialog.Filter = "Textures and Meshes|*.tif; *.jpg; *.png; *.bmp; *.fbx; *.dae; *.tga; *.psd";  // Supported files
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
                            Framework.BeginImport(file, customOutput);
                        }
                    }
                }
                catch (Exception)
                {
                    Framework.ShowError("Cannot read the file from the disk");
                }
            }
        }

        // Custom output folder
        private void useCustomOutput_CheckedChanged(object sender, EventArgs e)
        {
            if (useCustomOutput.Checked)
            {
                isCustomOutput = true;

#if DEBUG
                Framework.Log("Custom folder enabled: activate controls");
#endif

                customPath.Enabled = true;
                browseFolderButton.Enabled = true;
                customOutput = customPath.Text;
            }
            else
            {
                isCustomOutput = false;
                customPath.Enabled = false;
                browseFolderButton.Enabled = false;
                customOutput = null;
            }
        }

        private void browseFolderButton_Click(object sender, EventArgs e)
        {
            if (isCustomOutput)
            {
                FolderBrowserDialog dialog = new FolderBrowserDialog();
                dialog.Description = "Select custom output folder";

                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    string path = dialog.SelectedPath;
                    Framework.Log("Path selected from dialog: " + path);

                    customPath.Text = path;
                    customOutput = path;
                }
            }
        }

        private void customPath_TextChanged(object sender, EventArgs e)
        {
            if (useCustomOutput.Checked)
                customOutput = customPath.Text;
        }

        // Save settings on app close
        private void MainWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            RegistryManager.SaveAllSettings(customPath.Text, showCryTif.Checked);
        }

        // Custom slots
        private void SetCustomSlots()
        {
            Framework.Log("Set custom links");

            ResetCustomSlots();

            int i = 0;
            foreach (string noFormat in customSlotData)
            {
                string[] values = CustomSlot.GetDataFromId(customSlotData, i);
                if (values != null)
                {
                    Button currentButton = CustomSlot.GetButtonFromId(this, i);
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
            LinksManagementWindow managementWindow = new LinksManagementWindow(customSlotData);
            Dock = DockStyle.Fill;
            if (!Application.OpenForms.OfType<LinksManagementWindow>().Any())
            {
                managementWindow.Show();
                isLinksManagementPreviouslyOpen = true;
            }
        }

        // Custom slots events
        private void customSlotButton1_Click(object sender, EventArgs e)
        {
            CustomSlot.ExeSlotAction(1, customSlotData);
        }

        private void customSlotButton2_Click(object sender, EventArgs e)
        {
            CustomSlot.ExeSlotAction(2, customSlotData);
        }

        private void customSlotButton3_Click(object sender, EventArgs e)
        {
            CustomSlot.ExeSlotAction(3, customSlotData);
        }

        private void customSlotButton4_Click(object sender, EventArgs e)
        {
            CustomSlot.ExeSlotAction(4, customSlotData);
        }

        private void customSlotButton5_Click(object sender, EventArgs e)
        {
            CustomSlot.ExeSlotAction(5, customSlotData);
        }

        private void customSlotButton6_Click(object sender, EventArgs e)
        {
            CustomSlot.ExeSlotAction(6, customSlotData);
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("http:// www.guillaume-puyal.com/");
        }

        // Quixel Suite live
        private void quixelSuiteLive_Click(object sender, EventArgs e)
        {
            if (quixelSuiteLive.ForeColor != System.Drawing.Color.Red)
            {
                quixelSuiteDialog = new QuixelSuiteSetupDialog(quixelSuiteLive);

                Dock = DockStyle.Fill;
                if (!Application.OpenForms.OfType<QuixelSuiteSetupDialog>().Any())
                {
                    quixelSuiteDialog.Show();
                }
            }
            else
            {
                quixelSuiteDialog.Stop();
                quixelSuiteDialog.Dispose();

                quixelSuiteLive.Text = "Quixel Suite DDO";
                quixelSuiteLive.ForeColor = System.Drawing.Color.White;
                quixelSuiteLive.Width = 113;
            }
        }

        private Version GetAssemblyVersion()
        {
            return typeof(MainWindow).Assembly.GetName().Version;
        }
    }
}