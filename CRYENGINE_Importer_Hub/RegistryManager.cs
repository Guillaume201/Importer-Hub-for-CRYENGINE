//-----------------------------------------------------------------------
// <copyright file="RegistryManager.cs" company="Guillaume Puyal">
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
    using Microsoft.Win32;

    internal class RegistryManager
    {
        private const string ShortKeyPath = @"Software\CRYENGINE_ImportHub\Settings";
        private const string FullKeyPath = "HKEY_CURRENT_USER\\" + ShortKeyPath;

        public string CustomPathInput { get; set; }

        public bool UseCryTifDialog { get; set; }

        public static string[] CustomSlots
        {
            get;
            private set;
        }

        public RegistryManager()
        {
            RegistryManager.CustomSlots = new string[7];
#if DEBUG
            Framework.Log("Init registry manager");
#endif

            //Test if the settings are stored in the registry
            RegistryKey testKey = Registry.CurrentUser.OpenSubKey(ShortKeyPath);
            if (testKey != null)
            {
                UpdateKeys();
                GetAllValues();
            }
            else
            {
                CreateKeys();
                GetAllValues();
            }
        }

        private void GetAllValues()
        {
#if DEBUG
            Framework.Log("Get settings values from registry");
#endif

            CustomPathInput = (string)Registry.GetValue(FullKeyPath, "CustomPathInput", null);

            var tempCryTifVar = (string)Registry.GetValue(FullKeyPath, "UseCryTifDialog", null);
            if (tempCryTifVar == "True")
            {
                UseCryTifDialog = true;
            }
            else
            {
                UseCryTifDialog = false;
            }

            for (int i = 1; i <= 6; i++)
            {
                CustomSlots[i] = (string)Registry.GetValue(FullKeyPath, "CustomSlot" + i, null);
            }
        }

        private void UpdateKeys()
        {
            var update = false;

            //v0.3 to 0.4
            if ((string)Registry.GetValue(FullKeyPath, "UseCryTifDialog", null) == null)
            {
                Registry.SetValue(FullKeyPath, "UseCryTifDialog", "True");
                update = true;
            }

            if (update)
                Framework.Log("Registry keys updated");
        }

        private void CreateKeys()
        {
            Framework.Log("Registry keys not found: beginning creation");

            Registry.SetValue(FullKeyPath, "CustomPathInput", "");
            Registry.SetValue(FullKeyPath, "UseCryTifDialog", "True");

            Registry.SetValue(FullKeyPath, "CustomSlot1", "SDK Folder~[SDK_FOLDER]~");
            Registry.SetValue(FullKeyPath, "CustomSlot2", @"Sandbox Editor (x64)~[SDK_FOLDER]bin64\Editor.exe~");
            Registry.SetValue(FullKeyPath, "CustomSlot3", "CRYENGINE Documentation~http://docs.cryengine.com/~");
            Registry.SetValue(FullKeyPath, "CustomSlot4", "");
            Registry.SetValue(FullKeyPath, "CustomSlot5", "");
            Registry.SetValue(FullKeyPath, "CustomSlot6", "");

            Framework.Log("Registry keys created succefully");
        }

        public static void SaveAllSettings(string customPathInput, bool isUseCryTifDialog)
        {
            Framework.Log("Save settings on the registry");

            Registry.SetValue(FullKeyPath, "CustomPathInput", customPathInput);
            Registry.SetValue(FullKeyPath, "UseCryTifDialog", isUseCryTifDialog);

            Framework.Log("Settings saved");
        }

        public static void SaveCustomSlot(int id, string title, string path, string args)
        {
            if (title != null && title != "")
                Registry.SetValue(FullKeyPath, "CustomSlot" + id, title + "~" + path + "~" + args);
            else
                Registry.SetValue(FullKeyPath, "CustomSlot" + id, "");

            Framework.Log("Custom link " + title + " (" + id.ToString() + ") saved");
        }

        public static string[] GetCustomSlots()
        {
            return CustomSlots;
        }
    }
}