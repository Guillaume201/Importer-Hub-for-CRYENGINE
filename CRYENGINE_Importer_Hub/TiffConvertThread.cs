//-----------------------------------------------------------------------
// <copyright file="TiffConvertThread.cs" company="Guillaume Puyal">
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
    using System;
    using System.IO;
    using System.Windows.Media.Imaging;

    internal class TiffConvertThread
    {
        private string filePath;
        private string convertedFilePath;

        public TiffConvertThread(string filePath)
        {
            this.filePath = filePath;
            convertedFilePath = Path.GetDirectoryName(filePath) + @"\" + Path.GetFileNameWithoutExtension(filePath) + ".tif";
        }

        public void Convert()
        {
            try
            {
                var bitmapDecoder = BitmapDecoder.Create(new Uri(filePath), BitmapCreateOptions.IgnoreColorProfile, BitmapCacheOption.OnLoad);
                var sourceFrame = bitmapDecoder.Frames[0];

                if (Path.GetExtension(convertedFilePath).Equals(".tif", StringComparison.CurrentCultureIgnoreCase))
                {
                    using (var stream = new FileStream(convertedFilePath, FileMode.Create, FileAccess.Write, FileShare.Delete))
                    {
                        var encoder = new TiffBitmapEncoder();
                        encoder.Compression = TiffCompressOption.None;
                        encoder.Frames.Add(BitmapFrame.Create(sourceFrame));
                        encoder.Save(stream);
                    }
                }
            }
            catch (Exception ex)
            {
                Framework.ShowError("Unable to save the temporary file at {0}. {1}", true, convertedFilePath, ex.Message);
            }
        }
    }
}