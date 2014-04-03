using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace CRYENGINE_ImportHub
{
    class CTextureFromClipboard
    {
        private string m_tempFilePath;

        public CTextureFromClipboard(string dialogPath, string customPath, Image clipboardData)
        {
            //Allow filepaths with and without extensions
            if (Path.GetExtension(dialogPath) != "")
            {
                m_tempFilePath = Path.GetDirectoryName(dialogPath) + @"\" + Path.GetFileNameWithoutExtension(dialogPath) + ".bmp";
            }
            else
            {
                m_tempFilePath = dialogPath + ".bmp";
            }


            if (SaveTempBitmap(clipboardData))
            {
                new CTextureTiffConvert(m_tempFilePath);
                deleteTempFile();
            }
            else
            {
                Framework.ShowError("Unable to save the temporary file due to clipboard conversion");
            }
        }

        private bool SaveTempBitmap(Image image)
        {
            try
            {
                image.Save(m_tempFilePath, System.Drawing.Imaging.ImageFormat.Bmp);

                Framework.Log("(Clipbord) Temporary image saved: " + m_tempFilePath);
                return true;
            }
            catch (InvalidCastException e)
            {
                return false;
            }
        }

        private void deleteTempFile()
        {
            if (File.Exists(m_tempFilePath))
            {
                File.Delete(m_tempFilePath);
                Framework.Log("(Clipbord) Temporary image deleted: " + m_tempFilePath);
            }
        }
    }
}
