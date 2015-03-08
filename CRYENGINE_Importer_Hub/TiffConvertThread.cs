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

    /// <summary>
    /// Tiff conversion thread functionality wrapped in a class.
    /// </summary>
    internal class TiffConvertThread
    {
        /// <summary>
        /// Path to the file to convert.
        /// </summary>
        private string filePath;

        /// <summary>
        /// Path to store the converted filed.
        /// </summary>
        private string convertedFilePath;

        /// <summary>
        /// Initializes a new instance of the <see cref="TiffConvertThread"/> class.
        /// </summary>
        /// <param name="filePath">Path to the file to convert.</param>
        public TiffConvertThread(string filePath)
        {
            this.filePath = filePath;
            this.convertedFilePath = Path.GetDirectoryName(filePath) + @"\" + Path.GetFileNameWithoutExtension(filePath) + ".tif";
        }

        /// <summary>
        /// Convert function.
        /// </summary>
        /// <remarks>Should be executed on it's own thread.</remarks>
        public void Convert()
        {
            try
            {
                var bitmapDecoder = BitmapDecoder.Create(new Uri(this.filePath), BitmapCreateOptions.IgnoreColorProfile, BitmapCacheOption.OnLoad);
                var sourceFrame = bitmapDecoder.Frames[0];

                if (Path.GetExtension(this.convertedFilePath).Equals(".tif", StringComparison.CurrentCultureIgnoreCase))
                {
                    using (var stream = new FileStream(this.convertedFilePath, FileMode.Create, FileAccess.Write, FileShare.Delete))
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
                Framework.ShowError("Unable to save the temporary file at {0}. {1}", true, this.convertedFilePath, ex.Message);
            }
        }
    }
}