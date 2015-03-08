//-----------------------------------------------------------------------
// <copyright file="LinksManagementWindow.cs" company="Guillaume Puyal">
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
    using System.Windows.Forms;

    public partial class LinksManagementWindow : Form
    {
        private string[] customSlotData;
        private int currentId;
        private RegistryManager registryManager;

        public LinksManagementWindow(string[] customSlotData)
        {
            InitializeComponent();

#if DEBUG
            Framework.Log("Links manager openned");
#endif

            RefreshListBox();
            linksListBox.SelectedIndex = 0;
        }

        private void RefreshListBox()
        {
            // m_customSlotData = customSlotData;
            this.customSlotData = RegistryManager.CustomSlots;

            // Add slots values to the ListBox
            for (int i = 0; i < customSlotData.Length; i++)
            {
                if (i != 0)
                {
                    string[] values = CustomSlot.GetDataFromId(customSlotData, i);
                    if (values != null)
                    {
                        linksListBox.Items.Add(values[0]);
                    }
                    else
                    {
                        linksListBox.Items.Add("Custom link " + i);
                    }
                }
            }
        }


        private void linksListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.currentId = linksListBox.SelectedIndex + 1;

            this.registryManager = new RegistryManager();
            this.customSlotData = RegistryManager.GetCustomSlots();

            var linkValues = CustomSlot.GetDataFromId(customSlotData, currentId, false);
            if (linkValues == null)
            {
                this.LinkPath.Text = string.Empty;
                this.LinkArgs.Text = string.Empty;
                this.LinkTitle.Text = string.Empty;
            }
            else
            {
                this.LinkTitle.Text = linkValues[0];
                this.LinkPath.Text = linkValues[1];
                this.LinkArgs.Text = linkValues[2];
            }

#if DEBUG
            Framework.Log("Listbox refreshed");
#endif
        }

        private void CloseButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void LinkTitle_Leave(object sender, EventArgs e)
        {
            RegistryManager.SaveCustomSlot(this.currentId, this.LinkTitle.Text, this.LinkPath.Text, this.LinkArgs.Text);

            if (linksListBox.SelectedIndex != -1)
            {
                linksListBox.Items[linksListBox.SelectedIndex] = string.IsNullOrEmpty(this.LinkTitle.Text) ? "Custom link " + currentId.ToString() : this.LinkTitle.Text;
            }
        }

        private void LinkPath_Leave(object sender, EventArgs e)
        {
            RegistryManager.SaveCustomSlot(currentId, LinkTitle.Text, LinkPath.Text, LinkArgs.Text);
        }

        private void LinkArgs_Leave(object sender, EventArgs e)
        {
            RegistryManager.SaveCustomSlot(currentId, LinkTitle.Text, LinkPath.Text, LinkArgs.Text);
        }
    }
}
