//-----------------------------------------------------------------------
// <copyright file="Framework.cs" company="Guillaume Puyal">
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
    using System.IO;
    using System.Runtime.InteropServices;
    using System.Windows.Forms;
    using Microsoft.Win32;

    internal class Framework
    {
        public static string cryengineLocation;
        public static RichTextBox consoleControler;
        private static CheckBox cryTifCheckbox;
        public static bool LockVisualConsole = false;

        // WindowFocus Specific
        [DllImport("user32.dll")]
        public static extern bool ShowWindowAsync(HandleRef hWnd, int nCmdShow);

        [DllImport("user32.dll")]
        public static extern bool SetForegroundWindow(IntPtr WindowHandle);

        public const int SW_RESTORE = 3;    // MAXIMISE = 3, RESTORE = 9

        public Framework(RichTextBox console, CheckBox cryTifCheckbox)
        {
            Framework.consoleControler = console;
            Framework.cryTifCheckbox = cryTifCheckbox;
            Framework.Log("Init Framework");
        }

        public static void BeginImport(string filePath, string customPath)
        {
            // Custom path support
            var customPathObj = new CustomPath(customPath);

            if (!string.IsNullOrEmpty(customPath))
            {
                var tPath = customPathObj.ProcessFinalPath(filePath);
                if (!string.IsNullOrEmpty(tPath))
                {
                    filePath = tPath;
                }
            }

            switch (GetConversionType(filePath))
            {
                case "texture":
                    new TextureTiffConvert(filePath);
                    break;

                case "texture_magick":
                    new TextureMagick(filePath);
                    break;

                case "mesh":
                    Framework.CRYENGINE_RC_Call(filePath + " /refresh");

                    string newFilePath = Path.GetDirectoryName(filePath) + @"\" + Path.GetFileNameWithoutExtension(filePath) + ".cgf";
                    if (File.Exists(newFilePath))
                    {
                        Framework.Log("File " + Path.GetFileName(filePath) + " succefully send to the Ressource Compiler at " + newFilePath);
                    }
                    else
                    {
                        Framework.ShowError("The Resource Compiler was unable to process the file. Check rc_log.log for more details.");
                    }

                    break;
            }

            // If custom path call delete temp file
            if (customPath != null)
            {
                customPathObj.DeleteTempFile();
            }
        }

        public static string GetConversionType(string filePath)
        {
            string fileExtension = Path.GetExtension(filePath);

            switch (fileExtension)
            {
                // Supported textures extensions
                case ".png":
                    return "texture";

                case ".jpg":
                    return "texture";

                case ".tif":
                    return "texture";

                case ".bmp":
                    return "texture";

                case ".PNG":
                    return "texture";

                case ".JPG":
                    return "texture";

                case ".TIF":
                    return "texture";

                case ".BMP":
                    return "texture";

                // Supported textures via Magick
                case ".tga":
                    return "texture_magick";

                case ".TGA":
                    return "texture_magick";

                case ".psd":
                    return "texture_magick";

                case ".PSD":
                    return "texture_magick";

                // Supported meshes extensions
                case ".fbx":
                    return "mesh";

                case ".dae":
                    return "mesh";

                case ".FBX":
                    return "mesh";

                case ".DAE":
                    return "mesh";

                default:
                    ShowError("Unsuported file type: {0}", false, fileExtension);
                    return null;
            }
        }

        public static string GetCRYENGINELocation()
        {
            if (Framework.cryengineLocation == null)
            {
                string value = (string)Registry.GetValue(@"HKEY_CURRENT_USER\Software\Crytek\Settings", "ENG_RootPath", null);
                if (value != null)
                {
                    if (!Directory.Exists(value))
                    {
                        Framework.ShowError("The CRYENGINE path from the registry not exist\nBe sure to have set your CRYENGINE build path in the Settings Manager under <CESDK>/Tools/SettingsMgr.exe", true);
                        return null;
                    }
                    else
                    {
                        Framework.cryengineLocation = value + @"\";

                        Framework.Log("CRYENGINE path find at: " + value);

                        // Show a warning in the console if spaces detected in the path
                        if (value.Contains(" "))
                            Framework.Log("WARNING: Spaces detected in our CRYENGINE build path. This may cause some issues with CryTif.");

                        return value + @"\";
                    }
                }
                else
                {
                    ShowError("Unable to find the CRYENGINE path from the registry.\nBe sure to have set your CRYENGINE build path in the Settings Manager under <CESDK>/Tools/SettingsMgr.exe", true);
                    return null;
                }
            }
            else
            {
                return Framework.cryengineLocation;
            }
        }

        public static void CRYENGINE_RC_Call(string commands, string logWhenFinished = null)
        {
            var rcPathBin32 = GetCRYENGINELocation() + @"Bin32\RC\rc.exe";
            var rcPathBin64 = GetCRYENGINELocation() + @"Bin64\RC\rc.exe";
            var rcPath = File.Exists(rcPathBin64) ? rcPathBin64 : rcPathBin32;

            if (File.Exists(rcPath))
            {
#if DEBUG
                // commands = commands + " /wait";
#endif

                Framework.Log("Beginning RC call: " + commands);

                var process = new Process
                {
                    StartInfo = new ProcessStartInfo
                    {
                        FileName = rcPath,
                        Arguments = commands,
                        // RedirectStandardOutput = true,
                        // #if !DEBUG
                        CreateNoWindow = true,
                        UseShellExecute = false,
                        WindowStyle = ProcessWindowStyle.Hidden,
                        // #endif
                    }
                };

                process.Start();
                Framework.Log("RC call send");

                process.WaitForExit();
                Framework.Log("RC terminated");

                if (logWhenFinished != null)
                {
                    Framework.Log(logWhenFinished);
                }
            }
            else
            {
                Framework.ShowError("Unable to find RC.exe", true);
            }
        }

        public static bool IsCryTifDialogRequested()
        {
            return cryTifCheckbox.Checked;
        }

        public static bool IsFileLocked(string filePath)
        {
            try
            {
                using (File.Open(filePath, FileMode.Open)) { }
            }
            catch (IOException e)
            {
                var errorCode = Marshal.GetHRForException(e) & ((1 << 16) - 1);
                return errorCode == 32 || errorCode == 33;
            }

            return false;
        }

        public static void FocusProcess(string procName)
        {
            var objProcesses = System.Diagnostics.Process.GetProcessesByName(procName);
            if (objProcesses.Length > 0)
            {
                var hWnd = IntPtr.Zero;
                hWnd = objProcesses[0].MainWindowHandle;
                ShowWindowAsync(new HandleRef(null, hWnd), SW_RESTORE);
                SetForegroundWindow(objProcesses[0].MainWindowHandle);
            }
        }

        public static void ShowError(string error, bool isFatal = false)
        {
            Framework.Log("ERROR: " + error);

            if (!isFatal)
            {
                MessageBox.Show(error, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                MessageBox.Show(error, "Fatal Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Framework.Log("FATAL ERROR: Shutting down");
                Environment.Exit(1);
            }
        }

        /// <summary>
        /// Overload of ShowError including args for string.Format.
        /// </summary>
        /// <param name="error">Error message / string format.</param>
        /// <param name="isFatal">Fatal error so application must exit?</param>
        /// <param name="args">Args for string.Format of <paramref name="error"/>.</param>
        public static void ShowError(string error, bool isFatal = false, params object[] args)
        {
            var message = string.Format(error, args);
            Framework.ShowError(message, isFatal);
        }

        public static void Log(string msg)
        {
            Console.WriteLine(msg);

            if (!Framework.LockVisualConsole)
            {
                // Write in visual console
                Framework.consoleControler.Text = Framework.consoleControler.Text + "\n" + msg;
                Framework.consoleControler.SelectionStart = Framework.consoleControler.Text.Length;
                Framework.consoleControler.ScrollToCaret();
            }
        }

        public static void Log(string msg, params object[] args)
        {
            Framework.Log(string.Format(msg, args));
        }
    }
}