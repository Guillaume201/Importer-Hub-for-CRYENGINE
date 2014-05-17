using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using System.Threading;

namespace CRYENGINE_ImportHub
{
    class TiffConvertThread
    {
        private string m_filePath;
        private string m_convertedFilePath;

        public TiffConvertThread(string filePath)
        {
            m_filePath = filePath;
            m_convertedFilePath = Path.GetDirectoryName(m_filePath) + @"\" + Path.GetFileNameWithoutExtension(m_filePath) + ".tif";

        }

        public void Convert()
        {
           try
            {
                BitmapDecoder bmpDec = BitmapDecoder.Create(new Uri(m_filePath), BitmapCreateOptions.IgnoreColorProfile, BitmapCacheOption.OnLoad);
                BitmapSource srs = bmpDec.Frames[0];

                if (Path.GetExtension(m_convertedFilePath).Equals(".tif", StringComparison.CurrentCultureIgnoreCase))
                {
                    using (FileStream stream = new FileStream(m_convertedFilePath, FileMode.Create, FileAccess.Write, FileShare.Delete))
                    {
                        TiffBitmapEncoder encoder = new TiffBitmapEncoder();
                        encoder.Compression = TiffCompressOption.None;
                        encoder.Frames.Add(BitmapFrame.Create(srs));
                        encoder.Save(stream);
                    }
                }

            }
            catch (Exception ex)
            {
                Framework.ShowError("Unable to save the temporary file at: " + m_convertedFilePath, true);
            }
        }
    }


    class CTextureTiffConvert
    {
        private string m_filePath;
        private string m_fileName;
        private string m_convertedFilePath;

        public CTextureTiffConvert(string filePath)
        {
            m_filePath = filePath;
            m_fileName = Path.GetFileName(filePath);
            m_convertedFilePath = Path.GetDirectoryName(m_filePath) + @"\" + Path.GetFileNameWithoutExtension(m_filePath) + ".tif";

            if (Path.GetExtension(filePath) != ".tif")
            {
                Framework.Log(m_fileName + ": Valid file, beginning conversion");

                TiffConvertThread threadHandle = new TiffConvertThread(filePath);

                Thread tiffConvertThread = new Thread(new ThreadStart(threadHandle.Convert));
                tiffConvertThread.Start();

                tiffConvertThread.Join();
                tiffConvertThread.Abort();

                CallRC();
            }
            else
            {
                //Tiff files don't need to be converted
                Framework.Log(m_fileName + ": Tiff file, unnecessary conversion: skip phase");

                CallRC();
            }

        }

        private void CallRC()
        {
            string userdialogCmd;

            if (Framework.IsCryTifDialogRequested())
                userdialogCmd = " /userdialog=1";
            else
                userdialogCmd = null;

            Framework.CRYENGINE_RC_Call(m_convertedFilePath + " /refresh" + userdialogCmd, "File " + m_fileName + " succefully send to the Ressource Compiler at " + m_convertedFilePath);
        }
    }
}
