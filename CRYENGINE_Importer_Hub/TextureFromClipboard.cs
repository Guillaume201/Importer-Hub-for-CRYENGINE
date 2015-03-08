//-----------------------------------------------------------------------
// <copyright file="TextureFromClipboard.cs" company="Guillaume Puyal">
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
    using System.Drawing;
    using System.IO;

    /// <summary>
    /// Internal class to retrieve a texture off the Windows Clipboard.
    /// </summary>
    internal class TextureFromClipboard
    {
        /// <summary>
        /// Stores the path of the temporary file.
        /// </summary>
        private string tempFilePath;

        /// <summary>
        /// Initializes a new instance of the <see cref="TextureFromClipboard"/> class.
        /// </summary>
        /// <param name="outputPath">Path to store the image.</param>
        /// <param name="clipboardData">A reference to an <see cref="Image"/>.</param>
        public TextureFromClipboard(string outputPath, Image clipboardData)
        {
            // Allow filepaths with and without extensions
            if (string.IsNullOrEmpty(Path.GetExtension(outputPath)))
            {
                this.tempFilePath = outputPath + ".bmp";
            }
            else
            {
                this.tempFilePath = Path.GetDirectoryName(outputPath) + @"\" + Path.GetFileNameWithoutExtension(outputPath) + ".bmp";
            }

            if (this.SaveTempBitmap(clipboardData))
            {
                new TextureTiffConvert(this.tempFilePath);
                this.DeleteTempFile();
            }
            else
            {
                Framework.ShowError("Unable to save the temporary file due to clipboard conversion");
            }
        }

        /// <summary>
        /// Save the <paramref name="image"/> to a file.
        /// </summary>
        /// <param name="image">A reference to an image.</param>
        /// <returns><c>true</c> if succeeded; otherwise <c>false</c></returns>
        private bool SaveTempBitmap(Image image)
        {
            try
            {
                image.Save(this.tempFilePath, System.Drawing.Imaging.ImageFormat.Bmp);
                Framework.Log("(Clipbord) Temporary image saved: {0}", this.tempFilePath);
                return true;
            }
            catch (InvalidCastException)
            {
                return false;
            }
        }

        /// <summary>
        /// Deletes the temporary file created in the save process.
        /// </summary>
        private void DeleteTempFile()
        {
            if (File.Exists(this.tempFilePath))
            {
                File.Delete(this.tempFilePath);
                Framework.Log("(Clipbord) Temporary image deleted: {0}", this.tempFilePath);
            }
        }
    }
}