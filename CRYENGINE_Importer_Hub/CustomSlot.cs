//-----------------------------------------------------------------------
// <copyright file="CustomSlot.cs" company="Guillaume Puyal">
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
    using System.Diagnostics;
    using System.IO;
    using System.Windows.Forms;

    public static class CustomSlot
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

        public static string[] GetDataFromId(string[] registryData, int id, bool replaceTags = true)
        {
            var noFormat = registryData[id];
            if (string.IsNullOrEmpty(noFormat))
            {
                return null;
            }

            var values = noFormat.Split('~');

            var title = values[0];
            var path = values[1];
            var args = values[2];

            if (replaceTags)
            {
                path = path.Replace("[SDK_FOLDER]", Framework.GetCRYENGINELocation());
            }

            return new string[] { title, path, args };
        }

        public static void ExeSlotAction(int id, string[] registryData)
        {
            var itemValues = GetDataFromId(registryData, id);
            var title = itemValues[0];
            var path = itemValues[1];
            var args = itemValues[2];

            if (File.Exists(path) || Directory.Exists(path) || path.StartsWith("http"))
            {
                Framework.Log("Valid file, directory or URL, beginning process launch");
                var process = Process.Start(path, args);
                Framework.Log("Process {0} launched{1}", path, string.IsNullOrEmpty(args) ? string.Empty : "with " + args);
            }
            else
            {
                Framework.ShowError("The specified file or directory " + path + " don't exist");
            }
        }
    }
}