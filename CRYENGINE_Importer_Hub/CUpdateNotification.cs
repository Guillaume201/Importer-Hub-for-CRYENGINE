using System;
using System.Diagnostics;
using System.Net;
using System.IO;
using System.Windows.Forms;

namespace CRYENGINE_ImportHub
{
    class CUpdateNotification
    {
        const string UPDATE_CHECK_URL = "http://www.guillaume-puyal.com/systems/CEI_check_update.php";
        private string m_final_url;

        private string m_newUpdateVersion;
        private string m_newUpdateUrl;

        public CUpdateNotification(string version)
        {
            m_final_url = UPDATE_CHECK_URL + "?v=" + version;
            //TODO: verif

            Framework.Log("Beginning update check");
            GetRequest();
        }

        private void GetRequest()
        {
            WebRequest wrGETURL = WebRequest.Create(m_final_url);

            wrGETURL.Proxy = WebRequest.GetSystemWebProxy();
            wrGETURL.Timeout = 6000;

            try
            {
                Stream objStream = wrGETURL.GetResponse().GetResponseStream();

                StreamReader objReader = new StreamReader(objStream);

                string returnValues = objReader.ReadLine();
                objReader.Close();

                if (returnValues != null && returnValues != "")
                {
                    Framework.Log("New update available!");
                    GetUpdateInfos(returnValues);
                }
                else
                    Framework.Log("No update available");
            }
            catch (Exception ex)
            {
                Framework.Log("ERROR: Unable to access the online update system");
            }
        }

        private void GetUpdateInfos(string getValues)
        {
            string[] finalValues = getValues.Split(';');

            m_newUpdateVersion = finalValues[0];
            m_newUpdateUrl = finalValues[1];

            ShowDialog();
        }

        private void ShowDialog()
        {
            DialogResult dialogResult = MessageBox.Show("Do you want to access to the download page of the " + m_newUpdateVersion + " update?", "New update available!", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (dialogResult == DialogResult.Yes)
            {
                Process.Start(m_newUpdateUrl);
            }
        }
    }
}
