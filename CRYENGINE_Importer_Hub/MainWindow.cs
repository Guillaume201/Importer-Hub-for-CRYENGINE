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

    /// <summary>
    /// Implementation of MainWindow form.
    /// </summary>
    public partial class MainWindow : Form
    {
        private Image clipboardImage;
        private bool isImageInClipboard = false;

        private bool isCustomOutput = false;
        private string customOutput;
        private string[] customSlotData;
        private bool isLinksManagementPreviouslyOpen = false;

        /// <summary>
        /// Holds an instance of the <see cref="Framework"/>.
        /// </summary>
        private Framework framework;

        /// <summary>
        /// Holds an instance of the <see cref="RegistryManager"/>.
        /// </summary>
        private RegistryManager registryManager;

        /// <summary>
        /// Holds an instance of the <see cref="QuixelSuiteSetupDialog"/>.
        /// </summary>
        private QuixelSuiteSetupDialog quixelSuiteDialog;

        /// <summary>
        /// Initializes a new instance of the <see cref="MainWindow"/> class.
        /// </summary>
        public MainWindow()
        {
            this.InitializeComponent();

            this.framework = new Framework(consoleTextbox, showCryTif);
            this.registryManager = new RegistryManager();

            this.AllowDrop = true;
            this.DragEnter += new DragEventHandler(this.MainWindow_DragEnter);
            this.DragDrop += new DragEventHandler(this.MainWindow_DragDrop);

            this.Text = this.AppTitleName;
            this.TopMost = !this.IsDebug;

            this.customPath.Text = string.IsNullOrEmpty(this.registryManager.CustomPathInput) ? Framework.GetCRYENGINELocation() : this.registryManager.CustomPathInput;
            this.showCryTif.Checked = this.registryManager.UseCryTifDialog;

            this.customSlotData = RegistryManager.CustomSlots;
            this.SetCustomSlots();

            Framework.Log(this.AppTitleName + " ready!");
        }

        /// <summary>
        /// Gets the current version of the application from the assembly.
        /// </summary>
        private string Version
        {
            get
            {
                return this.GetAssemblyVersion().ToString();
            }
        }

        /// <summary>
        /// Gets the application title name.
        /// </summary>
        private string AppTitleName
        {
            get
            {
                return string.Format("{0} - v{1}{2}", "Importer Hub for CRYENGINE", this.Version, this.IsDebug ? "- DEV" : string.Empty);
            }
        }

        /// <summary>
        /// Gets a value indicating whether the current build is a debug build or not.
        /// </summary>
        private bool IsDebug
        {
            get
            {
#if DEBUG
                return true;
#else
                return false;
#endif
            }
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
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.Copy;
            }
        }

        private void MainWindow_DragDrop(object sender, DragEventArgs e)
        {
            // Multi-files support
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);

            foreach (string file in files)
            {
                Framework.BeginImport(file, this.customOutput);
            }
        }

        private void MainWindow_Activated(object sender, EventArgs e)
        {
            // Clipboard functions
            if (Clipboard.ContainsImage())
            {
                this.isImageInClipboard = true;
                this.pasteTextureButton.Enabled = true;
            }
            else
            {
                this.isImageInClipboard = false;
                this.pasteTextureButton.Enabled = false;
            }

            // Refresh custom links if the links management form was open
            if (this.isLinksManagementPreviouslyOpen)
            {
                this.registryManager = new RegistryManager();
                this.customSlotData = RegistryManager.GetCustomSlots();
                this.SetCustomSlots();

                this.isLinksManagementPreviouslyOpen = false;
            }
        }

        private void pasteTextureButton_Click(object sender, EventArgs e)
        {
            // Clipboard functions
            if (this.isImageInClipboard)
            {
                SaveFileDialog dialog = new SaveFileDialog();
                dialog.Title = "Set path and texture name";
                dialog.CheckPathExists = true;
                dialog.Filter = "Textures|*.tif; *.dds";

                if (this.customOutput != null && Directory.Exists(this.customOutput))
                {
                    dialog.InitialDirectory = this.customOutput;
                }

                Framework.Log("Image in clipboard: saving in memory");
                this.clipboardImage = (Image)Clipboard.GetDataObject().GetData(DataFormats.Bitmap, true);

                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    string file = dialog.FileName;
                    Framework.Log("Save path selected from dialog: " + file);

                    new TextureFromClipboard(file, this.clipboardImage);
                }

                this.clipboardImage.Dispose();
            }
        }

        // Browse files functions
        private void BrowseFilesButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog
            {
                Title = "Open textures or meshes",
                Filter = "Textures and Meshes|*.tif; *.jpg; *.png; *.bmp; *.fbx; *.dae; *.tga; *.psd",  // Supported files
                Multiselect = true
            };

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    var files = dialog.FileNames;
                    foreach (string file in files)
                    {
                        if (file != null)
                        {
                            Framework.Log("File selected from dialog: " + file);
                            Framework.BeginImport(file, this.customOutput);
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
                this.isCustomOutput = true;

#if DEBUG
                Framework.Log("Custom folder enabled: activate controls");
#endif

                this.customPath.Enabled = true;
                this.browseFolderButton.Enabled = true;
                this.customOutput = customPath.Text;
            }
            else
            {
                this.isCustomOutput = false;
                this.customPath.Enabled = false;
                this.browseFolderButton.Enabled = false;
                this.customOutput = null;
            }
        }

        private void browseFolderButton_Click(object sender, EventArgs e)
        {
            if (this.isCustomOutput)
            {
                FolderBrowserDialog dialog = new FolderBrowserDialog();
                dialog.Description = "Select custom output folder";

                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    string path = dialog.SelectedPath;
                    Framework.Log("Path selected from dialog: " + path);

                    this.customPath.Text = path;
                    this.customOutput = path;
                }
            }
        }

        private void customPath_TextChanged(object sender, EventArgs e)
        {
            if (useCustomOutput.Checked)
            {
                this.customOutput = customPath.Text;
            }
        }

        /// <summary>
        /// Event handler for Close event on MainWindow.
        /// Ensure to save all settings when closing the application.
        /// </summary>
        /// <param name="sender">Reference to the <see cref="Form"/> object.</param>
        /// <param name="e"><see cref="FormClosingEventArgs"/> arguments.</param>
        private void MainWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            RegistryManager.SaveAllSettings(customPath.Text, showCryTif.Checked);
        }

        /// <summary>
        /// Set values of custom slots.
        /// </summary>
        private void SetCustomSlots()
        {
            Framework.Log("Set custom links");
            this.ResetCustomSlots();

            for (int i = 0; i < this.customSlotData.Length; i++)
            {
                var values = CustomSlot.GetDataFromId(this.customSlotData, i);
                if (values != null)
                {
                    Button currentButton = CustomSlot.GetButtonFromId(this, i);
                    this.SetCustomSlot(currentButton, true, values[0]);
                }
            }
        }

        /// <summary>
        /// Reset custom slots with default text and disabled state.
        /// </summary>
        private void ResetCustomSlots()
        {
            string defaultText = "Custom link";

            this.SetCustomSlot(this.customSlotButton1, false, defaultText);
            this.SetCustomSlot(this.customSlotButton2, false, defaultText);
            this.SetCustomSlot(this.customSlotButton3, false, defaultText);
            this.SetCustomSlot(this.customSlotButton4, false, defaultText);
            this.SetCustomSlot(this.customSlotButton5, false, defaultText);
            this.SetCustomSlot(this.customSlotButton6, false, defaultText);
        }

        /// <summary>
        /// Set the value and enabled state of a specific button.
        /// </summary>
        /// <param name="button">Reference to a button, for custom slots.</param>
        /// <param name="enabled"><c>True</c> if button is enabled; otherwise <c>false</c></param>
        /// <param name="text">Text to display.</param>
        private void SetCustomSlot(Button button, bool enabled, string text)
        {
            button.Text = text;
            button.Enabled = enabled;
        }

        /// <summary>
        /// Single event handler for all custom slot buttons.
        /// </summary>
        /// <param name="sender">Reference to a <see cref="Button"/>.</param>
        /// <param name="e"><see cref="EventArgs"/> for Click event.</param>
        private void CustomSlotButton_Click(object sender, EventArgs e)
        {
            var index = CustomSlot.GetIdFromButton(this, sender as Button);
            CustomSlot.ExeSlotAction(index, this.customSlotData);
        }

        private void ManageButton_Click(object sender, EventArgs e)
        {
            LinksManagementWindow managementWindow = new LinksManagementWindow(this.customSlotData);
            this.Dock = DockStyle.Fill;
            if (!Application.OpenForms.OfType<LinksManagementWindow>().Any())
            {
                managementWindow.Show();
                this.isLinksManagementPreviouslyOpen = true;
            }
        }

        /// <summary>
        /// Event handler for clicking <see cref="linkLabel1"/>.
        /// </summary>
        /// <param name="sender">Reference to the object.</param>
        /// <param name="e">Event arguments.</param>
        private void LinkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("http:// www.guillaume-puyal.com/");
        }

        // Quixel Suite live
        private void QuixelSuiteLive_Click(object sender, EventArgs e)
        {
            if (this.quixelSuiteLive.ForeColor != System.Drawing.Color.Red)
            {
                this.Dock = DockStyle.Fill;
                this.quixelSuiteDialog = new QuixelSuiteSetupDialog(this.quixelSuiteLive);
                if (!Application.OpenForms.OfType<QuixelSuiteSetupDialog>().Any())
                {
                    this.quixelSuiteDialog.Show();
                }
            }
            else
            {
                this.quixelSuiteDialog.Stop();
                this.quixelSuiteDialog.Dispose();

                this.quixelSuiteLive.Text = "Quixel Suite DDO";
                this.quixelSuiteLive.Width = 113;
                this.quixelSuiteLive.ForeColor = System.Drawing.Color.White;
            }
        }

        /// <summary>
        /// Get the version from the assembly.
        /// </summary>
        /// <returns><see cref="Version"/>.</returns>
        private Version GetAssemblyVersion()
        {
            return typeof(MainWindow).Assembly.GetName().Version;
        }
    }
}