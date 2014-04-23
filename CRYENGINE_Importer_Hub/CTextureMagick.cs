using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using ImageMagick;

namespace CRYENGINE_ImportHub
{
    class CTextureMagick
    {
        private string m_filePath;
        private string m_fileName;
        private string m_tempFilePath;

        public CTextureMagick(string filePath)
        {
            m_filePath = filePath;
            m_fileName = Path.GetFileName(filePath);

            if (!File.Exists("Magick.NET-x64.dll"))
            {
                Framework.ShowError("Unable to find Magick.NET-x64.dll", true);
            }

            Framework.Log("ImageMagick library loaded, beginning conversion");

            if (MagickConvert())
            {
                new CTextureTiffConvert(m_tempFilePath);
                DeleteTempFile();
            }
        }

        private bool MagickConvert()
        {
            try
            {
                using (MagickImage image = new MagickImage(m_filePath))
                {
                    //Use PNG if the image have an alpha channel
                    if (image.HasAlpha)
                    {
                        m_tempFilePath = Path.GetDirectoryName(m_filePath) + @"\" + Path.GetFileNameWithoutExtension(m_filePath) + ".png";
                        Framework.Log("Alpha channel found, using PNG");
                    }
                    else
                    {
                        m_tempFilePath = Path.GetDirectoryName(m_filePath) + @"\" + Path.GetFileNameWithoutExtension(m_filePath) + ".bmp";
                    }

                    image.Write(m_tempFilePath);

                    Framework.Log("(Magick) Temporary image saved: " + m_tempFilePath);
                }
                return true;
            }
            catch (InvalidCastException e)
            {
                Framework.ShowError("Unable to save the temporary image at: " + m_tempFilePath);
                return false;
            }
        }

        private void DeleteTempFile()
        {
            if (File.Exists(m_tempFilePath))
            {
                File.Delete(m_tempFilePath);
                Framework.Log("(Magick) Temporary image deleted: " + m_tempFilePath);
            }
        }
    }
}
