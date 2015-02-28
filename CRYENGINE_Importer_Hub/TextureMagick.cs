//-----------------------------------------------------------------------
// <copyright file="TextureMagick.cs" company="Guillaume Puyal">
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
    using System.IO;
    using ImageMagick;

    internal class TextureMagick
    {
        private string filePath;
        private string fileName;
        private string tempFilePath;

        public TextureMagick(string filePath)
        {
            this.filePath = filePath;
            this.fileName = Path.GetFileName(filePath);

            if (!File.Exists("Magick.NET-x64.dll"))
            {
                Framework.ShowError("Unable to find Magick.NET-x64.dll", true);
            }

            Framework.Log("ImageMagick library loaded, beginning conversion");

            if (MagickConvert())
            {
                new TextureTiffConvert(tempFilePath);
                DeleteTempFile();
            }
        }

        private bool MagickConvert()
        {
            try
            {
                using (MagickImage image = new MagickImage(filePath))
                {
                    // Use PNG if the image have an alpha channel
                    if (image.HasAlpha)
                    {
                        tempFilePath = Path.GetDirectoryName(filePath) + @"\" + Path.GetFileNameWithoutExtension(filePath) + ".png";
                        Framework.Log("Alpha channel found, using PNG");
                    }
                    else
                    {
                        tempFilePath = Path.GetDirectoryName(filePath) + @"\" + Path.GetFileNameWithoutExtension(filePath) + ".bmp";
                    }

                    image.Write(tempFilePath);
                    Framework.Log("(Magick) Temporary image saved: {0}", tempFilePath);
                }
                return true;
            }
            catch (InvalidCastException)
            {
                Framework.ShowError("Unable to save the temporary image at: {0}", false, tempFilePath);
                return false;
            }
        }

        private void DeleteTempFile()
        {
            if (File.Exists(tempFilePath))
            {
                File.Delete(tempFilePath);
                Framework.Log("(Magick) Temporary image deleted: {0}", tempFilePath);
            }
        }
    }
}