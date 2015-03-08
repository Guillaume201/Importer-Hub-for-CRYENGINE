//-----------------------------------------------------------------------
// <copyright file="TextureTiffConvert.cs" company="Guillaume Puyal">
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
    using System.IO;
    using System.Threading;

    public class TextureTiffConvert
    {
        private string filePath;
        private string fileName;
        private string convertedFilePath;
        private bool isSilentMode;

        public TextureTiffConvert(string filePath, bool isSilentMode = false)
        {
            this.filePath = filePath;
            this.fileName = Path.GetFileName(filePath);
            this.convertedFilePath = Path.GetDirectoryName(filePath) + @"\" + Path.GetFileNameWithoutExtension(filePath) + ".tif";
            this.isSilentMode = isSilentMode;

            if (Path.GetExtension(filePath) != ".tif")
            {
                Framework.Log(fileName + ": Valid file, beginning conversion");

                var threadHandle = new TiffConvertThread(filePath);
                var tiffConvertThread = new Thread(new ThreadStart(threadHandle.Convert));

                tiffConvertThread.Start();
                tiffConvertThread.Join();
                tiffConvertThread.Abort();
                CallRC();
            }
            else
            {
                // Tiff files don't need to be converted
                Framework.Log(fileName + ": Tiff file, unnecessary conversion: skip phase");

                CallRC();
            }
        }

        private void CallRC()
        {
            // Fix CE 3.6 /refresh command - Launch BackgroundWorker for DeleteDDS
            var backgrundWorker = new System.ComponentModel.BackgroundWorker
            {
                WorkerSupportsCancellation = true,
                WorkerReportsProgress = true,
            };

            backgrundWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(DeleteDDS);
            backgrundWorker.RunWorkerAsync();
            Framework.CRYENGINE_RC_Call(convertedFilePath + " /refresh" + UserDialogCmd() + GetAdditionalCompressionPreset(), "File " + fileName + " succefully send to the Ressource Compiler at " + convertedFilePath);
        }

        private void DeleteDDS(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            var ddsFilePath = Path.GetDirectoryName(convertedFilePath) + @"\" + Path.GetFileNameWithoutExtension(convertedFilePath) + ".dds";

            if (File.Exists(ddsFilePath))
            {
                while (!Framework.IsFileLocked(ddsFilePath)) ;    // Wait until the source file is unlocked
                File.Delete(ddsFilePath);
                Framework.Log("DDS file found, deleting");
            }
        }

        private string UserDialogCmd()
        {
            if (Framework.IsCryTifDialogRequested() && !isSilentMode)
            {
                return " /userdialog=1";
            }
            else
            {
                return null;
            }
        }

        private string GetAdditionalCompressionPreset()
        {
            var fileName = Path.GetFileNameWithoutExtension(this.filePath);
            if (fileName.Length < 4)
            {
                return null;
            }

            var end = fileName.Substring(fileName.Length - 5);
            if (end == "_spec")
            {
                return " /preset=Specular_highQ";
            }
            else if (end == "_ddna")
            {
                return " /preset=NormalmapWithGlossInAlpha_highQ";
            }
            else if (end == "_diff")
            {
                return " /preset=Diffuse_highQ";
            }

            return null;
        }
    }
}