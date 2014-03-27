using Microsoft.Win32;
using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace CRYENGINE_ImportHub
{
    class Framework
    {
        public static string cryengineLocation;
        public static RichTextBox consoleControler;

        public Framework(RichTextBox console)
        {
            Framework.consoleControler = console;

            Framework.Log("Init Framework");
        }

        public static void BeginImport(string filePath, string customPath)
        {
            //Custom path support
            CCustomPath customPathObj = new CCustomPath(customPath);

            if (customPath != null && customPath != "")
            {
                string tPath = customPathObj.ProcessFinalPath(filePath);
                if (tPath != null)
                    filePath = tPath;
            }


            switch (GetConversionType(filePath))
            {
                case "texture":
                    new CTextureTiffConvert(filePath);
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

            //Custom path call delete temp file
            if (customPath != null)
                customPathObj.DeleteTempFile();
        }

        public static string GetConversionType(string filePath)
        {
            string fileExtension = Path.GetExtension(filePath);

            switch (fileExtension)
            {
                //Supported textures extensions
                case ".png":
                    return "texture";
                case ".jpg":
                    return "texture";
                /*case ".tga":
                    return "texture";*/
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

                //Supported meshes extensions
                case ".fbx":
                    return "mesh";
                case ".dae":
                    return "mesh";

                case ".FBX":
                    return "mesh";
                case ".DAE":
                    return "mesh";

                default:
                    ShowError("Unsuported file type: " + fileExtension);
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
                        Framework.ShowError("The CRYENGINE path from the registry not exist (try to use the Settings Manager)", true);
                        return null;
                    }
                    else
                    {
                        Framework.cryengineLocation = value + @"\";

                        Framework.Log("CRYENGINE path find at: " + value);
                        return value + @"\";
                    }
                }
                else
                {
                    ShowError("Unable to find the CRYENGINE path from the registry (try to use the Settings Manager)", true);
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
            string rcPath = GetCRYENGINELocation() + @"Bin32\RC\rc.exe";

            if (File.Exists(rcPath))
            {
                #if DEBUG
                //commands = commands + " /wait";
                #endif

                Framework.Log("Beginning RC call: " + commands);

                var process = Process.Start(rcPath, commands);
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

        public static void Log(string msg)
        {
            Console.WriteLine(msg);

            //Write in visual console
            Framework.consoleControler.Text = Framework.consoleControler.Text + "\n" + msg;
            Framework.consoleControler.SelectionStart = Framework.consoleControler.Text.Length;
            Framework.consoleControler.ScrollToCaret();
        }
    }
}
