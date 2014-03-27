using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace CRYENGINE_ImportHub
{
    class CCustomPath
    {
        private string m_inputPath;
        private string m_customTempFile;

        public CCustomPath(string customPath)
        {
            m_inputPath = customPath;
        }

        public string ProcessFinalPath(string trueFilePath)
        {
            if (m_inputPath != null)
            {
                m_customTempFile = m_inputPath + @"\" + Path.GetFileName(trueFilePath);

                if (Directory.Exists(m_inputPath))
                {
                    File.Copy(trueFilePath, m_customTempFile);
                    Framework.Log("(Custom path) file copied to: " + m_customTempFile);

                    m_inputPath = trueFilePath;

                    return m_customTempFile;
                }
                else
                {
                    Framework.ShowError("Unable to access to the specified path", true);
                    return null;
                }
            }
            else
            {
                return null;
            }
        }

        public void DeleteTempFile()
        {
            if (m_customTempFile != null && File.Exists(m_customTempFile) && Path.GetExtension(m_inputPath) != ".tif")
            {
                File.Delete(m_customTempFile);
                Framework.Log("(Custom path) deleted temporary file: " + m_customTempFile);
                m_customTempFile = null;
            }
        }
    }
}
