using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CRYENGINE_ImportHub
{
    public partial class LinksManagementWindow : Form
    {
        private string[] m_customSlotData;
        private int m_currentId;
        private CRegistryManager m_registryManager;

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
            //m_customSlotData = customSlotData;
            m_customSlotData = CRegistryManager.GetCustomSlots();

            //Add slots values to the ListBox
            int i = 0;
            foreach (string noFormat in m_customSlotData)
            {
                if (i != 0)
                {
                    string[] values = CCustomSlot.GetDataFromId(m_customSlotData, i);
                    if (values != null)
                    {
                        linksListBox.Items.Add(values[0]);
                    }
                    else
                    {
                        linksListBox.Items.Add("Custom link " + i);
                    }
                }
                i++;
            }
        }


        private void linksListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            m_currentId = linksListBox.SelectedIndex + 1;

            m_registryManager = null;
            m_registryManager = new CRegistryManager();
            m_customSlotData = CRegistryManager.GetCustomSlots();

            string[] linkValues = CCustomSlot.GetDataFromId(m_customSlotData, m_currentId, false);

            if (linkValues != null)
            {
                LinkTitle.Text = linkValues[0];
                LinkPath.Text = linkValues[1];
                LinkArgs.Text = linkValues[2];
            }
            else
            {
                LinkTitle.Text = "";
                LinkPath.Text = "";
                LinkArgs.Text = "";
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
            CRegistryManager.SaveCustomSlot(m_currentId, LinkTitle.Text, LinkPath.Text, LinkArgs.Text);

            if (linksListBox.SelectedIndex != -1)
            {
                if (LinkTitle.Text != null && LinkTitle.Text != "")
                    linksListBox.Items[linksListBox.SelectedIndex] = LinkTitle.Text;
                else
                    linksListBox.Items[linksListBox.SelectedIndex] = "Custom link " + m_currentId.ToString();
            }
        }

        private void LinkPath_Leave(object sender, EventArgs e)
        {
            CRegistryManager.SaveCustomSlot(m_currentId, LinkTitle.Text, LinkPath.Text, LinkArgs.Text);
        }

        private void LinkArgs_Leave(object sender, EventArgs e)
        {
            CRegistryManager.SaveCustomSlot(m_currentId, LinkTitle.Text, LinkPath.Text, LinkArgs.Text);
        }
    }
}
