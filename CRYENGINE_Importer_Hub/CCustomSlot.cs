using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;

namespace CRYENGINE_ImportHub
{
    public static class CCustomSlot
    {
        public static Button GetButtonFromId(MainWindow mainWindow, int id)
        {
            switch (id)
            {
                case 1:
                    return mainWindow.customSlotButton1;
                case 2:
                    return mainWindow.customSlotButton2;
                case 3:
                    return mainWindow.customSlotButton3;
                case 4:
                    return mainWindow.customSlotButton4;
                case 5:
                    return mainWindow.customSlotButton5;
                case 6:
                    return mainWindow.customSlotButton6;
                default:
                    return null;
            }
        }

        public static string[] GetDataFromId(string[] registryData, int id, bool remplaceTags = true)
        {
            string noFormat = registryData[id];

            if (noFormat != null && noFormat != "")
            {
                string[] values = noFormat.Split('~');

                string title = values[0];
                string path = values[1];
                string args = values[2];

                if (remplaceTags)
                    path = path.Replace("[SDK_FOLDER]", Framework.GetCRYENGINELocation());

                string[] finalReturn = { title, path, args };
                return finalReturn;
            }
            else
            {
                return null;
            }
        }

        public static void ExeSlotAction(int id, string[] registryData)
        {
            string[] itemValues = GetDataFromId(registryData, id);
            string title = itemValues[0];
            string path = itemValues[1];
            string args = itemValues[2];

            if (File.Exists(path) || Directory.Exists(path) || path.StartsWith("http"))
            {
                Framework.Log("Valid file, directory or URL, beginning process launch");
                var process = Process.Start(path, args);

                if (args != null && args != "")
                    Framework.Log("Process " + path + " launched with " + args);
                else
                    Framework.Log("Process " + path + " launched");
            }
            else
            {
                Framework.ShowError("The specified file or directory " + path + " don't exist");
            }
        }
    }
}
