//-----------------------------------------------------------------------
// <copyright file="CustomPath.cs" company="Guillaume Puyal">
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
    using System.IO;

    internal class CustomPath
    {
        private string inputPath;
        private string customTempFile;

        public CustomPath(string customPath)
        {
            this.inputPath = customPath;
        }

        public string ProcessFinalPath(string trueFilePath)
        {
            if (this.inputPath == null)
            {
                return null;
            }

            this.customTempFile = this.inputPath + @"\" + Path.GetFileName(trueFilePath);
            if (Directory.Exists(this.inputPath))
            {
                File.Copy(trueFilePath, this.customTempFile);
                Framework.Log("(Custom path) file copied to: " + this.customTempFile);
                this.inputPath = trueFilePath;
                return this.customTempFile;
            }
            else
            {
                Framework.ShowError("Unable to access to the specified path", true);
                return null;
            }
        }

        public void DeleteTempFile()
        {
            if (this.customTempFile != null && File.Exists(this.customTempFile) && Path.GetExtension(inputPath) != ".tif")
            {
                File.Delete(this.customTempFile);
                Framework.Log("(Custom path) deleted temporary file: " + this.customTempFile);
                this.customTempFile = null;
            }
        }
    }
}